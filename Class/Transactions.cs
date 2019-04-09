using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows;
using Sys_Sols_Inventory.Model;
namespace Sys_Sols_Inventory.Class
{
    class Transactions
    {
        //private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        //private SqlCommand cmd = new SqlCommand();
        //private SqlDataAdapter adapter = new SqlDataAdapter();

        public string TRANSACTIONID { get; set; }
        public string VOUCHERNO { get; set; }
        public string VOUCHERTYPE { get; set; }
        public string ACCNAME { get; set; }
        public string PARTICULARS { get; set; }
        public string DEBIT { get; set; }
        public string CREDIT { get; set; }
        public string DATED { get; set; }

        public string NARRATION { get; set; }
        public string VOUCHERPREFIX { get; set; }
        public string COSTCENTERID { get; set; }
        public string USERID { get; set; }
        public string COUNTERID { get; set; }
        public string SYSTEMTIME { get; set; }
        public string ACCID { get; set; }
        public string OldName { get; set; }
        public string BRANCH { get; set; }
        public int PROJECTID { get; set; }
        public DateTime CurrentDate { get; set; }



        public void insertTransaction()
        {
            //cmd.Parameters.Clear();
            //cmd.CommandText = "INSERT INTO tb_Transactions(VOUCHERNO, VOUCHERTYPE, ACCNAME, PARTICULARS, DEBIT, CREDIT, DATED, NARRATION, USERID, SYSTEMTIEM, ACCID,BRANCH) VALUES(@VOUCHERNO, @VOUCHERTYPE, @ACCNAME, @PARTICULARS, @DEBIT, @CREDIT, @DATED, @NARRATION, @USERID, @SYSTEMTIEM, @ACCID,@BRANCH)";
            //cmd.Parameters.AddWithValue("@VOUCHERNO", VOUCHERNO);
            //cmd.Parameters.AddWithValue("@VOUCHERTYPE", VOUCHERTYPE);
            //cmd.Parameters.AddWithValue("@ACCNAME", ACCNAME);
            //cmd.Parameters.AddWithValue("@PARTICULARS", PARTICULARS);
            //cmd.Parameters.AddWithValue("@DEBIT",Convert.ToDecimal(DEBIT));
            //cmd.Parameters.AddWithValue("@CREDIT", Convert.ToDecimal(CREDIT));
            //cmd.Parameters.AddWithValue("@DATED", DATED);
            //cmd.Parameters.AddWithValue("@NARRATION", NARRATION);
            //cmd.Parameters.AddWithValue("@USERID", USERID);
            //cmd.Parameters.AddWithValue("@SYSTEMTIEM", SYSTEMTIEM);
            //cmd.Parameters.AddWithValue("@ACCID", ACCID);
            //cmd.Parameters.AddWithValue("@BRANCH", BRANCH);
            //try
            //{
            //    cmd.Connection = conn;
            //    conn.Open();
            //    cmd.ExecuteNonQuery();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
            //finally
            //{
            //    conn.Close();
            //}
            string Query = "INSERT INTO tb_Transactions(VOUCHERNO, VOUCHERTYPE, ACCNAME, PARTICULARS, DEBIT, CREDIT, DATED, NARRATION, USERID, SYSTEMTIEM, ACCID,BRANCH,PROJECTID) VALUES(@VOUCHERNO, @VOUCHERTYPE, @ACCNAME, @PARTICULARS, @DEBIT, @CREDIT, @DATED, @NARRATION, @USERID, @SYSTEMTIEM, @ACCID,@BRANCH,@PROJECT)";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@VOUCHERNO", VOUCHERNO);
            parameters.Add("@VOUCHERTYPE", VOUCHERTYPE);
            parameters.Add("@ACCNAME", ACCNAME);
            parameters.Add("@PARTICULARS", PARTICULARS);
            parameters.Add("@DEBIT", Convert.ToDecimal(DEBIT));
            parameters.Add("@CREDIT", Convert.ToDecimal(CREDIT));
            parameters.Add("@DATED", DATED);
            parameters.Add("@NARRATION", NARRATION);
            parameters.Add("@USERID", USERID);
            parameters.Add("@SYSTEMTIEM", SYSTEMTIME);
            parameters.Add("@ACCID", ACCID);
            parameters.Add("@BRANCH", BRANCH);
            parameters.Add("@PROJECT", PROJECTID);
             DbFunctions.InsertUpdate(Query, parameters);
        }

