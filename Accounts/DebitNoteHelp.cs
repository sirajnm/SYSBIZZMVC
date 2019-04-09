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
    public partial class DebitNoteHelp : Form
    {
        //private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        //private SqlCommand cmd = new SqlCommand();
        //private SqlDataAdapter adapter = new SqlDataAdapter();
        private DataTable table = new DataTable();
        private BindingSource source = new BindingSource();
        public DataGridViewCellCollection c;
        private DataTable fieldTable = new DataTable();
        private string type;
        Class.Ledgers led = new Class.Ledgers();
        Class.Transactions trans = new Class.Transactions();
        Class.ModifiedTransaction modtrans = new Class.ModifiedTransaction();
        Login lg = (Login)Application.OpenForms["Login"];
        private int frm = 0;
        public DebitNoteHelp(int i,string Form)
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
            type = Form;
            this.Text = Form+" Help";

            string query = "";
            if (type == "Debit Note")
            {
                //query = "SELECT     VOUCHERNO, ACCNAME, PARTICULARS, DEBIT, CREDIT, DATED, NARRATION FROM  tb_Transactions WHERE     (VOUCHERTYPE = 'Debit Note') AND (DEBIT > 0)";
                query = "SELECT * FROM Tbl_DebitNote";
            }
            else if (type == "Credit Note")
            {
                //query = "SELECT     VOUCHERNO, ACCNAME, PARTICULARS, DEBIT, CREDIT, DATED, NARRATION FROM  tb_Transactions WHERE     (VOUCHERTYPE = 'Credit Note') AND (CREDIT > 0)";
                query = "SELECT * FROM Tbl_CreditNote";
            }
            else if (type == "Sale Invoice")
            {
                query = "Select DOC_DATE_GRE,DOC_ID,DOC_NO,SALE_TYPE,CUSTOMER_CODE,CUSTOMER_NAME_ENG,TOTAL_AMOUNT,TAX_TOTAL,NET_AMOUNT From INV_SALES_HDR ORDER BY DOC_DATE_GRE DESC";
            }
            else if (type == "Purchase Invoice")
            {
                query = "Select DOC_DATE_GRE,DOC_ID,DOC_NO,SUP_INV_NO,SUPPLIER_CODE,TAX_TOTAL,NET_VAL From INV_PURCHASE_HDR ORDER BY DOC_DATE_GRE DESC";
            }
            //adapter.Fill(table);
            table = Model.DbFunctions.GetDataTable(query);
            source.DataSource = table;
            dgItems.DataSource = source;
        }

        private void DeleteTransation(String Id)
        {
            try
            {

                trans.VOUCHERTYPE = type;

                trans.VOUCHERNO = Id;
                trans.DeletePurchaseTransaction();

            }
            catch
            {
            }


        }

        private void DebitNoteHelp_Load(object sender, EventArgs e)
        {
            
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
            string id = dgItems.CurrentRow.Cells["VOUCHERNO"].Value.ToString();
            string dat = "";
            try
            {
                dat = Convert.ToDateTime(dgItems.CurrentRow.Cells["DATED"].Value).ToString("MM/dd/yyyy");
            }
            catch { }
            dgItems.Rows.Remove(dgItems.CurrentRow);
            AddtoDeletedTransaction(id);

            modifiedtransaction(id, dat);
            DeleteTransation(id);
        }

        public void modifiedtransaction(string ID, string date)
        {
            try
            {
                modtrans.VOUCHERTYPE = type;
                modtrans.Date = date;
                modtrans.USERID = lg.EmpId;
                modtrans.VOUCHERNO = ID;
                modtrans.NARRATION = "";
                modtrans.STATUS = "Delete";
                modtrans.MODIFIEDDATE = DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss");
                modtrans.insertTransaction();
            }
            catch
            { }
        }

        public void AddtoDeletedTransaction(string id)
        {
            string vchr;
            try
            {
                vchr = type;
               // conn.Open();
                string query = "insert into     tbl_deletedTransaction(VOUCHERNO, VOUCHERTYPE, ACCNAME, PARTICULARS, DEBIT, CREDIT, DATED, NARRATION, USERID, SYSTEMTIEM, ACCID) select    VOUCHERNO, VOUCHERTYPE, ACCNAME, PARTICULARS, DEBIT, CREDIT, DATED, NARRATION, USERID, SYSTEMTIEM, ACCID from tb_Transactions where VOUCHERNO='" + id + "' and VOUCHERTYPE='" + vchr + "'";
                // cmd.CommandText = "DELETE FROM INV_PURCHASE_HDR WHERE DOC_NO = '" + id + "';DELETE FROM INV_PURCHASE_DTL WHERE DOC_NO = '" + id + "';DELETE FROM INV_STK_TRX_HDR WHERE DOC_REFERENCE = '" + id + "';DELETE FROM INV_STK_TRX_DTL WHERE DOC_REFERENCE = '" + id + "'";
                //cmd.ExecuteNonQuery();
                Model.DbFunctions.InsertUpdate(query);
                //  MessageBox.Show("Record Deleted!");
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
}
