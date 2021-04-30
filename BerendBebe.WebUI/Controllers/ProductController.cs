using AutoMapper;
using BerendBebe.Business.Abstract;
using BerendBebe.DTO.ProductDtos;
using BerendBebe.MvcHelpers.Enums;
using BerendBebe.MvcHelpers.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BerendBebe.WebUI.Controllers
{

    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, ICategoryService categoryService, IMapper mapper)
        {
            _productService = productService;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(int? categoryId, string s)
        {
            if (categoryId != null)
            {
                if (await _categoryService.CategoryExistsAsync(categoryId.Value))
                {
                    var productListCategoryId = _mapper.Map<List<ProductListDto>>(await _productService.GetAllByCategoryIdIsActiveAsync(categoryId.Value));
                    var categoryInDb = await _categoryService.FindByIdAsync(categoryId.Value);
                    ViewBag.CategorySearch = categoryInDb.CategoryName;

                    return View(productListCategoryId);
                }
            }

            if (!string.IsNullOrWhiteSpace(s))
            {

                var productListFilter = _mapper.Map<List<ProductListDto>>(await _productService.GetAllByFilterInProductNameAsync(s));
                ViewBag.Search = s;
                return View(productListFilter);
            }

            var productListDto = _mapper.Map<List<ProductListDto>>(await _productService.GetAllByActiveAsync());
            return View(productListDto);
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
                return RedirectToAction("Index").ShowMessage(Status.Error, "Uyarı", "Hatalı istek! Lütfen yeniden deneyiniz.");

            if (!await _productService.ProductExistsByActiveAsync(id.Value))
                return RedirectToAction("Index").ShowMessage(Status.Error, "Hata", "Talep edilen ürün bulunamadı!");

            var productDetailDto = _mapper.Map<ProductDetailListDto>(await _productService.FindByIdAsync(id.Value));

            return View(productDetailDto);
        }
    }
}
