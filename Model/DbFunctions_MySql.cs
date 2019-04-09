using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows;

namespace Sys_Sols_Inventory.Model
{
    public static class DbFunctionsMySQL
    {
        static MySqlConnectionStringBuilder connection = new MySqlConnectionStringBuilder { Server = @"www.sysbizz.in", UserID = @"sysbizz_test", Password = @"K[)v@l[yjv1N", Database = @"sysbizz_Activation" };


        // public static string connection = "server=192.168.1.107;user=root;database=test;password=;";
        //static SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        public static MySqlConnection conn = new MySqlConnection(connection.ToString()); 
        
        static MySqlCommand cmd = new MySqlCommand();
        static MySqlDataAdapter da = new MySqlDataAdapter();
        static MySqlDataReader dr;
        static DataTable dt = new DataTable();
       // conn.ConnectionString = Properties.Settings.Default.ConnectionStrings;
        public static MySqlConnection GetConnection()
        {            
           // conn = new SqlConnection(connection);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            return conn;
        }

        public static void CloseConnection()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        public static int InsertUpdate(string Command)
        {
            try
            {
                CloseConnection();
                conn = GetConnection();
                cmd.Connection = conn;
                cmd.CommandText = Command;
                cmd.Parameters.Clear();
                int rows = cmd.ExecuteNonQuery();
                CloseConnection();
                return rows;
            }
            catch
            {
                CloseConnection();
                return -1;
            }
        }

        public static int InsertUpdate(string Command, Dictionary<string, object> data)
        {
            try
            {
                CloseConnection();
                conn = GetConnection();
                cmd.Connection = conn;
                cmd.CommandText = Command;
                cmd.Parameters.Clear();
                foreach (KeyValuePair<string, object> ardata in data)
                {
                    cmd.Parameters.Add(new SqlParameter(ardata.Key, ardata.Value));
                }
                int rows = cmd.ExecuteNonQuery();
                CloseConnection();
                return rows;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error:Insert Update- " + ex);
                CloseConnection();
                return -1;
            }
        }

        public static int InsertUpdateProcedure(string procedureName, Dictionary<string, object> data)
        {
            try
            {
                CloseConnection();
                conn = GetConnection();
                cmd.Connection = conn;
                cmd.CommandText = procedureName;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                foreach (KeyValuePair<string, object> ardata in data)
                {
                    cmd.Parameters.Add(new SqlParameter(ardata.Key, ardata.Value));
                }
                int rows = cmd.ExecuteNonQuery();
                cmd.CommandText = "";
                cmd.CommandType = CommandType.Text;
                CloseConnection();
                return rows;
            }
            catch (Exception ex)
            {
               // MessageBox.Show("Error:Insert Update- " + ex);
                CloseConnection();
                return -1;
            }
        }
        public static DataTable GetDataTable(string Command)
        {
            try
            {
                CloseConnection();
                dt = new DataTable();
                conn = GetConnection();
                cmd.Connection = conn;
                cmd.CommandText = Command;
                cmd.Parameters.Clear();
                da.SelectCommand = cmd;
                da.Fill(dt);
                CloseConnection();
                return dt;
            }
            catch(Exception ex)
            {
               // MessageBox.Show("Error:Get DataTable- " + ex);
                CloseConnection();                
                return dt;
            }
        }

        public static MySqlDataReader GetDataReader(string Command)
        {
            try
            {
                CloseConnection();
                conn = GetConnection();
                cmd.Connection = conn;
                cmd.CommandText = Command;
                cmd.Parameters.Clear();
                dr = cmd.ExecuteReader();
                //CloseConnection();
                return dr;
            }
            catch (Exception ex)
            {
               // MessageBox.Show("Error:Get DataReader- " + ex);
                //CloseConnection();
                return dr;
            }
        }

        public static MySqlDataReader GetDataReaderProcedure(string Command)
        {
            try
            {
                CloseConnection();
                conn = GetConnection();
                cmd.Connection = conn;
                cmd.CommandText = Command;
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.StoredProcedure;
                dr = cmd.ExecuteReader();
                cmd.CommandType = CommandType.Text;
                //CloseConnection();
                return dr;
            }
            catch(Exception ex)
            {
                //MessageBox.Show("Error:Get DataReader- " + ex);
                //CloseConnection();
                return dr;
            }
        }

        public static MySqlDataReader GetDataReader(string Command, Dictionary<string, object> data)
        {
            try
            {
                CloseConnection();
                conn = GetConnection();
                cmd.Connection = conn;
                cmd.CommandText = Command;
                cmd.Parameters.Clear();
                foreach (KeyValuePair<string, object> ardata in data)
                {
                    cmd.Parameters.Add(new SqlParameter(ardata.Key, ardata.Value));
                }
                dr = cmd.ExecuteReader();
                //CloseConnection();
                return dr;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error:Get DataReader- " + ex);
                //CloseConnection();
                return dr;
            }
        }

