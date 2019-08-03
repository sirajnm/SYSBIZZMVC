using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using System.Globalization;
using Sys_Sols_Inventory.Model;
namespace Sys_Sols_Inventory
{
    public partial class StockTransaction : Form
    {
        #region properties declaration
        private string ID = "";
        //private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        public bool hasArabic = true;
        //private SqlCommand cmd = new SqlCommand();
        //private SqlDataAdapter adapter = new SqlDataAdapter();
        private BindingSource source = new BindingSource();
        private BindingSource source2 = new BindingSource();
        private DataTable table = new DataTable();
        private DataTable tableUnits = new DataTable();
        private bool hasBatch = General.IsEnabled(Settings.Batch);
        private bool hasTax = General.IsEnabled(Settings.Tax);
        InvStkTrxHdrDB invHdr = new InvStkTrxHdrDB();
        StockDB stkdb = new StockDB();
        InvStkTrxDtlDB invDtl = new InvStkTrxDtlDB();
        private int selectedRow = -1;
        private string type = "";
        string decimalFormat;
        double sales_price = 0;
        #endregion

        Initial mdi = (Initial)Application.OpenForms["Initial"];
        Login lg = (Login)Application.OpenForms["Login"];
        private DataTable table_for_batch = new DataTable();
        Class.ModifiedTransaction modtrans = new Class.ModifiedTransaction();
        private bool hasSaleExclusive = false;
        Login log = (Login)Application.OpenForms["Login"];
        bool hasPriceBatch = false;
        string SalesManCode;
        int TaxId;
        string suf;
          
