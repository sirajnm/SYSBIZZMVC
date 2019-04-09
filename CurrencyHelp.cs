using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sys_Sols_Inventory.Model;

namespace Sys_Sols_Inventory
{
    public partial class CurrencyHelp : Form
    {

        //private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        //private SqlCommand cmd = new SqlCommand();
        //private SqlDataAdapter adapter = new SqlDataAdapter();
        private DataTable table = new DataTable();
        private DataTable fieldTable = new DataTable();
        private BindingSource source = new BindingSource();
        clsCommon com = new clsCommon();
        public DataGridViewCellCollection c;
        public CurrencyHelp()
        {
            InitializeComponent();
        }

        private void CurrencyHelp_Load(object sender, EventArgs e)
        {
            try
            {
                //conn.Open();
                //cmd.Connection = conn;
                //cmd.CommandText = "SELECT * FROM GEN_CURRENCY";
                //adapter.SelectCommand = cmd;
                //adapter.Fill(table);
                table = com.getAllCurrency();
                source.DataSource = table;
                dgItems.DataSource = source;
            }
            catch
            {

            }
            
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (dgItems.Rows.Count > 0 && dgItems.CurrentRow != null)
            {
                c = dgItems.CurrentRow.Cells;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgItems.Rows.Count > 0 && dgItems.CurrentRow != null && MessageBox.Show("Are you sure?", "Item Deletion", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        //conn.Open();
                        //cmd.CommandText = "DELETE FROM GEN_CURRENCY WHERE CODE = '" + dgItems.CurrentRow.Cells[0].Value + "'";
                        //cmd.ExecuteNonQuery();
                        com.Code = dgItems.CurrentRow.Cells[0].Value.ToString();
                        com.deleteCurrency();
                        MessageBox.Show("Item Deleted!");
                        dgItems.Rows.Remove(dgItems.CurrentRow);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                   
                }
            }
            catch
            {
                MessageBox.Show("Sorry cant delete");
            }
        }

        private void dgItems_DoubleClick(object sender, EventArgs e)
        {
            btnOK.PerformClick();
        }

        private void dgItems_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnOK.PerformClick();
            }
        }
    }
}
