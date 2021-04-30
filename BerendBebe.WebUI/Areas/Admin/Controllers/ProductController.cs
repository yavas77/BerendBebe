using AutoMapper;
using BerendBebe.Business.Abstract;
using BerendBebe.DTO.CategoryDtos;
using BerendBebe.DTO.ProductDtos;
//using BerendBebe.DTO.ProductDtos;
using BerendBebe.Entities.Concrete;
using BerendBebe.MvcHelpers.Enums;
using BerendBebe.MvcHelpers.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BerendBebe.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IProductImageService _productImageService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, ICategoryService categoryService, IProductImageService productImageService, IMapper mapper)
        {
            _productService = productService;
            _categoryService = categoryService;
            _productImageService = productImageService;
            _mapper = mapper;
        }

        void FillCategories()
        {
            var categoryListDto = _mapper.Map<List<CategoryListDto>>(_categoryService.GetAllByActiveAsync().Result);
            ViewBag.Categories = new SelectList(categoryListDto, "Id", "CategoryName");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var productListDto = _mapper.Map<List<ProductListDto>>(await _productService.GetAllWithCategoryAsync());
            return View(productListDto);
        }

        [HttpGet]
        public IActionResult Add()
        {
            FillCategories();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductAddDto productAddDto)
        {
            if (!ModelState.IsValid)
            {
                return View(productAddDto);
            }

            //if (await _productService.ProductExistsAsync(productAddDto.ProductName, (int)productAddDto.CategoryId))
            //    return RedirectToAction("Index").ShowMessage(Status.Error, "Hata", "Eklenmek istenen ürün aynı kategoride zaten mevcut!");

            var product = _mapper.Map<Product>(productAddDto);

            await _productService.AddAsync(product);

            return RedirectToAction("Index").ShowMessage(Status.Ok, "Başarılı", $"{product.ProductName} adlı ürün başarıyla eklendi.");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {

            if (!await _productService.ProductExistsAsync(id))
                return RedirectToAction("Index").ShowMessage(Status.Error, "Uyarı", "Hatalı istek!");

            var productInDb = await _productService.FindByIdAsync(id);
            if (productInDb == null)
                return RedirectToAction("Index").ShowMessage(Status.Error, "Hata", "Güncellenmek istenen ürün bulunamadı!");

            var productUpdateDto = _mapper.Map<ProductUpdateDto>(productInDb);

            FillCategories();
            return View(productUpdateDto);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductUpdateDto productUpdateDto)
        {
            if (!await _productService.ProductExistsAsync(productUpdateDto.Id))
                return RedirectToAction("Index").ShowMessage(Status.Error, "Uyarı", "Hatalı istek!");

            if (!ModelState.IsValid)
                return RedirectToAction("Index").ShowMessage(Status.Error, "Hata", "Güncellenmek istenen ürün bulunamadı!");


            var product = _mapper.Map<Product>(productUpdateDto);

            await _productService.UpdateAsync(product);

            return RedirectToAction("Index").ShowMessage(Status.Ok, "Başarılı", $"<strong>{product.ProductName}</strong> adlı ürün başarıyla güncellendi .");
        }

        [HttpPost]
        public async Task<JsonResult> Delete(ProductDeleteDto productDeleteDto)
        {
            if (productDeleteDto == null)
            {
                return Json(new JResult
                {
                    Status = Status.BadRequest,
                    Message = "Silinmek istenen ürün bulunamadı!"
                });
            }

            if (!await _productService.ProductExistsAsync(productDeleteDto.Id))
            {
                return Json(new JResult
                {
                    Status = Status.NotFound,
                    Message = "Silinmek istenen ürün bulunamadı!"
                });
            }

            var product = await _productService.FindByIdAsync(productDeleteDto.Id);
            product.IsDelete = false;

            await _productService.UpdateAsync(product);


            return Json(new JResult
            {
                Status = Status.Ok,
                Message = "Ürün başarıyla silindi."
            });
        }

        [HttpGet]
        public async Task<IActionResult> ProductImageAdd(int? id)
        {
            if (id == null)
                return RedirectToAction("Index").ShowMessage(Status.Error, "Uyarı", "Hatalı talep! Lütfen yeniden deneyiniz.");

            if (!await _productService.ProductExistsAsync(id.Value))
                return RedirectToAction("Index").ShowMessage(Status.Error, "Hata", "Resim eklenmek istenen ürün bulunamadı!");

            var product = await _productService.FindByIdAsync(id.Value);
            var productImageDto = _mapper.Map<ProductImageAddDto>(product);
            return View(productImageDto);
        }

        [HttpPost]
        public async Task<IActionResult> ProductImageAdd(ProductImageAddDto productImageAddDto, IFormFile[] file)
        {
            if (file == null)
            {
                return View().ShowMessage(Status.Error, "Hata", "Lütfen resim seçiniz!");
            }




            //try
            //{
            foreach (var image in file)
            {
                //    image = new Bitmap(image, 1080, 1300);
                //using (Graphics g = Graphics.FromImage((System.Drawing.Image)yeniimg))
                //    g.DrawImage(img, 0, 0, genislik, yukseklik);

                //yeniimg.Save(Server.MapPath("resimler/") + "yontem1.jpeg");


                string imageExtension = Path.GetExtension(image.FileName);
                string imageName = Guid.NewGuid() + imageExtension;

                string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/img/{imageName}");

                using var stream = new FileStream(path, FileMode.Create);


                await image.CopyToAsync(stream);


                productImageAddDto.ImageTitle = image.FileName;
                productImageAddDto.ImageUrl = imageName;

                var productImage = _mapper.Map<ProductImage>(productImageAddDto);
                await _productImageService.AddAsync(productImage);
            }
            //}
            //catch (Exception)
            //{

            //    return View().ShowMessage(Status.Error, "Hata", "Resimler yüklenirken hata oluştu lütfen yeniden deneyiniz!");
            //}



            return RedirectToAction("ProductImageList", new { id = productImageAddDto.ProductId }).ShowMessage(Status.Ok, "Başarılı", "Resimler başarıyla yüklendi.");
        }

        [HttpGet]
        public async Task<IActionResult> ProductImageList(int? id)
        {
            if (id == null)
                return RedirectToAction("Index").ShowMessage(Status.Error, "Hata", "Hatalı istek! Lütfen yeniden deneyiniz.");

            if (!await _productService.ProductExistsAsync(id.Value))
                return RedirectToAction("Index").ShowMessage(Status.Error, "Hata", "Ürün bulunamadı!");

            var ProductImageListDto = _mapper.Map<ProductImageListDto>(await _productService.FindByIdAsync(id.Value));
            ProductImageListDto.ProductImages = _mapper.Map<List<ProductImageDetailListDto>>(await _productImageService.GetAllByProductId(id.Value));

            return View(ProductImageListDto);
        }

        [HttpGet]
        public async Task<IActionResult> ProductImageUpdate(int? id)
        {
            var productImageInDb = await _productImageService.GetWithProductByIdAsync(id.Value);
            if (productImageInDb == null)
                return RedirectToAction("Index").ShowMessage(Status.Error, "Hata", "Ürün resmi bulunamadı!");

            if (!await _productService.ProductExistsAsync(productImageInDb.ProductId))
                return RedirectToAction("Index").ShowMessage(Status.Error, "Hata", "Ürün bulunamadı!");

            var productInDb = await _productService.FindByIdAsync(productImageInDb.ProductId);
            productInDb.ShowImageUrl = productImageInDb.ImageUrl;
            await _productService.UpdateAsync(productInDb);

            return RedirectToAction("Index").ShowMessage(Status.Ok, "Başarılı", "Ürün ana resmi başarıyla değiştirildi.");
        }

        [HttpPost]
        public async Task<IActionResult> ImageDelete(ProductDeleteDto productDeleteDto)
        {
            if (productDeleteDto == null)
            {
                return Json(new JResult
                {
                    Status = Status.BadRequest,
                    Message = "Hatalı istek! Lütfen yeniden deneyiniz."
                });
            }

            if (!await _productImageService.ProductImageExistsAsync(productDeleteDto.Id))
            {
                return Json(new JResult
                {
                    Status = Status.NotFound,
                    Message = "Silinmek istenen resim bulunamadı!"
                });
            }

            var productImageInDb = await _productImageService.FindByIdAsync(productDeleteDto.Id);

            await _productImageService.RemoveAsync(productImageInDb);


            return RedirectToAction("Index").ShowMessage(Status.Ok, "Baraşılı", "Resim başarıyla silindi");
        }

    }
}
