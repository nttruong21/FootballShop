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
        private AccountDAO accountDao;

        // GET: Home
        public ActionResult Index()
        {
            if (ModelState.IsValid)
            {
                accountDao = new AccountDAO();
                var listAllAccounts = accountDao.getAllAccount();
                return View(listAllAccounts);
            }
            return View();
        }
    }
}