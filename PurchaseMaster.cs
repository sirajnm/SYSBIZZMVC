using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using System.IO;
using System.Drawing.Printing;
using System.Data.OleDb;
//using System.Data.OleDb;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Globalization;
using Sys_Sols_Inventory.Model;
namespace Sys_Sols_Inventory
{
    public partial class PurchaseMaster : Form
    {
        Class.PiceList prclst = new Class.PiceList();
        int pagewidth = 0;
        int pageheight = 0;
        int defaultheight = 0;
        int itemlength = 0;
        bool fixedheight = false;
        bool PAGETOTAL = false;
        String decimalFormat;
        #region properties declaration
        //private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        //private SqlConnection conn2 = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        private bool HasArabic = true;
        private SqlCommand cmd = new SqlCommand();
        //private SqlDataAdapter adapter = new SqlDataAdapter();
        private DataTable table = new DataTable();
        private DataTable tableUnits = new DataTable();
        private BindingSource source = new BindingSource();
        private BindingSource source2 = new BindingSource();
        private int selectedRow = -1;
        private bool hasBatch = false;
        private bool hasTax = false;
        private bool hasBarcode = false;
        private bool hasPurExclusive = false;
        private bool hasSaleExclusive = false;
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
        private string Excel03ConString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";
        private string Excel07ConString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";
        string SalesManCode, salesmanname,ModifyType="";
        Initial mdi = (Initial)Application.OpenForms["Initial"];
        Login lg = (Login)Application.OpenForms["Login"];
        InvPurchaseHdrDB invHdr = new InvPurchaseHdrDB();
        InvPurchaseDtlDB invDtl = new InvPurchaseDtlDB();
        InvStkTrxHdrDB stkHdr = new InvStkTrxHdrDB();
        InvStkTrxDtlDB stkDtl = new InvStkTrxDtlDB();
        PaySupplierDB clsSupplier = new PaySupplierDB();

        StockDB stkdb = new StockDB();
        bool firstitemlistbyname = false;
        bool clearitemname = false;
        string companyname;
        int HEIGHT, WIDTH;
        string PriceType;
        bool currencyflag = true;
        string curre="",basecur="",stcurcode="";
        public string decpoint = "2";
        private bool hasArabic = true;
        bool HASSERIAL = false;
        Class.DateSettings dset=new Class.DateSettings();
        bool IsMRP = false, IsProductCode = false, IsCompany = false, IsBarcodeValue = false;
        Class.Ledgers Ledg = new Class.Ledgers();
        Class.Transactions trans = new Class.Transactions();
      
        Class.ModifiedTransaction modtrans=new Class.ModifiedTransaction();
        Class.BarcodeSettings barcodse = new Class.BarcodeSettings();
        Class.Stock_Report stkrpt = new Class.Stock_Report();
        Class.CompanySetup ComSet = new Class.CompanySetup();

        ProjectDB ProjectDB = new ProjectDB();

