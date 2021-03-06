﻿   using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sys_Sols_Inventory.Model;

namespace Sys_Sols_Inventory
{
    public partial class CustomerStatement : Form
    {
        clsCustomer custObj = new clsCustomer();
        TbTransactionsDB transObj = new TbTransactionsDB();
       // private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
       // private SqlCommand cmd = new SqlCommand();
        bool PAGETOTAL = false;
        public int printeditems = 0;
        int m = 0;
        int b = 0;
        double totalbalance = 0;
        double TotamAmount = 0;
        int ledgerid;
        DateTime maxdate;
        double maxcreditamount = 0;
        public CustomerStatement()
        {
            InitializeComponent();
           // cmd.Connection = conn;
        }

        private bool valid()
        {
            if (SUP_CODE.Text != "")
            {
                return true;
            }
            else
            {
                MessageBox.Show("Please select the customer first!");
                return false;
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
            if (valid())
            {
                if (Chk.Checked == true)
                {
                    //this.CUS_STATTableAdapter.Connection.ConnectionString = Properties.Settings.Default.ConnectionStrings;
                    this.CUS_STATTableAdapter.Connection= Model.DbFunctions.GetConnection();
                    // TODO: This line of code loads data into the 'AIN_INVENTORYDataSet1.CUS_STAT' table. You can move, or remove it, as needed.
                    this.CUS_STATTableAdapter.Fill(this.AIN_INVENTORYDataSet2.CUS_STAT, SUP_CODE.Text, DATE_FROM.Value, DATE_TO.Value);
                   // this.CUS_STATTableAdapter.FillBy1(this.AIN_INVENTORYDataSet2.CUS_STAT, SUP_CODE.Text, Convert.ToDateTime(DATE_FROM.Text), Convert.ToDateTime(DATE_TO.Text));
                   
                    this.reportViewer1.RefreshReport();
                }
                else
                {
                  //this.CUS_STATTableAdapter.Connection.ConnectionString = Properties.Settings.Default.ConnectionStrings; 
                   this.CUS_STATTableAdapter.Connection = Model.DbFunctions.GetConnection();
                  this.CUS_STATTableAdapter.FillBy(this.AIN_INVENTORYDataSet2.CUS_STAT, SUP_CODE.Text);
                   // this.CUS_STATTableAdapter.FillBy1(this.AIN_INVENTORYDataSet2.CUS_STAT, SUP_CODE.Text);
                    this.reportViewer1.RefreshReport();
                }

               
                //conn.Open();
                //cmd.CommandType = CommandType.StoredProcedure;admin
                //cmd.CommandText = "CUS_STAT";
                //cmd.Parameters.Clear();
                //cmd.Parameters.AddWithValue("@CUS_CODE", SUP_CODE.Text);
                //cmd.Parameters.AddWithValue("@DATE_FROM", DATE_FROM.Value.ToString("MM/dd/yyyy"));
                //cmd.Parameters.AddWithValue("@DATE_TO", DATE_TO.Value.ToString("MM/dd/yyyy"));
                //SqlDataReader r = cmd.ExecuteReader();
                //dgDetail.Rows.Clear();
                //double debit = 0;
                //double credit = 0;
                //while (r.Read())
                //{
                //    int i = dgDetail.Rows.Add(new DataGridViewRow());
                //    DataGridViewCellCollection c = dgDetail.Rows[i].Cells;
                //    c["cDate"].Value = r["DATE"];
                //    c["cDocNo"].Value = r["DOC_NO"];
                //    c["cDocType"].Value = r["DOC_TYPE"];
                //    c["cRef"].Value = r["DOC_REFERENCE"];
                //    c["cDesc"].Value = r["DESCRIPTION"];
                //    c["cDebit"].Value = r["DEBIT"];
                //    debit = debit + Convert.ToDouble(r["DEBIT"]);
                //    c["cCredit"].Value = r["CREDIT"];
                //    credit = credit + Convert.ToDouble(r["CREDIT"]);
                //    c["cBalance"].Value = (credit - debit);
                //}
                //conn.Close();
            }
        }

        private void btnSup_Click(object sender, EventArgs e)
        {
            CommonHelp h = new CommonHelp(0, genEnum.Customer);
            if (h.ShowDialog() == DialogResult.OK && h.c != null)
            {
                SUP_CODE.Text = Convert.ToString(h.c[0].Value);
                SUP_NAME.Text = Convert.ToString(h.c[1].Value);
            }
        }

        private void SUP_CODE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                btnSup.PerformClick();
            }
        }

        private void CustomerStatement_Load(object sender, EventArgs e)
        {
           btnSup.Focus();
           customerSummary();

        }

