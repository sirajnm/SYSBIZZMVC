using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
namespace Sys_Sols_Inventory.Model
{
    class InvPurchaseHdrDB
    {
        private string branch, store, docNo, docType, docDateHij, docReference, blNo, supplierCode, countryCode, currencyCode, baseCurrencyCode;

     
        private  string notes, draftLcNo, supInvNo, FobCostFr, ContainerType, cancelFlag, printFlag, editFlag, userCode, posted, payCode;
        private  string cardNo, stocked, debitOrAcc, creditOrAcc, salesman, purType;
        private  DateTime docDateGre, refDocDate;
        private  decimal texPer, texTotal, exchangeRate, totalFobAmount, totalFobInsuranceAmount, lcAmount, freightAmt;
        private  decimal freightInsurancePrc, freightInsuranceAmt, containerSize, gross, netVal, grossLc, netValLc, costingTotal;
        private  decimal supAccExpVal, supAccExpValLc, fobFreightAmount, fobInsureFreightAmount;
        private  int containerNos, DiscountPer, discountVal, docId;
        private  float cessAmount;
        private  bool flagDel;
    
        public string BaseCurrencyCode
        {
            get { return baseCurrencyCode; }
            set { baseCurrencyCode = value; }
        }
        public bool FlagDel
        {
            get { return flagDel; }
            set { flagDel = value; }
        }

        public float CessAmount
        {
            get { return cessAmount; }
            set { cessAmount = value; }
        }
        public int DocId
        {
            get { return docId; }
            set { docId = value; }
        }

        public int DiscountVal
        {
            get { return discountVal; }
            set { discountVal = value; }
        }

        public int DiscountPer1
        {
            get { return DiscountPer; }
            set { DiscountPer = value; }
        }

        public int ContainerNos
        {
            get { return containerNos; }
            set { containerNos = value; }
        }
        
        public decimal FobInsureFreightAmount
        {
            get { return fobInsureFreightAmount; }
            set { fobInsureFreightAmount = value; }
        }

        public decimal FobFreightAmount
        {
            get { return fobFreightAmount; }
            set { fobFreightAmount = value; }
        }

        public decimal SupAccExpValLc
        {
            get { return supAccExpValLc; }
            set { supAccExpValLc = value; }
        }

        public decimal SupAccExpVal
        {
            get { return supAccExpVal; }
            set { supAccExpVal = value;}
        }

        public decimal CostingTotal
        {
            get { return costingTotal; }
            set { costingTotal = value; }
        }

        public decimal NetValLc
        {
            get { return netValLc; }
            set { netValLc = value; }
        }

        public decimal GrossLc
        {
            get { return grossLc; }
            set { grossLc = value; }
        }

        public decimal NetVal
        {
            get { return netVal; }
            set { netVal = value; }
        }

        public decimal Gross
        {
            get { return gross; }
            set { gross = value; }
        }

        public decimal ContainerSize
        {
            get { return containerSize; }
            set { containerSize = value; }
        }

        public decimal FreightInsuranceAmt
        {
            get { return freightInsuranceAmt; }
            set { freightInsuranceAmt = value; }
        }

        public decimal FreightInsurancePrc
        {
            get { return freightInsurancePrc; }
            set { freightInsurancePrc = value; }
        }

        public decimal FreightAmt
        {
            get { return freightAmt; }
            set { freightAmt = value; }
        }

        public decimal LcAmount
        {
            get { return lcAmount; }
            set { lcAmount = value; }
        }

        public decimal TotalFobInsuranceAmount
        {
            get { return totalFobInsuranceAmount; }
            set { totalFobInsuranceAmount = value; }
        }

        public decimal TotalFobAmount
        {
            get { return totalFobAmount; }
            set { totalFobAmount = value; }
        }

        public decimal ExchangeRate
        {
            get { return exchangeRate; }
            set { exchangeRate = value; }
        }

        public decimal TexTotal
        {
            get { return texTotal; }
            set { texTotal = value; }
        }

        public decimal TexPer
        {
            get { return texPer; }
            set { texPer = value; }
        }

        public DateTime RefDocDate
        {
            get { return refDocDate; }
            set { refDocDate = value; }
        }

