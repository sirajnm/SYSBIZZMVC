using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections;
using ComponentFactory.Krypton.Toolkit;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Drawing.Text;
using Sys_Sols_Inventory.Class;



namespace Sys_Sols_Inventory.Accounts
{
    public partial class Day_Book : Form
    {
        
        //private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        Class.AccountGroup accgrp = new Class.AccountGroup();
        //private SqlCommand cmd = new SqlCommand();
       // private SqlDataAdapter adapter = new SqlDataAdapter();
        private DataTable table = new DataTable();
        private DataTable tableType = new DataTable();
        private DataTable tableCurrency = new DataTable();
        private BindingSource source = new BindingSource();
        Class.CompanySetup cset = new Class.CompanySetup();
        Class.Daybook daybook = new Class.Daybook();
        DataTable GridData = new DataTable();
        double SumCredit = 0, SumDebit = 0;
        public DataGridViewCellCollection c;
        public String FormType,ClosingBal;
        DataGridView dgv;
        Initial mdi = (Initial)Application.OpenForms["Initial"];

        StringFormat strFormat; //Used to format the grid rows.
        ArrayList arrColumnLefts = new ArrayList();//Used to save left coordinates of columns
        ArrayList arrColumnWidths = new ArrayList();//Used to save column widths
        int iCellHeight = 0; //Used to get/set the datagridview cell height
        int iTotalWidth = 0; //
        int iRow = 0;//Used as counter
        bool bFirstPage = false; //Used to check whether we are printing first page
        bool bNewPage = false;// Used to check whether we are printing a new page
        int iHeaderHeight = 0; //Used for the header height

        Ledgers ldgObj = new Ledgers();


        public Day_Book()
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
        public Day_Book(string Type)
        {
            InitializeComponent();
            FormType = Type;
            lbltitle.Text = Type;
            this.Text = Type;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
           
            dataGridView1.DataSource = null;
            DataTable dt=new DataTable();
            lbltitle.Text = FormType +" Between " + Date_From.Value.ToShortDateString() + " and " + Date_To.Value.ToShortDateString();
          
            
            daybook.DATED1 = Convert.ToDateTime(Date_From.Value);
            daybook.DATED2 = Convert.ToDateTime(Date_To.Value);
            daybook.VOUCHERTYPE = DrpVoucherType.Text;
            switch(FormType)
            {
                case "Day Book":

                    try
                    {
                        daybook.LedgerId = cmLedgers.SelectedValue.ToString();
                    }
                    catch
                    {
                        daybook.LedgerId = "";
                    }
                         dt = daybook.SelectDayBook();
                         dataGridView1.DataSource = dt;
                 
                         break;
                case "Cash Book":
                         try
                         {
                             daybook.LedgerId = cmLedgers.SelectedValue.ToString();
                         }
                         catch
                         {
                             daybook.LedgerId = "";
                         }
                         try
                         {
                             daybook.UNDER = UNDER.SelectedValue.ToString();
                         }
                         catch
                         {
                             daybook.UNDER = "";
                         }
                         dt = daybook.SelectCashBook();
                         dataGridView1.DataSource = dt;
                         break;
             }
          
             dataGridView1.Columns["TRANSACTIONID"].Visible = false;
            
            dataGridView1.Columns["PARTICULARS"].Width = 200;

            if (ChkVoucherNo.Checked == false)
            {
                dataGridView1.Columns["VOUCHERNO"].Visible = false;
            }

            if (ChkVchrType.Checked == false)
            {
                dataGridView1.Columns["VOUCHERTYPE"].Visible = false;
            }
            if (ChkNarration.Checked == false)
            {
                dataGridView1.Columns["NARRATION"].Visible = false;
            }
            DataRow newrow = dt.NewRow();
            DataRow newRow4 = dt.NewRow();
            if (FormType == "Day Book")
            {
                newrow["BALANCE"] = "0.00 Dr";
                newRow4["BALANCE"] = "0.00 Dr";
            }
            else
            {
                string balance; 
                double bal = openingbalance();
                if (bal < 0)
                {
                    balance = (bal*-1).ToString() + " Cr";
                }
                else
                {
                    balance = bal.ToString() + " Dr";
                }

                newrow["BALANCE"] = balance;
              //  newrow["PARTICULARS"] = "Opening Balance";
                newRow4["BALANCE"] = balance;
            }
            dt.Rows.InsertAt(newrow, 0);
           
            sum();
            DataRow newrow1 = dt.NewRow();
            dt.Rows.Add(newrow1);
            
             DataRow newRow2 = dt.NewRow();
            
             newRow2["PARTICULARS"] = "TOTAL";
             newRow2["CREDIT"] = SumCredit.ToString();
             newRow2["DEBIT"] = SumDebit.ToString();
             string Sumba;
            
            
             newRow2["BALANCE"] =ClosingBal ;
           
            
             dt.Rows.Add(newRow2);
            
           //DataRow newRow3 = dt.NewRow();
           //  dt.Rows.Add(newRow3);
             

          
           newRow4["PARTICULARS"]="Opening Balance";
            
            
           dt.Rows.Add(newRow4);

            DataRow newRow5 = dt.NewRow();
           // newRow5["PARTICULARS"] = "Closing Balance";
           // newRow5["BALANCE"] = ClosingBal;
            dt.Rows.Add(newRow5);
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                string vr,cr;

                DataTable dtCloned = dt.Clone();
                dtCloned.Columns[5].DataType = typeof(string);
                dtCloned.Columns[6].DataType = typeof(string);
                vr = dataGridView1.Rows[i].Cells[5].Value.ToString(); // here you go vr = the value of the cel
                cr = dataGridView1.Rows[i].Cells[6].Value.ToString();
                if (vr == "0.00") // you can check for anything
                {

                    dataGridView1.Rows[i].Cells[5].Value = DBNull.Value;
                    // you can format this cell 
                }
                if (cr == "0.00") // you can check for anything
                {

                    dataGridView1.Rows[i].Cells[6].Value = DBNull.Value;
                    // you can format this cell 
                }
            }