        private void Chk_CheckedChanged(object sender, EventArgs e)
        {
            if (Chk.Checked == true)
                grpDate.Enabled = true;
            else
                grpDate.Enabled = false;
        }
        public void customerSummary()
        {

            //SqlCommand cmd = new SqlCommand("SELECT LedgerId FROM REC_CUSTOMER", conn);
            //SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
           // sda.Fill(dt);
            dt = custObj.getLedgerId();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ledgerid = Convert.ToInt32(dt.Rows[i]["LedgerId"].ToString());
                    double sumdebit = 0;
                    double sumcredit = 0;
                    double balance = 0;

                    dataGridView1.Rows.Add("", "", "", "");
                  //  SqlCommand cmd1 = new SqlCommand("SELECT ACCNAME,DEBIT,DATED,CREDIT FROM tb_Transactions WHERE ACCID='" + dt.Rows[i]["LedgerId"].ToString() + "' ORDER BY DATED", conn);
                  //  SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                    DataTable dt1 = new DataTable();
                  //  sda1.Fill(dt1);
                    dt1 = transObj.getDetailsUsingDataTable(ledgerid.ToString());
                  
                        

                    for (int j = 0; j < dt1.Rows.Count; j++)
                    {
                        
                        // maxdate = dt1.Rows[j]["DATED"].ToString();
                        dataGridView1.Rows[i].Cells["accname"].Value = dt1.Rows[j]["ACCNAME"].ToString();
                        double debit = Convert.ToDouble(dt1.Rows[j]["DEBIT"].ToString());
                        double credit = Convert.ToDouble(dt1.Rows[j]["CREDIT"].ToString());
                        // string date = dt1.Rows[j]["DATED"].ToString();
                        sumdebit = sumdebit + debit;
                        sumcredit = sumcredit + credit;
                        balance = sumdebit - sumcredit;
                        dataGridView1.Rows[i].Cells["debit"].Value = sumdebit;
                        dataGridView1.Rows[i].Cells["credit"].Value = sumcredit;
                        dataGridView1.Rows[i].Cells["balance"].Value = balance;
                        //dataGridView1.Rows[i].Cells["lastpaydate"].Value = date;
                        //dataGridView1.Rows[i].Cells["lastpayamount"].Value = credit;
                       
                        //SqlCommand cmd2 = new SqlCommand("SELECT DATED,CREDIT FROM tb_Transactions WHERE ACCID='" + dt.Rows[i]["LedgerId"].ToString() + "' AND VOUCHERTYPE!='SALES Normal' ORDER BY DATED", conn);
                        //SqlDataAdapter sda2 = new SqlDataAdapter(cmd2);
                        DataTable dt2 = new DataTable();
                       // sda2.Fill(dt2);
                        dt2 = transObj.getDateAndCredit(ledgerid.ToString());
                        for (int k = 0; k < dt2.Rows.Count; k++)
                        {
                            string date = dt2.Rows[k]["DATED"].ToString();
                            double credit1 = Convert.ToDouble(dt2.Rows[k]["CREDIT"].ToString());
                            dataGridView1.Rows[i].Cells["lastpaydate"].Value = date;
                            dataGridView1.Rows[i].Cells["lastpayamount"].Value = credit1;
                        }



                    }


                }
            }
            for (int k = 0; k < dataGridView1.Rows.Count; k++)
            {

                int a = Convert.ToInt32(dataGridView1.Rows[k].Cells["balance"].Value);
                b = b + a;
                textBox1.Text = b.ToString();
            }
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                
                if (dataGridView1.Rows[i].Cells["balance"].Value ==null)
                {
                   
                 
                    dataGridView1.Rows.RemoveAt(i);
                    i = 0;
                   
                }
            }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //SUP_CODE.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            //SUP_NAME.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
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
                e.Graphics.DrawString("last pay date", Headerfont5, new SolidBrush(Color.Black), 350, 120); e.Graphics.DrawString("No", Headerfont5, new SolidBrush(Color.Black), 8, 120);
                e.Graphics.DrawString("last pay amt", Headerfont5, new SolidBrush(Color.Black), 440, 120);
                e.Graphics.DrawString("debit", Headerfont5, new SolidBrush(Color.Black), 530, 120);
                e.Graphics.DrawString("credit", Headerfont5, new SolidBrush(Color.Black), 620, 120);
                e.Graphics.DrawString("balance", Headerfont5, new SolidBrush(Color.Black), 710, 120);
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
                    for (int k = 0; k < dataGridView1.Rows.Count - 1; k++)
                    {
                        PRINTTOTALPAGE = false;
                        if (j > printeditems)
                        {
                            if (nooflines < 31)
                            {
                                m = m + 1;


                                e.Graphics.DrawString(m.ToString(), Headerfont5, new SolidBrush(System.Drawing.Color.Black), startx - 40, starty + offset + 120);

                                vchno = dataGridView1.Rows[k].Cells["accname"].Value.ToString();
                                e.Graphics.DrawString(vchno.ToString(), Headerfont5, new SolidBrush(System.Drawing.Color.Black), startx - 15, starty + offset + 120);

                                dated = dataGridView1.Rows[k].Cells["lastpaydate"].Value.ToString();
                                string date1 = dated.Substring(0, 10);
                                e.Graphics.DrawString(date1.ToString(), Headerfont5, new SolidBrush(System.Drawing.Color.Black), startx + 300, starty + offset + 120);

                                vchtype = dataGridView1.Rows[k].Cells["lastpayamount"].Value.ToString();
                                e.Graphics.DrawString(vchtype.ToString(), Headerfont5, new SolidBrush(System.Drawing.Color.Black), startx + 390, starty + offset + 120);

                                partclrs = dataGridView1.Rows[k].Cells["debit"].Value.ToString();
                                e.Graphics.DrawString(partclrs.ToString(), Headerfont5, new SolidBrush(System.Drawing.Color.Black), startx + 480, starty + offset + 120);

                                debit = dataGridView1.Rows[k].Cells["credit"].Value.ToString();
                                e.Graphics.DrawString(debit.ToString(), Headerfont5, new SolidBrush(System.Drawing.Color.Black), startx + 570, starty + offset + 120);

                                credit = dataGridView1.Rows[k].Cells["balance"].Value.ToString();
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

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            CustomerStatement_Load(this,e);
        }


    }
}
