using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnHQT_DATABASE.Areas.Admin.Security;
using DoAnHQT_DATABASE.Areas.Admin.Service;
using DoAnHQT_DATABASE.Models;

namespace DoAnHQT_DATABASE.Areas.Admin.Controllers
{
    [CheckAthourize(Roles = "Admin, Nhân viên")]
    public class DashboardController : Controller
    {
        // GET: Admin/DashBoard
        QL_BANHANG_ONLINE db = new QL_BANHANG_ONLINE();
        OrderService orderService = new OrderService();
        public ActionResult Index()
        {
            if (db == null)
            {
                TempData["DatabaseError"] = "Lỗi database";
                return RedirectToAction("Error", "Exception");
            }

            ViewBag.TongSanPham = db.Product.Count();
            ViewBag.TongDonHang = db.Orders.Count();
            ViewBag.TongKhachHang = db.Users.Count();
            ViewBag.DoanhThu = orderService.TongDoanhThu().ToString("N0");
            return View();
        }
    }
}