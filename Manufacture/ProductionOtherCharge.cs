using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sys_Sols_Inventory.Manufacture
{
    class ProductionOtherCharge
    {
        public String Description { get; set; }
        public Double Amount { get; set; }

        public ProductionOtherCharge(String description, double amount)
        {
            Description = description;
            Amount = amount;
        }
    }
}
