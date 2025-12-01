using DoAnHQT_DATABASE.Areas.Admin.Security;
using DoAnHQT_DATABASE.Areas.Admin.Service;
using DoAnHQT_DATABASE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace DoAnHQT_DATABASE.Areas.Admin.Controllers
{
    [CheckAthourize(Roles = "Admin, Nhân viên")]
    public class GoodsReceiptNoteController : Controller
    {
        // GET: Admin/GoodsReceiptNote
        QL_BANHANG_ONLINE db = new QL_BANHANG_ONLINE();
        GoodsReceiptNoteService goodsReceiptNoteService = new GoodsReceiptNoteService();
        public ActionResult Index()
        {
            if (db == null)
            {
                TempData["DatabaseError"] = "Lỗi database";
                return RedirectToAction("Error", "Exception");
            }

            return View(db.GoodsReceiptNote.ToList());
        }
        public ActionResult TimPhieuNhap(string keyword)
        {
            keyword = keyword.Trim().ToLower();
            List<GoodsReceiptNote> ds = db.GoodsReceiptNote.ToList().FindAll(t => t.GoodsReceiptNoteID.Trim().ToLower().Contains(keyword));
            return View("Index", ds);
        }

        public ActionResult ThemPhieuNhap()
        {
            
            ViewBag.SupplierList = db.Supplier.ToList();

            return View();
        }

        [HttpPost]
        public ActionResult ThemPhieuNhapOnSubmit(FormCollection form)
        {
            DateTime receiptDate = DateTime.Parse(form["receiptDate"]);
            string supplierID = form["supplierID"].ToString();
            string employeeName = Session["EmployeeName"].ToString();

            // Them ID moi
            GoodsReceiptNote lastGRN = db.GoodsReceiptNote.OrderByDescending(t => t.GoodsReceiptNoteID).First();
            int newID = 1;
            if (lastGRN != null)
            {
                string lastIDString = lastGRN.GoodsReceiptNoteID;
                var match = Regex.Match(lastIDString, @"\d+");
                if (match.Success)
                {
                    newID = int.Parse(match.Value) + 1;
                }
            }
            string newGRNID = $"PH{newID:000}";

            TempData["GRNID"] = newGRNID;
            TempData["EmployeeName"] = employeeName;
            TempData["receiptDate"] = receiptDate;
            TempData["supplierID"] = supplierID;

            try
            {
                int ret = goodsReceiptNoteService.ThemPhieuNhap(newGRNID, supplierID, receiptDate, employeeName);
                if (ret != 0)
                {
                    TempData["AddGRNSuccess"] = "Thêm phiếu nhập thành công";
                    return RedirectToAction("ThemPhieuNhap");
                }
                else
                {
                    TempData["AddGRNError"] = "Thêm phiếu nhập thất bại";
                    return RedirectToAction("ThemPhieuNhap");
                }
            }
            catch(Exception e)
            {
                TempData["AddGRNError"] = $"Thêm phiếu nhập thất bại + {e.Message}";
                return RedirectToAction("ThemPhieuNhap");
            }
        }

        public ActionResult ThemSanPhamPhieuNhap(FormCollection form)
        {
            string productID = form["productID"].ToString();
            decimal unitPrice = decimal.Parse(form["unitPrice"]);
            int quantity = int.Parse(form["quantity"]);
            DateTime receiptDate = DateTime.Parse(form["receiptDate"]);
            string supplierID = form["supplierID"].ToString();
            string employeeName = form["createdUser"].ToString();
            string grnID = form["grnID"].ToString();

            TempData["GRNID"] = grnID;
            TempData["EmployeeName"] = employeeName;
            TempData["receiptDate"] = receiptDate;
            TempData["supplierID"] = supplierID;

            try
            {
                int ret = goodsReceiptNoteService.ThemSanPhamPhieuNhap(grnID, supplierID, receiptDate, employeeName, productID, quantity, unitPrice);
                if (ret != 0)
                {
                    TempData["AddItemSuccess"] = "Thêm sản phẩm phiếu nhập thành công";
                    return RedirectToAction("ThemPhieuNhap");
                }
                else
                {
                    TempData["AddItemError"] = "Thêm sản phẩm phiếu nhập thất bại";
                    return RedirectToAction("ThemPhieuNhap");
                }
            }
            catch(Exception e)
            {
                TempData["AddItemError"] = $"Thêm sản phẩm phiếu nhập thất bại {e.Message}";
                return RedirectToAction("ThemPhieuNhap");
            }
        }



    }
}