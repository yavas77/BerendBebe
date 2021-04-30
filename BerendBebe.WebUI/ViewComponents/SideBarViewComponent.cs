using AutoMapper;
using BerendBebe.Business.Abstract;
using BerendBebe.DTO.CategoryDtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BerendBebe.WebUI.ViewComponents
{
    public class SideBarViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public SideBarViewComponent(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categoryListDto = _mapper.Map<List<CategoryListDto>>(await _categoryService.GetAllByActiveAsync());
            return View(categoryListDto);
        }
    }
}
