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
    public partial class ItemMasterHelp : Form
    {
        //private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        //private SqlCommand cmd = new SqlCommand();
        //private SqlDataAdapter adapter = new SqlDataAdapter();        
        private DataTable table = new DataTable();
        private DataTable fieldTable = new DataTable();
        private BindingSource source = new BindingSource();
        clsHelper help = new clsHelper();
        public DataGridViewCellCollection c;
        bool hasPrBatch = false;
        public bool IsService = false; 
        public ItemMasterHelp(int i)
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

            //cmd.Connection = conn;
            //adapter.SelectCommand = cmd;
        }
        public ItemMasterHelp(int i,bool hasPriceBatch)
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
            hasPrBatch = hasPriceBatch;
            //cmd.Connection = conn;
            //adapter.SelectCommand = cmd;
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {

            if (keyData == (Keys.Escape))
            {
                 this.Close();            
            }
            //  else if (e.KeyCode == Keys.S && e.Control)
         
            return base.ProcessCmdKey(ref msg, keyData);

        }
        private void ItemMasterHelp_Load(object sender, EventArgs e)
        {           

            help.IsService = IsService;
            if (!hasPrBatch)
            {
                table.Clear();
                SqlCommand cmd = new SqlCommand();
                //cmd.Connection = conn;

                //cmd.CommandText = "ItemSuggestion";
                // cmd.CommandText = "itemSuggestion_test";
                //cmd.CommandText = "item_suggestionForReturn";
                //cmd.CommandType = CommandType.StoredProcedure;
                //adapter.SelectCommand = cmd;
                //adapter.Fill(table);
                //cmd.CommandType = CommandType.Text;
              
                table = help.getAllItemDetails();
                source.DataSource = table;
                dgItems.DataSource = source;
                fieldTable.Columns.Add("key");
                fieldTable.Columns.Add("value");
                cmbFilter.DataSource = fieldTable;
                cmbFilter.ValueMember = "key";
                cmbFilter.DisplayMember = "value";

                fieldTable.Rows.Add("ITEM_NAME", "Eng. Name");
                //fieldTable.Rows.Add("DESC_ENG", "Eng. Name");
                fieldTable.Rows.Add("ITEM_CODE", "Code");
                fieldTable.Rows.Add("DESC_ARB", "Arb. Name");
                fieldTable.Rows.Add("TYPE", "Type Code");
                fieldTable.Rows.Add("GROUP", "Group Code");
                fieldTable.Rows.Add("CATEGORY", "Category Code");
                fieldTable.Rows.Add("TRADEMARK", "Trademark Code");
                fieldTable.Rows.Add("LOCATION", "location");

            }
            else
            {
                string query = "";
                if (IsService)
                {
                    query = "SELECT * FROM itemDirectoryWithServ";
                }
                else
                {
                   query = "SELECT * FROM itemDirectory";
                }
                //adapter.Fill(table);
                table = Model.DbFunctions.GetDataTable(query);
                source.DataSource = table;
                dgItems.DataSource = source;
                fieldTable.Columns.Add("key");
                fieldTable.Columns.Add("value");
                cmbFilter.DataSource = fieldTable;
                cmbFilter.ValueMember = "key";
                cmbFilter.DisplayMember = "value";

                fieldTable.Rows.Add("DESC_ENG", "Eng. Name");
                fieldTable.Rows.Add("CODE", "Code");
                fieldTable.Rows.Add("DESC_ARB", "Arb. Name");
                fieldTable.Rows.Add("TYPE", "Type Code");
                fieldTable.Rows.Add("GROUP", "Group Code");
                fieldTable.Rows.Add("CATEGORY", "Category Code");
                fieldTable.Rows.Add("TRADEMARK", "Trademark Code");
                fieldTable.Rows.Add("LOCATION", "location");
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (dgItems.Rows.Count > 0 && dgItems.CurrentRow != null)
            {
                c = dgItems.CurrentRow.Cells;
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgItems.Rows.Count > 0 && dgItems.CurrentRow != null && MessageBox.Show("Are you sure?","Item Deletion",MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    help.Code = dgItems.CurrentRow.Cells[0].Value.ToString();
                    //conn.Open();
                    //cmd.CommandText = "DELETE FROM INV_ITEM_DIRECTORY WHERE CODE = '"+dgItems.CurrentRow.Cells[0].Value+"'";
                    //cmd.ExecuteNonQuery();
                    //cmd.CommandText="DELETE FROM INV_ITEM_DIRECTORY_PICTURE WHERE CODE = '"+dgItems.CurrentRow.Cells[0].Value+"'";
                    //cmd.ExecuteNonQuery();
                    help.deleteItem();
                    MessageBox.Show("Item Deleted!");
                    dgItems.Rows.Remove(dgItems.CurrentRow);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                   // conn.Close();
                }
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
                try
                {
                    source.Filter = cmbFilter.SelectedValue + " LIKE '%" + txtFilter.Text.Replace("'", "''") + "%'";
                }
                catch (Exception ex) { string s = ex.Message; }
            }
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void txtFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                btnOK.Focus();
            }
        }
    }
}
