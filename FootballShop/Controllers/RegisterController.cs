using FootballShopModel.DAO;
using FootballShopModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FootballShop.Controllers
{
    public class RegisterController : Controller
    {
        private AccountDAO accountDao;

        // [GET] /Register
        public ActionResult Index()
        {
            return View();
        }

        // [POST] /Register
        [HttpPost]
        public ActionResult Index(Account account)
        {
            if (account.name == null || account.name.Trim() == "")
            {
                ModelState.AddModelError("", "Bạn chưa nhập họ tên");
                return View();
            }
            if (account.email == null || account.email.Trim() == "")
            {
                ModelState.AddModelError("", "Bạn chưa nhập email");
                return View();
            }
            if (account.phone == null || account.phone.Trim() == "")
            {
                ModelState.AddModelError("", "Bạn chưa nhập số điện thoại");
                return View();
            }
            if (account.address == null || account.address.Trim() == "")
            {
                ModelState.AddModelError("", "Bạn chưa nhập địa chỉ");
                return View();
            }
            if (account.password == null || account.password.Trim() == "")
            {
                ModelState.AddModelError("", "Bạn chưa nhập mật khẩu");
                return View();
            }
            if(account.password.Trim().Length < 6)
            {
                ModelState.AddModelError("", "Mật khẩu phải chứa tối thiểu 6 ký tự");
                return View();
            }
            if (account.confirmPassword == null || account.confirmPassword == "")
            {
                ModelState.AddModelError("", "Bạn chưa nhập xác nhận mật khẩu");
                return View();
            }
            if (account.password != account.confirmPassword)
            {
                ModelState.AddModelError("", "Nhập lại mật khẩu không đúng");
                return View();
            }
            if (ModelState.IsValid)
            {
                var accountDao = new AccountDAO();

                if (accountDao.getAccountById(account.id) != null)
                {
                    ModelState.AddModelError("", "Mã tài khoản này đã tồn tại");
                    return View();
                }
                if (accountDao.getAccountByEmail(account.email.Trim()) != null)
                {
                    ModelState.AddModelError("", "Email này đã tồn tại");
                    return View();
                }
                if (accountDao.getAccountByPhone(account.phone.Trim()) != null)
                {
                    ModelState.AddModelError("", "Số điện thoại này đã tồn tại");
                    return View();
                }
                account.name = account.name.Trim();
                account.email = account.email.Trim();
                account.phone = account.phone.Trim();
                account.address = account.address.Trim();
                account.password = account.password.Trim();
                account.confirmPassword = "";
                account.role = 1;
                if (accountDao.CreateAccount(account))
                {
                    TempData["AlertMessage"] = "Đăng ký tài khoản thành công";
                    TempData["AlertType"] = "success";
                    TempData["AlertIcon"] = new HtmlString("<i class='fa-solid fa-circle-check text-white mr-3'></i>");
                    return RedirectToAction("Index", "Login");
                    // Thành công 
                } else
                {
                    ModelState.AddModelError("", "Đăng ký tài khoản thất bại");
                    return View();
                }
            }
            return View();
        }
    }
}