using System;
using System.Collections.Generic;
using System.Data
;
using System.Linq;
using System.Text;

namespace Sys_Sols_Inventory.Model
{
    class PaySupplierDB
    {
        private string branch, code, type, descEng, descArb, addressA, addressB, poBox, tele1, tele2, mobile, email;
        private string cityCode, regCode, countryCode, web, fax, initial, signature, defaultCurrency, contactPerson;
        private string multyCurrency, notes, resrvd1, resrvd2, ledgerId, debitAmount, debitPeriodType, state, tinNo;
        private decimal openingBal, debit, credit;
        private DateTime dateGre;
        private int blackList;
        private bool supplierStatus, debitLimit, dPeriodActive;

        public bool DPeriodActive
        {
            get { return dPeriodActive; }
            set { dPeriodActive = value; }
        }

        public bool DebitLimit
        {
            get { return debitLimit; }
            set { debitLimit = value; }
        }

        public bool SupplierStatus
        {
            get { return supplierStatus; }
            set { supplierStatus = value; }
        }

        public int BlackList
        {
            get { return blackList; }
            set { blackList = value; }
        }
        public DateTime DateGre
        {
            get { return dateGre; }
            set { dateGre = value; }
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

        public decimal OpeningBal
        {
            get { return openingBal; }
            set { openingBal = value; }
        }

        public string TinNo
        {
            get { return tinNo; }
            set { tinNo = value; }
        }

        public string State
        {
            get { return state; }
            set { state = value; }
        }

        public string DebitPeriodType
        {
            get { return debitPeriodType; }
            set { debitPeriodType = value; }
        }

        public string DebitAmount
        {
            get { return debitAmount; }
            set { debitAmount = value; }
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

        public string Notes
        {
            get { return notes; }
            set { notes = value; }
        }

        public string MultyCurrency
        {
            get { return multyCurrency; }
            set { multyCurrency = value; }
        }

        public string ContactPerson
        {
            get { return contactPerson; }
            set { contactPerson = value; }
        }

        public string DefaultCurrency
        {
            get { return defaultCurrency; }
            set { defaultCurrency = value; }
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

        public string CityCode
        {
            get { return cityCode; }
            set { cityCode = value; }
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

        public string Tele1
        {
            get { return tele1; }
            set { tele1 = value; }
        }

        public string PoBox
        {
            get { return poBox; }
            set { poBox = value; }
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

        public string DescArb
        {
            get { return descArb; }
            set { descArb = value; }
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

        public DataTable SelectAllCust()
        {
            string query = "SELECT CODE,TYPE,DESC_ENG,DESC_ARB,ADDRESS_A,ADDRESS_B,POBOX,TELE1,TELE2,MOBILE,EMAIL,CITY_CODE,COUNTRY_CODE,OPENING_BAL,FAX,CONVERT(NVARCHAR,DATE_GRE,103) AS DATE_GRE,INITIAL,DEFAULT_CURRENCY,CONTACT_PERSON,LedgerId,NOTES,Supplier_Status,DEBIT_LIMIT,DEBIT_AMOUNT,D_PERIODACTIVE,DEBIT_PERIOD_TYPE,[STATE] AS SUPPLIER_STATE, TIN_NO AS GSTN FROM PAY_SUPPLIER order by PAY_SUPPLIER.code asc";
            return DbFunctions.GetDataTable(query);
        }
        public DataTable getSupplierData()
        {
            string query = "SELECT CODE,CODE+' - '+DESC_ENG AS DESC_ENG FROM PAY_SUPPLIER_TYPE";
            return DbFunctions.GetDataTable(query);
        }
        public DataTable getGenCurrency()
        {
            string query = "SELECT CODE,CODE+' - '+DESC_ENG AS DESC_ENG FROM GEN_CURRENCY";
            return DbFunctions.GetDataTable(query);
        }

        public object getCusSup()
        {
            string query = "SELECT  IsCusSup  FROM   SYS_SETUP";
            return DbFunctions.GetAValue(query);
        }
        public DataTable getActiveDebitPeriod()
        {
            string query = "SELECT ACTIVE_DEBIT_PERIOD,DEBIT_PERIOD  FROM   SYS_SETUP";
            return DbFunctions.GetDataTable(query);
        }
        public object getMaxCode()
        {
            string query = "SELECT MAX(CODE) FROM PAY_SUPPLIER";
            return DbFunctions.GetAValue(query);
        }
        public int insertPaySupplier()
        {
            string query = "INSERT INTO PAY_SUPPLIER(CODE,TYPE,DESC_ENG,DESC_ARB,ADDRESS_A,ADDRESS_B,POBOX,TELE1,TELE2,MOBILE,EMAIL,CITY_CODE,COUNTRY_CODE,OPENING_BAL,FAX,DATE_GRE,INITIAL,DEFAULT_CURRENCY,CONTACT_PERSON,LedgerId,NOTES,Supplier_Status,DEBIT_LIMIT,DEBIT_AMOUNT,D_PERIODACTIVE,DEBIT_PERIOD_TYPE,STATE, TIN_NO) VALUES('" + code + "','" + type + "','" + descEng + "','" + descArb + "','" + addressA + "','" + addressB + "','" + poBox + "','" + tele1 + "','" + tele2 + "','" + mobile + "','" + email + "','" + cityCode + "','" + countryCode + "','" + openingBal + "','" + fax + "','" + dateGre.ToString("MM/dd/yyyy") + "','" + initial + "','" + defaultCurrency + "','" + contactPerson + "','" + LedgerId + "','" + notes + "','" + supplierStatus + "', '" + debitLimit + "','" + debitAmount + "','" + dPeriodActive + "','" + DebitPeriodType + "','" + state + "', '" + tinNo + "')";
            return DbFunctions.InsertUpdate(query);
        }
        public int updatePaySupplier()
        {
            string query = "UPDATE PAY_SUPPLIER SET CODE = '" + code + "',TYPE = '" + type + "',DESC_ENG = '" + descEng + "',DESC_ARB = '" + descArb + "',ADDRESS_A = '" + addressA + "',ADDRESS_B = '" + addressB + "',POBOX = '" + poBox + "',TELE1 = '" + tele1 + "',TELE2 = '" + tele2 + "',MOBILE = '" + mobile + "',EMAIL = '" + email + "',CITY_CODE = '" + cityCode + "',COUNTRY_CODE = '" + countryCode + "',OPENING_BAL = '" + openingBal + "',FAX = '" + fax + "',DATE_GRE = '" + dateGre + "',INITIAL = '" + initial + "',DEFAULT_CURRENCY = '" + defaultCurrency + "',CONTACT_PERSON = '" + contactPerson + "',NOTES='" + notes + "',Supplier_Status='" + supplierStatus + "',DEBIT_AMOUNT='" + debitAmount + "',D_PERIODACTIVE='" + dPeriodActive + "',DEBIT_PERIOD_TYPE='" + debitPeriodType + "',STATE='" + state + "', TIN_NO = '" + tinNo + "' WHERE CODE = '" + code + "'"; ;
            return DbFunctions.InsertUpdate(query);
        }
        public int updateCustomer()
        {
            string query = "UPDATE REC_CUSTOMER SET DESC_ENG = '" + descEng + "',DESC_ARB = '" + descArb + "',ADDRESS_A = '" + addressA + "',ADDRESS_B = '" + addressB + "',POBOX = '" + poBox + "',TELE1 = '" + tele1 + "',TELE2 = '" + tele2 + "',MOBILE = '" + mobile + "',EMAIL = '" + email + "',CITY_CODE = '" + cityCode + "',COUNTRY_CODE = '" + countryCode + "',OPENING_BAL = '" + openingBal + "',FAX = '" + fax + "',DATE_GRE = '" + dateGre + "',INITIAL = '" + initial + "',NOTES='" + notes + "',STATE='" + state + "', TIN_NO = '" + tinNo + "' WHERE LedgerId = '" + ledgerId + "'";
            return DbFunctions.InsertUpdate(query);

        }
        public object geTtransactionId()
        {
            string query = "SELECT ISNULL(COUNT(TRANSACTIONID), 0) FROM tb_Transactions WHERE ACCID = @id";
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@id", ledgerId);
            return DbFunctions.GetAValue(query, Parameters);
        }
        public System.Data.SqlClient.SqlDataReader getDebitCredit()
        {
            string query = "SELECT DEBIT, CREDIT FROM tb_Transactions WHERE ACCID = @id";
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@id", ledgerId);
            return DbFunctions.GetDataReader(query, Parameters);
        }
        public int deletePaySupplier()
        {
            string query = "DELETE FROM PAY_SUPPLIER WHERE CODE = '" + code + "'";
            return DbFunctions.InsertUpdate(query);
        }

        public DataTable selectDescEngLedgerIdBind()
        {
            string query = "SELECT DESC_ENG,LEDGERID FROM REC_CUSTOMER";
            return DbFunctions.GetDataTable(query);
        }
        public DataTable getRecCustomer()
        {
            string query = "SELECT * FROM REC_CUSTOMER where LEDGERID='" + ledgerId + "';";
            return DbFunctions.GetDataTable(query);
        }
        public DataTable getDetailsByCode()
        {
            string query = "SELECT CODE,TYPE,DESC_ENG,ADDRESS_A,ADDRESS_B,POBOX,TELE1,TELE2,MOBILE,EMAIL,CITY_CODE,COUNTRY_CODE,OPENING_BAL,FAX,CONVERT(NVARCHAR,DATE_GRE,103) AS DATE_GRE,INITIAL,DEFAULT_CURRENCY,CONTACT_PERSON,LedgerId,NOTES,Supplier_Status,DEBIT_LIMIT,DEBIT_AMOUNT,D_PERIODACTIVE,DEBIT_PERIOD_TYPE,[STATE] AS SUPPLIER_STATE,TIN_NO AS GSTN FROM PAY_SUPPLIER order by PAY_SUPPLIER.code asc";
            return DbFunctions.GetDataTable(query);
        }

        public DataTable GetAllData(bool hasArabic)
        {
            string query = "";
            if (hasArabic)
                query = "SELECT CODE,DESC_ENG,DESC_ARB,TYPE,ADDRESS_A,ADDRESS_B,POBOX,TELE1,TELE2,MOBILE,EMAIL,CITY_CODE,COUNTRY_CODE,OPENING_BAL,FAX,CONVERT(NVARCHAR,DATE_GRE,103) AS DATE_GRE,INITIAL,DEFAULT_CURRENCY,CONTACT_PERSON,LedgerId FROM PAY_SUPPLIER";
            else
                query = "SELECT CODE,DESC_ENG,ADDRESS_A,TYPE,ADDRESS_B,POBOX,TELE1,TELE2,MOBILE,EMAIL,CITY_CODE,COUNTRY_CODE,OPENING_BAL,FAX,CONVERT(NVARCHAR,DATE_GRE,103) AS DATE_GRE,INITIAL,DEFAULT_CURRENCY,CONTACT_PERSON,LedgerId FROM PAY_SUPPLIER";
            return DbFunctions.GetDataTable(query);
        }
        public DataTable getDataPartyAcc()
        {
            string query = "SELECT PAY_SUPPLIER.CODE,PAY_SUPPLIER.DESC_ENG,PAY_SUPPLIER.MOBILE,ISNULL(CREDIT.total_CREDIT,0) AS total_CREDIT,ISNULL(DEBIT.total_DEBIT,0) AS total_DEBIT,(ISNULL(CREDIT.total_CREDIT,0)-ISNULL(DEBIT.total_DEBIT,0)) AS BALANCE  FROM PAY_SUPPLIER LEFT OUTER JOIN (SELECT ACCID,ISNULL(SUM(CREDIT),0) as total_CREDIT FROM tb_Transactions GROUP BY ACCID) AS CREDIT ON PAY_SUPPLIER.LedgerId=CREDIT.ACCID  LEFT OUTER JOIN (SELECT ACCID,ISNULL(SUM(DEBIT),0) as total_DEBIT FROM tb_Transactions GROUP BY ACCID) AS DEBIT ON PAY_SUPPLIER.LedgerId=DEBIT.ACCID WHERE PAY_SUPPLIER.LedgerId=" + Convert.ToInt32(ledgerId) + " ORDER BY PAY_SUPPLIER.CODE";
            return DbFunctions.GetDataTable(query);
        }
        public DataTable getDataById()
        {
            string query = "SELECT * FROM PAY_SUPPLIER WHERE LedgerId='" + ledgerId + "'";
            return DbFunctions.GetDataTable(query);
        }
        public DataTable getDataByDateAndId()
        {
            string query = "SELECT PAY_SUPPLIER.CODE,PAY_SUPPLIER.DESC_ENG,PAY_SUPPLIER.MOBILE,CREDIT.total_CREDIT,DEBIT.total_DEBIT,(CREDIT.total_CREDIT-DEBIT.total_DEBIT) AS BALANCE  FROM PAY_SUPPLIER LEFT OUTER JOIN (SELECT ACCID,ISNULL(SUM(CREDIT),0) as total_CREDIT FROM tb_Transactions WHERE DATED<@date GROUP BY ACCID) AS CREDIT ON PAY_SUPPLIER.LedgerId=CREDIT.ACCID  LEFT OUTER JOIN (SELECT ACCID,ISNULL(SUM(DEBIT),0) as total_DEBIT FROM tb_Transactions GROUP BY ACCID) AS DEBIT ON PAY_SUPPLIER.LedgerId=DEBIT.ACCID WHERE PAY_SUPPLIER.LedgerId=@id ORDER BY PAY_SUPPLIER.CODE";
            return DbFunctions.GetDataTable(query);
        }
        public DataTable getLedgerId()
        {
            string query = "SELECT LedgerId FROM PAY_SUPPLIER";
            return DbFunctions.GetDataTable(query);

        }
        public DataTable getSupplierStatement()
        {
            string query = "select suppliers.desc_eng  NAME,last_pay.Date [LAST PAY DATE],last_pay.DEBIT [LAST PAY AMOUNT] ,suppliers.Dedit DEBIT,suppliers.Credit [CREDIT(With Opening Balance)],suppliers.Balance BALANCE from (SELECT  s.LedgerId ,s.DESC_ENG,sum( t.CREDIT) Credit,sum(t.DEBIT) Dedit,sum(t.CREDIT)-SUM(t.DEBIT) Balance from pay_supplier s join tb_Transactions t on t.ACCID=s.LedgerId group by s.LedgerId, s.DESC_ENG  ) Suppliers left join (select tr.accid,tr.TRANSACTIONID  id,tr.DATED [date],tr.debit,ROW_NUMBER() OVER(Partition by tr.accid ORDER BY tr.accid,tr.DATED desc,tr.TRANSACTIONID desc)  [Row_Number] from tb_Transactions tr join pay_supplier sp on tr.ACCID=sp.LedgerId and tr.VOUCHERTYPE not in('Purchase','Opening Balance') ) last_pay on Suppliers.LedgerId=last_pay.accid where last_pay.Row_Number=1";
            return DbFunctions.GetDataTable(query);

        }
        public DataTable GetAllDataFromPV(bool hasArabic)
        {
            string query = "";
            if (hasArabic)
                query = "SELECT CODE,DESC_ENG,DESC_ARB,TYPE,ADDRESS_A,ADDRESS_B,POBOX,TELE1,TELE2,MOBILE,EMAIL,CITY_CODE,COUNTRY_CODE,OPENING_BAL,FAX,CONVERT(NVARCHAR,DATE_GRE,103) AS DATE_GRE,INITIAL,DEFAULT_CURRENCY,CONTACT_PERSON FROM PAY_SUPPLIER";
            else
                query = "SELECT CODE,DESC_ENG,ADDRESS_A,TYPE,ADDRESS_B,POBOX,TELE1,TELE2,MOBILE,EMAIL,CITY_CODE,COUNTRY_CODE,OPENING_BAL,FAX,CONVERT(NVARCHAR,DATE_GRE,103) AS DATE_GRE,INITIAL,DEFAULT_CURRENCY,CONTACT_PERSON FROM PAY_SUPPLIER";
            return DbFunctions.GetDataTable(query);
        }
        public DataTable getDetailsByUnder()
        {
            string query = "SELECT PAY_SUPPLIER.LedgerId,ISNULL(DEBIT_PERIOD_TYPE,0) AS DEBIT_PERIOD FROM PAY_SUPPLIER LEFT OUTER JOIN TB_LEDGERS ON TB_LEDGERS.LEDGERID=PAY_SUPPLIER.LEDGERID WHERE TB_LEDGERS.UNDER=13";
            return DbFunctions.GetDataTable(query);
        }
        public string getIndividualLedgerId()
        {
            string query = "SELECT LedgerId FROM PAY_SUPPLIER WHERE CODE='" + code + "'";
            return Convert.ToString(DbFunctions.GetAValue(query));
        }
    }
}
