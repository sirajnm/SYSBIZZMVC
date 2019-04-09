using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace Sys_Sols_Inventory.reports
{
    public partial class Salesreportview : Form
    {

        private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        private SqlCommand cmd = new SqlCommand();
        private SqlDataAdapter adapter = new SqlDataAdapter();

        public Salesreportview()
        {
            InitializeComponent();
        }

        private void Salesreportview_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'salesDataSet.DataTable3' table. You can move, or remove it, as needed.
              this.DataTable3TableAdapter.Fill(this.salesDataSet.DataTable3);
            
            // TODO: This line of code loads data into the 'salesDataSet.DataTable2' table. You can move, or remove it, as needed.
            this.DataTable2TableAdapter.Fill(this.salesDataSet.DataTable2);
            // TODO: This line of code loads data into the 'salesDataSet.DataTable1' table. You can move, or remove it, as needed.
            this.DataTable1TableAdapter.Fill(this.salesDataSet.DataTable1);

       }


        public bool valid()
        {
            if (Drpselect.Text == "--select--")
            {
                MessageBox.Show("select one field to generate report");
                return false;
            }
            else
                return true;
        }
       
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (valid())
            {
                ReportParameter[] parameters = new ReportParameter[14];
                if (chkitemcode.Checked == true)
                {
                    parameters[0] = new ReportParameter("code", "true");
                }
                else
                {
                    parameters[0] = new ReportParameter("code", "false");
                }
                if (chkItemname.Checked == true)
                {
                    parameters[1] = new ReportParameter("Item", "True");
                }
                else
                {
                    parameters[1] = new ReportParameter("Item", "false");
                }



                if (chktype.Checked == true)
                {
                    parameters[2] = new ReportParameter("type", "true");
                }
                else
                {
                    parameters[2] = new ReportParameter("type", "false");
                }

                if (ChkGroup.Checked == true)
                {
                    parameters[3] = new ReportParameter("group", "true");
                }
                else
                {
                    parameters[3] = new ReportParameter("group", "false");
                }

                if (ChkCategory.Checked == true)
                {
                    parameters[4] = new ReportParameter("category", "true");
                }
                else
                {
                    parameters[4] = new ReportParameter("category", "false");
                }
                if (ChkTrademark.Checked == true)
                {
                    parameters[5] = new ReportParameter("trademark", "true");
                }
                else
                {
                    parameters[5] = new ReportParameter("trademark", "false");
                }
                if (ckkuom.Checked == true)
                {
                    parameters[6] = new ReportParameter("uom", "true");
                }
                else
                {
                    parameters[6] = new ReportParameter("uom", "false");
                }
                if (chksale.Checked == true)
                {
                    parameters[7] = new ReportParameter("quantity", "true");
                }
                else
                {
                    parameters[7] = new ReportParameter("quantity", "false");
                }
                if (chkdiscount.Checked == true)
                {
                    parameters[8] = new ReportParameter("discount", "true");
                }
                else
                {
                    parameters[8] = new ReportParameter("discount", "false");
                }
                if (chksalestotal.Checked == true)
                {
                    parameters[9] = new ReportParameter("gross", "true");
                }
                else
                {
                    parameters[9] = new ReportParameter("gross", "false");

                }

                
               
                    parameters[10] = new ReportParameter("itemtotal", "false");

                
                if (chktotal.Checked == true)
                {
                    parameters[11] = new ReportParameter("total", "true");
                }
                else
                {
                    parameters[11] = new ReportParameter("total", "false");

                }
                if (chksalestype.Checked == true)
                {
                    parameters[13] = new ReportParameter("salestype", "true");
                }
                else
                {
                    parameters[13] = new ReportParameter("salestype", "false");

                }
                string dropdown = Drpselect.SelectedItem.ToString();

                switch (dropdown)
                {
                    case "Product":
                        parameters[12] = new ReportParameter("parentgroup", "P");
                       
                        this.reportViewer1.LocalReport.SetParameters(parameters);
                        salesDataSetTableAdapters.DataTable1TableAdapter da1 = new salesDataSetTableAdapters.DataTable1TableAdapter();
                        salesDataSet.DataTable1DataTable tab = new salesDataSet.DataTable1DataTable();
                        da1.FillByproduct(tab, cbx_productname.SelectedValue.ToString());
                        ReportDataSource newdatasource2 = new ReportDataSource("DataSet1", (DataTable)tab);
                        ReportDataSource newda1 = new ReportDataSource("DataSet2", "");
                        ReportDataSource newda2= new ReportDataSource("DataSet3", "");
                       this.DataTable1TableAdapter.Fill(this.salesDataSet.DataTable1);
                        this.reportViewer1.LocalReport.DataSources.Clear();
                        this.reportViewer1.LocalReport.DataSources.Add(newdatasource2);
                        this.reportViewer1.LocalReport.DataSources.Add(newda1);
                        this.reportViewer1.LocalReport.DataSources.Add(newda2);
                        this.reportViewer1.LocalReport.Refresh();
                        this.reportViewer1.RefreshReport();
                        break;

                    case "Date":
                        parameters[12] = new ReportParameter("parentgroup", "D");
                        
                        this.reportViewer1.LocalReport.SetParameters(parameters);


                        salesDataSetTableAdapters.DataTable2TableAdapter sda = new salesDataSetTableAdapters.DataTable2TableAdapter();
                        salesDataSet.DataTable2DataTable table = new salesDataSet.DataTable2DataTable();

                        sda.FillBy(table, Convert.ToDateTime(Date_Start.Text), Convert.ToDateTime(Date_end.Text));
                        ReportDataSource newdatasource = new ReportDataSource("DataSet2", (DataTable)table);
                        ReportDataSource newd1 = new ReportDataSource("DataSet1", "");
                        ReportDataSource newd2 = new ReportDataSource("DataSet3", "");
                        this.DataTable1TableAdapter.Fill(this.salesDataSet.DataTable1);
                        this.reportViewer1.LocalReport.DataSources.Clear();
                        this.reportViewer1.LocalReport.DataSources.Add(newdatasource);
                        this.reportViewer1.LocalReport.DataSources.Add(newd1);
                        this.reportViewer1.LocalReport.DataSources.Add(newd2);
                        this.reportViewer1.LocalReport.Refresh();
                        this.reportViewer1.RefreshReport();

                        break;
                    default:
                        parameters[12] = new ReportParameter("parentgroup", "c");
                       
                        break;

                    case "Customer":
                        parameters[12] = new ReportParameter("parentgroup", "C");
                       
                        this.reportViewer1.LocalReport.SetParameters(parameters);
                        salesDataSetTableAdapters.DataTable3TableAdapter sda1 = new salesDataSetTableAdapters.DataTable3TableAdapter();
                        salesDataSet.DataTable3DataTable tbl = new salesDataSet.DataTable3DataTable();
                        sda1.FillBy(tbl, cbx_customer.SelectedValue.ToString());
                        ReportDataSource newdatasource1 = new ReportDataSource("DataSet3", (DataTable)tbl);
                        ReportDataSource newdata1 = new ReportDataSource("DataSet1","");
                        ReportDataSource newdata2 = new ReportDataSource("DataSet2","");
                        this.DataTable1TableAdapter.Fill(this.salesDataSet.DataTable1);
                        this.reportViewer1.LocalReport.DataSources.Clear();
                        this.reportViewer1.LocalReport.DataSources.Add(newdatasource1);
                        this.reportViewer1.LocalReport.DataSources.Add(newdata1);
                        this.reportViewer1.LocalReport.DataSources.Add(newdata2);
                        this.reportViewer1.LocalReport.Refresh();
                        this.reportViewer1.RefreshReport();
                        break;
                }


            }
          

            
        }

        private void panel3_VisibleChanged(object sender, EventArgs e)
        {
            if (panel3.Visible == true)
            {
                try
                {
                    cmd.CommandText = "SELECT     CODE, DESC_ENG FROM  INV_ITEM_DIRECTORY ";
                    cmd.Connection = conn;
                    DataTable dt = new DataTable();
                    adapter.SelectCommand = cmd;
                    adapter.Fill(dt);
                    cbx_productname.DataSource = dt;
                      cbx_productname.DisplayMember = "DESC_ENG";
                      cbx_productname.ValueMember = "DESC_ENG";

                }
                catch
                {
                }
            }
        }

      

        private void Drpselect_SelectedIndexChanged(object sender, EventArgs e)
        {
            string dropdown = Drpselect.SelectedItem.ToString();

            switch (dropdown)
            { 
                case "Product":
                    panel3.Visible = true;
                     paneldate.Visible = false;
                     panelCustomer.Visible = false;
                     chkItemname.Enabled = false;

                break;
                case "Date":
                     paneldate.Visible = true;
                     panel3.Visible =false;
                     panelCustomer.Visible = false;
                     chkItemname.Enabled = true;
                break;
                case "Customer" :
                     paneldate.Visible = false;
                     panel3.Visible =false;
                     panelCustomer.Visible = true;
                     chkItemname.Enabled = true;
                break;
            }
           
        }

        private void panelCustomer_VisibleChanged(object sender, EventArgs e)
        {
            if (panelCustomer.Visible == true)
            {
                try
                {
                    cmd.CommandText = "SELECT     DESC_ENG FROM         REC_CUSTOMER ";
                    cmd.Connection = conn;
                    DataTable dt = new DataTable();
                    adapter.SelectCommand = cmd;
                    adapter.Fill(dt);
                    cbx_customer.DataSource = dt;
                    cbx_customer.DisplayMember = "DESC_ENG";
                    cbx_customer.ValueMember = "DESC_ENG";

                }
                catch
                {
                }
            }
        }

       
}
}
