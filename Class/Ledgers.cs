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
    class Ledgers
    {
        private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        private SqlCommand cmd = new SqlCommand();
        private SqlDataAdapter adapter = new SqlDataAdapter();

        public int LEDGERID { get; set; }
        public string LEDGERNAME { get; set; }
        public string UNDER { get; set; }
        public string ISBUILTIN { get; set; }
        public string ADRESS { get; set; }
        public string TIN { get; set; }
        public string CST { get; set; }
        public string PIN { get; set; }
        public string PHONE { get; set; }
        public string MOBILE { get; set; }
        public string EMAIL { get; set; }
        public string CREDITPERIOD { get; set; }
        public string CREDITAMOUNT { get; set; }
        public string DISPLAYNAME { get; set; }
        public string LEDGERTYPE { get; set; }
        public string CODE { get; set; }
        public string BANK{get;set;}
        public string BANKBRANCH{get;set;}
        public string IFCCODE{get;set;}
        public string ACCOUNTNO { get; set; }
        public string TABLE { get; set; }
        public string CUSCODE { get; set; }
        public string AccName { get; set; }
        public DateTime date1 { get; set; }
        public DateTime date2 { get; set; }


        public DataTable Selectledger()
        {
            //DataTable dt = new DataTable();
            //cmd.Connection = conn;
            //cmd.CommandText = "SELECT     tb_Ledgers.LEDGERID, tb_Ledgers.LEDGERNAME, tb_Ledgers.UNDER AS u,  tb_AccountGroup.DESC_ENG AS UNDER, tb_Ledgers.ISBULTIN, tb_Ledgers.CREDITPERIOD, tb_Ledgers.CREDITAMOUNT, tb_Ledgers.DISPLAYNAME, tb_Ledgers.BANK, tb_Ledgers.BANKBRANCH, tb_Ledgers.ACCOUNTNO, tb_Ledgers.IFCCODE FROM  tb_Ledgers left JOIN tb_AccountGroup ON tb_Ledgers.UNDER = tb_AccountGroup.ACOUNTID";
            //adapter.SelectCommand = cmd;
            //adapter.Fill(dt);
            //return dt;
            string Query = "SELECT     tb_Ledgers.LEDGERID, tb_Ledgers.LEDGERNAME, tb_Ledgers.UNDER AS u,  tb_AccountGroup.DESC_ENG AS UNDER, tb_Ledgers.ISBULTIN, tb_Ledgers.CREDITPERIOD, tb_Ledgers.CREDITAMOUNT, tb_Ledgers.DISPLAYNAME, tb_Ledgers.BANK, tb_Ledgers.BANKBRANCH, tb_Ledgers.ACCOUNTNO, tb_Ledgers.IFCCODE FROM  tb_Ledgers left JOIN tb_AccountGroup ON tb_Ledgers.UNDER = tb_AccountGroup.ACOUNTID";
            return DbFunctions.GetDataTable(Query);
        }

        public DataTable Selectledger_GRid()
        {
            //DataTable dt = new DataTable();
            //cmd.Connection = conn;
            //cmd.CommandText = "SELECT     tb_Ledgers.LEDGERID, tb_Ledgers.LEDGERNAME, tb_Ledgers.UNDER AS u,  tb_AccountGroup.DESC_ENG AS UNDER, tb_Ledgers.ISBULTIN, tb_Ledgers.CREDITPERIOD, tb_Ledgers.CREDITAMOUNT, tb_Ledgers.DISPLAYNAME, tb_Ledgers.BANK, tb_Ledgers.BANKBRANCH, tb_Ledgers.ACCOUNTNO, tb_Ledgers.IFCCODE FROM  tb_Ledgers left JOIN tb_AccountGroup ON tb_Ledgers.UNDER = tb_AccountGroup.ACOUNTID";
            //adapter.SelectCommand = cmd;
            //adapter.Fill(dt);
            //return dt;
            string Query = "SELECT     tb_Ledgers.LEDGERID, tb_Ledgers.LEDGERNAME FROM  tb_Ledgers;";
            return DbFunctions.GetDataTable(Query);
        }

        public DataTable SelectledgerByName(string a)
        {
            //DataTable dt = new DataTable();
            //cmd.Connection = conn;
            //cmd.CommandText = "SELECT LEDGERID FROM tb_Ledgers WHERE LEDGERNAME='"+a+"'";
            //adapter.SelectCommand = cmd;
            //adapter.Fill(dt);
            string Query = "SELECT LEDGERID FROM tb_Ledgers WHERE LEDGERNAME='" + a + "'";
            return DbFunctions.GetDataTable(Query);
            //return dt;
        }

        public String insertLedger()
        {
            String id = null;
            //cmd.Connection = conn;
            //cmd.Parameters.Clear();
            //cmd.Parameters.AddWithValue("@LEDGERNAME", LEDGERNAME);
            //cmd.Parameters.AddWithValue("@UNDER", UNDER);
            //cmd.Parameters.AddWithValue("@ADRESS", ADRESS);
            //cmd.Parameters.AddWithValue("@ISBULTIN", ISBUILTIN);
            //cmd.Parameters.AddWithValue("@TIN", TIN);
            //cmd.Parameters.AddWithValue("@CST", CST);
            //cmd.Parameters.AddWithValue("@PIN", PIN);
            //cmd.Parameters.AddWithValue("@PHONE", PHONE);
            //cmd.Parameters.AddWithValue("@MOBILE", MOBILE);
            //cmd.Parameters.AddWithValue("@EMAIL", EMAIL);
            //cmd.Parameters.AddWithValue("@CREDITPERIOD", CREDITPERIOD);
            //cmd.Parameters.AddWithValue("@CREDITAMOUNT", CREDITAMOUNT);
            //cmd.Parameters.AddWithValue("@DISPLAYNAME", DISPLAYNAME);
            //cmd.Parameters.AddWithValue("@BANK", BANK);
            //cmd.Parameters.AddWithValue("@BANKBRANCH", BANKBRANCH);
            //cmd.Parameters.AddWithValue("@IFCCODE", IFCCODE);
            //cmd.Parameters.AddWithValue("@ACCOUNTNO", ACCOUNTNO);
            
            //cmd.CommandText = "insert into  tb_Ledgers(LEDGERNAME, UNDER, ISBULTIN, ADRESS, TIN, CST, PIN, "+
            //    "PHONE, MOBILE, EMAIL, CREDITPERIOD, CREDITAMOUNT, DISPLAYNAME,BANK,BANKBRANCH,IFCCODE,"+
            //    "ACCOUNTNO) "+
            //    "values(@LEDGERNAME, @UNDER, @ISBULTIN, @ADRESS, @TIN, @CST, @PIN, @PHONE, @MOBILE, @EMAIL, "+
            //    "@CREDITPERIOD, @CREDITAMOUNT, @DISPLAYNAME,@BANK,@BANKBRANCH,@IFCCODE,@ACCOUNTNO);"+
            //    "SELECT SCOPE_IDENTITY()";
           
            //try
            //{
            //    conn.Open();
            //    id = Convert.ToString(cmd.ExecuteScalar());
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //finally
            //{
            //    conn.Close();
            //}

            string Query =  "insert into  tb_Ledgers(LEDGERNAME, UNDER, ISBULTIN, ADRESS, TIN, CST, PIN, "+
                "PHONE, MOBILE, EMAIL, CREDITPERIOD, CREDITAMOUNT, DISPLAYNAME,BANK,BANKBRANCH,IFCCODE," +
                "ACCOUNTNO) " +
                "values(@LEDGERNAME, @UNDER, @ISBULTIN, @ADRESS, @TIN, @CST, @PIN, @PHONE, @MOBILE, @EMAIL, " +
                "@CREDITPERIOD, @CREDITAMOUNT, @DISPLAYNAME,@BANK,@BANKBRANCH,@IFCCODE,@ACCOUNTNO);" +
                "SELECT SCOPE_IDENTITY()";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@LEDGERNAME", LEDGERNAME);
            parameters.Add("@UNDER", UNDER);
            parameters.Add("@ADRESS", ADRESS);
            parameters.Add("@ISBULTIN", ISBUILTIN);
            parameters.Add("@TIN", TIN);
            parameters.Add("@CST", CST);
            parameters.Add("@PIN", PIN);
            parameters.Add("@PHONE", PHONE);
            parameters.Add("@MOBILE", MOBILE);
            parameters.Add("@EMAIL", EMAIL);
            parameters.Add("@CREDITPERIOD", CREDITPERIOD);
            parameters.Add("@CREDITAMOUNT", CREDITAMOUNT);
            parameters.Add("@DISPLAYNAME", DISPLAYNAME);
            parameters.Add("@BANK", BANK);
            parameters.Add("@BANKBRANCH", BANKBRANCH);
            parameters.Add("@IFCCODE", IFCCODE);
            parameters.Add("@ACCOUNTNO", ACCOUNTNO);
            try
            {
              
                id = Convert.ToString(DbFunctions.GetAValue(Query,parameters));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return id;
        }

        public void DeleteLedger(string ledgerID)
        {

           // cmd.CommandText = "delete from  tb_Ledgers WHERE LEDGERID='" + ledgerID + "' AND ISBULTIN='N'";
            string Query = "delete from  tb_Ledgers WHERE LEDGERID='" + ledgerID + "' AND ISBULTIN='N'";
            try
            {
                //cmd.Connection = conn;
                //conn.Open();
                //cmd.ExecuteNonQuery();
                //cmd.Parameters.Clear();
             DbFunctions.InsertUpdate(Query);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Deleting Ledger with ID:"+" "+ledgerID+ex.Message);
            }
            finally
            {
              //  conn.Close();

            }

        }
        public void UpdateLedger()
        {

            //cmd.CommandText = "Update  tb_Ledgers set LEDGERNAME=@LEDGERNAME, UNDER=@UNDER,"+
            //    " ISBULTIN=@ISBULTIN, ADRESS=@ADRESS, TIN=@TIN, CST=@CST, PIN=@PIN, PHONE=@PHONE,"+
            //    " MOBILE=@MOBILE, EMAIL=@EMAIL, CREDITPERIOD=@CREDITPERIOD, CREDITAMOUNT=@CREDITAMOUNT,"+
            //    " DISPLAYNAME=@DISPLAYNAME, BANK=@BANK, BANKBRANCH=@BANKBRANCH, IFCCODE=@IFCCODE,"+
            //    "ACCOUNTNO=@ACCOUNTNO where LEDGERID=@LEDGERID";
            //cmd.Parameters.Clear();
            //cmd.Parameters.AddWithValue("@LEDGERNAME", LEDGERNAME);
            //cmd.Parameters.AddWithValue("@UNDER", UNDER);
            //cmd.Parameters.AddWithValue("@ADRESS", ADRESS);
            //cmd.Parameters.AddWithValue("@ISBULTIN", ISBUILTIN);
            //cmd.Parameters.AddWithValue("@TIN", TIN);
            //cmd.Parameters.AddWithValue("@CST", CST);
            //cmd.Parameters.AddWithValue("@PIN", PIN);
            //cmd.Parameters.AddWithValue("@PHONE", PHONE);
            //cmd.Parameters.AddWithValue("@MOBILE", MOBILE); 
            //cmd.Parameters.AddWithValue("@EMAIL", EMAIL);
            //cmd.Parameters.AddWithValue("@CREDITPERIOD", CREDITPERIOD);
            //cmd.Parameters.AddWithValue("@CREDITAMOUNT", CREDITAMOUNT);
            //cmd.Parameters.AddWithValue("@DISPLAYNAME", DISPLAYNAME);
            //cmd.Parameters.AddWithValue("@LEDGERID", LEDGERID);
            //cmd.Parameters.AddWithValue("@BANK", BANK);
            //cmd.Parameters.AddWithValue("@BANKBRANCH", BANKBRANCH);
            //cmd.Parameters.AddWithValue("@IFCCODE", IFCCODE);
            //cmd.Parameters.AddWithValue("@ACCOUNTNO", ACCOUNTNO);
            
            //try
            //{
            //    cmd.Connection = conn;
            //    conn.Open();
            //    cmd.ExecuteNonQuery();
            //    cmd.Parameters.Clear();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Problem Updating Ledger \nMessage:"+ex.Message);
            //}
            //finally
            //{
            //    conn.Close();
            //}

            string Query = "Update  tb_Ledgers set LEDGERNAME=@LEDGERNAME, UNDER=@UNDER,"+
                " ISBULTIN=@ISBULTIN, ADRESS=@ADRESS, TIN=@TIN, CST=@CST, PIN=@PIN, PHONE=@PHONE," +
                " MOBILE=@MOBILE, EMAIL=@EMAIL, CREDITPERIOD=@CREDITPERIOD, CREDITAMOUNT=@CREDITAMOUNT," +
                " DISPLAYNAME=@DISPLAYNAME, BANK=@BANK, BANKBRANCH=@BANKBRANCH, IFCCODE=@IFCCODE," +
                "ACCOUNTNO=@ACCOUNTNO where LEDGERID=@LEDGERID";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@LEDGERNAME", LEDGERNAME);
            parameters.Add("@UNDER", UNDER);
            parameters.Add("@ADRESS", ADRESS);
            parameters.Add("@ISBULTIN", ISBUILTIN);
            parameters.Add("@TIN", TIN);
            parameters.Add("@CST", CST);
            parameters.Add("@PIN", PIN);
            parameters.Add("@PHONE", PHONE);
            parameters.Add("@MOBILE", MOBILE);
            parameters.Add("@EMAIL", EMAIL);
            parameters.Add("@CREDITPERIOD", CREDITPERIOD);
            parameters.Add("@CREDITAMOUNT", CREDITAMOUNT);
            parameters.Add("@DISPLAYNAME", DISPLAYNAME);
            parameters.Add("@BANK", BANK);
            parameters.Add("@LEDGERID", LEDGERID);
            parameters.Add("@BANKBRANCH", BANKBRANCH);
            parameters.Add("@IFCCODE", IFCCODE);
            parameters.Add("@ACCOUNTNO", ACCOUNTNO);
            try
            {
                //cmd.Connection = conn;
                //conn.Open();
                //cmd.ExecuteNonQuery();
                //cmd.Parameters.Clear();
                DbFunctions.InsertUpdate(Query, parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem Updating Ledger \nMessage:" + ex.Message);
            }
            finally
            {
              //  conn.Close();
            }

        }

        public DataTable GetLedgerId()
        {
            //DataTable dt = new DataTable();
            //cmd.Connection = conn;
            //cmd.CommandText = "SELECT LedgerId FROM REC_CUSTOMER where CODE=@CUSCODE";
            //cmd.Parameters.AddWithValue("@CUSCODE", CUSCODE);
            //adapter.SelectCommand = cmd;
            //adapter.Fill(dt);
            //cmd.Parameters.Clear();
            string Query = "SELECT LedgerId FROM REC_CUSTOMER where CODE=@CUSCODE";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@CUSCODE", CUSCODE);
            return DbFunctions.GetDataTable(Query,parameters);
        }

        public DataTable GetLedgerId1()
        {
        //    DataTable dt = new DataTable();
        //    cmd.Connection = conn;
        //    cmd.CommandText = "SELECT LedgerId FROM PAY_SUPPLIER where CODE=@CUSCODE";
        //    cmd.Parameters.AddWithValue("@CUSCODE", CUSCODE);
        //    adapter.SelectCommand = cmd;
        //    adapter.Fill(dt);
        //    cmd.Parameters.Clear();
        //    return dt;
            string Query = "SELECT LedgerId FROM PAY_SUPPLIER where CODE=@CUSCODE";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@CUSCODE", CUSCODE);
            return DbFunctions.GetDataTable(Query, parameters);
        }
        public DataTable GetLedgerIdPurchase()
        {
            //DataTable dt = new DataTable();
            //cmd.CommandText = "SELECT    LedgerId FROM         PAY_SUPPLIER where CODE=@CUSCODE";
            //cmd.Parameters.AddWithValue("@CUSCODE", CUSCODE);

            //cmd.Connection = conn;
            //adapter.SelectCommand = cmd;
            //adapter.Fill(dt);
            //cmd.Parameters.Clear();
            //return dt;
            string Query = "SELECT    LedgerId FROM         PAY_SUPPLIER where CODE=@CUSCODE";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@CUSCODE", CUSCODE);
            return DbFunctions.GetDataTable(Query, parameters);
        }

        public DataTable MaxLedId()
        {
            //DataTable dt = new DataTable();
            //try
            //{
            //    conn.Open();

            //    cmd.CommandText = "SELECT    MAX( LEDGERID) FROM         tb_Ledgers ";
            //    //  cmd.Parameters.AddWithValue("@LEDGERNAME", LEDGERNAME);
            //    cmd.Connection = conn;
            //    adapter.SelectCommand = cmd;
            //    adapter.Fill(dt);

            //}
            //catch (Exception ex)
            //{

            //}
            //finally
            //{
            //    conn.Close();

            //}
            //return dt;
            string Query = "SELECT    MAX( LEDGERID) FROM         tb_Ledgers ";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@LEDGERNAME", LEDGERNAME);
            return DbFunctions.GetDataTable(Query, parameters);
        }
        public int MaxLedGerid()
        {

            //cmd.CommandText = "SELECT ISNULL(MAX(LEDGERID), 0) FROM tb_Ledgers ";
            ////  cmd.Parameters.AddWithValue("@LEDGERNAME", LEDGERNAME);
            //cmd.Connection = conn;
            //conn.Open();
            //int value = Convert.ToInt32(cmd.ExecuteScalar());
            //conn.Close();
            //return value;
            string Query = "SELECT ISNULL(MAX(LEDGERID), 0) FROM tb_Ledgers ";
            //Dictionary<string, object> parameters = new Dictionary<string, object>();
            //parameters.Add("@LEDGERNAME", LEDGERNAME);
            return Convert.ToInt32(DbFunctions.GetAValue(Query));
        }
        public DataTable SelectLedgerNmae()
        {
            //DataTable dt = new DataTable();
            //cmd.CommandText = "SELECT     LEDGERID, LEDGERNAME FROM         tb_Ledgers ";
            ////  cmd.Parameters.AddWithValue("@LEDGERNAME", LEDGERNAME);
            //cmd.Connection = conn;
            //adapter.SelectCommand = cmd;
            //adapter.Fill(dt);
            //return dt;

            string Query = "SELECT     LEDGERID, LEDGERNAME FROM  tb_Ledgers ";
            return DbFunctions.GetDataTable(Query);
        }
        public DataTable SelectDIstictLedgerName()
        {

            string Query = "SELECT DISTINCT LEDGERID,LEDGERNAME FROM tb_Ledgers";
            return DbFunctions.GetDataTable(Query);
        }



        public DataTable SelectLedgerClosingBalance()
        {
            //DataTable dt = new DataTable();
            ////    cmd.CommandText = "SELECT     TRANSACTIONID, VOUCHERNO,DATED, VOUCHERTYPE, ACCNAME, PARTICULARS, DEBIT, CREDIT,  NARRATION FROM         tb_Transactions WHERE     (DATED <= @date2) AND (DATED >= @date1) AND (ACCNAME = @AccName) OR (DATED <= @date2) AND (DATED >= @date1) AND (PARTICULARS = @AccName) ORDER BY TRANSACTIONID ";
            ////  cmd.CommandText = "SELECT TRANSACTIONID, VOUCHERNO, DATED, VOUCHERTYPE, ACCNAME, PARTICULARS, DEBIT, CREDIT, NARRATION, ACCID FROM            tb_Transactions WHERE        (DATED < @date2) AND (DATED >= @date1) AND (ACCID = @ACCID) ORDER BY TRANSACTIONID";
            //cmd.CommandText = "SELECT SUM(DEBIT) - SUM(CREDIT) AS Closing FROM tb_Transactions  WHERE        (DATED < @date2) AND (DATED >= @date1) AND (ACCID = @ACCID)";
            //cmd.Parameters.AddWithValue("@AccName", AccName);
            //cmd.Parameters.AddWithValue("@ACCID", LEDGERID);
            ////cmd.Parameters.AddWithValue("@date1", date1);
            ////cmd.Parameters.AddWithValue("@date2", date2);
            //cmd.Parameters.Add("@date1", SqlDbType.Date).Value = date1.Date;
            //cmd.Parameters.Add("@date2", SqlDbType.Date).Value = date2.Date;
            //cmd.Connection = conn;
            //adapter.SelectCommand = cmd;
            //adapter.Fill(dt);
            //cmd.Parameters.Clear();
            //return dt;


            string Query = "SELECT SUM(DEBIT) - SUM(CREDIT) AS Closing FROM tb_Transactions  WHERE        (DATED < @date2) AND (DATED >= @date1) AND (ACCID = @ACCID)";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@AccName", AccName);
            parameters.Add("@ACCID", LEDGERID);
            parameters.Add("@date1", date1.Date);
            parameters.Add("@date2", date2.Date);
            return DbFunctions.GetDataTable(Query, parameters);
        }


        public DataTable SelectLedgerTransactionsDatewise()
        {
           // DataTable dt = new DataTable();

            //    cmd.CommandText = "SELECT     TRANSACTIONID, VOUCHERNO,DATED, VOUCHERTYPE, ACCNAME, PARTICULARS, DEBIT, CREDIT,  NARRATION FROM         tb_Transactions WHERE     (DATED <= @date2) AND (DATED >= @date1) AND (ACCNAME = @AccName) OR (DATED <= @date2) AND (DATED >= @date1) AND (PARTICULARS = @AccName) ORDER BY TRANSACTIONID ";
            //cmd.CommandText = "SELECT SUM(DEBIT) AS DEBIT, SUM(CREDIT) AS CREDIT, YEAR(DATED)AS YE, datename(MONTH,DATED) AS MONT FROM tb_Transactions WHERE  ACCID =@ACCID  GROUP BY YEAR(DATED), datename(MONTH,DATED),FORMAT(DATED,'MM') order by  YEAR(DATED) asc, FORMAT(DATED,'MM')";
            //// cmd.Parameters.AddWithValue("@AccName", AccName);
            //cmd.Parameters.AddWithValue("@ACCID", LEDGERID);
            //// cmd.Parameters.AddWithValue("@date1", date1);
            //// cmd.Parameters.AddWithValue("@date2", date2);
            //cmd.Connection = conn;
            //adapter.SelectCommand = cmd;
            //adapter.Fill(dt);
            //cmd.Parameters.Clear();
            //return dt;

            //---->>
            //string Query = "SELECT     TRANSACTIONID, VOUCHERNO,DATED, VOUCHERTYPE, ACCNAME, PARTICULARS, DEBIT, CREDIT,  NARRATION,SYSTEMTIEM AS SYSTEM_TIME FROM         tb_Transactions WHERE     (DATED <= @date2) AND (DATED >= @date1) AND (ACCNAME = @AccName) OR (DATED <= @date2) AND (DATED >= @date1) AND (PARTICULARS = @AccName) ORDER BY TRANSACTIONID AND SYSTEM_TIME ";
             string Query = "SELECT SUM(DEBIT) AS DEBIT, SUM(CREDIT) AS CREDIT, YEAR(DATED)AS YE, datename(MONTH,DATED) AS MONT FROM tb_Transactions WHERE  ACCID =@ACCID  GROUP BY YEAR(DATED), datename(MONTH,DATED),FORMAT(DATED,'MM') order by  YEAR(DATED) asc, FORMAT(DATED,'MM')";
             Dictionary<string, object> parameters = new Dictionary<string, object>();
             parameters.Add("@ACCID", LEDGERID);
             return DbFunctions.GetDataTable(Query, parameters);
            //--->>
            

        }

        public DataTable SelectLedgerTransactions()
        {
            DataTable dt = new DataTable();
            //cmd.CommandText = "SELECT TRANSACTIONID, VOUCHERNO, DATED, VOUCHERTYPE, ACCNAME, PARTICULARS, DEBIT, CREDIT, NARRATION, ACCID,SYSTEMTIEM AS SYSTEM_TIME FROM tb_Transactions WHERE (DATED <= @date2) AND (DATED >= @date1) AND (ACCID = @ACCID) AND(ACCNAME =@AccName) ORDER BY DATED,TRANSACTIONID ASC";
            //cmd.CommandText = "SELECT TRANSACTIONID, VOUCHERNO, DATED, VOUCHERTYPE, ACCNAME, PARTICULARS, DEBIT, CREDIT, NARRATION, ACCID,SYSTEMTIEM AS SYSTEM_TIME FROM tb_Transactions WHERE (DATED <= @date2) AND (DATED >= @date1) AND (ACCID = @ACCID) AND(ACCNAME =@AccName) ORDER BY DATED,VOUCHERTYPE,VOUCHERNO ASC";
            //cmd.CommandText = "SELECT TRANSACTIONID, VOUCHERNO, DATED, VOUCHERTYPE, ACCNAME, PARTICULARS, DEBIT, CREDIT, NARRATION, ACCID,SYSTEMTIEM AS SYSTEM_TIME FROM tb_Transactions WHERE (DATED <= @date2) AND (DATED >= @date1) AND (ACCID = @ACCID) ORDER BY DATED,VOUCHERTYPE,VOUCHERNO ASC";
            //cmd.Parameters.AddWithValue("@ACCID", LEDGERID);
            //cmd.Parameters.AddWithValue("@date1", date1);
            //cmd.Parameters.AddWithValue("@date2", date2);
            //cmd.Connection = conn;
            //adapter.SelectCommand = cmd;
            //adapter.Fill(dt);
            //cmd.Parameters.Clear();
            //return dt;

           //  string Query = "SELECT TRANSACTIONID, VOUCHERNO, DATED, VOUCHERTYPE, ACCNAME, PARTICULARS, DEBIT, CREDIT, NARRATION, ACCID,SYSTEMTIEM AS SYSTEM_TIME FROM tb_Transactions WHERE (DATED <= @date2) AND (DATED >= @date1) AND (ACCID = @ACCID) ORDER BY DATED,SYSTEM_TIME,VOUCHERTYPE,VOUCHERNO ASC"; 

            string Query = "SELECT TRANSACTIONID, VOUCHERNO, DATED, VOUCHERTYPE, ACCNAME, PARTICULARS, DEBIT, CREDIT, NARRATION, ACCID,SYSTEMTIEM AS SYSTEM_TIME FROM tb_Transactions WHERE (CONVERT(VARCHAR, tb_Transactions.DATED,101) >=@date1 ) AND (CONVERT(VARCHAR, tb_Transactions.DATED,101) <=@date2 ) AND (ACCID = @ACCID) ORDER BY DATED,SYSTEM_TIME,VOUCHERTYPE,VOUCHERNO ASC"; 
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@ACCID", LEDGERID);
            parameters.Add("@date1", date1);
            parameters.Add("@date2", date2);

            return DbFunctions.GetDataTable(Query, parameters);

            
        }
        public DataTable SelectLedgerTransactionsPTL()
        {
            DataTable dt = new DataTable();

            //string Query = "SELECT tb_Transactions.TRANSACTIONID, tb_Transactions.VOUCHERNO, tb_Transactions.DATED, tb_Transactions.VOUCHERTYPE, tb_Transactions.ACCNAME, tb_Transactions.PARTICULARS, tb_Transactions.DEBIT, tb_Transactions.CREDIT, tb_Transactions.NARRATION, tb_Transactions.ACCID,tb_Transactions.SYSTEMTIEM AS SYSTEM_TIME,INV_SALES_HDR.SHIP_VEHICLE_NO FROM tb_Transactions LEFT OUTER  JOIN INV_SALES_HDR ON tb_Transactions.VOUCHERNO=INV_SALES_HDR.DOC_NO";
            //Query += " WHERE (tb_Transactions.DATED <= @date1) AND (tb_Transactions.DATED >= @date2) AND (tb_Transactions.ACCID = @ACCID) ORDER BY tb_Transactions.DATED,SYSTEM_TIME,tb_Transactions.VOUCHERTYPE,tb_Transactions.VOUCHERNO ASC";
          //  string Query = @"SELECT tb_Transactions.TRANSACTIONID, tb_Transactions.VOUCHERNO, tb_Transactions.DATED, tb_Transactions.VOUCHERTYPE, tb_Transactions.ACCNAME, tb_Transactions.PARTICULARS, tb_Transactions.DEBIT, tb_Transactions.CREDIT, tb_Transactions.NARRATION, tb_Transactions.ACCID,tb_Transactions.SYSTEMTIEM AS SYSTEM_TIME,INV_SALES_HDR.SHIP_VEHICLE_NO,INV_SALES_DTL.QUANTITY,INV_SALES_DTL.PRICE FROM tb_Transactions LEFT OUTER  JOIN INV_SALES_HDR ON tb_Transactions.VOUCHERNO=INV_SALES_HDR.DOC_NO and vouchertype LIKE 'sal%' LEFT OUTER  JOIN INV_SALES_DTL ON INV_SALES_HDR.DOC_ID=INV_SALES_DTL.DOC_ID WHERE (tb_Transactions.DATED >= @date1) AND (tb_Transactions.DATED <= @date2) AND (tb_Transactions.ACCID = @ACCID) ORDER BY tb_Transactions.DATED,SYSTEM_TIME,tb_Transactions.VOUCHERTYPE,tb_Transactions.VOUCHERNO ASC";
            string Query = @"  SELECT tb_Transactions.TRANSACTIONID, tb_Transactions.VOUCHERNO, tb_Transactions.DATED, tb_Transactions.VOUCHERTYPE, tb_Transactions.ACCNAME, tb_Transactions.PARTICULARS, tb_Transactions.DEBIT, tb_Transactions.CREDIT, tb_Transactions.NARRATION, tb_Transactions.ACCID,tb_Transactions.SYSTEMTIEM AS SYSTEM_TIME,INV_SALES_HDR.SHIP_VEHICLE_NO,INV_SALES_DTL.QUANTITY,INV_SALES_DTL.PRICE FROM tb_Transactions LEFT OUTER  JOIN INV_SALES_HDR ON tb_Transactions.VOUCHERNO=INV_SALES_HDR.DOC_NO and vouchertype LIKE 'sal%' LEFT OUTER  JOIN INV_SALES_DTL ON tb_Transactions.VOUCHERNO=INV_SALES_DTL.DOC_NO and vouchertype LIKE 'sal%' WHERE (CONVERT(VARCHAR, tb_Transactions.DATED,101) >=@date1 ) AND (CONVERT(VARCHAR, tb_Transactions.DATED,101) <=@date2 ) AND  (tb_Transactions.ACCID =  @ACCID) ORDER BY tb_Transactions.DATED,SYSTEM_TIME,tb_Transactions.VOUCHERTYPE,tb_Transactions.VOUCHERNO  ASC";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@ACCID", LEDGERID);
            parameters.Add("@date1", date1.Date);
            parameters.Add("@date2", date2.Date);

            return DbFunctions.GetDataTable(Query, parameters);

           
        }
        public DataTable SelectLedgerTransactionsPTLSale()
        {
            DataTable dt = new DataTable();
            string Query = @"  SELECT tb_Transactions.TRANSACTIONID, tb_Transactions.VOUCHERNO, tb_Transactions.DATED, tb_Transactions.VOUCHERTYPE, tb_Transactions.ACCNAME, tb_Transactions.PARTICULARS, tb_Transactions.DEBIT, tb_Transactions.CREDIT, tb_Transactions.NARRATION, tb_Transactions.ACCID,tb_Transactions.SYSTEMTIEM AS SYSTEM_TIME,INV_SALES_HDR.SHIP_VEHICLE_NO,INV_SALES_DTL.QUANTITY,INV_SALES_DTL.PRICE ,INV_SALES_HDR.DOC_ID FROM tb_Transactions LEFT OUTER  JOIN INV_SALES_HDR ON tb_Transactions.VOUCHERNO=INV_SALES_HDR.DOC_NO and vouchertype LIKE 'sal%' LEFT OUTER  JOIN INV_SALES_DTL ON tb_Transactions.VOUCHERNO=INV_SALES_DTL.DOC_NO and vouchertype LIKE 'sal%' WHERE (CONVERT(VARCHAR, tb_Transactions.DATED,101) >=@date1 ) AND (CONVERT(VARCHAR, tb_Transactions.DATED,101) <=@date2 ) AND  (tb_Transactions.ACCID =  @ACCID) ORDER BY INV_SALES_HDR.DOC_ID ASC";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@ACCID", LEDGERID);
            parameters.Add("@date1", date1.Date);
            parameters.Add("@date2", date2.Date);

            return DbFunctions.GetDataTable(Query, parameters);
        }
        public DataTable getDistinctLedg(string type)
        {
            string query="";
            if (type.Equals("payable"))
            {
                query="SELECT DISTINCT LEDGERID,LEDGERNAME FROM tb_Ledgers WHERE UNDER IN (22)";
            }
            else
            {
                query="SELECT DISTINCT LEDGERID,LEDGERNAME FROM tb_Ledgers WHERE UNDER IN (10)";
            }
            return DbFunctions.GetDataTable(query);

        }
        public DataTable getDIstinctBankAccountLedg()
        {
            string query = "SELECT DISTINCT LEDGERID,LEDGERNAME FROM tb_Ledgers WHERE UNDER IN (10,21,22,20,71)";
            return DbFunctions.GetDataTable(query);
        }
        public DataTable getDistinctLedger()
        {
            string query = "SELECT DISTINCT LEDGERID,LEDGERNAME FROM tb_Ledgers WHERE UNDER IN (10,21,22)";
            return DbFunctions.GetDataTable(query);
        }
       public object checkNameExist()
        {
            string query = "SELECT LEDGERID FROM tb_Ledgers WHERE LEDGERNAME=@LEDGERNAME AND LEDGERID != @LEDGERID  ";
        Dictionary<string, object> parameters = new Dictionary<string, object>();
        parameters.Add("@LEDGERNAME", LEDGERNAME);
        parameters.Add("@LEDGERID",LEDGERID);
        return DbFunctions.GetAValue(query,parameters);
        }
       public object checkNameExistWithoutaId()
       {
           string query = "SELECT LEDGERID FROM tb_Ledgers WHERE LEDGERNAME=@LEDGERNAME  ";
           Dictionary<string, object> parameters = new Dictionary<string, object>();
           parameters.Add("@LEDGERNAME", LEDGERNAME);
          // parameters.Add("@LEDGERID", LEDGERID);
           return DbFunctions.GetAValue(query, parameters);
       }
        public DataTable getUnderByLedgerId()
        {
            string query = "SELECT UNDER  FROM    tb_Ledgers WHERE        (LEDGERID = @ALEDGERID)";
            Dictionary<string, object> parameter = new Dictionary<string, object>();
            parameter.Add("@ALEDGERID", LEDGERID);
            return DbFunctions.GetDataTable(query, parameter);
        }

    
    }
}
