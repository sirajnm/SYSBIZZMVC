using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
namespace Sys_Sols_Inventory.Model
{
    class InvStkTrxDtlDB
    {
        private int docId, forSort;
        private string itemCode, itemDescEng, itemDescArb, uom, rsrvdDtlFld1, rsrvdDtlFld2, rsrvdDtlFld3, batch, docNo;
        private string docType, branch, store, docReference, priceBatch;
        private double taxPer, quantity, itemTotal, uomQty, costPrice;
        private decimal taxAmount, price;
        private DateTime expiryDate;

        public DateTime ExpiryDate
        {
            get { return expiryDate; }
            set { expiryDate = value; }
        }

        public decimal Price
        {
            get { return price; }
            set { price = value; }
        }

        public decimal TaxAmount
        {
            get { return taxAmount; }
            set { taxAmount = value; }
        }
        public double CostPrice
        {
            get { return costPrice; }
            set { costPrice = value; }
        }

        public double UomQty
        {
            get { return uomQty; }
            set { uomQty = value; }
        }

        public double ItemTotal
        {
            get { return itemTotal; }
            set { itemTotal = value; }
        }

        public double Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        public double TaxPer
        {
            get { return taxPer; }
            set { taxPer = value; }
        }
       
        public string PriceBatch
        {
            get { return priceBatch; }
            set { priceBatch = value; }
        }

        public string DocReference
        {
            get { return docReference; }
            set { docReference = value; }
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

        public string Batch
        {
            get { return batch; }
            set { batch = value; }
        }

        public string RsrvdDtlFld3
        {
            get { return rsrvdDtlFld3; }
            set { rsrvdDtlFld3 = value; }
        }

        public string RsrvdDtlFld2
        {
            get { return rsrvdDtlFld2; }
            set { rsrvdDtlFld2 = value; }
        }

        public string RsrvdDtlFld1
        {
            get { return rsrvdDtlFld1; }
            set { rsrvdDtlFld1 = value; }
        }

        public string Uom
        {
            get { return uom; }
            set { uom = value; }
        }

        public string ItemDescArb
        {
            get { return itemDescArb; }
            set { itemDescArb = value; }
        }

        public string ItemDescEng
        {
            get { return itemDescEng; }
            set { itemDescEng = value; }
        }

        public string ItemCode
        {
            get { return itemCode; }
            set { itemCode = value; }
        }

        public int ForSort
        {
            get { return forSort; }
            set { forSort = value; }
        }

        public int DocId
        {
            get { return docId; }
            set { docId = value; }
        }


        public void Insert()
        {
          string query = "INSERT INTO INV_STK_TRX_DTL(DOC_TYPE,DOC_NO,ITEM_CODE,ITEM_DESC_ENG,UOM,PRICE,QUANTITY,BRANCH,PRICE_BATCH,UOM_QTY) VALUES('"+docType+"','" + docNo + "','" + itemCode + "','" + itemDescEng + "','" + uom + "','" + price + "','" + quantity + "','" + branch + "','" + priceBatch + "','"+UomQty+"')";
          DbFunctions.InsertUpdate(query);
        }

        public DataTable DocNobyItemCode()
        {
            string query = "SELECT (DOC_NO) FROM INV_STK_TRX_DTL WHERE ITEM_CODE='" + itemCode + "'";
            return DbFunctions.GetDataTable(query);
        }
        

        public DataTable selectUnits()
        {
            string query = "SELECT UNIT_CODE,PACK_SIZE FROM INV_ITEM_DIRECTORY_UNITS WHERE ITEM_CODE = '" + itemCode + "'";
            return DbFunctions.GetDataTable(query);
        }
        public SqlDataReader selectunits()
        {
            string command = "SELECT UNIT_CODE,PACK_SIZE FROM INV_ITEM_DIRECTORY_UNITS WHERE ITEM_CODE = '" + itemCode + "'";
           
            return DbFunctions.GetDataReader(command);
        }
        public SqlDataReader selectItemPrices()
        {
            string command = "SELECT UNIT_CODE,SAL_TYPE,PRICE FROM INV_ITEM_PRICE_DF WHERE ITEM_CODE = '" + itemCode + "' AND UNIT_CODE='" + uom + "' ";
            return DbFunctions.GetDataReader(command);
        }
        public SqlDataReader getAllSData()
        {
            string command = "SELECT * FROM INV_STK_TRX_DTL WHERE DOC_NO = '" + docNo + "'";
            return DbFunctions.GetDataReader(command);
        }
    }
}
