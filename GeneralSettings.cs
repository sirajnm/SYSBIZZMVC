using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.SqlServer.Server;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management;
using Sys_Sols_Inventory.Model;
using Sys_Sols_Inventory.Class;

namespace Sys_Sols_Inventory
{
    public partial class GeneralSettings : Form
    {
        Class.DateSettings datset = new Class.DateSettings();
        Class.CompanySetup cset = new Class.CompanySetup();
        BindingList<GenDocSerial> seriallist = new BindingList<GenDocSerial>();
        
        //private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        //private SqlCommand query = );
        //private SqlDataAdapter adapter = new SqlDataAdapter();
        Initial mdi = (Initial)Application.OpenForms["Initial"];
        Login lg = (Login)Application.OpenForms["Login"];
        Class.cls_backup bk = new Class.cls_backup();
        InputLanguage arabic;
        InputLanguage english;
        int indexofenglish;
        public string print = "";
        private string CompanyId;
        private string BranchId = "";
        private string Logo;
        public bool HasArabic = true;
        private string path = "";
        string comName;
        public bool HasType = true, HasCategory = true, HasGroup = true, HasTM = true, HasAccessLimit;
        private int selectedRow = -1;
        DataTable dtInv = new DataTable();
        DataTable dt1Inv = new DataTable();
        DataTable dtVouch = new DataTable();
        DataTable dt1Vouch = new DataTable();

        string query = "";

        public GeneralSettings()
        {

            InitializeComponent();

        }

