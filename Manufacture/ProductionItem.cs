using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sys_Sols_Inventory.Manufacture
{
    class ProductionItem
    {
        public String ItemCode { get; set; }
        public String ItemName { get; set; }
        public String Batch { get; set; }
        public String PriceBatch { get; set; }
        public DateTime ExDate { get; set; }
        public String UOM { get; set; }
        public double PackSize { get; set; }
        public double Qty { get; set; }
        public double CostPrice { get; set; }
        public double MRP { get; set; }
        public double Total { get; set; }

        public ProductionItem(String itemCode, String itemName, String batch,String pricebatch, DateTime Exdate, String uom, double packSize, double qty, double costPrice, double mrp, double total)
        {
            ItemCode = itemCode;
            ItemName = itemName;
            Batch = batch;
            PriceBatch = pricebatch;
            ExDate = Exdate;
            UOM = uom;
            PackSize = packSize;
            Qty = qty;
            CostPrice = costPrice;
            MRP = mrp;
            Total = total;
        }
    }
}
