using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Windows.Forms;



namespace Sys_Sols_Inventory
{
    public partial class Barcode_Stock_Items : Form
    {
        private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        private SqlCommand cmd = new SqlCommand();
        private SqlDataAdapter adapter = new SqlDataAdapter();
        Class.BarcodeSettings barcodse = new Class.BarcodeSettings();
        Initial mdi = (Initial)Application.OpenForms["Initial"];
        Login lg = (Login)Application.OpenForms["Login"];
        private BindingSource source = new BindingSource();
        //    private DataTable table = new DataTable();
        private bool hasBatch = General.IsEnabled(Settings.Batch);
        decimal decTotal = 0;
        string companyname;
        bool IsMRP = false, IsProductCode = false, IsCompany = false, IsBarcodeValue = false;
        int HEIGHT, WIDTH;
        bool HasArabic = General.IsEnabled(Settings.Arabic);
        string ID, PriceType;
        string[] PrnContent = new string[1000];
        int Totalnocopies = 0;
        int balancecopies = 0;
        int Reminder = 0;
        int noofcopies = 0;
        public bool Ismanual = true;
        public Barcode_Stock_Items()
        {
            InitializeComponent();
            AddColumnsTodgbarcodeprint();
        }


        public Barcode_Stock_Items(string itemcode, string name, string ara, string batch, string unit, string barcode, string stock, string rate, string code, int i)
        {
            InitializeComponent();
            AddColumnsTodgbarcodeprint();
            FillPrintingGrid(itemcode, name, ara, batch, unit, barcode, stock, rate, code, i);

        }


        public void ResetAll()
        {
            dgbarcodeprint.Rows.Clear();
            Totalnocopies = 0;
            balancecopies = 0;
            Reminder = 0;
            noofcopies = 0;
            printed = 0;
            x1 = 711;
            x2 = 702;
            x3 = 667;

            x4 = 703;
            x5 = 617;

            interval = 0;
            endprinted = false;
        }

        //public void bindgridview()
        //{
        //    try
        //    {
        //        bool ShowPurchase = false;
        //        cmd.Connection = conn;
        //        if (ShowPurchase)
        //        {
        //            //  cmd.CommandText = "SELECT     INV_ITEM_DIRECTORY.CODE AS [Item Code], INV_ITEM_DIRECTORY_UNITS.BARCODE AS Barcode, INV_ITEM_DIRECTORY.DESC_ENG AS [Item Name],INV_ITEM_DIRECTORY.DESC_ARB, INV_ITEM_DIRECTORY_UNITS.UNIT_CODE, INV_ITEM_PRICE.PRICE AS SALES, INV_ITEM_DIRECTORY.TaxId,INV_ITEM_PRICE_1.PRICE AS PURCHASE, GEN_TAX_MASTER.TaxRate FROM         INV_ITEM_DIRECTORY LEFT OUTER JOIN  GEN_TAX_MASTER ON INV_ITEM_DIRECTORY.TaxId = GEN_TAX_MASTER.TaxId LEFT OUTER JOIN  INV_ITEM_PRICE LEFT OUTER JOIN   INV_ITEM_DIRECTORY_UNITS ON INV_ITEM_PRICE.UNIT_CODE = INV_ITEM_DIRECTORY_UNITS.UNIT_CODE AND INV_ITEM_PRICE.ITEM_CODE = INV_ITEM_DIRECTORY_UNITS.ITEM_CODE LEFT OUTER JOIN INV_ITEM_PRICE AS INV_ITEM_PRICE_1 ON INV_ITEM_DIRECTORY_UNITS.UNIT_CODE = INV_ITEM_PRICE_1.UNIT_CODE AND INV_ITEM_DIRECTORY_UNITS.ITEM_CODE = INV_ITEM_PRICE_1.ITEM_CODE ON INV_ITEM_DIRECTORY.CODE = INV_ITEM_DIRECTORY_UNITS.ITEM_CODE WHERE     (INV_ITEM_PRICE.SAL_TYPE = '" + RATE_CODE.SelectedValue + "') AND (INV_ITEM_PRICE_1.SAL_TYPE = 'PUR')";
        //            // cmd.CommandText = "SELECT        INV_ITEM_DIRECTORY.CODE AS [Item Code], INV_ITEM_DIRECTORY_UNITS.BARCODE AS Barcode, INV_ITEM_DIRECTORY.DESC_ENG AS [Item Name], INV_ITEM_DIRECTORY.DESC_ARB, INV_ITEM_DIRECTORY_UNITS.UNIT_CODE, INV_ITEM_PRICE.PRICE AS SALES, GEN_TAX_MASTER.TaxRate,  ISNULL(INV_ITEM_PRICE_1.PRICE * GEN_TAX_MASTER.TaxRate / 100 + INV_ITEM_PRICE_1.PRICE, 0) AS PURVALUEWITHTAX,INV_ITEM_DIRECTORY.TaxId FROM            INV_ITEM_DIRECTORY LEFT OUTER JOIN  GEN_TAX_MASTER ON INV_ITEM_DIRECTORY.TaxId = GEN_TAX_MASTER.TaxId LEFT OUTER JOIN  INV_ITEM_PRICE LEFT OUTER JOIN INV_ITEM_DIRECTORY_UNITS ON INV_ITEM_PRICE.UNIT_CODE = INV_ITEM_DIRECTORY_UNITS.UNIT_CODE AND  INV_ITEM_PRICE.ITEM_CODE = INV_ITEM_DIRECTORY_UNITS.ITEM_CODE LEFT OUTER JOIN INV_ITEM_PRICE AS INV_ITEM_PRICE_1 ON INV_ITEM_DIRECTORY_UNITS.UNIT_CODE = INV_ITEM_PRICE_1.UNIT_CODE AND INV_ITEM_DIRECTORY_UNITS.ITEM_CODE = INV_ITEM_PRICE_1.ITEM_CODE ON INV_ITEM_DIRECTORY.CODE = INV_ITEM_DIRECTORY_UNITS.ITEM_CODE WHERE        (INV_ITEM_PRICE.SAL_TYPE = '" + RATE_CODE.SelectedValue + "') AND (INV_ITEM_PRICE_1.SAL_TYPE = 'PUR')";

        //            cmd.CommandText = "SELECT        INV_ITEM_DIRECTORY.CODE AS [Item Code], INV_ITEM_DIRECTORY_UNITS.BARCODE AS Barcode, INV_ITEM_DIRECTORY.DESC_ENG AS [Item Name], INV_ITEM_DIRECTORY.DESC_ARB, INV_ITEM_DIRECTORY_UNITS.UNIT_CODE, GEN_TAX_MASTER.TaxRate, INV_ITEM_PRICE.PRICE AS SALES, ISNULL(INV_ITEM_PRICE_1.PRICE * GEN_TAX_MASTER.TaxRate / 100 + INV_ITEM_PRICE_1.PRICE, 0) AS PURVALUEWITHTAX, INV_ITEM_PRICE_2.PRICE AS PROMO, INV_ITEM_PRICE_3.PRICE AS WHOLESALE, INV_ITEM_PRICE_4.PRICE AS MRP, INV_ITEM_TYPE.DESC_ENG AS TYPE, INV_ITEM_CATEGORY.DESC_ENG AS CATEGORY, INV_ITEM_GROUP.DESC_ENG AS [GROUP], INV_ITEM_TM.DESC_ENG AS TRADEMARK, INV_ITEM_DIRECTORY.TaxId, INV_ITEM_DIRECTORY.HASSERIAL FROM            INV_ITEM_PRICE AS INV_ITEM_PRICE_4 LEFT OUTER JOIN INV_ITEM_PRICE AS INV_ITEM_PRICE_2 INNER JOIN INV_ITEM_DIRECTORY_UNITS ON INV_ITEM_PRICE_2.ITEM_CODE = INV_ITEM_DIRECTORY_UNITS.ITEM_CODE AND INV_ITEM_PRICE_2.UNIT_CODE = INV_ITEM_DIRECTORY_UNITS.UNIT_CODE LEFT OUTER JOIN  INV_ITEM_PRICE AS INV_ITEM_PRICE_3 ON INV_ITEM_DIRECTORY_UNITS.ITEM_CODE = INV_ITEM_PRICE_3.ITEM_CODE AND INV_ITEM_DIRECTORY_UNITS.UNIT_CODE = INV_ITEM_PRICE_3.UNIT_CODE ON INV_ITEM_PRICE_4.ITEM_CODE = INV_ITEM_DIRECTORY_UNITS.ITEM_CODE AND INV_ITEM_PRICE_4.UNIT_CODE = INV_ITEM_DIRECTORY_UNITS.UNIT_CODE RIGHT OUTER JOIN INV_ITEM_PRICE ON INV_ITEM_DIRECTORY_UNITS.UNIT_CODE = INV_ITEM_PRICE.UNIT_CODE AND INV_ITEM_DIRECTORY_UNITS.ITEM_CODE = INV_ITEM_PRICE.ITEM_CODE LEFT OUTER JOIN INV_ITEM_PRICE AS INV_ITEM_PRICE_1 ON INV_ITEM_DIRECTORY_UNITS.UNIT_CODE = INV_ITEM_PRICE_1.UNIT_CODE AND INV_ITEM_DIRECTORY_UNITS.ITEM_CODE = INV_ITEM_PRICE_1.ITEM_CODE RIGHT OUTER JOIN INV_ITEM_DIRECTORY LEFT OUTER JOIN GEN_TAX_MASTER ON INV_ITEM_DIRECTORY.TaxId = GEN_TAX_MASTER.TaxId ON INV_ITEM_DIRECTORY_UNITS.ITEM_CODE = INV_ITEM_DIRECTORY.CODE LEFT OUTER JOIN INV_ITEM_TYPE ON INV_ITEM_DIRECTORY.TYPE=INV_ITEM_TYPE.CODE LEFT  OUTER JOIN INV_ITEM_CATEGORY ON INV_ITEM_DIRECTORY.CATEGORY=INV_ITEM_CATEGORY.CODE LEFT OUTER JOIN INV_ITEM_GROUP ON INV_ITEM_DIRECTORY.[GROUP]=INV_ITEM_GROUP.CODE LEFT OUTER JOIN INV_ITEM_TM ON INV_ITEM_DIRECTORY.TRADEMARK=INV_ITEM_TM.CODE WHERE (INV_ITEM_PRICE.SAL_TYPE = 'RTL') AND (INV_ITEM_PRICE_1.SAL_TYPE = 'PUR') AND (INV_ITEM_PRICE_2.SAL_TYPE = 'PMS') AND (INV_ITEM_PRICE_3.SAL_TYPE = 'WHL') AND (INV_ITEM_PRICE_4.SAL_TYPE = 'MRP')";
        //        }
        //        else
        //        {
        //            cmd.CommandText = "SELECT     INV_ITEM_DIRECTORY.CODE AS [Item Code], INV_ITEM_DIRECTORY_UNITS.BARCODE AS Barcode, INV_ITEM_DIRECTORY.DESC_ENG AS [Item Name],INV_ITEM_DIRECTORY.DESC_ARB, INV_ITEM_DIRECTORY_UNITS.UNIT_CODE, INV_ITEM_PRICE.PRICE AS SALES, INV_ITEM_DIRECTORY.TaxId,  GEN_TAX_MASTER.TaxRate,INV_ITEM_DIRECTORY.HASSERIAL FROM         INV_ITEM_PRICE INNER JOIN  INV_ITEM_DIRECTORY_UNITS ON INV_ITEM_PRICE.UNIT_CODE = INV_ITEM_DIRECTORY_UNITS.UNIT_CODE AND INV_ITEM_PRICE.ITEM_CODE = INV_ITEM_DIRECTORY_UNITS.ITEM_CODE LEFT OUTER JOIN INV_ITEM_DIRECTORY ON INV_ITEM_DIRECTORY_UNITS.ITEM_CODE = INV_ITEM_DIRECTORY.CODE LEFT OUTER JOIN GEN_TAX_MASTER ON GEN_TAX_MASTER.TaxId = INV_ITEM_DIRECTORY.TaxId WHERE     (INV_ITEM_PRICE.SAL_TYPE = 'RTL')";

