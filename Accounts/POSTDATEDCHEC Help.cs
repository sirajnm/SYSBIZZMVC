using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sys_Sols_Inventory.Accounts
{
    public partial class POSTDATEDCHEC_Help : Form
    {
        Class.Transactions trans = new Class.Transactions();
        public POSTDATEDCHEC_Help(int i)
        {
            InitializeComponent();

            if (i == 0)
            {
                btnDelete.Enabled = false;
            }
            else
            {
                btnOK.Enabled = false;
            }

            cmd.Connection = conn;
            adapter.SelectCommand = cmd;
        }


        private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        private SqlCommand cmd = new SqlCommand();
        private SqlDataAdapter adapter = new SqlDataAdapter();
        private DataTable table = new DataTable();
        private DataTable fieldTable = new DataTable();
        private BindingSource source = new BindingSource();

        public DataGridViewCellCollection c;


        private void POSTDATEDCHEC_Help_Load(object sender, EventArgs e)
        {
            cmd.CommandText = "SELECT * FROM tbl_Checks";
            adapter.Fill(table);
            source.DataSource = table;
            dgItems.DataSource = source;

            fieldTable.Columns.Add("key");
            fieldTable.Columns.Add("value");
            cmbFilter.DataSource = fieldTable;
            cmbFilter.ValueMember = "key";
            cmbFilter.DisplayMember = "value";
            fieldTable.Rows.Add("DOC_NO", "DOC_NO");
            fieldTable.Rows.Add("CHEQUENO", "CHEQUENO");
            
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
            if (dgItems.Rows.Count > 0 && dgItems.CurrentRow != null && MessageBox.Show("Are you sure?", "Item Deletion", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = "DELETE FROM tbl_Checks WHERE DOC_NO = '" + dgItems.CurrentRow.Cells["DOC_NO"].Value + "'";
                    cmd.ExecuteNonQuery();
                    DeleteTransation();
                    MessageBox.Show("Item Deleted!");
                    dgItems.Rows.Remove(dgItems.CurrentRow);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private void DeleteTransation()
        {

            try
            {
                trans.VOUCHERTYPE = dgItems.CurrentRow.Cells["VOUCHERTYPE"].Value.ToString();
                trans.VOUCHERNO = dgItems.CurrentRow.Cells["DOC_NO"].Value.ToString();
                trans.DeletePurchaseTransaction();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
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
                source.Filter = cmbFilter.SelectedValue + " LIKE '" + txtFilter.Text + "%'";
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
