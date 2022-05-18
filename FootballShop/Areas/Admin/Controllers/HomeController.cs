using FootballShopModel;
using FootballShopModel.DAO;
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
            BillDAO billDao = new BillDAO();
            ViewBag.revenue = billDao.getTotalRevenue();
            ViewBag.numberOfWaitingApproveBill = billDao.getNumberOfWaitingApproveBill();
            ViewBag.numberOfCanceledBill = billDao.getNumberOfCanceledBill();
            ViewBag.numberOfShippingBill = billDao.getNumberOfShippingBill();
            ViewBag.numberOfDeliveredBill = billDao.getNumberOfDeliveredBill();
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