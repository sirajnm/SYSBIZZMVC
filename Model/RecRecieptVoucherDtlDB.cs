using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sys_Sols_Inventory.Model
{
    class RecRecieptVoucherDtlDB
    {
       private  int id;
       private  string docNo, custCode, invDateHij, invNo;
       private  DateTime invDateGre;
       private  decimal amount, paid, balance, currentPayAmount;

        public decimal CurrentPayAmount
        {
            get { return currentPayAmount; }
            set { currentPayAmount = value; }
        }

        public decimal Balance
        {
            get { return balance; }
            set { balance = value; }
        }

        public decimal Paid
        {
            get { return paid; }
            set { paid = value; }
        }

        public decimal Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        public DateTime InvDateGre
        {
            get { return invDateGre; }
            set { invDateGre = value; }
        }
        public string InvNo
        {
            get { return invNo; }
            set { invNo = value; }
        }

        public string InvDateHij
        {
            get { return invDateHij; }
            set { invDateHij = value; }
        }

        public string CustCode
        {
            get { return custCode; }
            set { custCode = value; }
        }

        public string DocNo
        {
            get { return docNo; }
            set { docNo = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        
    }
}
