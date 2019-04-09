using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
namespace Sys_Sols_Inventory.Model
{
    class InvPurchaseDtlDB
    {
        private string branch, store, docNo, docType, poNo, itemCode, itemDescEng, itemDescArb, priceTypes;

      
       private  string uom, invoiceNo, distTax, batch, serialNo, diskType, discountAmt, discValue, priceBatch;
       private  float qtyOrd, qtyRcvd, itemGross, itemTotalFobInsCustom, uomQty, costPrice;
       private  decimal priceFob, itemDiscount, customPrc, insurancePrc, priceFobInsCustom, itemLandedCost;
       decimal itemLandedCostSar, itemTaxPer, itemTax, rtlPrice, netAmount;
        DateTime expiryDate;
        int docId, forSort;
        bool flagDel;
        public string PriceTypes
        {
            get { return priceTypes; }
            set { priceTypes = value; }
        }
        public bool FlagDel
        {
            get { return flagDel; }
            set { flagDel = value; }
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
        
        public DateTime ExpiryDate
        {
            get { return expiryDate; }
            set { expiryDate = value; }
        }
        
        public decimal NetAmount
        {
            get { return netAmount; }
            set { netAmount = value; }
        }

        public decimal RtlPrice
        {
            get { return rtlPrice; }
            set { rtlPrice = value; }
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

        public decimal ItemLandedCostSar
        {
            get { return itemLandedCostSar; }
            set { itemLandedCostSar = value; }
        }
        
        public decimal ItemLandedCost
        {
            get { return itemLandedCost; }
            set { itemLandedCost = value; }
        }

        public decimal PriceFobInsCustom
        {
            get { return priceFobInsCustom; }
            set { priceFobInsCustom = value; }
        }

        public decimal InsurancePrc
        {
            get { return insurancePrc; }
            set { insurancePrc = value; }
        }

        public decimal CustomPrc
        {
            get { return customPrc; }
            set { customPrc = value; }
        }

        public decimal ItemDiscount
        {
            get { return itemDiscount; }
            set { itemDiscount = value; }
        }

        public decimal PriceFob
        {
            get { return priceFob; }
            set { priceFob = value; }
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

        public float ItemTotalFobInsCustom
        {
            get { return itemTotalFobInsCustom; }
            set { itemTotalFobInsCustom = value; }
        }

        public float ItemGross
        {
            get { return itemGross; }
            set { itemGross = value; }
        }

        public float QtyRcvd
        {
            get { return qtyRcvd; }
            set { qtyRcvd = value; }
        }

        public float QtyOrd
        {
            get { return qtyOrd; }
            set { qtyOrd = value; }
        }

        public string PriceBatch
        {
            get { return priceBatch; }
            set { priceBatch = value; }
        }

        public string DiscValue
        {
            get { return discValue; }
            set { discValue = value; }
        }

        public string DiscountAmt
        {
            get { return discountAmt; }
            set { discountAmt = value; }
        }

        public string DiskType
        {
            get { return diskType; }
            set { diskType = value; }
        }

        public string SerialNo
        {
            get { return serialNo; }
            set { serialNo = value; }
        }

        public string Batch
        {
            get { return batch; }
            set { batch = value; }
        }

        public string DistTax
        {
            get { return distTax; }
            set { distTax = value; }
        }

        public string InvoiceNo
        {
            get { return invoiceNo; }
            set { invoiceNo = value; }
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

        public string PoNo
        {
            get { return poNo; }
            set { poNo = value; }
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
        public string getPriceWithType()
        {
            string query = "SELECT PRICE from INV_ITEM_PRICE_DF INNER JOIN SYS_SETUP ON DefaultRateType= SAL_TYPE where ITEM_CODE='" + itemCode + "' AND UNIT_CODE='" + uom + "'";

            return DbFunctions.GetAValue(query).ToString();
        }
        public DataTable  getPriceWithAllType()
        {
            string query = "SELECT PRICE from INV_ITEM_PRICE_DF  where ITEM_CODE='" + itemCode+ "' AND SAL_TYPE='" + priceTypes + "' and UNIT_CODE='" + uom + "'";

            return DbFunctions.GetDataTable(query);
        }
        public SqlDataReader getPrice()
        {
            string query = "SELECT        PRICE, SAL_TYPE, ITEM_CODE FROM  INV_ITEM_PRICE WHERE        (SAL_TYPE ='" + priceTypes + "') AND (ITEM_CODE = '" + itemCode + "')";

            return DbFunctions.GetDataReader(query);
        }
        public SqlDataReader selectPurchaseDlt()
        {

            string Query = "SELECT * FROM INV_PURCHASE_DTL WHERE DOC_NO = '" + docNo + "'";
           
            return DbFunctions.GetDataReader(Query);
        }
        public DataTable selectPurchaseHdr()
        {

            string Query = "SELECT * FROM INV_PURCHASE_HDR WHERE DOC_NO = '" + docNo + "'";

            return DbFunctions.GetDataTable(Query);
        }
        public SqlDataReader selectPurchaseDtlWthDelFlag()
        {

            string Query = "SELECT * FROM INV_PURCHASE_DTL WHERE DOC_NO = '" + docNo + "'AND FLAGDEL='TRUE'";

            return DbFunctions.GetDataReader(Query);
        }
        public DataTable getPurchaseDlt()
        {

            string Query = "SELECT * FROM INV_PURCHASE_DTL WHERE DOC_NO = '" + docNo + "'";

            return DbFunctions.GetDataTable(Query);
        }
        public DataTable getAllRates()
        {
            string query = "GET_ALL_RATES";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@ITEM_CODE", itemCode);
            parameters.Add("@UNIT_CODE", uom);
            return DbFunctions.GetDataTableProcedure(query, parameters);
        }
        public string getBarcode()
        {
            string query = "SELECT BARCODE FROM INV_ITEM_DIRECTORY_UNITS WHERE ITEM_CODE='" + itemCode + "'";
            return Convert.ToString(DbFunctions.GetAValue(query));
        }
        public SqlDataReader getPreviousPurDtl()
        {

            string Query = "PREV_PURCHASE_INV";
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@ID", DocNo);
            return DbFunctions.GetDataReaderProcedure(Query,Parameters);
        }
        public DataTable getDetails()
        {
            string query = "SELECT dtl.*,INV_PURCHASE_HDR.SUPPLIER_CODE FROM INV_PURCHASE_DTL as dtl LEFT JOIN INV_PURCHASE_HDR ON dtl.DOC_NO=INV_PURCHASE_HDR.DOC_NO WHERE dtl.DOC_NO ='" + docNo + "'AND dtl.FLAGDEL='TRUE'";
            return DbFunctions.GetDataTable(query);
        }
        public SqlDataReader getItemDetails()
        {
            string query = "SELECT ITEM_CODE, QTY_RCVD, UOM_QTY, cost_price,PRICE_BATCH FROM INV_PURCHASE_DTL WHERE DOC_NO = '" + docNo + "' AND DOC_TYPE = '" + docType + "'";
            return DbFunctions.GetDataReader(query);
        }

        public DataTable getItemDetailsDt()
        {
            string query = "SELECT ITEM_CODE, QTY_RCVD, UOM_QTY, cost_price,PRICE_BATCH FROM INV_PURCHASE_DTL WHERE DOC_NO = '" + docNo + "' AND DOC_TYPE = '" + docType + "'";
            return DbFunctions.GetDataTable(query);
        }
        public DataTable getItemDetailsTable()
        {
            string query = "SELECT ITEM_CODE, QTY_RCVD, UOM_QTY, cost_price,PRICE_BATCH FROM INV_PURCHASE_DTL WHERE DOC_NO = '" + docNo + "' AND DOC_TYPE in('" + docType + "')";

            return DbFunctions.GetDataTable(query);
        }
        public object getDocTypeFromDocId()
        {
            string query = "select DOC_TYPE FROM INV_PURCHASE_DTL WHERE DOC_ID='" + docId + "'";
            return DbFunctions.GetAValue(query);
        }
    }
}
