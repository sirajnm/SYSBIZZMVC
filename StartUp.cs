using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Sys_Sols_Inventory.CompanyCreation;
using System.Management;

namespace Sys_Sols_Inventory
{
    public partial class StartUp : Form
    {
        Class.Activation Act = new Class.Activation();
        //Class.DatabaseSettings dsSettings = new Class.DatabaseSettings();
        //string server;
        SClass cls = new SClass();
        CompanyCreation.CompanyCreation comp = new CompanyCreation.CompanyCreation();
        Class.CompanySetup cset = new Class.CompanySetup();
        public string productType = "";
        int remain;
        DateTime datecur;

        public StartUp()
        {
            InitializeComponent();
        }

        int timercount = 0;

        private void StatrtUp_Load(object sender, EventArgs e)
        {
            this.timer1.Start();
            RegistryKey rkey = Registry.CurrentUser.OpenSubKey(@"Control Panel\International", true);
            rkey.SetValue("sShortDate", "dd-MMM-yy");
            rkey.SetValue("sLongDate", "dd/MM/yy");
        }
       
        public bool DataBaseConnectionSettings()
        {

            //productType = "Demo";
            productType = CompanyCreation.CompanyCreation.checkPackageType() == "1" ? "Demo" : CompanyCreation.CompanyCreation.checkPackageType() == "2" ? "Premium" : "";
            //Properties.Settings.Default.ConnectionStrings = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=D:\Data\Data\Data\SYSBIZZ.mdf;Integrated Security=True;Connect Timeout=30";
            // Properties.Settings.Default.ConnectionStrings = "";
            //  Properties.Settings.Default.Save();

            if (productType == "Premium")
                CompanyCreation.CompanyCreation.packageType = true;



            

            if (!Directory.Exists(Application.StartupPath + @"\Companies"))
            {
                Directory.CreateDirectory(Application.StartupPath + @"\Companies");
            }
            DirectoryInfo info = new DirectoryInfo(Application.StartupPath + @"\Companies");
            using (ManagementObject dir = new ManagementObject("Win32_Directory.Name=\"" + info.FullName.Replace(@"\", @"\\") + "\""))
            { 
                    dir.InvokeMethod("Uncompress", null, null);
            }
            try
            {
                if (!File.Exists(Application.StartupPath + "\\DbConn.xml"))

                {                 


                    if (productType == "Demo")

                    {


                      
                        if (CompanyCreation.CompanyCreation.checkExpiry("Demo"))

                        {
                            string con = "";
                            con = comp.GetServer_Instance();
                            if (con == "" || con == null)
                            {
                                MessageBox.Show("SQL Error occurred please check sql configuration wizard");
                                this.Hide();
                                frmMsSqlInstaller MSQLI = new frmMsSqlInstaller();
                                MSQLI.button2.Visible = false;
                                MSQLI.cmbPath.Visible = false;
                                MSQLI.Show();
                                return false;
                            }
                            SqlConnectionStringBuilder cr = new SqlConnectionStringBuilder(con);
                            SqlConnection conn = new SqlConnection(con);
                            try
                            {
                                conn.Open();

                                cr.InitialCatalog = "master";
                                conn.Close();
                            }
                            catch
                            {
                                MessageBox.Show("SQL Error occurred please check sql configuration wizard");
                                this.Hide();
                                frmMsSqlInstaller MSQLI = new frmMsSqlInstaller();
                                MSQLI.button2.Visible = false;
                                MSQLI.cmbPath.Visible = false;
                                MSQLI.Show();
                                return false;
                            }
                            if (CompanyCreation.CompanyCreation.CheckDBExist("SYSBIZZ", cr.ToString()))
                            {
                                try
                                {
                                    cr.InitialCatalog = "SYSBIZZ";
                                    conn.ConnectionString = cr.ToString();
                                    conn.Open();
                                    cls.updateOgXml(conn.ConnectionString);
                                    return true;
                                }
                                catch (Exception e)
                                {
                                    MessageBox.Show(e.Message.ToString());
                                }
                            }
                            else
                            {

                                if (!File.Exists(Application.StartupPath + "\\Companies\\SYSBIZZ.mdf") && !File.Exists(Application.StartupPath + "\\Companies\\SYSBIZZ_log.ldf"))
                                {
                                    try
                                    {
                                        Sys_Sols_Inventory.CompanyCreation.CompanyCreation.RestoreDB(Application.StartupPath + @"\bak\Sysbizz.bak", "SYSBIZZ", cr.ToString());
                                        try
                                        {
                                            if (conn.State == ConnectionState.Open)
                                            {
                                                conn.Close();
                                            }
                                            cr.InitialCatalog = "SYSBIZZ";
                                            conn.ConnectionString = cr.ToString();
                                            conn.Open();
                                            cls.updateOgXml(conn.ConnectionString);
                                            Setfinancialyear();
                                            Sys_Sols_Inventory.Model.DbFunctions.conn = conn;
                                            cset.Company_Name = "DemoCompany";
                                            cset.ARBCompany_Name = "";
                                            cset.PAN_No = "";
                                            cset.CST_No = "";
                                            cset.TIN_No = "32DSFDSFFFNGH";
                                            cset.WebSite = "";
                                            cset.Logo = "";
                                            cset.SDate = from.Date;
                                            cset.EDate = to.Date;
                                            cset.Status = true;
                                            cset.Country = "IND";
                                            cset.insertcompanydetails();
                                            cset.insertFinancialYear();
                                            cset.SysSetup_InsertCompany();
                                            //  AddXlInfor();
                                            updateLedger();

                                            cset.CODE = "HDO";
                                            cset.DESC_ENG = "DemoCompany";
                                            cset.DESC_ARB = "";
                                            cset.POSTerminal = false;
                                            cset.BARCODE = true;
                                            cset.ALLOW_APPLIANCES = 0;
                                            cset.ALLOW_SPARES = 0;
                                            cset.ALLOW_SERVICES = 0;
                                            cset.ADDRESS_1 = "";
                                            cset.ADDRESS_2 = "";
                                            cset.TELE_1 = "";
                                            cset.Email = "";
                                            cset.Fax = "";
                                            cset.DEFAULT_CURRENCY_CODE = "INR";
                                            cset.ARBADDRESS_1 = "";
                                            cset.ARBADDRESS_2 = "";
                                            cset.InsertBranch();
                                            return true;
                                        }
                                        catch (Exception e)
                                        {
                                            MessageBox.Show(e.Message.ToString());
                                        }
                                    }
                                    catch
                                    {
                                    }
                                }
                                else
                                {
                                    if (File.Exists(Application.StartupPath + "\\Companies\\SYSBIZZ.mdf") && File.Exists(Application.StartupPath + "\\Companies\\SYSBIZZ_log.ldf"))
                                    {
                                        SqlConnection conn1 = CompanyCreation.CompanyCreation.attachDB(Application.StartupPath + "\\Companies\\SYSBIZZ.mdf", Application.StartupPath + "\\Companies\\SYSBIZZ_log.ldf", "SYSBIZZ", cr.ToString());
                                        if (conn1 != null)
                                        {


                                            if (conn1.State == ConnectionState.Open)
                                                conn1.Close();
                                            conn1.Open();
                                            cls.updateOgXml(conn1.ConnectionString);
                                            return true;
                                        }
                                        else
                                        {

                                            MessageBox.Show("Old demo Company is not valid please clear Companies folder");
                                            Application.Exit();
                                        }

                                    }
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Product Expired.. Please Activate the product or Contact the System Vendor.", "Sysbizz", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            Activation.Activation act = new Activation.Activation();
                            act.ShowDialog();
                        }
                    }
                    else if (productType == "Premium")
                    {
                        //is premium 
                        if (CompanyCreation.CompanyCreation.checkExpiry("Premium"))
                        {
                            CompanyCreation.CompanyCreation.packageType = true;
                            string con = "";
                            con = comp.GetServer_Instance();
                            if (con == "" || con == null)
                            {
                                MessageBox.Show("SQL Error occurred please check sql configuration wizard");
                                this.Hide();
                                frmMsSqlInstaller MSQLI = new frmMsSqlInstaller();
                                MSQLI.button2.Visible = false;
                                MSQLI.cmbPath.Visible = false;
                                MSQLI.Show();
                                return false;
                            }
                            SqlConnectionStringBuilder cr = new SqlConnectionStringBuilder(con);
                            SqlConnection conn = new SqlConnection(con);
                            try
                            {
                                conn.Open();

                                cr.InitialCatalog = "master";
                                conn.Close();
                            }
                            catch
                            {
                                MessageBox.Show("SQL Error occurred please check sql configuration wizard");
                                this.Hide();
                                frmMsSqlInstaller MSQLI = new frmMsSqlInstaller();
                                MSQLI.button2.Visible = false;
                                MSQLI.cmbPath.Visible = false;
                                MSQLI.Show();
                                return false;

                            }

                            SqlDataReader r = CompanyCreation.CompanyCreation.getCompanies(cr.ToString());
                            frmCompanies comp1 = new frmCompanies(cr.ToString());

                            int count = 0;
                            while (r.Read())
                            {
                                //  int a=(r["Location"].ToString().LastIndexOf('.'));
                                Button btn = new Button();
                                string b = r["Location"].ToString().Substring(0, (r["Location"].ToString().LastIndexOf('.'))) + "_log.ldf";
                                if (File.Exists(r["Location"].ToString()) && File.Exists(r["Location"].ToString().Substring(0, (r["Location"].ToString().LastIndexOf('.'))) + "_log.ldf"))
                                {
                                    btn.Text = r["DBName"].ToString();
                                    btn.Name = r["DBName"].ToString();
                                    btn.Size = new Size(100, 80);
                                    btn.FlatStyle = FlatStyle.Flat;
                                    btn.BackColor = Color.MediumVioletRed;
                                    btn.ForeColor = Color.White;
                                    btn.Click += comp1.Company_Click;
                                    btn.Cursor = Cursors.Hand;
                                    comp1.pnlLoadCompanies.Controls.Add(btn);
                                    count += 1;
                                    // btn.Text = r["DBName"].ToString();
                                }

                            }
                            CompanyCreation.CompanyCreation.conn.Close();
                            if (count > 1)
                            {
                                comp1.Show();
                            }
                            else
                            {
                                frmCompanyDetails fcom = new frmCompanyDetails();
                                fcom.serverConnection = cr.ToString();
                                fcom.Show();
                                this.Hide();
                            }
                        }
                        else
                        {
                            Activation.Activation act = new Activation.Activation();
                            act.ShowDialog();
                            this.Hide();
                        }
                    }
                    else
                    {
                        Activation.Activation act = new Activation.Activation();
                        act.ShowDialog();
                        this.Hide();
                        /*MessageBox.Show("Sysbizz configuration missing ,please contact vendor")*/
                        ;
                        //Application.Exit();
                    }
                }
                else if (productType != "")
                {
                    if (CompanyCreation.CompanyCreation.checkExpiry(productType))
                    {
                        if (cls.getConnection() != "")
                        // if (Properties.Settings.Default.ConnectionStrings.ToString() != "")
                        // if (Properties.Settings.Default.ConnectionStrings==dsSettings.ConnOriginal())
                        {
                            SqlConnection con = new SqlConnection(cls.getConnection());
                            Model.DbFunctions.conn = con;
                            return true;
                        }


                        else
                        {
                            frmMsSqlInstaller MSQLI = new frmMsSqlInstaller();
                            MSQLI.Show();
                            return false;
                        }
                    }
                    else
                    {
                        Activation.Activation act = new Activation.Activation();
                        act.ShowDialog();
                    }
                }
                else
                {
                    Activation.Activation act = new Activation.Activation();
                    act.ShowDialog();
                }
            }
            catch (Exception)
            {
                this.Hide();
                frmMsSqlInstaller MSQLI = new frmMsSqlInstaller();
                MSQLI.Show();
                return false;
            }
            finally
            {
                this.Hide();
            }
            return false;
        }
        DateTime from, to;
        public void Setfinancialyear()
        {
            try
            {
                if (DateTime.Now.Month >= 4)
                {
                    DateTime dt = new DateTime(DateTime.Now.Year, 4, 1);
                    from = dt;
                    DateTime dt2 = new DateTime(DateTime.Now.Year + 1, 3, 31);
                    to = dt2;
                }
                else
                {
                    DateTime dt = new DateTime(DateTime.Now.Year - 1, 4, 1);
                    from = dt;
                    DateTime dt2 = new DateTime(DateTime.Now.Year, 3, 31);
                    to = dt2;

                }
            }
            catch { }
        }
        void updateLedger()
        {
            Properties.Settings.Default.Tax_Type = "GST";
            Properties.Settings.Default.Save();
            cset.updateTaxLedger(66, "INPUT " + "GST");
            cset.updateTaxLedger(83, "OUTPUT " + "GST");
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (timercount < 100)
            {
                this.Progress.Increment(1);
                timercount = timercount + 1;
            }
            else
            {
                this.timer1.Stop();
                if (DataBaseConnectionSettings())
                {
                    if (GetActivations())
                    {

                        Class.CompanySetup comset = new Class.CompanySetup();
                        if (comset.ReadCompanyName() == true)
                        {
                            Login Login = new Login();
                            Login.Show();
                        }
                        else
                        {
                            Initinal_Setup_1 InSp1 = new Initinal_Setup_1();
                            InSp1.Show();
                        }


                        this.Visible = false;
                    }
                    else
                    {
                        AppLicense.Validate Vali = new AppLicense.Validate();
                        Vali.Show();
                        this.Visible = false;
                    }
                }
            }
        }

        public void Encrypt(string Date, string Value)
        {
            string cipherText = Class.CryptorEngine.Encrypt(DateTime.Now.ToShortDateString(), true);
            if (Date != cipherText)
            {
                string Decryptvalue = Class.CryptorEngine.Decrypt(Value, true);
                int a = Convert.ToInt32(Decryptvalue) - 1;
                Act.Value = Class.CryptorEngine.Encrypt(a.ToString(), true);
                Act.Status = Class.CryptorEngine.Encrypt("No", true);
                Act.Date = cipherText;
                Act.UpdateActivation();
                DeleteCurrentDate();
            }
        }

        public void Encryptforyes(string Date, string Value)
        {
            string cipherText = Class.CryptorEngine.Encrypt(DateTime.Now.ToShortDateString(), true);
            if (Date != cipherText)
            {
                string Decryptvalue = Class.CryptorEngine.Decrypt(Value, true);
                int a = Convert.ToInt32(Decryptvalue) + 1;
                Act.Value = Class.CryptorEngine.Encrypt(a.ToString(), true);
                Act.Status = Class.CryptorEngine.Encrypt("Yes", true);
                Act.Date = cipherText;
                Act.UpdateActivation();
                DeleteCurrentDate();
            }
        }

        public bool GetActivations()
        {
            bool value = false;
            try
            {
                DataTable dt = Act.GetDicrypt();
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0][0].ToString() == "1")
                    {
                        string Status = Class.CryptorEngine.Decrypt(dt.Rows[0][4].ToString(), true);
                        if (Status == "Yes")
                        {
                            Encryptforyes(dt.Rows[0][1].ToString(), dt.Rows[0][2].ToString());
                            value = true;
                        }
                        else if (Status == "No")
                        {
                            string Limitdate = Class.CryptorEngine.Decrypt(dt.Rows[0][2].ToString(), true);
                            if (Limitdate != "0")
                            {
                                Encrypt(dt.Rows[0][1].ToString(), dt.Rows[0][2].ToString());
                                value = true;
                            }
                            else
                            {
                                this.Visible = false;
                                AppLicense.Validate vali = new AppLicense.Validate();
                                vali.Show();
                                value = false;
                                return value;
                            }
                        }
                        else
                        {
                            value = false;
                        }
                        return value;
                    }
                    else
                    {
                        value = false;
                        return value;
                    }
                }
                else
                {
                    Act.Date = Class.CryptorEngine.Encrypt(DateTime.Now.ToShortDateString(), true);
                    Act.Value = Class.CryptorEngine.Encrypt("30", true);
                    Act.Status = Class.CryptorEngine.Encrypt("No", true);
                    Act.Data = Class.CryptorEngine.Encrypt("NEESAH33SYS", true);
                    Act.InsertEnryptDate();
                    return true;
                }
            }
            catch
            {
                return value;
            }
        }

        public void Decrypt()
        {
            DataTable dt = new DataTable();
            if (dt.Rows.Count > 0)
            {
                string decryptedText = Class.CryptorEngine.Decrypt(dt.Rows[0][0].ToString(), true);
            }
        }

        public void DeleteCurrentDate()
        {
            try
            {
                Class.CompanySetup Comp = new Class.CompanySetup();
                Comp.DeleteCurrentDate();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }
    }




}
