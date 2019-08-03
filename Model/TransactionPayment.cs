using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Sys_Sols_Inventory.Model
{
     class TransactionPayment 
    {
        [Key]
        public string ShiftNo { get; set; }
        [Key]
        public string Branch { get; set; }
        [Key]
        public string TillID { get; set; }
        [Key]
        public string TransactionNo { get; set; }
        public string CashierID { get; set; }
        public int LineNo { get; set; }
        public string PaymentType { get; set; }
        public Single ExchangeRate { get; set; }
        private string _currency;
        public Single CurrencyRate { get; set; }
        public string Currency { get
            {
                return _currency;
            }
            set
            {
                _currency = value;
                string query = "Select Max(RATE) CurrRate from GEN_CURRENCY where CODE = @curcode";
                Dictionary<string, object> parameter = new Dictionary<string, object>();
                parameter.Add("@curcode", value);
                object obj = DbFunctions.GetAValue(query, parameter);
                if (DBNull.Value == obj || obj.ToString() == "")
                {
                    CurrencyRate = 0; 
                }else
                {
                    CurrencyRate = Convert.ToSingle(obj);
                }


            }
        }
        private float _AmountinCurrency;
        public float AmountinCurrency
        {
            get
            { return _AmountinCurrency; }
            set
            {
                _AmountinCurrency = value;
                Amount = _AmountinCurrency * CurrencyRate;
            }
        }
        
        public float Amount { get; set; }



        public TransactionPayment()
        {

        }
        public TransactionPayment(TransactionHeaderTEMP transhead, string paymentmethod, string paymentcurrency, float paymentamount)
        {
            ShiftNo = transhead.ShiftNo;
            Branch = transhead.Branch;
            TillID = transhead.TillID;
            TransactionNo = transhead.TransactionNo;
            CashierID = transhead.CashierID;
            LineNo = NewLine();
            PaymentType = paymentmethod;
            Currency = paymentcurrency;
           
            AmountinCurrency = paymentamount;

            if (InsertTransaction() >= 1)
            {

            }


        }

        public static Single PaidAmount(TransactionHeaderTEMP transtemp)
        {
            string query = "Select sum(Amount) Amount from [POS_PaymentEntry] where Branch = @brch and TillID = @tid and TransactionNo = @trnsno";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@brch", transtemp.Branch);
            parameters.Add("@tid", transtemp.TillID);
            parameters.Add("@trnsno", transtemp.TransactionNo);
            object obj = DbFunctions.GetAValue(query, parameters);
            if (DBNull.Value == obj)
            {
                return 0;
            }
            else
            {
                return Convert.ToSingle(obj);
            }
            
           
        }

        private int NewLine()
        {
            string query = "Select Max([LineNo]) MaxLineNo from [POS_PaymentEntry] where Branch = @brch and TillID = @tid and TransactionNo = @trnsno";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@brch", Branch);
            parameters.Add("@tid", TillID);
            parameters.Add("@trnsno", TransactionNo);
            object obj = DbFunctions.GetAValue(query, parameters);
            if (DBNull.Value == obj || obj.ToString() == "")
            {
                return 10;
            }
            else
            {
                return (int)obj + 10;
            }

        }

        private int InsertTransaction()
        {
            string query = "Insert Into POS_PaymentEntry (ShiftNo, Branch, TillID, TransactionNo, CashierID, [LineNo], PaymentType, Currency, AmountinCurrency, CurrencyRate, Amount)";
            query += " values ( @ShiftNo, @Branch, @TillID,  @TransactionNo, @CashierID, @LineNo, @PaymentType, @Currency, @AmountinCurrency, @CurrencyRate, @Amount )";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@ShiftNo", ShiftNo);
            parameters.Add("@Branch", Branch);
            parameters.Add("@TillID", TillID);
            parameters.Add("@TransactionNo", TransactionNo);
            parameters.Add("@CashierID", CashierID);
            parameters.Add("@LineNo", LineNo);
            parameters.Add("@PaymentType", PaymentType);
            parameters.Add("@Currency", Currency);
            parameters.Add("@AmountinCurrency", AmountinCurrency);
            parameters.Add("@CurrencyRate", CurrencyRate);
            parameters.Add("@Amount", Amount);
            return DbFunctions.InsertUpdate(query, parameters);

        }


        public  int VoidPayment()
        {
            string deletequery = "Delete POS_PaymentEntry where ShiftNo= @ShiftNo and  Branch = @Branch and TillID = @TillID and TransactionNo = @TransactionNo and [LineNo] = @LineNo";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@ShiftNo", ShiftNo);
            parameters.Add("@Branch", Branch);
            parameters.Add("@TillID", TillID);
            parameters.Add("@TransactionNo",TransactionNo);
            parameters.Add("@LineNo", LineNo);
            return DbFunctions.InsertUpdate(deletequery, parameters);



        }

        

    }
}
