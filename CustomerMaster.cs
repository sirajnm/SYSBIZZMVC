using ComponentFactory.Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sys_Sols_Inventory.Model;
namespace Sys_Sols_Inventory
{
    public partial class CustomerMaster : Form
    {
        #region private properties declaration
        //private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        //private SqlCommand cmd = new SqlCommand();
        //private SqlDataAdapter adapterType = new SqlDataAdapter();
        private DataTable tableType = new DataTable();
        private bool HasArabic = true;
        private DataTable tableFilter = new DataTable();
        Class.CollectionDay colday = new Class.CollectionDay();
        DataTable dt = new DataTable();
        //private SqlDataAdapter adapterCustomers = new SqlDataAdapter();
        private DataTable tableCustomers = new DataTable();
        private DataTable tableCustomers1 = new DataTable();
        private BindingSource source = new BindingSource();
        private string UpdateLedgerId = "";
        Class.Ledgers led = new Class.Ledgers();
        Class.Transactions trans = new Class.Transactions();
        Class.CompanySetup Comp = new Class.CompanySetup();
        clsCustomer cust = new clsCustomer();
        clsCommon clsCommon = new clsCommon();
        private string ID = "";
        public string lid = "";
        #endregion
        Login lg = (Login)Application.OpenForms["Login"];
        SqlDataAdapter adapter = new SqlDataAdapter();
        public int flag = 0;
        public CustomerMaster()
        {
            InitializeComponent();
            HasArabic = General.IsEnabled(Settings.Arabic);

            //cmd.Connection = conn;
            //adapterType.SelectCommand = cmd;
            //adapterCustomers.SelectCommand = cmd;
            TYPE.Focus();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Alt | Keys.S))
            {
                if (DialogResult.Yes == MessageBox.Show("Are sure to continue", "Confirmation", MessageBoxButtons.YesNo))
                {

                    btnSave.PerformClick();
                    // EditActive = false;
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public void GetBranchDetails()
        {
            dt = Comp.GetCurrentBranchDetails();
            if (dt.Rows.Count > 0)
            {
                DEFAULT_CURRENCY.SelectedValue = dt.Rows[0]["DEFAULT_CURRENCY_CODE"];
            }
        }
        
        public DataTable getCus()
        {
            //SqlDataAdapter adapter = new SqlDataAdapter();
            //cmd.CommandText = "SELECT CODE,TYPE,DESC_ENG,ADDRESS_A,ADDRESS_B,POBOX,TELE1,TELE2,MOBILE,EMAIL,CITY_CODE,COUNTRY_CODE,FAX,TIN_NO,OPENING_BAL,DATE_GRE,DEFAULT_CURRENCY,SALESMAN_CODE,LedgerId,CreditActive,NOTES,CUSTOMER_STATUS,CREDIT_AMOUNT,CperiodActive,CREDIT_PERIOD,[STATE] AS CUSTOMER_STATE FROM REC_CUSTOMER order by REC_CUSTOMER.code asc";
            //cmd.Connection = conn;
            //adapter.SelectCommand = cmd;
            //adapter.Fill(tableCustomers1);
            tableCustomers1 = cust.getCustomer();
            return tableCustomers1;
        }

        public void getdata()
        {
            //SqlDataAdapter adapter = new SqlDataAdapter();
            ////DataTable tableCustomers = new DataTable();
            //cmd.CommandText = "SELECT CODE,TYPE,DESC_ENG,ADDRESS_A,ADDRESS_B,POBOX,TELE1,TELE2,MOBILE,EMAIL,CITY_CODE,COUNTRY_CODE,FAX,TIN_NO,OPENING_BAL,DATE_GRE,DEFAULT_CURRENCY,SALESMAN_CODE,LedgerId,CreditActive,NOTES,CUSTOMER_STATUS,CREDIT_AMOUNT,CperiodActive,CREDIT_PERIOD,[STATE] AS CUSTOMER_STATE FROM REC_CUSTOMER order by REC_CUSTOMER.code asc";
            //cmd.Connection = conn;
            //adapter.SelectCommand = cmd;
            //adapter.Fill(tableCustomers);
           tableCustomers= cust.getdata();
           source.DataSource = tableCustomers;
            dgCustomers.DataSource = source;
        }

        private void CustomerMaster_Load(object sender, EventArgs e)
        {
            countryDetails();
            Class.CompanySetup CompStep = new Class.CompanySetup();
            DATE_GRE.Text = CompStep.GettDate();
            
            ActiveControl = DESC_ENG;
            OPENING_BAL.KeyPress += new KeyPressEventHandler(General.NegFloat);

            //cmd.CommandText = "SELECT CODE,CODE+' - '+DESC_ENG AS DESC_ENG FROM REC_CUSTOMER_TYPE";
            //adapterType.Fill(tableType);
           // TYPE.DataSource = tableType;
            TYPE.DataSource = cust.getCustType();
            TYPE.DisplayMember = "DESC_ENG";
            TYPE.ValueMember = "CODE";
            getdata();            
            tableFilter.Columns.Add("key");
            tableFilter.Columns.Add("value");
            cmbFilter.DataSource = tableFilter;
            cmbFilter.DisplayMember = "value";
            cmbFilter.ValueMember = "key";
            tableFilter.Rows.Add("CODE", "Code");
            tableFilter.Rows.Add("TYPE", "Type Code");
            tableFilter.Rows.Add("DESC_ENG", "Eng. Name");
            if (HasArabic)
            {
                tableFilter.Rows.Add("DESC_ARB", "Arb. Name");
            }
            tableFilter.Rows.Add("ADDRESS_A", "Address Line 1");
           // tableFilter.Rows.Add("ADDRESS_B", "Address Line 2");
           // tableFilter.Rows.Add("POBOX", "Address Line 3");
            tableFilter.Rows.Add("TELE1", "Telephone 1");
           // tableFilter.Rows.Add("TELE2", "Telephone 2");
            tableFilter.Rows.Add("MOBILE", "Mobile");
            tableFilter.Rows.Add("CITY_CODE", "Email");
            tableFilter.Rows.Add("EMAIL", "City Code");
            tableFilter.Rows.Add("COUNTRY_CODE", "Country Code");
            tableFilter.Rows.Add("FAX", "Fax");
            OPENTYPE.SelectedIndex = 0;

          //  DataTable tableCurrency = new DataTable();
           // cmd.CommandText = "SELECT CODE,CODE+' - '+DESC_ENG AS DESC_ENG FROM GEN_CURRENCY";
            //adapterType.Fill(tableCurrency);
            DEFAULT_CURRENCY.DataSource = cust.getCurrency();
            DEFAULT_CURRENCY.DisplayMember = "DESC_ENG";
            DEFAULT_CURRENCY.ValueMember = "CODE";
            if (!HasArabic)
            {
                PnlArabic.Visible = false;

            }
            //cmb_state.DataSource =loadStates();
            //cmb_state.DisplayMember = "NAME";
            //cmb_state.ValueMember = "CODE";
            loadStates();

            cmb_salesman.DataSource = get_salesman();
            cmb_salesman.DisplayMember = "name";
            cmb_salesman.ValueMember = "EMPID";


            CODE.Text = generateItemCode();
            GetBranchDetails();
            ActiveControl = DESC_ENG;
            getGeneralSetup();
            CITY_CODE.KeyDown += TYPE_KeyDown;
            cmbFilter.SelectedValue = "DESC_ENG";
            bindSupplier();
            load_CusAsSup();
            this.ActiveControl = DESC_ENG;
            OPENTYPE.Text = "DR";

        }
        void countryDetails()
        {
            DataTable country = General.GetCountryDetails();
            if (country.Rows.Count > 0 && (country.Rows[0][0].ToString() != "" || country.Rows[0][1].ToString() != ""))
            {
                
                int a = lblState.Size.Width;
                string statelbl = country.Rows[0][0].ToString();
                string tinNum = country.Rows[0][1].ToString();
                lblState.Text = (!statelbl.Equals("")) ?"City/"+statelbl.First().ToString().ToUpper() + statelbl.Substring(1).ToLower() + ":" : lblState.Text;
                LB_TINNO.Text = (!tinNum.Equals("")) ? tinNum+":" : LB_TINNO.Text;
            
                if (lblState.Size.Width > 67)
                {
                    int addedWidth = lblState.Size.Width - 67;
                    lblState.Location = new Point(lblState.Location.X - addedWidth, lblState.Location.Y);
                }
                //Graphics e = this.CreateGraphics();
                //SizeF size = e.MeasureString(lblState.Text, lblState.Font);
                //int width = (int)size.Width;
                //width = width + width * 15 / 100;
                //MessageBox.Show(size.Width.ToString());
                
            }
        }
        void load_CusAsSup()
        {
            bool value = false;
            try
            {
                value = clsCommon.IsCusSup();            
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            if (value)
            {
                chk_cust.Visible = true;
                cmb_Customer.Visible = true;
            }
            else
            {
                chk_cust.Visible = false;
                cmb_Customer.Visible = false;
            }
        }
        private DataTable get_salesman()
        {

            //SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
            //SqlCommand cmd = new SqlCommand();
            //cmd.Connection = conn;
            //SqlDataAdapter adapter = new SqlDataAdapter();
            //adapter.SelectCommand = cmd;
            //cmd.CommandText = "SELECT EMPID,CONCAT(EMP_FNAME,' ',EMP_MNAME,' ',EMP_LNAME)as name from EMP_EMPLOYEES WHERE EMP_DESIG=21";
            DataTable table = cust.getSalesMan();
            //adapter.Fill(table);
            DataRow r = table.NewRow();
            table.Rows.InsertAt(r, 0);
            return table;
           
         
        }
        public DataTable loadStates()
        {
            DataTable table = Common.loadStates();
            DataRow row = table.NewRow();
            row[1] = "-----SELECT STATE-----";
            table.Rows.InsertAt(row, 0);
            cmb_state.DataSource = table;
            cmb_state.DisplayMember = "NAME";
            cmb_state.ValueMember = "CODE";
            if (lg.state != null && !lg.state.Equals(""))
            {
                cmb_state.SelectedValue = lg.state;
            }
            else
            {
                cmb_state.SelectedIndex = 0;
            }
            return table;
        }
        public DataTable getGeneralSetup()
         {
             DataTable dt = cust.getGenSetup();
             //cmd.CommandText = "SELECT ACTIVE_PERIOD,CREDIT_PERIOD,ACTIVE_DEBIT_PERIOD,DEBIT_PERIOD  FROM   SYS_SETUP";
             //cmd.Connection = conn;
             //adapter.SelectCommand = cmd;
             //adapter.Fill(dt);
             if (dt.Rows.Count > 0)
             {
                 if (dt.Rows[0]["ACTIVE_PERIOD"].ToString() == "True")
                 {
                     CHECK_CREDIT_PERIOD.Checked = true;
                     TXT_CREDIT_PERIOD.Text = dt.Rows[0]["CREDIT_PERIOD"].ToString();
                 }
                 else
                 {
                     CHECK_CREDIT_PERIOD.Checked = false;
                 }
             }
             return dt;
         }

        private bool valid()
        {
            if (CODE.Text == "" || DESC_ENG.Text == "" || TYPE.SelectedValue == null)
            {
                MessageBox.Show("Enter the following details\n 1.Code\n 2.English Name \n 3.Customer Type");
                CODE.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }

        string NOTES = "";
        public string CREATENOTES()
        {
            try
            {
                return NOTES = BANK.Text + "-" + BANKBRANCH.Text + "-" + ACCOUNTNO.Text + "-" + IFCCODE.Text;
            }
            catch
            {
              return  NOTES;
            }
        }
        
        public string generateItemCode()
        {
            //cmd.Connection = conn;
            //conn.Open();
            //cmd.CommandType = CommandType.Text;
            //cmd.CommandText = "SELECT MAX(CODE) FROM REC_CUSTOMER";
            string id = cust.getCustId();
            if (id == "")
            {
                id = "00001";
            }
            else
            {
                id = (Convert.ToUInt64(id) + 1).ToString();
                if (id.Length == 1)
                {
                    id = "0000" + id;
                }
                else if (id.Length == 2)
                {
                    id = "000" + id;
                }
                else if (id.Length == 3)
                {
                    id = "00" + id;
                }
                else if (id.Length == 4)
                {
                    if (Convert.ToInt32(id) > 999)
                    {

                    }
                    else
                    {
                        id = "0" + id;
                    }
                }
            }
            //conn.Close();
            return id;
        }

        string cmonth_day = "";
        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        public void addcollection()
        {
            if (Drp_repeat.Text != "None")
            {
                colday.SupId = CODE.Text;
                colday.Repeat = Drp_repeat.Text;

                switch (Drp_repeat.Text)
                {
                    case "Weekly":

                        if (drp_repeatModeDay.Text == "")
                        {
                            colday.RepeatMode = "1";
                        }
                        else
                        {
                            colday.RepeatMode = drp_repeatModeDay.Text;
                        }
                        if (DrpDay.Text == "")
                        {
                            colday.Day = "Monday";
                        }
                        else
                        {
                            colday.Day = DrpDay.Text;
                        }
                        colday.Date = DateTime.Now.ToString("MM/dd/yyyy");
                        break;
                    case "Monthly":
                        if (Drop_repeatmodeMonth.Text == "")
                        {
                            colday.RepeatMode = "1";
                        }
                        else
                        {
                            colday.RepeatMode = Drop_repeatmodeMonth.Text;
                        }

                        colday.Date = RepeatDate.Value.ToString("MM/dd/yyyy");

                        colday.Day = "Monday";
                        break;

                    default:

                        colday.Day = "0";
                        colday.Date = DateTime.Now.ToString("MM/dd/yyyy");
                        colday.Repeat = "None";
                        break;
                }
                colday.AddCollectionCus();
            }
        }
        
        public int GetMaxLedger()
        {
            return led.MaxLedGerid();
        }

        public int CheckLedger(string s)
        {
            int ret = 0;
            if (chk_cust.Checked)
            {
                return 0;
            }
            DataTable dtt = new DataTable();
            dtt = led.Selectledger();
            for (int c = 0; c < dtt.Rows.Count; ++c)
            {
                if (dtt.Rows[c]["LEDGERNAME"].ToString() == s)
                {
                    //  MessageBox.Show("Same LedgerName Exist Try With Another name");
                    ret = 1;
                }
            }
            return ret;
        }

        public String addLedger()
        {
            led.LEDGERNAME = DESC_ENG.Text;
            led.UNDER = "14";
            led.ADRESS = ADDRESS_A.Text + "," + ADDRESS_B.Text + "," + CITY_NAME.Text + "" + COUNTRY_NAME.Text;
            led.TIN = "";
            led.CST = "";
            led.PIN = "";
            led.PHONE = TELE1.Text;
            led.MOBILE = MOBILE.Text;
            led.EMAIL = EMAIL.Text;
            led.CREDITPERIOD = "";
            led.CREDITAMOUNT = "";
            led.DISPLAYNAME = DESC_ENG.Text;
            led.ISBUILTIN = "N";
            led.BANK = BANK.Text;
            led.BANKBRANCH = BANKBRANCH.Text;
            led.IFCCODE = IFCCODE.Text;
            led.ACCOUNTNO = ACCOUNTNO.Text;
            return led.insertLedger();
        }

        public String updateLedger()
        {
            led.LEDGERID = Convert.ToInt32(UpdateLedgerId);
            led.LEDGERNAME = DESC_ENG.Text;
            led.UNDER = "14";
            led.ADRESS = ADDRESS_A.Text + "," + ADDRESS_B.Text + "," + CITY_NAME.Text + "" + COUNTRY_NAME.Text;
            led.TIN = "";
            led.CST = "";
            led.PIN = "";
            led.PHONE = TELE1.Text;
            led.MOBILE = MOBILE.Text;
            led.EMAIL = EMAIL.Text;
            led.CREDITPERIOD = "";
            led.CREDITAMOUNT = "";
            led.DISPLAYNAME = DESC_ENG.Text;
            led.ISBUILTIN = "N";
            led.BANK = BANK.Text;
            led.BANKBRANCH = BANKBRANCH.Text;
            led.IFCCODE = IFCCODE.Text;
            led.ACCOUNTNO = ACCOUNTNO.Text;
            led.UpdateLedger();
            return UpdateLedgerId;
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            if (valid())
            {

                if(ID=="")
                {
                    if (CheckLedger(DESC_ENG.Text) == 1)
                    {
                        MessageBox.Show("Try With Another Name");
                        DESC_ENG.Focus();
                        return;

                    }
                }


                    String saveLedgerID = null;
                    String defaultCurrency = Convert.ToString(DEFAULT_CURRENCY.SelectedValue);
                    String typeCode = Convert.ToString(TYPE.SelectedValue);
                    String openingBalance = (OPENING_BAL.Text.Trim() != "") ? OPENING_BAL.Text : "0";
                    String creditAcive = IN_ACTIVE.Checked.ToString();
                    String active_customer = Active_Customer.Checked.ToString();
                    String salesman = cmb_salesman.SelectedIndex > 0 ? cmb_salesman.SelectedValue.ToString() : "";
                    String STATE = cmb_state.SelectedIndex > 0 ? cmb_state.SelectedValue.ToString() : "";
                    String credit_period_limit = CHECK_CREDIT_PERIOD.Checked.ToString();
                   

                    cust.DefaultCurrecy = Convert.ToString(DEFAULT_CURRENCY.SelectedValue);
                    cust.Type = Convert.ToString(TYPE.SelectedValue);
                    cust.OpenBalance = (OPENING_BAL.Text.Trim() != "") ?Convert.ToDecimal (OPENING_BAL.Text) : 0;
                    cust.CreditActve = IN_ACTIVE.Checked.ToString();
                    cust.CustomerStatus = Active_Customer.Checked.ToString();
                    cust.SalesmanCode = cmb_salesman.SelectedIndex > 0 ? cmb_salesman.SelectedValue.ToString() : "";
                    cust.State = cmb_state.SelectedIndex > 0 ? cmb_state.SelectedValue.ToString() : "";
                    cust.CPeriodActive = CHECK_CREDIT_PERIOD.Checked.ToString();

                    if (!IN_ACTIVE.Checked)
                    {
                        TXT_CREDIT_AMOUNT.Text = "0.00";
                        cust.CreditAmount = "0.00";
                    }
                    if (CHECK_CREDIT_PERIOD.Checked)
                    {
                        TXT_CREDIT_PERIOD.Text = TXT_CREDIT_PERIOD.Text;
                        cust.CreditPeriod = TXT_CREDIT_PERIOD.Text;
                    }
                    else
                    {
                        TXT_CREDIT_PERIOD.Text = "0";
                        cust.CreditPeriod = "0";
                    }
                    if (ID.Equals(""))
                    {
                        if (!chk_cust.Checked)
                        {
                            saveLedgerID = addLedger();
                            cust.LedgerId = saveLedgerID;
                        }
                        else
                        {
                            saveLedgerID = cmb_Customer.SelectedValue.ToString();
                            cust.LedgerId = cmb_Customer.SelectedValue.ToString();
                        }
                        
                    }
                    else
                    {
                        if (!chk_cust.Checked)
                        {
                            saveLedgerID = updateLedger();
                            cust.LedgerId = saveLedgerID;
                        }
                    }

                    cust.Code = CODE.Text;
                    cust.DescEng = DESC_ENG.Text;
                    cust.DescArb = DESC_ARB.Text;
                    cust.AddressA = ADDRESS_A.Text;
                    cust.AddressB = ADDRESS_B.Text;
                    cust.Pobox = POBOX.Text;
                    cust.Tele = TELE1.Text;
                    cust.Tele2 = TELE2.Text;
                    cust.Mobile = MOBILE.Text;
                    cust.Fax = FAX.Text;
                    cust.Email = EMAIL.Text;
                    cust.City = CITY_CODE.Text;
                    cust.CountryCode = COUNTRY_CODE.Text;
                    cust.DateGre = DATE_GRE.Value;
                    cust.Note = CREATENOTES();
                    cust.TinNo = TINNO.Text;
                    cust.CreditAmount = TXT_CREDIT_AMOUNT.Text;
                    //cmd.Parameters.Clear();
                    //cmd.Parameters.AddWithValue("@type", typeCode);
                    //cmd.Parameters.AddWithValue("@code", CODE.Text);
                    //cmd.Parameters.AddWithValue("@desc_eng", DESC_ENG.Text);
                    //cmd.Parameters.AddWithValue("@desc_arb", DESC_ARB.Text);
                    //cmd.Parameters.AddWithValue("@address_a", ADDRESS_A.Text);
                    //cmd.Parameters.AddWithValue("@address_b", ADDRESS_B.Text);
                    //cmd.Parameters.AddWithValue("@pobox", POBOX.Text);
                    //cmd.Parameters.AddWithValue("@tele1", TELE1.Text);
                    //cmd.Parameters.AddWithValue("@tele2", TELE2.Text);
                    //cmd.Parameters.AddWithValue("@mobile", MOBILE.Text);
                    //cmd.Parameters.AddWithValue("@fax", FAX.Text);
                    //cmd.Parameters.AddWithValue("@email", EMAIL.Text);
                    //cmd.Parameters.AddWithValue("@city_code", CITY_CODE.Text);
                    //cmd.Parameters.AddWithValue("@country_code", COUNTRY_CODE.Text);
                    //cmd.Parameters.AddWithValue("@opening_bal", openingBalance);
                    //cmd.Parameters.AddWithValue("@date_gre", DATE_GRE.Value.ToString("yyyy/MM/dd"));
                    //cmd.Parameters.AddWithValue("@salesman_code", salesman);
                    //cmd.Parameters.AddWithValue("@default_currency", defaultCurrency);
                    //cmd.Parameters.AddWithValue("@ledgerid", saveLedgerID);
                    //cmd.Parameters.AddWithValue("@creditactive", creditAcive);
                    //cmd.Parameters.AddWithValue("@notes", CREATENOTES());
                    //cmd.Parameters.AddWithValue("@tin_no", TINNO.Text);
                    //cmd.Parameters.AddWithValue("@customer_status", active_customer);
                    //cmd.Parameters.AddWithValue("@credit_amount", TXT_CREDIT_AMOUNT.Text);
                    //cmd.Parameters.AddWithValue("@cperiodactive", credit_period_limit);
                    //cmd.Parameters.AddWithValue("@credit_period", TXT_CREDIT_PERIOD.Text);
                    //cmd.Parameters.AddWithValue("@STATE", STATE);

                    if (ID.Equals(""))
                    {
                        #region insert logic

                        //cmd.CommandText = "INSERT INTO REC_CUSTOMER(TYPE,CODE,DESC_ENG,DESC_ARB,ADDRESS_A,ADDRESS_B,POBOX," +
                        //    "TELE1,TELE2,MOBILE,FAX,EMAIL,CITY_CODE,COUNTRY_CODE,OPENING_BAL,DATE_GRE,SALESMAN_CODE," +
                        //    "DEFAULT_CURRENCY,LedgerId,CreditActive,NOTES,TIN_NO,CUSTOMER_STATUS,CREDIT_AMOUNT," +
                        //    "CperiodActive,CREDIT_PERIOD,STATE)";
                        //cmd.CommandText += "VALUES(@type, @code, @desc_eng, @desc_arb, @address_a, @address_b, @pobox, " +
                        //    "@tele1, @tele2, @mobile, @fax, @email, @city_code, @country_code, @opening_bal, @date_gre, " +
                        //    "@salesman_code, @default_currency, @ledgerid, @creditactive, @notes, @tin_no, @customer_status, " +
                        //    "@credit_amount, @cperiodactive, @credit_period,@STATE)";
                        //conn.Open();
                        //cmd.ExecuteNonQuery();
                        //conn.Close();
                        //cmd.Parameters.Clear();
                        cust.add();
                        if (!chk_cust.Checked)
                        {
                            insertTransaction(saveLedgerID);
                        }
                        #endregion
                    }
                    else
                    {
                        #region update logic
                        //cmd.CommandText = "UPDATE REC_CUSTOMER SET TYPE=@type,DESC_ENG=@desc_eng, DESC_ARB=@desc_arb, ADDRESS_A=@address_a, " +
                        //"ADDRESS_B=@address_b, POBOX=@pobox,TELE1=@tele1,TELE2=@tele2,MOBILE=@mobile,FAX=@fax,EMAIL=@email,CITY_CODE=@city_code, " +
                        //"COUNTRY_CODE=@country_code,OPENING_BAL=@opening_bal,DATE_GRE=@date_gre,SALESMAN_CODE=@salesman_code, " +
                        //"DEFAULT_CURRENCY=@default_currency,LedgerId=@ledgerid,CreditActive=@creditactive,NOTES=@notes,TIN_NO=@tin_no," +
                        //"CUSTOMER_STATUS=@customer_status,CREDIT_AMOUNT=@credit_amount,CperiodActive=@cperiodactive,CREDIT_PERIOD=@credit_period,STATE=@STATE" +
                        //" WHERE CODE = @code";
                        //conn.Open();
                        //cmd.ExecuteNonQuery();
                        //conn.Close();
                        cust.Updste();
                        updateSupplier();
                        //cmd.Parameters.Clear();
                        if (!chk_cust.Checked)
                        {
                            DeleteTrans(saveLedgerID);
                            insertTransaction(saveLedgerID);
                        }
                        #endregion
                    }
              
                DataRow row = null;
                if (ID.Equals(""))
                {
                    tableCustomers1.Clear();
                    dgCustomers.DataSource = null;
                    dgCustomers.Refresh();
                    tableCustomers1 = getCus();
                    dgCustomers.DataSource = tableCustomers1;
                    tableCustomers = tableCustomers1;
                    MessageBox.Show("Customer Added!");
                    
                }
                else
                {
                    row = tableCustomers.Select("CODE = '" + ID + "'").First();
                    row["TYPE"] = typeCode;
                    row["CODE"] = CODE.Text;
                    row["DESC_ENG"] = DESC_ENG.Text;
                    if (HasArabic)
                    {
                        row["DESC_ARB"] = DESC_ARB.Text;
                    }
                    row["ADDRESS_A"] = ADDRESS_A.Text;
                    row["ADDRESS_B"] = ADDRESS_B.Text;
                    row["POBOX"] = POBOX.Text;
                    row["TELE1"] = TELE1.Text;
                    row["TELE2"] = TELE2.Text;
                    row["MOBILE"] = MOBILE.Text;
                    row["FAX"] = FAX.Text;
                    row["EMAIL"] = EMAIL.Text;
                    row["CITY_CODE"] = CITY_CODE.Text;
                    row["COUNTRY_CODE"] = COUNTRY_CODE.Text;
                    row["OPENING_BAL"] = OPENING_BAL.Text;
                    row["DATE_GRE"] = DATE_GRE.Value;
                    row["SALESMAN_CODE"] = cmb_salesman.SelectedValue.ToString();
                    row["DEFAULT_CURRENCY"] = defaultCurrency;
                    row["CreditActive"] = creditAcive;
                    row["NOTES"] = NOTES;
                    row["TIN_NO"] = TINNO.Text;
                    row["CUSTOMER_STATUS"] = active_customer;
                    row["CREDIT_AMOUNT"] = TXT_CREDIT_AMOUNT.Text;
                    row["CperiodActive"] = credit_period_limit;
                    row["CREDIT_PERIOD"] = TXT_CREDIT_PERIOD.Text;
                    row["CUSTOMER_STATE"] = cmb_state.SelectedValue.ToString(); ;
                    MessageBox.Show("Customer Updated!");
                }

              
                colday.SupId = CODE.Text;
                colday.DeleteCollection_cus();
                if (Drp_repeat.Text != "")
                {
                    addcollection();
                }
                
               
                btnClear.PerformClick();
                CODE.Text = generateItemCode();
                getGeneralSetup();
                if (flag == 1)
                {
                    flag = 0;
                    this.Close();
                }
                
            }
        }
        void updateSupplier()
        {
            //cmd.CommandText = "UPDATE PAY_SUPPLIER SET DESC_ENG=@desc_eng, DESC_ARB=@desc_arb, ADDRESS_A=@address_a, " +
            //           "ADDRESS_B=@address_b, POBOX=@pobox,TELE1=@tele1,TELE2=@tele2,MOBILE=@mobile,FAX=@fax,EMAIL=@email,CITY_CODE=@city_code, " +
            //           "COUNTRY_CODE=@country_code,OPENING_BAL=@opening_bal,DATE_GRE=@date_gre," +
            //           "DEFAULT_CURRENCY=@default_currency,TIN_NO=@tin_no," +
            //           "SUPPLIER_STATUS=@customer_status,STATE=@STATE" +
            //           " WHERE LEDGERID = @LedgerId";
            //conn.Open();
            //cmd.ExecuteNonQuery();
            //conn.Close();
            cust.UpdsteSup();
        }
        private void insertTransaction(String ledgerID)
        {
            trans.VOUCHERTYPE = "Opening Balance";
            //trans.DATED = DateTime.Now.ToString("MM/dd/yyyy");
            trans.DATED = DATE_GRE.Value.ToString("MM/dd/yyyy");
            trans.NARRATION = "Opening Balance";
            Login log = (Login)Application.OpenForms["Login"];
            trans.USERID = log.EmpId;

            if (OPENTYPE.Text == "CR")
            {
                trans.ACCNAME = DESC_ENG.Text;
                trans.PARTICULARS = "OPENING BALANCE";
                trans.VOUCHERNO = "";
                if (dt.Rows.Count > 0)
                    trans.ACCID = ledgerID;
                if (OPENING_BAL.Text != "")
                    trans.CREDIT = OPENING_BAL.Text;
                else
                    trans.CREDIT = "0";
                trans.DEBIT = "0";
                trans.BRANCH = lg.Branch;
                trans.SYSTEMTIME = DateTime.Now.ToString();
                trans.insertTransaction();

                /*trans.PARTICULARS = DESC_ENG.Text;
                trans.ACCNAME = "SUSPENSE ACCOUNT";
                trans.VOUCHERNO = "";
                trans.BRANCH = lg.Branch;
                if (dt.Rows.Count > 0)
                    trans.ACCID = "202";
                if (OPENING_BAL.Text != "")
                    trans.DEBIT = OPENING_BAL.Text;
                else
                    trans.DEBIT = "0";
                trans.CREDIT = "0";
                trans.SYSTEMTIEM = DateTime.Now.ToString();

                trans.insertTransaction();*/
            }
            else
            {
                trans.ACCNAME = DESC_ENG.Text;
                trans.PARTICULARS = "OPENING BALANCE";
                trans.VOUCHERNO = "";
                if (dt.Rows.Count > 0)
                    trans.ACCID = ledgerID;

                if (OPENING_BAL.Text != "")
                    trans.DEBIT = OPENING_BAL.Text;
                else
                    trans.DEBIT = "0";
                trans.CREDIT = "0";
                trans.BRANCH = lg.Branch;
                trans.SYSTEMTIME = DateTime.Now.ToString();

                trans.insertTransaction();

                /*trans.PARTICULARS = DESC_ENG.Text;
                trans.ACCNAME = "SUSPENSE ACCOUNT";
                trans.VOUCHERNO = "";
                if (dt.Rows.Count > 0)
                    trans.ACCID = "202";
                //trans.ACCID = dt.Rows[0][0].ToString();
                trans.BRANCH = lg.Branch;
                if (OPENING_BAL.Text != "")
                    trans.CREDIT = OPENING_BAL.Text;
                else
                    trans.CREDIT = "0";
                trans.DEBIT = "0";
                trans.BRANCH = lg.Branch;
                trans.SYSTEMTIEM = DateTime.Now.ToString();
                trans.insertTransaction();*/
            }
        }

        public void DeleteTrans(String ledgerID)
        {
            trans.ACCID = ledgerID;
            trans.VOUCHERTYPE = "Opening Balance";
            trans.DeleteTransaction();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ID = "";
            if (TYPE.Items.Count > 0)
            {
                TYPE.SelectedIndex = 0;
            }
           
            DESC_ENG.Text = "";
            DESC_ARB.Text = "";
            ADDRESS_A.Text = "";
            ADDRESS_B.Text = "";
            POBOX.Text = "";
            TELE1.Text = "";
            TELE2.Text = "";
            MOBILE.Text = "";
            FAX.Text = "";
            EMAIL.Text = "";
            CITY_CODE.Text = "";
            CITY_NAME.Text = "";
            COUNTRY_CODE.Text = "";
            COUNTRY_NAME.Text = "";
            OPENING_BAL.Text = "";
            BANK.Text = "";
            BANKBRANCH.Text = "";
            IFCCODE.Text = "";
            ACCOUNTNO.Text = "";
            TINNO.Text = "";
            DATE_GRE.Value = DateTime.Today;
            SALESMAN_CODE.Text = "";
            TXT_CREDIT_AMOUNT.Text = "";
            TXT_CREDIT_PERIOD.Text="";
            cmb_salesman.SelectedIndex = 0;
           // credit_period.SelectedText = "";
            //credit_monthly.SelectedText = "";
            //credit_days.SelectedText = "";
            cmonth_day = "";
            

            //if (DEFAULT_CURRENCY.Items.Count > 0)
            //{
            //    DEFAULT_CURRENCY.SelectedIndex = 0;
            //}
            GetBranchDetails();
            UpdateLedgerId = "";
            DESC_ENG.Focus();
            loadStates();
            CODE.Text = generateItemCode();
            cmb_Customer.SelectedIndex = 0;
            chk_cust.Checked = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            cust.LedgerId = UpdateLedgerId;
            
            //cmd.CommandText = "SELECT ISNULL(COUNT(TRANSACTIONID), 0) FROM tb_Transactions WHERE ACCID = @id";
            //cmd.Parameters.Clear();
            //cmd.Parameters.AddWithValue("@id", UpdateLedgerId);
            //conn.Open();
            int count = cust.SelectTransactionCount();
            //conn.Close();
            if (count >= 1)
            {
                if (count == 1)
                {
                    //cmd.CommandText = "SELECT DEBIT, CREDIT FROM tb_Transactions WHERE ACCID = @id";
                    //cmd.Parameters.Clear();
                    //cmd.Parameters.AddWithValue("@id", UpdateLedgerId);
                    //conn.Open();
                    SqlDataReader r = cust.SelectTransactionSum();
                    double debit = 0;
                    double credit = 0;
                    while (r.Read())
                    {
                        debit = Convert.ToDouble(r[0]);
                        credit = Convert.ToDouble(r[1]);
                    }
                    //conn.Close();
                    DbFunctions.CloseConnection();
                    if (debit > 0 || credit > 0)
                    {
                        MessageBox.Show("Sorry!, you can't delete the Customer which has transactions.");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Sorry!, you can't delete the Customer which has transactions.");
                    return;
                }

            }
            if (MessageBox.Show("Are you sure?", "Custome Deletion", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    //conn.Open();
                    //cmd.CommandText = "DELETE FROM REC_CUSTOMER WHERE CODE = '" + ID + "'";
                    //cmd.ExecuteNonQuery();
                    //conn.Close();
                    cust.Code = ID;
                    cust.DeleteCust();
                    led.LEDGERNAME = DESC_ENG.Text;
                    led.DeleteLedger(UpdateLedgerId);
                    tableCustomers.Select("CODE = '" + ID + "'").First().Delete();
                    btnClear.PerformClick();
                    MessageBox.Show("Customer Deleted!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occured while deleting Customer/Supplier");
                }
            }
        }

        private void dgCustomers_Click(object sender, EventArgs e)
        {
            if (dgCustomers.Rows.Count > 0 && dgCustomers.CurrentRow != null)
            {
                btnClear.PerformClick();
                DataGridViewCellCollection c = dgCustomers.CurrentRow.Cells;
                if (checkIsSupplier(Convert.ToInt32(c["ledgerid"].Value)))
                {
                    MessageBox.Show("Can't Update,It is a Supplier!");
                    return;
                }

                ID = Convert.ToString(c["CODE"].Value);
                CODE.Text = ID;
                if (TYPE.Items.Count > 0)
                {
                    TYPE.SelectedValue = c["TYPE"].Value;
                }
                DESC_ENG.Text = Convert.ToString(c["DESC_ENG"].Value);
                if (HasArabic)
                    DESC_ARB.Text = Convert.ToString(c["DESC_ARB"].Value);

                ADDRESS_A.Text = Convert.ToString(c["ADDRESS_A"].Value);
                ADDRESS_B.Text = Convert.ToString(c["ADDRESS_B"].Value);
                POBOX.Text = Convert.ToString(c["POBOX"].Value);
                TELE1.Text = Convert.ToString(c["TELE1"].Value);
                TELE2.Text = Convert.ToString(c["TELE2"].Value);
                MOBILE.Text = Convert.ToString(c["MOBILE"].Value);
                EMAIL.Text = Convert.ToString(c["EMAIL"].Value);
                CITY_CODE.Text = Convert.ToString(c["CITY_CODE"].Value);
                TINNO.Text = Convert.ToString(c["TIN_NO"].Value);
                //MessageBox.Show(Convert.ToString(c["CITY_CODE"].Value));
                COUNTRY_CODE.Text = Convert.ToString(c["COUNTRY_CODE"].Value);
                FAX.Text = Convert.ToString(c["FAX"].Value);
                OPENING_BAL.Text = Convert.ToString(c["OPENING_BAL"].Value);
                DATE_GRE.Value = Convert.ToDateTime(c["DATE_GRE"].Value);
                SALESMAN_CODE.Text = Convert.ToString(c["SALESMAN_CODE"].Value);
               
                CreateBankDetails(Convert.ToString(c["NOTES"].Value));
                string s = Convert.ToString(c["CreditActive"].Value);
                lid = Convert.ToString(c["LedgerId"].Value);
                TXT_CREDIT_AMOUNT.Text = Convert.ToString(c["CREDIT_AMOUNT"].Value);
                if (c["SALESMAN_CODE"].Value == "")
                    cmb_salesman.SelectedIndex = 0;
                else
                    cmb_salesman.SelectedValue = c["SALESMAN_CODE"].Value;
                if (c["CUSTOMER_STATE"].Value == "")
                    cmb_state.SelectedIndex = 0;
                else
                    cmb_state.SelectedValue = c["CUSTOMER_STATE"].Value;
                if (s == "True")
                {
                    IN_ACTIVE.Checked = true;
                }
                else
                {
                    IN_ACTIVE.Checked = false;
                }

                string customer = Convert.ToString(c["CUSTOMER_STATUS"].Value);

                if (customer == "True")
                {
                    Active_Customer.Checked = true;
                }
                else
                {
                    Active_Customer.Checked = false;
                }


                string check_period = Convert.ToString(c["CperiodActive"].Value);
                if (check_period == "True")
                {
                    CHECK_CREDIT_PERIOD.Checked = true;
                }
                else
                {
                    CHECK_CREDIT_PERIOD.Checked = false;
                }

                string DAYS = Convert.ToString(c["CREDIT_PERIOD"].Value);
               // DAYS = string.Concat(DAYS.Reverse().Skip(5).Reverse());
                if (CHECK_CREDIT_PERIOD.Checked)
                {
                    TXT_CREDIT_PERIOD.Text = DAYS;
                }

                if (DEFAULT_CURRENCY.Items.Count > 0)
                {
                    DEFAULT_CURRENCY.SelectedValue = c["DEFAULT_CURRENCY"].Value;
                }
                UpdateLedgerId = (c["LedgerId"].Value).ToString();
                selecttransaction();
            }
        }

        public void CreateBankDetails(string NOTES)
        {
            try
            {
                string[] duminote = NOTES.Split('-');
                BANK.Text = duminote[0];
                BANKBRANCH.Text = duminote[1];
                ACCOUNTNO.Text = duminote[2];
                IFCCODE.Text = duminote[3];
            }
            catch
            {
                //TODO: I DON'T KNOW EXACTLY WHAT THIS IS DOING, SO I'M LEAVING THIS CATCH EMPTY.
            }
        }

        public void selecttransaction()
        {
            DataTable dt = new DataTable();
            trans.ACCID = UpdateLedgerId;
            trans.VOUCHERTYPE = "Opening Balance";
            dt = trans.SelectTransaction();
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["ACCNAME"].ToString() == DESC_ENG.Text)
                {
                    float debit = float.Parse(dt.Rows[0]["DEBIT"].ToString());
                    if (debit > 0)
                    {
                        OPENING_BAL.Text = dt.Rows[0]["DEBIT"].ToString();
                        OPENTYPE.SelectedIndex = 1;
                    }
                    else
                    {
                        OPENING_BAL.Text = dt.Rows[0]["CREDIT"].ToString();
                        OPENTYPE.SelectedIndex = 0;
                    }


                }
                else
                {
                    float debit = float.Parse(dt.Rows[0]["DEBIT"].ToString());
                    if (debit > 0)
                    {
                        OPENING_BAL.Text = dt.Rows[0]["DEBIT"].ToString();
                        OPENTYPE.SelectedIndex = 0;
                    }
                    else
                    {
                        OPENING_BAL.Text = dt.Rows[0]["CREDIT"].ToString();
                        OPENTYPE.SelectedIndex = 1;
                    }
                }

            }
            else
            {
                OPENING_BAL.Text = "0.00";
            }
        }

        private void btnCityCode_Click(object sender, EventArgs e)
        {
            CommonHelp h = new CommonHelp(0, genEnum.City);
            if (h.ShowDialog() == DialogResult.OK && h.c != null)
            {
                CITY_CODE.Text = h.c["CODE"].Value.ToString();
                CITY_NAME.Text = h.c["DESC_ENG"].Value.ToString();
                cmb_state.Focus();
            }
        }

        private void btnCountryCode_Click(object sender, EventArgs e)
        {
            CommonHelp h = new CommonHelp(0, genEnum.Country);
            if (h.ShowDialog() == DialogResult.OK && h.c != null)
            {
                COUNTRY_CODE.Text = h.c["CODE"].Value.ToString();
                COUNTRY_NAME.Text = h.c["DESC_ENG"].Value.ToString();
                OPENING_BAL.Focus();
            }
        }

        private void CITY_CODE_TextChanged(object sender, EventArgs e)
        {
            if (CITY_CODE.Text.Length == 3)
            {
                string name = General.getName(CITY_CODE.Text, "GEN_CITY");
                if (name != "")
                {
                    CITY_NAME.Text = name;
                }
                else
                {
                    CITY_CODE.Text = "";
                    CITY_NAME.Text = "";
                    MessageBox.Show("City Not Found. Please Enter Correct Code!");
                }
            }

        }

        private void COUNTRY_CODE_TextChanged(object sender, EventArgs e)
        {
            if (COUNTRY_CODE.Text.Length == 3)
            {
                string name = General.getName(COUNTRY_CODE.Text, "GEN_COUNTRY");
                if (name != "")
                {
                    COUNTRY_NAME.Text = name;
                }
                else
                {
                    COUNTRY_CODE.Text = "";
                    COUNTRY_NAME.Text = "";
                    MessageBox.Show("Country Not Found. Please Enter Correct Code!");
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

        private void TYPE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                Common.preventDingSound(e);
                if (sender is KryptonTextBox)
                {
                    string name = (sender as KryptonTextBox).Name;
                    switch (name)
                    {

                        case "CODE":
                            DESC_ENG.Focus();
                            break;
                        case "DESC_ENG":
                            if (HasArabic)
                            {
                                DESC_ARB.Focus();
                            }
                            else
                            {
                                ADDRESS_A.Focus();
                            }
                            break;
                        case "DESC_ARB":
                            ADDRESS_A.Focus();
                            break;
                        case "ADDRESS_A":
                            TELE1.Focus();
                            break;
                        case "ADDRESS_B":
                            POBOX.Focus();
                            break;
                        case "POBOX":
                            TELE1.Focus();
                            break;
                        case "TELE1":
                            MOBILE.Focus();
                            break;
                        case "MOBILE":
                            TINNO.Focus();
                            break;
                        case "TINNO":
                            FAX.Focus();
                            break;
                        case "cmb_state":
                           BANK.Focus();
                            break;
                        case "FAX":
                            EMAIL.Focus();
                            break;
                        case "EMAIL":
                            CITY_CODE.Focus();
                            break;
                        case "OPENING_BAL":
                            OPENTYPE.Focus();
                            break;
                        case "SALESMAN_CODE":
                            DEFAULT_CURRENCY.Focus();
                            break;
                        case "BANK":
                            BANKBRANCH.Focus();
                            break;
                        case "BANKBRANCH":
                            ACCOUNTNO.Focus();
                            break;
                        case "ACCOUNTNO":
                            IFCCODE.Focus();
                            break;
                        case "IFCCODE":
                            OPENING_BAL.Focus();
                            break;
                        case "TXT_CREDIT_PERIOD":
                            if (IN_ACTIVE.Checked)
                                TXT_CREDIT_AMOUNT.Focus();
                            else
                                Drp_repeat.Focus();
                            break;
                        case "TXT_CREDIT_AMOUNT":
                            Drp_repeat.Focus();
                            break;
                        case "cmb_salesman":
                            Drp_repeat.Focus();
                            break;
                        default:
                            break;
                    }
                }
                else if (sender is KryptonComboBox)
                {
                    string name = (sender as KryptonComboBox).Name;
                    switch (name)
                    {
                        case "TYPE":
                            DESC_ENG.Focus();
                            break;
                        case "OPENTYPE":
                            DATE_GRE.Focus();
                            break;
                        case "DEFAULT_CURRENCY":
                            cmb_salesman.Focus();
                           // btnSave.Focus();
                            break;
                        case "Drp_repeat":
                            if (Convert.ToString(Drp_repeat.Text).Equals("") || Convert.ToString(Drp_repeat.Text).Equals("None"))
                            {
                                btnSave.Focus();
                            }
                            else
                            {
                                drp_repeatModeDay.Focus();
                            }
                            break;
                        case "drp_repeatModeDay":
                            DrpDay.Focus();
                            break;
                        case "DrpDay":
                            btnSave.Focus();
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
                        case "DATE_GRE":
                            DEFAULT_CURRENCY.Focus();
                            break;
                        default:
                            break;
                    }
                }
                else if (sender is KryptonCheckBox)
                {
                    string name = (sender as KryptonCheckBox).Name;
                    switch (name)
                    {
                        case "CHECK_CREDIT_PERIOD":
                            if (CHECK_CREDIT_PERIOD.Checked)
                            {
                                TXT_CREDIT_PERIOD.Focus();
                            }
                            else
                            {
                                IN_ACTIVE.Focus();
                            }
                            break;
                        case "IN_ACTIVE":
                            if (IN_ACTIVE.Checked)
                            {
                                TXT_CREDIT_AMOUNT.Focus();
                            }
                            else
                            {
                                Drp_repeat.Focus();
                            }
                            break;
                        default:
                            break;
                    }
                }
            }

            
            else if (e.KeyCode == Keys.Down)
            {
                if (sender is KryptonButton)
                {
                    string name = (sender as KryptonButton).Name;
                    switch (name)
                    {
                        case "btnCountryCode":
                            OPENING_BAL.Focus();
                            break;

                        default:
                            break;
                    }
                }
            }
        }

        private void btnCountryCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                OPENING_BAL.Focus();
            }
        }

        private void CITY_CODE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmb_state.Focus();
            }
            else if (e.KeyCode == Keys.Back)
            {
                if (CITY_CODE.Text == "")
                {
                    CITY_NAME.Text = "";
                }
            }
            else
            {
                btnCityCode.PerformClick();
            }
        }

