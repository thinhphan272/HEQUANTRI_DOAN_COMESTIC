using DoAnHQT_DATABASE.Areas.Admin.Security;
using DoAnHQT_DATABASE.Areas.Admin.Service;
using DoAnHQT_DATABASE.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace DoAnHQT_DATABASE.Areas.Admin.Controllers
{
    [CheckAthourize(Roles = "Admin, Nhân viên")]
    public class ProductController : Controller
    {
        QL_BANHANG_ONLINE db = new QL_BANHANG_ONLINE();
        ProductService productService = new ProductService();

        // GET: Admin/Product
        public ActionResult Index()
        {
            List<Product> dssp = db.Product.ToList();
            return View(dssp);
        }

        public ActionResult Create()
        {
            ViewBag.Brand = new SelectList(db.Brand.ToList(), "BrandID", "BrandName");
            ViewBag.ProductType = new SelectList(db.ProductType.ToList(), "ProductTypeID", "ProductTypeName");
            return View();
        }

        public ActionResult Edit(string productID)
        {
            ViewBag.Brand = new SelectList(db.Brand.ToList(), "BrandID", "BrandName");
            ViewBag.ProductType = new SelectList(db.ProductType.ToList(), "ProductTypeID", "ProductTypeName");
            Product product = db.Product.FirstOrDefault(t => t.ProductID.Equals(productID));
            return View(product);
        }

        public ActionResult CreateOnSubmit(Product product, HttpPostedFileBase ImageUpload, string IsAvailable)
        {
            if (ModelState.IsValid)
            {
                string FileName = "";
                string Dir = "/Content/Images/ProductIMG/";
               if (ImageUpload != null && ImageUpload.ContentLength > 0)
                {
                    FileName = Path.GetFileName(ImageUpload.FileName);
                    string physicalDir = Server.MapPath(Dir);
                    if (!Directory.Exists(physicalDir))
                    {
                        Directory.CreateDirectory(physicalDir);
                    }
                    string path = Path.Combine(Server.MapPath(Dir), FileName);
                    ImageUpload.SaveAs(path);
                    product.Image = FileName;
                }

                // Them ID moi
                Product lastProduct = db.Product.OrderByDescending(t => t.ProductID).First();
                string lastedProductID = lastProduct.ProductID.ToString();
                int newID = 1;
                if (lastProduct != null)
                {
                    string lastIDString = lastProduct.ProductID;
                    var match = Regex.Match(lastIDString, @"\d+");
                    if (match.Success)
                    {
                        newID = int.Parse(match.Value) + 1;
                    }
                }
                string newProductID = $"SP{newID:000}";
                string employeeName = Session["EmployeeName"].ToString();

                try
                {


                    int ret = productService.ThemSanPham(newProductID, product.ProductTypeID, product.ProductName, product.BrandID, product.Price.Value, product.Origin, product.Description, product.Image, product.Capacity.Value, product.Quantity.Value, product.ExpirationDate.Value, employeeName);

                    if (IsAvailable != null)
                    {
                        productService.EnableSanPham(newProductID);
                    }
                    else
                    {
                        productService.DisableSanPham(newProductID);
                        //Xóa tất cả sản phẩm hiện có trong giỏ
                        productService.XoaTatCaSPTrongGio(newProductID);
                    }

                    if (ret != 0)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["AddError"] = "Lỗi, cập nhật sản phẩm không thành công!";
                        return RedirectToAction("Create");
                    }
                }
                catch(Exception e)
                {
                    TempData["AddError"] = $"Lỗi, cập sản phẩm không thành công! \n {e.Message}";
                    return RedirectToAction("Create");
                }
            }
            return RedirectToAction("Create");
        }

        public ActionResult EditOnSubmit(Product product, HttpPostedFileBase ImageUpload, string IsAvailable)
        {
            ViewBag.Brand = new SelectList(db.Brand.ToList(), "BrandID", "BrandName");
            ViewBag.ProductType = new SelectList(db.ProductType.ToList(), "ProductTypeID", "ProductTypeName");
            if (ModelState.IsValid)
            {
                string FileName = "";
                string Dir = "/Content/Images/ProductIMG/";
                if (ImageUpload != null && ImageUpload.ContentLength > 0)
                {
                    FileName = Path.GetFileName(ImageUpload.FileName);
                    string physicalDir = Server.MapPath(Dir);
                    if (!Directory.Exists(physicalDir))
                    {
                        Directory.CreateDirectory(physicalDir);
                    }
                    string path = Path.Combine(Server.MapPath(Dir), FileName);
                    ImageUpload.SaveAs(path);
                    product.Image = FileName;
                }

                string employeeName = Session["EmployeeName"].ToString();

                try
                {
                    product.Description = string.IsNullOrEmpty(product.Description) ? "Không mô tả" : product.Description;
                    product.Origin = string.IsNullOrEmpty(product.Description) ? "Việt Nam" : product.Origin;

                    int ret = productService.SuaSanPham(product.ProductID, product.ProductTypeID, product.ProductName, product.BrandID, product.Price.Value, product.Origin, product.Description, product.Image, product.Capacity.Value, product.Quantity.Value, product.ExpirationDate.Value, employeeName);

                    if (IsAvailable != null)
                    {
                        productService.EnableSanPham(product.ProductID);
                    }
                    else
                    {
                        productService.DisableSanPham(product.ProductID);
                        //Xóa tất cả sản phẩm hiện có trong giỏ
                        productService.XoaTatCaSPTrongGio(product.ProductID);
                    }

                    if (ret != 0)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["UpdateError"] = "Lỗi, thêm sản phẩm không thành công!";
                        return View("Edit", product);
                    }
                }
                catch (Exception e)
                {
                    TempData["UpdateError"] = $"Lỗi, thêm sản phẩm không thành công! \n {e.Message}";
                    return View("Edit", product);
                }
            }
            return View("Edit", product);
        }

        public ActionResult DisableSanPham(string productID)
        {
            productService.DisableSanPham(productID);

            //Xóa tất cả sản phẩm hiện có trong giỏ
            productService.XoaTatCaSPTrongGio(productID);

            return RedirectToAction("Index", "Product");
        }

    }
}