using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Sys_Sols_Inventory.Model
{
    class RecRecieptVoucherHdrDB
    {
        private int docId, moditimes, printedTimes,projectId;
        private string branch, docNo, docDateHij, curCode, rDocNo, custCode, smanCode, payCode, chqNo, bankCode;
        private string accDetails, debitCode, desc1, creditCode, desc2, notes, userCode, distType, postFlag, cancelFlag;
        private decimal recNo, exchangeRate, amount, discount, totalPaid, totalcurrent, totalBalance;
        private DateTime? docDateGre, chqDate, transactionDate;

        public int ProjectId
        {
            get { return projectId; }
            set { projectId = value; }
        }
        public DateTime? TransactionDate
        {
            get { return transactionDate; }
            set { transactionDate = value; }
        }

        public DateTime? ChqDate
        {
            get { return chqDate; }
            set { chqDate = value; }
        }

        public DateTime? DocDateGre
        {
            get { return docDateGre; }
            set { docDateGre = value; }
        }

        public decimal TotalBalance
        {
            get { return totalBalance; }
            set { totalBalance = value; }
        }

        public decimal Totalcurrent
        {
            get { return totalcurrent; }
            set { totalcurrent = value; }
        }

        public decimal TotalPaid
        {
            get { return totalPaid; }
            set { totalPaid = value; }
        }

        public decimal Discount
        {
            get { return discount; }
            set { discount = value; }
        }

        public decimal Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        public decimal ExchangeRate
        {
            get { return exchangeRate; }
            set { exchangeRate = value; }
        }

        public decimal RecNo
        {
            get { return recNo; }
            set { recNo = value; }
        }
        public string CancelFlag
        {
            get { return cancelFlag; }
            set { cancelFlag = value; }
        }

        public string PostFlag
        {
            get { return postFlag; }
            set { postFlag = value; }
        }

        public string DistType
        {
            get { return distType; }
            set { distType = value; }
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

        public string SmanCode
        {
            get { return smanCode; }
            set { smanCode = value; }
        }

        public string CustCode
        {
            get { return custCode; }
            set { custCode = value; }
        }

        public string RDocNo
        {
            get { return rDocNo; }
            set { rDocNo = value; }
        }

        public string CurCode
        {
            get { return curCode; }
            set { curCode = value; }
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

        public int Moditimes
        {
            get { return moditimes; }
            set { moditimes = value; }
        }

        public int DocId
        {
            get { return docId; }
            set { docId = value; }
        }
        public object getMaxRecNoRecVouch()
        {
           

//            string query = "select Max(vh.DOC_NO) from REC_RECEIPTVOUCHER_HDR vh inner join tbl_FinancialYear fy ";
 //           query += " on vh.DOC_DATE_GRE between fy.SDate and fy.EDate and fy.CurrentFY = 1 ";

            string query = "SELECT ISNULL(MAX(CONVERT(DECIMAL(18,0),REC_NO)), 0) FROM REC_RECEIPTVOUCHER_HDR h inner join tbl_FinancialYear fy ";
            query += "on convert(varchar, h.DOC_DATE_GRE, 111) between fy.SDate and fy.EDate and fy.CurrentFY = 1 ";
           return DbFunctions.GetAValue(query);
        }
        public object getVouchStartFrom()
        {
            string query = "select Min(vh.DOC_NO) from REC_RECEIPTVOUCHER_HDR vh inner join tbl_FinancialYear fy ";
            query += " on convert(varchar, vh.DOC_DATE_GRE, 111) between fy.SDate and fy.EDate and fy.CurrentFY = 1 "; 
            //query = "SELECT VouchStartFrom FROM GEN_VOUCH_STARTFROM WHERE VouchTypeCode='REC'";
            return DbFunctions.GetAValue(query);
        }
        public int deleteByDocNo()
        {
            string query = "DELETE FROM REC_RECEIPTVOUCHER_HDR WHERE DOC_NO = '" + docNo + "';DELETE FROM REC_RECEIPTVOUCHER_DTL WHERE DOC_NO = '" + docNo + "'";
            return DbFunctions.InsertUpdate(query);
        }
        public DataTable getAllRecieptVoucher()
        {
            string query = "SELECT * FROM REC_RECEIPTVOUCHER_HDR LEFT OUTER JOIN GEN_PAYTYPE ON REC_RECEIPTVOUCHER_HDR.PAY_CODE=GEN_PAYTYPE.CODE WHERE REC_NO='" + recNo + "'";
            return DbFunctions.GetDataTable(query);
        }
        public DataTable getAllRecieptVoucher(string docno)
        {
            string query = "SELECT * FROM REC_RECEIPTVOUCHER_HDR LEFT OUTER JOIN GEN_PAYTYPE ON REC_RECEIPTVOUCHER_HDR.PAY_CODE=GEN_PAYTYPE.CODE WHERE DOC_NO='" + docno + "'";
            return DbFunctions.GetDataTable(query);
        }

        public object getDocDateGre()
        {
            string query = "SELECT DOC_DATE_GRE FROM REC_RECEIPTVOUCHER_HDR WHERE REC_NO='" + recNo + "'";
            return DbFunctions.GetAValue(query);
        }
        public System.Data.SqlClient.SqlDataReader getDataByDocDate()
        {
            string query = "SELECT        DOC_NO, DOC_DATE_GRE, DOC_DATE_HIJ, CUR_CODE, PAY_CODE, AMOUNT, CHQ_NO FROM            REC_RECEIPTVOUCHER_HDR WHERE        (DOC_DATE_GRE = @DATE)";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@DATE", docDateGre);
            return DbFunctions.GetDataReader(query,parameters);
        }

        public void Insert_Data()
        {
            string query = "INSERT INTO REC_RECEIPTVOUCHER_HDR (BRANCH,DOC_NO,REC_NO,DOC_DATE_GRE,DOC_DATE_HIJ,CUST_CODE,CUR_CODE,AMOUNT,PAY_CODE,BANK_CODE,CHQ_NO,CHQ_DATE,DEBIT_CODE,DESC1,CREDIT_CODE,DESC2,NOTES,TOTAL_PAID,TOTAL_CURRENT,TOTAL_BALANCE,PROJECTID) VALUES('" + branch + "','" + docNo + "','" + recNo + "','" + docDateGre + "','" + docDateHij + "','" + custCode + "','" + curCode + "','" + amount + "','" + payCode + "','" + bankCode + "','" + chqNo + "','" + chqDate + "','" + debitCode + "','" + desc1 + "','" + creditCode + "','" + desc2 + "','" + notes + "','" + totalPaid + "','" + totalcurrent + "','" + totalBalance + "','" + projectId + "')";
            DbFunctions.InsertUpdate(query);
        }

    }
}
