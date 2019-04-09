using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows;
using System.Globalization;
using Sys_Sols_Inventory.Model;
namespace Sys_Sols_Inventory.Class
{
    class ModifiedTransaction
    {
        //private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        //private SqlCommand cmd = new SqlCommand();
        //private SqlDataAdapter adapter = new SqlDataAdapter();
        public string SlNo { get; set; }
        public string VOUCHERNO { get; set; }
        public string VOUCHERTYPE { get; set; }
        public string ACCNAME { get; set; }
        public string PARTICULARS { get; set; }
        public string DEBIT { get; set; }
        public string CREDIT { get; set; }
        public string Date { get; set; }

        public string NARRATION { get; set; }
        public string STATUS { get; set; }
        public string USERID { get; set; }
        public string MODIFIEDDATE { get; set; }
        public string BRANCH { get; set; }

        public string INVOICENO { get; set; }


        public void insertTransaction()
        {
          
            string query = "insert into    tbl_ModifiedTransaction( Date, VOUCHERNO, VOUCHERTYPE, NARRATION, STATUS, USERID, MODIFIEDDATE,BRANCH,INVOICENO) values(@Date, @VOUCHERNO, @VOUCHERTYPE, @NARRATION, @STATUS, @USERID, @MODIFIEDDATE,@BRANCH,@INVOICENO)";
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@Date", Date);
            Parameters.Add("@VOUCHERNO", VOUCHERNO);
            Parameters.Add("@VOUCHERTYPE", VOUCHERTYPE);
            Parameters.Add("@MODIFIEDDATE", MODIFIEDDATE);
            Parameters.Add("@NARRATION", NARRATION);
            Parameters.Add("@USERID", USERID);
            Parameters.Add("@STATUS", STATUS);
            Parameters.Add("@BRANCH", BRANCH);
            if (INVOICENO == null)
            {
                INVOICENO = "";
            }
            Parameters.Add("@INVOICENO", INVOICENO);
            DbFunctions.InsertUpdate(query, Parameters);
        }
        public void insertDeletedTransaction()
        {
            string query = "insert into     tbl_deletedTransaction (VOUCHERNO, VOUCHERTYPE, ACCNAME, PARTICULARS, DEBIT, CREDIT, DATED, NARRATION, USERID, SYSTEMTIEM, ACCID) select    VOUCHERNO, VOUCHERTYPE, ACCNAME, PARTICULARS, DEBIT, CREDIT, DATED, NARRATION, USERID, SYSTEMTIEM, ACCID from tb_Transactions where VOUCHERNO='" + VOUCHERNO + "' and VOUCHERTYPE='" + VOUCHERTYPE + "'";
        }
    }
}
