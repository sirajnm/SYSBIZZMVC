using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using System.Drawing.Printing;
using System.Globalization;
using System.Threading;
using System.Net;
using System.Drawing.Imaging;
using System.IO;
using System.Text.RegularExpressions;

namespace Sys_Sols_Inventory
{
    public partial class CustomerSummary : Form
    {
        //SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        bool PAGETOTAL = false;
        public int printeditems = 0;
        int m = 0;
        int b = 0;
        double totalbalance = 0;
        bool is_customer;
        public CustomerSummary(bool i)
        {
            InitializeComponent();
            is_customer = i;
        }
        double TotamAmount = 0;
        int ledgerid;
        DateTime maxdate;
        double maxcreditamount = 0;



        public void customerSummary()
        {
            if (is_customer)
            {
                #region
                //SHAFEEK CODE

                //SqlCommand cmd = new SqlCommand("SELECT LedgerId FROM REC_CUSTOMER", conn);
                //SqlDataAdapter sda = new SqlDataAdapter(cmd);
                //DataTable dt = new DataTable();
                //sda.Fill(dt);
                //if (dt.Rows.Count > 0)
                //{
                //    for (int i = 0; i < dt.Rows.Count; i++)
                //    {
                //        ledgerid = Convert.ToInt32(dt.Rows[i]["LedgerId"].ToString());
                //        double sumdebit = 0;
                //        double sumcredit = 0;
                //        double balance = 0;

                //        dataGridView1.Rows.Add("", "", "", "");
                //        SqlCommand cmd1 = new SqlCommand("SELECT ACCNAME,DEBIT,DATED,CREDIT FROM tb_Transactions WHERE ACCID='" + dt.Rows[i]["LedgerId"].ToString() + "' ORDER BY DATED", conn);
                //        SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                //        DataTable dt1 = new DataTable();
                //        sda1.Fill(dt1);

                //        for (int j = 0; j < dt1.Rows.Count; j++)
                //        {
                //            // maxdate = dt1.Rows[j]["DATED"].ToString();
                //            dataGridView1.Rows[i].Cells["accname"].Value = dt1.Rows[j]["ACCNAME"].ToString();
                //            double debit = Convert.ToDouble(dt1.Rows[j]["DEBIT"].ToString());
                //            double credit = Convert.ToDouble(dt1.Rows[j]["CREDIT"].ToString());
                //            // string date = dt1.Rows[j]["DATED"].ToString();
                //            sumdebit = sumdebit + debit;
                //            sumcredit = sumcredit + credit;
                //            balance = sumdebit - sumcredit;
                //            dataGridView1.Rows[i].Cells["debit"].Value = sumdebit;
                //            dataGridView1.Rows[i].Cells["credit"].Value = sumcredit;
                //            dataGridView1.Rows[i].Cells["balance"].Value = balance;
                //            //dataGridView1.Rows[i].Cells["lastpaydate"].Value = date;
                //            //dataGridView1.Rows[i].Cells["lastpayamount"].Value = credit;
                //            SqlCommand cmd2 = new SqlCommand("SELECT DATED,CREDIT FROM tb_Transactions WHERE ACCID='" + dt.Rows[i]["LedgerId"].ToString() + "' AND VOUCHERTYPE!='SALES Normal' ORDER BY DATED", conn);
                //            SqlDataAdapter sda2 = new SqlDataAdapter(cmd2);
                //            DataTable dt2 = new DataTable();
                //            sda2.Fill(dt2);
                //            for (int k = 0; k < dt2.Rows.Count; k++)
                //            {
                //                string date = dt2.Rows[k]["DATED"].ToString();
                //                double credit1 = Convert.ToDouble(dt2.Rows[k]["CREDIT"].ToString());
                //                dataGridView1.Rows[i].Cells["lastpaydate"].Value = date;
                //                dataGridView1.Rows[i].Cells["lastpayamount"].Value = credit1;
                //            }



                //        }


                //    }
                //}
                //for (int k = 0; k < dataGridView1.Rows.Count; k++)
                //{

                //    int a = Convert.ToInt32(dataGridView1.Rows[k].Cells["balance"].Value);
                //    b = b + a;
                //    textBox1.Text = b.ToString();
                //}
                #endregion

                string qry = "";
                if (!checkBox1.Checked)
                {
                    if (dataGridView2.ColumnCount > 0)
                        dataGridView2.Columns.Clear();
                    
                    //SqlCommand cmd = new SqlCommand("SELECT REC_CUSTOMER.DESC_ENG AS NAME,CONVERT(VARCHAR,LAST_PAY.DATED,103) AS 'LAST PAY DATE',LAST_PAY.LAST_AMOUNT AS 'LAST PAY AMOUNT',BALANCE.DEBIT,BALANCE.CREDIT,BALANCE.DEBIT-BALANCE.CREDIT AS BALANCE FROM (SELECT ACCID,SUM(tb_Transactions.DEBIT) AS DEBIT,SUM(tb_Transactions.CREDIT) AS CREDIT FROM tb_Transactions LEFT OUTER JOIN REC_CUSTOMER ON REC_CUSTOMER.Ledgerid=tb_Transactions.ACCID WHERE REC_CUSTOMER.CODE IS NOT NULL GROUP BY ACCID) AS BALANCE LEFT OUTER JOIN REC_CUSTOMER ON REC_CUSTOMER.LedgerId=BALANCE.ACCID LEFT OUTER JOIN (SELECT TB1.ACCID,TB1.CREDIT AS 'LAST_AMOUNT',CONVERT(VARCHAR,DATED,103) AS 'DATED'  FROM (SELECT TRANSACTIONID,ACCID,ACCNAME,tb_transactions.CREDIT,DATED FROM tb_transactions INNER JOIN REC_CUSTOMER ON REC_CUSTOMER.LEDGERID=ACCID) AS TB1 INNER JOIN (SELECT ACCID,MAX(TRANSACTIONID)AS TRANSACTION_ID FROM tb_transactions WHERE DATED IN(SELECT MAX(DATED) FROM tb_Transactions WHERE CREDIT>0 GROUP BY ACCID) AND CREDIT>0 GROUP BY ACCID) AS TB2 ON TB1.TRANSACTIONID=TB2.TRANSACTION_ID) AS LAST_PAY ON LAST_PAY.ACCID=BALANCE.ACCID WHERE REC_CUSTOMER.SALESMAN_CODE LIKE '%" + cmb_salesman.SelectedValue + "%' AND REC_CUSTOMER.DESC_ENG LIKE '%" + textBox2.Text + "%'", conn);
                    //SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    //sda.Fill(dt);
                    //qry="SELECT REC_CUSTOMER.DESC_ENG AS NAME,CONVERT(VARCHAR,LAST_PAY.DATED,103) AS 'LAST PAY DATE',LAST_PAY.LAST_AMOUNT AS 'LAST PAY AMOUNT',BALANCE.DEBIT,BALANCE.CREDIT,BALANCE.DEBIT-BALANCE.CREDIT AS BALANCE FROM (SELECT ACCID,SUM(tb_Transactions.DEBIT) AS DEBIT,SUM(tb_Transactions.CREDIT) AS CREDIT FROM tb_Transactions LEFT OUTER JOIN REC_CUSTOMER ON REC_CUSTOMER.Ledgerid=tb_Transactions.ACCID WHERE REC_CUSTOMER.CODE IS NOT NULL GROUP BY ACCID) AS BALANCE LEFT OUTER JOIN REC_CUSTOMER ON REC_CUSTOMER.LedgerId=BALANCE.ACCID LEFT OUTER JOIN (SELECT TB1.ACCID,TB1.CREDIT AS 'LAST_AMOUNT',CONVERT(VARCHAR,DATED,103) AS 'DATED'  FROM (SELECT TRANSACTIONID,ACCID,ACCNAME,tb_transactions.CREDIT,DATED FROM tb_transactions INNER JOIN REC_CUSTOMER ON REC_CUSTOMER.LEDGERID=ACCID) AS TB1 INNER JOIN (SELECT ACCID,MAX(TRANSACTIONID)AS TRANSACTION_ID FROM tb_transactions WHERE DATED IN(SELECT MAX(DATED) FROM tb_Transactions WHERE CREDIT>0 GROUP BY ACCID) AND CREDIT>0 GROUP BY ACCID) AS TB2 ON TB1.TRANSACTIONID=TB2.TRANSACTION_ID) AS LAST_PAY ON LAST_PAY.ACCID=BALANCE.ACCID WHERE REC_CUSTOMER.SALESMAN_CODE LIKE '%" + cmb_salesman.SelectedValue + "%' AND REC_CUSTOMER.DESC_ENG LIKE '%" + textBox2.Text + "%'";
                    qry= "SELECT REC_CUSTOMER.DESC_ENG AS NAME,CONVERT(VARCHAR,LAST_PAY.DATED,103) AS 'LAST PAY DATE',LAST_PAY.LAST_AMOUNT AS 'LAST PAY AMOUNT',LAST_BILL.[LAST INVOICE],CONVERT(VARCHAR,LAST_BILL.INV_DATE,103) AS [INV DATE],BALANCE.DEBIT,BALANCE.CREDIT,BALANCE.DEBIT-BALANCE.CREDIT AS BALANCE FROM (SELECT ACCID,SUM(tb_Transactions.DEBIT) AS DEBIT,SUM(tb_Transactions.CREDIT) AS CREDIT FROM tb_Transactions LEFT OUTER JOIN REC_CUSTOMER ON REC_CUSTOMER.Ledgerid=tb_Transactions.ACCID WHERE REC_CUSTOMER.CODE IS NOT NULL GROUP BY ACCID) AS BALANCE LEFT OUTER JOIN REC_CUSTOMER ON REC_CUSTOMER.LedgerId=BALANCE.ACCID LEFT OUTER JOIN (SELECT TB1.ACCID,TB1.CREDIT AS 'LAST_AMOUNT',CONVERT(VARCHAR,DATED,103) AS 'DATED'  FROM (SELECT TRANSACTIONID,ACCID,ACCNAME,tb_transactions.CREDIT,DATED FROM tb_transactions INNER JOIN REC_CUSTOMER ON REC_CUSTOMER.LEDGERID=ACCID) AS TB1 INNER JOIN (SELECT ACCID,MAX(TRANSACTIONID)AS TRANSACTION_ID FROM tb_transactions WHERE DATED IN(SELECT MAX(DATED) FROM tb_Transactions WHERE CREDIT>0 GROUP BY ACCID) AND CREDIT>0 GROUP BY ACCID) AS TB2 ON TB1.TRANSACTIONID=TB2.TRANSACTION_ID) AS LAST_PAY ON LAST_PAY.ACCID=BALANCE.ACCID LEFT OUTER JOIN (SELECT T1.[LAST INVOICE],T1.DATE AS INV_DATE, REC_CUSTOMER.LedgerId FROM (SELECT MAX(DOC_ID) AS [LAST INVOICE],MAX(DOC_DATE_GRE) AS [DATE],CUSTOMER_CODE FROM INV_SALES_HDR GROUP BY CUSTOMER_CODE) T1 INNER JOIN REC_CUSTOMER ON T1.CUSTOMER_CODE=REC_CUSTOMER.CODE) AS LAST_BILL ON LAST_BILL.LedgerId=BALANCE.ACCID WHERE REC_CUSTOMER.SALESMAN_CODE LIKE '%" + cmb_salesman.SelectedValue + "%' AND REC_CUSTOMER.DESC_ENG LIKE '%" + textBox2.Text + "%' AND (BALANCE.CREDIT<>0.00 OR BALANCE.DEBIT<>0.00)"; 
                    dt =Model.DbFunctions.GetDataTable(qry);

                    dt.DefaultView.Sort = "NAME ASC";
                    dt = dt.DefaultView.ToTable();
                    string query = "SELECT SUM(ISNULL(BALANCE.DEBIT,0))-SUM(ISNULL(BALANCE.CREDIT,0)) AS BALANCE,SUM(ISNULL(BALANCE.DEBIT,0)) AS DEBIT_TOTAL,SUM(ISNULL(BALANCE.CREDIT,0)) AS CREDIT_TOTAL FROM (SELECT ACCID,SUM(tb_Transactions.DEBIT) AS DEBIT,SUM(tb_Transactions.CREDIT) AS CREDIT FROM tb_Transactions LEFT OUTER JOIN REC_CUSTOMER ON REC_CUSTOMER.Ledgerid=tb_Transactions.ACCID WHERE REC_CUSTOMER.CODE IS NOT NULL GROUP BY ACCID) AS BALANCE LEFT OUTER JOIN REC_CUSTOMER ON REC_CUSTOMER.LedgerId=BALANCE.ACCID LEFT OUTER JOIN (SELECT ACCID,MAX(tb_Transactions.DATED) AS DATED,SUM(tb_Transactions.DEBIT) AS LAST_AMOUNT FROM tb_transactions  INNER JOIN REC_CUSTOMER ON REC_CUSTOMER.LedgerId=tb_Transactions.ACCID GROUP BY ACCID) AS LAST_PAY ON LAST_PAY.ACCID=BALANCE.ACCID WHERE REC_CUSTOMER.SALESMAN_CODE LIKE '%" + cmb_salesman.SelectedValue + "%' AND REC_CUSTOMER.DESC_ENG LIKE '%" + textBox2.Text + "%'";
                    //SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                    DataTable dt1 = new DataTable();
                    //sda1.Fill(dt1);
                    dt1 = Model.DbFunctions.GetDataTable(query);

                    if (dt.Rows.Count > 0)
                    {
                        if (dt1.Rows.Count > 0)
                        {
                            //textBox1.Text = dt1.Rows[0][0].ToString();
                            //DataRow row = dt.NewRow();
                            //dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells["NAME"].Value = "Total";
                            //dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells["DEBIT"].Value = dt1.Rows[0][1].ToString();
                            //dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells["CREDIT"].Value = dt1.Rows[0][2].ToString();
                            //dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells["BALANCE"].Value = dt1.Rows[0][0].ToString();
                            ////dt.Rows.InsertAt(row, dt.Rows.Count);
                            ////dataGridView2.DataSource = dt;
                            //dataGridView2.Columns[0].Width = 400;

                            textBox1.Text = dt1.Rows[0][0].ToString();
                            DataRow row = dt.NewRow();
                            row["NAME"] = "Total";
                            row["DEBIT"] = dt1.Rows[0][1].ToString();
                            row["CREDIT"] = dt1.Rows[0][2].ToString();
                            row["BALANCE"] = dt1.Rows[0][0].ToString();
                            dt.Rows.InsertAt(row, dt.Rows.Count);
                            dataGridView2.DataSource = dt;
                            dataGridView2.Columns[0].Width = 400;
                        }

                    }
                    else
                        textBox1.Clear();

                }
                else
                {

                    if (dataGridView2.ColumnCount > 0)
                        dataGridView2.Columns.Clear();

                    //SqlCommand cmd = new SqlCommand("SELECT REC_CUSTOMER.DESC_ENG AS NAME,CONVERT(VARCHAR,LAST_PAY.DATED,103) AS 'LAST PAY DATE',LAST_PAY.LAST_AMOUNT AS 'LAST PAY AMOUNT',BALANCE.DEBIT,BALANCE.CREDIT,BALANCE.DEBIT-BALANCE.CREDIT AS BALANCE FROM (SELECT ACCID,SUM(tb_Transactions.DEBIT) AS DEBIT,SUM(tb_Transactions.CREDIT) AS CREDIT FROM tb_Transactions LEFT OUTER JOIN REC_CUSTOMER ON REC_CUSTOMER.Ledgerid=tb_Transactions.ACCID WHERE tb_Transactions.DATED BETWEEN @d1 and @d2 and REC_CUSTOMER.CODE IS NOT NULL GROUP BY ACCID) AS BALANCE LEFT OUTER JOIN REC_CUSTOMER ON REC_CUSTOMER.LedgerId=BALANCE.ACCID LEFT OUTER JOIN (SELECT TB1.ACCID,TB1.CREDIT AS 'LAST_AMOUNT',CONVERT(VARCHAR,DATED,103) AS 'DATED'  FROM (SELECT TRANSACTIONID,ACCID,ACCNAME,tb_transactions.CREDIT,DATED FROM tb_transactions INNER JOIN REC_CUSTOMER ON REC_CUSTOMER.LEDGERID=ACCID) AS TB1 INNER JOIN (SELECT ACCID,MAX(TRANSACTIONID)AS TRANSACTION_ID FROM tb_transactions WHERE DATED IN(SELECT MAX(DATED) FROM tb_Transactions WHERE CREDIT>0 and tb_transactions.DATED BETWEEN @d1 and @d2 GROUP BY ACCID) AND CREDIT>0 GROUP BY ACCID) AS TB2 ON TB1.TRANSACTIONID=TB2.TRANSACTION_ID) AS LAST_PAY ON LAST_PAY.ACCID=BALANCE.ACCID WHERE REC_CUSTOMER.SALESMAN_CODE LIKE '%" + cmb_salesman.SelectedValue + "%' AND REC_CUSTOMER.DESC_ENG LIKE '%" + textBox2.Text + "%'", conn);
                    //cmd.Parameters.Clear();
                    //cmd.Parameters.Add("@d1", SqlDbType.Date).Value = dateFrom.Value;
                    //cmd.Parameters.Add("@d2", SqlDbType.Date).Value =dateTo.Value;
                    //SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    //sda.Fill(dt);

                    //   qry = "SELECT REC_CUSTOMER.DESC_ENG AS NAME,CONVERT(VARCHAR,LAST_PAY.DATED,103) AS 'LAST PAY DATE',LAST_PAY.LAST_AMOUNT AS 'LAST PAY AMOUNT',BALANCE.DEBIT,BALANCE.CREDIT,BALANCE.DEBIT-BALANCE.CREDIT AS BALANCE FROM (SELECT ACCID,SUM(tb_Transactions.DEBIT) AS DEBIT,SUM(tb_Transactions.CREDIT) AS CREDIT FROM tb_Transactions LEFT OUTER JOIN REC_CUSTOMER ON REC_CUSTOMER.Ledgerid=tb_Transactions.ACCID WHERE (CONVERT(VARCHAR, tb_Transactions.DATED,101)) BETWEEN @d1 and @d2 and REC_CUSTOMER.CODE IS NOT NULL GROUP BY ACCID) AS BALANCE LEFT OUTER JOIN REC_CUSTOMER ON REC_CUSTOMER.LedgerId=BALANCE.ACCID LEFT OUTER JOIN (SELECT TB1.ACCID,TB1.CREDIT AS 'LAST_AMOUNT',CONVERT(VARCHAR,DATED,103) AS 'DATED'  FROM (SELECT TRANSACTIONID,ACCID,ACCNAME,tb_transactions.CREDIT,DATED FROM tb_transactions INNER JOIN REC_CUSTOMER ON REC_CUSTOMER.LEDGERID=ACCID) AS TB1 INNER JOIN (SELECT ACCID,MAX(TRANSACTIONID)AS TRANSACTION_ID FROM tb_transactions WHERE DATED IN(SELECT MAX(DATED) FROM tb_Transactions WHERE CREDIT>0 and (CONVERT(VARCHAR, tb_Transactions.DATED,101)) BETWEEN @d1 and @d2 GROUP BY ACCID) AND CREDIT>0 GROUP BY ACCID) AS TB2 ON TB1.TRANSACTIONID=TB2.TRANSACTION_ID) AS LAST_PAY ON LAST_PAY.ACCID=BALANCE.ACCID WHERE REC_CUSTOMER.SALESMAN_CODE LIKE '%" + cmb_salesman.SelectedValue + "%' AND REC_CUSTOMER.DESC_ENG LIKE '%" + textBox2.Text + "%'";
                   // qry = "SELECT REC_CUSTOMER.DESC_ENG AS NAME,CONVERT(VARCHAR,LAST_PAY.DATED,103) AS 'LAST PAY DATE',LAST_PAY.LAST_AMOUNT AS 'LAST PAY AMOUNT',BALANCE.DEBIT,BALANCE.CREDIT,BALANCE.DEBIT-BALANCE.CREDIT AS BALANCE FROM (SELECT ACCID,SUM(tb_Transactions.DEBIT) AS DEBIT,SUM(tb_Transactions.CREDIT) AS CREDIT FROM tb_Transactions LEFT OUTER JOIN REC_CUSTOMER ON REC_CUSTOMER.Ledgerid=tb_Transactions.ACCID WHERE (CONVERT(VARCHAR, tb_Transactions.DATED,101)) >= @d1 and (CONVERT(VARCHAR, tb_Transactions.DATED,101))<=@d2 and REC_CUSTOMER.CODE IS NOT NULL GROUP BY ACCID) AS BALANCE LEFT OUTER JOIN REC_CUSTOMER ON REC_CUSTOMER.LedgerId=BALANCE.ACCID LEFT OUTER JOIN (SELECT TB1.ACCID,TB1.CREDIT AS 'LAST_AMOUNT',CONVERT(VARCHAR,DATED,103) AS 'DATED'  FROM (SELECT TRANSACTIONID,ACCID,ACCNAME,tb_transactions.CREDIT,DATED FROM tb_transactions INNER JOIN REC_CUSTOMER ON REC_CUSTOMER.LEDGERID=ACCID) AS TB1 INNER JOIN (SELECT ACCID,MAX(TRANSACTIONID)AS TRANSACTION_ID FROM tb_transactions WHERE DATED IN(SELECT MAX(DATED) FROM tb_Transactions WHERE CREDIT>0 and (CONVERT(VARCHAR, tb_Transactions.DATED,101)) >= @d1 and (CONVERT(VARCHAR, tb_Transactions.DATED,101))<= @d2 GROUP BY ACCID) AND CREDIT>0 GROUP BY ACCID) AS TB2 ON TB1.TRANSACTIONID=TB2.TRANSACTION_ID) AS LAST_PAY ON LAST_PAY.ACCID=BALANCE.ACCID WHERE REC_CUSTOMER.SALESMAN_CODE LIKE '%" + cmb_salesman.SelectedValue + "%' AND REC_CUSTOMER.DESC_ENG LIKE '%" + textBox2.Text + "%'";
                    qry = "SELECT REC_CUSTOMER.DESC_ENG AS NAME,CONVERT(VARCHAR,LAST_PAY.DATED,103) AS 'LAST PAY DATE',LAST_PAY.LAST_AMOUNT AS 'LAST PAY AMOUNT',LAST_BILL.[LAST INVOICE],CONVERT(VARCHAR,LAST_BILL.INV_DATE,103) AS [INV DATE],BALANCE.DEBIT,BALANCE.CREDIT,BALANCE.DEBIT-BALANCE.CREDIT AS BALANCE FROM (SELECT ACCID,SUM(tb_Transactions.DEBIT) AS DEBIT,SUM(tb_Transactions.CREDIT) AS CREDIT FROM tb_Transactions LEFT OUTER JOIN REC_CUSTOMER ON REC_CUSTOMER.Ledgerid=tb_Transactions.ACCID WHERE (CONVERT(VARCHAR, tb_Transactions.DATED,101)) >= @d1 and (CONVERT(VARCHAR, tb_Transactions.DATED,101))<=@d2 and REC_CUSTOMER.CODE IS NOT NULL GROUP BY ACCID) AS BALANCE LEFT OUTER JOIN REC_CUSTOMER ON REC_CUSTOMER.LedgerId=BALANCE.ACCID LEFT OUTER JOIN (SELECT TB1.ACCID,TB1.CREDIT AS 'LAST_AMOUNT',CONVERT(VARCHAR,DATED,103) AS 'DATED'  FROM (SELECT TRANSACTIONID,ACCID,ACCNAME,tb_transactions.CREDIT,DATED FROM tb_transactions INNER JOIN REC_CUSTOMER ON REC_CUSTOMER.LEDGERID=ACCID) AS TB1 INNER JOIN (SELECT ACCID,MAX(TRANSACTIONID)AS TRANSACTION_ID FROM tb_transactions WHERE DATED IN(SELECT MAX(DATED) FROM tb_Transactions WHERE CREDIT>0 GROUP BY ACCID) AND CREDIT>0 and (CONVERT(VARCHAR, tb_Transactions.DATED,101)) >= @d1 and (CONVERT(VARCHAR, tb_Transactions.DATED,101))<= @d2 GROUP BY ACCID) AS TB2 ON TB1.TRANSACTIONID=TB2.TRANSACTION_ID) AS LAST_PAY ON LAST_PAY.ACCID=BALANCE.ACCID LEFT OUTER JOIN (SELECT T1.[LAST INVOICE],T1.DATE AS INV_DATE, REC_CUSTOMER.LedgerId FROM (SELECT MAX(DOC_ID) AS [LAST INVOICE],MAX(DOC_DATE_GRE) AS [DATE],CUSTOMER_CODE FROM INV_SALES_HDR GROUP BY CUSTOMER_CODE) T1 INNER JOIN REC_CUSTOMER ON T1.CUSTOMER_CODE=REC_CUSTOMER.CODE) AS LAST_BILL ON LAST_BILL.LedgerId=BALANCE.ACCID WHERE REC_CUSTOMER.SALESMAN_CODE LIKE '%%' AND REC_CUSTOMER.DESC_ENG LIKE '%%' AND (BALANCE.CREDIT<>0.00 OR BALANCE.DEBIT<>0.00)";
                    Dictionary<string ,object>Parameters=new Dictionary<string,object> ();
                   // Parameters.Add("@d1", dateFrom.Value);
                    Parameters.Add("@d1", Convert.ToDateTime(dateFrom.Value.ToShortDateString()));
                   // Parameters.Add("@d2", dateTo.Value);
                    Parameters.Add("@d2", Convert.ToDateTime(dateTo.Value.ToShortDateString()));
                     dt = Model.DbFunctions.GetDataTable(qry, Parameters);
                     dt.DefaultView.Sort = "NAME ASC";
                     dt = dt.DefaultView.ToTable();
                    
                    if (dt.Rows.Count > 0)
                    {
                        dataGridView2.DataSource = dt;
                        dataGridView2.Columns[0].Width = 400;
                    }
                    else
                        textBox1.Clear();

                    //SqlCommand cmd1 = new SqlCommand("SELECT SUM(ISNULL(BALANCE.DEBIT,0))-SUM(ISNULL(BALANCE.CREDIT,0)) AS BALANCE,SUM(ISNULL(BALANCE.DEBIT,0)) AS DEBIT_TOTAL,SUM(ISNULL(BALANCE.CREDIT,0)) AS CREDIT_TOTAL FROM (SELECT ACCID,SUM(tb_Transactions.DEBIT) AS DEBIT,SUM(tb_Transactions.CREDIT) AS CREDIT FROM tb_Transactions LEFT OUTER JOIN REC_CUSTOMER ON REC_CUSTOMER.Ledgerid=tb_Transactions.ACCID WHERE REC_CUSTOMER.CODE IS NOT NULL and tb_transactions.DATED BETWEEN @d1 and @d2  GROUP BY ACCID) AS BALANCE LEFT OUTER JOIN REC_CUSTOMER ON REC_CUSTOMER.LedgerId=BALANCE.ACCID LEFT OUTER JOIN (SELECT ACCID,MAX(tb_Transactions.DATED) AS DATED,SUM(tb_Transactions.DEBIT) AS LAST_AMOUNT FROM tb_transactions  INNER JOIN REC_CUSTOMER ON REC_CUSTOMER.LedgerId=tb_Transactions.ACCID GROUP BY ACCID) AS LAST_PAY ON LAST_PAY.ACCID=BALANCE.ACCID WHERE REC_CUSTOMER.SALESMAN_CODE LIKE '%" + cmb_salesman.SelectedValue + "%' AND REC_CUSTOMER.DESC_ENG LIKE '%" + textBox2.Text + "%'", conn);
                    //cmd1.Parameters.Clear();
                    //cmd1.Parameters.Add("@d1", SqlDbType.Date).Value = dateFrom.Value;
                    //cmd1.Parameters.Add("@d2", SqlDbType.Date).Value = dateTo.Value;
                    //SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                    DataTable dt1 = new DataTable();
                    //sda1.Fill(dt1);
                    string qry1 = "SELECT SUM(ISNULL(BALANCE.DEBIT,0))-SUM(ISNULL(BALANCE.CREDIT,0)) AS BALANCE,SUM(ISNULL(BALANCE.DEBIT,0)) AS DEBIT_TOTAL,SUM(ISNULL(BALANCE.CREDIT,0)) AS CREDIT_TOTAL FROM (SELECT ACCID,SUM(tb_Transactions.DEBIT) AS DEBIT,SUM(tb_Transactions.CREDIT) AS CREDIT FROM tb_Transactions LEFT OUTER JOIN REC_CUSTOMER ON REC_CUSTOMER.Ledgerid=tb_Transactions.ACCID WHERE REC_CUSTOMER.CODE IS NOT NULL and ((CONVERT(VARCHAR, tb_Transactions.DATED,101)) >= @d1 and (CONVERT(VARCHAR, tb_Transactions.DATED,101))<=@d2)  GROUP BY ACCID) AS BALANCE LEFT OUTER JOIN REC_CUSTOMER ON REC_CUSTOMER.LedgerId=BALANCE.ACCID LEFT OUTER JOIN (SELECT ACCID,MAX(tb_Transactions.DATED) AS DATED,SUM(tb_Transactions.DEBIT) AS LAST_AMOUNT FROM tb_transactions  INNER JOIN REC_CUSTOMER ON REC_CUSTOMER.LedgerId=tb_Transactions.ACCID GROUP BY ACCID) AS LAST_PAY ON LAST_PAY.ACCID=BALANCE.ACCID WHERE REC_CUSTOMER.SALESMAN_CODE LIKE '%" + cmb_salesman.SelectedValue + "%' AND REC_CUSTOMER.DESC_ENG LIKE '%" + textBox2.Text + "%'";
                    Dictionary<string ,object>Parameter=new Dictionary<string,object> ();
                    Parameter.Add("@d1", Convert.ToDateTime(dateFrom.Value.ToShortDateString()));
                    Parameter.Add("@d2", Convert.ToDateTime(dateTo.Value.ToShortDateString()));
                    dt1 = Model.DbFunctions.GetDataTable(qry1, Parameter);
                    if (dt.Rows.Count > 0)
                    {
                        if (dt1.Rows.Count > 0)
                        {
                            textBox1.Text = dt1.Rows[0][0].ToString();
                            DataRow row = dt.NewRow();
                            row["NAME"] = "Total";
                            row["DEBIT"] = dt1.Rows[0][1].ToString();
                            row["CREDIT"] = dt1.Rows[0][2].ToString();
                            row["BALANCE"] = dt1.Rows[0][0].ToString();
                            dt.Rows.InsertAt(row, dt.Rows.Count);
                            dataGridView2.DataSource = dt;
                            dataGridView2.Columns[0].Width = 400;
                        }
                        dataGridView2.DataSource = dt;
                        dataGridView2.Columns[0].Width = 400;
                    }
                    else
                        textBox1.Clear();

                }

            }
            else
            {
                string query="";
                if (!checkBox1.Checked)
                {
                    if (dataGridView2.ColumnCount > 0)
                        dataGridView2.Columns.Clear();
                    // SqlCommand cmd = new SqlCommand("SELECT PAY_SUPPLIER.DESC_ENG AS NAME,CONVERT(VARCHAR,LAST_PAY.DATED,103) AS 'LAST PAY DATE',LAST_PAY.LAST_AMOUNT AS 'LAST PAY AMOUNT',BALANCE.CREDIT,BALANCE.DEBIT,BALANCE.CREDIT-BALANCE.DEBIT AS BALANCE FROM (SELECT ACCID,SUM(tb_Transactions.DEBIT) AS DEBIT,SUM(tb_Transactions.CREDIT) AS CREDIT FROM tb_Transactions LEFT OUTER JOIN PAY_SUPPLIER ON PAY_SUPPLIER.Ledgerid=tb_Transactions.ACCID WHERE PAY_SUPPLIER.CODE IS NOT NULL GROUP BY ACCID) AS BALANCE LEFT OUTER JOIN PAY_SUPPLIER ON PAY_SUPPLIER.LedgerId=BALANCE.ACCID LEFT OUTER JOIN (SELECT TB1.ACCID,TB1.DEBIT AS 'LAST_AMOUNT',CONVERT(VARCHAR,DATED,103) AS 'DATED'  FROM (SELECT TRANSACTIONID,ACCID,ACCNAME,tb_transactions.DEBIT,DATED FROM tb_transactions INNER JOIN PAY_SUPPLIER ON PAY_SUPPLIER.LEDGERID=ACCID WHERE DATED IN(SELECT MAX(DATED) FROM tb_Transactions GROUP BY ACCID)) AS TB1 INNER JOIN (SELECT ACCID,MAX(TRANSACTIONID)AS TRANSACTION_ID FROM tb_transactions WHERE DATED IN(SELECT MAX(DATED) FROM tb_Transactions WHERE DEBIT>0 GROUP BY ACCID) AND DEBIT>0 GROUP BY ACCID) AS TB2 ON TB1.TRANSACTIONID=TB2.TRANSACTION_ID) AS LAST_PAY ON LAST_PAY.ACCID=BALANCE.ACCID WHERE PAY_SUPPLIER.DESC_ENG LIKE '%" + textBox2.Text + "%'", conn);
                    query="SELECT PAY_SUPPLIER.DESC_ENG AS NAME,CONVERT(VARCHAR,LAST_PAY.DATED,103) AS 'LAST PAY DATE',LAST_PAY.LAST_AMOUNT AS 'LAST PAY AMOUNT',BALANCE.CREDIT,BALANCE.DEBIT,BALANCE.CREDIT-BALANCE.DEBIT AS BALANCE FROM (SELECT ACCID,SUM(tb_Transactions.DEBIT) AS DEBIT,SUM(tb_Transactions.CREDIT) AS CREDIT FROM tb_Transactions LEFT OUTER JOIN PAY_SUPPLIER ON PAY_SUPPLIER.Ledgerid=tb_Transactions.ACCID WHERE PAY_SUPPLIER.CODE IS NOT NULL GROUP BY ACCID) AS BALANCE LEFT OUTER JOIN PAY_SUPPLIER ON PAY_SUPPLIER.LedgerId=BALANCE.ACCID LEFT OUTER JOIN (SELECT TB1.ACCID,TB1.DEBIT AS 'LAST_AMOUNT',CONVERT(VARCHAR,DATED,103) AS 'DATED'  FROM (SELECT TRANSACTIONID,ACCID,ACCNAME,tb_transactions.DEBIT,DATED FROM tb_transactions INNER JOIN PAY_SUPPLIER ON PAY_SUPPLIER.LEDGERID=ACCID) AS TB1 INNER JOIN (SELECT ACCID,MAX(TRANSACTIONID)AS TRANSACTION_ID FROM tb_transactions WHERE DATED IN(SELECT MAX(DATED) FROM tb_Transactions WHERE DEBIT>0 GROUP BY ACCID) AND DEBIT>0 GROUP BY ACCID) AS TB2 ON TB1.TRANSACTIONID=TB2.TRANSACTION_ID) AS LAST_PAY ON LAST_PAY.ACCID=BALANCE.ACCID WHERE PAY_SUPPLIER.DESC_ENG LIKE '%" + textBox2.Text + "%'";
                    //SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    //sda.Fill(dt);
                   dt=Model.DbFunctions.GetDataTable(query);


                    string query1= "SELECT sum(ISNULL(BALANCE.CREDIT,0))-sum(ISNULL(BALANCE.DEBIT,0)) AS BALANCE,SUM(ISNULL(BALANCE.DEBIT,0)) AS DEBIT_TOTAL,SUM(ISNULL(BALANCE.CREDIT,0)) AS CREDIT_TOTAL FROM (SELECT ACCID,SUM(tb_Transactions.DEBIT) AS DEBIT,SUM(tb_Transactions.CREDIT) AS CREDIT FROM tb_Transactions LEFT OUTER JOIN PAY_SUPPLIER ON PAY_SUPPLIER.Ledgerid=tb_Transactions.ACCID WHERE PAY_SUPPLIER.CODE IS NOT NULL GROUP BY ACCID) AS BALANCE LEFT OUTER JOIN PAY_SUPPLIER ON PAY_SUPPLIER.LedgerId=BALANCE.ACCID LEFT OUTER JOIN (SELECT TB1.ACCID,TB1.DEBIT AS 'LAST_AMOUNT',CONVERT(VARCHAR,DATED,103) AS 'DATED'  FROM (SELECT TRANSACTIONID,ACCID,ACCNAME,tb_transactions.DEBIT,DATED FROM tb_transactions INNER JOIN PAY_SUPPLIER ON PAY_SUPPLIER.LEDGERID=ACCID WHERE DATED IN(SELECT MAX(DATED) FROM tb_Transactions GROUP BY ACCID)) AS TB1 INNER JOIN (SELECT ACCID,MAX(TRANSACTIONID)AS TRANSACTION_ID FROM tb_transactions WHERE DATED IN(SELECT MAX(DATED) FROM tb_Transactions WHERE DEBIT>0 GROUP BY ACCID) AND DEBIT>0 GROUP BY ACCID) AS TB2 ON TB1.TRANSACTIONID=TB2.TRANSACTION_ID) AS LAST_PAY ON LAST_PAY.ACCID=BALANCE.ACCID WHERE PAY_SUPPLIER.DESC_ENG LIKE '%" + textBox2.Text + "%'";
                    //SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                    DataTable dt1 = new DataTable();
                    //sda1.Fill(dt1);
                    dt1=Model.DbFunctions.GetDataTable(query1);

                    if (dt.Rows.Count > 0)
                    {
                        if (dt1.Rows.Count > 0)
                        {

                            textBox1.Text = dt1.Rows[0][0].ToString();
                            DataRow row = dt.NewRow();
                            row["NAME"] = "Total";
                            row["DEBIT"] = dt1.Rows[0][1].ToString();
                            row["CREDIT"] = dt1.Rows[0][2].ToString();
                            row["BALANCE"] = dt1.Rows[0][0].ToString();
                            dt.Rows.InsertAt(row, dt.Rows.Count);
                            dataGridView2.DataSource = dt;
                            dataGridView2.Columns[0].Width = 400;
                        }
                        else
                        textBox1.Clear();
                        dataGridView2.DataSource = dt;
                        dataGridView2.Columns[0].Width = 400;
                    }
                   
                }
                else
                {
                    if (dataGridView2.ColumnCount > 0)
                        dataGridView2.Columns.Clear();
                    // SqlCommand cmd = new SqlCommand("SELECT PAY_SUPPLIER.DESC_ENG AS NAME,CONVERT(VARCHAR,LAST_PAY.DATED,103) AS 'LAST PAY DATE',LAST_PAY.LAST_AMOUNT AS 'LAST PAY AMOUNT',BALANCE.CREDIT,BALANCE.DEBIT,BALANCE.CREDIT-BALANCE.DEBIT AS BALANCE FROM (SELECT ACCID,SUM(tb_Transactions.DEBIT) AS DEBIT,SUM(tb_Transactions.CREDIT) AS CREDIT FROM tb_Transactions LEFT OUTER JOIN PAY_SUPPLIER ON PAY_SUPPLIER.Ledgerid=tb_Transactions.ACCID WHERE PAY_SUPPLIER.CODE IS NOT NULL GROUP BY ACCID) AS BALANCE LEFT OUTER JOIN PAY_SUPPLIER ON PAY_SUPPLIER.LedgerId=BALANCE.ACCID LEFT OUTER JOIN (SELECT TB1.ACCID,TB1.DEBIT AS 'LAST_AMOUNT',CONVERT(VARCHAR,DATED,103) AS 'DATED'  FROM (SELECT TRANSACTIONID,ACCID,ACCNAME,tb_transactions.DEBIT,DATED FROM tb_transactions INNER JOIN PAY_SUPPLIER ON PAY_SUPPLIER.LEDGERID=ACCID WHERE DATED IN(SELECT MAX(DATED) FROM tb_Transactions GROUP BY ACCID)) AS TB1 INNER JOIN (SELECT ACCID,MAX(TRANSACTIONID)AS TRANSACTION_ID FROM tb_transactions WHERE DATED IN(SELECT MAX(DATED) FROM tb_Transactions WHERE DEBIT>0 GROUP BY ACCID) AND DEBIT>0 GROUP BY ACCID) AS TB2 ON TB1.TRANSACTIONID=TB2.TRANSACTION_ID) AS LAST_PAY ON LAST_PAY.ACCID=BALANCE.ACCID WHERE PAY_SUPPLIER.DESC_ENG LIKE '%" + textBox2.Text + "%'", conn);
                    query="SELECT PAY_SUPPLIER.DESC_ENG AS NAME,CONVERT(VARCHAR,LAST_PAY.DATED,103) AS 'LAST PAY DATE',LAST_PAY.LAST_AMOUNT AS 'LAST PAY AMOUNT',BALANCE.CREDIT,BALANCE.DEBIT,BALANCE.CREDIT-BALANCE.DEBIT AS BALANCE FROM (SELECT ACCID,SUM(tb_Transactions.DEBIT) AS DEBIT,SUM(tb_Transactions.CREDIT) AS CREDIT FROM tb_Transactions LEFT OUTER JOIN PAY_SUPPLIER ON PAY_SUPPLIER.Ledgerid=tb_Transactions.ACCID WHERE tb_Transactions.DATED BETWEEN @d1 and @d2 AND PAY_SUPPLIER.CODE IS NOT NULL GROUP BY ACCID) AS BALANCE LEFT OUTER JOIN PAY_SUPPLIER ON PAY_SUPPLIER.LedgerId=BALANCE.ACCID LEFT OUTER JOIN (SELECT TB1.ACCID,TB1.DEBIT AS 'LAST_AMOUNT',CONVERT(VARCHAR,DATED,103) AS 'DATED'  FROM (SELECT TRANSACTIONID,ACCID,ACCNAME,tb_transactions.DEBIT,DATED FROM tb_transactions INNER JOIN PAY_SUPPLIER ON PAY_SUPPLIER.LEDGERID=ACCID) AS TB1 INNER JOIN (SELECT ACCID,MAX(TRANSACTIONID)AS TRANSACTION_ID FROM tb_transactions WHERE DATED IN(SELECT MAX(DATED) FROM tb_Transactions WHERE tb_Transactions.DATED BETWEEN @d1 and @d2 AND DEBIT>0 GROUP BY ACCID) AND DEBIT>0 GROUP BY ACCID) AS TB2 ON TB1.TRANSACTIONID=TB2.TRANSACTION_ID) AS LAST_PAY ON LAST_PAY.ACCID=BALANCE.ACCID WHERE PAY_SUPPLIER.DESC_ENG LIKE '%" + textBox2.Text + "%'";
                    //cmd.Parameters.Clear();
                    //cmd.Parameters.Add("@d1", SqlDbType.Date).Value = dateFrom.Value;
                    //cmd.Parameters.Add("@d2", SqlDbType.Date).Value = dateTo.Value;
                      Dictionary<string ,object> parameter =new Dictionary<string,object> ();
                      parameter.Add("@d1",dateFrom.Value);
                      parameter.Add("@d2",dateTo.Value);
                      DataTable dt = new DataTable();
                      dt = Model.DbFunctions.GetDataTable(query, parameter);

                    string query1="SELECT sum(ISNULL(BALANCE.CREDIT,0))-sum(ISNULL(BALANCE.DEBIT,0)) AS BALANCE,SUM(ISNULL(BALANCE.DEBIT,0)) AS DEBIT_TOTAL,SUM(ISNULL(BALANCE.CREDIT,0)) AS CREDIT_TOTAL FROM (SELECT ACCID,SUM(tb_Transactions.DEBIT) AS DEBIT,SUM(tb_Transactions.CREDIT) AS CREDIT FROM tb_Transactions LEFT OUTER JOIN PAY_SUPPLIER ON PAY_SUPPLIER.Ledgerid=tb_Transactions.ACCID WHERE PAY_SUPPLIER.CODE IS NOT NULL AND tb_Transactions.DATED BETWEEN @d1 and @d2 GROUP BY ACCID) AS BALANCE LEFT OUTER JOIN PAY_SUPPLIER ON PAY_SUPPLIER.LedgerId=BALANCE.ACCID LEFT OUTER JOIN (SELECT TB1.ACCID,TB1.DEBIT AS 'LAST_AMOUNT',CONVERT(VARCHAR,DATED,103) AS 'DATED'  FROM (SELECT TRANSACTIONID,ACCID,ACCNAME,tb_transactions.DEBIT,DATED FROM tb_transactions INNER JOIN PAY_SUPPLIER ON PAY_SUPPLIER.LEDGERID=ACCID WHERE DATED IN(SELECT MAX(DATED) FROM tb_Transactions GROUP BY ACCID)) AS TB1 INNER JOIN (SELECT ACCID,MAX(TRANSACTIONID)AS TRANSACTION_ID FROM tb_transactions WHERE DATED IN(SELECT MAX(DATED) FROM tb_Transactions WHERE DEBIT>0 GROUP BY ACCID) AND DEBIT>0 GROUP BY ACCID) AS TB2 ON TB1.TRANSACTIONID=TB2.TRANSACTION_ID) AS LAST_PAY ON LAST_PAY.ACCID=BALANCE.ACCID WHERE PAY_SUPPLIER.DESC_ENG LIKE '%" + textBox2.Text + "%'";
                    //cmd1.Parameters.Clear();
                    Dictionary<string ,object> parameters =new Dictionary<string,object> ();
                    parameters.Add("@d1",dateFrom.Value);
                    parameters.Add("@d2",dateTo.Value);
                    //SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                    DataTable dt1 = new DataTable();
                    //sda1.Fill(dt1);
                    dt1 = Model.DbFunctions.GetDataTable(query1, parameters);
                    if (dt.Rows.Count > 0)
                    {
                        if (dt1.Rows.Count > 0)
                        {
                            textBox1.Text = dt1.Rows[0][0].ToString();
                            DataRow row = dt.NewRow();
                            row["NAME"] = "Total";
                            row["DEBIT"] = dt1.Rows[0][1].ToString();
                            row["CREDIT"] = dt1.Rows[0][2].ToString();
                            row["BALANCE"] = dt1.Rows[0][0].ToString();
                            dt.Rows.InsertAt(row, dt.Rows.Count);
                            dataGridView2.DataSource = dt;
                            dataGridView2.Columns[0].Width = 400;
                        }
                        else
                            textBox1.Clear();

                        dataGridView2.DataSource = dt;
                        dataGridView2.Columns[0].Width = 400;
                    }

                   
                }
            }
           
        }


