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
    public partial class FastMovingItem : Form
    {
        Class.Stock_Report stkrpt = new Class.Stock_Report();
        DataTable dt = new DataTable();
        public FastMovingItem()
        {
            InitializeComponent();
        }

        private void FastMovingItem_Load(object sender, EventArgs e)
        {
            BindType();
            BindGroup();
            BindCategory();
            BindCustomer();
            BindpurchaseType();
        }

        public void BindType()
        {
            try
            {
                dt = stkrpt.BindType();



                TYPE.ValueMember = "CODE";
                TYPE.DisplayMember = "DESC_ENG";


                DataRow row = dt.NewRow();
                row[0] = "";
                dt.Rows.InsertAt(row, 0);
                TYPE.DataSource = dt;
            }
            catch
            {
            }
        }
        public void BindCategory()
        {

            try
            {
                dt = stkrpt.BindCategory();

                DrpCategory.ValueMember = "CODE";
                DrpCategory.DisplayMember = "DESC_ENG";

                DataRow row = dt.NewRow();
                row[0] = "";
                dt.Rows.InsertAt(row, 0);
                DrpCategory.DataSource = dt;

            }
            catch
            {
            }
        }
        public void BindGroup()
        {

            try
            {
                dt = stkrpt.BindGroup();

                Group.ValueMember = "CODE";
                Group.DisplayMember = "DESC_ENG";

                DataRow row = dt.NewRow();
                row[0] = "";
                dt.Rows.InsertAt(row, 0);
                Group.DataSource = dt;
            }
            catch
            {
            }
        }
        public void BindTradeMark()
        {
            try
            {
                dt = stkrpt.BindTrademark();

                Trademark.ValueMember = "CODE";
                Trademark.DisplayMember = "DESC_ENG";

                DataRow row = dt.NewRow();
                row[0] = "";
                dt.Rows.InsertAt(row, 0);
                Trademark.DataSource = dt;
            }
            catch
            {
            }
        }
        public void BindCustomer()
        {
            try
            {
                dt = stkrpt.BindCustomer();

                Cbx_supplier.ValueMember = "CODE";
                Cbx_supplier.DisplayMember = "DESC_ENG";

                DataRow row = dt.NewRow();
                row[0] = "";
                dt.Rows.InsertAt(row, 0);
                Cbx_supplier.DataSource = dt;
            }
            catch
            {
            }
        }

        public void BindpurchaseType()
        {
            try
            {
                dt = stkrpt.BindSTypesWithoutReturn();

                Cbx_salestype.ValueMember = "CODE";
                Cbx_salestype.DisplayMember = "DESC_ENG";

                DataRow row = dt.NewRow();
                row[0] = "";
                dt.Rows.InsertAt(row, 0);
                Cbx_salestype.DataSource = dt;
            }
            catch
            {
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //string conection = Properties.Settings.Default.ConnectionStrings.ToString();
            //FastmovingwithdateTableAdapter.Connection = new System.Data.SqlClient.SqlConnection(conection);
            FastmovingwithdateTableAdapter.Connection = Model.DbFunctions.GetConnection();

            DateTime datestart, dateend;
            string typ = "", grp = "", cat = "", tm = "", doc = "", sup = "";
            if (TYPE.Text != "")
                typ = TYPE.SelectedValue.ToString();
            if (Group.Text != "")
                grp = Group.SelectedValue.ToString();
            if (DrpCategory.Text != "")
                cat = DrpCategory.SelectedValue.ToString();
            if (Trademark.Text != "")
                tm = Trademark.SelectedValue.ToString();
            if (Cbx_salestype.Text != "")
                doc = Cbx_salestype.SelectedValue.ToString();
            if (Cbx_supplier.Text != "")
                sup = Cbx_supplier.SelectedValue.ToString();
            if (Chk.Checked == true)
            {
                datestart = Convert.ToDateTime(StartDate.Value.ToShortDateString());
                dateend = Convert.ToDateTime(EndDate.Value.ToShortDateString());
                // TODO: This line of code loads data into the 'fastmoving.Fastmovingwithdate' table. You can move, or remove it, as needed.
                this.FastmovingwithdateTableAdapter.Fill(this.fastmoving.Fastmovingwithdate, grp, tm, cat, doc,sup, typ, datestart, dateend);

                this.reportViewer1.RefreshReport();
            }
            else
            {
                this.FastmovingwithdateTableAdapter.FillBy(this.fastmoving.Fastmovingwithdate, grp, tm, cat, doc, sup, typ);

                this.reportViewer1.RefreshReport();

            }
        }

        private void Chk_CheckedChanged(object sender, EventArgs e)
        {
            if (Chk.Checked == true)
            {
                StartDate.Enabled = true;
                EndDate.Enabled = true;
            }
            else
            {
                StartDate.Enabled = false;
                EndDate.Enabled = false;
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
            SavePDF(reportViewer1, Path.GetTempPath() + "FastMoving_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xlsx");
            //    SavePDF(reportViewer1, Path.GetTempPath() + "salesvariation" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".docx");
            //  SavePDF(reportViewer1, Path.GetTempPath() + "salesvariation" + DateTime.Now.ToString("yyyyMMddHHmmssfff")+".pdf");
       
        }
    }
}
