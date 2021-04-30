using BerendBebe.Business.Abstract;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerendBebe.WebUI.Helpers.TagHelpers
{
    [HtmlTargetElement("cart-count")]
    public class CartCounterTagHelper : TagHelper
    {
        private readonly ICartService _cartService;

        public CartCounterTagHelper(ICartService cartService)
        {
            _cartService = cartService;
        }

        public string Customer { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "GetCartCount";
            output.TagMode = TagMode.StartTagAndEndTag;

            var cartInDb = await _cartService.GetAllWithProductAsync(Customer);
            
            string myHtml = $"{cartInDb.Count}";

            output.PreContent.SetHtmlContent(myHtml);

        }
    }
}
