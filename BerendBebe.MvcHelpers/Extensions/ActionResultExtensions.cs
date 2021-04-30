using BerendBebe.MvcHelpers.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerendBebe.MvcHelpers.Extensions
{
    public static class ActionResultExtensions
    {
        private static IHttpContextAccessor _httpContextAccessor;
        private static ITempDataDictionaryFactory _tempDataDictionaryFactory;

        public static void Configure(IHttpContextAccessor httpContextAccessor, ITempDataDictionaryFactory tempDataDictionaryFactory)
        {
            _httpContextAccessor = httpContextAccessor;
            _tempDataDictionaryFactory = tempDataDictionaryFactory;
        }
        public static IActionResult ShowMessage(this IActionResult result, Status status, string title, string message)
        {
            var htpContext = _httpContextAccessor.HttpContext;
            var tempData = _tempDataDictionaryFactory.GetTempData(htpContext);

            var messageObj = new JResult
            {
                Message = message,
                Status = status,
                Title = title
            };

            tempData["Message"] = JsonConvert.SerializeObject(messageObj);

            return result;
        }
    }
}
