using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sys_Sols_Inventory.Model
{
    class InvStkMovement
    {
        private string branch, store, docNo, docType, docDateHij, docReference, itemCode, descEng, descArb, batch;
        private DateTime docDateGre, expiryDate;
        private int quantityIn, quantityOut;
        private decimal costPrice, stockValue;

        public decimal StockValue
        {
            get { return stockValue; }
            set { stockValue = value; }
        }

        public decimal CostPrice
        {
            get { return costPrice; }
            set { costPrice = value; }
        }

        public int QuantityOut
        {
            get { return quantityOut; }
            set { quantityOut = value; }
        }

        public int QuantityIn
        {
            get { return quantityIn; }
            set { quantityIn = value; }
        }
        public DateTime ExpiryDate
        {
            get { return expiryDate; }
            set { expiryDate = value; }
        }

        public DateTime DocDateGre
        {
            get { return docDateGre; }
            set { docDateGre = value; }
        }
        
        public string Batch
        {
            get { return batch; }
            set { batch = value; }
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

        public string ItemCode
        {
            get { return itemCode; }
            set { itemCode = value; }
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
        
    }
}
