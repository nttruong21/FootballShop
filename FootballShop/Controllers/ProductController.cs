using FootballShopModel.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FootballShop.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index(int id)
        {
            return View();
        }

        public ActionResult ProductsByCategory(string slug)
        {
            if (ModelState.IsValid)
            {
                ViewBag.productsByCategory = new ProductDAO().getAllProductsByCategorySlug(slug);
                ViewBag.category = new CategoryDAO().getCategoryBySlug(slug);
                return View();
            }
            return View();
        }

        public ActionResult ProductDetail(string slug)
        {
            if (ModelState.IsValid)
            {
                var product = new ProductDAO().getProductBySlug(slug);
                var categoryId = (int)product.categoryId;
                ViewBag.product = product;
                ViewBag.productsByCategory = new ProductDAO().getAllProductsByCategoryId(categoryId);
                return View();
            }
            return View();
        }
    }
}