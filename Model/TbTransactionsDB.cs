using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Sys_Sols_Inventory.Model
{
    class TbTransactionsDB
    {
        private int transactionId;
        private string voucherNo, voucherType, accName, particulars, narration, vourcherPrefix, costCenterId, userId, counterId, systemTiem, accId, branch;
        private decimal debit, credit;
        private DateTime dated, dateFrom, dateTo;
        private bool active;
        private  string query;

        public DateTime DateTo
        {
            get { return dateTo; }
            set { dateTo = value; }
        }

        public DateTime DateFrom
        {
            get { return dateFrom; }
            set { dateFrom = value; }
        }
        public string CounterId
        {
            get { return counterId; }
            set { counterId = value; }
        }

        public string UserId
        {
            get { return userId; }
            set { userId = value; }
        }
        

        public bool Active
        {
            get { return active; }
            set { active = value; }
        }

        public DateTime Dated
        {
            get { return dated; }
            set { dated = value; }
        }

        public decimal Credit
        {
            get { return credit; }
            set { credit = value; }
        }

        public decimal Debit
        {
            get { return debit; }
            set { debit = value; }
        }
        
        public string Branch
        {
            get { return branch; }
            set { branch = value; }
        }

        public string AccId
        {
            get { return accId; }
            set { accId = value; }
        }

        public string SystemTiem
        {
            get { return systemTiem; }
            set { systemTiem = value; }
        }

        public string CostCenterId
        {
            get { return costCenterId; }
            set { costCenterId = value; }
        }

        public string VourcherPrefix
        {
            get { return vourcherPrefix; }
            set { vourcherPrefix = value; }
        }

        public string Narration
        {
            get { return narration; }
            set { narration = value; }
        }

        public string Particulars
        {
            get { return particulars; }
            set { particulars = value; }
        }

        public string AccName
        {
            get { return accName; }
            set { accName = value; }
        }

        public string VoucherType
        {
            get { return voucherType; }
            set { voucherType = value; }
        }

        public string VoucherNo
        {
            get { return voucherNo; }
            set { voucherNo = value; }
        }

        public int TransactionId
        {
            get { return transactionId; }
            set { transactionId = value; }
        }


        public double creditAmt()
        {
            query = "SELECT     SUM(DEBIT)-SUM(CREDIT)  AS creditamt FROM         tb_Transactions WHERE     (ACCNAME = '" + accName + "') AND (ACCID = '" + accId + "')";
            return Convert.ToDouble(DbFunctions.GetAValue(query));
        }
        public DataTable getAllDataByCondition()
        {
            query = "select * from tb_Transactions WHERE ((VOUCHERTYPE='Opening Balance')AND(ACCID='" + accId + "'))";
            return DbFunctions.GetDataTable(query);
        }
        public object getTransactionIdCount()
        {
            query = "SELECT ISNULL(COUNT(TRANSACTIONID), 0) FROM tb_Transactions WHERE ACCID = @id";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@id",accId);

            return DbFunctions.GetAValue(query,parameters);
        }
        public System.Data.SqlClient.SqlDataReader getDebitCredit()
        {
            query = "SELECT DEBIT, CREDIT FROM tb_Transactions WHERE ACCID = @id";
            Dictionary<string, object> parameter = new Dictionary<string, object>();
            parameter.Add("@id", accId);
            return DbFunctions.GetDataReader(query,parameter);
        }
        public object isDebitOrCredit(string Type)
        {
            if (Type == "Debit Note")
            {
                query = "SELECT MAX(VOUCHERNO) FROM tb_Transactions WHERE VOUCHERTYPE ='Debit Note'";
            }
            else
            {
                query = "SELECT MAX(VOUCHERNO) FROM tb_Transactions WHERE VOUCHERTYPE ='Credit Note'";
            }
            return DbFunctions.GetAValue(query);
        }
        public DataTable getData()
        {
            query = "   SELECT        DATED, VOUCHERNO, VOUCHERTYPE, PARTICULARS, DEBIT, ACCID, NARRATION, CREDIT FROM            tb_Transactions WHERE        (ACCID = @ALEDGERID) AND (VOUCHERTYPE <> N'Cash Receipt') AND (DATED BETWEEN DATEADD(d, - 1, @DATE1) AND DATEADD(d, 2, @DATE2)) ORDER BY VOUCHERTYPE";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@ALEDGERID",accId);
            parameters.Add("@DATE1", dateFrom);
            parameters.Add("@DATE2", dateTo);
            return DbFunctions.GetDataTable(query, parameters);
        }
        public DataTable getDataByCondition()
        {
            query = "SELECT  DATED, VOUCHERNO, VOUCHERTYPE, PARTICULARS, DEBIT, ACCID, NARRATION, CREDIT FROM            tb_Transactions WHERE        (ACCID = @ALEDGERID) AND (DATED BETWEEN DATEADD(d, - 1, @DATE1) AND DATEADD(d, 2, @DATE2)) ORDER BY VOUCHERTYPE";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@ALEDGERID", accId);
            parameters.Add("@DATE1", dateFrom);
            parameters.Add("@DATE2", dateTo);
            return DbFunctions.GetDataTable(query, parameters);
        }
        public DataTable getDataByDateAndId()
        {
            query = " SELECT        DATED, VOUCHERNO, VOUCHERTYPE, PARTICULARS, DEBIT, ACCID, NARRATION, CREDIT FROM            tb_Transactions WHERE        (ACCID = @ALEDGERID) AND (DATED BETWEEN @DATE1 AND @DATE2) AND (VOUCHERTYPE = 'Cash Receipt' OR VOUCHERTYPE = 'Sales Return') ORDER BY VOUCHERTYPE ";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@ALEDGERID", accId);
            parameters.Add("@DATE1", dateFrom);
            parameters.Add("@DATE2", dateTo);
            return DbFunctions.GetDataTable(query, parameters);
        }
        public DataTable getDetailsUsingDataTable(string ledgerid)
        {
            string query = "SELECT ACCNAME,DEBIT,DATED,CREDIT FROM tb_Transactions WHERE ACCID='" + ledgerid + "' ORDER BY DATED";
            return DbFunctions.GetDataTable(query);
        }
        public DataTable getDateAndDebit(string ledgerId)
        {
            string query = "SELECT DATED,DEBIT FROM tb_Transactions WHERE ACCID='" + ledgerId + "' AND VOUCHERTYPE!='Purchase' ORDER BY DATED";
            return DbFunctions.GetDataTable(query);
        }

        public DataTable getDateAndCredit(string ledgerId)
        {
            string query = "SELECT DATED,CREDIT FROM tb_Transactions WHERE ACCID='" + ledgerId + "' AND VOUCHERTYPE!='SALES Normal' ORDER BY DATED";
            return DbFunctions.GetDataTable(query);
        }
        public DataTable getTransWithoutDate()
        {
            string query = "SELECT tb_Transactions.DATED AS DATE,tb_Transactions.VOUCHERNO,tb_Transactions.ACCNAME AS [ACCOUNT NAME],tb_Transactions.PARTICULARS,tb_Transactions.DEBIT AS AMOUNT,tb_Transactions.NARRATION FROM tb_Transactions LEFT OUTER JOIN (select LEDGERID,ACOUNTID from tb_Ledgers LEFT OUTER JOIN (SELECT * FROM tb_AccountGroup where UNDER=9) AS ACC_GROUP ON tb_Ledgers.UNDER=ACC_GROUP.ACOUNTID WHERE ACOUNTID IS NOT NULL) AS Ledgers ON Ledgers.LEDGERID=tb_Transactions.ACCID WHERE LEDGERID IS NOT NULL AND DEBIT>0";
            return DbFunctions.GetDataTable(query);
        }
        public DataTable getTransWithDate(DateTime from,DateTime to)
        {
            string query = "SELECT tb_Transactions.DATED AS DATE,tb_Transactions.VOUCHERNO,tb_Transactions.ACCNAME AS [ACCOUNT NAME],tb_Transactions.PARTICULARS,tb_Transactions.DEBIT AS AMOUNT,tb_Transactions.NARRATION FROM tb_Transactions LEFT OUTER JOIN (select LEDGERID,ACOUNTID from tb_Ledgers LEFT OUTER JOIN (SELECT * FROM tb_AccountGroup where UNDER=9) AS ACC_GROUP ON tb_Ledgers.UNDER=ACC_GROUP.ACOUNTID WHERE ACOUNTID IS NOT NULL) AS Ledgers ON Ledgers.LEDGERID=tb_Transactions.ACCID WHERE LEDGERID IS NOT NULL AND DEBIT>0 AND DATED BETWEEN @D1 AND @D2";
            Dictionary<string ,object>param=new Dictionary<string,object> ();
            param.Add("@D1",from);
            param.Add("@D2",to);
            return DbFunctions.GetDataTable(query ,param);
        }
        public DataTable getDetails()
        {
            string query = "SELECT tb_Transactions.ACCID,tb_Transactions.ACCNAME, SUM(tb_Transactions.CREDIT) AS CREDIT, SUM(tb_Transactions.DEBIT) AS DEBIT, tb_Ledgers.UNDER, SUM(tb_Transactions.CREDIT)- SUM(tb_Transactions.DEBIT) AS BALANCE FROM            tb_Transactions INNER JOIN tb_Ledgers ON tb_Transactions.ACCID = tb_Ledgers.LEDGERID GROUP BY  tb_Transactions.ACCID,tb_Transactions.ACCNAME, tb_Ledgers.UNDER HAVING (tb_Ledgers.UNDER = '13') and (tb_Transactions.ACCID= '" + accId + "')";
            return DbFunctions.GetDataTable(query);
        }
        public DataTable getdataCus()
        {
            string query = "SELECT REC_CUSTOMER.CODE,REC_CUSTOMER.DESC_ENG,DEBIT.total_DEBIT,CREDIT.total_CREDIT,ISNULL((DEBIT.total_DEBIT-CREDIT.total_CREDIT),0) AS BALANCE  FROM REC_CUSTOMER LEFT OUTER JOIN (SELECT ACCID,ISNULL(SUM(DEBIT),0) as total_DEBIT FROM tb_Transactions WHERE DATED<=@date GROUP BY ACCID) AS DEBIT ON REC_CUSTOMER.LedgerId=DEBIT.ACCID  LEFT OUTER JOIN (SELECT ACCID,ISNULL(SUM(CREDIT),0) as total_CREDIT FROM tb_Transactions GROUP BY ACCID) AS CREDIT ON REC_CUSTOMER.LedgerId=CREDIT.ACCID WHERE REC_CUSTOMER.LedgerId=@id ORDER BY REC_CUSTOMER.CODE";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@date", dated.ToString("yyyy/MM/dd"));
            parameters.Add("@id", accId);
            return DbFunctions.GetDataTable(query, parameters);

        }
        public DataTable getDatabyIdAndDate()
        {
            string query = "SELECT PAY_SUPPLIER.CODE,PAY_SUPPLIER.DESC_ENG,PAY_SUPPLIER.MOBILE,PAY_SUPPLIER.ADDRESS_A,PAY_SUPPLIER.ADDRESS_B,PAY_SUPPLIER.EMAIL,PAY_SUPPLIER.TELE1,PAY_SUPPLIER.TELE2,CREDIT.total_CREDIT,DEBIT.total_DEBIT,(CREDIT.total_CREDIT-DEBIT.total_DEBIT) AS BALANCE  FROM PAY_SUPPLIER LEFT OUTER JOIN (SELECT ACCID,ISNULL(SUM(CREDIT),0) as total_CREDIT FROM tb_Transactions WHERE DATED<=@date GROUP BY ACCID) AS CREDIT ON PAY_SUPPLIER.LedgerId=CREDIT.ACCID  LEFT OUTER JOIN (SELECT ACCID,ISNULL(SUM(DEBIT),0) as total_DEBIT FROM tb_Transactions GROUP BY ACCID) AS DEBIT ON PAY_SUPPLIER.LedgerId=DEBIT.ACCID WHERE PAY_SUPPLIER.LedgerId=@id ORDER BY PAY_SUPPLIER.CODE";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@date", dated.ToString("yyyy/MM/dd"));
            parameters.Add("@id", accId);
            return DbFunctions.GetDataTable(query, parameters);
        }
    }
}