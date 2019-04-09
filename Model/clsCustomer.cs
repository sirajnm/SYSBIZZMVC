using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
namespace Sys_Sols_Inventory.Model
{
    class clsCustomer
    {
        private string branch;
        private string code;
        private string type;
        private string descEng;
        private string descArb;


        private string addressA;
        private string addressB;
        private string pobox;
        private string tele;
        private string tele2;
        private string mobile;
        private string email;
        private string city;
        private string regCode;
        private string countryCode;
        private string web;
        private string fax;
        private decimal openBalance;
        private DateTime dateGre;
        private decimal debit;
        private decimal credit;
        private string initial;
        private string signature;
        private string defaultCurrecy;
        private string salesmanCode;
        private int multiCurrency;
        private int blackList;
        private string note;
        private string resrvd1;
        private string resrvd2;
        private string ledgerId;
        private string creditActve;
        private string tinNo;
        private string customerStatus;
        private string creditAmount;
        private string cPeriodActive;
        private string creditPeriod;
        private string state;
        private string cstNo;


        public string DescArb
        {
            get { return descArb; }
            set { descArb = value; }
        }
        public string DefaultCurrecy
        {
            get { return defaultCurrecy; }
            set { defaultCurrecy = value; }
        }
        public string CstNo
        {
            get { return cstNo; }
            set { cstNo = value; }
        }
        public string State
        {
            get { return state; }
            set { state = value; }
        }
        public string CreditPeriod
        {
            get { return creditPeriod; }
            set { creditPeriod = value; }
        }
        public string CPeriodActive
        {
            get { return cPeriodActive; }
            set { cPeriodActive = value; }
        }
        public string CreditAmount
        {
            get { return creditAmount; }
            set { creditAmount = value; }
        }


        public string CustomerStatus
        {
            get { return customerStatus; }
            set { customerStatus = value; }
        }
        public string TinNo
        {
            get { return tinNo; }
            set { tinNo = value; }
        }

        public string CreditActve
        {
            get { return creditActve; }
            set { creditActve = value; }
        }


        public string LedgerId
        {
            get { return ledgerId; }
            set { ledgerId = value; }
        }

        public string Resrvd2
        {
            get { return resrvd2; }
            set { resrvd2 = value; }
        }
        public string Resrvd1
        {
            get { return resrvd1; }
            set { resrvd1 = value; }
        }

        public string Note
        {
            get { return note; }
            set { note = value; }
        }
        public int BlackList
        {
            get { return blackList; }
            set { blackList = value; }
        }
        public int MultiCurrency
        {
            get { return multiCurrency; }
            set { multiCurrency = value; }
        }
        public string SalesmanCode
        {
            get { return salesmanCode; }
            set { salesmanCode = value; }
        }
        public string Signature
        {
            get { return signature; }
            set { signature = value; }
        }
        public string Initial
        {
            get { return initial; }
            set { initial = value; }
        }

        public decimal Credit
        {
            get { return credit; }
            set { credit = value; }
        }
        public decimal Debit
        {
            get { return debit; }
            set { debit = value; }
        }
        public DateTime DateGre
        {
            get { return dateGre; }
            set { dateGre = value; }
        }
        public decimal OpenBalance
        {
            get { return openBalance; }
            set { openBalance = value; }
        }
        public string Fax
        {
            get { return fax; }
            set { fax = value; }
        }
        public string Web
        {
            get { return web; }
            set { web = value; }
        }
        public string CountryCode
        {
            get { return countryCode; }
            set { countryCode = value; }
        }
        public string RegCode
        {
            get { return regCode; }
            set { regCode = value; }
        }
        public string City
        {
            get { return city; }
            set { city = value; }
        }
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        public string Mobile
        {
            get { return mobile; }
            set { mobile = value; }
        }
        public string Tele2
        {
            get { return tele2; }
            set { tele2 = value; }
        }

        public string Tele
        {
            get { return tele; }
            set { tele = value; }
        }
        public string Pobox
        {
            get { return pobox; }
            set { pobox = value; }
        }