        public StockTransaction(string docType,string suffix)
        {
            InitializeComponent();
            //cmd.Connection = conn;
            //adapter.SelectCommand = cmd;
            type = docType;
            this.Text += " - " + suffix;
            suf = suffix;
        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            

            if (keyData == (Keys.Escape))
            {
                dataGridItem.Visible = false;
                txtItemName.Focus();
                btnClear.PerformClick();
                return true;
            }
            else if (keyData == (Keys.S | Keys.Control))
            {

                btnSave.Focus();
                btnSave.PerformClick();
                txtItemName.Focus();



            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void StockTransaction_Load(object sender, EventArgs e)
        {
            Class.CompanySetup CompStep = new Class.CompanySetup();
            DOC_DATE_GRE.Text = CompStep.GettDate();

            hasArabic = General.IsEnabled(Settings.Arabic);
            hasPriceBatch = General.IsEnabled(Settings.priceBatch);
            fillDGV(false, "", "");
            UOM.DataSource = tableUnits;
            if (!hasArabic)
            {
                DOC_DATE_HIJ.Enabled = false;
                PnlArabic.Visible = false;
            }

            if (!hasBatch)
            {
                panBatch.Visible = false;
                uBatch.Visible = false;
                uExpDate.Visible = false;
            }

            if (!hasTax)
            {
                panTax.Visible = false;
                uTaxPercent.Visible = false;
                uTaxAmt.Visible = false;
            }
            decimalFormat = Common.getDecimalFormat();
            hasSaleExclusive = General.IsEnabled(Settings.Pur_Exclusive_tax);
            QUANTITY.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            PRICE.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            if (hasTax)
            {
                TAX_PERCENT.KeyPress +=new KeyPressEventHandler(General.OnlyFloat);
                TAX_AMOUNT.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            }
            bindgridview();
            ActiveControl = BRANCH_OTHER;
            SalesManCode = log.EmpId;
            BindBatchTable();
            dgBatch.Visible = false;
        }
        public void BindBatchTable()
        {
            table_for_batch.Clear();
            table_for_batch = invHdr.getAllItemBatches();
            //SqlCommand cmd = new SqlCommand();
            //cmd.Connection = conn;

            ////cmd.CommandText = "ItemSuggestion";
            //// cmd.CommandText = "itemSuggestion_test";
            //cmd.CommandText = "itemSuggestion_test";
            //cmd.CommandType = CommandType.StoredProcedure;
            //adapter.SelectCommand = cmd;
            //adapter.Fill(table_for_batch);
            //cmd.CommandType = CommandType.Text;
            ////dataGridItem.DataSource = productDataTable;


        }
        public void bindgridview()
        {
            try
            {
                //cmd.Connection = conn;
                //cmd.CommandText = "SELECT INV_ITEM_DIRECTORY.CODE AS [Item Code], INV_ITEM_DIRECTORY_UNITS.BARCODE AS Barcode, INV_ITEM_DIRECTORY.DESC_ENG AS [Item Name],  INV_ITEM_DIRECTORY_UNITS.UNIT_CODE, INV_ITEM_PRICE.PRICE,GEN_TAX_MASTER.TaxRate as 'Tax Rate(%)' FROM            INV_ITEM_PRICE INNER JOIN   INV_ITEM_DIRECTORY_UNITS ON INV_ITEM_PRICE.UNIT_CODE = INV_ITEM_DIRECTORY_UNITS.UNIT_CODE AND INV_ITEM_PRICE.ITEM_CODE = INV_ITEM_DIRECTORY_UNITS.ITEM_CODE RIGHT OUTER JOIN  INV_ITEM_DIRECTORY ON INV_ITEM_DIRECTORY_UNITS.ITEM_CODE = INV_ITEM_DIRECTORY.CODE left outer join GEN_TAX_MASTER on INV_ITEM_DIRECTORY.TaxId=GEN_TAX_MASTER.TaxId WHERE        (INV_ITEM_PRICE.SAL_TYPE = 'RTL')";
                //cmd.CommandType = CommandType.Text;
                //DataTable dt = new DataTable();
                //adapter.SelectCommand = cmd;
                //adapter.Fill(dt);
                source2.DataSource = invHdr.getItemDetailsToStockTrx();

                dataGridItem.DataSource = source2;

                dataGridItem.RowHeadersVisible = false;
                dataGridItem.Columns[1].Visible = false;
                dataGridItem.Columns[2].Width = 250;
                dataGridItem.ClearSelection();

            }
            catch
            {
            }
        }

        private bool valid()
        {
            if (TOTAL_TAX_AMOUNT.Text == "" )
            {
                TOTAL_TAX_AMOUNT.Text = "0";
            }

            if (ID != "")
            {
                if (NOTES.Text == "")
                {
                    MessageBox.Show("Enter reason for updation in remarks");
                    return false;
                }

            }

            if (dgItems.Rows.Count == 0)
            {
                MessageBox.Show("Please add items to save.");
                return false;
            }
            else if (BRANCH_OTHER.Text == "")
            {
                MessageBox.Show("Select a Branch to transfer");
                return false;
            }
            else
            {
                return true;
            }
        }

        public void modifiedtransaction()
        {

            modtrans.VOUCHERTYPE = "Stock Transaction - "+suf;

            modtrans.Date = DOC_DATE_GRE.Value.ToString("MM/dd/yyyy");
            modtrans.USERID = log.EmpId;
            modtrans.VOUCHERNO = DOC_NO.Text;
            modtrans.NARRATION = NOTES.Text;
            modtrans.STATUS = "Update";
            modtrans.MODIFIEDDATE = DateTime.Now.ToString("MM/dd/yyyy");
            modtrans.BRANCH = lg.Branch;
            modtrans.insertTransaction();
        }
        StockEntry stockEntry = new StockEntry();
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (valid())
            {
                if (ID == "")
                {
                    DOC_NO.Text = General.generateStockID();
                    string query = "INSERT INTO INV_STK_TRX_HDR(BRANCH,DOC_NO,DOC_TYPE,DOC_DATE_GRE,DOC_DATE_HIJ,DOC_REFERENCE,BRANCH_OTHER,NOTES,TAX_AMOUNT,TOTAL_AMOUNT,AddedBy) VALUES('" + BRANCH_OTHER.Text + "','" + DOC_NO.Text + "','" + type + "','" + DOC_DATE_GRE.Value.ToString("MM/dd/yyyy") + "','" + DOC_DATE_HIJ.Text + "','" + DOC_REFERENCE.Text + "','" + BRANCH_OTHER.Text + "','" + NOTES.Text + "','" + TOTAL_TAX_AMOUNT.Text + "','" + TOTAL_AMOUNT.Text + "','" + SalesManCode + "');";
                       query += "INSERT INTO INV_STK_TRX_DTL(DOC_TYPE,DOC_NO,ITEM_CODE,ITEM_DESC_ENG,UOM,PRICE,QUANTITY,BRANCH";
                    // validation 
                    query += ",UOM_QTY,PRICE_BATCH";
                    // validation
                    if (hasBatch)
                    {
                        query += ",BATCH,EXPIRY_DATE";
                    }
                    if (hasTax)
                    {
                        query += ",TAX_PER,TAX_AMOUNT";
                    }
                    query += ")";

                    ItemLedger il = new ItemLedger();
                    il.DocumentNo = DOC_NO.Text;
                    il.Branch = lg.Branch;
                    il.EntryDate = DOC_DATE_GRE.Value;
                    il.EntryType = type;

                    for (int i = 0; i < dgItems.Rows.Count; i++)
                    {
                        DataGridViewCellCollection c = dgItems.Rows[i].Cells;
                        query += " SELECT '"+type+"','"+DOC_NO.Text+"','"+c["uCode"].Value+"','"+c["uName"].Value+"','"+c["uUnit"].Value+"','"+Convert.ToDouble( c["uPrice"].Value)+"','"+c["uQty"].Value+"','"+BRANCH_OTHER.Text+"'";
                       //not validated
                        query += ",'" + c["uUOM_QTY"].Value + "','" + c["colBATCH"].Value + "'";
                        //not validated
                        il.ItemNo = c["uCode"].Value.ToString();
                        il.UOM = c["uUnit"].Value.ToString();
                        il.UnitCostApplied = (float)c["uPrice"].Value;
                        il.EntryNo = InventoryRepositery.GetMaxEntryNO();
                        if (type == "INV.STK.IN")
                        {
                            il.UOMQuantity = (float)c["uUOM_QTY"].Value;
                            il.Quantity = (float)c["uQty"].Value;
                            il.CostValueApplied = il.Quantity * il.UnitCostApplied;
                            il.BatchEntryNo = il.EntryNo;

                        }
                        if (type == "INV.STK.OUT")
                        {
                            

                            il.UOMQuantity = (float)c["uUOM_QTY"].Value *-1;
                            il.Quantity = (float)c["uQty"].Value *-1;
                            il.CostValueApplied = il.Quantity * il.UnitCostApplied;


                        }

                            if (hasBatch)
                        {
                            query += ",'" + c["uBatch"].Value + "','" + DateTime.ParseExact(c["uExpDate"].Value.ToString(), "dd/MM/yyyy", null).ToString("yyyy/MM/dd") + "'";
                           // query += ",'" + c["uBatch"].Value + "','" + Convert.ToDateTime(c["uExpDate"].Value.ToString()) + "'";
                           // query += ",'" + c["uBatch"].Value + "','" + DateTime.ParseExact(c["uExpDate"].Value.ToString(), "dd/MM/yyyy", null).ToString("yyyy/MM/dd") + "'";
                        }
                        if (hasTax)
                        {
                            query += ",'"+c["uTaxPercent"].Value+"','"+c["uTaxAmt"].Value+"'";
                        }
                        query += " UNION ALL ";
                        string item_id = Convert.ToString(c["uCode"].Value);
                        double qty = Convert.ToDouble(c["uQty"].Value);
                        double uom_qty = Convert.ToDouble(c["uUOM_QTY"].Value);
                        double total_qty = 0;
                        if (type == "INV.STK.IN")
                            total_qty = qty * uom_qty;
                        if (type == "INV.STK.OUT")
                            total_qty =( qty * uom_qty)*-1;
                        stockEntry.addStockWithBatch(item_id, total_qty.ToString(), "", c["colBATCH"].Value.ToString());
                       
                    }
                    query = query.Substring(0, query.Length - 10);
                    //cmd.CommandText += query;
                    ////MessageBox.Show(cmd.CommandText);
                    //conn.Open();
                    //cmd.ExecuteNonQuery();
                    //conn.Close();
                    DbFunctions.InsertUpdate(query);
                    MessageBox.Show("Stock Transaction Added!");
                    btnClear.PerformClick();
                }
                else
                {
                    //SqlCommand reduceStockCommand = new SqlCommand();
                    //reduceStockCommand.Connection = conn;
                    //conn.Open();
                    //reduceStockCommand.CommandText = "SELECT ITEM_CODE,QUANTITY,UOM_QTY, cost_price,PRICE_BATCH FROM INV_STK_TRX_DTL WHERE DOC_NO = '" + DOC_NO.Text + "' AND DOC_TYPE = '" + type + "'";
                    //SqlDataReader r = reduceStockCommand.ExecuteReader();
                    stkdb.DocNo = DOC_NO.Text;
                    stkdb.DocType = type;
                    DataTable dt = stkdb.SelectDataForReduceStkWithType();
                    StockEntry se = new StockEntry();
                    //while (r.Read())
                    //{
                    //    double qty = 0;
                    //    if (type == "INV.STK.IN")
                    //        qty = -1 * (Convert.ToDouble(r["QUANTITY"]) * Convert.ToDouble(r["UOM_QTY"]));
                    //    if (type == "INV.STK.OUT")
                    //        qty = (Convert.ToDouble(r["QUANTITY"]) * Convert.ToDouble(r["UOM_QTY"]));   
                    //    se.addStockWithBatch(Convert.ToString(r["ITEM_CODE"]), Convert.ToString(qty), "", Convert.ToString(r["PRICE_BATCH"]));
                    //}
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                           double qty = 0;
                           if (type == "INV.STK.IN")
                            qty = -1 * (Convert.ToDouble(dt.Rows[i]["QUANTITY"]) * Convert.ToDouble(dt.Rows[i]["UOM_QTY"]));
                           if (type == "INV.STK.OUT")
                            qty =  (Convert.ToDouble(dt.Rows[i]["QUANTITY"]) * Convert.ToDouble(dt.Rows[i]["UOM_QTY"]));
                            se.addStockWithBatch(Convert.ToString(dt.Rows[i]["ITEM_CODE"]), Convert.ToString(qty), Convert.ToString(dt.Rows[i]["cost_price"]), Convert.ToString(dt.Rows[i]["PRICE_BATCH"]));

                    }
                   // conn.Close();
                    modifiedtransaction();
                    string query = "UPDATE INV_STK_TRX_HDR SET DOC_DATE_GRE = '" + DOC_DATE_GRE.Value.ToString("MM/dd/yyyy") + "',DOC_DATE_HIJ = '" + DOC_DATE_HIJ.Text + "',DOC_REFERENCE = '" + DOC_REFERENCE.Text + "',BRANCH_OTHER = '" + BRANCH_OTHER.Text + "',NOTES = '" + NOTES.Text + "',TOTAL_AMOUNT = '" + TOTAL_AMOUNT.Text + "',AddedBy='" + SalesManCode + "' WHERE DOC_NO = '" + DOC_NO.Text + "';DELETE FROM INV_STK_TRX_DTL WHERE DOC_NO = '" + DOC_NO.Text + "';";
                     query = "INSERT INTO INV_STK_TRX_DTL(ITEM_DESC_ENG,DOC_NO,ITEM_CODE,UOM,PRICE,QUANTITY";
                    // validation 
                    query += ",UOM_QTY,PRICE_BATCH";
                    // validation
                    if (hasBatch)
                    {
                        query += ",BATCH,EXPIRY_DATE ";
                    }
                    if (hasTax)
                    {
                        query += ",TAX_PER,TAX_AMOUNT";
                    }
                    query += ")";
                    for (int i = 0; i < dgItems.Rows.Count; i++)
                    {
                        DataGridViewCellCollection c = dgItems.Rows[i].Cells;
                        query += " SELECT '" + c["uName"].Value + "','" + DOC_NO.Text + "','" + c["uCode"].Value + "','" + c["uUnit"].Value + "','" + c["uPrice"].Value + "','" + c["uQty"].Value + "'";
                        //not validated
                        query += ",'" + c["uUOM_QTY"].Value + "','" + c["colBATCH"].Value + "'";
                        //not validated
                        if (hasBatch)
                        {
                            query += ",'" + c["uBatch"].Value + "','" + DateTime.ParseExact(c["uExpDate"].Value.ToString(), "dd/MM/yyyy", null).ToString("yyyy/MM/dd") + "'";
                            // query += ",'" + c["uBatch"].Value + "','" + Convert.ToDateTime(c["uExpDate"].Value.ToString()) + "'";
                        }
                        if (hasTax)
                        {
                            query += ",'" + c["uTaxPercent"].Value + "','" + c["uTaxAmt"].Value + "'";
                        }

                        query += " UNION ALL ";
                        string item_id = Convert.ToString(c["uCode"].Value);
                        double qty = Convert.ToDouble(c["uQty"].Value);
                        double uom_qty = Convert.ToDouble(c["uUOM_QTY"].Value);
                        double total_qty = 0;
                        if (type == "INV.STK.IN")
                            total_qty = qty * uom_qty;
                        if (type == "INV.STK.OUT")
                            total_qty = (qty * uom_qty) * -1;
                        stockEntry.addStockWithBatch(item_id, total_qty.ToString(), "", c["colBATCH"].Value.ToString());
                    }
                    query = query.Substring(0, query.Length - 10);
                    //cmd.CommandText += query;
                    //conn.Open();
                    //cmd.ExecuteNonQuery();
                    //conn.Close();
                    DbFunctions.InsertUpdate(query);
                    MessageBox.Show("Stock Transaction Updated!");
                    btnClear.PerformClick();
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            dgItems.Rows.Clear();
            ID = "";
            DOC_NO.Text = "";
            DOC_DATE_GRE.Value = DateTime.Today;
            DOC_DATE_HIJ.Text = "";
            BRANCH_OTHER.Text = "";
            txtBranchName.Text = "";
            NOTES.Text = "";
            ItemClear();
            TOTAL_TAX_AMOUNT.Text = "";
            TOTAL_AMOUNT.Text = "";
            sales_price = 0;
            BARCODE.Text = "";
            if (dgBatch.Visible==true)
            {
                dgBatch.Visible = false;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            btnClear.PerformClick();
            OpenStockEntryHelp h = new OpenStockEntryHelp(1,type);
            h.ShowDialog();
        }

        private void btnQuit_Click(object sender, EventArgs e)
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

        private void ItemClear()
        {
            selectedRow = -1;
            ITEM_CODE.Text = "";
            txtItemName.Text = "";
            if (hasBatch)
            {
                BATCH.Text = "";
                EXPIRY_DATE.Value = DateTime.Today;
            }
            tableUnits.Rows.Clear();
            QUANTITY.Text = "";
            PRICE.Text = "";
            if (hasTax)
            {
                TAX_PERCENT.Text = "";
                TAX_AMOUNT.Text = "";
            }
            ITEM_TOTAL.Text = "";
        }

        private void btnDoc_Click(object sender, EventArgs e)
        {
            try
            {
                OpenStockEntryHelp h = new OpenStockEntryHelp(0, type);
                if (h.ShowDialog() == DialogResult.OK)
                {
                    btnClear.PerformClick();
                    ID = Convert.ToString(h.c["DOC_NO"].Value);
                    DOC_NO.Text = ID;
                   //DOC_DATE_GRE.Value = Convert.ToDateTime(Convert.ToString(h.c["DOC_DATE_GRE"].Value));
                    try
                    {
                        DOC_DATE_GRE.Text = DateTime.ParseExact(h.c["DOC_DATE_GRE"].Value.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                    }
                    catch
                    {
                    }
                    if (hasArabic)
                    DOC_DATE_HIJ.Text = Convert.ToString(h.c["DOC_DATE_HIJ"].Value);
                    DOC_REFERENCE.Text = Convert.ToString(h.c["DOC_REFERENCE"].Value);
                    BRANCH_OTHER.Text = Convert.ToString(h.c["BRANCH_OTHER"].Value);
                    NOTES.Text = Convert.ToString(h.c["NOTES"].Value);
                    TOTAL_AMOUNT.Text = Convert.ToString(h.c["TOTAL_AMOUNT"].Value);
                    TOTAL_TAX_AMOUNT.Text = Convert.ToString(h.c["TAX_AMOUNT"].Value);
                    //cmd.CommandText = "SELECT * FROM INV_STK_TRX_DTL WHERE DOC_NO = '" + ID + "'";
                    //conn.Open();
                    invDtl.DocNo = ID;
                    SqlDataReader r = invDtl.getAllSData();
                    while (r.Read())
                    {
                        int i = dgItems.Rows.Add(new DataGridViewRow());
                        DataGridViewCellCollection c = dgItems.Rows[i].Cells;
                        c["uCode"].Value = r["ITEM_CODE"];
                        c["uName"].Value = r["ITEM_DESC_ENG"];
                        if (hasBatch)
                        {
                            c["uBatch"].Value = r["BATCH"];
                            c["uExpDate"].Value = Convert.ToDateTime(r["EXPIRY_DATE"]).ToString("dd/MM/yyyy");
                        }
                        if (hasTax)
                        {
                            c["uTaxPercent"].Value = r["TAX_PER"];
                            c["uTaxAmt"].Value = r["TAX_AMOUNT"];
                        }
                        c["uUnit"].Value = r["UOM"];
                        c["uQty"].Value = r["QUANTITY"];
                        c["uPrice"].Value = r["PRICE"];
                        c["uTotal"].Value = r["ITEM_TOTAL"];
                        c["uUOM_QTY"].Value = r["UOM_QTY"];
                        c["colBATCH"].Value = r["PRICE_BATCH"];
                    }
                  //  conn.Close();
                    DbFunctions.CloseConnection();
                }

            }
            catch
            {
            }
        }
        private void totalCalculation()
        {
            double grossTotal = 0, discount = 0, nettAmount = 0, tax = 0, vat = 0;
            for (int i = 0; i < dgItems.Rows.Count; i++)
            {
              //  grossTotal = grossTotal + Convert.ToDouble(dgItems.Rows[i].Cells["cGTotal"].Value);
              //  discount = discount + Convert.ToDouble(dgItems.Rows[i].Cells["cDisc"].Value);
                nettAmount = nettAmount + Convert.ToDouble(dgItems.Rows[i].Cells["uTotal"].Value);
                if (hasTax)
                {
                    tax = tax + Convert.ToDouble(dgItems.Rows[i].Cells["uTaxAmt"].Value);
                    vat = tax * .01;
                }
            }

         //   TOTAL_AMOUNT.Text = grossTotal.ToString();
            TOTAL_AMOUNT.Text = nettAmount.ToString();
            //DISCOUNT.Text = discount.ToString();
            //NET_AMOUNT.Text = nettAmount.ToString();
            if (hasTax)
            {
                TOTAL_TAX_AMOUNT.Text = tax.ToString(decimalFormat);
               // VAT.Text = vat.ToString();
            }
        }
        private void Item_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (sender.GetType() == typeof(DateTimePicker))
                    {
                        UOM.Focus();
                    }
                    else if (sender.GetType() == typeof(KryptonComboBox))
                    {
                        QUANTITY.Focus();
                    }
                    else
                    {
                        KryptonTextBox txtBox = (sender as KryptonTextBox);
                        switch (txtBox.Name)
                        {
                            case "ITEM_CODE":
                                txtItemName.Focus();
                                break;
                            case "txtItemName":
                                if (hasBatch)
                                {
                                    BATCH.Focus();
                                }
                                else
                                {
                                    UOM.Focus();
                                }
                                break;

                            case "BATCH":
                                EXPIRY_DATE.Focus();
                                break;

                            case "QUANTITY":
                                PRICE.Focus();
                                break;

                            case "PRICE":
                                if (hasTax)
                                {
                                    TAX_PERCENT.Focus();
                                }
                                else
                                {
                                    ITEM_TOTAL.Focus();
                                }
                                break;

                            case "TAX_PERCENT":
                                TAX_AMOUNT.Focus();
                                break;

                            case "TAX_AMOUNT":
                                ITEM_TOTAL.Focus();
                                break;

                            case "ITEM_TOTAL":
                                addItem();
                                ITEM_CODE.Focus();
                                break;

                            default:
                                break;
                        }
                    }
                }
                else if (e.KeyCode == Keys.F1)
                {
                    btnItemCode.PerformClick();
                }
            }
            catch
            {
            }
        }

        private bool ItemValid()
        {
            bool batch = true;
            if (hasBatch)
            {
                //if (BATCH.Text.Trim() == "")
                //{
                //    batch = false;
                //}
            }

            if (hasTax && TAX_PERCENT.Text == "")
            {
                TAX_PERCENT.Text = "0";
            }

            if (ITEM_CODE.Text.Trim() != "" && batch && UOM.Text.Trim() != "" && QUANTITY.Text.Trim() != "" && PRICE.Text.Trim() != "")
            {
                return true;
            }
            else
            {
                MessageBox.Show("Please enter all the fields!");
                return false;
            }
        }

        private void addItem()
        {
            try
            {
                if (ItemValid())
                {
                    if (selectedRow == -1)
                    {
                        selectedRow = dgItems.Rows.Add(new DataGridViewRow());
                    }
                    DataGridViewCellCollection c = dgItems.Rows[selectedRow].Cells;
                    c["uCode"].Value = ITEM_CODE.Text;
                    c["uName"].Value = txtItemName.Text;
                    if (hasBatch)
                    {
                        c["uBatch"].Value = BATCH.Text;
                        c["uExpDate"].Value = EXPIRY_DATE.Value.ToString("dd/MM/yyyy");
                    }
                    c["uUnit"].Value = UOM.Text;
                    c["uQty"].Value = QUANTITY.Text;
                    c["uPrice"].Value = PRICE.Text;
                    if (hasTax)
                    {
                        c["uTaxPercent"].Value = TAX_PERCENT.Text;
                        c["uTaxAmt"].Value = TAX_AMOUNT.Text;
                    }
                    c["uTotal"].Value = ITEM_TOTAL.Text;
                    c["colBATCH"].Value =BARCODE.Text;
                    c["uUOM_QTY"].Value = tableUnits.Select("UNIT_CODE = '" + UOM.Text + "'").First()["PACK_SIZE"];
                    ItemClear();
                   // totalItemAmount();
                    totalCalculation();
                }
                else
                {
                    ITEM_CODE.Focus();
                }
            }
            catch
            {
            }
        }

        private void ITEM_CODE_Leave(object sender, EventArgs e)
        {
            addUnits();
        }

        private void addUnits()
        {

            //tableUnits.Rows.Clear();
            //try
            //{
            //    cmd.CommandText = "SELECT UNIT_CODE FROM INV_ITEM_DIRECTORY_UNITS WHERE ITEM_CODE = '" + ITEM_CODE.Text + "'";
            //    adapter.Fill(tableUnits);
            //    UOM.DisplayMember = "UNIT_CODE";
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}



            tableUnits.Rows.Clear();
            //cmd.Connection = conn;
            //cmd.CommandText = "SELECT UNIT_CODE, PACK_SIZE FROM INV_ITEM_DIRECTORY_UNITS WHERE ITEM_CODE = '" + ITEM_CODE.Text + "'";
            //cmd.CommandType = CommandType.Text;
            //adapter = new SqlDataAdapter(cmd);
            //adapter.Fill(tableUnits);
            invDtl.ItemCode = ITEM_CODE.Text;
            tableUnits = invDtl.selectUnits();

            this.UOM.SelectedIndexChanged -= new EventHandler(UOM_SelectedIndexChanged);
            UOM.DataSource = tableUnits;
            UOM.DisplayMember = "UNIT_CODE";
            UOM.ValueMember = "PACK_SIZE";
           
            this.UOM.SelectedIndexChanged += new EventHandler(UOM_SelectedIndexChanged);
        }

