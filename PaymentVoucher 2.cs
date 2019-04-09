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
using System.Globalization;
using Sys_Sols_Inventory.Model;
using Sys_Sols_Inventory.Class;
using System.Text.RegularExpressions;

namespace Sys_Sols_Inventory
{
    public partial class PaymentVoucher2 : Form
    {
        #region properties declaration
        private bool editMode = false;
        Class.ModifiedTransaction modtrans = new Class.ModifiedTransaction();
        //private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
       // private SqlCommand cmd = new SqlCommand();
        Class.Ledgers led = new Class.Ledgers();
        Class.Transactions trans = new Class.Transactions();
        private DateTime TransDate;
        public bool edit = false;
        clsCommon cmnObj = new clsCommon();

        Class.DateSettings dset = new Class.DateSettings();
        //private SqlDataAdapter adapter = new SqlDataAdapter();
        private DataTable table = new DataTable();
        private BindingSource source = new BindingSource();
        Initial mdi = (Initial)Application.OpenForms["Initial"];
        Login lg = (Login)Application.OpenForms["Login"];
        public string previous_paycode = "";
        private String decimalFormat = "0.00";
        private bool HasArabic = true;
        private string ID = "";
        private string tableHDR = "";
        private string tableDTL = "";
        private string fld = "";
        private string title = "";
        private int form = 0;
        private bool GetStatement = false;
        private bool HasAccounts = false;
        decimal prev = 0;
        bool prev_status = false;
        string CompanyName = "", Address1, Addres1, Addres2, Phone, Fax, Email, TineNo, Billno, Date, CUSID, Website, panno, vat, logo;
        #endregion

