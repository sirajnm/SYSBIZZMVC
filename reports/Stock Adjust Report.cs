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

namespace Sys_Sols_Inventory.reports
{
    public partial class Stock_Adjust_Report : Form
    {
        //private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        //private SqlCommand cmd = new SqlCommand();
        //private SqlDataAdapter adapter = new SqlDataAdapter();
        private BindingSource source = new BindingSource();
        Class.CompanySetup Comset = new Class.CompanySetup();
        public Stock_Adjust_Report()
        {
            InitializeComponent();
        }

        private void Stock_Adjust_Report_Load(object sender, EventArgs e)
        {
           
            bindgridview();
            GetBranches();
         
 
        }

        public void GetBranches()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = Comset.getbaranchs();
                drpBranch.DataSource = dt;
                drpBranch.DisplayMember = "DESC_ENG";
                drpBranch.ValueMember = "CODE";
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message + ", Branch binding error");
            }
        }
        public void bindgridview()
        {
            try
            {
                //cmd.Connection = conn;
                string cmd = "SELECT        INV_ITEM_DIRECTORY.DESC_ENG AS [Item Name],CODE FROM   INV_ITEM_DIRECTORY ";
               // cmd.CommandType = CommandType.Text;
                DataTable dt = new DataTable();
                //adapter.SelectCommand = cmd;
               // adapter.Fill(dt);
                dt = Model.DbFunctions.GetDataTable(cmd);
                source.DataSource = dt;

                dataGridItem.DataSource = source;

                dataGridItem.RowHeadersVisible = false;
                dataGridItem.Columns[0].Width = 200;
                dataGridItem.Columns[1].Visible = false;
                dataGridItem.ClearSelection();

            }
            catch
            {
            }


        }
       
        private void DESC_ENG_TextChanged(object sender, EventArgs e)
        {
            if (DESC_ENG.Text != "")
            {
                dataGridItem.Visible = true;
                source.Filter = string.Format("[Item Name] LIKE '%{0}%' ", DESC_ENG.Text);
                dataGridItem.ClearSelection();
            }
            else
            {
                dataGridItem.Visible = false;

            }
        }

        private void dataGridItem_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                {
                    txtCode.Text = dataGridItem.CurrentRow.Cells[1].Value.ToString();
                    DESC_ENG.Text = dataGridItem.CurrentRow.Cells[0].Value.ToString();

                    dataGridItem.Visible = false;
                }
            }
            catch
            { }
        }

        private void DESC_ENG_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Down)
            {
                if (dataGridItem.Visible == true)
                {
                    dataGridItem.Focus();
                    dataGridItem.CurrentCell = dataGridItem.Rows[0].Cells[0];
                }

            }
            else if (e.KeyData == Keys.Escape)
            {
                dataGridItem.Visible = false;
                DESC_ENG.Text = "";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //string conection = Properties.Settings.Default.ConnectionStrings.ToString();
           // StockADJCodeTableAdapter.Connection = new System.Data.SqlClient.SqlConnection(conection);
            StockADJCodeTableAdapter.Connection = Model.DbFunctions.GetConnection();
            if (checkBox1.Checked == false)
            {
                this.StockADJCodeTableAdapter.Fill(this.StockADJDS.StockADJCode, txtCode.Text);
                this.reportViewer1.RefreshReport();
            }
            else
            {
                this.StockADJCodeTableAdapter.FillByDate(this.StockADJDS.StockADJCode, txtCode.Text,Convert.ToDateTime(StartDate.Value.ToShortDateString()),Convert.ToDateTime( EndDate.Value.ToShortDateString()));
                this.reportViewer1.RefreshReport();
            }
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                groupBox1.Enabled = true;
            }
            else
            {
                groupBox1.Enabled = false;
            }
        }

        private void dataGridItem_Leave(object sender, EventArgs e)
        {
            dataGridItem.Visible = false;
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
