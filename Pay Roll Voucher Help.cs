using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sys_Sols_Inventory
{
    public partial class Pay_Roll_Voucher_Help : Form
    {
        public DataGridViewCellCollection c;
        private BindingSource source = new BindingSource();
        private DataTable table = new DataTable();

        public Pay_Roll_Voucher_Help()
        {
            InitializeComponent();
        }

        private void Pay_Roll_Voucher_Help_Load(object sender, EventArgs e)
        {
            string query = "SELECT  DOC_NO,DOC_DATE_GRE,DOC_DATE_HIJ,REC_NO,EMP_CODE,AMOUNT,PAY_CODE,CHQ_NO,CHQ_DATE,DESC1,DEBIT_CODE,CREDIT_CODE,BANK_CODE,ACC_DETAILS,NOTES  from PAYROLL_VOUCHER_HDR ";
            table = Model.DbFunctions.GetDataTable(query);
            source.DataSource = table;
            dgItems.DataSource = source;
        }

        private void dgItems_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnOK.PerformClick();
            }
        }

        private void dgItems_DoubleClick(object sender, EventArgs e)
        {
            btnOK.PerformClick();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (dgItems.CurrentRow != null)
            {
                c = dgItems.CurrentRow.Cells;
            }
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            if (txtFilter.Text == "")
            {
                source.Filter = "";
            }
            else
            {
                source.Filter = "DOC_NO LIKE '" + txtFilter.Text + "%'";
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }
    }
}
