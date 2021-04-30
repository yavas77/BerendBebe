using AutoMapper;
using BerendBebe.Business.Abstract;
using BerendBebe.DTO.OrderDtos;
using BerendBebe.Entities.Concrete;
using BerendBebe.MvcHelpers.Enums;
using BerendBebe.MvcHelpers.Extensions;
using BerendBebe.MvcHelpers.Mail;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BerendBebe.Entities.Concrete.Order;

namespace BerendBebe.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IOrderDetailService _orderDetailService;
        private readonly IPaymentTypeService _paymentTypeService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService, IOrderDetailService orderDetailService, IPaymentTypeService paymentTypeService, IMapper mapper)
        {
            _orderService = orderService;
            _orderDetailService = orderDetailService;
            _paymentTypeService = paymentTypeService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var OrderAllListInDb = _mapper.Map<List<OrderAdminListDto>>(await _orderService.GetAllOrderDescById());
            return View(OrderAllListInDb);
        }

        [HttpGet]
        public async Task<IActionResult> OrderWaiting()
        {
            var notCompletedOrderInDb = _mapper.Map<List<OrderAdminListDto>>(await _orderService.GetAllReadyOrInCargoSortedByIdAsync());
            return View(notCompletedOrderInDb);
        }

        [HttpGet]
        public async Task<IActionResult> OrderCancelledList()
        {
            var cancelledOrderInDb = _mapper.Map<List<OrderAdminListDto>>(await _orderService.GetAllSortedByIdAsync(OrderStatus.Cancelled));
            return View(cancelledOrderInDb);
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmedOrderList()
        {
            var cancelledOrderInDb = _mapper.Map<List<OrderAdminListDto>>(await _orderService.GetAllSortedByIdAsync(OrderStatus.Confirmed));
            return View(cancelledOrderInDb);
        }

        [HttpGet]
        public async Task<IActionResult> OrderDetail(int? id)
        {
            if (id == null)
                return RedirectToAction("Index").ShowMessage(Status.Error, "Hata", "Hatalı istek! Lütfen yeniden deneyiniz.");

            if (!await _orderService.OrderExistsAsync(id.Value))
                return RedirectToAction("Index").ShowMessage(Status.Error, "Hata", "Sipariş bulunamadı!");

            var orderDetailDto = _mapper.Map<OrderAdminDetailDto>(await _orderService.GetByIdWithPaymentTypeAsync(id.Value));

            var paymentListInDb = await _paymentTypeService.GetAllAsync();
            ViewBag.PaymentTypes = new SelectList(paymentListInDb, "Id", "PaymentName");

            var orderDetailInDb = await _orderDetailService.GetByOrderIdAsync(id.Value);
            orderDetailDto.OrderDetails = orderDetailInDb;
            return View(orderDetailDto);
        }

        [HttpPost]
        public async Task<IActionResult> CargoAdd(OrderCargoAdminAddDto orderCargoAdminAdd)
        {
            if (!ModelState.IsValid)
                return View("OrderDetail", new { id = orderCargoAdminAdd.Id }).ShowMessage(Status.Error, "Hata", "Eksik veya hatalı bilgiler mevcut!");

            if (!await _orderService.OrderExistsAsync(orderCargoAdminAdd.Id))
                return RedirectToAction("Index").ShowMessage(Status.Error, "Hata", "Kargo bilgileri eklenmek istenen sipariş bulunamadı!");

            var orderInDb = await _orderService.FindByIdAsync(orderCargoAdminAdd.Id);

            orderInDb.CargoName = orderCargoAdminAdd.CargoName;
            orderInDb.CargoNo = orderCargoAdminAdd.CargoNo;
            orderInDb.CargoMessage = orderCargoAdminAdd.CargoMessage;

            await _orderService.UpdateAsync(orderInDb);


            return RedirectToAction("OrderDetail", new { id = orderInDb.Id }).ShowMessage(Status.Ok, "Başarılı", "Kargo bilgileri başarıyla eklendi.");
        }

        [HttpPost]
        public async Task<IActionResult> OrderCancel(OrderCancelAdminDto orderCancelAdminDto)
        {
            if (!ModelState.IsValid)
                return View("OrderDetail", new { id = orderCancelAdminDto.Id }).ShowMessage(Status.Error, "Hata", "Eksik veya hatalı bilgiler mevcut!");

            if (!await _orderService.OrderExistsAsync(orderCancelAdminDto.Id))
                return RedirectToAction("Index").ShowMessage(Status.Error, "Hata", "İptal edilmek istenen sipariş bulunamadı!");

            var orderInDb = await _orderService.FindByIdAsync(orderCancelAdminDto.Id);
            orderInDb.CancelDescription = orderCancelAdminDto.CancelDescription;
            orderInDb.Status = OrderStatus.Cancelled;
            orderInDb.LastUpDate = DateTime.Now;


            await _orderService.UpdateAsync(orderInDb);

            return RedirectToAction("Index").ShowMessage(Status.Ok, "Başarılı", "Sipariş başarıyla iptal edeldi");
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmOrder(int? id)
        {
            if (id == null)
                return RedirectToAction("Index").ShowMessage(Status.Error, "Hata", "Hatalı istek! Lütfen yeniden deneyiniz.");

            if (!await _orderService.OrderExistsAsync(id.Value))
                return RedirectToAction("Index").ShowMessage(Status.Error, "Hata", "Kargoya verildi olarak düzenlenmek istenen sipariş bulunamadı!");

            var orderInDb = await _orderService.FindByIdAsync(id.Value);
            orderInDb.LastUpDate = DateTime.Now;
            var message = string.Empty;
            var body = new StringBuilder();
            if (orderInDb.Status == OrderStatus.Ready)
            {
                orderInDb.Status = OrderStatus.InCargo;
                message = "Ürün kargoya verildi olarak düzenlendi.";
                try
                {

                    body.AppendLine($"Merhaba, {orderInDb.Name} {orderInDb.LastName},");
                    body.AppendLine($"<br />");
                    body.AppendLine($"<br />");
                    body.AppendLine($"<b>{orderInDb.Id}</b> numaralı teslimatını, kargoraya verdik");
                    body.AppendLine($"<br />");
                    body.AppendLine($"Güzel günlerde kullanmanız dileğiyle, iyi günler dileriz.");
                    body.AppendLine($"<br />");
                    body.AppendLine("<hr />");
                    body.AppendLine($"<br />");
                    body.AppendLine("<b>Kargo Bilgileri: </b>");
                    body.AppendLine($"<br />");
                    body.AppendLine($"<br />");
                    body.AppendLine($"<b>Firma : </b> {orderInDb.CargoName}");
                    body.AppendLine($"<br />");

                    body.AppendLine($"<b>Kargo No: </b> {orderInDb.CargoNo}");


                    SendMail.MailSender($"BerendBebe - Sipariş Takip", body.ToString(), orderInDb.Email);

                }
                catch (Exception)
                {

                    //return View(contactSendDto).ShowMessage(Status.Error, "Hata", "Mail gönderme işlemi sırasında hata oluştu!");
                }
            }
            else if (orderInDb.Status == OrderStatus.InCargo)
            {
                orderInDb.Status = OrderStatus.Confirmed;
                message = "Ürün teslim edildi olarak düzenlendi.";

                body.AppendLine($"Merhaba, {orderInDb.Name} {orderInDb.LastName},");
                body.AppendLine($"<br />");
                body.AppendLine($"<br />");
                body.AppendLine($"<b>{orderInDb.Id}</b> numaralı siparişini teslim ettik.");
                body.AppendLine($"<br />");
                body.AppendLine($"Alışverişinde bizi tercih ettiğin için teşekkür ederiz.");
                body.AppendLine("<hr />");
                body.AppendLine($"<br />");
                body.AppendLine($"<b>Teslimat Adresi : </b></br> {orderInDb.Address}");
                body.AppendLine($"<br />");

                SendMail.MailSender($"BerendBebe - Siparişiniz Teslim Edildi", body.ToString(), orderInDb.Email);


            }


            await _orderService.UpdateAsync(orderInDb);
            return RedirectToAction("Index").ShowMessage(Status.Ok, "Başarılı", message);
        }

        public async Task<IActionResult> OrderUpdate(OrderUpdateAdminDto orderUpdateAdminDto)
        {
            if (!ModelState.IsValid)
                return View("OrderDetail", new { id = orderUpdateAdminDto.Id }).ShowMessage(Status.Error, "Uyarı", "Eksik veya hatalı bilgiler mevcut!");

            if (!await _orderService.OrderExistsAsync(orderUpdateAdminDto.Id))
                return RedirectToAction("Index").ShowMessage(Status.Error, "Hata", "Güncellenmek istenen sipariş bulunamadı!");

            var order = _mapper.Map<Order>(orderUpdateAdminDto);
            await _orderService.UpdateAsync(order);

            return RedirectToAction("OrderDetail", new { id = orderUpdateAdminDto.Id }).ShowMessage(Status.Ok, "Başarılı", "Kişi bilgileri güncellendi.");
        }

        [HttpPost]
        public async Task<IActionResult> OrderProductDelete(OrderDeleteAdminDto orderDeleteAdminDto)
        {

            if (!await _orderDetailService.OrderDetailExistsAsync(orderDeleteAdminDto.OrderId, orderDeleteAdminDto.Id))
                return Json(new JResult
                {
                    Status = Status.BadRequest,
                    Message = "Silinmek istenen ürün detay bilgisi bulunamadı!"
                });

            var orderDetailIndDb = await _orderDetailService.FindByIdAsync(orderDeleteAdminDto.Id);

            await _orderDetailService.RemoveAsync(orderDetailIndDb);


            return Json(new JResult
            {
                Status = Status.Ok,
                Message = "Sipariş siparişten kaldırıldı."
            });
        }

        [HttpPost]
        public async Task<IActionResult> OrderUpdateQuantity(OrderDetailUpdateAdminDto orderDetailUpdateAdminDto)
        {
            if (!ModelState.IsValid)
                return View("OrderDetail", new { id = orderDetailUpdateAdminDto.Id }).ShowMessage(Status.Error, "Uyarı", "Ürün miktarı girilmelidir!");

            if (!await _orderService.OrderExistsAsync(orderDetailUpdateAdminDto.Id))
                return RedirectToAction("Index").ShowMessage(Status.Error, "Hata", "Güncellenmek istenen sipariş bulunamadı!");

            var orderInDb = await _orderService.FindByIdAsync(orderDetailUpdateAdminDto.Id);
            await _orderService.UpdateAsync(orderInDb);

            return RedirectToAction("OrderDetail", new { id = orderDetailUpdateAdminDto.Id }).ShowMessage(Status.Ok, "Başarılı", "Ürün miktarı güncellendi.");
        }


    }

}

