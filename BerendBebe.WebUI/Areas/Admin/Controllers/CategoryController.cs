using AutoMapper;
using BerendBebe.Business.Abstract;
using BerendBebe.DTO.CategoryDtos;
using BerendBebe.Entities.Concrete;
using BerendBebe.MvcHelpers.Enums;
using BerendBebe.MvcHelpers.Extensions;
using BerendBebe.WebUI.Areas.Admin.Models.CategoryModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BerendBebe.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(IMapper mapper, ICategoryService categoryService)
        {
            _categoryService = categoryService;
            _mapper = mapper;

        }

        public async Task<IActionResult> Index(string s)
        {

            //List<CategoryListViewModel> models = new List<CategoryListViewModel>();

            //var categories = await _categoryService.GetAllAsync();


            //foreach (var category in categories)
            //{
            //    CategoryListViewModel model = new CategoryListViewModel
            //    {
            //        CategoryName = category.CategoryName,
            //        IsActive = category.IsActive
            //    };
            //    models.Add(model);
            //};

            var categories = _mapper.Map<List<CategoryListDto>>(await _categoryService.GetAllNotDeletedAsync(s));

            return View(categories);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(CategoryAddDto categoryAddDto)
        {
            if (!ModelState.IsValid)
            {
                return View(categoryAddDto).ShowMessage(Status.Error, "Hata", "Eksik veya hatalı bilgiler mevcut!");
            }

            var category = _mapper.Map<Category>(categoryAddDto);

            await _categoryService.AddAsync(category);
            return RedirectToAction("Index").ShowMessage(Status.Ok, "Başarılı", "Kategori başarıyla eklendi!");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
                return RedirectToAction().ShowMessage(Status.Error, "Uyarı", "Talep hatalı lütfen menüleri kullanarak yeniden deneyiniz!");

            var categoryUpdateDto = _mapper.Map<CategoryUpdateDto>(await _categoryService.FindByIdAsync(id.Value));

            return View(categoryUpdateDto);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CategoryUpdateDto categoryUpdateDto)
        {
            var category = _mapper.Map<Category>(categoryUpdateDto);

            if (category == null)
                return RedirectToAction("Index").ShowMessage(Status.Error, "Uyarı", "Güncellenmek istenen kategori bulunamadı!");

            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index").ShowMessage(Status.Error, "Error", "Eksik veya hatalı bilgiler mevcut!");
            }

            await _categoryService.UpdateAsync(category);

            return RedirectToAction("Index").ShowMessage(Status.Ok, "Başarılı", $"<strong>{category.CategoryName}</strong> adlı kategori başarıyla güncellendi.");
        }


        [HttpPost]
        public async Task<JsonResult> Delete([FromBody] CategoryDeleteDto categoryDeleteDto)
        {
            if (categoryDeleteDto == null)
            {
                return Json(new JResult
                {
                    Status = Status.BadRequest,
                    Message = "Silinmek istenen kategori bulunamadı!"
                });
            }

            if (!await _categoryService.CategoryExistsAsync(categoryDeleteDto.Id))
            {
                return Json(new JResult
                {
                    Status = Status.NotFound,
                    Message = "Silinmek istenen kategori bulunamadı!"
                });
            }

            var category = await _categoryService.FindByIdAsync(categoryDeleteDto.Id);
            category.IsDelete = false;

            await _categoryService.UpdateAsync(category);


            return Json(new JResult
            {
                Status = Status.Ok,
                Message = "Kategori başarıyla silindi."
            });



        }

    }
}
