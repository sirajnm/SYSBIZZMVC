using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.Text.RegularExpressions;
using Sys_Sols_Inventory.Model;

namespace Sys_Sols_Inventory.Accounts
{
    
    public partial class LedgerReport : Form
    {
        string[] arr = { "january", "february", "March", "April", "may", "june", "july", "August","September","October","November","December" }; 
        public int ledgeridfordrilling = 0;
        public DataGridViewCellCollection c;
        Initial mdi = (Initial)Application.OpenForms["Initial"];
        Login lg = (Login)Application.OpenForms["Login"];
        public string ClosingBal;
        public static bool checkLedger;
        TbLedgersDB ldgObj = new TbLedgersDB();
        Company cmp = Common.getCompany();

        public LedgerReport()
        {
            InitializeComponent();
        }
    
    public int getMonth(string month)
    {
        for(int i = 0 ; i< arr.Length ; i++)
        {
            if(arr[i].Contains(month) || month.Contains(arr[i]))
            return i+1;
        }
        return -1;// if month is invalid , return -1
    }
        public LedgerReport(int id,int year,string month)
        {
            InitializeComponent();
            ledgeridfordrilling = id;
            int y = year;
            int mo = getMonth(month);
            try
            {
                DateTime val = new DateTime(year, mo, 1);
                int lastDayOfMonth = DateTime.DaysInMonth(val.Year, val.Month);
                DateTime en = new DateTime(year, mo, lastDayOfMonth);
               // DateTime s = DateTime.ParseExact(st, "MMMMddyyyy", CultureInfo.InvariantCulture);
                Date_From.Value = val;
                Date_To.Value = en;
                //this.Load -= new EventHandler(LedgerReport_Load); 
            }
            catch (Exception ex) { string st = ex.Message; }
        }

        Class.Ledgers led = new Class.Ledgers();
        double SumCredit = 0, SumDebit = 0;
        double Balance = 0;
        string ClosingBalance = "0.00";
        Class.CompanySetup ComSet = new Class.CompanySetup();

        DateTime FinancialStart;
        DateTime FinancialEnd;

        StringFormat strFormat; //Used to format the grid rows.
        ArrayList arrColumnLefts = new ArrayList();//Used to save left coordinates of columns
        ArrayList arrColumnWidths = new ArrayList();//Used to save column widths
        int iCellHeight = 0; //Used to get/set the datagridview cell height
        int iTotalWidth = 0; //
        int iRow = 0;//Used as counter
        bool bFirstPage = false; //Used to check whether we are printing first page
        bool bNewPage = false;// Used to check whether we are printing a new page
        int iHeaderHeight = 0; //Used for the header height
       

        public void bindledger()
        {
            DataTable dt = new DataTable();
            dt = led.SelectLedgerNmae();
            DataRow row = dt.NewRow();
            dt.Rows.InsertAt(row, 0);
            drpLedger.DisplayMember = "LEDGERNAME";
            drpLedger.ValueMember = "LEDGERID";
            drpLedger.DataSource = dt;
            if (dt.Rows.Count > 0)
            {
                drpLedger.SelectedIndex = 1;
            }
           
        }

        public void GetFinancialYear()
        {
            try
            {
                DataTable dt = new DataTable();
                ComSet.Status = true;
                dt = ComSet.GetFinancialYear();

                FinancialStart = Convert.ToDateTime(dt.Rows[0][1]);

                FinancialEnd= Convert.ToDateTime(dt.Rows[0][2]);

                //Date_From.MinDate = FinancialStart;
                //Date_From.MaxDate = FinancialEnd;

                //Date_To.MinDate = FinancialStart;
                //Date_To.MaxDate = FinancialEnd;
            }
            catch
            {
            }
        }
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
            //  else if (e.KeyCode == Keys.S && e.Control)
            else if (keyData == (Keys.Alt | Keys.S))
            {


            }
            if (keyData == (Keys.F3))
            {
               
            }
            else if (keyData == (Keys.Alt | Keys.N))
            {



            }




            return base.ProcessCmdKey(ref msg, keyData);

        }

        private void LedgerReport_Load(object sender, EventArgs e)
        {
            if (Regex.IsMatch(cmp.Name, "PTL"))
            {
                dataGridViewPTL.Visible = true;
                dataGridViewPTL.BringToFront();
                dgledgerTrns.Visible = false;
            }
            else
            {
                dgledgerTrns.Visible = true;
                dgledgerTrns.BringToFront();
            }
            bindledger();
            if (!drpLedger.Text.Equals(""))
            {
                drpLedger.SelectedIndex = 1;
            }
            GetFinancialYear();
        
            ActiveControl = drpLedger;
            DateTime date =Convert.ToDateTime(ComSet.GettDate());
            var firstday= new DateTime (date.Year, date.Month, 1);
          //  Date_From.Text = firstday.ToShortDateString();

            if (ledgeridfordrilling != 0)
            {
                drpLedger.SelectedValue = ledgeridfordrilling;
                btnSave.PerformClick();
            }
            lbltitle.Text = "Ledger Report from " + Date_From.Value.ToShortDateString() + " To" + Date_To.Value.ToShortDateString();
            btnSave.Focus();
            btnSave.PerformClick();
          
        }

