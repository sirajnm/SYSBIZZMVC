using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sys_Sols_Inventory.Class
{
    class Cls_additem_ba
    {
        public string code { get; set; }
        public string name { get; set; }
        public string saletype { get; set; }
        public string branch { get; set; }
        public string brcode { get; set; }
        public int tax { get; set; }
        public string unit { get; set; }
        public string price_batch { get; set; }

        public string qty { get; set; }
        public decimal pr { get; set; }
        public decimal rr { get; set; }

        public decimal wr { get; set; }
        public decimal mrp { get; set; }
        public decimal sr { get; set; }
        public decimal pum { get; set; }
    }
}
