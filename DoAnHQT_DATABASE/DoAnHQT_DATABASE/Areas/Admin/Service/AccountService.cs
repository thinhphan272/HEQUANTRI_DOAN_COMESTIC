using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DoAnHQT_DATABASE.Areas.Admin.Service
{
    public class AccountService
    {
        string connectionString = ConfigurationManager.ConnectionStrings["QL_BANHANG_ONLINE"].ToString();
        public bool Connect(string username, string password, string role)
        {
            string connStr = $"Data Source=localhost;Initial Catalog=QL_BANHANG_ONLINE;User ID={username};Password={password};";
            string query = $"SELECT IS_ROLEMEMBER('{role}');";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {

                        // Thực thi truy vấn và nhận kết quả (luôn là một số nguyên)
                        int result = (int)cmd.ExecuteScalar();

                        // Trả về true nếu User là thành viên của Role (result == 1)
                        return result == 1;
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi kết nối, đăng nhập sai, hoặc lỗi thực thi query
                Console.WriteLine($"Error during role check: {ex.Message}");
                return false;
            }
        }

        public List<string> GetAllStaffs()
        {
            List<string> lstNV = new List<string>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT UserName FROM dbo.F_GetAllUserInRole(@RoleName)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@RoleName", "Nhân viên");

                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Đọc giá trị của cột "UserName" và thêm vào List
                                // Đảm bảo cột UserName không bị null
                                lstNV.Add(reader["UserName"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi, ví dụ: ghi log hoặc throw lại lỗi
                lstNV.Add("N/A");
                return lstNV;
            }
            return lstNV;
        }

        public bool AddStaff(string username, string password)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string function = "P_ADD_DATABASEUSER";
                    using (SqlCommand cmd = new SqlCommand(function, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@USERNAME", username);
                        cmd.Parameters.AddWithValue("@PASSWORD", password);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }catch(Exception ex)
            {
                Console.WriteLine("Lỗi trong lúc xử lý thêm!");
                return false;
            }
        }

        public bool DeleteStaff(string username)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string function = "P_DELETE_DATABASEUSER";
                    using (SqlCommand cmd = new SqlCommand(function, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@USERNAME", username);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi trong lúc xử lý xoá!");
                return false;
            }
        }


    }
}