using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using System.IO;
using System.Drawing.Printing;
using System.Data.OleDb;
using System.Data;
using System.Data.SqlClient;

namespace Sys_Sols_Inventory
{

    public partial class Purchase_Order : Form
    {
        #region properties declaration
        //private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        private bool HasArabic = true;
        //private SqlCommand cmd = new SqlCommand();
        //private SqlDataAdapter adapter = new SqlDataAdapter();
        private DataTable table = new DataTable();
        private DataTable tableUnits = new DataTable();
        private BindingSource source = new BindingSource();
        private BindingSource source2 = new BindingSource();
        private int selectedRow = -1;
        private bool hasBatch = false;
        private bool hasTax = false;
        private bool hasBarcode = false;
        private bool PriceLastPurchase = false;
        private bool HasAccessLimit = false;
        private bool itemSelected = false;
        private string ID = "";
        private string POSTID = "";
        private string type;
        private bool Editactive = false;
        private DateTime TransDate;
        private bool HasAccounts;
        #endregion
        string SalesManCode, salesmanname, ModifyType = "";
        Initial mdi = (Initial)Application.OpenForms["Initial"];
        Login lg = (Login)Application.OpenForms["Login"];
        bool firstitemlistbyname = false;
        bool clearitemname = false;
        string companyname;
        int HEIGHT, WIDTH;
        string PriceType;
        bool HASSERIAL = false;
        Class.DateSettings dset = new Class.DateSettings();
        bool IsMRP = false, IsProductCode = false, IsCompany = false, IsBarcodeValue = false;
        Class.Ledgers Ledg = new Class.Ledgers();
        Class.Transactions trans = new Class.Transactions();
        Class.ModifiedTransaction modtrans = new Class.ModifiedTransaction();
        Class.BarcodeSettings barcodse = new Class.BarcodeSettings();
        Class.Stock_Report stkrpt = new Class.Stock_Report();
        Class.CompanySetup ComSet = new Class.CompanySetup();
        bool ActiveForm = false;
        decimal decTotal = 0;
        bool HasType;
        bool HasCategory;
        bool HasTM;
        bool HasGroup;
        bool excludechanged = false;
        bool includechang = false;
        double Item_Discount_Amt = 0;
        string CompanyName, ArabicName, EngBranch, ArbBranch, Place, ArbPlace, ArbCity, City, Phone, Email, Fax, TineNo, Billno, Date, CUSID, Website, panno, vat, Address1, ArbAddress1, ArbAddress2, Address2, logo, DiscType, DiscValue;
        bool PUR_MoveDisc, PUR_MoveRtlper, PUR_MoveRtlAmt, PUR_MoveTaxper, PUR_MoveTaxAmt, PUR_MoveTotal, PUR_FocusDate, PUR_FocusSupplier, PUR_FocusInvoice, PUR_FocusReference, PUR_FocusBarcode, PUR_FocusItemCode, PUR_FocusItemName;
        public Purchase_Order()
        {
            InitializeComponent();
            DISCOUNT.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            QTY_RCVD.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            PRICE_FOB.KeyPress += new KeyPressEventHandler(General.OnlyFloat);

            BindSettings();
            if (hasTax)
            {
                ITEM_TAX_PER.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            }
            ITEM_DISCOUNT.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {



            if (keyData == (Keys.Escape))
            {

                PNL_DATAGRIDITEM.Visible = false;

                ItemClear();
                ITEM_NAME.Focus();
                return true;
            }

         //  else if (e.KeyCode == Keys.S && e.Control)
            else if (keyData == (Keys.Alt | Keys.S))
            {

                //  btnSave.Focus();
                btnSave.PerformClick();
                SUPPLIER_CODE.Focus();
                clearitemname = true;

                return true;

            }
            else if (keyData == (Keys.Alt | Keys.N))
            {


                //btnNewItem.PerformClick();
                return true;

            }




            return base.ProcessCmdKey(ref msg, keyData);

        }
        public void GetMaxDocID()
        {

            try
            {
                //if (conn.State == ConnectionState.Closed)
                //{
                //    conn.Open();
                //}
                //adapter.SelectCommand = cmd;
                string query = "SELECT MAX(DOC_ID) FROM INV_PURCHASE_HDR";
                //cmd.CommandType = CommandType.Text;
                SqlDataReader rd5;
                //rd5 = cmd.ExecuteReader();
                rd5 = Model.DbFunctions.GetDataReader(query);
                while (rd5.Read())
                {
                    VOUCHNUM.Text = (Convert.ToInt32(rd5[0]) + 1).ToString();
                }
                //conn.Close();
                Model.DbFunctions.CloseConnection();
            }
            catch (Exception ee)
            {
                //conn.Close();
                Model.DbFunctions.CloseConnection();

                //MessageBox.Show(ee.Message);
            }

        }
        public void getDataFromDocNo()
        {
            try
            {
                ID = Convert.ToString(DOC_NO.Text);
                // DOC_NO.Text = ID;

                //conn.Open();
                string cmd = "SELECT * FROM INV_PURCHASE_HDR WHERE DOC_NO = '" + ID + "'";
                //SqlDataReader rd = cmd.ExecuteReader();
                SqlDataReader rd = Model.DbFunctions.GetDataReader(cmd);
                while (rd.Read())
                {
                    PUR_ORDER_DATE.Text = Convert.ToString(rd["DOC_DATE_GRE"]);
                    // DOC_DATE_HIJ.Text = Convert.ToString(rd["DOC_DATE_HIJ"]);
                    type = Convert.ToString(rd["DOC_TYPE"]);
                    SUPPLIER_CODE.Text = Convert.ToString(rd["SUPPLIER_CODE"]);
                 
                    NOTES.Text = Convert.ToString(rd["NOTES"]);
                    TOTAL_AMOUNT.Text = Convert.ToString(rd["GROSS"]);
                    DISCOUNT.Text = Convert.ToString(rd["DISCOUNT_VAL"]);
                    NETT_AMOUNT.Text = Convert.ToString(rd["NET_VAL"]);
                    TOTAL_TAX_AMOUNT.Text = Convert.ToString(rd["TAX_TOTAL"]);
                    CESS.Text = Convert.ToString(rd["CESS_AMOUNT"]);
                    PAY_CODE.Text = Convert.ToString(rd["PAY_CODE"]);
                    //CARD_NO.Text = Convert.ToString(rd["CARD_NO"]);
                    
                    VOUCHNUM.Text = Convert.ToString(rd["DOC_ID"]);
                }
                //conn.Close();
                Model.DbFunctions.CloseConnection();

                //conn.Open();
                cmd = "SELECT * FROM INV_PURCHASE_DTL WHERE DOC_NO = '" + ID + "'";
                //SqlDataReader r = cmd.ExecuteReader();
                SqlDataReader r = Model.DbFunctions.GetDataReader(cmd);
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
                    c["uUnit"].Value = r["UOM"];
                    c["uQty"].Value = r["QTY_RCVD"];
                    c["uPrice"].Value = r["PRICE_FOB"];
                    c["uTaxPercent"].Value = r["ITEM_TAX_PER"];
                    c["uTaxAmt"].Value = r["ITEM_TAX"];
                    c["uTotal"].Value = r["ITEM_GROSS"];
                    c["SerialNos"].Value = r["SERIALNO"];
                    c["uDiscType"].Value = r["DiscType"];
                    if (r["DiscountAmt"].ToString() == "")
                    {
                        c["uDiscount"].Value = "0";
                    }
                    else
                    {
                        c["uDiscount"].Value = r["DiscountAmt"];
                    }

                    if (r["DiscValue"].ToString() == "")
                    {
                        c["uDiscValue"].Value = "0";
                    }
                    else
                    {
                        c["uDiscValue"].Value = r["DiscValue"];
                    }


                    if (r["NET_AMOUNT"] == null || r["NET_AMOUNT"].ToString() == "")
                    {
                        c["uNet_Amount"].Value = "0";
                    }
                    else
                    {
                        c["uNet_Amount"].Value = r["NET_AMOUNT"];
                    }

                    if (r["RTL_PRICE"] == null || r["RTL_PRICE"].ToString() == "")
                    {
                        c["uRTL_PRICE"].Value = "0";
                    }
                    else
                    {
                        c["uRTL_PRICE"].Value = r["RTL_PRICE"];
                    }
                }
                //conn.Close();
                Model.DbFunctions.CloseConnection();
            }
            catch
            {
                //conn.Close();
                Model.DbFunctions.CloseConnection();
            }

        }
        public Purchase_Order(string docType, string prefix)
        {
            InitializeComponent();
            //cmd.Connection = conn;
            //adapter.SelectCommand = cmd;
            type = docType;
            //Text += " - " + prefix;
            QTY_RCVD.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            PRICE_FOB.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            ITEM_TAX_PER.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            ITEM_TAX.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            ITEM_GROSS.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            RTL_PRICE.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            PRICE_TYPE.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            ITEM_DISCOUNT.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            //GetLedgers();
            GetMaxDocID();
            //     SUPPLIER_CODE.Select();
        }
        public Purchase_Order(string docNo)
        {
            InitializeComponent();
            //cmd.Connection = conn;
            //adapter.SelectCommand = cmd;
            DOC_NO.Text = docNo;
            POSTID = docNo;
         //   GetLedgers();
            GetMaxDocID();
            getDataFromDocNo();
            //Text += " - " + prefix;
            QTY_RCVD.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            PRICE_FOB.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            ITEM_TAX_PER.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            ITEM_TAX.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            ITEM_GROSS.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            RTL_PRICE.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            PRICE_TYPE.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            ITEM_DISCOUNT.KeyPress += new KeyPressEventHandler(General.OnlyFloat);

            //     SUPPLIER_CODE.Select();
        }

