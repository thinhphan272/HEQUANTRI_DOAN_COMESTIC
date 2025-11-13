using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnHQT_DATABASE.Models;

namespace DoAnHQT_DATABASE.Controllers
{
    public class HomeController : Controller        
    {
        QL_BANHANG_ONLINE db = new QL_BANHANG_ONLINE();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowSanPham()
        {
            return View(db.Product.ToList());
        }

        public ActionResult SanPhamDetail()
        {
            return View();
        }

    }
}