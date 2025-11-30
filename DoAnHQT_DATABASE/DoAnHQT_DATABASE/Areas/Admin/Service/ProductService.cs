using DoAnHQT_DATABASE.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.Linq;
using System.Web;

namespace DoAnHQT_DATABASE.Areas.Admin.Service
{
    public class ProductService
    {
        string connectionString = ConfigurationManager.ConnectionStrings["QL_BANHANG_ONLINE"].ToString();

        public int ThemSanPham(string ProductID,
                                string ProductTypeID,
                                string ProductName,
                                string BrandID,
                                decimal Price,
                                string Origin,
                                string Description,
                                string Image,
                                double Capacity,
                                int Quantity,
                                DateTime ExpirationDate,
                                string CreatedUser)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                using (SqlCommand cmd = new SqlCommand("P_Add_Product", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ProductID", ProductID);
                    cmd.Parameters.AddWithValue("@ProductTypeID", ProductTypeID);
                    cmd.Parameters.AddWithValue("@ProductName", ProductName);
                    cmd.Parameters.AddWithValue("@BrandID", BrandID);
                    cmd.Parameters.AddWithValue("@Price", Price);
                    cmd.Parameters.AddWithValue("@Origin", Origin);
                    cmd.Parameters.AddWithValue("@Description", Description);
                    cmd.Parameters.AddWithValue("@Image", Image);
                    cmd.Parameters.AddWithValue("@Capacity", Capacity);
                    cmd.Parameters.AddWithValue("@Quantity", Quantity);
                    cmd.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
                    cmd.Parameters.AddWithValue("@CreatedUser", CreatedUser);

                    conn.Open();

                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public int SuaSanPham(string ProductID,
                                string ProductTypeID,
                                string ProductName,
                                string BrandID,
                                decimal Price,
                                string Origin,
                                string Description,
                                string Image,
                                double Capacity,
                                int Quantity,
                                DateTime ExpirationDate,
                                string UpdatedUser)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("P_Update_Product", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ProductID", ProductID);
                    cmd.Parameters.AddWithValue("@ProductTypeID", ProductTypeID);
                    cmd.Parameters.AddWithValue("@ProductName", ProductName);
                    cmd.Parameters.AddWithValue("@BrandID", BrandID);
                    cmd.Parameters.AddWithValue("@Price", Price);
                    cmd.Parameters.AddWithValue("@Origin", Origin);
                    cmd.Parameters.AddWithValue("@Description", Description);
                    cmd.Parameters.AddWithValue("@Image", Image);
                    cmd.Parameters.AddWithValue("@Capacity", Capacity);
                    cmd.Parameters.AddWithValue("@Quantity", Quantity);
                    cmd.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
                    cmd.Parameters.AddWithValue("@UpdatedUser", UpdatedUser);

                    conn.Open();

                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public int DisableSanPham(string ProductID)
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("P_Disable_Product", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ProductID", ProductID);

                    conn.Open();

                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public int EnableSanPham(string ProductID)
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("P_Enable_Product", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ProductID", ProductID);

                    conn.Open();

                    return cmd.ExecuteNonQuery();
                }
            }
        }


        public int XoaTatCaSPTrongGio(string ProductID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("P_DeleteAllItemInCart", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ProductID", ProductID);

                    conn.Open();

                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public int CancelOrderCuaSanPham(string ProductID, string UpdatedUser)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("P_CancelOrderWhenDisable", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ProductID", ProductID);
                    cmd.Parameters.AddWithValue("@UpdatedUser", UpdatedUser);

                    conn.Open();

                    return cmd.ExecuteNonQuery();
                }
            }
        }


    }
}