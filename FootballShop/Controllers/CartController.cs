using FootballShopModel.DAO;
using FootballShopModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FootballShop.Controllers
{
    public class CartController : Controller
    {
        private CartDAO cartDao = new CartDAO();
        private FootballShopEntities db = new FootballShopEntities();

        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]

        public ActionResult AddtoCart(CartDetail cart)
        {

            return cartDao.CreateProduct(cart)
                ? Json(new
                {
                    status = "success",

                }, JsonRequestBehavior.AllowGet)
                : Json(new
                {
                    status = "fail",
                }, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult GetAllCart(int id_user)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var data = from CartDetails in db.CartDetails
                       join Products in db.Products on CartDetails.productId equals Products.id
                       join Account in db.Accounts on CartDetails.accountId equals Account.id
                       where CartDetails.accountId == id_user
                       select new
                       {
                           CartDetails.id,
                           accountId= CartDetails.accountId,
                            productId= CartDetails.productId,
                            quantity= CartDetails.quantity,
                            price= CartDetails.price,
                           Products
                       };
            

            return Json(new
            {
                status = "success",
                data ,
            }, JsonRequestBehavior.AllowGet);


        }
    }
}