using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sys_Sols_Inventory.Manufacture
{
    class ProductionRawMaterial
    {
        public String ItemCode { get; set; }
        public String ItemName { get; set; }
        public String Batch { get; set; }
        public String UOM { get; set; }
        public double PackSize { get; set; }
        public double Qty { get; set; }
        public double CostPrice { get; set; }
        public double DamagePackSize { get; set; }
        public String DamageUOM { get; set; }
        public double DamageQty { get; set; }

        public ProductionRawMaterial(String itemCode, String itemName, String batch, String uom, double packSize, double qty, double costPrice, double damagePackSize, String damageUOM, double damageQty)
        {
            ItemCode = itemCode;
            ItemName = itemName;
            Batch = batch;
            UOM = uom;
            PackSize = packSize;
            Qty = qty;
            CostPrice = costPrice;
            DamagePackSize = damagePackSize;
            DamageUOM = damageUOM;
            DamageQty = damageQty;
        }
    }
}
