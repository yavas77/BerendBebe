using AutoMapper;
using BerendBebe.Business.Abstract;
using BerendBebe.DTO.CommentDtos;
using BerendBebe.Entities.Concrete;
using BerendBebe.MvcHelpers.Enums;
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
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public CommentController(ICommentService commentService, UserManager<User> userManager, IMapper mapper)
        {
            _commentService = commentService;
            _userManager = userManager;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Add(CommentAddDto commentAddDto)
        {

            if (!ModelState.IsValid)
                return Json(new JResult
                {
                    Status = Status.BadRequest,
                    Message = "Eksik veya hatalı kayıtlar mevcut!"
                });


            var comment = _mapper.Map<Comment>(commentAddDto);

            await _commentService.AddAsync(comment);

            try
            {
                var userInDb = await _userManager.FindByNameAsync("admin");
                var body = new StringBuilder();
                body.AppendLine($"Ad Soyad:  {commentAddDto.Name}");
                body.AppendLine($"<br />");
                body.AppendLine($"Email: {commentAddDto.EmailAdress}");
                body.AppendLine($"<br />");
                body.AppendLine($"Mesaj : {commentAddDto.Content}");
                SendMail.MailSender($"BerendBebe - Yeni Yorum", body.ToString(), userInDb.Email);

            }
            catch (Exception)
            {


            }

            return Json(new JResult
            {
                Status = Status.Ok,
                Message = "Yorumunuz alınmıştır. Değerlendikten sonra onaylanacaktır."
            });
        }

    }

}
