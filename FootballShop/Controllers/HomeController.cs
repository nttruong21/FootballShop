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
            //else
            //{
               // return Redirect("Auth/login");
            //}
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