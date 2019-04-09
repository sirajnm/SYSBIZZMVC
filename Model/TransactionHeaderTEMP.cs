using System;
using System.Data;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Sys_Sols_Inventory.Model
{
    class TransactionHeaderTEMP
    {

        private string _ShiftNo, _Branch, _TillID, _TransactionNo, _SupervisorID, _CashierID, _TransactionType, _TransactionStatus;
        private DateTime _AccountingDate, _TransactionDate, _TransactionTime;
        private Single _TotalQty = 0, _AmountForPay = 0, _VATAmount =0, _CESSAmount=0, _NetAmount=0, _PaidAmount=0, _BalancetoPay=0, _DiscountAmount = 0;
        private bool _updated, _IsCustomerAccount = false;

        [Key]
        public String ShiftNo
        {
            get { return _ShiftNo; }
            set { _ShiftNo = value; }
        }
        [Key]
        public string Branch
        {
            get { return _Branch; }
            set { _Branch = value; }
        }
        [Key]
        public string TillID
        {
            get { return _TillID; }
            set { _TillID = value; }
        }
        [Key]
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
            set { _AmountForPay = value;
                BalanceAmount = AmountForPay - PaidAmount;

            }
        }
        public Single PaidAmount
        {
            get { return _PaidAmount; }
            set
            {
                _PaidAmount = value;
                BalanceAmount = AmountForPay - _PaidAmount;

            }
        }
        public Single BalanceAmount
        {
            get { return _BalancetoPay; }
            set { _BalancetoPay = value; }
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
        public Single DiscountAmount
        {
            get { return _DiscountAmount; }
            set {
                _DiscountAmount = value;

                }
        }
        public Single NetAmount
        {
            get { return _NetAmount; }
            set { _NetAmount = value; }
        }

        public String TransactionStatus
        {
            get { return _TransactionStatus; }
            set { _TransactionStatus = value; }
        }



        public bool Updated
        {
            get { return _updated; }
            set
            {

                _updated = value;
            }
        }

        public string CustomerMobileNo
        {
            get; set;
        }

        public string CustomerName
        {
            get; set;
        }

        public string CustomerAddress1
        {
            get; set;
        }

        public string CustomerAddress2
        {
            get; set;
        }

        public bool IsCustomerAccount
        {
            get { return _IsCustomerAccount; }
            set { _IsCustomerAccount = value; }
        }

        public string CustomerAccount
        {
            get; set;
        }

        public List<TransactionDetailTEMP> TransactionDetails
        {
            get; set;
        }

        public List<TransactionPayment> TransactionPayments
        {
            get; set;
        }            

        public TransactionHeaderTEMP()
        {

        }

        public TransactionHeaderTEMP(ShiftMasterDB shift,   string trtype)
        {
            TransactionNo = SerialNoTemp();
            Branch = shift.Branch;
            TillID = shift.TillID;
            ShiftNo = shift.ShiftNo;
            SupervisorID = shift.SupervisorID;
            CashierID = shift.CashierID;
            TransactionDate = DateTime.Now.Date;
            TransactionTime = DateTime.Now;
            TransactionType = trtype;
            TransactionStatus = "Open"; 
            TransactionDetails = new List<TransactionDetailTEMP>();
            TransactionPayments = new List<TransactionPayment>();
           if (inserttransaction() >= 1)
            {
                Updated = true;
            }
           else
            {
                Updated = false;
            }


        }

       public bool Update()
        {
            TotalQty = TransactionDetails.Sum(s => s.Quantity);
            AmountForPay = TransactionDetails.Sum(s => s.AmountForPay);
            DiscountAmount = TransactionDetails.Sum(s => s.DiscountAmount);
            VATAmount = TransactionDetails.Sum(s => s.VATAmount);
            CESSAmount = TransactionDetails.Sum(s => s.CESSAmount);
            NetAmount = TransactionDetails.Sum(s => s.NetAmount);
            string transheadupdatequery = "Update [dbo].[POS_Transaction_Header] set  AmountForPay = @amountforpay, TotalQty = @totqty, DiscountAmount=@DiscountAmount, VATAmount= @vatamount, CESSAmount=@cessamount, NETAmount=@netamount, TransactionStatus=@TransactionStatus, IsCustomerAccount = @IsCustomerAccount where [TransactionNo] = @transnotemp";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@transnotemp", TransactionNo);
            parameters.Add("@transnonew", SerialNo());
            parameters.Add("@amountforpay", AmountForPay);
            parameters.Add("@totqty", TotalQty);
            
            parameters.Add("@DiscountAmount", DiscountAmount);
            parameters.Add("@vatamount", VATAmount);
            parameters.Add("@cessamount", CESSAmount);
            parameters.Add("@netamount", NetAmount);
            parameters.Add("@TransactionStatus", TransactionStatus);
            parameters.Add("@IsCustomerAccount", IsCustomerAccount);
            if (DbFunctions.InsertUpdate(transheadupdatequery, parameters) >= 1)
                return true;    
            return false;
        }
        
        public bool UpdateCustomerInfo(POS_Customer customer)
        {
            CustomerMobileNo = customer.CustomerMobileNo;
            CustomerName = customer.CustomerName;
            CustomerAddress1 = customer.CustomerAddress1;
            CustomerAddress2 = customer.CustomerAddress2;
            IsCustomerAccount = customer.IsCreditCustomer;
            CustomerAccount = customer.CustomerLedger;


            string query = "Update [dbo].[POS_Transaction_Header] set CustomerMobileNo = @CustomerMobileNo, CustomerName = @CustomerName, CustomerAddress1 = @CustomerAddress1, CustomerAddress2 = @CustomerAddress2, IsCustomerAccount = @IsCustomerAccount, CustomerAccount=@CustomerAccount  ";
            query = query + " where TransactionNo = @TransactionNo and Branch = @Branch and TillID = @TillID ";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@TransactionNo", TransactionNo);
            parameters.Add("@Branch", Branch);
            parameters.Add("@TillID", TillID);
            parameters.Add("@CustomerMobileNo", customer.CustomerMobileNo);
            parameters.Add("@CustomerName", customer.CustomerName);
            parameters.Add("@CustomerAddress1", customer.CustomerAddress1);
            parameters.Add("@CustomerAddress2", customer.CustomerAddress2);
            parameters.Add("@IsCustomerAccount", IsCustomerAccount?1:0  );
            parameters.Add("@CustomerAccount", CustomerAccount);
            
            if (DbFunctions.InsertUpdate(query,parameters) >= 1)
            {
                return true;
            }

            return false;
        }

        public bool VoidPayments()
        {
            if (TransactionPayments == null)
            {
                
            }
            foreach (TransactionPayment payment in TransactionPayments)
            {
                if (payment.VoidPayment() < 1) return false;
            }

            PaidAmount = 0;
            Update();
            return true;    
        }

        public int Post()
        {
            AmountForPay = TransactionDetails.Sum(s => s.AmountForPay);
            TotalQty = TransactionDetails.Sum(s => s.Quantity);
            DiscountAmount = TransactionDetails.Sum(s => s.DiscountAmount);
            VATAmount = TransactionDetails.Sum(s => s.VATAmount);
            CESSAmount = TransactionDetails.Sum(s => s.CESSAmount);
            NetAmount = TransactionDetails.Sum(s => s.NetAmount);
            PaidAmount = TransactionPayments.Sum(s => s.Amount);
            TransactionStatus = "Closed";

            string transheadupdatequery = "Update [dbo].[POS_Transaction_Header] set [TransactionNo] = @transnonew, AmountForPay = @amountforpay, TotalQty = @totqty, DiscountAmount=@DiscountAmount, VATAmount= @vatamount, CESSAmount=@cessamount, NETAmount=@netamount, TransactionStatus = @TransactionStatus where [TransactionNo] = @transnotemp";
            string transdetailupdatequery = "Update POS_Transaction_Detail set [TransactionNo] = @transnonew where [TransactionNo] = @transnotemp";
            string transpaymentupdatequery = "Update POS_PaymentEntry set [TransactionNo] = @transnonew where [TransactionNo] = @transnotemp";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@transnotemp", TransactionNo);
            string NewTransactionNo = SerialNo();
            parameters.Add("@transnonew", NewTransactionNo);
            parameters.Add("@amountforpay", AmountForPay);
            parameters.Add("@totqty", TotalQty);
            parameters.Add("@DiscountAmount", DiscountAmount);
            parameters.Add("@vatamount", VATAmount);
            parameters.Add("@cessamount", CESSAmount);
            parameters.Add("@netamount", NetAmount);
            parameters.Add("@TransactionStatus", TransactionStatus);
            
            if (DbFunctions.InsertUpdate(transheadupdatequery, parameters) >= 1)
                if (DbFunctions.InsertUpdate(transdetailupdatequery, parameters) >= 1)
                    if (DbFunctions.InsertUpdate(transpaymentupdatequery, parameters) >= 1)
                    {
                        TransactionNo = NewTransactionNo;
                        return 1;
                    }
                     
                        return 0;
                    




        }

        private string SerialNoTemp()
        {
            string selectquery = "Select PRIFIX + Right(Replicate('0', Serial_length) +   cast( (case when LastTransactionNo is null then  SERIAL_NO else cast(right(LastTransactionNo,SERIAL_LENGTH) as int) +1  end)  as varchar),SERIAL_LENGTH+1)  TransNo from";
            selectquery += " (Select  ds.PRIFIX, ds.SERIAL_NO, ds.SERIAL_LENGTH, Max(th.TransactionNo) LastTransactionNo";
            selectquery += " from GEN_DOC_SERIAL ds left join POS_Transaction_Header th ";
            selectquery += " on ds.DOC_TYPE = 'POSTRANSTEMP' and th.TransactionNo like ds.PRIFIX + '%'  where ds.DOC_TYPE = 'POSTRANSTEMP' group by  ds.PRIFIX, ds.SERIAL_NO, ds.SERIAL_LENGTH) t";
            return DbFunctions.GetAValue(selectquery).ToString();
        }



        private string SerialNo()
        {
            string selectquery = "Select PRIFIX + Right(Replicate('0', Serial_length) +   cast( (case when LastTransactionNo is null then  SERIAL_NO else cast(right(LastTransactionNo,SERIAL_LENGTH) as int) +1  end)  as varchar),SERIAL_LENGTH+1)  TransNo from";
            selectquery += " (Select  ds.PRIFIX, ds.SERIAL_NO, ds.SERIAL_LENGTH, Max(th.TransactionNo) LastTransactionNo";
            selectquery += " from GEN_DOC_SERIAL ds left join POS_Transaction_Header th ";
            selectquery += " on ds.DOC_TYPE = 'POSTRANS' and th.TransactionNo like ds.PRIFIX + '%'  where ds.DOC_TYPE = 'POSTRANS' group by  ds.PRIFIX, ds.SERIAL_NO, ds.SERIAL_LENGTH) t";
            return DbFunctions.GetAValue(selectquery).ToString();
        }

        private int inserttransaction()
        {
            string insertquery = "INSERT INTO [dbo].[POS_Transaction_Header] ([ShiftNo],[Branch],[TillID],[TransactionNo],[SupervisorID],[CashierID],[TransactionDate],[TransactionTime],[TransactionType], [TransactionStatus], [IsCustomerAccount]) ";
            insertquery += "values (@ShiftNo, @Branch, @TillID, @TransactionNo, @SupervisorID, @CashierID, @TransactionDate, @TransactionTime, @TransactionType, @TransactionStatus, @IsCustomerAccount )";
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
            parameters.Add("@TransactionStatus", TransactionStatus);
            parameters.Add("@IsCustomerAccount", IsCustomerAccount);
            return DbFunctions.InsertUpdate(insertquery, parameters);  
        }


    }
}
