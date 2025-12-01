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

        public ActionResult UpdateDonHang(string orderID)
        {
            Orders order = db.Orders.FirstOrDefault(t => t.OrderID.Equals(orderID));
            return View(order);
        }

        [HttpPost]
        public ActionResult UpdateOnSubmit(FormCollection form)
        {
            string status = form["status"].ToString();
            string orderID = form["orderID"].ToString();
            Orders order = db.Orders.FirstOrDefault(t => t.OrderID.Contains(orderID));
            string employeeName = Session["EmployeeName"].ToString();
            try
            {
                order.Status = status;
                int ret = orderService.UpdateDonHang(orderID, order.UserID, order.OrderDate.Value, order.Address, order.Status, order.UserPaymentMethod, employeeName); 
                if (ret != 0)
                {
                    TempData["UpdateSuccess"] = "Cập nhật thành công";
                    return View("ChiTietDonHang", order);
                }
                else
                {
                    TempData["UpdateError"] = "Cập nhật thất bại";
                    return View("ChiTietDonHang", order);
                }
            }
            catch(Exception e)
            {
                TempData["UpdateError"] = $"Cập nhật thất bại Lỗi: {e.Message}";
                return View("ChiTietDonHang", order);
            }
        }





    }
}