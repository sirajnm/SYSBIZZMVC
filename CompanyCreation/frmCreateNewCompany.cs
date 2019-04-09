using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sys_Sols_Inventory.CompanyCreation;
using System.Data;
using System.Data.SqlClient;
namespace Sys_Sols_Inventory.CompanyCreation
{
    public partial class frmCreateNewCompany : Form
    {
        public frmCreateNewCompany()
        {
            InitializeComponent();
        }
        CompanyCreation comp = new CompanyCreation();

        private void btnCreate_Click(object sender, EventArgs e)
        {
            CompanyCreation.CompanyName = txtCompanyName.Name;
            string con = "";
            con = comp.GetServer_Instance();
            SqlConnection conn = new SqlConnection(con);
            conn.Open();
            SqlConnectionStringBuilder cr=new SqlConnectionStringBuilder(con);
            cr.InitialCatalog = "master";
            CompanyCreation.RestoreDB(Application.StartupPath + @"\bak\Sysbizz.bak", txtCompanyName.Text,cr.ToString());
        }




    }
}