        private void COUNTRY_CODE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                OPENING_BAL.Focus();
            }
            else if (e.KeyCode == Keys.Back)
            {
                if (COUNTRY_CODE.Text == "")
                {
                    COUNTRY_NAME.Text = "";
                }
            }
            else
            {
                btnCountryCode.PerformClick();
            }
        }

        private void DESC_ENG_Leave(object sender, EventArgs e)
        {
            DESC_ENG.Text = DESC_ENG.Text.ToUpper();
        }

        private void Drp_repeat_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            switch (Drp_repeat.Text)
            {
                case "Weekly":
                    panelDay.Visible = true;
                    panelMonth.Visible = false;
                    break;
                case "Monthly":
                    panelDay.Visible = false;
                    panelMonth.Visible = true;
                    break;
                default:
                    panelDay.Visible = false;
                    panelMonth.Visible = false;
                    break;
            }
        }

        private void IN_ACTIVE_CheckedChanged(object sender, EventArgs e)
        {
            if (IN_ACTIVE.Checked == true)
            {
                TXT_CREDIT_AMOUNT.Enabled = true;
                //LB_CREDIT_AMOUNT.Visible = true;
            }
            else
            {
                TXT_CREDIT_AMOUNT.Enabled = false;
              //  LB_CREDIT_AMOUNT.Visible = false;
            }
        }

        private void CHECK_CREDIT_PERIOD_CheckedChanged(object sender, EventArgs e)
        {
            if (CHECK_CREDIT_PERIOD.Checked == true)
            {
                TXT_CREDIT_PERIOD.Enabled = true;
              //  LB_CREDIT_PERIOD.Visible = true;
            }
            else
            {
                TXT_CREDIT_PERIOD.Enabled = false;
              //  LB_CREDIT_PERIOD.Visible = false;
            }
        }

        private void Active_Customer_CheckedChanged(object sender, EventArgs e)
        {
            if (Active_Customer.Checked == true)
            {
                lb_active.Visible = true;
                lb_inactive.Visible = false;
            }
            else
            {
                lb_active.Visible = false;
                lb_inactive.Visible = true;
            }
        }

        private void dgCustomers_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cmb_state_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BANK.Focus();
            }
        }

        private void cmb_salesman_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (CHECK_CREDIT_PERIOD.Checked)
                    TXT_CREDIT_PERIOD.Focus();
                else if (IN_ACTIVE.Checked)
                    TXT_CREDIT_AMOUNT.Focus();
                else
                Drp_repeat.Focus();
            }
        }

        private void chk_cust_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_cust.Checked)
            {
                cmb_Customer.Enabled = true;

            }
            else
            {
                cmb_Customer.Enabled = false;
                cmb_Customer.SelectedIndex = 0;
            }
        }
        void bindSupplier()
        {
            DataTable dtTable = cust.GetSup();
            //conn.Open();
            //cmd = new SqlCommand("SELECT DESC_ENG,LEDGERID FROM PAY_SUPPLIER", conn);
            //SqlDataAdapter adp = new SqlDataAdapter(cmd);
            //adp.Fill(dtTable);
            //conn.Close();

            DataRow row = dtTable.NewRow();
            row[1] = "";
            dtTable.Rows.InsertAt(row, 0);
            cmb_Customer.DataSource = dtTable;
            cmb_Customer.DisplayMember = "DESC_ENG";
            cmb_Customer.ValueMember = "LEDGERID";

        }

        private void cmb_Customer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_Customer.SelectedIndex > 0)
            {
                cust.LedgerId = cmb_Customer.SelectedValue.ToString();
                DataTable dtTable = cust.GetSupDetails();
                //conn.Open();
                //cmd = new SqlCommand("SELECT * FROM PAY_SUPPLIER where LEDGERID="+Convert.ToInt32(cmb_Customer.SelectedValue), conn);
                //SqlDataAdapter adp = new SqlDataAdapter(cmd);
                //adp.Fill(dtTable);
                //conn.Close();
                

                DESC_ENG.Text = dtTable.Rows[0]["DESC_ENG"].ToString();

                if (HasArabic)
                    DESC_ARB.Text = dtTable.Rows[0]["DESC_ARB"].ToString();
                ADDRESS_A.Text = dtTable.Rows[0]["ADDRESS_A"].ToString();
                ADDRESS_B.Text = dtTable.Rows[0]["ADDRESS_B"].ToString();
                POBOX.Text = dtTable.Rows[0]["POBOX"].ToString();
                TELE1.Text = dtTable.Rows[0]["TELE1"].ToString();
                TELE2.Text = dtTable.Rows[0]["TELE2"].ToString();
                MOBILE.Text = dtTable.Rows[0]["MOBILE"].ToString();
                EMAIL.Text = dtTable.Rows[0]["EMAIL"].ToString();
                CITY_CODE.Text = dtTable.Rows[0]["CITY_CODE"].ToString();
                COUNTRY_CODE.Text = dtTable.Rows[0]["COUNTRY_CODE"].ToString();
                OPENING_BAL.Text = dtTable.Rows[0]["OPENING_BAL"].ToString();
                FAX.Text = dtTable.Rows[0]["FAX"].ToString();
                TINNO.Text = dtTable.Rows[0]["TIN_NO"].ToString();
                if (dtTable.Rows[0]["STATE"].ToString() == "")
                    cmb_state.SelectedIndex = 0;
                else
                    cmb_state.SelectedValue = dtTable.Rows[0]["STATE"];
            }
        }
        bool checkIsSupplier(int LEDGERID)
        {
            bool isSupplier = false;
            //conn.Open();
            //cmd = new SqlCommand("SELECT UNDER FROM tb_Ledgers WHERE ledgerid=" + LEDGERID, conn);
            cust.LedgerId = LEDGERID.ToString();
            string under = cust.chkIsSupplier().ToString();
            //conn.Close();
            isSupplier = under == "14" ? false : true;
            return isSupplier;

        }
    }
}