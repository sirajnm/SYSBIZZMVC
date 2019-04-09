using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sys_Sols_Inventory.Model
{
    class TblDeletedTransactionDB
    {
        private int transactionId;
        string voucherNo, voucherType, accName, particulars, narration, voucherPrefix, costCenterId, userId, counterId, systemTiem, accId;
        private decimal debit, credit;
        private DateTime dated;
        private bool active;

        public int TransactionId
        {
            get { return transactionId; }
            set { transactionId = value; }
        }
        public string VoucherNo
        {
            get { return voucherNo; }
            set { voucherNo = value; }
        }

        public string VoucherType
        {
            get { return voucherType; }
            set { voucherType = value; }
        }

        public string AccName
        {
            get { return accName; }
            set { accName = value; }
        }

        public string Particulars
        {
            get { return particulars; }
            set { particulars = value; }
        }

        public string Narration
        {
            get { return narration; }
            set { narration = value; }
        }

        public string VoucherPrefix
        {
            get { return voucherPrefix; }
            set { voucherPrefix = value; }
        }

        public string CostCenterId
        {
            get { return costCenterId; }
            set { costCenterId = value; }
        }

        public string UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        public string CounterId
        {
            get { return counterId; }
            set { counterId = value; }
        }

        public string SystemTiem
        {
            get { return systemTiem; }
            set { systemTiem = value; }
        }

        public string AccId
        {
            get { return accId; }
            set { accId = value; }
        }
       
        public decimal Debit
        {
            get { return debit; }
            set { debit = value; }
        }

        public decimal Credit
        {
            get { return credit; }
            set { credit = value; }
        }
       

        public DateTime Dated
        {
            get { return dated; }
            set { dated = value; }
        }

        public bool Active
        {
            get { return active; }
            set { active = value; }
        }
        public int insertDeletedTran()
        {
           string query= "insert into  tbl_deletedTransaction(VOUCHERNO, VOUCHERTYPE, ACCNAME, PARTICULARS, DEBIT, CREDIT, DATED, NARRATION, USERID, SYSTEMTIEM, ACCID) select    VOUCHERNO, VOUCHERTYPE, ACCNAME, PARTICULARS, DEBIT, CREDIT, DATED, NARRATION, USERID, SYSTEMTIEM, ACCID from tb_Transactions where VOUCHERNO='" + voucherNo + "' and VOUCHERTYPE='" + voucherType+ "'";
           return DbFunctions.InsertUpdate(query);
        }

    }
}