        bool initialload = true;


       
        private void Purchase_Ord_Load(object sender, EventArgs e)
        {
            Class.CompanySetup CompStep = new Class.CompanySetup();
            if (ID == "")
            {
                PUR_ORDER_DATE.Text = CompStep.GettDate();
            }
            bindgridview();
            hasBatch = General.IsEnabled(Settings.Batch);
            hasTax = General.IsEnabled(Settings.Tax);
            hasBarcode = General.IsEnabled(Settings.Barcode);
            HasArabic = General.IsEnabled(Settings.Arabic);
            PriceLastPurchase = General.IsEnabled(Settings.SelectLastPurchase);
            HasAccounts = Properties.Settings.Default.Account;

            //  HasAccessLimit = General.IsEnabled(Settings.HasAccessLimit);


            pnlarabic.Visible = hasTax;
            uTaxPercent.Visible = hasTax;
            uTaxAmt.Visible = hasTax;
            uBatch.Visible = hasBatch;
            uExpDate.Visible = hasBatch;
            panBatch.Visible = hasBatch;
            panTax.Visible = hasTax;
            panBarcode.Visible = hasBarcode;

            getsalesman();
            initialload = false;
            BindSettings();
            BindTradeMark();
            BindGroup();
            BindType();
            BindCategory();
            PNL_TYPE.Visible = HasType;
            PNL_CATEGORY.Visible = HasCategory;
            PNL_GROUP.Visible = HasGroup;
            PNL_TM.Visible = HasTM;

            

        }

        public void bindgridview()
        {
            try
            {
                //SqlCommand Cmd2 = new SqlCommand();
                string Cmd2 = "";
                DataTable dt2 = new DataTable();
                //SqlDataAdapter Adaptor2 = new SqlDataAdapter();
                //Cmd2.Connection = conn;
                if (PriceLastPurchase)
                {
                    //Cmd2.CommandText = "Sp_ItemLastPurchasePrice";
                    //Cmd2.CommandType = CommandType.StoredProcedure;
                    Cmd2 = "Sp_ItemLastPurchasePrice";
                   dt2= Model.DbFunctions.GetDataTableProcedure(Cmd2);
                }
                else
                {
                    Cmd2 = "SELECT        INV_ITEM_DIRECTORY.CODE AS [Item Code], INV_ITEM_DIRECTORY_UNITS.BARCODE AS Barcode, INV_ITEM_DIRECTORY.DESC_ENG AS [Item Name], INV_ITEM_DIRECTORY_UNITS.UNIT_CODE, INV_ITEM_PRICE.PRICE, INV_ITEM_DIRECTORY.TaxId, INV_ITEM_DIRECTORY.HASSERIAL, INV_ITEM_DIRECTORY.HASWARRENTY, INV_ITEM_DIRECTORY.PERIOD, INV_ITEM_DIRECTORY.PERIODTYPE,INV_ITEM_TYPE.DESC_ENG AS TYPE,INV_ITEM_CATEGORY.DESC_ENG AS CATEGORY,INV_ITEM_GROUP.DESC_ENG AS 'GROUP',INV_ITEM_TM.DESC_ENG AS TRADEMARK FROM            INV_ITEM_PRICE INNER JOIN  INV_ITEM_DIRECTORY_UNITS ON INV_ITEM_PRICE.UNIT_CODE = INV_ITEM_DIRECTORY_UNITS.UNIT_CODE AND INV_ITEM_PRICE.ITEM_CODE = INV_ITEM_DIRECTORY_UNITS.ITEM_CODE  RIGHT OUTER JOIN INV_ITEM_DIRECTORY ON INV_ITEM_DIRECTORY_UNITS.ITEM_CODE = INV_ITEM_DIRECTORY.CODE LEFT OUTER JOIN INV_ITEM_TYPE ON INV_ITEM_DIRECTORY.TYPE=INV_ITEM_TYPE.CODE LEFT OUTER JOIN INV_ITEM_CATEGORY ON INV_ITEM_DIRECTORY.CATEGORY=INV_ITEM_CATEGORY.CODE LEFT OUTER JOIN INV_ITEM_GROUP ON INV_ITEM_DIRECTORY.[GROUP]=INV_ITEM_GROUP.CODE LEFT OUTER JOIN INV_ITEM_TM ON INV_ITEM_DIRECTORY.TRADEMARK=INV_ITEM_TM.CODE WHERE (INV_ITEM_PRICE.SAL_TYPE = 'PUR')";
                    //Cmd2.CommandType = CommandType.Text;
                   dt2= Model.DbFunctions.GetDataTable(Cmd2);
                }
                //DataTable dt2 = new DataTable();
                //Adaptor2.SelectCommand = Cmd2;
                //Adaptor2.Fill(dt2);
                if (dt2.Rows.Count > 0)
                {
                    source2.DataSource = dt2;
                    dataGridItem.DataSource = source2;
                    dataGridItem.RowHeadersVisible = false;
                    dataGridItem.Columns[1].Visible = false;

                    dataGridItem.Columns["GROUP"].Visible = HasGroup;
                    dataGridItem.Columns["CATEGORY"].Visible = HasCategory;
                    dataGridItem.Columns["TYPE"].Visible = HasType;
                    dataGridItem.Columns["TRADEMARK"].Visible = HasTM;
                    dataGridItem.Columns["TaxId"].Visible = hasTax;
                    dataGridItem.Columns["Discount"].Width = 50;
                    //   dataGridItem.ClearSelection();
                }

            }
            catch (Exception EE)
            {
                //  MessageBox.Show(EE.Message);
            }


        }
        public void BindCategory()
        {

            try
            {
                DataTable dt = new DataTable();
                dt = stkrpt.BindCategory();

                DrpCategory.ValueMember = "CODE";
                DrpCategory.DisplayMember = "DESC_ENG";

                DataRow row = dt.NewRow();
                row[0] = "";
                dt.Rows.InsertAt(row, 0);
                DrpCategory.DataSource = dt;

            }
            catch
            {
            }
        }
        public void BindType()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = stkrpt.BindType();



                CMBTYPE.ValueMember = "CODE";
                CMBTYPE.DisplayMember = "DESC_ENG";