        public void updateTransaction()
        {
            //cmd.CommandText = "UPDATE tb_Transactions SET VOUCHERNO=@VOUCHERNO, VOUCHERTYPE=@VOUCHERTYPE, ACCNAME=@ACCNAME, PARTICULARS=@PARTICULARS, " +
            //    "DEBIT=@DEBIT, CREDIT=@CREDIT, DATED=@DATED, NARRATION=@NARRATION, USERID=@USERID, SYSTEMTIEM=@SYSTEMTIEM, ACCID=@ACCID,BRANCH=@BRANCH" +
            //    " where TRANSACTIONID=@TRANSACTIONID";
            ////cmd.CommandText = "update tb_Transactions set VOUCHERNO=@VOUCHERNO, VOUCHERTYPE=@VOUCHERTYPE, ACCNAME=@ACCNAME, PARTICULARS=@PARTICULARS, DEBIT=@DEBIT, CREDIT=@CREDIT, DATED=@DATED, NARRATION=@NARRATION,USERID=@USERID, SYSTEMTIEM=@SYSTEMTIEM, ACCID=@ACCID where TRANSACTIONID=@TRANSACTIONID";
            //cmd.Parameters.AddWithValue("@VOUCHERNO", VOUCHERNO);
            //cmd.Parameters.AddWithValue("@VOUCHERTYPE", VOUCHERTYPE);
            //cmd.Parameters.AddWithValue("@ACCNAME", ACCNAME);
            //cmd.Parameters.AddWithValue("@PARTICULARS", PARTICULARS);
            //cmd.Parameters.AddWithValue("@DEBIT", DEBIT);
            //cmd.Parameters.AddWithValue("@CREDIT", CREDIT);
            //cmd.Parameters.AddWithValue("@DATED", DATED);
            //cmd.Parameters.AddWithValue("@NARRATION", NARRATION);
            //cmd.Parameters.AddWithValue("@USERID", USERID);
            //cmd.Parameters.AddWithValue("@SYSTEMTIEM", SYSTEMTIEM);
            //cmd.Parameters.AddWithValue("@ACCID", ACCID);
            //cmd.Parameters.AddWithValue("@BRANCH", BRANCH);
            //cmd.Parameters.AddWithValue("@TRANSACTIONID", TRANSACTIONID);
            //conn.Open();
            //cmd.Connection = conn;
            //cmd.ExecuteNonQuery();
            //cmd.Parameters.Clear();
            //conn.Close();
            string Query =  "UPDATE tb_Transactions SET VOUCHERNO=@VOUCHERNO, VOUCHERTYPE=@VOUCHERTYPE, ACCNAME=@ACCNAME, PARTICULARS=@PARTICULARS, " +
                "DEBIT=@DEBIT, CREDIT=@CREDIT, DATED=@DATED,PROJECTID=@PROJECT, NARRATION=@NARRATION, USERID=@USERID, SYSTEMTIEM=@SYSTEMTIEM, ACCID=@ACCID,BRANCH=@BRANCH" +
                " where TRANSACTIONID=@TRANSACTIONID";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@VOUCHERNO", VOUCHERNO);
            parameters.Add("@VOUCHERTYPE", VOUCHERTYPE);
            parameters.Add("@ACCNAME", ACCNAME);
            parameters.Add("@PARTICULARS", PARTICULARS);
            parameters.Add("@DEBIT", DEBIT);
            parameters.Add("@CREDIT", CREDIT);
            parameters.Add("@DATED", DATED);
            parameters.Add("@NARRATION", NARRATION);
            parameters.Add("@USERID", USERID);
            parameters.Add("@SYSTEMTIEM", SYSTEMTIME);
            parameters.Add("@ACCID", ACCID);
            parameters.Add("@BRANCH", BRANCH);
            parameters.Add("@PROJECT", PROJECTID);
            parameters.Add("@TRANSACTIONID", TRANSACTIONID);
            DbFunctions.InsertUpdate(Query, parameters);
        }

