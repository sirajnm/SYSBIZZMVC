using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace Sys_Sols_Inventory.reports
{
    public partial class PurchaseReportSearch : Form
    {

        private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        private SqlCommand cmd = new SqlCommand();
        private SqlDataAdapter adapter = new SqlDataAdapter();
        Class.CompanySetup cs = new Class.CompanySetup();
        public string CompanyName, Address;

        public PurchaseReportSearch()
        {
            InitializeComponent();
        }

      
       
        private void PurchaseReportSearch_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'PurchaseDataSet.DataTable1' table. You can move, or remove it, as needed.
           // this.DataTable1TableAdapter.Fill(this.PurchaseDataSet.DataTable1);
            // TODO: This line of code loads data into the 'PurchaseDataSet.DataTable2' table. You can move, or remove it, as needed.
          //  this.DataTable2TableAdapter.Fill(this.PurchaseDataSet.DataTable2);
            // TODO: This line of code loads data into the 'PurchaseDataSet.DataTable3' table. You can move, or remove it, as needed.
           // this.DataTable3TableAdapter.Fill(this.PurchaseDataSet.DataTable3);
         
         
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
           
            ReportParameter[] parameters = new ReportParameter[14];
            if (chkItemname.Checked == true)
            {
                parameters[0] = new ReportParameter("item", "true");
            }
            else
            {
                parameters[0] = new ReportParameter("item", "false");
            }
           



            if (chktype.Checked == true)
            {
                parameters[1] = new ReportParameter("type", "true");
            }
            else
            {
                parameters[1] = new ReportParameter("type", "false");
            }

            if (ChkUom.Checked == true)
            {
                parameters[2] = new ReportParameter("UOM", "true");
            }
            else
            {
                parameters[2] = new ReportParameter("UOM", "false");
            }
            if (ChkCategory.Checked == true)
            {
                parameters[3] = new ReportParameter("category", "true");
            }
            else
            {
                parameters[3] = new ReportParameter("category", "false");
            }
            if (ChkCategory.Checked == true)
            {
                parameters[3] = new ReportParameter("category", "true");
            }
            else
            {
                parameters[3] = new ReportParameter("category", "false");
            }
            if (chkpurchase.Checked == true)
            {
                parameters[4] = new ReportParameter("purchase", "true");
            }
            else
            {
                parameters[4] = new ReportParameter("purchase", "false");
            }
            if (ChkTrademark.Checked == true)
            {
                parameters[5] = new ReportParameter("trademark", "true");
            }
            else
            {
                parameters[5] = new ReportParameter("trademark", "false");
            }

            if (chkpurchasetype.Checked == true)
            {
                parameters[6] = new ReportParameter("purctype", "true");
            }
            else
            {
                parameters[6] = new ReportParameter("purctype", "false");
            }
            if (chkpurchasetotal.Checked == true)
            {
                parameters[7] = new ReportParameter("purchasetotal", "true");
            }
            else
            {
                parameters[7] = new ReportParameter("purchasetotal", "false");
            }
            if (chkItemname.Checked == true)
            {
                parameters[8] = new ReportParameter("itemname", "true");
            }
            else
            {
                parameters[8] = new ReportParameter("itemname", "false");
            }
            if (chkgroup.Checked == true)
            {
                parameters[9] = new ReportParameter("group", "true");
            }
            else
            {
                parameters[9] = new ReportParameter("group", "false");
            }
            if (cbx_Purchasediscount.Checked == true)
            {
                parameters[10] = new ReportParameter("discount", "true");
            }
            else
            {
                parameters[10] = new ReportParameter("discount", "false");
            }

            if (cbx_Purchasediscount.Checked == true)
            {
                parameters[11] = new ReportParameter("code", "true");
            }
            else
            {
                parameters[11] = new ReportParameter("code", "false");
            }

            getaddress();
            parameters[13] = new ReportParameter("CompanyName", CompanyName);
            string dropdown = drpselect.SelectedItem.ToString();
            switch (dropdown)
            {
                case "Product":
                    parameters[12] = new ReportParameter("filter", "P");
                    this.reportViewer1.LocalReport.SetParameters(parameters);
                    PurchaseDataSetTableAdapters.DataTable1TableAdapter dadap = new PurchaseDataSetTableAdapters.DataTable1TableAdapter();
                    PurchaseDataSet.DataTable1DataTable table = new PurchaseDataSet.DataTable1DataTable();
                    dadap.FillBy(table, cbx_product.SelectedValue.ToString());
                    ReportDataSource newdatasource1 = new ReportDataSource("DataSet1", (DataTable)table);
                    ReportDataSource newdatasource2 = new ReportDataSource("DataSet2", "");
                    ReportDataSource newdatasource3 = new ReportDataSource("DataSet3", "");
                     // this.DataTable1TableAdapter.Fill(this.PurchaseDataSet.DataTable1);
                    //  this.DataTable2TableAdapter.Fill(this.PurchaseDataSet.DataTable2);
                        this.reportViewer1.LocalReport.DataSources.Clear();
                        this.reportViewer1.LocalReport.DataSources.Add(newdatasource1);
                        this.reportViewer1.LocalReport.DataSources.Add(newdatasource2);
                        this.reportViewer1.LocalReport.DataSources.Add(newdatasource3);
                        this.reportViewer1.LocalReport.Refresh();
                        this.reportViewer1.RefreshReport();
                        break;

                case "Supplier":
                        parameters[12] = new ReportParameter("filter", "S");
                        this.reportViewer1.LocalReport.SetParameters(parameters);
                        PurchaseDataSetTableAdapters.DataTable3TableAdapter dap = new PurchaseDataSetTableAdapters.DataTable3TableAdapter();
                        PurchaseDataSet.DataTable3DataTable custtable = new PurchaseDataSet.DataTable3DataTable();
                        dap.FillBy(custtable, cbx_customer.SelectedValue.ToString());
                        newdatasource1 = new ReportDataSource("DataSet3", (DataTable)custtable);
                        newdatasource2 = new ReportDataSource("DataSet2", "");
                        newdatasource3 = new ReportDataSource("DataSet1", "");
                        this.reportViewer1.LocalReport.DataSources.Clear();
                        this.reportViewer1.LocalReport.DataSources.Add(newdatasource1);
                        this.reportViewer1.LocalReport.DataSources.Add(newdatasource2);
                        this.reportViewer1.LocalReport.DataSources.Add(newdatasource3);
                        this.reportViewer1.LocalReport.Refresh();
                        this.reportViewer1.RefreshReport();
                        break;
                case "Date":
                        parameters[12] = new ReportParameter("filter", "D");
                        this.reportViewer1.LocalReport.SetParameters(parameters);
                        PurchaseDataSetTableAdapters.DataTable2TableAdapter d = new PurchaseDataSetTableAdapters.DataTable2TableAdapter();
                        PurchaseDataSet.DataTable2DataTable dttable = new PurchaseDataSet.DataTable2DataTable();
                        d.FillBy(dttable,Convert.ToDateTime(Date_Start.Text),Convert.ToDateTime(Date_end.Text));
                        newdatasource1 = new ReportDataSource("DataSet2", (DataTable)dttable);
                        newdatasource2 = new ReportDataSource("DataSet1", "");
                        newdatasource3 = new ReportDataSource("DataSet3", "");
                        this.reportViewer1.LocalReport.DataSources.Clear();
                        this.reportViewer1.LocalReport.DataSources.Add(newdatasource1);
                        this.reportViewer1.LocalReport.DataSources.Add(newdatasource2);
                        this.reportViewer1.LocalReport.DataSources.Add(newdatasource3);
                        this.reportViewer1.LocalReport.Refresh();
                        this.reportViewer1.RefreshReport();
                    break;
            }

            
            //this.DataTable1TableAdapter.Fill(this.PurchaseDataSet.DataTable1);
            //this.reportViewer1.LocalReport.SetParameters(parameters);

            //this.reportViewer1.RefreshReport();
        }

        private void drpselect_SelectedIndexChanged(object sender, EventArgs e)
        {
             string dropdown = drpselect.SelectedItem.ToString();

             switch (dropdown)
             {
                 case "Product":
                     panelproduct.Visible = true;
                     panelCustomer.Visible = false;
                     paneldate.Visible = false;
                     chkItemname.Enabled = false;
                     break;

                case "Supplier":
                       panelproduct.Visible = false;
                     panelCustomer.Visible = true;
                     paneldate.Visible = false;
                     chkItemname.Enabled = true;
                     break;
                 case "Date":
                      panelproduct.Visible = false;
                     panelCustomer.Visible = false;
                     paneldate.Visible = true;
                     chkItemname.Enabled = true;
                     break;
             }

        }

        private void panelproduct_VisibleChanged(object sender, EventArgs e)
        {
            if (panelproduct.Visible == true)
            {
                try
                {
                    cmd.CommandText = "SELECT     CODE, DESC_ENG FROM  INV_ITEM_DIRECTORY ";
                    cmd.Connection = conn;
                    DataTable dt = new DataTable();
                    adapter.SelectCommand = cmd;
                    adapter.Fill(dt);
                    cbx_product.DataSource = dt;
                    cbx_product.DisplayMember = "DESC_ENG";
                    cbx_product.ValueMember = "DESC_ENG";

                }
                catch
                {
                }
            }
        }

        private void panelCustomer_VisibleChanged(object sender, EventArgs e)
        {
            if (panelCustomer.Visible == true)
            {
                try
                {
                    cmd.CommandText = "SELECT    DESC_ENG FROM         PAY_SUPPLIER";
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

        public void getaddress()
        {
            DataTable dt = new DataTable();
            dt = cs.getcompanyname();
                if(dt.Rows.Count>0)
                {
                    CompanyName = dt.Rows[0][0].ToString();
                }
        }
    }
}
