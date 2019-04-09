using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Sys_Sols_Inventory.Model;
using Sys_Sols_Inventory.Class;
namespace Sys_Sols_Inventory.Accounts
{
    public partial class DueBills : Form
    {
       // private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
       // private SqlCommand cmd = new SqlCommand();
       // private SqlDataAdapter adapter = new SqlDataAdapter();
        private DataTable table = new DataTable();
        private DataTable tableType = new DataTable();
        private DataTable tableCurrency = new DataTable();
        private BindingSource source = new BindingSource();
        Class.CompanySetup cset = new Class.CompanySetup();
        
        clsCustomer custObj=new clsCustomer();
        TbTransactionsDB transObj = new TbTransactionsDB();
        Ledgers ldgObj = new Ledgers();
     

        double TotalAmountToPay=0, BalanceToPay=0, TotalPayed=0,PreviousBalance=0;
        public int LedgerId;
        string LedgerType = "Creditors";

        public DueBills()
        {
            InitializeComponent();
        }


        public DueBills(int ledgerid,string Supid)
        {
          
            InitializeComponent(); 
            LedgerId = ledgerid;
          
            
        }

        //[DllImport("User32.dll", CharSet = CharSet.Auto)]
        //public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        //[DllImport("User32.dll")]
        //private static extern IntPtr GetWindowDC(IntPtr hWnd);

        //protected override void WndProc(ref Message m)
        //{
        //    base.WndProc(ref m);
        //    const int WM_NCPAINT = 0x85;
        //    if (m.Msg == WM_NCPAINT)
        //    {
        //        IntPtr hdc = GetWindowDC(m.HWnd);
        //        if ((int)hdc != 0)
        //        {
        //            Graphics g = Graphics.FromHdc(hdc);
        //            g.FillRectangle(Brushes.DarkCyan, new Rectangle(0, 0, 4800, 28));
        //            g.Flush();
        //            ReleaseDC(m.HWnd, hdc);
        //        }
        //    }
        //}



        public void SearchDuebills()
        {
            try
            {
                if (cmbSuppliers.Text != "")
                {
                    GetPurchases();
                    GetPayments();

                    //try
                    //{
                    //    for (int i = 0; i < dgDuebills.Rows.Count; i++)
                    //    {
                    //      //  TotalAmountToPay = TotalAmountToPay + Convert.ToDouble(dgDuebills.Rows[i].Cells["ToAmount"].Value);
                    //        TotalPayed = TotalPayed + Convert.ToDouble(dgDuebills.Rows[i].Cells["PaidAmount"].Value);
                    //    }
                    //}
                    //catch(Exception ee)
                    //{
                    //    MessageBox.Show(ee.Message);
                    //}

                  //  BalanceToPay = TotalAmountToPay - TotalPayed;

                    BalanceToPay = TotalAmountToPay - TotalPayedAmount;
                    dgDuebills.Rows.Add();
                    if (BalanceToPay < 0)
                    {
                        dgDuebills.Rows.Add("", "", "", "Total", TotalAmountToPay.ToString("n2"), TotalPayedAmount.ToString("n2"), BalanceToPay.ToString("n2")+" CR", "");
                    }
                    else
                    {
                        dgDuebills.Rows.Add("", "", "", "Total", TotalAmountToPay.ToString("n2"), TotalPayedAmount.ToString("n2"), BalanceToPay.ToString("n2")+" DR", "");
                    }
                

                   
                  

                    int index = dgDuebills.Rows.Count - 1;
                    dgDuebills.Rows[index].Cells[4].Style.BackColor = Color.DarkCyan;
                    dgDuebills.Rows[index].Cells[4].Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgDuebills.Rows[index].Cells[4].Style.ForeColor = Color.White;

                    dgDuebills.Rows[index].Cells[5].Style.BackColor = Color.DarkCyan;
                    dgDuebills.Rows[index].Cells[5].Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgDuebills.Rows[index].Cells[5].Style.ForeColor = Color.White;

                    dgDuebills.Rows[index].Cells[6].Style.BackColor = Color.DarkCyan;
                    dgDuebills.Rows[index].Cells[6].Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgDuebills.Rows[index].Cells[6].Style.ForeColor = Color.White;
                }
                else
                {
                   // MessageBox.Show("Please Select a ledger");
                }
            }
            catch
            {
            }
        }

