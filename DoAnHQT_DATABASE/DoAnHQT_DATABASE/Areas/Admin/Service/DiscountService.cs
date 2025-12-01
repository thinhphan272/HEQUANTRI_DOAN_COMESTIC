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
    public class DiscountService
    {
        string connectionString = ConfigurationManager.ConnectionStrings["QL_BANHANG_ONLINE"].ToString();

        public int ThemDiscount(string DiscountID,
	                            string ProductID,
	                            string DiscountName,
	                            DateTime StartDate,
                                DateTime EndDate,
	                            double DiscountRate,
	                            string CreatedUser)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("P_Add_Discount", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DiscountID", DiscountID);
                    cmd.Parameters.AddWithValue("@ProductID", ProductID);
                    cmd.Parameters.AddWithValue("@DiscountName", DiscountName);
                    cmd.Parameters.AddWithValue("@StartDate", StartDate);
                    cmd.Parameters.AddWithValue("@DiscountRate", DiscountRate);
                    cmd.Parameters.AddWithValue("@CreatedUser", CreatedUser);
                    cmd.Parameters.AddWithValue("@EndDate", EndDate);

                    conn.Open();

                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public int SuaDiscount(string DiscountID,
                                string ProductID,
                                string DiscountName,
                                DateTime StartDate,
                                DateTime EndDate,
                                double DiscountRate,
                                string UpdatedUser)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("P_Update_Discount", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DiscountID", DiscountID);
                    cmd.Parameters.AddWithValue("@ProductID", ProductID);
                    cmd.Parameters.AddWithValue("@DiscountName", DiscountName);
                    cmd.Parameters.AddWithValue("@StartDate", StartDate);
                    cmd.Parameters.AddWithValue("@DiscountRate", DiscountRate);
                    cmd.Parameters.AddWithValue("@UpdatedUser", UpdatedUser);
                    cmd.Parameters.AddWithValue("@EndDate", EndDate);

                    conn.Open();

                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public int XoaDiscount(string DiscountID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("P_Delete_Discount", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DiscountID", DiscountID);
                    
                    conn.Open();

                    return cmd.ExecuteNonQuery();
                }
            }
        }



    }
}