        public void GetTaxRate()
        {
            try
            {
               //adapter = null;
                invHdr.TaxId = TaxId.ToString() ;
                DataTable dt = invHdr.selectTaxRateDt();
              //  SqlDataAdapter da = new SqlDataAdapter();
              ////  cmd.CommandText = "";
                
              //  cmd.Dispose();
              //  cmd.CommandType = CommandType.Text;
              //  cmd.CommandText = "";
              //  cmd.CommandText = "SELECT TaxRate from GEN_TAX_MASTER where TaxId="+TaxId;
              //  da.SelectCommand = cmd;
              //  da.Fill(dt);
                TAX_PERCENT.Text = dt.Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                
            }
        }
        private void btnItemCode_Click(object sender, EventArgs e)
        {
            ItemMasterHelp h = new ItemMasterHelp(0,hasPriceBatch);
            if (h.ShowDialog() == DialogResult.OK && h.c != null)
            {
                ITEM_CODE.Text = Convert.ToString(h.c[0].Value);
                txtItemName.Text = Convert.ToString(h.c[1].Value);
                TaxId = Convert.ToInt32(h.c["TaxId"].Value);
                if (hasPriceBatch)
                {
                    PRICE.Text = Convert.ToString(h.c[8].Value);
                }
                else
                    PRICE.Text = Convert.ToString(h.c["RTL"].Value);
                   addUnits();
                GetTaxRate();

             
                if (ITEM_CODE.Text != "")
                {
                    if (hasPriceBatch)
                    {
                        dgBatch.Visible = true;
                        bindBatchGrid(Convert.ToString(h.c[0].Value));
                        dgBatch.Focus();
                    }
                    else
                    {
                        dgBatch.Visible = false;
                        bindBatchGrid(Convert.ToString(h.c[0].Value));
                        if (dgBatch.CurrentRow != null)
                        {
                            //ShowStock = false;
                            //itemSelected = true;
                            QUANTITY.Text = "1";
                            string itemcode = dgBatch.CurrentRow.Cells["ITEM_CODE"].Value.ToString();
                            ITEM_CODE.Text = itemcode;

                            //NOT VALIDATED//
                            BARCODE.Text = dgBatch.CurrentRow.Cells["BATCH_ID"].Value.ToString();
                            if (hasBatch)
                            {
                                if (dgBatch.CurrentRow.Cells["BATCH CODE"].Value != null)
                                    BATCH.Text = dgBatch.CurrentRow.Cells["BATCH CODE"].Value.ToString();
                                if (dgBatch.CurrentRow.Cells["EXPIRY DATE"].Value != null && dgBatch.CurrentRow.Cells["EXPIRY DATE"].Value.ToString() != "")
                                    EXPIRY_DATE.Value = Convert.ToDateTime(dgBatch.CurrentRow.Cells["EXPIRY DATE"].Value);
                            }
                            //NOT VALIDATED//
                            // PurchasePrice = Convert.ToDecimal(dgBatch.CurrentRow.Cells["PUR"].Value);
                            // String rateType = Convert.ToString(RATE_CODE.SelectedValue);
                            TaxId = Convert.ToInt16(dgBatch.CurrentRow.Cells["TaxId"].Value.ToString());
                            GetTaxRate();

                            String rateType = "PUR";
                            string pricedecimal = h.c[rateType].Value.ToString();
                            sales_price = Convert.ToDouble(h.c[rateType].Value);
                            double pricedec = Convert.ToDouble(pricedecimal);
                            PRICE.Text = pricedec.ToString();
                            if (rateType.StartsWith("MRP"))
                            {
                                double taxcalc = 0;
                                taxcalc = (Convert.ToDouble(TAX_PERCENT.Text) / 100) + 1;
                                PRICE.Text = (Convert.ToDouble(PRICE.Text) / taxcalc).ToString(decimalFormat);
                            }
                            else if (!hasSaleExclusive)
                            {
                                double taxcalc = 0;
                                taxcalc = (Convert.ToDouble(TAX_PERCENT.Text) / 100) + 1;
                                PRICE.Text = (Convert.ToDouble(PRICE.Text) / taxcalc).ToString();
                            }
                            else
                            {
                                PRICE.Text = h.c[rateType].Value.ToString();
                            }
                            sales_price = Convert.ToDouble(PRICE.Text);
                            PRICE.Text = sales_price.ToString();
                            //TAX_PERCENT_TextChanged(sender, e);
                            //PRICE.Text = dgBatch.CurrentRow.Cells[rateType].Value.ToString();
                            //PRICE.Text = (dataGridItem.CurrentRow.Cells["SALES"].Value.ToString("N3");
                            //HASSERIAL = Convert.ToBoolean(dgBatch.CurrentRow.Cells["HASSERIAL"].Value);
                            //PNLSERIAL.Visible = HASSERIAL;
                            //mrp = Convert.ToDouble(dgBatch.CurrentRow.Cells["MRP"].Value);
                            //tb_mrp.Text = mrp.ToString(decimalFormat);
                            //kryptonLabel36.Visible = HASSERIAL;
                            //SERIALNO.Visible = HASSERIAL;
                            //kryptonLabel36.Visible = HASSERIAL;
                            //SERIALNO.Visible = HASSERIAL;

                            ////if (hasBatch)
                            ////{
                            ////    BATCH.Focus();
                            ////}
                            ////else
                            ////{
                            //QUANTITY.Text = "1";
                            //if (SalebyItemCode)
                            //    ITEM_CODE.Focus();
                            //else if (MoveToUnit)
                            //    UOM.Focus();
                            //else if (MoveToQty)
                            //    QUANTITY.Focus();
                            //else if (HASSERIAL)
                            //    SERIALNO.Focus();
                            //else if (txtfree.Visible)
                            //    txtfree.Focus();
                            //else if (MoveToPrice)
                            //    PRICE.Focus();
                            //else if (tb_mrp.Visible)
                            //    tb_mrp.Focus();
                            //else if (GROSS_TOTAL.Visible)
                            //    GROSS_TOTAL.Focus();
                            //else if (MoveToDisc)
                            //    ITEM_DISCOUNT.Focus();
                            //else if (tb_netvalue.Visible)
                            //    tb_netvalue.Focus();
                            //else if (tb_descr.Visible)
                            //    tb_descr.Focus();
                            //else if (hasTax)
                            //{
                            //    if (MoveToTaxper)
                            //        ITEM_TAX_PER.Focus();
                            //}
                            //else if (hasBatch)
                            //    BATCH.Focus();
                            //else
                            //{
                            //    addItem();
                            //    clearItem();
                            //    if (SalebyItemName)
                            //        ITEM_NAME.Focus();
                            //    else if (SalebyItemCode)
                            //        ITEM_CODE.Focus();
                            //    else if (SalebyBarcode)
                            //        BARCODE.Focus();
                            //    else
                            //        ITEM_NAME.Focus();
                            //}
                            ////    tb_descr.Focus();

                            //}

                            // this.UOM.SelectedIndexChanged -= new EventHandler(UOM_SelectedIndexChanged);
                            //  addUnits();
                            // this.UOM.SelectedIndexChanged -= new EventHandler(UOM_SelectedIndexChanged);
                            // UOM.Text = dgBatch.CurrentRow.Cells["UNIT_CODE"].Value.ToString();

                            //GetDiscount();
                            //ItemArabicName = dgBatch.CurrentRow.Cells["DESC_ARB"].Value.ToString();
                            //ITEM_NAME.Text = dgBatch.CurrentRow.Cells["ITEM NAME"].Value.ToString();
                            //PNL_DATAGRIDITEM.Visible = false;
                            //itemSelected = false;
                            //if (RATE_CODE.Text.StartsWith("MRP"))
                            //{
                            //    double taxcalc = 0;
                            //    taxcalc = (Convert.ToDouble(ITEM_TAX_PER.Text) / 100) + 1;
                            //    PRICE.Text = (Convert.ToDouble(PRICE.Text) / taxcalc).ToString(decimalFormat);
                            //}
                            dgBatch.Visible = false;
                            QUANTITY.Focus();
                        } 
                    }
                }
                //if (hasBatch)
                //{
                //    BATCH.Focus();
                //}

             
            }
        }
        public void bindBatchGrid(string code)
        {
            dgBatch.DataSource = null;
            dgBatch.Rows.Clear();
            DataRow[] dr = null; ;
            DataTable dt1 = null;
            dgBatch.DataSource = "";
            dr = table_for_batch.Select("ITEM_CODE = '" + code + "'");
            try
            {
                dt1 = dr.CopyToDataTable();
            }
            catch
            {

            }
            //dgBatch.Rows.Clear();
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
            }
            //dgBatch.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            ////dgBatch.Columns["ITEM_CODE"].HeaderText = "Item Code";
            ////dgBatch.Columns["TaxId"].Visible = false;

