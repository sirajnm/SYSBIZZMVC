using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Sys_Sols_Inventory
{
    class DoubleEntryTransaction
    {
        private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        private SqlCommand cmd = new SqlCommand();

        public DoubleEntryTransaction()
        {
            cmd.Connection = conn;
        }

        public void insertTransaction(string doc_no, string voucher_type, string date, string branch, string narration, string account_id, string account_name, string party_account_id, string party_account_name, string amount)
        {
            Class.Transactions trans = new Class.Transactions();
            trans.VOUCHERTYPE = voucher_type;
            trans.DATED = date;
            trans.NARRATION = narration;
            Login log = (Login)Application.OpenForms["Login"];
            trans.USERID = log.EmpId;
            trans.ACCNAME = account_name;
            trans.PARTICULARS = party_account_name;
            trans.VOUCHERNO = doc_no;
            trans.BRANCH = branch;
            trans.ACCID = account_id;
            trans.CREDIT = "0";
            trans.NARRATION = narration;
            trans.DEBIT = amount;
            trans.SYSTEMTIME = DateTime.Now.ToString();
            trans.insertTransaction();

            trans.PARTICULARS = account_name;
            trans.ACCNAME = party_account_name;
            trans.ACCID = party_account_id;
            trans.DEBIT = "0";
            trans.CREDIT = amount;
            trans.SYSTEMTIME = DateTime.Now.ToString();
            trans.insertTransaction();
        }
    }
}
