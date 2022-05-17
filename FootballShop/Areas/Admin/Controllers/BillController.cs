using FootballShopModel.DAO;
using FootballShopModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FootballShop.Areas.Admin.Controllers
{
    public class BillController : BaseController
    {
        private BillDAO billDao;
        private BillDetailDAO billDetailDao;

        // GET: Admin/Bill
        public ActionResult Index()
        {
            billDao = new BillDAO();
            var bills = billDao.getAllBills();
            return View(bills);
        }

        // [GET] /Admin/Bill/WaitingApproveBill
        public ActionResult WaitingApproveBills()
        {
            billDao = new BillDAO();
            var bills = billDao.getWaitingApproveBills();
            return View(bills);
        }

        // [GET] /Admin/Bill/CanceledBills
        public ActionResult CanceledBills()
        {
            billDao = new BillDAO();
            var bills = billDao.getCanceledBills();
            return View(bills);
        }

        // [GET] /Admin/Bill/ShippingBills
        public ActionResult ShippingBills()
        {
            billDao = new BillDAO();
            var bills = billDao.getShippingBills();
            return View(bills);
        }

        // [GET] /Admin/Bill/DeliveredBills
        public ActionResult DeliveredBills()
        {
            billDao = new BillDAO();
            var bills = billDao.getDeliveredBills();
            return View(bills);
        }

        // [GET] /Admin/Bill/BillDetail
        public ActionResult BillDetail(int billId)
        {
            billDao = new BillDAO();
            billDetailDao = new BillDetailDAO();
            var bill = billDao.getBillById(billId);
            var billDetails = billDetailDao.getAllByBillId(billId);
            ViewBag.bill = bill;
            ViewBag.billDetails = billDetails;
            return View();
        }


        // [GET]
        public JsonResult GetAllBill()
        {
            billDao = new BillDAO();
            var bills = billDao.getAllBills();

            return Json(new
            {
                data = bills
            }, JsonRequestBehavior.AllowGet);
        }

        // [POST] /Admin/Bill/UpdateBillStatus
        [HttpPost]
        public JsonResult UpdateBillStatus(Bill billObject)
        {
            billDao = new BillDAO();
            Bill bill = billDao.getBillById(billObject.id);
            if(bill != null)
            {
                if (billDao.UpdateBillStatus(bill, (int)billObject.status))
                {
                    return Json(new
                    {
                        code = 1
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new
            {
                code = 0
            }, JsonRequestBehavior.AllowGet);
        }
    }
}