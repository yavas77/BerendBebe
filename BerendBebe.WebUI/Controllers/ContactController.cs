using AutoMapper;
using BerendBebe.Business.Abstract;
using BerendBebe.DTO.ContactDtos;
using BerendBebe.Entities.Concrete;
using BerendBebe.MvcHelpers.Enums;
using BerendBebe.MvcHelpers.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BerendBebe.WebUI.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;
        private readonly IMapper _mapper;

        public ContactController(IContactService contactService, IMapper mapper)
        {
            _contactService = contactService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Send()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Send(ContactSendDto contactSendDto)
        {
            if (!ModelState.IsValid)
                return View().ShowMessage(Status.Error, "Hata", "Eksik veya hatalı bilgiler mevcut!");


            await _contactService.AddAsync(_mapper.Map<Contact>(contactSendDto));

            return RedirectToAction("Index","Home").ShowMessage(Status.Ok,"Başarılı","Mesajınız başarıyla iletilmiş olup en kısa sürede değerlendirilecektir. ");
        }

    }
}
