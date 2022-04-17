using FootballShopModel.Models;
using FootballShopModel.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FootballShop.Areas.Admin.Controllers
{
    public class AccountController : BaseController
    {
        private AccountDAO accountDao;

        // GET: /Admin/Account
        [HttpGet]
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

        // GET: /Admin/Account/Create
        [HttpGet]
        public ActionResult Create()
        {
            if (ModelState.IsValid)
            {
                List<SelectListItem> items = new List<SelectListItem>();
                items.Add(new SelectListItem { Text = "Quản trị", Value = "0", Selected = true });
                items.Add(new SelectListItem { Text = "Người dùng", Value = "1" });

                ViewBag.Roles = items;
            }
            return View();
        }

        // GET
        [HttpGet]
        public ActionResult Update(int id)
        {
            if (ModelState.IsValid)
            {
                accountDao = new AccountDAO();
                var account = accountDao.getAccountById(id);

                if (account == null)
                {
                    setAlert("Không tìm thấy tài khoản tương ứng", "danger");
                    return RedirectToAction("Index", "Account");
                }

                List<SelectListItem> items = new List<SelectListItem>();
                items.Add(new SelectListItem { Text = "Quản trị", Value = "0" });
                items.Add(new SelectListItem { Text = "Người dùng", Value = "1" });

                ViewBag.Roles = items;
                return View(account);
            }
            return View();
        }

        // GET
        [HttpGet]
        public ActionResult Detail(int id)
        {
            if (ModelState.IsValid)
            {
                accountDao = new AccountDAO();
                var account = accountDao.getAccountById(id);

                if (account == null)
                {
                    setAlert("Không tìm thấy tài khoản tương ứng", "danger");
                    return RedirectToAction("Index", "Account");
                }

                List<SelectListItem> items = new List<SelectListItem>();
                items.Add(new SelectListItem { Text = "Quản trị", Value = "0" });
                items.Add(new SelectListItem { Text = "Người dùng", Value = "1" });

                ViewBag.Roles = items;
                return View(account);
            }
            return View();
        }

        // POST: /Admin/Account/Create
        [HttpPost]
        public ActionResult Create(Account account)
        {
            if (ModelState.IsValid)
            {
                accountDao = new AccountDAO();

                // Name
                if (string.IsNullOrEmpty(account.name))
                {
                    setAlert("Vui lòng nhập họ tên", "danger");
                    return RedirectToAction("Create", "Account");
                }

                // Email
                if (string.IsNullOrEmpty(account.email))
                {
                    setAlert("Vui lòng nhập email", "danger");
                    return RedirectToAction("Create", "Account");
                }

                // Password
                if (string.IsNullOrEmpty(account.password))
                {
                    setAlert("Vui lòng nhập mật khẩu", "danger");
                    return RedirectToAction("Create", "Account");
                }

                // Role
                if (account.role == null)
                {
                    setAlert("Vui lòng chọn phân quyền", "danger");
                    return RedirectToAction("Create", "Account");
                }

                // Phone
                if (string.IsNullOrEmpty(account.phone))
                {
                    setAlert("Vui lòng nhập số điện thoại", "danger");
                    return RedirectToAction("Create", "Account");
                }

                // Address
                if (string.IsNullOrEmpty(account.address))
                {
                    setAlert("Vui lòng nhập số điện thoại", "danger");
                    return RedirectToAction("Create", "Account");
                }

                if (accountDao.getAccountById(account.id) != null)
                {
                    setAlert("Id này đã tồn tại", "danger");
                    return RedirectToAction("Create", "Account");
                }

                if (accountDao.getAccountByEmail(account.email) != null)
                {
                    setAlert("Email này đã tồn tại, vui lòng nhập một email khác", "danger");
                    return RedirectToAction("Create", "Account");
                }

                if (!accountDao.CreateAccount(account))
                {
                    setAlert("Tạo tài khoản mới thất bại", "danger");
                    return RedirectToAction("Create", "Account");
                    // Thành công 
                }
            }
            setAlert("Tạo tài khoản mới thành công", "success");
            return RedirectToAction("Create", "Account");
        }

        // POST
        [HttpPost]
        public ActionResult Update(Account account)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra xem laptop có tồn tại không 
                accountDao = new AccountDAO();
                var accountTmp = accountDao.getAccountById(account.id);
                if (accountTmp == null)
                {
                    setAlert("Không tìm thấy tài khoản tương ứng", "danger");
                    return RedirectToAction("Index", "Account");
                }

                // Name
                if (string.IsNullOrEmpty(account.name))
                {
                    setAlert("Vui lòng nhập họ tên", "danger");
                    return RedirectToAction("Create", "Account");
                }

                // Email
                if (string.IsNullOrEmpty(account.email))
                {
                    setAlert("Vui lòng nhập email", "danger");
                    return RedirectToAction("Create", "Account");
                }

                // Password
                if (string.IsNullOrEmpty(account.password))
                {
                    setAlert("Vui lòng nhập mật khẩu", "danger");
                    return RedirectToAction("Create", "Account");
                }

                // Role
                if (account.role == null)
                {
                    setAlert("Vui lòng chọn phân quyền", "danger");
                    return RedirectToAction("Create", "Account");
                }

                // Phone
                if (string.IsNullOrEmpty(account.phone))
                {
                    setAlert("Vui lòng nhập số điện thoại", "danger");
                    return RedirectToAction("Create", "Account");
                }

                // Address
                if (string.IsNullOrEmpty(account.address))
                {
                    setAlert("Vui lòng nhập số điện thoại", "danger");
                    return RedirectToAction("Create", "Account");
                }

                accountTmp.name = account.name;
                accountTmp.email = account.email;
                accountTmp.password = account.password;
                accountTmp.role = account.role;
                accountTmp.phone = account.phone;
                accountTmp.address = account.address;

                if (accountDao.UpdateAccount(accountTmp))
                {
                    setAlert("Cập nhật tài khoản thành công", "success");
                    return RedirectToAction("Update", "Account");
                }
                setAlert("Xảy ra lỗi khi cập nhật tài khoản", "danger");
                return RedirectToAction("Update", "Account");
            }
            return RedirectToAction("Update");
        }

        // Delete
        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                accountDao = new AccountDAO();
                var account = accountDao.getAccountById(id);

                if (account == null)
                {
                    setAlert("Không tìm thấy tài khoản tương ứng", "danger");
                    return RedirectToAction("Index", "Account");
                }

                if (accountDao.DeleteAccount(account))
                {
                    setAlert("Xóa tài khoản thành công", "success");
                    return RedirectToAction("Index", "Account");
                }
                setAlert("Xảy ra lỗi khi xóa tài khoản", "danger");
                return RedirectToAction("Index", "Account");
            }
            return RedirectToAction("Index");
        }
    }
}