using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sys_Sols_Inventory
{
    public partial class Due_Customers : Form
    {
        //private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        //private SqlCommand cmd = new SqlCommand();
        //private SqlDataAdapter adapter = new SqlDataAdapter();
        private DataTable table = new DataTable();

        StringFormat strFormat; //Used to format the grid rows.
        ArrayList arrColumnLefts = new ArrayList();//Used to save left coordinates of columns
        ArrayList arrColumnWidths = new ArrayList();//Used to save column widths
        int iCellHeight = 0; //Used to get/set the datagridview cell height
        int iTotalWidth = 0; //
        int iRow = 0;//Used as counter
        bool bFirstPage = false; //Used to check whether we are printing first page
        bool bNewPage = false;// Used to check whether we are printing a new page
        int iHeaderHeight = 0; //Used for the header height


        public Due_Customers()
        {
            InitializeComponent();
        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {

            
                if (keyData == (Keys.Escape))     
                {
                    this.Close();
                   return true;
                }


                return base.ProcessCmdKey(ref msg, keyData);
            
        }

        private void Due_Customers_Load(object sender, EventArgs e)
        {
            
            GET_CUS_BALANCE();
        }
        public void GET_CUS_BALANCE()
        {
            DataTable tmp = new DataTable();
            try
            {
                int indx = 0;
                //string query = "SELECT REC_CUSTOMER.LedgerId,ISNULL(CREDIT_PERIOD,0) AS CREDIT_PERIOD FROM REC_CUSTOMER LEFT OUTER JOIN TB_LEDGERS ON TB_LEDGERS.LEDGERID=REC_CUSTOMER.LEDGERID WHERE TB_LEDGERS.UNDER=14";
                string query = "SELECT * FROM(SELECT CODE,DESC_ENG NAME,sum(DEBIT) DEBIT,SUM(credit) CREDIT,sum(DEBIT)-SUM(credit) BALANCE FROM(SELECT T.*,C.CREDIT_PERIOD ,C.DESC_ENG,C.CODE,DATEADD(day, DATEDIFF(day, 0,DATEADD(DAY,C.CREDIT_PERIOD*-1, GETDATE())), 0) DueDate FROM tb_Transactions T INNER JOIN REC_CUSTOMER C ON C.LedgerId=T.ACCID )T1 WHERE DATED<=DueDate group By ACCID,DESC_ENG,CODE)dueCustomers where balance>0";
                DataTable dt = new DataTable();
                //adapter.SelectCommand = cmd;
                //adapter.Fill(dt);
                dt = Model.DbFunctions.GetDataTable(query);
                drgCustomers.DataSource = dt;
            }
            catch { }
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
                  
            //        int creditprd = 0 - Convert.ToInt32(dt.Rows[i]["CREDIT_PERIOD"]);
            //        tmp.Clear();
                   // tmp = count_due_customers(DateTime.Now.AddDays(creditprd), Convert.ToInt32(dt.Rows[i]["LedgerId"]));
            //        if (tmp.Rows.Count > 0)
            //        {

            //            try
            //            {
            //                    drgCustomers.Rows.Add();
            //                    drgCustomers.Rows[indx].Cells["ACCID"].Value = tmp.Rows[0]["CODE"].ToString();
            //                    drgCustomers.Rows[indx].Cells["NAME"].Value = tmp.Rows[0]["DESC_ENG"].ToString();
            //                    drgCustomers.Rows[indx].Cells["DEBIT"].Value = tmp.Rows[0]["total_DEBIT"].ToString();
            //                    drgCustomers.Rows[indx].Cells["CREDIT"].Value = tmp.Rows[0]["total_CREDIT"].ToString();
            //                    drgCustomers.Rows[indx].Cells["BALANCE"].Value = tmp.Rows[0]["BALANCE"].ToString();
            //                    drgCustomers.Rows[indx].Cells["CONTACT"].Value = tmp.Rows[0]["MOBILE"].ToString();
            //                    indx++;
            //            }
            //            catch
            //            {

            //            }



            //        }

            //    }

            //}
            //catch (Exception ee)
            //{
            //    MessageBox.Show(ee.Message);
            //}
        }
        public DataTable count_due_customers(DateTime date, int ledgerid)
        {
            DataTable dt = new DataTable();            
            
            string query ="SELECT REC_CUSTOMER.CODE,REC_CUSTOMER.DESC_ENG,REC_CUSTOMER.MOBILE,DEBIT.total_DEBIT,CREDIT.total_CREDIT,(DEBIT.total_DEBIT-CREDIT.total_CREDIT) AS BALANCE  FROM REC_CUSTOMER LEFT OUTER JOIN (SELECT ACCID,ISNULL(SUM(DEBIT),0) as total_DEBIT FROM tb_Transactions WHERE DATED<=@date GROUP BY ACCID) AS DEBIT ON REC_CUSTOMER.LedgerId=DEBIT.ACCID  LEFT OUTER JOIN (SELECT ACCID,ISNULL(SUM(CREDIT),0) as total_CREDIT FROM tb_Transactions GROUP BY ACCID) AS CREDIT ON REC_CUSTOMER.LedgerId=CREDIT.ACCID WHERE REC_CUSTOMER.LedgerId=@id ORDER BY REC_CUSTOMER.CODE";
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@date", date.ToString("yyyy/MM/dd"));
            Parameters.Add("@id", ledgerid);
            dt = Model.DbFunctions.GetDataTable(query, Parameters);
            return dt;
        }
        public 
        DateTime Datefrom, Dateto;
        public void GetFinancialYear()
        {
            try
            {
                Class.CompanySetup cset = new Class.CompanySetup();
                DataTable dt = new DataTable();
                cset.Status = true;
                dt = cset.GetFinancialYear();

                Datefrom = Convert.ToDateTime(dt.Rows[0][1]);

                Dateto = Convert.ToDateTime(dt.Rows[0][2]);




            }
            catch
            {
            }
        }
        public DataTable GetTransactions(string lid)
        {

            DataTable dt = new DataTable();
            try
            { 
                string query = "SELECT tb_Transactions.ACCID,tb_Transactions.ACCNAME, SUM(tb_Transactions.CREDIT) AS CREDIT, SUM(tb_Transactions.DEBIT) AS DEBIT, tb_Ledgers.UNDER, SUM(tb_Transactions.CREDIT)- SUM(tb_Transactions.DEBIT) AS BALANCE FROM            tb_Transactions INNER JOIN tb_Ledgers ON tb_Transactions.ACCID = tb_Ledgers.LEDGERID GROUP BY  tb_Transactions.ACCID,tb_Transactions.ACCNAME, tb_Ledgers.UNDER HAVING (tb_Ledgers.UNDER = '14') and (tb_Transactions.ACCID= '" + lid + "')";
                dt = Model.DbFunctions.GetDataTable(query);
                return dt;
            }
            catch (Exception ex)
            {

            }
            return dt;
        }
        private void drgCustomers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string LedgerId = drgCustomers.CurrentRow.Cells["ACCID"].Value.ToString();
                try
                {
                    string query = "SELECT REC_CUSTOMER.DESC_ENG, REC_CUSTOMER.ADDRESS_A, REC_CUSTOMER.ADDRESS_B, REC_CUSTOMER.TELE1, REC_CUSTOMER.MOBILE, REC_CUSTOMER.EMAIL, REC_CUSTOMER.CITY_CODE, REC_CUSTOMER.REG_CODE, REC_CUSTOMER.COUNTRY_CODE, REC_CUSTOMER.WEB, REC_CUSTOMER.NOTES, REC_CUSTOMER.DESC_ARB, REC_CUSTOMER.TYPE, tb_Ledgers.LEDGERID, GEN_CITY.DESC_ENG AS CITY FROM tb_Ledgers LEFT OUTER JOIN REC_CUSTOMER ON tb_Ledgers.LEDGERID = REC_CUSTOMER.LedgerId LEFT OUTER JOIN GEN_CITY ON REC_CUSTOMER.CITY_CODE = GEN_CITY.CODE WHERE  (tb_Ledgers.LEDGERID = @LEDGERID)";
                    DataTable dt = new DataTable();
                    Dictionary<string, object> Parameters = new Dictionary<string, object>();
                    Parameters.Add("LEDGERID", LedgerId);
                    dt = Model.DbFunctions.GetDataTable(query, Parameters);

                    if (dt.Rows.Count > 0)
                    {
                        lblName.Text = dt.Rows[0]["DESC_ENG"].ToString();
                        lbladd1.Text = dt.Rows[0]["ADDRESS_A"].ToString();
                        lbladd2.Text = dt.Rows[0]["CITY"].ToString();
                        lbltele.Text = dt.Rows[0]["TELE1"].ToString();
                        lblmob.Text = dt.Rows[0]["MOBILE"].ToString();
                        lblemail.Text = dt.Rows[0]["EMAIL"].ToString();
                        
                    }
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }

            }
            catch
            {
            }
        }

        private void btnDocNo_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
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
                    foreach (DataGridViewColumn GridCol in drgCustomers.Columns)
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
                while (iRow <= drgCustomers.Rows.Count - 1)
                {
                    DataGridViewRow GridRow = drgCustomers.Rows[iRow];
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
                            e.Graphics.DrawString("Customers having Due Amounts ", new System.Drawing.Font(drgCustomers.Font, FontStyle.Bold),
                                    Brushes.Black, e.MarginBounds.Left, e.MarginBounds.Top -
                                    e.Graphics.MeasureString("Report " , new System.Drawing.Font(drgCustomers.Font,
                                    FontStyle.Bold), e.MarginBounds.Width).Height - 13);

                            String strDate = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToShortTimeString();
                            //Draw Date
                            e.Graphics.DrawString(strDate, new System.Drawing.Font(drgCustomers.Font, FontStyle.Bold),
                                    Brushes.Black, e.MarginBounds.Left + (e.MarginBounds.Width -
                                    e.Graphics.MeasureString(strDate, new System.Drawing.Font(drgCustomers.Font,
                                    FontStyle.Bold), e.MarginBounds.Width).Width), e.MarginBounds.Top -
                                    e.Graphics.MeasureString("Ledger Report " + drgCustomers.Text, new System.Drawing.Font(new System.Drawing.Font(drgCustomers.Font,
                                    FontStyle.Bold), FontStyle.Bold), e.MarginBounds.Width).Height - 13);

                            //Draw Columns                 
                            iTopMargin = e.MarginBounds.Top;
                            foreach (DataGridViewColumn GridCol in drgCustomers.Columns)
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
                foreach (DataGridViewColumn dgvGridCol in drgCustomers.Columns)
                {
                    iTotalWidth += dgvGridCol.Width;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
