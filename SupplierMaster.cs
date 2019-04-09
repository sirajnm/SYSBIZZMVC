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
using Sys_Sols_Inventory.Model;

namespace Sys_Sols_Inventory
{
    public partial class SupplierMaster : Form
    {
        private bool HasArabic = true;
        //private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        //private SqlCommand cmd = new SqlCommand();
        //private SqlDataAdapter adapter = new SqlDataAdapter();
        private DataTable table = new DataTable();
        private DataTable tableType = new DataTable();
        private DataTable tableCurrency = new DataTable();
        private BindingSource source = new BindingSource();
        public string ledId = "";
        DataTable dt = new DataTable();
        Class.CompanySetup Comp = new Class.CompanySetup();
        Class.Ledgers led = new Class.Ledgers();
        Class.Transactions trans = new Class.Transactions();
        Class.CollectionDay colday = new Class.CollectionDay();
        Login lg = (Login)Application.OpenForms["Login"];
        private DataTable tableCustomers = new DataTable();
        private DataTable tableCustomers1 = new DataTable();
        private DataTable tableFilter = new DataTable();

        PaySupplierDB paysupdb = new PaySupplierDB();

        private string ID = "";
        private string UpdateLedgerId = "";
        private string OldName = "";
        public int flag = 0;
        public SupplierMaster()
        {
            InitializeComponent();
            //cmd.Connection = conn;
            //adapter.SelectCommand = cmd;
            TELE1.KeyPress+= new KeyPressEventHandler(General.OnlyInt);
            TELE2.KeyPress+= new KeyPressEventHandler(General.OnlyInt);
            MOBILE.KeyPress+= new KeyPressEventHandler(General.OnlyInt);

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
        public void getdata()
        {
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataTable dt = new DataTable();
                //if (conn.State == ConnectionState.Open)
                //{
                //}
                //else
                //{

                //    conn.Open();
                //}

                //--> cmd.CommandText = "SELECT CODE,TYPE,DESC_ENG,ADDRESS_A,ADDRESS_B,POBOX,TELE1,TELE2,MOBILE,EMAIL,CITY_CODE,COUNTRY_CODE,OPENING_BAL,FAX,CONVERT(NVARCHAR,DATE_GRE,103) AS DATE_GRE,INITIAL,DEFAULT_CURRENCY,CONTACT_PERSON,LedgerId,NOTES,Supplier_Status,DEBIT_LIMIT,DEBIT_AMOUNT,D_PERIODACTIVE,DEBIT_PERIOD_TYPE,[STATE] AS SUPPLIER_STATE, TIN_NO AS GSTN FROM PAY_SUPPLIER order by PAY_SUPPLIER.code asc";
                //--> cmd.Connection = conn;
                //--> adapter.SelectCommand = cmd;
                //--> adapter.Fill(tableCustomers);
                tableCustomers=paysupdb.SelectAllCust();
                source.DataSource = tableCustomers;
                dgSuppliers.DataSource = tableCustomers;

            }

            catch
            {

            }
        }
        private void SupplierMaster_Load(object sender, EventArgs e)
        {
            HasArabic = General.IsEnabled(Settings.Arabic);

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
            tableFilter.Rows.Add("CITY_CODE", "City Code");
            tableFilter.Rows.Add("EMAIL", "Email");
            tableFilter.Rows.Add("COUNTRY_CODE", "Country Code");
            tableFilter.Rows.Add("FAX", "Fax");

            countryDetails();
            Class.CompanySetup CompStep = new Class.CompanySetup();
            DATE_GRE.Text = CompStep.GettDate();
            OPENING_BAL.KeyPress += new KeyPressEventHandler(General.NegFloat);
            //if(HasArabic)
            //    cmd.CommandText = "SELECT CODE,TYPE,DESC_ENG,DESC_ARB,ADDRESS_A,ADDRESS_B,POBOX,TELE1,TELE2,MOBILE,EMAIL,CITY_CODE,COUNTRY_CODE,OPENING_BAL,FAX,CONVERT(NVARCHAR,DATE_GRE,103) AS DATE_GRE,INITIAL,DEFAULT_CURRENCY,CONTACT_PERSON,LedgerId,NOTES FROM PAY_SUPPLIER";
            //else
            //    cmd.CommandText = "SELECT CODE,TYPE,DESC_ENG,ADDRESS_A,ADDRESS_B,POBOX,TELE1,TELE2,MOBILE,EMAIL,CITY_CODE,COUNTRY_CODE,OPENING_BAL,FAX,CONVERT(NVARCHAR,DATE_GRE,103) AS DATE_GRE,INITIAL,DEFAULT_CURRENCY,CONTACT_PERSON,LedgerId,NOTES FROM PAY_SUPPLIER";

            //adapter.Fill(table);
            //source.DataSource = table;
            //dgSuppliers.DataSource = source;
            
            //supplier type
            getdata();
            //--> cmd.CommandText = "SELECT CODE,CODE+' - '+DESC_ENG AS DESC_ENG FROM PAY_SUPPLIER_TYPE";
            //--> adapter.Fill(tableType);
            tableType = paysupdb.getSupplierData();
            TYPE.DataSource = tableType;
            TYPE.DisplayMember = "DESC_ENG";
            TYPE.ValueMember = "CODE";

            //currency
            //-->cmd.CommandText = "SELECT CODE,CODE+' - '+DESC_ENG AS DESC_ENG FROM GEN_CURRENCY";
            //--> adapter.Fill(tableCurrency);
            tableCurrency = paysupdb.getGenCurrency();
            DEFAULT_CURRENCY.DataSource = tableCurrency;
            DEFAULT_CURRENCY.DisplayMember = "DESC_ENG";
            DEFAULT_CURRENCY.ValueMember = "CODE";

            loadStates();
            //cmb_state.DataSource = loadStates();
            //cmb_state.DisplayMember = "NAME";
            //cmb_state.ValueMember = "CODE";
           
            if (!HasArabic)
                DESC_ARB.Enabled = false;
            PnlArabic.Visible = false;
           // CODE.Focus();
            OPENTYPE.SelectedIndex = 0;
            CODE.Text = generateItemCode();
            GetBranchDetails();
            ActiveControl = CODE;
            TXT_DEBIT_PERIOD.ReadOnly = false;
            getGeneralSetup();
            bindCustomer();
            load_CusAsSup();
            ActiveControl = DESC_ENG;
            cmbFilter.SelectedValue = "DESC_ENG";
        }
        void load_CusAsSup()
        {
            bool value = false;
            //try
            //{
                //conn.Open();
                //--> cmd.CommandType = CommandType.Text;
                //--> cmd.CommandText = "SELECT  IsCusSup  FROM   SYS_SETUP";
                //-->  value = Convert.ToBoolean(cmd.ExecuteScalar());
            try
            {
                value = Convert.ToBoolean(paysupdb.getCusSup());
            }
            catch
            {
                value = false;
            }//conn.Close();
            //}
            //catch
            //{
                //if (conn.State == ConnectionState.Open)
                //    conn.Close();
            //}
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
            DataTable dt = new DataTable();
            try
            {
                //conn.Open();

                //--> cmd.CommandText = "SELECT ACTIVE_DEBIT_PERIOD,DEBIT_PERIOD  FROM   SYS_SETUP";
                //--> cmd.Connection = conn;
                //--> adapter.SelectCommand = cmd;
                //--> adapter.Fill(dt);
               dt= paysupdb.getActiveDebitPeriod();
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["ACTIVE_DEBIT_PERIOD"].ToString() == "True")
                    {
                        CHECK_DEBIT_PERIOD.Checked = true;
                        TXT_DEBIT_PERIOD.Text = dt.Rows[0]["DEBIT_PERIOD"].ToString();

                    }
                    else
                    {
                        CHECK_DEBIT_PERIOD.Checked = false;
                    }


                }

            }
            catch (Exception ex)
            {

            }
            //finally
            //{
               // conn.Close();