        public void BindRateType()
        {
            try
            {
                //    conn.Open();
                DataTable dt = new DataTable();
                //conn.Open();
                //query.Connection = conn;
                //adapter.SelectCommand = query;
                //query.CommandType = CommandType.Text;
                query = "SELECT CODE AS [key],CODE+' - '+DESC_ENG AS value FROM GEN_PRICE_TYPE";
                //adapter.Fill(dt);
                dt = DbFunctions.GetDataTable(query);
                cmbRateType.DisplayMember = "value";
                cmbRateType.ValueMember = "key";
                cmbRateType.DataSource = dt;
                //conn.Close();

            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        public void getdec()
        {
            ComboDeci.DisplayMember = "Text";
            ComboDeci.ValueMember = "Value";

            ComboDeci.Items.Add(new { Text = "0.0", Value = "1" });
            ComboDeci.Items.Add(new { Text = "0.00", Value = "2" });
            ComboDeci.Items.Add(new { Text = "0.000", Value = "3" });
            ComboDeci.Items.Add(new { Text = "0.0000", Value = "4" });

        }

        void SavePurchaseType()
        {
            try
            {
                //query.Connection = conn;
                //conn.Open();
                query = "UPDATE SYS_SETUP SET PUR_TYPE='" + cmbVoucherType.SelectedValue.ToString() + "'";
                DbFunctions.InsertUpdate(query);
                //query.CommandType = CommandType.Text;
                //query.Connection = conn;
                //Dbfunctions.InsertUpdate(query);
                //conn.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }
        DataTable GetType()
        {
            DataTable dt = new DataTable();
            try
            {
                //conn.Open();
                //query.Connection = conn;
                query = "SELECT CODE,DESC_ENG FROM GEN_PUR_TYPE";
                //query.CommandType = CommandType.Text;
                //query.Connection = conn;
                //SqlDataAdapter ad = new SqlDataAdapter(query);
                //ad.Fill(dt);
                //conn.Close();
                dt = DbFunctions.GetDataTable(query);
                DataRow row = dt.NewRow();
                row[1] = "<<-SELECT TYPE->>";
                dt.Rows.InsertAt(row, 0);
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
             return dt;
        }
        public void GetPurType()
        {
            try
            {
               
                DataTable DT = new DataTable();
                DT = GetType();
                cmbVoucherType.DataSource = DT;
                cmbVoucherType.ValueMember = "CODE";
                cmbVoucherType.DisplayMember = "DESC_ENG";

                DataTable dt1 = new DataTable();
                dt1 = GetType();
                cmbStartVoucher.DataSource = dt1;
                cmbStartVoucher.ValueMember = "CODE";
                cmbStartVoucher.DisplayMember = "DESC_ENG";
            }
            catch
            {

            }
        }

        void SetPurType()
        {
            try
            {
                //query.Connection = conn;
                //conn.Open();
                query = "SELECT TOP 1 PUR_TYPE FROM SYS_SETUP";
                //query.CommandType = CommandType.Text;
                //query.Connection = conn;
                cmbVoucherType.SelectedValue = Convert.ToString(DbFunctions.GetAValue(query));
                //conn.Close();
            }
            catch
            {

            }
        }
        void SaveStartVoucher()
        {
            //query.Connection = conn;
            //conn.Open();
            query = "UPDATE GEN_PUR_STARTFROM SET VoucherStart="+Convert.ToInt32(txtVoucherStart.Text)+" WHERE VoucherTypeCode='"+cmbStartVoucher.SelectedValue.ToString()+"'";
            DbFunctions.InsertUpdate(query);
            //query.CommandType = CommandType.Text;
            //query.Connection = conn;
            //Dbfunctions.InsertUpdate(query);
            //conn.Close();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.S))
            {
                //btnSave.PerformClick();
                //return true;
            }
            if (keyData == (Keys.Escape))
            {
                //   this.Close();
                //ComponentFactory.Krypton.Docking.KryptonDockableNavigator n = mdi.sender as ComponentFactory.Krypton.Docking.KryptonDockableNavigator;
                if (CompanyTab.SelectedIndex != 0)
                {
                    CompanyTab.SelectedIndex = 0;
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
        public void GetTaxRates()
        {
            //conn.Open();
            //query.Connection = conn;
            //adapter.SelectCommand = query;
            query = "SELECT        TaxId, CODE + ' --- ' +CONVERT(varchar(10),TaxRate)+' %' AS Expr1 FROM            GEN_TAX_MASTER";
            DataTable dt = new DataTable();
            //adapter.Fill(dt);
            dt = DbFunctions.GetDataTable(query);
            cmbTaxType.DataSource = dt;
            cmbTaxType.DisplayMember = "Expr1";
            cmbTaxType.ValueMember = "TaxId";
            cmbTaxType.SelectedIndex = -1;
            //conn.Close();
        }
  
        private void GeneralSettings_Load(object sender, EventArgs e)
        {
            try
            {
                query = "SELECT Production_Tax FROM SYS_SETUP";
                if (Convert.ToBoolean(DbFunctions.GetAValue(query)))
                    ptax.Checked = true;
                else
                    ptaxno.Checked = true;
            }
            catch
            {
 
            }

            string query2 = "Select * from GEN_DOC_SERIAL";
            try
            {
                DataTable noseriestable = DbFunctions.GetDataTable(query2);
                foreach(DataRow dr in noseriestable.Rows)
                {
                    Class.GenDocSerial docserial = new Class.GenDocSerial();
                    docserial.id = Convert.ToInt16(dr["id"]);
                    docserial.BRANCH_CODE = dr["BRANCH_CODE"].ToString();
                    docserial.STORE_CODE = dr["STORE_CODE"].ToString();
                    docserial.DOC_TYPE = dr["DOC_TYPE"].ToString();
                    docserial.PRIFIX = dr["PRIFIX"].ToString();
                    docserial.SUFIX = dr["SUFIX"].ToString();
                    docserial.NEED_ZERO_BEFORE = Convert.ToInt16(dr["NEED_ZERO_BEFORE"]);
                    docserial.SERIAL_LENGTH = Convert.ToInt16("SERIAL_LENGTH");
                    docserial.MODULES_CODE = dr["MODULES_CODE"].ToString();
                    docserial.AUTO_NUM = dr["AUTO_NUM"].ToString();
                    seriallist.Add(docserial);

                }

                if (noseriestable.Rows.Count > 0)
                {
                    NumberSeriesGrid.AutoGenerateColumns = true;
                    NumberSeriesGrid.DataSource = seriallist;
                    NumberSeriesGrid.Refresh();
                    this.Refresh();

                    
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

            
            
            //dgInv.Rows[0].Visible = false;
            HasArabic = General.IsEnabled(Settings.Arabic);
            HasType = General.IsEnabled(Settings.HasType);
            HasCategory = General.IsEnabled(Settings.HasCategory);
            HasGroup = General.IsEnabled(Settings.HasGroup);
            HasTM = General.IsEnabled(Settings.HasTM);
            string statelbl=General.GetStatelabelText();
            lblState.Text = (!statelbl.Equals("")) ? "Current "+statelbl.First().ToString().ToUpper() + statelbl.Substring(1).ToLower()+":" : lblState.Text;
            BindRateType();
            cmbSaleType.DataSource = Common.getSaleTypes();
            cmbSaleType.DisplayMember = "value";
            cmbSaleType.ValueMember = "key";
            cmbVouch.DataSource = Common.getVouchTypes();
            cmbVouch.DisplayMember = "value";
            cmbVouch.ValueMember = "key";
            comName = txt_cname.Text;

            GetTaxRates();
            bindbranch();
            binddataCompany();

            bindcurrency();
            BindToDataGridbranch();
            GetFinancialYear();
            radioSpareNo.Checked = true;
            radioNo.Checked = true;
            radioAppNo.Checked = true;
            GetGeneralDetails();
            if (!HasArabic)
                txt_arabbranch.Enabled = false;
            getaccesslimit();
            //GetMaxDocID();
            getdec();

            GetBranchDetails();
            GetDecimalSetting();
            saleType();
            get_print();
            GetVouchStart();
            GetInvoiceStartFrom();
            if (CHECK_CREDIT_PERIOD.Checked == false)
            {
                txt_credit_period.Text = null;
            }

            arabic = InputLanguage.CurrentInputLanguage;
            english = InputLanguage.CurrentInputLanguage;
            int count = InputLanguage.InstalledInputLanguages.Count;
            for (int i = 0; i <= count - 1; i++)
            {
                if (InputLanguage.InstalledInputLanguages[i].LayoutName.Contains("Arabic"))
                {
                    arabic = InputLanguage.InstalledInputLanguages[i];
                }
                if (InputLanguage.InstalledInputLanguages[i].LayoutName.Contains("English"))
                {
                    english = InputLanguage.InstalledInputLanguages[i];
                    indexofenglish = i;
                }
            }

            loadStates();
            load_CusAsSup();
            GetPurType();
            SetPurType();
            loadTemplate();
            DPyes.Checked = loadTempSettings();
            string taxType = Properties.Settings.Default.Tax_Type;
            if (!(taxType.ToUpper() == "VAT"))
            {
                cbx_Invoice.Items.Remove("VAT");
            }
            if (!(taxType.ToUpper()=="THERMAL VAT"))
            {
                cbx_Invoice.Items.Remove("Thermal VAT");
            }
        }
        void load_CusAsSup()
        {
            bool value = false;
            try
            {
                //conn.Open();
                //query.CommandType = CommandType.Text;
                query = "SELECT  IsCusSup  FROM   SYS_SETUP";
                value = Convert.ToBoolean(DbFunctions.GetAValue(query));
                //conn.Close();
            }
            catch
            {
                //if (conn.State == ConnectionState.Open)
                //    conn.Close();
            }
            if (value)
                RadCusandSupYes.Checked = true;
            else
                RadCusandSupNo.Checked = true;
        }
        private void loadStates()
        {
            DataTable table = Common.loadStates();
            DataRow row = table.NewRow();
            row[1] = "-----SELECT STATE-----";
            table.Rows.InsertAt(row, 0);
            cmbState.DataSource = table;
            cmbState.DisplayMember = "NAME";
            cmbState.ValueMember = "CODE";
            if (lg.state != null && !lg.state.Equals(""))
            {
                cmbState.SelectedValue = lg.state;
            }
            else
            {
                cmbState.SelectedIndex = 0;
            }
        }

        string p = "";
        string d = "";
        public void get_print()
        {
            //DataTable DT = new DataTable();
            //query = "select * from SYS_SETUP";
            //SqlDataAdapter SDA = new SqlDataAdapter(query);
            //SDA.Fill(DT);
            //if(DT.Rows.Count>0)
            //{
            //    p=DT.Rows[0]["Inv_print"].ToString();
            //}
            //if (p == "True")
            //{
            //    check_print.Checked = true;
            //}
            //else
            //{
            //    check_print.Checked = false;
            //}
        }


        public void GetDecimalSetting()
        {
            string result = "";
            //conn.Open();
            //query.CommandType = CommandType.Text;
            query = "SELECT  Dec_qty  FROM   SYS_SETUP";
            result = Convert.ToString(DbFunctions.GetAValue(query));
            switch (result)
            {
                case "2":
                    ComboDeci.SelectedIndex = 1;
                    break;
                case "3":
                    ComboDeci.SelectedIndex = 2;
                    break;
                case "4":
                    ComboDeci.SelectedIndex = 3;
                    break;
                case "1":
                case "":
                default:
                    ComboDeci.SelectedIndex = 0;
                    break;
            }
            //conn.Close();
        }
        void GetInvoiceStartFrom()
        {
            bool check = false;
            DataTable dt = new DataTable();
            query = "SELECT * FROM GEN_INV_STARTFROM";
            dt = DbFunctions.GetDataTable(query);
            //SqlDataAdapter adapter = new SqlDataAdapter(query);
            //adapter.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = i + 1; j < dt.Rows.Count; j++)
                {
                    if (dt.Rows[i][1].ToString() == dt.Rows[j][1].ToString())
                    {
                        check = true;
                    }
                    else
                    {
                        check = false;
                        break;
                    }
                    
                }
                if (check == false)
                {
                    break;
                }
                else
                {
                    continue;
                }
            }
            if (check == false)
            {
                cmbInvType.SelectedIndex = 1;
            }
            else
            {
                txtInvNo.Text = dt.Rows[0][1].ToString();
            }
            
        }

        public void GetBranchDetails()
        {
            DataTable dt = new DataTable();
            dt = cset.GetCurrentBranchDetails();
            if (dt.Rows.Count > 0)
            {

                Drp_Currency.SelectedValue = dt.Rows[0]["DEFAULT_CURRENCY_CODE"].ToString();
            }
        }
        //public void GetMaxDocID()
        //{
        //    conn.Open();
        //    adapter.SelectCommand = query;
        //    query = "SELECT MAX(DOC_ID) FROM INV_SALES_HDR";
        //    query.CommandType = CommandType.Text;
        //    String value = Convert.ToString(Dbfunctions.GetAValue(query));
        //    if (value.Equals(""))
        //    {
        //        VOUCHNUM.Text = "1";
        //    }
        //    else
        //    {
        //        VOUCHNUM.Text = Convert.ToString(Convert.ToInt32(value) + 1);
        //    }
        //    conn.Close();
        //}

        public void bindbranch()
        {
            DataTable branchtbl = new DataTable();
            branchtbl = cset.selectallbranch();
            cbxBranch.DataSource = branchtbl;
            cbxBranch.DisplayMember = "Description";
            cbxBranch.ValueMember = "Code";
        }


        public void GetFinancialYear()
        {
            DataTable dt = new DataTable();
            cset.Status = true;
            dt = cset.GetCurrentFinancialYear();

            dtpFrom.Value = Convert.ToDateTime(dt.Rows[0]["SDate"]);
            dtpUpdateFrom.Value = Convert.ToDateTime(dt.Rows[0]["SDate"]);
            dtpTo.Value = Convert.ToDateTime(dt.Rows[0]["EDate"]);
            dtpUpdateTo.Value = Convert.ToDateTime(dt.Rows[0]["EDate"]);
        }


        public void binddataCompany()
        {
            DataTable dt = new DataTable();
            dt = cset.getCompanyDetails();
            if (dt.Rows.Count > 0)
            {
                CompanyId = dt.Rows[0]["Id"].ToString();
                txt_cname.Text = dt.Rows[0]["Company_Name"].ToString();
                txt_otherdetails.Text = dt.Rows[0]["Other_details"].ToString();
                txt_website.Text = dt.Rows[0]["WebSite"].ToString();
                txt_pan.Text = dt.Rows[0]["PAN_No"].ToString();
                txt_tin.Text = dt.Rows[0]["TIN_No"].ToString();
                txt_cst.Text = dt.Rows[0]["CST_No"].ToString();
                txt_accno.Text = dt.Rows[0]["ACC_NO"].ToString();
                txt_ifsc.Text = dt.Rows[0]["IFSC"].ToString();
                ARBNAME.Text = dt.Rows[0]["ARBCompany_Name"].ToString();
                if (dt.Rows[0]["Logo"].ToString() != "")
                {
                    path = dt.Rows[0]["Logo"].ToString();
                    Image im = GetCopyImage(path);
                    pictureBox1.Image = im;
                    // pictureBox1.Image = Image.FromFile(dt.Rows[0]["Logo"].ToString());

                }
            }

            query = "SELECT COUNT(*) FROM GEN_DOC_SERIAL";
            //if(conn.State!=ConnectionState.Open)
            //conn.Open();
            //query.Connection = conn;
            int row = Convert.ToInt32(DbFunctions.GetAValue(query));
            if (row != 0)
            {
                query = "SELECT ISNULL(PRIFIX,'') FROM GEN_DOC_SERIAL";
                txt_b2b.Text = DbFunctions.GetAValue(query).ToString();
                query = "SELECT ISNULL(SUFIX,'') FROM GEN_DOC_SERIAL";
                txt_b2c.Text = DbFunctions.GetAValue(query).ToString();
            }
            //conn.Close();
        }

        public void getaccesslimit()
        {
            HasAccessLimit = General.IsEnabled(Settings.HasAccessLimit);
            if (HasAccessLimit)
            {
                rbtnlimitYes.Checked = true;
                paneldatelimit.Enabled = true;
                getdateaccessdetails();
            }
            else
            {
                rbtnlimitNo.Checked = true;
                paneldatelimit.Enabled = false;
            }
        }


        public void getdateaccessdetails()
        {
            DataTable dt = new DataTable();
            dt = datset.getdatdetails();
            paneldatelimit.Enabled = true;

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0][3].ToString() == "Date")
                {
                    drptype.SelectedIndex = 0;
                    panelperiod.Visible = false;
                    DOC_DATE_GRE.Value = Convert.ToDateTime(dt.Rows[0][1].ToString());

                }
                else if (dt.Rows[0][3].ToString() == "Period")
                {
                    drptype.SelectedIndex = 1;
                    panelDate1.Visible = false;
                    switch (dt.Rows[0][4].ToString())
                    {
                        case "M":
                            Month.Checked = true;
                            break;
                        case "D":
                            Day.Checked = true;
                            break;
                        case "Y":
                            year.Checked = true;
                            break;

                    }

                    txt_Duration.Text = dt.Rows[0][2].ToString();
                }
                else
                {
                    panelperiod.Visible = false;
                    panelDate1.Visible = false;
                }


            }

        }
        public void bindcurrency()
        {
            DataTable tableCurrency = new DataTable();
            tableCurrency = cset.getcurrency();
            Drp_Currency.DataSource = tableCurrency;
            Drp_Currency.DisplayMember = "DESC_ENG";
            Drp_Currency.ValueMember = "CODE";
        }

        private Image GetCopyImage(string path)
        {
            Bitmap bm = null;
            try
            {
                using (Image im = Image.FromFile(path))
                {

                    bm = new Bitmap(im);
                }
            }
            catch (Exception e)
            {
                //TODO: THROW ERROR IF LOGO NOT FOUND IF YOU WANT TO.
            }
            return bm;
        }

        private void btnSaveCompany_Click(object sender, EventArgs e)
        {
            if (validCompanyDetails())
            {
                cset.Id = Convert.ToInt16(CompanyId);
                cset.Company_Name = txt_cname.Text;
                cset.Other_Details = txt_otherdetails.Text;
                cset.PAN_No = txt_pan.Text;
                cset.CST_No = txt_cst.Text;
                cset.TIN_No = txt_tin.Text;
                cset.Acc_No = txt_accno.Text;
                cset.ifsc = txt_ifsc.Text;
                cset.WebSite = txt_website.Text;
                cset.Logo = pathdirectory;
                cset.DESC_ARB = ARBNAME.Text;
                cset.updateCompanydetils();
                cset.com_Name = comName;
                cset.SysSetup_UpdateCompanyName();
                cset.EDate = dtpTo.Value;
                cset.SDate = dtpFrom.Value;
                cset.B2B = txt_b2b.Text;
                cset.B2C = txt_b2c.Text;
                cset.BranchCode = lg.Branch;
                cset.Status = true;
                cset.UpdateFinancialYear();
                try
                {
                    cset.updateBank();
                    cset.Prefix_For_Invoice();
                }
                catch
                {
                    MessageBox.Show("Please contact the software vendor to Update Database", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                MessageBox.Show("Updated.");                
                binddataCompany();
                
            }
        }


        private bool validbranch()
        {
            if (txt_brnchCode.Text.Trim() != "" && txt_branch.Text != "")
            {
                return true;
            }
            else
            {
                MessageBox.Show("Please enter the following details \n 1 branch Name\n 2 branch code");
                return false;
            }
        }

        private bool validCompanyDetails()
        {
            if (txt_cname.Text.Trim() != "")
            {
                return true;
            }
            else
            {
                MessageBox.Show("Please enter company name");
                return false;
            }
        }
        string pathdirectory = "";
        private void BtnBrowse_Click(object sender, EventArgs e)
        {
           
            OpenFileDialog open = new OpenFileDialog();

            // image filters
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp;*.png;)|*.jpg; *.jpeg; *.gif; *.bmp;*.png;";
            if (open.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(open.FileName);
                pathdirectory = open.FileName;
            }
           /* // open file dialog 
            OpenFileDialog open = new OpenFileDialog();
            string appPath = Path.GetDirectoryName(Application.StartupPath) + @"\logo\";
            if (Directory.Exists(appPath) == false)
            {
                Directory.CreateDirectory(appPath);
            }
            // image filters
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp;*.png;)|*.jpg; *.jpeg; *.gif; *.bmp;*.png;";
            if (open.ShowDialog() == DialogResult.OK)
            {

                try
                {
                    string iName = open.SafeFileName;
                    string filepath = open.FileName;
                    if (path != "")
                    {
                        File.Delete(path);
                    }

                    File.Copy(filepath, appPath + iName);
                    path = appPath + iName;
                    // display image in picture box
                    pictureBox1.Image = new Bitmap(open.FileName);
                    // image file path
                    // textBox1.Text = open.FileName;
                }
                catch { }

            }*/
        }

        private void btnremove_Click(object sender, EventArgs e)
        {
            if (path != null)
            {

                pictureBox1.Image = null;
                File.Delete(path);
                
                path = "";
                pathdirectory = "";
            }
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            if (validbranch())
            {
                cset.CODE = txt_brnchCode.Text;
                cset.DESC_ENG = txt_branch.Text;
                cset.DESC_ARB = txt_arabbranch.Text;
                cset.ARBADDRESS_1 = ARBADRESS1.Text;
                cset.ARBADDRESS_2 = ARBADRESS2.Text;

                var checkedButton = panelappliance.Controls.OfType<RadioButton>()
                                          .FirstOrDefault(r => r.Checked);
                switch (checkedButton.Text)
                {
                    case "Yes":
                        cset.ALLOW_APPLIANCES = 1;
                        break;
                    case "No":
                        cset.ALLOW_APPLIANCES = 0;
                        break;
                    default:
                        cset.ALLOW_APPLIANCES = 0;
                        break;
                }

                var checkedsp = panelspare.Controls.OfType<RadioButton>()
                                          .FirstOrDefault(r => r.Checked);
                switch (checkedsp.Text)
                {
                    case "Yes":
                        cset.ALLOW_SPARES = 1;
                        break;
                    case "No":
                        cset.ALLOW_SPARES = 0;
                        break;
                    default:
                        cset.ALLOW_SPARES = 0;
                        break;
                }

                var checkedser = panelService.Controls.OfType<RadioButton>()
                                          .FirstOrDefault(r => r.Checked);
                switch (checkedser.Text)
                {
                    case "Yes":
                        cset.ALLOW_SERVICES = 1;
                        break;
                    case "No":
                        cset.ALLOW_SERVICES = 0;
                        break;
                    default:
                        cset.ALLOW_SERVICES = 0;
                        break;
                }
                cset.ADDRESS_1 = txt_addr1.Text;
                cset.ADDRESS_2 = txt_addr2.Text;
                cset.TELE_1 = txt_phone.Text;
                cset.Email = txt_mail.Text;
                cset.Fax = txt_fax.Text;
                cset.DEFAULT_CURRENCY_CODE = Drp_Currency.SelectedValue.ToString();
                if (BranchId != "")
                {
                    cset.BranchId = BranchId;
                    cset.UpdateBranchDetails();
                }
                else
                    cset.InsertBranch();
                BindToDataGridbranch();
                btnclear.PerformClick();
            }
        }

        private void BindToDataGridbranch()
        {

            DataTable dat = new DataTable();
            dat = cset.getbaranchdetails();
            grdBranch.DataSource = dat;
            if (!HasArabic)
                grdBranch.Columns[2].Visible = false;

        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (BranchId != "")
            {
                if (MessageBox.Show("Are you sure?", "Record Deletion", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    cset.CODE = BranchId;
                    cset.deletebranch();
                    BindToDataGridbranch();
                    btnclear.PerformClick();
                }


            }
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            BranchId = "";
            txt_branch.Text = "";
            txt_brnchCode.Text = "";
            txt_arabbranch.Text = "";
            txt_addr1.Text = "";
            txt_addr2.Text = "";
            txt_phone.Text = "";
            txt_mail.Text = "";
            txt_fax.Text = "";
            radioAppNo.Checked = true;
            radioAppYes.Checked = false;
            radioNo.Checked = true;
            radioYes.Checked = false;
            radioSpareYes.Checked = false;
            radioSpareNo.Checked = true;
        }


        public void GetGeneralDetails()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = cset.getGeneralSetup();
                if (dt.Rows.Count > 0)
                {
                    switch (dt.Rows[0][0].ToString())
                    {
                        case "True":
                            RbtbPosYes.Checked = true;
                            break;
                        case "False":
                            RbtnPosNo.Checked = true;
                            break;
                        default:
                            RbtnPosNo.Checked = true;
                            break;

                    }
                    switch (dt.Rows[0][1].ToString())
                    {
                        case "True":
                            RbtnArabicYes.Checked = true;
                            break;
                        case "False":
                            RbtnArabicNo.Checked = true;
                            break;
                        default:
                            RbtnArabicNo.Checked = true;
                            break;

                    }
                    switch (dt.Rows[0][2].ToString())
                    {
                        case "True":
                            RbtnTaxYes.Checked = true;
                            break;
                        case "False":
                            RbtnTaxNo.Checked = true;
                            break;
                        default:
                            RbtnTaxNo.Checked = true;
                            break;

                    }
                    switch (dt.Rows[0][3].ToString())
                    {
                        case "True":
                            rbtnBarcodeYes.Checked = true;
                            break;
                        case "False":
                            RbtnBarcodeNo.Checked = true;
                            break;
                        default:
                            RbtnBarcodeNo.Checked = true;
                            break;

                    }
                    switch (dt.Rows[0]["Free"].ToString())
                    {
                        case "True":
                            RbtnFreeYes.Checked = true;
                            break;
                        case "False":
                            RbtnFreeNo.Checked = true;
                            break;
                        default:
                            RbtnFreeNo.Checked = true;
                            break;
                    }
                    switch (dt.Rows[0]["Mrp"].ToString())
                    {
                        case "True":
                            RbtnMrpYes.Checked = true;
                            break;
                        case "False":
                            RbtnMrpNo.Checked = true;
                            break;
                        default:
                            RbtnMrpNo.Checked = true;
                            break;
                    }
                    switch (dt.Rows[0]["GrossValue"].ToString())
                    {
                        case "True":
                            RbtnGrossYes.Checked = true;
                            break;
                        case "False":
                            RbtnGrossNo.Checked = true;
                            break;
                        default:
                            RbtnGrossNo.Checked = true;
                            break;
                    }
                    switch (dt.Rows[0]["NetValue"].ToString())
                    {
                        case "True":
                            RbtnNetYes.Checked = true;
                            break;
                        case "False":
                            RbtnNetNo.Checked = true;
                            break;
                        default:
                            RbtnNetNo.Checked = true;
                            break;
                    }

                    switch (dt.Rows[0]["Focus_Rate_Type"].ToString())
                    {
                        case "True":
                            rbtnRateYes.Checked = true;
                            break;
                        case "False":
                            rbtnRateNo.Checked = true;
                            break;
                        default:
                            rbtnRateNo.Checked = true;
                            break;
                    }

                    switch (dt.Rows[0]["Focus_Sale_Type"].ToString())
                    {
                        case "True":
                            rbtnSaleYes.Checked = true;
                            break;
                        case "False":
                            rbtnSaleNo.Checked = true;
                            break;
                        default:
                            rbtnSaleNo.Checked = true;
                            break;
                    }


                    switch (dt.Rows[0][8].ToString())
                    {
                        case "True":
                            rdbTypeYes.Checked = true;
                            break;
                        case "False":
                            rdbTypeNo.Checked = true;
                            break;
                        default:
                            rdbTypeYes.Checked = true;
                            break;

                    }
                    switch (dt.Rows[0][9].ToString())
                    {
                        case "True":
                            rdbCateYes.Checked = true;
                            break;
                        case "False":
                            rdbCateNo.Checked = true;
                            break;
                        default:
                            rdbCateYes.Checked = true;
                            break;

                    }

                    switch (dt.Rows[0][10].ToString())
                    {
                        case "True":
                            rdbGroupYes.Checked = true;
                            break;
                        case "False":
                            rdbGroupNo.Checked = true;
                            break;
                        default:
                            rdbGroupYes.Checked = true;
                            break;

                    }
                    switch (dt.Rows[0][11].ToString())
                    {
                        case "True":
                            rdbBrandYes.Checked = true;
                            break;
                        case "False":
                            rdbBrandNo.Checked = true;
                            break;
                        default:
                            rdbBrandYes.Checked = true;
                            break;

                    }
                    switch (dt.Rows[0][12].ToString())
                    {
                        case "True":
                            BatchYes.Checked = true;
                            break;
                        case "False":
                            BatchNo.Checked = true;
                            break;
                        default:
                            BatchYes.Checked = true;
                            break;

                    }
                    switch (dt.Rows[0][13].ToString())
                    {
                        case "True":
                            RbtnPriceYes.Checked = true;
                            break;
                        case "False":
                            RbtnPriceNo.Checked = true;
                            break;
                        default:
                            RbtnPriceNo.Checked = true;
                            break;

                    }
                    switch (dt.Rows[0][14].ToString())
                    {
                        case "True":
                            PurchaseYes.Checked = true;
                            break;
                        case "False":
                            PurchaseNo.Checked = true;
                            break;
                        default:
                            PurchaseNo.Checked = true;
                            break;

                    }
                    switch (dt.Rows[0][15].ToString())
                    {
                        case "True":
                            RoundffYes.Checked = true;
                            break;
                        case "False":
                            RoundoffNo.Checked = true;
                            break;
                        default:
                            RoundoffNo.Checked = true;
                            break;

                    }
                    switch (dt.Rows[0][16].ToString())
                    {
                        case "True":
                            rbtnAllowdiscYes.Checked = true;
                            break;
                        case "False":
                            rbtnAllowDiscNo.Checked = true;
                            break;
                        default:
                            rbtnAllowDiscNo.Checked = true;
                            break;

                    }
                    switch (dt.Rows[0][17].ToString())
                    {
                        case "True":
                            rbtnshowPurPriYes.Checked = true;
                            break;
                        case "False":
                            RbtnShowPurPriNo.Checked = true;
                            break;
                        default:
                            RbtnShowPurPriNo.Checked = true;
                            break;

                    }
                    switch (dt.Rows[0]["Description"].ToString())
                    {
                        case "True":
                            RbtnDescYes.Checked = true;
                            break;
                        case "False":
                            RbtnDescNo.Checked = true;
                            break;
                        default:
                            RbtnDescNo.Checked = true;
                            break;
                    }

                    switch (dt.Rows[0]["ShowPurchase"].ToString())
                    {
                        case "True":
                            rbtnshowPurPriYes.Checked = true;
                            break;
                        case "False":
                            RbtnShowPurPriNo.Checked = true;
                            break;
                        default:
                            RbtnShowPurPriNo.Checked = true;
                            break;

                    }



                    switch (dt.Rows[0]["FocusCustomer"].ToString())
                    {
                        case "True":
                            rbtfocuscustomerYes.Checked = true;
                            break;
                        case "False":
                            rbtfocuscustomerNo.Checked = true;
                            break;
                        default:
                            rbtfocuscustomerNo.Checked = true;
                            break;

                    }
                    switch (dt.Rows[0]["FocusSalesMan"].ToString())
                    {
                        case "True":
                            rbtnSalesManYes.Checked = true;
                            break;
                        case "False":
                            rbtnSalesManNo.Checked = true;
                            break;
                        default:
                            rbtnSalesManNo.Checked = true;
                            break;

                    }
                    switch (dt.Rows[0]["MoveToTaxper"].ToString())
                    {
                        case "True":
                            rbtTaxYes.Checked = true;
                            break;
                        case "False":
                            rbtTaxNo.Checked = true;
                            break;
                        default:
                            rbtTaxNo.Checked = true;
                            break;

                    }
                    switch (dt.Rows[0]["MoveToQty"].ToString())
                    {
                        case "True":
                            rbtnQtyYes.Checked = true;
                            break;
                        case "False":
                            rbtnQtyNo.Checked = true;
                            break;
                        default:
                            rbtnQtyNo.Checked = true;
                            break;

                    }
                    switch (dt.Rows[0]["MoveToUnit"].ToString())
                    {
                        case "True":
                            rbtnUnitYes.Checked = true;
                            break;
                        case "False":
                            rbtnUnitNo.Checked = true;
                            break;
                        default:
                            rbtnUnitNo.Checked = true;
                            break;

                    }

                    switch (dt.Rows[0]["SalebyItemName"].ToString())
                    {
                        case "True":
                            rbtnSalebyNameYes.Checked = true;
                            break;
                        case "False":
                            rbtnSalebyNameNo.Checked = true;
                            break;
                        default:
                            rbtnSalebyNameNo.Checked = true;
                            break;

                    }

                    switch (dt.Rows[0]["SalebyItemCode"].ToString())
                    {
                        case "True":
                            rbtnSalebyCodeYes.Checked = true;
                            break;
                        case "False":
                            rbtnSalebyCodeNo.Checked = true;
                            break;
                        default:
                            rbtnSalebyCodeNo.Checked = true;
                            break;

                    }

                    switch (dt.Rows[0]["SalebyBarcode"].ToString())
                    {
                        case "True":
                            rbtnSaleBarcodeYes.Checked = true;
                            break;
                        case "False":
                            rbtnSaleBarcodeNo.Checked = true;
                            break;
                        default:
                            rbtnSaleBarcodeNo.Checked = true;
                            break;

                    }

                    switch (dt.Rows[0]["Hasdiscountlimit"].ToString())
                    {
                        case "True":
                            rbtnndiscountlimityes.Checked = true;
                            break;
                        case "False":
                            rbtnndiscountlimitno.Checked = true;
                            break;
                        default:
                            rbtnndiscountlimitno.Checked = true;
                            break;

                    }

                    switch (dt.Rows[0]["FocusDate"].ToString())
                    {
                        case "True":
                            rbtfocusdateyes.Checked = true;
                            break;
                        case "False":
                            rbtfocusdateno.Checked = true;
                            break;
                        default:
                            rbtfocusdateno.Checked = true;
                            break;

                    }

                    switch (dt.Rows[0]["AllowCustomerDiscount"].ToString())
                    {
                        case "True":
                            rbtnCusDiscYes.Checked = true;
                            break;
                        case "False":
                            RbtnCustDiscNo.Checked = true;
                            break;
                        default:
                            RbtnCustDiscNo.Checked = true;
                            break;

                    }
                    switch (dt.Rows[0]["StockOut"].ToString())
                    {
                        case "True":
                            rbtstockoutyes.Checked = true;
                            break;
                        case "False":
                            rbtstockoutno.Checked = true;
                            break;
                        default:
                            rbtstockoutno.Checked = true;
                            break;

                    }
                    switch (dt.Rows[0]["PrintInvoice"].ToString())
                    {
                        case "True":
                            rbtprintinvoiceyes.Checked = true;
                            break;
                        case "False":
                            rbtprintinvoiceno.Checked = true;
                            break;
                        default:
                            rbtprintinvoiceno.Checked = true;
                            break;

                    }
                    switch (dt.Rows[0]["PUR_MoveDisc"].ToString())
                    {
                        case "True":
                            rbtnMovetoDiscountYes.Checked = true;
                            break;
                        case "False":
                            rbtnMovetoDiscountNo.Checked = true;
                            break;
                        default:
                            rbtnMovetoDiscountNo.Checked = true;
                            break;

                    }

                    switch (dt.Rows[0]["PUR_MoveRtlper"].ToString())
                    {
                        case "True":
                            rbtnMoveToRtlperYes.Checked = true;
                            break;
                        case "False":
                            rbtnMoveToRtlperNo.Checked = true;
                            break;
                        default:
                            rbtnMoveToRtlperNo.Checked = true;
                            break;

                    }
                    switch (dt.Rows[0]["PUR_MoveRtlAmt"].ToString())
                    {
                        case "True":
                            rbtnMoveToRtlAmtYes.Checked = true;
                            break;
                        case "False":
                            rbtnMoveToRtlAmtNo.Checked = true;
                            break;
                        default:
                            rbtnMoveToRtlAmtNo.Checked = true;
                            break;

                    }
                    switch (dt.Rows[0]["PURMoveTaxper"].ToString())
                    {
                        case "True":
                            rbtnMoveToTaxperYes.Checked = true;
                            break;
                        case "False":
                            rbtnMoveToTaxperNo.Checked = true;
                            break;
                        default:
                            rbtnMoveToTaxperNo.Checked = true;
                            break;

                    }
                    switch (dt.Rows[0]["PURMoveTaxAmt"].ToString())
                    {
                        case "True":
                            rbtnMoveToTaxAmtYes.Checked = true;
                            break;
                        case "False":
                            rbtnMoveToTaxAmtNo.Checked = true;
                            break;
                        default:
                            rbtnMoveToTaxAmtNo.Checked = true;
                            break;

                    }
                    switch (dt.Rows[0]["PURMoveTotal"].ToString())
                    {
                        case "True":
                            rbtnMoveToTotalYes.Checked = true;
                            break;
                        case "False":
                            rbtnMoveToTotalNo.Checked = true;
                            break;
                        default:
                            rbtnMoveToTotalNo.Checked = true;
                            break;

                    }
                    switch (dt.Rows[0]["PURFocusDate"].ToString())
                    {
                        case "True":
                            rbtnFocusDateYes.Checked = true;
                            break;
                        case "False":
                            rbtnFocusDateNo.Checked = true;
                            break;
                        default:
                            rbtnFocusDateNo.Checked = true;
                            break;

                    }
                    switch (dt.Rows[0]["PURFocusSupplier"].ToString())
                    {
                        case "True":
                            rbtnlFocusSupplierYes.Checked = true;
                            break;
                        case "False":
                            rbtnlFocusSupplierNo.Checked = true;
                            break;
                        default:
                            rbtnlFocusSupplierNo.Checked = true;
                            break;

                    }
                    switch (dt.Rows[0]["PUR_FocusInvoice"].ToString())
                    {
                        case "True":
                            rbtnFocusInvoiceYes.Checked = true;
                            break;
                        case "False":
                            rbtnFocusInvoiceNo.Checked = true;
                            break;
                        default:
                            rbtnFocusInvoiceNo.Checked = true;
                            break;

                    }

                    switch (dt.Rows[0]["PUR_FocusReference"].ToString())
                    {
                        case "True":
                            rbtFocusReferenceYes.Checked = true;
                            break;
                        case "False":
                            rbtFocusReferenceNo.Checked = true;
                            break;
                        default:
                            rbtFocusReferenceYes.Checked = true;
                            break;

                    }
                    switch (dt.Rows[0]["PUR_FocusBarcode"].ToString())
                    {
                        case "True":
                            rbtnPurByBarcodeYes.Checked = true;
                            break;
                        case "False":
                            rbtnPurByBarcodeNo.Checked = true;
                            break;
                        default:
                            rbtnPurByBarcodeNo.Checked = true;
                            break;

                    }
                    switch (dt.Rows[0]["PUR_FocusItemCode"].ToString())
                    {
                        case "True":
                            rbtnPurByItemCodeYes.Checked = true;
                            break;
                        case "False":
                            rbtnPurByItemCodeNo.Checked = true;
                            break;
                        default:
                            rbtnPurByItemCodeNo.Checked = true;
                            break;

                    }

                    switch (dt.Rows[0]["ACTIVE_PERIOD"].ToString())
                    {
                        case "True":
                            CHECK_CREDIT_PERIOD.Checked = true;
                            break;
                        case "False":
                            CHECK_CREDIT_PERIOD.Checked = false;
                            break;
                    }

                    if (CHECK_CREDIT_PERIOD.Checked == true)
                    {
                        txt_credit_period.Text = dt.Rows[0]["CREDIT_PERIOD"].ToString();
                    }


                    switch (dt.Rows[0]["ACTIVE_DEBIT_PERIOD"].ToString())
                    {
                        case "True":
                            check_debit_period.Checked = true;
                            break;
                        case "False":
                            check_debit_period.Checked = false;
                            break;
                    }
                    switch (dt.Rows[0]["Description"].ToString())
                    {
                        case "True":
                            RbtnDescYes.Checked = true;
                            break;
                        case "False":
                            RbtnDescYes.Checked = false;
                            break;
                    }
                    switch (dt.Rows[0]["Exclusive_tax"].ToString())
                    {
                        case "True":
                            rbtnTaxExclusiveYes.Checked = true;
                            break;
                        case "False":
                            rbtnTaxExclusiveNo.Checked = true;
                            break;
                    }
                     switch (dt.Rows[0]["Pur_Exclusive_tax"].ToString())
                    {
                        case "True":
                            PurchaseTaxExclusiveYes.Checked = true;
                            break;
                        case "False":
                            PurchaseTaxExclusiveNo.Checked = true;
                            break;
                    }
                     switch (dt.Rows[0]["priceBatch"].ToString())
                     {
                         case "True":
                             priceBatchYes.Checked = true;
                             break;
                         case "False":
                             priceBatchNo.Checked = true;
                             break;
                         default:
                             priceBatchNo.Checked = true;
                             break;

                     }
                    if (check_debit_period.Checked == true)
                    {
                        txt_debit_period.Text = dt.Rows[0]["DEBIT_PERIOD"].ToString();
                    }

                    // AllowCustomerDiscount
                    txtdiscountamt.Text = dt.Rows[0]["DiscountAmt"].ToString();
                    txtDiscountperct.Text = dt.Rows[0]["DiscountPerct"].ToString();
                    cbxBranch.SelectedValue = dt.Rows[0][4].ToString();
                    cbx_Receiptvchr.Text = dt.Rows[0][5].ToString();
                    Cbx_PaymentVchr.Text = dt.Rows[0][6].ToString();
                    cbx_Invoice.Text = dt.Rows[0][7].ToString();
                    cmbItemNameformat.Text = dt.Rows[0]["ItemName"].ToString();
                    if (Convert.ToString(dt.Rows[0]["DefaultSaleType"]).Equals(""))
                    {
                        cmbSaleType.SelectedIndex = 0;
                    }
                    else
                    {
                        cmbSaleType.SelectedValue = dt.Rows[0]["DefaultSaleType"];
                    }

                    if (dt.Rows[0]["DefaultRateType"] == DBNull.Value)
                    {
                        cmbRateType.SelectedIndex = 1;
                    }
                    else
                    {
                        cmbRateType.SelectedValue = dt.Rows[0]["DefaultRateType"];
                    }
                    if (dt.Rows[0]["DefaultTax"] == DBNull.Value||dt.Rows[0]["DefaultTax"].ToString() == "")
                    {
                        cmbTaxType.SelectedIndex = 0;
                    }
                    else
                    {
                        cmbTaxType.SelectedValue = dt.Rows[0]["DefaultTax"].ToString();
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
        }


        private void grdBranch_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (grdBranch.Rows.Count > 0 && grdBranch.CurrentRow != null)
            {
                DataGridViewCellCollection c = grdBranch.CurrentRow.Cells;
                BranchId = c[0].Value.ToString();
                txt_brnchCode.Text = c["CODE"].Value.ToString();
                txt_branch.Text = c["DESC_ENG"].Value.ToString();
                //   txt_arabbranch.Text = c["DESC_ARB"].Value.ToString();
                txt_addr1.Text = c["ADDRESS_1"].Value.ToString();
                txt_addr2.Text = c["ADDRESS_2"].Value.ToString();
                ARBADRESS1.Text = c["ARBADDRESS_1"].Value.ToString();
                ARBADRESS2.Text = c["ARBADDRESS_2"].Value.ToString();
                txt_arabbranch.Text = c["DESC_ARB"].Value.ToString();
                Drp_Currency.SelectedValue = c["DEFAULT_CURRENCY_CODE"].Value.ToString();

                switch (c[3].Value.ToString())
                {
                    case "1":
                        radioAppYes.Checked = true;
                        break;
                    case "0":
                        radioAppNo.Checked = true;
                        break;
                    default:
                        radioAppNo.Checked = true;
                        break;
                }
                switch (c[4].Value.ToString())
                {
                    case "1":
                        radioSpareYes.Checked = true;
                        break;
                    case "0":
                        radioSpareNo.Checked = true;
                        break;
                    default:
                        radioSpareNo.Checked = true;
                        break;
                }
                switch (c[5].Value.ToString())
                {
                    case "1":
                        radioYes.Checked = true;
                        break;
                    case "0":
                        radioNo.Checked = true;
                        break;
                    default:
                        radioNo.Checked = true;
                        break;
                }
                switch (c["ALLOW_SERVICE"].Value.ToString())
                {
                    case "1":
                        radioButton5.Checked = true;
                        break;
                    case "0":
                        radioButton3.Checked = true;
                        break;
                    default:
                        radioButton3.Checked = true;
                        break;
                }
                txt_phone.Text = c["TELE_1"].Value.ToString();
            }
        }


        //public void ResetInvoiceNo()
        //{
        //    try
        //    {
        //        /* query = "dbcc checkident(INV_SALES_HDR"+VOUCHNUM.Text+")";
        //         query.CommandType = CommandType.Text;
        //         query.Connection = conn;
        //         Dbfunctions.InsertUpdate(query);*/
        //    }
        //    catch (Exception ee)
        //    {
        //        MessageBox.Show(ee.Message);
        //    }
        //}

        private void BtnGeneralSave_Click(object sender, EventArgs e)
        {
            cset.DiscountPerct = Convert.ToDecimal(txtDiscountperct.Text);
            cset.DiscountAmt = Convert.ToDecimal(txtdiscountamt.Text);

            string companyname = "";
            DataTable dt = new DataTable();
            dt = cset.SysSetup_selectcompany();
            if (dt.Rows.Count > 0)
            {
                companyname = dt.Rows[0][1].ToString();
            }

            //focus to customer on loading
            var checkcustomer = panelcustomer.Controls.OfType<RadioButton>()
                                         .FirstOrDefault(r => r.Checked);
            switch (checkcustomer.Text)
            {
                case "Yes":
                    cset.FocusCustomer = true;
                    break;
                case "No":
                    cset.FocusCustomer = false;
                    break;
                default:
                    cset.FocusCustomer = false;
                    break;
            }


            //Focusto Salesman
            var checksalesman = panelsalesman.Controls.OfType<RadioButton>()
                                         .FirstOrDefault(r => r.Checked);
            switch (checksalesman.Text)
            {
                case "Yes":
                    cset.FocusSalesMan = true;
                    break;
                case "No":
                    cset.FocusSalesMan = false;
                    break;
                default:
                    cset.FocusSalesMan = false;
                    break;
            }

            var checkrate_type = panelRatetype.Controls.OfType<RadioButton>()
                                            .FirstOrDefault(r => r.Checked);
            switch (checkrate_type.Text)
            {
                case "Yes":
                    cset.Focus_Rate_type = true;
                    break;
                case "No":
                    cset.Focus_Rate_type = false;
                    break;
                default:
                    cset.Focus_Rate_type = false;
                    break;
            }

            var checksale_type = panelSaletype.Controls.OfType<RadioButton>()
                                            .FirstOrDefault(r => r.Checked);
            switch (checksale_type.Text)
            {
                case "Yes":
                    cset.Focus_Sale_Type = true;
                    break;
                case "No":
                    cset.Focus_Sale_Type = false;
                    break;
                default:
                    cset.Focus_Sale_Type = false;
                    break;
            }

            var checkedunit = panelUnit.Controls.OfType<RadioButton>()
                                         .FirstOrDefault(r => r.Checked);
            switch (checkedunit.Text)
            {
                case "Yes":
                    cset.MoveToUnit = true;
                    break;
                case "No":
                    cset.MoveToUnit = false;
                    break;
                default:
                    cset.MoveToUnit = false;
                    break;
            }


            var checkqty = panelqty.Controls.OfType<RadioButton>()
                                       .FirstOrDefault(r => r.Checked);
            switch (checkqty.Text)
            {
                case "Yes":
                    cset.MoveToQty = true;
                    break;
                case "No":
                    cset.MoveToQty = false;
                    break;
                default:
                    cset.MoveToQty = false;
                    break;
            }



            var checksalebyitemname = panelSalebyname.Controls.OfType<RadioButton>()
                                      .FirstOrDefault(r => r.Checked);
            switch (checksalebyitemname.Text)
            {
                case "Yes":
                    cset.SalebyItemName = true;
                    break;
                case "No":
                    cset.SalebyItemName = false;
                    break;
                default:
                    cset.SalebyItemName = false;
                    break;
            }



            var checksalebyitemcode = panelSaleByCode.Controls.OfType<RadioButton>()
                                    .FirstOrDefault(r => r.Checked);
            switch (checksalebyitemcode.Text)
            {
                case "Yes":
                    cset.SalebyItemCode = true;
                    break;
                case "No":
                    cset.SalebyItemCode = false;
                    break;
                default:
                    cset.SalebyItemCode = false;
                    break;
            }


            var checkbarcode = panelSaleBarcode.Controls.OfType<RadioButton>()
                                    .FirstOrDefault(r => r.Checked);
            switch (checkbarcode.Text)
            {
                case "Yes":
                    cset.SalebyBarcode = true;
                    break;
                case "No":
                    cset.SalebyBarcode = false;
                    break;
                default:
                    cset.SalebyBarcode = false;
                    break;
            }

            var checkExclusiveTax =panelExlusiveTax.Controls.OfType<RadioButton>()
                                   .FirstOrDefault(r => r.Checked);
            switch (checkExclusiveTax.Text)
            {
                case "Yes":
                    cset.ExclusiveTax = true;
                    break;
                case "No":
                    cset.ExclusiveTax = false;
                    break;
                default:
                    cset.ExclusiveTax = false;
                    break;
            }



            cset.Company_Name = companyname;
            cset.BranchCode = Convert.ToString(cbxBranch.SelectedValue);
            cset.Payment_Voucher = Cbx_PaymentVchr.Text;
            cset.Receipt_Voucher = cbx_Receiptvchr.Text;
            cset.Invoice = cbx_Invoice.Text;
            var checkedtaxper = paneltaxper.Controls.OfType<RadioButton>()
                                        .FirstOrDefault(r => r.Checked);
            switch (checkedtaxper.Text)
            {
                case "Yes":
                    cset.MoveToTaxper = true;
                    break;
                case "No":
                    cset.MoveToTaxper = false;
                    break;
                default:
                    cset.MoveToTaxper = false;
                    break;
            }


            //edit item price or not
            var checkedmovetoPrice = Panelprice.Controls.OfType<RadioButton>()
                                        .FirstOrDefault(r => r.Checked);
            switch (checkedmovetoPrice.Text)
            {
                case "Yes":
                    cset.MoveToPrice = true;
                    break;
                case "No":
                    cset.MoveToPrice = false;
                    break;
                default:
                    cset.MoveToPrice = false;
                    break;
            }


            //show last purchase price or not
            var checkedPurchase = panelLASTPURCHAE.Controls.OfType<RadioButton>()
                                        .FirstOrDefault(r => r.Checked);
            switch (checkedPurchase.Text)
            {
                case "Yes":
                    cset.SelectLastPurchase = true;
                    break;
                case "No":
                    cset.SelectLastPurchase = false;
                    break;
                default:
                    cset.SelectLastPurchase = false;
                    break;
            }


            

            //move to discount text box
            var checkallowdiscount = panelallowdiscount.Controls.OfType<RadioButton>()
                                         .FirstOrDefault(r => r.Checked);
            switch (checkallowdiscount.Text)
            {
                case "Yes":
                    cset.MoveToDisc = true;
                    break;
                case "No":
                    cset.MoveToDisc = false;
                    break;
                default:
                    cset.MoveToDisc = false;
                    break;
            }

            //show purchase price
            var checkSHOWPURCHASE = panelShowPurchase.Controls.OfType<RadioButton>()
                                       .FirstOrDefault(r => r.Checked);
            switch (checkSHOWPURCHASE.Text)
            {
                case "Yes":
                    cset.ShowPurchase = true;
                    break;
                case "No":
                    cset.ShowPurchase = false;
                    break;
                default:
                    cset.ShowPurchase = false;
                    break;
            }

            var DiscountLimit = pnldiscountlimit.Controls.OfType<RadioButton>()
                                    .FirstOrDefault(r => r.Checked);
            switch (DiscountLimit.Text)
            {
                case "Yes":
                    cset.HasDiscountLimit = true;
                    break;
                case "No":
                    cset.HasDiscountLimit = false;
                    break;
                default:
                    cset.HasDiscountLimit = false;
                    break;
            }


            var FocusDate = paneldate.Controls.OfType<RadioButton>()
                                   .FirstOrDefault(r => r.Checked);
            switch (FocusDate.Text)
            {
                case "Yes":
                    cset.FocusDate = true;
                    break;
                case "No":
                    cset.FocusDate = false;
                    break;
                default:
                    cset.FocusDate = false;
                    break;
            }
            var custdisc = panelCustDisc.Controls.OfType<RadioButton>()
                                  .FirstOrDefault(r => r.Checked);
            switch (custdisc.Text)
            {
                case "Yes":
                    cset.AllowCustomerDiscount = true;
                    break;
                case "No":
                    cset.AllowCustomerDiscount = false;
                    break;
                default:
                    cset.AllowCustomerDiscount = false;
                    break;
            }

            var checkStockout = pnlstockout.Controls.OfType<RadioButton>()
                                         .FirstOrDefault(r => r.Checked);
            switch (checkStockout.Text)
            {
                case "Yes":
                    cset.StockOut = true;
                    break;
                case "No":
                    cset.StockOut = false;
                    break;
                default:
                    cset.StockOut = false;
                    break;
            }


            var chckprintinvoice = pnlprintinvoice.Controls.OfType<RadioButton>()
                                  .FirstOrDefault(r => r.Checked);
            switch (chckprintinvoice.Text)
            {
                case "Yes":
                    cset.PrintInvoice = true;
                    break;
                case "No":
                    cset.PrintInvoice = false;
                    break;
                default:
                    cset.PrintInvoice = false;
                    break;
            }
            //var checkedtax = paneltax.Controls.OfType<RadioButton>()
            //                             .FirstOrDefault(r => r.Checked);
            //switch (checkedtax.Text)
            //{
            //    case "Yes":
            //        cset.TAX = true;
            //        break;
            //    case "No":
            //        cset.TAX = false;
            //        break;
            //    default:
            //        cset.TAX = false;
            //        break;
            //}
            var checkedFree = panelfree.Controls.OfType<RadioButton>()
                                    .FirstOrDefault(r => r.Checked);
            switch (checkedFree.Text)
            {
                case "Yes":
                    cset.Free = true;
                    break;
                case "No":
                    cset.Free = false;
                    break;
                default:
                    cset.Free = false;
                    break;
            }

            var checkedMrp = panelMrp.Controls.OfType<RadioButton>()
                                    .FirstOrDefault(r => r.Checked);
            switch (checkedMrp.Text)
            {
                case "Yes":
                    cset.Mrp = true;
                    break;
                case "No":
                    cset.Mrp = false;
                    break;
                default:
                    cset.Mrp = false;
                    break;
            }

            var checkedGross = panelGross.Controls.OfType<RadioButton>()
                                        .FirstOrDefault(r => r.Checked);
            switch (checkedGross.Text)
            {
                case "Yes":
                    cset.GrossValue = true;
                    break;
                case "No":
                    cset.GrossValue = false;
                    break;
                default:
                    cset.GrossValue = false;
                    break;
            }

            var checkedNet = panelNet.Controls.OfType<RadioButton>()
                                    .FirstOrDefault(r => r.Checked);
            switch (checkedNet.Text)
            {
                case "Yes":
                    cset.NetValue = true;
                    break;
                case "No":
                    cset.NetValue = false;
                    break;
                default:
                    cset.NetValue = false;
                    break;
            }

            var checkedDesc = panelDesc.Controls.OfType<RadioButton>()
                                    .FirstOrDefault(r => r.Checked);
            switch (checkedDesc.Text)
            {
                case "Yes":
                    cset.Description = true;
                    break;
                case "No":
                    cset.Description = false;
                    break;
                default:
                    cset.Description = false;
                    break;
            }
            cset.DefaultSaleType = Convert.ToString(cmbSaleType.SelectedValue);
            cset.DefaultRateType = Convert.ToString(cmbRateType.SelectedValue);
            if (txtInvNo.Text != string.Empty)
            {
                StartFrom();
                cset.UpdateGeneralSetup();
                //ResetInvoiceNo();
                MessageBox.Show("Updated!!");
            }
            else
            {
                MessageBox.Show("Assign a starting point for the invoice");
            }                                
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string companyname = "";
            DataTable dt = new DataTable();
            dt = cset.SysSetup_selectcompany();
            if (dt.Rows.Count > 0)
            {
                companyname = dt.Rows[0][1].ToString();
            }

            var chekHasType = PanType.Controls.OfType<RadioButton>()
                                         .FirstOrDefault(r => r.Checked);
            switch (chekHasType.Text)
            {
                case "Yes":
                    cset.HasType = true;
                    break;
                case "No":
                    cset.HasType = false;
                    break;
                default:
                    cset.HasType = false;
                    break;
            }
            var chekHasCategory = PanCategory.Controls.OfType<RadioButton>()
                                         .FirstOrDefault(r => r.Checked);
            switch (chekHasCategory.Text)
            {
                case "Yes":
                    cset.HasCategory = true;
                    break;
                case "No":
                    cset.HasCategory = false;
                    break;
                default:
                    cset.HasCategory = false;
                    break;
            }
            var checkGroup = PanGroup.Controls.OfType<RadioButton>()
                                         .FirstOrDefault(r => r.Checked);
            switch (checkGroup.Text)
            {
                case "Yes":
                    cset.HasGroup = true;
                    break;
                case "No":
                    cset.HasGroup = false;
                    break;
                default:
                    cset.HasGroup = false;
                    break;
            }
            var chekBrand = PanBrand.Controls.OfType<RadioButton>()
                                        .FirstOrDefault(r => r.Checked);
            switch (chekBrand.Text)
            {
                case "Yes":
                    cset.HasTM = true;
                    break;
                case "No":
                    cset.HasTM = false;
                    break;
                default:
                    cset.HasTM = false;
                    break;
            }
            var checkedBarcode = panelBarcode.Controls.OfType<RadioButton>()
                                       .FirstOrDefault(r => r.Checked);
            switch (checkedBarcode.Text)
            {
                case "Yes":
                    cset.BARCODE = true;
                    break;
                case "No":
                    cset.BARCODE = false;
                    break;
                default:
                    cset.BARCODE = false;
                    break;
            }
            var checkedPos = panelPos.Controls.OfType<RadioButton>()
                                         .FirstOrDefault(r => r.Checked);
            switch (checkedPos.Text)
            {
                case "Yes":
                    cset.POSTerminal = true;
                    break;
                case "No":
                    cset.POSTerminal = false;
                    break;
                default:
                    cset.POSTerminal = false;
                    break;
            }

            var checkedtax = paneltax.Controls.OfType<RadioButton>()
                                        .FirstOrDefault(r => r.Checked);
            switch (checkedtax.Text)
            {
                case "Yes":
                    cset.TAX = true;
                    break;
                case "No":
                    cset.TAX = false;
                    break;
                default:
                    cset.TAX = false;
                    break;
            }
            
            var checkedArabic = panelArabic.Controls.OfType<RadioButton>()
                                        .FirstOrDefault(r => r.Checked);
            switch (checkedArabic.Text)
            {
                case "Yes":
                    cset.Arabic = true;
                    break;
                case "No":
                    cset.Arabic = false;
                    break;
                default:
                    cset.Arabic = false;
                    break;
            }

            

            //allow round off or not
            var checkedroundoff = panelRoundOff.Controls.OfType<RadioButton>()
                                        .FirstOrDefault(r => r.Checked);
            switch (checkedroundoff.Text)
            {
                case "Yes":
                    cset.HasRoundOff = true;
                    break;
                case "No":
                    cset.HasRoundOff = false;
                    break;
                default:
                    cset.HasRoundOff = false;
                    break;
            }
            // bool P;
            if (CHECK_CREDIT_PERIOD.Checked)
            {
                p = "True";
                cset.ACTIVE_PERIOD = Convert.ToBoolean(p);
            }
            else
            {
                p = "False";
                cset.ACTIVE_PERIOD = Convert.ToBoolean(p);
            }
            cset.CREDIT_PERIOD = txt_credit_period.Text;

            if (check_debit_period.Checked)
            {
                d = "True";
                cset.ACTIVE_DEBIT_PERIOD = Convert.ToBoolean(d);
            }
            else
            {
                d = "False";
                cset.ACTIVE_DEBIT_PERIOD = Convert.ToBoolean(d);
            }
            cset.DEBIT_PERIOD = txt_debit_period.Text;

            try
            {
                cset.BranchCode = cbxBranch.SelectedValue.ToString();
            }
            catch
            {
                cset.BranchCode = "";
            }


            cset.Payment_Voucher = Cbx_PaymentVchr.Text;
            cset.Receipt_Voucher = cbx_Receiptvchr.Text;
            cset.Invoice = cbx_Invoice.Text;

            var checkedBatch = panelBatch.Controls.OfType<RadioButton>()
                                         .FirstOrDefault(r => r.Checked);
            switch (checkedBatch.Text)
            {
                case "Yes":
                    cset.BATCH = true;
                    break;
                case "No":
                    cset.BATCH = false;
                    break;
                default:
                    cset.BATCH = false;
                    break;
            }
            var checkedPriceBatch = panelPriceBatch.Controls.OfType<RadioButton>()
                                      .FirstOrDefault(r => r.Checked);
            switch (checkedPriceBatch.Text)
            {
                case "Yes":
                    cset.priceBatch = true;
                    break;
                case "No":
                    cset.priceBatch = false;
                    break;
                default:
                    cset.priceBatch = false;
                    break;
            }
            cset.Company_Name = companyname;
            cset.ItemName = cmbItemNameformat.Text;

            try
            {
                cset.DefaultTax = cmbTaxType.SelectedValue.ToString();
            }
            catch
            {
                cset.DefaultTax = "-1";
            }

            cset.Dec_qty = Convert.ToDecimal(ComboDeci.SelectedIndex.ToString()) + 1;
            cset.UpdateGeneralItemSetup();
            updateState();
            update_CusAsSup();
            btnSavedate.PerformClick();
            MessageBox.Show("Updated!! ReOpen Form to see Changes");
        }
        void update_CusAsSup()
        {
            try
            {
                //SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
                //SqlCommand query = );
                //query.Connection = conn;
                query = "UPDATE SYS_SETUP SET IsCusSup = @value1";
                Dictionary<string, object> Parameter = new Dictionary<string, object>();
                Parameter.Clear();
                Parameter.Add("@value1", RadCusandSupYes.Checked);
                //conn.Open();
                DbFunctions.InsertUpdate(query);
                //conn.Close();
            }
            catch
            {
            }
            
        }
        private void updateState()
        {
            //SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
            //SqlCommand query = );
            //query.Connection = conn;
            query = "UPDATE SYS_SETUP SET CurrentState = @state";
            Dictionary<string, object> Parameter = new Dictionary<string, object>();
            Parameter.Clear();
            Parameter.Add("@state", cmbState.SelectedValue);
            DbFunctions.InsertUpdate(query,Parameter);
            lg.state = Convert.ToString(cmbState.SelectedValue);
        }

        private void btnSavedate_Click(object sender, EventArgs e)
        {
            if (rbtnlimitYes.Checked == true)
            {
                datset.HasAccessLimit = true;
                datset.updateAccessLimit();
                if (drptype.Text == "Date")
                {

                    datset.Type = drptype.Text;
                    datset.Date = DOC_DATE_GRE.Value;
                    datset.PeriodType = "";
                    datset.days = 0;
                    datset.InsertDateDetails();
                    // MessageBox.Show("Updated!");
                }
                else
                {
                    datset.Type = drptype.Text;
                    datset.Date = DOC_DATE_GRE.Value;
                    if (txt_Duration.Text == "")
                    {
                        datset.days = 0;
                    }
                    else
                        datset.days = Convert.ToInt16(txt_Duration.Text);
                    var periodtype = panelDMY.Controls.OfType<RadioButton>()
                                             .FirstOrDefault(r => r.Checked);
                    switch (periodtype.Text)
                    {
                        case "Year":
                            datset.PeriodType = "Y";
                            break;
                        case "Month":
                            datset.PeriodType = "M";
                            break;
                        case "Day":
                            datset.PeriodType = "D";
                            break;
                        default:
                            datset.PeriodType = "";
                            break;
                    }
                    datset.InsertDateDetails();
                    MessageBox.Show("Updated!");
                }
            }
            else
            {
                datset.HasAccessLimit = false;
                datset.updateAccessLimit();
                MessageBox.Show("Updated!");
            }
        }

        private void rbtnlimitYes_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnlimitYes.Checked == true)
            {
                getdateaccessdetails();
            }
            else
            {
                paneldatelimit.Enabled = false;

            }
        }

        private void drptype_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (drptype.Text == "")
            //{
            //    panelperiod.Visible = false;
            //    panelDate1.Visible = false;
            //}
            //if (drptype.SelectedIndex == 0)
            //{
            //    DOC_DATE_GRE.Value = DateTime.Now;
            //    panelperiod.Visible = false;
            //}

            //if (drptype.SelectedIndex == 1)
            //{
            //    txt_Duration.Text = "";
            //}
            if (drptype.SelectedIndex == 0)
            {
                DOC_DATE_GRE.Value = DateTime.Now;
                panelperiod.Visible = false;
                panelDate1.Visible = true;
            }

            if (drptype.SelectedIndex == 1)
            {
                txt_Duration.Text = "";
                panelperiod.Visible = true;
                panelDate1.Visible = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (DialogResult.Yes == MessageBox.Show("New Financial Year Set As" + dtpUpdateFrom.Value.ToShortDateString() + " to " + dtpUpdateTo.Value.ToShortDateString(), "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                {
                    cset.Status = false;
                    cset.UpdateFinancialYearStatus();
                    cset.SDate = dtpUpdateFrom.Value;
                    cset.EDate = dtpUpdateTo.Value;
                    cset.insertFinancialYear();
                    MessageBox.Show("New Financial Year Set As" + dtpUpdateFrom.Value.ToShortDateString() + " to " + dtpUpdateTo.Value.ToShortDateString());
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }
        Class.Translation Transl = new Class.Translation();

        private void txt_cname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                //ARBNAME.Text = Transl.TranslateText(txt_cname.Text.ToLower(), "en|ar");
                ARBNAME.Text = Class.Translator.Translate(txt_cname.Text.ToLower(), "English", "Arabic");
            }
        }
        

        private void btnclose_Click(object sender, EventArgs e)
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

        private void btnPurSave_Click(object sender, EventArgs e)
        {
            try
            {
                string companyname = "";
                DataTable dt = new DataTable();
                dt = cset.SysSetup_selectcompany();
                if (dt.Rows.Count > 0)
                {
                    companyname = dt.Rows[0][1].ToString();
                }


                cset.Company_Name = companyname;
                var checkfocusdate = pnlFocusDate.Controls.OfType<RadioButton>()
                                            .FirstOrDefault(r => r.Checked);
                switch (checkfocusdate.Text)
                {
                    case "Yes":
                        cset.PUR_FocusDate = true;
                        break;
                    case "No":
                        cset.PUR_FocusDate = false;
                        break;
                    default:
                        cset.PUR_FocusDate = false;
                        break;
                }
                var checkSupplier = pnlFocusSupplier.Controls.OfType<RadioButton>()
                                            .FirstOrDefault(r => r.Checked);
                switch (checkSupplier.Text)
                {
                    case "Yes":
                        cset.PUR_FocusSupplier = true;
                        break;
                    case "No":
                        cset.PUR_FocusSupplier = false;
                        break;
                    default:
                        cset.PUR_FocusSupplier = false;
                        break;
                }

                var chkinvoice = pnlFocusInvoice.Controls.OfType<RadioButton>()
                                           .FirstOrDefault(r => r.Checked);
                switch (chkinvoice.Text)
                {
                    case "Yes":
                        cset.PUR_FocusInvoice = true;
                        break;
                    case "No":
                        cset.PUR_FocusInvoice = false;
                        break;
                    default:
                        cset.PUR_FocusInvoice = false;
                        break;
                }
                var chkreference = pnlFocusReference.Controls.OfType<RadioButton>()
                                           .FirstOrDefault(r => r.Checked);
                switch (chkreference.Text)
                {
                    case "Yes":
                        cset.PUR_FocusReference = true;
                        break;
                    case "No":
                        cset.PUR_FocusReference = false;
                        break;
                    default:
                        cset.PUR_FocusReference = false;
                        break;
                }

                var chkpurbybarcode = pnlPurByBarcode.Controls.OfType<RadioButton>()
                                         .FirstOrDefault(r => r.Checked);
                switch (chkpurbybarcode.Text)
                {
                    case "Yes":
                        cset.PUR_FocusBarcode = true;
                        break;
                    case "No":
                        cset.PUR_FocusBarcode = false;
                        break;
                    default:
                        cset.PUR_FocusBarcode = false;
                        break;
                }

                var checkitemcode = pnlPurByItemCode.Controls.OfType<RadioButton>()
                                        .FirstOrDefault(r => r.Checked);
                switch (checkitemcode.Text)
                {
                    case "Yes":
                        cset.PUR_FocusItemCode = true;
                        break;
                    case "No":
                        cset.PUR_FocusItemCode = false;
                        break;
                    default:
                        cset.PUR_FocusItemCode = false;
                        break;
                }

                var checkdiscount = pnlMovetoDiscount.Controls.OfType<RadioButton>()
                                        .FirstOrDefault(r => r.Checked);
                switch (checkdiscount.Text)
                {
                    case "Yes":
                        cset.PUR_MoveDisc = true;
                        break;
                    case "No":
                        cset.PUR_MoveDisc = false;
                        break;
                    default:
                        cset.PUR_MoveDisc = false;
                        break;
                }

                var checkrtlper = pnlMoveToRtlper.Controls.OfType<RadioButton>()
                                        .FirstOrDefault(r => r.Checked);
                switch (checkrtlper.Text)
                {
                    case "Yes":
                        cset.PUR_MoveRtlper = true;
                        break;
                    case "No":
                        cset.PUR_MoveRtlper = false;
                        break;
                    default:
                        cset.PUR_MoveRtlper = false;
                        break;
                }

                var checkrtlamt = pnlMoveToRtlAmt.Controls.OfType<RadioButton>()
                                       .FirstOrDefault(r => r.Checked);
                switch (checkrtlamt.Text)
                {
                    case "Yes":
                        cset.PUR_MoveRtlAmt = true;
                        break;
                    case "No":
                        cset.PUR_MoveRtlAmt = false;
                        break;
                    default:
                        cset.PUR_MoveRtlAmt = false;
                        break;
                }

                var checktaxper = pnlMoveToTaxper.Controls.OfType<RadioButton>()
                                       .FirstOrDefault(r => r.Checked);
                switch (checktaxper.Text)
                {
                    case "Yes":
                        cset.PUR_MoveTaxper = true;
                        break;
                    case "No":
                        cset.PUR_MoveTaxper = false;
                        break;
                    default:
                        cset.PUR_MoveTaxper = false;
                        break;
                }


                var checktaxamt = pnlMoveToTaxAmt.Controls.OfType<RadioButton>()
                                       .FirstOrDefault(r => r.Checked);
                switch (checktaxamt.Text)
                {
                    case "Yes":
                        cset.PUR_MoveTaxAmt = true;
                        break;
                    case "No":
                        cset.PUR_MoveTaxAmt = false;
                        break;
                    default:
                        cset.PUR_MoveTaxAmt = false;
                        break;
                }

                var checktotal = pnlMoveToTotal.Controls.OfType<RadioButton>()
                                      .FirstOrDefault(r => r.Checked);
                switch (checktotal.Text)
                {
                    case "Yes":
                        cset.PUR_MoveTotal = true;
                        break;
                    case "No":
                        cset.PUR_MoveTotal = false;
                        break;
                    default:
                        cset.PUR_MoveTotal = false;
                        break;
                }
                var purchaseTaxExclusive =panelPurchaseTax.Controls.OfType<RadioButton>()
                                            .FirstOrDefault(r => r.Checked);
                switch (purchaseTaxExclusive.Text)
                {
                    case "Yes":
                        cset.PUR_tax_Exclusive = true;
                        break;
                    case "No":
                        cset.PUR_tax_Exclusive = false;
                        break;
                    default:
                        cset.PUR_tax_Exclusive = false;
                        break;
                }
                cset.PUR_expcper = Convert.ToDecimal(tb_adexpence.Text);
                cset.UpdateGeneralPurchaseSetup();
                SavePurchaseType();
                SaveStartVoucher();
                //  btnSavedate.PerformClick();
                MessageBox.Show("Updated!!");
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            string companyname = "";
            DataTable dt = new DataTable();
            dt = cset.SysSetup_selectcompany();
            if (dt.Rows.Count > 0)
            {
                companyname = dt.Rows[0][1].ToString();
            }
            cset.Company_Name = companyname;
            cset.Rep_LoadinDays = tb_ReportPeriod.Text;
            cset.Pro_tax_Exclusive = ptax.Checked;
            cset.UpdateProduction();
            cset.UpdateReportSetup();
            MessageBox.Show("Updated!! ReOpen Form to see Changes");
        }

        private void buttonConnectDb_Click(object sender, EventArgs e)
        {
            if (comboBoxServer.SelectedIndex >= 0)
            {
                if (textBoxUid.Text != "" && textBoxPassword.Text != "")
                {
                    GetAllDataBase();
                    MessageBox.Show("Successfully Connected.");
                }
                else
                {
                    MessageBox.Show("Please enter userId and password");
                }
            }
            else
            {
                MessageBox.Show("Please select a Server to connect");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fdb = new FolderBrowserDialog();
            fdb.ShowDialog();
            if (fdb.SelectedPath != null)
            {
                txtDirectory.Text = fdb.SelectedPath.ToString();

            }
        }

        public void reload()
        {
            SqlDataReader rd = null;//= new SqlDataReader();
            query = "select * from tbl_backup where id='1'";
            //conn.Open();
            //query.Connection = conn;
            rd = DbFunctions.GetDataReader(query);
            if (rd.Read() == true)
            {

                comboBoxServer.Text = rd["data_source"].ToString();
                textBoxUid.Text = rd["user_id"].ToString();
                comboBoxDatabaseList.Text = rd["db"].ToString();
                txtDirectory.Text = rd["path"].ToString();
                textBoxPassword.Text = Convert.ToString(rd["pass"]);
            }
            DbFunctions.CloseConnection();
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            try
            {
                int id = 1;
                bk.data_source = comboBoxServer.Text;
                bk.user_id = textBoxUid.Text;
                bk.password = textBoxPassword.Text;
                bk.db = comboBoxDatabaseList.Text;
                bk.path = txtDirectory.Text;
                bk.Update_backup(id);
                CreateFolder();
                MessageBox.Show("path successfully assigned to " + txtDirectory.Text.ToString());

            }
            catch (Exception ec)
            {
                MessageBox.Show(ec.Message);

            }
        }
        public void CreateFolder()
        {
            string[] days = new string[] { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
            for (int i = 0; i <= 6; i++)
            {
                string a = "\\sysbizz\\backup\\" + days[i];
                string path = txtDirectory.Text + a; // or whatever  
                if (!Directory.Exists(@path))
                {
                    Directory.CreateDirectory(@path);
                    //MessageBox.Show("ohoo");
                }
            }
        }

        public void GetAllServer()
        {
            DataTable dt = SmoApplication.EnumAvailableSqlServers(false);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    comboBoxServer.Items.Add((dr["Name"]));
                }
            }

            if (comboBoxServer.Items.Count > 0)
            {
                comboBoxServer.SelectedIndex = 0;
            }

            buttonConnectDb.PerformClick();
            if (comboBoxDatabaseList.Items.Count > 0)
            {
                comboBoxDatabaseList.SelectedIndex = 0;
            }
        }

        public void GetAllDataBase()
        {
            try
            {
                System.Data.SqlClient.SqlConnection SqlCon = new System.Data.SqlClient.SqlConnection("server=" + comboBoxServer.SelectedItem.ToString() + ";uid=" + textBoxUid.Text + ";pwd=" + textBoxPassword.Text + ";");
                SqlCon.Open();
                System.Data.SqlClient.SqlCommand SqlCom = new System.Data.SqlClient.SqlCommand();
                SqlCom.Connection = SqlCon;
                SqlCom.CommandType = CommandType.StoredProcedure;
                SqlCom.CommandText = "sp_databases";
                System.Data.SqlClient.SqlDataReader SqlDR;
                SqlDR = SqlCom.ExecuteReader();
                while (SqlDR.Read())
                {
                    comboBoxDatabaseList.Items.Add(SqlDR.GetString(0));
                }
            }
            catch (Exception ee)
            {

                MessageBox.Show(ee.Message);
            }

        }

        private void lb_refresh_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            reload();
            GetAllServer();
        }

        private void lb_advanced_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Advance_Panel.Visible = true;
        }

        private void CHECK_CREDIT_PERIOD_CheckedChanged(object sender, EventArgs e)
        {
            //if (CHECK_CREDIT_PERIOD.Checked == true)
            //{
            //    GetGeneralDetails();
            //}
            if (CHECK_CREDIT_PERIOD.Checked == false)
            {
                txt_credit_period.Text = null;
            }
        }

        private void check_debit_period_CheckedChanged(object sender, EventArgs e)
        {
            //if (check_debit_period.Checked == true)
            //{
            //    GetGeneralDetails();
            //}
            if (check_debit_period.Checked == false)
            {
                txt_debit_period.Text = null;
            }
        }

        void saleType()
        {
            DataTable dt = new DataTable();
            query = "SELECT CODE AS [key], DESC_ENG AS value FROM GEN_SALE_TYPE";
            dt = DbFunctions.GetDataTable(query);
            //query.CommandType = CommandType.Text;
            //SqlDataAdapter adapter = new SqlDataAdapter(query);
            //adapter.Fill(dt);
            DataRow row = dt.NewRow();
            row[1] = "---------ALL---------";
            dt.Rows.InsertAt(row, 0);
            cmbInvType.DisplayMember = "value";
            cmbInvType.ValueMember = "key";
            cmbInvType.DataSource = dt;
        }
        void GetVouchStart()
        {
            //conn.Open();
            query = "SELECT VouchStartFrom FROM GEN_VOUCH_STARTFROM ";
            string n = Convert.ToString(DbFunctions.GetAValue(query));
            VOUCHNUM.Text = n;
        }
        
        private void StartFrom()
        {            
            cmbInvType.SelectedIndexChanged += cmbInvType_SelectedIndexChanged;
            if (dt1Inv.Rows.Count > 0)
            {
                if (dtInv.Rows.Count > 1)
                {
                    query = "DELETE FROM GEN_INV_STARTFROM";
                    DbFunctions.InsertUpdate(query);
                    for (int i = 0; i < dtInv.Rows.Count; i++)
                    {
                        query = "INSERT INTO GEN_INV_STARTFROM VALUES('" + dtInv.Rows[i][0].ToString() + "','" + txtInvNo.Text + "')";
                        DbFunctions.InsertUpdate(query);

                    }

                }
                else
                {
                    query = "UPDATE GEN_INV_STARTFROM SET InvoiceStart = '" + txtInvNo.Text + "' WHERE InvoiceTypeCode = '" + dtInv.Rows[0][0].ToString() + "'";
                    DbFunctions.InsertUpdate(query);
                }
            }
            else
            {
                for (int i = 0; i < dtInv.Rows.Count; i++)
                {
                    query = "INSERT INTO GEN_INV_STARTFROM VALUES('" + dtInv.Rows[i][0].ToString() + "','" + txtInvNo.Text + "')";
                    DbFunctions.InsertUpdate(query);

                }

            }
            cmbVouch.SelectedIndexChanged += cmbVouch_SelectedIndexChanged;
            
            if (dt1Vouch.Rows.Count > 0)
            {
                if (dtVouch.Rows.Count > 1)
                {
                    query = "DELETE FROM GEN_VOUCH_STARTFROM";
                    DbFunctions.InsertUpdate(query);
                    for (int i = 0; i < dtVouch.Rows.Count; i++)
                    {
                        query = "INSERT INTO GEN_VOUCH_STARTFROM VALUES('" + dtVouch.Rows[i][0].ToString() + "','" + VOUCHNUM.Text + "')";
                        DbFunctions.InsertUpdate(query);

                    }

                }
                else
                {
                    query = "UPDATE GEN_VOUCH_STARTFROM SET VouchStartFrom = '" + VOUCHNUM.Text + "' WHERE VouchTypeCode = '" + dtVouch.Rows[0][0].ToString() + "'";
                    DbFunctions.InsertUpdate(query);
                }
            }
            else
            {
                for (int i = 0; i < dtVouch.Rows.Count; i++)
                {
                    query = "INSERT INTO GEN_VOUCH_STARTFROM VALUES('" + dtVouch.Rows[i][0].ToString() + "','" + VOUCHNUM.Text + "')";
                    DbFunctions.InsertUpdate(query);

                }

            }
            
        }

        private void cmbInvType_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtInv.Clear();
            dt1Inv.Clear();
            query = "SELECT InvoiceTypeCode FROM GEN_INV_STARTFROM";
            //query.CommandType = CommandType.Text;
            //SqlDataAdapter adapter1 = new SqlDataAdapter(query);
            //adapter1.Fill(dt1Inv);
            dt1Inv = DbFunctions.GetDataTable(query);
            if (dt1Inv.Rows.Count > 0)
            {
                if (cmbInvType.SelectedIndex != 0)
                {
                    query = "SELECT CODE FROM GEN_SALE_TYPE WHERE DESC_ENG = '" + cmbInvType.Text + "'";
                    dtInv = DbFunctions.GetDataTable(query);
                }
                else
                {
                    query = "SELECT CODE FROM GEN_SALE_TYPE";
                    dtInv = DbFunctions.GetDataTable(query);
                }
            }
            else
            {
                query = "SELECT CODE FROM GEN_SALE_TYPE";
                dtInv = DbFunctions.GetDataTable(query);
            }           
            //conn.Open();
            query = "SELECT InvoiceStart FROM GEN_INV_STARTFROM WHERE InvoiceTypeCode='" + cmbInvType.SelectedValue + "'";
            string n = Convert.ToString(DbFunctions.GetAValue(query));
            //conn.Close();
            txtInvNo.Text = n;            
        }

        

        private void Check_Click(object sender, EventArgs e)
        {

        }

        private void rbtnAllowDiscNo_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbtTaxYes_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbtnQtyYes_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbtnUnitNo_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbtnQtyNo_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbtfocuscustomerNo_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbtnSaleYes_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbtnSalesManYes_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbtnSaleNo_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbtnSalesManNo_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbtnRateNo_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbtnAllowdiscYes_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void RbtnPriceNo_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void RbtnPriceYes_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbtnUnitYes_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbtfocusdateno_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbtfocuscustomerYes_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbtTaxNo_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbtnRateYes_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbtfocusdateyes_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void kryptonLabel87_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnCloseSales_Click(object sender, EventArgs e)
        {
            ///this.Close();
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

        private void cmbVouch_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtVouch.Clear();
            dt1Vouch.Clear();
            query = "SELECT VouchTypeCode FROM GEN_VOUCH_STARTFROM";
            //query.CommandType = CommandType.Text;
            //SqlDataAdapter adapter1 = new SqlDataAdapter(query);
            dt1Vouch=DbFunctions.GetDataTable(query);
            if (dt1Vouch.Rows.Count > 0)
            {
                if (cmbVouch.SelectedIndex != 0)
                {
                    query = "SELECT CODE FROM GEN_VOUCH_TYPE WHERE DESC_ENG = '" + cmbVouch.Text + "'";
                    dtVouch=DbFunctions.GetDataTable(query);
                }
                else
                {
                    query = "SELECT CODE FROM GEN_VOUCH_TYPE";
                    dtVouch = DbFunctions.GetDataTable(query);
                }
            }
            else
            {
                query = "SELECT CODE FROM GEN_VOUCH_TYPE";
                dtVouch = DbFunctions.GetDataTable(query);
            }
            //conn.Open();
            query = "SELECT VouchStartFrom FROM GEN_VOUCH_STARTFROM WHERE VouchTypeCode='" + cmbVouch.SelectedValue.ToString() + "'";
            string n = Convert.ToString(DbFunctions.GetAValue(query));
            //conn.Close();
            VOUCHNUM.Text = n;            
        }

        private void cmbStartVoucher_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbStartVoucher.SelectedIndex > 0)
            {
                //conn.Open();
                query = "SELECT VoucherStart FROM GEN_PUR_STARTFROM WHERE VoucherTypeCode='" +cmbStartVoucher.SelectedValue.ToString() + "'";
                txtVoucherStart.Text = Convert.ToString(DbFunctions.GetAValue(query));
                //conn.Close();
            }
        }

        private void btnSaveDynamic_Click(object sender, EventArgs e)
        {
            //conn.Open();
            query = "UPDATE INVOICE_SETTINGS SET ISDYNAMIC=@isdynamic,ActiveInvoice=@Inv_template";
            Dictionary<string, object> Parameter = new Dictionary<string, object>();
            Parameter.Add("@isdynamic",DPyes.Checked);
            Parameter.Add("@Inv_template",cmbTemplate.Text);

            DbFunctions.InsertUpdate(query,Parameter);
            MessageBox.Show("Settings Saved Successfully", "Sysbizz");
            //conn.Close();
        }

        private void Save_NoSeris_Click(object sender, EventArgs e)
        {
            
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            InvTemplate frm = new InvTemplate();
           
            frm.Show();
        }
        void loadTemplate()
        {
            try
            {
                //conn.Open();
                query = "SELECT TEMPLATE FROM INVOICE_A4_GENERAL";
                DataTable dt = new DataTable();
                //SqlDataAdapter adapter = new SqlDataAdapter(query);
                //adapter.Fill(dt);
                dt=DbFunctions.GetDataTable(query);
                //conn.Close();

                cmbTemplate.DataSource = dt;
                DataRow row = dt.NewRow();
                dt.Rows.InsertAt(row, 0);
                cmbTemplate.DisplayMember = "TEMPLATE";
                cmbTemplate.ValueMember = "TEMPLATE";
            }
            catch
            {
            }
        }
        bool loadTempSettings()
        {
            bool val = false;
            try
            {
                //conn.Open();
                query = "SELECT IsDynamic FROM INVOICE_SETTINGS";
                val = Convert.ToBoolean(DbFunctions.GetAValue(query));
                //conn.Close();
            }
            catch
            {
            }
            return val;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
