using OnBarcode.Barcode;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using iTextSharp.text.pdf;
using System.IO;
using iTextSharp.text;


namespace Sys_Sols_Inventory
{
    
    public partial class Barcode_Settings : Form
    {
        Boolean update;
        Class.BarcodeSettings BarSettings = new Class.BarcodeSettings();
        public Barcode_Settings()
        {
            InitializeComponent();
         
           
        }

      
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (update == false)
            {
                InsertBarcode();
               
            }
            else
            {
                UpdateBarcodeSettings();
                
            }
            GenerateBarcode2();
            BarSettings.SaveManual(Convert.ToBoolean(isManual.Checked));
            GetBarcodeSettings();
    
        }

        private void UpdateBarcodeSettings()
        {
            try
            {
                try
                {
                    if (ckmrp.Checked == true)
                    {
                        BarSettings.IsMRP = 1;
                    }
                    else
                    {
                        BarSettings.IsMRP = 0;
                    }

                    if (ckcomp.Checked == true)
                    {
                        BarSettings.IsCompanyName = 1;
                        BarSettings.CompanyName = txtcompanyname.Text;

                    }
                    else
                    {
                        BarSettings.IsCompanyName = 0;
                        BarSettings.CompanyName = txtcompanyname.Text;
                    }
                    if (ckprcod.Checked == true)
                    {
                        BarSettings.IsProductCode = 1;
                    }
                    else
                    {
                        BarSettings.IsProductCode = 0;
                    }


                    if (chkbarcode.Checked == true)
                    {
                        BarSettings.IsBarcodeValue = 1;
                    }
                    else
                    {
                        BarSettings.IsBarcodeValue = 0;
                    }
                    BarSettings.isBoarder = chkboarder.Checked == true ? true : false;
                    BarSettings.font = txtfont.Text != "" ?(float)Convert.ToDouble(txtfont.Text): 6;
                    BarSettings.topMargine = txtmargin.Text != "" ? (float)Convert.ToDouble(txtmargin.Text) : 18;
                    BarSettings.cellheight = txtcellhegt.Text != "" ? (float)Convert.ToDouble(txtcellhegt.Text) : 62;
                    BarSettings.Height = Convert.ToInt32(txtheight.Text);
                    BarSettings.Width = Convert.ToInt32(txtwidth.Text);
                    BarSettings.PriceType = cmbPriceType.SelectedValue.ToString();
                    BarSettings.Length = txtLength.Text != "" ? Convert.ToInt32(txtLength.Text) : 23;
                    BarSettings.UPdateBarcodeSettings();
                    update = true;
                }
                catch
                {
                }
            }
            catch
            {
            }
        }
        void ResetSettings()
        {
            try
            {
                try
                {
                    if (ckmrp.Checked == true)
                    {
                        BarSettings.IsMRP = 1;
                    }
                    else
                    {
                        BarSettings.IsMRP = 0;
                    }

                    if (ckcomp.Checked == true)
                    {
                        BarSettings.IsCompanyName = 1;
                        BarSettings.CompanyName = txtcompanyname.Text;

                    }
                    else
                    {
                        BarSettings.IsCompanyName = 0;
                        BarSettings.CompanyName = txtcompanyname.Text;
                    }
                    if (ckprcod.Checked == true)
                    {
                        BarSettings.IsProductCode = 1;
                    }
                    else
                    {
                        BarSettings.IsProductCode = 0;
                    }


                    if (chkbarcode.Checked == true)
                    {
                        BarSettings.IsBarcodeValue = 1;
                    }
                    else
                    {
                        BarSettings.IsBarcodeValue = 0;
                    }
                    BarSettings.isBoarder = chkboarder.Checked = true ? true : false;
                    BarSettings.font = 6;
                    BarSettings.topMargine = 18;
                    BarSettings.cellheight =62;
                    BarSettings.Height = Convert.ToInt32(txtheight.Text);
                    BarSettings.Width = Convert.ToInt32(txtwidth.Text);
                    BarSettings.PriceType = cmbPriceType.SelectedValue.ToString();
                    BarSettings.Length = 23;
                    BarSettings.UPdateBarcodeSettings();
                    update = true;
                }
                catch
                {
                }
            }
            catch
            {
            }
        }
        private void InsertBarcode()
        {
            try
            {
                if (ckmrp.Checked == true)
                {
                    BarSettings.IsMRP = 1;
                }
                else
                {
                    BarSettings.IsMRP = 0;
                }

                if (ckcomp.Checked == true)
                {
                    BarSettings.IsCompanyName = 1;
                    BarSettings.CompanyName = txtcompanyname.Text;

                }
                else
                {
                    BarSettings.IsCompanyName = 0;
                    BarSettings.CompanyName = txtcompanyname.Text;
                }
                if (ckprcod.Checked == true)
                {
                    BarSettings.IsProductCode = 1;
                }
                else
                {
                    BarSettings.IsProductCode = 0;
                }

                if (chkbarcode.Checked == true)
                {
                    BarSettings.IsBarcodeValue = 1;
                }
                else
                {
                    BarSettings.IsBarcodeValue = 0;
                }

                BarSettings.Height =Convert.ToInt32(txtheight.Text);
                BarSettings.Width =Convert.ToInt32(txtwidth.Text);
                BarSettings.InsertBarcodeSettings();
                if (cmbPriceType.Enabled == true)
                {
                    BarSettings.PriceType = cmbPriceType.SelectedValue.ToString();
                }
                else
                    BarSettings.PriceType = "";
                update = true;
            }
            catch
            {
            }
        }

       
            
        

