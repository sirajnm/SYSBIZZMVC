using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sys_Sols_Inventory.Accounts
{
    public partial class CurrentDate : Form
    {
        public CurrentDate()
        {
            InitializeComponent();
        }

        Class.CompanySetup CompSetp = new Class.CompanySetup();

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                CompSetp.InsertCurrentDate(dtpCurrentDate.Value.ToString("MM/dd/yyyy"));
                this.Hide();
            }
            catch
            {
            }
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
          //  Application.Exit();
            this.Visible = false;
        }

        private void dtpCurrentDate_KeyDown(object sender, KeyEventArgs e)
        {
            btnOK.Focus();
        }
    }
}