        public DataTable LedgerDebitCreditSum2(int ACCID, DateTime Date1, DateTime Date2)
        {
            //DataTable dt = new DataTable();
            //cmd.CommandText = "SELECT        SUM(DEBIT)- SUM(CREDIT)   FROM            tb_Transactions WHERE        (ACCID = @ACCID) AND (DATED BETWEEN DATEADD(d, - 1, @DATE1) AND DATEADD(d, 1, @DATE2))";
            //cmd.CommandType = CommandType.Text;
            //adapter.SelectCommand = cmd;
            //adapter.SelectCommand.Parameters.AddWithValue("@ACCID", ACCID);
            //adapter.SelectCommand.Parameters.AddWithValue("@DATE1", Date1);
            //adapter.SelectCommand.Parameters.AddWithValue("@DATE2", Date2);
            //cmd.Connection = conn;
            //adapter.Fill(dt);
            //cmd.Parameters.Clear();
            //return dt;
            string Query = "SELECT        SUM(DEBIT)- SUM(CREDIT)   FROM            tb_Transactions WHERE        (ACCID = @ACCID) AND (DATED BETWEEN DATEADD(d, - 1, @DATE1) AND DATEADD(d, 1, @DATE2))";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@ACCID", ACCID);
            parameters.Add("@DATE1", Date1);
            parameters.Add("@DATE2", Date2);
            return DbFunctions.GetDataTable(Query, parameters);

        }

        public DataTable LedgerDebitCreditSum1(int ACCID, DateTime Date1, DateTime Date2)
        {
            //DataTable dt = new DataTable();
            //cmd.CommandText = "SELECT        SUM(CREDIT) - SUM(DEBIT)  FROM            tb_Transactions WHERE        (ACCID = @ACCID) AND (DATED BETWEEN DATEADD(d, - 1, @DATE1) AND DATEADD(d, 1, @DATE2))";
            //cmd.CommandType = CommandType.Text;
            //adapter.SelectCommand = cmd;
            //adapter.SelectCommand.Parameters.AddWithValue("@ACCID", ACCID);
            //adapter.SelectCommand.Parameters.AddWithValue("@DATE1", Date1);
            //adapter.SelectCommand.Parameters.AddWithValue("@DATE2", Date2);
            //cmd.Connection = conn;
            //adapter.Fill(dt);
            //cmd.Parameters.Clear();
            //return dt;
            string Query = "SELECT        SUM(CREDIT) - SUM(DEBIT)  FROM            tb_Transactions WHERE        (ACCID = @ACCID) AND (DATED BETWEEN DATEADD(d, - 1, @DATE1) AND DATEADD(d, 1, @DATE2))";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@ACCID", ACCID);
            parameters.Add("@DATE1", Date1);
            parameters.Add("@DATE2", Date2);
            return DbFunctions.GetDataTable(Query, parameters);
        }

        public DataTable SelectTransaction()
        {
            //DataTable dt = new DataTable();
            //cmd.Parameters.Clear();
            //cmd.CommandText = "SELECT TRANSACTIONID, VOUCHERNO, VOUCHERTYPE, ACCNAME, DEBIT, PARTICULARS, CREDIT,"+
            //    " DATED, NARRATION, VOURCHERPREFIX, COSTCENTERID, USERID, COUNTERID, SYSTEMTIEM, ACCID "+
            //    "FROM tb_Transactions WHERE   (ACCID = @ACCID) AND (VOUCHERTYPE = @VOUCHERTYPE)";
            //cmd.Parameters.AddWithValue("@VOUCHERTYPE", VOUCHERTYPE);
            //cmd.Parameters.AddWithValue("@ACCID", ACCID);
            //cmd.Connection = conn;
            //adapter.SelectCommand = cmd;
            //adapter.Fill(dt);
            //cmd.Parameters.Clear();
            //return dt;
            string Query = "SELECT TRANSACTIONID, VOUCHERNO, VOUCHERTYPE, ACCNAME, DEBIT, PARTICULARS, CREDIT,"+
                " DATED, NARRATION, VOURCHERPREFIX, COSTCENTERID, USERID, COUNTERID, SYSTEMTIEM, ACCID " +
                "FROM tb_Transactions WHERE   (ACCID = @ACCID) AND (VOUCHERTYPE = @VOUCHERTYPE)";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@VOUCHERTYPE", VOUCHERTYPE);
            parameters.Add("@ACCID", ACCID);
           
            return DbFunctions.GetDataTable(Query, parameters);
        }

