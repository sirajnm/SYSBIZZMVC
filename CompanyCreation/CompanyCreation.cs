using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Sdk.Sfc;
using Microsoft.SqlServer.Management.Common;
using System.IO;
using System.Security.Cryptography;

namespace Sys_Sols_Inventory.CompanyCreation
{
    public class CompanyCreation
     {
        private static string ipAddress;
        private static string companyName;
        public static bool packageType=false;
        public static string dayremain;
        string DataBaseName;
      public static SqlConnection conn = null;
      static SqlCommand cmd = new SqlCommand();
        public static string CompanyName
        {
            get { return CompanyCreation.companyName; }
            set { CompanyCreation.companyName = value; }
        }
        public static string IPAddress
        {
            get
            {
                return ipAddress;
            }
            set
            {
                ipAddress = value;
            }
        }
        public static string ServerInstance()
        {
            CompanyCreation db = new CompanyCreation();
            string serverName = null;
            string ConnStr = null;
            RegistryKey rk = null;

            if (IPAddress != null)
            {
                //rk =RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine,db.IPAddress).OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SQL Server");
                if (IPAddress != null && CompanyName == null)
                {
                    return "Data Source=" + IPAddress + ";Initial Catalog=master;User ID=sa;Password=12345;MultipleActiveResultSets=true";
                }
                else if (IPAddress != null && CompanyName != null)
                {
                    return "Data Source=" + IPAddress + ";Initial Catalog=" + CompanyName + ";User ID=sa;Password=12345;MultipleActiveResultSets=true";
                }

            }
            else
            {
                rk = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SQL Server");
            }

            // RegistryKey rk = localMachine.OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SQL Server");
            String[] instances = (String[])rk.GetValue("InstalledInstances");
            int i = 0;
            if (instances.Length > 0)
            {
                while (ConnStr == null)
                {
                    foreach (String element in instances)
                    {
                        if (i == 0)
                        {
                            if (element == "MSSQLSERVER")
                            {
                                if (CompanyName == null)
                                {

                                    ConnStr = "Data Source=" + Environment.MachineName + ";Integrated Security=True;";
                                    if (db.MastDBCheck(ConnStr))
                                    {
                                        return Environment.MachineName;
                                    }

                                }
                                else
                                {
                                    if (db.CheckDB(Environment.MachineName))
                                    {
                                        // ConnStr = "Data Source=" + Environment.MachineName + ";Initial Catalog=" + db.CompanyName + ";Integrated Security=True";
                                        return Environment.MachineName;
                                    }
                                }



                            }
                            else
                            {
                                serverName = Environment.MachineName + @"\" + element;
                                if (CompanyName == null)
                                {

                                    ConnStr = "Data Source=" + serverName + ";Integrated Security=True";
                                    if (db.MastDBCheck(ConnStr))
                                    {
                                        return serverName;
                                    }

                                }
                                else
                                {

                                    if (db.CheckDB(serverName))
                                    {
                                        //ConnStr = "Data Source=" + serverName + ";Initial Catalog=" + db.CompanyName + ";Integrated Security=True";
                                        return serverName;
                                    }

                                }


                            }
                        }
                        else
                        {

                        }

                    }
                    i++;

                }

            }
            return serverName;
        }
        private bool MastDBCheck(string constring)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = constring;
                con.Open();
                DataTable dt = con.GetSchema("Databases");


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["database_name"].ToString() != "master" && dt.Rows[i]["database_name"].ToString() != "model" && dt.Rows[i]["database_name"].ToString() != "msdb" && dt.Rows[i]["database_name"].ToString() != "tempdb")
                    {
                        return true;
                    }

                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private bool CheckDB(string ServerName)
        {
            try
            {
                System.Data.SqlClient.SqlConnection SqlCon = new System.Data.SqlClient.SqlConnection("Data Source=" + ServerName + ";Integrated Security=True");
                SqlCon.Open();
                System.Data.SqlClient.SqlCommand SqlCom = new System.Data.SqlClient.SqlCommand();
                SqlCom.Connection = SqlCon;

                SqlCom.CommandText = "select name from sysdatabases where name='" + DataBaseName + "'";
                SqlDataAdapter sda = new SqlDataAdapter(SqlCom);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public string GetServer_Instance()
        {
            string serverName = null;
            string ConnStr = null;



            RegistryKey rk = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SQL Server");
            // RegistryKey rk = localMachine.OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SQL Server");
            String[] instances = (String[])rk.GetValue("InstalledInstances");
            int i = 0;
            if (instances.Length > 0)
            {
                while (ConnStr == null)
                {
                    foreach (String element in instances)
                    {
                       // if (i == 0)
                        //{
                            if (element == "MSSQLSERVER")
                            {
                                serverName = Environment.MachineName;
                                if (CheckDB(serverName))
                                {
                                    ConnStr = "Data Source=" + serverName + ";Initial Catalog=" + DataBaseName + ";Integrated Security=True";
                                    // CreateTable(ConnStr);
                                    return ConnStr;
                                }
                            }
                            else
                            {
                                serverName = Environment.MachineName + @"\" + element;
                                if (CheckDB(serverName))
                                {
                                    ConnStr = "Data Source=" + serverName + ";Initial Catalog=" + DataBaseName + ";Integrated Security=True";
                                    // CreateTable(ConnStr);
                                    return ConnStr;
                                }
                            }
                       // }
                        //else
                        //{
                        //    ConnStr = CreateDb(serverName);
                        //    // CreateTable(ConnStr);
                        //    break;
                        //}

                    }


                    //i++;
                    ConnStr = CreateDb(serverName);
                }

            }
            return ConnStr;
        }
        private string CreateDb(string ServerName)
        {
           
            /* System.Data.SqlClient.SqlConnection SqlCon = new System.Data.SqlClient.SqlConnection("Data Source=" + ServerName + ";Integrated Security=True");
             SqlCon.Open();
             SqlCommand cmd = new SqlCommand();
             //string filename = Application.StartupPath + "\\MobileRechargeSystem.mdf";
             //string logfile = Application.StartupPath + "\\MobileRechargeSystem_1.ldf";
             cmd.Connection = SqlCon;
             cmd.CommandText = "if not exists(select name from sysdatabases where name='" + DataBaseName + "')CREATE DATABASE simpleaccounting ";


             cmd.ExecuteNonQuery();
             string SqlConStr = "Data Source=" + ServerName + ";Initial Catalog=" + DataBaseName + ";Integrated Security=True";

             cmd.Dispose();*/
            string SqlConStr = "Data Source=" + ServerName + ";Integrated Security=True";
            return SqlConStr;
            // conn.Dispose();

        }
        public static void RestoreDB(string backUpFilePath, string databaseName,string server)
        {
            conn = new SqlConnection(server);
          
            cmd.Connection = conn;
            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Open();
                string query = @"  RESTORE DATABASE [" + databaseName + "] FROM  DISK ='" + backUpFilePath + "' WITH  FILE = 1,  MOVE N'AIN_INVENTORY' TO '" + Application.StartupPath + @"\Companies\" + databaseName + ".mdf" + "',  MOVE 'AIN_INVENTORY_log' TO '" + Application.StartupPath + @"\Companies\" + databaseName + "_log.ldf" +"',NOUNLOAD,  STATS = 5 ";
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
               
                MessageBox.Show(ex.Message);
            }
            
        }
        public static SqlConnection attachDB(string mdf,string ldf, string databaseName, string server)
        {
            conn = new SqlConnection(server);
            SqlConnection con = null;
            cmd.Connection = conn;
            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Open();
                string query = @" CREATE DATABASE "+databaseName+"    ON (FILENAME = '"+mdf+ "'),    (FILENAME ='" + ldf + "')    FOR ATTACH";
                cmd.CommandText = query;
                try
                {
                int result= cmd.ExecuteNonQuery();
                conn.Close();
                if (result==-1)
                {
                    SqlConnectionStringBuilder cr = new SqlConnectionStringBuilder(server);
                    cr.InitialCatalog = databaseName;
                  con  = new SqlConnection(cr.ToString());
                    try
                    {
                        con.Open();
                        return con;
                    }
                    catch 
                    {

                        MessageBox.Show("Old demo Company is not valid please clear Companies folder");
                        Application.Exit();
                    }
                }
                }
                catch
                {
                    MessageBox.Show("Old demo Company is not valid please clear Companies folder");
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

                MessageBox.Show(ex.Message);
            }
            return null;
        }
        public static bool CheckDBExist(string databaseName,string server)
        {
            bool exist = false;
            conn = new SqlConnection(server);

            cmd.Connection = conn;
            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Open();
               // string query = @" SELECT name FROM master.sys.databases WHERE name = '" + databaseName + "'";
                string query = @"SELECT db.name AS DBName, type_desc AS FileType,  Physical_Name AS Location,mf.name FROM sys.master_files mf INNER JOIN  sys.databases db ON db.database_id = mf.database_id where db.name='" + databaseName + "'";
                cmd.CommandText = query;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable ds = new DataTable();
                sda.Fill(ds);
                if (ds.Rows.Count > 0)
                {
                    if (File.Exists(ds.Rows[0]["Location"].ToString()) && File.Exists(ds.Rows[0]["Location"].ToString().Substring(0, (ds.Rows[0]["Location"].ToString().LastIndexOf('.'))) + "_log.ldf"))
                    {
                        return true;
                    }
                    return false;
                }
                else
                    return false;
                exist=Convert.ToBoolean( cmd.ExecuteNonQuery());
                conn.Close();
               
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

                MessageBox.Show(ex.Message);
            }
            return exist;
        }
        public static SqlDataReader getCompanies( string server)
        {
            bool exist = false;
            conn = new SqlConnection(server);

            cmd.Connection = conn;
            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Open();
                string query = @" SELECT db.name AS DBName, type_desc AS FileType,  Physical_Name AS Location,mf.name FROM sys.master_files mf INNER JOIN  sys.databases db ON db.database_id = mf.database_id where mf.name='AIN_INVENTORY' ";
                cmd.CommandText = query;
              
              return  cmd.ExecuteReader();
               // conn.Close();

            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

                MessageBox.Show(ex.Message);
            }
            return null;
        }
        public static string checkPackageType()
        {
            RegistryKey rkSubKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\AIN", false);
            string fileValue = "";
            string regValue = "";
            if (rkSubKey == null)
            {

                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\AIN\info.dll"))
                {
                    fileValue = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\AIN\info.dll");
                    fileValue = fileValue != "" ? Class.CryptorEngine.DecryptPackage(fileValue,true) : "";
                }
                else
                {
                    if (File.Exists(Application.StartupPath + @"\info.dll"))
                    {
                        insertPackageType();
                        return checkPackageType();
                    }
                }
            }
            else
            {
                regValue = rkSubKey.GetValue("info").ToString();

                regValue = regValue != "" ? Class.CryptorEngine.DecryptPackage(regValue, true) : "";
               
            }

            if (regValue!=""||fileValue!="")
            {
                if (regValue != "")
                    return regValue;
                else
                    return fileValue; 
               
            }
            else
            {
              
            }
            return "";
        }

        public static bool checkExpiry(string ptype)
        {
            RegistryKey rkSubKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\AIN", false);
            string regcount = "";
            string filcount = "";
            if (rkSubKey == null)
            {
                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\AIN\Registry.dll"))
                {
                    filcount = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\AIN\Registry.dll");
                    filcount = filcount != "" ? Class.CryptorEngine.Decrypt(filcount, true) : "";
                }

            }
            else
            {
                regcount = rkSubKey.GetValue("Registry").ToString();
                regcount = regcount != "" ? Class.CryptorEngine.Decrypt(regcount, true) : "";
            }
           
            string regtype = "";
            string filtype = "";

            if (rkSubKey == null)
            {
                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\AIN\Type.dll"))
                {
                    filtype = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\AIN\Type.dll");
                    filtype = filtype != "" ? Class.CryptorEngine.Decrypt(filtype, true) : "";
                }

            }
            else
            {
                regtype = rkSubKey.GetValue("Type").ToString();
                regtype = regtype != "" ? Class.CryptorEngine.Decrypt(regtype, true) : "";
            }

            string regstats = "";
            string filstats = "";
            if (rkSubKey == null)
            {
                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\AIN\Status.dll"))
                {
                    filstats = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\AIN\Status.dll");
                    filstats = filstats != "" ? Class.CryptorEngine.Decrypt(filstats, true) : "";
                }

            }
            else
            {
                regstats = rkSubKey.GetValue("Status").ToString();
                regstats = regstats != "" ? Class.CryptorEngine.Decrypt(regstats, true) : "";
            }

            try
            {
                if (ptype == "Demo")
                {

                    if (regcount != "" || filcount != "")
                    {
                        double day = regcount == "" ? Convert.ToInt32(filcount) : Convert.ToInt32(regcount);
                        DateTime date = regstats == "" ? Convert.ToDateTime(filstats) : Convert.ToDateTime(regstats);
                        double dif = (DateTime.Now.Date - date.Date).TotalDays;
                        dayremain = (day - dif).ToString();
                        if ((day - dif) > 0 && dif>=0)
                        {
                            dateUpdate(date, day-dif);
                            return true;
                        }
                        else
                        {
                            //dateUpdate(date, day);
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (ptype == "Premium")
                {
                    string status = regtype == "" ? filtype : regtype;
                    double day = regcount == "" ? Convert.ToInt32(filcount) : Convert.ToInt32(regcount);
                    DateTime date = regstats == "" ? Convert.ToDateTime(filstats) : Convert.ToDateTime(regstats);
                    double dif = (DateTime.Now.Date - date.Date).TotalDays;
                    if (status == "Activated"&& (day - dif) > 0)
                    {
                        dateUpdate(date, day);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
                
            }
            catch
            {
                return false;
            }
        }

        public static void dateUpdate(DateTime date,double days)
        {
            if (date.Date <= DateTime.Now.Date)
            {
                double dif = (DateTime.Now.Date - date.Date).TotalDays;
                if (dif > 0)
                {
                    //dif = days - dif;
                    RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\AIN");
                    //storing the values
                    string enc1 = Class.CryptorEngine.Encrypt(days.ToString(), true);
                    key.SetValue("Registry", enc1);
                    key.Close();

                    string path = Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\AIN\Registry.dll";
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }

                    byte[] data = new UTF8Encoding(true).GetBytes(enc1);
                    // Create a new file 
                    if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\AIN"))
                    {
                        Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\AIN");
                    }

                    using (FileStream fs = File.Create(path))
                    {
                        // Add some text to file                    
                        fs.Write(data, 0, data.Length);
                    }
                    InsertDate(DateTime.Now);

                    path = Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\AIN\Status.dll";
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }

                    enc1 = Class.CryptorEngine.Encrypt(DateTime.Now.ToShortDateString(), true);
                    data = new UTF8Encoding(true).GetBytes(enc1);
                    // Create a new file 
                    if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\AIN"))
                    {
                        Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\AIN");
                    }

                    using (FileStream fs = File.Create(path))
                    {
                        // Add some text to file                    
                        fs.Write(data, 0, data.Length);
                    }
                }
            }
        }

        static byte[] Decrypt(byte[] data, byte[] key, byte[] iv)
        {
            using (Aes algo = Aes.Create())
            {
                using (ICryptoTransform decryptor = algo.CreateDecryptor(key, iv))
                {
                    return Crypt(data, decryptor);
                }
            }
        }

        static byte[] Crypt(byte[] data, ICryptoTransform cryptor)
        {
            var ms = new MemoryStream();
            using (Stream cs = new CryptoStream(ms, cryptor, CryptoStreamMode.Write))
            {
                cs.Write(data, 0, data.Length);
            }
            return ms.ToArray();
        }
        static byte[] Encrypt(byte[] data, byte[] key, byte[] iv)
        {
            using (Aes algo = Aes.Create())
            {
                using (ICryptoTransform encryptor = algo.CreateEncryptor(key, iv))
                {
                    return Crypt(data, encryptor);
                }
            }
        }
        public static void insertPackageType()
        {
            if (File.Exists(Application.StartupPath + @"\info.dll"))
            {
              string    fileValue = File.ReadAllText(Application.StartupPath + @"\info.dll");
                byte[] data = new UTF8Encoding(true).GetBytes(fileValue);
              


                RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\AIN");
                //storing the values  
                key.SetValue("info", fileValue);
                key.Close();

                string path = Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\AIN\info.dll";
                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                // Create a new file 
                if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\AIN"))
                {
                    Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\AIN");
                }
                using (FileStream fs = File.Create(path))
                {
                    // Add some text to file                    
                    fs.Write(data, 0, data.Length);
                }
                File.Delete(Application.StartupPath + @"\info.dll");

                Activation_Data(Class.CryptorEngine.DecryptPackage(fileValue, true));
                
            }
        }

        public static void Activation_Data(string value)
        {
            if (value == "1")
            {
                RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\AIN");
                //storing the values
                string enc1 = Class.CryptorEngine.Encrypt("30", true);
                key.SetValue("Registry", enc1);
                key.Close();

                RegistryKey key1 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\AIN");
                //storing the values
                string enc2 = Class.CryptorEngine.Encrypt("De-Activated", true);
                key1.SetValue("Type", enc2);
                key1.Close();
                FileCreation(enc1, enc2);

            }
            else if (value == "2")
            {
                RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\AIN");
                //storing the values
                string enc1 = Class.CryptorEngine.Encrypt("365", true);
                key.SetValue("Registry", enc1);
                key.Close();

                RegistryKey key1 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\AIN");
                //storing the values
                string enc2 = Class.CryptorEngine.Encrypt("De-Activated", true);
                key1.SetValue("Type", enc2);
                key1.Close();
                FileCreation(enc1, enc2);
            }
            else
            {
                MessageBox.Show("Invalid Activation Data", "Sysbizz v1.5", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            InsertDate(DateTime.Now);
        }

        public static void FileCreation(string registrty, string type)
        {
            //......................Registry:No.of Days.....................
            string path = Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\AIN\Registry.dll";
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            byte[] data = new UTF8Encoding(true).GetBytes(registrty);
            // Create a new file 
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\AIN"))
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\AIN");
            }

            using (FileStream fs = File.Create(path))
            {
                // Add some text to file                    
                fs.Write(data, 0, data.Length);
            }
            //.................Type:Activated or Not.........................
            path = Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\AIN\Type.dll";
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            data = new UTF8Encoding(true).GetBytes(type);
            // Create a new file 
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\AIN"))
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\AIN");
            }

            using (FileStream fs = File.Create(path))
            {
                // Add some text to file                    
                fs.Write(data, 0, data.Length);
            }
            //.................Status:Date Last Used................
            path = Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\AIN\Status.dll";
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            string enc1 = Class.CryptorEngine.Encrypt(DateTime.Now.ToShortDateString(), true);
            data = new UTF8Encoding(true).GetBytes(enc1);
            // Create a new file 
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\AIN"))
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\AIN");
            }

            using (FileStream fs = File.Create(path))
            {
                // Add some text to file                    
                fs.Write(data, 0, data.Length);
            }
        }

        public static void InsertDate(DateTime date)
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\AIN");
            //storing the values
            string enc1 = Class.CryptorEngine.Encrypt(date.ToShortDateString(), true);
            key.SetValue("Status", enc1);
            key.Close();
        }
    }
}
