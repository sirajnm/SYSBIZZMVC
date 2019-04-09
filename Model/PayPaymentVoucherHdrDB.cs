using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Sys_Sols_Inventory.Model
{
    class PayPaymentVoucherHdrDB
    {
       private int docId, modiTimes, printedTimes, ledgeId;
       private  string branch, docNo, docDateHij, supCode, empCode, rDocNo, payCode, chqNo, bankCode, accDetails;
       private  string debitCode, desc1, creditCode, desc2, curCode, notes, userCode, postFlag, distType, cancelFlag, debitCode2;
       private  decimal recNo, amount, rates, debitCode2Amt, totalPaid, totalCurrent, totalBalance;
       private  DateTime docDateGre, chqDate, transactionDate;

       private string formType;


       public int LedgeId
       {
           get { return ledgeId; }
           set { ledgeId = value; }
       }
       public string FormType
       {
           get { return formType; }
           set { formType = value; }
       }
        public DateTime TransactionDate
        {
            get { return transactionDate; }
            set { transactionDate = value; }
        }

        public DateTime ChqDate
        {
            get { return chqDate; }
            set { chqDate = value; }
        }

        public DateTime DocDateGre
        {
            get { return docDateGre; }
            set { docDateGre = value; }
        }

        public decimal TotalBalance
        {
            get { return totalBalance; }
            set { totalBalance = value; }
        }

        public decimal TotalCurrent
        {
            get { return totalCurrent; }
            set { totalCurrent = value; }
        }

        public decimal TotalPaid
        {
            get { return totalPaid; }
            set { totalPaid = value; }
        }

        public decimal DebitCode2Amt
        {
            get { return debitCode2Amt; }
            set { debitCode2Amt = value; }
        }

        public decimal Rates
        {
            get { return rates; }
            set { rates = value; }
        }

        public decimal Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        public decimal RecNo
        {
            get { return recNo; }
            set { recNo = value; }
        }
        public string DebitCode2
        {
            get { return debitCode2; }
            set { debitCode2 = value; }
        }

        public string CancelFlag
        {
            get { return cancelFlag; }
            set { cancelFlag = value; }
        }

        public string DistType
        {
            get { return distType; }
            set { distType = value; }
        }

        public string PostFlag
        {
            get { return postFlag; }
            set { postFlag = value; }
        }

        public string UserCode
        {
            get { return userCode; }
            set { userCode = value; }
        }

        public string Notes
        {
            get { return notes; }
            set { notes = value; }
        }

        public string CurCode
        {
            get { return curCode; }
            set { curCode = value; }
        }

        public string Desc2
        {
            get { return desc2; }
            set { desc2 = value; }
        }

        public string CreditCode
        {
            get { return creditCode; }
            set { creditCode = value; }
        }

        public string Desc1
        {
            get { return desc1; }
            set { desc1 = value; }
        }

        public string DebitCode
        {
            get { return debitCode; }
            set { debitCode = value; }
        }

        public string AccDetails
        {
            get { return accDetails; }
            set { accDetails = value; }
        }

        public string BankCode
        {
            get { return bankCode; }
            set { bankCode = value; }
        }

        public string ChqNo
        {
            get { return chqNo; }
            set { chqNo = value; }
        }

        public string PayCode
        {
            get { return payCode; }
            set { payCode = value; }
        }

        public string RDocNo
        {
            get { return rDocNo; }
            set { rDocNo = value; }
        }

        public string EmpCode
        {
            get { return empCode; }
            set { empCode = value; }
        }

        public string SupCode
        {
            get { return supCode; }
            set { supCode = value; }
        }

        public string DocDateHij
        {
            get { return docDateHij; }
            set { docDateHij = value; }
        }

        public string DocNo
        {
            get { return docNo; }
            set { docNo = value; }
        }

        public string Branch
        {
            get { return branch; }
            set { branch = value; }
        }

        public int PrintedTimes
        {
            get { return printedTimes; }
            set { printedTimes = value; }
        }

        public int ModiTimes
        {
            get { return modiTimes; }
            set { modiTimes = value; }
        }

        public int DocId
        {
            get { return docId; }
            set { docId = value; }
        }

        public System.Data.SqlClient.SqlDataReader getDataByDocNo()
        {
            string query = "SELECT DOC_NO,CONVERT(NVARCHAR,DOC_DATE_GRE,103) AS DOC_DATE_GRE,DOC_DATE_HIJ,AMOUNT,TOTAL_PAID,SUP_CODE,PAY_CODE,CHQ_NO FROM " + formType + " WHERE DOC_NO = '" + docNo + "'";
            return DbFunctions.GetDataReader(query);
        }

        public System.Data.SqlClient.SqlDataReader getDataByDocNoCus()
        {
            string query = "SELECT DOC_NO,CONVERT(NVARCHAR,DOC_DATE_GRE,103) AS DOC_DATE_GRE,DOC_DATE_HIJ,AMOUNT,TOTAL_PAID,CUST_CODE,PAY_CODE,CHQ_NO FROM " + formType + " WHERE DOC_NO = '" + docNo + "'";
            return DbFunctions.GetDataReader(query);
        }
        public DataTable getsupPayVoucherData()
        {
            string query = "select DOC_ID, BRANCH, DOC_NO, REC_NO, CONVERT(NVARCHAR(50), DOC_DATE_GRE, 103) AS DOC_DATE_GRE, DOC_DATE_HIJ, SUP_CODE, EMP_CODE, AMOUNT, RDOCNO, PAY_CODE, CHQ_NO, CHQ_DATE, BANK_CODE, ACC_DETAILS, DEBIT_CODE, DESC1, CREDIT_CODE, DESC2, CUR_CODE, RATES, NOTES, USER_CODE, POST_FLAG, MODI_TIMES, PRINTED_TIME, DIST_TYPE, CANCEL_FLAG, DEBIT_CODE_2, DEBIT_CODE_2_AMT, TOTAL_PAID, TOTAL_CURRENT, TOTAL_BALANCE from PAY_PAYMENT_VOUCHER_HDR WHERE DOC_NO='" + docNo + "'";
            return DbFunctions.GetDataTable(query);
        }
        public DataTable getDataFromRecieptVoucher()
        {
            string query = "select DOC_ID, BRANCH, DOC_NO, REC_NO, CONVERT(VARCHAR(50), DOC_DATE_GRE, 103) AS DOC_DATE_GRE, DOC_DATE_HIJ, CUR_CODE, EXCHANGE_RATE, RDOC_NO, CUST_CODE, SMAN_CODE, AMOUNT, PAY_CODE, CHQ_NO, CHQ_DATE, BANK_CODE, ACC_DETAILS, DEBIT_CODE, DESC1, CREDIT_CODE, DESC2, NOTES, USER_CODE, MODITIMES, PRINTEDTIMES, DIST_TYPE, DISCOUNT, POST_FLAG, CANCEL_FLAG, TOTAL_PAID, TOTAL_CURRENT, TOTAL_BALANCE from REC_RECEIPTVOUCHER_HDR WHERE DOC_NO='" + docNo + "'";
            return DbFunctions.GetDataTable(query);
        }
        public object  getMaxRecNo()
        {
            string query = "SELECT ISNULL(MAX(CONVERT(DECIMAL(18,0),REC_NO)), 0) FROM PAY_PAYMENT_VOUCHER_HDR";
            return DbFunctions.GetAValue(query);
        }
        public object getVouchStartFrom()
        {
            string query = "SELECT VouchStartFrom FROM GEN_VOUCH_STARTFROM WHERE VouchTypeCode='PAY'";
            return DbFunctions.GetAValue(query);
        }
        public int deleteByDocNo()
        {
            string query = "DELETE FROM PAY_PAYMENT_VOUCHER_HDR WHERE DOC_NO = '" + docNo + "';DELETE FROM PAY_PAYMENT_VOUCHER_DTL WHERE DOC_NO = '" + docNo + "'";
            return DbFunctions.InsertUpdate(query);
        }
        public DataTable bindDayPayment()
        {
            string query = "SELECT ROW_NUMBER() Over (Order by DOC_NO) as 'Sl_No',REC_NO AS 'VOUCHER_NO',DOC_NO, CONVERT(NVARCHAR(50), DOC_DATE_GRE, 103) AS DOC_DATE_GRE, DOC_DATE_HIJ, SUP_CODE,DESC1 DESC_ENG,DESC2 DESC_ENG2, AMOUNT, CHQ_NO, PAY_CODE FROM PAY_PAYMENT_VOUCHER_HDR LEFT JOIN PAY_SUPPLIER ON CODE=SUP_CODE WHERE convert(varchar,DOC_DATE_GRE,101) = @DATE";
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@DATE", docDateGre.Date);
            return DbFunctions.GetDataTable(query, Parameters);
        }
        public System.Data.SqlClient.SqlDataReader bindDayReciept()
        {
            string query = "SELECT ROW_NUMBER() Over (Order by DOC_NO) as 'Sl_No',REC_NO AS 'VOUCHER_NO',DOC_NO, CONVERT(NVARCHAR(50), DOC_DATE_GRE, 103) AS DOC_DATE_GRE, DOC_DATE_HIJ, CUST_CODE,DESC2 AS DESC_ENG,DESC1 AS DESC_ENG1, PAY_CODE, AMOUNT, CHQ_NO FROM REC_RECEIPTVOUCHER_HDR LEFT JOIN REC_CUSTOMER ON CODE = CUST_CODE WHERE convert(varchar,DOC_DATE_GRE,101)  = @DATE";
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@DATE", docDateGre.Date);
            return DbFunctions.GetDataReader(query, Parameters);
        }
        public DataTable getCustomerByLedgNo()
        {
            string query="SELECT REC_CUSTOMER.CODE,REC_CUSTOMER.DESC_ENG,REC_CUSTOMER.MOBILE,ISNULL(DEBIT.total_DEBIT,0) AS total_DEBIT,ISNULL(CREDIT.total_CREDIT,0) AS total_CREDIT,(ISNULL(DEBIT.total_DEBIT,0)-ISNULL(CREDIT.total_CREDIT,0)) AS BALANCE  FROM REC_CUSTOMER LEFT OUTER JOIN (SELECT ACCID,ISNULL(SUM(DEBIT),0) as total_DEBIT FROM tb_Transactions GROUP BY ACCID) AS DEBIT ON REC_CUSTOMER.LedgerId=DEBIT.ACCID  LEFT OUTER JOIN (SELECT ACCID,ISNULL(SUM(CREDIT),0) as total_CREDIT FROM tb_Transactions GROUP BY ACCID) AS CREDIT ON REC_CUSTOMER.LedgerId=CREDIT.ACCID WHERE REC_CUSTOMER.LedgerId=" + ledgeId + " ORDER BY REC_CUSTOMER.CODE";
            return DbFunctions.GetDataTable(query);
        }
        public DataTable getAllPaymentVoucher()
        {
            string query = "SELECT * FROM PAY_PAYMENT_VOUCHER_HDR LEFT OUTER JOIN GEN_PAYTYPE ON PAY_PAYMENT_VOUCHER_HDR.PAY_CODE=GEN_PAYTYPE.CODE WHERE REC_NO='" + recNo+ "'";
            return DbFunctions.GetDataTable(query);
        }
        public object  getDocDateGre()
        {
            string query = "SELECT DOC_DATE_GRE FROM PAY_PAYMENT_VOUCHER_HDR WHERE REC_NO='" + recNo + "'";
            return DbFunctions.GetAValue(query);
        }
        public System.Data.SqlClient.SqlDataReader getAllDataByDocNo(string tableDTL)
        {
            string query = "SELECT INV_NO,CONVERT(NVARCHAR,INV_DATE_GRE,103) AS INV_DATE_GRE,INV_DATE_HIJ,AMOUNT,PAID,CURRENT_PAY_AMOUNT,BALANCE FROM " + tableDTL + " WHERE DOC_NO = '" + docNo + "'";
            return DbFunctions.GetDataReader(query);
        }
        public DataTable supCreditPurchaseProcedure_1(string command,string para)
        {
            Dictionary<string, object> parameter = new Dictionary<string, object>();
            parameter.Add(para,supCode);
            return DbFunctions.GetDataTableProcedure(command, parameter);
        }

        public SqlDataReader supCreditPurchaseProcedure(string command, string para)
        {
            Dictionary<string, object> parameter = new Dictionary<string, object>();
            parameter.Add(para, supCode);
            return DbFunctions.GetDataReaderProcedure(command, parameter);
        }
    }
}
