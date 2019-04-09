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
using iTextSharp.text.pdf;
using iTextSharp.text;





namespace Sys_Sols_Inventory.Accounts
{
    public partial class Trail_Balance : Form
    {
        Class.Transactions Trans = new Class.Transactions();
        Class.CompanySetup Comstp = new Class.CompanySetup();
        Class.AccountGroup accgrp = new Class.AccountGroup();
        Initial mdi = (Initial)Application.OpenForms["Initial"];
        Login lg = (Login)Application.OpenForms["Login"];
        //DataGridView dgData;
           StringFormat strFormat; //Used to format the grid rows.
        ArrayList arrColumnLefts = new ArrayList();//Used to save left coordinates of columns
        ArrayList arrColumnWidths = new ArrayList();//Used to save column widths
        int iCellHeight = 0; //Used to get/set the datagridview cell height
        int iTotalWidth = 0; //
        int iRow = 0;//Used as counter
        bool bFirstPage = false; //Used to check whether we are printing first page
        bool bNewPage = false;// Used to check whether we are printing a new page
        int iHeaderHeight = 0; //Used for the header height
       

        bool IsTheSameCellValue(int column, int row)
        {
            DataGridViewCell cell1 = dgData[column, row];
            DataGridViewCell cell2 = dgData[column, row - 1];
            if (cell1.Value == null || cell2.Value == null)
            {
                return false;
            }
            return cell1.Value.ToString() == cell2.Value.ToString();
        }
    