            //}
            return dt;
        }

        public string generateItemCode()
        {
            //cmd.Connection = conn;
            //if (conn.State == ConnectionState.Open)
            //{
            //}
            //else
            //{

            //    conn.Open();
            //}
            //--> cmd.CommandType = CommandType.Text;
            //-->  cmd.CommandText = "SELECT MAX(CODE) FROM PAY_SUPPLIER";
            //--> string id = Convert.ToString(cmd.ExecuteScalar());
            string id = Convert.ToString(paysupdb.getMaxCode());
           
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
       
        

        public void GetBranchDetails()
        {
            try
            {
               
                dt = Comp.GetCurrentBranchDetails();
                if (dt.Rows.Count > 0)
                {

                    DEFAULT_CURRENCY.SelectedValue = dt.Rows[0]["DEFAULT_CURRENCY_CODE"].ToString();
                }
            }
            catch
            {
            }
        }

        private bool valid()
        {
            if (CODE.Text == "" | DESC_ENG.Text == "")
            { 
              
                MessageBox.Show("Enter the following details\n1 Code\n2 English Name");
                CODE.Focus();
                return false;
               
            }
              if(cmb_state.SelectedIndex<1)
                      {
                          MessageBox.Show("Please select state");
                          return false;
                      }
            return true;
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
                return NOTES;
            }
        }

        public int CheckLedger(string s)
        {
            int ret = 0;
            if (chk_cust.Checked)
            {
                return 0;
            }
            try
            {

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
            }
            catch
            {
            }
            return ret;
        }  
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (valid())
            {
                string supplierType = "";
                string defaultCurrency = "";
                string status = "Added!";
                string sup_period = "";
                string sup_mont_day = "";
                if (ID == "")
                {
                    
                    if (CheckLedger(DESC_ENG.Text) != 1)
                    {
                        if (General.ItemExists(CODE.Text, ID, "PAY_SUPPLIER"))
                        {

                            MessageBox.Show("An Supplier with the same code already exists!");
                            CODE.Focus();
                            return;
                        }

                        if (TYPE.Items.Count > 0)
                        {
                            supplierType = Convert.ToString(TYPE.SelectedValue);
                        }

                        if (DEFAULT_CURRENCY.Items.Count > 0)
                        {
                            defaultCurrency = Convert.ToString(DEFAULT_CURRENCY.SelectedValue);
                        }
                        if (!chk_cust.Checked)
                            Addledger();
                        int LedgerId = 0;
                        if (!chk_cust.Checked)
                            LedgerId = GetMaxLedger();
                        else
                            LedgerId = Convert.ToInt32(cmb_Customer.SelectedValue);

                        if (OPENING_BAL.Text == "")
                            OPENING_BAL.Text = "0";
                        string active_supplier;
                        if(Active_Supplier.Checked)
                        {
                            active_supplier = "True";
                        }
                        else
                        {
                            active_supplier = "False";
                        }
                        string debit_period = "";
                        if (CHECK_DEBIT_PERIOD.Checked)
                        {
                            debit_period = "True";
                            TXT_DEBIT_PERIOD.Text = TXT_DEBIT_PERIOD.Text;
                        }
                        else
                        {
                            TXT_DEBIT_PERIOD.Text = "0";
                            debit_period = "False";
                        }
                        string debit_limit = "";
                        
                        if (CHECK_DEBIT_LIMIT.Checked)
                        {
                            debit_limit = "True";
                        }
                        else
                        {
                            debit_limit = "False";
                        }
                    
                        if (ID == "")
                        {
                            //--> cmd.CommandText = "INSERT INTO PAY_SUPPLIER(CODE,TYPE,DESC_ENG,DESC_ARB,ADDRESS_A,ADDRESS_B,POBOX,TELE1,TELE2,MOBILE,EMAIL,CITY_CODE,COUNTRY_CODE,OPENING_BAL,FAX,DATE_GRE,INITIAL,DEFAULT_CURRENCY,CONTACT_PERSON,LedgerId,NOTES,Supplier_Status,DEBIT_LIMIT,DEBIT_AMOUNT,D_PERIODACTIVE,DEBIT_PERIOD_TYPE,STATE, TIN_NO) VALUES('" + CODE.Text + "','" + supplierType + "','" + DESC_ENG.Text + "','" + DESC_ARB.Text + "','" + ADDRESS_A.Text + "','" + ADDRESS_B.Text + "','" + POBOX.Text + "','" + TELE1.Text + "','" + TELE2.Text + "','" + MOBILE.Text + "','" + EMAIL.Text + "','" + CITY_CODE.Text + "','" + COUNTRY_CODE.Text + "','" + OPENING_BAL.Text + "','" + FAX.Text + "','" + DATE_GRE.Value.ToString("MM/dd/yyyy") + "','" + INITIAL.Text + "','" + defaultCurrency + "','" + CONTACT_PERSON.Text + "','" + LedgerId + "','" + CREATENOTES() + "','"+active_supplier+"', '"+debit_limit+"','"+TXT_DEBIT_AMOUNT.Text+"','"+debit_period+"','"+TXT_DEBIT_PERIOD.Text+"','"+cmb_state.SelectedValue.ToString()+"', '"+TINNO.Text+"')";
                            paysupdb.Code = CODE.Text;
                            paysupdb.Type = supplierType;
                            paysupdb.DescEng = DESC_ENG.Text;
                            paysupdb.DescArb = DESC_ARB.Text;
                            paysupdb.AddressA = ADDRESS_A.Text;
                            paysupdb.AddressB = ADDRESS_B.Text;
                            paysupdb.PoBox = POBOX.Text;
                            paysupdb.Tele1 = TELE1.Text;
                            paysupdb.Tele2 = TELE2.Text;
                            paysupdb.Mobile = MOBILE.Text;
                            paysupdb.Email = EMAIL.Text;
                            paysupdb.CityCode = CITY_CODE.Text;
                            paysupdb.CountryCode = COUNTRY_CODE.Text;
                            paysupdb.OpeningBal = Convert.ToDecimal(OPENING_BAL.Text);
                            paysupdb.Fax = FAX.Text;
                            paysupdb.DateGre=DATE_GRE.Value;
                            paysupdb.Initial = INITIAL.Text;
                            paysupdb.DefaultCurrency = defaultCurrency;
                            paysupdb.ContactPerson = CONTACT_PERSON.Text;
                            paysupdb.LedgerId = LedgerId.ToString();
                            paysupdb.Notes = CREATENOTES();
                            paysupdb.SupplierStatus  = Convert.ToBoolean(active_supplier);
                            paysupdb.DebitLimit =  Convert.ToBoolean(debit_limit);
                            paysupdb.DebitAmount = TXT_DEBIT_AMOUNT.Text;
                            paysupdb.DPeriodActive = Convert.ToBoolean( debit_period);
                            paysupdb.DebitPeriodType = TXT_DEBIT_PERIOD.Text;
                            paysupdb.State = cmb_state.SelectedValue.ToString();
                            paysupdb.TinNo = TINNO.Text;
                            paysupdb.insertPaySupplier();

                            //if(HasArabic)
                            //    table.Rows.Add(CODE.Text, supplierType, DESC_ENG.Text, DESC_ARB.Text, ADDRESS_A.Text, ADDRESS_B.Text, POBOX.Text, TELE1.Text, TELE2.Text, MOBILE.Text, EMAIL.Text, CITY_CODE.Text, COUNTRY_CODE.Text, OPENING_BAL.Text, FAX.Text, DATE_GRE.Value.ToString("dd/MM/yyyy"), INITIAL.Text, defaultCurrency, CONTACT_PERSON.Text, LedgerId, CREATENOTES());
                            //else
                            //    table.Rows.Add(CODE.Text, supplierType, DESC_ENG.Text, ADDRESS_A.Text, ADDRESS_B.Text, POBOX.Text, TELE1.Text, TELE2.Text, MOBILE.Text, EMAIL.Text, CITY_CODE.Text, COUNTRY_CODE.Text, OPENING_BAL.Text, FAX.Text, DATE_GRE.Value.ToString("dd/MM/yyyy"), INITIAL.Text, defaultCurrency, CONTACT_PERSON.Text, LedgerId, CREATENOTES());
                            if (!chk_cust.Checked)
                                Addtransaction();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Try With Another Name");
                        status = "Not Added";

                    }
                }
                if (ID != "")
                {
                    string active_supplier;
                    if (Active_Supplier.Checked)
                    {
                        active_supplier = "True";
                    }
                    else
                    {
                        active_supplier = "False";
                    }
                    if (TYPE.Items.Count > 0)
                    {
                        supplierType = Convert.ToString(TYPE.SelectedValue);
                    }
                    if (DEFAULT_CURRENCY.Items.Count > 0)
                    {
                        defaultCurrency = Convert.ToString(DEFAULT_CURRENCY.SelectedValue);
                    }
                      string debit_period = "";
                        if (CHECK_DEBIT_PERIOD.Checked)
                        {
                            debit_period = "True";
                            TXT_DEBIT_PERIOD.Text = TXT_DEBIT_PERIOD.Text;
                        }
                        else
                        {
                            TXT_DEBIT_PERIOD.Text = "0";
                            debit_period = "False";
                        }
                        string debit_limit = "";
                        if (CHECK_DEBIT_LIMIT.Checked)
                        {
                            debit_limit = "True";
                        }
                        else
                        {
                            debit_limit = "False";
                        }
                  
                    //--> cmd.CommandText = "UPDATE PAY_SUPPLIER SET CODE = '" + CODE.Text + "',TYPE = '" + supplierType + "',DESC_ENG = '" + DESC_ENG.Text + "',DESC_ARB = '" + DESC_ARB.Text + "',ADDRESS_A = '" + ADDRESS_A.Text + "',ADDRESS_B = '" + ADDRESS_B.Text + "',POBOX = '" + POBOX.Text + "',TELE1 = '" + TELE1.Text + "',TELE2 = '" + TELE2.Text + "',MOBILE = '" + MOBILE.Text + "',EMAIL = '" + EMAIL.Text + "',CITY_CODE = '" + CITY_CODE.Text + "',COUNTRY_CODE = '" + COUNTRY_CODE.Text + "',OPENING_BAL = '" + OPENING_BAL.Text + "',FAX = '" + FAX.Text + "',DATE_GRE = '" + DATE_GRE.Value.ToString("MM/dd/yyyy") + "',INITIAL = '" + INITIAL.Text + "',DEFAULT_CURRENCY = '" + defaultCurrency + "',CONTACT_PERSON = '" + CONTACT_PERSON.Text + "',NOTES='" + CREATENOTES() + "',Supplier_Status='" + active_supplier + "',DEBIT_AMOUNT='"+TXT_DEBIT_AMOUNT.Text+"',D_PERIODACTIVE='"+debit_period+"',DEBIT_PERIOD_TYPE='"+TXT_DEBIT_PERIOD.Text+"',STATE='"+cmb_state.SelectedValue.ToString()+"', TIN_NO = '"+TINNO.Text+"' WHERE CODE = '" + ID + "'";
                        paysupdb.Code = CODE.Text;
                        paysupdb.Type = supplierType;
                        paysupdb.DescEng=DESC_ENG.Text; 
                        paysupdb.DescArb=DESC_ARB.Text;
                        paysupdb.AddressA=ADDRESS_A.Text;
                        paysupdb.AddressB=ADDRESS_B.Text;
                        paysupdb.PoBox=POBOX.Text;
                        paysupdb.Tele1=TELE1.Text;
                        paysupdb.Tele2=TELE2.Text;
                        paysupdb.Mobile=MOBILE.Text;
                        paysupdb.Email=EMAIL.Text;
                        paysupdb.CityCode=CITY_CODE.Text;
                        paysupdb.CountryCode=COUNTRY_CODE.Text;
                        paysupdb.OpeningBal= Convert.ToDecimal(OPENING_BAL.Text);
                        paysupdb.Fax=FAX.Text;
                        paysupdb.DateGre= Convert.ToDateTime(DATE_GRE.Value);
                        paysupdb.Initial=INITIAL.Text;
                        paysupdb.DefaultCurrency=defaultCurrency;
                        paysupdb.ContactPerson=CONTACT_PERSON.Text;
                        paysupdb.Notes=CREATENOTES();
                        paysupdb.SupplierStatus=Convert.ToBoolean(active_supplier);
                        paysupdb.DebitAmount=TXT_DEBIT_AMOUNT.Text;
                        paysupdb.DPeriodActive=Convert.ToBoolean(debit_period);
                        paysupdb.DebitPeriodType=TXT_DEBIT_PERIOD.Text;
                        paysupdb.State=cmb_state.SelectedValue.ToString();
                        paysupdb.TinNo=TINNO.Text;
                        paysupdb.Code=ID;

                    DataRow row = tableCustomers.Select("CODE = '" + ID + "'").First();
                    row["CODE"] = CODE.Text;
                    row["TYPE"] = supplierType;
                    row["DESC_ENG"] = DESC_ENG.Text;
                    if (HasArabic)
                        row["DESC_ARB"] = DESC_ARB.Text;
                    row["ADDRESS_A"] = ADDRESS_A.Text;
                    row["ADDRESS_B"] = ADDRESS_B.Text;
                    row["POBOX"] = POBOX.Text;
                    row["TELE1"] = TELE1.Text;
                    row["TELE2"] = TELE2.Text;
                    row["MOBILE"] = MOBILE.Text;
                    row["EMAIL"] = EMAIL.Text;
                    row["CITY_CODE"] = CITY_CODE.Text;
                    row["COUNTRY_CODE"] = COUNTRY_CODE.Text;
                    row["OPENING_BAL"] = OPENING_BAL.Text;
                    row["FAX"] = FAX.Text;
                    row["DATE_GRE"] = DATE_GRE.Value.ToString("dd/MM/yyyy");
                    row["INITIAL"] = INITIAL.Text;
                    row["DEFAULT_CURRENCY"] = defaultCurrency;
                    row["CONTACT_PERSON"] = CONTACT_PERSON.Text;
                    row["NOTES"] = NOTES;
                    row["Supplier_Status"] = active_supplier;
                    row["DEBIT_AMOUNT"] = TXT_DEBIT_AMOUNT.Text;
                    row["D_PERIODACTIVE"] = debit_period;
                    row["DEBIT_PERIOD_TYPE"] = TXT_DEBIT_PERIOD.Text;
                    row["DEBIT_LIMIT"] = debit_limit;
                    row["GSTN"] = TINNO.Text;
                    row["SUPPLIER_STATE"] = cmb_state.SelectedValue.ToString();

                 //   row[""]
                    status = "Updated!";
                    try
                    {
                        if (Convert.ToDouble(OPENING_BAL.Text) > 0)
                        {

                            if (!chk_cust.Checked)
                            {
                                DeleteTrans();
                                Addtransaction();
                            }
                        }

                    }
                    catch
                    {
                    }
                }

                try
                {
                    //if (conn.State == ConnectionState.Open)
                    //{
                    //}
                    //else
                    //{

                    //    conn.Open();
                    //}
                    //--> cmd.ExecuteNonQuery();
                    paysupdb.updatePaySupplier();
                    UpdateCustomer();
                    colday.SupId = CODE.Text;
                    colday.DeleteCollection();
                        if (Drp_repeat.Text != "")
                        {
                            addcollection();
                        }
                  
                    MessageBox.Show("Supplier " + status);
                    if (ID == "")
                    {
                        
                        tableCustomers1.Clear();
                        
                        dgSuppliers.DataSource = null;
                        dgSuppliers.Refresh();
                       
                        tableCustomers1 = getCus();
                       dgSuppliers.DataSource = tableCustomers1;
                       tableCustomers = tableCustomers1;
                    }
                    btnClear.PerformClick();
                }
                catch (Exception ex)
                {
                   //MessageBox.Show(ex.Message);
                }
                //finally
                //{
                //    conn.Close();
                //}
                //getdata();
            }
            CODE.Text = generateItemCode();
            getGeneralSetup();
            if (flag == 1)
            {
                flag = 0;
              
                this.Close();
            }
        }
        void UpdateCustomer()
        {
            try
            {
                //--> cmd.CommandText = "UPDATE REC_CUSTOMER SET DESC_ENG = '" + DESC_ENG.Text + "',DESC_ARB = '" + DESC_ARB.Text + "',ADDRESS_A = '" + ADDRESS_A.Text + "',ADDRESS_B = '" + ADDRESS_B.Text + "',POBOX = '" + POBOX.Text + "',TELE1 = '" + TELE1.Text + "',TELE2 = '" + TELE2.Text + "',MOBILE = '" + MOBILE.Text + "',EMAIL = '" + EMAIL.Text + "',CITY_CODE = '" + CITY_CODE.Text + "',COUNTRY_CODE = '" + COUNTRY_CODE.Text + "',OPENING_BAL = '" + OPENING_BAL.Text + "',FAX = '" + FAX.Text + "',DATE_GRE = '" + DATE_GRE.Value.ToString("MM/dd/yyyy") + "',INITIAL = '" + INITIAL.Text + "',NOTES='" + CREATENOTES() + "',STATE='" + cmb_state.SelectedValue.ToString() + "', TIN_NO = '" + TINNO.Text + "' WHERE LedgerId = '" + ledId + "'";

                paysupdb.DescEng = DESC_ENG.Text;
                paysupdb.DescArb = DESC_ARB.Text;
                paysupdb.AddressA = ADDRESS_A.Text;
                paysupdb.AddressB = ADDRESS_B.Text;
                paysupdb.PoBox = POBOX.Text;
                paysupdb.Tele1 = TELE1.Text;
                paysupdb.Tele2 = TELE2.Text;
                paysupdb.Mobile = MOBILE.Text;
                paysupdb.Email = EMAIL.Text;
                paysupdb.CityCode = CITY_CODE.Text;
                paysupdb.CountryCode = COUNTRY_CODE.Text;
                paysupdb.OpeningBal = Convert.ToDecimal(OPENING_BAL.Text);
                paysupdb.Fax = FAX.Text;
                paysupdb.DateGre = Convert.ToDateTime(DATE_GRE.Value);
                paysupdb.Initial = INITIAL.Text;
                paysupdb.Notes = CREATENOTES();
                paysupdb.State = cmb_state.SelectedValue.ToString();
                paysupdb.TinNo = TINNO.Text;
                paysupdb.LedgerId = ledId;
                //if (conn.State == ConnectionState.Open)
                //{
                //}
                //else
                //{
                //  conn.Open();
                //}
                //--> cmd.ExecuteNonQuery();
                paysupdb.updateCustomer();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            //finally
            //{
            //    if (conn.State == ConnectionState.Open)
            //    {
            //      conn.Close();
            //    }
            //}
        }
        public DataTable getCus()
        {
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                //DataTable tableCustomers = new DataTable();
                //if (conn.State == ConnectionState.Open)
                //{
                //}
                //else
                //{

                //    conn.Open();
                //}

                //--> cmd.CommandText = "SELECT CODE,TYPE,DESC_ENG,ADDRESS_A,ADDRESS_B,POBOX,TELE1,TELE2,MOBILE,EMAIL,CITY_CODE,COUNTRY_CODE,OPENING_BAL,FAX,CONVERT(NVARCHAR,DATE_GRE,103) AS DATE_GRE,INITIAL,DEFAULT_CURRENCY,CONTACT_PERSON,LedgerId,NOTES,Supplier_Status,DEBIT_LIMIT,DEBIT_AMOUNT,D_PERIODACTIVE,DEBIT_PERIOD_TYPE,[STATE] AS SUPPLIER_STATE,TIN_NO AS GSTN FROM PAY_SUPPLIER order by PAY_SUPPLIER.code asc";
               //--> cmd.Connection = conn;
              //-->   adapter.SelectCommand = cmd;
              //-->  adapter.Fill(tableCustomers1);
                tableCustomers1 = paysupdb.getDetailsByCode();
            }

            catch
            {

            }
            return tableCustomers1;
        }
        public int GetMaxLedger()
        {
            return led.MaxLedGerid();
        }
        public void Addledger()
        {
            led.LEDGERNAME = DESC_ENG.Text;
            led.UNDER = "13";
            led.ADRESS = ADDRESS_A.Text + "," + ADDRESS_B.Text + "," + CITY_NAME.Text + "," + COUNTRY_NAME.Text;
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
             
            if (ID == "")
            {

                led.insertLedger();

            }
            else
            {
               
                led.LEDGERID = Convert.ToInt32(UpdateLedgerId);
                led.UpdateLedger();

            }

        }

        public void Addtransaction()
        {
            trans.VOUCHERTYPE = "Opening Balance";
            //trans.DATED = DateTime.Now.ToString("MM/dd/yyyy");
            trans.DATED = DATE_GRE.Value.ToString("MM/dd/yyyy");
            trans.NARRATION = "Opening Balance";
            Login log = (Login)Application.OpenForms["Login"];
            trans.USERID = log.EmpId;

            if (ID == "")
            {
                if (OPENTYPE.Text == "CR")
                {
                    trans.ACCNAME = DESC_ENG.Text;
                    trans.PARTICULARS = "SUSPENSE ACCOUNT";
                    trans.VOUCHERNO = "";
                    if (dt.Rows.Count > 0)
                        trans.ACCID = GetMaxLedger().ToString();
                    if (OPENING_BAL.Text != "")
                        trans.CREDIT = OPENING_BAL.Text;
                    else
                        trans.CREDIT = "0";
                    trans.DEBIT = "0";
                    trans.SYSTEMTIME = DateTime.Now.ToString("MM/dd/yyyy");
                    trans.BRANCH = lg.Branch;
                    trans.insertTransaction();

                 /*   trans.PARTICULARS = DESC_ENG.Text;
                    trans.ACCNAME = "SUSPENSE ACCOUNT";
                    trans.VOUCHERNO = "";
                    if (dt.Rows.Count > 0)
                        trans.ACCID = "202";
                    if (OPENING_BAL.Text != "")
                        trans.DEBIT = OPENING_BAL.Text;
                    else
                        trans.DEBIT = "0";
                    trans.CREDIT = "0";
                    trans.SYSTEMTIEM = DateTime.Now.ToString("MM/dd/yyyy");
                    trans.BRANCH = lg.Branch;
                    trans.insertTransaction();*/
                }
                else
                {
                    trans.ACCNAME = DESC_ENG.Text;
                    trans.PARTICULARS = "SUSPENSE ACCOUNT";
                    trans.VOUCHERNO = "";
                    if (dt.Rows.Count > 0)
                        trans.ACCID = GetMaxLedger().ToString();

                    if (OPENING_BAL.Text != "")
                        trans.DEBIT = OPENING_BAL.Text;
                    else
                        trans.DEBIT = "0";
                    trans.CREDIT = "0";
                    trans.SYSTEMTIME = DateTime.Now.ToString("MM/dd/yyyy");
                    trans.BRANCH = lg.Branch;
                    trans.insertTransaction();

                  /*  trans.PARTICULARS = DESC_ENG.Text;
                    trans.ACCNAME = "SUSPENSE ACCOUNT";
                    trans.VOUCHERNO = "";
                    if (dt.Rows.Count > 0)
                        trans.ACCID = "202";
                    if (OPENING_BAL.Text != "")
                        trans.CREDIT = OPENING_BAL.Text;
                    else
                        trans.CREDIT = "0";
                    trans.DEBIT = "0";
                    trans.SYSTEMTIEM = DateTime.Now.ToString("MM/dd/yyyy");
                    trans.BRANCH = lg.Branch;
                    trans.insertTransaction();*/
                }
            }
            else
            {
                DeleteTrans();
                if (OPENTYPE.Text == "CR")
                {
                    trans.ACCNAME = DESC_ENG.Text;
                    trans.PARTICULARS = "SUSPENSE ACCOUNT";
                    trans.VOUCHERNO = "";
                    if (dt.Rows.Count > 0)
                        trans.ACCID = UpdateLedgerId;
                    if (OPENING_BAL.Text != "")
                        trans.CREDIT = OPENING_BAL.Text;
                    else
                        trans.CREDIT = "0";
                    trans.DEBIT = "0";
                    trans.SYSTEMTIME = DateTime.Now.ToString();
                    trans.BRANCH = lg.Branch;
                    trans.insertTransaction();

                   /* trans.PARTICULARS = DESC_ENG.Text;
                    trans.ACCNAME = "SUSPENSE ACCOUNT";
                    trans.VOUCHERNO = "";
                    if (dt.Rows.Count > 0)
                        trans.ACCID = "202";
                    if (OPENING_BAL.Text != "")
                        trans.DEBIT = OPENING_BAL.Text;
                    else
                        trans.DEBIT = "0";
                    trans.CREDIT = "0";
                    trans.SYSTEMTIEM = DateTime.Now.ToString();
                    trans.BRANCH = lg.Branch;
                    trans.insertTransaction();*/
                }
                else
                {
                    trans.ACCNAME = DESC_ENG.Text;
                    trans.PARTICULARS = "SUSPENSE ACCOUNT";
                    trans.VOUCHERNO = "";
                    if (dt.Rows.Count > 0)
                        trans.ACCID = UpdateLedgerId;

                    if (OPENING_BAL.Text != "")
                        trans.DEBIT = OPENING_BAL.Text;
                    else
                        trans.DEBIT = "0";
                    trans.CREDIT = "0";
                    trans.SYSTEMTIME = DateTime.Now.ToString();
                    trans.BRANCH = lg.Branch;
                    trans.insertTransaction();

                   /* trans.PARTICULARS = DESC_ENG.Text;
                    trans.ACCNAME = "SUSPENSE ACCOUNT";
                    trans.VOUCHERNO = "";
                    if (dt.Rows.Count > 0)
                        trans.ACCID = UpdateLedgerId;
                    if (OPENING_BAL.Text != "")
                        trans.CREDIT = OPENING_BAL.Text;
                    else
                        trans.CREDIT = "0";
                    trans.DEBIT = "0";
                    trans.SYSTEMTIEM = DateTime.Now.ToString();
                    trans.BRANCH = lg.Branch;
                    trans.insertTransaction();*/
                }
            }
        }

        public void DeleteTrans()
        {
            try
            {
                trans.ACCID = UpdateLedgerId;
                trans.VOUCHERTYPE = "Opening Balance";
                trans.OldName = OldName;
                trans.DeleteTransaction();

            }
            catch
            {
            }
        }
        void countryDetails()
        {
            DataTable country = General.GetCountryDetails();
            if (country.Rows.Count > 0 && (country.Rows[0][0].ToString() != "" || country.Rows[0][1].ToString() != ""))
            {

                int a = lblState.Size.Width;
                string statelbl = country.Rows[0][0].ToString();
                string tinNum = country.Rows[0][1].ToString();
                lblState.Text = (!statelbl.Equals("")) ? "City/" + statelbl.First().ToString().ToUpper() + statelbl.Substring(1).ToLower() + ":" : lblState.Text;
                LB_TINNO.Text = (!tinNum.Equals("")) ? tinNum + ":" : LB_TINNO.Text;

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
        private void btnClear_Click(object sender, EventArgs e)
        {
            BANK.Text = "";
            BANKBRANCH.Text = "";
            ACCOUNTNO.Text = "";
            IFCCODE.Text = "";
            OldName = "";

            ID = "";
          
            TYPE.Text = "";
            DESC_ENG.Text = "";
            DESC_ARB.Text = "";
            ADDRESS_A.Text = "";
            ADDRESS_B.Text = "";
            POBOX.Text = "";
            TELE1.Text = "";
            TELE2.Text = "";
            MOBILE.Text = "";
            EMAIL.Text = "";
            CITY_CODE.Text = "";
            CITY_NAME.Text = "";
            COUNTRY_CODE.Text = "";
            COUNTRY_NAME.Text = "";
            OPENING_BAL.Text = "";
            FAX.Text = "";
            DATE_GRE.Value = DateTime.Today;
            INITIAL.Text = "";
            CONTACT_PERSON.Text = "";
            TINNO.Text = "";
            //if (DEFAULT_CURRENCY.Items.Count > 0)
            //{
            //    DEFAULT_CURRENCY.SelectedIndex = 0;
            //}
            GetBranchDetails();
            UpdateLedgerId = "";
            Drp_repeat.SelectedIndex = -1;
            CODE.Focus();
            CODE.Text = generateItemCode();
            TXT_DEBIT_AMOUNT.Text = "";
            loadStates();
            DESC_ENG.Focus();
            cmb_Customer.SelectedIndex = 0;
            chk_cust.Checked = false;
           
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //--> cmd.CommandText = "SELECT ISNULL(COUNT(TRANSACTIONID), 0) FROM tb_Transactions WHERE ACCID = @id";
            //--> cmd.Parameters.Clear();
            //--> cmd.Parameters.AddWithValue("@id", UpdateLedgerId);
            paysupdb.LedgerId = UpdateLedgerId;
           // conn.Open();
            int count = Convert.ToInt32(paysupdb.geTtransactionId());
           // conn.Close();
            if (count >= 1)
            {
                if (count == 1)
                {
                    //--> cmd.CommandText = "SELECT DEBIT, CREDIT FROM tb_Transactions WHERE ACCID = @id";
                    //--> cmd.Parameters.Clear();
                    //--> cmd.Parameters.AddWithValue("@id", UpdateLedgerId);
                    paysupdb.LedgerId = UpdateLedgerId;
                  //  conn.Open();
                    SqlDataReader r = paysupdb.getDebitCredit();
                    double debit = 0; 
                    double credit = 0;
                    while (r.Read())
                    {
                        debit = Convert.ToDouble(r[0]);
                        credit = Convert.ToDouble(r[1]);
                    }
                    DbFunctions.CloseConnection();
                    if (debit > 0 || credit > 0)
                    {
                        MessageBox.Show("Sorry!, you can't delete the supplier which has transactions.");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Sorry!, you can't delete the supplier which has transactions.");
                    return;
                }

            }
            if (ID != "")
            {
                if (MessageBox.Show("Are you sure?","Supplier Deletion",MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        //if (conn.State == ConnectionState.Open)
                        //{
                        //}
                        //else
                        //{

                        //    conn.Open();
                        //}
                     
                       //--> cmd.CommandText = "DELETE FROM PAY_SUPPLIER WHERE CODE = '"+ID+"'";
                        paysupdb.Code = ID;
                        //--> cmd.ExecuteNonQuery();
                        paysupdb.deletePaySupplier();
                        tableCustomers.Select("CODE = '" + ID + "'").First().Delete();
                        MessageBox.Show("Supplier Deleted!");
                        btnClear.PerformClick();
                        
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    //finally
                    //{
                    //    conn.Close();
                    //}
                }
            }
            else
            {
                MessageBox.Show("First select a supplier to delete!");
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
            }
        }
        private void dgSuppliers_Click(object sender, EventArgs e)
        {
            if (dgSuppliers.Rows.Count > 0 && dgSuppliers.CurrentRow != null)
            {
                try
                {
                    DataGridViewCellCollection c = dgSuppliers.CurrentRow.Cells;
                    if (checkIsCustomer(Convert.ToInt32(c["ledgerid"].Value)))
                    {
                        MessageBox.Show("Can't Update,It is a Customer!");
                        return;
                    }
                    ledId = Convert.ToString(c["LedgerId"].Value);
                    ID = Convert.ToString(c["CODE"].Value);
                    CODE.Text = ID;
                    if (TYPE.Items.Count > 0)
                    {
                        TYPE.SelectedValue = c["TYPE"].Value;
                    }
                    DESC_ENG.Text = Convert.ToString(c["DESC_ENG"].Value);
                    OldName = Convert.ToString(c["DESC_ENG"].Value);
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
                    COUNTRY_CODE.Text = Convert.ToString(c["COUNTRY_CODE"].Value);
                    OPENING_BAL.Text = Convert.ToString(c["OPENING_BAL"].Value);
                    FAX.Text = Convert.ToString(c["FAX"].Value);
                    TXT_DEBIT_AMOUNT.Text = Convert.ToString(c["DEBIT_AMOUNT"].Value);
                    if (c["SUPPLIER_STATE"].Value == "")
                        cmb_state.SelectedIndex = 0;
                    else
                        cmb_state.SelectedValue = c["SUPPLIER_STATE"].Value;
                    string DAYS = Convert.ToString(c["DEBIT_PERIOD_TYPE"].Value);
                   // DAYS = string.Concat(DAYS.Reverse().Skip(5).Reverse());
                    if (CHECK_DEBIT_PERIOD.Checked)
                    {
                        TXT_DEBIT_PERIOD.Text = DAYS;
                    }



                    string debit_active = Convert.ToString(c["DEBIT_LIMIT"].Value);
                    if (debit_active == "True")
                    {
                        CHECK_DEBIT_LIMIT.Checked = true;
                    }
                    else
                    {
                        CHECK_DEBIT_LIMIT.Checked = false;
                    }

                    string debit_period = Convert.ToString(c["D_PERIODACTIVE"].Value);
                    if (debit_period == "True")
                    {
                        CHECK_DEBIT_PERIOD.Checked = true;
                    }
                    else
                    {
                        CHECK_DEBIT_PERIOD.Checked = false;
                    }


                    string supplier = Convert.ToString(c["Supplier_Status"].Value);
                    if (supplier == "True")
                    {
                        Active_Supplier.Checked = true;
                    }
                    else
                    {
                        Active_Supplier.Checked = false;
                    }



                    try
                    {
                        DATE_GRE.Text = c["DATE_GRE"].Value.ToString();
                    }
                    catch (Exception)
                    {
                        
                        
                    }
                  
                    INITIAL.Text = Convert.ToString(c["INITIAL"].Value);
                    CreateBankDetails(Convert.ToString(c["NOTES"].Value));
                    if (DEFAULT_CURRENCY.Items.Count > 0)
                    {
                        DEFAULT_CURRENCY.SelectedValue = c["DEFAULT_CURRENCY"].Value;
                    }
                    CONTACT_PERSON.Text = Convert.ToString(c["CONTACT_PERSON"].Value);
                    TINNO.Text = Convert.ToString(c["GSTN"].Value);
                    UpdateLedgerId = (c["LedgerId"].Value).ToString();
                    GetSupplierCollectionDetails();
                }
                catch(Exception EXC)
                {
                    MessageBox.Show(EXC.Message);
                }
                selecttransaction();
            }
        }

        public void GetSupplierCollectionDetails()
        {
            DataTable dt = new DataTable();
            dt = colday.GetSupplierDetails(CODE.Text);
            if (dt.Rows.Count > 0)
            {
                switch (dt.Rows[0]["Repeat"].ToString())
                {

                    case "Weekly":

                        try
                        {
                            Drp_repeat.Text = dt.Rows[0]["Repeat"].ToString();
                            drp_repeatModeDay.Text = dt.Rows[0]["RepeatMode"].ToString();
                            DrpDay.Text = dt.Rows[0]["Day"].ToString();
                        }
                        catch { }


                        break;
                    case "Monthly":

                        try
                        {
                            Drp_repeat.Text = dt.Rows[0]["Repeat"].ToString();
                            Drop_repeatmodeMonth.Text = dt.Rows[0]["RepeatMode"].ToString();
                            RepeatDate.Value = Convert.ToDateTime(dt.Rows[0]["Date"].ToString());

                        }
                        catch { }

                        break;

                    default:

                        Drp_repeat.SelectedIndex = -1;
                        break;
                }
            }
            else
            {
                Drp_repeat.SelectedIndex = -1;
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
                    MessageBox.Show("Incorrect City Code!");
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
                    MessageBox.Show("Incorrect Country Code!");
                }
            }
        }

        private void codCity_Click(object sender, EventArgs e)
        {
            CommonHelp h = new CommonHelp(0, genEnum.City);
            if (h.ShowDialog() == DialogResult.OK && h.c != null)
            {
                CITY_CODE.Text = Convert.ToString(h.c[0].Value);
                CITY_NAME.Text = Convert.ToString(h.c[1].Value);
                cmb_state.Focus();
            }
        }

        private void codeCountry_Click(object sender, EventArgs e)
        {
            CommonHelp h = new CommonHelp(0, genEnum.Country);
            if (h.ShowDialog() == DialogResult.OK && h.c != null)
            {
                COUNTRY_CODE.Text = Convert.ToString(h.c[0].Value);
                COUNTRY_NAME.Text = Convert.ToString(h.c[1].Value);
                OPENING_BAL.Focus();
            }
        }

        private void CODE_KeyDown(object sender, KeyEventArgs e)
        {
             if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (sender is KryptonTextBox)
                {
                    string name = (sender as KryptonTextBox).Name;
                    switch (name)
                    {

                        case "CODE":
                           // DESC_ENG.Focus();
                            TYPE.Focus();
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
                            ADDRESS_B.Focus();
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
                        case "TELE2":
                            MOBILE.Focus();
                            break;
                        case "MOBILE":
                            TINNO.Focus();
                            break;
                        case "TINNO":
                            FAX.Focus();
                            break;
                        case "FAX":
                            // EMAIL.Focus();
                            EMAIL.Focus();
                            break;

                        case "INITIAL":
                            EMAIL.Focus();
                            break;
                        case "EMAIL":
                            CITY_CODE.Focus();
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
                        case "OPENING_BAL":
                            OPENTYPE.Focus();
                            break;
                        case "OPENTYPE":
                            DATE_GRE.Focus();
                            break;

                        case "CONTACT_PERSON":
                            DEFAULT_CURRENCY.Focus();
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
                        case "DEFAULT_CURRENCY":


                            if (CHECK_DEBIT_PERIOD.Checked)
                                TXT_DEBIT_PERIOD.Focus();
                            else if (CHECK_DEBIT_LIMIT.Checked)
                                TXT_DEBIT_AMOUNT.Focus();
                            else
                                Drp_repeat.Focus();
                           
                            break;
                        case "Drp_repeat":
                            if (Drp_repeat.Text == "None")
                            {
                                btnSave.Focus();
                            }
                            else if (Drp_repeat.Text == "Monthly")
                            {
                                Drop_repeatmodeMonth.Focus();
                            }
                            else if (Drp_repeat.Text == "Weekly")
                            {
                                drp_repeatModeDay.Focus();
                            }
                            else
                            {
                                btnSave.Focus();
                            }
                            break;
                        case "Drop_repeatmodeMonth":

                            RepeatDate.Focus();
                            break;
                        case "drp_repeatModeDay":

                            DrpDay.Focus();
                            break;

                        case "DrpDay":
                            btnSave.Focus();
                            break;
                        case "OPENTYPE":
                            DATE_GRE.Focus();
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
                        case "RepeatDate":
                            btnSave.Focus();
                            break;
                       
                        default:
                            break;
                    }
                }
                Common.preventDingSound(e);
            }
        
        }

        private void Drp_repeat_SelectedIndexChanged(object sender, EventArgs e)
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
                colday.AddCollection();
            }
        }

        private void codeCountry_KeyDown(object sender, KeyEventArgs e)
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
                codCity.PerformClick();
            }
        }

        private void COUNTRY_CODE_KeyDown_1(object sender, KeyEventArgs e)
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
                codeCountry.PerformClick();
            }
        }

        private void Drop_repeatmodeMonth_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                RepeatDate.Focus();
            }
        }

        private void dgSuppliers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            int index = e.RowIndex;
            dgSuppliers.Rows[index].Selected = true;
        }

        private void DESC_ENG_Leave(object sender, EventArgs e)
        {
            DESC_ENG.Text = DESC_ENG.Text.ToUpper();
        }

        private void panelMonth_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelDay_Paint(object sender, PaintEventArgs e)
        {

        }

        private void kryptonLabel21_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CMB_SUP_AMOUNT_SelectedIndexChanged(object sender, EventArgs e)
        {
            //switch (CMB_SUP_PERIOD.Text)
            //{
            //    case "None":
            //        CMB_SUP_DAY.Visible = false;
            //        CMB_SUP_MONTHLY.Visible = false;
            //        break;
            //    case "Day":
            //        CMB_SUP_DAY.Visible = true;
            //        CMB_SUP_MONTHLY.Visible = false;
            //        break;
            //    case "Monthly":
            //        CMB_SUP_MONTHLY.Visible = true;
            //        CMB_SUP_DAY.Visible = false;
            //        break;
            //}
        }

        private void CHECK_DEBIT_LIMIT_CheckedChanged(object sender, EventArgs e)
        {
            if (CHECK_DEBIT_LIMIT.Checked)
            {
                TXT_DEBIT_AMOUNT.Enabled = true;
                //LB_DEBIT_AMOUNT.Visible = true;
            }
            else
            {
                TXT_DEBIT_AMOUNT.Enabled = false;
              //  LB_DEBIT_AMOUNT.Visible = false;
            }
        }

        private void CHECK_DEBIT_PERIOD_CheckedChanged(object sender, EventArgs e)
        {
            if (CHECK_DEBIT_PERIOD.Checked)
            {
                TXT_DEBIT_PERIOD.Enabled = true;
              //  LB_DEBIT_PERIOD.Visible = true;
            }
            else
            {
              //  LB_DEBIT_PERIOD.Visible = false;
                TXT_DEBIT_PERIOD.Enabled = false;
            }
        }

        private void Active_Supplier_CheckedChanged(object sender, EventArgs e)
        {
            if (Active_Supplier.Checked == true)
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

        private void cmb_state_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BANK.Focus();
            }
        }

        private void TXT_DEBIT_PERIOD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (CHECK_DEBIT_LIMIT.Checked)
                    TXT_DEBIT_AMOUNT.Focus();
                else
                    Drp_repeat.Focus();
            }
        }

        private void TXT_DEBIT_AMOUNT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
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


        void bindCustomer()
        {
            DataTable dtTable = new DataTable();
           // conn.Open();
           //--> cmd = new SqlCommand("SELECT DESC_ENG,LEDGERID FROM REC_CUSTOMER", conn);


           //--> SqlDataAdapter adp = new SqlDataAdapter(cmd);
           //--> adp.Fill(dtTable);
           dtTable= paysupdb.selectDescEngLedgerIdBind();
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
                DataTable dtTable = new DataTable();
               // conn.Open();
                //--> cmd = new SqlCommand("SELECT * FROM REC_CUSTOMER where LEDGERID="+Convert.ToInt32(cmb_Customer.SelectedValue), conn);
               //-->  SqlDataAdapter adp = new SqlDataAdapter(cmd);
               //-->  adp.Fill(dtTable);
                paysupdb.LedgerId = cmb_Customer.SelectedValue.ToString();
                dtTable=paysupdb.getRecCustomer();
              //  conn.Close();

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
        bool checkIsCustomer(int LEDGERID)
        {
            bool isCustomer = false;
           // conn.Open();
            //--> cmd = new SqlCommand("SELECT UNDER FROM tb_Ledgers WHERE ledgerid=" + LEDGERID, conn);
            clsCustomer cls = new clsCustomer();
           //--> string under = cmd.ExecuteScalar().ToString();
            cls.LedgerId = LEDGERID.ToString();
            string under = cls.chkIsSupplier().ToString();
          //  conn.Close();
            isCustomer = under == "13" ? false : true;
            return isCustomer;

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

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


        
    }
}
