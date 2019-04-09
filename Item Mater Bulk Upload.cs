using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Sys_Sols_Inventory.Model;
namespace Sys_Sols_Inventory
{
    public partial class Item_Mater_Bulk_Upload : Form
    {
        Login lg = (Login)Application.OpenForms["Login"];
        private string Excel03ConString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";
        private string Excel07ConString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";
        private string ID = "";
        ItemDirectoryDB ItemDirectoryDB = new ItemDirectoryDB();
        StockDB StockDB=new StockDB();
        RateChangeDB RateChangeDB = new RateChangeDB();
        InvItemDirectoryUnits InvItemDirectoryUnits = new InvItemDirectoryUnits();
        InvStkTrxHdrDB InvStkTrxHdrDB = new InvStkTrxHdrDB();
       
        #region private properties declaration
        //private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        //private SqlCommand cmd = new SqlCommand();
        //private SqlDataAdapter adapter = new SqlDataAdapter();
        private DataTable table = new DataTable();
        private DataTable fieldTable = new DataTable();
        private BindingSource source = new BindingSource();
        private BindingSource source1 = new BindingSource();
        Initial mdi = (Initial)Application.OpenForms["Initial"];
        Class.BarcodeSettings barcodes = new Class.BarcodeSettings();
        string companyname = "", PriceType = "";
        bool IsMRP;
        bool IsProductCode;

        bool IsCompany, IsBarcodeValue;
        Int32 WIDTH, HEIGHT;      
                
        #endregion
        public Item_Mater_Bulk_Upload()
        {
            InitializeComponent();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
         
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            if (openFileDialog1.FileName!="")
            {
                lblFilename.Text = openFileDialog1.FileName;
            }
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.FileName != "")
            {
                string filePath = openFileDialog1.FileName;
            string extension = Path.GetExtension(filePath);
            string header = "YES";
            string conStr, sheetName;

            conStr = string.Empty;
            switch (extension)
            {

                case ".xls": //Excel 97-03
                    conStr = string.Format(Excel03ConString, filePath, header);
                    break;

                case ".xlsx": //Excel 07
                    conStr = string.Format(Excel07ConString, filePath, header);
                    break;
            }

            //Get the name of the First Sheet.
            using (OleDbConnection con = new OleDbConnection(conStr))
            {
                using (OleDbCommand cmd = new OleDbCommand())
                {
                    cmd.Connection = con;
                    con.Open();
                    DataTable dtExcelSchema = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                    con.Close();
                }
            }

            //Read Data from the First Sheet.
            using (OleDbConnection con = new OleDbConnection(conStr))
            {
                using (OleDbCommand cmd = new OleDbCommand())
                {
                    using (OleDbDataAdapter oda = new OleDbDataAdapter())
                    {
                        DataTable dt = new DataTable();
                        cmd.CommandText = "SELECT * From [" + sheetName + "]";
                        cmd.Connection = con;
                        con.Open();
                        oda.SelectCommand = cmd;
                        oda.Fill(dt);
                        con.Close();

                        //Populate DataGridView.
                        dataGridView1.DataSource = dt;
                    }
                }
            }
        }
            }
        

