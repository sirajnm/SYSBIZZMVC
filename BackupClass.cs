using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data;

namespace Sys_Sols_Inventory
{
    class BackupClass
    {
        public static void Restore(String datasource, String database, String path)
        {
            SqlConnection restoreConn = new SqlConnection("Data Source=" + datasource + "; Initial Catalog=master;Integrated Security=True");
            Server server = new Server(new ServerConnection(restoreConn));
            server.KillAllProcesses(database);
            Restore restore = new Restore();
            restore.Database = database;
            restore.Action = RestoreActionType.Database;
            restore.Devices.Add(new BackupDeviceItem(path, DeviceType.File));
            restore.ReplaceDatabase = true;
            restore.SqlRestore(server);
        }

        public static void Backup(String path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            SqlConnection connection = Model.DbFunctions.GetConnection();
            try
            {
                //connection.Open();
                Server sqlServerInstance = new Server(new Microsoft.SqlServer.Management.Common.ServerConnection(connection));
                Backup objBackup = new Backup();
                objBackup.Devices.AddDevice(@path + "\\"+connection.Database+".bak", DeviceType.File);
                objBackup.Database = connection.Database;
                objBackup.Action = BackupActionType.Database;
                objBackup.SqlBackup(sqlServerInstance);
                MessageBox.Show("The backup of database " + "'" + connection.Database + "'" + " completed sccessfully.", "Microsoft SQL Server Management Studio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Model.DbFunctions.CloseConnection();
            }
            catch (SmoException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Model.DbFunctions.CloseConnection();
            }
        }

        public static void Backup1(String path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            SqlConnection connection = Model.DbFunctions.GetConnection();
            try
            {
                //connection.Open();
                //Server sqlServerInstance = new Server(new Microsoft.SqlServer.Management.Common.ServerConnection(connection));
                //Backup objBackup = new Backup();
                //objBackup.Devices.AddDevice(@path + "\\Sysbizz.bak", DeviceType.File);
                //objBackup.Database = connection.Database;
                //objBackup.Action = BackupActionType.Database;
                //objBackup.SqlBackup(sqlServerInstance);
                //MessageBox.Show("The backup of database " + "'" + connection.Database + "'" + " completed sccessfully.", "Microsoft SQL Server Management Studio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Model.DbFunctions.CloseConnection();

                //string connectionString1 = (@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|Database1.mdf;Database=Database1;Integrated Security=True; User Instance=True");
                SqlConnection cn = connection;
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = new SqlCommand();
                SqlDataReader reader;
                //string query = @"use master; BACKUP DATABASE " + connection.Database + @" TO DISK = 'C:\Sysbizz.bak'";


                string comp = connection.Database;
                try
                {
                    comp = comp.Substring(comp.LastIndexOf(@"\"));
                }
                catch { }
                comp = comp.Replace(@"\", "");
                string db = comp.Replace(".mdf", "");
                db = comp.Replace(".MDF", "");

                string query = @"use master; BACKUP DATABASE @path TO DISK = '"+path+"\\"+ db+".bak'";
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@path", connection.Database);
                //Model.DbFunctions.GetDataReader(query,parameters);
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                reader = cmd.ExecuteReader();
                cn.Close();
                MessageBox.Show("Database Backup Successfull.");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Model.DbFunctions.CloseConnection();
            }
        }

        public static void Restore1(String datasource, String database, String path)
        {
            //SqlConnection restoreConn = new SqlConnection("Data Source=" + datasource + "; Initial Catalog=master;Integrated Security=True");
            //Server server = new Server(new ServerConnection(restoreConn));
            //server.KillAllProcesses(database);
            //Restore restore = new Restore();
            //restore.Database = database;
            //restore.Action = RestoreActionType.Database;
            //restore.Devices.Add(new BackupDeviceItem(path, DeviceType.File));
            //restore.ReplaceDatabase = true;
            //restore.SqlRestore(server);

            //string connectionString1 = (@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|Database1.mdf;Database=Database1;Integrated Security=True; User Instance=True");
            try
            {
                SqlConnection cn = Model.DbFunctions.GetConnection();
                if (cn.State == ConnectionState.Closed)
                    cn.Open();

                SqlCommand cmd = new SqlCommand();
                SqlDataReader reader;
                cmd.CommandText = @"use master; RESTORE DATABASE " + cn.Database + " FROM DISK = '" + path + "'";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                reader = cmd.ExecuteReader();
                cn.Close();
               
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error in Database Restore :"+ex);
            }
        }

    }
}
