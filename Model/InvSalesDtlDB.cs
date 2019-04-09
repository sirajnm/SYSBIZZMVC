using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Sys_Sols_Inventory.Model
{
    class InvSalesDtlDB
    {
        private int docId, forSort;
        private string itemCode, itemDescEng, itemDeskArb, uom, rsrvdFld1, rsrvdFld2, rsrvdFld3, docNo, docType, branch, store, batch;
        private string discType, discValue, serialNo, mrp, free, supplierId, groupId, priceBatch;
        private decimal cost, price, itemTaxPer, itemTax, taxDist, itemDiscount, itemTot, grossTot, purPrice;
        private float quantity, grossTotal, itemTotal, uomQty, costPrice;
        private DateTime expiryDate;
        private bool flagDel, hasBar;
        string query;
        public bool HasBar
        {
            get { return hasBar; }
            set { hasBar = value; }
        }

        public bool FlagDel
        {
            get { return flagDel; }
            set { flagDel = value; }
        }

        public DateTime ExpiryDate
        {
            get { return expiryDate; }
            set { expiryDate = value; }
        }
        public float CostPrice
        {
            get { return costPrice; }
            set { costPrice = value; }
        }

        public float UomQty
        {
            get { return uomQty; }
            set { uomQty = value; }
        }

        public float ItemTotal
        {
            get { return itemTotal; }
            set { itemTotal = value; }
        }

        public float GrossTotal
        {
            get { return grossTotal; }
            set { grossTotal = value; }
        }

        public float Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        public decimal PurPrice
        {
            get { return purPrice; }
            set { purPrice = value; }
        }

        public decimal GrossTot
        {
            get { return grossTot; }
            set { grossTot = value; }
        }

        public decimal ItemTot
        {
            get { return itemTot; }
            set { itemTot = value; }
        }

        public decimal ItemDiscount
        {
            get { return itemDiscount; }
            set { itemDiscount = value; }
        }

        public decimal TaxDist
        {
            get { return taxDist; }
            set { taxDist = value; }
        }

        public decimal ItemTax
        {
            get { return itemTax; }
            set { itemTax = value; }
        }

        public decimal ItemTaxPer
        {
            get { return itemTaxPer; }
            set { itemTaxPer = value; }
        }

        public decimal Price
        {
            get { return price; }
            set { price = value; }
        }

        public decimal Cost
        {
            get { return cost; }
            set { cost = value; }
        }

        public string PriceBatch
        {
            get { return priceBatch; }
            set { priceBatch = value; }
        }

        public string GroupId
        {
            get { return groupId; }
            set { groupId = value; }
        }

        public string SupplierId
        {
            get { return supplierId; }
            set { supplierId = value; }
        }

        public string Free
        {
            get { return free; }
            set { free = value; }
        }

        public string Mrp
        {
            get { return mrp; }
            set { mrp = value; }
        }

        public string SerialNo
        {
            get { return serialNo; }
            set { serialNo = value; }
        }

        public string DiscValue
        {
            get { return discValue; }
            set { discValue = value; }
        }

        public string DiscType
        {
            get { return discType; }
            set { discType = value; }
        }

        public string Batch
        {
            get { return batch; }
            set { batch = value; }
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

        public string RsrvdFld3
        {
            get { return rsrvdFld3; }
            set { rsrvdFld3 = value; }
        }

        public string RsrvdFld2
        {
            get { return rsrvdFld2; }
            set { rsrvdFld2 = value; }
        }

        public string RsrvdFld1
        {
            get { return rsrvdFld1; }
            set { rsrvdFld1 = value; }
        }

        public string Uom
        {
            get { return uom; }
            set { uom = value; }
        }

        public string ItemDeskArb
        {
            get { return itemDeskArb; }
            set { itemDeskArb = value; }
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

        public DataTable DtlsbyDocNo()
        {
            query = "SELECT * FROM INV_SALES_DTL WHERE DOC_NO = '" + docNo + "'";
            return DbFunctions.GetDataTable(query);
        }

        public string QuantyIitem()
        {
            query = "select QUANTITY from INV_SALES_DTL where ITEM_CODE='" + itemCode + "' AND DOC_ID='" + docId + "' ";
            return (string)DbFunctions.GetAValue(query);
        }
        public DataTable itemDtlByDocNoType()
        {
            query = "SELECT ITEM_CODE, QUANTITY, UOM_QTY, cost_price,PRICE_BATCH,FREE FROM INV_SALES_DTL WHERE DOC_NO = '" + docNo + "' AND DOC_TYPE = '" + docType + "'";
            return DbFunctions.GetDataTable(query);
        }
        public SqlDataReader DtlsbyDocNo_1()
        {
            query = "SELECT * FROM INV_SALES_DTL WHERE DOC_NO = '" + docNo + "'AND FLAGDEL='TRUE'";
            return DbFunctions.GetDataReader(query);
        }
        public DataTable DtlByDocNoReader()
        {
            query = "SELECT * FROM INV_SALES_DTL WHERE DOC_NO = '" + docNo + "'";
            return DbFunctions.GetDataTable(query);
        }
        public DataTable itemDtlByDocNoTypeReader()
        {
            query = "SELECT ITEM_CODE, QUANTITY, UOM_QTY, cost_price, supplier_id,PRICE_BATCH FROM INV_SALES_DTL WHERE DOC_NO = '" + docNo + "' AND DOC_TYPE = '" + docType + "'";
            return DbFunctions.GetDataTable(query);
        }
        public object getDocTypeFromDocId()
        {
            query = "select DOC_TYPE FROM INV_SALES_DTL WHERE DOC_ID='" + docId + "'";
            return DbFunctions.GetAValue(query);
        }
        public SqlDataReader getAllDetails()
        {
            query = "SELECT dtl.*, PAY_SUPPLIER.DESC_ENG AS supplier_name FROM INV_SALES_DTL as dtl LEFT JOIN PAY_SUPPLIER ON dtl.supplier_id = PAY_SUPPLIER.CODE WHERE DOC_NO = '" + docNo + "'AND FLAGDEL='TRUE'";
            return DbFunctions.GetDataReader(query);
        }
        public SqlDataReader getAllByDocTypr()
        {
            query = "SELECT * FROM INV_SALES_DTL WHERE DOC_TYPR ='SAL.CSR'";
            return DbFunctions.GetDataReader(query);
        }
        public SqlDataReader getDetailsByDocNoAndType()
        {
            query = "SELECT ITEM_CODE, QUANTITY, UOM_QTY, cost_price,PRICE_BATCH FROM INV_SALES_DTL WHERE DOC_NO = '" + docNo + "' AND DOC_TYPE = '" + docType + "'";
            return DbFunctions.GetDataReader(query);
        }
        
    }
}