        //        }
        //        cmd.CommandType = CommandType.Text;
        //        DataTable dt = new DataTable();
        //        adapter.SelectCommand = cmd;
        //        adapter.Fill(dt);
        //        source.DataSource = dt;
        //        dataGridItem.DataSource = source;
        //        dataGridItem.RowHeadersVisible = false;
        //        dataGridItem.Columns[1].Visible = false;


        //            dataGridItem.Columns["DESC_ARB"].Visible = false;


        //            dataGridItem.Columns["TaxId"].Visible = false;
        //            dataGridItem.Columns["TaxRate"].Visible = false;


        //        dataGridItem.Columns["HASSERIAL"].Visible = false;
        //        dataGridItem.Columns["TYPE"].Visible = false;
        //        dataGridItem.Columns["CATEGORY"].Visible = false;
        //        dataGridItem.Columns["GROUP"].Visible = false;
        //        ;
        //        dataGridItem.Columns["TRADEMARK"].Visible = false;


        //        dataGridItem.ClearSelection();
        //        dataGridItem.Columns["Item Name"].Width = 250;
        //        dataGridItem.Columns["Item Code"].Width = 100;

        //    }
        //    catch (Exception EE)
        //    {
        //        // MessageBox.Show(EE.Message);
        //    }


        //}
      
        private void Barcode_Stock_Items_Load(object sender, EventArgs e)
        {
            PrintFormat.SelectedIndex = 0;
            Bind_item_name();
            Generate_Barcode();
            GetUnits();
            if (!hasBatch)
            {
                //cmd.CommandText = "SELECT CODE,DESC_ENG FROM GEN_PRICE_TYPE";
                //cmd.Connection = conn;
                //conn.Open();
                SqlDataReader r = barcodse.selectPriceType();
                while (r.Read())
                {
                    dgRates.Columns.Add(r["CODE"].ToString(), r["DESC_ENG"].ToString());
                }
                Model.DbFunctions.CloseConnection();
            }
            PnlArabic.Visible = HasArabic;
            bindgridview();
            BindBatchTable();
            pricetype();
            GetBarcodeSettings();
            Ismanual = barcodse.getManualSettings();

        }

        public void Bind_item_name()
        {
            Class.Stock_Report stkrpt = new Class.Stock_Report();
           DataTable dt = stkrpt.Bind_item_name();
            cmb_item.ValueMember = "CODE";
            cmb_item.DisplayMember = "DESC_ENG";
            DataRow row = dt.NewRow();
            row[0] = "";
            dt.Rows.InsertAt(row, 0);
            cmb_item.DataSource = dt;
        }


        public void bindgridview()
        {
            //cmd.Connection = conn;
            //cmd.CommandText = "SELECT INV_ITEM_DIRECTORY.CODE AS [ItemCode],INV_ITEM_DIRECTORY.DESC_ENG AS [Item Name],INV_ITEM_DIRECTORY.DESC_ARB as [Arab Name],  RateChange.R_Id AS rid,INV_ITEM_DIRECTORY_UNITS.UNIT_CODE, INV_ITEM_DIRECTORY.CODE AS [BarCode],RateChange.Sale_Price AS [Rate],RateChange.Price   FROM  INV_ITEM_DIRECTORY LEFT OUTER JOIN  RateChange ON INV_ITEM_DIRECTORY.CODE = RateChange.Item_code left outer join INV_ITEM_DIRECTORY_UNITS   ON INV_ITEM_DIRECTORY.CODE = INV_ITEM_DIRECTORY_UNITS.ITEM_CODE";
            ////  cmd.CommandText = "  SELECT  INV_ITEM_DIRECTORY.CODE AS [Item Code], INV_ITEM_DIRECTORY.DESC_ENG AS [Item Name], GEN_LOCATION.DESC_ENG AS Location FROM            INV_ITEM_DIRECTORY LEFT OUTER JOIN  GEN_LOCATION ON INV_ITEM_DIRECTORY.LOCATION = GEN_LOCATION.CODE LEFT OUTER JOIN  INV_ITEM_DIRECTORY_UNITS ON INV_ITEM_DIRECTORY.CODE = INV_ITEM_DIRECTORY_UNITS.ITEM_CODE ";

            DataTable dt = barcodse.selectItemDetails();

          //  adapter.SelectCommand = cmd;


          //  adapter.Fill(dt);
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                string bar = dt.Rows[j]["BarCode"].ToString() + dt.Rows[j]["rid"].ToString();
                dt.Rows[j]["BarCode"] = bar;
            }

            source.DataSource = dt;