            ////if (!hasArabic)
            ////{
            ////    dgBatch.Columns["DESC_ARB"].Visible = false;
            ////}

            ////if (!hasTax)
            ////{
            ////    dgBatch.Columns["TaxRate"].Visible = false;

            ////}
            //dgBatch.Columns["HASSERIAL"].Visible = false;
            //dgBatch.Columns["Type"].Visible = false;
            //dgBatch.Columns["Category"].Visible = false;
            //dgBatch.Columns["Group"].Visible = false;
            //dgBatch.Columns["Trademark"].Visible = false;
            //dgBatch.Columns["Stock"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //dgBatch.Columns["TaxId"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //dgBatch.Columns["ITEM NAME"].DisplayIndex = 1;
            //dgBatch.Columns["ITEM NAME"].FillWeight = 800;
            //dgBatch.Columns["supplier_id"].Visible = false;
            //dgBatch.ClearSelection();
        }
        private void CalTotalAmount(object sender, EventArgs e)
        {
            if (PRICE.Text.Trim() != "" && QUANTITY.Text.Trim() != "")
            {

                ITEM_TOTAL.Text = (Convert.ToDouble(QUANTITY.Text) * Convert.ToDouble(PRICE.Text)).ToString();
            }
            else
            {
                ITEM_TOTAL.Text = "0";
            }

            if (TAX_PERCENT.Text.Trim() != "" && PRICE.Text.Trim() != "" && QUANTITY.Text.Trim() != "")
            {
                double total = 0;
               double tax=0;
                
                total = (Convert.ToDouble(QUANTITY.Text) * Convert.ToDouble(PRICE.Text));
               tax= (total* (Convert.ToDouble(TAX_PERCENT.Text) / 100));
                TAX_AMOUNT.Text = tax.ToString();
                ITEM_TOTAL.Text = (total + tax).ToString(decimalFormat);
                
            }
        }