        public Trail_Balance()
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
        private void btnSave_Click(object sender, EventArgs e)
        {
           

                lbltitle.Text = "Trail Balance from " + Date_From.Value.ToShortDateString() + " To" + Date_To.Value.ToShortDateString();
                double SumDebit = 0;
                double SumCredit = 0;
                double TotalDebit = 0;
                double TotalCredit = 0;
                DataTable dt2 = new DataTable();
                string ledunder = UNDER.SelectedValue.ToString();
                dt2 = Trans.GetTrailBalance(Convert.ToDateTime(Date_From.Value.ToShortDateString()), Convert.ToDateTime(Date_To.Value.ToShortDateString()), ledunder);

                dgData.Rows.Clear();
                if (dt2.Rows.Count > 0)
                {

                    //for (int i = 0; i < dt2.Rows.Count; i++)
                    //{
                    //    TotalDebit = TotalDebit + Convert.ToDouble(dt2.Rows[i][1]);
                    //    TotalCredit = TotalCredit + Convert.ToDouble(dt2.Rows[i][2]);
                    //}






                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        //DataRow row = dt2.NewRow();
                        //dt2.Rows.Add(row);


                        //     DataRow newRow2 = dt2.NewRow();
                        //    newRow2["ACCNAME"] = "TOTAL";


                        string accunder;

                        DataTable dt3 = new DataTable();

                        string Accgrp = dt2.Rows[i]["UNDER"].ToString();

                        accunder = accgrp.SelectAccountName(Accgrp);



                        //accunder=dt3.Rows[i]["DESC_ENG"].ToString();



                        if (dt2.Rows[i]["Debit"].ToString() != dt2.Rows[i]["Credit"].ToString())
                        {
                            TotalDebit = Convert.ToDouble(dt2.Rows[i]["Debit"]);
                            TotalCredit = Convert.ToDouble(dt2.Rows[i]["Credit"]);


                            if (TotalDebit - TotalCredit < 0)
                            {
                                dgData.Rows.Add(dt2.Rows[i]["ACCID"].ToString(), accunder, dt2.Rows[i]["ACCNAME"].ToString(), "0.00", Math.Abs(TotalDebit - TotalCredit).ToString("N2"));
                            }
                            else
                            {
                                dgData.Rows.Add(dt2.Rows[i]["ACCID"].ToString(), accunder, dt2.Rows[i]["ACCNAME"].ToString(), Math.Abs(TotalDebit - TotalCredit).ToString("N2"), "0.00");
                               
                            }

                        }

                    }

                    //  dgData.DataSource = dt2;
                    //dgData.Columns[0].Width = 250;
                    //dgData.Columns[1].Width = 150;
                    //dgData.Columns[2].Width = 150;
                    dgData.Columns["ACCID"].Visible = false;

                    try
                    {
                        for (int i = 0; i < dgData.Rows.Count; i++)
                        {
                            SumDebit = SumDebit + Convert.ToDouble(dgData.Rows[i].Cells["Debit"].Value);
                            SumCredit = SumCredit + Convert.ToDouble(dgData.Rows[i].Cells["Credit"].Value);
                        }

                    }
                    catch
                    {
                    }

                    dgData.Rows[dgData.Rows.Count - 1].Cells["ACCNAME"].Value = "Total";
                    dgData.Rows[dgData.Rows.Count - 1].Cells["Debit"].Value = SumDebit.ToString("n2");
                    dgData.Rows[dgData.Rows.Count - 1].Cells["Credit"].Value = SumCredit.ToString("n2");

                    dgData.Rows[dgData.Rows.Count - 1].DefaultCellStyle.ForeColor = System.Drawing.Color.Red;
                    dgData.Rows[dgData.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font(dgData.Font, FontStyle.Bold);
                    dgData.Columns["Debit"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
                    dgData.Columns["Credit"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
                    System.Windows.Forms.DataGridViewCellStyle boldStyle = new System.Windows.Forms.DataGridViewCellStyle();
                    boldStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
                    dgData.Columns[1].DefaultCellStyle = boldStyle;
                    for (int i = 0; i < dgData.Rows.Count; i++)
                    {
                        string vr, cr;


                        vr = dgData.Rows[i].Cells["Debit"].Value.ToString(); // here you go vr = the value of the cel
                        cr = dgData.Rows[i].Cells["Credit"].Value.ToString();
                        if (vr == "0.00") // you can check for anything
                        {

                            dgData.Rows[i].Cells["Debit"].Value = DBNull.Value;
                            // you can format this cell 
                        }
                        if (cr == "0.00") // you can check for anything
                        {

                            dgData.Rows[i].Cells["Credit"].Value = DBNull.Value;
                            // you can format this cell 
                        }
                    }
                    dgData.FirstDisplayedScrollingRowIndex = dgData.RowCount - 1;
                   
                    //    radGridView1.Rows.AddNew();
                    //    radGridView1.Rows[radGridView1.Rows.Count-1].Cells["UN"].Value = " Total";
                    //    radGridView1.Rows[radGridView1.Rows.Count-1].Cells["Debit"].Value = SumDebit.ToString("n2");
                    //    radGridView1.Rows[radGridView1.Rows.Count-1].Cells["Credit"].Value = SumCredit.ToString("n2");
                    //    radGridView1.TableElement.ScrollToRow(radGridView1.Rows.Count -1);
                   
                   
                    //radGridView1.TableElement.ScrollToRow(radGridView1.Rows.Count -1);

                }

                SumCredit = SumDebit = TotalCredit = TotalDebit = 0;
                // dgData.RowsDefaultCellStyle.BackColor = System.Drawing.Color.White;
                // dgData.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.Beige;

                //  GroupDescriptor descriptor1 = new GroupDescriptor();

          


            
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
        private void dgData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            
            
        }

        private void Trail_Balance_Load(object sender, EventArgs e)
        {
              DataTable dt = new DataTable();
            
            Comstp.Status = true;
            getgrpname();
            dt=Comstp.GetCurrentFinancialYear();
            if (dt.Rows.Count > 0)
            {
                Date_From.Value = Convert.ToDateTime(dt.Rows[0]["SDate"]);
                Date_To.Value = Convert.ToDateTime(dt.Rows[0]["EDate"]);
            }
            btnSave.PerformClick();
        //radGridView1.GroupDescriptors.Add("UN",  ListSortDirection.Ascending);
       
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
                foreach (DataGridViewColumn dgvGridCol in dgData.Columns)
                {
                    iTotalWidth += dgvGridCol.Width;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                for (int i = 1; i < dgData.Columns.Count + 1; i++)
                {
                    worksheet.Cells[1, i] = dgData.Columns[i - 1].HeaderText;
                }



                // storing Each row and column value to excel sheet
                for (int i = 0; i < dgData.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < dgData.Columns.Count; j++)
                    {
                        worksheet.Cells[i + 2, j + 1] = dgData.Rows[i].Cells[j].Value.ToString();
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
            PdfPTable pdfTable = new PdfPTable(dgData.ColumnCount);
            pdfTable.DefaultCell.Padding = 3;
            pdfTable.WidthPercentage = 100;
            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfTable.DefaultCell.BorderWidth = 1;

            //Adding Header row
            foreach (DataGridViewColumn column in dgData.Columns)
            {
                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                cell.BackgroundColor = new iTextSharp.text.Color(240, 240, 240);
                pdfTable.AddCell(cell);
            }

            //Adding DataRow
            foreach (DataGridViewRow row in dgData.Rows)
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
                    foreach (DataGridViewColumn GridCol in dgData.Columns)
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
                while (iRow <= dgData.Rows.Count - 1)
                {
                    DataGridViewRow GridRow = dgData.Rows[iRow];
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
                            e.Graphics.DrawString("Customer Summary", new System.Drawing.Font(dgData.Font, FontStyle.Bold),
                                    Brushes.Black, e.MarginBounds.Left, e.MarginBounds.Top -
                                    e.Graphics.MeasureString("Customer Summary", new System.Drawing.Font(dgData.Font,
                                    FontStyle.Bold), e.MarginBounds.Width).Height - 13);

                            String strDate = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToShortTimeString();
                            //Draw Date
                            e.Graphics.DrawString(strDate, new System.Drawing.Font(dgData.Font, FontStyle.Bold),
                                    Brushes.Black, e.MarginBounds.Left + (e.MarginBounds.Width -
                                    e.Graphics.MeasureString(strDate, new System.Drawing.Font(dgData.Font,
                                    FontStyle.Bold), e.MarginBounds.Width).Width), e.MarginBounds.Top -
                                    e.Graphics.MeasureString("Customer Summary", new System.Drawing.Font(new System.Drawing.Font(dgData.Font,
                                    FontStyle.Bold), FontStyle.Bold), e.MarginBounds.Width).Height - 13);

                            //Draw Columns                 
                            iTopMargin = e.MarginBounds.Top;
                            foreach (DataGridViewColumn GridCol in dgData.Columns)
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

        private void printDocument1_BeginPrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
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
                foreach (DataGridViewColumn dgvGridCol in dgData.Columns)
                {
                    iTotalWidth += dgvGridCol.Width;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void dgData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //try
            //{
            //    int ID = Convert.ToInt32(dgData.CurrentRow.Cells["ACCID"].Value);
            //    Accounts.LedgerReport Lgrpt = new LedgerReport(ID);
                
            //        if (lg.Theme == "1")
            //        {
            //            ComponentFactory.Krypton.Navigator.KryptonPage kp = new ComponentFactory.Krypton.Navigator.KryptonPage();
            //           mdi.maindocpanel.Pages.Add(kp);

            //           Lgrpt.Show();
            //           Lgrpt.TopLevel = false;
            //            //  splitContainer1.Panel2.Controls.Add(ad);
            //           kp.Controls.Add(Lgrpt);
            //           Lgrpt.Dock = DockStyle.Fill;
            //           kp.Text = Lgrpt.Text;
            //            kp.Name = "Ledger Report";
            //            // kp.Focus();
            //            Lgrpt.FormBorderStyle = FormBorderStyle.None;

            //            mdi.maindocpanel.SelectedPage = kp;
            //        mdi.onlyhide();
            //        }
            //        else
            //        {
            //            Lgrpt.ShowDialog();
            //        }
                
            //}
            //catch
            //{
            //}
            int ID = Convert.ToInt32(dgData.CurrentRow.Cells["ACCID"].Value);
            Accounts.Monthly_Report mrpt = new Monthly_Report(ID);
            mrpt.Show();
           
           
        }

        private void dgData_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            //try
            //{
            //    int ID = Convert.ToInt32(dgData.CurrentRow.Cells["ACCID"].Value);
            //    Accounts.LedgerReport Lgrpt = new LedgerReport(ID);

            //        if (lg.Theme == "1")
            //        {
            //            ComponentFactory.Krypton.Navigator.KryptonPage kp = new ComponentFactory.Krypton.Navigator.KryptonPage();
            //           mdi.maindocpanel.Pages.Add(kp);

            //           Lgrpt.Show();
            //           Lgrpt.TopLevel = false;
            //            //  splitContainer1.Panel2.Controls.Add(ad);
            //           kp.Controls.Add(Lgrpt);
            //           Lgrpt.Dock = DockStyle.Fill;
            //           kp.Text = Lgrpt.Text;
            //            kp.Name = "Ledger Report";
            //            // kp.Focus();
            //            Lgrpt.FormBorderStyle = FormBorderStyle.None;

            //            mdi.maindocpanel.SelectedPage = kp;
            //        mdi.onlyhide();
            //        }
            //        else
            //        {
            //            Lgrpt.ShowDialog();
            //        }

            //}
            //catch
            //{
            //}
           
       
            //int ID = Convert.ToInt32(dgData.CurrentRow.Cells["ACCID"].Value);
            //Accounts.Monthly_Report mrpt = new Monthly_Report(ID);
            //mrpt.Show();
          

          try
            {
                int ID = Convert.ToInt32(dgData.CurrentRow.Cells["ACCID"].Value);
                Accounts.Monthly_Report mrpt = new Monthly_Report(ID);

                if (lg.Theme == "1")
                {
                    ComponentFactory.Krypton.Navigator.KryptonPage kp = new ComponentFactory.Krypton.Navigator.KryptonPage();
                    mdi.maindocpanel.Pages.Add(kp);

                    mrpt.Show();
                    mrpt.TopLevel = false;
                    //  splitContainer1.Panel2.Controls.Add(ad);
                    kp.Controls.Add(mrpt);
                    mrpt.Dock = DockStyle.Fill;
                    kp.Text = mrpt.Text;
                    kp.Name = "Monthly Report";
                    // kp.Focus();
                    mrpt.FormBorderStyle = FormBorderStyle.None;

                    mdi.maindocpanel.SelectedPage = kp;
                    mdi.onlyhide();
                  
                }
                else
                {
                    mrpt.ShowDialog();
                }

            }
            catch
            {
            }
          
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dgData_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

        /*   if (e.RowIndex == 0)
                return;

             if (dgData.Rows[e.RowIndex].Cells["UNDER"].Value.ToString() == dgData.Rows[e.RowIndex].Cells["UNDER"].Value.ToString())
            {
            
                e.Value = "";
                e.FormattingApplied = true; // 以降の書式設定は不要

            }*/
            if (e.RowIndex == 0)
                return;
            if (e.ColumnIndex == 1)
            {
                if (IsTheSameCellValue(e.ColumnIndex, e.RowIndex))
                {
                    e.Value = "";
                    e.FormattingApplied = true;
                }
            }


            
       }

        //private void radGridView1_CellDoubleClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        //{
        //    int ID = Convert.ToInt32(radGridView1.CurrentRow.Cells["ACCID"].Value);
        //    Accounts.Monthly_Report mrpt = new Monthly_Report(ID);
        //    mrpt.Show();
        //}

        //private void radGridView1_CellFormatting(object sender, Telerik.WinControls.UI.CellFormattingEventArgs e)
        //{
            
            
           
           
        //}

        //private void radGridView1_RowFormatting(object sender, Telerik.WinControls.UI.RowFormattingEventArgs e)
        //{
        //    if (e.RowElement.RowInfo.Cells["ACCNAME"].Value != "")
        //    {

        //    }
        //    else
        //    {
        //        e.RowElement.DrawFill = true;
        //        e.RowElement.ForeColor = System.Drawing.Color.Red;
               
        //    }
        //}

        private void dgData_CellPainting_1(object sender, DataGridViewCellPaintingEventArgs e)
        {
            e.AdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None;
           if (e.RowIndex < 1 || e.ColumnIndex < 0)
        
            return;
           if (e.ColumnIndex == 1)
           {
               if (IsTheSameCellValue(e.ColumnIndex, e.RowIndex))
               {
                   e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
                 
               }
               else
               {
                   e.AdvancedBorderStyle.Top = dgData.AdvancedCellBorderStyle.Top;
               }

           }
           else
           {
               e.AdvancedBorderStyle.Top = dgData.AdvancedCellBorderStyle.Top;
           }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            dgData.Rows.Clear();
        }

        private void dgData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            int index = e.RowIndex;
            dgData.Rows[index].Selected = true;
        }
       
    }

}