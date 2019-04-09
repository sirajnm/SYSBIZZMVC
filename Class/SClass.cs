using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;
using Microsoft.Win32;
using System.Configuration;
using System.IO;
using System.Xml;
using System.Xml.Linq;
namespace Sys_Sols_Inventory
{
    class GetConnection 
    {
        private SqlConnection conn = new SqlConnection("");
      //  private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        internal string GetCurrentConnctionString()
        {
            string ss = conn.ConnectionString;
            if (ss.Contains("user id=\'"))
            {
              //  ss = ss  + ";password='" + password + "'";
            }
            return ss;
        }
    }
    class SClass
    {
        public bool CheckFilesExists(string FileNameWithFullPath)
        {
            if (File.Exists(FileNameWithFullPath))
            {
                return true;
            }
            return false;
        }

        public void SaveConnectionString(string connectionstring)
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var connectionStrings = config.ConnectionStrings;
                Properties.Settings.Default.ConnectionStrings = connectionstring;
                Properties.Settings.Default.Save();
               // MessageBox.Show(Properties.Settings.Default.ConnectionStrings.ToString());
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }
        public SqlConnection Conn=null;
        internal bool CheckMsSqlConnection(string serverName, string userId, string password, string ApplicationPath)
        {

            bool isTrue = false;
            SqlConnection sqlcon;

            if (serverName != null)
            {
                if (userId == null || password == null)
                {
                    sqlcon = new SqlConnection(@"Data Source=" + serverName + ";AttachDbFilename=" + ApplicationPath + "\\Data\\SYSBIZZ.mdf" + ";Integrated Security=True;Persist Security Info=True;Connect Timeout=30;Trusted_Connection=True");
                }
                else
                {
                    sqlcon = new SqlConnection(@"Data Source=" + serverName + ";AttachDbFilename=" + ApplicationPath + "\\Data\\SYSBIZZ.mdf" + ";user id='" + userId + "';password='" + password + "';Persist Security Info=True;Connect Timeout=30;");
                }
                try
                {                   
                   // sqlcon.Open();
                  
                    SaveConnectionString(sqlcon.ConnectionString.ToString());
                    sqlcon.Open();
                    Conn = sqlcon;
                    isTrue = true;
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 18493)
                    {
                        if (userId == null || password == null)
                        {
                            sqlcon = new SqlConnection(@"Data Source=" + serverName + ";AttachDbFilename=" + ApplicationPath + "\\Data\\SYSBIZZ.mdf" + ";Integrated Security=True;Persist Security Info=True;Connect Timeout=30");
                        }
                        else
                        {
                            sqlcon = new SqlConnection(@"Data Source=" + serverName + ";AttachDbFilename=" + ApplicationPath + "\\Data\\SYSBIZZ.mdf" + ";user id='" + userId + "';password='" + password + "';Persist Security Info=True;Connect Timeout=30");
                        }
                        try
                        {
                            sqlcon.Open();
                            SaveConnectionString(sqlcon.ConnectionString.ToString());
                            UpdateAppConfig("isServerConnection", "true");
                            isTrue = true;
                        }
                        catch (SqlException exa)
                        {

                            MessageBox.Show(exa.Message);
                            SaveConnectionString(sqlcon.ConnectionString.ToString());
                            isTrue = false;

                        }
                        finally
                        {
                            sqlcon.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show(ex.Message);
                        isTrue = false;
                    }
                }
                finally
                {
                    sqlcon.Close();
                }
            }
            return isTrue;

        }
        public DataTable GetLocalInstance()
        {
            DataTable LocalInstanceNames = new DataTable();
            LocalInstanceNames.Columns.Add("Server", typeof(string));
            LocalInstanceNames.Columns.Add("Instance", typeof(string));
            RegistryView registryView = Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32;
            using (RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, registryView))
            {
                RegistryKey instanceKey = hklm.OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SQL Server\Instance Names\SQL", false);
                if (instanceKey != null)
                {
                    foreach (string instanceName in instanceKey.GetValueNames())
                    {
                        LocalInstanceNames.Rows.Add(Environment.MachineName, instanceName);
                    }
                }
                else
                {
                    LocalInstanceNames.Rows.Add(Environment.MachineName, "");
                }
            }
            return LocalInstanceNames;
        }

       

        public void UpdateAppConfig(string key, string value)
        {
            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = configFile.AppSettings.Settings;
            if (settings[key] == null)
            {
                settings.Add(key, value);
            }
            else
            {
                settings[key].Value = value;
            }
            configFile.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
        }

        internal DataTable GetOmPath(string serverName, string userId, string password)
        {
            SqlConnection sqlcon;
            DataTable dtbl;
            if (serverName != null)
            {
                if (userId == null || password == null)
                {
                    sqlcon = new SqlConnection(@"Data Source=" + serverName + ";Integrated Security=True;Connect Timeout=30");
                }
                else
                {
                    sqlcon = new SqlConnection(@"Data Source=" + serverName + ";user id='" + userId + "';password='" + password + "'; Connect Timeout=30");
                }
                try
                {
                    SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT name, cast(replace(physical_name, '\\Data\\SYSBIZZ.mdf', '') as varchar(max)) AS [location] FROM sys.master_files WHERE physical_name like '%'+'\\Data\\SYSBIZZ.mdf'", sqlcon);
                    sqlDa.Fill(dtbl = new DataTable());
                    if (dtbl.Rows.Count == 0 && (serverName.Split('\\'))[0].ToString() == Environment.MachineName)
                    {
                        dtbl.Rows.Add("MyPath", Application.StartupPath);
                    }
                    return dtbl;
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 18493)
                    {
                        if (userId == null || password == null)
                        {
                            sqlcon = new SqlConnection(@"Data Source=" + serverName + ";Integrated Security=True;Connect Timeout=30");
                        }
                        else
                        {
                            sqlcon = new SqlConnection(@"Data Source=" + serverName + ";user id='" + userId + "';password='" + password + "'; Connect Timeout=30");
                        }
                        try
                        {
                            SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT name, cast(replace(physical_name, '\\Data\\SYSBIZZ.mdf', '') as varchar(max)) AS [location] FROM sys.master_files WHERE physical_name like '%'+'\\Data\\SYSBIZZ.mdf'", sqlcon);
                            sqlDa.Fill(dtbl = new DataTable());
                            if (dtbl.Rows.Count == 0 && (serverName.Split('\\'))[0].ToString() == Environment.MachineName)
                            {
                                dtbl.Rows.Add("MyPath", Application.StartupPath);
                            }
                            return dtbl;
                        }
                        catch (SqlException exa)
                        {
                            MessageBox.Show(exa.Message);
                        }
                    }
                    else
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

            }
            return dtbl = new DataTable();
        }

        public string getConnection()
        {
            if (!File.Exists(Application.StartupPath + "\\DbConn.xml"))
            {
                CreateXml();
            }
            string conn = "";
            XmlDocument xml = new XmlDocument();

            xml.Load(Application.StartupPath + "\\DbConn.xml");

            foreach (XmlElement element in xml.SelectNodes("//ConnectionOriginal"))
            {
                conn = element.InnerText;
            }
            return conn;
        }
        public void updateOgXml(string connection)
        {
            if (!File.Exists(Application.StartupPath + "\\DbConn.xml"))
            {
                CreateXml();
            }

            XmlDocument xml = new XmlDocument();

            xml.Load(Application.StartupPath + "\\DbConn.xml");

            foreach (XmlElement element in xml.SelectNodes("//ConnectionOriginal"))
            {
               // connection = Class.CryptorEngine.Encrypt(connection, true);
                element.InnerText = connection;
                xml.Save(Application.StartupPath + "\\DbConn.xml");
            }
        }
        public void CreateXml()
        {
            try
            {
                
                XElement xml = new XElement("Connection",
                   new XElement("ConnectionOriginal", ""));

                xml.Save(Application.StartupPath + "\\DbConn.xml");
            }
            catch (Exception ee)
            {
                //MessageBox.Show(ee.Message);
            }
        }
        public void CreateXml(string conn)
        {
            try
            {

                XElement xml = new XElement("Connection",
                   new XElement("ConnectionOriginal", conn));

                xml.Save(Application.StartupPath + "\\DbConn.xml");
            }
            catch (Exception ee)
            {
                //MessageBox.Show(ee.Message);
            }
        }
        public string GetXmlConnection()
        {
            string connection = "";
            XmlDocument xml = new XmlDocument();

            xml.Load(Application.StartupPath + "\\DbConn.xml");

            foreach (XmlElement element in xml.SelectNodes("//ConnectionOriginal"))
            {

                connection = element.InnerText;
               // connection = Class.CryptorEngine.Decrypt(connection, true);
            }
            if (connection == "")
            {
                CreateXml();
            }
            return connection;
        }
    }
}
