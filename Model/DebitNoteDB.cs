using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Sys_Sols_Inventory.Model
{
    class DebitNoteDB
    {
        int id, customer, cashAccount;

        public int CashAccount
        {
            get { return cashAccount; }
            set { cashAccount = value; }
        }

        public int Customer
        {
            get { return customer; }
            set { customer = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        DateTime date;

        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }
        string docNo, dateHIJ, reference, note, remarks, tax;

        public string Tax
        {
            get { return tax; }
            set { tax = value; }
        }

        public string Remarks
        {
            get { return remarks; }
            set { remarks = value; }
        }

        public string Note
        {
            get { return note; }
            set { note = value; }
        }

        public string Reference
        {
            get { return reference; }
            set { reference = value; }
        }

        public string DateHIJ
        {
            get { return dateHIJ; }
            set { dateHIJ = value; }
        }

        public string DocNo
        {
            get { return docNo; }
            set { docNo = value; }
        }
        decimal amount, balance;

        public decimal Balance
        {
            get { return balance; }
            set { balance = value; }
        }

        public decimal Amount
        {
            get { return amount; }
            set { amount = value; }
        }
        bool status, taxInclusive;

        public bool TaxInclusive
        {
            get { return taxInclusive; }
            set { taxInclusive = value; }
        }

        public bool Status
        {
            get { return status; }
            set { status = value; }
        }


        public int Insert_DebitNote()
        {
            string query = "INSERT INTO Tbl_DebitNote(DocNo,Date ,Date_hij,ReferenceNo, Customer,Note,Amount,Balance,Status,Remark,Tax,TaxInclusive,CashAccount) Output Inserted.ID VALUES('" + docNo + "','" + date + "','" + dateHIJ + "','" + reference + "','" + customer + "','" + note + "','" + amount + "','" + balance + "','" + status + "','" + remarks + "','" + tax + "','" + taxInclusive + "','" + cashAccount + "' )";
            return DbFunctions.InsertUpdate_Scalar(query);
        }

        public int Update_DebitNote()
        {
            string query = "update Tbl_DebitNote set Date=@Date,Date_hij=@Date_hij,ReferenceNo=@ReferenceNo,Customer=@Customer,Note=@Note,Amount=@Amount,Status=@Status,Remark=@Remark,Tax=@Tax,TaxInclusive=@TaxInclusive,CashAccount=@CashAccount WHERE DocNo='" + docNo + "'";
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@Date", date);
            Parameters.Add("@Date_hij", dateHIJ);
            Parameters.Add("@ReferenceNo", reference);
            Parameters.Add("@Customer", customer);
            Parameters.Add("@Note", note);
            Parameters.Add("@Amount", amount);
            Parameters.Add("@Status", status);
            Parameters.Add("@Remark", remarks);
            Parameters.Add("@Tax", tax);
            Parameters.Add("@TaxInclusive", taxInclusive);
            Parameters.Add("@CashAccount", cashAccount);
            return DbFunctions.InsertUpdate(query, Parameters);
        }

        public int maxId()
        {
            string query = "Select MAX(Id) From Tbl_DebitNote";
            try
            {
                return (Convert.ToInt32(DbFunctions.GetAValue(query)) + 1);
            }
            catch (Exception)
            {
                return 1;
            }
        }

        public int DocidByDocNo()
        {
            string query = "Select Id From Tbl_DebitNote Where DocNo='" + docNo+"'";
            try
            {
                return (Convert.ToInt32(DbFunctions.GetAValue(query)));
            }
            catch (Exception)
            {
                return 1;
            }
        }

        public bool Existing_DebitNote()
        {
            string query = "Select * From Tbl_DebitNote Where DocNo='" + docNo + "'";
            DataTable dt=DbFunctions.GetDataTable(query);
            if(dt.Rows.Count>0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public string CCodeByLedger()
        {
            string query = "Select CODE From REC_CUSTOMER Where LedgerId=" + customer;            
            return (Convert.ToString(DbFunctions.GetAValue(query)));            
        }

        public int DeleteDebitNote()
        {
            string query = "Delete From Tbl_DebitNote Where DocNo='" + docNo + "'";
            try
            {
                return DbFunctions.InsertUpdate(query);
            }
            catch (Exception)
            {
                return 0;
            }
        }

      /*  public DataTable InvoiceDtls()
        {
            string query = "Select DOC_DATE_GRE,DOC_ID,DOC_NO,CUSTOMER_CODE,CUSTOMER_NAME_ENG,TOTAL_AMOUNT,TAX_TOTAL,NET_AMOUNT From REC_CUSTOMER Where LedgerId=" + customer;
            return (DbFunctions.GetDataTable(query));  
        }*/

    }
}
