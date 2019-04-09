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
    public partial class Trading_PL : Form
    {
        Class.Transactions Trans = new Class.Transactions();
        Class.CompanySetup Comstp = new Class.CompanySetup();
        string Sales, SalesReturn, NetSales, OpeningStock, Purchases, CarriageIn, PurchaseReturn, NetPurchase, ClosingStock, GoodsSold,GrossProfit;
        double DirectExpense=0,IndirectExpense=0,DirectIncome=0,IndirectIncome=0;
        int index;
        int flag = 0;
        Initial mdi = (Initial)Application.OpenForms["Initial"];
        public Trading_PL()
        {
            InitializeComponent();
        }

        public static string DoFormat(double myNumber)
        {
            var s = string.Format("{0:0.00}", myNumber);

            if (s.EndsWith("00"))
            {
                return ((int)myNumber).ToString();
            }
            else
            {
                return s;
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
        private void Trading_PL_Load(object sender, EventArgs e)
        {

            DataTable dt = new DataTable();
            Comstp.Status = true;
            dt = Comstp.GetFinancialYear();
            if (dt.Rows.Count > 0)
            {
                Date_From.Value = Convert.ToDateTime(dt.Rows[0][1]);
                Date_To.Value = Convert.ToDateTime(dt.Rows[0][2]);
                lbltitle.Text = "Trading,Profit and Loss Account as on " + Date_To.Value.ToShortDateString();
            }
        }


        public void GetOpeningStock()
        {
            try
            {

                DataTable dt = new DataTable();
                dt = Trans.GetOpeningStock(Date_From.Value, Date_To.Value);
                if (dt.Rows.Count > 0)
                {

                    if (dt.Rows[0][0].ToString() == "")
                    {
                        SalesReturn = "0.00";
                    }
                    else
                    {
                        OpeningStock = DoFormat(Convert.ToDouble(dt.Rows[0][0]));
                    }
                }
                else
                {
                    OpeningStock = "0.00";
                }
            }
            catch
            {
                OpeningStock = "0.00";
            }


            dgDr.Rows.Add("Opening Stock", "                         " + OpeningStock);
            index = dgDr.Rows.Count - 1;

            dgDr.Rows[index].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
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
                        SalesReturn = "0.00";
                    }
                    else
                    {
                        PurchaseReturn = DoFormat(Convert.ToDouble(dt.Rows[0][0].ToString()));
                    }
                }
                else
                {
                    PurchaseReturn = "0.00";
                }
            }
            catch
            {
                PurchaseReturn = "0.00";
            }

            dgDr.Rows.Add("Purchase Return", PurchaseReturn);
        }


        public void GetPurchase()
        {
            try
            {

                DataTable dt = new DataTable();
                dt = Trans.Purchase(Date_From.Value, Date_To.Value);
                if (dt.Rows.Count > 0)
                {

                    if (dt.Rows[0][0].ToString() == "")
                    {
                        SalesReturn = "0.00";
                    }
                    else
                    {
                        Purchases =DoFormat(Convert.ToDouble( dt.Rows[0][0].ToString()));
                    }
                }
                else
                {
                    Purchases = "0.00";
                }
            }
            catch
            {
                Purchases = "0.00";
            }

            dgDr.Rows.Add("Purchase", Purchases);
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
                        SalesReturn = "0.00";
                    }
                    else
                    {
                        Sales =DoFormat(Convert.ToDouble(dt.Rows[0][0].ToString()));
                    }
                }
                else
                {
                    Sales = "0.00";
                }
            }
            catch
            {
                Sales = "0.00";
            }

            dgCr.Rows.Add("Sales", Sales);
        }


        public void GetSalesReturn()
        {
            try
            {

                DataTable dt = new DataTable();
                dt = Trans.GetSalesRetunr(Date_From.Value, Date_To.Value);
                if (dt.Rows.Count > 0)
                {
                    if(dt.Rows[0][0].ToString()=="")
                    {
                     SalesReturn = "0.00";
                    }
                    else
                    {
                    SalesReturn = DoFormat(Convert.ToDouble(dt.Rows[0][0].ToString()));
                    }
                }
                else
                {
                    SalesReturn = "0.00";
                }
            }
            catch
            {
                SalesReturn = "0.00";
            }

            dgCr.Rows.Add("Sales Return", SalesReturn);

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
                        if (dt1.Rows.Count>0 && dt1.Rows[0][0].ToString()!="")
                        {
                            string a = dt1.Rows[0][0].ToString();
                            DirectExpense = DirectExpense + Convert.ToDouble(dt1.Rows[0][0].ToString());
                        }
                    }

                }

                dgDr.Rows.Add("Direct Expense","                         "+ DirectExpense);
                index = dgDr.Rows.Count-1;

                dgDr.Rows[index].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            catch(Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
        }
        public void GetLedgerUnderINDirectExpense()
        {

            try
            {
                DataTable dt = new DataTable();
                dt = Trans.GetLedgerLoop(27, 28);
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

                dgDr.Rows.Add("InDirect Expense", "                         " + IndirectExpense);
                index = dgDr.Rows.Count - 1;

                dgDr.Rows[index].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
        }
        public void GetLedgerUnderINDirectIncome()
        {

            try
            {
                DataTable dt = new DataTable();
                dt = Trans.GetLedgerLoop(51, 58);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataTable dt1 = new DataTable();

                        dt1 = Trans.LedgerCreditDebitSum(Convert.ToInt32(dt.Rows[i][0]), Date_From.Value, Date_To.Value);
                        if (dt1.Rows.Count > 0 && dt1.Rows[0][0].ToString() != "")
                        {
                            string a = dt1.Rows[0][0].ToString();
                            IndirectIncome = IndirectIncome + Convert.ToDouble(dt1.Rows[0][0].ToString());
                        }
                    }

                }

                dgCr.Rows.Add("InDirect Income", "                                                              " + IndirectIncome);
                index = dgCr.Rows.Count - 1;

                dgDr.Rows[index].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
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

                        dt1 = Trans.LedgerDebitCreditSum(Convert.ToInt32(dt.Rows[i][0]), Date_From.Value, Date_To.Value);
                        if (dt1.Rows.Count > 0 && dt1.Rows[0][0].ToString() != "")
                        {
                            string a = dt1.Rows[0][0].ToString();
                            DirectIncome = DirectIncome + Convert.ToDouble(dt1.Rows[0][0].ToString());
                        }
                    }

                }

                dgCr.Rows.Add("Direct Income", "                         " + DirectIncome);

                index = dgCr.Rows.Count-1 ;

                dgCr.Rows[index].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
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

                        dt1 = Trans.LedgerDebitCreditSum(Convert.ToInt32(dt.Rows[i][0]),Date_From.Value,Date_To.Value);
                        if (dt1.Rows.Count > 0 && dt1.Rows[0][0].ToString() != "")
                        {
                            string a = dt1.Rows[0][0].ToString();
                            IndirectIncome = IndirectIncome + Convert.ToDouble(dt1.Rows[0][0].ToString());
                        }
                    }

                }

                dgCr.Rows.Add("Indirect Income", "                         " + IndirectIncome);
                index = dgCr.Rows.Count -1;

                dgCr.Rows[index].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
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

                dgDr.Rows.Add("Indirect Expense", "                         " + IndirectExpense);
                index = dgDr.Rows.Count - 1;

                dgDr.Rows[index].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
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

                //    if (dt.Rows[0][0].ToString() == "")
                //    {
                //        SalesReturn = "0.00";
                //    }
                //    else
                //    {
                //        ClosingStock = DoFormat(Convert.ToDouble(dt.Rows[0][0].ToString()));
                //    }
                //}
                //else
                //{
                //    ClosingStock = "0.00";
                //}
                ClosingStock =DoFormat(Trans.TOTALSTOCK());
            }
            catch
            {
                ClosingStock = "0.00";
            }

            dgCr.Rows.Add("Closing Stock", "                         " + ClosingStock);
            index = dgCr.Rows.Count-1;

            dgCr.Rows[index].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleRight;

        }

        
        private void btnSave_Click(object sender, EventArgs e)
        {
            #region

            //try
            //{
            //    DataGridViewRow row = new DataGridViewRow();

            //    GetOpeningStock();
            //    ClosingStocks();
            //    GetSales();
            //    GetSalesReturn();
            //    NetSales = (Convert.ToDouble(Sales) - Convert.ToDouble(SalesReturn)).ToString();
            //    dgCr.Rows.Add("", "                         " + NetSales);
            //    index = dgCr.Rows.Count - 1;
            //    dgCr.Rows[index].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleRight;



            //    dgCr.Rows.Add("", "");



            //    GetPurchase();
            //    GetPurchaseReturn();



            //    NetPurchase = (Convert.ToDouble(Purchases) - Convert.ToDouble(PurchaseReturn)).ToString();
            //    dgDr.Rows.Add("", "                         " + NetPurchase);
            //    index = dgCr.Rows.Count - 1;


            //    dgCr.Rows[index].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            //    index = dgDr.Rows.Count - 1;
            //    dgDr.Rows[index].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleRight;

            //    dgDr.Rows.Add("", "");

            //    GetLedgerUnderDirectExpense();
            //    GetLedgerUnderDirectIncome();

            //    dgDr.Rows.Add("", "");
            //    dgCr.Rows.Add("", "");

            //    GrossProfit = DoFormat(((Convert.ToDouble(NetSales) + Convert.ToDouble(ClosingStock) + Convert.ToDouble(DirectIncome)) - (Convert.ToDouble(OpeningStock) + Convert.ToDouble(NetPurchase) + Convert.ToDouble(DirectExpense)))).ToString();
            //    dgDr.Rows.Add("Gross Proft", "                         " + GrossProfit);
            //    index = dgDr.Rows.Count - 1;
            //    dgDr.Rows[index].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            //    dgDr.Rows[index].Cells[1].Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
            //    dgDr.Rows[index].Cells[0].Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);

            //    dgCr.Rows.Add("", "");

            //    dgCr.Rows.Add("", "                         " + (Convert.ToDouble(NetSales) + Convert.ToDouble(ClosingStock) + Convert.ToDouble(DirectIncome)).ToString());
            //    index = dgCr.Rows.Count - 1;
            //    dgCr.Rows[index].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            //    dgCr.Rows[index].Cells[1].Style.BackColor = Color.Cyan;

            //    dgDr.Rows.Add("", "                         " + (Convert.ToDouble(OpeningStock) + Convert.ToDouble(NetPurchase) + Convert.ToDouble(DirectExpense) + Convert.ToDouble(GrossProfit)).ToString());
            //    index = dgDr.Rows.Count - 1;
            //    dgDr.Rows[index].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            //    dgDr.Rows[index].Cells[1].Style.BackColor = Color.Cyan;



            //    //profit and loss account calculation
            //    dgCr.Rows.Add("", "");
            //    dgDr.Rows.Add("", "");

            //    GetLedgerUnderIndirectIncome();
            //    GetLedgerUnderIndirectExpense();


            //    dgCr.Rows.Add("Total Income", DoFormat(Convert.ToDouble(IndirectIncome) + (Convert.ToDouble(NetSales) + Convert.ToDouble(ClosingStock) + Convert.ToDouble(DirectIncome))));
            //    index = dgCr.Rows.Count - 1;
            //    dgCr.Rows[index].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleRight;


            //    dgDr.Rows.Add("Total Expense", (Convert.ToDouble(IndirectExpense) + (Convert.ToDouble(OpeningStock) + Convert.ToDouble(NetPurchase) + Convert.ToDouble(DirectExpense) + Convert.ToDouble(GrossProfit))) - Convert.ToDouble(GrossProfit));
            //    index = dgDr.Rows.Count - 1;
            //    dgDr.Rows[index].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleRight;

            //    double FinalValue = Convert.ToDouble(GrossProfit) + Convert.ToDouble(DoFormat(IndirectIncome));
            //    if (FinalValue < 0)
            //    {
            //        dgDr.Rows.Add("Net Loss", Math.Abs(Convert.ToDouble(GrossProfit) + Convert.ToDouble(DoFormat(IndirectIncome))));
            //        dgCr.Rows.Add("", "");
            //        index = dgDr.Rows.Count - 1;

            //        dgDr.Rows[index].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            //        dgDr.Rows[index].Cells[1].Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
            //        dgDr.Rows[index].Cells[0].Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
            //    }
            //    else
            //    {
            //        dgCr.Rows.Add("Net Profit", Convert.ToDouble(GrossProfit) + Convert.ToDouble(DoFormat(IndirectIncome)));
            //        dgDr.Rows.Add("", "");
            //        index = dgDr.Rows.Count - 1;

            //        dgCr.Rows[index].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            //        dgCr.Rows[index].Cells[1].Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
            //        dgCr.Rows[index].Cells[0].Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
            //    }











            //    this.dgCr.RowsDefaultCellStyle.BackColor = Color.White;
            //    this.dgCr.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;

            //    this.dgDr.RowsDefaultCellStyle.BackColor = Color.White;
            //    this.dgDr.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;
            //}
            //catch (Exception ee)
            //{
            //    MessageBox.Show("Exception occured the datas you see may be incorrect");
            //}
            #endregion
            try
            {

                if (flag == 0)
                {
                    flag = flag + 1;
                    DataGridViewRow row = new DataGridViewRow();

                    GetOpeningStock();
                    ClosingStocks();
                    GetSales();
                    GetSalesReturn();
                    NetSales = (Convert.ToDouble(Sales) - Convert.ToDouble(SalesReturn)).ToString();
                    dgCr.Rows.Add("", "                         " + NetSales);
                    index = dgCr.Rows.Count - 1;
                    dgCr.Rows[index].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleRight;



                   // dgCr.Rows.Add("", "");



                    GetPurchase();
                    GetPurchaseReturn();



                    NetPurchase = (Convert.ToDouble(Purchases) - Convert.ToDouble(PurchaseReturn)).ToString();
                    dgDr.Rows.Add("", "                         " + NetPurchase);
                    index = dgCr.Rows.Count - 1;


                    dgCr.Rows[index].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    index = dgDr.Rows.Count - 1;
                    dgDr.Rows[index].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleRight;

                    //dgDr.Rows.Add("", "");

                    GetLedgerUnderDirectExpense();
                    GetLedgerUnderDirectIncome();


                  //  dgDr.Rows.Add("", "");
                  //  dgCr.Rows.Add("", "");
                    string GrossLoss = "0";

                    GrossProfit = DoFormat(((Convert.ToDouble(NetSales) + Convert.ToDouble(ClosingStock) + Convert.ToDouble(DirectIncome)) - (Convert.ToDouble(OpeningStock) + Convert.ToDouble(NetPurchase) + Convert.ToDouble(DirectExpense)))).ToString();
                    if (Convert.ToDouble(GrossProfit) > 0)
                    {
                        dgDr.Rows.Add("Gross Profit   ", "                         " + GrossProfit);
                        index = dgDr.Rows.Count - 1;
                        dgDr.Rows[index].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dgDr.Rows[index].Cells[1].Style.Font = new Font("Tahoma", 12,FontStyle.Bold);
                        dgDr.Rows[index].Cells[0].Style.Font = new Font("Tahoma", 12, FontStyle.Bold);

                        dgCr.Rows.Add("", "");
                        dgDr.Rows.Add("", "                         " + (Convert.ToDouble(ClosingStock) + Convert.ToDouble(NetSales) + Convert.ToDouble(DirectIncome)).ToString());
                        index = dgDr.Rows.Count - 1;
                        dgDr.Rows[index].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dgDr.Rows[index].Cells[1].Style.BackColor = Color.LightSkyBlue;
                        dgCr.Rows.Add("", "                         " + (Convert.ToDouble(ClosingStock) + Convert.ToDouble(NetSales) + Convert.ToDouble(DirectIncome)).ToString());
                        index = dgDr.Rows.Count - 1;
                        dgCr.Rows[index].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dgCr.Rows[index].Cells[1].Style.BackColor = Color.CadetBlue;
                        dgDr.Rows[index].Cells[1].Style.BackColor = Color.CadetBlue;
                        dgDr.Rows[index].Cells[1].Style.ForeColor = Color.White;
                        dgCr.Rows[index].Cells[1].Style.ForeColor = Color.White;
                       
                    }
                    else if (Convert.ToDouble(GrossProfit) < 0)
                    {
                        GrossLoss = DoFormat(Math.Abs(((Convert.ToDouble(NetSales) + Convert.ToDouble(ClosingStock) + Convert.ToDouble(DirectIncome)) - (Convert.ToDouble(OpeningStock) + Convert.ToDouble(NetPurchase) + Convert.ToDouble(DirectExpense))))).ToString();
                        dgCr.Rows.Add("Gross Loss   ", "                         " + GrossLoss.ToString());
                       
                        index = dgCr.Rows.Count - 1;
                        dgCr.Rows[index].Cells[0].Style.Font = new Font("Tahoma", 12, FontStyle.Bold);
                        dgCr.Rows[index].Cells[1].Style.Font = new Font("Tahoma", 12, FontStyle.Bold);
                        
                        dgCr.Rows[index].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dgCr.Rows[index].Cells[1].Style.BackColor = Color.LightSkyBlue;
                        dgDr.Rows.Add("", "");

                        //total
                        dgDr.Rows.Add("", "                         " + (Convert.ToDouble(OpeningStock) + Convert.ToDouble(NetPurchase) + Convert.ToDouble(DirectExpense)).ToString());
                        index = dgDr.Rows.Count - 1;
                        dgDr.Rows[index].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dgDr.Rows[index].Cells[1].Style.BackColor = Color.PaleTurquoise;
                        dgCr.Rows.Add("", "                         " + (Convert.ToDouble(OpeningStock) + Convert.ToDouble(NetPurchase) + Convert.ToDouble(DirectExpense)).ToString());
                        index = dgDr.Rows.Count - 1;
                        dgCr.Rows[index].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dgCr.Rows[index].Cells[1].Style.BackColor = Color.CadetBlue;
                        dgDr.Rows[index].Cells[1].Style.BackColor = Color.CadetBlue;
                        dgDr.Rows[index].Cells[1].Style.ForeColor = Color.White;
                        dgCr.Rows[index].Cells[1].Style.ForeColor = Color.White;
                    }
                    else
                    {

                    }
                   

                    //profit and loss account calculation
                    dgCr.Rows.Add("", "");
                    dgDr.Rows.Add("", "");

                    if (Convert.ToDouble(GrossProfit) > 0)
                    {
                        dgCr.Rows.Add("Gross Profit   ", "                            " + GrossProfit, FontStyle.Bold);
                        dgCr.Rows[index + 2].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dgDr.Rows.Add("", "");
                    }
                    else if (Convert.ToDouble(GrossProfit) < 0)
                    {
                        dgDr.Rows.Add("Gross Loss   ", "                            " + GrossLoss, FontStyle.Bold);
                        dgDr.Rows[index + 2].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dgCr.Rows.Add("", "");

                    }
                    else
                    {
                        dgDr.Rows.Add("", "");
                        dgCr.Rows.Add("", "");
                    }


                    // GetLedgerUnderIndirectIncome();
                    //GetLedgerUnderIndirectExpense();

                    GetLedgerUnderINDirectExpense();
                    GetLedgerUnderINDirectIncome();

                    //  dgCr.Rows.Add("Total Income", DoFormat(Convert.ToDouble(IndirectIncome)));
                    // index = dgCr.Rows.Count - 1;
                    //   dgCr.Rows[index].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleRight;


                    //  dgDr.Rows.Add("Total Expense", (Convert.ToDouble(IndirectExpense)));
                    //  index = dgDr.Rows.Count - 1;
                    //  dgDr.Rows[index].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleRight;

                    double cr = Convert.ToDouble(GrossProfit) + Convert.ToDouble(DoFormat(IndirectIncome));
                    double dr = Convert.ToDouble(DoFormat(IndirectExpense));
                    double final = Convert.ToDouble(cr - dr);
                    double tt = cr;
                    if (cr > dr)
                    {
                        //  dgDr.Rows.Add("Net Loss",Math.Abs(Convert.ToDouble(GrossProfit) + Convert.ToDouble(DoFormat(IndirectIncome)))); 
                        dgDr.Rows.Add("Net Profit", final);
                        dgCr.Rows.Add("", "");
                        index = dgDr.Rows.Count - 1;
                        tt = cr;

                        dgDr.Rows[index].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dgDr.Rows[index].Cells[1].Style.Font = new Font("Tahoma", 12, FontStyle.Bold);
                        dgDr.Rows[index].Cells[0].Style.Font = new Font("Tahoma", 12, FontStyle.Bold);
                    }
                    else if (cr < dr)
                    {
                        dr = Convert.ToDouble(GrossLoss) + Convert.ToDouble(DoFormat(IndirectExpense));
                        cr = Convert.ToDouble(DoFormat(IndirectIncome));
                        final = Convert.ToDouble(dr - cr);
                        dgCr.Rows.Add("Net Loss", final);
                        dgDr.Rows.Add("", "");
                        index = dgDr.Rows.Count - 1;
                        tt = dr;
                        dgCr.Rows[index].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dgCr.Rows[index].Cells[1].Style.Font = new Font("Tahoma", 12, FontStyle.Bold);
                        dgCr.Rows[index].Cells[0].Style.Font = new Font("Tahoma", 12, FontStyle.Bold);
                    }
                    else
                    {

                    }

                    dgCr.Rows.Add("", "                                   " + tt);
                    dgCr.Rows[index + 1].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgCr.Rows[index + 1].Cells[1].Style.BackColor = Color.CadetBlue;
                    dgCr.Rows[index + 1].Cells[1].Style.ForeColor = Color.White;
                    dgDr.Rows.Add("", "                                   " + tt);
                    dgDr.Rows[index + 1].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgDr.Rows[index + 1].Cells[1].Style.BackColor = Color.CadetBlue;
                    dgDr.Rows[index + 1].Cells[1].Style.ForeColor = Color.White;

                    this.dgCr.RowsDefaultCellStyle.BackColor = Color.LightSkyBlue;
                    this.dgCr.AlternatingRowsDefaultCellStyle.BackColor = Color.LightSkyBlue;

                    this.dgDr.RowsDefaultCellStyle.BackColor = Color.LightSkyBlue;
                    this.dgDr.AlternatingRowsDefaultCellStyle.BackColor = Color.LightSkyBlue;
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("Exception occured the datas you see may be incorrect");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            dgDr.Rows.Clear();
            dgCr.Rows.Clear();
        }



    }
}
