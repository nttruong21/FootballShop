using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FootballShopModel.DAO;
using FootballShopModel;
namespace FootballShop.Areas.Admin.Controllers
{
    public class ProfileController : BaseController
    {
        // GET: Profile

        public ActionResult Index()
        {
            return View();
        }

        // GET: Profile/GetInfomation?id=4
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

        // PUT: Profile/Update
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
                    status = "danger",
                    message = "Số điện thoại đã tồn tại!",
                    icon = "<i class='fa-solid fa-xmark text-white mr-3'></i>"
                }, JsonRequestBehavior.AllowGet);
            }
            accountDao.UpdateAccount(user);

            return Json(new
            {
                status = "success",
                message = "Cập nhập thông tin cá nhân thành công",
                icon = "<i class='fa-solid fa-circle-check text-white mr-3'></i>",
                user,
            }, JsonRequestBehavior.AllowGet);
        }



    }
}