using FootballShopModel.DAO;
using FootballShopModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FootballShop.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        private CategoryDAO categoryDao;
        // GET: /Admin/Category
        public ActionResult Index()
        {
            if (ModelState.IsValid)
            {
                categoryDao = new CategoryDAO();
                var listAllCategories = categoryDao.getAllCategories();
                return View(listAllCategories);
            }
            return View();
        }

        // GET /Admin/Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // GET /Admin/Category/Detail/:id
        public ActionResult Detail(int id)
        {
            if(ModelState.IsValid)
            {
                categoryDao = new CategoryDAO();
                var cat = categoryDao.getCategoryById(id);
                if (cat == null)
                {
                    setAlert("Không tìm thấy danh mục sản phẩm tương ứng", "danger");
                    return RedirectToAction("Index");
                }
                return View(cat);
            }
            return View();
        }

        // GET /Admin/Category/Update/:id
        public ActionResult Update(int id)
        {
            if (ModelState.IsValid)
            {
                categoryDao = new CategoryDAO();
                var cat = categoryDao.getCategoryById(id);
                if (cat == null)
                {
                    setAlert("Không tìm thấy danh mục sản phẩm tương ứng", "danger");
                    return RedirectToAction("Index");
                }
                return View(cat);
            }
            return View();
        }

        // GET /Admin/Category/Delete/:id
        public ActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                categoryDao = new CategoryDAO();
                var cat = categoryDao.getCategoryById(id);
                if (cat == null)
                {
                    setAlert("Không tìm thấy danh mục sản phẩm tương ứng", "danger");
                    return RedirectToAction("Index");
                }
                categoryDao.DeleteCategory(cat);
            }
            return RedirectToAction("Index");
        }

        //  POST /Admin/Category/Create
        [HttpPost]
        public ActionResult Create(Category cat)
        {
            if (ModelState.IsValid)
            {
                // Name
                if (string.IsNullOrEmpty(cat.name))
                {
                    setAlert("Vui lòng nhập tên danh mục", "danger");
                    return RedirectToAction("Create");
                }
                categoryDao = new CategoryDAO();
                categoryDao.CreateCategory(cat);
            }
            setAlert("Tạo danh mục mới thành công", "success");
            return RedirectToAction("Create");
        }

        //  POST /Admin/Category/Update/:id
        [HttpPost]
        public ActionResult Update(Category cat)
        {
            if (ModelState.IsValid)
            {
                categoryDao = new CategoryDAO();
                // Name
                if (string.IsNullOrEmpty(cat.name))
                {
                    setAlert("Vui lòng nhập tên danh mục", "danger");
                    return RedirectToAction("Update", cat);
                }

                var catTmp = categoryDao.getCategoryById(cat.id);
                if(catTmp == null)
                {
                    setAlert("Không tìm thấy danh mục tương ứng", "danger");
                    return RedirectToAction("Update", cat);
                }

                catTmp.name = cat.name;
                categoryDao.UpdateCategory(catTmp);
                setAlert("Cập nhật danh mục mới thành công", "success");
                return RedirectToAction("Update", catTmp);
            }
            return View("Update");
        }
    }
}