        public void GenerateBarcode2()
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
                    pdfdoc = new Document(pgSize, 3, 3, 3, 3);
                    PdfWriter writer = PdfWriter.GetInstance(pdfdoc, new FileStream(Application.StartupPath + "\\Barcode\\Barcode.pdf", FileMode.Create));
                    PdfPTable tbl = new PdfPTable(1);
               //     float[] fltParentWidth = new float[] { 56f};
                    tbl.TotalWidth = 132;
                    tbl.LockedWidth = true;
                //    tbl.SetWidths(fltParentWidth);
                    tbl.DefaultCell.FixedHeight = 56;
                    tbl.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    tbl.DefaultCell.HorizontalAlignment = Element.ALIGN_MIDDLE;
                    tbl.DefaultCell.VerticalAlignment = Element.ALIGN_CENTER;
                    pdfdoc.Open();
                    int intotalCount = 0;

                    PdfContentByte pdfcb = writer.DirectContent;
                    Barcode128 code128 = new Barcode128();
                 //   BarcodeEAN eancode=new BarcodeEAN();
                    code128.Code = "3434223k";
                    code128.Extended = false;
                    code128.CodeType = iTextSharp.text.pdf.Barcode.CODE128;
                    code128.AltText = "3434223k";
                    if (chkbarcode.Checked == false)
                    {
                        code128.Font = null;
                        
                    }
                  

                    code128.BarHeight =Convert.ToUInt32(txtheight.Text);
                    code128.Size = 7;
                    code128.Baseline = 7;
                    code128.TextAlignment = Element.ALIGN_CENTER;
                    iTextSharp.text.Image image128 = code128.CreateImageWithBarcode(pdfcb, null, null);
                    Phrase phrase = new Phrase();
                   