                DataRow row = dt.NewRow();
                row[0] = "";
                dt.Rows.InsertAt(row, 0);
                CMBTYPE.DataSource = dt;
            }
            catch
            {
            }
        }
        public void BindGroup()
        {

            try
            {
                DataTable dt = new DataTable();
                dt = stkrpt.BindGroup();

                Group.ValueMember = "CODE";
                Group.DisplayMember = "DESC_ENG";

                DataRow row = dt.NewRow();
                row[0] = "";
                dt.Rows.InsertAt(row, 0);
                Group.DataSource = dt;
            }
            catch
            {
            }
        }
        public void BindTradeMark()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = stkrpt.BindTrademark();

                Trademark.ValueMember = "CODE";
                Trademark.DisplayMember = "DESC_ENG";

                DataRow row = dt.NewRow();
                row[0] = "";
                dt.Rows.InsertAt(row, 0);
                Trademark.DataSource = dt;
            }
            catch
            {
            }
        }
        public void getsalesman()
        {
            SalesManCode = lg.EmpId;
            //conn.Open();
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.CommandText = "GetSalesMan";
            //cmd.Parameters.Clear();
            //cmd.Parameters.AddWithValue("@Empid", lg.EmpId);
            string cmd = "GetSalesMan";
            Dictionary<string, object> parameter = new Dictionary<string, object>();
            parameter.Add("@Empid", lg.EmpId);
            salesmanname = Convert.ToString(Model.DbFunctions.GetAValueProcedure(cmd,parameter));
           // salesmanname = Convert.ToString(cmd.ExecuteScalar());
           // conn.Close();

        }
        public void BindSettings()
        {
            try
            {
                DataTable dt = new DataTable();
                //conn.Open();
                //cmd.Connection = conn;
               // cmd.CommandType = CommandType.Text;

                string query = "SELECT * FROM SYS_SETUP";
                //adapter.SelectCommand = cmd;
                //adapter.Fill(dt);
                dt = Model.DbFunctions.GetDataTable(query);

                if (dt.Rows.Count > 0)
                {



                    //hasBatch = Convert.ToBoolean(dt.Rows[0]["BATCH"]);
                    //hasTax = Convert.ToBoolean(dt.Rows[0]["TAX"]);
                    //hasBarcode = Convert.ToBoolean(dt.Rows[0]["BARCODE"]);
                    //hasArabic = Convert.ToBoolean(dt.Rows[0]["Arabic"]);
                    //MoveToPrice = Convert.ToBoolean(dt.Rows[0]["MoveToPrice"]);

                    //MoveToDisc = Convert.ToBoolean(dt.Rows[0]["MoveToDisc"]);
                    //ShowPurchase = Convert.ToBoolean(dt.Rows[0]["ShowPurchase"]);

                    //SeelctLastPurchase = Convert.ToBoolean(dt.Rows[0]["SelectLastPurchase"]);
                    //FocusDate = Convert.ToBoolean(dt.Rows[0]["FocusDate"]);
                    //FocusCustomer = Convert.ToBoolean(dt.Rows[0]["FocusCustomer"]);
                    //FocusSalesMan = Convert.ToBoolean(dt.Rows[0]["FocusSalesMan"]);
                    //MoveToTaxper = Convert.ToBoolean(dt.Rows[0]["MoveToTaxper"]);
                    //MoveToUnit = Convert.ToBoolean(dt.Rows[0]["MoveToUnit"]);
                    //MoveToQty = Convert.ToBoolean(dt.Rows[0]["MoveToQty"]);
                    //SalebyItemName = Convert.ToBoolean(dt.Rows[0]["SalebyItemName"]);
                    //SalebyItemCode = Convert.ToBoolean(dt.Rows[0]["SalebyItemCode"]);
                    //SalebyBarcode = Convert.ToBoolean(dt.Rows[0]["SalebyBarcode"]);


                    //Discountpercentlimit = Convert.ToDouble(dt.Rows[0]["DiscountPerct"]);
                    //DiscountAmtlimit = Convert.ToDouble(dt.Rows[0]["DiscountAmt"]);
                    //HasDiscountLimit = Convert.ToBoolean(dt.Rows[0]["Hasdiscountlimit"]);
                    //HasCustomerDiscount = Convert.ToBoolean(dt.Rows[0]["AllowCustomerDiscount"]);
                    //PrintPage.Text = Convert.ToString(dt.Rows[0]["Invoice"]);
                    //DafaultRateType = Convert.ToString(dt.Rows[0]["DefaultRateType"]);
                    //// HasStockoutSale = Convert.ToBoolean(dt.Rows[0]["StockOut"]);
                    //PrintInvoices = Convert.ToBoolean(dt.Rows[0]["PrintInvoice"]);
                    HasType = Convert.ToBoolean(dt.Rows[0]["HasType"]);
                    HasCategory = Convert.ToBoolean(dt.Rows[0]["HasCategory"]);
                    HasGroup = Convert.ToBoolean(dt.Rows[0]["HasGroup"]);
                    HasTM = Convert.ToBoolean(dt.Rows[0]["HasTM"]);

                    PUR_MoveDisc = Convert.ToBoolean(dt.Rows[0]["PUR_MoveDisc"]);
                    PUR_MoveRtlper = Convert.ToBoolean(dt.Rows[0]["PUR_MoveRtlper"]);
                    PUR_MoveRtlAmt = Convert.ToBoolean(dt.Rows[0]["PUR_MoveRtlAmt"]);
                    PUR_MoveTaxper = Convert.ToBoolean(dt.Rows[0]["PURMoveTaxper"]);
                    PUR_MoveTaxAmt = Convert.ToBoolean(dt.Rows[0]["PURMoveTaxAmt"]);
                    PUR_MoveTotal = Convert.ToBoolean(dt.Rows[0]["PURMoveTotal"]);
                    PUR_FocusDate = Convert.ToBoolean(dt.Rows[0]["PURFocusDate"]);
                    PUR_FocusSupplier = Convert.ToBoolean(dt.Rows[0]["PURFocusSupplier"]);
                    PUR_FocusInvoice = Convert.ToBoolean(dt.Rows[0]["PUR_FocusInvoice"]);
                    PUR_FocusReference = Convert.ToBoolean(dt.Rows[0]["PUR_FocusReference"]);
                    PUR_FocusBarcode = Convert.ToBoolean(dt.Rows[0]["PUR_FocusBarcode"]);
                    PUR_FocusItemCode = Convert.ToBoolean(dt.Rows[0]["PUR_FocusItemCode"]);
                    PUR_FocusItemName = Convert.ToBoolean(dt.Rows[0]["PUR_FocusItemName"]);





                   // conn.Close();
                }
            }
            catch (Exception ee)
            {
                //conn.Close();
                MessageBox.Show(ee.Message);
            }
        }

        private void ITEM_NAME_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (ITEM_NAME.Text == "")
                {
                    PNL_DATAGRIDITEM.Visible = false;
                }
                else
                {
                    //  bindgridview();
                    PNL_DATAGRIDITEM.Visible = true;
                    source2.Filter = string.Format("[Item Name] LIKE '%{0}%' ", ITEM_NAME.Text);
                    dataGridItem.ClearSelection();
                    dataGridItem.Columns["Item Name"].Width = 250;
                }

                if (clearitemname)
                {
                    ITEM_NAME.Text = "";
                    clearitemname = false;
                }


            }
            catch
            {
            }
        }
        int TaxId;
        private void dataGridItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //string itemcode = dataGridItem.CurrentRow.Cells[0].Value.ToString();
                // itemSelected = true;
                // PNL_DATAGRIDITEM.Visible = false;

                // ITEM_CODE.Text = itemcode;
                // HASSERIAL = Convert.ToBoolean(dataGridItem.CurrentRow.Cells["HASSERIAL"].Value);
                // PNLSERIAL.Visible = Convert.ToBoolean(dataGridItem.CurrentRow.Cells["HASSERIAL"].Value);
                // if (hasBatch)
                // {
                //     BATCH.Focus();
                // }
                // else
                // {
                //     if (HASSERIAL)
                //     {
                //         SERIALNO.Focus();
                //     }
                //     else
                //     {
                //         QTY_RCVD.Text = "1";
                //         QTY_RCVD.Focus();
                //     }

                // }


                // addUnits();
                // UOM.Text = dataGridItem.CurrentRow.Cells[3].Value.ToString();
                // TaxId = Convert.ToInt16(dataGridItem.CurrentRow.Cells["TaxId"].Value.ToString());
                // PRICE_FOB.Text = dataGridItem.CurrentRow.Cells[4].Value.ToString();
                // //  getRate();
                // GetTaxRate();

                // // addItem();
                // // clearItem();
                // //   ITEM_NAME.Focus();




                // ITEM_NAME.Text = dataGridItem.CurrentRow.Cells[2].Value.ToString();
                // ITEM_CODE.Text = itemcode;
                // BARCODE.Text = dataGridItem.CurrentRow.Cells["Barcode"].Value.ToString();
                // PNL_DATAGRIDITEM.Visible = false;
                // itemSelected = false;
                string itemcode = dataGridItem.CurrentRow.Cells["Item Code"].Value.ToString();
                itemSelected = true;
                ITEM_CODE.Text = itemcode;
                //  PNL_DATAGRIDITEM.Visible = false;

                ITEM_CODE.Text = itemcode;
                HASSERIAL = Convert.ToBoolean(dataGridItem.CurrentRow.Cells["HASSERIAL"].Value);
                PNLSERIAL.Visible = Convert.ToBoolean(dataGridItem.CurrentRow.Cells["HASSERIAL"].Value);
                if (hasBatch)
                {
                    BATCH.Focus();
                }
                else
                {
                    if (HASSERIAL)
                    {
                        SERIALNO.Focus();
                    }
                    else
                    {
                        QTY_RCVD.Text = "1";
                        QTY_RCVD.Focus();
                    }

                }


                addUnits();
                UOM.Text = dataGridItem.CurrentRow.Cells["UNIT_CODE"].Value.ToString();
                TaxId = Convert.ToInt16(dataGridItem.CurrentRow.Cells["TaxId"].Value.ToString());
                PRICE_FOB.Text = dataGridItem.CurrentRow.Cells["PRICE"].Value.ToString();
                //  getRate();
                GetTaxRate();

                // addItem();
                // clearItem();
                //   ITEM_NAME.Focus();




                ITEM_NAME.Text = dataGridItem.CurrentRow.Cells["Item Name"].Value.ToString();
                BARCODE.Text = dataGridItem.CurrentRow.Cells["Barcode"].Value.ToString();
                PNL_DATAGRIDITEM.Visible = false;
                itemSelected = false;



            }

            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }
        public void GetTaxRate()
        {
            try
            {
                //conn.Open();
                string query = "SELECT TaxRate from GEN_TAX_MASTER where TaxId=" + TaxId;
                //cmd.CommandType = CommandType.Text;
                SqlDataReader r = Model.DbFunctions.GetDataReader(query);
                while (r.Read())
                {
                    ITEM_TAX_PER.Text = r[0].ToString();
                }
                //conn.Close();
                Model.DbFunctions.CloseConnection();

            }
            catch (Exception ex)
            {
                //conn.Close();
                Model.DbFunctions.CloseConnection();

            }
        }
        private void addUnits()
        {
            tableUnits.Rows.Clear();
            DataTable dt = new DataTable();
            try
            {

                try
                {
                    //conn.Open();

                    string cmd = "SELECT UNIT_CODE FROM INV_ITEM_DIRECTORY_UNITS WHERE ITEM_CODE = '" + ITEM_CODE.Text + "'";
                    //cmd.Connection = conn;
                    //adapter.SelectCommand = cmd;
                    //adapter.Fill(dt);
                    dt = Model.DbFunctions.GetDataTable(cmd);
                    UOM.DataSource = dt;
                    UOM.DisplayMember = "UNIT_CODE";
                    UOM.ValueMember = "UNIT_CODE";
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    //conn.Close();

                }

                //cmd.CommandType = CommandType.Text;

                //cmd.CommandText = "SELECT UNIT_CODE FROM INV_ITEM_DIRECTORY_UNITS WHERE ITEM_CODE = '" + ITEM_CODE.Text + "'";

                //adapter.Fill(tableUnits);
                //UOM.DataSource = tableUnits;
                //UOM.DisplayMember = "UNIT_CODE";
                //UOM.ValueMember = "UNIT_CODE";

                //   DataTable DT = new DataTable();

                //cmd.CommandText = "SELECT PRICE FROM INV_ITEM_PRICE WHERE ITEM_CODE = '" + ITEM_CODE.Text + "' AND UNIT_CODE='" + Convert.ToString(UOM.SelectedValue) + "'";
                //adapter.Fill(DT);
                //if (DT.Rows.Count > 0)
                //    PRICE_FOB.Text = DT.Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Item_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (sender.GetType() == typeof(DateTimePicker))
                {
                    if (HASSERIAL)
                    {
                        SERIALNO.Focus();
                    }
                    else
                    {
                        UOM.Focus();
                    }
                }
                else if (sender.GetType() == typeof(KryptonComboBox))
                {
                    QTY_RCVD.Focus();
                }
                else
                {
                    KryptonTextBox txtBox = (sender as KryptonTextBox);
                    switch (txtBox.Name)
                    {
                        case "ITEM_CODE":
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

                        case "QTY_RCVD":
                            PRICE_FOB.Focus();
                            break;

                        case "PRICE_FOB":
                            if (PUR_MoveDisc)
                            {
                                ITEM_DISCOUNT.Focus();
                            }
                            else if (PUR_MoveRtlper)
                            {
                                PRICE_TYPE.Focus();
                            }
                            else if (PUR_MoveRtlAmt)
                            {
                                RTL_PRICE.Focus();
                            }
                            else if (PUR_MoveTaxper)
                            {
                                ITEM_TAX_PER.Focus();
                            }
                            else if (PUR_MoveTaxAmt)
                            {
                                ITEM_TAX.Focus();
                            }
                            else
                            {
                                ITEM_GROSS.Focus();

                            }

                            break;
                        case "ITEM_DISCOUNT":
                            if (PUR_MoveRtlper)
                            {
                                PRICE_TYPE.Focus();
                            }
                            else if (PUR_MoveRtlAmt)
                            {
                                RTL_PRICE.Focus();
                            }
                            else if (PUR_MoveTaxper)
                            {
                                ITEM_TAX_PER.Focus();
                            }
                            else if (PUR_MoveTaxAmt)
                            {
                                ITEM_TAX.Focus();
                            }
                            else
                            {
                                ITEM_GROSS.Focus();

                            }
                            break;
                        case "PRICE_TYPE":
                            if (PUR_MoveRtlAmt)
                            {
                                RTL_PRICE.Focus();
                            }
                            else if (PUR_MoveTaxper)
                            {
                                ITEM_TAX_PER.Focus();
                            }
                            else if (PUR_MoveTaxAmt)
                            {
                                ITEM_TAX.Focus();
                            }
                            else
                            {
                                ITEM_GROSS.Focus();

                            }
                            break;
                        case "RTL_PRICE":
                            if (hasTax)
                            {
                                if (PUR_MoveTaxper)
                                {
                                    ITEM_TAX_PER.Focus();
                                }
                                else if (PUR_MoveTaxAmt)
                                {
                                    ITEM_TAX.Focus();
                                }
                                else
                                {
                                    ITEM_GROSS.Focus();

                                }
                            }
                            else
                            {

                                ITEM_GROSS.Focus();


                            }
                            break;

                        case "ITEM_TAX_PER":
                            if (PUR_MoveTaxAmt)
                            {
                                ITEM_TAX.Focus();
                            }
                            else
                            {
                                ITEM_GROSS.Focus();
                            }
                            break;

                        case "ITEM_TAX":
                            ITEM_GROSS.Focus();
                            break;
                        case "SERIALNO":
                            UOM.Focus();
                            break;

                        case "ITEM_GROSS":
                            if (Editactive)
                            {
                                addItem();
                                ITEM_NAME.Focus();
                            }
                            else
                            {
                                string flag = "0";
                                for (int i = 0; i < dgItems.Rows.Count; i++)
                                {
                                    DataGridViewCellCollection c = dgItems.Rows[i].Cells;
                                    if (Convert.ToString(c[0].Value) == ITEM_CODE.Text && Convert.ToString(c[4].Value) == UOM.Text)
                                    {
                                        if (MessageBox.Show("Item Already Exists! Do you Want to add to Existing Item???", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                        {

                                            selectedRow = i;
                                            QTY_RCVD.Text = Convert.ToString(Convert.ToInt16(c["uQty"].Value) + Convert.ToInt16(QTY_RCVD.Text));

                                            //dgItems.Rows.RemoveAt(i);
                                            // dataGridItem.Visible = false;
                                            //QTY_RCVD.Focus();
                                            addItem();
                                            ITEM_NAME.Focus();
                                            flag = "1";
                                            break;

                                        }
                                        else
                                            break;


                                    }


                                }
                                if (flag == "0")
                                {
                                    addItem();
                                    ITEM_NAME.Focus();
                                }

                            }
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
            else if (e.KeyCode == Keys.Left)
            {
                if (sender is KryptonTextBox)
                {
                    string name = (sender as KryptonTextBox).Name;
                    switch (name)
                    {


                        case "BATCH":
                            EXPIRY_DATE.Focus();
                            break;

                        case "QTY_RCVD":

                            if (QTY_RCVD.SelectionStart == 0)
                            {
                                UOM.Focus();
                            }

                            break;
                        case "PRICE_FOB":

                            if (PRICE_FOB.SelectionStart == 0)
                            {

                                QTY_RCVD.Focus();

                            }
                            break;
                        case "ITEM_TAX_PER":

                            if (ITEM_TAX_PER.SelectionStart == 0)
                            {

                                PRICE_FOB.Focus();

                            }
                            break;
                        case "ITEM_GROSS":
                            if (ITEM_GROSS.SelectionStart == 0)
                            {

                                ITEM_TAX_PER.Focus();

                            }

                            break;
                        default:
                            break;
                    }
                }
                else if (sender is KryptonComboBox)
                {
                    if (hasBatch)
                    {
                        BATCH.Focus();
                    }

                }
            }
        }
        private bool ItemValid()
        {
            bool batch = true;
            if (hasBatch)
            {
                if (BATCH.Text.Trim() == "")
                {
                    batch = false;
                }
            }
            try
            {
                if (Convert.ToInt16(QTY_RCVD.Text) <= 0)
                {
                    MessageBox.Show("Purchase a Quantity greater tha zero");
                    return false;

                }
            }
            catch
            {
                MessageBox.Show("Purchase a Quantity greater tha zero");
                return false;
            }

            if (hasTax)
            {
                if (ITEM_TAX_PER.Text == "")
                {
                    ITEM_TAX_PER.Text = "0";
                }
            }


            if (ITEM_CODE.Text.Trim() != "" && batch && UOM.Text.Trim() != "" && QTY_RCVD.Text.Trim() != "" && PRICE_FOB.Text.Trim() != "")
            {
                if (Convert.ToDouble(QTY_RCVD.Text) > 0)
                {
                    return true;
                }
                else
                    return false;
            }
            else
            {
                MessageBox.Show("Please enter all the fields!");
                return false;
            }
        }

        private void addItem()
        {
            if (ItemValid())
            {
                if (selectedRow == -1)
                {
                    selectedRow = dgItems.Rows.Add(new DataGridViewRow());
                }
                DataGridViewCellCollection c = dgItems.Rows[selectedRow].Cells;
                c["uCode"].Value = ITEM_CODE.Text;
                c["uName"].Value = ITEM_NAME.Text;
                if (hasBatch)
                {
                    c["uBatch"].Value = BATCH.Text;
                    c["uExpDate"].Value = EXPIRY_DATE.Value.ToString("dd/MM/yyyy");
                }
                c["uUnit"].Value = UOM.Text;
                c["uQty"].Value = QTY_RCVD.Text;
                c["uPrice"].Value = PRICE_FOB.Text;
                if (hasTax)
                {
                    c["uTaxPercent"].Value = ITEM_TAX_PER.Text;
                    c["uTaxAmt"].Value = ITEM_TAX.Text;
                }
                c["uTotal"].Value = (Convert.ToDouble(ITEM_GROSS.Text) + Item_Discount_Amt).ToString();
                c["uBarcode"].Value = BARCODE.Text;
                c["SerialNos"].Value = SERIALNO.Text;
                if (RTL_PRICE.Text == "")
                {
                    c["uRTL_PRICE"].Value = "0";
                }
                else
                {
                    c["uRTL_PRICE"].Value = RTL_PRICE.Text;
                }

                c["uDiscType"].Value = DiscType;
                c["UDiscount"].Value = Item_Discount_Amt;
                c["uDiscValue"].Value = ITEM_DISCOUNT.Text;
                c["uNet_Amount"].Value = ITEM_GROSS.Text;

                totalItemAmount();
                ItemClear();
                dgItems.ClearSelection();
                ActiveForm = true;
            }
        }
        private void ItemClear()
        {
            selectedRow = -1;
            ITEM_CODE.Text = "";
            ITEM_NAME.Text = "";
            if (hasBatch)
            {
                BATCH.Text = "";
                EXPIRY_DATE.Value = DateTime.Today;
            }
            tableUnits.Rows.Clear();
            QTY_RCVD.Text = "";
            PRICE_FOB.Text = "";
            if (hasTax)
            {
                ITEM_TAX_PER.Text = "";
                ITEM_TAX.Text = "";
            }
            ITEM_GROSS.Text = "";
            BARCODE.Text = "";
            Editactive = false;
            SERIALNO.Text = "";
            HASSERIAL = false;
            RTL_PRICE.Text = "";
            PRICE_TYPE.Text = "";
            PriceType = "Percentage";
            LBLRATETYPE.Text = "Rtl %";
            source2.Filter = "";
            ITEM_DISCOUNT.Text = "0";
            Item_Discount_Amt = 0;
            UOM.SelectedText = "";
            DiscType = "Percentage";
            lblDiscRate.Text = "Disc %";
            excludechanged = false;
            includechang = false;
            lblPrice.Text = "Gr Price";
        }
        private void totalItemAmount()
        {
            try
            {
                double tax = 0;
                double total = 0;
                double totaldiscount = 0;
                for (int i = 0; i < dgItems.Rows.Count; i++)
                {
                    try
                    {
                        if (hasTax)
                        {
                            tax = tax + Convert.ToDouble(dgItems.Rows[i].Cells["uTaxAmt"].Value);
                        }
                    }
                    catch
                    {
                        tax = tax + 0;
                    }
                    total = total + Convert.ToDouble(dgItems.Rows[i].Cells["uTotal"].Value);

                    totaldiscount = totaldiscount + Convert.ToDouble(dgItems.Rows[i].Cells["uDiscount"].Value);
                }
                if (hasTax)
                {
                    TOTAL_TAX_AMOUNT.Text = tax.ToString();
                    CESS.Text = (tax * 0.01).ToString();
                }
                //   DISCOUNT.Text = (Convert.ToDouble(DISCOUNT.Text) + Convert.ToDouble(dgItems.Rows[i].Cells["uDiscount"].Value)).ToString();
                TOTAL_AMOUNT.Text = (total).ToString();
                DISCOUNT.Text = (totaldiscount).ToString();

            }
            catch (Exception ee)
            {
                //MessageBox.Show(ee.Message);
            }
        }

        private void dataGridItem_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                {
                    string itemcode = dataGridItem.CurrentRow.Cells["Item Code"].Value.ToString();
                    itemSelected = true;
                    ITEM_CODE.Text = itemcode;
                    //  PNL_DATAGRIDITEM.Visible = false;

                    ITEM_CODE.Text = itemcode;
                    HASSERIAL = Convert.ToBoolean(dataGridItem.CurrentRow.Cells["HASSERIAL"].Value);
                    PNLSERIAL.Visible = Convert.ToBoolean(dataGridItem.CurrentRow.Cells["HASSERIAL"].Value);
                    if (hasBatch)
                    {
                        BATCH.Focus();
                    }
                    else
                    {
                        if (HASSERIAL)
                        {
                            SERIALNO.Focus();
                        }
                        else
                        {
                            QTY_RCVD.Text = "1";
                            QTY_RCVD.Focus();
                        }

                    }


                    addUnits();
                    UOM.Text = dataGridItem.CurrentRow.Cells["UNIT_CODE"].Value.ToString();
                    TaxId = Convert.ToInt16(dataGridItem.CurrentRow.Cells["TaxId"].Value.ToString());
                    PRICE_FOB.Text = dataGridItem.CurrentRow.Cells["PRICE"].Value.ToString();
                    //  getRate();
                    GetTaxRate();

                    // addItem();
                    // clearItem();
                    //   ITEM_NAME.Focus();




                    ITEM_NAME.Text = dataGridItem.CurrentRow.Cells["Item Name"].Value.ToString();
                    BARCODE.Text = dataGridItem.CurrentRow.Cells["Barcode"].Value.ToString();
                    PNL_DATAGRIDITEM.Visible = false;
                    itemSelected = false;




                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void btnSupplier_Click(object sender, EventArgs e)
        {
            CommonHelp c = new CommonHelp(0, genEnum.Supplier);
            if (c.ShowDialog() == DialogResult.OK && c.c != null)
            {
              
                SUPPLIER_CODE.Text = Convert.ToString(c.c[0].Value);
                txtSupplierName.Text = Convert.ToString(c.c[1].Value);
            
                //type = "PUR.CRD";
            }
            else
            {
                SUPPLIER_CODE.Text = "";
            }
        }
      /*  public void GetLedgers()
        {
            try
            {
                DataTable dt = new DataTable();
                DataTable dt2 = new DataTable();
                dt = Ledg.Selectledger();
                dt2 = Ledg.Selectledger();
                drpdebitor.DataSource = dt;
                drpdebitor.DisplayMember = "LEDGERNAME";
                drpdebitor.ValueMember = "LEDGERID";

                drpCreditor.DataSource = dt2;
                drpCreditor.DisplayMember = "LEDGERNAME";
                drpCreditor.ValueMember = "LEDGERID";
                if (type == "LGR.PRT")
                    drpCreditor.Text = "CASH ACCOUNT";
                else
                    drpCreditor.Text = "PURCHASE ACCOUNT";

                if (type == "LGR.PRT")
                    drpdebitor.Text = "PURCHASE RETURN ACCOUNT";
                else
                    drpdebitor.Text = "CASH ACCOUNT";


            }
            catch
            {
            }
        }*/

        private void btnAddSupplier_Click(object sender, EventArgs e)
        {
            SupplierMaster sm = new SupplierMaster();
            sm.ShowDialog();
        }

        private void btnPayType_Click(object sender, EventArgs e)
        {
            CommonHelp c = new CommonHelp(0, genEnum.PayType);
            if (c.ShowDialog() == DialogResult.OK && c.c != null)
            {
                PAY_CODE.Text = Convert.ToString(c.c[0].Value);
                PAY_NAME.Text = Convert.ToString(c.c[1].Value);
            }
        }

        private void ITEM_NAME_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Down)
                {

                    // source2.Filter = "";
                    PNL_DATAGRIDITEM.Visible = true;
                    dataGridItem.Focus();
                    if (dataGridItem.Rows.Count > 0)
                    {
                        dataGridItem.CurrentCell = dataGridItem.Rows[0].Cells[2];
                        dataGridItem.Columns["Item Name"].Width = 250;
                    }


                    //if (PNL_DATAGRIDITEM.Visible == true)
                    //{
                    //    dataGridItem.Focus();
                    //    dataGridItem.CurrentCell = dataGridItem.Rows[0].Cells[2];
                    //}
                    //else
                    //{
                    //    if (dgItems.Rows.Count > 0)
                    //    {
                    //        try
                    //        {
                    //             dgItems.Focus();
                    //             dgItems.CurrentCell = dgItems.Rows[0].Cells["uName"]; 

                    //        }
                    //        catch(Exception ee)
                    //        {
                    //          //  MessageBox.Show(ee.Message);
                    //        }
                    //    }
                    //}
                }
                else if (e.KeyCode == Keys.Enter)
                {

                    if (dgItems.Rows.Count > 0)
                    {
                        try
                        {
                            dgItems.Focus();
                            dgItems.CurrentCell = dgItems.Rows[0].Cells["uName"];

                        }
                        catch (Exception ee)
                        {
                            //  MessageBox.Show(ee.Message);
                        }
                    }
                }
            }
            catch
            {
            }
        }

        private void QTY_RCVD_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == '+')
                {

                    if (QTY_RCVD.Text == "")
                    {
                        QTY_RCVD.Text = "1";
                    }
                    else
                    {
                        QTY_RCVD.Text = (Convert.ToInt32(QTY_RCVD.Text) + 1).ToString();
                    }
                }
                else if (e.KeyChar == '-')
                {
                    if (QTY_RCVD.Text == "" || QTY_RCVD.Text == "0" || QTY_RCVD.Text == "1")
                    {
                        QTY_RCVD.Text = "1";
                    }

                    else
                    {
                        QTY_RCVD.Text = (Convert.ToInt32(QTY_RCVD.Text) - 1).ToString();
                    }
                }
            }
            catch
            {
            }
        }

        private void PRICE_FOB_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void CalTotalAmount(object sender, EventArgs e)
        {
            try
            {
                if (PRICE_FOB.Text.Trim() != "" && QTY_RCVD.Text.Trim() != "")
                {
                    ITEM_GROSS.Text = (Convert.ToDouble(QTY_RCVD.Text) * Convert.ToDouble(PRICE_FOB.Text) - Item_Discount_Amt).ToString();
                }
                else
                {
                    ITEM_GROSS.Text = "0";
                }

                if (ITEM_TAX_PER.Text != "")
                {
                    double tax = Convert.ToDouble(ITEM_GROSS.Text) * (Convert.ToDouble(ITEM_TAX_PER.Text) / 100);
                    ITEM_TAX.Text = tax.ToString();
                }
                if (lblPrice.Text == "Gr Price")
                {
                    excludechanged = false;
                    includechang = true;
                }
                else
                {
                    includechang = false;
                    excludechanged = true;
                }
            }
            catch
            {
            }
        }
        public void Clear2()
        {

            ID = "";
            DOC_NO.Text = "";
            date_of_request.Value = DateTime.Today;
          
           
            SUPPLIER_CODE.Text = "";
            txtSupplierName.Text = "";
            PAY_CODE.Text = "";
            PAY_NAME.Text = "";
          
            NOTES.Text = "";
            TOTAL_TAX_AMOUNT.Text = "0.00";
            CESS.Text = "0.00";
            TOTAL_AMOUNT.Text = "0.00";
            DISCOUNT.Text = "0.00";
            NETT_AMOUNT.Text = "0.00";
           
            dgItems.Rows.Clear();
         
          
            BARCODE.Text = "";

            ModifyType = "";
            ItemClear();
            SUPPLIER_CODE.Focus();

            ActiveForm = false;
        }
        private void btnup_Click(object sender, EventArgs e)
        {
            if (!initialload)
            {
                string cmd = "";
                try
                {
                    Clear2();
                    VOUCHNUM.Text = (Convert.ToInt32(VOUCHNUM.Text) + 1).ToString();
                    // checkvoucher(Convert.ToInt16(VOUCHNUM.Text));
                    DataTable dt = new DataTable();
                    //conn.Open();
                     cmd = "SELECT *  INV_PUR_PO_HDR WHERE DOC_ID = '" + VOUCHNUM.Text + "'";
                    //cmd.CommandType = CommandType.Text;
                    //SqlDataAdapter Adap = new SqlDataAdapter();
                    //Adap.SelectCommand = cmd;
                    //Adap.Fill(dt);
                    dt = Model.DbFunctions.GetDataTable(cmd);

                    ID = Convert.ToString(dt.Rows[0]["DOC_NO"]);
                    DOC_NO.Text = ID;
                    POSTID = ID;
                    try
                    {
                        PUR_ORDER_DATE.Text = Convert.ToDateTime(dt.Rows[0]["DOC_DATE_GRE"].ToString()).ToShortDateString();

                    }
                    catch
                    { }
                    //  DOC_DATE_HIJ.Text = Convert.ToString(dt.Rows[0]["DOC_DATE_HIJ"]);
                    SUPPLIER_CODE.Text = Convert.ToString(dt.Rows[0]["SUPPLIER_CODE"]);
                    NOTES.Text = Convert.ToString(dt.Rows[0]["NOTES"]);
                    TOTAL_AMOUNT.Text = Convert.ToString(dt.Rows[0]["GROSS"]);
                    DISCOUNT.Text = Convert.ToString(dt.Rows[0]["DISCOUNT_VAL"]);
                    NETT_AMOUNT.Text = Convert.ToString(dt.Rows[0]["NET_VAL"]);
                    TOTAL_TAX_AMOUNT.Text = Convert.ToString(dt.Rows[0]["TAX_TOTAL"]);
                    CESS.Text = Convert.ToString(dt.Rows[0]["CESS_AMOUNT"]);
                    PAY_CODE.Text = Convert.ToString(dt.Rows[0]["PAY_CODE"]);
                    //     CARD_NO.Text = Convert.ToString(dt.Rows[0]["CARD_NO"]);
               
                    //   Txt_freight.Text = Convert.ToString(dt.Rows[0]["FREIGHT_AMT"]);


                   // conn.Close();
                }
                catch
                {
                   // conn.Close();
                }
                try
                {
                    //conn.Open();
                     cmd = "SELECT * FROM INV_PUR_PO_DTL WHERE DOC_NO = '" + ID + "'";
                    //cmd.CommandType = CommandType.Text;
                    SqlDataReader r = Model.DbFunctions.GetDataReader(cmd);
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
                        c["uUnit"].Value = r["UOM"];
                        c["uQty"].Value = r["QTY_RCVD"];
                        c["uPrice"].Value = r["PRICE_FOB"];
                        c["uTaxPercent"].Value = r["ITEM_TAX_PER"];
                        c["uTaxAmt"].Value = r["ITEM_TAX"];
                        c["uTotal"].Value = r["ITEM_GROSS"];
                        c["SerialNos"].Value = r["SERIALNO"];
                        c["uRTL_PRICE"].Value = r["RTL_PRICE"];
                        c["uDiscType"].Value = r["DiscType"];

                        if (r["DiscountAmt"].ToString() == "")
                        {
                            c["uDiscount"].Value = "0";
                        }
                        else
                        {
                            c["uDiscount"].Value = r["DiscountAmt"];
                        }

                        if (r["DiscValue"].ToString() == "")
                        {
                            c["uDiscValue"].Value = "0";
                        }
                        else
                        {
                            c["uDiscValue"].Value = r["DiscValue"];
                        }


                        if (r["NET_AMOUNT"] == null || r["NET_AMOUNT"].ToString() == "")
                        {
                            c["uNet_Amount"].Value = "0";
                        }
                        else
                        {
                            c["uNet_Amount"].Value = r["NET_AMOUNT"];
                        }
                    }
                    //conn.Close();
                    Model.DbFunctions.CloseConnection();
                  
                }
                catch
                {
                    //conn.Close();
                    Model.DbFunctions.CloseConnection();

                }
            }
        }

        private void btndown_Click(object sender, EventArgs e)
        {
             if (!initialload)
            {
                string query = "";
                try
                {
                    Clear2();
                    VOUCHNUM.Text = (Convert.ToInt32(VOUCHNUM.Text) - 1).ToString();
                    // checkvoucher(Convert.ToInt16(VOUCHNUM.Text));
                    DataTable dt = new DataTable();
                    //conn.Open();
                    query = "SELECT *  INV_PUR_PO_HDR WHERE DOC_ID = '" + VOUCHNUM.Text + "'";
                    //cmd.CommandType = CommandType.Text;
                    //SqlDataAdapter Adap = new SqlDataAdapter();
                    //Adap.SelectCommand = cmd;
                    //Adap.Fill(dt);
                    dt = Model.DbFunctions.GetDataTable(query);

                    ID = Convert.ToString(dt.Rows[0]["DOC_NO"]);
                    DOC_NO.Text = ID;
                    POSTID = ID;
                    try
                    {
                        PUR_ORDER_DATE.Text = Convert.ToDateTime(dt.Rows[0]["DOC_DATE_GRE"].ToString()).ToShortDateString();

                    }
                    catch
                    { }
                    //  DOC_DATE_HIJ.Text = Convert.ToString(dt.Rows[0]["DOC_DATE_HIJ"]);
                    SUPPLIER_CODE.Text = Convert.ToString(dt.Rows[0]["SUPPLIER_CODE"]);
                    NOTES.Text = Convert.ToString(dt.Rows[0]["NOTES"]);
                    TOTAL_AMOUNT.Text = Convert.ToString(dt.Rows[0]["GROSS"]);
                    DISCOUNT.Text = Convert.ToString(dt.Rows[0]["DISCOUNT_VAL"]);
                    NETT_AMOUNT.Text = Convert.ToString(dt.Rows[0]["NET_VAL"]);
                    TOTAL_TAX_AMOUNT.Text = Convert.ToString(dt.Rows[0]["TAX_TOTAL"]);
                    CESS.Text = Convert.ToString(dt.Rows[0]["CESS_AMOUNT"]);
                    PAY_CODE.Text = Convert.ToString(dt.Rows[0]["PAY_CODE"]);
                    //     CARD_NO.Text = Convert.ToString(dt.Rows[0]["CARD_NO"]);
                 
                    //   Txt_freight.Text = Convert.ToString(dt.Rows[0]["FREIGHT_AMT"]);


                    //conn.Close();
                }
                catch
                {
                    //conn.Close();
                }
                try
                {
                    //conn.Open();
                    query = "SELECT * FROM INV_PUR_PO_DTL WHERE DOC_NO = '" + ID + "'";
                    //cmd.CommandType = CommandType.Text;
                    SqlDataReader r = Model.DbFunctions.GetDataReader(query);
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
                        c["uUnit"].Value = r["UOM"];
                        c["uQty"].Value = r["QTY_RCVD"];
                        c["uPrice"].Value = r["PRICE_FOB"];
                        c["uTaxPercent"].Value = r["ITEM_TAX_PER"];
                        c["uTaxAmt"].Value = r["ITEM_TAX"];
                        c["uTotal"].Value = r["ITEM_GROSS"];
                        c["SerialNos"].Value = r["SERIALNO"];
                        c["uRTL_PRICE"].Value = r["RTL_PRICE"];
                        c["uDiscType"].Value = r["DiscType"];

                        if (r["DiscountAmt"].ToString() == "")
                        {
                            c["uDiscount"].Value = "0";
                        }
                        else
                        {
                            c["uDiscount"].Value = r["DiscountAmt"];
                        }

                        if (r["DiscValue"].ToString() == "")
                        {
                            c["uDiscValue"].Value = "0";
                        }
                        else
                        {
                            c["uDiscValue"].Value = r["DiscValue"];
                        }


                        if (r["NET_AMOUNT"] == null || r["NET_AMOUNT"].ToString() == "")
                        {
                            c["uNet_Amount"].Value = "0";
                        }
                        else
                        {
                            c["uNet_Amount"].Value = r["NET_AMOUNT"];
                        }
                    }
                    //conn.Close();
                    Model.DbFunctions.CloseConnection();
                }
                catch
                {
                   // conn.Close();
                    Model.DbFunctions.CloseConnection();

                }
            }
        }
        private bool valid()
        {
            if (type == "PUR.CRD" && SUPPLIER_CODE.Text == "")
            {
                MessageBox.Show("You must select a supplier for a Credit Purchase!");
                return false;
            }

            if (General.IsEnabled(Settings.HasAccessLimit))
            {
                DateTime date = DateTime.Now;
                DataTable dt = new DataTable();
                dt = dset.getdatdetails();
                switch (dt.Rows[0][3].ToString())
                {
                    case "Date":
                        date = Convert.ToDateTime(dt.Rows[0][1].ToString());
                        break;
                    case "Period":
                        int days = 0;
                        switch (dt.Rows[0][4].ToString())
                        {
                            case "Y":
                                days = 365 * Convert.ToInt16(dt.Rows[0][2].ToString()) * -1;
                                break;
                            case "M":
                                days = 30 * Convert.ToInt16(dt.Rows[0][2].ToString()) * -1;
                                break;
                            case "D":

                                days = Convert.ToInt16(dt.Rows[0][2].ToString()) * -1;
                                break;
                        }
                        try
                        {
                            date = DateTime.Now.AddDays(days);
                        }
                        catch
                        {
                            date = DateTime.Now;
                        }

                        break;
                }

                date = Convert.ToDateTime(date.ToShortDateString());
                if (date <= Convert.ToDateTime(PUR_ORDER_DATE.Value.ToShortDateString()))
                {

                }
                else
                {
                    MessageBox.Show("Date Limit Exceeded!!");
                    return false;
                }
            }

            if (ID != "")
            {
                if (NOTES.Text == "")
                {
                    NOTES.Focus();
                    MessageBox.Show("Enter Reason for Updation in Remarks");
                    return false;

                }
            }

            if (dgItems.Rows.Count <= 0)
            {
                MessageBox.Show("Please add items to save");
                return false;

            }


       /*     if (HasAccounts)
            {
                if (drpCreditor.SelectedIndex < 0 || drpdebitor.SelectedIndex < 0)
                {
                    MessageBox.Show("Please select debitor and creditor accounts");
                    return false;
                }
                else
                {

                    return true;
                }
            }*/
            else
            {
                return true;
            }


        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                string status = "Added!";
                TransDate = PUR_ORDER_DATE.Value;
                if (SUPPLIER_CODE.Text != "")
                {
                    type = "sal.crd";
                }
                else
                {
                    type = "sal.css";
                }

                string query = "";
                if (valid())
                {
                    query = "INSERT INTO INV_PUR_PO_HDR(BRANCH,DOC_NO,DOC_TYPE,DOC_DATE_GRE,DOC_DATE_GE,SUPPLIER_CODE,NOTES,TAX_TOTAL,DISCOUNT,TOTAL_AMOUNT,NET_AMOUNT,DOC_ID,BILLING_ADD,SHIPPING_ADD) ";
                    query += "values('" + lg.Branch + "','" + DOC_NO + "','" + type + "','" + PUR_ORDER_DATE.Value.ToString("mm/dd/yyyy") + "','" + date_of_request.Value.ToString("mm/dd/yyyy") + "','" + SUPPLIER_CODE.Text + "', '" + NOTES.Text + "','" + Convert.ToDecimal(TOTAL_TAX_AMOUNT.Text) + "','" + Convert.ToDecimal(DISCOUNT.Text) + "','" + Convert.ToDecimal(TOTAL_AMOUNT.Text) + "','" + Convert.ToDecimal(NETT_AMOUNT.Text) + "','" + Convert.ToInt32(VOUCHNUM.Text) + "','" + tb_billingadd.Text + "','" + krtb_shipaddr.Text + "');";


                    query += "INSERT INTO INV_SAL_ORD_DTL(DOC_ID,DOC_TYPE,DOC_NO,ITEM_CODE,ITEM_DESC_ENG,UOM,PRICE,QUANTITY,ITEM_DISCOUNT,DISC_TYPE,DISC_VALUE,BRANCH";
                    //if (hastax)
                    //{
                    //    cmd.commandtext += ",item_tax_per,item_tax";
                    //}
                    //if (hasbatch)
                    //{
                    //    cmd.commandtext += ",batch,expiry_date";
                    //}
                    query += ")";
                    for (int i = 0; i < dgItems.Rows.Count; i++)
                    {
                        DataGridViewCellCollection c = dgItems.Rows[i].Cells;
                        query += "SELECT '" + VOUCHNUM.Text + "','" + type + "','" + DOC_NO.Text + "','" + c["uCode"].Value + "','" + c["uName"].Value + "','" + c["uUnit"].Value + "','" + Convert.ToDecimal(c["uPrice"].Value) + "','" + Convert.ToDecimal(c["cDisc"].Value) + "','" + c["Disctypes"].Value + "','" + c["Discvalues"].Value + "','" + lg.Name + "'";
                        //if (hastax)
                        //{
                        //    cmd.commandtext += ",'" + c["ctaxper"].value + "','" + c["ctaxamt"].value + "'";
                        //}
                        //if (hasbatch)
                        //{
                        //    cmd.commandtext += ",'" + c["cbatch"].value + "','" + datetime.parseexact(c["cexpdate"].value.tostring(), "dd/mm/yyyy", null).tostring("mm/dd/yyyy") + "'";
                        //}
                        query += " union all ";
                    }
                    //inserting details into credit table 


                    // }
                }
                else
                {
                    //modifiedtransaction();
                    status = "updated!";
                    query = "UPDATE INV_PUR_PO_DTL SET DOC_ID='" + Convert.ToInt32(VOUCHNUM.Text) + "', DOC_DATE_ORDER = '" + PUR_ORDER_DATE.Value.ToString("mm/dd/yyyy") + "', DOC_DATE_VALID = '" + date_of_request.Value.ToString("mm/dd/yyyy") + "',CUSTOMER_CODE = '" + "',salesman_code = '" + SALESMAN_CODE.Text + "',DiscountAmt = '" + DISCOUNT.Text + "',NET_AMOUNT = '" + NETT_AMOUNT.Text + "' where DOC_NO = '" + DOC_NO.Text + "';";
                    query += "DELETE FROM INV_PUR_PO_DTL WHERE DOC_NO = '" + DOC_NO.Text + "'";
                    query += "INSERT INTO INV_SAL_ORD_DTL(DOC_TYPE,DOC_NO,ITEM_CODE,ITEM_DESC_ENG,UOM,PRICE,QUANTITY,ITEM_DISCOUNT,DISC_TYPE,DISC_VALUE,SERIALNO,BRANCH";
                    //if (hastax)
                    //{
                    //    cmd.commandtext += ",item_tax_per,item_tax";
                    //}
                    //if (hasbatch)
                    //{
                    //    cmd.commandtext += ",batch,expiry_date";
                    //}
                    query += ")";
                    for (int i = 0; i < dgItems.Rows.Count; i++)
                    {
                        DataGridViewCellCollection c = dgItems.Rows[i].Cells;
                        query += "select '" + type + "','" + DOC_NO.Text + "','" + c["uCode"].Value + "','" + c["uName"].Value + "','" + c["uUnit"].Value + "','" + Convert.ToDecimal(c["uPrice"].Value) + "','" + Convert.ToInt16(c["uQty"].Value) + "','" + Convert.ToDecimal(c["udisc"].Value) + "','" + c["udisctypes"].Value + "','" + c["udiscvalues"].Value + "','" + lg.Branch + "'";
                        //if (hastax)
                        //{
                        //    cmd.commandtext += ",'" + c["ctaxper"].value + "','" + c["ctaxamt"].value + "'";
                        //}
                        //if (hasbatch)
                        //{
                        //    cmd.commandtext += ",'" + c["cbatch"].value + "','" + datetime.parseexact(c["cexpdate"].value.tostring(), "dd/mm/yyyy", null).tostring("mm/dd/yyyy") + "'";
                        //}
                        query += " union all ";
                    }


                    // deletetransation();
                    //if (type == "sal.creditnote")
                    //{
                    //    insertintocredittable();
                    //}

                    //  if (printinvoice.checked == true)
                    //  {
                    //               printinginitial();
                    //  }
                }

                query = query.Substring(0, query.Length - 10);
                try
                {

                    //conn.Open();
                    //cmd.CommandType = CommandType.Text;
                    try
                    {
                        //cmd.ExecuteNonQuery();
                        Model.DbFunctions.InsertUpdate(query);
                    }
                    catch { }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                //inserttransaction();
                //freighttransaction();
                //roundofftransaction();
                //cesstransaction();
                //vattransaction();
                //discounttransaction();


                //if (type == "sal.creditnote")
                //{
                //    messagebox.show("credit note " + status);
                //}
                //else
                //{
                //    //  messagebox.show("sales " + status);

                //}
                //if (txtcreditnoteno.text != "")
                //{
                //    updatecreditnote();
                //}


                //receiptvoucher();

                btnClear.PerformClick();

            }
            catch(Exception ex) {
                string st = ex.Message;
            }

            }

        private void btnClear_Click(object sender, EventArgs e)
        {

        }
        

      
       
    }
}