            dataGridView1.Columns["BALANCE"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
          //  dataGridView1.Rows[0].DefaultCellStyle.ForeColor = System.Drawing.Color.Red;
            dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.ForeColor =System.Drawing.Color.Red;
            dataGridView1.Rows[dataGridView1.Rows.Count - 2].DefaultCellStyle.ForeColor = System.Drawing.Color.Red;
            System.Windows.Forms.DataGridViewCellStyle boldStyle = new System.Windows.Forms.DataGridViewCellStyle();
            boldStyle.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold); //Microsoft Sans Serif
            dataGridView1.Rows[dataGridView1.Rows.Count - 3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Rows[dataGridView1.Rows.Count - 3].DefaultCellStyle= boldStyle; 
            dataGridView1.Columns["DEBIT"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
            dataGridView1.Columns["CREDIT"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
            dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.RowCount - 1;
        }
        public void getgrpname()
        {
            DataTable dt = new DataTable();
            UNDER.DisplayMember = "DESC_ENG";
            UNDER.ValueMember = "ACOUNTID";
            dt = accgrp.SelectAccountGroupName();
            DataRow row = dt.NewRow();
            dt.Rows.InsertAt(row, 0);
            UNDER.DataSource = dt;


        }
        public void SelectVoucher()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = daybook.SelectDistinctVoucherType();
                DataRow row = dt.NewRow();
                dt.Rows.InsertAt(row, 0);
                DrpVoucherType.DisplayMember = "VOUCHERTYPE";
                DrpVoucherType.ValueMember = "VOUCHERTYPE";
                DrpVoucherType.DataSource = dt;
            }
            catch
            {
            }
           
           
        }

        public double  openingbalance()
        {
            DataTable dt = new DataTable();
            dt = daybook.GetFinancialYearStart();
            daybook.DATED1 = Convert.ToDateTime(dt.Rows[0][0].ToString());
            daybook.DATED2 = Convert.ToDateTime(Date_From.Value.ToShortDateString());
            string OpBal = daybook.selectOpeningbalance();
            if (OpBal != "")
                return Convert.ToDouble(OpBal);
            else
                return 0;
            
        }

