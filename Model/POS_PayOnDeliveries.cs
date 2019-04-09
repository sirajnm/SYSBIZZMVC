using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sys_Sols_Inventory.Model
{
    class POS_PayOnDelivery
    {
        string _ShiftNo, _Branch, _TillID, _TransactionNo, _CashierID, _DeliveryBoyID;
        Single _TransactionAmount, _CollectedAmount;

        public String ShiftNo
        {
            get { return _ShiftNo; }
            set { _ShiftNo = value; }
        }

        public String Branch
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
            get { return _CashierID; }
            set { _CashierID = value; }
        }
        public string DeliveryBoyID
        {
            get { return _DeliveryBoyID; }
            set { _DeliveryBoyID = value; }
        }

        public POS_PayOnDelivery()
        {

        }

    }
}