        public bool valid()
        {
            if (drpLedger.Text == "")
            {
                MessageBox.Show("Select a ledger");
                return false;
            }
            else
            {
                return true;
            }
        }
        public void OpeningBalance()
        {
            try
            {
                dgledgerTrns.Rows.Add("", "", "", "", "", "", "", "", "0.00", "", "");
            }
            catch(Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }


        public void GettingClosingBalance()
        {
            led.AccName = drpLedger.Text;
            led.LEDGERID = Convert.ToInt32(drpLedger.SelectedValue);
            led.date1 = Convert.ToDateTime(FinancialStart.ToShortDateString());
            led.date2 = Convert.ToDateTime(Date_From.Value.ToShortDateString());

            DataTable dt = new DataTable();
            dt = led.SelectLedgerClosingBalance();

            if (dt.Rows.Count > 0)
            {
                ClosingBalance =dt.Rows[0]["Closing"].ToString();
                if (ClosingBalance == "")
                {
                    Balance = 0;
                    ClosingBalance = "0";
                }
                if (ChekOPening.Checked == false)
                {
                    Balance = 0;
                    ClosingBalance = "0";
                }
                else
                {
                    Balance = Convert.ToDouble(ClosingBalance);
                }
                if (Convert.ToDecimal(ClosingBalance) < 0)
                {
                    dgledgerTrns.Rows.Add("", "", "", "", "", "", "", "0.00", (Math.Abs(Convert.ToDecimal(ClosingBalance))).ToString("n2"), (Math.Abs(Convert.ToDecimal(ClosingBalance))).ToString("n2") + " Cr", ClosingBalance, "");
                    ClosingBalance = Math.Abs(Convert.ToDouble(ClosingBalance)).ToString("n2") + " Cr";
                }
                else
                {
                    dgledgerTrns.Rows.Add("", "", "", "", "", "", "", (Math.Abs(Convert.ToDecimal(ClosingBalance))).ToString("n2"), "0.00", (Math.Abs(Convert.ToDecimal(ClosingBalance))).ToString("n2") + " Dr", ClosingBalance, "");
                    ClosingBalance = Math.Abs(Convert.ToDouble(ClosingBalance)).ToString("n2") + " Dr";
                }
            }

         
        }
        public void GettingClosingBalancePTL()
        {
            led.AccName = drpLedger.Text;
            led.LEDGERID = Convert.ToInt32(drpLedger.SelectedValue);
            led.date1 = Convert.ToDateTime(FinancialStart.ToShortDateString());
            led.date2 = Convert.ToDateTime(Date_From.Value.ToShortDateString());

            DataTable dt = new DataTable();
            dt = led.SelectLedgerClosingBalance();

            if (dt.Rows.Count > 0)
            {
                ClosingBalance = dt.Rows[0]["Closing"].ToString();
                if (ClosingBalance == "")
                {
                    Balance = 0;
                    ClosingBalance = "0";
                }
                if (ChekOPening.Checked == false)
                {
                    Balance = 0;
                    ClosingBalance = "0";
                }
                else
                {
                    Balance = Convert.ToDouble(ClosingBalance);
                }
                if (Convert.ToDecimal(ClosingBalance) < 0)
                {
                    dataGridViewPTL.Rows.Add("", "", "", "", "", "", "", "0.00", (Math.Abs(Convert.ToDecimal(ClosingBalance))).ToString("n2"), (Math.Abs(Convert.ToDecimal(ClosingBalance))).ToString("n2") + " Cr", ClosingBalance, "");
                    ClosingBalance = Math.Abs(Convert.ToDouble(ClosingBalance)).ToString("n2") + " Cr";
                }
                else
                {
                    dataGridViewPTL.Rows.Add("", "", "", "", "", "", "", (Math.Abs(Convert.ToDecimal(ClosingBalance))).ToString("n2"), "0.00", (Math.Abs(Convert.ToDecimal(ClosingBalance))).ToString("n2") + " Dr", ClosingBalance, "");
                    ClosingBalance = Math.Abs(Convert.ToDouble(ClosingBalance)).ToString("n2") + " Dr";
                }
            }


        }

        string a;

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Regex.IsMatch(cmp.Name, "PTL"))
            {
                getPtlLedgerTtans();
            }
            else
            {
                dgledgerTrns.Columns["DATED"].DisplayIndex = 0;
                dgledgerTrns.Columns["REFERENCE"].DisplayIndex = 2;
                lbltitle.Text = "Ledger Report from " + Date_From.Value.ToShortDateString() + " To" + Date_To.Value.ToShortDateString();
                dgledgerTrns.Rows.Clear();
                Balance = 0;

                ClosingBalance = "0";
                if (valid())
                {
                    try
                    {
                        if (FinancialStart.ToShortDateString() != Date_From.Value.ToShortDateString())
                        {
                            GettingClosingBalance();
                        }


                        led.AccName = drpLedger.Text;
                        led.LEDGERID = Convert.ToInt32(drpLedger.SelectedValue);
                        led.date1 = Convert.ToDateTime(Date_From.Value.ToShortDateString());
                        led.date2 = Convert.ToDateTime(Date_To.Value.ToShortDateString());

                        DataTable dt = led.SelectLedgerTransactions();
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                dgledgerTrns.Rows.Add((i + 1).ToString(), dt.Rows[i]["TRANSACTIONID"].ToString(), dt.Rows[i]["VOUCHERNO"].ToString(), Convert.ToDateTime(dt.Rows[i]["DATED"]).ToShortDateString(), dt.Rows[i]["VOUCHERTYPE"].ToString(), dt.Rows[i]["ACCNAME"].ToString(), dt.Rows[i]["PARTICULARS"].ToString(), dt.Rows[i]["DEBIT"].ToString(), dt.Rows[i]["CREDIT"].ToString(), "", "", dt.Rows[i]["NARRATION"].ToString(), dt.Rows[i]["ACCID"].ToString(), dt.Rows[i]["SYSTEM_TIME"].ToString());
                            }

                        }
                        Sum();
                        DataRow newrow1 = dt.NewRow();
                        dt.Rows.Add(newrow1);


                        dgledgerTrns.Columns["DEBIT"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
                        dgledgerTrns.Columns["CREDIT"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;

                        OpeningbalanceShowGrid();


                        for (int i = 0; i < dgledgerTrns.Rows.Count; i++)
                        {
                            string vr, cr;

                            DataTable dtCloned = dt.Clone();
                            dtCloned.Columns[7].DataType = typeof(string);
                            dtCloned.Columns[8].DataType = typeof(string);
                            vr = dgledgerTrns.Rows[i].Cells[7].Value.ToString(); // here you go vr = the value of the cel
                            cr = dgledgerTrns.Rows[i].Cells[8].Value.ToString();
                            if (vr == "0.00") // you can check for anything
                            {

                                dgledgerTrns.Rows[i].Cells[7].Value = DBNull.Value;
                                // you can format this cell 
                            }
                            if (cr == "0.00") // you can check for anything
                            {

                                dgledgerTrns.Rows[i].Cells[8].Value = DBNull.Value;
                                // you can format this cell 
                            }
                        }
                        refernce_add();

                    }
                    catch (Exception ee)
                    {
                        MessageBox.Show(ee.Message);
                    }
                }
            }
        }
        public void getPtlLedgerTtans()
        {
            dataGridViewPTL.Columns["DATES"].DisplayIndex = 0;
            dataGridViewPTL.Columns["REFERENCES"].DisplayIndex = 2;
            dataGridViewPTL.Columns["QUANTITY"].DisplayIndex =8;
            dataGridViewPTL.Columns["RATE"].DisplayIndex = 9;
            dataGridViewPTL.Columns["VEHICLENO"].DisplayIndex = 13;
            lbltitle.Text = "Ledger Report from " + Date_From.Value.ToShortDateString() + " To" + Date_To.Value.ToShortDateString();
            dataGridViewPTL.Rows.Clear();
            Balance = 0;

            ClosingBalance = "0";
            if (valid())
            {
                try
                {
                    if (FinancialStart.ToShortDateString() != Date_From.Value.ToShortDateString())
                    {
                        GettingClosingBalancePTL();
                    }


                    led.AccName = drpLedger.Text;
                    led.LEDGERID = Convert.ToInt32(drpLedger.SelectedValue);
                    led.date1 = Convert.ToDateTime(Date_From.Value.ToShortDateString());
                    led.date2 = Convert.ToDateTime(Date_To.Value.ToShortDateString());
                    DataTable dt;
                    if (drpLedger.Text == "SALES ACCOUNT")
                    {
                        dt = led.SelectLedgerTransactionsPTL();
                    }
                    else
                    {
                         dt = led.SelectLedgerTransactionsPTL();
                    }
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            dataGridViewPTL.Rows.Add((i + 1).ToString(), dt.Rows[i]["TRANSACTIONID"].ToString(), dt.Rows[i]["VOUCHERNO"].ToString(), Convert.ToDateTime(dt.Rows[i]["DATED"]).ToShortDateString(), dt.Rows[i]["VOUCHERTYPE"].ToString(), dt.Rows[i]["ACCNAME"].ToString(), dt.Rows[i]["PARTICULARS"].ToString(), dt.Rows[i]["DEBIT"].ToString(), dt.Rows[i]["CREDIT"].ToString(), "", "", dt.Rows[i]["NARRATION"].ToString(), dt.Rows[i]["ACCID"].ToString(), dt.Rows[i]["SYSTEM_TIME"].ToString(), "", dt.Rows[i]["SHIP_VEHICLE_NO"].ToString(), dt.Rows[i]["QUANTITY"].ToString(), dt.Rows[i]["PRICE"].ToString());
                        }

                    }
                    SumPTL();
                    DataRow newrow1 = dt.NewRow();
                    dt.Rows.Add(newrow1);


                    dataGridViewPTL.Columns["DEBITS"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
                    dataGridViewPTL.Columns["CREDITS"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;

                    OpeningbalanceShowGridPTL();


                    for (int i = 0; i < dataGridViewPTL.Rows.Count; i++)
                    {
                        string vr, cr;

                        DataTable dtCloned = dt.Clone();
                        dtCloned.Columns[7].DataType = typeof(string);
                        dtCloned.Columns[8].DataType = typeof(string);
                        vr = dataGridViewPTL.Rows[i].Cells[7].Value.ToString(); // here you go vr = the value of the cel
                        cr = dataGridViewPTL.Rows[i].Cells[8].Value.ToString();
                        if (vr == "0.00") // you can check for anything
                        {

                            dataGridViewPTL.Rows[i].Cells[7].Value = DBNull.Value;
                            // you can format this cell 
                        }
                        if (cr == "0.00") // you can check for anything
                        {

                            dataGridViewPTL.Rows[i].Cells[8].Value = DBNull.Value;
                            // you can format this cell 
                        }
                    }
                    refernce_addPTL();
                   //dataGridViewPTL.Sort(dataGridViewPTL.Columns[8], ListSortDirection.Descending);
                 //   this.dataGridViewPTL.Sort(this.dataGridViewPTL.Columns["REFERENCES"], ListSortDirection.Ascending);
                 

                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }
            }
        }

        public void  OpeningbalanceShowGrid()
        {
            try
            {
                string ba;
                if (ChekOPening.Checked == true)
                    ba = "Ledger Balance";
                else
                    ba = "Balance";
                dgledgerTrns.Rows.Add("","", "", "", "", "", "", "", "", "", "", "");
                string openbalance = ClosingBal.Substring(0,ClosingBal.Length - 2);

                double fulldebit = SumDebit;// +Convert.ToDouble(openbalance);
                double fullcredit = SumCredit;// +Convert.ToDouble(openbalance);
                dgledgerTrns.Rows.Add("","", "", "", ba,"","", fulldebit.ToString("N2"), fullcredit.ToString("N2"), ClosingBal, "", "");
                dgledgerTrns.Rows.Add("","", "", "", "", "", "", "**************", "", "", "", "");
                dgledgerTrns.Rows.Add("","", "", "", "", "", "Opening Balance",ClosingBalance, "", "", "", "");
                dgledgerTrns.Rows.Add("","", "", "", "", "", "Closing Balance",ClosingBal, "","" , "", "");

                //newRow3["DEBIT"] = dgledgerTrns.Rows[dgledgerTrns.Rows.Count - 3].Cells["BalanceS"].Value.ToString();
                //dgledgerTrns.Rows.Add(newRow3);

                //   newRow2["DEBIT"] = SumDebit.ToString();
                System.Windows.Forms.DataGridViewCellStyle boldStyle = new System.Windows.Forms.DataGridViewCellStyle();
                boldStyle.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold);
                dgledgerTrns.Rows[dgledgerTrns.Rows.Count - 4].DefaultCellStyle = boldStyle;
                 dgledgerTrns.Rows[dgledgerTrns.Rows.Count - 2].DefaultCellStyle.ForeColor = System.Drawing.Color.Red;
                dgledgerTrns.Rows[dgledgerTrns.Rows.Count - 2].DefaultCellStyle.Font = new System.Drawing.Font(dgledgerTrns.Font, FontStyle.Bold);


                dgledgerTrns.Rows[dgledgerTrns.Rows.Count - 1].DefaultCellStyle.ForeColor = System.Drawing.Color.Red;
                dgledgerTrns.Rows[dgledgerTrns.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font(dgledgerTrns.Font, FontStyle.Bold);
                dgledgerTrns.FirstDisplayedScrollingRowIndex = dgledgerTrns.RowCount - 1;
            }
            catch (Exception ez)
            {
                string e= ez.Message;
            }
        }
        public void OpeningbalanceShowGridPTL()
        {
            try
            {
                string ba;
                if (ChekOPening.Checked == true)
                    ba = "Ledger Balance";
                else
                    ba = "Balance";
                dataGridViewPTL.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "");
                string openbalance = ClosingBal.Substring(0, ClosingBal.Length - 2);

                double fulldebit = SumDebit;// +Convert.ToDouble(openbalance);
                double fullcredit = SumCredit;// +Convert.ToDouble(openbalance);
                dataGridViewPTL.Rows.Add("", "", "", "", ba, "", "", fulldebit.ToString("N2"), fullcredit.ToString("N2"), ClosingBal, "", "");
                dataGridViewPTL.Rows.Add("", "", "", "", "", "", "", "**************", "", "", "", "");
                dataGridViewPTL.Rows.Add("", "", "", "", "", "", "Opening Balance", ClosingBalance, "", "", "", "");
                dataGridViewPTL.Rows.Add("", "", "", "", "", "", "Closing Balance", ClosingBal, "", "", "", "");

                //newRow3["DEBIT"] = dgledgerTrns.Rows[dgledgerTrns.Rows.Count - 3].Cells["BalanceS"].Value.ToString();
                //dgledgerTrns.Rows.Add(newRow3);

                //   newRow2["DEBIT"] = SumDebit.ToString();
                System.Windows.Forms.DataGridViewCellStyle boldStyle = new System.Windows.Forms.DataGridViewCellStyle();
                boldStyle.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold);
                dataGridViewPTL.Rows[dataGridViewPTL.Rows.Count - 4].DefaultCellStyle = boldStyle;
                dataGridViewPTL.Rows[dataGridViewPTL.Rows.Count - 2].DefaultCellStyle.ForeColor = System.Drawing.Color.Red;
                dataGridViewPTL.Rows[dataGridViewPTL.Rows.Count - 2].DefaultCellStyle.Font = new System.Drawing.Font(dgledgerTrns.Font, FontStyle.Bold);


                dataGridViewPTL.Rows[dataGridViewPTL.Rows.Count - 1].DefaultCellStyle.ForeColor = System.Drawing.Color.Red;
                dataGridViewPTL.Rows[dataGridViewPTL.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font(dgledgerTrns.Font, FontStyle.Bold);
                dataGridViewPTL.FirstDisplayedScrollingRowIndex = dataGridViewPTL.RowCount - 1;
            }
            catch (Exception ez)
            {
                string e = ez.Message;
            }
        }

        public void AutoNumberRowsForGridView()
        {
            if (dgledgerTrns != null)
            {
                for (int count = 0; (count <= (dgledgerTrns.Rows.Count - 2)); count++)
                {
                    dgledgerTrns.Rows[count].Cells[0].Value = string.Format((count + 1).ToString(), "");
                }
            }
        }
       
        public void Sum()
        {
            SumCredit = 0;
            SumDebit = 0;
            Balance = 0;
            for (int i = 0; i < dgledgerTrns.Rows.Count; i++)
            {
                string balanceval;
                Balance = Balance - Convert.ToDouble(dgledgerTrns.Rows[i].Cells["CREDIT"].Value) + Convert.ToDouble(dgledgerTrns.Rows[i].Cells["DEBIT"].Value);
                dgledgerTrns.Rows[i].Cells["BalanceCalc"].Value = Balance;
                if (Balance < 0)
                {
                    dgledgerTrns.Rows[i].Cells["BalanceS"].Value = Math.Abs(Balance).ToString("n2") + " Cr";
                    dgledgerTrns.Columns["BalanceS"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
                }
                else
                {
                    dgledgerTrns.Rows[i].Cells["BalanceS"].Value = Math.Abs(Balance).ToString("n2") + " Dr";
                    dgledgerTrns.Columns["BalanceS"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
                }
                SumCredit = SumCredit + Convert.ToDouble(dgledgerTrns.Rows[i].Cells["CREDIT"].Value);
                SumDebit = SumDebit + Convert.ToDouble(dgledgerTrns.Rows[i].Cells["DEBIT"].Value);
                if (Balance < 0)
                {
                    balanceval = (Convert.ToDouble(Balance * -1)).ToString("n2") + " Cr";
                }
                else
                {
                    balanceval = Balance.ToString("n2") + " Dr";
                }
                ClosingBal = balanceval.ToString();

            }
        }
        public void SumPTL()
        {
            SumCredit = 0;
            SumDebit = 0;
            Balance = 0;
            for (int i = 0; i < dataGridViewPTL.Rows.Count; i++)
            {
                string balanceval;
                Balance = Balance - Convert.ToDouble(dataGridViewPTL.Rows[i].Cells["CREDITS"].Value) + Convert.ToDouble(dataGridViewPTL.Rows[i].Cells["DEBITS"].Value);
                dataGridViewPTL.Rows[i].Cells["BALNCCALC"].Value = Balance;
                if (Balance < 0)
                {
                    dataGridViewPTL.Rows[i].Cells["BALANCESS"].Value = Math.Abs(Balance).ToString("n2") + " Cr";
                    dataGridViewPTL.Columns["BALANCESS"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
                }
                else
                {
                    dataGridViewPTL.Rows[i].Cells["BALANCESS"].Value = Math.Abs(Balance).ToString("n2") + " Dr";
                    dataGridViewPTL.Columns["BALANCESS"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
                }
                SumCredit = SumCredit + Convert.ToDouble(dataGridViewPTL.Rows[i].Cells["CREDITS"].Value);
                SumDebit = SumDebit + Convert.ToDouble(dataGridViewPTL.Rows[i].Cells["DEBITS"].Value);
                if (Balance < 0)
                {
                    balanceval = (Convert.ToDouble(Balance * -1)).ToString("n2") + " Cr";
                }
                else
                {
                    balanceval = Balance.ToString("n2") + " Dr";
                }
                ClosingBal = balanceval.ToString();

            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            dgledgerTrns.Rows.Clear();
            drpLedger.SelectedIndex = -1;
            Date_From.Value = DateTime.Now;
            Date_To.Value = DateTime.Now;
            ledgeridfordrilling = 0;
            dataGridViewPTL.Rows.Clear();
        }

        private void PicPrint_Click(object sender, EventArgs e)
        {

            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDocument1;
            printDialog.UseEXDialog = true;
            //Get the document
            if (DialogResult.OK == printDialog.ShowDialog())
            {
                printDocument1.DocumentName = "Test Page Print";
                printDocument1.Print();
            }
        }

        private void PicExcel_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();


            //    // creating new WorkBook within Excel application
            //    Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);


            //    // creating new Excelsheet in workbook
            //    Microsoft.Office.Interop.Excel._Worksheet worksheet = null;

            //    // see the excel sheet behind the program
            //    app.Visible = true;

            //    // get the reference of first sheet. By default its name is Sheet1.
            //    // store its reference to worksheet
            //    worksheet = workbook.Sheets["Sheet1"];
            //    worksheet = workbook.ActiveSheet;

            //    // changing the name of active sheet
            //    worksheet.Name = "Exported from gridview";


            //    // storing header part in Excel
            //    for (int i = 1; i < dgledgerTrns.Columns.Count + 1; i++)
            //    {
            //        worksheet.Cells[1, i] = dgledgerTrns.Columns[i - 1].HeaderText;
            //    }



            //    // storing Each row and column value to excel sheet
            //    for (int i = 0; i < dgledgerTrns.Rows.Count - 1; i++)
            //    {
            //        for (int j = 0; j < dgledgerTrns.Columns.Count; j++)
            //        {
            //            worksheet.Cells[i + 2, j + 1] = dgledgerTrns.Rows[i].Cells[j].Value.ToString();
            //        }
            //    }


            //    // save the application
            //    workbook.SaveAs("c:\\output.xls", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            //    // Exit from the application
            //    app.Quit();

            //}
            //catch
            //{

            //}
            try
            {
                Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();


                // creating new WorkBook within Excel application
                Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);


                // creating new Excelsheet in workbook
                Microsoft.Office.Interop.Excel._Worksheet worksheet = null;

                // see the excel sheet behind the program
                app.Visible = true;

                // get the reference of first sheet. By default its name is Sheet1.
                // store its reference to worksheet
                worksheet = workbook.Sheets["Sheet1"];
                worksheet = workbook.ActiveSheet;

                // changing the name of active sheet
                worksheet.Name = "Exported from gridview";


                // storing header part in Excel
                for (int i = 1; i < dgledgerTrns.Columns.Count + 1; i++)
                {
                    worksheet.Cells[1, i] = dgledgerTrns.Columns[i - 1].HeaderText;
                }



                // storing Each row and column value to excel sheet
                for (int i = 0; i < dgledgerTrns.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < dgledgerTrns.Columns.Count; j++)
                    {
                        worksheet.Cells[i + 2, j + 1] = dgledgerTrns.Rows[i].Cells[j].Value.ToString();
                    }
                }


                // save the application
                workbook.SaveAs("c:\\output.xls", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                // Exit from the application
                app.Quit();

            }
            catch
            {

            }
        }

        private void PicPdf_Click(object sender, EventArgs e)
        {
            //Creating iTextSharp Table from the DataTable data
            PdfPTable pdfTable = new PdfPTable(dgledgerTrns.ColumnCount);
            pdfTable.DefaultCell.Padding = 3;
            pdfTable.WidthPercentage = 100;
            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfTable.DefaultCell.BorderWidth = 1;

            //Adding Header row
            foreach (DataGridViewColumn column in dgledgerTrns.Columns)
            {
                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                cell.BackgroundColor = new iTextSharp.text.Color(240, 240, 240);
                pdfTable.AddCell(cell);
            }

            //Adding DataRow
            foreach (DataGridViewRow row in dgledgerTrns.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {

                    if (cell.Value != null)
                    {

                        pdfTable.AddCell(cell.Value.ToString());
                    }
                    else
                    {
                        pdfTable.AddCell("");
                    }
                }
            }

            //Exporting to PDF
            //string folderPath = "C:\\PDFs\\";
            //if (!Directory.Exists(folderPath))
            //{
            //    Directory.CreateDirectory(folderPath);
            //}
            using (FileStream stream = new FileStream(Application.StartupPath + "Trail Balance.pdf", FileMode.Create))
            {
                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();
                pdfDoc.Add(pdfTable);

                pdfDoc.Close();
                stream.Close();
                System.Diagnostics.Process.Start(Application.StartupPath + "Trail Balance.pdf");
            }
        }

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                strFormat = new StringFormat();
                strFormat.Alignment = StringAlignment.Near;
                strFormat.LineAlignment = StringAlignment.Center;
                strFormat.Trimming = StringTrimming.EllipsisCharacter;

                arrColumnLefts.Clear();
                arrColumnWidths.Clear();
                iCellHeight = 0;
                iRow = 0;
                bFirstPage = true;
                bNewPage = true;

                // Calculating Total Widths
                iTotalWidth = 0;
                foreach (DataGridViewColumn dgvGridCol in dgledgerTrns.Columns)
                {
                    iTotalWidth += dgvGridCol.Width;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                //Set the left margin
                int iLeftMargin = e.MarginBounds.Left;
                //Set the top margin
                int iTopMargin = e.MarginBounds.Top;
                //Whether more pages have to print or not
                bool bMorePagesToPrint = false;
                int iTmpWidth = 0;

                //For the first page to print set the cell width and header height
                if (bFirstPage)
                {
                    foreach (DataGridViewColumn GridCol in dgledgerTrns.Columns)
                    {
                        iTmpWidth = (int)(Math.Floor((double)((double)GridCol.Width /
                                       (double)iTotalWidth * (double)iTotalWidth *
                                       ((double)e.MarginBounds.Width / (double)iTotalWidth))));

                        iHeaderHeight = (int)(e.Graphics.MeasureString(GridCol.HeaderText,
                                    GridCol.InheritedStyle.Font, iTmpWidth).Height) + 11;

                        // Save width and height of headres
                        arrColumnLefts.Add(iLeftMargin);
                        arrColumnWidths.Add(iTmpWidth);
                        iLeftMargin += iTmpWidth;
                    }
                }
                //Loop till all the grid rows not get printed
                while (iRow <= dgledgerTrns.Rows.Count - 1)
                {
                    DataGridViewRow GridRow = dgledgerTrns.Rows[iRow];
                    //Set the cell height
                    iCellHeight = GridRow.Height + 5;
                    int iCount = 0;
                    //Check whether the current page settings allo more rows to print
                    if (iTopMargin + iCellHeight >= e.MarginBounds.Height + e.MarginBounds.Top)
                    {
                        bNewPage = true;
                        bFirstPage = false;
                        bMorePagesToPrint = true;
                        break;
                    }
                    else
                    {
                        if (bNewPage)
                        {
                            //Draw Header
                            e.Graphics.DrawString("Ledger Report "+drpLedger.Text, new System.Drawing.Font(dgledgerTrns.Font, FontStyle.Bold),
                                    Brushes.Black, e.MarginBounds.Left, e.MarginBounds.Top -
                                    e.Graphics.MeasureString("Ledger Report " + drpLedger.Text, new System.Drawing.Font(dgledgerTrns.Font,
                                    FontStyle.Bold), e.MarginBounds.Width).Height - 13);

                            String strDate = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToShortTimeString();
                            //Draw Date
                            e.Graphics.DrawString(strDate, new System.Drawing.Font(dgledgerTrns.Font, FontStyle.Bold),
                                    Brushes.Black, e.MarginBounds.Left + (e.MarginBounds.Width -
                                    e.Graphics.MeasureString(strDate, new System.Drawing.Font(dgledgerTrns.Font,
                                    FontStyle.Bold), e.MarginBounds.Width).Width), e.MarginBounds.Top -
                                    e.Graphics.MeasureString("Ledger Report " + drpLedger.Text, new System.Drawing.Font(new System.Drawing.Font(dgledgerTrns.Font,
                                    FontStyle.Bold), FontStyle.Bold), e.MarginBounds.Width).Height - 13);

                            //Draw Columns                 
                            iTopMargin = e.MarginBounds.Top;
                            foreach (DataGridViewColumn GridCol in dgledgerTrns.Columns)
                            {
                                e.Graphics.FillRectangle(new SolidBrush(System.Drawing.Color.LightGray),
                                    new System.Drawing.Rectangle((int)arrColumnLefts[iCount], iTopMargin,
                                    (int)arrColumnWidths[iCount], iHeaderHeight));

                                e.Graphics.DrawRectangle(Pens.Black,
                                    new System.Drawing.Rectangle((int)arrColumnLefts[iCount], iTopMargin,
                                    (int)arrColumnWidths[iCount], iHeaderHeight));

                                e.Graphics.DrawString(GridCol.HeaderText, GridCol.InheritedStyle.Font,
                                    new SolidBrush(GridCol.InheritedStyle.ForeColor),
                                    new RectangleF((int)arrColumnLefts[iCount], iTopMargin,
                                    (int)arrColumnWidths[iCount], iHeaderHeight), strFormat);
                                iCount++;
                            }
                            bNewPage = false;
                            iTopMargin += iHeaderHeight;
                        }
                        iCount = 0;
                        //Draw Columns Contents                
                        foreach (DataGridViewCell Cel in GridRow.Cells)
                        {
                            if (Cel.Value != null)
                            {
                                e.Graphics.DrawString(Cel.Value.ToString(), Cel.InheritedStyle.Font,
                                            new SolidBrush(Cel.InheritedStyle.ForeColor),
                                            new RectangleF((int)arrColumnLefts[iCount], (float)iTopMargin,
                                            (int)arrColumnWidths[iCount], (float)iCellHeight), strFormat);
                            }
                            //Drawing Cells Borders 
                            e.Graphics.DrawRectangle(Pens.Black, new System.Drawing.Rectangle((int)arrColumnLefts[iCount],
                                    iTopMargin, (int)arrColumnWidths[iCount], iCellHeight));

                            iCount++;
                        }
                    }
                    iRow++;
                    iTopMargin += iCellHeight;
                }

                //If more lines exist, print another page.
                if (bMorePagesToPrint)
                    e.HasMorePages = true;
                else
                    e.HasMorePages = false;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void drpLedger_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == (Keys.Enter | Keys.Tab))
            {
                if (drpLedger.SelectedIndex >= 0)
                {
                    btnSave.Focus();
                }
            }
        }
        public void refernce_add()
        {
            for (int i = 0; i < dgledgerTrns.RowCount; i++)
            {
                if (dgledgerTrns.Rows[i].Cells["VOUCHERTYPE"].Value.ToString() != "" && dgledgerTrns.Rows[i].Cells["VOUCHERTYPE"].Value != null)
                {
                    string type = dgledgerTrns.Rows[i].Cells["VOUCHERTYPE"].Value.ToString();
                    if (type.Equals("Purchase") || type.Equals("Purchase Return"))
                    {
                        DataTable dt = getref("INV_PURCHASE_HDR", dgledgerTrns.Rows[i].Cells["VOUCHERNO"].Value.ToString());
                        if (dt.Rows.Count > 0)
                        {
                            dgledgerTrns.Rows[i].Cells["REFERENCE"].Value = dt.Rows[0][0].ToString();
                        }
                    }
                    else if (type.ToLower().StartsWith("sales") || type.ToLower().StartsWith("Sales Return"))
                    {
                        DataTable dt = getref("INV_SALES_HDR", dgledgerTrns.Rows[i].Cells["VOUCHERNO"].Value.ToString());
                        if (dt.Rows.Count > 0)
                        {
                            dgledgerTrns.Rows[i].Cells["REFERENCE"].Value = dt.Rows[0][0].ToString();
                        }
                    }
                    else if (type.ToLower().StartsWith("cash payment"))
                    {
                        DataTable dt = getref_recpay("PAY_PAYMENT_VOUCHER_HDR", dgledgerTrns.Rows[i].Cells["VOUCHERNO"].Value.ToString());
                        if (dt.Rows.Count > 0)
                        {
                            dgledgerTrns.Rows[i].Cells["REFERENCE"].Value = dt.Rows[0][0].ToString();
                        }
                    }
                    else if (type.ToLower().StartsWith("cash receipt"))
                    {
                        DataTable dt = getref_recpay("REC_RECEIPTVOUCHER_HDR", dgledgerTrns.Rows[i].Cells["VOUCHERNO"].Value.ToString());
                        if (dt.Rows.Count > 0)
                        {
                            dgledgerTrns.Rows[i].Cells["REFERENCE"].Value = dt.Rows[0][0].ToString();
                        }
                    }
                    else if (type.ToLower().StartsWith("cheque transaction"))
                    {
                        
                        DataTable dt = getref_recpay("REC_RECEIPTVOUCHER_HDR", dgledgerTrns.Rows[i].Cells["VOUCHERNO"].Value.ToString());
                        if (dt.Rows.Count > 0)
                        {
                            dgledgerTrns.Rows[i].Cells["REFERENCE"].Value = dt.Rows[0][0].ToString();
                        }
                        else
                        {
                            dt = getref_recpay("PAY_PAYMENT_VOUCHER_HDR", dgledgerTrns.Rows[i].Cells["VOUCHERNO"].Value.ToString());
                            if (dt.Rows.Count > 0)
                            {
                                dgledgerTrns.Rows[i].Cells["REFERENCE"].Value = dt.Rows[0][0].ToString();
                            }
                        }
                    }
                    else if (type.ToLower().StartsWith("journal"))
                    {
                        DataTable dt=getref_recpay("ACCOUNT_VOUCHER_HDR",dgledgerTrns.Rows[i].Cells["VOUCHERNO"].Value.ToString());
                        if (dt.Rows.Count > 0)
                        {
                            dgledgerTrns.Rows[i].Cells["REFERENCE"].Value = dt.Rows[0][0].ToString();
                        }
                        else
                        {
                            dt = getref_recpay("ACCOUNT_VOUCHER_HDR", dgledgerTrns.Rows[i].Cells["VOUCHERNO"].Value.ToString());
                            if (dt.Rows.Count > 0)
                            {
                                dgledgerTrns.Rows[i].Cells["REFERENCE"].Value = dt.Rows[0][0].ToString();
                            }
                        }
                    }
                    else if (type.ToLower().StartsWith("salary"))
                    {
                        DataTable dt = getref_recpay("PAYROLL_VOUCHER_HDR", dgledgerTrns.Rows[i].Cells["VOUCHERNO"].Value.ToString());
                        if (dt.Rows.Count > 0)
                        {
                            dgledgerTrns.Rows[i].Cells["REFERENCE"].Value = dt.Rows[0][0].ToString();
                        }
                        else
                        {
                            dt = getref_recpay("PAYROLL_VOUCHER_HDR", dgledgerTrns.Rows[i].Cells["VOUCHERNO"].Value.ToString());
                            if (dt.Rows.Count > 0)
                            {
                                dgledgerTrns.Rows[i].Cells["REFERENCE"].Value = dt.Rows[0][0].ToString();
                            }
                        }
                    }
                }
            }
        }

        public void refernce_addPTL()
        {
            for (int i = 0; i < dataGridViewPTL.RowCount; i++)
            {
                if (dataGridViewPTL.Rows[i].Cells["VOUCHERTYPES"].Value.ToString() != "" && dataGridViewPTL.Rows[i].Cells["VOUCHERTYPES"].Value != null)
                {
                    string type = dataGridViewPTL.Rows[i].Cells["VOUCHERTYPES"].Value.ToString();
                    if (type.Equals("Purchase") || type.Equals("Purchase Return"))
                    {
                        DataTable dt = getref("INV_PURCHASE_HDR", dataGridViewPTL.Rows[i].Cells["VOUCHERNUM"].Value.ToString());
                        if (dt.Rows.Count > 0)
                        {
                            dataGridViewPTL.Rows[i].Cells["REFERENCES"].Value = dt.Rows[0][0].ToString();
                        }
                    }
                    else if (type.ToLower().StartsWith("sales") || type.ToLower().StartsWith("Sales Return"))
                    {
                        DataTable dt = getref("INV_SALES_HDR", dataGridViewPTL.Rows[i].Cells["VOUCHERNUM"].Value.ToString());
                        if (dt.Rows.Count > 0)
                        {
                            dataGridViewPTL.Rows[i].Cells["REFERENCES"].Value = dt.Rows[0][0].ToString();
                        }
                    }
                    else if (type.ToLower().StartsWith("cash payment"))
                    {
                        DataTable dt = getref_recpay("PAY_PAYMENT_VOUCHER_HDR", dataGridViewPTL.Rows[i].Cells["VOUCHERNUM"].Value.ToString());
                        if (dt.Rows.Count > 0)
                        {
                            dataGridViewPTL.Rows[i].Cells["REFERENCES"].Value = dt.Rows[0][0].ToString();
                        }
                    }
                    else if (type.ToLower().StartsWith("cash receipt"))
                    {
                        DataTable dt = getref_recpay("REC_RECEIPTVOUCHER_HDR", dataGridViewPTL.Rows[i].Cells["VOUCHERNUM"].Value.ToString());
                        if (dt.Rows.Count > 0)
                        {
                            dataGridViewPTL.Rows[i].Cells["REFERENCES"].Value = dt.Rows[0][0].ToString();
                        }
                    }
                    else if (type.ToLower().StartsWith("cheque transaction"))
                    {

                        DataTable dt = getref_recpay("REC_RECEIPTVOUCHER_HDR", dataGridViewPTL.Rows[i].Cells["VOUCHERNUM"].Value.ToString());
                        if (dt.Rows.Count > 0)
                        {
                            dataGridViewPTL.Rows[i].Cells["REFERENCES"].Value = dt.Rows[0][0].ToString();
                        }
                        else
                        {
                            dt = getref_recpay("PAY_PAYMENT_VOUCHER_HDR", dataGridViewPTL.Rows[i].Cells["VOUCHERNUM"].Value.ToString());
                            if (dt.Rows.Count > 0)
                            {
                                dataGridViewPTL.Rows[i].Cells["REFERENCES"].Value = dt.Rows[0][0].ToString();
                            }
                        }
                    }
                    else if (type.ToLower().StartsWith("journal"))
                    {
                        DataTable dt = getref_recpay("ACCOUNT_VOUCHER_HDR", dataGridViewPTL.Rows[i].Cells["VOUCHERNUM"].Value.ToString());
                        if (dt.Rows.Count > 0)
                        {
                            dataGridViewPTL.Rows[i].Cells["REFERENCES"].Value = dt.Rows[0][0].ToString();
                        }
                        else
                        {
                            dt = getref_recpay("ACCOUNT_VOUCHER_HDR", dataGridViewPTL.Rows[i].Cells["VOUCHERNUM"].Value.ToString());
                            if (dt.Rows.Count > 0)
                            {
                                dataGridViewPTL.Rows[i].Cells["REFERENCES"].Value = dt.Rows[0][0].ToString();
                            }
                        }
                    }
                    else if (type.ToLower().StartsWith("salary"))
                    {
                        DataTable dt = getref_recpay("PAYROLL_VOUCHER_HDR", dataGridViewPTL.Rows[i].Cells["VOUCHERNUM"].Value.ToString());
                        if (dt.Rows.Count > 0)
                        {
                            dataGridViewPTL.Rows[i].Cells["REFERENCES"].Value = dt.Rows[0][0].ToString();
                        }
                        else
                        {
                            dt = getref_recpay("PAYROLL_VOUCHER_HDR", dataGridViewPTL.Rows[i].Cells["VOUCHERNUM"].Value.ToString());
                            if (dt.Rows.Count > 0)
                            {
                                dataGridViewPTL.Rows[i].Cells["REFERENCES"].Value = dt.Rows[0][0].ToString();
                            }
                        }
                    }
                }
            }
        }
        //private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        public DataTable getref(string tbl,string DOC_NO)
        {
           // conn.Open();
            //DataTable dt = new DataTable();
            //SqlCommand cmd = new SqlCommand("SELECT DOC_ID FROM " + tbl + " WHERE DOC_NO='" + DOC_NO + "'", conn);
            //SqlDataAdapter adptr = new SqlDataAdapter(cmd);
            //adptr.Fill(dt);
            //conn.Close();
            ldgObj.LedgerId = DOC_NO;
            return ldgObj.getDocId(tbl);
            ;
        }
        public DataTable getref_recpay(string tbl, string DOC_NO)
        {
           // conn.Open();
            //DataTable dt = new DataTable();
            //SqlCommand cmd = new SqlCommand("SELECT REC_NO FROM " + tbl + " WHERE DOC_NO='" + DOC_NO + "'", conn);
            //SqlDataAdapter adptr = new SqlDataAdapter(cmd);
            //adptr.Fill(dt);
            //conn.Close();
            ldgObj.LedgerId = DOC_NO;
            return ldgObj.getRecNo(tbl);;
        }
        private void dgledgerTrns_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {          
                if (dgledgerTrns.Rows.Count > 0 && dgledgerTrns.CurrentRow != null)
                {
                    c = dgledgerTrns.CurrentRow.Cells;
                    String voucher_type = c["VOUCHERTYPE"].Value.ToString();
                    if (voucher_type.Equals("Purchase"))
                    {
                        PurchaseMaster pur = new PurchaseMaster(c["VOUCHERNO"].Value.ToString());
                        pur.Show();
                    }
                    else if (voucher_type.Equals("Purchase Return"))
                    {
                        Purchase_Return purret = new Purchase_Return(c["VOUCHERNO"].Value.ToString());
                        purret.Show();
                    }
                    else if (voucher_type.Equals("Sales Return"))
                    {
                        Sales_Return ret = new Sales_Return(c["VOUCHERNO"].Value.ToString());
                        ret.Show();
                    }
                    else if (voucher_type.ToLower().StartsWith("sales"))
                    {
                        SalesQ sal = new SalesQ(c["VOUCHERNO"].Value.ToString());
                            Accounts.LedgerReport.checkLedger = true;
                            checkLedger = true;
                            sal.Show();
                            Accounts.LedgerReport.checkLedger = false;
                    }
                    else if (voucher_type.ToLower().StartsWith("cash payment"))
                    {
                        PaymentVoucher2 Paymt = new PaymentVoucher2(0, c["VOUCHERNO"].Value.ToString());
                        Paymt.Show();
                    }
                    else if (voucher_type.ToLower().StartsWith("cash receipt"))
                    {
                        PaymentVoucher2 Paymt2 = new PaymentVoucher2(1, c["VOUCHERNO"].Value.ToString());
                        Paymt2.Show();
                    }
                    else if (voucher_type.ToLower().StartsWith("journal"))
                    {
                        Accounting_Voucher acv = new Accounting_Voucher(1, c["VOUCHERNO"].Value.ToString());
                        acv.Show();
                    }
                    else if (voucher_type.ToLower().StartsWith("salary"))
                    {
                        Payrollvouch payrol = new Payrollvouch(c["VOUCHERNO"].Value.ToString());
                        payrol.Show();
                    }
                    /*switch (voucher_type)
                    {
                        case "Purchase":
                            PurchaseMaster pur = new PurchaseMaster(c["VOUCHERNO"].Value.ToString());
                            pur.Show();
                            break;
                        case "Purchase Return":
                            PurchaseMaster purret = new PurchaseMaster(c["VOUCHERNO"].Value.ToString());
                            purret.Show();
                            break;
                        case "SALES Normal":
                            SalesQ sal = new SalesQ(c["VOUCHERNO"].Value.ToString());
                            Accounts.LedgerReport.checkLedger = true;
                            checkLedger = true;
                            sal.Show();
                            Accounts.LedgerReport.checkLedger = false;
                            break;
                        case "Cash Payment":
                            PaymentVoucher2 Paymt = new PaymentVoucher2(0, c["VOUCHERNO"].Value.ToString());

                            Paymt.Show();
                            break;

                        case "Cash Receipt":
                            PaymentVoucher2 Paymt2 = new PaymentVoucher2(1, c["VOUCHERNO"].Value.ToString());

                            Paymt2.Show();
                            break;
                        case "Sales Return":
                            Sales_Return ret = new Sales_Return(c["VOUCHERNO"].Value.ToString());

                            ret.Show();
                            break;

                    }*/
                }
            
            
            
        }
       
        private void dgledgerTrns_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dgledgerTrns_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
        


        }

        private void dgledgerTrns_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            int index = e.RowIndex;
            dgledgerTrns.Rows[index].Selected = true;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();


                // creating new WorkBook within Excel application
                Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);


                // creating new Excelsheet in workbook
                Microsoft.Office.Interop.Excel._Worksheet worksheet = null;

                // see the excel sheet behind the program
                app.Visible = true;

                // get the reference of first sheet. By default its name is Sheet1.
                // store its reference to worksheet
                worksheet = workbook.Sheets["Sheet1"];
                worksheet = workbook.ActiveSheet;

                // changing the name of active sheet
                worksheet.Name = "Exported from gridview";


                // storing header part in Excel
                for (int i = 1; i <= (dgledgerTrns.Columns.Count); i++)
                {
                    worksheet.Cells[1, i] = dgledgerTrns.Columns[i - 1].HeaderText;
                }



                // storing Each row and column value to excel sheet
                for (int i = 0; i < dgledgerTrns.Rows.Count - 1; i++)
                {
                    for (int j = 0; j <= (dgledgerTrns.Columns.Count-1); j++)
                    {
                        if (dgledgerTrns.Rows[i].Cells[j].Value != "" && dgledgerTrns.Rows[i].Cells[j].Value!=null)
                        worksheet.Cells[i + 2, j + 1] = dgledgerTrns.Rows[i].Cells[j].Value.ToString();
                    }
                }


                // save the application
                workbook.SaveAs("c:\\output.xls", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                // Exit from the application
                app.Quit();

            }
            catch(Exception ex)
            {
                string st = ex.Message;
            }
        }

        private void dgledgerTrns_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            foreach (DataGridViewCell cell in dgledgerTrns.Rows[e.RowIndex].Cells)
            {
                if (cell.GetType() == typeof(DataGridViewImageCell))
                {
                    cell.Value = DBNull.Value;
                }
            }
        }


        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            printingReport();
        }
        public void printingReport()
        {
            PrintDialog printdialog = new PrintDialog();
            PrintDocument printDocument = new PrintDocument();
            printDocument.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Form8", 840, 1160);
            printDocument.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Form8", 840, 1160);
            Company cmp = Common.getCompany();
            if (Regex.IsMatch(cmp.Name, "PTL"))
            {
                printDocument.PrintPage += printDocumentA4Form8_PrintPage_CustomizedForPtlCrusher;
                printDocument.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Form8", 840, 1188);
            }
            else if (Regex.IsMatch(cmp.Name, "PRESTIGE"))
                printDocument.PrintPage += printDocumentA4Form8_PrintPage_Customized;
            else
            {
                printDocument.PrintPage += printDocumentA4Form8_PrintPage;
                printDocument.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Form8", 840, 1188);
            }
            printeditems = 0;
            printdialog.Document = printDocument;
            printDocument.Print();

        }
       
        int m = 0;
        bool PAGETOTAL = false;
        public int printeditems = 0;
       
        string name = "";
        string under = "";
      /*  void printDocumentA4Form8_PrintPage(object sender, PrintPageEventArgs e)
        {
            Pen blackpen = new Pen(System.Drawing.Color.Black, 1);
            System.Drawing.Font Headerfont2 = new System.Drawing.Font("Times New Roman", 14, FontStyle.Bold);
            System.Drawing.Font Headerfont1 = new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold);
            System.Drawing.Font Headerfont3 = new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular);
            System.Drawing.Font Headerfont4 = new System.Drawing.Font("Times New Roman", 10, FontStyle.Bold);
            m = 0;
            bool hasmorepages = false;
            float xpos;
            int startx = 50;
            int starty = 20;
            int offset = 15;
            bool PRINTTOTALPAGE = true;
            int w = e.MarginBounds.Width / 2;
            int x = e.MarginBounds.Left;
            int y = e.MarginBounds.Top;
            string vchno = "";
            string snno = "";
            string dated = "";
            string vchtype = "";
            string partclrs = "";
            string debit = "";
            string credit = "";
            string balance = "";
            string narration = "";
            int value = 0;
            var tabDataForeColor = System.Drawing.Color.Black;
            int height = 100 + y;
            Pen blackPen1 = new Pen(System.Drawing.Color.Black, 1);
            var txtDataWidth = e.Graphics.MeasureString(CompanyName, Headerfont1).Width;
            double pricWtax = 0;
            decimal a = 0;
            StringFormat format = new StringFormat(StringFormatFlags.DirectionRightToLeft);
            using (var sf = new StringFormat())
            {
                height += 15;
                sf.LineAlignment = StringAlignment.Center;
                sf.Alignment = StringAlignment.Center;
                xpos = e.MarginBounds.Left + (e.MarginBounds.Width / 2);
                e.Graphics.DrawRectangle(blackpen, 10, 10, 820, 1000);
                e.Graphics.DrawLine(blackpen, 10, 80, 830, 80); //h
                e.Graphics.DrawLine(blackpen, 10, 110, 830, 110); //h
                e.Graphics.DrawLine(blackpen, 10, 1090, 830, 1090); //h
                e.Graphics.DrawLine(blackpen, 10, 1130, 830, 1130); //h
                e.Graphics.DrawLine(blackpen, 35, 80, 35, 1090); //v sl no
                e.Graphics.DrawLine(blackpen, 120, 80, 120, 1090); //voucher
                e.Graphics.DrawLine(blackpen, 188, 80, 188, 1090); //date
                e.Graphics.DrawLine(blackpen, 290, 80, 290, 1090); //vouchrtype
                e.Graphics.DrawLine(blackpen, 460, 80, 460, 1130); //particulars
                e.Graphics.DrawLine(blackpen, 540, 80, 540, 1130); //debit     
                e.Graphics.DrawLine(blackpen, 625, 80, 625, 1130); //CREDIT
                e.Graphics.DrawLine(blackpen, 720, 80, 720, 1130); //balance             
                string date = "Ledger/Statement of Account for the period From " + Date_From.Value.ToShortDateString() + " To " + Date_To.Value.ToShortDateString();
                e.Graphics.DrawString(date, Headerfont3, new SolidBrush(System.Drawing.Color.Black), 180, 15);
                e.Graphics.DrawString("ACCOUNT : " + drpLedger.Text, Headerfont4, new SolidBrush(System.Drawing.Color.Black), 280, 35);

                if (name == drpLedger.Text && under == "14")
                {
                    e.Graphics.DrawString("SUNDRY DEBTORS", Headerfont4, new SolidBrush(System.Drawing.Color.Black), 360, 55);
                }

                if (name == drpLedger.Text && under == "13")
                {
                    e.Graphics.DrawString("SUNDRY CREDITORS", Headerfont4, new SolidBrush(System.Drawing.Color.Black), 360, 55);
                }

                string headtext = "No".PadRight(3) + "VoucherNo".PadRight(13) + "Date".PadRight(9) + "VoucherType".PadRight(20) + "Particulars".PadRight(28) + "Debit".PadRight(15) + "Credit".PadRight(15) + "Balance".PadRight(16) + "Narration";
                e.Graphics.DrawString(headtext, Headerfont1, new SolidBrush(System.Drawing.Color.Black), startx - 40, starty + offset + 50);
                offset = offset + 40;
                float fontheight = Headerfont1.GetHeight();
                try
                {
                    int i = 0;
                    int j = 1;
                    int nooflines = 0;
                    //foreach (DataGridViewRow row in dgledgerTrns.Rows)
                    for (int k = 0; k < dgledgerTrns.Rows.Count - 5; k++)
                    {
                        PRINTTOTALPAGE = false;
                        if (j > printeditems)
                        {
                            if (nooflines < 35)
                            {

                                snno = dgledgerTrns.Rows[k].Cells["sno"].Value.ToString();
                                e.Graphics.DrawString(snno.ToString(), Headerfont3, new SolidBrush(System.Drawing.Color.Black), startx - 40, starty + offset + 40);

                                vchno = dgledgerTrns.Rows[k].Cells["VOUCHERNO"].Value.ToString();
                                e.Graphics.DrawString(vchno.ToString(), Headerfont3, new SolidBrush(System.Drawing.Color.Black), startx - 15, starty + offset + 40);

                                dated = dgledgerTrns.Rows[k].Cells["DATED"].Value.ToString();
                                e.Graphics.DrawString(dated.ToString(), Headerfont3, new SolidBrush(System.Drawing.Color.Black), startx + 70, starty + offset + 40);

                                vchtype = dgledgerTrns.Rows[k].Cells["VOUCHERTYPE"].Value.ToString();
                                e.Graphics.DrawString(vchtype.ToString(), Headerfont3, new SolidBrush(System.Drawing.Color.Black), startx + 138, starty + offset + 40);

                                partclrs = dgledgerTrns.Rows[k].Cells["PARTICULARS"].Value.ToString();
                                e.Graphics.DrawString(partclrs.ToString(), Headerfont3, new SolidBrush(System.Drawing.Color.Black), startx + 237, starty + offset + 40);

                                debit = dgledgerTrns.Rows[k].Cells["DEBIT"].Value.ToString();
                                e.Graphics.DrawString(debit.ToString(), Headerfont3, new SolidBrush(System.Drawing.Color.Black), startx + 488, starty + offset + 40, format);

                                credit = dgledgerTrns.Rows[k].Cells["CREDIT"].Value.ToString();
                                e.Graphics.DrawString(credit.ToString(), Headerfont3, new SolidBrush(System.Drawing.Color.Black), startx + 573, starty + offset + 40, format);

                                balance = dgledgerTrns.Rows[k].Cells["BALANCES"].Value.ToString();
                                string bal = balance.Remove(balance.Length - 3);
                                e.Graphics.DrawString(bal.ToString(), Headerfont3, new SolidBrush(System.Drawing.Color.Black), startx + 670, starty + offset + 40, format);

                                narration = dgledgerTrns.Rows[k].Cells["NARRATION"].Value.ToString();
                                e.Graphics.DrawString(narration.ToString(), Headerfont3, new SolidBrush(System.Drawing.Color.Black), startx + 670, starty + offset + 40);

                                offset = offset + (int)fontheight + 10;
                                value = k;
                                nooflines++;
                                j++;

                            }
                            else
                            {
                                printeditems = j - 1;
                                hasmorepages = true;
                                PRINTTOTALPAGE = true;
                            }
                            if (hasmorepages == true)
                            {
                                e.Graphics.DrawString("coutinue...", Headerfont1, new SolidBrush(System.Drawing.Color.Black), startx + 40, 1140);
                            }
                        }
                        else
                        {
                            j++;
                            m++;
                        }
                    }
                }

                catch (Exception exc)
                {
                    string c = exc.Message;
                }
            }
            float newoffset = 900;
            if (!PRINTTOTALPAGE)
            {
                PAGETOTAL = true;
                if (PAGETOTAL)
                {
                    try
                    {
                        string ldblnce = dgledgerTrns.Rows[value + 2].Cells["VOUCHERTYPE"].Value.ToString();
                        e.Graphics.DrawString(ldblnce.ToString(), Headerfont1, new SolidBrush(System.Drawing.Color.Black), startx + 145, 1110);

                        string dbtotal = dgledgerTrns.Rows[value + 2].Cells["DEBIT"].Value.ToString();
                        e.Graphics.DrawString(dbtotal.ToString(), Headerfont1, new SolidBrush(System.Drawing.Color.Black), startx + 493, 1110, format);

                        string cdtotal = dgledgerTrns.Rows[value + 2].Cells["CREDIT"].Value.ToString();
                        e.Graphics.DrawString(cdtotal.ToString(), Headerfont1, new SolidBrush(System.Drawing.Color.Black), startx + 578, 1110, format);

                        string bltotal = dgledgerTrns.Rows[value + 2].Cells["BALANCES"].Value.ToString();
                        string BL = bltotal.Remove(bltotal.Length - 3);
                        e.Graphics.DrawString(BL.ToString(), Headerfont1, new SolidBrush(System.Drawing.Color.Black), startx + 670, 1110, format);

                        string opblnce = dgledgerTrns.Rows[value + 4].Cells["PARTICULARS"].Value.ToString();
                        e.Graphics.DrawString(opblnce.ToString(), Headerfont1, new SolidBrush(System.Drawing.Color.Black), startx + 258, 930);

                        string opblnceamt = dgledgerTrns.Rows[value + 4].Cells["DEBIT"].Value.ToString();
                        string op = opblnceamt.Remove(opblnceamt.Length - 3);
                        e.Graphics.DrawString(op.ToString() + "Dr", Headerfont1, new SolidBrush(System.Drawing.Color.Black), startx + 488, 930, format);

                        string clbalance = dgledgerTrns.Rows[value + 5].Cells["PARTICULARS"].Value.ToString();
                        e.Graphics.DrawString(clbalance.ToString() + " : ", Headerfont2, new SolidBrush(System.Drawing.Color.Black), startx + 400, 1140);

                        string clbalanceamt = dgledgerTrns.Rows[value + 5].Cells["DEBIT"].Value.ToString();
                        string cl = clbalanceamt.Remove(clbalanceamt.Length - 3);
                        e.Graphics.DrawString(cl.ToString() + "Dr", Headerfont2, new SolidBrush(System.Drawing.Color.Black), startx + 725, 1140, format);
                        newoffset = newoffset + 20;
                        newoffset = newoffset + 20;
                        newoffset = newoffset + 20;
                    }
                    catch
                    {
                    }
                }
                PAGETOTAL = false;
            }
            e.HasMorePages = hasmorepages;
        }*/

        void printDocumentA4Form8_PrintPage(object sender, PrintPageEventArgs e)
        {
            Pen blackpen = new Pen(System.Drawing.Color.Black, 1);
            System.Drawing.Font Headerfont2 = new System.Drawing.Font("Times New Roman", 14, FontStyle.Bold);//Times New Roman
            System.Drawing.Font Headerfont1 = new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold);
            System.Drawing.Font Headerfont3 = new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular);
            System.Drawing.Font Headerfont4 = new System.Drawing.Font("Times New Roman", 10, FontStyle.Bold);
            m = 0;
            bool hasmorepages = false;
            float xpos;
            int startx = 50;
            int starty = 20;
            int offset = 15;
            bool PRINTTOTALPAGE = true;
            int w = e.MarginBounds.Width / 2;
            int x = e.MarginBounds.Left;
            int y = e.MarginBounds.Top;
            string vchno = "";
            string snno = "";
            string dated = "";
            string vchtype = "";
            string partclrs = "";
            string debit = "";
            string credit = "";
            string balance = "";
            string narration = "";
            int value = 0;
            var tabDataForeColor = System.Drawing.Color.Black;
            int height = 100 + y;
            Pen blackPen1 = new Pen(System.Drawing.Color.Black, 1);
            var txtDataWidth = e.Graphics.MeasureString(CompanyName, Headerfont1).Width;
            double pricWtax = 0;
            decimal a = 0;
            StringFormat format = new StringFormat(StringFormatFlags.DirectionRightToLeft);
            using (var sf = new StringFormat())
            {
                height += 15;
                sf.LineAlignment = StringAlignment.Center;
                sf.Alignment = StringAlignment.Center;
                xpos = e.MarginBounds.Left + (e.MarginBounds.Width / 2);
                e.Graphics.DrawRectangle(blackpen, 10, 10, 820, 1168);
                e.Graphics.DrawLine(blackpen, 10, 80, 830, 80); //h
                e.Graphics.DrawLine(blackpen, 10, 110, 830, 110); //h
                e.Graphics.DrawLine(blackpen, 10, 1090, 830, 1090); //h
                e.Graphics.DrawLine(blackpen, 10, 1130, 830, 1130); //h
                e.Graphics.DrawLine(blackpen, 35, 80, 35, 1090); //v sl no
                e.Graphics.DrawLine(blackpen, 120, 80, 120, 1090); //voucher
                e.Graphics.DrawLine(blackpen, 188, 80, 188, 1090); //date
                e.Graphics.DrawLine(blackpen, 290, 80, 290, 1090); //vouchrtype
                e.Graphics.DrawLine(blackpen, 460, 80, 460, 1130); //particulars
                e.Graphics.DrawLine(blackpen, 540, 80, 540, 1130); //debit     
                e.Graphics.DrawLine(blackpen, 625, 80, 625, 1130); //CREDIT
                e.Graphics.DrawLine(blackpen, 720, 80, 720, 1130); //balance             
                string date = "Ledger/Statement of Account for the period From " + Date_From.Value.ToShortDateString() + " To " + Date_To.Value.ToShortDateString();
                e.Graphics.DrawString(date, Headerfont3, new SolidBrush(System.Drawing.Color.Black), 180, 15);
                e.Graphics.DrawString("ACCOUNT : " + drpLedger.Text, Headerfont4, new SolidBrush(System.Drawing.Color.Black), 280, 35);

                if (name == drpLedger.Text && under == "14")
                {
                    e.Graphics.DrawString("SUNDRY DEBTORS", Headerfont4, new SolidBrush(System.Drawing.Color.Black), 360, 55);
                }

                if (name == drpLedger.Text && under == "13")
                {
                    e.Graphics.DrawString("SUNDRY CREDITORS", Headerfont4, new SolidBrush(System.Drawing.Color.Black), 360, 55);
                }

                string headtext = "No".PadRight(3) + "VoucherNo".PadRight(13) + "Date".PadRight(9) + "VoucherType".PadRight(20) + "Particulars".PadRight(28) + "Debit".PadRight(15) + "Credit".PadRight(15) + "Balance".PadRight(16) + "Narration";
                e.Graphics.DrawString(headtext, Headerfont1, new SolidBrush(System.Drawing.Color.Black), startx - 40, starty + offset + 50);
                offset = offset + 40;
                float fontheight = Headerfont1.GetHeight();
                try
                {
                    int i = 0;
                    int j = 1;
                    int nooflines = 0;
                    //foreach (DataGridViewRow row in dgledgerTrns.Rows)
                    for (int k = 0; k < dgledgerTrns.Rows.Count - 5; k++)
                    {
                        PRINTTOTALPAGE = false;
                        if (j > printeditems)
                        {
                            if (nooflines < 35)
                            {

                                snno = dgledgerTrns.Rows[k].Cells["sno"].Value.ToString();
                                e.Graphics.DrawString(snno.ToString(), Headerfont3, new SolidBrush(System.Drawing.Color.Black), startx - 40, starty + offset + 40);

                                vchno = dgledgerTrns.Rows[k].Cells["VOUCHERNO"].Value.ToString();
                                e.Graphics.DrawString(vchno.ToString(), Headerfont3, new SolidBrush(System.Drawing.Color.Black), startx - 15, starty + offset + 40);

                                dated = dgledgerTrns.Rows[k].Cells["DATED"].Value.ToString();
                                e.Graphics.DrawString(dated.ToString(), Headerfont3, new SolidBrush(System.Drawing.Color.Black), startx + 70, starty + offset + 40);

                                vchtype = dgledgerTrns.Rows[k].Cells["VOUCHERTYPE"].Value.ToString();
                                e.Graphics.DrawString(vchtype.ToString(), Headerfont3, new SolidBrush(System.Drawing.Color.Black), startx + 138, starty + offset + 40);

                                partclrs = dgledgerTrns.Rows[k].Cells["PARTICULARS"].Value.ToString();
                                e.Graphics.DrawString(partclrs.ToString(), Headerfont3, new SolidBrush(System.Drawing.Color.Black), startx + 237, starty + offset + 40);

                                debit = dgledgerTrns.Rows[k].Cells["DEBIT"].Value.ToString();
                                e.Graphics.DrawString(debit.ToString(), Headerfont3, new SolidBrush(System.Drawing.Color.Black), startx + 488, starty + offset + 40, format);

                                credit = dgledgerTrns.Rows[k].Cells["CREDIT"].Value.ToString();
                                e.Graphics.DrawString(credit.ToString(), Headerfont3, new SolidBrush(System.Drawing.Color.Black), startx + 573, starty + offset + 40, format);

                                balance = dgledgerTrns.Rows[k].Cells["BALANCES"].Value.ToString();
                                string bal = balance.Remove(balance.Length - 3);
                                e.Graphics.DrawString(bal.ToString(), Headerfont3, new SolidBrush(System.Drawing.Color.Black), startx + 670, starty + offset + 40, format);

                                narration = dgledgerTrns.Rows[k].Cells["NARRATION"].Value.ToString();
                                e.Graphics.DrawString(narration.ToString(), Headerfont3, new SolidBrush(System.Drawing.Color.Black), startx + 670, starty + offset + 40);

                                offset = offset + (int)fontheight + 10;
                                value = k;
                                nooflines++;
                                j++;

                            }
                            else
                            {
                                printeditems = j - 1;
                                hasmorepages = true;
                                PRINTTOTALPAGE = true;
                            }
                            if (hasmorepages == true)
                            {
                                e.Graphics.DrawString("continue...", Headerfont1, new SolidBrush(System.Drawing.Color.Black), startx + 40, 1140);
                            }
                        }
                        else
                        {
                            j++;
                            m++;
                        }
                    }
                }

                catch (Exception exc)
                {
                    string c = exc.Message;
                }
            }
            float newoffset = 900;
            if (!PRINTTOTALPAGE)
            {
                PAGETOTAL = true;
                if (PAGETOTAL)
                {
                    try
                    {
                        string ldblnce = dgledgerTrns.Rows[value + 2].Cells["VOUCHERTYPE"].Value.ToString();
                        e.Graphics.DrawString(ldblnce.ToString(), Headerfont1, new SolidBrush(System.Drawing.Color.Black), startx + 145, 1110);

                        string dbtotal = dgledgerTrns.Rows[value + 2].Cells["DEBIT"].Value.ToString();
                        e.Graphics.DrawString(dbtotal.ToString(), Headerfont1, new SolidBrush(System.Drawing.Color.Black), startx + 493, 1110, format);

                        string cdtotal = dgledgerTrns.Rows[value + 2].Cells["CREDIT"].Value.ToString();
                        e.Graphics.DrawString(cdtotal.ToString(), Headerfont1, new SolidBrush(System.Drawing.Color.Black), startx + 578, 1110, format);

                        string bltotal = dgledgerTrns.Rows[value + 2].Cells["BALANCES"].Value.ToString();
                        string BL = bltotal.Remove(bltotal.Length - 3);
                        e.Graphics.DrawString(BL.ToString(), Headerfont1, new SolidBrush(System.Drawing.Color.Black), startx + 670, 1110, format);

                        //string opblnce = dgledgerTrns.Rows[value + 4].Cells["PARTICULARS"].Value.ToString();
                        //e.Graphics.DrawString(opblnce.ToString(), Headerfont1, new SolidBrush(System.Drawing.Color.Black), startx + 258, 930);

                        //string opblnceamt = dgledgerTrns.Rows[value + 4].Cells["DEBIT"].Value.ToString();
                        //string op = opblnceamt.Remove(opblnceamt.Length - 3);
                        //e.Graphics.DrawString(op.ToString() + "Dr", Headerfont1, new SolidBrush(System.Drawing.Color.Black), startx + 488, 930, format);

                        string clbalance = dgledgerTrns.Rows[value + 5].Cells["PARTICULARS"].Value.ToString();
                        e.Graphics.DrawString(clbalance.ToString() + " : ", Headerfont2, new SolidBrush(System.Drawing.Color.Black), startx + 400, 1140);

                        string clbalanceamt = dgledgerTrns.Rows[value + 5].Cells["DEBIT"].Value.ToString();
                        string cl = clbalanceamt.Remove(clbalanceamt.Length - 3);
                        e.Graphics.DrawString(cl.ToString() + "Dr", Headerfont2, new SolidBrush(System.Drawing.Color.Black), startx + 725, 1140, format);
                        newoffset = newoffset + 20;
                        newoffset = newoffset + 20;
                        newoffset = newoffset + 20;
                    }
                    catch
                    {
                    }
                }
                PAGETOTAL = false;
            }
            e.HasMorePages = hasmorepages;
        }

        void printDocumentA4Form8_PrintPage_Customized(object sender, PrintPageEventArgs e)
        {
            Company company = Common.getCompany();
            Pen blackpen = new Pen(System.Drawing.Color.Black, 1);
            System.Drawing.Font Headerfont2 = new System.Drawing.Font("Times New Roman", 14, FontStyle.Bold);
            System.Drawing.Font Headerfont1 = new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold);
            System.Drawing.Font Headerfont3 = new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular);
            System.Drawing.Font Headerfont4 = new System.Drawing.Font("Times New Roman", 10, FontStyle.Bold);
            m = 0;
            bool hasmorepages = false;
            float xpos;
            int startx = 50;
            int starty = 20;
            int offset = 15;
            bool PRINTTOTALPAGE = true;
            int w = e.MarginBounds.Width / 2;
            int x = e.MarginBounds.Left;
            int y = e.MarginBounds.Top;
            string vchno = "";
            string snno = "";
            string dated = "";
            string vchtype = "";
            string partclrs = "";
            string debit = "";
            string credit = "";
            string balance = "";
            string narration = "";
            int value = 0;
            var tabDataForeColor = System.Drawing.Color.Black;
            int height = 100 + y;
            Pen blackPen1 = new Pen(System.Drawing.Color.Black, 1);
            var txtDataWidth = e.Graphics.MeasureString(CompanyName, Headerfont1).Width;
            double pricWtax = 0;
            decimal a = 0;
            StringFormat format = new StringFormat(StringFormatFlags.DirectionRightToLeft);
            using (var sf = new StringFormat())
            {
                height += 15;
                sf.LineAlignment = StringAlignment.Center;
                sf.Alignment = StringAlignment.Center;
                xpos = e.MarginBounds.Left + (e.MarginBounds.Width / 2);
                e.Graphics.DrawRectangle(blackpen, 10, 10, 800, 1050);
                e.Graphics.DrawLine(blackpen, 10, 80, 810, 80); //h
                e.Graphics.DrawLine(blackpen, 10, 110, 810, 110); //h
             
               // e.Graphics.DrawLine(blackpen, 35, 80, 35, 1090); //v sl no
               //e.Graphics.DrawLine(blackpen, 120, 80, 120, 1090); //voucher
               // e.Graphics.DrawLine(blackpen, 188, 80, 188, 1090); //date
               // e.Graphics.DrawLine(blackpen, 290, 80, 290, 1090); //vouchrtype
               // e.Graphics.DrawLine(blackpen, 460, 80, 460, 1130); //particulars
               // e.Graphics.DrawLine(blackpen, 540, 80, 540, 1130); //debit     
               // e.Graphics.DrawLine(blackpen, 625, 80, 625, 1130); //CREDIT
               // e.Graphics.DrawLine(blackpen, 720, 80, 720, 1130); //balance    
                string date = "Ledger/Statement of Account for the period From " + Date_From.Value.ToShortDateString() + " To " + Date_To.Value.ToShortDateString();
                int centerOfPage = e.PageBounds.Width / 2;
                int nameStartPosision = centerOfPage - TextRenderer.MeasureText(company.Name, Headerfont4).Width / 2;
                int nameStartPosision0 = centerOfPage - TextRenderer.MeasureText(date, Headerfont3).Width / 2;
                int nameStartPosision1 = centerOfPage - TextRenderer.MeasureText("ACCOUNT : " + drpLedger.Text, Headerfont4).Width / 2;
             
                e.Graphics.DrawString(company.Name, Headerfont4, new SolidBrush(System.Drawing.Color.Black), nameStartPosision, 15);
                e.Graphics.DrawString(date, Headerfont3, new SolidBrush(System.Drawing.Color.Black), nameStartPosision0, 30);
                e.Graphics.DrawString("ACCOUNT : " + drpLedger.Text, Headerfont4, new SolidBrush(System.Drawing.Color.Black), nameStartPosision1, 45);

                if (name == drpLedger.Text && under == "14")
                {
                    e.Graphics.DrawString("SUNDRY DEBTORS", Headerfont4, new SolidBrush(System.Drawing.Color.Black), 360, 55);
                }

                if (name == drpLedger.Text && under == "13")
                {
                    e.Graphics.DrawString("SUNDRY CREDITORS", Headerfont4, new SolidBrush(System.Drawing.Color.Black), 360, 55);
                }

                string headtext = "No".PadRight(5) + "Date".PadRight(17) + "Particulars".PadRight(48) + "VoucherType".PadRight(20) + "VoucherNo".PadRight(20) + "Debit".PadRight(17) + "Credit";
                e.Graphics.DrawString(headtext, Headerfont1, new SolidBrush(System.Drawing.Color.Black), startx - 40, starty + offset + 50);
                offset = offset + 40;
                float fontheight = Headerfont1.GetHeight();
                try
                {
                    int i = 0;
                    int j = 1;
                    int nooflines = 0;
                    //foreach (DataGridViewRow row in dgledgerTrns.Rows)
                    for (int k = 0; k < dgledgerTrns.Rows.Count - 5; k++)
                    {
                        PRINTTOTALPAGE = false;
                        if (j > printeditems)
                        {
                            if (nooflines < 32)
                            {
                                string typ = "";
                                snno = dgledgerTrns.Rows[k].Cells["sno"].Value.ToString();
                                dated = dgledgerTrns.Rows[k].Cells["DATED"].Value.ToString();
                                vchno = dgledgerTrns.Rows[k].Cells["VOUCHERNO"].Value.ToString();
                                vchtype = dgledgerTrns.Rows[k].Cells["VOUCHERTYPE"].Value.ToString();
                                partclrs = dgledgerTrns.Rows[k].Cells["PARTICULARS"].Value.ToString();
                                debit = dgledgerTrns.Rows[k].Cells["DEBIT"].Value.ToString();
                                credit = dgledgerTrns.Rows[k].Cells["CREDIT"].Value.ToString();
                                balance = dgledgerTrns.Rows[k].Cells["BALANCES"].Value.ToString();
                                string bal = balance.Remove(balance.Length - 3);
                                narration = dgledgerTrns.Rows[k].Cells["NARRATION"].Value.ToString();
                                if (dated != "")
                                    typ = debit == "" ? "Dr" : "Cr";
                                else
                                {
                                   
                                    typ = "";
                                    partclrs = "Opening Balance";
                                }

                                e.Graphics.DrawString(snno.ToString(), Headerfont3, new SolidBrush(System.Drawing.Color.Black), startx - 40, starty + offset + 40);
                                e.Graphics.DrawString(dated+"  "+typ, Headerfont3, new SolidBrush(System.Drawing.Color.Black), startx - 15, starty + offset + 40);
                                e.Graphics.DrawString(partclrs.ToString(), Headerfont3, new SolidBrush(System.Drawing.Color.Black), startx + 85, starty + offset + 40);
                                e.Graphics.DrawString(vchtype.ToString(), Headerfont3, new SolidBrush(System.Drawing.Color.Black), startx + 328, starty + offset + 40);
                                e.Graphics.DrawString(vchno.ToString(), Headerfont3, new SolidBrush(System.Drawing.Color.Black), startx + 462, starty + offset + 40);
                                e.Graphics.DrawString(debit.ToString(), Headerfont3, new SolidBrush(System.Drawing.Color.Black), startx + 640, starty + offset + 40, format);
                                e.Graphics.DrawString(credit.ToString(), Headerfont3, new SolidBrush(System.Drawing.Color.Black), startx + 730, starty + offset + 40, format);
                                //e.Graphics.DrawString(bal.ToString(), Headerfont3, new SolidBrush(System.Drawing.Color.Black), startx + 670, starty + offset + 40, format);
                                //e.Graphics.DrawString(narration.ToString(), Headerfont3, new SolidBrush(System.Drawing.Color.Black), startx + 670, starty + offset + 40);

                                offset = offset + (int)fontheight + 10;
                                value = k;
                                nooflines++;
                                j++;

                            }
                            else
                            {
                                printeditems = j - 1;
                                hasmorepages = true;
                                PRINTTOTALPAGE = true;
                            }
                            if (hasmorepages == true)
                            {
                                e.Graphics.DrawString("coutinue...", Headerfont1, new SolidBrush(System.Drawing.Color.Black), startx + 40, 1020);
                            }
                        }
                        else
                        {
                            j++;
                            m++;
                        }
                    }
                }

                catch (Exception exc)
                {
                    string c = exc.Message;
                }
            }
          
            float newoffset = 957;
            if (!PRINTTOTALPAGE)
            {
                PAGETOTAL = true;
                if (PAGETOTAL)
                {
                    try
                    {
                        e.Graphics.DrawLine(blackpen, 10, 1000, 810, 1000); //h
                        e.Graphics.DrawLine(blackpen, 10, 1030, 810, 1030); //h
                        string ldblnce = dgledgerTrns.Rows[value + 2].Cells["VOUCHERTYPE"].Value.ToString();
                        //e.Graphics.DrawString(ldblnce.ToString(), Headerfont1, new SolidBrush(System.Drawing.Color.Black), startx + 145, 1110);
                        e.Graphics.DrawString(ldblnce.ToString(), Headerfont1, new SolidBrush(System.Drawing.Color.Black), startx + 145, 1011);
                        string dbtotal = dgledgerTrns.Rows[value + 2].Cells["DEBIT"].Value.ToString();
                        //e.Graphics.DrawString(dbtotal.ToString(), Headerfont1, new SolidBrush(System.Drawing.Color.Black), startx + 640, 1110, format);
                        e.Graphics.DrawString(dbtotal.ToString(), Headerfont1, new SolidBrush(System.Drawing.Color.Black), startx + 640, 1011, format);
                        string cdtotal = dgledgerTrns.Rows[value + 2].Cells["CREDIT"].Value.ToString();
                       // e.Graphics.DrawString(cdtotal.ToString(), Headerfont1, new SolidBrush(System.Drawing.Color.Black), startx + 730, 1110, format);
                        e.Graphics.DrawString(cdtotal.ToString(), Headerfont1, new SolidBrush(System.Drawing.Color.Black), startx + 730, 1011, format);
                        string bltotal = dgledgerTrns.Rows[value + 2].Cells["BALANCES"].Value.ToString();
                        string BL = bltotal.Remove(bltotal.Length - 3);
                     //   e.Graphics.DrawString(BL.ToString(), Headerfont1, new SolidBrush(System.Drawing.Color.Black), startx + 670, 1110, format);

                        string opblnce = dgledgerTrns.Rows[value + 4].Cells["PARTICULARS"].Value.ToString();
                      //  e.Graphics.DrawString(opblnce.ToString(), Headerfont1, new SolidBrush(System.Drawing.Color.Black), startx + 258, 930);

                      //  string opblnceamt = dgledgerTrns.Rows[value + 4].Cells["DEBIT"].Value.ToString();
                      //  string op = opblnceamt.Remove(opblnceamt.Length - 3);
                     //   e.Graphics.DrawString(op.ToString() + "Dr", Headerfont1, new SolidBrush(System.Drawing.Color.Black), startx + 488, 930, format);

                        string clbalance = dgledgerTrns.Rows[value + 5].Cells["PARTICULARS"].Value.ToString();
                   //     e.Graphics.DrawString(clbalance.ToString() + " : ", Headerfont2, new SolidBrush(System.Drawing.Color.Black), startx + 400, 1140);

                        string clbalanceamt = dgledgerTrns.Rows[value + 5].Cells["DEBIT"].Value.ToString();
                        string cl = clbalanceamt.Remove(clbalanceamt.Length - 3);
                        e.Graphics.DrawString(cl.ToString(), Headerfont2, new SolidBrush(System.Drawing.Color.Black), startx + 725, 1041, format);
                        newoffset = newoffset + 20;
                        newoffset = newoffset + 20;
                        newoffset = newoffset + 20;
                    }
                    catch
                    {
                    }
                }
                PAGETOTAL = false;
            }
            e.HasMorePages = hasmorepages;
        }

        void printDocumentA4Form8_PrintPage_CustomizedForPtlCrusher(object sender, PrintPageEventArgs e)
        {
            Company company = Common.getCompany();
            Pen blackpen = new Pen(System.Drawing.Color.Black, 1);
            System.Drawing.Font Headerfont2 = new System.Drawing.Font("Times New Roman", 14, FontStyle.Bold);//Times New Roman
            System.Drawing.Font Headerfont1 = new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold);
            System.Drawing.Font Headerfont3 = new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular);
            System.Drawing.Font Headerfont4 = new System.Drawing.Font("Times New Roman", 10, FontStyle.Bold);
            m = 0;
            bool hasmorepages = false;
            float xpos;
            int startx = 50;
            int starty = 20;
            int offset = 15;
            bool PRINTTOTALPAGE = true;
            int w = e.MarginBounds.Width / 2;
            int x = e.MarginBounds.Left;
            int y = e.MarginBounds.Top;
            string vchno = "";
            string vehNo = "";
            string rate = "";
            string qty = "";
            string snno = "";
            string dated = "";
            string vchtype = "";
            string partclrs = "";
            string vtype="";
            string debit = "";
            string credit = "";
            string balance = "";
            string narration = "";
            string cusName = "";
            int value = 0;
            var tabDataForeColor = System.Drawing.Color.Black;
            int height = 100 + y;
            Pen blackPen1 = new Pen(System.Drawing.Color.Black, 1);
            var txtDataWidth = e.Graphics.MeasureString(CompanyName, Headerfont1).Width;
            double pricWtax = 0;
            decimal a = 0;
            StringFormat format = new StringFormat(StringFormatFlags.DirectionRightToLeft);
            using (var sf = new StringFormat())
            {
                if (drpLedger.Text == "SALES ACCOUNT" )
                {
                    height += 15;
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Alignment = StringAlignment.Center;
                    xpos = e.MarginBounds.Left + (e.MarginBounds.Width / 2);
                    e.Graphics.DrawRectangle(blackpen, 10, 10, 800, 1050);
                    e.Graphics.DrawLine(blackpen, 10, 80, 810, 80); //h
                    e.Graphics.DrawLine(blackpen, 10, 110, 810, 110); //h
                    e.Graphics.DrawLine(blackpen, 10, 1030, 810, 1030); //h

                    e.Graphics.DrawLine(blackpen, 35, 80, 35, 1000); //v sl no
                    e.Graphics.DrawLine(blackpen, 105, 80, 105, 1000); //date
                    e.Graphics.DrawLine(blackpen, 205, 80, 205, 1000); //VEH NO
                    e.Graphics.DrawLine(blackpen, 395, 80, 395, 1000);// CUS NAME
                    e.Graphics.DrawLine(blackpen, 590, 80, 590, 1000); //particulars
                    e.Graphics.DrawLine(blackpen, 668, 80, 668, 1000); //debit     
                    e.Graphics.DrawLine(blackpen, 720, 80, 720, 1000); //balance    

                    string date = "Ledger/Statement of Account for the period From " + Date_From.Value.ToShortDateString() + " To " + Date_To.Value.ToShortDateString();
                    int centerOfPage = e.PageBounds.Width / 2;
                    int nameStartPosision = centerOfPage - TextRenderer.MeasureText(company.Name, Headerfont4).Width / 2;
                    int nameStartPosision0 = centerOfPage - TextRenderer.MeasureText(date, Headerfont3).Width / 2;
                    int nameStartPosision1 = centerOfPage - TextRenderer.MeasureText("ACCOUNT : " + drpLedger.Text, Headerfont4).Width / 2;

                    // e.Graphics.DrawString(company.Name, Headerfont4, new SolidBrush(System.Drawing.Color.Black), nameStartPosision, 15);
                    e.Graphics.DrawString(date, Headerfont3, new SolidBrush(System.Drawing.Color.Black), nameStartPosision0, 30);
                    e.Graphics.DrawString("ACCOUNT : " + drpLedger.Text, Headerfont4, new SolidBrush(System.Drawing.Color.Black), nameStartPosision1, 45);

                    if (name == drpLedger.Text && under == "14")
                    {
                        e.Graphics.DrawString("SUNDRY DEBTORS", Headerfont4, new SolidBrush(System.Drawing.Color.Black), 360, 55);
                    }

                    if (name == drpLedger.Text && under == "13")
                    {
                        e.Graphics.DrawString("SUNDRY CREDITORS", Headerfont4, new SolidBrush(System.Drawing.Color.Black), 360, 55);
                    }
                    string headtext = "No".PadRight(5) + "Date".PadRight(10) + "VehicleNo".PadRight(18) + "CustomerName".PadRight(28) + "Particulars".PadRight(39)+ "Rate".PadRight(13) + "Qty".PadRight(13) + "Credit";

                    e.Graphics.DrawString(headtext, Headerfont1, new SolidBrush(System.Drawing.Color.Black), startx - 40, starty + offset + 50);
                    offset = offset + 40;
                    float fontheight = Headerfont1.GetHeight();
                    try
                    {
                        int i = 0;
                        int j = 1;
                        int nooflines = 0;
                        //foreach (DataGridViewRow row in dgledgerTrns.Rows)
                        for (int k = 0; k < dataGridViewPTL.Rows.Count - 5; k++)
                        {
                            PRINTTOTALPAGE = false;
                            if (j > printeditems)
                            {
                                if (nooflines < 32)
                                {
                                    string typ = "";
                                    snno = dataGridViewPTL.Rows[k].Cells["SlNo"].Value.ToString();
                                    dated = dataGridViewPTL.Rows[k].Cells["DATES"].Value.ToString();
                                    vchno = dataGridViewPTL.Rows[k].Cells["VOUCHERNUM"].Value.ToString();
                                    vehNo = dataGridViewPTL.Rows[k].Cells["VEHICLENO"].Value == null ? "" : dataGridViewPTL.Rows[k].Cells["VEHICLENO"].Value.ToString();
                                    rate = dataGridViewPTL.Rows[k].Cells["RATE"].Value == null ? "" : dataGridViewPTL.Rows[k].Cells["RATE"].Value.ToString();
                                    qty = dataGridViewPTL.Rows[k].Cells["QUANTITY"].Value == null ? "" : dataGridViewPTL.Rows[k].Cells["QUANTITY"].Value.ToString();
                                    vtype = dataGridViewPTL.Rows[k].Cells["VOUCHERTYPES"].Value.ToString();
                                    // vchtype = dataGridViewPTL.Rows[k].Cells["VOUCHERTYPES"].Value.ToString();
                                    //    partclrs = dgledgerTrns.Rows[k].Cells["PARTICULARS"].Value.ToString();
                                    if (Regex.IsMatch(vtype.ToUpper(), "SALES"))
                                        partclrs = dataGridViewPTL.Rows[k].Cells["NARRATIONS"].Value.ToString();
                                    else
                                        partclrs = dataGridViewPTL.Rows[k].Cells["VOUCHERTYPES"].Value.ToString();
                                    debit = dataGridViewPTL.Rows[k].Cells["DEBITS"].Value.ToString();
                                    credit = dataGridViewPTL.Rows[k].Cells["CREDITS"].Value.ToString();
                                    balance = dataGridViewPTL.Rows[k].Cells["BALANCESS"].Value.ToString();
                                    string bal = balance.Remove(balance.Length - 3);
                                    narration = dataGridViewPTL.Rows[k].Cells["NARRATIONS"].Value.ToString();
                                    cusName = dataGridViewPTL.Rows[k].Cells["PARTICULAR"].Value.ToString();
                                    //if (dated != "")
                                    //    typ = debit == "" ? "Dr" : "Cr";
                                    //else
                                    //{
                                    if (dated == "")
                                    {
                                        typ = "";
                                        if(ChekOPening.Checked)
                                        partclrs = "Opening Balance";
                                    }

                                    //e.Graphics.DrawString(vchno.ToString(), Headerfont3, new SolidBrush(System.Drawing.Color.Black), startx + 462, starty + offset + 40);
                                    //e.Graphics.DrawString(debit.ToString(), Headerfont3, new SolidBrush(System.Drawing.Color.Black), startx + 640, starty + offset + 40, format);

                                    e.Graphics.DrawString(snno.ToString(), Headerfont3, new SolidBrush(System.Drawing.Color.Black), startx - 40, starty + offset + 40);
                                    e.Graphics.DrawString(dated + "  " + typ, Headerfont3, new SolidBrush(System.Drawing.Color.Black), startx - 13, starty + offset + 40);
                                    e.Graphics.DrawString(vehNo.ToString(), Headerfont3, new SolidBrush(System.Drawing.Color.Black), startx + 55, starty + offset + 40);
                                    int counts = cusName.Length;
                                    if (counts > 19)
                                    {
                                           int Idx = cusName.IndexOf(" ");
                                        //string v = cusName.Substring(0, counts / 2);
                                        //string s = cusName.Substring(counts / 2);
                                           string v = cusName.Substring(0, Idx);
                                           string s = cusName.Substring(Idx+1);
                                        string result = v + "\n" + s;
                                        //if (s.Length > 19)
                                        //{
                                        //    int idx1 = s.IndexOf(" ");
                                        //    string v1 = s.Substring(0, idx1);
                                        //    string s1 = s.Substring(idx1 + 1);
                                        //    string resultPar2 = v + "\n" + v1 + "\n" + s1;
                                        //    e.Graphics.DrawString(resultPar2.ToString(), Headerfont3, new SolidBrush(System.Drawing.Color.Black), startx + 158, starty + offset + 50);
                                        //}
                                        //else
                                        //{
                                            e.Graphics.DrawString(result.ToString(), Headerfont3, new SolidBrush(System.Drawing.Color.Black), startx + 158, starty + offset + 40);
                                        //}
                                    }
                                    else
                                    {
                                        e.Graphics.DrawString(cusName.ToString(), Headerfont3, new SolidBrush(System.Drawing.Color.Black), startx + 158, starty + offset + 40);

                                    }


                                    if (partclrs == "Opening Balance")
                                    {
                                        if (ChekOPening.Checked)
                                        {
                                            e.Graphics.DrawString(partclrs.ToString(), Headerfont3, new SolidBrush(System.Drawing.Color.Black), startx + 180, starty + offset + 40);
                                        }
                                    }
                                    else
                                    {
                                       // e.Graphics.DrawString(partclrs.ToString(), Headerfont3, new SolidBrush(System.Drawing.Color.Black), startx + 180, starty + offset + 40);
                                    int count = partclrs.Length;
                                    if (count > 19)
                                    {
                                        int Idx = partclrs.IndexOf(" ");

                                        //string v = cusName.Substring(0, counts / 2);
                                        //string s = cusName.Substring(counts / 2);
                                        string v = partclrs.Substring(0, Idx);
                                        string s = partclrs.Substring(Idx + 1);
                                        string resultparticular = v + "\n" + s;

                                        //if (s.Length > 19)
                                        //{
                                        //    int idx1 = s.IndexOf(" ");
                                        //    string v1 = s.Substring(0, idx1);
                                        //    string s1 = s.Substring(idx1 + 1);
                                        //    string resultPar2 = v + "\n" + v1 + "\n" + s1;
                                        //    e.Graphics.DrawString(resultPar2.ToString(), Headerfont3, new SolidBrush(System.Drawing.Color.Black), startx + 360, starty + offset + 50);

                                        //}
                                        //else
                                        //{
                                            e.Graphics.DrawString(resultparticular.ToString(), Headerfont3, new SolidBrush(System.Drawing.Color.Black), startx + 350, starty + offset + 40);
                                        //}
                                    }
                                    else
                                    {
                                        e.Graphics.DrawString(partclrs.ToString(), Headerfont3, new SolidBrush(System.Drawing.Color.Black), startx + 355, starty + offset + 40);
                                    }

                                }

                                    e.Graphics.DrawString(rate.ToString(), Headerfont3, new SolidBrush(System.Drawing.Color.Black), startx + 550, starty + offset + 40);

                                    //e.Graphics.DrawString(vchtype.ToString(), Headerfont3, new SolidBrush(System.Drawing.Color.Black), startx + 328, starty + offset + 40);
                                    e.Graphics.DrawString(qty.ToString(), Headerfont3, new SolidBrush(System.Drawing.Color.Black), startx + 670, starty + offset + 40, format);
                                    if (partclrs == "Opening Balance")
                                    {
                                        if (ChekOPening.Checked)
                                        {
                                            e.Graphics.DrawString(credit.ToString(), Headerfont3, new SolidBrush(System.Drawing.Color.Black), startx + 755, starty + offset + 40, format);
                                        }
                                    }
                                    else
                                    {
                                        e.Graphics.DrawString(credit.ToString(), Headerfont3, new SolidBrush(System.Drawing.Color.Black), startx + 755, starty + offset + 40, format);
                                    }

                                    e.Graphics.DrawLine(blackpen, 10, 1000, 810, 1000);



                                    //e.Graphics.DrawString(bal.ToString(), Headerfont3, new SolidBrush(System.Drawing.Color.Black), startx + 670, starty + offset + 40, format);
                                    //e.Graphics.DrawString(narration.ToString(), Headerfont3, new SolidBrush(System.Drawing.Color.Black), startx + 670, starty + offset + 40);

                                    offset = offset + (int)fontheight + 10;
                                    value = k;
                                    nooflines++;
                                    j++;

                                }
                                else
                                {
                                    printeditems = j - 1;
                                    hasmorepages = true;
                                    PRINTTOTALPAGE = true;
                                }
                                if (hasmorepages == true)
                                {
                                    e.Graphics.DrawString("continue...", Headerfont1, new SolidBrush(System.Drawing.Color.Black), startx + 40, 1040);
                                }
                            }
                            else
                            {
                                j++;
                                m++;
                            }
                        }
                    }

                    catch (Exception exc)
                    {
                        string c = exc.Message;
                    }
                }
                else
                {
                height += 15;
                sf.LineAlignment = StringAlignment.Center;
                sf.Alignment = StringAlignment.Center;
                xpos = e.MarginBounds.Left + (e.MarginBounds.Width / 2);
                e.Graphics.DrawRectangle(blackpen, 10, 10, 800, 1050);
                e.Graphics.DrawLine(blackpen, 10, 80, 810, 80); //h
                e.Graphics.DrawLine(blackpen, 10, 110, 810, 110); //h
                e.Graphics.DrawLine(blackpen, 10, 1030, 810, 1030); //h

                e.Graphics.DrawLine(blackpen, 35, 80, 35, 1000); //v sl no
                e.Graphics.DrawLine(blackpen, 120, 80, 120, 1000); //date
                e.Graphics.DrawLine(blackpen, 230, 80, 230, 1000); //voucher
                e.Graphics.DrawLine(blackpen, 450, 80, 450, 1000); //vehicle no
                e.Graphics.DrawLine(blackpen, 530, 80, 530, 1000); //particulars
                e.Graphics.DrawLine(blackpen, 590, 80, 590, 1000); //debit     
              //  e.Graphics.DrawLine(blackpen, 620, 80, 620, 1000); //CREDIT
                e.Graphics.DrawLine(blackpen, 700, 80, 700, 1030); //balance    


                string date = "Ledger/Statement of Account for the period From " + Date_From.Value.ToShortDateString() + " To " + Date_To.Value.ToShortDateString();
                int centerOfPage = e.PageBounds.Width / 2;
                int nameStartPosision = centerOfPage - TextRenderer.MeasureText(company.Name, Headerfont4).Width / 2;
                int nameStartPosision0 = centerOfPage - TextRenderer.MeasureText(date, Headerfont3).Width / 2;
                int nameStartPosision1 = centerOfPage - TextRenderer.MeasureText("ACCOUNT : " + drpLedger.Text, Headerfont4).Width / 2;

               // e.Graphics.DrawString(company.Name, Headerfont4, new SolidBrush(System.Drawing.Color.Black), nameStartPosision, 15);
                e.Graphics.DrawString(date, Headerfont3, new SolidBrush(System.Drawing.Color.Black), nameStartPosision0, 30);
                e.Graphics.DrawString("ACCOUNT : " + drpLedger.Text, Headerfont4, new SolidBrush(System.Drawing.Color.Black), nameStartPosision1, 45);

                if (name == drpLedger.Text && under == "14")
                {
                    e.Graphics.DrawString("SUNDRY DEBTORS", Headerfont4, new SolidBrush(System.Drawing.Color.Black), 360, 55);
                }

                if (name == drpLedger.Text && under == "13")
                {
                    e.Graphics.DrawString("SUNDRY CREDITORS", Headerfont4, new SolidBrush(System.Drawing.Color.Black), 360, 55);
                }
                //string headtext = "No".PadRight(5) + "Date".PadRight(32) + "Particulars".PadRight(32) + "VoucherType".PadRight(20) + "VoucherNo".PadRight(20) + "Debit".PadRight(17) + "Credit";
                
                    string headtext = "No".PadRight(5) + "Date".PadRight(18) + "VehicleNo".PadRight(15) + "Particulars".PadRight(43) + "Rate".PadRight(16) + "Qty".PadRight(20) + "Debit".PadRight(20) + "Credit";
                    e.Graphics.DrawString(headtext, Headerfont1, new SolidBrush(System.Drawing.Color.Black), startx - 40, starty + offset + 50);
                    offset = offset + 40;
                    float fontheight = Headerfont1.GetHeight();
                    try
                    {
                        int i = 0;
                        int j = 1;
                        int nooflines = 0;
                        //foreach (DataGridViewRow row in dgledgerTrns.Rows)
                        for (int k = 0; k < dataGridViewPTL.Rows.Count - 5; k++)
                        {
                            PRINTTOTALPAGE = false;
                            if (j > printeditems)
                            {
                                if (nooflines < 32)
                                {
                                    string typ = "";
                                    snno = dataGridViewPTL.Rows[k].Cells["SlNo"].Value.ToString();
                                    dated = dataGridViewPTL.Rows[k].Cells["DATES"].Value.ToString();
                                    vchno = dataGridViewPTL.Rows[k].Cells["VOUCHERNUM"].Value.ToString();
                                    vehNo = dataGridViewPTL.Rows[k].Cells["VEHICLENO"].Value == null ? "" : dataGridViewPTL.Rows[k].Cells["VEHICLENO"].Value.ToString();
                                    rate = dataGridViewPTL.Rows[k].Cells["RATE"].Value == null ? "" : dataGridViewPTL.Rows[k].Cells["RATE"].Value.ToString();
                                    qty = dataGridViewPTL.Rows[k].Cells["QUANTITY"].Value == null ? "" : dataGridViewPTL.Rows[k].Cells["QUANTITY"].Value.ToString();
                                    vtype = dataGridViewPTL.Rows[k].Cells["VOUCHERTYPES"].Value.ToString();
                                    // vchtype = dataGridViewPTL.Rows[k].Cells["VOUCHERTYPES"].Value.ToString();
                                    //    partclrs = dgledgerTrns.Rows[k].Cells["PARTICULARS"].Value.ToString();
                                    if (drpLedger.Text == "CASH ACCOUNT")
                                    {
                                        partclrs = dataGridViewPTL.Rows[k].Cells["PARTICULAR"].Value.ToString();
                                    }

                                    else  if (Regex.IsMatch(vtype.ToUpper(), "SALES"))
                                        partclrs = dataGridViewPTL.Rows[k].Cells["NARRATIONS"].Value.ToString();
                                    else
                                        partclrs = dataGridViewPTL.Rows[k].Cells["VOUCHERTYPES"].Value.ToString();
                                    debit = dataGridViewPTL.Rows[k].Cells["DEBITS"].Value.ToString();
                                    credit = dataGridViewPTL.Rows[k].Cells["CREDITS"].Value.ToString();
                                    balance = dataGridViewPTL.Rows[k].Cells["BALANCESS"].Value.ToString();
                                    string bal = balance.Remove(balance.Length - 3);
                                    narration = dataGridViewPTL.Rows[k].Cells["NARRATIONS"].Value.ToString();
                                    //if (dated != "")
                                    //    typ = debit == "" ? "Dr" : "Cr";
                                    //else
                                    //{
                                    if (dated == "")
                                    {
                                        typ = "";
                                        if (ChekOPening.Checked)
                                        partclrs = "Opening Balance";
                                    }

                                    //e.Graphics.DrawString(snno.ToString(), Headerfont3, new SolidBrush(System.Drawing.Color.Black), startx - 40, starty + offset + 40);
                                    //e.Graphics.DrawString(dated + "  " + typ, Headerfont3, new SolidBrush(System.Drawing.Color.Black), startx - 15, starty + offset + 40);
                                    //e.Graphics.DrawString(partclrs.ToString(), Headerfont3, new SolidBrush(System.Drawing.Color.Black), startx + 150, starty + offset + 40);
                                    //e.Graphics.DrawString(vchtype.ToString(), Headerfont3, new SolidBrush(System.Drawing.Color.Black), startx + 328, starty + offset + 40);
                                    //e.Graphics.DrawString(vchno.ToString(), Headerfont3, new SolidBrush(System.Drawing.Color.Black), startx + 462, starty + offset + 40);
                                    //e.Graphics.DrawString(debit.ToString(), Headerfont3, new SolidBrush(System.Drawing.Color.Black), startx + 640, starty + offset + 40, format);
                                    //e.Graphics.DrawString(credit.ToString(), Headerfont3, new SolidBrush(System.Drawing.Color.Black), startx + 730, starty + offset + 40, format);

                                    e.Graphics.DrawString(snno.ToString(), Headerfont3, new SolidBrush(System.Drawing.Color.Black), startx - 40, starty + offset + 40);
                                    e.Graphics.DrawString(dated + "  " + typ, Headerfont3, new SolidBrush(System.Drawing.Color.Black), startx - 13, starty + offset + 40);
                                    e.Graphics.DrawString(vehNo.ToString(), Headerfont3, new SolidBrush(System.Drawing.Color.Black), startx + 80, starty + offset + 40);
                                    if (partclrs == "Opening Balance" && dated == "")
                                    {
                                        if (ChekOPening.Checked)
                                        {
                                            e.Graphics.DrawString(partclrs.ToString(), Headerfont3, new SolidBrush(System.Drawing.Color.Black), startx + 180, starty + offset + 40);
                                        }
                                    }
                                    else
                                    {
                                        e.Graphics.DrawString(partclrs.ToString(), Headerfont3, new SolidBrush(System.Drawing.Color.Black), startx + 180, starty + offset + 40);

                                    }//e.Graphics.DrawString(partclrs.ToString(), Headerfont3, new SolidBrush(System.Drawing.Color.Black), startx + 300, starty + offset + 40);

                                    e.Graphics.DrawString(rate.ToString(), Headerfont3, new SolidBrush(System.Drawing.Color.Black), startx + 400, starty + offset + 40);
                                    e.Graphics.DrawString(qty.ToString(), Headerfont3, new SolidBrush(System.Drawing.Color.Black), startx + 490, starty + offset + 40);

                                    //e.Graphics.DrawString(vchtype.ToString(), Headerfont3, new SolidBrush(System.Drawing.Color.Black), startx + 328, starty + offset + 40);
                                    e.Graphics.DrawString(debit.ToString(), Headerfont3, new SolidBrush(System.Drawing.Color.Black), startx + 645, starty + offset + 40, format);
                                    e.Graphics.DrawString(credit.ToString(), Headerfont3, new SolidBrush(System.Drawing.Color.Black), startx + 745, starty + offset + 40, format);


                                    e.Graphics.DrawLine(blackpen, 10, 1000, 810, 1000);



                                    //e.Graphics.DrawString(bal.ToString(), Headerfont3, new SolidBrush(System.Drawing.Color.Black), startx + 670, starty + offset + 40, format);
                                    //e.Graphics.DrawString(narration.ToString(), Headerfont3, new SolidBrush(System.Drawing.Color.Black), startx + 670, starty + offset + 40);

                                    offset = offset + (int)fontheight + 10;
                                    value = k;
                                    nooflines++;
                                    j++;

                                }
                                else
                                {
                                    printeditems = j - 1;
                                    hasmorepages = true;
                                    PRINTTOTALPAGE = true;
                                }
                                if (hasmorepages == true)
                                {
                                    e.Graphics.DrawString("continue...", Headerfont1, new SolidBrush(System.Drawing.Color.Black), startx + 40, 1040);
                                }
                            }
                            else
                            {
                                j++;
                                m++;
                            }
                        }
                    }

                    catch (Exception exc)
                    {
                        string c = exc.Message;
                    }
            } 
            }

            float newoffset = 957;
            if (!PRINTTOTALPAGE)
            {
                PAGETOTAL = true;
                if (PAGETOTAL)
                {
                    try
                    {

                        e.Graphics.DrawLine(blackpen, 10, 1000, 810, 1000); //h
                        e.Graphics.DrawLine(blackpen, 10, 1030, 810, 1030); //h
                        string ldblnce = dataGridViewPTL.Rows[value + 2].Cells["VOUCHERTYPES"].Value.ToString();
                        //e.Graphics.DrawString(ldblnce.ToString(), Headerfont1, new SolidBrush(System.Drawing.Color.Black), startx + 145, 1110);
                        e.Graphics.DrawString(ldblnce.ToString(), Headerfont1, new SolidBrush(System.Drawing.Color.Black), startx + 145, 1011);
                        string dbtotal = dataGridViewPTL.Rows[value + 2].Cells["DEBITS"].Value.ToString();
                        //e.Graphics.DrawString(dbtotal.ToString(), Headerfont1, new SolidBrush(System.Drawing.Color.Black), startx + 640, 1110, format);

                        if (drpLedger.Text == "SALES ACCOUNT")
                        {

                        }
                        else
                        {
                            e.Graphics.DrawString(dbtotal.ToString(), Headerfont1, new SolidBrush(System.Drawing.Color.Black), startx + 640, 1011, format);
                        }
                        string cdtotal = dataGridViewPTL.Rows[value + 2].Cells["CREDITS"].Value.ToString();
                        // e.Graphics.DrawString(cdtotal.ToString(), Headerfont1, new SolidBrush(System.Drawing.Color.Black), startx + 730, 1110, format);
                        e.Graphics.DrawString(cdtotal.ToString(), Headerfont1, new SolidBrush(System.Drawing.Color.Black), startx + 750, 1011, format);
                        string bltotal = dataGridViewPTL.Rows[value + 2].Cells["BALANCESS"].Value.ToString();
                        string BL = bltotal.Remove(bltotal.Length - 3);
                        //   e.Graphics.DrawString(BL.ToString(), Headerfont1, new SolidBrush(System.Drawing.Color.Black), startx + 670, 1110, format);

                        string opblnce = dataGridViewPTL.Rows[value + 4].Cells["PARTICULAR"].Value.ToString();
                        //  e.Graphics.DrawString(opblnce.ToString(), Headerfont1, new SolidBrush(System.Drawing.Color.Black), startx + 258, 930);

                        //  string opblnceamt = dgledgerTrns.Rows[value + 4].Cells["DEBIT"].Value.ToString();
                        //  string op = opblnceamt.Remove(opblnceamt.Length - 3);
                        //   e.Graphics.DrawString(op.ToString() + "Dr", Headerfont1, new SolidBrush(System.Drawing.Color.Black), startx + 488, 930, format);

                        string clbalance = dataGridViewPTL.Rows[value + 5].Cells["PARTICULAR"].Value.ToString();
                        e.Graphics.DrawString(clbalance.ToString() + " : ", Headerfont2, new SolidBrush(System.Drawing.Color.Black), startx + 400, 1041);

                        string clbalanceamt = dataGridViewPTL.Rows[value + 5].Cells["DEBITS"].Value.ToString();
                        string cl = clbalanceamt.Remove(clbalanceamt.Length - 3);
                        e.Graphics.DrawString(cl.ToString(), Headerfont2, new SolidBrush(System.Drawing.Color.Black), startx + 725, 1041, format);
                        newoffset = newoffset + 20;
                        newoffset = newoffset + 20;
                        newoffset = newoffset + 20;
                    }
                    catch
                    {
                    }
                }
                PAGETOTAL = false;
            }
            e.HasMorePages = hasmorepages;
        }
        private void btn_print_Click(object sender, EventArgs e)
        {
            PrintDocument printDocument = new PrintDocument();
            PrintPreviewDialog prvdlg = new PrintPreviewDialog();
            printDocument.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Form8", 820, 1120);
            Company cmp = Common.getCompany();
            printDocument.PrintPage += printDocumentA4Form8_PrintPage_CustomizedForPtlCrusher;

            //if (Regex.IsMatch(cmp.Name, "PRESTIGE"))
            //    printDocument.PrintPage += printDocumentA4Form8_PrintPage_Customized;
            //else if (Regex.IsMatch(cmp.Name, "PRESTIGE"))
            //{
            //    printDocument.PrintPage += printDocumentA4Form8_PrintPage_CustomizedForPtlCrusher;
            //}
            //else
            //{
            //    printDocument.PrintPage += printDocumentA4Form8_PrintPage;
            //    printDocument.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Form8", 840, 1188);
            //} 
            prvdlg.Document = printDocument;
            prvdlg.ShowDialog();
         
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void linkLabel1cus_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CustomerSummary obj = new CustomerSummary(true);
            obj.ShowDialog();
        }

        private void linkLabel2sup_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CustomerSummary obj = new CustomerSummary(false);
            obj.ShowDialog();
        }

        private void dataGridViewPTL_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            foreach (DataGridViewCell cell in dataGridViewPTL.Rows[e.RowIndex].Cells)
            {
                if (cell.GetType() == typeof(DataGridViewImageCell))
                {
                    cell.Value = DBNull.Value;
                }
            }
        }

        private void dataGridViewPTL_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex < 0)
            //{
            //    return;
            //}

            //int index = e.RowIndex;
            //dgledgerTrns.Rows[index].Selected = true;
        }

        private void dataGridViewPTL_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewPTL.Rows.Count > 0 && dataGridViewPTL.CurrentRow != null)
            {
                c = dataGridViewPTL.CurrentRow.Cells;
                String voucher_type = c["VOUCHERTYPES"].Value.ToString();
                if (voucher_type.Equals("Purchase"))
                {
                    PurchaseMaster pur = new PurchaseMaster(c["VOUCHERNUM"].Value.ToString());
                    pur.Show();
                }
                else if (voucher_type.Equals("Purchase Return"))
                {
                    Purchase_Return purret = new Purchase_Return(c["VOUCHERNUM"].Value.ToString());
                    purret.Show();
                }
                else if (voucher_type.Equals("Sales Return"))
                {
                    Sales_Return ret = new Sales_Return(c["VOUCHERNUM"].Value.ToString());
                    ret.Show();
                }
                else if (voucher_type.ToLower().StartsWith("sales"))
                {
                    SalesQ sal = new SalesQ(c["VOUCHERNUM"].Value.ToString());
                    Accounts.LedgerReport.checkLedger = true;
                    checkLedger = true;
                    sal.Show();
                    Accounts.LedgerReport.checkLedger = false;
                }
                else if (voucher_type.ToLower().StartsWith("cash payment"))
                {
                    PaymentVoucher2 Paymt = new PaymentVoucher2(0, c["VOUCHERNUM"].Value.ToString());
                    Paymt.Show();
                }
                else if (voucher_type.ToLower().StartsWith("cash receipt"))
                {
                    PaymentVoucher2 Paymt2 = new PaymentVoucher2(1, c["VOUCHERNUM"].Value.ToString());
                    Paymt2.Show();
                }
                else if (voucher_type.ToLower().StartsWith("journal"))
                {
                    Accounting_Voucher acv = new Accounting_Voucher(1, c["VOUCHERNUM"].Value.ToString());
                    acv.Show();
                }
                else if (voucher_type.ToLower().StartsWith("salary"))
                {
                    Payrollvouch payrol = new Payrollvouch(c["VOUCHERNUM"].Value.ToString());
                    payrol.Show();
                }
                
            }
            
            
            
        }
    }
}
