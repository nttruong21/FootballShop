using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FootballShop.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            return View();
        }

        // [GET] /Error/NotPermission
        public ActionResult NotPermission()
        {
            return View();
        }

        // [GET] /Error/404
        public ActionResult NotFound()
        {
            return View();
        }
    }
}