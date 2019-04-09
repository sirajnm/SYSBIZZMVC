using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sys_Sols_Inventory.Model
{
    class POS_Customer
    {
        string _CustomerMobileNo, _CustomerName, _CustomerAddress1, _CustomerAddress2, _CustomerID;
        public string CustomerMobileNo
        {
            get { return _CustomerMobileNo; }
            set { _CustomerMobileNo = value; }
        }
        public string CustomerName
        {
            get { return _CustomerName; }
            set { _CustomerName = value; }
        }
        public string CustomerAddress1
        {
            get { return _CustomerAddress1; }
            set { _CustomerAddress1 = value; }
        }
        public string CustomerAddress2
        {
            get { return _CustomerAddress2; }
            set { _CustomerAddress2 = value; }
        }
        public string CustomerID
        {
            get { return _CustomerID; }
            set { _CustomerID = value; }
        }

        public bool IsCreditCustomer
        {
            get; set;
        }
        public string CustomerLedger
        {
            get; set;
        }

        public POS_Customer()
        {

        }

    }
}
