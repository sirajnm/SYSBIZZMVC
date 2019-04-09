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
    public partial class BatchHelp : Form
    {
        private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        private SqlCommand cmd = new SqlCommand();
        private SqlDataAdapter adapter = new SqlDataAdapter();
        private DataTable table = new DataTable();
        private BindingSource source = new BindingSource();
        public DataGridViewCellCollection c;
        private string code;

        public BatchHelp(string item_code)
        {
            InitializeComponent();
            cmd.Connection = conn;
            adapter.SelectCommand = cmd;
            code = item_code;
        }

        private void BatchHelp_Load(object sender, EventArgs e)
        {
            cmd.CommandText = "ItemStock";
            cmd.Parameters.AddWithValue("@ITEM_CODE", code);
            cmd.CommandType = CommandType.StoredProcedure;
            adapter.Fill(table);
            source.DataSource = table;
            dgBatch.DataSource = source;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (dgBatch.Rows.Count > 0 && dgBatch.CurrentRow != null)
            {
                c = dgBatch.CurrentRow.Cells;
            }
        }

        private void dgBatch_DoubleClick(object sender, EventArgs e)
        {
            btnOK.PerformClick();
        }

        private void dgBatch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnOK.PerformClick();
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
                source.Filter = "BATCH LIKE '"+txtFilter.Text+"%'";
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }
    }
}
