using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
namespace Sys_Sols_Inventory.Model
{
    class StockDB
    {
        int id, batchIncrement;

        public int BatchIncrement
        {
            get { return batchIncrement; }
            set { batchIncrement = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        string itemId, mrp, suppId, batchId, manBatch, docNo, docType;

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

        public string ManBatch
        {
            get { return manBatch; }
            set { manBatch = value; }
        }

        public string BatchId
        {
            get { return batchId; }
            set { batchId = value; }
        }

        public string SuppId
        {
            get { return suppId; }
            set { suppId = value; }
        }

        public string Mrp
        {
            get { return mrp; }
            set { mrp = value; }
        }

        public string ItemId
        {
            get { return itemId; }
            set { itemId = value; }
        }
        decimal qty;

        public decimal Qty
        {
            get { return qty; }
            set { qty = value; }
        }
        decimal costPrice, costPriceWotTax;

        public decimal CostPriceWotTax
        {
            get { return costPriceWotTax; }
            set { costPriceWotTax = value; }
        }

        public decimal CostPrice
        {
            get { return costPrice; }
            set { costPrice = value; }
        }
        DateTime exDate, manDate;

        public DateTime ManDate
        {
            get { return manDate; }
            set { manDate = value; }
        }

        public DateTime ExDate
        {
            get { return exDate; }
            set { exDate = value; }
        }

        public void Insert()
        {
            string query = "INSERT INTO tblStock(Item_id, qty, Cost_price, supplier_id, MRP,batch_id,batch_increment) values(@item_id, @qty, @cost_price, @supplier_id, @mrp,@batch_id,@batch_increment)";
            Dictionary<string,object> Parameters=new Dictionary<string,object>();
            Parameters.Add("@item_id", itemId);
            Parameters.Add("@qty", qty);
            Parameters.Add("@cost_price", costPrice);
            Parameters.Add("@mrp", mrp);
            Parameters.Add("@supplier_id", suppId);
            Parameters.Add("@batch_id", batchId);
            Parameters.Add("@batch_increment", batchIncrement);
            DbFunctions.InsertUpdate(query, Parameters);
        }

        public void UpdateCostMRP()
        {
            string query = "update tblstock set Cost_price=@cost,MRP=@MRP WHERE batch_id='" + batchId + "'";
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@cost", costPrice);
            Parameters.Add("@MRP", mrp);
            DbFunctions.InsertUpdate(query, Parameters);
        }
        public DataTable SelectDataForReduceStk()
        {

            string Query = "SELECT ITEM_CODE, QUANTITY, UOM_QTY, cost_price,PRICE_BATCH FROM INV_STK_TRX_DTL WHERE DOC_NO   = '" +docNo  + "'";
           
            return DbFunctions.GetDataTable(Query);
        }
        public DataTable SelectDataForReduceStkWithType()
        {

            string Query = "SELECT ITEM_CODE,QUANTITY,UOM_QTY, cost_price,PRICE_BATCH FROM INV_STK_TRX_DTL WHERE DOC_NO = '" + docNo + "' AND DOC_TYPE = '" + docType + "'";

            return DbFunctions.GetDataTable(Query);
        }
        public DataTable SelectDataForReduceStkWithTypeFromPur()
        {

            string Query = "SELECT ITEM_CODE,QTY_RCVD,UOM_QTY, cost_price,PRICE_BATCH FROM INV_PURCHASE_DTL WHERE DOC_NO = '" + docNo + "' AND DOC_TYPE = '" + docType + "'";

            return DbFunctions.GetDataTable(Query);
        }
        public SqlDataReader selectPreVoucher()
        {

            string Query = "SELECT ITEM_CODE, QUANTITY, UOM_QTY, cost_price,PRICE_BATCH FROM INV_STK_TRX_DTL WHERE DOC_NO   = '" + docNo + "'";

            return DbFunctions.GetDataReader(Query);
        }
    }
}
