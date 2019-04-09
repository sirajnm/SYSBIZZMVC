using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
namespace Sys_Sols_Inventory.Model
{
    public class dboperation
    {
        public static SqlConnection con = new SqlConnection();
        //string DataBaseName =clsproperties.CompanyName;
        bool boolServer = false;
        public dboperation()
        {
           /* try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.ConnectionString = GetServer_Instance();
                    con.Open();
                    Properties.Settings.Default.Connstring = GetServer_Instance();
                    Properties.Settings.Default.Save();
                    TableUpdate();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }*/
         
        }
        public static  SqlConnection  GetConn(string dbname)
        {
            //SqlConnection con = new SqlConnection();
            try
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
                
                if (con.State == ConnectionState.Closed)
                {
                    dboperation db = new dboperation();
                    string connecstring = db.GetServer_Instance();
                    con.ConnectionString = connecstring; // GetServer_Instance();
                    con.Open();
                    Properties.Settings.Default.ConnectionStrings =connecstring;
                    Properties.Settings.Default.Save();
                   // db.TableUpdate();
                    
                }
                return con;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return con;
                
            }
        }
        public static SqlConnection GetCurrentConn()
        {
            return con;
        }
        public string  GetServer_Instance()
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
                        if (i == 0)
                        {
                            if (element == "MSSQLSERVER")
                            {
                                serverName = Environment.MachineName;
                                /*if (CheckDB(serverName))
                                {
                                   // ConnStr = "Data Source=" + serverName + ";Initial Catalog=" + DataBaseName + ";Integrated Security=True";
                                   // CreateTable(ConnStr);
                                    return ConnStr;
                                }*/
                            }
                            else
                            {
                                serverName = Environment.MachineName + @"\" + element;
                                /*if (CheckDB(serverName))
                                {
                                    //ConnStr = "Data Source=" + serverName + ";Initial Catalog=" + DataBaseName + ";Integrated Security=True";
                                   // CreateTable(ConnStr);
                                    return ConnStr;
                                }*/
                            }
                        }
                        else
                        {
                            ConnStr = CreateDb(serverName);
                           // CreateTable(ConnStr);
                            break;
                        }

                    }
                    i++;

                }

            }
            return ConnStr;
        }   
   /*     public static string GetServer()
        {
            dboperation db = new dboperation();
            string serverName = null;
            string ConnStr = null;
            RegistryKey rk = null;

            if(clsproperties.IPAddress!=null)
            {
                //rk =RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine,clsproperties.IPAddress).OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SQL Server");
                if(clsproperties.IPAddress!=null && clsproperties.CompanyName==null)
                {
                    return "Data Source=" + clsproperties.IPAddress + ";Initial Catalog=master;User ID=sa;Password=12345;MultipleActiveResultSets=true";
                }
                else if(clsproperties.IPAddress!=null && clsproperties.CompanyName!=null)
                {
                    return "Data Source=" + clsproperties.IPAddress + ";Initial Catalog=" + clsproperties.CompanyName + ";User ID=sa;Password=12345;MultipleActiveResultSets=true";
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
                                if (clsproperties.CompanyName ==null)
                                {

                                    ConnStr = "Data Source=" + Environment.MachineName + ";Integrated Security=True;MultipleActiveResultSets=true";
                                        if (db.MastDBCheck(ConnStr))
                                        {
                                            return ConnStr;
                                        }
                                   
                                }
                                else
                                {
                                    if (db.CheckDB(Environment.MachineName))
                                    {
                                        ConnStr = "Data Source=" + Environment.MachineName + ";Initial Catalog=" + clsproperties.CompanyName + ";Integrated Security=True;MultipleActiveResultSets=true";
                                        return ConnStr;
                                    }
                                }
                                
                               
                                
                            }
                            else
                            {
                                serverName = Environment.MachineName + @"\" + element;
                                if (clsproperties.CompanyName == null)
                                {

                                    ConnStr = "Data Source=" + serverName + ";Integrated Security=True;MultipleActiveResultSets=true";
                                        if (db.MastDBCheck(ConnStr))
                                        {
                                            return ConnStr;
                                        }
                                   
                                }
                                else
                                {
                                   
                                    if (db.CheckDB(serverName))
                                    {
                                        ConnStr = "Data Source=" + serverName + ";Initial Catalog=" + clsproperties.CompanyName + ";Integrated Security=True;MultipleActiveResultSets=true";
                                         return ConnStr;
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
            return ConnStr;
        }
        public static string  ServerInstance()
        {
            dboperation db = new dboperation();
            string serverName = null;
            string ConnStr = null;
            RegistryKey rk = null;

            if (clsproperties.IPAddress != null)
            {
                //rk =RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine,clsproperties.IPAddress).OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SQL Server");
                if (clsproperties.IPAddress != null && clsproperties.CompanyName == null)
                {
                    return "Data Source=" + clsproperties.IPAddress + ";Initial Catalog=master;User ID=sa;Password=12345;MultipleActiveResultSets=true";
                }
                else if (clsproperties.IPAddress != null && clsproperties.CompanyName != null)
                {
                    return "Data Source=" + clsproperties.IPAddress + ";Initial Catalog=" + clsproperties.CompanyName + ";User ID=sa;Password=12345;MultipleActiveResultSets=true";
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
                                if (clsproperties.CompanyName == null)
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
                                       // ConnStr = "Data Source=" + Environment.MachineName + ";Initial Catalog=" + clsproperties.CompanyName + ";Integrated Security=True";
                                        return Environment.MachineName;
                                    }
                                }



                            }
                            else
                            {
                                serverName = Environment.MachineName + @"\" + element;
                                if (clsproperties.CompanyName == null)
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
                                        //ConnStr = "Data Source=" + serverName + ";Initial Catalog=" + clsproperties.CompanyName + ";Integrated Security=True";
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
        }*/
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
            catch (Exception)
            {
                return false;
            }
        }

        public static bool ExecuteSql(string strsql)
        {
            SqlCommand cmd = con.CreateCommand();
            SqlTransaction SqlTrans;
            SqlTrans = con.BeginTransaction("dbAction");
            cmd.Connection = con;
            cmd.Transaction = SqlTrans;
            try
            {
                cmd.CommandText = strsql;
                cmd.ExecuteNonQuery();
                SqlTrans.Commit();
                return true;
            }
            catch (Exception e)
            {
                SqlTrans.Rollback();
                MessageBox.Show(e.Message);
                return false;

            }
        }
        public static void ExecuteParameter(string strsql,Dictionary<string,object> data)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(strsql, con);
                foreach (KeyValuePair<string, object> ardata in data)
                {
                    cmd.Parameters.Add(new SqlParameter(ardata.Key, ardata.Value));
                }
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }
            

        }

        public static bool ExecuteBackup(string strsql)
        {
           
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = con ;
           
            try
            {
                cmd.CommandText = strsql;
                cmd.ExecuteNonQuery();
               
                return true;
            }
            catch (Exception e)
            {
               
                MessageBox.Show(e.Message);
                return false;

            }
        }

        public static object GetAValue(string qry)
        {
            SqlCommand cmd;
            
            cmd = new SqlCommand(qry, con);
            return (cmd.ExecuteScalar());
        }
        public static DataTable GetDataTable(string qry)
        {
           
                SqlDataAdapter cmd;
                DataSet dts = new DataSet();

                cmd = new SqlDataAdapter(qry, con);
                // cmd.SelectCommand.CommandTimeout = 0;
                cmd.Fill(dts);
                return (dts.Tables[0]);
            
        }
        public static DataSet GetDataTable(string qry, string TableName)
        {
            SqlDataAdapter cmd;
            DataSet dts = new DataSet();
            
            cmd = new SqlDataAdapter(qry, con);
            cmd.Fill(dts, TableName);
            return (dts);
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
            string SqlConStr="Data Source=" + ServerName + ";Integrated Security=True";
            return SqlConStr;
            // conn.Dispose();

        }
        private bool CreateTable(SqlConnection  SqlCon,string database)
        {
            try
            {
                string strsql = QueryString();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = SqlCon;
                cmd.CommandText = strsql;
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
                   
        }
        private string QueryString()
        {
            string strsql = "";
            strsql = "if not exists (select * from sysobjects where name='itemdetails' and xtype='U')"
                                + "CREATE TABLE [dbo].[itemdetails]("
                                + "[itemid] [varchar](500) NOT NULL,"
                                + "[Description] [varchar](500) NULL,"
                                + "[Price] [float] NULL,"
                                + "[oldprice] [float] NULL,"
                                + "[salesrate] [float] NULL,"
                                + "[unit][varchar](50)NULL,"
                                 + "CONSTRAINT [PK_itemdetails] PRIMARY KEY CLUSTERED "
                                + "("
                                + "[itemid] ASC"
                                + ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]"
                                + ") ON [PRIMARY]"

                                + "if not exists (select * from sysobjects where name='purchasedetails' and xtype='U')"
                                + "CREATE TABLE [dbo].[purchasedetails]("
                                + "[purchaseid] [bigint] NOT NULL,"
                                + "[invoiceno] [varchar](500) NULL,"
                                + "[itemid] [varchar](500) NULL,"
                                + "[quantity] [bigint] NULL,"
                                + "[unit] [varchar](50) NULL,"
                                + "CONSTRAINT [PK_purchasedetails] PRIMARY KEY CLUSTERED "
                                + "("
                                + "[purchaseid] ASC"
                                + ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]"
                                + ") ON [PRIMARY]"

                                + "if not exists (select * from sysobjects where name='purchaseinvoice' and xtype='U')"
                                + "CREATE TABLE [dbo].[purchaseinvoice]("
                                + "[invoiceno] [varchar](500) NOT NULL,"
                                + "[suplierName] [varchar](100) NULL,"
                                + "[phonenumber] [varchar](50) NULL,"
                                + "[address] [varchar](100) NULL,"
                                + "[discount] [float] NULL,"
                                + "[purchasedate] [date] NULL,"
                                + "[paidcash] [float] NULL,"
                                + "[paidcheque] [float] NULL,"
                                + "[referenceno] [varchar](100) NULL,"
                                + "CONSTRAINT [PK_purchaseinvoice] PRIMARY KEY CLUSTERED "
                                + "("
                                + "[invoiceno] ASC"
                                + ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]"
                                + ") ON [PRIMARY]"

                                + "if not exists (select * from sysobjects where name='purchasereturn' and xtype='U')"
                                + "CREATE TABLE [dbo].[purchasereturn]("
                                + "[returnno] [varchar](500) NOT NULL,"
                                + "[oldinvoiceno] [varchar](500) NULL,"
                                + "[phonenumber] [varchar](20) NULL,"
                                + "[address] [varchar](500) NULL,"
                                + "[supplier] [varchar](50) NULL,"
                                + "[paidamount] [float] NULL,"
                                + "[discount] [float] NULL,"
                                +"[returndate][datetime] NULL,"
                                + "CONSTRAINT [PK_purchasereturn] PRIMARY KEY CLUSTERED "
                                + "("
                                + "[returnno] ASC"
                                + ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]"
                                + ") ON [PRIMARY]"

                                + "if not exists (select * from sysobjects where name='purchasereturndetails' and xtype='U')"
                                + "CREATE TABLE [dbo].[purchasereturndetails]("
                                + "[purchasereturnid] [bigint] NOT NULL,"
                                + "[returnno] [varchar](500) NULL,"
                                + "[itemid] [varchar](500) NULL,"
                                + "[quantity] [bigint] NULL,"
                                + "[unit] [varchar](50) NULL,"
                                +"[price] [float] NULL,"
                                + "CONSTRAINT [PK_purchasereturndetails] PRIMARY KEY CLUSTERED "
                                + "("
                                + "[purchasereturnid] ASC"
                                + ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]"
                                + ") ON [PRIMARY]"

                                + "if not exists (select * from sysobjects where name='salesdetails' and xtype='U')"
                                + " CREATE TABLE [dbo].[salesdetails]("
                                + "[salesid] [bigint] NOT NULL,"
                                + "[invoiceno] [varchar](200) NULL,"
                                + "[itemid] [varchar](500) NULL,"
                                + "[quantity] [bigint] NULL,"
                                + "[unit] [varchar](50) NULL,"
                                + "[supplier] [varchar](100) NULL,"
                                + "[salesrate] [float] NULL,"
                                + "CONSTRAINT [PK_salesdetails] PRIMARY KEY CLUSTERED "
                                + "("
                                + "[salesid] ASC"
                                + ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]"
                                + ") ON [PRIMARY]"

                                + "if not exists (select * from sysobjects where name='salesinvoice' and xtype='U')"
                                + "CREATE TABLE [dbo].[salesinvoice]("
                                + "[salesinvoice] [varchar](200) NOT NULL,"
                                + "[customerid] [bigint] NULL,"
                                + "[customername] [varchar](200) NULL,"
                                + "[phonenumber] [varchar](20) NULL,"
                                + "[address] [varchar](500) NULL,"
                                + "[discount] [float] NULL,"
                                + "[additionaldiscount] [float] NULL,"
                                + "[paidcash] [float] NULL,"
                                + "[paidcheque] [float] NULL,"
                                + "[salesdate] [date] NULL,"
                                + "CONSTRAINT [PK_salesinvoice] PRIMARY KEY CLUSTERED "
                                + "("
                                + "[salesinvoice] ASC"
                                + ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]"
                                + ") ON [PRIMARY]"

                                + "if not exists (select * from sysobjects where name='generalledger' and xtype='U')"
                                + "CREATE TABLE [dbo].[generalledger]("
                                + "[ledcode] [bigint] NOT NULL,"
                                + "[ledgerdate] [date] NULL,"
                                + "[ledgertime] [datetime] NULL,"
                                + "[particulars] [varchar](200) NULL,"
                                + "[Debitamt] [float] NULL,"
                                + "[Creditamt] [float] NULL,"
                                + "[Balance] [float] NULL,"
                                + "[voucherno] [varchar](200) NULL,"
                                + "[vouchertype] [varchar](50) NULL,"
                                + "CONSTRAINT [PK_generalledger] PRIMARY KEY CLUSTERED "
                                + "("
                                + "[ledcode] ASC"
                                + ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]"
                                + ") ON [PRIMARY]"

                               + "if not exists (select * from sysobjects where name='voucher' and xtype='U')"
                               + "CREATE TABLE [dbo].[voucher]("
                               + "[voucherno] [bigint] NOT NULL,"
                               + "[typeoftransaction] [varchar](100) NULL,"
                               + "[typeofvoucher] [varchar](100) NULL,"
                               + "[voucherdate] [date] NULL,"
                               + "PRIMARY KEY CLUSTERED"
                               + "("
                               + "[voucherno] ASC"
                               + ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]"
                               + ") ON [PRIMARY]"

                               + "if not exists (select * from sysobjects where name='voucherdetails' and xtype='U')"
                               + "CREATE TABLE [dbo].[voucherdetails]("
                               + "[voucherdetailno] [bigint] NOT NULL,"
                               + "[voucherno] [bigint] NULL,"
                               + "[accounttype] [varchar](100) NULL,"
                               + "[name] [varchar](100) NULL,"
                               + "[referenceinv] [varchar](100) NULL,"
                               + "[amount] [float] NULL,"
                               + "[remark] [varchar](200) NULL,"
                               + "[oldbalance] [float] NULL,"
                               + "CONSTRAINT [PK_voucherdetails] PRIMARY KEY CLUSTERED "
                               + "("
                               + "[voucherdetailno] ASC"
                               + ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]"
                               + ") ON [PRIMARY]";
            return strsql;
        }
       /* private bool CheckDB(string ServerName)
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
            catch (Exception)
            {
                return false;
            }
        }*/
        private bool CreateTable(string conStr)
        {
            try
            {
                SqlConnection conTable = new SqlConnection();
                conTable.ConnectionString = conStr;
                conTable.Open();
                string strsql = QueryString();
                SqlCommand cmd = new SqlCommand(strsql, conTable);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        /*private bool TableUpdate()
        {
            try
            {
                string strsql = "IF not  EXISTS ("
                               + "SELECT * "
                               + "FROM   sys.columns "
                               + "WHERE  object_id = OBJECT_ID(N'[dbo].[generalledger]') "
                               + "AND name = 'RVNo'"
                               + ")alter table generalledger add RVNO varchar(500) "

                               + "IF not  EXISTS ("
                               + "SELECT * "
                               + "FROM   sys.columns "
                               + "WHERE  object_id = OBJECT_ID(N'[dbo].[itemdetails]') "
                               + "AND name = 'unit'"
                               + ")alter table itemdetails add unit varchar(50) "

                               + "IF not  EXISTS ("
                               + "SELECT * "
                               + "FROM   sys.columns "
                               + "WHERE  object_id = OBJECT_ID(N'[dbo].[purchasedetails]') "
                               + "AND name = 'price'"
                               + ")alter table purchasedetails add price float "

                               + "IF not  EXISTS ("
                               + "SELECT * "
                               + "FROM   sys.columns "
                               + "WHERE  object_id = OBJECT_ID(N'[dbo].[salesdetails]') "
                               + "AND name = 'newcost'"
                               + ")alter table salesdetails add newcost float ";
                SqlCommand cmd = new SqlCommand(strsql, con);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }*/
       /* public static void RollBackDb()
        {
            try
            {

                string strsql = "use master Drop database " + clsproperties.CompanyName;
                
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = strsql;
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }*/
   /*     public static DataTable RetrieveCompany()
        {
            DataTable dt = new DataTable();
            dt = getMastConn().GetSchema("Databases");
            return dt;

        }
/*        public static SqlConnection getMastConn()
        {
            string strsql;
            strsql = GetServer();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = strsql;
            if (con.State == ConnectionState.Open) { con.Close(); }
            con.Open();
            return con;
        }*/
       /* public static void SetServerConnection(bool defaultcmp=false)
        {
            if (defaultcmp)
            {
                Properties.Settings.Default.Defaultcompany = GetServer();
                Properties.Settings.Default.Save();
            }
            
            if (con.State == ConnectionState.Open)
                con.Close();
            con.ConnectionString = GetServer();
            con.Open();
            Properties.Settings.Default.Connstring = GetServer();
            Properties.Settings.Default.Save();

        }
        public static void setDefault()
        {
            try
            {
                string connstring = Properties.Settings.Default.Defaultcompany;

                if (con.State == ConnectionState.Open)
                    con.Close();
                con.ConnectionString = connstring;
                con.Open();
                Properties.Settings.Default.Connstring = connstring;
                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }*/

        public static void ClearConnection()
        {
            if (con.State == ConnectionState.Open)
                con.Close();
        }
      /*  public static bool DbRestore(OpenFileDialog backup,String databaseName)
        {
            try
            {
                dboperation db = new dboperation();
                dboperation.GetConn("master");
                ServerConnection conn = new ServerConnection();
                conn.ServerInstance = ServerInstance();
                Server srv = new Server(conn);
                Restore res = new Restore();
                res.Devices.AddDevice(backup.FileName, DeviceType.File);
                RelocateFile DataFile = new RelocateFile();
                string MDF = res.ReadFileList(srv).Rows[0][1].ToString();
                DataFile.LogicalFileName = res.ReadFileList(srv).Rows[0][0].ToString();
                DataFile.PhysicalFileName = srv.Databases[databaseName].FileGroups[0].Files[0].FileName;

                RelocateFile LogFile = new RelocateFile();
                string LDF = res.ReadFileList(srv).Rows[1][1].ToString();
                LogFile.LogicalFileName = res.ReadFileList(srv).Rows[1][0].ToString();
                LogFile.PhysicalFileName = srv.Databases[databaseName].LogFiles[0].FileName;

                res.RelocateFiles.Add(DataFile);
                res.RelocateFiles.Add(LogFile);

                res.Database = databaseName;
                res.NoRecovery = true;
                res.ReplaceDatabase = true;
                res.SqlRestore(srv);
                conn.Disconnect();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
        }
        */
    }

}