                    if (ckcomp.Checked == true)
                    {
                        phrase.Add(new Chunk(txtcompanyname.Text, new iTextSharp.text.Font(-1, 7, iTextSharp.text.Font.BOLD)));
                    }
                    phrase.Add(new Chunk(Environment.NewLine + Environment.NewLine, new iTextSharp.text.Font(-1,4)));

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
                    if (ckmrp.Checked == true)
                    {
                        phrase.Add(new Chunk(cmbPriceType.SelectedValue+" : 250.00", new iTextSharp.text.Font(-1, 7)));
                        phrase.Add(new Chunk(Environment.NewLine, new iTextSharp.text.Font(-1)));
                    }
                    if (ckprcod.Checked == true)
                    {
                        phrase.Add(new Chunk("001", new iTextSharp.text.Font(-1, 7)));
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
                    pdfdoc.Close();
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

        //private void GenerateBacode()
        //{
        //    int height = 97;
        //    int width = 10;
        //    Font printFont = new Font("Courier New", 10);

        //    Linear barcode = new Linear();
        //    barcode.Type = BarcodeType.CODE128;
        //    barcode.Data ="145625647";
        //    barcode.AddCheckSum = true;
        //    barcode.BarcodeHeight = Convert.ToInt32(txtheight.Text);
        //    barcode.BarcodeWidth =Convert.ToInt32(txtwidth.Text);


          

        //    Image image = barcode.drawBarcode();
       
         
        //    Graphics graphics = Graphics.FromImage(image);
        //    if (ckmrp.Checked == true)
        //    {
        //        graphics.DrawString("MRP:250.50", printFont, Brushes.Black, width, height);
        //        height = height +9;
        //    }

        //    if(ckprcod.Checked==true)
        //    {
        //        graphics.DrawString("Parx 4ed", printFont, Brushes.Black, width, height);
        //    }

        //    if (ckcomp.Checked == true)
        //    {
        //        graphics.DrawString(txtcompanyname.Text, printFont, Brushes.Black, width, 5);
        //    }
        //    picturebarcode.Image = image;

          
        //}

        private void txtheight_KeyPress(object sender, KeyPressEventArgs e)
        {
             if (e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == '.') //The  character represents a backspace
             {
        e.Handled = false; //Do not reject the input
                }
                 else
    {
        e.Handled = true; //Reject the input
             }
        }

        private void txtwidth_KeyPress(object sender, KeyPressEventArgs e)
        {
             if (e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == '.') //The  character represents a backspace
    {
        e.Handled = false; //Do not reject the input
    }
    else
    {
        e.Handled = true; //Reject the input
    }
        }

        private void ckcomp_CheckedChanged(object sender, EventArgs e)
        {
            if (ckcomp.Checked == true)
            {
                txtcompanyname.Enabled = true;
                txtcompanyname.Text = "";
            }
            else
            {
                txtcompanyname.Enabled = false;
            }
        }
        public void GetBarcodeSettings()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = BarSettings.GetSettings();
                if (dt.Rows.Count > 0)
                {
                    if (Convert.ToBoolean(dt.Rows[0][1]) == true)
                    {
                        ckmrp.Checked = true;
                    }
                    if (Convert.ToBoolean(dt.Rows[0][3]) == true)
                    {
                        ckprcod.Checked = true;
                    }
                    if (Convert.ToBoolean(dt.Rows[0][4]) == true)
                    {
                        ckcomp.Checked = true;
                    }
                    txtcompanyname.Text = dt.Rows[0][5].ToString();
                    txtheight.Text = dt.Rows[0][7].ToString();
                    txtwidth.Text = dt.Rows[0][6].ToString();
                    if (Convert.ToBoolean(dt.Rows[0][8]) == true)
                    {
                        chkbarcode.Checked = true;
                    }
                    if (dt.Rows[0][9].ToString()!="")
                    {
                        string str = dt.Rows[0][9].ToString();
                        cmbPriceType.SelectedValue =str;
                    }
                    if (Convert.ToBoolean(dt.Rows[0]["IsBoarder"]) == true)
                    {
                       chkboarder.Checked = true;
                    }
                    txtfont.Text = dt.Rows[0]["fontSize"].ToString();
                    txtcellhegt.Text = dt.Rows[0]["cellheight"].ToString();
                    txtmargin.Text = dt.Rows[0]["topmrgine"].ToString();
                    txtLength.Text = dt.Rows[0]["ItemLength"].ToString();
                    update = true;
                }
                else
                {
                    update = false;
                }
            }
            catch
            {
                update = true;
            }
        }
        public void pricetype()
        {
            DataTable dt = new DataTable();
            dt = BarSettings.GetPricetype();
           
           
            cmbPriceType.DataSource = dt;
            cmbPriceType.DisplayMember = "value";
            cmbPriceType.ValueMember = "key";
        }

        private void Barcode_Settings_Load(object sender, EventArgs e)
        {
            pricetype();
            GetBarcodeSettings();
            isManual.Checked = BarSettings.getManualSettings();
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            GenerateBarcode2();
        }

        private void ckmrp_CheckedChanged(object sender, EventArgs e)
        {
            if (ckmrp.Checked == true)
            {
                cmbPriceType.Enabled = true;
            }
            else {
                cmbPriceType.Enabled = false;
            }
        }

        private void linkRemoveRecord_LinkClicked(object sender, EventArgs e)
        {
            ResetSettings();
            GetBarcodeSettings();
        }
    }
}