        public bool Valid()
        {
            bool succvale=true;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells["Code"].Value == "" || dataGridView1.Rows[i].Cells["Code"].Value == null)
                {
                    MessageBox.Show("Please fill Item code in row " + i + 1, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    succvale = false;
                    return succvale;
                }

                else if (dataGridView1.Rows[i].Cells["Name"].Value == "" || dataGridView1.Rows[i].Cells["Name"].Value == null)
                {
                    MessageBox.Show("Please fill Item code in row " + i + 1, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    succvale = false;
                    return succvale;
                }
                else if (dataGridView1.Rows[i].Cells["UOM"].Value == "" || dataGridView1.Rows[i].Cells["UOM"].Value == null)
                {
                    MessageBox.Show("Please fill Item code in row " + i + 1, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    succvale = false;
                    return succvale;
                }

                else if (dataGridView1.Rows[i].Cells["Category"].Value.ToString().Length>3)
                {
                    MessageBox.Show("Category Should be In 3 Character " + i + 1, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    succvale = false;
                    return succvale;
                }
                else if (dataGridView1.Rows[i].Cells["Type"].Value.ToString().Length > 3)
                {
                    MessageBox.Show("Type Should be In 3 Character " + i + 1, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    succvale = false;
                    return succvale;
                }
                else if (dataGridView1.Rows[i].Cells["Group"].Value.ToString().Length > 3)
                {
                    MessageBox.Show("Group Should be In 3 Character " + i + 1, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    succvale = false;
                    return succvale;
                }

                else if (dataGridView1.Rows[i].Cells["Brand"].Value.ToString().Length > 3)
                {
                    MessageBox.Show("Brand Should be In 3 Character " + i + 1, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    succvale = false;
                    return succvale;
                }
                else if (dataGridView1.Rows[i].Cells["UOM"].Value.ToString().Length>3)
                {
                    MessageBox.Show("Unit Should be In 3 Character" + i + 1, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    succvale = false;
                    return succvale;
                }
                else
                {
                    return true;
                }
            }
        
            return succvale;
        }
        StockEntry se = new StockEntry();
        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            if (Valid())
            {
                Int16 insert = 0;
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    try
                    {
                        decimal Purchase_price;
                        string code = Convert.ToString(dataGridView1.Rows[i].Cells["Code"].Value);
                        string uom = Convert.ToString(dataGridView1.Rows[i].Cells["UOM"].Value);

                        if (dataGridView1.Rows[i].Cells["Purchase price"].Value.ToString() == "")
                        {
                            Purchase_price = 0;
                        }
                        else
                        {
                            Purchase_price = Convert.ToDecimal(dataGridView1.Rows[i].Cells["Purchase price"].Value);
                        }

                        string qty = Convert.ToString(dataGridView1.Rows[i].Cells["Quantity"].Value);
                        if (General.ItemExists(code, ID, "INV_ITEM_DIRECTORY"))
                        {
                            MessageBox.Show("Item with the same code:" + code + "  already exists!");
                            return;
                        }
                        // inserting to item directory

                        //try
                        //{
                            decimal CPR, SPR;
                            try
                            {
                                SPR = Convert.ToDecimal(dataGridView1.Rows[i].Cells["Sales price"].Value);
                            }
                            catch
                            {
                                SPR = 0;
                            }
                            try
                            {
                                CPR = Convert.ToDecimal(dataGridView1.Rows[i].Cells["Cost price"].Value);
                            }
                            catch
                            {
                                CPR = 0;
                            }
                            //if (conn.State == ConnectionState.Open)
                            //{
                            //}
                            //else
                            //{

                            //    conn.Open();
                            //}
                            //cmd.Connection = conn;
                            //string fields = "CODE,DESC_ENG,DESC_ARB,TYPE,[GROUP],CATEGORY,TRADEMARK,COST_PRICE,SALE_PRICE,IN_ACTIVE,TaxId,HSN";
                            //string values = "'" + dataGridView1.Rows[i].Cells["Code"].Value + "','" + dataGridView1.Rows[i].Cells["Name"].Value + "','" + dataGridView1.Rows[i].Cells["Arabic Name"].Value + "','" + dataGridView1.Rows[i].Cells["Type"].Value + "','" + dataGridView1.Rows[i].Cells["Group"].Value + "','" + dataGridView1.Rows[i].Cells["Category"].Value + "','" + dataGridView1.Rows[i].Cells["Brand"].Value + "','" + Convert.ToDecimal(CPR) + "','" + Convert.ToDecimal(SPR) + "','Y','" + Convert.ToInt32(dataGridView1.Rows[i].Cells["TaxId"].Value) + "','" + dataGridView1.Rows[i].Cells["HSN Code"].Value + "'";
                            ItemDirectoryDB.Code = dataGridView1.Rows[i].Cells["Code"].Value.ToString();
                            ItemDirectoryDB.Desc_Eng = dataGridView1.Rows[i].Cells["Name"].Value.ToString();
                            ItemDirectoryDB.Desc_Arb = dataGridView1.Rows[i].Cells["Arabic Name"].Value.ToString();
                            ItemDirectoryDB.Type = dataGridView1.Rows[i].Cells["Type"].Value.ToString();
                            ItemDirectoryDB.Group = dataGridView1.Rows[i].Cells["Group"].Value.ToString();
                            ItemDirectoryDB.Category = dataGridView1.Rows[i].Cells["Category"].Value.ToString();
                            ItemDirectoryDB.Trademark = dataGridView1.Rows[i].Cells["Brand"].Value.ToString();
                            ItemDirectoryDB.CostPrice = Convert.ToDecimal(CPR);
                            ItemDirectoryDB.SalePrice = Convert.ToDecimal(SPR);
                            ItemDirectoryDB.InActive="Y";
                            ItemDirectoryDB.TaxId = Convert.ToInt32(dataGridView1.Rows[i].Cells["TaxId"].Value);
                            ItemDirectoryDB.Hsn = dataGridView1.Rows[i].Cells["HSN Code"].Value.ToString();
                            ItemDirectoryDB.InsertBulk();

                            //cmd.CommandText = "INSERT INTO INV_ITEM_DIRECTORY(" + fields + ") VALUES(" + values + ")";
                            //cmd.ExecuteNonQuery();

                            insert++;

                            //  MessageBox.Show("Item Added!");
                            //conn.Close();
                        //}
                        //catch (Exception e2)
                        //{
                        //    MessageBox.Show(e2.Message);
                        //}

                        //inserting units



                        //inserting to item price


                        try
                        {

                            decimal PUR, RTL, MRP, PMS, WHL;
                            string b;
                            try
                            {

                                PUR = Convert.ToDecimal(dataGridView1.Rows[i].Cells["Purchase price"].Value);
                            }
                            catch
                            {
                                PUR = 0;
                            }
                            try
                            {

                                RTL = Convert.ToDecimal(dataGridView1.Rows[i].Cells["Retail price"].Value);
                            }
                            catch
                            {
                                RTL = 0;
                            }

                            try
                            {

                                MRP = Convert.ToDecimal(dataGridView1.Rows[i].Cells["Maximum price"].Value);
                            }
                            catch
                            {
                                MRP = 0;
                            }
                            try
                            {

                                PMS = Convert.ToDecimal(dataGridView1.Rows[i].Cells["Promotional price"].Value);
                            }
                            catch
                            {
                                PMS = 0;
                            }
                            try
                            {

                                WHL = Convert.ToDecimal(dataGridView1.Rows[i].Cells["Whole Sale"].Value);
                            }
                            catch
                            {
                                WHL = 0;
                            }
                         //   string item_id = Convert.ToString(c["cCode"].Value);
                            double qty1=0;
                            if (dataGridView1.Rows[i].Cells["Quantity"].Value != null && dataGridView1.Rows[i].Cells["Quantity"].Value.ToString()!="")
                            {
                                qty1 = Convert.ToDouble(dataGridView1.Rows[i].Cells["Quantity"].Value);
                            }
                            int next_batch_inc = se.max_batch_id(code);
                            string next_batch = "";
                            if (chkBarcode.Checked == false)
                            {
                                next_batch = code + "B" + next_batch_inc;
                            }
                            else
                            {
                                next_batch = code;
                            }

                            //cmd.CommandText = "INSERT INTO tblStock(Item_id, qty, Cost_price, supplier_id, MRP,batch_id,batch_increment) values(@item_id, @qty, @cost_price, @supplier_id, @mrp,@batch_id,@batch_increment)";
                            //cmd.Parameters.Clear();
                            //cmd.Parameters.AddWithValue("@item_id",code);
                            //cmd.Parameters.AddWithValue("@qty", (qty1.ToString().Equals("") ? 0: qty1));
                            //cmd.Parameters.AddWithValue("@cost_price", PUR);
                            //cmd.Parameters.AddWithValue("@mrp", MRP);  
                            //cmd.Parameters.AddWithValue("@supplier_id", sup_id);
                            //cmd.Parameters.AddWithValue("@batch_id", next_batch);
                            //cmd.Parameters.AddWithValue("@batch_increment", next_batch_inc);
                            //if (conn.State == ConnectionState.Open)
                            //{
                            //    conn.Close();
                            //}
                            //conn.Open();
                            //cmd.ExecuteNonQuery();
                            //conn.Close();
                            //cmd.Parameters.Clear();
                            string sup_id;
                            sup_id = ""; 
                            StockDB.ItemId = code;
                            StockDB.Qty =Convert.ToDecimal((qty1.ToString().Equals("") ? 0 : qty1));
                            StockDB.CostPrice = PUR;
                            StockDB.SuppId = sup_id;
                            StockDB.Mrp = MRP.ToString();
                            StockDB.BatchId = next_batch;
                            StockDB.BatchIncrement = next_batch_inc;
                            StockDB.Insert();

                            
                            string query = "INSERT INTO INV_ITEM_PRICE(ITEM_CODE,SAL_TYPE,PRICE,UNIT_CODE,BATCH_ID,BRANCH) ";
                            query += "SELECT '" + dataGridView1.Rows[i].Cells["Code"].Value + "','" + "PUR" + "','" + PUR + "','" + dataGridView1.Rows[i].Cells["UOM"].Value + "','" + next_batch + "','001' UNION ALL ";
                            query += "SELECT '" + dataGridView1.Rows[i].Cells["Code"].Value + "','" + "RTL" + "','" + RTL + "','" + dataGridView1.Rows[i].Cells["UOM"].Value + "','" + next_batch + "','001' UNION ALL ";
                            query += "SELECT '" + dataGridView1.Rows[i].Cells["Code"].Value + "','" + "WHL" + "','" + WHL + "','" + dataGridView1.Rows[i].Cells["UOM"].Value + "','" + next_batch + "','001' UNION ALL ";
                            query += "SELECT '" + dataGridView1.Rows[i].Cells["Code"].Value + "','" + "PMS" + "','" + PMS + "','" + dataGridView1.Rows[i].Cells["UOM"].Value + "','" + next_batch + "','001' UNION ALL ";
                            query += "SELECT '" + dataGridView1.Rows[i].Cells["Code"].Value + "','" + "MRP" + "','" + MRP + "','" + dataGridView1.Rows[i].Cells["UOM"].Value + "','" + next_batch + "','001' UNION ALL ";


                            query = query.Substring(0, query.Length - 10);
                            DbFunctions.InsertUpdate(query);
                            
                            string query1 = "INSERT INTO INV_ITEM_PRICE_DF(ITEM_CODE,SAL_TYPE,PRICE,UNIT_CODE,BATCH_ID,BRANCH)";
                            query1 += "SELECT '" + dataGridView1.Rows[i].Cells["Code"].Value + "','" + "PUR" + "','" + PUR + "','" + dataGridView1.Rows[i].Cells["UOM"].Value + "','" + next_batch + "','001' UNION ALL ";
                            query1 += "SELECT '" + dataGridView1.Rows[i].Cells["Code"].Value + "','" + "RTL" + "','" + RTL + "','" + dataGridView1.Rows[i].Cells["UOM"].Value + "','" + next_batch + "','001' UNION ALL ";
                            query1 += "SELECT '" + dataGridView1.Rows[i].Cells["Code"].Value + "','" + "WHL" + "','" + WHL + "','" + dataGridView1.Rows[i].Cells["UOM"].Value + "','" + next_batch + "','001' UNION ALL ";
                            query1 += "SELECT '" + dataGridView1.Rows[i].Cells["Code"].Value + "','" + "PMS" + "','" + PMS + "','" + dataGridView1.Rows[i].Cells["UOM"].Value + "','" + next_batch + "','001' UNION ALL ";
                            query1 += "SELECT '" + dataGridView1.Rows[i].Cells["Code"].Value + "','" + "MRP" + "','" + MRP + "','" + dataGridView1.Rows[i].Cells["UOM"].Value + "','" + next_batch + "','001' UNION ALL ";


                            query1 = query1.Substring(0, query1.Length - 10);
                            DbFunctions.InsertUpdate(query1);
                            //if (UploadItem.Checked == true)
                            //{
                            if (qty1 > 0)
                            {
                                addstock(code, uom, Purchase_price, qty, next_batch);
                            }

                            //}
                            try
                            {

                                //if (conn.State == ConnectionState.Open)
                                //{
                                //}
                                //else
                                //{

                                //    conn.Open();
                                //}
                                //cmd.Connection = conn;
                                //string updatequery = "INSERT INTO RateChange(Item_code,datee,Price,Sale_Price, Qty) VALUES('" + dataGridView1.Rows[i].Cells["Code"].Value + "','" + DateTime.Now.ToString() + "','" + Convert.ToDecimal(PUR) + "','" + Convert.ToDecimal(RTL) + "','" + Convert.ToDecimal(qty) + "' )";
                                //cmd.CommandText = updatequery;
                                //cmd.ExecuteNonQuery();
                                //conn.Close();
                                RateChangeDB.ItemCode = dataGridView1.Rows[i].Cells["Code"].Value.ToString();
                                RateChangeDB.Date = DateTime.Now;
                                RateChangeDB.Price = Convert.ToDecimal(PUR);
                                RateChangeDB.SalePrice = Convert.ToDecimal(RTL);
                                RateChangeDB.Qty= Convert.ToDecimal(qty);
                                RateChangeDB.Insert();
                            }

                            catch
                            {
                            }



                        }
                        catch (Exception e1)
                        {
                            MessageBox.Show(e1.Message);
                        }

                        //item directory unit insertion
                        try
                        {
                            //if (conn.State == ConnectionState.Open)
                            //{
                            //}
                            //else
                            //{

                            //    conn.Open();
                            //}
                            //cmd.Connection = conn;


                            //string itemfields = "ITEM_CODE,BARCODE,UNIT_CODE,PACK_SIZE";
                            float packsize = 1;
                            //string itemvalues = "'" + dataGridView1.Rows[i].Cells["Code"].Value + "','" + dataGridView1.Rows[i].Cells["Barcode"].Value + "','" + dataGridView1.Rows[i].Cells["UOM"].Value + "','" + packsize + "'";

                            InvItemDirectoryUnits.ItemCode= dataGridView1.Rows[i].Cells["Code"].Value.ToString();
                            InvItemDirectoryUnits.Barcode= dataGridView1.Rows[i].Cells["Barcode"].Value.ToString();
                            InvItemDirectoryUnits.Unitcode= dataGridView1.Rows[i].Cells["UOM"].Value.ToString();
                            InvItemDirectoryUnits.PackSize= packsize;
                            InvItemDirectoryUnits.Insert();
                            //cmd.CommandText = "INSERT INTO INV_ITEM_DIRECTORY_UNITS(" + itemfields + ") VALUES(" + itemvalues + ")";
                            //cmd.ExecuteNonQuery();
                            //conn.Close();


                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                    }
                    catch (Exception ee)
                    {

                        MessageBox.Show(ee.Message);
                    }
                }
                ExportToPDF();
                MessageBox.Show(insert + "Item Added!");
            }
        }

        public void addstock(string code,string uom,decimal Purchase_price,string qty,string batch)
        {
            try
            {
                
                if (ID == ""&&Convert.ToInt16(qty)>0&&UploadItem.Checked == true)                        
                {
                    //cmd.Parameters.Clear();
                    Int64 DID = Convert.ToInt64(General.generateStockID());
                    //cmd.CommandText = "INSERT INTO INV_STK_TRX_HDR(BRANCH,DOC_NO,DOC_DATE_GRE,DOC_TYPE,AddedBy) VALUES('" + lg.Branch + "','" + DID + "','" + Convert.ToDateTime(System.DateTime.Today.ToString("MM/dd/yyyy")) + "','INV.STK.OPN','" + lg.EmpId + "')";
                    InvStkTrxHdrDB.Branch = lg.Branch;
                    InvStkTrxHdrDB.DocNo = DID.ToString();;
                    InvStkTrxHdrDB.DocDateGre = Convert.ToDateTime(System.DateTime.Today.ToString("MM/dd/yyyy"));
                    InvStkTrxHdrDB.DocType= "INV.STK.OPN";
                    InvStkTrxHdrDB.AddedBy = lg.EmpId;
                    InvStkTrxHdrDB.Insert_Bulk();

                    string query = "";
                    query = "INSERT INTO INV_STK_TRX_DTL(DOC_TYPE,DOC_NO,ITEM_CODE,UOM,PRICE,QUANTITY,BRANCH,UOM_QTY,PRICE_BATCH) ";
                    query += "SELECT 'INV.STK.OPN','" + DID + "','" + code + "','" + uom + "','" + Purchase_price + "','" + qty + "','" + lg.Branch + "',1,'"+batch+"'";
                    query += " UNION ALL ";
                    query = query.Substring(0, query.Length - 10);
                    DbFunctions.InsertUpdate(query);                    
                }
            
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        
            
        }
        public void ExportToPDF( )
        {
            

            iTextSharp.text.Rectangle rect = PageSize.GetRectangle("A4");

            iTextSharp.text.Document pdfdoc = new iTextSharp.text.Document(new iTextSharp.text.Rectangle(rect.Width, rect.Height), 0, 0, 17, -35);

            try
            {
                DirectoryInfo dir1 = new DirectoryInfo(Application.StartupPath + "\\Barcode");
                if (!Directory.Exists(Application.StartupPath + "\\Barcode"))
                {
                    dir1.Create();
                }
                if (File.Exists(Application.StartupPath + "\\Barcode\\Barcode1.pdf"))
                {
                    File.Delete(Application.StartupPath + "\\Barcode\\Barcode1.pdf");
                }

                //pdfdoc = new Document(PageSize.A4, -2, 20, -1, 20);
                PdfWriter writer = PdfWriter.GetInstance(pdfdoc, new FileStream(Application.StartupPath + "\\Barcode\\Barcode1.pdf", FileMode.Create));
                PdfPTable tbl = new PdfPTable(5);
                tbl.WidthPercentage = 100;


                //   float[] widths = new float[] {1f, 1f,3f,1f,1f };
                //  tbl.SetWidths(widths);
                tbl.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                tbl.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                tbl.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;

                pdfdoc.Open();
                int intotalCount = 0;
                Class.BarcodeSettings Info = new Class.BarcodeSettings();

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Cells["Code"].Value != null && dataGridView1.Rows[i].Cells["Code"].Value.ToString() != "")
                    {
                        int inCopies = 0;
                        if (dataGridView1.Rows[i].Cells["Quantity"].Value != null)
                        {
                            int.TryParse(dataGridView1.Rows[i].Cells["Quantity"].Value.ToString(), out inCopies); // number of copies of arcode to be printed
                        }
                        string batch = "";
                        try
                        {
                            //if (conn.State == ConnectionState.Open)
                            //{
                            //}
                            //else
                            //{

                            //    conn.Open();
                            //}
                            //cmd.Connection = conn;
                            //cmd.CommandText = "select max(R_id)as batch FROM RateChange Where Item_code='" + dataGridView1.Rows[i].Cells["Code"].Value.ToString() + "'";
                            RateChangeDB.ItemCode = dataGridView1.Rows[i].Cells["Code"].Value.ToString();
                            RateChangeDB.BatchByItemCode();
                            //cmd.CommandType = CommandType.Text;
                            SqlDataReader rd5;
                            rd5 = RateChangeDB.BatchByItemCode();
                            while (rd5.Read())
                            {
                                batch = (Convert.ToInt32(rd5[0])).ToString() + dataGridView1.Rows[i].Cells["Code"].Value.ToString();
                            }
                            //.Close();
                        }
                        catch (Exception exc)
                        {
                        }
                        for (int j = 0; j < inCopies; j++)
                        {
                            string strProductCode = string.Empty;
                            string strCode = string.Empty;
                            string strCompanyName = string.Empty;
                            string strBarcodeValue = string.Empty;
                            string strProductName = string.Empty;
                            string RetailPrice = string.Empty;
                            if (IsProductCode == true)
                            {
                                //   strCode = dgbarcodeprint.Rows[i].Cells["Item_Code"].Value.ToString();

                                if (IsProductCode == true)
                                    strProductCode = strCode;
                            }
                            else
                            {
                                strProductCode = dataGridView1.Rows[i].Cells["Name"].Value.ToString();
                            }

                            strProductName = dataGridView1.Rows[i].Cells["Name"].Value.ToString();


                            if (IsBarcodeValue == true)
                            {
                                strBarcodeValue = batch;

                            }
                            else
                            {
                                strBarcodeValue = batch;
                            }

                            if (IsCompany == true)
                                strCompanyName = companyname;
                            string strMRP = string.Empty;

                            if (IsMRP == true)
                            {
                                //  strMRP = PriceType+":"+ dgbarcodeprint.Rows[i].Cells["Rate"].Value.ToString();
                                strMRP = "INR. " + dataGridView1.Rows[i].Cells["Sales price"].Value.ToString();
                            }
                            string strSecretPurchaseRateCode = string.Empty;

                            PdfContentByte pdfcb = writer.DirectContent;
                            Barcode128 code128 = new Barcode128();
                            code128.Code = strBarcodeValue;
                            code128.Extended = false;
                            code128.CodeType = iTextSharp.text.pdf.Barcode.CODE128;
                            code128.BarHeight = 15;

                            if (IsBarcodeValue == false)
                            {
                                code128.Font = null;
                                code128.AltText = strBarcodeValue;
                            }
                            code128.BarHeight = HEIGHT;

                            code128.Size = 8;
                            code128.Baseline = 8;
                            code128.TextAlignment = Element.ALIGN_CENTER;
                            iTextSharp.text.Image image128 = code128.CreateImageWithBarcode(pdfcb, null, null);

                            Phrase phrase = new Phrase();
                            phrase.Font.Size = 7f;

                            if (IsCompany == true)
                            {
                                phrase.Add(new Chunk(companyname + Environment.NewLine));
                            }

                            phrase.Add(new Chunk(strProductName + Environment.NewLine));
                            phrase.Add(new Chunk(Environment.NewLine));
                            PdfPCell cell = new PdfPCell(phrase);
                            //     cell.FixedHeight = 80.69f;

                            cell.PaddingRight = -10;
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cell.Border = iTextSharp.text.Rectangle.NO_BORDER;

                            //cell.PaddingBottom=20;
                            // 

                            phrase.Add(new Chunk(image128, 0, 0));
                            //     phrase.Add(new Chunk(Environment.NewLine));
                            if (IsMRP == true)
                            {
                                phrase.Add(new Chunk(Environment.NewLine + strMRP));
                            }
                            if (IsProductCode == true)
                            {
                                phrase.Add(new Chunk(Environment.NewLine + strCode));
                            }
                            phrase.Add(new Chunk(Environment.NewLine));
                            // phrase.Add(new Chunk(Environment.NewLine));
                            cell.PaddingRight = 3;
                            tbl.AddCell(cell);

                            intotalCount++;
                        }


                    }
                    
                }
                int reminder = intotalCount % 5;
                if (reminder != 0)
                {
                    for (int i = reminder; i < 6; ++i)
                    {
                        tbl.AddCell("");
                    }
                }
                if (tbl.Rows.Count != 0)
                {
                    pdfdoc.Add(tbl);
                    pdfdoc.SetMargins(0, 0, 17, -35);
                    pdfdoc.Close();
                    System.Diagnostics.Process.Start(Application.StartupPath + "\\Barcode\\Barcode1.pdf");
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
                    MessageBox.Show("Error:" + ex.Message, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

       
       
        private void Btn_Clear_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource = null;
                dataGridView1.Rows.Clear();
                lblFilename.Text = "";
            }
            catch (Exception exc)
            {
            }
        }
        private void Generate_Barcode()
        {
            DataTable dt = new DataTable();
            dt = barcodes.GetSettings();
            if (dt.Rows.Count > 0)
            {
                IsMRP = Convert.ToBoolean(dt.Rows[0][1]);
                IsProductCode = Convert.ToBoolean(dt.Rows[0][3]);
                IsCompany = Convert.ToBoolean(dt.Rows[0][4]);
                companyname = Convert.ToString(dt.Rows[0][5]);
                WIDTH = Convert.ToInt32(dt.Rows[0][6]);
                HEIGHT = Convert.ToInt32(dt.Rows[0][7]);
                IsBarcodeValue = Convert.ToBoolean(dt.Rows[0][8]);
                PriceType = dt.Rows[0][9].ToString();
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

        private void bt_download_Click(object sender, EventArgs e)
        {
            try
            {

                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter();
                //if (conn.State == ConnectionState.Open)
                //{
                //}
                //else
                //{
                //    conn.Open();
                //}
                #region
                //cmd.Connection = conn;
                //dt.Clear();

                
                /*cmd.CommandText = @"SELECT        INV_ITEM_DIRECTORY.CODE AS Code, INV_ITEM_DIRECTORY.HSN AS [HSN Code], INV_ITEM_DIRECTORY.DESC_ENG AS Name, 
                         INV_ITEM_DIRECTORY.DESC_ARB AS [Arabic Name], INV_ITEM_TYPE.CODE AS Type, INV_ITEM_GROUP.CODE AS [Group], 
                         INV_ITEM_CATEGORY.CODE AS Category, INV_ITEM_DIRECTORY.TaxId, INV_ITEM_TM.CODE AS Brand, INV_ITEM_DIRECTORY_UNITS.UNIT_CODE AS UOM, 
                         INV_ITEM_DIRECTORY_UNITS.BARCODE AS Barcode, INV_ITEM_PRICE_DF_3.PRICE AS [Whole Sale], STOCK.STOCK AS Quantity, 
                         INV_ITEM_PRICE_DF_1.PRICE AS [Purchase price], INV_ITEM_PRICE_DF.PRICE AS [Retail price], INV_ITEM_PRICE_DF_2.PRICE AS [Promotional price], 
                         INV_ITEM_PRICE_DF_4.PRICE AS [Maximum price], INV_ITEM_DIRECTORY.COST_PRICE AS [Cost price], INV_ITEM_DIRECTORY.SALE_PRICE AS [Sales price]
FROM            INV_ITEM_PRICE_DF AS INV_ITEM_PRICE_DF_4 LEFT OUTER JOIN
                         INV_ITEM_PRICE_DF AS INV_ITEM_PRICE_DF_2 INNER JOIN
                         INV_ITEM_DIRECTORY_UNITS ON INV_ITEM_PRICE_DF_2.ITEM_CODE = INV_ITEM_DIRECTORY_UNITS.ITEM_CODE AND 
                         INV_ITEM_PRICE_DF_2.UNIT_CODE = INV_ITEM_DIRECTORY_UNITS.UNIT_CODE LEFT OUTER JOIN
                         INV_ITEM_PRICE_DF AS INV_ITEM_PRICE_DF_3 ON INV_ITEM_DIRECTORY_UNITS.ITEM_CODE = INV_ITEM_PRICE_DF_3.ITEM_CODE AND 
                         INV_ITEM_DIRECTORY_UNITS.UNIT_CODE = INV_ITEM_PRICE_DF_3.UNIT_CODE ON 
                         INV_ITEM_PRICE_DF_4.ITEM_CODE = INV_ITEM_DIRECTORY_UNITS.ITEM_CODE AND 
                         INV_ITEM_PRICE_DF_4.UNIT_CODE = INV_ITEM_DIRECTORY_UNITS.UNIT_CODE RIGHT OUTER JOIN
                         INV_ITEM_PRICE_DF ON INV_ITEM_DIRECTORY_UNITS.UNIT_CODE = INV_ITEM_PRICE_DF.UNIT_CODE AND 
                         INV_ITEM_DIRECTORY_UNITS.ITEM_CODE = INV_ITEM_PRICE_DF.ITEM_CODE LEFT OUTER JOIN
                         INV_ITEM_PRICE_DF AS INV_ITEM_PRICE_DF_1 ON INV_ITEM_DIRECTORY_UNITS.UNIT_CODE = INV_ITEM_PRICE_DF_1.UNIT_CODE AND 
                         INV_ITEM_DIRECTORY_UNITS.ITEM_CODE = INV_ITEM_PRICE_DF_1.ITEM_CODE RIGHT OUTER JOIN
                         INV_ITEM_DIRECTORY LEFT OUTER JOIN
                         GEN_TAX_MASTER ON INV_ITEM_DIRECTORY.TaxId = GEN_TAX_MASTER.TaxId ON 
                         INV_ITEM_DIRECTORY_UNITS.ITEM_CODE = INV_ITEM_DIRECTORY.CODE LEFT OUTER JOIN
                         INV_ITEM_TYPE ON INV_ITEM_DIRECTORY.TYPE = INV_ITEM_TYPE.CODE LEFT OUTER JOIN
                         INV_ITEM_CATEGORY ON INV_ITEM_DIRECTORY.CATEGORY = INV_ITEM_CATEGORY.CODE LEFT OUTER JOIN
                         INV_ITEM_GROUP ON INV_ITEM_DIRECTORY.[GROUP] = INV_ITEM_GROUP.CODE LEFT OUTER JOIN
                         INV_ITEM_TM ON INV_ITEM_DIRECTORY.TRADEMARK = INV_ITEM_TM.CODE LEFT OUTER JOIN
                             (SELECT        INV_ITEM_DIRECTORY.CODE, INV_ITEM_DIRECTORY.DESC_ENG,INV_ITEM_DIRECTORY_UNITS.UNIT_CODE,ISNULL(SALES.QTY, 0) + ISNULL(SALES_RETURN.QTY, 0) 
                                                         - ISNULL(PURCHASE_RETURN.QTY, 0) + ISNULL(PURCHASE.QTY, 0)+ISNULL(OPENING.QTY,0) AS STOCK FROM INV_ITEM_DIRECTORY LEFT OUTER JOIN INV_ITEM_DIRECTORY_UNITS ON INV_ITEM_DIRECTORY.CODE=INV_ITEM_DIRECTORY_UNITS.ITEM_CODE LEFT OUTER JOIN
                                                             (SELECT        ITEM_CODE, ISNULL(SUM(QUANTITY), 0) AS QTY, UOM
                                                               FROM            INV_SALES_DTL
                                                               WHERE        (DOC_TYPE IN ('SAL.CSS', 'SAL.CRD'))
                                                               GROUP BY ITEM_CODE, UOM) AS SALES ON SALES.ITEM_CODE = INV_ITEM_DIRECTORY.CODE AND 
                                                         SALES.UOM = INV_ITEM_DIRECTORY_UNITS.UNIT_CODE LEFT OUTER JOIN
                                                             (SELECT        ITEM_CODE, ISNULL(SUM(QTY_RCVD), 0) AS QTY, UOM
                                                               FROM            INV_PURCHASE_DTL
                                                               WHERE        (DOC_TYPE IN ('PUR.CSS', 'PUR.CRD'))
                                                               GROUP BY ITEM_CODE, UOM) AS PURCHASE ON PURCHASE.ITEM_CODE = INV_ITEM_DIRECTORY.CODE AND 
                                                         PURCHASE.UOM = INV_ITEM_DIRECTORY_UNITS.UNIT_CODE LEFT OUTER JOIN

														 (SELECT        ITEM_CODE, ISNULL(SUM(QUANTITY), 0) AS QTY, UOM
                                                               FROM            INV_STK_TRX_DTL
                                                               WHERE        (DOC_TYPE IN ('INV.STK.OPN'))
                                                               GROUP BY ITEM_CODE, UOM) AS OPENING ON OPENING.ITEM_CODE = INV_ITEM_DIRECTORY.CODE AND 
                                                         OPENING.UOM = INV_ITEM_DIRECTORY_UNITS.UNIT_CODE LEFT OUTER JOIN

                                                             (SELECT        ITEM_CODE, ISNULL(SUM(QTY_RCVD), 0) AS QTY, UOM
                                                               FROM            INV_PURCHASE_DTL
                                                               WHERE        (DOC_TYPE IN ('LGR.PRT'))
                                                               GROUP BY ITEM_CODE, UOM) AS PURCHASE_RETURN ON PURCHASE_RETURN.ITEM_CODE = INV_ITEM_DIRECTORY.CODE AND 
                                                         PURCHASE_RETURN.UOM = INV_ITEM_DIRECTORY_UNITS.UNIT_CODE LEFT OUTER JOIN
                                                             (SELECT        ITEM_CODE, ISNULL(SUM(QUANTITY), 0) AS QTY, UOM
                                                               FROM            INV_SALES_DTL
                                                               WHERE        (DOC_TYPE IN ('SAL.CSR'))
                                                               GROUP BY ITEM_CODE, UOM) AS SALES_RETURN ON SALES_RETURN.ITEM_CODE = INV_ITEM_DIRECTORY.CODE AND 
                                                         PURCHASE_RETURN.UOM = INV_ITEM_DIRECTORY_UNITS.UNIT_CODE)  AS STOCK ON 
                         STOCK.CODE = INV_ITEM_DIRECTORY.CODE AND STOCK.UNIT_CODE=INV_ITEM_DIRECTORY_UNITS.UNIT_CODE
WHERE        (INV_ITEM_PRICE_DF.SAL_TYPE = 'RTL') AND (INV_ITEM_PRICE_DF_1.SAL_TYPE = 'PUR') AND (INV_ITEM_PRICE_DF_2.SAL_TYPE = 'PMS') AND 
                         (INV_ITEM_PRICE_DF_3.SAL_TYPE = 'WHL') AND (INV_ITEM_PRICE_DF_4.SAL_TYPE = 'MRP')";*/
                //cmd.CommandText = "SELECT INV_ITEM_DIRECTORY.CODE AS [Code],INV_ITEM_DIRECTORY.HSN AS [HSN Code],INV_ITEM_DIRECTORY.DESC_ENG AS [Name],INV_ITEM_DIRECTORY.DESC_ARB AS[Arabic Name],INV_ITEM_TYPE.CODE AS [Type], INV_ITEM_GROUP.CODE AS [Group], INV_ITEM_CATEGORY.CODE AS Category, INV_ITEM_DIRECTORY.TaxId,INV_ITEM_TM.CODE AS Brand, INV_ITEM_DIRECTORY_UNITS.UNIT_CODE as UOM, INV_ITEM_DIRECTORY_UNITS.PACK_SIZE AS Quantity,INV_ITEM_DIRECTORY_UNITS.BARCODE AS Barcode,INV_ITEM_PRICE_DF_3.PRICE AS [Whole Sale], INV_ITEM_PRICE_DF_1.PRICE AS [Purchase price], INV_ITEM_PRICE_DF.PRICE AS [Retail price],INV_ITEM_PRICE_DF_2.PRICE AS [Promotional price],  INV_ITEM_PRICE_DF_4.PRICE AS [Maximum price], INV_ITEM_DIRECTORY.COST_PRICE as [Cost price],INV_ITEM_DIRECTORY.SALE_PRICE as [Sales price] FROM INV_ITEM_PRICE_DF AS INV_ITEM_PRICE_DF_4 LEFT OUTER JOIN INV_ITEM_PRICE_DF AS INV_ITEM_PRICE_DF_2 INNER JOIN INV_ITEM_DIRECTORY_UNITS ON INV_ITEM_PRICE_DF_2.ITEM_CODE = INV_ITEM_DIRECTORY_UNITS.ITEM_CODE AND INV_ITEM_PRICE_DF_2.UNIT_CODE = INV_ITEM_DIRECTORY_UNITS.UNIT_CODE LEFT OUTER JOIN  INV_ITEM_PRICE_DF AS INV_ITEM_PRICE_DF_3 ON INV_ITEM_DIRECTORY_UNITS.ITEM_CODE = INV_ITEM_PRICE_DF_3.ITEM_CODE AND INV_ITEM_DIRECTORY_UNITS.UNIT_CODE = INV_ITEM_PRICE_DF_3.UNIT_CODE ON INV_ITEM_PRICE_DF_4.ITEM_CODE = INV_ITEM_DIRECTORY_UNITS.ITEM_CODE AND INV_ITEM_PRICE_DF_4.UNIT_CODE = INV_ITEM_DIRECTORY_UNITS.UNIT_CODE RIGHT OUTER JOIN INV_ITEM_PRICE_DF ON INV_ITEM_DIRECTORY_UNITS.UNIT_CODE = INV_ITEM_PRICE_DF.UNIT_CODE AND INV_ITEM_DIRECTORY_UNITS.ITEM_CODE = INV_ITEM_PRICE_DF.ITEM_CODE LEFT OUTER JOIN INV_ITEM_PRICE_DF AS INV_ITEM_PRICE_DF_1 ON INV_ITEM_DIRECTORY_UNITS.UNIT_CODE = INV_ITEM_PRICE_DF_1.UNIT_CODE AND INV_ITEM_DIRECTORY_UNITS.ITEM_CODE = INV_ITEM_PRICE_DF_1.ITEM_CODE RIGHT OUTER JOIN INV_ITEM_DIRECTORY LEFT OUTER JOIN GEN_TAX_MASTER ON INV_ITEM_DIRECTORY.TaxId = GEN_TAX_MASTER.TaxId ON INV_ITEM_DIRECTORY_UNITS.ITEM_CODE = INV_ITEM_DIRECTORY.CODE LEFT OUTER JOIN INV_ITEM_TYPE ON INV_ITEM_DIRECTORY.TYPE=INV_ITEM_TYPE.CODE LEFT  OUTER JOIN INV_ITEM_CATEGORY ON INV_ITEM_DIRECTORY.CATEGORY=INV_ITEM_CATEGORY.CODE LEFT OUTER JOIN INV_ITEM_GROUP ON INV_ITEM_DIRECTORY.[GROUP]=INV_ITEM_GROUP.CODE LEFT OUTER JOIN INV_ITEM_TM ON INV_ITEM_DIRECTORY.TRADEMARK=INV_ITEM_TM.CODE WHERE (INV_ITEM_PRICE_DF.SAL_TYPE = 'RTL') AND (INV_ITEM_PRICE_DF_1.SAL_TYPE = 'PUR') AND (INV_ITEM_PRICE_DF_2.SAL_TYPE = 'PMS') AND (INV_ITEM_PRICE_DF_3.SAL_TYPE = 'WHL') AND (INV_ITEM_PRICE_DF_4.SAL_TYPE = 'MRP')";
                //da.SelectCommand = cmd;
                //da.Fill(dt);
                //cmd.Parameters.Clear();

                ////adp.Fill(dt);
#endregion
                dataGridView1.DataSource = ItemDirectoryDB.GetDownloads();
            }

            catch (Exception R) { string X = R.Message; }
            {


            }
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

        private void Item_Mater_Bulk_Upload_Load(object sender, EventArgs e)
        {
            Generate_Barcode();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            if (!bck_Itemworker.IsBusy)
            {
                bck_Itemworker.RunWorkerAsync();
            }
        }

        private void bck_Itemworker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressUpload.Value = e.ProgressPercentage;
        }

        private void bck_Itemworker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Success");
            progressUpload.Value = 0;
        }

        private void bck_Itemworker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (Valid())
            {
                Int16 insert = 0;

                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    //try
                    //{
                    decimal Purchase_price;
                    string code = Convert.ToString(dataGridView1.Rows[i].Cells["Code"].Value);
                    string uom = Convert.ToString(dataGridView1.Rows[i].Cells["UOM"].Value);

                    if (dataGridView1.Rows[i].Cells["Purchase price"].Value.ToString() == "")
                    {
                        Purchase_price = 0;
                    }
                    else
                    {
                        Purchase_price = Convert.ToDecimal(dataGridView1.Rows[i].Cells["Purchase price"].Value);
                    }

                    string qty = Convert.ToString(dataGridView1.Rows[i].Cells["Quantity"].Value);
                    if (General.ItemExists(code, ID, "INV_ITEM_DIRECTORY"))
                    {
                        MessageBox.Show("Item with the same code:" + code + "  already exists!");
                        return;
                    }
                    // inserting to item directory

                    //try
                    //{
                    decimal CPR = 0, SPR = 0, TAXID = 0;
                    if (dataGridView1.Rows[i].Cells["Sales price"].Value != null)
                    {
                        if (dataGridView1.Rows[i].Cells["Sales price"].Value.ToString() != "")
                        {
                            SPR = Convert.ToDecimal(dataGridView1.Rows[i].Cells["Sales price"].Value);
                        }
                    }
                    if (dataGridView1.Rows[i].Cells["TaxId"].Value != null)
                    {
                        if (dataGridView1.Rows[i].Cells["TaxId"].Value.ToString() != "")
                        {
                            TAXID = Convert.ToDecimal(dataGridView1.Rows[i].Cells["TaxId"].Value);
                        }
                    }

                    if (dataGridView1.Rows[i].Cells["Cost price"].Value != null)
                    {
                        if (dataGridView1.Rows[i].Cells["Cost price"].Value.ToString() != "")
                        {
                            CPR = Convert.ToDecimal(dataGridView1.Rows[i].Cells["Cost price"].Value);
                        }
                    }

                    //if (conn.State == ConnectionState.Open)
                    //{
                    //}
                    //else
                    //{

                    //    conn.Open();
                    //}
                    //cmd.Connection = conn;
                    //string fields = "CODE,DESC_ENG,DESC_ARB,TYPE,[GROUP],CATEGORY,TRADEMARK,COST_PRICE,SALE_PRICE,IN_ACTIVE,TaxId,HSN";
                    //string values = "'" + dataGridView1.Rows[i].Cells["Code"].Value.ToString() + "','" + Common.sqlEscape(dataGridView1.Rows[i].Cells["Name"].Value.ToString()) + "','" + dataGridView1.Rows[i].Cells["Arabic Name"].Value.ToString() + "','" + dataGridView1.Rows[i].Cells["Type"].Value.ToString() + "','" + dataGridView1.Rows[i].Cells["Group"].Value.ToString() + "','" + dataGridView1.Rows[i].Cells["Category"].Value.ToString() + "','" + dataGridView1.Rows[i].Cells["Brand"].Value.ToString() + "'," + Convert.ToDecimal(CPR) + "," + Convert.ToDecimal(SPR) + ",'Y'," + Convert.ToInt32(TAXID) + ",'" + dataGridView1.Rows[i].Cells["HSN Code"].Value.ToString() + "'";


                    //cmd.CommandText = "INSERT INTO INV_ITEM_DIRECTORY(" + fields + ") VALUES(" + values + ")";
                    //cmd.ExecuteNonQuery();
                    ItemDirectoryDB.Code = dataGridView1.Rows[i].Cells["Code"].Value.ToString();
                    ItemDirectoryDB.Desc_Eng = dataGridView1.Rows[i].Cells["Name"].Value.ToString();
                    ItemDirectoryDB.Desc_Arb = dataGridView1.Rows[i].Cells["Arabic Name"].Value.ToString();
                    ItemDirectoryDB.Type = dataGridView1.Rows[i].Cells["Type"].Value.ToString();
                    ItemDirectoryDB.Group = dataGridView1.Rows[i].Cells["Group"].Value.ToString();
                    ItemDirectoryDB.Category = dataGridView1.Rows[i].Cells["Category"].Value.ToString();
                    ItemDirectoryDB.Trademark = dataGridView1.Rows[i].Cells["Brand"].Value.ToString();
                    ItemDirectoryDB.CostPrice = Convert.ToDecimal(CPR);
                    ItemDirectoryDB.SalePrice = Convert.ToDecimal(SPR);
                    ItemDirectoryDB.InActive = "Y";
                    ItemDirectoryDB.TaxId = Convert.ToInt32(dataGridView1.Rows[i].Cells["TaxId"].Value);
                    ItemDirectoryDB.Hsn = dataGridView1.Rows[i].Cells["HSN Code"].Value.ToString();
                    ItemDirectoryDB.InsertBulk();
                    insert++;

                    //  MessageBox.Show("Item Added!");
                   // conn.Close();
                    //}
                    //catch (Exception e2)
                    //{
                    //    MessageBox.Show(e2.Message);
                    //}

                    //inserting units



                    //inserting to item price


                    try
                    {

                        double PUR = 0.00, RTL = 0.00, MRP = 0.00, PMS = 0.00, WHL = 0.00;
                        string b;

                        if (dataGridView1.Rows[i].Cells["Purchase price"].Value != null)
                        {
                            if (dataGridView1.Rows[i].Cells["Purchase price"].Value.ToString() != "")
                            {
                                PUR = Convert.ToDouble(dataGridView1.Rows[i].Cells["Purchase price"].Value);
                            }
                        }


                        if (dataGridView1.Rows[i].Cells["Retail price"].Value != null)
                        {
                            if (dataGridView1.Rows[i].Cells["Retail price"].Value.ToString() != "")
                            {
                                RTL = Convert.ToDouble(dataGridView1.Rows[i].Cells["Retail price"].Value);
                            }
                        }

                        if (dataGridView1.Rows[i].Cells["Maximum price"].Value != null)
                        {
                            if (dataGridView1.Rows[i].Cells["Maximum price"].Value.ToString() != "")
                            {
                                MRP = Convert.ToDouble(dataGridView1.Rows[i].Cells["Maximum price"].Value);
                            }

                        }

                        if (dataGridView1.Rows[i].Cells["Promotional price"].Value != null)
                        {
                            if (dataGridView1.Rows[i].Cells["Promotional price"].Value.ToString() != "")
                            {
                                PMS = Convert.ToDouble(dataGridView1.Rows[i].Cells["Promotional price"].Value);
                            }

                        }
                        if (dataGridView1.Rows[i].Cells["Whole Sale"].Value != null)
                        {
                            if (dataGridView1.Rows[i].Cells["Whole Sale"].Value.ToString() != "")
                            {
                                WHL = Convert.ToDouble(dataGridView1.Rows[i].Cells["Whole Sale"].Value);
                            }

                        }
                        // string item_id = Convert.ToString(c["cCode"].Value);
                        double qty1 = 0;
                        if (dataGridView1.Rows[i].Cells["Quantity"].Value != null)
                        {
                            if (dataGridView1.Rows[i].Cells["Quantity"].Value.ToString() != "")
                            {
                                qty1 = Convert.ToDouble(dataGridView1.Rows[i].Cells["Quantity"].Value);
                            }
                        }
                        int next_batch_inc = se.max_batch_id(code);
                        string next_batch = "";
                        if (chkBarcode.Checked == false)
                        {
                            next_batch = code + "B" + next_batch_inc;
                        }
                        else
                        {
                            next_batch = code;
                        }
                        string price_batch = "";
                        string sup_id;
                        sup_id = "";
                        StockDB.ItemId = code;
                        StockDB.Qty = Convert.ToDecimal((qty1.ToString().Equals("") ? 0 : qty1));
                        StockDB.CostPrice = Convert.ToDecimal(PUR);
                        StockDB.SuppId = sup_id;
                        StockDB.Mrp = MRP.ToString();
                        StockDB.BatchId = next_batch;
                        StockDB.BatchIncrement = next_batch_inc;
                        StockDB.Insert();
                        
                        string query = "INSERT INTO INV_ITEM_PRICE(ITEM_CODE,SAL_TYPE,PRICE,UNIT_CODE,BATCH_ID,BRANCH) ";
                        query += "SELECT '" + dataGridView1.Rows[i].Cells["Code"].Value + "','" + "PUR" + "','" + PUR + "','" + dataGridView1.Rows[i].Cells["UOM"].Value + "','" + next_batch + "','001' UNION ALL ";
                        query += "SELECT '" + dataGridView1.Rows[i].Cells["Code"].Value + "','" + "RTL" + "','" + RTL + "','" + dataGridView1.Rows[i].Cells["UOM"].Value + "','" + next_batch + "','001' UNION ALL ";
                        query += "SELECT '" + dataGridView1.Rows[i].Cells["Code"].Value + "','" + "WHL" + "','" + WHL + "','" + dataGridView1.Rows[i].Cells["UOM"].Value + "','" + next_batch + "','001' UNION ALL ";
                        query += "SELECT '" + dataGridView1.Rows[i].Cells["Code"].Value + "','" + "PMS" + "','" + PMS + "','" + dataGridView1.Rows[i].Cells["UOM"].Value + "','" + next_batch + "','001' UNION ALL ";
                        query += "SELECT '" + dataGridView1.Rows[i].Cells["Code"].Value + "','" + "MRP" + "','" + MRP + "','" + dataGridView1.Rows[i].Cells["UOM"].Value + "','" + next_batch + "','001' UNION ALL ";


                        query = query.Substring(0, query.Length - 10);
                        DbFunctions.InsertUpdate(query);

                        string query1 = "INSERT INTO INV_ITEM_PRICE_DF(ITEM_CODE,SAL_TYPE,PRICE,UNIT_CODE,BATCH_ID,BRANCH) ";
                        query1 += "SELECT '" + dataGridView1.Rows[i].Cells["Code"].Value + "','" + "PUR" + "','" + PUR + "','" + dataGridView1.Rows[i].Cells["UOM"].Value + "','" + next_batch + "','001' UNION ALL ";
                        query1 += "SELECT '" + dataGridView1.Rows[i].Cells["Code"].Value + "','" + "RTL" + "','" + RTL + "','" + dataGridView1.Rows[i].Cells["UOM"].Value + "','" + next_batch + "','001' UNION ALL ";
                        query1 += "SELECT '" + dataGridView1.Rows[i].Cells["Code"].Value + "','" + "WHL" + "','" + WHL + "','" + dataGridView1.Rows[i].Cells["UOM"].Value + "','" + next_batch + "','001' UNION ALL ";
                        query1 += "SELECT '" + dataGridView1.Rows[i].Cells["Code"].Value + "','" + "PMS" + "','" + PMS + "','" + dataGridView1.Rows[i].Cells["UOM"].Value + "','" + next_batch + "','001' UNION ALL ";
                        query1 += "SELECT '" + dataGridView1.Rows[i].Cells["Code"].Value + "','" + "MRP" + "','" + MRP + "','" + dataGridView1.Rows[i].Cells["UOM"].Value + "','" + next_batch + "','001' UNION ALL ";


                        query1 = query1.Substring(0, query1.Length - 10);
                        DbFunctions.InsertUpdate(query1);
                        //if (UploadItem.Checked == true)
                        //{
                        if (qty1 > 0)
                        {
                            addstock(code, uom, Purchase_price, qty, next_batch);
                        }

                        //}
                        try
                        {

                            //if (conn.State == ConnectionState.Open)
                            //{
                            //}
                            //else
                            //{

                            //    conn.Open();
                            //}
                            //cmd.Connection = conn;
                            //string updatequery = "INSERT INTO RateChange(Item_code,datee,Price,Sale_Price, Qty) VALUES('" + dataGridView1.Rows[i].Cells["Code"].Value + "','" + DateTime.Now.ToString() + "','" + Convert.ToDecimal(PUR) + "','" + Convert.ToDecimal(RTL) + "','" + Convert.ToDecimal(qty) + "' )";
                            //cmd.CommandText = updatequery;
                            //cmd.ExecuteNonQuery();
                            //conn.Close();

                            RateChangeDB.ItemCode = dataGridView1.Rows[i].Cells["Code"].Value.ToString();
                            RateChangeDB.Date = DateTime.Now;
                            RateChangeDB.Price = Convert.ToDecimal(PUR);
                            RateChangeDB.SalePrice = Convert.ToDecimal(RTL);
                            RateChangeDB.Qty = Convert.ToDecimal(qty);
                            RateChangeDB.Insert();

                        }

                        catch
                        {
                        }



                    }
                    catch (Exception e1)
                    {
                        MessageBox.Show(e1.Message);
                    }

                    //item directory unit insertion
                    try
                    {
                        //if (conn.State == ConnectionState.Open)
                        //{
                        //}
                        //else
                        //{

                        //    conn.Open();
                        //}
                        //cmd.Connection = conn;
                        string barcodevalue = "";
                        if (chkBarcode.Checked)
                        {
                            barcodevalue = dataGridView1.Rows[i].Cells["Code"].Value.ToString();
                            if (Convert.ToInt32(barcodevalue) < 100)
                            {
                                if (barcodevalue.Length == 1)
                                {
                                    barcodevalue = "00" + barcodevalue;
                                }
                                else if (barcodevalue.Length == 2)
                                {
                                    barcodevalue = "0" + barcodevalue;
                                }
                            }
                        }
                        else
                        {
                            barcodevalue = dataGridView1.Rows[i].Cells["Barcode"].Value.ToString();
                        }

                        float packsize = 1;
                        InvItemDirectoryUnits.ItemCode = dataGridView1.Rows[i].Cells["Code"].Value.ToString();
                        InvItemDirectoryUnits.Barcode = dataGridView1.Rows[i].Cells["Barcode"].Value.ToString();
                        InvItemDirectoryUnits.Unitcode = dataGridView1.Rows[i].Cells["UOM"].Value.ToString();
                        InvItemDirectoryUnits.PackSize = packsize;
                        InvItemDirectoryUnits.Insert();
                        //string itemfields = "ITEM_CODE,BARCODE,UNIT_CODE,PACK_SIZE";
                        //float packsize = 1;
                        //string itemvalues = "'" + dataGridView1.Rows[i].Cells["Code"].Value + "','" + dataGridView1.Rows[i].Cells["Barcode"].Value + "','" + dataGridView1.Rows[i].Cells["UOM"].Value + "','" + packsize + "'";


                        //cmd.CommandText = "INSERT INTO INV_ITEM_DIRECTORY_UNITS(" + itemfields + ") VALUES(" + itemvalues + ")";
                        //cmd.ExecuteNonQuery();
                        //conn.Close();


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    //}
                    //catch (Exception ee)
                    //{

                    //    MessageBox.Show(ee.Message);
                    //}
                    int total = dataGridView1.Rows.Count;
                    int progress = i * 100 / total;
                    label2.BeginInvoke(new Action(() => label2.Text = progress.ToString() + "%"));
                    //  label2.Text = progress.ToString();
                    bck_Itemworker.ReportProgress(progress);
                    // System.Threading.Thread.Sleep(100);
                }
            }
        }

        private void kryptonButton4_Click(object sender, EventArgs e)
        {
            if (!barcodewrkr.IsBusy)
            {
                barcodewrkr.RunWorkerAsync();
            }
            
        }
        string com = "";
        private void barcodewrkr_DoWork(object sender, DoWorkEventArgs e)
        {
            getcomp();
            iTextSharp.text.Rectangle rect = PageSize.GetRectangle("A4");

            iTextSharp.text.Document pdfdoc = new iTextSharp.text.Document(new iTextSharp.text.Rectangle(rect.Width, rect.Height), 0, 0,(float)Convert.ToDouble(textBox1.Text), 0);

            try
            {
                DirectoryInfo dir1 = new DirectoryInfo(Application.StartupPath + "\\Barcode");
                if (!Directory.Exists(Application.StartupPath + "\\Barcode"))
                {
                    dir1.Create();
                }
                if (File.Exists(Application.StartupPath + "\\Barcode\\Barcode1.pdf"))
                {
                    File.Delete(Application.StartupPath + "\\Barcode\\Barcode1.pdf");
                }

                //pdfdoc = new Document(PageSize.A4, -2, 20, -1, 20);
                PdfWriter writer = PdfWriter.GetInstance(pdfdoc, new FileStream(Application.StartupPath + "\\Barcode\\Barcode1.pdf", FileMode.Create));
                PdfPTable tbl = new PdfPTable(5);
                tbl.WidthPercentage = 100;


                //   float[] widths = new float[] {1f, 1f,3f,1f,1f };
                //  tbl.SetWidths(widths);
                tbl.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                tbl.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                tbl.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;

                pdfdoc.Open();
                int intotalCount = 0;
                Class.BarcodeSettings Info = new Class.BarcodeSettings();

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Cells["Code"].Value != null && dataGridView1.Rows[i].Cells["Code"].Value.ToString() != "")
                    {
                        int inCopies = 0;
                        if (dataGridView1.Rows[i].Cells["Quantity"].Value != null)
                        {
                            int.TryParse(dataGridView1.Rows[i].Cells["Quantity"].Value.ToString(), out inCopies); // number of copies of arcode to be printed
                        }
                        string batch = "";
                        //try
                        //{
                        //    if (conn.State == ConnectionState.Open)
                        //    {
                        //    }
                        //    else
                        //    {

                        //        conn.Open();
                        //    }
                        //    cmd.Connection = conn;
                        //    cmd.CommandText = "select max(R_id)as batch FROM RateChange Where Item_code='" + dataGridView1.Rows[i].Cells["Code"].Value.ToString() + "'";
                        //    cmd.CommandType = CommandType.Text;
                        //    SqlDataReader rd5;
                        //    rd5 = cmd.ExecuteReader();
                        //    while (rd5.Read())
                        //    {
                        //        batch = (Convert.ToInt32(rd5[0])).ToString() + dataGridView1.Rows[i].Cells["Code"].Value.ToString();
                        //    }
                        //    conn.Close();
                        //}
                        //catch (Exception exc)
                        //{
                        //}


                        for (int j = 0; j < inCopies; j++)
                        {
                            string strProductCode = string.Empty;
                            string strCode = string.Empty;
                            string strCompanyName = string.Empty;
                            string strBarcodeValue = string.Empty;
                            string strProductName = string.Empty;
                            string RetailPrice = string.Empty;
                            if (IsProductCode == true)
                            {
                                //   strCode = dgbarcodeprint.Rows[i].Cells["Item_Code"].Value.ToString();

                                if (IsProductCode == true)
                                    strProductCode = strCode;
                            }
                            else
                            {
                                strProductCode = dataGridView1.Rows[i].Cells["Name"].Value.ToString();
                            }

                            strProductName = dataGridView1.Rows[i].Cells["Name"].Value.ToString();
                            int length = strProductName.Length;
                            if (length <= 23)
                            {
                                strProductName = dataGridView1.Rows[i].Cells["Name"].Value.ToString();
                            }
                            else
                            {
                                strProductName = strProductName.Substring(0, 23);
                            }
                            //if (IsBarcodeValue == true)
                            //{
                            //    strBarcodeValue = batch;

                            //}
                            //else
                            //{
                            //    strBarcodeValue = batch;
                            //}
                            if (chkBarcode.Checked)
                            {
                                strBarcodeValue = dataGridView1.Rows[i].Cells["Code"].Value.ToString();
                                if (Convert.ToInt32(strBarcodeValue) < 100)
                                {
                                    if (strBarcodeValue.Length == 1)
                                    {
                                        strBarcodeValue = "00" + strBarcodeValue;
                                    }
                                    else if (strBarcodeValue.Length == 2)
                                    {
                                        strBarcodeValue = "0" + strBarcodeValue;
                                    }                                    
                                }

                            }
                            else
                            {
                                strBarcodeValue = dataGridView1.Rows[i].Cells["Barcode"].Value.ToString();
                            }

                            if (IsCompany == true)
                                strCompanyName = com ;
                            string strMRP = string.Empty;

                            if (IsMRP == true)
                            {
                                //  strMRP = PriceType+":"+ dgbarcodeprint.Rows[i].Cells["Rate"].Value.ToString();
                                strMRP = getcurrency() + ". " + dataGridView1.Rows[i].Cells["Sales price"].Value.ToString();
                            }
                            string strSecretPurchaseRateCode = string.Empty;

                            PdfContentByte pdfcb = writer.DirectContent;
                            Barcode128 code128 = new Barcode128();
                            code128.Code = strBarcodeValue;
                            code128.Extended = false;
                            code128.CodeType = iTextSharp.text.pdf.Barcode.CODE128;
                            code128.BarHeight = 15;

                            if (IsBarcodeValue == false)
                            {
                                code128.Font = null;
                                code128.AltText = strBarcodeValue;
                            }
                            code128.BarHeight = HEIGHT;

                            code128.Size = 7;
                            code128.Baseline = 14;
                            code128.TextAlignment = Element.ALIGN_CENTER;
                            iTextSharp.text.Image image128 = code128.CreateImageWithBarcode(pdfcb, null, null);

                            Phrase phrase = new Phrase();
                            phrase.Font.Size = 8f;
                            iTextSharp.text.Font smallfont = FontFactory.GetFont("Book Antiqua", (float)Convert.ToDouble(textBox2.Text));
                            if (IsCompany == true)
                            {
                                phrase.Add(new Chunk(companyname + Environment.NewLine, smallfont));
                            }
                          
                            phrase.Add(new Chunk(strProductName + Environment.NewLine,smallfont));
                            phrase.Add(new Chunk(Environment.NewLine));
                            phrase.Add(new Chunk(image128, 0, 0));
                            //     phrase.Add(new Chunk(Environment.NewLine));
                            if (IsMRP == true)
                            {
                                phrase.Add(new Chunk(Environment.NewLine + strMRP));
                            }
                            if (IsProductCode == true)
                            {
                                phrase.Add(new Chunk(Environment.NewLine + strBarcodeValue, smallfont));
                            }
                            PdfPCell cell = new PdfPCell(phrase);
                            //cell.FixedHeight = 80.69f;

                            cell.PaddingRight = -10;
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                            if(!chkboarder.Checked)
                            cell.Border = iTextSharp.text.Rectangle.NO_BORDER;

                            phrase.Add(new Chunk(Environment.NewLine));
                            // phrase.Add(new Chunk(Environment.NewLine));
                            cell.PaddingRight = 3;
                            cell.FixedHeight = (float)Convert.ToDouble(textBox3.Text);
                            cell.BorderColor = new iTextSharp.text.Color(150);

                            tbl.AddCell(cell);

                            intotalCount++;

                        }


                    }
                    int total = dataGridView1.Rows.Count;
                    int progress = i * 100 / total;
                    label2.BeginInvoke(new Action(() => label2.Text = progress.ToString() + "%"));
                    //  label2.Text = progress.ToString();
                    barcodewrkr.ReportProgress(progress);
                }
                int reminder = intotalCount % 5;
                if (reminder != 0)
                {
                    for (int i = reminder; i < 6; ++i)
                    {
                        tbl.AddCell("");
                    }
                }
                if (tbl.Rows.Count != 0)
                {
                    pdfdoc.Add(tbl);
                    pdfdoc.SetMargins(0, 0, (float)Convert.ToDouble(textBox1.Text), 0);
                    pdfdoc.Close();
                    System.Diagnostics.Process.Start(Application.StartupPath + "\\Barcode\\Barcode1.pdf");
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
                    MessageBox.Show("Error:" + ex.Message, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void barcodewrkr_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressUpload.Value = e.ProgressPercentage;
        }

        private void barcodewrkr_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressUpload.Value = 0;
        }

        string getcurrency()
        {
            string cur = Common.getcurrency();            
            return cur;
        }
        void getcomp()
        {           
            com = Common.CompanyNamebarcode();            
        }
    }
}
