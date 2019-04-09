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
using System.IO;
using System.Net;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Sys_Sols_Inventory.Model;

namespace Sys_Sols_Inventory
{
    public partial class ItemMasterView : Form
    {
        #region private properties declaration
        //private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        //private SqlCommand cmd = new SqlCommand();
        //private SqlDataAdapter adapter = new SqlDataAdapter();
        private DataTable table = new DataTable();
        private DataTable fieldTable = new DataTable();
        private BindingSource source = new BindingSource();
        private BindingSource source1 = new BindingSource();
        Initial mdi = (Initial)Application.OpenForms["Initial"];
        Login lg = (Login)Application.OpenForms["Login"];
        //SalesQ sa = (SalesQ)Application.OpenForms["Sales"];
        //PurchaseMaster pu = (PurchaseMaster)Application.OpenForms["PurchaseMaster"];
        SalesQ sa = new SalesQ("Sales");
        PurchaseMaster pu = new PurchaseMaster("PurchaseMaster");
        private string ID = "";
        private bool fQty = false;
        private bool HasArabic = true;
        private bool HasType = true;
        private bool HasGroup = true;
        private bool hasBarcode = true;
        private bool HasCategory = true;
        private bool HasTM = true;
        private bool hasTax = false;
        private bool HasReloadUit = false;
        private bool hasBatch = General.IsEnabled(Settings.Batch);
        public bool addedfrompuchase = false;
        #endregion
        private string pathdirectory = "";
        private string ItemFormat = "Default";
        string companyname = "",PriceType="";
        bool IsMRP ; 
        bool IsProductCode ;
        bool IsCompany,IsBarcodeValue ;
        Int32 WIDTH,HEIGHT;     
        Class.item_Maste IM = new Class.item_Maste();
        Class.BarcodeSettings barcodes = new Class.BarcodeSettings();
        StockEntry stock_entry = new StockEntry();
        public string inactive = "N";
        private string batchId = "";
        private DataTable unitsTable = new DataTable();
        private DataTable unitsTable1 = new DataTable();
        Model.ItemDirectoryDB Itemdb = new Model.ItemDirectoryDB();
        Model.StockDB stckdb = new StockDB();
        InvItemDirectoryPictureDB itmdrpic = new InvItemDirectoryPictureDB();
        RateChangeDB ratechngdb = new RateChangeDB();
        InvStkTrxHdrDB InvStkTrxHdrDB = new InvStkTrxHdrDB();
        InvStkTrxDtlDB InvStkTrxDtlDB = new InvStkTrxDtlDB();
        InvItemDirectoryUnits InvItemDirectoryUnits = new InvItemDirectoryUnits();
        InvItemTypeDFDB InvItemTypeDFDB = new InvItemTypeDFDB();

        public ItemMasterView()
        {
            InitializeComponent();
            //cmd.Connection = conn;
            //adapter.SelectCommand = cmd;
            fieldTable.Columns.Add("key");
            fieldTable.Columns.Add("value");
            cmbFilter.DataSource = fieldTable;
            cmbFilter.ValueMember = "key";
            cmbFilter.DisplayMember = "value";
            fieldTable.Rows.Add("CODE", "Code");
            if(HasArabic)
            fieldTable.Rows.Add("DESC_ENG", "Eng. Name");
            fieldTable.Rows.Add("DESC_ARB", "Arb. Name");
            fieldTable.Rows.Add("TYPE", "Type Code");
            fieldTable.Rows.Add("GROUP", "Group Code");
            fieldTable.Rows.Add("CATEGORY", "Category Code");
            fieldTable.Rows.Add("TRADEMARK", "Trademark Code");
        }

        string From = "";
        public ItemMasterView(string from)
        {
            From = from;
            InitializeComponent();
            //cmd.Connection = conn;
            //adapter.SelectCommand = cmd;
            fieldTable.Columns.Add("key");
            fieldTable.Columns.Add("value");
            cmbFilter.DataSource = fieldTable;
            cmbFilter.ValueMember = "key";
            cmbFilter.DisplayMember = "value";
            fieldTable.Rows.Add("CODE", "Code");
            if (HasArabic)
                fieldTable.Rows.Add("DESC_ENG", "Eng. Name");
            fieldTable.Rows.Add("DESC_ARB", "Arb. Name");
            fieldTable.Rows.Add("TYPE", "Type Code");
            fieldTable.Rows.Add("GROUP", "Group Code");
            fieldTable.Rows.Add("CATEGORY", "Category Code");
            fieldTable.Rows.Add("TRADEMARK", "Trademark Code");
        }

