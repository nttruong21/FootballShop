using FootballShopModel;
using FootballShopModel.DAO;
using FootballShopModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FootballShop.Controllers
{
    public class LoginController : Controller
    {
        // [GET] /Login
        public ActionResult Index()
        {
            return View();
        }

        // [POST] /Login
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
                if (result == true)
                {
                    var accTmp = accountDao.getAccountByEmail(account.email);

                    Session.Add(Constants.EMAIL_SESSION, accTmp.email);
                    Session.Add(Constants.NAME_SESSION, accTmp.name);
                    Session.Add(Constants.ID_SESSION, accTmp.id);
                    Session.Add(Constants.ROLE_SESSION, accTmp.role);
                    Session["id_user"] = accTmp.id.ToString();

                    if(accTmp.role == 0)
                    {
                        return RedirectToAction("Index", "Home", new { area = "Admin" });
                    } else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Email hoặc mật khẩu không hợp lệ");
                }
            }
            return View();
        }

    }
}