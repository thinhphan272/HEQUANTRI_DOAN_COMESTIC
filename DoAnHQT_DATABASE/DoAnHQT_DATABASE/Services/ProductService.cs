using DoAnHQT_DATABASE.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace DoAnHQT_DATABASE.Services
{
    public class ProductService
    {
        string connectionString = ConfigurationManager.ConnectionStrings["QL_BANHANG_ONLINE"].ToString();
        QL_BANHANG_ONLINE db = new QL_BANHANG_ONLINE();
        public List<Product> SanPhamGiamSauNhat(int sl)
        {
            List<Product> list = new List<Product>();
            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                string sqlCmd = "SELECT * FROM dbo.F_LaySanPhamGiamSauNhat(@SL)";
                using (SqlCommand cmd = new SqlCommand(sqlCmd, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@SL", sl);

                    conn.Open();
                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Product sp = new Product();
                            sp.ProductID = reader["ProductID"].ToString();
                            sp.ProductName = reader["ProductName"].ToString();
                            sp.Image = reader["Image"].ToString();
                            sp.Price = (decimal)reader["Price"];
                            sp.DiscountRate = (double)reader["DiscountRate"];
                            sp.GiaDaGiam = (double)reader["GiaDaGiam"];
                            

                            list.Add(sp);
                        }
                    }
                }
            }
            return list;
        }

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
                    cmd.Parameters.AddWithValue("@CreatedUser", UpdatedUser);

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

        public List<Product> TimKiemTheoTen(string TenSP)
        {
            List<Product> list = new List<Product>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sqlCmd = "SELECT * FROM dbo.F_TimKiemSanPhamTheoTen(@TenSP)";
                using (SqlCommand cmd = new SqlCommand(sqlCmd, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@TenSP", TenSP);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Product sp = new Product();
                            sp.ProductID = reader["ProductID"].ToString();
                            sp.ProductName = reader["ProductName"].ToString();
                            sp.ProductTypeID = reader["ProductTypeID"].ToString();
                            sp.Image = reader["Image"].ToString();
                            sp.Price = (decimal)reader["Price"];
                            sp.BrandID = reader["BrandID"].ToString();
                            sp.Brand = db.Brand.FirstOrDefault(t => t.BrandID.Equals(sp.BrandID));
                            sp.Discount = db.Discount.ToList().FindAll(t => t.ProductID.Equals(sp.ProductID));
                            sp.GoodsReceiptNoteDetail = db.GoodsReceiptNoteDetail.ToList().FindAll(t => t.ProductID.Equals(sp.ProductID));
                            sp.OrderDetail = db.OrderDetail.ToList().FindAll(t => t.Equals(sp.ProductID));
                            sp.ProductType = db.ProductType.FirstOrDefault(t => t.ProductTypeID.Equals(sp.ProductTypeID));
                            sp.Rating = db.Rating.ToList().FindAll(t => t.ProductID.Equals(sp.ProductID));
                            sp.ShoppingCartItem = db.ShoppingCartItem.ToList().FindAll(t => t.ProductID.Equals(sp.ProductID));

                            list.Add(sp);
                        }
                    }
                }
            }
            return list;
        }

        public List<Product> LocSanPham(string element, string order)
        {
            List<Product> list = new List<Product>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sqlCmd = $"SELECT * FROM dbo.F_LocSanPham() ORDER BY {element} {order}";
                using (SqlCommand cmd = new SqlCommand(sqlCmd, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Product sp = new Product();
                            sp.ProductID = reader["ProductID"].ToString();
                            sp.ProductName = reader["ProductName"].ToString();
                            sp.ProductTypeID = reader["ProductTypeID"].ToString();
                            sp.Image = reader["Image"].ToString();
                            sp.Price = (decimal)reader["Price"];
                            sp.DiscountRate = (double)reader["DiscountRate"] ;
                            sp.GiaDaGiam = (double)reader["GiaDaGiam"];
                            sp.BrandID = reader["BrandID"].ToString();
                            sp.Brand = db.Brand.FirstOrDefault(t => t.BrandID.Equals(sp.BrandID));
                            sp.Discount = db.Discount.ToList().FindAll(t => t.ProductID.Equals(sp.ProductID));
                            sp.GoodsReceiptNoteDetail = db.GoodsReceiptNoteDetail.ToList().FindAll(t => t.ProductID.Equals(sp.ProductID));
                            sp.OrderDetail = db.OrderDetail.ToList().FindAll(t => t.Equals(sp.ProductID));
                            sp.ProductType = db.ProductType.FirstOrDefault(t => t.ProductTypeID.Equals(sp.ProductTypeID));
                            sp.Rating = db.Rating.ToList().FindAll(t => t.ProductID.Equals(sp.ProductID));
                            sp.ShoppingCartItem = db.ShoppingCartItem.ToList().FindAll(t => t.ProductID.Equals(sp.ProductID));

                            list.Add(sp);
                        }
                    }
                }
            }
            return list;
        }



    }
}