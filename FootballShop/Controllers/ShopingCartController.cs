using FootballShopModel.DAO;
using FootballShopModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FootballShop.Controllers
{
    public class ShopingCartController : Controller
    {
        private CartDAO cartDao = new CartDAO();
        // GET: ShopingCart
        
        public ActionResult Index()
        {
            
            return View();
        }


      
        [HttpGet]
        public JsonResult UpdateQuantityUp(int productId)
        {
            cartDao.UpdateQuantityCart(productId);
            CartDetail data = cartDao.getbyProductId(productId);
            return Json(new
            {
                status = "success",
                data,
            }, JsonRequestBehavior.AllowGet);
        }


        public JsonResult UpdateQuantityDown(int productId)
        {
            cartDao.UpdateQuantityCartDown(productId);
            CartDetail data = cartDao.getbyProductId(productId);
            return Json(new
            {
                status = "success",
                data,
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteShopingCart(int idCart)
        {
            cartDao.removeItem(idCart);

            return Json(new
            {
                status = "success",
            }, JsonRequestBehavior.AllowGet);

        }
    }
}