        public string AddressB
        {
            get { return addressB; }
            set { addressB = value; }
        }
        public string AddressA
        {
            get { return addressA; }
            set { addressA = value; }
        }
        public string DescEng
        {
            get { return descEng; }
            set { descEng = value; }
        }
        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        public string Code
        {
            get { return code; }
            set { code = value; }
        }
        public string Branch
        {
            get { return branch; }
            set { branch = value; }
        }
        public DataTable getCustomer()
        {
            string Query = "SELECT CODE,TYPE,DESC_ENG,ADDRESS_A,ADDRESS_B,POBOX,TELE1,TELE2,MOBILE,EMAIL,CITY_CODE,COUNTRY_CODE,FAX,TIN_NO,OPENING_BAL,DATE_GRE,DEFAULT_CURRENCY,SALESMAN_CODE,LedgerId,CreditActive,NOTES,CUSTOMER_STATUS,CREDIT_AMOUNT,CperiodActive,CREDIT_PERIOD,[STATE] AS CUSTOMER_STATE FROM REC_CUSTOMER order by REC_CUSTOMER.code asc";
            return DbFunctions.GetDataTable(Query);
        }
        public DataTable getdata()
        {
            string Query = "SELECT CODE,TYPE,DESC_ENG,DESC_ARB,ADDRESS_A,ADDRESS_B,POBOX,TELE1,TELE2,MOBILE,EMAIL,CITY_CODE,COUNTRY_CODE,FAX,TIN_NO,OPENING_BAL,DATE_GRE,DEFAULT_CURRENCY,SALESMAN_CODE,LedgerId,CreditActive,NOTES,CUSTOMER_STATUS,CREDIT_AMOUNT,CperiodActive,CREDIT_PERIOD,[STATE] AS CUSTOMER_STATE FROM REC_CUSTOMER order by REC_CUSTOMER.code asc";
            return DbFunctions.GetDataTable(Query);
        }
        public DataTable getCustType()
        {
            string Query = "SELECT CODE,CODE+' - '+DESC_ENG AS DESC_ENG FROM REC_CUSTOMER_TYPE";
            return DbFunctions.GetDataTable(Query);
        }
        public DataTable getCurrency()
        {
            string Query = "SELECT CODE,CODE+' - '+DESC_ENG AS DESC_ENG FROM GEN_CURRENCY";
            return DbFunctions.GetDataTable(Query);
        }
        public DataTable getSalesMan()
        {
            string Query = "SELECT EMPID,CONCAT(EMP_FNAME,' ',EMP_MNAME,' ',EMP_LNAME)as name from EMP_EMPLOYEES WHERE EMP_DESIG=21";
            return DbFunctions.GetDataTable(Query);
        }
        public string getCustId()
        {
            string Query = "SELECT MAX(CODE) FROM REC_CUSTOMER";
            return DbFunctions.GetAValue(Query).ToString();
        }
        public DataTable getGenSetup()
        {
            string Query = "SELECT ACTIVE_PERIOD,CREDIT_PERIOD,ACTIVE_DEBIT_PERIOD,DEBIT_PERIOD  FROM   SYS_SETUP";
            return DbFunctions.GetDataTable(Query);
        }
        public void add()
        {
            string Query = "INSERT INTO REC_CUSTOMER(TYPE,CODE,DESC_ENG,DESC_ARB,ADDRESS_A,ADDRESS_B,POBOX," +
                            "TELE1,TELE2,MOBILE,FAX,EMAIL,CITY_CODE,COUNTRY_CODE,OPENING_BAL,DATE_GRE,SALESMAN_CODE," +
                            "DEFAULT_CURRENCY,LedgerId,CreditActive,NOTES,TIN_NO,CUSTOMER_STATUS,CREDIT_AMOUNT," +
                            "CperiodActive,CREDIT_PERIOD,STATE)" +
                            "VALUES(@type, @code, @desc_eng, @desc_arb, @address_a, @address_b, @pobox, " +
                            "@tele1, @tele2, @mobile, @fax, @email, @city_code, @country_code, @opening_bal, @date_gre, " +
                            "@salesman_code, @default_currency, @ledgerid, @creditactive, @notes, @tin_no, @customer_status, " +
                            "@credit_amount, @cperiodactive, @credit_period,@STATE)";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@type", type);
            parameters.Add("@code", code);
            parameters.Add("@desc_eng", descEng);
            parameters.Add("@desc_arb", descArb);
            parameters.Add("@address_a", addressA);
            parameters.Add("@address_b", addressB);
            parameters.Add("@pobox", pobox);
            parameters.Add("@tele1", tele);
            parameters.Add("@tele2", tele2);
            parameters.Add("@mobile", mobile);
            parameters.Add("@fax", fax);
            parameters.Add("@email", email);
            parameters.Add("@city_code", city);
            parameters.Add("@country_code", countryCode);
            parameters.Add("@opening_bal", openBalance);
            parameters.Add("@date_gre", dateGre);
            parameters.Add("@salesman_code", salesmanCode);
            parameters.Add("@default_currency", defaultCurrecy);
            parameters.Add("@ledgerid", ledgerId);
            parameters.Add("@creditactive", creditActve);
            parameters.Add("@notes", note);
            parameters.Add("@tin_no", tinNo);
            parameters.Add("@customer_status", customerStatus);
            parameters.Add("@credit_amount", creditAmount);
            parameters.Add("@cperiodactive", cPeriodActive);
            parameters.Add("@credit_period", creditPeriod);
            parameters.Add("@STATE", state);

            DbFunctions.InsertUpdate(Query, parameters);
        }
        public void Updste()
        {

            string Query = "UPDATE REC_CUSTOMER SET TYPE=@type,DESC_ENG=@desc_eng, DESC_ARB=@desc_arb, ADDRESS_A=@address_a, " +
                           "ADDRESS_B=@address_b, POBOX=@pobox,TELE1=@tele1,TELE2=@tele2,MOBILE=@mobile,FAX=@fax,EMAIL=@email,CITY_CODE=@city_code, " +
                           "COUNTRY_CODE=@country_code,OPENING_BAL=@opening_bal,DATE_GRE=@date_gre,SALESMAN_CODE=@salesman_code, " +
                           "DEFAULT_CURRENCY=@default_currency,LedgerId=@ledgerid,CreditActive=@creditactive,NOTES=@notes,TIN_NO=@tin_no," +
                           "CUSTOMER_STATUS=@customer_status,CREDIT_AMOUNT=@credit_amount,CperiodActive=@cperiodactive,CREDIT_PERIOD=@credit_period,STATE=@STATE" +
                           " WHERE CODE = @code";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@type", type);
            parameters.Add("@code", code);
            parameters.Add("@desc_eng", descEng);
            parameters.Add("@desc_arb", descArb);
            parameters.Add("@address_a", addressA);
            parameters.Add("@address_b", addressB);
            parameters.Add("@pobox", pobox);
            parameters.Add("@tele1", tele);
            parameters.Add("@tele2", tele2);
            parameters.Add("@mobile", mobile);
            parameters.Add("@fax", fax);
            parameters.Add("@email", email);
            parameters.Add("@city_code", city);
            parameters.Add("@country_code", countryCode);
            parameters.Add("@opening_bal", openBalance);
            parameters.Add("@date_gre", dateGre);
            parameters.Add("@salesman_code", salesmanCode);
            parameters.Add("@default_currency", defaultCurrecy);
            parameters.Add("@ledgerid", ledgerId);
            parameters.Add("@creditactive", creditActve);
            parameters.Add("@notes", note);
            parameters.Add("@tin_no", tinNo);
            parameters.Add("@customer_status", customerStatus);
            parameters.Add("@credit_amount", creditAmount);
            parameters.Add("@cperiodactive", cPeriodActive);
            parameters.Add("@credit_period", creditPeriod);
            parameters.Add("@STATE", state);

            DbFunctions.InsertUpdate(Query, parameters);
        }
        public void UpdsteSup()
        {

            string Query = "UPDATE PAY_SUPPLIER SET DESC_ENG=@desc_eng, DESC_ARB=@desc_arb, ADDRESS_A=@address_a, " +
                       "ADDRESS_B=@address_b, POBOX=@pobox,TELE1=@tele1,TELE2=@tele2,MOBILE=@mobile,FAX=@fax,EMAIL=@email,CITY_CODE=@city_code, " +
                       "COUNTRY_CODE=@country_code,OPENING_BAL=@opening_bal,DATE_GRE=@date_gre," +
                       "DEFAULT_CURRENCY=@default_currency,TIN_NO=@tin_no," +
                       "SUPPLIER_STATUS=@customer_status,STATE=@STATE" +
                       " WHERE LEDGERID = @LedgerId";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@type", type);
            parameters.Add("@code", code);
            parameters.Add("@desc_eng", descEng);
            parameters.Add("@desc_arb", descArb);
            parameters.Add("@address_a", addressA);
            parameters.Add("@address_b", addressB);
            parameters.Add("@pobox", pobox);
            parameters.Add("@tele1", tele);
            parameters.Add("@tele2", tele2);
            parameters.Add("@mobile", mobile);
            parameters.Add("@fax", fax);
            parameters.Add("@email", email);
            parameters.Add("@city_code", city);
            parameters.Add("@country_code", countryCode);
            parameters.Add("@opening_bal", openBalance);
            parameters.Add("@date_gre", dateGre);
            parameters.Add("@salesman_code", salesmanCode);
            parameters.Add("@default_currency", defaultCurrecy);
            parameters.Add("@ledgerid", ledgerId);
            parameters.Add("@creditactive", creditActve);
            parameters.Add("@notes", note);
            parameters.Add("@tin_no", tinNo);
            parameters.Add("@customer_status", customerStatus);
            parameters.Add("@credit_amount", creditAmount);
            parameters.Add("@cperiodactive", cPeriodActive);
            parameters.Add("@credit_period", creditPeriod);
            parameters.Add("@STATE", state);

            DbFunctions.InsertUpdate(Query, parameters);
        }
        public int SelectTransactionCount()
        {

            string Query = "SELECT ISNULL(COUNT(TRANSACTIONID), 0) FROM tb_Transactions WHERE ACCID = @id";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@id", ledgerId);
            return Convert.ToInt32(DbFunctions.GetAValue(Query, parameters));

        }
        public SqlDataReader SelectTransactionSum()
        {

            string Query = "SELECT DEBIT, CREDIT FROM tb_Transactions WHERE ACCID = @id";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@id", ledgerId);
            //    return Convert.ToInt32(DbFunctions.GetAValue(Query, parameters));
            return DbFunctions.GetDataReader(Query, parameters);
        }
        public void DeleteCust()
        {
            string Query = "DELETE FROM REC_CUSTOMER WHERE CODE = '" + code + "'";

            DbFunctions.InsertUpdate(Query);
        }
        public DataTable GetSup()
        {
            string Query = "SELECT DESC_ENG,LEDGERID FROM PAY_SUPPLIER";

            DbFunctions.InsertUpdate(Query);
            return DbFunctions.GetDataTable(Query);
        }
        public DataTable GetSupDetails()
        {
            string Query = "SELECT * FROM PAY_SUPPLIER where LEDGERID= '" + ledgerId + "'";

            DbFunctions.InsertUpdate(Query);
            return DbFunctions.GetDataTable(Query);
        }
        public int chkIsSupplier()
        {
            string Query = "SELECT UNDER FROM tb_Ledgers WHERE ledgerid='" + ledgerId + "'";

            DbFunctions.InsertUpdate(Query);
            return Convert.ToInt32(DbFunctions.GetAValue(Query));
        }