        public void DeleteTransaction()
        {
          //  cmd.CommandText = "delete from tb_Transactions where ACCID=@ACCID and VOUCHERTYPE=@VOUCHERTYPE ";//;delete from tb_Transactions where PARTICULARS=@OldName
          //  cmd.Parameters.AddWithValue("@VOUCHERTYPE", VOUCHERTYPE);
          // cmd.Parameters.AddWithValue("@ACCID", ACCID);
          ////  cmd.Parameters.AddWithValue("@OldName", OldName);
          // conn.Open();
          // cmd.Connection = conn;
          // cmd.ExecuteNonQuery();
          // cmd.Parameters.Clear();
          // conn.Close();
            string Query = "delete from tb_Transactions where ACCID=@ACCID and VOUCHERTYPE=@VOUCHERTYPE ";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@VOUCHERTYPE", VOUCHERTYPE);
            parameters.Add("@ACCID", ACCID);            
            DbFunctions.InsertUpdate(Query, parameters);
        }

        public void DeletePurchaseTransaction()
        {
            //cmd.CommandText = "delete from tb_Transactions where VOUCHERNO=@VOUCHERNO and VOUCHERTYPE LIKE '%'+@VOUCHERTYPE+'%'";
            //cmd.Parameters.Clear();
            //cmd.Parameters.AddWithValue("@VOUCHERTYPE", VOUCHERTYPE);
            //cmd.Parameters.AddWithValue("@VOUCHERNO", VOUCHERNO);
            //conn.Open();
            //cmd.Connection = conn;
            //cmd.ExecuteNonQuery();
            //cmd.Parameters.Clear();
            //conn.Close();
            string Query = "delete from tb_Transactions where VOUCHERNO=@VOUCHERNO and VOUCHERTYPE LIKE '%'+@VOUCHERTYPE+'%'";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@VOUCHERTYPE", VOUCHERTYPE);
            parameters.Add("@VOUCHERNO", VOUCHERNO);
            DbFunctions.InsertUpdate(Query, parameters);
        }

        public DataTable GetOpeningBalance()
        {
            //DataTable dt = new DataTable();
            //cmd.CommandText = "SELECT     TRANSACTIONID, VOUCHERTYPE, DATED,  ACCNAME, DEBIT, CREDIT,NARRATION, ACCID FROM         tb_Transactions where VOUCHERTYPE='Opening Balance'";
            //cmd.Connection = conn;
            //adapter.SelectCommand = cmd;
            //adapter.Fill(dt);
            //cmd.Parameters.Clear();
            //return dt;
            string Query = "SELECT     TRANSACTIONID, VOUCHERTYPE, DATED,  ACCNAME, DEBIT, CREDIT,NARRATION, ACCID FROM         tb_Transactions where VOUCHERTYPE='Opening Balance'";
            return DbFunctions.GetDataTable(Query);
        }

        public DataTable GetACCID(string Voucherno, string VourcherType)
        {
            //DataTable dt = new DataTable();
            //cmd.CommandText = "SELECT ACCID FROM tb_Transactions where VOUCHERNO='" + Voucherno + "' and VOUCHERTYPE='" + VourcherType + "'";
            //cmd.Connection = conn;
            //adapter.SelectCommand = cmd;
            //adapter.Fill(dt);
            //cmd.Parameters.Clear();
            //return dt;
            string Query = "SELECT ACCID FROM tb_Transactions where VOUCHERNO='" + Voucherno + "' and VOUCHERTYPE='" + VourcherType + "'";
            return DbFunctions.GetDataTable(Query);
        }

        public DataTable GetTrailBalance(DateTime sdate, DateTime edate, string Under)
        {
            //DataTable dt = new DataTable();
            //cmd.Connection = conn;
            //// cmd.CommandText = "SELECT        ACCNAME, SUM(DEBIT) AS Debit, SUM(CREDIT) AS Credit,ACCID FROM            tb_Transactions WHERE        (DATED BETWEEN @Date1 AND @Date2) GROUP BY ACCNAME, ACCID";
            ////MODIFIED qUERY FOR TRAIL BALANCE WITHIN A LEDGER UNDER
            //cmd.CommandText = "SELECT ACCNAME, SUM(DEBIT) AS Debit, SUM(CREDIT) AS Credit,ACCID,UNDER FROM tb_Transactions inner join tb_Ledgers ON tb_Transactions.ACCID=tb_Ledgers.LEDGERID WHERE( tb_Ledgers.UNDER  LIKE '%' +@under+'%')AND((DATED BETWEEN @Date1 AND @Date2))  GROUP BY ACCNAME, ACCID,UNDER ORDER BY UNDER";
            //adapter.SelectCommand = cmd;
            //adapter.SelectCommand.Parameters.AddWithValue("@Date1", sdate);
            //adapter.SelectCommand.Parameters.AddWithValue("@Date2", edate);
            //adapter.SelectCommand.Parameters.AddWithValue("@under", Under);
            //adapter.Fill(dt);
            //cmd.Parameters.Clear();
            //return dt;

            string Query = "SELECT ACCNAME, SUM(DEBIT) AS Debit, SUM(CREDIT) AS Credit,ACCID,UNDER FROM tb_Transactions inner join tb_Ledgers ON tb_Transactions.ACCID=tb_Ledgers.LEDGERID WHERE( tb_Ledgers.UNDER  LIKE '%' +@under+'%')AND((DATED <= @date2) AND (DATED >= @date1))  GROUP BY ACCNAME, ACCID,UNDER ORDER BY UNDER";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@Date1", sdate);
            parameters.Add("@Date2", edate);
            parameters.Add("@under", Under);
            return DbFunctions.GetDataTable(Query,parameters);
        }

