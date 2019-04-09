using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sys_Sols_Inventory.Model;
using Sys_Sols_Inventory.Class;
namespace Sys_Sols_Inventory
{
    public partial class Accounting_Voucher : Form
    {
        AccountVoucherHdrDB accvhdrObj = new AccountVoucherHdrDB();
        RecRecieptVoucherHdrDB rcptvchrObj = new RecRecieptVoucherHdrDB();
        clsCommon cmnObj = new clsCommon();
        Initial mdi = (Initial)Application.OpenForms["Initial"];
        Login lg = (Login)Application.OpenForms["Login"];
        Class.DateSettings dset = new Class.DateSettings();
        Class.ModifiedTransaction modtrans = new Class.ModifiedTransaction();
        Class.Transactions trans = new Class.Transactions();
        Class.Ledgers led = new Class.Ledgers();
        Ledgers ldgObj = new Ledgers();
        ProjectDB ProjectDB = new ProjectDB();

        private DataTable table = new DataTable();
        private BindingSource source = new BindingSource();
        private DateTime TransDate;

        public bool edit = false;
        private bool HasArabic = true;
        private string ID = "";
        private string tableHDR = "";
        private string tableDTL = "";
        private string fld = "";
        private string title = "";
        private int form = 0;
        private bool GetStatement = false;
        private bool HasAccounts = false;
        bool ready = false;
        bool closeFrom = false;
   
