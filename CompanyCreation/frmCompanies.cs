using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sys_Sols_Inventory.CompanyCreation
{
    public partial class frmCompanies : Form
    {
        public string serverCon = "";
        public string currentForm = "";
        public SqlConnectionStringBuilder cr = null;
        SClass cls = new SClass();
        public frmCompanies(string server)
        {
            InitializeComponent();
            
            cr = new SqlConnectionStringBuilder(server);
        }
        public frmCompanies(string server,string from)
        {
            InitializeComponent();

            cr = new SqlConnectionStringBuilder(server);
            currentForm = server;
        }
        public void Company_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            cr.InitialCatalog = (sender as Button).Name;
            SqlConnection conn = new SqlConnection(cr.ToString());
            try
            {
                conn.Open();
                if (MessageBox.Show("Do you Want Set As Default Company", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    cls.updateOgXml(conn.ConnectionString);
                }
               
                Sys_Sols_Inventory.Model.DbFunctions.conn = conn;
                Class.CompanySetup comset = new Class.CompanySetup();
                if (comset.ReadCompanyName() == true)
                {
                    Login Login = new Login();
                    Login.Show();
                     this.Hide();
                }
            }
            catch
            {

                MessageBox.Show("SQL Error occurred please check sql configuration wizard");
                this.Hide();
                frmMsSqlInstaller MSQLI = new frmMsSqlInstaller();
                MSQLI.button2.Visible = false;
                MSQLI.cmbPath.Visible = false;
                MSQLI.Show();
              //  return false;
            }

           
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmCompanyDetails f = new frmCompanyDetails();
            f.serverConnection = cr.ToString();
            f.Show();
            this.Hide();
        }

        private void frmCompanies_Load(object sender, EventArgs e)
        {

        }

        private void frmCompanies_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Escape)
            {
               
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case (Keys.Escape):
                    {
                        if (currentForm=="")
                        {
                            Application.Exit();
                            return true;
                           
                        }
                        else
                        {
                            this.Close();
                            return true;
                        }
                        break;
                    }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
