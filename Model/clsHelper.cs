using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Sys_Sols_Inventory.Model
{
 public  class clsHelper
    {
        private string code, type, docNo, tableName;private bool isService;

        public bool IsService
        {
            get { return isService; }
            set { isService = value; }
        }

        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        public string DocNo
        {
            get { return docNo; }
            set { docNo = value; }
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
     public DataTable getAllItemDetails()
     {
         string Query = "item_suggestionForReturn";
         Dictionary<string, object> parameter = new Dictionary<string, object>();
         parameter.Add("@isService", isService);
         return DbFunctions.GetDataTableProcedure(Query, parameter);
    
     }
     public void deleteItem()
     {
         string Query = "DELETE FROM INV_ITEM_DIRECTORY WHERE CODE = '" + code + "';DELETE FROM INV_ITEM_DIRECTORY_PICTURE WHERE CODE = '" + code+ "'";
          DbFunctions.InsertUpdate(Query);
     }
     public DataTable getAllOpenigVouchers()
     {
         string Query = "SELECT    INV_STK_TRX_HDR.DOC_NO,CONVERT (NVARCHAR, INV_STK_TRX_HDR.DOC_DATE_GRE,103) AS DOC_DATE_GRE,INV_STK_TRX_HDR.DOC_DATE_HIJ,INV_STK_TRX_HDR.DOC_REFERENCE,INV_STK_TRX_HDR.NOTES,INV_STK_TRX_HDR.TOTAL_AMOUNT,INV_STK_TRX_HDR.TAX_AMOUNT,BRANCH,BRANCH_OTHER FROM INV_STK_TRX_HDR  WHERE INV_STK_TRX_HDR.DOC_TYPE= '" + type + "'";
         return DbFunctions.GetDataTable(Query);
     }
     public void deleteOpeningStockVoucher()
     {
         string Query = "DELETE FROM INV_STK_TRX_HDR WHERE DOC_NO = '" + docNo+ "';DELETE FROM INV_STK_TRX_DTL WHERE DOC_NO = '" + docNo + "'";
         DbFunctions.InsertUpdate(Query);
     }
     public DataTable getAllPaymentVouchers()
     {
         string Query = "SELECT DOC_NO,CONVERT(NVARCHAR,DOC_DATE_GRE,103) AS DOC_DATE_GRE,DOC_DATE_HIJ,SUP_CODE,CONVERT(NVARCHAR,AMOUNT) AS AMOUNT,PAY_CODE,CHQ_NO,CONVERT(NVARCHAR,CHQ_DATE,103) AS CHQ_DATE,BANK_CODE,CUR_CODE,NOTES,TOTAL_PAID,TOTAL_CURRENT,TOTAL_BALANCE, DEBIT_CODE, CREDIT_CODE FROM PAY_PAYMENT_VOUCHER_HDR";
         return DbFunctions.GetDataTable(Query);
     }
     public DataTable getAllReceiptVouchers()
     {
         string Query = "SELECT DOC_NO, DOC_DATE_GRE,DOC_DATE_HIJ,CUST_CODE AS SUP_CODE,CONVERT(NVARCHAR,AMOUNT) AS AMOUNT,PAY_CODE,CHQ_NO,CONVERT(NVARCHAR,CHQ_DATE,103) AS CHQ_DATE,BANK_CODE,CUR_CODE,NOTES,TOTAL_PAID,TOTAL_CURRENT,TOTAL_BALANCE, DEBIT_CODE, CREDIT_CODE,DISCOUNT FROM REC_RECEIPTVOUCHER_HDR";
         //string Query = "SELECT DOC_NO,CONVERT(NVARCHAR,DOC_DATE_GRE,103) AS DOC_DATE_GRE,DOC_DATE_HIJ,CUST_CODE AS SUP_CODE,CONVERT(NVARCHAR,AMOUNT) AS AMOUNT,PAY_CODE,CHQ_NO,CONVERT(NVARCHAR,CHQ_DATE,103) AS CHQ_DATE,BANK_CODE,CUR_CODE,NOTES,TOTAL_PAID,TOTAL_CURRENT,TOTAL_BALANCE, DEBIT_CODE, CREDIT_CODE,DISCOUNT FROM REC_RECEIPTVOUCHER_HDR";
         return DbFunctions.GetDataTable(Query);
     }
     public void deletePaymentVoucher()
     {
         string Query = "DELETE FROM PAY_PAYMENT_VOUCHER_HDR WHERE DOC_NO = '" + docNo + "';DELETE FROM PAY_PAYMENT_VOUCHER_DTL WHERE DOC_NO = '" + docNo + "'";
         DbFunctions.InsertUpdate(Query);
     }
     public void deleteReceiptVoucher()
     {
         string Query = "DELETE FROM REC_RECEIPTVOUCHER_HDR WHERE DOC_NO = '" + docNo +"';DELETE FROM REC_RECEIPTVOUCHER_DTL WHERE DOC_NO = '" + docNo + "'";
         DbFunctions.InsertUpdate(Query);
     }
     public DataTable getAllPurchaseVoucher()
     {
         string Query = "SELECT  CONVERT(NVARCHAR, DOC_ID) AS DOC_ID,DOC_NO,CONVERT(NVARCHAR,DOC_DATE_GRE,103) AS DOC_DATE_GRE,DOC_DATE_HIJ,PUR_TYPE,DOC_REFERENCE,CURRENCY_CODE,SUPPLIER_CODE,SalesMan,NOTES,CONVERT(NVARCHAR,TOTAL_FOB_AMOUNT) AS TOTAL_AMOUNT,GROSS,TAX_TOTAL,NET_VAL,PAY_CODE,CARD_NO FROM INV_PURCHASE_HDR WHERE DOC_TYPE  in('" + type + "')";       //and DOC_TYPE!='SAL.CREDITNOTE'       
         return DbFunctions.GetDataTable(Query);
     }
     public void deletePurchase()
     {
         string Query = "DELETE FROM INV_PURCHASE_HDR WHERE DOC_NO = '" + docNo + "';DELETE FROM INV_SALES_DTL WHERE DOC_NO = '" + docNo+ "'";
         DbFunctions.InsertUpdate(Query);
     }
     public DataTable getAllPurchaseVoucherInPurHelper()
     {
         string Query = "SELECT DOC_NO,CONVERT(NVARCHAR,DOC_DATE_GRE,103) AS C,DOC_DATE_HIJ,DOC_REFERENCE,SUPPLIER_CODE,NOTES,PAY_CODE,CARD_NO,TAX_TOTAL,CESS_AMOUNT,GROSS,DISCOUNT_VAL,NET_VAL,SUP_INV_NO,FREIGHT_AMT FROM INV_PURCHASE_HDR where DOC_TYPE<>'LGR.PRT' ";
         return DbFunctions.GetDataTable(Query);
     }
     public DataTable getAllPurchaseReturnVoucherInPurHelper()
     {
         string Query = "SELECT  CONVERT(NVARCHAR, DOC_ID) AS DOC_ID,DOC_NO,CONVERT(NVARCHAR,DOC_DATE_GRE,103) AS DOC_DATE_GRE,DOC_DATE_HIJ,DOC_REFERENCE,CURRENCY_CODE,SUPPLIER_CODE,SalesMan,NOTES,CONVERT(NVARCHAR,TOTAL_FOB_AMOUNT) AS TOTAL_AMOUNT,GROSS,TAX_TOTAL,NET_VAL,PAY_CODE,CARD_NO,DEBIT_NOTE FROM INV_PURCHASE_HDR WHERE DOC_TYPE  in('" + type + "')";       //and DOC_TYPE!='SAL.CREDITNOTE'       
         return DbFunctions.GetDataTable(Query);
     }
     public DataTable getAllSalesVoucher()
     {
         string Query = "SELECT  CONVERT(NVARCHAR, DOC_ID) AS DOC_ID,DOC_NO,SALE_TYPE,CONVERT(NVARCHAR,DOC_DATE_GRE,103) AS DOC_DATE_GRE,DOC_DATE_HIJ,DOC_REFERENCE,CURRENCY_CODE,CUSTOMER_CODE,CUSTOMER_NAME_ENG,SALESMAN_CODE,NOTES,CONVERT(NVARCHAR,TOTAL_AMOUNT) AS TOTAL_AMOUNT,TAX_TOTAL,VAT,DISCOUNT,NET_AMOUNT,PAY_CODE,CARD_NO FROM INV_SALES_HDR WHERE DOC_TYPE not in('" + type + "')";       //and DOC_TYPE!='SAL.CREDITNOTE'       
         return DbFunctions.GetDataTable(Query);
     }
     public void deleteSales()
     {
         string Query = "DELETE FROM INV_SALES_HDR WHERE DOC_NO = '" + docNo+ "';DELETE FROM INV_SALES_DTL WHERE DOC_NO = '" +docNo + "'";
         DbFunctions.InsertUpdate(Query);
     }
     public DataTable getAllSaleVoucherInSaleHelper()
     {
         string Query = "SELECT  CONVERT(NVARCHAR, DOC_ID) AS DOC_ID,DOC_NO,CONVERT(NVARCHAR,DOC_DATE_GRE,103) AS DOC_DATE_GRE,DOC_DATE_HIJ,DOC_REFERENCE,CURRENCY_CODE,CUSTOMER_CODE,CUSTOMER_NAME_ENG,SALESMAN_CODE,NOTES,CONVERT(NVARCHAR,TOTAL_AMOUNT) AS TOTAL_AMOUNT,TAX_TOTAL,VAT,DISCOUNT,NET_AMOUNT,PAY_CODE,CARD_NO,FREIGHT,ROUNDOFF,CESS,CUSLEVELDISCOUNT FROM INV_SALES_HDR where DOC_TYPE<>'SAL.CSR'";
         return DbFunctions.GetDataTable(Query);
     }
     public DataTable getAllSaleReturnVoucherInSaleHelper()
     {
         string Query = "SELECT  CONVERT(NVARCHAR, DOC_ID) AS DOC_ID,DOC_NO,CONVERT(NVARCHAR,DOC_DATE_GRE,103) AS DOC_DATE_GRE,DOC_DATE_HIJ,DOC_REFERENCE,CURRENCY_CODE,CUSTOMER_CODE,CUSTOMER_NAME_ENG,SALESMAN_CODE,NOTES,CONVERT(NVARCHAR,TOTAL_AMOUNT) AS TOTAL_AMOUNT,TAX_TOTAL,VAT,DISCOUNT,NET_AMOUNT,PAY_CODE,CARD_NO,FREIGHT,ROUNDOFF,CESS,CUSLEVELDISCOUNT  FROM INV_SALES_HDR WHERE DOC_TYPE in ('" + type + "')";
         return DbFunctions.GetDataTable(Query);
     }
     public DataTable getAllSupplier()
     {
         string Query = "SELECT CODE,DESC_ENG,DESC_ARB,TYPE,ADDRESS_A,ADDRESS_B,POBOX,TELE1,TELE2,MOBILE,EMAIL,CITY_CODE,COUNTRY_CODE,CONVERT(NVARCHAR,OPENING_BAL,103) AS OPENING_BAL,FAX,LedgerId FROM PAY_SUPPLIER" + tableName;
         return DbFunctions.GetDataTable(Query);
     }
    }
}