        public void sum()
        {
                 SumCredit = 0;
            SumDebit = 0;

     
            for (int i = 1; i < dataGridView1.Rows.Count; i++)
            {
                SumCredit = SumCredit + Convert.ToDouble(dataGridView1.Rows[i].Cells["CREDIT"].Value);
                SumDebit = SumDebit + Convert.ToDouble(dataGridView1.Rows[i].Cells["DEBIT"].Value);
               
                    string balanceval ;
                    double balance = 0;
                    //if (i == 1)
                    //{
                    //    balanceval = dataGridView1.Rows[i - 1].Cells["BALANCE"].Value.ToString();
                       
                    //    balance = Convert.ToDouble(balanceval);
                    //}
                    //else
                    
                        balanceval = dataGridView1.Rows[i - 1].Cells["BALANCE"].Value.ToString();
                        string last = balanceval.Substring(balanceval.Length- 2);
                        balanceval = balanceval.Substring(0, balanceval.Length - 2);
                        if (last == "Cr")
                        {
                            balance =Convert.ToDouble (Convert.ToDouble(balanceval) * -1);
                        }
                        else
                        {
                            balance = Convert.ToDouble(balanceval);
                        }
                    
                    balance = balance + Convert.ToDouble(dataGridView1.Rows[i].Cells["DEBIT"].Value) - Convert.ToDouble(dataGridView1.Rows[i].Cells["CREDIT"].Value);
                    if (balance < 0)
                    {
                        balanceval = (Convert.ToDouble( balance*-1)).ToString("n2") + " Cr";
                    }
                    else
                    {
                        balanceval = balance.ToString() + " Dr";
                    }
                    dataGridView1.Rows[i].Cells["BALANCE"].Value = balanceval;
                    this.dataGridView1.RowsDefaultCellStyle.BackColor = System.Drawing.Color.White;
                    this.dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.White;
                    ClosingBal = balanceval;
                        
                  }
           
          
           
           
        }
    
        public void bindLedgers()
        {
            try
            {
               // conn.Open();
                //    cmd.CommandText = "SELECT LEDGERID,LEDGERNAME FROM tb_Ledgers where UNDER =13 OR UNDER=14";
              //  cmd.CommandText = "SELECT LEDGERID,LEDGERNAME FROM tb_Ledgers";
              //  cmd.Connection = conn;
            //    cmd.CommandType = CommandType.Text;
           //     adapter.SelectCommand = cmd;

           //     adapter.Fill(tableType);
                tableType = ldgObj.SelectLedgerNmae();
                cmLedgers.DataSource = tableType;
                cmLedgers.DisplayMember = "LEDGERNAME";
                cmLedgers.ValueMember = "LEDGERID";
                cmLedgers.SelectedIndex = -1;
            }
            catch (Exception ee)
            {
                //  MessageBox.Show(ee.Message);
            }
            //finally
            //{
            //     conn.Close();
            //}
        }
        private void Day_Book_Load(object sender, EventArgs e)
        {
            SelectVoucher();
         //   btnSave.PerformClick();
            bindLedgers();
            getgrpname();
            string period = "";
            DataTable dt = new DataTable();
            dt = cset.SysSetup_selectcompany();
            
            if (dt.Rows.Count > 0)
            {
                period = dt.Rows[0]["Rep_LoadinDays"].ToString();
                
            }
            ActiveControl = Date_From;
            if (period != "")
            {
                Date_From.Value = Date_From.Value.AddMonths(0 - Convert.ToInt16(period));
            }
            btnSave.PerformClick();

            
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            DrpVoucherType.SelectedIndex = 0;
            Date_From.Value = DateTime.Now;
            Date_To.Value = DateTime.Now;
            ChkNarration.Checked = false;
            ChkVchrType.Checked = false;
            ChkVoucherNo.Checked = false;
            SumCredit = 0;
            SumDebit = 0;
            dataGridView1.DataSource = null;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows.Count > 0 && dataGridView1.CurrentRow != null)
            {
                c = dataGridView1.CurrentRow.Cells;
                switch (c["VOUCHERTYPE"].Value.ToString())
                { 
                    case "Purchase":
                        PurchaseMaster pur = new PurchaseMaster(c["VOUCHERNO"].Value.ToString());
                    pur.Show();
                    break;
                    case "Purchase Return":
                    PurchaseMaster purret = new PurchaseMaster(c["VOUCHERNO"].Value.ToString());
                    purret.Show();
                    break;
                    case "SALES Normal" :
                    SalesQ sal = new SalesQ(c["VOUCHERNO"].Value.ToString());
                    sal.Show();
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
                        
                }
                
            }
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
                int iCount = 0;
                bFirstPage = true;
                bNewPage = true;

