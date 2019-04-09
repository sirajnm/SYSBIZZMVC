using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
namespace Sys_Sols_Inventory.Model
{
    class clsDashBoard
    {
        private string userId, accName; private int accId, groupID;

        private DateTime? startDate, endDate;


        public int GroupID
        {
            get { return groupID; }
            set { groupID = value; }
        }
        public DateTime? EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }


        public DateTime? StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }
        public int AccId
        {
            get { return accId; }
            set { accId = value; }
        }

        public string AccName
        {
            get { return accName; }
            set { accName = value; }
        }
        


        public string UserId
        {
            get { return userId; }
            set { userId = value; }
        }
        public DataTable getUserType()
        {
            string Query = "select UserType  from LG_LOGIN where Empid='" + userId + "'";
            return DbFunctions.GetDataTable(Query);
        }
        public DataTable getUser()
        {
            string Query = "select top 1 lg_login.username,lg_emplog.logintime from lg_emplog left outer join lg_login on lg_emplog.emp_id=lg_login.empid order by id desc";
            return DbFunctions.GetDataTable(Query);
        }
        public string getMinimumStock()
        {
            string Query = "select count(*) as Minqty from (SELECT StockValues.CODE,StockValues.DESC_ENG,StockValues.MINQTY,StockValues.REODRQTY,StockValues.MQTY FROM (select INV_ITEM_DIRECTORY.CODE,INV_ITEM_DIRECTORY.DESC_ENG,Isnull(INV_ITEM_DIRECTORY.MINIMUM_QTY,0) as MINQTY,Isnull(INV_ITEM_DIRECTORY.REORDER_QTY,0) aS REODRQTY,(stock.QTY-Isnull(INV_ITEM_DIRECTORY.MINIMUM_QTY,0)) AS MQTY from INV_ITEM_DIRECTORY left outer join (select item_id,sum(qty) AS QTY from  tblstock group by item_id) AS STOCK ON INV_ITEM_DIRECTORY.CODE=STOCK.item_id where INV_ITEM_DIRECTORY.MINIMUM_QTY IS NOT NULL) AS StockValues where MQTY=0)src;";
            return Convert.ToString( DbFunctions.GetAValue(Query));
        }
        public string getBelowStock()
        {
            string Query = "select count(*) as Minqty from (SELECT StockValues.CODE,StockValues.DESC_ENG,StockValues.MINQTY,StockValues.REODRQTY,StockValues.MQTY FROM (select INV_ITEM_DIRECTORY.CODE,INV_ITEM_DIRECTORY.DESC_ENG,Isnull(INV_ITEM_DIRECTORY.MINIMUM_QTY,0) as MINQTY,Isnull(INV_ITEM_DIRECTORY.REORDER_QTY,0) aS REODRQTY,(stock.QTY-Isnull(INV_ITEM_DIRECTORY.MINIMUM_QTY,0)) AS MQTY from INV_ITEM_DIRECTORY left outer join (select item_id,sum(qty) AS QTY from  tblstock group by item_id) AS STOCK ON INV_ITEM_DIRECTORY.CODE=STOCK.item_id where INV_ITEM_DIRECTORY.REORDER_QTY IS NOT NULL) AS StockValues where MQTY<0)src;";
            return Convert.ToString(DbFunctions.GetAValue(Query));
        }
        public string bindChequeDueReceipt()
        {
            string Query = "SELECT COUNT(DOC_ID) FROM REC_RECEIPTVOUCHER_HDR WHERE PAY_CODE = 'CHQ' AND POST_FLAG = 'N' AND CANCEL_FLAG = 'N' AND (CONVERT(VARCHAR,CHQ_DATE,101))<@dt";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@dt", DateTime.Now);
            return Convert.ToString(DbFunctions.GetAValue(Query,parameters));
        }
        public DataTable getSuppliers()
        {
            string Query = "GETSUPPLIERS";
            return DbFunctions.GetDataTableProcedure(Query);
        }
        public string bindChequeDuePayments()
        {
            string Query = "SELECT COUNT(DOC_ID) FROM PAY_PAYMENT_VOUCHER_HDR WHERE PAY_CODE = 'CHQ' AND POST_FLAG = 'N' AND CANCEL_FLAG = 'N' AND (CONVERT(VARCHAR,CHQ_DATE,101))<@dt";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@dt", DateTime.Now);
            return Convert.ToString(DbFunctions.GetAValue(Query, parameters));
        }
        public DataTable cashInHand()
        {
            string Query = "SELECT SUM(DEBIT) - SUM(CREDIT) as [Total Balance] FROM tb_Transactions WHERE (CONVERT(VARCHAR,DATED,101) <= @date2) AND (CONVERT(VARCHAR,DATED,101) >= @date1) AND (ACCID = @ACCID) AND(ACCNAME =@AccName)";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@AccName", accName);
            parameters.Add("@ACCID", accId);
            parameters.Add("@date1", startDate);
            parameters.Add("@date2", endDate);
            return DbFunctions.GetDataTable(Query, parameters);
        }
        public DataTable getPayables()
        {
            string Query = "Getpayables";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@group_id", 64);
            return DbFunctions.GetDataTableProcedure(Query,parameters);
        }
        public DataTable getReceiveble()
        {
            string Query = "GetReceivebles";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@group_id", 63);
            return DbFunctions.GetDataTableProcedure(Query, parameters);
        }
        public double totalStock()
        {
            string Query = "GetCurrentStockValue";
          
            return Convert.ToDouble(DbFunctions.GetAValueProcedure(Query));
        }
        public DataTable amountDue()
        {
            string Query = "SELECT REC_CUSTOMER.LedgerId,ISNULL(CREDIT_PERIOD,0) AS CREDIT_PERIOD FROM REC_CUSTOMER LEFT OUTER JOIN TB_LEDGERS ON TB_LEDGERS.LEDGERID=REC_CUSTOMER.LEDGERID WHERE TB_LEDGERS.UNDER=14";
            return DbFunctions.GetDataTable(Query);
        }
        public DataTable amountDueByCustomer()
        {
            string Query = "select COUNT(*) DueCust, ISNULL( sum(balance),0) totalAmount from (SELECT sum(DEBIT)-SUM(credit) balance,ACCID FROM(SELECT T.*,C.CREDIT_PERIOD ,DATEADD(day, DATEDIFF(day, 0,DATEADD(DAY,C.CREDIT_PERIOD*-1, GETDATE())), 0) DueDate FROM tb_Transactions T INNER JOIN REC_CUSTOMER C ON C.LedgerId=T.ACCID )T1 WHERE DATED<=DueDate group By ACCID) dueByCustomer where balance>0";
            return DbFunctions.GetDataTable(Query);
        }

        public DataTable DetaildueCustomer()
        {
            string Query = "select COUNT(*) DueCust, ISNULL( sum(balance),0) totalAmount from (SELECT sum(DEBIT)-SUM(credit) balance,ACCID FROM(SELECT T.*,C.CREDIT_PERIOD ,DATEADD(day, DATEDIFF(day, 0,DATEADD(DAY,C.CREDIT_PERIOD*-1, GETDATE())), 0) DueDate FROM tb_Transactions T INNER JOIN REC_CUSTOMER C ON C.LedgerId=T.ACCID )T1 WHERE DATED<=DueDate group By ACCID) dueByCustomer where balance>0";
            return DbFunctions.GetDataTable(Query);
        }

        public DataTable amountDueToSuppliers()
        {
            string Query = "select COUNT(*) DueCust,ISNULL( sum(balance),0) totalAmount from (SELECT sum(credit)-SUM(DEBIT) balance,ACCID FROM(SELECT T.*,C.DEBIT_PERIOD_TYPE ,DATEADD(day, DATEDIFF(day, 0,DATEADD(DAY,C.DEBIT_PERIOD_TYPE *-1, GETDATE())), 0) DueDate FROM tb_Transactions T INNER JOIN PAY_SUPPLIER C ON C.LedgerId=T.ACCID )T1 WHERE DATED<=DueDate group By ACCID) dueToSupplier where balance>0";
            return DbFunctions.GetDataTable(Query);
        }
        public DataTable amountDueByCreditors()
        {
            string Query = "SELECT PAY_SUPPLIER.LedgerId,ISNULL(DEBIT_PERIOD_TYPE,0) AS DEBIT_PERIOD FROM PAY_SUPPLIER LEFT OUTER JOIN TB_LEDGERS ON TB_LEDGERS.LEDGERID=PAY_SUPPLIER.LEDGERID WHERE TB_LEDGERS.UNDER=13";
            return DbFunctions.GetDataTable(Query);
        }
    }
}
