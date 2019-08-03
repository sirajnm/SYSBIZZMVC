using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;

namespace Sys_Sols_Inventory.Model
{
    class InvSalOrdDtlDB
    {
        private int docId, forsort;
        private string itemCode, itemDescEng, itemDeskArb, uom, rsrvdFld1, rsrvdFld2, rsrvdFld3, docNo, docType;
        private string branch, store, batch, discType, discValue, serialNo, mrp, free, supplierId, groupId, priceBatch;
        private decimal cost, price, quantity, itemTaxPer, itemTax, taxDist, itemDiscount, grossTotal, itemTotal;
        private decimal itemTot, grossTot, purPrice, uomQty, costPrice;
        private DateTime expiryDate;
        private bool flagDel, hasBar;

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
        public decimal CostPrice
        {
            get { return costPrice; }
            set { costPrice = value; }
        }

        public decimal UomQty
        {
            get { return uomQty; }
            set { uomQty = value; }
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
       
        public decimal ItemTotal
        {
            get { return itemTotal; }
            set { itemTotal = value; }
        }

        public decimal GrossTotal
        {
            get { return grossTotal; }
            set { grossTotal = value; }
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

        public decimal Quantity
        {
            get { return quantity; }
            set { quantity = value; }
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

        public int Forsort
        {
            get { return forsort; }
            set { forsort = value; }
        }

        public int DocId
        {
            get { return docId; }
            set { docId = value; }
        }

        public SqlDataReader DtlsbyDocID()
        {
            string query = "SELECT * FROM INV_SAL_ORD_DTL WHERE DOC_ID = '" + docId + "'";
            return DbFunctions.GetDataReader(query);
        }

        
        
    }
}
