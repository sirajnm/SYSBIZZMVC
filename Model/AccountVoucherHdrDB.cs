using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Sys_Sols_Inventory.Model
{
    class AccountVoucherHdrDB
    {
        private int docId, modiTimes, printedTimes,projectid;
        private string branch, docNo, docDateHij, supCode, empCode, rDocNo, payCode, chqNo, bankCode, accountDetails;
        private string debitcode, desc1, creditCode, desc2, curCode, notes, userCode, postFlag, diskType, cancelFlag, debitCode2;
        private decimal recNo, amount, rates, debitCode2Amt, totalPaid, totalCurrent, totalBalance;
        private DateTime docDateGre, chqDate, transactionDate;

        public int ProjectId
        {
            get { return projectid; }
            set { projectid = value; }
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

        public string DiskType
        {
            get { return diskType; }
            set { diskType = value; }
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

        public string Debitcode
        {
            get { return debitcode; }
            set { debitcode = value; }
        }

        public string AccountDetails
        {
            get { return accountDetails; }
            set { accountDetails = value; }
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

        public System.Data.SqlClient.SqlDataReader getDatasFromRecNo(string tblHDR)
        {
            string query = "SELECT DOC_NO,CONVERT(NVARCHAR,DOC_DATE_GRE,103) AS DOC_DATE_GRE,DOC_DATE_HIJ,AMOUNT,TOTAL_PAID,SUP_CODE,PAY_CODE,CHQ_NO FROM " + tblHDR + " WHERE DOC_NO = '" + docNo + "'";
            return DbFunctions.GetDataReader(query);
        }
        public System.Data.SqlClient.SqlDataReader getAllDataByDocNo()
        {
            string query = "select * from ACCOUNT_VOUCHER_HDR WHERE DOC_NO='" + docNo + "'";
            return DbFunctions.GetDataReader(query);
        }
        public int insertData()
        {
            string query = "INSERT INTO ACCOUNT_VOUCHER_HDR (DOC_ID, DOC_NO,REC_NO,DOC_DATE_GRE,DOC_DATE_HIJ,CUR_CODE,AMOUNT,PAY_CODE,BANK_CODE,ACC_DETAILS,CHQ_NO,CHQ_DATE,CREDIT_CODE,DESC2,DEBIT_CODE,DESC1,NOTES,TOTAL_PAID,TOTAL_CURRENT,TOTAL_BALANCE,Project_Id) VALUES('"+docId+ "','" + docNo + "','" + recNo + "','" + docDateGre.ToString("MM/dd/yyyy") + "','" + docDateHij + "','" + curCode + "','" + amount + "','" + payCode + "','" + bankCode + "','" + accountDetails + "','" + chqNo + "','" + (payCode == "CHQ" ? chqDate.ToString("MM/dd/yyyy") : null) + "','" + creditCode + "','" + desc2 + "','" + debitcode + "','" + desc1 + "','" + notes + "','" + totalPaid + "','" + totalCurrent + "','" + totalBalance + "','" + projectid + "')";
            return DbFunctions.InsertUpdate(query);
        }
        public int updateDataByDocNo(string tableHDR)
        {
            string query = "UPDATE " + tableHDR + " SET DOC_DATE_GRE = '" + docDateGre.ToString("yyyy/MM/dd") + "',DOC_DATE_HIJ = '" + docDateHij + "',CUR_CODE = '" + curCode + "',AMOUNT = '" + amount + "',PAY_CODE = '" + payCode + "',CREDIT_CODE = '" + creditCode + "',DEBIT_CODE = '" + debitcode + "',BANK_CODE = '" + bankCode + "',CHQ_NO = '" + chqNo + "',CHQ_DATE = '" + (payCode == "CHQ" ? chqDate.ToString("yyyy/MM/dd") : null) + "',NOTES = '" + notes + "',TOTAL_PAID = '" + totalPaid + "',TOTAL_CURRENT = '" + totalCurrent + "',TOTAL_BALANCE = '" + totalBalance + "' , Project_Id='"+projectid+"' WHERE DOC_NO = '" + docNo + "'";
           return DbFunctions.InsertUpdate(query);
        }
        public object getMaxRecNo()
        {
            string query = "SELECT ISNULL(MAX(CONVERT(DECIMAL(18,0),REC_NO)), 0) FROM ACCOUNT_VOUCHER_HDR";
            return DbFunctions.GetAValue(query);
        }
        public object getVouchStartFrom()
        {
            string query = "SELECT VouchStartFrom FROM GEN_VOUCH_STARTFROM WHERE VouchTypeCode='ACC'";
            return DbFunctions.GetAValue(query);
        }
        public int deleteByDocNo()
        {
            string query = "DELETE FROM ACCOUNT_VOUCHER_HDR WHERE DOC_NO = '" + docNo+ "'";
            return DbFunctions.InsertUpdate(query);
        }
        public DataTable bindByDate()
        {
            string query = "SELECT ROW_NUMBER() Over (Order by DOC_NO) as 'Sl_No',REC_NO AS 'VOUCHER_NO',DOC_NO, CONVERT(NVARCHAR(50), DOC_DATE_GRE, 103) AS DOC_DATE_GRE, DOC_DATE_HIJ,DESC1 DESC_ENG,DESC2 DESC_ENG2, AMOUNT, CHQ_NO, PAY_CODE FROM ACCOUNT_VOUCHER_HDR WHERE DOC_DATE_GRE = @DATE";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@DATE",docDateGre);
            return DbFunctions.GetDataTable(query,parameters);
        }
        public DataTable getAllData()
        {
            string query = "SELECT * FROM ACCOUNT_VOUCHER_HDR LEFT OUTER JOIN GEN_PAYTYPE ON ACCOUNT_VOUCHER_HDR.PAY_CODE=GEN_PAYTYPE.CODE WHERE REC_NO='" + recNo + "'";
            return DbFunctions.GetDataTable(query);
        }

        public DataTable getAllData(string docno)
        {
            string query = "SELECT * FROM ACCOUNT_VOUCHER_HDR LEFT OUTER JOIN GEN_PAYTYPE ON ACCOUNT_VOUCHER_HDR.PAY_CODE=GEN_PAYTYPE.CODE WHERE DOC_NO='" + docno + "'";
            return DbFunctions.GetDataTable(query);
        }

        public object getDocDateGre()
        {
            string query = "SELECT DOC_DATE_GRE FROM ACCOUNT_VOUCHER_HDR WHERE REC_NO='" + recNo + "'";
            return DbFunctions.GetAValue(query);
        }
    }
}