        public DateTime DocDateGre
        {
            get { return docDateGre; }
            set { docDateGre = value; }
        }

        public string PurType
        {
            get { return purType; }
            set { purType = value; }
        }

        public string Salesman
        {
            get { return salesman; }
            set { salesman = value; }
        }

        public string CreditOrAcc
        {
            get { return creditOrAcc; }
            set { creditOrAcc = value; }
        }

        public string DebitOrAcc
        {
            get { return debitOrAcc; }
            set { debitOrAcc = value; }
        }

        public string Stocked
        {
            get { return stocked; }
            set { stocked = value; }
        }

        public string CardNo
        {
            get { return cardNo; }
            set { cardNo = value; }
        }

        public string PayCode
        {
            get { return payCode; }
            set { payCode = value; }
        }

        public string Posted
        {
            get { return posted; }
            set { posted = value; }
        }

        public string UserCode
        {
            get { return userCode; }
            set { userCode = value; }
        }

        public string EditFlag
        {
            get { return editFlag; }
            set { editFlag = value; }
        }

        public string PrintFlag
        {
            get { return printFlag; }
            set { printFlag = value; }
        }

        public string CancelFlag
        {
            get { return cancelFlag; }
            set { cancelFlag = value; }
        }

        public string ContainerType1
        {
            get { return ContainerType; }
            set { ContainerType = value; }
        }

        public string FobCostFr1
        {
            get { return FobCostFr; }
            set { FobCostFr = value; }
        }

        public string SupInvNo
        {
            get { return supInvNo; }
            set { supInvNo = value; }
        }

        public string DraftLcNo
        {
            get { return draftLcNo; }
            set { draftLcNo = value; }
        }

        public string Notes
        {
            get { return notes; }
            set { notes = value; }
        }

        public string BlNo
        {
            get { return blNo; }
            set { blNo = value; }
        }

        public string CurrencyCode
        {
            get { return currencyCode; }
            set { currencyCode = value; }
        }

        public string CountryCode
        {
            get { return countryCode; }
            set { countryCode = value; }
        }

        public string SupplierCode
        {
            get { return supplierCode; }
            set { supplierCode = value; }
        }

        public string DocReference
        {
            get { return docReference; }
            set { docReference = value; }
        }

        public string DocDateHij
        {
            get { return docDateHij; }
            set { docDateHij = value; }
        }

        public string DocType
        {
            get { return docType; }
            set { docType = value; }
        }

        public string DocNo
        {
            get { return docNo; }
            set { docNo = value; }
        }

        public string Store
        {
            get { return store; }
            set { store = value; }
        }

