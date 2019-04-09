using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Sys_Sols_Inventory.Model
{
    class TbLedgersDB
    {
        private string ledgerId;
        private string ledgerName, under, isBuiltIn, address, tin, cst, pin, phone, mobile, email;
        private string creditPeriod, creditAmount, displayName, costCentereId, title, bank, bankBranch, ifcCode, accountNo;

        public string AccountNo
        {
            get { return accountNo; }
            set { accountNo = value; }
        }

        public string IfcCode
        {
            get { return ifcCode; }
            set { ifcCode = value; }
        }

        public string BankBranch
        {
            get { return bankBranch; }
            set { bankBranch = value; }
        }

        public string Bank
        {
            get { return bank; }
            set { bank = value; }
        }

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public string CostCentereId
        {
            get { return costCentereId; }
            set { costCentereId = value; }
        }

        public string DisplayName
        {
            get { return displayName; }
            set { displayName = value; }
        }

        public string CreditAmount
        {
            get { return creditAmount; }
            set { creditAmount = value; }
        }

        public string CreditPeriod
        {
            get { return creditPeriod; }
            set { creditPeriod = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        public string Mobile
        {
            get { return mobile; }
            set { mobile = value; }
        }
        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }
        public string Pin
        {
            get { return pin; }
            set { pin = value; }
        }
        public string Cst
        {
            get { return cst; }
            set { cst = value; }    
        }
        public string Tin
        {
            get { return tin; }
            set { tin = value; }
        }
        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        public string IsBuiltIn
        {
            get { return isBuiltIn; }
            set { isBuiltIn = value; }
        }
        public string Under
        {
            get { return under; }
            set { under = value; }
        }
        public string LedgerName
        {
            get { return ledgerName; }
            set { ledgerName = value; }
        }
        public string LedgerId
        {
            get { return ledgerId; }
            set { ledgerId = value; }
        }
        public DataTable getDocId(string tbl)
        {
            string query = "SELECT DOC_ID FROM " + tbl + " WHERE DOC_NO='" + ledgerId + "'";
            return DbFunctions.GetDataTable(query);
        }
        public DataTable getRecNo(string tbl)
        {
            string query = "SELECT REC_NO FROM " + tbl + " WHERE DOC_NO='" + ledgerId + "'";
            return DbFunctions.GetDataTable(query);
        }

        public string GetLedgerName(string ledgerid)
        {
            string query = "Select LEDGERNAME from tb_Ledgers where LEDGERID = ' " + ledgerid + "'";
            return DbFunctions.GetAValue(query).ToString();
        }



    }
}
