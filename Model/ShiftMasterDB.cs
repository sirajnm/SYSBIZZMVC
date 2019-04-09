using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Globalization;
using Sys_Sols_Inventory.Class;
using Sys_Sols_Inventory.POSDESK;

namespace Sys_Sols_Inventory.Model
{
    class ShiftMasterDB
    {
        private string _shiftno, _branch, _tillid, _supervisorid, _cashierid, _shiftstatus;
        private DateTime _accountingdate, _shiftstartdate, _shiftenddate, _shiftstarttime, _shiftendtime;
        

        public string  ShiftNo
        { get  { return _shiftno; }
          set { _shiftno = value; }
        }

        public string Branch
        {
            get { return _branch;}
            set { _branch = value; }
        }

        public string TillID
        {
            get { return _tillid; }
            set { _tillid = value; }
        }

        public string SupervisorID
        {
            get { return _supervisorid; }
            set { _supervisorid = value; }
        }

        public string CashierID
        {
            get { return _cashierid; }
            set { _cashierid = value; }

        }

        public DateTime AccountingDate
        {
            get { return _accountingdate; }
            set { _accountingdate = value; }
        }
        public DateTime ShiftStartDate
        {
            get { return _shiftstartdate; }
            set { _shiftstartdate = value; }
        }
        public DateTime ShiftEndDate
        {
            get { return _shiftenddate; }
            set { _shiftenddate = value; }
        }
        public DateTime ShiftStartTime
        {
            get { return _shiftstarttime; }
            set { _shiftstarttime = value; }
        }
        public DateTime ShiftEndTime
        {
            get { return _shiftendtime; }
            set { _shiftendtime = value; }
        }
        public string ShiftStatus
        {
            get { return _shiftstatus; }
            set { _shiftstatus = value; }
        }

        public DataTable GetAllShift()
        {
            string query = "Select * from POS_ShiftMaster";
           return DbFunctions.GetDataTable(query);
        }

        public DataTable GetOpenShift()
        {
            string query = "Select * from POS_ShiftMaster where ShiftStatus='Open'";
            return DbFunctions.GetDataTable(query);
        }
        public Boolean GetOpenShift(string cashierid)
        {
            string query = "Select * from POS_ShiftMaster where ShiftStatus='Open' and CashierID = @CashierID";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@CashierID", cashierid);
            DataTable shiftdoc = DbFunctions.GetDataTable(query, parameters);
            if (shiftdoc.Rows.Count == 0)
            {
                return false;
            }
            ProcessDataTable(shiftdoc);
            return true;
        }

        public ShiftMasterDB()
        {

        }

        public ShiftMasterDB(string suid, string tid, string brch, string cshid)
        {
            

                string query = "NewShift";
                DataTable shiftdoc = new DataTable();
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("@Branch", brch);
                parameters.Add("@TillID", tid);
                parameters.Add("@SupervisorID", suid);
                parameters.Add("@CashierID", cshid);

                shiftdoc = DbFunctions.GetDataTableProcedure(query, parameters);
                ProcessDataTable(shiftdoc);
                
         }


        public bool ShiftClose()
        {
            if ( UpdateSalesHeaderDetail() && UpdateAccountsDetail() )
                return true;
            return false;
        }
        private  void ProcessDataTable(DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                ShiftNo = dr["ShiftNo"].ToString();
                Branch = dr["Branch"].ToString();
                SupervisorID = dr["SupervisorID"].ToString();
                CashierID = dr["CashierID"].ToString();
                TillID = dr["TillID"].ToString();
                object stdate = dr["ShiftStartDate"];
                ShiftStartDate = Convert.ToDateTime(stdate);
                object sttime = dr["ShiftStartTime"];
                ShiftStartTime = DateTime.Now;
                ShiftStatus = dr["ShiftStatus"].ToString();
            }

        }

        private void Update()
        {
            string query = "Update POS_ShiftMaster set ShiftEndDate = @ShiftEndDate, ShiftEndTime = @ShiftEndTime, ShiftStatus = @ShiftStatus where ShiftNo = @ShiftNo ";

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@Branch", Branch);
            parameters.Add("@ShiftNo", ShiftNo);
            parameters.Add("@ShiftStatus", ShiftStatus);
            parameters.Add("@ShiftEndDate", ShiftEndDate);
            parameters.Add("@ShiftEndTime", ShiftEndTime);
            DbFunctions.InsertUpdate(query, parameters);


        }