        string CompanyName="", Address1, Addres1, Addres2, Phone, Fax, Email, TineNo, Billno, Date, CUSID, Website, panno, vat, logo;
        Class.CompanySetup ComSet = new Class.CompanySetup();
        public Accounting_Voucher(int i)
        {  
          
            InitializeComponent();
            bindledgers();
          //  CASHACC.SelectedValue = "21";
            form = i;
          
                tableHDR = "ACCOUNT_VOUCHER_HDR";
                tableDTL = "ACCOUNT_VOUCHER_DTL";
                fld = "SUP_CODE";
                title = "Account Voucher";
           
            this.Text = title;
           
        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {


            if (keyData == (Keys.Alt | Keys.S))
            {
                if (DialogResult.Yes == MessageBox.Show("Are sure to continue", "Confirmation", MessageBoxButtons.YesNo))
                {

                    btnSave.PerformClick();
                    // EditActive = false;
                    return true;
                }
            }


            return base.ProcessCmdKey(ref msg, keyData);
        }


        public Accounting_Voucher(int i, string NO)
        {
            //cmd.Connection = conn;
            InitializeComponent();
            try
            {
              //  cmd.Connection = conn;
                bindledgers();
                form = i;
              
                    tableHDR = "ACCOUNT_VOUCHER_HDR";
                    tableDTL = "ACCOUNT_VOUCHER_DTL";
                    fld = "SUP_CODE";
                    title = "Account Voucher";
                    DOC_NO.Text = NO;
                    GetACCID(NO, "Journal");
                    GetDataFromDocNO();
                    this.Load -= new EventHandler(PaymentVoucher_Load);
                    closeFrom = true;

                  //  this.Sales_Load(sender, e);
                this.Text = title;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        public void GetACCID(string VoucherNO,string VourcherType)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = trans.GetACCID(VoucherNO, VourcherType);
                if (dt.Rows.Count > 0)
                {
                   // MessageBox.Show(dt.Rows[1][0].ToString());
                    PARTYACC.SelectedValue = dt.Rows[0][0].ToString();
                    CASHACC.SelectedValue = dt.Rows[1][0].ToString();
                }
            }
            catch
            {
            }
        }

        public void GetDataFromDocNO()
        {
            
                ID = DOC_NO.Text;
               // cmd.CommandType = CommandType.Text;
               // cmd.CommandText = "SELECT DOC_NO,CONVERT(NVARCHAR,DOC_DATE_GRE,103) AS DOC_DATE_GRE,DOC_DATE_HIJ,AMOUNT,TOTAL_PAID,SUP_CODE,PAY_CODE,CHQ_NO FROM " + tableHDR + " WHERE DOC_NO = '" + DOC_NO.Text + "'";
                accvhdrObj.DocNo = DOC_NO.Text;
               // conn.Open();
                SqlDataReader r = accvhdrObj.getDatasFromRecNo(tableHDR);
                dgDetail.Rows.Clear();
                while (r.Read())
                {
                   
                        dgDetail.Rows.Add(r["DOC_NO"], r["DOC_DATE_GRE"], r["DOC_DATE_HIJ"], r["SUP_CODE"], r["PAY_CODE"], r["CHQ_NO"], r["AMOUNT"]);
                   
                }
                //conn.Close();
                DbFunctions.CloseConnection();
               // conn.Open();
              //  SqlCommand cmd1 = new SqlCommand();
                try
                {
                    //accvhdrObj.DocNo = ID;
                   //SqlDataReader rd = accvhdrObj.getAllDataByDocNo();
                    //while (rd.Read())
                    //{

                    //    PAY_CODE.Text = Convert.ToString(rd["PAY_CODE"]);
                    //    PAY_NAME.Text = General.getName(PAY_CODE.Text, "GEN_PAYTYPE");
                    //    CHQ_NO.Text = Convert.ToString(rd["CHQ_NO"]);
                    //    CHQ_DATE.Value = Convert.ToDateTime(rd["CHQ_DATE"]);
                    //    try
                    //    {
                    //        //DOC_DATE_GRE.Value = DateTime.ParseExact(h.c["DOC_DATE_GRE"].Value.ToString(), "dd/MM/yyyy", null);
                    //        DOC_DATE_GRE.Value = Convert.ToDateTime(rd["DOC_DATE_GRE"]);
                    //    }
                    //    catch
                    //    {

                    //    }
                    //    DOC_DATE_HIJ.Text = Convert.ToString(rd["DOC_DATE_HIJ"]);
                    //    CUR_CODE.Text = Convert.ToString(rd["CUR_CODE"]);
                    //    AMOUNT.Text = Convert.ToString(rd["AMOUNT"]);
                    //    BANK_CODE.Text = Convert.ToString(rd["BANK_CODE"]);
                    //    NOTES.Text = Convert.ToString(rd["NOTES"]);

                    //    TOTAL_PAID.Text = Convert.ToString(rd["TOTAL_PAID"]);
                    //    TOTAL_CURRENT.Text = Convert.ToString(rd["TOTAL_CURRENT"]);
                    //    TOTAL_BALANCE.Text = Convert.ToString(rd["TOTAL_BALANCE"]);
                    //}
                    DataTable dt = new DataTable();
                   string query = "select * from ACCOUNT_VOUCHER_HDR WHERE DOC_NO='" + ID + "'";
                   dt = Model.DbFunctions.GetDataTable(query);
                   for (int i = 0; i < dt.Rows.Count; i++)
                   {
                       PAY_CODE.Text = Convert.ToString(dt.Rows[i]["PAY_CODE"]);
                       PAY_NAME.Text = General.getName(PAY_CODE.Text, "GEN_PAYTYPE");
                       CHQ_NO.Text = Convert.ToString(dt.Rows[i]["CHQ_NO"]);
                       CHQ_DATE.Value = Convert.ToDateTime(dt.Rows[i]["CHQ_DATE"]);
                       try
                       {
                       DOC_DATE_GRE.Value = Convert.ToDateTime(dt.Rows[i]["DOC_DATE_GRE"]);
                       }
                      catch
                       {
                       }
                       txtVoucherNo.Text = Convert.ToString(dt.Rows[i]["REC_NO"]);
                       DOC_DATE_HIJ.Text = Convert.ToString(dt.Rows[i]["DOC_DATE_HIJ"]);
                       CUR_CODE.Text = Convert.ToString(dt.Rows[i]["CUR_CODE"]);
                       AMOUNT.Text = Convert.ToString(dt.Rows[i]["AMOUNT"]);
                       BANK_CODE.Text = Convert.ToString(dt.Rows[i]["BANK_CODE"]);
                       NOTES.Text = Convert.ToString(dt.Rows[i]["NOTES"]);
                       TOTAL_PAID.Text = Convert.ToString(dt.Rows[i]["TOTAL_PAID"]);
                       TOTAL_CURRENT.Text = Convert.ToString(dt.Rows[i]["TOTAL_CURRENT"]);
                       TOTAL_BALANCE.Text = Convert.ToString(dt.Rows[i]["TOTAL_BALANCE"]);
                   }
                   // // GetLedgerId(Convert.ToString(SUP_CODE.Text));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
               
              //  conn.Close();
                DbFunctions.CloseConnection();
            
           
    
        }
      
      

        private void btnPay_Click(object sender, EventArgs e)
        {
            CommonHelp c = new CommonHelp(0, genEnum.PayType);
            if (c.ShowDialog() == DialogResult.OK && c.c != null)
            {
                PAY_CODE.Text = Convert.ToString(c.c[0].Value);
                PAY_NAME.Text = Convert.ToString(c.c[1].Value);
            }

            if(!edit)
            {
            if (CASHACC.Text != "CASH ACCOUNT")
            {
                SelectBank();
            }
                }
        }


        public void SelectBank()
        {
            if (PAY_CODE.Text == "CHQ" | PAY_CODE.Text == "DEP")
            {
                BANK_CODE.Text = CASHACC.Text;
            }
        }
        private void btnBank_Click(object sender, EventArgs e)
        {
            //bank
        }

        private void btnCurr_Click(object sender, EventArgs e)
        {
            try
            {
                CurrencyHelp h = new CurrencyHelp();
                if (h.ShowDialog() == DialogResult.OK && h.c != null)
                {
                    CUR_CODE.Text = Convert.ToString(h.c["CODE"].Value);
                }

            }
            catch
            {
            }
        }

        private bool valid()
        {

            if (General.IsEnabled(Settings.HasAccessLimit))
            {
                DateTime date = DateTime.Now;
                DataTable dt = new DataTable();
                dt = dset.getdatdetails();
                switch (dt.Rows[0][3].ToString())
                {
                    case "Date":
                        date = Convert.ToDateTime(dt.Rows[0][1].ToString());
                        break;
                    case "Period":
                        int days = 0;
                        switch (dt.Rows[0][4].ToString())
                        {
                            case "Y":
                                days = 365 * Convert.ToInt16(dt.Rows[0][2].ToString()) * -1;
                                break;
                            case "M":
                                days = 30 * Convert.ToInt16(dt.Rows[0][2].ToString()) * -1;
                                break;
                            case "D":

                                days = Convert.ToInt16(dt.Rows[0][2].ToString()) * -1;
                                break;
                        }
                        try
                        {
                            date = DateTime.Now.AddDays(days);
                        }
                        catch
                        {
                            date = DateTime.Now;
                        }

                        break;
                }
                date = Convert.ToDateTime(date.ToShortDateString());
                if (date <= Convert.ToDateTime(DOC_DATE_GRE.Value.ToShortDateString()))
                {
                    
                }
                else
                {
                    MessageBox.Show("Date Limit Exceeded!!");
                    return false;
                }
            }
            if (CASHACC.Text == "" | PARTYACC.Text == "")
            {
                MessageBox.Show("Select Accounts for transaction");
                return false;
            }
            if (PAY_CODE.Text == "CHQ")
            {
                if (string.IsNullOrEmpty(CHQ_NO.Text))
                {
                    MessageBox.Show("Enter cheque no.");
                    return false;
                }
            }
            else if (PAY_CODE.Text == "CRD")
            {
                if (string.IsNullOrEmpty(txtAccDetails.Text))
                {
                    MessageBox.Show("Enter card no.");
                    return false;
                }
            }
            else if (PAY_CODE.Text != "CHQ" && PAY_CODE.Text != "CSH" && PAY_CODE.Text != "CRD")
            {
                if (string.IsNullOrEmpty(txtAccDetails.Text))
                {
                    MessageBox.Show("Enter account no.");
                    return false;
                }
            }
            if (Convert.ToDouble(AMOUNT.Text) <= 0||AMOUNT.Text=="")
            {
                MessageBox.Show("Please Enter Amount ");
                AMOUNT.Focus();
                return false;
            }
            else
                return true;
           
           
            

            //if (dgDetail.Rows.Count > 0)
            //{
            //    return true;
            //}
            //else
            //{
            //    MessageBox.Show("There are No Credit Purchases");
            //    return false;
            //}
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if(DialogResult.Yes==MessageBox.Show("Are you sure to continue","Alert",MessageBoxButtons.YesNo,MessageBoxIcon.Warning))
                {
                    if (valid())
                    {
                        TransDate = Convert.ToDateTime(DOC_DATE_GRE.Value);
                        string status = "Added!";
                        if (ID == "")
                        {

                            DOC_NO.Text = General.generateAccVoucherCode();
                            accvhdrObj.PayCode = PAY_CODE.Text;
                            //if (PAY_CODE.Text != "CHQ")
                            //{
                            //    cmd.CommandText = "INSERT INTO ACCOUNT_VOUCHER_HDR (DOC_NO,REC_NO,DOC_DATE_GRE,DOC_DATE_HIJ,CUR_CODE,AMOUNT,PAY_CODE,BANK_CODE,ACC_DETAILS,CHQ_NO,CHQ_DATE,CREDIT_CODE,DESC2,DEBIT_CODE,DESC1,NOTES,TOTAL_PAID,TOTAL_CURRENT,TOTAL_BALANCE) VALUES('" + DOC_NO.Text + "','" + txtVoucherNo.Text + "','" + DOC_DATE_GRE.Value.ToString("yyyy/MM/dd") + "','" + DOC_DATE_HIJ.Text + "','" + CUR_CODE.Text + "','" + Convert.ToDecimal(AMOUNT.Text) + "','" + PAY_CODE.Text + "','" + BANK_CODE.Text + "','" + txtAccDetails.Text + "','" + CHQ_NO.Text + "',NULL,'" + CASHACC.SelectedValue + "','" + CASHACC.Text + "','" + PARTYACC.SelectedValue + "','" + PARTYACC.Text + "','" + Common.sqlEscape(NOTES.Text) + "','" + TOTAL_PAID.Text + "','" + TOTAL_CURRENT.Text + "','" + TOTAL_BALANCE.Text + "')";
                            //}
                            //else if (PAY_CODE.Text == "CHQ")
                            //{
                            //    cmd.CommandText = "INSERT INTO ACCOUNT_VOUCHER_HDR (DOC_NO,REC_NO,DOC_DATE_GRE,DOC_DATE_HIJ,CUR_CODE,AMOUNT,PAY_CODE,BANK_CODE,ACC_DETAILS,CHQ_NO,CHQ_DATE,CREDIT_CODE,DESC2,DEBIT_CODE,DESC1,NOTES,TOTAL_PAID,TOTAL_CURRENT,TOTAL_BALANCE) VALUES('" + DOC_NO.Text + "','" + txtVoucherNo.Text + "','" + DOC_DATE_GRE.Value.ToString("yyyy/MM/dd") + "','" + DOC_DATE_HIJ.Text + "','" + CUR_CODE.Text + "','" + Convert.ToDecimal(AMOUNT.Text) + "','" + PAY_CODE.Text + "','" + BANK_CODE.Text + "','" + txtAccDetails.Text + "','" + CHQ_NO.Text + "','" + CHQ_DATE.Value.ToString("yyyy/MM/dd") + "','" + CASHACC.SelectedValue + "','" + CASHACC.Text + "','" + PARTYACC.SelectedValue + "','" + PARTYACC.Text + "','" + Common.sqlEscape(NOTES.Text) + "','" + TOTAL_PAID.Text + "','" + TOTAL_CURRENT.Text + "','" + TOTAL_BALANCE.Text + "')";
                            //}
                            accvhdrObj.DocNo= DOC_NO.Text;
                            accvhdrObj.RecNo=Convert.ToDecimal(txtVoucherNo.Text);
                            accvhdrObj.DocDateGre=DOC_DATE_GRE.Value;
                            accvhdrObj.DocDateHij= DOC_DATE_HIJ.Text;
                            accvhdrObj.CurCode= CUR_CODE.Text;
                            accvhdrObj.Amount= Convert.ToDecimal(AMOUNT.Text);
                            accvhdrObj.PayCode= PAY_CODE.Text;
                            accvhdrObj.BankCode= BANK_CODE.Text;
                            accvhdrObj.AccountDetails= txtAccDetails.Text;
                            accvhdrObj.ChqNo= CHQ_NO.Text;
                            //if (PAY_CODE.Text != "CHQ")
                            //    accvhdrObj.ChqDate =;
                            //else if (PAY_CODE.Text == "CHQ")
                            accvhdrObj.ChqDate = CHQ_DATE.Value;
                            accvhdrObj.CreditCode= CASHACC.SelectedValue.ToString();
                            accvhdrObj.Desc2= CASHACC.Text;
                            accvhdrObj.Debitcode= PARTYACC.SelectedValue.ToString();
                            accvhdrObj.Desc1= PARTYACC.Text;
                            accvhdrObj.Notes= Common.sqlEscape(NOTES.Text);
                            accvhdrObj.TotalPaid= Convert.ToDecimal(TOTAL_PAID.Text);
                            accvhdrObj.TotalCurrent=Convert.ToDecimal(TOTAL_CURRENT.Text);
                            accvhdrObj.TotalBalance= Convert.ToDecimal(TOTAL_BALANCE.Text) ;
                            accvhdrObj.ProjectId =Convert.ToInt32(cmb_projects.SelectedValue);
                            accvhdrObj.insertData();

                        }
                        else
                        {
                         
                            DeleteTransation();
                            status = "Updated!";
                            //if (PAY_CODE.Text == "CHQ")
                            //{

                            //    cmd.CommandText = "UPDATE " + tableHDR + " SET DOC_DATE_GRE = '" + DOC_DATE_GRE.Value.ToString("yyyy/MM/dd") + "',DOC_DATE_HIJ = '" + DOC_DATE_HIJ.Text + "',CUR_CODE = '" + CUR_CODE.Text + "',AMOUNT = '" + Convert.ToDecimal(AMOUNT.Text) + "',PAY_CODE = '" + PAY_CODE.Text + "',CREDIT_CODE = '" + CASHACC.SelectedValue.ToString() + "',DEBIT_CODE = '" + PARTYACC.SelectedValue.ToString() + "',BANK_CODE = '" + BANK_CODE.Text + "',CHQ_NO = '" + CHQ_NO.Text + "',CHQ_DATE = '" + CHQ_DATE.Value.ToString("yyyy/MM/dd") + "',NOTES = '" + Common.sqlEscape(NOTES.Text) + "',TOTAL_PAID = '" + TOTAL_PAID.Text + "',TOTAL_CURRENT = '" + TOTAL_CURRENT.Text + "',TOTAL_BALANCE = '" + TOTAL_BALANCE.Text + "' WHERE DOC_NO = '" + DOC_NO.Text + "'";
                            //}
                            //else if (PAY_CODE.Text != "CHQ")
                            //{
                            //    cmd.CommandText = "UPDATE " + tableHDR + " SET DOC_DATE_GRE = '" + DOC_DATE_GRE.Value.ToString("yyyy/MM/dd") + "',DOC_DATE_HIJ = '" + DOC_DATE_HIJ.Text + "',CUR_CODE = '" + CUR_CODE.Text + "',AMOUNT = '" + Convert.ToDecimal(AMOUNT.Text) + "',PAY_CODE = '" + PAY_CODE.Text + "',CREDIT_CODE = '" + CASHACC.SelectedValue.ToString() + "',DEBIT_CODE = '" + PARTYACC.SelectedValue.ToString() + "',BANK_CODE = '" + BANK_CODE.Text + "',CHQ_NO = '" + CHQ_NO.Text + "',NOTES = '" + Common.sqlEscape(NOTES.Text) + "',TOTAL_PAID = '" + TOTAL_PAID.Text + "',TOTAL_CURRENT = '" + TOTAL_CURRENT.Text + "',TOTAL_BALANCE = '" + TOTAL_BALANCE.Text + "' WHERE DOC_NO = '" + DOC_NO.Text + "'";
                            //}

                            accvhdrObj.DocDateGre = DOC_DATE_GRE.Value;
                            accvhdrObj.DocDateHij = DOC_DATE_HIJ.Text;
                            accvhdrObj.CurCode = CUR_CODE.Text;
                            accvhdrObj.Amount = Convert.ToDecimal(AMOUNT.Text);
                            accvhdrObj.PayCode = PAY_CODE.Text;
                            accvhdrObj.CreditCode = CASHACC.SelectedValue.ToString();
                            accvhdrObj.Debitcode = PARTYACC.SelectedValue.ToString();
                            accvhdrObj.BankCode= BANK_CODE.Text;
                            accvhdrObj.ChqNo = CHQ_NO.Text;
                            accvhdrObj.Notes = Common.sqlEscape(NOTES.Text);
                            accvhdrObj.TotalPaid = Convert.ToDecimal(TOTAL_PAID.Text);
                            accvhdrObj.TotalCurrent = Convert.ToDecimal(TOTAL_CURRENT.Text);
                            accvhdrObj.TotalBalance = Convert.ToDecimal(TOTAL_BALANCE.Text);
                            accvhdrObj.DocNo = DOC_NO.Text;
                            accvhdrObj.ProjectId = Convert.ToInt32(cmb_projects.SelectedValue);
                            accvhdrObj.updateDataByDocNo(tableHDR);
                        }
                        //cmd.Connection = conn;
                       // conn.Open();
                       // cmd.CommandType = CommandType.Text;
                       // cmd.ExecuteNonQuery();
                      //  conn.Close();
                        if (PAY_CODE.Text != "CHQ")
                        {
                            paymentVoucherTransaction();
                        }
                        MessageBox.Show("Journal Entry Added Successfully" + status);
                        if (ChekPrint.Checked == true)
                        {
                            printingrecipt();
                        }
                        btnClear.PerformClick();
                    }
                }
            }
            catch
            {

                //conn.Close();
                MessageBox.Show("Error in Saving Please Contact Developer");
            }
        }


        public void modifiedtransaction()
        {
            if (form == 0)
            {
                modtrans.VOUCHERTYPE = "Cash Payment";
            }
            else
            {
                modtrans.VOUCHERTYPE = "Cash Receipt";
            }
        

            modtrans.Date = DOC_DATE_GRE.Value.ToString("MM/dd/yyyy");
            modtrans.USERID = lg.EmpId;
            modtrans.VOUCHERNO = DOC_NO.Text;
            modtrans.NARRATION = NOTES.Text;
            modtrans.STATUS = "Update";
            modtrans.MODIFIEDDATE = DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss"); ;
            modtrans.insertTransaction();
        }

        private void DeleteTransation()
        {
            try
            {

                trans.VOUCHERTYPE = "Journal";
               
                    trans.VOUCHERNO = DOC_NO.Text;
                    trans.DeletePurchaseTransaction();
               
            }
            catch
            {
            }


        }
        public void receiptVoucherTransaction()
        {
            trans.VOUCHERTYPE = "Journal";
            trans.DATED =TransDate.ToString("MM/dd/yyyy");
            trans.NARRATION = NOTES.Text;
            Login log = (Login)Application.OpenForms["Login"];
            trans.USERID = log.EmpId;
            trans.ACCNAME = CASHACC.Text;
            trans.PARTICULARS = PARTYACC.Text;
            trans.VOUCHERNO = DOC_NO.Text;
            trans.PROJECTID = Convert.ToInt32(cmb_projects.SelectedValue);

            trans.ACCID = CASHACC.SelectedValue.ToString();
            trans.CREDIT = "0";
            trans.NARRATION = NOTES.Text;
            trans.DEBIT = AMOUNT.Text;
            trans.SYSTEMTIME = DateTime.Now.ToString();
            trans.BRANCH = lg.Branch;
            trans.insertTransaction();
           
            trans.PARTICULARS = CASHACC.Text;
            trans.ACCNAME = PARTYACC.Text;
            trans.VOUCHERNO = DOC_NO.Text;
            trans.ACCID = PARTYACC.SelectedValue.ToString();
            trans.DEBIT = "0";
            trans.CREDIT = AMOUNT.Text;
            trans.SYSTEMTIME = DateTime.Now.ToString();
            trans.BRANCH = lg.Branch;
            trans.insertTransaction();
        }



        public void paymentVoucherTransaction()
        {
            trans.VOUCHERTYPE = "Journal";
            trans.DATED = TransDate.ToString("MM/dd/yyyy");
            trans.NARRATION = NOTES.Text;
            Login log = (Login)Application.OpenForms["Login"];
            trans.USERID = log.EmpId;
            trans.SYSTEMTIME = DateTime.Now.ToString();
            trans.BRANCH = lg.Branch;
            trans.VOUCHERNO = DOC_NO.Text;
            trans.PROJECTID = Convert.ToInt32(cmb_projects.SelectedValue);

            trans.ACCNAME = PARTYACC.Text;
            trans.PARTICULARS = CASHACC.Text;
            trans.ACCID = PARTYACC.SelectedValue.ToString();
            trans.CREDIT = "0";
            trans.DEBIT =AMOUNT.Text ;
            trans.insertTransaction();

            trans.PARTICULARS = PARTYACC.Text;
            trans.ACCNAME = CASHACC.Text;
            trans.ACCID = CASHACC.SelectedValue.ToString();
            trans.DEBIT = "0" ;
            trans.CREDIT =AMOUNT.Text;
            trans.insertTransaction();
        }



        private void dgDetail_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgDetail.CurrentCell.ColumnIndex == 5)
            {
                DataGridViewTextBoxEditingControl txtBox = (e.Control as DataGridViewTextBoxEditingControl);
                txtBox.KeyPress += new KeyPressEventHandler(General.CellOnlyFloat);
                txtBox.TextChanged += new EventHandler(txtBox_TextChanged);
            }
        }

