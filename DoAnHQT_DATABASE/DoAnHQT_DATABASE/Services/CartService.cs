using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using DoAnHQT_DATABASE.Models;
using System.Configuration;

namespace DoAnHQT_DATABASE.Services
{
    public class CartService
    {
        string connectionString = ConfigurationManager.ConnectionStrings["QL_BANHANG_ONLINE"].ToString();

        public int ThemGioHang(string ShoppingCartID, string UserID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("P_Add_ShoppingCart", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ShoppingCartID", ShoppingCartID);
                    cmd.Parameters.AddWithValue("@UserID", UserID);

                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public int XoaGioHang(string ShoppingCartID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("P_Delete_ShoppingCart", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ShoppingCartID", ShoppingCartID);
                    
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public int ThemSanPhamVaoGio(string ShoppingCartID,
	                                string ProductID ,
	                                int Quantity)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("P_Add_ShoppingCartItem", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ShoppingCartID", ShoppingCartID);
                    cmd.Parameters.AddWithValue("@ProductID", ProductID);
                    cmd.Parameters.AddWithValue("@Quantity", Quantity);

                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public int SuaSanPhamTrongGio(string ShoppingCartID,
                                    string ProductID,
                                    int Quantity)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("P_Update_ShoppingCartItem", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ShoppingCartID", ShoppingCartID);
                    cmd.Parameters.AddWithValue("@ProductID", ProductID);
                    cmd.Parameters.AddWithValue("@Quantity", Quantity);

                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public int XoaSanPhamTrongGio(string ShoppingCartID,
                                    string ProductID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("P_Delete_ShoppingCartItem", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ShoppingCartID", ShoppingCartID);
                    cmd.Parameters.AddWithValue("@ProductID", ProductID);
                    
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        

    }
}