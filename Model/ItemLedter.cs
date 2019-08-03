using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys_Sols_Inventory.Model
{
    class ItemLedger
    {
        public int EntryNo {get; set;}
        public string EntryType { get; set; }
        public DateTime EntryDate { get; set; }
        public String DocumentNo { get; set; }
        public string Branch { get; set; }
        public string ItemNo { get; set; }
        public string UOM { get; set; }
        public Single UOMQuantity { get; set; }
        public Single Quantity { get; set; }
        public double UnitCostApplied { get; set; }
        public double CostValueApplied { get; set; }
        public int BatchEntryNo { get; set; }
        public string SupplierBatch { get; set; }
        public DateTime ExpiryDate { get; set; }
        public Single RemainingQuantity { get; set; }
        public bool CostAdjusted { get; set; }


    }
}
