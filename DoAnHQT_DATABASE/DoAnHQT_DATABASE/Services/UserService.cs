using DoAnHQT_DATABASE.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Xml.Linq;
using System.Data;
using System.Web.Mvc;

namespace DoAnHQT_DATABASE.Services
{
    public class UserService
    {
        string connectionString = ConfigurationManager.ConnectionStrings["QL_BANHANG_ONLINE"].ToString();

        public int DangKy(string UserID,
                            string Name,
                            string Email,
                            string Password,
                            string Gender,
                            string Address,
                            string CreatedUser)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("P_Add_User", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    cmd.Parameters.AddWithValue("@Name", Name);
                    cmd.Parameters.AddWithValue("@Email", Email);
                    cmd.Parameters.AddWithValue("@Password", Password);
                    cmd.Parameters.AddWithValue("@Gender", Gender);
                    cmd.Parameters.AddWithValue("@Address", Address);
                    cmd.Parameters.AddWithValue("@CreatedUser", CreatedUser);

                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public int DangNhap(string Email, string Password)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "SELECT dbo.F_LOGIN(@Email, @Password)";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", Email.Trim());
                    cmd.Parameters.AddWithValue("@Password", Password.Trim());

                    conn.Open();
                    object result =  cmd.ExecuteScalar();
                    int ret = 0;
                    if (result != null)
                    {
                        ret =  Convert.ToInt32(result);
                    }
                    return ret;
                }
            }
        }

        public int SuaUser(string UserID,
                            string Name,
                            string Email,
                            string Password,
                            string Gender,
                            string Address,
                            string UpdatedUser)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("P_Update_User", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    cmd.Parameters.AddWithValue("@Name", Name);
                    cmd.Parameters.AddWithValue("@Email", Email);
                    cmd.Parameters.AddWithValue("@Password", Password);
                    cmd.Parameters.AddWithValue("@Gender", Gender);
                    cmd.Parameters.AddWithValue("@Address", Address);
                    cmd.Parameters.AddWithValue("@CreatedUser", UpdatedUser);

                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public int DeactiveUser(string UserID,
                            string UpdatedUser)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("P_Deactive_User", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    cmd.Parameters.AddWithValue("@CreatedUser", UpdatedUser);

                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public int RestoreUser(string UserID,
                            string UpdatedUser)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("P_Restore_User", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    cmd.Parameters.AddWithValue("@CreatedUser", UpdatedUser);

                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

       
    }
}