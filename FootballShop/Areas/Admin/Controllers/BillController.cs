using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FootballShop.Areas.Admin.Controllers
{
    public class BillController : BaseController
    {
        // GET: Admin/Bill
        public ActionResult Index()
        {
            return View();
        }
    }
}