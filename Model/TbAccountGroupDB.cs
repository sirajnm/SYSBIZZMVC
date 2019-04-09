using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sys_Sols_Inventory.Model
{
    class TbAccountGroupDB
    {
        private int accountId;
        private string descEng, descArb, under, isBuiltIn;

        public string IsBuiltIn
        {
            get { return isBuiltIn; }
            set { isBuiltIn = value; }
        }

        public string Under
        {
            get { return under; }
            set { under = value; }
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

        public int AccountId
        {
            get { return accountId; }
            set { accountId = value; }
        }
    }
}
