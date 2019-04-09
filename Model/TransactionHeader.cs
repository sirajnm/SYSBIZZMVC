using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sys_Sols_Inventory.Model
{
    class TransactionHeader
    {

        private string _ShiftNo, _Branch, _TillID, _TransactionNo, _SupervisorID, _CashierID, _TransactionType;
        private DateTime _AccountingDate, _TransactionDate, _TransactionTime;
        private Single _TotalQty, _AmountForPay, _VATAmount, _CESSAmount, _NetAmount;

        public String ShiftNo
        {
            get { return _ShiftNo; }
            set { _ShiftNo = value; }
        }

        public string Branch
        {
            get { return _Branch; }
            set { _Branch = value; }
        }

        public string TillID
        {
            get { return _TillID; }
            set { _TillID = value; }
        }

        public string TransactionNo
        {
            get { return _TransactionNo; }
            set { _TransactionNo = value; }
        }

        public string SupervisorID
        {
            get { return _SupervisorID; }
            set { _SupervisorID = value; }
        }

        public string CashierID
        {
            get { return _CashierID; }
            set { _CashierID = value; }
        }

        public DateTime AccountingDate
        {
            get { return _AccountingDate; }
            set { _AccountingDate = value; }
        }
        public DateTime TransactionDate
        {
            get { return _TransactionDate; }
            set { _TransactionDate = value; }
        }
        public DateTime TransactionTime
        {
            get { return _TransactionTime; }
            set { _TransactionTime = value; }
        }
        public String TransactionType
        {
            get { return _TransactionType; }
            set { _TransactionType = value; }
        }
        public Single TotalQty
        {
            get { return _TotalQty; }
            set { _TotalQty = value; }
        }

        public Single AmountForPay
        {
            get { return _AmountForPay; }
            set { _AmountForPay = value; }
        }
        public Single VATAmount
        {
            get { return _VATAmount; }
            set { _VATAmount = value; }
        }
        public Single CESSAmount
        {
            get { return _CESSAmount; }
            set { _CESSAmount = value; }
        }

        public Single NetAmount
        {
            get { return _NetAmount; }
            set { _NetAmount = value; }
        }

        public TransactionHeader()
        {

        }

        public TransactionHeader(TransactionHeaderTEMP transtemp)
        {
            TransactionNo = SerialNo();
            Branch = transtemp.Branch;
            TillID = transtemp.TillID;
            ShiftNo = transtemp.ShiftNo;
            SupervisorID = transtemp.SupervisorID;
            CashierID = transtemp.CashierID;
            TransactionDate = transtemp.TransactionDate;
            TransactionTime = DateTime.Now;
            TransactionType = transtemp.TransactionType;
            if (inserttransaction() >= 1)
            {

            }

        }
        private string SerialNo()
        {
            string selectquery = "Select PRIFIX + Right(Replicate('0', Serial_length) +   cast( (case when LastTransactionNo is null then  SERIAL_NO else cast(right(LastTransactionNo,SERIAL_LENGTH) as int) +1  end)  as varchar),SERIAL_LENGTH+1)  TransNo from";
            selectquery += " (Select  ds.PRIFIX, ds.SERIAL_NO, ds.SERIAL_LENGTH, Max(th.TransactionNo) LastTransactionNo";
            selectquery += " from GEN_DOC_SERIAL ds left join POS_Transaction_Header th ";
            selectquery += " on ds.DOC_TYPE = 'POSTRANS' where ds.DOC_TYPE = 'POSTRANS' group by  ds.PRIFIX, ds.SERIAL_NO, ds.SERIAL_LENGTH) t";
            return DbFunctions.GetAValue(selectquery).ToString();
        }
        private int inserttransaction()
        {
            string insertquery = "INSERT INTO [dbo].[POS_Transaction_Header] ([ShiftNo],[Branch],[TillID],[TransactionNo],[SupervisorID],[CashierID],[TransactionDate],[TransactionTime],[TransactionType]) ";
            insertquery += "values (@ShiftNo, @Branch, @TillID, @TransactionNo, @SupervisorID, @CashierID, @TransactionDate, @TransactionTime, @TransactionType )";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@ShiftNo", ShiftNo);
            parameters.Add("@Branch", Branch);
            parameters.Add("@TillID", TillID);
            parameters.Add("@TransactionNo", TransactionNo);
            parameters.Add("@SupervisorID", SupervisorID);
            parameters.Add("@CashierID", CashierID);
            parameters.Add("@TransactionDate", TransactionDate);
            parameters.Add("@TransactionTime", TransactionTime);
            parameters.Add("@TransactionType", TransactionType);
            return DbFunctions.InsertUpdate(insertquery, parameters);
        }



    }
}