        void txtBox_TextChanged(object sender, EventArgs e)
        {
            string txt = (sender as DataGridViewTextBoxEditingControl).Text;
            double bal = Convert.ToDouble(dgDetail.CurrentRow.Cells[3].Value) - Convert.ToDouble(dgDetail.CurrentRow.Cells[4].Value);
            double current = 0;
            if (txt != "")
            {
                current = Convert.ToDouble(txt);
            }

            if (current > bal)
            {
                current = bal;
                (sender as DataGridViewTextBoxEditingControl).Text = bal.ToString();
            }

            dgDetail.CurrentRow.Cells[6].Value = bal - current;
            double totalCurr = 0;
            double totalBal = 0;
            for (int i = 0; i < dgDetail.Rows.Count; i++)
            {
                if (i != dgDetail.CurrentRow.Index && dgDetail.CurrentRow.Cells[5].Value != null)
                {
                    totalCurr = totalCurr + Convert.ToDouble(dgDetail.CurrentRow.Cells[5].Value);
                }
                totalBal = totalBal + Convert.ToDouble(dgDetail.CurrentRow.Cells[6].Value);
            }
            totalCurr = totalCurr + current;
            AMOUNT.Text = totalCurr.ToString();
            TOTAL_CURRENT.Text = totalCurr.ToString();
            TOTAL_BALANCE.Text = totalBal.ToString();
        }

