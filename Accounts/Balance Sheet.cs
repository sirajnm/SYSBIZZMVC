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
    public partial class Balance_Sheet : Form
    {
        Class.Transactions Trans = new Class.Transactions();
        Initial mdi = (Initial)Application.OpenForms["Initial"];
        public double cr;
        public double dr;
        public string NetSales, NetPurchase, GrossProfit, GrossLoss;
        double Liability = 0, Asset = 0, Income = 0, Expense = 0, OpeningStock = 0, ClosingStock = 0, SalesReturn = 0, PurchaseReturn = 0, IndirectExpense = 0, Sales = 0, DirectExpense = 0, DirectIncome = 0, IndirectIncome = 0, Purchases = 0, fixedasset = 0, otherliability = 0, profitloss;
        public Balance_Sheet()
        {
            InitializeComponent();

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
        public void GetLedgerUnderLiability()
        {

            try
            {
                DataTable Accgrp = new DataTable();
                Accgrp = Trans.GetAccGroupLoop(11);
                int count = 0;
                for (int j = 0; j < Accgrp.Rows.Count; j++)
                {

                    int grpid = Convert.ToInt16(Accgrp.Rows[j][0].ToString());
                    if (grpid != 12 && grpid!=15)
                    {
                        DataTable dt = new DataTable();
                        dt = Trans.GetLedgerLoop(grpid, 0);
                        double groupvalue = 0;
                        if (dt.Rows.Count > 0)
                        {


                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                DataTable dt1 = new DataTable();

                                dt1 = Trans.LedgerDebitCreditSum1(Convert.ToInt32(dt.Rows[i][0]), Date_From.Value, Date_To.Value);
                                if (dt1.Rows.Count > 0 && dt1.Rows[0][0].ToString() != "")
                                {
                                    string a = dt1.Rows[0][0].ToString();
                                    groupvalue = groupvalue + Convert.ToDouble(dt1.Rows[0][0].ToString());
                                }

                            }
                            //string grptostring;
                            //if (groupvalue < 0)
                            //{
                            //    grptostring = (groupvalue * -1).ToString();
                            //}
                            //else
                            //{
                            //    grptostring = groupvalue.ToString();
                            //}
                            dataLiability.Rows.Add(Accgrp.Rows[j][1].ToString(), groupvalue.ToString("n2"));
                            dataLiability.Rows[dataLiability.Rows.Count - 1].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                            count++;
                        }
                        otherliability = otherliability + groupvalue;
                    }
                }
                //  dgDr.Rows.Add("Direct Expense", "                         " + DirectExpense);
                //  DataGridViewRow row = new DataGridViewRow();
                //  dataLiability.Rows.Insert(dataLiability.Rows.Count - count, "Current Liability", Liability.ToString());
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
        }
        public void GetLedgerUnderCurrentLiability()
        {

            try
            {
                DataTable Accgrp = new DataTable();
                Accgrp = Trans.GetAccGroupLoop(12);
                int count = 0;
                for (int j = 0; j < Accgrp.Rows.Count; j++)
                {
                    int grpid = Convert.ToInt16(Accgrp.Rows[j][0].ToString());

                    DataTable dt = new DataTable();
                    dt = Trans.GetLedgerLoop(grpid, 0);
                    double groupvalue = 0;
                    if (dt.Rows.Count > 0)
                    {


                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DataTable dt1 = new DataTable();

                            dt1 = Trans.LedgerCreditDebitSum(Convert.ToInt32(dt.Rows[i][0]), Date_From.Value, Date_To.Value);
                            if (dt1.Rows.Count > 0 && dt1.Rows[0][0].ToString() != "")
                            {
                                string a = dt1.Rows[0][0].ToString();
                                groupvalue = groupvalue + Convert.ToDouble(dt1.Rows[0][0].ToString());
                            }

                        }
                        //string grptostring;
                        //if (groupvalue < 0)
                        //{
                        //    grptostring = (groupvalue * -1).ToString();
                        //}
                        //else
                        //{
                        //    grptostring = groupvalue.ToString();
                        //}
                        dataLiability.Rows.Add("  " + Accgrp.Rows[j][1].ToString(), groupvalue.ToString("n2"));
                        count++;
                    }
                    Liability = Liability + groupvalue;
                }
                //  dgDr.Rows.Add("Direct Expense", "                         " + DirectExpense);
                //  DataGridViewRow row = new DataGridViewRow();
                dataLiability.Rows.Insert(dataLiability.Rows.Count - count, "Current Liability", Liability.ToString("n2"));
                dataLiability.Rows[dataLiability.Rows.Count - count - 1].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleRight;

            }

            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
        }

        public void GetLedgerUnderCurrentAsset()
        {

            try
            {
                DataTable Accgrp = new DataTable();
                Accgrp = Trans.GetAccGroupLoop(9);
                int count = 0;
                for (int j = 0; j < Accgrp.Rows.Count; j++)
                {
                    int grpid = Convert.ToInt16(Accgrp.Rows[j][0].ToString());

                    DataTable dt = new DataTable();
                    dt = Trans.GetLedgerLoop(grpid, 0);
                    double groupvalue = 0;
                    if (dt.Rows.Count > 0)
                    {


                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DataTable dt1 = new DataTable();

                            dt1 = Trans.LedgerDebitCreditSum(Convert.ToInt32(dt.Rows[i][0]), Date_From.Value, Date_To.Value);
                            if (dt1.Rows.Count > 0 && dt1.Rows[0][0].ToString() != "")
                            {
                                string a = dt1.Rows[0][0].ToString();
                                groupvalue = groupvalue + Convert.ToDouble(dt1.Rows[0][0].ToString());
                            }

                        }
                        //string grptostring;
                        //if (groupvalue < 0)
                        //{
                        //    grptostring = (groupvalue * -1).ToString("n2");
                        //}
                        //else
                        //{
                        //    grptostring = groupvalue.ToString("n2");
                        //}
                        dataAsset.Rows.Add("  " + Accgrp.Rows[j][1].ToString(), groupvalue.ToString("n2"));
                        count++;
                    }
                    Asset = Asset + groupvalue;
                }
                //  dgDr.Rows.Add("Direct Expense", "                         " + DirectExpense);
                //  DataGridViewRow row = new DataGridViewRow();
                dataAsset.Rows.Insert(dataAsset.Rows.Count - count, "Current Asset", Asset.ToString("n2"));
                dataAsset.Rows[dataAsset.Rows.Count - count - 1].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
        }


        public void GetLedgerUnderFixedAsset()
        {

            try
            {
                DataTable Accgrp = new DataTable();
                Accgrp = Trans.GetAccGroupLoop(16);
                int count = 0;

                // int grpid = Convert.ToInt32(Accgrp.Rows[j][0].ToString());

                DataTable dta = new DataTable();
                //    dta = Trans.GetLedgerLoop(grpid,0);
                dta = Trans.GetLedgerLoop(16, 24);
                for (int j = 0; j < dta.Rows.Count; j++)
                {

                    double groupvalue = 0;
                    if (dta.Rows.Count > 0)
                    {
                        string a = null; ;

                        //for (int i = 0; i < dta.Rows.Count; i++)
                        // {
                        DataTable dt2 = new DataTable();

                        dt2 = Trans.LedgerDebitCreditSum2(Convert.ToInt32(dta.Rows[j][0]), Date_From.Value, Date_To.Value);
                        a = dt2.Rows[0][0].ToString();
                        if (dt2.Rows.Count > 0 && dt2.Rows[0][0].ToString() != "")
                        {

                            groupvalue = groupvalue + Convert.ToDouble(dt2.Rows[0][0].ToString());
                        }

                        dataAsset.Rows.Add("  " + dta.Rows[j][1].ToString(), a.ToString());
                        count++;
                        // }



                    }
                    fixedasset = fixedasset + groupvalue;

                }
                //  dgDr.Rows.Add("Direct Expense", "                         " + DirectExpense);
                //  DataGridViewRow row = new DataGridViewRow();
                //  dataAsset.Rows.Add("  ",fixedasset.ToString("n2"));
                dataAsset.Rows.Insert(dataAsset.Rows.Count - count, "Fixed Asset", fixedasset.ToString("n2"));
                dataAsset.Rows[dataAsset.Rows.Count - count - 1].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleRight;

                ClosingStocks();
                dataAsset.Rows.Insert(dataAsset.Rows.Count, "Clossing Stock", ClosingStock.ToString("n2"));
                dataAsset.Rows[dataAsset.Rows.Count - count - 1].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
        }

        public void GetProfitAndLossAccount()
        {

            GetPurchase();
            GetSales();
            GetPurchaseReturn();
            GetSalesReturn();
            GetLedgerUnderDirectExpense();
            GetLedgerUnderDirectIncome();
            GetLedgerUnderIndirectIncome();
            GetLedgerUnderIndirectExpense();
            ClosingStocks();
            GetOpeningStock();

            NetSales = (Convert.ToDouble(Sales) - Convert.ToDouble(SalesReturn)).ToString();
            NetPurchase = (Convert.ToDouble(Purchases) - Convert.ToDouble(PurchaseReturn)).ToString();
            GrossProfit = (((Convert.ToDouble(NetSales) + Convert.ToDouble(ClosingStock) + Convert.ToDouble(DirectIncome)) - (Convert.ToDouble(OpeningStock) + Convert.ToDouble(NetPurchase) + Convert.ToDouble(DirectExpense)))).ToString();
            GrossLoss = (Math.Abs(((Convert.ToDouble(NetSales) + Convert.ToDouble(ClosingStock) + Convert.ToDouble(DirectIncome)) - (Convert.ToDouble(OpeningStock) + Convert.ToDouble(NetPurchase) + Convert.ToDouble(DirectExpense))))).ToString();


            double final;


            double ans = Convert.ToDouble(GrossProfit);

            if (ans > 0)
            {
                cr = Convert.ToDouble(GrossProfit) + Convert.ToDouble((IndirectIncome));
                dr = Convert.ToDouble((IndirectExpense));
                profitloss = (cr - dr);

                dataLiability.Rows.Add("Net profit", profitloss.ToString("n2"));
                dataLiability.Rows[dataLiability.Rows.Count - 1].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleRight;

            }
            else if (ans < 0)
            {

                dr = Convert.ToDouble(GrossLoss) + Convert.ToDouble((IndirectExpense));
                cr = Convert.ToDouble((IndirectIncome));
                final = Convert.ToDouble(dr - cr);
                dataLiability.Rows.Add("Net loss", final.ToString("n2"));
                dataLiability.Rows[dataLiability.Rows.Count - 1].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            else
            {

            }
        }


        //Profit and loss starts
        public void GetOpeningStock()
        {
            try
            {

                DataTable dt = new DataTable();
                dt = Trans.GetOpeningStock(Date_From.Value, Date_To.Value);
                if (dt.Rows.Count > 0)
                {
                    OpeningStock = Convert.ToDouble(dt.Rows[0][0]);
                }
                else
                {
                    OpeningStock = 0.00;
                }
            }
            catch
            {
                OpeningStock = 0.00;
            }



        }
        public void ClosingStocks()
        {
            try
            {

                //DataTable dt = new DataTable();
                //dt = Trans.ClosingStock(Date_From.Value, Date_To.Value);
                //if (dt.Rows.Count > 0)
                //{
                //    ClosingStock = Convert.ToDouble(dt.Rows[0][0].ToString());
                //}
                //else
                //{
                //    ClosingStock = 0.00;
                //}
                ClosingStock = Trans.TOTALSTOCK();
            }
            catch
            {
                ClosingStock = 0.00;
            }


        }

        public void GetLedgerUnderDirectExpense()
        {

            try
            {
                DataTable dt = new DataTable();
                dt = Trans.GetLedgerLoop(7, 17);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataTable dt1 = new DataTable();

                        dt1 = Trans.LedgerDebitCreditSum(Convert.ToInt32(dt.Rows[i][0]), Date_From.Value, Date_To.Value);
                        if (dt1.Rows.Count > 0 && dt1.Rows[0][0].ToString() != "")
                        {
                            string a = dt1.Rows[0][0].ToString();
                            DirectExpense = DirectExpense + Convert.ToDouble(dt1.Rows[0][0].ToString());
                        }
                    }

                }


            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
        }
        public void GetLedgerUnderDirectIncome()
        {

            try
            {
                DataTable dt = new DataTable();
                dt = Trans.GetLedgerLoop(6, 18);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataTable dt1 = new DataTable();

                        dt1 = Trans.LedgerDebitCreditSum1(Convert.ToInt32(dt.Rows[i][0]), Date_From.Value, Date_To.Value);
                        if (dt1.Rows.Count > 0 && dt1.Rows[0][0].ToString() != "")
                        {
                            string a = dt1.Rows[0][0].ToString();
                            DirectIncome = DirectIncome + Convert.ToDouble(dt1.Rows[0][0].ToString());
                        }
                    }

                }


            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
        }
        public void GetLedgerUnderIndirectIncome()
        {

            try
            {
                DataTable dt = new DataTable();
                dt = Trans.GetLedgerLoop(51, 0);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataTable dt1 = new DataTable();

                        dt1 = Trans.LedgerDebitCreditSum1(Convert.ToInt32(dt.Rows[i][0]), Date_From.Value, Date_To.Value);
                        if (dt1.Rows.Count > 0 && dt1.Rows[0][0].ToString() != "")
                        {
                            string a = dt1.Rows[0][0].ToString();
                            IndirectIncome = IndirectIncome + Convert.ToDouble(dt1.Rows[0][0].ToString());
                        }
                    }

                }


            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
        }
        public void GetLedgerUnderIndirectExpense()
        {

            try
            {
                DataTable dt = new DataTable();
                dt = Trans.GetLedgerLoop(27, 0);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataTable dt1 = new DataTable();

                        dt1 = Trans.LedgerDebitCreditSum(Convert.ToInt32(dt.Rows[i][0]), Date_From.Value, Date_To.Value);
                        if (dt1.Rows.Count > 0 && dt1.Rows[0][0].ToString() != "")
                        {
                            string a = dt1.Rows[0][0].ToString();
                            IndirectExpense = IndirectExpense + Convert.ToDouble(dt1.Rows[0][0].ToString());
                        }
                    }

                }


            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
        }
        public void GetPurchaseReturn()
        {
            try
            {

                DataTable dt = new DataTable();
                dt = Trans.PurchaseReturn(Date_From.Value, Date_To.Value);
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0][0].ToString() == "")
                    {
                        PurchaseReturn = 0.00;
                    }
                    else
                    {
                        PurchaseReturn = Convert.ToDouble(dt.Rows[0][0].ToString());
                    }
                }
                else
                {
                    PurchaseReturn = 0.00;
                }
            }
            catch
            {
                PurchaseReturn = 0.00;
            }

            //dgDr.Rows.Add("Purchase Return", PurchaseReturn);
        }
        public void GetPurchase()
        {
            try
            {

                DataTable dt = new DataTable();
                dt = Trans.Purchase(Date_From.Value, Date_To.Value);
                if (dt.Rows.Count > 0)
                {
                    Purchases = Convert.ToDouble(dt.Rows[0][0].ToString());
                }
                else
                {
                    Purchases = 0.00;
                }
            }
            catch
            {
                Purchases = 0.00;
            }


        }
        public void GetSales()
        {
            try
            {

                DataTable dt = new DataTable();
                dt = Trans.GetSales(Date_From.Value, Date_To.Value);
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0][0].ToString() == "")
                    {
                        Sales = 0.00;
                    }
                    else
                    {
                        Sales = Convert.ToDouble(dt.Rows[0][0].ToString());

                    }
                }
                else
                {
                    Sales = 0.00;
                }
            }
            catch
            {
                Sales = 0.00;
            }


        }
        public void GetSalesReturn()
        {
            try
            {

                DataTable dt = new DataTable();
                dt = Trans.GetSalesRetunr(Date_From.Value, Date_To.Value);
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0][0].ToString() == "")
                    {
                        SalesReturn = 0;
                    }
                    else
                    {
                      
                        SalesReturn = Convert.ToDouble(dt.Rows[0][0].ToString());
                    }
                }
                else
                {
                    SalesReturn = 0.00;
                }
            }
            catch
            {
                SalesReturn = 0.00;
            }

        }
        //profit and loss ends
        public void AddLiability()
        {
            dataLiability.Rows.Add("", "");
            dataAsset.Rows.Add("", "");
            GetLedgerUnderCurrentLiability();
            GetLedgerUnderLiability();
            GetLedgerUnderCurrentAsset();
            GetProfitAndLossAccount();
            GetLedgerUnderFixedAsset();
            balancecolumns();
            dataAsset.Rows.Add("", "");
            dataLiability.Rows.Add("", "");
            printtotal();
            this.dataAsset.RowsDefaultCellStyle.BackColor = Color.White;
            this.dataAsset.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;
            this.dataLiability.RowsDefaultCellStyle.BackColor = Color.White;
            this.dataLiability.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;
        }


        public void balancecolumns()
        {
            int liabilityrows = dataLiability.Rows.Count;
            int assetrows = dataAsset.Rows.Count;
            if (liabilityrows < assetrows)
            {
                while (dataLiability.Rows.Count != assetrows)
                {
                    dataLiability.Rows.Add("", "");
                }

            }

            if (liabilityrows > assetrows)
            {
                while (dataAsset.Rows.Count != liabilityrows)
                {
                    dataAsset.Rows.Add("", "");
                }

            }
        }

        public void printtotal()
        {
            try
            {
                if (cr > dr)
                {
                    double profitloss = cr - dr;
                    double finalLia = Liability + otherliability + profitloss;
                    double finalasset = Asset + fixedasset + ClosingStock;
                    dataLiability.Rows.Add("TOTAL", finalLia.ToString("n2"));
                    dataLiability.Rows[dataLiability.Rows.Count - 1].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleRight;

                    dataAsset.Rows.Add("TOTAL", finalasset.ToString("n2"));
                    dataAsset.Rows[dataAsset.Rows.Count - 1].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                else if (cr < dr)
                {
                    double profitloss = dr - cr;
                    double finalLia = Liability + otherliability - profitloss;
                    double finalasset = Asset + fixedasset + ClosingStock;
                    dataLiability.Rows.Add("TOTAL", finalLia.ToString("n2"));
                    dataLiability.Rows[dataLiability.Rows.Count - 1].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleRight;

                    dataAsset.Rows.Add("TOTAL", finalasset.ToString("n2"));
                    dataAsset.Rows[dataAsset.Rows.Count - 1].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                else
                {
                    double profitloss = dr - cr;
                    double finalLia = Liability + otherliability - profitloss;
                    double finalasset = Asset + fixedasset+ClosingStock;
                    dataLiability.Rows.Add("TOTAL", finalLia.ToString("n2"));
                    dataLiability.Rows[dataLiability.Rows.Count - 1].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleRight;

                    dataAsset.Rows.Add("TOTAL", finalasset.ToString("n2"));
                    dataAsset.Rows[dataAsset.Rows.Count - 1].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }
            catch
            { }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            lbltitle.Text = "Balance Sheet as on  " + Date_To.Value.ToShortDateString();
            btnClear.PerformClick();
            AddLiability();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            dataLiability.Rows.Clear();
            dataAsset.Rows.Clear();
            Liability = 0;
            Asset = 0;
            Income = 0;
            Expense = 0;
            ClosingStock = 0;
            OpeningStock = 0;
            SalesReturn = 0;
            PurchaseReturn = 0;
            IndirectExpense = 0;
            Sales = 0;
            DirectExpense = 0;
            DirectIncome = 0;
            IndirectIncome = 0;
            Purchases = 0;
            otherliability = 0;
            fixedasset = 0;
            profitloss = 0;
        }

        private void Date_From_ValueChanged(object sender, EventArgs e)
        {
            Date_To.Value = Date_From.Value.AddDays(364);
        }

        private void Balance_Sheet_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            Class.CompanySetup ComSet = new Class.CompanySetup();
            ComSet.Status = true;
            dt = ComSet.GetCurrentFinancialYear();

            DateTime end = Convert.ToDateTime(dt.Rows[0]["EDate"]);
            DateTime start = Convert.ToDateTime(dt.Rows[0]["SDate"]);
            lbltitle.Text = "Balance Sheet as on " + end.ToShortDateString();
            Date_From.Value = start;
            Date_To.Value = end;
        }
    }
}