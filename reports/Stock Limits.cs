using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sys_Sols_Inventory.Model;

namespace Sys_Sols_Inventory.reports
{
    public partial class Stock_Limits : Form
    {
        public Stock_Limits()
        {
            InitializeComponent();
        }
        //private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        //private SqlCommand cmd = new SqlCommand();
        //private SqlDataAdapter adapter = new SqlDataAdapter();
        Class.Stock_Report stkRpt = new Class.Stock_Report();
        public Stock_Limits(int Type)
        {
            InitializeComponent();
            //conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
            //cmd.Connection = conn;
            MinimumStockTableAdapter.Connection = new System.Data.SqlClient.SqlConnection(Properties.Settings.Default.ConnectionStrings);
            string query = "SELECT StockValues.DESC_ENG as Name,StockValues.Code,QTY as Stock,StockValues.MinQty,StockValues.ReodrQty,StockValues.MQty FROM (select INV_ITEM_DIRECTORY.CODE,INV_ITEM_DIRECTORY.DESC_ENG,stock.QTY,Isnull(INV_ITEM_DIRECTORY.MINIMUM_QTY,0) as MINQTY,Isnull(INV_ITEM_DIRECTORY.REORDER_QTY,0) aS REODRQTY,(stock.QTY-Isnull(INV_ITEM_DIRECTORY.MINIMUM_QTY,0)) AS MQTY from INV_ITEM_DIRECTORY left outer join (select item_id,sum(qty) AS QTY from  tblstock group by item_id) AS STOCK ON INV_ITEM_DIRECTORY.CODE=STOCK.item_id where INV_ITEM_DIRECTORY.MINIMUM_QTY IS NOT NULL) AS StockValues";
            //if (conn.State == ConnectionState.Closed)
            //    conn.Open();
            DataTable dt = new DataTable();
            //SqlDataAdapter Adp = new SqlDataAdapter();
            //Adp.SelectCommand = cmd;
            //Adp.Fill(dt);
            dt = DbFunctions.GetDataTable(query);
            reportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource RDS1 = new ReportDataSource("MinimumStock", dt);
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            reportViewer1.LocalReport.DataSources.Add(RDS1);
            if (Type == 0)
            {
                drpselect.SelectedIndex = 2;
                ReportParameter p1 = new ReportParameter("param", drpselect.SelectedIndex.ToString());
                this.reportViewer1.LocalReport.SetParameters(p1);

                this.reportViewer1.RefreshReport();
            }
            else
            {
                drpselect.SelectedIndex = 0;
                ReportParameter p1 = new ReportParameter("param", drpselect.SelectedIndex.ToString());
                this.reportViewer1.LocalReport.SetParameters(p1);

                this.reportViewer1.RefreshReport();
            }
        }
       
        private void btnSearch_Click(object sender, EventArgs e)
        {
            //MinimumStockTableAdapter.Connection = new System.Data.SqlClient.SqlConnection(Properties.Settings.Default.ConnectionStrings);
            //cmd.Connection = conn;
            //cmd.CommandText = "SELECT StockValues.DESC_ENG as Name,StockValues.Code,QTY as Stock,StockValues.MinQty,StockValues.ReodrQty,StockValues.MQty FROM (select INV_ITEM_DIRECTORY.CODE,INV_ITEM_DIRECTORY.DESC_ENG,stock.QTY,Isnull(INV_ITEM_DIRECTORY.MINIMUM_QTY,0) as MINQTY,Isnull(INV_ITEM_DIRECTORY.REORDER_QTY,0) aS REODRQTY,(stock.QTY-Isnull(INV_ITEM_DIRECTORY.MINIMUM_QTY,0)) AS MQTY from INV_ITEM_DIRECTORY left outer join (select item_id,sum(qty) AS QTY from  tblstock group by item_id) AS STOCK ON INV_ITEM_DIRECTORY.CODE=STOCK.item_id where INV_ITEM_DIRECTORY.MINIMUM_QTY IS NOT NULL) AS StockValues";
            //if (conn.State == ConnectionState.Closed)
            //    conn.Open();
            //DataTable dt = new DataTable();
            //SqlDataAdapter Adp = new SqlDataAdapter();
            //Adp.SelectCommand = cmd;
            //Adp.Fill(dt);
            reportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource RDS1 = new ReportDataSource("MinimumStock", stkRpt.getMinimumStock());
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            reportViewer1.LocalReport.DataSources.Add(RDS1);

            if (drpselect.SelectedIndex == 0)
            {
                ReportParameter p1 = new ReportParameter("param", drpselect.SelectedIndex.ToString());
                this.reportViewer1.LocalReport.SetParameters(p1);

                this.reportViewer1.RefreshReport();
            }
            else if (drpselect.SelectedIndex == 2)
            {
                ReportParameter p1 = new ReportParameter("param", drpselect.SelectedIndex.ToString());
                this.reportViewer1.LocalReport.SetParameters(p1);

                this.reportViewer1.RefreshReport();
            }
            else if (drpselect.SelectedIndex == 1)
            {
                ReportParameter p1 = new ReportParameter("param", drpselect.SelectedIndex.ToString());
                this.reportViewer1.LocalReport.SetParameters(p1);

                this.reportViewer1.RefreshReport();
            }

        }


        public void SavePDF(ReportViewer viewer, string savePath)
        {
            try
            {
                //  byte[] Bytes = viewer.LocalReport.Render(format:"WORDOPENXML", deviceInfo: "");
                byte[] Bytes = viewer.LocalReport.Render(format: "EXCELOPENXML", deviceInfo: "");
                // byte[] Bytes = viewer.LocalReport.Render(format: "PDF", deviceInfo: "");
                using (FileStream stream = new FileStream(savePath, FileMode.Create))
                {
                    stream.Write(Bytes, 0, Bytes.Length);

                }
            }
            catch { }

            try
            {
                if (DialogResult.Yes == MessageBox.Show("Make sure you have configued outlook for sending email", "OutLook Config", MessageBoxButtons.YesNo))
                {
                    Microsoft.Office.Interop.Outlook.Application oApp = new Microsoft.Office.Interop.Outlook.Application();
                    Microsoft.Office.Interop.Outlook._MailItem oMailItem = (Microsoft.Office.Interop.Outlook._MailItem)oApp.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);

                    // body, bcc etc...

                    oMailItem.Attachments.Add((object)savePath, Microsoft.Office.Interop.Outlook.OlAttachmentType.olEmbeddeditem, 1, (object)"Attachment");
                    oMailItem.Display(true);
                }
            }
            catch
            {
                MessageBox.Show("You may not have Outlook Installed Please check");
            }
        }

        private void Send_Mail_Click(object sender, EventArgs e)
        {
            SavePDF(reportViewer1, Path.GetTempPath() + "StockLimit_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xlsx");
            //    SavePDF(reportViewer1, Path.GetTempPath() + "salesvariation" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".docx");
            //  SavePDF(reportViewer1, Path.GetTempPath() + "salesvariation" + DateTime.Now.ToString("yyyyMMddHHmmssfff")+".pdf");
       
        }
    }
}
