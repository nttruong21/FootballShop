using FootballShopModel.DAO;
using FootballShopModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FootballShop.Controllers
{
    public class BillDetailController : Controller
    {
        private BillDetailDAO billDetailDao = new BillDetailDAO();
        private ProductDAO productDAO = new ProductDAO();
        private FootballEntities db = new FootballEntities();
        // GET: BillDetail
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult getAllByIdBill(int idBill)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var data = from bill in db.BillDetails
                       join product in db.Products on bill.productId equals product.id
                       where bill.billId == idBill
                       select new
                       {
                           bill.id,
                           productId = bill.productId,
                           quantity = bill.quantity,
                           price = bill.price,
                           product
                       };
            return Json(new
            {
                status = "success",
                data
            }, JsonRequestBehavior.AllowGet);
        }
    }
}