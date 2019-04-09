using ComponentFactory.Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Sys_Sols_Inventory
{
    public partial class New_Sales : Form
    {
    //    public New_Sales()
    //    {
    //        InitializeComponent();
    //    }


        Initial mdi = (Initial)Application.OpenForms["Initial"];
        #region properties declaration
        private bool hasBatch = true;
        private bool hasTax = true;
        private bool hasBarcode = true;
        private bool hasArabic = true;
        private bool HasRoundoff = true;
        private DataTable unitsTable = new DataTable();
        private int selectedRow = -1;
        private string ID = "";
        private string type;
        private bool EditActive = false;
        private bool HasAccounts=false;

        bool SeelctLastPurchase = false;
        bool FocusCustomer = false;
        bool FocusSalesMan = false;
        bool MoveToTaxper = false;
        bool MoveToUnit = false;
        bool MoveToQty = false;
        bool SalebyItemName = false;
        bool SalebyItemCode = false;
        bool SalebyBarcode = false;


        double Discountpercentlimit, DiscountAmtlimit;
        bool HasSeasonDiscount = false;
        private bool HasDiscountLimit = false;


        bool HasCustomerDiscount = false;
        decimal CustomerDiscountValue = 0;
        string CustomerDiscountType = "";





        private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        private SqlCommand cmd = new SqlCommand();
        private SqlDataAdapter adapter = new SqlDataAdapter();
        private DataTable table = new DataTable();
        private DataTable filterTable = new DataTable();
        private DataTable rateTable = new DataTable();
        private BindingSource source = new BindingSource();
        private AutoCompleteStringCollection coll = new AutoCompleteStringCollection();
        Class.CompanySetup ComSet = new Class.CompanySetup();
        Class.Printing Printing = new Class.Printing();
        Class.PaymentDetails PaymDet = new Class.PaymentDetails();
        Class.DateSettings dset = new Class.DateSettings();

        Class.Ledgers led = new Class.Ledgers();
        Class.Transactions trans = new Class.Transactions();
        private DateTime TransDate;
        #endregion
      
        Login lg = (Login)Application.OpenForms["Login"];
        Class.ModifiedTransaction modtrans = new Class.ModifiedTransaction();
        Class.Stock_Report Stkr = new Class.Stock_Report();

        String ItemArabicName = "";
        decimal DecPOSVoucherTypeId = 0;        //to get the selected voucher type id from frmVoucherTypeSelection       
        decimal decPOSSuffixPrefixId = 0;
        decimal decProductId = 0;               //to fill product using barcode
        decimal decBatchId;
        decimal decCurrentConversionRate;
        private bool itemSelected = false;
        decimal decCurrentRate;
        decimal decSalesMasterId = 0;
        decimal decSalesDetailsId = 0;
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
        bool MoveToPrice = false;
        bool FocusDate = false;
        decimal balance = 0;
        string status = "Active";
        bool MoveToDisc = false;
        bool ShowPurchase = false;
        bool PrintInvoices = true;
        bool StockOut = false;

        bool editsales = false;
        bool secondsave = false;
        bool firstitemlistbyname = false;
        private bool HasArabicInvoice = true;
        string DafaultRateType = "";
        public decimal PurchasePrice = 0;
        public decimal Profit = 0;
        public bool ShowStock = false;
        bool ActiveForm = false;
        int TaxId;
        bool Customerselect = false, creditneeded = false, Roundofflimit = false;



        string CompanyName, ArabicName,SalesManCode, EngBranch, ArbBranch, Place, ArbPlace, ArbCity, City, Phone, Email, Fax, TineNo, Billno, Date, CUSID, Website, panno, vat, Address1, ArbAddress1, ArbAddress2, Address2, logo, DiscType, DiscValue;

     



        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            
            {
                if (keyData == (Keys.Alt | Keys.S))
                {
                    if (secondsave == false)
                    {
                        if (dgItems.Rows.Count > 0)
                        {
                            secondsave = true;
                            PaymentPanel.Visible = true;
                            PaymentTab.Focus();
                            Payment_cash_amntrecvd.Focus();
                        }
                        else
                        {
                            MessageBox.Show("Add Items to Sale");
                        }
                        return true;
                    }
                   
                }
                else if(keyData==(Keys.Escape))
                {
                    secondsave = false;
                    PaymentPanel.Visible=false;
                    ShowStock = false;
                    dataGridItem.Visible = false;
                    if (SalebyItemCode)
                        ITEM_CODE.Focus();
                    else if (SalebyBarcode)
                        BARCODE.Focus();
                    else
                        ITEM_NAME.Focus();
                    return true;
                }
                else if (keyData == (Keys.LControlKey))
                {
                    dgItems.Focus();
                    return true;
                }
               
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        public void BindCurrency()
        {
            DataTable dt = new DataTable();
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = "SELECT * FROM GEN_CURRENCY";
            adapter.SelectCommand = cmd;
            adapter.Fill(dt);
            CURRENCY_CODE.DataSource = dt;
            CURRENCY_CODE.DisplayMember = "DESC_ENG";
            CURRENCY_CODE.ValueMember = "CODE";
            conn.Close();
        }


        public void BindSettings()
        {
            try
            {
                DataTable dt = new DataTable();
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "SELECT * FROM SYS_SETUP";
                adapter.SelectCommand = cmd;
                adapter.Fill(dt);


                if (dt.Rows.Count > 0)
                {



                    hasBatch = Convert.ToBoolean(dt.Rows[0]["BATCH"]);
                    hasTax = Convert.ToBoolean(dt.Rows[0]["TAX"]);
                    hasBarcode = Convert.ToBoolean(dt.Rows[0]["BARCODE"]);
                    hasArabic = Convert.ToBoolean(dt.Rows[0]["Arabic"]);
                    MoveToPrice = Convert.ToBoolean(dt.Rows[0]["MoveToPrice"]);

                    MoveToDisc = Convert.ToBoolean(dt.Rows[0]["MoveToDisc"]);
                    ShowPurchase = Convert.ToBoolean(dt.Rows[0]["ShowPurchase"]);

                    SeelctLastPurchase = Convert.ToBoolean(dt.Rows[0]["SelectLastPurchase"]);
                    FocusCustomer = Convert.ToBoolean(dt.Rows[0]["FocusCustomer"]);
                    FocusSalesMan = Convert.ToBoolean(dt.Rows[0]["FocusSalesMan"]);
                    MoveToTaxper = Convert.ToBoolean(dt.Rows[0]["MoveToTaxper"]);
                    MoveToUnit = Convert.ToBoolean(dt.Rows[0]["MoveToUnit"]);
                    MoveToQty = Convert.ToBoolean(dt.Rows[0]["MoveToQty"]);
                    SalebyItemName = Convert.ToBoolean(dt.Rows[0]["SalebyItemName"]);
                    SalebyItemCode = Convert.ToBoolean(dt.Rows[0]["SalebyItemCode"]);
                    SalebyBarcode = Convert.ToBoolean(dt.Rows[0]["SalebyBarcode"]);
                    FocusDate = Convert.ToBoolean(dt.Rows[0]["FocusDate"]);

                    Discountpercentlimit = Convert.ToDouble(dt.Rows[0]["DiscountPerct"]);
                    DiscountAmtlimit = Convert.ToDouble(dt.Rows[0]["DiscountAmt"]);
                    HasDiscountLimit = Convert.ToBoolean(dt.Rows[0]["Hasdiscountlimit"]);
                    HasCustomerDiscount = Convert.ToBoolean(dt.Rows[0]["AllowCustomerDiscount"]);
                    DafaultRateType = Convert.ToString(dt.Rows[0]["DefaultRateType"]);
                  //  StockOut = Convert.ToBoolean(dt.Rows[0]["StockOut"]);
                    PrintInvoices = Convert.ToBoolean(dt.Rows[0]["PrintInvoice"]);
                    conn.Close();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        public New_Sales(string docType, string prefix)
        {
            InitializeComponent();

            BindSettings();

          //  hasBatch = General.IsEnabled(Settings.Batch);
            //hasTax = General.IsEnabled(Settings.Tax);
            //hasBarcode = General.IsEnabled(Settings.Barcode);
            //hasArabic = General.IsEnabled(Settings.Arabic);
            //MoveToPrice = General.IsEnabled(Settings.MoveToPrice);
            HasRoundoff = General.IsEnabled(Settings.HasRoundOff);
            HasAccounts = Properties.Settings.Default.Account;
            //MoveToDisc = General.IsEnabled(Settings.MoveToDisc);
            //ShowPurchase = General.IsEnabled(Settings.ShowPurchase);
            unitsTable.Columns.Add("key");
            unitsTable.Columns.Add("value");
            UOM.DataSource = unitsTable;
            type = docType;
            Text += " - " + prefix;

            QUANTITY.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            PRICE.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            if (hasTax)
            {
                ITEM_TAX_PER.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            }
            ITEM_DISCOUNT.KeyPress += new KeyPressEventHandler(General.OnlyFloat);

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
            //cmbFilter.DataSource = filterTable;
            //cmbFilter.DisplayMember = "key";

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT CODE AS [key],CODE+' - '+DESC_ENG AS value FROM GEN_PRICE_TYPE";
            adapter.Fill(rateTable);
            RATE_CODE.DisplayMember = "value";
            RATE_CODE.ValueMember = "key";
            RATE_CODE.DataSource = rateTable;


            RATE_CODE.SelectedValue = DafaultRateType;
        }

        private void getDetails()
        {
        //    try
        //    {
        //        table.Rows.Clear();
        //        cmd.CommandText = "SELECT * FROM viewSalesHDR";
        //        cmd.CommandType = CommandType.Text;
        //        adapter.Fill(table);
        //        source.DataSource = table;
        //        dgDetail.DataSource = source;
        //    }
        //    catch
        //    {
        //    }
        }

        //private void Sales_Load(object sender, EventArgs e)
        //{
           
        //    GetCompanyDetails();
        //    GetBranchDetails();
        //    PrintPage.SelectedIndex = 0;
        //    if (!hasBatch)
        //    {
        //        panBatch.Visible = false;
        //        cBatch.Visible = false;
        //        cExpDate.Visible = false;
        //    }

        //    if (!hasTax)
        //    {
        //        panTax.Visible = false;
        //        cTaxPer.Visible = false;
        //        cTaxAmt.Visible = false;
        //    }

        //    panBarcode.Visible = hasBarcode;
        //}


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
                    PrintPage.Text = dt.Rows[0][6].ToString();
                     CURRENCY_CODE.SelectedValue = dt.Rows[0]["DEFAULT_CURRENCY_CODE"].ToString();
                    

                }
            }
            catch
            {
            }
        }
        public void GetCompanyDetails()
        {
            //DataTable dt = new DataTable();
            //dt = ComSet.getCompanyDetails();
            //if (dt.Rows.Count > 0)
            //{
            //    CompanyName = dt.Rows[0][1].ToString();
            //    TineNo = dt.Rows[0][8].ToString();
            //    CUSID = dt.Rows[0][10].ToString();
            //    Website = dt.Rows[0][11].ToString();
            //    panno = dt.Rows[0][9].ToString();
            //}
            DataTable dt = new DataTable();
            dt = ComSet.getCompanyDetails();
            if (dt.Rows.Count > 0)
            {
                CompanyName = dt.Rows[0][1].ToString();
                TineNo = dt.Rows[0][8].ToString();
                CUSID = dt.Rows[0][10].ToString();
                Website = dt.Rows[0][11].ToString();
                panno = dt.Rows[0][9].ToString();
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
        public void bindledgers()
        { 
            DataTable dt1 = new DataTable();
            dt1 = led.Selectledger();
            SALESACC.DataSource = dt1;
            SALESACC.DisplayMember = "LEDGERNAME";
            SALESACC.ValueMember = "LEDGERID";

            DataTable dt2 = new DataTable();
            dt2 = led.Selectledger();
            CASHACC.DataSource = dt2;
            CASHACC.DisplayMember = "LEDGERNAME";
            CASHACC.ValueMember = "LEDGERID";
        }

        private void ITEM_CODE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (sender is KryptonTextBox)
                {
                    string name = (sender as KryptonTextBox).Name;
                    switch (name)
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
                        case "QUANTITY":
                            PRICE.Focus();
                            break;

                        case "PRICE":
                            if (hasTax)
                            {
                                ITEM_TAX_PER.Focus();
                            }
                            break;

                        case "ITEM_TAX_PER":
                            ITEM_TAX.Focus();
                            break;

                        case "ITEM_TAX":
                            GROSS_TOTAL.Focus();
                            break;

                        case "GROSS_TOTAL":
                            ITEM_DISCOUNT.Focus();
                            break;

                        case "ITEM_DISCOUNT":
                            ITEM_TOTAL.Focus();
                            break;
                        case "ITEM_TOTAL":
                            addItem();
                            clearItem();
                            ITEM_CODE.Focus();
                            break;
                        default:
                            break;
                    }
                }
                else if(sender is DateTimePicker)
                {
                    UOM.Focus();
                }
                else if (sender is KryptonComboBox)
                {
                    QUANTITY.Focus(); 
                }
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
        }


        private void assignBatch()
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
                ITEM_NAME.Focus();
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
                        if (Convert.ToDecimal(QUANTITY.Text) > Convert.ToDecimal(lblstock.Text))
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
            if (CheckItemStokOut())
            {
                if (itemValid())
                {
                    if (selectedRow == -1)
                    {
                        selectedRow = dgItems.Rows.Add(new DataGridViewRow());
                    }
                    DataGridViewCellCollection c = dgItems.Rows[selectedRow].Cells;
                    c["cCode"].Value = ITEM_CODE.Text;
                    c["cName"].Value = ITEM_NAME.Text;
                    if (hasBatch)
                    {
                        c["cBatch"].Value = BATCH.Text;
                        c["cExpDate"].Value = EXPIRY_DATE.Value.ToString("dd/MM/yyyy");
                    }
                    c["cUnit"].Value = UOM.Text;
                    c["cQty"].Value = QUANTITY.Text;
                    c["cPrice"].Value = decimal.Round(Convert.ToDecimal(PRICE.Text), 2, MidpointRounding.AwayFromZero);
                    if (hasTax)
                    {
                        c["cTaxPer"].Value = ITEM_TAX_PER.Text;
                        c["cTaxAmt"].Value = ITEM_TAX.Text;
                    }
                    c["cGTotal"].Value = GROSS_TOTAL.Text;
                    //c["cDisc"].Value = ITEM_DISCOUNT.Text;
                    c["DiscTypes"].Value = DiscType;
                    c["cDisc"].Value = DiscAmt;
                    if (DiscAmt == null)
                    {
                        c["cDisc"].Value = "0";
                        c["DiscValues"].Value = "0";
                    }
                    c["DiscValues"].Value = ITEM_DISCOUNT.Text;
                    //if (DiscAmt == null)
                    //{
                    //    c["cDisc"].Value = "0";
                    //}

                    c["cTotal"].Value = decimal.Round(Convert.ToDecimal(ITEM_TOTAL.Text), 2, MidpointRounding.AwayFromZero);

                    totalCalculation();
                    Disctext.Text = "Disc %";
                    DiscType = "Percentage";
                    c["cDescArb"].Value = ItemArabicName;
                    ActiveForm = true;
                }
            }
            else
            {
                MessageBox.Show("Selected Item Quantity is not in stock");
                clearItem();

            }
        }


        


        private void btnDocNo_Click(object sender, EventArgs e)
        {
              SalesHelp h = new SalesHelp(0, type);
            if (h.ShowDialog() == DialogResult.OK && h.c != null)
            {
                btnClear.PerformClick();
                editsales = true;
                ID = Convert.ToString(h.c["DOC_NO"].Value);
                DOC_NO.Text = ID;
                try
                {
                    DOC_DATE_GRE.Value = Convert.ToDateTime(Convert.ToString(h.c["DOC_DATE_GRE"].Value));
                }
                catch
                {
                }
                DOC_DATE_HIJ.Text = Convert.ToString(h.c["DOC_DATE_HIJ"].Value);
                CURRENCY_CODE.Text = Convert.ToString(h.c["CURRENCY_CODE"].Value);
                DOC_REFERENCE.Text = Convert.ToString(h.c["DOC_REFERENCE"].Value);
                CUSTOMER_CODE.Text = Convert.ToString(h.c["CUSTOMER_CODE"].Value);
                GetLedgerId(Convert.ToString(h.c["CUSTOMER_CODE"].Value));
                CUSTOMER_NAME.Text = Convert.ToString(h.c["CUSTOMER_NAME_ENG"].Value);
              //  SALESMAN_CODE.Text = Convert.ToString(h.c["SALESMAN_CODE"].Value);
                NOTES.Text = Convert.ToString(h.c["NOTES"].Value);
                TAX_TOTAL.Text = Convert.ToString(h.c["TAX_TOTAL"].Value);
                VAT.Text = Convert.ToString(h.c["VAT"].Value);
                DISCOUNT.Text = Convert.ToString(h.c["DISCOUNT"].Value);
                TOTAL_AMOUNT.Text = Convert.ToString(h.c["TOTAL_AMOUNT"].Value);
                NET_AMOUNT.Text = Convert.ToString(h.c["NET_AMOUNT"].Value);
                PAY_CODE.Text = Convert.ToString(h.c["PAY_CODE"].Value);
                PAY_NAME.Text = General.getName(PAY_CODE.Text, "GEN_PAYTYPE");
                CARD_NO.Text = Convert.ToString(h.c["CARD_NO"].Value);

                TxtRoundOffAmt.Text = Convert.ToString(h.c["ROUNDOFF"].Value);
                txtFreight.Text = Convert.ToString(h.c["FREIGHT"].Value);
                TXT_CESS.Text = Convert.ToString(h.c["CESS"].Value);

                if (CUSTOMER_CODE.Text != "")
                {
                    chkCustomeleveldiscount.Checked = true;
                }


                conn.Open();
                cmd.CommandText = "SELECT * FROM INV_SALES_DTL WHERE DOC_NO = '"+DOC_NO.Text+"'";
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

                chkCustomeleveldiscount.Checked = Convert.ToBoolean(h.c["CUSLEVELDISCOUNT"].Value);
                GetPaymentDetails();
            }
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
        private bool valid()
        {
            if (CUSTOMER_CODE.Text == "" && type == "SAL.CRD")
            {
                MessageBox.Show("You must select a Customer for Credit Sale!");
                PaymentTab.SelectedIndex = 4;
                Payment_cash_amntrecvd.Focus();
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
            try
            {
                if (HasRoundoff)
                {
                    if (type != "SAL.CRD")
                    {

                        decimal amt = ((Convert.ToDecimal(lbltotal.Text) - Convert.ToDecimal(lbltotalamtrecived.Text)));
                        decimal check = decimal.Truncate(amt);

                        if (Convert.ToDecimal(TxtRoundOffAmt.Text) < check)
                        {
                            MessageBox.Show("Round of Amount is less than balance");
                            TxtRoundOffAmt.Focus();
                            return false;

                        }
                    }
                }
            }
            catch
            {
                return false;
            }



            if (ID != "")
            {
                if (NOTES.Text == "")
                {
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
                        double amountafterpurchase = credit + Convert.ToDouble(NET_AMOUNT.Text) - Convert.ToDouble(Payment_cash_amntrecvd.Text);
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

        public bool IsActive()
        {
            string result = "";
            try
            {
                conn.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT CreditActive FROM REC_CUSTOMER where CODE='" + CUSTOMER_CODE.Text + "'";
                result = Convert.ToString(cmd.ExecuteScalar());
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (result == "True")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public double creditamt()
        {
            double data = 0;
            try
            {
                DataTable dt = new DataTable();
                conn.Open();
                cmd.CommandText = "SELECT     SUM(DEBIT)-SUM(CREDIT)  AS creditamt FROM         tb_Transactions WHERE     (ACCNAME = '" + CASHACC.Text + "') AND (ACCID = '" + CASHACC.SelectedValue + "')";
                cmd.CommandType = CommandType.Text;
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    data = Convert.ToDouble(dt.Rows[0][0].ToString());
                }
                conn.Close();
            }
            catch
            { }
            return data;
        }

        public double GetCustomerCreditLimit()
        {
            double data = 0;
            try
            {
                DataTable dt = new DataTable();
                conn.Open();
                cmd.CommandText = "SELECT     REC_CUSTOMER_TYPE.CREDIT_LEVEL FROM         REC_CUSTOMER INNER JOIN REC_CUSTOMER_TYPE ON REC_CUSTOMER.TYPE = REC_CUSTOMER_TYPE.CODE WHERE     (REC_CUSTOMER.CODE = '" + CUSTOMER_CODE.Text + "')";
                cmd.CommandType = CommandType.Text;
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    data = Convert.ToDouble(dt.Rows[0][0].ToString());
                }
                conn.Close();

            }
            catch
            {

            }
            return data;
        }

                public void GetPaymentDetails()
                {
                    try
                    {
                        DataTable dt = new DataTable();
                        PaymDet.Doc_No = DOC_NO.Text;
                        dt = PaymDet.GetPaymentDetails();
                        if (dt.Rows.Count > 0)
                        {
                            Payment_cash_amntrecvd.Text = dt.Rows[0][2].ToString();
                            Payment_cash_balance.Text = dt.Rows[0][3].ToString();
                            CARD_NO.Text = dt.Rows[0][4].ToString();
                            Payment_Card_Amont.Text = dt.Rows[0][5].ToString();
                            Payment_Creditnote_no.Text = dt.Rows[0][6].ToString();
                            Payment_CreditNote_Amount.Text = dt.Rows[0][7].ToString();
                            Payment_Check_No.Text = dt.Rows[0][8].ToString();
                            Payment_Check_Amount.Text = dt.Rows[0][9].ToString();
                            Payment_Check_Bankname.Text = dt.Rows[0][10].ToString();
                            dateTimePicker1.Text = dt.Rows[0][11].ToString();
                            CUSTOMER_CODE.Text = dt.Rows[0][12].ToString();
                            CUSTOMER_NAME.Text = dt.Rows[0][13].ToString();
                            Payment_Credit_amount.Text = dt.Rows[0][14].ToString();
                            DOC_DATE_GRE.Text = dt.Rows[0][15].ToString();
                            lblDiscount.Text =Spell.SpellAmount.comma(Convert.ToDecimal( DISCOUNT.Text));
                            lbltotal.Text =Spell.SpellAmount.comma(Convert.ToDecimal(NET_AMOUNT.Text));
                            lbltotalamtrecived.Text =Spell.SpellAmount.comma(Convert.ToDecimal(dt.Rows[0][17].ToString()));
                        }
                        

                        

                    }
                    catch
                    {
                    }
                }


        private void btnCust_Click(object sender, EventArgs e)
        {
            try
            {
                // btnClear.PerformClick();
                //customer
                CommonHelp h = new CommonHelp(0, genEnum.Customer);
                if (h.ShowDialog() == DialogResult.OK && h.c != null)
                {
                    CUSTOMER_CODE.Text = Convert.ToString(h.c[0].Value);
                    CUSTOMER_NAME.Text = Convert.ToString(h.c[1].Value); 
                    creditneeded = true;
                    CASHACC.SelectedValue = Convert.ToString(h.c["LedgerId"].Value);

                   
                }
            }
            catch
            {
            }
           // Payment_Credit_amount.Focus();
            Payment_Credit_amount.Text = (Convert.ToDecimal(NET_AMOUNT.Text) - Convert.ToDecimal(Payment_cash_amntrecvd.Text)).ToString();

        }

        private void btnSales_Click(object sender, EventArgs e)
        {
        
        }

      

        private void RATE_CODE_SelectedIndexChanged(object sender, EventArgs e)
        {
            getRate();
            if (ShowStock)
            {
                bindgridview();
            }
        }
          private void getRate()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GET_RATE1";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@ITEM_CODE", ITEM_CODE.Text);
            cmd.Parameters.AddWithValue("@UNIT_CODE", UOM.Text);
            cmd.Parameters.AddWithValue("RATE_CODE", RATE_CODE.SelectedValue);
            string price = Convert.ToString(cmd.ExecuteScalar());
            PRICE.Text = price;
            conn.Close();
        }

        private void UOM_SelectedIndexChanged(object sender, EventArgs e)
        {
              getRate();
        }   

        private void BARCODE_KeyDown(object sender, KeyEventArgs e)
        {
            // if (BARCODE.Text != "")
            //{
            //    if (e.KeyCode == Keys.Enter)
            //    {
            //        DataTable t = General.Product4mBarcode(BARCODE.Text);
            //        if (t.Rows.Count > 0)
            //        {
            //            ITEM_CODE.Text = t.Rows[0][0].ToString();
            //            ITEM_NAME.Text = t.Rows[0][1].ToString();
            //            if (hasBatch)
            //            {
            //                BATCH.Focus();
            //            }
            //            else
            //            {
            //                QUANTITY.Focus();
            //            }
            //            addUnits();
            //            UOM.Text = t.Rows[0][2].ToString();
            //            TaxId = Convert.ToInt16(t.Rows[0][3].ToString());
            //            GetTaxRate();
            //        }
            //    }
            //}

            if (BARCODE.Text != "")
            {
                if (e.KeyCode == Keys.Enter)
                {
                    DataTable t = General.Product4mBarcode(BARCODE.Text);
                    if (t.Rows.Count > 0)
                    {
                        ITEM_CODE.Text = t.Rows[0][0].ToString();
                        ITEM_NAME.Text = t.Rows[0][1].ToString();
                        if (hasBatch)
                        {
                            BATCH.Focus();
                        }
                        else
                        {

                        }
                        addUnits();
                        UOM.Text = t.Rows[0][2].ToString();
                        TaxId = Convert.ToInt16(t.Rows[0][3].ToString());
                        GetTaxRate();
                        QUANTITY.Text = "1";
                        addItem();
                        clearItem();
                        BARCODE.Focus();
                        BARCODE.Text = "";

                    }
                }
            }
            else if(e.KeyData==Keys.Enter)
            {
                ITEM_NAME.Focus();
            }


        }


        public void GetTaxRate()
        {
            try
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
            catch (Exception ex)
            {
                conn.Close();
            }
        }
        private void addUnits()
        {
            unitsTable.Rows.Clear();
            try
            {
                cmd.CommandText = "SELECT UNIT_CODE FROM INV_ITEM_DIRECTORY_UNITS WHERE ITEM_CODE = '" + ITEM_CODE.Text + "'";
                cmd.CommandType = CommandType.Text;
                adapter.Fill(unitsTable);
                UOM.DataSource = unitsTable;
                UOM.DisplayMember = "UNIT_CODE";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            getRate();
        }
        private void BATCH_Enter(object sender, EventArgs e)
        {
            assignBatch();
        }

        private void linkRemoveRecord_LinkClicked(object sender, EventArgs e)
        {
            if (dgItems.Rows.Count > 0 && dgItems.CurrentRow != null)
            {
                dgItems.Rows.Remove(dgItems.CurrentRow);
                totalCalculation();
            }
        }

        private void totalCalculation()
        {
            double grossTotal = 0, discount = 0, nettAmount = 0, tax = 0, vat = 0;
            for (int i = 0; i < dgItems.Rows.Count; i++)
            {
                grossTotal = grossTotal + Convert.ToDouble(dgItems.Rows[i].Cells["cGTotal"].Value);
                discount = discount + Convert.ToDouble(dgItems.Rows[i].Cells["cDisc"].Value);
                nettAmount = nettAmount + Convert.ToDouble(dgItems.Rows[i].Cells["cTotal"].Value);
                if (hasTax)
                {
                    tax = tax + Convert.ToDouble(dgItems.Rows[i].Cells["cTaxAmt"].Value);
                    vat = tax * .01;
                }
            }

            TOTAL_AMOUNT.Text = grossTotal.ToString();
            DISCOUNT.Text = discount.ToString();
            lblDiscount.Text = discount.ToString();
            NET_AMOUNT.Text = nettAmount.ToString();
            lbltotal.Text = Spell.SpellAmount.comma(Convert.ToDecimal(nettAmount.ToString()));
            if (hasTax)
            {
                TAX_TOTAL.Text = tax.ToString();
                VAT.Text = vat.ToString();
            }
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            if (valid())
            {
                string status = "Added!";
                if (ID == "")
                {
                    GetDocId();
                    DOC_NO.Text = General.generateSalesID();

                    cmd.CommandText = "INSERT INTO INV_SALES_HDR(DOC_NO,DOC_TYPE,DOC_DATE_GRE,DOC_DATE_HIJ,CURRENCY_CODE,DOC_REFERENCE,CUSTOMER_CODE,CUSTOMER_NAME_ENG,SALESMAN_CODE,NOTES,TAX_TOTAL,VAT,DISCOUNT,TOTAL_AMOUNT,PAY_CODE,CARD_NO,FREIGHT,ROUNDOFF,CESS,CUSLEVELDISCOUNT) ";
                    cmd.CommandText += "VALUES('" + DOC_NO.Text + "','" + type + "','" + DOC_DATE_GRE.Value.ToString("MM/dd/yyyy") + "','" + DOC_DATE_HIJ.Text + "','" + CURRENCY_CODE.Text + "','" + DOC_REFERENCE.Text + "','" + CUSTOMER_CODE.Text + "','" + CUSTOMER_NAME.Text + "','" + SalesManCode + "','" + NOTES.Text + "','" + TAX_TOTAL.Text + "','" + VAT.Text + "','" + DISCOUNT.Text + "','" + TOTAL_AMOUNT.Text + "','" + PAY_CODE.Text + "','" + CARD_NO.Text + "','" + txtFreight.Text + "','" + TxtRoundOffAmt.Text + "','" + TXT_CESS.Text + "','" +chkCustomeleveldiscount.Checked + "');";
                    cmd.CommandText += "INSERT INTO INV_SALES_DTL(DOC_TYPE,DOC_NO,ITEM_CODE,ITEM_DESC_ENG,UOM,PRICE,QUANTITY,ITEM_DISCOUNT";
                    if (hasTax)
                    {
                        cmd.CommandText += ",ITEM_TAX_PER,ITEM_TAX";
                    }
                    if (hasBatch)
                    {
                        cmd.CommandText += ",BATCH,EXPIRY_DATE";
                    }
                    cmd.CommandText += ")";
                    for (int i = 0; i < dgItems.Rows.Count; i++)
                    {
                        DataGridViewCellCollection c = dgItems.Rows[i].Cells;
                        cmd.CommandText += "SELECT '" + type + "','" + DOC_NO.Text + "','" + c["cCode"].Value + "','" + c["cName"].Value + "','" + c["cUnit"].Value + "','" + c["cPrice"].Value + "','" + c["cQty"].Value + "','" + c["cDisc"].Value + "'";
                        if (hasTax)
                        {
                            cmd.CommandText += ",'" + c["cTaxPer"].Value + "','" + c["cTaxAmt"].Value + "'";
                        }
                        if (hasBatch)
                        {
                            cmd.CommandText += ",'" + c["cBatch"].Value + "','" + DateTime.ParseExact(c["cExpDate"].Value.ToString(), "dd/MM/yyyy", null).ToString("MM/dd/yyyy") + "'";
                        }
                        cmd.CommandText += " UNION ALL ";
                    }
                    //inserting details into credit table 
                    if (type == "SAL.CREDITNOTE")
                    {
                        InsertIntoCreditTable();
                    }
                }
                else
                {
                    status = "Updated!";
                    cmd.CommandText = "UPDATE INV_SALES_HDR SET DOC_TYPE = '',DOC_DATE_GRE = '" + DOC_DATE_GRE.Value.ToString("MM/dd/yyyy") + "',DOC_DATE_HIJ = '" + DOC_DATE_HIJ.Text + "',CURRENCY_CODE = '" + CURRENCY_CODE.Text + "',DOC_REFERENCE = '" + DOC_REFERENCE.Text + "',CUSTOMER_CODE = '" + CUSTOMER_CODE.Text + "',CUSTOMER_NAME_ENG = '" + CUSTOMER_NAME.Text + "',SALESMAN_CODE = '" + SalesManCode + "',NOTES = '" + NOTES.Text + "',TAX_TOTAL = '" + TAX_TOTAL.Text + "',VAT = '" + VAT.Text + "',DISCOUNT = '" + DISCOUNT.Text + "',TOTAL_AMOUNT = '" + TOTAL_AMOUNT.Text + "',PAY_CODE = '" + PAY_CODE.Text + "',FREIGHT = '" + txtFreight.Text + "',CARD_NO = '" + CARD_NO.Text + "',ROUNDOFF = '" + TxtRoundOffAmt.Text + "',CESS = '" + TXT_CESS.Text + "',CUSLEVELDISCOUNT = '" + chkCustomeleveldiscount.Checked + "' WHERE DOC_NO = '" + DOC_NO.Text + "';";
                    cmd.CommandText += "DELETE FROM INV_SALES_DTL WHERE DOC_NO = '" + DOC_NO.Text + "'";
                    cmd.CommandText += "INSERT INTO INV_SALES_DTL(DOC_NO,ITEM_CODE,ITEM_DESC_ENG,UOM,PRICE,QUANTITY,ITEM_DISCOUNT";
                    if (hasTax)
                    {
                        cmd.CommandText += ",ITEM_TAX_PER,ITEM_TAX";
                    }
                    if (hasBatch)
                    {
                        cmd.CommandText += ",BATCH,EXPIRY_DATE";
                    }
                    cmd.CommandText += ")";
                    for (int i = 0; i < dgItems.Rows.Count; i++)
                    {
                        DataGridViewCellCollection c = dgItems.Rows[i].Cells;
                        cmd.CommandText += "SELECT '" + DOC_NO.Text + "','" + c["cCode"].Value + "','" + c["cName"].Value + "','" + c["cUnit"].Value + "','" + c["cPrice"].Value + "','" + c["cQty"].Value + "','" + c["cDisc"].Value + "'";
                        if (hasTax)
                        {
                            cmd.CommandText += ",'" + c["cTaxPer"].Value + "','" + c["cTaxAmt"].Value + "'";
                        }
                        if (hasBatch)
                        {
                            cmd.CommandText += ",'" + c["cBatch"].Value + "','" + DateTime.ParseExact(c["cExpDate"].Value.ToString(), "dd/MM/yyyy", null) + "'";
                        }
                        cmd.CommandText += " UNION ALL ";
                    }


                    DeleteTransation();
                    //if (type == "SAL.CREDITNOTE")
                    //{
                    //    InsertIntoCreditTable();
                    //}

                }

                cmd.CommandText = cmd.CommandText.Substring(0, cmd.CommandText.Length - 10);
                try
                {
                    conn.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    if (type == "SAL.CREDITNOTE")
                    {
                        MessageBox.Show("Credit Note " + status);
                    }
                    else
                    {
                        MessageBox.Show("Sales " + status);
                        if (PrintInvoice.Checked == true)
                        {
                            PrintingInitial();
                        }
                    }
                    btnClear.PerformClick();
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public void GetCreditNote()
        {
            try
            {
               
                    SqlDataAdapter DataAdapter=new SqlDataAdapter();
                DataTable dt=new DataTable();
               SqlCommand Cmd=new SqlCommand();
               Cmd.Connection = conn;
              Cmd.CommandText = "select Nett_Amount,CN_Balance from tbl_CreditNote where CN_Doc_No=" + Payment_Creditnote_no.Text;
               DataAdapter.SelectCommand = Cmd;
                DataAdapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    Payment_CreditNote_Amount.Text = dt.Rows[0][0].ToString();
                    Payment_CreditNote_Amount.Focus();
                }
                else
                {
                    MessageBox.Show("Credit Note Number is Not Valid");
                    Payment_CreditNote_Amount.Focus();
                }
            }
            catch
            {
                Payment_CreditNote_Amount.Focus();
            }
        }

       public void InsertIntoCreditTable()
        {
            try
            {
                
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "insert into tbl_CreditNote(CN_Doc_No,CN_Date,CN_DateHij,CN_Reffrence_No,CUSTOMER_CODE,CUSTOMER_NAME_ENG,NOTES,CN_Balance,Nett_Amount,Status)values('" + DOC_NO.Text + "','" + DOC_DATE_GRE.Text + "','" + DOC_DATE_HIJ.Text + "','" + DOC_REFERENCE.Text + "','" + CUSTOMER_CODE.Text + "','" + CUSTOMER_NAME.Text + "','" + NOTES.Text + "','"+balance+"','" + NET_AMOUNT.Text + "','" + status + "')";
                    cmd.ExecuteNonQuery();
                    conn.Close();
                
            }
            catch
            {
                conn.Close();
            }
        }

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



          bool oldbill;
        public void printingrecipt()
        {
            try
            {

                int height = (dgItems.Rows.Count ) * 23;



                // PrintDialog printdialog = new PrintDialog();
                PrintDocument printDocument = new PrintDocument();
                if (PrintPage.SelectedIndex==0)
                {
                    printDocument.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("smallzize", 360, height + 240);
                    printDialog1.Document = printDocument;


                    printDocument.PrintPage += printDocument_PrintPage;
                    // printDocument.Print();
                    //DialogResult result = printDialog1.ShowDialog();
                    //if (result == DialogResult.OK)
                    //{
                        printDocument.Print();


                   // }
                }
                else
                {
                    if (hasArabic)
                    {

                        PrintDocument printDocumentArabicA4 = new PrintDocument();
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
                        printDocumentArabicA4.Print();


                    }
                    else
                    {
                        PrintDocument printDocumentA4 = new PrintDocument();
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
                        printDocumentA4.Print();


                        //  }
                    }
                }
               

                
            }
            catch
            {
                MessageBox.Show("printing Problem");
            }


        }


        public string ConvertToEasternArabicNumerals(string input)
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

        void printDocumentArabicA4_PrintPage(object sender, PrintPageEventArgs e)
        {
            //Thread.CurrentThread.CurrentUICulture=new System.Globalization.CultureInfo("ar-EG");
            //InitializeComponent();


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
                            //   using (Bitmap newBitmap = new Bitmap(bitmap))
                            // {
                            bitmap.SetResolution(300, 300);

                            int a = Convert.ToInt32(xpos);
                            e.Graphics.DrawImage(bitmap, new Rectangle(410 - 150, 50, 300, 70));

                            //     FileInfo fileInfo = new System.IO.FileInfo(logo);
                            //  if (fileInfo.IsReadOnly)
                            //  {
                            //  fileInfo.IsReadOnly = false;
                            // }
                            //       newBitmap.Save(Path.GetTempPath()+"\\ip.jpeg", ImageFormat.Jpeg);



                            // }
                        }
                    }
                    else
                    {

                        e.Graphics.DrawImage(img, new Rectangle(Convert.ToInt32(xpos) + 385 - 150, 315, 500, 70));
                    }
                }

            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }




            try
            {
                string item = Application.StartupPath + "\\Items2.jpg";
                System.Drawing.Image imgs = System.Drawing.Image.FromFile(item);

                Point loc = new Point(Convert.ToInt32(xpos), 50);
                e.Graphics.DrawImage(imgs, new Rectangle(50, 275, 750, 20));
            }




            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }





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

                //e.Graphics.DrawString(Website, Headerfont2, new SolidBrush(tabDataForeColor), headerstartposition, starty + offset);
                //offset = offset + 10;


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









                //  int number = Convert.ToInt32(txtcashrcvd.Text);
                string headtext = "Item".PadRight(115) + "Qty".PadRight(20) + "Unit Price".PadRight(30) + "Total";

                //   string k = "..";

                //string headtext2 = "العنصر".PadRight(115) + "كمية".PadRight(number) + "Unit price سعر الوحدة".PadRight(30) + "الإجمالي الكلي";




                // string headtext2 = "M"+"العنصر k" + "كمية".PadRight(20) + "سعر الوحدة".PadRight(30) + "الإجمالي الكلي";

                //   string headtext2 = "الإجماليالكلي".PadRight(30) + "سعرالوحدة".PadRight(20) + "كمية".PadRight(115) + "العنصر";


                e.Graphics.DrawString(headtext, Headerfont2, new SolidBrush(Color.Black), startx, starty + offset + 70);
                //  e.Graphics.DrawString("الإجمالي الكلي".PadRight(30) + "سعر الوحدة".PadRight(20) + "كمية".PadRight(115) + "العنصر", Headerfont2, new SolidBrush(Color.Black), 230, starty + offset + 30, format);

                //    e.Graphics.DrawString(headtext2, Headerfont2, new SolidBrush(Color.Black), startx, starty + offset + 30);














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
                        e.Graphics.DrawString(arbname, font, new SolidBrush(Color.Black), startx + 380, starty + offset, format);

                        e.Graphics.DrawString(qty, font, new SolidBrush(Color.Black), startx + 430, starty + offset);
                        e.Graphics.DrawString(rate, font, new SolidBrush(Color.Black), startx + 525, starty + offset);
                        e.Graphics.DrawString(price, font, new SolidBrush(Color.Black), startx + 630, starty + offset);
                        offset = offset + (int)fontheight + 20;
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
        //reversing string
        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
        

        void printDocumentA4_PrintPage(object sender, PrintPageEventArgs e)
        {
            float xpos;
            int startx = 50;
            int starty = 30;
            int offset = 15;
            int headerstartposition = 150;

            int w = e.MarginBounds.Width / 2;
            int x = e.MarginBounds.Left;
            int y = e.MarginBounds.Top;
            Font Headerfont1 = new Font("Times New Roman", 15, FontStyle.Bold);
            Font Headerfont2 = new Font("Times New Roman", 10, FontStyle.Bold);
            Font printFont = new Font("Times New Roman", 10);
            var tabDataForeColor = Color.Black;
            int height = 100 + y;


            var txtDataWidth = e.Graphics.MeasureString(CompanyName, Headerfont1).Width;

            try
            {
                if (logo != null || logo != "")
                {

                    System.Drawing.Image img = System.Drawing.Image.FromFile(logo);

                    Point loc = new Point(20, 50);
                    e.Graphics.DrawImage(img, new Rectangle(50, 50, 50, 50));
                }

            }
            catch
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


                e.Graphics.DrawString(CompanyName, Headerfont1, new SolidBrush(tabDataForeColor), headerstartposition, starty);
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
                e.Graphics.DrawString("Retail Invoice", Headerfont2, new SolidBrush(tabDataForeColor), xpos+400, starty + offset - 24, sf);
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


                e.Graphics.DrawLine(blackPen, 410, 219, 410, 900);
                e.Graphics.DrawLine(blackPen, 475, 219, 475, 900);
                e.Graphics.DrawLine(blackPen, 540, 219, 540, 900);
                e.Graphics.DrawLine(blackPen, 650, 219, 650, 900);
                e.Graphics.DrawLine(blackPen, 760, 219, 760, 900);

                e.Graphics.DrawLine(blackPen, 45, 900, 760, 900);


                string headtext = "Item".PadRight(100) + "Tax%".PadRight(15) + "Qty".PadRight(20) + "Rate".PadRight(30) + "Total";
                e.Graphics.DrawString(headtext, Headerfont2, new SolidBrush(Color.Black), startx, starty + offset - 1);

                offset = offset + 40;
                Font font = new Font("Times New Roman", 10);
                float fontheight = font.GetHeight();
                try
                {
                    foreach (DataGridViewRow row in dgItems.Rows)
                    {


                        string name = row.Cells[1].Value.ToString().Length <= 60 ? row.Cells[1].Value.ToString() : row.Cells[1].Value.ToString().Substring(0, 60); 
                        string tax = row.Cells[7].Value.ToString();
                        string qty = row.Cells[5].Value.ToString();
                        string rate = row.Cells[6].Value.ToString();
                        string price = row.Cells[11].Value.ToString();
                        string productline = name + tax + qty + rate + price;
                        e.Graphics.DrawString(name, font, new SolidBrush(Color.Black), startx, starty + offset);

                        e.Graphics.DrawString(tax, font, new SolidBrush(Color.Black), startx + 380, starty + offset);
                        e.Graphics.DrawString(qty, font, new SolidBrush(Color.Black), startx + 440, starty + offset);
                        e.Graphics.DrawString(rate, font, new SolidBrush(Color.Black), startx + 525, starty + offset);
                        e.Graphics.DrawString(price, font, new SolidBrush(Color.Black), startx + 630, starty + offset);
                        offset = offset + (int)fontheight + 10;
                    }
                }
                catch
                {

                }
            }

            float newoffset =  900;

            e.Graphics.DrawString(NOTES.Text, Headerfont2, new SolidBrush(Color.Black), startx, starty + newoffset + 3);

            e.Graphics.DrawString("Gross Total", Headerfont2, new SolidBrush(Color.Black), startx + 490, starty + newoffset + 3);
            e.Graphics.DrawString(Spell.SpellAmount.comma(Convert.ToDecimal(TOTAL_AMOUNT.Text)), Headerfont2, new SolidBrush(Color.Black), startx + 610, starty + newoffset + 3);
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
            e.Graphics.DrawString("Tax Amt", Headerfont2, new SolidBrush(Color.Black), startx + 490, starty + newoffset + 3);
            e.Graphics.DrawString(TAX_TOTAL.Text, Headerfont2, new SolidBrush(Color.Black), startx + 610, starty + newoffset + 3);

            newoffset = newoffset + 20;
              e.Graphics.DrawString("Discount", Headerfont2, new SolidBrush(Color.Black), startx + 490, starty + newoffset + 3);
              e.Graphics.DrawString(DISCOUNT.Text, Headerfont2, new SolidBrush(Color.Black), startx + 610, starty + newoffset + 3);

            newoffset = newoffset + 20;

            //e.Graphics.DrawString("Tax Amount", Headerfont2, new SolidBrush(Color.Black), startx + 490, starty + newoffset + 3);
            //e.Graphics.DrawString(TAX_TOTAL.Text, Headerfont2, new SolidBrush(Color.Black), startx + 610, starty + newoffset + 3);
            e.Graphics.DrawString("---------------------------------------", Headerfont2, new SolidBrush(Color.Black), startx + 490, starty + newoffset + 9);
            newoffset = newoffset + 25;
          
            e.Graphics.DrawString("Authorized Signature", Headerfont2, new SolidBrush(Color.Black), startx, starty + newoffset + 3);
            offset = offset + 25;
            e.Graphics.DrawString("Total", Headerfont2, new SolidBrush(Color.Black), startx + 490, starty + newoffset + 3);


            e.Graphics.DrawString(Spell.SpellAmount.comma(Convert.ToDecimal(NET_AMOUNT.Text)), Headerfont2, new SolidBrush(Color.Black), startx + 610, starty + newoffset + 3);

            newoffset = newoffset + 20;






            e.HasMorePages = false;
        }
         void printDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            
            
            float xpos;
            int startx = 10;
            int starty =30;
            int offset = 15;
            if(PrintPage.SelectedIndex==1)
            {

              
            }
            else
            {

                int w = e.MarginBounds.Width / 2;
                int x = e.MarginBounds.Left;
                int y = e.MarginBounds.Top;
                Font printFont = new Font("Courier New",8);
                var tabDataForeColor = Color.Black;
                int height = 100 + y;
            

                var txtDataWidth = e.Graphics.MeasureString(CompanyName, printFont).Width;


                using (var sf = new StringFormat())
                {
                    height +=15;
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Alignment = StringAlignment.Center;

                    xpos = e.MarginBounds.Left + (e.MarginBounds.Width / 2);

                    e.Graphics.DrawString(CompanyName, printFont,new SolidBrush(tabDataForeColor), xpos,starty,sf);
                    e.Graphics.DrawString("Kondotty", printFont, new SolidBrush(tabDataForeColor), xpos, starty + offset, sf);
                    offset = offset+10;
                    e.Graphics.DrawString("CASH Invoice- "+Billno, printFont, new SolidBrush(tabDataForeColor), xpos, starty + offset, sf);
                    offset = offset + 10;

                    e.Graphics.DrawString("-----------------------------------------------", printFont, new SolidBrush(Color.Black), xpos, starty + offset, sf);
                    e.Graphics.DrawString("-----------------------------------------------", printFont, new SolidBrush(Color.Black), xpos, starty + offset + 3, sf);

                    offset = offset + 10;
                    e.Graphics.DrawString("Tin No:"+TineNo, printFont, new SolidBrush(tabDataForeColor), startx, starty + offset);
                    offset = offset + 12;
                    e.Graphics.DrawString("Customer:" +CUSTOMER_NAME.Text, printFont, new SolidBrush(tabDataForeColor), startx, starty + offset);
                    Font itemhead = new Font("Courier New", 8);
                    offset = offset + 12;
                    e.Graphics.DrawString("-----------------------------------------------", printFont, new SolidBrush(Color.Black), startx, starty + offset);
                    offset = offset + 12;
                    
                    string headtext = "Item".PadRight(20) + "Tax%".PadRight(5) + "Qty".PadRight(5) + "Rate".PadRight(10) + "Total";
                    e.Graphics.DrawString(headtext, itemhead, new SolidBrush(Color.Black), startx, starty + offset-1 );
                    e.Graphics.DrawString("-----------------------------------------------", printFont, new SolidBrush(Color.Black), startx, starty + offset+7);
                    offset = offset + 15;
                    Font font = new Font("Courier New", 8);
                    float fontheight = font.GetHeight();
                    try
                    {
                        foreach (DataGridViewRow row in dgItems.Rows)
                        {
                            int nameLength = row.Cells[1].Value.ToString().Length;
                            string name = nameLength <= 18 ? row.Cells[1].Value.ToString().PadRight(20) : (row.Cells[1].Value.ToString().Substring(0, 18)).PadRight(20); 
                       //     string name = row.Cells[1].Value.ToString().PadRight(20);
                            string tax = row.Cells[7].Value.ToString().PadRight(5);
                            string qty = row.Cells[5].Value.ToString().PadRight(5);
                            string rate = row.Cells[6].Value.ToString().PadRight(10);
                            string price = row.Cells[11].Value.ToString();
                            string productline = name + tax+qty+rate+ price;
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
                    string total="Total:".PadRight(5)+Spell.SpellAmount.comma(Convert.ToDecimal(NET_AMOUNT.Text));

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
                    if (Payment_cash_amntrecvd.Text != "")
                    {
                        try
                        {
                           // decimal balance = Convert.ToDecimal(Payment_cash_amntrecvd.Text) - Convert.ToDecimal(TOTAL_AMOUNT.Text);
                            e.Graphics.DrawString("Cash Rcvd:" +Spell.SpellAmount.comma(Convert.ToDecimal( Payment_cash_amntrecvd.Text)) + "   " + "Balance:" +Spell.SpellAmount.comma(Convert.ToDecimal(Payment_cash_balance.Text)).ToString(), font, new SolidBrush(Color.Black), startx, starty + offset);

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

         private void addTaxGAmt(object sender, EventArgs e)
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
             GROSS_TOTAL.Text = total.ToString();
         }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            try
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

            }
            catch
            {
                this.Close();
            }


        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
        btnClear.PerformClick();
            SalesHelp h = new SalesHelp(1, "");
            h.ShowDialog();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            secondsave = false;
            editsales = false;
            creditneeded = false;
            ID = "";
            DOC_NO.Text = "";
            CUSTOMER_CODE.Text = "";
            CUSTOMER_NAME.Text = "";
          //  SALESMAN_CODE.Text = "";
        //    SALESMAN_NAME.Text = "";
            DOC_DATE_GRE.Value = DateTime.Today;
            DOC_DATE_HIJ.Text = "";
            DOC_REFERENCE.Text = "";
            CURRENCY_CODE.Text = "";
        
            dgItems.Rows.Clear();
            PAY_CODE.Text = "";
            PAY_NAME.Text = "";
            CARD_NO.Text = "";
            NOTES.Text = "";
            TAX_TOTAL.Text = "0.00";
            VAT.Text = "0.00";
            TOTAL_AMOUNT.Text = "0.00";
            DISCOUNT.Text = "0.00";
            NET_AMOUNT.Text = "0.00";
            Payment_Card_Amont.Text = "0";
            Payment_cash_amntrecvd.Text = "0";
            Payment_cash_balance.Text = "0";
            Payment_Credit_amount.Text = "0";
            Payment_CreditNote_Amount.Text = "0";
            Payment_Creditnote_no.Text = "";
            Payment_Check_Amount.Text = "0";
            Payment_Check_Bankname.Text = "0";
            Payment_Check_No.Text = "";
            Doc_NO = "";
            lbltotalamtrecived.Text = "0";
            lbltotal.Text = "0";
            lblDiscount.Text = "0";
            PaymentPanel.Visible = false;
            ActiveControl = BARCODE;
            TxtRoundOffAmt.Text = "0.00";
            type = "SAL.CSS";    
            clearItem();
            chkCustomeleveldiscount.Enabled = false;
            ActiveForm = false;
        }



        private void clearItem()
        {

            ShowStock = false;
            lblstock.Text = "";
            ITEM_CODE.Text = "";
            ITEM_NAME.Text = "";
            if (hasBatch)
            {
                BATCH.Text = "";
                EXPIRY_DATE.Value = DateTime.Today;
            }
            unitsTable.Rows.Clear();
            QUANTITY.Text = "";
            PRICE.Text = "0.00";
            if (hasTax)
            {
                ITEM_TAX_PER.Text = "0.00";
                ITEM_TAX.Text = "0.00";
            }
            else
            {
                ITEM_TAX_PER.Text = "0.00";
                ITEM_TAX.Text = "0.00";
            }
            GROSS_TOTAL.Text = "0.00";
            ITEM_DISCOUNT.Text = "0.00";
            ITEM_TOTAL.Text = "0.00";
            selectedRow = -1;

            if (SalebyItemName)
            {
                ActiveControl = ITEM_NAME;
            }
            else if (SalebyBarcode)
            {
                ActiveControl = BARCODE;
            }
            else
            {
                ActiveControl = ITEM_CODE;
            }

            EditActive = false;
            HasSeasonDiscount = false;

            PurchasePrice = 0;
            Profit = 0;
            lblProfit.Text = "0.00";
        }

        private void ITEM_CODE_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (sender is KryptonTextBox)
                {
                    string name = (sender as KryptonTextBox).Name;
                    switch (name)
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
                        case "QUANTITY":
                            PRICE.Focus();
                            break;

                        case "PRICE":
                            if (hasTax)
                            {
                                ITEM_TAX_PER.Focus();
                            }
                            break;

                        case "ITEM_TAX_PER":
                            ITEM_TAX.Focus();
                            break;

                        case "ITEM_TAX":
                            GROSS_TOTAL.Focus();
                            break;

                        case "GROSS_TOTAL":
                            ITEM_DISCOUNT.Focus();
                            break;

                        case "ITEM_DISCOUNT":
                            ITEM_TOTAL.Focus();
                            break;
                        case "ITEM_TOTAL":
                            addItem();
                            clearItem();
                            ITEM_CODE.Focus();
                            break;
                        default:
                            break;
                    }
                }
                else if (sender is DateTimePicker)
                {
                    UOM.Focus();
                }
                else if (sender is KryptonComboBox)
                {
                    QUANTITY.Focus();
                }
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
                            if (hasBatch)
                            {
                                BATCH.Focus();
                            }
                           
                            else if (MoveToUnit)
                            {
                                UOM.Focus();
                            }
                            else if (MoveToQty)
                            {
                                QUANTITY.Focus();
                            }
                            else if (MoveToPrice)
                            {
                                PRICE.Focus();
                            }
                            else if (MoveToTaxper)
                            {
                                ITEM_TAX_PER.Focus();
                            }
                            else if (MoveToDisc)
                            {
                                ITEM_DISCOUNT.Focus();
                            }
                            else
                            {
                                ITEM_TOTAL.Focus();
                            }
                            break;
                        case "BATCH":
                            EXPIRY_DATE.Focus();
                            break;
                        case "UOM":
                           
                           if (MoveToQty)
                            {
                                QUANTITY.Focus();
                            }
                            else if (MoveToPrice)
                            {
                                PRICE.Focus();
                            }
                            else if (MoveToTaxper)
                            {
                                ITEM_TAX_PER.Focus();
                            }
                            else if (MoveToDisc)
                            {
                                ITEM_DISCOUNT.Focus();
                            }
                            else
                            {
                                ITEM_TOTAL.Focus();
                            }
                            break;
                        case "QUANTITY":
                            //PRICE.Focus();
                            if (MoveToPrice)
                            {
                                PRICE.Focus();
                            }
                            else if (MoveToTaxper)
                            {
                                ITEM_TAX.Focus();
                            }
                               
                            else if (MoveToDisc)
                            {
                                ITEM_DISCOUNT.Focus();
                            }

                            else
                            {
                                if (EditActive)
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
                                else
                                {
                                    string flag1 = "0";
                                    for (int i = 0; i < dgItems.Rows.Count; i++)
                                    {
                                        DataGridViewCellCollection c = dgItems.Rows[i].Cells;
                                        if (Convert.ToString(c[0].Value) == ITEM_CODE.Text && Convert.ToString(c[4].Value) == UOM.Text)
                                        {
                                            if (MessageBox.Show("Item Already Exists! Do you Want to add to Existing Item???", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                            {

                                                selectedRow = i;
                                                QUANTITY.Text = Convert.ToString(Convert.ToInt16(c["cQty"].Value) + Convert.ToInt16(QUANTITY.Text));

                                                //dgItems.Rows.RemoveAt(i);
                                                // dataGridItem.Visible = false;
                                                //QTY_RCVD.Focus();
                                                addItem();
                                                clearItem();
                                                if (SalebyItemCode)
                                                    ITEM_CODE.Focus();
                                                else if (SalebyBarcode)
                                                    BARCODE.Focus();
                                                else
                                                    ITEM_NAME.Focus();
                                                flag1 = "1";
                                                break;

                                            }
                                            else
                                                break;


                                        }


                                    }
                                    if (flag1 == "0")
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
                                }
                               
                            }
                            break;

                        case "PRICE":
                            //if (hasTax)
                            //{
                            //    ITEM_TAX_PER.Focus();
                            //}
                            if (MoveToTaxper)
                            {
                                ITEM_TAX_PER.Focus();
                            }
                            else if (MoveToDisc)
                            {
                                ITEM_DISCOUNT.Focus();
                            }
                            else
                            {


                                if (EditActive)
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
                                else
                                {
                                    string flag2 = "0";
                                    for (int i = 0; i < dgItems.Rows.Count; i++)
                                    {
                                        DataGridViewCellCollection c = dgItems.Rows[i].Cells;
                                        if (Convert.ToString(c[0].Value) == ITEM_CODE.Text && Convert.ToString(c[4].Value) == UOM.Text)
                                        {
                                            if (MessageBox.Show("Item Already Exists! Do you Want to add to Existing Item???", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                            {

                                                selectedRow = i;
                                                QUANTITY.Text = Convert.ToString(Convert.ToInt16(c["cQty"].Value) + Convert.ToInt16(QUANTITY.Text));

                                                //dgItems.Rows.RemoveAt(i);
                                                // dataGridItem.Visible = false;
                                                //QTY_RCVD.Focus();
                                                addItem();
                                                clearItem();
                                                if (SalebyItemCode)
                                                    ITEM_CODE.Focus();
                                                else if (SalebyBarcode)
                                                    BARCODE.Focus();
                                                else
                                                    ITEM_NAME.Focus();
                                                flag2 = "1";
                                                break;

                                            }
                                            else
                                                break;


                                        }


                                    }
                                    if (flag2 == "0")
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
                                }
                            }
                            break;

                        case "ITEM_TAX_PER":
                            if (MoveToDisc)
                            {
                                ITEM_DISCOUNT.Focus();
                            }
                            else
                            {
                                ITEM_TOTAL.Focus();
                            }
                      //      ITEM_TAX.Focus();
                            break;

                        case "ITEM_TAX":
                            if (MoveToDisc)
                            {
                                ITEM_DISCOUNT.Focus();
                            }
                            else
                            {
                                GROSS_TOTAL.Focus();  
                            }
                          
                            break;

                        case "GROSS_TOTAL":
                            if (MoveToDisc)
                            {
                                ITEM_DISCOUNT.Focus();
                            }
                            break;

                        case "ITEM_DISCOUNT":
                            ITEM_TOTAL.Focus();
                            break;
                        case "ITEM_TOTAL":
                            if (EditActive)
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
                                            QUANTITY.Text = Convert.ToString(Convert.ToInt16(c["cQty"].Value) + Convert.ToInt16(QUANTITY.Text));

                                            //dgItems.Rows.RemoveAt(i);
                                            // dataGridItem.Visible = false;
                                            //QTY_RCVD.Focus();
                                            addItem();
                                            clearItem();
                                            if (SalebyItemCode)
                                                ITEM_CODE.Focus();
                                            else if (SalebyBarcode)
                                                BARCODE.Focus();
                                            else
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
                                    clearItem();
                                    if (SalebyItemCode)
                                        ITEM_CODE.Focus();
                                    else if (SalebyBarcode)
                                        BARCODE.Focus();
                                    else
                                        ITEM_NAME.Focus();
                                }
                            }
                          //  addItem();
                           // clearItem();
                        
                           // ITEM_NAME.Focus();
                            break;
                        default:
                            break;
                    }
                }
                else if (sender is DateTimePicker)
                {
                    UOM.Focus();
                }
                else if (sender is KryptonComboBox)
                {
                    QUANTITY.Focus();
                }
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
                    
                      case "QUANTITY" :

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

        public void GetDocId()
        {
            try
            {
                cmd.Connection = conn;
                cmd.CommandText = "SELECT MAX(DOC_ID) AS Expr1 FROM INV_SALES_HDR";
                cmd.CommandType = CommandType.Text;
                DataTable dt = new DataTable();
                adapter.SelectCommand = cmd;
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    Billno = (Convert.ToInt32(dt.Rows[0][0]) + 1).ToString();
                    lbllastinvoice.Text = Billno;
                }

            }
            catch
            {
            }
        }

        private void New_Sales_Load(object sender, EventArgs e)
        {
        
            DOC_DATE_GRE.Text = ComSet.GettDate();

            bindledgers();
            bindgridview();
            DiscType = "Percentage";
                GetCompanyDetails();
          
            BindCurrency();
            GetBranchDetails(); 
          //  PrintPage.SelectedIndex = 0;
            if (!hasBatch)
            {
                panBatch.Visible = false;
                cBatch.Visible = false;
                cExpDate.Visible = false;
            }

            if (!hasTax)
            {
                panTax.Visible = false;
                cTaxPer.Visible = false;
                cTaxAmt.Visible = false;
            }

            panBarcode.Visible = hasBarcode;

            if (SalebyItemName)
            {
                ActiveControl = ITEM_NAME;
            }
            else if (SalebyBarcode)
            {
                ActiveControl = BARCODE;
            }
            else
            {
                ActiveControl = ITEM_CODE;
            }
            if (!hasArabic)
            {
                DOC_DATE_HIJ.Enabled = false;
            }
            if (HasAccounts)
            {
                pnlacct.Visible = true;
            }

            if (FocusDate)
            {
                ActiveControl = DOC_DATE_GRE;
                DOC_DATE_GRE.Focus();
            }

            pnlcustomerwisediscount.Visible = HasCustomerDiscount;
            PrintInvoice.Checked = PrintInvoices;

            getsalesman();
            SALESACC.SelectedValue = 27;
          //  getsalesman();
        }

        private void btnItemCode_Click(object sender, EventArgs e)
        {
            ItemMasterHelp h = new ItemMasterHelp(0);
            if (h.ShowDialog() == DialogResult.OK && h.c != null)
            {
                ITEM_CODE.Text = Convert.ToString(h.c[0].Value);
                ITEM_NAME.Text = Convert.ToString(h.c[1].Value);
                TaxId = Convert.ToInt32(h.c[29].Value);
                PRICE.Text = Convert.ToString(h.c[8].Value);

                if (hasBatch)
                {
                    BATCH.Focus();
                }

                addUnits();
                GetTaxRate();
            }
        }

        private void calculateGrossAmount(object sender, EventArgs e)
        {
            if (QUANTITY.Text.Trim() != "" && PRICE.Text.Trim() != "")
            {
                GROSS_TOTAL.Text = (Convert.ToDouble(QUANTITY.Text) * Convert.ToDouble(PRICE.Text)).ToString();
            }
            
            gettax();

           
        }
        //getting tax amt of a product
        private void gettax()
        {
            double total = 0;
            double tax = 0;
            if (QUANTITY.Text.Trim() != "" && PRICE.Text.Trim() != "")
            {
                total = Convert.ToDouble(QUANTITY.Text) * Convert.ToDouble(PRICE.Text);
            }
            if (ITEM_TAX_PER.Text.Trim() != "" && GROSS_TOTAL.Text.Trim() != "")
            {
                tax = total * (Convert.ToDouble(ITEM_TAX_PER.Text) / 100);
            }
            ITEM_TAX.Text = tax.ToString();
        }

        string DiscAmt;
        private void ITEM_DISCOUNT_TextChanged(object sender, EventArgs e)
        {

            if (ITEM_DISCOUNT.Text.Trim() != "" && GROSS_TOTAL.Text.Trim() != "")
            {
                if (DiscType == "Percentage")
                {
                    ITEM_TOTAL.Text = (Convert.ToDouble(GROSS_TOTAL.Text) - (Convert.ToDouble(GROSS_TOTAL.Text) * (Convert.ToDouble(ITEM_DISCOUNT.Text) / 100))).ToString();
                    DiscAmt = (Convert.ToDouble(GROSS_TOTAL.Text) * (Convert.ToDouble(ITEM_DISCOUNT.Text) / 100)).ToString();
                }
                else
                {
                    ITEM_TOTAL.Text = (Convert.ToDouble(GROSS_TOTAL.Text) -Convert.ToDouble(ITEM_DISCOUNT.Text)).ToString();
                    DiscAmt = DiscValue;
                }
            }

            if (ITEM_DISCOUNT.Text == "")
            {
                ITEM_TOTAL.Text = GROSS_TOTAL.Text;
            }
        }

        private void BtnPanelClose_Click(object sender, EventArgs e)
        {
           PaymentPanel.Visible = false;
        }

        private void btnFianlSave_Click(object sender, EventArgs e)
        {
            btnSave.PerformClick();
        }

        private void tabControl2_DrawItem(object sender, DrawItemEventArgs e)
        {

            Graphics g = e.Graphics;
            Brush _textBrush;

            // Get the item from the collection.
            TabPage _tabPage = tabControl1.TabPages[e.Index];

            // Get the real bounds for the tab rectangle.
            Rectangle _tabBounds = tabControl1.GetTabRect(e.Index);

            if (e.State == DrawItemState.Selected)
            {

                // Draw a different background color, and don't paint a focus rectangle.
                _textBrush = new SolidBrush(Color.Red);
                g.FillRectangle(Brushes.Gray, e.Bounds);
            }
            else
            {
                _textBrush = new System.Drawing.SolidBrush(e.ForeColor);
                e.DrawBackground();
            }

            // Use our own font.
            Font _tabFont = new Font("Arial", (float)10.0, FontStyle.Bold, GraphicsUnit.Pixel);

            // Draw string. Center the text.
            StringFormat _stringFlags = new StringFormat();
            _stringFlags.Alignment = StringAlignment.Center;
            _stringFlags.LineAlignment = StringAlignment.Center;
            g.DrawString(_tabPage.Text, _tabFont, _textBrush, _tabBounds, new StringFormat(_stringFlags));

        }

        decimal TotalRecievedAmnt;
        public void CalculateAmount()
        {
            try
            {
                TotalRecievedAmnt = Convert.ToDecimal(Payment_cash_amntrecvd.Text) + Convert.ToDecimal(Payment_Card_Amont.Text) + Convert.ToDecimal(Payment_Check_Amount.Text) + Convert.ToDecimal(Payment_CreditNote_Amount.Text);
                lbltotalamtrecived.Text =Spell.SpellAmount.comma(TotalRecievedAmnt);
            }
            catch
            {
                TotalRecievedAmnt = 0;
                lbltotalamtrecived.Text = "0";
            }
        }


        private void Payment_cash_amntrecvd_TextChanged(object sender, EventArgs e)
        {
            lbltotalamtrecived.Text = "0.00";
            CalculateAmount();
            try
            {
                if (NET_AMOUNT.Text != "")
                {
                    Payment_cash_balance.Text = (Convert.ToDecimal(Payment_cash_amntrecvd.Text) - (Convert.ToDecimal(NET_AMOUNT.Text))).ToString();
                  //  Payment_Credit_amount.Text = (Convert.ToDecimal(Payment_cash_amntrecvd.Text) - (Convert.ToDecimal(NET_AMOUNT.Text))).ToString();
                    balance = Convert.ToDecimal(Payment_cash_balance.Text);
                }
            }
            catch
            {
             //   Payment_cash_amntrecvd.Text = "0";
             //   Payment_cash_balance.Text = (Convert.ToDecimal(Payment_cash_amntrecvd.Text) -( Convert.ToDecimal(NET_AMOUNT.Text))).ToString();
             //balance=Convert.ToDecimal(Payment_cash_balance.Text);
            }
        }

        private void Payment_Creditnote_no_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData==(Keys.Enter)||e.KeyData==(Keys.Tab))
            {
                try
                {
                    GetCreditNote();
                }
                catch
                {
                }
            }
        }
        string Doc_NO="0";
        private void PaymentBtnFinalSave_Click(object sender, EventArgs e)
        {
            TransDate = DOC_DATE_GRE.Value;
            if (CUSTOMER_CODE.Text != "")
            {
                type = "SAL.CRD";
                creditneeded = true;
            }
            else
            {
                type = "SAL.CSS";
                creditneeded = false;
            }
            //else if (Math.Round(Math.Abs(Convert.ToDecimal(Payment_cash_balance.Text))) > Convert.ToDecimal(TxtRoundOffAmt.Text))
            //{


            //}

            if (valid())
            {
                
                string status = "Added!";
                if (ID == "")
                {
                    GetDocId();
                    DOC_NO.Text = General.generateSalesID();
                    Doc_NO = DOC_NO.Text;

                    cmd.CommandText = "INSERT INTO INV_SALES_HDR(DOC_NO,DOC_TYPE,DOC_DATE_GRE,DOC_DATE_HIJ,CURRENCY_CODE,DOC_REFERENCE,CUSTOMER_CODE,CUSTOMER_NAME_ENG,SALESMAN_CODE,NOTES,TAX_TOTAL,VAT,DISCOUNT,TOTAL_AMOUNT,PAY_CODE,CARD_NO,FREIGHT,ROUNDOFF,CESS) ";
                    cmd.CommandText += "VALUES('" + DOC_NO.Text + "','" + type + "','" + DOC_DATE_GRE.Value.ToString("MM/dd/yyyy") + "','" + DOC_DATE_HIJ.Text + "','" + CURRENCY_CODE.Text + "','" + DOC_REFERENCE.Text + "','" + CUSTOMER_CODE.Text + "','" + CUSTOMER_NAME.Text + "','" + SalesManCode + "','" + NOTES.Text + "','" + TAX_TOTAL.Text + "','" + VAT.Text + "','" + DISCOUNT.Text + "','" + TOTAL_AMOUNT.Text + "','" + PAY_CODE.Text + "','" + CARD_NO.Text + "','" + txtFreight.Text + "','" + TxtRoundOffAmt.Text + "','" + TXT_CESS.Text + "');";
                    cmd.CommandText += "INSERT INTO INV_SALES_DTL(DOC_TYPE,DOC_NO,ITEM_CODE,ITEM_DESC_ENG,UOM,PRICE,QUANTITY,ITEM_DISCOUNT,DISC_TYPE,DISC_VALUE";
                    if (hasTax)
                    {
                        cmd.CommandText += ",ITEM_TAX_PER,ITEM_TAX";
                    }
                    if (hasBatch)
                    {
                        cmd.CommandText += ",BATCH,EXPIRY_DATE";
                    }
                    cmd.CommandText += ")";
                    for (int i = 0; i < dgItems.Rows.Count; i++)
                    {
                        DataGridViewCellCollection c = dgItems.Rows[i].Cells;
                        cmd.CommandText += "SELECT '" + type + "','" + DOC_NO.Text + "','" + c["cCode"].Value + "','" + c["cName"].Value + "','" + c["cUnit"].Value + "','" + c["cPrice"].Value + "','" + c["cQty"].Value + "','" + c["cDisc"].Value + "','" + c["DiscTypes"].Value + "','" + c["DiscValues"].Value + "'";
                        if (hasTax)
                        {
                            cmd.CommandText += ",'" + c["cTaxPer"].Value + "','" + c["cTaxAmt"].Value + "'";
                        }
                        if (hasBatch)
                        {
                            cmd.CommandText += ",'" + c["cBatch"].Value + "','" + DateTime.ParseExact(c["cExpDate"].Value.ToString(), "dd/MM/yyyy", null).ToString("MM/dd/yyyy") + "'";
                        }
                        cmd.CommandText += " UNION ALL ";
                    }
                    //inserting details into Tbl_Inv_Payment_Dtls

                  
                }
                else
                {
                    modifiedtransaction();
                    status = "Updated!";
                    cmd.CommandText = "UPDATE INV_SALES_HDR SET DOC_TYPE = '" + type + "',DOC_DATE_GRE = '" + DOC_DATE_GRE.Value.ToString("MM/dd/yyyy") + "',DOC_DATE_HIJ = '" + DOC_DATE_HIJ.Text + "',CURRENCY_CODE = '" + CURRENCY_CODE.Text + "',DOC_REFERENCE = '" + DOC_REFERENCE.Text + "',CUSTOMER_CODE = '" + CUSTOMER_CODE.Text + "',CUSTOMER_NAME_ENG = '" + CUSTOMER_NAME.Text + "',SALESMAN_CODE = '" + SalesManCode + "',NOTES = '" + NOTES.Text + "',TAX_TOTAL = '" + TAX_TOTAL.Text + "',VAT = '" + VAT.Text + "',DISCOUNT = '" + DISCOUNT.Text + "',TOTAL_AMOUNT = '" + TOTAL_AMOUNT.Text + "',PAY_CODE = '" + PAY_CODE.Text + "',FREIGHT = '" + txtFreight.Text + "',CARD_NO = '" + CARD_NO.Text + "',ROUNDOFF = '" + TxtRoundOffAmt.Text + "',CESS = '" + TXT_CESS.Text + "'  WHERE DOC_NO = '" + DOC_NO.Text + "';";
                    cmd.CommandText += "DELETE FROM INV_SALES_DTL WHERE DOC_NO = '" + DOC_NO.Text + "'";
                    cmd.CommandText += "INSERT INTO INV_SALES_DTL(DOC_NO,ITEM_CODE,ITEM_DESC_ENG,UOM,PRICE,QUANTITY,ITEM_DISCOUNT,DISC_TYPE,DISC_VALUE";
                    if (hasTax)
                    {
                        cmd.CommandText += ",ITEM_TAX_PER,ITEM_TAX";
                    }
                    if (hasBatch)
                    {
                        cmd.CommandText += ",BATCH,EXPIRY_DATE";
                    }
                    cmd.CommandText += ")";
                    for (int i = 0; i < dgItems.Rows.Count; i++)
                    {
                        DataGridViewCellCollection c = dgItems.Rows[i].Cells;
                        cmd.CommandText += "SELECT '" + DOC_NO.Text + "','" + c["cCode"].Value + "','" + c["cName"].Value + "','" + c["cUnit"].Value + "','" + c["cPrice"].Value + "','" + c["cQty"].Value + "','" + c["cDisc"].Value + "','" + c["DiscTypes"].Value + "','" + c["DiscValues"].Value + "'";
                        if (hasTax)
                        {
                            cmd.CommandText += ",'" + c["cTaxPer"].Value + "','" + c["cTaxAmt"].Value + "'";
                        }
                        if (hasBatch)
                        {
                            cmd.CommandText += ",'" + c["cBatch"].Value + "','" + DateTime.ParseExact(c["cExpDate"].Value.ToString(), "dd/MM/yyyy", null) + "'";
                        }
                        cmd.CommandText += " UNION ALL ";
                    }


                    DeleteTransation();
                    //if (type == "SAL.CREDITNOTE")
                    //{
                    //    InsertIntoCreditTable();
                    //}

                }

                cmd.CommandText = cmd.CommandText.Substring(0, cmd.CommandText.Length - 10);
                try
                {
                    conn.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();


                    InsertTransaction();
                    FreightTransaction();
                    RoundOFFTransaction();
                    CessTransaction();
                    VATTransaction();
                    DiscountTransaction();

                    try
                    {
                        if (editsales == false)
                        {
                            InsertPaymentDetails();
                            if (Convert.ToDecimal(Payment_cash_balance.Text) <= 0)
                            {
                                //if (creditneeded == true)
                                //{
                                //    if(Convert.ToDecimal(Payment_cash_amntrecvd.Text)!=0)
                                //    {
                                //    type = "SAL.CRD";
                                //    cmd.CommandText = "INSERT INTO INV_SALES_HDR(DOC_NO,DOC_TYPE,DOC_DATE_GRE,DOC_DATE_HIJ,CURRENCY_CODE,DOC_REFERENCE,CUSTOMER_CODE,CUSTOMER_NAME_ENG,SALESMAN_CODE,NOTES,TAX_TOTAL,VAT,DISCOUNT,TOTAL_AMOUNT,PAY_CODE,CARD_NO) ";
                                //    cmd.CommandText += "VALUES('" + Doc_NO + "','" + type + "','" + DOC_DATE_GRE.Value.ToString("MM/dd/yyyy") + "','" + DOC_DATE_HIJ.Text + "','" + CURRENCY_CODE.Text + "','" + DOC_REFERENCE.Text + "','" + CUSTOMER_CODE.Text + "','" + CUSTOMER_NAME.Text + "','" + SalesManCode + "','" + NOTES.Text + "','" + TAX_TOTAL.Text + "','" + VAT.Text + "','" + 0 + "','" + Payment_Credit_amount.Text + "','" + PAY_CODE.Text + "','" + CARD_NO.Text + "');";
                                //    cmd.CommandType = CommandType.Text;
                                //    cmd.ExecuteNonQuery();
                                //    }
                                //}
                                //else
                                //{

                                //}
                            }

                            //inserting details into credit table 
                            if (type == "SAL.CREDITNOTE")
                            {
                                InsertIntoCreditTable();
                            }
                        }
                        else
                        {
                            UpdatePaymentDetails();

                        }
                    }
                    catch
                    {

                    }

                    if (type == "SAL.CREDITNOTE")
                    {
                        MessageBox.Show("Credit Note " + status);
                    }
                    else
                    {
                        //   MessageBox.Show("Sales " + status);
                        if (PrintInvoice.Checked == true)
                        {
                            PrintingInitial();
                        }
                    }




                    ReceiptVoucher();


                   
                    btnClear.PerformClick();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public void ReceiptVoucher()
        {
            if (ID == "")
            {
                if (type =="SAL.CRD"&& Convert.ToDecimal(Payment_cash_amntrecvd.Text)>0)
                {
               string VoucherNo= General.generateReceiptVoucherCode();
                cmd.CommandText = "INSERT INTO REC_RECEIPTVOUCHER_HDR (DOC_NO,DOC_DATE_GRE,DOC_DATE_HIJ,CUST_CODE,CUR_CODE,AMOUNT,PAY_CODE,BANK_CODE,CHQ_NO,CHQ_DATE,NOTES,TOTAL_PAID,TOTAL_CURRENT,TOTAL_BALANCE) VALUES('" + VoucherNo + "','" + DOC_DATE_GRE.Value.ToString("MM/dd/yyyy") + "','" + DOC_DATE_HIJ.Text + "','" + CUSTOMER_CODE.Text + "','" + CURRENCY_CODE.Text + "','" + Payment_cash_amntrecvd.Text + "','" + PAY_CODE.Text + "','" + Payment_Check_Bankname.Text + "','" + Payment_Check_No.Text + "','" + dateTimePicker1.Value.ToString("MM/dd/yyyy") + "','" + NOTES.Text + "','" + Payment_cash_amntrecvd.Text + "','" + NET_AMOUNT.Text + "','" + NET_AMOUNT.Text + "')";
                try {
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                 }
            catch { }
                receiptVoucherTransaction();
                }
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
            modtrans.insertTransaction();
        }


        public void RoundOFFTransaction()
        {
            try
            {

                if (Convert.ToDouble(TxtRoundOffAmt.Text) > 0)
                {


                    trans.VOUCHERTYPE = "SALES Normal";
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

                    trans.DEBIT = TxtRoundOffAmt.Text;
                    trans.SYSTEMTIME = DateTime.Now.ToString();
                    trans.insertTransaction();

                    trans.PARTICULARS = "SL A/C";
                    trans.ACCNAME = "ROUND OFF";
                    trans.VOUCHERNO = DOC_NO.Text;

                    trans.ACCID = "92";


                    trans.DEBIT = "0";
                    trans.CREDIT = TxtRoundOffAmt.Text;
                    trans.SYSTEMTIME = DateTime.Now.ToString();
                    trans.insertTransaction();
                }
            }
            catch
            {
            }
        }
        public void FreightTransaction()
        {
            try
            {
                if (Convert.ToDouble(txtFreight.Text) > 0)
                {

                    trans.VOUCHERTYPE = "SALES Normal";
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

                    trans.DEBIT = txtFreight.Text;
                    trans.SYSTEMTIME = DateTime.Now.ToString();

                    trans.insertTransaction();

                    trans.PARTICULARS = "SL A/C";
                    trans.ACCNAME = "FREIGHT ON SALES";
                    trans.VOUCHERNO = DOC_NO.Text;

                    trans.ACCID = "60";


                    trans.DEBIT = "0";
                    trans.CREDIT = txtFreight.Text;
                    trans.SYSTEMTIME = DateTime.Now.ToString();
                    trans.insertTransaction();
                }
            }
            catch
            {
            }
        }
        public void CessTransaction()
        {
            try
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

                    trans.DEBIT = TXT_CESS.Text;
                    trans.SYSTEMTIME = DateTime.Now.ToString();
                    trans.insertTransaction();

                    trans.PARTICULARS = "SL A/C";
                    trans.ACCNAME = "OUTPUT CESS";
                    trans.VOUCHERNO = DOC_NO.Text;

                    trans.ACCID = "79";


                    trans.DEBIT = "0";
                    trans.CREDIT = TXT_CESS.Text;
                    trans.SYSTEMTIME = DateTime.Now.ToString();
                    trans.insertTransaction();
                }
            }
            catch
            {
            }
        }

        public void receiptVoucherTransaction()
        {
            trans.VOUCHERTYPE = "Cash Receipt";
            trans.DATED = TransDate.ToString("MM/dd/yyyy");
            trans.NARRATION = NOTES.Text;
            Login log = (Login)Application.OpenForms["Login"];
            trans.USERID = log.EmpId;
            trans.ACCNAME = "CASH ACCOUNT";
            trans.PARTICULARS = CASHACC.Text;
            trans.VOUCHERNO = DOC_NO.Text;

            trans.ACCID = "24";
            trans.CREDIT = "0";

            trans.DEBIT = Payment_cash_amntrecvd.Text;
            trans.SYSTEMTIME = DateTime.Now.ToString();
            trans.insertTransaction();
            trans.PARTICULARS = "CASH ACCOUNT";
            trans.ACCNAME = CASHACC.Text;
            trans.VOUCHERNO = DOC_NO.Text;

            trans.ACCID = CASHACC.SelectedValue.ToString();


            trans.DEBIT = "0";
            trans.CREDIT = Payment_cash_amntrecvd.Text;
            trans.SYSTEMTIME = DateTime.Now.ToString();
            trans.insertTransaction();
        }
        public void VATTransaction()
        {
            try
            {

                if (Convert.ToDouble(TAX_TOTAL.Text) > 0)
                {

                    trans.VOUCHERTYPE = "SALES Normal";
                    trans.DATED = TransDate.ToString("MM/dd/yyyy");
                    trans.NARRATION = NOTES.Text;
                    Login log = (Login)Application.OpenForms["Login"];
                    trans.USERID = log.EmpId;

                    trans.NARRATION = NOTES.Text;
                    trans.ACCNAME = "SL A/C";
                    trans.PARTICULARS = "OUTPUT VAT";
                    trans.VOUCHERNO = DOC_NO.Text;

                    trans.ACCID = "101";

                    trans.CREDIT = "0";

                    trans.DEBIT = TAX_TOTAL.Text;
                    trans.SYSTEMTIME = DateTime.Now.ToString();
                    trans.insertTransaction();

                    trans.PARTICULARS = "SL A/C";
                    trans.ACCNAME = "OUTPUT VAT";
                    trans.VOUCHERNO = DOC_NO.Text;

                    trans.ACCID = "83";


                    trans.DEBIT = "0";
                    trans.CREDIT = TAX_TOTAL.Text;
                    trans.SYSTEMTIME = DateTime.Now.ToString();
                    trans.insertTransaction();
                }
            }
            catch
            {
            }
        }

        public void DiscountTransaction()
        {
            try
            {

                if (Convert.ToDouble(DISCOUNT.Text) > 0)
                {


                    trans.VOUCHERTYPE = "SALES Normal";
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

                    trans.DEBIT = DISCOUNT.Text;
                    trans.SYSTEMTIME = DateTime.Now.ToString();
                    trans.insertTransaction();

                    trans.PARTICULARS = "DISCOUNT GIVEN";
                    trans.ACCNAME = "SL A/C";
                    trans.VOUCHERNO = DOC_NO.Text;

                    trans.ACCID = "101";


                    trans.DEBIT = "0";
                    trans.CREDIT = DISCOUNT.Text;
                    trans.SYSTEMTIME = DateTime.Now.ToString();
                    trans.insertTransaction();
                }
            }
            catch
            {
            }
        }

        public void UpdatePaymentDetails()
        {
            try
            {
                PaymDet.Doc_No = Doc_NO;
                PaymDet.AmtRecived = Payment_cash_amntrecvd.Text;
                PaymDet.BalanceAmt = Payment_cash_balance.Text;
                PaymDet.CardNo = CARD_NO.Text;
                PaymDet.CardAmt = Payment_Card_Amont.Text;
                PaymDet.CreditNoteNo = Payment_Creditnote_no.Text;
                PaymDet.CreditNoteAmt = Payment_Credit_amount.Text;
                PaymDet.CheckNo = Payment_Check_No.Text;
                PaymDet.CheckAmt = Payment_Check_Amount.Text;
                PaymDet.CheckBank = Payment_Check_Bankname.Text;
                PaymDet.CheckDate = dateTimePicker1.Value;
                PaymDet.CustomerCode = CUSTOMER_CODE.Text;
                PaymDet.CustomerName = CUSTOMER_NAME.Text;
                PaymDet.TotalAmtRcvd = lbltotalamtrecived.Text;
                if (creditneeded == true)
                {
                    PaymDet.AmountCredited = Payment_Credit_amount.Text;
                }
                else
                {
                    PaymDet.AmountCredited = "0";
                }
                PaymDet.Date = DOC_DATE_GRE.Value;
                PaymDet.TotalAmt = NET_AMOUNT.Text;
               

                PaymDet.UpdatePaymentDetails();


            }
            catch
            {
            }
        }
        private void InsertPaymentDetails()
        {
            try
            {
                PaymDet.Doc_No = Doc_NO;
                PaymDet.AmtRecived = Payment_cash_amntrecvd.Text;
                PaymDet.BalanceAmt = Payment_cash_balance.Text;
                PaymDet.CardNo = CARD_NO.Text;
                PaymDet.CardAmt = Payment_Card_Amont.Text;
                PaymDet.CreditNoteNo = Payment_Creditnote_no.Text;
                PaymDet.CreditNoteAmt = Payment_Credit_amount.Text;
                PaymDet.CheckNo = Payment_Check_No.Text;
                PaymDet.CheckAmt = Payment_Check_Amount.Text;
                PaymDet.CheckBank = Payment_Check_Bankname.Text;
                PaymDet.CheckDate = dateTimePicker1.Value;
                PaymDet.CustomerCode = CUSTOMER_CODE.Text;
                PaymDet.CustomerName = CUSTOMER_NAME.Text;
                PaymDet.TotalAmtRcvd = lbltotalamtrecived.Text;
                if (creditneeded == true)
                {
                    PaymDet.AmountCredited = Payment_Credit_amount.Text;
                }
                else
                {
                    PaymDet.AmountCredited = "0";
                }
                PaymDet.Date = DOC_DATE_GRE.Value;
                PaymDet.TotalAmt = NET_AMOUNT.Text;

                PaymDet.InsertPaymentDetails();
        

            }
            catch
            {
            }
        }
        private void DeleteTransation()
        {
            try
            {
                trans.VOUCHERTYPE = "SALES Normal";
                trans.VOUCHERNO = DOC_NO.Text;
                trans.DeletePurchaseTransaction();
            }
            catch
            {
            }


        }
        public void InsertTransaction()
        {
            try
            {
                trans.VOUCHERTYPE = "SALES Normal";
                trans.DATED = TransDate.ToString("MM/dd/yyyy");
                trans.NARRATION = NOTES.Text;
                Login log = (Login)Application.OpenForms["Login"];
                trans.USERID = log.EmpId;

                trans.NARRATION = NOTES.Text;
                trans.ACCNAME = CASHACC.Text;
                trans.PARTICULARS = SALESACC.Text;
                trans.VOUCHERNO = DOC_NO.Text;

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
            catch
            {
            }
        }

        private void btnclearcustomer_Click(object sender, EventArgs e)
        {
            creditneeded = false;
            Payment_Credit_amount.Text = "";
            CUSTOMER_CODE.Text = "";
            CUSTOMER_NAME.Text = "";
            chkCustomeleveldiscount.Enabled = false;
            chkCustomeleveldiscount.Checked = false;
        }

        private void PaymentTab_KeyDown(object sender, KeyEventArgs e)
        {
            string name = PaymentTab.SelectedTab.Name;
            if (e.KeyData == Keys.Right)
            {
                
               
                  switch (name)
                    {
                        case "Cash":
                            Payment_cash_amntrecvd.Focus();
                            break;
                        case "Card":
                          CARD_NO.Focus();
                            break;
                        case "Credit":
                            btnCust.Focus();
                            break;
                        case "CreditNote":
                            Payment_Creditnote_no.Focus();
                            break;
                        case "Check":
                            Payment_Check_No.Focus();
                            break;
                      default:
                            break;
                  }
                 
            }
            if (e.KeyData == Keys.Left)
            {
                //TabPage tab = new TabPage();
                //tab.Name = name;
                //tab.Focus();
                PaymentTab.Focus();
            }
            if (e.KeyData == (Keys.Alt | Keys.S))
            {
                PaymentBtnFinalSave.PerformClick();
            }
        }


       

        public void InsertionProcessSettings()
        {
            if (Convert.ToDecimal(Payment_cash_amntrecvd.Text) == 0)
            {
                Customerselect = true;
            }
            else if (Convert.ToDecimal(Payment_Credit_amount.Text) < 0)
            {

            }
        }

        private void Payment_Check_Amount_TextChanged(object sender, EventArgs e)
        {
            CalculateAmount();
        }

        private void Payment_Card_Amont_TextChanged(object sender, EventArgs e)
        {
            CalculateAmount();
        }

        private void Payment_CreditNote_Amount_TextChanged(object sender, EventArgs e)
        {
            CalculateAmount();
        }

        private void ITEM_NAME_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (ITEM_NAME.Text == "")
            {
                dataGridItem.Visible = false;
            }
            else
            {
                dataGridItem.Visible = true;
                source.Filter = string.Format("[Item Name] LIKE '%{0}%' ", ITEM_NAME.Text);
                dataGridItem.ClearSelection();
            }
          
            }
            catch
            {
            }
        }



        public void bindgridview()
        {
            try
            {
                cmd.Connection = conn;
                if (ShowPurchase)
                {
                 //   cmd.CommandText = "SELECT     INV_ITEM_DIRECTORY.CODE AS [Item Code], INV_ITEM_DIRECTORY_UNITS.BARCODE AS Barcode, INV_ITEM_DIRECTORY.DESC_ENG AS [Item Name],INV_ITEM_DIRECTORY.DESC_ARB, INV_ITEM_DIRECTORY_UNITS.UNIT_CODE, INV_ITEM_PRICE.PRICE AS SALES, INV_ITEM_DIRECTORY.TaxId,INV_ITEM_PRICE_1.PRICE AS PURCHASE, GEN_TAX_MASTER.TaxRate FROM         INV_ITEM_DIRECTORY LEFT OUTER JOIN  GEN_TAX_MASTER ON INV_ITEM_DIRECTORY.TaxId = GEN_TAX_MASTER.TaxId LEFT OUTER JOIN  INV_ITEM_PRICE LEFT OUTER JOIN   INV_ITEM_DIRECTORY_UNITS ON INV_ITEM_PRICE.UNIT_CODE = INV_ITEM_DIRECTORY_UNITS.UNIT_CODE AND INV_ITEM_PRICE.ITEM_CODE = INV_ITEM_DIRECTORY_UNITS.ITEM_CODE LEFT OUTER JOIN INV_ITEM_PRICE AS INV_ITEM_PRICE_1 ON INV_ITEM_DIRECTORY_UNITS.UNIT_CODE = INV_ITEM_PRICE_1.UNIT_CODE AND INV_ITEM_DIRECTORY_UNITS.ITEM_CODE = INV_ITEM_PRICE_1.ITEM_CODE ON INV_ITEM_DIRECTORY.CODE = INV_ITEM_DIRECTORY_UNITS.ITEM_CODE WHERE     (INV_ITEM_PRICE.SAL_TYPE = '" + RATE_CODE.SelectedValue + "') AND (INV_ITEM_PRICE_1.SAL_TYPE = 'PUR')";
                   // cmd.CommandText = "SELECT        INV_ITEM_DIRECTORY.CODE AS [Item Code], INV_ITEM_DIRECTORY_UNITS.BARCODE AS Barcode, INV_ITEM_DIRECTORY.DESC_ENG AS [Item Name], INV_ITEM_DIRECTORY.DESC_ARB, INV_ITEM_DIRECTORY_UNITS.UNIT_CODE, INV_ITEM_PRICE.PRICE AS SALES, GEN_TAX_MASTER.TaxRate,  ISNULL(INV_ITEM_PRICE_1.PRICE * GEN_TAX_MASTER.TaxRate / 100 + INV_ITEM_PRICE_1.PRICE, 0) AS PURVALUEWITHTAX,INV_ITEM_DIRECTORY.TaxId FROM            INV_ITEM_DIRECTORY LEFT OUTER JOIN  GEN_TAX_MASTER ON INV_ITEM_DIRECTORY.TaxId = GEN_TAX_MASTER.TaxId LEFT OUTER JOIN  INV_ITEM_PRICE LEFT OUTER JOIN INV_ITEM_DIRECTORY_UNITS ON INV_ITEM_PRICE.UNIT_CODE = INV_ITEM_DIRECTORY_UNITS.UNIT_CODE AND  INV_ITEM_PRICE.ITEM_CODE = INV_ITEM_DIRECTORY_UNITS.ITEM_CODE LEFT OUTER JOIN INV_ITEM_PRICE AS INV_ITEM_PRICE_1 ON INV_ITEM_DIRECTORY_UNITS.UNIT_CODE = INV_ITEM_PRICE_1.UNIT_CODE AND INV_ITEM_DIRECTORY_UNITS.ITEM_CODE = INV_ITEM_PRICE_1.ITEM_CODE ON INV_ITEM_DIRECTORY.CODE = INV_ITEM_DIRECTORY_UNITS.ITEM_CODE WHERE        (INV_ITEM_PRICE.SAL_TYPE = '" + RATE_CODE.SelectedValue + "') AND (INV_ITEM_PRICE_1.SAL_TYPE = 'PUR')";
                    cmd.CommandText = "SELECT        INV_ITEM_DIRECTORY.CODE AS [Item Code], INV_ITEM_DIRECTORY_UNITS.BARCODE AS Barcode, INV_ITEM_DIRECTORY.DESC_ENG AS [Item Name],  INV_ITEM_DIRECTORY.DESC_ARB, INV_ITEM_DIRECTORY_UNITS.UNIT_CODE, INV_ITEM_PRICE.PRICE AS SALES, GEN_TAX_MASTER.TaxRate,  ISNULL(INV_ITEM_PRICE_1.PRICE * GEN_TAX_MASTER.TaxRate / 100 + INV_ITEM_PRICE_1.PRICE, 0) AS PURVALUEWITHTAX, INV_ITEM_DIRECTORY.TaxId FROM            INV_ITEM_DIRECTORY LEFT OUTER JOIN  GEN_TAX_MASTER ON INV_ITEM_DIRECTORY.TaxId = GEN_TAX_MASTER.TaxId LEFT OUTER JOIN INV_ITEM_PRICE LEFT OUTER JOIN  INV_ITEM_DIRECTORY_UNITS ON INV_ITEM_PRICE.UNIT_CODE = INV_ITEM_DIRECTORY_UNITS.UNIT_CODE AND  INV_ITEM_PRICE.ITEM_CODE = INV_ITEM_DIRECTORY_UNITS.ITEM_CODE LEFT OUTER JOIN INV_ITEM_PRICE AS INV_ITEM_PRICE_1 ON INV_ITEM_DIRECTORY_UNITS.UNIT_CODE = INV_ITEM_PRICE_1.UNIT_CODE AND INV_ITEM_DIRECTORY_UNITS.ITEM_CODE = INV_ITEM_PRICE_1.ITEM_CODE ON INV_ITEM_DIRECTORY.CODE = INV_ITEM_DIRECTORY_UNITS.ITEM_CODE WHERE        (INV_ITEM_PRICE.SAL_TYPE = '" + RATE_CODE.SelectedValue + "') AND (INV_ITEM_PRICE_1.SAL_TYPE = 'PUR')";
                }
                else
                {
                    cmd.CommandText = "SELECT     INV_ITEM_DIRECTORY.CODE AS [Item Code], INV_ITEM_DIRECTORY_UNITS.BARCODE AS Barcode, INV_ITEM_DIRECTORY.DESC_ENG AS [Item Name],INV_ITEM_DIRECTORY.DESC_ARB, INV_ITEM_DIRECTORY_UNITS.UNIT_CODE, INV_ITEM_PRICE.PRICE AS SALES, INV_ITEM_DIRECTORY.TaxId,  GEN_TAX_MASTER.TaxRate FROM         INV_ITEM_PRICE INNER JOIN  INV_ITEM_DIRECTORY_UNITS ON INV_ITEM_PRICE.UNIT_CODE = INV_ITEM_DIRECTORY_UNITS.UNIT_CODE AND INV_ITEM_PRICE.ITEM_CODE = INV_ITEM_DIRECTORY_UNITS.ITEM_CODE LEFT OUTER JOIN INV_ITEM_DIRECTORY ON INV_ITEM_DIRECTORY_UNITS.ITEM_CODE = INV_ITEM_DIRECTORY.CODE LEFT OUTER JOIN GEN_TAX_MASTER ON GEN_TAX_MASTER.TaxId = INV_ITEM_DIRECTORY.TaxId WHERE     (INV_ITEM_PRICE.SAL_TYPE = '" + RATE_CODE.SelectedValue + "')";

                }
                cmd.CommandType = CommandType.Text;
                DataTable dt = new DataTable();
                adapter.SelectCommand = cmd;
                adapter.Fill(dt);
                source.DataSource = dt;
                dataGridItem.DataSource = source;
                dataGridItem.RowHeadersVisible = false;
                dataGridItem.Columns[1].Visible = false;
                dataGridItem.Columns[2].Width = 250;
                if (!hasArabic)
                {
                    dataGridItem.Columns["DESC_ARB"].Visible = false;
                }

                if (!hasTax)
                {
                    dataGridItem.Columns["TaxId"].Visible = false;
                    dataGridItem.Columns["TaxRate"].Visible = false;
                }

                dataGridItem.ClearSelection();
                    
            }
            catch
            {
            }


        }

        private void ITEM_NAME_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Down)
                {
                    if (dataGridItem.Visible == true)
                    {
                        ShowStock = true;
                        dataGridItem.Focus();
                        dataGridItem.CurrentCell = dataGridItem.Rows[0].Cells[2];

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
                else if (e.KeyCode == Keys.Up)
                {
                    source.Filter = "";
                    bindgridview();
                    dataGridItem.Visible = true;
                    dataGridItem.ClearSelection();

                }
            }
            catch
            {
            }
           
        }

        private void dataGridItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridItem_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                {
                    ShowStock = false;
                    itemSelected = true;
                    string itemcode = dataGridItem.CurrentRow.Cells["Item Code"].Value.ToString();
                    ITEM_CODE.Text = itemcode;
                    PurchasePrice = Convert.ToDecimal(dataGridItem.CurrentRow.Cells["PURVALUEWITHTAX"].Value);

                    ITEM_CODE.Text = itemcode;
                   
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
                    UOM.Text = dataGridItem.CurrentRow.Cells["UNIT_CODE"].Value.ToString();
                    TaxId = Convert.ToInt16(dataGridItem.CurrentRow.Cells["TaxId"].Value.ToString());
                    GetTaxRate();
             
                 
                    GetDiscount();
                    
                   // addItem();
                   // clearItem();
                 //   ITEM_NAME.Focus();
                    ItemArabicName = dataGridItem.CurrentRow.Cells["DESC_ARB"].Value.ToString();
                    ITEM_NAME.Text = dataGridItem.CurrentRow.Cells["Item Name"].Value.ToString();
                
                    dataGridItem.Visible = false;
                    itemSelected = false;
                    





                }
            }
            catch
            {
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
               // Disc.Date = DateTime.Now.ToString("dd-mmm-yyyy");
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

        private void dgItems_Click(object sender, EventArgs e)
        {
            PaymentPanel.Visible = false;
            secondsave = false;
            if (dgItems.Rows.Count > 0 && dgItems.CurrentRow != null)
            {
                EditActive = true;
                DataGridViewCellCollection c = dgItems.CurrentRow.Cells;
                selectedRow = dgItems.CurrentRow.Index;
                ITEM_CODE.Text = Convert.ToString(c["cCode"].Value);
                firstitemlistbyname = true;
                ITEM_NAME.Text = Convert.ToString(c["cName"].Value);
                
                if (hasBatch)
                {
                    BATCH.Text = Convert.ToString(c["cBatch"].Value);
                    EXPIRY_DATE.Value = DateTime.ParseExact(Convert.ToString(c["cExpDate"].Value), "dd/MM/yyyy", null);
                }
                addUnits();
                UOM.Text = Convert.ToString(c["cUnit"].Value);
                QUANTITY.Text = Convert.ToString(c["cQty"].Value);
                PRICE.Text = Convert.ToString(c["cPrice"].Value);
                if (hasTax)
                {
                    ITEM_TAX_PER.Text = Convert.ToString(c["cTaxPer"].Value);
                    ITEM_TAX.Text = Convert.ToString(c["cTaxAmt"].Value);
                }
                GROSS_TOTAL.Text = Convert.ToString(c["cGTotal"].Value);
               // ITEM_DISCOUNT.Text = Convert.ToString(c["cDisc"].Value);
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
               
                QUANTITY.Focus();
            }
             dataGridItem.Visible = false;
            firstitemlistbyname = false;
        }

        private void TxtRoundOffAmt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.Control | Keys.S))
            {
                PaymentBtnFinalSave.PerformClick();
            }
        }

        private void TxtRoundOffAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
           // e.Handled = char.IsLetter(e.KeyChar) || char.IsDigit(e.KeyChar) ? false : true;
            if (!(Char.IsDigit(e.KeyChar) || (e.KeyChar == (char)Keys.Back)||(e.KeyChar=='.')))
                e.Handled = true;
        }

        private void calculateTax(object sender, EventArgs e)
        {

            //GROSS_TOTAL.Text = (total + tax).ToString();
            gettax();
        }

        private void dgItems_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {

                if (dgItems.Rows.Count > 0 && dgItems.CurrentRow != null)
                {
                    EditActive = true;
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
                    if (hasTax)
                    {
                        ITEM_TAX_PER.Text = Convert.ToString(c["cTaxPer"].Value);
                        ITEM_TAX.Text = Convert.ToString(c["cTaxAmt"].Value);
                    }
                    GROSS_TOTAL.Text = Convert.ToString(c["cGTotal"].Value);
                   // ITEM_DISCOUNT.Text = Convert.ToString(c["cDisc"].Value);
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
                    QUANTITY.Focus();
                }
                dataGridItem.Visible = false;
                firstitemlistbyname = false;
               
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
                    ITEM_NAME.Focus();
                }
            }
        }

        private void NewPictureBox_Click(object sender, EventArgs e)
        
        {
            try
            {
         
            ComponentFactory.Krypton.Navigator.KryptonPage kp = new ComponentFactory.Krypton.Navigator.KryptonPage();
            mdi.maindocpanel.Pages.Add(kp);
            New_Sales NewSal = new New_Sales("SAL.CSS", "Cash");
            NewSal.Show();
            NewSal.TopLevel = false;
            //  splitContainer1.Panel2.Controls.Add(ad);
            kp.Controls.Add(NewSal);
            NewSal.Dock = DockStyle.Fill;
            kp.Text = NewSal.Text;
            kp.Name = "POS Desk";
           // kp.Focus();
            NewSal.FormBorderStyle = FormBorderStyle.None;

            mdi.maindocpanel.SelectedPage = kp;
            }
            catch
            {
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

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

        private void GROSS_TOTAL_TextChanged(object sender, EventArgs e)
        {

        }

        private void Payment_cash_amntrecvd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || (e.KeyChar == (char)Keys.Back) || (e.KeyChar == (char)Keys.Decimal)))
                e.Handled = true;
        }

        private void Payment_cash_balance_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || (e.KeyChar == (char)Keys.Back) || (e.KeyChar == (char)Keys.Decimal)))
                e.Handled = true;
        }

        private void Payment_Card_Amont_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || (e.KeyChar == (char)Keys.Back) || (e.KeyChar == (char)Keys.Decimal)))
                e.Handled = true;
        }

        private void CARD_NO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || (e.KeyChar == (char)Keys.Back) || (e.KeyChar == (char)Keys.Decimal)))
                e.Handled = true;
        }

        private void Payment_Check_Amount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || (e.KeyChar == (char)Keys.Back) || (e.KeyChar == (char)Keys.Decimal)))
                e.Handled = true;
        }

        private void Payment_CreditNote_Amount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || (e.KeyChar == (char)Keys.Back) || (e.KeyChar == (char)Keys.Decimal)))
                e.Handled = true;
        }

        private void Payment_cash_amntrecvd_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                 if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (sender is KryptonTextBox)
                {
                    string name = (sender as KryptonTextBox).Name;
                    switch (name)
                    {
                        case "Payment_cash_amntrecvd":
                            PaymentTab.SelectedIndex = 1;
                            CARD_NO.Focus();
                            break;
                        case "CARD_NO":
                            Payment_Card_Amont.Focus();
                            break;
                        case "Payment_Card_Amont":
                            PaymentTab.SelectedIndex = 2;
                            Payment_Check_No.Focus();
                            break;
                        case "Payment_Check_No":
                            
                            Payment_Check_Amount.Focus();
                            break;

                        case "Payment_Check_Amount":
                            Payment_Check_Bankname.Focus();
                            break;

                        case "Payment_Check_Bankname":
                            dateTimePicker1.Focus();
                            break;

                        case "dateTimePicker1":
                            PaymentTab.SelectedIndex = 3;
                            Payment_Creditnote_no.Focus();
                            break;

                        case "Payment_Creditnote_no":

                            Payment_CreditNote_Amount.Focus();
                            break;

                        case "Payment_CreditNote_Amount":
                            PaymentTab.SelectedIndex = 4;
                            CUSTOMER_CODE.Focus();
                            break;
                        case "CUSTOMER_CODE":
                            btnCust.PerformClick();
                            break;
                        case "CUSTOMER_NAME":
                            Payment_Credit_amount.Focus();
                            break;
                        case "Payment_Credit_amount":
                            TxtRoundOffAmt.Focus();
                            break;
                        
                        default:
                            break;
                    }
                }
                else if(sender is DateTimePicker)
                {
                    PaymentTab.SelectedIndex = 3;
                    Payment_Creditnote_no.Focus();
                }
               
            }
            }
            catch
            {
            }

        }

        string  salesmanname;

        public void getsalesman()
        {
            SalesManCode= lg.EmpId;
            conn.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GetSalesMan";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Empid", lg.EmpId);
            salesmanname = Convert.ToString(cmd.ExecuteScalar());
            conn.Close();

        }

        private void ITEM_DISCOUNT_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == '-')
                {
                    if (DiscType == "Percentage")
                    {
                        Disctext.Text = "Disc Amt";
                        DiscType = "Amount";
                        CalculateDiscAmt();

                    }
                    else if (DiscType == "Amount")
                    {
                        Disctext.Text = "Disc %";
                        DiscType = "Percentage";
                        CalculateDiscPerct();
                    }
                    else
                    {
                        DiscType = "Amount";
                        Disctext.Text = "Disc Amt";
                        CalculateDiscAmt();
                    }





                    //if (ITEM_DISCOUNT.Text == "")
                    //{
                    //    QUANTITY.Text = "1";
                    //}
                    //else
                    //{
                    //    QUANTITY.Text = (Convert.ToInt32(QUANTITY.Text) + 1).ToString();
                    //}
                }

            }
            catch
            {
            }
        }


        public void CalculateDiscPerct()
        {
            try
            {
                Disctext.Text = "Disc %";

                if (ITEM_DISCOUNT.Text.Trim() != "" && GROSS_TOTAL.Text.Trim() != "")
                {
                    ITEM_TOTAL.Text = (Convert.ToDouble(GROSS_TOTAL.Text) - (Convert.ToDouble(GROSS_TOTAL.Text) * (Convert.ToDouble(ITEM_DISCOUNT.Text) / 100))).ToString();
                    DiscAmt = (Convert.ToDouble(GROSS_TOTAL.Text) * (Convert.ToDouble(ITEM_DISCOUNT.Text) / 100)).ToString();
                }

                if (ITEM_DISCOUNT.Text == "")
                {
                    ITEM_TOTAL.Text = GROSS_TOTAL.Text;
                    DiscAmt = DiscValue;
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
                    ITEM_TOTAL.Text = (Convert.ToDouble(GROSS_TOTAL.Text) - (Convert.ToDouble(ITEM_DISCOUNT.Text))).ToString();
                    DiscAmt = ITEM_DISCOUNT.Text;
                }

                if (ITEM_DISCOUNT.Text == "")
                {
                    ITEM_TOTAL.Text = GROSS_TOTAL.Text;
                    DiscAmt = DiscValue;
                }
            }
            catch
            {
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Find_Item Fitem = new Find_Item();
            Fitem.ShowDialog();
        }

        private void ITEM_DISCOUNT_Leave(object sender, EventArgs e)
        {
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

                            }
                        }
                        else
                        {
                            if (Convert.ToDouble(ITEM_DISCOUNT.Text) > DiscountAmtlimit)
                            {
                                MessageBox.Show("Discount Amount exeed limt");
                                ITEM_DISCOUNT.Text = "0.00";
                                ITEM_DISCOUNT.Focus();

                            }
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Error on value");
                ITEM_DISCOUNT.Text = "0.00";
                ITEM_DISCOUNT.Focus();
            }
        }

        private void ITEM_CODE_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!itemSelected)
                {
                    if (ITEM_CODE.Text == "")
                    {
                        dataGridItem.Visible = false;
                    }
                    else
                    {
                        dataGridItem.Visible = true;
                        source.Filter = string.Format("[Item Code] LIKE '%{0}%' ", ITEM_CODE.Text);
                        dataGridItem.ClearSelection();
                    }

                }
            }
            catch
            {
            }
        }

        private void ITEM_CODE_KeyDown_2(object sender, KeyEventArgs e)
        {

            if (e.KeyData == Keys.Down)
            {
                if (dataGridItem.Visible == true)
                {
                    dataGridItem.Focus();
                    dataGridItem.CurrentCell = dataGridItem.Rows[0].Cells[2];

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

        private void dataGridItem_Click(object sender, EventArgs e)
        {
            try
            {
                ShowStock = false;
                itemSelected = true;
                string itemcode = dataGridItem.CurrentRow.Cells["Item Code"].Value.ToString();
                ITEM_CODE.Text = itemcode;
                PurchasePrice = Convert.ToDecimal(dataGridItem.CurrentRow.Cells["PURVALUEWITHTAX"].Value);
                PRICE.Text = dataGridItem.CurrentRow.Cells["SALES"].Value.ToString();




                if (hasBatch)
                {
                    QUANTITY.Text = "1";
                    BATCH.Focus();
                }
                else if (MoveToUnit)
                {
                    QUANTITY.Text = "1";
                    UOM.Focus();
                }
                else
                {
                    QUANTITY.Text = "1";
                    QUANTITY.Focus();

                }


                addUnits();
                UOM.Text = dataGridItem.CurrentRow.Cells["UNIT_CODE"].Value.ToString();
                TaxId = Convert.ToInt16(dataGridItem.CurrentRow.Cells["TaxId"].Value.ToString());
                GetTaxRate();

                GetDiscount();
                ItemArabicName = dataGridItem.CurrentRow.Cells["DESC_ARB"].Value.ToString();
                ITEM_NAME.Text = dataGridItem.CurrentRow.Cells["Item Name"].Value.ToString();

                dataGridItem.Visible = false;
                itemSelected = false;
            }
            catch
            { }
        }

        private void DOC_DATE_GRE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || Keys.KeyCode == Keys.Tab)
            {
                if (SalebyItemName)
                {
                    ActiveControl = ITEM_NAME;
                }
                else if (SalebyBarcode)
                {
                    ActiveControl = BARCODE;
                }
                else
                {
                    ActiveControl = ITEM_CODE;
                }
            }
        }

        public void GetCustomerLevelDiscount(string CUS_CODE)
        {
            try
            {
                try
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
                catch (Exception ex)
                {
                    conn.Close();
                }
            }
            catch
            {

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
                    TempDiscount = DISCOUNT.Text;
                    if (lblCustomerDiscType.Text != "Amount")
                    {
                        try
                        {
                            DISCOUNT.Text = ((Convert.ToDecimal(TOTAL_AMOUNT.Text) * Convert.ToDecimal(lblCustomerDisctValue.Text)) / 100).ToString();
                            lblDiscount.Text = DISCOUNT.Text;
                        }
                        catch
                        {

                        }
                    }
                    else
                    {
                        DISCOUNT.Text = lblCustomerDisctValue.Text;
                        lblDiscount.Text = lblCustomerDisctValue.Text;
                    }
                }
            }
            else
            {
                lblCustomerDisctValue.Text = "0.00";
                lblCustomerDiscType.Text = "Type";
                DISCOUNT.Text = TempDiscount;
                lblDiscount.Text = TempDiscount;
            }
        
        }

        private void DISCOUNT_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double disc;
                if (DISCOUNT.Text == "")
                    disc = 0;
                else
                    disc = Convert.ToDouble(DISCOUNT.Text);
                double netamt;
                netamt = Convert.ToDouble(TOTAL_AMOUNT.Text) -disc ;
                NET_AMOUNT.Text = netamt.ToString();
                lbltotal.Text = netamt.ToString();
            }
            catch
            { }
        }

        private void ITEM_TOTAL_TextChanged(object sender, EventArgs e)
        {
            try
            {
                lblProfit.Text = (Convert.ToDecimal(ITEM_TOTAL.Text) - (Convert.ToDecimal(QUANTITY.Text) * PurchasePrice)).ToString();
            }
            catch
            {
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
                lblstock.Text = dt.Rows[0][0].ToString();
            }
        }
        private void dataGridItem_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (ShowStock)
            {
                try
                {
                    int index = e.RowIndex;
                    GetItemStockvalue(dataGridItem.Rows[index].Cells["Item Code"].Value.ToString(), dataGridItem.Rows[index].Cells["UNIT_CODE"].Value.ToString());

                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }
            }
        }

        private void SALESACC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                CASHACC.Focus();
            }
        }

        private void CASHACC_KeyDown(object sender, KeyEventArgs e)
        {
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
        
}
}