        public DataTable GetAllData(bool hasArabic)
        {
            string query = "";
            if (hasArabic)
                query = "SELECT CODE,TYPE,DESC_ENG,DESC_ARB,TYPE,ADDRESS_A,ADDRESS_B,POBOX,TELE1,TELE2,MOBILE,EMAIL,CITY_CODE,COUNTRY_CODE,FAX,OPENING_BAL,DATE_GRE,DEFAULT_CURRENCY,SALESMAN_CODE,LedgerId FROM REC_CUSTOMER";
            else
                query = "SELECT CODE,DESC_ENG,TYPE,ADDRESS_A,ADDRESS_B,POBOX,TELE1,TELE2,MOBILE,EMAIL,CITY_CODE,COUNTRY_CODE,FAX,OPENING_BAL,DATE_GRE,DEFAULT_CURRENCY,SALESMAN_CODE,LedgerId FROM REC_CUSTOMER";
            return DbFunctions.GetDataTable(query);
        }

        public SqlDataReader GetCustomerLevelDiscount()
        {
            string query = "SELECT GEN_DISCOUNT_TYPE.VALUE, GEN_DISCOUNT_TYPE.TYPE AS Type FROM            REC_CUSTOMER_TYPE INNER JOIN REC_CUSTOMER ON REC_CUSTOMER_TYPE.CODE = REC_CUSTOMER.TYPE INNER JOIN  GEN_DISCOUNT_TYPE ON REC_CUSTOMER_TYPE.DISCOUNT_TYPE = GEN_DISCOUNT_TYPE.CODE WHERE        (REC_CUSTOMER.CODE ='" + code + "')";
            return DbFunctions.GetDataReader(query);
        }

