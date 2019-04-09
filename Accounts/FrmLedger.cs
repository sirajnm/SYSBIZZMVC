using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using System.Data;
using System.Data.SqlClient;
using Sys_Sols_Inventory.Model;
using Sys_Sols_Inventory.Class;


namespace Sys_Sols_Inventory.Accounts
{
    public partial class FrmLedger : Form
    {
        clsCustomer clscusObj = new clsCustomer();
        PaySupplierDB paysupdbobj = new PaySupplierDB();
        TbTransactionsDB transObj = new TbTransactionsDB();
        Ledgers ldgObj = new Ledgers();

        //private SqlDataAdapter adapter = new SqlDataAdapter();
        Class.AccountGroup accgrp = new Class.AccountGroup();
        Class.Ledgers led = new Class.Ledgers();
        Class.Transactions trans = new Class.Transactions();
        private BindingSource source = new BindingSource();
        //private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        //private SqlCommand cmd = new SqlCommand();
        private DataTable tableFilter = new DataTable();
        private SqlDataAdapter adapterType = new SqlDataAdapter();
        private DataTable tableType = new DataTable();
        Login lg = (Login)Application.OpenForms["Login"];
        Initial mdi = (Initial)Application.OpenForms["Initial"];
        public int flag1 = 0;
        private string ID = "";
        private string Cust_code = "";
        int LedgerId;

        public FrmLedger()
        {
            InitializeComponent();
            //cmd.Connection = conn;
            //adapter.SelectCommand = cmd;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
                if (keyData == (Keys.Control | Keys.S))
                {
                       btnSave.PerformClick();
                        return true;
                }
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
                return base.ProcessCmdKey(ref msg, keyData);
        }
        
