using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnHQT_DATABASE.Areas.Admin.Service;
using DoAnHQT_DATABASE.Areas.Admin.Security;
using DoAnHQT_DATABASE.Areas.Admin.Models;

namespace DoAnHQT_DATABASE.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        AccountService accountService = new AccountService();
        // GET: Admin/Account
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username, string password, string role)
        {
            bool isValid = accountService.Connect(username, password, role);
            if (isValid)
            {
                Session["Role"] = role;
                Session["EmployeeName"] = username;

                if (role == "Admin")
                {
                    // Chuyển hướng đến trang Admin
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    //Trang nhân viên
                    return RedirectToAction("Index", "Dashboard");
                }
            }
            else
            {
                // Dùng dòng này để xem lỗi cụ thể là gì
                ViewBag.Error = "Lỗi đăng nhập: " + "Vui lòng kiểm tra Username/Password hoặc Server SQL chưa chạy.";

                // Quan trọng: Trả về View() để giữ lại màn hình và hiện lỗi, KHÔNG dùng Redirect
                return View();
                //TempData["DatabaseError"] = "Sai thông tin hoặc lỗi database";
                //return RedirectToAction("Error", "Exception");
            }
        }
        public ActionResult Logout()
        {
            string role = Session["Role"]?.ToString() ?? "";
            if (string.IsNullOrWhiteSpace(role))
            {
                ViewBag.Error = "Bạn chưa đăng nhập!";
                return View("Login", new { area = "admin" });
            }
            else
            {
                Session.Clear();
                return RedirectToAction("Login", "Account", new { area = "admin" });
            }
        }
        [CheckAthourize(Roles = "Admin")]
        public ActionResult Index()
        {
            
            List<StaffViewModel> lstNhanViens = accountService.GetAllStaffs();
            return View(lstNhanViens);
        }

        [CheckAthourize(Roles = "Admin")]
        public ActionResult Unlock(string username)
        {
            bool check = accountService.UnlockStaff(username);
            if (!check) ViewBag.Error = "Mở khóa thất bại";
            return RedirectToAction("Index", new { area = "admin" });
        }

        
        [CheckAthourize(Roles = "Admin")]
        public ActionResult Lock(string username)
        {
            bool check = accountService.LockStaff(username);
            if (!check) ViewBag.Error = "Khóa thất bại";
            return RedirectToAction("Index", new { area = "admin" });
        }

        [CheckAthourize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Create(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ViewBag.Error = "Bạn chưa nhập đủ thông tin!";
                return RedirectToAction("Index", new { area = "admin" });
            }
            else
            {
                bool check = accountService.AddStaff(username, password);
                if (!check)
                {
                    ViewBag.Error = "Thêm không thành công";
                }
                return RedirectToAction("Index", new { area = "admin" });
            }
        }
        [CheckAthourize(Roles = "Admin")]
        public ActionResult Delete(string username)
        {
            bool check = accountService.DeleteStaff(username);
            if (!check)
            {
                ViewBag.Error = "Xoá không thành công";
            }
            return RedirectToAction("Index", new { area = "admin" });
        }
    }
}