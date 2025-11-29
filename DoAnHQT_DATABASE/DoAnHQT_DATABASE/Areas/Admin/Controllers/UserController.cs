using DoAnHQT_DATABASE.Areas.Admin.Security;
using DoAnHQT_DATABASE.Areas.Admin.Service;
using DoAnHQT_DATABASE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnHQT_DATABASE.Areas.Admin.Controllers
{
    [CheckAthourize(Roles = "Admin, Nhân viên")]
    public class UserController : Controller
    {
        // GET: Admin/User
        QL_BANHANG_ONLINE db = new QL_BANHANG_ONLINE();
        UserService userService = new UserService();
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        public ActionResult DisableKhachHang(string userID)
        {
            string userEmployee = Session["EmployeeName"].ToString();

            int ret = userService.DisableUser(userID, userEmployee);
            if (ret != 0)
            {
                TempData["SuccessDisable"] = $"Disable khách hàng {userID} thành công";
                return RedirectToAction("Index", "User");
            }
            else
            {
                TempData["ErrorDisable"] = $"Disable khách hàng {userID} không thành công";
                return RedirectToAction("Index", "User");
            }
        }

        public ActionResult EnableKhachHang(string userID)
        {
            string userEmployee = Session["EmployeeName"].ToString();

            int ret = userService.EnableUser(userID, userEmployee);
            if (ret != 0)
            {
                TempData["SuccessDisable"] = $"Enable khách hàng {userID} thành công";
                return RedirectToAction("Index", "User");
            }
            else
            {
                TempData["ErrorDisable"] = $"Enable khách hàng {userID} không thành công";
                return RedirectToAction("Index", "User");
            }
        }

        [HttpPost]
        public ActionResult TimKiemUser(string keyword)
        {
            keyword = keyword.Trim().ToLower();
            List<Users> ds = db.Users.ToList().FindAll(t => t.Name.Trim().ToLower().Contains(keyword) || t.UserID.ToLower().Trim().Equals(keyword));
            return View("Index", ds);
        }

        public ActionResult UserDetail()
        {
            return View()
        }

    }
}