        private void AMOUNT_TextChanged(object sender, EventArgs e)
        {
            //double amt = 0;
            //if (AMOUNT.Text != "" && AMOUNT.Text != ".")
            //{
            //    amt = Convert.ToDouble(AMOUNT.Text);
            //}

            //double totalBal = 0;

            //double totalCurr = 0;
            //double tBalance = 0;
            //if (dgDetail.Rows.Count > 0)
            //{
            //    DataGridViewCellCollection first = dgDetail.Rows[0].Cells;
            //    if (Convert.ToDouble(first[6].Value) >= 0)
            //    {
            //        for (int i = 1; i < dgDetail.Rows.Count; i++)
            //        {


            //            //DataGridViewCellCollection c = dgDetail.Rows[i].Cells;
            //            //c[6].Value = Convert.ToDouble(c[5].Value) + Convert.ToDouble(c[6].Value);
            //            //c[5].Value = 0;

            //        }

            //    }
            //}

            //for (int i = 0; i < dgDetail.Rows.Count; i++)
            //{
               
               
            //    DataGridViewCellCollection c = dgDetail.Rows[i].Cells;
                

            //    //double balAmt = Convert.ToDouble(c[3].Value) - Convert.ToDouble(c[4].Value);
            //    //totalBal = totalBal + balAmt;
               
            //    //if (amt > balAmt)
            //    //{
            //    //    c[5].Value = balAmt;
            //    //    c[6].Value = 0;
            //    //    amt = amt - balAmt;
            //    //    totalCurr = totalCurr + Convert.ToDouble(c[5].Value);
            //    //    tBalance = tBalance + Convert.ToDouble(c[6].Value);
            //    //}
            //    //else if (amt < balAmt)
            //    //{
            //    //    c[5].Value = amt;
            //    //    c[6].Value = balAmt - amt;

            //    //    totalCurr = totalCurr + Convert.ToDouble(c[5].Value);
            //    //    tBalance = tBalance + Convert.ToDouble(c[6].Value);
            //    //    break;
            //    //}
            //    //else if (amt == balAmt)
            //    //{
            //    //    c[5].Value = amt;
            //    //    c[6].Value = 0;

            //    //    totalCurr = totalCurr + Convert.ToDouble(c[5].Value);
            //    //    tBalance = tBalance + Convert.ToDouble(c[6].Value);
            //    //    break;
            //    //}
            //}
            ////if (amt > totalBal)
            ////{
            ////    AMOUNT.Text = totalBal.ToString();
            ////}
          

            //double total=0, balance=0;
            //for (int i = 0; i < dgDetail.Rows.Count; i++)
            //{
            //    DataGridViewCellCollection c = dgDetail.Rows[i].Cells;
            //        total = total + Convert.ToDouble(c[6].Value);
                
            ////    balance = balance + Convert.ToDouble(c[6].Value);
            //}

            //TOTAL_CURRENT.Text = total.ToString();
            //TOTAL_BALANCE.Text = balance.ToString();
            
        }


