using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerendBebe.MvcHelpers.Extensions
{
    public static class ActiveClassExtension
    {
        public static string ActiveClass(this IHtmlHelper htmlHelper, string controller = null, string action = null, string cssClass = null)
        {
            if (string.IsNullOrEmpty(cssClass))
                cssClass = "active";

            string currentAction = (string)htmlHelper.ViewContext.RouteData.Values["action"];

            string currentController = (string)htmlHelper.ViewContext.RouteData.Values["controller"];

            if (string.IsNullOrEmpty(controller))
                controller = currentController;

            if (string.IsNullOrEmpty(action))
                action = currentAction;

            return controller.ToLower().Contains(currentController.ToLower()) && action.ToLower().Contains(currentAction.ToLower()) ? cssClass : string.Empty;
        }

        public static string StatusBadge(this IHtmlHelper htmlHelper, bool status)
        {
            string result = string.Empty;
            if (status)
                result = "<span class=\"badge badge-success badge\">Aktif</span>";
            else
                result = "<span class=\"badge badge-danger badge\">Pasif</span>";

            return result;
        }
    }
}
