using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sys_Sols_Inventory;
using Sys_Sols_Inventory.Model;
using Sys_Sols_Inventory.Class;

namespace Sys_Sols_Inventory.Accounts
{
    public partial class YearEnd : Form
    {
        DataTable datatableincexp, datatableincexp2, datatablebalancesheet1, datatablebalansheet2;
        BindingList<FinancialYear> financialyear;
        FinancialYear selectedfinancialyear = new FinancialYear();
        string InsertUpdateMode = "Update";
        public YearEnd()
        {
            InitializeComponent();
            dataExpense.AutoGenerateColumns = true;
            financialyear = new BindingList<FinancialYear>(GetAllFinancialYear());
            FinancialYeardataGridView.AutoGenerateColumns = true;
            FinancialYeardataGridView.DataSource = financialyear;
            comboBox1.DataSource = financialyear;
            comboBox1.DisplayMember = "FinancialYearCode";
            comboBox1.ValueMember = "FinaYearId";



        }

        private void GenerateEntriesButton_Click(object sender, EventArgs e)
        {
            string query = "Select c.AccountCode, c.ACC_ID, c.ACC_DESC, l.LEDGERID, l.LEDGERNAME, (sum(t.DEBIT) - sum(t.CREDIT)) Amount, ";
            query += " case when(sum(t.DEBIT) - sum(t.CREDIT)) > 0 then 0 else -(sum(t.DEBIT) - sum(t.CREDIT)) end Debit, ";
            query += " case when(sum(t.DEBIT) - sum(t.CREDIT)) < 0 then 0 else (sum(t.DEBIT) - sum(t.CREDIT)) end Credit ";
            query += " from tb_Transactions t  inner join tb_Ledgers l on t.ACCID = l.LEDGERID ";
            query += " inner join ChartOfAccounts c on l.UNDER = c.ACC_ID ";
            query += " where t.DATED between @startdate and @enddate and c.AccountCode like '03%' ";
            query += " group by c.AccountCode, c.ACC_ID, c.ACC_DESC, l.LEDGERID, l.LEDGERNAME ";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            Sys_Sols_Inventory.Class.CompanySetup comsetup = new Sys_Sols_Inventory.Class.CompanySetup();
            DataTable dt = new DataTable();
            dt = comsetup.GetCurrentFinancialYear();
            parameters.Add("@startdate", dt.Rows[0]["SDate"]);
            parameters.Add("@enddate", dt.Rows[0]["EDate"]);
            datatableincexp = DbFunctions.GetDataTable(query, parameters);
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("No Entries Found");
                return;
            }
            dataExpense.AutoGenerateColumns = true;
            dataExpense.DataSource = datatableincexp;
                        dataExpense.Refresh();
            
            string query2 = "Select '0456' AccountCode, '56' ACC_ID, 'Equity' ACC_DESC,  '45' LEDGERID, 'CAPITAL A/C' LEDGERNAME, (sum(t.DEBIT) - sum(t.CREDIT)) Amount, ";
            query2 += " case when(sum(t.DEBIT) - sum(t.CREDIT)) > 0 then (sum(t.DEBIT) - sum(t.CREDIT)) else 0 end Debit, ";
            query2 += " case when(sum(t.DEBIT) - sum(t.CREDIT)) < 0 then -(sum(t.DEBIT) - sum(t.CREDIT)) else 0 end Credit ";
            query2 += " from tb_Transactions t  inner join tb_Ledgers l on t.ACCID = l.LEDGERID ";
            query2 += " inner join ChartOfAccounts c on l.UNDER = c.ACC_ID ";
            query2 += " where t.DATED between @startdate and @enddate and c.AccountCode like '03%' ";
            //query2 += " group by  l.LEDGERID, l.LEDGERNAME ";
            datatableincexp2 = DbFunctions.GetDataTable(query2, parameters);
            dataIncome.AutoGenerateColumns = true;
            dataIncome.DataSource = datatableincexp2;
            dataIncome.Refresh();




            this.Refresh();

        }

