using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sys_Sols_Inventory
{
    public enum genEnum
    {
        Country,
        City,
        Type,
        Group,
        Category,
        TradeMark,
        CustomerType,
        Units,
        RateSlab,
        PayType,
        Supplier,
        Customer,
        Branch,
        DiscountType,
        PriceType,
        SupplierType,
        Location,
        StockOut,
        State,
        Item_level_offer
    }

    public enum Settings
    {
        Tax,
        Batch,
        Barcode,
        POSTerminal,
        Arabic,
        HasType,
        HasGroup,
        HasCategory,
        HasTM,
        Free,
        Mrp,
        GrossValue,
        NetValue,
        Description,
        HasAccessLimit,
        MoveToPrice,
        SelectLastPurchase,
        HasRoundOff,
        MoveToDisc,
        ShowPurchase,
        StockOut,
        Exclusive_tax,
        Pur_Exclusive_tax,
        priceBatch
    }
}