        void GetMaxPayVouch()
        {
            int maxId;
            String value;

            //cmd.CommandText = "SELECT ISNULL(MAX(CONVERT(DECIMAL(18,0),REC_NO)), 0) FROM ACCOUNT_VOUCHER_HDR";
          //  cmd.CommandType = CommandType.Text;
           // conn.Open();

            value = Convert.ToString(accvhdrObj.getMaxRecNo());
           // conn.Close();


            if (value.Equals("0"))
            {
               // cmd.CommandText = "SELECT VouchStartFrom FROM GEN_VOUCH_STARTFROM WHERE VouchTypeCode='ACC'";
              //  cmd.CommandType = CommandType.Text;
              //  conn.Open();
                txtVoucherNo.Text = Convert.ToString(accvhdrObj.getVouchStartFrom());
              //  conn.Close();
            }
            else
            {
                maxId = Convert.ToInt32(value);
                txtVoucherNo.Text = (maxId + 1).ToString();
            }
        }
        private void PaymentVoucher_Load(object sender, EventArgs e)
        {

            HasAccounts = Properties.Settings.Default.Account;
            DOC_DATE_GRE.Text = ComSet.GettDate();

            //  CASHACC.SelectedValue = "21";
            HasArabic = General.IsEnabled(Settings.Arabic);
            if (!HasArabic)
                DOC_DATE_HIJ.Enabled = false;

            PnlArabic.Visible = HasArabic;
            AMOUNT.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            PrintPage.SelectedIndex = 0;
            
            ActiveControl = DOC_DATE_GRE;
            if (!HasAccounts)
            {
                pnlacct.Visible = false;
            }
          
            GetBranchDetails();

            ready= true;
            lblChqNo.Visible = false;
            CHQ_NO.Visible = false;
            lblChqDate.Visible = false;
            CHQ_DATE.Visible = false;
            lblAccDetails.Visible = false;
            txtAccDetails.Visible = false;

            LoadProject();
            GetMaxPayVouch();
        }

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
            DataRow row1 = dt2.NewRow();
            dt2.Rows.InsertAt(row1, 0);
            CASHACC.DataSource = dt2;
            CASHACC.DisplayMember = "LEDGERNAME";
            CASHACC.ValueMember = "LEDGERID";
        }



        private void btnDoc_Click(object sender, EventArgs e)
        {
            try
            {
                edit = true;
                Accounting_Voucher_Help h = new Accounting_Voucher_Help();
                if (h.ShowDialog() == DialogResult.OK && h.c != null)
                {

                    DOC_NO.Text = h.c["VOUCHERNO"].Value.ToString();
                    
                    DOC_DATE_GRE.Text = h.c["DATED"].Value.ToString();
                    
                    string query = "SELECT DOC_NO,CONVERT(NVARCHAR,DOC_DATE_GRE,103) AS DOC_DATE_GRE,DOC_DATE_HIJ,AMOUNT,TOTAL_PAID,SUP_CODE,PAY_CODE,CHQ_NO FROM ACCOUNT_VOUCHER_HDR WHERE DOC_NO = '" + DOC_NO.Text + "'";
                    SqlDataReader r = Model.DbFunctions.GetDataReader(query);
                    while (r.Read())
                    {
                        dgDetail.Rows.Add(r["DOC_NO"], r["DOC_DATE_GRE"], r["DOC_DATE_HIJ"], r["SUP_CODE"], " ", r["PAY_CODE"], r["CHQ_NO"], Convert.ToDouble(r["AMOUNT"]).ToString());
                    }

                    //dgDetail.Rows.Add(h.c["VOUCHERNO"].Value.ToString(), h.c["DATED"].Value.ToString(), h.c["PARTICULARS"].Value.ToString(), h.c["DEBIT"].Value.ToString(), h.c["CREDIT"].Value.ToString(), h.c["NARRATION"].Value.ToString(), h.c["SUP_CODE"].Value.ToString());
                }
                 //if (fld == "SUP_CODE")
                 //   {
                 //       GetLedgerId(Convert.ToString(h.c["SUP_CODE"].Value));

                 //   }

                    

                
            }
            catch(Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }

        }


        public void GetLedgerId(string CusCode)
        {
            try
            {
                if (CusCode == "")
                {
                    if (form == 0)
                    {
                        GetACCID(DOC_NO.Text, "Cash Payment");
                    }
                    else
                    {
                        GetACCID(DOC_NO.Text, "Cash Receipt");
                    }


                }
                else
                {
                    DataTable dt = new DataTable();
                    led.CUSCODE = CusCode;
                    //  led.TABLE = "REC_CUSTOMER";
                    if (fld == "SUP_CODE")
                    {
                        dt = led.GetLedgerIdPurchase();
                    }
                    else
                    {
                        dt = led.GetLedgerId();
                    }
                    if (dt.Rows.Count > 0)
                    {
                        string a = dt.Rows[0][0].ToString();
                        // MessageBox.Show(a);
                        PARTYACC.SelectedValue = dt.Rows[0][0].ToString();
                    }
                    else
                    {
                        CASHACC.SelectedValue = 21;
                    }
                }
            }
            catch(Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
           // dgDetail.Rows.Clear();
            ID = "";
            DOC_NO.Text = "";
            PAY_CODE.Text = "CSH";
            PAY_NAME.Text = "CASH";
            CHQ_NO.Text = "";
            CHQ_DATE.Value = DateTime.Today;
            DOC_DATE_GRE.Value = DateTime.Today;
            DOC_DATE_HIJ.Text = "";
            CUR_CODE.Text = "";
            AMOUNT.Text = "";
            BANK_CODE.Text = "";
            NOTES.Text = "";
            TOTAL_PAID.Text = "0.00";
            TOTAL_CURRENT.Text = "0.00";
            TOTAL_BALANCE.Text = "0.00";
            
            edit = false;
            PARTYACC.Focus();
            bindledgers();
            GetMaxPayVouch();
            bindDayPayments();
            LoadProject();
            cmb_projects.SelectedValue = 0;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //btnClear.PerformClick();
            //PaymentVoucherHelp h = new PaymentVoucherHelp(1,form);
            //h.ShowDialog();
            if (ID != "")
            {
                if (MessageBox.Show("Are you sure? you want to delete this?", "Record Deletion", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string id = DOC_NO.Text;
                    // String date = Convert.ToDateTime(dgItems.CurrentRow.Cells["DOC_DATE_GRE"].Value).ToString("MM/dd/yyyy");
                    String date = DOC_DATE_GRE.Text;
                    //dgItems.Rows.Remove(dgItems.CurrentRow);
                    //if (conn.State == ConnectionState.Open)
                    //{
                    //    conn.Close();
                    //}
                   // conn.Open();
                  //  cmd.CommandText = "DELETE FROM ACCOUNT_VOUCHER_HDR WHERE DOC_NO = '" + id + "'";
                  //  cmd.ExecuteNonQuery();
                    //conn.Close();
                    accvhdrObj.DocNo = id;
                    accvhdrObj.deleteByDocNo();

                    //AddtoDeletedTransaction(id);
                    //modifiedtransaction(id, date);
                    //DeleteTransation(id);
                    DeleteTransation();
                    btnClear.PerformClick();
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (valid())
                {
                 
                   
                    btnSave.PerformClick();
              

                }
                else
                {
                    MessageBox.Show("No Credits to Save");
                }
            }
            catch(Exception ee)
            {
                MessageBox.Show(ee.Message.ToString());
            }
        }


        public void GetBranchDetails()
        {

            try
            {
                DataTable dt = new DataTable();
                dt = ComSet.GetCurrentBranchDetails();
                Addres1 = dt.Rows[0][1].ToString();
                Addres2 = dt.Rows[0][2].ToString();
                Phone = dt.Rows[0][3].ToString();
                Email = dt.Rows[0][4].ToString();
                Fax = dt.Rows[0][5].ToString();
                CUR_CODE.Text = dt.Rows[0]["DEFAULT_CURRENCY_CODE"].ToString();
               
            }
            catch
            {
            }
        }
        public void GetCompanyDetails()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = ComSet.getCompanyDetails();
                CompanyName = dt.Rows[0][1].ToString();
                TineNo = dt.Rows[0][8].ToString();
                CUSID = dt.Rows[0][10].ToString();
                Website = dt.Rows[0][11].ToString();
                panno = dt.Rows[0][9].ToString();
                logo = dt.Rows[0][6].ToString();
            }
            catch(Exception ee)
            {
                MessageBox.Show(ee.Message);
            }

        }
        private void printingrecipt()
        {
            try
            {
                if (CompanyName == "")
                {
                    GetCompanyDetails();
                    GetBranchDetails();
                }
                
                try
                {

                    int height = (dgDetail.Rows.Count - 1) * 23;



                    // PrintDialog printdialog = new PrintDialog();

                    if (PrintPage.SelectedIndex == 0)
                    {
                        PrintDocument printDocument = new PrintDocument();
                        printDocument.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("smallzize", 650, height + 300);

                        printDialog1.Document = printDocument;


                        printDocument.PrintPage += printDocument_PrintPage;
                        // printDocument.Print();
                        DialogResult result = printDialog1.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            printDocument.Print();


                        }
                    }
                    else
                    {
                        PrintDocument printdocumentA4 = new PrintDocument();
                        PaperSize ps = new PaperSize();
                        ps.RawKind = (int)PaperKind.A4;
                        printdocumentA4.DefaultPageSettings.PaperSize = ps;
                        printDialog1.Document = printdocumentA4;


                        printdocumentA4.PrintPage += printdocumentA4_PrintPage;
                        // printDocument.Print();
                        DialogResult result = printDialog1.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            printdocumentA4.Print();


                        }
                    }



                }
                catch
                {
                    MessageBox.Show("printing Problem");
                }

            }
            catch
            {
            }

        }

        private void printdocumentA4_PrintPage(object sender, PrintPageEventArgs e)
        {

            float xpos;
            int startx = 50;
            int starty = 30;
            int offset = 15;
            int headerstartposition = 150;

            int w = e.MarginBounds.Width / 2;
            int x = e.MarginBounds.Left;
            int y = e.MarginBounds.Top;
            Font Headerfont1 = new Font("Times New Roman", 15, FontStyle.Bold);
            Font Headerfont2 = new Font("Times New Roman", 10, FontStyle.Bold);
            Font printFont = new Font("Times New Roman", 10);
            var tabDataForeColor = Color.Black;
            int height = 100 + y;


            var txtDataWidth = e.Graphics.MeasureString(CompanyName, Headerfont1).Width;

            if (logo != null || logo != "")
            {

                System.Drawing.Image img = System.Drawing.Image.FromFile(logo);

                Point loc = new Point(20, 50);
                e.Graphics.DrawImage(img, new Rectangle(50, 50, 50, 50));
            }


            using (var sf = new StringFormat())
            {
                height += 15;
                sf.LineAlignment = StringAlignment.Center;
                sf.Alignment = StringAlignment.Center;

                xpos = e.MarginBounds.Left + (e.MarginBounds.Width / 2);

                // e.Graphics.DrawString(CompanyName, Headerfont1, new SolidBrush(tabDataForeColor), xpos, starty, sf);
                //   e.Graphics.DrawString(Addres1+", "+Addres2, Headerfont2, new SolidBrush(tabDataForeColor), xpos, starty + offset, sf);
                //  offset = offset + 20;
                //  e.Graphics.DrawString(Phone, Headerfont2, new SolidBrush(tabDataForeColor), xpos, starty + offset, sf);
                //   offset = offset + 20;
                //  e.Graphics.DrawString("Credit Note: " + DOC_NO.Text, Headerfont2, new SolidBrush(tabDataForeColor), xpos, starty + offset, sf);
                //   offset = offset + 20;


                e.Graphics.DrawString(CompanyName, Headerfont1, new SolidBrush(tabDataForeColor), headerstartposition, starty);
                offset = offset + 9;
                e.Graphics.DrawString(Addres1 + ", " + Addres2, Headerfont2, new SolidBrush(tabDataForeColor), headerstartposition, starty + offset);
                offset = offset + 20;
                e.Graphics.DrawString("Phone:".PadRight(3) + Phone, Headerfont2, new SolidBrush(tabDataForeColor), headerstartposition, starty + offset);
                offset = offset + 20;
                e.Graphics.DrawString("Email:".PadRight(3) + Email, Headerfont2, new SolidBrush(tabDataForeColor), headerstartposition, starty + offset);
                offset = offset + 20;
                e.Graphics.DrawString("Website:".PadRight(3) + Website, Headerfont2, new SolidBrush(tabDataForeColor), headerstartposition, starty + offset);
                offset = offset + 20;


                e.Graphics.DrawString("No: " + DOC_NO.Text, Headerfont2, new SolidBrush(tabDataForeColor), 600, starty + offset);
                offset = offset + 16;


                e.Graphics.DrawString("Date:" + DateTime.Now.ToString(), Headerfont2, new SolidBrush(tabDataForeColor), 600, starty + offset);
                offset = offset + 16;
                e.Graphics.DrawString(title+": ", Headerfont2, new SolidBrush(tabDataForeColor), xpos, starty + offset - 24, sf);
                offset = offset + 16;
                Pen blackPen = new Pen(Color.Black, 1);
                Point point1 = new Point(0, 185);
                Point point2 = new Point(900, 185);
                e.Graphics.DrawLine(blackPen, point1, point2);




                e.Graphics.DrawString("To:" + PARTYACC.Text, Headerfont2, new SolidBrush(tabDataForeColor), startx, starty + offset - 36);
                Font itemhead = new Font("Times New Roman", 8);
                offset = offset + 6;

                Point point3 = new Point(45, 219);
                Point point4 = new Point(760, 219);
                e.Graphics.DrawLine(blackPen, point3, point4);



                e.Graphics.DrawLine(blackPen, 45, 219, 45, 900);


               // e.Graphics.DrawLine(blackPen, 355, 219, 355, 900);
               // e.Graphics.DrawLine(blackPen, 450, 219, 450, 900);
               // e.Graphics.DrawLine(blackPen, 540, 219, 540, 900);
                e.Graphics.DrawLine(blackPen, 650, 219, 650, 900);
                e.Graphics.DrawLine(blackPen, 760, 219, 760, 900);

                e.Graphics.DrawLine(blackPen, 45, 900, 760, 900);


                string headtext = "Item".PadRight(175) + "Total";
                e.Graphics.DrawString(headtext, Headerfont2, new SolidBrush(Color.Black), startx, starty + offset + 1);

                offset = offset + 40;
                Font font = new Font("Times New Roman", 10);
                float fontheight = font.GetHeight();
                
            }

           

            e.Graphics.DrawString(Noteonreciept.Text, Headerfont2, new SolidBrush(Color.Black), startx, starty + offset + 3);
            e.Graphics.DrawString(AMOUNT.Text, Headerfont2, new SolidBrush(Color.Black), startx+630, starty + offset + 3);
            
            offset = offset + 655;
            try
            {
                string test = Spell.SpellAmount.InWrods(Convert.ToDecimal(AMOUNT.Text));

                int index = test.IndexOf("Taka");
                int l = test.Length;
                test = test.Substring(index + 4);
                offset = offset + 20;
                e.Graphics.DrawString(test, Headerfont2, new SolidBrush(Color.Black), startx, starty + offset + 3);
            }
            catch
            {
            }


         
         //   e.Graphics.DrawString("---------------------------------------", Headerfont2, new SolidBrush(Color.Black), startx + 490, starty + offset + 3);
            e.Graphics.DrawString("Total", Headerfont2, new SolidBrush(Color.Black), startx + 490, starty + offset + 3);


            e.Graphics.DrawString(Spell.SpellAmount.comma(Convert.ToDecimal(AMOUNT.Text)), Headerfont2, new SolidBrush(Color.Black), startx + 610, starty + offset + 3);
            offset = offset + 30;
            e.Graphics.DrawString("Authorized Signature", Headerfont2, new SolidBrush(Color.Black), startx, starty + offset + 3);
            
          
            offset = offset + 20;






            e.HasMorePages = false;
        }

        private void printDocument_PrintPage(object sender, PrintPageEventArgs e)
        {

            float xpos;
            int startx = 30;
            int starty = 30;
            int offset = 15;
        

                int w = e.MarginBounds.Width / 2;
                int x = e.MarginBounds.Left;
                int y = e.MarginBounds.Top;
                Font Headerfont1 = new Font("Times New Roman", 13,FontStyle.Bold);
                Font Headerfont2 = new Font("Courier New", 10, FontStyle.Bold);
                Font printFont = new Font("Courier New", 9);
                var tabDataForeColor = Color.Black;
                int height = 100 + y;


                var txtDataWidth = e.Graphics.MeasureString(CompanyName, printFont).Width;


                using (var sf = new StringFormat())
                {
                    height += 15;
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Alignment = StringAlignment.Center;

                    xpos = e.MarginBounds.Left + (e.MarginBounds.Width / 2);

                    e.Graphics.DrawString(CompanyName, Headerfont1, new SolidBrush(tabDataForeColor), xpos, starty, sf);
                    e.Graphics.DrawString(Addres1, Headerfont2, new SolidBrush(tabDataForeColor), xpos, starty + offset, sf);
                    offset = offset + 15;
                    e.Graphics.DrawString(Addres2, Headerfont2, new SolidBrush(tabDataForeColor), xpos, starty + offset, sf);
                    offset = offset + 15;
                    e.Graphics.DrawString(Phone, Headerfont2, new SolidBrush(tabDataForeColor), xpos, starty + offset, sf);
                    offset = offset + 15;
                    e.Graphics.DrawString(Email, Headerfont2, new SolidBrush(tabDataForeColor), xpos, starty + offset, sf);
                    offset = offset + 15;
                    e.Graphics.DrawString(title, Headerfont2, new SolidBrush(tabDataForeColor), xpos, starty + offset, sf);
                    offset = offset + 15;

                    e.Graphics.DrawString("--------------------------------------------------------------------------------------", printFont, new SolidBrush(Color.Black), xpos, starty + offset, sf);
                    e.Graphics.DrawString("--------------------------------------------------------------------------------------", printFont, new SolidBrush(Color.Black), xpos, starty + offset + 3, sf);

                    offset = offset + 10;
    
                    e.Graphics.DrawString("Doc no: "  + DOC_NO.Text, printFont, new SolidBrush(tabDataForeColor), startx, starty + offset);
                    offset = offset + 12;
                    e.Graphics.DrawString("Date:" + DateTime.Now.ToString(), printFont, new SolidBrush(tabDataForeColor), startx, starty + offset);
                    offset = offset + 12;
                    e.Graphics.DrawString("Pay to:" + PARTYACC.Text, printFont, new SolidBrush(tabDataForeColor), startx, starty + offset);
                    Font itemhead = new Font("Courier New", 9);
                    offset = offset + 19;
                    e.Graphics.DrawString("-----------------------------------------------------------------------------------", printFont, new SolidBrush(Color.Black), startx, starty + offset);
                    offset = offset + 19;
                                       
                    Font font = new Font("Courier New", 9);
                    float fontheight = font.GetHeight();
                    e.Graphics.DrawString(Noteonreciept.Text, printFont, new SolidBrush(Color.Black), startx, starty + offset);

                    offset = offset + 19;
                    e.Graphics.DrawString("-----------------------------------------------------------------------------------", printFont, new SolidBrush(Color.Black), startx, starty + offset);
                    offset = offset + 12;

                    e.Graphics.DrawString("Total Amount: ", printFont, new SolidBrush(Color.Black), startx+423, starty + offset);
                
                    e.Graphics.DrawString(Spell.SpellAmount.comma(Convert.ToDecimal(AMOUNT.Text)), printFont, new SolidBrush(Color.Black), startx + 530, starty + offset);


                    string AmmountWord = Spell.SpellAmount.InWrods(Convert.ToDecimal(AMOUNT.Text));

                    int index = AmmountWord.IndexOf("Taka");
                    int l = AmmountWord.Length;
                    AmmountWord = AmmountWord.Substring(index + 4);


                    e.Graphics.DrawString(AmmountWord, printFont, new SolidBrush(Color.Black), startx, starty + offset);
                
                    offset = offset + 19;
                    e.Graphics.DrawString("Authorized Signature", printFont, new SolidBrush(Color.Black), startx, starty + offset);
                    offset = offset + 12;

                 
                    


                }

                e.HasMorePages = false;

            }

        private void kryptonTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            try
            {
                if (closeFrom)
                {
                    this.Close();
                }
                else
                {
                        if (lg.Theme == "1")
                        {

                            ComponentFactory.Krypton.Navigator.KryptonPage kp = new ComponentFactory.Krypton.Navigator.KryptonPage();

                            mdi.maindocpanel.SelectedPage.Dispose();
                        }
                        else
                        {
                            this.Close();
                        }
                    }
                }
            catch
            {
                this.Close();
            }


        }

        int movetincriment = 0;
        private void AMOUNT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                NOTES.Focus();
               // btnSave.Focus();
            }
        }

        private void LoadProject()
        {
            cmb_projects.DataSource = ProjectDB.ProjectForCombo();
            cmb_projects.DisplayMember = "DESC_ENG";
            cmb_projects.ValueMember = "Id";
            cmb_projects.SelectedIndex = 0;
        }
        private void CASHACC_SelectedIndexChanged(object sender, EventArgs e)
        {
         //   SelectBank();
        }

        private void PARTYACC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CASHACC.Focus();
            }
        }

        private void CASHACC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                AMOUNT.Focus();
            }
        }

      

        private void NOTES_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (movetincriment == 0)
                {
                    btnSave.Focus();
                }
                else
                {
                    movetincriment = 0;
                }
            }
            else
            {
                movetincriment++;
            }
        }

        private void DOC_DATE_GRE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                PARTYACC.Focus();
            }
        }

        private void ChekPrint_CheckedChanged(object sender, EventArgs e)
        {
            if (ChekPrint.Checked)
            {
                try
                {
                    DataTable dt = new DataTable();
                    //conn.Open();
                  
                    //cmd.CommandType = CommandType.Text;

                    //cmd.CommandText = "SELECT * FROM SYS_SETUP";
                    //adapter.SelectCommand = cmd;
                    //adapter.Fill(dt);
                    string tableName = "SYS_SETUP";
                    dt = cmnObj.getAllFromTable(tableName);

                    if (dt.Rows.Count > 0)
                    {
                        if (fld == "SUP_CODE")
                        {
                            PrintPage.Text = Convert.ToString(dt.Rows[0]["Payment_Voucher"]);
                        }
                        else
                        {
                            PrintPage.Text = Convert.ToString(dt.Rows[0]["Receipt_Voucher"]);
                        }
                       // conn.Close();
                    }
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }
            }
        }
        public void bindDayPayments()
        {
            DataTable data = new DataTable();
           // SqlConnection recConn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
           // SqlCommand recCmd = new SqlCommand();
         //   recCmd.Connection = recConn;
          //  recConn.Open();
          //  recCmd.CommandText = "SELECT ROW_NUMBER() Over (Order by DOC_NO) as 'Sl_No',REC_NO AS 'VOUCHER_NO',DOC_NO, CONVERT(NVARCHAR(50), DOC_DATE_GRE, 103) AS DOC_DATE_GRE, DOC_DATE_HIJ,DESC1 DESC_ENG,DESC2 DESC_ENG2, AMOUNT, CHQ_NO, PAY_CODE FROM ACCOUNT_VOUCHER_HDR WHERE DOC_DATE_GRE = @DATE";
         //   recCmd.CommandType = CommandType.Text;
         //   recCmd.Parameters.Clear();
          //  recCmd.Parameters.AddWithValue("@DATE", DOC_DATE_GRE.Value);
          //  SqlDataAdapter adptr = new SqlDataAdapter(recCmd);
         //   adptr.Fill(data);
            accvhdrObj.DocDateGre = DOC_DATE_GRE.Value;
            data = accvhdrObj.bindByDate();
            //SqlDataReader r2 = recCmd.ExecuteReader();
            //dgDetail.Rows.Clear();
            dgDetail.Columns.Clear();
            dgDetail.DataSource = data;
            //while (r2.Read())
            //{
            //    dgDetail.Rows.Add(r2["DOC_NO"], r2["DOC_DATE_GRE"], r2["DOC_DATE_HIJ"], r2["DESC_ENG"], r2["PAY_CODE"], r2["CHQ_NO"], Convert.ToDouble(r2["AMOUNT"]).ToString("N2"), r2["Sl_No"], r2["DESC_ENG2"], r2["VOUCHER_NO"]);
            //}
            //recConn.Close();
          //   DbFunctions.CloseConnection();
        }
       

        public void BindDayReceipts()
        {
         
            try
            {
               // conn.Open();
               // cmd.CommandText = "SELECT        DOC_NO, DOC_DATE_GRE, DOC_DATE_HIJ, CUR_CODE, PAY_CODE, AMOUNT, CHQ_NO FROM            REC_RECEIPTVOUCHER_HDR WHERE        (DOC_DATE_GRE = @DATE)";
              //  cmd.CommandType = CommandType.Text;
             //   cmd.Parameters.Clear();
             //   cmd.Parameters.AddWithValue("@DATE", DOC_DATE_GRE.Value);
                rcptvchrObj.DocDateGre = DOC_DATE_GRE.Value;
                SqlDataReader r2 = rcptvchrObj.getDataByDocDate();
                while (r2.Read())
                {
                    dgDetail.Rows.Add(r2["DOC_NO"], r2["DOC_DATE_GRE"], r2["DOC_DATE_HIJ"], r2["CUR_CODE"], r2["PAY_CODE"], r2["CHQ_NO"], r2["AMOUNT"]);
                }
                DbFunctions.CloseConnection();
            }
            catch
            {
            }
            finally
            {
                //conn.Close();
                DbFunctions.CloseConnection();
            }
        }

        private void DOC_DATE_GRE_ValueChanged(object sender, EventArgs e)
        {
           // dgDetail.Rows.Clear();
            bindDayPayments();
           
        }

        private void PARTYACC_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    if (ready)
            //    {
            //        conn.Open();
            //        cmd.Connection = conn;
            //        cmd.CommandText = "SELECT        VOUCHERNO, DATED, PARTICULARS, DEBIT, CREDIT, NARRATION FROM        tb_Transactions WHERE    (ACCID=@ACCID)";
            //        cmd.CommandType = CommandType.Text;
            //        adapter.SelectCommand = cmd;
            //        cmd.Parameters.AddWithValue("@ACCID", PARTYACC.SelectedValue);
            //        SqlDataReader r5 = cmd.ExecuteReader();
            //        cmd.Parameters.Clear();
            //        while (r5.Read())
            //        {
            //            dgDetail.Rows.Add(r5["VOUCHERNO"], r5["DATED"], r5["PARTICULARS"], r5["DEBIT"], r5["CREDIT"], r5["NARRATION"]);
            //        }
            //    }
            //}
            //catch(Exception EE)
            //{
            //    //MessageBox.Show(EE.Message);
            //}
            //finally
            //{
            //    conn.Close();
            //    cmd.Parameters.Clear();
            //}
        
        
        }

        private void PAY_CODE_TextChanged(object sender, EventArgs e)
        {
            if (PAY_CODE.Text == "CSH")
            {
                lblChqNo.Visible = false;
                CHQ_NO.Visible = false;
                lblChqDate.Visible = false;
                CHQ_DATE.Visible = false;
                lblAccDetails.Visible = false;
                txtAccDetails.Visible = false;
              //  bindledgers();
            }
            else if (PAY_CODE.Text == "CHQ")
            {
                lblChqNo.Visible = true; ;
                CHQ_NO.Visible = true;
                lblChqDate.Visible = true;
                CHQ_DATE.Visible = true;
                lblAccDetails.Visible = false;
                txtAccDetails.Visible = false;
              ///  bindledgers();
            }
            else if (PAY_CODE.Text == "CRD")
            {
                lblChqNo.Visible = false;
                CHQ_NO.Visible = false;
                lblChqDate.Visible = false;
                CHQ_DATE.Visible = false;
                lblAccDetails.Visible = true;
                lblAccDetails.Text = "Card No.";
                txtAccDetails.Visible = true;
                BankAccount();
            }
            else
            {
                //lblChqNo.Visible = false;
                //CHQ_NO.Visible = false;
                //lblChqDate.Visible = false;
                //CHQ_DATE.Visible = false;
                //lblAccDetails.Visible = true;
                //lblAccDetails.Text = "Account No.";
                //txtAccDetails.Visible = true;
                //BankAccount();
            }
        }
        private void BankAccount()
        {
            DataTable dt = new DataTable();

           // cmd = new SqlCommand("SELECT DISTINCT LEDGERID,LEDGERNAME FROM tb_Ledgers WHERE UNDER IN (10,21,22)", conn);
           // SqlDataAdapter adapter = new SqlDataAdapter(cmd);
           // adapter.Fill(dt);
            dt = ldgObj.getDistinctLedger();
            CASHACC.DataSource = dt;
            CASHACC.DisplayMember = "LEDGERNAME";
            CASHACC.ValueMember = "LEDGERID";
        }

        private void btn_plus_Click(object sender, EventArgs e)
        {
            //cmd.CommandText = "SELECT ISNULL(MAX(CONVERT(DECIMAL(18,0),REC_NO)), 0) FROM ACCOUNT_VOUCHER_HDR";
           // cmd.CommandType = CommandType.Text;
           // conn.Open();

           // decimal value = Convert.ToDecimal(cmd.ExecuteScalar());
            //dgDetail.Rows.Clear();
            //dgDetail.DataSource = null;

            decimal value=Convert.ToDecimal(accvhdrObj.getMaxRecNo());

           // conn.Close();
            if (Convert.ToDecimal(txtVoucherNo.Text) + 1 >= ++value)
            {
                ID = "";
                btnClear.PerformClick();
                return;
            }
            else
            {
                txtVoucherNo.Text = (Convert.ToDecimal(txtVoucherNo.Text) + 1).ToString();

                //cmd = new SqlCommand("SELECT * FROM ACCOUNT_VOUCHER_HDR LEFT OUTER JOIN GEN_PAYTYPE ON ACCOUNT_VOUCHER_HDR.PAY_CODE=GEN_PAYTYPE.CODE WHERE REC_NO='" + txtVoucherNo.Text + "'", conn);
                DataTable dt = new DataTable();
                //SqlDataAdapter adptr = new SqlDataAdapter(cmd);
                //adptr.Fill(dt);
               // conn.Close();
                accvhdrObj.RecNo = Convert.ToDecimal(txtVoucherNo.Text);
                dt = accvhdrObj.getAllData();
                if (dt.Rows.Count > 0)
                {
                    DOC_NO.Text = dt.Rows[0]["DOC_NO"].ToString();
                    ID = DOC_NO.Text;
                    tableHDR = "ACCOUNT_VOUCHER_HDR";
                    tableDTL = "ACCOUNT_VOUCHER_DTL";
                    fld = "SUP_CODE";
                    title = "Account Voucher";

                    PAY_CODE.Text = dt.Rows[0]["PAY_CODE"].ToString();
                    PAY_NAME.Text = dt.Rows[0]["DESC_ENG"].ToString();
                    //   previous_paycode = PAY_CODE.Text;
                    // SUP_CODE.Text = dt.Rows[0]["DEBIT_CODE"].ToString();
                    //SUP_NAME.Text = PARTYACC.DisplayMember;
                    if (PAY_CODE.Text == "CHQ")
                    {
                        CHQ_DATE.Value = Convert.ToDateTime(dt.Rows[0]["CHQ_DATE"]);
                        CHQ_NO.Text = dt.Rows[0]["CHQ_NO"].ToString();
                    }
                    AMOUNT.Text = Convert.ToDecimal(dt.Rows[0]["AMOUNT"]).ToString("n2");
                    // prev = Convert.ToDecimal(AMOUNT.Text);
                    // prev_status = false;
                    CASHACC.SelectedValue = dt.Rows[0]["CREDIT_CODE"].ToString();
                    PARTYACC.SelectedValue = dt.Rows[0]["DEBIT_CODE"].ToString();
                    NOTES.Text = dt.Rows[0]["NOTES"].ToString();
                    if (Convert.ToInt32(dt.Rows[0]["Project_Id"].ToString()) <= 0 || dt.Rows[0]["Project_Id"] == null)
                    {
                        cmb_projects.SelectedIndex = 0;
                    }
                    else
                    {
                        cmb_projects.SelectedValue = dt.Rows[0]["Project_Id"];
                    }
                    //conn.Open();
                    //cmd = new SqlCommand("SELECT DOC_DATE_GRE FROM ACCOUNT_VOUCHER_HDR WHERE REC_NO='" + txtVoucherNo.Text + "'", conn);
                    DOC_DATE_GRE.Value = Convert.ToDateTime(accvhdrObj.getDocDateGre());
                    //conn.Close();

                }
            }
        }

        private void btn_minus_Click(object sender, EventArgs e)
        {
            //conn.Open();
           // cmd = new SqlCommand("SELECT VouchStartFrom FROM GEN_VOUCH_STARTFROM WHERE vouchtypecode='ACC'", conn);
           // string MIN_ = Convert.ToString(cmd.ExecuteScalar());
            //conn.Close();
            //dgDetail.Rows.Clear();
            //dgDetail.DataSource = null;
            string MIN_ = Convert.ToString(accvhdrObj.getVouchStartFrom());
            decimal CURRENT = Convert.ToDecimal(txtVoucherNo.Text) - 1;

            if (CURRENT < Convert.ToDecimal(MIN_))
            {

                return;
            }
            else
            {

                txtVoucherNo.Text = (Convert.ToDecimal(txtVoucherNo.Text) - 1).ToString();
              //  cmd = new SqlCommand("SELECT * FROM ACCOUNT_VOUCHER_HDR LEFT OUTER JOIN GEN_PAYTYPE ON ACCOUNT_VOUCHER_HDR.PAY_CODE=GEN_PAYTYPE.CODE WHERE REC_NO='" + txtVoucherNo.Text + "'", conn);
                DataTable dt = new DataTable();
              //  SqlDataAdapter adptr = new SqlDataAdapter(cmd);
             //   adptr.Fill(dt);
             //   conn.Close();
                accvhdrObj.RecNo =Convert.ToDecimal(txtVoucherNo.Text);
                dt = accvhdrObj.getAllData();
                if (dt.Rows.Count > 0)
                {
                    btnDelete.Enabled = true;
                    DOC_NO.Text = dt.Rows[0]["DOC_NO"].ToString();
                    ID = DOC_NO.Text;
                    tableHDR = "ACCOUNT_VOUCHER_HDR";
                    tableDTL = "ACCOUNT_VOUCHER_DTL";
                    fld = "SUP_CODE";
                    title = "Account Voucher";

                    PAY_CODE.Text = dt.Rows[0]["PAY_CODE"].ToString();
                    PAY_NAME.Text = dt.Rows[0]["DESC_ENG"].ToString();
                   // previous_paycode = PAY_CODE.Text;
                   // SUP_CODE.Text = dt.Rows[0]["DEBIT_CODE"].ToString();
                    //SUP_NAME.Text = PARTYACC.DisplayMember;
                    if (PAY_CODE.Text == "CHQ")
                    {
                        CHQ_DATE.Value = Convert.ToDateTime(dt.Rows[0]["CHQ_DATE"]);
                        CHQ_NO.Text = dt.Rows[0]["CHQ_NO"].ToString();
                    }
                    AMOUNT.Text = Convert.ToDecimal(dt.Rows[0]["AMOUNT"]).ToString("n2");
                  //  prev = Convert.ToDecimal(AMOUNT.Text);
                  //  prev_status = false;
                    CASHACC.SelectedValue = dt.Rows[0]["CREDIT_CODE"].ToString();
                    PARTYACC.SelectedValue = dt.Rows[0]["DEBIT_CODE"].ToString();
                    NOTES.Text = dt.Rows[0]["NOTES"].ToString();
                   // conn.Open();
                   // cmd = new SqlCommand("SELECT DOC_DATE_GRE FROM ACCOUNT_VOUCHER_HDR WHERE REC_NO='" + txtVoucherNo.Text + "'", conn);
                   // DOC_DATE_GRE.Value = Convert.ToDateTime(cmd.ExecuteScalar());
                    accvhdrObj.RecNo =Convert.ToDecimal(txtVoucherNo.Text);
                    DOC_DATE_GRE.Value = Convert.ToDateTime(accvhdrObj.getDocDateGre());
                    if (Convert.ToInt32(dt.Rows[0]["Project_Id"].ToString()) <= 0 || dt.Rows[0]["Project_Id"] == null)
                    {
                        cmb_projects.SelectedIndex = 0;
                    }
                    else
                    {
                        cmb_projects.SelectedValue = dt.Rows[0]["Project_Id"];
                    }
                   // conn.Close();
                }

            }
        }

        private void btn_InvSearch_LinkClicked(object sender, EventArgs e)
        {
            if (txt_SearchVou.Text != "")
            {
                try
                {
                    btnClear.PerformClick();
                    decimal maxid = Convert.ToDecimal(txtVoucherNo.Text);
                    if (maxid > Convert.ToDecimal(txt_SearchVou.Text))
                    {
                        txtVoucherNo.Text = (Convert.ToDecimal(txt_SearchVou.Text) + 1).ToString();
                        btn_minus.PerformClick();
                    }
                    else
                    {
                        btnClear.PerformClick();
                    }
                    txt_SearchVou.Clear();
                }
                catch
                {
                    MessageBox.Show("Something Wrong..!");

                }
            }
        }
        }
    }

