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
using System.Drawing.Printing;
using System.Globalization;
using System.Threading;
using System.Net;
using System.Drawing.Imaging;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;
using Sys_Sols_Inventory.Model;
namespace Sys_Sols_Inventory.POSDESK
{
    public partial class SalesPOS : Form
    {
        private bool hasSaleExclusive = false;
        private bool hasSaleExclusive1 = false;
        string hcnNO = "";
        string c_price = "";
        string sup_id = "";
        string sup_name = "";
        string decimalFormat;
        int height_col_head;
        decimal tgrossrate = 0, ttaxva = 0, trate = 0, tcdis = 0, ttaxbl = 0, tfree = 0;
      double  pricefob=0;
      double sales_price = 0;
        Int32 tqty = 0;
        decimal txttot = 0;
        double mrp;
        Boolean flagPrintEventAssigned = false;
        int counter = 0;
        BindingSource src = new BindingSource();
        GeneralSettings obj = new GeneralSettings();
        #region properties declaration
        private bool hasBatch = true;
        private bool hasTax = true;
        private bool hasBarcode = true;
        private bool hasArabic = true;
        private bool hasFree = true;
        private bool hasMrp = true;
        private bool hasGrossValue = true;
        private bool hasDescription = true;
        private bool hasNetValue = true;
        private DataTable unitsTable = new DataTable();
        private int selectedRow = -1;
        private string ID = "";
        private string type = "";
        private string type1 = "";
        bool excludechanged = false;
        private bool EditActive = false;
        private bool HasAccounts = false;
        private bool itemSelected = false;
        private bool HasArabicInvoice = true;
        public bool ShowStock = false;
        bool includechang = false;

        double Discountpercentlimit, DiscountAmtlimit;
        bool HasSeasonDiscount = false;
        private bool HasDiscountLimit = false;
        public bool BarcodeFlag = false;
        private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        private SqlCommand cmd = new SqlCommand();
        private SqlDataAdapter adapter = new SqlDataAdapter();
        private DataTable table = new DataTable();
        private DataTable table_for_batch = new DataTable();
        private DataTable filterTable = new DataTable();
        private DataTable rateTable = new DataTable();
        private BindingSource source = new BindingSource();
        private AutoCompleteStringCollection coll = new AutoCompleteStringCollection();
        Class.CompanySetup ComSet = new Class.CompanySetup();
        Class.Printing Printing = new Class.Printing();
        Class.DateSettings dset = new Class.DateSettings();
        Class.Stock_Report stkrpt = new Class.Stock_Report();

        Class.Ledgers led = new Class.Ledgers();
        Class.Transactions trans = new Class.Transactions();
        private DateTime TransDate;
        #endregion
        Initial mdi = (Initial)Application.OpenForms["Initial"];
        Login lg = (Login)Application.OpenForms["Login"];
        Class.ModifiedTransaction modtrans = new Class.ModifiedTransaction();
        public decimal PurchasePrice = 0;
        public decimal Profit = 0;
        int m = 0;
        int k = 0;
        int n = 0;
        Class.Stock_Report Stkr = new Class.Stock_Report();
        Class.Cls_additem_ba ba = new Class.Cls_additem_ba();
        Class.Cls_additem_da da = new Class.Cls_additem_da();
        Class.Ledgers Ledg = new Class.Ledgers();
        Model.ClsTemplate temp = new ClsTemplate();

        InvSalesHdrDB InvSalesHdrDB = new InvSalesHdrDB();
        InvSalesDtlDB InvSalesDtlDB = new InvSalesDtlDB();
        InvStkTrxHdrDB InvStkTrxHdrDB = new InvStkTrxHdrDB();
        InvStkTrxDtlDB InvStkTrxDtlDB = new InvStkTrxDtlDB();
        StockDB StockDB = new StockDB();
        InvSalOrdDtlDB InvSalOrdDtlDB = new InvSalOrdDtlDB();
        clsCustomer clsCustomer = new clsCustomer();
        clsCommon clsCommon = new clsCommon();
        ItemDirectoryDB ItemDirectoryDB = new ItemDirectoryDB();
        InvItemDirectoryUnits InvItemDirectoryUnits = new InvItemDirectoryUnits();
        TbTransactionsDB TbTransactionsDB = new TbTransactionsDB();
        RateChangeDB RateChangeDB = new RateChangeDB();
        RecRecieptVoucherHdrDB RecRecieptVoucherHdrDB = new RecRecieptVoucherHdrDB();
        Class.Ledgers Ledgers = new Class.Ledgers();
        ProjectDB ProjectDB = new ProjectDB();
       
        List<CurrencyInfo> currencies = new List<CurrencyInfo>();

        Label lblGrossTotal = new Label();
        TextBox txtGrossTotal = new TextBox();
        Label lblLineDiscTotal = new Label();
        TextBox txtLineDiscTotal = new TextBox();
        Label lblNetTotal = new Label();
        TextBox txtNetTotal = new TextBox();
        Label lblTaxTotal = new Label();
        TextBox txtTaxTotal = new TextBox();
        Label lblItemTotal = new Label();
        TextBox txtItemTotal = new TextBox();
        Control controll = null;
        double grossTotal;
        double discTotal;
        double netTotal;
        double taxTotal;
        double itemTotal;
        string TAXTYPE = "";
        #region variables declaration and printing declaration
        PrintDocument printDocumentA4 = new PrintDocument();
        PrintDocument printDocumentA48b = new PrintDocument();
        PrintDocument printDocumentA4estimate = new PrintDocument();
        bool closeFrom = false;
        bool ActiveForm = false;
        decimal DecPOSVoucherTypeId = 0;        //to get the selected voucher type id from frmVoucherTypeSelection       
        decimal decPOSSuffixPrefixId = 0;
        decimal decProductId = 0;               //to fill product using barcode
        decimal decBatchId;
        decimal decCurrentConversionRate;
        decimal decCurrentRate;
        decimal decSalesMasterId = 0;
        decimal decSalesDetailsId = 0;
        decimal pur_price = 0;
        string strCashOrParty;
        string strSalesAccount;
        string strCounter;
        string strSalesMan;
        string strPrefix = string.Empty;        //to get the prefix string from frmvouchertypeselection
        string strSuffix = string.Empty;        //to get the suffix string from frmvouchertypeselection
        string strVoucherNo = string.Empty;
        string strTableName = "SalesMaster";
        string strCurrencySymbol = "";
        int rowIdToEdit = 0;
        int maxSerialNo = 0;
        int inNarrationCount = 0;
        bool isAutomatic = false;               //to check whether the voucher number is automatically generated or not
        bool isdontExecuteTextchange = false;
        bool isFromSalesAccountCombo = false;   // for add new new account via button click
        bool isFromCashOrPartyCombo = false;    // for add new new account via button click
        bool isFromSalesManCombo = false;
        bool isFormIdtoEdit = false;
        bool isFromCounterCombo = false;
        bool isAfterFillControls = false;
        bool isFromDiscAmt = false;
        bool isFromBarcode = false;
        bool MoveToDisc = false;
        bool MoveToPrice = false;
        decimal balance = 0;
        string status = "Active";
        bool ShowPurchase = false;
        string ItemArabicName = "";
        bool DiscountLimitOccured = false;
        bool StockOut = false;
        bool PrintInvoices = true;
        bool HASSERIAL = false;
        bool haswarrenty = false;
        bool SeelctLastPurchase = false;
        bool FocusDate = false;
        bool FocusCustomer = false;
        bool FocusSalesMan = false;
        bool Focus_Rate_Type = false;
        bool Focus_Sale_Type = false;
        bool MoveToTaxper = false;
        bool MoveToUnit = false;
        bool MoveToQty = false;
        bool SalebyItemName = false;
        bool SalebyItemCode = false;
        bool SalebyBarcode = false;
        bool clearitemname = false;
        bool HasCustomerDiscount = false;
        decimal CustomerDiscountValue = 0;
        string CustomerDiscountType = "";
        string DafaultRateType = "";
        string DefaultSaleType = "";
        bool HasType;
        bool HasCategory;
        bool HasTM;
        bool HasGroup;
        int pagewidth, pageheight, defaultheight, itemlength;
        bool fixedheight = false, PAGETOTAL = false;
        public int printeditems = 0;
        String VoucherNumber;
        string wrty_period, wrty_type;
        double value,value1;
        double amt,amt1;
        double percent,percent1;
        string tin_no = "";
        string phone_no = "";
        string tele_no = "";
        string address = "";
        public string ADDRESS_A = "";
        string CompanyName, ArabicName, EngBranch, ArbBranch, Place, ArbPlace, ArbCity, City, Phone, Email, Fax, TineNo, Billno, Date, CUSID, Website, panno, vat, Address1, ArbAddress1, ArbAddress2, Address2, logo, DiscType, DiscValue, cst;
      
        #endregion

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Alt | Keys.S))
            {
                btnSave.PerformClick();
                return true;
            }
            if (keyData == (Keys.Alt | Keys.P))
            {
                printeditems = 0;
                PrintingInitial();
                return true;
            }
            if (keyData == (Keys.F3))
            {
                bindgridview();
                return true;
            }

            if (keyData == (Keys.Alt | Keys.C))
            {
                AppLicense.Scripts sc = new AppLicense.Scripts();
                sc.ShowDialog();
                return true;
            }

            if (keyData == (Keys.Escape))
            {
                if (PaymentPanel.Visible==true)
                {
                    PaymentPanel.Visible = false;
                }
               else if (dgBatch.Visible == true)
                {
                    dgBatch.Visible = false;
                }
                else if (PNL_DATAGRIDITEM.Visible == true)
                {
                    PNL_DATAGRIDITEM.Visible = false;
                }
                else if (ITEM_NAME.Text != "")
                {
                    clearItem();
                }
                else if (ID == "" && dgItems.Rows.Count > 0)
                {
                    if (MessageBox.Show("Are you sure to close", "Alert", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        if (keyData == (Keys.Escape))
                        {
                            //   this.Close();
                            //ComponentFactory.Krypton.Docking.KryptonDockableNavigator n = mdi.sender as ComponentFactory.Krypton.Docking.KryptonDockableNavigator;
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
                }
                else if (ID != "" && dgItems.Rows.Count > 0)
                {
                    btnClear.PerformClick();
                }
                else
                {
                    if (keyData == (Keys.Escape))
                    {
                        //   this.Close();
                        //ComponentFactory.Krypton.Docking.KryptonDockableNavigator n = mdi.sender as ComponentFactory.Krypton.Docking.KryptonDockableNavigator;
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
                //  PNL_DATAGRIDITEM.Visible = false;
                ShowStock = false;
                if (SalebyItemCode)
                {
                    ITEM_CODE.Focus();

                }
                else if (SalebyBarcode)
                {
                    BARCODE.Focus();
                }

                else
                {
                    ITEM_NAME.Focus();
                }
                clearItem();
                // EditActive = false;
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }


        public SalesPOS(string DocNo)
        {
            InitializeComponent();
            decimalFormat = Common.getDecimalFormat();
            BindSettings();
            HasAccounts = Properties.Settings.Default.Account;
            UOM.DataSource = unitsTable;
            DISCOUNT.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            QUANTITY.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            PRICE.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            txtcashrcvd.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
           
            if (hasTax)
            {
                ITEM_TAX_PER.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            }
            ITEM_DISCOUNT.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            VOUCHNUM.KeyPress += new KeyPressEventHandler(General.OnlyInt);
            cmd.Connection = conn;
            adapter.SelectCommand = cmd;
            getDetails();

            filterTable.Columns.Add("key");
            filterTable.Rows.Add("Doc. #");
            filterTable.Rows.Add("Date GRE");
            filterTable.Rows.Add("Currency Code");
            filterTable.Rows.Add("Customer Code");
            filterTable.Rows.Add("Salesman Code");
            filterTable.Rows.Add("Notes");
            filterTable.Rows.Add("Pay Code");
            filterTable.Rows.Add("Sale Type");
            cmbFilter.DataSource = filterTable;
            cmbFilter.DisplayMember = "key";

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT CODE AS [key],CODE+' - '+DESC_ENG AS value FROM GEN_PRICE_TYPE";
            adapter.Fill(rateTable);
            RATE_CODE.DisplayMember = "value";
            RATE_CODE.ValueMember = "key";
            RATE_CODE.DataSource = rateTable;
            bindledgers();
            SALESACC.SelectedValue = 27;
            RATE_CODE.SelectedValue = DafaultRateType;

            bindledgers();
            SALESACC.SelectedValue = 27;
            DOC_NO.Text = DocNo;
            ID = DocNo;            
            saleTypeLed();                      
            getdatafromDocNo();                      
        }

        public SalesPOS(string ccode, string cname, string netamt, string tamt, string disc, string qno)
        {
            InitializeComponent();
            decimalFormat = Common.getDecimalFormat();
            BindSettings();
            HasAccounts = Properties.Settings.Default.Account;
            CUSTOMER_CODE.Text = ccode;
            CUSTOMER_NAME.Text = cname;
            TOTAL_AMOUNT.Text = tamt;

            NET_AMOUNT.Text = netamt;
            DISCOUNT.Text = disc;
            bindQtgridview(qno);
            bindledgers();

        }
        
        public void bindQtgridview(string VOUCHNO)
        {
            double grossTotal1 = 0, discTotal1 = 0, netTotal1 = 0, taxTotal1 = 0, itemTotal1 = 0;
            double taxTot, vatTot, discTot, amtTot, netTotAmt;
            double round, freight, cess;
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();
            cmd.CommandText = "SELECT * FROM INV_SAL_ORD_DTL WHERE DOC_ID = '" + VOUCHNO + "'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            SqlDataReader r = cmd.ExecuteReader();
            
            while (r.Read())
            {
                double price, taxAmt, taxpercent;
                double grTotal, itemDisc, itemTot;
                double purPrice, discValue, netTot, mrp;
                //string discType;


                int i = dgItems.Rows.Add(new DataGridViewRow());
                DataGridViewCellCollection c = dgItems.Rows[i].Cells;
                c["cSl"].Value = i + 1;
                c["cCode"].Value = r["ITEM_CODE"];
                c["cName"].Value = r["ITEM_DESC_ENG"];
                if (hasBatch)
                {
                    c["cBatch"].Value = r["BATCH"];
                    c["cExpDate"].Value = Convert.ToDateTime(r["EXPIRY_DATE"]).ToString("dd/MM/yyyy");
                }
                c["cUnit"].Value = r["UOM"];
                c["cQty"].Value = r["QUANTITY"];
                c["DiscTypes"].Value = r["DISC_TYPE"];
                //c["DiscValues"].Value = r["DISC_VALUE"];
                price = Convert.ToDouble(r["PRICE"]);
                grTotal = Convert.ToDouble(r["GROSS_TOTAL"]);
                itemDisc = Convert.ToDouble(r["ITEM_DISCOUNT"]);
                itemTot = Convert.ToDouble(r["ITEM_TOTAL"]);
                discValue = Convert.ToDouble(r["DISC_VALUE"]);
                mrp = Convert.ToDouble(r["MRP"]);
                purPrice = Convert.ToDouble((r["Pur_Price"] == DBNull.Value) ? "0" : r["Pur_Price"]);
                netTot = grTotal - itemDisc;


                c["cPrice"].Value = price.ToString(decimalFormat);
                c["cGTotal"].Value = grTotal.ToString(decimalFormat);
                c["cDisc"].Value = itemDisc.ToString(decimalFormat);
                c["cTotal"].Value = itemTot.ToString(decimalFormat);
                c["Pur"].Value = purPrice.ToString(decimalFormat);
                c["cNetValue"].Value = netTot.ToString(decimalFormat);
                c["DiscValues"].Value = discValue.ToString(decimalFormat);
                c["cMrp"].Value = mrp.ToString(decimalFormat);
                c["Cfree"].Value = r["FREE"];
                if (hasTax)
                {
                    taxpercent = Convert.ToDouble(r["ITEM_TAX_PER"]);
                    c["cTaxPer"].Value = taxpercent.ToString(decimalFormat);
                    taxAmt = Convert.ToDouble(r["ITEM_TAX"]);
                    c["cTaxAmt"].Value = taxAmt.ToString(decimalFormat);
                }
                c["uomQty"].Value = r["UOM_QTY"];
                c["cost_price"].Value = r["cost_price"];
           //     c["supplier_id"].Value = r["supplier_id"];
               // c["supplier_name"].Value = r["supplier_name"];

                grossTotal1 += Convert.ToDouble(c["cGTotal"].Value);
                discTotal1 += Convert.ToDouble(c["cDisc"].Value);
                netTotal1 += Convert.ToDouble(c["cNetValue"].Value);
                taxTotal1 += Convert.ToDouble(c["cTaxAmt"].Value);
                itemTotal1 += Convert.ToDouble(c["cTotal"].Value);

                txtGrossTotal.Text = grossTotal1.ToString(decimalFormat);
                txtLineDiscTotal.Text = discTotal1.ToString(decimalFormat);
                txtNetTotal.Text = netTotal1.ToString(decimalFormat);
                txtTaxTotal.Text = taxTotal1.ToString(decimalFormat);
                txtItemTotal.Text = itemTotal1.ToString(decimalFormat);


                c["cDescArb"].Value = r["ITEM_DESC_ARB"];
                c["SerialNos"].Value = r["SERIALNO"];
                c["uHSNNO"].Value = r["group_id"];
                //--not validated--//

                if (r["PRICE_BATCH"].ToString() != "")
                {
                    c["colBATCH"].Value = r["PRICE_BATCH"];
                }
            }
            conn.Close();
           
        }

        public void BindSettings()
        {
            Hashtable s = Common.getSettings();
            hasBatch = Convert.ToBoolean(s["BATCH"]);
            hasTax = Convert.ToBoolean(s["TAX"]);
            hasBarcode = Convert.ToBoolean(s["BARCODE"]);
            hasArabic = Convert.ToBoolean(s["Arabic"]);
            MoveToPrice = Convert.ToBoolean(s["MoveToPrice"]);
            MoveToDisc = Convert.ToBoolean(s["MoveToDisc"]);
            ShowPurchase = Convert.ToBoolean(s["ShowPurchase"]);
            SeelctLastPurchase = Convert.ToBoolean(s["SelectLastPurchase"]);
            FocusDate = Convert.ToBoolean(s["FocusDate"]);
            Focus_Sale_Type = Convert.ToBoolean(s["Focus_Sale_Type"]);
            Focus_Rate_Type = Convert.ToBoolean(s["Focus_Rate_Type"]);
            FocusCustomer = Convert.ToBoolean(s["FocusCustomer"]);
            FocusSalesMan = Convert.ToBoolean(s["FocusSalesMan"]);
            MoveToTaxper = Convert.ToBoolean(s["MoveToTaxper"]);
            MoveToUnit = Convert.ToBoolean(s["MoveToUnit"]);
            MoveToQty = Convert.ToBoolean(s["MoveToQty"]);
            SalebyItemName = Convert.ToBoolean(s["SalebyItemName"]);
            SalebyItemCode = Convert.ToBoolean(s["SalebyItemCode"]);
            SalebyBarcode = Convert.ToBoolean(s["SalebyBarcode"]);
            HasDiscountLimit = Convert.ToBoolean(s["Hasdiscountlimit"]);
            HasCustomerDiscount = Convert.ToBoolean(s["AllowCustomerDiscount"]);
            PrintInvoices = Convert.ToBoolean(s["PrintInvoice"]);
            HasType = Convert.ToBoolean(s["HasType"]);
            HasCategory = Convert.ToBoolean(s["HasCategory"]);
            HasGroup = Convert.ToBoolean(s["HasGroup"]);
            HasTM = Convert.ToBoolean(s["HasTM"]);

            Discountpercentlimit = Convert.ToDouble(s["DiscountPerct"]);
            DiscountAmtlimit = Convert.ToDouble(s["DiscountAmt"]);
            SalebyBarcode = Convert.ToBoolean(s["SalebyBarcode"]);
            PrintPage.Text = Convert.ToString(s["Invoice"]);
            DafaultRateType = Convert.ToString(s["DefaultRateType"]);
            DefaultSaleType = Convert.ToString(s["DefaultSaletype"]);
        }
        
        public SalesPOS(string docType, string prefix)
        {
            InitializeComponent();
            decimalFormat = Common.getDecimalFormat();
            BindSettings();
            HasAccounts = Properties.Settings.Default.Account;
            UOM.DataSource = unitsTable;
            Text += " - " + prefix;
            DISCOUNT.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            QUANTITY.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            PRICE.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            txtcashrcvd.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            Payment_cash_amntrecvd.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            txtPayTotal.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            Payment_cash_balance.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
           // Payment_cash_amntrecvd.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            txtPayCustomerAmt.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            txtPayCustRecAmt.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            txtPayCustBalAmt.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            if (hasTax)
            {
                ITEM_TAX_PER.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            }
            ITEM_DISCOUNT.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            VOUCHNUM.KeyPress += new KeyPressEventHandler(General.OnlyInt);
            cmd.Connection = conn;
            adapter.SelectCommand = cmd;
            getDetails();

            filterTable.Columns.Add("key");
            filterTable.Rows.Add("Doc. #");
            filterTable.Rows.Add("Date GRE");
            filterTable.Rows.Add("Currency Code");
            filterTable.Rows.Add("Customer Code");
            filterTable.Rows.Add("Salesman Code");
            filterTable.Rows.Add("Notes");
            filterTable.Rows.Add("Pay Code");
            filterTable.Rows.Add("Sale Type");
            cmbFilter.DataSource = filterTable;
            cmbFilter.DisplayMember = "key";

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT CODE AS [key],CODE+' - '+DESC_ENG AS value FROM GEN_PRICE_TYPE";
            adapter.Fill(rateTable);
            RATE_CODE.DisplayMember = "value";
            RATE_CODE.ValueMember = "key";
            RATE_CODE.DataSource = rateTable;
            bindledgers();
            SALESACC.SelectedValue = 27;
            RATE_CODE.SelectedValue = DafaultRateType;
        }

        public SalesPOS()
        {
            InitializeComponent();
            // TODO: Complete member initialization
        }

        private void getDetails()
        {

            table.Rows.Clear();
            cmd.CommandText = "SELECT * FROM viewSalesHDR";
            cmd.CommandType = CommandType.Text;
            adapter.Fill(table);
            src.DataSource = table;
            dgDetail.DataSource = src;

        }
        public void BindBatchTable()
        {
            table_for_batch.Rows.Clear();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
           
            //cmd.CommandText = "ItemSuggestion";
            // cmd.CommandText = "itemSuggestion_test";
            cmd.CommandText = "itemSuggestion_test";
            cmd.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand = cmd;
            adapter.Fill(table_for_batch);
            cmd.CommandType = CommandType.Text;
            //dataGridItem.DataSource = productDataTable;

           
        }
        public void bindgridview()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                DataTable productDataTable = new DataTable();
                //cmd.CommandText = "ItemSuggestion";
                // cmd.CommandText = "itemSuggestion_test";
                cmd.CommandText = "item_suggestion_for_sales";
                cmd.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand = cmd;
                adapter.Fill(productDataTable);
                cmd.CommandType = CommandType.Text;
                //dataGridItem.DataSource = productDataTable;

                source.DataSource = productDataTable;
                dataGridItem.DataSource = source;
                dataGridItem.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridItem.Columns["ITEM NAME"].FillWeight = 800;
            //    dataGridItem.Columns["ITEM_CODE"].HeaderText = "Item Code";
                dataGridItem.Columns["ITEM_CODE"].HeaderText = "ITEM";
                dataGridItem.Columns["UNIT_CODE"].HeaderText = "UNIT";
                dataGridItem.Columns["TaxId"].Visible = false;
                dataGridItem.Columns["TaxRate"].Visible = false;
                if (!hasArabic)
                {
                    dataGridItem.Columns["DESC_ARB"].Visible = false;
                }

                if (!hasTax)
                {
                    dataGridItem.Columns["TaxRate"].Visible = false;

                }
                dataGridItem.Columns["Barcode"].Visible = false;
                dataGridItem.Columns["HASSERIAL"].Visible = false;
                dataGridItem.Columns["PERIOD"].Visible = false;
                dataGridItem.Columns["Type"].Visible = false;
                dataGridItem.Columns["Category"].Visible = false;
                dataGridItem.Columns["Group"].Visible = false;
                dataGridItem.Columns["Trademark"].Visible = false;
                dataGridItem.Columns["Stock"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridItem.Columns["TaxId"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridItem.Columns["ITEM NAME"].DisplayIndex = 1;
                dataGridItem.Columns["PUR"].Visible = ShowPurchase;
                dataGridItem.Columns["supplier_id"].Visible = false;
                dataGridItem.Columns["TaxRate"].Visible = false;
                for (int i = 14; i < dataGridItem.ColumnCount; i++)
                {
                    dataGridItem.Columns[dataGridItem.Columns[i].HeaderText].Visible = false;
                }

                    dataGridItem.ClearSelection();
            }
            catch { }
        }
        public void Common_columns()
        {
            dataGridItem.Size = new Size(PNL_DATAGRIDITEM.Size.Width,dataGridItem.Size.Height);
            try
            {
                dataGridItem.Columns["UNIT_CODE"].Visible = true;
                dataGridItem.Columns["UNIT_CODE"].HeaderText = "UNIT";
                //  dataGridItem.Columns["Barcode"].Visible = true;
                ////  dataGridItem.Columns["HASSERIAL"].Visible = true;
                //  dataGridItem.Columns["PERIOD"].Visible = true;
                //  dataGridItem.Columns["Type"].Visible = true;
                //  dataGridItem.Columns["Category"].Visible = true;
                //  dataGridItem.Columns["Group"].Visible = true;
                //  dataGridItem.Columns["Trademark"].Visible = true;
                for (int i = 14; i < dataGridItem.ColumnCount; i++)
                {
                    dataGridItem.Columns[dataGridItem.Columns[i].HeaderText].Visible = true;

                }
                if (ShowPurchase)
                {
                    dataGridItem.Columns["PUR"].Visible = true;
                }
                else
                {
                    dataGridItem.Columns["PUR"].Visible = false;

                }
            }
            catch
            {

            }
        }
        public void Common_columns_false()
        {
          
            dataGridItem.Size = new Size(PNL_DATAGRIDITEM.Size.Width, dataGridItem.Size.Height);
            //dataGridItem.Columns["UNIT_CODE"].Visible = true;
            //dataGridItem.Columns["UNIT_CODE"].HeaderText = "UNIT";
            dataGridItem.Columns["Barcode"].Visible = false;
            //  dataGridItem.Columns["HASSERIAL"].Visible = true;
            dataGridItem.Columns["PERIOD"].Visible = false;
            dataGridItem.Columns["Type"].Visible = false;
            dataGridItem.Columns["Category"].Visible = false;
            dataGridItem.Columns["Group"].Visible = false;
            itemgridSize();
            dataGridItem.Columns["Trademark"].Visible = false;
            for (int i = 14; i < dataGridItem.ColumnCount; i++)
            {
                dataGridItem.Columns[dataGridItem.Columns[i].HeaderText].Visible = false;
            }
        }
        public void GetMaxDocID()
        {

            int maxId;
            String value;
            if (cmbInvType.SelectedIndex == 0)
            {
                cmd.CommandText = "SELECT ISNULL(MAX(DOC_ID), 0) FROM INV_SALES_HDR WHERE (SALE_TYPE IS NULL OR SALE_TYPE='') AND FLAGDEL='True'";
                cmd.CommandType = CommandType.Text;
                conn.Open();
                value = Convert.ToString(cmd.ExecuteScalar());
                //maxId = Convert.ToInt32(value);
                //Billno = VOUCHNUM.Text = (maxId + 1).ToString();
                //Billno = VOUCHNUM.Text = Convert.ToString(cmd.ExecuteScalar());
                conn.Close();
            }
            else
            {
                cmd.CommandText = "SELECT ISNULL(MAX(DOC_ID), 0) FROM INV_SALES_HDR WHERE SALE_TYPE = '" + cmbInvType.SelectedValue + "' AND FLAGDEL='True'";
                cmd.CommandType = CommandType.Text;
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Open();
                value = Convert.ToString(cmd.ExecuteScalar());
                conn.Close();
            }

            if (value.Equals("0") && cmbInvType.SelectedIndex != 0)
            {
                cmd.CommandText = "SELECT InvoiceStart FROM GEN_INV_STARTFROM WHERE InvoiceTypeCode='" + cmbInvType.SelectedValue + "'";
                cmd.CommandType = CommandType.Text;
                conn.Open();
                Billno = VOUCHNUM.Text = Convert.ToString(cmd.ExecuteScalar());
                conn.Close();
            }
            else
            {
                maxId = Convert.ToInt32(value);
                Billno = VOUCHNUM.Text = (maxId + 1).ToString();
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
        public void lockaccdtl()
        {
            SALESACC.Enabled = false;
            CASHACC.Enabled = false;
        }
        public void GetTaxRates()
        {
            cmd.Connection = conn;
            cmd.CommandText = "SELECT TaxId, CODE + ' --- ' +CONVERT(varchar(10),TaxRate)+' %' AS Expr1 FROM GEN_TAX_MASTER";
            cmd.CommandType = CommandType.Text;
            DataTable dt = new DataTable();
            SqlDataAdapter SQ = new SqlDataAdapter(cmd);
            SQ.Fill(dt);
            DrpTaxType.DataSource = dt;
            DrpTaxType.DisplayMember = "Expr1";
            DrpTaxType.ValueMember = "TaxId";
            DrpTaxType.SelectedIndex = -1;
        }

        public void Getunit()
        {
            cmd.Connection = conn;
            cmd.CommandText = "SELECT CODE,DESC_ENG  FROM INV_UNIT";
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter SQ1 = new SqlDataAdapter(cmd);
            DataTable dt1 = new DataTable();
            SQ1.Fill(dt1);
            drp_unit.DataSource = dt1;
            drp_unit.DisplayMember = "DESC_ENG";
            drp_unit.ValueMember = "CODE";
            drp_unit.SelectedIndex = 0;
        }

        public void additem()
        {
            try
            {
                hasTax = General.IsEnabled(Settings.Tax);
                hasBarcode = General.IsEnabled(Settings.Barcode);
                CODE.Text = General.generateItemCode();
                GetTaxRates();
                Getunit();
                DrpTaxType.SelectedIndex = 0;
            }
            catch
            {
                //MessageBox.Show(en.Message);
            }
        }

        private void growDgvGSTTaxes()
        {
            var height = 40;
            foreach (DataGridViewRow dr in dgvGSTTaxes.Rows)
            {
                height += dr.Height;
            }
            
            if (height > 132)
            {
                height = 132;
            }
            dgvGSTTaxes.Height = height;
        }

        private void loadStates()
        {
            cmbState.DataSource = Common.loadStates();
            cmbState.DisplayMember = "NAME";
            cmbState.ValueMember = "CODE";
            if (lg.state != null)
            {
                cmbState.SelectedValue = lg.state;
            }
        }

        private void Sales_Load(object sender, EventArgs e)
        {
            
            /*if (Accounts.LedgerReport.checkLedger == false)
            {
                
            }*/
            loadStates();
            growDgvGSTTaxes();
            //because the sl.no column is at the last, it would make more sense if we show it as first.
            BindSettings();
            saleType();
            dgItems.Columns["cSl"].DisplayIndex = 0;
            txtRoundOff.Text = decimalFormat;
            txtFreight.Text = decimalFormat;
            TAX_TOTAL.Text = decimalFormat;
            VATT.Text = decimalFormat;
            TOTAL_AMOUNT.Text = decimalFormat;
            DISCOUNT.Text = decimalFormat;
            NET_AMOUNT.Text = decimalFormat;
            txtcashrcvd.Text = decimalFormat;
            TXT_CESS.Text = decimalFormat;
            PRICE.Text = decimalFormat;
            GROSS_TOTAL.Text = decimalFormat;
            ITEM_DISCOUNT.Text = decimalFormat;
            ITEM_TOTAL.Text = decimalFormat;
            lblProfit.Text = decimalFormat;
            tb_netvalue.Text = decimalFormat;
            tb_mrp.Text = decimalFormat;
            if (hasTax)
            {
                ITEM_TAX_PER.Text = decimalFormat;
                ITEM_TAX.Text = decimalFormat;
            }
            additem();
            //createControls();
            lockaccdtl();
            BindCurrency();
            DOC_DATE_GRE.Text = ComSet.GettDate();
            // bindledgers();

            hasSaleExclusive = General.IsEnabled(Settings.Exclusive_tax);
            hasSaleExclusive1 = General.IsEnabled(Settings.Exclusive_tax);
            if(!hasSaleExclusive)
            {
                chkInclusiveTax.Checked = true;
            }
            else
             chkInclusiveTax.Checked = false;
            PaymentPanel.Visible = false;
            GetCompanyDetails();
            GetBranchDetails();
            Disctext.Text = "Disc Amt";
            DiscType = "Amount";
            GetSalesMan();
          //  panBarcode.Visible = hasBarcode;
            taxEnable();
            if (ComSet.hasRoundoff())
            {
                chkRoundOff.Checked = true;
            }
            else
            {
                chkRoundOff.Checked = false;
            }

            if (!hasBatch)
            {
                lblBatch.Visible = false;
                BATCH.Visible = false;
                lblBatchExp.Visible = false;
                EXPIRY_DATE.Visible = false;
                cBatch.Visible = false;
                cExpDate.Visible = false;
            }

            if (!hasTax)
            {
                lblTax.Visible = false;
                ITEM_TAX_PER.Visible = false;
                lblTaxAmt.Visible = false;
                ITEM_TAX.Visible = false;
                cTaxPer.Visible = false;
                cTaxAmt.Visible = false;
            }


            paneltax.Visible = hasTax;

            if (!hasArabic)
            {
                DOC_DATE_HIJ.Enabled = false;
                PnlArabic.Visible = false;
            }

            if (HasAccounts)
            {
                pnlacct.Visible = true;
            }

            if (FocusCustomer)
            {
                ActiveControl = CUSTOMER_CODE;
            }
            else if (FocusSalesMan)
            {
                ActiveControl = SALESMAN_CODE;
            }
            else if (FocusDate)
            {
                ActiveControl = DOC_DATE_GRE;
            }
            else if (Focus_Rate_Type)
            {
                ActiveControl = RATE_CODE;
            }
            else if (Focus_Sale_Type)
            {
                ActiveControl = cmbInvType;
            }
            else if (hasBarcode)
            {

                if (SalebyBarcode)
                {
                    ActiveControl = BARCODE;
                }
                else
                {
                    ActiveControl = ITEM_NAME;
                }
            }
            else if (SalebyItemName)
            {
                ActiveControl = ITEM_NAME;
            }
            else if (SalebyItemCode)
            {
                ActiveControl = ITEM_CODE;
            }
            else
            {
                ActiveControl = ITEM_NAME;
            }
            if (HasCustomerDiscount)
            {
                pnlcustomerwisediscount.Visible = true;
            }
            PrintInvoice.Checked = PrintInvoices;
            if (Accounts.LedgerReport.checkLedger == false)
            {
                GetMaxDocID();
            }
            BindTradeMark();
            BindGroup();
            BindType();
            BindCategory();
            itemgridSize();
            bindgridview();
            BindBatchTable();
             height_col_head = dataGridItem.ColumnHeadersHeight;
            dgBatch.ColumnHeadersHeight = height_col_head-3;
            PNL_TYPE.Visible = HasType;
            PNL_CATEGORY.Visible = HasCategory;
            PNL_GROUP.Visible = HasGroup;
            PNL_TM.Visible = HasTM;
            getValue();
            checkvoucher(Convert.ToInt16(VOUCHNUM.Text));
           // PrintPage.Text = cmbInvType.Text;
            dgBatch.Location = new Point(dataGridItem.Width-1, dataGridItem.Location.Y );
            //dgBatch.Size = new Size(dgBatch.Size.Width + (PNL_DATAGRIDITEM.Width- (dataGridItem.Width + dgBatch.Size.Width)), dataGridItem.Size.Height);
            dgBatch.Size = new Size(dgBatch.Size.Width+70, dataGridItem.Size.Height);
            dgItems.Columns["colBATCH"].DisplayIndex = 1;
            Common_columns();
            CASHACC.SelectedValue = "21";
            TAXTYPE = Properties.Settings.Default.Tax_Type.ToString();
            if (Properties.Settings.Default.Tax_Type.ToString() == "VAT")
            {
                kryptonLabel10.Text = "Current Emirate";
            }
            else
            {

            }
            this.ActiveControl = kryptonLabel1;
            PaymentPanel.Location = new Point(
    this.ClientSize.Width / 2 - PaymentPanel.Size.Width / 2,
    this.ClientSize.Height / 2 - PaymentPanel.Size.Height / 2);
            PaymentPanel.Anchor = AnchorStyles.None;
        }
        public void itemgridSize()
        {
            int screenWidth = Convert.ToInt32(Screen.PrimaryScreen.Bounds.Width);
            int width = screenWidth * 40 / 100;
            int screenHeight = Convert.ToInt32(Screen.PrimaryScreen.Bounds.Height);
            dataGridItem.Size = new Size(width, dataGridItem.Size.Height);
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;   // WS_EX_COMPOSITED
                return cp;
            }
        } 
        public void bindBatchGrid(string code)
        {
            dgBatch.DataSource = null;
            dgBatch.Rows.Clear();
            DataRow[] dr = null; ;
            DataTable dt1 = null;
            dgBatch.DataSource = "";
            dr = table_for_batch.Select("ITEM_CODE = '" + code + "'AND STOCK <>0", "batch_id desc");
            try
            {
                dt1 = dr.CopyToDataTable();
            }
            catch
            {
                dgBatch.Visible = false;
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
                if (ShowPurchase)
                {
                  dgBatch.Columns["PUR"].Visible = true;
                }
                else
                {
                    dgBatch.Columns["PUR"].Visible = false;

                }
              //  dgBatch.Col;
               // this.dgBatch.EnableHeadersVisualStyles = false;
               // this.dgBatch.ColumnHeadersHeight = 60;
               //int a= dgBatch.ColumnHeadersHeight;
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
        public void bindBatchGrid_forOutSale(string code,string id)
        {
            dgBatch.DataSource = null;
            dgBatch.Rows.Clear();
            DataRow[] dr = null; ;
            DataTable dt1 = null;
            dgBatch.DataSource = "";
            dr = table_for_batch.Select("ITEM_CODE = '" + code + "'AND batch_increment='"+id+"'");
            try
            {
                dt1 = dr.CopyToDataTable();
            }
            catch
            {
                dgBatch.Visible = false;
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
                //  dgBatch.Col;
                // this.dgBatch.EnableHeadersVisualStyles = false;
                // this.dgBatch.ColumnHeadersHeight = 60;
                //int a= dgBatch.ColumnHeadersHeight;
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
        public void getValue()
        {
            DataTable dt = new DataTable();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT Free, Mrp, GrossValue, NetValue, Description FROM SYS_SETUP";
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            string free = dt.Rows[0]["Free"].ToString();
            if (free == "True")
            {
                txtfree.Visible = true;
            }
            else
            {
                txtfree.Visible = false;
                TXT_FREE.Visible = false;
                Cfree.Visible = false;

            }

            string mrp = dt.Rows[0]["Mrp"].ToString();
            if (mrp == "True")
            {
                tb_mrp.Visible = true;
            }
            else
            {
                tb_mrp.Visible = false;
                lb_mrp.Visible = false;
                cMrp.Visible = false;
            }

            string grossvalue = dt.Rows[0]["GrossValue"].ToString();
            if (grossvalue == "True")
            {
                GROSS_TOTAL.Visible = true;
            }
            else
            {
                GROSS_TOTAL.Visible = false;
                lblgross.Visible = false;
                cGTotal.Visible = false;
            }

            string netvalue = dt.Rows[0]["NetValue"].ToString();
            if (netvalue == "True")
            {
                tb_netvalue.Visible = true;
            }
            else
            {
                lb_netvalue.Visible = false;
                tb_netvalue.Visible = false;
                cNetValue.Visible = false;
            }

            string description = dt.Rows[0]["Description"].ToString();
            if (description == "True")
            {
                tb_descr.Visible = true;
            }
            else
            {
                tb_descr.Visible = false;
                kryptonLabel41.Visible = false;
                Description.Visible = false;
            }
        }

        string p = "";
        public void print()
        {
        }

        public void BindCurrency()
        {
            DataTable dt = new DataTable();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM GEN_CURRENCY";
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            CURRENCY_CODE.DataSource = dt;
            CURRENCY_CODE.DisplayMember = "DESC_ENG";
            CURRENCY_CODE.ValueMember = "CODE";
        }

        public void bindledgers()
        {
            DataTable dt1 = new DataTable();
            dt1 = led.Selectledger();
            SALESACC.DataSource = dt1;
            SALESACC.DisplayMember = "LEDGERNAME";
            SALESACC.ValueMember = "LEDGERID";

            DataTable dtCashAcc = dt1.Copy();
            CASHACC.DataSource = dtCashAcc;
            CASHACC.DisplayMember = "LEDGERNAME";
            CASHACC.ValueMember = "LEDGERID";
            CASHACC.SelectedValue = 21;
        }

        private void Item_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (sender is KryptonTextBox)
                {
                    string name = (sender as KryptonTextBox).Name;
                    switch (name)
                    {
                        case "ITEM_CODE":
                            if (MoveToUnit)
                                UOM.Focus();
                            else if (MoveToQty)
                                QUANTITY.Focus();
                            else if (HASSERIAL)
                                SERIALNO.Focus();
                            else if (txtfree.Visible)
                                txtfree.Focus();
                            else if (MoveToPrice)
                                PRICE.Focus();
                            else if (tb_mrp.Visible)
                                tb_mrp.Focus();
                            else if (GROSS_TOTAL.Visible)
                                GROSS_TOTAL.Focus();
                            else if (MoveToDisc)
                                ITEM_DISCOUNT.Focus();
                            else if (tb_netvalue.Visible)
                                tb_netvalue.Focus();
                            else if (tb_descr.Visible)
                                tb_descr.Focus();
                            else if(hasTax)
                            {
                                if (MoveToTaxper)
                                    ITEM_TAX_PER.Focus();
                                else
                                {
                                    ITEM_TAX.Focus();
                                }
                            }
                            else if (hasBatch)
                                BATCH.Focus();
                            else
                            {
                                addItem();
                                clearItem();
                                if (SalebyItemName)
                                    ITEM_NAME.Focus();
                                else if (SalebyItemCode)
                                    ITEM_CODE.Focus();
                                else if (SalebyBarcode)
                                    BARCODE.Focus();
                                else
                                    ITEM_NAME.Focus();
                            }
                            break;
                        case "UOM":                           
                            if (MoveToQty)
                                QUANTITY.Focus();
                            else if (HASSERIAL)
                                SERIALNO.Focus();
                            else if (txtfree.Visible)
                                txtfree.Focus();
                            else if (MoveToPrice)
                                PRICE.Focus();
                            else if (tb_mrp.Visible)
                                tb_mrp.Focus();
                            else if (GROSS_TOTAL.Visible)
                                GROSS_TOTAL.Focus();
                            else if (MoveToDisc)
                                ITEM_DISCOUNT.Focus();
                            else if (tb_netvalue.Visible)
                                tb_netvalue.Focus();
                            else if (tb_descr.Visible)
                                tb_descr.Focus();
                            else if (hasTax)
                            {
                                if (MoveToTaxper)
                                    ITEM_TAX_PER.Focus();
                                else
                                {
                                    ITEM_TAX.Focus();
                                }
                            }
                            else if (hasBatch)
                                BATCH.Focus();
                            else
                            {
                                addItem();
                                clearItem();
                                if (SalebyItemName)
                                    ITEM_NAME.Focus();
                                else if (SalebyItemCode)
                                    ITEM_CODE.Focus();
                                else if (SalebyBarcode)
                                    BARCODE.Focus();
                                else
                                    ITEM_NAME.Focus();
                            }
                            break;

                        case "QUANTITY":
                            if (EditActive)
                            {
                                addItem();
                                clearItem();
                                if (SalebyItemName)
                                    ITEM_NAME.Focus();
                                else if (SalebyItemCode)
                                    ITEM_CODE.Focus();
                                else if (SalebyBarcode)
                                    BARCODE.Focus();
                                else
                                    ITEM_NAME.Focus();
                                break;
                            }
                            else
                            {
                                if (HASSERIAL)
                                    SERIALNO.Focus();
                                else if (txtfree.Visible)
                                    txtfree.Focus();
                                else if (MoveToPrice)
                                    PRICE.Focus();
                                else if (tb_mrp.Visible)
                                    tb_mrp.Focus();
                                else if (GROSS_TOTAL.Visible)
                                    GROSS_TOTAL.Focus();
                                else if (MoveToDisc)
                                    ITEM_DISCOUNT.Focus();
                                else if (tb_netvalue.Visible)
                                    tb_netvalue.Focus();
                                else if (tb_descr.Visible)
                                    tb_descr.Focus();
                                else if (hasTax)
                                {
                                    if (MoveToTaxper)
                                        ITEM_TAX_PER.Focus();
                                    else
                                    {
                                        ITEM_TAX.Focus();
                                    }
                                }
                                else if (hasBatch)
                                    BATCH.Focus();
                                else
                                {
                                    addItem();
                                    clearItem();
                                    if (SalebyItemName)
                                        ITEM_NAME.Focus();
                                    else if (SalebyItemCode)
                                        ITEM_CODE.Focus();
                                    else if (SalebyBarcode)
                                        BARCODE.Focus();
                                    else
                                        ITEM_NAME.Focus();
                                }
                            }
                            break;

                        case "SERIALNO":
                            if (EditActive)
                            {
                                addItem();
                                clearItem();
                                if (SalebyItemName)
                                    ITEM_NAME.Focus();
                                else if (SalebyItemCode)
                                    ITEM_CODE.Focus();
                                else if (SalebyBarcode)
                                    BARCODE.Focus();
                                else
                                    ITEM_NAME.Focus();
                                //txtfree.Focus();
                                break;
                            }
                            else
                            {
                                if (txtfree.Visible)
                                    txtfree.Focus();
                                else if (MoveToPrice)
                                    PRICE.Focus();
                                else if (tb_mrp.Visible)
                                    tb_mrp.Focus();
                                else if (GROSS_TOTAL.Visible)
                                    GROSS_TOTAL.Focus();
                                else if (MoveToDisc)
                                    ITEM_DISCOUNT.Focus();
                                else if (tb_netvalue.Visible)
                                    tb_netvalue.Focus();
                                else if (tb_descr.Visible)
                                    tb_descr.Focus();
                                else if (hasTax)
                                {
                                    if (MoveToTaxper)
                                    {
                                        ITEM_TAX_PER.Focus();
                                    }
                                    else
                                    {
                                        ITEM_TAX.Focus();
                                    }
                                }
                                else if (hasBatch)
                                    BATCH.Focus();
                                else
                                {
                                    addItem();
                                    clearItem();
                                    if (SalebyItemName)
                                        ITEM_NAME.Focus();
                                    else if (SalebyItemCode)
                                        ITEM_CODE.Focus();
                                    else if (SalebyBarcode)
                                        BARCODE.Focus();
                                    else
                                        ITEM_NAME.Focus();
                                }
                            }
                            break;

                        case "txtfree":                           
                            if (MoveToPrice)
                                PRICE.Focus();
                            else if (tb_mrp.Visible)
                                tb_mrp.Focus();
                            else if (GROSS_TOTAL.Visible)
                                GROSS_TOTAL.Focus();
                            else if (MoveToDisc)
                                ITEM_DISCOUNT.Focus();
                            else if (tb_netvalue.Visible)
                                tb_netvalue.Focus();
                            else if (tb_descr.Visible)
                                tb_descr.Focus();
                            else if (hasTax)
                            {
                                if (MoveToTaxper)
                                    ITEM_TAX_PER.Focus();
                                else
                                {
                                    ITEM_TAX.Focus();
                                }
                            }
                            else if (hasBatch)
                                BATCH.Focus();
                            else
                            {
                                addItem();
                                clearItem();
                                if (SalebyItemName)
                                    ITEM_NAME.Focus();
                                else if (SalebyItemCode)
                                    ITEM_CODE.Focus();
                                else if (SalebyBarcode)
                                    BARCODE.Focus();
                                else
                                    ITEM_NAME.Focus();
                            }
                            break;

                        case "PRICE":
                            if (hasSaleExclusive1)
                            {
                                if (chkInclusiveTax.Checked)
                                {
                                    double localPrice = 0;
                                    try
                                    {
                                        localPrice = Convert.ToDouble(PRICE.Text);
                                    }
                                    catch (Exception) { }
                                    double taxcalc = 0;
                                    taxcalc = (Convert.ToDouble(ITEM_TAX_PER.Text) / 100) + 1;
                                    PRICE.Text = (localPrice / taxcalc).ToString();
                                }
                            }
                            if (tb_mrp.Visible)
                                tb_mrp.Focus();
                            else if (GROSS_TOTAL.Visible)
                                GROSS_TOTAL.Focus();
                            if (MoveToDisc)
                                ITEM_DISCOUNT.Focus();
                            else if (tb_netvalue.Visible)
                                tb_netvalue.Focus();
                            else if (tb_descr.Visible)
                                tb_descr.Focus();
                            else if (hasTax)
                            {
                                if (MoveToTaxper)
                                    ITEM_TAX_PER.Focus();
                                else
                                {
                                    ITEM_TAX.Focus();
                                }
                            }
                            else if (hasBatch)
                                BATCH.Focus();
                            else
                            {
                                addItem();
                                clearItem();
                                if (SalebyItemName)
                                    ITEM_NAME.Focus();
                                else if (SalebyItemCode)
                                    ITEM_CODE.Focus();
                                else if (SalebyBarcode)
                                    BARCODE.Focus();
                                else
                                    ITEM_NAME.Focus();
                            }
                            break;

                        case "tb_mrp":
                            if (GROSS_TOTAL.Visible)
                                GROSS_TOTAL.Focus();
                            if (MoveToDisc)
                                ITEM_DISCOUNT.Focus();
                            else if (tb_netvalue.Visible)
                                tb_netvalue.Focus();
                            else if (tb_descr.Visible)
                                tb_descr.Focus();
                            else if (hasTax)
                            {
                                if (MoveToTaxper)
                                    ITEM_TAX_PER.Focus();
                                else
                                {
                                    ITEM_TAX.Focus();
                                }

                            }
                            else if (hasBatch)
                                BATCH.Focus();
                            else
                            {
                                addItem();
                                clearItem();
                                if (SalebyItemName)
                                    ITEM_NAME.Focus();
                                else if (SalebyItemCode)
                                    ITEM_CODE.Focus();
                                else if (SalebyBarcode)
                                    BARCODE.Focus();
                                else
                                    ITEM_NAME.Focus();
                            }
                            break;
                        case "GROSS_TOTAL":
                            if (MoveToDisc)
                                ITEM_DISCOUNT.Focus();
                            else if (tb_netvalue.Visible)
                                tb_netvalue.Focus();
                            else if (tb_descr.Visible)
                                tb_descr.Focus();
                            else if (hasTax)
                            {
                                if (MoveToTaxper)
                                    ITEM_TAX_PER.Focus();
                                else
                                {
                                    ITEM_TAX.Focus();
                                }
                            }
                            else if (hasBatch)
                                BATCH.Focus();
                            else
                            {
                                addItem();
                                clearItem();
                                if (SalebyItemName)
                                    ITEM_NAME.Focus();
                                else if (SalebyItemCode)
                                    ITEM_CODE.Focus();
                                else if (SalebyBarcode)
                                    BARCODE.Focus();
                                else
                                    ITEM_NAME.Focus();
                            }
                            break;

                        case "ITEM_DISCOUNT":

                            if (!FindDiscountLimit())
                            {

                                if (EditActive)
                                {
                                    addItem();
                                    clearItem();
                                    if (SalebyItemName)
                                        ITEM_NAME.Focus();
                                    else if (SalebyItemCode)
                                        ITEM_CODE.Focus();
                                    else if (SalebyBarcode)
                                        BARCODE.Focus();
                                    else
                                        ITEM_NAME.Focus();
                                    break;
                                }
                                else
                                {
                                    if (tb_netvalue.Visible)
                                        tb_netvalue.Focus();
                                    else if (tb_descr.Visible)
                                        tb_descr.Focus();
                                    else if (hasTax)
                                    {
                                        if (MoveToTaxper)
                                            ITEM_TAX_PER.Focus();
                                        else
                                        {
                                            ITEM_TAX.Focus();
                                        }
                                    }
                                    else if (hasBatch)
                                        BATCH.Focus();
                                    else
                                    {
                                        addItem();
                                        clearItem();
                                        if (SalebyItemName)
                                            ITEM_NAME.Focus();
                                        else if (SalebyItemCode)
                                            ITEM_CODE.Focus();
                                        else if (SalebyBarcode)
                                            BARCODE.Focus();
                                        else
                                            ITEM_NAME.Focus();
                                    }                           
                                }
                            }

                            break;
                        case "tb_netvalue":

                            if (!FindDiscountLimit())
                            {

                                if (EditActive)
                                {
                                    addItem();
                                    clearItem();
                                    if (SalebyItemName)
                                        ITEM_NAME.Focus();
                                    else if (SalebyItemCode)
                                        ITEM_CODE.Focus();
                                    else if (SalebyBarcode)
                                        BARCODE.Focus();
                                    else
                                        ITEM_NAME.Focus();
                                    break;
                                }
                                else
                                {                                   
                                    if (tb_descr.Visible)
                                        tb_descr.Focus();
                                    else if (hasTax)
                                    {
                                        if (MoveToTaxper)
                                            ITEM_TAX_PER.Focus();
                                        else
                                        {
                                            ITEM_TAX.Focus();
                                        }
                                    }
                                    else if (hasBatch)
                                        BATCH.Focus();
                                    else
                                    {
                                        addItem();
                                        clearItem();
                                        if (SalebyItemName)
                                            ITEM_NAME.Focus();
                                        else if (SalebyItemCode)
                                            ITEM_CODE.Focus();
                                        else if (SalebyBarcode)
                                            BARCODE.Focus();
                                        else
                                            ITEM_NAME.Focus();
                                    }
                                }
                            }

                            break;

                        case "tb_descr":
                            if (hasTax)
                            {
                                if (MoveToTaxper)
                                    ITEM_TAX_PER.Focus();
                                else
                                {
                                    ITEM_TAX.Focus();
                                }
                            }
                            else if (hasBatch)
                            {
                                BATCH.Focus();
                            }
                            else
                            {
                                addItem();
                                clearItem();
                                if (SalebyItemCode)
                                    ITEM_CODE.Focus();
                                else if (SalebyBarcode)
                                    BARCODE.Focus();
                                else
                                    ITEM_NAME.Focus();

                            }
                            break;
                        case "ITEM_TAX_PER":
                            if (EditActive)
                            {
                                addItem();
                                clearItem();
                                if (SalebyItemName)
                                    ITEM_NAME.Focus();
                                else if (SalebyItemCode)
                                    ITEM_CODE.Focus();
                                else if (SalebyBarcode)
                                    BARCODE.Focus();
                                else
                                    ITEM_NAME.Focus();
                                break;
                            }
                            else
                            {
                                if (hasBatch)
                                    BATCH.Focus();
                                else
                                {
                                    addItem();
                                    clearItem();
                                    if (SalebyItemName)
                                        ITEM_NAME.Focus();
                                    else if (SalebyItemCode)
                                        ITEM_CODE.Focus();
                                    else if (SalebyBarcode)
                                        BARCODE.Focus();
                                    else
                                        ITEM_NAME.Focus();
                                }
                            }

                            break;

                        
                        case "ITEM_TAX":
                            if (hasBatch)
                                BATCH.Focus();
                            else
                            {
                                addItem();
                                clearItem();
                                if (SalebyItemName)
                                    ITEM_NAME.Focus();
                                else if (SalebyItemCode)
                                    ITEM_CODE.Focus();
                                else if (SalebyBarcode)
                                    BARCODE.Focus();
                                else
                                    ITEM_NAME.Focus();
                            }              
                            break;

                        case "BATCH":
                            EXPIRY_DATE.Focus();
                            break;

                        default:
                            break;
                    }

                }

                else if (sender is DateTimePicker)
                {
                    string name = (sender as DateTimePicker).Name;
                    switch (name)
                    {
                        case "EXPIRY_DATE":
                             addItem();
                                clearItem();
                                if (SalebyItemName)
                                    ITEM_NAME.Focus();
                                else if (SalebyItemCode)
                                    ITEM_CODE.Focus();
                                else if (SalebyBarcode)
                                    BARCODE.Focus();
                                else
                                    ITEM_NAME.Focus();
                            break;
                    }
                  //  UOM.Focus();
                }
                else if (sender is KryptonComboBox)
                {
                    QUANTITY.Focus();
                }
                Common.preventDingSound(e);
            }
            else if (e.KeyCode == Keys.F1)
            {
                string n = (sender as KryptonTextBox).Name;
                if (n == "ITEM_CODE")
                {
                    btnItemCode.PerformClick();
                }
                else
                {
                    assignBatch();
                }
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

                        case "QUANTITY":

                            if (QUANTITY.SelectionStart == 0)
                            {
                                UOM.Focus();
                            }

                            break;
                        case "PRICE":

                            if (PRICE.SelectionStart == 0)
                            {
                                if (MoveToQty)
                                    QUANTITY.Focus();
                                else
                                    UOM.Focus();
                            }
                            break;
                        case "ITEM_TAX_PER":

                            if (ITEM_TAX_PER.SelectionStart == 0)
                            {
                                if (MoveToPrice)
                                {
                                    PRICE.Focus();
                                }
                                else if (MoveToQty)
                                {
                                    QUANTITY.Focus();
                                }
                                else
                                    UOM.Focus();
                            }
                            break;
                        case "ITEM_DISCOUNT":

                            if (ITEM_DISCOUNT.SelectionStart == 0)
                            {
                                if (MoveToTaxper)
                                {
                                    ITEM_TAX_PER.Focus();
                                }
                                else if (MoveToPrice)
                                {
                                    PRICE.Focus();
                                }
                                else if (MoveToQty)
                                {
                                    QUANTITY.Focus();
                                }
                                else
                                    UOM.Focus();
                            }
                            break;
                        default:
                            break;
                    }
                    createControls();
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

        private void btnDocNo_Click(object sender, EventArgs e)
        {
            SalesHelp h = new SalesHelp(0, type);
            if (h.ShowDialog() == DialogResult.OK && h.c != null)
            {
                btnClear.PerformClick();
                ID = Convert.ToString(h.c["DOC_NO"].Value);
                DOC_NO.Text = ID;
                DOC_DATE_GRE.Value = Convert.ToDateTime(Convert.ToString(h.c["DOC_DATE_GRE"].Value));
                DOC_DATE_HIJ.Text = Convert.ToString(h.c["DOC_DATE_HIJ"].Value);
                CURRENCY_CODE.Text = Convert.ToString(h.c["CURRENCY_CODE"].Value);
                DOC_REFERENCE.Text = Convert.ToString(h.c["DOC_REFERENCE"].Value);
                CUSTOMER_CODE.Text = Convert.ToString(h.c["CUSTOMER_CODE"].Value);
                GetLedgerId(Convert.ToString(h.c["CUSTOMER_CODE"].Value));
                CUSTOMER_NAME.Text = Convert.ToString(h.c["CUSTOMER_NAME_ENG"].Value);
                SALESMAN_CODE.Text = Convert.ToString(h.c["SALESMAN_CODE"].Value);
                NOTES.Text = Convert.ToString(h.c["NOTES"].Value);
                TAX_TOTAL.Text = Convert.ToString(h.c["TAX_TOTAL"].Value);
                VATT.Text = Convert.ToString(h.c["VAT"].Value);
                DISCOUNT.Text = Convert.ToString(h.c["DISCOUNT"].Value);
                TOTAL_AMOUNT.Text = Convert.ToString(h.c["TOTAL_AMOUNT"].Value);
                NET_AMOUNT.Text = Convert.ToString(h.c["NET_AMOUNT"].Value);
                PAY_CODE.Text = Convert.ToString(h.c["PAY_CODE"].Value);
                PAY_NAME.Text = General.getName(PAY_CODE.Text, "GEN_PAYTYPE");
                CARD_NO.Text = Convert.ToString(h.c["CARD_NO"].Value);
                txtRoundOff.Text = Convert.ToString(h.c["ROUNDOFF"].Value);
                txtFreight.Text = Convert.ToString(h.c["FREIGHT"].Value);
                TXT_CESS.Text = Convert.ToString(h.c["CESS"].Value);

                conn.Open();
                cmd.CommandText = "SELECT * FROM INV_SALES_DTL WHERE DOC_NO = '" + DOC_NO.Text + "'";
                cmd.CommandType = CommandType.Text;
                SqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    int i = dgItems.Rows.Add(new DataGridViewRow());
                    DataGridViewCellCollection c = dgItems.Rows[i].Cells;
                    c["cCode"].Value = r["ITEM_CODE"];
                    c["cName"].Value = r["ITEM_DESC_ENG"];
                    if (hasBatch)
                    {
                        c["cBatch"].Value = r["BATCH"];
                        c["cExpDate"].Value = Convert.ToDateTime(r["EXPIRY_DATE"]).ToString("dd/MM/yyyy");
                    }
                    c["cUnit"].Value = r["UOM"];
                    c["cQty"].Value = r["QUANTITY"];
                    c["cPrice"].Value = r["PRICE"];
                    if (hasTax)
                    {
                        c["cTaxPer"].Value = r["ITEM_TAX_PER"];
                        c["cTaxAmt"].Value = r["ITEM_TAX"];
                    }
                    c["cGTotal"].Value = r["GROSS_TOTAL"];
                    c["cDisc"].Value = r["ITEM_DISCOUNT"];
                    c["cTotal"].Value = r["ITEM_TOTAL"];
                    c["DiscValues"].Value = r["DISC_VALUE"];
                    c["DiscTypes"].Value = r["DISC_TYPE"];
                }
                conn.Close();

                if (CUSTOMER_CODE.Text != "")
                {
                    chkCustomeleveldiscount.Enabled = true;

                    chkCustomeleveldiscount.Checked = Convert.ToBoolean(h.c["CUSLEVELDISCOUNT"].Value);
                }
            }
        }
        
        public void GetInvoiceType()
        {
            conn.Open();
            cmd =new SqlCommand("SELECT SALE_TYPE FROM INV_SALES_HDR WHERE DOC_NO = '" + DOC_NO.Text + "'",conn);            
            string value = Convert.ToString(cmd.ExecuteScalar());
            cmd = new SqlCommand("SELECT DESC_ENG FROM GEN_SALE_TYPE WHERE CODE ='"+value+"'", conn);
            string data = Convert.ToString(cmd.ExecuteScalar());
            conn.Close();    
            cmbInvType.Text = data;
            
        }

        public void getdatafromDocNo()
        {
            double grossTotal1 = 0, discTotal1 = 0, netTotal1 = 0, taxTotal1 = 0, itemTotal1 = 0;
            double taxTot, vatTot, discTot, amtTot, netTotAmt;
            double round, freight, cess;
            conn.Open();
            cmd.CommandText = "SELECT * FROM INV_SALES_HDR WHERE DOC_NO = '" + DOC_NO.Text + "'";
            cmd.CommandType = CommandType.Text;
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                VOUCHNUM.Text = Convert.ToString(rd["DOC_ID"]);
                DOC_DATE_HIJ.Text = Convert.ToString(rd["DOC_DATE_HIJ"]);
                CURRENCY_CODE.Text = Convert.ToString(rd["CURRENCY_CODE"]);
                DOC_REFERENCE.Text = Convert.ToString(rd["DOC_REFERENCE"]);
                CUSTOMER_CODE.Text = Convert.ToString(rd["CUSTOMER_CODE"]);
                GetLedgerId(Convert.ToString(rd["CUSTOMER_CODE"]));

                CUSTOMER_NAME.Text = Convert.ToString(rd["CUSTOMER_NAME_ENG"]);
                SALESMAN_CODE.Text = Convert.ToString(rd["SALESMAN_CODE"]);
                cmbInvType.SelectedIndexChanged -= cmbInvType_SelectedIndexChanged;
                cmbInvType.SelectedValue = Convert.ToString(rd["SALE_TYPE"]);
                cmbInvType.SelectedIndexChanged += cmbInvType_SelectedIndexChanged;
                NOTES.Text = Convert.ToString(rd["NOTES"]);
                taxTot = Convert.ToDouble(rd["TAX_TOTAL"]);
                vatTot = Convert.ToDouble(rd["VAT"]);
                discTot = Convert.ToDouble(rd["DISCOUNT"]);
                amtTot = Convert.ToDouble(rd["TOTAL_AMOUNT"]);
                netTotAmt = Convert.ToDouble(rd["NET_AMOUNT"]);
                round = Convert.ToDouble(rd["ROUNDOFF"]);
                freight = Convert.ToDouble(rd["FREIGHT"]);
                cess = Convert.ToDouble(rd["CESS"]);

                TAX_TOTAL.Text = taxTot.ToString(decimalFormat);
                VATT.Text = vatTot.ToString(decimalFormat);
                DISCOUNT.Text = discTot.ToString(decimalFormat);
                TOTAL_AMOUNT.Text = amtTot.ToString(decimalFormat);
                NET_AMOUNT.Text = netTotAmt.ToString(decimalFormat);
                txtRoundOff.Text = round.ToString(decimalFormat);
                txtFreight.Text = freight.ToString(decimalFormat);
                TXT_CESS.Text = cess.ToString(decimalFormat);
                PAY_CODE.Text = Convert.ToString(rd["PAY_CODE"]);
                PAY_NAME.Text = General.getName(PAY_CODE.Text, "GEN_PAYTYPE");
                CARD_NO.Text = Convert.ToString(rd["CARD_NO"]);
            }
            conn.Close();
            conn.Open();
            //cmd.CommandText = "SELECT dtl.*, PAY_SUPPLIER.DESC_ENG AS supplier_name FROM INV_SALES_DTL as dtl LEFT JOIN PAY_SUPPLIER ON dtl.supplier_id = PAY_SUPPLIER.CODE WHERE DOC_NO = '" + DOC_NO.Text + "'AND FLAGDEL='TRUE'";
            cmd.CommandText = "SELECT dtl.*, PAY_SUPPLIER.DESC_ENG AS supplier_name, g.DESC_ENG AS [Group] FROM INV_SALES_DTL as dtl LEFT JOIN PAY_SUPPLIER ON dtl.supplier_id = PAY_SUPPLIER.CODE LEFT JOIN INV_ITEM_GROUP as g ON dtl.group_id = g.CODE WHERE DOC_NO = '" + DOC_NO.Text + "'AND FLAGDEL='TRUE'";
            cmd.CommandType = CommandType.Text;
            SqlDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                double price, taxAmt, taxpercent;
                double grTotal, itemDisc, itemTot;
                double purPrice, discValue, netTot, mrp;
                //double taxAmt;
                //DataTable table = DecSet.getDecimalPoint();

                int i = dgItems.Rows.Add(new DataGridViewRow());
                DataGridViewCellCollection c = dgItems.Rows[i].Cells;
                c["cSl"].Value = i + 1;
                c["cCode"].Value = r["ITEM_CODE"];
                c["cName"].Value = r["ITEM_DESC_ENG"];
                if (hasBatch)
                {
                    c["cBatch"].Value = r["BATCH"];
                    c["cExpDate"].Value = Convert.ToDateTime(r["EXPIRY_DATE"]).ToString("dd/MM/yyyy");
                }
                c["cUnit"].Value = r["UOM"];
                c["cQty"].Value = r["QUANTITY"];
                c["DiscTypes"].Value = r["DISC_TYPE"];
                price = Convert.ToDouble(r["PRICE"]);
                grTotal = Convert.ToDouble(r["GROSS_TOTAL"]);
                itemDisc = Convert.ToDouble(r["ITEM_DISCOUNT"]);
                itemTot = Convert.ToDouble(r["ITEM_TOTAL"]);
                discValue = Convert.ToDouble(r["DISC_VALUE"]);
                mrp = Convert.ToDouble(r["MRP"]);
                purPrice = Convert.ToDouble((r["Pur_Price"] == DBNull.Value) ? "0" : r["Pur_Price"]);
                netTot = grTotal - itemDisc;

                c["cPrice"].Value = price.ToString(decimalFormat);
                c["cGTotal"].Value = grTotal.ToString(decimalFormat);
                c["cDisc"].Value = itemDisc.ToString(decimalFormat);
                c["cTotal"].Value = itemTot.ToString(decimalFormat);
                c["Pur"].Value = purPrice.ToString(decimalFormat);
                c["cNetValue"].Value = netTot.ToString(decimalFormat);
                c["DiscValues"].Value = discValue.ToString(decimalFormat);
                c["cMrp"].Value = mrp.ToString(decimalFormat);
                c["Cfree"].Value = r["FREE"];
                if (hasTax)
                {
                    taxpercent = Convert.ToDouble(r["ITEM_TAX_PER"]);
                    c["cTaxPer"].Value = taxpercent.ToString(decimalFormat);
                    taxAmt = Convert.ToDouble(r["ITEM_TAX"]);
                    c["cTaxAmt"].Value = taxAmt.ToString(decimalFormat);
                }

                grossTotal1 += Convert.ToDouble(c["cGTotal"].Value);
                discTotal1 += Convert.ToDouble(c["cDisc"].Value);
                netTotal1 += Convert.ToDouble(c["cNetValue"].Value);
                taxTotal1 += Convert.ToDouble(c["cTaxAmt"].Value);
                itemTotal1 += Convert.ToDouble(c["cTotal"].Value);

                txtGrossTotal.Text = grossTotal1.ToString(decimalFormat);
                txtLineDiscTotal.Text = discTotal1.ToString(decimalFormat);
                txtNetTotal.Text = netTotal1.ToString(decimalFormat);
                txtTaxTotal.Text = taxTotal1.ToString(decimalFormat);
                txtItemTotal.Text = itemTotal1.ToString(decimalFormat);

                c["cDescArb"].Value = r["ITEM_DESC_ARB"];
                c["SerialNos"].Value = r["SERIALNO"];
                c["uomQty"].Value = r["UOM_QTY"];
                c["cost_price"].Value = r["cost_price"];
                c["supplier_id"].Value = r["supplier_id"];
                c["supplier_name"].Value = r["supplier_name"];
                c["uHSNNO"].Value = r["Group"];
            }
            conn.Close();
        }

        public void GetLedgerId(string CusCode)
        {
            if (CusCode == "")
            {
                CASHACC.SelectedValue = 21;
            }
            else
            {
                DataTable dt = new DataTable();
                led.CUSCODE = CusCode;
                led.TABLE = "REC_CUSTOMER";
                dt = led.GetLedgerId();
                if (dt.Rows.Count > 0)
                {
                    CASHACC.SelectedValue = dt.Rows[0][0].ToString();
                }
                else
                {
                    CASHACC.SelectedValue = 21;
                }
            }
        }

        public void GetCustomerLevelDiscount(string CUS_CODE)
        {
            conn.Open();
            cmd.CommandText = "SELECT GEN_DISCOUNT_TYPE.VALUE, GEN_DISCOUNT_TYPE.TYPE AS Type FROM            REC_CUSTOMER_TYPE INNER JOIN REC_CUSTOMER ON REC_CUSTOMER_TYPE.CODE = REC_CUSTOMER.TYPE INNER JOIN  GEN_DISCOUNT_TYPE ON REC_CUSTOMER_TYPE.DISCOUNT_TYPE = GEN_DISCOUNT_TYPE.CODE WHERE        (REC_CUSTOMER.CODE ='" + CUS_CODE + "')";
            cmd.CommandType = CommandType.Text;
            SqlDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                lblCustomerDisctValue.Text = r[0].ToString();
                lblCustomerDiscType.Text = r[1].ToString();
            }
            conn.Close();
        }

        private void btnCust_Click(object sender, EventArgs e)
        {
            //customer
            CommonHelp h = new CommonHelp(0, genEnum.Customer);
            if (h.ShowDialog() == DialogResult.OK && h.c != null)
            {
                CUSTOMER_CODE.Text = Convert.ToString(h.c[0].Value);
                CUSTOMER_NAME.Text = Convert.ToString(h.c[1].Value);

                customer(CUSTOMER_CODE.Text);

                CASHACC.SelectedValue = Convert.ToString(h.c["LedgerId"].Value);
                chkCustomeleveldiscount.Enabled = true;

                if (FocusSalesMan)
                {
                    ActiveControl = SALESMAN_CODE;
                    //SALESMAN_NAME.Focus();
                }
                else if (FocusDate)
                {
                    ActiveControl = DOC_DATE_GRE;
                }
                else if (Focus_Rate_Type)
                {
                    ActiveControl = RATE_CODE;
                    //RATE_CODE.Focus();
                }
                else if (Focus_Sale_Type)
                {
                    ActiveControl = cmbInvType;
                    //cmbInvType.Focus();
                }
                else if (hasBarcode)
                {

                    if (SalebyBarcode)
                    {
                        ActiveControl = BARCODE;
                    }
                    else
                    {
                        ActiveControl = ITEM_NAME;
                    }
                }
                else if (SalebyItemName)
                {
                    ActiveControl = ITEM_NAME;
                }
                else if (SalebyItemCode)
                {
                    ActiveControl = ITEM_CODE;
                }
                else
                {
                    ActiveControl = ITEM_NAME;
                }
            }
        }

        private void btnSales_Click(object sender, EventArgs e)
        {
            //salesman
            SalesManMasterHelp salmas = new SalesManMasterHelp();
            if (salmas.ShowDialog() == DialogResult.OK && salmas.c != null)
            {
                SALESMAN_CODE.Text = Convert.ToString(salmas.c[0].Value);
                SALESMAN_NAME.Text = Convert.ToString(salmas.c[1].Value);
            }
        }
        
        int TaxId;
        public void GetTaxRate()
        {
            conn.Open();
            cmd.CommandText = "SELECT TaxRate from GEN_TAX_MASTER where TaxId=" + TaxId;
            cmd.CommandType = CommandType.Text;
            SqlDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                ITEM_TAX_PER.Text = r[0].ToString();
            }
            conn.Close();
        }
        
        private void btnItemCode_Click(object sender, EventArgs e)
        {
            ItemMasterHelp h = new ItemMasterHelp(0);
            if (h.ShowDialog() == DialogResult.OK && h.c != null)
            {
                ITEM_CODE.Text = Convert.ToString(h.c[0].Value);
                ITEM_NAME.Text = Convert.ToString(h.c[1].Value);
                //   TaxId = Convert.ToInt32(h.c[29].Value);
                //  PRICE.Text = Convert.ToString(h.c[8].Value);

                if (hasBatch)
                {
                    BATCH.Focus();
                }

                addUnits();
                //   GetTaxRate();


                ITEM_NAME.Focus();




            }
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

        private void clearItem()
        {
           // hcnNO = "";
            txt_HSN.Text="";
            c_price = "";
            sup_id = "";
            sup_name = "";
            HasSeasonDiscount = false;
            ITEM_CODE.Text = "";
            BARCODE.Text = "";
            ITEM_NAME.Text = "";
            if (hasBatch)
            {
                BATCH.Text = "";
                EXPIRY_DATE.Value = DateTime.Today;
            }
            unitsTable.Rows.Clear();
            QUANTITY.Text = "";
            CFLAG = 0;
            //PRICE.Text = "0";

            PRICE.Text = decimalFormat;
            GROSS_TOTAL.Text = decimalFormat;
            ITEM_DISCOUNT.Text = decimalFormat;
            ITEM_TOTAL.Text = decimalFormat;
            lblProfit.Text = decimalFormat;
            tb_netvalue.Text = decimalFormat;
            txtfree.Text = "0";
            tb_mrp.Text = decimalFormat;
            if (hasTax)
            {
                ITEM_TAX_PER.Text = decimalFormat;
                ITEM_TAX.Text = decimalFormat;
            }
            tb_WarantyPrd.Text = "";
            tb_warty.Text = "";
            wrty_period = "";
            wrty_type = "";
            selectedRow = -1;
            EditActive = false;
            DiscountLimitOccured = false;
            PurchasePrice = 0;
            Profit = 0;

            ShowStock = false;
            lblstock.Text = "";
            SERIALNO.Text = "";
            if (!HASSERIAL)
            {
                PNLSERIAL.Visible = false;
                kryptonLabel36.Visible = false;
                SERIALNO.Visible = false;
            }
            else
            {
                PNLSERIAL.Visible = true;
                kryptonLabel36.Visible = true;
                SERIALNO.Visible = true;
            }
            tb_descr.Text = "";
            pur_price = 0;
            BarcodeFlag = false;
            tool.Hide(ITEM_NAME);

            // txtMultiSerials.Visible = false;
            panMultiSerials.Visible = false;
            txtMultiSerials.Text = "";
            //panel_waranty.Visible = false;
        }
        
        private bool itemValid()
        {
            if (ITEM_TAX_PER.Text == "")
            {
                ITEM_TAX_PER.Text = "0";
            }

            if (ITEM_DISCOUNT.Text == "")
            {
                ITEM_DISCOUNT.Text = "0";
            }

            if (ITEM_CODE.Text != "" && QUANTITY.Text != "" && PRICE.Text != "")
            {
                return true;
            }
            else
            {
                MessageBox.Show("Please fill all the fields!");
                ITEM_CODE.Focus();
                return false;
            }
        }

        public bool CheckItemStokOut()
        {
            try
            {

                if (!(StockOut = General.IsEnabled(Settings.StockOut)))
                {
                    if (Convert.ToDecimal(lblstock.Text) <= 0)
                    {
                        return false;
                    }
                    else
                    {
                        if (Convert.ToDouble(QUANTITY.Text) * Convert.ToDouble(UOM.SelectedValue) > Convert.ToDouble(lblstock.Text))
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
                else
                {
                    return true;
                }

            }
            catch
            {
                return false;
            }

        }

        private void addItem()
        {

            try
            {
                decimal pr, qt;
                double discVal;
                if (CheckItemStokOut())
                {
                    if (itemValid())
                    {
                        if (selectedRow == -1)
                        {
                            selectedRow = dgItems.Rows.Add(new DataGridViewRow());
                        }
                        dgItems.CurrentCell = dgItems.Rows[selectedRow].Cells[0];
                        DataGridViewCellCollection c = dgItems.Rows[selectedRow].Cells;

                        c["cSl"].Value = selectedRow + 1;

                        c["cCode"].Value = ITEM_CODE.Text;
                        c["cName"].Value = ITEM_NAME.Text;
                        if (hasBatch)
                        {
                            c["cBatch"].Value = BATCH.Text;
                            c["cExpDate"].Value = EXPIRY_DATE.Value.ToString("dd/MM/yyyy");
                        }

                        c["cUnit"].Value = UOM.Text;
                        c["cQty"].Value = QUANTITY.Text;
                        if (BARCODE.Text == "")
                        {
                            BarcodeFlag = false;
                            string t = purcalculation(ITEM_CODE.Text, Convert.ToDouble(c["cQty"].Value));
                            string[] parts = t.Split(',');
                            try
                            {
                                pr = Convert.ToDecimal(parts[0]);
                                if (pr == 0)
                                {
                                    string result = "";
                                    conn.Open();
                                    cmd.CommandType = CommandType.Text;
                                    cmd.CommandText = "select PRICE FROM INV_ITEM_PRICE WHERE SAL_TYPE='PUR' AND ITEM_CODE='" + ITEM_CODE.Text + "'";
                                    result = Convert.ToString(cmd.ExecuteScalar());
                                    pr = Convert.ToDecimal(result) * Convert.ToDecimal(QUANTITY.Text);
                                }

                            }
                            catch
                            {
                                pr = 0;
                            }
                            finally
                            {
                                conn.Close();
                            }

                            try
                            {
                                qt = Convert.ToDecimal(parts[1]);
                            }
                            catch
                            {
                                qt = 0;
                            }
                            c["Pur"].Value = pr.ToString(decimalFormat);
                            c["hasbar"].Value = BarcodeFlag.ToString();
                            pr = 0;

                        }

                        if (BARCODE.Text != "")
                        {
                            //BarcodeFlag = true;
                            //string co = BARCODE.Text;
                            //int len = Convert.ToInt16((co.Length - 1));
                            //string Bat = co.Remove(0, 7);
                            //string t = barcodePurcal(Bat, Convert.ToInt32(c["cQty"].Value));
                            //string[] parts = t.Split(',');
                            //try
                            //{
                            //    pr = Convert.ToDecimal(parts[0]);
                            //}
                            //catch
                            //{
                            //    pr = 0;
                            //}
                            //try
                            //{
                            //    qt = Convert.ToDecimal(parts[1]);
                            //}
                            //catch
                            //{
                            //    qt = 0;
                            //}
                            //if (qt < Convert.ToDecimal(QUANTITY.Text))
                            //{

                            //    MessageBox.Show("Quantity Greater Than In Stock");
                            //    QUANTITY.Text = qt.ToString();
                            //    c["cQty"].Value = qt.ToString();
                            //}
                            //c["Pur"].Value = pr.ToString(decimalFormat);
                            //c["hasbar"].Value = BarcodeFlag.ToString();
                            //c["pbatch"].Value = Bat;
                            //pr = 0;
                            c["colBATCH"].Value = BARCODE.Text;
                           
                        }
                        if (txt_HSN.Text != "")
                        {
                            c["uHSNNO"].Value = txt_HSN.Text;
                        }
                        c["cPrice"].Value = PRICE.Text;
                        if (hasTax)
                        {
                            c["cTaxPer"].Value = ITEM_TAX_PER.Text;
                            c["cTaxAmt"].Value = ITEM_TAX.Text;
                        }
                        c["cGTotal"].Value = GROSS_TOTAL.Text;
                        c["cNetValue"].Value = tb_netvalue.Text;
                        c["DiscTypes"].Value = DiscType;
                        discVal = Convert.ToDouble(ITEM_DISCOUNT.Text);
                        c["cDisc"].Value = DiscAmt.ToString(decimalFormat);
                        c["DiscValues"].Value = discVal.ToString(decimalFormat);
                        c["cMrp"].Value = Convert.ToDouble(tb_mrp.Text).ToString(decimalFormat);
                        c["Cfree"].Value = txtfree.Text;
                        c["cTotal"].Value = ITEM_TOTAL.Text;
                        c["cDescArb"].Value = ItemArabicName;
                        c["description"].Value = tb_descr.Text;
                        c["supplier_id"].Value = sup_id;
                        c["supplier_name"].Value = sup_name;
                        c["cost_price"].Value = c_price;
                       // c["uHSNNO"].Value = hcnNO;

                        double qty = 0;
                        qty = Convert.ToDouble(QUANTITY.Text);

                        if (qty > 1)
                        {
                            String[] data = Convert.ToString(txtMultiSerials.Text).Split('\n');
                            c["SerialNos"].Value = String.Join(",", data);
                        }
                        else
                        {
                            c["SerialNos"].Value = SERIALNO.Text;
                        }
                        //DDDY
                        c["uomQty"].Value = unitsTable.Select("UNIT_CODE= '" + UOM.Text + "'").First()["PACK_SIZE"];
                        double iqty = Convert.ToDouble(c["cQty"].Value);
                        double uqty = Convert.ToDouble(c["uomQty"].Value);
                        double nettAmount = Convert.ToDouble(c["cNetValue"].Value);

                        dgItems.Columns[1].Width = 150;
                        dgItems.Columns[dgItems.ColumnCount - 1].FillWeight = 20;
                        
                        grossTotal = 0;
                        discTotal = 0;
                        netTotal = 0;
                        taxTotal = 0;
                        itemTotal = 0;

                        for (int i = 0; i < dgItems.RowCount; i++)
                        {
                            DataGridViewCellCollection cell = dgItems.Rows[i].Cells;
                            grossTotal += Convert.ToDouble(cell["cGTotal"].Value);
                            discTotal += Convert.ToDouble(cell["cDisc"].Value);
                            netTotal += Convert.ToDouble(cell["cNetValue"].Value);
                            taxTotal += Convert.ToDouble(cell["cTaxAmt"].Value);
                            itemTotal += Convert.ToDouble(cell["cTotal"].Value);
                        }
                        txtGrossTotal.Text = grossTotal.ToString(decimalFormat);
                        txtLineDiscTotal.Text = discTotal.ToString(decimalFormat);
                        txtNetTotal.Text = netTotal.ToString(decimalFormat);
                        txtTaxTotal.Text = taxTotal.ToString(decimalFormat);
                        txtItemTotal.Text = itemTotal.ToString(decimalFormat);
                        totalCalculation();
                        Disctext.Text = "Disc Amt";
                        DiscType = "Disc Amt";
                        ActiveForm = true;                       
                    }
                }
                else
                {
                    MessageBox.Show("Selected Quantity Item is not in stock");
                    clearItem();

                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            calculateGSTTaxes();
        }

        private void calculateGSTTaxes()
        {
            Dictionary<Double, Double> taxTable = new Dictionary<Double, Double>();
            foreach (DataGridViewRow row in dgItems.Rows)
            {
                DataGridViewCellCollection c = row.Cells;
                double taxRate = 0;
                try
                {
                    taxRate = Convert.ToDouble(c["cTaxPer"].Value);
                }
                catch{}
                double taxAmount = 0;
                try
                {
                    taxAmount = Convert.ToDouble(c["cTaxAmt"].Value);
                }
                catch{}

                if (taxRate > 0)
                {
                    double previousAmount = 0;
                    if (taxTable.ContainsKey(taxRate))
                    {
                        previousAmount = Convert.ToDouble(taxTable[taxRate]);
                    }
                    else
                    {
                        taxTable.Add(taxRate, previousAmount);
                    }
                    taxTable[taxRate] = (previousAmount + taxAmount);
                }
            }

            dgvGSTTaxes.Rows.Clear();
            foreach (KeyValuePair<Double, Double> item in taxTable)
            {
                dgvGSTTaxes.Rows.Add((item.Key / 2).ToString(decimalFormat), (item.Value / 2).ToString(decimalFormat), (item.Key / 2).ToString(decimalFormat), (item.Value / 2).ToString(decimalFormat), (item.Key).ToString(decimalFormat), (item.Value).ToString(decimalFormat));
            }
        }

        private void totalCalculation()
        {
            if (txtFreight.Text.Trim() != string.Empty)
            {
                decimal freightAmount = Convert.ToDecimal(txtFreight.Text);
                double subTotal = 0, discount = 0, netAmount = 0, tax = 0, vat = 0;
                for (int i = 0; i < dgItems.Rows.Count; i++)
                {
                    subTotal += Convert.ToDouble(dgItems.Rows[i].Cells["cTotal"].Value);
                    if (hasTax)
                    {
                        tax = tax + Convert.ToDouble(dgItems.Rows[i].Cells["cTaxAmt"].Value);
                        vat = tax * .01;
                    }

                }
                if (kryptonLabel25.Text == "Disc Amt")
                {
                    discount = Convert.ToDouble(DISCOUNT.Text);
                }
                else
                {
                    discount = Convert.ToDouble(TOTAL_AMOUNT.Text) * (Convert.ToDouble(DISCOUNT.Text) / 100);
                }

                TOTAL_AMOUNT.Text = subTotal.ToString(decimalFormat);

                netAmount = subTotal - discount;
                if (chkRoundOff.Checked)
                {
                    decimal[] roundedValues = Common.getRoundOffValues(((decimal)netAmount + freightAmount));
                    NET_AMOUNT.Text = roundedValues[0].ToString(decimalFormat);
                    txtRoundOff.Text = roundedValues[1].ToString(decimalFormat);
                }
                else
                {
                    NET_AMOUNT.Text = (netAmount + Convert.ToDouble(freightAmount)).ToString(decimalFormat);
                    txtRoundOff.Text = decimalFormat;
                }

                if (hasTax)
                {
                    TAX_TOTAL.Text = tax.ToString(decimalFormat);
                    VATT.Text = vat.ToString(decimalFormat);
                }
                TempDiscount = discount.ToString();
            }
            else
            {
                txtFreight.Text = decimalFormat;
            }
            
        }

        private void dgItems_Click(object sender, EventArgs e)
        {
            SqlCommand cmd;
            DataTable dt = new DataTable();
            if (dgItems.Rows.Count > 0 && dgItems.CurrentRow != null)
            {
                EditActive = true;
                DataGridViewCellCollection c = dgItems.CurrentRow.Cells;
                selectedRow = dgItems.CurrentRow.Index;
                ITEM_CODE.Text = Convert.ToString(c["cCode"].Value);
                ITEM_NAME.Text = Convert.ToString(c["cName"].Value);
                if (c["colBATCH"].Value != null)
                {
                    BARCODE.Text = c["colBATCH"].Value.ToString();
                }
                if (c["uHSNNO"].Value != null)
                {
                   txt_HSN.Text = c["uHSNNO"].Value.ToString();
                }
                if (hasBatch)
                {
                    BATCH.Text = Convert.ToString(c["cBatch"].Value);
                    EXPIRY_DATE.Value = DateTime.ParseExact(Convert.ToString(c["cExpDate"].Value), "dd/MM/yyyy", null);
                }
                addUnits();
                UOM.Text = Convert.ToString(c["cUnit"].Value);
                QUANTITY.Text = Convert.ToString(c["cQty"].Value);
                PRICE.Text = Convert.ToString(c["cPrice"].Value);
                pricefob = Convert.ToDouble(PRICE.Text);
                sales_price = pricefob / Convert.ToDouble(c["uomQty"].Value);
                if (hasTax)
                {
                    ITEM_TAX_PER.Text = Convert.ToString(c["cTaxPer"].Value);
                    ITEM_TAX.Text = Convert.ToString(c["cTaxAmt"].Value);
                }
                GROSS_TOTAL.Text = Convert.ToString(c["cGTotal"].Value);
                ITEM_DISCOUNT.Text = Convert.ToString(c["DiscValues"].Value);
                if (Convert.ToString(c["DiscTypes"].Value) == "Percentage")
                {

                    CalculateDiscPerct();
                }
                else
                {
                    CalculateDiscAmt();
                }

                ITEM_TOTAL.Text = Convert.ToString(c["cTotal"].Value);


                /*if (c["cPeriod"].Value.ToString() != "" || c["cPeriod"].Value.ToString() != "null" || c["cPeriodtype"].Value.ToString() != "" || c["cPeriodtype"].Value.ToString() != "null")
                {
                    wrty_period = c["cPeriod"].Value.ToString();
                    wrty_type = c["cPeriodtype"].Value.ToString();
                }*/
                cmd = new SqlCommand("SELECT HASSERIAL FROM INV_ITEM_DIRECTORY WHERE CODE='" + Convert.ToString(c["cCode"].Value) + "'", conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
                HASSERIAL = Convert.ToBoolean(dt.Rows[0][0].ToString());
                PNLSERIAL.Visible = HASSERIAL;
                kryptonLabel36.Visible = HASSERIAL;
                SERIALNO.Visible = HASSERIAL;
                kryptonLabel36.Visible = HASSERIAL;
                SERIALNO.Visible = HASSERIAL;
                string serial = Convert.ToString(c["SerialNos"].Value);
                double q = Convert.ToDouble(c["cQty"].Value);
                if (q > 1 && serial != "")
                {
                    String[] data = Convert.ToString(c["SerialNos"].Value).Split(',');
                    txtMultiSerials.Text = String.Join("\n", data);
                    SERIALNO.Multiline = true;
                    SERIALNO.Height = 20;
                    panMultiSerials.Visible = true;
                    panMultiSerials.Location = new Point(panManageItem.Location.X + PNLSERIAL.Location.X + SERIALNO.Location.X, panManageItem.Location.Y + PNLSERIAL.Location.Y + SERIALNO.Location.Y + SERIALNO.Height);
                    txtMultiSerials.Visible = true;
                    SERIALNO.Text = Convert.ToString(c["SerialNos"].Value);
                }
                else
                {
                    SERIALNO.Text = Convert.ToString(c["SerialNos"].Value);
                    panMultiSerials.Visible = false;
                }

                //if (SERIALNO.Text != "")
                //{
                //    PNLSERIAL.Visible = true;
                //    kryptonLabel36.Visible = true;
                //    SERIALNO.Visible = true;
                //}
                //else
                //{
                //    PNLSERIAL.Visible = false;
                //    kryptonLabel36.Visible = false;
                //    SERIALNO.Visible = false;
                //}

                PNL_DATAGRIDITEM.Visible = false;
                QUANTITY.Focus();
            }
        }
        int CFLAG = 0;
        private void calculateGrossAmount(object sender, EventArgs e)
        {
            double localQty = 0;
            double localPrice = 0;

            try
            {
                localQty = Convert.ToDouble(QUANTITY.Text);
            }
            catch (Exception){}

            try
            {
                localPrice = Convert.ToDouble(PRICE.Text);
            }
            catch (Exception){}

            if (CFLAG == 1)
                pricefob = localPrice;
            GROSS_TOTAL.Text = (localQty * localPrice).ToString(decimalFormat);
            tb_netvalue.Text = ((localQty * localPrice) - Convert.ToDouble(DiscAmt)).ToString(decimalFormat);
            gettax();
            CFLAG = 1;
        }

        private void addTaxGAmt(object sender, EventArgs e)
        {
            try
            {
                double total = 0;
                if (QUANTITY.Text.Trim() != "" && PRICE.Text.Trim() != "")
                {
                    total = Convert.ToDouble(QUANTITY.Text) * Convert.ToDouble(PRICE.Text);
                }
                string txt = (sender as KryptonTextBox).Text;
                if (txt.Trim() != "")
                {
                    total = total + Convert.ToDouble(txt);
                }
                ITEM_TOTAL.Text = total.ToString(decimalFormat);
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void calculateTax(object sender, EventArgs e)
        {
            gettax();
        }


        private void gettax()
        {
            try
            {
                double total = 0;
                double tax = 0;
                if (QUANTITY.Text.Trim() != "" && PRICE.Text.Trim() != "")
                {
                    total = Convert.ToDouble(QUANTITY.Text) * Convert.ToDouble(PRICE.Text) - DiscAmt;
                }
                if (ITEM_TAX_PER.Text.Trim() != "" && GROSS_TOTAL.Text.Trim() != "")
                {
                    tax = total * (Convert.ToDouble(ITEM_TAX_PER.Text) / 100);
                }
                ITEM_TAX.Text = tax.ToString(decimalFormat);
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }
        double DiscAmt;
        public bool taxcal;
        
        public void taxEnable()
        {
          try
           {
                cmd.CommandText = "SELECT TAX FROM SYS_SETUP";
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.Text;
                DataTable dt = new DataTable();
                sda.Fill(dt);
                taxcal = Convert.ToBoolean(dt.Rows[0]["TAX"].ToString());
            }
            catch (Exception ex)
            {
               // conn.Close();
             }

        }

        private void ITEM_DISCOUNT_TextChanged(object sender, EventArgs e)
        {
           // taxEnable();
            if (ITEM_DISCOUNT.Text.Trim() != "" && GROSS_TOTAL.Text.Trim() != "")
            {
                if (DiscType == "Percentage")
                {
                    if (ITEM_DISCOUNT.Text == "NaN")
                    {
                        ITEM_DISCOUNT.Text = "0";
                    }
                    ITEM_TOTAL.Text = (Convert.ToDouble(GROSS_TOTAL.Text) - (Convert.ToDouble(GROSS_TOTAL.Text) * (Convert.ToDouble(ITEM_DISCOUNT.Text) / 100))).ToString();
                    DiscAmt = Convert.ToDouble(GROSS_TOTAL.Text) * (Convert.ToDouble(ITEM_DISCOUNT.Text) / 100);
                    if (ITEM_TAX.Text != "" && ITEM_DISCOUNT.Text.Trim() != "" && QUANTITY.Text.Trim() != "" && PRICE.Text.Trim() != "")
                    {
                        double tax = 0;
                        double DISC = 0;
                        if (ITEM_DISCOUNT.Text.Trim() != "")
                        {
                            DISC = Convert.ToDouble(ITEM_DISCOUNT.Text);
                        }
                        if (ITEM_TAX_PER.Text.Trim() != "")
                        {
                            tax = Convert.ToDouble(ITEM_TAX_PER.Text);
                        }
                        double disc_amt = Convert.ToDouble(PRICE.Text) * Convert.ToDouble(QUANTITY.Text) * DISC / 100;
                        double tax_amt = (Convert.ToDouble(PRICE.Text) * Convert.ToDouble(QUANTITY.Text) - disc_amt) * tax / 100;
                        string disc_amount = disc_amt.ToString(decimalFormat);
                        string tax_amount = tax_amt.ToString(decimalFormat);
                        ITEM_TAX.Text = ((Convert.ToDouble(PRICE.Text) * Convert.ToDouble(QUANTITY.Text) - disc_amt) * tax / 100).ToString(decimalFormat);

                        GROSS_TOTAL.Text = (Convert.ToDouble(PRICE.Text) * Convert.ToDouble(QUANTITY.Text)).ToString(decimalFormat);
                        //ITEM_TOTAL.Text = (((Convert.ToDouble(PRICE.Text) * Convert.ToDouble(QUANTITY.Text) - disc_amt) + (Convert.ToDouble(PRICE.Text) * Convert.ToDouble(QUANTITY.Text) - disc_amt) * tax / 100).ToString(decimalFormat));
                        if (taxcal == false)
                        {
                            ITEM_TOTAL.Text = ((Convert.ToDouble(PRICE.Text) * Convert.ToDouble(QUANTITY.Text)) - Convert.ToDouble(disc_amount)).ToString(decimalFormat);
                        }
                        else
                        {
                            ITEM_TOTAL.Text = ((Convert.ToDouble(PRICE.Text) * Convert.ToDouble(QUANTITY.Text) + Convert.ToDouble(tax_amount)) - Convert.ToDouble(disc_amount)).ToString(decimalFormat);
                        }
                            tb_netvalue.Text = ((Convert.ToDouble(PRICE.Text) * Convert.ToDouble(QUANTITY.Text) - Convert.ToDouble(disc_amount)).ToString(decimalFormat));
                    }
                }
                else
                {
                    ITEM_TOTAL.Text = (Convert.ToDouble(GROSS_TOTAL.Text) - (Convert.ToDouble(GROSS_TOTAL.Text) * (Convert.ToDouble(ITEM_DISCOUNT.Text) / 100))).ToString();
                    DiscAmt = Convert.ToDouble(ITEM_DISCOUNT.Text);
                    if (ITEM_TAX.Text != "" && ITEM_DISCOUNT.Text.Trim() != "" && QUANTITY.Text.Trim() != "" && PRICE.Text.Trim() != "")
                    {
                        double tax = 0;
                        double DISC = 0;
                        if (ITEM_DISCOUNT.Text.Trim() != "")
                        {
                            DISC = Convert.ToDouble(ITEM_DISCOUNT.Text);
                        }
                        if (ITEM_TAX_PER.Text.Trim() != "")
                        {
                            tax = Convert.ToDouble(ITEM_TAX_PER.Text);
                        }
                        double disc_amt = Convert.ToDouble(ITEM_DISCOUNT.Text);
                        double tax_amt = (Convert.ToDouble(PRICE.Text) * Convert.ToDouble(QUANTITY.Text) - disc_amt) * tax / 100;
                        string disc_amount = disc_amt.ToString(decimalFormat);
                        string tax_amount = tax_amt.ToString(decimalFormat);
                        ITEM_TAX.Text = ((Convert.ToDouble(PRICE.Text) * Convert.ToDouble(QUANTITY.Text) - disc_amt) * tax / 100).ToString(decimalFormat);
                        GROSS_TOTAL.Text = (Convert.ToDouble(PRICE.Text) * Convert.ToDouble(QUANTITY.Text)).ToString(decimalFormat);
                       // ITEM_TOTAL.Text = ((Convert.ToDouble(PRICE.Text) * Convert.ToDouble(QUANTITY.Text) + Convert.ToDouble(tax_amount)) - Convert.ToDouble(disc_amount)).ToString(decimalFormat);
                        if (taxcal == false)
                        {
                            ITEM_TOTAL.Text = ((Convert.ToDouble(PRICE.Text) * Convert.ToDouble(QUANTITY.Text)) - Convert.ToDouble(disc_amount)).ToString(decimalFormat);
                        }
                        else
                        {
                            ITEM_TOTAL.Text = ((Convert.ToDouble(PRICE.Text) * Convert.ToDouble(QUANTITY.Text) + Convert.ToDouble(tax_amount)) - Convert.ToDouble(disc_amount)).ToString(decimalFormat);
                        }
                        
                        tb_netvalue.Text = ((Convert.ToDouble(PRICE.Text) * Convert.ToDouble(QUANTITY.Text) - Convert.ToDouble(disc_amount)).ToString(decimalFormat));

                    }
                }


                if (ITEM_DISCOUNT.Text == "")
                {
                    ITEM_TOTAL.Text = GROSS_TOTAL.Text;
                }

            }
            else if (ITEM_DISCOUNT.Text.Trim() == "")
            {

                double disc_amt = 0;
                tb_netvalue.Text = tb_netvalue.Text = ((Convert.ToDouble(PRICE.Text) * Convert.ToDouble(QUANTITY.Text) - disc_amt)).ToString(decimalFormat);
            }
            
        }

        private void linkRemoveRecord_LinkClicked(object sender, EventArgs e)
        {
            clearItem();
            grossTotal = 0;
            discTotal = 0;
            netTotal = 0;
            taxTotal = 0;
            itemTotal = 0;
            try
            {
                if (dgItems.Rows.Count > 0 && dgItems.CurrentRow != null)
                {
                    dgItems.Rows.Remove(dgItems.CurrentRow);
                    for (int i = 0; i < dgItems.Rows.Count; i++)
                    {
                        grossTotal += Convert.ToDouble(dgItems.Rows[i].Cells["cGTotal"].Value);
                        discTotal += Convert.ToDouble(dgItems.Rows[i].Cells["cDisc"].Value);
                        netTotal += Convert.ToDouble(dgItems.Rows[i].Cells["cNetValue"].Value);
                        taxTotal += Convert.ToDouble(dgItems.Rows[i].Cells["cTaxAmt"].Value);
                        itemTotal += Convert.ToDouble(dgItems.Rows[i].Cells["cTotal"].Value);
                    }

                    txtGrossTotal.Text = grossTotal.ToString(decimalFormat);
                    txtLineDiscTotal.Text = discTotal.ToString(decimalFormat);
                    txtNetTotal.Text = netTotal.ToString(decimalFormat);
                    txtTaxTotal.Text = taxTotal.ToString(decimalFormat);
                    txtItemTotal.Text = itemTotal.ToString(decimalFormat);                    
                    totalCalculation();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void addUnits()
        {
            unitsTable.Rows.Clear();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT UNIT_CODE, PACK_SIZE FROM INV_ITEM_DIRECTORY_UNITS WHERE ITEM_CODE = '" + ITEM_CODE.Text + "'";
            cmd.CommandType = CommandType.Text;
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(unitsTable);
            //this.UOM.SelectedIndexChanged -= new EventHandler(UOM_SelectedIndexChanged);
            UOM.DisplayMember = "UNIT_CODE";
            UOM.ValueMember = "PACK_SIZE";
            UOM.DataSource = unitsTable;
            //this.UOM.SelectedIndexChanged += new EventHandler(UOM_SelectedIndexChanged);
        }

        private bool valid()
        {

            if (CUSTOMER_CODE.Text == "" && type == "SAL.CRD")
            {
                MessageBox.Show("You must select a Customer for Credit Sale!");
                return false;
            }
            if (CUSTOMER_CODE.Text == "" && cmbInvType.Text=="B2B")
            {
                MessageBox.Show("You must select a Customer for B2B sale!");
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
                if (date <= Convert.ToDateTime(DOC_DATE_GRE.Value.ToShortDateString()))
                {

                }
                else
                {
                    MessageBox.Show("Date Limit Exceeded!!");
                    return false;
                }
            }
            if (SALESACC.Text == CASHACC.Text)
            {
                MessageBox.Show("Choose different accounts for transaction");
                CASHACC.Focus();
                return false;
            }
          /*  if (string.IsNullOrEmpty(CARD_NO.Text) && PAY_CODE.Text != "CSH")
            {
                string field = string.Empty;
                if (PAY_CODE.Text == "CHQ")
                {
                    field = "cheque";
                }
                else if (PAY_CODE.Text == "CRD")
                {
                    field = "card";
                }
                else if (PAY_CODE.Text == "DEP")
                {
                    field = "account";
                }
                MessageBox.Show("Enter " + field + " number");
                CARD_NO.Focus();
                return false;
            }     */      

            if (ID != "")
            {
                if (NOTES.Text == "")
                {
                    NOTES.Focus();
                    MessageBox.Show("Enter reason for updation in remarks");
                    return false;
                }

            }

            if (ID == "")
            {
                if (CUSTOMER_CODE.Text != "")
                {
                    if (IsActive())
                    {
                        double credit = creditamt();
                        double customercredit = GetCustomerCreditLimit();
                        double amountafterpurchase = credit + Convert.ToDouble(NET_AMOUNT.Text) - Convert.ToDouble(txtcashrcvd.Text);
                        if (amountafterpurchase > customercredit)
                        {
                            MessageBox.Show("The Customer Credit limit has reached! Cannot buy Products in Credit \n Credit Limit Is " + customercredit.ToString() + "\n Credit Amount Till Last Purchase is " + credit.ToString() + "");
                            return false;
                        }
                    }
                }

            }
            if (dgItems.Rows.Count <= 0)
            {
                MessageBox.Show("Please add items to save.");
                return false;
            }
            else
            {
                return true;
            }
        }

        public double creditamt()
        {
            double data = 0;
            DataTable dt = new DataTable();
            cmd.CommandText = "SELECT     SUM(DEBIT)-SUM(CREDIT)  AS creditamt FROM         tb_Transactions WHERE     (ACCNAME = '" + CASHACC.Text + "') AND (ACCID = '" + CASHACC.SelectedValue + "')";
            cmd.CommandType = CommandType.Text;
            adapter.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                data = Convert.ToDouble(dt.Rows[0][0].ToString());
            }
            return data;
        }

        public double GetCustomerCreditLimit()
        {
            double data = 0;
            DataTable dt = new DataTable();
            cmd.CommandText = "SELECT     REC_CUSTOMER_TYPE.CREDIT_LEVEL FROM         REC_CUSTOMER INNER JOIN REC_CUSTOMER_TYPE ON REC_CUSTOMER.TYPE = REC_CUSTOMER_TYPE.CODE WHERE     (REC_CUSTOMER.CODE = '" + CUSTOMER_CODE.Text + "')";
            cmd.CommandType = CommandType.Text;
            adapter.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                data = Convert.ToDouble(dt.Rows[0][0].ToString());
            }
            return data;
        }
        
        public string saledtlChecker(string code, string inv)
        {
            string qty = "";
            cmd.Connection = conn;
            cmd.CommandText = "select QUANTITY from INV_SALES_DTL where ITEM_CODE='" + code + "' AND DOC_ID='" + inv + "' ";
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter Adap = new SqlDataAdapter();
            Adap.SelectCommand = cmd;
            DataTable dt = new DataTable();
            Adap.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                qty = dt.Rows[0]["QUANTITY"].ToString();
            }
            return qty;
        }
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            TransDate = DOC_DATE_GRE.Value;
            if (PAY_CODE.Text == "CHQ")
            {
                TransDate = Convert.ToDateTime(CHQ_DATE.Value);
            }
            if (CUSTOMER_CODE.Text != "")
            {
                type = "SAL.CRD";
                type1 = "SAL.CRD";
            }
            else
            {
                type = "SAL.CSS";
                type1 = "SAL.CRD";
            }


            if (valid())
            {
                string status = "Added!";
                double discount;
                discount = Convert.ToDouble(DISCOUNT.Text);
                if (ID == "")
                {
                    GetMaxDocID();
                    DOC_NO.Text = General.generateSalesID();
                    //stock Handling in Batch Process
                    for (int i = 0; i < dgItems.Rows.Count; i++)
                    {

                        DataGridViewCellCollection c = dgItems.Rows[i].Cells;
                        Boolean isBarcodeEnabled = Convert.ToBoolean(c["hasbar"].Value);
                        if (isBarcodeEnabled)
                        {
                            if (hasBarcode == false)
                            {
                                upStockonProfit(c["cCode"].Value.ToString(), Convert.ToInt32(c["cQty"].Value));
                            }
                            if (hasBarcode == true)
                            {
                                PurchaseupStockonProfit(c["pbatch"].Value.ToString(), Convert.ToInt32(c["cQty"].Value));
                            }
                        }
                        else
                        {
                            upStockonProfit(c["cCode"].Value.ToString(), Convert.ToDouble(c["cQty"].Value));
                        }
                    }
                    cmd.CommandText = "INSERT INTO INV_SALES_HDR(DOC_ID,DOC_NO,DOC_TYPE,DOC_DATE_GRE,DOC_DATE_HIJ,CURRENCY_CODE,DOC_REFERENCE,CUSTOMER_CODE,CUSTOMER_NAME_ENG,SALESMAN_CODE,NOTES,TAX_TOTAL,VAT,DISCOUNT,TOTAL_AMOUNT,PAY_CODE,CARD_NO,FREIGHT,ROUNDOFF,CESS,CUSLEVELDISCOUNT,BRANCH,SALE_TYPE) ";
                    cmd.CommandText += "VALUES('" + Convert.ToInt32(VOUCHNUM.Text) + "','" + DOC_NO.Text + "','" + type + "','" + DOC_DATE_GRE.Value.ToString("MM/dd/yyyy") + "','" + DOC_DATE_HIJ.Text + "','" + CURRENCY_CODE.Text + "','" + DOC_REFERENCE.Text + "','" + CUSTOMER_CODE.Text + "','" + CUSTOMER_NAME.Text + "','" + SALESMAN_CODE.Text + "','" + NOTES.Text + "','" + Convert.ToDecimal(TAX_TOTAL.Text) + "','" + Convert.ToDecimal(VATT.Text) + "','" + discount + "','" + Convert.ToDecimal(TOTAL_AMOUNT.Text) + "','" + PAY_CODE.Text + "','" + CARD_NO.Text + "','" + Convert.ToDecimal(txtFreight.Text) + "','" + txtRoundOff.Text + "','" + ((!TXT_CESS.Text.Equals(""))?Convert.ToDecimal(TXT_CESS.Text) : 0) + "','" + chkCustomeleveldiscount.Checked + "','" + lg.Branch + "','" + cmbInvType.SelectedValue + "')";
                }
                else
                {
                  //  string id = VOUCHNUM.Text;
                    string id = DOC_NO.Text;
                    DeleteTransation(id);
                    modifiedtransaction();

                    //---mubarak sir (code)----//

                    SqlCommand reduceStockCommand = new SqlCommand();
                    reduceStockCommand.Connection = conn;
                    conn.Open();
                    reduceStockCommand.CommandText = "SELECT ITEM_CODE, QUANTITY, UOM_QTY, cost_price, PRICE_BATCH FROM INV_SALES_DTL WHERE DOC_NO = '" + DOC_NO.Text + "' AND DOC_TYPE = '" + type + "'";
                    SqlDataReader r = reduceStockCommand.ExecuteReader();
                    StockEntry se = new StockEntry();
                    while (r.Read())
                    {
                        double qty = (Convert.ToDouble(r["QUANTITY"]) * Convert.ToDouble(r["UOM_QTY"]));
                        if (type.Equals("SAL.CSR"))
                        {
                            qty = -1 * qty;
                        }
                        se.addStockWithBatch(Convert.ToString(r["ITEM_CODE"]), Convert.ToString(qty), Convert.ToString(r["cost_price"]), Convert.ToString(r["PRICE_BATCH"]));
                    }
                    conn.Close();
                    //---mubarak sir (code)----//
                    status = "Updated!";
                    if (hasBarcode != true)
                    {
                        for (int i = 0; i < dgItems.Rows.Count; i++)
                        {
                            //stock Handling in Batch Process during update sale
                            DataGridViewCellCollection c = dgItems.Rows[i].Cells;
                            if (c["hasbar"].Value != null && c["hasbar"].Value.ToString() == "True")
                            {
                                string qty = c["cQty"].Value.ToString();
                                string code = c["cCode"].Value.ToString();
                                string dtlQty = saledtlChecker(code, VOUCHNUM.Text);
                                if (Convert.ToDouble(qty) > Convert.ToDouble(dtlQty))
                                {
                                    upStockonProfit(code, Convert.ToDouble(qty) - Convert.ToDouble(dtlQty));
                                }
                                if (Convert.ToDouble(qty) < Convert.ToDouble(dtlQty))
                                {
                                    DownStockonProfit(code, Convert.ToDouble(qty) - Convert.ToDouble(dtlQty));
                                }
                            }
                        }
                    }
                    cmd.CommandText = "UPDATE INV_SALES_HDR SET DOC_ID='" + Convert.ToInt32(VOUCHNUM.Text) + "', DOC_DATE_GRE = '" + DOC_DATE_GRE.Value.ToString("MM/dd/yyyy") + "',DOC_DATE_HIJ = '" + DOC_DATE_HIJ.Text + "',CURRENCY_CODE = '" + CURRENCY_CODE.Text + "',DOC_REFERENCE = '" + DOC_REFERENCE.Text + "',CUSTOMER_CODE = '" + CUSTOMER_CODE.Text + "',CUSTOMER_NAME_ENG = '" + CUSTOMER_NAME.Text + "',SALESMAN_CODE = '" + SALESMAN_CODE.Text + "',NOTES = '" + NOTES.Text + "',TAX_TOTAL = '" + Convert.ToDecimal(TAX_TOTAL.Text) + "',VAT = '" + Convert.ToDecimal(VATT.Text) + "',DISCOUNT = '" + discount + "',TOTAL_AMOUNT = '" + Convert.ToDecimal(TOTAL_AMOUNT.Text) + "',PAY_CODE = '" + PAY_CODE.Text + "',FREIGHT = '" + Convert.ToDecimal(txtFreight.Text) + "',CARD_NO = '" + CARD_NO.Text + "',ROUNDOFF = '" + txtRoundOff.Text + "',CESS = '" + Convert.ToDecimal(TXT_CESS.Text) + "',CUSLEVELDISCOUNT = '" + chkCustomeleveldiscount.Checked + "',SALE_TYPE='" + cmbInvType.SelectedValue + "'  WHERE DOC_NO = '" + DOC_NO.Text + "'";
                    cmd.CommandText += "DELETE FROM INV_SALES_DTL WHERE DOC_NO = '" + DOC_NO.Text + "'";
                    DeleteTransation();

                }
                #region insert sales deatils

                cmd.CommandText += "INSERT INTO INV_SALES_DTL(DOC_TYPE,DOC_ID,DOC_NO,ITEM_CODE,ITEM_DESC_ENG,UOM,PRICE,QUANTITY,ITEM_DISCOUNT,DISC_TYPE,DISC_VALUE,SERIALNO,BRANCH,Pur_Price,hasbar,MRP,FREE";

                //...NOT VALIDATED...//
                cmd.CommandText += ",PRICE_BATCH";
                if (hasTax)
                {
                    cmd.CommandText += ",ITEM_TAX_PER,ITEM_TAX";
                }
                else
                {
                    cmd.CommandText += ",ITEM_TAX_PER,ITEM_TAX";
                }
                if (hasBatch)
                {
                    cmd.CommandText += ",BATCH,EXPIRY_DATE";
                }
                cmd.CommandText += ",UOM_QTY, cost_price, supplier_id,group_id";
                cmd.CommandText += ")";
                StockEntry stockEntry = new StockEntry();
                for (int i = 0; i < dgItems.Rows.Count; i++)
                {

                    DataGridViewCellCollection c = dgItems.Rows[i].Cells;

                    cmd.CommandText += "SELECT '" + type + "','" + VOUCHNUM.Text + "', '" + DOC_NO.Text + "','" + c["cCode"].Value + "','" +Common.sqlEscape(c["cName"].Value.ToString())+ "','" + c["cUnit"].Value + "','" + Convert.ToDecimal(c["cPrice"].Value) + "','" + Convert.ToDouble(c["cQty"].Value) + "','" + Convert.ToDecimal(c["cDisc"].Value) + "','" + c["DiscTypes"].Value + "','" + c["DiscValues"].Value + "','" + c["SerialNos"].Value + "','" + lg.Branch + "','" + Convert.ToDecimal(c["Pur"].Value) + "','" + Convert.ToBoolean(c["hasbar"].Value) + "','" + Convert.ToDecimal(c["cMrp"].Value) + "','" + Convert.ToDecimal(c["Cfree"].Value) + "','" + c["colBATCH"].Value.ToString() + "'";
                    if (hasTax)
                    {
                        cmd.CommandText += ",'" + c["cTaxPer"].Value + "','" + c["cTaxAmt"].Value + "'";
                    }
                    else
                    {
                        cmd.CommandText += ",'" + 0 + "','" + 0 + "'";
                    }
                    if (hasBatch)
                    {
                      //  cmd.CommandText += ",'" + c["cBatch"].Value + "','" + DateTime.ParseExact(c["cExpDate"].Value.ToString(), "dd/MM/yyyy", null) + "'";
                        cmd.CommandText += ",'" + c["cBatch"].Value + "','" + DateTime.ParseExact(c["cExpDate"].Value.ToString(), "dd/MM/yyyy", null).ToString("yyyy/MM/dd") + "'";
                    }
                    cmd.CommandText += ", '" + c["uomQty"].Value + "', '" + c["cost_price"].Value + "', '" + c["supplier_id"].Value + "', '" + c["uHSNNO"].Value + "'";
                    cmd.CommandText += " UNION ALL ";

                    //-- MUBARAK SIR CODE---//

                    string item_id = Convert.ToString(c["cCode"].Value);
                    double qty = Convert.ToDouble(c["cQty"].Value);
                    double uom_qty = Convert.ToDouble(c["uomQty"].Value);
                    double total_qty = qty * uom_qty;
                    if (!type.Equals("SAL.CSR"))
                    {
                        total_qty = -1 * total_qty;
                    }
                    stockEntry.addStockWithBatch(item_id, total_qty.ToString(), c["cost_price"].Value.ToString(), Convert.ToString(c["colBATCH"].Value));
                    //-- MUBARAK SIR CODE---//
                }
                #endregion

                cmd.CommandText = cmd.CommandText.Substring(0, cmd.CommandText.Length - 10);
                if (cmbInvType.SelectedIndex != 1)
                {
                    conn.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    cmd.CommandText = "";
                    cmd.Dispose();
                    InsertTransaction();

                    FreightTransaction();
                    RoundOFFTransaction();
                 //   CessTransaction();
                    VATTransaction();
                    DiscountTransaction();
                    if (PrintInvoice.Checked == true)
                    {
                        flagPrintEventAssigned = false;
                        counter = 0;
                        printingrecipt2();
                    }

                    if (type == "SAL.CREDITNOTE")
                    {
                        MessageBox.Show("Credit Note " + status);
                    }
                    if (txtCreditNoteNo.Text != "")
                    {
                        UpdateCreditNote();
                    }


                    ReceiptVoucher();
                    grossTotal = 0;
                    discTotal = 0;
                    netTotal = 0;
                    taxTotal = 0;
                    itemTotal = 0;
                    btnClear.PerformClick();

                }
                else
                {

                    bool flag = false;
                    DataTable dt = new DataTable();
                    SqlCommand command = new SqlCommand("SELECT CODE,DESC_ENG,TIN_NO FROM REC_CUSTOMER", conn);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dt);
                    if (CUSTOMER_CODE.Text != string.Empty && CUSTOMER_NAME.Text != string.Empty)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (dt.Rows[i][0].ToString() == CUSTOMER_CODE.Text && dt.Rows[i][1].ToString() == CUSTOMER_NAME.Text)
                            {
                                if (dt.Rows[i][2].ToString() != string.Empty)
                                {
                                    flag = true;
                                    break;
                                }
                                else
                                {
                                    flag = false;
                                }
                            }
                            else
                            {
                                flag = false;
                            }
                        }
                        if (flag == false)
                        {
                            MessageBox.Show("This customer has no TIN No.");
                        }
                        else
                        {
                            conn.Open();
                            cmd.CommandType = CommandType.Text;
                            cmd.ExecuteNonQuery();
                            conn.Close();
                            cmd.CommandText = "";
                            cmd.Dispose();
                            InsertTransaction();
                            FreightTransaction();
                            RoundOFFTransaction();
                            CessTransaction();
                            VATTransaction();
                            DiscountTransaction();
                            if (PrintInvoice.Checked == true)
                            {
                                flagPrintEventAssigned = false;
                                counter = 0;
                                printingrecipt2();
                            }

                            if (type == "SAL.CREDITNOTE")
                            {
                                MessageBox.Show("Credit Note " + status);
                            }
                            if (txtCreditNoteNo.Text != "")
                            {
                                UpdateCreditNote();
                            }

                            ReceiptVoucher();
                            grossTotal = 0;
                            discTotal = 0;
                            netTotal = 0;
                            taxTotal = 0;
                            itemTotal = 0;
                            btnClear.PerformClick();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Select customer");
                    }

                }
                checkvoucher(Convert.ToInt16(VOUCHNUM.Text));
                bindgridview();
            }
        }

        public String purcalculation(string code, double qty)
        {
            string ret = "";
            string item_code = code;
            double squantity = qty;
            Decimal totPrice = 0;
            double QtyChecker = 0;

            cmd.Connection = conn;
            cmd.CommandText = "select Price,Qty,R_Id from RateChange where Item_code='" + code + "'";
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter Adap = new SqlDataAdapter();
            Adap.SelectCommand = cmd;
            DataTable dt = new DataTable();
            Adap.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; (i < dt.Rows.Count) && (squantity > 0); ++i)
                {
                    string price = dt.Rows[i]["Price"].ToString();
                    string Qty = dt.Rows[i]["Qty"].ToString();
                    string rid = dt.Rows[i]["R_Id"].ToString();
                    QtyChecker = QtyChecker + Convert.ToDouble(Qty);
                    if (squantity < Convert.ToDouble(Qty))
                    {
                        Qty = squantity.ToString();
                    }
                    totPrice = totPrice + (Convert.ToDecimal(Qty) * Convert.ToDecimal(price));
                    squantity = squantity - Convert.ToDouble(Qty);
                }
            }
            ret = totPrice.ToString() + "," + QtyChecker.ToString();
            return ret;
        }

        public String barcodePurcal(string code, double qty)
        {
            string ret = "";
            string item_code = code;
            double squantity = qty;
            Decimal totPrice = 0;
            double QtyChecker = 0;
            cmd.Connection = conn;
            cmd.CommandText = "select Price,Qty from RateChange where R_Id='" + code + "'";
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter Adap = new SqlDataAdapter();
            Adap.SelectCommand = cmd;
            DataTable dt = new DataTable();
            Adap.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; (i < dt.Rows.Count) && (squantity > 0); ++i)
                {
                    string price = dt.Rows[i]["Price"].ToString();
                    string Qty = dt.Rows[i]["Qty"].ToString();
                    //  string rid = dt.Rows[i]["R_Id"].ToString();
                    QtyChecker = QtyChecker + Convert.ToDouble(Qty);

                    if (squantity < Convert.ToDouble(Qty))
                    {
                        Qty = squantity.ToString();
                    }
                    totPrice = totPrice + (Convert.ToDecimal(Qty) * Convert.ToDecimal(price));
                    squantity = squantity - Convert.ToDouble(Qty);

                }
            }
            ret = totPrice.ToString() + "," + QtyChecker.ToString();
            return ret;
        }

        public void PurchaseupStockonProfit(string code, double qty)
        {
            SqlCommand cmd1 = new SqlCommand();
            string item_code = code;
            double squantity = qty;
            Decimal totPrice = 0;
            double QtyChecker = 0;
            string Qty = "0";

            cmd.Connection = conn;
            cmd.CommandText = "select Price,Qty from RateChange where R_Id='" + code + "'";
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter Adap = new SqlDataAdapter();
            Adap.SelectCommand = cmd;
            DataTable dt = new DataTable();
            Adap.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count && (squantity > 0); ++i)
                {
                    //string price = dt.Rows[i]["Price"].ToString();
                    // Qty = dt.Rows[i]["Qty"].ToString();
                    //string rid = dt.Rows[i]["R_Id"].ToString();

                    //totPrice = totPrice + (Convert.ToDecimal(Qty) * Convert.ToDecimal(price));
                    //if (squantity > Convert.ToInt32(Qty))
                    //{
                    //    Qty = squantity.ToString();
                    //}
                    //if (Qty != "0")
                    //{
                    //    squantity = Convert.ToInt32(Qty) - squantity;
                    //    QtyChecker = QtyChecker +Convert.ToInt16(squantity);
                    //}
                    //else
                    //{
                    //    string upstock = (Convert.ToInt32(Qty) - Convert.ToInt32(Qty)).ToString();
                    //}
                    string price = dt.Rows[i]["Price"].ToString();
                    Qty = dt.Rows[i]["Qty"].ToString();
                    string rid = code;
                    string tb_qty = Qty;

                    if (squantity < Convert.ToDouble(Qty))
                    {
                        Qty = squantity.ToString();
                    }
                    QtyChecker = QtyChecker + Convert.ToDouble(Qty);
                    totPrice = totPrice + (Convert.ToDecimal(Qty) * Convert.ToDecimal(price));
                    squantity = squantity - Convert.ToDouble(Qty);
                    string upstock = (Convert.ToDouble(tb_qty) - Convert.ToDouble(Qty)).ToString();
                    if (Qty == "0")
                    {
                        //  cmd.CommandText = "UPDATE RateChange SET Qty='" + Qty + "' where R_Id='" + rid + "'";
                    }
                    else
                    {
                        cmd1.CommandText = "UPDATE RateChange SET Qty='" + upstock + "' where R_Id='" + rid + "'";
                    }
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void upStockonProfit(string code, double qty)
        {
            try
            {
                SqlCommand cmd1 = new SqlCommand();
                string item_code = code;
                double squantity = qty;
                Decimal totPrice = 0;
                double QtyChecker = 0;
                string Qty = "0";
                cmd.Connection = conn;
                cmd.CommandText = "select Price,Qty,R_Id from RateChange where Item_code='" + code + "'";
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter Adap = new SqlDataAdapter();
                Adap.SelectCommand = cmd;
                DataTable dt = new DataTable();
                Adap.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count && (squantity > 0); ++i)
                    {
                        string price = dt.Rows[i]["Price"].ToString();
                        Qty = dt.Rows[i]["Qty"].ToString();
                        string rid = dt.Rows[i]["R_Id"].ToString();
                        string tb_qty = Qty;

                        if (squantity < Convert.ToDouble(Qty))
                        {
                            Qty = squantity.ToString();
                        }
                        QtyChecker = QtyChecker + Convert.ToDouble(Qty);
                        totPrice = totPrice + (Convert.ToDecimal(Qty) * Convert.ToDecimal(price));
                        squantity = squantity - Convert.ToInt32(Qty);
                        string upstock = (Convert.ToDouble(tb_qty) - Convert.ToDouble(Qty)).ToString();
                        cmd1.Connection = conn;
                        if (Qty != "0")
                        {
                            cmd1.CommandText = "UPDATE RateChange SET Qty='" + upstock + "' where R_Id='" + rid + "'";
                        }

                        cmd1.CommandType = CommandType.Text;
                        conn.Open();
                        cmd1.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
            catch(Exception ex)
            {
                conn.Close();
            }
        }
        
        public void DownStockonProfit(string code, double qty)
        {
            string item_code = code;
            double squantity = qty;
            double totPrice = 0;
            double QtyChecker = 0;
            double Qty = 0;
            cmd.CommandText = "select Price,Qty,R_Id from RateChange where Item_code='" + code + "'";
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter Adap = new SqlDataAdapter();
            Adap.SelectCommand = cmd;
            DataTable dt = new DataTable();
            Adap.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count && (squantity < 0); ++i)
                {
                    string price = dt.Rows[i]["Price"].ToString();
                    Qty =Convert.ToDouble( dt.Rows[i]["Qty"]);
                    string rid = dt.Rows[i]["R_Id"].ToString();
                    double tb_qty = Qty;
                    if (Convert.ToInt32(tb_qty) != 0)
                    {
                        double sumqty = Math.Abs(squantity);
                        QtyChecker = QtyChecker + Convert.ToDouble(Qty);
                        totPrice = totPrice + (Convert.ToDouble(Qty) * Convert.ToDouble(price));
                        squantity = squantity + sumqty;
                        string upstock = (Convert.ToDouble(sumqty) + Convert.ToDouble(Qty)).ToString();
                        if (Qty == 0)
                        {
                            cmd.CommandText = "UPDATE RateChange SET Qty='" + Qty + "' where R_Id='" + rid + "'";
                        }
                        else
                        {

                            cmd.CommandText = "UPDATE RateChange SET Qty='" + upstock + "' where R_Id='" + rid + "'";
                        }
                    }
                    conn.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
        
        public bool Validation()
        {
            bool retval = false;
            return retval;
        }

        public bool IsActive()
        {
            string result = "";
            conn.Open();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT CreditActive FROM REC_CUSTOMER where CODE='" + CUSTOMER_CODE.Text + "'";
            result = Convert.ToString(cmd.ExecuteScalar());
            conn.Close();

            if (result == "True")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        public void modifiedtransaction()
        {

            modtrans.VOUCHERTYPE = "SALES Normal";

            modtrans.Date = DOC_DATE_GRE.Value.ToString("MM/dd/yyyy");
            modtrans.USERID = lg.EmpId;
            modtrans.VOUCHERNO = DOC_NO.Text;
            modtrans.NARRATION = NOTES.Text;
            modtrans.STATUS = "Update";
            modtrans.MODIFIEDDATE = DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss");
            modtrans.BRANCH = lg.Branch;
            modtrans.INVOICENO = VOUCHNUM.Text;
            modtrans.insertTransaction();
        }

        #region transactions
        private void DeleteTransation()
        {

            try
            {
                trans.VOUCHERTYPE = "SALES Normal";
                trans.VOUCHERNO = DOC_NO.Text;
                trans.DeletePurchaseTransaction();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }


        }

        public void CessTransaction()
        {
            if (Convert.ToDouble(TXT_CESS.Text) > 0)
            {
                trans.VOUCHERTYPE = "SALES Normal";
                trans.DATED = TransDate.ToString("MM/dd/yyyy");
                trans.NARRATION = NOTES.Text;
                Login log = (Login)Application.OpenForms["Login"];
                trans.USERID = log.EmpId;
                trans.NARRATION = NOTES.Text;
                trans.ACCNAME = "SL A/C";
                trans.PARTICULARS = "OUTPUT CESS";
                trans.VOUCHERNO = DOC_NO.Text;
                trans.ACCID = "101";
                trans.CREDIT = "0";
                trans.BRANCH = lg.Branch;
                trans.DEBIT = TXT_CESS.Text;
                trans.SYSTEMTIME = DateTime.Now.ToString();
                trans.insertTransaction();
                trans.PARTICULARS = "SL A/C";
                trans.ACCNAME = "OUTPUT CESS";
                trans.VOUCHERNO = DOC_NO.Text;
                trans.ACCID = "79";
                trans.BRANCH = lg.Branch;
                trans.DEBIT = "0";
                trans.CREDIT = TXT_CESS.Text;
                trans.SYSTEMTIME = DateTime.Now.ToString();
                trans.insertTransaction();
            }
        }
        
        public void RoundOFFTransaction()
        {
            if (Convert.ToDouble(txtRoundOff.Text) > 0)
            {
                trans.VOUCHERTYPE = "SALES " + cmbInvType.Text;
                trans.DATED = TransDate.ToString("MM/dd/yyyy");
                trans.NARRATION = NOTES.Text;
                Login log = (Login)Application.OpenForms["Login"];
                trans.USERID = log.EmpId;
                trans.NARRATION = NOTES.Text;
                trans.ACCNAME = "SL A/C";
                trans.PARTICULARS = "ROUND OFF";
                trans.VOUCHERNO = DOC_NO.Text;
                trans.ACCID = "101";
                trans.CREDIT = "0";
                trans.BRANCH = lg.Branch;
                trans.DEBIT = txtRoundOff.Text;
                trans.SYSTEMTIME = DateTime.Now.ToString();
                trans.insertTransaction();
                trans.PARTICULARS = "SL A/C";
                trans.ACCNAME = "ROUND OFF";
                trans.VOUCHERNO = DOC_NO.Text;
                trans.ACCID = "92";
                trans.BRANCH = lg.Branch;
                trans.DEBIT = "0";
                trans.CREDIT = txtRoundOff.Text;
                trans.SYSTEMTIME = DateTime.Now.ToString();
                trans.insertTransaction();
            }
        }

        public void DiscountTransaction()
        {
            if (Convert.ToDouble(DISCOUNT.Text) > 0)
            {
                trans.VOUCHERTYPE = "SALES " + cmbInvType.Text;
                trans.DATED = TransDate.ToString("MM/dd/yyyy");
                trans.NARRATION = NOTES.Text;
                Login log = (Login)Application.OpenForms["Login"];
                trans.USERID = log.EmpId;
                trans.NARRATION = NOTES.Text;
                trans.ACCNAME = "DISCOUNT GIVEN";
                trans.PARTICULARS = "SL A/C";
                trans.VOUCHERNO = DOC_NO.Text;
                trans.ACCID = "53";
                trans.CREDIT = "0";
                trans.BRANCH = lg.Branch;
                trans.DEBIT = DISCOUNT.Text;
                trans.SYSTEMTIME = DateTime.Now.ToString();
                trans.insertTransaction();
                trans.PARTICULARS = "DISCOUNT GIVEN";
                trans.ACCNAME = "SL A/C";
                trans.VOUCHERNO = DOC_NO.Text;
                trans.ACCID = "101";
                trans.BRANCH = lg.Branch;
                trans.DEBIT = "0";
                trans.CREDIT = DISCOUNT.Text;
                trans.SYSTEMTIME = DateTime.Now.ToString();
                trans.insertTransaction();
            }
        }
        
        public void FreightTransaction()
        {
            if (Convert.ToDouble(txtFreight.Text) > 0)
            {
                trans.VOUCHERTYPE = "SALES " + cmbInvType.Text;
                trans.DATED = TransDate.ToString("MM/dd/yyyy");
                trans.NARRATION = NOTES.Text;
                Login log = (Login)Application.OpenForms["Login"];
                trans.USERID = log.EmpId;
                trans.NARRATION = NOTES.Text;
                trans.ACCNAME = "SL A/C";
                trans.PARTICULARS = "FREIGHT ON SALES";
                trans.VOUCHERNO = DOC_NO.Text;
                trans.ACCID = "101";
                trans.CREDIT = "0";
                trans.BRANCH = lg.Branch;
                trans.DEBIT = txtFreight.Text;
                trans.SYSTEMTIME = DateTime.Now.ToString();
                trans.insertTransaction();
                trans.PARTICULARS = "SL A/C";
                trans.ACCNAME = "FREIGHT ON SALES";
                trans.VOUCHERNO = DOC_NO.Text;
                trans.ACCID = "60";
                trans.BRANCH = lg.Branch;
                trans.DEBIT = "0";
                trans.CREDIT = txtFreight.Text;
                trans.SYSTEMTIME = DateTime.Now.ToString();
                trans.insertTransaction();
            }
        }

        public void InsertTransaction()
        {
            trans.VOUCHERTYPE = "SALES "+cmbInvType.Text;
            trans.DATED = TransDate.ToString("MM/dd/yyyy");
            trans.NARRATION = NOTES.Text;
            Login log = (Login)Application.OpenForms["Login"];
            trans.USERID = log.EmpId;
            trans.NARRATION = NOTES.Text;
            trans.ACCNAME = CASHACC.Text;
            trans.PARTICULARS = SALESACC.Text;
            trans.VOUCHERNO = DOC_NO.Text;
            trans.BRANCH = lg.Branch;
            trans.ACCID = CASHACC.SelectedValue.ToString();
            trans.CREDIT = "0";
            trans.DEBIT = NET_AMOUNT.Text;
            trans.SYSTEMTIME = DateTime.Now.ToString();
            trans.insertTransaction();
            trans.PARTICULARS = CASHACC.Text;
            trans.ACCNAME = SALESACC.Text;
            trans.VOUCHERNO = DOC_NO.Text;
            trans.ACCID = SALESACC.SelectedValue.ToString();
            trans.DEBIT = "0";
            trans.CREDIT = NET_AMOUNT.Text;
            trans.SYSTEMTIME = DateTime.Now.ToString();
            trans.insertTransaction();
        }

        public void VATTransaction()
        {
            if (Convert.ToDouble(TAX_TOTAL.Text) > 0)
            {
                trans.VOUCHERTYPE = "SALES " + cmbInvType.Text;
                trans.DATED = TransDate.ToString("MM/dd/yyyy");
                trans.NARRATION = NOTES.Text;
                Login log = (Login)Application.OpenForms["Login"];
                trans.USERID = log.EmpId;
                trans.NARRATION = NOTES.Text;
                trans.ACCNAME = "SL A/C";
                trans.PARTICULARS = "OUTPUT "+TAXTYPE;
                trans.VOUCHERNO = DOC_NO.Text;
                trans.ACCID = "101";
                trans.CREDIT = "0";
                trans.BRANCH = lg.Branch;
                trans.DEBIT = TAX_TOTAL.Text;
                trans.SYSTEMTIME = DateTime.Now.ToString();
                trans.insertTransaction();
                trans.PARTICULARS = "SL A/C";
                trans.ACCNAME = "OUTPUT " + TAXTYPE;
                trans.VOUCHERNO = DOC_NO.Text;
                trans.ACCID = "83";
                trans.BRANCH = lg.Branch;
                trans.DEBIT = "0";
                trans.CREDIT = TAX_TOTAL.Text;
                trans.SYSTEMTIME = DateTime.Now.ToString();
                trans.insertTransaction();
            }
        }

        #endregion

        public void ReceiptVoucher()
        {
            GetMaxPayVouch();
            string branch = ComSet.ReadBranch();
            if (ID == "")
            {
                if (txtcashrcvd.Text == "")
                    txtcashrcvd.Text = "0";
                if (type == "SAL.CRD" && Convert.ToDecimal(txtcashrcvd.Text) > 0)
                {
                    VoucherNumber = General.generateReceiptVoucherCode();
                    if (PAY_CODE.Text == "CSH")
                        cmd.CommandText = "INSERT INTO REC_RECEIPTVOUCHER_HDR (BRANCH,DOC_NO,REC_NO,DOC_DATE_GRE,DOC_DATE_HIJ,CUST_CODE,CUR_CODE,AMOUNT,PAY_CODE,BANK_CODE,CHQ_NO,CHQ_DATE,DEBIT_CODE,DESC1,CREDIT_CODE,DESC2,NOTES,TOTAL_PAID,TOTAL_CURRENT,TOTAL_BALANCE) VALUES('" + branch + "','" + VoucherNumber + "','" + RecMaxVoucher + "','" + DOC_DATE_GRE.Value.ToString("MM/dd/yyyy") + "','" + DOC_DATE_HIJ.Text + "','" + CUSTOMER_CODE.Text + "','" + CURRENCY_CODE.SelectedValue + "','" + txtcashrcvd.Text + "','" + PAY_CODE.Text + "','" + " " + "','" + "" + "','" + "" + "',21,'CASH ACCOUNT','" + CASHACC.SelectedValue + "','" + CASHACC.Text + "','" + NOTES.Text + "','" + txtcashrcvd.Text + "','" + NET_AMOUNT.Text + "','" + NET_AMOUNT.Text + "')";
                    else if (PAY_CODE.Text == "CHQ")
                        cmd.CommandText = "INSERT INTO REC_RECEIPTVOUCHER_HDR (BRANCH,DOC_NO,REC_NO,DOC_DATE_GRE,DOC_DATE_HIJ,CUST_CODE,CUR_CODE,AMOUNT,PAY_CODE,BANK_CODE,CHQ_NO,CHQ_DATE,DEBIT_CODE,DESC1,CREDIT_CODE,DESC2,NOTES,TOTAL_PAID,TOTAL_CURRENT,TOTAL_BALANCE) VALUES('" + branch + "','" + VoucherNumber + "','" + RecMaxVoucher + "','" + DOC_DATE_GRE.Value.ToString("MM/dd/yyyy") + "','" + DOC_DATE_HIJ.Text + "','" + CUSTOMER_CODE.Text + "','" + CURRENCY_CODE.SelectedValue + "','" + txtcashrcvd.Text + "','" + PAY_CODE.Text + "','" + CASHACC.Text + "','" + CARD_NO.Text + "','" + CHQ_DATE.Value.ToString("MM/dd/yyyy") + "',21,'CASH ACCOUNT','" + CASHACC.SelectedValue + "','" + CASHACC.Text + "','" + NOTES.Text + "','" + txtcashrcvd.Text + "','" + NET_AMOUNT.Text + "','" + NET_AMOUNT.Text + "')";
                    else
                        cmd.CommandText = "INSERT INTO REC_RECEIPTVOUCHER_HDR (BRANCH,DOC_NO,REC_NO,DOC_DATE_GRE,DOC_DATE_HIJ,CUST_CODE,CUR_CODE,AMOUNT,PAY_CODE,BANK_CODE,CHQ_NO,CHQ_DATE,DEBIT_CODE,DESC1,CREDIT_CODE,DESC2,NOTES,TOTAL_PAID,TOTAL_CURRENT,TOTAL_BALANCE) VALUES('" + branch + "','" + VoucherNumber + "','" + RecMaxVoucher + "','" + DOC_DATE_GRE.Value.ToString("MM/dd/yyyy") + "','" + DOC_DATE_HIJ.Text + "','" + CUSTOMER_CODE.Text + "','" + CURRENCY_CODE.SelectedValue + "','" + txtcashrcvd.Text + "','" + PAY_CODE.Text + "','" + CASHACC.Text + "','" + CARD_NO.Text + "','" + "" + "',21,'CASH ACCOUNT','" + CASHACC.SelectedValue + "','" + CASHACC.Text + "','" + NOTES.Text + "','" + txtcashrcvd.Text + "','" + NET_AMOUNT.Text + "','" + NET_AMOUNT.Text + "')";

                    cmd.CommandType = CommandType.Text;
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    receiptVoucherTransaction(VoucherNumber);

                }
            }
        }
        string RecMaxVoucher = "";
        void GetMaxPayVouch()
        {
            int maxId;
            String value;

            cmd.CommandText = "SELECT ISNULL(MAX(CONVERT(DECIMAL(18,0),REC_NO)), 0) FROM REC_RECEIPTVOUCHER_HDR";
            cmd.CommandType = CommandType.Text;
            conn.Open();
            value = Convert.ToString(cmd.ExecuteScalar());
            conn.Close();


            if (value.Equals("0"))
            {
                cmd.CommandText = "SELECT VouchStartFrom FROM GEN_VOUCH_STARTFROM WHERE VouchTypeCode='PAY'";
                cmd.CommandType = CommandType.Text;
                conn.Open();
                RecMaxVoucher = Convert.ToString(cmd.ExecuteScalar());
                conn.Close();
            }
            else
            {
                maxId = Convert.ToInt32(value);
                RecMaxVoucher = (maxId + 1).ToString();
            }
        }
        public void receiptVoucherTransaction(string recVoucherNo)
        {
            trans.VOUCHERTYPE = "Cash Receipt";
            trans.DATED = TransDate.ToString("MM/dd/yyyy");
            trans.NARRATION = NOTES.Text;
            Login log = (Login)Application.OpenForms["Login"];
            trans.USERID = log.EmpId;
            trans.ACCNAME = "CASH ACCOUNT";
            trans.PARTICULARS = CASHACC.Text;
            trans.VOUCHERNO = recVoucherNo;

            trans.ACCID = "21";
            trans.CREDIT = "0";
            trans.BRANCH = lg.Branch;
            trans.DEBIT = txtcashrcvd.Text;
            trans.SYSTEMTIME = DateTime.Now.ToString();
            trans.insertTransaction();
            trans.PARTICULARS = "CASH ACCOUNT";
            trans.ACCNAME = CASHACC.Text;
            trans.VOUCHERNO = recVoucherNo;

            trans.ACCID = CASHACC.SelectedValue.ToString();
            trans.BRANCH = lg.Branch;

            trans.DEBIT = "0";
            trans.CREDIT = txtcashrcvd.Text;
            trans.SYSTEMTIME = DateTime.Now.ToString();
            trans.insertTransaction();
        }
        
        public void Clear2()
        {
            ID = "";
            DOC_NO.Text = "";
            pricefob = 0;
            sales_price = 0;
            txtRoundOff.Text = decimalFormat;
            txtFreight.Text = decimalFormat;
            TAX_TOTAL.Text = decimalFormat;
            VATT.Text = decimalFormat;
            TOTAL_AMOUNT.Text = decimalFormat;
            DISCOUNT.Text = decimalFormat;
            NET_AMOUNT.Text = decimalFormat;
            txtcashrcvd.Text = decimalFormat;
            TXT_CESS.Text = decimalFormat;
            txtGrossTotal.Text = decimalFormat;
            txtLineDiscTotal.Text = decimalFormat;
            txtNetTotal.Text = decimalFormat;
            txtTaxTotal.Text = decimalFormat;
            txtItemTotal.Text = decimalFormat;

            grossTotal = 0;
            discTotal = 0;
            netTotal = 0;
            taxTotal = 0;
            itemTotal = 0;

            CUSTOMER_CODE.Text = "";
            CUSTOMER_NAME.Text = "";
            SALESMAN_CODE.Text = "";
            SALESMAN_NAME.Text = "";
            DOC_DATE_GRE.Value = DateTime.Today;
            DOC_DATE_HIJ.Text = "";
            DOC_REFERENCE.Text = "";
            CURRENCY_CODE.Text = "";
            clearItem();
            dgItems.Rows.Clear();
            //PAY_CODE.Text = "";
            //PAY_NAME.Text = "";
            CARD_NO.Text = "";
            NOTES.Text = "";
            //VoucherNumber = "";
            CASHACC.Text = "CASH ACCOUNT";
            SALESACC.Text = "SALES ACCOUNT";
            chkCustomeleveldiscount.Enabled = false;
            chkCustomeleveldiscount.Checked = false;
            ActiveForm = false;
            ShowStock = false;
            Disctext.Text = "Disc Amt";
            DiscType = "Amount";
           
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
             m = 0;
             k = 0;
             n = 0;
            cntr = 0;
            ID = "";
            DOC_NO.Text = "";
            pricefob = 0;
            txtRoundOff.Text = decimalFormat;
            txtFreight.Text = decimalFormat;
            TAX_TOTAL.Text = decimalFormat;
            VATT.Text = decimalFormat;
            TOTAL_AMOUNT.Text = decimalFormat;
            DISCOUNT.Text = decimalFormat;
            NET_AMOUNT.Text = decimalFormat;
            txtcashrcvd.Text = decimalFormat;
            TXT_CESS.Text = decimalFormat;
            txtGrossTotal.Text = decimalFormat;
            txtLineDiscTotal.Text = decimalFormat;
            txtNetTotal.Text = decimalFormat;
            txtTaxTotal.Text = decimalFormat;
            txtItemTotal.Text = decimalFormat;
            tb_mrp.Text = decimalFormat;
            
            txtfree.Text = "0";
            grossTotal = 0;
            discTotal = 0;
            netTotal = 0;
            taxTotal = 0;
            itemTotal = 0;
            sales_price = 0;
            CUSTOMER_CODE.Text = "";
            CUSTOMER_NAME.Text = "";
            SALESMAN_CODE.Text = "";
            SALESMAN_NAME.Text = "";
            DOC_DATE_GRE.Value = DateTime.Today;
            DOC_DATE_HIJ.Text = "";
            DOC_REFERENCE.Text = "";
            CURRENCY_CODE.Text = "";
            txtCreditNoteNo.Text = "";
            tb_warty.Text = "";
            tb_WarantyPrd.Text = "";
            if (PNL_DATAGRIDITEM.Visible == true)
            {
                PNL_DATAGRIDITEM.Visible = false;
            }
            txtfree.Text = "";
            //tb_mrp.Text = "0.00";
            dgItems.Rows.Clear();
            //PAY_CODE.Text = "";
            //PAY_NAME.Text = "";
            CARD_NO.Text = "";
            NOTES.Text = "";
            ADDRESS_A = "";
            tin_no = "";
            phone_no = "";
            tele_no = "";
            VoucherNumber = "";
            CASHACC.Text = "CASH ACCOUNT";
            SALESACC.Text = "SALES ACCOUNT";
            chkCustomeleveldiscount.Enabled = false;
            chkCustomeleveldiscount.Checked = false;
            ActiveForm = false;
            GetMaxDocID();
            ShowStock = false;
            pagewidth = 0;
            pageheight = 0;
            defaultheight = 0;
            itemlength = 0;
            printeditems = 0;
            tb_descr.Text = "";
            includechang = false;
            excludechanged = false;
            checkvoucher(Convert.ToInt16(VOUCHNUM.Text));
            clearItem();
            BindBatchTable();
            Disctext.Text = "Disc Amt";
            DiscType = "Amount";
            tabPage1.AutoScrollPosition = new Point(0, 0);
           
            this.Sales_Load(sender, e);
          
            PaymentPanel.Visible = false;
        }
        
        bool oldbill;
        public void printPreview_PrintClick(object sender, EventArgs e)
        {
            try
            {
                PrintDialog printdialog = new PrintDialog();
                //printdialog.Document = printDocument1;
                printdialog.AllowSelection = true;
                printdialog.AllowSomePages = true;
                
                if (chkMultiple.Checked)
                {
                    if (printdialog.ShowDialog() == DialogResult.OK)
                    {
                        if (PrintPage.SelectedIndex == 4)
                        {
                            PrintDocument doc = new PrintDocument();                           
                            m = 0;
                            tgrossrate = 0; ttaxva = 0; trate = 0; tcdis = 0; ttaxbl = 0; tfree = 0;
                            tqty = 0; printeditems = 0;
                            doc.PrintPage += new PrintPageEventHandler(printDocumentA4Form8_PrintPage);                           
                            doc.Print();                         
                        }

                        if (PrintPage.SelectedIndex == 5)
                        {
                            PrintDocument doc = new PrintDocument();
                            m = 0;
                            tgrossrate = 0; ttaxva = 0; trate = 0; tcdis = 0; ttaxbl = 0; tfree = 0;
                            tqty = 0; printeditems = 0;
                            doc.PrintPage += new PrintPageEventHandler(printDocumentA8B_PrintPage);
                            doc.Print();                    
                        }

                        if (PrintPage.SelectedIndex == 7)
                        {
                            PrintDocument doc = new PrintDocument();
                            m = 0;
                            tgrossrate = 0; ttaxva = 0; trate = 0; tcdis = 0; ttaxbl = 0; tfree = 0;
                            tqty = 0; printeditems = 0;
                            doc.PrintPage += new PrintPageEventHandler(Estimate_PrintPage);
                            doc.Print();
                        }

                    }
                }
                else
                {
                    if (PrintPage.SelectedIndex == 4)
                    {                       
                        PrintDocument doc = new PrintDocument();
                        m = 0;
                        tgrossrate = 0; ttaxva = 0; trate = 0; tcdis = 0; ttaxbl = 0; tfree = 0;
                        tqty = 0; printeditems = 0;
                        doc.PrintPage += new PrintPageEventHandler(printDocumentA4Form8_PrintPage);                       
                        doc.Print();
                    }

                    if (PrintPage.SelectedIndex == 5)
                    {
                        PrintDocument doc = new PrintDocument();
                        m = 0;
                        tgrossrate = 0; ttaxva = 0; trate = 0; tcdis = 0; ttaxbl = 0; tfree = 0;
                        tqty = 0; printeditems = 0;
                        doc.PrintPage += new PrintPageEventHandler(printDocumentA8B_PrintPage);                 
                        doc.Print();     
                    }

                    if (PrintPage.SelectedIndex == 7)
                    {
                        PrintDocument doc = new PrintDocument();
                        m = 0;
                        tgrossrate = 0; ttaxva = 0; trate = 0; tcdis = 0; ttaxbl = 0; tfree = 0;
                        tqty = 0; printeditems = 0;
                        doc.PrintPage += new PrintPageEventHandler(Estimate_PrintPage);
                        doc.Print();       
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ToString());
            }
        }
        
        public void printingrecipt()
        {
            try
            {

                int height = 0;
                if (dgItems.Rows.Count <= 10)
                {
                    height = (dgItems.Rows.Count) * 23;
                }
                if (dgItems.Rows.Count >= 11 && dgItems.Rows.Count <= 20)
                {
                    height = (dgItems.Rows.Count) * 30;
                }
                if (dgItems.Rows.Count > 20)
                {
                    height = (dgItems.Rows.Count) * 25;
                }
                // PrintDialog printdialog = new PrintDialog();
                //                Small Size
                //Medium Size
                //A4
                //Dynamic
                //Half
                //GST
                //Estimate
                //Estimate Half
                PrintDocument printDocument = new PrintDocument();
                PrintPreviewDialog prvdlg = new PrintPreviewDialog();
                if (PrintPage.SelectedItem.ToString() == "Small Size")
                {
                    printDocument.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("smallzize", 360, height + 170);
                    //printDialog1.Document = printDocument;
                    printDocument.PrintPage += printDocument_PrintPage;
                    //printDocument.Print();
                    //  DialogResult result = printDialog1.ShowDialog();
                    // if (result == DialogResult.OK)                 
                    prvdlg.Document = printDocument;
                    ((ToolStripButton)((ToolStrip)prvdlg.Controls[1]).Items[0]).Click += printPreview_PrintClick;
                    prvdlg.ShowDialog();
                    // printDialog2.ShowDialog();                 

                    //}
                }

                    //medium Size
                else if (PrintPage.SelectedItem.ToString() == "Medium Size")
                {
                    if (hasArabic)
                    {
                        PrintDocument printDocumentMediumSizearabic = new PrintDocument();
                        PrintPreviewDialog prvdlg1 = new PrintPreviewDialog();
                        // Printing.PrintA4();
                        PaperSize ps = new PaperSize();
                        ps.RawKind = (int)PaperKind.A4;
                        printDocumentMediumSizearabic.DefaultPageSettings.PaperSize = ps;
                        printDialog1.Document = printDocumentMediumSizearabic;
                        printDocumentMediumSizearabic.PrintPage += printDocumentMediumSize_PrintPageabic;
                        // printDocument.Print();
                        // DialogResult result = printDialog1.ShowDialog();
                        //  if (result == DialogResult.OK)
                        //  {
                        prvdlg.Document = printDocumentMediumSizearabic;
                        ((ToolStripButton)((ToolStrip)prvdlg.Controls[1]).Items[0]).Click += printPreview_PrintClick;
                        prvdlg.ShowDialog();
                        // printDocumentMediumSizearabic.Print();
                    }
                    else
                    {
                        PrintDocument printDocumentMediumSize = new PrintDocument();
                        PrintPreviewDialog prvdlg1 = new PrintPreviewDialog();
                        // Printing.PrintA4();
                        // printDocumentMediumSize.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("MediumSize", 540, height + 450);
                        printDocumentMediumSize.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("MediumSize", 914, height + 450);
                        printDialog1.Document = printDocumentMediumSize;
                        printDocumentMediumSize.PrintPage += printDocumentMediumSize_PrintPage;
                        // printDocument.Print();
                        // DialogResult result = printDialog1.ShowDialog();
                        //  if (result == DialogResult.OK)
                        //  {
                        prvdlg1.Document = printDocumentMediumSize;
                        ((ToolStripButton)((ToolStrip)prvdlg1.Controls[1]).Items[0]).Click += printPreview_PrintClick;
                        prvdlg1.ShowDialog();
                        //printDocumentMediumSize.Print();
                    }
                }
                else if (PrintPage.SelectedItem.ToString() == "A4")
                {
                    PrintDocument printDocumentA4 = new PrintDocument();
                    PrintPreviewDialog prvdlg2 = new PrintPreviewDialog();
                    // Printing.PrintA4();
                    PaperSize ps = new PaperSize();
                    ps.RawKind = (int)PaperKind.A4;
                    printDocumentA4.DefaultPageSettings.PaperSize = ps;
                    printDocumentA4.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("A4", 840, 1188);
                    printDialog1.Document = printDocumentA4;
                    printDocumentA4.PrintPage += printDocumentA4_PrintPage;
                    // printDocument.Print();
                    // DialogResult result = printDialog1.ShowDialog();
                    //  if (result == DialogResult.OK)
                    //  {
                    prvdlg2.Document = printDocumentA4;
                    ((ToolStripButton)((ToolStrip)prvdlg2.Controls[1]).Items[0]).Click += printPreview_PrintClick;
                    prvdlg2.ShowDialog();
                    //printDocumentA4.Print();
                }
                else if (PrintPage.SelectedItem.ToString() == "Dynamic")
                {
                    GetDynamicPageSetting();
                    PrintDocument printDocumentDynamic = new PrintDocument();
                    PrintPreviewDialog prvdlg3 = new PrintPreviewDialog();
                    // Printing.PrintA4();
                    if (fixedheight)
                    {
                        printDocumentDynamic.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Dynamic", pagewidth, pageheight);
                    }
                    else
                    {
                        printDocumentDynamic.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Dynamic", pagewidth, pageheight + defaultheight);
                    }

                    printDialog1.Document = printDocumentDynamic;
                    printDocumentDynamic.PrintPage += printDocumentDynamic_PrintPage;
                    // printDocument.Print();
                    // DialogResult result = printDialog1.ShowDialog();
                    //  if (result == DialogResult.OK)
                    //  {
                    prvdlg3.Document = printDocumentDynamic;
                    ((ToolStripButton)((ToolStrip)prvdlg3.Controls[1]).Items[0]).Click += printPreview_PrintClick;
                    prvdlg3.ShowDialog();
                    // printDocumentDynamic.Print();
                }
                else if (PrintPage.SelectedItem.ToString() == "Form8")
                {
                    counter = 0;
                    PrintDocument doc = new PrintDocument();
                    PrintPreviewDialog prvdlg4 = new PrintPreviewDialog();
                    ToolStripButton b = new ToolStripButton();
                    b.Image = Properties.Resources.PrintIcon;
                    b.DisplayStyle = ToolStripItemDisplayStyle.Image;

                    ((ToolStrip)(prvdlg4.Controls[1])).Items.RemoveAt(0);
                    ((ToolStrip)(prvdlg4.Controls[1])).Items.Insert(0, b);
                    
                    PaperSize ps = new PaperSize();
                    ps.RawKind = (int)PaperKind.A4;
                    //doc.DefaultPageSettings.PaperSize = ps;
                    doc.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Form8", 840, 1188);
                    m = 0;
                    tgrossrate = 0; ttaxva = 0; trate = 0; tcdis = 0; ttaxbl = 0; tfree = 0;
                    tqty = 0; printeditems = 0;
                    if (!flagPrintEventAssigned)
                    {

                        doc.PrintPage += new PrintPageEventHandler(printDocumentA4Form8_PrintPage);
                        flagPrintEventAssigned = true;
                        counter++;
                    }
                    if (counter > 0)
                    {
                        flagPrintEventAssigned = false;
                    }                                                                      
                    prvdlg4.Document = doc; 
                    b.Click += printPreview_PrintClick;                   
                    prvdlg4.ShowDialog();
   
                }
                else if (PrintPage.SelectedItem.ToString() == "GST")
                {
                    counter = 0;
                    PrintDocument doc = new PrintDocument();
                    PrintPreviewDialog prvdlg5 = new PrintPreviewDialog();
                    ToolStripButton b = new ToolStripButton();
                    b.Image = Properties.Resources.PrintIcon;
                    b.DisplayStyle = ToolStripItemDisplayStyle.Image;
                    b.Click += printPreview_PrintClick;
                    ((ToolStrip)(prvdlg5.Controls[1])).Items.RemoveAt(0);
                    ((ToolStrip)(prvdlg5.Controls[1])).Items.Insert(0, b);
                    doc.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Form8B", 840, 1188);
                    
                    if (!flagPrintEventAssigned)
                    {
                        doc.PrintPage += printDocumentA8B_PrintPage;
                        flagPrintEventAssigned = true;
                        counter++;
                    }
                    if (counter > 0)
                    {
                        flagPrintEventAssigned = false;
                    }
                    m = 0;
                    tgrossrate = 0; ttaxva = 0; trate = 0; tcdis = 0; ttaxbl = 0; tfree = 0;
                    tqty = 0; printeditems = 0;                    
                    prvdlg5.Document = doc;                   
                    prvdlg5.ShowDialog();
                    
                    

                }
                else if (PrintPage.SelectedItem.ToString() == "VAT")
                {
                    counter = 0;
                    PrintDocument doc = new PrintDocument();
                    PrintPreviewDialog prvdlg5 = new PrintPreviewDialog();
                    ToolStripButton b = new ToolStripButton();
                    b.Image = Properties.Resources.PrintIcon;
                    b.DisplayStyle = ToolStripItemDisplayStyle.Image;
                    b.Click += printPreview_PrintClick;
                    ((ToolStrip)(prvdlg5.Controls[1])).Items.RemoveAt(0);
                    ((ToolStrip)(prvdlg5.Controls[1])).Items.Insert(0, b);
                    doc.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Form8B", 840, 1188);

                    if (!flagPrintEventAssigned)
                    {
                        doc.PrintPage += printDocumentA8B_PrintPage;
                        flagPrintEventAssigned = true;
                        counter++;
                    }
                    if (counter > 0)
                    {
                        flagPrintEventAssigned = false;
                    }
                    m = 0;
                    tgrossrate = 0; ttaxva = 0; trate = 0; tcdis = 0; ttaxbl = 0; tfree = 0;
                    tqty = 0; printeditems = 0;
                    prvdlg5.Document = doc;
                    prvdlg5.ShowDialog();



                }
                else if (PrintPage.SelectedItem.ToString() == "Half")
                {
                    height = (dgItems.Rows.Count) * 13;
                    PrintDocument printDocumentHalfSize = new PrintDocument();
                    PrintPreviewDialog prvdlg6 = new PrintPreviewDialog();
                    
                    printDocumentHalfSize.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("HaldfSize", height + 800, 590);
                    printDialog1.Document = printDocumentHalfSize;
                    printDocumentHalfSize.PrintPage += printDocumentHalf_PrintPage;
                    
                    prvdlg6.Document = printDocumentHalfSize;
                    ((ToolStripButton)((ToolStrip)prvdlg6.Controls[1]).Items[0]).Click += printPreview_PrintClick;
                    prvdlg6.ShowDialog();
                    
                }
                else if (PrintPage.SelectedItem.ToString() == "Estimate")
                {

                    counter = 0;
                    PrintDocument doc = new PrintDocument();
                    PrintPreviewDialog prvdlg7 = new PrintPreviewDialog();
                    ToolStripButton b = new ToolStripButton();
                    b.Image = Properties.Resources.PrintIcon;
                    b.DisplayStyle = ToolStripItemDisplayStyle.Image;
                    b.Click += printPreview_PrintClick;
                    ((ToolStrip)(prvdlg7.Controls[1])).Items.RemoveAt(0);
                    ((ToolStrip)(prvdlg7.Controls[1])).Items.Insert(0, b);
                                   
                    doc.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Estimate", 840, 1188);
                    //printDialog1.Document = printDocumentA4;
                    if (!flagPrintEventAssigned)
                    {
                        doc.PrintPage += Estimate_PrintPage;
                        flagPrintEventAssigned = true;
                        counter++;
                    }
                    // printDocumentA4estimate.PrintPage += Estimate_PrintPage;
                    if (counter > 0)
                    {
                        flagPrintEventAssigned = false;
                    }
                    m = 0;
                    tgrossrate = 0; ttaxva = 0; trate = 0; tcdis = 0; ttaxbl = 0; tfree = 0;
                    tqty = 0; printeditems = 0;                    
                    prvdlg7.Document = doc;                   
                    prvdlg7.ShowDialog();
                   
                }
                else if (PrintPage.SelectedItem.ToString() == "Estimate Half")
                {
                    printeditems = 0;
                    k = 0;
                    tgrossrate = 0; ttaxva = 0; trate = 0; tcdis = 0; ttaxbl = 0; tfree = 0;
                    tqty = 0;
                    height = (dgItems.Rows.Count) * 13;
                    PrintDocument printDocumentHalfSize = new PrintDocument();
                    PrintPreviewDialog prvdlg8 = new PrintPreviewDialog();
                    // Printing.PrintA4();
                    printDocumentHalfSize.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Estimate Half", height + 800, 580);
                    printDialog1.Document = printDocumentHalfSize;
                    printDocumentHalfSize.PrintPage += customizedprivw;
                  
                    // printDocument.Print();
                    // DialogResult result = printDialog1.ShowDialog();
                    //  if (result == DialogResult.OK)
                    //  {
                    prvdlg8.Document = printDocumentHalfSize;
                    ((ToolStripButton)((ToolStrip)prvdlg8.Controls[1]).Items[0]).Click += printPreview_PrintClick;
                    prvdlg8.ShowDialog();
                    m = 0;
                    k = 0;
                    tgrossrate = 0; ttaxva = 0; trate = 0; tcdis = 0; ttaxbl = 0; tfree = 0;
                    tqty = 0;
                    //printDocumentHalfSize.Print();
                }
                if (PrintPage.SelectedItem.ToString() == "Thermal")
                {


                    int i = 1;
                    foreach (DataGridViewRow row in dgItems.Rows)
                    {

                        if (row.Cells[1].Value.ToString().Length <= 28)
                        {
                            i++;
                        }
                        else
                        {
                            i += 2;
                        }
                    }
                    Font font = new Font("Courier New", 8);
                    float fontheight = font.GetHeight() + 2;
                    int hgt = Convert.ToInt32(i * fontheight);
                    printDocument.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Thermal", 300, 320 + hgt);
                
                    //printDialog1.Document = printDocument;
                    if (Properties.Settings.Default.Tax_Type.ToString() == "VAT")
                    {
                        printDocument.PrintPage += Print_thermal_VAT;
                    }
                    else
                    {
                        printDocument.PrintPage += Print_thermal;
                    }
                   
                    //printDocument.Print();
                    //  DialogResult result = printDialog1.ShowDialog();
                    // if (result == DialogResult.OK)                 
                    prvdlg.Document = printDocument;
                    ((ToolStripButton)((ToolStrip)prvdlg.Controls[1]).Items[0]).Click += printPreview_PrintClick;
                    prvdlg.ShowDialog();
                    // printDialog2.ShowDialog();                 

                    //}
                }

                else
                {
                    if (hasArabic)
                    {

                        PrintDocument printDocumentArabicA4 = new PrintDocument();
                        PrintPreviewDialog prvdlg8 = new PrintPreviewDialog();
                        // Printing.PrintA4();
                        PaperSize ps = new PaperSize();
                        ps.RawKind = (int)PaperKind.A4;
                        printDocumentArabicA4.DefaultPageSettings.PaperSize = ps;
                        printDialog1.Document = printDocumentArabicA4;
                        printDocumentArabicA4.PrintPage += printDocumentArabicA4_PrintPage;
                        // printDocument.Print();
                        // DialogResult result = printDialog1.ShowDialog();
                        //  if (result == DialogResult.OK)
                        //  {
                        prvdlg8.Document = printDocumentArabicA4;
                        ((ToolStripButton)((ToolStrip)prvdlg8.Controls[1]).Items[0]).Click += printPreview_PrintClick;
                        prvdlg8.ShowDialog();
                        //printDocumentArabicA4.Print();


                    }
                    else
                    {
                        PrintDocument printDocumentA4 = new PrintDocument();
                        PrintPreviewDialog prvdlg8 = new PrintPreviewDialog();
                        // Printing.PrintA4();
                        PaperSize ps = new PaperSize();
                        ps.RawKind = (int)PaperKind.A4;
                        printDocumentA4.DefaultPageSettings.PaperSize = ps;
                        printDialog1.Document = printDocumentA4;
                        printDocumentA4.PrintPage += printDocumentA4_PrintPage;
                        // printDocument.Print();
                        // DialogResult result = printDialog1.ShowDialog();
                        //  if (result == DialogResult.OK)
                        //  {
                        prvdlg8.Document = printDocumentA4;
                        ((ToolStripButton)((ToolStrip)prvdlg8.Controls[1]).Items[0]).Click += printPreview_PrintClick;
                        prvdlg8.ShowDialog();
                        //printDocumentA4.Print();


                        //  }
                    }
                }
            }
            catch
            {
                MessageBox.Show("printing Problem");
            }
        }

        bool oldbill2;
        public void printingrecipt2()
        {
            int height = 0;
            if (dgItems.Rows.Count <= 10)
            {
                height = (dgItems.Rows.Count) * 23;
            }
            if (dgItems.Rows.Count >= 11 && dgItems.Rows.Count <= 20)
            {
                height = (dgItems.Rows.Count) * 30;
            }
            if (dgItems.Rows.Count > 20)
            {
                height = (dgItems.Rows.Count) * 25;
            }
            PrintDialog printdialog = new PrintDialog();
            PrintDocument printDocument = new PrintDocument();
            if (PrintPage.SelectedIndex == 0)
            {
                if (PrintInvoice.Checked)
                {
                    printDocument.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("smallzize", 360, height + 170);
                    printDocument.PrintPage += printDocument_PrintPage;
                    printdialog.Document = printDocument;
                    if (chkMultiple.Checked)
                    {
                        if (printdialog.ShowDialog() == DialogResult.OK)
                        {
                            printDocument.Print();
                        }
                    }
                    else
                    {
                        printDocument.Print();
                    }
                    //}
                }
            }

                //medium Size
            else if (PrintPage.SelectedIndex == 1)
            {
                if (PrintInvoice.Checked)
                {
                    printDocument.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("MediumSize", 914, height + 300);

                    printDocument.PrintPage += printDocumentMediumSize_PrintPage;
                    printdialog.Document = printDocument;
                    if (chkMultiple.Checked)
                    {
                        if (printdialog.ShowDialog() == DialogResult.OK)
                        {
                            printDocument.Print();
                        }
                    }
                    else
                    {
                        printDocument.Print();
                    }

                }
            }
            else if (PrintPage.SelectedIndex == 2)
            {
                if (PrintInvoice.Checked == false)
                {

                    PaperSize ps = new PaperSize();
                    ps.RawKind = (int)PaperKind.A4;

                    printDocument.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Form8", 840, 1188);

                    printDocument.PrintPage += printDocumentA4_PrintPage;
                    printdialog.Document = printDocument;
                    if (chkMultiple.Checked)
                    {
                        if (printdialog.ShowDialog() == DialogResult.OK)
                        {
                            printDocument.Print();
                        }
                    }
                    else
                    {
                        printDocument.Print();
                    }
                }

            }
            else if (PrintPage.SelectedIndex == 3)
            {
                if (PrintInvoice.Checked == false)
                {

                    GetDynamicPageSetting();

                    if (fixedheight)
                    {
                        printDocument.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Dynamic", pagewidth, pageheight);
                    }
                    else
                    {
                        printDocument.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Dynamic", pagewidth, pageheight + defaultheight);
                    }
                    printDocument.PrintPage += printDocumentDynamic_PrintPage;
                    printdialog.Document = printDocument;
                    if (chkMultiple.Checked)
                    {
                        if (printdialog.ShowDialog() == DialogResult.OK)
                        {
                            printDocument.Print();
                        }
                    }
                    else
                    {
                        printDocument.Print();
                    }
                }


            }
            else if (PrintPage.SelectedIndex == 4)
            {
                if (PrintInvoice.Checked)
                {
                    counter = 0;
                    printDocument.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Form8", 840, 1188);
                    if (!flagPrintEventAssigned)
                    {
                        printDocument.PrintPage += printDocumentA4Form8_PrintPage;
                        flagPrintEventAssigned = true;
                        counter++;
                    }
                    if (counter > 0)
                    {
                        flagPrintEventAssigned = false;
                    }
                    printdialog.Document = printDocument;
                    if (chkMultiple.Checked)
                    {
                        if (printdialog.ShowDialog() == DialogResult.OK)
                        {
                            printDocument.Print();
                        }
                    }
                    else
                    {
                        printDocument.Print();
                    }
                    m = 0;
                    tgrossrate = 0; ttaxva = 0; trate = 0; tcdis = 0; ttaxbl = 0; tfree = 0;
                    tqty = 0;
                }
            }
            else if (PrintPage.SelectedIndex == 5)
            {
                if (PrintInvoice.Checked)
                {
                    counter = 0;
                    printDocument.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Form8B", 840, 1188);
                    if (!flagPrintEventAssigned)
                    {
                        printDocument.PrintPage += printDocumentA8B_PrintPage;
                        flagPrintEventAssigned = true;
                        counter++;
                    }
                    if (counter > 0)
                    {
                        flagPrintEventAssigned = false;
                    }
                    printdialog.Document = printDocument;
                    if (chkMultiple.Checked)
                    {
                        if (printdialog.ShowDialog() == DialogResult.OK)
                        {
                            printDocument.Print();
                        }
                    }
                    else
                    {
                        printDocument.Print();
                    }
                    m = 0;
                    tgrossrate = 0; ttaxva = 0; trate = 0; tcdis = 0; ttaxbl = 0; tfree = 0;
                    tqty = 0;

                }

            }
            else if (PrintPage.SelectedIndex == 9)
            {
                if (PrintInvoice.Checked)
                {
                    counter = 0;
                    printDocument.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Form8B", 840, 1188);
                    if (!flagPrintEventAssigned)
                    {
                        printDocument.PrintPage += printDocumentA8B_PrintPage;
                        flagPrintEventAssigned = true;
                        counter++;
                    }
                    if (counter > 0)
                    {
                        flagPrintEventAssigned = false;
                    }
                    printdialog.Document = printDocument;
                    if (chkMultiple.Checked)
                    {
                        if (printdialog.ShowDialog() == DialogResult.OK)
                        {
                            printDocument.Print();
                        }
                    }
                    else
                    {
                        printDocument.Print();
                    }
                    m = 0;
                    tgrossrate = 0; ttaxva = 0; trate = 0; tcdis = 0; ttaxbl = 0; tfree = 0;
                    tqty = 0;

                }

            }
            else if (PrintPage.SelectedIndex == 7)
            {
                printeditems = 0;
                k = 0;
                m = 0;
                tgrossrate = 0; ttaxva = 0; trate = 0; tcdis = 0; ttaxbl = 0; tfree = 0;
                tqty = 0;
                if (PrintInvoice.Checked)
                {
                   
                    height = (dgItems.Rows.Count) * 13;
                    printDocument.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Estimate Half", height + 800, 590);
                    printDocument.PrintPage +=customizedHalfEstimate;
                    printdialog.Document = printDocument;
                    if (chkMultiple.Checked)
                    {
                        if (printdialog.ShowDialog() == DialogResult.OK)
                        {
                            printDocument.Print();
                        }
                    }
                    else
                    {
                        printDocument.Print();
                    }
                   
                }

            }
            else if (PrintPage.SelectedIndex == 6)
            {
                if (PrintInvoice.Checked)
                {
                    counter = 0;
                    printDocument.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Estimate", 840, 1188);
                    if (!flagPrintEventAssigned)
                    {
                        printDocument.PrintPage += Estimate_PrintPage;
                        flagPrintEventAssigned = true;
                        counter++;
                    }
                    if (counter > 0)
                    {
                        flagPrintEventAssigned = false;
                    }
                    //printDocument.PrintPage += Estimate_PrintPage;
                    printdialog.Document = printDocument;
                    if (chkMultiple.Checked)
                    {
                        if (printdialog.ShowDialog() == DialogResult.OK)
                        {
                            printDocument.Print();
                        }
                    }
                    else
                    {
                        printDocument.Print();
                    }
                    if (counter > 0)
                    {
                        flagPrintEventAssigned = false;
                    }
                    m = 0;
                    tgrossrate = 0; ttaxva = 0; trate = 0; tcdis = 0; ttaxbl = 0; tfree = 0;
                    tqty = 0;

                }
            }
            else if (PrintPage.SelectedIndex == 8)
            {
                if (PrintInvoice.Checked)
                {
                    int i = 1;
                    foreach (DataGridViewRow row in dgItems.Rows)
                    {

                        if (row.Cells[1].Value.ToString().Length <= 28)
                        {
                            i++;
                        }
                        else
                        {
                            i += 2;
                        }
                    }
                    Font font = new Font("Courier New", 8);
                    float fontheight = font.GetHeight() + 2;
                    int hgt = Convert.ToInt32(i * fontheight);
                    printDocument.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Thermal", 300, 320 + hgt);
                    printdialog.Document = printDocument;
                  
                    if (Properties.Settings.Default.Tax_Type.ToString() == "VAT")
                    {
                        printDocument.PrintPage += Print_thermal_VAT;
                    }
                    else
                    {
                        printDocument.PrintPage += Print_thermal;
                    }
                   

                    if (chkMultiple.Checked)
                    {
                        if (printdialog.ShowDialog() == DialogResult.OK)
                        {
                            printDocument.Print();
                        }
                    }
                    else
                    {
                        printDocument.Print();
                    }
                    k = 0;
                    tgrossrate = 0; ttaxva = 0; trate = 0; tcdis = 0; ttaxbl = 0; tfree = 0;
                    tqty = 0;

                }
            }
            else
            {
                if (PrintInvoice.Checked)
                {

                    PaperSize ps = new PaperSize();
                    ps.RawKind = (int)PaperKind.A4;
                    printDocument.DefaultPageSettings.PaperSize = ps;

                    printDocumentA4.PrintPage += printDocumentA4_PrintPage;
                    printdialog.Document = printDocument;
                    if (chkMultiple.Checked)
                    {
                        if (printdialog.ShowDialog() == DialogResult.OK)
                        {
                            printDocument.Print();
                        }
                    }
                    else
                    {
                        printDocument.Print();
                    }

                }
            }
        }

        void printDocumentA4_PrintPage(object sender, PrintPageEventArgs e)
        {
            float xpos;
            int startx = 50;
            int starty = 30;
            int offset = 15;
            int headerstartposition = 55;

            int w = e.MarginBounds.Width / 2;
            int x = e.MarginBounds.Left;
            int y = e.MarginBounds.Top;
            Font Headerfont1 = new Font("Calibri", 15, FontStyle.Bold);
            Font Headerfont2 = new Font("Calibri", 10, FontStyle.Regular);
            Font printFont = new Font("Calibri", 10);
            Font printFontBold = new Font("Calibri", 10, FontStyle.Bold);
            var tabDataForeColor = Color.Black;
            int height = 100 + y;
            var txtDataWidth = e.Graphics.MeasureString(CompanyName, Headerfont1).Width;
            using (var sf = new StringFormat())
            {
                height += 15;
                sf.LineAlignment = StringAlignment.Center;
                sf.Alignment = StringAlignment.Center;
                xpos = e.MarginBounds.Left + (e.MarginBounds.Width / 2);
                e.Graphics.DrawString(CompanyName, Headerfont1, new SolidBrush(tabDataForeColor), xpos + 400, starty);
                offset = offset + 9;
                e.Graphics.DrawString(Address1 + ", " + Address2, Headerfont2, new SolidBrush(tabDataForeColor), headerstartposition, starty + offset);
                offset = offset + 20;
                e.Graphics.DrawString("Phone:".PadRight(3) + Phone, Headerfont2, new SolidBrush(tabDataForeColor), headerstartposition, starty + offset);
                offset = offset + 20;
                e.Graphics.DrawString("Email:".PadRight(3) + Email, Headerfont2, new SolidBrush(tabDataForeColor), headerstartposition, starty + offset);
                offset = offset + 20;
                e.Graphics.DrawString("Website:".PadRight(3) + Website, Headerfont2, new SolidBrush(tabDataForeColor), headerstartposition, starty + offset);
                offset = offset + 20;
                e.Graphics.DrawString("Invoice No: " + Billno, Headerfont2, new SolidBrush(tabDataForeColor), 600, starty + offset);
                offset = offset + 16;
                e.Graphics.DrawString("Tin No:" + TineNo, Headerfont2, new SolidBrush(tabDataForeColor), 600, starty + offset);
                offset = offset + 16;
                e.Graphics.DrawString("Date:" + DateTime.Now.ToString(), Headerfont2, new SolidBrush(tabDataForeColor), 600, starty + offset);
                offset = offset + 16;
                offset = offset + 16;
                Pen blackPen = new Pen(Color.Black, 1);
                Point point1 = new Point(0, 185);
                Point point2 = new Point(900, 185);
                e.Graphics.DrawLine(blackPen, point1, point2);
                e.Graphics.DrawString("To:" + CUSTOMER_NAME.Text, Headerfont2, new SolidBrush(tabDataForeColor), startx, starty + offset - 36);
                Font itemhead = new Font("Times New Roman", 8);
                offset = offset + 2;
                Point point3 = new Point(45, 219);
                Point point4 = new Point(760, 219);
                e.Graphics.DrawLine(blackPen, point3, point4);
                e.Graphics.DrawLine(blackPen, 45, 219, 45, 900);
                e.Graphics.DrawLine(blackPen, 80, 219, 80, 900);
                e.Graphics.DrawLine(blackPen, 410, 219, 410, 900);
                e.Graphics.DrawLine(blackPen, 475, 219, 475, 900);
                e.Graphics.DrawLine(blackPen, 540, 219, 540, 900);
                e.Graphics.DrawLine(blackPen, 650, 219, 650, 900);
                e.Graphics.DrawLine(blackPen, 760, 219, 760, 900);
                e.Graphics.DrawLine(blackPen, 45, 900, 760, 900);
                string headtext = "Sl No".PadRight(10) + "Item".PadRight(100) + "Tax%".PadRight(14) + "Qty".PadRight(20) + "Rate".PadRight(35) + "Total";
                e.Graphics.DrawString(headtext, printFontBold, new SolidBrush(Color.Black), startx - 2, starty + offset - 1);
                offset = offset + 40;
                Font font = new Font("Times New Roman", 10);
                float fontheight = font.GetHeight();
                try
                {
                    int i = 0;
                    foreach (DataGridViewRow row in dgItems.Rows)
                    {
                        i = i + 1;
                        string name = row.Cells["cName"].Value.ToString().Length <= 60 ? row.Cells[1].Value.ToString() : row.Cells[1].Value.ToString().Substring(0, 60);
                        string tax = row.Cells[7].Value.ToString();
                        string qty = row.Cells[5].Value.ToString();
                        string rate = row.Cells[6].Value.ToString();
                        string price = row.Cells[11].Value.ToString();
                        string Serial = row.Cells["SerialNos"].Value.ToString();
                        string productline = name + tax + qty + rate + price;
                        e.Graphics.DrawString(i.ToString(), font, new SolidBrush(Color.Black), startx, starty + offset);                           //offset = offset + (int)fontheight + 10;
                        e.Graphics.DrawString(name, font, new SolidBrush(Color.Black), startx + 30, starty + offset);
                        e.Graphics.DrawString(tax, font, new SolidBrush(Color.Black), startx + 380, starty + offset);
                        e.Graphics.DrawString(qty, font, new SolidBrush(Color.Black), startx + 440, starty + offset);
                        e.Graphics.DrawString(rate, font, new SolidBrush(Color.Black), startx + 525, starty + offset);
                        e.Graphics.DrawString(price, font, new SolidBrush(Color.Black), startx + 630, starty + offset);
                        offset = offset + (int)fontheight + 10;
                        if (Serial != "")
                        {
                            e.Graphics.DrawString("SN No: " + Serial, font, new SolidBrush(Color.Black), startx + 30, starty + offset);
                            offset = offset + (int)fontheight + 10;
                        }
                    }
                }
                catch
                {

                }
            }

            e.Graphics.DrawString("E & OE", Headerfont2, new SolidBrush(Color.Black), startx, 901);
            float newoffset = 900;
            e.Graphics.DrawString(NOTES.Text, Headerfont2, new SolidBrush(Color.Black), startx, starty + newoffset + 13);
            e.Graphics.DrawString("Gross Total", printFontBold, new SolidBrush(Color.Black), startx + 490, starty + newoffset + 3);
            e.Graphics.DrawString(Spell.SpellAmount.comma(Convert.ToDecimal(TOTAL_AMOUNT.Text) - Convert.ToDecimal(TAX_TOTAL.Text)), Headerfont2, new SolidBrush(Color.Black), startx + 610, starty + newoffset + 3);
            try
            {
                string test = Spell.SpellAmount.InWrods(Convert.ToDecimal(NET_AMOUNT.Text));
                int index = test.IndexOf("Taka");
                int l = test.Length;
                test = test.Substring(index + 4);
                e.Graphics.DrawString(test, Headerfont2, new SolidBrush(Color.Black), startx, starty + newoffset + 3);
            }
            catch
            {
            }
            newoffset = newoffset + 20;
            e.Graphics.DrawString("Tax Amt", printFontBold, new SolidBrush(Color.Black), startx + 490, starty + newoffset + 3);
            e.Graphics.DrawString(TAX_TOTAL.Text, Headerfont2, new SolidBrush(Color.Black), startx + 610, starty + newoffset + 3);
            newoffset = newoffset + 20;
            e.Graphics.DrawString("Discount:", printFontBold, new SolidBrush(Color.Black), startx + 490, starty + newoffset + 3);
            e.Graphics.DrawString(DISCOUNT.Text, Headerfont2, new SolidBrush(Color.Black), startx + 610, starty + newoffset + 3);
            newoffset = newoffset + 20;
            e.Graphics.DrawString("---------------------------------------", Headerfont2, new SolidBrush(Color.Black), startx + 490, starty + newoffset + 9);
            newoffset = newoffset + 25;
            e.Graphics.DrawString("Net Amount:", printFontBold, new SolidBrush(Color.Black), startx + 490, starty + newoffset + 3);
            e.Graphics.DrawString(Spell.SpellAmount.comma(Convert.ToDecimal(NET_AMOUNT.Text)), Headerfont2, new SolidBrush(Color.Black), startx + 610, starty + newoffset + 3);
            newoffset = newoffset + 20;
            e.Graphics.DrawString("Terms & Condtions", Headerfont2, new SolidBrush(Color.Black), startx, starty + newoffset + 3);
            newoffset = newoffset + 20;
            e.Graphics.DrawString("1)Above Material recieved in good condition", Headerfont2, new SolidBrush(Color.Black), startx, starty + newoffset + 3);
            newoffset = newoffset + 20;
            e.Graphics.DrawString("2)Zed IT Shop Act as a Dealer of Goods on behalf of vendor and                                                        Authorized Signatory", Headerfont2, new SolidBrush(Color.Black), startx, starty + newoffset + 3);
            newoffset = newoffset + 20;
            e.Graphics.DrawString("Cannot provide any warranty covered under the bill is as per                                                           [With Status and Seal]", Headerfont2, new SolidBrush(Color.Black), startx, starty + newoffset + 3);
            newoffset = newoffset + 20;
            e.Graphics.DrawString("The waranty terms of the Manufacture", Headerfont2, new SolidBrush(Color.Black), startx, starty + newoffset + 3);
            newoffset = newoffset + 20;
            e.HasMorePages = false;
            
        }


        private void printGSTPage(PrintPageEventArgs e, String transportMode, String vehicleNumber, String dateOfSupply, String placeOfSupply,String customerName, String customerAddress, String customerGSTTin, String state, String stateCode, String accountNumber, String ifscCode)
        {
            Company company = Common.getCompany();

            decimal ttaxblevalue = 0;
            decimal ttaxamount = 0;
            decimal ttotalvalue = 0;
            decimal totalNet = 0;
            decimal totalDisc = 0;
            decimal gsttaxamt = 0;
            decimal divtaxamt = 0;
            //m = 0;
            //bool hasmorepages = false;
            //float xpos;
            //int startx = 50;
            //int starty = 20;
            //int offset = 15;
            //bool PRINTTOTALPAGE = true;
            //int w = e.MarginBounds.Width / 2;
            //int x = e.MarginBounds.Left;
            //int y = e.MarginBounds.Top;
            //Font Headerfont1 = new Font("Calibri", 15, FontStyle.Bold);
            //Font dec = new Font("Calibri", 8, FontStyle.Regular);
            //Font Headerfont2 = new Font("Times New Roman", 8, FontStyle.Regular);
            //Font Headerfont5 = new Font("Times New Roman", 10, FontStyle.Regular);
            //Font printFont = new Font("Calibri", 10);
            //Font printFontBold = new Font("Calibri", 10, FontStyle.Bold);
            //Font printFontBold1 = new Font("Calibri", 14, FontStyle.Bold);
            //Font printFontnet = new Font("Calibri", 11, FontStyle.Bold);
            //Font printbold = new Font("Calibri", 10, FontStyle.Bold);
            //Font printFontBold2 = new Font("Times New Roman", 16, FontStyle.Bold);
            //Font printFontBold3 = new Font("Times New Roman", 13, FontStyle.Regular);
            //Font font = new Font("Times New Roman", 8, FontStyle.Regular);
            //string address1 = "";
            //string address2 = "";
            //string address0 = "";
            //string disc = "";
            //string qty = "";
            //string rate = "";
            //string gross = "";
            //string price = "";
            //double taxamt = 0;
            //string txbval = "";
            //string free = "0";
            //string uom = "";
            //string netvalue = "";
            //string grossvalue = "";
            //string totalvalue = "";
            //var tabDataForeColor = Color.Black;
            //int height = 100 + y;
            //Pen blackPen1 = new Pen(Color.Black, 2);
            //Pen blackPen = new Pen(Color.Black, 1);
            //address0 = company.Name;
            //address1 = company.Other_details;
            ////address2 = "Areakode Road,KONDOTTY";
            //address2 = "";
            //string mob1 = company.Phone;
            ////string mob2 = "9400155201";
            //string mob2 = "";
            //string email = company.Email;

            //int centerOfPage = e.PageBounds.Width/2;

            //int nameStartPosision = centerOfPage - TextRenderer.MeasureText(address0, printFontBold2).Width / 2;

            //int addressStartPosition = centerOfPage - TextRenderer.MeasureText(address1, printFontBold3).Width / 2;
            ///*if (addressStartPosition >= 350)
            //{
            //    addressStartPosition = 300;
            //}*/
            //String mobile = "Mob:" + mob1 + "," + mob2;
            //int mobileStartPosition = centerOfPage - TextRenderer.MeasureText(mobile, printFontBold3).Width/2;
            //String gsttin = "GSTIN : " + company.TIN_No;
            //int gsttinStartPosition = centerOfPage - TextRenderer.MeasureText(gsttin, printFontBold3).Width / 2;
            //e.Graphics.DrawString(address0, printFontBold2, new SolidBrush(Color.Black), nameStartPosision, 15);
            //e.Graphics.DrawString(address1, printFontBold3, new SolidBrush(Color.Black), addressStartPosition, 40);
            //e.Graphics.DrawString(mobile, printFontBold3, new SolidBrush(Color.Black), mobileStartPosition, 60);
            //e.Graphics.DrawString(gsttin, printFontBold3, new SolidBrush(Color.Black), gsttinStartPosition, 80);
            //e.Graphics.DrawRectangle(blackPen1, 20, 115, 800, 1060); //BIG RECTANGLE
            //e.Graphics.DrawLine(blackPen, 20, 160, 820, 160); // H LINE 1
            //e.Graphics.DrawString("INVOICE", printFontBold2, new SolidBrush(Color.Black), 380, 125);
            //e.Graphics.DrawLine(blackPen, 630, 115, 630, 160); // V LINE 1
            //e.Graphics.DrawLine(blackPen, 640, 115, 640, 160); // V LINE 1
            //e.Graphics.DrawLine(blackPen, 630, 130, 820, 130); // HLINE 1         
            //e.Graphics.DrawLine(blackPen, 630, 145, 820, 145); // HLINE 1

            //e.Graphics.DrawString("Original for Receipient", Headerfont2, new SolidBrush(Color.Black), 642, 117);
            //e.Graphics.DrawString("Duplicate for Supplier/Transporter", Headerfont2, new SolidBrush(Color.Black), 642, 131);
            //e.Graphics.DrawString("Triplicate for Supplier", Headerfont2, new SolidBrush(Color.Black), 642, 146);
            //e.Graphics.DrawString("Reverese Charge", Headerfont5, new SolidBrush(Color.Black), 20, 163);
            //e.Graphics.DrawString("Invoice No", Headerfont5, new SolidBrush(Color.Black), 20, 183);
            //e.Graphics.DrawString("Invoice Date", Headerfont5, new SolidBrush(Color.Black), 20, 203);
            //e.Graphics.DrawString("State", Headerfont5, new SolidBrush(Color.Black), 20, 223);
            //e.Graphics.DrawString(": ", Headerfont5, new SolidBrush(Color.Black), 110, 163);
            //e.Graphics.DrawString(": " + VOUCHNUM.Text, Headerfont5, new SolidBrush(Color.Black), 110, 183);
            //e.Graphics.DrawString(": " + DOC_DATE_GRE.Text, Headerfont5, new SolidBrush(Color.Black), 110, 203);
            //e.Graphics.DrawString(": " + "Kerala", Headerfont5, new SolidBrush(Color.Black), 110, 223);

            //e.Graphics.DrawLine(blackPen, 20, 240, 820, 240); // H LINE 1
            //e.Graphics.DrawLine(blackPen, 450, 160, 450, 240); // V LINE seperation
            //e.Graphics.DrawRectangle(blackPen, 310, 222, 130, 18); //small RECTANGLE for state code
            //e.Graphics.DrawString("State Code :" + "       " + stateCode, Headerfont5, new SolidBrush(Color.Black), 315, 223);
            //e.Graphics.DrawLine(blackPen, 400, 222, 400, 240); // V LINE 1 in state code
            //e.Graphics.DrawString("Transportation Mode", Headerfont5, new SolidBrush(Color.Black), 450, 163);
            //e.Graphics.DrawString("Vehicle Number", Headerfont5, new SolidBrush(Color.Black), 450, 183);
            //e.Graphics.DrawString("Date of Supply", Headerfont5, new SolidBrush(Color.Black), 450, 203);
            //e.Graphics.DrawString("Place of Supply", Headerfont5, new SolidBrush(Color.Black), 450, 223);
            //e.Graphics.DrawString(": " + transportMode, Headerfont5, new SolidBrush(Color.Black), 590, 163);
            //e.Graphics.DrawString(": " + vehicleNumber, Headerfont5, new SolidBrush(Color.Black), 590, 183);
            //e.Graphics.DrawString(": " + dateOfSupply, Headerfont5, new SolidBrush(Color.Black), 590, 203);
            //e.Graphics.DrawString(": " + placeOfSupply, Headerfont5, new SolidBrush(Color.Black), 590, 223);

            //e.Graphics.DrawLine(blackPen, 20, 255, 820, 255); // H LINE for details of reciever and cosignee
            //e.Graphics.DrawLine(blackPen, 20, 275, 820, 275); // H LINE for details of reciever and cosignee
            //e.Graphics.DrawString("Details of Receiver | Bill to:", Headerfont5, new SolidBrush(Color.Black), 130, 257);
            //e.Graphics.DrawString("Details of Cosignee | Shipped to:", Headerfont5, new SolidBrush(Color.Black), 560, 257);
            //e.Graphics.DrawString("Name", Headerfont5, new SolidBrush(Color.Black), 20, 278);
            //e.Graphics.DrawString("Address", Headerfont5, new SolidBrush(Color.Black), 20, 298);
            //e.Graphics.DrawString("GSTIN", Headerfont5, new SolidBrush(Color.Black), 20, 338);
            //e.Graphics.DrawString("State", Headerfont5, new SolidBrush(Color.Black), 20, 358);
            //e.Graphics.DrawString(": " + customerName, Headerfont5, new SolidBrush(Color.Black), 110, 278);
            //e.Graphics.DrawString(": " + customerAddress, Headerfont5, new SolidBrush(Color.Black), 110, 298);
            //e.Graphics.DrawString(": " + customerGSTTin, Headerfont5, new SolidBrush(Color.Black), 110, 338);
            //e.Graphics.DrawString(": " + state, Headerfont5, new SolidBrush(Color.Black), 110, 358);

            ////bankDetails();
            
            //e.Graphics.DrawLine(blackPen, 450, 255, 450, 375); // V LINE separation for details of reciever and cosignee           
            //e.Graphics.DrawString("Name", Headerfont5, new SolidBrush(Color.Black), 450, 278);
            //e.Graphics.DrawString("Address", Headerfont5, new SolidBrush(Color.Black), 450, 298);
            //e.Graphics.DrawString("GSTIN", Headerfont5, new SolidBrush(Color.Black), 450, 338);
            //e.Graphics.DrawString("State", Headerfont5, new SolidBrush(Color.Black), 450, 358);
            //e.Graphics.DrawString(": " + customerName, Headerfont5, new SolidBrush(Color.Black), 510, 278);
            //e.Graphics.DrawString(": " + customerAddress, Headerfont5, new SolidBrush(Color.Black), 510, 298);
            //e.Graphics.DrawString(": " + customerGSTTin, Headerfont5, new SolidBrush(Color.Black), 510, 338);
            //e.Graphics.DrawString(": " + state, Headerfont5, new SolidBrush(Color.Black), 510, 358);

            //e.Graphics.DrawRectangle(blackPen, 310, 357, 130, 18); //small RECTANGLE for state code in details of reciever and cosignee
            //e.Graphics.DrawLine(blackPen, 400, 357, 400, 375); // V LINE 1 in state code
            //e.Graphics.DrawString("State Code :" + "       " + stateCode, Headerfont5, new SolidBrush(Color.Black), 315, 358);
            //e.Graphics.DrawRectangle(blackPen, 680, 357, 130, 18); //small RECTANGLE for state code in details of reciever and cosignee
            //e.Graphics.DrawLine(blackPen, 770, 357, 770, 375); // V LINE 1 in state code
            //e.Graphics.DrawString("State Code :" + "       " + stateCode, Headerfont5, new SolidBrush(Color.Black), 685, 358);
            //e.Graphics.DrawLine(blackPen, 20, 375, 820, 375); // H LINE for ending the details of reciever and cosignee
            //e.Graphics.DrawLine(blackPen, 20, 393, 820, 393); // H LINE below the details of reciever and cosignee
            //e.Graphics.DrawLine(blackPen, 20, 435, 820, 435); // H LINE 
            //offset = offset + 357;
            //string headtext = "Sl.".PadRight(7) + "Name of Product/Service".PadRight(27) + "HSN".PadRight(7) + "UOM".PadRight(5) + "Qty".PadRight(8) + "Rate".PadRight(10) +
            //    "Amount".PadRight(8) + "Less:".PadRight(8) + "Taxable".PadRight(18) + "CGST".PadRight(18) + "SGST".PadRight(18) + "IGST".PadRight(18) + "Total";
            //e.Graphics.DrawString(headtext, printFontBold, new SolidBrush(Color.Black), startx - 25, starty + offset);
            //e.Graphics.DrawString("No.", printFontBold, new SolidBrush(Color.Black), startx - 28, starty + offset + 16);
            //e.Graphics.DrawString("ACN.", printFontBold, new SolidBrush(Color.Black), startx + 160, starty + offset + 16);
            //e.Graphics.DrawString("Disc.", printFontBold, new SolidBrush(Color.Black), startx + 375, starty + offset + 16);
            //e.Graphics.DrawString("Value.", printFontBold, new SolidBrush(Color.Black), startx + 415, starty + offset + 16);
            //string text = "Rate" + "  " + "Amount";
            //e.Graphics.DrawString(text, printFontBold, new SolidBrush(Color.Black), startx + 460, starty + offset + 20);
            //e.Graphics.DrawString(text, printFontBold, new SolidBrush(Color.Black), startx + 545, starty + offset + 20);
            //e.Graphics.DrawString(text, printFontBold, new SolidBrush(Color.Black), startx + 628, starty + offset + 20);
            //e.Graphics.DrawLine(blackPen, 20, 900, 820, 900); // H LINE 
            //e.Graphics.DrawLine(blackPen, 46, 393, 46, 900); // v LINE for slno
            //e.Graphics.DrawLine(blackPen, 208, 393, 208, 900); // v LINE for name of product
            //e.Graphics.DrawLine(blackPen, 248, 393, 248, 900); // v LINE for hsn
            //e.Graphics.DrawLine(blackPen, 283, 393, 283, 930); // v LINE for uom
            //e.Graphics.DrawLine(blackPen, 315, 393, 315, 930); // v LINE for qty
            //e.Graphics.DrawLine(blackPen, 365, 393, 365, 930); // v LINE for rate
            //e.Graphics.DrawLine(blackPen, 420, 393, 420, 930); // v LINE for amount
            //e.Graphics.DrawLine(blackPen, 460, 393, 460, 930); // v LINE for LESS
            //e.Graphics.DrawLine(blackPen, 510, 393, 510, 930); // v LINE for TAXABLE
            //e.Graphics.DrawLine(blackPen, 595, 393, 595, 930); // v LINE for cgst
            //e.Graphics.DrawLine(blackPen, 680, 393, 680, 930); // v LINE for sgst
            //e.Graphics.DrawLine(blackPen, 760, 393, 760, 930); // v LINE for igst
            //e.Graphics.DrawLine(blackPen, 510, 413, 760, 413); // H LINE in between cgst and igst
            //e.Graphics.DrawLine(blackPen, 540, 413, 540, 900); // v LINE in between cgst
            //e.Graphics.DrawLine(blackPen, 625, 413, 625, 900); // v LINE in between sgst
            //e.Graphics.DrawLine(blackPen, 710, 413, 710, 900); // v LINE in between igst
            //e.Graphics.DrawLine(blackPen, 20, 930, 820, 930); // H LINE for all total amount
            //StringFormat format = new StringFormat(StringFormatFlags.DirectionRightToLeft);
            //float fontheight = font.GetHeight();
            //int i = 0;
            //int printpoint = 22; //32
            //int j = 1;
            //string mrp = "";
            //int nooflines = 0;
            //double pq;
            //string total = "";
            //foreach (DataGridViewRow row in dgItems.Rows)
            //{
            //    PRINTTOTALPAGE = false;
            //    if (j > printeditems)
            //    {
            //        if (nooflines < 16)
            //        {
            //            m = m + 1;
            //            string period, periodtype, tax;
            //            i = i + 1;
            //            int ORGLENGTH = row.Cells["cName"].Value.ToString().Length; /////32

            //            string name = row.Cells["cName"].Value.ToString().Length <= 22 ? row.Cells[1].Value.ToString() : row.Cells[1].Value.ToString().Substring(0, 22);
            //            string scnno = Convert.ToString(row.Cells["uHSNNO"].Value);
            //            string name2 = "";
            //            int BALANCELENGH = ORGLENGTH - 22;
            //            tax = Convert.ToString(row.Cells["cTaxPer"].Value);
            //            if (tax == "")
            //                tax = "0";

            //            double dtax = Convert.ToDouble(tax);
            //            tax = (dtax / 2).ToString();

            //            rate = row.Cells["cPrice"].Value.ToString();
            //            try
            //            {
            //                mrp = row.Cells["cMrp"].Value.ToString();
            //            }
            //            catch
            //            {
            //                mrp = "0";
            //            }
            //            try
            //            {
            //                free = row.Cells["Cfree"].Value.ToString();
            //            }
            //            catch
            //            {
            //                free = "0";
            //            }
            //            uom = row.Cells["cUnit"].Value.ToString();
            //            qty = row.Cells["cQty"].Value.ToString();
            //            grossvalue = row.Cells["cGTotal"].Value.ToString();
            //            netvalue = row.Cells["cNetValue"].Value.ToString();
            //            totalvalue = row.Cells["cTotal"].Value.ToString();
            //            disc = row.Cells["cDisc"].Value.ToString();
            //            tcdis = tcdis + Convert.ToDecimal(disc);

            //            gross = (Convert.ToDouble(qty) * Convert.ToDouble(rate)).ToString("N2");
            //            price = Convert.ToDouble(row.Cells[12].Value).ToString();
            //            taxamt = Convert.ToDouble(row.Cells["cTaxAmt"].Value);

            //            try
            //            {
            //                tfree = tfree + Convert.ToInt32(free);
            //            }
            //            catch
            //            {
            //            }
            //            tqty = tqty + Convert.ToInt32(qty);
            //            trate = trate + Convert.ToDecimal(rate);
            //            string productline = name + tax + qty + rate + price;
            //            e.Graphics.DrawString(m.ToString(), font, new SolidBrush(Color.Black), startx - 28, starty + offset + 50);
            //            e.Graphics.DrawString(name, Headerfont2, new SolidBrush(Color.Black), startx - 4, starty + offset + 50);
            //            e.Graphics.DrawString(scnno, Headerfont2, new SolidBrush(Color.Black), startx + 160, starty + offset + 50);
            //            e.Graphics.DrawString(uom, Headerfont2, new SolidBrush(Color.Black), startx + 198, starty + offset + 50);
            //            e.Graphics.DrawString(qty, font, new SolidBrush(Color.Black), startx + 265, starty + offset + 50, format);
            //            e.Graphics.DrawString(rate, font, new SolidBrush(Color.Black), startx + 315, starty + offset + 50, format);
            //            e.Graphics.DrawString(grossvalue, font, new SolidBrush(Color.Black), startx + 370, starty + offset + 50, format);
            //            e.Graphics.DrawString(disc, font, new SolidBrush(Color.Black), startx + 410, starty + offset + 50, format);
            //            e.Graphics.DrawString(netvalue, font, new SolidBrush(Color.Black), startx + 460, starty + offset + 50, format);
            //            if (lg.state.Equals(Convert.ToString(cmbState.SelectedValue)))
            //            {
            //                e.Graphics.DrawString(tax, font, new SolidBrush(Color.Black), startx + 490, starty + offset + 50, format);
            //                e.Graphics.DrawString((taxamt / 2).ToString(), font, new SolidBrush(Color.Black), startx + 540, starty + offset + 50, format);
            //                e.Graphics.DrawString(tax, font, new SolidBrush(Color.Black), startx + 575, starty + offset + 50, format);
            //                e.Graphics.DrawString((taxamt / 2).ToString(), font, new SolidBrush(Color.Black), startx + 625, starty + offset + 50, format);
            //            }
            //            else
            //            {
            //                //IGST
            //                e.Graphics.DrawString((Convert.ToDecimal(tax) * 2).ToString(), font, new SolidBrush(Color.Black), startx + 655, starty + offset + 50, format);
            //                e.Graphics.DrawString(taxamt.ToString(), font, new SolidBrush(Color.Black), startx + 700, starty + offset + 50, format);
            //            }

                        

            //            e.Graphics.DrawString(totalvalue, font, new SolidBrush(Color.Black), startx + 770, starty + offset + 50, format);
            //            tgrossrate = tgrossrate + Convert.ToDecimal(grossvalue);
            //            ttaxblevalue = ttaxblevalue + Convert.ToDecimal(netvalue);
            //            ttaxamount = ttaxamount + Convert.ToDecimal(taxamt / 2);
            //            ttotalvalue = ttotalvalue + Convert.ToDecimal(totalvalue);
            //            gsttaxamt = ttaxamount + ttaxamount;
            //            offset = offset + (int)fontheight + 2;
            //            while (BALANCELENGH > 1)
            //            {
            //                name2 = BALANCELENGH <= 22 ? row.Cells["cName"].Value.ToString().Substring(printpoint, BALANCELENGH) : row.Cells["cName"].Value.ToString().Substring(printpoint, 22);
            //                e.Graphics.DrawString(name2, Headerfont2, new SolidBrush(Color.Black), startx - 4, starty + offset + 50);
            //                BALANCELENGH = BALANCELENGH - 22;
            //                printpoint = printpoint + 22;
            //                starty = starty + (int)fontheight;
            //            }
            //            printpoint = 22;
            //            nooflines++;
            //            j++;
            //        }
            //        else
            //        {
            //            printeditems = j - 1;
            //            hasmorepages = true;
            //            PRINTTOTALPAGE = true;
            //        }
            //        if (hasmorepages == true)
            //        {
            //            e.Graphics.DrawString("coutinue...", printFontBold, new SolidBrush(Color.Black), startx + 40, 901);
            //        }
            //    }
            //    else
            //    {
            //        j++;
            //        m++;
            //    }
            //}

            //float newoffset = 900;

            //if (!PRINTTOTALPAGE)
            //{
            //    PAGETOTAL = true;
            //    if (PAGETOTAL)
            //    {
            //        try
            //        {
            //            e.Graphics.DrawString(tqty.ToString(), font, new SolidBrush(Color.Black), 283, 905);
            //            e.Graphics.DrawString(tgrossrate.ToString(), font, new SolidBrush(Color.Black), 363, 905); // total amount
            //            e.Graphics.DrawString(tcdis.ToString(), font, new SolidBrush(Color.Black), 420, 905);  // total discount
            //            e.Graphics.DrawString(ttaxblevalue.ToString(), font, new SolidBrush(Color.Black), 458, 905); // total taxble value
            //            e.Graphics.DrawString(ttaxamount.ToString(), font, new SolidBrush(Color.Black), 530, 905); // total cgst tax
            //            e.Graphics.DrawString(ttaxamount.ToString(), font, new SolidBrush(Color.Black), 605, 905); // total sgst tax
            //            e.Graphics.DrawString(ttotalvalue.ToString(), font, new SolidBrush(Color.Black), 760, 905); // total sgst tax

            //            e.Graphics.DrawLine(blackPen, 20, 1030, 820, 1030); // H LINE bottom
            //            e.Graphics.DrawLine(blackPen, 510, 930, 510, 1175); // v LINE separtion in footer
            //            e.Graphics.DrawString("Total Amount Before Tax", Headerfont5, new SolidBrush(Color.Black), 510, 933);
            //            e.Graphics.DrawString(ttaxblevalue.ToString(), Headerfont5, new SolidBrush(Color.Black), 700, 933); // total taxble value

            //            e.Graphics.DrawLine(blackPen, 510, 950, 820, 950); // H LINE bottom
            //            e.Graphics.DrawString("Add : CGST", Headerfont5, new SolidBrush(Color.Black), 510, 953);

            //            e.Graphics.DrawLine(blackPen, 510, 970, 820, 970); // H LINE bottom
            //            e.Graphics.DrawString("Add : SGST", Headerfont5, new SolidBrush(Color.Black), 510, 973);

            //            if (lg.state.Equals(Convert.ToString(cmbState.SelectedValue)))
            //            {
                            
            //                e.Graphics.DrawString(ttaxamount.ToString(), Headerfont5, new SolidBrush(Color.Black), 700, 953); // total cgst tax

            //                e.Graphics.DrawString(ttaxamount.ToString(), Headerfont5, new SolidBrush(Color.Black), 700, 973); // total cgst tax

            //            }
            //            else
            //            {
            //                e.Graphics.DrawString(gsttaxamt.ToString(), Headerfont5, new SolidBrush(Color.Black), 700, 993); // total igst tax
            //            }
            //            e.Graphics.DrawLine(blackPen, 510, 990, 820, 990); // H LINE bottom
            //            e.Graphics.DrawString("Add : IGST", Headerfont5, new SolidBrush(Color.Black), 510, 993);
                        

            //            e.Graphics.DrawLine(blackPen, 510, 1010, 820, 1010); // H LINE bottom
            //            e.Graphics.DrawString("Tax Amount : GST", Headerfont5, new SolidBrush(Color.Black), 510, 1013);
            //            e.Graphics.DrawString(gsttaxamt.ToString(), Headerfont5, new SolidBrush(Color.Black), 700, 1013); // total cgst tax

            //            e.Graphics.DrawString("Total Amount After Tax", Headerfont5, new SolidBrush(Color.Black), 510, 1033);
            //            e.Graphics.DrawString(ttotalvalue.ToString(), Headerfont5, new SolidBrush(Color.Black), 700, 1033); // total cgst tax

            //            e.Graphics.DrawString("GST Payable on Reverse Charge", Headerfont5, new SolidBrush(Color.Black), 510, 1063);
            //            e.Graphics.DrawLine(blackPen, 400, 1030, 400, 1175); // v LINE separtion of footer in near right side of bank details
            //            e.Graphics.DrawLine(blackPen, 20, 1080, 400, 1080); // H LINE bottom
            //            e.Graphics.DrawLine(blackPen, 510, 1080, 820, 1080); // H LINE bottom
            //            e.Graphics.DrawLine(blackPen, 510, 1050, 820, 1050); // H LINE bottom
            //            e.Graphics.DrawLine(blackPen, 510, 1060, 820, 1060); // H LINE bottom
            //            e.Graphics.DrawLine(blackPen, 700, 930, 700, 1050);  //v line in total amount
            //            e.Graphics.DrawLine(blackPen, 700, 1060, 700, 1080);  //v line in total amount
            //            e.Graphics.DrawString("Certified that the particulars given above are true", Headerfont5, new SolidBrush(Color.Black), 510, 1082);
            //            e.Graphics.DrawString("and correct.", Headerfont5, new SolidBrush(Color.Black), 510, 1100);
            //            e.Graphics.DrawString("Authorised Signatory", Headerfont5, new SolidBrush(Color.Black), 605, 1152);
            //            e.Graphics.DrawString("Total Invoice Amount in Words:", printFontBold, new SolidBrush(Color.Black), 165, 932);
            //            e.Graphics.DrawString("Bank Details:", printFontBold, new SolidBrush(Color.Black), 175, 1030);
            //            e.Graphics.DrawString("Bank Account Number", Headerfont5, new SolidBrush(Color.Black), 20, 1045);
            //            e.Graphics.DrawString("Bank Branch IFSC", Headerfont5, new SolidBrush(Color.Black), 20, 1062);
            //            if (CUSTOMER_CODE.Text != "" && CUSTOMER_NAME.Text != "")
            //            {
            //                e.Graphics.DrawString(":" + accountNumber, Headerfont5, new SolidBrush(Color.Black), 155, 1047);
            //                e.Graphics.DrawString(":" + ifscCode, Headerfont5, new SolidBrush(Color.Black), 155, 1062);
            //            }
            //            e.Graphics.DrawString("(Common Seal)", Headerfont2, new SolidBrush(Color.Black), 415, 1160);
            //            e.Graphics.DrawString("Terms and Conditions:", printFontBold, new SolidBrush(Color.Black), 140, 1080);
            //            e.Graphics.DrawString("(To be furnished by the seller)", Headerfont5, new SolidBrush(Color.Black), 120, 1098);
            //            e.Graphics.DrawString("Certified that all the particulars in the above tax invoice", Headerfont5, new SolidBrush(Color.Black), 50, 1116);
            //            e.Graphics.DrawString("are true and correct and that my/our  registration under", Headerfont5, new SolidBrush(Color.Black), 55, 1134);
            //            e.Graphics.DrawString("KVAT ACT 2003 is valued as on the date of this bill", Headerfont5, new SolidBrush(Color.Black), 60, 1152);
            //            try
            //            {
            //                int cash = (int)Convert.ToDouble(NET_AMOUNT.Text);
            //                string cas = NET_AMOUNT.Text;
            //                string[] parts = cas.Split('.');
            //                string test3 = "";
            //                long i1, i2;
            //                try
            //                {
            //                    i1 = (long)Convert.ToDouble(parts[0]);
            //                }
            //                catch
            //                {
            //                    i1 = 0;
            //                }
            //                try
            //                {
            //                    i2 = (long)Convert.ToDouble(parts[1]);
            //                }
            //                catch
            //                {
            //                    i2 = 0;
            //                }

            //                if (i1 != 0 && i2 != 0)
            //                {
            //                    string test = NumbersToWords(i1);
            //                    string test2 = NumbersToWords(i2);
            //                    test3 = test + " Rupees and " + test2 + "Paisa only";

            //                    string seclin, linef;
            //                    int index = test3.IndexOf("Rupees");
            //                }
            //                if (i1 > 0 && i2 == 0)
            //                {
            //                    string test = NumbersToWords(i1);
            //                    test3 = test + " only";
            //                }
            //                e.Graphics.DrawString(test3, printbold, new SolidBrush(Color.Black), 20, 951);
            //            }
            //            catch
            //            {
            //            }
            //        }
            //        catch
            //        {
            //        }
            //    }
            //    PAGETOTAL = false;
            //}
            //newoffset = newoffset + 25;
            //e.HasMorePages = hasmorepages;
            //accountNumber = "";
            //ifscCode = "";


















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
            Font Headerfont1 = new Font("Calibri", 15, FontStyle.Bold);
            Font dec = new Font("Calibri", 8, FontStyle.Regular);
            Font Headerfont2 = new Font("Times New Roman", 8, FontStyle.Regular);
            Font Headerfont5 = new Font("Times New Roman", 10, FontStyle.Regular);
            Font printFont = new Font("Calibri", 10);
            Font printFontBold = new Font("Calibri", 10, FontStyle.Bold);
            Font printFontBold1 = new Font("Calibri", 14, FontStyle.Bold);
            Font printFontnet = new Font("Calibri", 11, FontStyle.Bold);
            Font printbold = new Font("Calibri", 10, FontStyle.Bold);
            Font printFontBold2 = new Font("Times New Roman", 16, FontStyle.Bold);
            Font printFontBold3 = new Font("Times New Roman", 8, FontStyle.Regular);
            Font font = new Font("Times New Roman", 8, FontStyle.Regular);
            string address1 = "";
            string address2 = "";
            string address0 = "";
            string disc = "";
            string qty = "";
            string rate = "";
            string gross = "";
            string price = "";
            double taxamt = 0;
            string txbval = "";
            string free = "0";
            string uom = "";
            string netvalue = "";
            decimal netvalueLessDisc = 0;
            string grossvalue = "";
            string totalvalue = "";
            double cgsttax = 0;
            double sgsttax = 0;
            //string sgsttax = "";
            double sgstaxamt = 0;
            float ssss = 0;
            var tabDataForeColor = Color.Black;
            int height = 100 + y;
            Pen blackPen1 = new Pen(Color.Black, 2);
            Pen blackPen = new Pen(Color.Black, 1);
            var txtDataWidth = e.Graphics.MeasureString(CompanyName, Headerfont1).Width;
            try
            {
                if (logo != null || logo != "")
                {
                    System.Drawing.Image img = System.Drawing.Image.FromFile(logo);
                    Point loc = new Point(20, 50);
                    e.Graphics.DrawImage(img, new Rectangle(303, 5, 230, 70));
                }
            }
            catch (Exception ex)
            {
            }


            address0 = company.Name ;
            address1 = company.Address;
            address2 = company.Other_details;
            string mob1 = company.Phone;
            string mob2 = "";
            string email = company.Email ;
            String gsttin = "GSTIN : " + company.TIN_No;
            int centerOfPage = e.PageBounds.Width / 2;

            int nameStartPosision = centerOfPage - TextRenderer.MeasureText(address0, printFontBold2).Width / 2;

            int addressStartPosition = centerOfPage - TextRenderer.MeasureText(address1, printFontBold3).Width / 2;
            int address2StartPosition = centerOfPage - TextRenderer.MeasureText(address2+","+mob1+","+mob2, printFontBold3).Width / 2;
            if (logo == null || logo == "")
            {
                e.Graphics.DrawString(address0, printFontBold2, new SolidBrush(Color.Black), nameStartPosision, 15);
            }
            int gsttinStartPosition = centerOfPage - TextRenderer.MeasureText(gsttin, printFontBold3).Width / 2;
            e.Graphics.DrawString(address1, printFontBold3, new SolidBrush(Color.Black), addressStartPosition, starty + offset + 38);
            e.Graphics.DrawString(address2 + "," + "Mob:" + mob1 + "," + mob2, printFontBold3, new SolidBrush(Color.Black), address2StartPosition, starty + offset + 53);
            e.Graphics.DrawString(gsttin, printFontBold3, new SolidBrush(Color.Black), gsttinStartPosition, starty + offset + 65);
            e.Graphics.DrawRectangle(blackPen1, 7, 115, 778, 945); //BIG RECTANGLE 790
            e.Graphics.DrawLine(blackPen, 7, 160, 784, 160); // H LINE 1
            e.Graphics.DrawString("INVOICE", printFontBold2, new SolidBrush(Color.Black), 380, 125);
            e.Graphics.DrawLine(blackPen, 615, 115, 615, 160); // V LINE 1
            e.Graphics.DrawLine(blackPen, 625, 115, 625, 160); // V LINE 1
            e.Graphics.DrawLine(blackPen, 615, 130, 784, 130); // HLINE 1         
            e.Graphics.DrawLine(blackPen, 615, 145, 784, 145); // HLINE 1

            e.Graphics.DrawString("Original for Receipient", Headerfont2, new SolidBrush(Color.Black), 625, 117);
            e.Graphics.DrawString("Duplicate for Supplier/Transporter", Headerfont2, new SolidBrush(Color.Black), 625, 131);
            e.Graphics.DrawString("Triplicate for Supplier", Headerfont2, new SolidBrush(Color.Black), 625, 146);
            e.Graphics.DrawString("Reverese Charge", Headerfont5, new SolidBrush(Color.Black), 7, 163);
            e.Graphics.DrawString("Invoice No", Headerfont5, new SolidBrush(Color.Black), 7, 183);
            e.Graphics.DrawString("Invoice Date", Headerfont5, new SolidBrush(Color.Black), 7, 203);
            e.Graphics.DrawString("State", Headerfont5, new SolidBrush(Color.Black), 7, 223);
            e.Graphics.DrawString(": ", Headerfont5, new SolidBrush(Color.Black), 110, 163);
            e.Graphics.DrawString(": " + VOUCHNUM.Text, Headerfont5, new SolidBrush(Color.Black), 110, 183);
            e.Graphics.DrawString(": " + DOC_DATE_GRE.Text, Headerfont5, new SolidBrush(Color.Black), 110, 203);
            e.Graphics.DrawString(": " + "Kerala", Headerfont5, new SolidBrush(Color.Black), 110, 223);

            e.Graphics.DrawLine(blackPen, 7, 240, 784, 240); // H LINE 1
            e.Graphics.DrawLine(blackPen, 450, 160, 450, 240); // V LINE seperation
            e.Graphics.DrawRectangle(blackPen, 310, 222, 130, 18); //small RECTANGLE for state code
            e.Graphics.DrawString("State Code :" + "       " + stateCode, Headerfont5, new SolidBrush(Color.Black), 315, 223);
            e.Graphics.DrawLine(blackPen, 400, 222, 400, 240); // V LINE 1 in state code
            e.Graphics.DrawString("Transportation Mode", Headerfont5, new SolidBrush(Color.Black), 450, 163);
            e.Graphics.DrawString("Vehicle Number", Headerfont5, new SolidBrush(Color.Black), 450, 183);
            e.Graphics.DrawString("Date of Supply", Headerfont5, new SolidBrush(Color.Black), 450, 203);
            e.Graphics.DrawString("Place of Supply", Headerfont5, new SolidBrush(Color.Black), 450, 223);
            e.Graphics.DrawString(": " + transportMode, Headerfont5, new SolidBrush(Color.Black), 590, 163);
            e.Graphics.DrawString(": " + vehicleNumber, Headerfont5, new SolidBrush(Color.Black), 590, 183);
            e.Graphics.DrawString(": " + dateOfSupply, Headerfont5, new SolidBrush(Color.Black), 590, 203);
            e.Graphics.DrawString(": " + placeOfSupply, Headerfont5, new SolidBrush(Color.Black), 590, 223);

            e.Graphics.DrawLine(blackPen, 7, 255, 784, 255); // H LINE for details of reciever and cosignee
            e.Graphics.DrawLine(blackPen, 7, 275, 784, 275); // H LINE for details of reciever and cosignee
            e.Graphics.DrawString("Details of Receiver | Bill to:", Headerfont5, new SolidBrush(Color.Black), 130, 257);
            e.Graphics.DrawString("Details of Cosignee | Shipped to:", Headerfont5, new SolidBrush(Color.Black), 560, 257);
            e.Graphics.DrawString("Name", Headerfont5, new SolidBrush(Color.Black), 7, 278);
            e.Graphics.DrawString("Address", Headerfont5, new SolidBrush(Color.Black), 7, 298);
            e.Graphics.DrawString("Mob", Headerfont5, new SolidBrush(Color.Black), 7, 318);
            e.Graphics.DrawString("GSTIN", Headerfont5, new SolidBrush(Color.Black), 7, 338);
            e.Graphics.DrawString("State", Headerfont5, new SolidBrush(Color.Black), 7, 358);
            e.Graphics.DrawString(": " + CUSTOMER_NAME.Text, Headerfont5, new SolidBrush(Color.Black), 60, 278);
            e.Graphics.DrawString(": " + ADDRESS_A, Headerfont5, new SolidBrush(Color.Black), 60, 298);
            e.Graphics.DrawString(": " + phone_no, Headerfont5, new SolidBrush(Color.Black), 60, 318);
            e.Graphics.DrawString(": " + tin_no, Headerfont5, new SolidBrush(Color.Black), 60, 338);
            e.Graphics.DrawString(": " + "Kerala", Headerfont5, new SolidBrush(Color.Black), 60, 358);

            e.Graphics.DrawLine(blackPen, 450, 255, 450, 375); // V LINE separation for details of reciever and cosignee           
            e.Graphics.DrawString("Name", Headerfont5, new SolidBrush(Color.Black), 450, 278);
            e.Graphics.DrawString("Address", Headerfont5, new SolidBrush(Color.Black), 450, 298);
            e.Graphics.DrawString("Mob", Headerfont5, new SolidBrush(Color.Black), 450, 318);
            e.Graphics.DrawString("GSTIN", Headerfont5, new SolidBrush(Color.Black), 450, 338);
            e.Graphics.DrawString("State", Headerfont5, new SolidBrush(Color.Black), 450, 358);

            if (check_shipping.Checked == false)
            {
                // string name = CUSTOMER_NAME.Text;
                // string split = CUSTOMER_NAME.Text.Substring(0, 34);
                //string A=CUSTOMER_NAME.Text;
                //string split1 = A.Substring(A.Length - 5, 5);

                e.Graphics.DrawString(": " + CUSTOMER_NAME.Text, Headerfont5, new SolidBrush(Color.Black), 490, 278);
                //e.Graphics.DrawString(": " + split1, Headerfont5, new SolidBrush(Color.Black), 490, 298);
                e.Graphics.DrawString(": " + ADDRESS_A, Headerfont5, new SolidBrush(Color.Black), 490, 298);
                e.Graphics.DrawString(": " + phone_no, Headerfont5, new SolidBrush(Color.Black), 490, 318);
                e.Graphics.DrawString(": " + tin_no, Headerfont5, new SolidBrush(Color.Black), 490, 338);
                e.Graphics.DrawString(": " + "Kerala", Headerfont5, new SolidBrush(Color.Black), 490, 358);
            }

            if (check_shipping.Checked == true)
            {
                e.Graphics.DrawString(": " + txt_shiping_name.Text, Headerfont5, new SolidBrush(Color.Black), 490, 278);
                e.Graphics.DrawString(": " + txt_shipping_address.Text, Headerfont5, new SolidBrush(Color.Black), 490, 298);
                e.Graphics.DrawString(": " + txt_shipping_address1.Text, Headerfont5, new SolidBrush(Color.Black), 490, 318);
                e.Graphics.DrawString(": " + txt_shipping_gstin.Text, Headerfont5, new SolidBrush(Color.Black), 490, 338);
                e.Graphics.DrawString(": " + txt_shipping_state.Text, Headerfont5, new SolidBrush(Color.Black), 490, 358);
            }

            e.Graphics.DrawRectangle(blackPen, 310, 357, 130, 18); //small RECTANGLE for state code in details of reciever and cosignee
            e.Graphics.DrawLine(blackPen, 400, 357, 400, 375); // V LINE 1 in state code
            e.Graphics.DrawString("State Code :" + "       " + stateCode, Headerfont5, new SolidBrush(Color.Black), 315, 358);
            e.Graphics.DrawRectangle(blackPen, 665, 357, 118, 18); //small RECTANGLE for state code in details of reciever and cosignee 130
            e.Graphics.DrawLine(blackPen, 755, 357, 755, 375); // V LINE 1 in state code
            e.Graphics.DrawString("State Code :" + "       " + stateCode, Headerfont5, new SolidBrush(Color.Black), 670, 358);
            e.Graphics.DrawLine(blackPen, 7, 375, 784, 375); // H LINE for ending the details of reciever and cosignee
            e.Graphics.DrawLine(blackPen, 7, 393, 784, 393); // H LINE below the details of reciever and cosignee
            e.Graphics.DrawLine(blackPen, 7, 435, 784, 435); // H LINE 

            offset = offset + 357;
            //   string headtext = "Sl.".PadRight(6) + "Name of Product/Service".PadRight(26) + "HSN".PadRight(6) + "UOM".PadRight(5) + "Qty".PadRight(8) + "Rate".PadRight(8) + "Amount".PadRight(9) + "Less:".PadRight(10) + "Taxable".PadRight(16) + "CGST".PadRight(18) + "SGST".PadRight(18) + "IGST".PadRight(18) + "Total";
            string headtext = "Sl.".PadRight(6) + "Name of Product/Service".PadRight(26) + "HSN".PadRight(18) + "Qty".PadRight(8) + "Rate".PadRight(8) + "Amount".PadRight(9) + "Less:".PadRight(8) + "Taxable".PadRight(16) + "CGST".PadRight(18) + "SGST".PadRight(18) + "IGST".PadRight(18) + "Total";
            e.Graphics.DrawString(headtext, printFontBold, new SolidBrush(Color.Black), startx - 43, starty + offset);
            e.Graphics.DrawString("No.", printFontBold, new SolidBrush(Color.Black), startx - 43, starty + offset + 16);
            e.Graphics.DrawString("ACN.", printFontBold, new SolidBrush(Color.Black), startx + 135, starty + offset + 16);
             e.Graphics.DrawString("Disc.", printFontBold, new SolidBrush(Color.Black), startx + 345, starty + offset + 16);
            e.Graphics.DrawString("Value.", printFontBold, new SolidBrush(Color.Black), startx + 385, starty + offset + 16);
            string text = "Rate" + " " + "Amount";
            e.Graphics.DrawString(text, printFontBold, new SolidBrush(Color.Black), startx + 438, starty + offset + 20);
            e.Graphics.DrawString(text, printFontBold, new SolidBrush(Color.Black), startx + 518, starty + offset + 20);
            e.Graphics.DrawString(text, printFontBold, new SolidBrush(Color.Black), startx + 597, starty + offset + 20);
            e.Graphics.DrawLine(blackPen, 7, 800, 784, 800); // H LINE 
            e.Graphics.DrawLine(blackPen, 30, 393, 30, 800); // v LINE for slno
            e.Graphics.DrawLine(blackPen, 180, 393, 180, 800); // v LINE for name of product
            e.Graphics.DrawLine(blackPen, 247, 393, 247, 800); // v LINE for hsn
            //e.Graphics.DrawLine(blackPen, 255, 393, 255, 830); // v LINE for uom
            e.Graphics.DrawLine(blackPen, 285, 393, 285, 830); // v LINE for qty
            e.Graphics.DrawLine(blackPen, 333, 393, 333, 830); // v LINE for rate
            e.Graphics.DrawLine(blackPen, 390, 393, 390, 830); // v LINE for amount
            e.Graphics.DrawLine(blackPen, 433, 393, 433, 830); // v LINE for LESS
            e.Graphics.DrawLine(blackPen, 490, 393, 490, 830); // v LINE for TAXABLE
            e.Graphics.DrawLine(blackPen, 570, 393, 570, 830); // v LINE for cgst
            e.Graphics.DrawLine(blackPen, 649, 393, 649, 830); // v LINE for sgst
            e.Graphics.DrawLine(blackPen, 726, 393, 726, 830); // v LINE for igst
            e.Graphics.DrawLine(blackPen, 490, 413, 726, 413); // H LINE in between cgst and igst
            e.Graphics.DrawLine(blackPen, 520, 413, 520, 800); // v LINE in between cgst
            e.Graphics.DrawLine(blackPen, 600, 413, 600, 800); // v LINE in between sgst
            e.Graphics.DrawLine(blackPen, 680, 413, 680, 800); // v LINE in between igst
            e.Graphics.DrawLine(blackPen, 7, 830, 784, 830); // H LINE for all total amount
            //    e.Graphics.DrawString("Tin No:" + TineNo, Headerfont2, new SolidBrush(Color.Black), 45, starty + 40);
            //    e.Graphics.DrawString("CST No:" + cst, Headerfont2, new SolidBrush(Color.Black), 45, starty + 55);
            //    offset = offset + 16;
            //    e.Graphics.DrawString("Invoice No:" + VOUCHNUM.Text, Headerfont2, new SolidBrush(Color.Black), 45, starty + offset + 80);
            //    offset = offset + 16;
            //    e.Graphics.DrawString("Date:", Headerfont2, new SolidBrush(Color.Black), 605, starty + offset + 65);
            //    e.Graphics.DrawString("Purchase Order No:" + tb_pono.Text, Headerfont2, new SolidBrush(Color.Black), 480, starty + offset + 90);
            //    e.Graphics.DrawString("Telephone No:" + tele_no, Headerfont2, new SolidBrush(Color.Black), 480, starty + offset + 108);
            //    e.Graphics.DrawString("Tin No:" + tin_no, Headerfont2, new SolidBrush(Color.Black), 480, starty + offset + 126);
            //    e.Graphics.DrawString("Mobile No:" + phone_no, Headerfont2, new SolidBrush(Color.Black), 480, starty + offset + 145);
            //    e.Graphics.DrawString(DOC_DATE_GRE.Text, Headerfont2, new SolidBrush(Color.Black), 630, starty + offset + 65); 
            StringFormat format = new StringFormat(StringFormatFlags.DirectionRightToLeft);
            float fontheight = font.GetHeight();
            try
            {
                int i = 0;
                int printpoint = 22; //32
                int j = 1;
                string mrp = "";
                string hsn = "";
                int nooflines = 0;
                double pq;
                string total = "";
                foreach (DataGridViewRow row in dgItems.Rows)
                {
                    PRINTTOTALPAGE = false;
                    if (j > printeditems)
                    {
                        if (nooflines < 13)
                        {
                            m = m + 1;
                            string tax;
                            i = i + 1;
                            int ORGLENGTH = row.Cells["cName"].Value.ToString().Length; /////32

                            string name = row.Cells["cName"].Value.ToString().Length <= 22 ? row.Cells[1].Value.ToString() : row.Cells[1].Value.ToString().Substring(0, 22);
                            string name2 = "";
                            int BALANCELENGH = ORGLENGTH - 22;
                            tax = row.Cells["cTaxPer"].Value.ToString();
                            sgsttax = (Convert.ToDouble(tax) / 2);
                            rate = row.Cells["cPrice"].Value.ToString();
                            double rate1 = Convert.ToDouble(row.Cells["cPrice"].Value);
                            try
                            {
                                mrp = row.Cells["cMrp"].Value.ToString();
                            }
                            catch
                            {
                                mrp = "0";
                            }
                            try
                            {
                                free = row.Cells["Cfree"].Value.ToString();
                            }
                            catch
                            {
                                free = "0";
                            }
                            uom = row.Cells["cUnit"].Value.ToString();
                            qty = row.Cells["cQty"].Value.ToString();
                            grossvalue = row.Cells["cGTotal"].Value.ToString();
                            netvalue = row.Cells["cNetValue"].Value.ToString();
                            netvalueLessDisc = Convert.ToDecimal(row.Cells["cQty"].Value) * Convert.ToDecimal(row.Cells["cPrice"].Value);
                            totalvalue = row.Cells["cTotal"].Value.ToString();
                            if (row.Cells["uHSNNO"].Value != null)
                            {
                                hsn = Convert.ToString(row.Cells["uHSNNO"].Value);
                            }
                            //if (free != "0")
                            //{
                            //    qty = (Convert.ToInt32(qty) - Convert.ToInt32(free)).ToString();
                            //}
                            disc = row.Cells["cDisc"].Value.ToString();
                            tcdis = tcdis + Convert.ToDecimal(disc);

                            gross = (Convert.ToDouble(qty) * Convert.ToDouble(rate)).ToString("N2");
                            price = Convert.ToDouble(row.Cells[12].Value).ToString();
                            taxamt = Convert.ToDouble(row.Cells["cTaxAmt"].Value);
                            sgstaxamt = (taxamt / 2);

                            //if (taxval != 0)
                            // {
                            //txbval = (Convert.ToDouble(qty) * Convert.ToDouble(rate) - Convert.ToDouble(disc)).ToString("N2");
                            //ttaxbl = ttaxbl + Convert.ToDecimal(txbval);
                            // }
                            // else
                            // {
                            // txbval = (Convert.ToDouble(qty) * Convert.ToDouble(rate) - Convert.ToDouble(disc)).ToString("N2");
                            // }                              
                            //pricWtax = Convert.ToDouble(rate) + (Convert.ToDouble(rate) * Convert.ToDouble(qty)) * (Convert.ToDouble(tax) / 100 / Convert.ToDouble(qty));
                            try
                            {
                                tfree = tfree + Convert.ToInt32(free);
                            }
                            catch
                            {
                            }
                            tqty = tqty + Convert.ToInt32(qty);
                            //tgrossrate = tgrossrate + Convert.ToDecimal(gross);
                            // ttaxva = ttaxva + Convert.ToDecimal(taxval);
                            trate = trate + Convert.ToDecimal(rate);
                            // a = a + Convert.ToDecimal(pricWtax);
                            string productline = name + tax + qty + rate + price;
                            e.Graphics.DrawString(m.ToString(), font, new SolidBrush(Color.Black), startx - 40, starty + offset + 50);
                            e.Graphics.DrawString(name, Headerfont2, new SolidBrush(Color.Black), startx - 23, starty + offset + 50);
                            e.Graphics.DrawString(hsn, Headerfont2, new SolidBrush(Color.Black), startx + 155, starty + offset + 50);
                            e.Graphics.DrawString(qty, font, new SolidBrush(Color.Black), startx + 230, starty + offset + 50, format);
                            //e.Graphics.DrawString(free, font, new SolidBrush(Color.Black), startx + 305, starty + offset);
                            //e.Graphics.DrawString(mrp, font, new SolidBrush(Color.Black), startx + 335, starty + offset);
                            e.Graphics.DrawString(rate1.ToString(decimalFormat), font, new SolidBrush(Color.Black), startx + 280, starty + offset + 50, format);
                            e.Graphics.DrawString(grossvalue, font, new SolidBrush(Color.Black), startx + 340, starty + offset + 50, format);
                            e.Graphics.DrawString(disc, font, new SolidBrush(Color.Black), startx + 384, starty + offset + 50, format);
                            e.Graphics.DrawString(netvalue, font, new SolidBrush(Color.Black), startx + 440, starty + offset + 50, format);
                            e.Graphics.DrawString(sgsttax.ToString() + "%", font, new SolidBrush(Color.Black), startx + 470, starty + offset + 50, format);
                            e.Graphics.DrawString(sgstaxamt.ToString(), font, new SolidBrush(Color.Black), startx + 520, starty + offset + 50, format);
                            e.Graphics.DrawString(sgsttax.ToString() + "%", font, new SolidBrush(Color.Black), startx + 552, starty + offset + 50, format);
                            e.Graphics.DrawString(sgstaxamt.ToString(), font, new SolidBrush(Color.Black), startx + 600, starty + offset + 50, format);
                            e.Graphics.DrawString(totalvalue, font, new SolidBrush(Color.Black), startx + 735, starty + offset + 50, format);
                            tgrossrate = tgrossrate + Convert.ToDecimal(grossvalue);
                            ttaxblevalue = ttaxblevalue + Convert.ToDecimal(netvalueLessDisc);
                            ttaxamount = ttaxamount + Convert.ToDecimal(taxamt);
                            divtaxamt = ttaxamount / 2;
                            ttotalvalue = ttotalvalue + Convert.ToDecimal(totalvalue);
                            totalNet = Convert.ToDecimal(NET_AMOUNT.Text);
                            totalDisc = tcdis + Convert.ToDecimal(DiscAmt);
                            // gsttaxamt = ttaxamount + ttaxamount;

                            //if (taxval != 0)
                            //{
                            //    e.Graphics.DrawString(txbval, font, new SolidBrush(Color.Black), startx + 535, starty + offset);
                            //}
                            //else
                            //{
                            //    e.Graphics.DrawString("0", font, new SolidBrush(Color.Black), startx + 535, starty + offset);
                            //}
                            //e.Graphics.DrawString(taxval.ToString("N2"), font, new SolidBrush(Color.Black), startx + 595, starty + offset);                            
                            //total = row.Cells["cTotal"].Value.ToString();
                            //e.Graphics.DrawString(total, font, new SolidBrush(Color.Black), startx + 645, starty + offset);////total individual amount
                            offset = offset + (int)fontheight + 2;
                            while (BALANCELENGH > 1)
                            {
                                name2 = BALANCELENGH <= 22 ? row.Cells["cName"].Value.ToString().Substring(printpoint, BALANCELENGH) : row.Cells["cName"].Value.ToString().Substring(printpoint, 22);
                                e.Graphics.DrawString(name2, Headerfont2, new SolidBrush(Color.Black), startx - 21, starty + offset + 50);
                                BALANCELENGH = BALANCELENGH - 22;
                                printpoint = printpoint + 22;
                                starty = starty + (int)fontheight;
                            }
                            printpoint = 22;
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
                            e.Graphics.DrawString("coutinue...", printFontBold, new SolidBrush(Color.Black), startx + 40, 901);
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

            float newoffset = 900;

            if (!PRINTTOTALPAGE)
            {
                PAGETOTAL = true;
                if (PAGETOTAL)
                {
                    try
                    {
                        e.Graphics.DrawString("Total", printFontBold, new SolidBrush(Color.Black), 215, 805);
                        e.Graphics.DrawString(tqty.ToString(), font, new SolidBrush(Color.Black), 255, 805);
                        e.Graphics.DrawString(tgrossrate.ToString(decimalFormat), font, new SolidBrush(Color.Black), 333, 805); // total amount
                        e.Graphics.DrawString(tcdis.ToString(), font, new SolidBrush(Color.Black), 388, 805);  // total discount
                        e.Graphics.DrawString(ttaxblevalue.ToString(decimalFormat), font, new SolidBrush(Color.Black), 433, 805); // total taxble value
                        e.Graphics.DrawString(divtaxamt.ToString(), font, new SolidBrush(Color.Black), 495, 805); // total cgst tax
                        e.Graphics.DrawString(divtaxamt.ToString(), font, new SolidBrush(Color.Black), 570, 805); // total sgst tax
                        e.Graphics.DrawString(ttotalvalue.ToString(), font, new SolidBrush(Color.Black), 726, 805); // total sgst tax

                        e.Graphics.DrawLine(blackPen, 7, 930, 784, 930); // H LINE bottom
                        e.Graphics.DrawLine(blackPen, 510, 830, 510, 1060); // v LINE separtion in footer
                        e.Graphics.DrawString("Total Amount Before Tax", Headerfont5, new SolidBrush(Color.Black), 510, 833);
                        e.Graphics.DrawString(ttaxblevalue.ToString(decimalFormat), Headerfont5, new SolidBrush(Color.Black), 700, 833); // total taxble value

                        e.Graphics.DrawLine(blackPen, 510, 850, 784, 850); // H LINE bottom
                        e.Graphics.DrawString("Add : CGST", Headerfont5, new SolidBrush(Color.Black), 510, 853);
                        e.Graphics.DrawString(divtaxamt.ToString(), Headerfont5, new SolidBrush(Color.Black), 700, 853); // total cgst tax

                        e.Graphics.DrawLine(blackPen, 510, 870, 784, 870); // H LINE bottom
                        e.Graphics.DrawString("Add : SGST", Headerfont5, new SolidBrush(Color.Black), 510, 873);
                        e.Graphics.DrawString(divtaxamt.ToString(), Headerfont5, new SolidBrush(Color.Black), 700, 873); // total cgst tax

                        e.Graphics.DrawLine(blackPen, 510, 890, 784, 890); // H LINE bottom
                        e.Graphics.DrawString("Add : IGST", Headerfont5, new SolidBrush(Color.Black), 510, 893);

                        e.Graphics.DrawLine(blackPen, 510, 910, 784, 910); // H LINE bottom
                      //  e.Graphics.DrawString("Tax Amount : GST", Headerfont5, new SolidBrush(Color.Black), 510, 913);
                        e.Graphics.DrawString("Total Discount:", Headerfont5, new SolidBrush(Color.Black), 510, 913);
                        //e.Graphics.DrawString(ttaxamount.ToString(), Headerfont5, new SolidBrush(Color.Black), 700, 913); // total cgst tax
                        e.Graphics.DrawString(totalDisc.ToString(), Headerfont5, new SolidBrush(Color.Black), 700, 913); // total cgst tax

                        e.Graphics.DrawString("Total Amount After Tax", Headerfont5, new SolidBrush(Color.Black), 510, 933);
                        //e.Graphics.DrawString(ttotalvalue.ToString(), Headerfont5, new SolidBrush(Color.Black), 700, 933); // total cgst tax
                        e.Graphics.DrawString (totalNet.ToString(), Headerfont5, new SolidBrush(Color.Black), 700, 933); // total cgst tax
                        e.Graphics.DrawString("GST Payable on Reverse Charge", Headerfont5, new SolidBrush(Color.Black), 510, 963);
                        e.Graphics.DrawLine(blackPen, 400, 930, 400, 1060); // v LINE separtion of footer in near right side of bank details
                        e.Graphics.DrawLine(blackPen, 7, 980, 400, 980); // H LINE bottom
                        e.Graphics.DrawLine(blackPen, 510, 980, 780, 980); // H LINE bottom
                        e.Graphics.DrawLine(blackPen, 510, 950, 780, 950); // H LINE bottom
                        e.Graphics.DrawLine(blackPen, 510, 960, 780, 960); // H LINE bottom 796
                        e.Graphics.DrawLine(blackPen, 700, 830, 700, 950);  //v line in total amount
                        e.Graphics.DrawLine(blackPen, 700, 960, 700, 980);  //v line in total amount

                        e.Graphics.DrawString("Certified that the particulars given above are true", Headerfont5, new SolidBrush(Color.Black), 510, 982);
                        e.Graphics.DrawString("and correct.", Headerfont5, new SolidBrush(Color.Black), 510, 1000);
                     //   e.Graphics.DrawString("Alint Enterprises", Headerfont5, new SolidBrush(Color.Black), 615, 1025);
                        e.Graphics.DrawString("Authorised Signatory", Headerfont5, new SolidBrush(Color.Black), 605, 1040);
                        e.Graphics.DrawString("Total Invoice Amount in Words:", printFontBold, new SolidBrush(Color.Black), 165, 832);
                        e.Graphics.DrawString("Bank Details:", printFontBold, new SolidBrush(Color.Black), 175, 930);
                        e.Graphics.DrawString("Bank Account Number", Headerfont5, new SolidBrush(Color.Black), 7, 945);
                        e.Graphics.DrawString("Bank Branch IFSC", Headerfont5, new SolidBrush(Color.Black), 7, 962);

                         if (CUSTOMER_CODE.Text != "" && CUSTOMER_NAME.Text != "")
                         {
                        e.Graphics.DrawString(":" + accountNumber, Headerfont5, new SolidBrush(Color.Black), 155, 947);
                        e.Graphics.DrawString(":" + ifscCode, Headerfont5, new SolidBrush(Color.Black), 155, 962);
                         }

                        e.Graphics.DrawString("(Common Seal)", Headerfont2, new SolidBrush(Color.Black), 415, 1040);
                        e.Graphics.DrawString("Terms and Conditions:", printFontBold, new SolidBrush(Color.Black), 140, 978);
                       // e.Graphics.DrawString("(To be furnished by the seller)", Headerfont5, new SolidBrush(Color.Black), 120, 992);
                        //e.Graphics.DrawString("Certified that all the particulars in the above tax invoice", Headerfont5, new SolidBrush(Color.Black), 50, 1007);
                      //  e.Graphics.DrawString("are true and correct and that my/our  registration under", Headerfont5, new SolidBrush(Color.Black), 55, 1025);
                       // e.Graphics.DrawString("KVAT ACT 2003 is valued as on the date of this bill", Headerfont5, new SolidBrush(Color.Black), 60, 1043);
                        try
                        {
                            //int cash = (int)Convert.ToDouble(ttotalvalue);
                            int cash = (int)Convert.ToDouble(totalNet);
                            string cas = totalNet.ToString();
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
                            e.Graphics.DrawString(test3, printbold, new SolidBrush(Color.Black), 7, 851);
                        }
                        catch
                        {
                        }
                    }
                    catch
                    {
                    }
                }
                PAGETOTAL = false;
            }
            newoffset = newoffset + 25;
            e.HasMorePages = hasmorepages;
            //ac_number = "";
            //ifsc = "";
        }



        private void printGSTPage_ForMalaysia(PrintPageEventArgs e, String transportMode, String vehicleNumber, String dateOfSupply, String placeOfSupply, String customerName, String customerAddress, String customerGSTTin, String state, String stateCode, String accountNumber, String ifscCode)
        {

            Company company = Common.getCompany();
            m = 0;
            bool hasmorepages = false;
            float xpos;
            int startx = 50;
            int starty = 20;
            int offset = 15;
            int headerstartposition = 50;
            bool PRINTTOTALPAGE = true;
            int w = e.MarginBounds.Width / 2;
            int x = e.MarginBounds.Left;
            int y = e.MarginBounds.Top;

            Font Headerfont1 = new Font("Calibri", 15, FontStyle.Bold);
            Font dec = new Font("Calibri", 8, FontStyle.Regular);
            Font Headerfont2 = new Font("Times New Roman", 9, FontStyle.Bold);
            Font Headerfont0 = new Font("Times New Roman", 9, FontStyle.Regular);
            Font Headerfont5 = new Font("Times New Roman", 14, FontStyle.Bold);
            Font printFont = new Font("Calibri", 10);
            Font printFontBold = new Font("Calibri", 10, FontStyle.Bold);
            Font printFontBold1 = new Font("Calibri", 14, FontStyle.Bold);
            Font printFonttotal = new Font("Calibri", 10, FontStyle.Bold);
            Font printFontnet = new Font("Calibri", 11, FontStyle.Bold);
            Font printbold = new Font("Calibri", 10, FontStyle.Bold);
            string address1 = "";
            string address2 = "";
            string address0 = "";
            string disc = "";
            string qty = "";
            string rate = "";
            string gross = "";
            string price = "";
            double taxval = 0;
            string txbval = "";
            string free = "0";
            var tabDataForeColor = Color.Black;
            int height = 100 + y;
            Pen blackPen1 = new Pen(Color.Black, 1);
            var txtDataWidth = e.Graphics.MeasureString(CompanyName, Headerfont1).Width;

            try
            {
                if (logo != null || logo != "")
                {

                    System.Drawing.Image img = System.Drawing.Image.FromFile(logo);

                    Point loc = new Point(20, 50);
                    e.Graphics.DrawImage(img, new Rectangle(310, 5, 230, 70));
                }
            }
            catch (Exception ex)
            {

            }
            //else
            //{
            address0 = company.Name;
            address1 = company.Address;
            address2 = company.Other_details;
            string mob1 = company.Phone;
            string mob2 = "";
            string email = company.Email;
            String gsttin = "TIN : " + company.TIN_No;
            //Image iM = Image.FromFile("d://alintlogo.jpg");
            //Point p = new Point(100, 100);
            /////e.Graphics.DrawImage(i, new Rectangle(10, 10, 230, 60));
            //e.Graphics.DrawImage(iM, new Rectangle(303, 5, 230, 70));
            int centerOfPage = e.PageBounds.Width / 2;
            int nameStartPosision = centerOfPage - TextRenderer.MeasureText(address0, Headerfont5).Width / 2;
            int addressStartPosition = centerOfPage - TextRenderer.MeasureText(address1, Headerfont2).Width / 2;
            int TAXINVOICE = centerOfPage - TextRenderer.MeasureText("TAX INVOICE", Headerfont5).Width / 2;


            e.Graphics.DrawString(address0, Headerfont5, new SolidBrush(Color.Black), 20, starty + 20);
            e.Graphics.DrawString(address1 + "," + "Mob:" + mob1 + "," + mob2, Headerfont2, new SolidBrush(Color.Black),20 , starty + 40);
            e.Graphics.DrawString("Email:" + email, Headerfont2, new SolidBrush(Color.Black),20, starty  + 55);
            e.Graphics.DrawString("TAX INVOICE", Headerfont5, new SolidBrush(Color.Black),TAXINVOICE, starty + offset+50);
            //e.Graphics.DrawString("", Headerfont2, new SolidBrush(Color.Black), 370, starty + offset + 95);
            ///////e.Graphics.DrawLine(blackPen1, 45, 150, 760,150); //940
            e.Graphics.DrawLine(blackPen1, 45, 155, 760, 155); //940
        
            double pricWtax = 0;
            decimal a = 0;

            using (var sf = new StringFormat())
            {

                height += 15;
                sf.LineAlignment = StringAlignment.Center;
                sf.Alignment = StringAlignment.Center;

                xpos = e.MarginBounds.Left + (e.MarginBounds.Width / 2);

            

                offset = offset + 20;
                ///// e.Graphics.DrawString(Address1 + "," + Address2, Headerfont2, new SolidBrush(Color.Black), headerstartposition, starty + 20 + offset);
                offset = offset + 12;
                /////e.Graphics.DrawString("Phone:".PadRight(3) + Phone, Headerfont2, new SolidBrush(Color.Black), headerstartposition, starty + 20 + offset);
                offset = offset + 12;
                // e.Graphics.DrawString("Email:".PadRight(3) + Email, Headerfont2, new SolidBrush(Color.Black), headerstartposition, starty + 20 + offset);
                offset = offset + 12;
                // e.Graphics.DrawString("Website:".PadRight(3) + Website, Headerfont2, new SolidBrush(Color.Black), headerstartposition, starty + 20 + offset);
                offset = offset + 21;
                offset = offset + 11;
                //////////e.Graphics.DrawString("        THE KERALA VALUE ADDED TAX RULES,2005                                              ,Tax Invoice                     [See rule 58(10),(Cash/Credit)]".PadLeft(10), Headerfont2, new SolidBrush(Color.Black), 20, starty + offset + 9);
                //////////e.Graphics.DrawString("Form No.8B".PadLeft(10), printbold, new SolidBrush(Color.Black), 370, starty + offset + 9);
                offset = offset + 16;
                offset = 15;
                ///////////e.Graphics.DrawString("Tin No       :" + TineNo, Headerfont2, new SolidBrush(Color.Black), 550, starty);
                e.Graphics.DrawString("CR No  :" + cst, Headerfont2, new SolidBrush(Color.Black), 605, starty + 40);
                e.Graphics.DrawString("GST No:" + TineNo, Headerfont2, new SolidBrush(Color.Black), 605, starty + 55);
                // e.Graphics.DrawString("CST No      : ", Headerfont2, new SolidBrush(Color.Black), 550, starty + offset);
                //offset = offset + 16;
                offset = offset + 16;
                ///////////e.Graphics.DrawString("Invoice No: ", Headerfont2, new SolidBrush(Color.Black), 550, starty + offset);
                e.Graphics.DrawString("Invoice No:" + VOUCHNUM.Text, Headerfont2, new SolidBrush(Color.Black), 45, starty + offset + 80);
                ///////e.Graphics.DrawString(Billno, printbold, new SolidBrush(Color.Black), 620, starty + offset);
                offset = offset + 16;
                e.Graphics.DrawString("Date:" + DOC_DATE_GRE.Text, Headerfont2, new SolidBrush(Color.Black), 605, starty + offset + 65);
                e.Graphics.DrawString("Purchase Order No:" + tb_pono.Text, Headerfont2, new SolidBrush(Color.Black), 480, starty + offset + 90);
                e.Graphics.DrawString("Telephone No:" + tele_no, Headerfont2, new SolidBrush(Color.Black), 480, starty + offset + 108);
                e.Graphics.DrawString("GST No:" + tin_no, Headerfont2, new SolidBrush(Color.Black), 480, starty + offset + 126);
                e.Graphics.DrawString("Mobile No:" + phone_no, Headerfont2, new SolidBrush(Color.Black), 480, starty + offset + 145);
                ///// e.Graphics.DrawString("Tin No:"+ tin_no, Headerfont2, new SolidBrush(Color.Black), 610, starty + offset + 85);



                //////////////////e.Graphics.DrawString("Date:", Headerfont2, new SolidBrush(Color.Black), 610, starty + offset + 65);
                //////////////////e.Graphics.DrawString("Purchase Order No:" + P_ORDER_NO.Text, Headerfont2, new SolidBrush(Color.Black), 570, starty + offset + 85);
                //////////////////e.Graphics.DrawString("Telephone No:" + tele_no, Headerfont2, new SolidBrush(Color.Black), 570, starty + offset + 115);
                //////////////////e.Graphics.DrawString("Mobile No:" + phone_no, Headerfont2, new SolidBrush(Color.Black), 570, starty + offset + 145);
                /////////////////////// e.Graphics.DrawString("Tin No:"+ tin_no, Headerfont2, new SolidBrush(Color.Black), 610, starty + offset + 85);
                //////////////////e.Graphics.DrawString("Tin No:" + tin_no, Headerfont2, new SolidBrush(Color.Black), 430, starty + offset + 145);





                /////////////e.Graphics.DrawString(DateTime.Now.ToString(), printbold, new SolidBrush(Color.Black), 620, starty + offset);
                //// e.Graphics.DrawString(DateTime.Now.ToString(), Headerfont2, new SolidBrush(Color.Black), 630, starty + offset + 65);
              //  e.Graphics.DrawString(DOC_DATE_GRE.Text, Headerfont2, new SolidBrush(Color.Black), 630, starty + offset + 65);
                offset = offset + 16;
                //  e.Graphics.DrawString("Del.Note No & Date :", Headerfont2, new SolidBrush(Color.Black), 580, starty + offset);
                offset = offset + 16;
                //   e.Graphics.DrawString("Des.Docu.No & Date:", Headerfont2, new SolidBrush(Color.Black), 580, starty + offset);
                offset = offset + 16;


                //e.Graphics.DrawString("Form No.8", Headerfont2, new SolidBrush(Color.Black), 510, starty + offset);

                // e.Graphics.DrawString("[See rule 58(10)]", Headerfont2, new SolidBrush(Color.Black), 510, starty + offset);

                // e.Graphics.DrawString("Tax Invoice/Cash/Credit", Headerfont2, new SolidBrush(Color.Black), 510, starty + offset);
                offset = offset + 16;
                offset = offset + 16;
                offset = offset + 16;
                offset = offset + 16;
                Pen blackPen = new Pen(Color.Black, 1);
                Point point1 = new Point(45, 190);
                Point point2 = new Point(760, 190);
                e.Graphics.DrawLine(blackPen, 45, 230, 760, 230); //(45,190,760,190)
                ////////  e.Graphics.DrawLine(blackPen, point1, point2); //(45,190,760,190)org

                //offset = offset + 2;
                //Point point5 = new Point(45, 130);
                //Point point6 = new Point(760, 130);
                //e.Graphics.DrawLine(blackPen, point5,point6);


                //offset = offset + 2;
                //Point point7 = new Point(45, 155);
                //Point point8 = new Point(760, 155);
                //  e.Graphics.DrawLine(blackPen, point7, point8);
                // e.Graphics.DrawLine(blackPen, 45, 130, 45, 130);
                ////////// e.Graphics.DrawRectangle(blackPen, 45, 130, 715, 25);




                e.Graphics.DrawString("To:" + CUSTOMER_NAME.Text, Headerfont5, new SolidBrush(Color.Black), startx, starty + offset - 20);
                e.Graphics.DrawString(ADDRESS_A, Headerfont2, new SolidBrush(Color.Black), startx, starty + offset);

                Font itemhead = new Font("Times New Roman", 8);
                offset = offset + 2;

                Point point3 = new Point(45, 219);
                Point point4 = new Point(760, 219);
                e.Graphics.DrawLine(blackPen, 45, 250, 760, 250);//(45,219,760,219)
                //////////e.Graphics.DrawLine(blackPen, point3, point4);//(45,219,760,219) org




                e.Graphics.DrawLine(blackPen, 45, 155, 45, 900);
                ////e.Graphics.DrawLine(blackPen, 45, 230, 45, 900);

                //zed invoice

                //e.Graphics.DrawLine(blackPen, 45, 110, 45, 160);
                // e.Graphics.DrawLine(blackPen, 45 , 45 , 


                //////////////////////////////////e.Graphics.DrawLine(blackPen, 570, 230, 570, 920); //NEW net rate
                //////////////////////////////////e.Graphics.DrawLine(blackPen, 630, 230, 630, 947); //940 taxval

                ////////////e.Graphics.DrawLine(blackPen, 80, 230, 80, 900);
                ////////////e.Graphics.DrawLine(blackPen, 370, 230, 370, 920); // mrp
                ////////////e.Graphics.DrawLine(blackPen, 420, 230, 420, 920); //tax  
                ////////////e.Graphics.DrawLine(blackPen, 465, 230, 465, 920);//qty
                ////////////e.Graphics.DrawLine(blackPen, 515, 230, 515, 947); //940 rate
                ////////////e.Graphics.DrawLine(blackPen, 570, 230, 570, 920); //NEW net rate
                ////////////e.Graphics.DrawLine(blackPen, 620, 230, 620, 947); //940 taxval
                ////////////e.Graphics.DrawLine(blackPen, 680, 230, 680, 920); //total
                ////// e.Graphics.DrawLine(blackPen, 280, 230, 280, 920); // mrp
                //////e.Graphics.DrawLine(blackPen, 320, 230, 320, 920); // mrp
                //////e.Graphics.DrawLine(blackPen, 355, 230, 355, 920);





                e.Graphics.DrawLine(blackPen, 80, 230, 80, 900);
                // e.Graphics.DrawLine(blackPen, 280, 230, 280, 920); // mrp
                //e.Graphics.DrawLine(blackPen, 280, 230, 280, 920); // mrp
                e.Graphics.DrawLine(blackPen, 280, 230, 280, 920); //tax  end line
                e.Graphics.DrawLine(blackPen, 320, 230, 320, 920);
                e.Graphics.DrawLine(blackPen, 355, 230, 355, 920);
                e.Graphics.DrawLine(blackPen, 385, 230, 385, 920);//new qty
                e.Graphics.DrawLine(blackPen, 425, 230, 425, 920);//qty
                e.Graphics.DrawLine(blackPen, 480, 230, 480, 920); //940 rate
                e.Graphics.DrawLine(blackPen, 540, 230, 540, 920); //NEW net rate
                e.Graphics.DrawLine(blackPen, 585, 230, 585, 920); //940 taxval
                e.Graphics.DrawLine(blackPen, 645, 230, 645, 920); //total
                e.Graphics.DrawLine(blackPen, 695, 230, 695, 920); //new total
                ///// e.Graphics.DrawLine(blackPen, 760, 230, 760, 920);
                e.Graphics.DrawLine(blackPen, 760, 155, 760, 920);

                ///////// e.Graphics.DrawLine(blackPen, 45, 900, 760, 900);
                e.Graphics.DrawLine(blackPen, 45, 900, 760, 900);
                e.Graphics.DrawRectangle(blackPen, 45, 900, 715, 20);
                /// e.Graphics.DrawRectangle(blackPen, 45, 920, 715, 20);
               ///////////////////////////////////////////// e.Graphics.DrawRectangle(blackPen, 45, 920, 715, 27);
                ///////// e.Graphics.DrawRectangle(blackPen, 45, 920, 715, 27); org
                offset = offset + 50;
                ///////offset = offset + 16;org
                //////// string headtext = "Sl.No".PadRight(10) + "Item".PadRight(100) + "Tax%".PadRight(10) + "Qty".PadRight(12) + "Rate".PadRight(10) + "Net Rate".PadRight(11) +"Tax Val".PadRight(13)+ "Total";
                string headtext = "Sl.No".PadRight(10) + "Item".PadRight(55) + "Tax%".PadRight(7) + "Qty".PadRight(5) + "UOM".PadRight(7) + "MRP".PadRight(9) + "Rate".PadRight(10) + "Gross".PadRight(15) + "Disc".PadRight(9) + "Txble Val".PadRight(15) + "Tax".PadRight(12) + "Total";
                ////    string headtext = "Sl.No".PadRight(10) + "Item".PadRight(85) + "Tax%".PadRight(10) + "Qty".PadRight(10) + "MRP".PadRight(12) + "Rate".PadRight(10) + "Tax Val".PadRight(10) + "Net Rate".PadRight(13) + "Total";
                e.Graphics.DrawString(headtext, printFontBold, new SolidBrush(Color.Black), startx - 4, starty + offset - 1);

                offset = offset + 40;
                Font font = new Font("Times New Roman", 10);
                float fontheight = font.GetHeight();



                try
                {

                    int i = 0;
                    int printpoint = 25; //32
                    int j = 1;
                    string mrp = "";
                    int nooflines = 0;
                    //int INCRIMENTHEIGHT = starty + offset;
                    double pq;
                    string total = "";


                    foreach (DataGridViewRow row in dgItems.Rows)
                    {
                        PRINTTOTALPAGE = false;
                        if (j > printeditems)
                        {
                            if (nooflines < 15)
                            {
                                m = m + 1;

                                string period, periodtype, tax, uom;
                                i = i + 1;
                                int ORGLENGTH = row.Cells["cName"].Value.ToString().Length; /////32

                                string name = row.Cells["cName"].Value.ToString().Length <= 30 ? row.Cells[1].Value.ToString() : row.Cells[1].Value.ToString().Substring(0, 30);
                                string name2 = "";
                                int BALANCELENGH = ORGLENGTH - 30;
                                tax = row.Cells["cTaxPer"].Value.ToString();
                                if (tax == "")
                                    tax = "1";
                                qty = row.Cells["cQty"].Value.ToString();
                                uom = row.Cells["cunit"].Value.ToString();
                                rate = row.Cells["cPrice"].Value.ToString();
                                try
                                {
                                    mrp = row.Cells["cMrp"].Value.ToString();
                                }
                                catch
                                {

                                    mrp = "0";
                                }
                                try
                                {
                                    free = row.Cells["Cfree"].Value.ToString();
                                }
                                catch
                                {
                                    free = "0";
                                }
                                disc = row.Cells["cDisc"].Value.ToString();
                                tcdis = tcdis + Convert.ToDecimal(disc);
                                gross = (Convert.ToDouble(qty) * Convert.ToDouble(rate)).ToString("N2");

                                price = Convert.ToDouble(row.Cells[12].Value).ToString();
                                //    string Serial = row.Cells["SerialNos"].Value.ToString();
                                taxval = Convert.ToDouble(row.Cells["cTaxAmt"].Value);
                                txbval = "";
                                if (taxval != 0)
                                {
                                    txbval = (Convert.ToDouble(qty) * Convert.ToDouble(rate) - Convert.ToDouble(disc)).ToString("N2");
                                    ttaxbl = ttaxbl + Convert.ToDecimal(txbval);
                                }
                                else
                                {
                                    txbval = (Convert.ToDouble(qty) * Convert.ToDouble(rate) - Convert.ToDouble(disc)).ToString("N2");

                                }
                                //double taxval = (Convert.ToDouble(rate) * Convert.ToDouble(qty)) * (Convert.ToDouble(tax) / 100 / Convert.ToDouble(qty));
                                //Math.Round(taxval, 2);
                                pricWtax = Convert.ToDouble(rate) + (Convert.ToDouble(rate) * Convert.ToDouble(qty)) * (Convert.ToDouble(tax) / 100 / Convert.ToDouble(qty));
                                // Math.Round(pricWtax, 2);
                                try
                                {
                                    tfree = tfree + Convert.ToInt32(free);
                                }
                                catch
                                {

                                }
                                tqty = tqty + Convert.ToInt32(qty);
                               // t = tqty + Convert.ToInt32(qty);
                                tgrossrate = tgrossrate + Convert.ToDecimal(gross);
                                ttaxva = ttaxva + Convert.ToDecimal(taxval);
                                trate = trate + Convert.ToDecimal(rate);
                                a = a + Convert.ToDecimal(pricWtax);
                                string productline = name + tax + qty + rate + price;
                                e.Graphics.DrawString(m.ToString(), font, new SolidBrush(Color.Black), startx, starty + offset);
                                e.Graphics.DrawString(name, font, new SolidBrush(Color.Black), startx + 30, starty + offset);
                                e.Graphics.DrawString(tax, font, new SolidBrush(Color.Black), startx + 230, starty + offset);
                                e.Graphics.DrawString(qty, font, new SolidBrush(Color.Black), startx + 270, starty + offset);
                                e.Graphics.DrawString(uom, font, new SolidBrush(Color.Black), startx + 305, starty + offset);
                                e.Graphics.DrawString(mrp, font, new SolidBrush(Color.Black), startx + 335, starty + offset);
                                e.Graphics.DrawString(rate, font, new SolidBrush(Color.Black), startx + 375, starty + offset);
                                e.Graphics.DrawString(gross, font, new SolidBrush(Color.Black), startx + 430, starty + offset);
                                e.Graphics.DrawString(disc, font, new SolidBrush(Color.Black), startx + 490, starty + offset);

                                if (taxval != 0)
                                {
                                    e.Graphics.DrawString(txbval, font, new SolidBrush(Color.Black), startx + 535, starty + offset);
                                }
                                else
                                {
                                    e.Graphics.DrawString("0", font, new SolidBrush(Color.Black), startx + 535, starty + offset);
                                }

                                e.Graphics.DrawString(taxval.ToString("N2"), font, new SolidBrush(Color.Black), startx + 595, starty + offset);

                                total = row.Cells["cTotal"].Value.ToString();
                                e.Graphics.DrawString(total, font, new SolidBrush(Color.Black), startx + 645, starty + offset);////total individual amount
                                offset = offset + (int)fontheight + 10;
                                if (Convert.ToString(row.Cells["description"].Value) != "")
                                {
                                    try
                                    {
                                        e.Graphics.DrawString(row.Cells["description"].Value.ToString(), font, new SolidBrush(Color.Black), startx + 30, starty + offset);
                                        offset = offset + (int)fontheight + 13;
                                    }
                                    catch
                                    {


                                    }


                                }

                                //if (Serial != "")
                                //{
                                //    e.Graphics.DrawString("SN No: " + Serial, font, new SolidBrush(Color.Black), startx + 30, starty + offset);

                                //    offset = offset + (int)fontheight + 10;
                                //}




                                while (BALANCELENGH > 1)
                                {

                                    ///44
                                    name2 = BALANCELENGH <= 30 ? row.Cells["cName"].Value.ToString().Substring(printpoint, BALANCELENGH) : row.Cells["cName"].Value.ToString().Substring(printpoint, 30);
                                    e.Graphics.DrawString(name2, font, new SolidBrush(Color.Black), startx + 30, starty + offset - 9);
                                    BALANCELENGH = BALANCELENGH - 30;
                                    printpoint = printpoint + 30;
                                    starty = starty + (int)fontheight;

                                    //wf = wf + 16;
                                    // offset += 3;
                                    // nooflines++;
                                }
                                printpoint = 30;

                                ////////starty = starty + (int)fontheight - 5;

                                //while (BALANCELENGH > 0)
                                //{


                                //    name2 = BALANCELENGH <= 30 ? row.Cells["cName"].Value.ToString().Substring(printpoint, BALANCELENGH) : row.Cells["cName"].Value.ToString().Substring(printpoint, 30);
                                //    e.Graphics.DrawString(name2, Headerfont2, new SolidBrush(Color.Black), startx + 30, starty + offset);
                                //    BALANCELENGH = BALANCELENGH - 30;
                                //    printpoint = printpoint + 30;
                                //    starty = starty + (int)fontheight;

                                //    // wf = wf + 16;
                                //    // offset += 3;
                                //    //nooflines++;
                                //}
                                //starty = starty + (int)fontheight - 5;
                                nooflines++;
                                j++;

                            }
                            else
                            {

                                printeditems = j - 1;
                                //  e.HasMorePages = true;
                                hasmorepages = true;
                                //m = m - 1;
                                PRINTTOTALPAGE = true;

                            }
                            if (hasmorepages == true)
                            {
                                //m = m + 1;
                                e.Graphics.DrawString("coutinue...", printFontBold, new SolidBrush(Color.Black), startx + 40, 901);
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
            //  e.Graphics.DrawString("E & OE", Headerfont2, new SolidBrush(Color.Black), startx, 1080);
            float newoffset = 900;
            if (!PRINTTOTALPAGE)
            {
                PAGETOTAL = true;
                if (PAGETOTAL)
                {
                    try
                    {
                     //   e.Graphics.DrawString("Grand Total", printFontBold1, new SolidBrush(Color.Black), startx + 436, 925);
                        e.Graphics.DrawString("Authorised Signatory", printFontBold, new SolidBrush(Color.Black), startx, 1050);
                        //e.Graphics.DrawString("Alint Enterprises", printFontBold, new SolidBrush(Color.Black), startx + 600, 1070);
                        e.Graphics.DrawString(tfree.ToString(), printFontBold, new SolidBrush(Color.Black), startx + 305, 901);
                        e.Graphics.DrawString(tqty.ToString(), printFontBold, new SolidBrush(Color.Black), startx + 270, 901);
                        e.Graphics.DrawString(tgrossrate.ToString("n2"), printFontBold, new SolidBrush(Color.Black), startx + 430, 901);
                        e.Graphics.DrawString(ttaxbl.ToString("n2"), printFontBold, new SolidBrush(Color.Black), startx + 535, 901);
                        e.Graphics.DrawString(ttaxva.ToString("n2"), printFontBold, new SolidBrush(Color.Black), startx + 595, 901);
                        e.Graphics.DrawString("Total", printFontBold, new SolidBrush(Color.Black), startx + 193, 901);
                      //  e.Graphics.DrawString("Discount: ", Headerfont2, new SolidBrush(Color.Black), startx + 180, 921);
                      //  e.Graphics.DrawString("Freight: ", Headerfont2, new SolidBrush(Color.Black), startx + 60, 921);
                      //  e.Graphics.DrawString("Roundoff: ", Headerfont2, new SolidBrush(Color.Black), startx + 290, 921);
                      //  e.Graphics.DrawString(tcdis.ToString("n2"), printFontBold, new SolidBrush(Color.Black), startx + 490, 901);
                      //  string neta = Convert.ToDecimal(NET_AMOUNT.Text).ToString("n2");
                      //  e.Graphics.DrawString(Spell.SpellAmount.comma(Convert.ToDecimal(neta)), printFontBold1, new SolidBrush(Color.Black), startx + 550, 925);
                        try
                        {
                            int cash = (int)Convert.ToDouble(NET_AMOUNT.Text);
                            string cas = NET_AMOUNT.Text;
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
                                test3 = test + " Renggit and " + test2 + " Sen only";
                                int index = test3.IndexOf("Halala");
                            }
                            if (i1 > 0 && i2 == 0)
                            {
                                string test = NumbersToWords(i1);
                                test3 = test + " Renggit only";
                            }
                            e.Graphics.DrawString(test3, Headerfont2, new SolidBrush(Color.Black), startx, 951);
                            StringFormat format11 = new StringFormat(StringFormatFlags.DirectionRightToLeft);
                            Pen blackP = new Pen(Color.Black, 1);
                            e.Graphics.DrawRectangle(blackP, 530, 955, 250, 123);
                            e.Graphics.DrawString("Sub Total (Excluding GST):", printFonttotal, new SolidBrush(Color.Black), 531, 960);
                            e.Graphics.DrawString("Discount:" + DISCOUNT.Text, printFonttotal, new SolidBrush(Color.Black), 531, 985);
                            e.Graphics.DrawString("GST 6%:", printFonttotal, new SolidBrush(Color.Black), 531, 1010);
                            e.Graphics.DrawLine(blackP, 530, 1025, 780, 1025);
                            e.Graphics.DrawString("Roundoff: ", printFonttotal, new SolidBrush(Color.Black), 531, 1030);
                            e.Graphics.DrawString("Total:", printFontBold1, new SolidBrush(Color.Black), 531, 1050);

                            e.Graphics.DrawString(TOTAL_AMOUNT.Text, printFonttotal, new SolidBrush(Color.Black), 780, 960,format11);
                            e.Graphics.DrawString(DISCOUNT.Text, printFonttotal, new SolidBrush(Color.Black), 780, 985, format11);
                            e.Graphics.DrawString(ttaxva.ToString(), printFonttotal, new SolidBrush(Color.Black), 780, 1010, format11);
                            e.Graphics.DrawString(txtRoundOff.Text, printFonttotal, new SolidBrush(Color.Black), 780, 1035, format11);

                            string neta = Convert.ToDecimal(NET_AMOUNT.Text).ToString("n2");

                            e.Graphics.DrawString(Spell.SpellAmount.comma(Convert.ToDecimal(neta)), printFontBold1, new SolidBrush(Color.Black), 780, 1050, format11);


                        }
                        catch
                        {
                        }

                        newoffset = newoffset + 20;
                        newoffset = newoffset + 20;
                        //e.Graphics.DrawString(DISCOUNT.Text, printFontBold, new SolidBrush(Color.Black), startx + 242, 921);
                      //  e.Graphics.DrawString(txtFreight.Text, printFontBold, new SolidBrush(Color.Black), startx + 110, 921);
                      //  e.Graphics.DrawString(txtRoundOff.Text, printFontBold, new SolidBrush(Color.Black), startx + 350, 921);
                        newoffset = newoffset + 20;
                    }
                    catch
                    {
                    }
                }

                PAGETOTAL = false;
            }
            newoffset = newoffset + 25;
            e.HasMorePages = hasmorepages;
    
        }



        void threeStarGST(PrintPageEventArgs e, String transportMode, String vehicleNumber, String dateOfSupply, String placeOfSupply, String customerName, String customerAddress, String customerGSTTin, String state, String stateCode, String accountNumber, String ifscCode)
        {
            Company company = Common.getCompany();

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
            Font Headerfont1 = new Font("Calibri", 15, FontStyle.Bold);
            Font dec = new Font("Calibri", 8, FontStyle.Regular);
            Font Headerfont2 = new Font("Times New Roman", 8, FontStyle.Regular);
            Font Headerfont5 = new Font("Times New Roman", 10, FontStyle.Regular);
            Font printFont = new Font("Calibri", 10);
            Font printFontBold = new Font("Calibri", 10, FontStyle.Bold);
            Font printFontBold1 = new Font("Calibri", 14, FontStyle.Bold);
            Font printFontnet = new Font("Calibri", 11, FontStyle.Bold);
            Font printbold = new Font("Calibri", 10, FontStyle.Bold);
            Font printFontBold2 = new Font("Times New Roman", 13, FontStyle.Bold);
            Font printFontBold3 = new Font("Times New Roman", 11, FontStyle.Bold);
            Font FONTHEAD = new Font("Arial Black", 14, FontStyle.Bold);
            Font FONTGST = new Font("Arial Unicode MS", 11, FontStyle.Bold);
            Font FONTGST1 = new Font("Arial Unicode MS", 9, FontStyle.Bold);
            Font font = new Font("Times New Roman", 8, FontStyle.Regular);
            string address1 = "";
            string address2 = "";
            string address0 = "";
            string disc = "";
            string qty = "";
            string rate = "";
            string gross = "";
            string price = "";
            double taxamt = 0;
            string txbval = "";
            string free = "0";
            string uom = "";
            string netvalue = "";
            decimal netvalueLessDisc = 0;
            string grossvalue = "";
            string totalvalue = "";
            double cgsttax = 0;
            double sgsttax = 0;
            decimal ttaxblevalue = 0;
            decimal ttaxamount = 0;
            decimal divtaxamt = 0;
            decimal ttotalvalue = 0;
            decimal totalNet = 0;
            decimal totalDisc = 0;
            //string sgsttax = "";
            double sgstaxamt = 0;
            float ssss = 0;
            var tabDataForeColor = Color.Black;
            int height = 100 + y;
            Pen blackPen1 = new Pen(Color.Black, 2);
            Pen blackPen = new Pen(Color.Black, 1);
            var txtDataWidth = e.Graphics.MeasureString(CompanyName, Headerfont1).Width;
            try
            {
                if (logo != null || logo != "")
                {
                    System.Drawing.Image img = System.Drawing.Image.FromFile(logo);
                    Point loc = new Point(20, 50);
                    e.Graphics.DrawImage(img, new Rectangle(303, 5, 230, 70));
                }
            }
            catch (Exception ex)
            {
            }

            int startX = 10;
            int startY = 12;
            address0 = company.Name;
            address1 = company.Address;
            address2 = company.Other_details;
            string mob1 = company.Phone;
            string mob2 = "";
            string email = company.Email;
            String gsttin = "GSTIN : " + company.TIN_No;
            int centerOfPage = e.PageBounds.Width / 2;

            int nameStartPosision = centerOfPage - TextRenderer.MeasureText("THREE STAR ENTERPRISES", FONTHEAD).Width / 2;


            if (logo == null || logo == "")
            {
                e.Graphics.DrawString("THREE STAR ENTERPRISES", FONTHEAD, new SolidBrush(Color.Black), nameStartPosision, startY);
            }

            startY += 20;
            e.Graphics.DrawString("Main Road, Vengara, Mob:9847366609, 9847061593", printFontBold2, new SolidBrush(Color.Black), startY + 200, startY);
            startY += 20;
            Rectangle rect = new Rectangle(startX, startY, 800, 55);
            SolidBrush blackBrush = new SolidBrush(Color.LightGray);
            e.Graphics.FillRectangle(blackBrush, rect);//GRAY COLOR RECTANGLE
            e.Graphics.DrawString("GSTIN: 32AYPPM4402R1ZZ", FONTGST, new SolidBrush(Color.Black), startX + 20, startY += 15);
            e.Graphics.DrawString("TAX INVOICE  ", FONTGST1, new SolidBrush(Color.Black), startX + 700, startY);
            e.Graphics.DrawString("CREDIT", FONTGST1, new SolidBrush(Color.Black), startX + 700, startY += 15);
            startY += 25;

            e.Graphics.DrawRectangle(blackPen, startX, startY, 800, 1060);//INVOICE/DATE
            e.Graphics.DrawLine(blackPen, 170, startY, 170, startY + 80);
            e.Graphics.DrawLine(blackPen, 340, startY, 340, startY + 80);
            e.Graphics.DrawLine(blackPen, 520, startY, 520, startY + 80);
            e.Graphics.DrawLine(blackPen, 660, startY, 660, startY + 80);

            //invoice no and date
            e.Graphics.DrawString("Invoice No & Date", printFontBold3, new SolidBrush(Color.Black), startX + 15, startY + 12);
            e.Graphics.DrawString(VOUCHNUM.Text + " & " + DOC_DATE_GRE.Value.ToShortDateString(), printFontBold3, new SolidBrush(Color.Black), startX + 15, startY + 50);

            //delivery note 
            e.Graphics.DrawString("Delivery Note No &", printFontBold3, new SolidBrush(Color.Black), startX + 170, startY + 7);
            e.Graphics.DrawString("Date", printFontBold3, new SolidBrush(Color.Black), startX + 180, startY + 20);
            e.Graphics.DrawString("           " + " " + txt_date.Value.ToShortDateString(), printFontBold3, new SolidBrush(Color.Black), startX + 170, startY + 50);

            //purchase order
            e.Graphics.DrawString("Purchase Order No &", printFontBold3, new SolidBrush(Color.Black), startX + 340, startY + 7);
            e.Graphics.DrawString("Date", printFontBold3, new SolidBrush(Color.Black), startX + 350, startY + 20);
            if (tb_pono.Text != "")
            {
                e.Graphics.DrawString(tb_pono.Text + " & " + DOC_DATE_GRE.Value.ToShortDateString(), printFontBold3, new SolidBrush(Color.Black), startX + 340, startY + 50);
            }
            else
            {
                e.Graphics.DrawString("            " + "" + DOC_DATE_GRE.Value.ToShortDateString(), printFontBold3, new SolidBrush(Color.Black), startX + 340, startY + 50);
            }

            e.Graphics.DrawString("Dispatch Doc No &", printFontBold3, new SolidBrush(Color.Black), startX + 520, startY + 7);
            e.Graphics.DrawString("Date", printFontBold3, new SolidBrush(Color.Black), startX + 530, startY + 20);
            e.Graphics.DrawString("Terms of Delivery", printFontBold3, new SolidBrush(Color.Black), startX + 660, startY + 7);
            e.Graphics.DrawString("if any", printFontBold3, new SolidBrush(Color.Black), startX + 670, startY + 20);

            startY += 40;

            e.Graphics.DrawLine(blackPen, startX, startY, 800 + startX, startY);//center of invoice No
            startY += 40;
            e.Graphics.DrawLine(blackPen, startX, startY, 800 + startX, startY); //end of invoice no
            e.Graphics.DrawString("Name and Address of Receiver:", printFontBold3, new SolidBrush(Color.Black), startX + 2, startY + 2);
            e.Graphics.DrawString("" + CUSTOMER_NAME.Text, Headerfont5, new SolidBrush(Color.Black), startX + 10, startY + 20);
            e.Graphics.DrawString("" + ADDRESS_A, Headerfont5, new SolidBrush(Color.Black), startX + 10, startY + 40);
            e.Graphics.DrawString("" + phone_no, Headerfont5, new SolidBrush(Color.Black), startX + 10, startY + 60);
            e.Graphics.DrawString("Kerala" + ":32", Headerfont5, new SolidBrush(Color.Black), startX + 10, startY + 80);
            startY += 140;
            e.Graphics.DrawString("GSTIN:" + tin_no, printFontBold3, new SolidBrush(Color.Black), startX + 2, startY - 18);
            e.Graphics.DrawLine(blackPen, startX, startY, 800 + startX, startY); //details of receiver


            string headtext = "Sl.".PadRight(8) + "HSN Code".PadRight(10) + "Commodity/Item".PadRight(28) + "Unit Price".PadRight(12) + "Qty".PadRight(12) + "Unit".PadRight(10) + "MRP".PadRight(12) + "Disc".PadRight(14) + "GST%".PadRight(12) + "    ".PadRight(18) + "    ".PadRight(36) + "Total";
            e.Graphics.DrawString(headtext, printFontBold, new SolidBrush(Color.Black), startX, startY + 15);
            e.Graphics.DrawString("CGST".PadRight(25) + "SGST", printFontBold, new SolidBrush(Color.Black), 580, startY + 8);
            e.Graphics.DrawString("%".PadRight(12) + "Amt", printFontBold, new SolidBrush(Color.Black), 565, startY + 30);
            e.Graphics.DrawString("%".PadRight(12) + "Amt", printFontBold, new SolidBrush(Color.Black), 655, startY + 30);

            e.Graphics.DrawLine(blackPen, startX, startY + 50, 800 + startX, startY + 50);

            e.Graphics.DrawLine(blackPen, startX, 830, startX + 800, 830); // H LINE 
            e.Graphics.DrawLine(blackPen, 40, startY, 40, 830); // v LINE for slno
            e.Graphics.DrawLine(blackPen, 101, startY, 101, 800); // v LINE for HSN
            e.Graphics.DrawLine(blackPen, 250, startY, 250, 800); // v LINE for NAME
            e.Graphics.DrawLine(blackPen, 310, startY, 310, 800); // v LINE for uNIT RATE
            e.Graphics.DrawLine(blackPen, 360, startY, 360, 830); // v LINE for qty
            e.Graphics.DrawLine(blackPen, 400, startY, 400, 800); // v LINE for UOM
            e.Graphics.DrawLine(blackPen, 460, startY, 460, 800); // v LINE for MRP
            e.Graphics.DrawLine(blackPen, 515, startY, 515, 830); // v LINE DISC
            e.Graphics.DrawLine(blackPen, 560, startY, 560, 830); // v LINE for GST
            e.Graphics.DrawLine(blackPen, 650, startY, 650, 830); // v LINE for cgst
            e.Graphics.DrawLine(blackPen, 600, startY + 25, 600, 830); // v LINE for sgst
            e.Graphics.DrawLine(blackPen, 690, startY + 25, 690, 830); // v LINE for igst
            e.Graphics.DrawLine(blackPen, 560, startY + 25, 740, startY + 25); // H LINE in between cgst and igst
            //e.Graphics.DrawLine(blackPen, 520, startY+25, 520, 800); // v LINE in between cgst
            //e.Graphics.DrawLine(blackPen, 600, startY, 600, 800); // v LINE in between sgst
            e.Graphics.DrawLine(blackPen, 740, startY, 740, 830); // v LINE in between igst
            e.Graphics.DrawLine(blackPen, startX, 800, 800 + startX, 800); // H LINE for all total amount
            startY += 61;
            StringFormat format = new StringFormat(StringFormatFlags.DirectionRightToLeft);
            float fontheight = font.GetHeight();
            try
            {
                int i = 0;
                int printpoint = 22; //32
                int j = 1;
                string mrp = "";
                string hsn = "";
                int nooflines = 0;
                double pq;
                string total = "";
                foreach (DataGridViewRow row in dgItems.Rows)
                {
                    PRINTTOTALPAGE = false;
                    if (j > printeditems)
                    {
                        if (nooflines < 20)
                        {
                            m = m + 1;
                            string tax;
                            i = i + 1;
                            int length = 30;
                            int ORGLENGTH = row.Cells["cName"].Value.ToString().Length; /////32

                            string name = row.Cells["cName"].Value.ToString().Length <= length ? row.Cells[1].Value.ToString() : row.Cells[1].Value.ToString().Substring(0, length);
                            string name2 = "";
                            int BALANCELENGH = ORGLENGTH - length;
                            tax = row.Cells["cTaxPer"].Value.ToString();
                            sgsttax = (Convert.ToDouble(tax) / 2);
                            rate = row.Cells["cPrice"].Value.ToString();
                            double rate1 = Convert.ToDouble(row.Cells["cPrice"].Value);
                            try
                            {
                                mrp = row.Cells["cMrp"].Value.ToString();
                            }
                            catch
                            {
                                mrp = "0";
                            }
                            try
                            {
                                free = row.Cells["Cfree"].Value.ToString();
                            }
                            catch
                            {
                                free = "0";
                            }
                            uom = row.Cells["cUnit"].Value.ToString();
                            qty = row.Cells["cQty"].Value.ToString();
                            grossvalue = row.Cells["cGTotal"].Value.ToString();
                            netvalue = row.Cells["cNetValue"].Value.ToString();
                            netvalueLessDisc = Convert.ToDecimal(row.Cells["cQty"].Value) * Convert.ToDecimal(row.Cells["cPrice"].Value);
                            totalvalue = row.Cells["cTotal"].Value.ToString();
                            if (row.Cells["uHSNNO"].Value != null)
                            {
                                hsn = Convert.ToString(row.Cells["uHSNNO"].Value);
                            }
                            //if (free != "0")
                            //{
                            //    qty = (Convert.ToInt32(qty) - Convert.ToInt32(free)).ToString();
                            //}
                            disc = row.Cells["cDisc"].Value.ToString();
                            tcdis = tcdis + Convert.ToDecimal(disc);

                            gross = (Convert.ToDouble(qty) * Convert.ToDouble(rate)).ToString("N2");
                            price = Convert.ToDouble(row.Cells[12].Value).ToString();
                            taxamt = Convert.ToDouble(row.Cells["cTaxAmt"].Value);
                            sgstaxamt = (taxamt / 2);

                            //if (taxval != 0)
                            // {
                            //txbval = (Convert.ToDouble(qty) * Convert.ToDouble(rate) - Convert.ToDouble(disc)).ToString("N2");
                            //ttaxbl = ttaxbl + Convert.ToDecimal(txbval);
                            // }
                            // else
                            // {
                            // txbval = (Convert.ToDouble(qty) * Convert.ToDouble(rate) - Convert.ToDouble(disc)).ToString("N2");
                            // }                              
                            //pricWtax = Convert.ToDouble(rate) + (Convert.ToDouble(rate) * Convert.ToDouble(qty)) * (Convert.ToDouble(tax) / 100 / Convert.ToDouble(qty));
                            try
                            {
                                tfree = tfree + Convert.ToInt32(free);
                            }
                            catch
                            {
                            }
                            tqty = tqty + Convert.ToInt32(qty);
                            //tgrossrate = tgrossrate + Convert.ToDecimal(gross);
                            // ttaxva = ttaxva + Convert.ToDecimal(taxval);
                            trate = trate + Convert.ToDecimal(rate);
                            // a = a + Convert.ToDecimal(pricWtax);
                            string productline = name + tax + qty + rate + price;
                            e.Graphics.DrawString(m.ToString(), font, new SolidBrush(Color.Black), startX + 1, startY);
                            e.Graphics.DrawString(name, Headerfont2, new SolidBrush(Color.Black), 101, startY);
                            e.Graphics.DrawString(hsn, font, new SolidBrush(Color.Black), 41, startY);
                            e.Graphics.DrawString(rate1.ToString(decimalFormat), font, new SolidBrush(Color.Black), 309, startY, format);
                            e.Graphics.DrawString(qty, font, new SolidBrush(Color.Black), 359, startY, format);
                            e.Graphics.DrawString(uom, Headerfont2, new SolidBrush(Color.Black), 364, startY);
                            e.Graphics.DrawString(mrp.ToString(), font, new SolidBrush(Color.Black), 460, startY, format);

                            e.Graphics.DrawString(disc, font, new SolidBrush(Color.Black), 515, startY, format);
                            e.Graphics.DrawString(tax + "%", font, new SolidBrush(Color.Black), 560, startY, format);
                            e.Graphics.DrawString(sgsttax.ToString() + "%", font, new SolidBrush(Color.Black), 600, startY, format);
                            e.Graphics.DrawString(sgstaxamt.ToString(), font, new SolidBrush(Color.Black), 650, startY, format);
                            e.Graphics.DrawString(sgsttax.ToString() + "%", font, new SolidBrush(Color.Black), 690, startY, format);
                            e.Graphics.DrawString(sgstaxamt.ToString(), font, new SolidBrush(Color.Black), 740, startY, format);
                            e.Graphics.DrawString(totalvalue, font, new SolidBrush(Color.Black), startX + 800, startY, format);
                            tgrossrate = tgrossrate + Convert.ToDecimal(grossvalue);
                            ttaxblevalue = ttaxblevalue + Convert.ToDecimal(netvalueLessDisc);
                            ttaxamount = ttaxamount + Convert.ToDecimal(taxamt);
                            ttaxva = ttaxamount;
                            divtaxamt = ttaxamount / 2;
                            ttotalvalue = ttotalvalue + Convert.ToDecimal(totalvalue);
                            totalNet = Convert.ToDecimal(NET_AMOUNT.Text);
                            totalDisc = tcdis + Convert.ToDecimal(DiscAmt);

                            offset = offset + (int)fontheight + 2;
                            startY += (int)fontheight + 5;
                            while (BALANCELENGH > 1)
                            {
                                name2 = BALANCELENGH <= length ? row.Cells["cName"].Value.ToString().Substring(printpoint, BALANCELENGH) : row.Cells["cName"].Value.ToString().Substring(printpoint, length);
                                e.Graphics.DrawString(name2, Headerfont2, new SolidBrush(Color.Black), 101, startY);
                                BALANCELENGH = BALANCELENGH - length;
                                printpoint = printpoint + length;
                                starty = starty + (int)fontheight;
                                startY += (int)fontheight;
                            }
                            printpoint = length;
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
                            e.Graphics.DrawString("coutinue...", printFontBold, new SolidBrush(Color.Black), 101, 799);
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

            float newoffset = 900;

            if (!PRINTTOTALPAGE)
            {
                PAGETOTAL = true;
                if (PAGETOTAL)
                {
                    try
                    {
                        startY = 830;
                        e.Graphics.DrawString("Total", printFontBold, new SolidBrush(Color.Black), 101, startY - 20);
                        e.Graphics.DrawString(tqty.ToString(), font, new SolidBrush(Color.Black), 359, startY - 20, format);
                        // e.Graphics.DrawString(tgrossrate.ToString(decimalFormat), font, new SolidBrush(Color.Black), 333, startY-15); // total amount
                        e.Graphics.DrawString(tcdis.ToString(), font, new SolidBrush(Color.Black), 515, startY - 20, format);  // total discount
                        // e.Graphics.DrawString(ttaxblevalue.ToString(decimalFormat), font, new SolidBrush(Color.Black), 433, startY-15); // total taxble value
                        e.Graphics.DrawString(divtaxamt.ToString(), font, new SolidBrush(Color.Black), 650, startY - 20, format); // total cgst tax
                        e.Graphics.DrawString(divtaxamt.ToString(), font, new SolidBrush(Color.Black), 740, startY - 20, format); // total sgst tax
                        e.Graphics.DrawString(ttotalvalue.ToString(), font, new SolidBrush(Color.Black), startX + 800, startY - 20, format); // total sgst tax
                        e.Graphics.DrawLine(blackPen, 510, startY, 510, 1060); // v LINE separtion in footer
                        e.Graphics.DrawString("Tax Details", FONTGST, new SolidBrush(Color.Black), startX + 10, startY + 5);
                        e.Graphics.DrawLine(blackPen, startX, startY + 30, 340, startY + 30);

                        e.Graphics.DrawLine(blackPen, startX + 53, startY + 30, startX + 53, startY + 120); // v LINE 
                        e.Graphics.DrawLine(blackPen, startX + 106, startY + 30, startX + 106, startY + 120); // v LINE 
                        e.Graphics.DrawLine(blackPen, startX + 159, startY + 30, startX + 159, startY + 120); // v LINE 
                        e.Graphics.DrawLine(blackPen, startX + 212, startY + 30, startX + 212, startY + 120); // v LINE 
                        e.Graphics.DrawLine(blackPen, startX + 263, startY + 30, startX + 263, startY + 120); // v LINE 
                        e.Graphics.DrawLine(blackPen, startX, startY + 70, 340, startY + 70);

                        e.Graphics.DrawLine(blackPen, startX, startY + 120, 510, startY + 120);
                        e.Graphics.DrawLine(blackPen, 340, startY, 340, startY + 120);


                        Font HED = new Font("Times New Roman", 9, FontStyle.Bold);
                        e.Graphics.DrawString("GST%", HED, new SolidBrush(Color.Black), startX + 2, startY + 33);
                        e.Graphics.DrawString("Taxable", HED, new SolidBrush(Color.Black), startX + 55, startY + 33);
                        e.Graphics.DrawString("GST", HED, new SolidBrush(Color.Black), startX + 107, startY + 33);
                        e.Graphics.DrawString("SGST", HED, new SolidBrush(Color.Black), startX + 161, startY + 33);
                        e.Graphics.DrawString("CGST", HED, new SolidBrush(Color.Black), startX + 213, startY + 33);
                        e.Graphics.DrawString("IGST", HED, new SolidBrush(Color.Black), startX + 265, startY + 33);
                        e.Graphics.DrawString("12%", HED, new SolidBrush(Color.Black), startX + 2, startY + 75);
                        e.Graphics.DrawString(ttaxblevalue.ToString(), HED, new SolidBrush(Color.Black), startX + 55, startY + 75);
                        e.Graphics.DrawString(ttaxamount.ToString(), HED, new SolidBrush(Color.Black), startX + 107, startY + 75);
                        e.Graphics.DrawString(divtaxamt.ToString(), HED, new SolidBrush(Color.Black), startX + 161, startY + 75);
                        e.Graphics.DrawString(divtaxamt.ToString(), HED, new SolidBrush(Color.Black), startX + 213, startY + 75);
                        e.Graphics.DrawString("0.00", HED, new SolidBrush(Color.Black), startX + 265, startY + 75);


                        e.Graphics.DrawString("Bank Details", FONTGST, new SolidBrush(Color.Black), 345, startY + 5);
                        e.Graphics.DrawString("Canara Bank, Vengara ", HED, new SolidBrush(Color.Black), 345, startY + 30);
                        e.Graphics.DrawString("A/C No:4691201000015 ", HED, new SolidBrush(Color.Black), 345, startY + 50);
                        e.Graphics.DrawString("IFSC Code:CNRB0004691", HED, new SolidBrush(Color.Black), 345, startY + 70);

                        //rightside bottom
                        e.Graphics.DrawLine(blackPen, 690, startY, 690, startY + 160); //center line of amounts
                        e.Graphics.DrawLine(blackPen, 510, startY + 20, startX + 800, startY + 20);//total
                        e.Graphics.DrawLine(blackPen, 510, startY + 40, startX + 800, startY + 40); //disc
                        e.Graphics.DrawLine(blackPen, 510, startY + 60, startX + 800, startY + 60); //sub
                        e.Graphics.DrawLine(blackPen, 510, startY + 80, startX + 800, startY + 80); //gst
                        e.Graphics.DrawLine(blackPen, 510, startY + 100, startX + 800, startY + 100); //
                        e.Graphics.DrawLine(blackPen, 510, startY + 120, startX + 800, startY + 120); //disc
                        e.Graphics.DrawLine(blackPen, 510, startY + 140, startX + 800, startY + 140); //disc
                        e.Graphics.DrawLine(blackPen, 510, startY + 160, startX + 800, startY + 160); //disc


                        //string
                        e.Graphics.DrawString("Total", HED, new SolidBrush(Color.Black), startX + 510, startY + 2);
                        e.Graphics.DrawString("Cash Discount", HED, new SolidBrush(Color.Black), startX + 510, startY + 22);
                        e.Graphics.DrawString("Sub Total", HED, new SolidBrush(Color.Black), startX + 510, startY + 42);
                        e.Graphics.DrawString("GST", HED, new SolidBrush(Color.Black), startX + 510, startY + 62);
                        e.Graphics.DrawString("Discount After Tax", HED, new SolidBrush(Color.Black), startX + 510, startY + 82);
                        e.Graphics.DrawString("Return Amount", HED, new SolidBrush(Color.Black), startX + 510, startY + 102);
                        e.Graphics.DrawString("Grand Total", HED, new SolidBrush(Color.Black), startX + 510, startY + 122);
                        e.Graphics.DrawString("Net Amount", HED, new SolidBrush(Color.Black), startX + 510, startY + 142);

                        //values
                        e.Graphics.DrawString(tgrossrate.ToString(), HED, new SolidBrush(Color.Black), startX + 800, startY + 2, format);
                        e.Graphics.DrawString(tcdis.ToString(), HED, new SolidBrush(Color.Black), startX + 800, startY + 22, format);
                        e.Graphics.DrawString((tgrossrate - tcdis).ToString(), HED, new SolidBrush(Color.Black), startX + 800, startY + 42, format);
                        e.Graphics.DrawString(ttaxamount.ToString(), HED, new SolidBrush(Color.Black), startX + 800, startY + 62, format);
                        e.Graphics.DrawString("Discount After Tax", HED, new SolidBrush(Color.Black), startX + 800, startY + 82, format);
                        e.Graphics.DrawString("Return Amount", HED, new SolidBrush(Color.Black), startX + 800, startY + 102, format);
                        e.Graphics.DrawString("Grand Total", HED, new SolidBrush(Color.Black), startX + 800, startY + 122, format);
                        e.Graphics.DrawString(ttotalvalue.ToString(), HED, new SolidBrush(Color.Black), startX + 800, startY + 142, format);

                        e.Graphics.DrawString("Authorised Signatory ", HED, new SolidBrush(Color.Black), 600, 1010);
                        e.Graphics.DrawString("with Status and Seal ", HED, new SolidBrush(Color.Black), 600, 1020);


                        e.Graphics.DrawString("Total Invoice Amount in Words:", HED, new SolidBrush(Color.Black), startX + 5, startY + 132);

                        //if (CUSTOMER_CODE.Text != "" && CUSTOMER_NAME.Text != "")
                        //{
                        //    e.Graphics.DrawString(":" + accountNumber, Headerfont5, new SolidBrush(Color.Black), 155, 947);
                        //    e.Graphics.DrawString(":" + ifscCode, Headerfont5, new SolidBrush(Color.Black), 155, 962);
                        //}

                        e.Graphics.DrawString("(Common Seal)", HED, new SolidBrush(Color.Black), 415, 1040);


                        try
                        {
                            //int cash = (int)Convert.ToDouble(ttotalvalue);
                            int cash = (int)Convert.ToDouble(totalNet);
                            string cas = totalNet.ToString();
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
                            e.Graphics.DrawString(test3, HED, new SolidBrush(Color.Black), startX + 5, startY + 145);
                        }
                        catch
                        {
                        }
                    }
                    catch
                    {
                    }
                }
                PAGETOTAL = false;
            }
            newoffset = newoffset + 25;
            e.HasMorePages = hasmorepages;

        }
        public void GetDynamicPageSetting()
        {
            try
            {
                DataTable Pagesize = new DataTable();
                Pagesize = ComSet.DYNAMICPAGESIZE();
                if (Pagesize.Rows.Count > 0)
                {


                    pagewidth = Convert.ToInt32(Pagesize.Rows[0]["PAGESIZEWIDTH"]);
                    pageheight = Convert.ToInt32(Pagesize.Rows[0]["PAGESIZEHEIGHT"]);
                    defaultheight = Convert.ToInt32(Pagesize.Rows[0]["DEFAULTHEIGHT"]);
                    fixedheight = Convert.ToBoolean(Pagesize.Rows[0]["FIXEDHEIGHTBIT"]);
                    itemlength = Convert.ToInt32(Pagesize.Rows[0]["ITEMLENGTH"]);
                    PAGETOTAL = Convert.ToBoolean(Pagesize.Rows[0]["PAGETOTAL"]);
                }

            }
            catch
            {
            }
        }

        void printDocumentDynamic_PrintPage(object sender, PrintPageEventArgs e)
        {

            DataTable positions = new DataTable();
            positions = ComSet.DYNAMICPOSITIONS();
            bool PRINTTOTALPAGE = true;


            float xpos;
            int startx = 50;
            int starty = 30;
            int offset = 15;
            int headerstartposition = 50;

            int w = e.MarginBounds.Width / 2;
            int x = e.MarginBounds.Left;
            int y = e.MarginBounds.Top;
            Font Headerfont1 = new Font("Times New Roman", 15, FontStyle.Bold);
            Font arabicfont = new Font("Times New Roman", 8);
            Font Headerfont2 = new Font("Times New Roman", 10, FontStyle.Bold);
            Font InvoiceFont = new Font("Times New Roman", 15, FontStyle.Bold);

            Font printFont = new Font("Times New Roman", 10);
            var tabDataForeColor = Color.Black;
            int height = 100 + y;
            bool hasmorepages = false;

            var txtDataWidth = e.Graphics.MeasureString(CompanyName, Headerfont1).Width;
            xpos = e.MarginBounds.Left + (e.MarginBounds.Width / 2);

            //try
            //{
            //    if (logo != null || logo != "")
            //    {

            //        System.Drawing.Image img = System.Drawing.Image.FromFile(logo);

            //        Point loc = new Point(Convert.ToInt32(xpos), 50);
            //        float width, imgheight;
            //        width = img.Width;
            //        imgheight = img.Height;
            //        if (width > 300)
            //        {
            //            using (Bitmap bitmap = (Bitmap)Image.FromFile(logo))
            //            {
            //                //   using (Bitmap newBitmap = new Bitmap(bitmap))
            //                // {
            //                bitmap.SetResolution(300, 300);

            //                int a = Convert.ToInt32(xpos);
            //                e.Graphics.DrawImage(bitmap, new Rectangle(410 - 150, 50, 300, 70));

            //                //     FileInfo fileInfo = new System.IO.FileInfo(logo);
            //                //  if (fileInfo.IsReadOnly)
            //                //  {
            //                //  fileInfo.IsReadOnly = false;
            //                // }
            //                //       newBitmap.Save(Path.GetTempPath()+"\\ip.jpeg", ImageFormat.Jpeg);



            //                // }
            //            }
            //        }
            //        else
            //        {

            //            e.Graphics.DrawImage(img, new Rectangle(Convert.ToInt32(xpos) + 385 - 150, 315, 500, 70));
            //        }
            //    }

            //}
            //catch (Exception ee)
            //{
            //    //  MessageBox.Show(ee.Message);
            //}




            //try
            //{
            //    string item = Application.StartupPath + "\\Items2.jpg";
            //    System.Drawing.Image imgs = System.Drawing.Image.FromFile(item);

            //    Point loc = new Point(Convert.ToInt32(xpos), 50);
            //    e.Graphics.DrawImage(imgs, new Rectangle(50, 275, 750, 20));
            //}




            //catch (Exception ee)
            //{
            //    //   MessageBox.Show(ee.Message);
            //}





            //  RectangleF rect = new RectangleF(10.0F, 10.0F, 200.0F, 30.0F);
            StringFormat format = new StringFormat(StringFormatFlags.DirectionRightToLeft);

            using (var sf = new StringFormat())
            {
                height += 15;
                sf.LineAlignment = StringAlignment.Center;
                sf.Alignment = StringAlignment.Center;


                xpos = e.MarginBounds.Left + (e.MarginBounds.Width / 2);

                // e.Graphics.DrawString(CompanyName, Headerfont1, new SolidBrush(tabDataForeColor), xpos, starty, sf);
                //   e.Graphics.DrawString(Addres1+", "+Addres2, Headerfont2, new SolidBrush(tabDataForeColor), xpos, starty + offset, sf);
                //  offset = offset + 20;
                //  e.Graphics.DrawString(Phone, Headerfont2, new SolidBrush(tabDataForeColor), xpos, starty + offset, sf);
                //   offset = offset + 20;
                //  e.Graphics.DrawString("Credit Note: " + DOC_NO.Text, Headerfont2, new SolidBrush(tabDataForeColor), xpos, starty + offset, sf);
                //   offset = offset + 20;


                // StringFormat arb = new StringFormat(StringFormatFlags.DirectionRightToLeft);
                // arb=(StringFormatFlags.DirectionRightToLeft);
                //e.Graphics.DrawString("", Headerfont1, new SolidBrush(tabDataForeColor), headerstartposition, starty);
                //e.Graphics.DrawString("", Headerfont1, new SolidBrush(tabDataForeColor), 760, starty, format);
                offset = offset + 9;
                //e.Graphics.DrawString("", Headerfont2, new SolidBrush(tabDataForeColor), headerstartposition, starty + offset);
                //e.Graphics.DrawString("", Headerfont2, new SolidBrush(tabDataForeColor), 760, starty + offset, format);
                offset = offset + 15;

                //e.Graphics.DrawString("", Headerfont2, new SolidBrush(tabDataForeColor), headerstartposition, starty + offset);
                //e.Graphics.DrawString("", Headerfont2, new SolidBrush(tabDataForeColor), 760, starty + offset, format);
                offset = offset + 15;

                //e.Graphics.DrawString("", Headerfont2, new SolidBrush(tabDataForeColor), headerstartposition, starty + offset);
                //e.Graphics.DrawString("", Headerfont2, new SolidBrush(tabDataForeColor), 760, starty + offset, format);
                offset = offset + 15;

                //e.Graphics.DrawString("", Headerfont2, new SolidBrush(tabDataForeColor), headerstartposition, starty + offset);
                //e.Graphics.DrawString("", Headerfont2, new SolidBrush(tabDataForeColor), 760, starty + offset, format);
                offset = offset + 15;

                //e.Graphics.DrawString("", Headerfont2, new SolidBrush(tabDataForeColor), headerstartposition, starty + offset);
                //e.Graphics.DrawString("", Headerfont2, new SolidBrush(tabDataForeColor), 760, starty + offset, format);
                offset = offset + 25;

                //e.Graphics.DrawString(Website, Headerfont2, new SolidBrush(tabDataForeColor), headerstartposition, starty + offset);
                //offset = offset + 10;


                e.Graphics.DrawString("" + VOUCHNUM.Text, Headerfont2, new SolidBrush(tabDataForeColor), Convert.ToInt32(positions.Rows[8]["XAXIS"]), Convert.ToInt32(positions.Rows[8]["YAXIS"]));
                //e.Graphics.DrawString(" ", Headerfont2, new SolidBrush(tabDataForeColor), 760, starty + offset, format);
                //e.Graphics.DrawString("", Headerfont2, new SolidBrush(tabDataForeColor), 660, starty + offset);
                offset = offset + 16;

                e.Graphics.DrawString("" + DOC_DATE_GRE.Value.ToShortDateString(), Headerfont2, new SolidBrush(tabDataForeColor), Convert.ToInt32(positions.Rows[1]["XAXIS"]), Convert.ToInt32(positions.Rows[1]["YAXIS"]));
                //e.Graphics.DrawString("", Headerfont2, new SolidBrush(tabDataForeColor), 760, starty + offset, format);
                //e.Graphics.DrawString("", Headerfont2, new SolidBrush(tabDataForeColor), 660, starty + offset);
                offset = offset + 16;


                //e.Graphics.DrawString("", InvoiceFont, new SolidBrush(tabDataForeColor), xpos + 400, starty + offset + 70, sf);
                //offset = offset + 16;
                //Pen blackPen = new Pen(Color.Black, 1);
                //Point point1 = new Point(0, 200);
                //Point point2 = new Point(900, 200);
                //e.Graphics.DrawLine(blackPen, point1, point2);








                //e.Graphics.DrawString("", Headerfont2, new SolidBrush(tabDataForeColor), startx, starty + offset - 15);
                e.Graphics.DrawString("             " + CUSTOMER_NAME.Text, Headerfont2, new SolidBrush(tabDataForeColor), Convert.ToInt32(positions.Rows[0]["XAXIS"]), Convert.ToInt32(positions.Rows[0]["YAXIS"]));
                Font itemhead = new Font("Times New Roman", 10);
                offset = offset + 2;




                //Point point3 = new Point(45, 300);
                //Point point4 = new Point(760, 300);
                //e.Graphics.DrawLine(blackPen, point3, point4);



                //e.Graphics.DrawLine(blackPen, 45, 300, 45, 1000);
                ////e.Graphics.DrawLine(blackPen, 355, 219, 355, 900);
                //e.Graphics.DrawLine(blackPen, 450, 300, 450, 1000);
                //e.Graphics.DrawLine(blackPen, 540, 300, 540, 1000);
                //e.Graphics.DrawLine(blackPen, 650, 300, 650, 1000);
                //e.Graphics.DrawLine(blackPen, 760, 300, 760, 1000);

                //e.Graphics.DrawLine(blackPen, 45, 1000, 760, 1000);









                //  int number = Convert.ToInt32(txtcashrcvd.Text);
                // string headtext = "Item".PadRight(115) + "Qty".PadRight(20) + "Unit Price".PadRight(30) + "Total";

                //   string k = "..";

                //string headtext2 = "العنصر".PadRight(115) + "كمية".PadRight(number) + "Unit price سعر الوحدة".PadRight(30) + "الإجمالي الكلي";




                // string headtext2 = "M"+"العنصر k" + "كمية".PadRight(20) + "سعر الوحدة".PadRight(30) + "الإجمالي الكلي";

                //   string headtext2 = "الإجماليالكلي".PadRight(30) + "سعرالوحدة".PadRight(20) + "كمية".PadRight(115) + "العنصر";


                //   e.Graphics.DrawString(headtext, Headerfont2, new SolidBrush(Color.Black), startx, starty + offset + 70);
                //  e.Graphics.DrawString("الإجمالي الكلي".PadRight(30) + "سعر الوحدة".PadRight(20) + "كمية".PadRight(115) + "العنصر", Headerfont2, new SolidBrush(Color.Black), 230, starty + offset + 30, format);

                //    e.Graphics.DrawString(headtext2, Headerfont2, new SolidBrush(Color.Black), startx, starty + offset + 30);














                offset = offset + 120;
                Font font = new Font("Times New Roman", 10, FontStyle.Bold);
                float fontheight = font.GetHeight();
                try
                {

                    int i = 1;
                    int nooflines = 0;
                    int INCRIMENTHEIGHT = Convert.ToInt32(positions.Rows[2]["YAXIS"]);
                    foreach (DataGridViewRow row in dgItems.Rows)
                    {
                        PRINTTOTALPAGE = false;
                        if (i > printeditems)
                        {
                            if (nooflines < 23)
                            {

                                string ItemCode = row.Cells["cCode"].Value.ToString();
                                string Unit = row.Cells["cUnit"].Value.ToString();
                                int ORGLENGTH = row.Cells["cName"].Value.ToString().Length;
                                string name = row.Cells["cName"].Value.ToString().Length <= itemlength ? row.Cells["cName"].Value.ToString() : row.Cells["cName"].Value.ToString().Substring(0, itemlength);
                                string name2 = "";
                                int BALANCELENGH = ORGLENGTH - itemlength;

                                string arbname = row.Cells["cDescArb"].Value.ToString();
                                string qty = row.Cells["cQty"].Value.ToString();
                                string rate = row.Cells["cPrice"].Value.ToString();
                                string price = row.Cells["cTotal"].Value.ToString();
                                string productline = name + qty + rate + price;

                                e.Graphics.DrawString(i.ToString(), font, new SolidBrush(Color.Black), Convert.ToInt32(positions.Rows[13]["XAXIS"]), INCRIMENTHEIGHT);
                                e.Graphics.DrawString(ItemCode, font, new SolidBrush(Color.Black), Convert.ToInt32(positions.Rows[2]["XAXIS"]), INCRIMENTHEIGHT);
                                e.Graphics.DrawString(name, font, new SolidBrush(Color.Black), Convert.ToInt32(positions.Rows[3]["XAXIS"]), INCRIMENTHEIGHT);
                                //   e.Graphics.DrawString(arbname, font, new SolidBrush(Color.Black), startx+380, starty + offset,format);

                                e.Graphics.DrawString(qty, font, new SolidBrush(Color.Black), Convert.ToInt32(positions.Rows[4]["XAXIS"]), INCRIMENTHEIGHT);
                                e.Graphics.DrawString(Unit, font, new SolidBrush(Color.Black), Convert.ToInt32(positions.Rows[12]["XAXIS"]), INCRIMENTHEIGHT);
                                e.Graphics.DrawString(rate, font, new SolidBrush(Color.Black), Convert.ToInt32(positions.Rows[5]["XAXIS"]), INCRIMENTHEIGHT);
                                e.Graphics.DrawString(price, font, new SolidBrush(Color.Black), Convert.ToInt32(positions.Rows[7]["XAXIS"]), INCRIMENTHEIGHT);
                                nooflines++;
                                int printpoint = itemlength;
                                while (BALANCELENGH > 1)
                                {
                                    INCRIMENTHEIGHT = INCRIMENTHEIGHT + (int)fontheight + 9;

                                    name2 = BALANCELENGH <= itemlength ? row.Cells["cName"].Value.ToString().Substring(printpoint, BALANCELENGH) : row.Cells["cName"].Value.ToString().Substring(printpoint, itemlength);
                                    e.Graphics.DrawString(name2, font, new SolidBrush(Color.Black), Convert.ToInt32(positions.Rows[3]["XAXIS"]), INCRIMENTHEIGHT);
                                    BALANCELENGH = BALANCELENGH - itemlength;
                                    printpoint = printpoint + itemlength;
                                    nooflines++;
                                }
                                INCRIMENTHEIGHT = INCRIMENTHEIGHT + (int)fontheight + 9;
                                i++;

                                //if (hasArabic)
                                //{
                                //    //string Astr = "سُوْرَةُ الْفَاتِحَة";
                                //    //var num = "-1";
                                //    //var LRM = ((char)0x200E).ToString();
                                //    //var result = Astr + LRM + num;
                                //    //var correctedValue = Regex.Replace(arbname,"(?<=[0-9])(?=[A-Za-z])|(?<=[A-Za-z])(?=[0-9])",LRM);
                                //    PictureBox pkgb = new PictureBox();
                                //    pkgb.Image = ConverttoImage(arbname, "Times New Roman", 10);


                                //    try
                                //    {

                                //        System.Drawing.Image imgs = pkgb.Image;

                                //        Point loc = new Point(Convert.ToInt32(xpos), 50);
                                //        e.Graphics.DrawImage(imgs, new Rectangle(startx, starty + offset, imgs.Width, 15));
                                //    }
                                //    catch
                                //    {
                                //    }

                                //using (StringFormat formats = new StringFormat(StringFormatFlags.DirectionRightToLeft))
                                //{
                                //    e.Graphics.DrawString(TX.Text, font, new SolidBrush(Color.Black), startx + 400, starty + offset,formats);
                                //}
                                //e.Graphics.DrawString(arbname, font, new SolidBrush(Color.Black), startx, starty + offset);
                                //    offset = offset + (int)fontheight + 10;
                                //}
                                //else
                                //{
                                //    offset = offset + (int)fontheight + 12;
                                //}
                            }
                            else
                            {
                                printeditems = i - 1;
                                //  e.HasMorePages = true;
                                hasmorepages = true;
                                PRINTTOTALPAGE = true;
                            }
                        }
                        else
                        {
                            i++;
                        }
                    }
                }
                catch (Exception EE)
                {
                    MessageBox.Show(EE.Message);
                }
            }

            float newoffset = 895;

            //    e.Graphics.DrawString(NOTES.Text, Headerfont2, new SolidBrush(Color.Black), startx, starty + newoffset + 3);

            //e.Graphics.DrawString("Gross Total المجموع الكلي", Headerfont2, new SolidBrush(Color.Black), startx + 470, starty + newoffset + 3);
            if (PRINTTOTALPAGE)
            {
                if (PAGETOTAL)
                {
                    try
                    {
                        string test = Spell.SpellAmount.InWrods(Convert.ToDecimal(NET_AMOUNT.Text));

                        int index = test.IndexOf("Taka");
                        int l = test.Length;
                        test = test.Substring(index + 4);


                        e.Graphics.DrawString(test, Headerfont2, new SolidBrush(Color.Black), Convert.ToInt32(positions.Rows[14]["XAXIS"]), Convert.ToInt32(positions.Rows[14]["YAXIS"]));
                    }
                    catch
                    {
                    }

                }


            }
            else
            {
                try
                {
                    string test = Spell.SpellAmount.InWrods(Convert.ToDecimal(NET_AMOUNT.Text));

                    int index = test.IndexOf("Taka");
                    int l = test.Length;
                    test = test.Substring(index + 4);

                    e.Graphics.DrawString(test, Headerfont2, new SolidBrush(Color.Black), Convert.ToInt32(positions.Rows[14]["XAXIS"]), Convert.ToInt32(positions.Rows[14]["YAXIS"]));
                }
                catch
                {
                }
            }
            //try
            //{
            //    string test = Spell.SpellAmount.InWrods(Convert.ToDecimal(NET_AMOUNT.Text));

            //    int index = test.IndexOf("Taka");
            //    int l = test.Length;
            //    test = test.Substring(index + 4);

            //    e.Graphics.DrawString(test, Headerfont2, new SolidBrush(Color.Black), startx, starty + newoffset + 3);
            //}
            //catch
            //{
            //}


            //newoffset = newoffset + 20;
            //e.Graphics.DrawString("Tax Amt", Headerfont2, new SolidBrush(Color.Black), startx + 490, starty + newoffset + 3);
            //e.Graphics.DrawString(TAX_TOTAL.Text, Headerfont2, new SolidBrush(Color.Black), startx + 610, starty + newoffset + 3);

            newoffset = newoffset + 20;
            //e.Graphics.DrawString("Discount تخفيض ", Headerfont2, new SolidBrush(Color.Black), startx + 470, starty + newoffset + 3);
            if (PRINTTOTALPAGE)
            {
                if (PAGETOTAL)
                {
                    e.Graphics.DrawString(TOTAL_AMOUNT.Text, Headerfont2, new SolidBrush(Color.Black), Convert.ToInt32(positions.Rows[9]["XAXIS"]), Convert.ToInt32(positions.Rows[9]["YAXIS"]));
                    e.Graphics.DrawString(DISCOUNT.Text, Headerfont2, new SolidBrush(Color.Black), Convert.ToInt32(positions.Rows[10]["XAXIS"]), Convert.ToInt32(positions.Rows[10]["YAXIS"]));
                }
            }
            else
            {
                e.Graphics.DrawString(TOTAL_AMOUNT.Text, Headerfont2, new SolidBrush(Color.Black), Convert.ToInt32(positions.Rows[9]["XAXIS"]), Convert.ToInt32(positions.Rows[9]["YAXIS"]));
                e.Graphics.DrawString(DISCOUNT.Text, Headerfont2, new SolidBrush(Color.Black), Convert.ToInt32(positions.Rows[10]["XAXIS"]), Convert.ToInt32(positions.Rows[10]["YAXIS"]));
            }

            newoffset = newoffset + 20;

            //e.Graphics.DrawString("Tax Amount", Headerfont2, new SolidBrush(Color.Black), startx + 490, starty + newoffset + 3);
            //e.Graphics.DrawString(TAX_TOTAL.Text, Headerfont2, new SolidBrush(Color.Black), startx + 610, starty + newoffset + 3);
            ////e.Graphics.DrawString("---------------------------------------", Headerfont2, new SolidBrush(Color.Black), startx + 490, starty + newoffset + 9);
            //newoffset = newoffset + 25;

            //e.Graphics.DrawString("Signature توقيع", Headerfont2, new SolidBrush(Color.Black), startx, starty + newoffset + 3);


            offset = offset + 25;
            //e.Graphics.DrawString("Net Total المجموع الصافي", Headerfont2, new SolidBrush(Color.Black), 740, starty + newoffset + 3);
            //offset = offset + 25;
            //e.Graphics.DrawString("المجموع الصافي" + ConvertToEasternArabicNumerals(NET_AMOUNT.Text), Headerfont2, new SolidBrush(Color.Black), startx + 470, starty + newoffset + 3);
            if (PRINTTOTALPAGE)
            {
                if (PAGETOTAL)
                {

                    e.Graphics.DrawString(Spell.SpellAmount.comma(Convert.ToDecimal(NET_AMOUNT.Text)), Headerfont2, new SolidBrush(Color.Black), Convert.ToInt32(positions.Rows[11]["XAXIS"]), Convert.ToInt32(positions.Rows[11]["YAXIS"]));
                }
            }
            else
            {
                e.Graphics.DrawString(Spell.SpellAmount.comma(Convert.ToDecimal(NET_AMOUNT.Text)), Headerfont2, new SolidBrush(Color.Black), Convert.ToInt32(positions.Rows[11]["XAXIS"]), Convert.ToInt32(positions.Rows[11]["YAXIS"]));
            }


            newoffset = newoffset + 20;






            e.HasMorePages = hasmorepages;

        }
        
        DataTable datapositions = new DataTable();
        public void Getposstions()
        {
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * INV_PRINTPOSTION";
            adapter.SelectCommand = cmd;
            adapter.Fill(datapositions);
        }

        //grand tools printing invoice
        private void printDocumentMediumSize_PrintPageabic(object sender, PrintPageEventArgs e)
        {
            float xpos;
            int startx = 50;
            int starty = 30;
            int offset = 15;
            int headerstartposition = 50;

            int w = e.MarginBounds.Width / 2;
            int x = e.MarginBounds.Left;
            int y = e.MarginBounds.Top;
            Font Headerfont1 = new Font("Times New Roman", 15, FontStyle.Bold);
            Font arabicfont = new Font("Times New Roman", 8);
            Font Headerfont2 = new Font("Times New Roman", 10, FontStyle.Bold);
            Font InvoiceFont = new Font("Times New Roman", 15, FontStyle.Bold);

            Font printFont = new Font("Times New Roman", 10);
            var tabDataForeColor = Color.Black;
            int height = 100 + y;


            var txtDataWidth = e.Graphics.MeasureString(CompanyName, Headerfont1).Width;
            xpos = e.MarginBounds.Left + (e.MarginBounds.Width / 2);

            //try
            //{
            //    if (logo != null || logo != "")
            //    {

            //        System.Drawing.Image img = System.Drawing.Image.FromFile(logo);

            //        Point loc = new Point(Convert.ToInt32(xpos), 50);
            //        float width, imgheight;
            //        width = img.Width;
            //        imgheight = img.Height;
            //        if (width > 300)
            //        {
            //            using (Bitmap bitmap = (Bitmap)Image.FromFile(logo))
            //            {
            //                //   using (Bitmap newBitmap = new Bitmap(bitmap))
            //                // {
            //                bitmap.SetResolution(300, 300);

            //                int a = Convert.ToInt32(xpos);
            //                e.Graphics.DrawImage(bitmap, new Rectangle(410 - 150, 50, 300, 70));

            //                //     FileInfo fileInfo = new System.IO.FileInfo(logo);
            //                //  if (fileInfo.IsReadOnly)
            //                //  {
            //                //  fileInfo.IsReadOnly = false;
            //                // }
            //                //       newBitmap.Save(Path.GetTempPath()+"\\ip.jpeg", ImageFormat.Jpeg);



            //                // }
            //            }
            //        }
            //        else
            //        {

            //            e.Graphics.DrawImage(img, new Rectangle(Convert.ToInt32(xpos) + 385 - 150, 315, 500, 70));
            //        }
            //    }

            //}
            //catch (Exception ee)
            //{
            //    //  MessageBox.Show(ee.Message);
            //}




            //try
            //{
            //    string item = Application.StartupPath + "\\Items2.jpg";
            //    System.Drawing.Image imgs = System.Drawing.Image.FromFile(item);

            //    Point loc = new Point(Convert.ToInt32(xpos), 50);
            //    e.Graphics.DrawImage(imgs, new Rectangle(50, 275, 750, 20));
            //}




            //catch (Exception ee)
            //{
            //    //   MessageBox.Show(ee.Message);
            //}





            //  RectangleF rect = new RectangleF(10.0F, 10.0F, 200.0F, 30.0F);
            StringFormat format = new StringFormat(StringFormatFlags.DirectionRightToLeft);

            using (var sf = new StringFormat())
            {
                height += 15;
                sf.LineAlignment = StringAlignment.Center;
                sf.Alignment = StringAlignment.Center;


                xpos = e.MarginBounds.Left + (e.MarginBounds.Width / 2);

                // e.Graphics.DrawString(CompanyName, Headerfont1, new SolidBrush(tabDataForeColor), xpos, starty, sf);
                //   e.Graphics.DrawString(Addres1+", "+Addres2, Headerfont2, new SolidBrush(tabDataForeColor), xpos, starty + offset, sf);
                //  offset = offset + 20;
                //  e.Graphics.DrawString(Phone, Headerfont2, new SolidBrush(tabDataForeColor), xpos, starty + offset, sf);
                //   offset = offset + 20;
                //  e.Graphics.DrawString("Credit Note: " + DOC_NO.Text, Headerfont2, new SolidBrush(tabDataForeColor), xpos, starty + offset, sf);
                //   offset = offset + 20;


                // StringFormat arb = new StringFormat(StringFormatFlags.DirectionRightToLeft);
                // arb=(StringFormatFlags.DirectionRightToLeft);
                //e.Graphics.DrawString("", Headerfont1, new SolidBrush(tabDataForeColor), headerstartposition, starty);
                //e.Graphics.DrawString("", Headerfont1, new SolidBrush(tabDataForeColor), 760, starty, format);
                offset = offset + 9;
                //e.Graphics.DrawString("", Headerfont2, new SolidBrush(tabDataForeColor), headerstartposition, starty + offset);
                //e.Graphics.DrawString("", Headerfont2, new SolidBrush(tabDataForeColor), 760, starty + offset, format);
                offset = offset + 15;

                //e.Graphics.DrawString("", Headerfont2, new SolidBrush(tabDataForeColor), headerstartposition, starty + offset);
                //e.Graphics.DrawString("", Headerfont2, new SolidBrush(tabDataForeColor), 760, starty + offset, format);
                offset = offset + 15;

                //e.Graphics.DrawString("", Headerfont2, new SolidBrush(tabDataForeColor), headerstartposition, starty + offset);
                //e.Graphics.DrawString("", Headerfont2, new SolidBrush(tabDataForeColor), 760, starty + offset, format);
                offset = offset + 15;

                //e.Graphics.DrawString("", Headerfont2, new SolidBrush(tabDataForeColor), headerstartposition, starty + offset);
                //e.Graphics.DrawString("", Headerfont2, new SolidBrush(tabDataForeColor), 760, starty + offset, format);
                offset = offset + 15;

                //e.Graphics.DrawString("", Headerfont2, new SolidBrush(tabDataForeColor), headerstartposition, starty + offset);
                //e.Graphics.DrawString("", Headerfont2, new SolidBrush(tabDataForeColor), 760, starty + offset, format);
                offset = offset + 25;

                //e.Graphics.DrawString(Website, Headerfont2, new SolidBrush(tabDataForeColor), headerstartposition, starty + offset);
                //offset = offset + 10;


                e.Graphics.DrawString("        " + Billno, Headerfont2, new SolidBrush(tabDataForeColor), headerstartposition, starty + offset);
                //e.Graphics.DrawString(" ", Headerfont2, new SolidBrush(tabDataForeColor), 760, starty + offset, format);
                //e.Graphics.DrawString("", Headerfont2, new SolidBrush(tabDataForeColor), 660, starty + offset);
                offset = offset + 16;

                e.Graphics.DrawString("" + DOC_DATE_GRE.Value.ToShortDateString(), Headerfont2, new SolidBrush(tabDataForeColor), 680, starty + offset);
                //e.Graphics.DrawString("", Headerfont2, new SolidBrush(tabDataForeColor), 760, starty + offset, format);
                //e.Graphics.DrawString("", Headerfont2, new SolidBrush(tabDataForeColor), 660, starty + offset);
                offset = offset + 16;


                //e.Graphics.DrawString("", InvoiceFont, new SolidBrush(tabDataForeColor), xpos + 400, starty + offset + 70, sf);
                offset = offset + 16;
                Pen blackPen = new Pen(Color.Black, 1);
                Point point1 = new Point(0, 200);
                Point point2 = new Point(900, 200);
                e.Graphics.DrawLine(blackPen, point1, point2);








                //e.Graphics.DrawString("", Headerfont2, new SolidBrush(tabDataForeColor), startx, starty + offset - 15);
                e.Graphics.DrawString("             " + CUSTOMER_NAME.Text, Headerfont2, new SolidBrush(tabDataForeColor), startx + 80, starty + offset - 15);
                Font itemhead = new Font("Times New Roman", 10);
                offset = offset + 2;




                Point point3 = new Point(45, 300);
                Point point4 = new Point(760, 300);
                e.Graphics.DrawLine(blackPen, point3, point4);



                e.Graphics.DrawLine(blackPen, 45, 300, 45, 1000);
                //e.Graphics.DrawLine(blackPen, 355, 219, 355, 900);
                e.Graphics.DrawLine(blackPen, 450, 300, 450, 1000);
                e.Graphics.DrawLine(blackPen, 540, 300, 540, 1000);
                e.Graphics.DrawLine(blackPen, 650, 300, 650, 1000);
                e.Graphics.DrawLine(blackPen, 760, 300, 760, 1000);

                e.Graphics.DrawLine(blackPen, 45, 1000, 760, 1000);









                //  int number = Convert.ToInt32(txtcashrcvd.Text);
                string headtext = "Item".PadRight(115) + "Qty".PadRight(20) + "Unit Price".PadRight(30) + "Total";

                //   string k = "..";

                //string headtext2 = "العنصر".PadRight(115) + "كمية".PadRight(number) + "Unit price سعر الوحدة".PadRight(30) + "الإجمالي الكلي";




                // string headtext2 = "M"+"العنصر k" + "كمية".PadRight(20) + "سعر الوحدة".PadRight(30) + "الإجمالي الكلي";

                //   string headtext2 = "الإجماليالكلي".PadRight(30) + "سعرالوحدة".PadRight(20) + "كمية".PadRight(115) + "العنصر";


                //   e.Graphics.DrawString(headtext, Headerfont2, new SolidBrush(Color.Black), startx, starty + offset + 70);
                //  e.Graphics.DrawString("الإجمالي الكلي".PadRight(30) + "سعر الوحدة".PadRight(20) + "كمية".PadRight(115) + "العنصر", Headerfont2, new SolidBrush(Color.Black), 230, starty + offset + 30, format);

                //    e.Graphics.DrawString(headtext2, Headerfont2, new SolidBrush(Color.Black), startx, starty + offset + 30);














                offset = offset + 120;
                Font font = new Font("Times New Roman", 10, FontStyle.Bold);
                float fontheight = font.GetHeight();
                try
                {
                    int i = 1;
                    foreach (DataGridViewRow row in dgItems.Rows)
                    {
                        string ItemCode = row.Cells["cCode"].Value.ToString();
                        string Unit = row.Cells["cUnit"].Value.ToString();
                        string name = row.Cells["cName"].Value.ToString().Length <= 40 ? row.Cells["cName"].Value.ToString() : row.Cells["cName"].Value.ToString().Substring(0, 40);

                        string arbname = row.Cells["cDescArb"].Value.ToString();
                        string qty = row.Cells["cQty"].Value.ToString();
                        string rate = row.Cells["cPrice"].Value.ToString();
                        string price = row.Cells["cTotal"].Value.ToString();
                        string productline = name + qty + rate + price;

                        e.Graphics.DrawString(i.ToString(), font, new SolidBrush(Color.Black), startx, starty + offset);
                        e.Graphics.DrawString(ItemCode, font, new SolidBrush(Color.Black), startx + 30, starty + offset);
                        e.Graphics.DrawString(name, font, new SolidBrush(Color.Black), startx + 150, starty + offset);
                        //   e.Graphics.DrawString(arbname, font, new SolidBrush(Color.Black), startx+380, starty + offset,format);

                        e.Graphics.DrawString(qty, font, new SolidBrush(Color.Black), 540, starty + offset);
                        e.Graphics.DrawString(Unit, font, new SolidBrush(Color.Black), startx + 540, starty + offset);
                        e.Graphics.DrawString(rate, font, new SolidBrush(Color.Black), startx + 600, starty + offset);
                        e.Graphics.DrawString(price, font, new SolidBrush(Color.Black), 740, starty + offset);
                        offset = offset + (int)fontheight + 9;

                        i++;
                        //if (hasArabic)
                        //{
                        //    //string Astr = "سُوْرَةُ الْفَاتِحَة";
                        //    //var num = "-1";
                        //    //var LRM = ((char)0x200E).ToString();
                        //    //var result = Astr + LRM + num;
                        //    //var correctedValue = Regex.Replace(arbname,"(?<=[0-9])(?=[A-Za-z])|(?<=[A-Za-z])(?=[0-9])",LRM);
                        //    PictureBox pkgb = new PictureBox();
                        //    pkgb.Image = ConverttoImage(arbname, "Times New Roman", 10);


                        //    try
                        //    {

                        //        System.Drawing.Image imgs = pkgb.Image;

                        //        Point loc = new Point(Convert.ToInt32(xpos), 50);
                        //        e.Graphics.DrawImage(imgs, new Rectangle(startx, starty + offset, imgs.Width, 15));
                        //    }
                        //    catch
                        //    {
                        //    }

                        //using (StringFormat formats = new StringFormat(StringFormatFlags.DirectionRightToLeft))
                        //{
                        //    e.Graphics.DrawString(TX.Text, font, new SolidBrush(Color.Black), startx + 400, starty + offset,formats);
                        //}
                        //e.Graphics.DrawString(arbname, font, new SolidBrush(Color.Black), startx, starty + offset);
                        //    offset = offset + (int)fontheight + 10;
                        //}
                        //else
                        //{
                        //    offset = offset + (int)fontheight + 12;
                        //}
                    }
                }
                catch
                {

                }
            }

            float newoffset = 895;

            e.Graphics.DrawString(NOTES.Text, Headerfont2, new SolidBrush(Color.Black), startx, starty + newoffset + 3);

            //e.Graphics.DrawString("Gross Total المجموع الكلي", Headerfont2, new SolidBrush(Color.Black), startx + 470, starty + newoffset + 3);
            e.Graphics.DrawString(Spell.SpellAmount.comma(Convert.ToDecimal(TOTAL_AMOUNT.Text)), Headerfont2, new SolidBrush(Color.Black), 740, starty + newoffset + 3);
            //try
            //{
            //    string test = Spell.SpellAmount.InWrods(Convert.ToDecimal(NET_AMOUNT.Text));

            //    int index = test.IndexOf("Taka");
            //    int l = test.Length;
            //    test = test.Substring(index + 4);

            //    e.Graphics.DrawString(test, Headerfont2, new SolidBrush(Color.Black), startx, starty + newoffset + 3);
            //}
            //catch
            //{
            //}


            //newoffset = newoffset + 20;
            //e.Graphics.DrawString("Tax Amt", Headerfont2, new SolidBrush(Color.Black), startx + 490, starty + newoffset + 3);
            //e.Graphics.DrawString(TAX_TOTAL.Text, Headerfont2, new SolidBrush(Color.Black), startx + 610, starty + newoffset + 3);

            newoffset = newoffset + 20;
            //e.Graphics.DrawString("Discount تخفيض ", Headerfont2, new SolidBrush(Color.Black), startx + 470, starty + newoffset + 3);
            e.Graphics.DrawString(DISCOUNT.Text, Headerfont2, new SolidBrush(Color.Black), 740, starty + newoffset + 3);

            newoffset = newoffset + 20;

            //e.Graphics.DrawString("Tax Amount", Headerfont2, new SolidBrush(Color.Black), startx + 490, starty + newoffset + 3);
            //e.Graphics.DrawString(TAX_TOTAL.Text, Headerfont2, new SolidBrush(Color.Black), startx + 610, starty + newoffset + 3);
            ////e.Graphics.DrawString("---------------------------------------", Headerfont2, new SolidBrush(Color.Black), startx + 490, starty + newoffset + 9);
            //newoffset = newoffset + 25;

            //e.Graphics.DrawString("Signature توقيع", Headerfont2, new SolidBrush(Color.Black), startx, starty + newoffset + 3);


            offset = offset + 25;
            //e.Graphics.DrawString("Net Total المجموع الصافي", Headerfont2, new SolidBrush(Color.Black), 740, starty + newoffset + 3);
            //offset = offset + 25;
            //e.Graphics.DrawString("المجموع الصافي" + ConvertToEasternArabicNumerals(NET_AMOUNT.Text), Headerfont2, new SolidBrush(Color.Black), startx + 470, starty + newoffset + 3);


            e.Graphics.DrawString(Spell.SpellAmount.comma(Convert.ToDecimal(NET_AMOUNT.Text)), Headerfont2, new SolidBrush(Color.Black), 740, starty + newoffset + 3);

            newoffset = newoffset + 20;






            e.HasMorePages = false;
        }
        
        void printDocumentMediumSize_PrintPage(object sender, PrintPageEventArgs e)
        {

            float xpos;
            int startx = 10;
            int starty = 20;
            int offset = 15;


            int w = e.MarginBounds.Width / 2;
            int x = e.MarginBounds.Left;
            int y = e.MarginBounds.Top;
            Font printFont = new Font("Courier New", 11);
            var tabDataForeColor = Color.Black;
            int height = 100 + y;


            var txtDataWidth = e.Graphics.MeasureString(CompanyName, printFont).Width;


            using (var sf = new StringFormat())
            {
                height += 15;
                sf.LineAlignment = StringAlignment.Center;
                sf.Alignment = StringAlignment.Center;

                xpos = e.MarginBounds.Left + (e.MarginBounds.Width / 2);
                Billno = VOUCHNUM.Text;
                e.Graphics.DrawString(CompanyName, printFont, new SolidBrush(tabDataForeColor), xpos, starty, sf);
                e.Graphics.DrawString(Address1, printFont, new SolidBrush(tabDataForeColor), xpos, starty + offset + 3, sf);
                offset = offset + 24;
                e.Graphics.DrawString(Address2, printFont, new SolidBrush(tabDataForeColor), xpos, starty + offset + 3, sf);
                offset = offset + 24;
                e.Graphics.DrawString("Ph: " + Phone, printFont, new SolidBrush(tabDataForeColor), xpos, starty + offset + 3, sf);
                offset = offset + 24;
                e.Graphics.DrawString("Receipt: " + Billno, printFont, new SolidBrush(tabDataForeColor), startx, starty + offset - 3);
                offset = offset + 24;

                //e.Graphics.DrawString("---------------------------------------------------------------------------", printFont, new SolidBrush(Color.Black), xpos, starty + offset, sf);
                //e.Graphics.DrawString("---------------------------------------------------------------------------", printFont, new SolidBrush(Color.Black), xpos, starty + offset + 3, sf);
                //offset = offset + 14;

                e.Graphics.DrawString(Convert.ToDateTime(DOC_DATE_GRE.Text).ToString("dd/MMM/yyyy") + " " + DateTime.Now.ToShortTimeString(), printFont, new SolidBrush(tabDataForeColor), xpos, starty + offset, sf);
                offset = offset + 19;
                //e.Graphics.DrawString("Tin No:" + TineNo, printFont, new SolidBrush(tabDataForeColor), startx, starty + offset);
                //offset = offset + 12;
                e.Graphics.DrawString("Customer:" + CUSTOMER_NAME.Text, printFont, new SolidBrush(tabDataForeColor), startx, starty + offset - 2);
                Font itemhead = new Font("Courier New", 10);
                offset = offset + 15;
                e.Graphics.DrawString("----------------------------------------------------------", printFont, new SolidBrush(Color.Black), startx, starty + offset);
                offset = offset + 14;

                string headtext = "Item".PadRight(29) + "Price".PadRight(11) + "Qty".PadRight(8) + "Value";
                e.Graphics.DrawString(headtext, itemhead, new SolidBrush(Color.Black), startx, starty + offset);
                e.Graphics.DrawString("----------------------------------------------------------", printFont, new SolidBrush(Color.Black), startx, starty + offset + 13);
                offset = offset + 26;
                Font font = new Font("Courier New", 10);
                float fontheight = font.GetHeight();
                try
                {
                    foreach (DataGridViewRow row in dgItems.Rows)
                    {
                        string name = row.Cells[1].Value.ToString().Length <= 27 ? row.Cells[1].Value.ToString().PadRight(29) : row.Cells[1].Value.ToString().Substring(0, 27).PadRight(29);
                        //string name = row.Cells[1].Value.ToString().PadRight(20);
                        //string tax = row.Cells[7].Value.ToString().PadRight(5);
                        string qty = row.Cells[5].Value.ToString().PadRight(6);
                        string rate = row.Cells[6].Value.ToString().PadRight(12);
                        string price = row.Cells[11].Value.ToString();
                        string productline = name + rate + qty + price;
                        e.Graphics.DrawString(productline, font, new SolidBrush(Color.Black), startx, starty + offset);
                        offset = offset + (int)fontheight + 7;
                    }
                }
                catch
                {

                }

                for (int i = 0; i < 3; i++)
                {
                    e.Graphics.DrawString("", font, new SolidBrush(Color.Black), startx, starty + offset);
                    offset = offset + (int)fontheight + 5;
                }

                e.Graphics.DrawString("----------------------------------------------------------", printFont, new SolidBrush(Color.Black), startx, starty + offset);
                offset = offset + 8;
                string grosstotal = "Gross Total:".PadRight(7) + Spell.SpellAmount.comma(Convert.ToDecimal(TOTAL_AMOUNT.Text));
                //string vatstring = "Tax Amount:".PadRight(5) + Spell.SpellAmount.comma(Convert.ToDecimal(TAX_TOTAL.Text));
                string Discountstring = "Discount:".PadRight(47) + Spell.SpellAmount.comma(Convert.ToDecimal(DISCOUNT.Text));
                string total = "Total:".PadRight(47) + Spell.SpellAmount.comma(Convert.ToDecimal(NET_AMOUNT.Text));

                //e.Graphics.DrawString(grosstotal, font, new SolidBrush(Color.Black), startx + 290, starty + offset + 6);
                //offset = offset + (int)fontheight +3;
                //e.Graphics.DrawString(vatstring, font, new SolidBrush(Color.Black), startx + 200, starty + offset + 3);
                //offset = offset + (int)fontheight + 4;
                if (Convert.ToDecimal(DISCOUNT.Text) > 0)
                {
                    e.Graphics.DrawString(Discountstring, font, new SolidBrush(Color.Black), startx, starty + offset + 3);
                    offset = offset + (int)fontheight + 1;
                }
                //e.Graphics.DrawString("------------------", font, new SolidBrush(Color.Black), startx + 290, starty + offset + 3);
                //offset = offset + (int)fontheight + 1;
                e.Graphics.DrawString(total, font, new SolidBrush(Color.Black), startx, starty + offset + 3);

                offset = offset + 18;

                e.Graphics.DrawString("----------------------------------------------------------", printFont, new SolidBrush(Color.Black), startx, starty + offset);
                offset = offset + 59;


                //try
                //{
                //    Font amountingeng = new Font("Courier New", 8);
                //    string test = Spell.SpellAmount.InWrods(Convert.ToDecimal(NET_AMOUNT.Text));

                //    int index = test.IndexOf("Taka ");
                //    int l = test.Length;
                //    test = test.Substring(index + 4);

                //    e.Graphics.DrawString(test, amountingeng, new SolidBrush(Color.Black), startx, starty + offset + 3);
                //}
                //catch
                //{
                //}


                try
                {
                    Font amountingeng = new Font("Courier New", 10);


                    e.Graphics.DrawString("THANK YOU VISIT AGAIN...", amountingeng, new SolidBrush(Color.Black), xpos, starty + offset, sf);
                }
                catch
                {
                }





                offset = offset + 15;
                //if (txtcashrcvd.Text != "")
                //{
                //    try
                //    {
                //        decimal balance = Convert.ToDecimal(txtcashrcvd.Text) - Convert.ToDecimal(TOTAL_AMOUNT.Text);
                //        e.Graphics.DrawString("Cash Rcvd:" + Spell.SpellAmount.comma(Convert.ToDecimal(txtcashrcvd.Text)) + "   " + "Balance:" + Spell.SpellAmount.comma(Convert.ToDecimal(balance)).ToString(), font, new SolidBrush(Color.Black), startx, starty + offset);

                //        offset = offset + 12;

                //    }
                //    catch
                //    {
                //    }



                //}

                e.HasMorePages = false;
            }
        }
        
        void printDocumentHalf_PrintPage(object sender, PrintPageEventArgs e)
        {


            float xpos;
            int startx = 50;
            int starty = 20;
            int offset = 15;
            int headerstartposition = 50;

            int w = e.MarginBounds.Width / 2;
            int x = e.MarginBounds.Left;
            int y = e.MarginBounds.Top;
            Font Headerfont1 = new Font("Calibri", 15, FontStyle.Bold);
            Font dec = new Font("Calibri", 8, FontStyle.Regular);
            Font Headerfont2 = new Font("Calibri", 10, FontStyle.Regular);
            Font printFont = new Font("Calibri", 10);
            Font printbold = new Font("Calibri", 10, FontStyle.Bold);

            Font printnet = new Font("Calibri", 11, FontStyle.Bold);
            var tabDataForeColor = Color.Black;
            int height = 100 + y;
            decimal tgrossrate, ttaxva, trate;
            Int32 tqty;
            tqty = 0; trate = 0; ttaxva = 0; tgrossrate = 0;

            var txtDataWidth = e.Graphics.MeasureString(CompanyName, Headerfont1).Width;

            try
            {
                //if (logo != null || logo != "")
                //{

                //    System.Drawing.Image img = System.Drawing.Image.FromFile(logo);

                //    Point loc = new Point(20, 50);
                //    e.Graphics.DrawImage(img, new Rectangle(50, 50, 50, 50));
                //}
                //else
                //{
                Image i = Image.FromFile("d://logo.gif");
                Point p = new Point(100, 100);
                e.Graphics.DrawImage(i, new Rectangle(10, 10, 230, 60));
                //System.Drawing.Image img = System.Drawing.Image.FromFile("zed_logo.gif");
                //Point loc = new Point(20, 50);
                //e.Graphics.DrawImage(img, new Rectangle(50, 50, 50, 50));
                // }
            }
            catch (Exception ex)
            {

            }


            using (var sf = new StringFormat())
            {
                height += 15;
                sf.LineAlignment = StringAlignment.Center;
                sf.Alignment = StringAlignment.Center;

                xpos = e.MarginBounds.Left + (e.MarginBounds.Width / 2);

                // e.Graphics.DrawString(CompanyName, Headerfont1, new SolidBrush(tabDataForeColor), xpos, starty, sf);
                //   e.Graphics.DrawString(Addres1+", "+Addres2, Headerfont2, new SolidBrush(tabDataForeColor), xpos, starty + offset, sf);
                //  offset = offset + 20;
                //  e.Graphics.DrawString(Phone, Headerfont2, new SolidBrush(tabDataForeColor), xpos, starty + offset, sf);
                //   offset = offset + 20;
                //  e.Graphics.DrawString("Credit Note: " + DOC_NO.Text, Headerfont2, new SolidBrush(tabDataForeColor), xpos, starty + offset, sf);
                //   offset = offset + 20;

                //  e.Graphics.DrawString(CompanyName, Headerfont1, new SolidBrush(Color.Black), headerstartposition, starty + 40);
                offset = offset + 30;
                e.Graphics.DrawString(Address1 + "," + Address2, Headerfont2, new SolidBrush(Color.Black), headerstartposition, starty + 20 + offset);
                offset = offset + 12;
                e.Graphics.DrawString("Phone:".PadRight(3) + Phone, Headerfont2, new SolidBrush(Color.Black), headerstartposition, starty + 20 + offset);
                offset = offset + 12;
                e.Graphics.DrawString("Email:".PadRight(3) + Email, Headerfont2, new SolidBrush(Color.Black), headerstartposition, starty + 20 + offset);
                offset = offset + 12;
                e.Graphics.DrawString("Website:".PadRight(3) + Website, Headerfont2, new SolidBrush(Color.Black), headerstartposition, starty + 20 + offset);
                offset = offset + 21;
                offset = offset + 11;
                e.Graphics.DrawString("        THE KERALA VALUE ADDED TAX RULES,2005                                              ,Tax Invoice                     [See rule 58(10),(Cash/Credit)]".PadLeft(10), Headerfont2, new SolidBrush(Color.Black), 20, starty + offset + 7);
                e.Graphics.DrawString("Form No.8B".PadLeft(10), printbold, new SolidBrush(Color.Black), 370, starty + offset + 7);
                offset = offset + 16;
                offset = 15;
                e.Graphics.DrawString("Tin No       :" + TineNo, Headerfont2, new SolidBrush(Color.Black), 550, starty);

                e.Graphics.DrawString("CST No      : ", Headerfont2, new SolidBrush(Color.Black), 550, starty + offset);
                //offset = offset + 16;
                offset = offset + 16;
                e.Graphics.DrawString("Invoice No: ", Headerfont2, new SolidBrush(Color.Black), 550, starty + offset);
                e.Graphics.DrawString(Billno, printbold, new SolidBrush(Color.Black), 620, starty + offset);
                offset = offset + 16;
                e.Graphics.DrawString("Date          :", Headerfont2, new SolidBrush(Color.Black), 550, starty + offset);
                e.Graphics.DrawString(DateTime.Now.ToString(), printbold, new SolidBrush(Color.Black), 620, starty + offset);
                offset = offset + 16;
                //  e.Graphics.DrawString("Del.Note No & Date :", Headerfont2, new SolidBrush(Color.Black), 580, starty + offset);
                offset = offset + 16;
                //   e.Graphics.DrawString("Des.Docu.No & Date:", Headerfont2, new SolidBrush(Color.Black), 580, starty + offset);
                offset = offset + 16;




                //e.Graphics.DrawString("Form No.8", Headerfont2, new SolidBrush(Color.Black), 510, starty + offset);
                offset = offset + 16;
                // e.Graphics.DrawString("[See rule 58(10)]", Headerfont2, new SolidBrush(Color.Black), 510, starty + offset);
                offset = offset + 16;
                // e.Graphics.DrawString("Tax Invoice/Cash/Credit", Headerfont2, new SolidBrush(Color.Black), 510, starty + offset);
                offset = offset + 16;
                offset = offset + 16;
                offset = offset + 16;
                Pen blackPen = new Pen(Color.Black, 1);
                Point point1 = new Point(45, 190);
                Point point2 = new Point(760, 190);
                e.Graphics.DrawLine(blackPen, point1, point2);

                //offset = offset + 2;
                //Point point5 = new Point(45, 130);
                //Point point6 = new Point(760, 130);
                //e.Graphics.DrawLine(blackPen, point5,point6);


                //offset = offset + 2;
                //Point point7 = new Point(45, 155);
                //Point point8 = new Point(760, 155);
                //  e.Graphics.DrawLine(blackPen, point7, point8);
                // e.Graphics.DrawLine(blackPen, 45, 130, 45, 130);
                e.Graphics.DrawRectangle(blackPen, 45, 140, 715, 25);




                e.Graphics.DrawString("To:" + CUSTOMER_NAME.Text, Headerfont2, new SolidBrush(Color.Black), startx, starty + offset - 25);
                Font itemhead = new Font("Times New Roman", 8);
                offset = offset + 2;

                Point point3 = new Point(45, 219);
                Point point4 = new Point(760, 219);
                e.Graphics.DrawLine(blackPen, point3, point4);



                e.Graphics.DrawLine(blackPen, 45, 190, 45, 450);

                //zed invoice

                //e.Graphics.DrawLine(blackPen, 45, 110, 45, 160);
                // e.Graphics.DrawLine(blackPen, 45 , 45 , 






                e.Graphics.DrawLine(blackPen, 80, 190, 80, 450);
                e.Graphics.DrawLine(blackPen, 420, 190, 420, 450);

                e.Graphics.DrawLine(blackPen, 475, 190, 475, 470);
                e.Graphics.DrawLine(blackPen, 515, 190, 515, 490);
                e.Graphics.DrawLine(blackPen, 570, 190, 570, 470); //NEW
                e.Graphics.DrawLine(blackPen, 630, 190, 630, 490);
                e.Graphics.DrawLine(blackPen, 680, 190, 680, 470);
                e.Graphics.DrawLine(blackPen, 760, 190, 760, 470);

                e.Graphics.DrawLine(blackPen, 45, 450, 760, 450);
                e.Graphics.DrawRectangle(blackPen, 45, 450, 715, 20);
                e.Graphics.DrawRectangle(blackPen, 45, 470, 715, 20);

                string headtext = "Sl.No".PadRight(10) + "Item".PadRight(100) + "Tax%".PadRight(10) + "Qty".PadRight(9) + "Net Rate".PadRight(13) + "Rate".PadRight(11) + "Tax Val".PadRight(13) + "Total";
                e.Graphics.DrawString(headtext, printbold, new SolidBrush(Color.Black), startx - 4, starty + offset - 1);

                offset = offset + 40;
                Font font = new Font("Times New Roman", 10);
                float fontheight = font.GetHeight();
                try
                {
                    int i = 0;
                    foreach (DataGridViewRow row in dgItems.Rows)
                    {

                        string period, periodtype, tax;
                        i = i + 1;


                        string name = row.Cells["cName"].Value.ToString().Length <= 60 ? row.Cells[1].Value.ToString() : row.Cells[1].Value.ToString().Substring(0, 60);
                        tax = row.Cells[7].Value.ToString();
                        if (tax == "")
                            tax = "1";
                        string qty = row.Cells[5].Value.ToString();
                        string rate = row.Cells[6].Value.ToString();
                        string price = row.Cells[11].Value.ToString();
                        string Serial = row.Cells["SerialNos"].Value.ToString();

                        double pricWtax = Convert.ToDouble(rate) + (Convert.ToDouble(rate) * Convert.ToDouble(qty)) * (Convert.ToDouble(tax) / 100 / Convert.ToDouble(qty));
                        Math.Round(pricWtax, 2);
                        double taxval = (Convert.ToDouble(rate) * Convert.ToDouble(qty)) * (Convert.ToDouble(tax) / 100 / Convert.ToDouble(qty));
                        Math.Round(taxval, 2);
                        tqty = tqty + Convert.ToInt32(qty);
                        tgrossrate = tgrossrate + Convert.ToDecimal(pricWtax);
                        ttaxva = ttaxva + Convert.ToDecimal(taxval);
                        trate = trate + Convert.ToDecimal(rate);

                        string productline = name + tax + qty + rate + price;

                        e.Graphics.DrawString(i.ToString(), font, new SolidBrush(Color.Black), startx, starty + offset);
                        // offset = offset + (int)fontheight + 10;

                        e.Graphics.DrawString(name, printFont, new SolidBrush(Color.Black), startx + 30, starty + offset);

                        e.Graphics.DrawString(tax, font, new SolidBrush(Color.Black), startx + 380, starty + offset);
                        StringFormat format = new StringFormat(StringFormatFlags.DirectionRightToLeft);
                        e.Graphics.DrawString(qty, font, new SolidBrush(Color.Black), startx + 440, starty + offset, format);
                        e.Graphics.DrawString(pricWtax.ToString("F"), font, new SolidBrush(Color.Black), startx + 470, starty + offset);
                        e.Graphics.DrawString(taxval.ToString("F"), font, new SolidBrush(Color.Black), startx + 630, starty + offset, format);
                        e.Graphics.DrawString(rate, font, new SolidBrush(Color.Black), startx + 525, starty + offset);
                        e.Graphics.DrawString(price, font, new SolidBrush(Color.Black), startx + 690, starty + offset, format);

                        offset = offset + (int)fontheight + 10;
                        if (Serial != "")
                        {
                            e.Graphics.DrawString("SN No: " + Serial, font, new SolidBrush(Color.Black), startx + 30, starty + offset);

                            offset = offset + (int)fontheight + 10;
                        }
                        if (row.Cells["description"].Value != "")
                        {
                            e.Graphics.DrawString(row.Cells["description"].Value.ToString(), font, new SolidBrush(Color.Black), startx + 30, starty + offset);
                            offset = offset + (int)fontheight + 13;
                        }
                        if (i == 9)
                        {
                            break;
                        }
                    }
                    e.Graphics.DrawString(tqty.ToString(), printbold, new SolidBrush(Color.Black), startx + 430, 451);
                    e.Graphics.DrawString(trate.ToString(), printbold, new SolidBrush(Color.Black), startx + 470, 451);
                    // e.Graphics.DrawString(tgrossrate.ToString(), Headerfont2, new SolidBrush(Color.Black), startx + 380, 901);
                    e.Graphics.DrawString(ttaxva.ToString(), printbold, new SolidBrush(Color.Black), startx + 580, 451);

                }
                catch (Exception exc)
                {


                }
            }
            e.Graphics.DrawString("E & OE", Headerfont2, new SolidBrush(Color.Black), startx, 491);
            e.Graphics.DrawString("Total", Headerfont2, new SolidBrush(Color.Black), startx + 300, 451);
            e.Graphics.DrawString("Discount: ", Headerfont2, new SolidBrush(Color.Black), startx + 180, 471);
            e.Graphics.DrawString("Freight: ", Headerfont2, new SolidBrush(Color.Black), startx + 60, 471);
            e.Graphics.DrawString("Roundoff: ", Headerfont2, new SolidBrush(Color.Black), startx + 290, 471);
            float newoffset = 450;
            e.Graphics.DrawString(DISCOUNT.Text, printbold, new SolidBrush(Color.Black), startx + 242, 471);
            e.Graphics.DrawString(txtFreight.Text, printbold, new SolidBrush(Color.Black), startx + 110, 471);
            e.Graphics.DrawString(txtRoundOff.Text, printbold, new SolidBrush(Color.Black), startx + 350, 471);
            e.Graphics.DrawString("Note:" + NOTES.Text, Headerfont2, new SolidBrush(Color.Black), startx, starty + newoffset + 35);

            //  e.Graphics.DrawString("Gross Total", Headerfont2, new SolidBrush(Color.Black), startx + 490, starty + newoffset + 1);
            // e.Graphics.DrawString(Spell.SpellAmount.comma(Convert.ToDecimal(TOTAL_AMOUNT.Text)), Headerfont2, new SolidBrush(Color.Black), startx + 610, starty + newoffset + 1);
            e.Graphics.DrawString("Net Amount   ", printbold, new SolidBrush(Color.Black), startx + 490, starty + newoffset + 3);
            e.Graphics.DrawString(Spell.SpellAmount.comma(Convert.ToDecimal(NET_AMOUNT.Text)), printnet, new SolidBrush(Color.Black), startx + 625, starty + newoffset + 3);
            try
            {
                string test = Spell.SpellAmount.InWrods(Convert.ToDecimal(NET_AMOUNT.Text));

                int index = test.IndexOf("Rupees");
                int l = test.Length;
                test = test.Substring(index + 4);

                e.Graphics.DrawString(test, Headerfont2, new SolidBrush(Color.Black), startx + 180, 491);
            }
            catch
            {
            }


            newoffset = newoffset + 20;
            //  e.Graphics.DrawString("Tax Amt", Headerfont2, new SolidBrush(Color.Black), startx + 490, starty + newoffset + 1);
            //  e.Graphics.DrawString(TAX_TOTAL.Text, Headerfont2, new SolidBrush(Color.Black), startx + 610, starty + newoffset + 1);

            newoffset = newoffset + 20;


            // newoffset = newoffset + 20;

            //e.Graphics.DrawString("Tax Amount", Headerfont2, new SolidBrush(Color.Black), startx + 490, starty + newoffset + 3);
            //e.Graphics.DrawString(TAX_TOTAL.Text, Headerfont2, new SolidBrush(Color.Black), startx + 610, starty + newoffset + 3);
            //  e.Graphics.DrawString("---------------------------------------", Headerfont2, new SolidBrush(Color.Black), startx + 490, starty + newoffset + 1);
            newoffset = newoffset + 10;



            // e.Graphics.DrawString("Net Amount:", Headerfont2, new SolidBrush(Color.Black), startx + 490, starty + newoffset + 1);


            //    e.Graphics.DrawString(Spell.SpellAmount.comma(Convert.ToDecimal(NET_AMOUNT.Text)), Headerfont2, new SolidBrush(Color.Black), startx + 610, starty + newoffset + 1);
            // newoffset = newoffset + 12;
            e.Graphics.DrawString("Terms & Condtions", Headerfont2, new SolidBrush(Color.Black), startx, starty + newoffset + 3);
            newoffset = newoffset + 16;
            e.Graphics.DrawString("1)Above Material recieved in good condition", dec, new SolidBrush(Color.Black), startx, starty + newoffset + 3);
            newoffset = newoffset + 16;
            e.Graphics.DrawString("2)Zed IT Shop Act as a Dealer of Goods on behalf of vendor and                                                                                                                    Authorized Signatory", dec, new SolidBrush(Color.Black), startx, starty + newoffset + 3);
            newoffset = newoffset + 16;
            e.Graphics.DrawString("Cannot provide any warranty covered under the bill is as per                                                                                                                        [With Status and Seal]", dec, new SolidBrush(Color.Black), startx, starty + newoffset + 3);
            newoffset = newoffset + 16;
            e.Graphics.DrawString("The waranty terms of the Manufacture", dec, new SolidBrush(Color.Black), startx, starty + newoffset + 3);




            newoffset = newoffset + 16;
            e.Graphics.DrawString("Declaration", dec, new SolidBrush(Color.Black), startx, starty + newoffset + 3);
            newoffset = newoffset + 16;
            e.Graphics.DrawString("Certified all the particulars shown in the above tax invoice are true and Correct and that my/our Registration under KVAT ACT 2003 is valid as", dec, new SolidBrush(Color.Black), startx, starty + newoffset + 3);
            newoffset = newoffset + 16;
            e.Graphics.DrawString("on the date of this bill", dec, new SolidBrush(Color.Black), startx, starty + newoffset + 3);




            e.HasMorePages = false;

        }
        void customizedprivw(object sender, PrintPageEventArgs e)
        {
            bool PRINTTOTALPAGE = true;
            bool hasmorepages = false;
            float xpos;
            int startx = 50;
            int starty = 20;
            int offset = 15;
            int headerstartposition = 50;

            int w = e.MarginBounds.Width / 2;
            int x = e.MarginBounds.Left;
            int y = e.MarginBounds.Top;
            Font Headerfont1 = new Font("Calibri", 15, FontStyle.Bold);
            Font dec = new Font("Calibri", 8, FontStyle.Regular);
            Font Headerfont2 = new Font("Calibri", 10, FontStyle.Regular);
            Font number = new Font("Calibri", 8, FontStyle.Regular);
            Font printFont = new Font("Calibri", 10);
            Font printbold = new Font("Calibri", 10, FontStyle.Bold);

            Font printnet = new Font("Calibri", 11, FontStyle.Bold);
            var tabDataForeColor = Color.Black;
            int height = 100 + y;
            decimal tgrossrate, ttaxva, trate;
            Int32 tqty;
            tqty = 0; trate = 0; ttaxva = 0; tgrossrate = 0;

            var txtDataWidth = e.Graphics.MeasureString(CompanyName, Headerfont1).Width;

            try
            {
                Image i = Image.FromFile("d://logo.gif");
                Point p = new Point(100, 100);
                e.Graphics.DrawImage(i, new Rectangle(10, 10, 230, 60));
            }
            catch (Exception)
            {

            }


            using (var sf = new StringFormat())
            {
                height += 15;
                sf.LineAlignment = StringAlignment.Center;
                sf.Alignment = StringAlignment.Center;

                xpos = e.MarginBounds.Left + (e.MarginBounds.Width / 2);


                offset = offset + 30;
                e.Graphics.DrawString(Address1 + "," + Address2, Headerfont2, new SolidBrush(Color.Black), headerstartposition, starty + 20 + offset);
                offset = offset + 12;

                offset = offset + 16;
                offset = 15;

                offset = offset + 16;
                e.Graphics.DrawString("Estimate", Headerfont1, new SolidBrush(Color.Black), 400, starty + offset);

                e.Graphics.DrawString("Estimate No  :", Headerfont2, new SolidBrush(Color.Black), 550, starty + offset);
                e.Graphics.DrawString(VOUCHNUM.Text, printbold, new SolidBrush(Color.Black), 630, starty + offset);
                offset = offset + 16;
                e.Graphics.DrawString("Date                 :", Headerfont2, new SolidBrush(Color.Black), 550, starty + offset);
                e.Graphics.DrawString(DateTime.Now.ToString(), printbold, new SolidBrush(Color.Black), 630, starty + offset);
                offset = offset + 16;
                //  e.Graphics.DrawString("To                     :", Headerfont2, new SolidBrush(Color.Black), 550, starty + offset);
                // e.Graphics.DrawString(CUSTOMER_NAME.Text, printbold, new SolidBrush(Color.Black), 630, starty + offset);
                offset = offset + 16;
                //   e.Graphics.DrawString("Des.Docu.No & Date:", Headerfont2, new SolidBrush(Color.Black), 580, starty + offset);
                offset = offset + 16;




                //e.Graphics.DrawString("Form No.8", Headerfont2, new SolidBrush(Color.Black), 510, starty + offset);
                offset = offset + 16;
                // e.Graphics.DrawString("[See rule 58(10)]", Headerfont2, new SolidBrush(Color.Black), 510, starty + offset);
                offset = offset + 16;
                // e.Graphics.DrawString("Tax Invoice/Cash/Credit", Headerfont2, new SolidBrush(Color.Black), 510, starty + offset);
                offset = offset + 16;
                offset = offset + 16;
                offset = offset + 16;
                Pen blackPen = new Pen(Color.Black, 1);
                Point point1 = new Point(45, 190);
                Point point2 = new Point(760, 190);
                e.Graphics.DrawLine(blackPen, point1, point2);

                //offset = offset + 2;
                //Point point5 = new Point(45, 130);
                //Point point6 = new Point(760, 130);
                //e.Graphics.DrawLine(blackPen, point5,point6);


                //offset = offset + 2;
                //Point point7 = new Point(45, 155);
                //Point point8 = new Point(760, 155);
                //  e.Graphics.DrawLine(blackPen, point7, point8);
                // e.Graphics.DrawLine(blackPen, 45, 130, 45, 130);
                //  e.Graphics.DrawRectangle(blackPen, 45, 140, 715, 25);




                e.Graphics.DrawString("To:" + CUSTOMER_NAME.Text, Headerfont2, new SolidBrush(Color.Black), startx, starty + offset - 35);
                Font itemhead = new Font("Times New Roman", 8);
                offset = offset + 2;

                Point point3 = new Point(45, 219);
                Point point4 = new Point(760, 219);
                e.Graphics.DrawLine(blackPen, point3, point4);



                e.Graphics.DrawLine(blackPen, 45, 190, 45, 470);

                //zed invoice

                //e.Graphics.DrawLine(blackPen, 45, 110, 45, 160);
                // e.Graphics.DrawLine(blackPen, 45 , 45 , 






                e.Graphics.DrawLine(blackPen, 80, 190, 80, 470);
                e.Graphics.DrawLine(blackPen, 430, 190, 430, 470);

                //e.Graphics.DrawLine(blackPen, 475, 190, 475, 470);
                //uom
                e.Graphics.DrawLine(blackPen, 495, 190, 495, 470);
                //qty
                e.Graphics.DrawLine(blackPen, 565, 190, 565, 470);
                //e.Graphics.DrawLine(blackPen, 610, 190, 610, 490);
                e.Graphics.DrawLine(blackPen, 650, 190, 650, 470);
                e.Graphics.DrawLine(blackPen, 760, 190, 760, 490);

                // e.Graphics.DrawLine(blackPen, 45, 450, 760, 450);
                //e.Graphics.DrawRectangle(blackPen, 45, 450, 715, 20);
                e.Graphics.DrawRectangle(blackPen, 45, 470, 715, 20);

                //tax is not important in estimate so commented 
                //string headtext = "Sl.No".PadRight(10) + "Item".PadRight(100) + "Qty".PadRight(10) + "Rate".PadRight(9) + "Net Rate".PadRight(13) + "Tax%".PadRight(10) + "Tax Val".PadRight(13) + "Total";
                string headtext = "Sl.No".PadRight(10) + "Item".PadRight(105) + "Unit".PadRight(15) + "Qty".PadRight(20) + "Rate".PadRight(22) + "Total";
                e.Graphics.DrawString(headtext, printbold, new SolidBrush(Color.Black), startx - 4, starty + offset - 1);

                offset = offset + 35;
                Font font = new Font("Times New Roman", 10);
                float fontheight = font.GetHeight();
                try
                {
                    int j = 1;
                    int nooflines = 0;

                    foreach (DataGridViewRow row in dgItems.Rows)
                    {
                        PRINTTOTALPAGE = false;
                        if (j > printeditems)
                        {
                            if (nooflines < 12)
                            {

                                string period, periodtype, tax;
                                k = k + 1;

                                string name = row.Cells["cName"].Value.ToString().Length <= 60 ? row.Cells[1].Value.ToString() : row.Cells[1].Value.ToString().Substring(0, 60);
                                string qty = row.Cells[5].Value.ToString();
                                string rate = row.Cells[7].Value.ToString();
                                string price = row.Cells[11].Value.ToString();
                                string uom = row.Cells[4].Value.ToString();
                                //    string Serial = row.Cells["SerialNos"].Value.ToString();


                                tqty = tqty + Convert.ToInt32(qty);

                                trate = trate + Convert.ToDecimal(rate);

                                string productline = name + qty + price;

                                e.Graphics.DrawString(k.ToString(), font, new SolidBrush(Color.Black), startx, starty + offset);
                                // offset = offset + (int)fontheight + 10;

                                e.Graphics.DrawString(name, printFont, new SolidBrush(Color.Black), startx + 30, starty + offset);
                                e.Graphics.DrawString(uom, printFont, new SolidBrush(Color.Black), startx + 390, starty + offset);


                                StringFormat format = new StringFormat(StringFormatFlags.DirectionRightToLeft);
                                e.Graphics.DrawString(qty, font, new SolidBrush(Color.Black), startx + 500, starty + offset, format);

                                e.Graphics.DrawString(rate, font, new SolidBrush(Color.Black), startx + 600, starty + offset, format);
                                string total = (Convert.ToDecimal(qty) * Convert.ToDecimal(rate)).ToString();
                                tgrossrate = tgrossrate + Convert.ToDecimal(total);
                                e.Graphics.DrawString(total, font, new SolidBrush(Color.Black), startx + 705, starty + offset, format);

                                offset = offset + (int)fontheight + 2;
                                //if (Serial != "")
                                //{
                                //    string s = Serial;
                                //    string[] values = s.Split(',');
                                //    for (int i = 0; i < (values.Length) && (i < Convert.ToInt64(qty)); i++)
                                //    {
                                //        values[i] = values[i].Trim();
                                //        e.Graphics.DrawString("SN No: " + values[i].ToString(), font, new SolidBrush(Color.Black), startx + 30, starty + offset);

                                //        offset = offset + (int)fontheight + 2;
                                //        nooflines++;
                                //    }

                                //}
                                nooflines++;
                                //if (k == 12)
                                //{
                                //    break;
                                //}
                                j++;
                            }
                            else
                            {
                                printeditems = j - 1;
                                //  e.HasMorePages = true;
                                hasmorepages = true;
                                PRINTTOTALPAGE = true;
                            }
                        }
                        else
                        {
                            j++;
                        }



                    }


                }
                catch (Exception exc)
                {
                    string s = exc.Message;

                }
            }

            float newoffset = 450;
            e.Graphics.DrawString("Note:" + NOTES.Text, Headerfont2, new SolidBrush(Color.Black), 45, starty + newoffset + 35);
            e.Graphics.DrawString("Authorized Signatory", number, new SolidBrush(Color.Black), 600, starty + newoffset + 35);
            e.Graphics.DrawString("[With Status and Seal]", number, new SolidBrush(Color.Black), 600, starty + newoffset + 45);

            //newoffset = newoffset + 10;
            //e.Graphics.DrawString("Mob:-", number, new SolidBrush(Color.Black), 45, starty + newoffset + 45);
            //offset = offset + 12;
            //e.Graphics.DrawString("9656198448", number, new SolidBrush(Color.Black), 50, starty + newoffset + 55);
            //offset = offset + 12;
            //e.Graphics.DrawString("9605639467", number, new SolidBrush(Color.Black), 50, starty + newoffset + 65);
            //offset = offset + 12;
            //e.Graphics.DrawString("9037243262", number, new SolidBrush(Color.Black), 50, starty + newoffset + 75);
            //offset = offset + 12;
            //e.Graphics.DrawString("9633310444", number, new SolidBrush(Color.Black), 50, starty + newoffset + 85);


            if (!PRINTTOTALPAGE)
            {
                PAGETOTAL = true;
                if (PAGETOTAL)
                {
                    try
                    {
                        StringFormat format = new StringFormat(StringFormatFlags.DirectionRightToLeft);
                        e.Graphics.DrawString("Total", Headerfont2, new SolidBrush(Color.Black), startx + 300, 471);
                        // e.Graphics.DrawString(tqty.ToString(), printbold, new SolidBrush(Color.Black), startx + 500, 471,format);

                        e.Graphics.DrawString(NET_AMOUNT.Text, printbold, new SolidBrush(Color.Black), startx + 705, 471, format);
                        string test = Spell.SpellAmount.InWrods(Convert.ToDecimal(NET_AMOUNT.Text));
                        int index = test.IndexOf("Rupees");
                        int l = test.Length;
                        test = test.Substring(index + 5);

                        e.Graphics.DrawString("Amount in words:" + test, Headerfont2, new SolidBrush(Color.Black), 45, 491);
                    }
                    catch
                    {
                    }
                }

                PAGETOTAL = false;
            }



            e.HasMorePages = hasmorepages;



        }
        void customizedHalfEstimate(object sender, PrintPageEventArgs e)
        {
            bool PRINTTOTALPAGE = true;
            bool hasmorepages = false;
            float xpos;
            int startx = 50;
            int starty = 20;
            int offset = 15;
            int headerstartposition = 50;

            int w = e.MarginBounds.Width / 2;
            int x = e.MarginBounds.Left;
            int y = e.MarginBounds.Top;
            Font Headerfont1 = new Font("Calibri", 15, FontStyle.Bold);
            Font dec = new Font("Calibri", 8, FontStyle.Regular);
            Font Headerfont2 = new Font("Calibri", 10, FontStyle.Regular);
            Font number = new Font("Calibri", 8, FontStyle.Regular);
            Font printFont = new Font("Calibri", 10);
            Font printbold = new Font("Calibri", 10, FontStyle.Bold);

            Font printnet = new Font("Calibri", 11, FontStyle.Bold);
            var tabDataForeColor = Color.Black;
            int height = 100 + y;
            decimal tgrossrate, ttaxva, trate;
            Int32 tqty;
            tqty = 0; trate = 0; ttaxva = 0; tgrossrate = 0;

            var txtDataWidth = e.Graphics.MeasureString(CompanyName, Headerfont1).Width;

            try
            {
                Image i = Image.FromFile("d://logo.gif");
                Point p = new Point(100, 100);
                e.Graphics.DrawImage(i, new Rectangle(10, 10, 230, 60));
            }
            catch (Exception)
            {

            }


            using (var sf = new StringFormat())
            {
                height += 15;
                sf.LineAlignment = StringAlignment.Center;
                sf.Alignment = StringAlignment.Center;

                xpos = e.MarginBounds.Left + (e.MarginBounds.Width / 2);


                offset = offset + 30;
                e.Graphics.DrawString(Address1 + "," + Address2, Headerfont2, new SolidBrush(Color.Black), headerstartposition, starty + 20 + offset);
                offset = offset + 12;
               
                offset = offset + 16;
                offset = 15;

                offset = offset + 16;
                e.Graphics.DrawString("Estimate", Headerfont1, new SolidBrush(Color.Black), 400, starty + offset);

                e.Graphics.DrawString("Estimate No  :", Headerfont2, new SolidBrush(Color.Black), 550, starty + offset);
                e.Graphics.DrawString(VOUCHNUM.Text, printbold, new SolidBrush(Color.Black), 630, starty + offset);
                offset = offset + 16;
                e.Graphics.DrawString("Date                 :", Headerfont2, new SolidBrush(Color.Black), 550, starty + offset);
                e.Graphics.DrawString(DateTime.Now.ToString(), printbold, new SolidBrush(Color.Black), 630, starty + offset);
                offset = offset + 16;
              //  e.Graphics.DrawString("To                     :", Headerfont2, new SolidBrush(Color.Black), 550, starty + offset);
               // e.Graphics.DrawString(CUSTOMER_NAME.Text, printbold, new SolidBrush(Color.Black), 630, starty + offset);
                offset = offset + 16;
                //   e.Graphics.DrawString("Des.Docu.No & Date:", Headerfont2, new SolidBrush(Color.Black), 580, starty + offset);
                offset = offset + 16;




                //e.Graphics.DrawString("Form No.8", Headerfont2, new SolidBrush(Color.Black), 510, starty + offset);
                offset = offset + 16;
                // e.Graphics.DrawString("[See rule 58(10)]", Headerfont2, new SolidBrush(Color.Black), 510, starty + offset);
                offset = offset + 16;
                // e.Graphics.DrawString("Tax Invoice/Cash/Credit", Headerfont2, new SolidBrush(Color.Black), 510, starty + offset);
                offset = offset + 16;
                offset = offset + 16;
                offset = offset + 16;
                Pen blackPen = new Pen(Color.Black, 1);
                Point point1 = new Point(45, 190);
                Point point2 = new Point(760, 190);
                e.Graphics.DrawLine(blackPen, point1, point2);

                //offset = offset + 2;
                //Point point5 = new Point(45, 130);
                //Point point6 = new Point(760, 130);
                //e.Graphics.DrawLine(blackPen, point5,point6);


                //offset = offset + 2;
                //Point point7 = new Point(45, 155);
                //Point point8 = new Point(760, 155);
                //  e.Graphics.DrawLine(blackPen, point7, point8);
                // e.Graphics.DrawLine(blackPen, 45, 130, 45, 130);
                //  e.Graphics.DrawRectangle(blackPen, 45, 140, 715, 25);




                e.Graphics.DrawString("To:" + CUSTOMER_NAME.Text, Headerfont2, new SolidBrush(Color.Black), startx, starty + offset - 35);
                Font itemhead = new Font("Times New Roman", 8);
                offset = offset + 2;

                Point point3 = new Point(45, 219);
                Point point4 = new Point(760, 219);
                e.Graphics.DrawLine(blackPen, point3, point4);



                e.Graphics.DrawLine(blackPen, 45, 190, 45, 470);

                //zed invoice

                //e.Graphics.DrawLine(blackPen, 45, 110, 45, 160);
                // e.Graphics.DrawLine(blackPen, 45 , 45 , 






                e.Graphics.DrawLine(blackPen, 80, 190, 80, 470);
                e.Graphics.DrawLine(blackPen, 430, 190, 430, 470);

                //e.Graphics.DrawLine(blackPen, 475, 190, 475, 470);
                //uom
                e.Graphics.DrawLine(blackPen, 495, 190, 495, 470);
                //qty
                e.Graphics.DrawLine(blackPen, 565, 190, 565, 470);
                //e.Graphics.DrawLine(blackPen, 610, 190, 610, 490);
                e.Graphics.DrawLine(blackPen, 650, 190, 650, 470);
                e.Graphics.DrawLine(blackPen, 760, 190, 760, 490);

                // e.Graphics.DrawLine(blackPen, 45, 450, 760, 450);
                //e.Graphics.DrawRectangle(blackPen, 45, 450, 715, 20);
                e.Graphics.DrawRectangle(blackPen, 45, 470, 715, 20);

                //tax is not important in estimate so commented 
                //string headtext = "Sl.No".PadRight(10) + "Item".PadRight(100) + "Qty".PadRight(10) + "Rate".PadRight(9) + "Net Rate".PadRight(13) + "Tax%".PadRight(10) + "Tax Val".PadRight(13) + "Total";
                string headtext = "Sl.No".PadRight(10) + "Item".PadRight(105) + "Unit".PadRight(15) + "Qty".PadRight(20) + "Rate".PadRight(22) + "Total";
                e.Graphics.DrawString(headtext, printbold, new SolidBrush(Color.Black), startx - 4, starty + offset - 1);

                offset = offset + 35;
                Font font = new Font("Times New Roman", 10);
                float fontheight = font.GetHeight();
                try
                {
                    int j = 1;
                    int nooflines = 0;

                    foreach (DataGridViewRow row in dgItems.Rows)
                    {
                        PRINTTOTALPAGE = false;
                        if (j > printeditems)
                        {
                            if (nooflines < 12)
                            {

                                string period, periodtype, tax;
                                k = k + 1;

                                string name = row.Cells["cName"].Value.ToString().Length <= 60 ? row.Cells[1].Value.ToString() : row.Cells[1].Value.ToString().Substring(0, 60);
                                string qty = row.Cells[5].Value.ToString();
                                string rate = row.Cells[7].Value.ToString();
                                string price = row.Cells[11].Value.ToString();
                                string uom = row.Cells[4].Value.ToString();
                                //    string Serial = row.Cells["SerialNos"].Value.ToString();


                                tqty = tqty + Convert.ToInt32(qty);

                                trate = trate + Convert.ToDecimal(rate);

                                string productline = name + qty + price;

                                e.Graphics.DrawString(k.ToString(), font, new SolidBrush(Color.Black), startx, starty + offset);
                                // offset = offset + (int)fontheight + 10;

                                e.Graphics.DrawString(name, printFont, new SolidBrush(Color.Black), startx + 30, starty + offset);
                                e.Graphics.DrawString(uom, printFont, new SolidBrush(Color.Black), startx + 390, starty + offset);


                                StringFormat format = new StringFormat(StringFormatFlags.DirectionRightToLeft);
                                e.Graphics.DrawString(qty, font, new SolidBrush(Color.Black), startx + 500, starty + offset, format);

                                e.Graphics.DrawString(rate, font, new SolidBrush(Color.Black), startx + 600, starty + offset, format);
                                string total = (Convert.ToDecimal(qty) * Convert.ToDecimal(rate)).ToString();
                                tgrossrate = tgrossrate + Convert.ToDecimal(total);
                                e.Graphics.DrawString(total, font, new SolidBrush(Color.Black), startx + 705, starty + offset, format);

                                offset = offset + (int)fontheight + 2;
                                //if (Serial != "")
                                //{
                                //    string s = Serial;
                                //    string[] values = s.Split(',');
                                //    for (int i = 0; i < (values.Length) && (i < Convert.ToInt64(qty)); i++)
                                //    {
                                //        values[i] = values[i].Trim();
                                //        e.Graphics.DrawString("SN No: " + values[i].ToString(), font, new SolidBrush(Color.Black), startx + 30, starty + offset);

                                //        offset = offset + (int)fontheight + 2;
                                //        nooflines++;
                                //    }

                                //}
                                nooflines++;
                                //if (k == 12)
                                //{
                                //    break;
                                //}
                                j++;
                            }
                            else
                            {
                                printeditems = j - 1;
                                //  e.HasMorePages = true;
                                hasmorepages = true;
                                PRINTTOTALPAGE = true;
                            }
                        }
                        else
                        {
                            j++;
                        }



                    }


                }
                catch (Exception exc)
                {
                    string s = exc.Message;

                }
            }

            float newoffset = 450;
            e.Graphics.DrawString("Note:" + NOTES.Text, Headerfont2, new SolidBrush(Color.Black), 45, starty + newoffset + 35);
            e.Graphics.DrawString("Authorized Signatory", number, new SolidBrush(Color.Black), 600, starty + newoffset + 35);
            e.Graphics.DrawString("[With Status and Seal]", number, new SolidBrush(Color.Black), 600, starty + newoffset + 45);
          
            //newoffset = newoffset + 10;
            //e.Graphics.DrawString("Mob:-", number, new SolidBrush(Color.Black), 45, starty + newoffset + 45);
            //offset = offset + 12;
            //e.Graphics.DrawString("9656198448", number, new SolidBrush(Color.Black), 50, starty + newoffset + 55);
            //offset = offset + 12;
            //e.Graphics.DrawString("9605639467", number, new SolidBrush(Color.Black), 50, starty + newoffset + 65);
            //offset = offset + 12;
            //e.Graphics.DrawString("9037243262", number, new SolidBrush(Color.Black), 50, starty + newoffset + 75);
            //offset = offset + 12;
            //e.Graphics.DrawString("9633310444", number, new SolidBrush(Color.Black), 50, starty + newoffset + 85);


            if (!PRINTTOTALPAGE)
            {
                PAGETOTAL = true;
                if (PAGETOTAL)
                {
                    try
                    {
                        StringFormat format = new StringFormat(StringFormatFlags.DirectionRightToLeft);
                        e.Graphics.DrawString("Total", Headerfont2, new SolidBrush(Color.Black), startx + 300, 471);
                        // e.Graphics.DrawString(tqty.ToString(), printbold, new SolidBrush(Color.Black), startx + 500, 471,format);

                        e.Graphics.DrawString(NET_AMOUNT.Text, printbold, new SolidBrush(Color.Black), startx + 705, 471, format);
                        string test = Spell.SpellAmount.InWrods(Convert.ToDecimal(NET_AMOUNT.Text));
                        int index = test.IndexOf("Rupees");
                        int l = test.Length;
                        test = test.Substring(index + 5);

                        e.Graphics.DrawString("Amount in words:" + test, Headerfont2, new SolidBrush(Color.Black), 45, 491);
                    }
                    catch
                    {
                    }
                }

                PAGETOTAL = false;
            }



            e.HasMorePages = hasmorepages;


        }
        void EstimateHalf_PrintPage(object sender, PrintPageEventArgs e)
        {
            bool PRINTTOTALPAGE = true;
            bool hasmorepages = false;
            float xpos;
            int startx = 50;
            int starty = 20;
            int offset = 15;
            int headerstartposition = 50;

            int w = e.MarginBounds.Width / 2;
            int x = e.MarginBounds.Left;
            int y = e.MarginBounds.Top;
            Font Headerfont1 = new Font("Calibri", 15, FontStyle.Bold);
            Font dec = new Font("Calibri", 8, FontStyle.Regular);
            Font Headerfont2 = new Font("Calibri", 10, FontStyle.Regular);
            Font printFont = new Font("Calibri", 10);
            Font printbold = new Font("Calibri", 10, FontStyle.Bold);

            Font printnet = new Font("Calibri", 11, FontStyle.Bold);
            var tabDataForeColor = Color.Black;
            int height = 100 + y;
            decimal tgrossrate, ttaxva, trate;
            Int32 tqty;
            tqty = 0; trate = 0; ttaxva = 0; tgrossrate = 0;

            var txtDataWidth = e.Graphics.MeasureString(CompanyName, Headerfont1).Width;

            try
            {
                Image i = Image.FromFile("d://logo.gif");
                Point p = new Point(100, 100);
                e.Graphics.DrawImage(i, new Rectangle(10, 10, 230, 60));
            }
            catch (Exception)
            {

            }


            using (var sf = new StringFormat())
            {
                height += 15;
                sf.LineAlignment = StringAlignment.Center;
                sf.Alignment = StringAlignment.Center;

                xpos = e.MarginBounds.Left + (e.MarginBounds.Width / 2);

                // e.Graphics.DrawString(CompanyName, Headerfont1, new SolidBrush(tabDataForeColor), xpos, starty, sf);
                //   e.Graphics.DrawString(Addres1+", "+Addres2, Headerfont2, new SolidBrush(tabDataForeColor), xpos, starty + offset, sf);
                //  offset = offset + 20;
                //  e.Graphics.DrawString(Phone, Headerfont2, new SolidBrush(tabDataForeColor), xpos, starty + offset, sf);
                //   offset = offset + 20;
                //  e.Graphics.DrawString("Credit Note: " + DOC_NO.Text, Headerfont2, new SolidBrush(tabDataForeColor), xpos, starty + offset, sf);
                //   offset = offset + 20;

                //  e.Graphics.DrawString(CompanyName, Headerfont1, new SolidBrush(Color.Black), headerstartposition, starty + 40);
                offset = offset + 30;
                e.Graphics.DrawString(Address1 + "," + Address2, Headerfont2, new SolidBrush(Color.Black), headerstartposition, starty + 20 + offset);
                offset = offset + 12;
                e.Graphics.DrawString("Phone:".PadRight(3) + Phone, Headerfont2, new SolidBrush(Color.Black), headerstartposition, starty + 20 + offset);
                offset = offset + 12;
                e.Graphics.DrawString("Email:".PadRight(3) + Email, Headerfont2, new SolidBrush(Color.Black), headerstartposition, starty + 20 + offset);
                offset = offset + 12;
                e.Graphics.DrawString("Website:".PadRight(3) + Website, Headerfont2, new SolidBrush(Color.Black), headerstartposition, starty + 20 + offset);
                offset = offset + 21;
                offset = offset + 11;
                //       e.Graphics.DrawString("        THE KERALA VALUE ADDED TAX RULES,2005                                              ,Tax Invoice                     [See rule 58(10),(Cash/Credit)]".PadLeft(10), Headerfont2, new SolidBrush(Color.Black), 20, starty + offset + 7);
                //        e.Graphics.DrawString("Form No.8B".PadLeft(10), printbold, new SolidBrush(Color.Black), 370, starty + offset + 7);
                offset = offset + 16;
                offset = 15;
                //       e.Graphics.DrawString("Tin No       :" + TineNo, Headerfont2, new SolidBrush(Color.Black), 550, starty);

                //       e.Graphics.DrawString("CST No      : ", Headerfont2, new SolidBrush(Color.Black), 550, starty + offset);
                //offset = offset + 16;
                offset = offset + 16;
                e.Graphics.DrawString("Estimate", Headerfont1, new SolidBrush(Color.Black), 400, starty + offset + offset);

                e.Graphics.DrawString("Estimate No :", Headerfont2, new SolidBrush(Color.Black), 550, starty + offset);
                e.Graphics.DrawString(Billno, printbold, new SolidBrush(Color.Black), 630, starty + offset);
                offset = offset + 16;
                e.Graphics.DrawString("Date              :", Headerfont2, new SolidBrush(Color.Black), 550, starty + offset);
                e.Graphics.DrawString(DateTime.Now.ToString(), printbold, new SolidBrush(Color.Black), 630, starty + offset);
                offset = offset + 16;
                //  e.Graphics.DrawString("Del.Note No & Date :", Headerfont2, new SolidBrush(Color.Black), 580, starty + offset);
                offset = offset + 16;
                //   e.Graphics.DrawString("Des.Docu.No & Date:", Headerfont2, new SolidBrush(Color.Black), 580, starty + offset);
                offset = offset + 16;




                //e.Graphics.DrawString("Form No.8", Headerfont2, new SolidBrush(Color.Black), 510, starty + offset);
                offset = offset + 16;
                // e.Graphics.DrawString("[See rule 58(10)]", Headerfont2, new SolidBrush(Color.Black), 510, starty + offset);
                offset = offset + 16;
                // e.Graphics.DrawString("Tax Invoice/Cash/Credit", Headerfont2, new SolidBrush(Color.Black), 510, starty + offset);
                offset = offset + 16;
                offset = offset + 16;
                offset = offset + 16;
                Pen blackPen = new Pen(Color.Black, 1);
                Point point1 = new Point(45, 190);
                Point point2 = new Point(760, 190);
                e.Graphics.DrawLine(blackPen, point1, point2);

                //offset = offset + 2;
                //Point point5 = new Point(45, 130);
                //Point point6 = new Point(760, 130);
                //e.Graphics.DrawLine(blackPen, point5,point6);


                //offset = offset + 2;
                //Point point7 = new Point(45, 155);
                //Point point8 = new Point(760, 155);
                //  e.Graphics.DrawLine(blackPen, point7, point8);
                // e.Graphics.DrawLine(blackPen, 45, 130, 45, 130);
                //  e.Graphics.DrawRectangle(blackPen, 45, 140, 715, 25);




                e.Graphics.DrawString("To:" + CUSTOMER_NAME.Text, Headerfont2, new SolidBrush(Color.Black), startx, starty + offset - 35);
                Font itemhead = new Font("Times New Roman", 8);
                offset = offset + 2;

                Point point3 = new Point(45, 219);
                Point point4 = new Point(760, 219);
                e.Graphics.DrawLine(blackPen, point3, point4);



                e.Graphics.DrawLine(blackPen, 45, 190, 45, 450);

                //zed invoice

                //e.Graphics.DrawLine(blackPen, 45, 110, 45, 160);
                // e.Graphics.DrawLine(blackPen, 45 , 45 , 






                e.Graphics.DrawLine(blackPen, 80, 190, 80, 450);
                e.Graphics.DrawLine(blackPen, 420, 190, 420, 450);

                e.Graphics.DrawLine(blackPen, 475, 190, 475, 470);
                e.Graphics.DrawLine(blackPen, 515, 190, 515, 490);
                e.Graphics.DrawLine(blackPen, 570, 190, 570, 470); //NEW
                e.Graphics.DrawLine(blackPen, 630, 190, 630, 490);
                e.Graphics.DrawLine(blackPen, 680, 190, 680, 470);
                e.Graphics.DrawLine(blackPen, 760, 190, 760, 470);

                e.Graphics.DrawLine(blackPen, 45, 450, 760, 450);
                e.Graphics.DrawRectangle(blackPen, 45, 450, 715, 20);
                e.Graphics.DrawRectangle(blackPen, 45, 470, 715, 20);

                string headtext = "Sl.No".PadRight(10) + "Item".PadRight(100) + "Tax%".PadRight(10) + "Qty".PadRight(9) + "Net Rate".PadRight(13) + "Rate".PadRight(11) + "Tax Val".PadRight(13) + "Total";
                e.Graphics.DrawString(headtext, printbold, new SolidBrush(Color.Black), startx - 4, starty + offset - 1);

                offset = offset + 40;
                Font font = new Font("Times New Roman", 10);
                float fontheight = font.GetHeight();
                try
                {
                    int j = 1;
                    int nooflines = 0;

                    foreach (DataGridViewRow row in dgItems.Rows)
                    {
                        PRINTTOTALPAGE = false;
                        if (j > printeditems)
                        {
                            if (nooflines < 8)
                            {

                                string period, periodtype, tax;
                                k = k + 1;

                                string name = row.Cells["cName"].Value.ToString().Length <= 60 ? row.Cells[1].Value.ToString() : row.Cells[1].Value.ToString().Substring(0, 60);
                                tax = row.Cells["cTaxPer"].Value.ToString();
                                if (tax == "")
                                    tax = "1";
                                string qty = row.Cells[5].Value.ToString();
                                string rate = row.Cells[6].Value.ToString();
                                string price = row.Cells[11].Value.ToString();
                                string Serial = row.Cells["SerialNos"].Value.ToString();

                                double pricWtax = Convert.ToDouble(rate) + (Convert.ToDouble(rate) * Convert.ToDouble(qty)) * (Convert.ToDouble(tax) / 100 / Convert.ToDouble(qty));
                                Math.Round(pricWtax, 2);
                                double taxval = (Convert.ToDouble(rate) * Convert.ToDouble(qty)) * (Convert.ToDouble(tax) / 100 / Convert.ToDouble(qty));
                                Math.Round(taxval, 2);
                                tqty = tqty + Convert.ToInt32(qty);
                                tgrossrate = tgrossrate + Convert.ToDecimal(pricWtax);
                                ttaxva = ttaxva + Convert.ToDecimal(taxval);
                                trate = trate + Convert.ToDecimal(rate);

                                string productline = name + tax + qty + rate + price;

                                e.Graphics.DrawString(k.ToString(), font, new SolidBrush(Color.Black), startx, starty + offset);
                                // offset = offset + (int)fontheight + 10;

                                e.Graphics.DrawString(name, printFont, new SolidBrush(Color.Black), startx + 30, starty + offset);

                                e.Graphics.DrawString(tax, font, new SolidBrush(Color.Black), startx + 380, starty + offset);
                                StringFormat format = new StringFormat(StringFormatFlags.DirectionRightToLeft);
                                e.Graphics.DrawString(qty, font, new SolidBrush(Color.Black), startx + 440, starty + offset, format);
                                e.Graphics.DrawString(pricWtax.ToString("F"), font, new SolidBrush(Color.Black), startx + 470, starty + offset);
                                e.Graphics.DrawString(taxval.ToString("F"), font, new SolidBrush(Color.Black), startx + 620, starty + offset, format);
                                e.Graphics.DrawString(rate, font, new SolidBrush(Color.Black), startx + 525, starty + offset);
                                e.Graphics.DrawString(price, font, new SolidBrush(Color.Black), startx + 690, starty + offset, format);

                                offset = offset + (int)fontheight + 2;
                                if (Serial != "")
                                {
                                    string s = Serial;
                                    string[] values = s.Split(',');
                                    for (int i = 0; i < (values.Length) && (i < Convert.ToInt64(qty)); i++)
                                    {
                                        values[i] = values[i].Trim();
                                        e.Graphics.DrawString("SN No: " + values[i].ToString(), font, new SolidBrush(Color.Black), startx + 30, starty + offset);

                                        offset = offset + (int)fontheight + 2;
                                        nooflines++;
                                    }

                                }
                                if (row.Cells["description"].Value != "")
                                {
                                    e.Graphics.DrawString(row.Cells["description"].Value.ToString(), font, new SolidBrush(Color.Black), startx + 30, starty + offset);
                                    offset = offset + (int)fontheight + 2;
                                }
                                else
                                {
                                    //e.Graphics.DrawString("No warranty " , font, new SolidBrush(Color.Black), startx + 30, starty + offset);
                                    //offset = offset + (int)fontheight + 2;

                                }
                                nooflines++;
                                if (k == 9)
                                {
                                    break;
                                }
                                j++;
                            }
                            else
                            {
                                printeditems = j - 1;
                                //  e.HasMorePages = true;
                                hasmorepages = true;
                                PRINTTOTALPAGE = true;
                            }
                        }
                        else
                        {
                            j++;
                        }



                    }


                }
                catch (Exception exc)
                {
                    string s = exc.Message;

                }
            }
            e.Graphics.DrawString("E & OE", Headerfont2, new SolidBrush(Color.Black), startx, 491);

            e.Graphics.DrawString("Discount: ", Headerfont2, new SolidBrush(Color.Black), startx + 180, 471);
            e.Graphics.DrawString("Freight: ", Headerfont2, new SolidBrush(Color.Black), startx + 60, 471);
            e.Graphics.DrawString("Roundoff: ", Headerfont2, new SolidBrush(Color.Black), startx + 290, 471);
            float newoffset = 450;
            e.Graphics.DrawString(DISCOUNT.Text, printbold, new SolidBrush(Color.Black), startx + 242, 471);
            e.Graphics.DrawString(txtFreight.Text, printbold, new SolidBrush(Color.Black), startx + 110, 471);
            e.Graphics.DrawString(txtRoundOff.Text, printbold, new SolidBrush(Color.Black), startx + 350, 471);
            e.Graphics.DrawString("Note:" + NOTES.Text, Headerfont2, new SolidBrush(Color.Black), startx, starty + newoffset + 35);

            //  e.Graphics.DrawString("Gross Total", Headerfont2, new SolidBrush(Color.Black), startx + 490, starty + newoffset + 1);
            // e.Graphics.DrawString(Spell.SpellAmount.comma(Convert.ToDecimal(TOTAL_AMOUNT.Text)), Headerfont2, new SolidBrush(Color.Black), startx + 610, starty + newoffset + 1);


            if (!PRINTTOTALPAGE)
            {
                PAGETOTAL = true;
                if (PAGETOTAL)
                {
                    try
                    {
                        e.Graphics.DrawString("Total", Headerfont2, new SolidBrush(Color.Black), startx + 300, 451);
                        e.Graphics.DrawString(tqty.ToString(), printbold, new SolidBrush(Color.Black), startx + 430, 451);
                        e.Graphics.DrawString(trate.ToString(), printbold, new SolidBrush(Color.Black), startx + 470, 451);
                        // e.Graphics.DrawString(tgrossrate.ToString(), Headerfont2, new SolidBrush(Color.Black), startx + 380, 901);
                        e.Graphics.DrawString(ttaxva.ToString(), printbold, new SolidBrush(Color.Black), startx + 580, 451);
                        string test = Spell.SpellAmount.InWrods(Convert.ToDecimal(NET_AMOUNT.Text));
                        e.Graphics.DrawString("Net Amount   ", printbold, new SolidBrush(Color.Black), startx + 505, starty + newoffset + 3);

                        int index = test.IndexOf("Rupees");
                        int l = test.Length;
                        test = test.Substring(index + 4);
                        e.Graphics.DrawString(Spell.SpellAmount.comma(Convert.ToDecimal(NET_AMOUNT.Text)), printnet, new SolidBrush(Color.Black), startx + 625, starty + newoffset + 3);
                        e.Graphics.DrawString(test, Headerfont2, new SolidBrush(Color.Black), startx + 180, 491);
                    }
                    catch
                    {
                    }
                }

                PAGETOTAL = false;
            }


            newoffset = newoffset + 20;
            //  e.Graphics.DrawString("Tax Amt", Headerfont2, new SolidBrush(Color.Black), startx + 490, starty + newoffset + 1);
            //  e.Graphics.DrawString(TAX_TOTAL.Text, Headerfont2, new SolidBrush(Color.Black), startx + 610, starty + newoffset + 1);

            newoffset = newoffset + 20;


            // newoffset = newoffset + 20;

            //e.Graphics.DrawString("Tax Amount", Headerfont2, new SolidBrush(Color.Black), startx + 490, starty + newoffset + 3);
            //e.Graphics.DrawString(TAX_TOTAL.Text, Headerfont2, new SolidBrush(Color.Black), startx + 610, starty + newoffset + 3);
            //  e.Graphics.DrawString("---------------------------------------", Headerfont2, new SolidBrush(Color.Black), startx + 490, starty + newoffset + 1);
            newoffset = newoffset + 3;



            // e.Graphics.DrawString("Net Amount:", Headerfont2, new SolidBrush(Color.Black), startx + 490, starty + newoffset + 1);


            //    e.Graphics.DrawString(Spell.SpellAmount.comma(Convert.ToDecimal(NET_AMOUNT.Text)), Headerfont2, new SolidBrush(Color.Black), startx + 610, starty + newoffset + 1);
            // newoffset = newoffset + 12;
            e.Graphics.DrawString("Terms & Condtions", Headerfont2, new SolidBrush(Color.Black), startx, starty + newoffset + 1);
            newoffset = newoffset + 16;
            e.Graphics.DrawString("1)Above Material recieved in good condition", dec, new SolidBrush(Color.Black), startx, starty + newoffset + 2);
            newoffset = newoffset + 8;
            e.Graphics.DrawString("2)Zed IT Shop Act as a Dealer of Goods on behalf of vendor and                                                                                                                    Authorized Signatory", dec, new SolidBrush(Color.Black), startx, starty + newoffset + 3);
            newoffset = newoffset + 8;
            e.Graphics.DrawString("Cannot provide any warranty covered under the bill is as per                                                                                                                        [With Status and Seal]", dec, new SolidBrush(Color.Black), startx, starty + newoffset + 3);
            newoffset = newoffset + 8;
            e.Graphics.DrawString("The waranty terms of the Manufacture", dec, new SolidBrush(Color.Black), startx, starty + newoffset + 3);
            newoffset = newoffset + 16;





            e.Graphics.DrawString("Declaration", dec, new SolidBrush(Color.Black), startx, starty + newoffset + 3);
            newoffset = newoffset + 16;
            e.Graphics.DrawString("Certified all the particulars shown in the above tax invoice are true and Correct and that my/our Registration under KVAT ACT 2003 is valid as", dec, new SolidBrush(Color.Black), startx, starty + newoffset + 3);
            newoffset = newoffset + 16;
            e.Graphics.DrawString("on the date of this bill", dec, new SolidBrush(Color.Black), startx, starty + newoffset + 3);




            e.HasMorePages = hasmorepages;

        }
        
        public string ConvertToEasternArabicNumerals(string input)
        {
            try
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
            catch
            {
                return "";
            }

        }
        
        void printDocumentArabicA4_PrintPage(object sender, PrintPageEventArgs e)
        {
            float xpos;
            int startx = 50;
            int starty = 30;
            int offset = 15;
            int headerstartposition = 50;
            int w = e.MarginBounds.Width / 2;
            int x = e.MarginBounds.Left;
            int y = e.MarginBounds.Top;
            Font Headerfont1 = new Font("Times New Roman", 15, FontStyle.Bold);
            Font arabicfont = new Font("Times New Roman", 8);
            Font Headerfont2 = new Font("Times New Roman", 10, FontStyle.Bold);
            Font InvoiceFont = new Font("Times New Roman", 15, FontStyle.Bold);
            Font printFont = new Font("Times New Roman", 10);
            var tabDataForeColor = Color.Black;
            int height = 100 + y;
            var txtDataWidth = e.Graphics.MeasureString(CompanyName, Headerfont1).Width;
            xpos = e.MarginBounds.Left + (e.MarginBounds.Width / 2);

            try
            {
                if (logo != null || logo != "")
                {
                    System.Drawing.Image img = System.Drawing.Image.FromFile(logo);
                    Point loc = new Point(Convert.ToInt32(xpos), 50);
                    float width, imgheight;
                    width = img.Width;
                    imgheight = img.Height;
                    if (width > 300)
                    {
                        using (Bitmap bitmap = (Bitmap)Image.FromFile(logo))
                        {
                            bitmap.SetResolution(300, 300);
                            int a = Convert.ToInt32(xpos);
                            e.Graphics.DrawImage(bitmap, new Rectangle(410 - 150, 50, 300, 70));
                        }
                    }
                    else
                    {

                        e.Graphics.DrawImage(img, new Rectangle(Convert.ToInt32(xpos) + 385 - 150, 315, 500, 70));
                    }
                }

            }
            catch (Exception ee){}




            try
            {
                string item = Application.StartupPath + "\\Items2.jpg";
                System.Drawing.Image imgs = System.Drawing.Image.FromFile(item);
                Point loc = new Point(Convert.ToInt32(xpos), 50);
                e.Graphics.DrawImage(imgs, new Rectangle(50, 275, 750, 20));
            }
            catch (Exception ee){}

            StringFormat format = new StringFormat(StringFormatFlags.DirectionRightToLeft);
            using (var sf = new StringFormat())
            {
                height += 15;
                sf.LineAlignment = StringAlignment.Center;
                sf.Alignment = StringAlignment.Center;
                xpos = e.MarginBounds.Left + (e.MarginBounds.Width / 2);
                e.Graphics.DrawString(CompanyName, Headerfont1, new SolidBrush(tabDataForeColor), headerstartposition, starty);
                e.Graphics.DrawString(ArabicName, Headerfont1, new SolidBrush(tabDataForeColor), 760, starty, format);
                offset = offset + 9;
                e.Graphics.DrawString(EngBranch, Headerfont2, new SolidBrush(tabDataForeColor), headerstartposition, starty + offset);
                e.Graphics.DrawString(ArbBranch, Headerfont2, new SolidBrush(tabDataForeColor), 760, starty + offset, format);
                offset = offset + 15;
                e.Graphics.DrawString(Address1, Headerfont2, new SolidBrush(tabDataForeColor), headerstartposition, starty + offset);
                e.Graphics.DrawString(ArbAddress1, Headerfont2, new SolidBrush(tabDataForeColor), 760, starty + offset, format);
                offset = offset + 15;
                e.Graphics.DrawString(Address2, Headerfont2, new SolidBrush(tabDataForeColor), headerstartposition, starty + offset);
                e.Graphics.DrawString(ArbAddress2, Headerfont2, new SolidBrush(tabDataForeColor), 760, starty + offset, format);
                offset = offset + 15;
                e.Graphics.DrawString(Phone, Headerfont2, new SolidBrush(tabDataForeColor), headerstartposition, starty + offset);
                e.Graphics.DrawString(ConvertToEasternArabicNumerals(Phone), Headerfont2, new SolidBrush(tabDataForeColor), 760, starty + offset, format);
                offset = offset + 15;
                e.Graphics.DrawString(Email, Headerfont2, new SolidBrush(tabDataForeColor), headerstartposition, starty + offset);
                e.Graphics.DrawString(Email, Headerfont2, new SolidBrush(tabDataForeColor), 760, starty + offset, format);
                offset = offset + 25;
                e.Graphics.DrawString("Invoice No:" + Billno, Headerfont2, new SolidBrush(tabDataForeColor), headerstartposition, starty + offset);
                e.Graphics.DrawString("رقم الفاتورة", Headerfont2, new SolidBrush(tabDataForeColor), 760, starty + offset, format);
                e.Graphics.DrawString(ConvertToEasternArabicNumerals(Billno) + " :", Headerfont2, new SolidBrush(tabDataForeColor), 660, starty + offset);
                offset = offset + 16;
                e.Graphics.DrawString("Date: " + DOC_DATE_GRE.Value.ToString(), Headerfont2, new SolidBrush(tabDataForeColor), headerstartposition, starty + offset);
                e.Graphics.DrawString("الهجرة", Headerfont2, new SolidBrush(tabDataForeColor), 760, starty + offset, format);
                e.Graphics.DrawString(DOC_DATE_HIJ.Text + " :", Headerfont2, new SolidBrush(tabDataForeColor), 660, starty + offset);
                offset = offset + 16;
                e.Graphics.DrawString("Invoice فاتورة", InvoiceFont, new SolidBrush(tabDataForeColor), xpos + 400, starty + offset + 70, sf);
                offset = offset + 16;
                Pen blackPen = new Pen(Color.Black, 1);
                Point point1 = new Point(0, 200);
                Point point2 = new Point(900, 200);
                e.Graphics.DrawLine(blackPen, point1, point2);
                e.Graphics.DrawString("Name(اسم)", Headerfont2, new SolidBrush(tabDataForeColor), startx, starty + offset - 15);
                e.Graphics.DrawString(CUSTOMER_NAME.Text, Headerfont2, new SolidBrush(tabDataForeColor), startx + 80, starty + offset - 15);
                Font itemhead = new Font("Times New Roman", 10);
                offset = offset + 2;
                Point point3 = new Point(45, 300);
                Point point4 = new Point(760, 300);
                e.Graphics.DrawLine(blackPen, point3, point4);
                e.Graphics.DrawLine(blackPen, 45, 300, 45, 1000);
                //e.Graphics.DrawLine(blackPen, 355, 219, 355, 900);
                e.Graphics.DrawLine(blackPen, 450, 300, 450, 1000);
                e.Graphics.DrawLine(blackPen, 540, 300, 540, 1000);
                e.Graphics.DrawLine(blackPen, 650, 300, 650, 1000);
                e.Graphics.DrawLine(blackPen, 760, 300, 760, 1000);
                e.Graphics.DrawLine(blackPen, 45, 1000, 760, 1000);
                string headtext = "Item".PadRight(115) + "Qty".PadRight(20) + "Unit Price".PadRight(30) + "Total";
                e.Graphics.DrawString(headtext, Headerfont2, new SolidBrush(Color.Black), startx, starty + offset + 70);
                offset = offset + 120;
                Font font = new Font("Times New Roman", 10, FontStyle.Bold);
                float fontheight = font.GetHeight();
                try
                {
                    foreach (DataGridViewRow row in dgItems.Rows)
                    {
                        string name = row.Cells["cName"].Value.ToString().Length <= 40 ? row.Cells["cName"].Value.ToString() : row.Cells["cName"].Value.ToString().Substring(0, 40);
                        string arbname = row.Cells["cDescArb"].Value.ToString();
                        string qty = row.Cells["cQty"].Value.ToString();
                        string rate = row.Cells["cPrice"].Value.ToString();
                        string price = row.Cells["cTotal"].Value.ToString();
                        string productline = name + qty + rate + price;
                        e.Graphics.DrawString(name, font, new SolidBrush(Color.Black), startx, starty + offset);
                        //   e.Graphics.DrawString(arbname, font, new SolidBrush(Color.Black), startx+380, starty + offset,format);

                        e.Graphics.DrawString(qty, font, new SolidBrush(Color.Black), startx + 430, starty + offset);
                        e.Graphics.DrawString(rate, font, new SolidBrush(Color.Black), startx + 525, starty + offset);
                        e.Graphics.DrawString(price, font, new SolidBrush(Color.Black), startx + 630, starty + offset);
                        offset = offset + (int)fontheight + 9;
                        if (hasArabic)
                        {
                            //string Astr = "سُوْرَةُ الْفَاتِحَة";
                            //var num = "-1";
                            //var LRM = ((char)0x200E).ToString();
                            //var result = Astr + LRM + num;
                            //var correctedValue = Regex.Replace(arbname,"(?<=[0-9])(?=[A-Za-z])|(?<=[A-Za-z])(?=[0-9])",LRM);
                            PictureBox pkgb = new PictureBox();
                            pkgb.Image = ConverttoImage(arbname, "Times New Roman", 10);


                            try
                            {

                                System.Drawing.Image imgs = pkgb.Image;

                                Point loc = new Point(Convert.ToInt32(xpos), 50);
                                e.Graphics.DrawImage(imgs, new Rectangle(startx, starty + offset, imgs.Width, 15));
                            }
                            catch
                            {
                            }

                            //using (StringFormat formats = new StringFormat(StringFormatFlags.DirectionRightToLeft))
                            //{
                            //    e.Graphics.DrawString(TX.Text, font, new SolidBrush(Color.Black), startx + 400, starty + offset,formats);
                            //}
                            //e.Graphics.DrawString(arbname, font, new SolidBrush(Color.Black), startx, starty + offset);
                            offset = offset + (int)fontheight + 10;
                        }
                        else
                        {
                            offset = offset + (int)fontheight + 12;
                        }
                    }
                }
                catch
                {

                }
            }

            float newoffset = 970;

            e.Graphics.DrawString(NOTES.Text, Headerfont2, new SolidBrush(Color.Black), startx, starty + newoffset + 3);

            e.Graphics.DrawString("Gross Total المجموع الكلي", Headerfont2, new SolidBrush(Color.Black), startx + 470, starty + newoffset + 3);
            e.Graphics.DrawString(Spell.SpellAmount.comma(Convert.ToDecimal(TOTAL_AMOUNT.Text)), Headerfont2, new SolidBrush(Color.Black), startx + 620, starty + newoffset + 3);
            try
            {
                string test = Spell.SpellAmount.InWrods(Convert.ToDecimal(NET_AMOUNT.Text));

                int index = test.IndexOf("Taka");
                int l = test.Length;
                test = test.Substring(index + 4);

                e.Graphics.DrawString(test, Headerfont2, new SolidBrush(Color.Black), startx, starty + newoffset + 3);
            }
            catch
            {
            }


            //newoffset = newoffset + 20;
            //e.Graphics.DrawString("Tax Amt", Headerfont2, new SolidBrush(Color.Black), startx + 490, starty + newoffset + 3);
            //e.Graphics.DrawString(TAX_TOTAL.Text, Headerfont2, new SolidBrush(Color.Black), startx + 610, starty + newoffset + 3);

            newoffset = newoffset + 20;
            e.Graphics.DrawString("Discount تخفيض ", Headerfont2, new SolidBrush(Color.Black), startx + 470, starty + newoffset + 3);
            e.Graphics.DrawString(DISCOUNT.Text, Headerfont2, new SolidBrush(Color.Black), startx + 620, starty + newoffset + 3);

            newoffset = newoffset + 20;

            //e.Graphics.DrawString("Tax Amount", Headerfont2, new SolidBrush(Color.Black), startx + 490, starty + newoffset + 3);
            //e.Graphics.DrawString(TAX_TOTAL.Text, Headerfont2, new SolidBrush(Color.Black), startx + 610, starty + newoffset + 3);
            e.Graphics.DrawString("---------------------------------------", Headerfont2, new SolidBrush(Color.Black), startx + 490, starty + newoffset + 9);
            newoffset = newoffset + 25;

            e.Graphics.DrawString("Signature توقيع", Headerfont2, new SolidBrush(Color.Black), startx, starty + newoffset + 3);


            offset = offset + 25;
            e.Graphics.DrawString("Net Total المجموع الصافي", Headerfont2, new SolidBrush(Color.Black), startx + 470, starty + newoffset + 3);
            //offset = offset + 25;
            //e.Graphics.DrawString("المجموع الصافي" + ConvertToEasternArabicNumerals(NET_AMOUNT.Text), Headerfont2, new SolidBrush(Color.Black), startx + 470, starty + newoffset + 3);


            e.Graphics.DrawString(Spell.SpellAmount.comma(Convert.ToDecimal(NET_AMOUNT.Text)), Headerfont2, new SolidBrush(Color.Black), startx + 620, starty + newoffset + 3);

            newoffset = newoffset + 20;






            e.HasMorePages = false;
        }
        
        public static Bitmap ConverttoImage(string text, string fontName, int fontSize)
        {
            Bitmap b = new Bitmap(1, 1);
            try
            {

                Graphics g = Graphics.FromImage(b);
                Font f = new Font(fontName, fontSize);
                SizeF sf = g.MeasureString(text, f);
                b = new Bitmap(b, (int)sf.Width, (int)sf.Height);
                g = Graphics.FromImage(b);
                g.DrawString(text, f, Brushes.Black, 0, 0);
                f.Dispose();
                g.Flush();
                g.Dispose();
                //  return b;

            }
            catch
            {

            }
            return b;
        }
        
        //reversing string
        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
        
        //drawing contents as image
        private Image DrawText(String text, Font font, int x, int y, int offset)
        {
            //first, create a dummy bitmap just to get a graphics object
            Image img = new Bitmap(1, 1);
            Graphics drawing = Graphics.FromImage(img);

            //measure the string to see how big the image needs to be
            SizeF textSize = drawing.MeasureString(text, font);

            //free up the dummy image and old graphics object
            img.Dispose();
            drawing.Dispose();

            //create a new image of the right size
            img = new Bitmap((int)textSize.Width, (int)textSize.Height);

            drawing = Graphics.FromImage(img);

            //paint the background
            drawing.Clear(Color.White);

            //create a brush for the text
            Brush textBrush = new SolidBrush(Color.Black);

            drawing.DrawString(text, font, new SolidBrush(Color.Black), x, y + offset + 50);

            drawing.Save();

            textBrush.Dispose();
            drawing.Dispose();

            return img;

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
        
        void printDocumentA4Form8_PrintPage(object sender, PrintPageEventArgs e)
        {
            #region remove code after successful gst implementation.
            /*m = 0;
            bool hasmorepages = false;
            float xpos;
            int startx = 50;
            int starty = 20;
            int offset = 15;
            int headerstartposition = 50;
            bool PRINTTOTALPAGE = true;
            int w = e.MarginBounds.Width / 2;
            int x = e.MarginBounds.Left;
            int y = e.MarginBounds.Top;

            Font Headerfont1 = new Font("Calibri", 15, FontStyle.Bold);
            Font dec = new Font("Calibri", 8, FontStyle.Regular);
            Font Headerfont2 = new Font("Times New Roman", 8, FontStyle.Regular);
            Font Headerfont3 = new Font("Times New Roman", 12, FontStyle.Bold);
            Font Headerfont5 = new Font("Times New Roman", 10, FontStyle.Regular);
            Font Headerfont6 = new Font("Times New Roman", 10, FontStyle.Bold);
            Font Headerfont7 = new Font("Times New Roman", 13, FontStyle.Regular);
            Font Headerfont8 = new Font("Times New Roman", 11, FontStyle.Bold);
            Font printFont = new Font("Calibri", 10);
            Font printFontBold = new Font("Calibri", 10, FontStyle.Bold);
            Font printFontBold1 = new Font("Calibri", 14, FontStyle.Bold);
            Font printFontnet = new Font("Calibri", 11, FontStyle.Bold);
            Font printbold = new Font("Calibri", 10, FontStyle.Bold);
            string address1 = "";
            string address2 = "";
            string address3 = "";
            string disc = "";
            string qty = "";
            string rate = "";
            string gross = "";
            double grossamount = 0;
            double grossamounttotal = 0;
            double netamount = 0;
            double netamounttotal = 0;
            string price = "";
            double taxval = 0;
            string discp = "";
            double grsstot = 0;
            string txbval = "";
            string free = "0";
            string UOM = "";
            string unt = "";
            var tabDataForeColor = Color.Black;
            int height = 100 + y;

            Pen blackPen1 = new Pen(Color.Black, 1);
            var txtDataWidth = e.Graphics.MeasureString(CompanyName, Headerfont1).Width;

            try
            {

                address1 = "CLASSIC TRADE LINKS";
                address2 = "Door No.6/575-D, Nallalam Road, Areekkad, Nallalam P O";
                string mob1 = "7561020007";
                e.Graphics.DrawString(address1, Headerfont3, new SolidBrush(Color.Black), 320, 40);
                e.Graphics.DrawString(address2, Headerfont3, new SolidBrush(Color.Black), 220, 60);
                e.Graphics.DrawString("Ph : " + mob1, Headerfont2, new SolidBrush(Color.Black), 25, 35);
                e.Graphics.DrawString("Fax : ", Headerfont2, new SolidBrush(Color.Black), 25, 50);
                e.Graphics.DrawString("THE KERALA VALUE ADDED TAX RULES 2005", Headerfont2, new SolidBrush(Color.Black), 550, 90);
                e.Graphics.DrawString("FORM NO.8", Headerfont6, new SolidBrush(Color.Black), 710, 105);
                e.Graphics.DrawString("[See rule 58(10)]", Headerfont2, new SolidBrush(Color.Black), 605, 120);
                e.Graphics.DrawString("TAX INVOICE", Headerfont6, new SolidBrush(Color.Black), 694, 120);
                e.Graphics.DrawString("Date" + "          : " + DOC_DATE_GRE.Text, Headerfont6, new SolidBrush(Color.Black), 652, 135);
                e.Graphics.DrawString("C A S H/C R E D I T", Headerfont8, new SolidBrush(Color.Black), 325, 135);
                e.Graphics.DrawString("CST No:" + cst, Headerfont2, new SolidBrush(Color.Black), 688, 20);
                e.Graphics.DrawString("Ph No :" + phone_no, Headerfont2, new SolidBrush(Color.Black), startx - 26, 200);

            }
            catch (Exception ex)
            {

            }

            double pricWtax = 0;
            decimal a = 0;
            StringFormat format = new StringFormat(StringFormatFlags.DirectionRightToLeft);
            using (var sf = new StringFormat())
            {

                height += 15;
                sf.LineAlignment = StringAlignment.Center;
                sf.Alignment = StringAlignment.Center;
                xpos = e.MarginBounds.Left + (e.MarginBounds.Width / 2);
                offset = offset + 20;
                ///// e.Graphics.DrawString(Address1 + "," + Address2, Headerfont2, new SolidBrush(Color.Black), headerstartposition, starty + 20 + offset);
                offset = offset + 12;
                /////e.Graphics.DrawString("Phone:".PadRight(3) + Phone, Headerfont2, new SolidBrush(Color.Black), headerstartposition, starty + 20 + offset);
                offset = offset + 12;
                // e.Graphics.DrawString("Email:".PadRight(3) + Email, Headerfont2, new SolidBrush(Color.Black), headerstartposition, starty + 20 + offset);
                offset = offset + 12;
                // e.Graphics.DrawString("Website:".PadRight(3) + Website, Headerfont2, new SolidBrush(Color.Black), headerstartposition, starty + 20 + offset);
                offset = offset + 21;
                offset = offset + 11;
                //////////e.Graphics.DrawString("        THE KERALA VALUE ADDED TAX RULES,2005                                              ,Tax Invoice                     [See rule 58(10),(Cash/Credit)]".PadLeft(10), Headerfont2, new SolidBrush(Color.Black), 20, starty + offset + 9);
                //////////e.Graphics.DrawString("Form No.8B".PadLeft(10), printbold, new SolidBrush(Color.Black), 370, starty + offset + 9);
                offset = offset + 16;
                offset = 15;
                ///////////e.Graphics.DrawString("Tin No       :" + TineNo, Headerfont2, new SolidBrush(Color.Black), 550, starty);
                e.Graphics.DrawString("TIN No:" + TineNo, Headerfont2, new SolidBrush(Color.Black), 25, 20);
                //  e.Graphics.DrawString("CST No:" + cst, Headerfont2, new SolidBrush(Color.Black), 25, 35);
                // e.Graphics.DrawString("CST No      : ", Headerfont2, new SolidBrush(Color.Black), 550, starty + offset);
                //offset = offset + 16;
                offset = offset + 16;
                ///////////e.Graphics.DrawString("Invoice No: ", Headerfont2, new SolidBrush(Color.Black), 550, starty + offset);
                e.Graphics.DrawString("Invoice No" + "   :   " + VOUCHNUM.Text, Headerfont6, new SolidBrush(Color.Black), startx - 26, 135);
                ///////e.Graphics.DrawString(Billno, printbold, new SolidBrush(Color.Black), 620, starty + offset);
                offset = offset + 16;



                offset = offset + 16;
                //  e.Graphics.DrawString("Del.Note No & Date :", Headerfont2, new SolidBrush(Color.Black), 580, starty + offset);
                offset = offset + 16;
                //   e.Graphics.DrawString("Des.Docu.No & Date:", Headerfont2, new SolidBrush(Color.Black), 580, starty + offset);
                offset = offset + 16;

                offset = offset + 16;
                offset = offset + 16;
                offset = offset + 16;
                offset = offset + 16;
                Pen blackPen = new Pen(Color.Black, 1);
                Point point1 = new Point(45, 190);
                Point point2 = new Point(760, 190);
                // e.Graphics.DrawLine(blackPen, 10, 146, 10, 1033);
                //e.Graphics.DrawLine(blackPen, 820, 146, 820, 1033);
                //e.Graphics.DrawLine(blackPen, 45, 240, 45, 906);
                //e.Graphics.DrawLine(blackPen, 226, 240, 226, 906);
                //e.Graphics.DrawLine(blackPen, 260, 240, 260, 906);
                //e.Graphics.DrawLine(blackPen, 335, 240, 335, 906);
                //e.Graphics.DrawLine(blackPen, 395, 240, 395, 906);
                //e.Graphics.DrawLine(blackPen, 470, 240, 470, 906);
                //e.Graphics.DrawLine(blackPen, 530, 240, 530, 906);
                //e.Graphics.DrawLine(blackPen, 580, 240, 580, 906);
                //e.Graphics.DrawLine(blackPen, 630, 240, 630, 906);
                //e.Graphics.DrawLine(blackPen, 680, 240, 680, 906);
                //e.Graphics.DrawLine(blackPen, 740, 240, 740, 906);

                e.Graphics.DrawString("M/S:" + CUSTOMER_NAME.Text, Headerfont5, new SolidBrush(Color.Black), startx - 26, starty + offset - 20);
                e.Graphics.DrawString(ADDRESS_A, Headerfont5, new SolidBrush(Color.Black), 24, starty + offset);
                e.Graphics.DrawString("TIN:" + tin_no, Headerfont6, new SolidBrush(Color.Black), 550, 190);
                Font itemhead = new Font("Times New Roman", 8);
                offset = offset + 2;

                Point point3 = new Point(45, 219);
                Point point4 = new Point(760, 219);

                /// e.Graphics.DrawRectangle(blackPen, 45, 920, 715, 20);
                //e.Graphics.DrawRectangle(blackPen, 45, 920, 715, 27);
                e.Graphics.DrawLine(blackPen, 10, 240, 800, 240); //(45,190,760,190)
                e.Graphics.DrawLine(blackPen, 10, 260, 800, 260);//(45,219,760,219)
                // e.Graphics.DrawLine(blackPen, 10, 865, 820, 865);
                ///////// e.Graphics.DrawRectangle(blackPen, 45, 920, 715, 27); org
                offset = offset + 50;
                // string headtext = "Sl.No".PadRight(25) + "Particulars".PadRight(55) + "Tax%".PadRight(7) + "Qty".PadRight(6) + "Free".PadRight(7) + "MRP".PadRight(9) + "Rate".PadRight(10) + "Gross".PadRight(15) + "Disc".PadRight(9) + "Txble Val".PadRight(15) + "Tax".PadRight(12) + "Total";
                string headtext = "No".PadRight(18) + "Particulars".PadRight(118) + "Quantity".PadRight(15) + "Unit".PadRight(15) + "Tax".PadRight(15) + "Rate".PadRight(15) + "Amount";
                //e.Graphics.DrawString(headtext, printFontBold, new SolidBrush(Color.Black), startx - 4, starty + offset - 1);
                e.Graphics.DrawString(headtext, printFontBold, new SolidBrush(Color.Black), startx - 40, starty + offset + 10);

                offset = offset + 40;
                Font font = new Font("Times New Roman", 10);
                float fontheight = font.GetHeight();

                try
                {

                    int i = 0;
                    int printpoint = 70; //32
                    int j = 1;
                    string mrp = "";
                    int nooflines = 0;
                    //int INCRIMENTHEIGHT = starty + offset;
                    double pq;
                    string total = "";


                    foreach (DataGridViewRow row in dgItems.Rows)
                    {
                        PRINTTOTALPAGE = false;
                        if (j > printeditems)
                        {
                            if (nooflines < 13)
                            {
                                m = m + 1;

                                string period, periodtype, tax;
                                i = i + 1;
                                int ORGLENGTH = row.Cells["cName"].Value.ToString().Length; /////32

                                string name = row.Cells["cName"].Value.ToString().Length <= 70 ? row.Cells[1].Value.ToString() : row.Cells[1].Value.ToString().Substring(0, 70);
                                string name2 = "";
                                int BALANCELENGH = ORGLENGTH - 70;
                                if (row.Cells["cTaxPer"].Value == null)
                                {
                                    tax = "0";
                                }
                                else
                                {
                                    tax = row.Cells["cTaxPer"].Value.ToString();
                                }
                                grossamount = Convert.ToDouble(row.Cells["cGTotal"].Value);
                                grossamounttotal = grossamounttotal + Convert.ToDouble(row.Cells["cGTotal"].Value);
                                netamount = Convert.ToDouble(row.Cells["cNetValue"].Value);
                                netamounttotal = netamounttotal + Convert.ToDouble(row.Cells["cNetValue"].Value);
                                //if (tax == "")
                                //tax = "1";
                                qty = row.Cells["cQty"].Value.ToString();
                                UOM = row.Cells["cUnit"].Value.ToString();

                                rate = row.Cells["cPrice"].Value.ToString();
                                try
                                {
                                    mrp = row.Cells["Mrp"].Value.ToString();
                                }
                                catch
                                {

                                    mrp = "0";
                                }
                                try
                                {
                                    free = row.Cells["Cfree"].Value.ToString();
                                }
                                catch
                                {
                                    free = "0";
                                }
                                disc = row.Cells["cDisc"].Value.ToString();
                                tcdis = tcdis + Convert.ToDecimal(disc);
                                grsstot = Convert.ToDouble(TOTAL_AMOUNT.Text);
                                discp = ((Convert.ToDouble(tcdis) * 100) / Convert.ToDouble(grossamounttotal)).ToString("N2");
                                gross = (Convert.ToDouble(qty) * Convert.ToDouble(rate)).ToString("N2");

                                price = Convert.ToDouble(row.Cells[12].Value).ToString();
                                //    string Serial = row.Cells["SerialNos"].Value.ToString();
                                taxval = Convert.ToDouble(row.Cells["cTaxAmt"].Value);

                                txbval = "";
                                if (taxval != 0)
                                {
                                    txbval = (Convert.ToDouble(qty) * Convert.ToDouble(rate) - Convert.ToDouble(disc)).ToString("N2");
                                    ttaxbl = ttaxbl + Convert.ToDecimal(txbval);
                                }
                                else
                                {
                                    txbval = (Convert.ToDouble(qty) * Convert.ToDouble(rate) - Convert.ToDouble(disc)).ToString("N2");

                                }

                                pricWtax = Convert.ToDouble(rate) + (Convert.ToDouble(rate) * Convert.ToDouble(qty)) * (Convert.ToDouble(tax) / 100 / Convert.ToDouble(qty));
                                // Math.Round(pricWtax, 2);
                                try
                                {
                                    tfree = tfree + Convert.ToInt32(free);
                                }
                                catch
                                {

                                }

                                tqty = tqty + Convert.ToInt32(qty);
                                tgrossrate = tgrossrate + Convert.ToDecimal(gross);
                                ttaxva = ttaxva + Convert.ToDecimal(taxval);
                                ///////trate = trate + Convert.ToDecimal(rate);
                                trate = trate + Convert.ToDecimal(rate);
                                a = a + Convert.ToDecimal(pricWtax);
                                // ttotal = ttotal + Convert.ToDecimal(taxval);

                                string productline = name + tax + qty + rate + price;
                                // unt = row.Cells["Unit"].Value.ToString();
                                e.Graphics.DrawString(m.ToString(), font, new SolidBrush(Color.Black), 10, starty + offset);
                                // offset = offset + (int)fontheight + 10;
                                //
                                e.Graphics.DrawString(name, Headerfont2, new SolidBrush(Color.Black), startx + 5, starty + offset);

                                //////////e.Graphics.DrawString(qty, font, new SolidBrush(Color.Black), startx + 440, starty + offset);
                                //e.Graphics.DrawString("1" + UOM, font, new SolidBrush(Color.Black), startx + 175, starty + offset);
                                //e.Graphics.DrawString(MFR, font, new SolidBrush(Color.Black), startx + 214, starty + offset);
                                //e.Graphics.DrawString(batch, font, new SolidBrush(Color.Black), startx + 295, starty + offset);
                                //e.Graphics.DrawString(exp, font, new SolidBrush(Color.Black), startx + 365, starty + offset);
                                e.Graphics.DrawString(qty, font, new SolidBrush(Color.Black), startx + 455, starty + offset);
                                e.Graphics.DrawString(UOM, font, new SolidBrush(Color.Black), startx + 510, starty + offset);
                                //e.Graphics.DrawString(free, font, new SolidBrush(Color.Black), startx + 500, starty + offset);
                                e.Graphics.DrawString(rate, font, new SolidBrush(Color.Black), startx + 655, starty + offset, format);
                                e.Graphics.DrawString(tax, font, new SolidBrush(Color.Black), startx + 590, starty + offset, format);
                                //e.Graphics.DrawString(mrp, font, new SolidBrush(Color.Black), startx + 650, starty + offset);

                                //e.Graphics.DrawString(gross, font, new SolidBrush(Color.Black), startx + 430, starty + offset);
                                //e.Graphics.DrawString(disc, font, new SolidBrush(Color.Black), startx + 490, starty + offset);

                                if (taxval != 0)
                                {
                                    // e.Graphics.DrawString(txbval, font, new SolidBrush(Color.Black), startx + 535, starty + offset);
                                }
                                else
                                {
                                    //  e.Graphics.DrawString("0", font, new SolidBrush(Color.Black), startx + 535, starty + offset);
                                }


                                total = row.Cells["cTotal"].Value.ToString();
                                e.Graphics.DrawString(total, font, new SolidBrush(Color.Black), startx + 735, starty + offset, format);////total individual amount


                                //offset = offset;
                                offset = offset + (int)fontheight + 10;
                                if (row.Cells["description"].Value != "")
                                {
                                    try
                                    {
                                        e.Graphics.DrawString(row.Cells["description"].Value.ToString(), font, new SolidBrush(Color.Black), startx + 30, starty + offset);
                                        offset = offset + (int)fontheight + 35;
                                    }
                                    catch
                                    {


                                    }


                                }


                                while (BALANCELENGH > 1)
                                {
                                    name2 = BALANCELENGH <= 70 ? row.Cells["cName"].Value.ToString().Substring(printpoint, BALANCELENGH) : row.Cells["cName"].Value.ToString().Substring(printpoint, 70);
                                    e.Graphics.DrawString(name2, Headerfont2, new SolidBrush(Color.Black), startx + 30, starty + offset - 9);
                                    BALANCELENGH = BALANCELENGH - 70;
                                    printpoint = printpoint + 70;
                                    starty = starty + (int)fontheight;
                                }
                                printpoint = 70;

                                //starty = starty + (int)fontheight - 5;
                                nooflines++;
                                j++;

                            }
                            else
                            {

                                printeditems = j - 1;
                                //  e.HasMorePages = true;
                                hasmorepages = true;
                                //m = m - 1;
                                PRINTTOTALPAGE = true;

                            }
                            if (hasmorepages == true)
                            {
                                //m = m + 1;
                                e.Graphics.DrawString("coutinue...", printFontBold, new SolidBrush(Color.Black), startx + 40, 940);
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
            //e.Graphics.DrawString(tfree.ToString(), printFontBold, new SolidBrush(Color.Black), startx + 305, 901);

            //e.Graphics.DrawString(trate.ToString("n2"), printFontBold, new SolidBrush(Color.Black), startx + 537, 875);

            float newoffset = 900;


            if (!PRINTTOTALPAGE)
            {
                PAGETOTAL = true;
                if (PAGETOTAL)
                {
                    try
                    {
                        e.Graphics.DrawLine(blackPen1, 10, 865, 800, 865);
                        e.Graphics.DrawLine(blackPen1, 10, 887, 800, 887);
                        // e.Graphics.DrawString("Sub Total", printFontBold, new SolidBrush(Color.Black), 560, 910);
                        e.Graphics.DrawString("Gross Amount", printFontBold, new SolidBrush(Color.Black), 560, 888);
                        e.Graphics.DrawString("Discount (%)", Headerfont2, new SolidBrush(Color.Black), 560, 904);
                        e.Graphics.DrawString("Trd.Discount (Amt)", Headerfont2, new SolidBrush(Color.Black), 560, 919);
                        e.Graphics.DrawString("Total", Headerfont2, new SolidBrush(Color.Black), 560, 934);
                        e.Graphics.DrawString("VAT", Headerfont2, new SolidBrush(Color.Black), 560, 950);
                        e.Graphics.DrawString("Cess", Headerfont2, new SolidBrush(Color.Black), 560, 966);
                        e.Graphics.DrawString("Adl Discount", Headerfont2, new SolidBrush(Color.Black), 560, 982);
                        e.Graphics.DrawString("Handling/Other", Headerfont2, new SolidBrush(Color.Black), 560, 998);
                        e.Graphics.DrawString("G.Total", Headerfont6, new SolidBrush(Color.Black), 560, 1014);
                        // e.Graphics.DrawLine(blackPen1, 560, 1013, 820, 1013);

                        e.Graphics.DrawString(tqty.ToString(), printFontBold, new SolidBrush(Color.Black), startx + 280, 870);
                        e.Graphics.DrawString(tcdis.ToString(), Headerfont2, new SolidBrush(Color.Black), 788, 919, format);
                        e.Graphics.DrawString(ttaxva.ToString(), Headerfont2, new SolidBrush(Color.Black), 788, 950, format);
                        e.Graphics.DrawString(netamounttotal.ToString("N2"), Headerfont2, new SolidBrush(Color.Black), 788, 934, format);
                        e.Graphics.DrawString(txtFreight.Text, Headerfont2, new SolidBrush(Color.Black), 788, 998, format);
                        e.Graphics.DrawString(TXT_CESS.Text, Headerfont2, new SolidBrush(Color.Black), 788, 966, format);
                        e.Graphics.DrawString(DISCOUNT.Text, Headerfont2, new SolidBrush(Color.Black), 788, 982, format);
                        e.Graphics.DrawString(discp.ToString(), Headerfont2, new SolidBrush(Color.Black), 788, 904, format);



                        string t1 = "Certified that particulars shown in the above tax invoice are true and correct and that my/our registration under KVAT Act";
                        string t2 = "2003nis valid as on the date of the bill";
                        e.Graphics.DrawString("DECLARATION", Headerfont6, new SolidBrush(Color.Black), 25, 1085);
                        e.Graphics.DrawString(t1, Headerfont2, new SolidBrush(Color.Black), 25, 1100);
                        e.Graphics.DrawString(t2, Headerfont2, new SolidBrush(Color.Black), 25, 1115);
                        //e.Graphics.DrawString("Packed By", Headerfont6, new SolidBrush(Color.Black), 25, 1150);
                        //e.Graphics.DrawString("Checked By", Headerfont6, new SolidBrush(Color.Black), 400, 1150);
                        e.Graphics.DrawString("Authorised Signatory", printFontBold, new SolidBrush(Color.Black), 680, 1150);
                        //e.Graphics.DrawLine(blackPen1, 10, 1030, 820, 1030); //940
                        e.Graphics.DrawLine(blackPen1, 10, 1033, 800, 1033); //
                        e.Graphics.DrawString("Terms & Condition", printFontBold, new SolidBrush(Color.Black), 25, 1050);
                        ///// e.Graphics.DrawString(Spell.SpellAmount.comma(Convert.ToDecimal(NET_AMOUNT.Text)), printFontBold1, new SolidBrush(Color.Black), startx + 625, starty + newoffset-20 );
                        string neta = Convert.ToDecimal(NET_AMOUNT.Text).ToString("n2");

                        ////// e.Graphics.DrawString(Spell.SpellAmount.comma(Convert.ToDecimal(neta)), printFontBold1, new SolidBrush(Color.Black), startx + 625, 925);
                        e.Graphics.DrawString(neta, printFontBold, new SolidBrush(Color.Black), 788, 1014, format);
                        //e.Graphics.DrawString(netamounttotal.ToString(), printFontBold, new SolidBrush(Color.Black), 750, 1013);
                        //  e.Graphics.DrawString(TOTAL_AMOUNT.Text, printFontBold, new SolidBrush(Color.Black), 750, 910);
                        e.Graphics.DrawString(grossamounttotal.ToString("N2"), printFontBold, new SolidBrush(Color.Black), 788, 888, format); //Gross Amount
                        try
                        {
                            int cash = (int)Convert.ToDouble(NET_AMOUNT.Text);
                            string cas = NET_AMOUNT.Text;
                            // Math.Floor(cash);
                            //string test = Spell.SpellAmount.InWrods(cash);
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
                                test3 = test + " Rupees only";
                            }
                            e.Graphics.DrawString("Crd/Trk No :", Headerfont6, new SolidBrush(Color.Black), 25, 985);
                            e.Graphics.DrawString("R.Off :" + txtRoundOff.Text, Headerfont6, new SolidBrush(Color.Black), 25, 999);
                            e.Graphics.DrawString(test3, Headerfont6, new SolidBrush(Color.Black), 25, 1014);


                        }
                        catch
                        {
                        }


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

            // newoffset = newoffset + 25;
            newoffset = newoffset;


            e.HasMorePages = hasmorepages;
            */
            #endregion
            DataTable table = Common.getCustomer(CUSTOMER_CODE.Text);
            DataRow row = table.Rows[0];
           // printGSTPage(e, "", "", DOC_DATE_GRE.Value.ToString("dd/MM/yyyy"), "", CUSTOMER_NAME.Text, Convert.ToString(row["ADDRESS_A"]), Convert.ToString(row["TIN_NO"]), cmbState.Text, Convert.ToString(cmbState.SelectedValue), "", "");
          //  printGSTPage_ForMalaysia(e, "", "", DOC_DATE_GRE.Value.ToString("dd/MM/yyyy"), "", CUSTOMER_NAME.Text, Convert.ToString(row["ADDRESS_A"]), Convert.ToString(row["TIN_NO"]), cmbState.Text, Convert.ToString(cmbState.SelectedValue), "", "");
            printVATF8(e);
        }
        
        void Estimate_PrintPage(object sender, PrintPageEventArgs e)
        {
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
            Font Headerfont1 = new Font("Calibri", 15, FontStyle.Bold);
            Font dec = new Font("Calibri", 8, FontStyle.Regular);
            Font Headerfont2 = new Font("Times New Roman", 8, FontStyle.Regular);
            Font Headerfont4 = new Font("Times New Roman", 15, FontStyle.Bold);
            Font Headerfont5 = new Font("Times New Roman", 10, FontStyle.Regular);
            Font printFont = new Font("Calibri", 10);
            Font printFontBold = new Font("Calibri", 10, FontStyle.Bold);
            Font printFontBold1 = new Font("Calibri", 14, FontStyle.Bold);
            Font printFontnet = new Font("Calibri", 11, FontStyle.Bold);
            Font printbold = new Font("Calibri", 10, FontStyle.Bold);
            string address1 = "";
            string address2 = "";
            string unit = "";
            string qty = "";
            string rate = "";
            string gross = "";
            string price = "";
            double taxval = 0;
            string txbval = "";
            string free = "0";
            var tabDataForeColor = Color.Black;
            int height = 100 + y;
            Pen blackPen1 = new Pen(Color.Black, 1);
            var txtDataWidth = e.Graphics.MeasureString(CompanyName, Headerfont1).Width;

            try
            {

                e.Graphics.DrawString("ESTIMATE", Headerfont4, new SolidBrush(Color.Black), 370, starty + offset + 38);

                e.Graphics.DrawLine(blackPen1, 45, 100, 800, 100); //940

            }
            catch (Exception ex)
            {

            }

            double pricWtax = 0;
            decimal a = 0;
            StringFormat format = new StringFormat(StringFormatFlags.DirectionRightToLeft);
            using (var sf = new StringFormat())
            {

                height += 15;
                sf.LineAlignment = StringAlignment.Center;
                sf.Alignment = StringAlignment.Center;
                xpos = e.MarginBounds.Left + (e.MarginBounds.Width / 2);
                offset = offset + 20;
                offset = offset + 12;
                offset = offset + 12;
                offset = offset + 12;
                offset = offset + 21;
                offset = offset + 11;
                offset = offset + 16;
                offset = 15;

                offset = offset + 16;
                e.Graphics.DrawString("Bill No" + "  :  " + VOUCHNUM.Text, printbold, new SolidBrush(Color.Black), 650, starty + offset + 60);
                offset = offset + 16;
                e.Graphics.DrawString("Date" + "     :  " + DOC_DATE_GRE.Text, printbold, new SolidBrush(Color.Black), 650, starty + offset + 70);

                e.Graphics.DrawString("Ph:" + phone_no, Headerfont2, new SolidBrush(Color.Black), startx, starty + offset + 145);
                // e.Graphics.DrawString(DOC_DATE_GRE.Text, Headerfont2, new SolidBrush(Color.Black), 560, starty + offset + 65);
                offset = offset + 16;
                offset = offset + 16;
                offset = offset + 16;
                offset = offset + 16;
                offset = offset + 16;
                offset = offset + 16;
                offset = offset + 16;
                Pen blackPen = new Pen(Color.Black, 1);
                Point point1 = new Point(45, 190);
                Point point2 = new Point(760, 190);
                e.Graphics.DrawLine(blackPen, 45, 230, 800, 230); //(45,190,760,190)
                e.Graphics.DrawLine(blackPen, 45, 260, 800, 260); //(45,190,760,190)
                e.Graphics.DrawString("To:" + CUSTOMER_NAME.Text, Headerfont5, new SolidBrush(Color.Black), startx, starty + offset - 70);
                e.Graphics.DrawString(ADDRESS_A, Headerfont5, new SolidBrush(Color.Black), startx, starty + offset - 40);
                Font itemhead = new Font("Times New Roman", 8);
                offset = offset + 2;
                Point point3 = new Point(45, 219);
                Point point4 = new Point(760, 219);

                e.Graphics.DrawLine(blackPen, 45, 1120, 800, 1120);

                offset = offset + 50;
                string headtext = "No".PadRight(30) + "Particulars".PadRight(70) + "Quantity".PadRight(20) + "Unit".PadRight(30) + "Rate".PadRight(30) + "Amount";
                e.Graphics.DrawString(headtext, printFontBold, new SolidBrush(Color.Black), startx - 4, starty + offset + 3);
                offset = offset + 40;
                Font font = new Font("Times New Roman", 10);
                float fontheight = font.GetHeight();
                try
                {
                    int i = 0;
                    int printpoint = 40; //32
                    int j = 1;
                    string mrp = "";
                    int nooflines = 0;
                    //int INCRIMENTHEIGHT = starty + offset;
                    double pq;
                    string total = "";
                    foreach (DataGridViewRow row in dgItems.Rows)
                    {
                        PRINTTOTALPAGE = false;
                        if (j > printeditems)
                        {
                            if (nooflines < 18)
                            {
                                m = m + 1;
                                string tax;
                                i = i + 1;
                                int ORGLENGTH = row.Cells["cName"].Value.ToString().Length;

                                string name = row.Cells["cName"].Value.ToString().Length <= 40 ? row.Cells[1].Value.ToString() : row.Cells[1].Value.ToString().Substring(0, 40);
                                string name2 = "";
                                int BALANCELENGH = ORGLENGTH - 40;
                               // tax = row.Cells["cTaxPer"].Value.ToString();
                                //if (tax == "")
                                   // tax = "1";
                                qty = row.Cells["cQty"].Value.ToString();
                                rate = row.Cells["cPrice"].Value.ToString();
                                try
                                {
                                    mrp = row.Cells["cMrp"].Value.ToString();
                                }
                                catch
                                {
                                    mrp = "0";
                                }
                                try
                                {
                                    free = row.Cells["Cfree"].Value.ToString();
                                }
                                catch
                                {
                                    free = "0";
                                }
                                unit = row.Cells["cUnit"].Value.ToString();
                                //  tcdis = tcdis + Convert.ToDecimal(unit);
                                gross = (Convert.ToDouble(qty) * Convert.ToDouble(rate)).ToString("N2");
                                price = Convert.ToDouble(row.Cells[12].Value).ToString();
                                taxval = Convert.ToDouble(row.Cells["cTaxAmt"].Value);
                                txbval = "";
                                tqty = tqty + Convert.ToInt32(qty);
                                tgrossrate = tgrossrate + Convert.ToDecimal(gross);
                                ttaxva = ttaxva + Convert.ToDecimal(taxval);
                                trate = trate + Convert.ToDecimal(rate);
                                a = a + Convert.ToDecimal(pricWtax);
                                //string productline = name + tax + qty + rate + price;
                                e.Graphics.DrawString(m.ToString(), font, new SolidBrush(Color.Black), startx, starty + offset);
                                e.Graphics.DrawString(name, Headerfont2, new SolidBrush(Color.Black), startx + 30, starty + offset);
                                e.Graphics.DrawString(qty, font, new SolidBrush(Color.Black), startx + 360, starty + offset);
                                e.Graphics.DrawString(unit, font, new SolidBrush(Color.Black), startx + 440, starty + offset);
                                e.Graphics.DrawString(rate, font, new SolidBrush(Color.Black), startx + 585, starty + offset, format);
                                total = row.Cells["cNetValue"].Value.ToString();
                                e.Graphics.DrawString(total, font, new SolidBrush(Color.Black), startx + 700, starty + offset, format);
                                offset = offset + (int)fontheight + 10;
                                while (BALANCELENGH > 1)
                                {
                                    name2 = BALANCELENGH <= 40 ? row.Cells["cName"].Value.ToString().Substring(printpoint, BALANCELENGH) : row.Cells["cName"].Value.ToString().Substring(printpoint, 40);
                                    e.Graphics.DrawString(name2, Headerfont2, new SolidBrush(Color.Black), startx + 30, starty + offset - 9);
                                    BALANCELENGH = BALANCELENGH - 40;
                                    printpoint = printpoint + 40;
                                    starty = starty + (int)fontheight;
                                }
                                printpoint = 40;
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
                                e.Graphics.DrawString("coutinue...", printFontBold, new SolidBrush(Color.Black), startx + 40, 1020);
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
            e.Graphics.DrawString("E & OE", Headerfont2, new SolidBrush(Color.Black), startx, 1090);
            float newoffset = 900;
            if (!PRINTTOTALPAGE)
            {
                PAGETOTAL = true;
                if (PAGETOTAL)
                {
                    try
                    {
                        Pen blackPen2 = new Pen(Color.Black, 1);
                        e.Graphics.DrawLine(blackPen2, 45, 900, 800, 900);
                        e.Graphics.DrawLine(blackPen2, 45, 1030, 800, 1030);
                        e.Graphics.DrawString("Total" + ":", printFontBold, new SolidBrush(Color.Black), startx + 500, 910);
                        e.Graphics.DrawString(TOTAL_AMOUNT.Text, printFontBold, new SolidBrush(Color.Black), startx + 700, 910, format);
                        e.Graphics.DrawString("Disc " + ":", printFontBold, new SolidBrush(Color.Black), startx + 500, 940);
                        e.Graphics.DrawString(DISCOUNT.Text, printFontBold, new SolidBrush(Color.Black), startx + 700, 940, format);
                        //  e.Graphics.DrawString("Freight: ", Headerfont2, new SolidBrush(Color.Black), startx + 60, 921);
                        e.Graphics.DrawString("R.Off " + ":", printFontBold, new SolidBrush(Color.Black), startx + 500, 970);
                        e.Graphics.DrawString(txtRoundOff.Text, printFontBold, new SolidBrush(Color.Black), startx + 700, 970, format);
                        e.Graphics.DrawString("Grand Total" + ":", printFontBold, new SolidBrush(Color.Black), startx + 500, 1000);
                        e.Graphics.DrawString(NET_AMOUNT.Text, printFontBold1, new SolidBrush(Color.Black), startx + 700, 1000, format);
                        //e.Graphics.DrawString(tcdis.ToString("n2"), printFontBold, new SolidBrush(Color.Black), startx + 490, 901);
                        string neta = Convert.ToDecimal(NET_AMOUNT.Text).ToString("n2");

                        try
                        {
                            int cash = (int)Convert.ToDouble(NET_AMOUNT.Text);
                            string cas = NET_AMOUNT.Text;
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
                                int index = test3.IndexOf("Rupees");
                            }
                            if (i1 > 0 && i2 == 0)
                            {
                                string test = NumbersToWords(i1);
                                test3 = test + " Rupees only";
                            }
                            e.Graphics.DrawString("Rupees in words :" + " " + test3, printFontBold, new SolidBrush(Color.Black), startx, 1060);


                        }
                        catch
                        {
                        }
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
            newoffset = newoffset + 25;
            e.HasMorePages = hasmorepages;
        }
        
        void printDocumentA8B_PrintPage(object sender, PrintPageEventArgs e)
        {
            #region delete this after gst success.
            /*
            m = 0;
            bool hasmorepages = false;
            float xpos;
            int startx = 50;
            int starty = 20;
            int offset = 15;
            int headerstartposition = 50;
            bool PRINTTOTALPAGE = true;
            int w = e.MarginBounds.Width / 2;
            int x = e.MarginBounds.Left;
            int y = e.MarginBounds.Top;

            Font Headerfont1 = new Font("Calibri", 15, FontStyle.Bold);
            Font dec = new Font("Calibri", 8, FontStyle.Regular);
            Font Headerfont2 = new Font("Times New Roman", 8, FontStyle.Regular);
            Font Headerfont3 = new Font("Times New Roman", 12, FontStyle.Bold);
            Font Headerfont5 = new Font("Times New Roman", 10, FontStyle.Regular);
            Font Headerfont6 = new Font("Times New Roman", 10, FontStyle.Bold);
            Font Headerfont7 = new Font("Times New Roman", 13, FontStyle.Regular);
            Font Headerfont8 = new Font("Times New Roman", 11, FontStyle.Bold);
            Font printFont = new Font("Calibri", 10);
            Font printFontBold = new Font("Calibri", 10, FontStyle.Bold);
            Font printFontBold1 = new Font("Calibri", 14, FontStyle.Bold);
            Font printFontnet = new Font("Calibri", 11, FontStyle.Bold);
            Font printbold = new Font("Calibri", 10, FontStyle.Bold);
            string address1 = "";
            string address2 = "";
            string address3 = "";
            string disc = "";
            string qty = "";
            string rate = "";
            string gross = "";
            double grossamount = 0;
            double grossamounttotal = 0;
            double netamount = 0;
            double netamounttotal = 0;
            string price = "";
            double taxval = 0;
            string discp = "";
            double grsstot = 0;
            string txbval = "";
            string free = "0";
            string UOM = "";
            string unt = "";
            var tabDataForeColor = Color.Black;
            int height = 100 + y;

            Pen blackPen1 = new Pen(Color.Black, 1);
            var txtDataWidth = e.Graphics.MeasureString(CompanyName, Headerfont1).Width;

            try
            {

                address1 = "CLASSIC TRADE LINKS";
                address2 = "Door No.6/575-D, Nallalam Road, Areekkad, Nallalam P O";
                string mob1 = "7561020007";
                e.Graphics.DrawString(address1, Headerfont3, new SolidBrush(Color.Black), 320, 40);
                e.Graphics.DrawString(address2, Headerfont3, new SolidBrush(Color.Black), 220, 60);
                e.Graphics.DrawString("Ph : " + mob1, Headerfont2, new SolidBrush(Color.Black), 25, 35);
                e.Graphics.DrawString("Fax : ", Headerfont2, new SolidBrush(Color.Black), 25, 50);
                e.Graphics.DrawString("THE KERALA VALUE ADDED TAX RULES 2005", Headerfont2, new SolidBrush(Color.Black), 550, 90);
                e.Graphics.DrawString("FORM NO.8B", Headerfont6, new SolidBrush(Color.Black), 701, 105);
                e.Graphics.DrawString("[See rule 58(10)]", Headerfont2, new SolidBrush(Color.Black), 605, 120);
                e.Graphics.DrawString("TAX INVOICE", Headerfont6, new SolidBrush(Color.Black), 694, 120);
                e.Graphics.DrawString("Date" + "          : " + DOC_DATE_GRE.Text, Headerfont6, new SolidBrush(Color.Black), 652, 135);
                e.Graphics.DrawString("C A S H/C R E D I T", Headerfont8, new SolidBrush(Color.Black), 325, 135);
                e.Graphics.DrawString("CST No:" + cst, Headerfont2, new SolidBrush(Color.Black), 688, 20);
                e.Graphics.DrawString("Ph No :" + phone_no, Headerfont2, new SolidBrush(Color.Black), startx - 26, 200);

            }
            catch (Exception ex)
            {

            }

            double pricWtax = 0;
            decimal a = 0;
            StringFormat format = new StringFormat(StringFormatFlags.DirectionRightToLeft);
            using (var sf = new StringFormat())
            {

                height += 15;
                sf.LineAlignment = StringAlignment.Center;
                sf.Alignment = StringAlignment.Center;
                xpos = e.MarginBounds.Left + (e.MarginBounds.Width / 2);
                offset = offset + 20;
                ///// e.Graphics.DrawString(Address1 + "," + Address2, Headerfont2, new SolidBrush(Color.Black), headerstartposition, starty + 20 + offset);
                offset = offset + 12;
                /////e.Graphics.DrawString("Phone:".PadRight(3) + Phone, Headerfont2, new SolidBrush(Color.Black), headerstartposition, starty + 20 + offset);
                offset = offset + 12;
                // e.Graphics.DrawString("Email:".PadRight(3) + Email, Headerfont2, new SolidBrush(Color.Black), headerstartposition, starty + 20 + offset);
                offset = offset + 12;
                // e.Graphics.DrawString("Website:".PadRight(3) + Website, Headerfont2, new SolidBrush(Color.Black), headerstartposition, starty + 20 + offset);
                offset = offset + 21;
                offset = offset + 11;
                //////////e.Graphics.DrawString("        THE KERALA VALUE ADDED TAX RULES,2005                                              ,Tax Invoice                     [See rule 58(10),(Cash/Credit)]".PadLeft(10), Headerfont2, new SolidBrush(Color.Black), 20, starty + offset + 9);
                //////////e.Graphics.DrawString("Form No.8B".PadLeft(10), printbold, new SolidBrush(Color.Black), 370, starty + offset + 9);
                offset = offset + 16;
                offset = 15;
                ///////////e.Graphics.DrawString("Tin No       :" + TineNo, Headerfont2, new SolidBrush(Color.Black), 550, starty);
                e.Graphics.DrawString("TIN No:" + TineNo, Headerfont2, new SolidBrush(Color.Black), 25, 20);
                //  e.Graphics.DrawString("CST No:" + cst, Headerfont2, new SolidBrush(Color.Black), 25, 35);
                // e.Graphics.DrawString("CST No      : ", Headerfont2, new SolidBrush(Color.Black), 550, starty + offset);
                //offset = offset + 16;
                offset = offset + 16;
                ///////////e.Graphics.DrawString("Invoice No: ", Headerfont2, new SolidBrush(Color.Black), 550, starty + offset);
                e.Graphics.DrawString("Invoice No" + "   :   " + VOUCHNUM.Text, Headerfont6, new SolidBrush(Color.Black), startx - 26, 135);
                ///////e.Graphics.DrawString(Billno, printbold, new SolidBrush(Color.Black), 620, starty + offset);
                offset = offset + 16;



                offset = offset + 16;
                //  e.Graphics.DrawString("Del.Note No & Date :", Headerfont2, new SolidBrush(Color.Black), 580, starty + offset);
                offset = offset + 16;
                //   e.Graphics.DrawString("Des.Docu.No & Date:", Headerfont2, new SolidBrush(Color.Black), 580, starty + offset);
                offset = offset + 16;

                offset = offset + 16;
                offset = offset + 16;
                offset = offset + 16;
                offset = offset + 16;
                Pen blackPen = new Pen(Color.Black, 1);
                Point point1 = new Point(45, 190);
                Point point2 = new Point(760, 190);
                // e.Graphics.DrawLine(blackPen, 10, 146, 10, 1033);
                //e.Graphics.DrawLine(blackPen, 820, 146, 820, 1033);
                //e.Graphics.DrawLine(blackPen, 45, 240, 45, 906);
                //e.Graphics.DrawLine(blackPen, 226, 240, 226, 906);
                //e.Graphics.DrawLine(blackPen, 260, 240, 260, 906);
                //e.Graphics.DrawLine(blackPen, 335, 240, 335, 906);
                //e.Graphics.DrawLine(blackPen, 395, 240, 395, 906);
                //e.Graphics.DrawLine(blackPen, 470, 240, 470, 906);
                //e.Graphics.DrawLine(blackPen, 530, 240, 530, 906);
                //e.Graphics.DrawLine(blackPen, 580, 240, 580, 906);
                //e.Graphics.DrawLine(blackPen, 630, 240, 630, 906);
                //e.Graphics.DrawLine(blackPen, 680, 240, 680, 906);
                //e.Graphics.DrawLine(blackPen, 740, 240, 740, 906);

                e.Graphics.DrawString("M/S:" + CUSTOMER_NAME.Text, Headerfont5, new SolidBrush(Color.Black), startx - 26, starty + offset - 20);
                e.Graphics.DrawString(ADDRESS_A, Headerfont5, new SolidBrush(Color.Black), 24, starty + offset);
                e.Graphics.DrawString("TIN:" + tin_no, Headerfont6, new SolidBrush(Color.Black), 550, 190);
                Font itemhead = new Font("Times New Roman", 8);
                offset = offset + 2;

                Point point3 = new Point(45, 219);
                Point point4 = new Point(760, 219);

                /// e.Graphics.DrawRectangle(blackPen, 45, 920, 715, 20);
                //e.Graphics.DrawRectangle(blackPen, 45, 920, 715, 27);
                e.Graphics.DrawLine(blackPen, 10, 240, 800, 240); //(45,190,760,190)
                e.Graphics.DrawLine(blackPen, 10, 260, 800, 260);//(45,219,760,219)
                // e.Graphics.DrawLine(blackPen, 10, 865, 820, 865);
                ///////// e.Graphics.DrawRectangle(blackPen, 45, 920, 715, 27); org
                offset = offset + 50;
                // string headtext = "Sl.No".PadRight(25) + "Particulars".PadRight(55) + "Tax%".PadRight(7) + "Qty".PadRight(6) + "Free".PadRight(7) + "MRP".PadRight(9) + "Rate".PadRight(10) + "Gross".PadRight(15) + "Disc".PadRight(9) + "Txble Val".PadRight(15) + "Tax".PadRight(12) + "Total";
                string headtext = "No".PadRight(18) + "Particulars".PadRight(118) + "Quantity".PadRight(15) + "Unit".PadRight(15) + "Tax".PadRight(15) + "Rate".PadRight(15) + "Amount";
                //e.Graphics.DrawString(headtext, printFontBold, new SolidBrush(Color.Black), startx - 4, starty + offset - 1);
                e.Graphics.DrawString(headtext, printFontBold, new SolidBrush(Color.Black), startx - 40, starty + offset + 10);

                offset = offset + 40;
                Font font = new Font("Times New Roman", 10);
                float fontheight = font.GetHeight();

                try
                {

                    int i = 0;
                    int printpoint = 70; //32
                    int j = 1;
                    string mrp = "";
                    int nooflines = 0;
                    //int INCRIMENTHEIGHT = starty + offset;
                    double pq;
                    string total = "";


                    foreach (DataGridViewRow row in dgItems.Rows)
                    {
                        PRINTTOTALPAGE = false;
                        if (j > printeditems)
                        {
                            if (nooflines < 13)
                            {
                                m = m + 1;

                                string period, periodtype, tax;
                                i = i + 1;
                                int ORGLENGTH = row.Cells["cName"].Value.ToString().Length; /////32

                                string name = row.Cells["cName"].Value.ToString().Length <= 70 ? row.Cells[1].Value.ToString() : row.Cells[1].Value.ToString().Substring(0, 70);
                                string name2 = "";
                                int BALANCELENGH = ORGLENGTH - 70;
                                if (row.Cells["cTaxPer"].Value == null)
                                {
                                    tax = "0";
                                }
                                else
                                {
                                    tax = row.Cells["cTaxPer"].Value.ToString();
                                }
                                grossamount = Convert.ToDouble(row.Cells["cGTotal"].Value);
                                grossamounttotal = grossamounttotal + Convert.ToDouble(row.Cells["cGTotal"].Value);
                                netamount = Convert.ToDouble(row.Cells["cNetValue"].Value);
                                netamounttotal = netamounttotal + Convert.ToDouble(row.Cells["cNetValue"].Value);
                                //if (tax == "")
                                //tax = "1";
                                qty = row.Cells["cQty"].Value.ToString();
                                UOM = row.Cells["cUnit"].Value.ToString();

                                rate = row.Cells["cPrice"].Value.ToString();
                                try
                                {
                                    mrp = row.Cells["Mrp"].Value.ToString();
                                }
                                catch
                                {

                                    mrp = "0";
                                }
                                try
                                {
                                    free = row.Cells["Cfree"].Value.ToString();
                                }
                                catch
                                {
                                    free = "0";
                                }
                                disc = row.Cells["cDisc"].Value.ToString();
                                tcdis = tcdis + Convert.ToDecimal(disc);
                                grsstot = Convert.ToDouble(TOTAL_AMOUNT.Text);
                                discp = ((Convert.ToDouble(tcdis) * 100) / Convert.ToDouble(grossamounttotal)).ToString("N2");
                                gross = (Convert.ToDouble(qty) * Convert.ToDouble(rate)).ToString("N2");

                                price = Convert.ToDouble(row.Cells[12].Value).ToString();
                                //    string Serial = row.Cells["SerialNos"].Value.ToString();
                                taxval = Convert.ToDouble(row.Cells["cTaxAmt"].Value);

                                txbval = "";
                                if (taxval != 0)
                                {
                                    txbval = (Convert.ToDouble(qty) * Convert.ToDouble(rate) - Convert.ToDouble(disc)).ToString("N2");
                                    ttaxbl = ttaxbl + Convert.ToDecimal(txbval);
                                }
                                else
                                {
                                    txbval = (Convert.ToDouble(qty) * Convert.ToDouble(rate) - Convert.ToDouble(disc)).ToString("N2");

                                }

                                pricWtax = Convert.ToDouble(rate) + (Convert.ToDouble(rate) * Convert.ToDouble(qty)) * (Convert.ToDouble(tax) / 100 / Convert.ToDouble(qty));
                                // Math.Round(pricWtax, 2);
                                try
                                {
                                    tfree = tfree + Convert.ToInt32(free);
                                }
                                catch
                                {

                                }

                                tqty = tqty + Convert.ToInt32(qty);
                                tgrossrate = tgrossrate + Convert.ToDecimal(gross);
                                ttaxva = ttaxva + Convert.ToDecimal(taxval);
                                ///////trate = trate + Convert.ToDecimal(rate);
                                trate = trate + Convert.ToDecimal(rate);
                                a = a + Convert.ToDecimal(pricWtax);
                                // ttotal = ttotal + Convert.ToDecimal(taxval);

                                string productline = name + tax + qty + rate + price;
                                // unt = row.Cells["Unit"].Value.ToString();
                                e.Graphics.DrawString(m.ToString(), font, new SolidBrush(Color.Black), 10, starty + offset);
                                // offset = offset + (int)fontheight + 10;
                                //
                                e.Graphics.DrawString(name, Headerfont2, new SolidBrush(Color.Black), startx + 5, starty + offset);

                                //////////e.Graphics.DrawString(qty, font, new SolidBrush(Color.Black), startx + 440, starty + offset);
                                //e.Graphics.DrawString("1" + UOM, font, new SolidBrush(Color.Black), startx + 175, starty + offset);
                                //e.Graphics.DrawString(MFR, font, new SolidBrush(Color.Black), startx + 214, starty + offset);
                                //e.Graphics.DrawString(batch, font, new SolidBrush(Color.Black), startx + 295, starty + offset);
                                //e.Graphics.DrawString(exp, font, new SolidBrush(Color.Black), startx + 365, starty + offset);
                                e.Graphics.DrawString(qty, font, new SolidBrush(Color.Black), startx + 455, starty + offset);
                                e.Graphics.DrawString(UOM, font, new SolidBrush(Color.Black), startx + 510, starty + offset);
                                //e.Graphics.DrawString(free, font, new SolidBrush(Color.Black), startx + 500, starty + offset);
                                e.Graphics.DrawString(rate, font, new SolidBrush(Color.Black), startx + 655, starty + offset, format);
                                e.Graphics.DrawString(tax, font, new SolidBrush(Color.Black), startx + 590, starty + offset, format);
                                //e.Graphics.DrawString(mrp, font, new SolidBrush(Color.Black), startx + 650, starty + offset);

                                //e.Graphics.DrawString(gross, font, new SolidBrush(Color.Black), startx + 430, starty + offset);
                                //e.Graphics.DrawString(disc, font, new SolidBrush(Color.Black), startx + 490, starty + offset);

                                if (taxval != 0)
                                {
                                    // e.Graphics.DrawString(txbval, font, new SolidBrush(Color.Black), startx + 535, starty + offset);
                                }
                                else
                                {
                                    //  e.Graphics.DrawString("0", font, new SolidBrush(Color.Black), startx + 535, starty + offset);
                                }


                                total = row.Cells["cTotal"].Value.ToString();
                                e.Graphics.DrawString(total, font, new SolidBrush(Color.Black), startx + 735, starty + offset, format);////total individual amount


                                //offset = offset;
                                offset = offset + (int)fontheight + 10;
                                if (row.Cells["description"].Value != "")
                                {
                                    try
                                    {
                                        e.Graphics.DrawString(row.Cells["description"].Value.ToString(), font, new SolidBrush(Color.Black), startx + 30, starty + offset);
                                        offset = offset + (int)fontheight + 35;
                                    }
                                    catch
                                    {


                                    }


                                }


                                while (BALANCELENGH > 1)
                                {
                                    name2 = BALANCELENGH <= 70 ? row.Cells["cName"].Value.ToString().Substring(printpoint, BALANCELENGH) : row.Cells["cName"].Value.ToString().Substring(printpoint, 70);
                                    e.Graphics.DrawString(name2, Headerfont2, new SolidBrush(Color.Black), startx + 30, starty + offset - 9);
                                    BALANCELENGH = BALANCELENGH - 70;
                                    printpoint = printpoint + 70;
                                    starty = starty + (int)fontheight;
                                }
                                printpoint = 70;

                                //starty = starty + (int)fontheight - 5;
                                nooflines++;
                                j++;

                            }
                            else
                            {

                                printeditems = j - 1;
                                //  e.HasMorePages = true;
                                hasmorepages = true;
                                //m = m - 1;
                                PRINTTOTALPAGE = true;

                            }
                            if (hasmorepages == true)
                            {
                                //m = m + 1;
                                e.Graphics.DrawString("coutinue...", printFontBold, new SolidBrush(Color.Black), startx + 40, 940);
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
            //e.Graphics.DrawString(tfree.ToString(), printFontBold, new SolidBrush(Color.Black), startx + 305, 901);

            //e.Graphics.DrawString(trate.ToString("n2"), printFontBold, new SolidBrush(Color.Black), startx + 537, 875);

            float newoffset = 900;


            if (!PRINTTOTALPAGE)
            {
                PAGETOTAL = true;
                if (PAGETOTAL)
                {
                    try
                    {
                        e.Graphics.DrawLine(blackPen1, 10, 865, 800, 865);
                        e.Graphics.DrawLine(blackPen1, 10, 887, 800, 887);
                        // e.Graphics.DrawString("Sub Total", printFontBold, new SolidBrush(Color.Black), 560, 910);
                        e.Graphics.DrawString("Gross Amount", printFontBold, new SolidBrush(Color.Black), 560, 888);
                        e.Graphics.DrawString("Discount (%)", Headerfont2, new SolidBrush(Color.Black), 560, 904);
                        e.Graphics.DrawString("Trd.Discount (Amt)", Headerfont2, new SolidBrush(Color.Black), 560, 919);
                        e.Graphics.DrawString("Total", Headerfont2, new SolidBrush(Color.Black), 560, 934);
                        e.Graphics.DrawString("VAT", Headerfont2, new SolidBrush(Color.Black), 560, 950);
                        e.Graphics.DrawString("Cess", Headerfont2, new SolidBrush(Color.Black), 560, 966);
                        e.Graphics.DrawString("Adl Discount", Headerfont2, new SolidBrush(Color.Black), 560, 982);
                        e.Graphics.DrawString("Handling/Other", Headerfont2, new SolidBrush(Color.Black), 560, 998);
                        e.Graphics.DrawString("G.Total", Headerfont6, new SolidBrush(Color.Black), 560, 1014);
                        // e.Graphics.DrawLine(blackPen1, 560, 1013, 820, 1013);

                        e.Graphics.DrawString(tqty.ToString(), printFontBold, new SolidBrush(Color.Black), startx + 280, 870);
                        e.Graphics.DrawString(tcdis.ToString(), Headerfont2, new SolidBrush(Color.Black), 788, 919, format);
                        e.Graphics.DrawString(ttaxva.ToString(), Headerfont2, new SolidBrush(Color.Black), 788, 950, format);
                        e.Graphics.DrawString(netamounttotal.ToString("N2"), Headerfont2, new SolidBrush(Color.Black), 788, 934, format);
                        e.Graphics.DrawString(txtFreight.Text, Headerfont2, new SolidBrush(Color.Black), 788, 998, format);
                        e.Graphics.DrawString(TXT_CESS.Text, Headerfont2, new SolidBrush(Color.Black), 788, 966, format);
                        e.Graphics.DrawString(DISCOUNT.Text, Headerfont2, new SolidBrush(Color.Black), 788, 982, format);
                        e.Graphics.DrawString(discp.ToString(), Headerfont2, new SolidBrush(Color.Black), 788, 904, format);



                        string t1 = "Certified that particulars shown in the above tax invoice are true and correct and that my/our registration under KVAT Act";
                        string t2 = "2003nis valid as on the date of the bill";
                        e.Graphics.DrawString("DECLARATION", Headerfont6, new SolidBrush(Color.Black), 25, 1085);
                        e.Graphics.DrawString(t1, Headerfont2, new SolidBrush(Color.Black), 25, 1100);
                        e.Graphics.DrawString(t2, Headerfont2, new SolidBrush(Color.Black), 25, 1115);
                        //e.Graphics.DrawString("Packed By", Headerfont6, new SolidBrush(Color.Black), 25, 1150);
                        //e.Graphics.DrawString("Checked By", Headerfont6, new SolidBrush(Color.Black), 400, 1150);
                        e.Graphics.DrawString("Authorised Signatory", printFontBold, new SolidBrush(Color.Black), 680, 1150);
                        //e.Graphics.DrawLine(blackPen1, 10, 1030, 820, 1030); //940
                        e.Graphics.DrawLine(blackPen1, 10, 1033, 800, 1033); //
                        e.Graphics.DrawString("Terms & Condition", printFontBold, new SolidBrush(Color.Black), 25, 1050);
                        ///// e.Graphics.DrawString(Spell.SpellAmount.comma(Convert.ToDecimal(NET_AMOUNT.Text)), printFontBold1, new SolidBrush(Color.Black), startx + 625, starty + newoffset-20 );
                        string neta = Convert.ToDecimal(NET_AMOUNT.Text).ToString("n2");

                        ////// e.Graphics.DrawString(Spell.SpellAmount.comma(Convert.ToDecimal(neta)), printFontBold1, new SolidBrush(Color.Black), startx + 625, 925);
                        e.Graphics.DrawString(neta, printFontBold, new SolidBrush(Color.Black), 788, 1014, format);
                        //e.Graphics.DrawString(netamounttotal.ToString(), printFontBold, new SolidBrush(Color.Black), 750, 1013);
                        //  e.Graphics.DrawString(TOTAL_AMOUNT.Text, printFontBold, new SolidBrush(Color.Black), 750, 910);
                        e.Graphics.DrawString(grossamounttotal.ToString("N2"), printFontBold, new SolidBrush(Color.Black), 788, 888, format); //Gross Amount
                        try
                        {
                            int cash = (int)Convert.ToDouble(NET_AMOUNT.Text);
                            string cas = NET_AMOUNT.Text;
                            // Math.Floor(cash);
                            //string test = Spell.SpellAmount.InWrods(cash);
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
                                test3 = test + " Rupees only";
                            }
                            e.Graphics.DrawString("Crd/Trk No :", Headerfont6, new SolidBrush(Color.Black), 25, 985);
                            e.Graphics.DrawString("R.Off :" + txtRoundOff.Text, Headerfont6, new SolidBrush(Color.Black), 25, 999);
                            e.Graphics.DrawString(test3, Headerfont6, new SolidBrush(Color.Black), 25, 1014);


                        }
                        catch
                        {
                        }


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

            // newoffset = newoffset + 25;
            newoffset = newoffset;


            e.HasMorePages = hasmorepages;
            */
            #endregion
         //  printGSTPage(e, txt_transportation.Text, txt_vehicle.Text,txt_date.Value.ToShortDateString(), txt_placesupply.Text, CUSTOMER_NAME.Text, "", "", cmbState.Text, Convert.ToString(cmbState.SelectedValue), "", "");
           // threeStarGST(e, txt_transportation.Text, txt_vehicle.Text, txt_date.Value.ToShortDateString(), txt_placesupply.Text, CUSTOMER_NAME.Text, "", "", cmbState.Text, Convert.ToString(cmbState.SelectedValue), "", "");
         //   printGSTPage_ForMalaysia(e, txt_transportation.Text, txt_vehicle.Text, txt_date.Value.ToShortDateString(), txt_placesupply.Text, CUSTOMER_NAME.Text, "", "", cmbState.Text, Convert.ToString(cmbState.SelectedValue), "", "");
            printVATF8(e);
        }
        void printVATF8(PrintPageEventArgs e)
        {
            DataTable curr = getCurrency();
            Company company = Common.getCompany();
            m = 0;
            bool hasmorepages = false;
            float xpos;
            int startx = 50;
            int starty = 20;
            int offset = 15;
            int headerstartposition = 50;
            bool PRINTTOTALPAGE = true;
            int w = e.MarginBounds.Width / 2;
            int x = e.MarginBounds.Left;
            int y = e.MarginBounds.Top;

            Font Headerfont1 = new Font("Calibri", 15, FontStyle.Bold);
            Font dec = new Font("Calibri", 8, FontStyle.Regular);
            Font Headerfont2 = new Font("Times New Roman", 9, FontStyle.Bold);
            Font Headerfont0 = new Font("Times New Roman", 9, FontStyle.Regular);
            Font Headerfont5 = new Font("Times New Roman", 10, FontStyle.Bold);
            Font printFont = new Font("Calibri", 10);
            Font printFontBold = new Font("Calibri", 10, FontStyle.Bold);
            Font printFontBold1 = new Font("Calibri", 14, FontStyle.Bold);
            Font printFontnet = new Font("Calibri", 11, FontStyle.Bold);
            Font printbold = new Font("Calibri", 10, FontStyle.Bold);
            string address1 = "";
            string address2 = "";
            string address0 = "";
            string disc = "";
            string qty = "";
            string rate = "";
            string gross = "";
            string price = "";
            double taxval = 0;
            string txbval = "";
            string free = "0";
            var tabDataForeColor = Color.Black;
            int height = 100 + y;
            Pen blackPen1 = new Pen(Color.Black, 1);
            var txtDataWidth = e.Graphics.MeasureString(CompanyName, Headerfont1).Width;

            try
            {
                if (logo != null || logo != "")
                {

                    System.Drawing.Image img = System.Drawing.Image.FromFile(logo);

                    Point loc = new Point(20, 50);
                    e.Graphics.DrawImage(img, new Rectangle(303, 5, 230, 70));
                }
            }
            catch (Exception ex)
            {

            }
            //else
            //{
            address0 = company.Name;
            address1 = company.Address;
            address2 = company.Other_details;
            string mob1 = company.Phone;
            string mob2 = "";
            string email = company.Email;
            String gsttin = "TRN : " + company.TIN_No;
            // Image i = Image.FromFile("d://alintlogo.jpg");
            // Point p = new Point(100, 100);
            /////e.Graphics.DrawImage(i, new Rectangle(10, 10, 230, 60));
            //e.Graphics.DrawImage(i, new Rectangle(303, 5, 230, 70));
            int centerOfPage = e.PageBounds.Width / 2;
            int nameStartPosision = centerOfPage - TextRenderer.MeasureText(address0, Headerfont5).Width / 2;
            int addressStartPosition = centerOfPage - TextRenderer.MeasureText(address1, Headerfont2).Width / 2;
            int EMAILSTART = centerOfPage - TextRenderer.MeasureText(email, Headerfont2).Width / 2;


            e.Graphics.DrawString(address0, Headerfont5, new SolidBrush(Color.Black), nameStartPosision, starty + offset + 20);
            e.Graphics.DrawString(address1 + "," + "Mob:" + mob1 + "," + mob2, Headerfont2, new SolidBrush(Color.Black), addressStartPosition - 10, starty + offset + 35);
            e.Graphics.DrawString("Email:" + email, Headerfont2, new SolidBrush(Color.Black), EMAILSTART - 5, starty + offset + 47);
            e.Graphics.DrawString("VAT INVOICE", Headerfont5, new SolidBrush(Color.Black), nameStartPosision - 8, starty + offset + 80);
            e.Graphics.DrawString("", Headerfont2, new SolidBrush(Color.Black), 370, starty + offset + 95);
            ///////e.Graphics.DrawLine(blackPen1, 45, 150, 760,150); //940
            e.Graphics.DrawLine(blackPen1, 45, 155, 760, 155); //940
            //e.Graphics.DrawRectangle(blackPen1, 45, 980, 320, 100);
            //string a1 = "(To be furnished by the seller)";
            //string a2 = "Certified that all the particulars in the above tax invoice";
            ////string a3 = "Tax invoice are true and correct and that my/our";
            //string a4 = "are true and correct and that my/our  registration under";
            //string a5 = "KVAT ACT 2003 is valued as on the date of this bill";
            //e.Graphics.DrawString("DECLARATION", Headerfont2, new SolidBrush(Color.Black), startx + 100, 980);
            //e.Graphics.DrawString(a1, Headerfont2, new SolidBrush(Color.Black), startx + 70, 1000);
            //e.Graphics.DrawString(a2, Headerfont2, new SolidBrush(Color.Black), startx - 5, 1020);
            //// e.Graphics.DrawString(a3, Headerfont2, new SolidBrush(Color.Black), startx+10, 1040);
            //e.Graphics.DrawString(a4, Headerfont2, new SolidBrush(Color.Black), startx - 5, 1040);
            //e.Graphics.DrawString(a5, Headerfont2, new SolidBrush(Color.Black), startx + 20, 1060);



            //////e.Graphics.DrawLine(blackPen, 515, 190, 515, 947); //940
            ////////// ,Tax Invoice     
            //System.Drawing.Image img = System.Drawing.Image.FromFile("zed_logo.gif");
            //Point loc = new Point(20, 50);
            //e.Graphics.DrawImage(img, new Rectangle(50, 50, 50, 50));
            // }


            double pricWtax = 0;
            decimal a = 0;

            using (var sf = new StringFormat())
            {

                height += 15;
                sf.LineAlignment = StringAlignment.Center;
                sf.Alignment = StringAlignment.Center;

                xpos = e.MarginBounds.Left + (e.MarginBounds.Width / 2);

                // e.Graphics.DrawString(CompanyName, Headerfont1, new SolidBrush(tabDataForeColor), xpos, starty, sf);
                //   e.Graphics.DrawString(Addres1+", "+Addres2, Headerfont2, new SolidBrush(tabDataForeColor), xpos, starty + offset, sf);
                //  offset = offset + 20;
                //  e.Graphics.DrawString(Phone, Headerfont2, new SolidBrush(tabDataForeColor), xpos, starty + offset, sf);
                //   offset = offset + 20;
                //  e.Graphics.DrawString("Credit Note: " + DOC_NO.Text, Headerfont2, new SolidBrush(tabDataForeColor), xpos, starty + offset, sf);
                //   offset = offset + 20;



                offset = offset + 20;
                ///// e.Graphics.DrawString(Address1 + "," + Address2, Headerfont2, new SolidBrush(Color.Black), headerstartposition, starty + 20 + offset);
                offset = offset + 12;
                /////e.Graphics.DrawString("Phone:".PadRight(3) + Phone, Headerfont2, new SolidBrush(Color.Black), headerstartposition, starty + 20 + offset);
                offset = offset + 12;
                // e.Graphics.DrawString("Email:".PadRight(3) + Email, Headerfont2, new SolidBrush(Color.Black), headerstartposition, starty + 20 + offset);
                offset = offset + 12;
                // e.Graphics.DrawString("Website:".PadRight(3) + Website, Headerfont2, new SolidBrush(Color.Black), headerstartposition, starty + 20 + offset);
                offset = offset + 21;
                offset = offset + 11;
                //////////e.Graphics.DrawString("        THE KERALA VALUE ADDED TAX RULES,2005                                              ,Tax Invoice                     [See rule 58(10),(Cash/Credit)]".PadLeft(10), Headerfont2, new SolidBrush(Color.Black), 20, starty + offset + 9);
                //////////e.Graphics.DrawString("Form No.8B".PadLeft(10), printbold, new SolidBrush(Color.Black), 370, starty + offset + 9);
                offset = offset + 16;
                offset = 15;
                ///////////e.Graphics.DrawString("Tin No       :" + TineNo, Headerfont2, new SolidBrush(Color.Black), 550, starty);
                e.Graphics.DrawString("CR No:" + cst, Headerfont2, new SolidBrush(Color.Black), 45, starty + 40);
                e.Graphics.DrawString("TRN :" + TineNo, Headerfont2, new SolidBrush(Color.Black), 45, starty + 55);
                // e.Graphics.DrawString("CST No      : ", Headerfont2, new SolidBrush(Color.Black), 550, starty + offset);
                //offset = offset + 16;
                offset = offset + 16;
                ///////////e.Graphics.DrawString("Invoice No: ", Headerfont2, new SolidBrush(Color.Black), 550, starty + offset);
                e.Graphics.DrawString("Invoice No:" + VOUCHNUM.Text, Headerfont2, new SolidBrush(Color.Black), 45, starty + offset + 80);
                ///////e.Graphics.DrawString(Billno, printbold, new SolidBrush(Color.Black), 620, starty + offset);
                offset = offset + 16;
                e.Graphics.DrawString("Date:", Headerfont2, new SolidBrush(Color.Black), 605, starty + offset + 65);
                e.Graphics.DrawString("Purchase Order No:" + tb_pono.Text, Headerfont2, new SolidBrush(Color.Black), 480, starty + offset + 90);
                e.Graphics.DrawString("Telephone No:" + tele_no, Headerfont2, new SolidBrush(Color.Black), 480, starty + offset + 108);
                e.Graphics.DrawString("TRN :" + tin_no, Headerfont2, new SolidBrush(Color.Black), 480, starty + offset + 126);
                e.Graphics.DrawString("Mobile No:" + phone_no, Headerfont2, new SolidBrush(Color.Black), 480, starty + offset + 145);
                ///// e.Graphics.DrawString("Tin No:"+ tin_no, Headerfont2, new SolidBrush(Color.Black), 610, starty + offset + 85);



                //////////////////e.Graphics.DrawString("Date:", Headerfont2, new SolidBrush(Color.Black), 610, starty + offset + 65);
                //////////////////e.Graphics.DrawString("Purchase Order No:" + P_ORDER_NO.Text, Headerfont2, new SolidBrush(Color.Black), 570, starty + offset + 85);
                //////////////////e.Graphics.DrawString("Telephone No:" + tele_no, Headerfont2, new SolidBrush(Color.Black), 570, starty + offset + 115);
                //////////////////e.Graphics.DrawString("Mobile No:" + phone_no, Headerfont2, new SolidBrush(Color.Black), 570, starty + offset + 145);
                /////////////////////// e.Graphics.DrawString("Tin No:"+ tin_no, Headerfont2, new SolidBrush(Color.Black), 610, starty + offset + 85);
                //////////////////e.Graphics.DrawString("Tin No:" + tin_no, Headerfont2, new SolidBrush(Color.Black), 430, starty + offset + 145);





                /////////////e.Graphics.DrawString(DateTime.Now.ToString(), printbold, new SolidBrush(Color.Black), 620, starty + offset);
                //// e.Graphics.DrawString(DateTime.Now.ToString(), Headerfont2, new SolidBrush(Color.Black), 630, starty + offset + 65);
                e.Graphics.DrawString(DOC_DATE_GRE.Text, Headerfont2, new SolidBrush(Color.Black), 630, starty + offset + 65);
                offset = offset + 16;
                //  e.Graphics.DrawString("Del.Note No & Date :", Headerfont2, new SolidBrush(Color.Black), 580, starty + offset);
                offset = offset + 16;
                //   e.Graphics.DrawString("Des.Docu.No & Date:", Headerfont2, new SolidBrush(Color.Black), 580, starty + offset);
                offset = offset + 16;


                //e.Graphics.DrawString("Form No.8", Headerfont2, new SolidBrush(Color.Black), 510, starty + offset);

                // e.Graphics.DrawString("[See rule 58(10)]", Headerfont2, new SolidBrush(Color.Black), 510, starty + offset);

                // e.Graphics.DrawString("Tax Invoice/Cash/Credit", Headerfont2, new SolidBrush(Color.Black), 510, starty + offset);
                offset = offset + 16;
                offset = offset + 16;
                offset = offset + 16;
                offset = offset + 16;
                Pen blackPen = new Pen(Color.Black, 1);
                Point point1 = new Point(45, 190);
                Point point2 = new Point(760, 190);
                e.Graphics.DrawLine(blackPen, 45, 230, 760, 230); //(45,190,760,190)
                ////////  e.Graphics.DrawLine(blackPen, point1, point2); //(45,190,760,190)org

                //offset = offset + 2;
                //Point point5 = new Point(45, 130);
                //Point point6 = new Point(760, 130);
                //e.Graphics.DrawLine(blackPen, point5,point6);


                //offset = offset + 2;
                //Point point7 = new Point(45, 155);
                //Point point8 = new Point(760, 155);
                //  e.Graphics.DrawLine(blackPen, point7, point8);
                // e.Graphics.DrawLine(blackPen, 45, 130, 45, 130);
                ////////// e.Graphics.DrawRectangle(blackPen, 45, 130, 715, 25);




                e.Graphics.DrawString("To:" + CUSTOMER_NAME.Text, Headerfont5, new SolidBrush(Color.Black), startx, starty + offset - 20);
                e.Graphics.DrawString(ADDRESS_A, Headerfont5, new SolidBrush(Color.Black), startx, starty + offset);

                Font itemhead = new Font("Times New Roman", 8);
                offset = offset + 2;

                Point point3 = new Point(45, 219);
                Point point4 = new Point(760, 219);
                e.Graphics.DrawLine(blackPen, 45, 250, 760, 250);//(45,219,760,219)
                //////////e.Graphics.DrawLine(blackPen, point3, point4);//(45,219,760,219) org




                e.Graphics.DrawLine(blackPen, 45, 155, 45, 900);
                ////e.Graphics.DrawLine(blackPen, 45, 230, 45, 900);

                //zed invoice

                //e.Graphics.DrawLine(blackPen, 45, 110, 45, 160);
                // e.Graphics.DrawLine(blackPen, 45 , 45 , 


                //////////////////////////////////e.Graphics.DrawLine(blackPen, 570, 230, 570, 920); //NEW net rate
                //////////////////////////////////e.Graphics.DrawLine(blackPen, 630, 230, 630, 947); //940 taxval

                ////////////e.Graphics.DrawLine(blackPen, 80, 230, 80, 900);
                ////////////e.Graphics.DrawLine(blackPen, 370, 230, 370, 920); // mrp
                ////////////e.Graphics.DrawLine(blackPen, 420, 230, 420, 920); //tax  
                ////////////e.Graphics.DrawLine(blackPen, 465, 230, 465, 920);//qty
                ////////////e.Graphics.DrawLine(blackPen, 515, 230, 515, 947); //940 rate
                ////////////e.Graphics.DrawLine(blackPen, 570, 230, 570, 920); //NEW net rate
                ////////////e.Graphics.DrawLine(blackPen, 620, 230, 620, 947); //940 taxval
                ////////////e.Graphics.DrawLine(blackPen, 680, 230, 680, 920); //total
                ////// e.Graphics.DrawLine(blackPen, 280, 230, 280, 920); // mrp
                //////e.Graphics.DrawLine(blackPen, 320, 230, 320, 920); // mrp
                //////e.Graphics.DrawLine(blackPen, 355, 230, 355, 920);





                e.Graphics.DrawLine(blackPen, 80, 230, 80, 900);
                // e.Graphics.DrawLine(blackPen, 280, 230, 280, 920); // mrp
                //e.Graphics.DrawLine(blackPen, 280, 230, 280, 920); // mrp
                e.Graphics.DrawLine(blackPen, 280, 230, 280, 920); //tax  end line
                e.Graphics.DrawLine(blackPen, 320, 230, 320, 920);
                e.Graphics.DrawLine(blackPen, 355, 230, 355, 920);
                e.Graphics.DrawLine(blackPen, 385, 230, 385, 920);//new qty
                e.Graphics.DrawLine(blackPen, 425, 230, 425, 920);//qty
                e.Graphics.DrawLine(blackPen, 480, 230, 480, 947); //940 rate
                e.Graphics.DrawLine(blackPen, 540, 230, 540, 920); //NEW net rate
                e.Graphics.DrawLine(blackPen, 585, 230, 585, 947); //940 taxval
                e.Graphics.DrawLine(blackPen, 645, 230, 645, 920); //total
                e.Graphics.DrawLine(blackPen, 695, 230, 695, 920); //new total
                ///// e.Graphics.DrawLine(blackPen, 760, 230, 760, 920);
                e.Graphics.DrawLine(blackPen, 760, 155, 760, 920);

                ///////// e.Graphics.DrawLine(blackPen, 45, 900, 760, 900);
                e.Graphics.DrawLine(blackPen, 45, 900, 760, 900);
                e.Graphics.DrawRectangle(blackPen, 45, 900, 715, 20);
                /// e.Graphics.DrawRectangle(blackPen, 45, 920, 715, 20);
                e.Graphics.DrawRectangle(blackPen, 45, 920, 715, 27);
                ///////// e.Graphics.DrawRectangle(blackPen, 45, 920, 715, 27); org
                offset = offset + 50;
                ///////offset = offset + 16;org
                //////// string headtext = "Sl.No".PadRight(10) + "Item".PadRight(100) + "Tax%".PadRight(10) + "Qty".PadRight(12) + "Rate".PadRight(10) + "Net Rate".PadRight(11) +"Tax Val".PadRight(13)+ "Total";
                string headtext = "Sl.No".PadRight(10) + "Item".PadRight(55) + "Tax%".PadRight(7) + "Qty".PadRight(5) + "UOM".PadRight(7) + "MRP".PadRight(9) + "Rate".PadRight(10) + "Gross".PadRight(15) + "Disc".PadRight(9) + "Txble Val".PadRight(15) + "Tax".PadRight(12) + "Total";
                ////    string headtext = "Sl.No".PadRight(10) + "Item".PadRight(85) + "Tax%".PadRight(10) + "Qty".PadRight(10) + "MRP".PadRight(12) + "Rate".PadRight(10) + "Tax Val".PadRight(10) + "Net Rate".PadRight(13) + "Total";
                e.Graphics.DrawString(headtext, printFontBold, new SolidBrush(Color.Black), startx - 4, starty + offset - 1);

                offset = offset + 40;
                Font font = new Font("Times New Roman", 10);
                float fontheight = font.GetHeight();



                try
                {

                    int i = 0;
                    int printpoint = 25; //32
                    int j = 1;
                    string mrp = "";
                    int nooflines = 0;
                    //int INCRIMENTHEIGHT = starty + offset;
                    double pq;
                    string total = "";


                    foreach (DataGridViewRow row in dgItems.Rows)
                    {
                        PRINTTOTALPAGE = false;
                        if (j > printeditems)
                        {
                            if (nooflines < 15)
                            {
                                m = m + 1;

                                string period, periodtype, tax, uom;
                                i = i + 1;
                                int ORGLENGTH = row.Cells["cName"].Value.ToString().Length; /////32

                                string name = row.Cells["cName"].Value.ToString().Length <= 30 ? row.Cells[1].Value.ToString() : row.Cells[1].Value.ToString().Substring(0, 30);
                                string name2 = "";
                                int BALANCELENGH = ORGLENGTH - 30;
                                tax = row.Cells["cTaxPer"].Value.ToString();
                                if (tax == "")
                                    tax = "1";
                                qty = row.Cells["cQty"].Value.ToString();
                                uom = row.Cells["cunit"].Value.ToString();
                                rate = row.Cells["cPrice"].Value.ToString();
                                try
                                {
                                    mrp = row.Cells["cMrp"].Value.ToString();
                                }
                                catch
                                {

                                    mrp = "0";
                                }
                                try
                                {
                                    free = row.Cells["Cfree"].Value.ToString();
                                }
                                catch
                                {
                                    free = "0";
                                }
                                disc = row.Cells["cDisc"].Value.ToString();
                                tcdis = tcdis + Convert.ToDecimal(disc);
                                gross = (Convert.ToDouble(qty) * Convert.ToDouble(rate)).ToString("N2");

                                price = Convert.ToDouble(row.Cells[12].Value).ToString();
                                //    string Serial = row.Cells["SerialNos"].Value.ToString();
                                taxval = Convert.ToDouble(row.Cells["cTaxAmt"].Value);
                                txbval = "";
                                if (taxval != 0)
                                {
                                    txbval = (Convert.ToDouble(qty) * Convert.ToDouble(rate) - Convert.ToDouble(disc)).ToString("N2");
                                    ttaxbl = ttaxbl + Convert.ToDecimal(txbval);
                                }
                                else
                                {
                                    txbval = (Convert.ToDouble(qty) * Convert.ToDouble(rate) - Convert.ToDouble(disc)).ToString("N2");

                                }
                                //double taxval = (Convert.ToDouble(rate) * Convert.ToDouble(qty)) * (Convert.ToDouble(tax) / 100 / Convert.ToDouble(qty));
                                //Math.Round(taxval, 2);
                                pricWtax = Convert.ToDouble(rate) + (Convert.ToDouble(rate) * Convert.ToDouble(qty)) * (Convert.ToDouble(tax) / 100 / Convert.ToDouble(qty));
                                // Math.Round(pricWtax, 2);
                                try
                                {
                                    tfree = tfree + Convert.ToInt32(free);
                                }
                                catch
                                {

                                }
                                tqty = tqty + Convert.ToInt32(qty);
                                tgrossrate = tgrossrate + Convert.ToDecimal(gross);
                                ttaxva = ttaxva + Convert.ToDecimal(taxval);
                                trate = trate + Convert.ToDecimal(rate);
                                a = a + Convert.ToDecimal(pricWtax);
                                string productline = name + tax + qty + rate + price;
                                e.Graphics.DrawString(m.ToString(), font, new SolidBrush(Color.Black), startx, starty + offset);
                                e.Graphics.DrawString(name, font, new SolidBrush(Color.Black), startx + 30, starty + offset);
                                e.Graphics.DrawString(tax, font, new SolidBrush(Color.Black), startx + 230, starty + offset);
                                e.Graphics.DrawString(qty, font, new SolidBrush(Color.Black), startx + 270, starty + offset);
                                e.Graphics.DrawString(uom, font, new SolidBrush(Color.Black), startx + 305, starty + offset);
                                e.Graphics.DrawString(mrp, font, new SolidBrush(Color.Black), startx + 335, starty + offset);
                                e.Graphics.DrawString(rate, font, new SolidBrush(Color.Black), startx + 375, starty + offset);
                                e.Graphics.DrawString(gross, font, new SolidBrush(Color.Black), startx + 430, starty + offset);
                                e.Graphics.DrawString(disc, font, new SolidBrush(Color.Black), startx + 490, starty + offset);

                                if (taxval != 0)
                                {
                                    e.Graphics.DrawString(txbval, font, new SolidBrush(Color.Black), startx + 535, starty + offset);
                                }
                                else
                                {
                                    e.Graphics.DrawString("0", font, new SolidBrush(Color.Black), startx + 535, starty + offset);
                                }

                                e.Graphics.DrawString(taxval.ToString("N2"), font, new SolidBrush(Color.Black), startx + 595, starty + offset);

                                total = row.Cells["cTotal"].Value.ToString();
                                e.Graphics.DrawString(total, font, new SolidBrush(Color.Black), startx + 645, starty + offset);////total individual amount
                                offset = offset + (int)fontheight + 10;
                                if (Convert.ToString(row.Cells["description"].Value) != "")
                                {
                                    try
                                    {
                                        e.Graphics.DrawString(row.Cells["description"].Value.ToString(), font, new SolidBrush(Color.Black), startx + 30, starty + offset);
                                        offset = offset + (int)fontheight + 13;
                                    }
                                    catch
                                    {


                                    }


                                }

                                //if (Serial != "")
                                //{
                                //    e.Graphics.DrawString("SN No: " + Serial, font, new SolidBrush(Color.Black), startx + 30, starty + offset);

                                //    offset = offset + (int)fontheight + 10;
                                //}




                                while (BALANCELENGH > 1)
                                {

                                    ///44
                                    name2 = BALANCELENGH <= 30 ? row.Cells["cName"].Value.ToString().Substring(printpoint, BALANCELENGH) : row.Cells["cName"].Value.ToString().Substring(printpoint, 30);
                                    e.Graphics.DrawString(name2, font, new SolidBrush(Color.Black), startx + 30, starty + offset - 9);
                                    BALANCELENGH = BALANCELENGH - 30;
                                    printpoint = printpoint + 30;
                                    starty = starty + (int)fontheight;

                                    //wf = wf + 16;
                                    // offset += 3;
                                    // nooflines++;
                                }
                                printpoint = 30;

                                ////////starty = starty + (int)fontheight - 5;

                                //while (BALANCELENGH > 0)
                                //{


                                //    name2 = BALANCELENGH <= 30 ? row.Cells["cName"].Value.ToString().Substring(printpoint, BALANCELENGH) : row.Cells["cName"].Value.ToString().Substring(printpoint, 30);
                                //    e.Graphics.DrawString(name2, Headerfont2, new SolidBrush(Color.Black), startx + 30, starty + offset);
                                //    BALANCELENGH = BALANCELENGH - 30;
                                //    printpoint = printpoint + 30;
                                //    starty = starty + (int)fontheight;

                                //    // wf = wf + 16;
                                //    // offset += 3;
                                //    //nooflines++;
                                //}
                                //starty = starty + (int)fontheight - 5;
                                nooflines++;
                                j++;

                            }
                            else
                            {

                                printeditems = j - 1;
                                //  e.HasMorePages = true;
                                hasmorepages = true;
                                //m = m - 1;
                                PRINTTOTALPAGE = true;

                            }
                            if (hasmorepages == true)
                            {
                                //m = m + 1;
                                e.Graphics.DrawString("coutinue...", printFontBold, new SolidBrush(Color.Black), startx + 40, 901);
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
            //  e.Graphics.DrawString("E & OE", Headerfont2, new SolidBrush(Color.Black), startx, 1080);
            float newoffset = 900;
            if (!PRINTTOTALPAGE)
            {
                PAGETOTAL = true;
                if (PAGETOTAL)
                {
                    try
                    {
                        e.Graphics.DrawString("Grand Total", printFontBold1, new SolidBrush(Color.Black), startx + 436, 925);
                        e.Graphics.DrawString("Authorised Signatory", printFontBold, new SolidBrush(Color.Black), startx + 585, 1050);
                       // e.Graphics.DrawString("Alint Enterprises", printFontBold, new SolidBrush(Color.Black), startx + 600, 1070);
                        e.Graphics.DrawString(tfree.ToString(), printFontBold, new SolidBrush(Color.Black), startx + 305, 901);
                        e.Graphics.DrawString(tqty.ToString(), printFontBold, new SolidBrush(Color.Black), startx + 270, 901);
                        e.Graphics.DrawString(tgrossrate.ToString("n2"), printFontBold, new SolidBrush(Color.Black), startx + 430, 901);
                        e.Graphics.DrawString(ttaxbl.ToString("n2"), printFontBold, new SolidBrush(Color.Black), startx + 535, 901);
                        e.Graphics.DrawString(ttaxva.ToString("n2"), printFontBold, new SolidBrush(Color.Black), startx + 595, 901);
                        e.Graphics.DrawString("Total", printFontBold, new SolidBrush(Color.Black), startx + 193, 901);
                        e.Graphics.DrawString("Discount: ", Headerfont2, new SolidBrush(Color.Black), startx + 180, 921);
                        e.Graphics.DrawString("Freight: ", Headerfont2, new SolidBrush(Color.Black), startx + 60, 921);
                        e.Graphics.DrawString("Roundoff: ", Headerfont2, new SolidBrush(Color.Black), startx + 290, 921);
                        e.Graphics.DrawString(tcdis.ToString("n2"), printFontBold, new SolidBrush(Color.Black), startx + 490, 901);
                        string neta = Convert.ToDecimal(NET_AMOUNT.Text).ToString("n2");
                        e.Graphics.DrawString(Spell.SpellAmount.comma(Convert.ToDecimal(neta)), printFontBold1, new SolidBrush(Color.Black), startx + 550, 925);
                        //try
                        //{
                        //    int cash = (int)Convert.ToDouble(NET_AMOUNT.Text);
                        //    string cas = NET_AMOUNT.Text;
                        //    string[] parts = cas.Split('.');
                        //    string test3 = "";
                        //    long i1, i2;
                        //    try
                        //    {
                        //        i1 = (long)Convert.ToDouble(parts[0]);
                        //    }
                        //    catch
                        //    {
                        //        i1 = 0;
                        //    }
                        //    try
                        //    {
                        //        i2 = (long)Convert.ToDouble(parts[1]);
                        //    }
                        //    catch
                        //    {
                        //        i2 = 0;
                        //    }

                        //    if (i1 != 0 && i2 != 0)
                        //    {
                        //        string test = NumbersToWords(i1);
                        //        string test2 = NumbersToWords(i2);
                        //        test3 = test + " Dirham and " + test2 + " Fils only";
                        //        int index = test3.IndexOf("Fils");
                        //    }
                        //    if (i1 > 0 && i2 == 0)
                        //    {
                        //        string test = NumbersToWords(i1);
                        //        test3 = test + " Dirham only";
                        //    }
                        //    e.Graphics.DrawString(test3, Headerfont2, new SolidBrush(Color.Black), startx, 951);


                        //}
                        //catch
                        //{
                        //}




                   try
                        {
                            //int cash = (int)Convert.ToDouble(ttotalvalue);
                            int cash = (int)Convert.ToDouble(NET_AMOUNT.Text);
                            string cas = NET_AMOUNT.Text;
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
                                test3 = test + " Dirham and " + test2 + "Fils only";

                                string seclin, linef;
                                int index = test3.IndexOf("Dirham");
                            }
                            if (i1 > 0 && i2 == 0)
                            {
                                string test = NumbersToWords(i1);
                                test3 = test + " only";
                            }
                           // e.Graphics.DrawString(test3, printbold, new SolidBrush(Color.Black), 7, 851);
                        }
                        catch
                        {
                        }
                   
                  










                        newoffset = newoffset + 20;
                        newoffset = newoffset + 20;
                        e.Graphics.DrawString(DISCOUNT.Text, printFontBold, new SolidBrush(Color.Black), startx + 242, 921);
                        e.Graphics.DrawString(txtFreight.Text, printFontBold, new SolidBrush(Color.Black), startx + 110, 921);
                        e.Graphics.DrawString(txtRoundOff.Text, printFontBold, new SolidBrush(Color.Black), startx + 350, 921);
                        newoffset = newoffset + 20;
                    }
                    catch
                    {
                    }
                }

                PAGETOTAL = false;
            }
            newoffset = newoffset + 25;
            e.HasMorePages = hasmorepages;
        }
        public void GetBranchDetails()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = ComSet.GetCurrentBranchDetails();
                if (dt.Rows.Count > 0)
                {
                    Address1 = dt.Rows[0][1].ToString();
                    Address2 = dt.Rows[0][2].ToString();
                    Phone = dt.Rows[0][3].ToString();
                    Email = dt.Rows[0][4].ToString();
                    Fax = dt.Rows[0][5].ToString();
                    ArbBranch = dt.Rows[0]["DESC_ARB"].ToString();
                    ArbAddress1 = dt.Rows[0]["ARBADDRESS_1"].ToString();
                    ArbAddress2 = dt.Rows[0]["ARBADDRESS_2"].ToString();
                    PrintPage.Text = dt.Rows[0][6].ToString();
                    EngBranch = dt.Rows[0]["DESC_ENG"].ToString();
                    CURRENCY_CODE.SelectedValue = dt.Rows[0]["DEFAULT_CURRENCY_CODE"].ToString();
                }
            }
            catch
            {
            }
        }
        
        public void GetCompanyDetails()
        {
            DataTable dt = new DataTable();
            dt = ComSet.getCompanyDetails();
            if (dt.Rows.Count > 0)
            {
                CompanyName = dt.Rows[0][1].ToString();
                TineNo = dt.Rows[0][8].ToString();
                CUSID = dt.Rows[0][10].ToString();
                Website = dt.Rows[0][11].ToString();
                panno = dt.Rows[0][9].ToString();
                cst = dt.Rows[0][10].ToString();
                ArabicName = dt.Rows[0]["ARBCompany_Name"].ToString();

                try
                {
                    logo = dt.Rows[0]["Logo"].ToString();
                }
                catch
                {
                }

            }
        }
        
        public void GetSalesMan()
        {
            try
            {
                // Login log = (Login)Application.OpenForms["Login"];
                SALESMAN_CODE.Text = lg.EmpId;
                conn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetSalesMan";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Empid", lg.EmpId);
                SALESMAN_NAME.Text = Convert.ToString(cmd.ExecuteScalar());
                conn.Close();
                cmd.CommandType = CommandType.Text;
            }
            catch
            {
            }

        }
        DataTable getCurrency()
        {
            DataTable currencyTable = new DataTable();
            conn.Open();
            SqlCommand cmdd = new SqlCommand();
            cmdd.CommandText="SELECT * FROM GEN_CURRENCY WHERE CODE='" + CURRENCY_CODE.SelectedValue + "'" ;
            cmdd.Connection = conn;
            SqlDataAdapter adptr = new SqlDataAdapter(cmdd);

            adptr.Fill(currencyTable);
            conn.Close();
            return currencyTable;
        }
        void Print_thermal(object sender, PrintPageEventArgs e)
        {
            DataTable currencyTable = getCurrency();
            Company company = Common.getCompany();
            float xpos;
            int startx = 10;
            int starty = 30;

            int startX = 5;
            int startY = 15;
            decimal gross = 0;
            string companyName = company.Name;
            string address1 = company.Address;
            string mob1 = company.Phone;
            string email = company.Email;
            String gsttin = "GSTIN : " + company.TIN_No;
            Font printFontBold2 = new Font("Times New Roman", 12, FontStyle.Bold);
            Font Font2 = new Font("Times New Roman", 10, FontStyle.Bold);
            int w = e.MarginBounds.Width / 2;
            int x = e.MarginBounds.Left;
            int y = e.MarginBounds.Top;
            Font printFont = new Font("Courier New", 8);
            var tabDataForeColor = Color.Black;
            int height = 100 + y;
            StringFormat format = new StringFormat(StringFormatFlags.DirectionRightToLeft);

            var txtDataWidth = e.Graphics.MeasureString(CompanyName, printFont).Width;


            using (var sf = new StringFormat())
            {
                height += 15;
                sf.LineAlignment = StringAlignment.Center;
                sf.Alignment = StringAlignment.Center;

                xpos = e.MarginBounds.Left + (e.MarginBounds.Width / 2);

                e.Graphics.DrawString(companyName, printFontBold2, new SolidBrush(tabDataForeColor), xpos, startY - 2, sf);
                startY += 10;
                e.Graphics.DrawString(address1, printFont, new SolidBrush(tabDataForeColor), xpos, startY, sf);
                startY += 10;
                e.Graphics.DrawString("Mob:" + mob1, printFont, new SolidBrush(tabDataForeColor), xpos, startY, sf);
                startY += 10;
                string taxtype = Properties.Settings.Default.Tax_Type.ToString();
               // e.Graphics.DrawString(taxtype + " ID:" + TineNo, printFont, new SolidBrush(tabDataForeColor), xpos, startY, sf);
                startY += 15;
                e.Graphics.DrawString("INVOICE", Font2, new SolidBrush(tabDataForeColor), xpos, startY, sf);
                startY += 10;
                e.Graphics.DrawString("-----------------------------------------------", printFont, new SolidBrush(Color.Black), xpos, startY, sf);
                e.Graphics.DrawString("-----------------------------------------------", printFont, new SolidBrush(Color.Black), xpos, startY + 2, sf);
                startY += 10;
                e.Graphics.DrawString("Customer:" + CUSTOMER_NAME.Text, printFont, new SolidBrush(tabDataForeColor), startX, startY);
                startY += 10;
                e.Graphics.DrawString("Inv No:" + VOUCHNUM.Text, printFont, new SolidBrush(tabDataForeColor), startX, startY);
                startY += 10;
                e.Graphics.DrawString("Date:" + DOC_DATE_GRE.Value, printFont, new SolidBrush(tabDataForeColor), startX, startY);
                Font itemhead = new Font("Courier New", 8);
                startY += 10;
                e.Graphics.DrawString("---------------------------------------------------", printFont, new SolidBrush(Color.Black), 0, startY);
                string headtext = "Sl".PadRight(3) + "Item".PadRight(21) + "Qty".PadRight(5) + "Rate".PadRight(6) + "Total";
                e.Graphics.DrawString(headtext, itemhead, new SolidBrush(Color.Black), startX, startY + 8);
                startY += 15;
                e.Graphics.DrawString("---------------------------------------------------", printFont, new SolidBrush(Color.Black), 0, startY);
                startY += 15;
                Font font = new Font("Times New Roman", 8);
                Font font1 = new Font("Times New Roman", 7);
                float fontheight = font.GetHeight();
                
                try
                {
                    int i = 1;
                    foreach (DataGridViewRow row in dgItems.Rows)
                    {

                        string name = "";// row.Cells[1].Value.ToString().Length <= 28 ? row.Cells[1].Value.ToString() : row.Cells[1].Value.ToString().Substring(0, 28);
                        string qty = row.Cells[5].Value.ToString();
                              string price = row.Cells["cTotal"].Value.ToString();
                              string rate = (Convert.ToDecimal(price) / Convert.ToDecimal(qty)).ToString();
                  
                        string productline = name + qty + rate + price;
                        //string qty = row.Cells[5].Value.ToString();
                        //string rate = row.Cells["cPrice"].Value.ToString();
                        //string price = row.Cells[11].Value.ToString();
                        //string productline = name + qty + rate + price;
                        //e.Graphics.DrawString(i.ToString(), font, new SolidBrush(Color.Black), startX+12, startY,format);
                        //e.Graphics.DrawString(name, font, new SolidBrush(Color.Black), startX+14, startY);
                        //e.Graphics.DrawString(qty, font1, new SolidBrush(Color.Black), startX+187, startY,format);
                        //e.Graphics.DrawString(rate, font1, new SolidBrush(Color.Black), startX + 230, startY, format);
                        //e.Graphics.DrawString(price, font, new SolidBrush(Color.Black), startX + 272, startY, format);
                        //e.Graphics.DrawString("SR", font1, new SolidBrush(Color.Black), startX + 275, startY);
                        if (row.Cells[1].Value.ToString().Length <= 28)
                        {
                           // Convert.ToDouble(rate)).ToString("N2")
                            e.Graphics.DrawString(i.ToString(), font, new SolidBrush(Color.Black), startX + 12, startY, format);
                            e.Graphics.DrawString(row.Cells[1].Value.ToString(), font1, new SolidBrush(Color.Black), startX + 14, startY);
                            e.Graphics.DrawString(qty, font1, new SolidBrush(Color.Black), startX + 187, startY, format);
                            e.Graphics.DrawString((Convert.ToDecimal(rate)).ToString("N2"), font1, new SolidBrush(Color.Black), startX + 230, startY, format);
                            e.Graphics.DrawString(price, font, new SolidBrush(Color.Black), startX + 272, startY, format);
                            e.Graphics.DrawString(currencyTable.Rows[0]["CODE"].ToString(), font1, new SolidBrush(Color.Black), startX + 275, startY);
                        }
                        else
                        {
                            int lngth = row.Cells[1].Value.ToString().Length;
                            if(lngth<50)
                                name = row.Cells[1].Value.ToString();
                            else
                                name = row.Cells[1].Value.ToString().Substring(0, 50);
                            e.Graphics.DrawString(i.ToString(), font, new SolidBrush(Color.Black), startX + 12, startY, format);
                            e.Graphics.DrawString(name, font1, new SolidBrush(Color.Black), startX + 14, startY);
                            startY += (int)fontheight + 5;
                            if (lngth > 50)
                            {
                                
                                int tobePrint = 28;
                                if (lngth < 78)
                                    tobePrint = lngth - 50;
                                name = row.Cells[1].Value.ToString().Substring(50, tobePrint);
                                e.Graphics.DrawString(name, font1, new SolidBrush(Color.Black), startX + 14, startY);
                            }
                            e.Graphics.DrawString(qty, font1, new SolidBrush(Color.Black), startX + 187, startY, format);
                            e.Graphics.DrawString((Convert.ToDecimal(rate)).ToString("N2"), font1, new SolidBrush(Color.Black), startX + 230, startY, format);
                            e.Graphics.DrawString(price, font, new SolidBrush(Color.Black), startX + 272, startY, format);
                            e.Graphics.DrawString(currencyTable.Rows[0]["CODE"].ToString(), font1, new SolidBrush(Color.Black), startX + 275, startY);

                        }
                        i++;
                        startY += (int)fontheight + 5;
                    }
                }
                catch 
                {
                  
                }

                startY += 10;
                e.Graphics.DrawString("-----------------------------------------------", printFont, new SolidBrush(Color.Black), 0, startY);

                startY += 15;
                //try
                //{
                //    string test = Spell.SpellAmount.InWrods(Convert.ToDecimal(NET_AMOUNT.Text));

                //    int index = test.IndexOf("Taka ");
                //    int l = test.Length;
                //    test = test.Substring(index + 4);

                //    e.Graphics.DrawString(test, font, new SolidBrush(Color.Black), startX, startY);
                //}
                //catch
                //{
                //}
                //try
                //{
                //    int cash = (int)Convert.ToDouble(NET_AMOUNT.Text);
                //    string cas = NET_AMOUNT.Text;
                //    string[] parts = cas.Split('.');
                //    string test3 = "";
                //    long i1, i2;
                //    try
                //    {
                //        i1 = (long)Convert.ToDouble(parts[0]);
                //    }
                //    catch
                //    {
                //        i1 = 0;
                //    }
                //    try
                //    {
                //        i2 = (long)Convert.ToDouble(parts[1]);
                //    }
                //    catch
                //    {
                //        i2 = 0;
                //    }

                //    if (i1 != 0 && i2 != 0)
                //    {
                //        string test = NumbersToWords(i1);
                //        string test2 = NumbersToWords(i2);
                //        test3 = test + " Renggit and " + test2 + " Sen only";
                //        int index = test3.IndexOf("Sen");
                //    }
                //    if (i1 > 0 && i2 == 0)
                //    {
                //        string test = NumbersToWords(i1);
                //        test3 = test + " Renggit only";
                //    }
                //    Font font11 = new Font("Times New Roman", 7);
                //    // e.Graphics.DrawString(test3, font11, new SolidBrush(Color.Black), startx, 951);
                //    e.Graphics.DrawString(test3, font, new SolidBrush(Color.Black), startX, startY);
                //}
                //catch
                //{ }
                string grosstotal = "Gross Total:";
                string TAXP = "";
                if (taxtype == "GST")
                    TAXP = "6%";
                else
                    TAXP = "5%";
                string vatstring = "Tax Amount:" + TAXP;
                string Discountstring = "Discount:";
                string total = "Total:";
                startY += 10;
                e.Graphics.DrawString(grosstotal, font, new SolidBrush(Color.Black), startX + 130, startY);

                e.Graphics.DrawString(Spell.SpellAmount.comma(Convert.ToDecimal(TOTAL_AMOUNT.Text)), font, new SolidBrush(Color.Black), startX + 272, startY, format);
                startY += 10;
              //  e.Graphics.DrawString("Tax Amount" + TAXP + ":", font, new SolidBrush(Color.Black), startX + 130, startY);
              //  e.Graphics.DrawString(Spell.SpellAmount.comma(Convert.ToDecimal(TAX_TOTAL.Text)), font, new SolidBrush(Color.Black), startX + 272, startY, format);

                startY += 10;
                e.Graphics.DrawString(Discountstring, font, new SolidBrush(Color.Black), startX + 130, startY);

                e.Graphics.DrawString(Spell.SpellAmount.comma(Convert.ToDecimal(DISCOUNT.Text)), font, new SolidBrush(Color.Black), startX + 272, startY, format);

                startY += 10;
                e.Graphics.DrawString("---------------------------------------", font, new SolidBrush(Color.Black), startX + 130, startY);

                startY += 10;
                e.Graphics.DrawString(total, font, new SolidBrush(Color.Black), startX + 130, startY);
                e.Graphics.DrawString(Spell.SpellAmount.comma(Convert.ToDecimal(NET_AMOUNT.Text)), font, new SolidBrush(Color.Black), startX + 272, startY, format);

                startY += 15;
                e.Graphics.DrawString("---------------------------------------------------", printFont, new SolidBrush(Color.Black), 0, startY);
                e.Graphics.DrawString("***Tank You For Visit***", printFont, new SolidBrush(Color.Black), xpos, startY + 15, sf);
                e.Graphics.DrawString("---------------------------------------------------", printFont, new SolidBrush(Color.Black), 0, startY + 15);

           }

            e.HasMorePages = false;

        }


        void Print_thermal_VAT(object sender, PrintPageEventArgs e)
        {
            DataTable currencyTable = getCurrency();
            Company company = Common.getCompany();
            float xpos;
            int startx = 10;
            int starty = 30;

            int startX = 5;
            int startY = 15;
            decimal gross = 0;
            string companyName = company.Name;
            string address1 = company.Address;
            string mob1 = company.Phone;
            string email = company.Email;
            String TIN = "GSTIN : " + company.TIN_No;
            Font printFontBold2 = new Font("Times New Roman", 12, FontStyle.Bold);
            Font Font2 = new Font("Times New Roman", 10, FontStyle.Bold);
            int w = e.MarginBounds.Width / 2;
            int x = e.MarginBounds.Left;
            int y = e.MarginBounds.Top;
            Font printFont = new Font("Courier New", 8);
            var tabDataForeColor = Color.Black;
            int height = 100 + y;
            StringFormat format = new StringFormat(StringFormatFlags.DirectionRightToLeft);

            var txtDataWidth = e.Graphics.MeasureString(CompanyName, printFont).Width;


            using (var sf = new StringFormat())
            {
                height += 15;
                sf.LineAlignment = StringAlignment.Center;
                sf.Alignment = StringAlignment.Center;

                xpos = e.MarginBounds.Left + (e.MarginBounds.Width / 2);

                e.Graphics.DrawString(companyName, printFontBold2, new SolidBrush(tabDataForeColor), xpos, startY - 2, sf);
                startY += 10;
                e.Graphics.DrawString(address1, printFont, new SolidBrush(tabDataForeColor), xpos, startY, sf);
                startY += 10;
                e.Graphics.DrawString("Mob:" + mob1, printFont, new SolidBrush(tabDataForeColor), xpos, startY, sf);
                startY += 10;
                string taxtype = Properties.Settings.Default.Tax_Type.ToString();
                e.Graphics.DrawString("TRN:" + company.TIN_No, printFont, new SolidBrush(tabDataForeColor), xpos, startY, sf);
                startY += 15;
                e.Graphics.DrawString("TAX INVOICE", Font2, new SolidBrush(tabDataForeColor), xpos, startY, sf);
                startY += 10;
                e.Graphics.DrawString("-----------------------------------------------", printFont, new SolidBrush(Color.Black), xpos, startY, sf);
                e.Graphics.DrawString("-----------------------------------------------", printFont, new SolidBrush(Color.Black), xpos, startY + 2, sf);
                startY += 10;

                string customername = "";
                if (CUSTOMER_NAME.Text == "")
                {
                    customername = "Cash";
                }
                else
                {
                    customername = CUSTOMER_NAME.Text;
                }
                e.Graphics.DrawString("Customer:" + customername, printFont, new SolidBrush(tabDataForeColor), startX, startY);
                startY += 10;
                e.Graphics.DrawString("Inv No:" + VOUCHNUM.Text, printFont, new SolidBrush(tabDataForeColor), startX, startY);
                startY += 10;
                e.Graphics.DrawString("Date:" + DOC_DATE_GRE.Value.ToShortDateString(), printFont, new SolidBrush(tabDataForeColor), startX, startY);
                Font itemhead = new Font("Courier New", 8);
                startY += 10;
                e.Graphics.DrawString("---------------------------------------------------", printFont, new SolidBrush(Color.Black), 0, startY);
                string headtext = "Sl".PadRight(3) + "Item".PadRight(21) + "Qty".PadRight(5) + "Rate".PadRight(6) + "Total";
                e.Graphics.DrawString(headtext, itemhead, new SolidBrush(Color.Black), startX, startY + 8);
                startY += 15;
                e.Graphics.DrawString("---------------------------------------------------", printFont, new SolidBrush(Color.Black), 0, startY);
                startY += 15;
                Font font = new Font("Times New Roman", 8);
                Font font1 = new Font("Times New Roman", 7);
                float fontheight = font.GetHeight();

                try
                {
                    int i = 1;
                    foreach (DataGridViewRow row in dgItems.Rows)
                    {

                        string name = "";// row.Cells[1].Value.ToString().Length <= 28 ? row.Cells[1].Value.ToString() : row.Cells[1].Value.ToString().Substring(0, 28);
                        string qty = row.Cells[5].Value.ToString();
                        string price = row.Cells["cTotal"].Value.ToString();
                      //  string rate = (Convert.ToDecimal(price) / Convert.ToDecimal(qty)).ToString();
                        string rate = row.Cells["CPrice"].Value.ToString();
                        string tot = (Convert.ToDecimal(rate) * Convert.ToDecimal(qty)).ToString();
                        string productline = name + qty + rate + price;
                        //string qty = row.Cells[5].Value.ToString();
                        //string rate = row.Cells["cPrice"].Value.ToString();
                        //string price = row.Cells[11].Value.ToString();
                        //string productline = name + qty + rate + price;
                        //e.Graphics.DrawString(i.ToString(), font, new SolidBrush(Color.Black), startX+12, startY,format);
                        //e.Graphics.DrawString(name, font, new SolidBrush(Color.Black), startX+14, startY);
                        //e.Graphics.DrawString(qty, font1, new SolidBrush(Color.Black), startX+187, startY,format);
                        //e.Graphics.DrawString(rate, font1, new SolidBrush(Color.Black), startX + 230, startY, format);
                        //e.Graphics.DrawString(price, font, new SolidBrush(Color.Black), startX + 272, startY, format);
                        //e.Graphics.DrawString("SR", font1, new SolidBrush(Color.Black), startX + 275, startY);
                        if (row.Cells[1].Value.ToString().Length <= 28)
                        {
                            // Convert.ToDouble(rate)).ToString("N2")
                            e.Graphics.DrawString(i.ToString(), font, new SolidBrush(Color.Black), startX + 12, startY, format);
                            e.Graphics.DrawString(row.Cells[1].Value.ToString(), font1, new SolidBrush(Color.Black), startX + 14, startY);
                            e.Graphics.DrawString(qty, font1, new SolidBrush(Color.Black), startX + 187, startY, format);
                            e.Graphics.DrawString((Convert.ToDecimal(rate)).ToString("N2"), font1, new SolidBrush(Color.Black), startX + 230, startY, format);
                            e.Graphics.DrawString(tot, font, new SolidBrush(Color.Black), startX + 272, startY, format);
                          //  e.Graphics.DrawString(currencyTable.Rows[0]["CODE"].ToString(), font1, new SolidBrush(Color.Black), startX + 275, startY);
                        }
                        else
                        {
                            int lngth = row.Cells[1].Value.ToString().Length;
                            if (lngth < 50)
                                name = row.Cells[1].Value.ToString();
                            else
                                name = row.Cells[1].Value.ToString().Substring(0, 50);
                            e.Graphics.DrawString(i.ToString(), font, new SolidBrush(Color.Black), startX + 12, startY, format);
                            e.Graphics.DrawString(name, font1, new SolidBrush(Color.Black), startX + 14, startY);
                            startY += (int)fontheight + 5;
                            if (lngth > 50)
                            {

                                int tobePrint = 28;
                                if (lngth < 78)
                                    tobePrint = lngth - 50;
                                name = row.Cells[1].Value.ToString().Substring(50, tobePrint);
                                e.Graphics.DrawString(name, font1, new SolidBrush(Color.Black), startX + 14, startY);
                            }
                            e.Graphics.DrawString(qty, font1, new SolidBrush(Color.Black), startX + 187, startY, format);
                            e.Graphics.DrawString((Convert.ToDecimal(rate)).ToString("N2"), font1, new SolidBrush(Color.Black), startX + 230, startY, format);
                            e.Graphics.DrawString(tot, font, new SolidBrush(Color.Black), startX + 272, startY, format);
                          //  e.Graphics.DrawString(currencyTable.Rows[0]["CODE"].ToString(), font1, new SolidBrush(Color.Black), startX + 275, startY);

                        }
                        i++;
                        startY += (int)fontheight + 5;
                    }
                }
                catch
                {

                }

                startY += 10;
                e.Graphics.DrawString("-----------------------------------------------", printFont, new SolidBrush(Color.Black), 0, startY);

                startY += 15;

                try
                {
                    string baseunit = "", subunit = "";
                    baseunit = currencyTable.Rows[0]["SUPERUNIT"].ToString();
                    subunit = currencyTable.Rows[0]["SUBUNIT"].ToString();
                    int cash = (int)Convert.ToDouble(NET_AMOUNT.Text);
                    string cas = NET_AMOUNT.Text;
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
                        test3 = test + " "+baseunit+" and " + test2 + " "+subunit+" only";
                        int index = test3.IndexOf(baseunit);
                    }
                    if (i1 > 0 && i2 == 0)
                    {
                        string test = NumbersToWords(i1);
                        test3 = test + " "+baseunit+" only";
                    }
                    //e.Graphics.DrawString("Amount in words :" + " " + test3, font, new SolidBrush(Color.Black), startX, startY);
                    e.Graphics.DrawString(test3, font1, new SolidBrush(Color.Black), startX, startY-2);


                }
                catch
                {
                }




                string grosstotal = "Gross Total:";
                string TAXP = "";
                if (taxtype == "GST")
                    TAXP = "6%";
                else
                    TAXP = "5%";
                string vatstring = "Tax Amount:" + TAXP;
                string Discountstring = "Discount:";
                string total = "Total:";
                startY += 10;
                e.Graphics.DrawString(grosstotal, font, new SolidBrush(Color.Black), startX + 120, startY);

                e.Graphics.DrawString(Spell.SpellAmount.comma(Convert.ToDecimal(TOTAL_AMOUNT.Text) - Convert.ToDecimal(TAX_TOTAL.Text)), font, new SolidBrush(Color.Black), startX + 250, startY, format);
                startY += 10;
                 e.Graphics.DrawString("VAT Amount " + TAXP + ":", font, new SolidBrush(Color.Black), startX + 120, startY);
                 e.Graphics.DrawString(Spell.SpellAmount.comma(Convert.ToDecimal(TAX_TOTAL.Text)), font, new SolidBrush(Color.Black), startX + 250, startY, format);

                startY += 10;
                e.Graphics.DrawString(Discountstring, font, new SolidBrush(Color.Black), startX + 120, startY);

                e.Graphics.DrawString(Spell.SpellAmount.comma(Convert.ToDecimal(DISCOUNT.Text)), font, new SolidBrush(Color.Black), startX + 250, startY, format);

                startY += 10;
                e.Graphics.DrawString("---------------------------------------", font, new SolidBrush(Color.Black), startX + 120, startY);

                startY += 10;
                e.Graphics.DrawString(total, font, new SolidBrush(Color.Black), startX + 120, startY);
              
                e.Graphics.DrawString(Spell.SpellAmount.comma(Convert.ToDecimal(NET_AMOUNT.Text)), font, new SolidBrush(Color.Black), startX + 250, startY, format);
                e.Graphics.DrawString(currencyTable.Rows[0]["CODE"].ToString(), font, new SolidBrush(Color.Black), startX + 250, startY);
                startY += 15;
                e.Graphics.DrawString("---------------------------------------------------", printFont, new SolidBrush(Color.Black), 0, startY);
                e.Graphics.DrawString("***Tank You For Visit***", printFont, new SolidBrush(Color.Black), xpos, startY + 15, sf);
                e.Graphics.DrawString("---------------------------------------------------", printFont, new SolidBrush(Color.Black), 0, startY + 15);

            }

            e.HasMorePages = false;

        }
        void printDocument_PrintPage(object sender, PrintPageEventArgs e)
        {


            float xpos;
            int startx = 10;
            int starty = 30;
            int offset = 15;
            if (PrintPage.SelectedIndex == 1)
            {

                // Printing.PrintA4();
            }
            else
            {

                int w = e.MarginBounds.Width / 2;
                int x = e.MarginBounds.Left;
                int y = e.MarginBounds.Top;
                Font printFont = new Font("Courier New", 8);
                var tabDataForeColor = Color.Black;
                int height = 100 + y;


                var txtDataWidth = e.Graphics.MeasureString(CompanyName, printFont).Width;


                using (var sf = new StringFormat())
                {
                    height += 15;
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Alignment = StringAlignment.Center;

                    xpos = e.MarginBounds.Left + (e.MarginBounds.Width / 2);

                    e.Graphics.DrawString(CompanyName, printFont, new SolidBrush(tabDataForeColor), xpos, starty, sf);
                    e.Graphics.DrawString(Address1, printFont, new SolidBrush(tabDataForeColor), xpos, starty + offset, sf);
                    offset = offset + 10;
                    e.Graphics.DrawString("CASH BILL :-" + Billno, printFont, new SolidBrush(tabDataForeColor), xpos, starty + offset, sf);
                    offset = offset + 10;

                    e.Graphics.DrawString("-----------------------------------------------", printFont, new SolidBrush(Color.Black), xpos, starty + offset, sf);
                    e.Graphics.DrawString("-----------------------------------------------", printFont, new SolidBrush(Color.Black), xpos, starty + offset + 3, sf);

                    offset = offset + 10;
                    e.Graphics.DrawString("Tin No:" + TineNo, printFont, new SolidBrush(tabDataForeColor), startx, starty + offset);
                    offset = offset + 12;
                    e.Graphics.DrawString("Customer:" + CUSTOMER_NAME.Text, printFont, new SolidBrush(tabDataForeColor), startx, starty + offset);
                    Font itemhead = new Font("Courier New", 8);
                    offset = offset + 12;
                    e.Graphics.DrawString("-----------------------------------------------", printFont, new SolidBrush(Color.Black), startx, starty + offset);
                    offset = offset + 12;

                    string headtext = "Item".PadRight(20) + "Tax%".PadRight(5) + "Qty".PadRight(5) + "Rate".PadRight(10) + "Total";
                    e.Graphics.DrawString(headtext, itemhead, new SolidBrush(Color.Black), startx, starty + offset - 1);
                    e.Graphics.DrawString("-----------------------------------------------", printFont, new SolidBrush(Color.Black), startx, starty + offset + 7);
                    offset = offset + 15;
                    Font font = new Font("Courier New", 8);
                    float fontheight = font.GetHeight();
                    try
                    {
                        foreach (DataGridViewRow row in dgItems.Rows)
                        {
                            string name = row.Cells[1].Value.ToString().Length <= 18 ? row.Cells[1].Value.ToString().PadRight(20) : row.Cells[1].Value.ToString().Substring(0, 18).PadRight(20);
                            //string name = row.Cells[1].Value.ToString().PadRight(20);
                            string tax = row.Cells[7].Value.ToString().PadRight(5);
                            string qty = row.Cells[5].Value.ToString().PadRight(5);
                            string rate = row.Cells[6].Value.ToString().PadRight(10);
                            string price = row.Cells[11].Value.ToString();
                            string productline = name + tax + qty + rate + price;
                            e.Graphics.DrawString(productline, font, new SolidBrush(Color.Black), startx, starty + offset);
                            offset = offset + (int)fontheight + 5;
                        }
                    }
                    catch
                    {

                    }


                    e.Graphics.DrawString("-----------------------------------------------", printFont, new SolidBrush(Color.Black), startx, starty + offset);
                    offset = offset + 12;
                    string grosstotal = "Gros Total:".PadRight(5) + Spell.SpellAmount.comma(Convert.ToDecimal(TOTAL_AMOUNT.Text));
                    string vatstring = "Tax Amount:".PadRight(5) + Spell.SpellAmount.comma(Convert.ToDecimal(TAX_TOTAL.Text));
                    string Discountstring = "Discount:".PadRight(5) + Spell.SpellAmount.comma(Convert.ToDecimal(DISCOUNT.Text));
                    string total = "Total:".PadRight(5) + Spell.SpellAmount.comma(Convert.ToDecimal(NET_AMOUNT.Text));

                    e.Graphics.DrawString(grosstotal, font, new SolidBrush(Color.Black), startx + 200, starty + offset + 3);
                    offset = offset + (int)fontheight + 5;
                    e.Graphics.DrawString(vatstring, font, new SolidBrush(Color.Black), startx + 200, starty + offset + 3);
                    offset = offset + (int)fontheight + 4;
                    e.Graphics.DrawString(Discountstring, font, new SolidBrush(Color.Black), startx + 200, starty + offset + 3);
                    offset = offset + (int)fontheight + 4;
                    e.Graphics.DrawString("---------------", font, new SolidBrush(Color.Black), startx + 200, starty + offset + 3);
                    offset = offset + (int)fontheight + 4;
                    e.Graphics.DrawString(total, font, new SolidBrush(Color.Black), startx + 200, starty + offset + 3);

                    offset = offset + (int)fontheight + 5;


                    try
                    {
                        string test = Spell.SpellAmount.InWrods(Convert.ToDecimal(NET_AMOUNT.Text));

                        int index = test.IndexOf("Taka ");
                        int l = test.Length;
                        test = test.Substring(index + 4);

                        e.Graphics.DrawString(test, font, new SolidBrush(Color.Black), startx, starty + offset + 3);
                    }
                    catch
                    {
                    }





                    offset = offset + 15;
                    if (txtcashrcvd.Text != "")
                    {
                        try
                        {
                            decimal balance = Convert.ToDecimal(txtcashrcvd.Text) - Convert.ToDecimal(TOTAL_AMOUNT.Text);
                            e.Graphics.DrawString("Cash Rcvd:" + Spell.SpellAmount.comma(Convert.ToDecimal(txtcashrcvd.Text)) + "   " + "Balance:" + Spell.SpellAmount.comma(Convert.ToDecimal(balance)).ToString(), font, new SolidBrush(Color.Black), startx, starty + offset);

                            offset = offset + 12;

                        }
                        catch
                        {
                        }
                    }


                }

                e.HasMorePages = false;

            }

        }
        
        private Font FindBestFitFont(Graphics g, String text, Font font, Size proposedSize)
        {
            // Compute actual size, shrink if needed
            while (true)
            {
                SizeF size = g.MeasureString(text, font);

                // It fits, back out
                if (size.Height <= proposedSize.Height &&
                     size.Width <= proposedSize.Width) { return font; }

                // Try a smaller font (90% of old size)
                Font oldFont = font;
                font = new Font(font.Name, (float)(font.Size * .9), font.Style);
                oldFont.Dispose();
            }
        }
        
        string shopname = "Sys-Sols Gamrents";
        
        public void PrintingInitial()
        {
            if (dgItems.Rows.Count >= 1)
            {
                printingrecipt();
            }
            else
            {
                if (DialogResult.Yes == MessageBox.Show("Do You Want to create an empty bill", "alert", MessageBoxButtons.YesNo))
                {
                    printingrecipt();
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            PrintingInitial();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgItems.Rows.Count > 0 && dgItems.CurrentRow != null && MessageBox.Show("Are you sure to delete the Sales?", "Stock Deletion", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string dat = "";
                string id = ID;
                dat = Convert.ToDateTime(DOC_DATE_GRE.Value).ToString("MM/dd/yyyy");

                SqlCommand reduceStockCommand = new SqlCommand();
                reduceStockCommand.Connection = conn;
                conn.Open();
                reduceStockCommand.CommandText = "SELECT ITEM_CODE, QUANTITY, UOM_QTY, cost_price,PRICE_BATCH FROM INV_SALES_DTL WHERE DOC_ID = '" + VOUCHNUM.Text + "' AND DOC_TYPE = '" + type + "'";
                SqlDataReader r = reduceStockCommand.ExecuteReader();
                StockEntry se = new StockEntry();
                while (r.Read())
                {
                    double qty = (Convert.ToDouble(r["QUANTITY"]) * Convert.ToDouble(r["UOM_QTY"]));
                    if (type.Equals("SAL.CSR"))
                    {
                        qty = -1 * qty;
                    }
                    se.addStockWithBatch(Convert.ToString(r["ITEM_CODE"]), Convert.ToString(qty), Convert.ToString(r["cost_price"]), Convert.ToString(r["PRICE_BATCH"]));
                }
                conn.Close();

                conn.Open();
                cmd = conn.CreateCommand();
                //cmd.CommandText = "UPDATE  INV_SALES_HDR SET FLAGDEL='FALSE' WHERE DOC_NO ='" + id + "';UPDATE  INV_SALES_DTL SET FLAGDEL='FALSE' WHERE DOC_NO ='" + id + "'";
                cmd.CommandText = "DELETE FROM INV_SALES_HDR WHERE DOC_NO ='" + id + "';DELETE FROM  INV_SALES_DTL WHERE DOC_NO ='" + id + "'";
                cmd.ExecuteNonQuery();
                MessageBox.Show("Item Deleted!");
                conn.Close();
                dgItems.Rows.Remove(dgItems.CurrentRow);
                AddtoDeletedTransaction(id);

                modifiedtransaction(id, dat);
                DeleteTransation(id);
                btnClear.PerformClick();

                //    DeleteTransation(dgItems.CurrentRow.Cells[0].Value.ToString());
                checkvoucher(Convert.ToInt16(VOUCHNUM.Text));
            }
        }
        
        public void AddtoDeletedTransaction(string id)
        {
            string vchr;
            if (type == "SAL.CSR")
            {
                vchr = "Sales Return";
            }
            else
            {
                vchr = "SALES Normal";
            }
            conn.Open();
            cmd.CommandText = "insert into     tbl_deletedTransaction(VOUCHERNO, VOUCHERTYPE, ACCNAME, PARTICULARS, DEBIT, CREDIT, DATED, NARRATION, USERID, SYSTEMTIEM, ACCID) select    VOUCHERNO, VOUCHERTYPE, ACCNAME, PARTICULARS, DEBIT, CREDIT, DATED, NARRATION, USERID, SYSTEMTIEM, ACCID from tb_Transactions where VOUCHERNO='" + id + "' and VOUCHERTYPE='" + vchr + "'";
            // cmd.CommandText = "DELETE FROM INV_PURCHASE_HDR WHERE DOC_NO = '" + id + "';DELETE FROM INV_PURCHASE_DTL WHERE DOC_NO = '" + id + "';DELETE FROM INV_STK_TRX_HDR WHERE DOC_REFERENCE = '" + id + "';DELETE FROM INV_STK_TRX_DTL WHERE DOC_REFERENCE = '" + id + "'";
            cmd.ExecuteNonQuery();
            //  MessageBox.Show("Record Deleted!");
            conn.Close();

        }
        
        public void modifiedtransaction(string ID, string date)
        {
            try
            {
                if (type == "SAL.CSR")
                {
                    modtrans.VOUCHERTYPE = "Sales Return";
                }
                else
                {
                    modtrans.VOUCHERTYPE = "SALES Normal";
                }
                modtrans.Date = date;
                modtrans.USERID = lg.EmpId;
                modtrans.VOUCHERNO = ID;
                modtrans.NARRATION = "";
                modtrans.STATUS = "Delete";
                modtrans.MODIFIEDDATE = DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss"); ;
                modtrans.INVOICENO = VOUCHNUM.Text;
                modtrans.BRANCH = lg.Branch;
                modtrans.insertTransaction();
            }
            catch
            { }
        }
        
        public void DeleteTransation(string Id)
        {
            try
            {
                if (type == "SAL.CSR")
                {
                    trans.VOUCHERTYPE = "Sales Return";
                }
                else
                {
                    trans.VOUCHERTYPE = "SALES "+cmbInvType.Text;
                }
                trans.VOUCHERNO = Id;
                trans.DeletePurchaseTransaction();
            }
            catch
            {
            }

        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            try
            {
                if (closeFrom)
                {
                    this.Close();
                }
                else
                {
                    if (ActiveForm)
                    {
                        if (DialogResult.Yes == MessageBox.Show("Are you sure to cancel this bill", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
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
                    }
                    else
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
                }
            }
            catch
            {
                this.Close();
            }


        }

        private void linkRefresh_LinkClicked(object sender, EventArgs e)
        {
            getDetails();
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            if (txtFilter.Text == "")
            {
                src.Filter = "";
            }
            else
            {
                src.Filter = "[" + cmbFilter.Text + "] LIKE '" + txtFilter.Text + "%'";
            }
        }

        private void UOM_SelectedIndexChanged(object sender, EventArgs e)
        {
         //  getRate();    
            if (UOM.SelectedIndex > -1)
            {
                try
                {

                    PRICE.Text = ((sales_price * Convert.ToDouble(UOM.SelectedValue))*Convert.ToDouble(QUANTITY.Text)).ToString();
                }
                catch
                {
                }
            }
        }

        private void getRate()
        {
            if (RATE_CODE.SelectedValue != null)
            {
                conn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GET_RATE";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ITEM_CODE", ITEM_CODE.Text);
                cmd.Parameters.AddWithValue("@UNIT_CODE", UOM.Text);
                cmd.Parameters.AddWithValue("@RATE_CODE", RATE_CODE.SelectedValue);
                string price = Convert.ToString(cmd.ExecuteScalar());
                // PRICE.Text = price.ToString();
                if (price != "0")
                {
                    double pricedec = Convert.ToDouble(price);
                    PRICE.Text = pricedec.ToString("N3");
                }
                else
                {
                    PRICE.Text = price.ToString();
                }
                conn.Close();
                cmd.CommandType = CommandType.Text;
            }
        }

        private void getBarcodeRate(string barco)
        {
            conn.Open();
            cmd.CommandText = "SELECT Sale_Price from RateChange WHERE R_Id='" + barco + "'";
            string price = Convert.ToString(cmd.ExecuteScalar());
            // PRICE.Text = price.ToString();
            if (price != "0")
            {
                double pricedec = Convert.ToDouble(price);
                PRICE.Text = pricedec.ToString("N3");
            }
            else
            {
                PRICE.Text = price.ToString();
            }
            conn.Close();
        }
        
        private void BATCH_Enter(object sender, EventArgs e)
        {
            assignBatch();
        }

        private void assignBatch()
        {
            try
            {

                BatchHelp h = new BatchHelp(ITEM_CODE.Text);


                if (h.ShowDialog() == DialogResult.OK && h.c != null)
                {
                    BATCH.Text = Convert.ToString(h.c["BATCH"].Value);
                    //try
                    //{

                    //    EXPIRY_DATE.Value = DateTime.ParseExact(Convert.ToString(h.c["EXPIRY_DATE"].Value), "fr-FR", null);
                    //}
                    //catch { }

                    try
                    {
                        DateTime result;
                        string test = h.c["EXPIRY_DATE"].Value.ToString();
                        DateTime dttest = Convert.ToDateTime(test);
                        EXPIRY_DATE.Value = dttest;
                    }
                    catch { }

                    getRate();
                }
            }
            catch
            {
            }
        }

        private void RATE_CODE_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(Convert.ToString(RATE_CODE.SelectedValue));
            getRate();
            if (RATE_CODE.Text.StartsWith("MRP"))
            {
                double taxcalc = 0;
                if (!string.IsNullOrEmpty(ITEM_TAX_PER.Text))
                {
                    taxcalc = (Convert.ToDouble(ITEM_TAX_PER.Text) / 100) + 1;
                    PRICE.Text = (Convert.ToDouble(PRICE.Text) / taxcalc).ToString(decimalFormat);
                }
            }
            /*if (ShowStock)
            {
                bindgridview();
            }*/
        }
        
        public string barcodeConvert(string rcode)
        {
            string ret = "";
            conn.Open();
            cmd.CommandType = CommandType.Text;

            cmd.Connection = conn;
            cmd.CommandText = "SELECT Item_code from RateChange  where R_Id='" + rcode + "'";
            string price = Convert.ToString(cmd.ExecuteScalar()); ;
            ret = price;
            conn.Close();
            return ret;
        }
        
        private void BARCODE_KeyDown(object sender, KeyEventArgs e)
        {

            if (BARCODE.Text != "")
            {
                if (e.KeyCode == Keys.Enter)
                {

                    //PNLSERIAL.Visible = true;
                    //kryptonLabel36.Visible = true;
                    //SERIALNO.Visible = true;
                    string co = BARCODE.Text;
                    //int len = Convert.ToInt16((co.Length - 1));
                    //string Bat = co.Remove(0, 7);
                    DataRow[] dr = null; ;
                    DataTable dt1 = null;
                    DataTable t = new DataTable();
                    dgBatch.DataSource = "";
                    dr = table_for_batch.Select("batch_id = '" + co + "'");

                   
                        if(dr.Length>0)
                        {
                        t = dr.CopyToDataTable();
                        }
                       else
                        {
                 
                        //BARCODE.Text = "";
                        //if (SalebyItemName)
                        //    ITEM_NAME.Focus();
                        //else if (SalebyItemCode)
                        //    ITEM_CODE.Focus();
                        //else if (SalebyBarcode)
                        //    BARCODE.Focus();
                        //else
                        //    ITEM_NAME.Focus();
                        DataTable tt = General.Product4mBarcode(BARCODE.Text);
                        if (tt.Rows.Count > 0)
                        {
                            ITEM_CODE.Text = tt.Rows[0][0].ToString();
                           // ITEM_NAME.Text = tt.Rows[0][1].ToString();
                            dr = table_for_batch.Select("ITEM_CODE = '" + ITEM_CODE.Text + "' AND STOCK >0", "STOCK asc");
                            if (dr.Length>0)
                            {
                                t = dr.CopyToDataTable();
                            }
                            else
                            {
                                if ((StockOut = General.IsEnabled(Settings.StockOut)))
                                {
                                    int s = Convert.ToInt32(table_for_batch.Compute("MAX(batch_increment)", "ITEM_CODE = '" + ITEM_CODE.Text + "'"));
                                    dr = table_for_batch.Select("batch_increment = '" + s + "' AND ITEM_CODE = '" + ITEM_CODE.Text + "'");
                                    if (dr.Length > 0)
                                    {
                                        t = dr.CopyToDataTable();
                                    }
                                }

                            }
                            ITEM_CODE.Text = "";
                        }
                      }
                  //  General.Product4mBarcode(barcodeConvert(Bat));
                    if (t.Rows.Count > 0)
                    {
                        if (t.Rows.Count == 1)
                        {
                           ITEM_CODE.Text = t.Rows[0][0].ToString();
                           ITEM_NAME.Text = t.Rows[0][1].ToString();

                           try
                           {
                               if (t.Rows[0]["STOCK"].ToString() != "" )
                                   lblstock.Text = t.Rows[0]["STOCK"].ToString();
                               else
                                   lblstock.Text = t.Rows[0]["STOCK"].ToString();
                           }
                           catch
                           {
                           }

                            string itemcode = t.Rows[0]["ITEM_CODE"].ToString();
                            ITEM_CODE.Text = itemcode;

                            //NOT VALIDATED//
                            BARCODE.Text = t.Rows[0]["BATCH_ID"].ToString();

                            //NOT VALIDATED//
                            if (t.Rows[0]["HSN"].ToString() != "")
                            {
                                txt_HSN.Text = t.Rows[0]["HSN"].ToString();
                            }
                            PurchasePrice = Convert.ToDecimal(t.Rows[0]["PUR"]);
                            String rateType = Convert.ToString(RATE_CODE.SelectedValue);
                            string pricedecimal = t.Rows[0][rateType].ToString();
                            double pricedec = Convert.ToDouble(pricedecimal);
                            //PRICE.Text = pricedec.ToString("N3");
                            PRICE.Text = t.Rows[0][rateType].ToString();
                            //PRICE.Text = (dataGridItem.CurrentRow.Cells["SALES"].Value.ToString("N3");
                            HASSERIAL = Convert.ToBoolean(t.Rows[0]["HASSERIAL"]);
                            PNLSERIAL.Visible = HASSERIAL;
                            mrp = Convert.ToDouble(t.Rows[0]["MRP"]);
                            if (hasBatch)
                            {
                                if (t.Rows[0]["BATCH CODE"].ToString() != "")
                                    BATCH.Text = t.Rows[0]["BATCH CODE"].ToString();
                                if (t.Rows[0]["EXPIRY DATE"].ToString() != null && t.Rows[0]["EXPIRY DATE"].ToString() != "")
                                    EXPIRY_DATE.Value = Convert.ToDateTime(t.Rows[0]["EXPIRY DATE"]);
                            }
                            tb_mrp.Text = mrp.ToString(decimalFormat);



                            this.UOM.SelectedIndexChanged -= new EventHandler(UOM_SelectedIndexChanged);
                            addUnits();
                            this.UOM.SelectedIndexChanged -= new EventHandler(UOM_SelectedIndexChanged);
                            UOM.Text = t.Rows[0]["UNIT_CODE"].ToString();
                            TaxId = Convert.ToInt16(t.Rows[0]["TaxId"].ToString());
                            GetTaxRate();

                            GetDiscount();
                            ItemArabicName = t.Rows[0]["DESC_ARB"].ToString();
                            ITEM_NAME.Text = t.Rows[0]["ITEM NAME"].ToString();
                            PNL_DATAGRIDITEM.Visible = false;
                            itemSelected = false;
                            pricefob = Convert.ToDouble(PRICE.Text);
                            if (RATE_CODE.Text.StartsWith("MRP"))
                            {
                                double taxcalc = 0;
                                taxcalc = (Convert.ToDouble(ITEM_TAX_PER.Text) / 100) + 1;
                                PRICE.Text = (Convert.ToDouble(PRICE.Text) / taxcalc).ToString(decimalFormat);
                            }
                            else if (!hasSaleExclusive)
                            {
                                double taxcalc = 0;
                                taxcalc = (Convert.ToDouble(ITEM_TAX_PER.Text) / 100) + 1;
                                PRICE.Text = (Convert.ToDouble(PRICE.Text) / taxcalc).ToString();
                            }








                            QUANTITY.Text = "1";
                            if (SalebyItemCode)
                                ITEM_CODE.Focus();
                            else if (MoveToUnit)
                                UOM.Focus();
                            else if (MoveToQty)
                                QUANTITY.Focus();
                            else if (HASSERIAL)
                                SERIALNO.Focus();
                            else if (txtfree.Visible)
                                txtfree.Focus();
                            else if (MoveToPrice)
                                PRICE.Focus();
                            else if (tb_mrp.Visible)
                                tb_mrp.Focus();
                            else if (GROSS_TOTAL.Visible)
                                GROSS_TOTAL.Focus();
                            else if (MoveToDisc)
                                ITEM_DISCOUNT.Focus();
                            else if (tb_netvalue.Visible)
                                tb_netvalue.Focus();
                            else if (tb_descr.Visible)
                                tb_descr.Focus();
                            else if (hasTax && MoveToTaxper)
                            {
                                if (MoveToTaxper)
                                    ITEM_TAX_PER.Focus();
                            }
                            else
                            {
                                addItem();
                                clearItem();
                                if (SalebyItemName)
                                    ITEM_NAME.Focus();
                                else if (SalebyItemCode)
                                    ITEM_CODE.Focus();
                                else if (SalebyBarcode)
                                    BARCODE.Focus();
                                else
                                    ITEM_NAME.Focus();
                            }
                            addUnits();
                            //UOM.Text = t.Rows[0][2].ToString();
                            //getBarcodeRate(Bat);
                            //wrty_period = dataGridItem.CurrentRow.Cells["PERIOD"].Value.ToString();
                            //wrty_type = dataGridItem.CurrentRow.Cells["PERIODTYPE"].Value.ToString();
                            BarcodeFlag = true;
                        }
                        else
                        {
                            PNL_DATAGRIDITEM.Visible = true;
                            dgBatch.Visible=true;
                            dgBatch.DataSource = t;
                            dgBatch.Focus();
                            dgBatch.CurrentCell = dgBatch[3, 0] ;
                            Common_columns_false();
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
                if (ShowPurchase)
                {
                  dgBatch.Columns["PUR"].Visible = true;
                }
                else
                {
                    dgBatch.Columns["PUR"].Visible = false;

                }
              //  dgBatch.Col;
               // this.dgBatch.EnableHeadersVisualStyles = false;
               // this.dgBatch.ColumnHeadersHeight = 60;
               //int a= dgBatch.ColumnHeadersHeight;
                        }
      
                        }
                  }
                    else if (e.KeyData == Keys.Enter)
                    {
                        if (SalebyItemName)
                            ITEM_NAME.Focus();
                        else if (SalebyItemCode)
                            ITEM_CODE.Focus();
                        else if (SalebyBarcode)
                        {
                            
                            BARCODE.Focus();
                            BARCODE.SelectAll();
                        }
                        else
                            ITEM_NAME.Focus();
                    }
                }
            }
            else if (e.KeyData == Keys.Enter)
            {
                ITEM_NAME.Focus();
            }
        }

        private void Sales_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void Sales_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.P && e.Control)
            {
                btnPrint.PerformClick();
            }            
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            PrintForDotMatrix(decSalesMasterId);
          
        }

        public void InsertIntoCreditTable()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "insert into tbl_CreditNote(CN_Doc_No,CN_Date,CN_DateHij,CN_Reffrence_No,CUSTOMER_CODE,CUSTOMER_NAME_ENG,NOTES,CN_Balance,Nett_Amount,Status)values('" + DOC_NO.Text + "','" + DOC_DATE_GRE.Text + "','" + DOC_DATE_HIJ.Text + "','" + DOC_REFERENCE.Text + "','" + CUSTOMER_CODE.Text + "','" + CUSTOMER_NAME.Text + "','" + NOTES.Text + "','" + balance + "','" + NET_AMOUNT.Text + "','" + status + "')";
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void PrintForDotMatrix(decimal decSalesMasterId)
        {
            //  try
            //  {
            //      DataTable dtblOtherDetails = new DataTable();
            //      Class.CompanySetup spCompany = new Class.CompanySetup();
            //   //  CompanySP spComapany = new CompanySP();
            //      dtblOtherDetails = spCompany.getCompanyDetails();
            //      DataTable dtblGridDetails = new DataTable();
            //      dtblGridDetails.Columns.Add("SlNo");
            //      dtblGridDetails.Columns.Add("BarCode");
            //      dtblGridDetails.Columns.Add("ProductCode");
            //      dtblGridDetails.Columns.Add("ProductName");
            //      dtblGridDetails.Columns.Add("Qty");
            //      dtblGridDetails.Columns.Add("Unit");
            //      dtblGridDetails.Columns.Add("Godown");
            //      dtblGridDetails.Columns.Add("Brand");
            //      dtblGridDetails.Columns.Add("Tax");
            //      dtblGridDetails.Columns.Add("TaxAmount");
            //      dtblGridDetails.Columns.Add("NetAmount");
            //      dtblGridDetails.Columns.Add("DiscountAmount");
            //      dtblGridDetails.Columns.Add("DiscountPercentage");
            //      dtblGridDetails.Columns.Add("SalesRate");
            //      dtblGridDetails.Columns.Add("PurchaseRate");
            //      dtblGridDetails.Columns.Add("MRP");
            //      dtblGridDetails.Columns.Add("Rack");
            //      dtblGridDetails.Columns.Add("Batch");
            //      dtblGridDetails.Columns.Add("Rate");
            //      dtblGridDetails.Columns.Add("Amount");
            //      int inRowCount = 0;
            //      foreach (DataGridViewRow dRow in dgItems.Rows)
            //      {
            //          if (!dRow.IsNewRow)
            //          {
            //              DataRow dr = dtblGridDetails.NewRow();
            //              dr["SlNo"] = ++inRowCount;
            //              //if (dRow.Cells["dgvtxtBarcode"].Value != null)
            //              //{
            //              //    dr["BarCode"] = dRow.Cells["dgvtxtBarcode"].Value.ToString();
            //              //}
            //              if (dRow.Cells["cCode"].Value != null)
            //              {
            //                  dr["ProductCode"] = dRow.Cells["cCode"].Value.ToString();
            //              }
            //              if (dRow.Cells["cName"].Value != null)
            //              {
            //                  dr["ProductName"] = dRow.Cells["cName"].Value.ToString();
            //              }
            //              if (dRow.Cells["cQty"].Value != null)
            //              {
            //                  dr["Qty"] = dRow.Cells["cQty"].Value.ToString();
            //              }
            //              if (dRow.Cells["cUnit"].Value != null)
            //              {
            //                  dr["Unit"] = dRow.Cells["cUnit"].Value.ToString();
            //              }
            //              if (dRow.Cells["cPrice"].Value != null)
            //              {
            //                  dr["Rate"] = dRow.Cells["cPrice"].Value.ToString();
            //              }
            //              if (dRow.Cells["cGTotal"].Value != null)
            //              {
            //                  dr["Amount"] = dRow.Cells["cGTotal"].Value.ToString();
            //              }
            //              if (dRow.Cells["cTaxAmt"].Value != null)
            //              {
            //                  dr["TaxAmount"] = dRow.Cells["cTaxAmt"].Value.ToString();
            //              }
            //              if (dRow.Cells["cTotal"].Value != null)
            //              {
            //                  dr["NetAmount"] = dRow.Cells["cTotal"].Value.ToString();
            //              }
            //              if (dRow.Cells["cDisc"].Value != null)
            //              {
            //                  dr["DiscountAmount"] = dRow.Cells["cDisc"].Value.ToString();
            //              }
            //              dtblGridDetails.Rows.Add(dr);
            //          }
            //      }
            //      dtblOtherDetails.Columns.Add("voucherNo");
            //      dtblOtherDetails.Columns.Add("date");
            //      dtblOtherDetails.Columns.Add("ledgerName");
            //      dtblOtherDetails.Columns.Add("SalesMode");
            //      dtblOtherDetails.Columns.Add("SalesAccount");
            //      dtblOtherDetails.Columns.Add("SalesMan");
            //      dtblOtherDetails.Columns.Add("CreditPeriod");
            //      dtblOtherDetails.Columns.Add("VoucherType");
            //      dtblOtherDetails.Columns.Add("PricingLevel");
            //      dtblOtherDetails.Columns.Add("Customer");
            //      dtblOtherDetails.Columns.Add("Narration");
            //      dtblOtherDetails.Columns.Add("Currency");
            //      dtblOtherDetails.Columns.Add("TotalAmount");
            //      dtblOtherDetails.Columns.Add("BillDiscount");
            //      dtblOtherDetails.Columns.Add("GrandTotal");
            //      dtblOtherDetails.Columns.Add("AmountInWords");
            //      dtblOtherDetails.Columns.Add("Declaration");
            //      dtblOtherDetails.Columns.Add("Heading1");
            //      dtblOtherDetails.Columns.Add("Heading2");
            //      dtblOtherDetails.Columns.Add("Heading3");
            //      dtblOtherDetails.Columns.Add("Heading4");
            //      dtblOtherDetails.Columns.Add("CustomerAddress");
            //      dtblOtherDetails.Columns.Add("CustomerTIN");
            //      dtblOtherDetails.Columns.Add("CustomerCST");
            //      DataRow dRowOther = dtblOtherDetails.Rows[0];
            //      dRowOther["voucherNo"] = Billno;
            //      dRowOther["date"] = DOC_DATE_GRE.Text;
            //      dRowOther["ledgerName"] = CUSTOMER_NAME.Text;
            //   //   dRowOther["Narration"] = txtNarration.Text;
            //   //   dRowOther["SalesAccount"] = cmbSalesAccount.Text;
            //     // dRowOther["SalesMan"] = cmbSalesMan.Text;
            //      dRowOther["PricingLevel"] = RATE_CODE.SelectedValue.ToString();
            //      dRowOther["BillDiscount"] = DISCOUNT.Text;
            //      dRowOther["GrandTotal"] = TOTAL_AMOUNT.Text;
            //      dRowOther["TotalAmount"] = NET_AMOUNT.Text;
            //      dRowOther["address"] = (dtblOtherDetails.Rows[0]["address"].ToString().Replace("\n", ", ")).Replace("\r", "");
            //    //  AccountLedgerSP spAccountLedger = new AccountLedgerSP();
            //   //   AccountLedgerInfo infoAccountLedger = new AccountLedgerInfo();
            ////      infoAccountLedger = spAccountLedger.AccountLedgerView(Convert.ToDecimal(cmbCashOrParty.SelectedValue));
            //    //  dRowOther["CustomerAddress"] = (infoAccountLedger.Address.ToString().Replace("\n", ", ")).Replace("\r", "");
            //    //  dRowOther["CustomerTIN"] = infoAccountLedger.Tin;
            //   //   dRowOther["CustomerCST"] = infoAccountLedger.Cst;
            ////  dRowOther["AmountInWords"] = new NumToText().AmountWords(Convert.ToDecimal(txtGrandTotal.Text), PublicVariables._decCurrencyId);
            //  //    VoucherTypeSP spVoucherType = new VoucherTypeSP();
            //    //  DataTable dtblDeclaration = spVoucherType.DeclarationAndHeadingGetByVoucherTypeId(DecPOSVoucherTypeId);
            // //     dRowOther["Declaration"] = dtblDeclaration.Rows[0]["Declaration"].ToString();
            //  //    dRowOther["Heading1"] = dtblDeclaration.Rows[0]["Heading1"].ToString();
            // //     dRowOther["Heading2"] = dtblDeclaration.Rows[0]["Heading2"].ToString();
            ////      dRowOther["Heading3"] = dtblDeclaration.Rows[0]["Heading3"].ToString();
            ////      dRowOther["Heading4"] = dtblDeclaration.Rows[0]["Heading4"].ToString();
            //      int inFormId = spVoucherType.FormIdGetForPrinterSettings(Convert.ToInt32(dtblDeclaration.Rows[0]["masterId"].ToString()));
            //      PrintWorks.DotMatrixPrint.PrintDesign(inFormId, dtblOtherDetails, dtblGridDetails, dtblOtherDetails);
            //  }
            //  catch (Exception ex)
            //  {
            //      MessageBox.Show("POS: 48" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //  }
        }



        //bool movetodgitems = false;

        private void ITEM_NAME_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                Common.preventDingSound(e);
                if (!PNL_DATAGRIDITEM.Visible)
                {
                    source.Filter = "";
                    PNL_DATAGRIDITEM.Visible = true;
                    if (dataGridItem.Rows.Count > 0)
                    {
                        dataGridItem.CurrentCell = dataGridItem[2, 0];
                        dgBatch.Visible = false;
                        string code = dataGridItem.CurrentRow.Cells["ITEM_CODE"].Value.ToString();
                        if (code != "")
                        {
                            bindBatchGrid(code);
                            if (!hasBatch)
                            {
                                if (dgBatch.Rows.Count > 1)
                                {
                                    dgBatch.Visible = true;
                                    Common_columns_false();
                                }
                                else
                                {
                                    Common_columns();
                                }
                            }
                            else
                            {
                                if (dgBatch.Rows.Count > 0)
                                {
                                    dgBatch.Visible = true;
                                    Common_columns_false();
                                }
                                else
                                {
                                    Common_columns();
                                }
                            }
                        }
                        int id = dataGridItem.CurrentRow.Index;
                    }
                }
                else if (dataGridItem.CurrentRow != null)
                {
                    string code = dataGridItem.CurrentRow.Cells["ITEM_CODE"].Value.ToString();
                    bindBatchGrid(code);
                    if (dgBatch.Visible == true || dgBatch.RowCount > 0)
                    {
                        if (dgBatch.Rows.Count > 0)
                        {
                            try
                            {
                                if (dgBatch.Rows[0].Cells["ITEM_CODE"].Value.ToString() == dataGridItem.CurrentRow.Cells["ITEM_CODE"].Value.ToString())
                                {
                                    ShowStock = false;
                                    itemSelected = true;
                                    string itemcode = dgBatch.Rows[0].Cells["ITEM_CODE"].Value.ToString();
                                    ITEM_CODE.Text = itemcode;

                                    //NOT VALIDATED//
                                    BARCODE.Text = dgBatch.Rows[0].Cells["BATCH_ID"].Value.ToString();
                                    //NOT VALIDATED//
                                    if (dgBatch.Rows[0].Cells["HSN"].Value != null)
                                    {
                                        txt_HSN.Text = dgBatch.Rows[0].Cells["HSN"].Value.ToString();
                                    }
                                    PurchasePrice = Convert.ToDecimal(dgBatch.Rows[0].Cells["PUR"].Value);
                                    String rateType = Convert.ToString(RATE_CODE.SelectedValue);
                                    string pricedecimal = dgBatch.Rows[0].Cells[rateType].Value.ToString();
                                    double pricedec = Convert.ToDouble(pricedecimal);
                                    //PRICE.Text = pricedec.ToString("N3");
                                    PRICE.TextChanged -= new EventHandler(calculateGrossAmount);
                                    PRICE.Text = dgBatch.Rows[0].Cells[rateType].Value.ToString();
                                    sales_price = Convert.ToDouble(dgBatch.Rows[0].Cells[rateType].Value);
                                    //PRICE.Text = (dataGridItem.CurrentRow.Cells["SALES"].Value.ToString("N3");
                                    HASSERIAL = Convert.ToBoolean(dgBatch.Rows[0].Cells["HASSERIAL"].Value);
                                    PNLSERIAL.Visible = HASSERIAL;
                                    mrp = Convert.ToDouble(dgBatch.Rows[0].Cells["MRP"].Value);
                                    if (hasBatch)
                                    {
                                        if (dgBatch.Rows[0].Cells["BATCH CODE"].Value != null)
                                            BATCH.Text = dgBatch.Rows[0].Cells["BATCH CODE"].Value.ToString();
                                        if (dgBatch.Rows[0].Cells["EXPIRY DATE"].Value != null && dgBatch.Rows[0].Cells["EXPIRY DATE"].Value.ToString() != "")
                                            EXPIRY_DATE.Value = Convert.ToDateTime(dgBatch.Rows[0].Cells["EXPIRY DATE"].Value);
                                    }
                                    tb_mrp.Text = mrp.ToString(decimalFormat);
                                    kryptonLabel36.Visible = HASSERIAL;
                                    SERIALNO.Visible = HASSERIAL;
                                    kryptonLabel36.Visible = HASSERIAL;
                                    SERIALNO.Visible = HASSERIAL;
                                    Stockcheck();
                                    //if (hasBatch)
                                    //{
                                    //    BATCH.Focus();
                                    //}
                                    //else
                                    //{
                                    TaxId = Convert.ToInt16(dgBatch.Rows[0].Cells["TaxId"].Value.ToString());
                                    GetTaxRate();

                                    QUANTITY.Text = "1";

                                    if (SalebyItemCode)
                                        ITEM_CODE.Focus();
                                    else if (MoveToUnit)
                                        UOM.Focus();
                                    else if (MoveToQty)
                                        QUANTITY.Focus();
                                    else if (HASSERIAL)
                                        SERIALNO.Focus();
                                    else if (txtfree.Visible)
                                        txtfree.Focus();
                                    else if (MoveToPrice)
                                        PRICE.Focus();
                                    else if (tb_mrp.Visible)
                                        tb_mrp.Focus();
                                    else if (GROSS_TOTAL.Visible)
                                        GROSS_TOTAL.Focus();
                                    else if (MoveToDisc)
                                        ITEM_DISCOUNT.Focus();
                                    else if (tb_netvalue.Visible)
                                        tb_netvalue.Focus();
                                    else if (tb_descr.Visible)
                                        tb_descr.Focus();
                                    else if (hasTax)
                                    {
                                        if (MoveToTaxper)
                                            ITEM_TAX_PER.Focus();
                                        else
                                        {
                                            ITEM_TAX.Focus();
                                        }
                                    }
                                    else if (hasBatch)
                                        BATCH.Focus();
                                    else
                                    {
                                        addItem();
                                        clearItem();
                                        if (SalebyItemName)
                                            ITEM_NAME.Focus();
                                        else if (SalebyItemCode)
                                            ITEM_CODE.Focus();
                                        else if (SalebyBarcode)
                                            BARCODE.Focus();
                                        else
                                            ITEM_NAME.Focus();
                                    }
                                    //    tb_descr.Focus();

                                    //}

                                    //   this.UOM.SelectedIndexChanged -= new EventHandler(UOM_SelectedIndexChanged);
                                    addUnits();
                                    //  this.UOM.SelectedIndexChanged -= new EventHandler(UOM_SelectedIndexChanged);
                                    //   UOM.Text = dgBatch.Rows[0].Cells["UNIT_CODE"].Value.ToString();

                                    GetDiscount();

                                    ItemArabicName = dgBatch.Rows[0].Cells["DESC_ARB"].Value.ToString();
                                    ITEM_NAME.Text = dgBatch.Rows[0].Cells["ITEM NAME"].Value.ToString();
                                    PNL_DATAGRIDITEM.Visible = false;
                                    itemSelected = false;
                                    pricefob = Convert.ToDouble(PRICE.Text);
                                    if (RATE_CODE.Text.StartsWith("MRP"))
                                    {
                                        double taxcalc = 0;
                                        taxcalc = (Convert.ToDouble(ITEM_TAX_PER.Text) / 100) + 1;
                                        PRICE.Text = (Convert.ToDouble(PRICE.Text) / taxcalc).ToString(decimalFormat);
                                    }
                                    else if (!hasSaleExclusive)
                                    {
                                        double taxcalc = 0;
                                        taxcalc = (Convert.ToDouble(ITEM_TAX_PER.Text) / 100) + 1;
                                        PRICE.Text = (Convert.ToDouble(PRICE.Text) / taxcalc).ToString();
                                    }
                                    PRICE.TextChanged += new EventHandler(calculateGrossAmount);
                                    sales_price = Convert.ToDouble(PRICE.Text);
                                    PRICE.Text = sales_price.ToString();
                                }
                            }
                            catch
                            {
                            }
                        }

                    }
                    else
                    {
                        if ((StockOut = General.IsEnabled(Settings.StockOut)))
                        {
                            int id = stockEntry.MaxBatchId(code);
                            if (id > 0)
                            {
                                bindBatchGrid_forOutSale(code, id.ToString());
                                ShowStock = false;
                                itemSelected = true;
                                string itemcode = dgBatch.Rows[0].Cells["ITEM_CODE"].Value.ToString();
                                ITEM_CODE.Text = itemcode;

                                //NOT VALIDATED//
                                BARCODE.Text = dgBatch.Rows[0].Cells["BATCH_ID"].Value.ToString();

                                //NOT VALIDATED//
                                if (dgBatch.Rows[0].Cells["HSN"].Value != null)
                                {
                                    txt_HSN.Text = dgBatch.Rows[0].Cells["HSN"].Value.ToString();
                                }
                                PurchasePrice = Convert.ToDecimal(dgBatch.Rows[0].Cells["PUR"].Value);
                                String rateType = Convert.ToString(RATE_CODE.SelectedValue);
                                string pricedecimal = dgBatch.Rows[0].Cells[rateType].Value.ToString();
                                double pricedec = Convert.ToDouble(pricedecimal);
                                //PRICE.Text = pricedec.ToString("N3");
                                PRICE.TextChanged -= new EventHandler(calculateGrossAmount);
                                PRICE.Text = dgBatch.Rows[0].Cells[rateType].Value.ToString();
                                sales_price = Convert.ToDouble(dgBatch.Rows[0].Cells[rateType].Value);
                                //PRICE.Text = (dataGridItem.CurrentRow.Cells["SALES"].Value.ToString("N3");
                                HASSERIAL = Convert.ToBoolean(dgBatch.Rows[0].Cells["HASSERIAL"].Value);
                                PNLSERIAL.Visible = HASSERIAL;
                                mrp = Convert.ToDouble(dgBatch.Rows[0].Cells["MRP"].Value);
                                if (hasBatch)
                                {
                                    if (dgBatch.Rows[0].Cells["BATCH CODE"].Value != null)
                                        BATCH.Text = dgBatch.Rows[0].Cells["BATCH CODE"].Value.ToString();
                                    if (dgBatch.Rows[0].Cells["EXPIRY DATE"].Value != null && dgBatch.Rows[0].Cells["EXPIRY DATE"].Value.ToString() != "")
                                        EXPIRY_DATE.Value = Convert.ToDateTime(dgBatch.Rows[0].Cells["EXPIRY DATE"].Value);
                                }
                                tb_mrp.Text = mrp.ToString(decimalFormat);
                                kryptonLabel36.Visible = HASSERIAL;
                                SERIALNO.Visible = HASSERIAL;
                                kryptonLabel36.Visible = HASSERIAL;
                                SERIALNO.Visible = HASSERIAL;

                                //if (hasBatch)
                                //{
                                //    BATCH.Focus();
                                //}
                                //else
                                //{
                                TaxId = Convert.ToInt16(dgBatch.Rows[0].Cells["TaxId"].Value.ToString());
                                GetTaxRate();

                                QUANTITY.Text = "1";

                                if (SalebyItemCode)
                                    ITEM_CODE.Focus();
                                else if (MoveToUnit)
                                    UOM.Focus();
                                else if (MoveToQty)
                                    QUANTITY.Focus();
                                else if (HASSERIAL)
                                    SERIALNO.Focus();
                                else if (txtfree.Visible)
                                    txtfree.Focus();
                                else if (MoveToPrice)
                                    PRICE.Focus();
                                else if (tb_mrp.Visible)
                                    tb_mrp.Focus();
                                else if (GROSS_TOTAL.Visible)
                                    GROSS_TOTAL.Focus();
                                else if (MoveToDisc)
                                    ITEM_DISCOUNT.Focus();
                                else if (tb_netvalue.Visible)
                                    tb_netvalue.Focus();
                                else if (tb_descr.Visible)
                                    tb_descr.Focus();
                                else if (hasTax)
                                {
                                    if (MoveToTaxper)
                                        ITEM_TAX_PER.Focus();
                                    else
                                    {
                                        ITEM_TAX.Focus();
                                    }
                                }
                                else if (hasBatch)
                                    BATCH.Focus();
                                else
                                {
                                    addItem();
                                    clearItem();
                                    if (SalebyItemName)
                                        ITEM_NAME.Focus();
                                    else if (SalebyItemCode)
                                        ITEM_CODE.Focus();
                                    else if (SalebyBarcode)
                                        BARCODE.Focus();
                                    else
                                        ITEM_NAME.Focus();
                                }
                                //    tb_descr.Focus();

                                //}

                                //   this.UOM.SelectedIndexChanged -= new EventHandler(UOM_SelectedIndexChanged);
                                addUnits();
                                //  this.UOM.SelectedIndexChanged -= new EventHandler(UOM_SelectedIndexChanged);
                                //    UOM.Text = dgBatch.Rows[0].Cells["UNIT_CODE"].Value.ToString();
                                TaxId = Convert.ToInt16(dgBatch.Rows[0].Cells["TaxId"].Value.ToString());
                                GetTaxRate();

                                GetDiscount();

                                ItemArabicName = dgBatch.Rows[0].Cells["DESC_ARB"].Value.ToString();
                                ITEM_NAME.Text = dgBatch.Rows[0].Cells["ITEM NAME"].Value.ToString();
                                PNL_DATAGRIDITEM.Visible = false;
                                itemSelected = false;
                                pricefob = Convert.ToDouble(PRICE.Text);
                                if (RATE_CODE.Text.StartsWith("MRP"))
                                {
                                    double taxcalc = 0;
                                    double TAXPER = 0;
                                    if (ITEM_TAX_PER.Text != "")
                                        TAXPER = Convert.ToDouble(ITEM_TAX_PER.Text);
                                    taxcalc = (TAXPER / 100) + 1;
                                    PRICE.Text = (Convert.ToDouble(PRICE.Text) / taxcalc).ToString(decimalFormat);
                                }
                                else if (!hasSaleExclusive)
                                {
                                    double taxcalc = 0;
                                    double TAXPER = 0;
                                    if (ITEM_TAX_PER.Text != "")
                                        TAXPER = Convert.ToDouble(ITEM_TAX_PER.Text);
                                    taxcalc = (TAXPER / 100) + 1;
                                    PRICE.Text = (Convert.ToDouble(PRICE.Text) / taxcalc).ToString();
                                }
                                PRICE.TextChanged += new EventHandler(calculateGrossAmount);
                                sales_price = Convert.ToDouble(PRICE.Text);
                                PRICE.Text = sales_price.ToString();

                            }
                        }
                        else
                        {
                            MessageBox.Show("Selected item is out of Stock");
                            ITEM_NAME.Focus();
                            return;
                        }

                    }
                    //ShowStock = false;
                    //itemSelected = true;
                    //string itemcode = dataGridItem.CurrentRow.Cells["ITEM_CODE"].Value.ToString();
                    //ITEM_CODE.Text = itemcode;
                    //try
                    //{
                    //    //i've used try catch because i'm not sure that purchhase price is set by the user.
                    //    PurchasePrice = Convert.ToDecimal(dataGridItem.CurrentRow.Cells["PUR"].Value);
                    //}
                    //catch
                    //{
                    //    //if the purchase price is null then set it to zero.
                    //    PurchasePrice = 0;
                    //}

                    //String rateType = Convert.ToString(RATE_CODE.SelectedValue);
                    //string pricedecimal = dataGridItem.CurrentRow.Cells[rateType].Value.ToString();
                    //double pricedec = 0;
                    //try
                    //{
                    //    pricedec = Convert.ToDouble(pricedecimal);
                    //}
                    //catch
                    //{
                    //    //do nothing here since we're already setting pricedec to 0
                    //}
                    //PRICE.Text = dataGridItem.CurrentRow.Cells[rateType].Value.ToString();
                    //hcnNO = Convert.ToString(dataGridItem.CurrentRow.Cells["Group"].Value);
                    //c_price = Convert.ToString(dataGridItem.CurrentRow.Cells["cost_price"].Value);
                    //sup_id = Convert.ToString(dataGridItem.CurrentRow.Cells["supplier_id"].Value);
                    //sup_name = Convert.ToString(dataGridItem.CurrentRow.Cells["Supplier"].Value);
                    //HASSERIAL = Convert.ToBoolean(dataGridItem.CurrentRow.Cells["HASSERIAL"].Value);
                    //PNLSERIAL.Visible = HASSERIAL;
                    //if (dataGridItem.Columns.Contains("MRP"))
                    //{
                    //    if (dataGridItem.CurrentRow.Cells["MRP"].Value != null)
                    //    {
                    //        mrp = Convert.ToDouble(dataGridItem.CurrentRow.Cells["MRP"].Value);
                    //    }
                    //    else
                    //    {
                    //        mrp = 0;
                    //    }
                    //}
                    //else
                    //    mrp = 0;
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

                    ////}

                    //this.UOM.SelectedIndexChanged -= new EventHandler(UOM_SelectedIndexChanged);
                    //addUnits();
                    //this.UOM.SelectedIndexChanged -= new EventHandler(UOM_SelectedIndexChanged);
                    //UOM.Text = dataGridItem.CurrentRow.Cells["UNIT_CODE"].Value.ToString();
                    //TaxId = Convert.ToInt16(dataGridItem.CurrentRow.Cells["TaxId"].Value.ToString());
                    //GetTaxRate();

                    //GetDiscount();
                    //ItemArabicName = dataGridItem.CurrentRow.Cells["DESC_ARB"].Value.ToString();
                    //ITEM_NAME.Text = dataGridItem.CurrentRow.Cells["ITEM NAME"].Value.ToString();
                    //PNL_DATAGRIDITEM.Visible = false;
                    //itemSelected = false;
                    //if (RATE_CODE.Text.StartsWith("MRP"))
                    //{
                    //    double taxcalc = 0;
                    //    taxcalc = (Convert.ToDouble(ITEM_TAX_PER.Text) / 100) + 1;
                    //    PRICE.Text = (Convert.ToDouble(PRICE.Text) / taxcalc).ToString(decimalFormat);
                    //}
                }
            }
            else if (e.KeyCode == Keys.Up)
            {
                if (dataGridItem.Rows.Count > 0)
                {
                    int r = dataGridItem.CurrentCell.RowIndex;
                    if (r > 0) //check for index out of range
                        dataGridItem.CurrentCell = dataGridItem[2, r - 1];
                    dgBatch.Visible = false;
                    string code = dataGridItem.CurrentRow.Cells["ITEM_CODE"].Value.ToString();
                    if (code != "")
                    {
                        bindBatchGrid(code);
                        if (!hasBatch)
                        {
                            if (dgBatch.Rows.Count > 1)
                            {
                                dgBatch.Visible = true;
                                Common_columns_false();
                            }
                            else
                            {
                                Common_columns();
                            }
                        }
                        else
                        {
                            if (dgBatch.Rows.Count > 0)
                            {
                                dgBatch.Visible = true;
                                Common_columns_false();
                            }
                            else
                            {
                                Common_columns();
                            }
                        }

                    }
                }
            }
            else if (e.KeyCode == Keys.Down)
            {
                bool ifDoc_Id = false;
                if (ITEM_NAME.Text == "" && PNL_DATAGRIDITEM.Visible == false)
                {
                    if (cmbInvType.SelectedIndex == 0)
                    {
                        conn.Open();
                        cmd.CommandText = "SELECT DOC_ID FROM INV_SALES_HDR WHERE DOC_ID = '" + VOUCHNUM.Text + "'AND FLAGDEL='TRUE' AND (SALE_TYPE IS NULL OR SALE_TYPE='')";
                        cmd.CommandType = CommandType.Text;
                        SqlDataReader r = cmd.ExecuteReader();
                        while (r.Read())
                        {
                            ifDoc_Id = true;
                        }
                        conn.Close();
                        if (!ifDoc_Id)
                            GetMaxDocID();
                    }
                    else
                    {
                        if (cmbInvType.SelectedValue != null)
                        {
                            conn.Open();
                            cmd.CommandText = "SELECT * FROM INV_SALES_HDR WHERE DOC_ID = '" + VOUCHNUM.Text + "'AND FLAGDEL='TRUE' AND SALE_TYPE= '" + cmbInvType.SelectedValue.ToString() + "'";
                            cmd.CommandType = CommandType.Text;
                            SqlDataReader r = cmd.ExecuteReader();
                            while (r.Read())
                            {
                                ifDoc_Id = true;
                            }
                            conn.Close();
                        }
                        if (!ifDoc_Id)
                            GetMaxDocID();
                    }

                    source.Filter = "";
                    PNL_DATAGRIDITEM.Visible = true;
                    //PNL_DATAGRIDITEM.BringToFront();
                    //dataGridItem.BringToFront();
                    if (dataGridItem.Rows.Count > 0)
                    {
                        dataGridItem.CurrentCell = dataGridItem[2, 0];
                        dgBatch.Visible = false;
                        string code = dataGridItem.CurrentRow.Cells["ITEM_CODE"].Value.ToString();
                        if (code != "")
                        {
                            bindBatchGrid(code);
                            if (!hasBatch)
                            {
                                if (dgBatch.Rows.Count > 1)
                                {
                                    dgBatch.Visible = true;
                                    Common_columns_false();
                                }
                                else
                                {
                                    Common_columns();
                                }
                            }
                            else
                            {
                                if (dgBatch.Rows.Count > 0)
                                {
                                    dgBatch.Visible = true;
                                    Common_columns_false();
                                }
                                else
                                {
                                    Common_columns();
                                }
                            }
                        }
                        int id = dataGridItem.CurrentRow.Index;
                    }
                }
                else if (PNL_DATAGRIDITEM.Visible == true && dataGridItem.CurrentRow != null)
                {
                    int r = dataGridItem.CurrentCell.RowIndex;
                    if (r < dataGridItem.Rows.Count - 1)
                    {
                        dataGridItem.CurrentCell = dataGridItem[2, r + 1];
                        dgBatch.Visible = false;
                        string code = dataGridItem.CurrentRow.Cells["ITEM_CODE"].Value.ToString();
                        if (code != "")
                        {
                            bindBatchGrid(code);
                            if (!hasBatch)
                            {
                                if (dgBatch.Rows.Count > 1)
                                {
                                    dgBatch.Visible = true;
                                    Common_columns_false();
                                }
                                else
                                {
                                    Common_columns();
                                }
                            }
                            else
                            {

                                if (dgBatch.Rows.Count > 0)
                                {
                                    dgBatch.Visible = true;
                                    Common_columns_false();
                                }
                                else
                                {
                                    Common_columns();
                                }
                            }
                        }
                    }
                    else if (r == dataGridItem.Rows.Count - 1)
                    {
                        dataGridItem.CurrentCell = dataGridItem[2, 0];
                        dgBatch.Visible = false;
                        string code = dataGridItem.CurrentRow.Cells["ITEM_CODE"].Value.ToString();
                        if (code != "")
                        {
                            bindBatchGrid(code);
                            if (!hasBatch)
                            {
                                if (dgBatch.Rows.Count > 1)
                                {
                                    dgBatch.Visible = true;
                                    Common_columns_false();
                                }
                                else
                                {
                                    Common_columns();
                                }
                            }
                            else
                            {
                                if (dgBatch.Rows.Count > 0)
                                {
                                    dgBatch.Visible = true;
                                    Common_columns_false();
                                }
                                else
                                {
                                    Common_columns();
                                }
                            }
                        }

                    }

                }
            }
            else if (e.KeyCode == Keys.Right)
            {
                if (dgBatch.Visible == true && dgBatch.RowCount > 0)
                {
                    dgBatch.Focus();
                    dgBatch.CurrentCell = dgBatch[2, 0];
                }
            }
            else if (e.KeyCode == Keys.Space)
            {
                if (ITEM_NAME.Text.Trim() == "")
                {
                    PNL_DATAGRIDITEM.Visible = false;
                    firstitemlistbyname = true;
                    txtcashrcvd.Focus();
                    txtcashrcvd.Text = NET_AMOUNT.Text;
                }
            }
        }
        
        bool firstitemlistbyname = false;

        public string tooltip = "";
        public ToolTip tool = new ToolTip();

        private void ITEM_NAME_TextChanged(object sender, EventArgs e)
        {            
            bool ifDoc_Id = false;
            if (cmbInvType.SelectedIndex == 0)
            {
                conn.Open();
                cmd.CommandText = "SELECT DOC_ID FROM INV_SALES_HDR WHERE DOC_ID = '" + VOUCHNUM.Text + "'AND FLAGDEL='TRUE' AND (SALE_TYPE IS NULL OR SALE_TYPE='')";
                cmd.CommandType = CommandType.Text;
                SqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    ifDoc_Id = true;
                }
                conn.Close();
                if (!ifDoc_Id)
                    GetMaxDocID();
            }
            else
            {
                conn.Open();
                cmd.CommandText = "SELECT * FROM INV_SALES_HDR WHERE DOC_ID = '" + VOUCHNUM.Text + "'AND FLAGDEL='TRUE' AND SALE_TYPE= '" + cmbInvType.SelectedValue + "'";
                cmd.CommandType = CommandType.Text;
                SqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    ifDoc_Id = true;
                }
                conn.Close();
                if (!ifDoc_Id)
                    GetMaxDocID();
            }
            if (ITEM_NAME.Text.Trim() == "")
            {
                PNL_DATAGRIDITEM.Visible = false;
                firstitemlistbyname = true;
              //  txtcashrcvd.Focus();
              //  txtcashrcvd.Text = NET_AMOUNT.Text;
            }
            else
            {
                dgBatch.Visible = false;
                PNL_DATAGRIDITEM.Visible = true;
                try
                {
                    source.Filter = string.Format("[ITEM NAME] LIKE '%{0}%' ", ITEM_NAME.Text.Replace("'", "''").Replace("*", "[*]"));
                }
                catch
                {

                }
            }
            
        }

        Class.Discount Disc = new Class.Discount();
        public void GetDiscount()
        {
            try
            {
                DataTable dt = new DataTable();
                Disc.Product_Id = ITEM_CODE.Text;
                Disc.Uom = UOM.Text;

                Disc.SalType = RATE_CODE.SelectedValue.ToString();
                Disc.Date = DateTime.Now.ToString("MM/dd/yyyy");
                dt = Disc.GetProductDiscount();
                if (dt.Rows.Count > 0)
                {
                    DiscType = dt.Rows[0][0].ToString();
                    DiscValue = dt.Rows[0][1].ToString();
                    //if (DiscType == "Percentage")
                    //{
                    ITEM_DISCOUNT.Text = DiscValue;
                    // }
                    HasSeasonDiscount = true;
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
                    ShowStock = false;
                    itemSelected = true;
                    string itemcode = dataGridItem.CurrentRow.Cells["ITEM_CODE"].Value.ToString();
                    ITEM_CODE.Text = itemcode;
                    PurchasePrice = Convert.ToDecimal(dataGridItem.CurrentRow.Cells["COST"].Value);
                    string pricedecimal = dataGridItem.CurrentRow.Cells["SALES"].Value.ToString();
                    double pricedec = Convert.ToDouble(pricedecimal);
                    PRICE.Text = pricedec.ToString("N3");
                    //PRICE.Text = (dataGridItem.CurrentRow.Cells["SALES"].Value.ToString("N3");
                    HASSERIAL = Convert.ToBoolean(dataGridItem.CurrentRow.Cells["HASSERIAL"].Value);
                    PNLSERIAL.Visible = HASSERIAL;
                    kryptonLabel36.Visible = HASSERIAL;
                    SERIALNO.Visible = HASSERIAL;
                    kryptonLabel36.Visible = HASSERIAL;
                    SERIALNO.Visible = HASSERIAL;

                    //if (hasBatch)
                    //{
                    //    BATCH.Focus();
                    //}
                    //else
                    //{
                    QUANTITY.Text = "1";
                    if (SalebyItemCode)
                        ITEM_CODE.Focus();
                    else if (MoveToUnit)
                        UOM.Focus();
                    else if (MoveToQty)
                        QUANTITY.Focus();
                    else if (HASSERIAL)
                        SERIALNO.Focus();
                    else if (txtfree.Visible)
                        txtfree.Focus();
                    else if (MoveToPrice)
                        PRICE.Focus();
                    else if (tb_mrp.Visible)
                        tb_mrp.Focus();
                    else if (GROSS_TOTAL.Visible)
                        GROSS_TOTAL.Focus();
                    else if (MoveToDisc)
                        ITEM_DISCOUNT.Focus();
                    else if (tb_netvalue.Visible)
                        tb_netvalue.Focus();
                    else if (tb_descr.Visible)
                        tb_descr.Focus();
                    else if (hasTax)
                    {
                        if (MoveToTaxper)
                            ITEM_TAX_PER.Focus();
                    }
                    else if (hasBatch)
                        BATCH.Focus();
                    else
                    {
                        addItem();
                        clearItem();
                        if (SalebyItemName)
                            ITEM_NAME.Focus();
                        else if (SalebyItemCode)
                            ITEM_CODE.Focus();
                        else if (SalebyBarcode)
                            BARCODE.Focus();
                        else
                            ITEM_NAME.Focus();
                    }
                    //    tb_descr.Focus();

                    //}

                    this.UOM.SelectedIndexChanged -= new EventHandler(UOM_SelectedIndexChanged);
                    addUnits();
                    this.UOM.SelectedIndexChanged -= new EventHandler(UOM_SelectedIndexChanged);
                    //CHANGED IN UOM
                    UOM.Text = dataGridItem.CurrentRow.Cells["UNIT_CODE"].Value.ToString();
                    TaxId = Convert.ToInt16(dataGridItem.CurrentRow.Cells["TaxId"].Value.ToString());
                    GetTaxRate();

                    GetDiscount();
                    ItemArabicName = dataGridItem.CurrentRow.Cells["DESC_ARB"].Value.ToString();
                    ITEM_NAME.Text = dataGridItem.CurrentRow.Cells["Item Name"].Value.ToString();
                    PNL_DATAGRIDITEM.Visible = false;
                    itemSelected = false;
                    Common.preventDingSound(e);

                }
                if (e.KeyData == Keys.Down)
                {
                   
                            int id = dataGridItem.CurrentRow.Index;
                        
                    
        
                }
            }
            catch
            {
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
                else if (e.KeyChar == 'S' || e.KeyChar == 's')
                {
                    if (!HASSERIAL)
                    {
                        HASSERIAL = true;
                        PNLSERIAL.Visible = HASSERIAL;
                        SERIALNO.Visible = true;
                        kryptonLabel36.Visible = true;
                    }
                    else
                    {
                        HASSERIAL = false;
                        PNLSERIAL.Visible = HASSERIAL;
                        kryptonLabel36.Visible = true;
                        SERIALNO.Visible = false;
                    }
                }
            }
            catch
            {
            }
        }

        private void dgItems_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {

                if (dgItems.Rows.Count > 0 && dgItems.CurrentRow != null)
                {
                    DataGridViewCellCollection c = dgItems.CurrentRow.Cells;
                    selectedRow = dgItems.CurrentRow.Index;
                    ITEM_CODE.Text = Convert.ToString(c["cCode"].Value);
                    firstitemlistbyname = true;
                    ITEM_NAME.Text = Convert.ToString(c["cName"].Value);
                    addUnits();
                    if (hasBatch)
                    {
                        BATCH.Text = Convert.ToString(c["cBatch"].Value);
                        EXPIRY_DATE.Value = DateTime.ParseExact(Convert.ToString(c["cExpDate"].Value), "dd/MM/yyyy", null);
                    }
                    UOM.Text = Convert.ToString(c["cUnit"].Value);
                    QUANTITY.Text = Convert.ToString(c["cQty"].Value);
                    PRICE.Text = Convert.ToString(c["cPrice"].Value);
                    pricefob = Convert.ToDouble(PRICE.Text);
                    if (hasTax)
                    {
                        ITEM_TAX_PER.Text = Convert.ToString(c["cTaxPer"].Value);
                        ITEM_TAX.Text = Convert.ToString(c["cTaxAmt"].Value);
                    }
                    GROSS_TOTAL.Text = Convert.ToString(c["cGTotal"].Value);
                    ITEM_DISCOUNT.Text = Convert.ToString(c["DiscValues"].Value);
                    if (Convert.ToString(c["DiscTypes"].Value) == "Percentage")
                    {

                        CalculateDiscPerct();
                    }
                    else
                    {
                        CalculateDiscAmt();
                    }
                    ITEM_TOTAL.Text = Convert.ToString(c["cTotal"].Value);
                    PNL_DATAGRIDITEM.Visible = false;
                    EditActive = true;

                    if (SERIALNO.Text != "")
                    {
                        PNLSERIAL.Visible = true;
                        kryptonLabel36.Visible = true;
                        SERIALNO.Visible = true;
                    }
                    else
                    {
                        PNLSERIAL.Visible = false;
                        kryptonLabel36.Visible = false;
                        SERIALNO.Visible = false;
                    }
                }
                firstitemlistbyname = false;
                QUANTITY.Focus();
                Common.preventDingSound(e);

            }
            if (e.KeyData == Keys.Delete)
            {
                if (dgItems.Rows.Count > 0 && dgItems.CurrentRow != null)
                {
                    dgItems.Rows.Remove(dgItems.CurrentRow);
                    totalCalculation();
                }
                else
                {
                    if (SalebyItemCode)
                        ITEM_CODE.Focus();
                    else
                        ITEM_NAME.Focus();
                }
            }
        }

        private void dataGridItem_Click(object sender, EventArgs e)
        {
            ITEM_NAME.Focus();
        }

        private void ITEM_CODE_TextChanged(object sender, EventArgs e)
        {
            if (!itemSelected)
            {
                if (ITEM_CODE.Text == "")
                {
                    PNL_DATAGRIDITEM.Visible = false;
                }
                else
                {
                    PNL_DATAGRIDITEM.Visible = true;
                    source.Filter = string.Format("[ITEM_CODE] LIKE '%{0}%' ", ITEM_CODE.Text);
                    dataGridItem.ClearSelection();
                    dataGridItem.Columns["Item Name"].Width = 250;
                }

                if (clearitemname)
                {
                    ITEM_CODE.Text = "";
                    clearitemname = false;
                }
            }
        }

        private void ITEM_CODE_KeyDown(object sender, KeyEventArgs e)
        {
            if (PNL_DATAGRIDITEM.Visible == true)
            {
                dataGridItem.Focus();
                dataGridItem.CurrentCell = dataGridItem.Rows[0].Cells[2];

            }
            else
            {
                dgItems.Focus();
                dgItems.CurrentCell = dgItems.Rows[0].Cells[1];
            }

            if (e.KeyData == Keys.Enter)
            {
                //GROSS_TOTAL.Focus();
                //if (HASSERIAL)
                //    SERIALNO.Focus();
                //else
                QUANTITY.Focus();
                Common.preventDingSound(e);
            }

        }

        private void ITEM_DISCOUNT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '-')
            {
                
                if (DiscType == "Percentage")
                {
                    Disctext.Text = "Disc Amt";
                    DiscType = "Amount";
                    CalculateDiscAmt();
                    ITEM_DISCOUNT_Leave(sender, e);
                    ITEM_DISCOUNT.Text = amt.ToString(decimalFormat);
                    ITEM_DISCOUNT_TextChanged(sender, e);
                }
                else if (DiscType == "Amount")
                {
                    Disctext.Text = "Disc %";
                    DiscType = "Percentage";
                    DISCOUNT.Text = "0";
                    CalculateDiscPerct();
                    ITEM_DISCOUNT_Leave(sender, e);

                    ITEM_DISCOUNT.Text = percent.ToString(decimalFormat);
                    ITEM_DISCOUNT_TextChanged(sender, e);
                }
                else
                {
                    DiscType = "Amount";
                    Disctext.Text = "Disc Amt";
                    CalculateDiscAmt();
                    ITEM_DISCOUNT_Leave(sender, e);
                    ITEM_DISCOUNT.Text = amt.ToString(decimalFormat);
                }
            }
        }

        public void CalculateDiscPerct()
        {
            try
            {
                Disctext.Text = "Disc %";

                if (ITEM_DISCOUNT.Text.Trim() != "" && GROSS_TOTAL.Text.Trim() != "")
                {
                    ITEM_TOTAL.Text = (Convert.ToDouble(GROSS_TOTAL.Text) - (Convert.ToDouble(GROSS_TOTAL.Text) * (Convert.ToDouble(ITEM_DISCOUNT.Text) / 100))).ToString(decimalFormat);
                    DiscAmt = Convert.ToDouble(GROSS_TOTAL.Text) * (Convert.ToDouble(ITEM_DISCOUNT.Text) / 100);
                }

                if (ITEM_DISCOUNT.Text == "")
                {
                    ITEM_TOTAL.Text = GROSS_TOTAL.Text;
                    DiscAmt = Convert.ToDouble(DiscValue);
                }
            }

            catch
            { }
        }

        public void CalculateDiscAmt()
        {
            try
            {
                Disctext.Text = "Disc Amt";
                // DiscType2 = "Amt";
                if (ITEM_DISCOUNT.Text.Trim() != "" && GROSS_TOTAL.Text.Trim() != "")
                {
                    ITEM_TOTAL.Text = (Convert.ToDouble(GROSS_TOTAL.Text) - (Convert.ToDouble(ITEM_DISCOUNT.Text))).ToString(decimalFormat);
                    DiscAmt = Convert.ToDouble(ITEM_DISCOUNT.Text);
                }

                if (ITEM_DISCOUNT.Text == "")
                {
                    ITEM_TOTAL.Text = GROSS_TOTAL.Text;
                    DiscAmt = Convert.ToDouble(DiscValue);
                }
            }
            catch
            {
            }

        }

        public void CalculateBillDiscAmt()
        {
            try
            {
                Disctext.Text = "Disc Amt";
                // DiscType2 = "Amt";
                if (ITEM_DISCOUNT.Text.Trim() != "" && GROSS_TOTAL.Text.Trim() != "")
                {
                    NET_AMOUNT.Text = (Convert.ToDouble(TOTAL_AMOUNT.Text) - (Convert.ToDouble(DISCOUNT.Text))).ToString(decimalFormat);
                    DiscAmt = Convert.ToDouble(ITEM_DISCOUNT.Text);
                }

                if (ITEM_DISCOUNT.Text == "")
                {
                    NET_AMOUNT.Text = GROSS_TOTAL.Text;
                    DiscAmt = Convert.ToDouble(DiscValue);
                }
            }
            catch
            {
            }

        }
        
        private void CUSTOMER_CODE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                Common.preventDingSound(e);
                if (FocusSalesMan)
                {
                    ActiveControl = SALESMAN_CODE;
                    //SALESMAN_NAME.Focus();
                }
                else if (FocusDate)
                {
                    ActiveControl = DOC_DATE_GRE;
                }
                else if (Focus_Rate_Type)
                {
                    ActiveControl = RATE_CODE;
                    //RATE_CODE.Focus();
                }
                else if (Focus_Sale_Type)
                {
                    ActiveControl = cmbInvType;
                    //cmbInvType.Focus();
                }
                else if (hasBarcode)
                {

                    if (SalebyBarcode)
                    {
                        ActiveControl = BARCODE;
                    }
                    else
                    {
                        ActiveControl = ITEM_NAME;
                    }
                }
                else if (SalebyItemName)
                {
                    ActiveControl = ITEM_NAME;
                }
                else if (SalebyItemCode)
                {
                    ActiveControl = ITEM_CODE;
                }
                else
                {
                    ActiveControl = ITEM_NAME;
                }
            }
            else if (e.KeyCode == Keys.Back)
            {
                if (CUSTOMER_CODE.Text == "")
                {
                    CUSTOMER_NAME.Text = "";
                    chkCustomeleveldiscount.Checked = false;
                    chkCustomeleveldiscount.Enabled = false;
                }
            }
            else
            {
                btnCust.PerformClick();
            }
        }

        private void SALESMAN_CODE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                Common.preventDingSound(e);
                if (FocusDate)
                {
                    ActiveControl = DOC_DATE_GRE;
                }
                else if (Focus_Rate_Type)
                {
                    ActiveControl = RATE_CODE;
                    //RATE_CODE.Focus();
                }
                else if (Focus_Sale_Type)
                {
                    ActiveControl = cmbInvType;
                    //cmbInvType.Focus();
                }
                else if (hasBarcode)
                {

                    if (SalebyBarcode)
                    {
                        ActiveControl = BARCODE;
                    }
                    else
                    {
                        ActiveControl = ITEM_NAME;
                    }
                }
                else if (SalebyItemName)
                {
                    ActiveControl = ITEM_NAME;
                }
                else if (SalebyItemCode)
                {
                    ActiveControl = ITEM_CODE;
                }
                else
                {
                    ActiveControl = ITEM_NAME;
                }
            }
            else
            {
                btnSales.PerformClick();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Find_Item Fitem = new Find_Item();
            Fitem.ShowDialog();
        }

        public bool FindDiscountLimit()
        {
            DiscountLimitOccured = false;
            try
            {
                if (HasDiscountLimit)
                {
                    if (!HasSeasonDiscount)
                    {
                        if (DiscType == "Percentage")
                        {
                            if (Convert.ToDouble(ITEM_DISCOUNT.Text) > Discountpercentlimit)
                            {
                                MessageBox.Show("Discount Amount exeed limt");
                                ITEM_DISCOUNT.Text = "0.00";
                                ITEM_DISCOUNT.Focus();
                                DiscountLimitOccured = true;

                            }
                        }
                        else
                        {
                            if (Convert.ToDouble(ITEM_DISCOUNT.Text) > DiscountAmtlimit)
                            {
                                MessageBox.Show("Discount Amount exeed limt");
                                ITEM_DISCOUNT.Text = "0.00";
                                ITEM_DISCOUNT.Focus();
                                DiscountLimitOccured = true;

                            }
                        }
                    }
                }

                return DiscountLimitOccured;

            }
            catch (Exception)
            {
                return DiscountLimitOccured;
                // MessageBox.Show(ee.Message);
            }
        }
        
        private void ITEM_DISCOUNT_Leave(object sender, EventArgs e)
        {          
                if (Disctext.Text == "Disc Amt")
                {
                    if (ITEM_DISCOUNT.Text == string.Empty)
                    {
                        ITEM_DISCOUNT.Text = decimalFormat;
                    }
                    value = Convert.ToDouble(ITEM_DISCOUNT.Text);
                    amt = Convert.ToDouble(GROSS_TOTAL.Text) * (Convert.ToDouble(value) / 100);
                }
                else
                {
                    if (ITEM_DISCOUNT.Text == string.Empty)
                    {
                        ITEM_DISCOUNT.Text = "0";
                    }
                    value = Convert.ToDouble(ITEM_DISCOUNT.Text);
                    double grossTotal;
                    try
                    {
                        grossTotal = Convert.ToDouble(GROSS_TOTAL.Text);
                    }
                    catch (Exception)
                    {
                        grossTotal = 0;
                    }
                    percent = (Convert.ToDouble(value) * 100) / grossTotal;
                }
            
            
        }

        private void btnaddcategory_Click(object sender, EventArgs e)
        {
            CustomerMaster cm = new CustomerMaster();
            if (cm.ShowDialog() != DialogResult.Yes)
            {
                bindledgers();
            }
         
        }

        private void DOC_DATE_GRE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {               
                if (Focus_Rate_Type)
                {
                    ActiveControl = RATE_CODE;                    
                }
                else if (Focus_Sale_Type)
                {
                    ActiveControl = cmbInvType;
                    //cmbInvType.Focus();
                }
                else if (hasBarcode)
                {

                    if (SalebyBarcode)
                    {
                        ActiveControl = BARCODE;
                    }
                    else
                    {
                        ActiveControl = ITEM_NAME;
                    }
                }
                else if (SalebyItemName)
                {
                    ActiveControl = ITEM_NAME;
                }
                else if (SalebyItemCode)
                {
                    ActiveControl = ITEM_CODE;
                }
                else
                {
                    ActiveControl = ITEM_NAME;
                }
            }
        }

        public string TempDiscount = "";
        private void chkCustomeleveldiscount_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCustomeleveldiscount.Checked)
            {
                if (CUSTOMER_CODE.Text != "")
                {
                    GetCustomerLevelDiscount(CUSTOMER_CODE.Text);
                    //   TempDiscount = DISCOUNT.Text;
                    if (lblCustomerDiscType.Text != "Amount")
                    {
                        DISCOUNT.Text = ((Convert.ToDecimal(TOTAL_AMOUNT.Text) * Convert.ToDecimal(lblCustomerDisctValue.Text)) / 100).ToString();
                    }
                    else
                    {
                        DISCOUNT.Text = lblCustomerDisctValue.Text;
                    }
                }
            }
            else
            {
                lblCustomerDisctValue.Text = "0.00";
                lblCustomerDiscType.Text = "Type";
                DISCOUNT.Text = TempDiscount;
            }
        }

        private void DISCOUNT_TextChanged(object sender, EventArgs e)
        {
            double netAmount;
            if (DISCOUNT.Text.Trim() != "" && TOTAL_AMOUNT.Text.Trim() != "")
            {
                if (kryptonLabel25.Text == "Disc %")
                {
                    netAmount = (Convert.ToDouble(TOTAL_AMOUNT.Text) - (Convert.ToDouble(TOTAL_AMOUNT.Text) * (Convert.ToDouble(DISCOUNT.Text) / 100)) );
                    DiscAmt = Convert.ToDouble(TOTAL_AMOUNT.Text) * (Convert.ToDouble(DISCOUNT.Text) / 100);
                }
                else
                {
                    netAmount = (Convert.ToDouble(TOTAL_AMOUNT.Text) - Convert.ToDouble(DISCOUNT.Text));
                    DiscAmt = Convert.ToDouble(DISCOUNT.Text);
                }
                NET_AMOUNT.Text = netAmount.ToString(decimalFormat);
            }

            if (DISCOUNT.Text == "")
            {
                NET_AMOUNT.Text = TOTAL_AMOUNT.Text;
            }
            if (txtFreight.Text.Trim() != "")
            {
                NET_AMOUNT.Text = (Convert.ToDouble(NET_AMOUNT.Text) + Convert.ToDouble(txtFreight.Text)).ToString(decimalFormat);
            }
            chkRoundOff.CheckedChanged += chkRoundOff_CheckedChanged;
        }

        private void ITEM_TOTAL_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (ITEM_TOTAL.Text.Trim() != "" && QUANTITY.Text.Trim() != "")
                {
                    lblProfit.Text = (Convert.ToDecimal(ITEM_TOTAL.Text) - (Convert.ToDecimal(QUANTITY.Text) * PurchasePrice)).ToString();
                }
            }
            catch (Exception ex)
            {
                string st = ex.Message;
            }
        }

        public void GetItemStockvalue(string Code, string Unit)
        {
            lblstock.Text = "";
            DataTable dt = new DataTable();
            Stkr.Code = Code;
            Stkr.Unit = Unit;
            dt = Stkr.GetItemStockValue();
            if (dt.Rows.Count > 0)
            {
                lblstock.Text = dt.Rows[0][6].ToString();
            }
        }

        private void SALESACC_KeyDown(object sender, KeyEventArgs e)
        {
            SALESACC.DroppedDown = false;
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                CASHACC.Focus();
            }
        }

        private void CASHACC_KeyDown(object sender, KeyEventArgs e)
        {
            CASHACC.DroppedDown = false;
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (SalebyBarcode)
                {
                    BARCODE.Focus();
                }
                else if (SalebyItemName)
                {
                    ITEM_NAME.Focus();
                }
                else if (SalebyItemCode)
                {
                    ITEM_CODE.Focus();
                }
                else
                {
                    ITEM_NAME.Focus();
                }

            }
        }

        public void customer(string cus)
        {
            if (!cus.Equals(""))
            {
                SqlCommand cmd1 = new SqlCommand("SELECT * from REC_CUSTOMER where CODE =" + cus, conn);
                SqlDataAdapter Adap = new SqlDataAdapter();
                DataTable dt1 = new DataTable();
                Adap.SelectCommand = cmd1;
                Adap.Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    tele_no = Convert.ToString(dt1.Rows[0]["TELE1"].ToString());
                    phone_no = Convert.ToString(dt1.Rows[0]["MOBILE"].ToString());
                    tin_no = Convert.ToString(dt1.Rows[0]["TIN_NO"].ToString());
                    ADDRESS_A = Convert.ToString(dt1.Rows[0]["ADDRESS_A"].ToString());
                    //tin_no = rdd[2].ToString();
                    //address = rdd[3].ToString();
                }
            }
            // rdd.Close();
        }

        public void checkvoucher(int cvouch)
        {
            Decimal vouchmax;
            Decimal vouchmin;
            bool flagMax;
            bool flagMin;
            //btnup.Enabled = false;
            //btndown.Enabled = false;
            if (cmbInvType.SelectedIndex == 0)
            {
                DataTable dt2 = new DataTable();
                cmd.CommandText = "SELECT MAX(DOC_ID) AS Expr1 FROM INV_SALES_HDR WHERE (SALE_TYPE IS NULL OR SALE_TYPE='') AND FLAGDEL='True'";
                SqlDataAdapter adapter2 = new SqlDataAdapter(cmd);
                adapter2.Fill(dt2);

                DataTable dt3 = new DataTable();
                cmd.CommandText = "SELECT MIN(DOC_ID) AS Expr1 FROM INV_SALES_HDR WHERE (SALE_TYPE IS NULL OR SALE_TYPE='') AND FLAGDEL='True'";
                SqlDataAdapter adapter3 = new SqlDataAdapter(cmd);
                adapter3.Fill(dt3);
                if (dt2.Rows[0][0] != DBNull.Value)
                {
                    vouchmax = Convert.ToDecimal(dt2.Rows[0][0]);
                    flagMax = true;
                }
                else
                {
                    vouchmax = 0;
                    flagMax = false;
                }

                if (dt3.Rows[0][0] != DBNull.Value)
                {
                    vouchmin = Convert.ToDecimal(dt3.Rows[0][0]);
                    flagMin = true;
                }
                else
                {
                    vouchmin = 0;
                    flagMin = false;
                }
            }
            else
            {
                DataTable dt1 = new DataTable();
                cmd = new SqlCommand("SELECT CODE FROM GEN_SALE_TYPE WHERE DESC_ENG='" + cmbInvType.Text + "'", conn);
                //cmd.CommandType = CommandType.Text;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt1);

                DataTable dt2 = new DataTable();
                cmd.CommandText = "SELECT MAX(DOC_ID) AS Expr1 FROM INV_SALES_HDR WHERE SALE_TYPE='" + cmbInvType.SelectedValue + "' AND FLAGDEL='True'";
                SqlDataAdapter adapter2 = new SqlDataAdapter(cmd);
                adapter2.Fill(dt2);

                DataTable dt3 = new DataTable();
                cmd.CommandText = "SELECT MIN(DOC_ID) AS Expr1 FROM INV_SALES_HDR WHERE SALE_TYPE = '" + cmbInvType.SelectedValue + "' AND FLAGDEL='True'";
                SqlDataAdapter adapter3 = new SqlDataAdapter(cmd);
                adapter3.Fill(dt3);

                DataTable dt = new DataTable();
                cmd.CommandText = "SELECT InvoiceStart FROM GEN_INV_STARTFROM WHERE InvoiceTypeCode='" + dt1.Rows[0][0].ToString() + "'";
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter adapter1 = new SqlDataAdapter(cmd);
                adapter1.Fill(dt);
                if (dt2.Rows[0][0] != DBNull.Value)
                {
                    vouchmax = Convert.ToDecimal(dt2.Rows[0][0]);
                    flagMax = true;
                }
                else
                {
                    vouchmax = Convert.ToDecimal(dt.Rows[0][0]);
                    flagMax = false;
                }
                if (dt3.Rows[0][0] != DBNull.Value)
                {
                    vouchmin = Convert.ToDecimal(dt3.Rows[0][0]);
                    flagMin = true;
                }
                else
                {
                    vouchmin = Convert.ToDecimal(dt.Rows[0][0]);
                    flagMin = false;
                }
               
            }

            if (cvouch > vouchmax && flagMax)
            {
                btnup.Enabled = false;
                btndown.Enabled = true;
            }
            else if (cvouch <= vouchmin && flagMin)
            {
                btnup.Enabled = true;
                btndown.Enabled = false;
            }
            else if ((cvouch <= vouchmax && flagMax) || (cvouch >= vouchmin && flagMin))
            {
                btnup.Enabled = true;
                btndown.Enabled = true;
            }

            if (!flagMin || !flagMax)
            {
                btnup.Enabled = false;
                btndown.Enabled = false;
            }

        }

        private void btnup_Click(object sender, EventArgs e)
        {
            double grossTotal1 = 0, discTotal1 = 0, netTotal1 = 0, taxTotal1 = 0, itemTotal1 = 0;
            double taxTot, vatTot, discTot, amtTot, netTotAmt;
            double round, freight, cess;
            Clear2();
            try
            {
                VOUCHNUM.Text = (Convert.ToInt32(VOUCHNUM.Text) + 1).ToString();
                checkvoucher(Convert.ToInt16(VOUCHNUM.Text));
                DataTable dt = new DataTable();

                if (cmbInvType.SelectedIndex == 0)
                {
                    cmd.CommandText = "SELECT * FROM INV_SALES_HDR WHERE DOC_ID = '" + VOUCHNUM.Text + "' AND FLAGDEL='TRUE' AND (SALE_TYPE IS NULL OR SALE_TYPE='')";
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter Adap = new SqlDataAdapter();
                    Adap.SelectCommand = cmd;
                    Adap.Fill(dt);
                }
                else
                {
                    cmd.CommandText = "SELECT * FROM INV_SALES_HDR WHERE DOC_ID = '" + VOUCHNUM.Text + "' AND FLAGDEL='TRUE' AND SALE_TYPE='" + cmbInvType.SelectedValue + "'";
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter Adap = new SqlDataAdapter();
                    Adap.SelectCommand = cmd;
                    Adap.Fill(dt);
                }


                if (dt.Rows.Count > 0)
                {                    
                        ID = Convert.ToString(dt.Rows[0]["DOC_NO"]);
                        DOC_NO.Text = ID;
                        DOC_DATE_GRE.Text = Convert.ToDateTime(Convert.ToString(dt.Rows[0]["DOC_DATE_GRE"].ToString())).ToShortDateString();
                        DOC_DATE_HIJ.Text = Convert.ToString(dt.Rows[0]["DOC_DATE_HIJ"]);
                        CURRENCY_CODE.Text = Convert.ToString(dt.Rows[0]["CURRENCY_CODE"]);
                        DOC_REFERENCE.Text = Convert.ToString(dt.Rows[0]["DOC_REFERENCE"]);
                        CUSTOMER_CODE.Text = Convert.ToString(dt.Rows[0]["CUSTOMER_CODE"]);
                        GetLedgerId(Convert.ToString(dt.Rows[0]["CUSTOMER_CODE"]));
                        CUSTOMER_NAME.Text = Convert.ToString(dt.Rows[0]["CUSTOMER_NAME_ENG"]);
                        SALESMAN_CODE.Text = Convert.ToString(dt.Rows[0]["SALESMAN_CODE"]);
                        NOTES.Text = Convert.ToString(dt.Rows[0]["NOTES"]);
                        taxTot = Convert.ToDouble(dt.Rows[0]["TAX_TOTAL"]);
                        vatTot = Convert.ToDouble(dt.Rows[0]["VAT"]);
                        discTot = Convert.ToDouble(dt.Rows[0]["DISCOUNT"]);
                        amtTot = Convert.ToDouble(dt.Rows[0]["TOTAL_AMOUNT"]);
                        netTotAmt = Convert.ToDouble(dt.Rows[0]["NET_AMOUNT"]);
                        round = Convert.ToDouble(dt.Rows[0]["ROUNDOFF"]);
                        freight = Convert.ToDouble(dt.Rows[0]["FREIGHT"]);
                        cess = Convert.ToDouble(dt.Rows[0]["CESS"]);

                        TAX_TOTAL.Text = taxTot.ToString(decimalFormat);
                        VATT.Text = vatTot.ToString(decimalFormat);
                        DISCOUNT.Text = discTot.ToString(decimalFormat);
                        TOTAL_AMOUNT.Text = amtTot.ToString(decimalFormat);
                        NET_AMOUNT.Text = netTotAmt.ToString(decimalFormat);
                        txtRoundOff.Text = round.ToString(decimalFormat);
                        txtFreight.Text = freight.ToString(decimalFormat);
                        TXT_CESS.Text = cess.ToString(decimalFormat);

                        PAY_CODE.Text = Convert.ToString(dt.Rows[0]["PAY_CODE"]);
                        PAY_NAME.Text = General.getName(PAY_CODE.Text, "GEN_PAYTYPE");
                        CARD_NO.Text = Convert.ToString(dt.Rows[0]["CARD_NO"]);
                        //txtRoundOff.Text = Convert.ToString(dt.Rows[0]["ROUNDOFF"]);
                        //txtFreight.Text = Convert.ToString(dt.Rows[0]["FREIGHT"]);
                        //TXT_CESS.Text = Convert.ToString(dt.Rows[0]["CESS"]);
                        cmbInvType.SelectedIndexChanged -= cmbInvType_SelectedIndexChanged;
                        cmbInvType.SelectedValue = Convert.ToString(dt.Rows[0]["SALE_TYPE"]);
                        cmbInvType.SelectedIndexChanged += cmbInvType_SelectedIndexChanged;
                        type = Convert.ToString(dt.Rows[0]["DOC_TYPE"]);

                        conn.Open();
                        //cmd.CommandText = "SELECT dtl.*, PAY_SUPPLIER.DESC_ENG AS supplier_name FROM INV_SALES_DTL as dtl LEFT JOIN PAY_SUPPLIER ON dtl.supplier_id = PAY_SUPPLIER.CODE WHERE DOC_NO = '" + DOC_NO.Text + "'AND FLAGDEL='TRUE'";
                        cmd.CommandText = "SELECT dtl.*, PAY_SUPPLIER.DESC_ENG AS supplier_name, g.DESC_ENG AS [Group] FROM INV_SALES_DTL as dtl LEFT JOIN PAY_SUPPLIER ON dtl.supplier_id = PAY_SUPPLIER.CODE LEFT JOIN INV_ITEM_GROUP as g ON dtl.group_id = g.CODE WHERE DOC_NO = '" + DOC_NO.Text + "'AND FLAGDEL='TRUE'";
                        cmd.CommandType = CommandType.Text;
                        SqlDataReader r = cmd.ExecuteReader();
                        while (r.Read())
                        {
                            double price, taxAmt, taxpercent;
                            double grTotal, itemDisc, itemTot;
                            double purPrice, discValue, netTot, mrp;
                            //double taxAmt;
                            //DataTable table = DecSet.getDecimalPoint();

                            int i = dgItems.Rows.Add(new DataGridViewRow());
                            DataGridViewCellCollection c = dgItems.Rows[i].Cells;
                            c["cSl"].Value = i + 1;
                            c["cCode"].Value = r["ITEM_CODE"];
                            c["cName"].Value = r["ITEM_DESC_ENG"];
                            if (hasBatch)
                            {
                                c["cBatch"].Value = r["BATCH"];
                                if (r["EXPIRY_DATE"].ToString() != "")
                                c["cExpDate"].Value = Convert.ToDateTime(r["EXPIRY_DATE"]).ToString("dd/MM/yyyy");
                            }
                            c["cUnit"].Value = r["UOM"];
                            c["cQty"].Value = r["QUANTITY"];
                            c["DiscTypes"].Value = r["DISC_TYPE"];
                            price = Convert.ToDouble(r["PRICE"]);
                            grTotal = Convert.ToDouble(r["GROSS_TOTAL"]);
                            itemDisc = Convert.ToDouble(r["ITEM_DISCOUNT"]);
                            itemTot = Convert.ToDouble(r["ITEM_TOTAL"]);
                            discValue = Convert.ToDouble(r["DISC_VALUE"]);
                            mrp = Convert.ToDouble(r["MRP"]);
                            purPrice = Convert.ToDouble((r["Pur_Price"] == DBNull.Value) ? "0" : r["Pur_Price"]);
                            netTot = grTotal - itemDisc;

                            c["cPrice"].Value = price.ToString(decimalFormat);
                            c["cGTotal"].Value = grTotal.ToString(decimalFormat);
                            c["cDisc"].Value = itemDisc.ToString(decimalFormat);
                            c["cTotal"].Value = itemTot.ToString(decimalFormat);
                            c["Pur"].Value = purPrice.ToString(decimalFormat);
                            c["cNetValue"].Value = netTot.ToString(decimalFormat);
                            c["DiscValues"].Value = discValue.ToString(decimalFormat);
                            c["cMrp"].Value = mrp.ToString(decimalFormat);
                            c["Cfree"].Value = r["FREE"];
                            if (hasTax)
                            {
                                taxpercent = Convert.ToDouble(r["ITEM_TAX_PER"]);
                                c["cTaxPer"].Value = taxpercent.ToString(decimalFormat);
                                taxAmt = Convert.ToDouble(r["ITEM_TAX"]);
                                c["cTaxAmt"].Value = taxAmt.ToString(decimalFormat);
                            }
                            
                            grossTotal1 += Convert.ToDouble(c["cGTotal"].Value);
                            discTotal1 += Convert.ToDouble(c["cDisc"].Value);
                            netTotal1 += Convert.ToDouble(c["cNetValue"].Value);
                            taxTotal1 += Convert.ToDouble(c["cTaxAmt"].Value);
                            itemTotal1 += Convert.ToDouble(c["cTotal"].Value);

                            txtGrossTotal.Text = grossTotal1.ToString(decimalFormat);
                            txtLineDiscTotal.Text = discTotal1.ToString(decimalFormat);
                            txtNetTotal.Text = netTotal1.ToString(decimalFormat);
                            txtTaxTotal.Text = taxTotal1.ToString(decimalFormat);
                            txtItemTotal.Text = itemTotal1.ToString(decimalFormat);

                            c["cDescArb"].Value = r["ITEM_DESC_ARB"];
                            c["SerialNos"].Value = r["SERIALNO"];
                            c["uomQty"].Value = r["UOM_QTY"];
                            c["cost_price"].Value = r["cost_price"];
                            c["supplier_id"].Value = r["supplier_id"];
                            c["supplier_name"].Value = r["supplier_name"];
                            c["uHSNNO"].Value = r["group_id"];

                            //--not validated--//
                            if (r["PRICE_BATCH"].ToString() != "")
                            {
                                c["colBATCH"].Value = r["PRICE_BATCH"];
                            }

                        }
                        conn.Close();

                        if (CUSTOMER_CODE.Text != "")
                        {
                            chkCustomeleveldiscount.Enabled = true;

                            chkCustomeleveldiscount.Checked = Convert.ToBoolean(dt.Rows[0]["CUSLEVELDISCOUNT"]);
                        }

                        customer(CUSTOMER_CODE.Text);
                        if (dgItems.Rows.Count > 0)
                        {
                            calculateGSTTaxes();
                        }
                }
                else
                {
                    if (btndown.Enabled && btnup.Enabled)
                    {
                        MessageBox.Show("This invoice has been deleted");
                    }                      
                }
                
            }
            catch(Exception)
            {
                MessageBox.Show("Textbox doesn't contains voucher number");
                GetMaxDocID();
            }
        }

        private void btndown_Click(object sender, EventArgs e)
        {
            double grossTotal1 = 0, discTotal1 = 0, netTotal1 = 0, taxTotal1 = 0, itemTotal1 = 0;
            double taxTot, vatTot, discTot, amtTot, netTotAmt;
            double round, freight, cess;
            Clear2();
            try
            {
                VOUCHNUM.Text = (Convert.ToInt32(VOUCHNUM.Text) - 1).ToString();
                checkvoucher(Convert.ToInt16(VOUCHNUM.Text));

                DataTable dt = new DataTable();
                if (cmbInvType.SelectedIndex == 0)
                {
                    cmd.CommandText = "SELECT * FROM INV_SALES_HDR WHERE DOC_ID = '" + VOUCHNUM.Text + "'AND FLAGDEL='TRUE' AND (SALE_TYPE IS NULL OR SALE_TYPE='')";
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter Adap = new SqlDataAdapter();
                    Adap.SelectCommand = cmd;
                    Adap.Fill(dt);
                }
                else
                {
                    cmd.CommandText = "SELECT * FROM INV_SALES_HDR WHERE DOC_ID = '" + VOUCHNUM.Text + "'AND FLAGDEL='TRUE' AND SALE_TYPE= '" + cmbInvType.SelectedValue + "'";
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter Adap = new SqlDataAdapter();
                    Adap.SelectCommand = cmd;
                    Adap.Fill(dt);
                }
                if (dt.Rows.Count > 0)
                {                   
                        ID = dt.Rows[0]["DOC_NO"].ToString();
                        DOC_NO.Text = ID;
                        DOC_DATE_GRE.Text = Convert.ToDateTime(Convert.ToString(dt.Rows[0]["DOC_DATE_GRE"].ToString())).ToShortDateString();
                        DOC_DATE_HIJ.Text = Convert.ToString(dt.Rows[0]["DOC_DATE_HIJ"]);
                        CURRENCY_CODE.Text = Convert.ToString(dt.Rows[0]["CURRENCY_CODE"]);
                        DOC_REFERENCE.Text = Convert.ToString(dt.Rows[0]["DOC_REFERENCE"]);
                        CUSTOMER_CODE.Text = Convert.ToString(dt.Rows[0]["CUSTOMER_CODE"]);
                        GetLedgerId(Convert.ToString(dt.Rows[0]["CUSTOMER_CODE"]));
                        CUSTOMER_NAME.Text = Convert.ToString(dt.Rows[0]["CUSTOMER_NAME_ENG"]);
                        SALESMAN_CODE.Text = Convert.ToString(dt.Rows[0]["SALESMAN_CODE"]);
                        NOTES.Text = Convert.ToString(dt.Rows[0]["NOTES"]);
                        taxTot = Convert.ToDouble(dt.Rows[0]["TAX_TOTAL"]);
                        vatTot = Convert.ToDouble(dt.Rows[0]["VAT"]);
                        discTot = Convert.ToDouble(dt.Rows[0]["DISCOUNT"]);
                        amtTot = Convert.ToDouble(dt.Rows[0]["TOTAL_AMOUNT"]);
                        netTotAmt = Convert.ToDouble(dt.Rows[0]["NET_AMOUNT"]);
                        round = Convert.ToDouble(dt.Rows[0]["ROUNDOFF"]);
                        freight = Convert.ToDouble(dt.Rows[0]["FREIGHT"]);
                        cess = Convert.ToDouble(dt.Rows[0]["CESS"]);
                        type = Convert.ToString(dt.Rows[0]["DOC_TYPE"]);

                        TAX_TOTAL.Text = taxTot.ToString(decimalFormat);
                        VATT.Text = vatTot.ToString(decimalFormat);
                        DISCOUNT.Text = discTot.ToString(decimalFormat);
                        TOTAL_AMOUNT.Text = amtTot.ToString(decimalFormat);
                        NET_AMOUNT.Text = netTotAmt.ToString(decimalFormat);
                        txtRoundOff.Text = round.ToString(decimalFormat);
                        txtFreight.Text = freight.ToString(decimalFormat);
                        TXT_CESS.Text = cess.ToString(decimalFormat);

                        PAY_CODE.Text = Convert.ToString(dt.Rows[0]["PAY_CODE"]);
                        PAY_NAME.Text = General.getName(PAY_CODE.Text, "GEN_PAYTYPE");
                        CARD_NO.Text = Convert.ToString(dt.Rows[0]["CARD_NO"]);
                        cmbInvType.SelectedIndexChanged -= cmbInvType_SelectedIndexChanged;
                        string sale_type = Convert.ToString(dt.Rows[0]["SALE_TYPE"]);
                        if (sale_type.Equals(""))
                        {
                            cmbInvType.SelectedIndex = 0;
                        }
                        else
                        {
                            cmbInvType.SelectedValue = sale_type;
                        }
                        cmbInvType.SelectedIndexChanged += cmbInvType_SelectedIndexChanged;
                        //try
                        //{
                        conn.Open();
                        //cmd.CommandText = "SELECT dtl.*, PAY_SUPPLIER.DESC_ENG AS supplier_name FROM INV_SALES_DTL as dtl LEFT JOIN PAY_SUPPLIER ON dtl.supplier_id = PAY_SUPPLIER.CODE WHERE DOC_NO = '" + DOC_NO.Text + "'AND FLAGDEL='TRUE'";
                        cmd.CommandText = "SELECT dtl.*, PAY_SUPPLIER.DESC_ENG AS supplier_name, g.DESC_ENG AS [Group] FROM INV_SALES_DTL as dtl LEFT JOIN PAY_SUPPLIER ON dtl.supplier_id = PAY_SUPPLIER.CODE LEFT JOIN INV_ITEM_GROUP as g ON dtl.group_id = g.CODE WHERE DOC_NO = '" + DOC_NO.Text + "'AND FLAGDEL='TRUE'";
                        cmd.CommandType = CommandType.Text;
                        SqlDataReader r = cmd.ExecuteReader();
                        while (r.Read())
                        {
                            double price, taxAmt, taxpercent;
                            double grTotal, itemDisc, itemTot;
                            double purPrice, discValue, netTot, mrp;
                            //string discType;


                            int i = dgItems.Rows.Add(new DataGridViewRow());
                            DataGridViewCellCollection c = dgItems.Rows[i].Cells;
                            c["cSl"].Value = i + 1;
                            c["cCode"].Value = r["ITEM_CODE"];
                            c["cName"].Value = r["ITEM_DESC_ENG"];
                            if (hasBatch)
                            {
                                c["cBatch"].Value = r["BATCH"];
                                if (r["EXPIRY_DATE"].ToString() != "")
                                c["cExpDate"].Value = Convert.ToDateTime(r["EXPIRY_DATE"]).ToString("dd/MM/yyyy");
                            }
                            c["cUnit"].Value = r["UOM"];
                            c["cQty"].Value = r["QUANTITY"];
                            c["DiscTypes"].Value = r["DISC_TYPE"];
                            //c["DiscValues"].Value = r["DISC_VALUE"];
                            price = Convert.ToDouble(r["PRICE"]);
                            grTotal = Convert.ToDouble(r["GROSS_TOTAL"]);
                            itemDisc = Convert.ToDouble(r["ITEM_DISCOUNT"]);
                            itemTot = Convert.ToDouble(r["ITEM_TOTAL"]);
                            discValue = Convert.ToDouble(r["DISC_VALUE"]);
                            mrp = Convert.ToDouble(r["MRP"]);
                            purPrice = Convert.ToDouble((r["Pur_Price"] == DBNull.Value) ? "0" : r["Pur_Price"]);
                            netTot = grTotal - itemDisc;


                            c["cPrice"].Value = price.ToString(decimalFormat);
                            c["cGTotal"].Value = grTotal.ToString(decimalFormat);
                            c["cDisc"].Value = itemDisc.ToString(decimalFormat);
                            c["cTotal"].Value = itemTot.ToString(decimalFormat);
                            c["Pur"].Value = purPrice.ToString(decimalFormat);
                            c["cNetValue"].Value = netTot.ToString(decimalFormat);
                            c["DiscValues"].Value = discValue.ToString(decimalFormat);
                            c["cMrp"].Value = mrp.ToString(decimalFormat);
                            c["Cfree"].Value = r["FREE"];
                            if (hasTax)
                            {
                                taxpercent = Convert.ToDouble(r["ITEM_TAX_PER"]);
                                c["cTaxPer"].Value = taxpercent.ToString(decimalFormat);
                                taxAmt = Convert.ToDouble(r["ITEM_TAX"]);
                                c["cTaxAmt"].Value = taxAmt.ToString(decimalFormat);
                            }
                            c["uomQty"].Value = r["UOM_QTY"];
                            c["cost_price"].Value = r["cost_price"];
                            c["supplier_id"].Value = r["supplier_id"];
                            c["supplier_name"].Value = r["supplier_name"];

                            grossTotal1 += Convert.ToDouble(c["cGTotal"].Value);
                            discTotal1 += Convert.ToDouble(c["cDisc"].Value);
                            netTotal1 += Convert.ToDouble(c["cNetValue"].Value);
                            taxTotal1 += Convert.ToDouble(c["cTaxAmt"].Value);
                            itemTotal1 += Convert.ToDouble(c["cTotal"].Value);

                            txtGrossTotal.Text = grossTotal1.ToString(decimalFormat);
                            txtLineDiscTotal.Text = discTotal1.ToString(decimalFormat);
                            txtNetTotal.Text = netTotal1.ToString(decimalFormat);
                            txtTaxTotal.Text = taxTotal1.ToString(decimalFormat);
                            txtItemTotal.Text = itemTotal1.ToString(decimalFormat);


                            c["cDescArb"].Value = r["ITEM_DESC_ARB"];
                            c["SerialNos"].Value = r["SERIALNO"];
                            c["uHSNNO"].Value = r["group_id"];
                            //--not validated--//

                            if (r["PRICE_BATCH"].ToString() != "")
                            {
                                c["colBATCH"].Value = r["PRICE_BATCH"];
                            }
                        }
                        conn.Close();
                        if (CUSTOMER_CODE.Text != "")
                        {
                            chkCustomeleveldiscount.Enabled = true;

                            chkCustomeleveldiscount.Checked = Convert.ToBoolean(dt.Rows[0]["CUSLEVELDISCOUNT"]);
                        }

                        customer(CUSTOMER_CODE.Text);
                        if (dgItems.Rows.Count > 0)
                        {
                            calculateGSTTaxes();
                        }
                }
                else
                {
                    if (btndown.Enabled && btnup.Enabled)
                    {
                        MessageBox.Show("This invoice has been deleted");
                    }
                }
            }
            catch(Exception)
            {
                MessageBox.Show("Textbox doesn't contains voucher number");
                GetMaxDocID();
            }
        }

        private void ITEM_NAME_Click(object sender, EventArgs e)
        {
            if (ITEM_NAME.IsActive)
            {
                if (ITEM_NAME.SelectionLength == 0)
                    // Select all text in the text box.  
                    //     ITEM_NAME.Focus();
                    ITEM_NAME.SelectAll();
                ITEM_NAME.SelectionLength = ITEM_NAME.Text.Length;
            }
        }

        private void CMBTYPE_SelectedIndexChanged(object sender, EventArgs e)
        {
            KryptonComboBox cmb = new KryptonComboBox();
            cmb = (sender as KryptonComboBox);

            if (cmb.Text == "")
            {
                //  PNL_DATAGRIDITEM.Visible = false;
                //  firstitemlistbyname = true;
                source.Filter = string.Format("TYPE LIKE '%{0}%' ", "");
            }
            else
            {
                PNL_DATAGRIDITEM.Visible = true;
                if (cmb.Name == "CMBTYPE")
                {
                    source.Filter = string.Format("TYPE LIKE '%{0}%' ", cmb.Text);
                }
                else if (cmb.Name == "DrpCategory")
                {
                    source.Filter = string.Format("CATEGORY LIKE '%{0}%' ", cmb.Text);
                }
                else if (cmb.Name == "Group")
                {
                    source.Filter = string.Format("GROUP LIKE '%{0}%' ", cmb.Text);
                }
                else if (cmb.Name == "Trademark")
                {
                    source.Filter = string.Format("TRADEMARK LIKE '%{0}%' ", cmb.Text);
                }
                else
                {
                }
                dataGridItem.ClearSelection();
            }
        }

        private void CMBTYPE_TextChanged(object sender, EventArgs e)
        {
            KryptonComboBox cmb = new KryptonComboBox();
            cmb = (sender as KryptonComboBox);

            if (cmb.Text == "")
            {
                //  PNL_DATAGRIDITEM.Visible = false;
                //  firstitemlistbyname = true;
                source.Filter = string.Format("TYPE LIKE '%{0}%' ", "");
            }
            else
            {
                PNL_DATAGRIDITEM.Visible = true;
                if (cmb.Name == "CMBTYPE")
                {
                    source.Filter = string.Format("TYPE LIKE '%{0}%' ", cmb.Text);
                }
                else if (cmb.Name == "DrpCategory")
                {
                    source.Filter = string.Format(" CATEGORY LIKE '%{0}%' ", cmb.Text);
                }
                else if (cmb.Name == "Group")
                {
                    source.Filter = string.Format("GROUP LIKE '%{0}%' ", cmb.Text);
                }
                else if (cmb.Name == "Trademark")
                {
                    source.Filter = string.Format("TRADEMARK LIKE '%{0}%' ", cmb.Text);
                }
                else
                {
                }
                dataGridItem.ClearSelection();
            }
        }

        private void CMBTYPE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (dataGridItem.Rows.Count > 0)
                {
                    dataGridItem.Focus();
                    dataGridItem.CurrentCell = dataGridItem.Rows[0].Cells[1];
                }
            }
        }
        StockEntry stockEntry = new StockEntry();
        private void dataGridItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (!PNL_DATAGRIDITEM.Visible)
            //{
            //    PNL_DATAGRIDITEM.Visible = true;
            //}
            //else if (dataGridItem.CurrentRow != null)
            //{
            //    ShowStock = false;
            //    itemSelected = true;
            //    string itemcode = dataGridItem.CurrentRow.Cells["ITEM_CODE"].Value.ToString();
            //    ITEM_CODE.Text = itemcode;
            //    PurchasePrice = Convert.ToDecimal(dataGridItem.CurrentRow.Cells["PUR"].Value);
            //    String rateType = Convert.ToString(RATE_CODE.SelectedValue);
            //    string pricedecimal = dataGridItem.CurrentRow.Cells[rateType].Value.ToString();
            //    double pricedec = Convert.ToDouble(pricedecimal);
            //    //PRICE.Text = pricedec.ToString("N3");
            //    PRICE.Text = dataGridItem.CurrentRow.Cells[rateType].Value.ToString();
            //    //PRICE.Text = (dataGridItem.CurrentRow.Cells["SALES"].Value.ToString("N3");
            //    HASSERIAL = Convert.ToBoolean(dataGridItem.CurrentRow.Cells["HASSERIAL"].Value);
            //    PNLSERIAL.Visible = HASSERIAL;
            //    mrp = Convert.ToDouble(dataGridItem.CurrentRow.Cells["MRP"].Value);
            //    tb_mrp.Text = mrp.ToString(decimalFormat);
            //    kryptonLabel36.Visible = HASSERIAL;
            //    SERIALNO.Visible = HASSERIAL;
            //    kryptonLabel36.Visible = HASSERIAL;
            //    SERIALNO.Visible = HASSERIAL;

            //    //if (hasBatch)
            //    //{
            //    //    BATCH.Focus();
            //    //}
            //    //else
            //    //{
            //    QUANTITY.Text = "1";
            //    if (SalebyItemCode)
            //        ITEM_CODE.Focus();
            //    else if (MoveToUnit)
            //        UOM.Focus();
            //    else if (MoveToQty)
            //        QUANTITY.Focus();
            //    else if (HASSERIAL)
            //        SERIALNO.Focus();
            //    else if (txtfree.Visible)
            //        txtfree.Focus();
            //    else if (MoveToPrice)
            //        PRICE.Focus();
            //    else if (tb_mrp.Visible)
            //        tb_mrp.Focus();
            //    else if (GROSS_TOTAL.Visible)
            //        GROSS_TOTAL.Focus();
            //    else if (MoveToDisc)
            //        ITEM_DISCOUNT.Focus();
            //    else if (tb_netvalue.Visible)
            //        tb_netvalue.Focus();
            //    else if (tb_descr.Visible)
            //        tb_descr.Focus();
            //    else if (hasTax)
            //    {
            //        if (MoveToTaxper)
            //            ITEM_TAX_PER.Focus();
            //    }
            //    else if (hasBatch)
            //        BATCH.Focus();
            //    else
            //    {
            //        addItem();
            //        clearItem();
            //        if (SalebyItemName)
            //            ITEM_NAME.Focus();
            //        else if (SalebyItemCode)
            //            ITEM_CODE.Focus();
            //        else if (SalebyBarcode)
            //            BARCODE.Focus();
            //        else
            //            ITEM_NAME.Focus();
            //    }
            //    //    tb_descr.Focus();

            //    //}

            //    this.UOM.SelectedIndexChanged -= new EventHandler(UOM_SelectedIndexChanged);
            //    addUnits();
            //    this.UOM.SelectedIndexChanged -= new EventHandler(UOM_SelectedIndexChanged);
            //    UOM.Text = dataGridItem.CurrentRow.Cells["UNIT_CODE"].Value.ToString();
            //    TaxId = Convert.ToInt16(dataGridItem.CurrentRow.Cells["TaxId"].Value.ToString());
            //    GetTaxRate();

            //    GetDiscount();
            //    ItemArabicName = dataGridItem.CurrentRow.Cells["DESC_ARB"].Value.ToString();
            //    ITEM_NAME.Text = dataGridItem.CurrentRow.Cells["ITEM NAME"].Value.ToString();
            //    PNL_DATAGRIDITEM.Visible = false;
            //    itemSelected = false;
            //    if (RATE_CODE.Text.StartsWith("MRP"))
            //    {
            //        double taxcalc = 0;
            //        taxcalc = (Convert.ToDouble(ITEM_TAX_PER.Text) / 100) + 1;
            //        PRICE.Text = (Convert.ToDouble(PRICE.Text) / taxcalc).ToString(decimalFormat);
            //    }
            //}
             if (!PNL_DATAGRIDITEM.Visible)
                {
                    PNL_DATAGRIDITEM.Visible = true;
                }
             else if (dataGridItem.CurrentRow != null)
             {
                 string code = dataGridItem.CurrentRow.Cells["ITEM_CODE"].Value.ToString();
                 if (code != "")
                 {
                     bindBatchGrid(code);
                   
                 }
                 if (dgBatch.Visible == true || dgBatch.RowCount == 1)
                 {
                     if (dgBatch.Rows.Count > 0)
                     {
                         if (dgBatch.Rows[0].Cells["ITEM_CODE"].Value.ToString() == dataGridItem.CurrentRow.Cells["ITEM_CODE"].Value.ToString())
                         {
                             ShowStock = false;
                             itemSelected = true;
                             string itemcode = dgBatch.Rows[0].Cells["ITEM_CODE"].Value.ToString();
                             ITEM_CODE.Text = itemcode;

                             //NOT VALIDATED//
                             BARCODE.Text = dgBatch.Rows[0].Cells["BATCH_ID"].Value.ToString();

                             //NOT VALIDATED//
                             if (dgBatch.Rows[0].Cells["HSN"].Value != null)
                             {
                                 txt_HSN.Text = dgBatch.Rows[0].Cells["HSN"].Value.ToString();
                             }
                             PurchasePrice = Convert.ToDecimal(dgBatch.Rows[0].Cells["PUR"].Value);
                             String rateType = Convert.ToString(RATE_CODE.SelectedValue);
                             string pricedecimal = dgBatch.Rows[0].Cells[rateType].Value.ToString();
                             double pricedec = Convert.ToDouble(pricedecimal);
                             PRICE.TextChanged -= new EventHandler(calculateGrossAmount);
                             //PRICE.Text = pricedec.ToString("N3");
                             PRICE.Text = dgBatch.Rows[0].Cells[rateType].Value.ToString();
                             //PRICE.Text = (dataGridItem.CurrentRow.Cells["SALES"].Value.ToString("N3");
                             sales_price = Convert.ToDouble(dgBatch.Rows[0].Cells[rateType].Value);
                             HASSERIAL = Convert.ToBoolean(dgBatch.Rows[0].Cells["HASSERIAL"].Value);
                             PNLSERIAL.Visible = HASSERIAL;
                             mrp = Convert.ToDouble(dgBatch.Rows[0].Cells["MRP"].Value);
                             if (hasBatch)
                             {
                                 if (dgBatch.Rows[0].Cells["BATCH CODE"].Value != null)
                                     BATCH.Text = dgBatch.Rows[0].Cells["BATCH CODE"].Value.ToString();
                                 if (dgBatch.Rows[0].Cells["EXPIRY DATE"].Value != null && dgBatch.Rows[0].Cells["EXPIRY DATE"].Value.ToString() != "")
                                     EXPIRY_DATE.Value = Convert.ToDateTime(dgBatch.Rows[0].Cells["EXPIRY DATE"].Value);
                             }
                             tb_mrp.Text = mrp.ToString(decimalFormat);
                             kryptonLabel36.Visible = HASSERIAL;
                             SERIALNO.Visible = HASSERIAL;
                             kryptonLabel36.Visible = HASSERIAL;
                             SERIALNO.Visible = HASSERIAL;

                             Stockcheck();
                             //if (hasBatch)
                             //{
                             //    BATCH.Focus();
                             //}
                             //else
                             //{
                             QUANTITY.Text = "1";
                             TaxId = Convert.ToInt16(dgBatch.Rows[0].Cells["TaxId"].Value.ToString());
                             GetTaxRate();
                             if (SalebyItemCode)
                                 ITEM_CODE.Focus();
                             else if (MoveToUnit)
                                 UOM.Focus();
                             else if (MoveToQty)
                                 QUANTITY.Focus();
                             else if (HASSERIAL)
                                 SERIALNO.Focus();
                             else if (txtfree.Visible)
                                 txtfree.Focus();
                             else if (MoveToPrice)
                                 PRICE.Focus();
                             else if (tb_mrp.Visible)
                                 tb_mrp.Focus();
                             else if (GROSS_TOTAL.Visible)
                                 GROSS_TOTAL.Focus();
                             else if (MoveToDisc)
                                 ITEM_DISCOUNT.Focus();
                             else if (tb_netvalue.Visible)
                                 tb_netvalue.Focus();
                             else if (tb_descr.Visible)
                                 tb_descr.Focus();
                             else if (hasTax)
                             {
                                 if (MoveToTaxper)
                                     ITEM_TAX_PER.Focus();
                             }
                             else if (hasBatch)
                                 BATCH.Focus();
                             else
                             {
                                 addItem();
                                 clearItem();
                                 if (SalebyItemName)
                                     ITEM_NAME.Focus();
                                 else if (SalebyItemCode)
                                     ITEM_CODE.Focus();
                                 else if (SalebyBarcode)
                                     BARCODE.Focus();
                                 else
                                     ITEM_NAME.Focus();
                             }
                             //    tb_descr.Focus();

                             //}

                           //  this.UOM.SelectedIndexChanged -= new EventHandler(UOM_SelectedIndexChanged);
                             addUnits();
                          //   this.UOM.SelectedIndexChanged -= new EventHandler(UOM_SelectedIndexChanged);
                            // UOM.Text = dgBatch.Rows[0].Cells["UNIT_CODE"].Value.ToString();
                            

                             GetDiscount();
                             ItemArabicName = dgBatch.Rows[0].Cells["DESC_ARB"].Value.ToString();
                             ITEM_NAME.Text = dgBatch.Rows[0].Cells["ITEM NAME"].Value.ToString();
                             PNL_DATAGRIDITEM.Visible = false;
                             itemSelected = false;
                             pricefob = Convert.ToDouble(PRICE.Text);
                             if (RATE_CODE.Text.StartsWith("MRP"))
                             {
                                 double taxcalc = 0;
                                 taxcalc = (Convert.ToDouble(ITEM_TAX_PER.Text) / 100) + 1;
                                 PRICE.Text = (Convert.ToDouble(PRICE.Text) / taxcalc).ToString(decimalFormat);
                             }
                             else if (!hasSaleExclusive)
                             {
                                 double taxcalc = 0;
                                 taxcalc = (Convert.ToDouble(ITEM_TAX_PER.Text) / 100) + 1;
                                 PRICE.Text = (Convert.ToDouble(PRICE.Text) / taxcalc).ToString();
                             } 
                             PRICE.TextChanged += new EventHandler(calculateGrossAmount);
                             sales_price = Convert.ToDouble(PRICE.Text);
                             PRICE.Text = sales_price.ToString();
                         }
                     }
                 }
                 else
                 {
                     if ((StockOut = General.IsEnabled(Settings.StockOut)))
                     {
                     int id = stockEntry.MaxBatchId(code);
                     if (id > 0)
                     {
                         bindBatchGrid_forOutSale(code, id.ToString());
                         ShowStock = false;
                         itemSelected = true;
                         string itemcode = dgBatch.Rows[0].Cells["ITEM_CODE"].Value.ToString();
                         ITEM_CODE.Text = itemcode;

                         //NOT VALIDATED//
                         BARCODE.Text = dgBatch.Rows[0].Cells["BATCH_ID"].Value.ToString();

                         //NOT VALIDATED//
                         if (dgBatch.Rows[0].Cells["HSN"].Value != null)
                         {
                             txt_HSN.Text = dgBatch.Rows[0].Cells["HSN"].Value.ToString();
                         }
                         PurchasePrice = Convert.ToDecimal(dgBatch.Rows[0].Cells["PUR"].Value);
                         String rateType = Convert.ToString(RATE_CODE.SelectedValue);
                         string pricedecimal = dgBatch.Rows[0].Cells[rateType].Value.ToString();
                         double pricedec = Convert.ToDouble(pricedecimal);
                         //PRICE.Text = pricedec.ToString("N3");
                         PRICE.TextChanged -= new EventHandler(calculateGrossAmount);
                         PRICE.Text = dgBatch.Rows[0].Cells[rateType].Value.ToString();
                         sales_price = Convert.ToDouble(dgBatch.Rows[0].Cells[rateType].Value);
                         //PRICE.Text = (dataGridItem.CurrentRow.Cells["SALES"].Value.ToString("N3");
                         HASSERIAL = Convert.ToBoolean(dgBatch.Rows[0].Cells["HASSERIAL"].Value);
                         PNLSERIAL.Visible = HASSERIAL;
                         mrp = Convert.ToDouble(dgBatch.Rows[0].Cells["MRP"].Value);
                         if (hasBatch)
                         {
                             if (dgBatch.Rows[0].Cells["BATCH CODE"].Value != null)
                                 BATCH.Text = dgBatch.Rows[0].Cells["BATCH CODE"].Value.ToString();
                             if (dgBatch.Rows[0].Cells["EXPIRY DATE"].Value != null && dgBatch.Rows[0].Cells["EXPIRY DATE"].Value.ToString() != "")
                                 EXPIRY_DATE.Value = Convert.ToDateTime(dgBatch.Rows[0].Cells["EXPIRY DATE"].Value);
                         }
                         tb_mrp.Text = mrp.ToString(decimalFormat);
                         kryptonLabel36.Visible = HASSERIAL;
                         SERIALNO.Visible = HASSERIAL;
                         kryptonLabel36.Visible = HASSERIAL;
                         SERIALNO.Visible = HASSERIAL;

                         //if (hasBatch)
                         //{
                         //    BATCH.Focus();
                         //}
                         //else
                         //{
                         QUANTITY.Text = "1";
                         TaxId = Convert.ToInt16(dgBatch.Rows[0].Cells["TaxId"].Value.ToString());
                         GetTaxRate();
                         if (SalebyItemCode)
                             ITEM_CODE.Focus();
                         else if (MoveToUnit)
                             UOM.Focus();
                         else if (MoveToQty)
                             QUANTITY.Focus();
                         else if (HASSERIAL)
                             SERIALNO.Focus();
                         else if (txtfree.Visible)
                             txtfree.Focus();
                         else if (MoveToPrice)
                             PRICE.Focus();
                         else if (tb_mrp.Visible)
                             tb_mrp.Focus();
                         else if (GROSS_TOTAL.Visible)
                             GROSS_TOTAL.Focus();
                         else if (MoveToDisc)
                             ITEM_DISCOUNT.Focus();
                         else if (tb_netvalue.Visible)
                             tb_netvalue.Focus();
                         else if (tb_descr.Visible)
                             tb_descr.Focus();
                         else if (hasTax)
                         {
                             if (MoveToTaxper)
                                 ITEM_TAX_PER.Focus();
                         }
                         else if (hasBatch)
                             BATCH.Focus();
                         else
                         {
                             addItem();
                             clearItem();
                             if (SalebyItemName)
                                 ITEM_NAME.Focus();
                             else if (SalebyItemCode)
                                 ITEM_CODE.Focus();
                             else if (SalebyBarcode)
                                 BARCODE.Focus();
                             else
                                 ITEM_NAME.Focus();
                         }
                         //    tb_descr.Focus();

                         //}

                        // this.UOM.SelectedIndexChanged -= new EventHandler(UOM_SelectedIndexChanged);
                         addUnits();
                       //  this.UOM.SelectedIndexChanged -= new EventHandler(UOM_SelectedIndexChanged);
                      //   UOM.Text = dgBatch.Rows[0].Cells["UNIT_CODE"].Value.ToString();
                       
                         GetDiscount();
                         ItemArabicName = dgBatch.Rows[0].Cells["DESC_ARB"].Value.ToString();
                         ITEM_NAME.Text = dgBatch.Rows[0].Cells["ITEM NAME"].Value.ToString();
                         PNL_DATAGRIDITEM.Visible = false;
                         itemSelected = false;
                         pricefob = Convert.ToDouble(PRICE.Text);
                         if (RATE_CODE.Text.StartsWith("MRP"))
                         {
                             double taxcalc = 0;
                             taxcalc = (Convert.ToDouble(ITEM_TAX_PER.Text) / 100) + 1;
                             PRICE.Text = (Convert.ToDouble(PRICE.Text) / taxcalc).ToString(decimalFormat);
                         }
                         else if (!hasSaleExclusive)
                         {
                             double taxcalc = 0;
                             taxcalc = (Convert.ToDouble(ITEM_TAX_PER.Text) / 100) + 1;
                             PRICE.Text = (Convert.ToDouble(PRICE.Text) / taxcalc).ToString();
                         }
                         PRICE.TextChanged += new EventHandler(calculateGrossAmount);
                         sales_price = Convert.ToDouble(PRICE.Text);
                         PRICE.Text = sales_price.ToString();
                     }
                 }
                     else
                     {
                         MessageBox.Show("Selected item is out of Stock");
                         ITEM_NAME.Focus();
                         return;
                     }
                 }
             }
        }

        public PrintDocument printDocumentMediumSizearabic { get; set; }

        public void UpdateCreditNote()
        {
            conn.Open();
            cmd.CommandText = "UPDATE tbl_CreditNote SET  Status='Inactive'  WHERE CN_Doc_No = '" + txtCreditNoteNo.Text + "' ";
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        private void txtCreditNoteNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (txtCreditNoteNo.Text != "")
                {
                    DataTable dt = new DataTable();
                    cmd.CommandText = "SELECT * FROM tbl_CreditNote WHERE CN_Doc_No = '" + txtCreditNoteNo.Text + "'";
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter Adap = new SqlDataAdapter();
                    Adap.SelectCommand = cmd;
                    Adap.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["Status"].ToString() == "Inactive")
                        {
                            MessageBox.Show("Credit Note Already Used ");
                        }
                        else
                        {
                            txtcashrcvd.Text = dt.Rows[0]["Nett_Amount"].ToString();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Credit Note Not Found ");

                    }
                }
            }

        }
        
        int cntr = 0;   
        private void PRICE_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cntr == 0)
            {
                includechang = true;
            }
            double taxcalc = 0;
            try
            {
                taxcalc = (Convert.ToDouble(ITEM_TAX_PER.Text) / 100) + 1;
            }
            catch { }

            if (e.KeyChar == '-')
            {
                //excludechanged = false;
                if (!excludechanged)
                {
                    double temp = 0;
                    temp = pricefob;
                    PRICE.Text = (temp / taxcalc).ToString(decimalFormat);
                    //ITEM_TAX.Text = (Convert.ToDouble(tb_netvalue.Text) * (Convert.ToDouble(ITEM_TAX_PER.Text) / 100)).ToString(decimalFormat);
                    excludechanged = true;
                    includechang = false;
                    lb_rate.Text = "Gr Rate:";
                    cntr++;
                }
               else if (!includechang)
                {
                    double localPrice = 0;
                    try
                    {
                        localPrice = Convert.ToDouble(PRICE.Text);
                    }
                    catch (Exception){}

                    PRICE.Text = (localPrice * taxcalc).ToString(decimalFormat);
                    includechang = true;
                    excludechanged = false;
                    lb_rate.Text = "Rate:";

                }
            }
            //if (e.KeyChar == '+')
            //{
            //    if (!includechang)
            //    {
            //        PRICE.Text = (Convert.ToDouble(PRICE.Text) * taxcalc).ToString(decimalFormat);
            //        includechang = true;
            //        excludechanged = false; 
            //        lb_rate.Text = "Rate:";
                    
            //    }
            //}
        }
        
        private void txtFreight_TextChanged(object sender, EventArgs e)
        {
            totalCalculation();
        }

        private void DISCOUNT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSave.Focus();
            }
        }

        private void DISCOUNT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '-')
            {
                if (DiscType == "Percentage")
                {
                    kryptonLabel25.Text = "Disc Amt";
                    DiscType = "Amount";
                    DISCOUNT_Leave(sender, e);
                    DISCOUNT.Text = amt1.ToString(decimalFormat);
                }
                else if (DiscType == "Amount")
                {
                    kryptonLabel25.Text = "Disc %";
                    DiscType = "Percentage";
                    DISCOUNT_Leave(sender, e);
                    DISCOUNT.Text = percent1.ToString(decimalFormat);
                }
                else
                {
                    DiscType = "Amount";
                    kryptonLabel25.Text = "Disc Amt";
                    //Calculate_total_DiscAmt();
                    DISCOUNT_Leave(sender, e);
                    DISCOUNT.Text = amt1.ToString(decimalFormat);

                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panel4.Visible = true;
            panel4.Location = new Point((this.Width / 2) - panel4.Width/2, (this.Height/2) - panel4.Height/2);
            panel4.BringToFront();

        }

        private Boolean itemAddValid()
        {
            if (!DESC_ENG.Text.Equals("") && !Convert.ToString(DrpTaxType.SelectedValue).Equals("") && 
                !Convert.ToString(drp_unit.SelectedValue).Equals("") && !text_qty.Text.Equals("") &&
                !txt_pr.Text.Equals("") && !txt_rr.Text.Equals("") && !txt_wr.Text.Equals("") && 
                !txt_mr.Text.Equals(""))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        StockEntry stock_entry = new StockEntry();
        private void button2_Click(object sender, EventArgs e)
        {
            if (itemAddValid())
            {
                ba.code = CODE.Text;
                ba.name = DESC_ENG.Text;
                ba.tax = Convert.ToInt32(DrpTaxType.SelectedValue);
                ba.sr = Convert.ToDecimal(txt_rr.Text);
                ba.rr = Convert.ToDecimal(txt_rr.Text);
                ba.pr = Convert.ToDecimal(txt_pr.Text);
                ba.wr = Convert.ToDecimal(txt_wr.Text);
                ba.mrp = Convert.ToDecimal(txt_mr.Text);
                ba.unit = drp_unit.SelectedValue.ToString();
                ba.brcode = txt_barcode.Text;


                da.additem(ba);

                da.addunit(ba);
                int next_batch_inc = stock_entry.max_batch_id(CODE.Text);
                string next_batch = CODE.Text + "B" + next_batch_inc;
                ba.price_batch = next_batch;
                da.addprice(ba);
                da.addprice_DF(ba);
                cmd.CommandText = "INSERT INTO tblStock(Item_id, qty, Cost_price, supplier_id, MRP,batch_id,batch_increment) values(@item_id, @qty, @cost_price, @supplier_id, @mrp,@batch_id,@batch_increment)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@item_id", CODE.Text);
                cmd.Parameters.AddWithValue("@qty",0);
                cmd.Parameters.AddWithValue("@cost_price", ba.pr);
                cmd.Parameters.AddWithValue("@mrp", ba.mrp);
                string sup_id="";
                
                cmd.Parameters.AddWithValue("@supplier_id", sup_id);
                cmd.Parameters.AddWithValue("@batch_id", next_batch);
                cmd.Parameters.AddWithValue("@batch_increment", next_batch_inc);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                panel4.Visible = false;
                bindgridview();
                ITEM_CODE.Text = CODE.Text;
                ITEM_NAME.Text = DESC_ENG.Text;
                UOM.SelectedItem = drp_unit.SelectedValue.ToString();
            }
            else
            {
                MessageBox.Show("Incorrect Details!");
            }
        }

        private void linkLabel1_MouseHover(object sender, EventArgs e)
        {
            panel4.Visible = false;
        }

        private void CODE_TextChanged(object sender, EventArgs e)
        {
            txt_barcode.Text = CODE.Text;
        }

        private void SERIALNO_Leave(object sender, EventArgs e)
        {
           // txtMultiSerials.Focus();
        }

        private void VOUCHNUM_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !VOUCHNUM.Text.Equals(""))
            {
                double grossTotal1 = 0, discTotal1 = 0, netTotal1 = 0, taxTotal1 = 0, itemTotal1 = 0;
                double taxTot, vatTot, discTot, amtTot, netTotAmt;
                double round, freight, cess;
                Common.preventDingSound(e);
                Clear2();
                VOUCHNUM.Text = (Convert.ToInt32(VOUCHNUM.Text)).ToString();
                checkvoucher(Convert.ToInt32(VOUCHNUM.Text));
                DataTable dt = new DataTable();
                if (cmbInvType.SelectedIndex == 0)
                {
                    cmd.CommandText = "SELECT * FROM INV_SALES_HDR WHERE DOC_ID = '" + VOUCHNUM.Text + "'AND FLAGDEL='TRUE' AND (SALE_TYPE IS NULL OR SALE_TYPE='')";
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter Adap = new SqlDataAdapter();
                    Adap.SelectCommand = cmd;
                    Adap.Fill(dt);
                }
                else
                {
                    cmd.CommandText = "SELECT * FROM INV_SALES_HDR WHERE DOC_ID = '" + VOUCHNUM.Text + "'AND FLAGDEL='TRUE' AND SALE_TYPE= '" + cmbInvType.SelectedValue + "'";
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter Adap = new SqlDataAdapter();
                    Adap.SelectCommand = cmd;
                    Adap.Fill(dt);
                }


                if (dt.Rows.Count > 0)
                {
                    ID = dt.Rows[0]["DOC_NO"].ToString();
                    DOC_NO.Text = ID;
                    DOC_DATE_GRE.Text = Convert.ToDateTime(Convert.ToString(dt.Rows[0]["DOC_DATE_GRE"].ToString())).ToShortDateString();
                    DOC_DATE_HIJ.Text = Convert.ToString(dt.Rows[0]["DOC_DATE_HIJ"]);
                    CURRENCY_CODE.Text = Convert.ToString(dt.Rows[0]["CURRENCY_CODE"]);
                    DOC_REFERENCE.Text = Convert.ToString(dt.Rows[0]["DOC_REFERENCE"]);
                    CUSTOMER_CODE.Text = Convert.ToString(dt.Rows[0]["CUSTOMER_CODE"]);
                    GetLedgerId(Convert.ToString(dt.Rows[0]["CUSTOMER_CODE"]));
                    CUSTOMER_NAME.Text = Convert.ToString(dt.Rows[0]["CUSTOMER_NAME_ENG"]);
                    SALESMAN_CODE.Text = Convert.ToString(dt.Rows[0]["SALESMAN_CODE"]);
                    NOTES.Text = Convert.ToString(dt.Rows[0]["NOTES"]);
                    taxTot = Convert.ToDouble(dt.Rows[0]["TAX_TOTAL"]);
                    vatTot = Convert.ToDouble(dt.Rows[0]["VAT"]);
                    discTot = Convert.ToDouble(dt.Rows[0]["DISCOUNT"]);
                    amtTot = Convert.ToDouble(dt.Rows[0]["TOTAL_AMOUNT"]);
                    netTotAmt = Convert.ToDouble(dt.Rows[0]["NET_AMOUNT"]);
                    round = Convert.ToDouble(dt.Rows[0]["ROUNDOFF"]);
                    freight = Convert.ToDouble(dt.Rows[0]["FREIGHT"]);
                    cess = Convert.ToDouble(dt.Rows[0]["CESS"]);


                    TAX_TOTAL.Text = taxTot.ToString(decimalFormat);
                    VATT.Text = vatTot.ToString(decimalFormat);
                    DISCOUNT.Text = discTot.ToString(decimalFormat);
                    TOTAL_AMOUNT.Text = amtTot.ToString(decimalFormat);
                    NET_AMOUNT.Text = netTotAmt.ToString(decimalFormat);
                    txtRoundOff.Text = round.ToString(decimalFormat);
                    txtFreight.Text = freight.ToString(decimalFormat);
                    TXT_CESS.Text = cess.ToString(decimalFormat);

                    PAY_CODE.Text = Convert.ToString(dt.Rows[0]["PAY_CODE"]);
                    PAY_NAME.Text = General.getName(PAY_CODE.Text, "GEN_PAYTYPE");
                    CARD_NO.Text = Convert.ToString(dt.Rows[0]["CARD_NO"]);
                    cmbInvType.SelectedIndexChanged -= cmbInvType_SelectedIndexChanged;
                    cmbInvType.Text = Convert.ToString(dt.Rows[0]["SALE_TYPE"]);
                    cmbInvType.SelectedIndexChanged += cmbInvType_SelectedIndexChanged;

                    conn.Open();
                    cmd.CommandText = "SELECT * FROM INV_SALES_DTL WHERE DOC_NO = '" + DOC_NO.Text + "'AND FLAGDEL='TRUE'";
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader r = cmd.ExecuteReader();
                    while (r.Read())
                    {
                        double price, taxAmt, taxpercent;
                        double grTotal, itemDisc, itemTot;
                        double purPrice, discValue, netTot, mrp;
                        //string discType;


                        int i = dgItems.Rows.Add(new DataGridViewRow());
                        DataGridViewCellCollection c = dgItems.Rows[i].Cells;
                        c["cSl"].Value = i + 1;
                        c["cCode"].Value = r["ITEM_CODE"];
                        c["cName"].Value = r["ITEM_DESC_ENG"];
                        if (hasBatch)
                        {
                            c["cBatch"].Value = r["BATCH"];
                            c["cExpDate"].Value = Convert.ToDateTime(r["EXPIRY_DATE"]).ToString("dd/MM/yyyy");
                        }
                        c["cUnit"].Value = r["UOM"];
                        c["cQty"].Value = r["QUANTITY"];
                        c["DiscTypes"].Value = r["DISC_TYPE"];
                        //c["DiscValues"].Value = r["DISC_VALUE"];
                        price = Convert.ToDouble(r["PRICE"]);
                        grTotal = Convert.ToDouble(r["GROSS_TOTAL"]);
                        itemDisc = Convert.ToDouble(r["ITEM_DISCOUNT"]);
                        itemTot = Convert.ToDouble(r["ITEM_TOTAL"]);
                        discValue = Convert.ToDouble(r["DISC_VALUE"]);
                        mrp = Convert.ToDouble(r["MRP"]);
                        purPrice = Convert.ToDouble((r["Pur_Price"] == DBNull.Value) ? "0" : r["Pur_Price"]);
                        netTot = grTotal - itemDisc;


                        c["cPrice"].Value = price.ToString(decimalFormat);
                        c["cGTotal"].Value = grTotal.ToString(decimalFormat);
                        c["cDisc"].Value = itemDisc.ToString(decimalFormat);
                        c["cTotal"].Value = itemTot.ToString(decimalFormat);
                        c["Pur"].Value = purPrice.ToString(decimalFormat);
                        c["cNetValue"].Value = netTot.ToString(decimalFormat);
                        c["DiscValues"].Value = discValue.ToString(decimalFormat);
                        c["cMrp"].Value = mrp.ToString(decimalFormat);
                        c["Cfree"].Value = r["FREE"];
                        if (hasTax)
                        {
                            taxpercent = Convert.ToDouble(r["ITEM_TAX_PER"]);
                            c["cTaxPer"].Value = taxpercent.ToString(decimalFormat);
                            taxAmt = Convert.ToDouble(r["ITEM_TAX"]);
                            c["cTaxAmt"].Value = taxAmt.ToString(decimalFormat);
                        }

                        grossTotal1 += Convert.ToDouble(c["cGTotal"].Value);
                        discTotal1 += Convert.ToDouble(c["cDisc"].Value);
                        netTotal1 += Convert.ToDouble(c["cNetValue"].Value);
                        taxTotal1 += Convert.ToDouble(c["cTaxAmt"].Value);
                        itemTotal1 += Convert.ToDouble(c["cTotal"].Value);

                        txtGrossTotal.Text = grossTotal1.ToString(decimalFormat);
                        txtLineDiscTotal.Text = discTotal1.ToString(decimalFormat);
                        txtNetTotal.Text = netTotal1.ToString(decimalFormat);
                        txtTaxTotal.Text = taxTotal1.ToString(decimalFormat);
                        txtItemTotal.Text = itemTotal1.ToString(decimalFormat);


                        c["cDescArb"].Value = r["ITEM_DESC_ARB"];
                        c["SerialNos"].Value = r["SERIALNO"];
                    }
                    conn.Close();

                    if (CUSTOMER_CODE.Text != "")
                    {
                        chkCustomeleveldiscount.Enabled = true;

                        chkCustomeleveldiscount.Checked = Convert.ToBoolean(dt.Rows[0]["CUSLEVELDISCOUNT"]);
                    }
                }
                else
                {
                    MessageBox.Show("Sales voucher not found for given number.");
                    return;
                }

                
            }
            if (e.KeyCode == Keys.Back)
            {
                VOUCHNUM.ReadOnly = false;
            }


        }

        private void bt_close_Click(object sender, EventArgs e)
        {
            panel4.Visible = false;
        }

        private void SERIALNO_Enter(object sender, EventArgs e)
        {
            txtMultiSerials.Focus();
            double qty = 0;
            if (!QUANTITY.Text.Equals(""))
            {
                qty = Convert.ToDouble(QUANTITY.Text);
            }

            if (qty > 1)
            {
                //MessageBox.Show(SERIALNO.Height.ToString());
                SERIALNO.Multiline = true;
                txtMultiSerials.Focus();
                SERIALNO.Height = 20;
                panMultiSerials.Visible = true;
                panMultiSerials.Location = new Point(panManageItem.Location.X + PNLSERIAL.Location.X + SERIALNO.Location.X, panManageItem.Location.Y + PNLSERIAL.Location.Y + SERIALNO.Location.Y + SERIALNO.Height);
            }
        }
        
        int numberOflines = 0;
        double qty1 = 0;
        int lineIndex = 0;
        int cursorPosition;
        private void txtMultiSerials_TextChanged(object sender, EventArgs e)
        {

            //int numberOflines = 0 ;
            int line_length;
            try
            {
                qty1 = Convert.ToDouble(QUANTITY.Text);
                string text = txtMultiSerials.Text;
                string[] lines = text.Split('\n');
                numberOflines = lines.Count();
                //line_length = lines.Length;
                //string lastvalue = lines[lines.Length-1];
               cursorPosition = txtMultiSerials.SelectionStart;
                lineIndex = txtMultiSerials.GetLineFromCharIndex(cursorPosition);
                if (txtMultiSerials.Lines.Length > 0)
                {
                    for (int i = 0; i <= qty1; i++)
                    {
                        for (int j = i + 1; j < qty1; j++)
                        {
                            if (txtMultiSerials.Lines[i] == txtMultiSerials.Lines[j])
                            {
                                MessageBox.Show("Serial Number Already Exist");
                            }
                        }
                    }
                }
              
                if (numberOflines >= qty1+1)
                {
                    // check_serial_number();
                    //int cursorPosition = txtMultiSerials.SelectionStart;
                   // lineIndex = txtMultiSerials.GetLineFromCharIndex(cursorPosition);
                    //if (hasFree)
                    //{
                    //    txtfree.Focus();
                    //}
                    //else
                    //{
                    //    PRICE.Focus();
                    //}
                    //panMultiSerials.Visible = false;
                }
            }
            catch
            {

            }


        }

        void saleType()
        {
            cmbInvType.SelectedIndexChanged -= cmbInvType_SelectedIndexChanged;
            cmbInvType.DataSource = Common.getSaleTypes();           
            cmbInvType.DisplayMember = "value";
            cmbInvType.ValueMember = "key";
            if (DefaultSaleType.Equals(""))
            {
                cmbInvType.SelectedIndex = 0;
            }
            else
            {
                cmbInvType.SelectedValue = DefaultSaleType;
            }
            cmbInvType.SelectedIndexChanged += cmbInvType_SelectedIndexChanged;
        }
        
        void saleTypeLed()
        {
            cmbInvType.SelectedIndexChanged -= cmbInvType_SelectedIndexChanged;
            cmbInvType.DataSource = Common.getSaleTypes();
            cmbInvType.DisplayMember = "value";
            cmbInvType.ValueMember = "key";            
            cmbInvType.SelectedIndexChanged += cmbInvType_SelectedIndexChanged;
        }

        private void cmbInvType_SelectedIndexChanged(object sender, EventArgs e)
        {
          //  PrintPage.Text = cmbInvType.Text;
            GetMaxDocID();

            double grossTotal1 = 0, discTotal1 = 0, netTotal1 = 0, taxTotal1 = 0, itemTotal1 = 0;
            double taxTot, vatTot, discTot, amtTot, netTotAmt;
            double round, freight, cess;
            Clear2();
            if (!VOUCHNUM.Text.Equals(""))
            {
                checkvoucher(Convert.ToInt32(VOUCHNUM.Text));
                DataTable dt = new DataTable();
                cmd.CommandText = "SELECT * FROM INV_SALES_HDR WHERE DOC_ID = '" + VOUCHNUM.Text + "' AND FLAGDEL='TRUE' AND SALE_TYPE='" + cmbInvType.SelectedValue + "'";
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter Adap = new SqlDataAdapter();
                Adap.SelectCommand = cmd;
                Adap.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    ID = Convert.ToString(dt.Rows[0]["DOC_NO"]);
                    DOC_NO.Text = ID;
                    DOC_DATE_GRE.Text = Convert.ToDateTime(Convert.ToString(dt.Rows[0]["DOC_DATE_GRE"].ToString())).ToShortDateString();
                    DOC_DATE_HIJ.Text = Convert.ToString(dt.Rows[0]["DOC_DATE_HIJ"]);
                    CURRENCY_CODE.Text = Convert.ToString(dt.Rows[0]["CURRENCY_CODE"]);
                    DOC_REFERENCE.Text = Convert.ToString(dt.Rows[0]["DOC_REFERENCE"]);
                    CUSTOMER_CODE.Text = Convert.ToString(dt.Rows[0]["CUSTOMER_CODE"]);
                    GetLedgerId(Convert.ToString(dt.Rows[0]["CUSTOMER_CODE"]));
                    CUSTOMER_NAME.Text = Convert.ToString(dt.Rows[0]["CUSTOMER_NAME_ENG"]);
                    SALESMAN_CODE.Text = Convert.ToString(dt.Rows[0]["SALESMAN_CODE"]);
                    NOTES.Text = Convert.ToString(dt.Rows[0]["NOTES"]);
                    taxTot = Convert.ToDouble(dt.Rows[0]["TAX_TOTAL"]);
                    vatTot = Convert.ToDouble(dt.Rows[0]["VAT"]);
                    discTot = Convert.ToDouble(dt.Rows[0]["DISCOUNT"]);
                    amtTot = Convert.ToDouble(dt.Rows[0]["TOTAL_AMOUNT"]);
                    netTotAmt = Convert.ToDouble(dt.Rows[0]["NET_AMOUNT"]);
                    round = Convert.ToDouble(dt.Rows[0]["ROUNDOFF"]);
                    freight = Convert.ToDouble(dt.Rows[0]["FREIGHT"]);
                    cess = Convert.ToDouble(dt.Rows[0]["CESS"]);
                    TAX_TOTAL.Text = taxTot.ToString(decimalFormat);
                    VATT.Text = vatTot.ToString(decimalFormat);
                    DISCOUNT.Text = discTot.ToString(decimalFormat);
                    TOTAL_AMOUNT.Text = amtTot.ToString(decimalFormat);
                    NET_AMOUNT.Text = netTotAmt.ToString(decimalFormat);
                    txtRoundOff.Text = round.ToString(decimalFormat);
                    txtFreight.Text = freight.ToString(decimalFormat);
                    TXT_CESS.Text = cess.ToString(decimalFormat);
                    PAY_CODE.Text = Convert.ToString(dt.Rows[0]["PAY_CODE"]);
                    PAY_NAME.Text = General.getName(PAY_CODE.Text, "GEN_PAYTYPE");
                    CARD_NO.Text = Convert.ToString(dt.Rows[0]["CARD_NO"]);
                    cmbInvType.Text = Convert.ToString(dt.Rows[0]["SALE_TYPE"]);
                }

                cmd.CommandText = "SELECT * FROM INV_SALES_DTL WHERE DOC_NO = '" + DOC_NO.Text + "'AND FLAGDEL='TRUE'";
                cmd.CommandType = CommandType.Text;
                conn.Open();
                SqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    double price, taxAmt, taxpercent;
                    double grTotal, itemDisc, itemTot;
                    double purPrice, discValue, netTot;
                    //double taxAmt;
                    //DataTable table = DecSet.getDecimalPoint();

                    int i = dgItems.Rows.Add(new DataGridViewRow());
                    DataGridViewCellCollection c = dgItems.Rows[i].Cells;
                    c["cCode"].Value = r["ITEM_CODE"];
                    c["cName"].Value = r["ITEM_DESC_ENG"];
                    if (hasBatch)
                    {
                        c["cBatch"].Value = r["BATCH"];
                        c["cExpDate"].Value = Convert.ToDateTime(r["EXPIRY_DATE"]).ToString("dd/MM/yyyy");
                    }
                    c["cUnit"].Value = r["UOM"];
                    c["cQty"].Value = r["QUANTITY"];
                    c["DiscTypes"].Value = r["DISC_TYPE"];
                    price = Convert.ToDouble(r["PRICE"]);
                    grTotal = Convert.ToDouble(r["GROSS_TOTAL"]);
                    itemDisc = Convert.ToDouble(r["ITEM_DISCOUNT"]);
                    itemTot = Convert.ToDouble(r["ITEM_TOTAL"]);
                    discValue = Convert.ToDouble(r["DISC_VALUE"]);
                    purPrice = Convert.ToDouble(r["Pur_Price"]);
                    netTot = grTotal - itemDisc;

                    c["cPrice"].Value = price.ToString(decimalFormat);
                    c["cGTotal"].Value = grTotal.ToString(decimalFormat);
                    c["cDisc"].Value = itemDisc.ToString(decimalFormat);
                    c["cTotal"].Value = itemTot.ToString(decimalFormat);
                    c["Pur"].Value = purPrice.ToString(decimalFormat);
                    c["cNetValue"].Value = netTot.ToString(decimalFormat);
                    c["DiscValues"].Value = discValue.ToString(decimalFormat);
                    if (hasTax)
                    {
                        taxpercent = Convert.ToDouble(r["ITEM_TAX_PER"]);
                        c["cTaxPer"].Value = taxpercent.ToString(decimalFormat);
                        taxAmt = Convert.ToDouble(r["ITEM_TAX"]);
                        c["cTaxAmt"].Value = taxAmt.ToString(decimalFormat);
                    }

                    grossTotal1 += Convert.ToDouble(c["cGTotal"].Value);
                    discTotal1 += Convert.ToDouble(c["cDisc"].Value);
                    netTotal1 += Convert.ToDouble(c["cNetValue"].Value);
                    taxTotal1 += Convert.ToDouble(c["cTaxAmt"].Value);
                    itemTotal1 += Convert.ToDouble(c["cTotal"].Value);

                    txtGrossTotal.Text = grossTotal1.ToString(decimalFormat);
                    txtLineDiscTotal.Text = discTotal1.ToString(decimalFormat);
                    txtNetTotal.Text = netTotal1.ToString(decimalFormat);
                    txtTaxTotal.Text = taxTotal1.ToString(decimalFormat);
                    txtItemTotal.Text = itemTotal1.ToString(decimalFormat);

                    c["cDescArb"].Value = r["ITEM_DESC_ARB"];
                    c["SerialNos"].Value = r["SERIALNO"];
                }
                conn.Close();
                if (CUSTOMER_CODE.Text != "")
                {
                    chkCustomeleveldiscount.Enabled = true;

                    chkCustomeleveldiscount.Checked = Convert.ToBoolean(dt.Rows[0]["CUSLEVELDISCOUNT"]);
                }
                customer(CUSTOMER_CODE.Text);
            }
        }

        public void createControls()
        {
            if (dgItems.RowCount > 0)
            {
                Rectangle firstRect = dgItems.GetCellDisplayRectangle(dgItems.ColumnCount - 1, 0, true);
                lblGrossTotal.Font = new Font("Arial", 9, FontStyle.Bold);
                lblGrossTotal.BorderStyle = BorderStyle.FixedSingle;
                lblGrossTotal.Anchor = AnchorStyles.None;
                lblGrossTotal.Location = new Point(dgItems.Location.X, dgItems.Location.Y + dgItems.Height - SystemInformation.HorizontalScrollBarHeight);
                //this.Controls.Add(lblGrossTotal);
                tabControl1.TabPages[0].Controls.Add(lblGrossTotal);
                lblGrossTotal.Text = "Gross Total";
                lblGrossTotal.BringToFront();
                lblGrossTotal.TextAlign = ContentAlignment.MiddleCenter;
                lblGrossTotal.BackColor = Color.LightCyan;


                txtGrossTotal.Font = new Font("Arial", 10, FontStyle.Bold);
                txtGrossTotal.BorderStyle = BorderStyle.FixedSingle;
                txtGrossTotal.Anchor = AnchorStyles.None;
                txtGrossTotal.Width = 100;
                txtGrossTotal.Location = new Point(dgItems.Location.X + lblGrossTotal.Width, dgItems.Location.Y + dgItems.Height - SystemInformation.HorizontalScrollBarHeight);
                tabControl1.TabPages[0].Controls.Add(txtGrossTotal);

                txtGrossTotal.Text = decimalFormat;
                txtGrossTotal.Enabled = false;
                txtGrossTotal.BringToFront();
                txtGrossTotal.TextAlign = HorizontalAlignment.Center;
                txtGrossTotal.BackColor = Color.LightCyan;


                lblLineDiscTotal.Font = new Font("Arial", 9, FontStyle.Bold);
                lblLineDiscTotal.BorderStyle = BorderStyle.FixedSingle;
                lblLineDiscTotal.Anchor = AnchorStyles.None;
                lblLineDiscTotal.Location = new Point(dgItems.Location.X + lblGrossTotal.Width + txtGrossTotal.Width, dgItems.Location.Y + dgItems.Height - SystemInformation.HorizontalScrollBarHeight);
                tabControl1.TabPages[0].Controls.Add(lblLineDiscTotal);
                lblLineDiscTotal.Text = "LDisc.Total";
                lblLineDiscTotal.BringToFront();
                lblLineDiscTotal.TextAlign = ContentAlignment.MiddleCenter;


                txtLineDiscTotal.Font = new Font("Arial", 10, FontStyle.Bold);
                txtLineDiscTotal.BorderStyle = BorderStyle.FixedSingle;
                txtLineDiscTotal.Anchor = AnchorStyles.None;
                txtLineDiscTotal.Width = 100;
                txtLineDiscTotal.Location = new Point(dgItems.Location.X + lblGrossTotal.Width + txtGrossTotal.Width + lblLineDiscTotal.Width, dgItems.Location.Y + dgItems.Height - SystemInformation.HorizontalScrollBarHeight);
                tabControl1.TabPages[0].Controls.Add(txtLineDiscTotal);
                txtLineDiscTotal.Text = decimalFormat;
                txtLineDiscTotal.Enabled = false;
                txtLineDiscTotal.BringToFront();
                txtLineDiscTotal.TextAlign = HorizontalAlignment.Center;


                lblNetTotal.Font = new Font("Arial", 9, FontStyle.Bold);
                lblNetTotal.BorderStyle = BorderStyle.FixedSingle;
                lblNetTotal.Anchor = AnchorStyles.None;
                lblNetTotal.Location = new Point(dgItems.Location.X + lblGrossTotal.Width + txtGrossTotal.Width + lblLineDiscTotal.Width + txtLineDiscTotal.Width, dgItems.Location.Y + dgItems.Height - SystemInformation.HorizontalScrollBarHeight);
                tabControl1.TabPages[0].Controls.Add(lblNetTotal);
                lblNetTotal.Text = "Net Total";
                lblNetTotal.BringToFront();
                lblNetTotal.TextAlign = ContentAlignment.MiddleCenter;
                lblNetTotal.BackColor = Color.LightCyan;


                txtNetTotal.Font = new Font("Arial", 10, FontStyle.Bold);
                txtNetTotal.BorderStyle = BorderStyle.FixedSingle;
                txtNetTotal.Anchor = AnchorStyles.None;
                txtNetTotal.Width = 100;
                txtNetTotal.Location = new Point(dgItems.Location.X + lblGrossTotal.Width + txtGrossTotal.Width + lblLineDiscTotal.Width + txtLineDiscTotal.Width + lblNetTotal.Width, dgItems.Location.Y + dgItems.Height - SystemInformation.HorizontalScrollBarHeight);
                tabControl1.TabPages[0].Controls.Add(txtNetTotal);
                txtNetTotal.Text = decimalFormat;
                txtNetTotal.Enabled = false;
                txtNetTotal.BringToFront();
                txtNetTotal.TextAlign = HorizontalAlignment.Center;
                txtNetTotal.BackColor = Color.LightCyan;


                lblTaxTotal.Font = new Font("Arial", 9, FontStyle.Bold);
                lblTaxTotal.BorderStyle = BorderStyle.FixedSingle;
                lblTaxTotal.Anchor = AnchorStyles.None;
                lblTaxTotal.Location = new Point(dgItems.Location.X + lblGrossTotal.Width + txtGrossTotal.Width + lblLineDiscTotal.Width + txtLineDiscTotal.Width + lblNetTotal.Width + txtNetTotal.Width, dgItems.Location.Y + dgItems.Height - SystemInformation.HorizontalScrollBarHeight);
                tabControl1.TabPages[0].Controls.Add(lblTaxTotal);
                lblTaxTotal.Text = "Tax Total";
                lblTaxTotal.BringToFront();
                lblTaxTotal.TextAlign = ContentAlignment.MiddleCenter;


                txtTaxTotal.Font = new Font("Arial", 10, FontStyle.Bold);
                txtTaxTotal.BorderStyle = BorderStyle.FixedSingle;
                txtTaxTotal.Anchor = AnchorStyles.None;
                txtTaxTotal.Width = 100;
                txtTaxTotal.Location = new Point(dgItems.Location.X + lblGrossTotal.Width + txtGrossTotal.Width + lblLineDiscTotal.Width + txtLineDiscTotal.Width + lblNetTotal.Width + txtNetTotal.Width + lblTaxTotal.Width, dgItems.Location.Y + dgItems.Height - SystemInformation.HorizontalScrollBarHeight);
                tabControl1.TabPages[0].Controls.Add(txtTaxTotal);
                txtTaxTotal.Text = decimalFormat;
                txtTaxTotal.Enabled = false;
                txtTaxTotal.BringToFront();
                txtTaxTotal.TextAlign = HorizontalAlignment.Center;


                lblItemTotal.Font = new Font("Arial", 9, FontStyle.Bold);
                lblItemTotal.BorderStyle = BorderStyle.FixedSingle;
                lblItemTotal.Anchor = AnchorStyles.None;
                lblItemTotal.Location = new Point(dgItems.Location.X + lblGrossTotal.Width + txtGrossTotal.Width + lblLineDiscTotal.Width + txtLineDiscTotal.Width + lblNetTotal.Width + txtNetTotal.Width + lblTaxTotal.Width + txtTaxTotal.Width, dgItems.Location.Y + dgItems.Height - SystemInformation.HorizontalScrollBarHeight);
                tabControl1.TabPages[0].Controls.Add(lblItemTotal);
                lblItemTotal.Text = "Sub Total";
                lblItemTotal.BringToFront();
                lblItemTotal.TextAlign = ContentAlignment.MiddleCenter;
                lblItemTotal.BackColor = Color.LightCyan;

                txtItemTotal.Font = new Font("Arial", 10, FontStyle.Bold);
                txtItemTotal.BorderStyle = BorderStyle.FixedSingle;
                txtItemTotal.Anchor = AnchorStyles.None;
                txtItemTotal.Width = dgItems.Width - (lblItemTotal.Location.X + lblItemTotal.Width);
                txtItemTotal.Location = new Point(dgItems.Location.X + lblGrossTotal.Width + txtGrossTotal.Width + lblLineDiscTotal.Width + txtLineDiscTotal.Width + lblNetTotal.Width + txtNetTotal.Width + lblTaxTotal.Width + txtTaxTotal.Width + lblItemTotal.Width, dgItems.Location.Y + dgItems.Height - SystemInformation.HorizontalScrollBarHeight);
                tabControl1.TabPages[0].Controls.Add(txtItemTotal);
                txtItemTotal.Text = decimalFormat;
                txtItemTotal.Enabled = false;
                txtItemTotal.BringToFront();
                txtItemTotal.TextAlign = HorizontalAlignment.Center;
                txtItemTotal.BackColor = Color.LightCyan;
            }
        }

        private void DISCOUNT_Leave(object sender, EventArgs e)
        {
            if (kryptonLabel25.Text == "Disc Amt")
            {
                if (DISCOUNT.Text == string.Empty)
                {
                    DISCOUNT.Text = decimalFormat;
                }
                value1 = Convert.ToDouble(DISCOUNT.Text);
                amt1 = Convert.ToDouble(TOTAL_AMOUNT.Text) * (Convert.ToDouble(value1) / 100);
            }
            else
            {
                if (DISCOUNT.Text == string.Empty)
                {
                    DISCOUNT.Text = "0";
                }
                value1 = Convert.ToDouble(DISCOUNT.Text);
                percent1 = (Convert.ToDouble(value1) * 100) / Convert.ToDouble(TOTAL_AMOUNT.Text);
            }
        }

        private void dgItems_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            createControls();
            VOUCHNUM.ReadOnly = true;
            lb_rate.Text = "Rate:";
            excludechanged = false;
            cntr = 0;
            calculateGSTTaxes();
        }

        private void CUSTOMER_NAME_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Common.preventDingSound(e);
                SALESMAN_CODE.Focus();
            }
        }

        private void chkRoundOff_CheckedChanged(object sender, EventArgs e)
        {
            totalCalculation();
        }
                       
        private void VOUCHNUM_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void TOTAL_AMOUNT_TextChanged(object sender, EventArgs e)
        {
            chkRoundOff.CheckedChanged += new EventHandler(chkRoundOff_CheckedChanged);            
        }

        private void VOUCHNUM_Leave(object sender, EventArgs e)
        {
            if (VOUCHNUM.Text.Trim() == string.Empty)
            {
                GetMaxDocID();
            }
        }

        private void PrintPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            flagPrintEventAssigned = false;
            counter = 0;
        }

        private void CURRENCY_CODE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Common.preventDingSound(e);
                RATE_CODE.Focus();
            }
        }

        private void RATE_CODE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                Common.preventDingSound(e);
                if (Focus_Sale_Type)
                {
                    ActiveControl = cmbInvType;
                    //cmbInvType.Focus();
                }
                else if (hasBarcode)
                {

                    if (SalebyBarcode)
                    {
                        ActiveControl = BARCODE;
                    }
                    else
                    {
                        ActiveControl = ITEM_NAME;
                    }
                }
                else if (SalebyItemName)
                {
                    ActiveControl = ITEM_NAME;
                }
                else if (SalebyItemCode)
                {
                    ActiveControl = ITEM_CODE;
                }
                else
                {
                    ActiveControl = ITEM_NAME;
                }
            }
            else
            {
                btnSales.PerformClick();
            }
        }

        private void SALESMAN_NAME_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Common.preventDingSound(e);
                DOC_DATE_GRE.Focus();
            }
        }

        private void tb_pono_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Common.preventDingSound(e);
                CURRENCY_CODE.Focus();
            }
        }

        private void cmbInvType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                Common.preventDingSound(e);                
                if (hasBarcode)
                {

                    if (SalebyBarcode)
                    {
                        ActiveControl = BARCODE;
                    }
                    else
                    {
                        ActiveControl = ITEM_NAME;
                    }
                }
                else if (SalebyItemName)
                {
                    ActiveControl = ITEM_NAME;
                }
                else if (SalebyItemCode)
                {
                    ActiveControl = ITEM_CODE;
                }
                else
                {
                    ActiveControl = ITEM_NAME;
                }
            }
        }

        private void ITEM_TAX_PER_TextChanged(object sender, EventArgs e)
        {
          //  taxEnable();
            if (ITEM_DISCOUNT.Text.Trim() != "" && GROSS_TOTAL.Text.Trim() != "")
            {
                if (DiscType == "Percentage")

                {
                    ITEM_TOTAL.Text = (Convert.ToDouble(GROSS_TOTAL.Text) - (Convert.ToDouble(GROSS_TOTAL.Text) * (Convert.ToDouble(ITEM_DISCOUNT.Text) / 100))).ToString();
                    DiscAmt = Convert.ToDouble(GROSS_TOTAL.Text) * (Convert.ToDouble(ITEM_DISCOUNT.Text) / 100);
                    if (ITEM_TAX.Text != "" && ITEM_DISCOUNT.Text != "0" && ITEM_DISCOUNT.Text.Trim() != "" && QUANTITY.Text.Trim() != "" && PRICE.Text.Trim() != "")
                    {
                        double tax = 0;
                        double DISC = 0;
                        if (ITEM_DISCOUNT.Text.Trim() != "")
                        {
                            DISC = Convert.ToDouble(ITEM_DISCOUNT.Text);
                        }
                        if (ITEM_TAX_PER.Text.Trim() != "")
                        {
                            tax = Convert.ToDouble(ITEM_TAX_PER.Text);
                        }
                        double disc_amt = Convert.ToDouble(PRICE.Text) * Convert.ToDouble(QUANTITY.Text) * DISC / 100;
                        double tax_amt = (Convert.ToDouble(PRICE.Text) * Convert.ToDouble(QUANTITY.Text) - disc_amt) * tax / 100;
                        string disc_amount = disc_amt.ToString(decimalFormat);
                        string tax_amount = tax_amt.ToString(decimalFormat);
                        ITEM_TAX.Text = ((Convert.ToDouble(PRICE.Text) * Convert.ToDouble(QUANTITY.Text) - disc_amt) * tax / 100).ToString(decimalFormat);

                        GROSS_TOTAL.Text = (Convert.ToDouble(PRICE.Text) * Convert.ToDouble(QUANTITY.Text)).ToString(decimalFormat);
                        //ITEM_TOTAL.Text = (((Convert.ToDouble(PRICE.Text) * Convert.ToDouble(QUANTITY.Text) - disc_amt) + (Convert.ToDouble(PRICE.Text) * Convert.ToDouble(QUANTITY.Text) - disc_amt) * tax / 100).ToString(decimalFormat));
                        //ITEM_TOTAL.Text = ((Convert.ToDouble(PRICE.Text) * Convert.ToDouble(QUANTITY.Text) + Convert.ToDouble(tax_amount)) - Convert.ToDouble(disc_amount)).ToString(decimalFormat);

                        if (taxcal == false)
                        {
                            ITEM_TOTAL.Text = ((Convert.ToDouble(PRICE.Text) * Convert.ToDouble(QUANTITY.Text)) - Convert.ToDouble(disc_amount)).ToString(decimalFormat);
                        }
                        else
                        {
                            ITEM_TOTAL.Text = ((Convert.ToDouble(PRICE.Text) * Convert.ToDouble(QUANTITY.Text) + Convert.ToDouble(tax_amount)) - Convert.ToDouble(disc_amount)).ToString(decimalFormat);
                        }               
                        tb_netvalue.Text = ((Convert.ToDouble(PRICE.Text) * Convert.ToDouble(QUANTITY.Text) - Convert.ToDouble(disc_amount)).ToString(decimalFormat));
                    }
                }
                else
                {
                    //ITEM_TOTAL.Text = ((Convert.ToDouble(GROSS_TOTAL.Text) + Convert.ToDouble(GROSS_TOTAL.Text)) - (Convert.ToDouble(ITEM_DISCOUNT.Text))).ToString(decimalFormat);
                    DiscAmt = Convert.ToDouble(ITEM_DISCOUNT.Text);
                    if (ITEM_TAX.Text != "" && !ITEM_DISCOUNT.Text.StartsWith("0.0") && ITEM_DISCOUNT.Text.Trim() != "" && QUANTITY.Text.Trim() != "" && PRICE.Text.Trim() != "")
                    {
                        double tax = 0;
                        double DISC = 0;
                        if (ITEM_DISCOUNT.Text.Trim() != "")
                        {
                            DISC = Convert.ToDouble(ITEM_DISCOUNT.Text);
                        }
                        if (ITEM_TAX_PER.Text.Trim() != "")
                        {
                            tax = Convert.ToDouble(ITEM_TAX_PER.Text);
                        }
                        double disc_amt = Convert.ToDouble(ITEM_DISCOUNT.Text);
                        double tax_amt = (Convert.ToDouble(PRICE.Text) * Convert.ToDouble(QUANTITY.Text) - disc_amt) * tax / 100;
                        string disc_amount = disc_amt.ToString(decimalFormat);
                        string tax_amount = tax_amt.ToString(decimalFormat);
                        ITEM_TAX.Text = ((Convert.ToDouble(PRICE.Text) * Convert.ToDouble(QUANTITY.Text) - disc_amt) * tax / 100).ToString(decimalFormat);
                        GROSS_TOTAL.Text = (Convert.ToDouble(PRICE.Text) * Convert.ToDouble(QUANTITY.Text)).ToString(decimalFormat);
                       // ITEM_TOTAL.Text = ((Convert.ToDouble(PRICE.Text) * Convert.ToDouble(QUANTITY.Text) + Convert.ToDouble(tax_amount)) - Convert.ToDouble(disc_amount)).ToString(decimalFormat);
                        if (taxcal == false)
                        {
                            ITEM_TOTAL.Text = ((Convert.ToDouble(PRICE.Text) * Convert.ToDouble(QUANTITY.Text)) - Convert.ToDouble(disc_amount)).ToString(decimalFormat);
                        }
                        else
                        {
                            ITEM_TOTAL.Text = ((Convert.ToDouble(PRICE.Text) * Convert.ToDouble(QUANTITY.Text) + Convert.ToDouble(tax_amount)) - Convert.ToDouble(disc_amount)).ToString(decimalFormat);
                        }
                        tb_netvalue.Text = ((Convert.ToDouble(PRICE.Text) * Convert.ToDouble(QUANTITY.Text) - Convert.ToDouble(disc_amount)).ToString(decimalFormat));

                    }
                }


                if (ITEM_DISCOUNT.Text == "")
                {
                    ITEM_TOTAL.Text = GROSS_TOTAL.Text;
                }

            }
            else if (ITEM_DISCOUNT.Text.Trim() == "")
            {

                double disc_amt = 0;
                tb_netvalue.Text = tb_netvalue.Text = ((Convert.ToDouble(PRICE.Text) * Convert.ToDouble(QUANTITY.Text) - disc_amt)).ToString(decimalFormat);
            }
        }

        private void PAY_CODE_TextChanged(object sender, EventArgs e)
        {
            if (PAY_CODE.Text == "CSH")
            {
                lblChqNo.Visible = false;
                CARD_NO.Visible = false;
                lblChqDate.Visible = false;
                CHQ_DATE.Visible = false;
                lblAccDetails.Visible = false;
                txtAccDetails.Visible = false;
                bindledgers();
            }
            else if (PAY_CODE.Text == "CHQ")
            {
                lblChqNo.Visible = true; ;
                CARD_NO.Visible = true;
                lblChqDate.Visible = true;
                CHQ_DATE.Visible = true;
                lblAccDetails.Visible = false;
                txtAccDetails.Visible = false;
                bindledgers();
            }
            else if (PAY_CODE.Text == "CRD")
            {
                lblChqNo.Visible = false;
                CARD_NO.Visible = false;
                lblChqDate.Visible = false;
                CHQ_DATE.Visible = false;
                lblAccDetails.Visible = true;
                lblAccDetails.Text = "Card No.";
                txtAccDetails.Visible = true;
                BankAccount();
            }
            else
            {
                lblChqNo.Visible = false;
                CARD_NO.Visible = false;
                lblChqDate.Visible = false;
                CHQ_DATE.Visible = false;
                lblAccDetails.Visible = true;
                lblAccDetails.Text = "Account No.";
                txtAccDetails.Visible = true;
                BankAccount();
            }
        }
        
        private void BankAccount()
        {
            DataTable dt = new DataTable();
            cmd = new SqlCommand("SELECT DISTINCT LEDGERNAME FROM tb_Ledgers WHERE UNDER IN (10,21,22)", conn);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            CASHACC.DataSource = dt;
            CASHACC.DisplayMember = "LEDGERNAME";
        }

        private void txtfree_TextChanged(object sender, EventArgs e)
        {
            if (txtfree.Text.Trim() == "")
            {
                txtfree.Text = "0";
            }            
        }

        private void dgvGSTTaxes_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            growDgvGSTTaxes();
        }

        private void dgItems_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            calculateGSTTaxes();
        }

        private void cmbState_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToString(cmbState.SelectedValue).Equals(lg.state))
            {
                dgvGSTTaxes.Columns[0].Visible = true;
                dgvGSTTaxes.Columns[1].Visible = true;
                dgvGSTTaxes.Columns[2].Visible = true;
                dgvGSTTaxes.Columns[3].Visible = true;
                dgvGSTTaxes.Columns[4].Visible = false;
                dgvGSTTaxes.Columns[5].Visible = false;
            }
            else
            {
                dgvGSTTaxes.Columns[0].Visible = false;
                dgvGSTTaxes.Columns[1].Visible = false;
                dgvGSTTaxes.Columns[2].Visible = false;
                dgvGSTTaxes.Columns[3].Visible = false;
                dgvGSTTaxes.Columns[4].Visible = true;
                dgvGSTTaxes.Columns[5].Visible = true;
            }
        }

        private void dataGridItem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridItem_RowHeaderCellChanged(object sender, DataGridViewRowEventArgs e)
        {
            int id = dataGridItem.CurrentRow.Index;
        }

        private void dataGridItem_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void dgBatch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                dgBatch.Visible = false;
                ITEM_NAME.Focus();
                Common_columns();

            }
            else if (e.KeyCode == Keys.Enter)
            {

                if (!PNL_DATAGRIDITEM.Visible)
                {
                    PNL_DATAGRIDITEM.Visible = true;
                }
                else if (dgBatch.CurrentRow != null)
                {
                    ShowStock = false;
                    itemSelected = true;
                    string itemcode = dgBatch.CurrentRow.Cells["ITEM_CODE"].Value.ToString();
                    ITEM_CODE.Text = itemcode;

                    //NOT VALIDATED//
                    BARCODE.Text = dgBatch.CurrentRow.Cells["BATCH_ID"].Value.ToString();

                    //NOT VALIDATED//
                    if (dgBatch.CurrentRow.Cells["HSN"].Value != null)
                    {
                        txt_HSN.Text = dgBatch.CurrentRow.Cells["HSN"].Value.ToString();
                    }
                    PurchasePrice = Convert.ToDecimal(dgBatch.CurrentRow.Cells["PUR"].Value);
                    String rateType = Convert.ToString(RATE_CODE.SelectedValue);
                    string pricedecimal = dgBatch.CurrentRow.Cells[rateType].Value.ToString();
                    double pricedec = Convert.ToDouble(pricedecimal);
                    //PRICE.Text = pricedec.ToString("N3");
                    PRICE.TextChanged -= new EventHandler(calculateGrossAmount);
                    PRICE.Text = dgBatch.CurrentRow.Cells[rateType].Value.ToString();
                    sales_price =Convert.ToDouble( dgBatch.CurrentRow.Cells[rateType].Value);
                    //PRICE.Text = (dataGridItem.CurrentRow.Cells["SALES"].Value.ToString("N3");
                    HASSERIAL = Convert.ToBoolean(dgBatch.CurrentRow.Cells["HASSERIAL"].Value);
                    PNLSERIAL.Visible = HASSERIAL;
                    mrp = Convert.ToDouble(dgBatch.CurrentRow.Cells["MRP"].Value);
                    if (hasBatch)
                    {
                        if (dgBatch.CurrentRow.Cells["BATCH CODE"].Value != null)
                            BATCH.Text = dgBatch.CurrentRow.Cells["BATCH CODE"].Value.ToString();
                        if (dgBatch.CurrentRow.Cells["EXPIRY DATE"].Value != null && dgBatch.CurrentRow.Cells["EXPIRY DATE"].Value.ToString() != "")
                            EXPIRY_DATE.Value = Convert.ToDateTime(dgBatch.CurrentRow.Cells["EXPIRY DATE"].Value);
                    }
                    tb_mrp.Text = mrp.ToString(decimalFormat);
                    kryptonLabel36.Visible = HASSERIAL;
                    SERIALNO.Visible = HASSERIAL;
                    kryptonLabel36.Visible = HASSERIAL;
                    SERIALNO.Visible = HASSERIAL;
                    StockCheck();
                    //if (hasBatch)
                    //{
                    //    BATCH.Focus();
                    //}
                    //else
                    //{
                   
                    //    tb_descr.Focus();

                    //}

                  //S  this.UOM.SelectedIndexChanged -= new EventHandler(UOM_SelectedIndexChanged);
                    addUnits();
                 //   this.UOM.SelectedIndexChanged -= new EventHandler(UOM_SelectedIndexChanged);
                  //  UOM.Text = dgBatch.CurrentRow.Cells["UNIT_CODE"].Value.ToString();
                   

                    GetDiscount();
                    ItemArabicName = dgBatch.CurrentRow.Cells["DESC_ARB"].Value.ToString();
                    ITEM_NAME.Text = dgBatch.CurrentRow.Cells["ITEM NAME"].Value.ToString();
                    PNL_DATAGRIDITEM.Visible = false;
                    itemSelected = false;
                    pricefob = Convert.ToDouble(PRICE.Text);
                    if (RATE_CODE.Text.StartsWith("MRP"))
                    {
                        double taxcalc = 0;
                        taxcalc = (Convert.ToDouble(ITEM_TAX_PER.Text) / 100) + 1;
                        PRICE.Text = (Convert.ToDouble(PRICE.Text) / taxcalc).ToString(decimalFormat);
                    }
                    else if (!hasSaleExclusive)
                    {
                        double taxcalc = 0;
                        taxcalc = (Convert.ToDouble(ITEM_TAX_PER.Text) / 100) + 1;
                        PRICE.Text = (Convert.ToDouble(PRICE.Text) / taxcalc).ToString();
                    }
                    PRICE.TextChanged += new EventHandler(calculateGrossAmount);
                    sales_price = Convert.ToDouble(PRICE.Text);
                    PRICE.Text = sales_price.ToString();
                    QUANTITY.Text = "1";
                    TaxId = Convert.ToInt16(dgBatch.CurrentRow.Cells["TaxId"].Value.ToString());
                    GetTaxRate();
                    if (SalebyItemCode)
                        ITEM_CODE.Focus();
                    else if (MoveToUnit)
                        UOM.Focus();
                    else if (MoveToQty)
                        QUANTITY.Focus();
                    else if (HASSERIAL)
                        SERIALNO.Focus();
                    else if (txtfree.Visible)
                        txtfree.Focus();
                    else if (MoveToPrice)
                        PRICE.Focus();
                    else if (tb_mrp.Visible)
                        tb_mrp.Focus();
                    else if (GROSS_TOTAL.Visible)
                        GROSS_TOTAL.Focus();
                    else if (MoveToDisc)
                        ITEM_DISCOUNT.Focus();
                    else if (tb_netvalue.Visible)
                        tb_netvalue.Focus();
                    else if (tb_descr.Visible)
                        tb_descr.Focus();
                    else if (hasTax && MoveToTaxper)
                    {
                        if (MoveToTaxper)
                            ITEM_TAX_PER.Focus();
                    }
                    else if (hasBatch)
                        BATCH.Focus();
                    else
                    {
                        addItem();
                        clearItem();
                        if (SalebyItemName)
                            ITEM_NAME.Focus();
                        else if (SalebyItemCode)
                            ITEM_CODE.Focus();
                        else if (SalebyBarcode)
                            BARCODE.Focus();
                        else
                            ITEM_NAME.Focus();
                    }
                    ITEM_DISCOUNT_TextChanged(sender, e);
                }
            }
        }

        private void dgBatch_DoubleClick(object sender, EventArgs e)
        {

            if (!PNL_DATAGRIDITEM.Visible)
            {
                PNL_DATAGRIDITEM.Visible = true;

            }
            else if (dgBatch.CurrentRow != null)
            {
                ShowStock = false;
                itemSelected = true;
                string itemcode = dgBatch.CurrentRow.Cells["ITEM_CODE"].Value.ToString();
                //NOT VALIDATED//
                BARCODE.Text = dgBatch.CurrentRow.Cells["BATCH_ID"].Value.ToString();
                //NOT VALIDATED//
                if (dgBatch.CurrentRow.Cells["HSN"].Value != null)
                {
                    txt_HSN.Text = dgBatch.CurrentRow.Cells["HSN"].Value.ToString();
                }
                ITEM_CODE.Text = itemcode;
                PurchasePrice = Convert.ToDecimal(dgBatch.CurrentRow.Cells["PUR"].Value);
                String rateType = Convert.ToString(RATE_CODE.SelectedValue);
                string pricedecimal = dgBatch.CurrentRow.Cells[rateType].Value.ToString();
                double pricedec = Convert.ToDouble(pricedecimal);
                //PRICE.Text = pricedec.ToString("N3");
                PRICE.TextChanged -= new EventHandler(calculateGrossAmount);
                PRICE.Text = dgBatch.CurrentRow.Cells[rateType].Value.ToString();
                //PRICE.Text = (dataGridItem.CurrentRow.Cells["SALES"].Value.ToString("N3");
                sales_price = Convert.ToDouble(dgBatch.CurrentRow.Cells[rateType].Value);
                HASSERIAL = Convert.ToBoolean(dgBatch.CurrentRow.Cells["HASSERIAL"].Value);
                PNLSERIAL.Visible = HASSERIAL;
                mrp = Convert.ToDouble(dgBatch.CurrentRow.Cells["MRP"].Value);
                if (hasBatch)
                {
                    if (dgBatch.CurrentRow.Cells["BATCH CODE"].Value != null)
                        BATCH.Text = dgBatch.CurrentRow.Cells["BATCH CODE"].Value.ToString();
                    if (dgBatch.CurrentRow.Cells["EXPIRY DATE"].Value != null && dgBatch.CurrentRow.Cells["EXPIRY DATE"].Value.ToString() != "")
                        EXPIRY_DATE.Value = Convert.ToDateTime(dgBatch.CurrentRow.Cells["EXPIRY DATE"].Value);
                }
                tb_mrp.Text = mrp.ToString(decimalFormat);
                kryptonLabel36.Visible = HASSERIAL;
                SERIALNO.Visible = HASSERIAL;
                kryptonLabel36.Visible = HASSERIAL;
                SERIALNO.Visible = HASSERIAL;
                StockCheck();
                //if (hasBatch)
                //{
                //    BATCH.Focus();
                //}
                //else
                TaxId = Convert.ToInt16(dgBatch.CurrentRow.Cells["TaxId"].Value.ToString());
                GetTaxRate();
                //{
              
                //    tb_descr.Focus();

                //}

               // this.UOM.SelectedIndexChanged -= new EventHandler(UOM_SelectedIndexChanged);
                addUnits();
               // this.UOM.SelectedIndexChanged -= new EventHandler(UOM_SelectedIndexChanged);
               // UOM.Text = dgBatch.CurrentRow.Cells["UNIT_CODE"].Value.ToString();
               
                GetDiscount();
                ItemArabicName = dgBatch.CurrentRow.Cells["DESC_ARB"].Value.ToString();
                ITEM_NAME.Text = dgBatch.CurrentRow.Cells["ITEM NAME"].Value.ToString();
                PNL_DATAGRIDITEM.Visible = false;
                itemSelected = false;
                pricefob = Convert.ToDouble(PRICE.Text);
                if (RATE_CODE.Text.StartsWith("MRP"))
                {
                    double taxcalc = 0;
                    taxcalc = (Convert.ToDouble(ITEM_TAX_PER.Text) / 100) + 1;
                    PRICE.Text = (Convert.ToDouble(PRICE.Text) / taxcalc).ToString(decimalFormat);
                }
                else if (!hasSaleExclusive)
                {
                    double taxcalc = 0;
                    taxcalc = (Convert.ToDouble(ITEM_TAX_PER.Text) / 100) + 1;
                    PRICE.Text = (Convert.ToDouble(PRICE.Text) / taxcalc).ToString();
                }
                PRICE.TextChanged += new EventHandler(calculateGrossAmount);
                sales_price = Convert.ToDouble(PRICE.Text);
                PRICE.Text = sales_price.ToString();
                QUANTITY.Text = "1";

                if (SalebyItemCode)
                    ITEM_CODE.Focus();
                else if (MoveToUnit)
                    UOM.Focus();
                else if (MoveToQty)
                    QUANTITY.Focus();
                else if (HASSERIAL)
                    SERIALNO.Focus();
                else if (txtfree.Visible)
                    txtfree.Focus();
                else if (MoveToPrice)
                    PRICE.Focus();
                else if (tb_mrp.Visible)
                    tb_mrp.Focus();
                else if (GROSS_TOTAL.Visible)
                    GROSS_TOTAL.Focus();
                else if (MoveToDisc)
                    ITEM_DISCOUNT.Focus();
                else if (tb_netvalue.Visible)
                    tb_netvalue.Focus();
                else if (tb_descr.Visible)
                    tb_descr.Focus();
                else if (hasTax && MoveToTaxper)
                {
                    if (MoveToTaxper)
                        ITEM_TAX_PER.Focus();
                }
                else if (hasBatch)
                    BATCH.Focus();
                else
                {
                    addItem();
                    clearItem();
                    if (SalebyItemName)
                        ITEM_NAME.Focus();
                    else if (SalebyItemCode)
                        ITEM_CODE.Focus();
                    else if (SalebyBarcode)
                        BARCODE.Focus();
                    else
                        ITEM_NAME.Focus();
                }
            }
        }

        private void dgItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridItem_ColumnMinimumWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
          //  dgBatch.ColumnHeadersHeight = height_col_head;
        }

        private void dataGridItem_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
           // dgBatch.ColumnHeadersHeight = height_col_head;
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void chkInclusiveTax_CheckedChanged(object sender, EventArgs e)
        {
            if (chkInclusiveTax.Checked == true)
            {
                hasSaleExclusive = false;

            }
            else
            {
                hasSaleExclusive = true;
            }
        }
        public void Stockcheck()
        {

            if (dgBatch.RowCount > 0)
            {
                if (dgBatch.Rows[0].Cells["STOCK"].Value != null)
                    lblstock.Text = dgBatch.Rows[0].Cells["STOCK"].Value.ToString();
            }
            else if (dataGridItem.RowCount > 0 && dgBatch.Visible == false)
            {
                //if (dataGridItem.CurrentRow.Cells["STOCK"].Value.ToString()!= "")
                try
                {
                    dataGridItem.Focus();
                    lblstock.Text = dataGridItem.CurrentRow.Cells["STOCK"].Value.ToString();
                    ITEM_CODE.Focus();
                }
                catch
                {
                }
            }
        }
        private void dataGridItem_RowEnter(object sender, DataGridViewCellEventArgs e)
        {

        }
        public void StockCheck()
        {
            if (dgBatch.RowCount > 0)
            {
                try
                {
                    if (dgBatch.CurrentRow.Cells["STOCK"].Value != null && dgBatch.Visible == true)
                        lblstock.Text = dgBatch.CurrentRow.Cells["STOCK"].Value.ToString();
                    else
                        lblstock.Text = dgBatch.Rows[0].Cells["STOCK"].Value.ToString();
                }
                catch
                {
                }
            }
        }
        private void dgBatch_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
          
        }

        private void txtMultiSerials_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                qty1 = Convert.ToDouble(QUANTITY.Text);
                lineIndex = txtMultiSerials.GetLineFromCharIndex(cursorPosition);
                lineIndex += 1;
                if (lineIndex >= qty1)
                {
                    if (hasFree)
                    {
                        txtfree.Focus();
                    }
                    else
                    {
                        PRICE.Focus();
                    }
                    panMultiSerials.Visible = false;
                }
            }
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgItems_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            //if (dgItems.CurrentCell.ColumnIndex == 5)
            //{
            //    dgItems.CommitEdit(DataGridViewDataErrorContexts.Commit);
            //}
        }

        private void dgItems_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //double QTY = 0;
            //double PRICEFOB = 0;
            //try
            //{
            //    if (dgItems.CurrentCell.ColumnIndex == 5)
            //    {
            //        if (dgItems.CurrentCell.Value != null && dgItems.CurrentCell.Value.ToString() != "")
            //            QTY = Convert.ToDouble(dgItems.CurrentCell.Value);
            //        if (dgItems.CurrentRow.Cells["cPrice"].Value != null && dgItems.CurrentRow.Cells["cPrice"].Value.ToString() != "")
            //            PRICEFOB = Convert.ToDouble(dgItems.CurrentRow.Cells["cPrice"].Value);
            //        double gtotal = QTY * PRICEFOB;
            //        dgItems.CurrentRow.Cells[9].Value = gtotal.ToString();
            //    }
            //}
            //catch
            //{
            //}
        }

        private void BARCODE_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Scroll(object sender, ScrollEventArgs e)
        {
           
        }

        private void DOC_DATE_GRE_CloseUp(object sender, EventArgs e)
        {
          
        }

        private void kryptonLabel1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DOC_DATE_GRE.Focus();
            }
        }

        private void txtcashrcvd_TextChanged(object sender, EventArgs e)
        {
            if (txtcashrcvd.Text != "")
            {
                double amt = 0;
                double recAmt = 0;
                double balance = 0;
                amt = Convert.ToDouble(NET_AMOUNT.Text);
                recAmt = Convert.ToDouble(txtcashrcvd.Text);
                balance = (recAmt - amt);
                txtBalance.Text = balance.ToString(decimalFormat);
            }
            else
            {
                txtBalance.Text = "";
            }
        }

        private void txtcashrcvd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
               btnSave.Focus();
            }
        }

        private void dgBatch_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void kryptonLabel54_Paint(object sender, PaintEventArgs e)
        {

        }

        private void kryptonLinkLabel1_LinkClicked(object sender, EventArgs e)
        {
            if (PrintInvoice.Checked == true)
            {
                flagPrintEventAssigned = false;
                counter = 0;
                printingrecipt2();
            }
        }

        private void txtPay_Click(object sender, EventArgs e)
        {
            if (dgItems.RowCount > 0)
            {
                PaymentPanel.Visible = true;
                PaymentPanel.BringToFront();
                PaymentTab.SelectedIndex = 0;
                Payment_cash_amntrecvd.Text = "";
                Payment_cash_amntrecvd.Focus();
                txtPayTotal.Text = NET_AMOUNT.Text;
                txtPayCust.Text = CUSTOMER_NAME.Text;
            }
            else
            {
                MessageBox.Show("no item to sale");
            }
        }

        private void kryptonLabel77_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {

        }

        private void kryptonButton4_Click(object sender, EventArgs e)
        {

        }

        private void kryptonButton7_Click(object sender, EventArgs e)
        {
            KryptonButton cobtn = (KryptonButton)sender;
            if (controll is KryptonTextBox)
            {
                KeyPressEventArgs ee=new KeyPressEventArgs(Convert.ToChar( cobtn.Text));
                KryptonTextBox txt = (KryptonTextBox)controll;

                //if (OnlyFloat(controll, ee))
                //{
                    txt.Focus();
                   SendKeys.Send(cobtn.Text);
                   // txt.Text = txt.Text + cobtn.Text;
               // }
              
             
            }
        }

        private void Payment_cash_amntrecvd_Enter(object sender, EventArgs e)
        {
            controll =(Control) sender;
        }
        public static bool OnlyFloat(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
                return false;
            }

            if (e.KeyChar == '.' && (sender as KryptonTextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
                return false;
            }
            return true;
        }

        private void Payment_cash_amntrecvd_TextChanged(object sender, EventArgs e)
        {
            if (Payment_cash_amntrecvd.Text != "")
            {
                double amt = 0;
                double recAmt = 0;
                double balance = 0;
                amt = Convert.ToDouble(NET_AMOUNT.Text);
                recAmt = Convert.ToDouble(Payment_cash_amntrecvd.Text);
                balance = (amt - recAmt);
                Payment_cash_balance.Text = balance.ToString(decimalFormat);
            }
            else
            {
                Payment_cash_balance.Text = "0.00";
            }
        }

        private void kryptonButton15_Click(object sender, EventArgs e)
        {
            if (controll is KryptonTextBox)
            {
               // KeyPressEventArgs ee = new KeyPressEventArgs(Convert.ToChar(cobtn.Text));
                KryptonTextBox txt = (KryptonTextBox)controll;

                //if (OnlyFloat(controll, ee))
                //{
                txt.Focus();
                SendKeys.Send("{BKSP}");
                // txt.Text = txt.Text + cobtn.Text;
                // }


            }
        }

        private void PaymentTab_TabIndexChanged(object sender, EventArgs e)
        {
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            CommonHelp h = new CommonHelp(0, genEnum.Customer);
            if (h.ShowDialog() == DialogResult.OK && h.c != null)
            {
                CUSTOMER_CODE.Text = Convert.ToString(h.c[0].Value);
                CUSTOMER_NAME.Text = Convert.ToString(h.c[1].Value);
                txtPayCustName.Text = Convert.ToString(h.c[0].Value);
                kryptonTextBox2.Text = Convert.ToString(h.c[1].Value);
                customer(txtPayCustName.Text);

                CASHACC.SelectedValue = Convert.ToString(h.c["LedgerId"].Value);
                chkCustomeleveldiscount.Enabled = true;
                txtPayCustRecAmt.Focus();
              
            }
        }

        private void PaymentTab_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (PaymentTab.SelectedIndex == 0)
            {
                CASHACC.SelectedValue = "21";
            }
            else if (PaymentTab.SelectedIndex == 1)
            {
                if ( CUSTOMER_CODE.Text!=""&& CUSTOMER_NAME.Text!="")
                {
                    txtPayCustName.Text =  CUSTOMER_CODE.Text;
                    kryptonTextBox2.Text = CUSTOMER_NAME.Text;
                  
                }
                txtPayCustomerAmt.Text = NET_AMOUNT.Text;
                txtPayCustRecAmt.Focus();
            }
        }

        private void txtPayCustRecAmt_TextChanged(object sender, EventArgs e)
        {
            if (txtPayCustRecAmt.Text != "")
            {
                double amt = 0;
                double recAmt = 0;
                double balance = 0;
                amt = Convert.ToDouble(NET_AMOUNT.Text);
                recAmt = Convert.ToDouble(txtPayCustRecAmt.Text);
                balance = (amt - recAmt);
                txtPayCustBalAmt.Text = balance.ToString(decimalFormat);
            }
            else
            {
                Payment_cash_balance.Text = "0.00";
            }
        }

        private void kryptonButton43_Click(object sender, EventArgs e)
        {
            btnSave_Click(sender, e);
            //txtPayCustName.Text = "";
            //kryptonTextBox2.Text = "";
            //txtPayCustomerAmt.Text = "";
            //txtPayCustRecAmt.Text = "";
            //txtPayCustBalAmt.Text = "";
        }

        private void txtPayCustBalAmt_TextChanged(object sender, EventArgs e)
        {

        }

        private void kryptonButton42_Click(object sender, EventArgs e)
        {
            txtPayCustName.Text = "";
            kryptonTextBox2.Text = "";
            txtPayCustomerAmt.Text = "";
            txtPayCustRecAmt.Text = "";
            txtPayCustBalAmt.Text = "";
        }

        private void kryptonButton41_Click(object sender, EventArgs e)
        {
            PaymentPanel.Visible = false;
        }

        private void kryptonButton5_Click(object sender, EventArgs e)
        {
            PaymentPanel.Visible = false;
        }
    }
}

