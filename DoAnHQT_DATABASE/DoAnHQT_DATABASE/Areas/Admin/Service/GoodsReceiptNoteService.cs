using DoAnHQT_DATABASE.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DoAnHQT_DATABASE.Areas.Admin.Service
{
    public class GoodsReceiptNoteService
    {
        string connectionString = ConfigurationManager.ConnectionStrings["QL_BANHANG_ONLINE"].ToString();

        public int ThemPhieuNhap(string GoodsReceiptNoteID,
                                string SupplierID,
                                DateTime ReceiptDate,
                                string CreatedUser)
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("P_Add_GoodsReceiptNote", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@GoodsReceiptNoteID", GoodsReceiptNoteID);
                    cmd.Parameters.AddWithValue("@SupplierID", SupplierID);
                    cmd.Parameters.AddWithValue("@ReceiptDate", ReceiptDate);
                    cmd.Parameters.AddWithValue("@CreatedUser", CreatedUser);

                    conn.Open();

                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public int SuaPhieuNhap(string GoodsReceiptNoteID,
                                string SupplierID,
                                DateTime ReceiptDate,
                                string UpdatedUser)
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("P_Update_GoodsReceiptNote", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@GoodsReceiptNoteID", GoodsReceiptNoteID);
                    cmd.Parameters.AddWithValue("@SupplierID", SupplierID);
                    cmd.Parameters.AddWithValue("@ReceiptDate", ReceiptDate);
                    cmd.Parameters.AddWithValue("@UpdatedUser", UpdatedUser);

                    conn.Open();

                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public int ThemSanPhamPhieuNhap(string GoodsReceiptNoteID,
                                 string SupplierID,
                                 DateTime ReceiptDate,
                                 string CreatedUser,
                                 string ProductID,
                                    int Quantity,
                                    decimal UnitPrice)
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("P_ThemPN", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@GoodsReceiptNoteID", GoodsReceiptNoteID);
                    cmd.Parameters.AddWithValue("@SupplierID", SupplierID);
                    cmd.Parameters.AddWithValue("@ReceiptDate", ReceiptDate);
                    cmd.Parameters.AddWithValue("@CreatedUser", CreatedUser);
                    cmd.Parameters.AddWithValue("@ProductID", ProductID);
                    cmd.Parameters.AddWithValue("@Quantity", Quantity);
                    cmd.Parameters.AddWithValue("@UnitPrice", UnitPrice);

                    conn.Open();

                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public int XoaPhieuNhap(string GoodsReceiptNoteID)
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("P_ReverseGoodsReceipt", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@GoodsReceiptNoteID", GoodsReceiptNoteID);
                    
                    conn.Open();

                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public int ChinhSuaSanPhamPhieuNhap(string ProductID,
	                                    string GoodsReceiptNoteID,
	                                    decimal UnitPrice,
                                        int Quantity)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("P_Update_GoodsReceiptNoteDetail", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ProductID", ProductID);
                    cmd.Parameters.AddWithValue("@GoodsReceiptNoteID", GoodsReceiptNoteID);
                    cmd.Parameters.AddWithValue("@UnitPrice", UnitPrice);
                    cmd.Parameters.AddWithValue("@Quantity", Quantity);

                    conn.Open();

                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public int XoaSuaSanPhamPhieuNhap(string ProductID,
                                        string GoodsReceiptNoteID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("P_Delete_GoodsReceiptNoteDetail", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ProductID", ProductID);
                    cmd.Parameters.AddWithValue("@GoodsReceiptNoteID", GoodsReceiptNoteID);
                   
                    conn.Open();

                    return cmd.ExecuteNonQuery();
                }
            }
        }




    }
}