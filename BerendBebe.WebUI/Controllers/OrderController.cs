using AutoMapper;
using BerendBebe.Business.Abstract;
using BerendBebe.DTO.OrderDtos;
using BerendBebe.Entities.Concrete;
using BerendBebe.MvcHelpers.Enums;
using BerendBebe.MvcHelpers.Extensions;
using BerendBebe.MvcHelpers.Mail;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerendBebe.WebUI.Controllers
{
    public class OrderController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;
        private readonly IPaymentTypeService _paymentTypeService;
        private readonly IOrderDetailService _orderDetailService;
        private readonly IProductService _productService;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public OrderController(ICartService cartService, IOrderService orderService, IPaymentTypeService paymentTypeService, IOrderDetailService orderDetailService, IProductService productService, UserManager<User> userManager, IMapper mapper)
        {
            _cartService = cartService;
            _orderService = orderService;
            _paymentTypeService = paymentTypeService;
            _orderDetailService = orderDetailService;
            _userManager = userManager;
            _productService = productService;
            _mapper = mapper;
        }

        public async Task<IActionResult> ConfirmOrder()
        {
            var cartsInDb = await _cartService.GetAllWithProductAsync(HttpContext.GetCookie("BerendBebeCart"));

            var orderAddDto = _mapper.Map<OrderAddDto>(new Order());
            orderAddDto.Carts = cartsInDb;

            var paymentTypesInDb = await _paymentTypeService.GetAllAsync();
            orderAddDto.PaymentTypes = paymentTypesInDb;


            return View(orderAddDto);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmOrder(OrderAddDto orderAddDto)
        {
            var cartsInDbList = await _cartService.GetAllWithProductAsync(HttpContext.GetCookie("BerendBebeCart"));
            if (!ModelState.IsValid)
            {
                orderAddDto.Carts = cartsInDbList;
                orderAddDto.PaymentTypes = await _paymentTypeService.GetAllAsync();
                return View(orderAddDto).ShowMessage(Status.Error, "Hata", "Eksik veya hatalı bilgiler mevcut");
            }



            var order = _mapper.Map<Order>(orderAddDto);




            var savedOrder = await _orderService.AddAsync(order);



            foreach (var cart in cartsInDbList)
            {
                OrderDetail orderDetail = new OrderDetail
                {
                    ProductId = cart.ProductId,
                    OrderId = savedOrder.Id,
                    Price = cart.Product.Price,
                    ProductName = cart.Product.ProductName,
                    Quantity = cart.Quantity,
                    ShowImageUrl = cart.Product.ShowImageUrl
                };

                //Ürün stok kontrol işlemi
                if (!await _productService.ProductUnitInStokCountControlAsync(cart.ProductId, cart.Quantity))
                {
                    var productInDb = await _productService.FindByIdAsync(cart.ProductId);
                    orderAddDto.Carts = cartsInDbList;
                    orderAddDto.PaymentTypes = await _paymentTypeService.GetAllAsync();
                    await _orderService.RemoveAsync(savedOrder);
                    return View(orderAddDto).ShowMessage(Status.Error, "Hata", "Sepetinizde stoğu yeterli olmayan ürünler bulunmaktadrı.");
                }

                await _orderDetailService.AddAsync(orderDetail);
            }

            await _cartService.RemoveAsync(cartsInDbList);

            try
            {
                var body = new StringBuilder();
                body.AppendLine($"Merhaba {orderAddDto.Name} {orderAddDto.LastName},");
                body.AppendLine($"<br />");
                body.AppendLine($"<br />");
                body.AppendLine($"<b>{savedOrder.Id}</b> numaralı siparişini aldık. Alışverişinde bizi tercih ettiğiniz için teşekkür ederiz.");
                body.AppendLine($"<br />");
                body.AppendLine("<hr />");
                body.AppendLine($"<br />");
                body.AppendLine("<b>Teslimat Adresi: </b> ");
                body.AppendLine($"<br />");
                body.AppendLine($"{orderAddDto.Address} {orderAddDto.City}");


                SendMail.MailSender($"BerendBebe - Siparişini Aldık", body.ToString(), orderAddDto.Email);

                body.Clear();

                var userInDb = await _userManager.FindByNameAsync("admin");

                body.AppendLine($"Yeni bir siparişiniz var.");
                body.AppendLine($"<br />");
                body.AppendLine($"<a href=\"https://berendbebe.com/Admin/Order/OrderDetail/2\"{savedOrder.Id}\">Sipiş Detayına Git</a>");
                body.AppendLine($"<br />");
                body.AppendLine("<hr />");
                body.AppendLine($"<br />");
                body.AppendLine("<b>Teslimat Adresi: </b> ");
                body.AppendLine($"<br />");
                body.AppendLine($"{orderAddDto.Address} {orderAddDto.City}");


                SendMail.MailSender($"BerendBebe - Yeni Sipariş", body.ToString(), orderAddDto.Email);

            }
            catch (Exception)
            {

                //return View(contactSendDto).ShowMessage(Status.Error, "Hata", "Mail gönderme işlemi sırasında hata oluştu!");
            }

            return RedirectToAction("Index", "Product").ShowMessage(Status.Ok, "Başarılı", "Siparişiniz alınmıştır. Onaylanıp kargoya verildikten sonra tarafınıza bilgi verilecektir.");
        }
    }
}