        private void PostButton_Click(object sender, EventArgs e)
        {
           Sys_Sols_Inventory.Class.CompanySetup  company = new Sys_Sols_Inventory.Class.CompanySetup();
            String Branch = company.ReadBranch();
            Sys_Sols_Inventory.Class.CompanySetup comsetup = new Sys_Sols_Inventory.Class.CompanySetup();
            DataTable ds = new DataTable();
            ds = comsetup.GetCurrentFinancialYear();

            foreach (DataRow dr in datatableincexp.Rows)
            {
                Transactions trans = new Transactions();
                trans.ACCID = dr["LEDGERID"].ToString();
                trans.ACCNAME = dr["LEDGERNAME"].ToString();
                trans.VOUCHERNO = this.textBox1.Text;
                trans.VOUCHERTYPE = "FYClosing";
                trans.DATED = ds.Rows[0]["EDate"].ToString();
                trans.CurrentDate = DateTime.Today;
                trans.SYSTEMTIME = DateTime.UtcNow.ToShortTimeString();
                trans.USERID = "Admin";
                trans.BRANCH = Branch;
                trans.DEBIT = dr["Debit"].ToString();
                trans.CREDIT = dr["Credit"].ToString();
                trans.NARRATION = this.textBox2.Text;
                trans.PARTICULARS = this.textBox2.Text;
                try
                {
                    trans.insertTransaction();
                }
                catch (Exception ex)
                {

                    throw ex;
                }


            }
            foreach (DataRow dr in datatableincexp2.Rows)
            {
                Transactions trans = new Transactions();
                trans.ACCID = dr["LEDGERID"].ToString();
                trans.ACCNAME = dr["LEDGERNAME"].ToString();
                trans.VOUCHERNO = this.textBox1.Text;
                trans.VOUCHERTYPE = "FYClosing";
                trans.DATED = dateTimePicker1.Value.ToString();
                trans.CurrentDate = DateTime.Today;
                trans.SYSTEMTIME = DateTime.UtcNow.ToShortTimeString();
                trans.USERID = "Admin";
                trans.BRANCH = Branch;
                trans.DEBIT = dr["Debit"].ToString();
                trans.CREDIT = dr["Credit"].ToString();
                trans.NARRATION = this.textBox2.Text;
                trans.PARTICULARS = this.textBox2.Text;
                try
                {
                    trans.insertTransaction();
                }
                catch (Exception ex)
                {

                    throw ex;
                }


            }

            MessageBox.Show("Year Closed.");
            datatableincexp.Clear();
            datatableincexp2.Clear();
            dataIncome.DataSource = datatableincexp;
            dataExpense.DataSource = datatableincexp2;

        }