        public void Total(DataTable dt)
        {
            
        }
        //public void totalBalance()
        //{
        //    for (int i = 0; i < dataGridView1.Rows.Count; i++)
        //    {
        //        double total = Convert.ToDouble(dataGridView1.Rows[i].Cells["balance"].Value.ToString());
        //        TotamAmount = TotamAmount + total;
        //        label4.Text = TotamAmount.ToString();
        //    }
        //}
        //public void totalAmount()
        //{
        //    if (dataGridView1.Rows.Count > 0)
        //    {
        //        for (int i = 0; i < dataGridView1.Rows.Count; i++)
        //        {
        //            double total = Convert.ToDouble(dataGridView1.Rows[i].Cells["balance"].Value.ToString());
        //            TotamAmount = TotamAmount + total;
        //            textBox1.Text = TotamAmount.ToString();
        //        }
        //    }
        //    else
        //    {
        //        textBox1.Text = "0.00";
        //    }
        //}

  
        private void CustomerSummary_Load(object sender, EventArgs e)
        {
            if (is_customer)
            {
                get_salesman();
                btnSave.Enabled = true;
                cmb_salesman.Enabled = true;

            }
            else
            {
                btnSave.Enabled = true;
                cmb_salesman.Enabled = false;

            }
            btnSave.PerformClick();
           
        }
        private void get_salesman()
        {
            //SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dt1 = new DataTable();
            //SqlCommand cmd1 = new SqlCommand();
            string query="SELECT EMPID,CONCAT(EMP_FNAME,' ',EMP_MNAME,' ',EMP_LNAME)as name from EMP_EMPLOYEES WHERE EMP_DESIG=21";
           // cmd1.Connection = conn;
           // adapter.SelectCommand = cmd1;
           //adapter.Fill(dt1);
           // conn.Close();
            dt1 = Model.DbFunctions.GetDataTable(query);
            cmb_salesman.ValueMember = "EMPID";
            cmb_salesman.DisplayMember = "name";
            DataRow row = dt1.NewRow();
            dt1.Rows.InsertAt(row, 0);
            cmb_salesman.DataSource = dt1;

        }
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            printReport();
        }