        public void SearchDebitorDueBills()
        {
            GetSales();
            GetDebitorRecipts();

            //try
            //{
            //    for (int i = 0; i < drgDueBillsCustomers.Rows.Count; i++)
            //    {
            //        //  TotalAmountToPay = TotalAmountToPay + Convert.ToDouble(dgDuebills.Rows[i].Cells["ToAmount"].Value);
            //        TotalPayed = TotalPayed + Convert.ToDouble(drgDueBillsCustomers.Rows[i].Cells["cPaidAmount"].Value);
            //    }
            //}
            //catch (Exception ee)
            //{
            //    MessageBox.Show(ee.Message);
            //}

            BalanceToPay = TotalAmountToPay - TotalPayedAmount;
            try
            {


              //  BalanceToPay = TotalAmountToPay - TotalPayed;
                

                drgDueBillsCustomers.Rows.Add();

                if (BalanceToPay < 0)
                {
                    drgDueBillsCustomers.Rows.Add("", "", "", "Total", TotalAmountToPay.ToString("n2"), TotalPayedAmount.ToString("n2"), BalanceToPay.ToString("n2") + " DR", "");
                }
                else
                {
                    drgDueBillsCustomers.Rows.Add("", "", "", "Total", TotalAmountToPay.ToString("n2"), TotalPayedAmount.ToString("n2"), BalanceToPay.ToString("n2") + " CR", "");
                }
                

                int index = drgDueBillsCustomers.Rows.Count - 1;
                drgDueBillsCustomers.Rows[index].Cells[4].Style.BackColor = Color.DarkCyan;
                drgDueBillsCustomers.Rows[index].Cells[4].Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                drgDueBillsCustomers.Rows[index].Cells[4].Style.ForeColor = Color.White;

                drgDueBillsCustomers.Rows[index].Cells[5].Style.BackColor = Color.DarkCyan;
                drgDueBillsCustomers.Rows[index].Cells[5].Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                drgDueBillsCustomers.Rows[index].Cells[5].Style.ForeColor = Color.White;

                drgDueBillsCustomers.Rows[index].Cells[6].Style.BackColor = Color.DarkCyan;
                drgDueBillsCustomers.Rows[index].Cells[6].Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                drgDueBillsCustomers.Rows[index].Cells[6].Style.ForeColor = Color.White;
            }
            catch
            {
            }
        }

