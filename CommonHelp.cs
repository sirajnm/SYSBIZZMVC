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
    public partial class CommonHelp : Form
    {
        //private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        //private SqlCommand cmd = new SqlCommand();
        //private SqlDataAdapter adapter = new SqlDataAdapter();
        private DataTable table = new DataTable();
        private BindingSource source = new BindingSource();
        public DataGridViewCellCollection c;
        private genEnum currentForm = genEnum.Group;
        private bool HasArabic = true;
        private string tableName = "";
        private DataTable fieldTable = new DataTable();


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            

            if (keyData == (Keys.Escape))
            {
                this.Close();
                // EditActive = false;
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public CommonHelp(int i,genEnum form)
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
            currentForm = form;
            //cmd.Connection = conn;
            //adapter.SelectCommand = cmd;
            switch (currentForm)
            {
                case genEnum.Country:
                    tableName = "GEN_COUNTRY";
                    break;
                case genEnum.City:
                    tableName = "GEN_CITY";
                    break;
                case genEnum.Type:
                    tableName = "INV_ITEM_TYPE";
                    break;
                case genEnum.Group:
                    tableName = "INV_ITEM_GROUP";
                    break;
                case genEnum.Category:
                    tableName = "INV_ITEM_CATEGORY";
                    break;
                case genEnum.TradeMark:
                    tableName = "INV_ITEM_TM";
                    break;
                case genEnum.CustomerType:
                    tableName = "REC_CUSTOMER_TYPE";
                    break;
                case genEnum.Supplier:
                    tableName = "PAY_SUPPLIER";
                    break;
                case genEnum.PayType:
                    tableName = "GEN_PAYTYPE";
                    break;
                case genEnum.Customer:
                    tableName = "REC_CUSTOMER" ;
                    break;
                case genEnum.Branch:
                    tableName = "GEN_BRANCH";
                    break;
                default:
                    break;
            }
            HasArabic = General.IsEnabled(Settings.Arabic);
        }

        private void CommonHelp_Load(object sender, EventArgs e)
        {
            string query = "";
            
            if (currentForm != genEnum.CustomerType)
            {
                if (currentForm == genEnum.Customer)
                {
                    if (HasArabic)
                        query = "SELECT  CODE,DESC_ENG,DESC_ARB,LedgerId,TELE1,MOBILE,ADDRESS_A,TIN_NO FROM " + tableName + " WHERE CUSTOMER_STATUS= 'True'";
                    else
                        query = "SELECT CODE,DESC_ENG,LedgerId,TELE1,MOBILE,ADDRESS_A,TIN_NO  FROM " + tableName + " WHERE CUSTOMER_STATUS= 'True'";
                }
                else if (currentForm == genEnum.Supplier)
                {
                    if (HasArabic)
                        query = "SELECT CODE,DESC_ENG,DESC_ARB,LedgerId,ADDRESS_A,ADDRESS_B,TELE1,MOBILE,EMAIL FROM " + tableName + " WHERE Supplier_Status='True'";
                    else
                        query = "SELECT CODE,DESC_ENG,LedgerId ,ADDRESS_A,ADDRESS_B,TELE1,MOBILE,EMAIL FROM " + tableName + " WHERE Supplier_Status='True'";
                }
                else
                {
                    if (HasArabic)
                        query = "SELECT CODE,DESC_ENG,DESC_ARB FROM " + tableName;
                    else
                        query = "SELECT CODE,DESC_ENG FROM " + tableName;
                }
               
            }
            else
            {
                if (HasArabic)
                    query = "SELECT CODE,DESC_ENG,DESC_ARB,CREDIT_LEVEL,PRICE_TYPE,DISCOUNT_TYPE FROM " + tableName;
                else
                    query = "SELECT CODE,DESC_ENG,CREDIT_LEVEL,PRICE_TYPE,DISCOUNT_TYPE FROM " + tableName;
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
            fieldTable.Rows.Add("CODE", "Code");
            fieldTable.Rows.Add("DESC_ENG", "Name");
            if(HasArabic)
            fieldTable.Rows.Add("DESC_ARB", "Arb. Name");

            DataGridViewColumnCollection c = dgItems.Columns;

            c[0].HeaderText = "Code";
            c[1].HeaderText = "Name";
            if(HasArabic)
            c[2].HeaderText = "Arb. Name";
            if (currentForm != genEnum.CustomerType && currentForm != genEnum.Customer)
            {
                int colWidth = (dgItems.Width - 100) / 2;
                c[1].Width = colWidth - 22;
                if(HasArabic)
                c[2].Width = colWidth - 22;
            }
            cmbFilter.SelectedIndex = 1;
            ActiveControl = dgItems;

            //dgItems.ClearSelection();
            
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
            else if (e.KeyCode == Keys.Down)
            {
            }
            else if (e.KeyCode == Keys.Up)
            {
            }
            else
            {
                txtFilter.Focus();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgItems.Rows.Count > 0 && dgItems.CurrentRow != null && MessageBox.Show("Are you sure?", "Item Deletion", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                   // conn.Open();
                    string query = "DELETE FROM "+tableName+" WHERE CODE = '" + dgItems.CurrentRow.Cells[0].Value + "'";
                    //cmd.ExecuteNonQuery();
                    Model.DbFunctions.InsertUpdate(query);
                    MessageBox.Show("Item Deleted!");
                    dgItems.Rows.Remove(dgItems.CurrentRow);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
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
                source.Filter = cmbFilter.SelectedValue + " LIKE '%" + txtFilter.Text + "%'";
            }
        }

        private void txtFilter_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
                {
                    dgItems.Focus();
                    dgItems.CurrentCell = dgItems.Rows[0].Cells[0];

                }
                if (e.KeyCode == Keys.Enter)
                {
                    btnOK.Focus();
                }
            }
            catch
            {
            }
        }
    }
}
