using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Drawing.Printing;

namespace Sys_Sols_Inventory
{
    public partial class Payrollvouch : Form
    {
        Class.Transactions trans = new Class.Transactions();
        Class.ModifiedTransaction modtrans = new Class.ModifiedTransaction();
        Class.Employee Emp = new Class.Employee();
        Class.CompanySetup ComSet = new Class.CompanySetup();
        Class.DateSettings dset = new Class.DateSettings();
        Class.Ledgers led = new Class.Ledgers();
        Login log = (Login)Application.OpenForms["Login"];
        Initial mdi = (Initial)Application.OpenForms["Initial"];

        private BindingSource source = new BindingSource();
        private DataTable table = new DataTable();
        private DateTime TransDate;

        string decimalFormat = "";
        private string tableHDR = "";
        private string ID = "";
        private bool closeFrom = false;
        private bool HasArabic = true;
        private bool HasAccounts = false;
        private int selectedRow = -1;
        string CompanyName = "", Address1, Addres1, Addres2, Phone, Fax, Email, TineNo, Billno, Date, CUSID, Website, panno, vat, logo, title;

        public Payrollvouch()
        {
            InitializeComponent();
        }

        public Payrollvouch(string vNo)
        {
            InitializeComponent();
            bindledgeronLoad();
            tableHDR = "PAYROLL_VOUCHER_HDR";
            DOC_NO.Text = vNo;
            PARTYACC.Visible = true;
            CASHACC.Visible = true;
            kptn.Visible = true;
            kryptonLabel28.Visible = true;
            closeFrom = true;
            GetACCID(vNo, "Salary Payment");
            GetDataFromDocNO();
            this.Load -= new EventHandler(Payrollvouch_Load);
        }
        private void Payrollvouch_Load(object sender, EventArgs e)
        {
            HasAccounts = Properties.Settings.Default.Account;
            bindledgeronLoad();
            comboSalaryType();
            GetCurrency();
            HasArabic = General.IsEnabled(Settings.Arabic);
            if (HasArabic)
            {
                DOC_DATE_HIJ.Enabled = true;
            }
            else
            {
                DOC_DATE_HIJ.Enabled = false;
            }
            decimalFormat = Common.getDecimalFormat();
            SALARYAMOUNT.Text = decimalFormat;
            AMOUNT.Text = decimalFormat;
            TOTAL_PAID.Text = decimalFormat;
            TOTAL_CURRENT.Text = decimalFormat;
            TOTAL_BALANCE.Text = decimalFormat;
            PAY_CODE.Text = "CSH";
            PAY_NAME.Text = "CASH";
            comboSalType.SelectedIndex = 0;
            GetMaxPayVouch();
            ActiveControl = SUP_NAME;
        }
        public void GetACCID(string VoucherNO, string VourcherType)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = trans.GetACCID(VoucherNO, VourcherType);
                if (dt.Rows.Count > 0)
                {
                    PARTYACC.SelectedValue = dt.Rows[0][0].ToString(); ;
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
            string query = "SELECT DOC_NO,CONVERT(NVARCHAR,DOC_DATE_GRE,103) AS DOC_DATE_GRE,DOC_DATE_HIJ,AMOUNT,TOTAL_PAID,SUP_CODE,PAY_CODE,CHQ_NO FROM " + tableHDR + " WHERE DOC_NO = '" + DOC_NO.Text + "'";
            SqlDataReader r= Model.DbFunctions.GetDataReader(query);
            dgdetail.Rows.Clear();
            while (r.Read())
            {
                    dgdetail.Rows.Add(r["DOC_NO"].ToString(), r["DOC_DATE_GRE"].ToString(), r["DOC_DATE_HIJ"].ToString(), r["SUP_CODE"].ToString(), r["PAY_CODE"].ToString(), r["CHQ_NO"].ToString(), Convert.ToDouble(r["AMOUNT"]).ToString(decimalFormat));
            }
            Model.DbFunctions.CloseConnection();
            string query1 = "select * from PAYROLL_VOUCHER_HDR WHERE DOC_NO='" + ID + "'";
            DataTable dt=Model.DbFunctions.GetDataTable(query1);
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
                string query2 = "SELECT Emp_Fname FROM EMP_EMPLOYEES WHERE Empid=" + Convert.ToString(dt.Rows[i]["EMP_CODE"]) + "";
                DataTable dt2= Model.DbFunctions.GetDataTable(query2);
                SUP_NAME.Text = Convert.ToString(dt2.Rows[0][0]);
                txtVoucherNo.Text = Convert.ToString(dt.Rows[i]["REC_NO"]);
                DOC_DATE_HIJ.Text = Convert.ToString(dt.Rows[i]["DOC_DATE_HIJ"]);
                CUR_CODE.Text = Convert.ToString(dt.Rows[i]["CUR_CODE"]);
                AMOUNT.Text = Convert.ToString(dt.Rows[i]["AMOUNT"]);
                BANK_CODE.Text = Convert.ToString(dt.Rows[i]["BANK_CODE"]);
                NOTES.Text = Convert.ToString(dt.Rows[i]["NOTES"]);
                TOTAL_PAID.Text = Convert.ToString(dt.Rows[i]["TOTAL_PAID"]);
                TOTAL_CURRENT.Text = Convert.ToString(dt.Rows[i]["TOTAL_CURRENT"]);
                TOTAL_BALANCE.Text = Convert.ToString(dt.Rows[i]["TOTAL_BALANCE"]);
                comboSalType.Text = Convert.ToString(dt.Rows[i]["SALARY_TYPE"]);
                dgClients.Visible = false;
            }
        }
        public void bindledgeronLoad()
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
            PARTYACC.Text = "CASH ACCOUNT";
            CASHACC.Text = "SALARY";
        }
        public void comboSalaryType()
        {
            comboSalType.Items.Add("JOURNEL");
            comboSalType.Items.Add("DUE");
            comboSalType.Items.Add("ADVANCE");
        }
        public void GetCurrency()
        {
            try
            {
                DataTable dt = new DataTable();
                string query = "select DEFAULT_CURRENCY_CODE from GEN_BRANCH";
                dt = Model.DbFunctions.GetDataTable(query);
                if (dt.Rows.Count > 0)
                {
                    CUR_CODE.Text = dt.Rows[0]["DEFAULT_CURRENCY_CODE"].ToString();
                }
            }
            catch
            {
            }
        }
        void GetMaxPayVouch()
        {
            int maxId;
            String value;
            value = Convert.ToString(getMaxRecNo());
            if (value.Equals("0"))
            {
                txtVoucherNo.Text = Convert.ToString(getVouchStartFrom());
            }
            else
            {
                maxId = Convert.ToInt32(value);
                txtVoucherNo.Text = (maxId + 1).ToString();
            }
        }
        private object getMaxRecNo()
        {
            string query = "SELECT ISNULL(MAX(CONVERT(DECIMAL(18,0),REC_NO)), 0) FROM PAYROLL_VOUCHER_HDR";
            return Model.DbFunctions.GetAValue(query);
        }
        public object getVouchStartFrom()
        {
            string query = "SELECT VouchStartFrom FROM GEN_VOUCH_STARTFROM WHERE VouchTypeCode='PAY'";
            return Model.DbFunctions.GetAValue(query);
        }
       
        private void SUP_NAME_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Down)
            {
                if (SUP_NAME.Text == "")
                {
                    BindEmployees();
                    // bindDayPayments();
                    bindledgers();
                    dgClients.Visible = true;
                    if (dgClients.Visible == true)
                    {
                        dgClients.Focus();
                        dgClients.CurrentCell = dgClients.Rows[0].Cells[1];
                    }
                }
                else 
                {
                    if (dgClients.CurrentRow != null)
                    {
                        int r = dgClients.CurrentCell.RowIndex;
                        dgClients.Focus();
                        if (r < dgClients.Rows.Count - 1)
                        {
                            dgClients.CurrentCell = dgClients[2, r + 1];

                        }
                    }
                }
            }
            else if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (SUP_NAME.Text == "")
                {
                    if (HasAccounts)
                    {
                        PARTYACC.Focus();
                    }
                }
            }
            else if (e.KeyCode == Keys.Back)
            {
                if (SUP_NAME.Text == "")
                {
                    SUP_CODE.Text = "";
                }
            }
        }
        public void BindEmployees()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = Emp.GetEmployees();
                dgClients.DataSource = dt;
            }
            catch
            {
            }
        }
        public void bindledgers()
        {
            //DataTable dt1 = new DataTable();
            //dt1 = led.Selectledger();
            //DataRow row = dt1.NewRow();
            //dt1.Rows.InsertAt(row, 0);
            //PARTYACC.DataSource = dt1;
            //PARTYACC.DisplayMember = "LEDGERNAME";
            //PARTYACC.ValueMember = "LEDGERID";

            DataTable dt2 = new DataTable();
            dt2 = led.Selectledger();
            CASHACC.DataSource = dt2;
            CASHACC.DisplayMember = "LEDGERNAME";
            CASHACC.ValueMember = "LEDGERID";

        }
        
        private void dgClients_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                btnClear.PerformClick();
                SUP_CODE.Text = dgClients.CurrentRow.Cells[0].Value.ToString();
                try
                {
                    PARTYACC.SelectedValue = dgClients.CurrentRow.Cells["LedgerId"].Value.ToString();
                }
                catch { }
                SUP_NAME.Text = dgClients.CurrentRow.Cells[1].Value.ToString();
                dgClients.Visible = false;
                try
                {
                    string cmd = "EMP_PAIDSALARY";
                    Dictionary<string, object> parameter = new Dictionary<string, object>();
                    parameter.Add("@EMP_CODE", SUP_CODE.Text);
                    SqlDataReader r = Model.DbFunctions.GetDataReaderProcedure(cmd, parameter);

                    dgdetail.Rows.Clear();
                    double totalPaid = 0;
                    double totalBal = 0;
                    while (r.Read())
                    {
                        dgdetail.Rows.Add(r["DOC_NO"], r["DOC_DATE_GRE"], r["DOC_DATE_HIJ"], r["EMP_CODE"], r["PAY_CODE"], r["CHQ_NO"], r["AMOUNT"], r["TOTAL_BALANCE"],r["SALARY_TYPE"],r["NOTES"]);
                        totalPaid = totalPaid + Convert.ToDouble(r["AMOUNT"]);
                    }

                    TOTAL_PAID.Text = totalPaid.ToString(decimalFormat);
                    Model.DbFunctions.CloseConnection();
                    dgClients.Visible = false;

                   string query = "select debit,credit from tb_transactions where vouchertype='Salary Payment' and accid= '" + PARTYACC.SelectedValue + "'";
                   DataTable dt = new DataTable();
                   dt= Model.DbFunctions.GetDataTable(query);
                   double val1=0,val2=0;
                   for(int i=0;i<dt.Rows.Count;i++)
                   {
                       val1 = val1 + Convert.ToDouble(dt.Rows[i]["DEBIT"]);
                       val2=val2+ Convert.ToDouble(dt.Rows[i]["CREDIT"]);
                   }
                  double valResult = 0;
                  valResult = val2 - val1;
                  TOTAL_BALANCE.Text = valResult.ToString();
                  string query1 = "select credit from tb_transactions where vouchertype='Salary Payment'and accname= '" + PARTYACC.Text + "' and PARTICULARS='salary' and accid='" + PARTYACC.SelectedValue + "'";
                  DataTable dt1 = new DataTable();
                  dt1 = Model.DbFunctions.GetDataTable(query1);
                  double value = 0;
                  for (int i = 0; i < dt1.Rows.Count; i++)
                  {
                      value = value + Convert.ToDouble(dt1.Rows[i]["CREDIT"]);
                  }
                  TOTAL_CURRENT.Text = value.ToString(decimalFormat);
                  comboSalType.Focus();
                }
                catch (Exception ex)
                {
                    string x = ex.Message;
                }
            }
        }

        private void dgClients_Leave(object sender, EventArgs e)
        {
            dgClients.Visible = false;
            PARTYACC.Visible = true;
            CASHACC.Visible = true;
            kryptonLabel28.Visible = true;
            kptn.Visible = true;
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
            else if (PAY_CODE.Text =="DEP")
            {
                if (string.IsNullOrEmpty(txtAccDetails.Text))
                {
                    MessageBox.Show("Enter account no.");
                    return false;
                }
            }
            if (Convert.ToDouble(AMOUNT.Text) <= 0 || AMOUNT.Text == "")
            {
                MessageBox.Show("Please Enter Amount ");
                AMOUNT.Focus();
                return false;
            }
            if (comboSalType.Text == "JOURNEL" && SALARYAMOUNT.Text == "")
            {
                MessageBox.Show("Enter Salary");
                return false;
            }
            if(Convert.ToDouble(AMOUNT.Text)>Convert.ToDouble(SALARYAMOUNT.Text) && comboSalType.Text=="JOURNEL")
            {
                    MessageBox.Show("payment amount should less than salary amount");
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
                if (DialogResult.Yes == MessageBox.Show("Are you sure to continue payment", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                {
                    if (valid())
                    {
                        TransDate = Convert.ToDateTime(DOC_DATE_GRE.Value);
                        string status = "Added!";
                        string query = "";
                        
                        if (ID == "")
                        {
                            DOC_NO.Text = generatePayVoucherCode();
                            query = "INSERT INTO PAYROLL_VOUCHER_HDR (DOC_NO,REC_NO,DOC_DATE_GRE,DOC_DATE_HIJ,EMP_CODE,CUR_CODE,AMOUNT,PAY_CODE,BANK_CODE,ACC_DETAILS,CHQ_NO,CHQ_DATE,DEBIT_CODE,DESC1,CREDIT_CODE,DESC2,NOTES,TOTAL_PAID,TOTAL_CURRENT,TOTAL_BALANCE,SALARY_TYPE) VALUES('" + DOC_NO.Text + "','" + txtVoucherNo.Text + "','" + DOC_DATE_GRE.Value.ToString("MM/dd/yyyy") + "','" + DOC_DATE_HIJ.Text + "','" + SUP_CODE.Text + "','" + CUR_CODE.Text + "','" + AMOUNT.Text + "','" + PAY_CODE.Text + "','" + BANK_CODE.Text + "','" + txtAccDetails.Text + "','" + CHQ_NO.Text + "','" + CHQ_DATE.Value.ToString("MM/dd/yyyy") + "','" + PARTYACC.SelectedValue + "','" + PARTYACC.Text + "','" + CASHACC.SelectedValue + "','" + CASHACC.Text + "', '" + NOTES.Text + "','" + TOTAL_PAID.Text + "','" + TOTAL_CURRENT.Text + "','" + TOTAL_BALANCE.Text + "','" + comboSalType.Text + "')";
                        }
                        else
                        {
                            modifiedtransaction();
                            DeleteTransation();
                            status = "Updated!";
                            query = "UPDATE PAYROLL_VOUCHER_HDR SET DOC_DATE_GRE = '" + DOC_DATE_GRE.Value.ToString("MM/dd/yyyy") + "',DOC_DATE_HIJ = '" + DOC_DATE_HIJ.Text + "',EMP_CODE = '" + SUP_CODE.Text + "',CUR_CODE = '" + CUR_CODE.Text + "',AMOUNT = '" + AMOUNT.Text + "',PAY_CODE = '" + PAY_CODE.Text + "',BANK_CODE = '" + BANK_CODE.Text + "',CHQ_NO = '" + CHQ_NO.Text + "',CHQ_DATE = '" + CHQ_DATE.Value.ToString("MM/dd/yyyy") + "',NOTES = '" + NOTES.Text + "',TOTAL_PAID = '" + TOTAL_PAID.Text + "',TOTAL_CURRENT = '" + TOTAL_CURRENT.Text + "',TOTAL_BALANCE = '" + TOTAL_BALANCE.Text + "',SALARY_TYPE='" + comboSalType.Text + "' WHERE DOC_NO = '" + DOC_NO.Text + "'";//DELETE FROM PAYROLL_VOUCHER_HDR WHERE DOC_NO = '" + DOC_NO.Text + "'";
                        }
                        //   cmd.CommandText += " INSERT INTO " + tableDTL + "(DOC_NO," + fld + ",INV_DATE_GRE,INV_DATE_HIJ,INV_NO,AMOUNT,PAID,BALANCE,CURRENT_PAY_AMOUNT) ";
                        // foreach (DataGridViewRow row in dgDetail.Rows)
                        //{
                        //  cmd.CommandText += " SELECT '" + DOC_NO.Text + "','" + SUP_CODE.Text + "','" + Convert.ToDateTime(row.Cells[1].Value.ToString()).ToString("MM/dd/yyyy") + "','" + row.Cells[2].Value.ToString() + "','" + row.Cells[0].Value + "','" + row.Cells[3].Value + "','" + row.Cells[4].Value.ToString() + "','" + row.Cells[6].Value.ToString() + "','" + row.Cells[5].Value.ToString() + "' UNION ALL ";
                        //}
                        //   cmd.CommandText = cmd.CommandText.Substring(0, cmd.CommandText.Length - 10);
                        //cmd.Connection = conn;//new code exception
                        //cmd.CommandType = CommandType.Text;
                        //cmd.ExecuteNonQuery();
                        paymentVoucherTransaction();
                        Model.DbFunctions.InsertUpdate(query);
                        MessageBox.Show("Payroll  Voucher " + status);
                        if (ChekPrint.Checked == true)
                        {
                            printingrecipt();
                        }
                        btnClear.PerformClick();
                    }
                }
            }
            catch (Exception ex)
            {
                string z = ex.Message;
                MessageBox.Show("Error in Saving Please Contact Developer");
            }
        }
        public static string generatePayVoucherCode()
        {
            string value = DateTime.Today.ToString("ddMMyy");
            string testvalue = (Convert.ToInt64(value)).ToString();
            string Query = "SELECT MAX(DOC_NO) FROM PAYROLL_VOUCHER_HDR WHERE DOC_NO LIKE '" + testvalue + "%'";
            string id = Convert.ToString(Model.DbFunctions.GetAValue(Query));
            if (id == "")
            {
                id = testvalue + "0001";
            }
            else
            {
                id = (Convert.ToInt64(id) + 1).ToString();
            }
            return id;
        }
        public void modifiedtransaction()
        {
            modtrans.VOUCHERTYPE = "Salary Payment";
            modtrans.Date = DOC_DATE_GRE.Value.ToString("MM/dd/yyyy");
            modtrans.USERID = log.EmpId;
            modtrans.VOUCHERNO = DOC_NO.Text;
            modtrans.BRANCH = log.Branch;
            modtrans.NARRATION = NOTES.Text;
            modtrans.STATUS = "Update";
            modtrans.MODIFIEDDATE = DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss"); ;
            modtrans.insertTransaction();
        }
        private void DeleteTransation()
        {
            try
            {
                trans.VOUCHERTYPE = "Cash Payment";

                trans.VOUCHERNO = DOC_NO.Text;
                trans.DeletePurchaseTransaction();
            }
            catch
            {
            }

        }
        public void paymentVoucherTransaction()
        {
            
            trans.VOUCHERTYPE = "Salary Payment";
            trans.DATED = TransDate.ToString("MM/dd/yyyy");
            trans.NARRATION = NOTES.Text;
            trans.USERID = log.EmpId;

            if (comboSalType.Text == "JOURNEL")
            {
                trans.ACCNAME = PARTYACC.Text;
                trans.PARTICULARS = "SALARY";
                trans.VOUCHERNO = DOC_NO.Text;
                trans.BRANCH = log.Branch;
                trans.ACCID = PARTYACC.SelectedValue.ToString();
                trans.DEBIT = "0";
                trans.CREDIT = SALARYAMOUNT.Text;
                trans.SYSTEMTIME = DateTime.Now.ToString();
                trans.insertTransaction();

                trans.ACCNAME = "SALARY";
                trans.PARTICULARS = PARTYACC.Text;
                trans.VOUCHERNO = DOC_NO.Text;
                trans.BRANCH = log.Branch;
                trans.ACCID = "93";
                trans.DEBIT = SALARYAMOUNT.Text;
                trans.CREDIT = "0";
                trans.SYSTEMTIME = DateTime.Now.ToString();
                trans.insertTransaction();
            }
           
                if (PAY_CODE.Text == "CSH" || PAY_CODE.Text == "CHQ" || PAY_CODE.Text == "CRD" || PAY_CODE.Text == "DEP")
                {
                    trans.ACCNAME = CASHACC.Text;
                    trans.PARTICULARS = PARTYACC.Text;
                    trans.VOUCHERNO = DOC_NO.Text;
                    trans.BRANCH = log.Branch;
                    trans.ACCID = CASHACC.SelectedValue.ToString();
                    trans.DEBIT = "0";
                    trans.CREDIT = AMOUNT.Text;
                    trans.SYSTEMTIME = DateTime.Now.ToString();
                    trans.insertTransaction();

                    trans.ACCNAME = PARTYACC.Text;
                    trans.PARTICULARS = CASHACC.Text;
                    trans.VOUCHERNO = DOC_NO.Text;
                    trans.BRANCH = log.Branch;
                    trans.ACCID = PARTYACC.SelectedValue.ToString();
                    trans.CREDIT = "0";
                    trans.DEBIT = AMOUNT.Text;
                    trans.SYSTEMTIME = DateTime.Now.ToString();
                    trans.insertTransaction();
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
                    int height = (dgdetail.Rows.Count - 1) * 23;

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
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
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
        private void printDocument_PrintPage(object sender, PrintPageEventArgs e)
        {

            float xpos;
            int startx = 30;
            int starty = 30;
            int offset = 15;

            int w = e.MarginBounds.Width / 2;
            int x = e.MarginBounds.Left;
            int y = e.MarginBounds.Top;
            Font Headerfont1 = new Font("Times New Roman", 13, FontStyle.Bold);
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

                e.Graphics.DrawString("Doc no: " + DOC_NO.Text, printFont, new SolidBrush(tabDataForeColor), startx, starty + offset);
                offset = offset + 12;
                e.Graphics.DrawString("Date:" + DateTime.Now.ToString(), printFont, new SolidBrush(tabDataForeColor), startx, starty + offset);
                offset = offset + 12;
                e.Graphics.DrawString("Pay to:" + SUP_NAME.Text, printFont, new SolidBrush(tabDataForeColor), startx, starty + offset);
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

                e.Graphics.DrawString("Total Amount: ", printFont, new SolidBrush(Color.Black), startx + 423, starty + offset);

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

        private void btnClear_Click(object sender, EventArgs e)
        {
            dgdetail.Rows.Clear();
            ID = "";
            DOC_NO.Text = "";
            DOC_DATE_HIJ.Text = "";
            CHQ_NO.Text = "";
            BANK_CODE.Text = "";
            NOTES.Text = ""; 
            SUP_NAME.Text = "";
            PAY_CODE.Text = "CSH";
            PAY_NAME.Text = "CASH";
            CASHACC.Text = "";
            comboSalType.Text = "";
            PARTYACC.SelectedIndex = 0;
            comboSalType.SelectedIndex = 0;
            CHQ_DATE.Value = DateTime.Today;
            DOC_DATE_GRE.Value = DateTime.Today;
            GetCurrency();
            AMOUNT.Text = decimalFormat;
            TOTAL_PAID.Text = decimalFormat;
            TOTAL_CURRENT.Text = decimalFormat;
            TOTAL_BALANCE.Text = decimalFormat;
            SALARYAMOUNT.Text = decimalFormat;
            SUP_NAME.Focus();
            SALARYAMOUNT.Enabled = true;
            dgClients.Visible = false;
            GetMaxPayVouch();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (ID != "")
            {
                if (PAY_CODE.Text != "CHQ")
                {
                    if (MessageBox.Show("Are you sure? you want to delete this?", "Record Deletion", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {

                        string id = DOC_NO.Text;
                        String date = DOC_DATE_GRE.Text;
                        {

                            string query = "DELETE FROM PAYROLL_VOUCHER_HDR WHERE DOC_NO = '" + id + "'";//DELETE FROM PAYROLL_VOUCHER_DTL WHERE DOC_NO = '" + id + "'";
                            Model.DbFunctions.InsertUpdate(query);
                        }
                        AddtoDeletedTransaction(id);
                        modifiedtransaction(id, date);
                        DeleteTransation(id);
                        btnClear.PerformClick();
                    }
                }
                else
                {
                    MessageBox.Show("Please Use Cheque report to Delete the cheque..!", "Sysbizz", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        public void AddtoDeletedTransaction(string id)
        {
            string vchr;
            vchr = "Salary Payment";
            string query = "insert into  tbl_deletedTransaction(VOUCHERNO, VOUCHERTYPE, ACCNAME, PARTICULARS, DEBIT, CREDIT, DATED, NARRATION, USERID, SYSTEMTIEM, ACCID) select    VOUCHERNO, VOUCHERTYPE, ACCNAME, PARTICULARS, DEBIT, CREDIT, DATED, NARRATION, USERID, SYSTEMTIEM, ACCID from tb_Transactions where VOUCHERNO='" + id + "' and VOUCHERTYPE='" + vchr + "'";
            Model.DbFunctions.InsertUpdate(query);
        }
        public void modifiedtransaction(string ID, string date)
        {
            
            modtrans.VOUCHERTYPE = "Salary Payment";
           
            modtrans.Date = DOC_DATE_GRE.Value.ToString("MM/dd/yyyy");
            modtrans.USERID = log.EmpId;
            modtrans.BRANCH = log.Branch;
            modtrans.VOUCHERNO = ID;
            modtrans.NARRATION = "";
            modtrans.STATUS = "Delete";
            modtrans.MODIFIEDDATE = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss tt");
            modtrans.insertTransaction();
        }
        private void DeleteTransation(string Id)
        {
            
            trans.VOUCHERTYPE = "Salary Payment";
            trans.VOUCHERNO = Id;
            trans.DeletePurchaseTransaction();

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
                kryptonBankLabel.Visible = false;
                BANK_CODE.Visible = false;
                btnBank.Visible = false;
                bindledgers();
            }
            else if (PAY_CODE.Text == "CHQ")
            {
                lblChqNo.Visible = true; ;
                CHQ_NO.Visible = true;
                lblChqDate.Visible = true;
                CHQ_DATE.Visible = true;
                lblAccDetails.Visible = false;
                txtAccDetails.Visible = false;
                kryptonBankLabel.Visible = true;
                BANK_CODE.Visible = true;
                BankAccount();
            }
            else if (PAY_CODE.Text == "DEP")
            {
                lblChqNo.Visible = false;
                CHQ_NO.Visible = false;
                lblChqDate.Visible = false;
                CHQ_DATE.Visible = false;
                lblAccDetails.Visible = true;
                lblAccDetails.Text = "Account No.";
                txtAccDetails.Visible = true;
                kryptonBankLabel.Visible = true;
                BANK_CODE.Visible = true;
                BankAccount();
            }
            else
            {
                lblChqNo.Visible = false;
                CHQ_NO.Visible = false;
                lblChqDate.Visible = false;
                CHQ_DATE.Visible = false;
                lblAccDetails.Visible = false;
                kryptonBankLabel.Visible = false;
                BANK_CODE.Visible = false;
            }
        }
        private void BankAccount()
        {
            DataTable dt = new DataTable();
            string query = "";
            query = "SELECT DISTINCT LEDGERID,LEDGERNAME FROM tb_Ledgers WHERE UNDER IN (10,21,22)";
            dt = Model.DbFunctions.GetDataTable(query);
            CASHACC.DataSource = dt;
            CASHACC.DisplayMember = "LEDGERNAME";
            CASHACC.ValueMember = "LEDGERID";
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            CommonHelp c = new CommonHelp(0, genEnum.PayType);
            if (c.ShowDialog() == DialogResult.OK && c.c != null)
            {
                if (Convert.ToString(c.c[0].Value) != "CRD")
                {
                    PAY_CODE.Text = Convert.ToString(c.c[0].Value);
                    PAY_NAME.Text = Convert.ToString(c.c[1].Value);
                }
                else
                {
                    PAY_CODE.Text = "CSH";
                    PAY_NAME.Text = "CASH";
                }
                if (CHQ_NO.Visible)
                    CHQ_NO.Focus();
                else if (txtAccDetails.Visible)
                    txtAccDetails.Focus();
                else
                    btnSave.Focus();
            }
                if (CASHACC.Text != "CASH ACCOUNT")
                {
                    SelectBank();
                }
           
        }
        public void SelectBank()
        {
            if (PAY_CODE.Text == "CHQ" | PAY_CODE.Text == "DEP" )
            {
                BANK_CODE.Text = CASHACC.Text;
            }
        }

        private void btnCurr_Click(object sender, EventArgs e)
        {
            try
            {
                CurrencyHelp h = new CurrencyHelp();
                if (h.ShowDialog() == DialogResult.OK && h.c != null)
                {
                    CUR_CODE.Text = Convert.ToString(h.c["CODE"].Value);
                    btnSave.Focus();
                }
            }
            catch
            {
            }
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
                    if (log.Theme == "1")
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

        private void btnDoc_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    btnClear.PerformClick();
            //    Pay_Roll_Voucher_Help h = new Pay_Roll_Voucher_Help();
            //    if (h.ShowDialog() == DialogResult.OK && h.c != null)
            //    {
            //        DOC_NO.Text = h.c["DOC_NO"].Value.ToString();
            //        ID = DOC_NO.Text;
            //        btnDelete.Enabled = true;
            //        txtVoucherNo.Text = h.c["REC_NO"].Value.ToString();
            //        DOC_DATE_GRE.Text = h.c["DOC_DATE_GRE"].Value.ToString();
            //        PARTYACC.SelectedValue = Convert.ToString(h.c["DEBIT_CODE"].Value);
            //        CASHACC.SelectedValue  = Convert.ToString(h.c["CREDIT_CODE"].Value);
            //        SUP_NAME.Text = Convert.ToString(h.c["DESC1"].Value);
            //        PAY_CODE.Text = Convert.ToString(h.c["PAY_CODE"].Value);
            //        PAY_NAME.Text = General.getName(PAY_CODE.Text, "GEN_PAYTYPE");
            //        dgClients.Visible = false;
            //        string query = "SELECT DOC_NO,DOC_DATE_GRE,DOC_DATE_HIJ,EMP_CODE,PAY_CODE,CHQ_NO,AMOUNT,TOTAL_BALANCE,SALARY_TYPE,NOTES  from PAYROLL_VOUCHER_HDR WHERE   DOC_No ='" + DOC_NO.Text + "'";
            //        SqlDataReader r = Model.DbFunctions.GetDataReader(query);
            //        dgdetail.Rows.Clear();
            //        while (r.Read())
            //        {
            //            dgdetail.Rows.Add(r["DOC_NO"], r["DOC_DATE_GRE"], r["DOC_DATE_HIJ"], r["EMP_CODE"],r["PAY_CODE"], r["CHQ_NO"], Convert.ToDouble(r["AMOUNT"]).ToString(decimalFormat), Convert.ToDouble(r["TOTAL_BALANCE"]).ToString(decimalFormat),r["SALARY_TYPE"],r["NOTES"]);
            //            AMOUNT.Text = r["AMOUNT"].ToString();
            //            comboSalType.Text = r["SALARY_TYPE"].ToString();
            //            NOTES.Text = r["NOTES"].ToString();
            //        }
            //            getSalary();
            //            Model.DbFunctions.CloseConnection();
            //            kptn.Visible = true;
            //            lb_employee.Visible = true;
            //            PARTYACC.Visible = true;
            //            CASHACC.Visible = true;
            //            TOTAL_BALANCE.Text = decimalFormat;
                
            //    }
            //}
            //catch (Exception ee)
            //{
            //    MessageBox.Show(ee.ToString());
            //}
        }
        private void getSalary()
        {
            if (comboSalType.Text == "JOURNEL")
            {
                DataTable dt1 = new DataTable();
                string QUERYS = "SELECT DEBIT FROM tb_Transactions where vouchertype='salary payment' and dated='" + DOC_DATE_GRE.Value.ToShortDateString() + "' and accname='SALARY'and particulars='" + SUP_NAME.Text + "' and  voucherno='" + DOC_NO.Text + "'";
                dt1 = Model.DbFunctions.GetDataTable(QUERYS);
                if(dt1.Rows.Count>0)
                SALARYAMOUNT.Text = dt1.Rows[0]["DEBIT"].ToString();
            }
            else
                SALARYAMOUNT.Text = decimalFormat;
        }

        private void AMOUNT_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (AMOUNT.Text == "")
                    AMOUNT.Text = "0";
                if (comboSalType.Text == "JOURNEL")
                {
                    if (AMOUNT.Text != "" || SALARYAMOUNT.Text != "")
                    {
                        TOTAL_BALANCE.Text = (Convert.ToDouble(SALARYAMOUNT.Text) - Convert.ToDouble(AMOUNT.Text) + (Convert.ToDouble(TOTAL_CURRENT.Text) - Convert.ToDouble(TOTAL_PAID.Text))).ToString(decimalFormat);
                    }
                    else
                        TOTAL_BALANCE.Text = decimalFormat;
                }
                else if (comboSalType.Text == "DUE" )
                {
                        TOTAL_BALANCE.Text = (Convert.ToDouble(TOTAL_CURRENT.Text) - (Convert.ToDouble(TOTAL_PAID.Text) + Convert.ToDouble(AMOUNT.Text))).ToString(decimalFormat);
                }
                else { }
                
            }
            catch { }
          }

        private void comboSalType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboSalType.Text == "DUE" || comboSalType.Text == "ADVANCE")
                SALARYAMOUNT.Enabled = false;
            else
                SALARYAMOUNT.Enabled = true;
        }

        private void AMOUNT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                btnPay.Focus();
            }
        }

        private void comboSalType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (SALARYAMOUNT.Enabled)
                    SALARYAMOUNT.Focus();
                else
                    AMOUNT.Focus();
            }
        }

        private void SALARYAMOUNT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
                AMOUNT .Focus();
        }

        private void CHQ_NO_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
                    CHQ_DATE.Focus();
        }

        private void CHQ_DATE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
                btnSave.Focus();
        }

        private void txtAccDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
                btnSave.Focus();
        }

        private void SUP_NAME_TextChanged(object sender, EventArgs e)
        {
            if (SUP_NAME.Text != "")
            {
                DataTable dt = new DataTable();
                dt = Emp.GetEmployees();
                source.DataSource = dt;
                dgClients.DataSource = source;
                try
                {
                    dgClients.Visible = true;
                    source.Filter = string.Format("[NAME] LIKE '%{0}%' ", SUP_NAME.Text.Trim().Replace("'", "''").Replace("*", "[*]"));
                }
                catch
                {

                }
            }
            else
            {

                dgClients.DataSource = Emp.GetEmployees();
            }
        }

        private void btnup_Click(object sender, EventArgs e)
        {
            decimal value = Convert.ToDecimal(getMaxRecNo());
            if (Convert.ToDecimal(txtVoucherNo.Text) + 1 >= ++value)
            {
                ID = "";
                btnClear.PerformClick();
                return;
            }
            else
            {
                dgClients.Visible = false;
                txtVoucherNo.Text = (Convert.ToDecimal(txtVoucherNo.Text) + 1).ToString();
                DataTable dt = new DataTable();
                dt = getAllPaymentVoucher();
                if (dt.Rows.Count > 0)
                {
                    DOC_NO.Text = dt.Rows[0]["DOC_NO"].ToString();
                    ID = DOC_NO.Text;
                    PAY_CODE.Text = dt.Rows[0]["PAY_CODE"].ToString();
                    SUP_CODE.Text = dt.Rows[0]["DEBIT_CODE"].ToString();
                    comboSalType.Text = dt.Rows[0]["SALARY_TYPE"].ToString();
                    if (PAY_CODE.Text == "CHQ")
                    {
                        CHQ_DATE.Value = Convert.ToDateTime(dt.Rows[0]["CHQ_DATE"]);
                        CHQ_NO.Text = dt.Rows[0]["CHQ_NO"].ToString();
                    }
                    AMOUNT.Text = Convert.ToDecimal(dt.Rows[0]["AMOUNT"]).ToString("n2");
                    //prev = Convert.ToDecimal(AMOUNT.Text);
                   
                    CASHACC.SelectedValue = dt.Rows[0]["CREDIT_CODE"].ToString();
                    PARTYACC.SelectedValue = dt.Rows[0]["DEBIT_CODE"].ToString();
                    SUP_NAME.Text = PARTYACC.Text;
                    NOTES.Text = dt.Rows[0]["NOTES"].ToString();
                    DOC_DATE_GRE.Value = Convert.ToDateTime(getDocDateGre());
                    dgClients.Visible = false;
                    getSalary();
                    TOTAL_BALANCE.Text = decimalFormat;
                }
                else
                {
                    MessageBox.Show("Deleted Records");
                    DOC_NO.Text = "";
                    SUP_NAME.Text = "";
                    PARTYACC.Text = "";
                    CASHACC.Text = "";
                    SALARYAMOUNT.Text = decimalFormat;
                    comboSalType.Text = "";
                    AMOUNT.Text = "";
                    dgClients.Visible = false;

                }

                bindDayPayments();
            }

        }
        
        public DataTable getAllPaymentVoucher()
        {
            string query = "SELECT * FROM PAYROLL_VOUCHER_HDR LEFT OUTER JOIN GEN_PAYTYPE ON PAYROLL_VOUCHER_HDR.PAY_CODE=GEN_PAYTYPE.CODE WHERE REC_NO='" + Convert.ToDecimal(txtVoucherNo.Text) + "'";
            return Model.DbFunctions.GetDataTable(query);
        }
        public object getDocDateGre()
        {
            string query = "SELECT DOC_DATE_GRE FROM PAYROLL_VOUCHER_HDR WHERE REC_NO='" + Convert.ToDecimal(txtVoucherNo.Text) + "'";
            return Model.DbFunctions.GetAValue(query);
        }

        private void btndown_Click(object sender, EventArgs e)
        {
            dgClients.Visible = false;
            string MIN_PAY = Convert.ToString(getVouchStartFrom());
            decimal CURRENT = Convert.ToDecimal(txtVoucherNo.Text) - 1;
           
                if (CURRENT < Convert.ToDecimal(MIN_PAY))
                {
                  
                    return;
                }
                else
                {
                    txtVoucherNo.Text = (Convert.ToDecimal(txtVoucherNo.Text) - 1).ToString();
                    DataTable dt = new DataTable();
                    dt = getAllPaymentVoucher();
                    if (dt.Rows.Count > 0)
                    {
                        btnDelete.Enabled = true;
                        DOC_NO.Text = dt.Rows[0]["DOC_NO"].ToString();
                        ID = DOC_NO.Text;
                        // fld = "SUP_CODE";
                        PAY_CODE.Text = dt.Rows[0]["PAY_CODE"].ToString();
                        PAY_NAME.Text = dt.Rows[0]["DESC_ENG"].ToString();
                        comboSalType.Text = dt.Rows[0]["SALARY_TYPE"].ToString();
                        //previous_paycode = PAY_CODE.Text;
                        SUP_CODE.Text = dt.Rows[0]["DEBIT_CODE"].ToString();
                        if (PAY_CODE.Text == "CHQ")
                        {
                            CHQ_DATE.Value = Convert.ToDateTime(dt.Rows[0]["CHQ_DATE"]);
                            CHQ_NO.Text = dt.Rows[0]["CHQ_NO"].ToString();
                        }
                        AMOUNT.Text = Convert.ToDecimal(dt.Rows[0]["AMOUNT"]).ToString("n2");
                        //prev = Convert.ToDecimal(AMOUNT.Text);
                        //prev_status = false;
                        CASHACC.Visible = true;
                        PARTYACC.Visible = true;
                        kptn.Visible = true;
                        kryptonLabel28.Visible = true;
                        CASHACC.SelectedValue = dt.Rows[0]["CREDIT_CODE"].ToString();
                        PARTYACC.SelectedValue = dt.Rows[0]["DEBIT_CODE"].ToString();
                        SUP_NAME.Text = PARTYACC.Text;
                        NOTES.Text = dt.Rows[0]["NOTES"].ToString();
                        TOTAL_BALANCE.Text = decimalFormat;
                        //if (Convert.ToInt32(dt.Rows[0]["PROJECTID"].ToString()) <= 0 || dt.Rows[0]["PROJECTID"] == null)
                        //{
                        //    cmb_projects.SelectedIndex = 0;
                        //}
                        //else
                        //    cmb_projects.SelectedIndex = Convert.ToInt32(dt.Rows[0]["PROJECTID"]);
                        DOC_DATE_GRE.Value = Convert.ToDateTime(getDocDateGre());
                        dgClients.Visible = false;
                        getSalary();
                    }
                    else
                    {
                        MessageBox.Show("Deleted Records");
                        DOC_NO.Text = "";
                        SUP_NAME.Text = "";
                        PARTYACC.Text = "";
                        CASHACC.Text = "";
                        SALARYAMOUNT.Text = decimalFormat;
                        comboSalType.Text = "";
                        AMOUNT.Text = "";
                        dgClients.Visible = false;
                    }

                    bindDayPayments();
                }
            }
        public void BindEmp()
        {
            table.Rows.Clear();
            try
            {
                HasArabic = General.IsEnabled(Settings.Arabic);
                string query = "";
                if (HasArabic)
                    query = "SELECT CODE,DESC_ENG,DESC_ARB,TYPE,ADDRESS_A,ADDRESS_B,POBOX,TELE1,TELE2,MOBILE,EMAIL,CITY_CODE,COUNTRY_CODE,OPENING_BAL,FAX,CONVERT(NVARCHAR,DATE_GRE,103) AS DATE_GRE,INITIAL,DEFAULT_CURRENCY,CONTACT_PERSON,LedgerId FROM PAY_SUPPLIER";
                else
                    query = "SELECT CODE,DESC_ENG,ADDRESS_A,TYPE,ADDRESS_B,POBOX,TELE1,TELE2,MOBILE,EMAIL,CITY_CODE,COUNTRY_CODE,OPENING_BAL,FAX,CONVERT(NVARCHAR,DATE_GRE,103) AS DATE_GRE,INITIAL,DEFAULT_CURRENCY,CONTACT_PERSON,LedgerId FROM PAY_SUPPLIER";
                table = Model.DbFunctions.GetDataTable(query);
                //source.DataSource = table;
                dgClients.DataSource = table;
            }
            catch
            {
            }
        }
        public void bindDayPayments()
        {
            string query = "SELECT ROW_NUMBER() Over (Order by DOC_NO) as 'Sl_No',REC_NO AS 'VOUCHER_NO',DOC_NO, CONVERT(NVARCHAR(50), DOC_DATE_GRE, 103) AS DOC_DATE_GRE, DOC_DATE_HIJ, SUP_CODE,DESC1 DESC_ENG,DESC2 DESC_ENG2, AMOUNT, CHQ_NO, PAY_CODE,TOTAL_BALANCE,SALARY_TYPE,PAYROLL_VOUCHER_HDR.NOTES FROM PAYROLL_VOUCHER_HDR LEFT JOIN PAY_SUPPLIER ON CODE=SUP_CODE WHERE DOC_DATE_GRE =@DATE";
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@DATE", DOC_DATE_GRE.Value.ToShortDateString());
            DataTable r2 =Model.DbFunctions.GetDataTable(query, Parameters);
            dgdetail.Rows.Clear();
            for (int i = 0; i < r2.Rows.Count; i++)
            {
                dgdetail.Rows.Add(r2.Rows[i]["DOC_NO"], r2.Rows[i]["DOC_DATE_GRE"], r2.Rows[i]["DOC_DATE_HIJ"], r2.Rows[i]["SUP_CODE"], r2.Rows[i]["PAY_CODE"], r2.Rows[i]["CHQ_NO"], Convert.ToDouble(r2.Rows[i]["AMOUNT"]).ToString(decimalFormat), r2.Rows[i]["TOTAL_BALANCE"], r2.Rows[i]["SALARY_TYPE"],r2.Rows[i]["NOTES"]);
            }
        }

        private void DOC_DATE_GRE_ValueChanged(object sender, EventArgs e)
        {
            bindDayPayments();
        }

        private void CASHACC_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if(PAY_CODE.Text=="CHQ" || PAY_CODE.Text=="DEP")
            BANK_CODE.Text = CASHACC.Text;
        }

        private void dgdetail_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgdetail.Rows.Count > 0)
            {
                DataGridViewCellCollection c = dgdetail.CurrentRow.Cells;
                selectedRow = dgdetail.CurrentRow.Index;
                DOC_NO.Text = Convert.ToString(c["DocumentNo"].Value);
                DOC_DATE_GRE.Text = Convert.ToString(c["DateGre"].Value);
                if (DOC_DATE_HIJ.Enabled)
                DOC_DATE_HIJ.Text = Convert.ToString(c["cDateHIJ"].Value);
                //PAY_CODE.Text = Convert.ToString(c["PayCode"].Value);
               // PAY_NAME.Text = General.getName(PAY_CODE.Text, "GEN_PAYTYPE");
               // AMOUNT.Text = Convert.ToString(c["AMOUNT PAID"].Value);
            }
        }

    }
}
