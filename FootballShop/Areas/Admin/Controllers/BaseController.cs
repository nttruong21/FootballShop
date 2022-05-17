using FootballShopModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FootballShop.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        // [GET] Admin/Base
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (Session[Constants.ID_SESSION] == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Login", action = "Index", area = "" }));
            } else if ((int)Session[Constants.ROLE_SESSION] != 0)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Error", action = "NotPermission", area = "" }));
            }
            base.OnActionExecuted(filterContext);
        }

        protected void setAlert(string message, string type)
        {
            TempData["AlertMessage"] = message;
            TempData["AlertType"] = type;
            switch (type)
            {
                case "success":
                    TempData["AlertIcon"] = new HtmlString("<i class='fa-solid fa-circle-check text-white mr-3'></i>");
                    break;
                case "danger":
                    TempData["AlertIcon"] = new HtmlString("<i class='fa-solid fa-xmark text-white mr-3'></i>");
                    break;
                case "warning":
                    TempData["AlertIcon"] = new HtmlString("<i class='fa-solid fa-brake-warning'></i>");
                    break;
            }
        }

        public static string GenerateSlug(string phrase)
        {
            string str = RemoveAccent(phrase).ToLower();
            // invalid chars           
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            // convert multiple spaces into one space   
            str = Regex.Replace(str, @"\s+", " ").Trim();
            // cut and trim 
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
            str = Regex.Replace(str, @"\s", "-"); // hyphens   
            return str;
        }

        public static string convertToUnSign3(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }

        public static string RemoveAccent(string txt)
        {
            byte[] bytes = System.Text.Encoding.GetEncoding("UTF-8").GetBytes(txt);
            return System.Text.Encoding.ASCII.GetString(bytes);
        }
    }
}