        private bool UpdateSalesHeaderDetail()
        {
            //Updating Sales Header and Sales Detail as Retail Transaction
            string headerquery = "Begin Tran ";
            headerquery += " Insert Into INV_SALES_HDR (BRANCH, DOC_NO, DOC_TYPE, DOC_DATE_GRE,  CURRENCY_CODE, SALE_TYPE,  TAX_TOTAL, VAT, DISCOUNT, TOTAL_AMOUNT,  CESS, POSTED)  ";
            headerquery += " select h.Branch, h.ShiftNo DOC_NO, 'SAL.RET' DOC_TYPE, sh.ShiftStartDate DOC_DATE_GRE, s.StoreCurrency CURRENCY_CODE , 'B2C' SALE_TYPE, sum(VATAmount) TAX_TOTAL, sum(VATAmount) VAT, sum(h.DiscountAmount) DISCOUNT, sum(h.AmountForPay) TOTAL_AMOUNT,  sum(h.CESSAmount) CESS, 'N' POSTED  from POS_Transaction_Header h inner join POS_Setup s";
            headerquery += " on h.Branch = s.LocalStoreNo inner join POS_ShiftMaster sh on h.ShiftNo = sh.ShiftNo   where h.ShiftNo = @ShiftNo group by h.Branch, h.ShiftNo,  sh.ShiftStartDate, s.StoreCurrency";
            headerquery += " Declare @DOC_ID int = SCOPE_IDENTITY()";
            headerquery += " Insert Into INV_SALES_DTL (DOC_ID, DOC_TYPE, DOC_NO, BRANCH, ITEM_CODE, ITEM_DESC_ENG, UOM, UOM_QTY, QUANTITY, PRICE,  DISC_VALUE, ITEM_TAX) ";
            headerquery += " Select @DOC_ID DOC_ID, 'SAL.RET' DOC_TYPE, sh.ShiftNo DOC_NO,  sh.Branch, d.ItemNo ITEM_CODE, d.ItemDescription ITEM_DESC_ENG, UOM,sum(UOMQty) UOM_QTY, sum(Quantity) QUANTITY, d.PriceVAT PRICE, sum(d.DiscountAmount) DISC_VALUE, sum(VATAmount) ITEM_TAX from POS_Transaction_Detail d inner join POS_ShiftMaster sh on d.ShiftNo = sh.ShiftNo ";
            headerquery += " where sh.ShiftNo = @ShiftNo ";
            headerquery += " Group by sh.ShiftNo, sh.Branch, d.ItemNo, d.ItemDescription, UOM, PriceVAT ";
            headerquery += "commit tran";


            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@ShiftNo", ShiftNo);


            if (DbFunctions.InsertUpdate(headerquery, parameters) > 1)
            {
                ShiftEndDate = DateTime.Today;
                ShiftEndTime = DateTime.Now;
                ShiftStatus = "Closed";
                this.Update();
                return true;
            }
            return false;

        }
        private bool UpdateAccountsDetail()
        {
            POS_Setup setup = new POS_Setup();
            List<Class.Transactions> AccountTransactions = new List<Class.Transactions>();
            List<string> Postingtypes = new List<string>(new string[] { "Sales", "VAT", "Discount", "ReceivablesAccount", "ReceivablesOther" });
            Class.AccountGroup account = new Class.AccountGroup();
            TbLedgersDB ledger = new TbLedgersDB();
            foreach(string postingtype in Postingtypes)
            {
                Class.Transactions Trans = new Class.Transactions();
                Trans.BRANCH = Branch;
                Trans.VOUCHERNO = ShiftNo;
                Trans.VOUCHERTYPE = "Sales";
                Trans.DATED = ShiftStartDate.ToLongDateString();
                Trans.SYSTEMTIME = DateTime.Now.ToShortTimeString();
                Trans.PARTICULARS = "Retail Sales" + Branch + " " + ShiftNo;
                Trans.NARRATION = "Retail Sales" + Branch + " " + ShiftNo;
                Trans.USERID = SupervisorID;
                if (postingtype == "Sales")
                {
                    Trans.ACCID = setup.SalesAccount;
                    Trans.ACCNAME = ledger.GetLedgerName(setup.SalesAccount);
                    Trans.DEBIT = "0";
                    Trans.CREDIT = POS_Repositery.GetAllTransactions(ShiftNo).Sum(a => a.NetAmount).ToString();
                    AccountTransactions.Add(Trans);
                }
                if (postingtype == "VAT")
                {
                    Trans.ACCID = setup.VATOutPutAccount;
                    Trans.ACCNAME = ledger.GetLedgerName(setup.VATOutPutAccount);
                    Trans.DEBIT = "0";
                    Trans.CREDIT = POS_Repositery.GetAllTransactions(ShiftNo).Sum(a => a.VATAmount).ToString();
                    AccountTransactions.Add(Trans);
                }
                if (postingtype == "Discount")
                {
                    Trans.ACCID = setup.DiscountAccount;
                    Trans.ACCNAME = ledger.GetLedgerName(setup.DiscountAccount);
                    Trans.DEBIT = "0";
                    Trans.CREDIT = POS_Repositery.GetAllTransactions(ShiftNo).Sum(a => a.DiscountAmount).ToString();
                    AccountTransactions.Add(Trans);
                }

                if (postingtype == "ReceivablesAccount")
                {

                    foreach(TransactionHeaderTEMP transtemp in POS_Repositery.GetAllTransactions(ShiftNo).Where(a => a.IsCustomerAccount))
                    {
                        Class.Transactions AccountTrans = new Class.Transactions();
                        AccountTrans.BRANCH = Branch;
                        AccountTrans.VOUCHERNO = transtemp.TransactionNo;
                        AccountTrans.VOUCHERTYPE = "Invoice";
                        AccountTrans.DATED = ShiftStartDate.ToShortDateString();
                        AccountTrans.SYSTEMTIME = DateTime.Now.ToShortDateString();
                        AccountTrans.USERID = SupervisorID;
                        AccountTrans.PARTICULARS = "Retail Sales " + Branch + " " + ShiftNo;
                        AccountTrans.NARRATION = "Retail Sales " + Branch + " " + ShiftNo; ;
                        AccountTrans.ACCID = transtemp.CustomerAccount;
                        AccountTrans.ACCNAME = ledger.GetLedgerName(transtemp.CustomerAccount);
                        AccountTrans.DEBIT = transtemp.NetAmount.ToString();
                        AccountTrans.CREDIT = "0";
                        AccountTransactions.Add(AccountTrans);

                    }

                }

                if (postingtype == "ReceivablesOther")
                {
                    string query = "Select p.ShiftNo, p.Branch, PaymentType, h.TransactionNo, h.IsCustomerAccount, h.CustomerAccount,  sum(Amount) Amount  from POS_PaymentEntry p  ";
                    query += "inner join POS_Transaction_Header h on p.TransactionNo = h.TransactionNo ";
                    query += "where h.ShiftNo = @ShiftNo and h.Branch=@Branch ";
                    query += "group by p.ShiftNo, p.Branch, PaymentType, h.TransactionNo, h.IsCustomerAccount, h.CustomerAccount";
                    Dictionary<string, object> parameter = new Dictionary<string, object>();
                    parameter.Add("@ShiftNo", ShiftNo);
                    parameter.Add("@Branch", Branch);
                    DataTable dt = DbFunctions.GetDataTable(query, parameter);
                    foreach (DataRow dr in dt.Rows)
                    {
                        
                        if (Convert.ToBoolean(dr["IsCustomerAccount"]) && dr["PaymentType"].ToString() != "Account")
                        {
                            Class.Transactions Receivabledr = new Class.Transactions();
                            Class.Transactions Receivablecr = new Class.Transactions();
                            Receivabledr.BRANCH = Branch;
                            Receivabledr.VOUCHERNO = ShiftNo;
                            Receivabledr.VOUCHERTYPE = "Receipt";
                            Receivabledr.DATED = ShiftStartDate.ToShortDateString();
                            Receivabledr.SYSTEMTIME = DateTime.Now.ToShortTimeString();
                            Receivabledr.PARTICULARS = "Retail Sales" + Branch + " " + ShiftNo;
                            Receivabledr.NARRATION = "Retail Sales" + Branch + " " + ShiftNo;
                            Receivabledr.ACCID = dr["CustomerAccount"].ToString();
                            Receivabledr.ACCNAME = ledger.GetLedgerName(Receivabledr.ACCID);
                            Receivabledr.DEBIT = "0";
                            Receivabledr.CREDIT = dr["Amount"].ToString(); 
                            Receivabledr.USERID = SupervisorID;
                            AccountTransactions.Add(Receivabledr);

                          /*  Receivablecr.BRANCH = Branch;
                            Receivablecr.VOUCHERNO = ShiftNo;
                            Receivablecr.VOUCHERTYPE = "Sales";
                            Receivablecr.DATED = ShiftStartDate.ToShortDateString();
                            Receivablecr.SYSTEMTIME = DateTime.Now.ToShortTimeString();
                            Receivablecr.PARTICULARS = "Retail Sales" + Branch + " " + ShiftNo;
                            Receivablecr.NARRATION = "Retail Sales" + Branch + " " + ShiftNo;
                            Receivablecr.ACCID = POS_Repositery.GetAllPaymentTypes().Where(a => a.PaymentType == dr["PaymentType"].ToString()).FirstOrDefault().GLAccount;
                            Receivablecr.ACCNAME = ledger.GetLedgerName(Receivablecr.ACCID);
                            Receivablecr.DEBIT = dr["Amount"].ToString();
                            Receivablecr.CREDIT = "0";
                            Receivablecr.USERID = SupervisorID;
                            AccountTransactions.Add(Receivablecr);*/


                        }
 
                      /*  if (Convert.ToBoolean(dr["IsCustomerAccount"]) && dr["PaymentType"].ToString() == "Account")
                        {
                            Class.Transactions Receivable = new Class.Transactions();
                            Receivable.BRANCH = Branch;
                            Receivable.VOUCHERNO = ShiftNo;
                            Receivable.VOUCHERTYPE = "Sales";
                            Receivable.DATED = ShiftStartDate.ToShortDateString();
                            Receivable.SYSTEMTIME = DateTime.Now.ToShortTimeString();
                            Receivable.PARTICULARS = "Retail Sales" + Branch + " " + ShiftNo;
                            Receivable.NARRATION = "Retail Sales" + Branch + " " + ShiftNo;
                            Receivable.ACCID = dr["CustomerAccount"].ToString();
                            Receivable.ACCNAME = ledger.GetLedgerName(Receivable.ACCID);
                            Receivable.DEBIT = dr["Amount"].ToString();
                            Receivable.CREDIT = "0";
                            Receivable.USERID = SupervisorID;
                            AccountTransactions.Add(Receivable);

                        }*/
                        if (dr["PaymentType"].ToString() != "Account")
                        {
                            Class.Transactions Receivable = new Class.Transactions();
                            Receivable.BRANCH = Branch;
                            Receivable.VOUCHERNO = ShiftNo;
                            Receivable.VOUCHERTYPE = "Sales";
                            Receivable.DATED = ShiftStartDate.ToShortDateString();
                            Receivable.SYSTEMTIME = DateTime.Now.ToShortTimeString();
                            Receivable.PARTICULARS = "Retail Sales" + Branch + " " + ShiftNo;
                            Receivable.NARRATION = "Retail Sales" + Branch + " " + ShiftNo;
                            Receivable.ACCID = POS_Repositery.GetAllPaymentTypes().Where(a => a.PaymentType == dr["PaymentType"].ToString()).FirstOrDefault().GLAccount;
                            Receivable.ACCNAME = ledger.GetLedgerName(Receivable.ACCID);
                            Receivable.DEBIT = dr["Amount"].ToString();
                            Receivable.CREDIT = "0";
                            Receivable.USERID = SupervisorID;
                            AccountTransactions.Add(Receivable);
                        }

                    }
                }

            }

            //update Receivable Account

            if (AccountTransactions.Sum(a => Convert.ToSingle(a.DEBIT)) == AccountTransactions.Sum(a => Convert.ToSingle(a.CREDIT)))
            {
                foreach (Transactions trs in AccountTransactions)
                {
                    trs.insertTransaction();
                }


                return true;
            }
            return false;




        }

        private bool UpdateInventory()
        {

            InvStkTrxHdrDB invheader = new InvStkTrxHdrDB();
            List<InvStkTrxDtlDB> indetails = new List<InvStkTrxDtlDB>();
            invheader.Branch = Branch;
            invheader.DocDateGre = ShiftStartDate;
            invheader.DocType = "SAL.POS";
            invheader.DocNo = ShiftNo;
           


            return false;
        }
    }
}
