
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.Drawing.Printing;
using System.Data.SqlClient;
using System.Globalization;
using Sys_Sols_Inventory.Model;

namespace Sys_Sols_Inventory
{
    public class laser_print
    {
        SqlCommand cmd = new SqlCommand();
        #region declarations

        public DataTable company = new DataTable();
        public Stack<String> stack = new Stack<String>();
        public Queue<string> queue = new Queue<string>();
        public bool isok = true;
        public  string tail = "";
        public int k = 0;
        //taking min lines
        public bool printerheight = true;
        public double rowheightadd = 0;
        public string template = "";
        public int morepage = 50000;// Convert.ToInt32(Properties.Settings.Default.minline.ToString());
        public int maxline = 50000;//Convert.ToInt32(Properties.Settings.Default.maxline.ToString());
        public int minline = 50000;// Convert.ToInt32(Properties.Settings.Default.minline.ToString());
        public int addline = 10;// Convert.ToInt32(Properties.Settings.Default.line_height.ToString());
        public int add_gridheight=0;//= Convert.ToInt32(Properties.Settings.Default.grid_height.ToString());
        public int add_gridbottom = 20;// Convert.ToInt32(Properties.Settings.Default.grid_bottom.ToString());

        public int rowLength=0;
        public int numberofpage = 0;
        public int pageno = 0;
        public int fullpage = 0;
        public int linecount = 0;
        public Font headfont = new Font("Times New Roman", 8);
        public Font detailsfont = new Font("Times New Roman", 6);
        public DataTable sales = new DataTable();
        public DataTable captions = new DataTable();
        public DataTable sales_dtl = new DataTable();
        public DataTable labels = new DataTable();
        public DataTable footerlabels = new DataTable();
       
        public DataTable ogsales = new DataTable();
        public DataTable settings = new DataTable();
        public string language = "";
        public bool isdesc = false;

        public DataTable lines = new DataTable();
        public DataTable rect= new DataTable();
        public int rowcount = 0;
       public int columncount = 0;

        public int rowindex = 0;
        public int columnindex = 0;
        public DataTable gridview = new DataTable();
        #endregion
       Info inf = new Info();
        string frmna = "";

        ItemDirectoryDB ItemDirectoryDB = new ItemDirectoryDB();


# region INVOCE DESIGN
        public DataTable Details = new DataTable();
        public DataTable CustomerDetails = new DataTable();
        public DataTable SalesHDR = new DataTable();
        public DataTable SalesDTL = new DataTable();

        public string CODE = "";
        public string DOC_NO = "";
        public string TYPE = "";
        public int hgt = 0;
#endregion
        public void selection(string templatename, string entryno,string formname)
        {
            frmna = formname;
            #region notimportant
            if (formname == "Transportation")
            {
                
                cmd = new SqlCommand("select Transportation_dtl.* ,Transporter.tName as [ITEM_NAME] from Transportation_dtl inner join Transporter on Transportation_dtl.CLIENT = Transporter.Id  where Transportation_dtl.INVOICE_NO ='" + entryno + "'order by Transportation_dtl.SL_NO asc");
                sales_dtl = inf.get_genaraldata(cmd);
                cmd = new SqlCommand("select  Transportation.*,customer.*,EMP_EMPLOYEES.* from Transportation inner join customer on Transportation.CUSTOMER=CUSTOMER.Ledger_id  inner join EMP_EMPLOYEES on Transportation.SALES_MAN=EMP_EMPLOYEES.ID   where Transportation.invoice_no='" + entryno + "'");
                sales = inf.get_genaraldata(cmd);
            }
            else if(formname == "Transport")
        {

                cmd = new SqlCommand("select  Transport_Dtl.* ,tb_Ledgers.*, tb_ledgers.arabic_name as[description] from Transport_Dtl inner join tb_Ledgers on Transport_Dtl.ITEM =tb_Ledgers.LEDGERID  where Transport_Dtl.INVOICE_NO='" + entryno + "'order by transport_dtl.SL_NO asc");
                sales_dtl = inf.get_genaraldata(cmd);

                cmd = new SqlCommand("select  Transport.*,customer.*,EMP_EMPLOYEES.*,transporter.* from Transport inner join customer on Transport.CUSTOMER=CUSTOMER.Ledger_id  inner join EMP_EMPLOYEES on Transport.SALES_MAN=EMP_EMPLOYEES.ID inner join transporter on transporter.id=Transport.TRANSPORTER  where Transport.invoice_no='" + entryno + "'");
                sales = inf.get_genaraldata(cmd);
            }

            else
            {

           
                cmd = new SqlCommand("select   SALES_DTL.*,item.*,item.ITEM_NAME from SALES_DTL inner join ITEM on SALES_DTL.ITEM_CODE =ITEM.ITEM_CODE where SALES_DTL.[status]='True' and SALES_DTL.INVOICE_NO ='" + entryno + "'order by SALES_DTL.SL_NO asc");
                sales_dtl = inf.get_genaraldata(cmd);

                cmd = new SqlCommand("select  sales.*,customer.*,EMP_EMPLOYEES.* from sales inner join customer on SALES.CUST_NAME=CUSTOMER.Ledger_id  inner join EMP_EMPLOYEES on SALES.SALESMAN=EMP_EMPLOYEES.ID  where sales.invoice_no='" + entryno + "'");
                sales = inf.get_genaraldata(cmd);

            }

            #endregion
            cmd = new SqlCommand("select * from company");
           // company = inf.get_genaraldata(cmd);
            cmd = new SqlCommand("select * from  invoiceprnline where template='" + templatename + "' and visible='True' ");
            labels = inf.get_genaraldata(cmd);
            cmd = new SqlCommand("select * from a4columns   where template='" + templatename + "' and visible='True'  ORDER BY [INDEX]");
           // gridview = inf.get_genaraldata(cmd);
            cmd = new SqlCommand("select * from invoice_lines where template='" + templatename + "'");
            lines = inf.get_genaraldata(cmd);
            cmd = new SqlCommand("select * from INVOICE_a4_general where template='" + templatename + "'");
            settings = inf.get_genaraldata(cmd);
            cmd = new SqlCommand("select * from invoice_rectangle where template='" + templatename + "'");
           
        }
        public laser_print()
        {
           
            cmd = new SqlCommand("SELECT * FROM INVOICE_DETAILS");
            Details = inf.get_genaraldata(cmd);

           
        }
        public laser_print(string code, string doc_no)
        {
            CODE = code;
            DOC_NO = doc_no;
            cmd = new SqlCommand("SELECT * FROM INVOICE_DETAILS");
            Details = inf.get_genaraldata(cmd);

            cmd = new SqlCommand("SELECT * FROM REC_CUSTOMER WHERE CODE='"+CODE+"'");
            CustomerDetails = inf.get_genaraldata(cmd);

            cmd = new SqlCommand("SELECT * FROM INV_SALES_HDR WHERE DOC_NO='" + DOC_NO + "'");
            SalesHDR = inf.get_genaraldata(cmd);

            cmd = new SqlCommand("SELECT * FROM INV_SALES_DTL WHERE DOC_NO='" + DOC_NO + "'");
            SalesDTL = inf.get_genaraldata(cmd);

        }
       public laser_print(string code, string doc_no, DataTable HDR, DataTable DTL)
       {
           CODE = code;
           DOC_NO = doc_no;
           cmd = new SqlCommand("SELECT * FROM INVOICE_DETAILS");
           Details = inf.get_genaraldata(cmd);
           cmd = new SqlCommand("SELECT * FROM REC_CUSTOMER WHERE CODE='" + CODE + "'");
           CustomerDetails = inf.get_genaraldata(cmd);
           SalesHDR = HDR;
           SalesDTL = DTL;
           
       }
        public void selectionMultiSales(string entryno)
        {
            
              cmd = new SqlCommand("select   SALES_DTL.*,item.*from SALES_DTL inner join ITEM on SALES_DTL.ITEM_CODE =ITEM.ITEM_CODE where SALES_DTL.[status]='True' and SALES_DTL.INVOICE_NO ='" + entryno + "'order by SALES_DTL.SL_NO asc");
              sales_dtl = inf.get_genaraldata(cmd);
              cmd = new SqlCommand("select  sales.*,customer.*,EMP_EMPLOYEES.* from sales inner join customer on SALES.CUST_NAME=CUSTOMER.Ledger_id  inner join EMP_EMPLOYEES on SALES.SALESMAN=EMP_EMPLOYEES.ID  where sales.invoice_no='" + entryno + "'");
              sales = inf.get_genaraldata(cmd);
              cmd = new SqlCommand("select * from company");
              company = inf.get_genaraldata(cmd);
            
        }

        public Font Invoicefont = new Font("Calibri", 36, FontStyle.Bold);
        List<string> printers = new List<string>();
        void Token_PrintPage(object sender, PrintPageEventArgs e)
        {
         
        StringFormat formatLeft = new StringFormat(StringFormatFlags.NoClip);
            StringFormat formatCenter = new StringFormat(formatLeft);
            StringFormat formatRight = new StringFormat(formatLeft);
            float leading = 4;


            float startX = e.PageSettings.PaperSize.Width / 2;
            float startY = e.PageSettings.PaperSize.Height / 2;
            float Offset = 0;
            float lineheight14 = Invoicefont.GetHeight() + leading;
            SizeF layoutSize = new SizeF(e.PageSettings.PaperSize.Width - Offset * 2, lineheight14);
            RectangleF layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);

            formatCenter.Alignment = StringAlignment.Center;
            formatRight.Alignment = StringAlignment.Far;
            formatLeft.Alignment = StringAlignment.Near;



            //  e.Graphics.DrawString(txt_InvcNo.Text, Invoicefont, new SolidBrush(invoicecolor), layout, formatCenter );
            e.Graphics.DrawString(tokenno, Invoicefont, new SolidBrush(Color.Black), e.PageSettings.PaperSize.Width / 2, e.PageSettings.PaperSize.Height / 2 - lineheight14 / 2, formatCenter);

            e.HasMorePages = false;

        }
        string tokenno;
        public void getallprinter()
        {
            if (printers.Count > 0)
            {

            }
            else
            {

                foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
                {
                    printers.Add(printer);

                }
            }
        }
        public void tokenprinting()
        {


            PrintDocument Token = new PrintDocument();


            Token.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("MediumSize", 300, 150);
         

            Token.PrintPage += Token_PrintPage;

            foreach (var items in printers)
            {
                if
                 (items.EndsWith("_DB"))

                {
                    try
                    {
                        Token.PrinterSettings.PrinterName = items;
                        Token.Print();
                    }
                    catch (Exception ex)
                    {


                    }
                }
            }

        }


        public string ArabicNumerals(string input)
        {
            System.Text.UTF8Encoding utf8Encoder = new UTF8Encoding();
            System.Text.Decoder utf8Decoder = utf8Encoder.GetDecoder();
            System.Text.StringBuilder convertedChars = new System.Text.StringBuilder();
            char[] convertedChar = new char[1];
            byte[] bytes = new byte[] { 217, 160 };
            char[] inputCharArray = input.ToCharArray();
            foreach (char c in inputCharArray)
            {
                if (char.IsDigit(c))
                {
                    bytes[1] = Convert.ToByte(160 + char.GetNumericValue(c));
                    utf8Decoder.GetChars(bytes, 0, 2, convertedChar, 0);
                    convertedChars.Append(convertedChar[0]);
                }
                else
                {
                    convertedChars.Append(c);
                }
            }
            return convertedChars.ToString();
        }

        #region thermalprinting


      public   DataTable ThermalSalesDt = new DataTable();

      void CreateDt()
        {


            if (ThermalSalesDt.Rows.Count > 0)
                ThermalSalesDt.Rows.Clear();
            if (ThermalSalesDt.Columns.Count > 0)
                ThermalSalesDt.Columns.Clear();
            ThermalSalesDt.Columns.Add(new DataColumn("SL_No", typeof(string)));
            ThermalSalesDt.Columns.Add(new DataColumn("ITEM_NAME", typeof(string)));
            ThermalSalesDt.Columns.Add(new DataColumn("PRICE", typeof(string)));
            ThermalSalesDt.Columns.Add(new DataColumn("QTY", typeof(string)));

          

            ThermalSalesDt.Columns.Add(new DataColumn("TOTAL", typeof(string)));


        }

        public int pgeheight = 0;
        public void ThermalPgeDef()
        {
            // int i=0;

          
            CreateDt();


          

            foreach (DataRow row in sales_dtl.Rows)
            {
                bool want = true;
                bool Arabic = true;
                //  = 25;// Convert.ToInt32(Inventory.Properties.Settings.Default.length.ToString());
                string word = "";
                string hed = "";
                rowLength =14 ;
                StringBuilder result = new StringBuilder();
                StringBuilder line = new StringBuilder();

              

                if (word == "" && tail == "")
                {
                    string input = "";
                    //draw item name
                    if (frmna == "Sales" || frmna == "Transportation")
                    {
                        input = row["ITEM_NAME"].ToString();
                    }
                    else
                    {
                        input = row["LEDGERNAME"].ToString();

                    }
                    stack.Push(input);
                    want = true;
                }


                while (stack.Count > 0)
                {
                    word = stack.Pop();
                    if (word != "")
                    {
                        if (word.Length > rowLength)
                        {
                            hed = word.Substring(0, rowLength);
                            tail = word.Substring(rowLength);

                            word = hed;
                            stack.Push(tail);
                            pgeheight++;
                        }

                        else
                        {
                            hed = word;
                            word = "";
                            tail = "";
                            pgeheight++;
                        }
                        result.AppendLine(line.ToString());
                        linecount++;


                    }







                    DataRow newrow =ThermalSalesDt.NewRow();

                    DataRow newrowArabic = ThermalSalesDt.NewRow();
                    if (Arabic)
                    {
                        newrowArabic["ITEM_NAME"] = row["Description"].ToString();
                        newrowArabic["SL_NO"] = "";
                        newrowArabic["QTY"] = "";
                        newrowArabic["PRICE"] = "";
                        newrowArabic["TOTAL"] = "";
                        ThermalSalesDt.Rows.Add(newrowArabic);
                        Arabic = false;

                    }
                    if (want)
                    {
                        newrow["ITEM_NAME"] = hed;
                        newrow["SL_NO"] = row["Sl_NO"];
                        newrow["QTY"] = row["QTY"];
                        newrow["PRICE"] = row["ITEM_PRICE"];
                        newrow["TOTAL"] = row["GRAND_TOTAL"];
                        want = false;
                      ThermalSalesDt.Rows.Add(newrow);
                    }
                    else
                    {
                        newrow["ITEM_NAME"] = hed;
                        newrow["SL_NO"] = "";
                        newrow["QTY"] = "";
                        newrow["PRICE"] = "";
                        newrow["TOTAL"] = "";
                        ThermalSalesDt.Rows.Add(newrow);
                    }
                  




                }
            }

            stack.Clear();
            queue.Clear();

        }

        public Font FontHead = new Font("Times New Roman", 12, FontStyle.Bold);
        public Font FontItem = new Font("Times New Roman", 8, FontStyle.Regular);
        public Font FontItemHead = new Font("Times New Roman", 10, FontStyle.Regular);

        public void PrintThermal(string entryno,int Selectedvalue)
        {
            tokenno = entryno;
            getallprinter();
            cmd = new SqlCommand("select   SALES_DTL.*,item.*,item.ITEM_NAME from SALES_DTL inner join ITEM on SALES_DTL.ITEM_CODE =ITEM.ITEM_CODE where SALES_DTL.[status]='True' and SALES_DTL.INVOICE_NO ='" + entryno + "'order by SALES_DTL.SL_NO asc");
            sales_dtl = inf.get_genaraldata(cmd);
            cmd = new SqlCommand("select  sales.*,customer.*,EMP_EMPLOYEES.* ,tickettype.name as [tickettype] from sales inner join customer on SALES.CUST_NAME=CUSTOMER.Ledger_id  inner join EMP_EMPLOYEES on SALES.SALESMAN=EMP_EMPLOYEES.ID inner join tickettype on tickettype.id=sales.ticket_type where sales.invoice_no='" + entryno + "'");
            sales = inf.get_genaraldata(cmd);
            cmd = new SqlCommand("select * from company");
            company = inf.get_genaraldata(cmd);
            frmna = "Sales";
              ThermalPgeDef();
            string dd = ThermalSalesDt.Rows[0][0].ToString();
            PrintDocument printData = new PrintDocument();
            selectedval = Selectedvalue;
            pgeheight =580+(ThermalSalesDt.Rows.Count* 15);

            printData.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("ps",300, pgeheight);
      
            printData.PrinterSettings.DefaultPageSettings.Margins.Left = 0;
            printData.PrinterSettings.DefaultPageSettings.Margins.Right = 0;
            printData.PrinterSettings.DefaultPageSettings.Margins.Top = 0;
            printData.PrinterSettings.DefaultPageSettings.Margins.Bottom = 0;
       
            printData.PrintPage += newthermalprintPrintPage;

            foreach (var items in printers)
            {
                if
                 (items.StartsWith("DB_"))

                {
                    try
                    {
                       
                        printData.PrinterSettings.PrinterName = items;
                        printData.Print();
                    }
                    catch (Exception ex)
                    {


                    }
                }
            }




            //if (Properties.Settings.Default.print_token == true)
            //{

            //    try
            //    {
                   
            //        //  e.Graphics.DrawString(txt_InvcNo.Text, Invoicefont, new SolidBrush(invoicecolor), layout, formatCenter );
            //        // e.Graphics.DrawString(token, Invoicefont, new SolidBrush(invoicecolor), e.PageSettings.PaperSize.Width / 2, e.PageSettings.PaperSize.Height / 2 - lineheight14 / 2, formatCenter);

            //        cmd = new SqlCommand("Update settings set token=token+1");
           
            //        inf.get_genarlExecution(cmd);



                 
            //    }
            //    catch (Exception ex)
            //    {

            //        MessageBox.Show(ex.Message, "Token Printing");
            //    }
            //}

            //if (Properties.Settings.Default.cashdrwer == true)
            //{

            //   // System.Diagnostics.Process.Start("cmd", "/c type drawer >> com7");
            //}



        }
       
