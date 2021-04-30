using AutoMapper;
using BerendBebe.Business.Abstract;
using BerendBebe.DTO.NotificationDtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerendBebe.WebUI.Areas.Admin.ViewComponents
{
    [Area("Admin ")]
    public class NotificationViewComponent : ViewComponent
    {
        private readonly IOrderService _orderService;
        private readonly ICommentService _commentService;
        private readonly IContactService _contactService;
        private readonly IMapper _mapper;

        public NotificationViewComponent(IOrderService orderService, ICommentService commentService, IContactService contactService, IMapper mapper)
        {
            _orderService = orderService;
            _commentService = commentService;
            _contactService = contactService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var ordersInDb = await _orderService.GetAllGettingReadyAsync();
            var commentInDb = await _commentService.GetAllPassiveAsync();
            var contactInDb = await _contactService.GetIncomingByActiveAsync();
            var model = new NewNotificationDto
            {
                OrderCount = ordersInDb.Count,
                CommentCount = commentInDb.Count,
                MailCount = contactInDb.Count,
                CountTotal = ordersInDb.Count + commentInDb.Count + contactInDb.Count
            };

            return View(model);
        }
    }
}
