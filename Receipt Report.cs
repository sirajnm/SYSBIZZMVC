using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Sys_Sols_Inventory.Model;

namespace Sys_Sols_Inventory
{
    public partial class Receipt_Report : Form
    {
        //SqlCommand cmd = new SqlCommand();
        //SqlDataAdapter adapter = new SqlDataAdapter();
        //private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        public Receipt_Report()
        {
            InitializeComponent();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            string query;
            if (rb_reciept.Checked)
            {
                try
                {
                    DataTable dt = new DataTable();
                    if (!Chk.Checked)
                    {
                        query = "SELECT tb_Transactions.DATED AS DATE,tb_Transactions.VOUCHERNO,tb_Transactions.ACCNAME AS [ACCOUNT NAME],tb_Transactions.PARTICULARS,tb_Transactions.DEBIT AS AMOUNT,tb_Transactions.NARRATION FROM tb_Transactions LEFT OUTER JOIN (select LEDGERID,ACOUNTID from tb_Ledgers LEFT OUTER JOIN (SELECT * FROM tb_AccountGroup where UNDER=9) AS ACC_GROUP ON tb_Ledgers.UNDER=ACC_GROUP.ACOUNTID WHERE ACOUNTID IS NOT NULL) AS Ledgers ON Ledgers.LEDGERID=tb_Transactions.ACCID WHERE LEDGERID IS NOT NULL AND DEBIT>0";
                        dt = DbFunctions.GetDataTable(query);
                    }
                    else
                    {
                        query = "SELECT tb_Transactions.DATED AS DATE,tb_Transactions.VOUCHERNO,tb_Transactions.ACCNAME AS [ACCOUNT NAME],tb_Transactions.PARTICULARS,tb_Transactions.DEBIT AS AMOUNT,tb_Transactions.NARRATION FROM tb_Transactions LEFT OUTER JOIN (select LEDGERID,ACOUNTID from tb_Ledgers LEFT OUTER JOIN (SELECT * FROM tb_AccountGroup where UNDER=9) AS ACC_GROUP ON tb_Ledgers.UNDER=ACC_GROUP.ACOUNTID WHERE ACOUNTID IS NOT NULL) AS Ledgers ON Ledgers.LEDGERID=tb_Transactions.ACCID WHERE LEDGERID IS NOT NULL AND DEBIT>0 AND DATED BETWEEN @D1 AND @D2";
                        //cmd.Parameters.Add("@D1", SqlDbType.Date).Value = DATE_FROM.Value;
                        //cmd.Parameters.Add("@D2", SqlDbType.Date).Value = DATE_TO.Value;

                        Dictionary<string, object> parameter = new Dictionary<string, object>();
                        parameter.Add("@D1", DATE_FROM.Value);
                        parameter.Add("@D2", DATE_TO.Value);
                        dt = DbFunctions.GetDataTable(query, parameter);
                    }
                    //adapter = new SqlDataAdapter(cmd);
                    //adapter.Fill(dt);
                    dg.DataSource = dt;
                }
                catch (Exception E)
                {
                    MessageBox.Show(E.ToString());
                }
            }
            else
            {
                try
                {
                    DataTable dt = new DataTable();
                    if (!Chk.Checked)
                    {
                        query = "SELECT tb_Transactions.DATED AS DATE,tb_Transactions.VOUCHERNO,tb_Transactions.ACCNAME AS [ACCOUNT NAME],tb_Transactions.PARTICULARS,tb_Transactions.CREDIT AS AMOUNT,tb_Transactions.NARRATION FROM tb_Transactions LEFT OUTER JOIN (select LEDGERID,ACOUNTID from tb_Ledgers LEFT OUTER JOIN (SELECT * FROM tb_AccountGroup where UNDER=9) AS ACC_GROUP ON tb_Ledgers.UNDER=ACC_GROUP.ACOUNTID WHERE ACOUNTID IS NOT NULL) AS Ledgers ON Ledgers.LEDGERID=tb_Transactions.ACCID WHERE LEDGERID IS NOT NULL AND CREDIT>0";
                        dt = DbFunctions.GetDataTable(query);
                    }
                    else
                    {
                        query = "SELECT tb_Transactions.DATED AS DATE,tb_Transactions.VOUCHERNO,tb_Transactions.ACCNAME AS [ACCOUNT NAME],tb_Transactions.PARTICULARS,tb_Transactions.CREDIT AS AMOUNT,tb_Transactions.NARRATION FROM tb_Transactions LEFT OUTER JOIN (select LEDGERID,ACOUNTID from tb_Ledgers LEFT OUTER JOIN (SELECT * FROM tb_AccountGroup where UNDER=9) AS ACC_GROUP ON tb_Ledgers.UNDER=ACC_GROUP.ACOUNTID WHERE ACOUNTID IS NOT NULL) AS Ledgers ON Ledgers.LEDGERID=tb_Transactions.ACCID WHERE LEDGERID IS NOT NULL AND CREDIT>0 AND DATED BETWEEN @D1 AND @D2";
                        Dictionary<string, object> parameter = new Dictionary<string, object>();
                        //cmd.Parameters.Add("@D1", SqlDbType.Date).Value = DATE_FROM.Value;
                        //cmd.Parameters.Add("@D2", SqlDbType.Date).Value = DATE_TO.Value;
                        parameter.Add("@D1", DATE_FROM.Value);
                        parameter.Add("@D2", DATE_TO.Value);
                        dt = DbFunctions.GetDataTable(query, parameter);
                    }
                    //adapter = new SqlDataAdapter(cmd);
                    //adapter.Fill(dt);
                    dg.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("" + ex);
                }
            }
            DESIGN();
        }
        public void DESIGN()
        {
            decimal total = 0;
            if (dg.RowCount > 0)
            {
                dg.Columns["ACCOUNT NAME"].Width = 200;
                dg.Columns["NARRATION"].Width = 210;
                dg.Columns["PARTICULARS"].Width = 200;

                for (int i = 0; i < dg.RowCount; i++)
                {
                    total = total + Convert.ToDecimal(dg.Rows[i].Cells["AMOUNT"].Value);
                }
                dg.Rows[dg.RowCount - 1].Cells["ACCOUNT NAME"].Value = "TOTAL";
                dg.Rows[dg.RowCount - 1].Cells["ACCOUNT NAME"].Style.Font = new Font(dg.Rows[dg.RowCount - 1].Cells["ACCOUNT NAME"].InheritedStyle.Font, FontStyle.Bold);
                dg.Rows[dg.RowCount - 1].Cells["AMOUNT"].Value = total.ToString();
                dg.Rows[dg.RowCount - 1].Cells["AMOUNT"].Style.Font = new Font(dg.Rows[dg.RowCount - 1].Cells["AMOUNT"].InheritedStyle.Font, FontStyle.Bold);
            }
        }

        private void grpDate_Enter(object sender, EventArgs e)
        {

        }

        private void Receipt_Report_Load(object sender, EventArgs e)
        {
            rb_reciept.Checked = true;
            btnRun.PerformClick();
        }
    }
}