        private void TAX_PERCENT_TextChanged(object sender, EventArgs e)
        {
            if (TAX_PERCENT.Text.Trim() != "" && (ITEM_TOTAL.Text.Trim() != "" || ITEM_TOTAL.Text == "0"))
            {
                
                TAX_AMOUNT.Text = ((Convert.ToDouble(TAX_PERCENT.Text) / 100) * Convert.ToDouble(ITEM_TOTAL.Text)).ToString();
               // ITEM_TOTAL.Text = Convert.ToDouble(ITEM_TOTAL.Text) + Convert.ToDouble(TAX_AMOUNT.Text).ToString();
            }
            else
            {
                TAX_AMOUNT.Text = "0";
            }
        }

        private void totalItemAmount()
        {
            double total = 0;
            double tax = 0;
            for (int i = 0; i < dgItems.Rows.Count; i++)
            {
                total = total + Convert.ToDouble(dgItems.Rows[i].Cells[dgItems.Columns.Count-1].Value);
                tax = tax + Convert.ToDouble(dgItems.Rows[i].Cells["uTaxAmt"].Value);
            }
            TOTAL_AMOUNT.Text = total.ToString();
            TOTAL_TAX_AMOUNT.Text = tax.ToString();
        }

        private void dgItems_Click(object sender, EventArgs e)
        {
            if (dgItems.Rows.Count > 0 && dgItems.CurrentRow != null)
            {
                selectedRow = dgItems.CurrentRow.Index;
                DataGridViewCellCollection c = dgItems.CurrentRow.Cells;
                ITEM_CODE.Text = Convert.ToString(c["uCode"].Value);
                txtItemName.Text = Convert.ToString(c["uName"].Value);
                if (hasBatch)
                {
                    BATCH.Text = Convert.ToString(c["uBatch"].Value);
                    EXPIRY_DATE.Value = DateTime.ParseExact(Convert.ToString(c["uExpDate"].Value), "dd/MM/yyyy", null);
                }
                addUnits();
                UOM.Text = Convert.ToString(c["uUnit"].Value);
                QUANTITY.Text = Convert.ToString(c["uQty"].Value);
                PRICE.Text = Convert.ToString(c["uPrice"].Value);
                BARCODE.Text = Convert.ToString(c["colBATCH"].Value);
                if (hasTax)
                {
                    TAX_PERCENT.Text = Convert.ToString(c["uTaxPercent"].Value);
                    TAX_AMOUNT.Text = Convert.ToString(c["uTaxAmt"].Value);
                }
                ITEM_TOTAL.Text = Convert.ToString(c["uTotal"].Value);
                addUnits();
            }
        }

