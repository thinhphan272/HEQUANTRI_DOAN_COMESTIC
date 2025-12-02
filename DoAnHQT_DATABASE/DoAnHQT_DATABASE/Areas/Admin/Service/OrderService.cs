using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DoAnHQT_DATABASE.Areas.Admin.Service
{
    public class OrderService
    {
        string connectionString = ConfigurationManager.ConnectionStrings["QL_BANHANG_ONLINE"].ToString();

        public int CancelDonHang(string OrderID, string UpdatedUser)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("P_Cancel_Order", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@OrderID", OrderID);
                    cmd.Parameters.AddWithValue("@UpdatedUser", UpdatedUser);

                    conn.Open();

                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public double TongThanhTienDonHang(string OrderID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Select dbo.F_TinhTongThanhTienDonHang(@OrderID)", conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@OrderID", OrderID);

                    conn.Open();

                    object result = cmd.ExecuteScalar();

                    // Chuyển đổi kết quả sang kiểu dữ liệu mong muốn
                    if (result != null)
                    {
                        double finalValue = Convert.ToDouble(result);
                        // Xử lý giá trị finalValue
                        return finalValue;
                    }
                    
                }
            }
            return 0;
        }

        public double TongDoanhThu()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("select dbo.F_TinhTongDoanhThu()", conn))
                {
                    conn.Open();

                    object result = cmd.ExecuteScalar();

                    // Chuyển đổi kết quả sang kiểu dữ liệu mong muốn
                    if (result != null)
                    {
                        double finalValue = Convert.ToDouble(result);
                        // Xử lý giá trị finalValue
                        return finalValue;
                    }

                }
            }
            return 0;
        }

        public bool UpdateOrderStatus(string orderID, string newStatus, string updatedUser)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    // Câu lệnh SQL cập nhật trạng thái
                    string query = "UPDATE Orders SET Status = @Status, UpdatedAt = GETDATE(), UpdatedBy = @UpdatedUser WHERE OrderID = @OrderID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Status", newStatus);
                        cmd.Parameters.AddWithValue("@UpdatedUser", updatedUser);
                        cmd.Parameters.AddWithValue("@OrderID", orderID);

                        int rows = cmd.ExecuteNonQuery();
                        return rows > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Ghi log lỗi nếu cần
                return false;
            }
        }


    }
}