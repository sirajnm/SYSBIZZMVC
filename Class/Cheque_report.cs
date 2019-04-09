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
    class Cheque_report
    {
        public DataTable GetAllDatas(string table)
        {
            DataTable dt = new DataTable();
            try
            {
                string query = @"SELECT       "+table+@".REC_NO, "+table+@".DOC_DATE_GRE,CONVERT(decimal(18,2), "+table+@".AMOUNT) AS AMOUNT, 
                          "+table+@".CHQ_NO, "+table+@".CHQ_DATE, 
                         "+table+@".NOTES, "+table+@".Transaction_Date, tb_Ledgers_1.LEDGERNAME AS DEBITER, tb_Ledgers.LEDGERNAME AS CREDITER, 
                         CASE WHEN "+table+@".POST_FLAG='Y' THEN 'POSTED' WHEN "+table+@".CANCEL_FLAG='Y' THEN 'CANCELED' ELSE 'PENDING' END AS [CHQ STS] 
FROM            "+table+@" INNER JOIN
                         tb_Ledgers AS tb_Ledgers_1 ON "+table+@".DEBIT_CODE = tb_Ledgers_1.LEDGERID INNER JOIN
                         tb_Ledgers ON "+table+@".CREDIT_CODE = tb_Ledgers.LEDGERID
WHERE        ("+table+@".PAY_CODE = 'CHQ')";
                dt = DbFunctions.GetDataTable(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return dt;
        }

        public DataTable ComboData_Party(string table)
        {
            DataTable dt = new DataTable();
            try
            {
               string  query = @"SELECT DISTINCT tb_Ledgers.LEDGERNAME, "+table+@".DEBIT_CODE FROM "+table+@" INNER JOIN tb_Ledgers ON "+table+@".DEBIT_CODE = tb_Ledgers.LEDGERID WHERE("+table+@".PAY_CODE = 'CHQ')";
               dt = DbFunctions.GetDataTable(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return dt;
        }

        public DataTable VoucherDataById(int Rec_No,string table)
        {
            DataTable dt = new DataTable();
            try
            {
                string query = @"SELECT DOC_NO,REC_NO,BRANCH,DEBIT_CODE,CREDIT_CODE,NOTES,DESC1,DESC2,convert(decimal(18,2),AMOUNT) AS AMOUNT FROM " + table + @" WHERE REC_NO=" + Rec_No;
                dt= DbFunctions.GetDataTable(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return dt;
        }

        
        public DataTable ComboData_Bank(string table)
        {
            DataTable dt = new DataTable();
            try
            {
                string query = @"SELECT DISTINCT tb_Ledgers.LEDGERNAME, "+table+@".CREDIT_CODE
FROM            "+table+@" INNER JOIN
                         tb_Ledgers ON "+table+@".CREDIT_CODE = tb_Ledgers.LEDGERID
WHERE        ("+table+@".PAY_CODE = 'CHQ')";
                dt = DbFunctions.GetDataTable(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return dt;
        }

        public DataTable GetAllDatas_Bydate(DateTime date1,DateTime date2,string table)
        {
            DataTable dt = new DataTable();
            try
            {
               string  query = @"SELECT        "+table+@".REC_NO, "+table+@".DOC_DATE_GRE,CONVERT(decimal(18,2), "+table+@".AMOUNT) AS AMOUNT, 
                          "+table+@".CHQ_NO, "+table+@".CHQ_DATE, 
                         "+table+@".NOTES, "+table+@".Transaction_Date, tb_Ledgers_1.LEDGERNAME AS DEBITER, tb_Ledgers.LEDGERNAME AS CREDITER, 
                         CASE WHEN "+table+@".POST_FLAG='Y' THEN 'POSTED' WHEN "+table+@".CANCEL_FLAG='Y' THEN 'CANCELED' ELSE 'PENDING' END AS [CHQ STS] 
FROM            "+table+@" INNER JOIN
                         tb_Ledgers AS tb_Ledgers_1 ON "+table+@".DEBIT_CODE = tb_Ledgers_1.LEDGERID INNER JOIN
                         tb_Ledgers ON "+table+@".CREDIT_CODE = tb_Ledgers.LEDGERID
WHERE        ("+table+@".PAY_CODE = 'CHQ') AND DOC_DATE_GRE BETWEEN @d1 AND @d2";
                //cmd.Parameters.Add("@d1", SqlDbType.Date).Value = date1;
                //cmd.Parameters.Add("@d2", SqlDbType.Date).Value = date2;
               Dictionary<string, object> param = new Dictionary<string, object>();
               param.Add("@d1", date1);
               param.Add("@d2", date2);
               dt = DbFunctions.GetDataTable(query, param);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return dt;


        }

        public DataTable GetAllDatas_bybank(int bank,DateTime date1,DateTime date2,string table)
        {
            DataTable dt = new DataTable();
            try
            {
                string query = @"SELECT        "+table+@".REC_NO, "+table+@".DOC_DATE_GRE,CONVERT(decimal(18,2), "+table+@".AMOUNT) AS AMOUNT, 
                          "+table+@".CHQ_NO, "+table+@".CHQ_DATE, 
                         "+table+@".NOTES, "+table+@".Transaction_Date, tb_Ledgers_1.LEDGERNAME AS DEBITER, tb_Ledgers.LEDGERNAME AS CREDITER, 
                         CASE WHEN "+table+@".POST_FLAG='Y' THEN 'POSTED' WHEN "+table+@".CANCEL_FLAG='Y' THEN 'CANCELED' ELSE 'PENDING' END AS [CHQ STS] 
FROM            "+table+@" INNER JOIN
                         tb_Ledgers AS tb_Ledgers_1 ON "+table+@".DEBIT_CODE = tb_Ledgers_1.LEDGERID INNER JOIN
                         tb_Ledgers ON "+table+@".CREDIT_CODE = tb_Ledgers.LEDGERID
WHERE        ("+table+@".PAY_CODE = 'CHQ') AND "+table+@".CREDIT_CODE='" + bank + "' AND DOC_DATE_GRE BETWEEN @d1 AND @d2";
                
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@d1",date1);
                param.Add("@d2 ", date2);
                dt=DbFunctions.GetDataTable(query,param);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Getting Activation Data \n Error: " + ex.Message);
            }
            return dt;


        }

        public DataTable GetAllDatas_byparty(int party,DateTime date1,DateTime date2,string table)
        {
            //try
            //{
            string query= @"SELECT        "+table+@".REC_NO, "+table+@".DOC_DATE_GRE,CONVERT(decimal(18,2), "+table+@".AMOUNT) AS AMOUNT, 
                          "+table+@".CHQ_NO, "+table+@".CHQ_DATE, 
                         "+table+@".NOTES, "+table+@".Transaction_Date, tb_Ledgers_1.LEDGERNAME AS DEBITER, tb_Ledgers.LEDGERNAME AS CREDITER, 
                         CASE WHEN "+table+@".POST_FLAG='Y' THEN 'POSTED' WHEN "+table+@".CANCEL_FLAG='Y' THEN 'CANCELED' ELSE 'PENDING' END AS [CHQ STS] 
FROM            "+table+@" INNER JOIN
                         tb_Ledgers AS tb_Ledgers_1 ON "+table+@".DEBIT_CODE = tb_Ledgers_1.LEDGERID INNER JOIN
                         tb_Ledgers ON "+table+@".CREDIT_CODE = tb_Ledgers.LEDGERID
WHERE        ("+table+@".DEBIT_CODE = '"+party+"') AND ("+table+@".PAY_CODE ='CHQ') AND ("+table+@".DOC_DATE_GRE  BETWEEN @d1 AND @d2)";
                
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@d1", date1);
            param.Add("@d2 ", date2);
            return DbFunctions.GetDataTable(query,param);


        }

        public DataTable GetAllDatas_byBoth(int bank, int party,DateTime date1,DateTime date2,string table)
        {
            DataTable dt = new DataTable();
            try
            {
                string query = @"SELECT  "+table+@".REC_NO, "+table+@".DOC_DATE_GRE,CONVERT(decimal(18,2), "+table+@".AMOUNT) AS AMOUNT, 
                          "+table+@".CHQ_NO, "+table+@".CHQ_DATE, 
                         "+table+@".NOTES, "+table+@".Transaction_Date, tb_Ledgers_1.LEDGERNAME AS DEBITER, tb_Ledgers.LEDGERNAME AS CREDITER, 
                         CASE WHEN "+table+@".POST_FLAG='Y' THEN 'POSTED' WHEN "+table+@".CANCEL_FLAG='Y' THEN 'CANCELED' ELSE 'PENDING' END AS [CHQ STS] 
FROM            "+table+@" INNER JOIN
                         tb_Ledgers AS tb_Ledgers_1 ON "+table+@".DEBIT_CODE = tb_Ledgers_1.LEDGERID INNER JOIN
                         tb_Ledgers ON "+table+@".CREDIT_CODE = tb_Ledgers.LEDGERID
WHERE        ("+table+@".PAY_CODE = 'CHQ') AND "+table+@".DEBIT_CODE='"+party+"' AND "+table+@".CREDIT_CODE='"+bank+"' AND DOC_DATE_GRE BETWEEN @d1 AND @d2";
               
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@d1", date1);
                param.Add("@d2 ", date2);    
                dt = DbFunctions.GetDataTable(query, param);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Getting Activation Data \n Error: " + ex.Message);
            }
            return dt;

        }


        public bool Update_Status(char post, char cancel, DateTime date, int recno,string table)
        {
            try
            {
                string query = "UPDATE "+table+@" SET POST_FLAG=@POST,CANCEL_FLAG=@CANCEL,Transaction_Date=@TDATE WHERE REC_NO=@RECNO";
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@POST", post);
                param.Add("@CANCEL", cancel);
                param.Add("@TDATE", date);
                param.Add("@RECNO", recno);
                if (DbFunctions.InsertUpdate(query, param) > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }                    
                
            }
            catch
            {
                return false;
            }
        }

        public String VoucherIdById(string Rec_No, string table)
        {            
            try
            {
                string query = @"SELECT DOC_NO FROM " + table + @" WHERE REC_NO=" + Rec_No;
                string doc_id = DbFunctions.GetAValue(query).ToString();
                return doc_id;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Getting Activation Data \n Error: " + ex.Message);
                return "Error";
            }
            
        }

        public bool Delete_Transaction(string vouno)
        {
            try
            {
                string query = "DELETE FROM TB_TRANSACTIONS WHERE VOUCHERNO=@vouno AND VOUCHERTYPE='CHEQUE TRANSACTION'";
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@vouno", vouno);
                if (DbFunctions.InsertUpdate(query,param) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch
            {
                return false;
            }
        }

        public void DeleteVoucher(string Rec_No, string table)
        {
            try
            {
                string query = @"DELETE FROM " + table + " WHERE REC_NO=" + Rec_No;
                DbFunctions.InsertUpdate(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in Data Deletion \n Error: " + ex.Message);                
            }

        }

    }
}
