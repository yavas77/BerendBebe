using AutoMapper;
using BerendBebe.Business.Abstract;
using BerendBebe.DTO.ProductDtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerendBebe.WebUI.ViewComponents
{
    public class ProductImagesViewComponent : ViewComponent
    {
        private readonly IProductImageService _productImageService;
        private readonly IMapper _mapper;

        public ProductImagesViewComponent(IProductImageService productImageService, IMapper mapper)
        {
            _productImageService = productImageService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            //var productimage = _productImageService.GetAllAsync();
            var productImageListDto = _mapper.Map<List<ProductImageDetailListDto>>(await _productImageService.GetAllByProductId(id));

            return View(productImageListDto);
        }
    }
}
