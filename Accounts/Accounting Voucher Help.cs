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
    public partial class Accounting_Voucher_Help : Form
    {
        //private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        //private SqlCommand cmd = new SqlCommand();
        //private SqlDataAdapter adapter = new SqlDataAdapter();
        private DataTable table = new DataTable();
        private BindingSource source = new BindingSource();
        Class.Transactions trans = new Class.Transactions();
        Class.ModifiedTransaction modtrans = new Class.ModifiedTransaction();
        Login lg = (Login)Application.OpenForms["Login"];

        public DataGridViewCellCollection c;
        public Accounting_Voucher_Help()
        {
            InitializeComponent();
            //cmd.Connection = conn;
            //adapter.SelectCommand = cmd;
        }

        private void Accounting_Voucher_Help_Load(object sender, EventArgs e)
        {
            //if (conn.State == ConnectionState.Open)
            //{
            //    conn.Open();
            //}
           string query = "SELECT   VOUCHERNO, DATED, ACCNAME, DEBIT, CREDIT, NARRATION FROM   tb_Transactions WHERE    (VOUCHERTYPE='JOURNAL')";
           
            //adapter.Fill(table);
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
                source.Filter = "VOUCHERNO LIKE '" + txtFilter.Text + "%'";
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }
    }
}
