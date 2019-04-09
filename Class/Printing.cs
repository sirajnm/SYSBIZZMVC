using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Sys_Sols_Inventory.Class
{
    class Printing
    {
        public string CompanyName { get; set; }
        public string BranchName { get; set; }
        public string Phoneno { get; set; }
        public string Email { get; set; }
        public string Date { get; set; }
        public string TinNo { get; set; }
        public string CUSID { get; set; }
        public string CustomerName { get; set; }
        public string SupplierName { get; set; }
        public string SalesMan { get; set; }
        public string AmtRcvd { get; set; }
        public string Balance { get; set; }
        public string Discount { get; set; }
        public string TaxAmt { get; set; }
        public string GrandTotal { get; set; }
        public string NetTotal { get; set; }
        public string VAT { get; set; }

   



        public void PrintA4()
        { 
            PrintDocument printDocument = new PrintDocument();
            PaperSize ps = new PaperSize();
            ps.RawKind = (int)PaperKind.A4;
            printDocument.DefaultPageSettings.PaperSize = ps;
            printDocument.PrintPage += printDocument_PrintPage;
            printDocument.Print();
        }

        private void printDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            float xpos;
            int startx =20;
            int starty = 20;
            int offset = 10;


            int w = e.MarginBounds.Width / 2;
            int x = e.MarginBounds.Left;
            int y = e.MarginBounds.Top;
            Font printFont = new Font("Courier New", 7);
            Bitmap logo = System.Drawing.SystemIcons.WinLogo.ToBitmap();

            int height = 100 + y;
            string tabDataText = "Hello World";

            var txtDataWidth = e.Graphics.MeasureString(tabDataText, printFont).Width;

            e.Graphics.DrawImage(logo,
                 e.MarginBounds.Left + (e.MarginBounds.Width / 2) - (logo.Width / 2),
                 e.MarginBounds.Top + (e.MarginBounds.Height / 2) - (logo.Height));

            using (var sf = new StringFormat())
            {
                height += logo.Height + 15;
                sf.LineAlignment = StringAlignment.Center;
                sf.Alignment = StringAlignment.Center;
                xpos = e.MarginBounds.Left + (e.MarginBounds.Width / 2);
                Pen myPen = new Pen(Color.Red);
              //  myPen = new System.Drawing.Pen(System.Drawing.Color.Red);
                e.Graphics.DrawLine(myPen, startx,starty,0,0 );
                e.Graphics.DrawString(tabDataText, printFont,
                     new SolidBrush(Color.Black),
                    xpos, starty);
            }

            e.HasMorePages = false;
        }
    }
}