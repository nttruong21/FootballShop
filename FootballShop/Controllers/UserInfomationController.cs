using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FootballShopModel.Models;
using FootballShopModel.DAO;
using FootballShopModel;

namespace FootballShop.Controllers
{
    public class UserInfomationController : Controller
    {
        // GET: UserInfomation

        public ActionResult Index()
        {
            return View();
        }

        // GET: UserInfomation/GetInfomation?id=4
        [HttpGet]
        public JsonResult GetInfomation(int id)
        {
            var accountDao = new AccountDAO();
            var user = accountDao.getAccountById(id);

            if (user == null)
            {

            }
            return Json(new
            {
                status = "succes",
                user,
            }, JsonRequestBehavior.AllowGet);
        }

        // PUT: UserInfomation/Update
        [HttpPut]
        public JsonResult Update(int id, string name, string phone, string address)
        {
            var accountDao = new AccountDAO();
            var user = accountDao.getAccountById(id);
            user.name = name;
            user.phone = phone;
            user.address = address;
            if (accountDao.checkPhoneNumberExists(id, phone))
            {
                return Json(new
                {
                    status = "error",
                    message = "Thất bại",
                    reason = "Số điện thoại đã tồn tại!"
                }, JsonRequestBehavior.AllowGet);
            }
            accountDao.UpdateAccount(user);
            return Json(new
            {
                status = "success",
                message = "Thành công",
                reason = "Cập nhập thông tin cá nhân thành công",
                user,
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPut]
        public JsonResult ChangePassword(int id, string oldpass, string newpass)
        {
            var accountDao = new AccountDAO();
            var user = accountDao.getAccountById(id);
            if (!accountDao.checkPassword(id, oldpass))
            {
                return Json(new
                {
                    status = "error",
                    message = "Thất bại",
                    reason = "Mật khẩu cũ không hợp lệ",
                    oldpass

                }, JsonRequestBehavior.AllowGet);
            }
            var hashpass = Common.MD5Hash(newpass);
            user.password = hashpass;
            accountDao.UpdateAccount(user);

            return Json(new
            {
                status = "success",
                message = "Thành công",
                reason = "Thay đổi mật khẩu thành công",
                newpass,
                hashpass,


            }, JsonRequestBehavior.AllowGet);
        }


    }
}