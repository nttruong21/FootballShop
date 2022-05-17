using FootballShopModel.DAO;
using FootballShopModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FootballShop.Controllers
{
    public class BillController : Controller
    {
        private BillDAO billDAO = new BillDAO();
        private CartDAO cartDao = new CartDAO();
        private FootballShopEntities db = new FootballShopEntities();
        // GET: Bill
        [Route("/bill")]
        public ActionResult Index()
        {
            return View();
        }

        
        [HttpPost]
        public JsonResult CreateBill(Bill bill)
        {
            var b = billDAO.CreateBill(bill);
            var data = from CartDetails in db.CartDetails
                       join Products in db.Products on CartDetails.productId equals Products.id
                       join Account in db.Accounts on CartDetails.accountId equals Account.id
                       where CartDetails.accountId == bill.accountId
                       select new
                       {
                           id_cart = CartDetails.id,
                           accountId = CartDetails.accountId,
                           productId = CartDetails.productId,
                           quantity = CartDetails.quantity,
                           price = CartDetails.price,
                           Products
                       };

            foreach(var t in data)
            {
                BillDetail bil = new BillDetail();
                bil.billId = bill.id;
                bil.productId = t.productId;
                bil.quantity = t.quantity;
                bil.price = t.price;
                db.BillDetails.Add(bil);
                cartDao.removeItem(t.id_cart);
            }
            db.SaveChangesAsync();
            return Json(new
            {
                status = "success",
                b
            }, JsonRequestBehavior.AllowGet);
                 
        }


        public JsonResult GetAllBillbyUser(int idUser)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Bill> bill = db.Bills.Where(b => b.accountId == idUser).ToList();

            return Json(new
            {
                status = "success",
                bill
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateStatusById(int id)
        {
            billDAO.updateStatus(id);
            return Json(new
            {
                status = "success",

            }, JsonRequestBehavior.AllowGet);
        }
    }
}