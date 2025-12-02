using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DoAnHQT_DATABASE.Areas.Admin.Service
{
    public class BackupService
    {
        string connectionString = $"Data Source=LAPTOP-6U5MG7PD\\SQLEXPRESS;Initial Catalog=master;User ID=sa;Password=123;";

        public int BackUpFull(string DatabaseName, string BackupDirectory)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("P_PerformFullBackup", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DatabaseName", DatabaseName);
                    cmd.Parameters.AddWithValue("@BackupDirectory", BackupDirectory);

                    conn.Open();

                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public int BackUpDifferential(string DatabaseName, string BackupDirectory)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("P_PerformDifferentialBackup", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DatabaseName", DatabaseName);
                    cmd.Parameters.AddWithValue("@BackupDirectory", BackupDirectory);

                    conn.Open();

                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public int BackUpLog(string DatabaseName, string BackupDirectory)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("P_PerformLogBackup", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DatabaseName", DatabaseName);
                    cmd.Parameters.AddWithValue("@BackupDirectory", BackupDirectory);

                    conn.Open();

                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public int RestoreDataBase(string DatabaseName, string BackupPath, int Option, int FileNumber)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("P_PerformRestore", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DatabaseName", DatabaseName);
                    cmd.Parameters.AddWithValue("@BackupPath", BackupPath);
                    cmd.Parameters.AddWithValue("@Option", Option);
                    cmd.Parameters.AddWithValue("@FileNumber", FileNumber);

                    conn.Open();

                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public int RestoreLog(string DatabaseName, string BackupPath, int Option, int NumberFile)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("P_PerformLogRestore", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DatabaseName", DatabaseName);
                    cmd.Parameters.AddWithValue("@BackupPath", BackupPath);
                    cmd.Parameters.AddWithValue("@Option", Option);
                    cmd.Parameters.AddWithValue("@NumberFile", NumberFile);

                    conn.Open();

                    return cmd.ExecuteNonQuery();
                }
            }
        }
        



    }
}