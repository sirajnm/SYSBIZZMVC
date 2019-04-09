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
    public partial class PaymentVoucherHelp : Form
    {
        //private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        //private SqlCommand cmd = new SqlCommand();
        //private SqlDataAdapter adapter = new SqlDataAdapter();
        private DataTable table = new DataTable();
        private BindingSource source = new BindingSource();
        Class.Transactions trans = new Class.Transactions();
        Class.ModifiedTransaction modtrans = new Class.ModifiedTransaction();
        Login lg = (Login)Application.OpenForms["Login"];
        clsHelper help = new clsHelper();
        public DataGridViewCellCollection c;
        private int frm = 0;

        public PaymentVoucherHelp(int i,int form)
        {
            InitializeComponent();
            //cmd.Connection = conn;
            //adapter.SelectCommand = cmd;

            if (i == 0)
            {
                btnDelete.Enabled = true;
            }
            else
            {
                btnOK.Enabled = true;
            }

            frm = form;
        }

        private void PaymentVoucherHelp_Load(object sender, EventArgs e)
        {
            if (frm == 0)
            {

               // cmd.CommandText = "SELECT DOC_NO,CONVERT(NVARCHAR,DOC_DATE_GRE,103) AS DOC_DATE_GRE,DOC_DATE_HIJ,SUP_CODE,CONVERT(NVARCHAR,AMOUNT) AS AMOUNT,PAY_CODE,CHQ_NO,CONVERT(NVARCHAR,CHQ_DATE,103) AS CHQ_DATE,BANK_CODE,CUR_CODE,NOTES,TOTAL_PAID,TOTAL_CURRENT,TOTAL_BALANCE, DEBIT_CODE, CREDIT_CODE FROM PAY_PAYMENT_VOUCHER_HDR";
                table = help.getAllPaymentVouchers();
            }
            //if (frm == 1)
            //{
            //    cmd.CommandText = "SELECT DOC_NO,CONVERT(NVARCHAR,DOC_DATE_GRE,103) AS DOC_DATE_GRE,DOC_DATE_HIJ,EMP_CODE,CONVERT(NVARCHAR,AMOUNT) AS AMOUNT,PAY_CODE,CHQ_NO,CONVERT(NVARCHAR,CHQ_DATE,103) AS CHQ_DATE,BANK_CODE,CUR_CODE,NOTES,TOTAL_PAID,TOTAL_CURRENT,TOTAL_BALANCE FROM PAY_PAYMENT_VOUCHER_HDR WHERE EMP_CODE!='" + "" + "'";
            //}
            else
            {
               // cmd.CommandText = "SELECT DOC_NO,CONVERT(NVARCHAR,DOC_DATE_GRE,103) AS DOC_DATE_GRE,DOC_DATE_HIJ,CUST_CODE AS SUP_CODE,CONVERT(NVARCHAR,AMOUNT) AS AMOUNT,PAY_CODE,CHQ_NO,CONVERT(NVARCHAR,CHQ_DATE,103) AS CHQ_DATE,BANK_CODE,CUR_CODE,NOTES,TOTAL_PAID,TOTAL_CURRENT,TOTAL_BALANCE, DEBIT_CODE, CREDIT_CODE FROM REC_RECEIPTVOUCHER_HDR";
                table = help.getAllReceiptVouchers();
            }
            
            //adapter.Fill(table);
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

        private void DeleteTransation(string Id)
        {
            if (frm == 0)
            {
                trans.VOUCHERTYPE = "Cash Payment";
            }
            //if (frm == 1)
            //{
            //    trans.VOUCHERTYPE = "Salary Payment";
            //}
            else
            {
                trans.VOUCHERTYPE = "Cash Receipt";
            }
            trans.VOUCHERNO = Id;
            trans.DeletePurchaseTransaction();

        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgItems.CurrentRow != null)
            {
                if (MessageBox.Show("Are you sure? you want to delete this?","Record Deletion",MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string id = dgItems.CurrentRow.Cells["DOC_NO"].Value.ToString();
                    String date = Convert.ToDateTime(dgItems.CurrentRow.Cells["DOC_DATE_GRE"].Value).ToString("MM/dd/yyyy");
                    dgItems.Rows.Remove(dgItems.CurrentRow);
                  //  conn.Open();
                    help.DocNo = id;
                    if (frm == 0)
                    {
                       
                        help.deletePaymentVoucher();
                       // cmd.CommandText = "DELETE FROM PAY_PAYMENT_VOUCHER_HDR WHERE DOC_NO = '"+id+"';DELETE FROM PAY_PAYMENT_VOUCHER_DTL WHERE DOC_NO = '"+id+"'";
                    }
                    //if (frm == 1)
                    //{
                    //    cmd.CommandText = "DELETE FROM PAY_PAYMENT_VOUCHER_HDR WHERE DOC_NO = '" + id + "';DELETE FROM PAY_PAYMENT_VOUCHER_DTL WHERE DOC_NO = '" + id + "'";
                    //}
                    else
                    {
                        //cmd.CommandText = "DELETE FROM REC_RECEIPTVOUCHER_HDR WHERE DOC_NO = '" + id + "';DELETE FROM REC_RECEIPTVOUCHER_DTL WHERE DOC_NO = '" + id + "'";
                        help.deleteReceiptVoucher();
                    }
                    //cmd.ExecuteNonQuery();
                    //conn.Close();
                    AddtoDeletedTransaction(id);
                    modifiedtransaction(id, date);
                    DeleteTransation(id);
                }
            }
        }

        public void modifiedtransaction(string ID, string date)
        {
            if (frm == 0)
            {
                modtrans.VOUCHERTYPE = "Cash Payment";
            }
            //if (frm == 1)
            //{
            //    modtrans.VOUCHERTYPE = "Salary Payment";
            //}
            else
            {
                modtrans.VOUCHERTYPE = "Cash Receipt";
            }
            modtrans.Date = date;
            modtrans.USERID = lg.EmpId;
            modtrans.BRANCH = lg.Branch;
            modtrans.VOUCHERNO = ID;
            modtrans.NARRATION = "";
            modtrans.STATUS = "Delete";
            modtrans.MODIFIEDDATE = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss tt");
            modtrans.insertTransaction();
        }

        public void AddtoDeletedTransaction(string id)
        {
            string vchr;
            if (frm == 0)
            {
              //  vchr = "Cash Payment";
                modtrans.VOUCHERTYPE = "Cash Payment";
            }
            // if (frm == 1)
            //{
            //    vchr = "Salary Payment";
            //}
            else
            {
                modtrans.VOUCHERTYPE = "Cash Receipt";
               // vchr = "Cash Receipt";
            }
            modtrans.VOUCHERNO = id;
           // conn.Open();
            //cmd.CommandText = "insert into     tbl_deletedTransaction(VOUCHERNO, VOUCHERTYPE, ACCNAME, PARTICULARS, DEBIT, CREDIT, DATED, NARRATION, USERID, SYSTEMTIEM, ACCID) select    VOUCHERNO, VOUCHERTYPE, ACCNAME, PARTICULARS, DEBIT, CREDIT, DATED, NARRATION, USERID, SYSTEMTIEM, ACCID from tb_Transactions where VOUCHERNO='" + id + "' and VOUCHERTYPE='" + vchr + "'";
            //// cmd.CommandText = "DELETE FROM INV_PURCHASE_HDR WHERE DOC_NO = '" + id + "';DELETE FROM INV_PURCHASE_DTL WHERE DOC_NO = '" + id + "';DELETE FROM INV_STK_TRX_HDR WHERE DOC_REFERENCE = '" + id + "';DELETE FROM INV_STK_TRX_DTL WHERE DOC_REFERENCE = '" + id + "'";
           // cmd.ExecuteNonQuery();
            //  MessageBox.Show("Record Deleted!");
          //  conn.Close();
            modtrans.insertDeletedTransaction();

        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            if (txtFilter.Text == "")
            {
                source.Filter = "";
            }
            else
            {
                source.Filter = "DOC_NO LIKE '"+txtFilter.Text+"%'";
            }
        }
    }
}