        public string Branch
        {
            get { return branch; }
            set { branch = value; }
        }
        public string getMaxId()
        {
            string Query = "SELECT ISNULL(MAX(DOC_ID), 0) FROM INV_PURCHASE_HDR WHERE (PUR_TYPE IS NULL OR PUR_TYPE='') AND FLAGDEL='True'";
            return Convert.ToString( DbFunctions.GetAValue(Query));
        }
        public string getMaxIdWithPurType()
        {
            string Query = "SELECT ISNULL(MAX(DOC_ID), 0) FROM INV_PURCHASE_HDR WHERE PUR_TYPE = '" + purType+ "' AND FLAGDEL='True'";
            return Convert.ToString(DbFunctions.GetAValue(Query));
        }
        public int purVoucherStartFrom()
        {
            string Query = "SELECT ISNULL(VoucherStart,1) FROM GEN_PUR_STARTFROM WHERE VoucherTypeCode='" + purType + "'";
            return Convert.ToInt32(DbFunctions.GetAValue(Query));
        }
        public DataTable selecrGenPriceType()
        {
            string query = "SELECT CODE,DESC_ENG FROM GEN_PRICE_TYPE";

            return DbFunctions.GetDataTable(query);
        }
        public DataTable selecrGenPurType()
        {
            string query = "SELECT CODE,DESC_ENG FROM GEN_PUR_TYPE";

            return DbFunctions.GetDataTable(query);
        }
        public string getDefaultPurchaseType()
        {
            string query = "SELECT TOP 1 PUR_TYPE FROM SYS_SETUP";

            return DbFunctions.GetAValue(query).ToString();
        }
        public string getSalesMan()
        {
            string query = "GetSalesMan";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@Empid", salesman);
            return Convert.ToString( DbFunctions.GetAValueProcedure(query,parameters));
        }
         public DataTable bindSubPrice()
        {
            string query = " SELECT CODE as [PRICE TYPES] FROM GEN_PRICE_TYPE";

            return DbFunctions.GetDataTable(query);
        }
         public DataTable itemsForSuggestion()
         {
             string query = "item_suggestion_without_stock";

             return DbFunctions.GetDataTableProcedure(query);
         }
         public DataTable getHdr()
         {
             string query = "SELECT DOC_NO as 'DOC NO',DOC_DATE_GRE Date,DOC_DATE_HIJ 'Hij Date',DOC_REFERENCE,TAX_TOTAL as 'Tax Amount',SUPPLIER_CODE as 'Supplier Code',COUNTRY_CODE as 'Country Code',CURRENCY_CODE,NOTES,GROSS,NET_VAL as 'Net Value',PAY_CODE,CARD_NO,POSTED,STOCKED FROM INV_PURCHASE_HDR WHERE DOC_NO='" + docNo + "'";

             return DbFunctions.GetDataTable(query);
         }
         public string getCurrencyRate()
         {
             string query = "SELECT RATE FROM GEN_CURRENCY WHERE CODE='" + currencyCode+ "'";

             return DbFunctions.GetAValue(query).ToString();
         }
         //public string getBaseCurrencyRate()
         //{
         //    string query = "SELECT RATE FROM GEN_CURRENCY WHERE CODE='" + baseCurrencyCode + "'";

         //    return DbFunctions.GetAValue(query).ToString();
         //}
         public void insertCurrencyDetails()
         {
             string query = "INSERT INTO tbl_curRepor (Invoice_no,Currency_code,Currency_amt) VALUES('" + docNo+ "','" + currencyCode + "','" + exchangeRate + "')";
              DbFunctions.InsertUpdate(query);
         }
         public void deletePurchase()
         {
             string query = "UPDATE INV_PURCHASE_HDR SET FLAGDEL='FALSE'  WHERE DOC_NO = '" + docNo + "' ;UPDATE  INV_PURCHASE_DTL SET FLAGDEL='FALSE' WHERE DOC_NO = '" + docNo + "';DELETE FROM INV_STK_TRX_HDR WHERE DOC_REFERENCE = '" + docNo + "'AND DOC_TYPE='" + docType + "';DELETE FROM INV_STK_TRX_DTL WHERE DOC_REFERENCE = '" + docNo + "' AND DOC_TYPE='" + docType + "'";
             DbFunctions.InsertUpdate(query);
         }
         public void insertDeletedTransactions()
         {
             string query = "insert into     tbl_deletedTransaction(VOUCHERNO, VOUCHERTYPE, ACCNAME, PARTICULARS, DEBIT, CREDIT, DATED, NARRATION, USERID, SYSTEMTIEM, ACCID) select    VOUCHERNO, VOUCHERTYPE, ACCNAME, PARTICULARS, DEBIT, CREDIT, DATED, NARRATION, USERID, SYSTEMTIEM, ACCID from tb_Transactions where VOUCHERNO='" + docNo + "' and VOUCHERTYPE='" + docType + "'";

             DbFunctions.InsertUpdate(query);
         }
         public string getState()
         {
             string query = "SELECT STATE FROM PAY_SUPPLIER WHERE CODE=" + supplierCode;

             return DbFunctions.GetAValue(query).ToString();
         }
         public void postingProduct()
         {
             string query = "UPDATE INV_PURCHASE_HDR SET POSTED = 'Y' WHERE DOC_NO IN (" + docNo + ")";

             DbFunctions.InsertUpdate(query);
         }
         public string getSupplier()
         {
             string query = "SELECT DESC_ENG FROM PAY_SUPPLIER WHERE CODE='" + supplierCode + "'";

             return Convert.ToString( DbFunctions.GetAValue(query));
         }
         public SqlDataReader getLedgerIdFromSupplier()
         {
             string query = "select DESC_ENG,LedgerId from PAY_SUPPLIER WHERE CODE='" + supplierCode + "'";

             return DbFunctions.GetDataReader(query);
         }
         public DataTable getVoucherCurrency()
         {
             string query = "SELECT tbl_curRepor.Currency_amt as ExcangeRate ,tbl_curRepor.Currency_code as Currency,  tbl_curRepor.Invoice_no,INV_PURCHASE_HDR.DOC_NO, INV_PURCHASE_HDR.DOC_DATE_GRE as Date,  PAY_SUPPLIER.DESC_ENG as Supplier, (INV_PURCHASE_HDR.TAX_TOTAL)*tbl_curRepor.Currency_amt as 'Tax Total', INV_PURCHASE_HDR.FREIGHT_AMT as Freight, INV_PURCHASE_HDR.GROSS*tbl_curRepor.Currency_amt as 'Gross Amount', INV_PURCHASE_HDR.DISCOUNT_VAL*tbl_curRepor.Currency_amt as Discount,INV_PURCHASE_HDR.NET_VAL*tbl_curRepor.Currency_amt as 'Net Value' FROM            INV_PURCHASE_HDR INNER JOIN tbl_CurRepor ON  INV_PURCHASE_HDR.DOC_ID=tbl_CurRepor.Invoice_no LEFT OUTER JOIN   PAY_SUPPLIER  ON INV_PURCHASE_HDR.SUPPLIER_CODE = PAY_SUPPLIER.CODE WHERE  (INV_PURCHASE_HDR.DOC_ID='" + docNo+ "')";

             return DbFunctions.GetDataTable(query);
         }
         public DataTable getPurchaseHdrWthDelFlag()
         {
             string query = "SELECT * FROM INV_PURCHASE_HDR WHERE DOC_NO = '" + docNo + "'AND FLAGDEL='TRUE' AND PUR_TYPE='" + purType+"'";
            
             return DbFunctions.GetDataTable(query);
         }
         
