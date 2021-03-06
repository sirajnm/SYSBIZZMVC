﻿using ComponentFactory.Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sys_Sols_Inventory.Model;

namespace Sys_Sols_Inventory
{
    public partial class Purchase_Return : Form
    {

        #region properties declaration
        private bool hasBatch = true;
        private bool hasTax = true;
        private bool hasBarcode = true;
        private bool hasArabic = true;
        private DataTable unitsTable = new DataTable();
        private int selectedRow = -1;
        private string ID = "";
        private string STOCKID = "";
        private string type;

        //private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        //private SqlCommand cmd = new SqlCommand();
        //private SqlDataAdapter adapter = new SqlDataAdapter();
        private DataTable table = new DataTable();
        private DataTable filterTable = new DataTable();
        private DataTable rateTable = new DataTable();
        private BindingSource source = new BindingSource();
        private AutoCompleteStringCollection coll = new AutoCompleteStringCollection();
        Class.CompanySetup ComSet = new Class.CompanySetup();
        Class.Printing Printing = new Class.Printing();
        Initial mdi = (Initial)Application.OpenForms["Initial"];
        Login lg = (Login)Application.OpenForms["Login"];
        Class.ModifiedTransaction modtrans = new Class.ModifiedTransaction();
        InvPurchaseDtlDB purchDtlObj = new InvPurchaseDtlDB();
        InvSalesHdrDB salehdrObj = new InvSalesHdrDB();
        InvSalesDtlDB saledtlObj = new InvSalesDtlDB();
        InvStkTrxHdrDB stkhdrObj = new InvStkTrxHdrDB();
        InvPurchaseHdrDB purchHdrObj = new InvPurchaseHdrDB();
        InvItemDirectoryUnits itemdirObj = new InvItemDirectoryUnits();
        InvPurchaseDtlDB purchdtlObj = new InvPurchaseDtlDB();
        TblDeletedTransactionDB dltdtranobj = new TblDeletedTransactionDB();
        #endregion


        bool fixedheight = false, PAGETOTAL = false;
        decimal DecPOSVoucherTypeId = 0;        //to get the selected voucher type id from frmVoucherTypeSelection       
        decimal decPOSSuffixPrefixId = 0;
        decimal decProductId = 0;               //to fill product using barcode
        decimal decBatchId;
        decimal decCurrentConversionRate;
        decimal decCurrentRate;
        decimal decSalesMasterId = 0;
        string decimalFormat;
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
        public int printeditems = 0;
        int k = 0;
        decimal tgrossrate = 0, ttaxva = 0, trate = 0, tcdis = 0, ttaxbl = 0, tfree = 0;
        int rowIdToEdit = 0;
        int maxSerialNo = 0;
        int inNarrationCount = 0;
        bool isAutomatic = false;
        private bool hasPurExclusive = false;//to check whether the voucher number is automatically generated or not
        bool isdontExecuteTextchange = false;
        bool isFromSalesAccountCombo = false;   // for add new new account via button click
        bool isFromCashOrPartyCombo = false;    // for add new new account via button click
        bool isFromSalesManCombo = false;
        bool isFormIdtoEdit = false;
        bool isFromCounterCombo = false;
        bool isAfterFillControls = false;
        bool isFromDiscAmt = false;
        bool isFromBarcode = false;
        bool hasPriceBatch = false;
        string PID = "";
        string PDATE = "";
        decimal balance = 0;
        string Retunstatus = "Active";
        string ReturnBillId = "";
        private bool HasAccounts = false;
        private DataTable table_for_batch = new DataTable();
        Class.Ledgers led = new Class.Ledgers();
        Class.Transactions trans = new Class.Transactions();
        DebitNoteDB DebitNoteDB = new DebitNoteDB();
        private DateTime TransDate;
        double sales_price = 0;
        int crnoteno = 0;
        string CompanyName, Address1, Addres1, Addres2, Phone, Fax, Email, TineNo, Billno, Date, CUSID, Website, panno, vat, logo, SalesManCode, salesmanname;

