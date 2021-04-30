using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BerendBebe.Entities.Concrete.Order;

namespace BerendBebe.WebUI.Helpers.TagHelpers
{
    [HtmlTargetElement("OrderStatusWrite")]
    public class OrderStatusWriterTagHelper : TagHelper
    {
        public OrderStatus OrderStatus { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "OrderStatusWriter";
            output.TagMode = TagMode.StartTagAndEndTag;

            string myHtml = string.Empty;

            switch (OrderStatus)
            {
                case OrderStatus.Ready:
                    myHtml = "<span class=\"badge badge-pill badge-info\">Hazırlanıyor</span>";
                    break;
                case OrderStatus.InCargo:
                    myHtml = "<span class=\"badge badge-pill badge-info\">Kargoda</span>"; ;
                    break;
                case OrderStatus.Confirmed:
                    myHtml = "<span class=\"badge badge-pill badge-success\">Teslim Edildi</span>";
                    break;
                case OrderStatus.Cancelled:
                    myHtml = "<span class=\"badge badge-pill badge-danger\">İptal Edildi</span>";
                    break;
                case OrderStatus.Returned:
                    myHtml = "<span class=\"badge badge-pill badge-danger\">İade Edildi</span>";
                    break;
            }

            output.PreContent.SetHtmlContent(myHtml);
        }
    }
}