        //opening stock for trading pl
        public DataTable GetOpeningStock(DateTime sdate, DateTime edate)
        {
            //DataTable dt = new DataTable();
            //cmd.CommandText = "Acc_TradingPL_OpeningStock";
            //cmd.CommandType = CommandType.StoredProcedure;
            //adapter.SelectCommand = cmd;
            //adapter.SelectCommand.Parameters.AddWithValue("@Date1", sdate);
            //adapter.SelectCommand.Parameters.AddWithValue("@Date2", edate);
            //cmd.Connection = conn;
            //adapter.Fill(dt);
            //cmd.Parameters.Clear();
            //return dt;
            string Query = "Acc_TradingPL_OpeningStock";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@Date1", sdate);
            parameters.Add("@Date2", edate);
            return DbFunctions.GetDataTableProcedure(Query, parameters);
        }

        //Purchase Return for trading pl
        public DataTable PurchaseReturn(DateTime sdate, DateTime edate)
        {
            //DataTable dt = new DataTable();
            //cmd.CommandText = "Acc_TradingPL_PurchaseReturn";
            //cmd.CommandType = CommandType.StoredProcedure;
            //adapter.SelectCommand = cmd;
            //adapter.SelectCommand.Parameters.AddWithValue("@Date1", sdate);
            //adapter.SelectCommand.Parameters.AddWithValue("@Date2", edate);
            //cmd.Connection = conn;
            //adapter.Fill(dt);
            //cmd.Parameters.Clear();
            //return dt;
            string Query = "Acc_TradingPL_PurchaseReturn";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@Date1", sdate);
            parameters.Add("@Date2", edate);
            return DbFunctions.GetDataTableProcedure(Query, parameters);

        }

        //Purchase for trading pl
        public DataTable Purchase(DateTime sdate, DateTime edate)
        {
            //DataTable dt = new DataTable();
            //cmd.CommandText = "Acc_TradingPL_Purchases";
            //cmd.CommandType = CommandType.StoredProcedure;
            //adapter.SelectCommand = cmd;
            //adapter.SelectCommand.Parameters.AddWithValue("@Date1", sdate);
            //adapter.SelectCommand.Parameters.AddWithValue("@Date2", edate);
            //cmd.Connection = conn;
            //adapter.Fill(dt);
            //cmd.Parameters.Clear();
            //return dt;
            string Query = "Acc_TradingPL_Purchases";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@Date1", sdate);
            parameters.Add("@Date2", edate);
            return DbFunctions.GetDataTableProcedure(Query, parameters);
        }

        //Getting Sales for trading pl
        public DataTable GetSales(DateTime sdate, DateTime edate)
        {
            //DataTable dt = new DataTable();
            //cmd.CommandText = "Acc_Sales";
            //cmd.CommandType = CommandType.StoredProcedure;
            //adapter.SelectCommand = cmd;
            //adapter.SelectCommand.Parameters.AddWithValue("@Date1", sdate);
            //adapter.SelectCommand.Parameters.AddWithValue("@Date2", edate);
            //cmd.Connection = conn;
            //adapter.Fill(dt);
            //cmd.Parameters.Clear();
            //return dt;
            string Query = "Acc_Sales";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@Date1", sdate);
            parameters.Add("@Date2", edate);
            return DbFunctions.GetDataTableProcedure(Query, parameters);

        }

