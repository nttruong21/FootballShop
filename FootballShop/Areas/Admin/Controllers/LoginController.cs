using FootballShopModel;
using FootballShopModel.DAO;
using FootballShopModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FootballShop.Areas.Admin.Controllers
{
    public class LoginController : BaseController
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Account account)
        {
            if(ModelState.IsValid)
            {
                var accountDao = new AccountDAO();
                var result = accountDao.login(account.email, Common.MD5Hash(account.password));
                if(result == true)
                {
                    //ModelState.AddModelError("", "Đăng nhập thành công");
                    Session.Add(Constants.EMAIL_SESSION, account.email);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // setAlert("Tài khoản hoặc mật khẩu không hợp lệ", "danger");
                    ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không hợp lệ");
                }
            }
            return View();
        }
    }
}