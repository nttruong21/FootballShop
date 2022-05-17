using FootballShopModel;
using FootballShopModel.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FootballShop.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {             
            
            if (ModelState.IsValid )
            {
                ViewBag.allProducts = new ProductDAO().getAllProducts();
                ViewBag.discountProducts = new ProductDAO().getAllDiscountProducts();
                ViewBag.tenMostSoldProducts = new ProductDAO().getTenMostSoldProducts();
                ViewBag.allCategories = new CategoryDAO().getAllCategories();

            }
            return View();
        }

        // [GET] /Logout
        public ActionResult Logout()
        {
            Session.Remove(Constants.EMAIL_SESSION);
            Session.Remove(Constants.NAME_SESSION);
            Session.Remove(Constants.ID_SESSION);
            Session.Remove(Constants.ROLE_SESSION);
            Session.Remove("id_user");
            return RedirectToAction("Index", "Login");
        }

        [ChildActionOnly]
        public PartialViewResult Header()
        {
            if (ModelState.IsValid)
            {
                var allCategories = new CategoryDAO().getAllCategories();
                return PartialView(allCategories);
            }
            return PartialView();
        }
    }
}