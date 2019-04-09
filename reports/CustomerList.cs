using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sys_Sols_Inventory.reports
{
    public partial class CustomerList : Form
    {
        public CustomerList()
        {
            InitializeComponent();
        }

        private void CustomerList_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'DataSetcustomer.CUSTOMERLIST' table. You can move, or remove it, as needed.
           
            // TODO: This line of code loads data into the 'NewPurchase.DataTable1' table. You can move, or remove it, as needed.
           

            //this.reportViewer1.RefreshReport();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            string conection = Properties.Settings.Default.ConnectionStrings.ToString();
            CUSTOMERLISTTableAdapter.Connection = new System.Data.SqlClient.SqlConnection(conection);
            this.CUSTOMERLISTTableAdapter.Fill(this.DataSetcustomer.CUSTOMERLIST,SUP_CODE.Text);
            this.reportViewer1.RefreshReport();
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

        private void Btn_Clear_Click(object sender, EventArgs e)
        {
            SUP_CODE.Text = "";
            SUP_NAME.Text = "";
        }

        private void Send_Mail_Click(object sender, EventArgs e)
        {
            SavePDF(reportViewer1, Path.GetTempPath() + "CustomerLists_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xlsx");
            //    SavePDF(reportViewer1, Path.GetTempPath() + "salesvariation" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".docx");
            //  SavePDF(reportViewer1, Path.GetTempPath() + "salesvariation" + DateTime.Now.ToString("yyyyMMddHHmmssfff")+".pdf");
        
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
    }
}
