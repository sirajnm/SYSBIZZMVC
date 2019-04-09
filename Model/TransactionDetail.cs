using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sys_Sols_Inventory.Model
{
    class TransactionDetail
    {
        private string _ShiftNo, _Branch, _TillID, _TransactionNo, _CashierID, _ItemNo, _UOM;
        private int _lineno;
        private Single _UOMQty, _Quantity, _PriceVAT, _VATPercentage, _CESSPercentage, _DiscountPercentage, _DiscountAmount, _AmountForPay, _VATAmount, _CESSAmount, _NetAmount;

       public string ShiftNo
        {
            get { return _ShiftNo; }
            set { _ShiftNo = value; }
        }

        public string Branch
        {
            get { return _Branch; }
            set { _Branch = value; }
        }

        public string TillID
        {
            get { return _TillID; }
            set { _TillID = value; }
        }
        public string TransactionNo
        {
            get { return _TransactionNo; }
            set { _TransactionNo = value; }
        }
        public string CashierID
        {
            get { return _TransactionNo; }
            set { _TransactionNo = value; }
        }
        public string ItemNo
        {
            get { return _ItemNo; }
            set { _ItemNo = value; }
        }
        public string UOM
        {
            get { return _UOM; }
            set { _UOM = value; }
        }
        public int LineNo
        {
            get { return _lineno; }
            set { _lineno = value; }
        }
        public Single UOMQty
        {
            get { return _UOMQty; }
            set { _UOMQty = value; }
        }
        public Single Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
        public Single PriceVAT
        {
            get { return _PriceVAT; }
            set { _PriceVAT = value; }
        }
        public Single VATPercentage
        {
            get { return _VATPercentage; }
            set { _VATPercentage = value; }
        }
        public Single CESSPercentage
        {
            get { return _CESSPercentage; }
            set { _CESSPercentage = value; }
        }
        public Single DiscountPercentage
        {
            get { return _DiscountPercentage; }
            set { _DiscountPercentage = value; }
        }
        public Single DiscountAmount
        {
            get { return _DiscountAmount; }
            set { _DiscountAmount = value; }
        }
        public Single AmountForPay
        {
            get { return _AmountForPay; }
            set { _AmountForPay = value; }
        }
        public Single VATAmount
        {
            get { return _VATAmount; }
            set { _VATAmount = value; }
        }
        public Single CESSAmount
        {
            get { return _CESSAmount; }
            set { _CESSAmount = value; }
        }
        public Single NetAmount
        {
            get { return _NetAmount; }
            set { _NetAmount = value; }
        }

        public TransactionDetail()
        {

        }




    }
}