        public DataTable bill = new DataTable();
        void newthermalprintPrintPage(object sender, PrintPageEventArgs e)
        {
            float xpos = 0, ypos = 0, vatx = 0, custnamex = 0, smanx = 0, invoicex = 0, datex, slnox = 5, descriptionx = 40, qtyx = 145, pricex =190, totalx = 240;
            //   var txtDataWidth = e.Graphics.MeasureString(head, printFont).Width;
            Point point3 = new Point(25, 219);
            Point point4 = new Point(800, 219);
            int addy = 15;

            float[] dashValues = { 5, 5, 5, 5 };
            Pen kPen = new Pen(Color.Black, 1);
            kPen.DashPattern = dashValues;

        //
        //   e.Graphics.DrawLine(kPen, 0, right + heading, pwidth, right + heading);
        //
        // e.Graphics.DrawLine(kPen, 0, right, pwidth, right);

        Pen blackPen = new Pen(Color.Black, 1);

            using (var sf = new StringFormat())
            {
                //=\] height += 15;
                sf.LineAlignment = StringAlignment.Center;
                sf.Alignment = StringAlignment.Center;

                xpos = e.MarginBounds.Left + (e.MarginBounds.Width / 2);
                ypos =30;

                e.Graphics.DrawString(Info.companyName, FontItemHead, new SolidBrush(Color.Black), xpos,ypos,sf);
                ypos += 20;
                e.Graphics.DrawString(Info.companyArabicName, FontItemHead, new SolidBrush(Color.Black), xpos,ypos, sf);
                ypos += 20;
                e.Graphics.DrawString(Info.Address, FontItemHead, new SolidBrush(Color.Black), xpos, ypos, sf);
                ypos += 20;
               // e.Graphics.DrawString(, FontItemHead, new SolidBrush(Color.Black), xpos, ypos, sf);
                e.Graphics.DrawString("Mob No "+" :"+Info.Cmobno, FontItemHead, new SolidBrush(Color.Black), xpos, ypos, sf);
                
                ypos += 20;
                // StringBuilder gen = new StringBuilder();

               string  vatno = "VATNO[الرقم ضريبة]";

                // gen.Append("                                                                                                                        ");
                // gen.Insert(1, vatno);
                // gen.Insert(30, classes.Info.VATNO.ToString());
                  //string vv= vatno.classes.Info.VATNO.ToString() ;

                var txtDataWidth = e.Graphics.MeasureString(vatno,FontHead).Width;
                e.Graphics.DrawString(vatno, FontItemHead, new SolidBrush(Color.Black), slnox, ypos);
               e.Graphics.DrawString(":"+Info.VATNO.ToString().Trim(), FontItemHead, new SolidBrush(Color.Black), slnox+txtDataWidth, ypos);
                ypos += 20;
                //if (Properties.Settings.Default.print_tokentype == true)
                //{
                //    e.Graphics.DrawString(sales.Rows[0]["tickettype"].ToString(), FontItemHead, new SolidBrush(Color.Black), xpos, ypos, sf);



                //    ypos += 20;
                //}


            ///    if (Properties.Settings.Default.print_token == true)
                 if (false)
                {

                    try
                    {
                        cmd = new SqlCommand("select isnull(token,0) from settings");
                        string token = inf.generalexecutescalar(cmd);
                        
                        e.Graphics.DrawString("Token No" + " :" + token, FontItemHead, new SolidBrush(Color.Black), xpos, ypos, sf);
                       
                        ypos += 20;
                    }
                    catch(Exception ex)
                    {

MessageBox.Show(ex.Message,"Token Printing");
                    }
                    }
                


                e.Graphics.DrawString("Customer  [اسم الزبون]" + " :" + sales.Rows[0]["Name"], FontItemHead, new SolidBrush(Color.Black), slnox, ypos);
                ypos += 20; 
                e.Graphics.DrawString("SalesMan" + " :" + sales.Rows[0]["F_Name"].ToString(), FontItemHead, new SolidBrush(Color.Black), slnox, ypos);

                ypos += 20;
                e.Graphics.DrawLine(kPen,0, ypos , e.MarginBounds.Left + e.MarginBounds.Right,ypos);
                ypos += 5;

                string invoic = "Invoice No[رقم الفاتورة]";
                txtDataWidth = e.Graphics.MeasureString(invoic, FontItemHead).Width;
                e.Graphics.DrawString(invoic,FontItemHead, new SolidBrush(Color.Black), slnox, ypos);

                e.Graphics.DrawString(":"+sales.Rows[0]["Invoice_No"].ToString(), FontItemHead, new SolidBrush(Color.Black), slnox+ txtDataWidth, ypos);
                ypos += 20;

                string date = "Date  [تاريخ]";
                txtDataWidth = e.Graphics.MeasureString(date, FontItemHead).Width;
                e.Graphics.DrawString(date, FontItemHead, new SolidBrush(Color.Black), slnox, ypos);
                e.Graphics.DrawString(" :" +Convert.ToDateTime(sales.Rows[0]["Date"]).ToShortDateString(), FontItemHead, new SolidBrush(Color.Black), slnox+txtDataWidth, ypos);
                ypos += 20;


                e.Graphics.DrawLine(kPen, 0, ypos, e.MarginBounds.Left + e.MarginBounds.Right, ypos);



               float xminus = 35;

                e.Graphics.DrawString("SlNo", FontItemHead, new SolidBrush(Color.Black),slnox,ypos);
                e.Graphics.DrawString("Description", FontItemHead, new SolidBrush(Color.Black), descriptionx, ypos);
                e.Graphics.DrawString("Qty", FontItemHead, new SolidBrush(Color.Black), pricex- xminus, ypos);
                e.Graphics.DrawString("Price", FontItemHead, new SolidBrush(Color.Black), totalx- xminus, ypos);
                e.Graphics.DrawString("Total", FontItemHead, new SolidBrush(Color.Black), (e.MarginBounds.Left + e.MarginBounds.Right)- (xminus+14), ypos);
                  ypos += addy;
                e.Graphics.DrawString("رقم", FontItemHead, new SolidBrush(Color.Black), slnox, ypos);
                e.Graphics.DrawString("الوصف", FontItemHead, new SolidBrush(Color.Black), descriptionx, ypos);
                e.Graphics.DrawString("الكمية", FontItemHead, new SolidBrush(Color.Black), pricex- xminus, ypos);
                e.Graphics.DrawString("السعر", FontItemHead, new SolidBrush(Color.Black), totalx- xminus, ypos);
                e.Graphics.DrawString("الإجمالي", FontItemHead, new SolidBrush(Color.Black), (e.MarginBounds.Left + e.MarginBounds.Right) - (xminus + 14), ypos);
                ypos += addy+1;
                e.Graphics.DrawLine(kPen, 0, ypos, e.MarginBounds.Left + e.MarginBounds.Right, ypos);

                StringFormat format = new StringFormat(StringFormatFlags.DirectionRightToLeft);
                foreach (DataRow row in ThermalSalesDt.Rows)
                {
                    ypos += addy;

                    e.Graphics.DrawString(row["Sl_NO"].ToString(), FontItem, new SolidBrush(Color.Black), slnox, ypos);
                    e.Graphics.DrawString(row["ITEM_NAME"].ToString(), FontItem, new SolidBrush(Color.Black), descriptionx, ypos);
                    e.Graphics.DrawString(row["QTY"].ToString(), FontItem, new SolidBrush(Color.Black), pricex-5, ypos, format);
                    e.Graphics.DrawString(row["PRICE"].ToString(), FontItem, new SolidBrush(Color.Black), totalx-5, ypos, format);
                    e.Graphics.DrawString(row["TOTAL"].ToString(), FontItem, new SolidBrush(Color.Black), (e.MarginBounds.Left + e.MarginBounds.Right)-18, ypos, format);

                  
                }
                addy = 20;
                ypos += addy;
                e.Graphics.DrawLine(kPen, 0, ypos, e.MarginBounds.Left + e.MarginBounds.Right, ypos);


                ypos += addy;
                e.Graphics.DrawString("Gross Total   [ الإجمالي]", FontItemHead, new SolidBrush(Color.Black), slnox, ypos);
                e.Graphics.DrawString(sales.Rows[0]["GROSS_TOTAL"].ToString(), FontItemHead, new SolidBrush(Color.Black),totalx, ypos, format);
                ypos += addy;
                e.Graphics.DrawString("VAT ضريبة]   %5]", FontItemHead, new SolidBrush(Color.Black), slnox, ypos);

                e.Graphics.DrawString( sales.Rows[0]["TAX"].ToString(), FontItemHead, new SolidBrush(Color.Black), totalx, ypos,format);
                ypos += addy;

                e.Graphics.DrawString("Discount [الخصم]", FontItemHead, new SolidBrush(Color.Black), slnox, ypos);
                e.Graphics.DrawString( sales.Rows[0]["DISC"].ToString(), FontItemHead, new SolidBrush(Color.Black), totalx, ypos, format);
                ypos += addy;

                e.Graphics.DrawString("GrandTotal   [المبلغ الإجمالي]", FontItemHead, new SolidBrush(Color.Black), slnox, ypos);
                e.Graphics.DrawString( sales.Rows[0]["GRAND_TOTAL"].ToString(), FontHead, new SolidBrush(Color.Black), totalx, ypos, format);
                ypos += addy;

                e.Graphics.DrawString("Cash Tenderd   [نقد]", FontItemHead, new SolidBrush(Color.Black), slnox, ypos);
                e.Graphics.DrawString( sales.Rows[0]["Received"].ToString(), FontItemHead, new SolidBrush(Color.Black), totalx, ypos, format);
                ypos += addy;
                e.Graphics.DrawString("Change   [الباقي]", FontItemHead, new SolidBrush(Color.Black), slnox, ypos);
                e.Graphics.DrawString( sales.Rows[0]["balance"].ToString(), FontItemHead, new SolidBrush(Color.Black), totalx, ypos, format);
                ypos += addy;


                e.Graphics.DrawLine(kPen, 0, ypos, e.MarginBounds.Left + e.MarginBounds.Right, ypos);
                //loadcurrency();
                double cash_inwords = (double)Convert.ToDouble(sales.Rows[0]["GRAND_TOTAL"]);

                //NumbersToWords.ToWord toWord = new NumbersToWords.ToWord(Convert.ToDecimal(cash_inwords), currencies[selectedval]);
                ypos += addy;

               // e.Graphics.DrawString(toWord.ConvertToEnglish(), FontItem, new SolidBrush(Color.Black), xpos, ypos, sf);
                ypos += addy;
               // e.Graphics.DrawString(toWord.ConvertToArabic(), FontItem, new SolidBrush(Color.Black), xpos, ypos, sf);

















            }
        }
        #endregion

        public  FontStyle details = new FontStyle();

        public  int rloc=0;
        public void fonttype()
        {
            if (settings.Rows.Count > 0)
            {
                FontStyle hed = new FontStyle();

               
                if (settings.Rows[0]["heading_fontstyle"].ToString() == "Bold")
                    hed = FontStyle.Bold;
                else if (settings.Rows[0]["heading_fontstyle"].ToString() == "Italic")
                    hed = FontStyle.Italic;
                else
                    hed = FontStyle.Regular;


                if (settings.Rows[0]["details_fontstyle"].ToString() == "Bold")
                    details = FontStyle.Bold;
                else if (settings.Rows[0]["details_fontstyle"].ToString() == "Italic")
                    details = FontStyle.Italic;
                else
                    details = FontStyle.Regular;





                headfont = new Font(settings.Rows[0]["heading_fontname"].ToString(), (float)Convert.ToInt32(settings.Rows[0]["heading_fontsize"].ToString()), hed);

                detailsfont = new Font(settings.Rows[0]["details_fontname"].ToString(), (float)Convert.ToInt32(settings.Rows[0]["details_fontsize"].ToString()), details);
            }
        }
        public void printpages()
        {
            // int i=0;
            foreach (DataRow row in sales_dtl.Rows)
            {

                //  = 25;// Convert.ToInt32(Inventory.Properties.Settings.Default.length.ToString());
                string word = "";
                string hed = "";
                StringBuilder result = new StringBuilder();
                StringBuilder line = new StringBuilder();

                if (word == "" && tail == "")
                {
                    string input = "";
                    //draw item name
                    if (frmna == "Sales"|| frmna=="Transportation")
                    {
                        input = row["ITEM_NAME"].ToString();
                    }
                    else
                    {
                        input = row["LEDGERNAME"].ToString();

                    }
                    stack.Push(input);
                    isok = true;
                }
    

                while (stack.Count > 0)
                {
                    word = stack.Pop();
                    if (word != "")
                    {
                        if (word.Length > rowLength)
                        {
                            hed = word.Substring(0, rowLength);
                            tail = word.Substring(rowLength);
                            
                            word = hed;
                            stack.Push(tail);
                        }

                        else
                        {
                            hed = word;
                            word = "";
                            tail = "";
                        }
                        result.AppendLine(line.ToString());
                        linecount++;
       

                    }
                }
            }
           
            stack.Clear();
            queue.Clear();

        }

        public string datatype=" ";
        private string GetFormatedText(string Cont, int Length)
        {
            string space = "";
            decimal io = 0;
            if (decimal.TryParse(Cont, out io) )
           // Cont = GlobalFunc.DecimalConversion(Cont, GlobalVar.MoneyDecimals, false).ToString(GlobalFunc.StrDecimal(GlobalVar.MoneyDecimals));
            rloc = Length - Cont.Trim().Length;
          
            string Code = Cont;
            //if (rLoc < 0)
            //{
            //    Cont = Cont.Substring(0, Length);
            //    Code = Cont;
            //}
            //else
            //{
            //    int nos;
            //    for (nos = 0; nos < rLoc; nos++)
            //    {
            //        space += "";

            //    }
            //    if (!decimal.TryParse(Cont, out io))
            //    {
            //        Cont += space;
            //        Code = Cont;
            //    }
            //    else
            //    {
                 
                    
            //        space += Cont;
            //        Code = space;
            //    }
           // }
            return (Code);
        }

        public bool ispreprint = false;
        public void print(string templatename,string entryno)
        {
            template = templatename;
           // ispreprint = Convert.ToBoolean(Inventory.Properties.Settings.Default.is_preprint.ToString());

            PrintDocument printa4 = new PrintDocument();
            //  PaperSize ps = new PaperSize("ps", 500, 200);

            //  ps.RawKind = (int)PaperKind.A4;
            //  ps.Height = 1100;
            // ps.Width = 840;
            //    ps.Height = 2000;
            // ps.Width = 840;
            selection(templatename, entryno,"Sales");
            int paperwidth; //Convert.ToInt32(settings.Rows[0]["paper_width"]);
            int paperheight; //Convert.ToInt32(settings.Rows[0]["paper_height"]);
    
          
            if (settings.Rows.Count > 0)
            {

                paperwidth = Convert.ToInt32(settings.Rows[0]["paper_width"]);
                paperheight = Convert.ToInt32(settings.Rows[0]["paper_height"]);
                rowLength= Convert.ToInt32(settings.Rows[0]["name_length"]);
                rowheightadd= Convert.ToDouble(settings.Rows[0]["row_height"]);
               // = Convert.ToDouble(settings.Rows[0]["row_height"]);

                printpages();
                int height = Convert.ToInt32(settings.Rows[0]["details_height"]);
                add_gridheight = linecount * (Convert.ToInt32(rowheightadd*2) )+ 60;//+100);
                // if (paperheight <= 0)
                // {
                paperheight = add_gridheight + Convert.ToInt32(settings.Rows[0]["details_height"]);
                    printerheight = false;
               // }
              


                printa4.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("ps", paperwidth,paperheight);
            }
            else
            {

                MessageBox.Show("Pleas set Paper Size");

                goto end;

            }


            fonttype();


            #region fullpage
            if (printerheight)
            {
                int length = linecount;//p sales_dtl.Rows.Count;
                                       //  int result = length / morepage;

                double k;

                ss:
                k = (double)length / morepage;

                if (k > 1)
                {

                    length = length - maxline;

                    fullpage++;

                    goto ss;
                }
            }

            #endregion;

            numberofpage = fullpage+1 ;
            //   printa4.DefaultPageSettings.PaperSize =ps;
            printa4.PrinterSettings.DefaultPageSettings.Margins.Left = 0;
            printa4.PrinterSettings.DefaultPageSettings.Margins.Right = 0;
            printa4.PrinterSettings.DefaultPageSettings.Margins.Top = 0;
            printa4.PrinterSettings.DefaultPageSettings.Margins.Bottom = 0;
            printa4.PrintPage += PrintPage;

    
            printa4.Print();
            end:
            string ll = "";

        }


        /*  public void sales_assign()
           {

             foreach(DataRow row in sales_dtl.Rows)
               {

                   if (a == "ITEM_NAME")
                   {
                       Font font = new Font("Times New Roman", 10);
                       int rowLength = Convert.ToInt32(Inventory.Properties.Settings.Default.length.ToString());
                       string word = "";

                       string hed = "";
                       StringBuilder result = new StringBuilder();
                       StringBuilder line = new StringBuilder();

                       if (word == "" && tail == "")
                       {
                           //draw item name
                           string input = rows[a].ToString();
                           stack.Push(input);
                           isok = true;
                       }

                    // nameheight = Convert.ToInt32(rowheight);
                      //ame_morepage = morepage;

                       while (stack.Count > 0)
                       {
                           word = stack.Pop();
                           if (word != "")
                           {
                               if (word.Length > rowLength)
                               {
                                   hed = word.Substring(0, rowLength);
                                   tail = word.Substring(rowLength);

                                   word = hed;
                                   stack.Push(tail);
                               }

                               else
                               {
                                   hed = word;
                                   word = "";
                               }
                               result.AppendLine(line.ToString());
                               e.Graphics.DrawString(hed, fn, new SolidBrush(Color.Black), (float)psx, (float)psy + (float)nameheight);
                               nameheight = nameheight + 15;
                               //  name_morepage--;
                               //rowcount++;



                           }
                       }

                   }








               }




           }
           */

        //  void print
        //List<NumbersToWords.CurrencyInfo> currencies = new List<NumbersToWords.CurrencyInfo>();
        //void loadcurrency()
        //{

        //    currencies.Add(new NumbersToWords.CurrencyInfo(NumbersToWords.CurrencyInfo.Currencies.Syria));
        //    currencies.Add(new NumbersToWords.CurrencyInfo(NumbersToWords.CurrencyInfo.Currencies.UAE));
        //    currencies.Add(new NumbersToWords.CurrencyInfo(NumbersToWords.CurrencyInfo.Currencies.SaudiArabia));
        //    currencies.Add(new NumbersToWords.CurrencyInfo(NumbersToWords.CurrencyInfo.Currencies.Tunisia));
        //    currencies.Add(new NumbersToWords.CurrencyInfo(NumbersToWords.CurrencyInfo.Currencies.Gold));

        //   // cboCurrency.DataSource = currencies;
        //   // cboCurrency.SelectedValue = Convert.ToInt32(Properties.Settings.Default.Country);

        //    // cboCurrency_DropDownClosed(null, null);
        //}



        public void NewPrintDataMultiWholeSales(string tempname, string invoiceno, int Selectedvalue)
        {
            selectedval = Selectedvalue;
            PrintDocument printData = new PrintDocument();
            morepage =19 ;
            template = tempname;

           selectionMultiSales(invoiceno);

           // PaperSize ps = new PaperSize();
           // ps.RawKind = (int)PaperKind.A4;

            //printData.DefaultPageSettings.PaperSize = ps;

          //  int paperwidth=820;
           // int paperheight=11; 

            

           printData.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("ps",825,1150);




            numberofpage = fullpage + 1;
            //   printa4.DefaultPageSettings.PaperSize =ps;
            printData.PrinterSettings.DefaultPageSettings.Margins.Left = 0;
            printData.PrinterSettings.DefaultPageSettings.Margins.Right = 0;
            printData.PrinterSettings.DefaultPageSettings.Margins.Top = 0;
            printData.PrinterSettings.DefaultPageSettings.Margins.Bottom = 0;
            printData.PrinterSettings.PrinterName = Info.Currentprinter;
            printData.PrintPage += MultiWholSalePrint;


            printData.Print();


        }
        public void NewPrintData(string tempname,string invoiceno,int Selectedvalue)
        {
            selectedval = Selectedvalue;
            PrintDocument printData = new PrintDocument();

            template = tempname;
            // ispreprint = Convert.ToBoolean(Inventory.Properties.Settings.Default.is_preprint.ToString());

            // PrintDocument printa4 = new PrintDocument();
          //  ps = new PaperSize("ps", 500, 200);


            selection(template, invoiceno,"Sales");
            int paperwidth;// = Convert.ToInt32(settings.Rows[0]["paper_width"]);
            int paperheight; //= Convert.ToInt32(settings.Rows[0]["paper_height"]);

            if (settings.Rows.Count > 0)
            {

                ispreprint = Convert.ToBoolean(settings.Rows[0]["IS_PREPRINTED"]);
                paperwidth = Convert.ToInt32(settings.Rows[0]["paper_width"]);
                paperheight = Convert.ToInt32(settings.Rows[0]["paper_height"]);
                rowLength = Convert.ToInt32(settings.Rows[0]["name_length"]);
                rowheightadd = Convert.ToDouble(settings.Rows[0]["row_height"]);
                // = Convert.ToDouble(settings.Rows[0]["row_height"]);

                printpages();
                int height = Convert.ToInt32(settings.Rows[0]["details_height"]);
                //  add_gridheight = linecount * (Convert.ToInt32(rowheightadd * 2)) + 60;//+100);
                // if (paperheight <= 0)
                // {
                maxline = Convert.ToInt32(settings.Rows[0]["max_line"]);
                minline = Convert.ToInt32(settings.Rows[0]["min_line"]);
                morepage = Convert.ToInt32(settings.Rows[0]["min_line"]);
                if (!ispreprint)
                {
                    paperheight = add_gridheight + Convert.ToInt32(settings.Rows[0]["details_height"]);
                }
                //  printerheight = false;
              printData.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("ps", paperwidth, paperheight);
            }
            else
            {

                MessageBox.Show("Pleas set Paper Size");
                return;
              

            }

            printpages();
            fonttype();

           



            #region fullpage
            int length = linecount;//p sales_dtl.Rows.Count;
                                   //  int result = length / morepage;

            double k;

            ss:

            k = (double)length / morepage;

            if (k > 1)
            {

                length = length - maxline;

                fullpage++;

                goto ss;
            }

            #endregion;

            numberofpage = fullpage + 1;
            //   printa4.DefaultPageSettings.PaperSize =ps;
            printData.PrinterSettings.DefaultPageSettings.Margins.Left = 0;
            printData.PrinterSettings.DefaultPageSettings.Margins.Right = 0;
            printData.PrinterSettings.DefaultPageSettings.Margins.Top = 0;
            printData.PrinterSettings.DefaultPageSettings.Margins.Bottom = 0;
            printData.PrinterSettings.PrinterName = Info.Currentprinter;
            printData.PrintPage += printdata;


            printData.Print();


        }


