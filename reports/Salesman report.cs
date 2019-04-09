using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sys_Sols_Inventory.reports
{
    public partial class Salesman_report : Form
    {
        Class.Salesman_Report srpt = new Class.Salesman_Report();

        public Salesman_report()
        {
            InitializeComponent();
        }

        private void Salesman_report_Load(object sender, EventArgs e)
        {            
            DataTable sm=srpt.salesman_Details();
            cmb_sman.DataSource = sm;
            Btn_Search.PerformClick();
            this.reportViewer1.RefreshReport();
        }

        private void Btn_Search_Click(object sender, EventArgs e)
        {
            if (!chk_date.Checked)
            {
                try
                {
                    reportViewer1.LocalReport.DataSources.Clear();
                    srpt.Saleman = cmb_sman.Text;
                    DataTable dt = srpt.Report_All();
                    ReportDataSource rds = new ReportDataSource("SalesMan", dt);
                    ReportParameter[] rp = new ReportParameter[3];
                    rp[0] = new ReportParameter("FromDate", dtp_from.Value.ToShortDateString());
                    rp[1] = new ReportParameter("ToDate", dtp_to.Value.ToShortDateString());
                    rp[2] = new ReportParameter("RptType", "All");
                    reportViewer1.LocalReport.SetParameters(rp);
                    reportViewer1.LocalReport.DataSources.Add(rds);
                    reportViewer1.LocalReport.Refresh();
                    reportViewer1.RefreshReport();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                try
                {
                    reportViewer1.LocalReport.DataSources.Clear();
                    srpt.Saleman = cmb_sman.Text;
                    srpt.Date1 = dtp_from.Value;
                    srpt.Date2 = dtp_to.Value;
                    DataTable dt = srpt.Report_All_byDate();
                    ReportDataSource rds = new ReportDataSource("SalesMan", dt);
                    ReportParameter[] rp = new ReportParameter[3];
                    rp[0] = new ReportParameter("FromDate", dtp_from.Value.ToShortDateString());
                    rp[1] = new ReportParameter("ToDate", dtp_to.Value.ToShortDateString());
                    rp[2] = new ReportParameter("RptType", "Date");
                    reportViewer1.LocalReport.SetParameters(rp);
                    reportViewer1.LocalReport.DataSources.Add(rds);
                    reportViewer1.LocalReport.Refresh();
                    reportViewer1.RefreshReport();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void chk_date_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_date.Checked)
            {
                dtp_from.Enabled = true;
                dtp_to.Enabled = true;
            }
            else
            {
                dtp_from.Enabled = false;
                dtp_to.Enabled = false;
            }
        }
    }
}
