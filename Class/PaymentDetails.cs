using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Sys_Sols_Inventory.Model;

namespace Sys_Sols_Inventory.Class
{
    class PaymentDetails
    {

        //private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        //private SqlCommand cmd = new SqlCommand();
        //private SqlDataAdapter adapter = new SqlDataAdapter();

        string query;
        public int Id { get; set; }
        public string Doc_No { get; set; }
        public string AmtRecived { get; set; }
        public string BalanceAmt { get; set; }
        public string CardNo { get; set; }
        public string CardAmt { get; set; }
        public string CreditNoteNo{ get; set; }
    public string CreditNoteAmt{ get; set; }
    public string CheckNo { get; set; }
    public string CheckAmt { get; set; }
    public string CheckBank { get; set; }
    public DateTime CheckDate { get; set; }
    public string CustomerCode { get; set; }
    public string CustomerName { get; set; }
    public string AmountCredited { get; set; }
        public DateTime Date{get;set;}
        public string TotalAmt{get;set;}
        public string TotalAmtRcvd { get; set; }

        public void InsertPaymentDetails()
        {
            try
            {
                query = "INSERT INTO Tbl_Inv_Payment_Dtls (Doc_No, CashAmtRecived, BalanceAmt, CardNo, CardAmt, CreditNoteNo, CreditNoteAmt, CheckNo, CheckAmt, CheckBank, CheckDate, CustomerCode, CustomerName, AmountCredited, Date, TotalAmt,TotalAmtRcvd) VALUES (@Doc_NO,@AmtRecived,@BalanceAmt,@CardNo,@CardAmt,@CreditNoteNo,@CreditNoteAmt,@CheckNo,@CheckAmt,@CheckBank,@CheckDate, @CustomerCode,@CustomerName,@AmountCredited,@Date,@TotalAmt,@TotalAmtRcvd)";
                Dictionary<string, object> Parameter = new Dictionary<string, object>();
                Parameter.Add("@Doc_NO", Doc_No);
                Parameter.Add("@AmtRecived", AmtRecived);
                Parameter.Add("@BalanceAmt", BalanceAmt);
                Parameter.Add("@CardNo", CardNo);
                Parameter.Add("@CardAmt", CardAmt);
                Parameter.Add("@CreditNoteNo", CreditNoteNo);
                Parameter.Add("@CreditNoteAmt", CreditNoteAmt);
                Parameter.Add("@CheckNo", CheckNo);
                Parameter.Add("@CheckAmt", CheckAmt);
                Parameter.Add("@CheckBank", CheckBank);
                Parameter.Add("@CheckDate", CheckDate);
                Parameter.Add("@CustomerCode", CustomerCode);
                Parameter.Add("@CustomerName", CustomerName);
                Parameter.Add("@AmountCredited", AmountCredited);
                Parameter.Add("@Date", Date);
                Parameter.Add("@TotalAmt", TotalAmt);
                Parameter.Add("@TotalAmtRcvd", TotalAmtRcvd);
                try
                {
                    DbFunctions.InsertUpdate(query, Parameter);
                }
                catch (Exception ex)
                {

                }

            }
            catch { }
        }
       

  public  void UpdatePaymentDetails()
    {
        try
        {

            query = "Update Tbl_Inv_Payment_Dtls set CashAmtRecived=@AmtRecived, BalanceAmt=@BalanceAmt, CardNo=, CardAmt=@CardAmt, CreditNoteNo=@CreditNoteNo, CreditNoteAmt=@CreditNoteAmt, CheckNo=@CheckNo, CheckAmt=@CheckAmt, CheckBank=@CheckBank,CheckDate=@CheckDate, CustomerCode=@CustomerCode, CustomerName=@CustomerName, AmountCredited=@AmountCredited, Date=@Date,TotalAmt=@TotalAmt,TotalAmtRcvd=@TotalAmtRcvd where Doc_No=@Doc_No";
            Dictionary<string, object> Parameter = new Dictionary<string, object>();
            Parameter.Add("@Doc_NO", Doc_No);
            Parameter.Add("@AmtRecived", AmtRecived);
            Parameter.Add("@BalanceAmt", BalanceAmt);
            Parameter.Add("@CardNo", CardNo);
            Parameter.Add("@CardAmt", CardAmt);
            Parameter.Add("@CreditNoteNo", CreditNoteNo);
            Parameter.Add("@CreditNoteAmt", CreditNoteAmt);
            Parameter.Add("@CheckNo", CheckNo);
            Parameter.Add("@CheckAmt", CheckAmt);
            Parameter.Add("@CheckBank", CheckBank);
            Parameter.Add("@CheckDate", CheckDate);
            Parameter.Add("@CustomerCode", CustomerCode);
            Parameter.Add("@CustomerName", CustomerName);
            Parameter.Add("@AmountCredited", AmountCredited);
            Parameter.Add("@Date", Date);
            Parameter.Add("@TotalAmt", TotalAmt);
            Parameter.Add("@TotalAmtRcvd", TotalAmtRcvd);
            try
            {
                DbFunctions.InsertUpdate(query, Parameter);
            }
            catch (Exception ex)
            {

            }            
        }
        catch
        {
        }
    }

  public DataTable GetPaymentDetails()
  {
      DataTable dt = new DataTable();
      try
      {          
          query = "select * from Tbl_Inv_Payment_Dtls where Doc_No=@Doc_No";
          Dictionary<string, object> Parameter = new Dictionary<string, object>();
          Parameter.Add("@Doc_No", Doc_No);
          DbFunctions.InsertUpdate(query, Parameter);          
      }
      catch { }
      return dt;
  }
             




    }
  }