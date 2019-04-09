using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Sys_Sols_Inventory.Class
{
    class DatabaseSettings
    {
        public SqlConnection conn = null;
        private SqlCommand cmd = new SqlCommand();
        public void setConnectionString(String connectionString)
        {
            conn.ConnectionString = connectionString;
        }

        public void InsertConnOriginal()
        {
            //string connOriginal = Properties.Settings.Default.ConnectionStrings;

            //cmd.Parameters.Clear();
            //cmd.CommandText = "INSERT INTO Tbl_ConnectionStrings(ConnOriginal,ConnDuplicate)VALUES(@connOriginal,'')";
            //cmd.Parameters.AddWithValue("@connOriginal", connOriginal);
            //conn.Open();
            //cmd.Connection = conn;
            //cmd.ExecuteNonQuery();
            //conn.Close();

            //SClass sc = new SClass();
            //sc.updateOgXml(connOriginal);
        }
        public void UpdateConnOriginal()
        {
            //try
            //{
            //    string connOriginal = Properties.Settings.Default.ConnectionStrings;
            //    //conn.ConnectionString = connOriginal;
            //    cmd.Parameters.Clear();
            //    cmd.CommandText = "UPDATE Tbl_ConnectionStrings SET ConnOriginal = @connOriginal";
            //    cmd.Parameters.AddWithValue("@connOriginal", connOriginal);
            //    conn.Open();
            //    cmd.Connection = conn;
            //    cmd.ExecuteNonQuery();
            //    conn.Close();

            //    SClass sc = new SClass();
            //    sc.updateOgXml(connOriginal);
            //}
            //catch
            //{
            //    string connOriginal = Properties.Settings.Default.ConnectionStrings;
            //    conn.ConnectionString = connOriginal;
            //    cmd.Parameters.Clear();
            //    cmd.CommandText = "UPDATE Tbl_ConnectionStrings SET ConnOriginal = @connOriginal";
            //    cmd.Parameters.AddWithValue("@connOriginal", connOriginal);
            //    conn.Open();
            //    cmd.Connection = conn;
            //    cmd.ExecuteNonQuery();
            //    conn.Close();

            //    SClass sc = new SClass();
            //    sc.updateOgXml(connOriginal);
            //}
        }
        //public void InsertConnDuplicate()
        //{
        //    string connDuplicate = Properties.Settings.Default.ConnectionStrings;
        //    cmd.Parameters.Clear();
        //    cmd.CommandText = "INSERT INTO Tbl_ConnectionStrings(ConnDuplicate)VALUES(@connDuplicate)";
        //    cmd.Parameters.AddWithValue("@connDuplicate", connDuplicate);
        //    conn.Open();
        //    cmd.Connection = conn;
        //    cmd.ExecuteNonQuery();
        //    conn.Close();
        //}
        public void UpdateConnDuplicate()
        {
            //try
            //{
            //    string connDuplicate = Properties.Settings.Default.ConnectionStrings;
            //    //conn.ConnectionString = connDuplicate;
            //    cmd.Parameters.Clear();
            //    cmd.CommandText = "UPDATE Tbl_ConnectionStrings SET ConnDuplicate = @connDuplicate";
            //    cmd.Parameters.AddWithValue("@connDuplicate", connDuplicate);
            //    conn.Open();
            //    cmd.Connection = conn;
            //    cmd.ExecuteNonQuery();
            //    conn.Close();
            //}
            //catch
            //{
            //    string connDuplicate = Properties.Settings.Default.ConnectionStrings;
            //    conn.ConnectionString = connDuplicate;
            //    cmd.Parameters.Clear();
            //    cmd.CommandText = "UPDATE Tbl_ConnectionStrings SET ConnDuplicate = @connDuplicate";
            //    cmd.Parameters.AddWithValue("@connDuplicate", connDuplicate);
            //    conn.Open();
            //    cmd.Connection = conn;
            //    cmd.ExecuteNonQuery();
            //    conn.Close();
            //}
        }
        public bool HasConnOriginal()
        {
        //    cmd.CommandText = "SELECT ConnOriginal FROM Tbl_ConnectionStrings";
        //    cmd.Connection = conn;
        //    conn.Open();
        //    string hasconnOriginal = Convert.ToString(cmd.ExecuteScalar());
        //    conn.Close();
        //    if (string.IsNullOrEmpty(hasconnOriginal))
        //    {
        //        return false;
        //    }
        //    else
                return true;
        }
        public bool HasConnDuplicate()
        {
        //    cmd.CommandText = "SELECT ConnDuplicate FROM Tbl_ConnectionStrings";
        //    conn.Open();
        //    cmd.Connection = conn;
        //    string hasconnDuplicate = Convert.ToString(cmd.ExecuteScalar());
        //    conn.Close();
        //    if (hasconnDuplicate == string.Empty)
        //    {
        //        return false;
        //    }
        //    else
               return true;
        }
        public string ConnOriginal()
        {
            cmd.CommandText = "SELECT ConnOriginal FROM Tbl_ConnectionStrings";
            conn.Open();
            cmd.Connection = conn;
            string connOriginal = Convert.ToString(cmd.ExecuteScalar());
            conn.Close();
            return connOriginal;
        }
        public string ConnDuplicate()
        {
            cmd.CommandText = "SELECT ConnDuplicate FROM Tbl_ConnectionStrings";
            conn.Open();
            cmd.Connection = conn;
            string connDuplicate = Convert.ToString(cmd.ExecuteScalar());
            conn.Close();
            return connDuplicate;
        }
    }
}
