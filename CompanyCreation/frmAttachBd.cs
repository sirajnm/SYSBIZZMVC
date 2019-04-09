using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sys_Sols_Inventory.CompanyCreation
{
    public partial class frmAttachBd : Form
    {
        public frmAttachBd()
        {
            InitializeComponent();
        }
        frmCompanyDetails frmCom =(frmCompanyDetails)Application.OpenForms["frmCompanyDetails"];
         SClass cls = new SClass();
        private void button1_Click(object sender, EventArgs e)
        {


            //Filter
            openFileDialog1.Filter = " Files|*.mdf";
            openFileDialog1.Title = "Select Data";
            //When the user select the file
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Get the file's path
               txtPath.Text= openFileDialog1.FileName;
            }


        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case (Keys.Escape):
                   
                        
                            this.Close();
                            return true;
                      
                        break;
               
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnAttach_Click(object sender, EventArgs e)
        {
            if (txtPath.Text != "")
            {
                if (File.Exists(txtPath.Text)&& File.Exists(txtPath.Text.Substring(0, (txtPath.Text.LastIndexOf('.'))) + "_log.ldf"))
                {
                    var onlyFileName = System.IO.Path.GetFileNameWithoutExtension(openFileDialog1.FileName);
                   if(  CompanyCreation.CheckDBExist(onlyFileName.ToString(), frmCom.serverConnection))
                    {
                        MessageBox.Show("this company already attached please check company list");
                    }
                   else
                    {

                        // SqlConnectionStringBuilder cr = new SqlConnectionStringBuilder(frmCom.serverConnection);
                        // cr.InitialCatalog = onlyFileName;
                         SqlConnection conn1 = CompanyCreation.attachDB(txtPath.Text, txtPath.Text.Substring(0, (txtPath.Text.LastIndexOf('.'))) + "_log.ldf", onlyFileName, frmCom.serverConnection);
                        if (conn1 != null)
                        {
                            SqlConnection conn = conn1;
                            try
                            {
                                if (conn1.State == ConnectionState.Open)
                                    conn1.Close();
                                    conn1.Open();
                                if (MessageBox.Show("Do You Want Set As Default Company", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                                {
                                    cls.updateOgXml(conn.ConnectionString);
                                }

                                Sys_Sols_Inventory.Model.DbFunctions.conn = conn;
                                Class.CompanySetup comset = new Class.CompanySetup();
                                if (comset.ReadCompanyName() == true)
                                {
                                    for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
                                    {
                                        if (Application.OpenForms[i].Name != "frmAttachBd")
                                            Application.OpenForms[i].Hide();
                                    }
                                    Login Login = new Login();
                                    Login.Show();
                                    this.Hide();
                                   
                                }
                                else
                                {

                                    Initinal_Setup_1 InSp1 = new Initinal_Setup_1();
                                    InSp1.Show();
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
                            }
                        }
                    }
                    
                }
            }
           
        }
    }
}
