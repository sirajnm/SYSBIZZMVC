using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace Sys_Sols_Inventory.reports
{
    public partial class Recievables_and_Payables : Form
    {
        Class.Transactions Tr = new Class.Transactions();
        double Liability = 0, Asset = 0;
        public string decpoint = "2";
        private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        private SqlCommand cmd = new SqlCommand();
        private SqlDataAdapter adapter = new SqlDataAdapter();
        private DataTable table = new DataTable();
        public Recievables_and_Payables()
        {
            InitializeComponent();
        }

        private void Find_Click(object sender, EventArgs e)
        {
            payables();
            recievables();
        }

        public void payables()
        {
            DataTable Accgrp = new DataTable();
            Accgrp = Tr.GetAccGroupLoop(12);
            int count = 0;
            for (int j = 0; j < Accgrp.Rows.Count; j++)
            {
                int grpid = Convert.ToInt16(Accgrp.Rows[j][0].ToString());
                DataTable dt = new DataTable();
                dt = Tr.GetLedgerLoop(grpid, 0);
                // DG_REPORT.DataSource = dt;
                double groupvalue = 0;
                if (dt.Rows.Count > 0)
                {
                    DataTable dt1 = new DataTable();
                    DataTable dt2 = new DataTable();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        dt1 = Tr.LedgerDebitCreditSum(Convert.ToInt32(dt.Rows[i][0]), Date_From.Value, Date_To.Value);
                        dt2 = Tr.LedgerDebitCreditSumDetails(Convert.ToInt32(dt.Rows[i][0]), Date_From.Value, Date_To.Value);
                        if (dt1.Rows.Count > 0 && dt1.Rows[0][0].ToString() != "")
                        {
                            string a = dt1.Rows[0][0].ToString();
                            groupvalue = groupvalue + Convert.ToDouble(dt1.Rows[0][0].ToString());

                        }
                        if (i < dt2.Rows.Count)
                        {
                            dg_payables.Rows.Add(dt2.Rows[i]["ACCNAME"], dt2.Rows[i]["bal"], "");
                        }
                    }

                    dg_payables.Rows.Add("", "  " + Accgrp.Rows[j][1].ToString(), groupvalue.ToString("n2"));
                    count++;
                }
                Liability = Liability + groupvalue;
            }

            dg_payables.Rows.Insert(dg_payables.Rows.Count, "", "Total Payables", Liability.ToString("n2"));
            dg_payables.Rows[dg_payables.Rows.Count - count - 1].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dg_payables.Rows[dg_payables.Rows.Count - 1].DefaultCellStyle.ForeColor = System.Drawing.Color.Red;
        }
        
        public void BindSettings()
        {
            DataTable dt = new DataTable();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = "SELECT * FROM SYS_SETUP";
            adapter.SelectCommand = cmd;
            adapter.Fill(dt);


            if (dt.Rows.Count > 0)
            {
                decpoint = Convert.ToString(dt.Rows[0]["Dec_qty"]);
            }
        }

        public void recievables()
        {
            DataTable Accgrp = new DataTable();
            Accgrp = Tr.GetAccGroupLoop(9);
            int count = 0;
            for (int j = 0; j < Accgrp.Rows.Count; j++)
            {
                int grpid = Convert.ToInt16(Accgrp.Rows[j][0].ToString());
                DataTable dt = new DataTable();
                dt = Tr.GetLedgerLoop(grpid, 0);
                double groupvalue = 0;
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataTable dt1 = new DataTable();
                        DataTable dt2 = new DataTable();
                        dt1 = Tr.LedgerDebitCreditSum(Convert.ToInt32(dt.Rows[i][0]), Date_From.Value, Date_To.Value);
                        dt2 = Tr.LedgerDebitCreditSumDetails(Convert.ToInt32(dt.Rows[i][0]), Date_From.Value, Date_To.Value);
                        if (dt1.Rows.Count > 0 && dt1.Rows[0][0].ToString() != "")
                        {
                            string a = dt1.Rows[0][0].ToString();
                            groupvalue = groupvalue + Convert.ToDouble(dt1.Rows[0][0].ToString());
                        }
                        if (i < dt2.Rows.Count)
                        {
                            dg_recieble.Rows.Add(dt2.Rows[i]["ACCNAME"], dt2.Rows[i]["bal"], "");
                        }
                    }
                    dg_recieble.Rows.Add("", "  " + Accgrp.Rows[j][1].ToString(), groupvalue.ToString("n2"));
                    count++;
                }
                Asset = Asset + groupvalue;
            }
            dg_recieble.Rows.Insert(dg_recieble.Rows.Count, "", "Total Recievbles", Asset.ToString("n2"));
            dg_recieble.Rows[dg_recieble.Rows.Count - count - 1].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dg_recieble.Rows[dg_payables.Rows.Count - 1].DefaultCellStyle.ForeColor = System.Drawing.Color.Red;
        }

        private void Find_Click_1(object sender, EventArgs e)
        {
            bind_grid();
            //net_profit.Text = "";
            //try
            //{
            //    Liability = 0;
            //    Asset = 0;
            //    dg_recieble.Rows.Clear();
            //    dg_payables.Rows.Clear();
            //    payables();
            //    sum();
            //    recievables(); 
            //    dg_payables.Columns["b"].DefaultCellStyle.Format = "N" + decpoint;
            //    dg_payables.Columns["c"].DefaultCellStyle.Format = "N" + decpoint;
            //    dg_recieble.Columns["e"].DefaultCellStyle.Format = "N" + decpoint;
            //    dg_recieble.Columns["f"].DefaultCellStyle.Format = "N" + decpoint;
            //    dg_payables.Columns["b"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
            //    dg_payables.Columns["c"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
            //    dg_recieble.Columns["e"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
            //    dg_recieble.Columns["f"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
            //}
            //catch
            //{
            //}  
        }

        public void bind_grid()
        {
            net_profit.Text = "";
            Liability = 0;
            Asset = 0;
            dg_recieble.Rows.Clear();
            dg_payables.Rows.Clear();
            payables();
            sum();
            recievables();
            dg_payables.Columns["b"].DefaultCellStyle.Format = "N" + decpoint;
            dg_payables.Columns["c"].DefaultCellStyle.Format = "N" + decpoint;
            dg_recieble.Columns["e"].DefaultCellStyle.Format = "N" + decpoint;
            dg_recieble.Columns["f"].DefaultCellStyle.Format = "N" + decpoint;
            dg_payables.Columns["b"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
            dg_payables.Columns["c"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
            dg_recieble.Columns["e"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
            dg_recieble.Columns["f"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
        }

        public void sum()
        {
            if (Liability < Asset)
            {
                net_profit.Text = Convert.ToString(Liability + Asset);
            }
            else
            {
                net_profit.Text = Convert.ToString(Asset + Liability);
            }
        }

        private void cb_datserch_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_datserch.Checked)
            {
                Date_From.Enabled = true;
                Date_To.Enabled = true;
            }
            else
            {
                Date_From.Enabled = false;
                Date_To.Enabled = false;
            }

        }

        private void Recievables_and_Payables_Load(object sender, EventArgs e)
        {
            BindSettings();
            bind_grid();
        }
        
        public void Hiding_Payables()
        {
            dg_payables.Visible = false;
            lb_payables.Visible = false;
           // dg_recieble.Location = new Point(panel3.Location.X+dg_payables.Location.X,panel3.Location.Y+dg_payables.Location.Y);
            lb_recievable.Location = new Point(lb_payables.Location.X, lb_payables.Location.Y);
        }
        
        public void Hiding_Recievables()
        {
            dg_recieble.Visible = false;
            lb_recievable.Visible = false;
        }
 
    }
}