        public void NewPrintData(string tempname, string invoiceno, int Selectedvalue,string formname)
        {
            selectedval = Selectedvalue;
            PrintDocument printData = new PrintDocument();

            template = tempname;
            // ispreprint = Convert.ToBoolean(Inventory.Properties.Settings.Default.is_preprint.ToString());

            // PrintDocument printa4 = new PrintDocument();
            //  ps = new PaperSize("ps", 500, 200);


            selection(template, invoiceno, formname);
            int paperwidth;// = Convert.ToInt32(settings.Rows[0]["paper_width"]);
            int paperheight; //= Convert.ToInt32(settings.Rows[0]["paper_height"]);

            if (settings.Rows.Count > 0)
            {

                ispreprint = Convert.ToBoolean(settings.Rows[0]["IS_PREPRINTED"]);
                paperwidth = Convert.ToInt32(settings.Rows[0]["paper_width"]);
                paperheight = Convert.ToInt32(settings.Rows[0]["paper_height"]);
                rowLength = Convert.ToInt32(settings.Rows[0]["name_length"]);
                rowheightadd = Convert.ToDouble(settings.Rows[0]["row_height"]);
                // = Convert.ToDouble(settings.Rows[0]["row_height"]);

                printpages();
                int height = Convert.ToInt32(settings.Rows[0]["details_height"]);
                //  add_gridheight = linecount * (Convert.ToInt32(rowheightadd * 2)) + 60;//+100);
                // if (paperheight <= 0)
                // {
                maxline = Convert.ToInt32(settings.Rows[0]["max_line"]);
                minline = Convert.ToInt32(settings.Rows[0]["min_line"]);
                morepage = Convert.ToInt32(settings.Rows[0]["min_line"]);
                if (!ispreprint)
                {
                    paperheight = add_gridheight + Convert.ToInt32(settings.Rows[0]["details_height"]);
                }
               
                printData.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("ps", paperwidth, paperheight);
                //  printerheight = false;
            }
            else
            {

                MessageBox.Show("Pleas set Paper Size");
                return;
              

            }

          // printpages();
            fonttype();





            #region fullpage
            int length = linecount;//p sales_dtl.Rows.Count;
                                   //  int result = length / morepage;

            double k;

            ss:

            k = (double)length / morepage;

            if (k > 1)
            {

                length = length - maxline;

                fullpage++;

                goto ss;
            }

            #endregion;

            numberofpage = fullpage + 1;
            //   printa4.DefaultPageSettings.PaperSize =ps;
            printData.PrinterSettings.DefaultPageSettings.Margins.Left = 0;
            printData.PrinterSettings.DefaultPageSettings.Margins.Right = 0;
            printData.PrinterSettings.DefaultPageSettings.Margins.Top = 0;
            printData.PrinterSettings.DefaultPageSettings.Margins.Bottom = 0;
            printData.PrinterSettings.PrinterName = Info.Currentprinter;
            printData.PrintPage += printdata;


            printData.Print();


        }
        public int selectedval;


        #region wholeSales
        public int i = 0, j = 0;
        string word = "", BAKKI = "";
        public bool WANT = true;
       int line_nos=0;
        private void MultiWholSalePrint(object sender, PrintPageEventArgs e)
        {

       //     morepage = 20;
            float xpos;
            int startx = 30;
            int starty = 30;
            int offset = 15;

            StringFormat format = new StringFormat(StringFormatFlags.DirectionRightToLeft);
            int w = e.MarginBounds.Width / 2;
            int x = e.MarginBounds.Left;
            int y = e.MarginBounds.Top;
            int addy = 20,slnox=0,codex=22,namex=130,ctnx=387,dznx=420,pcx=460,pricex=542,grossx=610,vatpx=660,vatax=715,totalx=790;
            Font Headerfont1 = new Font("Times New Roman", 14, FontStyle.Bold);
            Font Headerfont2 = new Font("Courier New", 12, FontStyle.Bold);
            Font printFont = new Font("Courier New", 10);
            var tabDataForeColor = Color.Black;
            int height = 100 + y;


            //  var txtDataWidth = e.Graphics.MeasureString(CompanyName, printFont).Width;
            y = 207;
            x = 623;

            using (var sf = new StringFormat())
            {
                height += 15;
                sf.LineAlignment = StringAlignment.Center;
                sf.Alignment = StringAlignment.Center;

                xpos = e.MarginBounds.Left + (e.MarginBounds.Width / 2);


                //  e.Graphics.DrawString(company.Rows[0]["TIN_No"].ToString(), printFont, new SolidBrush(tabDataForeColor), x, y);



                addy = 37;

                string input = "", hed = "";
                      StringBuilder result = new StringBuilder();
                StringBuilder line = new StringBuilder(); 
                e.Graphics.DrawString(sales.Rows[0]["Name"].ToString(), printFont, new SolidBrush(tabDataForeColor), 22, y+15);

                e.Graphics.DrawString(sales.Rows[0]["Address1"].ToString(), printFont, new SolidBrush(tabDataForeColor), 22, y+30);

                e.Graphics.DrawString(Convert.ToDateTime(sales.Rows[0]["Date"]).ToShortDateString(), Headerfont2, new SolidBrush(tabDataForeColor), x, y);

                y +=addy;
                e.Graphics.DrawString(sales.Rows[0]["Invoice_No"].ToString(), Headerfont1, new SolidBrush(tabDataForeColor), x,y);
                y += addy;

                e.Graphics.DrawString(sales.Rows[0]["Sal_type"].ToString(), Headerfont2, new SolidBrush(tabDataForeColor), x, y);
                e.Graphics.DrawString(sales.Rows[0]["tin_no"].ToString(), printFont, new SolidBrush(tabDataForeColor), 22, y+10);

                y +=100;

                addy = 25;

                foreach (DataRow row in sales_dtl.Rows)
                {
                    WANT = true;



                    j = sales_dtl.Rows.IndexOf(row);


                    if (sales_dtl.Rows.IndexOf(row) == i)
                    {
                        i = i + 1;

                        string slno, Qty,ItemCode,ItemName,CTN,DZN,PC, item_price,Total,description,grosstotal,vatp,vata;


                        slno = row["Sl_NO"].ToString();

                        Qty = row["qty"].ToString();
                        ItemCode = row["ITEM_NAME1"].ToString();
                        // ItemName = row["Item_Name"].ToString();
                        CTN = row["CTN"].ToString();
                        DZN =Convert.ToInt32(row["DZN"]).ToString();
                        PC =Convert.ToInt32(row["PC"]).ToString();
                        item_price = row["Item_Price"].ToString();
                        Total = row["Grand_Total"].ToString();
                        description = row["Description"].ToString();
                        grosstotal = row["Gross_Total"].ToString();
                        vatp = row["ITEM_TAXP"].ToString();
                        vata = row["ITEM_TAX"].ToString();

                        //if (Properties.Settings.Default.printdescription == true)
                        //{
                          
                        //    e.Graphics.DrawString(description, printFont, new SolidBrush(Color.Black), ctnx-5, y,format);
                        //    y += addy;
                        //}



                      ///  if (word == "" && BAKKI == "")
                       /// {
                            //draw item name
                         input = row["Item_Partno"].ToString();
                        ///    stack.Push(input);
                        ///    WANT = true;
                       // }

                        //  stack.Push(input);
                        rowLength = 19;


                        // while (stack.Count > 0)
                        //  {

                        ///  word = stack.Pop();
                        //  if (word != "")
                        // {
                        if (input != "")
                        {
                            if (input.Length > rowLength)
                            {
                                input = input.Substring(0, rowLength);
                                //  tail = word.Substring(rowLength);

                                // word = hed;
                                // stack.Push(tail);
                            }
                        }

                             //   else
                               // {
                                    //   line_nos = 0;
                               //     hed = word;
                              //      word = "";
                              ///  }

                             
                              //  result.AppendLine(line.ToString());


                             

                                e.Graphics.DrawString(input, Headerfont2, new SolidBrush(Color.Black), namex,y);


                               /// if (WANT)
                               // {

                                    e.Graphics.DrawString(slno, printFont, new SolidBrush(Color.Black), slnox, y);
                                    e.Graphics.DrawString(ItemCode, printFont, new SolidBrush(Color.Black), codex, y);
                                    e.Graphics.DrawString(CTN, printFont, new SolidBrush(Color.Black), ctnx , y,format);
                                    e.Graphics.DrawString(DZN, printFont, new SolidBrush(Color.Black), dznx, y,format);
                                    e.Graphics.DrawString(PC, printFont, new SolidBrush(Color.Black), pcx, y,format);
                                    e.Graphics.DrawString(item_price, printFont, new SolidBrush(Color.Black), pricex, y,format);
                                    e.Graphics.DrawString(Total, printFont, new SolidBrush(Color.Black), totalx, y,format);
                                    e.Graphics.DrawString(grosstotal, printFont, new SolidBrush(Color.Black), grossx, y,format);
                                    e.Graphics.DrawString(vatp, printFont, new SolidBrush(Color.Black), vatpx, y,format);
                                    e.Graphics.DrawString(vata, printFont, new SolidBrush(Color.Black), vatax, y,format);

                                   // WANT = false;
                              //  }

                                y += addy;




                                line.Clear();
                                word = ""; 

                                //     }

                                line.Append(word + " ");
                                line_nos = line_nos + 1;
                                if (line_nos == morepage)
                                {
                                    // line_nos = 0;
                                 

                                    e.HasMorePages = true;
                                    if (word != "")
                                    {
                                        BAKKI = word;
                                    }

                                    goto nxt;


                                }
                          //  }
                      

                       // }

                        result.Append(line);
                        line.Clear();
                        result.Clear();

                        nxt:

                        if (line_nos == morepage)
                        {


                            line_nos = 0;
                            i = i + 1;
                            return;
                        }




                    }
                    else
                    {
                        //i = row.Index + 1;
                        continue;
                        // i--; 
                    }
                }


            
           
                e.Graphics.DrawString(sales.Rows[0]["Gross_total"].ToString(), Headerfont2, new SolidBrush(Color.Black), totalx,870 , format);
                e.Graphics.DrawString(sales.Rows[0]["TAX"].ToString(), Headerfont2, new SolidBrush(Color.Black), totalx, 900, format);
                e.Graphics.DrawString(sales.Rows[0]["Grand_total"].ToString(), Headerfont2, new SolidBrush(Color.Black), totalx, 940, format);
                string printname = "";
                try
                {
                   // loadcurrency();
                    double cash_inwords = (double)Convert.ToDouble(sales.Rows[0]["GRAND_TOTAL"]);

                   // NumbersToWords.ToWord toWord = new NumbersToWords.ToWord(Convert.ToDecimal(cash_inwords), currencies[selectedval]);


                  //  printname = toWord.ConvertToEnglish();
                }
                catch(Exception ex)
                {

                    MessageBox.Show(ex.Message);
                    MessageBox.Show(selectedval.ToString());
                }


                //draw item name
                 input = printname;
                    stack.Push(input);
                y = 885;
            
                rowLength = 55;

                if (input.Length > 55)
                {
                    while (stack.Count > 0)
                    {

                        word = stack.Pop();
                        if (word != "")
                        {
                            if (word.Length > rowLength)
                            {
                                hed = word.Substring(0, rowLength);
                                tail = word.Substring(rowLength);

                                word = hed;
                                stack.Push(tail);
                            }

                            else
                            {
                                //   line_nos = 0;
                                hed = word;
                                word = "";
                            }


                            result.AppendLine(line.ToString());




                          

                            e.Graphics.DrawString(hed, printFont, new SolidBrush(Color.Black), 2,y);
                            y += 20;






                            line.Clear();
                            word = "";

                            //     }

                            line.Append(word + " ");
                          

                        }


                    }
                   
                }
                else
                {
                    e.Graphics.DrawString(printname, printFont, new SolidBrush(Color.Black), 2, 940);

                }

            }






            e.HasMorePages = false;
        }

       

       

    #endregion