        private void button5_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fdb = new FolderBrowserDialog();
            fdb.ShowDialog();
            if (fdb.SelectedPath != null)
            {
                txtDirectory.Text = fdb.SelectedPath.ToString();
            }
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            BackupClass.Backup1(txtDirectory.Text);
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void GenerateBalanceSheet_Click(object sender, EventArgs e)
        {
            if (dateTimePicker3.Value == null)
            {
                MessageBox.Show("Select New Financial year");
                return;
            }

            string query = "Select @enddate Dated, 'FYClosing' VoucherType, c.AccountCode, c.ACC_ID, c.ACC_DESC, l.LEDGERID, l.LEDGERNAME, (sum(t.DEBIT) - sum(t.CREDIT)) Amount, ";
            query += " case when(sum(t.DEBIT) - sum(t.CREDIT)) > 0 then 0 else -(sum(t.DEBIT) - sum(t.CREDIT)) end Debit, ";
            query += " case when(sum(t.DEBIT) - sum(t.CREDIT)) < 0 then 0 else (sum(t.DEBIT) - sum(t.CREDIT)) end Credit ";
            query += " from tb_Transactions t  inner join tb_Ledgers l on t.ACCID = l.LEDGERID ";
            query += " inner join ChartOfAccounts c on l.UNDER = c.ACC_ID ";
            query += " where t.DATED between @startdate and @enddate and c.AccountCode like '04%' ";
            query += " group by c.AccountCode, c.ACC_ID, c.ACC_DESC, l.LEDGERID, l.LEDGERNAME ";
            query += " union all ";
            query += "Select @openingdate Dated, 'FYOpening' VoucherType, c.AccountCode, c.ACC_ID, c.ACC_DESC, l.LEDGERID, l.LEDGERNAME, -(sum(t.DEBIT) - sum(t.CREDIT)) Amount, ";
            query += " case when(sum(t.DEBIT) - sum(t.CREDIT)) > 0 then (sum(t.DEBIT) - sum(t.CREDIT)) else 0 end Debit, ";
            query += " case when(sum(t.DEBIT) - sum(t.CREDIT)) < 0 then -(sum(t.DEBIT) - sum(t.CREDIT)) else 0 end Credit ";
            query += " from tb_Transactions t  inner join tb_Ledgers l on t.ACCID = l.LEDGERID ";
            query += " inner join ChartOfAccounts c on l.UNDER = c.ACC_ID ";
            query += " where t.DATED between @startdate and @enddate and c.AccountCode like '04%' ";
            query += " group by c.AccountCode, c.ACC_ID, c.ACC_DESC, l.LEDGERID, l.LEDGERNAME ";



            Dictionary<string, object> parameters = new Dictionary<string, object>();
            Sys_Sols_Inventory.Class.CompanySetup comsetup = new Sys_Sols_Inventory.Class.CompanySetup();
            DataTable ds = comsetup.GetCurrentFinancialYear();
            parameters.Add("@startdate", ds.Rows[0]["SDate"]);
            parameters.Add("@enddate", ds.Rows[0]["EDate"]);
            parameters.Add("@openingdate", dateTimePicker3.Value);
            datatablebalancesheet1 = DbFunctions.GetDataTable(query, parameters);
            if (datatablebalancesheet1.Rows.Count == 0)
            {
                MessageBox.Show("No Entries Found");
                return;
            }

            TrialBalanceGrid.AutoGenerateColumns = true;
            TrialBalanceGrid.DataSource = datatablebalancesheet1;
            TrialBalanceGrid.Refresh();
            this.Refresh();
        }

        private void FinancialYeardataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedfinancialyear = (FinancialYear)financialyear.Where(f => f.FinaYearId == (int)FinancialYeardataGridView.CurrentRow.Cells["FinaYearId"].Value).FirstOrDefault();
if (selectedfinancialyear != null)
            {
                textBox3.Text = selectedfinancialyear.FinancialYearCode;
                dateTimePicker2.Value = selectedfinancialyear.SDate;
                dateTimePicker1.Value = selectedfinancialyear.EDate;
                checkBox2.Checked = selectedfinancialyear.Status;
                checkBox1.Checked = selectedfinancialyear.CurrentFY;
                textBox4.Text = selectedfinancialyear.NoSeriesSuffix;
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                MessageBox.Show("Enter Valid Financial Year Code");
                return;
            }
            if(dateTimePicker2.Value == null)
            {
                MessageBox.Show("Selected Starting Date for Financial Year");
                return;

            }
            if (dateTimePicker1.Value == null)
            {
                MessageBox.Show("Selected End Date for Financial Year");
                return;

            }

            //selectedfinancialyear = new FinancialYear();
            selectedfinancialyear.FinancialYearCode = textBox3.Text;
            selectedfinancialyear.SDate = dateTimePicker2.Value;
            selectedfinancialyear.EDate = dateTimePicker1.Value;
            selectedfinancialyear.Status = checkBox2.Checked;
            selectedfinancialyear.CurrentFY = checkBox1.Checked;
            selectedfinancialyear.NoSeriesSuffix = textBox4.Text;

            if (InsertUpdateMode == "Update")
            {
                selectedfinancialyear.Update();
            }
            if (InsertUpdateMode == "Insert")
            {
                selectedfinancialyear.Insert();

                InsertUpdateMode = "Update";
            }

