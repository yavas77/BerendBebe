using BerendBebe.Business.Abstract;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerendBebe.WebUI.Helpers.TagHelpers
{
    [HtmlTargetElement("mail-count")]
    public class NewMailCounterTagHelpers : TagHelper
    {
        private readonly IContactService _contactService;


        public NewMailCounterTagHelpers(IContactService contactService)
        {
            _contactService = contactService;
        }


        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "GetMailCount";
            output.TagMode = TagMode.StartTagAndEndTag;

            var activeMailInDb = await _contactService.GetIncomingByActiveAsync();

            string myHtml = string.Empty;

            if (activeMailInDb.Count > 0)
            {
                myHtml = $"<span class=\"badge bg-primary float-right\">";
                myHtml += $"{activeMailInDb.Count}";
                myHtml += "</span>";
            }
            else
                myHtml = null;

            output.PreContent.SetHtmlContent(myHtml);

        }
    }
}
