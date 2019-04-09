using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.Reporting.WinForms;
using System.IO;
using System.Diagnostics;
using Sys_Sols_Inventory.Model;

namespace Sys_Sols_Inventory.reports
{
    public partial class SalesProfitReport : Form
    {
       // private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
     //   private SqlCommand cmd = new SqlCommand();
        private SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable dt = new DataTable();
        Class.Stock_Report stkrpt = new Class.Stock_Report();
        private BindingSource source = new BindingSource();
        Initial mdi = (Initial)Application.OpenForms["Initial"];
        Login lg = (Login)Application.OpenForms["Login"];
        clsSalesProfitReport spr = new clsSalesProfitReport();
        public string decpoint = "2";
        public SalesProfitReport()
        {
            InitializeComponent();
        }
        public void BindSettings()
        {
            try
            {
                DataTable dt = spr.getSysSetup();
                //conn.Open();
                //cmd.Connection = conn;
                //cmd.CommandType = CommandType.Text;

                //cmd.CommandText = "SELECT * FROM SYS_SETUP";
                //adapter.SelectCommand = cmd;
                //adapter.Fill(dt);


                if (dt.Rows.Count > 0)
                {
                    decpoint = Convert.ToString(dt.Rows[0]["Dec_qty"]);
                  //  conn.Close();
                }
            }
            catch (Exception ee)
            {
                //conn.Close();
                MessageBox.Show(ee.Message);
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtCode.Text == "")
            {
                if (Chk.Checked == true)
                {
                    this.StoredProcedure1TableAdapter.Connection = DbFunctions.GetConnection();
                    DateTime startdate, enddate;
                    startdate = Convert.ToDateTime(DATE_FROM.Value.ToShortDateString());
                    enddate = Convert.ToDateTime(DATE_TO.Value.ToShortDateString());
                    this.StoredProcedure1TableAdapter.FillByDate(this.SalesProfitDS.StoredProcedure1,startdate,enddate);
                    this.reportViewer1.RefreshReport();
             
                    
                }
                else 
                {
                    this.StoredProcedure1TableAdapter.Connection = DbFunctions.GetConnection();
                    this.StoredProcedure1TableAdapter.FillAll(this.SalesProfitDS.StoredProcedure1);
                    this.reportViewer1.RefreshReport();
                   
                }


            }
            else
            {
                if (Chk.Checked == true)
                {
                    this.StoredProcedure1TableAdapter.Connection = DbFunctions.GetConnection();
                    DateTime startdate, enddate;
                    startdate = Convert.ToDateTime(DATE_FROM.Value.ToShortDateString());
                    enddate = Convert.ToDateTime(DATE_TO.Value.ToShortDateString());
                    this.StoredProcedure1TableAdapter.FillByDateCode(this.SalesProfitDS.StoredProcedure1, startdate, enddate,txtCode.Text);
                    this.reportViewer1.RefreshReport();
                }
                else
                {
                    this.StoredProcedure1TableAdapter.Connection = DbFunctions.GetConnection();
                    this.StoredProcedure1TableAdapter.Fill(this.SalesProfitDS.StoredProcedure1, txtCode.Text);
                    this.reportViewer1.RefreshReport();
                  
                }
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

        public void bindgridview()
        {
            try
            {
                //cmd.Connection = conn;
                //cmd.CommandText = "SELECT        INV_ITEM_DIRECTORY.DESC_ENG AS [Item Name],CODE FROM   INV_ITEM_DIRECTORY ";
                //cmd.CommandType = CommandType.Text;
                //DataTable dt = new DataTable();
                //adapter.SelectCommand = cmd;
                //adapter.Fill(dt);
                source.DataSource = spr.getItemDetails();

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
       
        private void SalesProfitReport_Load(object sender, EventArgs e)
        {
            BindSettings();
            dataGridItem.Visible = false;
            panel_itemname.Visible = false;
            bindgridview();
            DRP_VARIATIONON.SelectedIndex = 0;
            BindSettings();
           
        }

        private void DESC_ENG_TextChanged(object sender, EventArgs e)
        {
            if (DESC_ENG.Text != "")
            {
                panel_itemname.Visible = true;


                dataGridItem.Visible = true;
                source.Filter = string.Format("[Item Name] LIKE '%{0}%' ", DESC_ENG.Text);
                dataGridItem.ClearSelection();
            }
            else
            {
                dataGridItem.Visible = false;
                panel_itemname.Visible = false;
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
                    panel_itemname.Visible = false;
                    dataGridItem.Visible = false;
                }
            }
            catch
            { }
        }

        private void DESC_ENG_KeyDown(object sender, KeyEventArgs e)
        {
            panel_itemname.Visible = true;
            dataGridItem.Visible = true;
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
                panel_itemname.Visible = false;
                DESC_ENG.Text = "";
            }
        }

        private void Chk_CheckedChanged(object sender, EventArgs e)
        {
            if (Chk.Checked == true)
            {
                paneldate.Enabled = true;
            }
            else
                paneldate.Enabled = false;
        }

        private void txtCode_TextChanged(object sender, EventArgs e)
        {
            DESC_ENG.Text = "";
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            SavePDF(reportViewer1, Path.GetTempPath() + "SalesProfit_" + DateTime.Now.ToString("yyyyMMddHHmmssfff")+".xlsx");
        //    SavePDF(reportViewer1, Path.GetTempPath() + "salesvariation" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".docx");
         //  SavePDF(reportViewer1, Path.GetTempPath() + "salesvariation" + DateTime.Now.ToString("yyyyMMddHHmmssfff")+".pdf");
        }

        private void Find_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            //if (conn.State == ConnectionState.Open)
            //{
            //}
            //else
            //{

            //    conn.Open();
            //}



            if (Chk.Checked)
            {
                spr.FromDate = DATE_FROM.Value;
                spr.FromDate = DATE_TO.Value;
                if (DRP_VARIATIONON.Text == "Purchase Price")
                {
                    //cmd.CommandText = "VAR_PURCHASE_COST";
                    //cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.Clear();
                    //cmd.Parameters.AddWithValue("@Op", "s2");

                    //cmd.Parameters.AddWithValue("@Date1", DATE_FROM.Value);
                    //cmd.Parameters.AddWithValue("@Date2", DATE_TO.Value);
                    //adapter.Fill(dt);
                    //cmd.Parameters.Clear();
                   
                    source.DataSource = spr.getPurchasePrice() ; DG_REPORT.DataSource = source;
                }
                else if (DRP_VARIATIONON.Text == "Last Purchase Cost")
                {
                    //cmd.CommandText = "VAR_LASTPURCHASE_COST";
                    //cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.Clear();
                    //cmd.Parameters.AddWithValue("@Op", "s2");
                    //cmd.Parameters.AddWithValue("@Date1", DATE_FROM.Value);
                    //cmd.Parameters.AddWithValue("@Date2", DATE_TO.Value);
                    //adapter.Fill(dt);
                    //cmd.Parameters.Clear();
                    source.DataSource = spr.getLastPurchasePrice(); DG_REPORT.DataSource = source;
                }
                else if (DRP_VARIATIONON.Text == "Last Purchase Rate")
                {
                    spr.FromDate = DATE_FROM.Value;
                    spr.FromDate = DATE_TO.Value;
                    //cmd.CommandText = "VAR_LASTPURCHASE_RATE";
                    //cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.Clear();
                    //cmd.Parameters.AddWithValue("@Op", "s2");
                    //cmd.Parameters.AddWithValue("@Date1", DATE_FROM.Value);
                    //cmd.Parameters.AddWithValue("@Date2", DATE_TO.Value);
                    //adapter.Fill(dt);
                    //cmd.Parameters.Clear();
                    source.DataSource = spr.getLastPurchaseRate(); DG_REPORT.DataSource = source;
                }
                else if (DRP_VARIATIONON.Text == "Last Sales Rate")
                {
                    //cmd.CommandText = "VAR_LASTSALES_RATE";
                    //cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.Clear();
                    //cmd.Parameters.AddWithValue("@Op", "s2");
                    //cmd.Parameters.AddWithValue("@Date1", DATE_FROM.Value);
                    //cmd.Parameters.AddWithValue("@Date2", DATE_TO.Value);
                    //adapter.Fill(dt);
                    //cmd.Parameters.Clear();
                    source.DataSource = spr.getLastSalesRate(); DG_REPORT.DataSource = source;
                }
                else if (DRP_VARIATIONON.Text == "Last Sales Cost")
                {
                    //cmd.CommandText = "VAR_LASTSALES_COST";
                    //cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.Clear();
                    //cmd.Parameters.AddWithValue("@Op", "s2");
                    //cmd.Parameters.AddWithValue("@Date1", DATE_FROM.Value);
                    //cmd.Parameters.AddWithValue("@Date2", DATE_TO.Value);
                    //adapter.Fill(dt);
                    //cmd.Parameters.Clear();
                    source.DataSource = spr.getLastSalesCost();
                    DG_REPORT.DataSource = source;
                }

            }
            else
            {
                if (DRP_VARIATIONON.Text == "Purchase Price")
                {
                    //cmd.CommandText = "VAR_PURCHASE_COST";
                    //cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.Clear();
                    //cmd.Parameters.AddWithValue("@Op", "s1");
                    //adapter.Fill(dt);
                    //cmd.Parameters.Clear();
                    source.DataSource = spr.getPurchasePriceWthoutDate(); 
                    DG_REPORT.DataSource = source;
                }
                else if (DRP_VARIATIONON.Text == "Last Purchase Cost")
                {
                    //cmd.CommandText = "VAR_LASTPURCHASE_COST";
                    //cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.Clear();
                    //cmd.Parameters.AddWithValue("@Op", "s1");
                    //adapter.Fill(dt);
                    //cmd.Parameters.Clear();
                    source.DataSource =spr.getLastPurchaseCostWthoutDate(); 
                    DG_REPORT.DataSource = source;
                }

                else if (DRP_VARIATIONON.Text == "Last Purchase Rate")
                {
                    //cmd.CommandText = "VAR_LASTPURCHASE_RATE";
                    //cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.Clear();
                    //cmd.Parameters.AddWithValue("@Op", "s1");
                    //adapter.Fill(dt);
                    //cmd.Parameters.Clear();
                    source.DataSource = spr.getLastPurchaseRateWthoutDate(); DG_REPORT.DataSource = source;
                }
                else if (DRP_VARIATIONON.Text == "Last Sales Rate")
                {
                    //cmd.CommandText = "VAR_LASTSALES_RATE";
                    //cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.AddWithValue("@Op", "s1");
                    //adapter.Fill(dt);
                    //cmd.Parameters.Clear();
                    source.DataSource = spr.getLastSalesRateWthoutDate(); DG_REPORT.DataSource = source;
                }
                else if (DRP_VARIATIONON.Text == "Last Sales Cost")
                {
                    //cmd.CommandText = "VAR_LASTSALES_COST";
                    //cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.Clear();
                    //cmd.Parameters.AddWithValue("@Op", "s1");
                    //adapter.Fill(dt);
                    //cmd.Parameters.Clear();
                    source.DataSource = spr.getLastSalesCostWthoutDate(); DG_REPORT.DataSource = source;
                }
                DG_REPORT.Columns["UNIT PRICE"].DefaultCellStyle.Format = "N" + decpoint;
                DG_REPORT.Columns["ITEM_TAX"].DefaultCellStyle.Format = "N" + decpoint;
                DG_REPORT.Columns["TOTAL"].DefaultCellStyle.Format = "N" + decpoint;
                DG_REPORT.Columns["Unit Purchase Price"].DefaultCellStyle.Format = "N" + decpoint;
                DG_REPORT.Columns["Total Purchase Price"].DefaultCellStyle.Format = "N" + decpoint;
                DG_REPORT.Columns["profit"].DefaultCellStyle.Format = "N" + decpoint;
                DG_REPORT.Columns["UNIT PRICE"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
                DG_REPORT.Columns["ITEM_TAX"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
                DG_REPORT.Columns["TOTAL"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
                DG_REPORT.Columns["Unit Purchase Price"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
                DG_REPORT.Columns["Total Purchase Price"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
                DG_REPORT.Columns["profit"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;

            }

          //   conn.Close();
             
           
            
          
            try
            {
                try
                {
                    DG_REPORT.FirstDisplayedScrollingRowIndex = DG_REPORT.RowCount - 1;
                    DG_REPORT.Columns["Item Code"].Width = 60;
                    DG_REPORT.Columns["Qty"].Width = 30;
                }
                catch 
                {
                    
                  
                }
              
             //   DG_REPORT.Columns["Item Name"].Width = 200;
                for (int i = 0; i < DG_REPORT.Rows.Count; i++)
                {
                    if(Convert.ToDecimal(DG_REPORT.Rows[i].Cells["Profit"].Value)<0)
                    {
                        DG_REPORT.Rows[i].DefaultCellStyle.BackColor=Color.Tomato;
                    }
                }

                if (DESC_ENG.Text != "")
                {
                    source.Filter = string.Format("[Item Name] LIKE '%{0}%' ", DESC_ENG.Text);
                }
            }
            catch
            {
            }
        }

        private void DG_REPORT_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DG_REPORT.Rows.Count > 0)
            {
                try
                {
                    ComponentFactory.Krypton.Navigator.KryptonPage kp = new ComponentFactory.Krypton.Navigator.KryptonPage();
                    mdi.maindocpanel.Pages.Add(kp);
                    string docno = DG_REPORT.CurrentRow.Cells["Invoice no"].Value.ToString();
                    SalesQ m = new SalesQ(docno);
                    m.Show();
                    m.BackColor = Color.White;
                    m.TopLevel = false;
                    kp.Controls.Add(m);
                    m.Dock = DockStyle.Fill;
                    kp.Text = m.Text;
                    kp.Name = "Sale";
                    m.FormBorderStyle = FormBorderStyle.None;
                    //kp.Focus();
                    mdi.maindocpanel.SelectedPage = kp;
                    // m.Focus();

                }
                catch
                {
                }
            }
        }

        private void DESC_ENG_Leave(object sender, EventArgs e)
        {
           
        }

        private void combo_item_name_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
        }

    }

