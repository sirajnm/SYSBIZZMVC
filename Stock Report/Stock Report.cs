using Sys_Sols_Inventory.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sys_Sols_Inventory.Stock_Report
{
    public partial class Stock_Report : Form
    {
        bool PAGETOTAL = false;
        public int printeditems = 0;
        int m = 0;

        stockClass stockClass = new stockClass();

        public Stock_Report()
        {
            InitializeComponent();
        }
        
        private void btn_Stock_Click(object sender, EventArgs e)
        {
            if (ch_ngtve.Checked)
                dgv_stock.DataSource = stockClass.StockOnDate(dtp_stock.Value.Date);
            else
                dgv_stock.DataSource = stockClass.StockOnDate_value(dtp_stock.Value.Date);
            if (dgv_stock.Rows.Count > 0)
            {
                dgv_stock.Columns["ITEM NAME"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
                dgv_stock.Columns["QTY"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            button1.PerformClick();
        }

        public void ExportExcel()
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
                worksheet.Name = "Stock on Date";

                DataTable dt = new DataTable();               
                string query = "SELECT * FROM tbl_companysetup";
                dt = DbFunctions.GetDataTable(query);

                string report = "Stock Report";                

                //heading
                worksheet.Cells[1, 3] = "       " + dt.Rows[0]["company_name"].ToString();
                worksheet.Cells[2, 3] = dt.Rows[0]["address"].ToString();
                worksheet.Cells[2, 3] = dt.Rows[0]["address"].ToString();
                worksheet.Cells[3, 3] = report; ;
                //merging
                worksheet.Range[worksheet.Cells[1, 2], worksheet.Cells[1, 4]].Merge();
                worksheet.Range[worksheet.Cells[2, 2], worksheet.Cells[2, 4]].Merge();
                worksheet.Range[worksheet.Cells[3, 2], worksheet.Cells[3, 4]].Merge();

                //font bold
                worksheet.Cells[1, 2].EntireRow.Font.Bold = true;
                worksheet.Cells[1, 2].Interior.Color = Color.FromArgb(192, 192, 192);


                //  storing header part in Excel
                for (int i = 0; i < (dgv_stock.Columns.Count); i++)
                {
                    worksheet.Cells[5, i + 1] = dgv_stock.Columns[i].HeaderText;
                    worksheet.Cells[5, i + 1].EntireRow.Font.Bold = true;
                    worksheet.Columns[i + 1].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;

                    // worksheet.Columns[i + 1].ColumnWidth = dataGridView1.Columns[i].Width;
                }

                // worksheet.Columns[1].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                // storing Each row and column value to excel sheet
                for (int i = 0; i < dgv_stock.Rows.Count; i++)
                {
                    for (int j = 0; j < (dgv_stock.Columns.Count); j++)
                    {
                        if (j == 1)
                        {
                            worksheet.Cells[i + 6, j + 1] = dgv_stock.Rows[i].Cells[j].Value.ToString();


                        }
                        else
                        {
                            worksheet.Cells[i + 6, j + 1] = dgv_stock.Rows[i].Cells[j].Value.ToString();
                        }                        
                    }
                }
                worksheet.Columns.AutoFit();
                worksheet.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlPortrait;
                worksheet.PageSetup.Zoom = false;
                worksheet.PageSetup.FitToPagesTall = 100;
                worksheet.PageSetup.FitToPagesWide = 1;
                // save the application
                System.Windows.Forms.SaveFileDialog saveDlg = new System.Windows.Forms.SaveFileDialog();
                saveDlg.InitialDirectory = @"D:\";
                saveDlg.Filter = "Excel files (*.xls)|*.xls";
                saveDlg.FilterIndex = 0;
                saveDlg.RestoreDirectory = true;
                saveDlg.Title = "Export Excel File To";
                if (saveDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string path = saveDlg.FileName;
                    workbook.SaveCopyAs(path);
                    workbook.Saved = true;
                    workbook.Close(true, Type.Missing, Type.Missing);
                    app.Quit();
                }

            }
            catch (Exception ex)
            {
                string st = ex.Message;
            }
        }

        private void btn_Export_Click(object sender, EventArgs e)
        {
            if (dgv_stock.Rows.Count > 0)
                ExportExcel();
        }

        private void printA4(object sender, PrintPageEventArgs e)
        {
            Pen blackpen = new Pen(System.Drawing.Color.Black, 1);
            Font Headerfont5 = new Font("Times New Roman", 10, FontStyle.Regular);
            Font Headerfont1 = new Font("Times New Roman", 12, FontStyle.Regular);
            Font Headerfont2 = new Font("Times New Roman", 14, FontStyle.Bold);
            //System.Drawing.Font Headerfont2 = new System.Drawing.Font("Times New Roman", 14, FontStyle.Bold);
            //System.Drawing.Font Headerfont1 = new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold);
            //System.Drawing.Font Headerfont3 = new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular);
            //System.Drawing.Font Headerfont4 = new System.Drawing.Font("Times New Roman", 10, FontStyle.Bold);
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
            
            string itemcode = "";
            string itemname = "";
            string quantity = "";
            //string vchtype = "";
            //string partclrs = "";
            //string debit = "";
            //string credit = "";
            //string balance = "";
            //string narration = "";
            int value = 0;
            var tabDataForeColor = System.Drawing.Color.Black;
            int height = 100 + y;
            Pen blackPen1 = new Pen(System.Drawing.Color.Black, 1);
            //var txtDataWidth = e.Graphics.MeasureString(CompanyName, Headerfont1).Width;
            double pricWtax = 0;
            decimal a = 0;
            StringFormat format = new StringFormat(StringFormatFlags.DirectionRightToLeft);
            using (var sf = new StringFormat())
            {
                height += 15;
                sf.LineAlignment = StringAlignment.Center;
                sf.Alignment = StringAlignment.Center;
                xpos = e.MarginBounds.Left + (e.MarginBounds.Width / 2);
                e.Graphics.DrawRectangle(blackPen1, 7, 115, 790, 945); //BIG RECTANGLE
                //e.Graphics.DrawLine(blackpen, 7, 80, 790, 80); //h
                e.Graphics.DrawLine(blackpen, 7, 150, 797, 150); //h
                //e.Graphics.DrawLine(blackpen, 10, 1090, 830, 1090); //h
                //e.Graphics.DrawLine(blackpen, 10, 1130, 830, 1130); //h
                e.Graphics.DrawLine(blackpen, 35, 115, 35, 1060); //v sl no
                e.Graphics.DrawLine(blackpen, 350, 115, 350, 1060); //customner name
                e.Graphics.DrawLine(blackpen, 440, 115, 440, 1060); //lastpaydate
                e.Graphics.DrawLine(blackpen, 530, 115, 530, 1060); //lastpayamnt
                //e.Graphics.DrawLine(blackpen, 620, 115, 620, 1030); //debit
                //e.Graphics.DrawLine(blackpen, 710, 115, 710, 1030); //credit     
                //e.Graphics.DrawLine(blackpen, 7, 1030, 797, 1030); //h
                e.Graphics.DrawString("No", Headerfont5, new SolidBrush(Color.Black), 8, 120);
                e.Graphics.DrawString("Item Name", Headerfont5, new SolidBrush(Color.Black), 140, 120);
                e.Graphics.DrawString("Item code", Headerfont5, new SolidBrush(Color.Black), 350, 120); e.Graphics.DrawString("No", Headerfont5, new SolidBrush(Color.Black), 8, 120);
                e.Graphics.DrawString("Quantity", Headerfont5, new SolidBrush(Color.Black), 440, 120);
                e.Graphics.DrawString("Remarks", Headerfont5, new SolidBrush(Color.Black), 530, 120);
                //e.Graphics.DrawString("Credit", Headerfont5, new SolidBrush(Color.Black), 620, 120);
                //e.Graphics.DrawString("Balance", Headerfont5, new SolidBrush(Color.Black), 710, 120);
                e.Graphics.DrawString("STOCK ON "+dtp_stock.Value.Date.ToShortDateString(), Headerfont2, new SolidBrush(Color.Black), 300, 80);



                //string date = "Ledger/Statement of Account for the period From " + Date_From.Value.ToShortDateString() + " To " + Date_To.Value.ToShortDateString();
                //e.Graphics.DrawString(date, Headerfont3, new SolidBrush(System.Drawing.Color.Black), 180, 15);

                float fontheight = Headerfont1.GetHeight();
                try
                {
                    int i = 0;
                    int j = 1;
                    int nooflines = 0;
                    int extraline=0;
                    //foreach (DataGridViewRow row in dgledgerTrns.Rows)
                    for (int k = 0; k < dgv_stock.Rows.Count - 1; k++)
                    {
                        //if (Convert.ToDecimal(dgv_stock.Rows[k].Cells["BALANCE"].Value) == 0)
                        //{
                        //    continue;
                        //}
                        int orglgth=0;
                        PRINTTOTALPAGE = false;
                        int printpoint = 35;
                        if (j > printeditems)
                        {
                            if (nooflines < 31)
                            {
                                m = m + 1;

                                //SELECT REC_CUSTOMER.DESC_ENG AS NAME,CONVERT(VARCHAR,LAST_PAY.DATED,103) AS 'LAST PAY DATE',LAST_PAY.LAST_AMOUNT AS 'LAST PAY AMOUNT',BALANCE.DEBIT,BALANCE.CREDIT,BALANCE.DEBIT-BALANCE.CREDIT AS BALANCE FROM (SELECT ACCID,SUM(tb_Transactions.DEBIT) AS DEBIT,SUM(tb_Transactions.CREDIT) AS CREDIT FROM tb_Transactions LEFT OUTER JOIN REC_CUSTOMER ON REC_CUSTOMER.Ledgerid=tb_Transactions.ACCID WHERE REC_CUSTOMER.CODE IS NOT NULL GROUP BY ACCID) AS BALANCE LEFT OUTER JOIN REC_CUSTOMER ON REC_CUSTOMER.LedgerId=BALANCE.ACCID LEFT OUTER JOIN (SELECT ACCID,MAX(tb_Transactions.DATED) AS DATED,SUM(tb_Transactions.DEBIT) AS LAST_AMOUNT FROM tb_transactions  INNER JOIN REC_CUSTOMER ON REC_CUSTOMER.LedgerId=tb_Transactions.ACCID GROUP BY ACCID) AS LAST_PAY ON LAST_PAY.ACCID=BALANCE.ACCID WHERE REC_CUSTOMER.SALESMAN_CODE LIKE '%" + cmb_salesman.SelectedValue + "%' AND REC_CUSTOMER.DESC_ENG LIKE '%" +textBox2.Text + "%'", conn);
                                e.Graphics.DrawString(m.ToString(), Headerfont5, new SolidBrush(System.Drawing.Color.Black), startx - 40, starty + offset + 120);

                                itemname = dgv_stock.Rows[k].Cells["ITEM NAME"].Value.ToString();
                                orglgth = itemname.Length;
                                string subname = itemname.Length <= 35 ? itemname : itemname.Substring(0, 35);
                                int blength = orglgth - 35;
                                e.Graphics.DrawString(subname.ToString(), Headerfont5, new SolidBrush(System.Drawing.Color.Black), startx - 15, starty + offset + 120);

                                itemcode = dgv_stock.Rows[k].Cells["ITEM_CODE"].Value.ToString();
                                // string date1 = dated.Substring(0, 10);

                                e.Graphics.DrawString(itemcode.ToString(), Headerfont5, new SolidBrush(System.Drawing.Color.Black), startx + 300, starty + offset + 120);

                                quantity = dgv_stock.Rows[k].Cells["QTY"].Value.ToString();
                                e.Graphics.DrawString(quantity.ToString(), Headerfont5, new SolidBrush(System.Drawing.Color.Black), startx + 390, starty + offset + 120);

                                //partclrs = dgv_stock.Rows[k].Cells["DEBIT"].Value.ToString();
                                //e.Graphics.DrawString(partclrs.ToString(), Headerfont5, new SolidBrush(System.Drawing.Color.Black), startx + 480, starty + offset + 120);

                                //debit = dgv_stock.Rows[k].Cells["CREDIT"].Value.ToString();
                                //e.Graphics.DrawString(debit.ToString(), Headerfont5, new SolidBrush(System.Drawing.Color.Black), startx + 570, starty + offset + 120);

                                //credit = dgv_stock.Rows[k].Cells["BALANCE"].Value.ToString();
                                //e.Graphics.DrawString(credit.ToString(), Headerfont5, new SolidBrush(System.Drawing.Color.Black), startx + 660, starty + offset + 120);
                                int linec = 0;
                                while (blength > 1)
                                {
                                    linec++;
                                    offset = offset + (int)fontheight;
                                    subname = blength <= 35 ? itemname.Substring(printpoint, blength) : itemname.Substring(printpoint, 35);
                                    e.Graphics.DrawString(subname.ToString(), Headerfont5, new SolidBrush(System.Drawing.Color.Black), startx - 15, starty + offset + 120);
                                    blength = blength - 35;
                                    printpoint = printpoint + 35;
                                    extraline++;
                                }
                                if (linec > 2)
                                {
                                    nooflines++;                                    
                                }
                                if (extraline >= 2)
                                {
                                    nooflines++;
                                    extraline = 0;
                                }
                                
                                printpoint = 35;
                                e.Graphics.DrawLine(blackpen, 7, starty + offset + 142, 797, starty + offset + 142); //h

                                //balance = dataGridView1.Rows[k].Cells["BALANCES"].Value.ToString();
                                //string bal = balance.Remove(balance.Length - 3);
                                //e.Graphics.DrawString(bal.ToString(), Headerfont3, new SolidBrush(System.Drawing.Color.Black), startx + 670, starty + offset + 40, format);

                                //narration = dataGridView1.Rows[k].Cells["NARRATION"].Value.ToString();
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


                        //string opblnce = dgledgerTrns.Rows[value + 4].Cells["PARTICULARS"].Value.ToString();
                        //e.Graphics.DrawString("Total", Headerfont2, new SolidBrush(System.Drawing.Color.Black), startx + 450, 1032);
                        //e.Graphics.DrawString(textBox1.Text, Headerfont2, new SolidBrush(System.Drawing.Color.Black), startx + 580, 1032);
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
        public void printReport()
        {
            try
            {
                printeditems = 0;
                PrintDialog printdlg = new PrintDialog();
                PrintDocument doc = new PrintDocument();
                doc.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("A4", 840, 1188);
                doc.PrintPage += printA4;
                printdlg.Document = doc;
                doc.Print();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Printer Error: " + ex.Message);
            }
        }
        private void btn_print_Click(object sender, EventArgs e)
        {
            printReport();
        }
    }
}
