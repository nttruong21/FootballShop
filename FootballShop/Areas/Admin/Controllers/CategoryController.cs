using FootballShopModel.DAO;
using FootballShopModel.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace FootballShop.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        private CategoryDAO categoryDao;

        // [GET] /Admin/Category
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

        // [GET] /Admin/Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // [GET] /Admin/Category/Detail/:id
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

        // [GET] /Admin/Category/Update/:id
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

        // [GET] /Admin/Category/Delete/:id
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

                if (categoryDao.RemoveCategory(cat))
                {
                    setAlert("Xóa danh mục sản phẩm thành công", "success");
                }
                else
                {
                    setAlert("Xóa danh mục sản phẩm thất bại", "danger");
                }
            }
            return RedirectToAction("Index");
        }

        //  [POST] /Admin/Category/Create
        [HttpPost]
        public ActionResult Create(Category cat, HttpPostedFileBase imageUpload)
        {
            if (ModelState.IsValid)
            {
                categoryDao = new CategoryDAO();

                // Kiểm tra name có rỗng?
                if (string.IsNullOrEmpty(cat.name))
                {
                    ModelState.AddModelError("", "Vui lòng nhập tên danh mục");
                    // setAlert("Vui lòng nhập tên danh mục", "danger");
                    return View("Create");
                }

                // Kiểm tra name đã tồn tại chưa?
                var catTmp = categoryDao.getCategoryByName(cat.name);
                if (catTmp != null)
                {
                    ModelState.AddModelError("", "Tên danh mục này đã tồn tại, vui lòng nhập tên khác");
                    return View("Create");
                }

                // Kiểm tra ảnh 
                if (imageUpload == null || imageUpload.ContentLength <= 0)
                {
                    ModelState.AddModelError("", "Vui lòng chọn ảnh cho danh mục");
                    return View("Create");
                }

                // Save file
                saveFile(categoryDao, cat, imageUpload);

                // Thực hiện thêm danh mục 
                cat.slug = GenerateSlug(convertToUnSign3(cat.name)) + "-" + (categoryDao.getMaxId() + 1);
                cat.uppercaseName = cat.name.ToUpper();

                categoryDao = new CategoryDAO();
                // Create product to database
                if (categoryDao.CreateCategory(cat))
                {
                    setAlert("Tạo danh mục sản phẩm mới thành công", "success");
                }
                else
                {
                    setAlert("Tạo danh mục sản phẩm mới thất bại", "danger");
                }
            }
            return RedirectToAction("Create");
        }

        //  [POST] /Admin/Category/Update/:id
        [HttpPost]
        public ActionResult Update(Category cat, HttpPostedFileBase imageUpload)
        {
            if (ModelState.IsValid)
            {
                categoryDao = new CategoryDAO();
                var catTmp = categoryDao.getCategoryById(cat.id);
                if (catTmp == null)
                {
                    ModelState.AddModelError("", "Không tìm thấy danh mục tương ứng");
                    return View("Update");
                }

                // Validate
                if (string.IsNullOrEmpty(cat.name))
                {
                    ModelState.AddModelError("", "Vui lòng nhập tên danh mục");
                    return View("Update", catTmp);
                }

                if (imageUpload != null && imageUpload.ContentLength > 0)
                {
                    // Save file
                    saveFile(categoryDao, catTmp, imageUpload);
                }

                catTmp.name = cat.name;
                catTmp.slug = GenerateSlug(convertToUnSign3(cat.name)) + "-" + cat.id;
                catTmp.uppercaseName = cat.name.ToUpper();

                // Update product to database
                if (categoryDao.UpdateCategory(catTmp))
                {
                    setAlert("Cập nhật danh mục sản phẩm thành công", "success");
                }
                else
                {
                    setAlert("Cập nhật danh mục sản phẩm thất bại", "danger");
                }
                return RedirectToAction("Update", catTmp);
            }
            return View("Update");
        }

        // Function Save File
        public void saveFile(CategoryDAO categoryDao, Category cat, HttpPostedFileBase imageUpload)
        {
            var listAllCategories = categoryDao.getAllCategories();
            int id = listAllCategories.Count > 0 ? listAllCategories.Last().id : 1;
            string imageName = "dm-" + id.ToString() + "." + Path.GetFileName(imageUpload.FileName).Split('.')[1];
            string path = Path.Combine(Server.MapPath("~/Assets/Admin/img/category"), imageName);
            imageUpload.SaveAs(path);
            cat.image = imageName;
        }
    }
}