        //getting sales return for trading pl
        public DataTable GetSalesRetunr(DateTime sdate, DateTime edate)
        {
            //DataTable dt = new DataTable();
            //cmd.CommandText = "Acc_SalesReturn";
            //cmd.CommandType = CommandType.StoredProcedure;
            //adapter.SelectCommand = cmd;
            //adapter.SelectCommand.Parameters.AddWithValue("@Date1", sdate);
            //adapter.SelectCommand.Parameters.AddWithValue("@Date2", edate);
            //cmd.Connection = conn;
            //adapter.Fill(dt);
            //cmd.Parameters.Clear();
            //return dt;
            string Query = "Acc_SalesReturn";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@Date1", sdate);
            parameters.Add("@Date2", edate);
            return DbFunctions.GetDataTableProcedure(Query, parameters);
        }

        //getting all ledgers under a Account fro trading pl
        public DataTable GetLedgerLoop(int id, int id1)
        {
            //DataTable dt = new DataTable();
            //cmd.CommandText = "Acc_SelectAccGroupExcept";
            //cmd.CommandType = CommandType.StoredProcedure;
            //adapter.SelectCommand = cmd;
            //adapter.SelectCommand.Parameters.AddWithValue("@id", id);
            //adapter.SelectCommand.Parameters.AddWithValue("@id1", id1);
            //cmd.Connection = conn;
            //adapter.Fill(dt);
            //cmd.Parameters.Clear();
            //return dt;
            string Query = "Acc_SelectAccGroupExcept";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@id", id);
            parameters.Add("@id1", id1);
            return DbFunctions.GetDataTableProcedure(Query, parameters);
        }

        public DataTable GetAccGroupLoop(int Id)
        {
            //DataTable dt = new DataTable();
            //cmd.CommandText = "Acc_GetAccGrpLoop";
            //cmd.CommandType = CommandType.StoredProcedure;
            //adapter.SelectCommand = cmd;
            //adapter.SelectCommand.Parameters.AddWithValue("@Id", Id);
            //cmd.Connection = conn;
            //adapter.Fill(dt);
            //cmd.Parameters.Clear();
            //return dt;
            string Query = "Acc_GetAccGrpLoop";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@Id", Id);
            return DbFunctions.GetDataTableProcedure(Query, parameters);
        }

        //getting Sum of debits on an direct income for trading pl
        public DataTable LedgerDebitCreditSum(int ACCID, DateTime Date1, DateTime Date2)
        {
            //DataTable dt = new DataTable();
            //cmd.CommandText = "SELECT SUM(DEBIT) - SUM(CREDIT) FROM tb_Transactions WHERE (ACCID = @ACCID) AND (DATED BETWEEN DATEADD(d, - 1, @DATE1) AND DATEADD(d, 1, @DATE2))";
            //cmd.CommandType = CommandType.Text;
            //adapter.SelectCommand = cmd;
            //adapter.SelectCommand.Parameters.AddWithValue("@ACCID", ACCID);
            //adapter.SelectCommand.Parameters.AddWithValue("@DATE1", Date1);
            //adapter.SelectCommand.Parameters.AddWithValue("@DATE2", Date2);
            //cmd.Connection = conn;
            //adapter.Fill(dt);
            //cmd.Parameters.Clear();
            //return dt;
            string Query = "SELECT SUM(DEBIT) - SUM(CREDIT) FROM tb_Transactions WHERE (ACCID = @ACCID) AND (DATED BETWEEN DATEADD(d, - 1, @DATE1) AND DATEADD(d, 1, @DATE2))";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@ACCID", ACCID);
            parameters.Add("@DATE1", Date1);
            parameters.Add("@DATE2", Date2);
            return DbFunctions.GetDataTable(Query, parameters);
        }
        //public DataTable LedgerCreditDebitSum(int ACCID, DateTime Date1, DateTime Date2)
        //{
        //    DataTable dt = new DataTable();
        //    cmd.CommandText = "SELECT SUM(CREDIT) - SUM(DEBIT) FROM tb_Transactions WHERE (ACCID = @ACCID) AND (DATED BETWEEN DATEADD(d, - 1, @DATE1) AND DATEADD(d, 1, @DATE2))";
        //    cmd.CommandType = CommandType.Text;
        //    adapter.SelectCommand = cmd;
        //    adapter.SelectCommand.Parameters.AddWithValue("@ACCID", ACCID);
        //    adapter.SelectCommand.Parameters.AddWithValue("@DATE1", Date1);
        //    adapter.SelectCommand.Parameters.AddWithValue("@DATE2", Date2);
        //    cmd.Connection = conn;
        //    adapter.Fill(dt);
        //    cmd.Parameters.Clear();
        //    return dt;
        //}
        public DataTable LedgerCreditDebitSum(int ACCID, DateTime Date1, DateTime Date2)
        {
            //DataTable dt = new DataTable();
            //cmd.CommandText = "SELECT SUM(CREDIT) - SUM(DEBIT) FROM tb_Transactions WHERE (ACCID = @ACCID) AND (DATED BETWEEN DATEADD(d, - 1, @DATE1) AND DATEADD(d, 1, @DATE2))";
            //cmd.CommandType = CommandType.Text;
            //adapter.SelectCommand = cmd;
            //adapter.SelectCommand.Parameters.AddWithValue("@ACCID", ACCID);
            //adapter.SelectCommand.Parameters.AddWithValue("@DATE1", Date1);
            //adapter.SelectCommand.Parameters.AddWithValue("@DATE2", Date2);
            //cmd.Connection = conn;
            //adapter.Fill(dt);
            //cmd.Parameters.Clear();
            //return dt;
            string Query = "SELECT SUM(CREDIT) - SUM(DEBIT) FROM tb_Transactions WHERE (ACCID = @ACCID) AND (DATED BETWEEN DATEADD(d, - 1, @DATE1) AND DATEADD(d, 1, @DATE2))";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@ACCID", ACCID);
            parameters.Add("@DATE1", Date1);
            parameters.Add("@DATE2", Date2);
            return DbFunctions.GetDataTable(Query, parameters);
        }

