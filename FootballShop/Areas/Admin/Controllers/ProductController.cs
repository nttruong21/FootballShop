using FootballShopModel.DAO;
using FootballShopModel.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FootballShop.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        private ProductDAO productDao;
        // GET: /Admin/Product
        public ActionResult Index()
        {
            if(ModelState.IsValid)
            {
                productDao = new ProductDAO();
                var listAllProducts = productDao.getAllProducts();
                return View(listAllProducts);
            }
            return View();
        }

        // GET: /Admin/Product/Create
        public ActionResult Create()
        {
            CategoryDAO categoryDao = new CategoryDAO();
            var listAllCategories = categoryDao.getAllCategories();
            ViewBag.MenuCategories = new SelectList(listAllCategories, "id", "name", null);
            return View();
        }

        // GET: /Admin/Product/Update
        public ActionResult Update(int id)
        {
            if(id == null)
            {
                setAlert("Không nhận được id sản phẩm", "danger");
                return RedirectToAction("Index");
            }

            productDao = new ProductDAO();
            var product = productDao.getProductById(id);
            if(product == null)
            {
                setAlert("Không tìm thấy sản phẩm tương ứng", "danger");
                return RedirectToAction("Index");
            }

            CategoryDAO categoryDao = new CategoryDAO();
            var listAllCategories = categoryDao.getAllCategories();
            ViewBag.MenuCategories = new SelectList(listAllCategories, "id", "name", null);
            return View(product);
        }

        // GET: /Admin/Product/Detail
        public ActionResult Detail(int id)
        {
            if (id == null)
            {
                setAlert("Không nhận được id sản phẩm", "danger");
                return RedirectToAction("Index");
            }

            productDao = new ProductDAO();
            var product = productDao.getProductById(id);
            if (product == null)
            {
                setAlert("Không tìm thấy sản phẩm tương ứng", "danger");
                return RedirectToAction("Index");
            }

            CategoryDAO categoryDao = new CategoryDAO();
            var listAllCategories = categoryDao.getAllCategories();
            ViewBag.MenuCategories = new SelectList(listAllCategories, "id", "name", null);
            return View(product);
        }

        // POST: /Admin/Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product obj, HttpPostedFileBase imageUpload)
        {
            if(ModelState.IsValid)
            {
                // Validate
                string errorMessage = getErrorMessage(obj);

                if(imageUpload == null || imageUpload.ContentLength <= 0)
                {
                    errorMessage += "Vui lòng chọn ảnh cho sản phẩm.";
                }

                if (errorMessage != "")
                {
                    ModelState.AddModelError("", errorMessage.Split('.')[0]);
                    CategoryDAO categoryDao = new CategoryDAO();
                    var listAllCategories = categoryDao.getAllCategories();
                    ViewBag.MenuCategories = new SelectList(listAllCategories, "id", "name", null);
                    return View("Create");
                }

                productDao = new ProductDAO();

                // Save file
                //var listAllProduct = productDao.getAllProducts();
                //int id = listAllProduct.Count > 0 ? listAllProduct.Last().id : 1;
                //string imageName = id.ToString() + "." + Path.GetFileName(imageUpload.FileName).Split('.')[1];
                //string path = Path.Combine(Server.MapPath("~/Upload"), imageName);
                //imageUpload.SaveAs(path);
                saveFile(productDao, obj, imageUpload);

                // Create product to database
                obj.rate = 0;
                productDao.CreateProduct(obj);
                setAlert("Tạo mới sản phẩm thành công", "success");
            }
            return RedirectToAction("Create");
        }

        // POST: /Admin/Product/Update/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(Product obj, HttpPostedFileBase imageUpload)
        {
            if (ModelState.IsValid)
            {
                // Validate
                string errorMessage = getErrorMessage(obj);
                if (errorMessage != "")
                {
                    ModelState.AddModelError("", errorMessage.Split('.')[0]);
                    CategoryDAO categoryDao = new CategoryDAO();
                    var listAllCategories = categoryDao.getAllCategories();
                    ViewBag.MenuCategories = new SelectList(listAllCategories, "id", "name", null);
                    return View("Create");
                }

                productDao = new ProductDAO();
                var product = productDao.getProductById(obj.id);
                if(product != null)
                {
                    if (imageUpload != null && imageUpload.ContentLength > 0)
                    {
                        // Save file
                        //var listAllProduct = productDao.getAllProducts();
                        //int id = listAllProduct.Count > 0 ? listAllProduct.Last().id : 1;
                        //string imageName = id.ToString() + "." + Path.GetFileName(imageUpload.FileName).Split('.')[1];
                        //string path = Path.Combine(Server.MapPath("~/Upload"), imageName);
                        //imageUpload.SaveAs(path);
                        //product.image = imageName;
                        saveFile(productDao, product, imageUpload);
                    }
                    product.name = obj.name;
                    product.categoryId = obj.categoryId;
                    product.brand = obj.brand;
                    product.price = obj.price;
                    product.discount = obj.discount;
                    product.video = obj.video;
                    product.size = obj.size;
                    product.quantity = obj.quantity;
                    product.infor = obj.infor;

                    // Update product to database
                    if (productDao.UpdateProduct(product))
                    {
                        setAlert("Cập nhật sản phẩm thành công", "success");
                    }
                    else
                    {
                        setAlert("Có lỗi xảy ra khi cập nhật thông tin sản phẩm", "danger");
                    }
                }
            }
            return RedirectToAction("Update");
        }

        // POST: /Admin/Product/Delete/:id
        [HttpPost]
        public ActionResult Delete(int id)
        {
            if(ModelState.IsValid)
            {
                productDao = new ProductDAO();
                var product = productDao.getProductById(id);
                if (product == null)
                {
                    setAlert("Không tìm thấy sản phẩm", "danger");
                    return RedirectToAction("Index");
                }
                if(!productDao.DeleteProduct(product))
                {
                    setAlert("Có lỗi khi xóa sản phẩm", "danger");
                    return RedirectToAction("Index");
                }
                setAlert("Xóa sản phẩm thành công", "success");
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        // Function Save File
        public void saveFile(ProductDAO productDao, Product obj, HttpPostedFileBase imageUpload)
        {
            var listAllProduct = productDao.getAllProducts();
            int id = listAllProduct.Count > 0 ? listAllProduct.Last().id : 1;
            string imageName = id.ToString() + "." + Path.GetFileName(imageUpload.FileName).Split('.')[1];
            string path = Path.Combine(Server.MapPath("~/Upload"), imageName);
            imageUpload.SaveAs(path);
            obj.image = imageName;
        }

        // Function Validate
        public string getErrorMessage(Product obj)
        {
            string errorMessage = "";
            if (string.IsNullOrEmpty(obj.name))
            {
                errorMessage += "Vui lòng nhập tên sản phẩm.";
            }

            if (obj.categoryId == null || obj.categoryId < 1)
            {
                errorMessage += "Vui lòng chọn danh mục sản phẩm.";
            }

            if (string.IsNullOrEmpty(obj.brand))
            {
                errorMessage += "Vui lòng nhập thương hiệu sản phẩm.";
            }

            if (obj.price == null || obj.price <= 0)
            {
                errorMessage += "Vui lòng nhập đúng giá sản phẩm.";
            }

            if (obj.discount < 0)
            {
                errorMessage += "Khuyến mãi không được nhỏ hơn 0.";
            }

            if (obj.discount == null)
            {
                obj.discount = 0;
            }

            if (string.IsNullOrEmpty(obj.size))
            {
                errorMessage += "Vui lòng nhập kích thuớc của sản phẩm.";
            }

            if (obj.quantity == null || obj.quantity <= 0)
            {
                errorMessage += "Vui lòng nhập đúng số lượng sản phẩm.";
            }
            return errorMessage;
        }

    }
}