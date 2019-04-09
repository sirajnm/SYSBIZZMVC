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
    public partial class OpenStockEntryHelp : Form
    {
        //private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        //private SqlCommand cmd = new SqlCommand();
        //private SqlDataAdapter adapter = new SqlDataAdapter();
        private DataTable table = new DataTable();
        private BindingSource source = new BindingSource();
        public DataGridViewCellCollection c;
        private DataTable fieldTable = new DataTable();
        private string type;
        clsHelper help = new clsHelper();
        public OpenStockEntryHelp(int i,string docType)
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
            type = docType;
        }

        private void OpenStockEntryHelp_Load(object sender, EventArgs e)
        {
           
           ////cmd.CommandText = "SELECT DOC_NO,CONVERT(NVARCHAR,DOC_DATE_GRE,103) AS DOC_DATE_GRE,DOC_DATE_HIJ,DOC_REFERENCE,NOTES,CONVERT(NVARCHAR,TOTAL_AMOUNT) AS TOTAL_AMOUNT,TAX_AMOUNT,BRANCH_OTHER FROM INV_STK_TRX_HDR WHERE DOC_TYPE = '"+type+"'";
           // cmd.CommandText = "SELECT    INV_STK_TRX_HDR.DOC_NO,CONVERT (NVARCHAR, INV_STK_TRX_HDR.DOC_DATE_GRE,103) AS DOC_DATE_GRE,INV_STK_TRX_HDR.DOC_DATE_HIJ,INV_STK_TRX_HDR.DOC_REFERENCE,INV_STK_TRX_HDR.NOTES,INV_STK_TRX_HDR.TOTAL_AMOUNT,INV_STK_TRX_HDR.TAX_AMOUNT,BRANCH,BRANCH_OTHER FROM INV_STK_TRX_HDR  WHERE INV_STK_TRX_HDR.DOC_TYPE= '" + type + "'";
           // // cmd.CommandText = "SELECT  INV_STK_TRX_DTL.ITEM_DESC_ENG,  INV_STK_TRX_HDR.DOC_NO,CONVERT (NVARCHAR, INV_STK_TRX_HDR.DOC_DATE_GRE,103) AS DOC_DATE_GRE,INV_STK_TRX_HDR.DOC_DATE_HIJ,INV_STK_TRX_HDR.DOC_REFERENCE,INV_STK_TRX_HDR.NOTES,INV_STK_TRX_HDR.TOTAL_AMOUNT FROM INV_STK_TRX_HDR INNER JOIN  INV_STK_TRX_DTL ON INV_STK_TRX_HDR.DOC_NO=INV_STK_TRX_DTL.DOC_NO WHERE INV_STK_TRX_DTL.DOC_TYPE='"+type+"'";
           // adapter.Fill(table);
            help.Type = type;
            table = help.getAllOpenigVouchers();
            source.DataSource = table;
            dgItems.DataSource = source;

            fieldTable.Columns.Add("key");
            fieldTable.Columns.Add("value");
            cmbFilter.DataSource = fieldTable;
            cmbFilter.ValueMember = "key";
            cmbFilter.DisplayMember = "value";
           // fieldTable.Rows.Add("ITEM_DESC_ENG", "ITEM_NAME");
           // fieldTable.Rows.Add("DOC_NO", "Doc. No.");
            fieldTable.Rows.Add("DOC_DATE_GRE", "Date[GRE]");
            fieldTable.Rows.Add("DOC_DATE_HIJ", "Date[HIJ]");
            fieldTable.Rows.Add("DOC_REFERENCE", "Doc. Ref.");
            fieldTable.Rows.Add("NOTES", "Notes");
            fieldTable.Rows.Add("TOTAL_AMOUNT", "Total Amount");
            fieldTable.Rows.Add("Name", "name");
            DataGridViewColumnCollection c = dgItems.Columns;
           // c["ITEM_DESC_ENG"].HeaderText = "ITEM_NAME";
           // c["DOC_NO"].HeaderText = "Doc. No.";
            c["DOC_DATE_GRE"].HeaderText = "Date[GRE]";
            c["DOC_DATE_HIJ"].HeaderText = "Date[HIJ]";
            c["DOC_REFERENCE"].HeaderText = "Doc. Ref.";
            c["NOTES"].HeaderText = "Notes";
            c["TOTAL_AMOUNT"].HeaderText = "Total Amount";

            if (type != "INV.STK.TRX")
            {
             //  c["BRANCH_OTHER"].Visible = false;
            }
            //cmd.CommandText = "Select ITEM_DESC_ENG FROM INV_STK_TRX_DTL WHERE DOC_TYPE = '" + type + "'";
            //adapter.Fill(table);
            //source.DataSource = table;
            //dgItems.DataSource = source;
            //cmbFilter.DataSource = fieldTable;
            //fieldTable.Rows.Add("ITEM_DESC_ENG", "ITEM_NAME");
            // c["ITEM_DESC_ENG"].HeaderText = "ITEM_NAME";
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
            if (dgItems.Rows.Count > 0 && dgItems.CurrentRow != null && MessageBox.Show("Are you sure?", "Stock Deletion", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    help.DocNo = dgItems.CurrentRow.Cells[0].Value.ToString();
                    //conn.Open();
                    //cmd.CommandText = "DELETE FROM INV_STK_TRX_HDR WHERE DOC_NO = '" + dgItems.CurrentRow.Cells[0].Value + "';DELETE FROM INV_STK_TRX_DTL WHERE DOC_NO = '" + dgItems.CurrentRow.Cells[0].Value + "'";
                    //cmd.ExecuteNonQuery();
                    help.deleteOpeningStockVoucher();
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
                source.Filter = cmbFilter.SelectedValue + " LIKE '" + txtFilter.Text + "%'";
            }
        }
    }
}
