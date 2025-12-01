using DoAnHQT_DATABASE.Areas.Admin.Security;
using DoAnHQT_DATABASE.Areas.Admin.Service;
using DoAnHQT_DATABASE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace DoAnHQT_DATABASE.Areas.Admin.Controllers
{
    [CheckAthourize(Roles = "Admin, Nhân viên")]
    public class DiscountController : Controller
    {
        // GET: Admin/Discount
        QL_BANHANG_ONLINE db = new QL_BANHANG_ONLINE();
        DiscountService discountService = new DiscountService();
        public ActionResult Index()
        {
            if (db == null)
            {
                TempData["DatabaseError"] = "Lỗi database";
                return RedirectToAction("Error", "Exception");
            }

            return View(db.Discount.ToList());
        }

        public ActionResult UpdateDiscount(string discountID)
        {
            Discount discount = db.Discount.FirstOrDefault(t => t.DiscountID.Equals(discountID));
            ViewBag.ProductList = db.Product.ToList();
            return View(discount);
        }

        [HttpPost]
        public ActionResult UpdateDiscountOnSubmit(FormCollection form)
        {
            string discountID = form["discountID"].ToString();
            string productID = form["productID"].ToString();
            string discountName = form["discountName"].ToString();
            DateTime startDate = DateTime.Parse(form["startDate"]);
            DateTime endDate = DateTime.Parse(form["endDate"]);
            double discountRate = double.Parse(form["discountRate"]);
            string employeeName = Session["EmployeeName"].ToString();

            Discount discount = db.Discount.FirstOrDefault(t => t.DiscountID.Contains(discountID));
            try
            {
                int ret = discountService.SuaDiscount(discountID, productID, discountName, startDate, endDate, discountRate, employeeName);
                
                if (ret != 0)
                {
                    TempData["UpdateSucess"] = "Cập nhật discount thành công";
                    return View("DetailDiscountPage", discount);
                }
                else
                {
                    TempData["UpdateError"] = "Cập nhật discount không thành công";
                    return View("UpdateDiscount", discount);
                }
            }
            catch(Exception e)
            {
                TempData["UpdateError"] = $"Cập nhật discount không thành công \n {e.Message}";
                return View("UpdateDiscount", discount);
            }
        }

        public ActionResult DetailDiscountPage(string discountID)
        {
            Discount discount = db.Discount.FirstOrDefault(t => t.DiscountID.Equals(discountID));
            return View(discount);
        }

        public ActionResult DeleteDiscount(string discountID)
        {
            try
            {
                int ret = discountService.XoaDiscount(discountID);
                if (ret != 0)
                {
                    TempData["DeleteSucess"] = "Xóa discount thành công";
                    return RedirectToAction("Index", "Discount");
                }
                else
                {
                    TempData["DeleteError"] = "Xóa discount không thành công";
                    return RedirectToAction("Index", "Discount");
                }
            }
            catch(Exception e)
            {
                TempData["DeleteError"] = $"Xóa discount không thành công \n {e.Message}";
                return RedirectToAction("Index", "Discount");
            }
        }

        public ActionResult AddDiscountPage()
        {
            ViewBag.ProductList = db.Product.ToList();
            return View();
        }

      
        public ActionResult TimGiamGia(string keyword)
        {
            keyword = keyword.Trim().ToLower();
            List<Discount> ds = db.Discount.ToList().FindAll(t => t.DiscountName.Trim().ToLower().Contains(keyword) || t.DiscountID.Trim().ToLower().Contains(keyword));
            return View("Index", ds);
        }

        [HttpPost]
        public ActionResult AddDiscountOnSubmit(FormCollection form)
        {
            string productID = form["productID"].ToString();
            string discountName = form["discountName"].ToString();
            DateTime startDate = DateTime.Parse(form["startDate"]);
            DateTime endDate = DateTime.Parse(form["endDate"]);
            double discountRate = double.Parse(form["discountRate"]);
            string employeeName = Session["EmployeeName"].ToString();

            try
            {
                // Them ID moi
                Discount lastDiscount = db.Discount.OrderByDescending(t => t.DiscountID).First();
                int newID = 1;
                if (lastDiscount != null)
                {
                    string lastIDString = lastDiscount.DiscountID;
                    var match = Regex.Match(lastIDString, @"\d+");
                    if (match.Success)
                    {
                        newID = int.Parse(match.Value) + 1;
                    }
                }
                string newDiscountID = $"GG{newID:000}";

                int ret = discountService.ThemDiscount(newDiscountID, productID, discountName, startDate, endDate, discountRate, employeeName);

                if (ret != 0)
                {
                    TempData["AddSucess"] = "Thêm discount thành công";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["AddError"] = "Thêm discount không thành công";
                    return RedirectToAction("AddDiscountPage");
                }
            }
            catch (Exception e)
            {
                TempData["AddError"] = $"Thêm discount không thành công \n {e.Message}";
                return RedirectToAction("AddDiscountPage");
            }
        }

    }
}