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

namespace Sys_Sols_Inventory
{

    public partial class Current_Stock : Form
    {
        
        Class.Stock_Report stkrpt = new Class.Stock_Report();
        Class.CompanySetup Comset = new Class.CompanySetup();
        DataTable dt = new DataTable();
        Initial mdi = (Initial)Application.OpenForms["Initial"];
        public Current_Stock()
        {
            InitializeComponent();
        }
        public void GetBranches()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = Comset.getbaranchs();
                DataRow row = dt.NewRow();
                row[0] = "";
                dt.Rows.InsertAt(row, 0);
                drpBranch.DataSource = dt;
                drpBranch.DisplayMember = "DESC_ENG";
                drpBranch.ValueMember = "CODE";
                drpBranch.SelectedIndex = -1;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message + ", Branch binding error");
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {



            if (keyData == (Keys.Escape))
            {
                //   this.Close();
                //ComponentFactory.Krypton.Docking.KryptonDockableNavigator n = mdi.sender as ComponentFactory.Krypton.Docking.KryptonDockableNavigator;
                ComponentFactory.Krypton.Navigator.KryptonPage k = new ComponentFactory.Krypton.Navigator.KryptonPage();
                k = mdi.maindocpanel.SelectedPage;
                if (k.Name == "Home")
                {


                }
                else
                {
                    mdi.maindocpanel.Pages.Remove(k);
                }
            }
            //  else if (e.KeyCode == Keys.S && e.Control)
            else if (keyData == (Keys.Alt | Keys.S))
            {


            }
            if (keyData == (Keys.F3))
            {

            }
            else if (keyData == (Keys.Alt | Keys.N))
            {



            }




            return base.ProcessCmdKey(ref msg, keyData);

        }
        private void Current_Stock_Load(object sender, EventArgs e)
        {
            BindType();
            BindGroup();
            BindCategory();
            BindTradeMark();
            GetBranches();
            btnSave.PerformClick();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            GetCurrentStockTableAdapter.Connection = Model.DbFunctions.GetConnection();
            try
            {
                if (Valid())
                {
                    string type = Convert.ToString(TYPE.SelectedValue);
                    type = type.Equals("") ? null : type;
                    string group = Convert.ToString(Group.SelectedValue);
                    group = group.Equals("") ? null : group;
                    string cat = Convert.ToString(DrpCategory.SelectedValue);
                    cat = cat.Equals("") ? null : cat;
                    string brand = Convert.ToString(Trademark.SelectedValue);
                    brand = brand.Equals("") ? null : brand;
                    string branch = Convert.ToString(drpBranch.SelectedValue);
                    branch = branch.Equals("") ? null : branch;
                    string name = DESC_ENG.Text.Equals("") ? null : DESC_ENG.Text;
                    this.GetCurrentStockTableAdapter.Fill(this.AIN_INVENTORYDataSet1.GetCurrentStock, type, group, cat, brand, branch, name);
                    ReportParameter param = new ReportParameter("decimalFormat", Common.getDecimalFormatCount().ToString());
                    reportViewer1.LocalReport.SetParameters(param);
                    reportViewer1.RefreshReport();
                    try
                    {
                        reportViewer1.CurrentPage = reportViewer1.GetTotalPages();
                    }
                    catch
                    {
                    }
                }
            }
            catch(Exception EE)
            {
                MessageBox.Show(EE.Message);
            }
        }
        public void BindType()
        {
            dt = stkrpt.BindType();
            TYPE.ValueMember = "CODE";
            TYPE.DisplayMember = "DESC_ENG";
            DataRow row = dt.NewRow();
            row[0] = "";
            dt.Rows.InsertAt(row, 0);
            TYPE.DataSource = dt;
        }
        public void BindCategory()
        {
            dt = stkrpt.BindCategory();
            DrpCategory.ValueMember = "CODE";
            DrpCategory.DisplayMember = "DESC_ENG";
            DataRow row = dt.NewRow();
            row[0] = "";
            dt.Rows.InsertAt(row, 0);
            DrpCategory.DataSource = dt;
        }
        public void BindGroup()
        {
            dt = stkrpt.BindGroup();
            Group.ValueMember = "CODE";
            Group.DisplayMember = "DESC_ENG";
            DataRow row = dt.NewRow();
            row[0] = "";
            dt.Rows.InsertAt(row, 0);
            Group.DataSource = dt;
        }
        public void BindTradeMark()
        {
            dt = stkrpt.BindTrademark();
            Trademark.ValueMember = "CODE";
            Trademark.DisplayMember = "DESC_ENG";
            DataRow row = dt.NewRow();
            row[0] = "";
            dt.Rows.InsertAt(row, 0);
            Trademark.DataSource = dt;
        }
        public bool Valid()
        {
            return true;
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
        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            SavePDF(reportViewer1, Path.GetTempPath() + "PurchaseRport_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xlsx");
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            reports.stkreport a = new reports.stkreport();
            a.Show();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Stock_Report.Stock_Report stock_Report = new Stock_Report.Stock_Report();
            stock_Report.ShowDialog();
        }
    }
}