        public int GetMaxLedger()
        {
            return led.MaxLedGerid();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (valid())
            {
                led.LEDGERNAME = LEDGERNAME.Text;
                led.UNDER = UNDER.SelectedValue.ToString();
                led.ADRESS = "";
                led.TIN = "";
                led.CST = "";
                led.PIN = "";
                led.PHONE = "";
                led.MOBILE = "";
                led.EMAIL = "";
                led.CREDITPERIOD = CREDITPERIOD.Text;
                led.CREDITAMOUNT = CREDITLIMIT.Text;
                led.DISPLAYNAME = DISPLAYNAME.Text;
                led.BANK = BANK.Text;
                led.BANKBRANCH = BANKBRANCH.Text;
                led.IFCCODE = IFCCODE.Text;
                led.ACCOUNTNO = ACCOUNTNO.Text;
                led.ISBUILTIN = (ISBUILDIN.Checked) ? "Y" : "N";
                String saveLedgerID = null;
                String typeCode = null;
                // String defaultCurrency = Convert.ToString(DEFAULT_CURRENCY.SelectedValue);
                if (UNDER.SelectedValue.ToString() == "13")
                {
                    typeCode = Convert.ToString(cmbSUPTYPE.SelectedValue);
                }
                else if (UNDER.SelectedValue.ToString() == "14")
                    typeCode = Convert.ToString(cmbCUSTTYPE.SelectedValue);
                // String openingBalance = (OPENING_BAL.Text.Trim() != "") ? OPENING_BAL.Text : "0";
                String creditAcive = IN_ACTIVE.Checked.ToString();
                //    String active_customer = Active_Customer.Checked.ToString();
                //   String salesman = cmb_salesman.SelectedIndex > 0 ? cmb_salesman.SelectedValue.ToString() : "";
                String STATE = cmb_state.SelectedIndex > 0 ? cmb_state.SelectedValue.ToString() : "";
                String credit_period_limit = CHECK_CREDIT_PERIOD.Checked.ToString();
                String cPerson = "";
                String initial = "";


                string code = generateItemCode();
                string sucode = generateItemCodeSup();
                double openingBalance = 0;
                Dictionary <string ,object>parameters=new Dictionary<string,object> (); 
                if (ID == "")
                {
                    if (!OPENING_BAL.Text.Equals(""))
                    {
                        openingBalance = Convert.ToDouble(OPENING_BAL.Text);
                    }
                }
                try
                {
                   // cmd.Parameters.Clear();
                    parameters.Add("@type", typeCode);
                    if (UNDER.SelectedValue.ToString() == "13")
                    {
                        if (ID == "")
                            parameters.Add("@code", sucode);
                        else
                            parameters.Add("@code", Cust_code);
                    }
                    else if (UNDER.SelectedValue.ToString() == "14")
                        if (ID == "")
                            parameters.Add("@code", code);
                        else
                            parameters.Add("@code", Cust_code);

                    parameters.Add("@desc_eng", LEDGERNAME.Text);
                    parameters.Add("@desc_arb", "");
                    parameters.Add("@address_a", ADDRESS_A.Text);
                    parameters.Add("@address_b", "");
                    parameters.Add("@pobox", "");
                    parameters.Add("@tele1", TELE1.Text);
                    parameters.Add("@tele2", "");
                    parameters.Add("@mobile", MOBILE.Text);
                    parameters.Add("@fax", FAX.Text);
                    parameters.Add("@email", EMAIL.Text);
                    parameters.Add("@city_code", CITY_CODE.Text);
                    parameters.Add("@country_code", "");
                    parameters.Add("@opening_bal", openingBalance);
                    parameters.Add("@date_gre", DATE_GRE.Value.ToString("yyyy/MM/dd"));
                    parameters.Add("@salesman_code", "");
                    parameters.Add("@default_currency", "");
                    //  cmd.Parameters.AddWithValue("@ledgerid", saveLedgerID);
                    parameters.Add("@creditactive", creditAcive);
                    parameters.Add("@notes", "");
                    parameters.Add("@tin_no", TINNO.Text);
                    parameters.Add("@customer_status", "true");
                    parameters.Add("@credit_amount", (!CREDITLIMIT.Text.Equals("") ? CREDITLIMIT.Text : "0"));
                    parameters.Add("@cperiodactive", credit_period_limit);
                    parameters.Add("@credit_period", (!CREDITPERIOD.Text.Equals("") ? CREDITPERIOD.Text : "0"));
                    parameters.Add("@STATE", STATE);
                }
                catch
                {
                }
                string query = "";
                if (ID == "")
                {
                    string ledID = led.insertLedger();
                    LedgerId = Convert.ToInt32(ledID);
                    insertTransaction(ledID);
                    
                    if (!OPENING_BAL.Text.Equals(""))
                    {
                        openingBalance = Convert.ToDouble(OPENING_BAL.Text);
                    }
                    if (UNDER.SelectedValue.ToString() == "13" || UNDER.SelectedValue.ToString() == "14")
                    {
                        if (UNDER.SelectedValue.ToString() == "13")
                        {
                            if (CREDITPERIOD.Text != "")
                            {
                                CREDITPERIOD.Text = CREDITPERIOD.Text;
                            }
                            //cmd.CommandText = "INSERT INTO REC_CUSTOMER(CODE,DESC_ENG,DATE_GRE) VALUES('" + code + "','" + LEDGERNAME.Text + "', '" + DATE_GRE.Value.ToString("yyyy/MM/dd") + "')";
                            // cmd.CommandText = "INSERT INTO PAY_SUPPLIER(CODE, DESC_ENG, DATE_GRE, OPENING_BAL,Supplier_Status,TYPE,DEFAULT_CURRENCY, LedgerId, DEBIT_LIMIT, DEBIT_AMOUNT, D_PERIODACTIVE, DEBIT_PERIOD_TYPE)"
                            //  + "VALUES('" + sucode + "', '" + LEDGERNAME.Text + "', '" + DATE_GRE.Value.ToString("yyyy/MM/dd") + "', '" + openingBalance + "','True','DST','INR','" + LedgerId + "','True','" + CREDITLIMIT.Text + "','True','" + CREDITPERIOD.Text + "')";
                            try
                            {
                                query = "INSERT INTO PAY_SUPPLIER(CODE,TYPE,DESC_ENG,DESC_ARB,ADDRESS_A,ADDRESS_B,POBOX,TELE1,TELE2,MOBILE,EMAIL,CITY_CODE,COUNTRY_CODE,OPENING_BAL,FAX,DATE_GRE,INITIAL,DEFAULT_CURRENCY,CONTACT_PERSON,LedgerId,NOTES,Supplier_Status,DEBIT_LIMIT,DEBIT_AMOUNT,D_PERIODACTIVE,DEBIT_PERIOD_TYPE,STATE,TIN_NO)";
                                query += "values(@code,@type, @desc_eng, @desc_arb, @address_a, @address_b, @pobox,@tele1, @tele2, @mobile,@email, @city_code, @country_code, @opening_bal,@fax, @date_gre,'" + initial + "', @default_currency,'" + cPerson + "','" + LedgerId + "',@notes, @customer_status, @creditactive,@credit_amount, @cperiodactive,@credit_period,@STATE,@tin_no)";

                            }
                            catch (Exception t)
                            {

                            }
                        }
                        if (UNDER.SelectedValue.ToString() == "14")
                        {
                            if (CREDITPERIOD.Text != "")
                            {
                                CREDITPERIOD.Text = CREDITPERIOD.Text;
                            }
                            query = "INSERT INTO REC_CUSTOMER(TYPE,CODE,DESC_ENG,DESC_ARB,ADDRESS_A,ADDRESS_B,POBOX,TELE1,TELE2,MOBILE,FAX,EMAIL,CITY_CODE,COUNTRY_CODE,OPENING_BAL,DATE_GRE,SALESMAN_CODE,DEFAULT_CURRENCY,LedgerId,CreditActive,NOTES,TIN_NO,CUSTOMER_STATUS,CREDIT_AMOUNT,CperiodActive,CREDIT_PERIOD,STATE)";
                            query += "VALUES(@type, @code, @desc_eng, @desc_arb, @address_a, @address_b, @pobox, " +
                                "@tele1, @tele2, @mobile, @fax, @email, @city_code, @country_code, @opening_bal, @date_gre, " +
                                "@salesman_code, @default_currency,'" + LedgerId + "', @creditactive, @notes, @tin_no, @customer_status, " +
                                "@credit_amount, @cperiodactive, @credit_period,@STATE)";
                        }
                        try
                        {
                           // conn.Open();
                           // cmd.ExecuteNonQuery();
                           // cmd.Parameters.Clear();
                           // cmd.CommandText = "";
                            DbFunctions.InsertUpdate(query,parameters);
                            
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
                        MessageBox.Show("Ledger Added!");
                        code = "";
                    
                }
                else
                {
                    led.LEDGERID = Convert.ToInt16(ID);
                    led.UpdateLedger();
                    DeleteTrans(ID);
                    DataTable dt = new DataTable();
                    insertTransaction(ID);

                    if (UNDER.SelectedValue.ToString() == "13" || UNDER.SelectedValue.ToString() == "14")
                    {
                        LedgerId = led.LEDGERID;
                        if (UNDER.SelectedValue.ToString() == "13")
                        {
                           // cmd.Parameters.Clear();
                            query = "DELETE FROM PAY_SUPPLIER WHERE LedgerId='" + led.LEDGERID + "';";
                            //  cmd.CommandText += "INSERT INTO PAY_SUPPLIER(CODE, DESC_ENG, DATE_GRE, OPENING_BAL,Supplier_Status,TYPE,DEFAULT_CURRENCY, LedgerId) VALUES('" + sucode + "', '" + LEDGERNAME.Text + "', '" + DATE_GRE.Value.ToString("yyyy/MM/dd") + "', '" + openingBalance + "','True','DST','INR','" + led.LEDGERID + "')";
                            query += "INSERT INTO PAY_SUPPLIER(CODE,TYPE,DESC_ENG,DESC_ARB,ADDRESS_A,ADDRESS_B,POBOX,TELE1,TELE2,MOBILE,EMAIL,CITY_CODE,COUNTRY_CODE,OPENING_BAL,FAX,DATE_GRE,INITIAL,DEFAULT_CURRENCY,CONTACT_PERSON,LedgerId,NOTES,Supplier_Status,DEBIT_LIMIT,DEBIT_AMOUNT,D_PERIODACTIVE,DEBIT_PERIOD_TYPE,STATE,TIN_NO)";
                            query += "values(@code,@type, @desc_eng, @desc_arb, @address_a, @address_b, @pobox,@tele1, @tele2, @mobile,@email, @city_code, @country_code, @opening_bal,@fax, @date_gre,'" + initial + "', @default_currency,'" + cPerson + "','" + LedgerId + "',@notes, @customer_status, @creditactive,@credit_amount, @cperiodactive,@credit_period,@STATE,@tin_no)";
                        }
                        if (UNDER.SelectedValue.ToString() == "14")
                        {
                            //cmd.Parameters.Clear();
                            query = "DELETE FROM REC_CUSTOMER WHERE LedgerId='" + led.LEDGERID + "';";
                            // cmd.CommandText += "INSERT INTO REC_CUSTOMER(CODE,DESC_ENG,DATE_GRE, OPENING_BAL,CUSTOMER_STATUS,TYPE,DEFAULT_CURRENCY,LedgerId) VALUES('" + code + "','" + LEDGERNAME.Text + "', '" + DATE_GRE.Value.ToString("yyyy/MM/dd") + "', '" + openingBalance + "','True','REG','INR', '" + led.LEDGERID + "')";
                            query += "INSERT INTO REC_CUSTOMER(TYPE,CODE,DESC_ENG,DESC_ARB,ADDRESS_A,ADDRESS_B,POBOX,TELE1,TELE2,MOBILE,FAX,EMAIL,CITY_CODE,COUNTRY_CODE,OPENING_BAL,DATE_GRE,SALESMAN_CODE,DEFAULT_CURRENCY,LedgerId,CreditActive,NOTES,TIN_NO,CUSTOMER_STATUS,CREDIT_AMOUNT,CperiodActive,CREDIT_PERIOD,STATE)";
                            query += "VALUES(@type, @code, @desc_eng, @desc_arb, @address_a, @address_b, @pobox, " +
                                "@tele1, @tele2, @mobile, @fax, @email, @city_code, @country_code, @opening_bal, @date_gre, " +
                                "@salesman_code, @default_currency,'" + LedgerId + "', @creditactive, @notes, @tin_no, @customer_status, " +
                                "@credit_amount, @cperiodactive, @credit_period,@STATE)";
                        }
                      //  conn.Open();
                      //  cmd.Connection = conn;
                      //  cmd.ExecuteNonQuery();
                      //  cmd.Parameters.Clear();
                      //  cmd.CommandText = "";
                      //  conn.Close();
                        DbFunctions.InsertUpdate(query,parameters);
                    }
                    MessageBox.Show("Ledger Updated!");
                }
                    btnClear.PerformClick();
                    GetData();
                    LEDGERNAME.Focus();
                
            }
        }
        public void BindCustType()
        {
            DataTable dt = new DataTable();
           // SqlDataAdapter ad = new SqlDataAdapter();
          //  cmd.CommandText = "SELECT CODE,CODE+' - '+DESC_ENG AS DESC_ENG FROM REC_CUSTOMER_TYPE";
          //  ad.SelectCommand = cmd;
          //  ad.Fill(dt);
            dt = clscusObj.getCustType();
            cmbCUSTTYPE.DataSource = dt;
           cmbCUSTTYPE.DisplayMember = "DESC_ENG";
           cmbCUSTTYPE.ValueMember = "CODE";
        }
        public void BindSupType()
        {
            //cmd.CommandText = "SELECT CODE,CODE+' - '+DESC_ENG AS DESC_ENG FROM PAY_SUPPLIER_TYPE";
           // adapterType.SelectCommand = cmd;
           // adapter.Fill(tableType);

           tableType = paysupdbobj.getSupplierData();
           cmbSUPTYPE.DataSource = tableType;
           cmbSUPTYPE.DisplayMember = "DESC_ENG";
           cmbSUPTYPE.ValueMember = "CODE";
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
        public void DeleteTrans(String ledgerID)
        {
            trans.ACCID = ledgerID;
            trans.VOUCHERTYPE = "Opening Balance";
            trans.DeleteTransaction();
        }


        private void insertTransaction(String ledgerID)
        {
            double openBalance = 0;
            try
            {
                openBalance = Convert.ToDouble(OPENING_BAL.Text);
            }
            catch (Exception){}

            if (openBalance <= 0)
            {
                return;
            }
            trans.VOUCHERTYPE = "Opening Balance";
            trans.DATED = DATE_GRE.Value.ToString();
            trans.NARRATION = "Opening Balance";
            Login log = (Login)Application.OpenForms["Login"];
            trans.USERID = log.EmpId;

            if (OPENTYPE.Text == "CR")
            {
                trans.ACCNAME = LEDGERNAME.Text;
                trans.PARTICULARS = "OPENING BALANCE";
                trans.VOUCHERNO = "";
                trans.ACCID = ledgerID;
                if (OPENING_BAL.Text != "")
                    trans.CREDIT = OPENING_BAL.Text;
                else
                    trans.CREDIT = "0";
                trans.DEBIT = "0";
                trans.BRANCH = lg.Branch;
                trans.SYSTEMTIME = DateTime.Now.ToString();
                trans.insertTransaction();

                /*trans.PARTICULARS = LEDGERNAME.Text;
                trans.ACCNAME = "SUSPENSE ACCOUNT";
                trans.VOUCHERNO = "";
                trans.BRANCH = lg.Branch;
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
                trans.ACCNAME = LEDGERNAME.Text;
                trans.PARTICULARS = "OPENING BALANCE";
                trans.VOUCHERNO = "";
                trans.ACCID = ledgerID;
                if (OPENING_BAL.Text != "")
                    trans.DEBIT = OPENING_BAL.Text;
                else
                    trans.DEBIT = "0";
                trans.CREDIT = "0";
                trans.BRANCH = lg.Branch;
                trans.SYSTEMTIME = DateTime.Now.ToString();

                trans.insertTransaction();

                /*trans.PARTICULARS = LEDGERNAME.Text;
                trans.ACCNAME = "SUSPENSE ACCOUNT";
                trans.VOUCHERNO = "";
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


        public DataTable openbal_check(string acid)
        {
            DataTable dt = new DataTable();
            string co = acid;
           // cmd.CommandText = "select * from tb_Transactions WHERE ((VOUCHERTYPE='Opening Balance')AND(ACCID='" + acid + "'))";
           // cmd.Connection = conn;
          //  adapter.SelectCommand = cmd;
          //  adapter.Fill(dt);
            transObj.AccId = acid;
            dt = transObj.getAllDataByCondition();
            return dt;
        }

        public bool valid()
        {
            if (LEDGERNAME.Text == "" || UNDER.Text == "")
            {
                MessageBox.Show("Enter ledger name and the account group it contains.");
                return false;
            }
            else
            {
                //check whether the name exists
               // cmd.Parameters.Clear();
              //  cmd.Parameters.AddWithValue("@LEDGERNAME", LEDGERNAME.Text);
              //  cmd.Parameters.AddWithValue("@LEDGERID", ID);
             //   cmd.CommandText = "SELECT LEDGERID FROM tb_Ledgers WHERE LEDGERNAME=@LEDGERNAME AND LEDGERID != @LEDGERID";
             //   conn.Open();
              //  String id = Convert.ToString(cmd.ExecuteScalar());
              //  conn.Close();
                ldgObj.LEDGERNAME = LEDGERNAME.Text;
               
                string id = "";
                if (ID != "")
                {
                    ldgObj.LEDGERID = Convert.ToInt16(ID);
                    id = Convert.ToString(ldgObj.checkNameExist());
                    if (!id.Equals(""))
                    {
                        MessageBox.Show("Ledger with same name already exists!");
                        return false;
                    }
                }
                else
                {
                    id = Convert.ToString(ldgObj.checkNameExistWithoutaId());
                    if (!id.Equals(""))
                    {
                        MessageBox.Show("Ledger with same name already exists!");
                        return false;
                    }
                }
                
               // cmd.Parameters.Clear();
                return true;
            }
        }

        public void getgrpname()
        {
            DataTable dt = new DataTable();
            UNDER.DisplayMember = "DESC_ENG";
            UNDER.ValueMember = "ACOUNTID";
            dt = accgrp.SelectAccountGroupName();
            DataRow row = dt.NewRow();
            dt.Rows.InsertAt(row, 0);
            UNDER.DataSource = dt;


        }

        public void GetData()
        {
            DataTable dt = new DataTable();
            dt = led.Selectledger();

            source.DataSource = dt;
            dgvLedger.DataSource = source;
            dgvLedger.Columns[0].Visible = false;
            dgvLedger.Columns[2].Visible = false;
        }

        public string generateItemCodeSup()
        {
           // cmd.Connection = conn;
           // conn.Open();
           // cmd.CommandType = CommandType.Text;
           // cmd.CommandText = "SELECT MAX(CODE) FROM PAY_SUPPLIER";
           // string id = Convert.ToString(cmd.ExecuteScalar());
            string id = Convert.ToString(paysupdbobj.getMaxCode());
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
          //  conn.Close();
            return id;
        }
       
        public string generateItemCode()
        {
            //cmd.Connection = conn;
            //conn.Open();
            //cmd.CommandType = CommandType.Text;
            //cmd.CommandText = "SELECT MAX(CODE) FROM REC_CUSTOMER";
            //string id = Convert.ToString(cmd.ExecuteScalar());
            string id = Convert.ToString(clscusObj.getCustId());
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
           // conn.Close();
            return id;
        }

        private void FrmLedger_Load(object sender, EventArgs e)
        {
            GetData();
            timer1.Start();
            getgrpname();
            BindCustType();
            BindSupType();
            loadStates();
            tableFilter.Columns.Add("key");
            tableFilter.Columns.Add("value");
            cmbFilter.DataSource = tableFilter;
            cmbFilter.DisplayMember = "value";
            cmbFilter.ValueMember = "key";

            tableFilter.Rows.Add("LEDGERNAME", "Ledger Name");
            tableFilter.Rows.Add("UNDER", "Parent Group");
            tableFilter.Rows.Add("PHONE", "Phone");
            tableFilter.Rows.Add("MOBILE", "Mobile");
            tableFilter.Rows.Add("EMAIL", "Email");
            tableFilter.Rows.Add("DISPLAYNAME", "Display Name");
            ActiveControl = LEDGERNAME;
            OPENING_BAL.KeyPress += new KeyPressEventHandler(General.NegFloat);
            pn_supp.Visible = false;
        }

        private void dgvLedger_Click(object sender, EventArgs e)
        {
            if (dgvLedger.Rows.Count > 0 && dgvLedger.CurrentRow != null)
            {
                btnClear.PerformClick();
                DataGridViewCellCollection c = dgvLedger.CurrentRow.Cells;
                ID = Convert.ToString(c["LEDGERID"].Value);
                LEDGERNAME.Text = Convert.ToString(c["LEDGERNAME"].Value);
                
                CREDITPERIOD.Text = Convert.ToString(c["CREDITPERIOD"].Value);
                CREDITLIMIT.Text = Convert.ToString(c["CREDITAMOUNT"].Value);
                DISPLAYNAME.Text = Convert.ToString(c["DISPLAYNAME"].Value);
                BANK.Text = Convert.ToString(c["BANK"].Value);
                BANKBRANCH.Text = Convert.ToString(c["BANKBRANCH"].Value);
                IFCCODE.Text = Convert.ToString(c["IFCCODE"].Value);
                ACCOUNTNO.Text = Convert.ToString(c["ACCOUNTNO"].Value);
                if (Convert.ToString(c["u"].Value) != "")
                {
                    UNDER.SelectedValue = Convert.ToString(c["u"].Value);
                }
              

                if (Convert.ToString(c["ISBULTIN"].Value) == "Y"||Convert.ToString(c["ISBULTIN"].Value) == "True")
                {
                    ISBUILDIN.Checked = true;
                }
                else
                {
                    ISBUILDIN.Checked = false;
                }
                selecttransaction();

                if (Convert.ToString(c["u"].Value) == "13")
                {
                    OPENTYPE.SelectedIndex = 0;
                    lblSupType.Visible = true;
                    lblCustType.Visible = false;
                    cmbCUSTTYPE.Visible = false;
                    cmbSUPTYPE.Visible = true;
                    selectSupplierPeriod();
                }

                if (Convert.ToString(c["u"].Value) == "14")
                {
                    OPENTYPE.SelectedIndex = 1;
                    lblSupType.Visible = false;
                    lblCustType.Visible = true;
                    cmbCUSTTYPE.Visible = true;
                    cmbSUPTYPE.Visible = false;
                    selectCustomerPeriod();
                }
            }
        }

        public void selectCustomerPeriod()
        {
            DataTable dt = new DataTable();
            trans.ACCID = ID;
           // cmd.CommandText = "SELECT CreditActive,CREDIT_AMOUNT,CperiodActive,CREDIT_PERIOD FROM REC_CUSTOMER WHERE LedgerId='" + ID + "'";
          
           // cmd.CommandText = "SELECT * FROM REC_CUSTOMER WHERE LedgerId='" + ID + "'";
           // SqlDataAdapter sda = new SqlDataAdapter(cmd);
          //  dt.Rows.Clear();
           // sda.Fill(dt);
            clscusObj.LedgerId = ID;
            dt = clscusObj.getDataById();

            if (dt.Rows.Count > 0)
            {
                Cust_code = dt.Rows[0]["CODE"].ToString();
                string cap = Convert.ToString(dt.Rows[0]["CreditActive"].ToString());
                string crm = dt.Rows[0]["CREDIT_AMOUNT"].ToString();
                string crp = Convert.ToString(dt.Rows[0]["CperiodActive"].ToString());
                string cp = dt.Rows[0]["CREDIT_PERIOD"].ToString();

                if (crp == "True")
                {
                   // cp = string.Concat(cp.Reverse().Skip(5).Reverse());
                    CREDITPERIOD.Text = cp;
                    CHECK_CREDIT_PERIOD.Checked = true;
                }
                else
                {
                    CREDITPERIOD.Text = "0";
                }

                if (cap == "True")
                {
                    CREDITLIMIT.Text = crm;
                    IN_ACTIVE.Checked = true;
                }
                else
                {
                    CREDITLIMIT.Text = "0.00";
                }

                cmbCUSTTYPE.SelectedValue= dt.Rows[0]["TYPE"].ToString();
                ADDRESS_A.Text = dt.Rows[0]["ADDRESS_A"].ToString();
                TELE1.Text = dt.Rows[0]["TELE1"].ToString();
                MOBILE.Text = dt.Rows[0]["MOBILE"].ToString();
                EMAIL.Text = dt.Rows[0]["EMAIL"].ToString();
                TINNO.Text = dt.Rows[0]["TIN_NO"].ToString();
                FAX.Text = dt.Rows[0]["FAX"].ToString();
                CITY_CODE.Text = dt.Rows[0]["CITY_CODE"].ToString();
                cmb_state.SelectedValue = dt.Rows[0]["STATE"].ToString();

            }
        }

        public void selectSupplierPeriod()
        {
            DataTable dt = new DataTable();
            trans.ACCID = ID;
           // cmd.CommandText = "SELECT DEBIT_LIMIT,DEBIT_AMOUNT,D_PERIODACTIVE,DEBIT_PERIOD_TYPE FROM PAY_SUPPLIER WHERE LedgerId='" + ID + "'";
         
         //   cmd.CommandText = "SELECT * FROM PAY_SUPPLIER WHERE LedgerId='" + ID + "'";
         //   SqlDataAdapter sda = new SqlDataAdapter(cmd);
         //   dt.Rows.Clear();
         //   sda.Fill(dt);
            paysupdbobj.LedgerId = ID;
            dt = paysupdbobj.getDataById();

            if (dt.Rows.Count > 0)
            {
                Cust_code = dt.Rows[0]["CODE"].ToString();
                string cap = Convert.ToString(dt.Rows[0]["DEBIT_LIMIT"].ToString());
                string crm = dt.Rows[0]["DEBIT_AMOUNT"].ToString();
                string crp = Convert.ToString(dt.Rows[0]["D_PERIODACTIVE"].ToString());
                string cp = dt.Rows[0]["DEBIT_PERIOD_TYPE"].ToString();
                if (crp == "True")
                {
                  //  cp = string.Concat(cp.Reverse().Skip(5).Reverse());
                    CREDITPERIOD.Text = cp;
                    CHECK_CREDIT_PERIOD.Checked = true;
                }
                else
                {
                    CREDITPERIOD.Text = "0";
                }

                if (cap == "True")
                {
                    IN_ACTIVE.Checked = true;
                    CREDITLIMIT.Text = crm;
                }
                else
                {
                    CREDITLIMIT.Text = "0.00";
                }
                cmbSUPTYPE.SelectedValue = dt.Rows[0]["TYPE"].ToString() ;
                ADDRESS_A.Text = dt.Rows[0]["ADDRESS_A"].ToString();
                TELE1.Text = dt.Rows[0]["TELE1"].ToString();
                MOBILE.Text = dt.Rows[0]["MOBILE"].ToString();
                EMAIL.Text = dt.Rows[0]["EMAIL"].ToString();
                TINNO.Text = dt.Rows[0]["TIN_NO"].ToString();
                FAX.Text = dt.Rows[0]["FAX"].ToString();
                CITY_CODE.Text = dt.Rows[0]["CITY_CODE"].ToString();
                cmb_state.SelectedValue = dt.Rows[0]["STATE"].ToString();
            }
        }

        public void selecttransaction()
        {
            DataTable dt = new DataTable();
            trans.ACCID = ID;
            trans.VOUCHERTYPE = "Opening Balance";
          //  cmd.Parameters.Clear();
            dt = trans.SelectTransaction();
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["ACCNAME"].ToString() == LEDGERNAME.Text)
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



        private void btnClear_Click(object sender, EventArgs e)
        {
            ID = "";
            LEDGERNAME.Text = "";
            OPENING_BAL.Text = "";

            UNDER.SelectedIndex = 0;
          
            CREDITPERIOD.Text = "";
            CREDITLIMIT.Text = "";
            DISPLAYNAME.Text = "";
            BANKBRANCH.Text = "";
            BANK.Text = "";
            IFCCODE.Text = "";
            ACCOUNTNO.Text = "";
            cmbSUPTYPE.SelectedIndex = 0;
            cmbCUSTTYPE.SelectedIndex = 0;
            ADDRESS_A.Text = "";
            TELE1.Text = "";
            MOBILE.Text = "";
            TINNO.Text = "";
            FAX.Text = "";
            EMAIL.Text = "";
            CITY_CODE.Text = "";
            cmb_state.SelectedIndex = 0;
            CHECK_CREDIT_PERIOD.Checked = false;
            IN_ACTIVE.Checked = false;

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

        private void LEDGERNAME_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                Common.preventDingSound(e);
                if (sender is KryptonTextBox)
                {
                    string name = (sender as KryptonTextBox).Name;
                    switch (name)
                    {
                        case "LEDGERNAME":
                            UNDER.Focus();
                            break;
                        case "CREDITPERIOD":
                            CREDITLIMIT.Focus();
                            break;
                        case "CREDITLIMIT":
                            DISPLAYNAME.Focus();
                            break;
                        case "DISPLAYNAME":
                            OPENING_BAL.Focus();
                            break;
                        case "OPENING_BAL":
                            OPENTYPE.Focus();
                            break;
                        case "ADDRESS_A":
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
                        case "FAX":
                            EMAIL.Focus();
                            break;
                        case "EMAIL":
                            CITY_CODE.Focus();
                            break;
                        case "CITY_CODE":
                         
                            cmb_state.Focus();
                            break;
                           
                            

                    }
                }
                else if (sender is KryptonComboBox)
                {
                    string name = (sender as KryptonComboBox).Name;
                    switch (name)
                    {
                        case "UNDER":
                            String underValue = Convert.ToString(UNDER.SelectedValue);
                            if (underValue.Equals("13") || underValue.Equals("14"))
                            {
                                //CREDITPERIOD.Focus();
                                if (underValue.Equals("13"))
                                    cmbSUPTYPE.Focus();
                                else
                                    cmbCUSTTYPE.Focus();

                            }
                            else
                            {
                                OPENING_BAL.Focus();
                            }
                            break;
                        case "OPENTYPE":
                            DATE_GRE.Focus();
                            break;
                        case "cmbSUPTYPE":
                            ADDRESS_A.Focus();
                            break;
                        case "cmbCUSTTYPE":
                            ADDRESS_A.Focus();
                            break;
                        case "cmb_state":
                           // CHECK_CREDIT_PERIOD.Focus();
                            DISPLAYNAME.Focus();
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
                        case "ISBUILDIN":
                            btnSave.Focus();
                            break;

                        default:
                            break;
                    }

                }
                
            }
        }

        private void DATE_GRE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                btnSave.Focus();
            }
        }

        private void LEDGERNAME_Leave(object sender, EventArgs e)
        {
            LEDGERNAME.Text = LEDGERNAME.Text.ToUpper();
        }

        private void DISPLAYNAME_Leave(object sender, EventArgs e)
        {
            DISPLAYNAME.Text = DISPLAYNAME.Text.ToUpper();
        }

        private void UNDER_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UNDER.SelectedValue.ToString() == "13" || UNDER.SelectedValue.ToString() == "14")
            {
                pn_supp.Visible = true;
                if (UNDER.SelectedValue.ToString().Equals("13"))
                {
                    OPENTYPE.SelectedIndex = 0;
                    lblSupType.Visible = true;
                    lblCustType.Visible = false;
                    cmbCUSTTYPE.Visible = false;
                    cmbSUPTYPE.Visible = true;
                   
                }
                else
                {
                    OPENTYPE.SelectedIndex = 1;
                    lblSupType.Visible = false;
                    lblCustType.Visible = true;
                    cmbCUSTTYPE.Visible = true;
                    cmbSUPTYPE.Visible = false ;
                    
                }
            }
            else
            {
                pn_supp.Visible = false;
            }
            //if (UNDER.SelectedValue.ToString() == "13")
            //{
            //    SupplierMaster sf = new SupplierMaster();
            //    sf.DESC_ENG.Text = LEDGERNAME.Text;
            //    sf.flag = 1;
              
            //    btnClear.PerformClick();
            //    sf.ShowDialog();
            //}
            //if (UNDER.SelectedValue.ToString() == "14")
            //{
            //    CustomerMaster cf = new CustomerMaster();
            //    cf.DESC_ENG.Text = LEDGERNAME.Text;
            //    cf.flag = 1;
            //    btnClear.PerformClick();
            //    cf.ShowDialog();
            //}
        }

        private void BT_DELETE_Click(object sender, EventArgs e)
        {
            if (ISBUILDIN.Checked)
            {
                MessageBox.Show("You cannot delete Built In Ledger, Sorry!!");
            }
            else
            {
                //cmd.CommandText = "SELECT ISNULL(COUNT(TRANSACTIONID), 0) FROM tb_Transactions WHERE ACCID = @id";
               // cmd.Parameters.Clear();
               // cmd.Parameters.AddWithValue("@id", ID);
               // conn.Open();
               // int count = Convert.ToInt32(cmd.ExecuteScalar());
               // conn.Close();
                transObj.AccId = ID;
                int count = Convert.ToInt32(transObj.getTransactionIdCount());
                if (count >= 1)
                {
                    if (count == 1)
                    {
                        //cmd.CommandText = "SELECT DEBIT, CREDIT FROM tb_Transactions WHERE ACCID = @id";
                        //cmd.Parameters.Clear();
                        //cmd.Parameters.AddWithValue("@id", ID);
                       // conn.Open();
                       // SqlDataReader r = cmd.ExecuteReader();
                        SqlDataReader r = transObj.getDebitCredit();
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
                            MessageBox.Show("Sorry!, you can't delete the ledger which has transactions.");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Sorry!, you can't delete the ledger which has transactions.");
                        return;
                    }
                    
                }

                if (MessageBox.Show("Are you sure?", "Ledger Deletion ", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string query="";
                    //cmd.CommandText = "DELETE FROM tb_Transactions WHERE ACCID = @id";
                    query = "DELETE FROM tb_Transactions WHERE ACCID = @id";
                    if (UNDER.SelectedValue.ToString() == "13")
                    {
                       // cmd.CommandText += ";DELETE FROM PAY_SUPPLIER WHERE LedgerId = @id";
                        query += ";DELETE FROM PAY_SUPPLIER WHERE LedgerId = @id";
                    }
                    if (UNDER.SelectedValue.ToString() == "14")
                       // cmd.CommandText += ";DELETE FROM REC_CUSTOMER WHERE LedgerId = @id";
                        query += ";DELETE FROM REC_CUSTOMER WHERE LedgerId = @id";
                   // cmd.Parameters.Clear();
                  // cmd.Parameters.AddWithValue("@id", ID);
                  //  conn.Open();
                    Dictionary<string, object> parameter = new Dictionary<string, object>();
                    parameter.Add("@id", ID);
                    DbFunctions.InsertUpdate(query,parameter);
                    //cmd.ExecuteNonQuery();
                    //conn.Close();
                    led.LEDGERNAME = LEDGERNAME.Text;
                    led.DeleteLedger(ID);
                    MessageBox.Show("Ledger Deleted!");
                    dgvLedger.Rows.Remove(dgvLedger.CurrentRow);
                    btnClear.PerformClick();
                }
            }
            
        }

        private void UNDER_Validated(object sender, EventArgs e)
        {
            if (UNDER.SelectedIndex < 0)
            {
                MessageBox.Show("Please select valid Under", "Warning");

                UNDER.Focus();
                UNDER.SelectedIndex = 0;
                return;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           
           // timer1.Stop();
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void cmbCUSTTYPE_SelectedIndexChanged(object sender, EventArgs e)
        {

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

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            getgrpname();
        }
    }
}
