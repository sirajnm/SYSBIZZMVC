using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sys_Sols_Inventory.Model
{
    class InvItemCategoryDB
    {
        string code, desc_Eng, desc_Arb;

        public string Desc_Arb
        {
            get { return desc_Arb; }
            set { desc_Arb = value; }
        }

        public string Desc_Eng
        {
            get { return desc_Eng; }
            set { desc_Eng = value; }
        }

        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        dboperation db = new dboperation();

        
    }
}
