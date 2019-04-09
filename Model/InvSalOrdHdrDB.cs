using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sys_Sols_Inventory.Model
{
    class InvSalOrdHdrDB
    {
        private string branch, store, docNo, docType, docDateHij, currencyCode, docReference, branchOther, saleType;
        private string priceType, customerCode, customerNameEng, customerNameArb, SalesmanCode, notes, cancelFlag;
        private string printFlag, editFlag, payCode, cardNo, poNo, plantCode, posted, debitOrAcc, creditOrAcc;
        private DateTime docDateGre, currentDate;
        private decimal exchangeRate, taxPer, taxTotal, vat, dicount, totalAmount, netAmount, totalCost, docId;
        private decimal freight, roundOff, cess;
        private bool cusLevelDiscount, flagDel;

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

        public decimal Dicount
        {
            get { return dicount; }
            set { dicount = value; }
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

        public string PoNo
        {
            get { return poNo; }
            set { poNo = value; }
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

        public string SalesmanCode1
        {
            get { return SalesmanCode; }
            set { SalesmanCode = value; }
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
    }
}
