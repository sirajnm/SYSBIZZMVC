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
    public partial class Post_Dated_Checks : Form
    {
        private bool HasArabic = true;
        Class.Ledgers led = new Class.Ledgers();
        Class.Transactions trans = new Class.Transactions();
        Initial mdi = (Initial)Application.OpenForms["Initial"];
        Login lg = (Login)Application.OpenForms["Login"];


        private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        private SqlCommand cmd = new SqlCommand();
        bool Edit = false;
        string Type = "";
        string ID = "";

        string SalesMan = "";
        public void bindledgers()
        {
            DataTable dt1 = new DataTable();
            dt1 = led.Selectledger();
            DataRow row = dt1.NewRow();
            dt1.Rows.InsertAt(row, 0);
            PARTYACC.DataSource = dt1;

            PARTYACC.DisplayMember = "LEDGERNAME";
            PARTYACC.ValueMember = "LEDGERID";

            DataTable dt2 = new DataTable();
            dt2 = led.Selectledger();
            CASHACC.DataSource = dt2;
            CASHACC.DisplayMember = "LEDGERNAME";
            CASHACC.ValueMember = "LEDGERID";
        }

        public Post_Dated_Checks(int i)
        {
            InitializeComponent();
            bindledgers();
            if (i == 1)
            {
                Type = "Cash Payment";
            }
            else
            {
                Type = "Cash Receipt";
            }

           
        }




        private void Post_Dated_Checks_Load(object sender, EventArgs e)
        {
            Class.CompanySetup CompStep = new Class.CompanySetup();
            DOC_DATE_GRE.Text = CompStep.GettDate();

            SalesMan = lg.EmpId;
            //  CASHACC.SelectedValue = "21";
            HasArabic = General.IsEnabled(Settings.Arabic);
            if (!HasArabic)
            DOC_DATE_HIJ.Enabled = false;
            AMOUNT.KeyPress += new KeyPressEventHandler(General.OnlyFloat);

            //if (Type == "DEBIT_NOTE")
            //{
            //    // BindSupplier();
            //}
            //else
            //{
            //    //BindCustomer();
            //}
        }

        public string generateCheckCode()
        {
            string id = "";
            try
            {
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandType = CommandType.Text;
                string value = "9"+DateTime.Today.ToString("ddMMyy");
                string testvalue = (Convert.ToInt64(value)).ToString();
                cmd.CommandText = "SELECT MAX(VOUCHERNO) FROM tbl_Checks WHERE DOC_NO LIKE'"+testvalue+"%'";
                id = Convert.ToString(cmd.ExecuteScalar());
                conn.Close();
                if (id == "")
                {
                    id = testvalue + "001";
                    //  id = DateTime.Today.ToString("ddMMyy") + "0001";
                }
                else
                {
                    id = (Convert.ToInt64(id) + 1).ToString();
                }

            }
            catch
            {
            }
            return id;
        }


        public bool Valid()
        {
            if (PARTYACC.Text == "" || CASHACC.Text == "")
            {
                MessageBox.Show("Select Accounts");
                return false;
            }
            else if (Convert.ToDouble(AMOUNT.Text) <= 0)
            {
                MessageBox.Show("Amount should be greaterthan 0.00");
                return false;
            }
            else
            {
                return true;
            }

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Valid())
            {
                if (ID == "")
                {
                    DOC_NO.Text = generateCheckCode();
                    cmd.CommandText = "INSERT INTO tbl_Checks (DOC_NO,VOUCHERTYPE,VOUCHERNO,BANK_CASH_LEDGER,PARTYLEDGER,AMOUNT,CHEQUENO,BANKNAME,TransactionDate,ChequeDate,ClearedDate,BANKCOMMISSION,ADDEDBY,UPDATEDBY,NOTES,DATE_HIJ) VALUES('" + DOC_NO.Text + "','" + Type + "','" + DOC_NO.Text + "','" + CASHACC.SelectedValue + "','" + PARTYACC.SelectedValue + "','" + AMOUNT.Text + "','" + TXT_CHECKNO.Text + "','" + TXT_BANK.Text + "','" + DOC_DATE_GRE.Value.ToString("MM/dd/yyyy") + "','" + DTP_CHECKDATE.Value.ToString("MM/dd/yyyy") + "','" + DOC_DATE_GRE.Value.ToString("MM/dd/yyyy") + "','" + 0.00 + "','" + SalesMan + "','" + " " + "','" + NOTES.Text + "','" + DOC_DATE_HIJ.Text + "')";
                }
                else
                {


                    cmd.CommandText = "UPDATE tbl_Checks    SET BANK_CASH_LEDGER='" + CASHACC.SelectedValue + "',PARTYLEDGER='" + PARTYACC.SelectedValue + "',AMOUNT='" + AMOUNT.Text + "',CHEQUENO='" + TXT_CHECKNO.Text + "',BANKNAME='" + TXT_BANK.Text + "',TransactionDate='" + DOC_DATE_GRE.Value.ToString("MM/dd/yyyy") + "',ChequeDate='"+DTP_CHECKDATE.Value.ToString("MM/dd/yyyy")+"',ClearedDate='"+DTP_CHECKDATE.Value.ToString("MM/dd/yyyy")+"',UPDATEDBY='"+SalesMan+"',NOTES='"+NOTES.Text+"' WHERE DOC_NO='"+DOC_NO.Text+"'";




                    DeleteTransation();
                    //if (type == "SAL.CREDITNOTE")
                    //{
                    //    InsertIntoCreditTable();
                    //}

                }


             //   cmd.CommandText = cmd.CommandText.Substring(0, cmd.CommandText.Length - 10);
                try
                {
                    conn.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();
                    InsertTransaction();
                    MessageBox.Show("Successfully Posted");
                    btnClear.PerformClick();
                    conn.Close();


                }
                catch(Exception ee)
                {
                    conn.Close();
                    MessageBox.Show(ee.Message);
                }
            }
            
        }

        private void DeleteTransation()
        {

            try
            {
                trans.VOUCHERTYPE = Type;
                trans.VOUCHERNO = DOC_NO.Text;
                trans.DeletePurchaseTransaction();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }


        }
        public void InsertTransaction()
        {
            try
            {
               // DOC_NO.Text = generateCheckCode();
               
                trans.VOUCHERTYPE = Type;
                trans.DATED = DOC_DATE_GRE.Value.ToString("MM/dd/yyyy");
                trans.NARRATION = NOTES.Text;
                Login log = (Login)Application.OpenForms["Login"];
                trans.USERID = log.EmpId;
                if (Type == "Cash Payment")
                {
                    trans.ACCNAME = PARTYACC.Text;
                    trans.PARTICULARS = CASHACC.Text; 
                    trans.ACCID = PARTYACC.SelectedValue.ToString();
                }
                else
                {
                    trans.ACCNAME = CASHACC.Text;
                    trans.PARTICULARS = PARTYACC.Text;
                    trans.ACCID = CASHACC.SelectedValue.ToString();
                }
                trans.VOUCHERNO = DOC_NO.Text;
                trans.CREDIT = "0";
                trans.NARRATION = NOTES.Text;
                trans.DEBIT = AMOUNT.Text;
                trans.SYSTEMTIME = DateTime.Now.ToString();
                trans.insertTransaction();




                //changing here

              
               
                trans.VOUCHERNO = DOC_NO.Text;

                if (Type == "Cash Payment")
                {
                    trans.ACCNAME = CASHACC.Text;
                    trans.PARTICULARS = PARTYACC.Text;
                    trans.ACCID = CASHACC.SelectedValue.ToString();
                }
                else
                {   
                    trans.ACCNAME = PARTYACC.Text;
                    trans.PARTICULARS = CASHACC.Text;
                    trans.ACCID = PARTYACC.SelectedValue.ToString();
                }

               


                trans.DEBIT = "0";
                trans.CREDIT = AMOUNT.Text;
                trans.SYSTEMTIME = DateTime.Now.ToString();
                trans.insertTransaction();
           
            }
            catch
            {
            }
        }

        private void btnDoc_Click(object sender, EventArgs e)
        {
            POSTDATEDCHEC_Help h = new POSTDATEDCHEC_Help(0);
            if (h.ShowDialog() == DialogResult.OK && h.c != null)
            {
                ID = Convert.ToString(h.c["DOC_NO"].Value);
                DOC_NO.Text = Convert.ToString(h.c["DOC_NO"].Value);
               DOC_DATE_GRE.Text=  Convert.ToString(h.c["TransactionDate"].Value);
              DOC_DATE_HIJ.Text = Convert.ToString(h.c["DATE_HIJ"].Value);
              CASHACC.SelectedValue= Convert.ToString(h.c["BANK_CASH_LEDGER"].Value);
                PARTYACC.SelectedValue = Convert.ToString(h.c["PARTYLEDGER"].Value);
               TXT_BANK.Text = Convert.ToString(h.c["BANKNAME"].Value);
                TXT_CHECKNO.Text = Convert.ToString(h.c["CHEQUENO"].Value);
              DTP_CHECKDATE.Text = Convert.ToString(h.c["ChequeDate"].Value);
               AMOUNT.Text = Convert.ToString(h.c["AMOUNT"].Value);
               NOTES.Text= Convert.ToString(h.c["NOTES"].Value);
               

            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            TXT_CHECKNO.Text = "";
            TXT_BANK.Text = "";
            AMOUNT.Text = "0.00";
            NOTES.Text = "";
            DOC_NO.Text = "";
            PARTYACC.SelectedIndex = -1;
            CASHACC.SelectedIndex = -1;
            DTP_CHECKDATE.Value = DateTime.Now;
            DOC_DATE_GRE.Value = DateTime.Now;
            ID = "";
            

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            btnClear.PerformClick();
            POSTDATEDCHEC_Help h = new POSTDATEDCHEC_Help(1);
            h.ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            try
            {
                if (lg.Theme == "1")
                {

                    ComponentFactory.Krypton.Navigator.KryptonPage kp = new ComponentFactory.Krypton.Navigator.KryptonPage();

                    mdi.maindocpanel.SelectedPage.Dispose();
                }
                else
                {
                    this.Close();
                    //ComponentFactory.Krypton.Navigator.KryptonPage kp = new ComponentFactory.Krypton.Navigator.KryptonPage();

                    //mdi.maindocpanel.SelectedPage.Dispose();
                }


            }
            catch
            {
                this.Close();
            }

        }
    }
}
