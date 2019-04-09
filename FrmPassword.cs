using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sys_Sols_Inventory
{
    public partial class FrmPassword : Form
    {
        Class.DatabaseSettings dsSettings = new Class.DatabaseSettings();
        Class.CompanySetup Comp = new Class.CompanySetup();
        public static string originalDate;
        public static string duplicateDate;
        Initial mdi = (Initial)Application.OpenForms["Initial"];
        public FrmPassword()
        {
            InitializeComponent();
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (txtPassword.Text == "incorrect")
                {
                    try
                    {
                        if (Initial.dbSet == true)
                        {
                            Initial init = (Initial)Application.OpenForms["Initial"];
                            Properties.Settings.Default.ConnectionStrings = dsSettings.ConnDuplicate();
                            Properties.Settings.Default.Save();
                            //btnSwitch.BackColor = Color.Green; 
                            duplicateDate = GetDate();
                            if (HasCurrentDate())
                            {
                                if (originalDate != duplicateDate)
                                {
                                    UpdateCurrentDate();
                                }
                            }
                            else
                            {
                                InsertCurrentDate();
                            }
                            init.btnSwitch.BackColor = Color.MidnightBlue;
                            init.picsales.Focus();
                            this.Close();
                        }
                        else
                        {
                            Initial init = (Initial)Application.OpenForms["Initial"];
                            init.picsales.Focus();                            
                            frmMsSqlInstaller MSQLI = new frmMsSqlInstaller();
                            MSQLI.Show();
                            MSQLI.BringToFront();
                            Initial.flag = true;
                            this.Close();
                        }
                    }
                    catch
                    {
                        Initial init = (Initial)Application.OpenForms["Initial"];
                        init.picsales.Focus();                        
                        frmMsSqlInstaller MSQLI = new frmMsSqlInstaller();
                        MSQLI.Show();
                        MSQLI.BringToFront();
                        Initial.flag = true;
                        this.Close();
                    }
                    
                }
                else
                {
                    txtPassword.Text = string.Empty;
                    lblMessage.Visible = true;                    
                }
                Common.preventDingSound(e);
            }
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }   
        }
        public void GetCurrentDate()
        {
            int datecout;
            datecout = Comp.GetCurrentDate();
            if (datecout == 0)
            {
                Accounts.CurrentDate CD = new Accounts.CurrentDate();
                CD.ShowDialog();
            }
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            lblMessage.Visible = false;
        }

        private void FrmPassword_Load(object sender, EventArgs e)
        {            
            originalDate = GetDate();
        }
        public static string GetDate()
        {
            //SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
            //SqlCommand cmd;
            //conn.Open();
            //cmd = new SqlCommand("SELECT Date FROM tbl_CurrentDate",conn);            
            //conn.Close();
            string qry = "SELECT Date FROM tbl_CurrentDate";
            string CurrentDate = Model.DbFunctions.GetAValue(qry).ToString();

            return CurrentDate;
        }
        public static bool HasCurrentDate()
        {
            //SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
            //SqlCommand cmd;
            int countvalue = 0;
            //conn.Open();
            //cmd = new SqlCommand("SELECT COUNT(Date) FROM tbl_CurrentDate", conn);
            //conn.Close();
            string qry = "SELECT COUNT(Date) FROM tbl_CurrentDate";
            countvalue = Convert.ToInt32(Model.DbFunctions.GetAValue(qry));
            if (countvalue == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static void InsertCurrentDate()
        {
            //SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
            //SqlCommand cmd;
            string qry = "INSERT INTO tbl_CurrentDate (Date)VALUES('" + originalDate + "')";
            //conn.Open();
            //cmd.Connection = conn;
            //cmd.ExecuteNonQuery();           
            //conn.Close();
            Model.DbFunctions.InsertUpdate(qry);
        }
        public static void UpdateCurrentDate()
        {
            //SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
            //SqlCommand cmd;
            //cmd = new SqlCommand("UPDATE tbl_CurrentDate SET Date = '"+ originalDate+"'",conn);
            string qry = "UPDATE tbl_CurrentDate SET Date = '" + originalDate + "'";
            //conn.Open();
            //cmd.Connection = conn;
            //cmd.ExecuteNonQuery();            
            //conn.Close();
            Model.DbFunctions.InsertUpdate(qry);
        }

        private void FrmPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
