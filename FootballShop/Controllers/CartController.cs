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
        CartDAO cartDao = new CartDAO();
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
     
        public ActionResult AddtoCart(CartDetail cart)
        {
            
            if(cartDao.CreateProduct(cart))
            {
                return Json(new
                {
                    status = "success",
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    status = "fail",
                }, JsonRequestBehavior.AllowGet);
            }  
            
        }

        [HttpGet]
        public ActionResult GetAllCart()
        {
            List<CartDetail> data =  cartDao.getAll();
           
            return Json(new
            {
                status = "success",
                data
            }, JsonRequestBehavior.AllowGet);
            

        }
    }
}