        //public double GetCustomerCreditLimit()
        //{
        //    string query = "SELECT REC_CUSTOMER_TYPE.CREDIT_LEVEL FROM REC_CUSTOMER INNER JOIN REC_CUSTOMER_TYPE ON REC_CUSTOMER.TYPE = REC_CUSTOMER_TYPE.CODE WHERE (REC_CUSTOMER.CODE = '" + code + "')";
        //    return Convert.ToDouble(DbFunctions.GetAValue(query));

        //}

        public double GetCustomerCreditLimit()
        {
            double data = 0;
            DataTable dt = new DataTable();
            string query = "select CreditActive,CREDIT_AMOUNT from REC_CUSTOMER where CODE='" + code + "'";
            //adapter.Fill(dt);
            dt = DbFunctions.GetDataTable(query);
            if (dt.Rows.Count > 0)
            {
                data = Convert.ToDouble(dt.Rows[0][1].ToString());
            }
            return data;
        }

        public DataTable getDataById()
        {
            string query = "SELECT * FROM REC_CUSTOMER WHERE LedgerId='" + ledgerId + "'";
            return DbFunctions.GetDataTable(query);
        }
        public DataTable getAllDataByLedgeId(string LedgerType)
        {
            string query = "";
             query = "SELECT        DATED, VOUCHERNO, VOUCHERTYPE, PARTICULARS, DEBIT, ACCID, NARRATION,CREDIT FROM  tb_Transactions WHERE        (ACCID = @ALEDGERID) AND (VOUCHERTYPE <> N'Cash Payment') ORDER BY VOUCHERTYPE";

            if (LedgerType == "Creditors")
            {
                query = "SELECT *  FROM    PAY_SUPPLIER WHERE        (LedgerId = @ALEDGERID)";
            }
            else if (LedgerType == "Debitors")
            {
                query = "SELECT *  FROM    REC_CUSTOMER WHERE        (LedgerId = @ALEDGERID)";
            }
            Dictionary<string, object> parameter = new Dictionary<string, object>();
            parameter.Add("@ALEDGERID", ledgerId);
            return DbFunctions.GetDataTable(query,parameter);
        }
        