            dataGridView1.DataSource = source;



        }
        private void Generate_Barcode()
        {
            DataTable dt = new DataTable();
            dt = barcodse.GetSettings();
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


        public void GetUnits()
        {
            try
            {
                //  conn.Open();
                //cmd.Connection = conn;
               // DataTable unitsTable = 
               // cmd.CommandText = "SELECT CODE FROM INV_UNIT";
               // adapter.SelectCommand = cmd;
                //     SqlDataAdapter daadap = new SqlDataAdapter();
                //adapter.Fill(unitsTable);
                rUnit.DataSource = barcodse.selectUnits();
                rUnit.DisplayMember = "CODE";
            }
            catch
            {
            }
        }
        private void btnCode_Click(object sender, EventArgs e)
        {
            try
            {
                ItemMasterHelp h = new ItemMasterHelp(0);
                if (h.ShowDialog() == DialogResult.OK && h.c != null)
                {
                    ID = Convert.ToString(h.c[0].Value);
                    CODE.Text = Convert.ToString(h.c[0].Value);
                    DESC_ENG.Text = Convert.ToString(h.c[1].Value);
                   
                }

                if (CODE.Text != "")
                {
                    bindBatchGrid(CODE.Text);

                    if (dgBatch.Rows.Count > 0)
                    {
                        dgBatch.Rows[0].Selected = true;
                        dgBatch.Focus();
                    }
                }


            }
            catch
            {
            }

        }


        public void AddColumnsTodgbarcodeprint()
        {
            dgbarcodeprint.Columns.Add("Item_Code", "Item Code");
            dgbarcodeprint.Columns.Add("Item_Name", "Item Name");
            dgbarcodeprint.Columns.Add("Item_Arabic_Name", "Arab Name");
            dgbarcodeprint.Columns.Add("Batch", "Batch");
            dgbarcodeprint.Columns.Add("UNIT", "Unit");
            dgbarcodeprint.Columns.Add("Barcode", "Barcode");
            dgbarcodeprint.Columns.Add("Current_Stock", "Stock");
            dgbarcodeprint.Columns.Add("Rate", PriceType);
            dgbarcodeprint.Columns.Add("dgvCopies", "No of Copies");
            dgbarcodeprint.Columns.Add("Code", "Extra Code");

        }


        public void FillPrintingGridhasnotbatch()
        {
            try
            {
                if (CODE.Text != "")
                {

                    foreach (DataGridViewRow item in dgRates.Rows)
                    {

                        dgbarcodeprint.Rows.Add();
                        try
                        {
                            dgbarcodeprint.Rows[dgbarcodeprint.Rows.Count - 2].Cells["Item_Code"].Value = CODE.Text;
                            dgbarcodeprint.Rows[dgbarcodeprint.Rows.Count - 2].Cells["Item_Name"].Value = DESC_ENG.Text;
                            dgbarcodeprint.Rows[dgbarcodeprint.Rows.Count - 2].Cells["Item_Arabic_Name"].Value = DESC_ARB.Text;
                            dgbarcodeprint.Rows[dgbarcodeprint.Rows.Count - 2].Cells["Batch"].Value = "";
                            dgbarcodeprint.Rows[dgbarcodeprint.Rows.Count - 2].Cells["UNIT"].Value = item.Cells["rUnit"].Value.ToString();
                            dgbarcodeprint.Rows[dgbarcodeprint.Rows.Count - 2].Cells["Barcode"].Value = item.Cells["rBarcode"].Value.ToString();
                            dgbarcodeprint.Rows[dgbarcodeprint.Rows.Count - 2].Cells["Current_Stock"].Value = "";

                            dgbarcodeprint.Rows[dgbarcodeprint.Rows.Count - 2].Cells["Rate"].Value = item.Cells[PriceType].Value.ToString();
                            dgbarcodeprint.Rows[dgbarcodeprint.Rows.Count - 2].Cells["Code"].Value = " ";
                        }
                        catch
                        {

                        }
                    }


                }
                else
                {
                    MessageBox.Show("Please Select an Item");
                }
            }
            catch
            {
            }
        }


        public void FillPrintingGrid(string itemcode, string name, string ara, string batch, string unit, string barcode, string stock, string rate, string code, int i)
        {
            try
            {

                // dgbarcodeprint.Rows.Add();
                try
                {

                    dgbarcodeprint.Rows.Add(itemcode, name, ara, batch, unit, barcode, stock, rate, code);
                    /*dgbarcodeprint.Rows[i].Cells["Item_Code"].Value = itemcode;
                    dgbarcodeprint.Rows[i].Cells["Item_Name"].Value = name;
                    dgbarcodeprint.Rows[i].Cells["Item_Arabic_Name"].Value = ara;
                    dgbarcodeprint.Rows[i].Cells["Batch"].Value = batch;
                    dgbarcodeprint.Rows[i].Cells["UNIT"].Value = unit;
                    dgbarcodeprint.Rows[i].Cells["Barcode"].Value = barcode;
                    dgbarcodeprint.Rows[i].Cells["Current_Stock"].Value = stock;

                    dgbarcodeprint.Rows[i].Cells["Rate"].Value = rate;
                    dgbarcodeprint.Rows[i].Cells["Code"].Value = code;*/
                }
                catch
                {
                }



            }


            catch (Exception ex)
            {
                string xc = ex.Message;
            }
        }



        private void btnadd_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (!hasBatch)
            //    {
            //        FillPrintingGridhasnotbatch();
            //    }
            //    else
            //    {
            //        FillPrintingGridhasbatch();
            //    }
            //}
            //catch
            //{
            //    MessageBox.Show("Error in fetching has batch or not batch");
            //}
            //finally
            //{
            //    CODE.Text = "";
            //    MRP.Text = "";
            //    DESC_ARB.Text = "";
            //    DESC_ENG.Text = "";
            //    dgRates.Rows.Clear();
            //}
            //dgbarcodeprint.Columns.Add("Item_Code", "Item Code");
            //dgbarcodeprint.Columns.Add("Item_Name", "Item Name");
            //dgbarcodeprint.Columns.Add("Item_Arabic_Name", "Arab Name");
            //dgbarcodeprint.Columns.Add("Batch", "Batch");
            //dgbarcodeprint.Columns.Add("UNIT", "Unit");
            //dgbarcodeprint.Columns.Add("Barcode", "Barcode");
            //dgbarcodeprint.Columns.Add("Current_Stock", "Stock");
            //dgbarcodeprint.Columns.Add("Rate", PriceType);
            //dgbarcodeprint.Columns.Add("dgvCopies", "No of Copies");
            //dgbarcodeprint.Columns.Add("Code", "Extra Code");


              int indx = Convert.ToInt32(dgBatch.CurrentRow.Index);
              string rate = "";
              if (cmbPriceType.SelectedValue.ToString() == "PUR")
              {
                  rate = dgBatch.Rows[indx].Cells["PUR"].Value.ToString();
              }
              else if (cmbPriceType.SelectedValue.ToString() == "MRP")
              {
                  rate = dgBatch.Rows[indx].Cells["MRP"].Value.ToString();
              }
              else if (cmbPriceType.SelectedValue.ToString() == "RTL")
              {
                  rate = dgBatch.Rows[indx].Cells["RTL"].Value.ToString();
              }
              else if (cmbPriceType.SelectedValue.ToString() == "WHL")
              {
                  rate = dgBatch.Rows[indx].Cells["WHL"].Value.ToString();
              }
              else
              {
                  rate = dgBatch.Rows[indx].Cells["PMS"].Value.ToString();
              }
              string Barcode="";
              if(Ismanual)
              {
                Barcode=GetBarcode(CODE.Text);
              }
              else
              {
                Barcode=dgBatch.Rows[indx].Cells["BATCH_ID"].Value.ToString();
              }
              dgbarcodeprint.Rows.Add(CODE.Text, DESC_ENG.Text, DESC_ARB.Text,"","",Barcode,"",rate,"1","");


              dgBatch.Columns.Clear();
              CODE.Clear();
              txtBarcode.Clear();
              DESC_ENG.Clear();
              GetBarcodeSettings();
              cmb_item.SelectedIndex = 0;
              cmb_item.Focus();
        }
        string GetBarcode(string ITEMCODE)
        {
            
                //conn.Open();
               // SqlCommand cmd = new SqlCommand("select barcode from INV_ITEM_DIRECTORY_UNITS WHERE ITEM_CODE=" + ITEMCODE, conn);
               // string Barcodevalue = cmd.ExecuteScalar().ToString();
                //conn.Close();b
            barcodse.ItemCode = ITEMCODE;
            return barcodse.selectItemBarcode();
         
        }
        private void FillPrintingGridhasbatch()
        {
            try
            {
                if (CODE.Text != "")
                {

                    foreach (DataGridViewRow item in dgRates.Rows)
                    {

                        dgbarcodeprint.Rows.Add();
                        try
                        {
                            dgbarcodeprint.Rows[dgbarcodeprint.Rows.Count - 2].Cells["Item_Code"].Value = CODE.Text;
                            dgbarcodeprint.Rows[dgbarcodeprint.Rows.Count - 2].Cells["Item_Name"].Value = DESC_ENG.Text;
                            dgbarcodeprint.Rows[dgbarcodeprint.Rows.Count - 2].Cells["Item_Arabic_Name"].Value = DESC_ARB.Text;
                            dgbarcodeprint.Rows[dgbarcodeprint.Rows.Count - 2].Cells["Batch"].Value = "";
                            dgbarcodeprint.Rows[dgbarcodeprint.Rows.Count - 2].Cells["UNIT"].Value = item.Cells["rUnit"].Value.ToString();
                            dgbarcodeprint.Rows[dgbarcodeprint.Rows.Count - 2].Cells["Barcode"].Value = item.Cells["rBarcode"].Value.ToString();
                            dgbarcodeprint.Rows[dgbarcodeprint.Rows.Count - 2].Cells["Current_Stock"].Value = "";
                            if (IsMRP == true)
                            {
                                dgbarcodeprint.Rows[dgbarcodeprint.Rows.Count - 2].Cells[PriceType].Value = item.Cells[PriceType].Value.ToString();
                            }
                            dgbarcodeprint.Rows[dgbarcodeprint.Rows.Count - 2].Cells["Code"].Value = "";
                        }
                        catch
                        {
                        }
                    }


                }
                else
                {
                    MessageBox.Show("Please Select an Item");
                }
            }
            catch
            {
            }
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            dgbarcodeprint.Rows.Clear();
            CODE.Text = "";
            MRP.Text = "";
            DESC_ARB.Text = "";
            DESC_ENG.Text = "";
            dgRates.Rows.Clear();


            dgBatch.Columns.Clear();
            CODE.Clear();
            txtBarcode.Clear();
            DESC_ENG.Clear();
            GetBarcodeSettings();
            cmb_item.SelectedIndex = 0;
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
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

        private void BtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                Generate_Barcode();
                if (dgbarcodeprint.Rows.Count > 0)
                {
                    //if (decTotal != 0)
                    //{
                        if (PrintFormat.Text == "A4")
                        {
                            // if (dgvBarcodePrinting.Rows[inRowIndex].Cells["dgvCopies"].Value != null)
                            //  {
                            ExportToPDF();
                            // }
                        }
                        else if (PrintFormat.Text == "Thermal Printing")
                        {
                            //   ExportToPDFforThermalPrinter();
                        }
                        else
                        {
                            MessageBox.Show("Please select printing format");
                        }
                    //}
                    //else
                    //{
                    //    MessageBox.Show("Please enter number of copies to Print", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //}
                }
                else
                {
                    MessageBox.Show("NO Data To Print", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch
            {
                MessageBox.Show("Erro! in initial export");
            }
        }

       
        public void ExportToPDF()
        {
            DataTable dt = new DataTable();
            dt = BarSettings.GetSettings();
            iTextSharp.text.Rectangle rect = PageSize.GetRectangle("A4");

            iTextSharp.text.Document pdfdoc = new iTextSharp.text.Document(new iTextSharp.text.Rectangle(rect.Width, rect.Height), 0, 0, (float)Convert.ToDouble(dt.Rows[0]["topmrgine"]), 0);

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

                for (int i = 0; i < dgbarcodeprint.Rows.Count; i++)
                {
                    if (dgbarcodeprint.Rows[i].Cells["Item_Code"].Value != null && dgbarcodeprint.Rows[i].Cells["Item_Code"].Value.ToString() != "")
                    {
                        int inCopies = 0;
                        if (dgbarcodeprint.Rows[i].Cells["dgvCopies"].Value != null)
                        {
                            int.TryParse(dgbarcodeprint.Rows[i].Cells["dgvCopies"].Value.ToString(), out inCopies); // number of copies of arcode to be printed
                        }
                        for (int j = 0; j < inCopies; j++)
                        {
                            string strProductCode = string.Empty;
                            string strCode = string.Empty;
                            string strCompanyName = string.Empty;
                            string strBarcodeValue = string.Empty;
                            string strProductName = string.Empty;
                            string RetailPrice = string.Empty;
                           
                            strProductName = dgbarcodeprint.Rows[i].Cells["Item_Name"].Value.ToString();
                            int length = strProductName.Length;
                            if (length <= Convert.ToInt32(dt.Rows[0]["ItemLength"]))
                            {
                                strProductName = dgbarcodeprint.Rows[i].Cells["Item_Name"].Value.ToString();
                            }
                            else
                            {
                                strProductName = strProductName.Substring(0, Convert.ToInt32(dt.Rows[0]["ItemLength"]));
                            }

                            if (IsBarcodeValue == true)
                            {
                                strBarcodeValue = dgbarcodeprint.Rows[i].Cells["Barcode"].Value.ToString();

                            }
                            else
                            {
                                strBarcodeValue = dgbarcodeprint.Rows[i].Cells["Barcode"].Value.ToString();
                            }

                            if (IsCompany == true)
                            {
                                strCompanyName = companyname;
                            }
                            string strMRP = string.Empty;

                            if (IsMRP == true)
                            {
                               
                                strMRP = getcurrency() + "." + dgbarcodeprint.Rows[i].Cells["Rate"].Value.ToString();
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
                            code128.Baseline = 7;
                            code128.TextAlignment = Element.ALIGN_CENTER;
                            iTextSharp.text.Image image128 = code128.CreateImageWithBarcode(pdfcb, null, null);
                            iTextSharp.text.Font smallfont = FontFactory.GetFont("Book Antiqua", (float)Convert.ToDouble(dt.Rows[0]["fontSize"]));
                            Phrase phrase = new Phrase();
                            phrase.Font.Size = 7f;
                            if (IsCompany == true)
                            {
                                phrase.Add(new Chunk(companyname + Environment.NewLine, smallfont));
                            }
                            else
                            {
                                phrase.Add(new Chunk("" + Environment.NewLine));
                            }

                            phrase.Add(new Chunk(strProductName + Environment.NewLine));
                            phrase.Add(new Chunk(Environment.NewLine));
                            phrase.Add(new Chunk(image128, 0, 0));
                            //     phrase.Add(new Chunk(Environment.NewLine));
                            if (IsMRP == true)
                            {
                                phrase.Add(new Chunk(Environment.NewLine + strMRP));
                            }
                            if (IsProductCode == true)
                            {

                                phrase.Add(new Chunk(Environment.NewLine + strBarcodeValue,smallfont));

                            }
                            else
                            {
                                phrase.Add(new Chunk(Environment.NewLine +""));
                            }
                            PdfPCell cell = new PdfPCell(phrase);
                          

                            cell.PaddingRight = -10;
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                           // cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                            if (!Convert.ToBoolean(dt.Rows[0]["IsBoarder"]))
                                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                            phrase.Add(new Chunk(Environment.NewLine));
                            // phrase.Add(new Chunk(Environment.NewLine));
                            cell.PaddingRight = 3;
                            cell.FixedHeight = (float)Convert.ToDouble(dt.Rows[0]["cellheight"]);
                            cell.BorderColor = new iTextSharp.text.Color(150);
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
                    pdfdoc.SetMargins(0, 0,(float)Convert.ToDouble(dt.Rows[0]["topmrgine"]), 0);
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
        string getcurrency()
        {
            //if (conn.State == ConnectionState.Closed)
            //    conn.Open();
            //string cur = "";
            //cmd = new SqlCommand("select DEFAULT_CURRENCY_CODE from gen_branch", conn);
            //cur = cmd.ExecuteScalar().ToString();
            //conn.Close();
            return barcodse.selectCurrencyCode();
        }
        private void dgbarcodeprint_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1 && e.ColumnIndex > -1)
                {
                    if (dgbarcodeprint.Columns[e.ColumnIndex].Name == "dgvCopies")
                    {
                        if (!dgbarcodeprint.Rows[e.RowIndex].Cells["dgvCopies"].ReadOnly && dgbarcodeprint.Rows[e.RowIndex].Cells["dgvCopies"].Value != null && dgbarcodeprint.Rows[e.RowIndex].Cells["dgvCopies"].Value.ToString() != "")
                        {
                            TotalCountOfCopies();
                        }
                        else
                        {
                            dgbarcodeprint.Rows[e.RowIndex].Cells["dgvCopies"].Value = 0;
                            TotalCountOfCopies();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void TotalCountOfCopies()
        {
            try
            {
                decTotal = 0;
                foreach (DataGridViewRow dgvrow in dgbarcodeprint.Rows)
                {
                    if (dgvrow.Cells["dgvCopies"].Value != null)
                    {
                        if (dgvrow.Cells["dgvCopies"].Value.ToString() != string.Empty)
                        {
                            decTotal = decTotal + Convert.ToDecimal(dgvrow.Cells["dgvCopies"].Value.ToString());
                            decTotal = Math.Round(decTotal, 0);
                            lblTotalCountValue.Text = decTotal.ToString();
                        }
                    } 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("BCP3:" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnexportexcel_Click(object sender, EventArgs e)
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
                for (int i = 1; i < dgbarcodeprint.Columns.Count + 1; i++)
                {
                    worksheet.Cells[1, i] = dgbarcodeprint.Columns[i - 1].HeaderText;
                }



                // storing Each row and column value to excel sheet
                for (int i = 0; i < dgbarcodeprint.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < dgbarcodeprint.Columns.Count - 1; j++)
                    {
                        worksheet.Cells[i + 2, j + 1] = dgbarcodeprint.Rows[i].Cells[j].Value.ToString();
                    }
                }


                // save the application
                workbook.SaveAs("c:\\output.xls", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                // Exit from the application
                app.Quit();

            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }


        public void converttopedf()
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
                pdfdoc = new Document(PageSize.A4, 12, 1, 20, 20);

                PdfWriter writer = PdfWriter.GetInstance(pdfdoc, new FileStream(Application.StartupPath + "\\Barcode\\Barcode.pdf", FileMode.Create));
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

                for (int i = 0; i < dgbarcodeprint.Rows.Count; i++)
                {
                    if (dgbarcodeprint.Rows[i].Cells["Item_Code"].Value != null && dgbarcodeprint.Rows[i].Cells["Item_Code"].Value.ToString() != "")
                    {
                        int inCopies = 0;
                        if (dgbarcodeprint.Rows[i].Cells["dgvCopies"].Value != null)
                        {
                            int.TryParse(dgbarcodeprint.Rows[i].Cells["dgvCopies"].Value.ToString(), out inCopies); // number of copies of arcode to be printed
                        }
                        for (int j = 0; j < inCopies; j++)
                        {
                            string strProductCode = string.Empty;
                            string strCode = string.Empty;
                            string strCompanyName = string.Empty;
                            string strBarcodeValue = string.Empty;
                            if (IsProductCode == true)
                            {
                                strCode = dgbarcodeprint.Rows[i].Cells["Item_Code"].Value.ToString();

                                if (IsProductCode == true)
                                    strProductCode = strCode;
                            }
                            else
                            {
                                strProductCode = dgbarcodeprint.Rows[i].Cells["Item_Name"].Value.ToString();
                            }

                            if (IsBarcodeValue == true)
                            {
                                strBarcodeValue = dgbarcodeprint.Rows[i].Cells["Barcode"].Value.ToString();

                            }
                            else
                            {
                                strBarcodeValue = dgbarcodeprint.Rows[i].Cells["Barcode"].Value.ToString();
                            }

                            if (IsCompany == true)
                                strCompanyName = companyname;
                            string strMRP = string.Empty;
                            if (IsMRP == true)
                            {
                                strMRP = "MRP:" + dgbarcodeprint.Rows[i].Cells["MRP"].Value.ToString();
                            }
                            string strSecretPurchaseRateCode = string.Empty;

                            PdfContentByte pdfcb = writer.DirectContent;
                            Barcode128 code128 = new Barcode128();
                            code128.Code = strBarcodeValue;
                            code128.Extended = false;
                            code128.CodeType = iTextSharp.text.pdf.Barcode.CODE128;
                            if (IsBarcodeValue == false)
                            {
                                code128.Font = null;
                                code128.AltText = strBarcodeValue;
                            }
                            code128.BarHeight = HEIGHT;

                            code128.Size = 6;
                            code128.Baseline = 8;
                            code128.TextAlignment = Element.ALIGN_CENTER;
                            iTextSharp.text.Image image128 = code128.CreateImageWithBarcode(pdfcb, null, null);
                            Phrase phrase = new Phrase();
                            phrase.Font.Size = 8;
                            if (IsCompany == true)
                            {
                                phrase.Add(new Chunk(companyname + Environment.NewLine + Environment.NewLine));
                            }


                            PdfPCell cell = new PdfPCell(phrase);
                            cell.FixedHeight = 61.69f;

                            cell.PaddingRight = -10;
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cell.Border = iTextSharp.text.Rectangle.NO_BORDER;

                            phrase.Add(new Chunk(image128, 0, 0));
                            if (IsMRP == true)
                            {
                                phrase.Add(new Chunk(Environment.NewLine + strMRP));
                            }
                            if (IsProductCode == true)
                            {
                                phrase.Add(new Chunk(Environment.NewLine + strCode));
                            }

                            tbl.AddCell(cell);

                            intotalCount++;
                        }


                    }
                }
                int reminder = intotalCount % 5;
                if (reminder != 0)
                {
                    for (int i = reminder; i < 5; ++i)
                    {
                        tbl.AddCell("");
                    }
                }
                if (tbl.Rows.Count != 0)
                {
                    pdfdoc.Add(tbl);
                    pdfdoc.Close();
                    //System.Diagnostics.Process.Start(Application.StartupPath + "\\Barcode\\Barcode.pdf");
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
        public void converttoexcel()
        {


            //CSVExtractor extractor = new CSVExtractor();
            //extractor.RegistrationName = "demo";
            //extractor.RegistrationKey = "demo";

            //// Load sample PDF document
            //extractor.LoadDocumentFromFile("sample3.pdf");

            ////extractor.CSVSeparatorSymbol = ","; // you can change CSV separator symbol (if needed) from "," symbol to another if needed for non-US locales

            //extractor.SaveCSVToFile("output.csv");

            //Console.WriteLine();
            //Console.WriteLine("Data has been extracted to 'output.csv' file.");
            //Console.WriteLine();
            //Console.WriteLine("Press any key to continue and open CSV in default CSV viewer (or Excel)...");
            //Console.ReadKey();

            //Process.Start("output.csv");








        }

        Class.BarcodeSettings BarSettings = new Class.BarcodeSettings();
        public void pricetype()
        {
            DataTable dt = new DataTable();
            dt = BarSettings.GetPricetype();


            cmbPriceType.DataSource = dt;
            cmbPriceType.DisplayMember = "value";
            cmbPriceType.ValueMember = "key";
        }
        public void GetBarcodeSettings()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = BarSettings.GetSettings();
                if (dt.Rows.Count > 0)
                {

                    if (dt.Rows[0][9].ToString() != "")
                    {
                        string str = dt.Rows[0][9].ToString();
                        cmbPriceType.SelectedValue = str;
                    }
                }
            }
            catch
            {

            }
        }
        private void kryptonButton1_Click_1(object sender, EventArgs e)
        {
            Barcode_Settings bsettinges = new Barcode_Settings();
            bsettinges.ShowDialog();
        }

        private void CODE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (CODE.Text == "")
                {
                    btnCode.PerformClick();
                }
                else
                {
                    btnadd.Focus();
                }
            }
        }

        public int TotalCopies()
        {
            int a = 0;
            try
            {
                for (int i = 0; i < dgbarcodeprint.Rows.Count; i++)
                {
                    a = a + Convert.ToInt32(dgbarcodeprint.Rows[i].Cells["dgvCopies"].Value);
                }
                return a;
            }
            catch
            {
                return 0;
            }

        }

        public void SpoolerRestart()
        {
            try
            {
                ServiceController scController = new ServiceController();
                scController.ServiceName = "Spooler";
                scController.MachineName = SystemInformation.ComputerName;
                scController.Stop();
                string sStatus = scController.Status.ToString();
                MessageBox.Show(sStatus);

                scController.Start();
                sStatus = scController.Status.ToString();
                MessageBox.Show(sStatus);
            }
            catch (Exception Ex)
            {
                //   MessageBox.Show(Ex.Message);
            }
        }

        private void btnBarcodePrint_Click(object sender, EventArgs e)
        {
            try
            {

                if (dgbarcodeprint.Rows.Count > 0)
                {
                    if (decTotal != 0)
                    {
                        if (!rdLabel.Checked)
                        {
                            WritingContentstoFile();
                            string computername = System.Windows.Forms.SystemInformation.ComputerName;
                            System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo("cmd.exe", "/c copy / b file11.prn \\\\" + computername + "\\barcode");
                            //    System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo("cmd.exe", "/c copy / b file11.prn \\\\" + computername + "\\barcode");
                            //   MessageBox.Show("copy /b file11.prn \\\\"+computername+"\\barcode");
                            System.Diagnostics.Process p = new System.Diagnostics.Process();
                            p.StartInfo = info;
                            p.Start();
                            SpoolerRestart();
                            ResetAll();
                        }
                        else
                        {
                            WritingContentstoFile_Label();
                            string computername = System.Windows.Forms.SystemInformation.ComputerName;
                            System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo("cmd.exe", "/c copy /b file11.prn \\\\" + computername + "\\barcode");
                            //   MessageBox.Show("copy /b file11.prn \\\\"+computername+"\\barcode");
                            System.Diagnostics.Process p = new System.Diagnostics.Process();
                            p.StartInfo = info;
                            p.Start();
                            SpoolerRestart();
                            ResetAll();
                        }

                    }
                    else
                    {
                        MessageBox.Show("Please enter number of copies to Print", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("NO Data To Print", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }





            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }

        }

        int printed = 0;
        int x1 = 711;
        int x2 = 702;
        int x3 = 667;

        int x4 = 703;
        int x5 = 617;

        int interval;
        bool endprinted = false;
        public void WritingContentstoFile()
        {
            try
            {
                Totalnocopies = TotalCopies();
                PrnContent[0] = "<xpml><page quantity='0' pitch='20.0 mm'></xpml>SIZE 92";
                PrnContent[1] = "mm, 20 mm";
                PrnContent[2] = "DIRECTION 0,0";
                PrnContent[3] = "REFERENCE 0,0";
                PrnContent[4] = "OFFSET 0 mm";
                PrnContent[5] = "SET PEEL OFF";
                PrnContent[6] = "SET CUTTER OFF";
                PrnContent[7] = "SET PARTIAL_CUTTER OFF";
                PrnContent[8] = "<xpml></page></xpml><xpml><page quantity='1' pitch='20.0";
                PrnContent[9] = "mm'></xpml>SET TEAR ON";
                PrnContent[10] = "CLS";
                PrnContent[11] = "CODEPAGE 1252";
                //  StreamReader sr = new StreamReader();
                TextWriter sw = new StreamWriter(@"file11.prn"); //true for append
                int i = 0;
                sw.WriteLine(PrnContent[0]);
                sw.WriteLine(PrnContent[1]);
                sw.WriteLine(PrnContent[2]);
                sw.WriteLine(PrnContent[3]);
                sw.WriteLine(PrnContent[4]);
                sw.WriteLine(PrnContent[5]);
                sw.WriteLine(PrnContent[6]);
                sw.WriteLine(PrnContent[7]);
                sw.WriteLine(PrnContent[8]);
                sw.WriteLine(PrnContent[9]);
                sw.WriteLine(PrnContent[10]);
                sw.WriteLine(PrnContent[11]);


                interval = 0;
                for (int row = 0; row < dgbarcodeprint.Rows.Count - 1; row++)
                {
                    interval = printed;
                    noofcopies = printed + Convert.ToInt32(dgbarcodeprint.Rows[row].Cells["dgvCopies"].Value);
                    //  interval = 0;
                    for (printed = interval; printed < noofcopies; printed++)
                    {
                        bool split = false;
                        string a = "", b = "";

                        if (dgbarcodeprint.Rows[row].Cells["Item_Name"].Value.ToString().Length > 15)
                        {
                            try
                            {
                                a = dgbarcodeprint.Rows[row].Cells["Item_Name"].Value.ToString().Substring(0, 15);
                                b = dgbarcodeprint.Rows[row].Cells["Item_Name"].Value.ToString().Substring(15, dgbarcodeprint.Rows[row].Cells["Item_Name"].Value.ToString().Length - 15);
                                split = true;
                            }
                            catch (Exception ee)
                            {
                                MessageBox.Show(ee.Message + " , error in reading name");
                            }
                        }
                        else
                        {
                            a = dgbarcodeprint.Rows[row].Cells["Item_Name"].Value.ToString();
                        }
                        //  string name = row.Cells[1].Value.ToString().Length <= 18 ? row.Cells[1].Value.ToString().PadRight(25) : row.Cells[1].Value.ToString().Substring(0, 18).PadRight(25);

                        sw.WriteLine("TEXT " + x1.ToString() + ",147,\"ROMAN.TTF\", 180, 1, 8, \"" + a + "\"");
                        if (split)
                        {
                            sw.WriteLine("TEXT " + x1.ToString() + ",120,\"ROMAN.TTF\",180,1,8,\"" + b + "\"");
                        }
                        split = false;
                        sw.WriteLine("BARCODE " + x2 + ",93,\"128M\",34,0,180,2,4,\"!105" + dgbarcodeprint.Rows[row].Cells["Barcode"].Value.ToString() + "\"");
                        sw.WriteLine("TEXT " + x3 + ",53,\"ROMAN.TTF\",180,1,8,\"" + dgbarcodeprint.Rows[row].Cells["Barcode"].Value.ToString() + "\"");
                        sw.WriteLine("TEXT " + x4 + ",25,\"ROMAN.TTF\",180,1,7,\"PRP: \"");
                        sw.WriteLine("TEXT " + x5 + ",24,\"ROMAN.TTF\",180,1,7,\"" + dgbarcodeprint.Rows[row].Cells["Rate"].Value.ToString() + "\"");
                        x1 = x1 - 256;
                        x2 = x2 - 256;
                        x3 = x3 - 256;
                        x4 = x4 - 256;
                        x5 = x5 - 256;
                        endprinted = false;
                        if (printed == 2)
                        {

                            noofcopies = noofcopies - 3;
                            if (noofcopies > 0)
                            {
                                sw.WriteLine("PRINT 1,1");
                                sw.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='20.0 mm'></xpml>CLS CODEPAGE 1252");

                                interval = -1;
                                printed = interval;
                                x1 = 711;
                                x2 = 702;
                                x3 = 667;
                                x4 = 703;
                                x5 = 617;
                                endprinted = true;
                            }
                            else
                            {
                                try
                                {
                                    if (Convert.ToInt32(dgbarcodeprint.Rows[row + 1].Cells["dgvCopies"].Value) > 0)
                                    {
                                        sw.WriteLine("PRINT 1,1");
                                        sw.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='20.0 mm'></xpml>CLS CODEPAGE 1252");
                                        interval = -1;
                                        printed = interval;
                                        x1 = 711;
                                        x2 = 702;
                                        x3 = 667;
                                        x4 = 703;
                                        x5 = 617;
                                        endprinted = true;
                                    }
                                    else
                                    {
                                    }
                                }
                                catch
                                {
                                }
                            }
                        }
                        else
                        {

                        }
                    }
                    //  sw.WriteLine("PRINT 1,1");
                    //   balancecopies = noofcopies - 3;
                }
                if (!endprinted)
                {
                    sw.WriteLine("PRINT 1,1");
                }


                //balancecopies= Totalnocopies - noofcopies;
                //sw.WriteLine("PRINT 1,"+ GetBarcodeRowRepeatMode(noofcopies));

                //if (Reminder > 0)
                //{
                //    sw.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='20.0 mm'></xpml>CLS CODEPAGE 1252");
                //}

                //TEXT 617,24,"ROMAN.TTF",180,1,7,"00 "
                //    sw.WriteLine(dgRates.Rows[row].Cellss[col].Value.ToString());


                sw.WriteLine("<xpml></page></xpml><xpml><end/></xpml>");

                sw.Close();
                // record seperator could be written here.

                //while (i < 1000)
                //{

                //    sw.WriteLine(sr.ReadLine());
                //    i++;
                //}
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }



        int y1 = 729;
        int y2 = 716;
        int y3 = 673;

        int y4 = 649;
        int y5 = 563;

        public void WritingContentstoFile_Label()
        {
            try
            {
                Totalnocopies = TotalCopies();
                PrnContent[0] = "<xpml><page quantity='0' pitch='20.0 mm'></xpml>SIZE 92";
                PrnContent[1] = "mm, 20 mm";
                PrnContent[2] = "DIRECTION 0,0";
                PrnContent[3] = "REFERENCE 0,0";
                PrnContent[4] = "OFFSET 0 mm";
                PrnContent[5] = "SET PEEL OFF";
                PrnContent[6] = "SET CUTTER OFF";
                PrnContent[7] = "SET PARTIAL_CUTTER OFF";
                PrnContent[8] = "<xpml></page></xpml><xpml><page quantity='1' pitch='20.0";
                PrnContent[9] = "mm'></xpml>SET TEAR ON";
                PrnContent[10] = "CLS";
                PrnContent[11] = "CODEPAGE 1252";
                //  StreamReader sr = new StreamReader();
                TextWriter sw = new StreamWriter(@"file11.prn"); //true for append
                int i = 0;
                sw.WriteLine(PrnContent[0]);
                sw.WriteLine(PrnContent[1]);
                sw.WriteLine(PrnContent[2]);
                sw.WriteLine(PrnContent[3]);
                sw.WriteLine(PrnContent[4]);
                sw.WriteLine(PrnContent[5]);
                sw.WriteLine(PrnContent[6]);
                sw.WriteLine(PrnContent[7]);
                sw.WriteLine(PrnContent[8]);
                sw.WriteLine(PrnContent[9]);
                sw.WriteLine(PrnContent[10]);
                sw.WriteLine(PrnContent[11]);
                //  StreamReader sr = new StreamReader();


                interval = 0;
                for (int row = 0; row < dgbarcodeprint.Rows.Count - 1; row++)
                {
                    interval = printed;
                    noofcopies = printed + Convert.ToInt32(dgbarcodeprint.Rows[row].Cells["dgvCopies"].Value);
                    //  interval = 0;
                    for (printed = interval; printed < noofcopies; printed++)
                    {
                        bool split = false;
                        string a = "", b = "";

                        if (dgbarcodeprint.Rows[row].Cells["Item_Name"].Value.ToString().Length > 15)
                        {
                            try
                            {
                                a = dgbarcodeprint.Rows[row].Cells["Item_Name"].Value.ToString().Substring(0, 15);
                                b = dgbarcodeprint.Rows[row].Cells["Item_Name"].Value.ToString().Substring(15, dgbarcodeprint.Rows[row].Cells["Item_Name"].Value.ToString().Length - 15);
                                split = true;
                            }
                            catch (Exception ee)
                            {
                                MessageBox.Show(ee.Message + " , error in reading name");
                            }
                        }
                        else
                        {
                            a = dgbarcodeprint.Rows[row].Cells["Item_Name"].Value.ToString();
                        }
                        //  string name = row.Cells[1].Value.ToString().Length <= 18 ? row.Cells[1].Value.ToString().PadRight(25) : row.Cells[1].Value.ToString().Substring(0, 18).PadRight(25);

                        sw.WriteLine("TEXT " + y1.ToString() + ",147,\"ROMAN.TTF\", 180, 1, 7, \"" + a + "\"");
                        if (split)
                        {
                            sw.WriteLine("TEXT " + y1.ToString() + ",124,\"ROMAN.TTF\",180,1,7,\"" + b + "\"");
                        }
                        split = false;
                        sw.WriteLine("TEXT " + y2 + ",35,\"ROMAN.TTF\",180,1,7,\"SR. \"");
                        sw.WriteLine("TEXT " + y3 + ",35,\"ROMAN.TTF\",180,1,7,\"" + dgbarcodeprint.Rows[row].Cells["Rate"].Value.ToString() + "\"");
                        sw.WriteLine("TEXT " + y4 + ",73,\"ROMAN.TTF\",180,1,7,\"" + dgbarcodeprint.Rows[row].Cells["Barcode"].Value.ToString() + "\"");
                        sw.WriteLine("TEXT " + y5 + ",34,\"ROMAN.TTF\",180,1,7,\"" + dgbarcodeprint.Rows[row].Cells["Code"].Value.ToString() + "\"");
                        y1 = y1 - 256;
                        y2 = y2 - 256;
                        y3 = y3 - 256;
                        y4 = y4 - 256;
                        y5 = y5 - 256;
                        endprinted = false;
                        if (printed == 2)
                        {

                            noofcopies = noofcopies - 3;
                            if (noofcopies > 0)
                            {
                                sw.WriteLine("PRINT 1,1");
                                sw.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='20.0 mm'></xpml>CLS CODEPAGE 1252");

                                interval = -1;
                                printed = interval;

                                y1 = 729;
                                y2 = 716;
                                y3 = 673;

                                y4 = 649;
                                y5 = 563;
                                endprinted = true;
                            }
                        }
                        else
                        {

                        }
                    }
                    //  sw.WriteLine("PRINT 1,1");
                    //   balancecopies = noofcopies - 3;
                }
                if (!endprinted)
                {
                    sw.WriteLine("PRINT 1,1");
                }


                //balancecopies= Totalnocopies - noofcopies;
                //sw.WriteLine("PRINT 1,"+ GetBarcodeRowRepeatMode(noofcopies));

                //if (Reminder > 0)
                //{
                //    sw.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='20.0 mm'></xpml>CLS CODEPAGE 1252");
                //}

                //TEXT 617,24,"ROMAN.TTF",180,1,7,"00 "
                //    sw.WriteLine(dgRates.Rows[row].Cellss[col].Value.ToString());


                sw.WriteLine("<xpml></page></xpml><xpml><end/></xpml>");

                sw.Close();
                // record seperator could be written here.

                //while (i < 1000)
                //{

                //    sw.WriteLine(sr.ReadLine());
                //    i++;
                //}
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }


        public void WritngContentstoFileZebra()
        {
            try
            {
                Totalnocopies = TotalCopies();
                PrnContent[0] = "<xpml><page quantity='0' pitch='20.0 mm'></xpml>SIZE 92 mm, 20 mm DIRECTION 0,0 REFERENCE 0,0 OFFSET 0 mm SET PEEL OFF SET CUTTER OFF <xpml></page></xpml><xpml><page quantity='1' pitch='20.0 mm'></xpml>SET TEAR ON CLS CODEPAGE 1252";
                //  StreamReader sr = new StreamReader();
                TextWriter sw = new StreamWriter(@"file11.prn"); //true for append
                int i = 0;
                sw.WriteLine(PrnContent[0]);

                interval = 0;
                for (int row = 0; row < dgbarcodeprint.Rows.Count - 1; row++)
                {
                    //interval = printed;
                    //noofcopies = printed + Convert.ToInt32(dgbarcodeprint.Rows[row].Cells["dgvCopies"].Value);
                    ////  interval = 0;
                    //for (printed = interval; printed < noofcopies; printed++)
                    //{
                    bool split = false;
                    string a = "", b = "";

                    if (dgbarcodeprint.Rows[row].Cells["Item_Name"].Value.ToString().Length > 15)
                    {
                        try
                        {
                            a = dgbarcodeprint.Rows[row].Cells["Item_Name"].Value.ToString().Substring(0, 15);
                            b = dgbarcodeprint.Rows[row].Cells["Item_Name"].Value.ToString().Substring(15, dgbarcodeprint.Rows[row].Cells["Item_Name"].Value.ToString().Length - 15);
                            split = true;
                        }
                        catch (Exception ee)
                        {
                            MessageBox.Show(ee.Message + " , error in reading name");
                        }
                    }
                    else
                    {
                        a = dgbarcodeprint.Rows[row].Cells["Item_Name"].Value.ToString();
                    }
                    //  string name = row.Cells[1].Value.ToString().Length <= 18 ? row.Cells[1].Value.ToString().PadRight(25) : row.Cells[1].Value.ToString().Substring(0, 18).PadRight(25);

                    sw.WriteLine("TEXT " + x1.ToString() + ",147,\"ROMAN.TTF\", 180, 1, 8, \"" + a + "\"");
                    //if (split)
                    //{
                    //    sw.WriteLine("TEXT " + x1.ToString() + ",120,\"ROMAN.TTF\",180,1,8,\"" + b + "\"");
                    //}
                    split = false;
                    sw.WriteLine("BARCODE " + x2 + ",93,\"128M\",34,0,180,2,4,\"!105" + dgbarcodeprint.Rows[row].Cells["Barcode"].Value.ToString() + "\"");
                    sw.WriteLine("TEXT " + x3 + ",53,\"ROMAN.TTF\",180,1,8,\"" + dgbarcodeprint.Rows[row].Cells["Barcode"].Value.ToString() + "\"");
                    sw.WriteLine("TEXT " + x4 + ",25,\"ROMAN.TTF\",180,1,7,\"PRP: RS. \"");
                    sw.WriteLine("TEXT " + x5 + ",24,\"ROMAN.TTF\",180,1,7,\"" + dgbarcodeprint.Rows[row].Cells["Rate"].Value.ToString() + "\"");
                    x1 = x1 - 256;
                    x2 = x2 - 256;
                    x3 = x3 - 256;
                    x4 = x4 - 256;
                    x5 = x5 - 256;
                    endprinted = false;
                    if (printed == 2)
                    {

                        noofcopies = noofcopies - 3;
                        if (noofcopies > 0)
                        {
                            sw.WriteLine("PRINT 1,1");
                            sw.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='20.0 mm'></xpml>CLS CODEPAGE 1252");

                            interval = -1;
                            printed = interval;
                            x1 = 711;
                            x2 = 702;
                            x3 = 667;
                            x4 = 703;
                            x5 = 617;
                            endprinted = true;
                        }
                    }
                    else
                    {

                    }
                    //  }
                    //  sw.WriteLine("PRINT 1,1");
                    //   balancecopies = noofcopies - 3;
                }
                if (!endprinted)
                {
                    sw.WriteLine("PRINT 1,1");
                }


                //balancecopies= Totalnocopies - noofcopies;
                //sw.WriteLine("PRINT 1,"+ GetBarcodeRowRepeatMode(noofcopies));

                //if (Reminder > 0)
                //{
                //    sw.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='20.0 mm'></xpml>CLS CODEPAGE 1252");
                //}

                //TEXT 617,24,"ROMAN.TTF",180,1,7,"00 "
                //    sw.WriteLine(dgRates.Rows[row].Cellss[col].Value.ToString());


                sw.WriteLine("<xpml></page></xpml><xpml><end/></xpml>");

                sw.Close();
                // record seperator could be written here.

                //while (i < 1000)
                //{

                //    sw.WriteLine(sr.ReadLine());
                //    i++;
                //}
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }


        int TotalRepeatMode = 0;
        public int GetBarcodeRowRepeatMode(int noofcopies)
        {
            try
            {
                Reminder = noofcopies % 3;
                TotalRepeatMode = Reminder + (Convert.ToInt32(noofcopies / 3));
                return Convert.ToInt32(noofcopies / 3);

            }
            catch
            {
                return 1;
            }
        }

        private void DESC_ENG_TextChanged(object sender, EventArgs e)
        {

        }

        private void DESC_ENG_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void CODE_TextChanged(object sender, EventArgs e)
        {

        }

        private void DESC_ENG_TextChanged_1(object sender, EventArgs e)
        {
            //if (CODE.Text == "" && DESC_ENG.Text == "")
            //{
            //    dataGridView1.Visible = false;
            //    dataGridView1.Columns["rid"].Visible = false;
            //}
            //else
            //{
            //    dataGridView1.Columns["rid"].Visible = false;
            //    dataGridView1.Visible = true;
            //    source.Filter = string.Format("[BarCode] LIKE '%{0}%' and [Item Name] like '%{1}%'", CODE.Text, DESC_ENG.Text);
            //}
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {



        }
        private void moveUp()
        {
            if (dataGridView1.RowCount > 0)
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    int rowCount = dataGridView1.Rows.Count;
                    int index = dataGridView1.SelectedCells[0].OwningRow.Index;

                    if (index == 0)
                    {
                        return;
                    }
                    DataGridViewRowCollection rows = dataGridView1.Rows;

                    // remove the previous row and add it behind the selected row.
                    DataGridViewRow prevRow = rows[index - 1];
                    rows.Remove(prevRow);
                    prevRow.Frozen = false;
                    rows.Insert(index, prevRow);
                    dataGridView1.ClearSelection();
                    dataGridView1.Rows[index - 1].Selected = true;
                }
            }
        }

        private void moveDown()
        {
            if (dataGridView1.RowCount > 0)
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    int rowCount = dataGridView1.Rows.Count;
                    int index = dataGridView1.SelectedCells[0].OwningRow.Index;

                    if (index == (rowCount - 2)) // include the header row
                    {
                        return;
                    }
                    DataGridViewRowCollection rows = dataGridView1.Rows;

                    // remove the next row and add it in front of the selected row.
                    DataGridViewRow nextRow = rows[index + 1];
                    rows.Remove(nextRow);
                    nextRow.Frozen = false;
                    rows.Insert(index, nextRow);
                    dataGridView1.ClearSelection();
                    dataGridView1.Rows[index + 1].Selected = true;
                }
            }
        }
        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == (Keys.Up))
                {

                    moveUp();

                }
                if (e.KeyData == (Keys.Down))
                {
                    moveDown();

                }
                e.Handled = true;
                if (e.KeyData == Keys.Enter)
                {

                    dgbarcodeprint.Rows.Add();
                    try
                    {
                        dgbarcodeprint.Rows[dgbarcodeprint.Rows.Count - 2].Cells["Item_Code"].Value = dataGridView1.CurrentRow.Cells["ItemCode"].Value.ToString();
                        dgbarcodeprint.Rows[dgbarcodeprint.Rows.Count - 2].Cells["Item_Name"].Value = dataGridView1.CurrentRow.Cells["Item Name"].Value.ToString();
                        dgbarcodeprint.Rows[dgbarcodeprint.Rows.Count - 2].Cells["Item_Arabic_Name"].Value = dataGridView1.CurrentRow.Cells["Arab Name"].Value.ToString();
                        dgbarcodeprint.Rows[dgbarcodeprint.Rows.Count - 2].Cells["Batch"].Value = "";
                        dgbarcodeprint.Rows[dgbarcodeprint.Rows.Count - 2].Cells["UNIT"].Value = dataGridView1.CurrentRow.Cells["UNIT_CODE"].Value.ToString();
                        dgbarcodeprint.Rows[dgbarcodeprint.Rows.Count - 2].Cells["Barcode"].Value = dataGridView1.CurrentRow.Cells["BarCode"].Value.ToString();
                        dgbarcodeprint.Rows[dgbarcodeprint.Rows.Count - 2].Cells["Current_Stock"].Value = "";
                        dgbarcodeprint.Rows[dgbarcodeprint.Rows.Count - 2].Cells["Rate"].Value = dataGridView1.CurrentRow.Cells["Rate"].Value.ToString();
                        dgbarcodeprint.Rows[dgbarcodeprint.Rows.Count - 2].Cells["Code"].Value = " ";
                        //dgbarcodeprint.Rows[dgbarcodeprint.Rows.Count - 2].Cells["Item_Name"].Value = DESC_ENG.Text;
                        //dgbarcodeprint.Rows[dgbarcodeprint.Rows.Count - 2].Cells["Item_Arabic_Name"].Value = DESC_ARB.Text;
                        //dgbarcodeprint.Rows[dgbarcodeprint.Rows.Count - 2].Cells["Batch"].Value = "";
                        //dgbarcodeprint.Rows[dgbarcodeprint.Rows.Count - 2].Cells["UNIT"].Value = item.Cells["rUnit"].Value.ToString();
                        //dgbarcodeprint.Rows[dgbarcodeprint.Rows.Count - 2].Cells["Barcode"].Value = item.Cells["rBarcode"].Value.ToString();
                        //dgbarcodeprint.Rows[dgbarcodeprint.Rows.Count - 2].Cells["Current_Stock"].Value = "";

                        //dgbarcodeprint.Rows[dgbarcodeprint.Rows.Count - 2].Cells["Rate"].Value = item.Cells[PriceType].Value.ToString();
                        //dgbarcodeprint.Rows[dgbarcodeprint.Rows.Count - 2].Cells["Code"].Value = " ";
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

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            foreach (DataGridViewCell cell in dataGridView1.Rows[e.RowIndex].Cells)
            {
                if (cell.GetType() == typeof(DataGridViewImageCell))
                {
                    cell.Value = DBNull.Value;
                }
            }
        }

        private void DESC_ENG_KeyDown_1(object sender, KeyEventArgs e)
        {

            dataGridView1.Visible = true;
            if (e.KeyData == Keys.Down)
            {
                if (dataGridView1.Visible == true)
                {
                    dataGridView1.Focus();
                    dataGridView1.CurrentCell = dataGridView1.Rows[1].Cells[1];

                }

            }
            else if (e.KeyData == Keys.Escape)
            {
                dataGridView1.Visible = false;

                DESC_ENG.Text = "";
            }


        }

        private void linkRemoveRecord_LinkClicked(object sender, EventArgs e)
        {
            try
            {
                if (dgbarcodeprint.Rows.Count > 0 && dgbarcodeprint.CurrentRow != null)
                {
                    dgbarcodeprint.Rows.Remove(dgbarcodeprint.CurrentRow);

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {


            try
            {
                int index = e.RowIndex;


            }
            catch (Exception ee)
            {
                // MessageBox.Show(ee.Message);

            }
        }
        //void printpdf()
        //{
        //    iTextSharp.text.Rectangle rect = PageSize.GetRectangle("A4");

        //    iTextSharp.text.Document pdfdoc = new iTextSharp.text.Document(new iTextSharp.text.Rectangle(rect.Width, rect.Height), 0, 0, 17, -35);

        //    try
        //    {
        //        DirectoryInfo dir1 = new DirectoryInfo(Application.StartupPath + "\\Barcode");
        //        if (!Directory.Exists(Application.StartupPath + "\\Barcode"))
        //        {
        //            dir1.Create();
        //        }
        //        if (File.Exists(Application.StartupPath + "\\Barcode\\Barcode1.pdf"))
        //        {
        //            File.Delete(Application.StartupPath + "\\Barcode\\Barcode1.pdf");
        //        }

        //        //pdfdoc = new Document(PageSize.A4, -2, 20, -1, 20);
        //        PdfWriter writer = PdfWriter.GetInstance(pdfdoc, new FileStream(Application.StartupPath + "\\Barcode\\Barcode1.pdf", FileMode.Create));
        //        PdfPTable tbl = new PdfPTable(5);
        //        tbl.WidthPercentage = 100;


        //        //   float[] widths = new float[] {1f, 1f,3f,1f,1f };
        //        //  tbl.SetWidths(widths);
        //        tbl.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        //        tbl.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
        //        tbl.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;

        //        pdfdoc.Open();
        //        int intotalCount = 0;
        //        Class.BarcodeSettings Info = new Class.BarcodeSettings();

        //        for (int i = 0; i < dataGridView1.Rows.Count; i++)
        //        {
        //            if (dataGridView1.Rows[i].Cells["Code"].Value != null && dataGridView1.Rows[i].Cells["Code"].Value.ToString() != "")
        //            {
        //                int inCopies = 0;
        //                if (dataGridView1.Rows[i].Cells["Quantity"].Value != null)
        //                {
        //                    int.TryParse(dataGridView1.Rows[i].Cells["Quantity"].Value.ToString(), out inCopies); // number of copies of arcode to be printed
        //                }
        //                string batch = "";


        //                for (int j = 0; j < inCopies; j++)
        //                {
        //                    string strProductCode = string.Empty;
        //                    string strCode = string.Empty;
        //                    string strCompanyName = string.Empty;
        //                    string strBarcodeValue = string.Empty;
        //                    string strProductName = string.Empty;
        //                    string RetailPrice = string.Empty;
        //                    if (IsProductCode == true)
        //                    {
        //                        //   strCode = dgbarcodeprint.Rows[i].Cells["Item_Code"].Value.ToString();

        //                        if (IsProductCode == true)
        //                            strProductCode = strCode;
        //                    }
        //                    else
        //                    {
        //                        strProductCode = dataGridView1.Rows[i].Cells["Name"].Value.ToString();
        //                    }

        //                    strProductName = dataGridView1.Rows[i].Cells["Name"].Value.ToString();


        //                    //if (IsBarcodeValue == true)
        //                    //{
        //                    //    strBarcodeValue = batch;

        //                    //}
        //                    //else
        //                    //{
        //                    //    strBarcodeValue = batch;
        //                    //}
        //                    if (chkBarcode.Checked)
        //                    {
        //                        strBarcodeValue = dataGridView1.Rows[i].Cells["Code"].Value.ToString();
        //                    }
        //                    else
        //                    {
        //                        strBarcodeValue = dataGridView1.Rows[i].Cells["Barcode"].Value.ToString();
        //                    }

        //                    if (IsCompany == true)
        //                        strCompanyName = companyname;
        //                    string strMRP = string.Empty;

        //                    if (IsMRP == true)
        //                    {
        //                        //  strMRP = PriceType+":"+ dgbarcodeprint.Rows[i].Cells["Rate"].Value.ToString();
        //                        strMRP = getcurrency() + ". " + dataGridView1.Rows[i].Cells["Sales price"].Value.ToString();
        //                    }
        //                    string strSecretPurchaseRateCode = string.Empty;

        //                    PdfContentByte pdfcb = writer.DirectContent;
        //                    Barcode128 code128 = new Barcode128();
        //                    code128.Code = strBarcodeValue;
        //                    code128.Extended = false;
        //                    code128.CodeType = iTextSharp.text.pdf.Barcode.CODE128;
        //                    code128.BarHeight = 15;

        //                    if (IsBarcodeValue == false)
        //                    {
        //                        code128.Font = null;
        //                        code128.AltText = strBarcodeValue;
        //                    }
        //                    code128.BarHeight = HEIGHT;

        //                    code128.Size = 8;
        //                    code128.Baseline = 7;
        //                    code128.TextAlignment = Element.ALIGN_CENTER;
        //                    iTextSharp.text.Image image128 = code128.CreateImageWithBarcode(pdfcb, null, null);

        //                    Phrase phrase = new Phrase();
        //                    phrase.Font.Size = 7f;

        //                    if (IsCompany == true)
        //                    {
        //                        phrase.Add(new Chunk(companyname + Environment.NewLine));
        //                    }

        //                    phrase.Add(new Chunk(strProductName + Environment.NewLine));
        //                    phrase.Add(new Chunk(Environment.NewLine));
        //                    phrase.Add(new Chunk(image128, 0, 0));
        //                    //     phrase.Add(new Chunk(Environment.NewLine));
        //                    if (IsMRP == true)
        //                    {
        //                        phrase.Add(new Chunk(Environment.NewLine + strMRP));
        //                    }
        //                    if (IsProductCode == true)
        //                    {
        //                        phrase.Add(new Chunk(Environment.NewLine + strCode));
        //                    }
        //                    PdfPCell cell = new PdfPCell(phrase);
        //                    //cell.FixedHeight = 80.69f;

        //                    cell.PaddingRight = -10;
        //                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
        //                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //                    cell.Border = iTextSharp.text.Rectangle.NO_BORDER;

        //                    //cell.PaddingBottom=20;
        //                    // 


        //                    phrase.Add(new Chunk(Environment.NewLine));
        //                    // phrase.Add(new Chunk(Environment.NewLine));
        //                    cell.PaddingRight = 3;
        //                    tbl.AddCell(cell);

        //                    intotalCount++;

        //                }


        //            }

        //        }
        //        int reminder = intotalCount % 5;
        //        if (reminder != 0)
        //        {
        //            for (int i = reminder; i < 6; ++i)
        //            {
        //                tbl.AddCell("");
        //            }
        //        }
        //        if (tbl.Rows.Count != 0)
        //        {
        //            pdfdoc.Add(tbl);
        //            pdfdoc.SetMargins(0, 0, 17, -35);
        //            pdfdoc.Close();
        //            System.Diagnostics.Process.Start(Application.StartupPath + "\\Barcode\\Barcode1.pdf");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex.Message.Contains("The process cannot access the file") && ex.Message.Contains("Barcode.pdf' because it is being used by another process."))
        //        {
        //            MessageBox.Show("Close the PDF file and try again", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }
        //        else
        //        {
        //            MessageBox.Show("Error:" + ex.Message, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        private DataTable table_for_batch = new DataTable();
        public void BindBatchTable()
        {
            table_for_batch.Rows.Clear();
            //SqlCommand cmd = new SqlCommand();
            //cmd.Connection = conn;

           
            //cmd.CommandText = "itemSuggestion_test";
            //cmd.CommandType = CommandType.StoredProcedure;
            //adapter.SelectCommand = cmd;
            //adapter.Fill(table_for_batch);
            //cmd.CommandType = CommandType.Text;
           table_for_batch= barcodse.selectAllBatches();

        }
        public void bindBatchGrid(string code)
        {
            dgBatch.DataSource = null;
            dgBatch.Rows.Clear();
            DataRow[] dr = null; ;
            DataTable dt1 = null;
            dgBatch.DataSource = "";
            dr = table_for_batch.Select("ITEM_CODE = '" + code + "'", "batch_id desc");
            try
            {
                dt1 = dr.CopyToDataTable();
            }
            catch
            {
               // dgBatch.Visible = false;
            }
            dgBatch.DataSource = dt1;
            if (dgBatch.RowCount > 0)
            {
                dgBatch.Columns["BATCH CODE"].Visible = hasBatch;
                dgBatch.Columns["EXPIRY DATE"].Visible = hasBatch;
                if (!hasBatch)
                {
                    dgBatch.Columns["STOCK"].DisplayIndex = 0;
                    dgBatch.Columns["RTL"].DisplayIndex = 4;
                    dgBatch.Columns["MRP"].DisplayIndex = 5;
                    dgBatch.Columns["PUR"].DisplayIndex = 6;
                }
                else
                {
                    dgBatch.Columns["STOCK"].DisplayIndex = 0;
                    dgBatch.Columns["BATCH CODE"].DisplayIndex = 4;
                    dgBatch.Columns["EXPIRY DATE"].DisplayIndex = 5;
                    dgBatch.Columns["RTL"].DisplayIndex = 6;
                    dgBatch.Columns["MRP"].DisplayIndex = 7;
                    dgBatch.Columns["PUR"].DisplayIndex = 8;
                }
                dgBatch.Columns["ITEM_CODE"].Visible = false;
                dgBatch.Columns["UNIT_CODE"].Visible = false;
                dgBatch.Columns["batch_increment"].Visible = false;
                dgBatch.Columns["batch_id"].HeaderText = "PID";
               
            }
        }

        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataRow[] dr = null; ;
                DataTable dt1 = null;
                DataTable t = new DataTable();
                if (Ismanual)
                {
                  
                    DataTable tt = General.Product4mBarcode(txtBarcode.Text);
                    if (tt.Rows.Count > 0)
                    {
                        CODE.Text = tt.Rows[0][0].ToString();
                        DESC_ENG.Text = tt.Rows[0][1].ToString();
                        bindBatchGrid(CODE.Text);

                       
                    }
                }
                else
                {
                    string co = txtBarcode.Text;
                    dgBatch.DataSource = "";
                    dr = table_for_batch.Select("batch_id = '" + co + "'");
                    if (dr.Length > 0)
                    {
                        t = dr.CopyToDataTable();
                    }
                    if (t.Rows.Count > 0)
                    {
                        CODE.Text = t.Rows[0][0].ToString();
                        DESC_ENG.Text = t.Rows[0]["ITEM NAME"].ToString();
                        bindBatchGrid(CODE.Text);
                    }
                }
                if (dgBatch.Rows.Count > 0)
                {
                    dgBatch.Rows[0].Selected = true;
                    dgBatch.Focus();
                }
            }
        }

        private void txtBarcode_Click(object sender, EventArgs e)
        {
            if(txtBarcode.Text!=null)
            {
                txtBarcode.Select();
            }
        }

        private void cmb_item_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_item.SelectedIndex > 0)
            {
                CODE.Text = cmb_item.SelectedValue.ToString();
                DESC_ENG.Text = cmb_item.Text;

                if (CODE.Text != "")
                {
                    bindBatchGrid(CODE.Text);

                    if (dgBatch.Rows.Count > 0)
                    {
                        dgBatch.Rows[0].Selected = true;
                        dgBatch.Focus();
                    }
                }
            }
        }

        private void dgBatch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnadd.PerformClick();
            }
        }

        private void kryptonLinkLabel1_LinkClicked(object sender, EventArgs e)
        {
            Barcode_Settings bsettinges = new Barcode_Settings();
            bsettinges.ShowDialog();
        }

        private void kryptonLinkLabel2_LinkClicked(object sender, EventArgs e)
        {
             DialogResult dr = MessageBox.Show("Are you sure to refresh settings,It will delete all the data..?", "Sysbizz", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
             if (dr == DialogResult.Yes)
             {
                 BtnClear.PerformClick();
                 this.Barcode_Stock_Items_Load(sender, e);
             }
        }

        private void dgbarcodeprint_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            //try
            //{
            //    if (e.RowIndex > -1 )
            //    {
                    
            //            if (!dgbarcodeprint.Rows[e.RowIndex].Cells["dgvCopies"].ReadOnly && dgbarcodeprint.Rows[e.RowIndex].Cells["dgvCopies"].Value != null && dgbarcodeprint.Rows[e.RowIndex].Cells["dgvCopies"].Value.ToString() != "")
            //            {
            //                TotalCountOfCopies();
            //            }
            //            else
            //            {
            //                dgbarcodeprint.Rows[e.RowIndex].Cells["dgvCopies"].Value = 0;
            //                TotalCountOfCopies();
            //            }
                    
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}

        }
    }
}