                // Calculating Total Widths
                iTotalWidth = 0;
                foreach (DataGridViewColumn dgvGridCol in dataGridView1.Columns)
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
                    foreach (DataGridViewColumn GridCol in dataGridView1.Columns)
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
                while (iRow <= dataGridView1.Rows.Count - 1)
                {
                    DataGridViewRow GridRow = dataGridView1.Rows[iRow];
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
                            e.Graphics.DrawString(lbltitle.Text, new System.Drawing.Font(dataGridView1.Font, FontStyle.Bold),
                                    Brushes.Black, e.MarginBounds.Left, e.MarginBounds.Top -
                                    e.Graphics.MeasureString(lbltitle.Text, new System.Drawing.Font(dataGridView1.Font,
                                    FontStyle.Bold), e.MarginBounds.Width).Height - 13);

                            String strDate = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToShortTimeString();
                            //Draw Date
                            e.Graphics.DrawString(strDate, new System.Drawing.Font(dataGridView1.Font, FontStyle.Bold),
                                    Brushes.Black, e.MarginBounds.Left + (e.MarginBounds.Width -
                                    e.Graphics.MeasureString(strDate, new System.Drawing.Font(dataGridView1.Font,
                                    FontStyle.Bold), e.MarginBounds.Width).Width), e.MarginBounds.Top -
                                    e.Graphics.MeasureString(lbltitle.Text, new System.Drawing.Font(new System.Drawing.Font(dataGridView1.Font,
                                    FontStyle.Bold), FontStyle.Bold), e.MarginBounds.Width).Height - 13);

                            //Draw Columns                 
                            iTopMargin = e.MarginBounds.Top;
                            foreach (DataGridViewColumn GridCol in dataGridView1.Columns)
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

        private void PicExcel_Click(object sender, EventArgs e)
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
                for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
                {
                    worksheet.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;
                }



                // storing Each row and column value to excel sheet
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        worksheet.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
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
            try
            {
                //Creating iTextSharp Table from the DataTable data
                PdfPTable pdfTable = new PdfPTable(dataGridView1.ColumnCount);
                pdfTable.DefaultCell.Padding = 3;
                pdfTable.WidthPercentage = 100;
                pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfTable.DefaultCell.BorderWidth = 1;

                //Adding Header row
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                    cell.BackgroundColor = new iTextSharp.text.Color(240, 240, 240);
                    pdfTable.AddCell(cell);
                }

                //Adding DataRow
                foreach (DataGridViewRow row in dataGridView1.Rows)
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
            catch
            { }
        }

        private void Date_From_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==(Keys.Enter|Keys.Tab))
            {
                Date_To.Focus();
            }
        }

        private void Date_To_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == (Keys.Enter | Keys.Tab))
            {
                cmLedgers.Focus();
            }
        }

        private void cmLedgers_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == (Keys.Enter | Keys.Tab))
            {
                DrpVoucherType.Focus();
            }
        }

        private void DrpVoucherType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == (Keys.Enter | Keys.Tab))
            {
                btnSave.Focus();
            }
        }

        private void dataGridView1_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
           
        }

      

    }
}