       public  void printdata(object sender, PrintPageEventArgs e)
        {
            int xplus = 5;

            k = 0;
            pageno++;
            e.HasMorePages = false;
            // 
            //Convert.ToInt32(Inventory.Properties.Settings.Default.height.ToString());

            DataTable margins = new DataTable();
            margins = new invoiceLineBLL().getgeneral(template);
            if (margins.Rows.Count > 0)
            {

                language = margins.Rows[0]["language"].ToString();
            }
            else
            {
                MessageBox.Show("There Is no Design With This Template");
                return;
            }



            //int y = 0;
            int PrintId = 0;
            int ItemHieght = 0;



            Pen black = new Pen(Color.Black, 1);

            System.Drawing.Font fn = null;
            FontStyle fs1 = new FontStyle();

            cmd = new SqlCommand("select * from INVOICE_a4_general where template='" + template + "'");
            settings = inf.get_genaraldata(cmd);
            int panl_width = Convert.ToInt32(settings.Rows[0]["form_width"]);// this.main_pnl.ClientSize.Width;


            int pnl_height = Convert.ToInt32(settings.Rows[0]["form_height"]); //;556; //this.main_pnl.ClientSize.Height;


            double actualwidth = (double)Convert.ToDouble(margins.Rows[0]["paper_width"]) / panl_width;
            double actualheight = (double)Convert.ToDouble(margins.Rows[0]["paper_height"]) / pnl_height; ;

            //double actualwidth = (double)840 / panl_width;
            // double actualheight = (double)1100 / pnl_height; ;
          //  e.Graphics.DrawString("Page" + "  " + pageno + " " + "Of" + "    " + numberofpage, detailsfont, new SolidBrush(Color.Black), e.PageSettings.PaperSize.Width / 2, e.PageSettings.PaperSize.Height - 25);

            double left = 0;
            double right = 0;
            double width = 0;
            double height = 0;
            double rowheaderheight = 0;
            double rowheadery = 0;
            double bottom = 0;

            int nameheight = 0;
           // int morepage = 10;




            //details
            int inc = 0;

            //drawrectangle


            left = (double)Convert.ToDouble(margins.Rows[0]["location_x"]) * actualwidth;
            right = (double)Convert.ToDouble(margins.Rows[0]["location_y"]) * actualheight;
            width = (double)Convert.ToDouble(margins.Rows[0]["width"]) * actualwidth;
            height = (double)Convert.ToDouble(margins.Rows[0]["height"]) * actualheight;

            rowheaderheight = (double)Convert.ToDouble(margins.Rows[0]["rowheader_height"]) * actualheight;
            rowheadery = (double)Convert.ToDouble(margins.Rows[0]["right"]) * actualwidth;
            bottom = (double)Convert.ToDouble(margins.Rows[0]["bottom"]) * actualheight;



            if (fullpage > 0)
            {

                if (ispreprint)
                {

                    if (Convert.ToBoolean(margins.Rows[0]["draw_margin"]) == true)
                    {
                        e.Graphics.DrawRectangle(black, (float)left, (float)right, (float)width, (float)height);
                    }
                }
                else
                {
                    if (Convert.ToBoolean(margins.Rows[0]["draw_margin"]) == true)
                    {
                        e.Graphics.DrawRectangle(black, (float)left, (float)right, (float)width, (float)height + add_gridheight);
                    
                    }
                }

                morepage = maxline;


            }
            else
            {

                if (Convert.ToBoolean(margins.Rows[0]["draw_margin"]) == true)
                {

                    e.Graphics.DrawRectangle(black, (float)left, (float)right, (float)width, (float)height);
                 
                }

                morepage = minline;
            }

            fs1 = new FontStyle();
            fs1 = FontStyle.Bold;




            if (Convert.ToBoolean(margins.Rows[0]["draw_margin"]) == true)
            {
                e.Graphics.DrawLine(black, (float)left, (float)right + (float)rowheaderheight, (float)rowheadery, (float)right + (float)rowheaderheight);
            }
            if (fullpage > 0)
            {

            }
            else
            {
                if (Convert.ToBoolean(margins.Rows[0]["draw_margin"]) == true)
                {

                    e.Graphics.DrawLine(black, (float)left, (float)bottom - (float)rowheaderheight, (float)rowheadery, (float)bottom - (float)rowheaderheight);
                }
            }

            //  e.Graphics.DrawLine(black, 10, 25, 800, 25);


            #region headings

            int INCR = 0;
            double psx = 0, pnames_x = 0, psy = 0, pex = 0, pey = 0;

            Object PRE_X = null, PRE_Y = null;

            foreach (DataRow rows in gridview.Rows)
            {


                if (rows["visible"].ToString() == "True")
                {


                    try
                    {

                        string search = rows["TEXT"].ToString();




                        double sx = (double)Convert.ToDouble(rows["startx"]) * actualwidth;
                        double sy = (double)Convert.ToDouble(rows["starty"]) * actualheight;
                        double ex = (double)Convert.ToDouble(rows["endx"]) * actualwidth;
                        double ey = (double)Convert.ToDouble(rows["endy"]) * actualheight;
                        if (fullpage > 0)
                        {
                            if (!ispreprint)
                            {

                                //   sy = sy + addline;

                                ey = ey + addline;


                            }


                        }





                        fn = new Font("arial",
                              (float)6, fs1, System.Drawing.GraphicsUnit.Point);




                        if (k != 0)
                        {
                            if (Convert.ToBoolean(margins.Rows[0]["draw_line"]) == true)
                            {

                                e.Graphics.DrawLine(black, (float)sx, (float)sy, (float)sx, (float)ey);
                            }
                        }

                     
                        StringFormat format = new StringFormat(StringFormatFlags.MeasureTrailingSpaces);


                   

                        if (k != 0)
                        {
                            if (Convert.ToBoolean(margins.Rows[0]["is_cheader"]) == true)
                            {

                             
                                e.Graphics.DrawString(search, headfont, new SolidBrush(Color.Black), (float)sx +1, (float)sy + 5);

                            }
                        }
                        else
                        {
                            if (Convert.ToBoolean(margins.Rows[0]["is_cheader"]) == true)
                            {
                                e.Graphics.DrawString(search, headfont, new SolidBrush(Color.Black), (float)sx + 10, (float)sy + 5);
                            }
                        }


                        k++;




                    }
                    catch (Exception EX)
                    {

                        continue;

                    }


                }


            }
            #endregion

            #region details

            k = 0;

            int itemnameindex = 0;
            decimal outval;

            int j = 0; int i = 0;
            double rowheight = rowheaderheight + 5;
            try
            {
                if (frmna == "Sales")
                {
                    sales_dtl.Columns["ITEM_NAME"].SetOrdinal(sales_dtl.Columns.Count - 1);
                }
               else if(frmna == "Transport")
                {

                    sales_dtl.Columns["LEDGERNAME"].SetOrdinal(sales_dtl.Columns.Count - 1);
                }
                else if(frmna == "Transportation")
                    {

                    sales_dtl.Columns["CLIENT"].SetOrdinal(sales_dtl.Columns.Count - 1);
                }
            }
            catch
            {

            }
        

            foreach (DataRow rows in sales_dtl.Rows)
            {



                i++;

                if (rowcount >= i)
                {
                    continue;

                }

                // columncount = 0;

                foreach (DataColumn cl in sales_dtl.Columns)
                {


                    // ;
                    j++;
                    if (columncount > j)
                        continue;

                    string a = cl.ColumnName.ToString();

                    cmd = new SqlCommand("select [type] from a4columns where template='" + template + "' and name='" + a + "' ");
                    object string_type = inf.generalexecutescalar(cmd);

                    bool vis = false;
                    try
                    {

                        cmd = new SqlCommand("select visible from a4columns where template='" + template + "' and name='" + a + "' ");
                        object visb = inf.generalexecutescalar(cmd);

                        //vis = Convert.ToBoolean(visb);
                        if (visb.ToString() != "")

                        {
                            vis = Convert.ToBoolean(visb);
                        }
                        else
                        {
                            vis = false;

                        }
                        if (vis == false)
                        {
                            columncount++;
                            continue;
                        }

                        #region cc
                        //   DataRow row = gridview.Rows[0];
                        //  string rowValue = row["ColumnName"].ToString();
                        //   row = null;
                        //  row = gridview.Select("name='" + ss + "'");



                        //  DataRow[] newrow=null;

                        //       newrow = gridview.Select("name='"+ ss+"'");

                        // string vib = newrow[0].vaToString();
                        #endregion
                    }
                    catch (Exception exx)
                    {
                        columncount++;
                        continue;
                    }

                    fn = new Font("arial",
                          (float)6, fs1, System.Drawing.GraphicsUnit.Point);
                    if (vis == true)
                    {

                        cmd = new SqlCommand("select endx from a4columns where template='" + template + "' and name='" + a + "' ");
                        object startx = inf.generalexecutescalar(cmd);
                        cmd = new SqlCommand("select startx from a4columns where template='" + template + "' and name='" + a + "' ");
                        object pstartx = inf.generalexecutescalar(cmd);
                        cmd = new SqlCommand("select starty from a4columns where template='" + template + "' and  name='" + a + "' ");

                        object starty = inf.generalexecutescalar(cmd);


                        double sx = (double)Convert.ToDouble(startx) * actualwidth;
                        double sy = (double)Convert.ToDouble(starty) * actualheight;

                        double sxp = (double)Convert.ToDouble(pstartx) * actualwidth;




                        // if (k == 0)
                        // {
                        //  pnames_x = left;
                        //   psx = left;
                        // psy = right;
                        // }
                        //  else
                        // {
                        pnames_x = sxp;
                        psx = sx;
                        psy = sy;



                        if (!ispreprint)
                        {


                            //    psy=psy+



                        }
                        // }
                        //   fn = new Font("arial",
                        // (float)10, fs1, System.Drawing.GraphicsUnit.Point);


                        if (a.ToString() == "ITEM_NAME"|| a.ToString() == "LEDGERNAME"|| a.ToString() == "CLIENT")
                        {
                            // 
                            itemnameindex = rowindex;
                            nameheight = Convert.ToInt32(rowheight);
                            if (Convert.ToBoolean(margins.Rows[0]["is_description"]) == true)

                            {
                                if (frmna != "Transportation")
                                {
                                    StringFormat format = new StringFormat(StringFormatFlags.DirectionRightToLeft);
                                    e.Graphics.DrawString(rows["description"].ToString(), detailsfont, new SolidBrush(Color.Black), (float)psx - (float)xplus, (float)psy + (float)nameheight, format);

                                    nameheight = nameheight + Convert.ToInt32(margins.Rows[0]["row_height"]);
                                }




                            }
                            int rowLength = Convert.ToInt32(margins.Rows[0]["name_length"]);//= Convert.ToInt32(Inventory.Properties.Settings.Default.length.ToString());
                            string word = "";

                            string hed = "";
                            StringBuilder result = new StringBuilder();
                            StringBuilder line = new StringBuilder();

                            if (word == "" && tail == "")
                            {
                                //draw item name
                                string input = "";
                                if (frmna == "Transportation")
                                {
                                   input = rows["ITEM_NAME"].ToString();
                                }
                                else
                                {
                                   input = rows[a].ToString();

                                }
                                stack.Push(input);
                                isok = true;
                            }

                            //name_morepage = morepage;

                            while (stack.Count > 0)
                            {
                                word = stack.Pop();
                                if (word != "")
                                {
                                    if (word.Length > rowLength)
                                    {
                                        hed = word.Substring(0, rowLength);
                                        tail = word.Substring(rowLength);

                                        word = hed;
                                        stack.Push(tail);
                                    }

                                    else
                                    {
                                        hed = word;
                                        word = "";
                                        tail = "";
                                        columncount = 0;
                                        j = 0;
                                    }
                                    result.AppendLine(line.ToString());

                                    if (language == "English")
                                    {
                                        StringFormat format = new StringFormat(StringFormatFlags.DirectionRightToLeft);
                                        //      e.Graphics.DrawString(hed, detailsfont, new SolidBrush(Color.Black), (float)psx, (float)psy + (float)nameheight, format);
                                        e.Graphics.DrawString(hed, detailsfont, new SolidBrush(Color.Black), (float)pnames_x+(float)xplus, (float)psy + (float)nameheight);
                                    }
                                    else if (language == "Arabic")
                                    {


                                        StringFormat format = new StringFormat(StringFormatFlags.DirectionRightToLeft);
                                        e.Graphics.DrawString(hed, detailsfont, new SolidBrush(Color.Black), (float)psx + (float)xplus, (float)psy + (float)nameheight);
                                        // e.Graphics.DrawString(hed, detailsfont, new SolidBrush(Color.Black), (float)pnames_x, (float)psy + (float)nameheight);



                                    }
                                    nameheight = nameheight + Convert.ToInt32(Convert.ToInt32(margins.Rows[0]["row_height"]));
                                    //  name_morepage--;
                                    //rowcount++;

                                    int index = sales_dtl.Rows.IndexOf(rows);
                                    itemnameindex++;

                                    if (itemnameindex == morepage)
                                    {
                                        if (tail == "")
                                        {
                                            columncount = 0;

                                            rowcount++;
                                        }
                                        if (fullpage > 0)
                                        {

                                            fullpage--;
                                            e.HasMorePages = true;

                                            //  e.PageSettings.PaperSize.Height = 600;
                                        }
                                        else
                                        {

                                            e.HasMorePages = false;
                                        }
                                        if (rowindex < itemnameindex)
                                        {
                                            rowindex = itemnameindex;
                                            itemnameindex = 0;
                                        }

                                        goto nextpage;

                                    }





                                }
                            }
                            // if (itemnameindex > rowindex)
                            //{
                            //    rowindex = itemnameindex;
                            //}

                        }


                        else if(a.ToString()=="SL_NO")
                        {


                            string kk = a.ToString();
                            string val = rows[a].ToString();
                            if (rowheight < nameheight)
                            {
                                rowheight = nameheight;
                                nameheight = 0;

                            }
                            if (rowindex == itemnameindex)
                            {
                                rowindex = itemnameindex;
                                itemnameindex = 0;
                            }
                            try
                            {

                              //  StringFormat format = new StringFormat(StringFormatFlags.dire);

                                if (language == "English")
                                {
                                    //  string numbers = ArabicNumerals(rows[a].ToString());
                                    //  e.Graphics.DrawString(numbers, detailsfont, new SolidBrush(Color.Black), (float)psx, (float)psy + (float)rowheight, format);



                                    if (string_type.ToString() == "0")
                                    {
                                        e.Graphics.DrawString(rows[a].ToString(), detailsfont, new SolidBrush(Color.Black), (float)psx -20, (float)psy + (float)rowheight);
                                    }

                                    else if (string_type.ToString() == "1")
                                    {

                                        if (decimal.TryParse(rows[a].ToString(), out outval))
                                        {
                                            // e.Graphics.DrawString(Convert.ToDecimal(rows[a].ToString()).ToString(GlobalFunc.StrDecimal(GlobalVar.QtyDecimals)), detailsfont, new SolidBrush(Color.Black), (float)psx, (float)psy + (float)rowheight, format);
                                        }

                                    }

                                    else if (string_type.ToString() == "2")
                                    {

                                        if (decimal.TryParse(rows[a].ToString(), out outval))
                                        {
                                            //   e.Graphics.DrawString(Convert.ToDecimal(rows[a].ToString()).ToString(GlobalFunc.StrDecimal(GlobalVar.MoneyDecimals)), detailsfont, new SolidBrush(Color.Black), (float)psx, (float)psy + (float)rowheight, format);
                                        }

                                    }

                                    else
                                    {

                                        e.Graphics.DrawString(rows[a].ToString(), detailsfont, new SolidBrush(Color.Black), (float)psx -20, (float)psy + (float)rowheight);

                                    }
                                }
                                else if (language == "Arabic")
                                {
                                    string numbers = ArabicNumerals(rows[a].ToString());
                                    e.Graphics.DrawString(numbers, detailsfont, new SolidBrush(Color.Black), (float)psx, (float)psy + (float)rowheight);
                                    // e.Graphics.DrawString(rows[a].ToString(), detailsfont, new SolidBrush(Color.Black), (float)psx, (float)psy + (float)rowheight, format);




                                }
                            }
                            catch (Exception ex)
                            {


                            }

                        }

                      else
                        {

                            string kk = a.ToString();
                            string val = rows[a].ToString();
                            if (rowheight < nameheight)
                            {
                                rowheight = nameheight;
                                nameheight = 0;

                            }
                            if (rowindex == itemnameindex)
                            {
                                rowindex = itemnameindex;
                                itemnameindex = 0;
                            }
                            try
                            {
                                DateTime dattime;
                                StringFormat format = new StringFormat(StringFormatFlags.DirectionRightToLeft);

                                if (language == "English")
                                {
                                    //  string numbers = ArabicNumerals(rows[a].ToString());
                                    //  e.Graphics.DrawString(numbers, detailsfont, new SolidBrush(Color.Black), (float)psx, (float)psy + (float)rowheight, format);



                                    if (string_type.ToString() == "0")
                                    {
                                        string dd = rows[a].ToString();

                                             string formats = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern+" "+CultureInfo.CurrentCulture.DateTimeFormat.LongTimePattern;

                                    if (DateTime.TryParseExact(dd, formats,
                                System.Globalization.CultureInfo.InvariantCulture,
                                System.Globalization.DateTimeStyles.None, out dattime))
                                    { 
                                            e.Graphics.DrawString(dattime.ToShortDateString(), detailsfont, new SolidBrush(Color.Black), (float)psx - (float)xplus, (float)psy + (float)rowheight, format);

                                        }
                                        else
                                        {

                                            e.Graphics.DrawString(rows[a].ToString(), detailsfont, new SolidBrush(Color.Black), (float)psx - (float)xplus, (float)psy + (float)rowheight, format);
                                        }
                                    }

                                    else if (string_type.ToString() == "1")
                                    {

                                        if (decimal.TryParse(rows[a].ToString(), out outval))
                                        {
                                            // e.Graphics.DrawString(Convert.ToDecimal(rows[a].ToString()).ToString(GlobalFunc.StrDecimal(GlobalVar.QtyDecimals)), detailsfont, new SolidBrush(Color.Black), (float)psx, (float)psy + (float)rowheight, format);
                                        }

                                    }

                                    else if (string_type.ToString() == "2")
                                    {

                                        if (decimal.TryParse(rows[a].ToString(), out outval))
                                        {
                                            //   e.Graphics.DrawString(Convert.ToDecimal(rows[a].ToString()).ToString(GlobalFunc.StrDecimal(GlobalVar.MoneyDecimals)), detailsfont, new SolidBrush(Color.Black), (float)psx, (float)psy + (float)rowheight, format);
                                        }

                                    }

                                    else
                                    {
                                        string formats = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern + " " + CultureInfo.CurrentCulture.DateTimeFormat.LongTimePattern;

                                        if (DateTime.TryParseExact(rows[a].ToString(), formats,
                                    System.Globalization.CultureInfo.InvariantCulture,
                                    System.Globalization.DateTimeStyles.None, out dattime))
                                        {
                                            e.Graphics.DrawString(dattime.ToShortDateString(), detailsfont, new SolidBrush(Color.Black), (float)psx - (float)xplus, (float)psy + (float)rowheight, format);

                                        }
                                        else
                                        {

                                            e.Graphics.DrawString(rows[a].ToString(), detailsfont, new SolidBrush(Color.Black), (float)psx - (float)xplus, (float)psy + (float)rowheight, format);
                                        }
                                    }
                                }
                                else if (language == "Arabic")
                                {
                                    string numbers = ArabicNumerals(rows[a].ToString());
                                    e.Graphics.DrawString(numbers, detailsfont, new SolidBrush(Color.Black), (float)psx, (float)psy - (float)rowheight, format);
                                    // e.Graphics.DrawString(rows[a].ToString(), detailsfont, new SolidBrush(Color.Black), (float)psx, (float)psy + (float)rowheight, format);




                                }
                            }
                            catch (Exception ex)
                            {


                            }
                        }
                        k++;



                        if (k == 0)
                        {
                            psx = left;
                            psy = right;
                        }



                    }
                    columncount++;

                }

                rowheight = rowheight + Convert.ToInt32(Convert.ToInt32(margins.Rows[0]["row_height"])); 
                float arabic_heght=Convert.ToInt32(Convert.ToInt32(margins.Rows[0]["row_height"]));
                #region drawhline
                if (Convert.ToBoolean(margins.Rows[0]["DRAW_HLINE"]) == true)
                {
                    if (Convert.ToBoolean(margins.Rows[0]["is_description"]) == true)

                    {
                        e.Graphics.DrawLine(black, (float)left, (float)rowheight + (float)psy + arabic_heght - 5, (float)left + (float)width, (float)rowheight + (float)psy + arabic_heght - 5);
                    }
                    else
                    {
                        e.Graphics.DrawLine(black, (float)left, (float)rowheight + (float)psy -5, (float)left + (float)width, (float)rowheight + (float)psy-5 );

                    }
                }
              
                #endregion
                rowindex++;
                rowcount++;
                if (morepage == rowindex)

                {
                    //  rowcount = rowindex;


                    rowindex = 0;
                    if (fullpage > 0)
                    {
                        e.HasMorePages = true;

                        //
                        //e.PageSettings.PaperSize.Height = 250;
                    }
                    else
                    {

                        e.HasMorePages = false;
                    }
                    goto nextpage;
                }


                columncount = 0;
                j = 0;

      

            }
            #endregion




            nextpage:
            //fullpage--;
            DateTime dttime = new DateTime();

            if (e.HasMorePages == true)
            {





                foreach (DataRow row in labels.Rows)
                {

                    if (row["visible"].ToString() == "True")
                    {


                        string a = row["Type"].ToString();
                        string search = row["objectname"].ToString();
                        string data = row["tbl"].ToString();
                        string printname = "";

                        if (row["type"].ToString() == "general")
                        {



                            foreach (DataColumn cl in labels.Columns)
                            {
                                try
                                {



                                 
                                    if (data == "company")
                                    {

                                        printname = company.Rows[0][search].ToString();

                                    }
                                    else if (data == "sales"|| data == "Transport"|| data == "Transportation")
                                    {

                                        printname = sales.Rows[0][search].ToString();
                                    }
                                    else if (data == "Customer" || data == "EMP_EMPLOYEES" || data == "Transporter")
                                    {

                                        printname = sales.Rows[0][search].ToString();


                                    }




                                    else if (data == "AmtInWords")
                                    {

                                        //loadcurrency();


                                        if (!sales.Columns.Contains("Grand_Total"))
                                        {
                                            if (sales.Columns.Contains("Total"))
                                            {

                                                sales.Columns["Total"].ColumnName = "Grand_Total";
                                            }


                                        }
                                        double cash_inwords = (double)Convert.ToDouble(sales.Rows[0]["GRAND_TOTAL"]);

                                     //   NumbersToWords.ToWord toWord = new NumbersToWords.ToWord(Convert.ToDecimal(cash_inwords), currencies[selectedval]);


                                     //   printname = toWord.ConvertToEnglish();





                                    }
                                    else if (data == "AmtInWordsArabic")
                                    {

                                       // loadcurrency();


                                        if (!sales.Columns.Contains("Grand_Total"))
                                        {
                                            if (sales.Columns.Contains("Total"))
                                            {

                                                sales.Columns["Total"].ColumnName = "Grand_Total";
                                            }


                                        }
                                        double cash_inwords = (double)Convert.ToDouble(sales.Rows[0]["GRAND_TOTAL"]);

                                        //NumbersToWords.ToWord toWord = new NumbersToWords.ToWord(Convert.ToDecimal(cash_inwords), currencies[selectedval]);



                                      //  printname = toWord.ConvertToArabic();








                                    }

                                    else if (data == "PageNo")
                                    {

                                        printname = "Page   " + pageno + "   Of  " + numberofpage;
                                    }
                                    else
                                    {


                                   
                                    printname = row["text"].ToString();



                                    }



                                    if (row["bold"].ToString() == "True")
                                        fs1 = FontStyle.Bold;
                                    if (row["underline"].ToString() == "True")
                                        fs1 = FontStyle.Underline;
                                    if (row["strikeout"].ToString() == "True")
                                        fs1 = FontStyle.Strikeout;
                                    fn = new Font(row["fontname"].ToString(),
                                     float.Parse(row["fontsize"].ToString()), fs1, System.Drawing.GraphicsUnit.Point);
                                    Brush coloBrush = Brushes.Black;

                                    double x = (double)Convert.ToDouble(row["objectx"]) * actualwidth;
                                    double y = (double)Convert.ToDouble(row["objecty"]) * actualheight;
                                    float ss = (float)x;
                                    string formats = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern + " " + CultureInfo.CurrentCulture.DateTimeFormat.LongTimePattern;

                                    if (DateTime.TryParseExact(printname, formats,
                                System.Globalization.CultureInfo.InvariantCulture,
                                System.Globalization.DateTimeStyles.None, out dttime))
                                    { 
                                   
                                        e.Graphics.DrawString(dttime.ToShortDateString(), fn, new SolidBrush(Color.Black), (float)x, (float)y);

                                    }
                                    else
                                    {


                                        e.Graphics.DrawString(printname, fn, new SolidBrush(Color.Black), (float)x, (float)y);
                                    }
                                    break;

                                }

                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);

                                    continue;
                                }
                            }
                        }


                     

                      

                        if (data == "PageNo")
                        {

                            printname = "Page   " + pageno + "   Of  " + numberofpage;


                            if (row["bold"].ToString() == "True")
                                fs1 = FontStyle.Bold;
                            if (row["underline"].ToString() == "True")
                                fs1 = FontStyle.Underline;
                            if (row["strikeout"].ToString() == "True")
                                fs1 = FontStyle.Strikeout;
                            fn = new Font(row["fontname"].ToString(),
                             float.Parse(row["fontsize"].ToString()), fs1, System.Drawing.GraphicsUnit.Point);
                            Brush coloBrush = Brushes.Black;

                            double x = (double)Convert.ToDouble(row["objectx"]) * actualwidth;
                            double y = (double)Convert.ToDouble(row["objecty"]) * actualheight;
                            float ss = (float)x;
                            e.Graphics.DrawString(printname, fn, new SolidBrush(Color.Black), (float)x, (float)y);

                        }

                    }
                    else
                    {



                    }

                }


                #region drawing lines


                if (Convert.ToBoolean(margins.Rows[0]["draw_line"]) == true)
                {

                    foreach (DataRow rw in lines.Rows)
                    {
                        string type = "";

                        type = rw["type"].ToString();
                        if (type == "general" || ispreprint)
                        {


                            double x1, x2, y1, y2;

                            x1 = (double)Convert.ToDouble(rw["x1"]) * actualwidth;
                            y1 = (double)Convert.ToDouble(rw["y1"]) * actualheight;
                            x2 = (double)Convert.ToDouble(rw["x2"]) * actualwidth;
                            y2 = (double)Convert.ToDouble(rw["y2"]) * actualheight;




                            e.Graphics.DrawLine(black, (float)x1, (float)y1, (float)x2, (float)y2);
                        }


                    }


                    #endregion
                    #region drwingrectangle


                    foreach (DataRow rw in rect.Rows)
                    {
                        string type = "";

                        type = rw["type"].ToString();
                        if (type == "general" || ispreprint)
                        {


                            double x1, x2, y1, y2;

                            x1 = (double)Convert.ToDouble(rw["x1"]) * actualwidth;
                            y1 = (double)Convert.ToDouble(rw["y1"]) * actualheight;
                            width = (double)Convert.ToDouble(rw["width"]) * actualwidth;
                            height = (double)Convert.ToDouble(rw["height"]) * actualheight;



                            e.Graphics.DrawRectangle(black, (float)x1, (float)y1, (float)width, (float)height);
                            // e.Graphics.DrawLine(black, (float)x1, (float)y1, (float)x2, (float)y2);
                        }
                    }
                    #endregion

                }