        bool notlastColumn = true;
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == ( Keys.Alt|Keys.S))
            {
                btnSave.Focus();
                if (DialogResult.Yes == MessageBox.Show("Are sure to add new Item", "Confirmation", MessageBoxButtons.YesNo))
                {

                    btnSave.PerformClick();
                    // EditActive = false;
                    return true;
                }
            }
            else if (keyData == Keys.Escape)
            {
                if (From != "")
                {
                    btnQuit.PerformClick();
                    return true;
                }
                else
                {
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
            }
            else if (keyData == Keys.Enter)
            {
                if (this.ActiveControl == cmb_units || this.ActiveControl == cmb_units)
                {
                    enter = true;

                }
                else
                {
                    enter = false;
                }
                if (enter)
                {
                    try
                    {
                    int icolumn = dgRates.CurrentCell.ColumnIndex;
                    int irow = dgRates.CurrentCell.RowIndex;
                    int i = irow;
                   
                    if (icolumn == dgRates.Columns.Count - 1)
                    {
                        //dataGridView1.Rows.Add();
                        if (notlastColumn == true)
                        {
                            int count = dgRates.Columns.Count - 1;
                            int colindex = dgRates.CurrentCell.ColumnIndex;
                            if (count == colindex)
                            {
                                btnSave.Focus();
                            }
                            else
                            {
                                dgRates.CurrentCell = dgRates.Rows[i].Cells[0];
                            }
                        }
                        dgRates.CurrentCell = dgRates[0, irow + 1];
                    }
                    else
                    {
                        dgRates.CurrentCell = dgRates[icolumn + 1, irow];
                    }
                    return true;
                    }
                    catch
                    {
                    }
                }
              
            }


            //else if (keyData == (Keys.Escape))
            //{
            //    dataGridItem.Visible = false;
            //    DESC_ENG.Focus();
            //    return true;
            //}
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public void BindSettings()
        {
            DataTable dt = new DataTable();
            string query = "SELECT DefaultTax,ItemName FROM SYS_SETUP";
            dt = DbFunctions.GetDataTable(query);

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["DefaultTax"] != DBNull.Value && dt.Rows[0]["DefaultTax"]!="")
                {
                    DrpTaxType.SelectedValue = Convert.ToString(dt.Rows[0]["DefaultTax"]);
                    ItemFormat = Convert.ToString(dt.Rows[0]["ItemName"]);
                }
            }
        }
        public void bindProductType()
        {
            Dictionary<string, string> ProductType = new Dictionary<string, string>();
            ProductType.Add("", " <<Select>>");
            ProductType.Add("FGD", "FINISHED GOODS");
            ProductType.Add("MFG", "MANUFACT.GOODS");
            ProductType.Add("RML", "RAW MATERIALS");
            ProductType.Add("SRV", "SERVICE EQP");            
            cmbProductType.DisplayMember = "Value";
            cmbProductType.ValueMember = "Key";
            cmbProductType.DataSource = new BindingSource(ProductType, null);
        }
        private void ItemMasterView_Load(object sender, EventArgs e)
        {
            HasArabic = General.IsEnabled(Settings.Arabic);
            HasType = General.IsEnabled(Settings.HasType);
            HasCategory = General.IsEnabled(Settings.HasCategory);
            HasGroup = General.IsEnabled(Settings.HasGroup);
            HasTM = General.IsEnabled(Settings.HasTM);
            hasTax = General.IsEnabled(Settings.Tax);
            hasBarcode = General.IsEnabled(Settings.Barcode);
            MINIMUM_QTY.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            MAXIMUM_QTY.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            REORDER_QTY.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            COST_PRICE.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            SALE_PRICE.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            if (!HasArabic)
                DESC_ARB.Visible = HasArabic;
            lblarabic.Visible = HasArabic;
            if (!HasType)
            {
                TYPE.Enabled = false;
                btnType.Enabled = false;
                TYPE_NAME.Enabled = false;
            }
            if (!HasCategory)
            {
                CATEGORY.Enabled = false;
                btnCategory.Enabled = false;
                CATEGORY_NAME.Enabled = false;
            }
            if (!HasGroup)
            {
                GROUP.Enabled = false;
                btnGroup.Enabled = false;
                GROUP_NAME.Enabled = false;
            }
            if (!HasTM)
            {
                TRADEMARK.Enabled = false;
                btnTrademark.Enabled = false;
                TRADEMARK_NAME.Enabled = false;
            }
            if (!hasTax)
            {
                kryptonLabel15.Visible = false;

                DrpTaxType.Visible = false;
                btnAddTaxType.Visible = false;
                DrpTaxType.Enabled = false;
                btnAddTaxType.Enabled = false;
            }
            if (!hasBarcode)
            {
                dgRates.Columns["rBarcode"].Visible = false;
            }
            panel_baseqty.Visible = false;
            GetItemLocation();
            loadItems();
           // Generate_Barcode();
            CODE.Text = General.generateItemCode();
            //rename columns
            DataGridViewColumnCollection c = dgItems.Columns;
            c["CODE"].HeaderText = "Code";
            c["DESC_ENG"].HeaderText = "Eng. Name";
            if (HasArabic)
            {
                c["DESC_ARB"].HeaderText = "Arb. Name";
            }
            c["TYPE"].HeaderText = "Type Code";
            c["GROUP"].HeaderText = "Group Code";
            c["CATEGORY"].HeaderText = "Category Code";
            c["TRADEMARK"].HeaderText = "Trademark Code";
            c["COST_PRICE"].HeaderText = "Cost Price";
            c["SALE_PRICE"].HeaderText = "Sale Price";
            c["MINIMUM_QTY"].HeaderText = "Minimum Qty";
            c["MAXIMUM_QTY"].HeaderText = "Maximum Qty";
            c["IN_ACTIVE"].HeaderText = "Is Active";
            //cmd.CommandText = "SELECT CODE,DESC_ENG FROM GEN_PRICE_TYPE";
            //conn.Open();
            SqlDataReader r = Itemdb.GetPriceType();
            try
            {
                while (r.Read())
                {
                    //dgRates.Columns.Add(r["CODE"].ToString(), r["DESC_ENG"].ToString());
                    DataGridViewColumn col = new DataGridViewTextBoxColumn();
                    col.Name = r["CODE"].ToString();
                    col.HeaderText = r["CODE"].ToString();
                    col.Width = 50;
                    dgRates.Columns.Add(col);
                }
                DbFunctions.CloseConnection();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error:" + ex);
            }
            
            GetTaxRates();
            bindgridview();
            BarcodeasItemcode();
            DrpTaxType.SelectedIndex = 0;
            ActiveControl = cmbProductType;
            BindSettings();
            bindUnit();
            if (dataGridItem.Rows.Count > 0)
            {
                dataGridItem.CurrentCell = dataGridItem[2, 0];
            }
            //cmd.CommandText = "SELECT CODE FROM INV_UNIT";
            //adapter.Fill(unitsTable);
            unitsTable = Itemdb.GetCodeInvUnit();
            cmbUnit.DataSource = unitsTable;
            cmbUnit.DisplayMember = "CODE";
            cmbUnit.ValueMember = "CODE";

            DataTable dtSupplier = Itemdb.GetSupplyerForCombo();
            //cmd = new SqlCommand("SELECT CODE,DESC_ENG FROM PAY_SUPPLIER",conn);
            //adapter = new SqlDataAdapter(cmd);
            //adapter.Fill(dtSupplier);
            DataRow row = dtSupplier.NewRow();
            row["DESC_ENG"] = "<<Select>>";
            dtSupplier.Rows.InsertAt(row,0);
            cmbPrimeSupplier.DataSource = dtSupplier;
            cmbPrimeSupplier.DisplayMember = "DESC_ENG";
            cmbPrimeSupplier.ValueMember = "CODE";
            bindProductType();
        }
        public void BarcodeasItemcode()
        {
            if (dgRates.RowCount > 0)
            {
                if (Cb_BracodeSetter.Checked == true)
                {
                    dgRates.Rows[0].Cells[2].Value = CODE.Text;

                }
                if (Cb_BracodeSetter.Checked == false)
                {
                    dgRates.Rows[0].Cells[2].Value = "";
                }
            }
        }
        public void bindUnit()
        {
            //SqlDataAdapter ad = new SqlDataAdapter();
            DataTable dt = Itemdb.GetCodeInvUnit();
            //cmd.CommandText = "SELECT CODE FROM INV_UNIT";
            //cmd.CommandType = CommandType.Text;
            //cmd.Connection = conn;
            //ad.SelectCommand = cmd;
            //ad.Fill(dt);
            cmb_units.DataSource = dt;
            cmb_units.DisplayMember = "CODE";
            cmb_units.ValueMember = "CODE";
      
            
        }
        public void bindUnit1()
        {
            //SqlDataAdapter ad = new SqlDataAdapter();
            DataTable dt = Itemdb.GetCodeInvUnit();
            //cmd.CommandText = "SELECT CODE FROM INV_UNIT";
            //cmd.CommandType = CommandType.Text;
            //cmd.Connection = conn;
            //ad.SelectCommand = cmd;
            //ad.Fill(dt);
            cmbUnit. DataSource = dt;
            cmbUnit.DisplayMember = "CODE";
            cmbUnit.ValueMember = "CODE";


        }
        public void GetItemLocation()
        {
            //cmd.Connection = conn;
            //cmd.CommandText = "SELECT * From GEN_LOCATION";
            //cmd.CommandType = CommandType.Text;
            //DataTable dt = Itemdb.GetLocation();
            //adapter.SelectCommand = cmd;
            //adapter.Fill(dt);
            DataTable dt = Itemdb.GetLocation();
            Drp_Location.DataSource = dt;
            Drp_Location.DisplayMember = "DESC_ENG";
            Drp_Location.ValueMember = "CODE";
            Drp_Location.SelectedIndex = -1;
        }
        public void bindgridview()
        {
            //cmd.Connection = conn;
            //cmd.CommandText = "SELECT * From INV_ITEM_DIRECTORY";
            //cmd.CommandType = CommandType.Text;
            DataTable dt = Itemdb.ItemForGrid();
            //adapter.SelectCommand = cmd;
            //adapter.Fill(dt);
            source1.DataSource = dt;

            dataGridItem.DataSource = source1;

            dataGridItem.RowHeadersVisible = false;
            dataGridItem.Columns[2].Width = 200;
            dataGridItem.Columns[0].Visible = false;

            dataGridItem.Columns[3].Visible = false;

            //dataGridItem.Columns[4].Visible = false;
            //dataGridItem.Columns[5].Visible = false;
            //dataGridItem.Columns[6].Visible = false;
            //dataGridItem.Columns[7].Visible = false;

            dataGridItem.Columns[8].Visible = false;

            //dataGridItem.Columns[9].Visible = false;

            dataGridItem.Columns[10].Visible = false;
            dataGridItem.Columns[11].Visible = false;
            dataGridItem.Columns[12].Visible = false;
            dataGridItem.Columns[13].Visible = false;
            dataGridItem.Columns[14].Visible = false;
            dataGridItem.Columns[15].Visible = false;
            dataGridItem.Columns[16].Visible = false;
            dataGridItem.Columns[17].Visible = false;
            dataGridItem.Columns[18].Visible = false;
            dataGridItem.Columns[19].Visible = false;
            dataGridItem.Columns[20].Visible = false;
            dataGridItem.Columns[21].Visible = false;
            dataGridItem.Columns[22].Visible = false;
            dataGridItem.Columns[23].Visible = false;
            dataGridItem.Columns[24].Visible = false;
            dataGridItem.Columns[25].Visible = false;
            dataGridItem.Columns[26].Visible = false;
            dataGridItem.Columns[27].Visible = false;
            dataGridItem.Columns[28].Visible = false;
            dataGridItem.Columns[29].Visible = false;
            dataGridItem.Columns[30].Visible = false;
            dataGridItem.Columns[31].Visible = false;
            dataGridItem.Columns[32].Visible = false;
            dataGridItem.Columns[33].Visible = false;
            dataGridItem.Columns[34].Visible = false;
            dataGridItem.Columns["SUPPLIER"].Visible = true;
            dataGridItem.ClearSelection();
        }


      

        public void GetTaxRates()
        {
            //cmd.CommandText = "SELECT TaxId, CODE + ' --- ' +CONVERT(varchar(10),TaxRate)+' %' AS Expr1 FROM GEN_TAX_MASTER";
            DataTable dt = Itemdb.GetTaxRate();
            //adapter.Fill(dt);
            DrpTaxType.DataSource = dt;
            DrpTaxType.DisplayMember = "Expr1";
            DrpTaxType.ValueMember = "TaxId";
            DrpTaxType.SelectedIndex = -1;
        }


        private bool valid()
        {
            if (dgRates.Rows.Count <= 0)
            {
                MessageBox.Show("Please add atleast one unit!");
                return false;
            }
            else
            {
                for (int i = 0; i < dgRates.Rows.Count; i++)
                {
                    DataGridViewCellCollection c = dgRates.Rows[i].Cells;

                    if (Convert.ToString(c[0].Value).Equals(""))
                    {
                        MessageBox.Show("Units Cannot be empty, please add unit or remove the " + (i + 1)+" Row.");
                        return false;
                    }

                    for (int j = i + 1; j < dgRates.Rows.Count; j++)
                    {
                        DataGridViewCellCollection cj = dgRates.Rows[j].Cells;
                        if (Convert.ToString(c[0].Value) == Convert.ToString(cj[0].Value))
                        {
                            MessageBox.Show("Duplicate unit found!");
                            return false;
                        }
                    }
                }
            }

            if (MINIMUM_QTY.Text == "")
            {
                MINIMUM_QTY.Text = "0";
            }

            if (MAXIMUM_QTY.Text == "")
            {
                MAXIMUM_QTY.Text = "0";
            }

            if (REORDER_QTY.Text == "")
            {
                REORDER_QTY.Text = "0";
            }

            if (COST_PRICE.Text == "")
            {
                COST_PRICE.Text = "0";
            }
            if (cmbProductType.SelectedIndex <1)
            {
                MessageBox.Show("Select a product type");
                return false;
            }
            if (SALE_PRICE.Text == "")
            {
                SALE_PRICE.Text = "0";
            }

            if (DESC_ENG.Text == "")
            {
                MessageBox.Show("Enter Name of Item");
                return false;
            }
            else
            {
                return true;


            }
            
            //if (TYPE.Text.Trim() != "" && GROUP.Text.Trim() != "" && CATEGORY.Text.Trim() != "" && TRADEMARK.Text.Trim() != "")
            //{
            //    return true;
            //}
            //else
            //{
            //    MessageBox.Show("Please fill the required fields.\n 1.Item Type \n 2.Group \n 3.Category \n 4.Trademark");
            //    return false;
            //}
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            batchId = CODE.Text + "B1";
            if (valid())
            {

                if (General.ItemExists(CODE.Text, ID, "INV_ITEM_DIRECTORY"))
                {
                    MessageBox.Show("An Item with the same code already exists!");
                    return;
                }
                //if (cmbPrimeSupplier.SelectedIndex < 1)
                //{
                //    MessageBox.Show("Please Select Primary Supplier");
                //    return;
                //}


                if (IN_ACTIVE.Checked)
                {
                    inactive = "Y";
                }
                else
                {
                    inactive = "N";
                }

                if (ID == "")
                {
                    //add
                    CODE.Text = General.generateItemCode();
                    // string fields = "CODE,DESC_ENG,DESC_ARB,TYPE,[GROUP],CATEGORY,TRADEMARK,COST_PRICE,SALE_PRICE,MINIMUM_QTY,MAXIMUM_QTY,REORDER_QTY,IN_ACTIVE,TaxId,LOCATION,HASSERIAL,HASWARRENTY,PERIOD,PERIODTYPE";
                    //   string values = "'" + CODE.Text + "','" + DESC_ENG.Text + "','" + DESC_ARB.Text + "','" + TYPE.Text + "','" + GROUP.Text + "','" + CATEGORY.Text + "','" + TRADEMARK.Text + "','" + COST_PRICE.Text + "','" + SALE_PRICE.Text + "','" + MINIMUM_QTY.Text + "','" + MAXIMUM_QTY.Text + "','" + REORDER_QTY.Text + "','" + inactive + "','" + DrpTaxType.SelectedValue + "','" + Drp_Location.SelectedValue + "','" + chKSerial.Checked + "','" + chkWarranty.Checked + "','" +Convert.ToDecimal(txtWarrentyPeriod.Text) + "','" + cmbWarrentyType.Text + "'";
                    update_values();

                    IM.update_item_master(IM, "insert");


                    //  cmd.CommandText = "INSERT INTO INV_ITEM_DIRECTORY("+fields+") VALUES("+values+")";
                    MessageBox.Show("Item Added!");

                    double s = Convert.ToDouble(tb_stock.Text);
                    if (s > 0)
                    {
                       // opening_stock();
                        //insert batch for barcode in opening Stock entry 
                        // MessageBox.Show(dgRates.Rows[0].Cells[7].Value.ToString());
                        // MessageBox.Show(dgRates.Rows[0].Cells[4].Value.ToString());
                        string updatequery = "INSERT INTO RateChange(Item_code,datee,Price,Sale_Price,Qty) VALUES('" + CODE.Text + "',@date,'" + dgRates.Rows[0].Cells[7].Value.ToString() + "','" + dgRates.Rows[0].Cells[4].Value.ToString() + "','" + Convert.ToDecimal(tb_stock.Text) + "' )";
                        Dictionary<string, object> parameters = new Dictionary<string, object>();
                        parameters.Add("@date",DateTime.Now);
                        DbFunctions.InsertUpdate(updatequery, parameters);
                        //   Generate_Barcode();
                        //CODE.Text = General.generateItemCode();
                        //  barcodeprint();
                    }
                }
                else
                {
                    if (txtWarrentyPeriod.Text == "")
                    {
                        txtWarrentyPeriod.Text = "0";
                    }
                    update_values();
                    IM.update_item_master(IM, "update");
                    if (DialogResult.Yes == MessageBox.Show("Are you sure to update Stock", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                    {
                        int s = 0;
                        try
                        {
                            s = Convert.ToInt32(tb_stock.Text);
                        }
                        catch
                        {
                            //can't do anything in catch.
                        }
                        if (s > 0)
                        {
                          //  opening_stock();
                            
                            string updatequery = "INSERT INTO RateChange(Item_code,datee,Price,Sale_Price, Qty) VALUES('" + CODE.Text + "',@date,'" + dgRates.Rows[0].Cells["PUR"].Value.ToString() + "','" + dgRates.Rows[0].Cells[3].Value.ToString() + "','" + Convert.ToDecimal(tb_stock.Text) + "' )";
                            Dictionary<string, object> parameters = new Dictionary<string, object>();
                            parameters.Add("@date", DateTime.Now);
                            DbFunctions.InsertUpdate(updatequery, parameters);
                        }
                        // barcodeprint();
                    }
                    // cmd.CommandText = "UPDATE INV_ITEM_DIRECTORY SET CODE = '" + CODE.Text + "',DESC_ENG = '" + DESC_ENG.Text + "',DESC_ARB = '" + DESC_ARB.Text + "',[TYPE] = '" + TYPE.Text + "',[GROUP] = '" + GROUP.Text + "',CATEGORY = '" + CATEGORY.Text + "',TRADEMARK = '" + TRADEMARK.Text + "',COST_PRICE = '" + COST_PRICE.Text + "',SALE_PRICE = '" + SALE_PRICE.Text + "',MINIMUM_QTY = '" + MINIMUM_QTY.Text + "',MAXIMUM_QTY = '" + MAXIMUM_QTY.Text + "',REORDER_QTY = '" + REORDER_QTY.Text + "',IN_ACTIVE = '" + inactive + "',TaxId='" + DrpTaxType.SelectedValue + "',LOCATION='" + Drp_Location.SelectedValue + "',HASSERIAL='" + chKSerial.Checked + "',HASWARRENTY='" + chkWarranty.Checked + "',PERIOD='" + Convert.ToDecimal(txtWarrentyPeriod.Text) + "',PERIODTYPE='" + cmbWarrentyType.Text + "'" + " WHERE CODE = '" + ID + "';UPDATE INV_PURCHASE_DTL SET ITEM_DESC_ENG='" + DESC_ENG.Text + "' WHERE ITEM_CODE='" + ID + "';UPDATE INV_SALES_DTL SET ITEM_DESC_ENG='" + DESC_ENG.Text + "' WHERE ITEM_CODE='" + ID + "'";
                    MessageBox.Show("Item Updated!");

                    //update
                    sa.bindgridview();
                    pu.bindgridview();
                }


             //   if (!hasBatch)
             //   {
                    if (ID == "")
                    {
                        int next_batch_inc = stock_entry.max_batch_id(CODE.Text);
                        // string query = "DELETE FROM INV_ITEM_PRICE WHERE ITEM_CODE = '" + ID + "';INSERT INTO INV_ITEM_PRICE(ITEM_CODE,SAL_TYPE,PRICE,UNIT_CODE,BATCH_ID,BRANCH,STATUS) ";

                        string query = "INSERT INTO INV_ITEM_PRICE(ITEM_CODE,SAL_TYPE,PRICE,UNIT_CODE,BATCH_ID,BRANCH,STATUS) ";
                        string query1 = "INSERT INTO INV_ITEM_PRICE_DF(ITEM_CODE,SAL_TYPE,PRICE,UNIT_CODE,BATCH_ID,BRANCH,STATUS) ";

                        double MRP = 0;
                        double PUR = 0;
                        int FLAG = 0;
                        for (int i = 0; i < dgRates.Rows.Count; i++)
                        {
                            MRP = 0;
                            PUR = 0;

                            string next_batch = CODE.Text + "B" + next_batch_inc;
                            for (int j = 3; j < dgRates.Columns.Count; j++)
                            {

                                double price = 0;
                                try
                                {

                                    price = Convert.ToDouble(dgRates.Rows[i].Cells[j].Value);
                                }
                                catch
                                {
                                    //nothing to catch.
                                }
                                if (FLAG == 0)
                                    query += "SELECT '" + CODE.Text + "','" + dgRates.Columns[j].Name + "','" + price + "','" + dgRates.Rows[i].Cells[0].Value + "','" + next_batch + "','001',1 UNION ALL ";
                                query1 += "SELECT '" + CODE.Text + "','" + dgRates.Columns[j].Name + "','" + price + "','" + dgRates.Rows[i].Cells[0].Value + "','" + next_batch + "','001',1 UNION ALL ";
                                if (dgRates.Columns[j].Name == "MRP")
                                {
                                    MRP = price;
                                }
                                else if (dgRates.Columns[j].Name == "PUR")
                                {
                                    PUR = price;
                                }
                            }
                            if (FLAG == 0)
                            {
                                //cmd.CommandText = "INSERT INTO tblStock(Item_id, qty, Cost_price, supplier_id, MRP,batch_id,batch_increment) values(@item_id, @qty, @cost_price, @supplier_id, @mrp,@batch_id,@batch_increment)";
                                //cmd.Parameters.Clear();
                                stckdb.ItemId= CODE.Text;
                                stckdb.Qty=Convert.ToDecimal((tb_stock.Text.Equals("") ? "0" : tb_stock.Text));
                                stckdb.CostPrice = Convert.ToDecimal(PUR);
                                stckdb.Mrp = MRP.ToString();
                                string sup_id;
                                if (cmbPrimeSupplier.SelectedIndex > 0)
                                {
                                    sup_id = cmbPrimeSupplier.SelectedValue.ToString();
                                }
                                else
                                {
                                    sup_id = "";
                                }

                                stckdb.SuppId = sup_id;
                                stckdb.BatchId = next_batch;
                                stckdb.BatchIncrement = next_batch_inc;
                                //if (conn.State == ConnectionState.Open)
                                //{
                                //    conn.Close();
                                //}
                                //conn.Open();
                                //cmd.ExecuteNonQuery();
                                //conn.Close();
                                //cmd.Parameters.Clear();
                                stckdb.Insert();
                                if (FLAG == 0)
                                {
                                    if (Convert.ToDecimal(tb_stock.Text) > 0)
                                    {
                                        opening_stock(next_batch);
                                    }
                                }
                                FLAG = 1;
                                next_batch_inc += 1;

                                //conn.Open();
                                query = query.Substring(0, query.Length - 10);
                                //cmd.CommandText = query;
                                DbFunctions.InsertUpdate(query);
                                //cmd.ExecuteNonQuery();
                                //conn.Close();
                            }
                        }
                        //cmd.CommandText = "";
                        //conn.Open();
                        query1 = query1.Substring(0, query1.Length - 10);
                        //cmd.CommandText = query1;
                        //cmd.ExecuteNonQuery();
                        DbFunctions.InsertUpdate(query1);
                        //conn.Close();
                    }
                    else
                    {
                        
                        string query2 = "DELETE FROM INV_ITEM_PRICE_DF WHERE ITEM_CODE = '" + CODE.Text + "';INSERT INTO INV_ITEM_PRICE_DF(ITEM_CODE,SAL_TYPE,PRICE,UNIT_CODE,BATCH_ID,BRANCH,STATUS) ";

                        //double MRP = 0;
                        //double PUR = 0;
                        for (int i = 0; i < dgRates.Rows.Count; i++)
                        {
                            //MRP = 0;
                            //PUR = 0;
                            //string next_batch = CODE.Text + "B" + next_batch_inc;
                            for (int j = 3; j < dgRates.Columns.Count; j++)
                            {

                                double price = 0;
                                try
                                {

                                    price = Convert.ToDouble(dgRates.Rows[i].Cells[j].Value);
                                }
                                catch
                                {
                                    //nothing to catch.
                                }

                                query2 += "SELECT '" + CODE.Text + "','" + dgRates.Columns[j].Name + "','" + price + "','" + dgRates.Rows[i].Cells[0].Value + "','" + CODE.Text + "','001',1 UNION ALL ";

                                //if (dgRates.Columns[j].Name == "MRP")
                                //{
                                //    MRP = price;
                                //}
                                //else if (dgRates.Columns[j].Name == "PUR")
                                //{
                                //    PUR = price;
                                //}
                            }
                        }
                        //conn.Open();
                        query2 = query2.Substring(0, query2.Length - 10);
                        //cmd.CommandText = query2;
                        //cmd.ExecuteNonQuery();
                        DbFunctions.InsertUpdate(query2);
                        //conn.Close();
                        update_mainBatchPrice();
                    }
                    if (ID == "")
                    {

                        ID = CODE.Text;
                    }
                    //if (cb_baseqty.Checked == true)
                    //{
                    //    string q = "INSERT INTO TB_UNITMESSURMENT(ITEM_CODE,ITEM_NAME,CARTOON,BOX,PIECE,DEFAUL) VALUES('" + CODE.Text + "','" + DESC_ENG.Text + "','" + tb_basecar.Text + "','" + tb_basebx.Text + "','" + tb_basepc.Text + "','" + cb_default.SelectedItem.ToString() + "')";
                    //    cmd.CommandText = q;
                    //    cmd.ExecuteNonQuery();
                    //}
                    string itemQuery = "DELETE FROM INV_ITEM_DIRECTORY_UNITS WHERE ITEM_CODE = '" + ID + "';INSERT INTO INV_ITEM_DIRECTORY_UNITS(ITEM_CODE,BARCODE,UNIT_CODE,PACK_SIZE) ";
                    for (int i = 0; i < dgRates.Rows.Count; i++)
                    {
                        DataGridViewCellCollection c = dgRates.Rows[i].Cells;
                        itemQuery += "SELECT '" + ID + "','" + c[2].Value + "','" + c[0].Value + "','" + c[1].Value + "' UNION ALL ";
                    }
                    itemQuery = itemQuery.Substring(0, itemQuery.Length - 10);
                    //cmd.CommandText = itemQuery;
                    //if (conn.State == ConnectionState.Closed)
                    //{
                    //    conn.Open();
                    //}
                    //cmd.ExecuteNonQuery();
                    DbFunctions.InsertUpdate(itemQuery);

                    if (pictureBox1.Image != null)
                    {                        
                        if (pathdirectory != "")
                        {
                            string image = pathdirectory;
                            Bitmap bmp = new Bitmap(image);
                            FileStream fs = new FileStream(image, FileMode.Open, FileAccess.Read);
                            byte[] bimage = new byte[fs.Length];
                            fs.Read(bimage, 0, Convert.ToInt32(fs.Length));
                            fs.Close();
                            itmdrpic.Code = ID;
                            itmdrpic.Picture = bimage;
                            itmdrpic.DeleteInsert();                            
                        }
                    }

                    bindgridview();

                    btnClear.PerformClick();
                    addedfrompuchase = true;
                    
                //}
                loadItems();
                CODE.Text = General.generateItemCode();
                BarcodeasItemcode();
            }
        }
        public void update_mainBatchPrice()
        {
             string id = stock_entry.MinBatchId(CODE.Text);
             if (id != "")
             {
                 string query2 = "DELETE FROM INV_ITEM_PRICE WHERE BATCH_ID = '" + id + "';INSERT INTO INV_ITEM_PRICE(ITEM_CODE,SAL_TYPE,PRICE,UNIT_CODE,BATCH_ID,BRANCH,STATUS) ";

                 double MRP = 0;
                 double PUR = 0;
               
                     //MRP = 0;
                     //PUR = 0;
                     //string next_batch = CODE.Text + "B" + next_batch_inc;
                     for (int j = 3; j < dgRates.Columns.Count; j++)
                     {

                         
                         double price = 0;
                        
                         try
                         {

                             price = Convert.ToDouble(dgRates.Rows[0].Cells[j].Value);
                         }
                         catch
                         {
                             //nothing to catch.
                         }
                         if (dgRates.Columns[j].Name == "MRP")
                         {
                             MRP = price;
                         }
                         else if (dgRates.Columns[j].Name == "PUR")
                         {
                             PUR = price;
                         }
                         query2 += "SELECT '" + CODE.Text + "','" + dgRates.Columns[j].Name + "','" + price + "','" + dgRates.Rows[0].Cells[0].Value + "','" + id + "','001',1 UNION ALL ";

                         //if (dgRates.Columns[j].Name == "MRP")
                         //{
                         //    MRP = price;
                         //}
                         //else if (dgRates.Columns[j].Name == "PUR")
                         //{
                         //    PUR = price;
                         //}
                     }
                 
                 //conn.Open();
                 query2 = query2.Substring(0, query2.Length - 10);
                 DbFunctions.InsertUpdate(query2);
                 //cmd.CommandText = query2;
                 //cmd.ExecuteNonQuery();
                 //conn.Close();
                 //if (conn.State == ConnectionState.Open)
                 //{
                 //    conn.Close();
                 //}
                
                 //cmd.CommandText = "update tblstock set Cost_price=@cost,MRP=@MRP WHERE batch_id='"+id+"'";
                 //conn.Open();
                 //cmd.Connection = conn;
                 //cmd.Parameters.Clear();
                 //cmd.Parameters.AddWithValue("@cost", PUR);
                 //cmd.Parameters.AddWithValue("@MRP", MRP);
                 //cmd.ExecuteNonQuery();
                 //conn.Close();

                 stckdb.BatchId = id;
                 stckdb.CostPrice = Convert.ToDecimal(PUR);
                 stckdb.Mrp = MRP.ToString(); ;
                 stckdb.UpdateCostMRP();

             }
        }
        public void barcodeprint()
        {
            Barcode_Stock_Items br = (Barcode_Stock_Items)Application.OpenForms["Barcode_Stock_Items"];
            try
            {
              ExportToPDF();
            }
            catch
            {
                MessageBox.Show("Erro! in initial export");
            }
        }

        public void ExportToPDF()
        {
          
            //iTextSharp.text.Rectangle rect = PageSize.GetRectangle("A4");

            //iTextSharp.text.Document pdfdoc = new iTextSharp.text.Document(new iTextSharp.text.Rectangle(rect.Width, rect.Height), 0, 0, 17, -35);
            //string batch = "";
            //try
            //{
            //    DirectoryInfo dir1 = new DirectoryInfo(Application.StartupPath + "\\Barcode");
            //    if (!Directory.Exists(Application.StartupPath + "\\Barcode"))
            //    {
            //        dir1.Create();
            //    }
            //    if (File.Exists(Application.StartupPath + "\\Barcode\\Barcode1.pdf"))
            //    {
            //        File.Delete(Application.StartupPath + "\\Barcode\\Barcode1.pdf");
            //    }

            //    //pdfdoc = new Document(PageSize.A4, -2, 20, -1, 20);
            //    PdfWriter writer = PdfWriter.GetInstance(pdfdoc, new FileStream(Application.StartupPath + "\\Barcode\\Barcode1.pdf", FileMode.Create));
            //    PdfPTable tbl = new PdfPTable(5);
            //    tbl.WidthPercentage = 100;
               
            //    if (conn.State == ConnectionState.Open)
            //    {
            //    }
            //    else
            //    {

            //        conn.Open();
            //    }
            //    cmd.Connection = conn;
            //    cmd.CommandText = "select max(R_id)as batch FROM RateChange Where Item_code='" + CODE.Text + "'";
            //    cmd.CommandType = CommandType.Text;
            //    SqlDataReader rd5;
            //    rd5 = cmd.ExecuteReader();
            //    while (rd5.Read())
            //    {
            //        batch = (Convert.ToInt32(rd5[0])).ToString();
            //    }
            //    conn.Close();

            //    //   float[] widths = new float[] {1f, 1f,3f,1f,1f };
            //    //  tbl.SetWidths(widths);
            //    tbl.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            //    tbl.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            //    tbl.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;

            //    pdfdoc.Open();
            //    int intotalCount = 0;
            //    Class.BarcodeSettings Info = new Class.BarcodeSettings();


            //    if (CODE.Text != null && CODE.Text != "")
            //    {
            //        int inCopies = 0;

            //        int.TryParse(tb_stock.Text, out inCopies); // number of copies of arcode to be printed

            //        for (int j = 0; j < inCopies; j++)
            //        {
            //            string strProductCode = string.Empty;
            //            string strCode = string.Empty;
            //            string strCompanyName = string.Empty;
            //            string strBarcodeValue = string.Empty;
            //            string strProductName = string.Empty;
            //            string RetailPrice = string.Empty;


            //            strProductCode = batch;


            //            strProductName = DESC_ENG.Text;



            //            strBarcodeValue = batch;


            //            strCompanyName = companyname;



                      
            //            string strMRP = string.Empty;


            //            //  strMRP = PriceType+":"+ dgbarcodeprint.Rows[i].Cells["Rate"].Value.ToString();
                         
            //            strMRP = "INR. " + "122"; //dgbarcodeprint.Rows[i].Cells["Rate_"].Value.ToString();

            //            string strSecretPurchaseRateCode = string.Empty;

            //            PdfContentByte pdfcb = writer.DirectContent;
            //            Barcode128 code128 = new Barcode128();
            //            code128.Code = strBarcodeValue;
            //            code128.Extended = false;
            //            code128.CodeType = iTextSharp.text.pdf.Barcode.CODE128;
            //            code128.BarHeight = 15;

            //            if (IsBarcodeValue == false)
            //            {
            //                code128.Font = null;
            //                code128.AltText = strBarcodeValue;
            //            }
                     

            //            code128.BarHeight = HEIGHT;

            //            code128.Size = 8;
            //            code128.Baseline = 8;
            //            code128.TextAlignment = Element.ALIGN_CENTER;
            //            iTextSharp.text.Image image128 = code128.CreateImageWithBarcode(pdfcb, null, null);

            //            Phrase phrase = new Phrase();
            //            phrase.Font.Size = 7f;


            //            phrase.Add(new Chunk(companyname + Environment.NewLine));


            //            phrase.Add(new Chunk(strProductName + Environment.NewLine));
            //            phrase.Add(new Chunk(Environment.NewLine));
            //            PdfPCell cell = new PdfPCell(phrase);
            //            //     cell.FixedHeight = 80.69f;

            //            cell.PaddingRight = -10;
            //            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            //            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            //            cell.Border = iTextSharp.text.Rectangle.NO_BORDER;

            //            //cell.PaddingBottom=20;
            //            // 

            //            phrase.Add(new Chunk(image128, 0, 0));
            //            //     phrase.Add(new Chunk(Environment.NewLine));

            //            phrase.Add(new Chunk(Environment.NewLine + strMRP));


            //            phrase.Add(new Chunk(Environment.NewLine + strCode));

            //            phrase.Add(new Chunk(Environment.NewLine));
            //            // phrase.Add(new Chunk(Environment.NewLine));
            //            cell.PaddingRight = 3;
            //            tbl.AddCell(cell);

            //            intotalCount++;
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
            //}
            //catch (Exception ex)
            //{
            //    if (ex.Message.Contains("The process cannot access the file") && ex.Message.Contains("Barcode.pdf' because it is being used by another process."))
            //    {
            //        MessageBox.Show("Close the PDF file and try again", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    }
            //    else
            //    {
            //        MessageBox.Show("Error:" + ex.Message, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    }
            //}
            //finally
            //{
            //    try
            //    {
            //        pdfdoc.Close();
            //    }
            //    catch
            //    {
            //    }
            //}
         iTextSharp.text.Rectangle rect = PageSize.GetRectangle("A4");
           
            iTextSharp.text.Document pdfdoc = new iTextSharp.text.Document(new iTextSharp.text.Rectangle(rect.Width,rect.Height), 0, 0, 17, -35);
        string batch="";
        //conn.Open();
        //cmd.Connection = conn;
        //cmd.CommandText = "select max(R_id)as batch FROM RateChange Where Item_code='" + CODE.Text + "'";
        //cmd.CommandType = CommandType.Text;        
        //rd5 = cmd.ExecuteReader();
        ratechngdb.ItemCode = CODE.Text;
        SqlDataReader rd5 = ratechngdb.BatchByItemCode();

        while (rd5.Read())
        {
            batch = CODE.Text + (Convert.ToString(rd5[0]));
        }
        DbFunctions.CloseConnection();

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

                for (int i = 0; i <Convert.ToInt32(tb_stock.Text); i++)
                {
                    if (CODE.Text != null && CODE.Text != "")
                    {
                        int inCopies = 0;
                        if (tb_stock.Text != null)
                        {
                            int.TryParse(CODE.Text , out inCopies); // number of copies of arcode to be printed
                        }
                        //for (int j = 0; j < Convert.ToInt32(tb_stock.Text); j++)
                        //{
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
                                strProductCode =DESC_ENG.Text;
                            }

                            strProductName =DESC_ENG.Text;


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
                                strMRP = "INR. " + dgRates.Rows[0].Cells[4].Value.ToString();
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
                            phrase.Font.Size =7f;

                            if (IsCompany == true)
                            {
                                phrase.Add(new Chunk(companyname + Environment.NewLine ));
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
                        //}


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
                    pdfdoc.SetMargins(0,0, 17,-35);
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
        private void opening_stock(string BATCH)
        {
            //conn.Open();
            //cmd.Connection = conn;
            string docno = General.generateStockID();
            string query = "";
            InvStkTrxHdrDB.DocNo = docno;
            InvStkTrxHdrDB.DocDateGre = System.DateTime.Now;
            InvStkTrxHdrDB.DocType = "INV.STK.OPN";
            InvStkTrxHdrDB.Branch = lg.Branch;
            InvStkTrxHdrDB.Insert();


            //query = "INSERT INTO INV_STK_TRX_HDR(DOC_NO,DOC_DATE_GRE,DOC_TYPE,BRANCH) VALUES('" + docno + "','" + System.DateTime.Now.ToString("MM/dd/yyyy") + "','INV.STK.OPN','" + lg.Branch + "');";
            //query = "INSERT INTO INV_STK_TRX_DTL(DOC_TYPE,DOC_NO,ITEM_CODE,ITEM_DESC_ENG,UOM,PRICE,QUANTITY,BRANCH,PRICE_BATCH,UOM_QTY) VALUES('INV.STK.OPN','" + docno + "','" + CODE.Text + "','" + DESC_ENG.Text + "','" + dgRates.Rows[0].Cells[0].Value.ToString() + "','" + Convert.ToDecimal(dgRates.Rows[0].Cells["PUR"].Value.ToString()) + "','" + Convert.ToDouble (tb_stock.Text )+ "','" + lg.Branch + "','"+BATCH+"',1) ";
            
            InvStkTrxDtlDB.DocType="INV.STK.OPN";
            InvStkTrxDtlDB.DocNo=docno;
            InvStkTrxDtlDB.ItemCode=CODE.Text;
            InvStkTrxDtlDB.ItemDescEng = DESC_ENG.Text;
            InvStkTrxDtlDB.Uom = dgRates.Rows[0].Cells[0].Value.ToString();
            InvStkTrxDtlDB.Price = Convert.ToDecimal(dgRates.Rows[0].Cells["PUR"].Value.ToString());
            InvStkTrxDtlDB.Quantity = Convert.ToDouble(tb_stock.Text);
            InvStkTrxDtlDB.Branch = lg.Branch;
            InvStkTrxDtlDB.PriceBatch = BATCH;
            InvStkTrxDtlDB.UomQty=1;

            //cmd.ExecuteNonQuery();
            //conn.Close();
            InvStkTrxDtlDB.Insert();
        }

        private void update_values()
        {

            IM.CODE = CODE.Text;
            IM.DESC_ENG = DESC_ENG.Text;
            IM.DESC_ARB = DESC_ARB.Text;
            IM.TYPE = TYPE.Text;
            IM.GROUP = GROUP.Text;
            IM.CATEGORY = CATEGORY.Text;
            IM.TRADEMARK = TRADEMARK.Text;
            if (cmbPrimeSupplier.SelectedIndex > 0)
            {
                IM.Supplier = cmbPrimeSupplier.SelectedValue.ToString();
            }
            else
            {
                IM.Supplier = "";
            }
            IM.COST_PRICE = Convert.ToDecimal(COST_PRICE.Text);
            IM.SALE_PRICE = Convert.ToDecimal(SALE_PRICE.Text);
            IM.MINIMUM_QTY = Convert.ToInt32(MINIMUM_QTY.Text);
            IM.MAXIMUM_QTY = Convert.ToInt32(MAXIMUM_QTY.Text);
            IM.REORDER_QTY = Convert.ToInt32(REORDER_QTY.Text);
            IM.IN_ACTIVE = inactive;
            IM.productType = cmbProductType.SelectedValue.ToString();
            IM.TaxId = Convert.ToInt32(DrpTaxType.SelectedValue);
            if (Drp_Location.SelectedIndex < 0)
                IM.LOCATION = "";
            else
                IM.LOCATION = Drp_Location.Text;
            IM.HASSERIAL = Convert.ToDecimal(chKSerial.Checked);
            if (chkWarranty.Checked == true)
            {
                IM.HASWARRENTY = Convert.ToDecimal(chkWarranty.Checked);
                IM.PERIOD = Convert.ToDecimal(txtWarrentyPeriod.Text);
                IM.PERIODTYPE = cmbWarrentyType.Text;
            }
            else
            {
                IM.HASWARRENTY = 0;
                IM.PERIOD = 0;
                IM.PERIODTYPE = "0";
            }
            IM.ID = ID;
            IM.ITEM_DESC_ENG = DESC_ENG.Text;

            IM.hsn = txt_HSN.Text;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ID = "";
            CODE.Text = "";
            DESC_ENG.Text = "";
            DESC_ARB.Text = "";
            TYPE.Text = "";
            TYPE_NAME.Text = "";
            GROUP.Text = "";
            GROUP_NAME.Text = "";
            CATEGORY.Text = "";
            CATEGORY_NAME.Text = "";
            Drp_Location.Text = "";
            TRADEMARK.Text = "";
            pictureBox1.Image = null;
            TRADEMARK_NAME.Text = "";
            MINIMUM_QTY.Text = "0";
            MAXIMUM_QTY.Text = "0";
            REORDER_QTY.Text = "0";
            COST_PRICE.Text = "0";
            SALE_PRICE.Text = "0";
            txt_MRP.Text = "0";
            txt_wholesale.Text = "0";
            tb_stock.Text = "0";
            txt_HSN.Text = "";
          //  DrpTaxType.SelectedIndex = 0;
            IN_ACTIVE.Checked = true;
            dgRates.Rows.Clear();
            btn_editfornew.Visible = false;
            txtWarrentyPeriod.Text = "0";
            cmbWarrentyType.Text = "";
            chkWarranty.Checked = false;
            chKSerial.Checked = false;
            DESC_ENG.Focus();
            CODE.Text = General.generateItemCode();
            cmbUnit.Visible = false;
            txtUnitProperty.Visible = false;
            cmbProductType.SelectedIndex = 0;
        }
        public void Clear()
        {
            ID = "";
            CODE.Text = "";
            DESC_ENG.Text = "";
            DESC_ARB.Text = "";
            TYPE.Text = "";
            TYPE_NAME.Text = "";
            GROUP.Text = "";
            GROUP_NAME.Text = "";
            CATEGORY.Text = "";
            CATEGORY_NAME.Text = "";
            TRADEMARK.Text = "";
            Drp_Location.Text = "";
            pictureBox1.Image = null;
            TRADEMARK_NAME.Text = "";
            MINIMUM_QTY.Text = "0";
            MAXIMUM_QTY.Text = "0";
            REORDER_QTY.Text = "0";
            COST_PRICE.Text = "0";
            SALE_PRICE.Text = "0";
            tb_stock.Text = "0.00";
            txt_HSN.Text = "";
            //  DrpTaxType.SelectedIndex = 0;
            IN_ACTIVE.Checked = true;
            dgRates.Rows.Clear();
            btn_editfornew.Visible = false;
            txtWarrentyPeriod.Text = "0";
            cmbWarrentyType.Text = "";
            chkWarranty.Checked = false;
            chKSerial.Checked = false;
            DESC_ENG.Focus();     
        }
        public void Clear_ListSelection()
        {
            ID = "";
            CODE.Text = "";
           // DESC_ENG.Text = "";
         //   DESC_ARB.Text = "";
            TYPE.Text = "";
            TYPE_NAME.Text = "";
            GROUP.Text = "";
            GROUP_NAME.Text = "";
            CATEGORY.Text = "";
            CATEGORY_NAME.Text = "";
            TRADEMARK.Text = "";
            Drp_Location.Text = "";
            pictureBox1.Image = null;
            TRADEMARK_NAME.Text = "";
            MINIMUM_QTY.Text = "0";
            MAXIMUM_QTY.Text = "0";
            REORDER_QTY.Text = "0";
            COST_PRICE.Text = "0";
            SALE_PRICE.Text = "0";
            tb_stock.Text = "0.00";
            txt_HSN.Text = "";
            //  DrpTaxType.SelectedIndex = 0;
            IN_ACTIVE.Checked = true;
            dgRates.Rows.Clear();
            btn_editfornew.Visible = false;
            txtWarrentyPeriod.Text = "0";
            cmbWarrentyType.Text = "";
            chkWarranty.Checked = false;
            chKSerial.Checked = false;
          //  DESC_ENG.Focus();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            ////delete
            //btnClear.PerformClick();
            //ItemMasterHelp h = new ItemMasterHelp(1);
            //h.ShowDialog();
            if (dataGridItem.Rows.Count > 0 && dataGridItem.CurrentRow != null && MessageBox.Show("Are you sure?", "Item Deletion", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                //DataGridViewRow row = CustomersGridView.SelectedRow;

                //// In this example, the first column (index 0) contains
                //TextBox1.Text = row.Cells[0].Text;
                string st = dataGridItem.CurrentRow.Cells["CODE"].Value.ToString();
                int value = Itemdb.IsProductUsed(st);
                //conn.Open();
                //cmd.Parameters.Clear();
                //cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandText = "IsProductUsed";
                //cmd.Parameters.AddWithValue("@item_code", st);
                //    int value = Convert.ToInt32(cmd.ExecuteScalar());                
                //cmd.CommandType = CommandType.Text;

                if (value >= 1)
                {
                    MessageBox.Show("Item cannot be deleted, since it is used in purchase and sales!");
                    return;
                }

                delete();
                //cmd.CommandText = "DELETE FROM INV_ITEM_DIRECTORY WHERE CODE = '" + dataGridItem.CurrentRow.Cells["CODE"].Value.ToString() + "'";
                //cmd.ExecuteNonQuery();
                //cmd.CommandText = "DELETE FROM INV_ITEM_DIRECTORY_PICTURE WHERE CODE = '" + dataGridItem.CurrentRow.Cells["CODE"].Value.ToString() + "'";
                //cmd.ExecuteNonQuery();
                //cmd.CommandText = "DELETE FROM INV_ITEM_PRICE WHERE ITEM_CODE='" + dataGridItem.CurrentRow.Cells["CODE"].Value.ToString() + "'";
                //cmd.ExecuteNonQuery();
                //cmd.CommandText = "DELETE FROM INV_ITEM_PRICE_DF WHERE ITEM_CODE='" + dataGridItem.CurrentRow.Cells["CODE"].Value.ToString() + "'";
                //cmd.ExecuteNonQuery();
                //cmd.CommandText = "DELETE FROM tblStock WHERE Item_id='" + dataGridItem.CurrentRow.Cells["CODE"].Value.ToString() + "'";
                //cmd.ExecuteNonQuery();
                //cmd.CommandText = "DELETE FROM INV_STK_TRX_DTL WHERE ITEM_CODE='" + dataGridItem.CurrentRow.Cells["CODE"].Value.ToString() + "'";
                //cmd.ExecuteNonQuery();
                //cmd.CommandText = "DELETE FROM INV_STK_TRX_HDR WHERE DOC_NO='" + Doc_no + "'";
                //cmd.ExecuteNonQuery();
                Itemdb.DeleteItem(dataGridItem.CurrentRow.Cells["CODE"].Value.ToString(), Doc_no);
                MessageBox.Show("Item Deleted!");
                Clear();
                dataGridItem.Rows.Remove(dataGridItem.CurrentRow);
                
            }
        }
        string Doc_no = "";
        public void delete()
        {
            InvStkTrxDtlDB.ItemCode = dataGridItem.CurrentRow.Cells["CODE"].Value.ToString();
            DataTable dt = InvStkTrxDtlDB.DocNobyItemCode();
            //cmd.CommandText = "SELECT (DOC_NO) FROM INV_STK_TRX_DTL WHERE ITEM_CODE='" + dataGridItem.CurrentRow.Cells["CODE"].Value.ToString() + "'";
            //SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                Doc_no = dt.Rows[0]["DOC_NO"].ToString();
            }
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            if (From == "" && lg.Theme == "1")
            {
                ComponentFactory.Krypton.Navigator.KryptonPage kp = new ComponentFactory.Krypton.Navigator.KryptonPage();
                mdi.maindocpanel.SelectedPage.Dispose();
            }
            else
            {
                this.Close();
            }
        }

        private void btnCode_Click(object sender, EventArgs e)
        {
            ItemMasterHelp h = new ItemMasterHelp(0);
            if (h.ShowDialog() == DialogResult.OK && h.c != null)
            {
                ID = Convert.ToString(h.c["CODE"].Value);
                CODE.Text = Convert.ToString(h.c["CODE"].Value);
                DESC_ENG.Text = Convert.ToString(h.c["DESC_ENG"].Value);
                DESC_ARB.Text = Convert.ToString(h.c["DESC_ARB"].Value);
                TYPE.Text = Convert.ToString(h.c["TYPE"].Value);
                GROUP.Text = Convert.ToString(h.c["GROUP"].Value);
                CATEGORY.Text = Convert.ToString(h.c["CATEGORY"].Value);
                TRADEMARK.Text = Convert.ToString(h.c["TRADEMARK"].Value);
                COST_PRICE.Text = Convert.ToString(h.c["COST_PRICE"].Value);
                SALE_PRICE.Text = Convert.ToString(h.c["SALE_PRICE"].Value);
                MINIMUM_QTY.Text = Convert.ToString(h.c["MINIMUM_QTY"].Value);
                MAXIMUM_QTY.Text = Convert.ToString(h.c["MAXIMUM_QTY"].Value);
                REORDER_QTY.Text = Convert.ToString(h.c["REORDER_QTY"].Value);
               
                if (Convert.ToString(h.c["IN_ACTIVE"].Value) == "Yes")
                {
                    IN_ACTIVE.Checked = true;
                }
                else
                {
                    IN_ACTIVE.Checked = false;
                }
                DrpTaxType.SelectedValue= Convert.ToInt32(h.c["TaxId"].Value);
                Drp_Location.SelectedValue = h.c["LOCATION"].Value;
                chKSerial.Checked =Convert.ToBoolean(h.c["HASSERIAL"].Value);
                chkWarranty.Checked = Convert.ToBoolean(h.c["HASWARRENTY"].Value);
                txtWarrentyPeriod.Text = Convert.ToString(h.c["PERIOD"].Value);
               cmbWarrentyType.Text= Convert.ToString(h.c["PERIODTYPE"].Value);
                getItemData();
                
            }

        }

        public void getItemData()
        {
 
            InvItemDirectoryUnits.ItemCode = ID;
            //conn.Open();
            //cmd.CommandText = "SELECT UNIT_CODE,PACK_SIZE,BARCODE FROM INV_ITEM_DIRECTORY_UNITS WHERE ITEM_CODE = '" + ID + "'";
            SqlDataReader r = InvItemDirectoryUnits.GetDataByItemCode();

            dgRates.Rows.Clear();
            while (r.Read())
            {
                dgRates.Rows.Add(r["UNIT_CODE"], r["PACK_SIZE"], r["BARCODE"]);
                //dgRates.CurrentRow.Cells[0].Value = r["UNIT_CODE"].ToString();
            }
            DbFunctions.CloseConnection();
           // if (!hasBatch)
          //  {
                string[] priceColumnNames = new string[dgRates.ColumnCount];
                for (int i = 0; i < dgRates.ColumnCount; i++)
                {
                    priceColumnNames[i] = dgRates.Columns[i].Name;
                }


                //conn.Open();
                //cmd.CommandText = "SELECT UNIT_CODE,SAL_TYPE,PRICE FROM INV_ITEM_PRICE_DF WHERE ITEM_CODE = '" + ID + "'";
                InvItemTypeDFDB.ItemCode = ID;
                SqlDataReader r1 = InvItemTypeDFDB.GetDatabyItemCode();

                while (r1.Read())
                {
                    for (int i = 0; i < dgRates.Rows.Count; i++)
                    {
                        DataGridViewCellCollection c = dgRates.Rows[i].Cells;
                        if (Convert.ToString(r1["UNIT_CODE"]).Equals(Convert.ToString(c[0].Value)))
                        {
                            if (priceColumnNames.Contains(Convert.ToString(r1["SAL_TYPE"])))
                            {
                                c[Convert.ToString(r1["SAL_TYPE"])].Value = r1["PRICE"];
                            }
                        }
                    }
                }

                DbFunctions.CloseConnection();

                if (dgRates.Rows.Count > 0)
                {
                    if (dgRates.Rows[0].Cells["MRP"].Value != null)
                    {
                        txt_MRP.Text = dgRates.Rows[0].Cells["MRP"].Value.ToString();
                    }
                    if (dgRates.Rows[0].Cells["WHL"].Value != null)
                    {
                        txt_wholesale.Text = dgRates.Rows[0].Cells["WHL"].Value.ToString();
                    }
                }
                //cmd.CommandText = "SELECT Picture from INV_ITEM_DIRECTORY_PICTURE where Code = '" + ID + "'";
                itmdrpic.Code = ID;;
                //adapter.SelectCommand = cmd;
                DataSet ds = itmdrpic.PictureByCode();
                //adapter.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    byte[] myImage = new byte[0];
                    myImage = (byte[])ds.Tables[0].Rows[0]["Picture"];
                    MemoryStream stream = new MemoryStream(myImage);
                }
                else
                {
                    pictureBox1.Image = null;
                }
                btn_editfornew.Visible = true;
            // }
            if (dgRates.RowCount > 0)
            {
                if (dgRates.Rows[0].Cells[0].Value != "")
                {
                    cmbUnit.SelectedValue = dgRates.Rows[0].Cells[0].Value.ToString();
                }
            }
        }
        
        private void btntype_Click(object sender, EventArgs e)
        {
            CommonHelp h = new CommonHelp(0, genEnum.Type);
            if (h.ShowDialog() == DialogResult.OK && h.c != null)
            {
                TYPE.Text = Convert.ToString(h.c["CODE"].Value);
                TYPE_NAME.Text = Convert.ToString(h.c["DESC_ENG"].Value);
                if (GROUP.Enabled == true)
                    GROUP.Focus();
                else if (TRADEMARK.Enabled == true)
                    TRADEMARK.Focus();
                else
                    cmbPrimeSupplier.Focus();
                //btnGroup.Focus();
            }
        }

        private void btnGroup_Click(object sender, EventArgs e)
        {
            CommonHelp h = new CommonHelp(0, genEnum.Group);
            if (h.ShowDialog() == DialogResult.OK && h.c != null)
            {
                GROUP.Text = Convert.ToString(h.c["CODE"].Value);
                GROUP_NAME.Text = Convert.ToString(h.c["DESC_ENG"].Value);
                if (TRADEMARK.Enabled == true)
                    TRADEMARK.Focus();
                else
                    cmbPrimeSupplier.Focus();
                //btnTrademark.Focus();
            }
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            CommonHelp h = new CommonHelp(0, genEnum.Category);
            if (h.ShowDialog() == DialogResult.OK && h.c != null)
            {
                CATEGORY.Text = Convert.ToString(h.c["CODE"].Value);
                CATEGORY_NAME.Text = Convert.ToString(h.c["DESC_ENG"].Value);
                if (TYPE.Enabled == true)
                    TYPE.Focus();
                else if (GROUP.Enabled == true)
                    GROUP.Focus();
                else if (TRADEMARK.Enabled == true)
                    TRADEMARK.Focus();
                else
                    cmbPrimeSupplier.Focus();
              //  btnType.Focus();
            }
        }

        private void btnTrademark_Click(object sender, EventArgs e)
        {
            CommonHelp h = new CommonHelp(0, genEnum.TradeMark);
            if (h.ShowDialog() == DialogResult.OK && h.c != null)
            {
                TRADEMARK.Text = Convert.ToString(h.c["CODE"].Value);
                TRADEMARK_NAME.Text = Convert.ToString(h.c["DESC_ENG"].Value);
                cmbPrimeSupplier.Focus();
            }
        }

        private void TYPE_TextChanged(object sender, EventArgs e)
        {
            if (TYPE.Text.Length == 3)
            {
                string name = General.getName(TYPE.Text, "INV_ITEM_TYPE");
                if (name != "")
	            {
            		 TYPE_NAME.Text = name;
	            }
                else
                {
                    TYPE.Text = "";
                    TYPE_NAME.Text = "";
                    MessageBox.Show("Type Not Found. Please Enter Correct Code!");
                }
            }
        }

        private void GROUP_TextChanged(object sender, EventArgs e)
        {
            if (GROUP.Text.Length == 3)
            {
                string name = General.getName(GROUP.Text, "INV_ITEM_GROUP");
                if (name != "")
                {
                    GROUP_NAME.Text = name;
                }
                else
                {
                    GROUP.Text = "";
                    GROUP_NAME.Text = "";
                    MessageBox.Show("Group Not Found. Please Enter Correct Code!");
                }
            }
            
        }

        private void CATEGORY_TextChanged(object sender, EventArgs e)
        {
            if (CATEGORY.Text.Length == 3)
            {
                string name = General.getName(CATEGORY.Text, "INV_ITEM_CATEGORY");
                if (name != "")
                {
                    CATEGORY_NAME.Text = name;
                }
                else
                {
                    CATEGORY.Text = "";
                    CATEGORY_NAME.Text = "";
                    MessageBox.Show("Category Not Found. Please Enter Correct Code!");
                }
            }
            
        }

        private void TRADEMARK_TextChanged(object sender, EventArgs e)
        {
            if (TRADEMARK.Text.Length == 3)
            {
                string name = General.getName(TRADEMARK.Text, "INV_ITEM_TM");
                if (name != "")
                {
                    TRADEMARK_NAME.Text = name;
                }
                else
                {
                    TRADEMARK.Text = "";
                    TRADEMARK_NAME.Text = "";
                    MessageBox.Show("TradeMark Not Found. Please Enter Correct Code!");
                }
            }
            
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            if (txtFilter.Text == "")
            {
                source.Filter = "";
            }
            else
            {
                source.Filter = cmbFilter.SelectedValue + " LIKE '" + txtFilter.Text + "%'";
            }
        }

        private void btn2Quit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgRates_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgRates.CurrentCell.ColumnIndex != 0)
            {
                DataGridViewTextBoxEditingControl txtBox = (e.Control as DataGridViewTextBoxEditingControl);
                if (dgRates.CurrentCell.ColumnIndex == 1 || dgRates.CurrentCell.ColumnIndex > 2)
                {
                    if (!fQty)
                    {
                        txtBox.KeyPress += new KeyPressEventHandler(General.CellOnlyFloat);
                        fQty = true;
                    }
                }
                else
                {
                    if (fQty)
                    {
                        txtBox.KeyPress -= new KeyPressEventHandler(General.CellOnlyFloat);
                        fQty = false;
                    }
                }
            }
            else
            {
                fQty = false;
            }
        }

        private void loadItems()
        {
            table.Clear();
            //if(HasArabic)
            //cmd.CommandText = "SELECT CODE,DESC_ENG,DESC_ARB,TYPE,[GROUP],CATEGORY,TRADEMARK,COST_PRICE,SALE_PRICE,MINIMUM_QTY,MAXIMUM_QTY,IN_ACTIVE,TaxId FROM INV_ITEM_DIRECTORY";
            //else
            //    cmd.CommandText = "SELECT CODE,DESC_ENG,TYPE,[GROUP],CATEGORY,TRADEMARK,COST_PRICE,SALE_PRICE,MINIMUM_QTY,MAXIMUM_QTY,IN_ACTIVE,TaxId FROM INV_ITEM_DIRECTORY";
            table = Itemdb.GetAllData(HasArabic);
            //adapter.Fill(table);
            source.DataSource = table;
            dgItems.DataSource = source;
        }

        private void linkRefresh_LinkClicked(object sender, EventArgs e)
        {
            loadItems();
        }

        private void linkRemoveUnit_LinkClicked(object sender, EventArgs e)
        {
            if (dgRates.Rows.Count > 0 && dgRates.CurrentRow != null && dgRates.NewRowIndex != dgRates.CurrentRow.Index)
            {
                dgRates.Rows.Remove(dgRates.CurrentRow);
            }
        }

        private void CODE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1||e.KeyCode==Keys.Enter)
            {
                btnCode.PerformClick();
            }
        }

        private void TYPE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                btnType.PerformClick();
            }
            else if (e.KeyCode == Keys.Back)
            {
                if (TYPE.Text == "")
                {
                    TYPE_NAME.Text = "";
                }
            }
            else if (e.KeyCode == Keys.Enter)
            {
                if (HasGroup)
                {
                    GROUP.Focus();
                }
                else if (HasTM)
                {
                    TRADEMARK.Focus();
                }
                else
                {
                    MINIMUM_QTY.Focus();
                }
            }
            else
            {
                btnType.PerformClick();
            }
            
        }

        private void GROUP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1 )
            {
                btnGroup.PerformClick();
            }
            else if (e.KeyCode == Keys.Back)
            {
                if (GROUP.Text == "")
                {
                    GROUP_NAME.Text = "";
                }
            }
            else if (e.KeyCode == Keys.Enter)
            {
                if (HasTM)
                {
                    TRADEMARK.Focus();
                }
                else
                {
                    MINIMUM_QTY.Focus();
                }
            }
            else
            {
                btnGroup.PerformClick();
            }
        }

        private void CATEGORY_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1 )
            {
                btnCategory.PerformClick();
            }
            else if (e.KeyCode == Keys.Back)
            {
                if (CATEGORY.Text == "")
                {
                    CATEGORY_NAME.Text = "";
                }
            }
            else if (e.KeyCode == Keys.Enter)
            {
                if (HasType)
                {
                    TYPE.Focus();
                }
                else if (HasGroup)
                {
                    GROUP.Focus();
                }
                else if (HasTM)
                {
                    TRADEMARK.Focus();
                }
                else
                {
                    MINIMUM_QTY.Focus();
                }
            }
            else
            {
                btnCategory.PerformClick();
            }
            
        }

        private void TRADEMARK_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1 )
            {
                btnTrademark.PerformClick();
            }
            else if (e.KeyCode == Keys.Back)
            {
                if (TRADEMARK.Text == "")
                {
                    TRADEMARK_NAME.Text = "";
                }
            }
            else if (e.KeyCode == Keys.Enter)
            {
                MINIMUM_QTY.Focus();
            }
            else
            {
                btnTrademark.PerformClick();
            }
        }

        private void btnIMGClear_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
        }

        private void btnAddTaxType_Click(object sender, EventArgs e)
        {
            Tax_Type txType = new Tax_Type(1);
            txType.ShowDialog();
           if (txType.AddedfromMaster !=false)
            {
                GetTaxRates();
            }
        }

        private void DESC_ENG_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyData == Keys.Down)
            //{

            //    if (dataGridItem.Visible == true)
            //    {
            //        try
            //        {
            //            dataGridItem.Focus();
            //            dataGridItem.CurrentCell = dataGridItem.Rows[0].Cells["DESC_ENG"];
            //        }
            //        catch
            //        { 
                    
            //        }
            //    }

            //}
            //else 
            if (e.KeyCode == Keys.Up)
            {
                if (dataGridItem.Rows.Count > 0)
                {
                    int r = dataGridItem.CurrentCell.RowIndex;
                    if (r > 0) //check for index out of range
                        dataGridItem.CurrentCell = dataGridItem[2, r - 1];
                }

                if (dataGridItem.Rows.Count > 0 && dataGridItem.CurrentRow != null)
                {

                    DataGridViewCellCollection c = dataGridItem.CurrentRow.Cells;

                    ID = Convert.ToString(c["CODE"].Value);

                    CODE.Text = Convert.ToString(c["CODE"].Value);
                    if (HasArabic)
                        DESC_ARB.Text = Convert.ToString(c["DESC_ARB"].Value);
                    TYPE.Text = Convert.ToString(c["TYPE"].Value);
                    GROUP.Text = Convert.ToString(c["GROUP"].Value);
                    CATEGORY.Text = Convert.ToString(c["CATEGORY"].Value);
                    TRADEMARK.Text = Convert.ToString(c["TRADEMARK"].Value);
                    COST_PRICE.Text = Convert.ToString(c["COST_PRICE"].Value);
                    SALE_PRICE.Text = Convert.ToString(c["SALE_PRICE"].Value);
                    MINIMUM_QTY.Text = Convert.ToString(c["MINIMUM_QTY"].Value);
                    MAXIMUM_QTY.Text = Convert.ToString(c["MAXIMUM_QTY"].Value);
                    REORDER_QTY.Text = Convert.ToString(c["REORDER_QTY"].Value);
                    Drp_Location.Text = Convert.ToString(c["LOCATION"].Value);
                    get_image();

                    if (Convert.ToString(c["IN_ACTIVE"].Value) == "Y")
                    {
                        IN_ACTIVE.Checked = true;
                    }
                    else
                    {
                        IN_ACTIVE.Checked = false;
                    }

                    try
                    {

                        DrpTaxType.SelectedValue = Convert.ToInt32(c["TaxId"].Value);
                    }
                    catch
                    {
                        DrpTaxType.SelectedValue = 0;
                    }


                    chKSerial.Checked = Convert.ToBoolean(c["HASSERIAL"].Value);
                    chkWarranty.Checked = Convert.ToBoolean(c["HASWARRENTY"].Value);
                    txtWarrentyPeriod.Text = Convert.ToString(c["PERIOD"].Value);
                    cmbWarrentyType.Text = Convert.ToString(c["PERIODTYPE"].Value);
                    flg = true;
                    DESC_ENG.Text = Convert.ToString(c["DESC_ENG"].Value);

                    getItemData();

                    DESC_ENG.Focus();

                }
                if (dataGridItem.CurrentRow == null || dataGridItem.CurrentRow.Index < 0)
                {
                    return;
                }

                int index = dataGridItem.CurrentRow.Index;
                dataGridItem.Rows[index].Selected = true;

                dataGridItem.FirstDisplayedScrollingRowIndex = index;

            }
            if (e.KeyCode == Keys.Down)
            {
                if (dataGridItem.CurrentCell != null)
                {
                    int r = dataGridItem.CurrentCell.RowIndex;
                    if (r < dataGridItem.Rows.Count - 1)
                    {
                        dataGridItem.CurrentCell = dataGridItem[2, r + 1];
                    }
                }

                if (dataGridItem.Rows.Count > 0 && dataGridItem.CurrentRow != null)
                {

                    DataGridViewCellCollection c = dataGridItem.CurrentRow.Cells;

                    ID = Convert.ToString(c["CODE"].Value);

                    CODE.Text = Convert.ToString(c["CODE"].Value);
                    if (HasArabic)
                        DESC_ARB.Text = Convert.ToString(c["DESC_ARB"].Value);
                    TYPE.Text = Convert.ToString(c["TYPE"].Value);
                    GROUP.Text = Convert.ToString(c["GROUP"].Value);
                    CATEGORY.Text = Convert.ToString(c["CATEGORY"].Value);
                    TRADEMARK.Text = Convert.ToString(c["TRADEMARK"].Value);
                    COST_PRICE.Text = Convert.ToString(c["COST_PRICE"].Value);
                    SALE_PRICE.Text = Convert.ToString(c["SALE_PRICE"].Value);
                    MINIMUM_QTY.Text = Convert.ToString(c["MINIMUM_QTY"].Value);
                    MAXIMUM_QTY.Text = Convert.ToString(c["MAXIMUM_QTY"].Value);
                    REORDER_QTY.Text = Convert.ToString(c["REORDER_QTY"].Value);
                    Drp_Location.Text = Convert.ToString(c["LOCATION"].Value);
                    get_image();

                    if (Convert.ToString(c["IN_ACTIVE"].Value) == "Y")
                    {
                        IN_ACTIVE.Checked = true;
                    }
                    else
                    {
                        IN_ACTIVE.Checked = false;
                    }

                    try
                    {

                        DrpTaxType.SelectedValue = Convert.ToInt32(c["TaxId"].Value);
                    }
                    catch
                    {
                        DrpTaxType.SelectedValue = 0;
                    }


                    chKSerial.Checked = Convert.ToBoolean(c["HASSERIAL"].Value);
                    chkWarranty.Checked = Convert.ToBoolean(c["HASWARRENTY"].Value);
                    txtWarrentyPeriod.Text = Convert.ToString(c["PERIOD"].Value);
                    cmbWarrentyType.Text = Convert.ToString(c["PERIODTYPE"].Value);
                    flg = true;
                    DESC_ENG.Text = Convert.ToString(c["DESC_ENG"].Value);

                    getItemData();

                    DESC_ENG.Focus();
                   
                }             
                if (dataGridItem.CurrentRow == null || dataGridItem.CurrentRow.Index<0)
                {
                    return;
                }

                int index = dataGridItem.CurrentRow.Index;
                dataGridItem.Rows[index].Selected = true;

                dataGridItem.FirstDisplayedScrollingRowIndex = index;
            }
                
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (sender is KryptonTextBox)
                {
                    enter = false;
                    string name = (sender as KryptonTextBox).Name;
                    switch (name)
                    {
                        case "DESC_ENG":
                          //  dataGridItem.Visible = false;
                            if (HasArabic)
                            {
                                DESC_ARB.Focus();
                             //   dataGridItem.Visible = false;
                            }
                            else
                            {
                             //   dataGridItem.Visible = false;
                                if (HasCategory)
                                {

                                    CATEGORY.Focus();
                                    //btnCategory.Focus();
                                }
                                else
                                {
                                    if (HasType)
                                    {
                                        TYPE.Focus();
                                      //  btnType.Focus(); 
                                    }
                                    else {
                                        if (HasGroup)
                                        {
                                            GROUP.Focus();
                                          //  btnGroup.Focus();
                                        }
                                        else
                                        {
                                            if (HasTM)
                                            {
                                                TRADEMARK.Focus();
                                               //1 TRADEMARK.Focus();
                                            }
                                            else 
                                            {
                                                
                                                MINIMUM_QTY.Focus();
                                            }
                                        }
                                    }
                                
                                }
                            }
                            break;

                        case "DESC_ARB":
                            if (HasCategory)
                            {
                                CATEGORY.Focus();
                                //btnCategory.Focus();
                            }
                            else
                            {
                                if (HasCategory)
                                {
                                    btnCategory.Focus();
                                }
                                else
                                {
                                    if (HasType)
                                    {
                                        TYPE.Focus();
                                        //  btnType.Focus(); 
                                    }
                                    else
                                    {
                                        if (HasGroup)
                                        {
                                            GROUP.Focus();
                                            //  btnGroup.Focus();
                                        }
                                        else
                                        {
                                            if (HasTM)
                                            {
                                                TRADEMARK.Focus();
                                                //1 TRADEMARK.Focus();
                                            }
                                            else
                                            {

                                                MINIMUM_QTY.Focus();
                                            }
                                        }
                                    }

                                }
                            }
                            break;
                        case "TRADEMARK":
                            MINIMUM_QTY.Focus();
                            break;
                        case "MINIMUM_QTY":
                            MAXIMUM_QTY.Focus();
                            
                            break;
                        case "MAXIMUM_QTY":
                            REORDER_QTY.Focus();
                            break;
                        case "REORDER_QTY":

                            Drp_Location.Focus();
                            break;
                        case "txt_HSN":
                            COST_PRICE.Focus();
                            break;
                        case "COST_PRICE":
                            SALE_PRICE.Focus();
                            break;
                        case "SALE_PRICE":
                            txt_MRP.Focus();
                            break;
                        case "txt_MRP":
                            txt_wholesale.Focus();
                            break;
                        case "txt_wholesale":
                            cmb_units.Focus();
                            break;
                        case "tb_stock":
                            if (hasTax)
                                DrpTaxType.Focus();
                            else
                                COST_PRICE.Focus();
                            break;
                        case "txtWarrentyPeriod":
                         
                                cmbWarrentyType.Focus();
                         
                            break;
                       

                        default:
                            break;
                    }
                }
                else if (sender is KryptonComboBox)
                {
                    enter = false;
                      string name = (sender as KryptonComboBox).Name;
                    switch (name)
                    {
                        case "DrpTaxType":
                            txt_HSN.Focus();
                            //COST_PRICE.Focus();
                            break;
                        case "cmbProductType":
                            DESC_ENG.Focus();
                            //COST_PRICE.Focus();
                            break;
                        case "Drp_Location":
                            //if (dgRates.RowCount == 0)
                            //{
                            //    dgRates.Rows.Add();
                            //}
                            tb_stock.Focus();
                            //dgRates.Focus();
                            //dgRates.CurrentCell = dgRates.Rows[0].Cells[0];
                            break;
                        case "cmb_units":
                        if (dgRates.RowCount == 0)
                            {
                                dgRates.Rows.Add();
                            }
                            dgRates.Focus();
                            BarcodeasItemcode();
                            dgRates.CurrentCell = dgRates.Rows[0].Cells[0];
                            dgRates.Rows[0].Cells["RTL"].Value=SALE_PRICE.Text;
                            dgRates.Rows[0].Cells["WHL"].Value = txt_wholesale.Text;
                            dgRates.Rows[0].Cells["MRP"].Value =txt_MRP.Text;
                            dgRates.Rows[0].Cells["PUR"].Value = COST_PRICE.Text;
                            dgRates.Rows[0].Cells[1].Value =1;
                            cmbUnit.Text = cmb_units.Text;
                            btnSave.Focus();
                            this.ActiveControl = btnSave;
                            break;
                         default :
                            break;
                    }
                }
                else if (sender is KryptonCheckBox)
                { 
                     string name = (sender as KryptonCheckBox).Name;
                    switch (name)
                    {
                    case "IN_ACTIVE":
                    chKSerial.Focus();
                
                    break;
                    case "chKSerial":
                    chkWarranty.Focus();
                 
                    break;
                    case "chkWarranty":
                    if (PnlWarranty.Visible)
                    {
                        txtWarrentyPeriod.Focus();
                    }
                    else
                    {
                        dgRates.Focus();
                        dgRates.CurrentCell = dgRates.Rows[0].Cells[0];
                        dgRates.CurrentCell.Selected = true;
                    }
                    break;
                    default:
                    break;
                    }

                }
                else if (sender is KryptonButton)
                {
                    string name = (sender as KryptonButton).Name;
                    switch (name)
                    {
                        case "btnCategory":
                            if (HasType)
                            {
                                btnType.Focus();
                            }
                            else
                            {
                                if (HasGroup)
                                {
                                    btnGroup.Focus();
                                }
                                else
                                {
                                    if (HasTM)
                                    {
                                        TRADEMARK.Focus();
                                    }
                                    else
                                    {
                                        MINIMUM_QTY.Focus();
                                    }
                                }
                            }
                            break;
                        case "btnType":
                            if (HasGroup)
                            {
                                btnGroup.Focus();
                            }
                            else
                            {
                                if (HasTM)
                                {
                                    btnTrademark.Focus();
                                }
                                else
                                {
                                    MINIMUM_QTY.Focus();
                                }
                            }
                            break;
                        case "btnGroup":
                            if (HasTM)
                            {
                                btnTrademark.Focus();
                            }
                            else
                            {
                                MINIMUM_QTY.Focus();
                            }
                            break;
                        case "btnTrademark":
                            MINIMUM_QTY.Focus();
                            break;

                        default:
                            break;
                    }
                }

            }
        }

        private void btn_editfornew_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to use the details to create a new item", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
               ID = "";  
               CODE.Text = "";
               btn_editfornew.Visible = false;
               DESC_ENG.Focus();
                btnSave.PerformClick();
            }
        }
        public bool flg = false;
        private void DESC_ENG_TextChanged(object sender, EventArgs e)
        {
            // if (DESC_ENG.Text != "")
            //{
            //   dataGridItem.Visible = true;
            if (flg == false)
            {
                source1.Filter = string.Format("DESC_ENG LIKE '%{0}%' AND DESC_ENG LIKE'%{1}%'", DESC_ENG.Text.Replace("'", "''").Replace("*", "[*]"), txt_search.Text.Replace("'", "''").Replace("*", "[*]"));

            }
            else
            {
                flg = false;

            }
            // dataGridItem.ClearSelection();
            // }
            // else
            // {
            // dataGridItem.Visible = false;

            //}
            // ind = ID;
        }

        private void dataGridItem_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Down)
            //    {

            //        if (dataGridItem.CurrentRow == null) return;
            //        if (dataGridItem.CurrentRow.Index - 1 >= 0)
            //        {

            //            dataGridItem.CurrentCell = dataGridItem.Rows[dataGridItem.CurrentRow.Index - 1].Cells[0];
            //            dataGridItem.Rows[dataGridItem.CurrentCell.RowIndex].Selected = true;

            //        }

            //      }
            if (e.KeyData == Keys.Enter)
            {
                if (dataGridItem.Rows.Count > 0 && dataGridItem.CurrentRow != null)
                {
                    DataGridViewCellCollection c = dataGridItem.CurrentRow.Cells;
                    ID = Convert.ToString(c["CODE"].Value);
                    CODE.Text = Convert.ToString(c["CODE"].Value);
                    if (HasArabic)
                        DESC_ARB.Text = Convert.ToString(c["DESC_ARB"].Value);
                    TYPE.Text = Convert.ToString(c["TYPE"].Value);
                    GROUP.Text = Convert.ToString(c["GROUP"].Value);
                    CATEGORY.Text = Convert.ToString(c["CATEGORY"].Value);
                    TRADEMARK.Text = Convert.ToString(c["TRADEMARK"].Value);
                    COST_PRICE.Text = Convert.ToString(c["COST_PRICE"].Value);
                    SALE_PRICE.Text = Convert.ToString(c["SALE_PRICE"].Value);
                    MINIMUM_QTY.Text = Convert.ToString(c["MINIMUM_QTY"].Value);
                    MAXIMUM_QTY.Text = Convert.ToString(c["MAXIMUM_QTY"].Value);
                    REORDER_QTY.Text = Convert.ToString(c["REORDER_QTY"].Value);

                    if (Convert.ToString(c["IN_ACTIVE"].Value) == "Y")
                    {
                        IN_ACTIVE.Checked = true;
                    }
                    else
                    {
                        IN_ACTIVE.Checked = false;
                    }
                    DrpTaxType.SelectedValue = Convert.ToInt32(c["TaxId"].Value);
                    DESC_ENG.Text = Convert.ToString(c["DESC_ENG"].Value);
                    chKSerial.Checked = Convert.ToBoolean(c["HASSERIAL"].Value);
                    chkWarranty.Checked = Convert.ToBoolean(c["HASWARRENTY"].Value);
                    txtWarrentyPeriod.Text = Convert.ToString(c["PERIOD"].Value);
                    cmbWarrentyType.Text = Convert.ToString(c["PERIODTYPE"].Value);
                    getItemData();
                    DESC_ENG.Focus();
                }
                //  txtCode.Text = dataGridItem.CurrentRow.Cells[1].Value.ToString();
                // DESC_ENG.Text = dataGridItem.CurrentRow.Cells[0].Value.ToString();

                //  dataGridItem.Visible = false;
            }
            else if (e.KeyData == Keys.Escape)
            {
                DESC_ENG.Focus();
            }
        }

        public string TranslateText(string input, string languagePair)
        {
            try
            {
                string url = String.Format("http://www.google.com/translate_t?h1=en&ie=UTF8&text={0}&langpair={1}", input, languagePair);
                WebClient webClient = new WebClient();
                webClient.Encoding = System.Text.Encoding.GetEncoding("windows-1256");
                string result = webClient.DownloadString(url);
                int len = result.Length;
                result = result.Remove(0,result.IndexOf("id=result_box"));
          
                int len2 = result.Length;
                result = result.Remove(0, result.IndexOf("title="));
                  result=result.Remove(0,result.IndexOf(">"));
                  result = result.Remove(0,1);
                result = result.Remove(result.IndexOf("</span>"));
              
            //   return "<span>" + result + "</span>";
               return result;
            }
            catch(Exception ee)
            {
                MessageBox.Show(ee.Message);
                return "";
                
            }
        }


        private void DESC_ARB_Enter(object sender, EventArgs e)
        {
            if (HasArabic)
            {
                //DESC_ARB.Text = TranslateText(DESC_ENG.Text, "en|ar");
                DESC_ARB.Text = Class.Translator.Translate(DESC_ENG.Text, "English", "Arabic");
            }
            SetKeyboardLayout(GetInputLanguageByName("ar"));
           
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            Country c = new Country(genEnum.Units,1,1);
            c.ShowDialog();
            HasReloadUit = true;
            bindUnit();
            bindUnit1();
        }
        
        private void dgRates_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void btnaddcategory_Click(object sender, EventArgs e)
        {
            
            Country c = new Country(genEnum.Category,1,1);
            if (c.ShowDialog() == DialogResult.OK)
            {
                CATEGORY.Text = c.code;
                CATEGORY_NAME.Text = c.name;
                if (HasType)
                {

                    TYPE.Focus();
                }
                else if (HasGroup)
                {
                    GROUP.Focus();
                }
                else if (HasTM)
                {
                    TRADEMARK.Focus();
                }
                else
                {
                    MINIMUM_QTY.Focus();
                }
            }
            else
            {
                CATEGORY.Focus();
            }
        }

        private void btnaddtype_Click(object sender, EventArgs e)
        {
            Country c = new Country(genEnum.Type, 1,1);
            if (c.ShowDialog() == DialogResult.OK)
            {
                TYPE.Text = c.code;
                TYPE_NAME.Text = c.name;
                if (HasGroup)
                {
                    GROUP.Focus();
                }
                else if (HasTM)
                {
                    TRADEMARK.Focus();
                }
                else
                {
                    MINIMUM_QTY.Focus();
                }
            }
            else
            {
                TYPE.Focus();
            }
        }

        private void btnaddgroup_Click(object sender, EventArgs e)
        {
            Country c = new Country(genEnum.Group, 1,1);
            if (c.ShowDialog() == DialogResult.OK)
            {
                GROUP.Text = c.code;
                GROUP_NAME.Text = c.name;
                if (HasTM)
                {
                    TRADEMARK.Focus();
                }
                else
                {
                    MINIMUM_QTY.Focus();
                }
            }
            else
            {
                GROUP.Focus();
            }
        }

        private void btnaddbrand_Click(object sender, EventArgs e)
        {
            Country c = new Country(genEnum.TradeMark, 1,1);
            if (c.ShowDialog() == DialogResult.OK)
            {
                TRADEMARK.Text = c.code;
                TRADEMARK_NAME.Text = c.name;
                MINIMUM_QTY.Focus();
            }
            else
            {
                TRADEMARK.Focus();
            }
        }

        private void btnBrowse_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();

            // image filters
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp;*.png;)|*.jpg; *.jpeg; *.gif; *.bmp;*.png;";
            if (open.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(open.FileName);
                pathdirectory = open.FileName;
            }
        }

        private void btnIMGClear_Click_1(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
        }

        private void dataGridItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridItem.Rows.Count > 0 && dataGridItem.CurrentRow != null)
            {
                Clear_ListSelection();
                DataGridViewCellCollection c = dataGridItem.CurrentRow.Cells;
                ID = Convert.ToString(c["CODE"].Value);
                CODE.Text = Convert.ToString(c["CODE"].Value);
                if (HasArabic)
                {
                    DESC_ARB.Text = Convert.ToString(c["DESC_ARB"].Value);
                }
                TYPE.Text = Convert.ToString(c["TYPE"].Value);
                GROUP.Text = Convert.ToString(c["GROUP"].Value);
                CATEGORY.Text = Convert.ToString(c["CATEGORY"].Value);
                TRADEMARK.Text = Convert.ToString(c["TRADEMARK"].Value);
                COST_PRICE.Text = Convert.ToString(c["COST_PRICE"].Value);
                SALE_PRICE.Text = Convert.ToString(c["SALE_PRICE"].Value);
                MINIMUM_QTY.Text = Convert.ToString(c["MINIMUM_QTY"].Value);
                MAXIMUM_QTY.Text = Convert.ToString(c["MAXIMUM_QTY"].Value);
                REORDER_QTY.Text = Convert.ToString(c["REORDER_QTY"].Value);
                Drp_Location.Text = Convert.ToString(c["LOCATION"].Value);
               txt_HSN.Text = Convert.ToString(c["HSN"].Value);
                cmbPrimeSupplier.SelectedValue = Convert.ToString(c["SUPPLIER"].Value);
                get_image();

                if (Convert.ToString(c["IN_ACTIVE"].Value) == "Y")
                {
                    IN_ACTIVE.Checked = true;
                }
                else
                {
                    IN_ACTIVE.Checked = false;
                }

                try
                {

                    DrpTaxType.SelectedValue = Convert.ToInt32(c["TaxId"].Value);
                }
                catch
                {
                    DrpTaxType.SelectedValue = 0;
                }
                chKSerial.Checked = Convert.ToBoolean(c["HASSERIAL"].Value);
                chkWarranty.Checked = Convert.ToBoolean(c["HASWARRENTY"].Value);
                txtWarrentyPeriod.Text = Convert.ToString(c["PERIOD"].Value);
                cmbWarrentyType.Text = Convert.ToString(c["PERIODTYPE"].Value);
                cmbProductType.SelectedValue = Convert.ToString(c["PRODUCT_TYPE"].Value);
                flg = true;
                DESC_ENG.Text = Convert.ToString(c["DESC_ENG"].Value);
                
                getItemData();
                DESC_ENG.Focus();
                enter = false;
              //  btn_editfornew.Visible = true;
            }
        }

        public void get_image()
        {
            //cmd.CommandText = "SELECT Picture from INV_ITEM_DIRECTORY_PICTURE where Code = '" + ID + "'";
            //adapter.SelectCommand = cmd;
            itmdrpic.Code = ID;
            DataSet ds = itmdrpic.PictureByCode();
            //adapter.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                byte[] myImage = new byte[0];
                myImage = (byte[])ds.Tables[0].Rows[0]["Picture"];
                MemoryStream stream = new MemoryStream(myImage);
                pictureBox1.Image = System.Drawing.Image.FromStream(stream);
            }
            else
            {
                pictureBox1.Image = null;
            }
        }

        bool enter = false;
        private void dgRates_Enter(object sender, EventArgs e)
        {
            enter = true;
        }
        
        private void dgRates_Leave(object sender, EventArgs e)
        {
            enter = false;
        }

        private void DESC_ENG_Leave(object sender, EventArgs e)
        {
            if (ItemFormat == "Upper Case")
            {
                DESC_ENG.Text = DESC_ENG.Text.ToUpper();
            }
            if (ItemFormat == "Lower Case")
            {
                DESC_ENG.Text = DESC_ENG.Text.ToLower();
            }
            if (ItemFormat == "First Letter Upper Case")
            {
                DESC_ENG.Text = UppercaseWords(DESC_ENG.Text);
            }
        }

        static string UppercaseWords(string value)
        {
            char[] array = value.ToCharArray();
            // Handle the first letter in the string.
            if (array.Length >= 1)
            {
                if (char.IsLower(array[0]))
                {
                    array[0] = char.ToUpper(array[0]);
                }
            }
            // Scan through the letters, checking for spaces.
            // ... Uppercase the lowercase letters following spaces.
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i - 1] == ' ')
                {
                    if (char.IsLower(array[i]))
                    {
                        array[i] = char.ToUpper(array[i]);
                    }
                }
            }
            return new string(array);
        }

        public static InputLanguage GetInputLanguageByName(string inputName)
        {
            foreach (InputLanguage lang in InputLanguage.InstalledInputLanguages)
            {
                if (lang.Culture.EnglishName.ToLower().StartsWith(inputName))
                    return lang;
            }
            return null;
        }
        
        public void SetKeyboardLayout(InputLanguage layout)
        {
            InputLanguage.CurrentInputLanguage = layout;
        }

        private void DESC_ARB_Leave(object sender, EventArgs e)
        {
            SetKeyboardLayout(GetInputLanguageByName("eng"));
        }

        private void chkWarranty_CheckedChanged(object sender, EventArgs e)
        {
            if (chkWarranty.Checked)
            {
                PnlWarranty.Visible = true;
            }
            else
            {
                PnlWarranty.Visible = false;
            }
        }

        private void Cb_BracodeSetter_CheckedChanged(object sender, EventArgs e)
        {
            BarcodeasItemcode();
        }

        private void dataGridItem_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            foreach (DataGridViewCell cell in dataGridItem.Rows[e.RowIndex].Cells)
            {
                if (cell.GetType() == typeof(DataGridViewImageCell))
                {
                    cell.Value = DBNull.Value;
                }
            }
        }

        private void dataGridItem_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {
                if (e.Exception.Message == "DataGridViewComboBoxCell value is not valid.")
                {
                    object value = dataGridItem.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    if (!((DataGridViewComboBoxColumn)dataGridItem.Columns[e.ColumnIndex]).Items.Contains(value))
                    {
                        ((DataGridViewComboBoxColumn)dataGridItem.Columns[e.ColumnIndex]).Items.Add(value);
                    }
                }

                throw e.Exception;
            }
            catch (Exception ex)
            {
                
                    MessageBox.Show(string.Format(@"Failed to bind ComboBox. "
                    + "Please contact support with this message:"
                    + "\n\n" + ex.Message));
                
            }
        }

        private void cb_baseqty_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_baseqty.Checked)
            {
                panel_baseqty.Visible = true;
            }
            else
            {
                panel_baseqty.Visible = false;
            }
        }

        private void btn_pricechange_Click(object sender, EventArgs e)
        {
            PriceChange obj = new PriceChange();
            obj.ShowDialog();
        }

        private void dgRates_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && dgRates.CurrentRow != null && !dgRates.CurrentRow.IsNewRow)
            {
                if (MessageBox.Show("Are you sure? you want to delete this row?", "Price Row Deletion", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    dgRates.Rows.Remove(dgRates.CurrentRow);
                }
            }
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void ItemMasterView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void dgRates_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                e.Handled = false;
        }

        private void dgRates_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            System.Drawing.Rectangle rect = dgRates.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
            if (e.ColumnIndex == 0)
            {
                txtUnitProperty.Visible = false;
                cmbUnit.Width = rect.Width;
                cmbUnit.Height = rect.Height;
                cmbUnit.Location = new Point(dgRates.Location.X + rect.X, dgRates.Location.Y + rect.Y);
                var value = Convert.ToString(dgRates.CurrentCell.Value);
                if (value.Equals("") && !cmbUnit.Text.Equals("System.Data.DataRowView"))
                {
                    dgRates.CurrentCell.Value = cmbUnit.Text;
                }
                cmbUnit.Visible = true;
                cmbUnit.Focus();
                cmbUnit.SelectedValue= Convert.ToString(dgRates.CurrentCell.Value);
            }
            else
            {
                cmbUnit.Visible = false;
                txtUnitProperty.Width = rect.Width;
                txtUnitProperty.Height = rect.Height;
                txtUnitProperty.Location = new Point(dgRates.Location.X + rect.X, dgRates.Location.Y + rect.Y);
                txtUnitProperty.Text = Convert.ToString(dgRates.CurrentCell.Value);
                txtUnitProperty.Visible = true;
                txtUnitProperty.Focus();
                txtUnitProperty.SelectionStart = txtUnitProperty.Text.Length;
            }
        }

        private void cmbUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dgRates.CurrentRow != null)
            {
                dgRates.CurrentRow.Cells[0].Value = cmbUnit.Text;
            }
        }

        private void txtUnitProperty_TextChanged(object sender, EventArgs e)
        {
            dgRates.CurrentCell.Value = txtUnitProperty.Text;
        }

        private void dgRates_Scroll(object sender, ScrollEventArgs e)
        {
            cmbUnit.Visible = false;
            txtUnitProperty.Visible = false;
        }

        private void lnkAddUnit_LinkClicked(object sender, EventArgs e)
        {
            dgRates.Rows.Add();
        }

        private void txtUnitProperty_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter)
            {
                int currentColumn = dgRates.CurrentCell.ColumnIndex;
                if (sender.GetType() == typeof(TextBox))
                {
                    txtUnitProperty.Visible = false;
                }
                else
                {
                    cmbUnit.Visible = false;
                }

                if (e.Shift)
                {
                    currentColumn--;
                    if (currentColumn > 0 && !dgRates.CurrentRow.Cells[currentColumn].Visible)
                    {
                        currentColumn--;
                    }

                    if (currentColumn >= 0)
                    {
                        dgRates.CurrentCell = dgRates.CurrentRow.Cells[currentColumn];
                    }
                    else if(dgRates.CurrentRow.Index > 0)
                    {
                        dgRates.CurrentCell = dgRates.Rows[dgRates.CurrentRow.Index - 1].Cells[dgRates.ColumnCount - 1];
                    }
                }
                else
                {
                    currentColumn++;
                    if (currentColumn < dgRates.ColumnCount - 1 && !dgRates.CurrentRow.Cells[currentColumn].Visible)
                    {
                        currentColumn++;
                    }

                    if (dgRates.CurrentCell.ColumnIndex < dgRates.ColumnCount - 1)
                    {
                        dgRates.CurrentCell = dgRates.CurrentRow.Cells[currentColumn];
                    }
                    else
                    {
                        if (dgRates.CurrentRow.Index == dgRates.RowCount - 1)
                        {
                            btnSave.Focus();
                        }
                        else
                        {
                            dgRates.CurrentCell = dgRates.Rows[dgRates.CurrentRow.Index + 1].Cells[0];
                        }
                    }
                }
                //int count = dgRates.Columns.Count - 1;
                //int colindex = dgRates.CurrentCell.ColumnIndex;
                //if (count == colindex)
                //{
                //    btnSave.Focus();
                //}
                //else
                //{
                    e.IsInputKey = true;
              //  }
            }
        }

        private void txtUnitProperty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter)
            {
                Common.preventDingSound(e);
               
            }

        }

        private void cmbPrimeSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void kryptonLabel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void kryptonLabel20_Paint(object sender, PaintEventArgs e)
        {

        }

        private void kryptonLabel10_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmbUnit_Leave(object sender, EventArgs e)
        {
           // enter = false;
        }

        private void txtUnitProperty_Leave(object sender, EventArgs e)
        {
           // enter = false;
        }

        private void COST_PRICE_TextChanged(object sender, EventArgs e)
        {
            if (dgRates.Rows.Count > 0)
            {
                if (COST_PRICE.Text != "")
                {
                //    dgRates.Rows[0].Cells["RTL"].Value = SALE_PRICE.Text;
                //    dgRates.Rows[0].Cells["WHL"].Value = txt_wholesale.Text;
                //    dgRates.Rows[0].Cells["MRP"].Value = txt_MRP.Text;
                    dgRates.Rows[0].Cells["PUR"].Value = COST_PRICE.Text;
                }
            }
        }

        private void SALE_PRICE_TextChanged(object sender, EventArgs e)
        {
            if (dgRates.Rows.Count > 0)
            {
                if (SALE_PRICE.Text != "")
                {
                    dgRates.Rows[0].Cells["RTL"].Value = SALE_PRICE.Text;
                    //    dgRates.Rows[0].Cells["WHL"].Value = txt_wholesale.Text;
                    //    dgRates.Rows[0].Cells["MRP"].Value = txt_MRP.Text;
                    // dgRates.Rows[0].Cells["PUR"].Value = COST_PRICE.Text;
                }

            }
       }

        private void txt_MRP_TextChanged(object sender, EventArgs e)
        {
            if (dgRates.Rows.Count > 0)
            {
                if (txt_MRP.Text != "")
                {
                     //gRates.Rows[0].Cells["RTL"].Value = SALE_PRICE.Text;
                    //    dgRates.Rows[0].Cells["WHL"].Value = txt_wholesale.Text;
                       dgRates.Rows[0].Cells["MRP"].Value = txt_MRP.Text;
                    // dgRates.Rows[0].Cells["PUR"].Value = COST_PRICE.Text;
                }

            }
        }

        private void txt_wholesale_TextChanged(object sender, EventArgs e)
        {
            if (dgRates.Rows.Count > 0)
            {
                if (txt_wholesale.Text != "")
                {
                    //gRates.Rows[0].Cells["RTL"].Value = SALE_PRICE.Text;
                        dgRates.Rows[0].Cells["WHL"].Value = txt_wholesale.Text;
                  //  dgRates.Rows[0].Cells["MRP"].Value = txt_MRP.Text;
                    // dgRates.Rows[0].Cells["PUR"].Value = COST_PRICE.Text;
                }

            }
        }

        private void dgRates_Click(object sender, EventArgs e)
        {

        }

        private void cmbPrimeSupplier_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                MINIMUM_QTY.Focus();
            }
        }

        private void DESC_ARB_TextChanged(object sender, EventArgs e)
        {

        }

        private void DESC_ENG_TextChanged_1(object sender, EventArgs e)
        {
            if (flg == false)
            {
                source1.Filter = string.Format("DESC_ENG LIKE '%{0}%' AND DESC_ENG LIKE'%{1}%'", DESC_ENG.Text.Replace("'", "''").Replace("*", "[*]"), txt_search.Text.Replace("'", "''").Replace("*", "[*]"));

            }
            else
            {
                flg = false;

            }
        }
    }
}
