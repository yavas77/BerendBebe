using AutoMapper;
using BerendBebe.Business.Abstract;
using BerendBebe.DTO.ContactDtos;
using BerendBebe.Entities.Concrete;
using BerendBebe.MvcHelpers.Enums;
using BerendBebe.MvcHelpers.Extensions;
using BerendBebe.MvcHelpers.Mail;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerendBebe.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class MailBoxController : Controller
    {
        private readonly IContactService _contactService;
        private readonly IMapper _mapper;

        public MailBoxController(IContactService contactService, IMapper mapper)
        {
            _contactService = contactService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var mailListDto = _mapper.Map<List<ContactListAdminDto>>(await _contactService.GetIncomingShortByIdAsync());
            return View(mailListDto);
        }

        public async Task<IActionResult> SendedMail()
        {
            var sendedMailListDto = _mapper.Map<List<SendedMailListDto>>(await _contactService.GetAllSendedShortByIdAsync());
            return View(sendedMailListDto);
        }

        public async Task<IActionResult> Trash()
        {
            var mailListDto = _mapper.Map<List<ContactListAdminDto>>(await _contactService.GetAllIsDeleteShortByIdAsync());
            return View(mailListDto);
        }

        [HttpGet]
        public async Task<IActionResult> Reply(int? id)
        {
            if (id == null)
                return RedirectToAction("Index").ShowMessage(Status.Error, "Hata", "Hatalı İstek! Lütfen yeniden deneyiniz.");


            if (!await _contactService.MailExistsAsync(id.Value))
                return RedirectToAction("Index").ShowMessage(Status.Error, "Hata", "Talep edilen mail bulunamadı!");


            var contactComposeDto = _mapper.Map<ContactReplyDto>(await _contactService.FindByIdAsync(id.Value));

            return View(contactComposeDto);
        }

        [HttpPost]
        public async Task<IActionResult> Reply(ContactReplyDto contactComposeDto)
        {
            if (!ModelState.IsValid)
                return View(contactComposeDto).ShowMessage(Status.Error, "Hata", "Eksik veya hatalı bilgiler mevcut!");



            if (!await _contactService.MailExistsAsync(contactComposeDto.Id))
                return RedirectToAction("Index").ShowMessage(Status.Error, "Hata", "Cevaplanmak istenen mail bulunamadı!");



            var contactInDb = await _contactService.FindByIdAsync(contactComposeDto.Id);
            contactInDb.ReplyMessage = contactComposeDto.Message;

            await _contactService.UpdateAsync(contactInDb);

            try
            {
                var body = new StringBuilder();
                body.AppendLine($"Konu:  {contactComposeDto.Subject}");
                body.AppendLine($"Mesaj: {contactComposeDto.Message}");
                SendMail.MailSender($"BerendBebe - {contactComposeDto.Subject}", body.ToString(), contactComposeDto.EmailAdress);
            }
            catch (Exception)
            {

                return View(contactComposeDto).ShowMessage(Status.Error, "Hata", "Mail gönderme işlemi sırasında hata oluştu!");
            }

            return RedirectToAction("Index").ShowMessage(Status.Ok, "Başarılı", "Mail başarıyla gönderildi.");
        }

        [HttpGet]
        public IActionResult Compose()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Compose(ContactComposeDto contactSendDto)
        {
            if (!ModelState.IsValid)
                return View(contactSendDto).ShowMessage(Status.Error, "Hata", "Eksik veya hatalı bilgiler mevcut!");

            try
            {
                var body = new StringBuilder();
                body.AppendLine($"Konu:  {contactSendDto.Subject}");
                body.AppendLine($"Mesaj: {contactSendDto.Message}");
                SendMail.MailSender($"BerendBebe - {contactSendDto.Subject}", body.ToString(), contactSendDto.EmailAdress);

                var contact = _mapper.Map<Contact>(contactSendDto);
                await _contactService.AddAsync(contact);
            }
            catch (Exception)
            {

                return View(contactSendDto).ShowMessage(Status.Error, "Hata", "Mail gönderme işlemi sırasında hata oluştu!");
            }

            return RedirectToAction("Index").ShowMessage(Status.Ok, "Başarılı", "Mail başarıyla gönderildi.");
        }


        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
                return RedirectToAction("Index").ShowMessage(Status.Error, "Hata", "Hatalı İstek! Lütfen yeniden deneyiniz.");


            if (!await _contactService.MailExistsAsync(id.Value))
                return RedirectToAction("Index").ShowMessage(Status.Error, "Hata", "Talep edilen mail bulunamadı!");

            var contactInDb = await _contactService.FindByIdAsync(id.Value);

            contactInDb.IsActive = false;

            await _contactService.UpdateAsync(contactInDb);

            var contactDto = _mapper.Map<ContactDetailAdminDto>(contactInDb);
            return View(contactDto);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return RedirectToAction("Index").ShowMessage(Status.Error, "Hata", "Hatalı İstek! Lütfen yeniden deneyiniz.");


            if (!await _contactService.MailExistsAsync(id.Value))
                return RedirectToAction("Index").ShowMessage(Status.Error, "Hata", "Talep edilen mail bulunamadı!");


            var contactInDb = await _contactService.FindByIdAsync(id.Value);

            contactInDb.IsActive = true;
            contactInDb.IsDelete = true;

            await _contactService.UpdateAsync(contactInDb);


            return RedirectToAction("Index").ShowMessage(Status.Ok, "Başarılı", "Mail okunmadı olarak güncellendi.");
        }

        public async Task<IActionResult> Delete(ContactDelete contactDelete)
        {
            if (contactDelete == null)
                return Json(new JResult
                {
                    Status = Status.BadRequest,
                    Message = "Silinmek istenen mail bulunamadı"
                });



            if (!await _contactService.MailExistsAsync(contactDelete.Id))
                return Json(new
                {
                    Status = Status.NotFound,
                    Message = "Silinmek istenen mail bulunamadı!"

                });

            var contactInDb = await _contactService.FindByIdAsync(contactDelete.Id);
            string message = "";

            if (contactInDb.IsDelete)
            {
                contactInDb.IsDelete = false;
                message = "Mail çöp kutusuna gönderildi.";

                await _contactService.UpdateAsync(contactInDb);
            }
            else
            {
                await _contactService.RemoveAsync(contactInDb);
                message = "Mail tamamen silindi!";

            }


            return Json(new JResult
            {
                Status = Status.Ok,
                Message = message
            });




        }
    }

}

