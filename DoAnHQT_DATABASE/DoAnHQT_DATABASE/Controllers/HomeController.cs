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
            return View("ShowSanPham", db.Product.ToList().FindAll(t => t.BrandID.Trim().Equals(id.Trim()) && t.IsAvailable == 0));
        }

        public ActionResult TimTheoLoai(string id)
        {
            List<Product> ls = db.Product.ToList().FindAll(t => t.ProductTypeID.Trim().Equals(id.Trim()) && t.IsAvailable == 0);
            Session["DanhSachSanPhamHienTai"] = ls;
            return View("ShowSanPham", ls);
        }

        public ActionResult ShowSanPham()
        {
            Session["DanhSachSanPhamHienTai"] = null;
            return View(db.Product.Where(t => t.IsAvailable == 0).ToList());
        }

        public ActionResult HienThiDanhMuc()
        {
            return PartialView(db.ProductType.Where(t => t.IsDeleted == 0).ToList());
        }
        public ActionResult SanPhamDetail(string id)
        {
            Product sp = db.Product.FirstOrDefault(t => t.ProductID.Trim().Equals(id.Trim()));
            return View(sp);
        }

        public ActionResult SapXepMoiNhat()
        {
            List<Product> list = productService.LocSanPham("CreatedAt", "Desc");
            if (Session["DanhSachSanPhamHienTai"] != null)
            {
                List<Product> lsHienTai = Session["DanhSachSanPhamHienTai"] as List<Product>;
                List<string> lsID = lsHienTai.Select(t => t.ProductID).ToList();
                list = list.Where(t => lsID.Contains(t.ProductID)).ToList();
            }
            return View("ShowSanPham", list);
        }

        public ActionResult SapXepBanChay()
        {
            List<Product> list = productService.LocSanPham("TotalSold", "Desc");
            if (Session["DanhSachSanPhamHienTai"] != null)
            {
                List<Product> lsHienTai = Session["DanhSachSanPhamHienTai"] as List<Product>;
                List<string> lsID = lsHienTai.Select(t => t.ProductID).ToList();
                list = list.Where(t => lsID.Contains(t.ProductID)).ToList();
            }
            return View("ShowSanPham", list);
        }

        public ActionResult SapXepGia(int order)
        {
            List<Product> list = new List<Product>();
            if (order == 1)
                list = productService.LocSanPham("GiaDaGiam", "ASC");
            else
                list = productService.LocSanPham("GiaDaGiam", "Desc");

            if (Session["DanhSachSanPhamHienTai"] != null)
            {
                List<Product> lsHienTai = Session["DanhSachSanPhamHienTai"] as List<Product>;
                List<string> lsID = lsHienTai.Select(t => t.ProductID).ToList();
                list = list.Where(t => lsID.Contains(t.ProductID)).ToList();
            }
            return View("ShowSanPham", list);
        }

        [HttpPost]
        public ActionResult TimKiemTheoTen(string name)
        {
            List<Product> list = productService.TimKiemTheoTen(name);
            List<string> searchHistory = Session["searchHistory"] != null ? Session["searchHistory"] as List<string> : new List<string>();
            if (searchHistory.Contains(name) == false)
                searchHistory.Add(name);
            Session["searchHistory"] = searchHistory;

            return View("ShowSanPham", list);
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult OrderPage()
        {
            Users user = Session["User"] as Users;
            if (user == null)
            {
                return View("Index");
            }
            List<Orders> ds = db.Orders.ToList().FindAll(t => t.UserID.Equals(user.UserID));

            return View(ds);
        }

        //public ActionResult TimKiemDonHang()
        //{

        //}

        public ActionResult OrderDetail(string orderID)
        {
            Orders order = db.Orders.FirstOrDefault(t => t.OrderID.Equals(orderID));
            return View(order);
        }
    }
}