        Class.CompanySetup ComSet = new Class.CompanySetup();
        PayPaymentVoucherHdrDB payvchrhdrdb = new PayPaymentVoucherHdrDB();
        PaySupplierDB paysupdb = new PaySupplierDB();
        clsCustomer clsCus = new clsCustomer();
        RecRecieptVoucherHdrDB rrvhdb = new RecRecieptVoucherHdrDB();
        TblDeletedTransactionDB dltdTranObj = new TblDeletedTransactionDB();
        Ledgers ldgObj = new Ledgers();
        ProjectDB ProjectDB = new ProjectDB();
        Company cmp = Common.getCompany();
        public PaymentVoucher2(int i)
        {
            InitializeComponent();
            
             
            btnDelete.Enabled = false;
            DOC_DATE_GRE.Text = DateTime.Now.ToString();
          //  cmd.Connection = conn;
            bindledgers();
            CASHACC.SelectedValue = "21";
            form = i;
            if (i == 0)
            {
                tableHDR = "PAY_PAYMENT_VOUCHER_HDR";
                tableDTL = "PAY_PAYMENT_VOUCHER_DTL";
                fld = "SUP_CODE";
                title = "Payment Voucher";
             
            }
            else
            {
                tableHDR = "REC_RECEIPTVOUCHER_HDR";
                tableDTL = "REC_RECEIPTVOUCHER_DTL";
                fld = "CUST_CODE";
                kryptonLabel2.Text = "Customer";
                kryptonLabel6.Text = "Recieved Amt";
                Noteonreciept.Text = "Amount Recieved Against";
                kryptonLabel12.Text = "Total Recieved";
                kryptonLabel13.Text = "Total Amount On Sale";
                kryptonLabel7.Text = "Reciept Type:";
                title = "Receipt Voucher";
                dgDetail.Columns[4].HeaderText = "RECIEVE CODE";
                dgDetail.Columns[3].HeaderText = "CUSTOMER CODE";
            }
            this.Text = title;
            decimalFormat = Common.getDecimalFormat();
        }
        private void GetAllControl(Control c, List<Control> list)
        {
            foreach (Control control in c.Controls)
            {
                list.Add(control);

                if (control.GetType() == typeof(Panel))
                    GetAllControl(control, list);
            }
        }
        List<Control> list = new List<Control>();
      
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Escape))
            {
                //   this.Close();
                //ComponentFactory.Krypton.Docking.KryptonDockableNavigator n = mdi.sender as ComponentFactory.Krypton.Docking.KryptonDockableNavigator;
                ComponentFactory.Krypton.Navigator.KryptonPage k = new ComponentFactory.Krypton.Navigator.KryptonPage();
                k = mdi.maindocpanel.SelectedPage;
                if (k.Name == "Home")
                {


                }
                else
                {
                    mdi.maindocpanel.Pages.Remove(k);
                }
            }

            if (keyData == (Keys.Alt | Keys.S))
            {
                if (DialogResult.Yes == MessageBox.Show("Are sure to continue", "Confirmation", MessageBoxButtons.YesNo))
                {

                    btnSave.PerformClick();
                    // EditActive = false;
                    return true;
                }
            }
            if(keyData==(Keys.Alt|Keys.P))
            {
                if (ChekPrint.Checked == true)
                {
                    printingrecipt();
                }
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }


        public PaymentVoucher2(int i,string NO)
        {

            editMode = true;
            InitializeComponent();
            btnDelete.Enabled = false;
            //cmd.Connection = conn;
            bindledgers();
            form = i;
            // CASHACC.SelectedValue = "21";
            if (i == 0)
            {
                tableHDR = "PAY_PAYMENT_VOUCHER_HDR";
                tableDTL = "PAY_PAYMENT_VOUCHER_DTL";
                fld = "SUP_CODE";
                title = "Payment Voucher";
                DOC_NO.Text = NO;
                GetACCID(NO, "Cash Payment");
                GetDataFromDocNO();
                btnDelete.Enabled = true;
            }
            else
            {
                tableHDR = "REC_RECEIPTVOUCHER_HDR";
                tableDTL = "REC_RECEIPTVOUCHER_DTL";
                fld = "CUST_CODE";
                kryptonLabel2.Text = "Customer Code:";
                title = "Receipt Voucher";
                DOC_NO.Text = NO;
                GetACCIDDD(NO, "Cash Receipt");
                GetDataFromDocNO();
                btnDelete.Enabled = true;
            }
            this.Text = title;
            decimalFormat = Common.getDecimalFormat();
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
                    CASHACC.SelectedValue = "21";
                }
            }
            catch
            {
            }
        }
        public void GetACCIDDD(string VoucherNO, string VourcherType)
        {
            DataTable dt = new DataTable();
            dt = trans.GetACCID(VoucherNO, VourcherType);
            if (dt.Rows.Count > 0)
            {
                //  MessageBox.Show(dt.Rows[1][0].ToString());
                PARTYACC.SelectedValue = dt.Rows[1][0].ToString();
                CASHACC.SelectedValue = "21";
            }
        }
        public void GetDataFromDocNO()
        {
            ID = DOC_NO.Text;


            //cmd.CommandType = CommandType.Text;
           // conn.Open();
            SqlDataReader r;

            if (fld == "SUP_CODE")
            {
                //cmd.CommandText = "SELECT DOC_NO,CONVERT(NVARCHAR,DOC_DATE_GRE,103) AS DOC_DATE_GRE,DOC_DATE_HIJ,AMOUNT,TOTAL_PAID,SUP_CODE,PAY_CODE,CHQ_NO FROM " + tableHDR + " WHERE DOC_NO = '" + DOC_NO.Text + "'";
                payvchrhdrdb.FormType = tableHDR;
                payvchrhdrdb.DocNo = DOC_NO.Text;
                 r = payvchrhdrdb.getDataByDocNo();

            }
            else
            {
                //cmd.CommandText = "SELECT DOC_NO,CONVERT(NVARCHAR,DOC_DATE_GRE,103) AS DOC_DATE_GRE,DOC_DATE_HIJ,AMOUNT,TOTAL_PAID,CUST_CODE,PAY_CODE,CHQ_NO FROM " + tableHDR + " WHERE DOC_NO = '" + DOC_NO.Text + "'";
                payvchrhdrdb.FormType = tableHDR;
                payvchrhdrdb.DocNo = DOC_NO.Text;
                 r = payvchrhdrdb.getDataByDocNoCus();

            }
           // conn.Open();
            //SqlDataReader r = cmd.ExecuteReader();
            //dgDetail.Rows.Clear();
            while (r.Read())
            {

                if (fld == "SUP_CODE")
                {
                    dgDetail.Rows.Add(r["DOC_NO"], r["DOC_DATE_GRE"], r["DOC_DATE_HIJ"], r["SUP_CODE"], r["PAY_CODE"], r["CHQ_NO"], Convert.ToDouble(r["AMOUNT"]).ToString(decimalFormat));
                }
                else
                {
                    dgDetail.Rows.Add(r["DOC_NO"].ToString(), r["DOC_DATE_GRE"].ToString(), r["DOC_DATE_HIJ"].ToString(), r["CUST_CODE"].ToString(), r["PAY_CODE"].ToString(), r["CHQ_NO"].ToString(), Convert.ToDouble(r["AMOUNT"]).ToString(decimalFormat));
                }
            }
           // r.Close();
            DbFunctions.CloseConnection();
            SqlCommand cmd1 = new SqlCommand();
            if (fld == "SUP_CODE")
            {
               // cmd1.CommandText = "select DOC_ID, BRANCH, DOC_NO, REC_NO, CONVERT(NVARCHAR(50), DOC_DATE_GRE, 103) AS DOC_DATE_GRE, DOC_DATE_HIJ, SUP_CODE, EMP_CODE, AMOUNT, RDOCNO, PAY_CODE, CHQ_NO, CHQ_DATE, BANK_CODE, ACC_DETAILS, DEBIT_CODE, DESC1, CREDIT_CODE, DESC2, CUR_CODE, RATES, NOTES, USER_CODE, POST_FLAG, MODI_TIMES, PRINTED_TIME, DIST_TYPE, CANCEL_FLAG, DEBIT_CODE_2, DEBIT_CODE_2_AMT, TOTAL_PAID, TOTAL_CURRENT, TOTAL_BALANCE from PAY_PAYMENT_VOUCHER_HDR WHERE DOC_NO='" + ID + "'";
               // cmd1.Connection = conn;
                payvchrhdrdb.DocNo = ID;
                //SqlDataReader rd = cmd1.ExecuteReader();
                DataTable rd = payvchrhdrdb.getsupPayVoucherData();
                for (int i = 0; i < rd.Rows.Count;i++ )
                {
                    //  DOC_DATE_GRE.Value = DateTime.ParseExact(Convert.ToString(rd["DOC_DATE_GRE"]), "MM/dd/yyyy", null);
                    //  DOC_DATE_GRE.Value = DateTime.ParseExact(Convert.ToString(rd["DOC_DATE_GRE"]), "dd/MM/yyyy", null);
                    DOC_DATE_GRE.Value = Convert.ToDateTime(rd.Rows[i]["DOC_DATE_GRE"]);
                    DOC_DATE_HIJ.Text = Convert.ToString(rd.Rows[i]["DOC_DATE_HIJ"]);
                    SUP_CODE.Text = Convert.ToString(rd.Rows[i]["SUP_CODE"]);
                    SUP_NAME.Text = General.getName(SUP_CODE.Text, "PAY_SUPPLIER");
                    PAY_CODE.Text = Convert.ToString(rd.Rows[i]["PAY_CODE"]);
                    PAY_NAME.Text = General.getName(PAY_CODE.Text, "GEN_PAYTYPE");
                    CHQ_NO.Text = Convert.ToString(rd.Rows[i]["CHQ_NO"]);
                    if (rd.Rows[i]["CHQ_DATE"] != DBNull.Value)
                        CHQ_DATE.Value = Convert.ToDateTime(rd.Rows[i]["CHQ_DATE"]);
                    CUR_CODE.Text = Convert.ToString(rd.Rows[i]["CUR_CODE"]);
                    AMOUNT.Text = Convert.ToString(rd.Rows[i]["AMOUNT"]);
                    BANK_CODE.Text = Convert.ToString(rd.Rows[i]["BANK_CODE"]);
                    NOTES.Text = Convert.ToString(rd.Rows[i]["NOTES"]);
                    TOTAL_PAID.Text = Convert.ToString(rd.Rows[i]["TOTAL_PAID"]);
                    TOTAL_CURRENT.Text = Convert.ToString(rd.Rows[i]["TOTAL_CURRENT"]);
                    TOTAL_BALANCE.Text = Convert.ToString(rd.Rows[i]["TOTAL_BALANCE"]);

                    
                }
            }
            else
            {
                // cmd1.CommandText = "select DOC_ID, BRANCH, DOC_NO, REC_NO, CONVERT(VARCHAR(50), DOC_DATE_GRE, 103) AS DOC_DATE_GRE, DOC_DATE_HIJ, CUR_CODE, EXCHANGE_RATE, RDOC_NO, CUST_CODE, SMAN_CODE, AMOUNT, PAY_CODE, CHQ_NO, CHQ_DATE, BANK_CODE, ACC_DETAILS, DEBIT_CODE, DESC1, CREDIT_CODE, DESC2, NOTES, USER_CODE, MODITIMES, PRINTEDTIMES, DIST_TYPE, DISCOUNT, POST_FLAG, CANCEL_FLAG, TOTAL_PAID, TOTAL_CURRENT, TOTAL_BALANCE from REC_RECEIPTVOUCHER_HDR WHERE DOC_NO='" + ID + "'";
                // cmd1.Connection = conn;
                // SqlDataReader rd = cmd1.ExecuteReader();
                payvchrhdrdb.DocNo = ID;
                DataTable rd = payvchrhdrdb.getDataFromRecieptVoucher();

                if (rd.Rows.Count>0)
                {
                    for(int i=0;i<rd.Rows.Count;i++)
                    {
                        DOC_DATE_GRE.Value = DateTime.ParseExact(Convert.ToString(rd.Rows[i]["DOC_DATE_GRE"]), "dd/MM/yyyy",CultureInfo.InvariantCulture);
                       // DOC_DATE_GRE.Value =Convert.ToDateTime(Convert.ToString(rd["DOC_DATE_GRE"]));
                       // DOC_DATE_GRE.Value = Convert.ToDateTime(rd["DOC_DATE_GRE"]);
                        DOC_DATE_HIJ.Text = Convert.ToString(rd.Rows[i]["DOC_DATE_HIJ"]);
                        SUP_CODE.Text = Convert.ToString(rd.Rows[i]["CUST_CODE"]);
                        SUP_NAME.Text = General.getName(SUP_CODE.Text, "REC_CUSTOMER");
                        PAY_CODE.Text = Convert.ToString(rd.Rows[i]["PAY_CODE"]);
                        PAY_NAME.Text = General.getName(PAY_CODE.Text, "GEN_PAYTYPE");
                        CHQ_NO.Text = Convert.ToString(rd.Rows[i]["CHQ_NO"]);
                        if (rd.Rows[i]["CHQ_DATE"] != DBNull.Value)
                            CHQ_DATE.Value = Convert.ToDateTime(rd.Rows[i]["CHQ_DATE"]);
                        //   DOC_DATE_HIJ.Text = Convert.ToString(rd["DOC_DATE_HIJ"]);
                        CUR_CODE.Text = Convert.ToString(rd.Rows[i]["CUR_CODE"]);
                        AMOUNT.Text = Convert.ToString(rd.Rows[i]["AMOUNT"]);
                        BANK_CODE.Text = Convert.ToString(rd.Rows[i]["BANK_CODE"]);
                        NOTES.Text = Convert.ToString(rd.Rows[i]["NOTES"]);
                        TOTAL_PAID.Text = Convert.ToString(rd.Rows[i]["TOTAL_PAID"]);
                        TOTAL_CURRENT.Text = Convert.ToString(rd.Rows[i]["TOTAL_CURRENT"]);
                        TOTAL_BALANCE.Text = Convert.ToString(rd.Rows[i]["TOTAL_BALANCE"]);
                        if (TextDiscount.Visible)
                        {
                            TextDiscount.Text = Convert.ToString(rd.Rows[i]["DISCOUNT"]);
                        }
                    }
                }
            }
           // conn.Close();
            //DbFunctions.CloseConnection();
            dgClients.Visible = false;
        }
      
        private void btnSup_Click(object sender, EventArgs e)
        {
            if (form == 0)
            {
                try
                {
                    SupplierMasterHelp h = new SupplierMasterHelp(0);
                    if (h.ShowDialog() == DialogResult.OK && h.c != null)
                    {
                        btnClear.PerformClick();
                        GetStatement = false;                      
                        SUP_CODE.Text = Convert.ToString(h.c[0].Value);

                        PARTYACC.SelectedValue = Convert.ToString(h.c["LedgerId"].Value);
                        SUP_NAME.Text = Convert.ToString(h.c[1].Value);

                        //conn.Open();
                        //cmd.CommandText = "SUP_PAIDAMNTS";
                        //cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.Parameters.Clear();
                        //cmd.Parameters.AddWithValue("@SUP_CODE", SUP_CODE.Text);
                        string cmd="SUP_PAIDAMNTS";
                        payvchrhdrdb.SupCode = SUP_CODE.Text;
                        SqlDataReader r = payvchrhdrdb.supCreditPurchaseProcedure(cmd, "@SUP_CODE");
                       // SqlDataReader r = cmd.ExecuteReader();
                        dgDetail.Rows.Clear();
                        double totalPaid = 0;
                        double totalBal = 0;
                        while (r.Read())
                        {
                            dgDetail.Rows.Add(r["DOC_NO"], r["DOC_DATE_GRE"], r["DOC_DATE_HIJ"], r["SUP_CODE"], r["PAY_CODE"], r["CHQ_NO"], Convert.ToDouble(r["AMOUNT"]).ToString(decimalFormat));
                            totalPaid = totalPaid + Convert.ToDouble(r["AMOUNT"]);
                            //  totalBal = totalBal + Convert.ToDouble(r["BALANCE"]);
                        }
                        //conn.Close();
                        DbFunctions.CloseConnection();

                        //conn.Open();
                        //cmd.CommandText = "SUP_CREDIT_PUR";
                        //cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.Parameters.Clear();
                        //cmd.Parameters.AddWithValue("@SUP_CODE", SUP_CODE.Text);
                        //SqlDataReader r1 = cmd.ExecuteReader();
                        string cmds = "SUP_CREDIT_PUR";
                        payvchrhdrdb.SupCode = SUP_CODE.Text;
                        SqlDataReader r1 = payvchrhdrdb.supCreditPurchaseProcedure(cmds, "@SUP_CODE");
                        while (r1.Read())
                        {

                            totalBal = totalBal + Convert.ToDouble(r1["NET_VAL"]);
                        }

                        TOTAL_CURRENT.Text = totalBal.ToString();
                        TOTAL_PAID.Text = totalPaid.ToString();
                        TOTAL_BALANCE.Text = (totalBal - totalPaid).ToString();
                        //conn.Close();
                        DbFunctions.CloseConnection();
                        dgClients.Visible = false;
                    }

                    AMOUNT.Focus();
                }
                catch(Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }
            }
            else
            {
                CommonHelp h = new CommonHelp(0, genEnum.Customer);
                if (h.ShowDialog() == DialogResult.OK && h.c != null)
                {
                    try
                    {
                        SUP_CODE.Text = Convert.ToString(h.c[0].Value);
                        PARTYACC.SelectedValue = Convert.ToString(h.c["LedgerId"].Value);
                        SUP_NAME.Text = Convert.ToString(h.c[1].Value);
                        
                        //conn.Open();
                        //cmd.CommandText = "CUS_PAIDAMNTS";
                        //cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.Parameters.Clear();
                        //cmd.Parameters.AddWithValue("@CUS_CODE", SUP_CODE.Text);
                        //SqlDataReader r = cmd.ExecuteReader();
                        string command = "CUS_PAIDAMNTS";
                        payvchrhdrdb.SupCode = SUP_CODE.Text;
                        SqlDataReader r = payvchrhdrdb.supCreditPurchaseProcedure(command, "@CUS_CODE");
                        dgDetail.Rows.Clear();
                        double totalPaid = 0;
                        double totalBal = 0;
                        while (r.Read())
                        {
                            dgDetail.Rows.Add(r["DOC_NO"], r["DOC_DATE_GRE"], r["DOC_DATE_HIJ"], r["CUST_CODE"], r["PAY_CODE"], r["CHQ_NO"], Convert.ToDouble(r["AMOUNT"]).ToString(decimalFormat));
                            totalPaid = totalPaid + Convert.ToDouble(r["AMOUNT"]);
                            //  totalBal = totalBal + Convert.ToDouble(r["BALANCE"]);
                        }
                        //conn.Close();
                        DbFunctions.CloseConnection();
                        //conn.Open();
                        //cmd.CommandText = "CUS_CREDIT_PUR";
                        //cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.Parameters.Clear();
                        //cmd.Parameters.AddWithValue("@CUS_CODE", SUP_CODE.Text);
                        //SqlDataReader r2 = cmd.ExecuteReader();
                        string commands = "CUS_CREDIT_PUR";
                        payvchrhdrdb.SupCode = SUP_CODE.Text;
                        SqlDataReader r2 = payvchrhdrdb.supCreditPurchaseProcedure(commands, "@CUS_CODE");

                        while (r2.Read())
                        {
                            totalBal = totalBal + Convert.ToDouble(r2["NET_AMOUNT"]);
                            //  totalBal = totalBal + Convert.ToDouble(r["BALANCE"]);
                        }
                        TOTAL_CURRENT.Text = totalBal.ToString();
                        TOTAL_PAID.Text = totalPaid.ToString();
                        TOTAL_BALANCE.Text = (totalBal - totalPaid).ToString();
                    }
                    catch (Exception ee)
                    {
                        MessageBox.Show(ee.Message);
                    }
                    finally
                    {
                        //conn.Close();
                        DbFunctions.CloseConnection();
                    }
                }
            }
            dgClients.Visible = false;
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            CommonHelp c = new CommonHelp(0, genEnum.PayType);
            if (c.ShowDialog() == DialogResult.OK && c.c != null)
            {
                PAY_CODE.Text = Convert.ToString(c.c[0].Value);
                PAY_NAME.Text = Convert.ToString(c.c[1].Value);
                PARTYACC.Focus();
            }

            if (!edit)
            {
                if (CASHACC.Text != "CASH ACCOUNT")
                {
                    SelectBank();
                }
            }
        }


        public void SelectBank()
        {
            if (PAY_CODE.Text == "CHQ" | PAY_CODE.Text == "DEP" | PAY_CODE.Text == "CRD")
            {
                BANK_CODE.Text = CASHACC.Text;
            }
        }

        private void btnCurr_Click(object sender, EventArgs e)
        {
            CurrencyHelp h = new CurrencyHelp();
            if (h.ShowDialog() == DialogResult.OK && h.c != null)
            {
                CUR_CODE.Text = Convert.ToString(h.c["CODE"].Value);
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
                if (date > Convert.ToDateTime(DOC_DATE_GRE.Value.ToString()))
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
            if (AMOUNT.Text=="" || Convert.ToDouble(AMOUNT.Text) <= 0)
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
            string discVal = TextDiscount.Text == "" ? "0" : TextDiscount.Text;
            string query="";
            if (editMode && MessageBox.Show("Are you sure, you want to commit the changes?", "Confirm changes", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }
            string branch = ComSet.ReadBranch();
            //if (DialogResult.Yes == MessageBox.Show("Are you sure to continue payment", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            //{
            if (PARTYACC.Text != CASHACC.Text)
            {
                if (valid())
                {

                    TransDate = Convert.ToDateTime(DOC_DATE_GRE.Value.ToString());
                    if (PAY_CODE.Text == "CHQ")
                    {
                        TransDate = Convert.ToDateTime(CHQ_DATE.Value);
                    }
                    string status = "Added!";
                    if (ID == "")
                    {

                        if (fld.Equals("SUP_CODE"))
                        {
                            if (PAY_CODE.Text != "CHQ")
                            {
                                query += "INSERT INTO PAY_PAYMENT_VOUCHER_HDR (BRANCH,DOC_NO,REC_NO,DOC_DATE_GRE,DOC_DATE_HIJ,SUP_CODE,CUR_CODE,AMOUNT,PAY_CODE,BANK_CODE,ACC_DETAILS,CHQ_NO,CHQ_DATE,CREDIT_CODE,DESC2,DEBIT_CODE,DESC1,NOTES,TOTAL_PAID,TOTAL_CURRENT,TOTAL_BALANCE,PROJECTID) VALUES('" + branch + "','" + DOC_NO.Text + "','" + txtVoucherNo.Text + "','" + DOC_DATE_GRE.Value.ToString() + "','" + DOC_DATE_HIJ.Text + "','" + SUP_CODE.Text + "','" + CUR_CODE.Text + "','" + Convert.ToDecimal(AMOUNT.Text) + "','" + PAY_CODE.Text + "','" + BANK_CODE.Text + "','" + txtAccDetails.Text + "','" + CHQ_NO.Text + "',NULL,'" + CASHACC.SelectedValue + "','" + CASHACC.Text + "','" + PARTYACC.SelectedValue + "','" + PARTYACC.Text + "','" + Common.sqlEscape(NOTES.Text) + "','" + TOTAL_PAID.Text + "','" + TOTAL_CURRENT.Text + "','" + TOTAL_BALANCE.Text + "','" + cmb_projects.SelectedValue + "')";

                            }
                            else if (PAY_CODE.Text == "CHQ")
                            {
                                query += "INSERT INTO PAY_PAYMENT_VOUCHER_HDR (BRANCH,DOC_NO,REC_NO,DOC_DATE_GRE,DOC_DATE_HIJ,SUP_CODE,CUR_CODE,AMOUNT,PAY_CODE,BANK_CODE,ACC_DETAILS,CHQ_NO,CHQ_DATE,CREDIT_CODE,DESC2,DEBIT_CODE,DESC1,NOTES,TOTAL_PAID,TOTAL_CURRENT,TOTAL_BALANCE,PROJECTID) VALUES('" + branch + "','" + DOC_NO.Text + "','" + txtVoucherNo.Text + "','" + DOC_DATE_GRE.Value.ToString() + "','" + DOC_DATE_HIJ.Text + "','" + SUP_CODE.Text + "','" + CUR_CODE.Text + "','" + Convert.ToDecimal(AMOUNT.Text) + "','" + PAY_CODE.Text + "','" + BANK_CODE.Text + "','" + txtAccDetails.Text + "','" + CHQ_NO.Text + "','" + CHQ_DATE.Value.ToString() + "','" + CASHACC.SelectedValue + "','" + CASHACC.Text + "','" + PARTYACC.SelectedValue + "','" + PARTYACC.Text + "','" + Common.sqlEscape(NOTES.Text) + "','" + TOTAL_PAID.Text + "','" + TOTAL_CURRENT.Text + "','" + TOTAL_BALANCE.Text + "','" + cmb_projects.SelectedValue + "')";
                            }
                        }
                        //  DOC_NO.Text = General.generatePayVoucherCode();
                        else
                        {
                            if (PAY_CODE.Text != "CHQ")
                            {
                                query += "INSERT INTO REC_RECEIPTVOUCHER_HDR (BRANCH,DOC_NO,REC_NO,DOC_DATE_GRE,DOC_DATE_HIJ,CUST_CODE,CUR_CODE,AMOUNT,PAY_CODE,BANK_CODE,ACC_DETAILS,CHQ_NO,CHQ_DATE,DEBIT_CODE,DESC1,CREDIT_CODE,DESC2,NOTES,DISCOUNT,TOTAL_PAID,TOTAL_CURRENT,TOTAL_BALANCE,PROJECTID) VALUES('" + branch + "','" + DOC_NO.Text + "','" + txtVoucherNo.Text + "','" + DOC_DATE_GRE.Value.ToString() + "','" + DOC_DATE_HIJ.Text + "','" + SUP_CODE.Text + "','" + CUR_CODE.Text + "','" + Convert.ToDecimal(AMOUNT.Text) + "','" + PAY_CODE.Text + "','" + BANK_CODE.Text + "','" + txtAccDetails.Text + "','" + CHQ_NO.Text + "',NULL,'" + CASHACC.SelectedValue + "','" + CASHACC.Text + "','" + PARTYACC.SelectedValue + "','" + PARTYACC.Text + "','" + Common.sqlEscape(NOTES.Text) + "','" + discVal + "','" + TOTAL_PAID.Text + "','" + TOTAL_CURRENT.Text + "','" + TOTAL_BALANCE.Text + "','" + cmb_projects.SelectedValue + "')";
                            }
                            else if (PAY_CODE.Text == "CHQ")
                            {
                                query += "INSERT INTO REC_RECEIPTVOUCHER_HDR (BRANCH,DOC_NO,REC_NO,DOC_DATE_GRE,DOC_DATE_HIJ,CUST_CODE,CUR_CODE,AMOUNT,PAY_CODE,BANK_CODE,ACC_DETAILS,CHQ_NO,CHQ_DATE,DEBIT_CODE,DESC1,CREDIT_CODE,DESC2,NOTES,DISCOUNT,TOTAL_PAID,TOTAL_CURRENT,TOTAL_BALANCE,PROJECTID) VALUES('" + branch + "','" + DOC_NO.Text + "','" + txtVoucherNo.Text + "','" + DOC_DATE_GRE.Value.ToString() + "','" + DOC_DATE_HIJ.Text + "','" + SUP_CODE.Text + "','" + CUR_CODE.Text + "','" + Convert.ToDecimal(AMOUNT.Text) + "','" + PAY_CODE.Text + "','" + BANK_CODE.Text + "','" + txtAccDetails.Text + "','" + CHQ_NO.Text + "','" + CHQ_DATE.Value.ToString() + "','" + CASHACC.SelectedValue + "','" + CASHACC.Text + "','" + PARTYACC.SelectedValue + "','" + PARTYACC.Text + "','" + Common.sqlEscape(NOTES.Text) + "','" + discVal + "','" + TOTAL_PAID.Text + "','" + TOTAL_CURRENT.Text + "','" + TOTAL_BALANCE.Text + "','" + cmb_projects.SelectedValue + "')";
                            }
                        }
                    }
                    else
                    {


                        DeleteTransation();
                        if (TextDiscount.Visible)
                        {
                            string id = DOC_NO.Text;
                            trans.VOUCHERTYPE = "DISCOUNT GIVEN";
                            trans.VOUCHERNO = id;
                            trans.DeletePurchaseTransaction();
                        }

                        status = "Updated!";
                        if (fld.Equals("SUP_CODE"))
                        {
                            if (PAY_CODE.Text == "CHQ")
                            {
                                query += "UPDATE " + tableHDR + " SET DOC_DATE_GRE = '" + DOC_DATE_GRE.Value.ToString() + "',DOC_DATE_HIJ = '" + DOC_DATE_HIJ.Text + "'," + fld + " = '" + SUP_CODE.Text + "',CUR_CODE = '" + CUR_CODE.Text + "',AMOUNT = '" + Convert.ToDecimal(AMOUNT.Text) + "',PAY_CODE = '" + PAY_CODE.Text + "',CREDIT_CODE = '" + CASHACC.SelectedValue.ToString() + "',DESC2='" + CASHACC.Text + "',DEBIT_CODE = '" + PARTYACC.SelectedValue.ToString() + "',DESC1='" + PARTYACC.Text + "',BANK_CODE = '" + BANK_CODE.Text + "',CHQ_NO = '" + CHQ_NO.Text + "',CHQ_DATE = '" + CHQ_DATE.Value.ToString() + "',NOTES = '" + Common.sqlEscape(NOTES.Text) + "',TOTAL_PAID = '" + TOTAL_PAID.Text + "',TOTAL_CURRENT = '" + TOTAL_CURRENT.Text + "',TOTAL_BALANCE = '" + TOTAL_BALANCE.Text + "',PROJECTID = '" + cmb_projects.SelectedValue + "' WHERE DOC_NO = '" + DOC_NO.Text + "';DELETE FROM " + tableDTL + " WHERE DOC_NO = '" + DOC_NO.Text + "'";
                            }
                            else if (PAY_CODE.Text != "CHQ")
                            {
                                query += "UPDATE " + tableHDR + " SET DOC_DATE_GRE = '" + DOC_DATE_GRE.Value.ToString() + "',DOC_DATE_HIJ = '" + DOC_DATE_HIJ.Text + "'," + fld + " = '" + SUP_CODE.Text + "',CUR_CODE = '" + CUR_CODE.Text + "',AMOUNT = '" + Convert.ToDecimal(AMOUNT.Text) + "',PAY_CODE = '" + PAY_CODE.Text + "',CREDIT_CODE = '" + CASHACC.SelectedValue.ToString() + "',DESC2='" + CASHACC.Text + "',DEBIT_CODE = '" + PARTYACC.SelectedValue.ToString() + "',DESC1='" + PARTYACC.Text + "',BANK_CODE = '" + BANK_CODE.Text + "',CHQ_NO = '" + CHQ_NO.Text + "',NOTES = '" + Common.sqlEscape(NOTES.Text) + "',TOTAL_PAID = '" + TOTAL_PAID.Text + "',TOTAL_CURRENT = '" + TOTAL_CURRENT.Text + "',TOTAL_BALANCE = '" + TOTAL_BALANCE.Text + "',PROJECTID = '" + cmb_projects.SelectedValue + "' WHERE DOC_NO = '" + DOC_NO.Text + "';DELETE FROM " + tableDTL + " WHERE DOC_NO = '" + DOC_NO.Text + "'";
                            }
                        }
                        else
                        {
                            if (PAY_CODE.Text == "CHQ")
                            {
                                query += "UPDATE " + tableHDR + " SET DOC_DATE_GRE = '" + DOC_DATE_GRE.Value.ToString() + "',DOC_DATE_HIJ = '" + DOC_DATE_HIJ.Text + "'," + fld + " = '" + SUP_CODE.Text + "',CUR_CODE = '" + CUR_CODE.Text + "',AMOUNT = '" + Convert.ToDecimal(AMOUNT.Text) + "',PAY_CODE = '" + PAY_CODE.Text + "',CREDIT_CODE = '" + PARTYACC.SelectedValue.ToString() + "',DESC2='" + PARTYACC.Text + "',DEBIT_CODE = '" + CASHACC.SelectedValue.ToString() + "',DESC1='" + CASHACC.Text + "',BANK_CODE = '" + BANK_CODE.Text + "',CHQ_NO = '" + CHQ_NO.Text + "',CHQ_DATE = '" + CHQ_DATE.Value.ToString() + "',NOTES = '" + Common.sqlEscape(NOTES.Text) + "',DISCOUNT='" + discVal + "', TOTAL_PAID = '" + TOTAL_PAID.Text + "',TOTAL_CURRENT = '" + TOTAL_CURRENT.Text + "',TOTAL_BALANCE = '" + TOTAL_BALANCE.Text + "',PROJECTID = '" + cmb_projects.SelectedValue + "' WHERE DOC_NO = '" + DOC_NO.Text + "';DELETE FROM " + tableDTL + " WHERE DOC_NO = '" + DOC_NO.Text + "'";
                            }
                            else if (PAY_CODE.Text != "CHQ")
                            {
                                query += "UPDATE " + tableHDR + " SET DOC_DATE_GRE = '" + DOC_DATE_GRE.Value.ToString() + "',DOC_DATE_HIJ = '" + DOC_DATE_HIJ.Text + "'," + fld + " = '" + SUP_CODE.Text + "',CUR_CODE = '" + CUR_CODE.Text + "',AMOUNT = '" + Convert.ToDecimal(AMOUNT.Text) + "',PAY_CODE = '" + PAY_CODE.Text + "',CREDIT_CODE = '" + PARTYACC.SelectedValue.ToString() + "',DESC2='" + PARTYACC.Text + "',DEBIT_CODE = '" + CASHACC.SelectedValue.ToString() + "',DESC1='" + CASHACC.Text + "',BANK_CODE = '" + BANK_CODE.Text + "',CHQ_NO = '" + CHQ_NO.Text + "',NOTES = '" + Common.sqlEscape(NOTES.Text) + "',DISCOUNT='" + discVal + "',TOTAL_PAID = '" + TOTAL_PAID.Text + "',TOTAL_CURRENT = '" + TOTAL_CURRENT.Text + "',TOTAL_BALANCE = '" + TOTAL_BALANCE.Text + "',PROJECTID = '" + cmb_projects.SelectedValue + "' WHERE DOC_NO = '" + DOC_NO.Text + "';DELETE FROM " + tableDTL + " WHERE DOC_NO = '" + DOC_NO.Text + "'";
                            }
                        }
                    }
                    //   cmd.CommandText += " INSERT INTO " + tableDTL + "(DOC_NO," + fld + ",INV_DATE_GRE,INV_DATE_HIJ,INV_NO,AMOUNT,PAID,BALANCE,CURRENT_PAY_AMOUNT) ";
                    // foreach (DataGridViewRow row in dgDetail.Rows)
                    //{
                    //  cmd.CommandText += " SELECT '" + DOC_NO.Text + "','" + SUP_CODE.Text + "','" + Convert.ToDateTime(row.Cells[1].Value.ToString()).ToString("MM/dd/yyyy") + "','" + row.Cells[2].Value.ToString() + "','" + row.Cells[0].Value + "','" + row.Cells[3].Value + "','" + row.Cells[4].Value.ToString() + "','" + row.Cells[6].Value.ToString() + "','" + row.Cells[5].Value.ToString() + "' UNION ALL ";
                    //}
                    //   cmd.CommandText = cmd.CommandText.Substring(0, cmd.CommandText.Length - 10);
                   
                    //cmd.Connection = conn;
                    //conn.Open();
                    //cmd.CommandType = CommandType.Text;
                    //cmd.ExecuteNonQuery();
                    //conn.Close();
                   int i= DbFunctions.InsertUpdate(query);
                   if (i > 0)
                   {


                       if (!PAY_CODE.Text.Equals("CHQ"))
                       {
                           if (form == 0)
                           {
                               paymentVoucherTransaction();
                               //bindDayPayments();
                           }
                           else
                           {
                               receiptVoucherTransaction();
                               if (TextDiscount.Visible)
                               {
                                   if (TextDiscount.Text != "")
                                   {
                                       if (Convert.ToDouble(TextDiscount.Text) > 0)
                                       {
                                           receiptVoucherTransactionDIscount();
                                       }
                                   }
                               }
                               //BindDayReceipts();

                           }
                       }

                   }
                   else
                   {
                       MessageBox.Show("Error");
                   }

                    // MessageBox.Show("Payment Voucher " + status);
                    if (ChekPrint.Checked == true)
                    {
                        printingrecipt();
                    }
                    GetMaxRecVouch();
                    btnClear.PerformClick();
                }
                dgDetail.Columns["cBalance"].DisplayIndex = 5;
                dgDetail.Columns["cDateGRE"].Width = 95;
                dgDetail.Columns["cDateHIJ"].Width = 10;
                dgDetail.Columns["cInvAmt"].Width = 80;
                dgDetail.Columns["cName"].Width = 170;
                dgDetail.Columns["cBalance"].Width = 100;
                dgDetail.Columns["cCurrent"].Width = 150;
                dgDetail.Columns["cPaidAmt"].Width = 55;
                dgDetail.Columns["SL_NO"].DisplayIndex = 0;
                dgDetail.Columns["VOUCHER"].DisplayIndex = 1;
                dgDetail.Columns["ACCOUNT"].DisplayIndex = 6;
                dgDetail.Columns["SL_NO"].Width = 40;
                dgDetail.Columns["VOUCHER"].Width = 70;
                dgDetail.Columns["ACCOUNT"].Width = 150;
            }
            else
            {
                MessageBox.Show("Can't save, Choose a different account.");
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
            modtrans.Date = DOC_DATE_GRE.Value.ToString();
            modtrans.USERID = lg.EmpId;
            modtrans.VOUCHERNO = DOC_NO.Text;
            modtrans.BRANCH = lg.Branch;
            modtrans.NARRATION = NOTES.Text;
            modtrans.STATUS = "Update";
            modtrans.MODIFIEDDATE = DateTime.Now.ToString("MM/dd/yyyy"); ;
            modtrans.insertTransaction();
        }

        private void DeleteTransation()
        {
            if (form == 0)
            {
                trans.VOUCHERTYPE = "Cash Payment";
            }
            else
            {
                trans.VOUCHERTYPE = "Cash Receipt";
            }
            trans.VOUCHERNO = DOC_NO.Text;
            trans.DeletePurchaseTransaction();
        }
        
        public void receiptVoucherTransaction()
        {
           
            trans.VOUCHERTYPE = "Cash Receipt";
            trans.DATED = TransDate.ToString();
            trans.NARRATION = NOTES.Text;
            Login log = (Login)Application.OpenForms["Login"];
            trans.USERID = log.EmpId;
            trans.ACCNAME = CASHACC.Text;
            trans.PARTICULARS = PARTYACC.Text;
            trans.VOUCHERNO = DOC_NO.Text;
            trans.BRANCH = lg.Branch;
            trans.ACCID = CASHACC.SelectedValue.ToString();
            trans.CREDIT = "0";
            trans.NARRATION = NOTES.Text;
            trans.DEBIT = AMOUNT.Text;
            trans.PROJECTID=Convert.ToInt32(cmb_projects.SelectedValue);
            trans.SYSTEMTIME = DateTime.Now.ToString();
            trans.insertTransaction();
            trans.PARTICULARS = CASHACC.Text;
            trans.ACCNAME = PARTYACC.Text;
            trans.VOUCHERNO = DOC_NO.Text;
            trans.ACCID = PARTYACC.SelectedValue.ToString();
            trans.DEBIT = "0";
            trans.CREDIT = AMOUNT.Text;
            trans.PROJECTID=Convert.ToInt32(cmb_projects.SelectedValue);
            trans.SYSTEMTIME = DateTime.Now.ToString();
            trans.insertTransaction();

        }
        public void receiptVoucherTransactionDIscount()
        {
            trans.VOUCHERTYPE = "Discount Given";
            trans.DATED = TransDate.ToString();
            trans.NARRATION = NOTES.Text;
            Login log = (Login)Application.OpenForms["Login"];
            trans.USERID = log.EmpId;

            trans.ACCNAME = "DISCOUNT GIVEN";
            trans.PARTICULARS = PARTYACC.Text;
            trans.VOUCHERNO = DOC_NO.Text;
            trans.BRANCH = lg.Branch;
            trans.ACCID = "53";
            trans.CREDIT = "0";
            trans.NARRATION = NOTES.Text;
            trans.DEBIT = TextDiscount.Text == "" ? "0" : TextDiscount.Text;
            trans.PROJECTID = Convert.ToInt32(cmb_projects.SelectedValue);
            trans.SYSTEMTIME = DateTime.Now.ToString();
            trans.insertTransaction();

            trans.PARTICULARS = "DISCOUNT GIVEN";
            trans.ACCNAME = PARTYACC.Text;
            trans.VOUCHERNO = DOC_NO.Text;
            trans.ACCID = PARTYACC.SelectedValue.ToString();
            trans.DEBIT = "0";
            trans.CREDIT = TextDiscount.Text == "" ? "0" : TextDiscount.Text;
            trans.PROJECTID = Convert.ToInt32(cmb_projects.SelectedValue);
            trans.SYSTEMTIME = DateTime.Now.ToString();
            trans.insertTransaction();

        }

        public void checkpaymentTransaction()
        {
            
            trans.VOUCHERTYPE = "Bank Payment";
            trans.DATED = TransDate.ToString();
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
            trans.SYSTEMTIME = DateTime.Now.ToString();
            trans.PROJECTID=Convert.ToInt32(cmb_projects.SelectedValue);
            trans.insertTransaction();


            trans.PARTICULARS = PARTYACC.Text;
            trans.ACCNAME = CASHACC.Text;
            trans.VOUCHERNO = DOC_NO.Text;
            trans.ACCID = CASHACC.SelectedValue.ToString();
            trans.DEBIT = "0";
            trans.CREDIT = AMOUNT.Text;
            trans.PROJECTID=Convert.ToInt32(cmb_projects.SelectedValue);
            trans.SYSTEMTIME = DateTime.Now.ToString();
            trans.insertTransaction();
        }

        public void paymentVoucherTransaction()
        {
            trans.VOUCHERTYPE = "Cash Payment";
            trans.DATED = TransDate.ToString();
            trans.NARRATION = NOTES.Text;
            Login log = (Login)Application.OpenForms["Login"];
            trans.USERID = log.EmpId;
            trans.ACCNAME = PARTYACC.Text;
            trans.PARTICULARS = CASHACC.Text;
            trans.VOUCHERNO = DOC_NO.Text;
            trans.BRANCH = lg.Branch;
            trans.ACCID = PARTYACC.SelectedValue.ToString();
            trans.CREDIT = "0";
            trans.DEBIT =AMOUNT.Text ;
            trans.PROJECTID=Convert.ToInt32(cmb_projects.SelectedValue);
            trans.SYSTEMTIME = DateTime.Now.ToString();
            trans.insertTransaction();

            trans.PARTICULARS = PARTYACC.Text;
            trans.ACCNAME = CASHACC.Text;
            trans.VOUCHERNO = DOC_NO.Text;
            trans.ACCID = CASHACC.SelectedValue.ToString();
            trans.DEBIT = "0" ;
            trans.CREDIT =AMOUNT.Text;
            trans.SYSTEMTIME = DateTime.Now.ToString();
            trans.PROJECTID=Convert.ToInt32(cmb_projects.SelectedValue);
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

        public void BindSupplier()
        {
            table.Rows.Clear();
            HasArabic = General.IsEnabled(Settings.Arabic);

            /*if (HasArabic)
                cmd.CommandText = "SELECT CODE,DESC_ENG,DESC_ARB,TYPE,ADDRESS_A,ADDRESS_B,POBOX,TELE1,TELE2,MOBILE,EMAIL,CITY_CODE,COUNTRY_CODE,OPENING_BAL,FAX,CONVERT(NVARCHAR,DATE_GRE,103) AS DATE_GRE,INITIAL,DEFAULT_CURRENCY,CONTACT_PERSON,LedgerId FROM PAY_SUPPLIER";
            
            else
                cmd.CommandText = "SELECT CODE,DESC_ENG,ADDRESS_A,TYPE,ADDRESS_B,POBOX,TELE1,TELE2,MOBILE,EMAIL,CITY_CODE,COUNTRY_CODE,OPENING_BAL,FAX,CONVERT(NVARCHAR,DATE_GRE,103) AS DATE_GRE,INITIAL,DEFAULT_CURRENCY,CONTACT_PERSON,LedgerId FROM PAY_SUPPLIER";
            cmd.CommandType = CommandType.Text;
            adapter.SelectCommand = cmd;
            adapter.Fill(table);
            source.DataSource =table;
             */

            source.DataSource = paysupdb.GetAllData(HasArabic);
            dgClients.DataSource = source;
        }
        public void BindCustomer()
        {
            table.Rows.Clear();
          /*  if (HasArabic)
                cmd.CommandText = "SELECT CODE,TYPE,DESC_ENG,DESC_ARB,TYPE,ADDRESS_A,ADDRESS_B,POBOX,TELE1,TELE2,MOBILE,EMAIL,CITY_CODE,COUNTRY_CODE,FAX,OPENING_BAL,DATE_GRE,DEFAULT_CURRENCY,SALESMAN_CODE,LedgerId FROM REC_CUSTOMER";
            else
                cmd.CommandText = "SELECT CODE,DESC_ENG,TYPE,ADDRESS_A,ADDRESS_B,POBOX,TELE1,TELE2,MOBILE,EMAIL,CITY_CODE,COUNTRY_CODE,FAX,OPENING_BAL,DATE_GRE,DEFAULT_CURRENCY,SALESMAN_CODE,LedgerId FROM REC_CUSTOMER";
            cmd.CommandType = CommandType.Text;
            adapter.SelectCommand = cmd;
            adapter.Fill(table);
            source.DataSource = table;
           */
            source.DataSource = clsCus.GetAllData(HasArabic);
            dgClients.DataSource = source;

        }

        private void PaymentVoucher_Load(object sender, EventArgs e)
        {
            if (form == 0)
            {
                kryptonLabel9.Visible = false;
                TextDiscount.Visible = false;
            }
            if(!Regex.IsMatch(cmp.Name, "PTL"))
            {
                kryptonLabel9.Visible = false;
                TextDiscount.Visible = false;
            }
            HasAccounts = Properties.Settings.Default.Account;
           // DOC_DATE_GRE.Value = System.DateTime.Now;
          
          //  btnDelete.Enabled = false;
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
            if (HasArabic)
            {
                dgDetail.Columns["cDateHIJ"].Visible = true;
            }
            else
            {
                dgDetail.Columns["cDateHIJ"].Visible = false;
            }
            GetBranchDetails();
           // CASHACC.Enabled = false;
            CASHACC.Enabled = true;
            foreach (DataGridViewColumn col in dgDetail.Columns)
            {
                col.Width = 150;
            }
            if (fld.Equals("SUP_CODE"))
            {
                GetMaxPayVouch();
                dgDetail.Columns["cName"].HeaderText = "SUPPLIER NAME";
               
                dgDetail.Columns["cBalance"].HeaderText = "AMOUNT PAID";
                dgDetail.Columns["cPaidAmt"].HeaderText = "PAY CODE";
                bindDayPayments();
            }
            else if (fld.Equals("CUST_CODE"))
            {
                GetMaxRecVouch();
                dgDetail.Columns["cName"].HeaderText = "CUSTOMER NAME";
                dgDetail.Columns["cBalance"].HeaderText = "AMOUNT RECEIVED";
                dgDetail.Columns["cPaidAmt"].HeaderText = "RECEIPT CODE";
                BindDayReceipts();
            }
            if (ID == "")
            {
                if (form == 0)
                {
                  //  DOC_NO.Text = General.generatePayVoucherCode();
                    GetMaxPayVouch();

                }
                else
                {
                    GetMaxRecVouch();
                    //DOC_NO.Text = General.generateReceiptVoucherCode();
                }
                panel3.Visible = false;
            }
            dgDetail.Columns["cBalance"].DisplayIndex = 5;
            dgDetail.Columns["cDateGRE"].Width = 95;
            dgDetail.Columns["cDateHIJ"].Width = 10;
            dgDetail.Columns["cInvAmt"].Width = 80;
            dgDetail.Columns["cName"].Width = 170;
            dgDetail.Columns["cBalance"].Width = 100;
            dgDetail.Columns["cCurrent"].Width = 150;
            dgDetail.Columns["cPaidAmt"].Width = 55;
            dgDetail.Columns["SL_NO"].DisplayIndex = 0;
            dgDetail.Columns["VOUCHER"].DisplayIndex = 1;
            dgDetail.Columns["ACCOUNT"].DisplayIndex = 6;
            dgDetail.Columns["SL_NO"].Width = 40;
            dgDetail.Columns["VOUCHER"].Width = 70;
            dgDetail.Columns["ACCOUNT"].Width = 150;

            LoadProject();
        }

        private void LoadProject()
        {
            cmb_projects.DataSource = ProjectDB.ProjectForCombo();
            cmb_projects.DisplayMember = "DESC_ENG";
            cmb_projects.ValueMember = "Id";
            cmb_projects.SelectedIndex = 0;
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
            CASHACC.DataSource = dt2;
            CASHACC.DisplayMember = "LEDGERNAME";
            CASHACC.ValueMember = "LEDGERID";
        }

        void GetMaxRecVouch()
        {
            int maxId;
            String value;

                //cmd.CommandText = "SELECT ISNULL(MAX(CONVERT(DECIMAL(18,0),REC_NO)), 0) FROM REC_RECEIPTVOUCHER_HDR";
                //cmd.CommandType = CommandType.Text;
                //conn.Open();
               // value = Convert.ToString(cmd.ExecuteScalar());
                value = Convert.ToString(rrvhdb.getMaxRecNoRecVouch());
               // conn.Close();
           

            if (value.Equals("0"))
            {
              //  cmd.CommandText = "SELECT VouchStartFrom FROM GEN_VOUCH_STARTFROM WHERE VouchTypeCode='REC'";
              //  cmd.CommandType = CommandType.Text;
                //conn.Open();
              //  txtVoucherNo.Text = Convert.ToString(cmd.ExecuteScalar());
                txtVoucherNo.Text = Convert.ToString(rrvhdb.getVouchStartFrom());
               // conn.Close();
            }
            else
            {
                maxId = Convert.ToInt32(value);
                txtVoucherNo.Text = (maxId + 1).ToString();
            }
        }

        void GetMaxPayVouch()
        {
            int maxId;
            String value;
            /*
                        // cmd.CommandText = "SELECT ISNULL(MAX(CONVERT(DECIMAL(18,0),REC_NO)), 0) FROM PAY_PAYMENT_VOUCHER_HDR";
                       // cmd.CommandType = CommandType.Text;
                       // conn.Open();

                      //  value = Convert.ToString(cmd.ExecuteScalar());
                        value = Convert.ToString(payvchrhdrdb.getMaxRecNo());
                       // conn.Close();


                        if (value.Equals("0"))
                        {
                          //  cmd.CommandText = "SELECT VouchStartFrom FROM GEN_VOUCH_STARTFROM WHERE VouchTypeCode='PAY'";
                           // cmd.CommandType = CommandType.Text;
                           // conn.Open();
                          //  txtVoucherNo.Text = Convert.ToString(cmd.ExecuteScalar());
                            txtVoucherNo.Text = Convert.ToString(payvchrhdrdb.getVouchStartFrom());
                           // conn.Close();
                        }
                        else
                        {
                            maxId = Convert.ToInt32(value);
                            txtVoucherNo.Text = (maxId + 1).ToString();
                        }

                        */
            string vouchertype = "PaymentVoucher";
       
            string query = "Declare @MaxDocID as int, @NoSeriesSuffix as varchar(5) ";
            query += " Select @MaxDocID = case when Max(Doc_ID) is null then 0 else Max(Doc_ID) end + 1, @NoSeriesSuffix = max(f.NoSeriesSuffix) from PAY_PAYMENT_VOUCHER_HDR p right join tbl_FinancialYear f on p.DOC_DATE_GRE between f.SDate and f.EDate ";
            query += " where f.CurrentFY = 1 ";
            query += " Select s.PRIFIX + @NoSeriesSuffix + Right(Replicate('0', s.SERIAL_LENGTH) + cast(@MaxDocID as varchar), s.SERIAL_LENGTH) DOCNo, @MaxDocID DocID from GEN_DOC_SERIAL s ";
            query += " where s.DOC_TYPE = '" + vouchertype + "' ";
            DataTable dt = DbFunctions.GetDataTable(query);
            if (dt.Rows.Count >= 1)
            {
                Billno = txtVoucherNo.Text = dt.Rows[0]["DOCID"].ToString();
                DOC_NO.Text = dt.Rows[0]["DOCNo"].ToString();
            }



        }

        private void btnDoc_Click(object sender, EventArgs e)
        {
            edit = true;
            if (fld == "SUP_CODE")
            {
                form = 0;
            }
            PaymentVoucherHelp h = new PaymentVoucherHelp(0, form);
            if (h.ShowDialog() == DialogResult.OK && h.c != null)
            {
                btnClear.PerformClick();
                ID = Convert.ToString(h.c["DOC_NO"].Value);
                DOC_NO.Text = ID;
                btnDelete.Enabled = true;
               // cmd.CommandType = CommandType.Text;
              //  conn.Open();
                SqlDataReader r;
                if (fld == "SUP_CODE")
                {
                    //cmd.CommandText = "SELECT DOC_NO,CONVERT(NVARCHAR,DOC_DATE_GRE,103) AS DOC_DATE_GRE,DOC_DATE_HIJ,AMOUNT,TOTAL_PAID,SUP_CODE,PAY_CODE,CHQ_NO FROM " + tableHDR + " WHERE DOC_NO = '" + DOC_NO.Text + "'";
                    payvchrhdrdb.FormType = tableHDR;
                    payvchrhdrdb.DocNo = DOC_NO.Text;
                     r =payvchrhdrdb.getDataByDocNo();
                }
                else
                {
                    //cmd.CommandText = "SELECT DOC_NO,CONVERT(NVARCHAR,DOC_DATE_GRE,103) AS DOC_DATE_GRE,DOC_DATE_HIJ,AMOUNT,TOTAL_PAID,CUST_CODE,PAY_CODE,CHQ_NO FROM " + tableHDR + " WHERE DOC_NO = '" + DOC_NO.Text + "'";
                    payvchrhdrdb.FormType = tableHDR;
                    payvchrhdrdb.DocNo = DOC_NO.Text;
                    r = payvchrhdrdb.getDataByDocNoCus();
                }
               // conn.Open();
              //  SqlDataReader r = cmd.ExecuteReader();
                dgDetail.Rows.Clear();
                while (r.Read())
                {
                    if (fld == "SUP_CODE")
                    {
                        dgDetail.Rows.Add(r["DOC_NO"], r["DOC_DATE_GRE"], r["DOC_DATE_HIJ"], r["SUP_CODE"], " ", r["PAY_CODE"], r["CHQ_NO"], Convert.ToDouble(r["AMOUNT"]).ToString(decimalFormat));
                    }
                    else
                    {
                        dgDetail.Rows.Add(r["DOC_NO"], r["DOC_DATE_GRE"], r["DOC_DATE_HIJ"], r["CUST_CODE"], " ", r["PAY_CODE"], r["CHQ_NO"], Convert.ToDouble(r["AMOUNT"]).ToString(decimalFormat));
                    }
                }
              //  conn.Close();
                DbFunctions.CloseConnection();

                if (fld == "SUP_CODE")
                {
                    SUP_CODE.Text = Convert.ToString(h.c["SUP_CODE"].Value);
                    SUP_NAME.Text = General.getName(SUP_CODE.Text, "PAY_SUPPLIER");
                    PARTYACC.SelectedValue = Convert.ToString(h.c["DEBIT_CODE"].Value);
                }
                else
                {
                    PARTYACC.SelectedValue = Convert.ToString(h.c["CREDIT_CODE"].Value);
                }

                PAY_CODE.Text = Convert.ToString(h.c["PAY_CODE"].Value);
                PAY_NAME.Text = General.getName(PAY_CODE.Text, "GEN_PAYTYPE");
                CHQ_NO.Text = Convert.ToString(h.c["CHQ_NO"].Value);
                if (PAY_CODE.Text == "CHQ")
                    CHQ_DATE.Value = Convert.ToDateTime(h.c["CHQ_DATE"].Value.ToString());
                //DOC_DATE_GRE.Value = DateTime.ParseExact(h.c["DOC_DATE_GRE"].Value.ToString(), "dd/MM/yyyy", null);
                DOC_DATE_GRE.Value = Convert.ToDateTime(h.c["DOC_DATE_GRE"].Value.ToString());
                DOC_DATE_HIJ.Text = Convert.ToString(h.c["DOC_DATE_HIJ"].Value);
                CUR_CODE.Text = Convert.ToString(h.c["CUR_CODE"].Value);
                AMOUNT.Text = Convert.ToString(h.c["AMOUNT"].Value);
                //===maysa
                CASHACC.Text = Convert.ToString(h.c["BANK_CODE"].Value);
                //if (fld == "SUP_CODE")
                //{
                //    CASHACC.SelectedValue = Convert.ToString(h.c["CREDIT_CODE"].Value);
                //}
                //else
                //{
                //    CASHACC.SelectedValue = Convert.ToString(h.c["DEBIT_CODE"].Value);
                //}
                //=====
                //BANK_CODE.Text = Convert.ToString(h.c["BANK_CODE"].Value);
                NOTES.Text = Convert.ToString(h.c["NOTES"].Value);
                if (TextDiscount.Visible)
                {
                    TextDiscount.Text = Convert.ToString(h.c["DISCOUNT"].Value);
                }
                TOTAL_PAID.Text = Convert.ToString(h.c["TOTAL_PAID"].Value);
                TOTAL_CURRENT.Text = Convert.ToString(h.c["TOTAL_CURRENT"].Value);
                TOTAL_BALANCE.Text = Convert.ToString(h.c["TOTAL_BALANCE"].Value);
                if (fld == "SUP_CODE")
                {
                    GetLedgerId(Convert.ToString(h.c["SUP_CODE"].Value));
                }
                else
                {
                    GetLedgerId(Convert.ToString(h.c["SUP_CODE"].Value));
                }
            }
            dgClients.Visible = false;
            edit = false;

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
            //dgDetail.Rows.Clear();
            ID = "";
            btnDelete.Enabled = false;
            DOC_NO.Text = "";
            SUP_CODE.Text = "";
            SUP_NAME.Text = "";
            PAY_CODE.Text = "CSH";
            PAY_NAME.Text = "CASH";
            CHQ_NO.Text = "";
            CHQ_DATE.Value = DateTime.Now;
            DOC_DATE_GRE.Value = DateTime.Now;
            DOC_DATE_HIJ.Text = "";
            CUR_CODE.Text = "";
            AMOUNT.Text = "";
            BANK_CODE.Text = "";
            NOTES.Text = "";
            TOTAL_PAID.Text = "0.00";
            TOTAL_CURRENT.Text = "0.00";
            TOTAL_BALANCE.Text = "0.00";
            PARTYACC.SelectedIndex = 0;
            prev = 0;
            CASHACC.SelectedValue = "21";
            edit = false;
            PARTYACC.Focus();
           // DOC_DATE_GRE.Text = ComSet.GettDate();
            if (TextDiscount.Visible)
            {
                TextDiscount.Text = "0";
            }
            bindledgers();
            CASHACC.SelectedValue = "21";

            GetBranchDetails();
            // CASHACC.Enabled = false;
            CASHACC.Enabled = true;
            foreach (DataGridViewColumn col in dgDetail.Columns)
            {
                col.Width = 150;
            }
            if (fld.Equals("SUP_CODE"))
            {
                GetMaxPayVouch();
                dgDetail.Columns["cName"].HeaderText = "SUPPLIER NAME";
                dgDetail.Columns["cBalance"].HeaderText = "AMOUNT PAID";
                dgDetail.Columns["cPaidAmt"].HeaderText = "PAY CODE";
                bindDayPayments();
            }
            else if (fld.Equals("CUST_CODE"))
            {
                GetMaxRecVouch();
                dgDetail.Columns["cName"].HeaderText = "CUSTOMER NAME";
                dgDetail.Columns["cBalance"].HeaderText = "AMOUNT RECEIVED";
                dgDetail.Columns["cPaidAmt"].HeaderText = "RECEIPT CODE";
                BindDayReceipts();
            }
            if (ID == "")
            {
                if (form == 0)
                {
                    // DOC_NO.Text = General.generatePayVoucherCode();
                    GetMaxPayVouch();
                }
                else
                {
                    // DOC_NO.Text = General.generateReceiptVoucherCode();
                    GetMaxRecVouch();

                }
            }
            DOC_DATE_GRE.Focus();
            ActiveControl = DOC_DATE_GRE;
            cmb_projects.SelectedValue = 0;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //btnClear.PerformClick();
            //PaymentVoucherHelp h = new PaymentVoucherHelp(1,form);
            //h.ShowDialog();
            if (ID != "")
            {
                if (PAY_CODE.Text != "CHQ")
                {
                    if (MessageBox.Show("Are you sure? you want to delete this?", "Record Deletion", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {

                        string id = DOC_NO.Text;
                        // String date = Convert.ToDateTime(dgItems.CurrentRow.Cells["DOC_DATE_GRE"].Value).ToString("MM/dd/yyyy");
                        String date = DOC_DATE_GRE.Text;
                        //dgItems.Rows.Remove(dgItems.CurrentRow);
                       // if (conn.State == ConnectionState.Open)
                       // {
                       //     conn.Close();
                      //  }
                      //  conn.Open();
                        if (form == 0)
                        {
                            //cmd.CommandText = "DELETE FROM PAY_PAYMENT_VOUCHER_HDR WHERE DOC_NO = '" + id + "';DELETE FROM PAY_PAYMENT_VOUCHER_DTL WHERE DOC_NO = '" + id + "'";
                            payvchrhdrdb.DocNo = id;
                            payvchrhdrdb.deleteByDocNo();
                        }
                        //if (frm == 1)
                        //{
                        //    cmd.CommandText = "DELETE FROM PAY_PAYMENT_VOUCHER_HDR WHERE DOC_NO = '" + id + "';DELETE FROM PAY_PAYMENT_VOUCHER_DTL WHERE DOC_NO = '" + id + "'";
                        //}
                        else
                        {
                           // cmd.CommandText = "DELETE FROM REC_RECEIPTVOUCHER_HDR WHERE DOC_NO = '" + id + "';DELETE FROM REC_RECEIPTVOUCHER_DTL WHERE DOC_NO = '" + id + "'";
                            rrvhdb.DocNo = id;
                            rrvhdb.deleteByDocNo();
                        }
                       // cmd.ExecuteNonQuery();
                       // conn.Close();
                        AddtoDeletedTransaction(id);
                        modifiedtransaction(id, date);
                        DeleteTransation(id);
                        if (form != 0)
                        {
                            if (TextDiscount.Visible)
                            {
                                 trans.VOUCHERTYPE = "DISCOUNT GIVEN";
                                 trans.VOUCHERNO = id;
                                 trans.DeletePurchaseTransaction();
                            }
                        }
                        btnClear.PerformClick();

                    }
                }
                else
                {
                    MessageBox.Show("Please Use Cheque report to Delete the cheque..!", "Sysbizz", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private void DeleteTransation(string Id)
        {
            if (form == 0)
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
        public void modifiedtransaction(string ID, string date)
        {
            if (form == 0)
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
            modtrans.Date = DOC_DATE_GRE.Value.ToString(); 
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
            if (form == 0)
            {
                vchr = "Cash Payment";
            }
            // if (frm == 1)
            //{
            //    vchr = "Salary Payment";
            //}
            else
            {
                vchr = "Cash Receipt";
            }
            // conn.Open();
         //-->   cmd.CommandText = "insert into     tbl_deletedTransaction(VOUCHERNO, VOUCHERTYPE, ACCNAME, PARTICULARS, DEBIT, CREDIT, DATED, NARRATION, USERID, SYSTEMTIEM, ACCID) select    VOUCHERNO, VOUCHERTYPE, ACCNAME, PARTICULARS, DEBIT, CREDIT, DATED, NARRATION, USERID, SYSTEMTIEM, ACCID from tb_Transactions where VOUCHERNO='" + id + "' and VOUCHERTYPE='" + vchr + "'";
            // cmd.CommandText = "DELETE FROM INV_PURCHASE_HDR WHERE DOC_NO = '" + id + "';DELETE FROM INV_PURCHASE_DTL WHERE DOC_NO = '" + id + "';DELETE FROM INV_STK_TRX_HDR WHERE DOC_REFERENCE = '" + id + "';DELETE FROM INV_STK_TRX_DTL WHERE DOC_REFERENCE = '" + id + "'";
         //-->   cmd.ExecuteNonQuery();
            //  MessageBox.Show("Record Deleted!");
           // conn.Close();
            dltdTranObj.VoucherNo = id;
            dltdTranObj.VoucherType = vchr;
            dltdTranObj.insertDeletedTran();




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
            DataTable dt = new DataTable();
            dt = ComSet.GetCurrentBranchDetails();
            Addres1 = dt.Rows[0][1].ToString();
            Addres2 = dt.Rows[0][2].ToString();
            Phone = dt.Rows[0][3].ToString();
            Email = dt.Rows[0][4].ToString();
            Fax = dt.Rows[0][5].ToString();
            CUR_CODE.Text = dt.Rows[0]["DEFAULT_CURRENCY_CODE"].ToString();
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
            if (CompanyName == "")
            {
                GetCompanyDetails();
                GetBranchDetails();
            }

            try
            {
                int height = (dgDetail.Rows.Count - 1) * 23;
                if (PrintPage.SelectedIndex == 0)
                {
                    PrintDocument printDocument = new PrintDocument();
                    printDocument.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("smallzize", 650, height + 300);
                    printDialog1.Document = printDocument;
                    printDocument.PrintPage += printDocument_PrintPage;
                    DialogResult result = printDialog1.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        printDocument.Print();
                    }
                }
                else
                {
                    PrintDocument printDocumentA4 = new PrintDocument();
                    PaperSize ps = new PaperSize();
                    ps.RawKind = (int)PaperKind.A4;
                    printDocumentA4.DefaultPageSettings.PaperSize = ps;
                    printDialog1.Document = printDocumentA4;
                    printDocumentA4.PrintPage += printDocumentA4_PrintPage;
                    printDocumentA4.Print();
                }
            }
            catch
            {
                MessageBox.Show("Printing Problem");
            }
        }

        private void printDocumentA4_PrintPage(object sender, PrintPageEventArgs e)
        {
           // throw new NotImplementedException();


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

                //System.Drawing.Image img = System.Drawing.Image.FromFile(logo);

                //Point loc = new Point(20, 50);
                //e.Graphics.DrawImage(img, new Rectangle(50, 50, 50, 50));
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
                e.Graphics.DrawString(title + ": ", Headerfont2, new SolidBrush(tabDataForeColor), xpos, starty + offset - 24, sf);
                offset = offset + 16;
                Pen blackPen = new Pen(Color.Black, 1);
                Point point1 = new Point(0, 185);
                Point point2 = new Point(900, 185);
                e.Graphics.DrawLine(blackPen, point1, point2);




                e.Graphics.DrawString("To:" + SUP_NAME.Text, Headerfont2, new SolidBrush(tabDataForeColor), startx, starty + offset - 36);
                Font itemhead = new Font("Times New Roman", 8);
                offset = offset + 6;

                Point point3 = new Point(45, 219);
                Point point4 = new Point(760, 219);
                e.Graphics.DrawLine(blackPen, point3, point4);
                e.Graphics.DrawLine(blackPen, 45, 219, 45, 900);
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
            e.Graphics.DrawString(AMOUNT.Text, Headerfont2, new SolidBrush(Color.Black), startx + 630, starty + offset + 3);
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

               // System.Drawing.Image img = System.Drawing.Image.FromFile(logo);

               // Point loc = new Point(20, 50);
                //e.Graphics.DrawImage(img, new Rectangle(50, 50, 50, 50));
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




                e.Graphics.DrawString("To:" + SUP_NAME.Text, Headerfont2, new SolidBrush(tabDataForeColor), startx, starty + offset - 36);
                Font itemhead = new Font("Times New Roman", 8);
                offset = offset + 6;

                Point point3 = new Point(45, 219);
                Point point4 = new Point(760, 219);
                e.Graphics.DrawLine(blackPen, point3, point4);
                e.Graphics.DrawLine(blackPen, 45, 219, 45, 900);
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
            int starty = 25;
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
                    string type = "";
                    string party="";
                    if (title.Equals("Payment Voucher"))
                    {
                        type = "Pay To:";
                        party=PARTYACC.Text;
                    }
                    else
                    {
                        type = "Received From:";
                        party=PARTYACC.Text;
                    }
                    e.Graphics.DrawString(type+ party, printFont, new SolidBrush(tabDataForeColor), startx, starty + offset);
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
                    e.Graphics.DrawString("Authorized Signature", printFont, new SolidBrush(Color.Black), startx + 423, starty + offset);
                    e.Graphics.DrawString("Total", printFont, new SolidBrush(Color.Black), startx, starty + offset-3);
                    e.Graphics.DrawString(TOTAL_BALANCE.Text, printFont, new SolidBrush(Color.Black), startx + 90, starty + offset - 3);
                    offset = offset + 12;
                    e.Graphics.DrawString("Total Piad", printFont, new SolidBrush(Color.Black), startx, starty + offset);
                    e.Graphics.DrawString(AMOUNT.Text, printFont, new SolidBrush(Color.Black), startx + 90, starty + offset);
                    offset = offset + 12;
                    e.Graphics.DrawString("Balance", printFont, new SolidBrush(Color.Black), startx, starty + offset);
                    e.Graphics.DrawString((Convert.ToDecimal(TOTAL_BALANCE.Text) - Convert.ToDecimal(AMOUNT.Text)).ToString(), printFont, new SolidBrush(Color.Black), startx + 90, starty + offset);
                }
                e.HasMorePages = false;
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
                }
            }
            catch
            {
                this.Close();
            }


        }

        private void SUP_NAME_TextChanged(object sender, EventArgs e)
        {
            //if (SUP_NAME.Text.Trim() == "")
            //{
            //    dgClients.Visible = false;
            //    source.Filter = "";
            //    if (kryptonLabel2.Text == "Customer")
            //        BindDayReceipts();
            //    else if (kryptonLabel2.Text == "Supplier")
            //        bindDayPayments();

            //}
            //else
            //{
            //    source.Filter = string.Format("[DESC_ENG] LIKE '%{0}%' ", SUP_NAME.Text);

            //    dgClients.Visible = true;
            //    if (dgClients.Rows.Count > 0)
            //    {
            //        dgClients.CurrentCell = dgClients.Rows[0].Cells[1];
            //        dgClients.Visible = false;
            //    }
            //}
        }

        private void SUP_NAME_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Common.preventDingSound(e);
                if (dgClients.CurrentRow != null)
                {
                    SUP_CODE.Text = dgClients.CurrentRow.Cells[0].Value.ToString();
                    PARTYACC.SelectedValue = dgClients.CurrentRow.Cells["LedgerId"].Value.ToString();
                    SUP_NAME.Text = dgClients.CurrentRow.Cells[1].Value.ToString();
                    dgClients.Visible = false;

                    if (form == 0)
                    {
                        //cmd.CommandText = "SUP_PAIDAMNTS";
                        //cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.Parameters.Clear();
                        //cmd.Parameters.AddWithValue("@SUP_CODE", SUP_CODE.Text);
                        //conn.Open();
                        //SqlDataReader r = cmd.ExecuteReader();
                        string cmd = "SUP_PAIDAMNTS";
                        payvchrhdrdb.SupCode = SUP_CODE.Text;
                        SqlDataReader r = payvchrhdrdb.supCreditPurchaseProcedure(cmd, "@SUP_CODE");
                        dgDetail.Rows.Clear();
                        double totalPaid = 0;
                        double totalBal = 0;
                        while (r.Read())
                        {
                            dgDetail.Rows.Add(r["DOC_NO"], r["DOC_DATE_GRE"], r["DOC_DATE_HIJ"], r["SUP_CODE"], r["PAY_CODE"], r["CHQ_NO"], Convert.ToDouble(r["AMOUNT"]).ToString(decimalFormat));
                            totalPaid = totalPaid + Convert.ToDouble(r["AMOUNT"]);
                        }
                        //conn.Close();
                        DbFunctions.CloseConnection();

                        //cmd.CommandText = "SUP_CREDIT_PUR";
                        //cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.Parameters.Clear();
                        //cmd.Parameters.AddWithValue("@SUP_CODE", SUP_CODE.Text);
                        //conn.Open();
                        //SqlDataReader r1 = cmd.ExecuteReader();
                        string cmds = "SUP_CREDIT_PUR";
                        payvchrhdrdb.SupCode = SUP_CODE.Text;
                        SqlDataReader r1 = payvchrhdrdb.supCreditPurchaseProcedure(cmds, "@SUP_CODE");

                        while (r1.Read())
                        {
                            totalBal = totalBal + Convert.ToDouble(r1["NET_VAL"]);
                        }
                       // conn.Close();
                        DbFunctions.CloseConnection();
                        TOTAL_CURRENT.Text = totalBal.ToString();
                        TOTAL_PAID.Text = totalPaid.ToString();
                        TOTAL_BALANCE.Text = (totalBal - totalPaid).ToString();
                        dgClients.Visible = false;
                    }
                    else
                    {
                        //conn.Open();
                        //cmd.CommandText = "CUS_PAIDAMNTS";
                        //cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.Parameters.Clear();
                        //cmd.Parameters.AddWithValue("@CUS_CODE", SUP_CODE.Text);
                        //SqlDataReader r = cmd.ExecuteReader();
                        //dgDetail.Rows.Clear();
                        string cmdd = "CUS_PAIDAMNTS";
                        payvchrhdrdb.SupCode = SUP_CODE.Text;
                        SqlDataReader r = payvchrhdrdb.supCreditPurchaseProcedure(cmdd, "@CUS_CODE");

                        double totalPaid = 0;
                        double totalBal = 0;
                        while (r.Read())
                        {
                            dgDetail.Rows.Add(r["DOC_NO"], r["DOC_DATE_GRE"], r["DOC_DATE_HIJ"], r["CUST_CODE"], r["PAY_CODE"], r["CHQ_NO"], Convert.ToDouble(r["AMOUNT"]).ToString(decimalFormat));
                            totalPaid = totalPaid + Convert.ToDouble(r["AMOUNT"]);
                        }
                        //conn.Close();
                        DbFunctions.CloseConnection();
                        //conn.Open();
                        //cmd.CommandText = "CUS_CREDIT_PUR";
                        //cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.Parameters.Clear();
                        //cmd.Parameters.AddWithValue("@CUS_CODE", SUP_CODE.Text);
                        //SqlDataReader r2 = cmd.ExecuteReader();
                        string command = "CUS_CREDIT_PUR";
                        payvchrhdrdb.SupCode = SUP_CODE.Text;
                        SqlDataReader r2 = payvchrhdrdb.supCreditPurchaseProcedure(command, "@CUS_CODE");

                        while (r2.Read())
                        {
                            totalBal = totalBal + Convert.ToDouble(r2["NET_AMOUNT"]);
                        }
                        TOTAL_CURRENT.Text = totalBal.ToString();
                        TOTAL_PAID.Text = totalPaid.ToString();
                        TOTAL_BALANCE.Text = (totalBal - totalPaid).ToString();
                        //conn.Close();
                        DbFunctions.CloseConnection();
                    }
                    AMOUNT.Focus();
                    CASHACC.SelectedValue = "21";
                }
            }
            else if(e.KeyCode == Keys.Down)
            {
                if (dgClients.CurrentRow != null)
                {
                    if (dgClients.CurrentRow.Index < dgClients.Rows.Count - 1)
                    {
                        dgClients.CurrentCell = dgClients.Rows[dgClients.CurrentRow.Index + 1].Cells[1];
                    }
                    else
                    {
                        dgClients.CurrentCell = dgClients.Rows[0].Cells[1];
                    }
                }
                Common.preventDingSound(e);
            }
            else if (e.KeyCode == Keys.Up)
            {
                if (dgClients.Visible)
                {
                    if (dgClients.CurrentRow != null)
                    {
                        if (dgClients.CurrentRow.Index > 0)
                        {
                            dgClients.CurrentCell = dgClients.Rows[dgClients.CurrentRow.Index - 1].Cells[1];
                        }
                        else
                        {
                            dgClients.CurrentCell = dgClients.Rows[dgClients.Rows.Count - 1].Cells[1];
                        }
                    }
                }
                Common.preventDingSound(e);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                dgClients.Visible = false;
                Common.preventDingSound(e);
            }
            else
            {
                fillCustomerSupplierAutoSuggest();
                dgClients.Visible = true;
            }
        }

        private void fillCustomerSupplierAutoSuggest()
        {
            if (fld == "SUP_CODE")
            {
                BindSupplier();
            }
            else
            {
                BindCustomer();
            }
            bindledgers();
        }

        private void dgClients_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                SUP_NAME.Focus();
                dgClients.Visible = false;
            }
        }

        int movetincriment = 0;
        private void AMOUNT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (PAY_CODE.Text == "CSH")
                {
                    NOTES.Focus();
                }
                else
                {
                    CHQ_NO.Focus();
                }
              
               // btnSave.Focus();
            }
        }

        private void dgClients_Leave(object sender, EventArgs e)
        {
            dgClients.Visible = false;
        }

        private void CASHACC_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectBank();
        }

        private void PARTYACC_KeyDown(object sender, KeyEventArgs e)
        {
            PARTYACC.DroppedDown = false;
            if (e.KeyCode == Keys.Enter)
            {
                CASHACC.Focus(); 
            }
        }

        private void CASHACC_KeyDown(object sender, KeyEventArgs e)
        {
            CASHACC.DroppedDown = false;
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
               // PARTYACC.Focus();
                btnPay.Focus();
            }
        }

        private void ChekPrint_CheckedChanged(object sender, EventArgs e)
        {
            if (ChekPrint.Checked)
            {
                try
                {
                    DataTable dt = new DataTable();
                    //cmd.Connection = conn;
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
            //SqlConnection recConn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
           // SqlCommand recCmd = new SqlCommand();
           // recCmd.Connection = recConn;
           // recConn.Open();
            // recCmd.CommandText = "SELECT ROW_NUMBER() Over (Order by DOC_NO) as 'Sl_No',REC_NO AS 'VOUCHER_NO',DOC_NO, CONVERT(NVARCHAR(50), DOC_DATE_GRE, 103) AS DOC_DATE_GRE, DOC_DATE_HIJ, SUP_CODE,DESC1 DESC_ENG,DESC2 DESC_ENG2, AMOUNT, CHQ_NO, PAY_CODE FROM PAY_PAYMENT_VOUCHER_HDR LEFT JOIN PAY_SUPPLIER ON CODE=SUP_CODE WHERE DOC_DATE_GRE = @DATE";
           payvchrhdrdb.DocDateGre = DOC_DATE_GRE.Value;
           // recCmd.CommandType = CommandType.Text;
          //  recCmd.Parameters.Clear();
          //  recCmd.Parameters.AddWithValue("@DATE", DOC_DATE_GRE.Value);
            DataTable r2 = payvchrhdrdb.bindDayPayment();
            dgDetail.Rows.Clear();
            for (int i = 0; i < r2.Rows.Count;i++)
            {
                dgDetail.Rows.Add(r2.Rows[i]["DOC_NO"], r2.Rows[i]["DOC_DATE_GRE"], r2.Rows[i]["DOC_DATE_HIJ"], r2.Rows[i]["SUP_CODE"], r2.Rows[i]["DESC_ENG"], r2.Rows[i]["PAY_CODE"], r2.Rows[i]["CHQ_NO"], Convert.ToDouble(r2.Rows[i]["AMOUNT"]).ToString(decimalFormat), r2.Rows[i]["Sl_No"], r2.Rows[i]["DESC_ENG2"], r2.Rows[i]["VOUCHER_NO"]);
            }
           // recConn.Close();
            //DbFunctions.CloseConnection();
        }

        public void RemoveRows()
        {
            for (int i = 0; i < dgClients.Rows.Count; i++)
            {
                dgClients.Rows.RemoveAt(i);
            }
        }

        public void BindDayReceipts()
        {
           // SqlConnection recConn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
           // SqlCommand recCmd = new SqlCommand();
           // recCmd.Connection = recConn;
           // recConn.Open();
             // recCmd.CommandText = "SELECT ROW_NUMBER() Over (Order by DOC_NO) as 'Sl_No',REC_NO AS 'VOUCHER_NO',DOC_NO, CONVERT(NVARCHAR(50), DOC_DATE_GRE, 103) AS DOC_DATE_GRE, DOC_DATE_HIJ, CUST_CODE,DESC2 AS DESC_ENG,DESC1 AS DESC_ENG1, PAY_CODE, AMOUNT, CHQ_NO FROM REC_RECEIPTVOUCHER_HDR LEFT JOIN REC_CUSTOMER ON CODE = CUST_CODE WHERE  DOC_DATE_GRE  = @DATE";
            // recCmd.CommandType = CommandType.Text;
           // recCmd.Parameters.Clear();
            //recCmd.Parameters.AddWithValue("@DATE", DOC_DATE_GRE.Value);
          //  recCmd.Parameters.Add("@DATE", SqlDbType.Date).Value = DOC_DATE_GRE.Value;
            payvchrhdrdb.DocDateGre = DOC_DATE_GRE.Value ;
            //SqlDataReader r2 = recCmd.ExecuteReader();
            SqlDataReader r2 = payvchrhdrdb.bindDayReciept();
            dgDetail.Rows.Clear();
            while (r2.Read())
            {
                dgDetail.Rows.Add(r2["DOC_NO"], Convert.ToString(r2["DOC_DATE_GRE"]), Convert.ToString(r2["DOC_DATE_HIJ"]), r2["CUST_CODE"], r2["DESC_ENG"], r2["PAY_CODE"], r2["CHQ_NO"], Convert.ToDouble(r2["AMOUNT"]).ToString(decimalFormat), r2["Sl_No"], r2["DESC_ENG1"], r2["VOUCHER_NO"]);
            }
            //recConn.Close();
            DbFunctions.CloseConnection();
        }

        private void DOC_DATE_GRE_ValueChanged(object sender, EventArgs e)
        {
            dgDetail.Rows.Clear();
            if (fld == "SUP_CODE")
            {
                bindDayPayments();
            }
            else
            {
                BindDayReceipts();
            }
            dgDetail.Columns["cBalance"].DisplayIndex = 5;
            dgDetail.Columns["cDateGRE"].Width = 95;
            dgDetail.Columns["cDateHIJ"].Width = 10;
            dgDetail.Columns["cInvAmt"].Width = 80;
            dgDetail.Columns["cName"].Width = 170;
            dgDetail.Columns["cBalance"].Width = 100;
            dgDetail.Columns["cCurrent"].Width = 150;
            dgDetail.Columns["cPaidAmt"].Width = 55;
            dgDetail.Columns["SL_NO"].DisplayIndex = 0;
            dgDetail.Columns["VOUCHER"].DisplayIndex = 1;
            dgDetail.Columns["ACCOUNT"].DisplayIndex = 6;
            dgDetail.Columns["SL_NO"].Width = 40;
            dgDetail.Columns["VOUCHER"].Width = 70;
            dgDetail.Columns["ACCOUNT"].Width = 150;
        }

        private void PARTYACC_Leave(object sender, EventArgs e)
        {
            //if (PARTYACC.SelectedIndex < 0)
            //{
            //    PARTYACC.Focus();
            //}
        }

        private void bt_Print_Click(object sender, EventArgs e)
        {
            if (ChekPrint.Checked == true)
            {
                printingrecipt();
            }
        }

        private void PAY_CODE_TextChanged(object sender, EventArgs e)
        {
            if (PAY_CODE.Text == "CSH")
            {
                panel3.Visible = false;
                lblChqNo.Visible = false;
                CHQ_NO.Visible = false;
                lblChqDate.Visible = false;
                CHQ_DATE.Visible = false;
                lblAccDetails.Visible = false;
                txtAccDetails.Visible = false;
               // bindledgers();
                DataTable dt = new DataTable();
              //  cmd = new SqlCommand("SELECT DISTINCT LEDGERID,LEDGERNAME FROM tb_Ledgers", conn);
                
              //  SqlDataAdapter adapter = new SqlDataAdapter(cmd);
             //   adapter.Fill(dt);
                dt = ldgObj.SelectDIstictLedgerName();
                DataRow row = dt.NewRow();
                dt.Rows.InsertAt(row, 0);
                CASHACC.DataSource = dt;
                CASHACC.DisplayMember = "LEDGERNAME";
                CASHACC.ValueMember = "LEDGERID";
            }
            else if (PAY_CODE.Text == "CHQ")
            {
                panel3.Visible = true;
               
                lblChqNo.Visible = true; ;
                CHQ_NO.Visible = true;
                lblChqDate.Visible = true;
                CHQ_DATE.Visible = true;
                lblAccDetails.Visible = true;
                txtAccDetails.Visible = true;
                if (fld.Equals("SUP_CODE"))
                {
                  //  ChequeAccount("payable");
                    BankAccount();
                }
                else
                {
                   // ChequeAccount("receivable");
                    BankAccount();
                }
                //bindledgers();
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
                lblChqNo.Visible = false;
                CHQ_NO.Visible = false;
                lblChqDate.Visible = false;
                CHQ_DATE.Visible = false;
                lblAccDetails.Visible = true;
                lblAccDetails.Text = "Account No.";
                txtAccDetails.Visible = true;
                BankAccount();
            }
        }

        private void ChequeAccount(string type)
        {
           DataTable dt = new DataTable();
           /*   if (type.Equals("payable"))
             {
                 cmd = new SqlCommand("SELECT DISTINCT LEDGERID,LEDGERNAME FROM tb_Ledgers WHERE UNDER IN (22)", conn);
             }
             else
             {
                 cmd = new SqlCommand("SELECT DISTINCT LEDGERID,LEDGERNAME FROM tb_Ledgers WHERE UNDER IN (10)", conn);
             }
             SqlDataAdapter adapter = new SqlDataAdapter(cmd);
             adapter.Fill(dt);
 */
            dt = ldgObj.getDistinctLedg(type);
            DataRow row = dt.NewRow();
            dt.Rows.InsertAt(row, 0);
            CASHACC.DataSource = dt;
            CASHACC.DisplayMember = "LEDGERNAME";
            CASHACC.ValueMember = "LEDGERID";
        }

        private void BankAccount()
        {
            //SqlConnection connBank = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
           // SqlCommand cmdBank = new SqlCommand();
            //cmd.Connection = connBank;
            DataTable dt = new DataTable();

           // cmdBank = new SqlCommand("SELECT DISTINCT LEDGERID,LEDGERNAME FROM tb_Ledgers WHERE UNDER IN (10,21,22,20,71)", connBank);
           // SqlDataAdapter adapter = new SqlDataAdapter(cmdBank);
          //  adapter.Fill(dt);
            dt = ldgObj.getDIstinctBankAccountLedg();
            DataRow row = dt.NewRow();
            dt.Rows.InsertAt(row, 0);
            CASHACC.DataSource = dt;
            CASHACC.DisplayMember = "LEDGERNAME";
            CASHACC.ValueMember = "LEDGERID";
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text.Trim() == "")
            {
                source.Filter = "";
            }
            else
            {
                source.Filter = string.Format("DESC_ENG LIKE '"+ txtSearch.Text +"%' ");
            }
        }

        private void DOC_NO_TextChanged(object sender, EventArgs e)
        {

            
        }

        private void PARTYACC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (fld.Equals("SUP_CODE"))
            {
                if (PARTYACC.SelectedIndex > 0)
                {
                    SUP_CODE.Text = "";
                    SUP_NAME.Text = "";
                    //DataTable dt = new DataTable();
                    //cmd = new SqlCommand("SELECT  CODE,DESC_ENG from PAY_SUPPLIER WHERE LedgerId=" + Convert.ToInt32(PARTYACC.SelectedValue), conn);
                    //SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    //adapter.Fill(dt);
                    //if (dt.Rows.Count > 0)
                    //{
                    //    SUP_CODE.Text = dt.Rows[0][0].ToString();
                    //     SUP_NAME.Text = dt.Rows[0][1].ToString();
                    //}
                    SUP_CODE.Text = PARTYACC.SelectedValue.ToString();
                    SUP_NAME.Text = PARTYACC.Text;
                    DataTable dt1 = new DataTable();

                 //   SqlCommand cmd1 = new SqlCommand("SELECT PAY_SUPPLIER.CODE,PAY_SUPPLIER.DESC_ENG,PAY_SUPPLIER.MOBILE,ISNULL(CREDIT.total_CREDIT,0) AS total_CREDIT,ISNULL(DEBIT.total_DEBIT,0) AS total_DEBIT,(ISNULL(CREDIT.total_CREDIT,0)-ISNULL(DEBIT.total_DEBIT,0)) AS BALANCE  FROM PAY_SUPPLIER LEFT OUTER JOIN (SELECT ACCID,ISNULL(SUM(CREDIT),0) as total_CREDIT FROM tb_Transactions GROUP BY ACCID) AS CREDIT ON PAY_SUPPLIER.LedgerId=CREDIT.ACCID  LEFT OUTER JOIN (SELECT ACCID,ISNULL(SUM(DEBIT),0) as total_DEBIT FROM tb_Transactions GROUP BY ACCID) AS DEBIT ON PAY_SUPPLIER.LedgerId=DEBIT.ACCID WHERE PAY_SUPPLIER.LedgerId=" + Convert.ToInt32(PARTYACC.SelectedValue) + " ORDER BY PAY_SUPPLIER.CODE", conn);
                 //   SqlDataAdapter adapter1 = new SqlDataAdapter(cmd1);
                 //   adapter1.Fill(dt1);
                    paysupdb.LedgerId = PARTYACC.SelectedValue.ToString(); ;
                    dt1 = paysupdb.getDataPartyAcc();
                    if (dt1.Rows.Count > 0)
                    {
                        TOTAL_PAID.Text = dt1.Rows[0]["total_DEBIT"].ToString();
                        TOTAL_CURRENT.Text = dt1.Rows[0]["total_CREDIT"].ToString();
                        TOTAL_BALANCE.Text = dt1.Rows[0]["BALANCE"].ToString();
                    }
                    else
                    {
                        TOTAL_PAID.Text = "0.00";
                        TOTAL_CURRENT.Text = "0.00";
                        TOTAL_BALANCE.Text = "0.00";
                    }
                }
            }
            else
            {
                if (PARTYACC.SelectedIndex > 0)
                {
                    SUP_CODE.Text = "";
                    SUP_NAME.Text = "";
                    //DataTable dt = new DataTable();
                    //cmd = new SqlCommand("SELECT  CODE,DESC_ENG from REC_CUSTOMER WHERE LedgerId=" + Convert.ToInt32(PARTYACC.SelectedValue), conn);
                    //SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    //adapter.Fill(dt);
                    //if (dt.Rows.Count > 0)
                    //{
                    //    SUP_CODE.Text = dt.Rows[0][0].ToString();
                    //    //  SUP_NAME.Text = dt.Rows[0][1].ToString();
                    //}

                    SUP_CODE.Text = PARTYACC.SelectedValue.ToString();
                    SUP_NAME.Text = PARTYACC.Text;
                    DataTable dt1 = new DataTable();

                   // SqlCommand cmd1 = new SqlCommand("SELECT REC_CUSTOMER.CODE,REC_CUSTOMER.DESC_ENG,REC_CUSTOMER.MOBILE,ISNULL(DEBIT.total_DEBIT,0) AS total_DEBIT,ISNULL(CREDIT.total_CREDIT,0) AS total_CREDIT,(ISNULL(DEBIT.total_DEBIT,0)-ISNULL(CREDIT.total_CREDIT,0)) AS BALANCE  FROM REC_CUSTOMER LEFT OUTER JOIN (SELECT ACCID,ISNULL(SUM(DEBIT),0) as total_DEBIT FROM tb_Transactions GROUP BY ACCID) AS DEBIT ON REC_CUSTOMER.LedgerId=DEBIT.ACCID  LEFT OUTER JOIN (SELECT ACCID,ISNULL(SUM(CREDIT),0) as total_CREDIT FROM tb_Transactions GROUP BY ACCID) AS CREDIT ON REC_CUSTOMER.LedgerId=CREDIT.ACCID WHERE REC_CUSTOMER.LedgerId=" + Convert.ToInt32(PARTYACC.SelectedValue) + " ORDER BY REC_CUSTOMER.CODE", conn);
                   // SqlDataAdapter adapter1 = new SqlDataAdapter(cmd1);
                  //  adapter1.Fill(dt1);
                    payvchrhdrdb.LedgeId = Convert.ToInt32(PARTYACC.SelectedValue);
                    dt1 = payvchrhdrdb.getCustomerByLedgNo();

                    if (dt1.Rows.Count > 0)
                    {
                        TOTAL_CURRENT.Text= dt1.Rows[0]["total_DEBIT"].ToString();
                        TOTAL_PAID.Text = dt1.Rows[0]["total_CREDIT"].ToString();
                        TOTAL_BALANCE.Text = dt1.Rows[0]["BALANCE"].ToString();
                    }
                    else
                    {
                        TOTAL_PAID.Text = "0.00";
                        TOTAL_CURRENT.Text = "0.00";
                        TOTAL_BALANCE.Text = "0.00";
                    }
                }
            }
        }

        private void btn_plus_Click(object sender, EventArgs e)
        {
            if (form == 0)
            {

               // cmd.CommandText = "SELECT ISNULL(MAX(CONVERT(DECIMAL(18,0),REC_NO)), 0) FROM PAY_PAYMENT_VOUCHER_HDR";
              //  cmd.CommandType = CommandType.Text;
              //  conn.Open();
                decimal value = Convert.ToDecimal(payvchrhdrdb.getMaxRecNo());
                
              //  conn.Close();
                if (Convert.ToDecimal(txtVoucherNo.Text)+1>= ++value)
                {
                    ID = "";
                    btnClear.PerformClick();
                    return;
                }
                else
                {
                    txtVoucherNo.Text = (Convert.ToDecimal(txtVoucherNo.Text) + 1).ToString();
                    //cmd = new SqlCommand("SELECT * FROM PAY_PAYMENT_VOUCHER_HDR LEFT OUTER JOIN GEN_PAYTYPE ON PAY_PAYMENT_VOUCHER_HDR.PAY_CODE=GEN_PAYTYPE.CODE WHERE REC_NO='" + txtVoucherNo.Text + "'", conn);
                    DataTable dt = new DataTable();
                   // SqlDataAdapter adptr = new SqlDataAdapter(cmd);
                   // adptr.Fill(dt);
                  //  conn.Close();
                    payvchrhdrdb.RecNo =Convert.ToDecimal(txtVoucherNo.Text);
                
                    dt = payvchrhdrdb.getAllPaymentVoucher();
                    if (dt.Rows.Count > 0)
                    {
                        DOC_NO.Text = dt.Rows[0]["DOC_NO"].ToString();
                        ID = DOC_NO.Text;
                        tableHDR = "PAY_PAYMENT_VOUCHER_HDR";
                        tableDTL = "PAY_PAYMENT_VOUCHER_DTL";
                        fld = "SUP_CODE";
                        title = "Payment Voucher";

                        PAY_CODE.Text = dt.Rows[0]["PAY_CODE"].ToString();
                        PAY_NAME.Text = dt.Rows[0]["DESC_ENG"].ToString();
                        previous_paycode = PAY_CODE.Text;
                        SUP_CODE.Text = dt.Rows[0]["DEBIT_CODE"].ToString();
                        //SUP_NAME.Text = PARTYACC.DisplayMember;
                        if (PAY_CODE.Text == "CHQ")
                        {
                            CHQ_DATE.Value = Convert.ToDateTime(dt.Rows[0]["CHQ_DATE"]);
                            CHQ_NO.Text = dt.Rows[0]["CHQ_NO"].ToString();
                        }
                        AMOUNT.Text = Convert.ToDecimal(dt.Rows[0]["AMOUNT"]).ToString("n2");
                        prev = Convert.ToDecimal(AMOUNT.Text);
                        prev_status = false;
                        CASHACC.SelectedValue = dt.Rows[0]["CREDIT_CODE"].ToString();
                        PARTYACC.SelectedValue = dt.Rows[0]["DEBIT_CODE"].ToString();
                        NOTES.Text = dt.Rows[0]["NOTES"].ToString();
                        if (Convert.ToInt32(dt.Rows[0]["PROJECTID"].ToString()) <= 0 || dt.Rows[0]["PROJECTID"] == null)
                        {
                            cmb_projects.SelectedIndex = 0;
                        }
                        else
                            cmb_projects.SelectedIndex = Convert.ToInt32(dt.Rows[0]["PROJECTID"]);
                        // conn.Open();
                        //  cmd = new SqlCommand("SELECT DOC_DATE_GRE FROM PAY_PAYMENT_VOUCHER_HDR WHERE REC_NO='" + txtVoucherNo.Text + "'", conn);
                        //   DOC_DATE_GRE.Value = Convert.ToDateTime(cmd.ExecuteScalar());
                        //    conn.Close();
                        payvchrhdrdb.RecNo = Convert.ToDecimal(txtVoucherNo.Text);
                        DOC_DATE_GRE.Value = Convert.ToDateTime(payvchrhdrdb.getDocDateGre());
                    }
                    else
                    {
                        MessageBox.Show(" entry deleted");
                      //  dgDetail.Rows.Clear();
                        PARTYACC.SelectedIndex = 0;

                        AMOUNT.Text = "";
                        TextDiscount.Text = "0";
                        SUP_NAME.Text = "";
                        DOC_NO.Text = "";
                    }
                }
            }
            else 
            {
                //cmd.CommandText = "SELECT ISNULL(MAX(CONVERT(DECIMAL(18,0),REC_NO)), 0) FROM REC_RECEIPTVOUCHER_HDR";
               // cmd.CommandType = CommandType.Text;
               // conn.Open();

                decimal value = Convert.ToDecimal(rrvhdb.getMaxRecNoRecVouch());
             //   conn.Close();
                if (Convert.ToDecimal(txtVoucherNo.Text) + 1 >= ++value)
                {
                    ID = "";
                    btnClear.PerformClick();
                    return;
                }
              
                else
                {
                   
                    txtVoucherNo.Text = (Convert.ToDecimal(txtVoucherNo.Text) + 1).ToString();
                   // conn.Open();
                   // cmd = new SqlCommand("SELECT * FROM REC_RECEIPTVOUCHER_HDR LEFT OUTER JOIN GEN_PAYTYPE ON REC_RECEIPTVOUCHER_HDR.PAY_CODE=GEN_PAYTYPE.CODE WHERE REC_NO='" + txtVoucherNo.Text + "'", conn);
                    DataTable dt = new DataTable();
                  //  SqlDataAdapter adptr = new SqlDataAdapter(cmd);
                  //  adptr.Fill(dt);
                  //  conn.Close();
                    rrvhdb.RecNo =Convert.ToDecimal(txtVoucherNo.Text);
                    dt = rrvhdb.getAllRecieptVoucher();
                    if (dt.Rows.Count > 0)
                    {
                        DOC_NO.Text = dt.Rows[0]["DOC_NO"].ToString();
                        ID = DOC_NO.Text;
                        tableHDR = "REC_RECEIPTVOUCHER_HDR";
                        tableDTL = "REC_RECEIPTVOUCHER_DTL";
                        fld = "CUST_CODE";
                        kryptonLabel2.Text = "Customer Code:";
                        title = "Receipt Voucher";
                        PAY_CODE.Text = dt.Rows[0]["PAY_CODE"].ToString();
                        PAY_NAME.Text = dt.Rows[0]["DESC_ENG"].ToString();
                        previous_paycode = PAY_CODE.Text;
                        SUP_CODE.Text = dt.Rows[0]["CREDIT_CODE"].ToString();
                        //SUP_NAME.Text = PARTYACC.DisplayMember;
                        if (PAY_CODE.Text == "CHQ")
                        {
                            CHQ_DATE.Value = Convert.ToDateTime(dt.Rows[0]["CHQ_DATE"]);
                            CHQ_NO.Text = dt.Rows[0]["CHQ_NO"].ToString();
                        }
                        AMOUNT.Text = Convert.ToDecimal(dt.Rows[0]["AMOUNT"]).ToString("n2");

                        if (TextDiscount.Visible)
                        {
                            TextDiscount.Text = dt.Rows[0]["DISCOUNT"].ToString();
                            
                        }

                        prev = Convert.ToDecimal(AMOUNT.Text);
                        prev_status = false;
                        PARTYACC.SelectedValue = dt.Rows[0]["CREDIT_CODE"].ToString();
                        CASHACC.SelectedValue = dt.Rows[0]["DEBIT_CODE"].ToString();
                        NOTES.Text = dt.Rows[0]["NOTES"].ToString();
                        if (Convert.ToInt32(dt.Rows[0]["PROJECTID"].ToString()) <= 0 || dt.Rows[0]["PROJECTID"] == null)
                        {
                            cmb_projects.SelectedIndex = 0;
                        }
                        else
                            cmb_projects.SelectedIndex = Convert.ToInt32(dt.Rows[0]["PROJECTID"]);
                        //conn.Open();
                        //  cmd = new SqlCommand("SELECT DOC_DATE_GRE FROM REC_RECEIPTVOUCHER_HDR WHERE REC_NO='" + txtVoucherNo.Text + "'", conn);
                        rrvhdb.RecNo = Convert.ToDecimal(txtVoucherNo.Text);
                        DOC_DATE_GRE.Value = Convert.ToDateTime(rrvhdb.getDocDateGre());
                        //    conn.Close();

                        SUP_NAME.Text = PARTYACC.SelectedText.ToString();
                    }
                    else
                    {
                        MessageBox.Show(" entry deleted");
                        //dgDetail.Rows.Clear();
                        AMOUNT.Text = "";
                        PARTYACC.SelectedIndex = 0;
                        TextDiscount.Text = "0";
                        SUP_NAME.Text = "";
                        DOC_NO.Text = "";
                    }
                }
               
            }
            this.Text = title;
            decimalFormat = Common.getDecimalFormat();
        }

        private void btn_minus_Click(object sender, EventArgs e)
        {
          //  conn.Open();
          //  cmd = new SqlCommand("SELECT VouchStartFrom FROM GEN_VOUCH_STARTFROM WHERE vouchtypecode='REC'", conn);
          //  string MIN_REC = Convert.ToString(cmd.ExecuteScalar());
            string MIN_REC = Convert.ToString(rrvhdb.getVouchStartFrom());
            //conn.Close();
           // conn.Open();
          //  cmd = new SqlCommand("SELECT VouchStartFrom FROM GEN_VOUCH_STARTFROM WHERE vouchtypecode='PAY'", conn);
          //  string MIN_PAY = Convert.ToString(cmd.ExecuteScalar());
         //   conn.Close();
            string MIN_PAY = Convert.ToString(payvchrhdrdb.getVouchStartFrom());
            decimal CURRENT = Convert.ToDecimal(txtVoucherNo.Text) - 1;
            if (form == 0)
            {
                if (CURRENT < Convert.ToDecimal(MIN_PAY))
                {
                  
                    return;
                }
                else
                {
                   
                    txtVoucherNo.Text = (Convert.ToDecimal(txtVoucherNo.Text) - 1).ToString();

                  //  cmd = new SqlCommand("SELECT * FROM PAY_PAYMENT_VOUCHER_HDR LEFT OUTER JOIN GEN_PAYTYPE ON PAY_PAYMENT_VOUCHER_HDR.PAY_CODE=GEN_PAYTYPE.CODE WHERE REC_NO='" + txtVoucherNo.Text + "'", conn);
                      DataTable dt = new DataTable();
                   // SqlDataAdapter adptr = new SqlDataAdapter(cmd);
                  //  adptr.Fill(dt);
                 //   conn.Close();
                      payvchrhdrdb.RecNo =Convert.ToDecimal(txtVoucherNo.Text);
                      dt = payvchrhdrdb.getAllPaymentVoucher();
                      if (dt.Rows.Count > 0)
                      {
                          btnDelete.Enabled = true;
                          DOC_NO.Text = dt.Rows[0]["DOC_NO"].ToString();
                          ID = DOC_NO.Text;
                          tableHDR = "PAY_PAYMENT_VOUCHER_HDR";
                          tableDTL = "PAY_PAYMENT_VOUCHER_DTL";
                          fld = "SUP_CODE";
                          title = "Payment Voucher";

                          PAY_CODE.Text = dt.Rows[0]["PAY_CODE"].ToString();
                          PAY_NAME.Text = dt.Rows[0]["DESC_ENG"].ToString();
                          previous_paycode = PAY_CODE.Text;
                          SUP_CODE.Text = dt.Rows[0]["DEBIT_CODE"].ToString();
                          //SUP_NAME.Text = PARTYACC.DisplayMember;
                          if (PAY_CODE.Text == "CHQ")
                          {
                              CHQ_DATE.Value = Convert.ToDateTime(dt.Rows[0]["CHQ_DATE"]);
                              CHQ_NO.Text = dt.Rows[0]["CHQ_NO"].ToString();
                          }
                          AMOUNT.Text = Convert.ToDecimal(dt.Rows[0]["AMOUNT"]).ToString("n2");
                          prev = Convert.ToDecimal(AMOUNT.Text);
                          prev_status = false;
                          CASHACC.SelectedValue = dt.Rows[0]["CREDIT_CODE"].ToString();
                          PARTYACC.SelectedValue = dt.Rows[0]["DEBIT_CODE"].ToString();
                          NOTES.Text = dt.Rows[0]["NOTES"].ToString();
                          if (dt.Rows[0]["PROJECTID"] == null || Convert.ToInt32(dt.Rows[0]["PROJECTID"].ToString()) <= 0)
                          {
                              cmb_projects.SelectedIndex = 0;
                          }
                          else
                              cmb_projects.SelectedIndex = Convert.ToInt32(dt.Rows[0]["PROJECTID"]);
                          // conn.Open();
                          //  cmd = new SqlCommand("SELECT DOC_DATE_GRE FROM PAY_PAYMENT_VOUCHER_HDR WHERE REC_NO='" + txtVoucherNo.Text + "'", conn);
                          //   DOC_DATE_GRE.Value = Convert.ToDateTime(cmd.ExecuteScalar());
                          payvchrhdrdb.RecNo = Convert.ToDecimal(txtVoucherNo.Text);
                          DOC_DATE_GRE.Value = Convert.ToDateTime(payvchrhdrdb.getDocDateGre());

                          // conn.Close();
                      }
                      else
                      {
                          MessageBox.Show(" entry deleted");
                          //  dgDetail.Rows.Clear();
                          AMOUNT.Text = "";
                          PARTYACC.SelectedIndex = 0;
                          TextDiscount.Text = "0";
                          SUP_NAME.Text = "";
                          DOC_NO.Text = "";
                      }
                }
            }
            else
            {

                if (Convert.ToDecimal(txtVoucherNo.Text) - 1 < Convert.ToDecimal(MIN_REC))
                {
                    return;
                }
                else
                {
                    txtVoucherNo.Text = (Convert.ToDecimal(txtVoucherNo.Text) - 1).ToString();
                    // conn.Open();
                    //  cmd = new SqlCommand("SELECT * FROM REC_RECEIPTVOUCHER_HDR LEFT OUTER JOIN GEN_PAYTYPE ON REC_RECEIPTVOUCHER_HDR.PAY_CODE=GEN_PAYTYPE.CODE WHERE REC_NO='" + txtVoucherNo.Text + "'", conn);
                    DataTable dt = new DataTable();
                    //  SqlDataAdapter adptr = new SqlDataAdapter(cmd);
                    //  adptr.Fill(dt);
                    //  conn.Close();
                    rrvhdb.RecNo = Convert.ToDecimal(txtVoucherNo.Text);
                    dt = rrvhdb.getAllRecieptVoucher();
                    if (dt.Rows.Count > 0)
                    {
                        btnDelete.Enabled = true;
                        DOC_NO.Text = dt.Rows[0]["DOC_NO"].ToString();
                        ID = DOC_NO.Text;
                        tableHDR = "REC_RECEIPTVOUCHER_HDR";
                        tableDTL = "REC_RECEIPTVOUCHER_DTL";
                        fld = "CUST_CODE";
                        kryptonLabel2.Text = "Customer Code:";
                        title = "Receipt Voucher";
                        PAY_CODE.Text = dt.Rows[0]["PAY_CODE"].ToString();
                        PAY_NAME.Text = dt.Rows[0]["DESC_ENG"].ToString();
                        previous_paycode = PAY_CODE.Text;
                        SUP_CODE.Text = dt.Rows[0]["CREDIT_CODE"].ToString();

                        //SUP_NAME.Text = PARTYACC.DisplayMember;
                        if (PAY_CODE.Text == "CHQ")
                        {
                            CHQ_DATE.Value = Convert.ToDateTime(dt.Rows[0]["CHQ_DATE"]);
                            CHQ_NO.Text = dt.Rows[0]["CHQ_NO"].ToString();
                        }
                        AMOUNT.Text = Convert.ToDecimal(dt.Rows[0]["AMOUNT"]).ToString("n2");
                        if (TextDiscount.Visible)
                        {
                            TextDiscount.Text = dt.Rows[0]["DISCOUNT"].ToString();
                        }
                        prev = Convert.ToDecimal(AMOUNT.Text);
                        prev_status = false;
                        PARTYACC.SelectedValue = dt.Rows[0]["CREDIT_CODE"].ToString();
                        CASHACC.SelectedValue = dt.Rows[0]["DEBIT_CODE"].ToString();
                        NOTES.Text = dt.Rows[0]["NOTES"].ToString();
                        if (Convert.ToInt32(dt.Rows[0]["PROJECTID"].ToString()) <= 0 || dt.Rows[0]["PROJECTID"] == null)
                        {
                            cmb_projects.SelectedIndex = 0;
                        }
                        else
                            cmb_projects.SelectedIndex = Convert.ToInt32(dt.Rows[0]["PROJECTID"]);
                        // conn.Open();
                        //  cmd = new SqlCommand("SELECT DOC_DATE_GRE FROM REC_RECEIPTVOUCHER_HDR WHERE REC_NO='" + txtVoucherNo.Text + "'", conn);
                        //  DOC_DATE_GRE.Value = Convert.ToDateTime(cmd.ExecuteScalar());
                        // conn.Close();
                        rrvhdb.RecNo = Convert.ToDecimal(txtVoucherNo.Text);
                        DOC_DATE_GRE.Value = Convert.ToDateTime(rrvhdb.getDocDateGre());

                        SUP_NAME.Text = PARTYACC.SelectedText.ToString();
                    }
                    else
                    {
                        MessageBox.Show(" entry deleted");
                        //dgDetail.Rows.Clear();
                        AMOUNT.Text = "";
                        TextDiscount.Text = "0";
                        SUP_NAME.Text = "";
                        DOC_NO.Text = "";
                        PARTYACC.SelectedIndex = 0;
                    }
                }

            }
        }

        private void NOTES_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSave.Focus();
                // btnSave.Focus();
            }
        }

        private void dgDetail_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            /*String doc_no = Convert.ToString(dgDetail.Rows[e.RowIndex].Cells[0].Value);
            btnClear.PerformClick();
            if (form == 0)
            {

                DOC_NO.Text = doc_no;
                GetACCID(doc_no, "Cash Payment");
                GetDataFromDocNO();
            }
            else
            {
                DOC_NO.Text = doc_no;
                GetACCIDDD(doc_no, "Cash Receipt");
                GetDataFromDocNO();
            }*/
        }

        private void CHQ_NO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
               
                   CHQ_DATE.Focus();
              
            }
        }

        private void CHQ_DATE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                txtAccDetails.Focus();

            }
        }

        private void txtAccDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                NOTES.Focus();

            }
        }

        private void CASHACC_Leave(object sender, EventArgs e)
        {
            if (CASHACC.SelectedIndex < 0)
            {
                CASHACC.Focus();
            }
        }
        void get_prev()
        {
            if (fld.Equals("SUP_CODE"))
            {
                if (PARTYACC.SelectedIndex > 0)
                {
                    SUP_CODE.Text = "";
                    SUP_NAME.Text = "";

                    SUP_CODE.Text = PARTYACC.SelectedValue.ToString();
                    SUP_NAME.Text = PARTYACC.Text;
                    DataTable dt1 = new DataTable();
                    //SqlCommand cmd1 = new SqlCommand("SELECT PAY_SUPPLIER.CODE,PAY_SUPPLIER.DESC_ENG,PAY_SUPPLIER.MOBILE,ISNULL(CREDIT.total_CREDIT,0) AS total_CREDIT,ISNULL(DEBIT.total_DEBIT,0) AS total_DEBIT,(ISNULL(CREDIT.total_CREDIT,0)-ISNULL(DEBIT.total_DEBIT,0)) AS BALANCE  FROM PAY_SUPPLIER LEFT OUTER JOIN (SELECT ACCID,ISNULL(SUM(CREDIT),0) as total_CREDIT FROM tb_Transactions GROUP BY ACCID) AS CREDIT ON PAY_SUPPLIER.LedgerId=CREDIT.ACCID  LEFT OUTER JOIN (SELECT ACCID,ISNULL(SUM(DEBIT),0) as total_DEBIT FROM tb_Transactions GROUP BY ACCID) AS DEBIT ON PAY_SUPPLIER.LedgerId=DEBIT.ACCID WHERE PAY_SUPPLIER.LedgerId=" + Convert.ToInt32(PARTYACC.SelectedValue) + " ORDER BY PAY_SUPPLIER.CODE", conn);
                   // SqlDataAdapter adapter1 = new SqlDataAdapter(cmd1);
                  //  adapter1.Fill(dt1);
                    paysupdb.LedgerId = PARTYACC.SelectedValue.ToString();
                    dt1=paysupdb.getDataPartyAcc();
                    if (dt1.Rows.Count > 0)
                    {

                        decimal amt = 0;
                        if (AMOUNT.Text != "")
                            amt = Convert.ToDecimal(AMOUNT.Text);

                        TOTAL_PAID.Text = (Convert.ToDecimal(dt1.Rows[0]["total_DEBIT"]) + amt - prev).ToString();
                        TOTAL_CURRENT.Text = dt1.Rows[0]["total_CREDIT"].ToString();
                        TOTAL_BALANCE.Text = (Convert.ToDecimal(dt1.Rows[0]["BALANCE"]) - amt + prev).ToString();
                    }
                    else
                    {
                        TOTAL_PAID.Text = "0.00";
                        TOTAL_CURRENT.Text = "0.00";
                        TOTAL_BALANCE.Text = "0.00";
                    }
                }
            }
            else
            {
                if (PARTYACC.SelectedIndex > 0)
                {
                    SUP_CODE.Text = "";
                    SUP_NAME.Text = "";

                    SUP_CODE.Text = PARTYACC.SelectedValue.ToString();
                    SUP_NAME.Text = PARTYACC.Text;
                    DataTable dt1 = new DataTable();
                   // SqlCommand cmd1 = new SqlCommand("SELECT REC_CUSTOMER.CODE,REC_CUSTOMER.DESC_ENG,REC_CUSTOMER.MOBILE,ISNULL(DEBIT.total_DEBIT,0) AS total_DEBIT,ISNULL(CREDIT.total_CREDIT,0) AS total_CREDIT,(ISNULL(DEBIT.total_DEBIT,0)-ISNULL(CREDIT.total_CREDIT,0)) AS BALANCE  FROM REC_CUSTOMER LEFT OUTER JOIN (SELECT ACCID,ISNULL(SUM(DEBIT),0) as total_DEBIT FROM tb_Transactions GROUP BY ACCID) AS DEBIT ON REC_CUSTOMER.LedgerId=DEBIT.ACCID  LEFT OUTER JOIN (SELECT ACCID,ISNULL(SUM(CREDIT),0) as total_CREDIT FROM tb_Transactions GROUP BY ACCID) AS CREDIT ON REC_CUSTOMER.LedgerId=CREDIT.ACCID WHERE REC_CUSTOMER.LedgerId=" + Convert.ToInt32(PARTYACC.SelectedValue) + " ORDER BY REC_CUSTOMER.CODE", conn);
                  //  SqlDataAdapter adapter1 = new SqlDataAdapter(cmd1);
                  //  adapter1.Fill(dt1);
                    payvchrhdrdb.LedgeId=  Convert.ToInt32(PARTYACC.SelectedValue);
                    dt1 = payvchrhdrdb.getCustomerByLedgNo();

                    if (dt1.Rows.Count > 0)
                    {

                        decimal amt = 0;
                        if (AMOUNT.Text != "")
                            amt = Convert.ToDecimal(AMOUNT.Text);

                        TOTAL_CURRENT.Text = dt1.Rows[0]["total_DEBIT"].ToString();
                        TOTAL_PAID.Text = (Convert.ToDecimal(dt1.Rows[0]["total_CREDIT"]) + amt - prev).ToString();
                        TOTAL_BALANCE.Text = (Convert.ToDecimal(dt1.Rows[0]["BALANCE"]) - amt + prev).ToString();
                    }
                    else
                    {
                        TOTAL_PAID.Text = "0.00";
                        TOTAL_CURRENT.Text = "0.00";
                        TOTAL_BALANCE.Text = "0.00";
                    }
                }
            }
         
        }
        private void AMOUNT_TextChanged(object sender, EventArgs e)
        {
            //if (prev_status)
            //{
            //    if (fld.Equals("SUP_CODE"))
            //    {
            //        if (PARTYACC.SelectedIndex > 0)
            //        {
            //            SUP_CODE.Text = "";
            //            SUP_NAME.Text = "";

            //            SUP_CODE.Text = PARTYACC.SelectedValue.ToString();
            //            SUP_NAME.Text = PARTYACC.Text;
            //            DataTable dt1 = new DataTable();
            //            SqlCommand cmd1 = new SqlCommand("SELECT PAY_SUPPLIER.CODE,PAY_SUPPLIER.DESC_ENG,PAY_SUPPLIER.MOBILE,ISNULL(CREDIT.total_CREDIT,0) AS total_CREDIT,ISNULL(DEBIT.total_DEBIT,0) AS total_DEBIT,(ISNULL(CREDIT.total_CREDIT,0)-ISNULL(DEBIT.total_DEBIT,0)) AS BALANCE  FROM PAY_SUPPLIER LEFT OUTER JOIN (SELECT ACCID,ISNULL(SUM(CREDIT),0) as total_CREDIT FROM tb_Transactions GROUP BY ACCID) AS CREDIT ON PAY_SUPPLIER.LedgerId=CREDIT.ACCID  LEFT OUTER JOIN (SELECT ACCID,ISNULL(SUM(DEBIT),0) as total_DEBIT FROM tb_Transactions GROUP BY ACCID) AS DEBIT ON PAY_SUPPLIER.LedgerId=DEBIT.ACCID WHERE PAY_SUPPLIER.LedgerId=" + Convert.ToInt32(PARTYACC.SelectedValue) + " ORDER BY PAY_SUPPLIER.CODE", conn);
            //            SqlDataAdapter adapter1 = new SqlDataAdapter(cmd1);
            //            adapter1.Fill(dt1);

            //            if (dt1.Rows.Count > 0)
            //            {

            //                decimal amt = 0;
            //                if (AMOUNT.Text != "")
            //                    amt = Convert.ToDecimal(AMOUNT.Text);

            //                TOTAL_PAID.Text = (Convert.ToDecimal(dt1.Rows[0]["total_DEBIT"]) + amt - prev).ToString();
            //                TOTAL_CURRENT.Text = dt1.Rows[0]["total_CREDIT"].ToString();
            //                TOTAL_BALANCE.Text = (Convert.ToDecimal(dt1.Rows[0]["BALANCE"]) - amt + prev).ToString();
            //            }
            //            else
            //            {
            //                TOTAL_PAID.Text = "0.00";
            //                TOTAL_CURRENT.Text = "0.00";
            //                TOTAL_BALANCE.Text = "0.00";
            //            }
            //        }
            //    }
            //    else
            //    {
            //        if (PARTYACC.SelectedIndex > 0)
            //        {
            //            SUP_CODE.Text = "";
            //            SUP_NAME.Text = "";

            //            SUP_CODE.Text = PARTYACC.SelectedValue.ToString();
            //            SUP_NAME.Text = PARTYACC.Text;
            //            DataTable dt1 = new DataTable();
            //            SqlCommand cmd1 = new SqlCommand("SELECT REC_CUSTOMER.CODE,REC_CUSTOMER.DESC_ENG,REC_CUSTOMER.MOBILE,ISNULL(DEBIT.total_DEBIT,0) AS total_DEBIT,ISNULL(CREDIT.total_CREDIT,0) AS total_CREDIT,(ISNULL(DEBIT.total_DEBIT,0)-ISNULL(CREDIT.total_CREDIT,0)) AS BALANCE  FROM REC_CUSTOMER LEFT OUTER JOIN (SELECT ACCID,ISNULL(SUM(DEBIT),0) as total_DEBIT FROM tb_Transactions GROUP BY ACCID) AS DEBIT ON REC_CUSTOMER.LedgerId=DEBIT.ACCID  LEFT OUTER JOIN (SELECT ACCID,ISNULL(SUM(CREDIT),0) as total_CREDIT FROM tb_Transactions GROUP BY ACCID) AS CREDIT ON REC_CUSTOMER.LedgerId=CREDIT.ACCID WHERE REC_CUSTOMER.LedgerId=" + Convert.ToInt32(PARTYACC.SelectedValue) + " ORDER BY REC_CUSTOMER.CODE", conn);
            //            SqlDataAdapter adapter1 = new SqlDataAdapter(cmd1);
            //            adapter1.Fill(dt1);

            //            if (dt1.Rows.Count > 0)
            //            {

            //                decimal amt = 0;
            //                if (AMOUNT.Text != "")
            //                    amt = Convert.ToDecimal(AMOUNT.Text);

            //                TOTAL_CURRENT.Text = dt1.Rows[0]["total_DEBIT"].ToString();
            //                TOTAL_PAID.Text = (Convert.ToDecimal(dt1.Rows[0]["total_CREDIT"]) + amt - prev).ToString();
            //                TOTAL_BALANCE.Text = (Convert.ToDecimal(dt1.Rows[0]["BALANCE"]) - amt + prev).ToString();
            //            }
            //            else
            //            {
            //                TOTAL_PAID.Text = "0.00";
            //                TOTAL_CURRENT.Text = "0.00";
            //                TOTAL_BALANCE.Text = "0.00";
            //            }
            //        }
            //    }
            //}
        }

        private void AMOUNT_Enter(object sender, EventArgs e)
        {
            prev_status = true;
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

        private void TextDiscount_TextChanged(object sender, EventArgs e)
        {
          
        }
    }
}