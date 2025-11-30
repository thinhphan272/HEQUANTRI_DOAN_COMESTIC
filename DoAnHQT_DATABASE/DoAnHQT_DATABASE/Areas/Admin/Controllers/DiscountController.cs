using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnHQT_DATABASE.Models;

namespace DoAnHQT_DATABASE.Areas.Admin.Controllers
{
    public class DiscountController : Controller
    {
        // GET: Admin/Discount
        QL_BANHANG_ONLINE db = new QL_BANHANG_ONLINE();
        public ActionResult Index()
        {
            if (db == null)
            {
                TempData["DatabaseError"] = "Lỗi database";
                return RedirectToAction("Error", "Exception");
            }

            return View();
        }
    }
}