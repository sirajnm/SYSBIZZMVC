using ComponentFactory.Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using OnBarcode.Barcode;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Drawing.Drawing2D;
using System.Diagnostics;
using Sys_Sols_Inventory.Model;
namespace Sys_Sols_Inventory
{
    public partial class Generate_Barcode : Form
    {

        //private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        //private SqlCommand cmd = new SqlCommand();
        //private SqlDataAdapter adapter = new SqlDataAdapter();
        Class.BarcodeSettings barcodse = new Class.BarcodeSettings();
        private bool hasBatch = General.IsEnabled(Settings.Batch);
        Initial mdi = (Initial)Application.OpenForms["Initial"];
        Login lg = (Login)Application.OpenForms["Login"];
      //  clsBarcode clbar = new clsBarcode();
        string companyname,PriceType;
        bool IsMRP = false, IsProductCode = false, IsCompany = false, IsBarcode = false;
        int HEIGHT, WIDTH;

        public Generate_Barcode()
        {
            InitializeComponent();
        }

       
        

        private void btnItemCode_Click(object sender, EventArgs e)
        {
            ItemMasterHelp h = new ItemMasterHelp(0);
            if (h.ShowDialog() == DialogResult.OK && h.c != null)
            {
                ITEM_CODE.Text = Convert.ToString(h.c[0].Value);
                ITEM_NAME.Text = Convert.ToString(h.c[1].Value);
               
                PRICE.Text = Convert.ToString(h.c[8].Value);


                //conn.Open();
                //cmd.CommandText = "SELECT UNIT_CODE,PACK_SIZE,BARCODE FROM INV_ITEM_DIRECTORY_UNITS WHERE ITEM_CODE = '" + ITEM_CODE.Text + "'";
                barcodse.ItemCode = ITEM_CODE.Text;
                SqlDataReader r = barcodse.selectBarcode();
                dgRates.Rows.Clear();
                while (r.Read())
                {
                    dgRates.Rows.Add(r["UNIT_CODE"], r["PACK_SIZE"], r["BARCODE"]);
                }
                //conn.Close();
                DbFunctions.CloseConnection();
                if (!hasBatch)
                {
                    //conn.Open();
                    //cmd.CommandText = "SELECT UNIT_CODE,SAL_TYPE,PRICE FROM INV_ITEM_PRICE WHERE ITEM_CODE = '" + ITEM_CODE.Text + "'";
                    SqlDataReader r1 = barcodse.selectPriceList();
                    while (r1.Read())
                    {
                        for (int i = 0; i < dgRates.Rows.Count - 1; i++)
                        {
                            DataGridViewCellCollection c = dgRates.Rows[i].Cells;
                            if (Convert.ToString(r1["UNIT_CODE"]).Equals(Convert.ToString(c[0].Value)))
                            {
                                c[Convert.ToString(r1["SAL_TYPE"])].Value = r1["PRICE"];
                            }
                        }
                    }
                 //   conn.Close();
                    DbFunctions.CloseConnection();
                }
              

            }
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            try
            {
                if (lg.Theme == "1")
                {

                    ComponentFactory.Krypton.Navigator.KryptonPage kp = new ComponentFactory.Krypton.Navigator.KryptonPage();

                    mdi.maindocpanel.SelectedPage.Dispose();
                }
                else
                {
                    this.Close();
                    //ComponentFactory.Krypton.Navigator.KryptonPage kp = new ComponentFactory.Krypton.Navigator.KryptonPage();

                    //mdi.maindocpanel.SelectedPage.Dispose();
                }


            }
            catch
            {
                this.Close();
            }


        }



        private void BARCODE_TextChanged(object sender, EventArgs e)
        {
            lblbarcode.Text = dgRates.Rows[0].Cells["rBarcode"].Value.ToString();
        }

       

