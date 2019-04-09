using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sys_Sols_Inventory.Model
{
    class InvStkMaster
    {
        private string branch, store, itemCode, batch;
        private int openingQty, transInQty, transferOutQty, writeOffQty, issueQty, issueRetQty, adjustQty;
        private int bonusQty, focQty, damageQty, purchaseQty, purchaseRetQty, cashSaleQty, creditSaleQty;
        private int cashSalesRetQty, creditSalesRetQty, availableQty, totalSalesQty;
        private decimal openingValue, transInValue, transferOutValue, writeOffValue, IssueValue, issueRetValue, adjustValue;
        private decimal bonusValue, focValue, damageValue, purchaseValue, purchaseRetValue, cashSaleValue, creditSaleValue;
        private decimal cashSalesRetValue, creditSalesRetValue, costOfSale, stockValue, totalSalesValue, profit;
        private DateTime expiryDAte;

        public DateTime ExpiryDAte
        {
            get { return expiryDAte; }
            set { expiryDAte = value; }
        }

        public decimal Profit
        {
            get { return profit; }
            set { profit = value; }
        }

        public decimal TotalSalesValue
        {
            get { return totalSalesValue; }
            set { totalSalesValue = value; }
        }

        public decimal StockValue
        {
            get { return stockValue; }
            set { stockValue = value; }
        }

        public decimal CostOfSale
        {
            get { return costOfSale; }
            set { costOfSale = value; }
        }

        public decimal CreditSalesRetValue
        {
            get { return creditSalesRetValue; }
            set { creditSalesRetValue = value; }
        }

        public decimal CashSalesRetValue
        {
            get { return cashSalesRetValue; }
            set { cashSalesRetValue = value; }
        }
        public decimal CreditSaleValue
        {
            get { return creditSaleValue; }
            set { creditSaleValue = value; }
        }

        public decimal CashSaleValue
        {
            get { return cashSaleValue; }
            set { cashSaleValue = value; }
        }

        public decimal PurchaseRetValue
        {
            get { return purchaseRetValue; }
            set { purchaseRetValue = value; }
        }

        public decimal PurchaseValue
        {
            get { return purchaseValue; }
            set { purchaseValue = value; }
        }

        public decimal DamageValue
        {
            get { return damageValue; }
            set { damageValue = value; }
        }

        public decimal FocValue
        {
            get { return focValue; }
            set { focValue = value; }
        }

        public decimal BonusValue
        {
            get { return bonusValue; }
            set { bonusValue = value; }
        }
        
        public decimal AdjustValue
        {
            get { return adjustValue; }
            set { adjustValue = value; }
        }

        public decimal IssueRetValue
        {
            get { return issueRetValue; }
            set { issueRetValue = value; }
        }

        public decimal IssueValue1
        {
            get { return IssueValue; }
            set { IssueValue = value; }
        }

        public decimal WriteOffValue
        {
            get { return writeOffValue; }
            set { writeOffValue = value; }
        }

        

        public decimal TransferOutValue
        {
            get { return transferOutValue; }
            set { transferOutValue = value; }
        }

        public decimal TransInValue
        {
            get { return transInValue; }
            set { transInValue = value; }
        }

        public decimal OpeningValue
        {
            get { return openingValue; }
            set { openingValue = value; }
        }
        
        public int TotalSalesQty
        {
            get { return totalSalesQty; }
            set { totalSalesQty = value; }
        }

        public int AvailableQty
        {
            get { return availableQty; }
            set { availableQty = value; }
        }

        public int CreditSalesRetQty
        {
            get { return creditSalesRetQty; }
            set { creditSalesRetQty = value; }
        }

        public int CashSalesRetQty
        {
            get { return cashSalesRetQty; }
            set { cashSalesRetQty = value; }
        }
        
        public int CreditSaleQty
        {
            get { return creditSaleQty; }
            set { creditSaleQty = value; }
        }

        public int CashSaleQty
        {
            get { return cashSaleQty; }
            set { cashSaleQty = value; }
        }

        public int PurchaseRetQty
        {
            get { return purchaseRetQty; }
            set { purchaseRetQty = value; }
        }

        public int PurchaseQty
        {
            get { return purchaseQty; }
            set { purchaseQty = value; }
        }

        public int DamageQty
        {
            get { return damageQty; }
            set { damageQty = value; }
        }

        public int FocQty
        {
            get { return focQty; }
            set { focQty = value; }
        }

        public int BonusQty
        {
            get { return bonusQty; }
            set { bonusQty = value; }
        }

        public int AdjustQty
        {
            get { return adjustQty; }
            set { adjustQty = value; }
        }

        public int IssueRetQty
        {
            get { return issueRetQty; }
            set { issueRetQty = value; }
        }

        public int IssueQty
        {
            get { return issueQty; }
            set { issueQty = value; }
        }

        public int WriteOffQty
        {
            get { return writeOffQty; }
            set { writeOffQty = value; }
        }

        public int TransferOutQty
        {
            get { return transferOutQty; }
            set { transferOutQty = value; }
        }

        public int TransInQty
        {
            get { return transInQty; }
            set { transInQty = value; }
        }

        public int OpeningQty
        {
            get { return openingQty; }
            set { openingQty = value; }
        }

        public string Batch
        {
            get { return batch; }
            set { batch = value; }
        }

        public string ItemCode
        {
            get { return itemCode; }
            set { itemCode = value; }
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