            financialyear = new BindingList<FinancialYear>(GetAllFinancialYear());
            FinancialYeardataGridView.AutoGenerateColumns = true;
            FinancialYeardataGridView.DataSource = financialyear;

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dateTimePicker3.Value = financialyear.Where(f => f.FinaYearId == (int)comboBox1.SelectedValue).FirstOrDefault().SDate;
            dateTimePicker4.Value = financialyear.Where(f => f.FinaYearId == (int)comboBox1.SelectedValue).FirstOrDefault().EDate;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox3.Text = "";
            dateTimePicker2.Value = DateTime.Now;
            dateTimePicker1.Value = DateTime.Now;
            InsertUpdateMode = "Insert";
            textBox3.Focus();

        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            Sys_Sols_Inventory.Class.CompanySetup company = new Sys_Sols_Inventory.Class.CompanySetup();
            String Branch = company.ReadBranch();
            Sys_Sols_Inventory.Class.CompanySetup comsetup = new Sys_Sols_Inventory.Class.CompanySetup();
            DataTable ds = new DataTable();
            ds = comsetup.GetCurrentFinancialYear();

            foreach (DataRow dr in datatablebalancesheet1.Rows)
            {
                Transactions trans = new Transactions();
                trans.ACCID = dr["LEDGERID"].ToString();
                trans.ACCNAME = dr["LEDGERNAME"].ToString();
                trans.VOUCHERNO = this.textBox1.Text;
                trans.VOUCHERTYPE = dr["VOUCHERTYPE"].ToString();
               // trans.DATED = datatableincexp.Rows[0]["EDate"].ToString();
                trans.DATED = dr["DATED"].ToString();
                trans.CurrentDate = DateTime.Today;
                trans.SYSTEMTIME = DateTime.UtcNow.ToShortTimeString();
                trans.USERID = "Admin";
                trans.BRANCH = Branch;
                trans.DEBIT = dr["Debit"].ToString();
                trans.CREDIT = dr["Credit"].ToString();
                trans.NARRATION = this.textBox2.Text;
                trans.PARTICULARS = this.textBox2.Text;
                try
                {
                    trans.insertTransaction();
                }
                catch (Exception ex)
                {

                    throw ex;
                }



            }
            datatablebalancesheet1.Clear();
            TrialBalanceGrid.DataSource = datatablebalancesheet1;
            MessageBox.Show("Transaction carryforwarded to new FY successfully.");

        }

        private static List<FinancialYear>  GetAllFinancialYear()
    {
            string query = "Select * from tbl_FinancialYear";
            DataTable dt = DbFunctions.GetDataTable(query);
            List<FinancialYear> FYears = new List<FinancialYear>();
            foreach (DataRow dr in dt.Rows)
            {
                FinancialYear fy = new FinancialYear();
                fy.FinaYearId = Convert.ToInt16(dr["FinaYearId"]);
                fy.FinancialYearCode = dr["FinancialYearCode"] == DBNull.Value ? "" :  dr["FinancialYearCode"].ToString();
                fy.SDate = Convert.ToDateTime(dr["Sdate"]);
                fy.EDate = Convert.ToDateTime(dr["EDate"]);
                fy.AllowPostingFrom = dr["AllowPostingFrom"] == DBNull.Value ? Convert.ToDateTime(dr["Sdate"]) : Convert.ToDateTime(dr["AllowPostingFrom"]);
                fy.AllowPostingTill = dr["AllowPostingTill"] == DBNull.Value ? Convert.ToDateTime(dr["EDate"]) : Convert.ToDateTime(dr["AllowPostingTill"]);
                fy.Status = dr["Status"] == DBNull.Value ? false :  Convert.ToBoolean(dr["Status"]);
                fy.CurrentFY = Convert.ToBoolean(dr["CurrentFY"]);
                fy.NoSeriesSuffix = dr["NoSeriesSuffix"].ToString();
                FYears.Add(fy);
            }
            return FYears;
        }
    }
}
