using FootballShopModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FootballShop.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        // [GET] /Admin/Home/Index
        public ActionResult Index()
        {
            // KIỂM TRA SESION (ĐÃ ĐĂNG NHẬP HAY CHƯA)
            //if (Session[Constants.ID_SESSION] == null)
            //{
            //    return Redirect("/Login");
            //}
            //if ((int)Session[Constants.ROLE_SESSION] != 0)
            //{
            //    return Redirect("/Error/NotPermission");
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