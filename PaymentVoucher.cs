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

namespace Sys_Sols_Inventory
{
    public partial class PaymentVoucher : Form
    {
      //  private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
      //  private SqlCommand cmd = new SqlCommand();
        Class.Ledgers led = new Class.Ledgers();
        Class.Transactions trans = new Class.Transactions();
        PaySupplierDB paysupdbObj = new PaySupplierDB();
        clsCustomer custObj = new clsCustomer();
        PayPaymentVoucherHdrDB pvhdrObj = new PayPaymentVoucherHdrDB();
        Class.DateSettings dset = new Class.DateSettings();
      //  private SqlDataAdapter adapter = new SqlDataAdapter();
        private DataTable table = new DataTable();
        private BindingSource source = new BindingSource();
        Initial mdi = (Initial)Application.OpenForms["Initial"];
        Login lg = (Login)Application.OpenForms["Login"];
        private bool HasArabic = true;
        private string ID = "";
        private string tableHDR = "";
        private string tableDTL = "";
        private string fld = "";
        private string title = "";
        private int form = 0;
        string CompanyName="", Address1, Addres1, Addres2, Phone, Fax, Email, TineNo, Billno, Date, CUSID, Website, panno, vat, logo;
        Class.CompanySetup ComSet = new Class.CompanySetup();
        public PaymentVoucher(int i)
        {
            InitializeComponent();
            //cmd.Connection = conn;
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
                kryptonLabel2.Text = "Customer Code:";
                title = "Receipt Voucher";
            }
            this.Text = title;
            bindledgers();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.F12))
            {
               btnSave.PerformClick();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnSup_Click(object sender, EventArgs e)
        {
            if (form == 0)
            {
                SupplierMasterHelp h = new SupplierMasterHelp(0);
                if (h.ShowDialog() == DialogResult.OK && h.c != null)
                {
                    SUP_CODE.Text = Convert.ToString(h.c[0].Value);
                    SUP_NAME.Text = Convert.ToString(h.c[1].Value);

                    //conn.Open();
                    //cmd.CommandText = "SUP_CREDIT_PUR";
                    string command = "SUP_CREDIT_PUR";
                    //cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.Clear();
                    //cmd.Parameters.AddWithValue("@SUP_CODE", SUP_CODE.Text);
                    pvhdrObj.SupCode = SUP_CODE.Text;
                    SqlDataReader r = pvhdrObj.supCreditPurchaseProcedure(command, "@SUP_CODE");
                    //SqlDataReader r = cmd.ExecuteReader();
                    dgDetail.Rows.Clear();
                    double totalPaid = 0;
                    double totalBal = 0;
                    while (r.Read())
                    {
                        dgDetail.Rows.Add(r["DOC_NO"], r["DOC_DATE_GRE"], r["DOC_DATE_HIJ"], r["NET_VAL"], r["PAID"], 0, r["BALANCE"]);
                        totalPaid = totalPaid + Convert.ToDouble(r["PAID"]);
                        totalBal = totalBal + Convert.ToDouble(r["BALANCE"]);
                    }
                    TOTAL_PAID.Text = totalPaid.ToString();
                    TOTAL_BALANCE.Text = totalBal.ToString();
                    //conn.Close();
                    DbFunctions.CloseConnection();
                    dgClients.Visible = false;
                }
            }
            else
            {
                CommonHelp h = new CommonHelp(0, genEnum.Customer);
                if (h.ShowDialog() == DialogResult.OK && h.c != null)
                {
                    SUP_CODE.Text = Convert.ToString(h.c[0].Value);
                    SUP_NAME.Text = Convert.ToString(h.c[1].Value);

                   // conn.Open();
                  //  cmd.CommandText = "CUS_CREDIT_PUR";
                    string cmd = "CUS_CREDIT_PUR";
                   // cmd.CommandType = CommandType.StoredProcedure;
                  //  cmd.Parameters.Clear();
                  //  cmd.Parameters.AddWithValue("@CUS_CODE", SUP_CODE.Text);
                    pvhdrObj.SupCode = SUP_CODE.Text;
                    SqlDataReader r = pvhdrObj.supCreditPurchaseProcedure(cmd, "@CUS_CODE");
                   // SqlDataReader r = cmd.ExecuteReader();
                    dgDetail.Rows.Clear();
                    double totalPaid = 0;
                    double totalBal = 0;
                    while (r.Read())
                    {
                        dgDetail.Rows.Add(r["DOC_NO"], r["DOC_DATE_GRE"], r["DOC_DATE_HIJ"], r["NET_VAL"], r["PAID"], 0, r["BALANCE"]);
                        totalPaid = totalPaid + Convert.ToDouble(r["PAID"]);
                        totalBal = totalBal + Convert.ToDouble(r["BALANCE"]);
                    }
                    TOTAL_PAID.Text = totalPaid.ToString();
                    TOTAL_BALANCE.Text = totalBal.ToString();
                    //conn.Close();
                    DbFunctions.CloseConnection();
                    dgClients.Visible = false;
                }
            }

            if (dgDetail.Rows.Count <= 0)
            {
                //dgDetail.Rows.Add("dd");
                dgDetail.Rows.Add(" ");
            }
            
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            CommonHelp c = new CommonHelp(0, genEnum.PayType);
            if (c.ShowDialog() == DialogResult.OK && c.c != null)
            {
                PAY_CODE.Text = Convert.ToString(c.c[0].Value);
                PAY_NAME.Text = Convert.ToString(c.c[1].Value);
            }
        }

        private void btnBank_Click(object sender, EventArgs e)
        {
            //bank
        }

        private void btnCurr_Click(object sender, EventArgs e)
        {
            //currency
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
                if ( date <= Convert.ToDateTime( DOC_DATE_GRE.Value.ToShortDateString()))
                {

                }
                else
                {
                    MessageBox.Show("Date Limit Exceeded!!");
                    return false;
                }
            }

            if (dgDetail.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                MessageBox.Show("There are No Credit Purchases");
                return false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if(DialogResult.Yes==MessageBox.Show("Are you sure to continue payment","Alert",MessageBoxButtons.YesNo))
                {
                    if (valid())
                    {
                        string status = "Added!";
                        string query = "";
                        if (ID == "")
                        {
                            
                            DOC_NO.Text = General.generatePayVoucherCode();
                            query  = "INSERT INTO " + tableHDR + "(DOC_NO,DOC_DATE_GRE,DOC_DATE_HIJ," + fld + ",CUR_CODE,AMOUNT,PAY_CODE,BANK_CODE,CHQ_NO,CHQ_DATE,NOTES,TOTAL_PAID,TOTAL_CURRENT,TOTAL_BALANCE) VALUES('" + DOC_NO.Text + "','" + DOC_DATE_GRE.Value.ToString("MM/dd/yyyy") + "','" + DOC_DATE_HIJ.Text + "','" + SUP_CODE.Text + "','" + CUR_CODE.Text + "','" + AMOUNT.Text + "','" + PAY_CODE.Text + "','" + BANK_CODE.Text + "','" + CHQ_NO.Text + "','" + CHQ_DATE.Value.ToString("MM/dd/yyyy") + "','" + NOTES.Text + "','" + TOTAL_PAID.Text + "','" + TOTAL_CURRENT.Text + "','" + TOTAL_BALANCE.Text + "')";
                        }
                        else
                        {
                            status = "Updated!";
                            query  = "UPDATE " + tableHDR + " SET DOC_DATE_GRE = '" + DOC_DATE_GRE.Value.ToString("MM/dd/yyyy") + "',DOC_DATE_HIJ = '" + DOC_DATE_HIJ.Text + "'," + fld + " = '" + SUP_CODE.Text + "',CUR_CODE = '" + CUR_CODE.Text + "',AMOUNT = '" + AMOUNT.Text + "',PAY_CODE = '" + PAY_CODE.Text + "',BANK_CODE = '" + BANK_CODE.Text + "',CHQ_NO = '" + CHQ_NO.Text + "',CHQ_DATE = '" + CHQ_DATE.Value.ToString("MM/dd/yyyy") + "',NOTES = '" + NOTES.Text + "',TOTAL_PAID = '" + TOTAL_PAID.Text + "',TOTAL_CURRENT = '" + TOTAL_CURRENT.Text + "',TOTAL_BALANCE = '" + TOTAL_BALANCE.Text + "' WHERE DOC_NO = '" + DOC_NO.Text + "';DELETE FROM " + tableDTL + " WHERE DOC_NO = '" + DOC_NO.Text + "'";
                        }
                        query += " INSERT INTO " + tableDTL + "(DOC_NO," + fld + ",INV_DATE_GRE,INV_DATE_HIJ,INV_NO,AMOUNT,PAID,BALANCE,CURRENT_PAY_AMOUNT) ";
                        foreach (DataGridViewRow row in dgDetail.Rows)
                        {
                            query += " SELECT '" + DOC_NO.Text + "','" + SUP_CODE.Text + "','" + Convert.ToDateTime(row.Cells[1].Value.ToString()).ToString("MM/dd/yyyy") + "','" + row.Cells[2].Value.ToString() + "','" + row.Cells[0].Value + "','" + row.Cells[3].Value + "','" + row.Cells[4].Value.ToString() + "','" + row.Cells[6].Value.ToString() + "','" + row.Cells[5].Value.ToString() + "' UNION ALL ";
                        }
                        query = query.Substring(0, query.Length - 10);
                        //conn.Open();
                       // cmd.CommandType = CommandType.Text;
                        //cmd.ExecuteNonQuery();
                        //conn.Close();
                        DbFunctions.InsertUpdate(query);
                        MessageBox.Show("Payment Voucher " + status);
                        if (!PAY_CODE.Text.Equals("CHQ"))
                        {
                            paymentVoucherTransaction();
                        }
                        printingrecipt();
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
            double amt = 0;
            if (AMOUNT.Text != "" && AMOUNT.Text != ".")
            {
                amt = Convert.ToDouble(AMOUNT.Text);
            }

            double totalBal = 0;

            double totalCurr = 0;
            double tBalance = 0;
            if (dgDetail.Rows.Count > 0)
            {
                DataGridViewCellCollection first = dgDetail.Rows[0].Cells;
                if (Convert.ToDouble(first[6].Value) >= 0)
                {
                    for (int i = 1; i < dgDetail.Rows.Count; i++)
                    {


                        DataGridViewCellCollection c = dgDetail.Rows[i].Cells;
                        c[6].Value = Convert.ToDouble(c[5].Value) + Convert.ToDouble(c[6].Value);
                        c[5].Value = 0;

                    }

                }
            }

            for (int i = 0; i < dgDetail.Rows.Count; i++)
            {
               
               
                DataGridViewCellCollection c = dgDetail.Rows[i].Cells;
                

                double balAmt = Convert.ToDouble(c[3].Value) - Convert.ToDouble(c[4].Value);
                totalBal = totalBal + balAmt;
               
                if (amt > balAmt)
                {
                    c[5].Value = balAmt;
                    c[6].Value = 0;
                    amt = amt - balAmt;
                    totalCurr = totalCurr + Convert.ToDouble(c[5].Value);
                    tBalance = tBalance + Convert.ToDouble(c[6].Value);
                }
                else if (amt < balAmt)
                {
                    c[5].Value = amt;
                    c[6].Value = balAmt - amt;

                    totalCurr = totalCurr + Convert.ToDouble(c[5].Value);
                    tBalance = tBalance + Convert.ToDouble(c[6].Value);
                    break;
                }
                else if (amt == balAmt)
                {
                    c[5].Value = amt;
                    c[6].Value = 0;

                    totalCurr = totalCurr + Convert.ToDouble(c[5].Value);
                    tBalance = tBalance + Convert.ToDouble(c[6].Value);
                    break;
                }
            }
            if (amt > totalBal)
            {
                AMOUNT.Text = totalBal.ToString();
            }
          

            double total=0, balance=0;
            for (int i = 0; i < dgDetail.Rows.Count; i++)
            {
                DataGridViewCellCollection c = dgDetail.Rows[i].Cells;
                    total = total + Convert.ToDouble(c[5].Value);
                
                balance = balance + Convert.ToDouble(c[6].Value);
            }

            TOTAL_CURRENT.Text = total.ToString();
            TOTAL_BALANCE.Text = balance.ToString();
            
        }
        //-------------------------------
        public void paymentVoucherTransaction()
        {
            trans.VOUCHERTYPE = "Customer Reciept";
            trans.DATED = Convert.ToDateTime(DOC_DATE_GRE.Value).ToString();
            trans.NARRATION = NOTES.Text;
            Login log = (Login)Application.OpenForms["Login"];
            trans.USERID = log.EmpId;
            trans.ACCNAME = SUP_NAME.Text;
            trans.PARTICULARS = CASHACC.Text;
            trans.VOUCHERNO = DOC_NO.Text;
            trans.BRANCH = lg.Branch;
           // trans.ACCID = PARTYACC.SelectedValue.ToString();
            trans.ACCID = SUP_CODE.Text;
            trans.CREDIT = "0";
            trans.DEBIT = AMOUNT.Text;
            trans.PROJECTID = 0;
            trans.SYSTEMTIME = DateTime.Now.ToString();
            trans.insertTransaction();

            trans.PARTICULARS = SUP_NAME.Text;
            trans.ACCNAME = CASHACC.Text;
            trans.VOUCHERNO = DOC_NO.Text;
            trans.ACCID = CASHACC.SelectedValue.ToString();
            trans.DEBIT = "0";
            trans.CREDIT = AMOUNT.Text;
            trans.SYSTEMTIME = DateTime.Now.ToString();
            trans.PROJECTID = 0;
            trans.insertTransaction();
        }
        //-------------------------------
        public void bindledgers()
        {
            DataTable dt2 = new DataTable();
            dt2 = led.Selectledger();
            CASHACC.DataSource = dt2;
            CASHACC.DisplayMember = "LEDGERNAME";
            CASHACC.ValueMember = "LEDGERID";
        }

        public void BindSupplier()
        {
                HasArabic = General.IsEnabled(Settings.Arabic);
              
                //if (HasArabic)
                //    cmd.CommandText = "SELECT CODE,DESC_ENG,DESC_ARB,TYPE,ADDRESS_A,ADDRESS_B,POBOX,TELE1,TELE2,MOBILE,EMAIL,CITY_CODE,COUNTRY_CODE,OPENING_BAL,FAX,CONVERT(NVARCHAR,DATE_GRE,103) AS DATE_GRE,INITIAL,DEFAULT_CURRENCY,CONTACT_PERSON FROM PAY_SUPPLIER";
                //else
                //    cmd.CommandText = "SELECT CODE,DESC_ENG,ADDRESS_A,TYPE,ADDRESS_B,POBOX,TELE1,TELE2,MOBILE,EMAIL,CITY_CODE,COUNTRY_CODE,OPENING_BAL,FAX,CONVERT(NVARCHAR,DATE_GRE,103) AS DATE_GRE,INITIAL,DEFAULT_CURRENCY,CONTACT_PERSON FROM PAY_SUPPLIER";
                //adapter.SelectCommand = cmd;
                //adapter.Fill(table);
                table = paysupdbObj.GetAllDataFromPV(HasArabic);
                source.DataSource = table;
                dgClients.DataSource = source;
                Noteonreciept.Text = "Amount paid agaist purchase";
           
        }
        public void BindCustomer()
        {

            //if (HasArabic)
            //    cmd.CommandText = "SELECT CODE,TYPE,DESC_ENG,DESC_ARB,TYPE,ADDRESS_A,ADDRESS_B,POBOX,TELE1,TELE2,MOBILE,EMAIL,CITY_CODE,COUNTRY_CODE,FAX,OPENING_BAL,DATE_GRE,DEFAULT_CURRENCY,SALESMAN_CODE FROM REC_CUSTOMER";
            //else
            //    cmd.CommandText = "SELECT CODE,DESC_ENG,TYPE,ADDRESS_A,ADDRESS_B,POBOX,TELE1,TELE2,MOBILE,EMAIL,CITY_CODE,COUNTRY_CODE,FAX,OPENING_BAL,DATE_GRE,DEFAULT_CURRENCY,SALESMAN_CODE FROM REC_CUSTOMER";
            //adapter.SelectCommand = cmd;
            //adapter.Fill(table);
            table = custObj.GetAllDataFromPV(HasArabic);
            source.DataSource = table;
            dgClients.DataSource = source;
            Noteonreciept.Text = "Amount paid agaist sales";
        }

        private void PaymentVoucher_Load(object sender, EventArgs e)
        {
            HasArabic = General.IsEnabled(Settings.Arabic);
            if (!HasArabic)
                DOC_DATE_HIJ.Enabled = false;
            AMOUNT.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            PrintPage.SelectedIndex = 0;
            if (fld == "SUP_CODE")
            {
                BindSupplier();
            }
            else
            {
                BindCustomer();
            }
            ActiveControl = SUP_NAME;
        }

        private void btnDoc_Click(object sender, EventArgs e)
        {
            PaymentVoucherHelp h = new PaymentVoucherHelp(0,form);
            if (h.ShowDialog() == DialogResult.OK && h.c != null)
            {
                ID = Convert.ToString(h.c["DOC_NO"].Value);
                DOC_NO.Text = ID;

                //cmd.CommandType = CommandType.Text;
                //cmd.CommandText = "SELECT INV_NO,CONVERT(NVARCHAR,INV_DATE_GRE,103) AS INV_DATE_GRE,INV_DATE_HIJ,AMOUNT,PAID,CURRENT_PAY_AMOUNT,BALANCE FROM "+tableDTL+" WHERE DOC_NO = '" + DOC_NO.Text + "'";
                //conn.Open();
                pvhdrObj.DocNo = DOC_NO.Text;
                SqlDataReader r = pvhdrObj.getAllDataByDocNo(tableDTL);
                //SqlDataReader r = cmd.ExecuteReader();
                dgDetail.Rows.Clear();
                while (r.Read())
                {
                    dgDetail.Rows.Add(r["INV_NO"], r["INV_DATE_GRE"], r["INV_DATE_HIJ"], r["AMOUNT"], r["PAID"], r["CURRENT_PAY_AMOUNT"], r["BALANCE"]);
                }
                //conn.Close();
                DbFunctions.CloseConnection();
                SUP_CODE.Text = Convert.ToString(h.c["SUP_CODE"].Value);
                SUP_NAME.Text = General.getName(SUP_CODE.Text, "PAY_SUPPLIER");
                PAY_CODE.Text = Convert.ToString(h.c["PAY_CODE"].Value);
                PAY_NAME.Text = General.getName(PAY_CODE.Text, "GEN_PAYTYPE");
                CHQ_NO.Text = Convert.ToString(h.c["CHQ_NO"].Value);
                if (!(h.c["CHQ_DATE"].Value.ToString() == ""))
                    CHQ_DATE.Value = Convert.ToDateTime(h.c["CHQ_DATE"].Value.ToString());
                try
                {
                    //DOC_DATE_GRE.Value = DateTime.ParseExact(h.c["DOC_DATE_GRE"].Value.ToString(), "dd/MM/yyyy", null);
                    DOC_DATE_GRE.Value =Convert.ToDateTime(h.c["DOC_DATE_GRE"].Value.ToString());
                }
                catch
                {

                }
                DOC_DATE_HIJ.Text = Convert.ToString(h.c["DOC_DATE_HIJ"].Value);
                CUR_CODE.Text = Convert.ToString(h.c["CUR_CODE"].Value);
                AMOUNT.Text = Convert.ToString(h.c["AMOUNT"].Value);
                BANK_CODE.Text = Convert.ToString(h.c["BANK_CODE"].Value);
                NOTES.Text = Convert.ToString(h.c["NOTES"].Value);
                TOTAL_PAID.Text = Convert.ToString(h.c["TOTAL_PAID"].Value);
                TOTAL_CURRENT.Text = Convert.ToString(h.c["TOTAL_CURRENT"].Value);
                TOTAL_BALANCE.Text = Convert.ToString(h.c["TOTAL_BALANCE"].Value);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            dgDetail.Rows.Clear();
            ID = "";
            DOC_NO.Text = "";
            SUP_CODE.Text = "";
            SUP_NAME.Text = "";
            PAY_CODE.Text = "";
            PAY_NAME.Text = "";
            CHQ_NO.Text = "";
            CHQ_DATE.Value = DateTime.Today;
            DOC_DATE_GRE.Value = DateTime.Today;
            DOC_DATE_HIJ.Text = "";
            CUR_CODE.Text = "";
            AMOUNT.Text = "";
            BANK_CODE.Text = "";
            NOTES.Text = "";
            TOTAL_PAID.Text = "";
            TOTAL_CURRENT.Text = "";
            TOTAL_BALANCE.Text = "";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            btnClear.PerformClick();
            PaymentVoucherHelp h = new PaymentVoucherHelp(1,form);
            h.ShowDialog();
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
            }
            catch
            {
            }
        }
        public void GetCompanyDetails()
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

        private void SUP_NAME_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (SUP_NAME.Text == "")
                {
                    dgClients.Visible = false;
                   
                }
                else
                {
                    dgClients.Visible = true;
                    source.Filter = string.Format("[DESC_ENG] LIKE '%{0}%' ", SUP_NAME.Text);
                    dgClients.ClearSelection();
                }

            }
            catch
            {
            }
        }

        private void SUP_NAME_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyData == Keys.Down)
            {
                if (dgClients.Visible == true)
                {
                    dgClients.Focus();
                    dgClients.CurrentCell = dgClients.Rows[0].Cells[1];
                }
                
            }
        }

        private void dgClients_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                {
                    SUP_NAME.Text = dgClients.CurrentRow.Cells[1].Value.ToString();
                    SUP_CODE.Text = dgClients.CurrentRow.Cells[0].Value.ToString();
                    dgClients.Visible = false;


                   // conn.Open();
                   // cmd.CommandText = "SUP_CREDIT_PUR";
                   // cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.Clear();
                   // cmd.Parameters.AddWithValue("@SUP_CODE", SUP_CODE.Text);
                   // SqlDataReader r = cmd.ExecuteReader();
                    string cmd = "SUP_CREDIT_PUR";
                    pvhdrObj.SupCode = SUP_CODE.Text;
                    SqlDataReader r = pvhdrObj.supCreditPurchaseProcedure(cmd, "@SUP_CODE");
                    dgDetail.Rows.Clear();
                    double totalPaid = 0;
                    double totalBal = 0;
                    while (r.Read())
                    {
                        dgDetail.Rows.Add(r["DOC_NO"], r["DOC_DATE_GRE"], r["DOC_DATE_HIJ"], r["NET_VAL"], r["PAID"], 0, r["BALANCE"]);
                        totalPaid = totalPaid + Convert.ToDouble(r["PAID"]);
                        totalBal = totalBal + Convert.ToDouble(r["BALANCE"]);
                    }
                    TOTAL_PAID.Text = totalPaid.ToString();
                    TOTAL_BALANCE.Text = totalBal.ToString();
                    //conn.Close();
                    DbFunctions.CloseConnection();
                    AMOUNT.Focus();

                }
                if (e.KeyData == Keys.Escape)
                {
                    SUP_NAME.Focus();
                    dgClients.Visible = false;
                }
            }
            catch
            {
            }
        }

        private void AMOUNT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                btnSave.Focus();
            }
        }

        private void dgDetail_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        
        }
    }
}