        private void btnBranch_Click(object sender, EventArgs e)
        {
           //show branch here.
            CommonHelp c = new CommonHelp(0,genEnum.Branch);
            if (c.ShowDialog() == DialogResult.OK && c.c != null)
            {
                BRANCH_OTHER.Text = Convert.ToString(c.c[0].Value);
                txtBranchName.Text = Convert.ToString(c.c[1].Value);
            }
        }

        private void fillDGV(bool between,string from,string to)
        {
            //conn.Open();
            string query = "";
            if (!between)
            {
                query = "SELECT DOC_NO,CONVERT(NVARCHAR,DOC_DATE_GRE,103) AS DOC_DATE_GRE,DOC_DATE_HIJ = CASE DOC_DATE_HIJ WHEN '  /  /    ' THEN '' ELSE DOC_DATE_HIJ END,DOC_REFERENCE,BRANCH_OTHER,NOTES,TOTAL_AMOUNT FROM INV_STK_TRX_HDR WHERE DOC_TYPE = '" + type + "' AND POSTED = 'N'";
            }
            else
            {
                query = "SELECT DOC_NO,CONVERT(NVARCHAR,DOC_DATE_GRE,103) AS DOC_DATE_GRE,DOC_DATE_HIJ = CASE DOC_DATE_HIJ WHEN '  /  /    ' THEN '' ELSE DOC_DATE_HIJ END,DOC_REFERENCE,BRANCH_OTHER,NOTES,TOTAL_AMOUNT FROM INV_STK_TRX_HDR WHERE DOC_TYPE = '" + type + "' AND DOC_DATE_GRE BETWEEN '" + from + "' AND '" + to + "' AND POSTED = 'N'";
            }
            table.Clear();
            //adapter.Fill(table);
            table = Model.DbFunctions.GetDataTable(query);
            source.DataSource = table;
            dgTrans.DataSource = source;
            //conn.Close();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            fillDGV(true, dpFrom.Value.ToString("MM/dd/yyyy"), dpTo.Value.ToString("MM/dd/yyyy"));
        }

        private void btnFilterClear_Click(object sender, EventArgs e)
        {
            fillDGV(false, "", "");
        }

        private void btnPost_Click(object sender, EventArgs e)
        {
            if (dgTrans.Rows.Count > 0)
            {
                string docIDs = "";
                for (int i = 0; i < dgTrans.Rows.Count; i++)
                {
                    docIDs += "'"+Convert.ToString(dgTrans.Rows[i].Cells[0].Value)+"',";
                }
                docIDs = docIDs.Substring(0, docIDs.Length - 1);
               // conn.Open();
                string query = "UPDATE INV_STK_TRX_HDR SET POSTED = 'Y' WHERE DOC_NO IN ("+docIDs+")";
                //cmd.ExecuteNonQuery();
                //conn.Close();
                DbFunctions.InsertUpdate(query);
                MessageBox.Show("Posted!");
                table.Rows.Clear();
            }
            else
            {
                MessageBox.Show("No Data to Post!");
            }
        }

