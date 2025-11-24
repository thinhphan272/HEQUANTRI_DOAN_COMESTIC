using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DoAnHQT_DATABASE.Areas.Admin.Service
{
    public class AccountService
    {

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
    }
}