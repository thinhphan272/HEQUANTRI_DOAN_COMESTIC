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
    public class OrderController : Controller
    {
        QL_BANHANG_ONLINE db = new QL_BANHANG_ONLINE();
        OrderService orderService = new OrderService();
        // GET: Admin/Order
        public ActionResult Index()
        {
            if (db == null)
            {
                TempData["DatabaseError"] = "Lỗi database";
                return RedirectToAction("Error", "Exception");
            }

            List<Orders> listOrders = db.Orders.ToList();
            return View(listOrders);
        }

        public ActionResult HuyDonHang(string orderID)
        {
            string userName = Session["EmployeeName"].ToString();
            orderService.CancelDonHang(orderID, userName);
            return RedirectToAction("Index", "Order");
        }

        public ActionResult ChiTietDonHang(string orderID)
        {
            Orders order = db.Orders.FirstOrDefault(t => t.OrderID.Equals(orderID));
            return View(order);
        }

        [HttpPost]
        public ActionResult TimDonHang(string keyword)
        {
            keyword = keyword.Trim().ToLower();
            List<Orders> ds = db.Orders.ToList().FindAll(t => t.Users.Name.Trim().ToLower().Contains(keyword) || t.UserID.Trim().ToLower().Contains(keyword) || t.OrderID.Trim().ToLower().Contains(keyword));
            return View("Index", ds);
        }


    }
}