        public void printReport()
        {
            PrintDialog printdlg = new PrintDialog();
            PrintDocument doc = new PrintDocument();
            doc.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("A4", 840, 1188);
            doc.PrintPage += printA4;
            printdlg.Document = doc;
            doc.Print();

        }

        private void printA4(object sender, PrintPageEventArgs e)
        {
            Pen blackpen = new Pen(System.Drawing.Color.Black, 1);
            Font Headerfont5 = new Font("Times New Roman", 10, FontStyle.Regular);
            Font Headerfont1 = new Font("Times New Roman", 12, FontStyle.Regular);
            Font Headerfont2 = new Font("Times New Roman", 14, FontStyle.Bold);
            //System.Drawing.Font Headerfont2 = new System.Drawing.Font("Times New Roman", 14, FontStyle.Bold);
            //System.Drawing.Font Headerfont1 = new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold);
            //System.Drawing.Font Headerfont3 = new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular);
            //System.Drawing.Font Headerfont4 = new System.Drawing.Font("Times New Roman", 10, FontStyle.Bold);
            m = 0;
            bool hasmorepages = false;
            float xpos;
            int startx = 50;
            int starty = 20;
            int offset = 15;
            bool PRINTTOTALPAGE = true;
            int w = e.MarginBounds.Width / 2;
            int x = e.MarginBounds.Left;
            int y = e.MarginBounds.Top;
            string vchno = "";
            string snno = "";
            string dated = "";
            string vchtype = "";
            string partclrs = "";
            string debit = "";
            string credit = "";
            string balance = "";
            string narration = "";
            int value = 0;
            var tabDataForeColor = System.Drawing.Color.Black;
            int height = 100 + y;
            Pen blackPen1 = new Pen(System.Drawing.Color.Black, 1);
            //var txtDataWidth = e.Graphics.MeasureString(CompanyName, Headerfont1).Width;
            double pricWtax = 0;
            decimal a = 0;
            StringFormat format = new StringFormat(StringFormatFlags.DirectionRightToLeft);
            using (var sf = new StringFormat())
            {
                height += 15;
                sf.LineAlignment = StringAlignment.Center;
                sf.Alignment = StringAlignment.Center;
                xpos = e.MarginBounds.Left + (e.MarginBounds.Width / 2);
                e.Graphics.DrawRectangle(blackPen1, 7, 115, 790, 945); //BIG RECTANGLE
                //e.Graphics.DrawLine(blackpen, 7, 80, 790, 80); //h
                e.Graphics.DrawLine(blackpen, 7, 150, 797, 150); //h
                //e.Graphics.DrawLine(blackpen, 10, 1090, 830, 1090); //h
                //e.Graphics.DrawLine(blackpen, 10, 1130, 830, 1130); //h
                e.Graphics.DrawLine(blackpen, 35, 115, 35, 1030); //v sl no
                e.Graphics.DrawLine(blackpen, 350, 115, 350, 1030); //customner name
                e.Graphics.DrawLine(blackpen, 440, 115, 440, 1030); //lastpaydate
                e.Graphics.DrawLine(blackpen, 530, 115, 530, 1030); //lastpayamnt
                e.Graphics.DrawLine(blackpen, 620, 115, 620, 1030); //debit
                e.Graphics.DrawLine(blackpen, 710, 115, 710, 1030); //credit     
                e.Graphics.DrawLine(blackpen, 7, 1030, 797, 1030); //h
                e.Graphics.DrawString("No", Headerfont5, new SolidBrush(Color.Black), 8, 120);
                e.Graphics.DrawString("Name", Headerfont5, new SolidBrush(Color.Black), 140, 120);
                e.Graphics.DrawString("Last Pay Date", Headerfont5, new SolidBrush(Color.Black), 350, 120); e.Graphics.DrawString("No", Headerfont5, new SolidBrush(Color.Black), 8, 120);
                e.Graphics.DrawString("Last Pay Amt", Headerfont5, new SolidBrush(Color.Black), 440, 120);
                e.Graphics.DrawString("Debit", Headerfont5, new SolidBrush(Color.Black), 530, 120);
                e.Graphics.DrawString("Credit", Headerfont5, new SolidBrush(Color.Black), 620, 120);
                e.Graphics.DrawString("Balance", Headerfont5, new SolidBrush(Color.Black), 710, 120);
                e.Graphics.DrawString("CUSTOMER BALANCE SHEET", Headerfont2, new SolidBrush(Color.Black), 300, 80);

             
                 
                //string date = "Ledger/Statement of Account for the period From " + Date_From.Value.ToShortDateString() + " To " + Date_To.Value.ToShortDateString();
                //e.Graphics.DrawString(date, Headerfont3, new SolidBrush(System.Drawing.Color.Black), 180, 15);
              
                float fontheight = Headerfont1.GetHeight();
                try
                {
                    int i = 0;
                    int j = 1;
                    int nooflines = 0;
                    //foreach (DataGridViewRow row in dgledgerTrns.Rows)
                    for (int k = 0; k < dataGridView2.Rows.Count - 1; k++)
                    {
                        if (Convert.ToDecimal( dataGridView2.Rows[k].Cells["BALANCE"].Value)==0)
                        {
                            continue;
                        }
                        PRINTTOTALPAGE = false;
                        if (j > printeditems)
                        {
                            if (nooflines < 31)
                            {
                                m = m + 1;

                                //SELECT REC_CUSTOMER.DESC_ENG AS NAME,CONVERT(VARCHAR,LAST_PAY.DATED,103) AS 'LAST PAY DATE',LAST_PAY.LAST_AMOUNT AS 'LAST PAY AMOUNT',BALANCE.DEBIT,BALANCE.CREDIT,BALANCE.DEBIT-BALANCE.CREDIT AS BALANCE FROM (SELECT ACCID,SUM(tb_Transactions.DEBIT) AS DEBIT,SUM(tb_Transactions.CREDIT) AS CREDIT FROM tb_Transactions LEFT OUTER JOIN REC_CUSTOMER ON REC_CUSTOMER.Ledgerid=tb_Transactions.ACCID WHERE REC_CUSTOMER.CODE IS NOT NULL GROUP BY ACCID) AS BALANCE LEFT OUTER JOIN REC_CUSTOMER ON REC_CUSTOMER.LedgerId=BALANCE.ACCID LEFT OUTER JOIN (SELECT ACCID,MAX(tb_Transactions.DATED) AS DATED,SUM(tb_Transactions.DEBIT) AS LAST_AMOUNT FROM tb_transactions  INNER JOIN REC_CUSTOMER ON REC_CUSTOMER.LedgerId=tb_Transactions.ACCID GROUP BY ACCID) AS LAST_PAY ON LAST_PAY.ACCID=BALANCE.ACCID WHERE REC_CUSTOMER.SALESMAN_CODE LIKE '%" + cmb_salesman.SelectedValue + "%' AND REC_CUSTOMER.DESC_ENG LIKE '%" +textBox2.Text + "%'", conn);
                                e.Graphics.DrawString(m.ToString(), Headerfont5, new SolidBrush(System.Drawing.Color.Black), startx - 40, starty + offset + 120);

                                vchno = dataGridView2.Rows[k].Cells["NAME"].Value.ToString();
                                e.Graphics.DrawString(vchno.ToString(), Headerfont5, new SolidBrush(System.Drawing.Color.Black), startx - 15, starty + offset + 120);

                              dated = dataGridView2.Rows[k].Cells["LAST PAY DATE"].Value.ToString();
                               // string date1 = dated.Substring(0, 10);
                                
                                e.Graphics.DrawString(dated.ToString(), Headerfont5, new SolidBrush(System.Drawing.Color.Black), startx + 300, starty + offset + 120);

                                vchtype = dataGridView2.Rows[k].Cells["LAST PAY AMOUNT"].Value.ToString();
                                e.Graphics.DrawString(vchtype.ToString(), Headerfont5, new SolidBrush(System.Drawing.Color.Black), startx + 390, starty + offset + 120);

                                partclrs = dataGridView2.Rows[k].Cells["DEBIT"].Value.ToString();
                                e.Graphics.DrawString(partclrs.ToString(), Headerfont5, new SolidBrush(System.Drawing.Color.Black), startx + 480, starty + offset + 120);

                                debit = dataGridView2.Rows[k].Cells["CREDIT"].Value.ToString();
                                e.Graphics.DrawString(debit.ToString(), Headerfont5, new SolidBrush(System.Drawing.Color.Black), startx + 570, starty + offset + 120);

                                credit = dataGridView2.Rows[k].Cells["BALANCE"].Value.ToString();
                                e.Graphics.DrawString(credit.ToString(), Headerfont5, new SolidBrush(System.Drawing.Color.Black), startx + 660, starty + offset + 120);

                                e.Graphics.DrawLine(blackpen, 7, starty + offset + 142, 797, starty + offset + 142); //h

                                //balance = dataGridView1.Rows[k].Cells["BALANCES"].Value.ToString();
                                //string bal = balance.Remove(balance.Length - 3);
                                //e.Graphics.DrawString(bal.ToString(), Headerfont3, new SolidBrush(System.Drawing.Color.Black), startx + 670, starty + offset + 40, format);

                                //narration = dataGridView1.Rows[k].Cells["NARRATION"].Value.ToString();
                                //e.Graphics.DrawString(narration.ToString(), Headerfont3, new SolidBrush(System.Drawing.Color.Black), startx + 670, starty + offset + 40);

                                offset = offset + (int)fontheight + 10;
                                value = k;
                                nooflines++;
                                j++;

                            }
                            else
                            {
                                printeditems = j - 1;
                                hasmorepages = true;
                                PRINTTOTALPAGE = true;
                            }
                            if (hasmorepages == true)
                            {
                                e.Graphics.DrawString("coutinue...", Headerfont1, new SolidBrush(System.Drawing.Color.Black), startx + 40, 1140);
                            }
                        }
                        else
                        {
                            j++;
                            m++;
                        }
                    }
                }

                catch (Exception exc)
                {
                    string c = exc.Message;
                }
            }
            float newoffset = 900;
            if (!PRINTTOTALPAGE)
            {
                PAGETOTAL = true;
                if (PAGETOTAL)
                {
                    try
                    {

                     
                        //string opblnce = dgledgerTrns.Rows[value + 4].Cells["PARTICULARS"].Value.ToString();
                        e.Graphics.DrawString("Total", Headerfont2, new SolidBrush(System.Drawing.Color.Black), startx + 450, 1032);
                        e.Graphics.DrawString(textBox1.Text, Headerfont2, new SolidBrush(System.Drawing.Color.Black), startx + 580, 1032);
                        newoffset = newoffset + 20;
                        newoffset = newoffset + 20;
                        newoffset = newoffset + 20;
                    }
                    catch
                    {
                    }
                }
                PAGETOTAL = false;
            }
            e.HasMorePages = hasmorepages;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            customerSummary();
            if (dataGridView2.Rows.Count > 0)
            {
                dataGridView2.CurrentCell = dataGridView2.Rows[dataGridView2.RowCount - 1].Cells[0];
                Font font = new Font(dataGridView2.DefaultCellStyle.Font.FontFamily, 9, FontStyle.Bold);
                dataGridView2.Rows[dataGridView2.RowCount - 1].DefaultCellStyle.Font = font;
                dataGridView2.Rows[dataGridView2.RowCount - 1].DefaultCellStyle.ForeColor = Color.Red;
                dataGridView2.Columns["NAME"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

                dataGridView2.Columns["DEBIT"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridView2.Columns["CREDIT"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridView2.Columns["BALANCE"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            customerSummary();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                dateFrom.Enabled = true;
                dateTo.Enabled = true;
            }
            else
            {
                dateFrom.Enabled = false;
                dateTo.Enabled = false;
            }
        }

        private void cmb_salesman_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView2_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            dataGridView2.Columns[e.Column.Index].SortMode = DataGridViewColumnSortMode.NotSortable;
        }
    }
}