        public DataTable Custdtls()
        {
            string query = "SELECT CODE,DESC_ENG,TIN_NO FROM REC_CUSTOMER";
            return DbFunctions.GetDataTable(query);
        }

        public DataTable getLedgerId()
        {
            string query = "SELECT LedgerId FROM REC_CUSTOMER";
            return DbFunctions.GetDataTable(query);
        }
        public string getIndividualLedgerId()
        {
            string query = "SELECT LedgerId FROM REC_CUSTOMER WHERE CODE='"+code+"'";
            return Convert.ToString( DbFunctions.GetAValue(query));
        }
        public DataTable GetAllDataFromPV(bool hasArabic)
        {
            string query = "";
            if (hasArabic)
                query = "SELECT CODE,TYPE,DESC_ENG,DESC_ARB,TYPE,ADDRESS_A,ADDRESS_B,POBOX,TELE1,TELE2,MOBILE,EMAIL,CITY_CODE,COUNTRY_CODE,FAX,OPENING_BAL,DATE_GRE,DEFAULT_CURRENCY,SALESMAN_CODE FROM REC_CUSTOMER";
            else
                query = "SELECT CODE,DESC_ENG,TYPE,ADDRESS_A,ADDRESS_B,POBOX,TELE1,TELE2,MOBILE,EMAIL,CITY_CODE,COUNTRY_CODE,FAX,OPENING_BAL,DATE_GRE,DEFAULT_CURRENCY,SALESMAN_CODE FROM REC_CUSTOMER";
            return DbFunctions.GetDataTable(query);
        }

        public string IsActive()
        {
           string query = "SELECT CreditActive FROM REC_CUSTOMER where CODE='" + code + "'";
           return Convert.ToString(DbFunctions.GetAValue(query));
        }

        public DataTable DatasByCode()
        {
            string query = "SELECT * FROM REC_CUSTOMER where CODE='" + code + "'";
            return DbFunctions.GetDataTable(query);
        }
    }
}
