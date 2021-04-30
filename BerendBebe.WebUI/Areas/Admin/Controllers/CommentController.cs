using AutoMapper;
using BerendBebe.Business.Abstract;
using BerendBebe.DTO.CommentDtos;
using BerendBebe.MvcHelpers.Enums;
using BerendBebe.MvcHelpers.Extensions;
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
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public CommentController(ICommentService commentService, IMapper mapper)
        {
            _commentService = commentService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var commentAdminListDto = _mapper.Map<List<CommentAdminListDto>>(await _commentService.GetAllWithProducAsync());
            return View(commentAdminListDto);
        }


        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
                return RedirectToAction("Index").ShowMessage(Status.Error, "Uyarı", "Hatalı istek! Lüftyen yeniden deneyiniz.");



            if (!await _commentService.CommentExistsAsync(id.Value))
                return RedirectToAction("Index").ShowMessage(Status.Error, "Hata", "Talep edilen yorum bulunamadı!");

            var commentDetailDto = _mapper.Map<CommentDetailAdminDto>(await _commentService.FindByIdAsync(id.Value));

            return View(commentDetailDto);
        }

        [HttpGet]
        public async Task<IActionResult> Confirm(int? id)
        {
            if (id == null)
                return View("Index").ShowMessage(Status.Error, "Hata", "Hatalı istek! Lütfen yeniden deneyiniz!");



            if (!await _commentService.CommentExistsAsync(id.Value))
                return RedirectToAction("Index").ShowMessage(Status.Error, "Hata", "Onaylanmak istenen yorum bulunamadı!");

            string message = "";

            var commentInDb = await _commentService.FindByIdAsync(id.Value);

            if (commentInDb.IsActive)
            {
                commentInDb.IsActive = false;
                message = "Yorum pasiflendi";
            }


            else
            {
                commentInDb.IsActive = true;
                message = "Yorum onaylandı";
            }


            await _commentService.UpdateAsync(commentInDb);

            return RedirectToAction("Index").ShowMessage(Status.Ok, "Başarılı", message);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return Json(new JResult
                {
                    Status = Status.Error,
                    Message = "Silinmek istenen yorum bulunamadı!"
                });


            var commentInDb = await _commentService.FindByIdAsync(id.Value);

            if (commentInDb == null)
                return Json(new
                {
                    Status = Status.Error,
                    Message = "Silinmek istenen yorum bulunamadı!"
                });

            await _commentService.RemoveAsync(commentInDb);


            return Json(new JResult
            {
                Status = Status.Ok,
                Message = "Yorum başarıyla silindi."
            });

        }
    }
}
