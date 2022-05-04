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
        // [GET] Admin/Login
        public ActionResult Index()
        {
            return View();
        }

        // [POST] /Admin/Login
        [HttpPost]
        public ActionResult Index(Account account)
        {
            if (account.email == null || account.email == "")
            {
                ModelState.AddModelError("", "Vui lòng nhập email");
                return View();
            }
            if (account.password == null || account.password == "")
            {
                ModelState.AddModelError("", "Vui lòng nhập mật khẩu");
                return View();
            }
            if (ModelState.IsValid)
            {
                var accountDao = new AccountDAO();
                var result = accountDao.login(account.email, account.password);
                if(result == true)
                {
                    var accTmp = accountDao.getAccountByEmail(account.email);

                    Session.Add(Constants.EMAIL_SESSION, accTmp.email);
                    Session.Add(Constants.NAME_SESSION, accTmp.name);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // setAlert("Tài khoản hoặc mật khẩu không hợp lệ", "danger");
                    ModelState.AddModelError("", "Email hoặc mật khẩu không hợp lệ");
                }
            }
            return View();
        }
    }
}