        public DataTable LedgerDebitCreditSumDetails(int ACCID, DateTime Date1, DateTime Date2)
        {
            //DataTable dt = new DataTable();
            //cmd.CommandText = " SELECT        ACCNAME , DEBIT - CREDIT as [bal]  FROM            tb_Transactions WHERE        (ACCID = @ACCID) AND (DATED BETWEEN DATEADD(d, - 1, @DATE1) AND DATEADD(d, 1, @DATE2)) group by ACCNAME,DEBIT,CREDIT ";
            //cmd.CommandType = CommandType.Text;
            //adapter.SelectCommand = cmd;
            //adapter.SelectCommand.Parameters.AddWithValue("@ACCID", ACCID);
            //adapter.SelectCommand.Parameters.AddWithValue("@DATE1", Date1);
            //adapter.SelectCommand.Parameters.AddWithValue("@DATE2", Date2);
            //cmd.Connection = conn;
            //adapter.Fill(dt);
            //cmd.Parameters.Clear();
            //return dt;
            string Query = " SELECT        ACCNAME , DEBIT - CREDIT as [bal]  FROM            tb_Transactions WHERE        (ACCID = @ACCID) AND (DATED BETWEEN DATEADD(d, - 1, @DATE1) AND DATEADD(d, 1, @DATE2)) group by ACCNAME,DEBIT,CREDIT ";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@ACCID", ACCID);
            parameters.Add("@DATE1", Date1);
            parameters.Add("@DATE2", Date2);
            return DbFunctions.GetDataTable(Query, parameters);
        }

        public DataTable LedgerDebitCreditSumDetailsWithouDate(int ACCID)
        {
            //DataTable dt = new DataTable();
            //cmd.CommandText = " SELECT        ACCNAME , DEBIT - CREDIT as [bal]  FROM            tb_Transactions WHERE        (ACCID = @ACCID) group by ACCNAME,DEBIT,CREDIT ";
            //cmd.CommandType = CommandType.Text;
            //adapter.SelectCommand = cmd;
            //adapter.SelectCommand.Parameters.AddWithValue("@ACCID", ACCID);
            //cmd.Connection = conn;
            //adapter.Fill(dt);
            //cmd.Parameters.Clear();
            //return dt;
            string Query = " SELECT        ACCNAME , DEBIT - CREDIT as [bal]  FROM            tb_Transactions WHERE        (ACCID = @ACCID) group by ACCNAME,DEBIT,CREDIT ";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@ACCID", ACCID);
            return DbFunctions.GetDataTable(Query, parameters);
        }