                rowindex = 0;
                return;


            }

            else
            {

                #region lastpage


                foreach (DataRow row in labels.Rows)
                {

                    if (row["visible"].ToString() == "True")
                    {
                      
                        foreach (DataColumn cl in labels.Columns)
                        {
                            try
                            {
                                string search = row["objectname"].ToString();
                                string data = row["tbl"].ToString();
                            
                                string printname = "";
                                string dd = row["bold"].ToString();
                                if (data == "company")
                                {

                                    printname = company.Rows[0][search].ToString();



                                }
                                else if (data == "sales"|| data=="Transport" || data=="Transportation")
                                {

                                    printname = sales.Rows[0][search].ToString();






                                }
                                else if (data == "Customer" || data == "EMP_EMPLOYEES"|| data == "Transporter")
                                {

                                    printname = sales.Rows[0][search].ToString();


                                }


                                else if (data == "AmtInWords")
                                {
                                    
                                    //loadcurrency();



                                    if (!sales.Columns.Contains("Grand_Total"))
                                    {
                                        if (sales.Columns.Contains("Total"))
                                        {

                                            sales.Columns["Total"].ColumnName = "Grand_Total";
                                        }


                                    }
                                    double cash_inwords = (double)Convert.ToDouble(sales.Rows[0]["GRAND_TOTAL"]);

                                    //NumbersToWords.ToWord toWord = new NumbersToWords.ToWord(Convert.ToDecimal(cash_inwords), currencies[selectedval]);



                                   // printname = toWord.ConvertToEnglish();




                                }
                                else if (data == "AmtInWordsArabic")
                                {
                                  
                                    //loadcurrency();

                                    if (!sales.Columns.Contains("Grand_Total"))
                                    {
                                        if (sales.Columns.Contains("Total"))
                                        {

                                            sales.Columns["Total"].ColumnName = "Grand_Total";
                                        }


                                    }
                                    double cash_inwords = (double)Convert.ToDouble(sales.Rows[0]["GRAND_TOTAL"]);

                                    //NumbersToWords.ToWord toWord = new NumbersToWords.ToWord(Convert.ToDecimal(cash_inwords), currencies[selectedval]);



                                     //   printname = toWord.ConvertToArabic();

                                    






                                }

                                else if (data == "PageNo")
                                {

                                    printname = "Page   " + pageno + "   Of  " + numberofpage;
                                }

                                else
                                {
                                    //cmd = new SqlCommand("select text from invoiceprnline where template="+template_name+"and objectname='" + search.ToString() + "' ");

                                    printname = row["text"].ToString();

                                }

                                cmd = new SqlCommand("select [value_type] from invoiceprnline where template=" + template + " and objectname='" + search.ToString() + "' ");
                                datatype = inf.generalexecutescalar(cmd);
                                if (row["bold"].ToString() == "True")
                                    fs1 = FontStyle.Bold;
                                if (row["underline"].ToString() == "True")
                                    fs1 = FontStyle.Underline;
                                if (row["strikeout"].ToString() == "True")
                                    fs1 = FontStyle.Strikeout;
                                fn = new Font(row["fontname"].ToString(),
                                 float.Parse(row["fontsize"].ToString()), fs1, System.Drawing.GraphicsUnit.Point);
                                Brush coloBrush = Brushes.Black;
                                //  headfont = new Font("Times New Roman", 10);
                                double x = (double)Convert.ToDouble(row["objectx"]) * actualwidth;
                                double y = (double)Convert.ToDouble(row["objecty"]) * actualheight;
                                float ss = (float)x;

                                decimal msg;



                                if (decimal.TryParse(printname, out msg))
                                {

                                    if (row["type"].ToString() == "general")
                                    {


                                        e.Graphics.DrawString(printname, fn, new SolidBrush(Color.Black), (float)x, (float)y);
                                    }

                                    else
                                    {
                                        StringFormat format = new StringFormat(StringFormatFlags.DirectionRightToLeft);

                                        if (decimal.TryParse(printname, out outval))
                                        {

                                            double widthl = (double)Convert.ToDouble(row["objectwidth"]) * actualwidth;

                                            x = x + widthl;


                                            if (datatype == "0")
                                            {

                                                e.Graphics.DrawString(printname, fn, new SolidBrush(Color.Black), (float)x, (float)y, format);
                                            }
                                            else if (datatype == "1")
                                            {

                                                //  e.Graphics.DrawString(Convert.ToDecimal(printname).ToString(GlobalFunc.StrDecimal(GlobalVar.QtyDecimals)), headfont, new SolidBrush(Color.Black), (float)x, (float)y, format);


                                            }
                                            else if (datatype == "2")
                                            {

                                                // e.Graphics.DrawString(Convert.ToDecimal(printname).ToString(GlobalFunc.StrDecimal(GlobalVar.MoneyDecimals)), headfont, new SolidBrush(Color.Black), (float)x, (float)y, format);


                                            }
                                            else
                                            {
                                                e.Graphics.DrawString(printname, fn, new SolidBrush(Color.Black), (float)x, (float)y, format);


                                            }

                                        }
                                    }
                                }
                                else
                                {
                                    string formats = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern + " " + CultureInfo.CurrentCulture.DateTimeFormat.LongTimePattern;

                                    if (DateTime.TryParseExact(printname, formats,
                                System.Globalization.CultureInfo.InvariantCulture,
                                System.Globalization.DateTimeStyles.None, out dttime))
                                    {
                                        e.Graphics.DrawString(dttime.ToShortDateString(), fn, new SolidBrush(Color.Black), (float)x, (float)y);

                                    }
                                    else
                                    {


                                        e.Graphics.DrawString(printname, fn, new SolidBrush(Color.Black), (float)x, (float)y);
                                    }
                                    break;

                                   
                                }
                                break;



                            }



                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);

                                continue;
                            }
                        }

                    

                    }
                    else
                    {



                    }




                }
                if (Convert.ToBoolean(margins.Rows[0]["draw_line"]) == true)
                {
                    #region drawing lines

                    foreach (DataRow rw in lines.Rows)
                    {
                        //string type = "";

                        // type = rw["type"].ToString();



                        double x1, x2, y1, y2;

                        x1 = (double)Convert.ToDouble(rw["x1"]) * actualwidth;
                        y1 = (double)Convert.ToDouble(rw["y1"]) * actualheight;
                        x2 = (double)Convert.ToDouble(rw["x2"]) * actualwidth;
                        y2 = (double)Convert.ToDouble(rw["y2"]) * actualheight;

                        e.Graphics.DrawLine(black, (float)x1, (float)y1, (float)x2, (float)y2 );
                        //  }
                    }


                    #endregion

                    #region drwingrectangle


                    foreach (DataRow rw in rect.Rows)
                    {



                        double x1, x2, y1, y2;

                        x1 = (double)Convert.ToDouble(rw["x1"]) * actualwidth;
                        y1 = (double)Convert.ToDouble(rw["y1"]) * actualheight;
                        width = (double)Convert.ToDouble(rw["width"]) * actualwidth;
                        height = (double)Convert.ToDouble(rw["height"]) * actualheight;



                        e.Graphics.DrawRectangle(black, (float)x1, (float)y1, (float)width, (float)height );

                    }
                    #endregion
                }

                #endregion


            }









            //printing pageno

            string final = "";
        }
        public void PrintPage(object sender, PrintPageEventArgs e)
        {
            k = 0;
            pageno++;
            e.HasMorePages = false;
            // 
            //Convert.ToInt32(Inventory.Properties.Settings.Default.height.ToString());

            DataTable margins = new DataTable();
            margins = new invoiceLineBLL().getgeneral(template);
            if (margins.Rows.Count > 0)
            {

                language = margins.Rows[0]["language"].ToString();
            }
            else
            {
                MessageBox.Show("There Is no Design With This Template");
                return;
            }



            //int y = 0;
            int PrintId = 0;
            int ItemHieght = 0;



            Pen black = new Pen(Color.Black, 1);

            System.Drawing.Font fn = null;
            FontStyle fs1 = new FontStyle();

            //609=width,556=height;
            int panl_width = Convert.ToInt32(settings.Rows[0]["form_width"]);// this.main_pnl.ClientSize.Width;


            int pnl_height = Convert.ToInt32(settings.Rows[0]["form_height"]); //;556; //this.main_pnl.ClientSize.Height;

            
                
            double actualwidth = (double)Convert.ToDouble(margins.Rows[0]["paper_width"]) / panl_width;
            double actualheight = (double)Convert.ToDouble(margins.Rows[0]["paper_height"]) / pnl_height;
           if (!printerheight)
                actualheight = (double)Convert.ToDouble(e.PageSettings.PaperSize.Height) / pnl_height;
                //double actualwidth = (double)840 / panl_width;
                // double actualheight = (double)1100 / pnl_height; ;
            // e.Graphics.DrawString("Page" + "  " + pageno + " " + "Of" + "    " + numberofpage, detailsfont, new SolidBrush(Color.Black), e.PageSettings.PaperSize.Width / 2, e.PageSettings.PaperSize.Height - 25);

            double left = 0;
            double right = 0;
            double width = 0;
            double height = 0;
            double rowheaderheight = 0;
            double rowheadery = 0;
            double bottom = 0;

            int nameheight = 0;
            int name_morepage = 0;




            //details
            int inc = 0;

            //drawrectangle


            left = (double)Convert.ToDouble(margins.Rows[0]["location_x"]) * actualwidth;
            right = (double)Convert.ToDouble(margins.Rows[0]["location_y"]);// * actualheight;
            width = (double)Convert.ToDouble(margins.Rows[0]["width"]) * actualwidth;
            height = (double)Convert.ToDouble(margins.Rows[0]["height"]);// * actualheight;

            rowheaderheight = (double)Convert.ToDouble(margins.Rows[0]["rowheader_height"]);
            rowheadery = (double)Convert.ToDouble(margins.Rows[0]["right"]) * actualwidth;
            bottom = (double)Convert.ToDouble(margins.Rows[0]["bottom"]);// * actualheight;
         
                if (fullpage > 0)
            {

                if (ispreprint)
                {

                    e.Graphics.DrawRectangle(black, (float)left, (float)right, (float)width, (float)height);
                }
                else
                {

                    e.Graphics.DrawRectangle(black, (float)left, (float)right, (float)width, (float) add_gridheight);
                    morepage = maxline;
                }




            }
            else
            {

                e.Graphics.DrawRectangle(black, (float)left, (float)right, (float)width, (float) add_gridheight);
                morepage = minline;


            }

            fs1 = new FontStyle();
            fs1 = FontStyle.Bold;





            e.Graphics.DrawLine(black, (float)left, (float)right + (float)rowheaderheight, (float)rowheadery, (float)right + (float)rowheaderheight);
            if (fullpage > 0)
            {

            }
            else
            {

               float y= ((float)add_gridheight + (float)add_gridheight / 4 - (float)rowheaderheight);

                e.Graphics.DrawLine(black,(float)left, y, (float)rowheadery, (float)y);

            }

            //  e.Graphics.DrawLine(black, 10, 25, 800, 25);


            #region headings

            int INCR = 0;
            double psx = 0, pnames_x = 0, psy = 0, pex = 0, pey = 0;

            Object PRE_X = null, PRE_Y = null;

            foreach (DataRow rows in gridview.Rows)
            {


                if (rows["visible"].ToString() == "True")
                {


                    try
                    {

                        string search = rows["TEXT"].ToString();




                        double sx = (double)Convert.ToDouble(rows["startx"])*actualwidth;
                        double sy = (double)Convert.ToDouble(rows["starty"]); //* actualheight;
                        double ex = (double)Convert.ToDouble(rows["endx"]) * actualwidth;
                        double ey = (double)Convert.ToDouble(rows["endy"]) * actualheight;
                        if (fullpage > 0)
                        {
                            if (!ispreprint)
                            {

                                //   sy = sy + addline;

                              //  ey = ey + addline;


                            }


                        }





                       // fn = new Font("arial",
                             // (float)6, fs1, System.Drawing.GraphicsUnit.Point);






                        if (k != 0)
                        {


                            e.Graphics.DrawLine(black, (float)sx, (float)sy, (float)sx, (float)add_gridheight);
                        }

                        //   ogsales.Columns.Add(search, typeof(string));
                        StringFormat format = new StringFormat(StringFormatFlags.MeasureTrailingSpaces);


                        //  e.Graphics.DrawString(search, headfont, new SolidBrush(Color.Black), (float)sx, (float)sy,format);

                      if (k != 0)
                        {
                          e.Graphics.DrawString(search, headfont, new SolidBrush(Color.Black), (float)sx +1, (float)sy + 5);


                       }
                       else
                       {
                            e.Graphics.DrawString(search, headfont, new SolidBrush(Color.Black), (float)sx + 10, (float)sy + 5);
                      }


                        k++;




                    }
                    catch (Exception EX)
                    {

                        continue;

                    }


                }


            }
            #endregion

            #region details

            k = 0;

            int itemnameindex = 0;
            decimal outval;
            detailsfont = new Font("Times New Roman", 6);
            int j = 0; int i = 0;
            double rowheight = rowheaderheight + 5;
            foreach (DataRow rows in sales_dtl.Rows)
            {



                i++;

                if (rowcount >= i)
                {
                    continue;

                }

                // columncount = 0;

               nameheight = Convert.ToInt32(rowheaderheight + 5);






                foreach (DataColumn cl in sales_dtl.Columns)
                {


                    // ;
                    j++;
                    if (columncount > j)
                        continue;

                    string a = cl.ColumnName.ToString();

                    cmd = new SqlCommand("select [type] from a4columns where template='" + template + "' and name='" + a + "' ");
                    object string_type = new Info().generalexecutescalar(cmd);

                    bool vis = false;
                    try
                    {

                        cmd = new SqlCommand("select visible from a4columns where template='" + template + "' and name='" + a + "' ");
                        object visb = new Info().generalexecutescalar(cmd);
                        if (visb.ToString() != "")

                        { 
                        vis = Convert.ToBoolean(visb);
                         }
                        else
                        {
                            vis = false;

                        }

                        if (vis == false)
                        {
                            columncount++;
                            continue;
                        }

                        #region cc
                        //   DataRow row = gridview.Rows[0];
                        //  string rowValue = row["ColumnName"].ToString();
                        //   row = null;
                        //  row = gridview.Select("name='" + ss + "'");



                        //  DataRow[] newrow=null;

                        //       newrow = gridview.Select("name='"+ ss+"'");

                        // string vib = newrow[0].vaToString();
                        #endregion
                    }
                    catch (Exception exx)
                    {
                        columncount++;
                        continue;
                    }

                   // fn = new Font("arial",
                         // (float)6, fs1, System.Drawing.GraphicsUnit.Point);
                    if (vis == true)
                    {

                        cmd = new SqlCommand("select endx from a4columns where template='" + template + "' and name='" + a + "' ");
                        object startx = new Info().generalexecutescalar(cmd);
                        cmd = new SqlCommand("select startx from a4columns where template='" + template + "' and name='" + a + "' ");
                        object pstartx = new Info().generalexecutescalar(cmd);
                        cmd = new SqlCommand("select starty from a4columns where template='" + template + "' and  name='" + a + "' ");

                        object starty = new Info().generalexecutescalar(cmd);


                        double sx = (double)Convert.ToDouble(startx)* actualwidth;
                        double sy = (double)Convert.ToDouble(starty);// * actualheight;

                        double sxp = (double)Convert.ToDouble(pstartx) * actualwidth;




                        // if (k == 0)
                        // {
                        //  pnames_x = left;
                        //   psx = left;
                        // psy = right;
                        // }
                        //  else
                        // {
                        pnames_x = sxp;
                        psx = sx;
                        psy = sy;



                        if (!ispreprint)
                        {


                            //    psy=psy+



                        }
                        // }
                        //   fn = new Font("arial",
                        // (float)10, fs1, System.Drawing.GraphicsUnit.Point);

                        if (a == "ITEM_NAME")
                        {
                           
                         
                            string word = "";
                            double inamerowheight =Convert.ToDouble(settings.Rows[0]["row_height"]);
                            string hed = "";
                            StringBuilder result = new StringBuilder();
                            StringBuilder line = new StringBuilder();

                            if (word == "" && tail == "")
                            {
                                //draw item name
                                string input = rows[a].ToString();
                                stack.Push(input);
                                isok = true;
                            }
                            
                            itemnameindex = rowindex;
                            nameheight = Convert.ToInt32(rowheight);

                            StringFormat format = new StringFormat(StringFormatFlags.DirectionRightToLeft);
                            e.Graphics.DrawString(  rows["DESCRIPTION"].ToString(), detailsfont, new SolidBrush(Color.Black), (float)pnames_x, (float)psy + (float)nameheight);
                            nameheight = nameheight + Convert.ToInt32(rowheightadd);
                            //name_morepage = morepage;
                            // rowheaderheight = 0;
                            while (stack.Count > 0)
                            {
                                word = stack.Pop();
                                if (word != "")
                                {
                                    if (word.Length > rowLength)
                                    {
                                        hed = word.Substring(0, rowLength);
                                        tail = word.Substring(rowLength);

                                        word = hed;
                                        stack.Push(tail);
                                    }

                                    else
                                    {
                                        hed = word;
                                        word = "";
                                        tail = "";
                                        columncount = 0;
                                        j = 0;
                                    }
                                    result.AppendLine(line.ToString());

                                    if (language == "English")
                                    {
                                        format = new StringFormat(StringFormatFlags.DirectionRightToLeft);
                                        //      e.Graphics.DrawString(hed, detailsfont, new SolidBrush(Color.Black), (float)psx, (float)psy + (float)nameheight, format);
                                        e.Graphics.DrawString(hed, detailsfont, new SolidBrush(Color.Black), (float)pnames_x+5, (float)psy + (float)nameheight);
                                    }
                                    else if (language == "Arabic")
                                    {


                                        format = new StringFormat(StringFormatFlags.DirectionRightToLeft);
                                        e.Graphics.DrawString(hed, detailsfont, new SolidBrush(Color.Black), (float)psx+5, (float)psy + (float)nameheight);
                                        // e.Graphics.DrawString(hed, detailsfont, new SolidBrush(Color.Black), (float)pnames_x, (float)psy + (float)nameheight);



                                    }
                                    nameheight = nameheight +Convert.ToInt32(rowheightadd);
                                    //  name_morepage--;
                                    //rowcount++;

                                    int index = sales_dtl.Rows.IndexOf(rows);
                                    itemnameindex++;

                                    if (itemnameindex == morepage)
                                    {
                                        if (tail == "")
                                        {
                                            columncount = 0;

                                            rowcount++;
                                        }
                                        if (fullpage > 0)
                                        {

                                            fullpage--;
                                            e.HasMorePages = true;

                                            //  e.PageSettings.PaperSize.Height = 600;
                                        }
                                        else
                                        {

                                            e.HasMorePages = false;
                                        }
                                        if (rowindex < itemnameindex)
                                        {
                                            rowindex = itemnameindex;
                                            itemnameindex = 0;
                                        }

                                        goto nextpage;

                                    }





                                }
                            }
                            if (nameheight > rowheight)
                            {
                                rowheight = nameheight-20;
                           }






                            isdesc = true;












                        }
                        

                    ////  if(isdesc)
                    //    {

                           
                    //        //string word = "";
                    //        double inamerowheight = Convert.ToDouble(settings.Rows[0]["row_height"]);
                    //        //string hed = "";
                    //        //StringBuilder result = new StringBuilder();
                    //        //StringBuilder line = new StringBuilder();

                    //        //if (word == "" && tail == "")
                    //        //{
                    //        //    //draw item name
                    //        //    string input = rows["DESCRIPTION"].ToString();
                    //        //    stack.Push(input);
                    //        //    isok = true;
                    //        //}

                    //        e.Graphics.DrawString(rows["DESCRIPTION"].ToString(), detailsfont, new SolidBrush(Color.Black), (float)pnames_x, (float)psy + (float)nameheight);

                    //        ////name_morepage = morepage;
                    //        //// rowheaderheight = 0;
                    //        //while (stack.Count > 0)
                    //        //{
                    //        //    word = stack.Pop();
                    //        //    if (word != "")
                    //        //    {
                    //        //        if (word.Length > rowLength)
                    //        //        {
                    //        //            hed = word.Substring(0, rowLength);
                    //        //            tail = word.Substring(rowLength);

                    //        //            word = hed;
                    //        //            stack.Push(tail);
                    //        //        }

                    //        //        else
                    //        //        {
                    //        //            hed = word;
                    //        //            word = "";
                    //        //            tail = "";
                    //        //            columncount = 0;
                    //        //            j = 0;
                    //        //        }
                    //        //        result.AppendLine(line.ToString());

                    //        //        if (language == "English")
                    //        //        {
                    //        //            StringFormat format = new StringFormat(StringFormatFlags.DirectionRightToLeft);
                    //        //            //      e.Graphics.DrawString(hed, detailsfont, new SolidBrush(Color.Black), (float)psx, (float)psy + (float)nameheight, format);
                    //        //            e.Graphics.DrawString(hed, detailsfont, new SolidBrush(Color.Black), (float)pnames_x, (float)psy + (float)nameheight);
                    //        //        }
                    //        //        else if (language == "Arabic")
                    //        //        {


                    //        //            StringFormat format = new StringFormat(StringFormatFlags.DirectionRightToLeft);
                    //        //            e.Graphics.DrawString(hed, detailsfont, new SolidBrush(Color.Black), (float)psx, (float)psy + (float)nameheight);
                    //        //            // e.Graphics.DrawString(hed, detailsfont, new SolidBrush(Color.Black), (float)pnames_x, (float)psy + (float)nameheight);



                    //        //        }
                    //             nameheight = nameheight + Convert.ToInt32(rowheightadd);
                    //        //        //  name_morepage--;
                    //        //        //rowcount++;

                    //        //        int index = sales_dtl.Rows.IndexOf(rows);
                    //        //        itemnameindex++;

                    //        //        if (itemnameindex == morepage)
                    //        //        {
                    //        //            if (tail == "")
                    //        //            {
                    //        //                columncount = 0;

                    //        //                rowcount++;
                    //        //            }
                    //        //            if (fullpage > 0)
                    //        //            {

                    //        //                fullpage--;
                    //        //                e.HasMorePages = true;

                    //        //                //  e.PageSettings.PaperSize.Height = 600;
                    //        //            }
                    //        //            else
                    //        //            {

                    //        //                e.HasMorePages = false;
                    //        //            }
                    //        //            if (rowindex < itemnameindex)
                    //        //            {
                    //        //                rowindex = itemnameindex;
                    //        //                itemnameindex = 0;
                    //        //            }

                    //        //            goto nextpage;

                    //        //        }





                    //        //    }
                    //        //}
                    //        //if (nameheight > rowheight)
                    //        //{
                    //        //    rowheight = nameheight - 20;
                    //        //}







                    //        isdesc = false;









                    //    }

                        else
                        {

                            string kk = a.ToString();
                            string val = rows[a].ToString();
                            if (rowheight < nameheight)
                            {
                                rowheight = nameheight;
                                nameheight = 0;

                            }
                            if (rowindex == itemnameindex)
                            {
                                rowindex = itemnameindex;
                                itemnameindex = 0;
                            }
                            try
                            {

                                StringFormat format = new StringFormat(StringFormatFlags.DirectionRightToLeft);

                                if (language == "English")
                                {
                                    //  string numbers = ArabicNumerals(rows[a].ToString());
                                    //  e.Graphics.DrawString(numbers, detailsfont, new SolidBrush(Color.Black), (float)psx, (float)psy + (float)rowheight, format);



                                    if (string_type.ToString() == "0")
                                    {
                                        e.Graphics.DrawString(rows[a].ToString(), detailsfont, new SolidBrush(Color.Black), (float)psx, (float)psy + (float)rowheight, format);
                                    }

                                    else if (string_type.ToString() == "1")
                                    {

                                        if (decimal.TryParse(rows[a].ToString(), out outval))
                                        {
                                            // e.Graphics.DrawString(Convert.ToDecimal(rows[a].ToString()).ToString(GlobalFunc.StrDecimal(GlobalVar.QtyDecimals)), detailsfont, new SolidBrush(Color.Black), (float)psx, (float)psy + (float)rowheight, format);
                                        }

                                    }

                                    else if (string_type.ToString() == "2")
                                    {

                                        if (decimal.TryParse(rows[a].ToString(), out outval))
                                        {
                                            //   e.Graphics.DrawString(Convert.ToDecimal(rows[a].ToString()).ToString(GlobalFunc.StrDecimal(GlobalVar.MoneyDecimals)), detailsfont, new SolidBrush(Color.Black), (float)psx, (float)psy + (float)rowheight, format);
                                        }

                                    }

                                    else
                                    {

                                        e.Graphics.DrawString(rows[a].ToString(), detailsfont, new SolidBrush(Color.Black), (float)psx, (float)psy + (float)rowheight, format);

                                    }
                                }
                                else if (language == "Arabic")
                                {
                                    string numbers = ArabicNumerals(rows[a].ToString());
                                    e.Graphics.DrawString(numbers, detailsfont, new SolidBrush(Color.Black), (float)psx, (float)psy + (float)rowheight, format);
                                    // e.Graphics.DrawString(rows[a].ToString(), detailsfont, new SolidBrush(Color.Black), (float)psx, (float)psy + (float)rowheight, format);




                                }
                            }
                            catch (Exception ex)
                            {


                            }
                        }
                        k++;



                        if (k == 0)
                        {
                            psx = left;
                            psy = right;
                        }



                    }
                    columncount++;

                }

                rowheight = rowheight + rowheightadd;
                rowindex++;
                rowcount++;
                if (morepage == rowindex)

                {
                    //  rowcount = rowindex;


                    rowindex = 0;
                    if (fullpage > 0)
                    {
                        e.HasMorePages = true;

                        //
                        //e.PageSettings.PaperSize.Height = 250;
                    }
                    else
                    {

                        e.HasMorePages = false;
                    }
                    goto nextpage;
                }


                columncount = 0;
                j = 0;

            }
            #endregion





            nextpage:
            //fullpage--;


            if (e.HasMorePages == true)
            {





                foreach (DataRow row in labels.Rows)
                {

                    if (row["visible"].ToString() == "True")
                    {


                        string a = row["Type"].ToString();
                        if (row["type"].ToString() == "general")
                        {



                            foreach (DataColumn cl in labels.Columns)
                            {
                                try
                                {



                                    string search = row["objectname"].ToString();
                                    string data = row["tbl"].ToString();
                                    string printname = "";

                                    if (data == "company")
                                    {

                                        printname = company.Rows[0][search].ToString();

                                    }
                                    else if (data == "sales")
                                    {

                                        printname = sales.Rows[0][search].ToString();
                                    }
                                    else
                                    {


                                        cmd = new SqlCommand("select text from invoiceprnline where objectname='" + search.ToString() + "' ");
                                        printname = new Info().generalexecutescalar(cmd);



                                    }



                                    if (row["bold"].ToString() == "True")
                                        fs1 = FontStyle.Bold;
                                    if (row["underline"].ToString() == "True")
                                        fs1 = FontStyle.Underline;
                                    if (row["strikeout"].ToString() == "True")
                                        fs1 = FontStyle.Strikeout;
                                    fn = new Font(row["fontname"].ToString(),
                                     float.Parse(row["fontsize"].ToString()), fs1, System.Drawing.GraphicsUnit.Point);
                                    Brush coloBrush = Brushes.Black;

                                    double x = (double)Convert.ToDouble(row["objectx"]) * actualwidth;
                                    double y = (double)Convert.ToDouble(row["objecty"]) * actualheight;
                                    float ss = (float)x;
                                    e.Graphics.DrawString(printname, fn, new SolidBrush(Color.Black), (float)x, (float)y);

                                    break;

                                }

                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);

                                    continue;
                                }
                            }
                        }



                    }
                    else
                    {



                    }

                }


                #region drawing lines




                foreach (DataRow rw in lines.Rows)
                {
                    string type = "";

                    type = rw["type"].ToString();
                    if (type == "general" || ispreprint)
                    {


                        double x1, x2, y1, y2;

                        x1 = (double)Convert.ToDouble(rw["x1"]) * actualwidth;
                        y1 = (double)Convert.ToDouble(rw["y1"]) * actualheight;
                        x2 = (double)Convert.ToDouble(rw["x2"]) * actualwidth;
                        y2 = (double)Convert.ToDouble(rw["y2"]) * actualheight;




                        e.Graphics.DrawLine(black, (float)x1, (float)y1, (float)x2, (float)y2);
                    }


                }


                #endregion
                #region drwingrectangle


                foreach (DataRow rw in rect.Rows)
                {
                    string type = "";

                    type = rw["type"].ToString();
                    if (type == "general" || ispreprint)
                    {


                        double x1, x2, y1, y2;

                        x1 = (double)Convert.ToDouble(rw["x1"]) * actualwidth;
                        y1 = (double)Convert.ToDouble(rw["y1"]) * actualheight;
                        width = (double)Convert.ToDouble(rw["width"]) * actualwidth;
                        height = (double)Convert.ToDouble(rw["height"]) * actualheight;



                        e.Graphics.DrawRectangle(black, (float)x1, (float)y1, (float)width, (float)height);
                        // e.Graphics.DrawLine(black, (float)x1, (float)y1, (float)x2, (float)y2);
                    }
                }
                #endregion




                rowindex = 0;
                return;


            }

            else
            {

                #region lastpage
                if (!printerheight)
                    rowheight = 0;

                foreach (DataRow row in labels.Rows)
                {

                    if (row["visible"].ToString() == "True")
                    {

                      

                        foreach (DataColumn cl in labels.Columns)
                        {
                            try
                            {
                                string search = row["objectname"].ToString();
                                string data = row["tbl"].ToString();
                                string printname = "";

                                if (data == "company")
                                {

                                    printname = company.Rows[0][search].ToString();



                                }
                                else if (data == "sales")
                                {

                                    printname = sales.Rows[0][search].ToString();






                                }

                                else
                                {
                                    cmd = new SqlCommand("select text from invoiceprnline where objectname='" + search.ToString() + "' ");

                                    printname = new Info().generalexecutescalar(cmd);

                                }

                                cmd = new SqlCommand("select [value_type] from invoiceprnline where objectname='" + search.ToString() + "' ");
                                datatype = new Info().generalexecutescalar(cmd);
                                if (row["bold"].ToString() == "True")
                                    fs1 = FontStyle.Bold;
                                if (row["underline"].ToString() == "True")
                                    fs1 = FontStyle.Underline;
                                if (row["strikeout"].ToString() == "True")
                                    fs1 = FontStyle.Strikeout;
                                fn = new Font(row["fontname"].ToString(),
                                 float.Parse(row["fontsize"].ToString()), fs1, System.Drawing.GraphicsUnit.Point);
                                Brush coloBrush = Brushes.Black;
                                //  headfont = new Font("Times New Roman", 10);
                                double x = (double)Convert.ToDouble(row["objectx"]) * actualwidth;
                                double y = (double)Convert.ToDouble(row["objecty"]) * actualheight;
                                float ss = (float)x;

                                decimal msg;



                                if (decimal.TryParse(printname, out msg))
                                {

                                    if (row["type"].ToString() == "general")
                                    {


                                        e.Graphics.DrawString(printname, headfont, new SolidBrush(Color.Black), (float)x, (float)y);
                                    }

                                    else
                                    {
                                        StringFormat format = new StringFormat(StringFormatFlags.DirectionRightToLeft);

                                        if (decimal.TryParse(printname, out outval))
                                        {

                                            double widthl = (double)Convert.ToDouble(row["objectwidth"]) * actualwidth;

                                            x = x + widthl;


                                            if (datatype == "0")
                                            {

                                                e.Graphics.DrawString(printname, headfont, new SolidBrush(Color.Black), (float)x, (float)y+(float)rowheight, format);
                                            }
                                            else if (datatype == "1")
                                            {

                                                  // e.Graphics.DrawString(Convert.ToDecimal(printname).ToString(GlobalFunc.StrDecimal(GlobalVar.QtyDecimals)), headfont, new SolidBrush(Color.Black), (float)x, (float)y, format);


                                            }
                                            else if (datatype == "2")
                                            {

                                              //  e.Graphics.DrawString(Convert.ToDecimal(printname).ToString(GlobalFunc.StrDecimal(GlobalVar.MoneyDecimals)), headfont, new SolidBrush(Color.Black), (float)x, (float)y, format);


                                            }
                                            else
                                            {
                                                e.Graphics.DrawString(printname, headfont, new SolidBrush(Color.Black), (float)x, (float)y+(float)rowheight, format);


                                            }

                                        }
                                    }
                                }
                                else
                                {

                                    e.Graphics.DrawString(printname, headfont, new SolidBrush(Color.Black), (float)x, (float)y);
                                }
                                break;



                            }



                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);

                                continue;
                            }
                        }
                    
                   



                    }
                    else
                    {



                    }




                }

                #region drawing lines

                foreach (DataRow rw in lines.Rows)
                {
                    //string type = "";

                    // type = rw["type"].ToString();



                    double x1, x2, y1, y2;

                    x1 = (double)Convert.ToDouble(rw["x1"]) * actualwidth;
                    y1 = (double)Convert.ToDouble(rw["y1"]) * actualheight;
                    x2 = (double)Convert.ToDouble(rw["x2"]) * actualwidth;
                    y2 = (double)Convert.ToDouble(rw["y2"]) * actualheight;

                    e.Graphics.DrawLine(black, (float)x1, (float)y1, (float)x2, (float)y2);
                    //  }
                }


                #endregion

                #region drwingrectangle


                foreach (DataRow rw in rect.Rows)
                {



                    double x1, x2, y1, y2;

                    x1 = (double)Convert.ToDouble(rw["x1"]) * actualwidth;
                    y1 = (double)Convert.ToDouble(rw["y1"]) * actualheight;
                    width = (double)Convert.ToDouble(rw["width"]) * actualwidth;
                    height = (double)Convert.ToDouble(rw["height"]) * actualheight;



                    e.Graphics.DrawRectangle(black, (float)x1, (float)y1, (float)width, (float)height);

                }
                #endregion

                #endregion


            }









            //printing pageno

            string final = "";



        
        }
        public int printeditems = 0;
        
        public bool IsFirstPage = true;
        public bool IsLastPage = false;
        public void printPageDynamic(object sender, PrintPageEventArgs e)
        {
            k = 0;
            pageno++;
            e.HasMorePages = false;
           
            DataTable margins = new DataTable();
            margins = new invoiceLineBLL().getgeneral(template);
            if (margins.Rows.Count > 0)
            {

                language = margins.Rows[0]["language"].ToString();
            }
            else
            {
                MessageBox.Show("There Is no Design With This Template");
                return;
            }
            GetValues(template);
            int PrintId = 0;
            int ItemHieght = 0;
            Pen black = new Pen(Color.Black, 1);
            System.Drawing.Font fn = null;
            FontStyle fs1 = new FontStyle();
            int panl_width = Convert.ToInt32(settings.Rows[0]["form_width"]);// this.main_pnl.ClientSize.Width;
            int pnl_height = Convert.ToInt32(settings.Rows[0]["form_height"]); //;556; //this.main_pnl.ClientSize.Height;
            
           
            double actualwidth = (double)Convert.ToDouble(margins.Rows[0]["paper_width"]) / panl_width;
            double actualheight = (double)Convert.ToDouble(margins.Rows[0]["paper_height"]) / pnl_height;
            if (!printerheight)
                actualheight = (double)Convert.ToDouble(e.PageSettings.PaperSize.Height) / pnl_height;
            
            double left = 0;
            double right = 0;
            double width = 0;
            double height = 0;
            double rowheaderheight = 0;
            double rowheadery = 0;
            double bottom = 0;
            int nameheight = 0;
            int name_morepage = 0;
            //details
            int inc = 0;

            //drawrectangle
            left = (double)Convert.ToDouble(margins.Rows[0]["location_x"]) * actualwidth;
            right = (double)Convert.ToDouble(margins.Rows[0]["location_y"]) * actualheight;
            width = (double)Convert.ToDouble(margins.Rows[0]["width"]) * actualwidth;
            height = (double)Convert.ToDouble(margins.Rows[0]["height"]) * actualheight;
            rowheaderheight = (double)Convert.ToDouble(margins.Rows[0]["rowheader_height"]);
            bool IsRepeatHdr = Convert.ToBoolean(margins.Rows[0]["HEADER_RPT"]);
            bool IsRepeatFtr = Convert.ToBoolean(margins.Rows[0]["FOOTER_RPT"]);
            //grid line
            e.Graphics.DrawRectangle(black, (int)left, (int)right, (int)width, (int)height);
            e.Graphics.DrawLine(black, (float)left, (float)right + (float)rowheaderheight, ((float)width + (float)left), (float)right + (float)rowheaderheight);

            
            rowheadery = (double)Convert.ToDouble(margins.Rows[0]["right"]) * actualwidth;
            bottom = Convert.ToDouble(margins.Rows[0]["bottom"])* actualheight;
            add_gridheight =Convert.ToInt32((Convert.ToDouble(margins.Rows[0]["bottom"])*actualheight));


            # region DrawLine
           
            foreach (DataRow rw in lines.Rows)
            {
               
                double x1, x2, y1, y2;

                x1 = (double)Convert.ToDouble(rw["x1"]) * actualwidth;
                y1 = (double)Convert.ToDouble(rw["y1"]) * actualheight;
                x2 = (double)Convert.ToDouble(rw["x2"]) * actualwidth;
                y2 = (double)Convert.ToDouble(rw["y2"]) * actualheight;

                e.Graphics.DrawLine(black, (float)x1, (float)y1, (float)x2, (float)y2);
              
            }

            #endregion
            #region drwingrectangle


            foreach (DataRow rw in rect.Rows)
            {
                  double x1, y1;
                  x1 = (double)Convert.ToDouble(rw["x1"]) * actualwidth;
                  y1 = (double)Convert.ToDouble(rw["y1"]) * actualheight;
                  width = (double)Convert.ToDouble(rw["width"]) * actualwidth;
                  height = (double)Convert.ToDouble(rw["height"]) * actualheight;
                  e.Graphics.DrawRectangle(black, (float)x1, (float)y1, (float)width, (float)height);
            }
            #endregion
            #region headings
           
            foreach (DataRow rows in gridview.Rows)
            {
               if (rows["visible"].ToString() == "True")
                {

                    try
                    {

                        string search = rows["TEXT"].ToString();
                        double sx = (double)Convert.ToDouble(rows["startx"]) * actualwidth;
                        double sy = (double)Convert.ToDouble(rows["starty"])* actualheight;
                        double ex = (double)Convert.ToDouble(rows["endx"]) * actualwidth;
                        double ey = (double)Convert.ToDouble(rows["endy"]) * actualheight;
                        if (fullpage > 0)
                        {
                            if (!ispreprint)
                            {

                                //   sy = sy + addline;

                                //  ey = ey + addline;


                            }


                        }

                        if (k != 0)
                        {
                            StringFormat format = new StringFormat(StringFormatFlags.MeasureTrailingSpaces);
                            e.Graphics.DrawString(search, headfont, new SolidBrush(Color.Black), (float)sx+2, (float)sy+2);
                            e.Graphics.DrawLine(black, (float)sx, (float)sy, (float)sx, (float)add_gridheight);
                        }
                         
                        else
                        {
                            e.Graphics.DrawString(search, headfont, new SolidBrush(Color.Black), (float)left+2, (float)sy+2);
                        }


                        k++;




                    }
                    catch (Exception EX)
                    {

                        continue;

                    }


                }


            }
            #endregion



            if (!IsRepeatHdr)
            {
                if (IsFirstPage)
                {
                    #region Labels
                    foreach (DataRow row in labels.Rows)
                    {
                        try
                        {
                            string search = row["objectname"].ToString();
                            string data = row["tbl"].ToString();
                            string printname = "";


                            if (data == "New")
                            {
                                printname = row["text"].ToString();
                            }
                            else if (data == "Logo")
                            {
                                try
                                {
                                    string logo = getPath();
                                    if (logo != null || logo != "")
                                    {
                                        double x1 = Convert.ToDouble(row["objectx"]) * actualwidth;
                                        double y1 = Convert.ToDouble(row["objecty"]) * actualheight;
                                        double w = Convert.ToDouble(row["objectwidth"]) * actualwidth;
                                        double h = Convert.ToDouble(row["objecthieght"]) * actualheight;
                                        System.Drawing.Image img = System.Drawing.Image.FromFile(logo);
                                        e.Graphics.DrawImage(img, new Rectangle((int)x1, (int)y1, (int)w, (int)h));
                                        continue;
                                    }
                                }
                                catch (Exception ex)
                                {
                                }

                            }
                            else if (data == "REC_CUSTOMER")
                            {
                                DataRow[] r = Details.Select("TEXT='" + search + "'");
                                DataRow row1 = r.FirstOrDefault();
                                if (CustomerDetails.Rows.Count > 0)
                                {
                                    printname = CustomerDetails.Rows[0][row1["COLUMN_NAME"].ToString()].ToString();
                                }
                                else
                                {
                                    printname = search;
                                }
                            }
                            else if (data == "INV_SALES_HDR")
                            {
                                if (SalesHDR.Rows.Count > 0)
                                {
                                    printname = SalesHDR.Rows[0][search].ToString();
                                }
                                else
                                {
                                    printname = search;
                                }
                            }
                            else if (data == "")
                            {
                                if (data == "AmtInWords")
                                {
                                    if (SalesHDR.Rows.Count > 0)
                                    {
                                        decimal amt = Convert.ToDecimal(SalesHDR.Rows[0]["NET_AMOUNT"]);
                                        printname = AmtToString(amt);
                                    }
                                    else
                                    {
                                        printname = "[AmtInWords]";
                                    }

                                }
                            }
                            else
                            {
                                DataRow[] r = Details.Select("TEXT='" + search + "'");
                                DataRow row1 = r.FirstOrDefault();
                                printname = GetDbValues(row1["TABLE"].ToString(), row1["COLUMN_NAME"].ToString());
                            }
                            cmd = new SqlCommand("select [value_type] from invoiceprnline where objectname='" + search.ToString() + "' and template='" + template + "'");
                            datatype = new Info().generalexecutescalar(cmd);
                            if (row["bold"].ToString() == "True")
                                fs1 = FontStyle.Bold;
                            if (row["underline"].ToString() == "True")
                                fs1 = FontStyle.Underline;
                            if (row["strikeout"].ToString() == "True")
                                fs1 = FontStyle.Strikeout;
                            fn = new Font(row["fontname"].ToString(),
                             float.Parse(row["fontsize"].ToString()), fs1, System.Drawing.GraphicsUnit.Point);
                            Brush coloBrush = Brushes.Black;
                            //  headfont = new Font("Times New Roman", 10);
                            double x = (double)Convert.ToDouble(row["objectx"]) * actualwidth;
                            double y = (double)Convert.ToDouble(row["objecty"]) * actualheight;
                            float ss = (float)x;

                            decimal msg;



                            if (decimal.TryParse(printname, out msg))
                            {

                                if (row["type"].ToString() == "general")
                                {
                                    e.Graphics.DrawString(printname, fn, new SolidBrush(Color.Black), (float)x, (float)y);
                                }

                                else
                                {
                                    StringFormat format = new StringFormat(StringFormatFlags.DirectionRightToLeft);
                                    double widthl = (double)Convert.ToDouble(row["objectwidth"]) * actualwidth;
                                    x = x + widthl;
                                    e.Graphics.DrawString(printname, fn, new SolidBrush(Color.Black), (float)x, (float)y, format);

                                }
                            }
                            else
                            {
                                e.Graphics.DrawString(printname, fn, new SolidBrush(Color.Black), (float)x, (float)y);
                            }



                        }

                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);

                            continue;
                        }

                    }
                    #endregion
                }
            }
            else
            {
                #region Labels
                foreach (DataRow row in labels.Rows)
                {
                    try
                    {
                        string search = row["objectname"].ToString();
                        string data = row["tbl"].ToString();
                        string printname = "";


                        if (data == "New")
                        {
                            printname = row["text"].ToString();
                        }
                        else if (data == "Logo")
                        {
                            try
                            {
                                string logo = getPath();
                                if (logo != null || logo != "")
                                {
                                    double x1 = Convert.ToDouble(row["objectx"]) * actualwidth;
                                    double y1 = Convert.ToDouble(row["objecty"]) * actualheight;
                                    double w = Convert.ToDouble(row["objectwidth"]) * actualwidth;
                                    double h = Convert.ToDouble(row["objecthieght"]) * actualheight;
                                    System.Drawing.Image img = System.Drawing.Image.FromFile(logo);
                                    e.Graphics.DrawImage(img, new Rectangle((int)x1, (int)y1, (int)w, (int)h));
                                    continue;
                                }
                            }
                            catch (Exception ex)
                            {
                            }

                        }
                        else if (data == "REC_CUSTOMER")
                        {
                            DataRow[] r = Details.Select("TEXT='" + search + "'");
                            DataRow row1 = r.FirstOrDefault();
                            if (CustomerDetails.Rows.Count > 0)
                            {
                                printname = CustomerDetails.Rows[0][row1["COLUMN_NAME"].ToString()].ToString();
                            }
                            else
                            {
                                printname = search;
                            }
                        }
                        else if (data == "INV_SALES_HDR")
                        {
                            if (SalesHDR.Rows.Count > 0)
                            {
                                printname = SalesHDR.Rows[0][search].ToString();
                            }
                            else
                            {
                                printname = search;
                            }
                        }
                        else if (data == "")
                        {
                            if (data == "AmtInWords")
                            {
                                if (SalesHDR.Rows.Count > 0)
                                {
                                    decimal amt = Convert.ToDecimal(SalesHDR.Rows[0]["NET_AMOUNT"]);
                                    printname = AmtToString(amt);
                                }
                                else
                                {
                                    printname = "[AmtInWords]";
                                }

                            }
                        }
                        else
                        {
                            DataRow[] r = Details.Select("TEXT='" + search + "'");
                            DataRow row1 = r.FirstOrDefault();
                            printname = GetDbValues(row1["TABLE"].ToString(), row1["COLUMN_NAME"].ToString());
                        }
                        cmd = new SqlCommand("select [value_type] from invoiceprnline where objectname='" + search.ToString() + "' and template='" + template + "'");
                        datatype = new Info().generalexecutescalar(cmd);
                        if (row["bold"].ToString() == "True")
                            fs1 = FontStyle.Bold;
                        if (row["underline"].ToString() == "True")
                            fs1 = FontStyle.Underline;
                        if (row["strikeout"].ToString() == "True")
                            fs1 = FontStyle.Strikeout;
                        fn = new Font(row["fontname"].ToString(),
                         float.Parse(row["fontsize"].ToString()), fs1, System.Drawing.GraphicsUnit.Point);
                        Brush coloBrush = Brushes.Black;
                        //  headfont = new Font("Times New Roman", 10);
                        double x = (double)Convert.ToDouble(row["objectx"]) * actualwidth;
                        double y = (double)Convert.ToDouble(row["objecty"]) * actualheight;
                        float ss = (float)x;

                        decimal msg;



                        if (decimal.TryParse(printname, out msg))
                        {

                            if (row["type"].ToString() == "general")
                            {
                                e.Graphics.DrawString(printname, fn, new SolidBrush(Color.Black), (float)x, (float)y);
                            }

                            else
                            {
                                StringFormat format = new StringFormat(StringFormatFlags.DirectionRightToLeft);
                                double widthl = (double)Convert.ToDouble(row["objectwidth"]) * actualwidth;
                                x = x + widthl;
                                e.Graphics.DrawString(printname, fn, new SolidBrush(Color.Black), (float)x, (float)y, format);

                            }
                        }
                        else
                        {
                            e.Graphics.DrawString(printname, fn, new SolidBrush(Color.Black), (float)x, (float)y);
                        }



                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);

                        continue;
                    }

                }
                #endregion
            }


            if (IsRepeatFtr)
            {
                if (!IsLastPage)
                {
                    #region Footer section
                    foreach (DataRow row in footerlabels.Rows)
                    {
                        try
                        {
                            string search = row["objectname"].ToString();
                            string data = row["tbl"].ToString();
                            string printname = "";


                            if (data == "New")
                            {
                                printname = row["text"].ToString();
                            }
                            else if (data == "Logo")
                            {
                                try
                                {
                                    string logo = getPath();
                                    if (logo != null || logo != "")
                                    {
                                        double x1 = Convert.ToDouble(row["objectx"]) * actualwidth;
                                        double y1 = Convert.ToDouble(row["objecty"]) * actualheight;
                                        double w = Convert.ToDouble(row["objectwidth"]) * actualwidth;
                                        double h = Convert.ToDouble(row["objecthieght"]) * actualheight;
                                        System.Drawing.Image img = System.Drawing.Image.FromFile(logo);
                                        e.Graphics.DrawImage(img, new Rectangle((int)x1, (int)y1, (int)w, (int)h));
                                        continue;
                                    }
                                }
                                catch (Exception ex)
                                {
                                }

                            }
                            else if (data == "REC_CUSTOMER")
                            {
                                DataRow[] r = Details.Select("TEXT='" + search + "'");
                                DataRow row1 = r.FirstOrDefault();
                                if (CustomerDetails.Rows.Count > 0)
                                {
                                    printname = CustomerDetails.Rows[0][row1["COLUMN_NAME"].ToString()].ToString();
                                }
                                else
                                {
                                    printname = search;
                                }
                            }
                            else if (data == "INV_SALES_HDR")
                            {
                                if (SalesHDR.Rows.Count > 0)
                                {
                                    printname = SalesHDR.Rows[0][search].ToString();
                                }
                                else
                                {
                                    printname = search;
                                }
                            }
                            else if (data == "")
                            {
                                if (search == "AmtInWords")
                                {
                                    if (SalesHDR.Rows.Count > 0)
                                    {
                                        decimal amt = Convert.ToDecimal(SalesHDR.Rows[0]["NET_AMOUNT"]);
                                        printname = AmtToString(amt);
                                    }
                                    else
                                    {
                                        printname = "[AmtInWords]";
                                    }
                                }
                            }
                            else
                            {
                                DataRow[] r = Details.Select("TEXT='" + search + "'");
                                DataRow row1 = r.FirstOrDefault();
                                printname = GetDbValues(row1["TABLE"].ToString(), row1["COLUMN_NAME"].ToString());
                            }
                            cmd = new SqlCommand("select [value_type] from invoiceprnline where objectname='" + search.ToString() + "' and template='" + template + "'");
                            datatype = new Info().generalexecutescalar(cmd);
                            if (row["bold"].ToString() == "True")
                                fs1 = FontStyle.Bold;
                            if (row["underline"].ToString() == "True")
                                fs1 = FontStyle.Underline;
                            if (row["strikeout"].ToString() == "True")
                                fs1 = FontStyle.Strikeout;
                            fn = new Font(row["fontname"].ToString(),
                             float.Parse(row["fontsize"].ToString()), fs1, System.Drawing.GraphicsUnit.Point);
                            Brush coloBrush = Brushes.Black;
                            //  headfont = new Font("Times New Roman", 10);
                            double x = (double)Convert.ToDouble(row["objectx"]) * actualwidth;
                            double y = (double)Convert.ToDouble(row["objecty"]) * actualheight;
                            float ss = (float)x;

                            decimal msg;



                            if (decimal.TryParse(printname, out msg))
                            {
                                StringFormat format = new StringFormat(StringFormatFlags.DirectionRightToLeft);
                                double widthl = (double)Convert.ToDouble(row["objectwidth"]) * actualwidth;
                                x = x + widthl;
                                e.Graphics.DrawString(printname, fn, new SolidBrush(Color.Black), (float)x, (float)y, format);

                            }
                            else
                            {
                                e.Graphics.DrawString(printname, fn, new SolidBrush(Color.Black), (float)x, (float)y);
                            }


                        }

                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);

                            continue;
                        }
                    }
                    #endregion
                }
            }
            
               

                #region Details
                k = 0;
                int itemnameindex = 1;
                decimal outval;
                //  detailsfont = new Font("Times New Roman", 8);
                detailsfont = new Font(margins.Rows[0]["details_fontname"].ToString(),
                                    float.Parse(margins.Rows[0]["details_fontsize"].ToString()), fs1, System.Drawing.GraphicsUnit.Point);
                int j = 0; int i = 0;
                double rowheight = rowheaderheight + 5;
                int minLine = Convert.ToInt32(margins.Rows[0]["MIN_LINE"]);
                int maxLine = Convert.ToInt32(margins.Rows[0]["MAX_LINE"]);
                string AmountAlignmnt = margins.Rows[0]["AMT_ALGNMNT"].ToString();
                int LineLength = Convert.ToInt32(margins.Rows[0]["NAME_LENGTH"]);
                decimal output;

                for (int it = printeditems; it < SalesDTL.Rows.Count; it++)
                {

                    double pos = rowheight, TotalHeight = 0;

                    foreach (DataRow rows in gridview.Rows)
                    {
                        string column = rows["NAME"].ToString();
                        string printname = "";
                        if (gridview.Rows.Count > 0)
                        {
                            if (column == "Sl No")
                            {
                                printname = (it+1).ToString();
                            }
                            else if (column == "CGST %" || column == "SGST %")
                            {
                                printname = (Convert.ToDecimal(SalesDTL.Rows[it]["ITEM_TAX_PER"]) / 2).ToString();
                            }
                            else if (column == "CGST Amt" || column == "SGST Amt")
                            {
                                printname = (Convert.ToDecimal(SalesDTL.Rows[it]["ITEM_TAX"]) / 2).ToString();
                            }
                              
                            else if (column == "HSN")
                            {
                                string item = SalesDTL.Rows[it]["ITEM_CODE"].ToString() ;
                                printname = ItemDirectoryDB.HSN(item);
                            }
                            else if (column == "IGST %")
                            {
                                printname = (Convert.ToDecimal(SalesDTL.Rows[it]["ITEM_TAX_PER"])).ToString();
                            }
                            else if (column == "IGST Amt")
                            {
                                printname = (Convert.ToDecimal(SalesDTL.Rows[it]["ITEM_TAX"])).ToString();
                            }
                            else if (column == "Amount")
                            {
                                printname = (Convert.ToDecimal(SalesDTL.Rows[it]["PRICE"]) * Convert.ToDecimal(SalesDTL.Rows[it]["QUANTITY"])).ToString();
                            }
                                //mayoosha code

                            else
                            {
                                printname = SalesDTL.Rows[it][column].ToString();
                            }
                        }
                        else
                        {
                            printname = column;
                        }
                        double startx = Convert.ToDouble(rows["startx"].ToString()) * actualwidth;
                        double starty = Convert.ToDouble(rows["starty"].ToString()) * actualheight;
                        double endx = Convert.ToDouble(rows["endx"].ToString()) * actualwidth;
                        double endy = Convert.ToDouble(rows["endy"].ToString()) * actualheight;
                        if (Convert.ToInt32(rows["INDEX"]) == 0)
                        {
                            if (column == "ITEM_DESC_ENG")
                            {
                                int StringLength = column.Length;
                                int TotalStringwidth = TextRenderer.MeasureText(printname, detailsfont).Width;
                                int Onewidth = TotalStringwidth / StringLength;
                                int ColumnWidth =Convert.ToInt32((Convert.ToDouble(rows["width"]) * actualheight));
                                int CapableLength = ColumnWidth / Onewidth;
                                foreach (string name in SplitByLength(printname, CapableLength))
                                {
                                    e.Graphics.DrawString(name, detailsfont, new SolidBrush(Color.Black), (float)left + 2, (float)(starty + pos));
                                    pos = pos + 10;
                                }


                            }
                            else
                            {

                                if (decimal.TryParse(printname, out output) && AmountAlignmnt == "Right To Left")
                                {
                                    StringFormat rightToLeft = new StringFormat(StringFormatFlags.DirectionRightToLeft);
                                    double widthl = (double)Convert.ToDouble(rows["WIDTH"]) * actualwidth;
                                    e.Graphics.DrawString(printname, detailsfont, new SolidBrush(Color.Black), (float)widthl- 2, (float)(starty + rowheight), rightToLeft);
                                }
                                else
                                {
                                    e.Graphics.DrawString(printname, detailsfont, new SolidBrush(Color.Black), (float)left + 2, (float)(starty + rowheight));
                                }
                            }
                        }
                        else
                        {
                            if (column == "ITEM_DESC_ENG")
                            {
                                int StringLength = printname.Length;
                                printname= printname.ToUpper();
                                int TotalStringwidth = TextRenderer.MeasureText(printname, detailsfont).Width; //560


                                int Onewidth = TotalStringwidth / StringLength;
                                double ColumnWidth = (Convert.ToDouble(rows["width"]) * actualwidth)-2; //336
                                double CapableLength = ColumnWidth / Onewidth;
                                //MessageBox.Show("STRING=" + TotalStringwidth + " GRID=" + ColumnWidth+" LENGTH="+A.Length);





                                foreach (string name in SplitByLength(printname,(int)(CapableLength-7)))
                                {
                                    e.Graphics.DrawString(name, detailsfont, new SolidBrush(Color.Black), (float)startx + 2, (float)(starty + pos));
                                    pos = pos + 10;
                                }

                            }
                            else
                            {
                                if (decimal.TryParse(printname, out output) && AmountAlignmnt == "Right To Left")
                                {
                                    StringFormat format = new StringFormat(StringFormatFlags.DirectionRightToLeft);
                                    double widthl = (double)Convert.ToDouble(rows["WIDTH"]) * actualwidth;
                                    e.Graphics.DrawString(printname, detailsfont, new SolidBrush(Color.Black), (float)startx + (float)widthl - 2, (float)(starty + rowheight), format);
                                }
                                else
                                {
                                    e.Graphics.DrawString(printname, detailsfont, new SolidBrush(Color.Black), (float)startx + 2, (float)(starty + rowheight));
                                }
                            }
                        }
                        TotalHeight = starty;
                        //e.Graphics.DrawString(rows[a].ToString(), detailsfont, new SolidBrush(Color.Black), (float)psx - (float)xplus, (float)psy + (float)rowheight, format);
                    }
                    i++;
                    printeditems++;

                    rowheight = pos + Convert.ToInt32(Convert.ToInt32(margins.Rows[0]["row_height"]));
                    if (maxLine == i || bottom <= TotalHeight + rowheight)
                    {

                        goto Loop;
                    }
                    else
                    {
                        e.HasMorePages = false;
                    }
                }

                #endregion
           
            Loop:
                if (printeditems < SalesDTL.Rows.Count)
                {
                    IsFirstPage = false;
                    e.HasMorePages = true;
                }
                else
                {
                    IsLastPage = true;
                    #region Footer section
                    foreach (DataRow row in footerlabels.Rows)
                    {
                        try
                        {
                            string search = row["objectname"].ToString();
                            string data = row["tbl"].ToString();
                            string printname = "";


                            if (data == "New")
                            {
                                printname = row["text"].ToString();
                            }
                            else if (data == "Logo")
                            {
                                try
                                {
                                    string logo = getPath();
                                    if (logo != null || logo != "")
                                    {
                                        double x1 = Convert.ToDouble(row["objectx"]) * actualwidth;
                                        double y1 = Convert.ToDouble(row["objecty"]) * actualheight;
                                        double w = Convert.ToDouble(row["objectwidth"]) * actualwidth;
                                        double h = Convert.ToDouble(row["objecthieght"]) * actualheight;
                                        System.Drawing.Image img = System.Drawing.Image.FromFile(logo);
                                        e.Graphics.DrawImage(img, new Rectangle((int)x1, (int)y1, (int)w, (int)h));
                                        continue;
                                    }
                                }
                                catch (Exception ex)
                                {
                                }

                            }
                            else if (data == "REC_CUSTOMER")
                            {
                                DataRow[] r = Details.Select("TEXT='" + search + "'");
                                DataRow row1 = r.FirstOrDefault();
                                if (CustomerDetails.Rows.Count > 0)
                                {
                                    printname = CustomerDetails.Rows[0][row1["COLUMN_NAME"].ToString()].ToString();
                                }
                                else
                                {
                                    printname = search;
                                }
                            }
                            else if (data == "INV_SALES_HDR")
                            {
                                if (SalesHDR.Rows.Count > 0)
                                {
                                    printname = SalesHDR.Rows[0][search].ToString();
                                }
                                else
                                {
                                    printname = search;
                                }
                            }
                            else if (data == "")
                            {
                                if (search == "AmtInWords")
                                {
                                    if (SalesHDR.Rows.Count > 0)
                                    {
                                        decimal amt = Convert.ToDecimal(SalesHDR.Rows[0]["NET_AMOUNT"]);
                                        printname = AmtToString(amt);
                                    }
                                    else
                                    {
                                        printname = "[AmtInWords]";
                                    }
                                }
                            }
                            else
                            {
                                DataRow[] r = Details.Select("TEXT='" + search + "'");
                                DataRow row1 = r.FirstOrDefault();
                                printname = GetDbValues(row1["TABLE"].ToString(), row1["COLUMN_NAME"].ToString());
                            }
                            cmd = new SqlCommand("select [value_type] from invoiceprnline where objectname='" + search.ToString() + "' and template='" + template + "'");
                            datatype = new Info().generalexecutescalar(cmd);
                            if (row["bold"].ToString() == "True")
                                fs1 = FontStyle.Bold;
                            if (row["underline"].ToString() == "True")
                                fs1 = FontStyle.Underline;
                            if (row["strikeout"].ToString() == "True")
                                fs1 = FontStyle.Strikeout;
                            fn = new Font(row["fontname"].ToString(),
                             float.Parse(row["fontsize"].ToString()), fs1, System.Drawing.GraphicsUnit.Point);
                            Brush coloBrush = Brushes.Black;
                            //  headfont = new Font("Times New Roman", 10);
                            double x = (double)Convert.ToDouble(row["objectx"]) * actualwidth;
                            double y = (double)Convert.ToDouble(row["objecty"]) * actualheight;
                            float ss = (float)x;

                            decimal msg;



                            if (decimal.TryParse(printname, out msg))
                            {
                                StringFormat format = new StringFormat(StringFormatFlags.DirectionRightToLeft);
                                double widthl = (double)Convert.ToDouble(row["objectwidth"]) * actualwidth;
                                x = x + widthl;
                                e.Graphics.DrawString(printname, fn, new SolidBrush(Color.Black), (float)x, (float)y, format);

                            }
                            else
                            {
                                e.Graphics.DrawString(printname, fn, new SolidBrush(Color.Black), (float)x, (float)y);
                            }


                        }

                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);

                            continue;
                        }
                    }
                    #endregion
                }
           }

        

       
        void GetValues(string templatename)
        {
           
            cmd = new SqlCommand("select * from  invoiceprnline where template='" + templatename + "' and visible='True' and type='general'");
            labels = inf.get_genaraldata(cmd);
            cmd = new SqlCommand("select * from  invoiceprnline where template='" + templatename + "' and visible='True' and type='footer'");
            footerlabels = inf.get_genaraldata(cmd);
            cmd = new SqlCommand("select * from INVOICE_a4columns   where template='" + templatename + "' and visible='True'  ORDER BY [INDEX]");
            gridview = inf.get_genaraldata(cmd);
            cmd = new SqlCommand("select * from invoice_lines where template='" + templatename + "'");
            lines = inf.get_genaraldata(cmd);
            cmd = new SqlCommand("select * from INVOICE_a4_general where template='" + templatename + "'");
            settings = inf.get_genaraldata(cmd);
            cmd = new SqlCommand("select * from invoice_rectangle where template='" + templatename + "'");
            rect = inf.get_genaraldata(cmd);
        }

        string GetDbValues(string table, string column)
        {
            string values="";
            cmd=new SqlCommand("select "+column+" from "+table);
            values=inf.get_Scalar(cmd);
            return values;
        }
        string GetDbValues(string table, string column,string key)
        {
            string PrimaryKey="";
            if(table=="REC_CUSTOMER"||table=="PAY_SUPPLUER")
            {

            }
            string values = "";
            cmd = new SqlCommand("select " + column + " from " + table + " WHERE " + PrimaryKey+"'="+key+"'");
            values = inf.get_Scalar(cmd);
            return values;
        }
       
        IEnumerable<string> SplitByLength(string str, int maxLength)
        {
            for (int index = 0; index < str.Length; index += maxLength)
            {
                yield return str.Substring(index, Math.Min(maxLength, str.Length - index));
            }
        }
        string AmtToString(decimal Amt)
        {
            try
            {
                //int cash = (int)Convert.ToDouble(ttotalvalue);
                int cash = (int)Convert.ToDouble(Amt);
                string cas = Amt.ToString();
                string[] parts = cas.Split('.');
                string test3 = "";
                long i1, i2;
                try
                {
                    i1 = (long)Convert.ToDouble(parts[0]);
                }
                catch
                {
                    i1 = 0;
                }
                try
                {
                    i2 = (long)Convert.ToDouble(parts[1]);
                }
                catch
                {
                    i2 = 0;
                }

                if (i1 != 0 && i2 != 0)
                {
                    string test = NumbersToWords(i1);
                    string test2 = NumbersToWords(i2);
                    test3 = test + " Rupees and " + test2 + "Paisa only";

                    string seclin, linef;
                    int index = test3.IndexOf("Rupees");
                }
                if (i1 > 0 && i2 == 0)
                {
                    string test = NumbersToWords(i1);
                    test3 = test + " only";
                }
                return test3;
            }
            catch
            {
                return "";
            }

        }
        public static string NumbersToWords(long inputNumber)
        {
            long inputNo = inputNumber;

            if (inputNo == 0)
                return "Zero";

            long[] numbers = new long[4];
            long first = 0;
            long u, h, t;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            if (inputNo < 0)
            {
                sb.Append("Minus ");
                inputNo = -inputNo;
            }

            string[] words0 = {"" ,"One ", "Two ", "Three ", "Four ",
            "Five " ,"Six ", "Seven ", "Eight ", "Nine "};
            string[] words1 = {"Ten ", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ",
            "Fifteen ","Sixteen ","Seventeen ","Eighteen ", "Nineteen "};
            string[] words2 = {"Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ",
            "Seventy ","Eighty ", "Ninety "};
            string[] words3 = { "Thousand ", "Lakh ", "Crore " };

            numbers[0] = inputNo % 1000; // units
            numbers[1] = inputNo / 1000;
            numbers[2] = inputNo / 100000;
            numbers[1] = numbers[1] - 100 * numbers[2]; // thousands
            numbers[3] = inputNo / 10000000; // crores
            numbers[2] = numbers[2] - 100 * numbers[3]; // lakhs

            for (int i = 3; i > 0; i--)
            {
                if (numbers[i] != 0)
                {
                    first = i;
                    break;
                }
            }
            for (long i = first; i >= 0; i--)
            {
                if (numbers[i] == 0) continue;
                u = numbers[i] % 10; // ones
                t = numbers[i] / 10;
                h = numbers[i] / 100; // hundreds
                t = t - 10 * h; // tens
                if (h > 0) sb.Append(words0[h] + "Hundred ");
                if (u > 0 || t > 0)
                {
                    if (h > 0 || i == 0) sb.Append("");
                    if (t == 0)
                        sb.Append(words0[u]);
                    else if (t == 1)
                        sb.Append(words1[u]);
                    else
                        sb.Append(words2[t - 2] + words0[u]);
                }
                if (i != 0) sb.Append(words3[i - 1]);
            }
            return sb.ToString().TrimEnd();
        }
        public void printPageDynamicThermal(object sender, PrintPageEventArgs e)
        {
            k = 0;
            pageno++;
            e.HasMorePages = false;

            DataTable margins = new DataTable();
            margins = new invoiceLineBLL().getgeneral(template);
            if (margins.Rows.Count > 0)
            {

                language = margins.Rows[0]["language"].ToString();
            }
            else
            {
                MessageBox.Show("There Is no Design With This Template");
                return;
            }
            GetValues(template);
            int PrintId = 0;
            int ItemHieght = 0;
            Pen black = new Pen(Color.Black, 1);
            System.Drawing.Font fn = null;
            FontStyle fs1 = new FontStyle();
            int panl_width = Convert.ToInt32(settings.Rows[0]["form_width"]);// this.main_pnl.ClientSize.Width;
            int pnl_height = Convert.ToInt32(settings.Rows[0]["form_height"]); //;556; //this.main_pnl.ClientSize.Height;

            double actualwidth = (double)Convert.ToDouble(margins.Rows[0]["paper_width"]) / panl_width;

            double actualheight = getHeight(margins) / pnl_height;
            
            double left = 0;
            double right = 0;
            double width = 0;
            double height = 0;
            double rowheaderheight = 0;
            double rowheadery = 0;
            double bottom = 0;
            int nameheight = 0;
            int name_morepage = 0;
            //details
            int inc = 0;

            //drawrectangle
            left = (double)Convert.ToDouble(margins.Rows[0]["location_x"]) * actualwidth;
            right = (double)Convert.ToDouble(margins.Rows[0]["location_y"]) * actualheight;
            width = (double)Convert.ToDouble(margins.Rows[0]["width"]) * actualwidth;
            height = (double)Convert.ToDouble(margins.Rows[0]["height"]) * actualheight;
            rowheaderheight = (double)Convert.ToDouble(margins.Rows[0]["rowheader_height"]);
            //grid line
          //  e.Graphics.DrawRectangle(black, (int)left, (int)right, (int)width, (int)height);
           // e.Graphics.DrawLine(black, (float)left, (float)right + (float)rowheaderheight, ((float)width + (float)left), (float)right + (float)rowheaderheight);


            rowheadery = (double)Convert.ToDouble(margins.Rows[0]["right"]) * actualwidth;
            bottom = Convert.ToDouble(margins.Rows[0]["bottom"]) * actualheight;
            add_gridheight = Convert.ToInt32((Convert.ToDouble(margins.Rows[0]["bottom"]) * actualheight));

          
        
            #region headings

            foreach (DataRow rows in gridview.Rows)
            {
                if (rows["visible"].ToString() == "True")
                {

                    try
                    {

                        string search = rows["TEXT"].ToString();
                        double sx = (double)Convert.ToDouble(rows["startx"]) * actualwidth;
                        double sy = (double)Convert.ToDouble(rows["starty"]) * actualheight;
                        double ex = (double)Convert.ToDouble(rows["endx"]) * actualwidth;
                        double ey = (double)Convert.ToDouble(rows["endy"]) * actualheight;
                       
                        if (k != 0)
                        {
                            StringFormat format = new StringFormat(StringFormatFlags.MeasureTrailingSpaces);
                            e.Graphics.DrawString(search, headfont, new SolidBrush(Color.Black), (float)sx + 2, (float)sy + 2);
                          //  e.Graphics.DrawLine(black, (float)sx, (float)sy, (float)sx, (float)add_gridheight);
                        }

                        else
                        {
                            e.Graphics.DrawString(search, headfont, new SolidBrush(Color.Black), (float)left + 2, (float)sy + 2);
                        }


                        k++;

                    }
                    catch (Exception EX)
                    {

                        continue;

                    }


                }


            }
            #endregion


            #region Labels
            foreach (DataRow row in labels.Rows)
            {
                try
                {
                    string search = row["objectname"].ToString();
                    string data = row["tbl"].ToString();
                    string printname = "";


                    if (data == "New")
                    {
                        printname = row["text"].ToString();
                    }
                    else if (data == "REC_CUSTOMER")
                    {
                        DataRow[] r = Details.Select("TEXT='" + search + "'");
                        DataRow row1 = r.FirstOrDefault();
                        if (CustomerDetails.Rows.Count > 0)
                        {
                            printname = CustomerDetails.Rows[0][row1["COLUMN_NAME"].ToString()].ToString();
                        }
                        else
                        {
                            printname = search;
                        }
                    }
                    else if (data == "INV_SALES_HDR")
                    {
                        if (SalesHDR.Rows.Count > 0)
                        {
                            printname = SalesHDR.Rows[0][search].ToString();
                        }
                        else
                        {
                            printname = search;
                        }
                    }
                    else if (data == "")
                    {
                        if (data == "AmtInWords")
                        {
                            if (SalesHDR.Rows.Count > 0)
                            {
                                decimal amt = Convert.ToDecimal(SalesHDR.Rows[0]["NET_AMOUNT"]);
                                printname = AmtToString(amt);
                            }
                            else
                            {
                                printname = "[AmtInWords]";
                            }

                        }
                    }
                    else
                    {
                        DataRow[] r = Details.Select("TEXT='" + search + "'");
                        DataRow row1 = r.FirstOrDefault();
                        printname = GetDbValues(row1["TABLE"].ToString(), row1["COLUMN_NAME"].ToString());
                    }
                    cmd = new SqlCommand("select [value_type] from invoiceprnline where objectname='" + search.ToString() + "' and template='" + template + "'");
                    datatype = new Info().generalexecutescalar(cmd);
                    if (row["bold"].ToString() == "True")
                        fs1 = FontStyle.Bold;
                    if (row["underline"].ToString() == "True")
                        fs1 = FontStyle.Underline;
                    if (row["strikeout"].ToString() == "True")
                        fs1 = FontStyle.Strikeout;
                    fn = new Font(row["fontname"].ToString(),
                     float.Parse(row["fontsize"].ToString()), fs1, System.Drawing.GraphicsUnit.Point);
                    Brush coloBrush = Brushes.Black;
                    //  headfont = new Font("Times New Roman", 10);
                    double x = (double)Convert.ToDouble(row["objectx"]) * actualwidth;
                    double y = (double)Convert.ToDouble(row["objecty"]) * actualheight;
                    float ss = (float)x;

                    decimal msg;



                    if (decimal.TryParse(printname, out msg))
                    {

                        if (row["type"].ToString() == "general")
                        {
                            e.Graphics.DrawString(printname, fn, new SolidBrush(Color.Black), (float)x, (float)y);
                        }

                        else
                        {
                            StringFormat format = new StringFormat(StringFormatFlags.DirectionRightToLeft);
                            double widthl = (double)Convert.ToDouble(row["objectwidth"]) * actualwidth;
                            x = x + widthl;
                            e.Graphics.DrawString(printname, fn, new SolidBrush(Color.Black), (float)x, (float)y, format);

                        }
                    }
                    else
                    {
                        e.Graphics.DrawString(printname, fn, new SolidBrush(Color.Black), (float)x, (float)y);
                    }


                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                    continue;
                }
            }
            #endregion

            #region Details
            k = 0;
            int itemnameindex = 1;
            decimal outval;
            //  detailsfont = new Font("Times New Roman", 8);
            detailsfont = new Font(margins.Rows[0]["details_fontname"].ToString(),
                                float.Parse(margins.Rows[0]["details_fontsize"].ToString()), fs1, System.Drawing.GraphicsUnit.Point);
            
            int j = 0; int i = 0;
            double rowheight = rowheaderheight + 5;
            int minLine = Convert.ToInt32(margins.Rows[0]["MIN_LINE"]);
            int maxLine = Convert.ToInt32(margins.Rows[0]["MAX_LINE"]);
            string AmountAlignmnt = margins.Rows[0]["AMT_ALGNMNT"].ToString();
            int LineLength = Convert.ToInt32(margins.Rows[0]["NAME_LENGTH"]);
            decimal output;

            for (int it = printeditems; it < SalesDTL.Rows.Count; it++)
            {

                double pos = rowheight, TotalHeight = 0;
                bool isCurrentLine = true;
                foreach (DataRow rows in gridview.Rows)
                {
                    string column = rows["NAME"].ToString();
                    string printname = "";
                    if (gridview.Rows.Count > 0)
                    {
                        if (column == "Sl No")
                        {
                            printname = (it + 1).ToString();
                        }
                        else if (column == "CGST %" || column == "SGST %")
                        {
                            printname = (Convert.ToDecimal(SalesDTL.Rows[it]["ITEM_TAX_PER"]) / 2).ToString();
                        }
                        else if (column == "CGST Amt" || column == "SGST Amt")
                        {
                            printname = (Convert.ToDecimal(SalesDTL.Rows[it]["ITEM_TAX"]) / 2).ToString();
                        }

                        else
                        {
                            printname = SalesDTL.Rows[it][column].ToString();
                        }
                    }
                    else
                    {
                        printname = column;
                    }
                    double startx = Convert.ToDouble(rows["startx"].ToString()) * actualwidth;
                    double starty = Convert.ToDouble(rows["starty"].ToString()) * actualheight;
                    double endx = Convert.ToDouble(rows["endx"].ToString()) * actualwidth;
                    double endy = Convert.ToDouble(rows["endy"].ToString()) * actualheight;
                    if (Convert.ToInt32(rows["INDEX"]) == 0)
                    {
                       
                        if (column == "ITEM_DESC_ENG")
                        {
                           
                            printname = printname.ToUpper();
                            int TotalStringwidth = TextRenderer.MeasureText(printname, detailsfont).Width;
                            int stringLength=printname.Length;
                            if (TotalStringwidth <=Convert.ToInt32(rows["width"])*actualwidth)
                            {
                                //in case of item_name width is shorter than column width
                                e.Graphics.DrawString(printname, detailsfont, new SolidBrush(Color.Black), (float)left + 2, (float)(starty + pos));
                                pos = pos + 10;
                            }
                            else
                            {
                                //in case of item_name width is larger than column width
                                isCurrentLine = false;
                                double TotalColumn = (Convert.ToDouble(margins.Rows[0]["right"]) * actualwidth)- (Convert.ToDouble(rows["STARTX"]) * actualwidth);
                                int available_length = Convert.ToInt32(stringLength * (Convert.ToInt32(rows["width"]) * actualwidth) / TotalStringwidth);
                                int getLength=Convert.ToInt32(stringLength*TotalColumn/TotalStringwidth);
                                string name = "";
                                if (getLength < stringLength)
                                {
                                    name = printname.Substring(0, getLength);
                                    e.Graphics.DrawString(name, detailsfont, new SolidBrush(Color.Black), (float)left + 2, (float)(starty + pos));
                                    pos = pos + 10;
                                    //to get balance string
                                    int balanceLength=stringLength - getLength;
                                    if (balanceLength > available_length)
                                    {
                                        name = printname.Substring(getLength, available_length);
                                        

                                    }
                                    else
                                    {
                                        name = printname.Substring(getLength, balanceLength);
                                        
                                    }
                                    e.Graphics.DrawString(name, detailsfont, new SolidBrush(Color.Black), (float)left + 2, (float)(starty + pos));
                                }
                                else
                                {

                                    name = printname;
                                    e.Graphics.DrawString(name, detailsfont, new SolidBrush(Color.Black), (float)left + 2, (float)(starty + pos));
                                    pos = pos + 10;
                                }
                               
                               
                            }
                             
                        }
                        else
                        {
                            if (!isCurrentLine)
                            {
                                rowheight = (pos - 10) + Convert.ToInt32(margins.Rows[0]["row_height"]);
                                //  pos =pos+ Convert.ToInt32(margins.Rows[0]["row_height"]);
                                // isCurrentLine = true;
                            }
                            if (decimal.TryParse(printname, out output) && AmountAlignmnt == "Right To Left")
                            {
                                StringFormat rightToLeft = new StringFormat(StringFormatFlags.DirectionRightToLeft);
                                double widthl = (double)Convert.ToDouble(rows["WIDTH"]) * actualwidth;
                                e.Graphics.DrawString(printname, detailsfont, new SolidBrush(Color.Black), (float)left + (float)widthl - 2, (float)(starty + rowheight), rightToLeft);
                            }
                            else
                            {
                                e.Graphics.DrawString(printname, detailsfont, new SolidBrush(Color.Black), (float)left + 2, (float)(starty + rowheight));
                            }
                            if (!isCurrentLine)
                            {
                                // rowheight = pos + Convert.ToInt32(margins.Rows[0]["row_height"]);
                                pos = pos + Convert.ToInt32(margins.Rows[0]["row_height"]);
                                isCurrentLine = true;
                            }
                        }
                    }
                    else
                    {
                        if (column == "ITEM_DESC_ENG")
                        {
                           
                            printname = printname.ToUpper();
                            int TotalStringwidth = TextRenderer.MeasureText(printname, detailsfont).Width;
                            int stringLength=printname.Length;
                            if (TotalStringwidth <= Convert.ToInt32(rows["width"]) * actualwidth)
                            {
                                e.Graphics.DrawString(printname, detailsfont, new SolidBrush(Color.Black), (float)startx + 2, (float)(starty + pos));
                                pos = pos + 10;
                            }
                            else
                            {

                                //in case of item_name width is larger than column width
                                isCurrentLine = false;
                                double TotalColumn = (Convert.ToDouble(margins.Rows[0]["right"]) * actualwidth) - (Convert.ToDouble(rows["STARTX"]) * actualwidth);
                                int available_length = Convert.ToInt32(stringLength * (Convert.ToInt32(rows["width"]) * actualwidth) / TotalStringwidth);
                                int getLength = Convert.ToInt32(stringLength * TotalColumn / TotalStringwidth);
                                string name = "";
                                if (getLength < stringLength)
                                {
                                    name = printname.Substring(0, getLength);
                                    e.Graphics.DrawString(name, detailsfont, new SolidBrush(Color.Black), (float)startx + 2, (float)(starty + pos));
                                    pos = pos + 10;
                                  ////  to get balance string
                                  //  int balanceLength = stringLength - getLength;
                                  //  if (balanceLength > available_length)
                                  //  {
                                  //      name = printname.Substring(getLength, available_length);


                                  //  }
                                  //  else
                                  //  {
                                  //      name = printname.Substring(getLength, balanceLength);

                                  //  }
                                  //  e.Graphics.DrawString(name, detailsfont, new SolidBrush(Color.Black), (float)startx + 2, (float)(starty + pos));
                                }
                                else
                                {

                                    name = printname;
                                    e.Graphics.DrawString(name, detailsfont, new SolidBrush(Color.Black), (float)startx + 2, (float)(starty + pos));
                                    pos = pos + 10;
                                }
                            }
                        }
                        else
                        {
                            if (!isCurrentLine)
                            {
                                rowheight = (pos-10) + Convert.ToInt32(margins.Rows[0]["row_height"]);
                              //  pos =pos+ Convert.ToInt32(margins.Rows[0]["row_height"]);
                               // isCurrentLine = true;
                            }
                            if (decimal.TryParse(printname, out output) && AmountAlignmnt == "Right To Left")
                            {
                                StringFormat format = new StringFormat(StringFormatFlags.DirectionRightToLeft);
                                double widthl = (double)Convert.ToDouble(rows["WIDTH"]) * actualwidth;
                                e.Graphics.DrawString(printname, detailsfont, new SolidBrush(Color.Black), (float)startx + (float)widthl - 2, (float)(starty + rowheight), format);
                            }
                            else
                            {
                                e.Graphics.DrawString(printname, detailsfont, new SolidBrush(Color.Black), (float)startx + 2, (float)(starty + rowheight));
                            }
                            if (!isCurrentLine)
                            {
                               // rowheight = pos + Convert.ToInt32(margins.Rows[0]["row_height"]);
                                pos = pos + Convert.ToInt32(margins.Rows[0]["row_height"]);
                                isCurrentLine = true;
                            }
                        }
                    }
                    TotalHeight = starty;
                    //e.Graphics.DrawString(rows[a].ToString(), detailsfont, new SolidBrush(Color.Black), (float)psx - (float)xplus, (float)psy + (float)rowheight, format);
                }
                i++;
                printeditems++;

                rowheight = pos + Convert.ToInt32(Convert.ToInt32(margins.Rows[0]["row_height"]));
              
            }

            #endregion

      
                #region Footer section
                foreach (DataRow row in footerlabels.Rows)
                {
                    try
                    {
                        string search = row["objectname"].ToString();
                        string data = row["tbl"].ToString();
                        string printname = "";


                        if (data == "New")
                        {
                            printname = row["text"].ToString();
                        }
                        else if (data == "REC_CUSTOMER")
                        {
                            DataRow[] r = Details.Select("TEXT='" + search + "'");
                            DataRow row1 = r.FirstOrDefault();
                            if (CustomerDetails.Rows.Count > 0)
                            {
                                printname = CustomerDetails.Rows[0][row1["COLUMN_NAME"].ToString()].ToString();
                            }
                            else
                            {
                                printname = search;
                            }
                        }
                        else if (data == "INV_SALES_HDR")
                        {
                            if (SalesHDR.Rows.Count > 0)
                            {
                                printname = SalesHDR.Rows[0][search].ToString();
                            }
                            else
                            {
                                printname = search;
                            }
                        }
                        else if (data == "")
                        {
                            if (search == "AmtInWords")
                            {
                                if (SalesHDR.Rows.Count > 0)
                                {
                                    decimal amt = Convert.ToDecimal(SalesHDR.Rows[0]["NET_AMOUNT"]);
                                    printname = AmtToString(amt);
                                }
                                else
                                {
                                    printname = "[AmtInWords]";
                                }
                            }
                        }
                        else
                        {
                            DataRow[] r = Details.Select("TEXT='" + search + "'");
                            DataRow row1 = r.FirstOrDefault();
                            printname = GetDbValues(row1["TABLE"].ToString(), row1["COLUMN_NAME"].ToString());
                        }
                        cmd = new SqlCommand("select [value_type] from invoiceprnline where objectname='" + search.ToString() + "' and template='" + template + "'");
                        datatype = new Info().generalexecutescalar(cmd);
                        if (row["bold"].ToString() == "True")
                            fs1 = FontStyle.Bold;
                        if (row["underline"].ToString() == "True")
                            fs1 = FontStyle.Underline;
                        if (row["strikeout"].ToString() == "True")
                            fs1 = FontStyle.Strikeout;
                        fn = new Font(row["fontname"].ToString(),
                         float.Parse(row["fontsize"].ToString()), fs1, System.Drawing.GraphicsUnit.Point);
                        Brush coloBrush = Brushes.Black;
                        //  headfont = new Font("Times New Roman", 10);
                        double x = (double)Convert.ToDouble(row["objectx"]) * actualwidth;
                        double y = (double)Convert.ToDouble(row["objecty"]) * actualheight;
                        float ss = (float)x;

                        decimal msg;



                        if (decimal.TryParse(printname, out msg))
                        {
                            StringFormat format = new StringFormat(StringFormatFlags.DirectionRightToLeft);
                            double widthl = (double)Convert.ToDouble(row["objectwidth"]) * actualwidth;
                            x = x + widthl;
                            e.Graphics.DrawString(printname, fn, new SolidBrush(Color.Black), (float)x, (float)y, format);

                        }
                        else
                        {
                            e.Graphics.DrawString(printname, fn, new SolidBrush(Color.Black), (float)x, (float)y);
                        }


                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);

                        continue;
                    }
                //}
                #endregion
            }
        }
        double getHeight(DataTable templateDesign)
        {
            double TotalHeight = 0;
            double Header = Convert.ToDouble(templateDesign.Rows[0]["TOP"]);
            double Footer = Convert.ToDouble(templateDesign.Rows[0]["FORM_HEIGHT"]) - Convert.ToDouble(templateDesign.Rows[0]["BOTTOM"]);
            double Rowheight = Convert.ToDouble(templateDesign.Rows[0]["Row_Height"]);
            double GridHeight = Convert.ToDouble(templateDesign.Rows[0]["height"]);
            if(TYPE=="PREVIEW")
            {
                TotalHeight = GridHeight;
                return TotalHeight + Header + Footer;
            }
            else
            {
                TotalHeight = hgt;
                return TotalHeight;
            }
            
        }
       public string getType(string template)
        {
            cmd = new SqlCommand("select paper_type from  INVOICE_a4_general where template='" + template + "'");
            return Convert.ToString(inf.get_Scalar(cmd));
        }
       string getPath()
       {
           cmd = new SqlCommand("Select logo from Tbl_CompanySetup");
           string Path = inf.get_Scalar(cmd).ToString();
           return Path;
       }
        
    }

}




