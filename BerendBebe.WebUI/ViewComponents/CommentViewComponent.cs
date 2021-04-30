using AutoMapper;
using BerendBebe.Business.Abstract;
using BerendBebe.DTO.CommentDtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerendBebe.WebUI.ViewComponents
{
    public class CommentViewComponent : ViewComponent
    {
        private readonly ICommentService _commentService;

        public CommentViewComponent(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var commentListInDb = await _commentService.GetAllConfirmedByProducdIdAsync(id);

            var model = new CommentListDto
            {
                Comments = commentListInDb,
                CommentCount = commentListInDb.Count
            };
            return View(model);
        }
    }
}