        public static MySqlDataReader GetDataReaderProcedure(string Command, Dictionary<string, object> data)
        {
            try
            {
                CloseConnection();
                conn = GetConnection();
                cmd.Connection = conn;
                cmd.CommandText = Command;
                cmd.Parameters.Clear();
                foreach (KeyValuePair<string, object> ardata in data)
                {
                    cmd.Parameters.Add(new SqlParameter(ardata.Key, ardata.Value));
                }
                cmd.CommandType = CommandType.StoredProcedure;
                dr = cmd.ExecuteReader();
                cmd.CommandType = CommandType.Text;
                //CloseConnection();
                return dr;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error:Get DataReader- " + ex);
                //CloseConnection();
                return dr;
            }
        }
        public static DataTable GetDataTable(string Command, Dictionary<string, object> data)
        {
            try
            {
                CloseConnection();
                dt = new DataTable();
                conn = GetConnection();
                cmd.Connection = conn;
                cmd.CommandText = Command;
                cmd.Parameters.Clear();
                foreach (KeyValuePair<string, object> ardata in data)
                {
                    cmd.Parameters.Add(new SqlParameter(ardata.Key, ardata.Value));
                }
                da.SelectCommand = cmd;
                da.Fill(dt);
                CloseConnection();
                return dt;
            }
            catch (Exception ex)
            {
               // MessageBox.Show("Error:GetDataTable- " + ex);
                CloseConnection();
                return dt;
            }
        }

        public static DataTable GetDataTableProcedure(string procedureName)
        {
            try
            {
                CloseConnection();
                dt = new DataTable();
                conn = GetConnection();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "";
                cmd.Parameters.Clear();
                cmd.CommandText = procedureName;
                da.SelectCommand = cmd;
                da.Fill(dt);
                CloseConnection();
                cmd.CommandType = CommandType.Text;
                return dt;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error:GetDataTable- " + ex);
                CloseConnection();
                cmd.CommandType = CommandType.Text;
                return dt;
            }
        }

        public static DataTable GetDataTableProcedure(string procedureName, Dictionary<string, object> data)
        {
            try
            {
                CloseConnection();
                dt = new DataTable();
                conn = GetConnection();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "";
                cmd.CommandText = procedureName;
                cmd.Parameters.Clear();
                foreach (KeyValuePair<string, object> ardata in data)
                {
                    cmd.Parameters.Add(new SqlParameter(ardata.Key, ardata.Value));
                }
                da.SelectCommand = cmd;
                da.Fill(dt);
                CloseConnection();
                cmd.CommandType = CommandType.Text;
                return dt;
            }
            catch (Exception ex)
            {
               // MessageBox.Show("Error:GetDataTable- " + ex);
                CloseConnection();
                cmd.CommandType = CommandType.Text;
                return dt;
            }
        }

        public static object GetAValue(string Command)
        {
            object var = "";
            try
            {
                CloseConnection();
                conn = GetConnection();
                cmd.Connection = conn;
                cmd.CommandText = Command;
                cmd.Parameters.Clear();
                var = (cmd.ExecuteScalar());
                CloseConnection();
                return var;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error:GetAValue- " + ex);
                CloseConnection();
                var = 0;
                return var;
            }
        }

        public static object GetAValue(string Command, Dictionary<string, object> data)
        {
            object var = "";
            try
            {
                CloseConnection();
                conn = GetConnection();
                cmd.Connection = conn;
                cmd.CommandText = Command;
                cmd.Parameters.Clear();
                foreach (KeyValuePair<string, object> ardata in data)
                {
                    cmd.Parameters.Add(new SqlParameter(ardata.Key, ardata.Value));
                }
                var = (cmd.ExecuteScalar());
                CloseConnection();
                return var;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error:GetAValue- " + ex);
                CloseConnection();
                return var;
            }
        }

        public static object GetAValueProcedure(string Command, Dictionary<string, object> data)
        {
            object var = "";
            try
            {
                CloseConnection();
                conn = GetConnection();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = Command;
                cmd.Parameters.Clear();
                foreach (KeyValuePair<string, object> ardata in data)
                {
                    cmd.Parameters.Add(new SqlParameter(ardata.Key, ardata.Value));
                }
                var = (cmd.ExecuteScalar());
                CloseConnection();
                cmd.CommandType = CommandType.Text;
                return var;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error:GetAValue- " + ex);
                CloseConnection();
                cmd.CommandType = CommandType.Text;
                return var;
            }
        }
        public static object GetAValueProcedure(string Command)
        {
            object var = "";
            try
            {
                CloseConnection();
                conn = GetConnection();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = Command;
                cmd.Parameters.Clear();
               
                var = (cmd.ExecuteScalar());
                CloseConnection();
                cmd.CommandType = CommandType.Text;
                return var;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error:GetAValue- " + ex);
                CloseConnection();
                cmd.CommandType = CommandType.Text;
                return var;
            }
        }
    }
}
