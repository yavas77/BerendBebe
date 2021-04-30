using BerendBebe.Business.Abstract;
using BerendBebe.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerendBebe.WebUI.ViewComponents
{
    public class CartCountViewComponent : ViewComponent
    {
        private readonly ICartService _cartService;

        public CartCountViewComponent(ICartService cartService)
        {
            _cartService = cartService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string customer)
        {
            var cartsInDb = await _cartService.GetAllWithProductAsync(customer);

            var cartCountModel = new CartCountModel
            {
                Count = cartsInDb.Count()
            };

            return View(cartCountModel);
        }
    }
}
