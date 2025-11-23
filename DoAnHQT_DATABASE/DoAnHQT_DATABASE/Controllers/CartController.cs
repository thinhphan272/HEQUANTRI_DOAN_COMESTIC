using DoAnHQT_DATABASE.Models;
using DoAnHQT_DATABASE.Services;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace DoAnHQT_DATABASE.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        QL_BANHANG_ONLINE db = new QL_BANHANG_ONLINE();
        CartService cartService = new CartService();
        OrderService orderService = new OrderService();
        public ActionResult Cart()
        {
            Users user = Session["User"] as Users;
            user = db.Users.FirstOrDefault(t => t.UserID.Equals(user.UserID));
            ShoppingCart cart = user.ShoppingCart.First();

            return View(cart);
        }

        public ActionResult CartMini()
        {
            Users user = Session["User"] as Users;
            user = db.Users.FirstOrDefault(t => t.UserID.Equals(user.UserID));
            ShoppingCart cart = user.ShoppingCart.First();
            return PartialView(cart);
        }

        public ActionResult PaymentPage()
        {
            Users user = Session["User"] as Users;
            user = db.Users.FirstOrDefault(t => t.UserID.Equals(user.UserID));
            ShoppingCart cart = user.ShoppingCart.First();

            return View(cart);
        }

        public ActionResult ThemVaoGio(string productID, int sl)
        {
            if (Session["User"] == null)
            {
                return Json(new { success = false, requiresLogin = true, message = "Vui lòng đăng nhập để thêm sản phẩm vào giỏ hàng." }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                Users user = Session["User"] as Users;
                string userID = user.UserID.ToString();

                //Tìm giỏ hàng hiện tại
                var userCart = db.ShoppingCart.FirstOrDefault(t => t.UserID.Equals(userID));
                string currentCartID = userCart.ShoppingCartID.ToString(); ;

                int ret = 0;
                ShoppingCartItem cartItem = userCart.ShoppingCartItem.FirstOrDefault(t => t.ProductID.Equals(productID));
                if (cartItem == null)
                {
                    ret = cartService.ThemSanPhamVaoGio(currentCartID, productID, sl);
                }
                else
                {
                    ret = cartService.SuaSanPhamTrongGio(currentCartID, productID, cartItem.Quantity.Value + sl);
                }

                if (ret != 0)
                    return Json(new { success = true, message = "Đã thêm sản phẩm vào giỏ hàng!" }, JsonRequestBehavior.AllowGet);
                return Json(new { success = false, message = "Thêm không thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi Server: " + ex.Message });
            }
        }

        public ActionResult XoaTrongGio(string productID)
        {
            Users user = Session["User"] as Users;
            user = db.Users.FirstOrDefault(t => t.UserID.Equals(user.UserID));
            ShoppingCart cart = user.ShoppingCart.First();
            int ret = cartService.XoaSanPhamTrongGio(cart.ShoppingCartID, productID);

            return View("Cart", cart);
        }

        public ActionResult UpdateSoLuong(string productID, int type)
        {
            Users user = Session["User"] as Users;
           
            var dbUser = db.Users.FirstOrDefault(t => t.UserID == user.UserID);
            var cart = dbUser.ShoppingCart.FirstOrDefault(); 

            if (cart != null)
            {
                var cartItem = cart.ShoppingCartItem.FirstOrDefault(t => t.ProductID == productID);

                if (cartItem != null)
                {
                    int currentQuantity = cartItem.Quantity ?? 0; 
                    int newQuantity = currentQuantity;

                    if (type == 1)
                    {
                        newQuantity++;
                    }
                    else if (type == -1)
                    {
                        newQuantity--;
                    }

                    if (newQuantity < 1)
                    {
                        newQuantity = 1; 
                    }

                    if (newQuantity != currentQuantity)
                    {
                        cartService.SuaSanPhamTrongGio(cart.ShoppingCartID, productID, newQuantity);
                    }
                }
            }

            return View("Cart", cart);
        }

        public ActionResult DatHang(FormCollection form)
        {
            Users user = Session["User"] as Users;
            string userID = user.UserID;
            DateTime orderDate = DateTime.Now;
            string address = form["address"];
            string status = "Chờ giao hàng";
            string userPayment = form["payment"];

            //Tao orderID
            var lastOrder = db.Orders.OrderByDescending(t => t.OrderID).FirstOrDefault();
            int newID = 1;
            if (lastOrder != null)
            {
                string lastID = lastOrder.OrderID;
                var match = Regex.Match(lastID, @"\d+");
                if (match.Success)
                    newID = int.Parse(match.Value) + 1;
            }
            string newOrderID = $"OD{newID:00}";

            int ret = orderService.DatHang(newOrderID, userID, orderDate, address, status, userPayment, user.Name);

            if (ret == 0)
            {
                ViewBag.OrderError = "Lỗi! Đặt hàng không thành công";
                return View("PaymentPage");
            }
            return View("OrderSuccess");
        }

        public ActionResult OrderSuccess()
        {
            return View();
        }
    }
}