        private void Generate_Barcode_Load(object sender, EventArgs e)
        {





            DataTable dt = new DataTable();
            dt=barcodse.GetSettings();
            if (dt.Rows.Count > 0)
            {
                
                IsMRP = Convert.ToBoolean(dt.Rows[0][1]);
                IsProductCode = Convert.ToBoolean(dt.Rows[0][3]);
                IsCompany = Convert.ToBoolean(dt.Rows[0][4]);
                companyname =Convert.ToString(dt.Rows[0][5]);
                WIDTH = Convert.ToInt32(dt.Rows[0][6]);
                HEIGHT = Convert.ToInt32(dt.Rows[0][7]);
                IsBarcode = Convert.ToBoolean(dt.Rows[0][8]);
                PriceType = dt.Rows[0][9].ToString();
                lblPriceType.Text = PriceType;

            }


            try
            {
                //cmd.CommandText = "SELECT CODE FROM INV_UNIT";
                //conn.Open();
                //cmd.Connection = conn;
                DataTable unitsTable = barcodse.selectUnits();
                //adapter.SelectCommand = cmd;
                //adapter.Fill(unitsTable);
                rUnit.DataSource = unitsTable;
                rUnit.DisplayMember = "CODE";
            }
            catch
            {
            }

            if (!hasBatch)
            {
                //cmd.CommandText = "SELECT CODE,DESC_ENG FROM GEN_PRICE_TYPE";
               
                //cmd.Connection = conn;
                SqlDataReader r = barcodse.selectPriceType();
                while (r.Read())
                {
                    dgRates.Columns.Add(r["CODE"].ToString(), r["DESC_ENG"].ToString());
                }
               // conn.Close();
                DbFunctions.CloseConnection();
            }

        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
          //  if (BARCODE.Text != "")
           // {
                if (PrintFormat.Text == "A4")
                {

                }
                else if (PrintFormat.Text == "Thermal Printer")
                {
                    ExportToPDFforThermalPrinter();
                }
                else
                {
                    MessageBox.Show("Select a printing format");
                }
         //   }
           // else
           // {
               // MessageBox.Show("Enter a Item to gnerate barcode");
///}
        }
//exporting to thermal pdf
        public void ExportToPDFforThermalPrinter()
        {

            try
            {
                iTextSharp.text.Document pdfdoc = new iTextSharp.text.Document();
                try
                {
                    DirectoryInfo dir1 = new DirectoryInfo(Application.StartupPath + "\\Barcode");
                    if (!Directory.Exists(Application.StartupPath + "\\Barcode"))
                    {
                        dir1.Create();
                    }
                    if (File.Exists(Application.StartupPath + "\\Barcode\\Barcode.pdf"))
                    {
                        File.Delete(Application.StartupPath + "\\Barcode\\Barcode.pdf");
                    }
                    iTextSharp.text.Rectangle pgSize = new iTextSharp.text.Rectangle(132, 56);
                    pdfdoc = new Document(pgSize, 1, 1, 1, 1);
                    PdfWriter writer = PdfWriter.GetInstance(pdfdoc, new FileStream(Application.StartupPath + "\\Barcode\\Barcode.pdf", FileMode.Create));
                    PdfPTable tbl = new PdfPTable(1);
                    //float[] fltParentWidth = new float[] { 104f,104f };
                    tbl.TotalWidth = 132;
                    tbl.LockedWidth = true;
                    //tbl.SetWidths(fltParentWidth);
                    tbl.DefaultCell.FixedHeight = 56;
                    tbl.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    tbl.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbl.DefaultCell.VerticalAlignment = Element.ALIGN_CENTER;
                    pdfdoc.Open();
                    int intotalCount = 0;

                    PdfContentByte pdfcb = writer.DirectContent;
                    Barcode128 code128 = new Barcode128();
                    //   BarcodeEAN eancode=new BarcodeEAN();
                    code128.Code = dgRates.Rows[0].Cells["rBarcode"].Value.ToString();
                    code128.Extended = false;
                    code128.CodeType = iTextSharp.text.pdf.Barcode.CODE128;
                    code128.AltText = dgRates.Rows[0].Cells["rBarcode"].Value.ToString();
                    if (IsBarcode==false)
                    {
                        code128.Font = null;

                    }


                    code128.BarHeight = Convert.ToUInt32(HEIGHT);
                    code128.Size = 7;
                    code128.Baseline = 7;
                    code128.TextAlignment = Element.ALIGN_CENTER;
                    iTextSharp.text.Image image128 = code128.CreateImageWithBarcode(pdfcb, null, null);
                    Phrase phrase = new Phrase();

                    if (IsCompany == true)
                    {
                        phrase.Add(new Chunk(ITEM_NAME.Text, new iTextSharp.text.Font(-1, 7, iTextSharp.text.Font.BOLD)));
                    }
                    phrase.Add(new Chunk(Environment.NewLine + Environment.NewLine, new iTextSharp.text.Font(-1, 4)));


                    PdfPCell cell = new PdfPCell(phrase);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    phrase.Add(new Chunk(image128, 0, 0));
                    phrase.Add(new Chunk(Environment.NewLine, new iTextSharp.text.Font(-1, 4)));
                    //if (chkbarcode.Checked == true)
                    //{
                    //    phrase.Add(new Chunk("3434223k", new iTextSharp.text.Font(-1, 8)));
                    //    phrase.Add(new Chunk(Environment.NewLine, new iTextSharp.text.Font(-1, 4)));
                    //}
                    if (IsMRP == true)
                    {
                        phrase.Add(new Chunk("SR: " + dgRates.Rows[0].Cells[3].Value.ToString(), new iTextSharp.text.Font(-1, 7)));
                        phrase.Add(new Chunk(Environment.NewLine, new iTextSharp.text.Font(-1)));
                    }
                    if (IsProductCode== true)
                    {
                        phrase.Add(new Chunk(dgRates.Rows[0].Cells["rBarcode"].Value.ToString() , new iTextSharp.text.Font(-1, 7)));
                        phrase.Add(new Chunk(Environment.NewLine, new iTextSharp.text.Font(-1, 4)));
                    }

                    // phrase.Add(new Chunk(Environment.NewLine + BARCODE.Text, new iTextSharp.text.Font(-1, 7)));
                    phrase.Add(new Chunk(Environment.NewLine + Environment.NewLine, new iTextSharp.text.Font(-1, 1.2f)));
                    tbl.AddCell(cell);


                    intotalCount++;








                    int reminder = intotalCount % 2;
                    if (reminder != 0)
                    {
                        for (int i = reminder; i < 2; ++i)
                        {
                            tbl.AddCell("");
                        }
                    }
                    if (tbl.Rows.Count != 0)
                    {
                        pdfdoc.Add(tbl);
                        pdfdoc.Close();
                        System.Diagnostics.Process.Start(Application.StartupPath + "\\Barcode\\Barcode.pdf");
                    }
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("The process cannot access the file") && ex.Message.Contains("Barcode.pdf' because it is being used by another process."))
                    {
                        MessageBox.Show("Close the PDF file and try again", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
                finally
                {
                    try
                    {
                        pdfdoc.Close();
                    }
                    catch
                    {
                    }
                }
            }
            catch
            {
            }
        }

       




        private void btngeneratecode_Click(object sender, EventArgs e)
        {
              if (BARCODE.Text != "" &&ITEM_CODE.Text!="")
              {
                 // GenereateBarcodeUsingItext();
            //   GenerateBacode();
            }
            else
            {
                MessageBox.Show("Enter a Item to gnerate barcode");
            }
        }






















        //public void GenereateBarcodeUsingItext()
        //{
        //    int height = 150;
        //    int width = 10;

        //    System.Drawing.Font printFont = new System.Drawing.Font("Courier New", 10);


        //    Barcode128 code128 = new Barcode128();
        //    code128.CodeType = Barcode.CODE128;
        //    code128.ChecksumText = true;
        //    code128.GenerateChecksum = true;
        //    code128.StartStopText = true;
        //    code128.Code = BARCODE.Text;
        //    System.Drawing.Bitmap bm = new System.Drawing.Bitmap(code128.CreateDrawingImage(System.Drawing.Color.Black, System.Drawing.Color.White),WIDTH,HEIGHT);
        //  //  bm.Height = HEIGHT; 
        //  //  bm.Width = WIDTH;
        //  //  picturebarcode.Image = bm;
        //  //  bm.Save(context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Gif);   
        //    RectangleF rectf = new RectangleF(70, 90, 90, 50);

        //    Graphics g = Graphics.FromImage(bm);

        //    g.SmoothingMode = SmoothingMode.AntiAlias;
        //    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
        //    g.PixelOffsetMode = PixelOffsetMode.HighQuality;
        //    g.DrawString("yourText", new System.Drawing.Font("Tahoma", 8), Brushes.Black, rectf);

        //    g.Flush();
     










        //    //Graphics graphics = Graphics.FromImage(bm);
        //    //if (IsMRP == true)
        //    //{
        //    //    graphics.DrawString("MRP:250.50", printFont, Brushes.Black, width, height);
        //    //    height = height + 9;
        //    //}

        //    //if (IsProductCode == true)
        //    //{
        //    //    graphics.DrawString("Parx 4ed", printFont, Brushes.Black, width, height);
        //    //}

        //    //if (IsCompany == true)
        //    //{
        //    //    graphics.DrawString(companyname, printFont, Brushes.Black, width, 5);
        //    //}
        //    //picturebarcode.Image = bm;





        //}




     
        private void GenerateBacode()
        {
            int height = 97;
            int width = 10;

            System.Drawing.Font printFont = new System.Drawing.Font("Courier New", 10);

            Linear barcode = new Linear();
            barcode.Type = BarcodeType.CODE128;
            barcode.Data = dgRates.Rows[0].Cells["rBarcode"].Value.ToString();
            barcode.AddCheckSum = true;
            barcode.BarcodeHeight = Convert.ToInt32(HEIGHT);
            barcode.BarcodeWidth = Convert.ToInt32(WIDTH);




            System.Drawing.Image image = barcode.drawBarcode();


            Graphics graphics = Graphics.FromImage(image);
            if (IsMRP == true)
            {
                graphics.DrawString("MRP:250.50", printFont, Brushes.Black, width, height);
                height = height + 9;
            }

            if (IsProductCode == true)
            {
                graphics.DrawString("Parx 4ed", printFont, Brushes.Black, width, height);
            }

            if (IsCompany== true)
            {
                graphics.DrawString(CompanyName, printFont, Brushes.Black, width, 5);
            }
        //    picturebarcode.Image = image;


        }

        private void dgRates_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string UNITCODE = "";
            try
            {
                UNITCODE = dgRates.CurrentRow.Cells[2].Value.ToString();
                PRICE.Text = dgRates.CurrentRow.Cells[PriceType].Value.ToString();
            }
            catch
            {
            }


            BARCODE.Text = UNITCODE;
                lblbarcode.Text = BARCODE.Text;
          
            
        }

        string[] PrnContent=new string[1000];

        public void WritingContentstoFile()
        {
            try
            {
                PrnContent[0] = "<xpml><page quantity='0' pitch='20.0 mm'></xpml>SIZE 92 mm, 20 mm DIRECTION 0,0 REFERENCE 0,0 OFFSET 0 mm SET PEEL OFF SET CUTTER OFF <xpml></page></xpml><xpml><page quantity='2' pitch='20.0 mm'></xpml>SET TEAR ON CLS CODEPAGE 1252";
              //  StreamReader sr = new StreamReader();
                TextWriter sw = new StreamWriter(@"G:\\file11.prn"); //true for append
                int i = 0;

                for (int row = 0; row < dgRates.Rows.Count-1; row++)
                {
                    int colCount = dgRates.Rows[row].Cells.Count;
                    sw.WriteLine(PrnContent[0]);
                    for (int col = 0; col < colCount; col++)
                    {
                        sw.WriteLine("TEXT 711,147,\"ROMAN.TTF\", 180, 1, 8, \"" + dgRates.Rows[row].Cells[col].Value.ToString() + "\"");
                        //BARCODE 702,93,"128M",34,0,180,2,4,"!10512345678"
                        //TEXT 667,53,"ROMAN.TTF",180,1,8,"12345678"
                        //TEXT 703,25,"ROMAN.TTF",180,1,7,"MRP: RS. "
                        //TEXT 617,24,"ROMAN.TTF",180,1,7,"00 "
                    //    sw.WriteLine(dgRates.Rows[row].Cells[col].Value.ToString());

                    }
                    sw.Close();
                    // record seperator could be written here.
                }
                //while (i < 1000)
                //{
                
                //    sw.WriteLine(sr.ReadLine());
                //    i++;
                //}
            }
            catch(Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }
    

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            try
            {


                WritingContentstoFile();
               

                //WritingContentstoFile();
                //System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo("cmd.exe", @"/c copy /b print.prn \\ains\barcode");
                //System.Diagnostics.Process p = new System.Diagnostics.Process();
                //p.StartInfo = info;
                //p.Start();









                //Process process1 = new Process();
                //process1.StartInfo.FileName = "cmd.exe";
                //process1.StartInfo.Arguments = "copy output.prn /b \\ains\barcode";
                //process1.Start();
                //Console.Write("hi");

                //string fijiCmdText = "/b copy output.prn /b \\ains\barcode";
                //System.Diagnostics.Process.Start("cmd.exe", fijiCmdText);




                //     System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo("cmd.exe", @"copy G:\output.prn /b \\ains\barcode");

                
                
                ////  p.WaitForExit();

                //ProcessStartInfo proc = new ProcessStartInfo();
              
                //proc.FileName = @"C:\windows\system32\cmd.exe";
                //proc.Arguments = "Help";
                //Process.Start(proc);


                //Process proc = new Process();
                //proc.StartInfo.UseShellExecute = true;
                //proc.StartInfo.FileName = "CMD";
                //proc.StartInfo.Arguments = "copy G:\\output.prn /b \\ains\barcode";
                //proc.Start();
                //proc.WaitForExit();

            }
            catch(Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

       
   

        //public void ExportToPDF()
        //{
        //    iTextSharp.text.Document pdfdoc = new iTextSharp.text.Document();
        //    try
        //    {
        //        DirectoryInfo dir1 = new DirectoryInfo(Application.StartupPath + "\\Barcode");
        //        if (!Directory.Exists(Application.StartupPath + "\\Barcode"))
        //        {
        //            dir1.Create();
        //        }
        //        if (File.Exists(Application.StartupPath + "\\Barcode\\Barcode.pdf"))
        //        {
        //            File.Delete(Application.StartupPath + "\\Barcode\\Barcode.pdf");
        //        }
        //        pdfdoc = new Document(PageSize.A4, 12, 1, 20, 20);
        //        PdfWriter writer = PdfWriter.GetInstance(pdfdoc, new FileStream(Application.StartupPath + "\\Barcode\\Barcode.pdf", FileMode.Create));
        //        PdfPTable tbl = new PdfPTable(5);
        //        tbl.WidthPercentage = 100;
        //        tbl.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        //        tbl.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
        //        tbl.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //        pdfdoc.Open();
        //        int intotalCount = 0;
        //        BarcodeSettingsInfo Info = new BarcodeSettingsInfo();
        //        SettingsSP Sp = new SettingsSP();
        //        Info = spBarcodeSettings.BarcodeSettingsViewForBarCodePrinting();
        //        for (int i = 0; i < dgvBarcodePrinting.Rows.Count; i++)
        //        {
        //            if (dgvBarcodePrinting.Rows[i].Cells["dgvProductCode"].Value != null && dgvBarcodePrinting.Rows[i].Cells["dgvProductCode"].Value.ToString() != "")
        //            {
        //                int inCopies = 0;
        //                if (dgvBarcodePrinting.Rows[i].Cells["dgvCopies"].Value != null)
        //                {
        //                    int.TryParse(dgvBarcodePrinting.Rows[i].Cells["dgvCopies"].Value.ToString(), out inCopies); // number of copies of arcode to be printed
        //                }
        //                for (int j = 0; j < inCopies; j++)
        //                {
        //                    string strProductCode = string.Empty;
        //                    string strCode = string.Empty;
        //                    string strCompanyName = string.Empty;
        //                    if (Info.ShowProductCode)
        //                    {
        //                        strCode = dgvBarcodePrinting.Rows[i].Cells["dgvProductCode"].Value.ToString();

        //                        if (Info.ShowProductCode)
        //                            strProductCode = strCode;
        //                    }
        //                    else
        //                    {
        //                        strProductCode = dgvBarcodePrinting.Rows[i].Cells["dgvproductName"].Value.ToString();
        //                    }
        //                    if (Info.ShowCompanyName)
        //                        strCompanyName = Info.CompanyName;
        //                    string strMRP = string.Empty;
        //                    if (Info.ShowMRP)
        //                    {
        //                        strMRP = new CurrencySP().CurrencyView(PublicVariables._decCurrencyId).CurrencySymbol + ": " + dgvBarcodePrinting.Rows[i].Cells["dgvMRP"].Value.ToString();
        //                    }
        //                    string strSecretPurchaseRateCode = string.Empty;
        //                    if (Info.ShowPurchaseRate)
        //                    {
        //                        string strPurchaseRate = dgvBarcodePrinting.Rows[i].Cells["dgvPurchaseRate"].Value.ToString();
        //                        if (strPurchaseRate.Contains("."))
        //                        {
        //                            strPurchaseRate = strPurchaseRate.TrimEnd('0');
        //                            if (strPurchaseRate[strPurchaseRate.Length - 1] == '.')
        //                                strPurchaseRate = strPurchaseRate.Replace(".", "");
        //                        }
        //                        for (int k = 0; k < strPurchaseRate.Length; k++)
        //                        {
        //                            switch (strPurchaseRate[k])
        //                            {
        //                                case '0':
        //                                    strSecretPurchaseRateCode += Info.Zero;
        //                                    break;
        //                                case '1':
        //                                    strSecretPurchaseRateCode += Info.One;
        //                                    break;
        //                                case '2':
        //                                    strSecretPurchaseRateCode += Info.Two;
        //                                    break;
        //                                case '3':
        //                                    strSecretPurchaseRateCode += Info.Three;
        //                                    break;
        //                                case '4':
        //                                    strSecretPurchaseRateCode += Info.Four;
        //                                    break;
        //                                case '5':
        //                                    strSecretPurchaseRateCode += Info.Five;
        //                                    break;
        //                                case '6':
        //                                    strSecretPurchaseRateCode += Info.Six;
        //                                    break;
        //                                case '7':
        //                                    strSecretPurchaseRateCode += Info.Seven;
        //                                    break;
        //                                case '8':
        //                                    strSecretPurchaseRateCode += Info.Eight;
        //                                    break;
        //                                case '9':
        //                                    strSecretPurchaseRateCode += Info.Nine;
        //                                    break;
        //                                case '.':
        //                                    strSecretPurchaseRateCode += Info.Point;
        //                                    break;
        //                            }
        //                        }
        //                    }
        //                    PdfContentByte pdfcb = writer.DirectContent;
        //                    Barcode128 code128 = new Barcode128();
        //                    code128.Code = strCode;
        //                    code128.Extended = false;
        //                    code128.CodeType = iTextSharp.text.pdf.Barcode.CODE128;
        //                    code128.AltText = strProductCode;
        //                    code128.BarHeight = 13;
        //                    code128.Size = 6;
        //                    code128.Baseline = 8;
        //                    code128.TextAlignment = Element.ALIGN_CENTER;
        //                    iTextSharp.text.Image image128 = code128.CreateImageWithBarcode(pdfcb, null, null);
        //                    Phrase phrase = new Phrase();
        //                    phrase.Font.Size = 8;
        //                    phrase.Add(new Chunk(strCompanyName + Environment.NewLine + Environment.NewLine));
        //                    PdfPCell cell = new PdfPCell(phrase);
        //                    cell.FixedHeight = 61.69f;
        //                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
        //                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //                    cell.Border = iTextSharp.text.Rectangle.NO_BORDER;

        //                    phrase.Add(new Chunk(image128, 0, 0));
        //                    phrase.Add(new Chunk(Environment.NewLine + strMRP));
        //                    phrase.Add(new Chunk(Environment.NewLine + strSecretPurchaseRateCode));
        //                    tbl.AddCell(cell);
        //                    intotalCount++;
        //                }
        //            }
        //        }
        //        int reminder = intotalCount % 5;
        //        if (reminder != 0)
        //        {
        //            for (int i = reminder; i < 5; ++i)
        //            {
        //                tbl.AddCell("");
        //            }
        //        }
        //        if (tbl.Rows.Count != 0)
        //        {
        //            pdfdoc.Add(tbl);
        //            pdfdoc.Close();
        //            System.Diagnostics.Process.Start(Application.StartupPath + "\\Barcode\\Barcode.pdf");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex.Message.Contains("The process cannot access the file") && ex.Message.Contains("Barcode.pdf' because it is being used by another process."))
        //        {
        //            MessageBox.Show("Close the PDF file and try again", "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }
        //        else
        //        {
        //            MessageBox.Show("BCP4:" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }
        //    }
        //    finally
        //    {
        //        try
        //        {
        //            pdfdoc.Close();
        //        }
        //        catch
        //        {
        //        }
        //    }
        //}
    }
}
