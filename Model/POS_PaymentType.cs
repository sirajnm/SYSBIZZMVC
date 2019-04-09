using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sys_Sols_Inventory.Model
{
    class POS_PaymentType
    {
    public string  PaymentCode { get; set; }
    public string PaymentDescription { get; set; }
    public string CurrencyCode { get; set; }
    public string GLAccount { get; set; }
    public string PaymentType { get; set; }
    public Boolean RefundAllowed { get; set; }
public Boolean OverTenderAllowed { get; set; }



    }
}