        public void GetSales()
        {
            try
            {
                DataTable dt = new DataTable();
                int j = 0;
                //cmd.CommandText = "SELECT        DATED, VOUCHERNO, VOUCHERTYPE, PARTICULARS, DEBIT, ACCID, NARRATION,CREDIT FROM  tb_Transactions WHERE        (ACCID = @ALEDGERID) AND (VOUCHERTYPE <> N'Cash Payment') ORDER BY VOUCHERTYPE";
               
                //cmd.CommandText = "   SELECT        DATED, VOUCHERNO, VOUCHERTYPE, PARTICULARS, DEBIT, ACCID, NARRATION, CREDIT FROM            tb_Transactions WHERE        (ACCID = @ALEDGERID) AND (VOUCHERTYPE <> N'Cash Receipt') AND (DATED BETWEEN DATEADD(d, - 1, @DATE1) AND DATEADD(d, 2, @DATE2)) ORDER BY VOUCHERTYPE";
                //cmd.Parameters.AddWithValue("@ALEDGERID", cmbSuppliers.SelectedValue);
                //cmd.Parameters.AddWithValue("@DATE1", Date_From.Value);
                //cmd.Parameters.AddWithValue("@DATE2", Date_To.Value);
                //cmd.Connection = conn;
                //cmd.CommandType = CommandType.Text;
                //adapter.SelectCommand = cmd;

                //adapter.Fill(dt);
                transObj.AccId = cmbSuppliers.SelectedValue.ToString();
                transObj.DateFrom = Date_From.Value;
                transObj.DateTo = Date_To.Value;
                dt = transObj.getData();

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["VOUCHERTYPE"].ToString() == "Sales Return" || dt.Rows[i]["VOUCHERTYPE"].ToString() == "Credit Note")
                        {
                          //  drgDueBillsCustomers.Rows.Add(dt.Rows[i]["DATED"].ToString(), dt.Rows[i]["VOUCHERNO"].ToString(), dt.Rows[i]["VOUCHERTYPE"].ToString(), dt.Rows[i]["PARTICULARS"].ToString(), "0.00", dt.Rows[i]["DEBIT"].ToString(), "0.00", dt.Rows[i]["NARRATION"].ToString());
                        }
                        else
                        {
                            drgDueBillsCustomers.Rows.Add(dt.Rows[i]["DATED"].ToString(), dt.Rows[i]["VOUCHERNO"].ToString(), dt.Rows[i]["VOUCHERTYPE"].ToString(), dt.Rows[i]["PARTICULARS"].ToString(), dt.Rows[i]["DEBIT"].ToString(), "", "", dt.Rows[i]["NARRATION"].ToString());
                            TotalAmountToPay = TotalAmountToPay + Convert.ToDouble(drgDueBillsCustomers.Rows[j].Cells["cToAmount"].Value);
                            j++;
                        }

                       
                    }
                }



            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
            //finally
            //{
            //    cmd.Parameters.Clear();
            //}

          
        }

        private void btnSave_Click(object sender, EventArgs e)
        {  
            btnClear.PerformClick();
            if (cmbSuppliers.SelectedIndex >= 0)
            {
                if (LedgerType == "Debitors")
                {
                    dgDuebills.Visible = false;
                    drgDueBillsCustomers.Visible = true;
                    SearchDebitorDueBills();
                    drgDueBillsCustomers.FirstDisplayedScrollingRowIndex = drgDueBillsCustomers.RowCount - 1;


                }
                else
                {
                    dgDuebills.Visible = true;
                    drgDueBillsCustomers.Visible = false;
                    SearchDuebills();
                    dgDuebills.FirstDisplayedScrollingRowIndex = dgDuebills.RowCount - 1;

                }

                try
                {


                    GetLedgerContactDetails(cmbSuppliers.SelectedValue.ToString());
                }
                catch
                {
                }
                cmbSuppliers.Focus();
            }
            else
            {
                cmbSuppliers.Focus();
            }

        }
        public void GetFinancialYear()
        {
            try
            {
                DataTable dt = new DataTable();
                cset.Status = true;
                dt = cset.GetFinancialYear();

                Date_From.Value = Convert.ToDateTime(dt.Rows[0][1]);
               
                Date_To.Value = Convert.ToDateTime(dt.Rows[0][2]);

          


            }
            catch
            {
            }
        }

        public void GetLedgerContactDetails(string ledgerid)
        {
            
                try
                {
                    //cmd.Connection = conn;
                   DataTable dt1 = new DataTable();
                    //SqlDataAdapter sqadp = new SqlDataAdapter();
                    //cmd.CommandText = "SELECT        DATED, VOUCHERNO, VOUCHERTYPE, PARTICULARS, DEBIT, ACCID, NARRATION,CREDIT FROM  tb_Transactions WHERE        (ACCID = @ALEDGERID) AND (VOUCHERTYPE <> N'Cash Payment') ORDER BY VOUCHERTYPE";
                    //if (LedgerType == "Creditors")
                   // {
                    //    cmd.CommandText = "SELECT *  FROM    PAY_SUPPLIER WHERE        (LedgerId = @ALEDGERID)";
                  //  }
                   // else if (LedgerType == "Debitors")
                   //  {
                    //    cmd.CommandText = "SELECT *  FROM    REC_CUSTOMER WHERE        (LedgerId = @ALEDGERID)";
                   // }

                    //cmd.Parameters.AddWithValue("@ALEDGERID", Convert.ToInt32(ledgerid));

                    //cmd.CommandType = CommandType.Text;
                    //sqadp.SelectCommand = cmd;

                    //sqadp.Fill(dt1);
                    //cmd.Parameters.Clear();
                    custObj.LedgerId = ledgerid;
                    dt1 = custObj.getAllDataByLedgeId( LedgerType);


                if (dt1.Rows.Count > 0)
                {
                    if (LedgerType == "Creditors")
                    {
                        dgDuebills.Rows.Add();
                        dgDuebills.Rows.Add();
                        dgDuebills.Rows.Add("Name", dt1.Rows[0]["DESC_ENG"].ToString(), "", "", "", "", "", "");
                        dgDuebills.Rows.Add("Address", dt1.Rows[0]["ADDRESS_A"].ToString(), "", "", "", "", "", "");
                        dgDuebills.Rows.Add("", dt1.Rows[0]["ADDRESS_B"].ToString(), "", "", "", "", "", "");
                        dgDuebills.Rows.Add("Phone", dt1.Rows[0]["TELE1"].ToString(), "", "", "", "", "", "");
                        dgDuebills.Rows.Add("Mobile", dt1.Rows[0]["MOBILE"].ToString(), "", "", "", "", "", "");
                        dgDuebills.Rows.Add("City", dt1.Rows[0]["CITY_CODE"].ToString(), "", "", "", "", "", "");
                        string[] NOTES = dt1.Rows[0]["NOTES"].ToString().Split('-');

                        dgDuebills.Rows.Add("Bank", NOTES[0], "", "", "", "", "", "");
                        dgDuebills.Rows.Add("Branch", NOTES[1], "", "", "", "", "", "");
                        dgDuebills.Rows.Add("Account No", NOTES[2], "", "", "", "", "", "");
                        dgDuebills.Rows.Add("IFC Code", NOTES[3], "", "", "", "", "", "");
                     
                    }
                    else if (LedgerType == "Debitors")
                    {
                        drgDueBillsCustomers.Rows.Add();
                        drgDueBillsCustomers.Rows.Add();
                        drgDueBillsCustomers.Rows.Add("Name", dt1.Rows[0]["DESC_ENG"].ToString(), "", "", "","", "", "");
                        drgDueBillsCustomers.Rows.Add("Address", dt1.Rows[0]["ADDRESS_A"].ToString(), "", "", "", "", "", "");
                        drgDueBillsCustomers.Rows.Add("", dt1.Rows[0]["ADDRESS_B"].ToString(), "", "", "", "", "", "");
                        drgDueBillsCustomers.Rows.Add("Phone", dt1.Rows[0]["TELE1"].ToString(), "", "", "", "", "", "");
                        drgDueBillsCustomers.Rows.Add("Mobile", dt1.Rows[0]["MOBILE"].ToString(), "", "", "", "", "", "");
                        drgDueBillsCustomers.Rows.Add("City", dt1.Rows[0]["CITY_CODE"].ToString(), "", "", "", "", "", "");
                        string[] NOTES = dt1.Rows[0]["NOTES"].ToString().Split('-');

                        drgDueBillsCustomers.Rows.Add("Bank", NOTES[0], "", "", "", "", "", "");
                        drgDueBillsCustomers.Rows.Add("Branch", NOTES[1], "", "", "", "", "", "");
                        drgDueBillsCustomers.Rows.Add("Account No", NOTES[2], "", "", "", "", "", "");
                        drgDueBillsCustomers.Rows.Add("IFC Code", NOTES[3], "", "", "", "", "", "");
                        
                    }
                    
                }
                }
                catch (Exception EE)
                {
                     MessageBox.Show(EE.Message);
                }
                //finally
                //{
                //    conn.Close();
                //}
        }



        private void DueBills_Load(object sender, EventArgs e)
        {
            GetFinancialYear();
            try
            {
                //conn.Open();
            //    cmd.CommandText = "SELECT LEDGERID,LEDGERNAME FROM tb_Ledgers where UNDER =13 OR UNDER=14";
                
                //cmd.CommandText = "SELECT LEDGERID,LEDGERNAME FROM tb_Ledgers";
                //cmd.Connection = conn;
               // cmd.CommandType = CommandType.Text;
               // adapter.SelectCommand = cmd;
         
               // adapter.Fill(tableType);
                tableType = ldgObj.SelectLedgerNmae();
                cmbSuppliers.DataSource = tableType;
                cmbSuppliers.DisplayMember = "LEDGERNAME";
                cmbSuppliers.ValueMember = "LEDGERID";
                cmbSuppliers.SelectedIndex = -1;
            }
            catch(Exception ee)
            {
              //  MessageBox.Show(ee.Message);
            }
            //finally
            //{
              //  conn.Close();
            //}

            ActiveControl = cmbSuppliers;
            cmbSuppliers.SelectedValue = LedgerId;
            SearchDuebills();


        }

        public void GetUnder(string ledgerid)
        {
            try
            {
               // cmd.Connection = conn;
                DataTable dt1 = new DataTable();
               // SqlDataAdapter sqadp = new SqlDataAdapter();
                //cmd.CommandText = "SELECT        DATED, VOUCHERNO, VOUCHERTYPE, PARTICULARS, DEBIT, ACCID, NARRATION,CREDIT FROM  tb_Transactions WHERE        (ACCID = @ALEDGERID) AND (VOUCHERTYPE <> N'Cash Payment') ORDER BY VOUCHERTYPE";
               
                //cmd.CommandText = "SELECT UNDER  FROM    tb_Ledgers WHERE        (LEDGERID = @ALEDGERID)";
                //cmd.Parameters.AddWithValue("@ALEDGERID",Convert.ToInt32(ledgerid));

                //cmd.CommandType = CommandType.Text;
                //sqadp.SelectCommand = cmd;
             
               // sqadp.Fill(dt1); 
                //cmd.Parameters.Clear();
                ldgObj.LEDGERID = Convert.ToInt32(ledgerid);
                dt1 = ldgObj.getUnderByLedgerId();
                if (dt1.Rows.Count > 0)
                {

                    if (dt1.Rows[0][0].ToString() == "13")
                    {
                        LedgerType = "Creditors";
                    }
                    else if (dt1.Rows[0][0].ToString() == "14")
                    {
                        LedgerType = "Debitors";
                    }
                }
            }
            catch (Exception EE)
            {
               // MessageBox.Show(EE.Message);
            }
            //finally
            //{
            //    conn.Close();
            //}
        }

        public void GetPurchases()
        {
            try
            {
                DataTable dt = new DataTable();
                int j = 0;
                //cmd.CommandText = "SELECT        DATED, VOUCHERNO, VOUCHERTYPE, PARTICULARS, DEBIT, ACCID, NARRATION,CREDIT FROM  tb_Transactions WHERE        (ACCID = @ALEDGERID) AND (VOUCHERTYPE <> N'Cash Payment') ORDER BY VOUCHERTYPE";
              //  cmd.CommandText = "   SELECT        DATED, VOUCHERNO, VOUCHERTYPE, PARTICULARS, DEBIT, ACCID, NARRATION, CREDIT FROM            tb_Transactions WHERE        (ACCID = @ALEDGERID) AND (VOUCHERTYPE <> N'Cash Payment') AND (DATED BETWEEN DATEADD(d, - 1, @DATE1) AND DATEADD(d, 2, @DATE2)) ORDER BY VOUCHERTYPE";
               
                
                //cmd.CommandText = "SELECT  DATED, VOUCHERNO, VOUCHERTYPE, PARTICULARS, DEBIT, ACCID, NARRATION, CREDIT FROM            tb_Transactions WHERE        (ACCID = @ALEDGERID) AND (DATED BETWEEN DATEADD(d, - 1, @DATE1) AND DATEADD(d, 2, @DATE2)) ORDER BY VOUCHERTYPE";
                //cmd.Parameters.AddWithValue("@ALEDGERID", cmbSuppliers.SelectedValue);
                //cmd.Parameters.AddWithValue("@DATE1", Date_From.Value);
                //cmd.Parameters.AddWithValue("@DATE2", Date_To.Value);
                //cmd.Connection = conn;
                //cmd.CommandType = CommandType.Text;
                //adapter.SelectCommand = cmd;


                //adapter.Fill(dt);
                transObj.AccId = cmbSuppliers.SelectedValue.ToString();
                transObj.DateFrom = Date_From.Value;
                transObj.DateTo = Date_To.Value;
                dt = transObj.getDataByCondition();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["VOUCHERTYPE"].ToString() == "Purchase Return" || dt.Rows[i]["VOUCHERTYPE"].ToString() == "Debit Note" || dt.Rows[i]["VOUCHERTYPE"].ToString() == "Cash Payment")
                        {
                          //  dgDuebills.Rows.Add(dt.Rows[i]["DATED"].ToString(), dt.Rows[i]["VOUCHERNO"].ToString(), dt.Rows[i]["VOUCHERTYPE"].ToString(), dt.Rows[i]["PARTICULARS"].ToString(), "0.00", dt.Rows[i]["DEBIT"].ToString(), "0.00", dt.Rows[i]["NARRATION"].ToString());
                        }
                        else
                        {
                         
                            dgDuebills.Rows.Add(dt.Rows[i]["DATED"].ToString(), dt.Rows[i]["VOUCHERNO"].ToString(), dt.Rows[i]["VOUCHERTYPE"].ToString(), dt.Rows[i]["PARTICULARS"].ToString(), dt.Rows[i]["CREDIT"].ToString(), "", "", dt.Rows[i]["NARRATION"].ToString());
                         //   MessageBox.Show(dgDuebills.Rows[j].Cells["ToAmount"].Value.ToString());
                            TotalAmountToPay = TotalAmountToPay + Convert.ToDouble(dgDuebills.Rows[j].Cells["ToAmount"].Value);
                            j++;
                        }

                       
                    }
                }


       
            }
            catch (Exception ee)
            {
               MessageBox.Show(ee.Message);
            }
            //finally
            //{
            //    cmd.Parameters.Clear();
            //}

          
            
        }

        public void GetDebitorRecipts()
        {
            double Topay = 0, Balance = 0;
            try
            {
                DataTable dt = new DataTable();

               // cmd.CommandText = "SELECT        DATED, VOUCHERNO, VOUCHERTYPE, PARTICULARS, DEBIT, ACCID, NARRATION,CREDIT FROM  tb_Transactions WHERE        (ACCID = @ALEDGERID) AND ((VOUCHERTYPE = 'Cash Payment') or (VOUCHERTYPE = 'Purchase Return')) AND (DATED BETWEEN DATEADD(d, - 1, @DATE1) AND DATEADD(d, 2, @DATE2)) ORDER BY VOUCHERTYPE";
               
                //cmd.CommandText = " SELECT        DATED, VOUCHERNO, VOUCHERTYPE, PARTICULARS, DEBIT, ACCID, NARRATION, CREDIT FROM            tb_Transactions WHERE        (ACCID = @ALEDGERID) AND (DATED BETWEEN @DATE1 AND @DATE2) AND (VOUCHERTYPE = 'Cash Receipt' OR VOUCHERTYPE = 'Sales Return') ORDER BY VOUCHERTYPE ";
                //cmd.Parameters.AddWithValue("@ALEDGERID", cmbSuppliers.SelectedValue);
                //cmd.Parameters.AddWithValue("@DATE1", Date_From.Value);
               // cmd.Parameters.AddWithValue("@DATE2", Date_To.Value);
               // cmd.Connection = conn;
               // cmd.CommandType = CommandType.Text;
               // adapter.SelectCommand = cmd;
                //adapter.Fill(dt);
                transObj.AccId = cmbSuppliers.SelectedValue.ToString();
                transObj.DateFrom = Date_From.Value;
                transObj.DateTo = Date_To.Value;
                dt = transObj.getDataByDateAndId();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //if (dt.Rows[i]["VOUCHERTYPE"].ToString() != "Purchase Return")

                        //  {
                        Balance = Balance + Convert.ToDouble(dt.Rows[i]["CREDIT"]);
                        //  }
                    }

                    TotalPayedAmount = Balance;

                    for (int i = 0; i < drgDueBillsCustomers.Rows.Count; i++)
                    {
                        Topay = Convert.ToDouble(drgDueBillsCustomers.Rows[i].Cells["cToAmount"].Value);
                        if (Balance > Topay)
                        {
                            drgDueBillsCustomers.Rows[i].Cells["cPaidAmount"].Value = Topay;
                            Balance = Balance - Topay;
                            PreviousBalance = PreviousBalance + (Convert.ToDouble(drgDueBillsCustomers.Rows[i].Cells["cToAmount"].Value) - Convert.ToDouble(drgDueBillsCustomers.Rows[i].Cells["cPaidAmount"].Value));
                            drgDueBillsCustomers.Rows[i].Cells["cBalance"].Value = PreviousBalance;
                        }
                        else
                        {
                            drgDueBillsCustomers.Rows[i].Cells["cPaidAmount"].Value = Balance;
                            PreviousBalance = PreviousBalance + (Convert.ToDouble(drgDueBillsCustomers.Rows[i].Cells["cToAmount"].Value) - Convert.ToDouble(drgDueBillsCustomers.Rows[i].Cells["cPaidAmount"].Value));
                            drgDueBillsCustomers.Rows[i].Cells["cBalance"].Value = PreviousBalance;
                            //dgDuebills.Rows[i].Cells["Balance"].Value = Convert.ToDouble(dgDuebills.Rows[i].Cells["ToAmount"].Value) - Convert.ToDouble(dgDuebills.Rows[i].Cells["PaidAmount"].Value);
                            //  TotalPayed = TotalPayed + Convert.ToDouble(dgDuebills.Rows[i].Cells["PaidAmount"].Value);
                            // BalanceToPay = BalanceToPay + Convert.ToDouble(dgDuebills.Rows[i].Cells["Balance"].Value);
                            Balance = 0.00;
                            //  break;
                        }

                        //  TotalPayed = TotalPayed + Convert.ToDouble(dgDuebills.Rows[i].Cells["PaidAmount"].Value);
                        //  BalanceToPay = BalanceToPay + Convert.ToDouble(dgDuebills.Rows[i].Cells["Balance"].Value);
                    }
                }
                else
                {

                    Balance = 0.00;



                    for (int i = 0; i < drgDueBillsCustomers.Rows.Count; i++)
                    {
                        Topay = Convert.ToDouble(drgDueBillsCustomers.Rows[i].Cells["cToAmount"].Value);
                        if (Balance > Topay)
                        {
                            drgDueBillsCustomers.Rows[i].Cells["cPaidAmount"].Value = Topay;
                            Balance = Balance - Topay;
                            drgDueBillsCustomers.Rows[i].Cells["cBalance"].Value = Convert.ToDouble(drgDueBillsCustomers.Rows[i].Cells["cToAmount"].Value) - Convert.ToDouble(drgDueBillsCustomers.Rows[i].Cells["cPaidAmount"].Value);
                        }
                        else
                        {
                            drgDueBillsCustomers.Rows[i].Cells["cPaidAmount"].Value = Balance;
                            drgDueBillsCustomers.Rows[i].Cells["cBalance"].Value = Convert.ToDouble(drgDueBillsCustomers.Rows[i].Cells["cToAmount"].Value) - Convert.ToDouble(drgDueBillsCustomers.Rows[i].Cells["cPaidAmount"].Value);
                            //  TotalPayed = TotalPayed + Convert.ToDouble(dgDuebills.Rows[i].Cells["PaidAmount"].Value);
                            //  BalanceToPay = BalanceToPay + Convert.ToDouble(dgDuebills.Rows[i].Cells["Balance"].Value);
                            //  break;
                        }

                        //  TotalPayed = TotalPayed + Convert.ToDouble(dgDuebills.Rows[i].Cells["PaidAmount"].Value);
                        // BalanceToPay =Convert.ToDouble(TotalAmountToPay.ToString("n2"));
                    }
                }



            }
            catch (Exception ee)
            {
                //MessageBox.Show(ee.Message);
            }
            //finally
            //{
            //    cmd.Parameters.Clear();
            //}

        }


        double TotalPayedAmount = 0;
        public void GetPayments()
        {
            double Topay = 0, Balance = 0;
            try
            {
                DataTable dt = new DataTable();

             //   cmd.CommandText = "SELECT        DATED, VOUCHERNO, VOUCHERTYPE, PARTICULARS, DEBIT, ACCID, NARRATION,CREDIT FROM  tb_Transactions WHERE        (ACCID = @ALEDGERID) AND ((VOUCHERTYPE = 'Cash Payment') or (VOUCHERTYPE = 'Purchase Return')) AND (DATED BETWEEN DATEADD(d, - 1, @DATE1) AND DATEADD(d, 2, @DATE2)) ORDER BY VOUCHERTYPE";
               
                //cmd.CommandText = "SELECT        DATED, VOUCHERNO, VOUCHERTYPE, PARTICULARS, DEBIT, ACCID, NARRATION ,CREDIT FROM  tb_Transactions WHERE        (ACCID = @ALEDGERID) AND (DATED BETWEEN DATEADD(d, - 1, @DATE1) AND DATEADD(d, 2, @DATE2)) ORDER BY VOUCHERTYPE";
                //cmd.Parameters.AddWithValue("@ALEDGERID", cmbSuppliers.SelectedValue);
                //cmd.Parameters.AddWithValue("@DATE1", Date_From.Value);
                //cmd.Parameters.AddWithValue("@DATE2", Date_To.Value);
                //cmd.Connection = conn;
                //cmd.CommandType = CommandType.Text;
                //adapter.SelectCommand = cmd;
                //adapter.Fill(dt);

                transObj.AccId = cmbSuppliers.SelectedValue.ToString();
                transObj.DateFrom = Date_From.Value;
                transObj.DateTo = Date_To.Value;
                dt = transObj.getDataByCondition();
                
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //if (dt.Rows[i]["VOUCHERTYPE"].ToString() != "Purchase Return")
                       
                      //  {
                            Balance = Balance + Convert.ToDouble(dt.Rows[i]["DEBIT"]);
                       
                      //  }
                    }

                    TotalPayedAmount = Balance;

                    for (int i = 0; i < dgDuebills.Rows.Count; i++)
                    {
                        Topay = Convert.ToDouble(dgDuebills.Rows[i].Cells["ToAmount"].Value);
                        if (Balance > Topay)
                        {
                            dgDuebills.Rows[i].Cells["PaidAmount"].Value = Topay;
                            Balance = Balance - Topay;
                            PreviousBalance=PreviousBalance+(Convert.ToDouble(dgDuebills.Rows[i].Cells["ToAmount"].Value) - Convert.ToDouble(dgDuebills.Rows[i].Cells["PaidAmount"].Value));
                            dgDuebills.Rows[i].Cells["Balance"].Value = PreviousBalance;
                        }
                        else
                        {
                            dgDuebills.Rows[i].Cells["PaidAmount"].Value = Balance;
                            PreviousBalance = PreviousBalance + (Convert.ToDouble(dgDuebills.Rows[i].Cells["ToAmount"].Value) - Convert.ToDouble(dgDuebills.Rows[i].Cells["PaidAmount"].Value));
                            dgDuebills.Rows[i].Cells["Balance"].Value = PreviousBalance;
                            //dgDuebills.Rows[i].Cells["Balance"].Value = Convert.ToDouble(dgDuebills.Rows[i].Cells["ToAmount"].Value) - Convert.ToDouble(dgDuebills.Rows[i].Cells["PaidAmount"].Value);
                          //  TotalPayed = TotalPayed + Convert.ToDouble(dgDuebills.Rows[i].Cells["PaidAmount"].Value);
                           // BalanceToPay = BalanceToPay + Convert.ToDouble(dgDuebills.Rows[i].Cells["Balance"].Value);
                            Balance = 0.00;
                          //  break;
                        }

                      //  TotalPayed = TotalPayed + Convert.ToDouble(dgDuebills.Rows[i].Cells["PaidAmount"].Value);
                      //  BalanceToPay = BalanceToPay + Convert.ToDouble(dgDuebills.Rows[i].Cells["Balance"].Value);
                    }
                }
                else
                {

                    Balance = 0.00;
                        
                    

                    for (int i = 0; i < dgDuebills.Rows.Count; i++)
                    {
                        Topay = Convert.ToDouble(dgDuebills.Rows[i].Cells["ToAmount"].Value);
                        if (Balance > Topay)
                        {
                            dgDuebills.Rows[i].Cells["PaidAmount"].Value = Topay;
                            Balance = Balance - Topay;
                            dgDuebills.Rows[i].Cells["Balance"].Value = Convert.ToDouble(dgDuebills.Rows[i].Cells["ToAmount"].Value) - Convert.ToDouble(dgDuebills.Rows[i].Cells["PaidAmount"].Value);
                        }
                        else
                        {
                            dgDuebills.Rows[i].Cells["PaidAmount"].Value = Balance;
                            dgDuebills.Rows[i].Cells["Balance"].Value = Convert.ToDouble(dgDuebills.Rows[i].Cells["ToAmount"].Value) - Convert.ToDouble(dgDuebills.Rows[i].Cells["PaidAmount"].Value);

                          //  TotalPayed = TotalPayed + Convert.ToDouble(dgDuebills.Rows[i].Cells["PaidAmount"].Value);
                          //  BalanceToPay = BalanceToPay + Convert.ToDouble(dgDuebills.Rows[i].Cells["Balance"].Value);
                          //  break;
                        }

                      //  TotalPayed = TotalPayed + Convert.ToDouble(dgDuebills.Rows[i].Cells["PaidAmount"].Value);
                        BalanceToPay = Balance;
                    }
                }



            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
            //finally
            //{
            //    cmd.Parameters.Clear();
            //}



        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            dgDuebills.Rows.Clear();
            drgDueBillsCustomers.Rows.Clear();
            TotalAmountToPay=0;
            BalanceToPay = 0;
            PreviousBalance = 0;
            TotalPayed = 0;
            TotalPayedAmount = 0;
        }

        private void cmbSuppliers_KeyDown(object sender, KeyEventArgs e)
        {
           if(e.KeyCode==Keys.Enter)
            {
                btnSave.Focus();
            }
        }

        private void cmbSuppliers_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetUnder(cmbSuppliers.SelectedValue.ToString());
        }
    }
}
