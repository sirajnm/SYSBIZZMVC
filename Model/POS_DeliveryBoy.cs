using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sys_Sols_Inventory.Model
{
    class POS_DeliveryBoy
    {
       
       

        public string DeliveryBoyID
        {
            get; set;
        }

public string DeliveryBoyName
        {
            get; set;
        }
public string EmployeeNo
        {
            get; set;
        }
public string ContactNo
        {
            get; set;
        }
public bool Status
        {
            get; set;
        }

     public    List<TransactionHeaderTEMP> PayOnDeliveryTransaction
        {
            get; set;
        }

        public POS_DeliveryBoy()
        {

        }

        

        public bool AddTransaction(TransactionHeaderTEMP trans)
        {

            string query = "Insert Into POS_CashOnDeliveryEntries ([ShiftNo],[Branch],[TillID],[TransactionNo],[CashierID],[DeliveryBoyID],[TransactionAmount]) ";
            query += " Values (@ShiftNo,@Branch,@TillID,@TransactionNo,@CashierID,@DeliveryBoyID,@TransactionAmount) ";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@ShiftNo", trans.ShiftNo);
            parameters.Add("@Branch", trans.Branch);
            parameters.Add("@TillID", trans.TillID);
            parameters.Add("@TransactionNo", trans.TransactionNo);
            parameters.Add("@CashierID", trans.CashierID);
            parameters.Add("@DeliveryBoyID", DeliveryBoyID);
            parameters.Add("@TransactionAmount", trans.AmountForPay);
            if (DbFunctions.InsertUpdate(query, parameters) >= 1)
            {
                return true;
            }
            return false;

            
        }


         
       
        

    }
}
