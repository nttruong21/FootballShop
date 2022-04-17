using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FootballShop.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        // GET: Admin/Base
        //protected override void OnActionExecuted(ActionExecutedContext filterContext)
        //{
        //    if (Session[Constants.USER_SESSION] == null)
        //    {
        //        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Login", action = "Index", area = "Admin" }));
        //    }
        //    base.OnActionExecuted(filterContext);
        //}

        protected void setAlert(string message, string type)
        {
            TempData["AlertMessage"] = message;
            TempData["AlertType"] = type;
            switch (type)
            {
                case "success":
                    TempData["AlertIcon"] = new HtmlString("<i class='fa-solid fa-check text-white mr-2'></i>");
                    break;
                case "danger":
                    TempData["AlertIcon"] = new HtmlString("<i class='fa-solid fa-xmark text-white mr-2'></i>");
                    break;
                case "warning":
                    TempData["AlertIcon"] = new HtmlString("<i class='fa-solid fa-brake-warning'></i>");
                    break;
            }
}
    }
}