        public Purchase_Return()
        {
            InitializeComponent();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.F12))
            {
                btnSave.PerformClick();
                return true;
            }
            if (keyData == (Keys.Escape))
            {
                if (dgBatch.Visible == true)
                {
                    dgBatch.Visible = false;
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
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnDocNo_Click(object sender, EventArgs e)
        {
            Purchase_Return_Help h = new Purchase_Return_Help(0, "LGR.CPR','LGR.PRT");

            if (h.ShowDialog() == DialogResult.OK && h.c != null)
            {
                btnClear.PerformClick();
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
                txtinvoiceno.Text = Convert.ToString(h.c["DOC_REFERENCE"].Value);
                CUSTOMER_CODE.Text = Convert.ToString(h.c["SUPPLIER_CODE"].Value);
                GetLedgerId(Convert.ToString(h.c["SUPPLIER_CODE"].Value));
                //  CUSTOMER_NAME.Text = Convert.ToString(h.c["CUSTOMER_NAME_ENG"].Value);
                SALESMAN_CODE.Text = Convert.ToString(h.c["SalesMan"].Value);
                NOTES.Text = Convert.ToString(h.c["NOTES"].Value);
                TAX_TOTAL.Text = Convert.ToString(h.c["TAX_TOTAL"].Value);
                //VAT.Text = Convert.ToString(h.c["VAT"].Value);
                //DISCOUNT.Text = Convert.ToString(h.c["DISCOUNT"].Value);
                TOTAL_AMOUNT.Text = Convert.ToString(h.c["GROSS"].Value);
                NET_AMOUNT.Text = Convert.ToString(h.c["NET_VAL"].Value);
                PAY_CODE.Text = Convert.ToString(h.c["PAY_CODE"].Value);
                //     PAY_NAME.Text = General.getName(PAY_CODE.Text, "GEN_PAYTYPE");
                CARD_NO.Text = Convert.ToString(h.c["CARD_NO"].Value);
                txt_srno.Text = Convert.ToString(h.c["DOC_id"].Value);

                try
                {
                    crnoteno = Convert.ToInt32(h.c["DEBIT_NOTE"].Value == "" ? "0" : h.c["DEBIT_NOTE"].Value);
                    DOC_REFERENCE.Text = crnoteno == 0 ? DOC_REFERENCE.Text : crnoteno.ToString();
                }
                catch
                {
 
                }
                //conn.Open();
                //cmd.CommandText = "SELECT dtl.*,INV_PURCHASE_HDR.SUPPLIER_CODE FROM INV_PURCHASE_DTL as dtl LEFT JOIN INV_PURCHASE_HDR ON dtl.DOC_NO=INV_PURCHASE_HDR.DOC_NO WHERE dtl.DOC_NO ='" + DOC_NO.Text + "'AND dtl.FLAGDEL='TRUE'";
                //cmd.CommandType = CommandType.Text;
                //SqlDataReader r = cmd.ExecuteReader();
                purchDtlObj.DocNo = DOC_NO.Text;
                DataTable r = purchDtlObj.getDetails();
                for (int j = 0; j < r.Rows.Count; j++)
                {
                    int i = dgItems.Rows.Add(new DataGridViewRow());
                    DataGridViewCellCollection c = dgItems.Rows[i].Cells;
                    c["cCode"].Value = r.Rows[j]["ITEM_CODE"];
                    c["cName"].Value = r.Rows[j]["ITEM_DESC_ENG"];
                    if (hasBatch)
                    {
                        c["cBatch"].Value = r.Rows[j]["BATCH"];
                        //c["cExpDate"].Value = Convert.ToDateTime(r["EXPIRY_DATE"]).ToString("dd/MM/yyyy");
                        try
                        {
                            c["cExpDate"].Value = Convert.ToDateTime(r.Rows[j]["EXPIRY_DATE"]).ToString();
                        }

                        catch
                        {
                        }
                    }
                    c["cUnit"].Value = r.Rows[j]["UOM"];
                    c["cQty"].Value = r.Rows[j]["QTY_RCVD"];
                    c["cPrice"].Value = r.Rows[j]["PRICE_FOB"];
                    if (hasTax)
                    {
                        if (r.Rows[j]["ITEM_TAX_PER"].ToString() == "")
                        {
                            c["cTaxPer"].Value = "0";
                        }
                        else
                        {
                            c["cTaxPer"].Value = r.Rows[j]["ITEM_TAX_PER"];
                        }

                        if (r.Rows[j]["ITEM_TAX"].ToString() == "")
                        {
                            c["cTaxAmt"].Value = "0";
                        }
                        else
                        {
                            c["cTaxAmt"].Value = r.Rows[j]["ITEM_TAX"];
                        }
                    }
                    else
                    {
                        c["cTaxPer"].Value = "0";
                        c["cTaxAmt"].Value = "0";

                    }
                    if (r.Rows[j]["ITEM_GROSS"].ToString() == "")
                        c["cGTotal"].Value = "0.00";
                    else
                        c["cGTotal"].Value = r.Rows[j]["ITEM_GROSS"];

                    c["cDisc"].Value = r.Rows[j]["ITEM_DISCOUNT"];
                    if (r.Rows[j]["NET_AMOUNT"].ToString() == "")
                        c["cTotal"].Value = (Convert.ToDecimal(c["cGTotal"].Value) + Convert.ToDecimal(c["cTaxAmt"].Value)) - Convert.ToDecimal(c["cDisc"].Value);
                    else
                        c["cTotal"].Value = r.Rows[j]["NET_AMOUNT"];
                    DOC_DATE_GRE.Value = DateTime.Now;

                    c["uomQty"].Value = r.Rows[j]["UOM_QTY"];
                    c["cost_price"].Value = r.Rows[j]["cost_price"];

                    c["supplier_id"].Value = r.Rows[j]["SUPPLIER_CODE"];
                    //c["supplier_name"].Value = r["supplier_name"];
                    if (r.Rows[j]["PRICE_BATCH"].ToString() != null)
                    {
                        c["colBATCH"].Value = r.Rows[j]["PRICE_BATCH"];
                    }
                    //  c["supplier_name"].Value = r["supplier_name"];
                }
                //conn.Close();
                DbFunctions.CloseConnection();
                GetStockIDtoUpdate();
                KrBt_print.Visible = true;
            }
        }

        public void getdatafromDocNo()
        {
            //conn.Open();
            //cmd.CommandText = "SELECT * FROM INV_SALES_HDR WHERE DOC_NO = '" + DOC_NO.Text + "'";
            //cmd.CommandType = CommandType.Text;
            //SqlDataReader rd = cmd.ExecuteReader();
            salehdrObj.DocNo = DOC_NO.Text;
            DataTable rd = salehdrObj.getdatafromDocNo_1();
            for (int i = 0; i < rd.Rows.Count; i++)
            {

                try
                {
                    DOC_DATE_GRE.Value = Convert.ToDateTime(Convert.ToString(rd.Rows[i]["DOC_DATE_GRE"]));
                }
                catch
                {
                }
                DOC_DATE_HIJ.Text = Convert.ToString(rd.Rows[i]["DOC_DATE_HIJ"]);
                CURRENCY_CODE.Text = Convert.ToString(rd.Rows[i]["CURRENCY_CODE"]);
                DOC_REFERENCE.Text = Convert.ToString(rd.Rows[i]["DOC_REFERENCE"]);
                CUSTOMER_CODE.Text = Convert.ToString(rd.Rows[i]["CUSTOMER_CODE"]);
                CUSTOMER_NAME.Text = Convert.ToString(rd.Rows[i]["CUSTOMER_NAME_ENG"]);
                GetLedgerId(Convert.ToString(rd.Rows[i]["CUSTOMER_CODE"]));
                SALESMAN_CODE.Text = Convert.ToString(rd.Rows[i]["SALESMAN_CODE"]);
                NOTES.Text = Convert.ToString(rd.Rows[i]["NOTES"]);
                TAX_TOTAL.Text = Convert.ToString(rd.Rows[i]["TAX_TOTAL"]);
                VAT.Text = Convert.ToString(rd.Rows[i]["VAT"]);
                DISCOUNT.Text = Convert.ToString(rd.Rows[i]["DISCOUNT"]);
                TOTAL_AMOUNT.Text = Convert.ToString(rd.Rows[i]["TOTAL_AMOUNT"]);
                NET_AMOUNT.Text = Convert.ToString(rd.Rows[i]["NET_AMOUNT"]);
                PAY_CODE.Text = Convert.ToString(rd.Rows[i]["PAY_CODE"]);
                PAY_NAME.Text = General.getName(PAY_CODE.Text, "GEN_PAYTYPE");
                CARD_NO.Text = Convert.ToString(rd.Rows[i]["CARD_NO"]);
            }
            // cmd.CommandText = "SELECT * FROM INV_SALES_DTL WHERE DOC_NO = '" + DOC_NO.Text + "'";
            saledtlObj.DocNo = DOC_NO.Text;
            DataTable r = saledtlObj.DtlByDocNoReader();
            for (int j = 0; j < r.Rows.Count; j++)
            {
                int i = dgItems.Rows.Add(new DataGridViewRow());
                DataGridViewCellCollection c = dgItems.Rows[i].Cells;
                c["cCode"].Value = r.Rows[j]["ITEM_CODE"];
                c["cName"].Value = r.Rows[j]["ITEM_DESC_ENG"];
                c["colBatch"].Value = r.Rows[j]["PRICE_BATCH"];
                if (hasBatch)
                {

                    //c["cExpDate"].Value = Convert.ToDateTime(r["EXPIRY_DATE"]).ToString("dd/MM/yyyy");
                    try
                    {
                        c["cExpDate"].Value = Convert.ToDateTime(r.Rows[j]["EXPIRY_DATE"]).ToString();
                    }

                    catch
                    {
                    }
                }
                c["cUnit"].Value = r.Rows[j]["UOM"];
                c["cQty"].Value = r.Rows[j]["QUANTITY"];
                c["cPrice"].Value = r.Rows[j]["PRICE"];
                if (hasTax)
                {
                    c["cTaxPer"].Value = r.Rows[j]["ITEM_TAX_PER"];
                    c["cTaxAmt"].Value = r.Rows[j]["ITEM_TAX"];
                }
                c["cGTotal"].Value = r.Rows[j]["GROSS_TOTAL"];
                c["cDisc"].Value = r.Rows[j]["ITEM_DISCOUNT"];
                c["cTotal"].Value = r.Rows[j]["ITEM_TOTAL"];
            }
            DbFunctions.CloseConnection();
            GetStockIDtoUpdate();


        }

        public void GetLedgerId(string CusCode)
        {
            if (CusCode == "")
            {
                DEBITACC.SelectedValue = 21;
            }
            else
            {
                DataTable dt = new DataTable();
                led.CUSCODE = CusCode;
                led.TABLE = "PAY_SUPPLIR";
                dt = led.GetLedgerId1();
                if (dt.Rows.Count > 0)
                {
                    DEBITACC.SelectedValue = dt.Rows[0][0].ToString();
                }
                else
                {
                    DEBITACC.SelectedValue = 21;
                }
            }
        }

        public void GetStockIDtoUpdate()
        {
            try
            {
                // cmd.CommandText = "SELECT DOC_NO FROM INV_STK_TRX_HDR WHERE DOC_REFERENCE = '" + DOC_NO.Text + "'";
                stkhdrObj.DocReference = DOC_NO.Text;
                SqlDataReader r = stkhdrObj.getDocNoByDocRef();
                while (r.Read())
                {
                    STOCKID = r["DOC_NO"].ToString();
                }
                DbFunctions.CloseConnection();
            }
            catch
            {
                DbFunctions.CloseConnection();
            }

        }

        public Purchase_Return(string docType, string prefix)
        {
            InitializeComponent();
            hasBatch = General.IsEnabled(Settings.Batch);
            hasTax = General.IsEnabled(Settings.Tax);
            hasBarcode = General.IsEnabled(Settings.Barcode);
            hasArabic = General.IsEnabled(Settings.Arabic);
            HasAccounts = Properties.Settings.Default.Account;
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

            //cmd.Connection = conn;
            //adapter.SelectCommand = cmd;
            getDetails();

            filterTable.Columns.Add("key");
            filterTable.Rows.Add("Doc. #");
            filterTable.Rows.Add("Date GRE");
            filterTable.Rows.Add("Currency Code");
            filterTable.Rows.Add("Customer Code");
            filterTable.Rows.Add("Salesman Code");
            filterTable.Rows.Add("Notes");
            filterTable.Rows.Add("Pay Code");
            cmbFilter.DataSource = filterTable;
            cmbFilter.DisplayMember = "key";

            //cmd.CommandText = "SELECT CODE AS [key],CODE+' - '+DESC_ENG AS value FROM GEN_PRICE_TYPE";
            rateTable = salehdrObj.GetPriceType();
            RATE_CODE.DisplayMember = "value";
            RATE_CODE.ValueMember = "key";
            RATE_CODE.DataSource = rateTable;
            bindledgers();
        }

        public Purchase_Return(string docNo)
        {
            InitializeComponent();
            hasBatch = General.IsEnabled(Settings.Batch);
            hasTax = General.IsEnabled(Settings.Tax);
            hasBarcode = General.IsEnabled(Settings.Barcode);
            hasArabic = General.IsEnabled(Settings.Arabic);
            HasAccounts = Properties.Settings.Default.Account;
            unitsTable.Columns.Add("key");
            unitsTable.Columns.Add("value");
            UOM.DataSource = unitsTable;
            // type = docType;
            // Text += " - " + prefix;

            QUANTITY.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            PRICE.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            if (hasTax)
            {
                ITEM_TAX_PER.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            }
            ITEM_DISCOUNT.KeyPress += new KeyPressEventHandler(General.OnlyFloat);

            getDetails();

            filterTable.Columns.Add("key");
            filterTable.Rows.Add("Doc. #");
            filterTable.Rows.Add("Date GRE");
            filterTable.Rows.Add("Currency Code");
            filterTable.Rows.Add("Customer Code");
            filterTable.Rows.Add("Salesman Code");
            filterTable.Rows.Add("Notes");
            filterTable.Rows.Add("Pay Code");
            cmbFilter.DataSource = filterTable;
            cmbFilter.DisplayMember = "key";

            //cmd.CommandText = "SELECT CODE AS [key],CODE+' - '+DESC_ENG AS value FROM GEN_PRICE_TYPE";
            rateTable = salehdrObj.GetPriceType();
            RATE_CODE.DisplayMember = "value";
            RATE_CODE.ValueMember = "key";
            RATE_CODE.DataSource = rateTable;
            bindledgers();
            DOC_NO.Text = docNo;
            getdatafromDocNo();
        }

        private void getDetails()
        {
            table.Rows.Clear();
            //cmd.CommandText = "SELECT * FROM viewSalesHDR";
            //cmd.CommandType = CommandType.Text;
            //adapter.Fill(table);
            table = salehdrObj.ViewSalesHDR();
            source.DataSource = table;
            dgDetail.DataSource = source;
        }

        private void Sales_Return_Load(object sender, EventArgs e)
        {
            GetBranchDetails();
         
            DOC_DATE_GRE.Text = ComSet.GettDate();

            PrintPage.SelectedIndex = 0;
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

         //  panBarcode.Visible = hasBarcode;
            if (hasArabic)
            {
                DOC_DATE_HIJ.Enabled = false;
                PnlArabic.Visible = false;
            }

            //if (hasBarcode)
            //{
            //  //  panBarcode.Visible = false;
                
            //}
            //not validated//

            BARCODE.Visible = true;
            //not validated//
            if (HasAccounts)
            {
                pnlacct.Visible = true;
            }
            decimalFormat = Common.getDecimalFormat();
            hasPurExclusive = General.IsEnabled(Settings.Pur_Exclusive_tax);
            hasPriceBatch = General.IsEnabled(Settings.priceBatch);
            ActiveControl = txtinvoiceno;
            DEBITACC.Text = "CASH ACCOUNT";
            CREDITACC.Text = "PURCHASE RETURN ACCOUNT";
            getsalesman();
            SALESMAN_CODE.Text = SalesManCode;
            SALESMAN_NAME.Text = salesmanname;
            BindBatchTable();
            dgBatch.Visible = false;
            BindCurrency();

            chkDebit.Checked = true;


            RATE_CODE.SelectedIndex = 4;
            CURRENCY_CODE.SelectedIndex = 1;

        }
        public void BindBatchTable()
        {
            table_for_batch.Clear();
            SqlCommand cmd = new SqlCommand();
            string command = "itemSuggestion_test";
            table_for_batch = DbFunctions.GetDataTableProcedure(command);

        }
        public void bindBatchGrid(string code)
        {
            dgBatch.DataSource = null;
            dgBatch.Rows.Clear();
            DataRow[] dr = null; ;
            DataTable dt1 = null;
            dgBatch.DataSource = "";
            dr = table_for_batch.Select("ITEM_CODE = '" + code + "'AND STOCK>0");
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
        public void BindCurrency()
        {
            DataTable dt = salehdrObj.GetCurrency();
            //cmd.Connection = conn;
            //cmd.CommandText = "SELECT * FROM GEN_CURRENCY";
            //cmd.CommandType = CommandType.Text;
            //SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            //adapter.Fill(dt);
            CURRENCY_CODE.DataSource = dt;
            CURRENCY_CODE.DisplayMember = "DESC_ENG";
            CURRENCY_CODE.ValueMember = "CODE";
        }
        public void getsalesman()
        {
            SalesManCode = lg.EmpId;
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.CommandText = "GetSalesMan";
            //cmd.Parameters.Clear();
            //cmd.Parameters.AddWithValue("@Empid", lg.EmpId);
            //salesmanname = Convert.ToString(cmd.ExecuteScalar());

            string command = "GetSalesMan";
            string val = lg.EmpId;
            salesmanname = Convert.ToString(salehdrObj.storedProc(command, "@Empid", val));


        }
        public string GetMaxDocID()
        {
            long maxId;
            String value;
            /*
            //  cmd.CommandText = "SELECT ISNULL(MAX(DOC_ID), 0) FROM INV_PURCHASE_HDR WHERE DOC_TYPE = '" + type + "' AND FLAGDEL='True'";
            value = Convert.ToString(purchHdrObj.getMaxDocId("LGR.CPR','LGR.PRT"));
            if (value.Equals("0"))
            {
                return 1;
            }
            else
            {
                maxId = Convert.ToInt64(value) + 1;
                return maxId;
            }
            */
            string doctype = "PurchaseReturn";
            string saletype = "PurchaseReturn";
            string query = "Declare @MaxDocID as int, @NoSeriesSuffix as varchar(5) ";
            query += " Select @MaxDocID = case when Max(Doc_ID) is null then 0 else Max(Doc_ID) end + 1, @NoSeriesSuffix = max(f.NoSeriesSuffix) from INV_PURCHASE_HDR p right join tbl_FinancialYear f on Convert(Varchar, p.DOC_DATE_GRE, 111) between f.SDate and f.EDate ";
            if (saletype == "PurchaseReturn") query += " and p.DOC_TYPE = 'LGR.ESR' ";
            if (saletype == "PurchaseReturn") query += " and p.DOC_TYPE in ('LGR.CPR','LGR.PRT') ";
            query += " where f.CurrentFY = 1 ";
            query += " Select s.PRIFIX + @NoSeriesSuffix + Right(Replicate('0', s.SERIAL_LENGTH) + cast(@MaxDocID as varchar), s.SERIAL_LENGTH) DOCNo, @MaxDocID DocID from GEN_DOC_SERIAL s ";
            query += " where s.DOC_TYPE = '" + saletype + "' ";
            DataTable dt = DbFunctions.GetDataTable(query);
            if (dt.Rows.Count >= 1)
            {
                Billno = txt_srno.Text = dt.Rows[0]["DOCID"].ToString();
                DOC_NO.Text = dt.Rows[0]["DOCNO"].ToString();

                return dt.Rows[0]["DOCID"].ToString();
            }

            return "";




        }
        DataTable dt1 = new DataTable();
        public void bindledgers()
        {
            dt1.Clear();
            dt1 = led.Selectledger();
            DEBITACC.DataSource = dt1;
            DEBITACC.DisplayMember = "LEDGERNAME";
            DEBITACC.ValueMember = "LEDGERID";

            DataTable dt2 = new DataTable();
            dt2 = led.Selectledger();
            CREDITACC.DataSource = dt2;
            CREDITACC.DisplayMember = "LEDGERNAME";
            CREDITACC.ValueMember = "LEDGERID";
        }

        public void GetBranchDetails()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = ComSet.GetCurrentBranchDetails();
                Addres1 = dt.Rows[0][1].ToString();
                Addres2 = dt.Rows[0][2].ToString();
                Phone = dt.Rows[0][3].ToString();
                Email = dt.Rows[0][4].ToString();
                Fax = dt.Rows[0][5].ToString();
            }
            catch
            {
            }
        }

        public void GetCompanyDetails()
        {
            DataTable dt = new DataTable();
            dt = ComSet.getCompanyDetails();
            CompanyName = dt.Rows[0][1].ToString();
            TineNo = dt.Rows[0][8].ToString();
            CUSID = dt.Rows[0][10].ToString();
            Website = dt.Rows[0][11].ToString();
            panno = dt.Rows[0][9].ToString();
            logo = dt.Rows[0][6].ToString();

        }

        int TaxId;
        private bool flagPrintEventAssigned = false;
        private int counter = 0;
        private int m = 0;

        private void btnItemCode_Click(object sender, EventArgs e)
        {
            ItemMasterHelp h = new ItemMasterHelp(0, hasPriceBatch);
            if (h.ShowDialog() == DialogResult.OK && h.c != null)
            {
                ITEM_CODE.Text = Convert.ToString(h.c[0].Value);
                ITEM_NAME.Text = Convert.ToString(h.c[1].Value);
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
                        //  dgBatch.Focus();
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
                            String rateType = Convert.ToString(RATE_CODE.SelectedValue);
                            string pricedecimal = h.c[rateType].Value.ToString();
                            sales_price = Convert.ToDouble(h.c[rateType].Value);
                            double pricedec = Convert.ToDouble(pricedecimal);
                            PRICE.Text = pricedec.ToString();
                            if (RATE_CODE.Text.StartsWith("MRP"))
                            {
                                double taxcalc = 0;
                                taxcalc = (Convert.ToDouble(ITEM_TAX_PER.Text) / 100) + 1;
                                PRICE.Text = (Convert.ToDouble(PRICE.Text) / taxcalc).ToString(decimalFormat);
                            }
                            else if (!hasPurExclusive)
                            {
                                double taxcalc = 0;
                                taxcalc = (Convert.ToDouble(ITEM_TAX_PER.Text) / 100) + 1;
                                PRICE.Text = (Convert.ToDouble(PRICE.Text) / taxcalc).ToString();
                            }
                            else
                            {
                                PRICE.Text = h.c[rateType].Value.ToString();
                            }
                            sales_price = Convert.ToDouble(PRICE.Text);
                            PRICE.Text = sales_price.ToString();
                            ITEM_TAX_TextChanged(sender, e);
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

                            //   this.UOM.SelectedIndexChanged -= new EventHandler(UOM_SelectedIndexChanged);
                            //  addUnits();
                            //   this.UOM.SelectedIndexChanged -= new EventHandler(UOM_SelectedIndexChanged);
                            // UOM.Text = dgBatch.CurrentRow.Cells["UNIT_CODE"].Value.ToString();
                            TaxId = Convert.ToInt16(dgBatch.CurrentRow.Cells["TaxId"].Value.ToString());
                            GetTaxRate();

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
        public void GetTaxRate()
        {
            try
            {
                // cmd.CommandText = "SELECT TaxRate from GEN_TAX_MASTER where TaxId=" + TaxId;
                stkhdrObj.TaxId = TaxId.ToString();
                SqlDataReader r = stkhdrObj.selectTaxRate();

                while (r.Read())
                {
                    ITEM_TAX_PER.Text = r[0].ToString();
                }
                DbFunctions.CloseConnection();
            }
            catch (Exception ex)
            {
            } DbFunctions.CloseConnection();
        }

        private void addUnits()
        {
            unitsTable.Rows.Clear();
            //cmd.CommandText = "SELECT UNIT_CODE, PACK_SIZE FROM INV_ITEM_DIRECTORY_UNITS WHERE ITEM_CODE = '" + ITEM_CODE.Text + "'";
            itemdirObj.ItemCode = ITEM_CODE.Text;
            unitsTable = itemdirObj.UnitPacksizeSales();
            this.UOM.SelectedIndexChanged -= new EventHandler(UOM_SelectedIndexChanged);
            UOM.DataSource = unitsTable;
            UOM.DisplayMember = "UNIT_CODE";
            UOM.ValueMember = "PACK_SIZE";

            this.UOM.SelectedIndexChanged += new EventHandler(UOM_SelectedIndexChanged);
        }

        private void ITEM_CODE_KeyDown(object sender, KeyEventArgs e)
        {

        }


        private void clearItem()
        {
            ITEM_CODE.Text = "";
            ITEM_NAME.Text = "";
            if (hasBatch)
            {
                BATCH.Text = "";
                EXPIRY_DATE.Value = DateTime.Today;
            }
            unitsTable.Rows.Clear();
            QUANTITY.Text = "";
            PRICE.Text = "";
            if (hasTax)
            {
                ITEM_TAX_PER.Text = "";
                ITEM_TAX.Text = "";
            }
            GROSS_TOTAL.Text = "";
            ITEM_DISCOUNT.Text = "";
            ITEM_TOTAL.Text = "";
            selectedRow = -1;
        }
        private void btnCust_Click(object sender, EventArgs e)
        {
            //customer
            CommonHelp h = new CommonHelp(0, genEnum.Supplier);
            if (h.ShowDialog() == DialogResult.OK && h.c != null)
            {
                try
                {
                    CUSTOMER_CODE.Text = Convert.ToString(h.c[0].Value);
                    CUSTOMER_NAME.Text = Convert.ToString(h.c[1].Value);
                    DEBITACC.SelectedValue = Convert.ToString(h.c["LedgerId"].Value);
                    CUSTOMER_CODE.Focus();
                }
                catch
                { }
            }
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


            //  this.Close();
        }

        private void linkRemoveRecord_LinkClicked(object sender, EventArgs e)
        {
            if (dgItems.Rows.Count > 0 && dgItems.CurrentRow != null)
            {
                if (dgItems.CurrentRow.Index == selectedRow)
                {
                    selectedRow = -1;
                }
                dgItems.Rows.Remove(dgItems.CurrentRow);
                totalCalculation();
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

            TOTAL_AMOUNT.Text = grossTotal.ToString(decimalFormat);
            DISCOUNT.Text = discount.ToString();
            NET_AMOUNT.Text = nettAmount.ToString();
            if (hasTax)
            {
                TAX_TOTAL.Text = tax.ToString(decimalFormat);
                VAT.Text = vat.ToString();
            }
        }

        private void addItem()
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
                c["uomQty"].Value = unitsTable.Select("UNIT_CODE = '" + UOM.Text + "'").First()["PACK_SIZE"];
                c["cPrice"].Value = PRICE.Text;
                if (hasTax)
                {

                    c["cTaxPer"].Value = ITEM_TAX_PER.Text;
                    c["cTaxAmt"].Value = ITEM_TAX.Text;
                }
                c["cGTotal"].Value = GROSS_TOTAL.Text;
                c["cDisc"].Value = ITEM_DISCOUNT.Text;

                c["cTotal"].Value = decimal.Round(Convert.ToDecimal(ITEM_TOTAL.Text), 2, MidpointRounding.AwayFromZero);
                //---NOT VALIDATED---//
                if (BARCODE.Text != "")
                {
                    c["colBATCH"].Value = BARCODE.Text;

                }
                totalCalculation();
            }
        }

        private bool itemValid()
        {
            // NOT VALIDATED//
            if (BARCODE.Text == "")
            {
                MessageBox.Show("Please  Select any price batch!");
                ITEM_CODE.Focus();
                return false;
            }
            // NOT VALIDATED//
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            ID = "";
            crnoteno = 0;
            STOCKID = "";
            txtinvoiceno.Text = "";
            DOC_NO.Text = "";
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
            PAY_CODE.Text = "";
            PAY_NAME.Text = "";
            CARD_NO.Text = "";
            NOTES.Text = "";
            TAX_TOTAL.Text = "0.00";
            VAT.Text = "0.00";
            TOTAL_AMOUNT.Text = "0.00";
            DISCOUNT.Text = "0.00";
            NET_AMOUNT.Text = "0.00";
            txtcashrcvd.Text = "0.00";
            DEBITACC.Text = "CASH ACCOUNT";
            CREDITACC.Text = "PURCHASE RETURN ACCOUNT";
            BARCODE.Text = "";

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            btnClear.PerformClick();
            SalesHelp h = new SalesHelp(1, "");
            h.ShowDialog();
        }

        public void retcheck()
        {

        }

        private bool valid()
        {

            if (dgItems.Rows.Count <= 0)
            {
                MessageBox.Show("Please add items to Return.");
                return false;
            }



            else
            {
                return true;
            }
        }

        //-->public void DownStockonProfit(string code, int qty)
        //{
        //    string item_code = code;
        //    Int32 squantity = qty;
        //    Decimal totPrice = 0;
        //    Int32 QtyChecker = 0;
        //    string Qty = "0";
        //    try
        //    {

        //if (conn.State == ConnectionState.Open)
        //{
        //}
        //else
        //{

        //    conn.Open();
        //}
        //        cmd.Connection = conn;
        //        cmd.CommandText = "select Price,Qty,R_Id from RateChange where Item_code='" + code + "'";
        //        cmd.CommandType = CommandType.Text;
        //        SqlDataAdapter Adap = new SqlDataAdapter();
        //        Adap.SelectCommand = cmd;
        //        DataTable dt = new DataTable();
        //        Adap.Fill(dt);
        //        if (dt.Rows.Count > 0)
        //        {
        //            for (int i = 0; i < dt.Rows.Count && (squantity < 0); ++i)
        //            {
        //                string price = dt.Rows[i]["Price"].ToString();
        //                Qty = dt.Rows[i]["Qty"].ToString();
        //                string rid = dt.Rows[i]["R_Id"].ToString();
        //                string tb_qty = Qty;
        //                if (Convert.ToInt32(tb_qty) != 0)
        //                {
        //                    int sumqty = Math.Abs(squantity);
        //                    QtyChecker = QtyChecker + Convert.ToInt32(Qty);
        //                    totPrice = totPrice + (Convert.ToDecimal(Qty) * Convert.ToDecimal(price));
        //                    squantity = squantity + sumqty;
        //                    string upstock = (Convert.ToInt32(sumqty) + Convert.ToInt32(Qty)).ToString();
        //                    if (Qty == "0")
        //                    {
        //                        cmd.CommandText = "UPDATE RateChange SET Qty='" + Qty + "' where R_Id='" + rid + "'";
        //                    }
        //                    else
        //                    {

        //                        cmd.CommandText = "UPDATE RateChange SET Qty='" + upstock + "' where R_Id='" + rid + "'";
        //                    }
        //                }
        //                try
        //                {
        //                    cmd.CommandType = CommandType.Text;
        //                    cmd.ExecuteNonQuery();
        //                }
        //                catch { }
        //            }
        //        }
        //    }
        //    catch
        //    {
        //    }
        //}

        private void btnSave_Click(object sender, EventArgs e)
        {
            TransDate = Convert.ToDateTime(DOC_DATE_GRE.Value.ToString());
            if (valid())
            {
                if (DialogResult.Yes == MessageBox.Show("Are you sure to return the products", "Purchase Return Alert", MessageBoxButtons.YesNo))
                {
                    if (DialogResult.Yes == MessageBox.Show("Do you Want to Generate Debit Note.?", "Credit Note Generation", MessageBoxButtons.YesNo))
                    {
                        if (CUSTOMER_CODE.Text == "")
                        {
                            MessageBox.Show("Select the Customer..!","Sysbizz",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                        }
                        else
                        {     
                            SalesReturnFunction();
                            TaxTransaction();
                            DebitNoteDB.DocNo = DOC_NO.Text;
                            if (DebitNoteDB.Existing_DebitNote())
                            {
                                crnoteno=InsertDebitNote();
                                Update_Note();
                            }
                            else
                            {
                                UpdateDebitNote();
                            } 
                            PrintingCreditNOte();
                            btnClear.PerformClick();
                        }
                    }
                    else
                    {
                        SalesReturnFunction();
                        TaxTransaction();
                        //PrintingCreditNOte();
                        btnClear.PerformClick();
                    }
                }
            }
            else
            {

            }
        }

        public void Update_Note()
        {
            string query = "Update INV_PURCHASE_HDR set DEDIT_NOTE=" + crnoteno + " where DOC_NO='" + DOC_NO.Text + "'";
            DbFunctions.InsertUpdate(query);
        }

        public void InsertIntoCreditTable()
        {
            try
            {
                string query = "insert into tbl_CreditNote(CN_Doc_No,CN_Date,CN_DateHij,CN_Reffrence_No,CUSTOMER_CODE,CUSTOMER_NAME_ENG,NOTES,CN_Balance,Nett_Amount,Status)values('" + DOC_NO.Text + "','" + DOC_DATE_GRE.Text + "','" + DOC_DATE_HIJ.Text + "','" + DOC_REFERENCE.Text + "','" + CUSTOMER_CODE.Text + "','" + CUSTOMER_NAME.Text + "','" + NOTES.Text + "','" + balance + "','" + NET_AMOUNT.Text + "','" + Retunstatus + "')";
                DbFunctions.InsertUpdate(query);

            }
            catch (Exception e)
            {
                MessageBox.Show("exception");
            }
        }

        public void SalesReturnFunction()
        {
            string status = "Added!";
            string query = "";
            if (ID == "")
            {
                txt_srno.Text = GetMaxDocID().ToString();
                DOC_NO.Text = General.generatePurchaseID();
                query = "INSERT INTO INV_PURCHASE_HDR(DOC_ID,DOC_NO,DOC_TYPE,DOC_DATE_GRE,DOC_DATE_HIJ,CURRENCY_CODE,DOC_REFERENCE,SUPPLIER_CODE,SalesMan,NOTES,TAX_TOTAL,GROSS,NET_VAL,PAY_CODE,CARD_NO,BRANCH) ";
                query += "VALUES('" + txt_srno.Text + "','" + DOC_NO.Text + "','" + type + "','" + DOC_DATE_GRE.Value.ToString("MM/dd/yyyy") + "','" + DOC_DATE_HIJ.Text + "','" + CURRENCY_CODE.SelectedValue.ToString() + "','" + txtinvoiceno.Text + "','" + CUSTOMER_CODE.Text + "','" + SALESMAN_CODE.Text + "','" + NOTES.Text + "','" + TAX_TOTAL.Text + "','" + TOTAL_AMOUNT.Text + "','" + NET_AMOUNT.Text + "','" + PAY_CODE.Text + "','" + CARD_NO.Text + "','" + lg.Branch + "');";
                query += "INSERT INTO INV_PURCHASE_DTL(DOC_ID,DOC_TYPE,DOC_NO,ITEM_CODE,ITEM_DESC_ENG,UOM,PRICE_FOB,QTY_RCVD,ITEM_DISCOUNT,BRANCH";
                // NOT VALIDATED//
                query += ",PRICE_BATCH";
                // NOT VALIDATED//
                if (hasTax)
                {
                    query += ",ITEM_TAX_PER,ITEM_TAX";
                }
                if (hasBatch)
                {
                    query += ",BATCH,EXPIRY_DATE";
                }
                query += ",  UOM_QTY, cost_price";
                query += ")";
                StockEntry stockEntry = new StockEntry();
                for (int i = 0; i < dgItems.Rows.Count; i++)
                {
                    DataGridViewCellCollection c = dgItems.Rows[i].Cells;
                    query += "SELECT '" + txt_srno.Text + "','" + type + "','" + DOC_NO.Text + "','" + c["cCode"].Value + "','" + c["cName"].Value + "','" + c["cUnit"].Value + "','" + Convert.ToDecimal(c["cPrice"].Value) + "','" + c["cQty"].Value + "','" + c["cDisc"].Value + "','" + lg.Branch + "','" + c["colBATCH"].Value.ToString() + "'";
                    if (hasTax)
                    {
                        query += ",'" + c["cTaxPer"].Value + "','" + Convert.ToDecimal(c["cTaxAmt"].Value) + "'";
                    }
                    if (hasBatch)
                    {
                        query += ",'" + c["cBatch"].Value + "','" + Convert.ToDateTime(c["cExpDate"].Value).ToString("yyyy/MM/dd") + "'";
                    }
                    query += ", '" + c["uomQty"].Value + "', '" + c["cost_price"].Value + "'";
                    query += " UNION ALL ";

                    string item_id = Convert.ToString(c["cCode"].Value);
                    double qty = Convert.ToDouble(c["cQty"].Value);
                    double uom_qty = Convert.ToDouble(c["uomQty"].Value);
                    double total_qty = (qty * uom_qty) * -1;
                    stockEntry.addStockWithBatch(item_id, total_qty.ToString(), "", Convert.ToString(c["colBATCH"].Value));
                }

                //inserting details into credit table 


            }
            else
            {

                //SqlCommand reduceStockCommand = new SqlCommand();
                ///reduceStockCommand.Connection = conn;
                //conn.Open();
                //reduceStockCommand.CommandText = "SELECT ITEM_CODE, QTY_RCVD, UOM_QTY, cost_price,PRICE_BATCH FROM INV_PURCHASE_DTL WHERE DOC_NO = '" + ID + "' AND DOC_TYPE = '" + type + "'";
                //SqlDataReader r = reduceStockCommand.ExecuteReader();
                purchDtlObj.DocNo = ID;
                purchDtlObj.DocType = type;

              //  SqlDataReader r = purchDtlObj.getItemDetails();
                //StockEntry se = new StockEntry();
                //while (r.Read())
                //{
                //    double qty = 1 * (Convert.ToDouble(r["QTY_RCVD"]) * Convert.ToDouble(r["UOM_QTY"]));
                //    se.addStockWithBatch(Convert.ToString(r["ITEM_CODE"]), Convert.ToString(qty), "", Convert.ToString(r["PRICE_BATCH"]));
                //}

                //////NOT CHECK

                //DbFunctions.CloseConnection();


                DataTable r = purchDtlObj.getItemDetailsTable();

                StockEntry se = new StockEntry();

                for (int i = 0; i < r.Rows.Count; i++)

                {
                    double qty = 1 * (Convert.ToDouble(r.Rows[i]["QTY_RCVD"]) * Convert.ToDouble(r.Rows[i]["UOM_QTY"]));
                    se.addStockWithBatch(Convert.ToString(r.Rows[i]["ITEM_CODE"]), Convert.ToString(qty), "", Convert.ToString(r.Rows[i]["PRICE_BATCH"]));
                }



                DeleteTransation();
                modifiedtransaction();
                status = "Updated!";
                query = "UPDATE  INV_PURCHASE_HDR SET  DOC_ID='" + txt_srno.Text + "',DOC_TYPE='" + type + "',DOC_DATE_GRE = '" + DOC_DATE_GRE.Value.ToString("MM/dd/yyyy") + "',DOC_DATE_HIJ = '" + DOC_DATE_HIJ.Text + "',CURRENCY_CODE= '" + CURRENCY_CODE.SelectedValue + "',DOC_REFERENCE= '" + txtinvoiceno.Text + "',SUPPLIER_CODE= '" + CUSTOMER_CODE.Text + "',SalesMan = '" + SALESMAN_CODE.Text + "',NOTES = '" + NOTES.Text + "',TAX_TOTAL= '" + TAX_TOTAL.Text + "',GROSS='" + TOTAL_AMOUNT.Text + "',NET_VAL='" + NET_AMOUNT.Text + "',PAY_CODE = '" + PAY_CODE.Text + "',CARD_NO= '" + CARD_NO.Text + "',BRANCH='" + lg.Branch + "'  WHERE DOC_NO = '" + DOC_NO.Text + "';";
                //cmd.CommandText = "UPDATE INV_SALES_HDR SET DOC_ID='" + txtinvoiceno.Text + "', DOC_TYPE = 'SAL.CSR',DOC_DATE_GRE = '" + DOC_DATE_GRE.Value.ToString("MM/dd/yyyy") + "',DOC_DATE_HIJ = '" + DOC_DATE_HIJ.Text + "',CURRENCY_CODE = '" + CURRENCY_CODE.Text + "',DOC_REFERENCE = '" + DOC_REFERENCE.Text + "',CUSTOMER_CODE = '" + CUSTOMER_CODE.Text + "',CUSTOMER_NAME_ENG = '" + CUSTOMER_NAME.Text + "',SALESMAN_CODE = '" + SALESMAN_CODE.Text + "',NOTES = '" + NOTES.Text + "',TAX_TOTAL = '" + TAX_TOTAL.Text + "',VAT = '" + VAT.Text + "',DISCOUNT = '" + DISCOUNT.Text + "',TOTAL_AMOUNT = '" + TOTAL_AMOUNT.Text + "',PAY_CODE = '" + PAY_CODE.Text + "',CARD_NO = '" + CARD_NO.Text + "' WHERE DOC_NO = '" + DOC_NO.Text + "';";
                query += "DELETE FROM INV_PURCHASE_DTL WHERE DOC_NO = '" + DOC_NO.Text + "';";
                query += "INSERT INTO INV_PURCHASE_DTL(DOC_ID,DOC_TYPE,DOC_NO,ITEM_CODE,ITEM_DESC_ENG,UOM,PRICE_FOB,QTY_RCVD,ITEM_DISCOUNT,BRANCH";
                // NOT VALIDATED//
                query += ",PRICE_BATCH";
                // NOT VALIDATED//
                if (hasTax)
                {
                    query += ",ITEM_TAX_PER,ITEM_TAX";
                }
                if (hasBatch)
                {
                    query += ",BATCH,EXPIRY_DATE";
                }
                query += ",  UOM_QTY, cost_price";
                query += ")";
                StockEntry stockEntry = new StockEntry();
                for (int i = 0; i < dgItems.Rows.Count; i++)
                {
                    DataGridViewCellCollection c = dgItems.Rows[i].Cells;
                    query += "SELECT '" + txt_srno.Text + "','" + type + "','" + DOC_NO.Text + "','" + c["cCode"].Value + "','" + c["cName"].Value + "','" + c["cUnit"].Value + "','" + Convert.ToDecimal(c["cPrice"].Value) + "','" + c["cQty"].Value + "','" + c["cDisc"].Value + "','" + lg.Branch + "','" + c["colBATCH"].Value.ToString() + "'";
                    if (hasTax)
                    {
                        query += ",'" + c["cTaxPer"].Value + "','" + Convert.ToDecimal(c["cTaxAmt"].Value) + "'";
                    }
                    if (hasBatch)
                    {
                        query += ",'" + c["cBatch"].Value + "','" + Convert.ToDateTime(c["cExpDate"].Value).ToString("yyyy/MM/dd") + "'";
                    }
                    query += ", '" + c["uomQty"].Value + "', '" + c["cost_price"].Value + "'";
                    query += " UNION ALL ";

                    string item_id = Convert.ToString(c["cCode"].Value);
                    double qty = Convert.ToDouble(c["cQty"].Value);
                    double uom_qty = Convert.ToDouble(c["uomQty"].Value);
                    double total_qty = (qty * uom_qty) * -1;
                    stockEntry.addStockWithBatch(item_id, total_qty.ToString(), "", Convert.ToString(c["colBATCH"].Value));
                }
            }
            query = query.Substring(0, query.Length - 10);
                try
                {
                    //conn.Open();
                    //cmd.CommandType = CommandType.Text;
                    //cmd.ExecuteNonQuery();
                    DbFunctions.InsertUpdate(query);

                    InsertTransaction();
                    ReturningToStock();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    //conn.Close();
                }
            
        }

        public void TaxTransaction()
        {
            if (Convert.ToDouble(TAX_TOTAL.Text) > 0)
            {
                trans.VOUCHERTYPE = "Purchase Return";
                trans.DATED = DOC_DATE_GRE.Value.ToShortDateString();
                trans.NARRATION = NOTES.Text;
                Login log = (Login)Application.OpenForms["Login"];
                trans.USERID = log.EmpId;
                trans.NARRATION = NOTES.Text;
                //trans.ACCNAME = "PR A/C";
                //trans.PARTICULARS = (dt1.AsEnumerable().FirstOrDefault(p => p["LEDGERID"].ToString() == "66")["LEDGERNAME"].ToString()); 
                //trans.ACCID = "84";
                //trans.VOUCHERNO = DOC_NO.Text;
                //trans.CREDIT = "0";
                //trans.BRANCH = lg.Branch;
                //trans.DEBIT = TAX_TOTAL.Text;
                //trans.SYSTEMTIME = DateTime.Now.ToString();
                //trans.insertTransaction();
                trans.PARTICULARS = "PR A/C";
                trans.ACCNAME = (dt1.AsEnumerable().FirstOrDefault(p => p["LEDGERID"].ToString() == "66")["LEDGERNAME"].ToString()); 
                trans.ACCID = "66";
                trans.VOUCHERNO = DOC_NO.Text;
                trans.BRANCH = lg.Branch;
                trans.DEBIT = "0";
                trans.CREDIT = TAX_TOTAL.Text;
                trans.SYSTEMTIME = DateTime.Now.ToString();
                trans.insertTransaction();
            }
        }

        public int InsertDebitNote()
        {
            DebitNoteDB.Id = DebitNoteDB.maxId();
            DebitNoteDB.DocNo = DOC_NO.Text;
            DebitNoteDB.Date = Convert.ToDateTime(DOC_DATE_GRE.Value.ToShortDateString());
            DebitNoteDB.DateHIJ = DOC_DATE_HIJ.Text;
            DebitNoteDB.Reference = DOC_REFERENCE.Text;
            DebitNoteDB.Customer = Convert.ToInt32(CUSTOMER_CODE.Text);
            DebitNoteDB.CashAccount = 0;
            DebitNoteDB.Note = NOTES.Text;
            DebitNoteDB.Amount = Convert.ToDecimal(NET_AMOUNT.Text);
            DebitNoteDB.Balance = Convert.ToDecimal(NET_AMOUNT.Text);
            DebitNoteDB.Status = true;
            DebitNoteDB.Tax = "";
            DebitNoteDB.TaxInclusive = true;
            DebitNoteDB.Remarks = NOTES.Text;
            return DebitNoteDB.Insert_DebitNote();
        }

        public void UpdateDebitNote()
        {
            DebitNoteDB.DocNo = DOC_NO.Text;
            DebitNoteDB.Date = Convert.ToDateTime(DOC_DATE_GRE.Value.ToShortDateString());
            DebitNoteDB.DateHIJ = DOC_DATE_HIJ.Text;
            DebitNoteDB.Reference = DOC_REFERENCE.Text == "" ? "0" : DOC_REFERENCE.Text;
            DebitNoteDB.Customer = Convert.ToInt32(CUSTOMER_CODE.Text);
            DebitNoteDB.CashAccount = 0;
            DebitNoteDB.Note = NOTES.Text;
            DebitNoteDB.Amount = Convert.ToDecimal(NET_AMOUNT.Text);
            DebitNoteDB.Balance = Convert.ToDecimal(NET_AMOUNT.Text);
            DebitNoteDB.Status = true;
            DebitNoteDB.Tax = "";
            DebitNoteDB.TaxInclusive = true;
            DebitNoteDB.Remarks = NOTES.Text;
            DebitNoteDB.Update_DebitNote();
        }
        public void modifiedtransaction()
        {

            modtrans.VOUCHERTYPE = "Purchase Return";
            modtrans.Date = DOC_DATE_GRE.Value.ToString("MM/dd/yyyy");
            modtrans.USERID = lg.EmpId;
            modtrans.VOUCHERNO = DOC_NO.Text;
            modtrans.NARRATION = NOTES.Text;
            modtrans.STATUS = "Update";
            modtrans.MODIFIEDDATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            modtrans.BRANCH = lg.Branch;
            modtrans.insertTransaction();
        }

        public void InsertTransaction()
        {
            trans.VOUCHERTYPE = "Purchase Return";
            trans.DATED = TransDate.ToString("MM/dd/yyyy");
            trans.NARRATION = NOTES.Text;
            Login log = (Login)Application.OpenForms["Login"];
            trans.USERID = log.EmpId;
            trans.NARRATION = NOTES.Text;

            trans.PARTICULARS = CREDITACC.Text;
            trans.ACCNAME = DEBITACC.Text;
            trans.VOUCHERNO = DOC_NO.Text;
            trans.BRANCH = lg.Branch;
            trans.ACCID = DEBITACC.SelectedValue.ToString();


            trans.DEBIT = NET_AMOUNT.Text;
            trans.CREDIT = "0";
            trans.SYSTEMTIME = DateTime.Now.ToString();
            trans.insertTransaction();



            trans.ACCNAME = CREDITACC.Text;
            trans.PARTICULARS = DEBITACC.Text;
            trans.VOUCHERNO = DOC_NO.Text;

            trans.ACCID = CREDITACC.SelectedValue.ToString();

            trans.CREDIT =(Convert.ToDecimal( NET_AMOUNT.Text)-Convert.ToDecimal(TAX_TOTAL.Text)).ToString();
            //  trans.DATED = TransDate.ToString("MM/dd/yyyy");
            trans.DEBIT = "0";
            trans.SYSTEMTIME = DateTime.Now.ToString();

            trans.BRANCH = lg.Branch;

            trans.insertTransaction();
        }

        private void DeleteTransation()
        {
            trans.VOUCHERTYPE = "Purchase Return";
            trans.VOUCHERNO = DOC_NO.Text;
            trans.DeletePurchaseTransaction();
        }

        public void ReturningToStock()
        {
            if (chkDebit.Checked)
                type = "LGR.PRT";
            else
                type = "LGR.CPR";
            string branch = ComSet.ReadBranch();
            if (STOCKID == "")
            {
                string STOCKDOC_NO = General.generateStockID();

                //   DOC_NO.Text = General.generateStockID();
                string qry1 = "INSERT INTO INV_STK_TRX_HDR(BRANCH,DOC_NO,DOC_TYPE,DOC_DATE_GRE,DOC_DATE_HIJ,DOC_REFERENCE,BRANCH_OTHER,NOTES,TAX_AMOUNT,TOTAL_AMOUNT) VALUES('" + branch + "','" + STOCKDOC_NO + "','" + type + "','" + DOC_DATE_GRE.Value.ToString("MM/dd/yyyy") + "','" + DOC_DATE_HIJ.Text + "','" + DOC_NO.Text + "','','" + NOTES.Text + "','" + TAX_TOTAL.Text + "','" + TOTAL_AMOUNT.Text + "');";
                string query = "INSERT INTO INV_STK_TRX_DTL(DOC_TYPE,DOC_NO,ITEM_CODE,ITEM_DESC_ENG,UOM,PRICE,QUANTITY";
                //--NOT VALIDATED--//
                query += ",PRICE_BATCH";
                //--NOT VALIDATED--//
                if (hasBatch)
                {
                    query += ",BATCH,EXPIRY_DATE";
                }
                if (hasTax)
                {
                    query += ",TAX_PER,TAX_AMOUNT";
                }
                query += ")";
                for (int i = 0; i < dgItems.Rows.Count; i++)
                {
                    DataGridViewCellCollection c = dgItems.Rows[i].Cells;
                    query += " SELECT '" + type + "','" + STOCKDOC_NO + "','" + c["cCode"].Value + "','" + c["cName"].Value + "','" + c["cUnit"].Value + "','" + c["cPrice"].Value + "','" + c["cQty"].Value + "','" + c["colBATCH"].Value + "'";
                    if (hasBatch)
                    {
                        query += ",'" + c["cBatch"].Value + "','" + Convert.ToDateTime(c["cExpDate"].Value).ToString("yyyy/MM/dd") + "'";
                    }
                    if (hasTax)
                    {
                        query += ",'" + c["cTaxPer"].Value + "','" + c["cTaxAmt"].Value + "'";
                    }
                    query += " UNION ALL ";
                }
                query = query.Substring(0, query.Length - 10);
                qry1 += query;
                //MessageBox.Show(cmd.CommandText);

                //cmd.ExecuteNonQuery();
                DbFunctions.InsertUpdate(qry1);

                //   MessageBox.Show("Stock Transaction Added!");

            }
            else
            {
                string qry1 = "UPDATE INV_STK_TRX_HDR SET BRANCH = '" + branch + "', DOC_DATE_GRE = '" + DOC_DATE_GRE.Value.ToString("MM/dd/yyyy") + "',DOC_DATE_HIJ = '" + DOC_DATE_HIJ.Text + "',DOC_REFERENCE = '" + DOC_NO.Text + "',BRANCH_OTHER = ' ',NOTES = '" + NOTES.Text + "',TOTAL_AMOUNT = '" + TOTAL_AMOUNT.Text + "' WHERE DOC_NO = '" + STOCKID + "';DELETE FROM INV_STK_TRX_DTL WHERE DOC_NO = '" + STOCKID + "';";
                string query = "INSERT INTO INV_STK_TRX_DTL(DOC_TYPE,DOC_NO,ITEM_CODE,ITEM_DESC_ENG,UOM,PRICE,QUANTITY";
                //--NOT VALIDATED--//
                query += ",PRICE_BATCH";
                //--NOT VALIDATED--//
                if (hasBatch)
                {
                    query += ",BATCH,EXPIRY_DATE";
                }
                if (hasTax)
                {
                    query += ",TAX_PER,TAX_AMOUNT";
                }
                query += ")";
                for (int i = 0; i < dgItems.Rows.Count; i++)
                {
                    DataGridViewCellCollection c = dgItems.Rows[i].Cells;
                    query += " SELECT '" + type + "','" + STOCKID + "','" + c["cCode"].Value + "','" + c["cName"].Value + "','" + c["cUnit"].Value + "','" + c["cPrice"].Value + "','" + c["cQty"].Value + "','" + c["colBATCH"].Value + "'";
                    if (hasBatch)
                    {
                        query += ",'" + c["cBatch"].Value + "','" + Convert.ToDateTime(c["cExpDate"].Value).ToString("yyyy/MM/dd") + "'";
                    }
                    if (hasTax)
                    {
                        query += ",'" + c["cTaxPer"].Value + "','" + c["cTaxAmt"].Value + "'";
                    }
                    query += " UNION ALL ";
                }
                query = query.Substring(0, query.Length - 10);
                qry1 += query;
                //MessageBox.Show(cmd.CommandText);

                //cmd.ExecuteNonQuery();
                DbFunctions.InsertUpdate(qry1);


                //   MessageBox.Show("Stock Transaction Updated!");
            }
        }

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

        private void QUANTITY_TextChanged(object sender, EventArgs e)
        {

        }

        private void PRICE_TextChanged(object sender, EventArgs e)
        {

        }

        private void ITEM_TAX_PER_TextChanged(object sender, EventArgs e)
        {

            //GROSS_TOTAL.Text = (total + tax).ToString();
            gettax();
        }

        private void ITEM_TAX_TextChanged(object sender, EventArgs e)
        {
            double total = 0;
            if (QUANTITY.Text.Trim() != "" && PRICE.Text.Trim() != "")
            {
                total = Convert.ToDouble(QUANTITY.Text) * Convert.ToDouble(PRICE.Text);
            }
            // string txt = (sender as KryptonTextBox).Text;
            string txt = ITEM_TAX.Text;
            if (txt.Trim() != "")
            {
                total = total + Convert.ToDouble(txt);
            }
            GROSS_TOTAL.Text = total.ToString(decimalFormat);
        }

        private void ITEM_DISCOUNT_TextChanged(object sender, EventArgs e)
        {

            if (ITEM_DISCOUNT.Text.Trim() != "" && GROSS_TOTAL.Text.Trim() != "")
            {
                ITEM_TOTAL.Text = (Convert.ToDouble(GROSS_TOTAL.Text) - (Convert.ToDouble(GROSS_TOTAL.Text) * (Convert.ToDouble(ITEM_DISCOUNT.Text) / 100))).ToString(decimalFormat);
            }

            if (ITEM_DISCOUNT.Text == "")
            {
                ITEM_TOTAL.Text = GROSS_TOTAL.Text;
            }
        }

        private void UOM_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (hasPriceBatch)
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
            else
            {
                getRate();
            }
        }

        private void getRate()
        {
            //if (conn.State == ConnectionState.Open)
            //{
            //    conn.Close();
            //}
            //conn.Open();
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.CommandText = "GET_RATE";
            //cmd.Parameters.Clear();
            //cmd.Parameters.AddWithValue("@ITEM_CODE", ITEM_CODE.Text);
            //cmd.Parameters.AddWithValue("@UNIT_CODE", UOM.Text);
            //cmd.Parameters.AddWithValue("RATE_CODE", RATE_CODE.SelectedValue);
            //string price = Convert.ToString(cmd.ExecuteScalar());
            //conn.Close();

            string cmdText = "GET_RATE1";
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ITEM_CODE", ITEM_CODE.Text);
            param.Add("@UNIT_CODE", UOM.Text);
            param.Add("RATE_CODE", RATE_CODE.SelectedValue);
            string price = Convert.ToString(DbFunctions.GetAValueProcedure(cmdText, param));
            PRICE.Text = price;
            if (!hasPurExclusive)
            {
                double taxcalc = 0;
                taxcalc = (Convert.ToDouble(ITEM_TAX_PER.Text) / 100) + 1;
                PRICE.Text = (Convert.ToDouble(PRICE.Text) / taxcalc).ToString();
            }
        }

        private void BATCH_Enter(object sender, EventArgs e)
        {
            //   assignBatch();
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

        private void BARCODE_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void BATCH_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (sender is KryptonTextBox)
                {
                    string name = (sender as KryptonTextBox).Name;
                    switch (name)
                    {
                        case "CUSTOMER_CODE":
                            if (CUSTOMER_CODE.Text == "")
                            {
                                btnCust.PerformClick();
                            }
                            else
                            {
                                BARCODE.Focus();
                            }
                            break;
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

        private void EXPIRY_DATE_KeyDown(object sender, KeyEventArgs e)
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

        private void BARCODE_KeyDown_1(object sender, KeyEventArgs e)
        {
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
                            QUANTITY.Focus();
                        }
                        addUnits();
                        UOM.Text = t.Rows[0][2].ToString();
                        TaxId = Convert.ToInt16(t.Rows[0][3].ToString());
                        GetTaxRate();
                    }
                }
            }
        }

        public int valid(String docid)
        {
            int RET = 0;
            try
            {
                string result = "";

                //if (conn.State == ConnectionState.Open)
                //{
                //}

                //else
                //{
                //    conn.Open();
                //}
                //cmd.Connection = conn;
                //cmd.CommandType = CommandType.Text;
                // cmd.CommandText = "select DOC_TYPE FROM INV_PURCHASE_DTL WHERE DOC_ID='"+docid+"'";
                // result = Convert.ToString(cmd.ExecuteScalar());

                purchdtlObj.DocId = Convert.ToInt32(docid);
                result = Convert.ToString(purchdtlObj.getDocTypeFromDocId());
                if (result == "LGR.PRT")
                {
                    RET = 1;
                }
                else
                {
                    RET = 0;
                }
                //conn.Close();
            }
            catch
            {

            }
            return RET;

        }

        private void btninvoiceno_Click(object sender, EventArgs e)
        {
            Purchase_Return_Help h = new Purchase_Return_Help(0, "LGR.CPR','LGR.PRT");
            if (h.ShowDialog() == DialogResult.OK && h.c != null)
            {

                btnClear.PerformClick();
                ReturnBillId = Convert.ToString(h.c["DOC_ID"].Value);
                int check = valid(ReturnBillId);
                if (check == 0)
                {
                    PID = txtinvoiceno.Text = ReturnBillId;
                    txtinvoiceno.Text = Convert.ToString(h.c["PUR_TYPE"].Value) + "/" + PID;
                    DOC_REFERENCE.Text = Convert.ToString(h.c["DOC_NO"].Value);
                    try
                    {
                        PDATE = Convert.ToDateTime(Convert.ToString(h.c["DOC_DATE_GRE"].Value)).ToString();
                        DOC_DATE_GRE.Value = Convert.ToDateTime(Convert.ToString(h.c["DOC_DATE_GRE"].Value));
                    }
                    catch
                    {
                    }
                    DOC_DATE_HIJ.Text = Convert.ToString(h.c["DOC_DATE_HIJ"].Value);
                    CURRENCY_CODE.Text = Convert.ToString(h.c["CURRENCY_CODE"].Value);
                    //   DOC_REFERENCE.Text = Convert.ToString(h.c["DOC_REFERENCE"].Value);
                    CUSTOMER_CODE.Text = Convert.ToString(h.c["SUPPLIER_CODE"].Value);
                    GetLedgerId(Convert.ToString(h.c["SUPPLIER_CODE"].Value));
                    //  CUSTOMER_NAME.Text = Convert.ToString(h.c["CUSTOMER_NAME_ENG"].Value);
                    SALESMAN_CODE.Text = Convert.ToString(h.c["SalesMan"].Value);
                    NOTES.Text = Convert.ToString(h.c["NOTES"].Value);
                    TAX_TOTAL.Text = Convert.ToString(h.c["TAX_TOTAL"].Value);
                    //VAT.Text = Convert.ToString(h.c["VAT"].Value);
                    //DISCOUNT.Text = Convert.ToString(h.c["DISCOUNT"].Value);
                    //   TOTAL_AMOUNT.Text = Convert.ToString(h.c["TOTAL_AMOUNT"].Value);
                    TOTAL_AMOUNT.Text = Convert.ToString(h.c["GROSS"].Value);
                    NET_AMOUNT.Text = Convert.ToString(h.c["NET_VAL"].Value);
                    PAY_CODE.Text = Convert.ToString(h.c["PAY_CODE"].Value);
                    //     PAY_NAME.Text = General.getName(PAY_CODE.Text, "GEN_PAYTYPE");
                    CARD_NO.Text = Convert.ToString(h.c["CARD_NO"].Value);

                    //cmd.CommandText = "SELECT dtl.*,INV_PURCHASE_HDR.SUPPLIER_CODE FROM INV_PURCHASE_DTL as dtl LEFT JOIN INV_PURCHASE_HDR ON dtl.DOC_NO=INV_PURCHASE_HDR.DOC_NO WHERE dtl.DOC_NO ='" + DOC_REFERENCE.Text + "'AND dtl.FLAGDEL='TRUE'";
                    //cmd.CommandType = CommandType.Text;
                    //SqlDataReader r = cmd.ExecuteReader();
                    purchdtlObj.DocNo = DOC_REFERENCE.Text;
                    DataTable r = purchdtlObj.getDetails();
                    for (int j = 0; j < r.Rows.Count; j++)
                    {
                        int i = dgItems.Rows.Add(new DataGridViewRow());
                        DataGridViewCellCollection c = dgItems.Rows[i].Cells;
                        c["cCode"].Value = r.Rows[j]["ITEM_CODE"];
                        c["cName"].Value = r.Rows[j]["ITEM_DESC_ENG"];
                        if (hasBatch)
                        {
                            c["cBatch"].Value = r.Rows[j]["BATCH"];
                            //c["cExpDate"].Value = Convert.ToDateTime(r["EXPIRY_DATE"]).ToString("dd/MM/yyyy");
                            try
                            {
                                c["cExpDate"].Value = Convert.ToDateTime(r.Rows[j]["EXPIRY_DATE"]).ToString();
                            }

                            catch
                            {
                            }
                        }
                        c["cUnit"].Value = r.Rows[j]["UOM"];
                        c["cQty"].Value = r.Rows[j]["QTY_RCVD"];
                        c["cPrice"].Value = r.Rows[j]["PRICE_FOB"];
                        if (hasTax)
                        {
                            if (r.Rows[j]["ITEM_TAX_PER"].ToString() == "")
                            {
                                c["cTaxPer"].Value = "0";
                            }
                            else
                            {
                                c["cTaxPer"].Value = r.Rows[j]["ITEM_TAX_PER"];
                            }

                            if (r.Rows[j]["ITEM_TAX"].ToString() == "")
                            {
                                c["cTaxAmt"].Value = "0";
                            }
                            else
                            {
                                c["cTaxAmt"].Value = r.Rows[j]["ITEM_TAX"];
                            }
                        }
                        else
                        {
                            c["cTaxPer"].Value = "0";
                            c["cTaxAmt"].Value = "0";

                        }
                        if (r.Rows[j]["ITEM_GROSS"].ToString() == "")
                            c["cGTotal"].Value = "0.00";
                        else
                            c["cGTotal"].Value = r.Rows[j]["ITEM_GROSS"];

                        c["cDisc"].Value = r.Rows[j]["ITEM_DISCOUNT"];
                        if (r.Rows[j]["NET_AMOUNT"].ToString() == "")
                            c["cTotal"].Value = "0.00";
                        else
                            c["cTotal"].Value = r.Rows[j]["NET_AMOUNT"];
                        DOC_DATE_GRE.Value = DateTime.Now;

                        c["uomQty"].Value = r.Rows[j]["UOM_QTY"];
                        c["cost_price"].Value = r.Rows[j]["cost_price"];

                        c["supplier_id"].Value = r.Rows[j]["SUPPLIER_CODE"];
                        //c["supplier_name"].Value = r["supplier_name"];
                        if (r.Rows[j]["PRICE_BATCH"].ToString() != null)
                        {
                            c["colBATCH"].Value = r.Rows[j]["PRICE_BATCH"];
                        }
                        //  c["supplier_name"].Value = r["supplier_name"];
                    }
                    DbFunctions.CloseConnection();
                    GETLEDGERDETAILS();
                }
                else
                {
                    MessageBox.Show("alredy Returned Invoice " + ReturnBillId);
                }
            }
        }
        public void GETLEDGERDETAILS()
        {
            purchHdrObj.SupplierCode = CUSTOMER_CODE.Text;
            if (ID == "")
                purchHdrObj.DocNo = DOC_REFERENCE.Text;
            else
                purchHdrObj.DocNo = ID;
            SqlDataReader r = purchHdrObj.getSupLedId();
            while (r.Read())
            {
                CUSTOMER_NAME.Text = Convert.ToString(r[0]);
                DEBITACC.SelectedValue = r[1];

            }
            DbFunctions.CloseConnection();

            //to get purchase Type


            string docType = purchHdrObj.getDocType();
            if (docType == "PUR.CSS")
            {
                chkDebit.Checked = true;
                DEBITACC.SelectedValue = 21;
            }
            else
            {
                chkCredit.Checked = true;
            }


            /*adapter.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                txtSupplierName.Text = dt1.Rows[0][0].ToString();
                drpdebitor.SelectedValue = Convert.ToInt32(dt1.Rows[0][1]);
            }*/
        }
        private void txtinvoiceno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                try
                {
                    FindingInvoiceNo();
                }
                catch
                {
                }
            }
        }

        public void FindingInvoiceNo()
        {
            DataTable dt = new DataTable();
            try
            {

                SqlDataAdapter adapter = new SqlDataAdapter();
                ReturnBillId = txtinvoiceno.Text;
                btnClear.PerformClick();
                txtinvoiceno.Text = ReturnBillId;
                //cmd.CommandText = "select * from INV_PURCHASE_HDR WHERE DOC_ID=" + ReturnBillId;
                //adapter.Fill(dt);
                dt = purchHdrObj.getDetailsFromReturnBillId();
            }
            catch (Exception e)
            {
                //  conn.Close();
                MessageBox.Show(e.Message);
            }
            if (dt.Rows.Count > 0)
            {

                //   DOC_REFERENCE.Text = DOC_NO.Text;
                try
                {
                    DOC_DATE_GRE.Value = Convert.ToDateTime(Convert.ToString(dt.Rows[0]["DOC_DATE_GRE"]));
                }
                catch
                {
                }
                DOC_REFERENCE.Text = Convert.ToString(dt.Rows[0]["DOC_NO"]);
                DOC_DATE_HIJ.Text = Convert.ToString(dt.Rows[0]["DOC_DATE_HIJ"]);
                CURRENCY_CODE.Text = Convert.ToString(dt.Rows[0]["CURRENCY_CODE"]);
                //   DOC_REFERENCE.Text = Convert.ToString(dt.Rows[0]["DOC_REFERENCE"]);
                CUSTOMER_CODE.Text = Convert.ToString(dt.Rows[0]["SUPPLIER_CODE"]);
                GetLedgerId(Convert.ToString(dt.Rows[0]["SUPPLIER_CODE"]));
                //  CUSTOMER_NAME.Text = Convert.ToString(dt.Rows[0]["CUSTOMER_NAME_ENG"]);
                SALESMAN_CODE.Text = Convert.ToString(dt.Rows[0]["SalesMan"]);
                NOTES.Text = Convert.ToString(dt.Rows[0]["NOTES"]);
                TAX_TOTAL.Text = Convert.ToString(dt.Rows[0]["TAX_TOTAL"]);
                // VAT.Text = Convert.ToString(dt.Rows[0]["VAT"]);
                // DISCOUNT.Text = Convert.ToString(dt.Rows[0]["DISCOUNT"]);
                TOTAL_AMOUNT.Text = Convert.ToString(dt.Rows[0]["GROSS"]);
                NET_AMOUNT.Text = Convert.ToString(dt.Rows[0]["NET_VAL"]);
                PAY_CODE.Text = Convert.ToString(dt.Rows[0]["PAY_CODE"]);
                // PAY_NAME.Text = General.getName(PAY_CODE.Text, "GEN_PAYTYPE");
                CARD_NO.Text = Convert.ToString(dt.Rows[0]["CARD_NO"]);

                //conn.Open();
                //cmd.CommandText = "SELECT * FROM INV_SALES_DTL WHERE DOC_NO = '" + DOC_REFERENCE.Text+ "'";
                //cmd.CommandType = CommandType.Text;
                //SqlDataReader r = cmd.ExecuteReader();
                //while (r.Read())
                //{
                //    int i = dgItems.Rows.Add(new DataGridViewRow());
                //    DataGridViewCellCollection c = dgItems.Rows[i].Cells;
                //    c["cCode"].Value = r["ITEM_CODE"];
                //    c["cName"].Value = r["ITEM_DESC_ENG"];
                //    if (hasBatch)
                //    {
                //        c["cBatch"].Value = r["BATCH"];
                //        //c["cExpDate"].Value = Convert.ToDateTime(r["EXPIRY_DATE"]).ToString("dd/MM/yyyy");
                //        try
                //        {
                //            c["cExpDate"].Value = Convert.ToDateTime(r["EXPIRY_DATE"]).ToString();
                //        }

                //        catch
                //        {
                //        }
                //    }
                //    c["cUnit"].Value = r["UOM"];
                //    c["cQty"].Value = r["QUANTITY"];
                //    c["cPrice"].Value = r["PRICE"];
                //    if (hasTax)
                //    {
                //        c["cTaxPer"].Value = r["ITEM_TAX_PER"];
                //        c["cTaxAmt"].Value = r["ITEM_TAX"];
                //    }
                //    c["cGTotal"].Value = r["GROSS_TOTAL"];
                //    c["cDisc"].Value = r["ITEM_DISCOUNT"];
                //    c["cTotal"].Value = r["ITEM_TOTAL"];
                //}
                //conn.Close();

            }
            else
            {
                MessageBox.Show("No Such Inovice No exists Please Try again");
                txtinvoiceno.Text = "";
                txtinvoiceno.Focus();
            }
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {

        }

        public void PrintingCreditNOte()
        {
            trate = 0; ttaxva = 0; tgrossrate = 0;
            try
            {
                GetCompanyDetails();
                GetBranchDetails();
                try
                {

                    int height = (dgItems.Rows.Count - 1) * 23;
                    if (PrintPage.SelectedIndex == 0)
                    {
                        printeditems = 0;
                        k = 0;
                        tqty = 0; trate = 0; ttaxva = 0; tgrossrate = 0;
                        tgrossrate = 0; ttaxva = 0; trate = 0; tcdis = 0; ttaxbl = 0; tfree = 0;
                        PrintDocument printDocument = new PrintDocument();
                        printDocument.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("GST Half", 840, 560);

                        printDialog1.Document = printDocument;
                        printDocument.PrintPage += Print_GSTHALF;
                        printDocument.Print();
                    }
                    if (PrintPage.SelectedIndex == 1)
                    {
                        printeditems = 0;
                        k = 0;
                        tqty = 0; trate = 0; ttaxva = 0; tgrossrate = 0;
                        tgrossrate = 0; ttaxva = 0; trate = 0; tcdis = 0; ttaxbl = 0; tfree = 0;
                        PrintDocument printDocument = new PrintDocument();
                        printDocument.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("GST Half", 840, 560);

                        printDialog1.Document = printDocument;
                        printDocument.PrintPage += Print_GSTHALF;
                        printDocument.Print();
                    }
                    else if (PrintPage.SelectedIndex == 2)
                    {
                        printeditems = 0;
                        k = 0;
                        tqty = 0; trate = 0; ttaxva = 0; tgrossrate = 0;
                        tgrossrate = 0; ttaxva = 0; trate = 0; tcdis = 0; ttaxbl = 0; tfree = 0;
                        PrintDocument printdocumentA4 = new PrintDocument();
                        PaperSize ps = new PaperSize();
                        ps.RawKind = (int)PaperKind.A4;
                        printdocumentA4.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("GST Half", 840, 560);
                        printDialog1.Document = printdocumentA4;
                        printdocumentA4.PrintPage += Print_GSTHALF;
                        printdocumentA4.Print();
                    }
                }
                catch
                {
                    MessageBox.Show("printing Problem");
                }

            }
            catch
            {
            }

        }
        string Gst1, Gst2;
        decimal tqty;
        void Print_GSTHALF(object sender, PrintPageEventArgs e)
        {
            Company company = Common.getCompany();
            bool PRINTTOTALPAGE = true;
            bool hasmorepages = false;
            float xpos;
            int startx = 50;
            int starty = 20;
            int offset = 15;
            int headerstartposition = 50;
            int startX = 5;
            int startY = 10;
            int w = e.MarginBounds.Width / 2;
            int x = e.MarginBounds.Left;
            int y = e.MarginBounds.Top;
            Font Headerfont1 = new Font("Calibri", 15, FontStyle.Bold);
            Font dec = new Font("Calibri", 8, FontStyle.Regular);
            Font Headerfont2 = new Font("Calibri", 10, FontStyle.Regular);
            Font number = new Font("Calibri", 8, FontStyle.Regular);
            Font printFont = new Font("Calibri", 10);
            Font printbold = new Font("Calibri", 10, FontStyle.Bold);
            Font printbold1 = new Font("Calibri", 11, FontStyle.Bold);

            Font printnet = new Font("Calibri", 11, FontStyle.Bold);
            var tabDataForeColor = Color.Black;
            int height = 100 + y;

            Font FONTHEAD = new Font("Arial Black", 12, FontStyle.Bold);
            Font FONTHEAD1 = new Font("Arial Black", 9, FontStyle.Bold);
            Font FONTHEAD2 = new Font("Arial Black", 8, FontStyle.Bold);
            Font FONTGST = new Font("Arial Unicode MS", 11, FontStyle.Bold);
            Font FONTGST1 = new Font("Arial Unicode MS", 9, FontStyle.Bold);
            var txtDataWidth = e.Graphics.MeasureString(CompanyName, Headerfont1).Width;

            Pen blackPen = new Pen(Color.Black, 1);
            using (var sf = new StringFormat())
            {
                height += 15;
                sf.LineAlignment = StringAlignment.Center;
                sf.Alignment = StringAlignment.Center;

                xpos = e.MarginBounds.Left + (e.MarginBounds.Width / 2);


                offset = offset + 30;
                // e.Graphics.DrawString(Address1 + "," + Address2, Headerfont2, new SolidBrush(Color.Black), headerstartposition, starty + 20 + offset);
                offset = offset + 12;
                string tin_no = "";
                offset = offset + 16;
                offset = 15;
                Gst1 = company.TIN_No == "" ? "32" : company.TIN_No.Substring(0, 2);

                offset = offset + 16;
                e.Graphics.DrawString("DEBIT NOTE", FONTHEAD, new SolidBrush(Color.Black), 360, startY + 15);
                e.Graphics.DrawString(company.Name, FONTHEAD1, new SolidBrush(Color.Black), startX, startY);
                e.Graphics.DrawString(company.Address, FONTHEAD2, new SolidBrush(Color.Black), startX, startY + 15);
                e.Graphics.DrawString(company.Phone, FONTHEAD2, new SolidBrush(Color.Black), startX, startY + 30);
                e.Graphics.DrawString("GSTIN:" + company.TIN_No, FONTHEAD1, new SolidBrush(Color.Black), startX, startY + 45);
                e.Graphics.DrawString("Dbt.Note No", Headerfont2, new SolidBrush(Color.Black), 550, startY + 10);
                e.Graphics.DrawString(":" + "" +crnoteno, Headerfont2, new SolidBrush(Color.Black), 620, startY + 10);
                offset = offset + 16;
                e.Graphics.DrawString("Date", Headerfont2, new SolidBrush(Color.Black), 550, startY + 25);
                e.Graphics.DrawString(":" + DOC_DATE_GRE.Value.ToShortDateString(), Headerfont2, new SolidBrush(Color.Black), 620, startY + 25);

                startY += 90;

                e.Graphics.DrawRectangle(blackPen, startX, startY, 780, 450);
                DataTable table = Common.getSupplyer(CUSTOMER_CODE.Text);
                Font font12 = new Font("Calibri", 9);
                e.Graphics.DrawString("Name", font12, new SolidBrush(Color.Black), startX, startY + 1);
                e.Graphics.DrawString("Address", font12, new SolidBrush(Color.Black), startX, startY + 12);
                e.Graphics.DrawString("Mob", font12, new SolidBrush(Color.Black), startX, startY + 22);
                e.Graphics.DrawString("GSTIN", font12, new SolidBrush(Color.Black), startX, startY + 32);
                e.Graphics.DrawString("State", font12, new SolidBrush(Color.Black), startX, startY + 42);
                if (table.Rows.Count > 0)
                {
                    tin_no = table.Rows[0]["TIN_NO"].ToString();
                    e.Graphics.DrawString(":" + table.Rows[0]["DESC_ENG"].ToString(), font12, new SolidBrush(Color.Black), startX + 60, startY + 1);
                    e.Graphics.DrawString(":" + table.Rows[0]["ADDRESS_A"].ToString(), font12, new SolidBrush(Color.Black), startX + 60, startY + 12);
                    e.Graphics.DrawString(":" + table.Rows[0]["MOBILE"].ToString(), font12, new SolidBrush(Color.Black), startX + 60, startY + 22);
                    e.Graphics.DrawString(":" + table.Rows[0]["TIN_NO"].ToString(), font12, new SolidBrush(Color.Black), startX + 60, startY + 32);
                    e.Graphics.DrawString(":" + table.Rows[0]["STATE"].ToString(), font12, new SolidBrush(Color.Black), startX + 60, startY + 42);
                }
                else
                {
                    e.Graphics.DrawString(":" + CUSTOMER_NAME.Text.ToString(), font12, new SolidBrush(Color.Black), startX + 60, startY + 1);
                    e.Graphics.DrawString(":" + "", font12, new SolidBrush(Color.Black), startX + 60, startY + 12);
                }

                if (tin_no.Length > 0)
                {
                    Gst2 = tin_no.Substring(0, 2);
                }
                else
                {
                    Gst2 = Gst1;
                }

                e.Graphics.DrawString("Ref. Invoice No", Headerfont2, new SolidBrush(Color.Black), startX + 450, startY + 1);
                e.Graphics.DrawString(":" + txtinvoiceno.Text, Headerfont2, new SolidBrush(Color.Black), startX + 610, startY + 1);
                e.Graphics.DrawString("Date", Headerfont2, new SolidBrush(Color.Black), startX + 450, startY + 14);
                try
                {
                    e.Graphics.DrawString(":" + Convert.ToDateTime(PDATE).ToShortDateString(), Headerfont2, new SolidBrush(Color.Black), startX + 610, startY + 14);
                }
                catch
                {
                    e.Graphics.DrawString(":", Headerfont2, new SolidBrush(Color.Black), startX + 610, startY + 14);
                }//e.Graphics.DrawString("Dispatch Doc No & Date", Headerfont2, new SolidBrush(Color.Black), startX + 450, startY + 26);
                //e.Graphics.DrawString(":", Headerfont2, new SolidBrush(Color.Black), startX + 610, startY + 26);
                e.Graphics.DrawString("Terms of Delivery if any", Headerfont2, new SolidBrush(Color.Black), startX + 450, startY + 42);
                e.Graphics.DrawString(":", Headerfont2, new SolidBrush(Color.Black), startX + 610, startY + 42);
                startY += 60;
                e.Graphics.DrawRectangle(blackPen, startX, startY, 780, 280);
                e.Graphics.DrawLine(blackPen, startX, startY + 260, startX + 780, startY + 260);
                e.Graphics.DrawLine(blackPen, startX, startY + 300, startX + 780, startY + 300);
                //   e.Graphics.DrawLine(blackPen, startX, startY + 100, 800 + startX, startY + 100);
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
                string headtext = "";
                if (Gst1 == Gst2)
                {
                    headtext = "Sl.No".PadRight(10) + "Item".PadRight(88) + "HSN".PadRight(10) + "Unit".PadRight(8) + "Qty".PadRight(10) + "Rate".PadRight(11) + "Disc".PadRight(14) + "CGST".PadRight(21) + "SGST".PadRight(17) + "Total";
                }
                else
                {
                    headtext = "Sl.No".PadRight(10) + "Item".PadRight(88) + "HSN".PadRight(10) + "Unit".PadRight(8) + "Qty".PadRight(10) + "Rate".PadRight(11) + "Disc".PadRight(14) + "IGST".PadRight(21) + "-----".PadRight(17) + "Total";
                }

                e.Graphics.DrawString(headtext, printbold, new SolidBrush(Color.Black), startX, startY);
                startY += 25;
                //Column head Hline
                Point point1 = new Point(startX, startY);
                Point point2 = new Point(startX + 780, startY);
                e.Graphics.DrawLine(blackPen, point1, point2);





                Font itemhead = new Font("Times New Roman", 8);

                //sl_No

                e.Graphics.DrawLine(blackPen, startX + 35, startY - 25, startX + 35, startY + 255);
                //item
                e.Graphics.DrawLine(blackPen, startX + 340, startY - 25, startX + 340, startY + 255);
                //HSN 
                e.Graphics.DrawLine(blackPen, startX + 390, startY - 25, startX + 390, startY + 255);
                //uom
                e.Graphics.DrawLine(blackPen, startX + 420, startY - 25, startX + 420, startY + 255);
                //qty
                e.Graphics.DrawLine(blackPen, startX + 460, startY - 25, startX + 460, startY + 255);
                //RATE
                e.Graphics.DrawLine(blackPen, startX + 520, startY - 25, startX + 520, startY + 255);

                e.Graphics.DrawLine(blackPen, startX + 520, startY + 275, startX + 520, 550);
                //DISC
                e.Graphics.DrawLine(blackPen, startX + 560, startY - 25, startX + 560, startY + 255);
                //CGST
                e.Graphics.DrawLine(blackPen, startX + 640, startY - 25, startX + 640, startY + 255);
                //SGST
                e.Graphics.DrawLine(blackPen, startX + 720, startY - 25, startX + 720, startY + 255);
                //Remarks
                e.Graphics.DrawString("Remarks: " + NOTES.Text, font12, new SolidBrush(Color.Black), startX, startY + 258);


                int printpoint = 50; //32
                offset = offset + 35;
                startY += 3;
                Font font = new Font("Calibri", 8);
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
                            if (nooflines < 14)
                            {

                                string period, periodtype, tax;
                                k = k + 1;
                                int ORGLENGTH = row.Cells["cName"].Value.ToString().Length;
                                string name = row.Cells["cName"].Value.ToString().Length <= 50 ? row.Cells["cName"].Value.ToString() : row.Cells["cName"].Value.ToString().Substring(0, 50);
                                string name2 = "";
                                int BALANCELENGH = ORGLENGTH - 50;
                                string qty = row.Cells["cQty"].Value.ToString();
                                string rate = row.Cells["cPrice"].Value.ToString();
                                string gross = row.Cells["cGTotal"].Value.ToString();
                                string uom = row.Cells["cUnit"].Value.ToString();
                                string HSN = "";
                                if (row.Cells["uHSNNO"].Value != null)
                                {
                                    HSN = row.Cells["uHSNNO"].Value.ToString();
                                }
                                else
                                {
                                    HSN = "";
                                }
                                decimal TaxPer = Convert.ToDecimal(row.Cells["cTaxPer"].Value);
                                decimal TaxVal = Convert.ToDecimal(row.Cells["cTaxAmt"].Value);
                                string TotalAmt = row.Cells["cTotal"].Value.ToString();
                                string Disc = row.Cells["cDisc"].Value.ToString();
                                tqty = tqty + Convert.ToDecimal(qty);
                                trate = trate + Convert.ToDecimal(rate);
                                ttaxva += Convert.ToDecimal(qty) * Convert.ToDecimal(rate);
                                tcdis = tcdis + Convert.ToDecimal(row.Cells["cDisc"].Value.ToString());

                                //to add % in taxpercentage
                                if ((TaxPer / 2) % 1 > 0)
                                    tax = (TaxPer / 2).ToString();
                                else
                                    tax = Convert.ToInt16(TaxPer / 2).ToString();

                                e.Graphics.DrawString(k.ToString(), font, new SolidBrush(Color.Black), startX, startY);
                                e.Graphics.DrawString(name, font, new SolidBrush(Color.Black), startX + 35, startY);
                                e.Graphics.DrawString(HSN, font, new SolidBrush(Color.Black), startX + 340, startY);
                                e.Graphics.DrawString(uom, font, new SolidBrush(Color.Black), startX + 390, startY);


                                StringFormat format = new StringFormat(StringFormatFlags.DirectionRightToLeft);
                                e.Graphics.DrawString(Convert.ToDecimal(qty).ToString(decimalFormat), font, new SolidBrush(Color.Black), startX + 460, startY, format);
                                e.Graphics.DrawString(Convert.ToDecimal(rate).ToString(decimalFormat), font, new SolidBrush(Color.Black), startX + 520, startY, format);
                                e.Graphics.DrawString(Convert.ToDecimal(Disc).ToString(decimalFormat), font, new SolidBrush(Color.Black), startX + 560, startY, format);

                                if (Gst1 == Gst2)
                                {
                                    e.Graphics.DrawString(tax + "%", font, new SolidBrush(Color.Black), startX + 560, startY);
                                    e.Graphics.DrawString((TaxVal / 2).ToString(decimalFormat), font, new SolidBrush(Color.Black), startX + 640, startY, format);
                                    e.Graphics.DrawString(tax + "%", font, new SolidBrush(Color.Black), startX + 640, startY);
                                    e.Graphics.DrawString((TaxVal / 2).ToString(decimalFormat), font, new SolidBrush(Color.Black), startX + 720, startY, format);
                                }
                                else
                                {
                                    e.Graphics.DrawString(TaxPer + "%", font, new SolidBrush(Color.Black), startX + 560, startY);
                                    e.Graphics.DrawString((TaxVal).ToString(decimalFormat), font, new SolidBrush(Color.Black), startX + 640, startY, format);
                                }
                                e.Graphics.DrawString(Convert.ToDecimal(TotalAmt).ToString(decimalFormat), font, new SolidBrush(Color.Black), startX + 780, startY, format);

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
                                while (BALANCELENGH > 1)
                                {
                                    startY += (int)fontheight;
                                    name2 = BALANCELENGH <= 50 ? row.Cells["cName"].Value.ToString().Substring(printpoint, BALANCELENGH) : row.Cells["cName"].Value.ToString().Substring(printpoint, 50);
                                    e.Graphics.DrawString(name2, font, new SolidBrush(Color.Black), startX + 35, startY);
                                    BALANCELENGH = BALANCELENGH - 50;
                                    printpoint = printpoint + 50;
                                    // startY = startY + (int)fontheight;
                                }
                                printpoint = 50;
                                j++;
                                startY += (int)fontheight + 2;
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
            startY = 460;
            float newoffset = 460;



            if (!PRINTTOTALPAGE)
            {
                PAGETOTAL = true;
                if (PAGETOTAL)
                {
                    try
                    {
                        decimal TotalDisc = Convert.ToDecimal(DISCOUNT.Text);
                        StringFormat format1 = new StringFormat(StringFormatFlags.DirectionRightToLeft);
                        e.Graphics.DrawString("Total Amount Before Tax", printbold, new SolidBrush(Color.Black), startX + 520, startY + 3);
                        e.Graphics.DrawString(ttaxva.ToString(decimalFormat), printbold, new SolidBrush(Color.Black), startX + 780, startY + 3, format1);
                        e.Graphics.DrawLine(blackPen, startX + 520, startY + 20, startX + 780, startY + 20);
                        e.Graphics.DrawString("Tax Amount: GST", printbold, new SolidBrush(Color.Black), startX + 520, startY + 23);
                        e.Graphics.DrawString(Convert.ToDecimal(TAX_TOTAL.Text).ToString(decimalFormat), printbold, new SolidBrush(Color.Black), startX + 780, startY + 23, format1);
                        e.Graphics.DrawLine(blackPen, startX + 520, startY + 40, startX + 780, startY + 40);
                        e.Graphics.DrawString("Total Discount", printbold, new SolidBrush(Color.Black), startX + 520, startY + 43);
                        e.Graphics.DrawString(TotalDisc.ToString(decimalFormat), printbold, new SolidBrush(Color.Black), startX + 780, startY + 43, format1);
                        e.Graphics.DrawLine(blackPen, startX + 520, startY + 60, startX + 780, startY + 60);
                        //    e.Graphics.DrawString("Total Amount After Tax", printbold, new SolidBrush(Color.Black), startX + 520, startY + 63);
                        e.Graphics.DrawString(Convert.ToDecimal(NET_AMOUNT.Text).ToString(decimalFormat), printbold1, new SolidBrush(Color.Black), startX + 780, startY + 63, format1);
                        //   e.Graphics.DrawLine(blackPen, startX + 520, startY + 80, startX + 780, startY + 80);
                        e.Graphics.DrawString("Grand Total", printbold, new SolidBrush(Color.Black), startX + 520, startY + 66);
                        e.Graphics.DrawString("Authorized Signatory", number, new SolidBrush(Color.Black), startX + 383, startY + 65);
                        e.Graphics.DrawString("[With Status and Seal]", number, new SolidBrush(Color.Black), startX + 380, startY + 74);

                        //CGST IGST SGST.............
                        e.Graphics.DrawLine(blackPen, startX + 100, startY + 28, startX + 350, startY + 28); //TOP

                        e.Graphics.DrawLine(blackPen, startX + 100, startY + 85, startX + 350, startY + 85); //down

                        e.Graphics.DrawLine(blackPen, startX + 100, startY + 28, startX + 100, startY + 85); //left side

                        e.Graphics.DrawLine(blackPen, startX + 183, startY + 28, startX + 183, startY + 85); // 1 Ve

                        e.Graphics.DrawLine(blackPen, startX + 266, startY + 28, startX + 266, startY + 85); // 2 Ve

                        e.Graphics.DrawLine(blackPen, startX + 100, startY + 56, startX + 350, startY + 56); //1-Ho

                        e.Graphics.DrawLine(blackPen, startX + 350, startY + 28, startX + 350, startY + 85); //right side


                        e.Graphics.DrawLine(blackPen, startX + 375, startY, startX + 375, startY + 90); // Authorised Line
                        e.Graphics.DrawLine(blackPen, startX + 700, startY, startX + 700, startY + 90); // Authorised Line

                        //TAX DATA

                        e.Graphics.DrawString("CGST", printbold, new SolidBrush(Color.Black), startX + 103, startY + 28);
                        e.Graphics.DrawString("SGST", printbold, new SolidBrush(Color.Black), startX + 186, startY + 28);
                        e.Graphics.DrawString("IGST", printbold, new SolidBrush(Color.Black), startX + 266, startY + 28);

                        if (Gst1 == Gst2)
                        {
                            e.Graphics.DrawString((Convert.ToDecimal(TAX_TOTAL.Text) / 2).ToString(), printbold, new SolidBrush(Color.Black), startX + 103, startY + 60);
                            e.Graphics.DrawString((Convert.ToDecimal(TAX_TOTAL.Text) / 2).ToString(), printbold, new SolidBrush(Color.Black), startX + 186, startY + 60);
                            e.Graphics.DrawString("0.00", printbold, new SolidBrush(Color.Black), startX + 266, startY + 60);

                        }
                        else
                        {
                            e.Graphics.DrawString("0.00", printbold, new SolidBrush(Color.Black), startX + 103, startY + 60);
                            e.Graphics.DrawString("0.00", printbold, new SolidBrush(Color.Black), startX + 186, startY + 60);
                            e.Graphics.DrawString((Convert.ToDecimal(TAX_TOTAL.Text)).ToString(), printbold, new SolidBrush(Color.Black), startX + 266, startY + 60);
                        }


                        Font font11 = new Font("Calibri", 8);
                        StringFormat format = new StringFormat(StringFormatFlags.DirectionRightToLeft);
                        e.Graphics.DrawString("Total", font11, new SolidBrush(Color.Black), startx + 300, 420);
                        e.Graphics.DrawString(Convert.ToDecimal(tqty).ToString(decimalFormat), font11, new SolidBrush(Color.Black), startX + 460, 420, format);
                        if (Gst1 == Gst2)
                        {
                            e.Graphics.DrawString((Convert.ToDecimal(TAX_TOTAL.Text) / 2).ToString(decimalFormat), font11, new SolidBrush(Color.Black), startX + 640, 420, format);
                            e.Graphics.DrawString((Convert.ToDecimal(TAX_TOTAL.Text) / 2).ToString(decimalFormat), font11, new SolidBrush(Color.Black), startX + 720, 420, format);
                        }
                        else
                        {
                            e.Graphics.DrawString((Convert.ToDecimal(TAX_TOTAL.Text)).ToString(decimalFormat), font11, new SolidBrush(Color.Black), startX + 640, 420, format);
                            e.Graphics.DrawString((Convert.ToDecimal("0.00")).ToString(decimalFormat), font11, new SolidBrush(Color.Black), startX + 720, 420, format);
                        }
                        e.Graphics.DrawString((Convert.ToDecimal(NET_AMOUNT.Text).ToString(decimalFormat)), font11, new SolidBrush(Color.Black), startX + 780, 420, format);
                        string test = Spell.SpellAmount.InWrods(Convert.ToDecimal(NET_AMOUNT.Text));
                        int index = test.IndexOf("Rupees");
                        int l = test.Length;
                        test = test.Substring(index + 5);
                        //e.Graphics.DrawString("Terms & conditions:", Headerfont2, new SolidBrush(Color.Black), startX, startY + 22);
                        e.Graphics.DrawString("Amount in words:", font11, new SolidBrush(Color.Black), startX, startY + 3);
                        e.Graphics.DrawString(test, new Font("Calibri", 8, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + 14);
                        printeditems = 0;
                        k = 0;
                        tcdis = 0;

                    }
                    catch (Exception ex)
                    {
                        string cc = "" + ex;
                    }
                }

                PAGETOTAL = false;
            }
            e.HasMorePages = hasmorepages;
        }


        /*  void Print_GSTHALF(object sender, PrintPageEventArgs e)
          {
              Company company = Common.getCompany();
              bool PRINTTOTALPAGE = true;
              bool hasmorepages = false;
              float xpos;
              int startx = 50;
              int starty = 20;
              int offset = 15;
              int headerstartposition = 50;
              int startX = 5;
              int startY = 10;
              int w = e.MarginBounds.Width / 2;
              int x = e.MarginBounds.Left;
              int y = e.MarginBounds.Top;
              Font Headerfont1 = new Font("Calibri", 15, FontStyle.Bold);
              Font dec = new Font("Calibri", 8, FontStyle.Regular);
              Font Headerfont2 = new Font("Calibri", 10, FontStyle.Regular);
              Font number = new Font("Calibri", 8, FontStyle.Regular);
              Font printFont = new Font("Calibri", 10);
              Font printbold = new Font("Calibri", 10, FontStyle.Bold);
              Font printbold1 = new Font("Calibri", 11, FontStyle.Bold);
              string Gst1, Gst2;
              Font printnet = new Font("Calibri", 11, FontStyle.Bold);
              var tabDataForeColor = Color.Black;
              int height = 100 + y;
              decimal tgrossrate, ttaxva, trate;
              decimal tqty;
              tqty = 0; trate = 0; ttaxva = 0; tgrossrate = 0;
              Font FONTHEAD = new Font("Arial Black", 12, FontStyle.Bold);
              Font FONTHEAD1 = new Font("Arial Black", 9, FontStyle.Bold);
              Font FONTHEAD2 = new Font("Arial Black", 8, FontStyle.Bold);
              Font FONTGST = new Font("Arial Unicode MS", 11, FontStyle.Bold);
              Font FONTGST1 = new Font("Arial Unicode MS", 9, FontStyle.Bold);
              var txtDataWidth = e.Graphics.MeasureString(CompanyName, Headerfont1).Width;
              Gst1 = company.TIN_No.Substring(0, 2);
              Pen blackPen = new Pen(Color.Black, 1);
              using (var sf = new StringFormat())
              {
                  height += 15;
                  sf.LineAlignment = StringAlignment.Center;
                  sf.Alignment = StringAlignment.Center;

                  xpos = e.MarginBounds.Left + (e.MarginBounds.Width / 2);


                  offset = offset + 30;
                  // e.Graphics.DrawString(Address1 + "," + Address2, Headerfont2, new SolidBrush(Color.Black), headerstartposition, starty + 20 + offset);
                  offset = offset + 12;

                  offset = offset + 16;
                  offset = 15;

                  offset = offset + 16;
                
                      e.Graphics.DrawString("Debit Note", FONTHEAD, new SolidBrush(Color.Black), 360, startY + 15);
                      e.Graphics.DrawString("Debit Note.No", Headerfont2, new SolidBrush(Color.Black), 550, startY + 10);
                
                
                  e.Graphics.DrawString(company.Name, FONTHEAD1, new SolidBrush(Color.Black), startX, startY);
                  e.Graphics.DrawString(company.Address, FONTHEAD2, new SolidBrush(Color.Black), startX, startY + 15);
                  e.Graphics.DrawString(company.Phone, FONTHEAD2, new SolidBrush(Color.Black), startX, startY + 30);
                  e.Graphics.DrawString("GSTIN:" + company.TIN_No, FONTHEAD1, new SolidBrush(Color.Black), startX, startY + 45);

                  e.Graphics.DrawString(":" + DOC_NO.Text, Headerfont2, new SolidBrush(Color.Black), 628, startY + 10);
                  offset = offset + 16;
                  e.Graphics.DrawString("Date", Headerfont2, new SolidBrush(Color.Black), 550, startY + 25);
                  e.Graphics.DrawString(":" + DOC_DATE_GRE.Value.ToShortDateString(), Headerfont2, new SolidBrush(Color.Black), 620, startY + 25);

                  startY += 90;

                  e.Graphics.DrawRectangle(blackPen, startX, startY, 780, 450);
                  //DebitNoteDB.Customer = Convert.ToInt32(PARTYACC.SelectedValue);
                  DataTable table = Common.getCustomer(CUSTOMER_CODE.Text);
                  Font font12 = new Font("Calibri", 9);
                  e.Graphics.DrawString("Name", font12, new SolidBrush(Color.Black), startX, startY + 1);
                  e.Graphics.DrawString("Address", font12, new SolidBrush(Color.Black), startX, startY + 12);
                  e.Graphics.DrawString("Mob", font12, new SolidBrush(Color.Black), startX, startY + 22);
                  e.Graphics.DrawString("GSTIN", font12, new SolidBrush(Color.Black), startX, startY + 32);
                  e.Graphics.DrawString("State", font12, new SolidBrush(Color.Black), startX, startY + 42);
                  if (table.Rows.Count > 0)
                  {
                      e.Graphics.DrawString(":" + table.Rows[0]["DESC_ENG"].ToString(), font12, new SolidBrush(Color.Black), startX + 60, startY + 1);
                      e.Graphics.DrawString(":" + table.Rows[0]["ADDRESS_A"].ToString(), font12, new SolidBrush(Color.Black), startX + 60, startY + 12);
                      e.Graphics.DrawString(":" + table.Rows[0]["MOBILE"].ToString(), font12, new SolidBrush(Color.Black), startX + 60, startY + 22);
                      e.Graphics.DrawString(":" + table.Rows[0]["TIN_NO"].ToString(), font12, new SolidBrush(Color.Black), startX + 60, startY + 32);
                      e.Graphics.DrawString(":" + table.Rows[0]["STATE"].ToString(), font12, new SolidBrush(Color.Black), startX + 60, startY + 42);
                  }
                  string tin_no = "";
                  try
                  {
                      tin_no = table.Rows[0]["TIN_NO"].ToString();
                  }
                  catch
                  {

                  }
                  if (tin_no.Length > 0)
                  {
                      Gst2 = tin_no.Substring(0, 2);
                  }
                  else
                  {
                      Gst2 = Gst1;
                  }
                  e.Graphics.DrawString("Delivery Note No & Date", Headerfont2, new SolidBrush(Color.Black), startX + 450, startY + 1);
                  e.Graphics.DrawString(":", Headerfont2, new SolidBrush(Color.Black), startX + 610, startY + 1);
                  e.Graphics.DrawString("Purchase Order No & Date", Headerfont2, new SolidBrush(Color.Black), startX + 450, startY + 14);
                  e.Graphics.DrawString(":", Headerfont2, new SolidBrush(Color.Black), startX + 610, startY + 14);
                  e.Graphics.DrawString("Dispatch Doc No & Date", Headerfont2, new SolidBrush(Color.Black), startX + 450, startY + 26);
                  e.Graphics.DrawString(":", Headerfont2, new SolidBrush(Color.Black), startX + 610, startY + 26);
                  e.Graphics.DrawString("Terms of Delivery if any", Headerfont2, new SolidBrush(Color.Black), startX + 450, startY + 42);
                  e.Graphics.DrawString(":", Headerfont2, new SolidBrush(Color.Black), startX + 610, startY + 42);
                  startY += 60;
                  e.Graphics.DrawRectangle(blackPen, startX, startY, 780, 280);
                  e.Graphics.DrawLine(blackPen, startX, startY + 300, startX + 780, startY + 300);
                  //   e.Graphics.DrawLine(blackPen, startX, startY + 100, 800 + startX, startY + 100);
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
                  string headtext;
                  if (Gst1 == Gst2)
                  {
                      headtext = "Sl.No".PadRight(10) + "Item".PadRight(88) + "HSN".PadRight(11) + "Unit".PadRight(8) + "Qty".PadRight(11) + "Rate".PadRight(11) + "Disc".PadRight(13) + "CGST".PadRight(19) + "SGST".PadRight(18) + "Total";
                  }
                  else
                  {
                      headtext = "Sl.No".PadRight(10) + "Item".PadRight(88) + "HSN".PadRight(11) + "Unit".PadRight(8) + "Qty".PadRight(11) + "Rate".PadRight(11) + "Disc".PadRight(13) + "IGST".PadRight(19) + "-----------".PadRight(18) + "Total";
                  }
                  e.Graphics.DrawString(headtext, printbold, new SolidBrush(Color.Black), startX, startY);
                  startY += 25;
                  //Column head Hline
                  Point point1 = new Point(startX, startY);
                  Point point2 = new Point(startX + 780, startY);
                  e.Graphics.DrawLine(blackPen, point1, point2);





                  Font itemhead = new Font("Times New Roman", 8);

                  //sl_No

                  e.Graphics.DrawLine(blackPen, startX + 35, startY - 25, startX + 35, startY + 275);
                  //item
                  e.Graphics.DrawLine(blackPen, startX + 340, startY - 25, startX + 340, startY + 275);
                  //HSN 
                  e.Graphics.DrawLine(blackPen, startX + 390, startY - 25, startX + 390, startY + 275);
                  //uom
                  e.Graphics.DrawLine(blackPen, startX + 420, startY - 25, startX + 420, startY + 275);
                  //qty
                  e.Graphics.DrawLine(blackPen, startX + 460, startY - 25, startX + 460, startY + 275);
                  //RATE
                  e.Graphics.DrawLine(blackPen, startX + 520, startY - 25, startX + 520, 550);
                  //DISC
                  e.Graphics.DrawLine(blackPen, startX + 560, startY - 25, startX + 560, startY + 275);
                  //CGST
                  e.Graphics.DrawLine(blackPen, startX + 640, startY - 25, startX + 640, startY + 275);
                  //SGST
                  e.Graphics.DrawLine(blackPen, startX + 720, startY - 25, startX + 720, startY + 275);


                  int printpoint = 50; //32
                  offset = offset + 35;
                  startY += 3;
                  Font font = new Font("Calibri", 8);
                  float fontheight = font.GetHeight();
                  try
                  {
                      int j = 1;
                      int nooflines = 0;


                      PRINTTOTALPAGE = false;

                      if (nooflines < 14)
                      {

                          string period, periodtype, tax;
                          int ORGLENGTH = Noteonreciept.Text.Length;
                          string name = Noteonreciept.Text.Length <= 50 ? Noteonreciept.Text : Noteonreciept.Text.Substring(0, 50);
                          string name2 = "";
                          int BALANCELENGH = ORGLENGTH - 50;
                          string qty = "1";
                          string rate, gross, TotalAmt;
                          if (ch_tax.Checked)
                          {
                              rate = (Convert.ToDecimal(AMOUNT.Text) - Convert.ToDecimal(txt_taxamt.Text)).ToString();
                              TotalAmt = AMOUNT.Text;
                          }
                          else
                          {
                              rate = AMOUNT.Text;
                              TotalAmt = (Convert.ToDecimal(AMOUNT.Text) + Convert.ToDecimal(txt_taxamt.Text)).ToString();
                          }

                          string uom = "";
                          string HSN = "";

                          decimal TaxPer = Convert.ToDecimal(TAX.SelectedValue);
                          decimal TaxVal = Convert.ToDecimal(txt_taxamt.Text);
                          string Disc = "0.00";
                          tqty = tqty + Convert.ToDecimal(qty);
                          trate = trate + Convert.ToDecimal(rate);
                          ttaxva += Convert.ToDecimal(qty) * Convert.ToDecimal(rate);
                          //tcdis = tcdis + Convert.ToDecimal(row.Cells["cDisc"].Value.ToString());

                          //to add % in taxpercentage
                          if ((TaxPer / 2) % 1 > 0)
                              tax = (TaxPer / 2).ToString();
                          else
                              tax = Convert.ToInt16(TaxPer / 2).ToString();

                          e.Graphics.DrawString("1.", font, new SolidBrush(Color.Black), startX, startY);
                          e.Graphics.DrawString(name, font, new SolidBrush(Color.Black), startX + 35, startY);
                          e.Graphics.DrawString(HSN, font, new SolidBrush(Color.Black), startX + 340, startY);
                          e.Graphics.DrawString(uom, font, new SolidBrush(Color.Black), startX + 390, startY);


                          StringFormat format = new StringFormat(StringFormatFlags.DirectionRightToLeft);
                          e.Graphics.DrawString(qty, font, new SolidBrush(Color.Black), startX + 460, startY, format);
                          e.Graphics.DrawString(rate, font, new SolidBrush(Color.Black), startX + 520, startY, format);
                          e.Graphics.DrawString(Disc, font, new SolidBrush(Color.Black), startX + 560, startY, format);
                          if (Gst1 == Gst2)
                          {
                              e.Graphics.DrawString(tax + "%", font, new SolidBrush(Color.Black), startX + 560, startY);
                              e.Graphics.DrawString((TaxVal / 2).ToString(), font, new SolidBrush(Color.Black), startX + 640, startY, format);
                              e.Graphics.DrawString(tax + "%", font, new SolidBrush(Color.Black), startX + 640, startY);
                              e.Graphics.DrawString((TaxVal / 2).ToString(), font, new SolidBrush(Color.Black), startX + 720, startY, format);
                          }
                          else
                          {
                              e.Graphics.DrawString(TaxPer + "%", font, new SolidBrush(Color.Black), startX + 560, startY);
                              e.Graphics.DrawString((TaxVal).ToString(), font, new SolidBrush(Color.Black), startX + 640, startY, format);
                          }
                          e.Graphics.DrawString(TotalAmt, font, new SolidBrush(Color.Black), startX + 780, startY, format);


                          offset = offset + (int)fontheight + 2;
                          nooflines++;
                          while (BALANCELENGH > 1)
                          {
                              startY += (int)fontheight;
                              name2 = BALANCELENGH <= 50 ? Noteonreciept.Text.Substring(printpoint, BALANCELENGH) : Noteonreciept.Text.Substring(printpoint, 50);
                              e.Graphics.DrawString(name2, font, new SolidBrush(Color.Black), startX + 35, startY);
                              BALANCELENGH = BALANCELENGH - 50;
                              printpoint = printpoint + 50;
                              // startY = startY + (int)fontheight;
                          }
                          printpoint = 50;
                          j++;
                          startY += (int)fontheight + 2;
                      }
                      else
                      {
                          hasmorepages = true;
                          PRINTTOTALPAGE = true;
                      }
                  }
                  catch (Exception exc)
                  {
                      string s = exc.Message;

                  }
              }
              startY = 460;
              float newoffset = 460;



              if (!PRINTTOTALPAGE)
              {
                  try
                  {
                      StringFormat format1 = new StringFormat(StringFormatFlags.DirectionRightToLeft);
                      e.Graphics.DrawString("Total Amount Before Tax", printbold, new SolidBrush(Color.Black), startX + 520, startY + 3);
                      e.Graphics.DrawString(ttaxva.ToString(), printbold, new SolidBrush(Color.Black), startX + 780, startY + 3, format1);
                      e.Graphics.DrawLine(blackPen, startX + 520, startY + 20, startX + 780, startY + 20);
                      if (Gst1 == Gst2)
                      {
                          e.Graphics.DrawString("Tax Amount : CGST+SGST", printbold, new SolidBrush(Color.Black), startX + 520, startY + 23);
                      }
                      else
                      {
                          e.Graphics.DrawString("Tax Amount : IGST", printbold, new SolidBrush(Color.Black), startX + 520, startY + 23);
                      }
                      e.Graphics.DrawString(txt_taxamt.Text, printbold, new SolidBrush(Color.Black), startX + 780, startY + 23, format1);
                      e.Graphics.DrawLine(blackPen, startX + 520, startY + 40, startX + 780, startY + 40);
                      e.Graphics.DrawString("Total Discount:", printbold, new SolidBrush(Color.Black), startX + 520, startY + 43);
                      e.Graphics.DrawString("0.00", printbold, new SolidBrush(Color.Black), startX + 780, startY + 43, format1);
                      e.Graphics.DrawLine(blackPen, startX + 520, startY + 60, startX + 780, startY + 60);
                      //    e.Graphics.DrawString("Total Amount After Tax", printbold, new SolidBrush(Color.Black), startX + 520, startY + 63);
                      if (ch_tax.Checked)
                          e.Graphics.DrawString(AMOUNT.Text, printbold1, new SolidBrush(Color.Black), startX + 780, startY + 63, format1);
                      else
                          e.Graphics.DrawString((Convert.ToDecimal(AMOUNT.Text) + Convert.ToDecimal(txt_taxamt.Text)).ToString(), printbold1, new SolidBrush(Color.Black), startX + 780, startY + 63, format1);
                      //   e.Graphics.DrawLine(blackPen, startX + 520, startY + 80, startX + 780, startY + 80);

                      e.Graphics.DrawString("Authorized Signatory", number, new SolidBrush(Color.Black), startX + 520, startY + 65);
                      e.Graphics.DrawString("[With Status and Seal]", number, new SolidBrush(Color.Black), startX + 520, startY + 74);

                      Font font11 = new Font("Calibri", 8);
                      StringFormat format = new StringFormat(StringFormatFlags.DirectionRightToLeft);
                      e.Graphics.DrawString("Total", font11, new SolidBrush(Color.Black), startx + 300, 440);
                      e.Graphics.DrawString(tqty.ToString(), font11, new SolidBrush(Color.Black), startX + 460, 440, format);
                      if (Gst1 == Gst2)
                      {
                          e.Graphics.DrawString((Convert.ToDecimal(txt_taxamt.Text) / 2).ToString(), font11, new SolidBrush(Color.Black), startX + 640, 440, format);
                          e.Graphics.DrawString((Convert.ToDecimal(txt_taxamt.Text) / 2).ToString(), font11, new SolidBrush(Color.Black), startX + 720, 440, format);
                      }
                      else
                      {
                          e.Graphics.DrawString((Convert.ToDecimal(txt_taxamt.Text)).ToString(), font11, new SolidBrush(Color.Black), startX + 640, 440, format);
                      }
                      string test = "";

                      if (ch_tax.Checked)
                      {
                          e.Graphics.DrawString(AMOUNT.Text, font11, new SolidBrush(Color.Black), startX + 780, 440, format);
                          test = Spell.SpellAmount.InWrods(Convert.ToDecimal(AMOUNT.Text));
                      }
                      else
                      {
                          e.Graphics.DrawString((Convert.ToDecimal(AMOUNT.Text) + Convert.ToDecimal(txt_taxamt.Text)).ToString(), font11, new SolidBrush(Color.Black), startX + 780, 440, format);
                          test = Spell.SpellAmount.InWrods(Convert.ToDecimal((Convert.ToDecimal(AMOUNT.Text) + Convert.ToDecimal(txt_taxamt.Text)).ToString()));
                      }


                      int index = test.IndexOf("Rupees");
                      int l = test.Length;
                      test = test.Substring(index + 5);
                      e.Graphics.DrawString("Terms & conditions:", Headerfont2, new SolidBrush(Color.Black), startX, startY + 22);
                      e.Graphics.DrawString("Amount in words:", font11, new SolidBrush(Color.Black), startX, startY + 3);
                      e.Graphics.DrawString(test, new Font("Calibri", 8, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + 14);

                  }
                  catch
                  {
                  }

              }
              e.HasMorePages = hasmorepages;
          }

  */



        void printDocumentMediumSize_PrintPage(object sender, PrintPageEventArgs e)
        {

            float xpos;
            int startx = 10;
            int starty = 30;
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
                // Billno = VOUCHNUM.Text;
                e.Graphics.DrawString(CompanyName, printFont, new SolidBrush(tabDataForeColor), xpos, starty, sf);
                e.Graphics.DrawString(Address1, printFont, new SolidBrush(tabDataForeColor), xpos, starty + offset + 3, sf);
                offset = offset + 24;
                // e.Graphics.DrawString(Address2, printFont, new SolidBrush(tabDataForeColor), xpos, starty + offset + 3, sf);
                offset = offset + 24;
                e.Graphics.DrawString("Ph: " + Phone, printFont, new SolidBrush(tabDataForeColor), xpos, starty + offset + 3, sf);
                offset = offset + 24;
                e.Graphics.DrawString("Credit Note" + DOC_NO.Text, printFont, new SolidBrush(tabDataForeColor), xpos, starty + offset, sf);
                offset = offset + 24;

                //e.Graphics.DrawString("---------------------------------------------------------------------------", printFont, new SolidBrush(Color.Black), xpos, starty + offset, sf);
                //e.Graphics.DrawString("---------------------------------------------------------------------------", printFont, new SolidBrush(Color.Black), xpos, starty + offset + 3, sf);
                //offset = offset + 14;
                //DateTime dt = DateTime.ParseExact(Convert.ToDateTime(DOC_DATE_GRE.Text).ToShortDateString(), "dd/MMM/yyyy", CultureInfo.InvariantCulture);
                //DateTime selectedDate = DateTime.ParseExact(Convert.ToDateTime(DOC_DATE_GRE.Text).ToShortDateString(), "yyyy/MM/dd", CultureInfo.InvariantCulture);
                e.Graphics.DrawString(Convert.ToDateTime(DOC_DATE_GRE.Value).ToShortDateString() + " " + DateTime.Now.ToShortTimeString(), printFont, new SolidBrush(tabDataForeColor), xpos, starty + offset, sf);
                offset = offset + 19;
                //e.Graphics.DrawString("Tin No:" + TineNo, printFont, new SolidBrush(tabDataForeColor), startx, starty + offset);
                //offset = offset + 12;
                e.Graphics.DrawString("Customer:" + CUSTOMER_NAME.Text, printFont, new SolidBrush(tabDataForeColor), startx, starty + offset - 2);
                Font itemhead = new Font("Courier New", 10);
                offset = offset + 15;
                e.Graphics.DrawString("-----------------------------------------------------", printFont, new SolidBrush(Color.Black), startx, starty + offset);
                offset = offset + 14;

                string headtext = "Item".PadRight(29) + "Price".PadRight(11) + "Qty".PadRight(8) + "Value";
                e.Graphics.DrawString(headtext, itemhead, new SolidBrush(Color.Black), startx, starty + offset);
                e.Graphics.DrawString("-----------------------------------------------------", printFont, new SolidBrush(Color.Black), startx, starty + offset + 13);
                offset = offset + 36;
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

                e.Graphics.DrawString("-----------------------------------------------------", printFont, new SolidBrush(Color.Black), startx, starty + offset);
                offset = offset + 13;
                string grosstotal = "Gross Total:".PadRight(7) + Spell.SpellAmount.comma(Convert.ToDecimal(TOTAL_AMOUNT.Text));
                //string vatstring = "Tax Amount:".PadRight(5) + Spell.SpellAmount.comma(Convert.ToDecimal(TAX_TOTAL.Text));
                string Discountstring = "Discount:".PadRight(13) + Spell.SpellAmount.comma(Convert.ToDecimal(DISCOUNT.Text));
                string total = "Total:".PadRight(13) + Spell.SpellAmount.comma(Convert.ToDecimal(NET_AMOUNT.Text));

                //e.Graphics.DrawString(grosstotal, font, new SolidBrush(Color.Black), startx + 290, starty + offset + 6);
                //offset = offset + (int)fontheight +3;
                //e.Graphics.DrawString(vatstring, font, new SolidBrush(Color.Black), startx + 200, starty + offset + 3);
                //offset = offset + (int)fontheight + 4;
                if (Convert.ToDecimal(DISCOUNT.Text) > 0)
                {
                    e.Graphics.DrawString(Discountstring, font, new SolidBrush(Color.Black), startx + 290, starty + offset + 3);
                    offset = offset + (int)fontheight + 1;
                }
                //e.Graphics.DrawString("------------------", font, new SolidBrush(Color.Black), startx + 290, starty + offset + 3);
                //offset = offset + (int)fontheight + 1;
                e.Graphics.DrawString(total, font, new SolidBrush(Color.Black), startx + 290, starty + offset + 3);

                offset = offset + 18;

                e.Graphics.DrawString("-----------------------------------------------------", printFont, new SolidBrush(Color.Black), startx, starty + offset);
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


                    e.Graphics.DrawString("KEEP IT...", amountingeng, new SolidBrush(Color.Black), xpos, starty + offset, sf);
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

        void printdocumentA4_PrintPage(object sender, PrintPageEventArgs e)
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

            if (logo != null || logo != "")
            {

                System.Drawing.Image img = System.Drawing.Image.FromFile(logo);

                Point loc = new Point(20, 50);
                e.Graphics.DrawImage(img, new Rectangle(50, 50, 50, 50));
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
                e.Graphics.DrawString(Addres1 + ", " + Addres2, Headerfont2, new SolidBrush(tabDataForeColor), headerstartposition, starty + offset);
                offset = offset + 20;
                e.Graphics.DrawString("Phone:".PadRight(3) + Phone, Headerfont2, new SolidBrush(tabDataForeColor), headerstartposition, starty + offset);
                offset = offset + 20;
                e.Graphics.DrawString("Email:".PadRight(3) + Email, Headerfont2, new SolidBrush(tabDataForeColor), headerstartposition, starty + offset);
                offset = offset + 20;
                e.Graphics.DrawString("Website:".PadRight(3) + Website, Headerfont2, new SolidBrush(tabDataForeColor), headerstartposition, starty + offset);
                offset = offset + 20;


                e.Graphics.DrawString("No: " + DOC_NO.Text, Headerfont2, new SolidBrush(tabDataForeColor), 600, starty + offset);
                offset = offset + 16;

                e.Graphics.DrawString("Tin No:" + TineNo, Headerfont2, new SolidBrush(tabDataForeColor), 600, starty + offset);
                offset = offset + 16;
                e.Graphics.DrawString("Date:" + DateTime.Now.ToString(), Headerfont2, new SolidBrush(tabDataForeColor), 600, starty + offset);
                offset = offset + 16;
                e.Graphics.DrawString("Credit Note", Headerfont2, new SolidBrush(tabDataForeColor), xpos, starty + offset - 24, sf);
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


                e.Graphics.DrawLine(blackPen, 355, 219, 355, 900);
                e.Graphics.DrawLine(blackPen, 450, 219, 450, 900);
                e.Graphics.DrawLine(blackPen, 540, 219, 540, 900);
                e.Graphics.DrawLine(blackPen, 650, 219, 650, 900);
                e.Graphics.DrawLine(blackPen, 760, 219, 760, 900);

                e.Graphics.DrawLine(blackPen, 45, 900, 760, 900);


                string headtext = "Item".PadRight(90) + "".PadRight(22) + "Qty".PadRight(22) + "Rate".PadRight(30) + "Total";
                e.Graphics.DrawString(headtext, Headerfont2, new SolidBrush(Color.Black), startx, starty + offset - 1);

                offset = offset + 40;
                Font font = new Font("Times New Roman", 10);
                float fontheight = font.GetHeight();
                try
                {
                    foreach (DataGridViewRow row in dgItems.Rows)
                    {


                        string name = row.Cells[1].Value.ToString();
                        string tax = row.Cells[7].Value.ToString();
                        string qty = row.Cells[5].Value.ToString();
                        string rate = row.Cells[6].Value.ToString();
                        string price = row.Cells[11].Value.ToString();
                        string productline = name + tax + qty + rate + price;
                        e.Graphics.DrawString(name, font, new SolidBrush(Color.Black), startx, starty + offset);

                        e.Graphics.DrawString(tax, font, new SolidBrush(Color.Black), startx + 310, starty + offset);
                        e.Graphics.DrawString(qty, font, new SolidBrush(Color.Black), startx + 430, starty + offset);
                        e.Graphics.DrawString(rate, font, new SolidBrush(Color.Black), startx + 525, starty + offset);
                        e.Graphics.DrawString(price, font, new SolidBrush(Color.Black), startx + 630, starty + offset);
                        offset = offset + (int)fontheight + 10;
                    }
                }
                catch
                {

                }
            }

            float newoffset = 900;

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
            e.Graphics.DrawString("Discount", Headerfont2, new SolidBrush(Color.Black), startx + 490, starty + newoffset + 3);
            e.Graphics.DrawString(DISCOUNT.Text, Headerfont2, new SolidBrush(Color.Black), startx + 610, starty + newoffset + 3);

            //  offset = offset + 20;
            //e.Graphics.DrawString("VAT", Headerfont2, new SolidBrush(Color.Black), startx + 490, starty + offset + 3);
            //e.Graphics.DrawString(VAT.Text, Headerfont2, new SolidBrush(Color.Black), startx + 610, starty + offset + 3);

            //newoffset = newoffset + 20;

            //e.Graphics.DrawString("Tax Amount", Headerfont2, new SolidBrush(Color.Black), startx + 490, starty + offset + 3);
            //e.Graphics.DrawString(TAX_TOTAL.Text, Headerfont2, new SolidBrush(Color.Black), startx + 610, starty + offset + 3);

            offset = offset + 20;
            e.Graphics.DrawString("---------------------------------------", Headerfont2, new SolidBrush(Color.Black), startx + 490, starty + newoffset + 3);
            e.Graphics.DrawString("Authorized Signature", Headerfont2, new SolidBrush(Color.Black), startx, starty + newoffset + 3);
            offset = offset + 25;
            e.Graphics.DrawString("Total", Headerfont2, new SolidBrush(Color.Black), startx + 460, starty + newoffset + 3);


            e.Graphics.DrawString(Spell.SpellAmount.comma(Convert.ToDecimal(NET_AMOUNT.Text)), Headerfont2, new SolidBrush(Color.Black), startx + 610, starty + newoffset + 3);

            newoffset = newoffset + 20;






            e.HasMorePages = false;


        }

        private void printDocument_PrintPage(object sender, PrintPageEventArgs e)
        {

            float xpos;
            int startx = 10;
            int starty = 30;
            int offset = 15;

            int w = e.MarginBounds.Width / 2;
            int x = e.MarginBounds.Left;
            int y = e.MarginBounds.Top;
            Font Headerfont1 = new Font("Courier New", 12);
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
                e.Graphics.DrawString(Addres2, printFont, new SolidBrush(tabDataForeColor), xpos, starty + offset, sf);
                offset = offset + 10;
                e.Graphics.DrawString("Credit Note" + DOC_NO.Text, printFont, new SolidBrush(tabDataForeColor), xpos, starty + offset, sf);
                offset = offset + 10;

                e.Graphics.DrawString("-------------------------------------------------------------", printFont, new SolidBrush(Color.Black), xpos, starty + offset, sf);
                e.Graphics.DrawString("-------------------------------------------------------------", printFont, new SolidBrush(Color.Black), xpos, starty + offset + 3, sf);

                offset = offset + 10;
                e.Graphics.DrawString("Tin No:" + TineNo, printFont, new SolidBrush(tabDataForeColor), startx, starty + offset);
                offset = offset + 12;
                e.Graphics.DrawString("Date:" + DateTime.Now.ToString(), printFont, new SolidBrush(tabDataForeColor), startx, starty + offset);
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

                        string name = row.Cells[1].Value.ToString().PadRight(20);
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
                string vatstring = "Tax Amount:" + TAX_TOTAL.Text.PadRight(30);
                string total = "Total:" + NET_AMOUNT.Text;
                string endtotal = total;
                e.Graphics.DrawString(endtotal, font, new SolidBrush(Color.Black), startx + 200, starty + offset + 3);

                offset = offset + 15;
                //if (txtcashrcvd.Text != "")
                //{
                //    try
                //    {
                //        decimal balance = Convert.ToDecimal(txtcashrcvd.Text) - Convert.ToDecimal(TOTAL_AMOUNT.Text);
                //        e.Graphics.DrawString("Cash Rcvd:" + txtcashrcvd.Text + "   " + "Balance:" + balance.ToString(), font, new SolidBrush(Color.Black), startx, starty + offset);

                //        offset = offset + 12;

                //    }
                //    catch
                //    {
                //    }
                //}


            }

            e.HasMorePages = false;


        }

        private void btnSave_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Credit Note Will be generated", btnSave);
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void btnSales_Click(object sender, EventArgs e)
        {
            SalesManMasterHelp salmas = new SalesManMasterHelp();
            if (salmas.ShowDialog() == DialogResult.OK && salmas.c != null)
            {
                SALESMAN_CODE.Text = Convert.ToString(salmas.c[0].Value);
                SALESMAN_NAME.Text = Convert.ToString(salmas.c[1].Value);
            }
        }

        private void dgItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgItems.Rows.Count > 0 && dgItems.CurrentRow != null)
            {
                DataGridViewCellCollection c = dgItems.CurrentRow.Cells;
                selectedRow = dgItems.CurrentRow.Index;
                ITEM_CODE.Text = Convert.ToString(c["cCode"].Value);
                ITEM_NAME.Text = Convert.ToString(c["cName"].Value);
                if (hasBatch)
                {
                    BATCH.Text = Convert.ToString(c["cBatch"].Value);
                    // EXPIRY_DATE.Value = DateTime.ParseExact(Convert.ToString(c["cExpDate"].Value), "dd/MM/yyyy", null);
                    if (c["cExpDate"].Value != null)
                    {
                        EXPIRY_DATE.Value = Convert.ToDateTime(c["cExpDate"].Value);
                    }
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
                ITEM_DISCOUNT.Text = Convert.ToString(c["cDisc"].Value);
                ITEM_TOTAL.Text = Convert.ToString(c["cTotal"].Value);
                sales_price = Convert.ToDouble(c["cPrice"].Value) / Convert.ToDouble(c["uomQty"].Value);
                if (c["colBATCH"].Value != null)
                {
                    BARCODE.Text = c["colBATCH"].Value.ToString();
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
                            else
                            {
                                UOM.Focus();
                            }
                            break;
                        case "BATCH":
                            EXPIRY_DATE.Focus();
                            break;
                        case "QUANTITY":
                            //  PRICE.Focus();
                            addItem();
                            clearItem();
                            ITEM_NAME.Focus();
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

        private void calculateGrossAmount(object sender, EventArgs e)
        {
            if (QUANTITY.Text.Trim() != "" && PRICE.Text.Trim() != "")
            {
                GROSS_TOTAL.Text = (Convert.ToDouble(QUANTITY.Text) * Convert.ToDouble(PRICE.Text)).ToString();
            }
            gettax();
        }

        private void txtinvoiceno_TextChanged()
        {

        }

        private void KrBt_print_Click(object sender, EventArgs e)
        {
            PrintingCreditNOte();
        }

        private void kryptonButton1_Click_1(object sender, EventArgs e)
        {
            Sales_Return_Help h = new Sales_Return_Help(0, type);
            if (h.ShowDialog() == DialogResult.OK && h.c != null)
            {
                btnClear.PerformClick();
                ReturnBillId = Convert.ToString(h.c["DOC_ID"].Value);
                txtinvoiceno.Text = ReturnBillId;
                DOC_REFERENCE.Text = Convert.ToString(h.c["DOC_NO"].Value);
                try
                {
                    DOC_DATE_GRE.Value = Convert.ToDateTime(Convert.ToString(h.c["DOC_DATE_GRE"].Value));
                }
                catch
                {
                }
                DOC_DATE_HIJ.Text = Convert.ToString(h.c["DOC_DATE_HIJ"].Value);
                CURRENCY_CODE.Text = Convert.ToString(h.c["CURRENCY_CODE"].Value);
                //   DOC_REFERENCE.Text = Convert.ToString(h.c["DOC_REFERENCE"].Value);
                CUSTOMER_CODE.Text = Convert.ToString(h.c["CUSTOMER_CODE"].Value);
                GetLedgerId(Convert.ToString(h.c["CUSTOMER_CODE"].Value));
                CUSTOMER_NAME.Text = Convert.ToString(h.c["CUSTOMER_NAME_ENG"].Value);
                SALESMAN_CODE.Text = Convert.ToString(h.c["SALESMAN_CODE"].Value);
                NOTES.Text = Convert.ToString(h.c["NOTES"].Value);
                TAX_TOTAL.Text = Convert.ToString(h.c["TAX_TOTAL"].Value);
                VAT.Text = Convert.ToString(h.c["VAT"].Value);
                DISCOUNT.Text = Convert.ToString(h.c["DISCOUNT"].Value);
                TOTAL_AMOUNT.Text = Convert.ToString(h.c["TOTAL_AMOUNT"].Value);
                NET_AMOUNT.Text = Convert.ToString(h.c["NET_AMOUNT"].Value);
                PAY_CODE.Text = Convert.ToString(h.c["PAY_CODE"].Value);
                PAY_NAME.Text = General.getName(PAY_CODE.Text, "GEN_PAYTYPE");
                CARD_NO.Text = Convert.ToString(h.c["CARD_NO"].Value);

                //conn.Open();
                //cmd.CommandText = "SELECT * FROM INV_SALES_DTL WHERE DOC_TYPR ='SAL.CSR'";
                //cmd.CommandType = CommandType.Text;
                //SqlDataReader r = cmd.ExecuteReader();
                SqlDataReader r = saledtlObj.getAllByDocTypr();
                while (r.Read())
                {
                    int i = dgItems.Rows.Add(new DataGridViewRow());
                    DataGridViewCellCollection c = dgItems.Rows[i].Cells;
                    c["cCode"].Value = r["ITEM_CODE"];
                    c["cName"].Value = r["ITEM_DESC_ENG"];
                    c["cBatch"].Value = r["BATCH"];
                    if (hasBatch)
                    {

                        //c["cExpDate"].Value = Convert.ToDateTime(r["EXPIRY_DATE"]).ToString("dd/MM/yyyy");
                        try
                        {
                            c["cExpDate"].Value = Convert.ToDateTime(r["EXPIRY_DATE"]).ToString();
                        }

                        catch
                        {
                        }
                    }
                    c["cUnit"].Value = r["UOM"];
                    c["cQty"].Value = r["QUANTITY"];
                    c["cPrice"].Value = r["PRICE"];
                    if (hasTax)
                    {
                        if (r["ITEM_TAX_PER"].ToString() == "")
                        {
                            c["cTaxPer"].Value = "0";
                        }
                        else
                        {
                            c["cTaxPer"].Value = r["ITEM_TAX_PER"];
                        }

                        if (r["ITEM_TAX"].ToString() == "")
                        {
                            c["cTaxAmt"].Value = "0";
                        }
                        else
                        {
                            c["cTaxAmt"].Value = r["ITEM_TAX"];
                        }
                    }
                    else
                    {
                        c["cTaxPer"].Value = "0";
                        c["cTaxAmt"].Value = "0";

                    }
                    if (r["GROSS_TOTAL"].ToString() == "")
                        c["cGTotal"].Value = "0.00";
                    else
                        c["cGTotal"].Value = r["GROSS_TOTAL"];

                    c["cDisc"].Value = r["ITEM_DISCOUNT"];
                    if (r["ITEM_TOTAL"].ToString() == "")
                        c["cTotal"].Value = "0.00";
                    else
                        c["cTotal"].Value = r["ITEM_TOTAL"];
                    DOC_DATE_GRE.Value = DateTime.Now;
                }
                DbFunctions.CloseConnection();
            }
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

                    //NOT VALIDATED//
                    // PurchasePrice = Convert.ToDecimal(dgBatch.CurrentRow.Cells["PUR"].Value);
                    if (hasBatch)
                    {
                        if (dgBatch.CurrentRow.Cells["BATCH CODE"].Value != null)
                            BATCH.Text = dgBatch.CurrentRow.Cells["BATCH CODE"].Value.ToString();
                        if (dgBatch.CurrentRow.Cells["EXPIRY DATE"].Value != null && dgBatch.CurrentRow.Cells["EXPIRY DATE"].Value.ToString() != "")
                            EXPIRY_DATE.Value = Convert.ToDateTime(dgBatch.CurrentRow.Cells["EXPIRY DATE"].Value);
                    }
                    String rateType = Convert.ToString(RATE_CODE.SelectedValue);
                    string pricedecimal = dgBatch.CurrentRow.Cells[rateType].Value.ToString();
                    double pricedec = Convert.ToDouble(pricedecimal);
                    sales_price = Convert.ToDouble(dgBatch.Rows[0].Cells[rateType].Value);
                    //PRICE.Text = pricedec.ToString("N3");
                    PRICE.Text = dgBatch.CurrentRow.Cells[rateType].Value.ToString();

                    TaxId = Convert.ToInt16(dgBatch.CurrentRow.Cells["TaxId"].Value.ToString());
                    if (!hasPurExclusive)
                    {
                        double taxcalc = 0;
                        taxcalc = (Convert.ToDouble(ITEM_TAX_PER.Text) / 100) + 1;
                        PRICE.Text = (Convert.ToDouble(PRICE.Text) / taxcalc).ToString();
                    }
                    sales_price = Convert.ToDouble(PRICE.Text);
                    PRICE.Text = sales_price.ToString();
                    GetTaxRate();
                    calculateGrossAmount(sender, e);
                    ITEM_TAX_TextChanged(sender, e);
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

        private void panTax_Paint(object sender, PaintEventArgs e)
        {

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

                //NOT VALIDATED//
                // PurchasePrice = Convert.ToDecimal(dgBatch.CurrentRow.Cells["PUR"].Value);
                if (hasBatch)
                {
                    if (dgBatch.CurrentRow.Cells["BATCH CODE"].Value != null)
                        BATCH.Text = dgBatch.CurrentRow.Cells["BATCH CODE"].Value.ToString();
                    if (dgBatch.CurrentRow.Cells["EXPIRY DATE"].Value != null && dgBatch.CurrentRow.Cells["EXPIRY DATE"].Value.ToString() != "")
                        EXPIRY_DATE.Value = Convert.ToDateTime(dgBatch.CurrentRow.Cells["EXPIRY DATE"].Value);
                }
                String rateType = Convert.ToString(RATE_CODE.SelectedValue);
                string pricedecimal = dgBatch.CurrentRow.Cells[rateType].Value.ToString();
                double pricedec = Convert.ToDouble(pricedecimal);
                sales_price = Convert.ToDouble(dgBatch.Rows[0].Cells[rateType].Value);
                //PRICE.Text = pricedec.ToString("N3");
                PRICE.Text = dgBatch.CurrentRow.Cells[rateType].Value.ToString();

                TaxId = Convert.ToInt16(dgBatch.CurrentRow.Cells["TaxId"].Value.ToString());
                if (!hasPurExclusive)
                {
                    double taxcalc = 0;
                    taxcalc = (Convert.ToDouble(ITEM_TAX_PER.Text) / 100) + 1;
                    PRICE.Text = (Convert.ToDouble(PRICE.Text) / taxcalc).ToString();
                }
                sales_price = Convert.ToDouble(PRICE.Text);
                PRICE.Text = sales_price.ToString();
                GetTaxRate();
                calculateGrossAmount(sender, e);
                ITEM_TAX_TextChanged(sender, e);

                dgBatch.Visible = false;
                QUANTITY.Focus();
            }
        }
        public void modifiedtransaction(string ID, string date)
        {
            try
            {
                if (type == "LGR.PRT" || type == "LGR.CPR")
                {
                    modtrans.VOUCHERTYPE = "Purchase Return";
                }
                else
                {
                    modtrans.VOUCHERTYPE = "Purchase";
                }
                // modtrans.Date = date;
                modtrans.Date = DOC_DATE_GRE.Value.ToString("MM/dd/yyyy");
                modtrans.USERID = lg.EmpId;
                modtrans.VOUCHERNO = ID;
                modtrans.NARRATION = "";
                modtrans.STATUS = "Delete";
                modtrans.MODIFIEDDATE = DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss");
                modtrans.INVOICENO = txtinvoiceno.Text;
                modtrans.BRANCH = lg.Branch;
                modtrans.Date = date;
                modtrans.insertTransaction();
            }
            catch (Exception c)
            {
                string x = c.Message;
            }
        }
        private void DeleteTransation(string ID)
        {
            try
            {
                if (type == "LGR.PRT" || type == "LGR.CPR")
                {
                    trans.VOUCHERTYPE = "Purchase Return";
                }
                else
                {
                    trans.VOUCHERTYPE = "Purchase";
                }

                trans.VOUCHERNO = ID;
                trans.DeletePurchaseTransaction();
            }
            catch
            {
            }


        }
        public void AddtoDeletedTransaction(string id)
        {
            string vchr;
            if (type == "LGR.PRT" || type == "LGR.CPR")
            {
                vchr = "Purchase Return";
            }
            else
            {
                vchr = "Purchase";
            }
            //conn.Open();
            //cmd.CommandText = "insert into     tbl_deletedTransaction(VOUCHERNO, VOUCHERTYPE, ACCNAME, PARTICULARS, DEBIT, CREDIT, DATED, NARRATION, USERID, SYSTEMTIEM, ACCID) select    VOUCHERNO, VOUCHERTYPE, ACCNAME, PARTICULARS, DEBIT, CREDIT, DATED, NARRATION, USERID, SYSTEMTIEM, ACCID from tb_Transactions where VOUCHERNO='" + id + "' and VOUCHERTYPE='" + vchr + "'";
            //cmd.ExecuteNonQuery();
            // // MessageBox.Show("Record Deleted!");
            //conn.Close();
            dltdtranobj.VoucherNo = id;
            dltdtranobj.VoucherType = vchr;
            dltdtranobj.insertDeletedTran();

        }
        private void btnDlt_Click(object sender, EventArgs e)
        {
            if (ID != "")
            {
                if (MessageBox.Show("Are You Sure To Delete Purchase return?", "Record Deletion", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string dat = ""; ;
                    string id = ID;
                    try
                    {
                        dat = Convert.ToDateTime(DOC_DATE_GRE.Text).ToString("MM/dd/yyyy");
                    }
                    catch (Exception da) { string z = da.Message; }
                    modifiedtransaction(id, dat);
                    AddtoDeletedTransaction(id);
                    DeleteTransation(id);

                    //SqlCommand reduceStockCommand = new SqlCommand();
                    //reduceStockCommand.Connection = conn;
                    //conn.Open();
                    //reduceStockCommand.CommandText = "SELECT ITEM_CODE, QTY_RCVD, UOM_QTY, cost_price,PRICE_BATCH FROM INV_PURCHASE_DTL WHERE DOC_NO = '" + DOC_NO.Text + "' AND DOC_TYPE = '" + type + "'";
                    // SqlDataReader r = reduceStockCommand.ExecuteReader();

                    purchDtlObj.DocNo = ID;
                    purchDtlObj.DocType = type;
                    DataTable r = purchDtlObj.getItemDetailsTable();



                    StockEntry se = new StockEntry();


                    for (int i = 0; i < r.Rows.Count; i++)
                    {

                       
                        double qty = (Convert.ToDouble(r.Rows[i]["QTY_RCVD"]) * Convert.ToDouble(r.Rows[i]["UOM_QTY"]));
                        if (type.Equals("LGR.PRT") || type.Equals("LGR.CPR"))
                        {
                            qty = 1 * qty;
                        }
                        se.addStockWithBatch(Convert.ToString(r.Rows[i]["ITEM_CODE"]), Convert.ToString(qty), Convert.ToString(r.Rows[i]["cost_price"]), Convert.ToString(r.Rows[i]["PRICE_BATCH"]));

                    }

                    //conn.Open();
                    //cmd.CommandText = "DELETE  FROM INV_PURCHASE_HDR   WHERE DOC_NO = '" + id + "' ;DELETE FROM INV_PURCHASE_DTL WHERE DOC_NO = '" + id + "';DELETE FROM INV_STK_TRX_HDR WHERE DOC_REFERENCE = '" + id + "' AND DOC_TYPE='" + type + "' ;DELETE FROM INV_STK_TRX_DTL WHERE DOC_REFERENCE = '" + id + "'AND DOC_TYPE='" + type + "'";
                    //cmd.ExecuteNonQuery();
                    //conn.Close();
                    purchHdrObj.DocNo = id;
                    purchHdrObj.DocReference = id;
                    purchHdrObj.DocType = type;
                    purchHdrObj.deleteDetails();

                    DebitNoteDB.DocNo = DOC_NO.Text;
                    if (!DebitNoteDB.Existing_DebitNote())
                    {
                        DebitNoteDB.DeleteDebitNote();
                    }

                    MessageBox.Show("Record Deleted!");

                }

                btnClear.PerformClick();
            }
            else
            {
                MessageBox.Show("Please Select a Purhcase Return to Delete");
            }
        }


        private void chkDebit_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDebit.Checked)
            {
                bindledgers();
                DEBITACC.SelectedValue = 21;
                CREDITACC.SelectedValue = 32;
                type = "LGR.PRT";
            }
        }

        private void chkCredit_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCredit.Checked)
            {
                if (CUSTOMER_CODE.Text == "")
                    DEBITACC.Text = "";
                else
                {
                    GetLedgerId(CUSTOMER_CODE.Text);
                    type = "LGR.CPR";
                }

            }
        }

        private void dgItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        decimal servTotal = 0;
        decimal grosTotal = 0;
        decimal servTax = 0;
        decimal otherTax = 0;
        decimal discAmt = 0;
        decimal grossValue = 0;
        void CalculateTotals()
        {

            servTotal = 0;
            grosTotal = 0;
            servTax = 0;
            otherTax = 0;
            discAmt = 0;
            grossValue = 0;
            // servTotal = dgItems.Rows.Cast<DataGridViewRow>().Where(r => (r.Cells["colPtype"].Value).ToString() == "SRV").Sum(t => Convert.ToDecimal(t.Cells["cNetValue"].Value));
            //servTotal = dgItems.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToDecimal(t.Cells["cNetValue"].Value));
            // txtService.Text = servTotal.ToString();
            // servTax = dgItems.Rows.Cast<DataGridViewRow>().Where(r => (r.Cells["colPtype"].Value).ToString() == "SRV").Sum(t => Convert.ToDecimal(t.Cells["cTaxAmt"].Value));
            // otherTax = dgItems.Rows.Cast<DataGridViewRow>().Where(r => (r.Cells["colPtype"].Value).ToString() != "SRV").Sum(t => Convert.ToDecimal(t.Cells["cTaxAmt"].Value));
            grosTotal = dgItems.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToDecimal(t.Cells["uTotal"].Value));
            discAmt = dgItems.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToDecimal(t.Cells["uDiscount"].Value));
            grossValue = grosTotal - discAmt;
            // txtService.Text = servTotal.ToString();
        } 
    }
}