using DoAnHQT_DATABASE.Areas.Admin.Models;
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
            string connStr = $"Data Source=LAPTOP-6U5MG7PD\\SQLEXPRESS;Initial Catalog=QL_BANHANG_ONLINE;User ID={username};Password={password};";
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

        public List<StaffViewModel> GetAllStaffs()
        {
            List<StaffViewModel> lstNV = new List<StaffViewModel>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Gọi hàm SQL mới tạo ở Bước 1
                    string query = "SELECT * FROM F_GetStaffWithStatus()";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                StaffViewModel staff = new StaffViewModel();
                                staff.Username = reader["Username"].ToString();
                                // Đọc trạng thái (nếu null thì coi như không khóa)
                                staff.IsLocked = reader["IsLocked"] != DBNull.Value && (bool)reader["IsLocked"];
                                lstNV.Add(staff);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi lấy danh sách nhân viên: " + ex.Message);
            }
            return lstNV;
        }

        // 2. THÊM HÀM MỞ KHÓA (UNLOCK)
        public bool UnlockStaff(string username)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    // Câu lệnh SQL để Enable Login
                    string query = $"ALTER LOGIN [{username}] ENABLE";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch { return false; }
        }

        // 3. THÊM HÀM KHÓA (LOCK) - Tùy chọn nếu muốn dùng nút Khóa thay vì Xóa
        public bool LockStaff(string username)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = $"ALTER LOGIN [{username}] DISABLE";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch { return false; }
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