         public string chckVoucher()
         {
             string query = "SELECT ISNULL(MAX(DOC_ID),0) FROM INV_PURCHASE_HDR WHERE PUR_TYPE = '" + purType + "' AND FLAGDEL='True'";

             return Convert.ToString(DbFunctions.GetAValue(query));

         }
         public DataTable getBankAccounts()
         {
             string query = "SELECT DISTINCT LEDGERNAME FROM tb_Ledgers WHERE UNDER IN (10,21,22)";

             return DbFunctions.GetDataTable(query);
         }
         public object getMaxDocId(string type)
         {
             string query = "SELECT ISNULL(MAX(DOC_ID), 0) FROM INV_PURCHASE_HDR WHERE DOC_TYPE in('" + type + "')AND FLAGDEL='True'";
             return DbFunctions.GetAValue(query);
         }
         public DataTable getDetailsFromReturnBillId()
         {
             string query = "select * from INV_PURCHASE_HDR WHERE DOC_ID=" + docId;
             return DbFunctions.GetDataTable(query);
         }
         public object deleteDetails()
         {
             string query = "DELETE  FROM INV_PURCHASE_HDR   WHERE DOC_NO = '" + docNo + "' ;DELETE FROM INV_PURCHASE_DTL WHERE DOC_NO = '" + docNo + "';DELETE FROM INV_STK_TRX_HDR WHERE DOC_REFERENCE = '" + docReference + "' AND DOC_TYPE='" + docType + "' ;DELETE FROM INV_STK_TRX_DTL WHERE DOC_REFERENCE = '" + docReference + "'AND DOC_TYPE='" + docType + "'";
             return DbFunctions.InsertUpdate(query);        
         }

         public SqlDataReader getSupLedId()
         {

             string query = "select DESC_ENG,LedgerId from PAY_SUPPLIER WHERE CODE='" + supplierCode + "'";
             return DbFunctions.GetDataReader(query);
         }
         public string getDocType()
         {
             string query = "select DOC_TYPE from INV_PURCHASE_HDR WHERE DOC_NO='" + docNo + "'";
             return (string)DbFunctions.GetAValue(query);
         }
    }
}
