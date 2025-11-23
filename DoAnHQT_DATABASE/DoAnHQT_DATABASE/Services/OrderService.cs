using DoAnHQT_DATABASE.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DoAnHQT_DATABASE.Services
{
    public class OrderService
    {
        string connectionString = ConfigurationManager.ConnectionStrings["QL_BANHANG_ONLINE"].ToString();

        public int SuaDonHang(string OrderID,
	                            string UserID,
	                            DateTime OrderDate,
                                string Address,
	                            string Status,
	                            string UserPaymentMethod,
	                            string UpdatedUser)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("P_Update_Order", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@OrderID", OrderID);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    cmd.Parameters.AddWithValue("@OrderDate", OrderDate);
                    cmd.Parameters.AddWithValue("@Address", Address);
                    cmd.Parameters.AddWithValue("@Status", Status);
                    cmd.Parameters.AddWithValue("@UserPaymentMethod", UserPaymentMethod);
                    cmd.Parameters.AddWithValue("@UpdatedUser", UpdatedUser);

                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public int HuyDonHang(string OrderID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("P_Delete_Order", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@OrderID", OrderID);

                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public int DatHang(string OrderID,
                            string UserID,
                            DateTime OrderDate,
                            string Address,
                            string Status,
                            string UserPaymentMethod,
                            string CreatedUser)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("P_DATHANG", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@OrderID", OrderID);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    cmd.Parameters.AddWithValue("@OrderDate", OrderDate);
                    cmd.Parameters.AddWithValue("@Address", Address);
                    cmd.Parameters.AddWithValue("@Status", Status);
                    cmd.Parameters.AddWithValue("@UserPaymentMethod", UserPaymentMethod);
                    cmd.Parameters.AddWithValue("@CreatedUser", CreatedUser);

                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }




    }
}