using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnHQT_DATABASE.Models;
using DoAnHQT_DATABASE.Services;

namespace DoAnHQT_DATABASE.Controllers
{
    public class HomeController : Controller
    {
        QL_BANHANG_ONLINE db = new QL_BANHANG_ONLINE();
        ProductService productService = new ProductService();

        public ActionResult Index()
        {
            ViewBag.BrandList = db.Brand.ToList();
            ViewBag.ProductTypeList = db.ProductType.ToList();
            ViewBag.SanPhamGiamSau = productService.SanPhamGiamSauNhat(10);
            return View();
        }

        public ActionResult TimTheoBrand(string id)
        {
            return View("ShowSanPham", db.Product.ToList().FindAll(t => t.BrandID.Trim().Equals(id.Trim())));
        }

        public ActionResult TimTheoLoai(string id)
        {
            return View("ShowSanPham", db.Product.ToList().FindAll(t => t.ProductTypeID.Trim().Equals(id.Trim())));
        }

        public ActionResult ShowSanPham()
        {
            return View(db.Product.ToList());
        }

        public ActionResult HienThiDanhMuc()
        {
            return PartialView(db.ProductType.ToList());
        }
        public ActionResult SanPhamDetail(string id)
        {
            Product sp = db.Product.FirstOrDefault(t => t.ProductID.Trim().Equals(id.Trim()));
            return View(sp);
        }

        public ActionResult SapXepMoiNhat()
        {
            List<Product> list = productService.LocSanPham("CreatedAt", "Desc");
            return View("ShowSanPham", list);
        }

        public ActionResult SapXepBanChay()
        {
            List<Product> list = productService.LocSanPham("TotalSold", "Desc");
            return View("ShowSanPham", list);
        }

        public ActionResult SapXepGia(int order)
        {
            List<Product> list = new List<Product>();
            if (order == 1)
                list = productService.LocSanPham("TotalSold", "ASC");
            else
                list = productService.LocSanPham("TotalSold", "Desc");
            return View("ShowSanPham", list);
        }

        public ActionResult TimKiemTheoTen(string name)
        {
            List<Product> list = productService.TimKiemTheoTen(name);
            List<string> searchHistory = Session["searchHistory"] != null ? Session["searchHistory"] as List<string> : new List<string>();
            if (searchHistory.Contains(name) == false)
                searchHistory.Add(name);
            Session["searchHistory"] = searchHistory;

            return View("ShowSanPham", list);
        }

       
    }
}