        private void DOC_NO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                btnDoc.PerformClick();
            }
        }

        private void linkRemoveRecord_LinkClicked(object sender, EventArgs e)
        {
            if (dgItems.Rows.Count > 0 && dgItems.CurrentRow != null)
            {
                dgItems.Rows.Remove(dgItems.CurrentRow);
                ItemClear();
                totalCalculation();
            }
        }

        private void BRANCH_OTHER_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                btnBranch.PerformClick();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                ITEM_CODE.Focus();
            }
            else if (e.KeyCode == Keys.Back)
            {

            }
            else
            {
                btnBranch.PerformClick();
            }
            
        }

        private void txtItemName_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyData == Keys.Down)
            {
                if (dataGridItem.Visible == true)
                {
                    //bindgridview();
                    //dataGridItem.Focus();
                    //dataGridItem.CurrentCell = dataGridItem.Rows[0].Cells[2];
                }
                else
                {
                    if (dgItems.Rows.Count > 0)
                    {
                        dgItems.Focus();
                        dgItems.CurrentCell = dgItems.Rows[0].Cells[1];
                    }
                }
            }
        }

        private void txtItemName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtItemName.Text == "")
                {
                    dataGridItem.Visible = false;
                 
                }
                else
                {
                    //dataGridItem.Visible = true;
                    //source2.Filter = string.Format("[Item Name] LIKE '%{0}%' ", txtItemName.Text);
                    //dataGridItem.ClearSelection();
                }

            }
            catch
            {
            }
        }

        private void dataGridItem_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                {
                    string itemcode = dataGridItem.CurrentRow.Cells[0].Value.ToString();
                    ITEM_CODE.Text = itemcode;
                    PRICE.Text = dataGridItem.CurrentRow.Cells[4].Value.ToString();




                    if (hasBatch)
                    {
                        BATCH.Focus();
                    }
                    else
                    {
                        QUANTITY.Text = "1";
                        QUANTITY.Focus();

                    }

                    addUnits();
                    UOM.Text = dataGridItem.CurrentRow.Cells[3].Value.ToString();
                    if (dataGridItem.CurrentRow.Cells[5].Value.ToString() == "")
                    {
                        TAX_PERCENT.Text = "0";                   
                    }
                    else
                    {
                        TaxId = Convert.ToInt16(dataGridItem.CurrentRow.Cells[5].Value.ToString());
                        //  GetTaxRate();
                        TAX_PERCENT.Text = Convert.ToString(TaxId);
                    
                    }

                    txtItemName.Text = dataGridItem.CurrentRow.Cells[2].Value.ToString();
                    dataGridItem.Visible = false;
                    //    QUANTITY.Text = "1";
                    //    addItem();
                    //  clearItem();
                    //  ITEM_NAME.Focus();






                    //  firstitemlistbyname = false;



                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void QUANTITY_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == '+')
                {

                    if (QUANTITY.Text == "")
                    {
                        QUANTITY.Text = "1";
                    }
                    else
                    {
                        QUANTITY.Text = (Convert.ToInt32(QUANTITY.Text) + 1).ToString();
                    }
                }
                else if (e.KeyChar == '-')
                {
                    if (QUANTITY.Text == "" || QUANTITY.Text == "0" || QUANTITY.Text == "1")
                    {
                        QUANTITY.Text = "1";
                    }

                    else
                    {
                        QUANTITY.Text = (Convert.ToInt32(QUANTITY.Text) - 1).ToString();
                    }
                }
            }
            catch
            {
            }
        }

        private void dgBatch_DoubleClick(object sender, EventArgs e)
        {
            if (dgBatch.CurrentRow != null)
            {
                //ShowStock = false;
                //itemSelected = true;
                QUANTITY.Text = "1";
                string itemcode = dgBatch.CurrentRow.Cells["ITEM_CODE"].Value.ToString();
                ITEM_CODE.Text = itemcode;

                //NOT VALIDATED//
                BARCODE.Text = dgBatch.CurrentRow.Cells["BATCH_ID"].Value.ToString();
                if (hasBatch)
                {
                    if (dgBatch.CurrentRow.Cells["BATCH CODE"].Value != null)
                        BATCH.Text = dgBatch.CurrentRow.Cells["BATCH CODE"].Value.ToString();
                    if (dgBatch.CurrentRow.Cells["EXPIRY DATE"].Value != null && dgBatch.CurrentRow.Cells["EXPIRY DATE"].Value.ToString() != "")
                        EXPIRY_DATE.Value = Convert.ToDateTime(dgBatch.CurrentRow.Cells["EXPIRY DATE"].Value);
                }
                //NOT VALIDATED//
                // PurchasePrice = Convert.ToDecimal(dgBatch.CurrentRow.Cells["PUR"].Value);
               // String rateType = Convert.ToString(RATE_CODE.SelectedValue);
                TaxId = Convert.ToInt16(dgBatch.CurrentRow.Cells["TaxId"].Value.ToString());
                GetTaxRate();
                String rateType = "PUR";
                string pricedecimal = dgBatch.CurrentRow.Cells[rateType].Value.ToString();
                sales_price = Convert.ToDouble(dgBatch.Rows[0].Cells[rateType].Value);
                double pricedec = Convert.ToDouble(pricedecimal);
                PRICE.Text = pricedec.ToString();
                if (rateType.StartsWith("MRP"))
                {
                    double taxcalc = 0;
                    taxcalc = (Convert.ToDouble(TAX_PERCENT.Text) / 100) + 1;
                    PRICE.Text = (Convert.ToDouble(PRICE.Text) / taxcalc).ToString(decimalFormat);
                }
                else if (!hasSaleExclusive)
                {
                    double taxcalc = 0;
                    taxcalc = (Convert.ToDouble(TAX_PERCENT.Text) / 100) + 1;
                    PRICE.Text = (Convert.ToDouble(PRICE.Text) / taxcalc).ToString();
                }
                else
                {
                    PRICE.Text = dgBatch.CurrentRow.Cells[rateType].Value.ToString();
                }
                sales_price = Convert.ToDouble(PRICE.Text);
                PRICE.Text = sales_price.ToString();
               // TAX_PERCENT_TextChanged(sender, e);
                //PRICE.Text = dgBatch.CurrentRow.Cells[rateType].Value.ToString();
                //PRICE.Text = (dataGridItem.CurrentRow.Cells["SALES"].Value.ToString("N3");
                //HASSERIAL = Convert.ToBoolean(dgBatch.CurrentRow.Cells["HASSERIAL"].Value);
                //PNLSERIAL.Visible = HASSERIAL;
                //mrp = Convert.ToDouble(dgBatch.CurrentRow.Cells["MRP"].Value);
                //tb_mrp.Text = mrp.ToString(decimalFormat);
                //kryptonLabel36.Visible = HASSERIAL;
                //SERIALNO.Visible = HASSERIAL;
                //kryptonLabel36.Visible = HASSERIAL;
                //SERIALNO.Visible = HASSERIAL;

                ////if (hasBatch)
                ////{
                ////    BATCH.Focus();
                ////}
                ////else
                ////{
                //QUANTITY.Text = "1";
                //if (SalebyItemCode)
                //    ITEM_CODE.Focus();
                //else if (MoveToUnit)
                //    UOM.Focus();
                //else if (MoveToQty)
                //    QUANTITY.Focus();
                //else if (HASSERIAL)
                //    SERIALNO.Focus();
                //else if (txtfree.Visible)
                //    txtfree.Focus();
                //else if (MoveToPrice)
                //    PRICE.Focus();
                //else if (tb_mrp.Visible)
                //    tb_mrp.Focus();
                //else if (GROSS_TOTAL.Visible)
                //    GROSS_TOTAL.Focus();
                //else if (MoveToDisc)
                //    ITEM_DISCOUNT.Focus();
                //else if (tb_netvalue.Visible)
                //    tb_netvalue.Focus();
                //else if (tb_descr.Visible)
                //    tb_descr.Focus();
                //else if (hasTax)
                //{
                //    if (MoveToTaxper)
                //        ITEM_TAX_PER.Focus();
                //}
                //else if (hasBatch)
                //    BATCH.Focus();
                //else
                //{
                //    addItem();
                //    clearItem();
                //    if (SalebyItemName)
                //        ITEM_NAME.Focus();
                //    else if (SalebyItemCode)
                //        ITEM_CODE.Focus();
                //    else if (SalebyBarcode)
                //        BARCODE.Focus();
                //    else
                //        ITEM_NAME.Focus();
                //}
                ////    tb_descr.Focus();

                //}

                // this.UOM.SelectedIndexChanged -= new EventHandler(UOM_SelectedIndexChanged);
                //  addUnits();
                // this.UOM.SelectedIndexChanged -= new EventHandler(UOM_SelectedIndexChanged);
                // UOM.Text = dgBatch.CurrentRow.Cells["UNIT_CODE"].Value.ToString();
              

                //GetDiscount();
                //ItemArabicName = dgBatch.CurrentRow.Cells["DESC_ARB"].Value.ToString();
                //ITEM_NAME.Text = dgBatch.CurrentRow.Cells["ITEM NAME"].Value.ToString();
                //PNL_DATAGRIDITEM.Visible = false;
                //itemSelected = false;
                //if (RATE_CODE.Text.StartsWith("MRP"))
                //{
                //    double taxcalc = 0;
                //    taxcalc = (Convert.ToDouble(ITEM_TAX_PER.Text) / 100) + 1;
                //    PRICE.Text = (Convert.ToDouble(PRICE.Text) / taxcalc).ToString(decimalFormat);
                //}
                dgBatch.Visible = false;
                QUANTITY.Focus();
            }
        }

        private void TAX_AMOUNT_TextChanged(object sender, EventArgs e)
        {

        }

        private void panTax_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ITEM_TOTAL_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgBatch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (dgBatch.CurrentRow != null)
                {
                    //ShowStock = false;
                    //itemSelected = true;
                    QUANTITY.Text = "1";
                    string itemcode = dgBatch.CurrentRow.Cells["ITEM_CODE"].Value.ToString();
                    ITEM_CODE.Text = itemcode;

                    //NOT VALIDATED//
                    BARCODE.Text = dgBatch.CurrentRow.Cells["BATCH_ID"].Value.ToString();
                    if (hasBatch)
                    {
                        if (dgBatch.CurrentRow.Cells["BATCH CODE"].Value != null)
                            BATCH.Text = dgBatch.CurrentRow.Cells["BATCH CODE"].Value.ToString();
                        if (dgBatch.CurrentRow.Cells["EXPIRY DATE"].Value != null && dgBatch.CurrentRow.Cells["EXPIRY DATE"].Value.ToString() != "")
                            EXPIRY_DATE.Value = Convert.ToDateTime(dgBatch.CurrentRow.Cells["EXPIRY DATE"].Value);
                    }
                    //NOT VALIDATED//
                    // PurchasePrice = Convert.ToDecimal(dgBatch.CurrentRow.Cells["PUR"].Value);
                    // String rateType = Convert.ToString(RATE_CODE.SelectedValue);
                    TaxId = Convert.ToInt16(dgBatch.CurrentRow.Cells["TaxId"].Value.ToString());
                    GetTaxRate();

                    String rateType = "PUR";
                    string pricedecimal = dgBatch.CurrentRow.Cells[rateType].Value.ToString();
                    sales_price = Convert.ToDouble(dgBatch.Rows[0].Cells[rateType].Value);
                    double pricedec = Convert.ToDouble(pricedecimal);
                    PRICE.Text = pricedec.ToString();
                    if (rateType.StartsWith("MRP"))
                    {
                        double taxcalc = 0;
                        taxcalc = (Convert.ToDouble(TAX_PERCENT.Text) / 100) + 1;
                        PRICE.Text = (Convert.ToDouble(PRICE.Text) / taxcalc).ToString(decimalFormat);
                    }
                    else if (!hasSaleExclusive)
                    {
                        double taxcalc = 0;
                        taxcalc = (Convert.ToDouble(TAX_PERCENT.Text) / 100) + 1;
                        PRICE.Text = (Convert.ToDouble(PRICE.Text) / taxcalc).ToString();
                    }
                    else
                    {
                        PRICE.Text = dgBatch.CurrentRow.Cells[rateType].Value.ToString();
                    }
                    sales_price = Convert.ToDouble(PRICE.Text);
                    PRICE.Text = sales_price.ToString();
                    //TAX_PERCENT_TextChanged(sender, e);
                    //PRICE.Text = dgBatch.CurrentRow.Cells[rateType].Value.ToString();
                    //PRICE.Text = (dataGridItem.CurrentRow.Cells["SALES"].Value.ToString("N3");
                    //HASSERIAL = Convert.ToBoolean(dgBatch.CurrentRow.Cells["HASSERIAL"].Value);
                    //PNLSERIAL.Visible = HASSERIAL;
                    //mrp = Convert.ToDouble(dgBatch.CurrentRow.Cells["MRP"].Value);
                    //tb_mrp.Text = mrp.ToString(decimalFormat);
                    //kryptonLabel36.Visible = HASSERIAL;
                    //SERIALNO.Visible = HASSERIAL;
                    //kryptonLabel36.Visible = HASSERIAL;
                    //SERIALNO.Visible = HASSERIAL;

                    ////if (hasBatch)
                    ////{
                    ////    BATCH.Focus();
                    ////}
                    ////else
                    ////{
                    //QUANTITY.Text = "1";
                    //if (SalebyItemCode)
                    //    ITEM_CODE.Focus();
                    //else if (MoveToUnit)
                    //    UOM.Focus();
                    //else if (MoveToQty)
                    //    QUANTITY.Focus();
                    //else if (HASSERIAL)
                    //    SERIALNO.Focus();
                    //else if (txtfree.Visible)
                    //    txtfree.Focus();
                    //else if (MoveToPrice)
                    //    PRICE.Focus();
                    //else if (tb_mrp.Visible)
                    //    tb_mrp.Focus();
                    //else if (GROSS_TOTAL.Visible)
                    //    GROSS_TOTAL.Focus();
                    //else if (MoveToDisc)
                    //    ITEM_DISCOUNT.Focus();
                    //else if (tb_netvalue.Visible)
                    //    tb_netvalue.Focus();
                    //else if (tb_descr.Visible)
                    //    tb_descr.Focus();
                    //else if (hasTax)
                    //{
                    //    if (MoveToTaxper)
                    //        ITEM_TAX_PER.Focus();
                    //}
                    //else if (hasBatch)
                    //    BATCH.Focus();
                    //else
                    //{
                    //    addItem();
                    //    clearItem();
                    //    if (SalebyItemName)
                    //        ITEM_NAME.Focus();
                    //    else if (SalebyItemCode)
                    //        ITEM_CODE.Focus();
                    //    else if (SalebyBarcode)
                    //        BARCODE.Focus();
                    //    else
                    //        ITEM_NAME.Focus();
                    //}
                    ////    tb_descr.Focus();

                    //}

                    // this.UOM.SelectedIndexChanged -= new EventHandler(UOM_SelectedIndexChanged);
                    //  addUnits();
                    // this.UOM.SelectedIndexChanged -= new EventHandler(UOM_SelectedIndexChanged);
                    // UOM.Text = dgBatch.CurrentRow.Cells["UNIT_CODE"].Value.ToString();
                  
                    //GetDiscount();
                    //ItemArabicName = dgBatch.CurrentRow.Cells["DESC_ARB"].Value.ToString();
                    //ITEM_NAME.Text = dgBatch.CurrentRow.Cells["ITEM NAME"].Value.ToString();
                    //PNL_DATAGRIDITEM.Visible = false;
                    //itemSelected = false;
                    //if (RATE_CODE.Text.StartsWith("MRP"))
                    //{
                    //    double taxcalc = 0;
                    //    taxcalc = (Convert.ToDouble(ITEM_TAX_PER.Text) / 100) + 1;
                    //    PRICE.Text = (Convert.ToDouble(PRICE.Text) / taxcalc).ToString(decimalFormat);
                    //}
                    dgBatch.Visible = false;
                    QUANTITY.Focus();
                } 
            }
        }

        private void dgBatch_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void UOM_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UOM.SelectedIndex > -1)
            {
                try
                {

                    PRICE.Text = ((sales_price * Convert.ToDouble(UOM.SelectedValue)) * Convert.ToDouble(QUANTITY.Text)).ToString();
                }
                catch
                {
                }
            }
        }

        private void TOTAL_TAX_AMOUNT_TextChanged(object sender, EventArgs e)
        {

        }        
    }
}
