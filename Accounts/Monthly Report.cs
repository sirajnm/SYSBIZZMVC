using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sys_Sols_Inventory.Accounts
{
    public partial class Monthly_Report : Form
    {
        int ledgeridfordrilling = 0;
        Class.Ledgers led = new Class.Ledgers();
        Login lg = (Login)Application.OpenForms["Login"];
        Initial mdi = (Initial)Application.OpenForms["Initial"];
        Class.CompanySetup ComSet = new Class.CompanySetup();
        double Balance = 0;
        string ClosingBalance = "0.00";
        double SumCredit = 0, SumDebit = 0,sumclosing=0;
        DateTime FinancialStart;
        DateTime FinancialEnd;
        public String FormType, ClosingBal;
         public Monthly_Report(int id)
        {
            InitializeComponent();
            ledgeridfordrilling = id;
        }
        public Monthly_Report()
        {
            InitializeComponent();
        }

        private void lbltitle_Click(object sender, EventArgs e)
        {

        }
        public void bindledger()
        {
            DataTable dt = new DataTable();
            dt = led.SelectLedgerNmae();
            DataRow row = dt.NewRow();
            dt.Rows.InsertAt(row, 0);
            drpLedger.DisplayMember = "LEDGERNAME";
            drpLedger.ValueMember = "LEDGERID";
            drpLedger.DataSource = dt;


        }
        public void GetFinancialYear()
        {
            try
            {
                DataTable dt = new DataTable();
                ComSet.Status = true;
                dt = ComSet.GetFinancialYear();

                FinancialStart = Convert.ToDateTime(dt.Rows[0][1]);

                FinancialEnd = Convert.ToDateTime(dt.Rows[0][2]);

                Date_From.MinDate = FinancialStart;
                Date_From.MaxDate = FinancialEnd;

                Date_To.MinDate = FinancialStart;
                Date_To.MaxDate = FinancialEnd;


            }
            catch
            {
            }
        }
        public void chart()
        {
            try
            {
                chart1.DataSource = dgledgerTrns.DataSource;
                chart1.Series["Debit"].XValueMember = "MONT";
                chart1.Series["Debit"].YValueMembers = "DEBIT";
                chart1.Series["Credit"].XValueMember = "DATED";
                chart1.Series["Credit"].YValueMembers = "CREDIT";
                for (int i = 0; i< dgledgerTrns.Rows.Count; i++)
                {
                    string mon =dgledgerTrns.Rows[i].Cells["MONT"].Value.ToString();                   
                    string st = dgledgerTrns.Rows[i].Cells["DATED"].Value.ToString() +mon.Substring(0,3) ;
                    double va = Convert.ToDouble(dgledgerTrns.Rows[i].Cells["DEBIT"].Value);
                    double cr = Convert.ToDouble(dgledgerTrns.Rows[i].Cells["CREDIT"].Value);
                    chart1.Series["Debit"].Points.AddXY(st, va);
                    chart1.Series["Credit"].Points.AddXY(st, cr);
                }
                //dt = led.SelectLedgerTransactionsDatewise();
              //  chart1.Series["Series1"].Points.AddXY(dgledgerTrns.Columns["MONTH"].Value.ToString(), Convert.ToDouble (dgledgerTrns.Columns["DEBIT"]));
                //chart1.Series["Series1"].Points.AddXY("b", 35);
                //chart1.Series["Series1"].Points.AddXY("c", 12);
                //chart1.Series["Series1"].Points.AddXY("d", 40);
                chart1.DataBind();
            }
            catch (Exception EXC)
            {
                string sa = EXC.Message;
                
            }
           

            //this.chart1.Series[0].Points.AddXY(Convert.ToDouble(dgledgerTrns.Columns["MBAL"]),Convert.ToDouble(dgledgerTrns.Columns["M"]));
            //chart1.DataBind();
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
        private void Monthly_Report_Load(object sender, EventArgs e)
        {
            GetFinancialYear();
            bindledger();
             
            if (ledgeridfordrilling != 0)
            {
                drpLedger.SelectedValue = ledgeridfordrilling;
                string title = "Monthly Report Of " + drpLedger.Text.ToString();
                lbltitle.Text = title;

                Getrpt();
                AutoNumberRowsForGridView();
                chart();
                dgledgerTrns.FirstDisplayedScrollingRowIndex = dgledgerTrns.RowCount - 1;
            }

       

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
        public void GettingClosingBalance()
        {
            led.AccName = drpLedger.Text;
            led.LEDGERID = Convert.ToInt32(drpLedger.SelectedValue);
            led.date1 = Convert.ToDateTime(FinancialStart.ToShortDateString());
            led.date2 = Convert.ToDateTime(System.DateTime.Today.ToShortDateString());

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
               
                else
                {
                    Balance = Convert.ToDouble(ClosingBalance);
                }
                if (Convert.ToDecimal(ClosingBalance) < 0)
                {
                  //  dgledgerTrns.Rows.Add("", "", "", "", "", "", "0.00", "0.00", "0.00", "0.00", "");
                  //  ClosingBalance = Math.Abs(Convert.ToDouble(ClosingBalance)).ToString("n2") + " Cr";
                }
                else
                {
                   // dgledgerTrns.Rows.Add("", "", "", "", "", "", "0.00", "0.00", "0.00", "0.00", "");
                   // ClosingBalance = Math.Abs(Convert.ToDouble(ClosingBalance)).ToString("n2") + " Dr";
                }
            }


        }
        public void OpeningbalanceShowGrid()
        {
            dgledgerTrns.Rows.Add("", "", "", "", "", "", "", "", "", "", "");
            dgledgerTrns.Rows.Add("", "", "", "", "", "Balance", SumDebit, SumCredit, "", "", "");
            //  dgledgerTrns.Rows.Add("", "", "", "", "", "Opening Balance", ClosingBalance, "", "", "", "");
          

            //newRow3["DEBIT"] = dgledgerTrns.Rows[dgledgerTrns.Rows.Count - 3].Cells["BalanceS"].Value.ToString();
            //dgledgerTrns.Rows.Add(newRow3);

            //   newRow2["DEBIT"] = SumDebit.ToString();
            System.Windows.Forms.DataGridViewCellStyle boldStyle = new System.Windows.Forms.DataGridViewCellStyle();
            boldStyle.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold);
            dgledgerTrns.Rows[dgledgerTrns.Rows.Count - 2].DefaultCellStyle = boldStyle;
            // dgledgerTrns.Rows[dgledgerTrns.Rows.Count - 2].DefaultCellStyle.ForeColor = System.Drawing.Color.Red;
            // dgledgerTrns.Rows[dgledgerTrns.Rows.Count - 2].DefaultCellStyle.Font = new System.Drawing.Font(dgledgerTrns.Font, FontStyle.Bold);


            dgledgerTrns.Rows[dgledgerTrns.Rows.Count - 1].DefaultCellStyle.ForeColor = System.Drawing.Color.Red;
            dgledgerTrns.Rows[dgledgerTrns.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font(dgledgerTrns.Font, FontStyle.Bold);
            dgledgerTrns.FirstDisplayedScrollingRowIndex = dgledgerTrns.RowCount - 1;
        }
        public void Sum()
        {
            SumCredit = 0;
            SumDebit = 0;
            sumclosing = 0;
            double closbalance=0;

            for (int i = 0; i < (dgledgerTrns.Rows.Count)-1; i++)
            {
                SumCredit = SumCredit + Convert.ToDouble(dgledgerTrns.Rows[i].Cells["CREDIT"].Value);
                SumDebit = SumDebit + Convert.ToDouble(dgledgerTrns.Rows[i].Cells["DEBIT"].Value);

                string balanceval,dispmbal;
                double balance = 0, mnbalance=0;
                //if (i == 1)
                //{
                //    balanceval = dataGridView1.Rows[i - 1].Cells["BALANCE"].Value.ToString();

                //    balance = Convert.ToDouble(balanceval);
                //}
                //else
              
                  
                
                /*balanceval = dgledgerTrns.Rows[i - 1].Cells["MBAL"].Value.ToString();
                    string last = balanceval.Substring(balanceval.Length - 2);
              
                balanceval = balanceval.Substring(0, balanceval.Length - 2);
                if (last == "Cr")
                {
                    balance = Convert.ToDouble(Convert.ToDouble(balanceval) * -1);
                }
                else
                {
                    balance = Convert.ToDouble(balanceval);
                }*/

                mnbalance = Convert.ToDouble(dgledgerTrns.Rows[i].Cells["DEBIT"].Value) - Convert.ToDouble(dgledgerTrns.Rows[i].Cells["CREDIT"].Value);
                if (mnbalance < 0)
                {
                    balanceval = (Convert.ToDouble(balance * -1)).ToString("n2") + " Cr";
                }
                else
                {
                    balanceval = balance.ToString() + " Dr";
                }
                balance = balance + Convert.ToDouble(dgledgerTrns.Rows[i].Cells["DEBIT"].Value) - Convert.ToDouble(dgledgerTrns.Rows[i].Cells["CREDIT"].Value);
                if (mnbalance < 0)
                {
                    dispmbal = (Convert.ToDouble(mnbalance * -1)).ToString("n2") + " Cr";
                }
                else
                {
                    dispmbal = mnbalance.ToString() + " Dr";
                }
                dgledgerTrns.Rows[i].Cells["MBAL"].Value = dispmbal;

                this.dgledgerTrns.RowsDefaultCellStyle.BackColor = System.Drawing.Color.White;
                this.dgledgerTrns.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.White;
                ClosingBal = balanceval;

            }
            sumclosing = 0;
            for (int i = 0; i < (dgledgerTrns.Rows.Count)-1; i++)
            {
                string cbal,discbal;
               
                cbal = dgledgerTrns.Rows[i].Cells["MBAL"].Value.ToString();
                string last = cbal.Substring(cbal.Length - 2);
                cbal = cbal.Substring(0, cbal.Length - 2);
               
                if (last == "Cr")
                {
                    closbalance = Convert.ToDouble(Convert.ToDouble(cbal) * -1);
                }
                else
                {
                    closbalance = Convert.ToDouble(cbal);
                }
                sumclosing = Convert.ToDouble(sumclosing) +Convert.ToDouble(closbalance);
               
                if (sumclosing < 0)
                {
                    discbal = (Convert.ToDouble(sumclosing * -1)).ToString("n2") + " Cr";
                }
                else
                {
                    discbal = sumclosing.ToString("N") + " Dr";
                }
                dgledgerTrns.Rows[i].Cells["CBAL"].Value = discbal;
               
            }
            //for (int i = 1; i < dgledgerTrns.Rows.Count; i++)
            //{
            //    string balanceval;
            //    balanceval = dgledgerTrns.Rows[i - 1].Cells["MBAL"].Value.ToString();
            //    balanceval = balanceval.Substring(0, balanceval.Length - 2);
            //    string last = balanceval.Substring(balanceval.Length - 2);
            //    if (last == "Cr")
            //    {
            //         closbalance= Convert.ToDouble(Convert.ToDouble(balanceval) * -1);
            //    }
            //    else
            //    {
            //        closbalance = Convert.ToDouble(balanceval);
            //    }
            //    closbalance = closbalance + Convert.ToDouble(dgledgerTrns.Rows[i].Cells["M"].Value) - Convert.ToDouble(dgledgerTrns.Rows[i].Cells["CREDIT"].Value);
            //    if (balance < 0)
            //    {
            //        balanceval = (Convert.ToDouble(balance * -1)).ToString("n2") + " Cr";
            //    }
            //    else
            //    {
            //        balanceval = balance.ToString() + " Dr";
            //    }
            //    dgledgerTrns.Rows[i].Cells["MBAL"].Value = balanceval;

            //}

        }
        public void Getrpt()
        {
            dgledgerTrns.Rows.Clear();
            Balance = 0;

            ClosingBalance = "0";
            if (valid())
            {
                try
                {
                    if (FinancialStart.ToShortDateString() != System.DateTime.Today.ToShortDateString())
                    {


                     GettingClosingBalance();
                    }


                   // led.AccName = drpLedger.Text;
                    led.LEDGERID = Convert.ToInt32(drpLedger.SelectedValue);
                    //led.date1 = Convert.ToDateTime(FinancialStart.ToShortDateString());
                   // led.date2 = Convert.ToDateTime(System.DateTime.Today.ToShortDateString());

                    DataTable dt = new DataTable();
                    dt = led.SelectLedgerTransactionsDatewise();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            dgledgerTrns.Rows.Add("",dt.Rows[i]["YE"].ToString(),dt.Rows[i]["MONT"].ToString(), dt.Rows[i]["DEBIT"].ToString(), dt.Rows[i]["CREDIT"].ToString(),"", dt.Rows[i]["DEBIT"].ToString(), dt.Rows[i]["CREDIT"].ToString());
                            //dgledgerTrns.Rows.Add("", "", dt.Rows[i]["DEBIT"].ToString(), dt.Rows[i]["CREDIT"].ToString(), "");
                        }
                       
                    }
                   
                    Sum();
                    DataRow newrow1 = dt.NewRow();
                    dt.Rows.Add(newrow1);


                    dgledgerTrns.Columns["DEBIT"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
                    dgledgerTrns.Columns["CREDIT"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
                    dgledgerTrns.Columns["MBAL"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
                    dgledgerTrns.Columns["CBAL"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
                   // OpeningbalanceShowGrid();
                    dgledgerTrns.FirstDisplayedScrollingRowIndex = dgledgerTrns.RowCount - 1;

                    for (int i = 0; i < dgledgerTrns.Rows.Count; i++)
                    {
                        string vr, cr;

                        DataTable dtCloned = dt.Clone();
                        dtCloned.Columns[6].DataType = typeof(string);
                        dtCloned.Columns[7].DataType = typeof(string);
                        vr = dgledgerTrns.Rows[i].Cells[6].Value.ToString(); // here you go vr = the value of the cel
                        cr = dgledgerTrns.Rows[i].Cells[7].Value.ToString();
                        if (vr == "0.00") // you can check for anything
                        {

                            dgledgerTrns.Rows[i].Cells[6].Value = DBNull.Value;
                            // you can format this cell 
                        }
                        if (cr == "0.00") // you can check for anything
                        {

                            dgledgerTrns.Rows[i].Cells[7].Value = DBNull.Value;
                            // you can format this cell 
                        }
                    }
                  




                }
                catch (Exception ee)
                {
                    //   MessageBox.Show(ee.Message);
                    string st = ee.Message;
                }
            }
        }

        private void dgledgerTrns_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgledgerTrns_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int ID = ledgeridfordrilling;
                int year = int.Parse(dgledgerTrns.Rows[e.RowIndex].Cells["DATED"].Value.ToString());
                string month = dgledgerTrns.Rows[e.RowIndex].Cells["MONT"].Value.ToString();
                Accounts.LedgerReport Lgrpt = new LedgerReport(ID,year,month);

                if (lg.Theme == "1")
                {
                    ComponentFactory.Krypton.Navigator.KryptonPage kp = new ComponentFactory.Krypton.Navigator.KryptonPage();
                    mdi.maindocpanel.Pages.Add(kp);

                    Lgrpt.Show();
                    Lgrpt.TopLevel = false;
                    //  splitContainer1.Panel2.Controls.Add(ad);
                    kp.Controls.Add(Lgrpt);
                    Lgrpt.Dock = DockStyle.Fill;
                    kp.Text = Lgrpt.Text;
                    kp.Name = "Ledger Report";
                    // kp.Focus();
                    Lgrpt.FormBorderStyle = FormBorderStyle.None;

                    mdi.maindocpanel.SelectedPage = kp;
                    mdi.onlyhide();
                   
                }
                else
                {
                    Lgrpt.ShowDialog();
                }

            }
            catch(Exception ex)
            {
                string st = ex.Message;
            }
        }
    }
}