        bool ActiveForm = false;
        decimal decTotal = 0, corrier=0;
        bool HasType;
        bool HasCategory;
        bool HasTM;
        bool HasGroup;
        bool excludechanged = false;
        bool includechang = false;
        string DefaultSaleType = "";
        bool hasPriceBatch = false;
        double Item_Discount_Amt = 0 , pricefob=0;
        string CompanyName, ArabicName, EngBranch, ArbBranch, Place, ArbPlace, ArbCity, City, Phone, Email, Fax, TineNo, Billno, Date, CUSID, Website, panno, vat, Address1, ArbAddress1, ArbAddress2, Address2, logo, DiscType, DiscValue;
        bool PUR_MoveDisc, PUR_MoveRtlper, PUR_MoveRtlAmt, PUR_MoveTaxper, PUR_MoveTaxAmt, PUR_MoveTotal, PUR_FocusDate, PUR_FocusSupplier, PUR_FocusInvoice, PUR_FocusReference, PUR_FocusBarcode, PUR_FocusItemCode, PUR_FocusItemName;
        public int printeditems = 0;
       
                 
           
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Escape))
            {
                if (dgSubPrices.Visible == true)
                {
                    dgSubPrices.Visible = false;
                }
                else if (PNL_DATAGRIDITEM.Visible==true)
                {
                    PNL_DATAGRIDITEM.Visible = false;

                    ItemClear();
                    ITEM_NAME.Focus();
                    return true;
                }
                else if (ITEM_NAME.Text != "")
                {
                    ItemClear();
                }
                else if (ID == "" && dgItems.Rows.Count > 0)
                {
                    if (MessageBox.Show("Are you sure to close", "Alert", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        if (keyData == (Keys.Escape))
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
                }
                
            
                 else if (ID!=""&&dgItems.Rows.Count>0)
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
            }

         //  else if (e.KeyCode == Keys.S && e.Control)
           else if (keyData == ( Keys.Alt|Keys.S ))
            {
               
              //  btnSave.Focus();
                btnSave.PerformClick();
                SUPPLIER_CODE.Focus();
                clearitemname = true;

                return true;
               
            }
            if (keyData == (Keys.F3))
            {
                bindgridview();
                return true;
            }
            else if (keyData == (Keys.Alt | Keys.N))
            {


                btnNewItem.PerformClick();
                return true;

            }
            
                
                return base.ProcessCmdKey(ref msg, keyData);
            
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

        public PurchaseMaster(string docType, string prefix)
        {
            InitializeComponent();
            GET_DEFAULTCURRENCY();
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
            GetLedgers();
           // GetPurType();
            GetMaxDocID();
        }

        public PurchaseMaster(string docNo)
        {
            InitializeComponent();
            GET_DEFAULTCURRENCY();
            //cmd.Connection = conn;
            //adapter.SelectCommand = cmd;
            DOC_NO.Text = docNo;
            POSTID = docNo;
            GetLedgers();
            GetMaxDocID();
            PurTypeLed();
            getDataFromDocNo();
            QTY_RCVD.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            PRICE_FOB.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            ITEM_TAX_PER.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            ITEM_TAX.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            ITEM_GROSS.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            RTL_PRICE.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            PRICE_TYPE.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            ITEM_DISCOUNT.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            this.Load -= new EventHandler(PurchaseMaster_Load);
        }

        public void GetMaxDocID()
        {
            int maxId;
            String value;
            /*
            if (cmbInvType.SelectedIndex == 0)
            {
                
                 value = invHdr.getMaxId();
               
            }
            else
            {
               
                invHdr.PurType = cmbInvType.SelectedValue == null ? "" : cmbInvType.SelectedValue.ToString();
                value = invHdr.getMaxIdWithPurType();
               
            }
            if (value.Equals("0") && cmbInvType.SelectedIndex != 0)
            {
                invHdr.PurType = cmbInvType.SelectedValue == null ? "" : cmbInvType.SelectedValue.ToString();
                //cmd.CommandText = "SELECT VoucherStart FROM GEN_PUR_STARTFROM WHERE VoucherTypeCode='" + cmbInvType.SelectedValue + "'";
                //cmd.CommandType = CommandType.Text;
                //conn.Open();
                Billno = VOUCHNUM.Text = invHdr.purVoucherStartFrom().ToString();
               // conn.Close();
            }
            else
            {
                maxId = Convert.ToInt32(value);
                Billno = VOUCHNUM.Text = (maxId + 1).ToString();
            }
            */
            string purchasetype = "Purchase";
            if (cmbInvType.SelectedIndex > 0)
            {
                
                purchasetype = cmbInvType.SelectedValue.ToString() == "EST" ? "PurchEstimate" : "Purchase" ;


            } 

            string query = "Declare @MaxDocID as int, @NoSeriesSuffix as varchar(5) ";
            query += " Select @MaxDocID = case when Max(Doc_ID) is null then 0 else Max(Doc_ID) end + 1, @NoSeriesSuffix = max(f.NoSeriesSuffix) from INV_PURCHASE_HDR p right join tbl_FinancialYear f on p.DOC_DATE_GRE between f.SDate and f.EDate ";
            if (purchasetype == "PurchEstimate") query += " and p.DOC_TYPE = 'PUR.EST' ";
            if (purchasetype == "Purchase") query += " and p.DOC_TYPE in ('PUR.CRD','PUR.CSS') ";
            query += " where f.CurrentFY = 1 ";
            query += " Select s.PRIFIX + @NoSeriesSuffix + Right(Replicate('0', s.SERIAL_LENGTH) + cast(@MaxDocID as varchar), s.SERIAL_LENGTH) DOCNo, @MaxDocID DocID from GEN_DOC_SERIAL s ";
            query += " where s.DOC_TYPE = '" + purchasetype + "' ";
            DataTable dt = DbFunctions.GetDataTable(query);
            if (dt.Rows.Count >= 1)
            {
                Billno = VOUCHNUM.Text = dt.Rows[0]["DOCID"].ToString();
                DOC_NO.Text = dt.Rows[0]["DOCNo"].ToString();
            }

            
        }

        public void BindSettings()
        {
            try
            {
                DataTable dt = ComSet.SysSetup_selectcompany();
                //cmd.Connection = conn;
                //cmd.CommandType = CommandType.Text;

                //cmd.CommandText = "SELECT * FROM SYS_SETUP";
                //adapter.SelectCommand = cmd;
                //adapter.Fill(dt);


                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["HasType"] == DBNull.Value)
                        HasType = false;
                    else
                        HasType = Convert.ToBoolean(dt.Rows[0]["HasType"]);

                    if (dt.Rows[0]["HasCategory"] == DBNull.Value)
                        HasCategory = false;
                    else
                        HasCategory = Convert.ToBoolean(dt.Rows[0]["HasCategory"]);

                    if (dt.Rows[0]["HasGroup"] == DBNull.Value)
                        HasGroup = false;
                    else
                        HasGroup = Convert.ToBoolean(dt.Rows[0]["HasGroup"]);

                    if (dt.Rows[0]["HasTM"] == DBNull.Value)
                        HasTM = false;
                    else
                        HasTM = Convert.ToBoolean(dt.Rows[0]["HasTM"]);

                    if (dt.Rows[0]["PUR_MoveDisc"] == DBNull.Value)
                        PUR_MoveDisc = false;
                    else
                        PUR_MoveDisc = Convert.ToBoolean(dt.Rows[0]["PUR_MoveDisc"]);

                    if (dt.Rows[0]["PUR_MoveRtlper"] == DBNull.Value)
                        PUR_MoveRtlper = false;
                    else
                        PUR_MoveRtlper = Convert.ToBoolean(dt.Rows[0]["PUR_MoveRtlper"]);

                    if (dt.Rows[0]["PUR_MoveRtlAmt"] == DBNull.Value)
                        PUR_MoveRtlAmt = false;
                    else
                        PUR_MoveRtlAmt = Convert.ToBoolean(dt.Rows[0]["PUR_MoveRtlAmt"]);

                    if (dt.Rows[0]["PURMoveTaxper"] == DBNull.Value)
                        PUR_MoveTaxper = false;
                    else
                        PUR_MoveTaxper = Convert.ToBoolean(dt.Rows[0]["PURMoveTaxper"]);

                    if (dt.Rows[0]["PURMoveTaxAmt"] == DBNull.Value)
                        PUR_MoveTaxAmt = false;
                    else
                        PUR_MoveTaxAmt = Convert.ToBoolean(dt.Rows[0]["PURMoveTaxAmt"]);

                    if (dt.Rows[0]["PURMoveTotal"] == DBNull.Value)
                        PUR_MoveTotal = false;
                    else
                        PUR_MoveTotal = Convert.ToBoolean(dt.Rows[0]["PURMoveTotal"]);

                    if (dt.Rows[0]["PURFocusDate"] == DBNull.Value)
                        PUR_FocusDate = false;
                    else
                        PUR_FocusDate = Convert.ToBoolean(dt.Rows[0]["PURFocusDate"]);

                    if (dt.Rows[0]["PURFocusSupplier"] == DBNull.Value)
                        PUR_FocusSupplier = false;
                    else
                        PUR_FocusSupplier = Convert.ToBoolean(dt.Rows[0]["PURFocusSupplier"]);

                    if (dt.Rows[0]["PUR_FocusInvoice"] == DBNull.Value)
                        PUR_FocusInvoice = false;
                    else
                        PUR_FocusInvoice = Convert.ToBoolean(dt.Rows[0]["PUR_FocusInvoice"]);

                    if (dt.Rows[0]["PUR_FocusReference"] == DBNull.Value)
                        PUR_FocusReference = false;
                    else
                        PUR_FocusReference = Convert.ToBoolean(dt.Rows[0]["PUR_FocusReference"]);

                    if (dt.Rows[0]["PUR_FocusBarcode"] == DBNull.Value)
                        PUR_FocusBarcode = false;
                    else
                        PUR_FocusBarcode = Convert.ToBoolean(dt.Rows[0]["PUR_FocusBarcode"]);

                    if (dt.Rows[0]["PUR_FocusItemCode"] == DBNull.Value)
                        PUR_FocusItemCode = false;
                    else
                        PUR_FocusItemCode = Convert.ToBoolean(dt.Rows[0]["PUR_FocusItemCode"]);

                    if (dt.Rows[0]["PUR_FocusItemCode"] == DBNull.Value)
                        PUR_FocusItemName = false;
                    else
                        PUR_FocusItemName = Convert.ToBoolean(dt.Rows[0]["PUR_FocusItemName"]);
                    if (dt.Rows[0]["Dec_qty"] == DBNull.Value)
                        decpoint = "0.00";
                    else
                        decpoint = Convert.ToString(dt.Rows[0]["Dec_qty"]);
                    if (dt.Rows[0]["PUR_expcper"] == DBNull.Value)
                        corrier = 0;
                    else
                        corrier = Convert.ToDecimal(dt.Rows[0]["PUR_expcper"].ToString());
                    if (dt.Rows[0]["DefaultRateType"] == DBNull.Value)
                        DefaultSaleType = "";
                    else
                    {
                        DefaultSaleType = dt.Rows[0]["DefaultRateType"].ToString();
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }
        
        public void GET_DEFAULTCURRENCY()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = ComSet.getDEFAULTcurrency();
                tb_baseCur.Text = dt.Rows[0][0].ToString();
            }
            catch(Exception EX)
            {
                string s = EX.Message;
            }
            

        }
        
        public void GetBranchDetails()
        {

            try
            {
                DataTable dt = new DataTable();
                dt = ComSet.GetCurrentBranchDetails();
               

                Phone = dt.Rows[0][3].ToString();
                Email = dt.Rows[0][4].ToString();
                Fax = dt.Rows[0][5].ToString();
                tb_baseCur.Text = dt.Rows[0]["DEFAULT_CURRENCY_CODE"].ToString();

            }
            catch
            {
            }
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
        public void GetPurType()
        {
            //conn2.Open();
            //cmd.Connection = conn2;
            //cmd.CommandText = "SELECT CODE,DESC_ENG FROM GEN_PUR_TYPE";
            //cmd.CommandType = CommandType.Text;
            //cmd.Connection = conn;
            DataTable DT = invHdr.selecrGenPurType();
            //SqlDataAdapter ad = new SqlDataAdapter(cmd);
            //ad.Fill(DT);
            //conn2.Close();
            DataRow row = DT.NewRow();
            row[1] = "SELECT TYPE";
            DT.Rows.InsertAt(row, 0);
            cmbInvType.DataSource = DT;
            cmbInvType.ValueMember = "CODE";
            cmbInvType.DisplayMember = "DESC_ENG";

            SetPurType();

        }
        void SetPurType()
        {
            //cmd.Connection = conn;
            //conn.Open();
            //cmd.CommandText = "SELECT TOP 1 PUR_TYPE FROM SYS_SETUP";
            //cmd.CommandType = CommandType.Text;
            //cmd.Connection = conn;
            cmbInvType.SelectedValue = invHdr.getDefaultPurchaseType();
           // conn.Close();
        }
        int GetMaxVoucher()
        {
            int Vouch = 0;
            //cmd.Connection = conn;
            //conn.Open();
            //cmd.CommandText = "SELECT ISNULL(VoucherStart,1) FROM GEN_PUR_STARTFROM where VoucherTypeCode='" + cmbInvType.SelectedValue.ToString() + "'";
            //cmd.CommandType = CommandType.Text;
            //cmd.Connection = conn;
            //Vouch = Convert.ToInt32(cmd.ExecuteScalar());
            //conn.Close();
            invHdr.PurType = cmbInvType.SelectedValue == null ? "" : cmbInvType.SelectedValue.ToString();
             Vouch= Convert.ToInt32( invHdr.purVoucherStartFrom());
            return Vouch;
        }
        private void PurchaseMaster_Load(object sender, EventArgs e)
        {
            string taxType = Properties.Settings.Default.Tax_Type;
            if (!taxType.Contains("INDIA"))
            {
                dgvGSTTaxes.Visible = false;
            }
            decimalFormat = Common.getDecimalFormat();
            PRICE_FOB.Text = decimalFormat;
            txtMRP.Text = decimalFormat;
            tb_Wholesale.Text = decimalFormat;
            RTL_PRICE.Text = decimalFormat;
            ITEM_TAX.Text = decimalFormat;
            Class.CompanySetup CompStep = new Class.CompanySetup();
            if (ID == "")
            {
                GetPurType();
                DOC_DATE_GRE.Text = CompStep.GettDate();
            }
            string statelbl = General.GetStatelabelText();
            lblState.Text = (!statelbl.Equals("")) ? statelbl.First().ToString().ToUpper() + statelbl.Substring(1).ToLower() + ":" : lblState.Text;
            hasBatch = General.IsEnabled(Settings.Batch);
            hasTax = General.IsEnabled(Settings.Tax);
            hasPurExclusive = General.IsEnabled(Settings.Pur_Exclusive_tax);
            hasSaleExclusive = General.IsEnabled(Settings.Exclusive_tax);
            hasBarcode = General.IsEnabled(Settings.Barcode);
            HasArabic = General.IsEnabled(Settings.Arabic);
            PriceLastPurchase = General.IsEnabled(Settings.SelectLastPurchase);
            HasAccounts = Properties.Settings.Default.Account;
            hasPriceBatch = General.IsEnabled(Settings.priceBatch);
            loadStates();
           
            PrintPage.SelectedIndex = 1;
            if (!hasPurExclusive)
                chkInlusiveTax.Checked = true;
            else
                chkInlusiveTax.Checked = false;

            if (!HasArabic)
            {
                DOC_DATE_HIJ.Enabled = false;
            }
            pnlarabic.Visible = hasTax;
            uTaxPercent.Visible = hasTax;
            uTaxAmt.Visible = hasTax;
            uBatch.Visible = hasBatch;
            uExpDate.Visible = hasBatch;
            panBatch.Visible = hasBatch;
            panTax.Visible = hasTax;
            panBarcode.Visible = hasBarcode;
            pnlacct.Visible = false;
            PnlArabic2.Visible = HasArabic;
            growDgvGSTTaxes();
            if (ComSet.hasRoundoff())
            {
                checkBox1.Checked = true;
            }
            else
            {
                checkBox1.Checked = false;
            }
            if (!HasArabic)
            {
                tabControl1.TabPages.Remove(tbBarcode);
            }
            tabControl1.TabPages.Remove(tbBarcode);
            tabControl1.TabPages.Add(tbBarcode);
            PNLSERIAL.Visible = false;
            BindDetails();
            LoadProject();

            if (HasAccounts)
            {
                pnlacct.Visible = true;
            }
            getsalesman();
          
            AddColumnsTodgbarcodeprint();
            Generate_Barcode();
            GetCompanyDetails();
            GetBranchDetails();
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
            bindgridview();
            
            bindSubPrices();
            dgSubPrices.BringToFront();
            addRate_column();
            dgSubPrices.Visible = false;
            btnup.Enabled = false;
            if (PUR_FocusDate)
            {
                ActiveControl = DOC_DATE_GRE;
            }
            else if(PUR_FocusSupplier)
            {
                ActiveControl = SUPPLIER_CODE;
            }
            else if (PUR_FocusInvoice)
            {
                ActiveControl = SUPPLIER_INV;
            }
            else if (PUR_FocusReference)
            {
                ActiveControl = DOC_REFERENCE;
            }
            else
            {
                if (PUR_FocusBarcode)
                {
                    ActiveControl = BARCODE;
                }
                else if (PUR_FocusItemCode)
                {
                    ActiveControl = ITEM_CODE;
                }
                else
                {
                    ActiveControl = ITEM_NAME;
                }
            }
            PrintFormat.SelectedIndex = 0;
            drpCreditor.Enabled = false;
        //    PAY_CODE_TextChanged(sender, e);
            pnlacct.Enabled = false;
            dgSubPrices.Location = new Point(panUnit.Location.X + dgSubPrices.Width, dgItems.Location.Y + 30);
            dgvPriceList.Location = new Point(panUnit.Location.X + dgvPriceList.Width, dgItems.Location.Y + 30);
            dgvPriceList.Visible = false;
            dgItems.Columns["cSlno"].DisplayIndex = 0;
            FLAG_TAX = 0;
            chkDebit.Checked = true;

            if (cmbInvType.Text == "")
            {
                MessageBox.Show("Info: Some Default settings is missing.For easy use, update general settings..!", "Sysbizz", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void LoadProject()
        {
            cmb_projects.DataSource = ProjectDB.ProjectForCombo();
            cmb_projects.DisplayMember = "DESC_ENG";
            cmb_projects.ValueMember = "Id";
            cmb_projects.SelectedIndex = 0;
        }

        public void serialNo()
        {
            int slno=1;
            for (int i = 0; i <dgItems.RowCount; i++)
            {
                if (dgItems.Rows.Count > 0)
                {
                    dgItems.Rows[i].Cells["cSlno"].Value = slno.ToString();
                    slno++;
                }
            }
        }
        public void BindType()
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
                ArabicName = dt.Rows[0]["ARBCompany_Name"].ToString();
                logo = dt.Rows[0]["Logo"].ToString();
            }
        }
        
        public void BindCategory()
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
        
        public void BindGroup()
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
        
        public void BindTradeMark()
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
        
        public void getsalesman()
        {
             invHdr.Salesman= SalesManCode = lg.EmpId;
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.CommandText = "GetSalesMan";
            //cmd.Parameters.Clear();
            //cmd.Parameters.AddWithValue("@Empid", lg.EmpId);
            //conn.Open();
            //salesmanname = cmd.ExecuteScalar().ToString();
            salesmanname = invHdr.getSalesMan();
           //conn.Close();

        }
        DataTable dt2 = new DataTable();
        public void GetLedgers()
        {
            dt2.Clear();
            DataTable dt = new DataTable();
           
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
        public void bindSubPrices()
        {
            //   SqlCommand cmd = new SqlCommand();
            //cmd.Connection = conn;
            DataTable dt = invHdr.bindSubPrice();
            //cmd.CommandText = " SELECT CODE as [PRICE TYPES] FROM GEN_PRICE_TYPE";
            //cmd.CommandType = CommandType.Text ;
            //adapter.SelectCommand = cmd;
            //adapter.Fill(dt);
            dgSubPrices.AutoGenerateColumns = false;
            dgSubPrices.Columns[0].DataPropertyName = "PRICE TYPES";
            dgSubPrices.DataSource = dt;
           
        }
       
        public void bindgridview()
        {
            try
            {
            //    SqlCommand cmd = new SqlCommand();
            //cmd.Connection = conn;
                DataTable productDataTable = invHdr.itemsForSuggestion();
            //cmd.CommandText = "ItemSuggestion";
           // cmd.CommandText = "itemSuggestion_test";
            //cmd.CommandText = "item_suggestion_without_stock";
            //cmd.CommandType = CommandType.StoredProcedure;
            //adapter.SelectCommand = cmd;
            //adapter.Fill(productDataTable);
            //cmd.CommandType = CommandType.Text;
            source2.DataSource = productDataTable;
            dataGridItem.DataSource = source2;

            //source.DataSource = productDataTable;
            //dataGridItem.DataSource = source;
            dataGridItem.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridItem.Columns["ITEM_CODE"].HeaderText = "Item Code";
            dataGridItem.Columns["TaxId"].Visible = false;

            if (!hasArabic)
            {
                dataGridItem.Columns["DESC_ARB"].Visible = false;
            }

            if (!hasTax)
            {
                dataGridItem.Columns["TaxRate"].Visible = false;

            }
            dataGridItem.Columns["HASSERIAL"].Visible = false;
            dataGridItem.Columns["Type"].Visible = false;
            dataGridItem.Columns["Category"].Visible = false;
            dataGridItem.Columns["Group"].Visible = false;
            dataGridItem.Columns["Trademark"].Visible = false;
            dataGridItem.Columns["Stock"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridItem.Columns["TaxId"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridItem.Columns["ITEM NAME"].DisplayIndex = 1;

            dataGridItem.ClearSelection();
            }
            catch {}
        }
        
        public void BindDetails()
        {
            table.Rows.Clear();
            //SqlCommand cmd2 = new SqlCommand();
            //cmd2.Connection = conn2;
            ////     cmd.CommandText = "SELECT DOC_NO as 'DOC NO',DOC_DATE_GRE Date,DOC_DATE_HIJ 'Hij Date',DOC_REFERENCE,TAX_TOTAL as 'Tax Amount',SUPPLIER_CODE as 'Supplier Code',COUNTRY_CODE as 'Country Code',CURRENCY_CODE,NOTES,GROSS,NET_VAL as 'Net Value',PAY_CODE,CARD_NO,POSTED,STOCKED FROM INV_PURCHASE_HDR WHERE DOC_TYPE = '" + type + "'";
            //cmd2.CommandText = "SELECT DOC_NO as 'DOC NO',DOC_DATE_GRE Date,DOC_DATE_HIJ 'Hij Date',DOC_REFERENCE,TAX_TOTAL as 'Tax Amount',SUPPLIER_CODE as 'Supplier Code',COUNTRY_CODE as 'Country Code',CURRENCY_CODE,NOTES,GROSS,NET_VAL as 'Net Value',PAY_CODE,CARD_NO,POSTED,STOCKED FROM INV_PURCHASE_HDR WHERE DOC_NO='" + POSTID + "'";
            //adapter.SelectCommand = cmd2;
            //adapter.Fill(table);
            invHdr.DocNo = POSTID;
            table = invHdr.getHdr(); 
            source.DataSource = table;
            dgTrans.DataSource = source;
            dgTrans.Columns["DOC_REFERENCE"].Visible = false;
            dgTrans.Columns["CURRENCY_CODE"].Visible = false;
            dgTrans.Columns["CARD_NO"].Visible = false;
            dgTrans.Columns["PAY_CODE"].Visible = false;

            dgTrans.Columns["Country Code"].Visible = false;

            if (!HasArabic)
            {
                dgTrans.Columns["Hij Date"].Visible = false;
            }
           // adapter.SelectCommand = cmd;
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
                if (Convert.ToDouble(QTY_RCVD.Text) <= 0)
                {
                    MessageBox.Show("Purchase a Quantity greater than zero");
                    return false;

                }
            }
            catch
            {
                MessageBox.Show("Purchase a Quantity greater than zero");
                return false;
            }

            if (hasTax)
            {
                if (ITEM_TAX_PER.Text == "")
                {
                    ITEM_TAX_PER.Text = "0";
                }
            }


            if (ITEM_CODE.Text.Trim() != ""  && UOM.Text.Trim() != "" && QTY_RCVD.Text.Trim() != "" && PRICE_FOB.Text.Trim() != "")
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
        public void addRate_column()
        {

            //cmd.CommandText = "SELECT CODE,DESC_ENG FROM GEN_PRICE_TYPE";
            //conn.Open();
            SqlDataReader r = stkHdr.selecrGenPriceType();
            while (r.Read())
            {
                //dgRates.Columns.Add(r["CODE"].ToString(), r["DESC_ENG"].ToString());
                DataGridViewColumn col = new DataGridViewTextBoxColumn();
                col.Name = r["CODE"].ToString();
                col.HeaderText = r["CODE"].ToString();
                col.Width = 50;
                dgItems.Columns.Add(col);
            }
           // conn.Close();
            DbFunctions.CloseConnection();
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
                            //if (chkInlusiveTax.Checked)
                            //{
                            //    double pPrice = 0;
                            //    pricefob = Convert.ToDouble(dataGridItem.CurrentRow.Cells["PUR"].Equals(null) ? 0 : dataGridItem.CurrentRow.Cells["PUR"].Value);
                            //    double taxcalc = (Convert.ToDouble(ITEM_TAX_PER.Text) / 100) + 1;
                            //    pPrice = Convert.ToDouble(dataGridItem.CurrentRow.Cells["PUR"].Equals(null) ? 0 : dataGridItem.CurrentRow.Cells["PUR"].Value);
                            //    PRICE_FOB.Text = (pPrice / taxcalc).ToString();
                            //    // double ROUND = (pPrice / taxcalc);
                            //    // double rfig = Math.Round(ROUND, 2);
                            //    //  PRICE_FOB.Text = rfig.ToString() ;
                            //    // PRICE_FOB.Text = dataGridItem.CurrentRow.Cells["PUR"].Value.ToString();
                            //}
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
                            AddRates();
                            break;
                        case "ITEM_DISCOUNT":
                            txtMRP.Focus();
                            break;                          
                        case "txtMRP":
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
                            AddRates();
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
                            if (e.KeyCode == Keys.Down)
                            {
                              //  dgvPriceList.Visible = true;
                               // Visible = true;
                                dgSubPrices.Visible = false;

                            }
                            else
                            {
                                dgSubPrices.Visible = false;
                                AddRates();
                                dgvPriceList.Visible = false;
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
                                AddRates();
                                addItem();
                                ITEM_NAME.Focus();
                                AddRates();
                            }
                            else
                            {
                                addItem();
                                AddRates();
                                firstFocus();
                            }
                         break;
                        default:
                            break;
                    }
                }
                Common.preventDingSound(e);
            }
            else if (e.KeyCode == Keys.Down)
            {
                if (sender is KryptonTextBox)
                {
                    KryptonTextBox txtBox = (sender as KryptonTextBox);
                    switch (txtBox.Name)
                    {
                        case "RTL_PRICE":
                            //dgvPriceList.Visible = true;
                            dgSubPrices.Visible = true;
                            dgSubPrices.Focus();
                            dgSubPrices.CurrentCell = dgSubPrices.Rows[0].Cells[2];
                            break;

                    }
                }
                if (sender is KryptonComboBox)
                {
                    KryptonComboBox cmbx = (sender as KryptonComboBox);
                 
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
        public void AddRates()
        {
            for (int i = 0; i < dgSubPrices.Rows.Count; i++)
            {
                if (dgSubPrices.Rows[i].Cells[0].Value.ToString().Trim() == "RTL")
                {
                    dgSubPrices.Rows[i].Cells[2].Value = RTL_PRICE.Text;
                }
                else if (dgSubPrices.Rows[i].Cells[0].Value.ToString().Trim() == "WHL")
                {
                    dgSubPrices.Rows[i].Cells[2].Value = tb_Wholesale.Text;
                }
                else if (dgSubPrices.Rows[i].Cells[0].Value.ToString().Trim() == "PUR")
                {
                    dgSubPrices.Rows[i].Cells[2].Value = PRICE_FOB.Text;
                }
                else if (dgSubPrices.Rows[i].Cells[0].Value.ToString().Trim() == "MRP")
                {
                    dgSubPrices.Rows[i].Cells[2].Value = txtMRP.Text;
                }


            }
        }
        private void addItem()
        {
            if (ItemValid())
            {
                if (selectedRow == -1)
                {
                    selectedRow = dgItems.Rows.Add(new DataGridViewRow());
                    dgItems.CurrentCell = dgItems.Rows[selectedRow].Cells[0];
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
                c["colMRP"].Value = txtMRP.Text;
                if (hasTax)
                {
                    c["uTaxPercent"].Value = ITEM_TAX_PER.Text;
                    c["uTaxAmt"].Value = ITEM_TAX.Text;
                }
              //  c["uTotal"].Value =(Convert.ToDouble(ITEM_GROSS.Text)+Item_Discount_Amt).ToString(decimalFormat);
                c["uTotal"].Value = ((Convert.ToDouble(QTY_RCVD.Text) * Convert.ToDouble(PRICE_FOB.Text)) + Item_Discount_Amt).ToString(decimalFormat);
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
                c["uomQty"].Value = tableUnits.Select("UNIT_CODE = '" + UOM.Text + "'").First()["PACK_SIZE"];

                if (tb_baseCur.Text != "" && CURCODE.Text != "" && currencyflag==true)
                {
                    DataTable dt = new DataTable();
                    //cmd.Connection = conn;
                    //conn.Open();
                    //cmd.CommandText = "SELECT RATE FROM GEN_CURRENCY WHERE CODE='" + CURCODE.Text + "'";
                    invHdr.CurrencyCode = CURCODE.Text;
                    curre = invHdr.getCurrencyRate();
                    //cmd.CommandText = "SELECT RATE FROM GEN_CURRENCY WHERE CODE='" + tb_baseCur.Text + "'";
                    invHdr.CurrencyCode = tb_baseCur.Text;
                    basecur = invHdr.getCurrencyRate();
                   // conn.Close();
                    Double basec = Convert.ToDouble(basecur);
                    Double crate = Convert.ToDouble(curre);
                    Double add = Convert.ToDouble(c["uPrice"].Value.ToString()) + Convert.ToDouble(c["uPrice"].Value.ToString()) * (Convert.ToDouble(corrier) / 100);
                    Double qty = Convert.ToDouble(c["uQty"].Value.ToString());
                    Double netamt = add * qty;
                    Double retail = Convert.ToDouble(c["uRTL_PRICE"].Value.ToString());
                    Double Total = netamt;
                    Double grosstot = netamt;
                    Total = Total * (basec / crate);
                    add = add * (basec / crate);
                    grosstot = grosstot * (basec / crate);
                    string res = add.ToString("N" + decpoint);
                    retail = retail * (basec / crate);
                    string rtlreslt = retail.ToString("N" + decpoint);
                    string stotal = add.ToString("N" + decpoint);
                    string sgross = grosstot.ToString("N" + decpoint);
                    c["uRTL_PRICE"].Value = rtlreslt;
                    c["uPrice"].Value = res;
                    c["uTotal"].Value = sgross;
                    c["uNet_Amount"].Value = sgross;
                    if (hasTax)
                    {
                        Double taxamt = Convert.ToDouble(c["uTaxAmt"].Value.ToString());
                        taxamt = taxamt * (basec / crate);
                        string stax = taxamt.ToString("N" + decpoint);
                        c["uTaxAmt"].Value = stax;
                    }
                }

                c["cost_price"].Value = Convert.ToDouble(c["uNet_Amount"].Value) / (Convert.ToDouble(c["uomQty"].Value) * Convert.ToDouble(c["uQty"].Value));
                serialNo();
                int colm_count = dgItems.Columns.Count;
                for (int i = 0; i < dgSubPrices.Rows.Count;i++ )
                {
                    for (int j = 22; j < colm_count; j++)
                    {
                        if (dgSubPrices.Rows[i].Cells[0].Value.ToString() == dgItems.Columns[j].HeaderText)
                        {
                            double rate = 0;
                            if (dgSubPrices.Rows[i].Cells[2].Value == null || dgSubPrices.Rows[i].Cells[2].Value == "")
                            {
                                rate = 0;
                            }
                            else
                            {
                                rate =Convert.ToDouble( dgSubPrices.Rows[i].Cells[2].Value);
                            }
                            c[dgItems.Columns[j].Name.ToString()].Value = rate.ToString();
                        }
                    }

                }
                totalItemAmount();
                CalculateTotals();
                ItemClear();
              
                ActiveForm = true;
                calculateGSTTaxes();
            }
          
        }
        
        //public DataTable getunitdata(string CODE)
        //{
        //    DataTable dt = new DataTable();
        //    string co = CODE;
        //    cmd.CommandText = "SELECT * FROM TB_UNITMESSURMENT where ITEM_CODE='" + co + "'";
        //    cmd.Connection = conn;
        //    adapter.SelectCommand = cmd;
        //    adapter.Fill(dt);
        //    return dt;
        //}
        
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
            txtMRP.Text = "";
            PRICE_TYPE.Text = "";
            PriceType = "Percentage";
            LBLRATETYPE.Text = "Sale %";
            source2.Filter = "";
            ITEM_DISCOUNT.Text = "0";
            Item_Discount_Amt = 0;
            cntr = 0;
            pricefob = 0;
            FLAG_TAX = 0;
            DiscType = "Percentage";
            lblDiscRate.Text = "Disc %";
            excludechanged = false;
            includechang = false;
            lblPrice.Text = "Gr Price";
            tb_Wholesale.Text = "";
        }

        private void totalItemAmount()
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
                //total = total + Convert.ToDouble(dgItems.Rows[i].Cells["uTotal"].Value);
                total = total + Convert.ToDouble(dgItems.Rows[i].Cells["uNet_Amount"].Value);
             //   totaldiscount = totaldiscount + Convert.ToDouble(dgItems.Rows[i].Cells["uDiscount"].Value);
            }
            if (hasTax)
            {
                TOTAL_TAX_AMOUNT.Text = tax.ToString();
                CESS.Text = (tax * 0.01).ToString();
            }
            TOTAL_AMOUNT.Text = (total).ToString();
            DISCOUNT.Text = (totaldiscount).ToString();
        }
        int FLAG_TAX = 0;
        
        private void CalTotalAmount(object sender, EventArgs e)
        {
            try
            {
                if (FLAG_TAX==1)
                {
                    pricefob = Convert.ToDouble(PRICE_FOB.Text);
                }
                 
            }
            catch (Exception)
            {
             
            }
            if (PRICE_FOB.Text.Trim() != "" && QTY_RCVD.Text.Trim() != "")
            {
                ITEM_GROSS.Text = (Convert.ToDouble(QTY_RCVD.Text) * Convert.ToDouble(PRICE_FOB.Text) - Item_Discount_Amt).ToString(decimalFormat);
            }
            else
            {
                ITEM_GROSS.Text = decimalFormat;
            }

            if (ITEM_TAX_PER.Text != "")
            {
                double tax = Convert.ToDouble(ITEM_GROSS.Text) * (Convert.ToDouble(ITEM_TAX_PER.Text) / 100);
                ITEM_TAX.Text = tax.ToString(decimalFormat);
                ITEM_TAX_TextChanged(sender, e);
              //  double rtax = Math.Round(tax, 2);
               // ITEM_TAX.Text = rtax.ToString();
            }
            if (lblPrice.Text == "Gr Price:" || lblPrice.Text == "Gr Price")
            {
                excludechanged = false;
                includechang = true;
            }
            else
            {
                includechang = false;
                excludechanged = true;
            }
            //if (FLAG_TAX == 0)
            //{
            //    if (PRICE_FOB.Text != "")
            //    {
            //        pricefob = Convert.ToDouble(PRICE_FOB.Text);
            //    }
            //    else
            //    {
            //        FLAG_TAX = 0;
            //    }
            //}
            FLAG_TAX = 1;
        }

        private void TAX_PERCENT_TextChanged(object sender, EventArgs e)
        {
            if (ITEM_TAX_PER.Text == ".")
            {
                ITEM_TAX_PER.Text = "0.";
                ITEM_TAX_PER.Select(2, 0);
            }
            double total = 0;
            double tax = 0;  
            if (PRICE_FOB.Text.Trim() != "" && QTY_RCVD.Text.Trim() != "")
            {
                total = (Convert.ToDouble(QTY_RCVD.Text) * Convert.ToDouble(PRICE_FOB.Text))-Item_Discount_Amt;
            }

            if (ITEM_TAX_PER.Text.Trim() != "" && ITEM_TAX_PER.Text.Trim() != ".")
            {
                tax = ((Convert.ToDouble(ITEM_TAX_PER.Text) / 100) * total);
            }
            ITEM_TAX.Text = tax.ToString();
            ITEM_GROSS.Text = (total + tax).ToString(decimalFormat);
        }

        private void TOTAL_AMOUNT_TextChanged(object sender, EventArgs e)
        {
            //roundOffNettAmount();
            checkBox1_CheckedChanged(sender, e);
        }

        private void DISCOUNT_TextChanged(object sender, EventArgs e)
        {
            //roundOffNettAmount();
            checkBox1_CheckedChanged(sender, e);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            roundOffNettAmount();
           
        }

        private void roundOffNettAmount()
        {
            decimal freightValue = 0;
            decimal loadCharge = 0;
            try
            {
                freightValue = Convert.ToDecimal(Txt_freight.Text);
            }
            catch (Exception)
            {
                freightValue = 0;
            }
            try
            {
                loadCharge = Convert.ToDecimal(txtLoadingCharge.Text);
            }
            catch (Exception)
            {
                loadCharge = 0;
            }
            decimal totalValue = 0;
            try
            {
                totalValue = Convert.ToDecimal(TOTAL_AMOUNT.Text);
            }
            catch (Exception)
            {
                totalValue = 0;
            }

            decimal discountValue = 0;
            try
            {
                discountValue = Convert.ToDecimal(DISCOUNT.Text);
            }
            catch (Exception)
            {
                discountValue = 0;
            }

            decimal NET = totalValue - discountValue + freightValue+loadCharge;
            if (checkBox1.Checked)
            //if (checkBox1.Checked)
            {
                decimal[] roundedValues = Common.getRoundOffValues(NET);

                NETT_AMOUNT.Text = roundedValues[0].ToString(decimalFormat);
                txtRoundOff.Text = roundedValues[1].ToString(decimalFormat);
            }
            else
            {
                NETT_AMOUNT.Text = NET.ToString(decimalFormat);
                txtRoundOff.Text = decimalFormat;
            }
        }

        int TaxId;
        public void GetTaxRate()
        {
            //conn.Open();
            //cmd.CommandText = "SELECT TaxRate from GEN_TAX_MASTER where TaxId=" + TaxId;
            //cmd.CommandType = CommandType.Text;
            stkHdr.TaxId = TaxId.ToString();
            SqlDataReader r = stkHdr.selectTaxRate();
            while (r.Read())
            {
                ITEM_TAX_PER.Text = r[0].ToString();
            }
            //conn.Close();
            DbFunctions.CloseConnection();
        }
        
        public void GetRTlRate(string CO)
        {
            //conn.Open();
            //cmd.CommandText = "SELECT PRICE from INV_ITEM_PRICE_DF INNER JOIN SYS_SETUP ON DefaultRateType= SAL_TYPE where ITEM_CODE='" + CO + "' AND UNIT_CODE='"+UOM.Text+"'";
            //cmd.CommandType = CommandType.Text;
            invDtl.ItemCode = CO;
            invDtl.Uom = UOM.Text;
            try
            {
                RTL_PRICE.Text = invDtl.getPriceWithType();
            }
            catch (Exception)
            {
                MessageBox.Show("Please check the Default Sale/Purchase Type..!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
           // conn.Close();
        }
    
        private void btnItemCode_Click(object sender, EventArgs e)
        {
            ItemMasterHelp h = new ItemMasterHelp(0);
            if (h.ShowDialog() == DialogResult.OK && h.c != null)
            {
                BARCODE.Text = "";
                ITEM_CODE.Text = Convert.ToString(h.c[0].Value);
                ITEM_NAME.Text = Convert.ToString(h.c[1].Value);
                TaxId = Convert.ToInt32(h.c[29].Value);
                GetTaxRate();
                addUnits();
                PNL_DATAGRIDITEM.Visible = false;
            }
        }
        
        private void addUnits()
        {
            //SqlConnection conn2 = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
            //SqlCommand cmd2 = new SqlCommand();
            //cmd2.Connection = conn2;
            //SqlDataAdapter adapter2 = new SqlDataAdapter();
            //adapter2.SelectCommand = cmd2;
            tableUnits.Clear();
            //cmd2.CommandType = CommandType.Text;
            //cmd2.CommandText = "SELECT UNIT_CODE,PACK_SIZE FROM INV_ITEM_DIRECTORY_UNITS WHERE ITEM_CODE = @item_code";
            //cmd2.Parameters.AddWithValue("@item_code", ITEM_CODE.Text);
            //adapter2.Fill(tableUnits);
            stkDtl.ItemCode = ITEM_CODE.Text;
            tableUnits = stkDtl.selectUnits();
            UOM.DisplayMember = "UNIT_CODE";
            UOM.ValueMember = "UNIT_CODE";
            UOM.DataSource = tableUnits;
            
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
                DateTime date=DateTime.Now;
                DataTable dt=new DataTable();
                dt = dset.getdatdetails();
                switch (dt.Rows[0][3].ToString())
                { 
                    case "Date":
                        date = Convert.ToDateTime(dt.Rows[0][1].ToString());
                        break;
                    case "Period":
                        int days=0;
                        switch (dt.Rows[0][4].ToString())
                        {
                            case "Y":
                                days = 365 * Convert.ToInt16(dt.Rows[0][2].ToString())*-1;
                                break;
                            case "M":
                                days = 30 * Convert.ToInt16(dt.Rows[0][2].ToString())*-1;
                                break;
                            case "D":
                                
                                days =Convert.ToInt16(dt.Rows[0][2].ToString())*-1;
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
            if (drpCreditor.Text == drpdebitor.Text)
            {
                MessageBox.Show("Choose different accounts for transaction");
                drpdebitor.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(CARD_NO.Text) && PAY_CODE.Text != "CSH")
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


            if (HasAccounts)
            {
                if (drpCreditor.SelectedIndex<0 || drpdebitor.SelectedIndex<0)
                {
                    MessageBox.Show("Please select debitor and creditor accounts");
                    return false;
                }
                else
                {

                    return true;
                }
            }
            else
            {
                return true;
            }

           
        }
        public void UpdateRtl_Price()
        {
            try
            {
                SqlCommand cmd1 = new SqlCommand();
                string updatequery = "";
                for(int i=0;i<dgItems.Rows.Count;i++)
                {
                    string ItemCode = dgItems.Rows[i].Cells["uCode"].Value.ToString();
                    string UnitCode = dgItems.Rows[i].Cells["uUnit"].Value.ToString();
                   decimal Price=Convert.ToDecimal(dgItems.Rows[i].Cells["uRTL_PRICE"].Value);
                
                   updatequery = updatequery+"UPDATE INV_ITEM_PRICE SET  PRICE = '"+Price+"' WHERE (SAL_TYPE = 'RTL') AND (ITEM_CODE = '"+ItemCode+"') AND (UNIT_CODE = '"+UnitCode+"');";
                }
               // cmd1.CommandText = updatequery;
                //.ExecuteNonQuery();
                DbFunctions.InsertUpdate(updatequery);
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }
        public void UpdatePur_Price()
        {
            try
            {
                string updatequery = "";
                for (int i = 0; i < dgItems.Rows.Count; i++)
                {
                    string ItemCode = dgItems.Rows[i].Cells["uCode"].Value.ToString();
                    string UnitCode = dgItems.Rows[i].Cells["uUnit"].Value.ToString();
                    decimal Price = Convert.ToDecimal(dgItems.Rows[i].Cells["uPrice"].Value);

                    updatequery = updatequery + "UPDATE INV_ITEM_PRICE SET  PRICE = '" + Price + "' WHERE (SAL_TYPE = 'PUR') AND (ITEM_CODE = '" + ItemCode + "') AND (UNIT_CODE = '" + UnitCode + "');";
                }
               /// cmd.CommandText = updatequery;
               // cmd.ExecuteNonQuery();
                DbFunctions.InsertUpdate(updatequery);

            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            string branch = ComSet.ReadBranch();
            TransDate = DOC_DATE_GRE.Value;
            if (cmbInvType.SelectedIndex < 1)
            {
                MessageBox.Show("Select any purchase type");
                return;
            }
           
            if (PAY_CODE.Text == "CHQ")
            {
                TransDate = Convert.ToDateTime(CHQ_DATE.Value);
            }
            if (type != "LGR.PRT")
            {
                //if (SUPPLIER_CODE.Text == "")
                //{
                //    type = "PUR.CSS";
                //}
                //else
                //{
                //    type = "PUR.CRD";
                ////}
                if (chkDebit.Checked)
                {
                    type = "PUR.CSS";
                }
                else
                {
                    type = "PUR.CRD";
                }
            }

            if (valid())
            {
                if (DISCOUNT.Text == "")
                {
                    DISCOUNT.Text = "0";
                }
                string status = "Added!";
                string Query = "";
                if (ID == "")
                {
                    //insert

                    //DOC_NO.Text = General.generatePurchaseID();
                    GetMaxDocID();
                    POSTID = DOC_NO.Text;
                    Query= "INSERT INTO INV_PURCHASE_HDR(DOC_ID,DOC_TYPE,DOC_NO,DOC_DATE_GRE,DOC_DATE_HIJ,DOC_REFERENCE,SUPPLIER_CODE,NOTES,PAY_CODE,CARD_NO,"
                    +"TAX_TOTAL,CESS_AMOUNT,GROSS,DISCOUNT_VAL,NET_VAL,SUP_INV_NO,DEBITORACC,CREDITORACC,FREIGHT_AMT,SalesMan,BRANCH,PUR_TYPE,OTHER_EXPENSES,PROJECTID)"
                    +"VALUES(@doc_id, @doc_type, @doc_no, @doc_date_gre, @doc_date_hij, @doc_reference, @supplier_code, @notes, @pay_code, @card_no, @tax_total,"
                    + "@cess_amount, @gross, @discount_val, @net_val, @sup_inv_no, @debitoracc, @creditoracc, @freight_amt, @salesman, @branch,@PurType,@OTHEREXPENSES,@PROJECTID)";

                   
                   
                    parameters.Add("@doc_id", VOUCHNUM.Text);
                    parameters.Add("@doc_type", type);
                    parameters.Add("@doc_no", DOC_NO.Text);
                    parameters.Add("@doc_date_gre", DOC_DATE_GRE.Value);
                    parameters.Add("@doc_date_hij", DOC_DATE_HIJ.Text);
                    parameters.Add("@doc_reference", DOC_REFERENCE.Text);
                    parameters.Add("@supplier_code", SUPPLIER_CODE.Text);
                    parameters.Add("@notes", NOTES.Text);
                    parameters.Add("@pay_code", PAY_CODE.Text);
                    parameters.Add("@card_no", CARD_NO.Text);
                    parameters.Add("@tax_total", TOTAL_TAX_AMOUNT.Text);
                    parameters.Add("@cess_amount", CESS.Text);
                    parameters.Add("@gross", TOTAL_AMOUNT.Text);
                    parameters.Add("@discount_val", DISCOUNT.Text);
                    parameters.Add("@net_val", NETT_AMOUNT.Text);
                    parameters.Add("@sup_inv_no", SUPPLIER_INV.Text);
                    parameters.Add("@debitoracc", drpdebitor.SelectedValue);
                    parameters.Add("@creditoracc", drpCreditor.SelectedValue);
                    parameters.Add("@freight_amt", Txt_freight.Text);
                    parameters.Add("@salesman", SalesManCode);
                    parameters.Add("@branch", lg.Branch);
                    parameters.Add("@PurType", cmbInvType.SelectedValue.ToString());
                    parameters.Add("@OTHEREXPENSES", txtLoadingCharge.Text);
                    parameters.Add("@PROJECTID", cmb_projects.SelectedValue);
                }
                else
                {
                   
                    DeleteTransation();
                   
                    ModifyType = "Update";
                    modifiedtransaction();
                  
                    //SqlCommand reduceStockCommand = new SqlCommand();
                    //reduceStockCommand.Connection = conn;
                    //conn.Open();
                   // reduceStockCommand.CommandText = "SELECT ITEM_CODE, QTY_RCVD, UOM_QTY, cost_price,PRICE_BATCH FROM INV_PURCHASE_DTL WHERE DOC_NO = '"+DOC_NO.Text+"' AND DOC_TYPE = '"+type+"'";
                    stkdb.DocNo = DOC_NO.Text;
                    stkdb.DocType = type;
                   // SqlDataReader r = reduceStockCommand.ExecuteReader();
                    DataTable dt = stkdb.SelectDataForReduceStkWithTypeFromPur();
                    StockEntry se = new StockEntry();

                   
                     
                    //while (r.Read())
                    //{
                        //double qty = -1 * (Convert.ToDouble(r["QTY_RCVD"]) * Convert.ToDouble(r["UOM_QTY"]));
                        //if (type.Equals("LGR.PRT"))
                        //{
                        //    qty = 1 * qty;
                        //}
                        //se.addStockWithBatch(Convert.ToString(r["ITEM_CODE"]), Convert.ToString(qty), Convert.ToString(r["cost_price"]), Convert.ToString(r["PRICE_BATCH"])); 
                  //  }

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        double qty = -1 * (Convert.ToDouble(dt.Rows[i]["QTY_RCVD"]) * Convert.ToDouble(dt.Rows[i]["UOM_QTY"]));
                        se.addStockWithBatch(Convert.ToString(dt.Rows[i]["ITEM_CODE"]), Convert.ToString(qty), Convert.ToString(dt.Rows[i]["cost_price"]), Convert.ToString(dt.Rows[i]["PRICE_BATCH"]));

                    }
                   
                   // conn.Close();


                    Query = "UPDATE INV_PURCHASE_HDR SET DOC_DATE_GRE = '" + DOC_DATE_GRE.Value.ToString("yyyy/MM/dd") + "',DOC_DATE_HIJ = '" + DOC_DATE_HIJ.Text + "',DOC_TYPE= '" + type
                        + "',DOC_REFERENCE = '" + DOC_REFERENCE.Text + "',SUPPLIER_CODE = '" + SUPPLIER_CODE.Text + "',NOTES = '" + NOTES.Text + "',PAY_CODE = '" + PAY_CODE.Text + "',CARD_NO = '" + CARD_NO.Text + "',TAX_TOTAL = '" + Convert.ToDecimal(TOTAL_TAX_AMOUNT.Text) + "',CESS_AMOUNT = '" + float.Parse(CESS.Text) + "',GROSS = '" + Convert.ToDecimal(TOTAL_AMOUNT.Text) + "',DISCOUNT_VAL = '" + Convert.ToDecimal(DISCOUNT.Text) + "',NET_VAL = '" + Convert.ToDecimal(NETT_AMOUNT.Text) + "',SUP_INV_NO='" + SUPPLIER_INV.Text + "',POSTED='" + "N" + "',FREIGHT_AMT='" + Convert.ToDecimal(Txt_freight.Text) + "',SalesMan='" + SalesManCode + "',PUR_TYPE='" + cmbInvType.SelectedValue.ToString() + "',OTHER_EXPENSES='" + Convert.ToDecimal(txtLoadingCharge.Text) + "',PROJECTID='" + cmb_projects.SelectedValue + "' WHERE DOC_NO = '" + ID + "';DELETE FROM INV_PURCHASE_DTL WHERE DOC_NO = '" + ID + "';DELETE FROM INV_STK_TRX_HDR WHERE DOC_REFERENCE='" + ID + "';DELETE FROM INV_STK_TRX_DTL WHERE DOC_REFERENCE='" + ID + "';";
                    status = "Updated!";
                }

                #region insert purchase detail
                Query+= "INSERT INTO INV_PURCHASE_DTL(DOC_TYPE,DOC_NO,DOC_ID,ITEM_CODE,ITEM_DESC_ENG,UOM,QTY_RCVD,PRICE_FOB,SERIALNO,RTL_PRICE,DiscType,DiscountAmt,DiscValue,ITEM_DISCOUNT,NET_AMOUNT,BRANCH";
                //validation here
                Query += ",PRICE_BATCH";
                if (hasBatch)
                {
                    Query += ",BATCH,EXPIRY_DATE";
                }
                if (hasTax)
                {
                    Query += ",ITEM_TAX_PER,ITEM_TAX";
                }
                Query += ",UOM_QTY, cost_price";
                Query += ")";

                StockEntry stockEntry = new StockEntry();

                for (int i = 0; i < dgItems.Rows.Count; i++)
                {
                   
                    DataGridViewCellCollection c = dgItems.Rows[i].Cells;
                    string item_id = Convert.ToString(c["uCode"].Value);
                    double qty = Convert.ToDouble(c["uQty"].Value);
                    double uom_qty = Convert.ToDouble(c["uomQty"].Value);
                    double cost_wot_tax = Convert.ToDouble(c["uPrice"].Value);
                    double total_qty = qty * uom_qty;
                    double c_price = Convert.ToDouble(c["uNet_Amount"].Value)/total_qty;
                    double MRP = Convert.ToDouble(c["colMRP"].Value) / uom_qty ;
                    string unit_code = c["uUnit"].Value.ToString();
                    DataTable dt_rates = new DataTable();
                    dt_rates.Columns.Add("Rate_type", typeof(string));
                    dt_rates.Columns.Add("rate", typeof(double));
                    for (int j = 22; j < dgItems.Columns.Count; j++)
                    {
                        DataRow dRow = dt_rates.NewRow();
                        dRow[0] = dgItems.Columns[j].HeaderText;
                        if (dgItems.Columns[j].HeaderText == "PUR")
                        {
                            dRow[1] = c_price;
                        }
                        else
                        {
                            dRow[1] = Convert.ToDouble(dgItems.Rows[i].Cells[j].Value) / uom_qty;
                        }
                        dt_rates.Rows.Add(dRow);
                    }
                    //for price batch
                    string price_batch = "";
                    DateTime Exdat=new DateTime();
                    if (c["uExpDate"].Value!=null)
                    Exdat = DateTime.ParseExact(c["uExpDate"].Value.ToString(), "dd/MM/yyyy", null);
                   
                    string flag = "";
                    if (c["uBatch"].Value == null)
                        flag = "false";
                    else
                        flag = "true";
                    if (c["uBatch"].Value != null)
                    price_batch = c["uBatch"].Value.ToString();
                    //if (c["colBATCH"].Value != null)
                    //    price_batch = c["colBATCH"].Value.ToString();
                    //else
                    //    price_batch = "";
                    if (type.Equals("LGR.PRT"))
                    {
                        total_qty = -1 * total_qty;
                    }
                    string PRICE_BATCH = stockEntry.addStock_with_batch(item_id, total_qty.ToString(), c_price.ToString(), SUPPLIER_CODE.Text, MRP, dt_rates, unit_code, price_batch, Exdat, flag, hasPriceBatch);
                    dgItems.Rows[i].Cells["colBATCH"].Value = PRICE_BATCH;

                    Query += " SELECT '" + type + "','" + DOC_NO.Text + "','" + VOUCHNUM.Text + "','" + c["uCode"].Value + "','" + c["uName"].Value + "','" + c["uUnit"].Value + "','" + Convert.ToDouble(c["uQty"].Value) + "','" + c["uPrice"].Value + "','" + c["SerialNos"].Value + "','" + c["uRTL_PRICE"].Value + "','" + c["uDiscType"].Value + "','" + c["uDiscount"].Value + "','" + c["uDiscValue"].Value + "','" + c["uDiscount"].Value + "','" + c["uNet_Amount"].Value + "','" + lg.Branch + "','" + PRICE_BATCH + "'";
                    if (hasBatch)
                    {
                        Query += ",'" + c["uBatch"].Value + "','" + DateTime.ParseExact(c["uExpDate"].Value.ToString(), "dd/MM/yyyy", null).ToString("yyyy/MM/dd") + "'";
                    }
                    if (hasTax)
                    {
                        Query += ",'" + (Convert.ToString(c["uTaxPercent"].Value).Equals("") ? "0" : Convert.ToString(c["uTaxPercent"].Value)) + "','" + c["uTaxAmt"].Value + "'";
                    }
                    Query += ", '" + c["uomQty"].Value + "', '" + c["cost_price"].Value + "'";
                    Query += " UNION ALL ";
                    //string item_id = Convert.ToString(c["uCode"].Value);
                    //double qty = Convert.ToDouble(c["uQty"].Value);
                    //double uom_qty = Convert.ToDouble(c["uomQty"].Value);
                    //double total_qty = qty * uom_qty;
                    //double c_price = Convert.ToDouble(c["uNet_Amount"].Value) / total_qty;
                    //double MRP = Convert.ToDouble(c["colMRP"].Value);
                    //if (type.Equals("LGR.PRT"))
                    //{
                    //    total_qty = -1 * total_qty;
                    //}
                    //stockEntry.addStock(item_id, total_qty.ToString(), c_price.ToString(), SUPPLIER_CODE.Text, MRP);
                }

                Query = Query.Substring(0, Query.Length - 10);
                //cmd.CommandText += query;
                #endregion

                //conn.Open();
                //cmd.ExecuteNonQuery();
                //conn.Close();
                DbFunctions.InsertUpdate(Query,parameters);
                //SqlConnection conn2 = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
                //conn2.Open();
                //for (int i = 0; i < dgItems.Rows.Count; i++)
                //{
                //    DataGridViewCellCollection c = dgItems.Rows[i].Cells;
                //    cmd.Connection = conn;
                //    conn.Open();
                //    cmd.CommandText = "SELECT PRICE from INV_ITEM_PRICE  where ITEM_CODE='" + c["uCode"].Value + "' AND SAL_TYPE='MRP' AND STATUS=1";
                //    cmd.CommandType = CommandType.Text;
                //    SqlDataReader r = cmd.ExecuteReader();
                //    while (r.Read())
                //    {
                //        string price = r[0].ToString();
                //        if (price != "0.00" || price == "0.00")
                //        {
                //            if ((Convert.ToDecimal(c["colMRP"].Value) != Convert.ToDecimal(price)))
                //            {
                //                if (DialogResult.Yes == MessageBox.Show("Do you want to update MRP Price?" + c["uName"].Value, "MRP Price Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                //                {

                //                    SqlCommand cmd1 = new SqlCommand();
                //                    cmd1.Connection = conn2;
                //                    string updatequery = "";
                //                    for (int j = 0; j < dgItems.Rows.Count; j++)
                //                    {
                //                        string ItemCode = dgItems.Rows[i].Cells["uCode"].Value.ToString();
                //                        string UnitCode = dgItems.Rows[i].Cells["uUnit"].Value.ToString();
                //                        decimal Price = Convert.ToDecimal(dgItems.Rows[i].Cells["uRTL_PRICE"].Value);
                //                        string batch = dgItems.Rows[i].Cells["colBATCH"].Value.ToString();
                //                        updatequery = updatequery + "UPDATE INV_ITEM_PRICE SET STATUS=0 WHERE (ITEM_CODE='" + ItemCode + "') AND (SAL_TYPE='MRP');";
                //                        updatequery = updatequery + "UPDATE INV_ITEM_PRICE SET  STATUS = 1 WHERE (SAL_TYPE = 'MRP') AND (ITEM_CODE = '" + ItemCode + "') AND (BATCH_ID = '" + batch + "');";
                //                    }
                //                    cmd1.CommandText = updatequery;
                //                    cmd1.ExecuteNonQuery();
                //                }
                //            }
                //        }
                //    }
                //    conn.Close();
                //}
                //conn2.Close();
                if (DefaultSaleType != "")
                {
                    //SqlConnection conn2 = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
                    //conn2.Open();
                    for (int i = 0; i < dgItems.Rows.Count; i++)
                    {
                        DataGridViewCellCollection c = dgItems.Rows[i].Cells;
                        //cmd.Connection = conn;
                        //conn.Open();
                        //cmd.CommandText = "SELECT PRICE from INV_ITEM_PRICE_DF  where ITEM_CODE='" + c["uCode"].Value + "' AND SAL_TYPE='" + DefaultSaleType + "' and UNIT_CODE='" + dgItems.Rows[i].Cells["uUnit"].Value.ToString() + "'";
                        //cmd.CommandType = CommandType.Text;
                        //SqlDataReader r = cmd.ExecuteReader();
                        invDtl.ItemCode = c["uCode"].Value.ToString();
                        invDtl.PriceTypes = DefaultSaleType;
                        invDtl.Uom = dgItems.Rows[i].Cells["uUnit"].Value.ToString();
                        DataTable dt = invDtl.getPriceWithAllType();

                        for (int P = 0; P < dt.Rows.Count; P++)
                      
                        {
                            string price = dt.Rows[0][0].ToString();
                            if (price != "0.00" || price == "0.00")
                            {
                                if ((Convert.ToDecimal(c["uRTL_PRICE"].Value) != Convert.ToDecimal(price)))
                                {
                                    if (DialogResult.Yes == MessageBox.Show("Do you want to update Retail Price?" + c["uName"].Value, "Retail Price Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                                    {
                                        //SqlCommand cmd1 = new SqlCommand();
                                        //cmd1.Connection = conn2;
                                        string updatequery = "";
                                        for (int j = 0; j < dgItems.Rows.Count; j++)
                                        {
                                            string ItemCode = dgItems.Rows[P].Cells["uCode"].Value.ToString();
                                            string UnitCode = dgItems.Rows[P].Cells["uUnit"].Value.ToString();
                                            decimal Price = Convert.ToDecimal(dgItems.Rows[i].Cells["uRTL_PRICE"].Value);
                                            updatequery = updatequery + "UPDATE INV_ITEM_PRICE_DF SET  PRICE = '" + Price + "' WHERE (SAL_TYPE ='" + DefaultSaleType + "') AND (ITEM_CODE = '" + ItemCode + "') AND (UNIT_CODE = '" + UnitCode + "');";
                                        }
                                        //cmd1.CommandText = updatequery;
                                        //cmd1.ExecuteNonQuery();
                                        DbFunctions.InsertUpdate(updatequery);
                                    }
                                }
                            }
                        }
                        //conn.Close();
                    }
                    //conn2.Close();
                }


                for (int i = 0; i < dgItems.Rows.Count; i++)
                {
                    DataGridViewCellCollection c = dgItems.Rows[i].Cells;
                    //conn.Open();
                    //cmd.Connection = conn;
                    //cmd.CommandText = "SELECT PRICE from INV_ITEM_PRICE_DF  where ITEM_CODE='" + c["uCode"].Value + "' AND SAL_TYPE='PUR' and UNIT_CODE='" + dgItems.Rows[i].Cells["uUnit"].Value.ToString() + "' ";
                    //cmd.CommandType = CommandType.Text;
                    //SqlDataReader r = cmd.ExecuteReader();
                    invDtl.ItemCode = c["uCode"].Value.ToString();
                    invDtl.PriceTypes = "PUR";
                    invDtl.Uom = dgItems.Rows[i].Cells["uUnit"].Value.ToString();
                    DataTable dt = invDtl.getPriceWithAllType();
                    for (int P = 0; P < dt.Rows.Count; P++)
                    {
                        double c_price = 0;
                        string price = dt.Rows[0][0].ToString();
                        string old_price = price;
                        if (price != "0.00" || price == "0.00")
                        {
                                double qty = Convert.ToDouble(c["uQty"].Value);
                                double uom_qty = Convert.ToDouble(c["uomQty"].Value);
                                double cost_wot_tax = Convert.ToDouble(c["uPrice"].Value);
                                double total_qty = qty;
                                 c_price = Convert.ToDouble(c["uNet_Amount"].Value) / total_qty;
                                 if (hasPurExclusive)
                                 {
                                     c_price = Convert.ToDouble(c["uPrice"].Value);
                                 }
                            if ((c_price != Convert.ToDouble(price)))
                            {
                                if (DialogResult.Yes == MessageBox.Show("Do you want to update Purchase Price?" + c["uName"].Value, "Purchase Price Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                                {
                                    try
                                    {
                                        //SqlCommand cmd1 = new SqlCommand();
                                        //cmd1.Connection = conn2;
                                        string updatequery = "";
                                        for (int j = 0; j < dgItems.Rows.Count; j++)
                                        {
                                            string ItemCode = dgItems.Rows[P].Cells["uCode"].Value.ToString();
                                            string UnitCode = dgItems.Rows[P].Cells["uUnit"].Value.ToString();
                                            decimal Price = Convert.ToDecimal(dgItems.Rows[P].Cells["uPrice"].Value);

                                        //    updatequery = updatequery + "UPDATE INV_ITEM_PRICE_DF SET  PRICE = '" + Convert.ToString((Convert.ToDecimal(c["uNet_Amount"].Value) / Convert.ToDecimal(c["uQty"].Value))) + "'  WHERE (SAL_TYPE = 'PUR') AND (ITEM_CODE = '" + ItemCode + "') AND (UNIT_CODE = '" + UnitCode + "');";
                                            updatequery = updatequery + "UPDATE INV_ITEM_PRICE_DF SET  PRICE = '" + Convert.ToString(c_price) + "'  WHERE (SAL_TYPE = 'PUR') AND (ITEM_CODE = '" + ItemCode + "') AND (UNIT_CODE = '" + UnitCode + "');";
                                            //updatequery = updatequery + "INSERT INTO RateChange(Item_code,datee,Price,Qty) VALUES('" + dgItems.Rows[i].Cells["uCode"].Value.ToString() + "','" + DateTime.Now.ToString() + "','" + old_price + "','" + dgItems.Rows[i].Cells["uQty"].Value.ToString() + "')";
                                        }
                                        //conn2.Open();
                                        //cmd1.CommandText = updatequery;
                                        //cmd1.ExecuteNonQuery();
                                        //conn2.Close();
                                        DbFunctions.InsertUpdate(updatequery);
                                    }
                                    catch (Exception ee)
                                    {
                                        MessageBox.Show(ee.Message);
                                    }
                                }
                            }
                        }
                    }
                 //   conn.Close();
                }


                //SqlConnection conn3 = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
                //conn3.Open();
                //for (int i = 0; i < dgItems.Rows.Count; i++)
                //{
                //    DataGridViewCellCollection c = dgItems.Rows[i].Cells;
                //    cmd.Connection = conn;
                //    conn.Open();
                //    cmd.CommandText = "SELECT PRICE from INV_ITEM_PRICE  where ITEM_CODE='" + c["uCode"].Value + "' AND SAL_TYPE='PUR' AND STATUS=1";
                //    cmd.CommandType = CommandType.Text;
                //    SqlDataReader r = cmd.ExecuteReader();
                //    while (r.Read())
                //    {
                //        string price = r[0].ToString();
                //        if (price != "0.00" || price == "0.00")
                //        {
                //            if ((Convert.ToDecimal(c["uPrice"].Value) != Convert.ToDecimal(price)))
                //            {
                //                if (DialogResult.Yes == MessageBox.Show("Do you want to update Purchase Price?" + c["uName"].Value, "PUR Price Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                //                {

                //                    SqlCommand cmd2 = new SqlCommand();
                //                    cmd2.Connection = conn3;
                //                    string updatequery = "";
                //                    for (int j = 0; j < dgItems.Rows.Count; j++)
                //                    {
                //                        string ItemCode = dgItems.Rows[i].Cells["uCode"].Value.ToString();
                //                        string UnitCode = dgItems.Rows[i].Cells["uUnit"].Value.ToString();
                //                        decimal Price = Convert.ToDecimal(dgItems.Rows[i].Cells["uPrice"].Value);
                //                        string batch = dgItems.Rows[i].Cells["colBATCH"].Value.ToString();
                //                        updatequery = updatequery + "UPDATE INV_ITEM_PRICE SET STATUS=0 WHERE (ITEM_CODE='" + ItemCode + "') AND (SAL_TYPE='PUR');";
                //                        updatequery = updatequery + "UPDATE INV_ITEM_PRICE SET  STATUS = 1 WHERE (SAL_TYPE = 'PUR') AND (ITEM_CODE = '" + ItemCode + "') AND (BATCH_ID = '" + batch + "');";
                //                    }
                //                    cmd2.CommandText = updatequery;
                //                    cmd2.ExecuteNonQuery();
                //                }
                //            }
                //        }
                //    }
                //    conn.Close();
                //}
                //conn3.Close();







                //Batch generation for Updated Sales iF Need
                //if (ID != "")
                //{
                //    string ratequery = "";
                //    ratequery += "DELETE FROM RateChange where Refernce='" + VOUCHNUM.Text + "';";
                //    for (int i = 0; i < dgItems.Rows.Count; i++)
                //    {
                //        string ItemCode = dgItems.Rows[i].Cells["uCode"].Value.ToString();
                //        string UnitCode = dgItems.Rows[i].Cells["uUnit"].Value.ToString();
                //        decimal Price = Convert.ToDecimal(dgItems.Rows[i].Cells["uPrice"].Value);
                //        decimal rtlprice = Convert.ToDecimal(dgItems.Rows[i].Cells["uRTL_PRICE"].Value);  //uNet_Amount
                //        decimal avgprice = Convert.ToDecimal(dgItems.Rows[i].Cells["uPrice"].Value);
                //        ratequery = ratequery + "INSERT INTO RateChange(Item_code,datee,Price,sale_Price,Qty,Refernce) VALUES('" + dgItems.Rows[i].Cells["uCode"].Value.ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss tt") + "'," + avgprice + "," + rtlprice + ", '" + dgItems.Rows[i].Cells["uQty"].Value.ToString() + "','" + VOUCHNUM.Text + "')";
                //        conn.Open();
                //        cmd.CommandText = ratequery;
                //        cmd.ExecuteNonQuery();
                //        conn.Close();
                //    }

                //}
               //for (int i = 0; i < dgItems.Rows.Count; i++)
               // {
               //     string selectQuery = "";
               //     selectQuery = "SELECT * FROM INV_ITEM_PRICE WHERE ITEM_CODE='"+dgItems.Rows[i].Cells["uCode"].Value.ToString()+"'";
               //     SqlDataAdapter adapter = new SqlDataAdapter(selectQuery,conn);
               //     DataTable dt = new DataTable();
               //     adapter.Fill(dt);
               //     if (dt.Rows[0]["ITEM_CODE"].ToString() != dgItems.Rows[i].Cells["uCode"].Value.ToString() && dt.Rows[0]["PRICE"].ToString() != dgItems.Rows[i].Cells["colMRP"].Value.ToString() && dt.Rows[0]["UNIT_CODE"].ToString() != dgItems.Rows[i].Cells["uUnit"].Value.ToString())
               //     {
               //         string insertBatchQuery = "";
               //         //insertBatchQuery = "INSERT INTO INV_ITEM_PRICE_BATCH VALUES('"++"')";
               //     }
               // }

                InsertTransaction();
                //insert table for Currency Conversion
                if (tb_baseCur.Text != "" && CURCODE.Text != "")
                {
                    //conn.Open();
                    //cmd.Connection = conn;
                    //cmd.CommandText = "INSERT INTO tbl_curRepor (Invoice_no,Currency_code,Currency_amt) VALUES('" + VOUCHNUM.Text + "','" + CURCODE.Text + "','" + curre + "')";
                    //cmd.ExecuteNonQuery();
                    //conn.Close();
                    invHdr.DocNo = VOUCHNUM.Text;
                    invHdr.CurrencyCode = CURCODE.Text;
                    invHdr.ExchangeRate =Convert.ToDecimal( curre);
                    invHdr.insertCurrencyDetails();
                }

                if (printinvoice.Checked == true)
                {
                    printBill();
                }
                if (cmbInvType.Text != "ESTIMATE")
                {
                    if (Convert.ToDouble(TOTAL_TAX_AMOUNT.Text) > 0)
                    {
                        TaxTransaction();
                    }

                    if (Convert.ToDouble(EXCISE_DUTY.Text) > 0)
                    {
                        ExciseDutyTransaction();
                    }
                }
                if (Convert.ToDouble(DISCOUNT.Text) > 0)
                {
                    DiscountTransaction();
                }
                if (Convert.ToDouble(Txt_freight.Text) > 0)
                {
                    Freighttransaction();
                }
                if (Convert.ToDouble(txtLoadingCharge.Text) > 0)
                {
                    DataTable dt = Ledg.SelectledgerByName("LOADING CHARGE ON PURCHASE");
                   if (dt.Rows.Count>0)
	                {
                        loadingChargeTransaction(dt.Rows[0][0].ToString());

	                }
                  
                }
                //if (Convert.ToDouble(CESS.Text) > 0)
                //{
                //    CessTransaction();
                //}

                if (cb_Barcode.Checked == true)
                {
                    FillPrintingGridhasnotbatch();
                    barcodeprint();
                }
                MessageBox.Show("Record " + status);
                btnClear.PerformClick();

                //printing barcode as pdf
                if (hasBarcode)
                {
                    AddingToBacodeGrid();
                }
                BindDetails();
                PostingProduct();
            }
        }
        public void AddSerialNo()
        {
         //   conn.Open();
            string query = "INSERT INTO INV_SERIALS(ITEM_CODE,SERIAL_NO,DOC_NO1_HOLDING,PURCHASEDON) ";
            for (int i = 0; i < dgItems.Rows.Count; i++)
            {
                DataGridViewCellCollection c = dgItems.Rows[i].Cells;
                query += "SELECT '" + c["uCode"].Value + "','" + c["SerialNos"].Value + "','" + DOC_NO.Text + "','" + DOC_DATE_GRE.Value + "'";
                query += " UNION ALL";
            }
            query = query.Substring(0, query.Length - 10);
           // cmd.CommandText += query;
           // cmd.ExecuteNonQuery();
           // conn.Close();
            DbFunctions.InsertUpdate(query);
        }
        public void modifiedtransaction()
        {
            if (type == "LGR.PRT")
            {
                modtrans.VOUCHERTYPE = "Purchase Return";
            }
            else
            {
                modtrans.VOUCHERTYPE = "Purchase";
            }
            modtrans.Date = DOC_DATE_GRE.Value.ToString("MM/dd/yyyy"); 
            modtrans.USERID = SalesManCode;
            modtrans.VOUCHERNO = DOC_NO.Text;
            modtrans.NARRATION = NOTES.Text;
            modtrans.STATUS = ModifyType;
            modtrans.MODIFIEDDATE = DateTime.Now.ToString("MM/dd/yyyy");
            modtrans.BRANCH = lg.Branch;
            modtrans.INVOICENO = VOUCHNUM.Text;
            modtrans.insertTransaction();
        }
        public void InsertTransaction()
        {
            if (type == "LGR.PRT")
            {
                trans.VOUCHERTYPE = "Purchase Return";
            }
            else
            {
                trans.VOUCHERTYPE = "Purchase";
            }

            trans.DATED = TransDate.ToString("MM/dd/yyyy");
            trans.NARRATION = NOTES.Text;
            Login log = (Login)Application.OpenForms["Login"];
            trans.USERID = log.EmpId;
            trans.BRANCH = lg.Branch;
            trans.NARRATION = NOTES.Text;
            trans.ACCNAME = drpCreditor.Text;
           
            trans.VOUCHERNO = DOC_NO.Text;
            trans.ACCID = drpCreditor.SelectedValue.ToString();
            trans.CREDIT = "0";
            trans.DEBIT = grossValue.ToString();
            trans.SYSTEMTIME = DateTime.Now.ToString("MM/dd/yyyy");


            if (chkDebit.Checked && SUPPLIER_CODE.Text != "")
            {
                trans.PARTICULARS = txtSupplierName.Text;
              //  clsCustomer.Code = CUSTOMER_CODE.Text;
                //trans.ACCID = clsCustomer.getIndividualLedgerId();
            }
            else
            {
                //trans.ACCNAME = CASHACC.Text;
                //trans.ACCID = CASHACC.SelectedValue.ToString();
                trans.PARTICULARS = drpdebitor.Text;
            }

            trans.insertTransaction();
            trans.PARTICULARS = drpCreditor.Text;
           
            trans.VOUCHERNO = DOC_NO.Text;
           
            trans.DEBIT = "0";
            trans.CREDIT = NETT_AMOUNT.Text;
            trans.SYSTEMTIME = DateTime.Now.ToString();
            trans.BRANCH = lg.Branch;
            trans.PROJECTID =Convert.ToInt32(cmb_projects.SelectedValue);

            if (chkDebit.Checked && SUPPLIER_CODE.Text != "")
            {

                 trans.ACCNAME = txtSupplierName.Text;
                 clsSupplier.Code = SUPPLIER_CODE.Text;
                 trans.ACCID = clsSupplier.getIndividualLedgerId();
            }
            else
            {
                //trans.ACCNAME = CASHACC.Text;
                trans.ACCID = drpdebitor.SelectedValue.ToString();
                trans.ACCNAME = drpdebitor.Text;

            }
            trans.insertTransaction();
            if (chkDebit.Checked && SUPPLIER_CODE.Text != "")
            {
                InsertTransactionForCashType();
            }

            }
        public void InsertTransactionForCashType()
        {
            if (type == "LGR.PRT")
            {
                trans.VOUCHERTYPE = "Purchase Return";
            }
            else
            {
                trans.VOUCHERTYPE = "Purchase";
            }

            trans.DATED = TransDate.ToString("MM/dd/yyyy");
            trans.NARRATION = NOTES.Text;
            Login log = (Login)Application.OpenForms["Login"];
            trans.USERID = log.EmpId;
            trans.BRANCH = lg.Branch;
            trans.NARRATION = NOTES.Text;
            trans.ACCNAME = txtSupplierName.Text;

            trans.VOUCHERNO = DOC_NO.Text;
            trans.ACCID = clsSupplier.getIndividualLedgerId();
            trans.CREDIT = "0";
            trans.DEBIT = NETT_AMOUNT.Text;
            trans.SYSTEMTIME = DateTime.Now.ToString("MM/dd/yyyy");
            trans.PARTICULARS = drpdebitor.Text;


            trans.insertTransaction();
            trans.PARTICULARS = txtSupplierName.Text;

            trans.VOUCHERNO = DOC_NO.Text;

            trans.DEBIT = "0";
            trans.CREDIT = NETT_AMOUNT.Text;
            trans.SYSTEMTIME = DateTime.Now.ToString();
            trans.BRANCH = lg.Branch;
            trans.PROJECTID = Convert.ToInt32(cmb_projects.SelectedValue);
            trans.ACCID = drpdebitor.SelectedValue.ToString();
            trans.ACCNAME = drpdebitor.Text;

            
            trans.insertTransaction();


        }
        public void Freighttransaction()
        {
            if (type == "LGR.PRT")
            {
                trans.VOUCHERTYPE = "Purchase Return";
            }
            else
            {
                trans.VOUCHERTYPE = "Purchase";
            }

            trans.DATED = TransDate.ToString("MM/dd/yyyy");
            trans.NARRATION = NOTES.Text;
            Login log = (Login)Application.OpenForms["Login"];
            trans.USERID = log.EmpId;

            trans.NARRATION = NOTES.Text;
            trans.ACCNAME = "FREIGHT ON PURCHASE";
            trans.PARTICULARS = drpdebitor.Text;
            trans.VOUCHERNO = DOC_NO.Text;
            trans.BRANCH = lg.Branch;
            trans.ACCID = "59";

            trans.CREDIT = "0";

            trans.DEBIT = Txt_freight.Text;
            trans.SYSTEMTIME = DateTime.Now.ToString("MM/dd/yyyy");
            trans.insertTransaction();

            //trans.PARTICULARS = "FREIGHT ON PURCHASE";
            //trans.ACCNAME = "PU A/C";
            //trans.VOUCHERNO = DOC_NO.Text;

            //trans.ACCID = "85";

            //trans.BRANCH = lg.Branch;
            //trans.DEBIT = "0";
            //trans.CREDIT = Txt_freight.Text;
            //trans.SYSTEMTIME = DateTime.Now.ToString();
            //trans.insertTransaction();        
        }
        public void loadingChargeTransaction(string ID)
        {
            if (type == "LGR.PRT")
            {
                trans.VOUCHERTYPE = "Purchase Return";
            }
            else
            {
                trans.VOUCHERTYPE = "Purchase";
            }

            trans.DATED = TransDate.ToString("MM/dd/yyyy");
            trans.NARRATION = NOTES.Text;
            Login log = (Login)Application.OpenForms["Login"];
            trans.USERID = log.EmpId;

            trans.NARRATION = NOTES.Text;
            trans.ACCNAME = "LOADING CHARGE ON PURCHASE";
            trans.PARTICULARS = drpdebitor.Text;
            trans.VOUCHERNO = DOC_NO.Text;
            trans.BRANCH = lg.Branch;
            trans.ACCID = ID;

            trans.CREDIT = "0";

            trans.DEBIT = txtLoadingCharge.Text;
            trans.SYSTEMTIME = DateTime.Now.ToString("MM/dd/yyyy");
            trans.insertTransaction();

            //trans.PARTICULARS = "LOADING CHARGE ON PURCHASE";
            //trans.ACCNAME = "PU A/C";
            //trans.VOUCHERNO = DOC_NO.Text;

            //trans.ACCID = "85";

            //trans.BRANCH = lg.Branch;
            //trans.DEBIT = "0";
            //trans.CREDIT = txtLoadingCharge.Text;
            //trans.SYSTEMTIME = DateTime.Now.ToString();
            //trans.insertTransaction();
        }
        public void TaxTransaction()
        {
            if (type == "LGR.PRT")
            {
                trans.VOUCHERTYPE = "Purchase Return";
            }
            else
            {
                trans.VOUCHERTYPE = "Purchase";
            }

            trans.DATED = TransDate.ToString("MM/dd/yyyy");
            trans.NARRATION = NOTES.Text;
            Login log = (Login)Application.OpenForms["Login"];
            trans.USERID = log.EmpId;

            trans.NARRATION = NOTES.Text;
            trans.ACCNAME = (dt2.AsEnumerable().FirstOrDefault(p => p["LEDGERID"].ToString() == "66")["LEDGERNAME"].ToString());
            trans.PARTICULARS = drpdebitor.Text;
            trans.VOUCHERNO = DOC_NO.Text;

            trans.ACCID = "66";

            trans.CREDIT = "0";
            trans.BRANCH = lg.Branch;
            trans.DEBIT = TOTAL_TAX_AMOUNT.Text;
            trans.SYSTEMTIME = DateTime.Now.ToString("MM/dd/yyyy");
            trans.insertTransaction();

            //trans.PARTICULARS = (dt2.AsEnumerable().FirstOrDefault(p => p["LEDGERID"].ToString() == "66")["LEDGERNAME"].ToString());
            //trans.ACCNAME = "PU A/C";
            //trans.VOUCHERNO = DOC_NO.Text;

            //trans.ACCID = "85";

            //trans.BRANCH = lg.Branch;
            //trans.DEBIT = "0";
            //trans.CREDIT = TOTAL_TAX_AMOUNT.Text;
            //trans.SYSTEMTIME = DateTime.Now.ToString();
            //trans.insertTransaction();

        }
        public void ExciseDutyTransaction()
        {
            if (type == "LGR.PRT")
            {
                trans.VOUCHERTYPE = "Purchase Return";
            }
            else
            {
                trans.VOUCHERTYPE = "Purchase";
            }

            trans.DATED = TransDate.ToString("MM/dd/yyyy");
            trans.NARRATION = NOTES.Text;
            Login log = (Login)Application.OpenForms["Login"];
            trans.USERID = log.EmpId;

            trans.NARRATION = NOTES.Text;
            trans.ACCNAME = "INPUT EXICE DUTY";
            trans.PARTICULARS = drpdebitor.Text;
            trans.VOUCHERNO = DOC_NO.Text;

            trans.ACCID = "64";

            trans.CREDIT = "0";
            trans.BRANCH = lg.Branch;
            trans.DEBIT = EXCISE_DUTY.Text;
            trans.SYSTEMTIME = DateTime.Now.ToString("MM/dd/yyyy");
            trans.insertTransaction();

            //trans.PARTICULARS = "INPUT GST";
            //trans.ACCNAME = "PU A/C";
            //trans.VOUCHERNO = DOC_NO.Text;

            //trans.ACCID = "85";

            //trans.BRANCH = lg.Branch;
            //trans.DEBIT = "0";
            //trans.CREDIT = EXCISE_DUTY.Text;
            //trans.SYSTEMTIME = DateTime.Now.ToString();
            //trans.insertTransaction();
        }
        public void DiscountTransaction()
        {
            if (type == "LGR.PRT")
            {
                trans.VOUCHERTYPE = "Purchase Return";
            }
            else
            {
                trans.VOUCHERTYPE = "Purchase";
            }

            trans.DATED = TransDate.ToString("MM/dd/yyyy");
            trans.NARRATION = NOTES.Text;
            Login log = (Login)Application.OpenForms["Login"];
            trans.USERID = log.EmpId;

            trans.NARRATION = NOTES.Text;
            //trans.ACCNAME = "PU A/C";
            //trans.PARTICULARS = "INVOICE DISCOUNT RECIEVED";
            //trans.VOUCHERNO = DOC_NO.Text;

            //trans.ACCID = "85";

            //trans.CREDIT = "0";
            //trans.BRANCH = lg.Branch;
            //trans.DEBIT = DISCOUNT.Text;
            //trans.SYSTEMTIME = DateTime.Now.ToString("MM/dd/yyyy");
            //trans.insertTransaction();

            trans.PARTICULARS = drpdebitor.Text;
            trans.ACCNAME = "INVOICE DISCOUNT RECIEVED";
            trans.VOUCHERNO = DOC_NO.Text;

            trans.ACCID = "68";

            trans.BRANCH = lg.Branch;
            trans.DEBIT = "0";
            trans.CREDIT = DISCOUNT.Text;
            trans.SYSTEMTIME = DateTime.Now.ToString("MM/dd/yyyy");
            trans.insertTransaction();
        }
        public void CessTransaction()
        {
            if (type == "LGR.PRT")
            {
                trans.VOUCHERTYPE = "Purchase Return";
            }
            else
            {
                trans.VOUCHERTYPE = "Purchase";
            }

            trans.DATED = TransDate.ToString("MM/dd/yyyy");
            trans.NARRATION = NOTES.Text;
            Login log = (Login)Application.OpenForms["Login"];
            trans.USERID = log.EmpId;

            trans.NARRATION = NOTES.Text;
            trans.ACCNAME = "INPUT CESS";
            trans.PARTICULARS = drpdebitor.Text;
            trans.VOUCHERNO = DOC_NO.Text;

            trans.ACCID = "62";

            trans.CREDIT = "0";
            trans.BRANCH = lg.Branch;
            trans.DEBIT = CESS.Text;
            trans.SYSTEMTIME = DateTime.Now.ToString("MM/dd/yyyy");
            trans.insertTransaction();

            //trans.PARTICULARS = "INPUT CESS";
            //trans.ACCNAME = "PU A/C";
            //trans.VOUCHERNO = DOC_NO.Text;

            //trans.ACCID = "85";

            //trans.BRANCH = lg.Branch;
            //trans.DEBIT = "0";
            //trans.CREDIT = CESS.Text;
            //trans.SYSTEMTIME = DateTime.Now.ToString();
            //trans.insertTransaction();
        } 
        private void DeleteTransation()
        {
            trans.VOUCHERTYPE = "Purchase";
            trans.VOUCHERNO = ID;
            trans.DeletePurchaseTransaction();
        }
        public void Clear2()
        {
            pricefob=0;
            ID = "";
            DOC_NO.Text = "";
            DOC_DATE_GRE.Value = DateTime.Today;
            DOC_DATE_HIJ.Text = "";
            DOC_REFERENCE.Text = "";
            SUPPLIER_CODE.Text = "";
            txtSupplierName.Text = "";
           
            CARD_NO.Text = "";
            NOTES.Text = "";
            PRICE_FOB.Text = decimalFormat;
            txtMRP.Text = decimalFormat;
            RTL_PRICE.Text = decimalFormat;
            tb_Wholesale.Text = decimalFormat;
            ITEM_TAX.Text = decimalFormat;
            TOTAL_TAX_AMOUNT.Text = "0.00";
            CESS.Text = "0.00";
            TOTAL_AMOUNT.Text = "0.00";
            DISCOUNT.Text = "0.00";
            NETT_AMOUNT.Text = "0.00";
            Txt_freight.Text = "0.00";
            dgItems.Rows.Clear();
            SUPPLIER_INV.Text = "";
            if (type == "LGR.PRT")
                drpCreditor.Text = "CASH ACCOUNT";
            else
                drpCreditor.Text = "PURCHASE ACCOUNT";
            if (type == "LGR.PRT")
                drpdebitor.Text = "PURCHASE RETURN ACCOUNT";
            else
                drpdebitor.Text = "CASH ACCOUNT";
            BARCODE.Text = "";

            ModifyType = "";
            ItemClear();
            SUPPLIER_CODE.Focus();
            ActiveForm = false;
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            ID = "";
            DOC_NO.Text = "";
            pricefob = 0;
            cntr = 0;
            DOC_DATE_GRE.Value = DateTime.Today;
            DOC_DATE_HIJ.Text = "";
            DOC_REFERENCE.Text = "";
            SUPPLIER_CODE.Text = "";
            txtSupplierName.Text = "";
            txtMRP.Text = "";
            //PAY_CODE.Text = "";
            //PAY_NAME.Text = "";
            CARD_NO.Text = "";
            NOTES.Text = "";
            TOTAL_TAX_AMOUNT.Text = "0.00";
            CESS.Text = "0.00";
            TOTAL_AMOUNT.Text = "0.00";
            DISCOUNT.Text = "0.00";
            NETT_AMOUNT.Text = "0.00";
            Txt_freight.Text = "0.00";
            dgItems.Rows.Clear();
            SUPPLIER_INV.Text = "";
            txtLoadingCharge.Text = "0.00";
            if (type == "LGR.PRT")
            {
                drpCreditor.Text = "CASH ACCOUNT";
            }
            else
	        {
                drpCreditor.Text = "PURCHASE ACCOUNT";
	        }
                
            if (type == "LGR.PRT")
            {
                drpdebitor.Text = "PURCHASE RETURN ACCOUNT";
            }
            drpdebitor.Text = "CASH ACCOUNT";
            BARCODE.Text = "";
          
            ModifyType = "";
            ItemClear();
            SUPPLIER_CODE.Focus();
            checkvoucher(Convert.ToInt32(VOUCHNUM.Text));
            GetMaxDocID();
             ActiveForm = false;
             lblDiscRate.Text = "Disc %";
             DiscType = "Percentage";
             Item_Discount_Amt = 0;
             ITEM_DISCOUNT.Text = "0";
             tb_baseCur.Text = "";
             CURCODE.Text = "";
             tb_Wholesale.Text = "";
             this.Load += new EventHandler(PurchaseMaster_Load);
             cmb_projects.SelectedIndex = 0;
             chkDebit.Checked = true;
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (ID != "")
            {
                if (MessageBox.Show("Are You Sure To Delete Purchase?", "Record Deletion", MessageBoxButtons.YesNo) == DialogResult.Yes)
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
                    //SqlDataReader r = reduceStockCommand.ExecuteReader();
                    stkdb.DocNo = DOC_NO.Text;
                    stkdb.DocType = type;
                    // SqlDataReader r = reduceStockCommand.ExecuteReader();
                    DataTable dt = stkdb.SelectDataForReduceStkWithTypeFromPur();
                  
                    StockEntry se = new StockEntry();
                    //while (r.Read())
                    //{
                    //    double qty = -1 * (Convert.ToDouble(r["QTY_RCVD"]) * Convert.ToDouble(r["UOM_QTY"]));
                    //    if (type.Equals("LGR.PRT"))
                    //    {
                    //        qty = 1 * qty;
                    //    }
                    //    se.addStockWithBatch(Convert.ToString(r["ITEM_CODE"]), Convert.ToString(qty), Convert.ToString(r["cost_price"]), Convert.ToString(r["PRICE_BATCH"]));
                    //}
                    //conn.Close();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        double qty = -1 * (Convert.ToDouble(dt.Rows[i]["QTY_RCVD"]) * Convert.ToDouble(dt.Rows[i]["UOM_QTY"]));
                        if (type.Equals("LGR.PRT"))
                        {
                            qty = 1 * qty;
                        }
                        se.addStockWithBatch(Convert.ToString(dt.Rows[i]["ITEM_CODE"]), Convert.ToString(qty), Convert.ToString(dt.Rows[i]["cost_price"]), Convert.ToString(dt.Rows[i]["PRICE_BATCH"]));

                    }
                  //  conn.Open();
                    //cmd.CommandText = "UPDATE INV_PURCHASE_HDR SET FLAGDEL='FALSE'  WHERE DOC_NO = '" + id + "' ;UPDATE  INV_PURCHASE_DTL SET FLAGDEL='FALSE' WHERE DOC_NO = '" + id + "';DELETE FROM INV_STK_TRX_HDR WHERE DOC_REFERENCE = '" + id + "';DELETE FROM INV_STK_TRX_DTL WHERE DOC_REFERENCE = '" + id + "'";
                    //cmd.ExecuteNonQuery();
                    invHdr.deletePurchase();
                    MessageBox.Show("Record Deleted!");
                   // conn.Close();
                }

                btnClear.PerformClick();
            }
            else
            {
                MessageBox.Show("Please Select a Purhcase to Delete");
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
            //// cmd.CommandText = "DELETE FROM INV_PURCHASE_HDR WHERE DOC_NO = '" + id + "';DELETE FROM INV_PURCHASE_DTL WHERE DOC_NO = '" + id + "';DELETE FROM INV_STK_TRX_HDR WHERE DOC_REFERENCE = '" + id + "';DELETE FROM INV_STK_TRX_DTL WHERE DOC_REFERENCE = '" + id + "'";
            //cmd.ExecuteNonQuery();
            ////  MessageBox.Show("Record Deleted!");
            //conn.Close();
            invHdr.DocNo = id;
            invHdr.DocType = vchr;
            invHdr.insertDeletedTransactions();

        }
        string getState(string code)
        {
            //conn.Open();
            //cmd.CommandText = "SELECT STATE FROM PAY_SUPPLIER WHERE CODE="+code;
            //cmd.Connection = conn;
            invHdr.SupplierCode = code;
            string state = invHdr.getState();
           // conn.Close();
            return state;
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
             modtrans.Date= DOC_DATE_GRE.Value.ToString("MM/dd/yyyy"); 
                modtrans.USERID = lg.EmpId;
                modtrans.VOUCHERNO = ID;
                modtrans.NARRATION = "";
                modtrans.STATUS = "Delete";
                modtrans.MODIFIEDDATE = DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss");
                modtrans.INVOICENO = VOUCHNUM.Text;
                modtrans.BRANCH = lg.Branch;
                modtrans.Date = date;
                modtrans.insertTransaction();
            }
            catch(Exception c)
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
        private void btnQuit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ActiveForm)
                {
                    if (DialogResult.Yes == MessageBox.Show("Are you sure to cancel this Purchase", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
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
            catch
            {
                this.Close();
            }


        }
        private void btnSupplier_Click(object sender, EventArgs e)
        {
            
            CommonHelp c = new CommonHelp(0, genEnum.Supplier);
            if (c.ShowDialog() == DialogResult.OK && c.c != null)
            {
                GetLedgers();
                SUPPLIER_CODE.Text = Convert.ToString(c.c[0].Value);
                txtSupplierName.Text = Convert.ToString(c.c[1].Value);

                String Query = "select suppliers.desc_eng  NAME,last_pay.Date [LAST PAY DATE],last_pay.DEBIT [LAST PAY AMOUNT] ,suppliers.Dedit DEBIT,suppliers.Credit [CREDIT(With Opening Balance)],suppliers.Balance BALANCE from (SELECT  s.LedgerId ,s.DESC_ENG,sum( t.CREDIT) Credit,sum(t.DEBIT) Dedit,sum(t.CREDIT)-SUM(t.DEBIT) Balance from pay_supplier s join tb_Transactions t on t.ACCID=s.LedgerId group by s.LedgerId, s.DESC_ENG  ) Suppliers left join (select tr.accid,tr.TRANSACTIONID  id,tr.DATED [date],tr.debit,ROW_NUMBER() OVER(Partition by tr.accid ORDER BY tr.accid,tr.DATED desc,tr.TRANSACTIONID desc)  [Row_Number] from tb_Transactions tr join pay_supplier sp on tr.ACCID=sp.LedgerId and tr.VOUCHERTYPE not in('Purchase','Opening Balance') ) last_pay on Suppliers.LedgerId=last_pay.accid where last_pay.Row_Number=1 AND suppliers.desc_eng LIKE '" + txtSupplierName.Text + "'";
                DataTable dt = DbFunctions.GetDataTable(Query);
                if (dt.Rows.Count > 0)
                {
                    if (Convert.ToDecimal(dt.Rows[0]["BALANCE"].ToString()) > 0)
                    {
                        txt_cusbalance.Text = dt.Rows[0]["BALANCE"].ToString();
                        txt_cusbalance.ForeColor = System.Drawing.Color.Red;
                        txt_drcr.Text = "Dr";
                        txt_drcr.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        txt_cusbalance.Text = dt.Rows[0]["BALANCE"].ToString();
                        txt_cusbalance.ForeColor = System.Drawing.Color.Green;
                        txt_drcr.Text = "Cr";
                        txt_drcr.ForeColor = System.Drawing.Color.Green;
                    }
                }


                if (type == "LGR.PRT")
                {
                    drpCreditor.SelectedValue = Convert.ToString(c.c["LedgerId"].Value);

                }
                else
                {
                    if (chkCredit.Checked)
                        drpdebitor.SelectedValue = Convert.ToString(c.c["LedgerId"].Value);
                }
                SUPPLIER_INV.Focus();
                chkCredit.Checked = true;
                //type = "PUR.CRD";
            }
            else
            {
                SUPPLIER_CODE.Text = "";
            }

            if (DOC_NO.Text == "")
            {
                GetMaxDocID();
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
        private void btnDoc_Click(object sender, EventArgs e)
        {
            PurchaseMasterHelp h = new PurchaseMasterHelp(0, type);
            if (h.ShowDialog() == DialogResult.OK)
            {
                btnClear.PerformClick();
                ID = Convert.ToString(h.c["DOC_NO"].Value);
                DOC_NO.Text = ID;
                DOC_DATE_HIJ.Text = Convert.ToString(h.c["DOC_DATE_HIJ"].Value);
                SUPPLIER_CODE.Text = Convert.ToString(h.c["SUPPLIER_CODE"].Value);
                NOTES.Text = Convert.ToString(h.c["NOTES"].Value);
                TOTAL_AMOUNT.Text = Convert.ToString(h.c["GROSS"].Value);
                DISCOUNT.Text = Convert.ToString(h.c["DISCOUNT_VAL"].Value);
                NETT_AMOUNT.Text = Convert.ToString(h.c["NET_VAL"].Value);
                TOTAL_TAX_AMOUNT.Text = Convert.ToString(h.c["TAX_TOTAL"].Value);
                CESS.Text = Convert.ToString(h.c["CESS_AMOUNT"].Value);
                PAY_CODE.Text = Convert.ToString(h.c["PAY_CODE"].Value);
                CARD_NO.Text = Convert.ToString(h.c["CARD_NO"].Value);
                SUPPLIER_INV.Text = Convert.ToString(h.c["SUP_INV_NO"].Value);
                Txt_freight.Text = Convert.ToString(h.c["FREIGHT_AMT"].Value);
               // conn.Open();
                invDtl.DocNo = ID;
                //cmd.CommandText = "SELECT * FROM INV_PURCHASE_DTL WHERE DOC_NO = '" + ID + "'";
                //cmd.CommandType = CommandType.Text;
                SqlDataReader r = invDtl.selectPurchaseDlt();
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
                }
                DbFunctions.CloseConnection();
              //  conn.Close();
            }
        }
        public void GetLedgerId(string CusCode)
        {
            if (CusCode == "")
            {
            //    CASHACC.SelectedValue = 21;
            }
            else
            {
                DataTable dt = new DataTable();
                Ledg.CUSCODE = CusCode;
              
                dt = Ledg.GetLedgerIdPurchase();
                if (dt.Rows.Count > 0)
                {
                  
                  //  CASHACC.SelectedValue = dt.Rows[0][0].ToString();
                    if (type == "LGR.PRT")
                        drpCreditor.SelectedValue = dt.Rows[0][0].ToString();
                    else
                        drpCreditor.Text = "PURCHASE ACCOUNT";
                    if (type == "LGR.PRT")
                        drpdebitor.Text = "PURCHASE RETURN ACCOUNT";
                    else
                        drpdebitor.SelectedValue = dt.Rows[0][0].ToString();
                    
                }
                else
                {
                  //  CASHACC.SelectedValue = 21;
                    if (type == "LGR.PRT")
                        drpCreditor.Text = "CASH ACCOUNT";
                    else
                        drpCreditor.Text = "PURCHASE ACCOUNT";
                    if (type == "LGR.PRT")
                        drpdebitor.Text = "PURCHASE RETURN ACCOUNT";
                    else
                        drpdebitor.Text = "SALES ACCOUNT";
                }
            }
        }
        private void linkRemoveRecord_LinkClicked(object sender, EventArgs e)
        {
            if (dgItems.Rows.Count > 0 && dgItems.CurrentRow != null)
            {
                dgItems.Rows.Remove(dgItems.CurrentRow);
                totalItemAmount();
            }
        }
        private void DOC_NO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                btnDoc.PerformClick();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Enter)
            { 
            
            }
        }
        private void PAY_CODE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                btnPayType.PerformClick();
            }
        }

        private void firstFocus()
        {
            if (PUR_FocusItemCode)
            {
                ActiveControl = ITEM_CODE;
            }
            else if (PUR_FocusBarcode)
            {
                ActiveControl = BARCODE;
            }
            else
            {
                ActiveControl = ITEM_NAME;
            }
        }

        private void SUPPLIER_CODE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Common.preventDingSound(e);
                KryptonTextBox txtBox = (sender as KryptonTextBox);
                switch (txtBox.Name)
                {
                    case "SUPPLIER_CODE":
                        ActiveControl = txtSupplierName;
                        break;
                    case "txtSupplierName":
                        ActiveControl = SUPPLIER_INV;
                        break;
                    case "SUPPLIER_INV":
                        if (!SUPPLIER_INV.Text.Equals("") || MessageBox.Show("NOTE: Invoice Number is Empty!", "Invoice Number Empty", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            firstFocus();
                        }
                        break;
                    default:
                        break;
                }
            }
            else if (e.KeyCode == Keys.Back)
            {
                KryptonTextBox txtBox = (sender as KryptonTextBox);
                switch (txtBox.Name)
                {
                    case "SUPPLIER_CODE":
                        txtSupplierName.Text = "";
                        break;
                    case "txtSupplierName":
                      
                        break;
                    case "SUPPLIER_INV":
                      
                        break;
                    default:
                        break;
                }
              
            }


            else
            {
                if (e.KeyCode != Keys.Alt)
                {
                    KryptonTextBox txtBox = (sender as KryptonTextBox);
                    switch (txtBox.Name)
                    {
                        case "SUPPLIER_CODE":

                            btnSupplier.PerformClick();

                            break;

                        default:
                            break;
                    }
                }
            }
            
            
            

           
        }
        private void btnFilter_Click(object sender, EventArgs e)
        {
            source.Filter = "DOC_DATE_GRE > '" + dpFrom.Value + "' AND DOC_DATE_GRE < '" + dpTo.Value + "'";
        }
        private void btnFilterClear_Click(object sender, EventArgs e)
        {
            source.Filter = "";
        }
        private void BARCODE_KeyDown(object sender, KeyEventArgs e)
        {
            if (BARCODE.Text != null || BARCODE.Text !="")
            {
                currencyflag = true;
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
                            QTY_RCVD.Focus();
                            QTY_RCVD.Text = "1";
                        }
                        addUnits();
                        UOM.Text = t.Rows[0][2].ToString();
                    }
                }
            }
            else
            {
                ITEM_NAME.Focus();
            }
        }
        private void assignBatch()
        {
            BatchHelp h = new BatchHelp(ITEM_CODE.Text);
            if (h.ShowDialog() == DialogResult.OK && h.c != null)
            {
                BATCH.Text = Convert.ToString(h.c["BATCH"].Value);
                EXPIRY_DATE.Value = DateTime.ParseExact(Convert.ToString(h.c["EXPIRY_DATE"].Value), "dd/MM/yyyy", null);
            }
        }
        private void UOM_SelectedIndexChanged(object sender, EventArgs e)
        {
            //PRICE_FOB.Text = Convert.ToString( UOM.SelectedValue) ;
            //DataTable DT = new DataTable();
            //DT.Rows.Clear();
            //cmd.CommandText = "SELECT PRICE FROM INV_ITEM_PRICE_DF WHERE ITEM_CODE = '" + ITEM_CODE.Text + "' AND UNIT_CODE='" + Convert.ToString(UOM.SelectedValue) + "'";
            //adapter.Fill(DT);
            //if (DT.Rows.Count > 0)
            //    PRICE_FOB.Text = DT.Rows[0][0].ToString();
            getRate();
            if (!hasPurExclusive)
            {
                double pPrice = 0;
                pricefob = Convert.ToDouble(PRICE_FOB.Text.Equals(null) ? 0 :Convert.ToDouble(PRICE_FOB.Text));
                double taxcalc = (Convert.ToDouble(ITEM_TAX_PER.Text) / 100) + 1;
                pPrice = Convert.ToDouble(PRICE_FOB.Text.Equals(null) ? 0 : Convert.ToDouble(PRICE_FOB.Text));
                PRICE_FOB.Text = (pPrice / taxcalc).ToString();
                // double ROUND = (pPrice / taxcalc);
                // double rfig = Math.Round(ROUND, 2);
                //  PRICE_FOB.Text = rfig.ToString() ;
                // PRICE_FOB.Text = dataGridItem.CurrentRow.Cells["PUR"].Value.ToString();
            }
        }
        private void getRate()
        {
            invDtl.ItemCode = ITEM_CODE.Text;
            invDtl.Uom = UOM.Text;
            DataTable dt = invDtl.getAllRates();
        //    SqlDataAdapter da = new SqlDataAdapter();
        //    conn.Open();
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.CommandText = "GET_ALL_RATES";
        //    cmd.Parameters.Clear();
        //    cmd.Parameters.AddWithValue("@ITEM_CODE", ITEM_CODE.Text);
        //    cmd.Parameters.AddWithValue("@UNIT_CODE", UOM.Text);
        ////    cmd.Parameters.AddWithValue("RATE_CODE", "PUR");
        //    da.SelectCommand = cmd;
        //   // string price = Convert.ToString(cmd.ExecuteScalar());
        //    da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count;i++)
                {
                    for (int j = 0; j < dgSubPrices.RowCount; j++)
                    {
                        if (dt.Rows[i][0].ToString() == dgSubPrices.Rows[j].Cells[0].Value.ToString())
                        {
                            dgSubPrices.Rows[j].Cells[2].Value = dt.Rows[i]["PRICE"].ToString();
                            if (dt.Rows[i]["SAL_TYPE"].ToString()=="PUR")
                            {
                                 PRICE_FOB.Text = dt.Rows[i]["PRICE"].ToString();
                            }
                            if (dt.Rows[i]["SAL_TYPE"].ToString() == DefaultSaleType)
                            {
                                RTL_PRICE.Text = dt.Rows[i]["PRICE"].ToString();
                            }
                            if (dt.Rows[i]["SAL_TYPE"].ToString() == "MRP")
                            {
                                txtMRP.Text = dt.Rows[i]["PRICE"].ToString();
                            }
                            if (dt.Rows[i]["SAL_TYPE"].ToString() == "WHL")
                            {
                                tb_Wholesale.Text = dt.Rows[i]["PRICE"].ToString();
                            }
                        }
                    }
                }
            }
          //  PRICE_FOB.Text = price;
           // conn.Close();
        //    cmd.CommandType = CommandType.Text;
        }
        public void PostingProduct()
        {
            if (dgTrans.Rows.Count > 0)
            {
                string docIDs = "";
                for (int i = 0; i < dgTrans.Rows.Count; i++)
                {
                    if (Convert.ToString(dgTrans.Rows[i].Cells[13].Value) == "N")
                    {
                        docIDs += "'" + Convert.ToString(dgTrans.Rows[i].Cells[0].Value) + "',";
                    }
                }
                docIDs = docIDs.Substring(0, docIDs.Length - 1);
                //SqlCommand cmd2 = new SqlCommand();
                //cmd2.Connection = conn2;
                //conn2.Open();
                invHdr.DocNo = docIDs;
                //cmd2.CommandText = "UPDATE INV_PURCHASE_HDR SET POSTED = 'Y' WHERE DOC_NO IN (" + docIDs + ")";
                //cmd2.CommandType = CommandType.Text;
                //cmd2.ExecuteNonQuery();
               // conn2.Close();
                invHdr.postingProduct();
                if (type != "LGR.PRT")
                {
                    StockIn();
                }
            }
            BindDetails();
        }
        private void btnPost_Click(object sender, EventArgs e)
        {
            PostingProduct();
        }
        public void StockIn()
        {
            for (int i = 0; i < dgTrans.Rows.Count; i++)
            {
                if (Convert.ToString(dgTrans.Rows[i].Cells[13].Value) == "N")
                {
                    string StockDOC_NO = General.generateStockID();
                    string Query = "";
                    string stocktype = type;
                    string str = Convert.ToString(Convert.ToDateTime(dgTrans.Rows[i].Cells[1].Value).ToString("dd-MMM-yyyy"));
                    Query = "INSERT INTO INV_STK_TRX_HDR(DOC_NO,DOC_TYPE,DOC_DATE_GRE,DOC_DATE_HIJ,DOC_REFERENCE,BRANCH,NOTES,TAX_AMOUNT,TOTAL_AMOUNT,AddedBy) VALUES('" + StockDOC_NO + "','" + stocktype + "','" + Convert.ToString(Convert.ToDateTime(dgTrans.Rows[i].Cells[1].Value).ToString("dd-MMM-yyyy")) + "','" + Convert.ToString(dgTrans.Rows[i].Cells[2].Value) + "','" + Convert.ToString(dgTrans.Rows[i].Cells[0].Value) + "','" + lg.Branch + "','" + Convert.ToString(dgTrans.Rows[i].Cells[8].Value) + "','" + Convert.ToString(dgTrans.Rows[i].Cells[4].Value) + "','" + Convert.ToString(dgTrans.Rows[i].Cells[10].Value) + "','" + SalesManCode + "');";
                   
                   /// conn.Open();
                   // cmd.ExecuteNonQuery();
                   // conn.Close();
                    DbFunctions.InsertUpdate(Query);

                    invDtl.DocNo = Convert.ToString(dgTrans.Rows[i].Cells[0].Value); 
                    DataTable dt = invDtl.getPurchaseDlt();
                    //SqlDataAdapter Adap = new SqlDataAdapter();
                    //string a = Convert.ToString(dgTrans.Rows[i].Cells[0].Value);
                    //cmd.CommandText = "SELECT * FROM INV_PURCHASE_DTL WHERE DOC_NO = '" + a + "'";
                    //Adap.SelectCommand = cmd;
                    //adapter.Fill(dt);

                  //  conn.Open();
                    Dictionary<string, object> parameters = new Dictionary<string, object>();
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        double taxper = 0;
                        double taxamt = 0;
                        try
                        {
                            taxamt = Convert.ToDouble(dt.Rows[j][22].ToString());
                        }
                        catch {}

                        try
                        {
                            taxper = Convert.ToDouble(dt.Rows[j][21].ToString());
                        }
                        catch {}
                        string mdate = dt.Rows[j][25].ToString();
                        string manbatch = "";
                        try
                        {
                            manbatch = dt.Rows[j][24].ToString();
                        }
                        catch
                        {
                        }
                    //    string gdate = (!mdate.Equals("")) ? DateTime.ParseExact(mdate, "dd/MM/yyyy hh:mm:ss tt", null).ToString("yyyy/MM/dd") : "";
                    //    cmd.CommandText = "INSERT INTO INV_STK_TRX_DTL(DOC_TYPE,DOC_NO,ITEM_CODE,ITEM_DESC_ENG,UOM,PRICE,QUANTITY,BATCH,EXPIRY_DATE,TAX_PER,TAX_AMOUNT,DOC_REFERENCE,BRANCH,PRICE_BATCH)values('" + dt.Rows[j][3].ToString() + "','" + StockDOC_NO + "','" + dt.Rows[j][5].ToString() + "','" + dt.Rows[j][6].ToString() + "','" + dt.Rows[j][8].ToString() + "','" + dt.Rows[j][12].ToString() + "','" + dt.Rows[j][11].ToString() + "','" + dt.Rows[j][24].ToString() + "','" + gdate + "','" + taxper + "','" + taxamt + "','" + dt.Rows[j][2].ToString() + "','" + lg.Branch + "','" + dt.Rows[j]["PRICE_BATCH"].ToString() + "')";
                       // cmd.Parameters.Add("@DATE", SqlDbType.Date).Value = (!mdate.Equals("")) ? mdate:"";
                        //cmd.Parameters.AddWithValue("@DATE", mdate);
                        parameters.Clear();
                        
                        if (manbatch != "")
                        {
                           // cmd.Parameters.Add("@DATE1", SqlDbType.Date).Value = Convert.ToDateTime(mdate);
                            parameters.Add("@DATE1", Convert.ToDateTime(mdate));
                        }
                        else
                        {
                         //   cmd.Parameters.Add("@DATE1", SqlDbType.Date).Value = DBNull.Value;
                            parameters.Add("@DATE1", DBNull.Value);
                        }

                       Query = "INSERT INTO INV_STK_TRX_DTL(DOC_TYPE,DOC_NO,ITEM_CODE,ITEM_DESC_ENG,UOM,PRICE,QUANTITY,BATCH,EXPIRY_DATE,TAX_PER,TAX_AMOUNT,DOC_REFERENCE,BRANCH,PRICE_BATCH)values('" + dt.Rows[j][3].ToString() + "','" + StockDOC_NO + "','" + dt.Rows[j][5].ToString() + "','" + dt.Rows[j][6].ToString() + "','" + dt.Rows[j][8].ToString() + "','" + dt.Rows[j][12].ToString() + "','" + dt.Rows[j][11].ToString() + "','" + dt.Rows[j][24].ToString() + "',@DATE1,'" + taxper + "','" + taxamt + "','" + dt.Rows[j][2].ToString() + "','" + lg.Branch + "','" + dt.Rows[j]["PRICE_BATCH"].ToString() + "')";
                        //cmd.ExecuteNonQuery();
                        //cmd.Parameters.Clear();
                       DbFunctions.InsertUpdate(Query, parameters);
                    }
                   // conn.Close();
                }

            }
            //  MessageBox.Show("Posting Successfull");
            btnClear.PerformClick();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ItemMasterView ItemMV = new ItemMasterView("Purchase");
            ItemMV.ShowDialog();
            if (ItemMV.addedfrompuchase == true)
            {
                bindgridview();
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
                    PNL_DATAGRIDITEM.Visible = false;

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
                    UOM.Text = dataGridItem.CurrentRow.Cells[3].Value.ToString();
                    TaxId = Convert.ToInt16(dataGridItem.CurrentRow.Cells[5].Value.ToString());
                    PRICE_FOB.Text = dataGridItem.CurrentRow.Cells[4].Value.ToString();
                  //  getRate();
                    GetTaxRate();
                    
                    // addItem();
                    // clearItem();
                    //   ITEM_NAME.Focus();




                    ITEM_NAME.Text = dataGridItem.CurrentRow.Cells[2].Value.ToString();
                    BARCODE.Text = dataGridItem.CurrentRow.Cells["Barcode"].Value.ToString();
                    PNL_DATAGRIDITEM.Visible = false;

                 
                  

                }
            }
            catch(Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }
        private void ITEM_NAME_TextChanged(object sender, EventArgs e)
        {
            if (ITEM_NAME.Text == "")
            {
                PNL_DATAGRIDITEM.Visible = false;
            }
            else
            {
                //  bindgridview();
                PNL_DATAGRIDITEM.Visible = true;
                try
                {
                  //  source2.Filter = string.Format("[ITEM NAME] LIKE '%{0}%' ", ITEM_NAME.Text);
                    source2.Filter = string.Format("[ITEM NAME] LIKE '%{0}%' ", ITEM_NAME.Text.Replace("'", "''").Replace("*", "[*]"));
                    //dataGridItem.ClearSelection();
                    dataGridItem.Columns["ITEM NAME"].Width = 250;
                }
                catch
                {
                }
            }

            if (clearitemname)
            {
                ITEM_NAME.Text = "";
                clearitemname = false;
            }
        }

        private void showPanelSuggestion()
        {
            if (ITEM_NAME.Text == "" && PNL_DATAGRIDITEM.Visible == false)
            {
                source2.Filter = "";
                PNL_DATAGRIDITEM.Location = new Point(flowLayoutPanel1.Location.X + +ITEM_CODE.Location.X, flowLayoutPanel1.Location.Y + ITEM_CODE.Location.Y + ITEM_CODE.Height + 10);
                PNL_DATAGRIDITEM.Visible = true;
                PNL_DATAGRIDITEM.BringToFront();
                //dataGridItem.Focus();
                if (dataGridItem.Rows.Count > 0)
                {
                    dataGridItem.CurrentCell = dataGridItem.Rows[0].Cells[2];
                    dataGridItem.Columns["ITEM NAME"].Width = 250;
                    dataGridItem.Columns["TYPE"].Visible = false;
                    dataGridItem.Columns["CATEGORY"].Visible = false;
                    dataGridItem.Columns["GROUP"].Visible = false;
                    dataGridItem.Columns["TRADEMARK"].Visible = false;
                    dataGridItem.CurrentCell = dataGridItem[2, 0];
                }
            }
            else if (PNL_DATAGRIDITEM.Visible == true)
            {
                if (dataGridItem.RowCount > 0)
                {
                    int r = dataGridItem.CurrentCell.RowIndex;
                    if (r < dataGridItem.Rows.Count-1) //check for index out of range
                        dataGridItem.CurrentCell = dataGridItem[2, r + 1];
                }
            }
        }

        private void ITEM_NAME_KeyDown(object sender, KeyEventArgs e)
        {
            currencyflag = true;
            if (e.KeyData == Keys.Down)
            {
                showPanelSuggestion();
            }
            else if (e.KeyCode == Keys.Up)
            {
                if (dataGridItem.RowCount > 0)
                {
                    int r = dataGridItem.CurrentCell.RowIndex;
                    if (r > 0) //check for index out of range
                        dataGridItem.CurrentCell = dataGridItem[2, r - 1];
                }
            }
            else if (e.KeyCode == Keys.Enter)
            {
                Common.preventDingSound(e);
                if (PNL_DATAGRIDITEM.Visible)
                {
                    if (dataGridItem.CurrentRow != null)
                    {
                        addItemToDataGridView(e);
                    }
                    if (hasBatch)
                        BATCH.Focus();
                    else
                    QTY_RCVD.Focus();
                }
                else
                {
                    showPanelSuggestion();
                }
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
        private void btnAddSupplier_Click(object sender, EventArgs e)
        {
            SupplierMaster sm = new SupplierMaster();
            sm.ShowDialog();
           
        }
        int cntr = 0;
        private void PRICE_FOB_KeyPress(object sender, KeyPressEventArgs e)
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
                    PRICE_FOB.Text = (temp / taxcalc).ToString();
                 ///   ITEM_TAX_TextChanged(sender, e);
                    //ITEM_TAX.Text = (Convert.ToDouble(tb_netvalue.Text) * (Convert.ToDouble(ITEM_TAX_PER.Text) / 100)).ToString(decimalFormat);
                    excludechanged = true;
                    includechang = false;
                    lblPrice.Text = "Gr Price:";
                    cntr++;
                }
                else if (!includechang)
                {
                    double localPrice = 0;
                    try
                    {
                        localPrice = Convert.ToDouble(PRICE_FOB.Text);
                    }
                    catch (Exception) { }

                    PRICE_FOB.Text = (localPrice * taxcalc).ToString();
                    includechang = true;
                    excludechanged = false;
                    lblPrice.Text = "Price:";

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
        private void ITEM_TAX_TextChanged(object sender, EventArgs e)
        {
            
            double total = 0;
            if (QTY_RCVD .Text.Trim() != "" && PRICE_FOB.Text.Trim() != "")
            {
                total = Convert.ToDouble(QTY_RCVD.Text) * Convert.ToDouble(PRICE_FOB.Text);
            }
            string txt = ITEM_TAX.Text;
            if (txt.Trim() != "")
            {
                total = total + Convert.ToDouble(txt)-Item_Discount_Amt;
            }
            ITEM_GROSS.Text = total.ToString(decimalFormat);
        }
        private void dgItems_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                firstitemlistbyname = true;
                if (dgItems.Rows.Count > 0 && dgItems.CurrentRow != null)
                {
                    Editactive = true;
                    selectedRow = dgItems.CurrentRow.Index;
                    DataGridViewCellCollection c = dgItems.CurrentRow.Cells;
                    ITEM_CODE.Text = Convert.ToString(c["uCode"].Value);
                    ITEM_NAME.Text = Convert.ToString(c["uName"].Value);
                    if (hasBatch)
                    {
                        BATCH.Text = Convert.ToString(c["uBatch"].Value);
                        EXPIRY_DATE.Value = DateTime.ParseExact(Convert.ToString(c["uExpDate"].Value), "dd/MM/yyyy", null);
                    }

                    QTY_RCVD.Text = Convert.ToString(c["uQty"].Value);
                    PRICE_FOB.Text = Convert.ToString(c["uPrice"].Value);
                    pricefob = Convert.ToDouble(PRICE_FOB.Text);
                    if (hasTax)
                    {
                        ITEM_TAX_PER.Text = Convert.ToString(c["uTaxPercent"].Value);
                        ITEM_TAX.Text = Convert.ToString(c["uTaxAmt"].Value);
                    }
                    ITEM_GROSS.Text = Convert.ToDouble(c["uTotal"].Value).ToString(decimalFormat);

                    addUnits();
                    UOM.Text = Convert.ToString(c["uUnit"].Value);
                    SERIALNO.Text = Convert.ToString(c["SerialNos"].Value);

                    if (SERIALNO.Text != "")
                    {
                        PNLSERIAL.Visible = true;
                    }
                    else
                    {
                        PNLSERIAL.Visible = false;
                    }
                    RTL_PRICE.Text = Convert.ToString(c["uRTL_PRICE"].Value);
                    Common.preventDingSound(e);
                }
                PNL_DATAGRIDITEM.Visible = false;
                QTY_RCVD.Focus();
                //Common.preventDingSound(e);
            }
            else if (e.KeyData == Keys.Delete)
            {
                if (dgItems.Rows.Count > 0 && dgItems.CurrentRow != null)
                {
                    dgItems.Rows.Remove(dgItems.CurrentRow);
                    totalItemAmount();
                }
            }
        }
        void PurTypeLed()
        {
            cmbInvType.SelectedIndexChanged -= cmbInvType_SelectedIndexChanged;
            //conn2.Open();
            //cmd.Connection = conn2;
            //cmd.CommandText = "SELECT CODE,DESC_ENG FROM GEN_PUR_TYPE";
            //cmd.CommandType = CommandType.Text;
            //cmd.Connection = conn;
            DataTable DT = stkHdr.genPriceType();
            //SqlDataAdapter ad = new SqlDataAdapter(cmd);
            //ad.Fill(DT);
            //conn2.Close();
            DataRow row = DT.NewRow();
            row[1] = "SELECT TYPE";
            DT.Rows.InsertAt(row, 0);
            cmbInvType.DataSource = DT;
            cmbInvType.ValueMember = "CODE";
            cmbInvType.DisplayMember = "DESC_ENG";
            cmbInvType.SelectedIndexChanged += cmbInvType_SelectedIndexChanged;
            SetLabelPrefix();
        }
        string getSupplier(string CODE)
        {
            string Value = "";
            //conn.Open();
            invHdr.SupplierCode = CODE;
           // cmd = new SqlCommand("SELECT DESC_ENG FROM PAY_SUPPLIER WHERE CODE='"+CODE+"'",conn);
            Value = invHdr.getSupplier() ;
           // conn.Close();
            return Value;
        }
        public void getDataFromDocNo()
        {
            try
            {
                ID = Convert.ToString(DOC_NO.Text);
                // DOC_NO.Text = ID;

               // conn.Open();
               // cmd.CommandText = "SELECT * FROM INV_PURCHASE_HDR WHERE DOC_NO = '" + ID + "'";
                invDtl.DocNo = ID;
                //SqlDataReader rd = invDtl.selectPurchaseHdr();
                DataTable rd = invDtl.selectPurchaseHdr();
                for (int i = 0; i < rd.Rows.Count;i++)
                {
                    DOC_DATE_GRE.Text = Convert.ToString(rd.Rows[i]["DOC_DATE_GRE"]);
                    DOC_DATE_HIJ.Text = Convert.ToString(rd.Rows[i]["DOC_DATE_HIJ"]);
                    type = Convert.ToString(rd.Rows[i]["DOC_TYPE"]);
                    SUPPLIER_CODE.Text = Convert.ToString(rd.Rows[i]["SUPPLIER_CODE"]);

                    GetLedgerId(Convert.ToString(rd.Rows[i]["SUPPLIER_CODE"]));
                    NOTES.Text = Convert.ToString(rd.Rows[i]["NOTES"]);
                    TOTAL_AMOUNT.Text = Convert.ToString(rd.Rows[i]["GROSS"]);
                    DISCOUNT.Text = Convert.ToString(rd.Rows[i]["DISCOUNT_VAL"]);
                    NETT_AMOUNT.Text = Convert.ToString(rd.Rows[i]["NET_VAL"]);
                    TOTAL_TAX_AMOUNT.Text = Convert.ToString(rd.Rows[i]["TAX_TOTAL"]);
                    CESS.Text = Convert.ToString(rd.Rows[i]["CESS_AMOUNT"]);
                    PAY_CODE.Text = Convert.ToString(rd.Rows[i]["PAY_CODE"]);
                    CARD_NO.Text = Convert.ToString(rd.Rows[i]["CARD_NO"]);
                    SUPPLIER_INV.Text = Convert.ToString(rd.Rows[i]["SUP_INV_NO"]);
                    VOUCHNUM.Text = Convert.ToString(rd.Rows[i]["DOC_ID"]);

                    cmbInvType.SelectedIndexChanged -= cmbInvType_SelectedIndexChanged;
                    cmbInvType.SelectedValue = Convert.ToString(rd.Rows[i]["PUR_TYPE"].ToString());
                    SetLabelPrefix();
                    cmbInvType.SelectedIndexChanged += cmbInvType_SelectedIndexChanged;
                }
                //DbFunctions.CloseConnection();
               // conn.Close();
                txtSupplierName.Text = getSupplier(SUPPLIER_CODE.Text);
                //conn.Open();
                //cmd.CommandText = "SELECT * FROM INV_PURCHASE_DTL WHERE DOC_NO = '" + ID + "'AND FLAGDEL='TRUE'";
                //cmd.CommandType = CommandType.Text;
                SqlDataReader r = invDtl.selectPurchaseDtlWthDelFlag();
                while (r.Read())
                {
                    int i = dgItems.Rows.Add(new DataGridViewRow());
                    DataGridViewCellCollection c = dgItems.Rows[i].Cells;

                    c["uCode"].Value = r["ITEM_CODE"];
                    c["uName"].Value = r["ITEM_DESC_ENG"];
                    c["colBATCH"].Value = r["PRICE_BATCH"];
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
                    c["uomQty"].Value = r["UOM_QTY"];
                    c["cost_price"].Value = r["cost_price"];
                }
                DbFunctions.CloseConnection();
               // conn.Close();
                GETLEDGERDETAILS();
            }
            catch(Exception ex)
            {
                MessageBox.Show("ERROR IN getDataFromDocNo MESSAGE: "+ex.Message);
            }
        
        }
        private void DOC_DATE_GRE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (PUR_FocusSupplier)
                {
                    SUPPLIER_CODE.Focus();
                }
                else if (PUR_FocusInvoice)
                {
                    SUPPLIER_INV.Focus();
                }
                else if (PUR_FocusReference)
                {
                    DOC_REFERENCE.Focus();
                }
                else
                {
                    if (PUR_FocusBarcode)
                    {
                        ActiveControl = BARCODE;
                    }
                    else if (PUR_FocusItemCode)
                    {
                        ActiveControl = ITEM_CODE;
                    }
                    else
                    {
                        ActiveControl = ITEM_NAME;
                    }
                }
                Common.preventDingSound(e);


            }
        }
        private void Txt_freight_TextChanged(object sender, EventArgs e)
        {
            double loadcharge = 0;
            
            if (txtLoadingCharge.Text == "")
                loadcharge = 0;
            else
                loadcharge = Convert.ToDouble(txtLoadingCharge.Text);

            if (Txt_freight.Text == "")
                Txt_freight.Text = "0";
          
              
                try
                {

                    NETT_AMOUNT.Text = (Convert.ToDouble(TOTAL_AMOUNT.Text) + Convert.ToDouble(Txt_freight.Text)+loadcharge).ToString();
                }
                catch
                {
                    Txt_freight.Text = "0";
                    NETT_AMOUNT.Text = (Convert.ToDouble(TOTAL_AMOUNT.Text) + Convert.ToDouble(Txt_freight.Text)+loadcharge).ToString();
                }

            roundOffNettAmount();
        }
        public void AddingToBacodeGrid()
        { 
            try
            {
                //if (!hasBatch)
                //{
                    FillPrintingGridhasnotbatch();
                //}
                //else
                //{
                //    FillPrintingGridhasbatch();
                //}
            }
            catch
            {
                MessageBox.Show("Error in fetching has batch or not batch");
            }
            finally
            {
                
              //  dgbarcodeprint.Rows.Clear();
            }
        }
        public void FillPrintingGridhasnotbatch()
        {
            foreach (DataGridViewRow item in dgItems.Rows)
            {
                dgbarcodeprint.Rows.Add();
                string batch = "";
                //cmd.Connection = conn;
                //conn.Open();
                //cmd.CommandText = "select max(R_id)as batch FROM RateChange Where Item_code='" + item.Cells["uCode"].Value.ToString() + "'";
                //cmd.CommandType = CommandType.Text;
                //SqlDataReader rd5 = cmd.ExecuteReader();
                //while (rd5.Read())
                //{
                //    batch = Convert.ToString(rd5[0]);
                //}
                //conn.Close();

                dgbarcodeprint.Rows[dgbarcodeprint.Rows.Count - 2].Cells["Item_Code"].Value = item.Cells["uCode"].Value.ToString();
                dgbarcodeprint.Rows[dgbarcodeprint.Rows.Count - 2].Cells["Item_Name"].Value = item.Cells["uName"].Value.ToString();
                dgbarcodeprint.Rows[dgbarcodeprint.Rows.Count - 2].Cells["Item_Arabic_Name"].Value = item.Cells["uName"].Value.ToString();
                dgbarcodeprint.Rows[dgbarcodeprint.Rows.Count - 2].Cells["Batch"].Value = "";
                  if(hasSaleExclusive)
                  {
                    decimal  tax=0;
                      tax=Convert.ToDecimal(item.Cells["uTaxPercent"].Value);
                      dgbarcodeprint.Rows[dgbarcodeprint.Rows.Count - 2].Cells["Rate_"].Value = (((Convert.ToDecimal(item.Cells["uRTL_PRICE"].Value) + Convert.ToDecimal(item.Cells["uRTL_PRICE"].Value) * tax / 100)) / Convert.ToDecimal(item.Cells["uomQty"].Value)).ToString(decimalFormat);
                  }
                    else
                   dgbarcodeprint.Rows[dgbarcodeprint.Rows.Count - 2].Cells["Rate_"].Value =(Convert.ToDouble( item.Cells["uRTL_PRICE"].Value)/Convert.ToDouble(item.Cells["uomQty"].Value)).ToString();
                dgbarcodeprint.Rows[dgbarcodeprint.Rows.Count - 2].Cells["UNIT"].Value = item.Cells["uUnit"].Value.ToString();
               // dgbarcodeprint.Rows[dgbarcodeprint.Rows.Count - 2].Cells["Barcode"].Value = item.Cells["uBarcode"].Value.ToString() + batch;
                dgbarcodeprint.Rows[dgbarcodeprint.Rows.Count - 2].Cells["Barcode"].Value = item.Cells["colBATCH"].Value.ToString();
                dgbarcodeprint.Rows[dgbarcodeprint.Rows.Count - 2].Cells["dgvCopies"].Value = (Convert.ToDouble(item.Cells["uQty"].Value.ToString()) * Convert.ToDouble(item.Cells["uomQty"].Value)).ToString();
                dgbarcodeprint.Rows[dgbarcodeprint.Rows.Count - 2].Cells["ManBarcode"].Value = GetBarcode(item.Cells["uCode"].Value.ToString());
                // GetPrice(PriceType,item.Cells["uCode"].Value.ToString());

                //dgbarcodeprint.Rows[dgbarcodeprint.Rows.Count - 2].Cells["Rate"].Value = GetPrice(PriceType, item.Cells["uCode"].Value.ToString());
            }
        }
        private void FillPrintingGridhasbatch()
        {
            try
            {
              

                    foreach (DataGridViewRow item in dgItems.Rows)
                    {

                        dgbarcodeprint.Rows.Add();
                        try
                        {
                            dgbarcodeprint.Rows[dgbarcodeprint.Rows.Count - 2].Cells["Item_Code"].Value = item.Cells["uCode"].Value.ToString();
                            dgbarcodeprint.Rows[dgbarcodeprint.Rows.Count - 2].Cells["Item_Name"].Value = item.Cells["uName"].Value.ToString();
                            dgbarcodeprint.Rows[dgbarcodeprint.Rows.Count - 2].Cells["Item_Arabic_Name"].Value = item.Cells["uName"].Value.ToString();
                            dgbarcodeprint.Rows[dgbarcodeprint.Rows.Count - 2].Cells["Batch"].Value = "";
                            dgbarcodeprint.Rows[dgbarcodeprint.Rows.Count - 2].Cells["UNIT"].Value = item.Cells["uUnit"].Value.ToString();
                            dgbarcodeprint.Rows[dgbarcodeprint.Rows.Count - 2].Cells["Barcode"].Value = item.Cells["colBATCH"].Value.ToString();
                            dgbarcodeprint.Rows[dgbarcodeprint.Rows.Count - 2].Cells["ManBarcode"].Value = GetBarcode(item.Cells["uCode"].Value.ToString());
                            dgbarcodeprint.Rows[dgbarcodeprint.Rows.Count - 2].Cells["Current_Stock"].Value = "";
                            dgbarcodeprint.Rows[dgbarcodeprint.Rows.Count - 2].Cells["dgvCopies"].Value = (Convert.ToDouble(item.Cells["uQty"].Value.ToString()) * Convert.ToDouble(item.Cells["uomQty"].Value)).ToString();
                            if (hasSaleExclusive)
                            {
                                decimal tax = 0;
                                tax = Convert.ToDecimal(item.Cells["uTaxPercent"].Value);
                                dgbarcodeprint.Rows[dgbarcodeprint.Rows.Count - 2].Cells["Rate_"].Value = (((Convert.ToDecimal(item.Cells["uRTL_PRICE"].Value) + Convert.ToDecimal(item.Cells["uRTL_PRICE"].Value) * tax / 100)) / Convert.ToDecimal(item.Cells["uomQty"].Value)).ToString(decimalFormat);
                            }
                            else
                                dgbarcodeprint.Rows[dgbarcodeprint.Rows.Count - 2].Cells["Rate_"].Value = (Convert.ToDouble(item.Cells["uRTL_PRICE"].Value) / Convert.ToDouble(item.Cells["uomQty"].Value)).ToString();
                            
                            //if (IsMRP == true)
                            //  //  GetPrice(PriceType, item.Cells["uCode"].Value.ToString());
                            //dgbarcodeprint.Rows[dgbarcodeprint.Rows.Count - 2].Cells[PriceType].Value = GetPrice(PriceType, item.Cells["uCode"].Value.ToString());
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
        string GetBarcode(string itemCode)
        {
            string value = "";
          //  conn.Open();
            invDtl.ItemCode = itemCode;
          //  SqlCommand command = new SqlCommand("SELECT BARCODE FROM INV_ITEM_DIRECTORY_UNITS WHERE ITEM_CODE='"+itemCode+"'",conn);
            value = invDtl.getBarcode();
           // conn.Close();
            return value;

        }
        public void AddColumnsTodgbarcodeprint()
        {
            dgbarcodeprint.Columns.Add("Item_Code", "Item Code");
            dgbarcodeprint.Columns.Add("Item_Name", "Item Name");
            dgbarcodeprint.Columns.Add("Item_Arabic_Name", "Arab Name");
            dgbarcodeprint.Columns.Add("Batch", "Batch");
            dgbarcodeprint.Columns.Add("UNIT", "Unit");
            dgbarcodeprint.Columns.Add("Barcode", "Barcode");
            dgbarcodeprint.Columns.Add("ManBarcode", "Manufacture Barcode");
            dgbarcodeprint.Columns.Add("Current_Stock", "Stock");
            dgbarcodeprint.Columns.Add("Rate_","Rate" );//PriceType
            dgbarcodeprint.Columns.Add("dgvCopies", "No of Copies");

        }
        public string GetPrice(string type,string code)
        {  
            string val = "";
            try
            {
                //conn.Open();
                //cmd.CommandText = "SELECT        PRICE, SAL_TYPE, ITEM_CODE FROM  INV_ITEM_PRICE WHERE        (SAL_TYPE ='" + type + "') AND (ITEM_CODE = '" + code + "')";
                invDtl.ItemCode = code;
                invDtl.PriceTypes = type;

                SqlDataReader rdprice = invDtl.getPrice();
                while (rdprice.Read())
                {
                    val = rdprice["PRICE"].ToString();
                }
              //  conn.Close();
                DbFunctions.CloseConnection();
                return val;
            }
            catch
            {
                return val;
            }
            
        }
        private void btnBarcode_Click(object sender, EventArgs e)
        {
            Barcode_Stock_Items br = (Barcode_Stock_Items)Application.OpenForms["Barcode_Stock_Items"];
            try
            {
                // Generate_Barcode();
                if (dgbarcodeprint.Rows.Count > 0)
                {
                    if (decTotal != 0)
                    {
                        if (PrintFormat.Text == "A4")
                        {
                            // if (dgvBarcodePrinting.Rows[inRowIndex].Cells["dgvCopies"].Value != null)
                            //  {
                            ExportToPDF();
                            if (DialogResult.Yes == MessageBox.Show("Clear The Items form Barcode to print", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                            {
                                dgbarcodeprint.Rows.Clear();
                            }
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
            catch
            {
                MessageBox.Show("Erro! in initial export");
            }
        }
        public void barcodeprint()
        {
            Barcode_Stock_Items br = (Barcode_Stock_Items)Application.OpenForms["Barcode_Stock_Items"];
            try
            {
                // Generate_Barcode();
                if (dgbarcodeprint.Rows.Count > 0)
                {
                    if (decTotal != 0)
                    {
                        if (PrintFormat.Text == "A4")
                        {
                            // if (dgvBarcodePrinting.Rows[inRowIndex].Cells["dgvCopies"].Value != null)
                            //  {
                            ExportToPDF();
                            if (DialogResult.Yes == MessageBox.Show("Clear The Items form Barcode to print", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                            {
                                dgbarcodeprint.Rows.Clear();
                            }
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
            catch
            {
                MessageBox.Show("Erro! in initial export");
            }
        }
        public void ExportToPDF()
        {
            Class.BarcodeSettings BarSettings = new Class.BarcodeSettings();
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
                            if (IsProductCode == true)
                            {
                             //   strCode = dgbarcodeprint.Rows[i].Cells["Item_Code"].Value.ToString();

                                if (IsProductCode == true)
                                    strProductCode = strCode;
                            }
                            else
                            {
                                strProductCode = dgbarcodeprint.Rows[i].Cells["Item_Code"].Value.ToString();
                            }

                         string cName = dgbarcodeprint.Rows[i].Cells["Item_Name"].Value.ToString();
                         int length = cName.Length;
                         if (length <= Convert.ToInt32(dt.Rows[0]["ItemLength"]))
                         {
                             strProductName = cName;
                            
                         }
                         else
                         {
                             strProductName = cName.Substring(0, Convert.ToInt32(dt.Rows[0]["ItemLength"]));
                         }

                         if (Convert.ToBoolean(dt.Rows[0]["ISMANUAL"]))
                         {
                             strBarcodeValue = dgbarcodeprint.Rows[i].Cells["ManBarcode"].Value.ToString();
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
                                //  strMRP = PriceType+":"+ dgbarcodeprint.Rows[i].Cells["Rate"].Value.ToString();
                                strMRP = "INR. " + dgbarcodeprint.Rows[i].Cells["Rate_"].Value.ToString();
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
                            iTextSharp.text.Font smallfont = FontFactory.GetFont("Book Antiqua", (float)Convert.ToDouble(dt.Rows[0]["fontSize"]));
                            if (IsCompany == true)
                            {
                                phrase.Add(new Chunk(companyname + Environment.NewLine,smallfont ));
                            }

                            phrase.Add(new Chunk(strProductName + Environment.NewLine,smallfont));
                            phrase.Add(new Chunk(Environment.NewLine));
                            PdfPCell cell = new PdfPCell(phrase);
                            //     cell.FixedHeight = 80.69f;

                            cell.PaddingRight = -10;
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                            if (!Convert.ToBoolean(dt.Rows[0]["IsBoarder"]))
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
                                phrase.Add(new Chunk(Environment.NewLine + strProductCode, smallfont));
                            }
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
                    pdfdoc.SetMargins(0, 0, (float)Convert.ToDouble(dt.Rows[0]["topmrgine"]), 0);
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
                            //lblTotalCountValue.Text = decTotal.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("BCP3:" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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
        bool initialload = true;
      
        
      
        //public void getCur()
        //{
        //    try
        //    {
        //        invHdr.DocNo = VOUCHNUM.Text;
        //        DataTable dt = invHdr.getVoucherCurrency();
        //        //cmd.Connection = conn;
        //        //cmd.CommandText = "SELECT tbl_curRepor.Currency_amt as ExcangeRate ,tbl_curRepor.Currency_code as Currency,  tbl_curRepor.Invoice_no,INV_PURCHASE_HDR.DOC_NO, INV_PURCHASE_HDR.DOC_DATE_GRE as Date,  PAY_SUPPLIER.DESC_ENG as Supplier, (INV_PURCHASE_HDR.TAX_TOTAL)*tbl_curRepor.Currency_amt as 'Tax Total', INV_PURCHASE_HDR.FREIGHT_AMT as Freight, INV_PURCHASE_HDR.GROSS*tbl_curRepor.Currency_amt as 'Gross Amount', INV_PURCHASE_HDR.DISCOUNT_VAL*tbl_curRepor.Currency_amt as Discount,INV_PURCHASE_HDR.NET_VAL*tbl_curRepor.Currency_amt as 'Net Value' FROM            INV_PURCHASE_HDR INNER JOIN tbl_CurRepor ON  INV_PURCHASE_HDR.DOC_ID=tbl_CurRepor.Invoice_no LEFT OUTER JOIN   PAY_SUPPLIER  ON INV_PURCHASE_HDR.SUPPLIER_CODE = PAY_SUPPLIER.CODE WHERE  (INV_PURCHASE_HDR.DOC_ID='" + VOUCHNUM.Text + "')";
        //        //adapter.SelectCommand = cmd;
        //        //adapter.Fill(dt);
        //        curre = dt.Rows[0][0].ToString();
        //        stcurcode = dt.Rows[0][1].ToString();
        //        CURCODE.Text = stcurcode;
            
        //    }
        //    catch(Exception c)
        //    {
        //        string exc = c.Message;
        //        CURCODE.Text = "";
        //    }


        //}
        private void btndown_Click(object sender, EventArgs e)
        {
            
            if (!initialload)
            {
                Clear2();
                int lastVoucherID = Convert.ToInt32(VOUCHNUM.Text);
                VOUCHNUM.Text = (lastVoucherID - 1).ToString();
                checkvoucher(Convert.ToInt32(VOUCHNUM.Text));
                try
                {
                    invHdr.DocNo = VOUCHNUM.Text;
                    if (cmbInvType.SelectedValue != null)
                    {
                        invHdr.PurType = cmbInvType.SelectedValue.ToString();
                    }
                    else
                    {
                        invHdr.PurType = "";
                    }

                    DataTable dt = invHdr.getPurchaseHdrWthDelFlag();
                    //cmd.CommandText = "SELECT * FROM INV_PURCHASE_HDR WHERE DOC_ID = '" + VOUCHNUM.Text + "'AND FLAGDEL='TRUE'";
                    //cmd.CommandType = CommandType.Text;
                    //SqlDataAdapter Adap = new SqlDataAdapter();
                    //Adap.SelectCommand = cmd;
                    //Adap.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        ID = Convert.ToString(dt.Rows[0]["DOC_NO"]);
                        DOC_NO.Text = ID;
                        POSTID = ID;
                        DOC_DATE_GRE.Text = Convert.ToDateTime(dt.Rows[0]["DOC_DATE_GRE"].ToString()).ToShortDateString();
                        DOC_DATE_HIJ.Text = Convert.ToString(dt.Rows[0]["DOC_DATE_HIJ"]);
                        SUPPLIER_CODE.Text = Convert.ToString(dt.Rows[0]["SUPPLIER_CODE"]);
                        NOTES.Text = Convert.ToString(dt.Rows[0]["NOTES"]);
                        TOTAL_AMOUNT.Text = Convert.ToString(dt.Rows[0]["GROSS"]);
                        DISCOUNT.Text = Convert.ToString(dt.Rows[0]["DISCOUNT_VAL"]);
                        NETT_AMOUNT.Text = Convert.ToString(dt.Rows[0]["NET_VAL"]);
                        TOTAL_TAX_AMOUNT.Text = Convert.ToString(dt.Rows[0]["TAX_TOTAL"]);
                        CESS.Text = Convert.ToString(dt.Rows[0]["CESS_AMOUNT"]);
                        PAY_CODE.Text = Convert.ToString(dt.Rows[0]["PAY_CODE"]);
                        CARD_NO.Text = Convert.ToString(dt.Rows[0]["CARD_NO"]);
                        SUPPLIER_INV.Text = Convert.ToString(dt.Rows[0]["SUP_INV_NO"]);
                        Txt_freight.Text = Convert.ToString(dt.Rows[0]["FREIGHT_AMT"]);
                        cmbInvType.SelectedValue = Convert.ToString(dt.Rows[0]["PUR_TYPE"]);
                        if (dt.Rows[0]["OTHER_EXPENSES"].ToString() == "" || dt.Rows[0]["OTHER_EXPENSES"] == null)
                        {
                            txtLoadingCharge.Text = decimalFormat;
                        }
                        else
                        txtLoadingCharge.Text = dt.Rows[0]["OTHER_EXPENSES"].ToString();

                        if (dt.Rows[0]["PROJECTID"] == "" || Convert.ToInt32(dt.Rows[0]["PROJECTID"].ToString()) <= 0 || dt.Rows[0]["PROJECTID"] == null)
                        {
                            cmb_projects.SelectedIndex = 0;
                        }
                        else
                            cmb_projects.SelectedIndex = Convert.ToInt32(dt.Rows[0]["PROJECTID"]);
                        //getCur();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Button Down Click Catch 1 Message: " + ex.Message);
                }
                if (ID != "")
                {
                //conn.Open();
                //cmd.CommandText = "SELECT * FROM INV_PURCHASE_DTL WHERE DOC_NO = '" + ID + "'AND FLAGDEL='TRUE'";
                //cmd.CommandType = CommandType.Text;
                //----FOR PRICE BATCH----//



                //cmd.CommandText = "PREV_PURCHASE_INV";
                //cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.Clear();
            //    cmd.Parameters.AddWithValue("@ID", SqlDbType.VarChar).Value = ID;
                invDtl.DocNo = ID;
                SqlDataReader r = invDtl.getPreviousPurDtl();
              //  cmd.CommandType = CommandType.Text;


                int count = r.FieldCount;
                while (r.Read())
                {
                    int i = dgItems.Rows.Add(new DataGridViewRow());
                    DataGridViewCellCollection c = dgItems.Rows[i].Cells;
                    c["cSlno"].Value = i + 1;
                    c["uCode"].Value = r["ITEM_CODE"];
                    c["uName"].Value = r["ITEM_DESC_ENG"];
                    c["colBATCH"].Value = r["PRICE_BATCH"];
                    if (hasBatch)
                    {
                        c["uBatch"].Value = r["BATCH"];
                        if (r["EXPIRY_DATE"].ToString()!="")
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
                    c["uomQty"].Value = r["UOM_QTY"];
                    c["cost_price"].Value = r["cost_price"];
                    if (r["PRICE_BATCH"].ToString() == "")
                    {
                        c["colBATCH"].Value = null;
                    }
                    else
                    {
                        c["colBATCH"].Value = r["PRICE_BATCH"];
                    }
                   // int count = r.FieldCount;
                    for (int j= 38; j < count; j++)
                    {
                        for (int k = 22; k < dgItems.Columns.Count;k++ )
                        {
                            if (r.GetName(j).ToString() == dgItems.Columns[k].HeaderText)
                            {
                              //  c[dgItems.Columns[k].HeaderText].Value = (Convert.ToDouble(r[j]) * Convert.ToDouble(r["UOM_QTY"])).ToString();
                                c[dgItems.Columns[k].HeaderText].Value = (Convert.ToDouble(r[j].ToString().Equals("") ? "0" : r[j].ToString()) * Convert.ToDouble(r["UOM_QTY"])).ToString();
                            }
                        }
                    }
                }
                //conn.Close();
                DbFunctions.CloseConnection();
                GETLEDGERDETAILS();
                CalculateTotals();
                if (dgItems.Rows.Count > 0)
                {
                    calculateGSTTaxes();
                }
            }
             else
             {
                    MessageBox.Show("No invoice");
             }
            }
        }

        public void checkvoucher(int cvouch)
        {

            //conn.Open();
            //cmd.CommandText = "SELECT ISNULL(MAX(DOC_ID),0) FROM INV_PURCHASE_HDR WHERE PUR_TYPE = '" + cmbInvType.SelectedValue.ToString() + "' AND FLAGDEL='True'";
            if ( cmbInvType.SelectedValue != null)
            {
                invHdr.PurType = cmbInvType.SelectedValue.ToString();
            }
            else
            {
                invHdr.PurType = "";
            }
                String stringMaxVoucherID = invHdr.chckVoucher();
           // conn.Close();

            int vouchmax = -1;
            int startVouch = GetMaxVoucher();
            if (!stringMaxVoucherID.Equals(""))
            {
                vouchmax = Convert.ToInt32(stringMaxVoucherID);
            }
            if (cvouch == startVouch && vouchmax < startVouch)
            {
                btnup.Enabled = false;
                btndown.Enabled = false;
                return;
            }
            else if (cvouch == startVouch && vouchmax >= startVouch)
            {
                btnup.Enabled = true;
                btndown.Enabled = false;
                return;
            }
            if (cvouch > vouchmax)
            {
                btnup.Enabled = false;
                btndown.Enabled = true;
            }
            else if (cvouch <= startVouch)
            {
                if (cvouch < startVouch)
                {
                    VOUCHNUM.Text = Convert.ToString(startVouch);
                }
                btndown.Enabled = false;
                btnup.Enabled = true;
            }
            else if (cvouch <= vouchmax)
            {
                btndown.Enabled = true;
                btnup.Enabled = true;
            }

        }
        private void btnup_Click(object sender, EventArgs e)
        {
            if (!initialload)
            {
                Clear2();
                VOUCHNUM.Text = (Convert.ToInt32(VOUCHNUM.Text) + 1).ToString();
                checkvoucher(Convert.ToInt32(VOUCHNUM.Text));
                //DataTable dt = new DataTable();
                //cmd.CommandText = "SELECT * FROM INV_PURCHASE_HDR WHERE DOC_ID = '" + VOUCHNUM.Text + "' AND FLAGDEL='TRUE'";
                //cmd.CommandType = CommandType.Text;
                //SqlDataAdapter Adap = new SqlDataAdapter();
                //Adap.SelectCommand = cmd;
                //Adap.Fill(dt);
                invHdr.DocNo = VOUCHNUM.Text;
                DataTable dt = invHdr.getPurchaseHdrWthDelFlag();
                if (dt.Rows.Count > 0)
                {
                    ID = Convert.ToString(dt.Rows[0]["DOC_NO"]);
                    DOC_NO.Text = ID;
                    POSTID = ID;
                    DOC_DATE_GRE.Text = Convert.ToDateTime(dt.Rows[0]["DOC_DATE_GRE"].ToString()).ToShortDateString();
                    DOC_DATE_HIJ.Text = Convert.ToString(dt.Rows[0]["DOC_DATE_HIJ"]);
                    SUPPLIER_CODE.Text = Convert.ToString(dt.Rows[0]["SUPPLIER_CODE"]);
                    NOTES.Text = Convert.ToString(dt.Rows[0]["NOTES"]);
                    TOTAL_AMOUNT.Text = Convert.ToString(dt.Rows[0]["GROSS"]);
                    DISCOUNT.Text = Convert.ToString(dt.Rows[0]["DISCOUNT_VAL"]);
                    NETT_AMOUNT.Text = Convert.ToString(dt.Rows[0]["NET_VAL"]);
                    TOTAL_TAX_AMOUNT.Text = Convert.ToString(dt.Rows[0]["TAX_TOTAL"]);
                    CESS.Text = Convert.ToString(dt.Rows[0]["CESS_AMOUNT"]);
                    PAY_CODE.Text = Convert.ToString(dt.Rows[0]["PAY_CODE"]);
                    CARD_NO.Text = Convert.ToString(dt.Rows[0]["CARD_NO"]);
                    SUPPLIER_INV.Text = Convert.ToString(dt.Rows[0]["SUP_INV_NO"]);
                    Txt_freight.Text = Convert.ToString(dt.Rows[0]["FREIGHT_AMT"]);
                    cmbInvType.SelectedValue = Convert.ToString(dt.Rows[0]["PUR_TYPE"]);
                    if (dt.Rows[0]["OTHER_EXPENSES"].ToString() == "" || dt.Rows[0]["OTHER_EXPENSES"] == null)
                    {
                        txtLoadingCharge.Text = decimalFormat;
                    }
                    else
                        txtLoadingCharge.Text = dt.Rows[0]["OTHER_EXPENSES"].ToString();

                    if (Convert.ToInt32(dt.Rows[0]["PROJECTID"].ToString()) <= 0 || dt.Rows[0]["PROJECTID"] == null)
                    {
                        cmb_projects.SelectedIndex = 0;
                    }
                    else
                        cmb_projects.SelectedIndex = Convert.ToInt32(dt.Rows[0]["PROJECTID"]);

                    //getCur();
                    if (ID != "")
                    {
                       // conn.Open();
                        //cmd.CommandText = "SELECT * FROM INV_PURCHASE_DTL WHERE DOC_NO = '" + ID + "'AND FLAGDEL='TRUE'";
                        //cmd.CommandType = CommandType.Text;
                        //----FOR PRICE BATCH----//

                        //cmd.CommandText = "PREV_PURCHASE_INV";
                        //cmd.CommandType = CommandType.StoredProcedure;
                        //// cmd.Parameters.AddWithValue("@ID",ID);
                        //cmd.Parameters.Clear();
                        //cmd.Parameters.AddWithValue("@ID", SqlDbType.VarChar).Value = ID;

                        //SqlDataReader r = cmd.ExecuteReader();
                        //int count = r.FieldCount;
                        //cmd.CommandType = CommandType.Text;
                        invDtl.DocNo = ID;
                        SqlDataReader r = invDtl.getPreviousPurDtl();
                        int count = r.FieldCount;
                        while (r.Read())
                        {
                            int i = dgItems.Rows.Add(new DataGridViewRow());
                            DataGridViewCellCollection c = dgItems.Rows[i].Cells;
                            c["cSlno"].Value = i + 1;
                            c["uCode"].Value = r["ITEM_CODE"];
                            c["uName"].Value = r["ITEM_DESC_ENG"];
                            c["colBATCH"].Value = r["PRICE_BATCH"];
                            if (hasBatch)
                            {
                                c["uBatch"].Value = r["BATCH"];
                                if (r["EXPIRY_DATE"].ToString() != "")
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
                            c["uomQty"].Value = r["UOM_QTY"];
                            c["cost_price"].Value = r["cost_price"];

                            if (r["PRICE_BATCH"].ToString() == "")
                            {
                                c["colBATCH"].Value = null;
                            }
                            else
                            {
                                c["colBATCH"].Value = r["PRICE_BATCH"];
                            }
                            for (int j = 38; j < count; j++)
                            {
                                for (int k = 22; k < dgItems.Columns.Count; k++)
                                {
                                    if (r.GetName(j).ToString() == dgItems.Columns[k].HeaderText)
                                    {
                                       // c[dgItems.Columns[k].HeaderText].Value = (Convert.ToDouble(r[j]) * Convert.ToDouble(r["UOM_QTY"])).ToString();
                                        c[dgItems.Columns[k].HeaderText].Value = (Convert.ToDouble(r[j].ToString().Equals("") ? "0" : r[j].ToString()) * Convert.ToDouble(r["UOM_QTY"])).ToString();
                                    }
                                }
                            }
                        }
                       // conn.Close();
                        DbFunctions.CloseConnection();
                        GETLEDGERDETAILS();
                        CalculateTotals();
                        if (dgItems.Rows.Count > 0)
                        {
                            calculateGSTTaxes();
                        }
                    }
                    else
                    {
                        MessageBox.Show("No invoice");
                    }
                }
                else
                {
                    MessageBox.Show("No invoice");
                }
            }
        }
        private void dataGridItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridItem.CurrentRow != null)
            {
                decimal CN, BX, PC;
                string def = "";
                string itemcode = dataGridItem.CurrentRow.Cells["ITEM_CODE"].Value.ToString();
                itemSelected = true;
                ITEM_CODE.Text = itemcode;
                DataTable dt = new DataTable();
                ITEM_CODE.Text = itemcode;
                HASSERIAL = Convert.ToBoolean(dataGridItem.CurrentRow.Cells["HASSERIAL"].Value);
                PNLSERIAL.Visible = Convert.ToBoolean(dataGridItem.CurrentRow.Cells["HASSERIAL"].Value);
               
                tb_Wholesale.Text = dataGridItem.CurrentRow.Cells["WHL"].Value.ToString();
                TaxId = Convert.ToInt16(dataGridItem.CurrentRow.Cells["TaxId"].Value.ToString());
                //if (!hasPurExclusive)
                //{
                //    double taxcalc = (Convert.ToDouble(ITEM_TAX_PER.Text) / 100) + 1;
                //    pricefob = Convert.ToDouble(PRICE_FOB.Text);
                //    PRICE_FOB.Text = String.Format("{0:0.00}", (Convert.ToDouble(PRICE_FOB.Text) / taxcalc));
                //  // PRICE_FOB.Text = dataGridItem.CurrentRow.Cells["PUR"].Value.ToString();
                //}
                txtMRP.Text = dataGridItem.CurrentRow.Cells["MRP"].Value.ToString();
                DataTable dtPriceList = prclst.getItemPriceList(ITEM_CODE.Text);
                RTL_PRICE.Text = dataGridItem.CurrentRow.Cells["RTL"].Value.ToString();
                //dgvPriceList.DataSource = dtPriceList;
                for (int i = 0; i < dtPriceList.Rows.Count; i++)
                {
                    dgvPriceList.Rows.Add(dtPriceList.Rows[i][0].ToString(), "", dtPriceList.Rows[i][1].ToString(), "Save");
                }
                for (int k = 0; k < dgSubPrices.RowCount; k++)
                {
                    for (int j = 3; j < dataGridItem.Columns.Count; j++)
                    {
                        if (dataGridItem.Columns[j].HeaderText == dgSubPrices.Rows[k].Cells[0].Value.ToString())
                        {
                            if (dataGridItem.CurrentRow.Cells[dataGridItem.Columns[j].HeaderText].Value != null)
                                dgSubPrices.Rows[k].Cells[2].Value = dataGridItem.CurrentRow.Cells[dataGridItem.Columns[j].HeaderText].Value.ToString();
                        }
                    }
                }
                QTY_RCVD.Text = "1";
                GetTaxRate();
                addUnits();
                UOM.Text = dataGridItem.CurrentRow.Cells["UNIT_CODE"].Value.ToString();
                if (!hasPurExclusive)
                {
                    double pPrice = 0;
                    
                    pricefob = Convert.ToDouble(dataGridItem.CurrentRow.Cells["PUR"].Equals(null) ? 0 : dataGridItem.CurrentRow.Cells["PUR"].Value);
                    double taxcalc = (Convert.ToDouble(ITEM_TAX_PER.Text) / 100) + 1;
                    pPrice = Convert.ToDouble(dataGridItem.CurrentRow.Cells["PUR"].Equals(null) ? 0 : dataGridItem.CurrentRow.Cells["PUR"].Value);
                    PRICE_FOB.Text = (pPrice / taxcalc).ToString();
                    // double ROUND = (pPrice / taxcalc);
                    // double rfig = Math.Round(ROUND, 2);
                    //  PRICE_FOB.Text = rfig.ToString() ;
                    // PRICE_FOB.Text = dataGridItem.CurrentRow.Cells["PUR"].Value.ToString();
                }
                else
                {
                    pricefob = Convert.ToDouble(dataGridItem.CurrentRow.Cells["PUR"].Equals(null) ? 0 : dataGridItem.CurrentRow.Cells["PUR"].Value);
                    PRICE_FOB.Text = dataGridItem.CurrentRow.Cells["PUR"].Value.ToString();
                }
                ITEM_NAME.Text = dataGridItem.CurrentRow.Cells["ITEM NAME"].Value.ToString();
                PNL_DATAGRIDITEM.Visible = false;
                itemSelected = false;
                GetRTlRate(itemcode);
                if (hasBatch)
                    BATCH.Focus();
                else
                    QTY_RCVD.Focus();
            }
            else
            {
                MessageBox.Show("No Product to add!");
            }
            
           
        }

        private void dataGridItem_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Common.preventDingSound(e);
                addItemToDataGridView(e);
            }
        }

        private void addItemToDataGridView(KeyEventArgs e)
        {
            if (dataGridItem.CurrentRow != null)
            {
                decimal CN, BX, PC;
                string def = "";
                string itemcode = dataGridItem.CurrentRow.Cells["ITEM_CODE"].Value.ToString();
                itemSelected = true;
                ITEM_CODE.Text = itemcode;
                DataTable dt = new DataTable();
                ITEM_CODE.Text = itemcode;
                HASSERIAL = Convert.ToBoolean(dataGridItem.CurrentRow.Cells["HASSERIAL"].Value);
                PNLSERIAL.Visible = Convert.ToBoolean(dataGridItem.CurrentRow.Cells["HASSERIAL"].Value);


                QTY_RCVD.TextChanged -= new EventHandler(CalTotalAmount);
                QTY_RCVD.Text = "1";
                QTY_RCVD.TextChanged += new EventHandler(CalTotalAmount);
                tb_Wholesale.Text = dataGridItem.CurrentRow.Cells["WHL"].Value.ToString();
                TaxId = Convert.ToInt16(dataGridItem.CurrentRow.Cells["TaxId"].Value.ToString());
                //if (!hasPurExclusive)
                //{
                //    double taxcalc = (Convert.ToDouble(ITEM_TAX_PER.Text) / 100) + 1;
                //    pricefob = Convert.ToDouble(PRICE_FOB.Text);
                //    PRICE_FOB.Text = String.Format("{0:0.00}", (Convert.ToDouble(PRICE_FOB.Text) / taxcalc));
                //  // PRICE_FOB.Text = dataGridItem.CurrentRow.Cells["PUR"].Value.ToString();
                //}
                txtMRP.Text = dataGridItem.CurrentRow.Cells["MRP"].Value.ToString();
                DataTable dtPriceList = prclst.getItemPriceList(ITEM_CODE.Text);
                RTL_PRICE.Text = dataGridItem.CurrentRow.Cells["RTL"].Value.ToString();
                //dgvPriceList.DataSource = dtPriceList;
                for (int i = 0; i < dtPriceList.Rows.Count; i++)
                {                   
                    dgvPriceList.Rows.Add(dtPriceList.Rows[i][0].ToString(), "", dtPriceList.Rows[i][1].ToString(),"Save");
                }
                for (int k = 0; k < dgSubPrices.RowCount; k++)
                {
                    for (int j = 3; j < dataGridItem.Columns.Count; j++)
                    {
                        if (dataGridItem.Columns[j].HeaderText == dgSubPrices.Rows[k].Cells[0].Value.ToString())
                        {
                            if (dataGridItem.CurrentRow.Cells[dataGridItem.Columns[j].HeaderText].Value != null)
                                dgSubPrices.Rows[k].Cells[2].Value = dataGridItem.CurrentRow.Cells[dataGridItem.Columns[j].HeaderText].Value.ToString();
                        }
                    }
                }
               
                GetTaxRate();
                addUnits();
                UOM.Text = dataGridItem.CurrentRow.Cells["UNIT_CODE"].Value.ToString();
                if (!hasPurExclusive)
                {
                    double pPrice = 0;
                    pricefob = Convert.ToDouble(dataGridItem.CurrentRow.Cells["PUR"].Equals(null) ? 0 : dataGridItem.CurrentRow.Cells["PUR"].Value);
                    double taxcalc = (Convert.ToDouble(ITEM_TAX_PER.Text) / 100) + 1;
                    pPrice = Convert.ToDouble(dataGridItem.CurrentRow.Cells["PUR"].Equals(null) ? 0 : dataGridItem.CurrentRow.Cells["PUR"].Value);
                    PRICE_FOB.Text = (pPrice / taxcalc).ToString();
                    // double ROUND = (pPrice / taxcalc);
                    // double rfig = Math.Round(ROUND, 2);
                    //  PRICE_FOB.Text = rfig.ToString() ;
                    // PRICE_FOB.Text = dataGridItem.CurrentRow.Cells["PUR"].Value.ToString();
                }
                else
                {
                    pricefob = Convert.ToDouble(dataGridItem.CurrentRow.Cells["PUR"].Equals(null) ? 0 : dataGridItem.CurrentRow.Cells["PUR"].Value);
                    PRICE_FOB.Text = dataGridItem.CurrentRow.Cells["PUR"].Value.ToString();
                }
               
                    ITEM_NAME.Text = dataGridItem.CurrentRow.Cells["ITEM NAME"].Value.ToString();
                PNL_DATAGRIDITEM.Visible = false;
                itemSelected = false;
                GetRTlRate(itemcode);
                if (hasBatch)
                    BATCH.Focus();
                else
                    QTY_RCVD.Focus();
            }
            else
            {
                MessageBox.Show("No Product to add!");
            }
            
        }

        private void CMBTYPE_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                KryptonComboBox cmb = new KryptonComboBox();
                cmb = (sender as KryptonComboBox);

                if (cmb.Text == "")
                {
                    //PNL_DATAGRIDITEM.Visible = false;
                    //firstitemlistbyname = true;
                    source2.Filter = "";
                }
                else
                {
                    PNL_DATAGRIDITEM.Visible = true;
                    if (cmb.Name == "CMBTYPE")
                    {
                        source2.Filter = string.Format("TYPE LIKE '%{0}%' ", cmb.Text);
                    }
                    else if (cmb.Name == "DrpCategory")
                    {
                        source2.Filter = string.Format("CATEGORY LIKE '%{0}%' ", cmb.Text);
                    }
                    else if (cmb.Name == "Group")
                    {
                        source2.Filter = string.Format("GROUP LIKE '%{0}%' ", cmb.Text);
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
            catch
            {
            }

            GetMaxDocID();
        }
        private void CMBTYPE_TextChanged(object sender, EventArgs e)
        {
            try
            {
                KryptonComboBox cmb = new KryptonComboBox();
                cmb = (sender as KryptonComboBox);
                if (cmb.Text == "")
                {
                    //  PNL_DATAGRIDITEM.Visible = false;
                    //  firstitemlistbyname = true;
                    source.Filter = "";
                }
                else
                {

                    PNL_DATAGRIDITEM.Visible = true;
                    if (cmb.Name == "CMBTYPE")
                    {
                        source2.Filter = string.Format("TYPE LIKE '%{0}%' ", cmb.Text);
                    }
                    else if (cmb.Name == "DrpCategory")
                    {
                        source2.Filter = string.Format(" CATEGORY LIKE '%{0}%' ", cmb.Text);
                    }
                    else if (cmb.Name == "Group")
                    {
                        source.Filter = string.Format("GROUP LIKE '%{0}%' ", cmb.Text);
                    }
                    else if (cmb.Name == "Trademark")
                    {
                        source2.Filter = string.Format("TRADEMARK LIKE '%{0}%' ", cmb.Text);
                    }
                    else
                    {
                    }
                    dataGridItem.ClearSelection();
                }
            }
            catch
            {
            }

            GetMaxDocID();
        }
        private void ITEM_CODE_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!itemSelected)
                {
                    if (ITEM_CODE.Text == "")
                    {
                        PNL_DATAGRIDITEM.Visible = false;
                    }
                    else
                    {
                       // bindgridview();
                        PNL_DATAGRIDITEM.Visible = true;
                        source2.Filter = string.Format("[ITEM_CODE] LIKE '%{0}%'", ITEM_CODE.Text);
                        //dataGridItem.ClearSelection();
                        dataGridItem.Columns["Item Name"].Width = 250;
                    }

                    if (clearitemname)
                    {
                        ITEM_CODE.Text = "";
                        clearitemname = false;
                    }
                }

            }
            catch
            {
            }
        }
        private void RTL_PRICE_Leave(object sender, EventArgs e)
        {
            if (RTL_PRICE.Text == "")
            {
                RTL_PRICE.Text = "0.00";
            }
        }
        private void SUPPLIER_CODE_TextChanged(object sender, EventArgs e)
        {
            CalculateRate();
            if (SUPPLIER_CODE.Text != "")
            {
                cmbState.SelectedValue = getState(SUPPLIER_CODE.Text);
            }
            else
            {
                cmbState.SelectedValue = "KL";
            }
        }
        private void PRICE_TYPE_TextChanged(object sender, EventArgs e)
        {
            CalculateRate();
        }
        public void CalculateRate()
        {
            if (PRICE_FOB.Text != "" && PRICE_TYPE.Text != "")
            {
                try
                {
                    if (RateType == "Percentage")
                    {
                        RTL_PRICE.Text = (Convert.ToDecimal(PRICE_FOB.Text) + ((Convert.ToDecimal(PRICE_FOB.Text) * Convert.ToDecimal(PRICE_TYPE.Text)) / 100)).ToString();
                    }
                    else
                    {
                        RTL_PRICE.Text = (Convert.ToDecimal(PRICE_FOB.Text) + Convert.ToDecimal(PRICE_TYPE.Text)).ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        string RateType = "Percentage";
        private void PRICE_TYPE_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == '-')
                {
                    if (RateType == "Percentage")
                    {
                        LBLRATETYPE.Text = "Rtl.Amt";
                        RateType = "Amount";
                        CalculateRate();

                    }
                    else if (RateType == "Amount")
                    {
                        LBLRATETYPE.Text = "Sale %";
                        RateType = "Percentage";
                        CalculateRate();
                    }
                    else
                    {
                        RateType = "Amount";
                        LBLRATETYPE.Text = "Rtl.Amt";
                        CalculateRate();
                    }


                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void dgItems_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            firstitemlistbyname = true;
            if (dgItems.Rows.Count > 0 && dgItems.CurrentRow != null)
            {
                Editactive = true;
                selectedRow = dgItems.CurrentRow.Index;
                DataGridViewCellCollection c = dgItems.CurrentRow.Cells;
                ITEM_CODE.Text = Convert.ToString(c["uCode"].Value);
                ITEM_NAME.Text = Convert.ToString(c["uName"].Value);
                DiscType = Convert.ToString(c["uDiscType"].Value);
                if (hasBatch)
                {
                    BATCH.Text = Convert.ToString(c["uBatch"].Value);
                    EXPIRY_DATE.Value = DateTime.ParseExact(Convert.ToString(c["uExpDate"].Value), "dd/MM/yyyy",CultureInfo.CurrentCulture);
                }

                QTY_RCVD.Text = Convert.ToString(c["uQty"].Value);
                PRICE_FOB.Text = Convert.ToString(c["uPrice"].Value);
                pricefob = Convert.ToDouble(PRICE_FOB.Text);
                if (hasTax)
                {
                    ITEM_TAX_PER.Text = Convert.ToString(c["uTaxPercent"].Value);
                    ITEM_TAX.Text = Convert.ToString(c["uTaxAmt"].Value);
                }
                ITEM_GROSS.Text = Convert.ToString(c["uTotal"].Value);

                addUnits();
                UOM.Text = Convert.ToString(c["uUnit"].Value);
                SERIALNO.Text = Convert.ToString(c["SerialNos"].Value);
                if (SERIALNO.Text != "")
                {
                    PNLSERIAL.Visible = true;
                }
                else
                {
                    PNLSERIAL.Visible = false;
                }

                RTL_PRICE.Text = Convert.ToString(c["uRTL_PRICE"].Value);
                ITEM_DISCOUNT.Text =Convert.ToString(c["uDiscValue"].Value);
                if (c["MRP"].Value != null)
                {
                    txtMRP.Text = Convert.ToString(c["MRP"].Value);
                }
                Item_Discount_Amt = Convert.ToDouble(c["uDiscount"].Value);
                lblDiscRate.Text = (Convert.ToString(c["uDiscType"].Value) == "Percentage" ? "Disc %" : "Amt");
                for (int j = 22; j < dgItems.Columns.Count; j++)
                {
                    for (int k = 0; k < dgSubPrices.RowCount; k++)
                    {
                        if (dgItems.Columns[j].HeaderText == dgSubPrices.Rows[k].Cells[0].Value.ToString())
                        {
                            dgSubPrices.Rows[k].Cells[2].Value = c[dgItems.Columns[j].HeaderText].Value.ToString();
                        }
                    }
                }
            }
            PNL_DATAGRIDITEM.Visible = false;
            QTY_RCVD.Focus();
            currencyflag = false;
            ITEM_DISCOUNT_TextChanged(sender, e);
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            //PrintingInitial();
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

                worksheet.Cells[2, 2] = "Supplier:";
                worksheet.Cells[2, 3] = txtSupplierName.Text;
                worksheet.Cells[3, 2] = "Date:";
                worksheet.Cells[3, 3] = DOC_DATE_GRE.Text;
                worksheet.Cells[4, 2] = "Invoice No:";
                worksheet.Cells[4, 3] = SUPPLIER_INV.Text;
                // storing header part in Excel
                for (int i = 1; i < dgItems.Columns.Count + 1; i++)
                {
                    worksheet.Cells[6, i] = dgItems.Columns[i - 1].HeaderText;
                }

                 int row=9;
              

                // storing Each row and column value to excel sheet
                for (int i = 0; i < dgItems.Rows.Count; i++)
                {
                    for (int j = 0; j < dgItems.Columns.Count-4; j++)
                    {
                        if (dgItems.Rows[i].Cells[j].Value != null)
                        {
                            worksheet.Cells[i + 8, j + 1] = dgItems.Rows[i].Cells[j].Value.ToString();
                        }
                    }
                    row++;
                }

                worksheet.Cells[row, 11] = "Gross Total :";
                worksheet.Cells[row, 12] = TOTAL_AMOUNT.Text;
                worksheet.Cells[row+1, 11] = "Discount :";
                worksheet.Cells[row+1, 12] = DISCOUNT.Text;
                worksheet.Cells[row + 2, 11] = "Net Total :";
                worksheet.Cells[row + 2, 12] = NETT_AMOUNT.Text;
                // save the application
                workbook.SaveAs("c:\\output.xls", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                // Exit from the application
                app.Quit();

            }
            catch
            {

            }
        }
        private void ITEM_DISCOUNT_TextChanged(object sender, EventArgs e)
        {
            CalculatingDiscount();
            TAX_PERCENT_TextChanged(sender, e);
        }
        public void CalculatingDiscount()
        {

            if (ITEM_DISCOUNT.Text.Trim() != "" && PRICE_FOB.Text.Trim() != "")
            {
                if (DiscType == "Amount")
                {

                    Item_Discount_Amt = Convert.ToDouble(ITEM_DISCOUNT.Text) * Convert.ToDouble(QTY_RCVD.Text);
                }
                else
                {
                    Item_Discount_Amt = ((Convert.ToDouble(PRICE_FOB.Text) * Convert.ToDouble(ITEM_DISCOUNT.Text)) / 100) * Convert.ToDouble(QTY_RCVD.Text);
                }
                if (ITEM_TAX.Text != "")
                {
                    ITEM_GROSS.Text = (((Convert.ToDouble(PRICE_FOB.Text) * Convert.ToDouble(QTY_RCVD.Text)) - Item_Discount_Amt + Convert.ToDouble(ITEM_TAX.Text))).ToString(decimalFormat);
                }
                else
                {
                    ITEM_GROSS.Text = ((Convert.ToDouble(PRICE_FOB.Text) * Convert.ToDouble(QTY_RCVD.Text)) - Item_Discount_Amt).ToString(decimalFormat);
                }
            }
            if (ITEM_DISCOUNT.Text.Trim() == "")
            {
                ITEM_DISCOUNT.Text = "0";
            }
            
        }
        private void ITEM_DISCOUNT_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == '-')
                {
                    if (DiscType == "Percentage")
                    {
                        lblDiscRate.Text = "Dis Amt";
                        DiscType = "Amount";
                        CalculatingDiscount();
                    

                    }
                    else if (DiscType == "Amount")
                    {
                        lblDiscRate.Text = "Disc %";
                        DiscType = "Percentage";
                        CalculatingDiscount();
                        
                    }
                    else
                    {
                        DiscType = "Amount";
                        lblDiscRate.Text = "Dis Amt";
                        CalculatingDiscount();
                        
                    }


                }


            }
            catch
            {
            }
        }
        private void DOC_REFERENCE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {

                if (PUR_FocusBarcode)
                {
                    ActiveControl = BARCODE;
                }
                else if (PUR_FocusItemCode)
                {
                    ActiveControl = ITEM_CODE;
                }
                else
                {
                    ActiveControl = ITEM_NAME;
                }
                Common.preventDingSound(e);


            }
        }
        private void Btb_upload_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
           
        }
        private void openFileDialog1_FileOk_1(object sender, CancelEventArgs e)
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
                        dgItems.Columns.Clear();
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
                        //    addItem();
                            dgItems.DataSource = dt;
                           
                        }
                    }
                }
            }

        }
        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            //TransDate = DOC_DATE_GRE.Value;
            ////if (type != "LGR.PRT")
            ////{
            ////    if (SUPPLIER_CODE.Text == "")
            ////    {
            ////        type = "PUR.CSS";
            ////    }
            ////}

            //if (type == "LGR.PRT")
            //{

            //}
            //else
            //{
            //    if (SUPPLIER_CODE.Text == "")
            //    {
            //        type = "PUR.CSS";
            //    }
            //    else
            //    {
            //        type = "PUR.CRD";
            //    }

            //}
            //if (valid())
            //{
            //    if (DISCOUNT.Text == "")
            //    {
            //        DISCOUNT.Text = "0";
            //    }
            //    string status = "Added!";
            //    if (ID == "")
            //    {
            //        //insert
            //        DOC_NO.Text = General.generatePurchaseID();
            //        POSTID = DOC_NO.Text;
            //        cmd.CommandText = "INSERT INTO INV_PURCHASE_HDR(DOC_TYPE,DOC_NO,DOC_DATE_GRE,DOC_DATE_HIJ,DOC_REFERENCE,SUPPLIER_CODE,NOTES,PAY_CODE,CARD_NO,TAX_TOTAL,CESS_AMOUNT,GROSS,DISCOUNT_VAL,NET_VAL,SUP_INV_NO,DEBITORACC,CREDITORACC,FREIGHT_AMT,SalesMan,BRANCH) VALUES('" + type + "','" + DOC_NO.Text + "','" + DOC_DATE_GRE.Value.ToString("yyyy/MM/dd") + "','" + DOC_DATE_HIJ.Text + "','" + DOC_REFERENCE.Text + "','" + SUPPLIER_CODE.Text + "','" + NOTES.Text + "','" + PAY_CODE.Text + "','" + CARD_NO.Text + "','" + TOTAL_TAX_AMOUNT.Text + "','" + CESS.Text + "','" + TOTAL_AMOUNT.Text + "','" + DISCOUNT.Text + "','" + NETT_AMOUNT.Text + "','" + SUPPLIER_INV.Text + "','" + drpdebitor.SelectedValue + "','" + drpCreditor.SelectedValue + "','" + Txt_freight.Text + "','" + SalesManCode + "','" + lg.Branch + "');";
            //    }
            //    else
            //    {

            //        DeleteTransation();

            //        ModifyType = "Update";
            //        modifiedtransaction();
            //        //update
            //        //       cmd.CommandText = "UPDATE INV_PURCHASE_HDR SET DOC_DATE_GRE = '" + DOC_DATE_GRE.Value.ToString("yyyy/MM/dd") + "',DOC_DATE_HIJ = '" + DOC_DATE_HIJ.Text + "',DOC_REFERENCE = '" + DOC_REFERENCE.Text + "',SUPPLIER_CODE = '" + SUPPLIER_CODE.Text + "',NOTES = '" + NOTES.Text + "',PAY_CODE = '" + PAY_CODE.Text + "',CARD_NO = '" + CARD_NO.Text + "',TAX_TOTAL = '" + TOTAL_TAX_AMOUNT.Text + "',CESS_AMOUNT = '" + CESS.Text + "',GROSS = '" + TOTAL_AMOUNT.Text + "',DISCOUNT_VAL = '" + DISCOUNT.Text + "',NET_VAL = '" + NETT_AMOUNT.Text + "',SUP_INV_NO='" + SUPPLIER_INV.Text + "' WHERE DOC_NO = '" + ID + "';DELETE FROM INV_PURCHASE_DTL WHERE DOC_NO = '" + ID + "';";
            //        cmd.CommandText = "UPDATE INV_PURCHASE_HDR SET DOC_DATE_GRE = '" + DOC_DATE_GRE.Value.ToString("yyyy/MM/dd") + "',DOC_DATE_HIJ = '" + DOC_DATE_HIJ.Text + "',DOC_REFERENCE = '" + DOC_REFERENCE.Text + "',SUPPLIER_CODE = '" + SUPPLIER_CODE.Text + "',NOTES = '" + NOTES.Text + "',PAY_CODE = '" + PAY_CODE.Text + "',CARD_NO = '" + CARD_NO.Text + "',TAX_TOTAL = '" + TOTAL_TAX_AMOUNT.Text + "',CESS_AMOUNT = '" + CESS.Text + "',GROSS = '" + TOTAL_AMOUNT.Text + "',DISCOUNT_VAL = '" + DISCOUNT.Text + "',NET_VAL = '" + NETT_AMOUNT.Text + "',SUP_INV_NO='" + SUPPLIER_INV.Text + "',POSTED='" + "N" + "',FREIGHT_AMT='" + Txt_freight.Text + "',SalesMan='" + SalesManCode + "' WHERE DOC_NO = '" + ID + "';DELETE FROM INV_PURCHASE_DTL WHERE DOC_NO = '" + ID + "';DELETE FROM INV_STK_TRX_HDR WHERE DOC_REFERENCE='" + ID + "';DELETE FROM INV_STK_TRX_DTL WHERE DOC_REFERENCE='" + ID + "';";

            //        status = "Updated!";
            //    }

            //    conn.Open();
            //    cmd.ExecuteNonQuery();
            //    conn.Close();

            //    #region insert purchase detail
            //    string query = "INSERT INTO INV_PURCHASE_DTL(DOC_TYPE,DOC_NO,ITEM_CODE,ITEM_DESC_ENG,UOM,QTY_RCVD,PRICE_FOB,RTL_PRICE,DiscType,DiscValue,NET_AMOUNT,BRANCH";
            //    if (hasBatch)
            //    {
            //        query += ",BATCH,EXPIRY_DATE";
            //    }
            //    if (hasTax)
            //    {
            //        query += ",ITEM_TAX_PER,ITEM_TAX";
            //    }
            //    query += ")";
            //    for (int i = 0; i < dgItems.Rows.Count; i++)
            //    {
            //        DataGridViewCellCollection c = dgItems.Rows[i].Cells;
            //        query += " SELECT '" + type + "','" + DOC_NO.Text + "','" + c["Code"].Value + "','" + c["Name"].Value + "','" + c["Unit"].Value + "','" + c["Qty"].Value + "','" + c["Price"].Value + "','" + c["RTL PRICE"].Value + "','" + c["Discount Type"].Value + "','" + c["Discount Amt"].Value + "','" + c["Net Amount"].Value + "','" + lg.Branch + "'";
            //        if (hasBatch)
            //        {
            //            query += ",'" + c["uBatch"].Value + "','" + DateTime.ParseExact(c["uExpDate"].Value.ToString(), "dd/MM/yyyy",null).ToString("yyyy/MM/dd") + "'";
            //        }
            //        if (hasTax)
            //        {
            //            query += ",'" + c["Tax%"].Value + "','" + c["Tax amt"].Value + "'";
            //        }
            //        query += " UNION ALL ";
            //    }

            //    query = query.Substring(0, query.Length - 10);
            //    cmd.CommandText = query;
            //    #endregion
            //    conn.Open();
            //    cmd.ExecuteNonQuery();
            //    conn.Close();

            //    if (DialogResult.Yes == MessageBox.Show("Do you want to update Retail Price?", "Retail Price Updatge", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            //    {
            //        UpdateRtl_Price();
            //    }

            //    if (!PAY_CODE.Text.Equals("CHQ"))
            //    {
            //        InsertTransaction();
            //    }

            //    if (Convert.ToDouble(TOTAL_TAX_AMOUNT.Text) > 0)
            //    {
            //        TaxTransaction();
            //    }
            //    if (Convert.ToDouble(EXCISE_DUTY.Text) > 0)
            //    {
            //        ExciseDutyTransaction();
            //    }
            //    if (Convert.ToDouble(DISCOUNT.Text) > 0)
            //    {
            //        DiscountTransaction();
            //    }
            //    if (Convert.ToDouble(CESS.Text) > 0)
            //    {
            //        CessTransaction();
            //    }
            //    MessageBox.Show("Record " + status);
            //    getDataFromDocNo();
            //    BindDetails();
            //    PostingProduct();
            //}
        }
        private void bt_barcode_Click(object sender, EventArgs e)
        {
            try
            {
                //for (int i = 0; i < dgItems.Rows.Count; ++i)
                //{
                //    string code = dgItems.Rows[i]. Cells["uCode"].Value.ToString();
                //    string na = dgItems.Rows[i].Cells["uName"].Value.ToString();
                //    string ar = dgItems.Rows[i].Cells["uName"].Value.ToString();
                //    string co = "";
                //    string un = dgItems.Rows[i].Cells["uUnit"].Value.ToString();
                //    string batch = "";
                //    string sto="";
                //    string rate = "";
                //    string ba = dgItems.Rows[i].Cells["uBarcode"].Value.ToString();
                //    string qty = dgItems.Rows[i].Cells["uQty"].Value.ToString();
                //    Barcode_Stock_Items bar = new Barcode_Stock_Items(code, na, ar, batch, un, ba, sto, rate, code,i);
                //    bar.Show();
                //}
                if (!hasBatch)
                {
                    FillPrintingGridhasnotbatch();
                    tabControl1.SelectedTab = tbBarcode;
                }
                else
                {
                    FillPrintingGridhasbatch();
                    tabControl1.SelectedTab = tbBarcode;
                }
                

            }
            catch
            {
                MessageBox.Show("Error in fetching has batch or not batch");
            }

            //    Barcode_Stock_Items ba = new Barcode_Stock_Items(itemcode, name, ara, batch, unit, barcode, sto, rate, code);
            //   br.FillPrintingGrid(itemcode,name,ara,batch,unit,barcode,sto,rate,code);
        }
        private void btnCurr_Click(object sender, EventArgs e)
        {
            try
            {
                CurrencyHelp h = new CurrencyHelp();
                if (h.ShowDialog() == DialogResult.OK && h.c != null)
                {
                    CURCODE.Text = Convert.ToString(h.c["CODE"].Value);
                }

            }
            catch
            {
            }
        }
        private void kryptonButton2_Click_1(object sender, EventArgs e)
        {
            try
            {
                CurrencyHelp h = new CurrencyHelp();
                if (h.ShowDialog() == DialogResult.OK && h.c != null)
                {
                   tb_baseCur.Text = Convert.ToString(h.c["CODE"].Value);
                }

            }
            catch
            {
            }
        }
        private void CURCODE_TextChanged(object sender, EventArgs e)
        {
            
        }
        private void kryptonLabel41_Paint(object sender, PaintEventArgs e)
        {

        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (tb_baseCur.Text != "" && CURCODE.Text != "" && currencyflag == true )
            {
                if(checkforex.Checked == true)
                {
                    for (int j = 0; j < dgItems.Rows.Count; ++j)
                    {
                        DataGridViewCellCollection c = dgItems.Rows[j].Cells;
                        //cmd.Connection = conn;
                        //conn.Open();
                        //cmd.CommandText = "SELECT RATE FROM GEN_CURRENCY WHERE CODE='" + CURCODE.Text + "'";
                        invHdr.CurrencyCode = CURCODE.Text;
                        curre = invHdr.getCurrencyRate();
                        //cmd.CommandText = "SELECT RATE FROM GEN_CURRENCY WHERE CODE='" + tb_baseCur.Text + "'";
                        invHdr.CurrencyCode = tb_baseCur.Text;
                        basecur = invHdr.getCurrencyRate();
                       // conn.Close();
                        Double basec = Convert.ToDouble(basecur);
                        Double crate = Convert.ToDouble(curre);
                        Double add = Convert.ToDouble(c["uPrice"].Value.ToString());
                        Double retail = Convert.ToDouble(c["uRTL_PRICE"].Value.ToString());
                        Double Total = Convert.ToDouble(c["uTotal"].Value.ToString());
                        Double netamt = Convert.ToDouble(c["uTotal"].Value.ToString());
                        Double grosstot = Convert.ToDouble(c["uNet_Amount"].Value.ToString());

                        Total = Total * (basec / crate);
                        add = add * (basec / crate);
                        grosstot = grosstot * (basec / crate);
                        string res = add.ToString("N" + decpoint);
                        retail = retail * (basec / crate);
                        string rtlreslt = retail.ToString("N" + decpoint);
                        string stotal = Total.ToString("N" + decpoint);
                        string sgross = grosstot.ToString("N" + decpoint);
                        c["uRTL_PRICE"].Value = rtlreslt;
                        c["uPrice"].Value = res;
                        c["uTotal"].Value = stotal;
                        c["uNet_Amount"].Value = sgross;
                        if (hasTax)
                        {
                            Double taxamt = Convert.ToDouble(c["uTaxAmt"].Value.ToString());
                            taxamt = taxamt * (basec / crate);
                            string stax = taxamt.ToString("N" + decpoint);
                            c["uTaxAmt"].Value = stax;
                        }
                    }
                }
            }
        }
        private void kryptonLinkLabel1_LinkClicked(object sender, EventArgs e)
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
        private void VOUCHNUM_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !VOUCHNUM.Text.Equals(""))
            {
                if (!initialload)
                {
                    Clear2();
                    int lastVoucherID = Convert.ToInt32(VOUCHNUM.Text);
                    VOUCHNUM.Text = (lastVoucherID).ToString();
                    checkvoucher(Convert.ToInt32(VOUCHNUM.Text));
                    try
                    {
                       // DataTable dt = new DataTable();
                        //cmd.CommandText = "SELECT * FROM INV_PURCHASE_HDR WHERE DOC_ID = '" + VOUCHNUM.Text + "'AND FLAGDEL='TRUE'";
                        //cmd.CommandType = CommandType.Text;
                        //SqlDataAdapter Adap = new SqlDataAdapter();
                        //Adap.SelectCommand = cmd;
                        //Adap.Fill(dt);
                        invHdr.DocNo = VOUCHNUM.Text;
                        DataTable dt = invHdr.getPurchaseHdrWthDelFlag();
                        if (dt.Rows.Count > 0)
                        {
                            ID = Convert.ToString(dt.Rows[0]["DOC_NO"]);
                            DOC_NO.Text = ID;
                            POSTID = ID;
                            DOC_DATE_GRE.Text = Convert.ToDateTime(dt.Rows[0]["DOC_DATE_GRE"].ToString()).ToShortDateString();
                            DOC_DATE_HIJ.Text = Convert.ToString(dt.Rows[0]["DOC_DATE_HIJ"]);
                            SUPPLIER_CODE.Text = Convert.ToString(dt.Rows[0]["SUPPLIER_CODE"]);
                            NOTES.Text = Convert.ToString(dt.Rows[0]["NOTES"]);
                            TOTAL_AMOUNT.Text = Convert.ToString(dt.Rows[0]["GROSS"]);
                            DISCOUNT.Text = Convert.ToString(dt.Rows[0]["DISCOUNT_VAL"]);
                            NETT_AMOUNT.Text = Convert.ToString(dt.Rows[0]["NET_VAL"]);
                            TOTAL_TAX_AMOUNT.Text = Convert.ToString(dt.Rows[0]["TAX_TOTAL"]);
                            CESS.Text = Convert.ToString(dt.Rows[0]["CESS_AMOUNT"]);
                            PAY_CODE.Text = Convert.ToString(dt.Rows[0]["PAY_CODE"]);
                            CARD_NO.Text = Convert.ToString(dt.Rows[0]["CARD_NO"]);
                            SUPPLIER_INV.Text = Convert.ToString(dt.Rows[0]["SUP_INV_NO"]);
                            Txt_freight.Text = Convert.ToString(dt.Rows[0]["FREIGHT_AMT"]);

                            if (dt.Rows[0]["OTHER_EXPENSES"].ToString() == "" || dt.Rows[0]["OTHER_EXPENSES"] == null)
                            {
                                txtLoadingCharge.Text = decimalFormat;
                            }
                            else
                                txtLoadingCharge.Text = dt.Rows[0]["OTHER_EXPENSES"].ToString();

                            if (Convert.ToInt32(dt.Rows[0]["PROJECTID"].ToString()) <= 0 || dt.Rows[0]["PROJECTID"] == null)
                            {
                                cmb_projects.SelectedIndex = 0;
                            }
                            else
                                cmb_projects.SelectedIndex = Convert.ToInt32(dt.Rows[0]["PROJECTID"]);

                            //getCur();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Button Down Click Catch 1 Message: " + ex.Message);
                    }
                    if (ID != "")
                    {
                       // conn.Open();
                        //cmd.CommandText = "SELECT * FROM INV_PURCHASE_DTL WHERE DOC_NO = '" + ID + "'AND FLAGDEL='TRUE'";
                        //cmd.CommandType = CommandType.Text;
                        //----FOR PRICE BATCH----//
                        //cmd.CommandText = "PREV_PURCHASE_INV";
                        //cmd.CommandType = CommandType.StoredProcedure;
                        //// cmd.Parameters.AddWithValue("@ID",ID);
                        //cmd.Parameters.Clear();
                        //cmd.Parameters.AddWithValue("@ID", SqlDbType.VarChar).Value = ID;

                         invDtl.DocNo = ID;
                        SqlDataReader r = invDtl.getPreviousPurDtl();
                       // cmd.CommandType = CommandType.Text;
                        int count = r.FieldCount;
                        while (r.Read())
                        {
                            int i = dgItems.Rows.Add(new DataGridViewRow());
                            DataGridViewCellCollection c = dgItems.Rows[i].Cells;
                            c["cSlno"].Value = i + 1;
                            c["uCode"].Value = r["ITEM_CODE"];
                            c["uName"].Value = r["ITEM_DESC_ENG"];
                            c["colBATCH"].Value = r["PRICE_BATCH"];
                            if (hasBatch)
                            {
                                c["uBatch"].Value = r["BATCH"];
                                if (r["EXPIRY_DATE"].ToString() != "")
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
                            c["uomQty"].Value = r["UOM_QTY"];
                            c["cost_price"].Value = r["cost_price"];
                            if (r["PRICE_BATCH"].ToString() == "")
                            {
                                c["colBATCH"].Value = null;
                            }
                            else
                            {
                                c["colBATCH"].Value = r["PRICE_BATCH"];
                            }
                            // int count = r.FieldCount;
                            for (int j = 38; j < count; j++)
                            {
                                for (int k = 22; k < dgItems.Columns.Count; k++)
                                {
                                    if (r.GetName(j).ToString() == dgItems.Columns[k].HeaderText)
                                    {
                                        //c[dgItems.Columns[k].HeaderText].Value = (Convert.ToDouble(r[j]) * Convert.ToDouble(r["UOM_QTY"])).ToString();
                                        c[dgItems.Columns[k].HeaderText].Value = (Convert.ToDouble(r[j].ToString().Equals("") ? "0" : r[j].ToString()) * Convert.ToDouble(r["UOM_QTY"])).ToString();
                                    }
                                }
                            }
                        }
                       // conn.Close();
                        DbFunctions.CloseConnection();
                        GETLEDGERDETAILS();
                        if (dgItems.Rows.Count > 0)
                        {
                            calculateGSTTaxes();
                        }
                    }
                    else
                    {
                        MessageBox.Show("No invoice");
                    }
                    
                }
            }
            if (e.KeyCode == Keys.Back)
            {
                VOUCHNUM.ReadOnly = false;
            }

        }
        private void drpCreditor_KeyDown(object sender, KeyEventArgs e)
        {
            drpCreditor.DroppedDown = false;
        }
        private void drpdebitor_KeyDown(object sender, KeyEventArgs e)
        {
            drpdebitor.DroppedDown = false;
        }
        private void BankAccount()
        {
            //DataTable dt = new DataTable();
            //cmd = new SqlCommand("SELECT DISTINCT LEDGERNAME FROM tb_Ledgers WHERE UNDER IN (10,21,22)", conn);
            //SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            //adapter.Fill(dt);
            drpdebitor.DataSource = invHdr.getBankAccounts();
            drpdebitor.DisplayMember = "LEDGERNAME";
        }
        private void PAY_CODE_TextChanged(object sender, EventArgs e)
        {
            if (PAY_CODE.Text == "CRD" || PAY_CODE.Text == "CHQ" || PAY_CODE.Text == "DEP")
            {
                kryptonLabel29.Text = "Bank Acc :";
                BankAccount();
                kryptonLabel23.Visible = true;
                CARD_NO.Visible = true;
            }
            else
            {
                kryptonLabel29.Text = "Cash Acc /Party Acc :";
                GetLedgers();
                kryptonLabel23.Visible = false;
                CARD_NO.Visible = false;
                kryptonLabel44.Visible = false;
                CHQ_DATE.Visible = false;
            }
            if (PAY_CODE.Text == "CRD")
            {
                kryptonLabel23.Text = "Card No:";
                kryptonLabel44.Visible = false;
                CHQ_DATE.Visible = false;
            }
            else if (PAY_CODE.Text == "DEP")
            {
                kryptonLabel23.Text = "Acc No:";
                kryptonLabel44.Visible = false;
                CHQ_DATE.Visible = false;
            }
            else
            {
                kryptonLabel23.Text = "Cheque No.:";
                kryptonLabel44.Visible = true;
                CHQ_DATE.Visible = true;
            }
        }
        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            printeditems = 0;
            PrintingInitial();
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
                PrintDocument printDocument = new PrintDocument();
                PrintPreviewDialog prvdlg = new PrintPreviewDialog();
                if (PrintPage.SelectedIndex == 0)
                {
                    printDocument.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("smallzize", 360, height + 250);
                    printDocument.PrintPage += printDocument_PrintPage;         
                    prvdlg.Document = printDocument;
                    ((ToolStripButton)((ToolStrip)prvdlg.Controls[1]).Items[0]).Click += printPreview_PrintClick;
                    prvdlg.ShowDialog();
                }
                else if (PrintPage.SelectedIndex == 1)
                {
                    PrintDocument printDocumentMediumSize = new PrintDocument();
                    PrintPreviewDialog prvdlg1 = new PrintPreviewDialog();
                    printDocumentMediumSize.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("MediumSize", 914, height + 450);
                    printDialog1.Document = printDocumentMediumSize;
                    printDocumentMediumSize.PrintPage += printDocumentMediumSize_PrintPage;
                    prvdlg1.Document = printDocumentMediumSize;
                    ((ToolStripButton)((ToolStrip)prvdlg1.Controls[1]).Items[0]).Click += printPreview_PrintClick;
                    prvdlg1.ShowDialog();
                }
                else if (PrintPage.SelectedIndex == 2)
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
                    prvdlg2.Document = printDocumentA4;
                    ((ToolStripButton)((ToolStrip)prvdlg2.Controls[1]).Items[0]).Click += printPreview_PrintClick;
                    prvdlg2.ShowDialog();
                }
                else if (PrintPage.SelectedIndex == 3)
                {
                    GetDynamicPageSetting();
                    PrintDocument printDocumentDynamic = new PrintDocument();
                    PrintPreviewDialog prvdlg3 = new PrintPreviewDialog();
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
                    prvdlg3.Document = printDocumentDynamic;
                    ((ToolStripButton)((ToolStrip)prvdlg3.Controls[1]).Items[0]).Click += printPreview_PrintClick;
                    prvdlg3.ShowDialog();
                }
            }
            catch
            {
                MessageBox.Show("printing Problem");
            }
        }
        int m = 0;
        decimal tgrossrate = 0; decimal ttaxva = 0; decimal trate = 0; decimal tcdis = 0; decimal ttaxbl = 0; decimal tfree = 0;
        decimal tqty = 0;

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
                System.Drawing.Font printFont = new System.Drawing.Font("Courier New", 8);
                var tabDataForeColor = System.Drawing.Color.Black;
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

                    e.Graphics.DrawString("-----------------------------------------------", printFont, new SolidBrush(System.Drawing.Color.Black), xpos, starty + offset, sf);
                    e.Graphics.DrawString("-----------------------------------------------", printFont, new SolidBrush(System.Drawing.Color.Black), xpos, starty + offset + 3, sf);

                    offset = offset + 10;
                    e.Graphics.DrawString("GSTIN:" + TineNo, printFont, new SolidBrush(tabDataForeColor), startx, starty + offset);
                    offset = offset + 12;
                    e.Graphics.DrawString("Supplier:" + txtSupplierName.Text, printFont, new SolidBrush(tabDataForeColor), startx, starty + offset);
                    System.Drawing.Font itemhead = new System.Drawing.Font("Courier New", 8);
                    offset = offset + 12;
                    e.Graphics.DrawString("-----------------------------------------------", printFont, new SolidBrush(System.Drawing.Color.Black), startx, starty + offset);
                    offset = offset + 12;

                    string headtext = "Item".PadRight(20) + "Tax%".PadRight(5) + "Qty".PadRight(5) + "Rate".PadRight(10) + "Total";
                    e.Graphics.DrawString(headtext, itemhead, new SolidBrush(System.Drawing.Color.Black), startx, starty + offset - 1);
                    e.Graphics.DrawString("-----------------------------------------------", printFont, new SolidBrush(System.Drawing.Color.Black), startx, starty + offset + 7);
                    offset = offset + 15;
                    System.Drawing.Font font = new System.Drawing.Font("Courier New", 8);
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
                            e.Graphics.DrawString(productline, font, new SolidBrush(System.Drawing.Color.Black), startx, starty + offset);
                            offset = offset + (int)fontheight + 5;
                        }
                    }
                    catch
                    {

                    }


                    e.Graphics.DrawString("-----------------------------------------------", printFont, new SolidBrush(System.Drawing.Color.Black), startx, starty + offset);
                    offset = offset + 12;
                    string grosstotal = "Gros Total:".PadRight(5) + Spell.SpellAmount.comma(Convert.ToDecimal(TOTAL_AMOUNT.Text));
                    string vatstring = "Tax Amount:".PadRight(5) + Spell.SpellAmount.comma(Convert.ToDecimal(TOTAL_TAX_AMOUNT.Text));
                    string Discountstring = "Discount:".PadRight(5) + Spell.SpellAmount.comma(Convert.ToDecimal(DISCOUNT.Text));
                    string total = "Total:".PadRight(5) + Spell.SpellAmount.comma(Convert.ToDecimal(NETT_AMOUNT.Text));

                    e.Graphics.DrawString(grosstotal, font, new SolidBrush(System.Drawing.Color.Black), startx + 200, starty + offset + 3);
                    offset = offset + (int)fontheight + 5;
                    e.Graphics.DrawString(vatstring, font, new SolidBrush(System.Drawing.Color.Black), startx + 200, starty + offset + 3);
                    offset = offset + (int)fontheight + 4;
                    e.Graphics.DrawString(Discountstring, font, new SolidBrush(System.Drawing.Color.Black), startx + 200, starty + offset + 3);
                    offset = offset + (int)fontheight + 4;
                    e.Graphics.DrawString("---------------", font, new SolidBrush(System.Drawing.Color.Black), startx + 200, starty + offset + 3);
                    offset = offset + (int)fontheight + 4;
                    e.Graphics.DrawString(total, font, new SolidBrush(System.Drawing.Color.Black), startx + 200, starty + offset + 3);
                    offset = offset + (int)fontheight + 5;
                    try
                    {
                        string test = Spell.SpellAmount.InWrods(Convert.ToDecimal(NETT_AMOUNT.Text));

                        int index = test.IndexOf("Taka ");
                        int l = test.Length;
                        test = test.Substring(index + 4);

                        e.Graphics.DrawString(test, font, new SolidBrush(System.Drawing.Color.Black), startx, starty + offset + 3);
                    }
                    catch
                    {
                    }
                }
                e.HasMorePages = false;
            }

        }

        public void printPreview_PrintClick(object sender, EventArgs e)
        {
            try
            {
                PrintDialog printdialog = new PrintDialog();
                printdialog.AllowSelection = true;
                printdialog.AllowSomePages = true;
                PrintDocument doc = new PrintDocument();
                m = 0;
                tgrossrate = 0; ttaxva = 0; trate = 0; tcdis = 0; ttaxbl = 0; tfree = 0;
                tqty = 0; printeditems = 0;
                doc.PrintPage += new PrintPageEventHandler(printDocumentA4Form8_PrintPage);
                doc.Print();
                /*if (PrintPage.SelectedIndex == 0)
                {
                    PrintDocument doc = new PrintDocument();
                    m = 0;
                    tgrossrate = 0; ttaxva = 0; trate = 0; tcdis = 0; ttaxbl = 0; tfree = 0;
                    tqty = 0; printeditems = 0;
                    doc.PrintPage += new PrintPageEventHandler(printDocumentA4Form8_PrintPage);
                    doc.Print();
                }*/
                

                /*if (PrintPage.SelectedIndex == 5)
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
                }*/

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ToString());
            }
        }

        void printDocumentA4Form8_PrintPage(object sender, PrintPageEventArgs e)
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

            System.Drawing.Font Headerfont1 = new System.Drawing.Font("Calibri", 15, FontStyle.Bold);
            System.Drawing.Font dec = new System.Drawing.Font("Calibri", 8, FontStyle.Regular);
            System.Drawing.Font Headerfont2 = new System.Drawing.Font("Times New Roman", 8, FontStyle.Regular);
            System.Drawing.Font Headerfont3 = new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold);
            System.Drawing.Font Headerfont5 = new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular);
            System.Drawing.Font Headerfont6 = new System.Drawing.Font("Times New Roman", 10, FontStyle.Bold);
            System.Drawing.Font Headerfont7 = new System.Drawing.Font("Times New Roman", 13, FontStyle.Regular);
            System.Drawing.Font Headerfont8 = new System.Drawing.Font("Times New Roman", 11, FontStyle.Bold);
            System.Drawing.Font printFont = new System.Drawing.Font("Calibri", 10);
            System.Drawing.Font printFontBold = new System.Drawing.Font("Calibri", 10, FontStyle.Bold);
            System.Drawing.Font printFontBold1 = new System.Drawing.Font("Calibri", 14, FontStyle.Bold);
            System.Drawing.Font printFontnet = new System.Drawing.Font("Calibri", 11, FontStyle.Bold);
            System.Drawing.Font printbold = new System.Drawing.Font("Calibri", 10, FontStyle.Bold);
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
            var tabDataForeColor = System.Drawing.Color.Black;
            int height = 100 + y;

            Pen blackPen1 = new Pen(System.Drawing.Color.Black, 1);
            var txtDataWidth = e.Graphics.MeasureString(CompanyName, Headerfont1).Width;

            try
            {

               // address1 = "CLASSIC TRADE LINKS";
             //   address2 = "Door No.6/575-D, Nallalam Road, Areekkad, Nallalam P O";
              //  string mob1 = "7561020007";


                address1 = company.Name;
                address2 = company.Address;
                string mob1 = company.Phone;



                int centerOfPage = e.PageBounds.Width / 2;

                int nameStartPosision = centerOfPage - TextRenderer.MeasureText(company.Name, Headerfont3).Width / 2;

                e.Graphics.DrawString(address1, Headerfont3, new SolidBrush(System.Drawing.Color.Black), centerOfPage, 40);
                e.Graphics.DrawString(address2, Headerfont3, new SolidBrush(System.Drawing.Color.Black), 220, 60);
                e.Graphics.DrawString("Ph : " + mob1, Headerfont2, new SolidBrush(System.Drawing.Color.Black), 25, 35);
                e.Graphics.DrawString("Fax : ", Headerfont2, new SolidBrush(System.Drawing.Color.Black), 25, 50);
               // e.Graphics.DrawString("THE KERALA VALUE ADDED TAX RULES 2005", Headerfont2, new SolidBrush(System.Drawing.Color.Black), 550, 90);
                e.Graphics.DrawString("FORM NO.8", Headerfont6, new SolidBrush(System.Drawing.Color.Black), 710, 105);
                e.Graphics.DrawString("[See rule 58(10)]", Headerfont2, new SolidBrush(System.Drawing.Color.Black), 605, 120);
                e.Graphics.DrawString("TAX INVOICE", Headerfont6, new SolidBrush(System.Drawing.Color.Black), 694, 120);
                e.Graphics.DrawString("Date" + "          : " + DOC_DATE_GRE.Text, Headerfont6, new SolidBrush(System.Drawing.Color.Black), 652, 135);
                e.Graphics.DrawString("C A S H/C R E D I T", Headerfont8, new SolidBrush(System.Drawing.Color.Black), 325, 135);
                //e.Graphics.DrawString("CST No:" + c, Headerfont2, new SolidBrush(System.Drawing.Color.Black), 688, 20);
                e.Graphics.DrawString("Ph No :" + Phone, Headerfont2, new SolidBrush(System.Drawing.Color.Black), startx - 26, 200);

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
                e.Graphics.DrawString("GSTIN:" + TineNo, Headerfont2, new SolidBrush(System.Drawing.Color.Black), 25, 20);
                offset = offset + 16;
                e.Graphics.DrawString("Invoice No" + "   :   " + VOUCHNUM.Text, Headerfont6, new SolidBrush(System.Drawing.Color.Black), startx - 26, 135);
                offset = offset + 16;
                offset = offset + 16;
                offset = offset + 16;
                offset = offset + 16;
                offset = offset + 16;
                offset = offset + 16;
                offset = offset + 16;
                offset = offset + 16;
                Pen blackPen = new Pen(System.Drawing.Color.Black, 1);
                Point point1 = new Point(45, 190);
                Point point2 = new Point(760, 190);
                e.Graphics.DrawString("M/S:" + txtSupplierName.Text, Headerfont5, new SolidBrush(System.Drawing.Color.Black), startx - 26, starty + offset - 20);
                e.Graphics.DrawString(address1, Headerfont5, new SolidBrush(System.Drawing.Color.Black), 24, starty + offset);
                e.Graphics.DrawString("TIN:" + TineNo, Headerfont6, new SolidBrush(System.Drawing.Color.Black), 550, 190);
                System.Drawing.Font itemhead = new System.Drawing.Font("Times New Roman", 8);
                offset = offset + 2;
                Point point3 = new Point(45, 219);
                Point point4 = new Point(760, 219);
                e.Graphics.DrawLine(blackPen, 10, 240, 800, 240); //(45,190,760,190)
                e.Graphics.DrawLine(blackPen, 10, 260, 800, 260);//(45,219,760,219)
                offset = offset + 50;
                string headtext = "No".PadRight(18) + "Particulars".PadRight(118) + "Quantity".PadRight(15) + "Unit".PadRight(15) + "Tax".PadRight(15) + "Rate".PadRight(15) + "Amount";
                e.Graphics.DrawString(headtext, printFontBold, new SolidBrush(System.Drawing.Color.Black), startx - 40, starty + offset + 10);

                offset = offset + 40;
                System.Drawing.Font font = new System.Drawing.Font("Times New Roman", 10);
                float fontheight = font.GetHeight();

                try
                {

                    int i = 0;
                    int printpoint = 70; //32
                    int j = 1;
                    string mrp = "";
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

                                rate = Convert.ToDouble(row.Cells["cPrice"].Value).ToString(decimalFormat);
                                try
                                {
                                    mrp = Convert.ToDouble( row.Cells["Mrp"].Value).ToString(decimalFormat);
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
                                discp = ((Convert.ToDouble(tcdis) * 100) / Convert.ToDouble(grossamounttotal)).ToString(decimalFormat);
                                gross = (Convert.ToDouble(qty) * Convert.ToDouble(rate)).ToString(decimalFormat);
                                price = Convert.ToDouble(row.Cells[12].Value).ToString(decimalFormat);
                                taxval = Convert.ToDouble(row.Cells["cTaxAmt"].Value);
                                txbval = "";
                                if (taxval != 0)
                                {
                                    txbval = (Convert.ToDouble(qty) * Convert.ToDouble(rate) - Convert.ToDouble(disc)).ToString(decimalFormat);
                                    ttaxbl = ttaxbl + Convert.ToDecimal(txbval);
                                }
                                else
                                {
                                    txbval = (Convert.ToDouble(qty) * Convert.ToDouble(rate) - Convert.ToDouble(disc)).ToString(decimalFormat);

                                }

                                pricWtax = Convert.ToDouble(rate) + (Convert.ToDouble(rate) * Convert.ToDouble(qty)) * (Convert.ToDouble(tax) / 100 / Convert.ToDouble(qty));
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
                                e.Graphics.DrawString(m.ToString(), font, new SolidBrush(System.Drawing.Color.Black), 10, starty + offset);
                                e.Graphics.DrawString(name, Headerfont2, new SolidBrush(System.Drawing.Color.Black), startx + 5, starty + offset);
                                e.Graphics.DrawString(qty, font, new SolidBrush(System.Drawing.Color.Black), startx + 455, starty + offset);
                                e.Graphics.DrawString(UOM, font, new SolidBrush(System.Drawing.Color.Black), startx + 510, starty + offset);
                                e.Graphics.DrawString(rate, font, new SolidBrush(System.Drawing.Color.Black), startx + 655, starty + offset, format);
                                e.Graphics.DrawString(tax, font, new SolidBrush(System.Drawing.Color.Black), startx + 590, starty + offset, format);
                                total = row.Cells["cTotal"].Value.ToString();
                                e.Graphics.DrawString(total, font, new SolidBrush(System.Drawing.Color.Black), startx + 735, starty + offset, format);
                                offset = offset + (int)fontheight + 10;
                                if ((string)row.Cells["description"].Value != "")
                                {
                                    try
                                    {
                                        e.Graphics.DrawString(row.Cells["description"].Value.ToString(), font, new SolidBrush(System.Drawing.Color.Black), startx + 30, starty + offset);
                                        offset = offset + (int)fontheight + 35;
                                    }
                                    catch {}
                                }


                                while (BALANCELENGH > 1)
                                {
                                    name2 = BALANCELENGH <= 70 ? row.Cells["cName"].Value.ToString().Substring(printpoint, BALANCELENGH) : row.Cells["cName"].Value.ToString().Substring(printpoint, 70);
                                    e.Graphics.DrawString(name2, Headerfont2, new SolidBrush(System.Drawing.Color.Black), startx + 30, starty + offset - 9);
                                    BALANCELENGH = BALANCELENGH - 70;
                                    printpoint = printpoint + 70;
                                    starty = starty + (int)fontheight;
                                }
                                printpoint = 70;
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
                                e.Graphics.DrawString("coutinue...", printFontBold, new SolidBrush(System.Drawing.Color.Black), startx + 40, 940);
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
                        e.Graphics.DrawString("Gross Amount", printFontBold, new SolidBrush(System.Drawing.Color.Black), 560, 888);
                        e.Graphics.DrawString("Discount (%)", Headerfont2, new SolidBrush(System.Drawing.Color.Black), 560, 904);
                        e.Graphics.DrawString("Trd.Discount (Amt)", Headerfont2, new SolidBrush(System.Drawing.Color.Black), 560, 919);
                        e.Graphics.DrawString("Total", Headerfont2, new SolidBrush(System.Drawing.Color.Black), 560, 934);
                        e.Graphics.DrawString("VAT", Headerfont2, new SolidBrush(System.Drawing.Color.Black), 560, 950);
                        e.Graphics.DrawString("Cess", Headerfont2, new SolidBrush(System.Drawing.Color.Black), 560, 966);
                        e.Graphics.DrawString("Adl Discount", Headerfont2, new SolidBrush(System.Drawing.Color.Black), 560, 982);
                        e.Graphics.DrawString("Handling/Other", Headerfont2, new SolidBrush(System.Drawing.Color.Black), 560, 998);
                        e.Graphics.DrawString("G.Total", Headerfont6, new SolidBrush(System.Drawing.Color.Black), 560, 1014);
                        e.Graphics.DrawString(tqty.ToString(), printFontBold, new SolidBrush(System.Drawing.Color.Black), startx + 280, 870);
                        e.Graphics.DrawString(tcdis.ToString(), Headerfont2, new SolidBrush(System.Drawing.Color.Black), 788, 919, format);
                        e.Graphics.DrawString(ttaxva.ToString(), Headerfont2, new SolidBrush(System.Drawing.Color.Black), 788, 950, format);
                        e.Graphics.DrawString(netamounttotal.ToString("N2"), Headerfont2, new SolidBrush(System.Drawing.Color.Black), 788, 934, format);
                        e.Graphics.DrawString(Txt_freight.Text, Headerfont2, new SolidBrush(System.Drawing.Color.Black), 788, 998, format);
                        e.Graphics.DrawString(CESS.Text, Headerfont2, new SolidBrush(System.Drawing.Color.Black), 788, 966, format);
                        e.Graphics.DrawString(DISCOUNT.Text, Headerfont2, new SolidBrush(System.Drawing.Color.Black), 788, 982, format);
                        e.Graphics.DrawString(discp.ToString(), Headerfont2, new SolidBrush(System.Drawing.Color.Black), 788, 904, format);
                        string t1 = "Certified that particulars shown in the above tax invoice are true and correct and that my/our registration under KVAT Act";
                        string t2 = "2003nis valid as on the date of the bill";
                        e.Graphics.DrawString("DECLARATION", Headerfont6, new SolidBrush(System.Drawing.Color.Black), 25, 1085);
                        e.Graphics.DrawString(t1, Headerfont2, new SolidBrush(System.Drawing.Color.Black), 25, 1100);
                        e.Graphics.DrawString(t2, Headerfont2, new SolidBrush(System.Drawing.Color.Black), 25, 1115);
                        e.Graphics.DrawString("Authorised Signatory", printFontBold, new SolidBrush(System.Drawing.Color.Black), 680, 1150);
                        e.Graphics.DrawLine(blackPen1, 10, 1033, 800, 1033);
                        e.Graphics.DrawString("Terms & Condition", printFontBold, new SolidBrush(System.Drawing.Color.Black), 25, 1050);
                        string neta = Convert.ToDecimal(NETT_AMOUNT.Text).ToString(decimalFormat);
                        e.Graphics.DrawString(neta, printFontBold, new SolidBrush(System.Drawing.Color.Black), 788, 1014, format);
                        e.Graphics.DrawString(grossamounttotal.ToString(decimalFormat), printFontBold, new SolidBrush(System.Drawing.Color.Black), 788, 888, format); //Gross Amount
                        try
                        {
                            int cash = (int)Convert.ToDouble(NETT_AMOUNT.Text);
                            string cas = NETT_AMOUNT.Text;
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
                            e.Graphics.DrawString("Crd/Trk No :", Headerfont6, new SolidBrush(System.Drawing.Color.Black), 25, 985);
                            e.Graphics.DrawString("R.Off :" + txtRoundOff.Text, Headerfont6, new SolidBrush(System.Drawing.Color.Black), 25, 999);
                            e.Graphics.DrawString(test3, Headerfont6, new SolidBrush(System.Drawing.Color.Black), 25, 1014);
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
            e.HasMorePages = hasmorepages;
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
            System.Drawing.Font printFont = new System.Drawing.Font("Courier New", 11);
            var tabDataForeColor = System.Drawing.Color.Black;
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
                e.Graphics.DrawString("SUPPLIER:" + txtSupplierName.Text, printFont, new SolidBrush(tabDataForeColor), startx, starty + offset - 2);
                System.Drawing.Font itemhead = new System.Drawing.Font("Courier New", 10);
                offset = offset + 15;
                e.Graphics.DrawString("----------------------------------------------------------", printFont, new SolidBrush(System.Drawing.Color.Black), startx, starty + offset);
                offset = offset + 14;

                string headtext = "Item".PadRight(29) + "Price".PadRight(11) + "Qty".PadRight(8) + "Value";
                e.Graphics.DrawString(headtext, itemhead, new SolidBrush(System.Drawing.Color.Black), startx, starty + offset);
                e.Graphics.DrawString("----------------------------------------------------------", printFont, new SolidBrush(System.Drawing.Color.Black), startx, starty + offset + 13);
                offset = offset + 26;
                System.Drawing.Font font = new System.Drawing.Font("Courier New", 10);

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
                        e.Graphics.DrawString(productline, font, new SolidBrush(System.Drawing.Color.Black), startx, starty + offset);
                        offset = offset + (int)fontheight + 7;
                    }
                }
                catch {}

                for (int i = 0; i < 3; i++)
                {
                    e.Graphics.DrawString("", font, new SolidBrush(System.Drawing.Color.Black), startx, starty + offset);
                    offset = offset + (int)fontheight + 5;
                }

                e.Graphics.DrawString("----------------------------------------------------------", printFont, new SolidBrush(System.Drawing.Color.Black), startx, starty + offset);
                offset = offset + 8;
                string grosstotal = "Gross Total:".PadRight(7) + Spell.SpellAmount.comma(Convert.ToDecimal(TOTAL_AMOUNT.Text));
                //string vatstring = "Tax Amount:".PadRight(5) + Spell.SpellAmount.comma(Convert.ToDecimal(TAX_TOTAL.Text));
                string Discountstring = "Discount:".PadRight(47) + Spell.SpellAmount.comma(Convert.ToDecimal(DISCOUNT.Text));
                string total = "Total:".PadRight(47) + Spell.SpellAmount.comma(Convert.ToDecimal(NETT_AMOUNT.Text));

                //e.Graphics.DrawString(grosstotal, font, new SolidBrush(Color.Black), startx + 290, starty + offset + 6);
                //offset = offset + (int)fontheight +3;
                //e.Graphics.DrawString(vatstring, font, new SolidBrush(Color.Black), startx + 200, starty + offset + 3);
                //offset = offset + (int)fontheight + 4;
                if (Convert.ToDecimal(DISCOUNT.Text) > 0)
                {
                    e.Graphics.DrawString(Discountstring, font, new SolidBrush(System.Drawing.Color.Black), startx, starty + offset + 3);
                    offset = offset + (int)fontheight + 1;
                }
                e.Graphics.DrawString(total, font, new SolidBrush(System.Drawing.Color.Black), startx, starty + offset + 3);
                offset = offset + 18;
                e.Graphics.DrawString("----------------------------------------------------------", printFont, new SolidBrush(System.Drawing.Color.Black), startx, starty + offset);
                offset = offset + 59;
                try
                {
                    System.Drawing.Font amountingeng = new System.Drawing.Font("Courier New", 10);
                    e.Graphics.DrawString("THANK YOU VISIT AGAIN...", amountingeng, new SolidBrush(System.Drawing.Color.Black), xpos, starty + offset, sf);
                }
                catch
                {
                }
                offset = offset + 15;
                e.HasMorePages = false;
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
            System.Drawing.Font Headerfont1 = new System.Drawing.Font("Calibri", 15, FontStyle.Bold);
            System.Drawing.Font Headerfont2 = new System.Drawing.Font("Calibri", 10, FontStyle.Regular);
            System.Drawing.Font printFont = new System.Drawing.Font("Calibri", 10);
            System.Drawing.Font printFontBold = new System.Drawing.Font("Calibri", 10, FontStyle.Bold);
            var tabDataForeColor = System.Drawing.Color.Black;
            int height = 100 + y;
            var txtDataWidth = e.Graphics.MeasureString(CompanyName, Headerfont1).Width;
            using (var sf = new StringFormat())
            {
                height += 15;
                sf.LineAlignment = StringAlignment.Center;
                sf.Alignment = StringAlignment.Center;
                xpos = e.MarginBounds.Left + (e.MarginBounds.Width / 2);
                int centerOfPage = e.PageBounds.Width / 2;

                int nameStartPosision = centerOfPage - TextRenderer.MeasureText(CompanyName, Headerfont1).Width / 2;

                e.Graphics.DrawString(CompanyName, Headerfont1, new SolidBrush(tabDataForeColor), nameStartPosision, starty);
                offset = offset + 9;
                e.Graphics.DrawString(Address1 + " " + Address2, Headerfont2, new SolidBrush(tabDataForeColor), headerstartposition, starty + offset);
                offset = offset + 20;
                e.Graphics.DrawString("Phone:".PadRight(3) + Phone, Headerfont2, new SolidBrush(tabDataForeColor), headerstartposition, starty + offset);
                offset = offset + 20;
                e.Graphics.DrawString("Email:".PadRight(3) + Email, Headerfont2, new SolidBrush(tabDataForeColor), headerstartposition, starty + offset);
                offset = offset + 20;
                e.Graphics.DrawString("Website:".PadRight(3) + Website, Headerfont2, new SolidBrush(tabDataForeColor), headerstartposition, starty + offset);
                offset = offset + 20;
                e.Graphics.DrawString("Invoice No: " + VOUCHNUM.Text, Headerfont2, new SolidBrush(tabDataForeColor), 600, starty + offset);
                offset = offset + 16;
                e.Graphics.DrawString("GSTIN:" + TineNo, Headerfont2, new SolidBrush(tabDataForeColor), 600, starty + offset);
                offset = offset + 16;
                e.Graphics.DrawString("Date:" +DOC_DATE_GRE.Value.ToShortDateString(), Headerfont2, new SolidBrush(tabDataForeColor), 600, starty + offset);
                offset = offset + 16;
                offset = offset + 16;
                Pen blackPen = new Pen(System.Drawing.Color.Black, 1);
                Point point1 = new Point(0, 185);
                Point point2 = new Point(900, 185);
                e.Graphics.DrawLine(blackPen, point1, point2);
                e.Graphics.DrawString("To:" + txtSupplierName.Text, Headerfont2, new SolidBrush(tabDataForeColor), startx, starty + offset - 36);
                System.Drawing.Font itemhead = new System.Drawing.Font("Times New Roman", 8);
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
                e.Graphics.DrawString(headtext, printFontBold, new SolidBrush(System.Drawing.Color.Black), startx - 2, starty + offset - 1);
                offset = offset + 40;
                System.Drawing.Font font = new System.Drawing.Font("Times New Roman", 10);
                float fontheight = font.GetHeight();
                int i = 0;
                foreach (DataGridViewRow row in dgItems.Rows)
                {
                    i = i + 1;
                    string name = row.Cells["uName"].Value.ToString().Length <= 60 ? row.Cells[1].Value.ToString() : row.Cells[1].Value.ToString().Substring(0, 60);
                    string tax =Convert.ToDouble( row.Cells[7].Value).ToString(decimalFormat);
                    string qty = row.Cells[5].Value.ToString();
                    //string rate =Convert.ToDouble( row.Cells[6].Value).ToString(decimalFormat);
                   // string price =Convert.ToDouble( row.Cells[11].Value).ToString(decimalFormat);
                    string rate = Convert.ToDouble(row.Cells[6].Value).ToString();
                    string price = Convert.ToDouble(row.Cells[11].Value).ToString();
                    string Serial = row.Cells["SerialNos"].Value.ToString();
                    string productline = name + tax + qty + rate + price;
                    e.Graphics.DrawString(i.ToString(), font, new SolidBrush(System.Drawing.Color.Black), startx, starty + offset);
                    e.Graphics.DrawString(name, font, new SolidBrush(System.Drawing.Color.Black), startx + 30, starty + offset);
                    e.Graphics.DrawString(tax, font, new SolidBrush(System.Drawing.Color.Black), startx + 380, starty + offset);
                    e.Graphics.DrawString(qty, font, new SolidBrush(System.Drawing.Color.Black), startx + 440, starty + offset);
                    e.Graphics.DrawString(rate, font, new SolidBrush(System.Drawing.Color.Black), startx + 525, starty + offset);
                    e.Graphics.DrawString(price, font, new SolidBrush(System.Drawing.Color.Black), startx + 630, starty + offset);
                    offset = offset + (int)fontheight + 10;
                    if (Serial != "")
                    {
                        e.Graphics.DrawString("SN No: " + Serial, font, new SolidBrush(System.Drawing.Color.Black), startx + 30, starty + offset);
                        offset = offset + (int)fontheight + 10;
                    }
                }
            }

         //   e.Graphics.DrawString("E & OE", Headerfont2, new SolidBrush(System.Drawing.Color.Black), startx, 901);
            float newoffset = 900;

            e.Graphics.DrawString(NOTES.Text, Headerfont2, new SolidBrush(System.Drawing.Color.Black), startx, starty + newoffset + 13);

            e.Graphics.DrawString("Gross Total", printFontBold, new SolidBrush(System.Drawing.Color.Black), startx + 490, starty + newoffset + 3);
            e.Graphics.DrawString(Spell.SpellAmount.comma(Convert.ToDecimal(TOTAL_AMOUNT.Text) - Convert.ToDecimal(TOTAL_TAX_AMOUNT.Text)), Headerfont2, new SolidBrush(System.Drawing.Color.Black), startx + 610, starty + newoffset + 3);
            try
            {
                string test = Spell.SpellAmount.InWrods(Convert.ToDecimal(NETT_AMOUNT.Text));

                int index = test.IndexOf("Taka");
                int l = test.Length;
                test = test.Substring(index + 4);

               // e.Graphics.DrawString(test, Headerfont2, new SolidBrush(System.Drawing.Color.Black), startx, starty + newoffset + 3);
            }
            catch
            {
            }


            newoffset = newoffset + 20;
            e.Graphics.DrawString("Tax Amt", printFontBold, new SolidBrush(System.Drawing.Color.Black), startx + 490, starty + newoffset + 3);
            e.Graphics.DrawString(TOTAL_TAX_AMOUNT.Text, Headerfont2, new SolidBrush(System.Drawing.Color.Black), startx + 610, starty + newoffset + 3);

            newoffset = newoffset + 20;
            e.Graphics.DrawString("Discount:", printFontBold, new SolidBrush(System.Drawing.Color.Black), startx + 490, starty + newoffset + 3);
            e.Graphics.DrawString(DISCOUNT.Text, Headerfont2, new SolidBrush(System.Drawing.Color.Black), startx + 610, starty + newoffset + 3);

            newoffset = newoffset + 20;

            //e.Graphics.DrawString("Tax Amount", Headerfont2, new SolidBrush(Color.Black), startx + 490, starty + newoffset + 3);
            //e.Graphics.DrawString(TAX_TOTAL.Text, Headerfont2, new SolidBrush(Color.Black), startx + 610, starty + newoffset + 3);
            e.Graphics.DrawString("---------------------------------------", Headerfont2, new SolidBrush(System.Drawing.Color.Black), startx + 490, starty + newoffset + 9);
            newoffset = newoffset + 25;



            e.Graphics.DrawString("Net Amount:", printFontBold, new SolidBrush(System.Drawing.Color.Black), startx + 490, starty + newoffset + 3);


            e.Graphics.DrawString(Spell.SpellAmount.comma(Convert.ToDecimal(NETT_AMOUNT.Text)), Headerfont2, new SolidBrush(System.Drawing.Color.Black), startx + 610, starty + newoffset + 3);

            newoffset = newoffset + 20;
            e.Graphics.DrawString("Terms & Condtions", Headerfont2, new SolidBrush(System.Drawing.Color.Black), startx, starty + newoffset + 3);
            newoffset = newoffset + 20;
            e.Graphics.DrawString("1)", Headerfont2, new SolidBrush(System.Drawing.Color.Black), startx, starty + newoffset + 3);
            newoffset = newoffset + 20;
            e.Graphics.DrawString("2)", Headerfont2, new SolidBrush(System.Drawing.Color.Black), startx, starty + newoffset + 3);
            newoffset = newoffset + 20;
          //  e.Graphics.DrawString("Cannot provide any warranty covered under the bill is as per                                                           [With Status and Seal]", Headerfont2, new SolidBrush(System.Drawing.Color.Black), startx, starty + newoffset + 3);
            newoffset = newoffset + 20;
           // e.Graphics.DrawString("The waranty terms of the Manufacture", Headerfont2, new SolidBrush(System.Drawing.Color.Black), startx, starty + newoffset + 3);
            newoffset = newoffset + 20;





            e.HasMorePages = false;
        }

        void printDocumentDynamic_PrintPage(object sender, PrintPageEventArgs e)
        {
            DataTable positions = new DataTable();
            positions = ComSet.DYNAMICPOSITIONS();
            bool PRINTTOTALPAGE = true;
            float xpos;
            int offset = 15;
            int w = e.MarginBounds.Width / 2;
            int x = e.MarginBounds.Left;
            int y = e.MarginBounds.Top;
            System.Drawing.Font Headerfont1 = new System.Drawing.Font("Times New Roman", 15, FontStyle.Bold);
            System.Drawing.Font arabicfont = new System.Drawing.Font("Times New Roman", 8);
            System.Drawing.Font Headerfont2 = new System.Drawing.Font("Times New Roman", 10, FontStyle.Bold);
            System.Drawing.Font InvoiceFont = new System.Drawing.Font("Times New Roman", 15, FontStyle.Bold);

            System.Drawing.Font printFont = new System.Drawing.Font("Times New Roman", 10);
            var tabDataForeColor = System.Drawing.Color.Black;
            int height = 100 + y;
            bool hasmorepages = false;

            var txtDataWidth = e.Graphics.MeasureString(CompanyName, Headerfont1).Width;
            xpos = e.MarginBounds.Left + (e.MarginBounds.Width / 2);
            StringFormat format = new StringFormat(StringFormatFlags.DirectionRightToLeft);
            using (var sf = new StringFormat())
            {
                height += 15;
                sf.LineAlignment = StringAlignment.Center;
                sf.Alignment = StringAlignment.Center;
                xpos = e.MarginBounds.Left + (e.MarginBounds.Width / 2);
                offset = offset + 9;
                offset = offset + 15;
                offset = offset + 15;
                offset = offset + 15;
                offset = offset + 15;
                offset = offset + 25;
                e.Graphics.DrawString("" + VOUCHNUM.Text, Headerfont2, new SolidBrush(tabDataForeColor), Convert.ToInt32(positions.Rows[8]["XAXIS"]), Convert.ToInt32(positions.Rows[8]["YAXIS"]));
                offset = offset + 16;
                e.Graphics.DrawString("" + DOC_DATE_GRE.Value.ToShortDateString(), Headerfont2, new SolidBrush(tabDataForeColor), Convert.ToInt32(positions.Rows[1]["XAXIS"]), Convert.ToInt32(positions.Rows[1]["YAXIS"]));
                offset = offset + 16;
                e.Graphics.DrawString("             " + txtSupplierName.Text, Headerfont2, new SolidBrush(tabDataForeColor), Convert.ToInt32(positions.Rows[0]["XAXIS"]), Convert.ToInt32(positions.Rows[0]["YAXIS"]));
                System.Drawing.Font itemhead = new System.Drawing.Font("Times New Roman", 10);
                offset = offset + 2;
                offset = offset + 120;
                System.Drawing.Font font = new System.Drawing.Font("Times New Roman", 10, FontStyle.Bold);
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

                                e.Graphics.DrawString(i.ToString(), font, new SolidBrush(System.Drawing.Color.Black), Convert.ToInt32(positions.Rows[13]["XAXIS"]), INCRIMENTHEIGHT);
                                e.Graphics.DrawString(ItemCode, font, new SolidBrush(System.Drawing.Color.Black), Convert.ToInt32(positions.Rows[2]["XAXIS"]), INCRIMENTHEIGHT);
                                e.Graphics.DrawString(name, font, new SolidBrush(System.Drawing.Color.Black), Convert.ToInt32(positions.Rows[3]["XAXIS"]), INCRIMENTHEIGHT);
                                e.Graphics.DrawString(qty, font, new SolidBrush(System.Drawing.Color.Black), Convert.ToInt32(positions.Rows[4]["XAXIS"]), INCRIMENTHEIGHT);
                                e.Graphics.DrawString(Unit, font, new SolidBrush(System.Drawing.Color.Black), Convert.ToInt32(positions.Rows[12]["XAXIS"]), INCRIMENTHEIGHT);
                                e.Graphics.DrawString(rate, font, new SolidBrush(System.Drawing.Color.Black), Convert.ToInt32(positions.Rows[5]["XAXIS"]), INCRIMENTHEIGHT);
                                e.Graphics.DrawString(price, font, new SolidBrush(System.Drawing.Color.Black), Convert.ToInt32(positions.Rows[7]["XAXIS"]), INCRIMENTHEIGHT);
                                nooflines++;
                                int printpoint = itemlength;
                                while (BALANCELENGH > 1)
                                {
                                    INCRIMENTHEIGHT = INCRIMENTHEIGHT + (int)fontheight + 9;

                                    name2 = BALANCELENGH <= itemlength ? row.Cells["cName"].Value.ToString().Substring(printpoint, BALANCELENGH) : row.Cells["cName"].Value.ToString().Substring(printpoint, itemlength);
                                    e.Graphics.DrawString(name2, font, new SolidBrush(System.Drawing.Color.Black), Convert.ToInt32(positions.Rows[3]["XAXIS"]), INCRIMENTHEIGHT);
                                    BALANCELENGH = BALANCELENGH - itemlength;
                                    printpoint = printpoint + itemlength;
                                    nooflines++;
                                }
                                INCRIMENTHEIGHT = INCRIMENTHEIGHT + (int)fontheight + 9;
                                i++;
                            }
                            else
                            {
                                printeditems = i - 1;
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
            if (PRINTTOTALPAGE)
            {
                if (PAGETOTAL)
                {
                    try
                    {
                        string test = Spell.SpellAmount.InWrods(Convert.ToDecimal(NETT_AMOUNT.Text));

                        int index = test.IndexOf("Taka");
                        int l = test.Length;
                        test = test.Substring(index + 4);
                      //  e.Graphics.DrawString(test, Headerfont2, new SolidBrush(System.Drawing.Color.Black), Convert.ToInt32(positions.Rows[14]["XAXIS"]), Convert.ToInt32(positions.Rows[14]["YAXIS"]));
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
                    string test = Spell.SpellAmount.InWrods(Convert.ToDecimal(NETT_AMOUNT.Text));

                    int index = test.IndexOf("Taka");
                    int l = test.Length;
                    test = test.Substring(index + 4);

                   // e.Graphics.DrawString(test, Headerfont2, new SolidBrush(System.Drawing.Color.Black), Convert.ToInt32(positions.Rows[14]["XAXIS"]), Convert.ToInt32(positions.Rows[14]["YAXIS"]));
                }
                catch
                {
                }
            }
            newoffset = newoffset + 20;
            if (PRINTTOTALPAGE)
            {
                if (PAGETOTAL)
                {
                    e.Graphics.DrawString(TOTAL_AMOUNT.Text, Headerfont2, new SolidBrush(System.Drawing.Color.Black), Convert.ToInt32(positions.Rows[9]["XAXIS"]), Convert.ToInt32(positions.Rows[9]["YAXIS"]));
                    e.Graphics.DrawString(DISCOUNT.Text, Headerfont2, new SolidBrush(System.Drawing.Color.Black), Convert.ToInt32(positions.Rows[10]["XAXIS"]), Convert.ToInt32(positions.Rows[10]["YAXIS"]));
                }
            }
            else
            {
                e.Graphics.DrawString(TOTAL_AMOUNT.Text, Headerfont2, new SolidBrush(System.Drawing.Color.Black), Convert.ToInt32(positions.Rows[9]["XAXIS"]), Convert.ToInt32(positions.Rows[9]["YAXIS"]));
                e.Graphics.DrawString(DISCOUNT.Text, Headerfont2, new SolidBrush(System.Drawing.Color.Black), Convert.ToInt32(positions.Rows[10]["XAXIS"]), Convert.ToInt32(positions.Rows[10]["YAXIS"]));
            }

            newoffset = newoffset + 20;
            offset = offset + 25;
            if (PRINTTOTALPAGE)
            {
                if (PAGETOTAL)
                {

                    e.Graphics.DrawString(Spell.SpellAmount.comma(Convert.ToDecimal(NETT_AMOUNT.Text)), Headerfont2, new SolidBrush(System.Drawing.Color.Black), Convert.ToInt32(positions.Rows[11]["XAXIS"]), Convert.ToInt32(positions.Rows[11]["YAXIS"]));
                }
            }
            else
            {
                e.Graphics.DrawString(Spell.SpellAmount.comma(Convert.ToDecimal(NETT_AMOUNT.Text)), Headerfont2, new SolidBrush(System.Drawing.Color.Black), Convert.ToInt32(positions.Rows[11]["XAXIS"]), Convert.ToInt32(positions.Rows[11]["YAXIS"]));
            }
            newoffset = newoffset + 20;
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

        private void kryptonLabel16_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtMRP_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvPriceList.Rows.Count; i++)
            {
                if (dgvPriceList.Rows.Count > 0)
                {
                    if (dgvPriceList.Rows[i].Cells[0].Value.ToString() == "MRP")
                    {
                        dgvPriceList.Rows[i].Cells[2].Value = txtMRP.Text;
                        break;
                    }                 
                }

            }
        }

        private void tb_Wholesale_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvPriceList.Rows.Count; i++)
            {
                if (dgvPriceList.Rows.Count > 0)
                {
                    if (dgvPriceList.Rows[i].Cells[0].Value.ToString() == "WHL")
                    {
                        dgvPriceList.Rows[i].Cells[2].Value = tb_Wholesale.Text;
                        break;
                    }
                }
            }
        }

        private void dgvPriceList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dgvPriceList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
            conn.Open();
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                if (DialogResult.Yes == MessageBox.Show("Do you want to update " + senderGrid.CurrentRow.Cells["colSaleType"].Value.ToString() + " Price to " + senderGrid.CurrentRow.Cells["colAmount"].Value.ToString() + "?", "Price list update", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    string updatequery = "";
                    string selectquery = "";
                    string ItemCode = ITEM_CODE.Text;
                    string UnitCode = UOM.SelectedValue.ToString();
                    string salType = dgvPriceList.CurrentRow.Cells["colSaleType"].Value.ToString();
                    decimal Price = Convert.ToDecimal(dgvPriceList.CurrentRow.Cells["colAmount"].Value);
                    updatequery = updatequery + "UPDATE INV_ITEM_PRICE SET  PRICE = '" + Price + "' WHERE (SAL_TYPE = '" + salType + "') AND (ITEM_CODE = '" + ItemCode + "') AND (UNIT_CODE = '" + UnitCode + "');";                                        
                    cmd.CommandText = updatequery;
                    cmd.ExecuteNonQuery();
                    selectquery = "SELECT DefaultRateType FROM SYS_SETUP";
                    cmd.CommandText = selectquery;
                    string defaultRateType = Convert.ToString(cmd.ExecuteScalar());
                    conn.Close();
                    if (dgvPriceList.CurrentRow.Cells["colSaleType"].Value.ToString() == "MRP")
                    {
                        txtMRP.Text = dgvPriceList.CurrentRow.Cells["colAmount"].Value.ToString();
                    }
                    else if (dgvPriceList.CurrentRow.Cells["colSaleType"].Value.ToString() == defaultRateType)
                    {
                        RTL_PRICE.Text = dgvPriceList.CurrentRow.Cells["colAmount"].Value.ToString();
                    }
                    else if (dgvPriceList.CurrentRow.Cells["colSaleType"].Value.ToString() == "WHL")
                    {
                        tb_Wholesale.Text = dgvPriceList.CurrentRow.Cells["colAmount"].Value.ToString();
                    }
                }
            }
        }

        private void dgvPriceList_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
           
        }

        private void dgvPriceList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {        
            if (!string.IsNullOrEmpty(dgvPriceList.CurrentRow.Cells[1].Value.ToString()))
            {
                double value = ((double.Parse(dgvPriceList.CurrentRow.Cells[1].Value.ToString()) * double.Parse(PRICE_FOB.Text)) / 100) + double.Parse(dgvPriceList.CurrentRow.Cells[2].Value.ToString());
                dgvPriceList.CurrentRow.Cells[2].Value = value.ToString("0.00");
            }
        }

        private void dgvPriceList_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
           
        }

        private void dgvPriceList_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dgvPriceList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                dgvPriceList.Visible = false;
            }
        }

        private void RTL_PRICE_TextChanged(object sender, EventArgs e)
        {
            //for (int i = 0; i < dgvPriceList.Rows.Count; i++)
            //{
            //    if (dgvPriceList.Rows.Count > 0)
            //    {
            //        if (dgvPriceList.Rows[i].Cells[0].Value.ToString() == "RTL")
            //        {
            //            dgvPriceList.Rows[i].Cells[2].Value = RTL_PRICE.Text;
            //            break;
            //        }
            //    }

            //}

        }

        private void dataGridItem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgSubPrices_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgSubPrices_KeyDown(object sender, KeyEventArgs e)
        {
            int count = dgSubPrices.Rows.Count-1;
            int ccount = dgSubPrices.CurrentRow.Index;
            if (e.KeyCode == Keys.Enter)
            {
                if (count == ccount)
                {
                    dgSubPrices.Visible = false;
                    ITEM_GROSS.Focus();
                }
            }


        }

        private void ITEM_GROSS_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void chkInlusiveTax_CheckedChanged(object sender, EventArgs e)
        {
            if (chkInlusiveTax.Checked == true)
            {
                hasPurExclusive = false;

            }
            else
            {
                hasPurExclusive = true;
            }
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
                    taxRate = Convert.ToDouble(c["uTaxPercent"].Value);
                }
                catch { }
                double taxAmount = 0;
                try
                {
                    taxAmount = Convert.ToDouble(c["uTaxAmt"].Value);
                }
                catch { }

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

        private void dgItems_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            calculateGSTTaxes();
            serialNo();
        }

        private void dgItems_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            calculateGSTTaxes();
        }

        private void dgvGSTTaxes_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            growDgvGSTTaxes();
        }

        private void dgvGSTTaxes_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            growDgvGSTTaxes();
        }

        private void SUPPLIER_INV_TextChanged(object sender, EventArgs e)
        {

        }

        private void NETT_AMOUNT_TextChanged(object sender, EventArgs e)
        {

        }

        private void panItem_Paint(object sender, PaintEventArgs e)
        {

        }


        void printBill()
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
                if (printinvoice.Checked)
                {
                    printDocument.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("smallzize", 360, height + 170);
                    printDocument.PrintPage += printDocument_PrintPage;
                    printdialog.Document = printDocument;
                   
                        if (printdialog.ShowDialog() == DialogResult.OK)
                        {
                            printDocument.Print();
                        }
                   
                }
            }

                //medium Size
            else if (PrintPage.SelectedIndex == 1)
            {
                if (printinvoice.Checked)
                {
                    printDocument.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("MediumSize", 914, height + 450);

                    printDocument.PrintPage += printDocumentMediumSize_PrintPage;
                    printdialog.Document = printDocument;
                   
                        if (printdialog.ShowDialog() == DialogResult.OK)
                        {
                            printDocument.Print();
                        }
                   

                }
            }
            else if (PrintPage.SelectedIndex == 2)
            {
                if (printinvoice.Checked)
                {

                    PaperSize ps = new PaperSize();
                    ps.RawKind = (int)PaperKind.A4;

                    printDocument.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("A4", 840, 1188);
                    printDocument.PrintPage += printDocumentA4_PrintPage;
                    //printDocument.PrintPage += printDocumentA4_PrintPage;
                    printdialog.Document = printDocument;
                     if (printdialog.ShowDialog() == DialogResult.OK)
                        {
                            printDocument.Print();
                        }
                    
                }

            }
        }

        private void kryptonButton5_Click(object sender, EventArgs e)
        {
            printBill();
        }

        private void PRICE_FOB_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void cmbInvType_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetMaxDocID();
            SetLabelPrefix();
            Clear2();
            checkvoucher(Convert.ToInt32(VOUCHNUM.Text));
        }

        void SetLabelPrefix()
        {
            if (cmbInvType.SelectedValue != null)
            {
                if (cmbInvType.SelectedValue.ToString() == "GST")
                {
                    lbl_prefix.Text = "GST";
                }
                else if (cmbInvType.SelectedValue.ToString() == "EST")
                {
                    lbl_prefix.Text = "EST";
                }
                else
                {
                    lbl_prefix.Text = "--";
                }
            }
            else
            {
                lbl_prefix.Text = "--";
            }
        }

        private void btn_inv_browse_Click(object sender, EventArgs e)
        {            
        }

        private void VOUCHNUM_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void kryptonLabel39_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void chkDebit_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDebit.Checked)
            {
                GetLedgers();
                drpdebitor.SelectedValue = 21;
            }
        }

        private void chkCredit_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCredit.Checked)
            {
                if (SUPPLIER_CODE.Text == "")
                    drpdebitor.Text = "";
                else
                    GetLedgerId(SUPPLIER_CODE.Text);

            }
        }
        public void GETLEDGERDETAILS()
        {
            invHdr.SupplierCode = SUPPLIER_CODE.Text;
            invHdr.DocNo = ID;

            SqlDataReader r = invHdr.getSupLedId();
            while (r.Read())
            {
                txtSupplierName.Text = Convert.ToString(r[0]);
                drpdebitor.SelectedValue = r[1];

            }
            DbFunctions.CloseConnection();

          
            string PurchaseType = invHdr.getDocType();
            if (PurchaseType == "PUR.CSS")
            {
                chkDebit.Checked = true;
                drpdebitor.SelectedValue = 21;
            }
            else
            {
                chkCredit.Checked = true;
            }

           
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