        public DataTable LedgerDebitCreditSumWithoutDate(int ACCID)
        {
            //DataTable dt = new DataTable();
            //cmd.CommandText = "SELECT        SUM(DEBIT) - SUM(CREDIT)  FROM            tb_Transactions WHERE        (ACCID = @ACCID) ";
            //cmd.CommandType = CommandType.Text;
            //adapter.SelectCommand = cmd;
            //adapter.SelectCommand.Parameters.AddWithValue("@ACCID", ACCID);
            //cmd.Connection = conn;
            //adapter.Fill(dt);
            //cmd.Parameters.Clear();
            //return dt;
            string Query = "SELECT        SUM(DEBIT) - SUM(CREDIT)  FROM            tb_Transactions WHERE        (ACCID = @ACCID) ";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@ACCID", ACCID);
            return DbFunctions.GetDataTable(Query, parameters);
        }

        //Getting Closing Stock
        public DataTable ClosingStock(DateTime Date1, DateTime Date2)
        {
            //DataTable dt = new DataTable();
            //cmd.CommandText = "Acc_ClosingStock";
            //cmd.CommandType = CommandType.StoredProcedure;
            //adapter.SelectCommand = cmd;
            //adapter.SelectCommand.Parameters.AddWithValue("@DATE1", Date1);
            //adapter.SelectCommand.Parameters.AddWithValue("@DATE2", Date2);
            //cmd.Connection = conn;
            //adapter.Fill(dt);
            //cmd.Parameters.Clear();
            //return dt;
            string Query = "Acc_ClosingStock";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@DATE1", Date1);
            parameters.Add("@DATE2", Date2);
            return DbFunctions.GetDataTableProcedure(Query, parameters);
        }
        public double TOTALSTOCK()
        {

            //cmd.CommandText = "GetCurrentStockValue";
            //cmd.Parameters.Clear();
            ////   cmd.Parameters.AddWithValue("@group_id", 63);
            //cmd.CommandType = CommandType.StoredProcedure;
            //double value = 0;
            //conn.Open();
            //value = Convert.ToDouble(cmd.ExecuteScalar());
            //conn.Close();
            //cmd.CommandType = CommandType.Text;
            //return value;
            string Query = "GetCurrentStockValue";
          //  Dictionary<string, object> parameters = new Dictionary<string, object>();
           // parameters.Add("@group_id", 63);
           
            return Convert.ToDouble( DbFunctions.GetAValueProcedure(Query));
        }
        public DataTable GetOpeningBalance(DateTime Date1, DateTime Date2)
        {
            //DataTable dt = new DataTable();
            ////cmd.CommandText = " SELECT        SUM(DEBIT) AS debitsum, SUM(CREDIT) AS creditsum FROM            tb_Transactions WHERE        (ACCID = 21) AND (DATED BETWEEN DATEADD(d, - 1, '" + @Date1 + "') AND DATEADD(d, 0, '" + @Date2 + "'))";
            //cmd.CommandText = " SELECT        SUM(DEBIT) - SUM(CREDIT) AS OpeningBalance FROM            tb_Transactions WHERE        (ACCID = 21) AND (DATED BETWEEN DATEADD(d, - 1, '" + @Date1 + "') AND DATEADD(d, 0, '" + @Date2 + "'))";
            //cmd.CommandType = CommandType.Text;
            //adapter.SelectCommand = cmd;
            //adapter.SelectCommand.Parameters.AddWithValue("@DATE1", Date1);
            //adapter.SelectCommand.Parameters.AddWithValue("@DATE2", Date2);
            //cmd.Connection = conn;
            //adapter.Fill(dt);
            //cmd.Parameters.Clear();
            //return dt;
            string Query = " SELECT        SUM(DEBIT) - SUM(CREDIT) AS OpeningBalance FROM            tb_Transactions WHERE        (ACCID = 21) AND (DATED BETWEEN DATEADD(d, - 1, '" + @Date1 + "') AND DATEADD(d, 0, '" + @Date2 + "'))";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@DATE1", Date1);
            parameters.Add("@DATE2", Date2);
            return DbFunctions.GetDataTable(Query, parameters);
        }
        public DataTable getDebitNote(string vType)
        {
            string query = "SELECT VOUCHERNO,ACCNAME,PARTICULARS,CREDIT,DEBIT,DATED,NARRATION FROM tb_Transactions WHERE VOUCHERNO='" + VOUCHERNO + "'and VOUCHERTYPE='" + vType + "'and DATED='" + DATED + "'";
            return DbFunctions.GetDataTable(query);
        }
    }
}