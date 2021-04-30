
using AutoMapper;
using BerendBebe.Business.Abstract;
using BerendBebe.DTO.CartDtos;
using BerendBebe.Entities.Concrete;
using BerendBebe.MvcHelpers.Enums;
using BerendBebe.MvcHelpers.Extensions;
using BerendBebe.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BerendBebe.WebUI.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public CartController(ICartService cartService, IProductService productService, IMapper mapper)
        {
            _cartService = cartService;
            _productService = productService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var cart = await _cartService.GetAllWithProductAsync(HttpContext.GetCookie("BerendBebeCart"));
            var cartListDto = _mapper.Map<List<CartListDto>>(cart);

            return View(cartListDto);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CartAddDto cartAddDto)
        {
            //if (ModelState.IsValid)
            //    return View(cartAddDto).ShowMessage(Status.Error, "Hata", "Eksik veya hatalı bilgiler mevcut!");




            if (!HttpContext.HasCookie("BerendBebeCart"))
            {
                var guid = Guid.NewGuid().ToString();
                HttpContext.SetCookie("BerendBebeCart", guid, TimeSpan.FromDays(5));
                cartAddDto.Customer = guid;
            }
            else
            {
                cartAddDto.Customer = HttpContext.GetCookie("BerendBebeCart");
            }

            if (!ModelState.IsValid)
                return Json(new CartResultModel
                {
                    Status = Status.Error,
                    Message = "Miktar giriniz!"
                });

            var productInDb = await _productService.FindByIdAsync(cartAddDto.ProductId);
            //Ürün stok ile girilen miktar kontrol ediliyor.
            if (!await _productService.ProductUnitInStokCountControlAsync(cartAddDto.ProductId, cartAddDto.Quantity))
            {

                return Json(new CartResultModel
                {
                    Status = Status.Error,
                    Message = $"Stokta {productInDb.UnitInStok} adet mevcut! Lüften girdiğiniz miktarı kontrol edeniz."
                });
            }





            var cart = _mapper.Map<Cart>(cartAddDto);
            int cartProductQuantity = 0;
            decimal cartProductTotal = 0;
            string message = string.Empty;

            if (!await _cartService.ProductExistsInCartAsync(cart.ProductId, cartAddDto.Customer))
            {
                cartProductTotal = cart.Quantity * productInDb.Price;
                await _cartService.AddAsync(cart);
                message = "Ürün sepete eklendi.";
            }
            else
            {
                var cartInDb = await _cartService.GetCartByProductWithCustomerAsync(cart.ProductId, cartAddDto.Customer);
                //Ürün stok ile girilen miktar+sepetteki toplam miktar kontrol ediliyor.
                if (!await _productService.ProductUnitInStokCountControlAsync(cartAddDto.ProductId, cartAddDto.Quantity + cartInDb.Quantity))
                {

                    return Json(new CartResultModel
                    {
                        Status = Status.Error,
                        Message = $"Sepetinizdeki miktar ile girilen miktar toplamı stok toplamından fazla!"
                    });
                }

                cartInDb.Quantity = cart.Quantity + cartInDb.Quantity;
                cartProductTotal = cartInDb.Quantity * productInDb.Price;
                cartProductQuantity = cartInDb.Quantity;



                if (cartInDb.Quantity == 0)
                {
                    await _cartService.RemoveAsync(cartInDb);
                    message = "Ürün sepetten kaldırıldı.";
                }
                else if (cartAddDto.Quantity < 0)
                {
                    message = "Ürün sepetten düşüldü.";
                    await _cartService.UpdateAsync(cartInDb);
                }
                else
                {
                    message = "Ürün sepete eklendi!.";
                    await _cartService.UpdateAsync(cartInDb);
                }
            }


            var count = await _cartService.GetAllWithProductAsync(cartAddDto.Customer);


            return Json(new CartResultModel
            {
                Id = cartAddDto.ProductId,
                Status = Status.Ok,
                Message = message,
                Quantity = cartProductQuantity,
                Total = cartProductTotal.ToString(),
                Count = $"({count.Count})"
            });

        }

        public async Task<JsonResult> Delete(CartDeleteDto cartDeleteDto)
        {
            if (cartDeleteDto == null)
                return Json(new JResult
                {
                    Status = Status.BadRequest,
                    Message = "Silinmek istenen ürün bulunamadı"
                });

            if (!await _cartService.CartExistsAsync(cartDeleteDto.Id))
                return Json(new JResult
                {
                    Status = Status.BadRequest,
                    Message = "Silinmek istenen ürün sepette bulunamadı"
                });

            var cart = _mapper.Map<Cart>(cartDeleteDto);

            await _cartService.RemoveAsync(cart);

            return Json(new JResult
            {
                Status = Status.Ok,
                Message = "Ürün sepetten kaldırıldı."
            });

        }
    }
}
