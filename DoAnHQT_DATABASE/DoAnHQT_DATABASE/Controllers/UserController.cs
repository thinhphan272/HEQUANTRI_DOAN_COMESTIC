using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using DoAnHQT_DATABASE.Models;
using DoAnHQT_DATABASE.Services;

namespace DoAnHQT_DATABASE.Controllers
{
    public class UserController : Controller
    {
        UserService userService = new UserService();
        CartService cartService = new CartService();
        QL_BANHANG_ONLINE db = new QL_BANHANG_ONLINE();
        // GET: User
        [HttpPost]
        public ActionResult RegisterOnSubmit(string name, string email, string password, string gioitinh, string address)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                return Json(new { success = false, message = "Vui lòng nhập đầy đủ thông tin!" });
            }

            try
            {
                var lastUser = db.Users.OrderByDescending(u => u.UserID).FirstOrDefault();

                int newID = 1; 
                if (lastUser != null)   
                {
                    string lastIDString = lastUser.UserID;
                    var match = Regex.Match(lastIDString, @"\d+");
                    if (match.Success)
                    {
                        newID = int.Parse(match.Value) + 1;
                    }
                }

                string newUserID = $"US{newID:00}";

                int ret = userService.DangKy(newUserID, name, email, password, gioitinh, address, name);

                //Thêm giỏ hàng
                ShoppingCart lastCart = db.ShoppingCart.OrderByDescending(u => u.UserID).FirstOrDefault();
                string lastedCartID = lastCart.ShoppingCartID.ToString();
                newID = 1;
                if (lastCart != null)
                {
                    string lastIDString = lastCart.ShoppingCartID;
                    var match = Regex.Match(lastIDString, @"\d+");
                    if (match.Success)
                    {
                        newID = int.Parse(match.Value) + 1;
                    }
                }
                string newcartID = $"SC{newID:00}";
                cartService.ThemGioHang(newcartID, newUserID);

                if (ret == 0)
                    return Json(new { success = false, message = "Đăng ký không thành công (Có thể Email đã tồn tại)" });

                return Json(new { success = true, message = "Đăng ký thành công!" });
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Json(new { success = false, message = "Lỗi hệ thống: " + msg });
            }
        }

        [HttpPost]
        public ActionResult LoginOnSubmit(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                return Json(new { success = false, message = "Vui lòng nhập email và mật khẩu!" });
            }

            try
            {
                int ret = userService.DangNhap(email.Trim(), password);

                if (ret == 1)
                {
                    Users user = db.Users.FirstOrDefault(t => t.Email.Equals(email));
                    Session["User"] = user;

                    return Json(new { success = true, message = "Đăng nhập thành công!" });
                }
                else
                {
                    return Json(new { success = false, message = "Email hoặc mật khẩu không chính xác!" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi hệ thống: " + ex.Message });
            }
        }


        public ActionResult DangXuat()
        {
            Session["User"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}