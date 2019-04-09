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

namespace Sys_Sols_Inventory.Accounts
{
    public partial class Check_Dues : Form
    {
        string docID = "";
        int selectedRowIndex = -1;
        Class.Transactions trans = new Class.Transactions();
        Login lg = (Login)Application.OpenForms["Login"];
        //private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        //private SqlCommand cmd = new SqlCommand();
        //private SqlDataAdapter adapter = new SqlDataAdapter();
        private DataTable table = new DataTable();
        int Type;
        public Check_Dues()
        {
            InitializeComponent();
        }
        public Check_Dues(int i)
        {
            InitializeComponent();
            Type = i;
           
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Escape))
            {
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void Check_Dues_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        private void Check_Dues_Load(object sender, EventArgs e)
        {
            if (Type == 1)
            {
                BINDCHEQUEDUEPAYMENTS();
            }
            else
            {
                BINDCHEQUEDUERECIPTS();
            }
        }
        public void BINDCHEQUEDUEPAYMENTS()
        {
            try
            {
                string query = "SELECT DOC_NO,REC_NO AS VOUCHER_NO,DOC_DATE_GRE AS T_DATE,SUP.LEDGERNAME AS SUP_NAME,AMOUNT,CHQ_DATE,CHQ_NO,BANK.LEDGERNAME AS BANK,NOTES,DOC_ID,POST_FLAG,CANCEL_FLAG,PAY_PAYMENT_VOUCHER_HDR.BRANCH,DEBIT_CODE,CREDIT_CODE,DESC1,DESC2 FROM PAY_PAYMENT_VOUCHER_HDR LEFT OUTER JOIN tb_ledgers AS SUP ON SUP.LEDGERID=PAY_PAYMENT_VOUCHER_HDR.DEBIT_CODE LEFT OUTER JOIN tb_ledgers AS BANK ON BANK.LEDGERID=PAY_PAYMENT_VOUCHER_HDR.CREDIT_CODE WHERE PAY_CODE = 'CHQ' AND POST_FLAG = 'N' AND CANCEL_FLAG = 'N' AND [CHQ_DATE]<@dt";
                Dictionary<string, object> Parameters = new Dictionary<string, object>();
                Parameters.Add("@dt", DateTime.Now);                
                DataTable dt = new DataTable();
                dt = DbFunctions.GetDataTable(query, Parameters);
                dgCheques.DataSource = dt;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }
        public void BINDCHEQUEDUERECIPTS()
        {
            try
            {
                string query = "SELECT DOC_NO,REC_NO AS VOUCHER_NO,DOC_DATE_GRE AS T_DATE,CUS.LEDGERNAME AS CUS_NAME,AMOUNT,CHQ_DATE,CHQ_NO,BANK.LEDGERNAME AS BANK,NOTES,DOC_ID,POST_FLAG,CANCEL_FLAG,REC_RECEIPTVOUCHER_HDR.BRANCH,DEBIT_CODE,CREDIT_CODE,DESC1,DESC2 FROM REC_RECEIPTVOUCHER_HDR LEFT OUTER JOIN tb_ledgers AS CUS ON CUS.LEDGERID=REC_RECEIPTVOUCHER_HDR.credit_CODE LEFT OUTER JOIN tb_ledgers AS BANK ON BANK.LEDGERID=REC_RECEIPTVOUCHER_HDR.DEBIT_CODE WHERE PAY_CODE = 'CHQ' AND POST_FLAG = 'N' AND CANCEL_FLAG = 'N' AND [CHQ_DATE]<@dt";
                Dictionary<string, object> Parameters = new Dictionary<string, object>();
                Parameters.Add("@dt", DateTime.Now);
                DataTable dt = new DataTable();
                dt = DbFunctions.GetDataTable(query, Parameters);
                dgCheques.DataSource = dt;
                dgCheques.DataSource = dt;


            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }
        private void btnDocNo_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void dgCheques_DoubleClick(object sender, EventArgs e)
        {
            if (dgCheques.CurrentRow != null)
            {
                selectedRowIndex = dgCheques.CurrentRow.Index;
                DataGridViewCellCollection c = dgCheques.CurrentRow.Cells;
                docID = Convert.ToString(c["DOC_ID"].Value);
                char post = Convert.ToChar(c["POST_FLAG"].Value);
                char cancel = Convert.ToChar(c["CANCEL_FLAG"].Value);
                if (cancel.Equals('Y')) 
                {
                    cmbStatus.SelectedIndex = 2;
                }
                else if (post.Equals('N'))
                {
                    cmbStatus.SelectedIndex = 0;
                }
                else
                {
                    cmbStatus.SelectedIndex = 1;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string query="";
            if (!docID.Equals(""))
            {
                bool isPosted = false;
                DataGridViewCellCollection c = dgCheques.Rows[selectedRowIndex].Cells;
                if (Type == 1)
                {
                    query = "UPDATE PAY_PAYMENT_VOUCHER_HDR SET POST_FLAG = @post_flag, CANCEL_FLAG = @cancel_flag, Transaction_Date=@dt WHERE DOC_ID = @id";
                }
                else
                {
                    query = "UPDATE REC_RECEIPTVOUCHER_HDR SET POST_FLAG = @post_flag, CANCEL_FLAG = @cancel_flag, Transaction_Date=@dt WHERE DOC_ID = @id";
                }
                Dictionary<string, object> Parameters = new Dictionary<string, object>();
                Parameters.Add("@dt", DateTime.Now);
                if (cmbStatus.SelectedIndex == 0)
                {
                    Parameters.Add("@post_flag", 'N');
                    Parameters.Add("@cancel_flag", 'N');
                    c["POST_FLAG"].Value = 'N';
                    c["CANCEL_FLAG"].Value = 'N';
                }
                else if (cmbStatus.SelectedIndex == 1)
                {
                    Parameters.Add("@post_flag", 'Y');
                    Parameters.Add("@cancel_flag", 'N');
                    c["POST_FLAG"].Value = 'Y';
                    c["CANCEL_FLAG"].Value = 'N';
                    isPosted = true;
                }
                else
                {
                    Parameters.Add("@post_flag", 'N');
                    Parameters.Add("@cancel_flag", 'Y');
                    c["POST_FLAG"].Value = 'N';
                    c["CANCEL_FLAG"].Value = 'Y';
                }
                Parameters.Add("@id", docID);
                DbFunctions.InsertUpdate(query, Parameters);


                if (isPosted)
                {
                    string doc_no = Convert.ToString(c["DOC_NO"].Value);
                    string voucher_type = "CHEQUE TRANSACTION";
                    string date = DateTime.Parse(Convert.ToString(c["T_DATE"].Value), null).ToString("MM/dd/yyyy hh:mm:ss tt");
                    string branch = Convert.ToString(c["BRANCH"].Value);
                    string narration = Convert.ToString(c["NOTES"].Value);
                    string account_id = Convert.ToString(c["DEBIT_CODE"].Value);
                    string account_name = Convert.ToString(c["DESC1"].Value);
                    string party_account_id = Convert.ToString(c["CREDIT_CODE"].Value);
                    string party_account_name = Convert.ToString(c["DESC2"].Value);
                    string amount = Convert.ToString(c["AMOUNT"].Value);
                    DoubleEntryTransaction transaction = new DoubleEntryTransaction();
                    if (Type == 1)
                    {
                        //payment
                        transaction.insertTransaction(doc_no, voucher_type, dateTimePicker1.Value.ToString("MM/dd/yyyy hh:mm:ss tt"), branch, narration, account_id, account_name, party_account_id, party_account_name, amount);
                    }
                    else
                    {
                        //receipt
                        transaction.insertTransaction(doc_no, voucher_type, dateTimePicker1.Value.ToString("MM/dd/yyyy hh:mm:ss tt"), branch, narration, account_id, account_name, party_account_id, party_account_name, amount);
                    }
                    dgCheques.Rows.Remove(dgCheques.Rows[selectedRowIndex]);
                }

                MessageBox.Show("Changes Saved!");
            }
            else
            {
                MessageBox.Show("Please select a record to edit it.");
            }
            docID = "";
            cmbStatus.SelectedIndex = 0;
            selectedRowIndex = -1;
        }


        public void receiptVoucherTransaction(string doc_no, string date, string account_id, string customer_id, string amount, string notes)
        {
            trans.VOUCHERTYPE = "Cheqeue Receipt";
            trans.DATED = date;
            trans.NARRATION = notes;
            Login log = (Login)Application.OpenForms["Login"];
            trans.USERID = log.EmpId;
            trans.ACCNAME = account_id;
            trans.PARTICULARS = customer_id;
            trans.VOUCHERNO = doc_no;
            trans.BRANCH = lg.Branch;
            trans.ACCID = account_id;
            trans.CREDIT = "0";
            trans.DEBIT = amount;
            trans.SYSTEMTIME = DateTime.Now.ToString();
            trans.insertTransaction();
            trans.PARTICULARS = account_id;
            trans.ACCID = customer_id;
            trans.DEBIT = "0";
            trans.CREDIT = amount;
            trans.SYSTEMTIME = DateTime.Now.ToString();
            trans.insertTransaction();
        }

        private void dgCheques_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgCheques.CurrentRow != null)
            {
                selectedRowIndex = dgCheques.CurrentRow.Index;
                DataGridViewCellCollection c = dgCheques.CurrentRow.Cells;
                docID = Convert.ToString(c["DOC_ID"].Value);
                char post = Convert.ToChar(c["POST_FLAG"].Value);
                char cancel = Convert.ToChar(c["CANCEL_FLAG"].Value);
                if (cancel.Equals('Y'))
                {
                    cmbStatus.SelectedIndex = 2;
                }
                else if (post.Equals('N'))
                {
                    cmbStatus.SelectedIndex = 0;
                }
                else
                {
                    cmbStatus.SelectedIndex = 1;
                }
            }
        }

        /*public void checkpaymentTransaction()
        {
            trans.VOUCHERTYPE = "Bank Payment";
            trans.DATED = TransDate.ToString("MM/dd/yyyy");
            trans.NARRATION = NOTES.Text;
            Login log = (Login)Application.OpenForms["Login"];
            trans.USERID = log.EmpId;
            trans.ACCNAME = PARTYACC.Text;
            trans.PARTICULARS = CASHACC.Text;
            trans.VOUCHERNO = DOC_NO.Text;
            trans.BRANCH = lg.Branch;
            trans.ACCID = PARTYACC.SelectedValue.ToString();
            trans.CREDIT = "0";
            trans.DEBIT = AMOUNT.Text;
            trans.SYSTEMTIEM = DateTime.Now.ToString();
            trans.insertTransaction();


            trans.PARTICULARS = PARTYACC.Text;
            trans.ACCNAME = CASHACC.Text;
            trans.VOUCHERNO = DOC_NO.Text;
            trans.ACCID = CASHACC.SelectedValue.ToString();
            trans.DEBIT = "0";
            trans.CREDIT = AMOUNT.Text;
            trans.SYSTEMTIEM = DateTime.Now.ToString();
            trans.insertTransaction();
        }

        public void paymentVoucherTransaction()
        {
            trans.VOUCHERTYPE = "Cash Payment";
            trans.DATED = TransDate.ToString("MM/dd/yyyy");
            trans.NARRATION = NOTES.Text;
            Login log = (Login)Application.OpenForms["Login"];
            trans.USERID = log.EmpId;
            trans.ACCNAME = PARTYACC.Text;
            trans.PARTICULARS = CASHACC.Text;
            trans.VOUCHERNO = DOC_NO.Text;
            trans.BRANCH = lg.Branch;
            trans.ACCID = PARTYACC.SelectedValue.ToString();
            trans.CREDIT = "0";
            trans.DEBIT = AMOUNT.Text;
            trans.SYSTEMTIEM = DateTime.Now.ToString();
            trans.insertTransaction();

            trans.PARTICULARS = PARTYACC.Text;
            trans.ACCNAME = CASHACC.Text;
            trans.VOUCHERNO = DOC_NO.Text;
            trans.ACCID = CASHACC.SelectedValue.ToString();
            trans.DEBIT = "0";
            trans.CREDIT = AMOUNT.Text;
            trans.SYSTEMTIEM = DateTime.Now.ToString();
            trans.insertTransaction();
        }
        */
    }
}
