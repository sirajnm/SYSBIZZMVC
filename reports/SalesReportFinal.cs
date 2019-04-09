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
    public partial class SalesReportFinal : Form
    {
        private bool HasType = true;
        private bool HasGroup = true;
        private bool HasCategory = true;
        private bool HasTM = true;
        Class.Stock_Report stkrpt = new Class.Stock_Report();
        DataTable dt = new DataTable();
        public SalesReportFinal()
        {
            InitializeComponent();
            HasType = General.IsEnabled(Settings.HasType);
            HasCategory = General.IsEnabled(Settings.HasCategory);
            HasGroup = General.IsEnabled(Settings.HasGroup);
            HasTM = General.IsEnabled(Settings.HasTM);
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
                dt = stkrpt.BindSTypes();

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

        private void SalesReportFinal_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'SaleDataNew.DataTable1' table. You can move, or remove it, as needed.
          
            // TODO: This line of code loads data into the 'SaleDataNew.DataTable1' table. You can move, or remove it, as needed.
           
            // TODO: This line of code loads data into the 'SaleDataNew.DataTable1' table. You can move, or remove it, as needed.
           
            // TODO: This line of code loads data into the 'SaleDataNew.DataTable1' table. You can move, or remove it, as needed.
         
            // TODO: This line of code loads data into the 'SaleDataNew.DataTable1' table. You can move, or remove it, as needed.
          
            // TODO: This line of code loads data into the 'SaleDataNew.DataTable1' table. You can move, or remove it, as needed.
            
            // TODO: This line of code loads data into the 'SaleDataNew.DataTable1' table. You can move, or remove it, as needed.
            
            // TODO: This line of code loads data into the 'SaleDataNew.DataTable1' table. You can move, or remove it, as needed.
           
            // TODO: This line of code loads data into the 'SaleDataNew.DataTable1' table. You can move, or remove it, as needed.
            //this.DataTable1TableAdapter.Fill(this.SaleDataNew.DataTable1);
            // TODO: This line of code loads data into the 'SaleDataNew.DataTable1' table. You can move, or remove it, as needed.
           
            // TODO: This line of code loads data into the 'SaleDataNew.DataTable1' table. You can move, or remove it, as needed.
            
            // TODO: This line of code loads data into the 'SaleDataNew.DataTable1' table. You can move, or remove it, as needed.
          
            // TODO: This line of code loads data into the 'SaleDataNew.DataTable1' table. You can move, or remove it, as needed.
            
          
           
            BindCategory();
            BindTradeMark();
            BindGroup();
            BindpurchaseType();
            BindType();
            BindCustomer();

           
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            string conection = Properties.Settings.Default.ConnectionStrings.ToString();
            DataTable1TableAdapter.Connection = new System.Data.SqlClient.SqlConnection(conection);
            if (Chk.Checked == false)
            {

                ReportParameter[] parameters = new ReportParameter[5];
                parameters[0] = new ReportParameter("Date", "false");

                if (HasType)
                    parameters[1] = new ReportParameter("Type", "True");
                else
                    parameters[1] = new ReportParameter("Type", "false");
                if (HasGroup)
                    parameters[2] = new ReportParameter("Group", "True");
                else
                    parameters[2] = new ReportParameter("Group", "false");
                if (HasCategory)
                    parameters[3] = new ReportParameter("Category", "True");
                else
                    parameters[3] = new ReportParameter("Category", "false");
                if (HasTM)
                    parameters[4] = new ReportParameter("Tm", "True");
                else
                    parameters[4] = new ReportParameter("Tm", "false");
                this.reportViewer1.LocalReport.SetParameters(parameters);
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
                //this.DataTable1TableAdapter.Fill(this.NewSales.DataTable1, grp, cat, tm, sup, typ, doc);
               

                this.DataTable1TableAdapter.Fill(this.SaleDataNew.DataTable1,grp,cat,tm,sup,typ,doc);
                this.reportViewer1.RefreshReport();

            }
            else
            {
                ReportParameter[] parameters = new ReportParameter[5];
                parameters[0] = new ReportParameter("Date", "true");
                if (HasType)
                    parameters[1] = new ReportParameter("Type", "True");
                else
                    parameters[1] = new ReportParameter("Type", "false");
                if (HasGroup)
                    parameters[2] = new ReportParameter("Group", "True");
                else
                    parameters[2] = new ReportParameter("Group", "false");
                if (HasCategory)
                    parameters[3] = new ReportParameter("Category", "True");
                else
                    parameters[3] = new ReportParameter("Category", "false");
                if (HasTM)
                    parameters[4] = new ReportParameter("Tm", "True");
                else
                    parameters[4] = new ReportParameter("Tm", "false");
                this.reportViewer1.LocalReport.SetParameters(parameters);
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
                datestart =Convert.ToDateTime(StartDate.Value.ToShortDateString());
                dateend = Convert.ToDateTime(EndDate.Value.ToShortDateString());
                this.DataTable1TableAdapter.FillByDate(this.SaleDataNew.DataTable1, grp, cat, tm, sup, typ, doc,datestart,dateend);
                this.reportViewer1.RefreshReport();
            //    this.DataTable1TableAdapter.FillByDate(this.NewSales.DataTable1, grp, cat, tm, sup, typ, doc, datestart, dateend);
            //    this.reportViewer1.RefreshReport();

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
            SavePDF(reportViewer1, Path.GetTempPath() + "SalesReport_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xlsx");
            //    SavePDF(reportViewer1, Path.GetTempPath() + "salesvariation" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".docx");
            //  SavePDF(reportViewer1, Path.GetTempPath() + "salesvariation" + DateTime.Now.ToString("yyyyMMddHHmmssfff")+".pdf");
     
        }

        private void Category_Enter(object sender, EventArgs e)
        {

        }
    }
}
