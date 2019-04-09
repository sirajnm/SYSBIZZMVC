using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Sys_Sols_Inventory.Model
{
    class InvSalesHdrDB
    {
        private string branch, store, docNo, docType, docDateHij, currencyCode, docReference, branchOther, saleType, priceType;
        private string customerCode, customerNameEng, customerNameArb, salesmanCode, notes, cancelFlag, printFlag, editFlag;
        private string payCode, cardNo, pono, plantCode, posted, debitOrAcc, creditOrAcc, shipName, shipAddress, shipPhone;
        private string shipGst, shipState, shipTrMode, shipVehicleNo, shipPliceOfSupply, custAddress;
        private decimal exchangeRate, taxPer, taxTotal, vat, discount, totalAmount, netAmount, totalCost, docId, freight;
        private decimal roundOff, cess;
        private bool cusLevelDiscount, flagDel, isService;

       
        private DateTime docDateGre, currentDate, shipSupplyDate;
        private string query;
        public bool IsService
        {
            get { return isService; }
            set { isService = value; }
        }
        public string PriceType
        {
            get { return priceType; }
            set { priceType = value; }
        }

        public string SaleType
        {
            get { return saleType; }
            set { saleType = value; }
        }

        public string BranchOther
        {
            get { return branchOther; }
            set { branchOther = value; }
        }

        public string DocReference
        {
            get { return docReference; }
            set { docReference = value; }
        }

        public string CurrencyCode
        {
            get { return currencyCode; }
            set { currencyCode = value; }
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

        public string Notes
        {
            get { return notes; }
            set { notes = value; }
        }

        public string SalesmanCode
        {
            get { return salesmanCode; }
            set { salesmanCode = value; }
        }

        public string CustomerNameArb
        {
            get { return customerNameArb; }
            set { customerNameArb = value; }
        }

        public string CustomerNameEng
        {
            get { return customerNameEng; }
            set { customerNameEng = value; }
        }

        public string CustomerCode
        {
            get { return customerCode; }
            set { customerCode = value; }
        }

        public string ShipPhone
        {
            get { return shipPhone; }
            set { shipPhone = value; }
        }

        public string ShipAddress
        {
            get { return shipAddress; }
            set { shipAddress = value; }
        }

        public string ShipName
        {
            get { return shipName; }
            set { shipName = value; }
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

        public string Posted
        {
            get { return posted; }
            set { posted = value; }
        }

        public string PlantCode
        {
            get { return plantCode; }
            set { plantCode = value; }
        }

        public string Pono
        {
            get { return pono; }
            set { pono = value; }
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

        public string CustAddress
        {
            get { return custAddress; }
            set { custAddress = value; }
        }

        public string ShipPliceOfSupply
        {
            get { return shipPliceOfSupply; }
            set { shipPliceOfSupply = value; }
        }

        public string ShipVehicleNo
        {
            get { return shipVehicleNo; }
            set { shipVehicleNo = value; }
        }

        public string ShipTrMode
        {
            get { return shipTrMode; }
            set { shipTrMode = value; }
        }

        public string ShipState
        {
            get { return shipState; }
            set { shipState = value; }
        }

        public string ShipGst
        {
            get { return shipGst; }
            set { shipGst = value; }
        }

        public DateTime ShipSupplyDate
        {
            get { return shipSupplyDate; }
            set { shipSupplyDate = value; }
        }

        public DateTime CurrentDate
        {
            get { return currentDate; }
            set { currentDate = value; }
        }

        public DateTime DocDateGre
        {
            get { return docDateGre; }
            set { docDateGre = value; }
        }

        public decimal Freight
        {
            get { return freight; }
            set { freight = value; }
        }

        public decimal DocId
        {
            get { return docId; }
            set { docId = value; }
        }

        public decimal TotalCost
        {
            get { return totalCost; }
            set { totalCost = value; }
        }

        public decimal NetAmount
        {
            get { return netAmount; }
            set { netAmount = value; }
        }

        public decimal TotalAmount
        {
            get { return totalAmount; }
            set { totalAmount = value; }
        }

        public decimal Discount
        {
            get { return discount; }
            set { discount = value; }
        }

        public decimal Vat
        {
            get { return vat; }
            set { vat = value; }
        }

        public decimal TaxTotal
        {
            get { return taxTotal; }
            set { taxTotal = value; }
        }

        public decimal TaxPer
        {
            get { return taxPer; }
            set { taxPer = value; }
        }

        public decimal ExchangeRate
        {
            get { return exchangeRate; }
            set { exchangeRate = value; }
        }

        public decimal Cess
        {
            get { return cess; }
            set { cess = value; }
        }

        public decimal RoundOff
        {
            get { return roundOff; }
            set { roundOff = value; }
        }

        public bool FlagDel
        {
            get { return flagDel; }
            set { flagDel = value; }
        }

        public bool CusLevelDiscount
        {
            get { return cusLevelDiscount; }
            set { cusLevelDiscount = value; }
        }

        public DataTable GetPriceType()
        {
            query = "SELECT CODE AS [key],CODE+' - '+DESC_ENG AS value FROM GEN_PRICE_TYPE";
            return DbFunctions.GetDataTable(query);
        }

        public DataTable ViewSalesHDR()
        {
            query = "SELECT * FROM viewSalesHDR";
            return DbFunctions.GetDataTable(query);
        }

        public DataTable ItemSuggestionForSales()
        {
            query = "item_suggestion_for_sales";
            Dictionary<string, object> parameter = new Dictionary<string, object>();
            parameter.Add("@isService", isService);
            return DbFunctions.GetDataTableProcedure(query,parameter);
        }

        public DataTable ItemSuggestionTest()
        {
            query = "itemSuggestion_test";
            return DbFunctions.GetDataTableProcedure(query);
        }

        public string MaxDocID()
        {
            query = "SELECT ISNULL(MAX(DOC_ID), 0) FROM INV_SALES_HDR WHERE (SALE_TYPE IS NULL OR SALE_TYPE='" + saleType + "') AND FLAGDEL='True'";
            return Convert.ToString(DbFunctions.GetAValue(query));
        }

        public string InvoiceTypeCode(string type)
        {
            query = "SELECT InvoiceStart FROM GEN_INV_STARTFROM WHERE InvoiceTypeCode='" + type + "'";
            return Convert.ToString(DbFunctions.GetAValue(query));
        }

        public DataTable TaxMaster()
        {
            query = "SELECT TaxId, CODE + ' --- ' +CONVERT(varchar(10),TaxRate)+' %' AS Expr1 FROM GEN_TAX_MASTER";
            return DbFunctions.GetDataTable(query);
        }

        public DataTable getValueSysSetup()
        {
            query = "SELECT Free, Mrp, GrossValue, NetValue, Description FROM SYS_SETUP";
            return DbFunctions.GetDataTable(query);
        }

        public DataTable GetCurrency()
        {
            query = "SELECT * FROM GEN_CURRENCY";
            return DbFunctions.GetDataTable(query);
        }

        public string SaleTypebyDoc_no()
        {
            query = "SELECT SALE_TYPE FROM INV_SALES_HDR WHERE DOC_NO = '" + docNo + "'";
            return (string)DbFunctions.GetAValue(query);
        }

        public string GenSaleType(string saletype)
        {
            query = "SELECT DESC_ENG FROM GEN_SALE_TYPE WHERE CODE ='" + saletype + "'";
            return (string)DbFunctions.GetAValue(query);
        }

        public DataTable getdatafromDocNo()
        {
            query = "SELECT dtl.*, PAY_SUPPLIER.DESC_ENG AS supplier_name, g.DESC_ENG AS [Group],ITEM.PRODUCT_TYPE PTYPE FROM INV_SALES_DTL as dtl LEFT JOIN PAY_SUPPLIER ON dtl.supplier_id = PAY_SUPPLIER.CODE LEFT JOIN INV_ITEM_GROUP as g ON dtl.group_id = g.CODE LEFT JOIN INV_ITEM_DIRECTORY ITEM ON ITEM.CODE=dtl.ITEM_CODE  WHERE DOC_NO = '" + docNo + "'AND FLAGDEL='TRUE'";
            return DbFunctions.GetDataTable(query);
        }
        public DataTable getdatafromDocNo_1()
        {
            query = "SELECT * FROM INV_SALES_HDR WHERE DOC_NO = '" + docNo + "'";
            return DbFunctions.GetDataTable(query);
        }

        public string VouchStartFrom()
        {
            query = "SELECT VouchStartFrom FROM GEN_VOUCH_STARTFROM WHERE VouchTypeCode='PAY'";
            return (string)DbFunctions.GetAValue(query);
        }


        public SqlDataReader DocIdbyDocId()
        {
            query = "SELECT DOC_ID FROM INV_SALES_HDR WHERE DOC_ID = '" + docId + "'AND FLAGDEL='TRUE' AND (SALE_TYPE IS NULL OR SALE_TYPE='')";
            return DbFunctions.GetDataReader(query);
        }

        public SqlDataReader DetailsByDocIDSaleType()
        {
            query = "SELECT * FROM INV_SALES_HDR WHERE DOC_ID = '" + docId + "'AND FLAGDEL='TRUE' AND SALE_TYPE= '" + saleType + "'";
            return DbFunctions.GetDataReader(query);
        }

        public DataTable DetailsByDocIDSaleType_Dt()
        {
            query = "SELECT * FROM INV_SALES_HDR WHERE DOC_ID = '" + docId + "'AND FLAGDEL='TRUE' AND SALE_TYPE= '" + saleType + "'";
            return DbFunctions.GetDataTable(query);
        }

        public DataTable MaxDocid_2()
        {
            query = "SELECT MAX(DOC_ID) AS Expr1 FROM INV_SALES_HDR WHERE (SALE_TYPE IS NULL OR SALE_TYPE='"+saleType+"') AND FLAGDEL='True'";
            return DbFunctions.GetDataTable(query);
        }

        public DataTable MinDocid_2()
        {
            query = "SELECT MIN(DOC_ID) AS Expr1 FROM INV_SALES_HDR WHERE (SALE_TYPE IS NULL OR SALE_TYPE='"+saleType+"') AND FLAGDEL='True'";
            return DbFunctions.GetDataTable(query);
        }

        public DataTable SalTypeDesc(string desc)
        {
            query = "SELECT CODE FROM GEN_SALE_TYPE WHERE DESC_ENG='" + desc + "'";
            return DbFunctions.GetDataTable(query);
        }

        public void UpdateCreditNote(string cndoc)
        {
            query = "UPDATE tbl_CreditNote SET  Status='Inactive'  WHERE CN_Doc_No = '" + cndoc + "' ";
            DbFunctions.InsertUpdate(query);
        }

        public DataTable CreditNoteDtl(string cndoc)
        {
            query = "SELECT * FROM tbl_CreditNote WHERE CN_Doc_No = '" + cndoc + "'";
            return DbFunctions.GetDataTable(query);
        }
        

        public string prefix_set(string InvType)
        {
            query = "SELECT COUNT(*) FROM GEN_DOC_SERIAL";
            
            int row = Convert.ToInt32(DbFunctions.GetAValue(query));
            if (row != 0)
            {
                if (InvType == "B2B")
                {
                    query = "SELECT ISNULL(PRIFIX,'') FROM GEN_DOC_SERIAL";
                    return (string)DbFunctions.GetAValue(query);
                }

                else if (InvType == "B2C")
                {
                    query = "SELECT ISNULL(SUFIX,'') FROM GEN_DOC_SERIAL";
                    return (string)DbFunctions.GetAValue(query);
                }

                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }
        public DataTable getAllFromView()
        {
            query = "SELECT * FROM viewSalesHDR";
            return DbFunctions.GetDataTable(query);
        }
        public object storedProc(string cmd,string para,string val)
        {
            Dictionary<string, object> parameter = new Dictionary<string, object>();
            parameter.Add(para,val);
            return DbFunctions.GetAValueProcedure(cmd, parameter);
        }
        public object getMaxDocId()
        {
            query = "SELECT ISNULL(MAX(DOC_ID), 0) FROM INV_SALES_HDR WHERE DOC_TYPE IN( '" + docType + "') AND FLAGDEL='True'";
            return DbFunctions.GetAValue(query);
        }
        public DataTable getAllByDocId()
        {
            query = "select * from INV_SALES_HDR WHERE DOC_ID=" + docId;
            return DbFunctions.GetDataTable(query);

        }
        public int deleteByDocNo()
        {
            query = "DELETE FROM INV_SALES_HDR WHERE DOC_NO ='" + docNo + "';DELETE FROM  INV_SALES_DTL WHERE DOC_NO ='" + docNo + "';DELETE FROM  tbl_CreditNote WHERE CN_Doc_No ='" + docNo + "'";
            return DbFunctions.InsertUpdate(query);
        }

        public string getRate(string itemcode, string uom, string ratecode)
        {              
            Dictionary<string, object> parameter = new Dictionary<string, object>();
            query = "GET_RATE1";
            parameter.Add("@ITEM_CODE", itemcode);
            parameter.Add("@UNIT_CODE", uom);
            parameter.Add("@RATE_CODE", ratecode);
            return Convert.ToString(DbFunctions.GetAValueProcedure(query, parameter));
        }

        public int insertCreditNotes()
        {
            query = "insert into tbl_CreditNote(CN_Doc_No,CN_Date,CN_DateHij,CN_Reffrence_No,CUSTOMER_CODE,CUSTOMER_NAME_ENG,NOTES,CN_Balance,Nett_Amount,Status)values('" + docNo + "','" + docDateGre + "','" + docDateHij + "','" + docReference + "','" + customerCode + "','" + customerNameEng + "','" + notes + "','" + exchangeRate + "','" + netAmount + "','" + branch + "')";
            return DbFunctions.InsertUpdate(query);
        }

        public DataTable Getunit()
        {
            //cmd.Connection = conn;          
            query = "SELECT CODE,DESC_ENG  FROM INV_UNIT";
            return DbFunctions.GetDataTable(query);
        }

        public string getBarcodeRate(string barco)
        {
            query = "SELECT Sale_Price from RateChange WHERE R_Id='" + barco + "'";
            string price = Convert.ToString(DbFunctions.GetAValueProcedure(query));
            return price;
        }
        public SqlDataReader getCustLedId()
        {
            
            query = "select DESC_ENG,LedgerId from REC_CUSTOMER WHERE CODE='" + customerCode + "'";
            return DbFunctions.GetDataReader(query);
        }
        public string getDocType()
        {
            query = "select DOC_TYPE from INV_SALES_HDR WHERE DOC_NO='" + docNo+ "'";
            return (string)DbFunctions.GetAValue(query);
        }
        public string getState()
        {
            string query = "SELECT STATE FROM REC_CUSTOMER WHERE CODE=" + customerCode;

            return DbFunctions.GetAValue(query).ToString();
        }
    }
}
