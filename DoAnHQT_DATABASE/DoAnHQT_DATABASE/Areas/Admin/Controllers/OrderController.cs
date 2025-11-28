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




    }
}