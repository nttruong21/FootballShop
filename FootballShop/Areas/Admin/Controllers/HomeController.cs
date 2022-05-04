using FootballShopModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FootballShop.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        // [GET] /Admin/Home/Index
        public ActionResult Index()
        {
            // KIỂM TRA SESION (ĐÃ ĐĂNG NHẬP HAY CHƯA)
            //if (Session[Constants.USER_SESSION] == null)
            //{
            //    return RedirectToAction("Index", "Login");
            //}
            return View();
        }

        // [GET] Admin/Home/Account
        public ActionResult Account()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session.Remove(Constants.EMAIL_SESSION);
            return RedirectToAction("Index", "Login");
        }
    }
}