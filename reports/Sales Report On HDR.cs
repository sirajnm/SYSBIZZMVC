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
using System.Data.SqlClient;
using Sys_Sols_Inventory.Model;

namespace Sys_Sols_Inventory.reports
{
    public partial class Sales_Report_On_HDR : Form
    {
        Class.Print_Direct pd = new Class.Print_Direct();
        //private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        //private SqlCommand cmd = new SqlCommand();
        //private SqlDataAdapter adapter = new SqlDataAdapter();
        LocalReport report = new LocalReport();
        decimal TotalDiscount = 0;
        decimal bill = 0;
        Class.Stock_Report stkrpt = new Class.Stock_Report();

        Model.clsCommon clsCommon = new Model.clsCommon();

        Initial mdi = (Initial)Application.OpenForms["Initial"];
        DataTable dt = new DataTable();
        private bool HasType = true;
        private bool HasGroup = true;
        private bool HasCategory = true;
        private bool HasTM = true;
        public string[] month = { "", "JAN", "FEB", "MAR", "APR", "MAY", "JUN", "JULY", "AUG", "SEP", "OCT", "NOV","DEC" };
       
        public Sales_Report_On_HDR()
        {
            InitializeComponent();            
            HasType = General.IsEnabled(Settings.HasType);
            HasCategory = General.IsEnabled(Settings.HasCategory);
            HasGroup = General.IsEnabled(Settings.HasGroup);
            HasTM = General.IsEnabled(Settings.HasTM);
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
        
        public void BindType()
        {
            dt = stkrpt.BindType();
            TYPE.ValueMember = "CODE";
            TYPE.DisplayMember = "DESC_ENG";
            DataRow row = dt.NewRow();
            row[0] = "";
            dt.Rows.InsertAt(row, 0);
            TYPE.DataSource = dt;

            cmb_saletype.DataSource = Common.getSaleTypes_rpt();
            cmb_saletype.DisplayMember = "value";
            cmb_saletype.ValueMember = "key";
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
        public void BindCustomer()
        {
            dt = stkrpt.BindCustomer();
            Cbx_supplier.ValueMember = "CODE";
            Cbx_supplier.DisplayMember = "DESC_ENG";
            DataRow row = dt.NewRow();
            row[0] = "";
            dt.Rows.InsertAt(row, 0);
            Cbx_supplier.DataSource = dt;
        }

        public void BindpurchaseType()
        {
            
            dt = stkrpt.BindSTypes();
            Cbx_salemode.ValueMember = "CODE";
            Cbx_salemode.DisplayMember = "DESC_ENG";
            DataRow row = dt.NewRow();
            row[0] = "";
            dt.Rows.InsertAt(row, 0);
            Cbx_salemode.DataSource = dt;
        }

        public void SalesSummary()
        {
            string query;
            DataTable dt=new DataTable();
            if (!Chk.Checked)
            {
                query = "SELECT  INV_SALES_HDR.SALE_TYPE as [Sale Type],INV_SALES_HDR.DOC_ID AS [Inv.No],INV_SALES_HDR.DOC_NO AS [Voucher No],CASE WHEN INV_SALES_HDR.DOC_TYPE='SAL.CSS' THEN 'CASH SALES'  WHEN INV_SALES_HDR.DOC_TYPE='SAL.CRD' THEN 'CREDIT SALES'  WHEN INV_SALES_HDR.DOC_TYPE='SAL.CSR' THEN 'CASH SALE RETURN' WHEN INV_SALES_HDR.DOC_TYPE='SAL.CDR' THEN 'CREDIT SALE RETURN' ELSE '' END as Type, INV_SALES_HDR.DOC_DATE_GRE as Date, REC_CUSTOMER.DESC_ENG as Supplier, ISNULL(INV_SALES_HDR.TAX_TOTAL,0) as 'Tax Total', ISNULL(INV_SALES_HDR.FREIGHT,0) as 'Freight', ISNULL(INV_SALES_HDR.TOTAL_AMOUNT,0) as 'Gross Amount', ISNULL(INV_SALES_HDR.DISCOUNT,0) as Discount,ISNULL(INV_SALES_HDR.NET_AMOUNT,0) as 'Net Value' FROM            INV_SALES_HDR INNER JOIN  REC_CUSTOMER ON INV_SALES_HDR.CUSTOMER_CODE = REC_CUSTOMER.CODE WHERE   (INV_SALES_HDR.SALESMAN_CODE LIKE '%" + cmb_salesman.SelectedValue + "%') AND  (INV_SALES_HDR.SALE_TYPE LIKE '%" + cmb_saletype.SelectedValue + "%') AND     (INV_SALES_HDR.DOC_TYPE LIKE '%" + Cbx_salemode.SelectedValue + "%') AND (INV_SALES_HDR.CUSTOMER_CODE LIKE '%" + Cbx_supplier.SelectedValue + "%') AND (INV_SALES_HDR.FLAGDEL=1) and (INV_SALES_HDR.DOC_TYPE IN('SAL.CSS','SAL.CRD','SAL.CSR','SAL.CSR','SAL.CDR')) ORDER BY DOC_ID, INV_SALES_HDR.DOC_DATE_GRE";
                dt = DbFunctions.GetDataTable(query);
            }
            else
            {
                query = "SELECT  INV_SALES_HDR.SALE_TYPE as [Sale Type],INV_SALES_HDR.DOC_ID AS [Inv.No],INV_SALES_HDR.DOC_NO AS [Voucher No],CASE WHEN INV_SALES_HDR.DOC_TYPE='SAL.CSS' THEN 'CASH SALES'  WHEN INV_SALES_HDR.DOC_TYPE='SAL.CRD' THEN 'CREDIT SALES'  WHEN INV_SALES_HDR.DOC_TYPE='SAL.CSR' THEN 'CASH SALE RETURN' WHEN INV_SALES_HDR.DOC_TYPE='SAL.CDR' THEN 'CREDIT SALE RETURN' ELSE '' END as Type, INV_SALES_HDR.DOC_DATE_GRE as Date, REC_CUSTOMER.DESC_ENG as Supplier, ISNULL(INV_SALES_HDR.TAX_TOTAL,0) as 'Tax Total', ISNULL(INV_SALES_HDR.FREIGHT,0) as 'Freight', ISNULL(INV_SALES_HDR.TOTAL_AMOUNT,0) as 'Gross Amount', ISNULL(INV_SALES_HDR.DISCOUNT,0) as Discount,ISNULL(INV_SALES_HDR.NET_AMOUNT,0) as 'Net Value' FROM            INV_SALES_HDR INNER JOIN  REC_CUSTOMER ON INV_SALES_HDR.CUSTOMER_CODE = REC_CUSTOMER.CODE WHERE   (INV_SALES_HDR.SALESMAN_CODE LIKE '%" + cmb_salesman.SelectedValue + "%') AND  (INV_SALES_HDR.SALE_TYPE LIKE '%" + cmb_saletype.SelectedValue + "%') AND     (INV_SALES_HDR.DOC_TYPE LIKE '%" + Cbx_salemode.SelectedValue + "%') AND (INV_SALES_HDR.CUSTOMER_CODE LIKE '%" + Cbx_supplier.SelectedValue + "%') AND (INV_SALES_HDR.FLAGDEL=1) and (INV_SALES_HDR.DOC_TYPE IN('SAL.CSS','SAL.CRD','SAL.CSR','SAL.CSR','SAL.CDR')) AND convert(varchar, INV_SALES_HDR.DOC_DATE_GRE, 101) BETWEEN @d1 AND @d2 ORDER BY DOC_ID, INV_SALES_HDR.DOC_DATE_GRE";
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@d1", StartDate.Value.Date);
                param.Add("@d2", EndDate.Value.Date);
                dt = DbFunctions.GetDataTable(query, param);
            }
                
            var grosstotal = ((from s in dt.AsEnumerable()
                         select decimal.Parse(s["Gross Amount"].ToString())) as IEnumerable<decimal>).Sum();
            var netamount = ((from s in dt.AsEnumerable()
                             select decimal.Parse(s["Net Value"].ToString())) as IEnumerable<decimal>).Sum();
            var taxamount = ((from s in dt.AsEnumerable()
                          select decimal.Parse(s["Tax Total"].ToString())) as IEnumerable<decimal>).Sum();
            var discount = ((from s in dt.AsEnumerable()
                             select decimal.Parse(s["Discount"].ToString())) as IEnumerable<decimal>).Sum();
            var freight = ((from s in dt.AsEnumerable()
                          select decimal.Parse(s["Freight"].ToString())) as IEnumerable<decimal>).Sum();

            dt.Rows.Add(null,null, null, null, null, "Total", taxamount, freight, grosstotal,discount,netamount);
            dataGridView1.DataSource = dt;



        }

        private void Sales_Report_On_HDR_Load(object sender, EventArgs e)
        {  
            Bind_item_id();
            Bind_item_name();
            BindCategory();
            BindTradeMark();
            BindGroup();
            BindpurchaseType();
            BindType();
            BindCustomer();
            cmb_report.SelectedIndex = 0;
            get_salesman();
            btnSave.PerformClick();
            dataGridView1.Focus();
            dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[0];

            this.reportViewer1.RefreshReport();
        }
        private void get_salesman()
        {
            DataTable dt1 = clsCommon.GetSalesMan(); ;
            
            //cmd1.CommandText = "SELECT EMPID,CONCAT(EMP_FNAME,' ',EMP_MNAME,' ',EMP_LNAME)as name from EMP_EMPLOYEES WHERE EMP_DESIG=21";
            //cmd1.Connection = conn;
            //adapter.SelectCommand = cmd1;           
            
            cmb_salesman.ValueMember = "EMPID";
            cmb_salesman.DisplayMember = "name";
            DataRow row = dt1.NewRow();
            dt1.Rows.InsertAt(row, 0);
            cmb_salesman.DataSource = dt1;

        }
        public void Bind_item_id()
        {
            dt = stkrpt.Bind_item_id();
            combo_item_id.ValueMember = "CODE";
            combo_item_id.DisplayMember = "CODE";
            DataRow row = dt.NewRow();
            row[0] = "";
            dt.Rows.InsertAt(row, 0);
            combo_item_id.DataSource = dt;

           DataTable dt1 = stkrpt.Bind_item_name();
            cmb.ValueMember = "CODE";
            cmb.DisplayMember = "DESC_ENG";
            DataRow row1 = dt1.NewRow();
            row1[0] = "";
            dt1.Rows.InsertAt(row1, 0);
            cmb.DataSource = dt1;

        }
        public void Bind_item_name()
        {
            dt = stkrpt.Bind_item_name();
            combo_item_name.ValueMember = "DESC_ENG";
            combo_item_name.DisplayMember = "DESC_ENG";
            DataRow row = dt.NewRow();
            row[0] = "";
            dt.Rows.InsertAt(row, 0);
            combo_item_name.DataSource = dt;
        }

        private void Send_Mail_Click(object sender, EventArgs e)
        {
            SavePDF(reportViewer1, Path.GetTempPath() + "SalesReport_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xlsx");
            //    SavePDF(reportViewer1, Path.GetTempPath() + "salesvariation" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".docx");
        
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
        //public int[] indx;
        List<int> indx = new List<int>();
        private void btnSave_Click(object sender, EventArgs e)
        {            
             DataTable1TableAdapter.Connection = DbFunctions.GetConnection();
            if (cmb_report.Text == "Bill Wise Summary")
            {
                tabControl1.SelectedIndex = 1;
            }
            else
            {
                tabControl1.SelectedIndex = 0;
            }
            if (tabControl1.SelectedIndex == 0)
            {
                
                if(dataGridView1.Columns.Count>0)
                {
                    dataGridView1.Columns.Clear();
                }

                if (cmb_report.Text == "")
                {
                    SalesSummary();
                    
                }

                if (cmb_report.Text== "Item Summary")
                {
                    string item_name = combo_item_name.SelectedValue.ToString();
                    string itemtype = TYPE.SelectedValue.ToString();
                    string category = DrpCategory.SelectedValue.ToString();
                    string group = Group.SelectedValue.ToString();
                    string trade_mark = Trademark.SelectedValue.ToString();
                    //string item_code = combo_item_id.SelectedValue.ToString();
                    DataTable dt=new DataTable();
                    //conn.Open();
                    if (Chk.Checked)
                    {
                        clsCommon.Item_name = item_name;
                        clsCommon.Category = category;
                        clsCommon.Group = group;
                        clsCommon.Trade_mark = trade_mark;
                        clsCommon.StartDate = StartDate.Value.Date;
                        clsCommon.EndDate = EndDate.Value.Date;
                        dt = clsCommon.salesReport_date();
                        //cmd = new SqlCommand("SELECT SALES.ITEM_CODE,INV_ITEM_DIRECTORY.DESC_ENG AS ITEM_NAME,SALES.QTY,SALES.TOTAL FROM INV_ITEM_DIRECTORY LEFT OUTER JOIN (SELECT ITEM_CODE,SUM(QUANTITY) AS QTY,SUM(GROSS_TOTAL) AS TOTAL FROM INV_SALES_DTL LEFT OUTER JOIN INV_SALES_HDR ON INV_SALES_DTL.DOC_ID=INV_SALES_HDR.DOC_ID WHERE INV_SALES_HDR.DOC_DATE_GRE BETWEEN @d1 AND @d2  GROUP BY ITEM_CODE) AS SALES ON INV_ITEM_DIRECTORY.CODE=SALES.ITEM_CODE LEFT OUTER JOIN INV_ITEM_CATEGORY ON INV_ITEM_DIRECTORY.CATEGORY=INV_ITEM_CATEGORY.CODE LEFT OUTER JOIN INV_ITEM_GROUP ON INV_ITEM_DIRECTORY.[GROUP]=INV_ITEM_GROUP.CODE LEFT OUTER JOIN INV_ITEM_TM ON INV_ITEM_DIRECTORY.TRADEMARK=INV_ITEM_TM.CODE WHERE ITEM_CODE IS NOT NULL AND INV_ITEM_DIRECTORY.DESC_ENG LIKE '%" + item_name + "%' AND INV_ITEM_DIRECTORY.CATEGORY LIKE '%" + category + "%' AND INV_ITEM_DIRECTORY.TRADEMARK LIKE '%" + trade_mark + "%' AND INV_ITEM_DIRECTORY.[GROUP] LIKE '%" + group + "%'", conn);
                        //cmd.Parameters.Add("@d1", SqlDbType.Date).Value = StartDate.Value;
                        //cmd.Parameters.Add("@d2", SqlDbType.Date).Value = EndDate.Value;
                    }
                    else
                    {
                        //cmd = new SqlCommand("SELECT SALES.ITEM_CODE,INV_ITEM_DIRECTORY.DESC_ENG AS ITEM_NAME,SALES.QTY,SALES.TOTAL FROM INV_ITEM_DIRECTORY LEFT OUTER JOIN (SELECT ITEM_CODE,SUM(QUANTITY) AS QTY,SUM(GROSS_TOTAL) AS TOTAL FROM INV_SALES_DTL GROUP BY ITEM_CODE) AS SALES ON INV_ITEM_DIRECTORY.CODE=SALES.ITEM_CODE LEFT OUTER JOIN INV_ITEM_CATEGORY ON INV_ITEM_DIRECTORY.CATEGORY=INV_ITEM_CATEGORY.CODE LEFT OUTER JOIN INV_ITEM_GROUP ON INV_ITEM_DIRECTORY.[GROUP]=INV_ITEM_GROUP.CODE LEFT OUTER JOIN INV_ITEM_TM ON INV_ITEM_DIRECTORY.TRADEMARK=INV_ITEM_TM.CODE WHERE ITEM_CODE IS NOT NULL AND INV_ITEM_DIRECTORY.DESC_ENG LIKE '%" + item_name + "%' AND INV_ITEM_DIRECTORY.CATEGORY LIKE '%" + category + "%' AND INV_ITEM_DIRECTORY.TRADEMARK LIKE '%" + trade_mark + "%' AND INV_ITEM_DIRECTORY.[GROUP] LIKE '%" + group + "%'", conn);
                        clsCommon.Item_name = item_name;
                        clsCommon.Category = category;
                        clsCommon.Group = group;
                        clsCommon.Trade_mark = trade_mark;
                        dt = clsCommon.salesReport_all();
                    }

                    if (dt.Rows.Count > 0)
                    {
                        dataGridView1.DataSource = dt;
                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.Font = new Font(dataGridView1.DefaultCellStyle.Font, FontStyle.Bold);
                    }
                    
                        dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[0];
                        Font font = new Font(dataGridView1.DefaultCellStyle.Font.FontFamily, 9, FontStyle.Bold);
                        dataGridView1.Rows[dataGridView1.RowCount - 1].DefaultCellStyle.Font = font;
                        dataGridView1.Rows[dataGridView1.RowCount - 1].DefaultCellStyle.ForeColor = Color.Red;
                        dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                        dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                        dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

                        dataGridView1.Columns["GROSS TOTAL"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dataGridView1.Columns["TAX AMOUNT"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dataGridView1.Columns["NET TOTAL"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dataGridView1.Columns["QUANTITY"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        
                        dataGridView1.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    


                }
                if (cmb_report.Text == "Salesman Summary")
                {
                    if (cmb_salesman.SelectedIndex<1)
                    {
                        MessageBox.Show("Please select salesman");
                        return;
                    }
                    DataTable dt = new DataTable();
                    
                    if (Chk.Checked)
                    {
                        //cmd = new SqlCommand("SELECT DOC_ID AS 'INVOICE NO',REC_CUSTOMER.DESC_ENG as 'CUSTOMER NAME',CAST(INV_SALES_HDR.DOC_DATE_GRE AS date) AS [DATE],convert(decimal(18,2),NET_AMOUNT) as NET_AMOUNT FROM INV_SALES_HDR LEFT OUTER JOIN REC_CUSTOMER ON REC_CUSTOMER.CODE=INV_SALES_HDR.CUSTOMER_CODE WHERE REC_CUSTOMER.SALESMAN_CODE=" + cmb_salesman.SelectedValue + " AND INV_SALES_HDR.DOC_TYPE='SAL.CRD' AND INV_SALES_HDR.DOC_DATE_GRE BETWEEN @d1 AND @d2 AND INV_SALES_HDR.CUSTOMER_CODE LIKE '%" + Cbx_supplier.SelectedValue + "%' ORDER BY DOC_ID ASC", conn);
                        //cmd.Parameters.Add("@d1", SqlDbType.Date).Value = StartDate.Value;
                        //cmd.Parameters.Add("@d2", SqlDbType.Date).Value = EndDate.Value;

                        clsCommon.Salemancode = cmb_salesman.SelectedValue.ToString();
                        clsCommon.Custcode = Cbx_supplier.SelectedValue.ToString();
                        
                        clsCommon.StartDate = StartDate.Value;
                        clsCommon.EndDate = EndDate.Value;
                        dt = clsCommon.salesReport_CustSalesman_Date();

                    }
                    else
                    {
                        //cmd = new SqlCommand("SELECT DOC_ID AS 'INVOICE NO',REC_CUSTOMER.DESC_ENG as 'CUSTOMER NAME',CAST(INV_SALES_HDR.DOC_DATE_GRE AS date) AS [DATE],convert(decimal(18,2),NET_AMOUNT) as NET_AMOUNT FROM INV_SALES_HDR LEFT OUTER JOIN REC_CUSTOMER ON REC_CUSTOMER.CODE=INV_SALES_HDR.CUSTOMER_CODE WHERE REC_CUSTOMER.SALESMAN_CODE=" + cmb_salesman.SelectedValue + " AND INV_SALES_HDR.DOC_TYPE='SAL.CRD' AND INV_SALES_HDR.CUSTOMER_CODE LIKE '%" + Cbx_supplier.SelectedValue + "%' ORDER BY DOC_ID ASC", conn);
                        dt = clsCommon.salesReport_CustSalesman_Date();
                    }
                    //SqlDataAdapter adptr = new SqlDataAdapter(cmd);
                    //adptr.Fill(dt);
                    //conn.Close();
                    //cmd.Dispose();
                    //cmd.Parameters.Clear();
                    if (dt.Rows.Count > 0)
                    {
                        //SqlCommand cmd1 = new SqlCommand();
                        //SqlCommand total_cmd = new SqlCommand();
                        DataTable p = new DataTable();
                        DataRow row = dt.NewRow();
                        dt.Rows.InsertAt(row, dt.Rows.Count);
                        dataGridView1.DataSource = dt;
                        // dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.Fill;
                        dataGridView1.Columns["CUSTOMER NAME"].Width = 500;
                        decimal total = 0;
                        decimal receipt = 0;
                        string query="",query1;
                        Dictionary<string, object> parameter1=new Dictionary<string, object>(),parameter2 = new Dictionary<string, object>();
                        //conn.Open();
                        if (Chk.Checked)
                        {
                            if(Cbx_supplier.SelectedIndex>0)
                                query = "SELECT ISNULL(SUM(RECEIPT.AMOUNT),0) AS AMOUNT FROM REC_CUSTOMER LEFT OUTER JOIN (SELECT REC_CUSTOMER.CODE,SUM(ISNULL(tb_Transactions.CREDIT,0)) AS AMOUNT FROM REC_CUSTOMER LEFT OUTER JOIN tb_Transactions ON REC_CUSTOMER.LedgerId=tb_Transactions.ACCID WHERE tb_transactions.DATED BETWEEN @date1 AND @date2 GROUP BY REC_CUSTOMER.CODE) AS RECEIPT ON RECEIPT.CODE=REC_CUSTOMER.CODE WHERE REC_CUSTOMER.CODE=" + Cbx_supplier.SelectedValue.ToString();
                            else
                                query = "SELECT ISNULL(SUM(RECEIPT.AMOUNT),0) AS AMOUNT FROM REC_CUSTOMER LEFT OUTER JOIN (SELECT REC_CUSTOMER.CODE,SUM(ISNULL(tb_Transactions.CREDIT,0)) AS AMOUNT FROM REC_CUSTOMER LEFT OUTER JOIN tb_Transactions ON REC_CUSTOMER.LedgerId=tb_Transactions.ACCID WHERE tb_transactions.DATED BETWEEN @date1 AND @date2 GROUP BY REC_CUSTOMER.CODE) AS RECEIPT ON RECEIPT.CODE=REC_CUSTOMER.CODE WHERE REC_CUSTOMER.SALESMAN_CODE=" + cmb_salesman.SelectedValue.ToString();
                            
                            query1 = "SELECT convert(decimal(18,2),SUM(NET_AMOUNT)) as NET_AMOUNT FROM INV_SALES_HDR LEFT OUTER JOIN REC_CUSTOMER ON REC_CUSTOMER.CODE=INV_SALES_HDR.CUSTOMER_CODE WHERE REC_CUSTOMER.SALESMAN_CODE=" + cmb_salesman.SelectedValue + " AND INV_SALES_HDR.DOC_TYPE='SAL.CRD' AND INV_SALES_HDR.DOC_DATE_GRE BETWEEN @d1 AND @d2 AND INV_SALES_HDR.CUSTOMER_CODE LIKE '%" + Cbx_supplier.SelectedValue + "%'";
                            parameter2.Add("@d1",StartDate.Value);
                            parameter2.Add("@d2", EndDate.Value);

                            parameter1.Add("@date1", StartDate.Value.ToString("yyyy/MM/dd"));
                            parameter1.Add("@date2",  EndDate.Value.ToString("yyyy/MM/dd"));

                            total = Convert.ToDecimal(DbFunctions.GetAValue(query1,parameter2));
                            receipt = Convert.ToDecimal(DbFunctions.GetAValue(query,parameter1));
                        }
                        else
                        {
                            if(Cbx_supplier.SelectedIndex>0)
                                query = "SELECT ISNULL(SUM(RECEIPT.AMOUNT),0) AS AMOUNT FROM REC_CUSTOMER LEFT OUTER JOIN (SELECT REC_CUSTOMER.CODE,SUM(ISNULL(tb_Transactions.CREDIT,0)) AS AMOUNT FROM REC_CUSTOMER LEFT OUTER JOIN tb_Transactions ON REC_CUSTOMER.LedgerId=tb_Transactions.ACCID GROUP BY REC_CUSTOMER.CODE) AS RECEIPT ON RECEIPT.CODE=REC_CUSTOMER.CODE WHERE REC_CUSTOMER.CODE='" + Cbx_supplier.SelectedValue.ToString() + "'";
                            else
                                query = "SELECT ISNULL(SUM(RECEIPT.AMOUNT),0) AS AMOUNT FROM REC_CUSTOMER LEFT OUTER JOIN (SELECT REC_CUSTOMER.CODE,SUM(ISNULL(tb_Transactions.CREDIT,0)) AS AMOUNT FROM REC_CUSTOMER LEFT OUTER JOIN tb_Transactions ON REC_CUSTOMER.LedgerId=tb_Transactions.ACCID GROUP BY REC_CUSTOMER.CODE) AS RECEIPT ON RECEIPT.CODE=REC_CUSTOMER.CODE WHERE REC_CUSTOMER.SALESMAN_CODE='" + cmb_salesman.SelectedValue.ToString() + "'";
                            query1 = "SELECT convert(decimal(18,2),SUM(NET_AMOUNT)) as NET_AMOUNT FROM INV_SALES_HDR LEFT OUTER JOIN REC_CUSTOMER ON REC_CUSTOMER.CODE=INV_SALES_HDR.CUSTOMER_CODE WHERE REC_CUSTOMER.SALESMAN_CODE=" + cmb_salesman.SelectedValue + " AND INV_SALES_HDR.DOC_TYPE='SAL.CRD'  AND INV_SALES_HDR.CUSTOMER_CODE LIKE '%" + Cbx_supplier.SelectedValue + "%'";

                            total = Convert.ToDecimal(DbFunctions.GetAValue(query1));
                            receipt = Convert.ToDecimal(DbFunctions.GetAValue(query));
                        }
                        
                        
                        dataGridView1.Rows[dataGridView1.RowCount - 1].Cells["CUSTOMER NAME"].Value = "Total  [Total Receipt"+receipt.ToString()+"]";                      
                        
                         dataGridView1.Rows[dataGridView1.RowCount - 1].Cells["NET_AMOUNT"].Value = total.ToString();
                         dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.Font = new Font(dataGridView1.DefaultCellStyle.Font, FontStyle.Bold);
                         //conn.Close();
                         //cmd.Dispose();
                    }
                    else

                        MessageBox.Show("No data");
                   
                }
                if (cmb_report.Text == "Customer Summary")
                {
                    if (Cbx_supplier.SelectedIndex == 0)
                    {
                        MessageBox.Show("Please select customer");
                        return;
                    }
                    DataTable dt = new DataTable();
                    //conn.Open();
                    if (Chk.Checked)
                    {
                        string query = "SELECT INV_SALES_HDR.SALE_TYPE as [Sale Type],INV_SALES_HDR.DOC_ID AS [Inv.No],INV_SALES_HDR.DOC_NO AS [Voucher No],CASE WHEN INV_SALES_HDR.DOC_TYPE='SAL.CSS' THEN 'CASH SALES'  WHEN INV_SALES_HDR.DOC_TYPE='SAL.CRD' THEN 'CREDIT SALES'  WHEN INV_SALES_HDR.DOC_TYPE='SAL.CSR' THEN 'CASH SALE RETURN' WHEN INV_SALES_HDR.DOC_TYPE='SAL.CDR' THEN 'CREDIT SALE RETURN' ELSE '' END as Type, convert(varchar, INV_SALES_HDR.DOC_DATE_GRE, 101) as Date, ISNULL(INV_SALES_HDR.TAX_TOTAL,0) as 'Tax Total', ISNULL(INV_SALES_HDR.FREIGHT,0) as 'Freight', ISNULL(INV_SALES_HDR.TOTAL_AMOUNT,0) as 'Gross Amount', ISNULL(INV_SALES_HDR.DISCOUNT,0) as Discount,ISNULL(INV_SALES_HDR.NET_AMOUNT,0) as 'Net Value' FROM INV_SALES_HDR LEFT OUTER JOIN REC_CUSTOMER ON REC_CUSTOMER.CODE=INV_SALES_HDR.CUSTOMER_CODE WHERE REC_CUSTOMER.CODE=" + Cbx_supplier.SelectedValue + " AND  INV_SALES_HDR.DOC_TYPE='SAL.CRD' AND (convert(varchar, INV_SALES_HDR.DOC_DATE_GRE, 101) BETWEEN @d1 AND @d2) ORDER BY DOC_ID ASC"; 
                        //string query = "SELECT DOC_ID AS 'INVOICE NO',REC_CUSTOMER.DESC_ENG as 'CUSTOMER NAME',CAST(INV_SALES_HDR.DOC_DATE_GRE AS date) AS [DATE],NET_AMOUNT FROM INV_SALES_HDR LEFT OUTER JOIN REC_CUSTOMER ON REC_CUSTOMER.CODE=INV_SALES_HDR.CUSTOMER_CODE WHERE REC_CUSTOMER.CODE=" + Cbx_supplier.SelectedValue + " AND  INV_SALES_HDR.DOC_TYPE='SAL.CRD' AND convert(varchar, INV_SALES_HDR.DOC_DATE_GRE, 101) BETWEEN @d1 AND @d2 ORDER BY DOC_ID ASC";
                        //cmd.Parameters.Add("@d1", SqlDbType.Date).Value = StartDate.Value;
                        //cmd.Parameters.Add("@d2", SqlDbType.Date).Value = EndDate.Value;
                        Dictionary<string, object> parameter = new Dictionary<string, object>();
                        parameter.Add("@d1", StartDate.Value);
                        parameter.Add("@d2", EndDate.Value);
                        dt= DbFunctions.GetDataTable(query, parameter);

                        var grosstotal = ((from s in dt.AsEnumerable()
                                           select decimal.Parse(s["Gross Amount"].ToString())) as IEnumerable<decimal>).Sum();
                        var netamount = ((from s in dt.AsEnumerable()
                                          select decimal.Parse(s["Net Value"].ToString())) as IEnumerable<decimal>).Sum();
                        var taxamount = ((from s in dt.AsEnumerable()
                                          select decimal.Parse(s["Tax Total"].ToString())) as IEnumerable<decimal>).Sum();
                        var discount = ((from s in dt.AsEnumerable()
                                         select decimal.Parse(s["Discount"].ToString())) as IEnumerable<decimal>).Sum();
                        var freight = ((from s in dt.AsEnumerable()
                                        select decimal.Parse(s["Freight"].ToString())) as IEnumerable<decimal>).Sum();

                        dt.Rows.Add(null, null, null, null, "Total", taxamount, freight, grosstotal, discount, netamount);
                        dataGridView1.DataSource = dt;

                    }
                    else
                    {
                        string query = "SELECT INV_SALES_HDR.SALE_TYPE as [Sale Type],INV_SALES_HDR.DOC_ID AS [Inv.No],INV_SALES_HDR.DOC_NO AS [Voucher No],CASE WHEN INV_SALES_HDR.DOC_TYPE='SAL.CSS' THEN 'CASH SALES'  WHEN INV_SALES_HDR.DOC_TYPE='SAL.CRD' THEN 'CREDIT SALES'  WHEN INV_SALES_HDR.DOC_TYPE='SAL.CSR' THEN 'CASH SALE RETURN' WHEN INV_SALES_HDR.DOC_TYPE='SAL.CDR' THEN 'CREDIT SALE RETURN' ELSE '' END as Type, convert(varchar, INV_SALES_HDR.DOC_DATE_GRE, 101) as Date, ISNULL(INV_SALES_HDR.TAX_TOTAL,0) as 'Tax Total', ISNULL(INV_SALES_HDR.FREIGHT,0) as 'Freight', ISNULL(INV_SALES_HDR.TOTAL_AMOUNT,0) as 'Gross Amount', ISNULL(INV_SALES_HDR.DISCOUNT,0) as Discount,ISNULL(INV_SALES_HDR.NET_AMOUNT,0) as 'Net Value' FROM INV_SALES_HDR LEFT OUTER JOIN REC_CUSTOMER ON REC_CUSTOMER.CODE=INV_SALES_HDR.CUSTOMER_CODE WHERE REC_CUSTOMER.CODE=" + Cbx_supplier.SelectedValue + " AND  INV_SALES_HDR.DOC_TYPE='SAL.CRD' ORDER BY DOC_ID ASC";
                        //string query = "SELECT DOC_ID AS 'INVOICE NO',REC_CUSTOMER.DESC_ENG as 'CUSTOMER NAME',CAST(INV_SALES_HDR.DOC_DATE_GRE AS date) AS [DATE],NET_AMOUNT FROM INV_SALES_HDR LEFT OUTER JOIN REC_CUSTOMER ON REC_CUSTOMER.CODE=INV_SALES_HDR.CUSTOMER_CODE WHERE REC_CUSTOMER.CODE=" + Cbx_supplier.SelectedValue + " AND  INV_SALES_HDR.DOC_TYPE='SAL.CRD' ORDER BY DOC_ID ASC";
                        dt = DbFunctions.GetDataTable(query);
                        var grosstotal = ((from s in dt.AsEnumerable()
                                           select decimal.Parse(s["Gross Amount"].ToString())) as IEnumerable<decimal>).Sum();
                        var netamount = ((from s in dt.AsEnumerable()
                                          select decimal.Parse(s["Net Value"].ToString())) as IEnumerable<decimal>).Sum();
                        var taxamount = ((from s in dt.AsEnumerable()
                                          select decimal.Parse(s["Tax Total"].ToString())) as IEnumerable<decimal>).Sum();
                        var discount = ((from s in dt.AsEnumerable()
                                         select decimal.Parse(s["Discount"].ToString())) as IEnumerable<decimal>).Sum();
                        var freight = ((from s in dt.AsEnumerable()
                                        select decimal.Parse(s["Freight"].ToString())) as IEnumerable<decimal>).Sum();

                        dt.Rows.Add(null, null, null, null, "Total", taxamount, freight, grosstotal, discount, netamount);
                        dataGridView1.DataSource = dt;
                    }
                    
                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.Font = new Font(dataGridView1.DefaultCellStyle.Font, FontStyle.Bold);
                        //conn.Close();
                        //cmd.Dispose();
                    //}
                   
                }
                if (cmb_report.Text == "Bill Wise Summary")
                {

                    tabControl1.SelectedIndex = 1;
                    btnSave.PerformClick();

                }
                if (cmb_report.Text == "Tax Wise Summary")
                {

                    DataTable dt = new DataTable();
                    dt.Columns.Add("Sl No", typeof(System.String));
                    dt.Columns.Add("Date", typeof(System.String));
                    dt.Columns.Add("Inv No", typeof(System.String));
                    dt.Columns.Add("Party", typeof(System.String));
                    dt.Columns.Add("GSTIN / UIN", typeof(System.String));
                    dt.Columns.Add("City", typeof(System.String));
                    dt.Columns.Add("State", typeof(System.String));
                    dt.Columns.Add("Item name", typeof(System.String));
                    dt.Columns.Add("HSN", typeof(System.String));
                    dt.Columns.Add("Qty", typeof(System.String));
                    dt.Columns.Add("Unit", typeof(System.String));
                    dt.Columns.Add("Gross Total", typeof(System.String));
                    dt.Columns.Add("CGST%", typeof(System.String));
                    dt.Columns.Add("CGST", typeof(System.String));
                    dt.Columns.Add("SGST%", typeof(System.String));
                    dt.Columns.Add("SGST", typeof(System.String));
                    dt.Columns.Add("IGST%", typeof(System.String));
                    dt.Columns.Add("IGST", typeof(System.String));
                    dt.Columns.Add("Net Total", typeof(System.String));




                    string item_name = combo_item_name.SelectedValue.ToString();
                    string itemtype = TYPE.SelectedValue.ToString();
                    string category = DrpCategory.SelectedValue.ToString();
                    string group = Group.SelectedValue.ToString();
                    string trade_mark = Trademark.SelectedValue.ToString();
                    string saleType = "";
                    if (cmb_saletype.SelectedIndex > 0)
                    {
                        saleType = "'"+cmb_saletype.SelectedValue.ToString()+"'";
                    }
                    else
                    {
                        saleType = "'B2B'"+","+"'B2C'";
                    }
                    //conn.Open();
                    string query = "SELECT DISTINCT CONVERT(DECIMAL(18,2),ITEM_TAX_PER) AS ITEM_TAX_PER FROM INV_SALES_DTL";
                    //SqlDataAdapter adptr = new SqlDataAdapter(cmd);
                    //DataTable tax = new DataTable();
                    //adptr.Fill(tax);
                    //conn.Close();
                    DataTable tax = DbFunctions.GetDataTable(query);
                    //cmd.Dispose();
                    if (tax.Rows.Count > 0)
                    {
                        //SqlCommand cmd1 = new SqlCommand();
                        for (int i = 0; i < tax.Rows.Count; i++)
                        {
                            //    //CHECK TAX DETAILS IS ZERO OR MOT
                            DataTable rows = new DataTable();
                            if (Chk.Checked)
                            {
                                query = "SELECT count(*) as rowcounts FROM INV_SALES_HDR LEFT OUTER JOIN INV_SALES_DTL ON INV_SALES_HDR.DOC_NO=INV_SALES_DTL.DOC_NO LEFT OUTER JOIN INV_ITEM_DIRECTORY ON INV_ITEM_DIRECTORY.CODE=INV_SALES_DTL.ITEM_CODE LEFT OUTER JOIN (SELECT REC_CUSTOMER.*,GEN_CITY.CODE AS CITY,GEN_CITY.DESC_ENG AS CITY_NAME FROM REC_CUSTOMER LEFT OUTER JOIN GEN_CITY ON REC_CUSTOMER.CITY_CODE=GEN_CITY.CODE)REC_CUSTOMER ON REC_CUSTOMER.CODE=INV_SALES_HDR.CUSTOMER_CODE WHERE INV_ITEM_DIRECTORY.CODE IS NOT NULL AND INV_ITEM_DIRECTORY.DESC_ENG LIKE '%" + item_name + "%' AND INV_ITEM_DIRECTORY.CATEGORY LIKE '%" + category + "%' AND INV_ITEM_DIRECTORY.TRADEMARK LIKE '%" + trade_mark + "%' AND INV_ITEM_DIRECTORY.[GROUP] LIKE '%" + group + "%' AND  INV_SALES_DTL.ITEM_TAX_PER=" + tax.Rows[i]["ITEM_TAX_PER"] + " AND INV_SALES_HDR.SALE_TYPE IN (" + saleType + ") AND (convert(varchar, INV_SALES_HDR.DOC_DATE_GRE, 101) BETWEEN @d1 AND @d2)";
                                //cmd.Parameters.Add("@d1", SqlDbType.Date).Value = StartDate.Value;
                                //cmd.Parameters.Add("@d2", SqlDbType.Date).Value = EndDate.Value;

                                Dictionary<string, object> parameter = new Dictionary<string, object>();
                                parameter.Add("@d1", StartDate.Value);
                                parameter.Add("@d2", EndDate.Value);
                                rows = DbFunctions.GetDataTable(query,parameter);
                            }
                            else
                            {

                                query = "SELECT count(*) as rowcounts FROM INV_SALES_HDR LEFT OUTER JOIN INV_SALES_DTL ON INV_SALES_HDR.DOC_NO=INV_SALES_DTL.DOC_NO LEFT OUTER JOIN INV_ITEM_DIRECTORY ON INV_ITEM_DIRECTORY.CODE=INV_SALES_DTL.ITEM_CODE LEFT OUTER JOIN (SELECT REC_CUSTOMER.*,GEN_CITY.CODE AS CITY,GEN_CITY.DESC_ENG AS CITY_NAME FROM REC_CUSTOMER LEFT OUTER JOIN GEN_CITY ON REC_CUSTOMER.CITY_CODE=GEN_CITY.CODE)REC_CUSTOMER ON REC_CUSTOMER.CODE=INV_SALES_HDR.CUSTOMER_CODE WHERE INV_ITEM_DIRECTORY.CODE IS NOT NULL AND INV_ITEM_DIRECTORY.DESC_ENG LIKE '%" + item_name + "%' AND INV_ITEM_DIRECTORY.CATEGORY LIKE '%" + category + "%' AND INV_ITEM_DIRECTORY.TRADEMARK LIKE '%" + trade_mark + "%' AND INV_ITEM_DIRECTORY.[GROUP] LIKE '%" + group + "%' AND INV_SALES_HDR.SALE_TYPE IN (" + saleType + ") AND INV_SALES_DTL.ITEM_TAX_PER=" + tax.Rows[i]["ITEM_TAX_PER"];
                                rows = DbFunctions.GetDataTable(query);
                            }
                            //adptr = new SqlDataAdapter(cmd);
                            //adptr.Fill(rows);
                            //conn.Close();
                            //cmd.Dispose();
                            if (rows.Rows.Count > 0)
                            {
                                if (Convert.ToInt32(rows.Rows[0][0]) > 0)
                                {
                                    //TAX DETAILS FOR EVERY TAX
                                    dt.Rows.Add("", "Sales-Taxable " + tax.Rows[i]["ITEM_TAX_PER"].ToString(), "", "", "", "", "", "");
                                    indx.Add(dt.Rows.Count - 1);
                                    //conn.Open();
                                    DataTable details = new DataTable();
                                    if (Chk.Checked)
                                    {
                                        query = "SELECT ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS 'Sl No',INV_SALES_HDR.DOC_DATE_GRE as 'Date',INV_SALES_DTL.DOC_ID as'Inv No',REC_CUSTOMER.DESC_ENG AS 'Party',REC_CUSTOMER.TIN_NO AS 'GSTIN / UIN',REC_CUSTOMER.CITY_NAME AS City,('Kerala') as State,INV_ITEM_DIRECTORY.DESC_ENG as 'Item Name',INV_ITEM_DIRECTORY.HSN as HSN,INV_SALES_DTL.QUANTITY 'Qty',INV_SALES_DTL.UOM AS Unit,convert(decimal(18,2),INV_SALES_DTL.GROSS_TOTAL) AS 'Amount',convert(decimal(18,2),ITEM_TAX_PER/2) AS 'CGST%',convert(decimal(18,2),ITEM_TAX/2) AS 'CGST',convert(decimal(18,2),ITEM_TAX_PER/2) AS 'SGST%',convert(decimal(18,2),ITEM_TAX/2) AS 'SGST',('') AS 'IGST%',('0.00') AS 'IGST',convert(decimal(18,2),INV_SALES_DTL.ITEM_TOTAL) as 'Net Total' FROM INV_SALES_HDR LEFT OUTER JOIN INV_SALES_DTL ON INV_SALES_HDR.DOC_NO=INV_SALES_DTL.DOC_NO LEFT OUTER JOIN INV_ITEM_DIRECTORY ON INV_ITEM_DIRECTORY.CODE=INV_SALES_DTL.ITEM_CODE LEFT OUTER JOIN (SELECT REC_CUSTOMER.*,GEN_CITY.CODE AS CITY,GEN_CITY.DESC_ENG AS CITY_NAME FROM REC_CUSTOMER LEFT OUTER JOIN GEN_CITY ON REC_CUSTOMER.CITY_CODE=GEN_CITY.CODE) REC_CUSTOMER ON REC_CUSTOMER.CODE=INV_SALES_HDR.CUSTOMER_CODE WHERE INV_ITEM_DIRECTORY.CODE IS NOT NULL AND INV_ITEM_DIRECTORY.DESC_ENG LIKE '%" + item_name + "%' AND INV_ITEM_DIRECTORY.CATEGORY LIKE '%" + category + "%' AND INV_ITEM_DIRECTORY.TRADEMARK LIKE '%" + trade_mark + "%' AND INV_ITEM_DIRECTORY.[GROUP] LIKE '%" + group + "%' AND INV_SALES_HDR.SALE_TYPE IN (" + saleType + ") AND  INV_SALES_DTL.ITEM_TAX_PER=" + tax.Rows[i]["ITEM_TAX_PER"] + " AND convert(varchar, INV_SALES_HDR.DOC_DATE_GRE, 101) BETWEEN @d1 AND @d2 ORDER BY INV_SALES_HDR.DOC_DATE_GRE";
                                        //cmd1.Parameters.Add("@d1", SqlDbType.Date).Value = StartDate.Value;
                                        //cmd1.Parameters.Add("@d2", SqlDbType.Date).Value = EndDate.Value;
                                        Dictionary<string, object> parameter = new Dictionary<string, object>();
                                        parameter.Add("@d1", StartDate.Value);
                                        parameter.Add("@d2", EndDate.Value);
                                        details = DbFunctions.GetDataTable(query, parameter);
                                    }
                                    else
                                    {

                                        query = "SELECT ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS 'Sl No',INV_SALES_HDR.DOC_DATE_GRE as 'Date',INV_SALES_DTL.DOC_ID as'Inv No',REC_CUSTOMER.DESC_ENG AS 'Party',REC_CUSTOMER.TIN_NO AS 'GSTIN / UIN',REC_CUSTOMER.CITY_NAME AS City,('Kerala') as State,INV_ITEM_DIRECTORY.DESC_ENG as 'Item Name',INV_ITEM_DIRECTORY.HSN AS HSN,INV_SALES_DTL.QUANTITY 'Qty',INV_SALES_DTL.UOM AS Unit,convert(decimal(18,2),INV_SALES_DTL.GROSS_TOTAL) AS 'Amount',convert(decimal(18,2),ITEM_TAX_PER/2) AS 'CGST%',convert(decimal(18,2),ITEM_TAX/2) AS 'CGST',convert(decimal(18,2),ITEM_TAX_PER/2) AS 'SGST%',convert(decimal(18,2),ITEM_TAX/2) AS 'SGST',('') AS 'IGST%',('0.00') AS 'IGST',convert(decimal(18,2),INV_SALES_DTL.ITEM_TOTAL) as 'Net Total' FROM INV_SALES_HDR LEFT OUTER JOIN INV_SALES_DTL ON INV_SALES_HDR.DOC_NO=INV_SALES_DTL.DOC_NO LEFT OUTER JOIN INV_ITEM_DIRECTORY ON INV_ITEM_DIRECTORY.CODE=INV_SALES_DTL.ITEM_CODE LEFT OUTER JOIN (SELECT REC_CUSTOMER.*,GEN_CITY.CODE AS CITY,GEN_CITY.DESC_ENG AS CITY_NAME FROM REC_CUSTOMER LEFT OUTER JOIN GEN_CITY ON REC_CUSTOMER.CITY_CODE=GEN_CITY.CODE) REC_CUSTOMER ON REC_CUSTOMER.CODE=INV_SALES_HDR.CUSTOMER_CODE WHERE INV_ITEM_DIRECTORY.CODE IS NOT NULL AND INV_ITEM_DIRECTORY.DESC_ENG LIKE '%" + item_name + "%' AND INV_ITEM_DIRECTORY.CATEGORY LIKE '%" + category + "%' AND INV_ITEM_DIRECTORY.TRADEMARK LIKE '%" + trade_mark + "%' AND INV_ITEM_DIRECTORY.[GROUP] LIKE '%" + group + "%' AND INV_SALES_HDR.SALE_TYPE IN (" + saleType + ") AND INV_SALES_DTL.ITEM_TAX_PER=" + tax.Rows[i]["ITEM_TAX_PER"] + " ORDER BY INV_SALES_HDR.DOC_DATE_GRE";
                                        details = DbFunctions.GetDataTable(query);
                                    }
                                    //SqlDataAdapter adptr1 = new SqlDataAdapter(cmd1);                                
                                    //adptr1.Fill(details);
                                    //conn.Close();
                                    //cmd1.Dispose();
                                    //ADD TO DATATBLE
                                    if (details.Rows.Count > 0)
                                    {
                                        foreach (DataRow dr in details.Rows)
                                        {
                                            dt.Rows.Add(dr.ItemArray);
                                        }

                                    }
                                    //CALCULATE TOTAL
                                    details = new DataTable();
                                    if (Chk.Checked)
                                    {
                                        query = "SELECT convert(decimal(18,2),SUM(ISNULL(INV_SALES_DTL.QUANTITY,0)))AS 'Qty',convert(decimal(18,2),SUM(ISNULL(INV_SALES_DTL.ITEM_TAX,0))/2) AS 'CGST_TOTAL',convert(decimal(18,2),SUM(ISNULL(INV_SALES_DTL.ITEM_TAX,0))/2) AS 'SGST_TOTAL',convert(decimal(18,2),SUM(ISNULL(INV_SALES_DTL.GROSS_TOTAL,0))) AS 'Gross Total',convert(decimal(18,2),SUM(ISNULL(INV_SALES_DTL.ITEM_TOTAL,0))) as 'Net Total' FROM INV_SALES_HDR LEFT OUTER JOIN INV_SALES_DTL ON INV_SALES_HDR.DOC_NO=INV_SALES_DTL.DOC_NO LEFT OUTER JOIN INV_ITEM_DIRECTORY ON INV_ITEM_DIRECTORY.CODE=INV_SALES_DTL.ITEM_CODE LEFT OUTER JOIN (SELECT REC_CUSTOMER.*,GEN_CITY.CODE AS CITY,GEN_CITY.DESC_ENG AS CITY_NAME FROM REC_CUSTOMER LEFT OUTER JOIN GEN_CITY ON REC_CUSTOMER.CITY_CODE=GEN_CITY.CODE) REC_CUSTOMER ON REC_CUSTOMER.CODE=INV_SALES_HDR.CUSTOMER_CODE WHERE INV_ITEM_DIRECTORY.CODE IS NOT NULL AND INV_ITEM_DIRECTORY.DESC_ENG LIKE '%" + item_name + "%' AND INV_ITEM_DIRECTORY.CATEGORY LIKE '%" + category + "%' AND INV_ITEM_DIRECTORY.TRADEMARK LIKE '%" + trade_mark + "%' AND INV_ITEM_DIRECTORY.[GROUP] LIKE '%" + group + "%' AND INV_SALES_HDR.SALE_TYPE IN (" + saleType + ") AND  INV_SALES_DTL.ITEM_TAX_PER=" + tax.Rows[i]["ITEM_TAX_PER"] + " AND (Convert(Varchar,INV_SALES_HDR.DOC_DATE_GRE,101) BETWEEN @d1 AND @d2)";
                                        //cmd1.Parameters.Add("@d1", SqlDbType.Date).Value = StartDate.Value;
                                        //cmd1.Parameters.Add("@d2", SqlDbType.Date).Value = EndDate.Value;
                                        Dictionary<string, object> parameter = new Dictionary<string, object>();
                                        parameter.Add("@d1", StartDate.Value);
                                        parameter.Add("@d2", EndDate.Value);
                                        details = DbFunctions.GetDataTable(query, parameter);
                                    }
                                    else
                                    {

                                        query = "SELECT convert(decimal(18,2),SUM(ISNULL(INV_SALES_DTL.QUANTITY,0)))AS 'Qty',convert(decimal(18,2),SUM(ISNULL(INV_SALES_DTL.ITEM_TAX,0))/2) AS 'CGST_TOTAL',convert(decimal(18,2),SUM(ISNULL(INV_SALES_DTL.ITEM_TAX,0))/2) AS 'SGST_TOTAL',convert(decimal(18,2),SUM(ISNULL(INV_SALES_DTL.GROSS_TOTAL,0))) AS 'Gross Total',convert(decimal(18,2),SUM(ISNULL(INV_SALES_DTL.ITEM_TOTAL,0))) as 'Net Total' FROM INV_SALES_HDR LEFT OUTER JOIN INV_SALES_DTL ON INV_SALES_HDR.DOC_NO=INV_SALES_DTL.DOC_NO LEFT OUTER JOIN INV_ITEM_DIRECTORY ON INV_ITEM_DIRECTORY.CODE=INV_SALES_DTL.ITEM_CODE LEFT OUTER JOIN (SELECT REC_CUSTOMER.*,GEN_CITY.CODE AS CITY,GEN_CITY.DESC_ENG AS CITY_NAME FROM REC_CUSTOMER LEFT OUTER JOIN GEN_CITY ON REC_CUSTOMER.CITY_CODE=GEN_CITY.CODE) REC_CUSTOMER ON REC_CUSTOMER.CODE=INV_SALES_HDR.CUSTOMER_CODE WHERE INV_ITEM_DIRECTORY.CODE IS NOT NULL AND INV_ITEM_DIRECTORY.DESC_ENG LIKE '%" + item_name + "%' AND INV_ITEM_DIRECTORY.CATEGORY LIKE '%" + category + "%' AND INV_ITEM_DIRECTORY.TRADEMARK LIKE '%" + trade_mark + "%' AND INV_ITEM_DIRECTORY.[GROUP] LIKE '%" + group + "%' AND INV_SALES_HDR.SALE_TYPE IN (" + saleType + ") AND INV_SALES_DTL.ITEM_TAX_PER=" + tax.Rows[i]["ITEM_TAX_PER"];
                                        details = DbFunctions.GetDataTable(query);
                                    }
                                    //adptr1 = new SqlDataAdapter(cmd1);
                                    //details = new DataTable();
                                    //adptr1.Fill(details);
                                    //conn.Close();
                                    //cmd1.Dispose();
                                    //ADD TO DATATABLE
                                    if (details.Rows.Count > 0)
                                    {
                                        dt.Rows.Add("", "", "", "", "", "", "", "TOTAL", "", details.Rows[0][0].ToString(), "", details.Rows[0][3].ToString(), "", details.Rows[0][1].ToString(), "", details.Rows[0][2].ToString(), "", "", details.Rows[0][4].ToString());
                                        // dataGridView1.Rows[indx[i]].DefaultCellStyle.Font = new Font(dataGridView1.DefaultCellStyle.Font, FontStyle.Bold);
                                    }
                                }
                            }
                        }
                    }
                    dataGridView1.DataSource = dt;
                    for (int i = 0; i < indx.Count; i++)
                    {
                        int a = indx[i];
                        dataGridView1.Rows[indx[i]].DefaultCellStyle.Font = new Font(dataGridView1.DefaultCellStyle.Font, FontStyle.Bold);
                        dataGridView1.Rows[indx[i]].DefaultCellStyle.BackColor = Color.DimGray;
                        dataGridView1.Rows[indx[i]].DefaultCellStyle.ForeColor = Color.White;
                    }
                }
                indx.Clear();
                if (cmb_report.Text == "")
                {
                    dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[0];
                    Font font = new Font(dataGridView1.DefaultCellStyle.Font.FontFamily, 9, FontStyle.Bold);
                    dataGridView1.Rows[dataGridView1.RowCount - 1].DefaultCellStyle.Font = font;
                    dataGridView1.Rows[dataGridView1.RowCount - 1].DefaultCellStyle.ForeColor = Color.Red;
                    dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                    dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                    dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                    dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;

                    dataGridView1.Columns["Net Value"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridView1.Columns["Tax Total"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridView1.Columns["Freight"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridView1.Columns["Gross Amount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridView1.Columns["Discount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridView1.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
            else
            {
                string query;
                GetTotalDiscount();
                //string conection = Properties.Settings.Default.ConnectionStrings.ToString();
                DataTable1TableAdapter.Connection = DbFunctions.GetConnection();

                DataTable dt = new DataTable();
                //if(conn.State==ConnectionState.Closed)
                //conn.Open();
                query = "SELECT * FROM tbl_companysetup";
                dt = DbFunctions.GetDataTable(query);


                if (Chk.Checked)
                {
                    ReportParameter[] parameters = new ReportParameter[9];
                    parameters[0] = new ReportParameter("TotDiscount", TotalDiscount.ToString());
                    parameters[1] = new ReportParameter("type", chk_type.Checked.ToString());
                    parameters[2] = new ReportParameter("heading", dt.Rows[0]["company_name"].ToString());
                    parameters[3] = new ReportParameter("address", TotalDiscount.ToString());
                    parameters[4] = new ReportParameter("report_title", TotalDiscount.ToString());
                    parameters[5] = new ReportParameter("date", chk_date.Checked.ToString());
                    parameters[6] = new ReportParameter("itemcode", chk_code.Checked.ToString());
                    parameters[7] = new ReportParameter("unit", chk_unit.Checked.ToString());
                    parameters[8] = new ReportParameter("customer", chk_cname.Checked.ToString());
                    this.reportViewer1.LocalReport.SetParameters(parameters);
                    var setup = reportViewer1.GetPageSettings();
                    setup.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
                    reportViewer1.SetPageSettings(setup);

                    string name = "", code = "", typ = "", grp = "", cat = "", tm = "", doc = "", sup = "", styp="";
                    if (combo_item_name.Text != "")
                        name = combo_item_name.SelectedValue.ToString();
                    if (combo_item_id.Text != "")
                        code = combo_item_id.SelectedValue.ToString();
                    if (TYPE.Text != "")
                        typ = TYPE.SelectedValue.ToString();
                    if (Group.Text != "")
                        grp = Group.SelectedValue.ToString();
                    if (DrpCategory.Text != "")
                        cat = DrpCategory.SelectedValue.ToString();
                    if (Trademark.Text != "")
                        tm = Trademark.SelectedValue.ToString();
                    if (cmb_saletype.Text != "")
                        doc = cmb_saletype.SelectedValue.ToString();
                    if (Cbx_supplier.Text != "")
                        sup = Cbx_supplier.SelectedValue.ToString();
                    DateTime datestart, dateend;
                    datestart = Convert.ToDateTime(StartDate.Value.ToShortDateString());
                    dateend = Convert.ToDateTime(EndDate.Value.ToShortDateString());


                    if (Cbx_salemode.Text == "")
                    {
                        this.DataTable1TableAdapter.FillByDataDate(this.SalesReportDatasetOnHDR.DataTable1, grp, cat, tm, sup, typ, doc,name, datestart, dateend);
                    }
                    else
                    {
                        styp = Cbx_salemode.SelectedValue.ToString();
                        this.DataTable1TableAdapter.FillBySaleTypeDate(this.SalesReportDatasetOnHDR.DataTable1, grp, cat, tm, sup, typ, styp, datestart, dateend,doc,name);
                    }
                    reportViewer1.RefreshReport();


                }
                else
                {
                    ReportParameter[] parameters = new ReportParameter[9];

                    parameters[0] = new ReportParameter("TotDiscount", TotalDiscount.ToString());
                    parameters[1] = new ReportParameter("type", chk_type.Checked.ToString());
                    parameters[2] = new ReportParameter("heading", dt.Rows[0]["company_name"].ToString());
                    parameters[3] = new ReportParameter("address", TotalDiscount.ToString());
                    parameters[4] = new ReportParameter("report_title", TotalDiscount.ToString());
                    parameters[5] = new ReportParameter("date", chk_date.Checked.ToString());
                    parameters[6] = new ReportParameter("itemcode", chk_code.Checked.ToString());
                    parameters[7] = new ReportParameter("unit", chk_unit.Checked.ToString());
                    parameters[8] = new ReportParameter("customer", chk_cname.Checked.ToString());
                    this.reportViewer1.LocalReport.SetParameters(parameters);
                    var setup = reportViewer1.GetPageSettings();
                    setup.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
                    reportViewer1.SetPageSettings(setup);
                    string name = "", code = "", typ = "", grp = "", cat = "", tm = "", doc = "", sup = "", co = "", na = "",styp="";
                    if (cmb.Text != "")
                        name = combo_item_name.SelectedValue.ToString();
                    if (combo_item_id.Text != "")
                        code = combo_item_id.SelectedValue.ToString();
                    if (TYPE.Text != "")
                        typ = TYPE.SelectedValue.ToString();
                    if (Group.Text != "")
                        grp = Group.SelectedValue.ToString();
                    if (DrpCategory.Text != "")
                        cat = DrpCategory.SelectedValue.ToString();
                    if (Trademark.Text != "")
                        tm = Trademark.SelectedValue.ToString();
                    if (cmb_saletype.Text != "")
                        doc = cmb_saletype.SelectedValue.ToString();
                    if (Cbx_supplier.Text != "")
                        sup = Cbx_supplier.SelectedValue.ToString();
                    // name = combo_item_name.SelectedValue.ToString();
                    // code = combo_item_id.SelectedValue.ToString();
                 
                       //this.DataTable1TableAdapter.FillDataOnHDR(this.SalesReportDatasetOnHDR.DataTable1, grp, cat, tm, sup, typ, doc,name);

                    if (Cbx_salemode.Text == "")
                    {
                        try
                        {
                            this.DataTable1TableAdapter.FillDataOnHDR(this.SalesReportDatasetOnHDR.DataTable1, grp, cat, tm, sup, typ, name, doc, name);
                        }
                        catch { }
                    }
                    else
                    {
                        styp = Cbx_salemode.SelectedValue.ToString();
                        this.DataTable1TableAdapter.FillBySaleType(this.SalesReportDatasetOnHDR.DataTable1, grp, cat, tm, sup, typ, name,styp, doc);
                    }//  parameters[1] = new ReportParameter("bill", "False");
                    reportViewer1.RefreshReport();
                    ReportParameter[] parameters1 = new ReportParameter[1];
                    if (checkBox1.Checked)
                    {
                        parameters1[0] = new ReportParameter("chkview", "True");
                    }
                    else
                    {
                        parameters1[0] = new ReportParameter("column_visible", "False");
                    }
                    this.reportViewer1.LocalReport.SetParameters(parameters);
                    setup = reportViewer1.GetPageSettings();
                    setup.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
                    reportViewer1.SetPageSettings(setup);
                    reportViewer1.RefreshReport();
                    report = reportViewer1.LocalReport;
                }
            }
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //if (dataGridView1.Columns.Count > 3)
            //    dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            //dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        }

        private void Chk_CheckedChanged(object sender, EventArgs e)
        {
            if (Chk.Checked)
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
        public void GetTotalDiscount()
        {
            try
            {
                DataTable dt = new DataTable();
                string query;
                if (Chk.Checked)
                {
                  //  cmd.CommandText = "select sum(DISCOUNT) FROM INV_SALES_HDR where  (DOC_DATE_GRE >= @date) AND (DOC_DATE_GRE <= @date2)";
                    
                    query = "sp_DiscountTotalDate";
                    //cmd.Parameters.AddWithValue("@date", StartDate.Value.ToString("MM/dd/yyyy"));
                    //cmd.Parameters.AddWithValue("@date2", EndDate.Value.ToString("MM/dd/yyyy"));

                    Dictionary<string, object> parameter = new Dictionary<string, object>();
                    parameter.Add("@date", StartDate.Value.ToString("MM/dd/yyyy"));
                    parameter.Add("@date2", EndDate.Value.ToString("MM/dd/yyyy"));
                    dt=DbFunctions.GetDataTableProcedure(query, parameter);
                }
                else
                {
                    //cmd.CommandText = "select sum(DISCOUNT) FROM INV_SALES_HDR";
                    //cmd.Connection = conn;
                    query = "sp_DiscountTotal";
                    dt = DbFunctions.GetDataTableProcedure(query);
                }
                //cmd.CommandType = CommandType.StoredProcedure;
                //adapter = new SqlDataAdapter(cmd);
                //adapter.Fill(dt);
               
                if (dt.Rows.Count > 0)
                {
                    TotalDiscount = 0;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TotalDiscount =TotalDiscount+ Convert.ToDecimal(dt.Rows[i][0].ToString());
                    }

                  //  MessageBox.Show(dt.Rows[0][0].ToString());
                }
            }
            catch(Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }
        private void Category_Enter(object sender, EventArgs e)
        {

        }

        private void combo_item_name_Leave(object sender, EventArgs e)
        {
            btnSave.Focus();
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            //GetTotalDiscount();
           // string conection = Properties.Settings.Default.ConnectionStrings.ToString();
           // DataTable1TableAdapter.Connection = new System.Data.SqlClient.SqlConnection(conection);
            DataTable1TableAdapter.Connection = Model.DbFunctions.GetConnection();
          
                try
                {
                    ReportParameter[] parameters = new ReportParameter[1];

                   parameters[0] = new ReportParameter("TotDiscount", TotalDiscount.ToString());
                    //  parameters[1] = new ReportParameter("bill", "False");
                    //   parameters[0] = new ReportParameter("Date", "false");
                    //  if (HasType)
                    //      parameters[0] = new ReportParameter("Type", "True");
                    //  else
                    //      parameters[0] = new ReportParameter("Type", "false");
                    //  if (HasGroup)
                    //      parameters[1] = new ReportParameter("Group", "True");
                    //  else
                    //      parameters[1] = new ReportParameter("Group", "false");
                    //  if (HasCategory)
                    //      parameters[2] = new ReportParameter("Category", "True");
                    //  else
                    //      parameters[2] = new ReportParameter("Category", "false");
                    //  if (HasTM)
                    //      parameters[3] = new ReportParameter("Tm", "True");
                    //  else
                    //      parameters[3] = new ReportParameter("Tm", "false");

                    this.reportViewer1.LocalReport.SetParameters(parameters);
                    var setup = reportViewer1.GetPageSettings();
                    setup.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
                    reportViewer1.SetPageSettings(setup);
                    string name = "", code = "", typ = "", grp = "", cat = "", tm = "", doc = "", sup = "", co = "", na = "";
                    if (combo_item_name.Text != "")
                        name = combo_item_name.SelectedValue.ToString();
                    if (combo_item_id.Text != "")
                        code = combo_item_id.SelectedValue.ToString();
                    if (TYPE.Text != "")
                        typ = TYPE.SelectedValue.ToString();
                    if (Group.Text != "")
                        grp = Group.SelectedValue.ToString();
                    if (DrpCategory.Text != "")
                        cat = DrpCategory.SelectedValue.ToString();
                    if (Trademark.Text != "")
                        tm = Trademark.SelectedValue.ToString();
                    if (Cbx_salemode.Text != "")
                        doc = Cbx_salemode.SelectedValue.ToString();
                    if (Cbx_supplier.Text != "")
                        sup = Cbx_supplier.SelectedValue.ToString();
                    name = combo_item_name.SelectedValue.ToString();
                    code = combo_item_id.SelectedValue.ToString();
                    this.DataTable1TableAdapter.FillByDeletedInvoice (this.SalesReportDatasetOnHDR.DataTable1, grp, cat, tm, sup, typ, doc);
                    //  parameters[1] = new ReportParameter("bill", "False");
                    reportViewer1.RefreshReport();

                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }


        }

        private void cmb_month_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        
        private void combo_item_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try
            //{
               
            //    if (combo_item_name.SelectedIndex > 0)
            //    {

            //        cmb.Text = combo_item_name.SelectedValue.ToString();
            //            //DataTable dt = new DataTable();
            //            //conn.Open();
            //            //SqlCommand cmd = new SqlCommand("SELECT CODE FROM INV_ITEM_DIRECTORY WHERE DESC_ENG='" + combo_item_name.SelectedValue.ToString() + "'", conn);
            //            //// SqlCommand cmd = new SqlCommand("SELECT SALES.ITEM_CODE,INV_ITEM_DIRECTORY.DESC_ENG AS ITEM_NAME,SALES.QTY,SALES.TOTAL FROM INV_ITEM_DIRECTORY LEFT OUTER JOIN (SELECT ITEM_CODE,SUM(QUANTITY) AS QTY,SUM(GROSS_TOTAL) AS TOTAL FROM INV_SALES_DTL GROUP BY ITEM_CODE) AS SALES ON INV_ITEM_DIRECTORY.CODE=SALES.ITEM_CODE LEFT OUTER JOIN INV_ITEM_CATEGORY ON INV_ITEM_DIRECTORY.CATEGORY=INV_ITEM_CATEGORY.CODE LEFT OUTER JOIN INV_ITEM_GROUP ON INV_ITEM_DIRECTORY.[GROUP]=INV_ITEM_GROUP.CODE LEFT OUTER JOIN INV_ITEM_TM ON INV_ITEM_DIRECTORY.TRADEMARK=INV_ITEM_TM.CODE WHERE ITEM_CODE IS NOT NULL AND INV_ITEM_DIRECTORY.DESC_ENG LIKE '%" + item_name + "%' AND INV_ITEM_DIRECTORY.CATEGORY LIKE '%" + category + "%' AND INV_ITEM_DIRECTORY.TRADEMARK LIKE '%" + trade_mark + "%' AND INV_ITEM_DIRECTORY.[GROUP]='%" + group + "%'", conn);
            //            //SqlDataAdapter adptr = new SqlDataAdapter(cmd);
            //            //adptr.Fill(dt);
            //            //conn.Close();
            //            //if (dt.Rows.Count > 0)
            //            //{
            //            //    cmb.SelectedValue = dt.Rows[0][0].ToString();
            //            //}
                    
            //    }
            //}
            //catch(Exception E)
            //{

            //}
        }

        private void combo_item_id_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                cmb.SelectedValue = combo_item_id.SelectedValue;
               
            }
            catch
            {

            }
        }

        private void cmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb.SelectedIndex > 0)
            {
                combo_item_name.SelectedValue = cmb.Text;
                combo_item_id.SelectedValue = cmb.SelectedValue;
            }
            else
            {
                combo_item_id.SelectedValue = 0;
                combo_item_name.SelectedValue = 0;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (dataGridView1.Columns.Count > 0)
            {
                dataGridView1.Columns.Clear();
            }
            TYPE.SelectedIndex = 0;
            combo_item_name.SelectedIndex = 0;
            combo_item_id.SelectedIndex = 0;
            Group.SelectedIndex = 0;
            Trademark.SelectedIndex = 0;
            cmb_salesman.SelectedIndex = 0;
            Cbx_supplier.SelectedIndex = 0;
            cmb.SelectedIndex = 0;
        }

        private void chk_code_CheckedChanged(object sender, EventArgs e)
        {
            
            
            btnSave.PerformClick();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();


                // creating new WorkBook within Excel application
                Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);


                // creating new Excelsheet in workbook
                Microsoft.Office.Interop.Excel._Worksheet worksheet = null;

                // see the excel sheet behind the program
                app.Visible = true;

                // get the reference of first sheet. By default its name is Sheet1.
                // store its reference to worksheet
                worksheet = workbook.Sheets["Sheet1"];
                worksheet = workbook.ActiveSheet;

                // changing the name of active sheet
                worksheet.Name = "Exported from gridview";


               
                DataTable dt = new DataTable();
                //if (conn.State == ConnectionState.Closed)
                //    conn.Open();
                string query = "SELECT * FROM tbl_companysetup";
                dt = DbFunctions.GetDataTable(query);
                //SqlDataAdapter adptr = new SqlDataAdapter(cmd);
                //adptr.Fill(dt);
                //conn.Close();
                string report = "";
                if (Chk.Checked)
                {
                    if (cmb_report.Text == "Salesman Summary")
                        report = cmb_report.Text + " for the period from " + StartDate.Value.ToShortDateString() + " to " + EndDate.Value.ToShortDateString()+"("+cmb_salesman.Text+")";
                    else
                    report = cmb_report.Text + " for the period from " + StartDate.Value.ToShortDateString() + " to " + EndDate.Value.ToShortDateString();
                }
                else
                {
                    if (cmb_report.Text == "Salesman Summary")
                        report = cmb_report.Text + "(" + cmb_salesman.Text + ")";
                    else
                    report = cmb_report.Text;
                }

                //heading
                worksheet.Cells[1,3]="       "+dt.Rows[0]["company_name"].ToString();
                worksheet.Cells[2,3] = dt.Rows[0]["address"].ToString();
                worksheet.Cells[2, 3] = dt.Rows[0]["address"].ToString();
                worksheet.Cells[3, 3] = report; ;
                //merging
                worksheet.Range[worksheet.Cells[1, 2], worksheet.Cells[1, 4]].Merge();
                worksheet.Range[worksheet.Cells[2, 2], worksheet.Cells[2, 4]].Merge();
                worksheet.Range[worksheet.Cells[3, 2], worksheet.Cells[3, 4]].Merge();

                //font bold
                worksheet.Cells[1, 2].EntireRow.Font.Bold = true;
                worksheet.Cells[1, 2].Interior.Color = Color.FromArgb(192, 192, 192);
  

               //  storing header part in Excel
                for (int i = 0; i < (dataGridView1.Columns.Count); i++)
                {
                    worksheet.Cells[5, i+1] = dataGridView1.Columns[i].HeaderText;
                    worksheet.Cells[5, i + 1].EntireRow.Font.Bold = true;
                    worksheet.Columns[i + 1].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                  
                   // worksheet.Columns[i + 1].ColumnWidth = dataGridView1.Columns[i].Width;
                }

               // worksheet.Columns[1].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                // storing Each row and column value to excel sheet
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    for (int j = 0; j < (dataGridView1.Columns.Count); j++)
                    {
                        if (j == 1)
                        {
                            worksheet.Cells[i + 6, j + 1] =dataGridView1.Rows[i].Cells[j].Value.ToString();
                           

                        }
                        else
                        {
                            worksheet.Cells[i + 6, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                        }
                        if (cmb_report.Text == "Tax Wise Summary")
                        {
                            if (dataGridView1.Rows[i].Cells[0].Value =="")
                            {
                                worksheet.Cells[i + 6, 2].Interior.Color = Color.FromArgb(232, 218, 239);
                               
                            }

                        }
                    }
                }
                //if (cmb_report.Text == "Salesman Summary" || cmb_report.Text == "Customer Summary" || cmb_report.Text == "Tax Wise Summary")
                //{
                //    Microsoft.Office.Interop.Excel.Range formatRange;
                //    formatRange = worksheet.get_Range("DATE", "DATE");
                //    formatRange.NumberFormat = "mm/dd/yyyy";
                //    //formatRange.NumberFormat = "mm/dd/yyyy hh:mm:ss";
                //    worksheet.Cells[1, 1] = "31/5/2014";
                //}
                worksheet.Columns.AutoFit();
                worksheet.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlPortrait;
                worksheet.PageSetup.Zoom = false;
                worksheet.PageSetup.FitToPagesTall = 100;
                worksheet.PageSetup.FitToPagesWide = 1;
                // save the application
                System.Windows.Forms.SaveFileDialog saveDlg = new System.Windows.Forms.SaveFileDialog();
                saveDlg.InitialDirectory = @"D:\";
                saveDlg.Filter = "Excel files (*.xls)|*.xls";
                saveDlg.FilterIndex = 0;
                saveDlg.RestoreDirectory = true;
                saveDlg.Title = "Export Excel File To";
                if (saveDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string path = saveDlg.FileName;
                    workbook.SaveCopyAs(path);
                    workbook.Saved = true;
                    workbook.Close(true, Type.Missing, Type.Missing);
                    app.Quit();
                }
              //  workbook.SaveAs("D:\\output.xls", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                // Exit from the application
              //  app.Quit();

            }
            catch (Exception ex)
            {
                string st = ex.Message;
            }
        }

        private void cmb_report_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_report.Text == "Tax Wise Summary")
            {
                linkLabel3.Visible = true;
                btnexport.Enabled = true;
            }
            else if (cmb_report.Text == "Bill Wise Summary")
            {
                btnexport.Enabled = false;
            }
            else
            {
                btnexport.Enabled = true;
                linkLabel3.Visible = false;
            }
        }

        private void cmb_salesman_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_salesman.SelectedIndex > 0)
            {
                dt = stkrpt.BindCustomerbY_Saleman(cmb_salesman.SelectedValue.ToString());

            }
            else
            {
                 dt = stkrpt.BindCustomer();
            }
                Cbx_supplier.ValueMember = "CODE";
                Cbx_supplier.DisplayMember = "DESC_ENG";
                DataRow row = dt.NewRow();
                row[0] = "";
                dt.Rows.InsertAt(row, 0);
                Cbx_supplier.DataSource = dt;
            
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!Chk.Checked)
            {
                MessageBox.Show("please select period");
                return;
            }
            try
            {
                Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
                Microsoft.Office.Interop.Excel._Worksheet worksheet = null;

                app.Visible = true;

                //B2B
                worksheet = workbook.Sheets["Sheet1"];
                worksheet = workbook.ActiveSheet;
                worksheet.Name = "B2B";
                DataTable dt = new DataTable();
                //if (conn.State == ConnectionState.Closed)
                //    conn.Open();
                string query = "SELECT * FROM tbl_companysetup";
                dt = DbFunctions.GetDataTable(query);
                //SqlDataAdapter adptr = new SqlDataAdapter(cmd);
                //adptr.Fill(dt);
                //conn.Close();
                string report = "";

                //heading
                worksheet.Cells[1, 3] = "       " + dt.Rows[0]["company_name"].ToString();
                worksheet.Cells[2, 3] = dt.Rows[0]["address"].ToString();
                worksheet.Cells[3, 3] = "4A-B2B(Other than Reverse Charge & E-Commerce Operator)";
                worksheet.Cells[5, 1] = "GISTIN / UIN";
                worksheet.Cells[5, 2] = "Party";
                worksheet.Cells[5, 3] = "Invoice No";
                worksheet.Cells[5, 4] = "Invoice Date";
                worksheet.Cells[5, 5] = "Invoice Value";
                worksheet.Cells[5, 6] = "Rate(%)";
                worksheet.Cells[5, 7] = "Taxable value";
                worksheet.Cells[5, 8] = "Integrated Tax";
                worksheet.Cells[5, 9] = "Central Tax";
                worksheet.Cells[5, 10] = "State Tax";

                worksheet.Cells[5, 1].EntireRow.Font.Bold = true;
                int indx = 6;

                //GET DATA
                dt = new DataTable();
                //if (conn.State == ConnectionState.Closed)
                //    conn.Open();

                if (Chk.Checked)
                {
                    //cmd = new SqlCommand("SELECT REC_CUSTOMER.TIN_NO AS 'GISTIN / UIN',REC_CUSTOMER.DESC_ENG AS Party,TAX_BILL.DOC_ID as 'Inv No',CONVERT(varchar,INV_SALES_HDR.DOC_DATE_GRE,103) as 'Inv Date',CONVERT(DECIMAL(18,2),TAX_BILL.TOTAL) AS 'Inv Value',CONVERT(DECIMAL(18,2),TAX_BILL.ITEM_TAX_PER) AS 'Rate',CONVERT(DECIMAL(18,2),TAX_BILL.GROSS) AS 'Taxable Value',CASE WHEN REC_CUSTOMER.STATE='KL' THEN 0 ELSE CONVERT(DECIMAL(18,2),TAX_BILL.TAX) END AS 'Integrated Tax',CASE WHEN REC_CUSTOMER.STATE='KL' THEN CONVERT(DECIMAL(18,2),TAX_BILL.TAX/2) ELSE 0 END AS 'Central Tax',CASE WHEN REC_CUSTOMER.STATE='KL' THEN CONVERT(DECIMAL(18,2),TAX_BILL.TAX/2) ELSE 0 END AS 'State Tax' FROM (SELECT INV_SALES_HDR.DOC_ID,REC_CUSTOMER.CODE AS CUSTOMER_CODE,INV_SALES_DTL.ITEM_TAX_PER,sum(INV_SALES_DTL.ITEM_TAX) AS TAX,sum(INV_SALES_DTL.GROSS_TOTAL) AS GROSS,SUM(INV_SALES_DTL.ITEM_TOTAL) AS TOTAL FROM INV_SALES_DTL LEFT OUTER JOIN INV_SALES_HDR ON INV_SALES_HDR.DOC_ID=INV_SALES_DTL.DOC_ID  LEFT OUTER JOIN (SELECT * FROM REC_CUSTOMER WHERE TIN_NO!='') AS REC_CUSTOMER ON REC_CUSTOMER.CODE=INV_SALES_HDR.CUSTOMER_CODE WHERE REC_CUSTOMER.CODE IS NOT NULL AND IN_SALES_HDR.DATED BETWEEN @d1 AND @d2 GROUP BY INV_SALES_HDR.DOC_ID,REC_CUSTOMER.CODE,INV_SALES_DTL.ITEM_TAX_PER) AS TAX_BILL  LEFT OUTER JOIN (SELECT * FROM REC_CUSTOMER WHERE TIN_NO!='') AS REC_CUSTOMER ON REC_CUSTOMER.CODE=TAX_BILL.CUSTOMER_CODE LEFT OUTER JOIN INV_SALES_HDR ON TAX_BILL.DOC_ID=INV_SALES_HDR.DOC_ID ORDER BY INV_SALES_HDR.DOC_DATE_GRE,TAX_BILL.ITEM_TAX_PER", conn);
                    query = @"SELECT        REC_CUSTOMER.TIN_NO,INV_SALES_DTL.ITEM_TAX_PER, CASE WHEN REC_CUSTOMER.STATE='KL'THEN SUM(INV_SALES_DTL.ITEM_TAX) ELSE 0 END  AS ITEM_TAX_CGST,CASE WHEN REC_CUSTOMER.STATE!='KL'THEN SUM(INV_SALES_DTL.ITEM_TAX)  ELSE 0 END AS ITEM_TAX_IGST,
                         REC_CUSTOMER.DESC_ENG as CUSTOMER, SUM(INV_SALES_DTL.GROSS_TOTAL) AS GROSS_TOTAL, SUM(INV_SALES_DTL.ITEM_TOTAL) AS ITEM_TOTAL, INV_SALES_HDR.DOC_ID, 
                         INV_SALES_HDR.DOC_DATE_GRE
FROM            INV_SALES_DTL INNER JOIN
                         INV_SALES_HDR ON INV_SALES_DTL.DOC_NO = INV_SALES_HDR.DOC_NO INNER JOIN
                         REC_CUSTOMER ON INV_SALES_HDR.CUSTOMER_CODE = REC_CUSTOMER.CODE
WHERE        (INV_SALES_HDR.FLAGDEL = 1) AND (INV_SALES_HDR.DOC_DATE_GRE BETWEEN @d1 AND @d2)
GROUP BY INV_SALES_DTL.ITEM_TAX_PER, INV_SALES_HDR.SALE_TYPE, INV_SALES_HDR.CUSTOMER_CODE, REC_CUSTOMER.DESC_ENG, INV_SALES_HDR.DOC_ID, 
                         INV_SALES_HDR.DOC_DATE_GRE, REC_CUSTOMER.TIN_NO,REC_CUSTOMER.STATE
HAVING        (INV_SALES_HDR.SALE_TYPE = N'B2B')
ORDER BY INV_SALES_HDR.DOC_ID, INV_SALES_HDR.DOC_DATE_GRE";
                    //cmd.Parameters.Add("@d1", SqlDbType.Date).Value = StartDate.Value;
                    //cmd.Parameters.Add("@d2", SqlDbType.Date).Value = EndDate.Value;

                    Dictionary<string, object> parameter = new Dictionary<string, object>();
                    parameter.Add("@d1", StartDate.Value);
                    parameter.Add("@d2", EndDate.Value);
                    dt = DbFunctions.GetDataTable(query, parameter);

                }
               
                //adptr = new SqlDataAdapter(cmd);
                //adptr.Fill(dt);
                //conn.Close();

                decimal inv_total = 0;
                decimal gross_total = 0;
                decimal cgst_total = 0;
                decimal igst_total = 0;
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        worksheet.Cells[indx, 1] = dt.Rows[i]["TIN_NO"].ToString();
                        worksheet.Cells[indx, 2] = dt.Rows[i]["CUSTOMER"].ToString();
                        worksheet.Cells[indx, 3] = dt.Rows[i]["DOC_ID"].ToString();
                        worksheet.Cells[indx, 4] = dt.Rows[i]["DOC_DATE_GRE"].ToString();
                        worksheet.Cells[indx, 5] = dt.Rows[i]["ITEM_TOTAL"].ToString();
                        worksheet.Cells[indx, 6] = dt.Rows[i]["ITEM_TAX_PER"].ToString();
                        worksheet.Cells[indx, 7] = dt.Rows[i]["GROSS_TOTAL"].ToString();
                        worksheet.Cells[indx, 8] = dt.Rows[i]["ITEM_TAX_IGST"].ToString();
                        worksheet.Cells[indx, 9] = (Convert.ToDecimal(dt.Rows[i]["ITEM_TAX_CGST"]) / 2).ToString();
                        worksheet.Cells[indx, 10] = (Convert.ToDecimal(dt.Rows[i]["ITEM_TAX_CGST"]) / 2).ToString();
                        inv_total += Convert.ToDecimal(dt.Rows[i]["ITEM_TOTAL"]);
                        gross_total += Convert.ToDecimal(dt.Rows[i]["GROSS_TOTAL"]);
                        cgst_total += Convert.ToDecimal(dt.Rows[i]["ITEM_TAX_CGST"])/2;
                        igst_total += Convert.ToDecimal(dt.Rows[i]["ITEM_TAX_IGST"]);
                        indx++;
                    }
                }
                

                //Total
               

               
                //adptr = new SqlDataAdapter(cmd);
                //adptr.Fill(dt);
                //conn.Close();
                if (dt.Rows.Count > 0)
                {

                    worksheet.Cells[indx, 3] = "Total";
                    worksheet.Cells[indx, 5] = inv_total.ToString();
                    worksheet.Cells[indx, 7] = gross_total.ToString();
                    worksheet.Cells[indx, 8] = igst_total.ToString();
                    worksheet.Cells[indx, 9] = cgst_total.ToString();
                    worksheet.Cells[indx, 10] = cgst_total.ToString();
                    worksheet.Cells[indx, 1].EntireRow.Font.Bold = true;
                }


                //B2C
                Microsoft.Office.Interop.Excel._Worksheet worksheet2 = workbook.Sheets.Add(Type.Missing, Type.Missing, 1, Type.Missing);
                worksheet2.Name = "B2C";
               
                 dt = new DataTable();
                //if (conn.State == ConnectionState.Closed)
                //    conn.Open();
                query = "SELECT * FROM tbl_companysetup";
                dt = DbFunctions.GetDataTable(query);
                // adptr = new SqlDataAdapter(cmd);
                //adptr.Fill(dt);
                //conn.Close();
                 report = "";

                //heading
                 worksheet2.Cells[1, 3] = "       " + dt.Rows[0]["company_name"].ToString();
                 worksheet2.Cells[2, 3] = dt.Rows[0]["address"].ToString();
                 worksheet2.Cells[3, 3] = "4A-B2C(Other than Reverse Charge & E-Commerce Operator)";
                 worksheet2.Cells[5, 1] = "GISTIN / UIN";
                 worksheet2.Cells[5, 2] = "Party";
                 worksheet2.Cells[5, 3] = "Invoice No";
                 worksheet2.Cells[5, 4] = "Invoice Date";
                 worksheet2.Cells[5, 5] = "Invoice Value";
                 worksheet2.Cells[5, 6] = "Rate(%)";
                worksheet2.Cells[5, 7] = "Taxable value";
                worksheet2.Cells[5, 8] = "Integrated Tax";
                worksheet2.Cells[5, 9] = "Central Tax";
                worksheet2.Cells[5, 10] = "State Tax";

                worksheet2.Cells[5, 1].EntireRow.Font.Bold = true;
                indx = 6;
                //GET DATA
                dt = new DataTable();
                //if (conn.State == ConnectionState.Closed)
                //    conn.Open();

                if (Chk.Checked)
                {
                    //cmd = new SqlCommand("SELECT REC_CUSTOMER.TIN_NO AS 'GISTIN / UIN',REC_CUSTOMER.DESC_ENG AS Party,TAX_BILL.DOC_ID as 'Inv No',CONVERT(varchar,INV_SALES_HDR.DOC_DATE_GRE,103) as 'Inv Date',CONVERT(DECIMAL(18,2),TAX_BILL.TOTAL) AS 'Inv Value',CONVERT(DECIMAL(18,2),TAX_BILL.ITEM_TAX_PER) AS 'Rate',CONVERT(DECIMAL(18,2),TAX_BILL.GROSS) AS 'Taxable Value',CASE WHEN REC_CUSTOMER.STATE='KL' THEN 0 ELSE CONVERT(DECIMAL(18,2),TAX_BILL.TAX) END AS 'Integrated Tax',CASE WHEN REC_CUSTOMER.STATE='KL' THEN CONVERT(DECIMAL(18,2),TAX_BILL.TAX/2) ELSE 0 END AS 'Central Tax',CASE WHEN REC_CUSTOMER.STATE='KL' THEN CONVERT(DECIMAL(18,2),TAX_BILL.TAX/2) ELSE 0 END AS 'State Tax' FROM (SELECT INV_SALES_HDR.DOC_ID,REC_CUSTOMER.CODE AS CUSTOMER_CODE,INV_SALES_DTL.ITEM_TAX_PER,sum(INV_SALES_DTL.ITEM_TAX) AS TAX,sum(INV_SALES_DTL.GROSS_TOTAL) AS GROSS,SUM(INV_SALES_DTL.ITEM_TOTAL) AS TOTAL FROM INV_SALES_DTL LEFT OUTER JOIN INV_SALES_HDR ON INV_SALES_HDR.DOC_ID=INV_SALES_DTL.DOC_ID  LEFT OUTER JOIN (SELECT * FROM REC_CUSTOMER WHERE TIN_NO!='') AS REC_CUSTOMER ON REC_CUSTOMER.CODE=INV_SALES_HDR.CUSTOMER_CODE WHERE REC_CUSTOMER.CODE IS NOT NULL AND IN_SALES_HDR.DATED BETWEEN @d1 AND @d2 GROUP BY INV_SALES_HDR.DOC_ID,REC_CUSTOMER.CODE,INV_SALES_DTL.ITEM_TAX_PER) AS TAX_BILL  LEFT OUTER JOIN (SELECT * FROM REC_CUSTOMER WHERE TIN_NO!='') AS REC_CUSTOMER ON REC_CUSTOMER.CODE=TAX_BILL.CUSTOMER_CODE LEFT OUTER JOIN INV_SALES_HDR ON TAX_BILL.DOC_ID=INV_SALES_HDR.DOC_ID ORDER BY INV_SALES_HDR.DOC_DATE_GRE,TAX_BILL.ITEM_TAX_PER", conn);
                    query = @"SELECT        INV_SALES_DTL.ITEM_TAX_PER, SUM(INV_SALES_DTL.ITEM_TAX) AS ITEM_TAX, 
                         REC_CUSTOMER.DESC_ENG as CUSTOMER, SUM(INV_SALES_DTL.GROSS_TOTAL) AS GROSS_TOTAL, SUM(INV_SALES_DTL.ITEM_TOTAL) AS ITEM_TOTAL, INV_SALES_HDR.DOC_ID, 
                         INV_SALES_HDR.DOC_DATE_GRE
FROM            INV_SALES_DTL INNER JOIN
                         INV_SALES_HDR ON INV_SALES_DTL.DOC_NO = INV_SALES_HDR.DOC_NO INNER JOIN
                         REC_CUSTOMER ON INV_SALES_HDR.CUSTOMER_CODE = REC_CUSTOMER.CODE
WHERE        (INV_SALES_HDR.FLAGDEL = 1) AND (INV_SALES_HDR.DOC_DATE_GRE BETWEEN @d1 AND @d2)
GROUP BY INV_SALES_DTL.ITEM_TAX_PER, INV_SALES_HDR.SALE_TYPE, INV_SALES_HDR.CUSTOMER_CODE, REC_CUSTOMER.DESC_ENG, INV_SALES_HDR.DOC_ID, 
                         INV_SALES_HDR.DOC_DATE_GRE
HAVING        (INV_SALES_HDR.SALE_TYPE = N'B2C')
ORDER BY INV_SALES_HDR.DOC_ID, INV_SALES_HDR.DOC_DATE_GRE";
                    Dictionary<string, object> parameter = new Dictionary<string, object>();
                    parameter.Add("@d1", StartDate.Value);
                    parameter.Add("@d2", EndDate.Value);
                    dt = DbFunctions.GetDataTable(query, parameter);
                }
                //adptr = new SqlDataAdapter(cmd);
                //adptr.Fill(dt);
                //conn.Close();

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        worksheet2.Cells[indx, 1] = "";
                        worksheet2.Cells[indx, 2] = dt.Rows[i]["CUSTOMER"].ToString();
                        worksheet2.Cells[indx, 3] = dt.Rows[i]["DOC_ID"].ToString();
                        worksheet2.Cells[indx, 4] = dt.Rows[i]["DOC_DATE_GRE"].ToString();
                        worksheet2.Cells[indx, 5] = dt.Rows[i]["ITEM_TOTAL"].ToString();
                        worksheet2.Cells[indx, 6] = dt.Rows[i]["ITEM_TAX_PER"].ToString();
                        worksheet2.Cells[indx, 7] = dt.Rows[i]["GROSS_TOTAL"].ToString();
                        worksheet2.Cells[indx, 8] = "0.00";
                        worksheet2.Cells[indx, 9] = (Convert.ToDecimal(dt.Rows[i]["ITEM_TAX"]) / 2).ToString();
                        worksheet2.Cells[indx, 10] = (Convert.ToDecimal(dt.Rows[i]["ITEM_TAX"]) / 2).ToString();
                        indx++;
                    }
                }

                dt = new DataTable();
                //if (conn.State == ConnectionState.Closed)
                //    conn.Open();
                if (Chk.Checked)
                {

                    query = @"SELECT  SUM(INV_SALES_DTL.ITEM_TAX) AS ITEM_TAX, SUM(INV_SALES_DTL.GROSS_TOTAL) AS GROSS_TOTAL, SUM(INV_SALES_DTL.ITEM_TOTAL) AS ITEM_TOTAL FROM            INV_SALES_DTL INNER JOIN
                         INV_SALES_HDR ON INV_SALES_DTL.DOC_NO = INV_SALES_HDR.DOC_NO INNER JOIN
                         REC_CUSTOMER ON INV_SALES_HDR.CUSTOMER_CODE = REC_CUSTOMER.CODE
WHERE        (INV_SALES_HDR.FLAGDEL = 1) AND (INV_SALES_HDR.DOC_DATE_GRE BETWEEN @d1 AND @d2) AND  (INV_SALES_HDR.SALE_TYPE = N'B2C')";
                    Dictionary<string, object> parameter = new Dictionary<string, object>();
                    parameter.Add("@d1", StartDate.Value);
                    parameter.Add("@d2", EndDate.Value);
                    dt = DbFunctions.GetDataTable(query, parameter);

                }

               
                //adptr = new SqlDataAdapter(cmd);
                //adptr.Fill(dt);
                //conn.Close();
                if (dt.Rows.Count > 0)
                {

                    worksheet2.Cells[indx, 3] = "Total";
                    worksheet2.Cells[indx, 5] = dt.Rows[0]["ITEM_TOTAL"].ToString();
                    worksheet2.Cells[indx, 7] = dt.Rows[0]["GROSS_TOTAL"].ToString();
                    worksheet2.Cells[indx, 8] ="0.00";
                    worksheet2.Cells[indx, 9] = (Convert.ToDecimal(dt.Rows[0]["ITEM_TAX"]) / 2).ToString();
                    worksheet2.Cells[indx, 10] = (Convert.ToDecimal(dt.Rows[0]["ITEM_TAX"]) / 2).ToString();
                    worksheet2.Cells[indx, 1].EntireRow.Font.Bold = true;
                }







                ////B2C
                //indx = 0;
                //Microsoft.Office.Interop.Excel._Worksheet worksheet2 = workbook.Sheets.Add(Type.Missing, Type.Missing, 1, Type.Missing);
                //worksheet2.Name = "B2C";
                //dt = new DataTable();
                //if (conn.State == ConnectionState.Closed)
                //    conn.Open();
                //cmd = new SqlCommand("SELECT * FROM tbl_companysetup", conn);
                //adptr = new SqlDataAdapter(cmd);
                //adptr.Fill(dt);
                //conn.Close();
                //report = "";

                ////heading
                //worksheet2.Cells[1, 3] = "       " + dt.Rows[0]["company_name"].ToString();
                //worksheet2.Cells[2, 3] = dt.Rows[0]["address"].ToString();
                //worksheet2.Cells[3, 3] = "B2C(Other than Reverse Charge & E-Commerce Operator)";
                //worksheet2.Cells[5, 1] = "Rate%";
                //worksheet2.Cells[5, 2] = "Total Taxable value";
                //worksheet2.Cells[5, 3] = "Integrated Tax";
                //worksheet2.Cells[5, 4] = "Central Tax";
                //worksheet2.Cells[5, 5] = "State Tax";

                //worksheet2.Cells[5, 1].EntireRow.Font.Bold = true;
                //indx = 6;

                //dt = new DataTable();
                //if (conn.State == ConnectionState.Closed)
                //    conn.Open();
                //if (Chk.Checked)
                //{
                //    cmd = new SqlCommand("SELECT ITEM_TAX_PER as 'Rate%',SUM(TAX) AS TAX,SUM(IGST) AS IGST,SUM(CGST) AS CGST,SUM(SGST) AS SGST,SUM(GROSS_TOTAL) AS GROSS,SUM(ITEM_TOTAL) AS TOTAL FROM (SELECT ITEM_TAX_PER,[STATE] AS 'CUSTOMER_STATE',CASE WHEN REC_CUSTOMER.[STATE]!='KL' THEN SUM(ITEM_TAX) ELSE 0 END AS 'IGST',CASE WHEN REC_CUSTOMER.[STATE]='KL' THEN SUM(ITEM_TAX)/2 ELSE 0 END AS 'CGST',CASE WHEN REC_CUSTOMER.[STATE]='KL' THEN SUM(ITEM_TAX)/2 ELSE 0 END AS 'SGST',SUM(ITEM_TAX) AS TAX,SUM(GROSS_TOTAL) AS GROSS_TOTAL,SUM(ITEM_TOTAL) AS ITEM_TOTAL FROM INV_SALES_DTL LEFT OUTER JOIN (SELECT DOC_ID,CUSTOMER_CODE,DATED FROM INV_SALES_HDR)AS INV_SALES_HDR ON INV_SALES_DTL.DOC_NO=INV_SALES_HDR.DOC_NO INNER JOIN (SELECT CODE,DESC_ENG,[STATE] FROM REC_CUSTOMER WHERE TIN_NO='') AS REC_CUSTOMER ON REC_CUSTOMER.CODE=INV_SALES_HDR.CUSTOMER_CODE WHERE INV_SALES_HDR.DOC_DATE_GRE BETWEEN @d1 AND @d2 GROUP BY ITEM_TAX_PER,[STATE]) AS Table1 GROUP BY ITEM_TAX_PER", conn);
                //    cmd.Parameters.Add("@d1", SqlDbType.Date).Value = StartDate.Value;
                //    cmd.Parameters.Add("@d2", SqlDbType.Date).Value = EndDate.Value;

                //}
                //else
                //    cmd = new SqlCommand("SELECT ITEM_TAX_PER as 'Rate%',SUM(TAX) AS TAX,SUM(IGST) AS IGST,SUM(CGST) AS CGST,SUM(SGST) AS SGST,SUM(GROSS_TOTAL) AS GROSS,SUM(ITEM_TOTAL) AS TOTAL FROM (SELECT ITEM_TAX_PER,[STATE] AS 'CUSTOMER_STATE',CASE WHEN REC_CUSTOMER.[STATE]!='KL' THEN SUM(ITEM_TAX) ELSE 0 END AS 'IGST',CASE WHEN REC_CUSTOMER.[STATE]='KL' THEN SUM(ITEM_TAX)/2 ELSE 0 END AS 'CGST',CASE WHEN REC_CUSTOMER.[STATE]='KL' THEN SUM(ITEM_TAX)/2 ELSE 0 END AS 'SGST',SUM(ITEM_TAX) AS TAX,SUM(GROSS_TOTAL) AS GROSS_TOTAL,SUM(ITEM_TOTAL) AS ITEM_TOTAL FROM INV_SALES_DTL LEFT OUTER JOIN (SELECT DOC_ID,CUSTOMER_CODE FROM INV_SALES_HDR)AS INV_SALES_HDR ON INV_SALES_DTL.DOC_ID=INV_SALES_HDR.DOC_ID INNER JOIN (SELECT CODE,DESC_ENG,[STATE] FROM REC_CUSTOMER WHERE TIN_NO='') AS REC_CUSTOMER ON REC_CUSTOMER.CODE=INV_SALES_HDR.CUSTOMER_CODE GROUP BY ITEM_TAX_PER,[STATE]) AS Table1 GROUP BY ITEM_TAX_PER", conn);
                //adptr = new SqlDataAdapter(cmd);
                //adptr.Fill(dt);
                //conn.Close();


                //if (dt.Rows.Count > 0)
                //{
                //    for (int i = 0; i < dt.Rows.Count; i++)
                //    {
                //        worksheet2.Cells[indx, 1] = dt.Rows[i]["Rate%"].ToString();
                //        worksheet2.Cells[indx, 2] = dt.Rows[i]["GROSS"].ToString();
                //        worksheet2.Cells[indx, 3] = dt.Rows[i]["IGST"].ToString();
                //        worksheet2.Cells[indx, 4] = dt.Rows[i]["CGST"].ToString();
                //        worksheet2.Cells[indx, 5] = dt.Rows[i]["SGST"].ToString();

                //        indx++;
                //    }
                //}


                ////B2C TOTAL

                //dt = new DataTable();
                //if (conn.State == ConnectionState.Closed)
                //    conn.Open();
                //if (Chk.Checked)
                //{
                //    cmd = new SqlCommand("SELECT SUM(TAX) AS TAX,SUM(IGST) AS IGST,SUM(CGST) AS CGST,SUM(SGST) AS SGST,SUM(GROSS_TOTAL) AS GROSS,SUM(ITEM_TOTAL) AS TOTAL FROM (SELECT ITEM_TAX_PER,[STATE] AS 'CUSTOMER_STATE',CASE WHEN REC_CUSTOMER.[STATE]!='KL' THEN SUM(ITEM_TAX) ELSE 0 END AS 'IGST',CASE WHEN REC_CUSTOMER.[STATE]='KL' THEN SUM(ITEM_TAX)/2 ELSE 0 END AS 'CGST',CASE WHEN REC_CUSTOMER.[STATE]='KL' THEN SUM(ITEM_TAX)/2 ELSE 0 END AS 'SGST',SUM(ITEM_TAX) AS TAX,SUM(GROSS_TOTAL) AS GROSS_TOTAL,SUM(ITEM_TOTAL) AS ITEM_TOTAL FROM INV_SALES_DTL LEFT OUTER JOIN (SELECT DOC_ID,CUSTOMER_CODE,DATED FROM INV_SALES_HDR)AS INV_SALES_HDR ON INV_SALES_DTL.DOC_ID=INV_SALES_HDR.DOC_ID INNER JOIN (SELECT CODE,DESC_ENG,[STATE] FROM REC_CUSTOMER WHERE TIN_NO='') AS REC_CUSTOMER ON REC_CUSTOMER.CODE=INV_SALES_HDR.CUSTOMER_CODE WHERE INV_SALES_HDR.DOC_DATED_GRE BETWEEN @d1 AND @d2 GROUP BY ITEM_TAX_PER,[STATE]) AS Table1", conn);
                //    cmd.Parameters.Add("@d1", SqlDbType.Date).Value = StartDate.Value;
                //    cmd.Parameters.Add("@d2", SqlDbType.Date).Value = EndDate.Value;

                //}
                //else
                //    cmd = new SqlCommand("SELECT SUM(TAX) AS TAX,SUM(IGST) AS IGST,SUM(CGST) AS CGST,SUM(SGST) AS SGST,SUM(GROSS_TOTAL) AS GROSS,SUM(ITEM_TOTAL) AS TOTAL FROM (SELECT ITEM_TAX_PER,[STATE] AS 'CUSTOMER_STATE',CASE WHEN REC_CUSTOMER.[STATE]!='KL' THEN SUM(ITEM_TAX) ELSE 0 END AS 'IGST',CASE WHEN REC_CUSTOMER.[STATE]='KL' THEN SUM(ITEM_TAX)/2 ELSE 0 END AS 'CGST',CASE WHEN REC_CUSTOMER.[STATE]='KL' THEN SUM(ITEM_TAX)/2 ELSE 0 END AS 'SGST',SUM(ITEM_TAX) AS TAX,SUM(GROSS_TOTAL) AS GROSS_TOTAL,SUM(ITEM_TOTAL) AS ITEM_TOTAL FROM INV_SALES_DTL LEFT OUTER JOIN (SELECT DOC_ID,CUSTOMER_CODE FROM INV_SALES_HDR)AS INV_SALES_HDR ON INV_SALES_DTL.DOC_ID=INV_SALES_HDR.DOC_ID INNER JOIN (SELECT CODE,DESC_ENG,[STATE] FROM REC_CUSTOMER WHERE TIN_NO='') AS REC_CUSTOMER ON REC_CUSTOMER.CODE=INV_SALES_HDR.CUSTOMER_CODE GROUP BY ITEM_TAX_PER,[STATE]) AS Table1 ", conn);
                //adptr = new SqlDataAdapter(cmd);
                //adptr.Fill(dt);
                //conn.Close();
                //if (dt.Rows.Count > 0)
                //{

                //    worksheet2.Cells[indx, 1] = "Total";
                //    worksheet2.Cells[indx, 2] = dt.Rows[0]["GROSS"].ToString();
                //    worksheet2.Cells[indx, 3] = dt.Rows[0]["IGST"].ToString();
                //    worksheet2.Cells[indx, 4] = dt.Rows[0]["CGST"].ToString();
                //    worksheet2.Cells[indx, 5] = dt.Rows[0]["SGST"].ToString();
                //    worksheet2.Cells[indx, 1].EntireRow.Font.Bold = true;
                //}



               









                //HSN WISE
                indx = 0;
                Microsoft.Office.Interop.Excel._Worksheet worksheet3 = workbook.Sheets.Add(Type.Missing, Type.Missing, 1, Type.Missing);
                worksheet3.Name = "HSN wise summary";

                dt = new DataTable();
                //if (conn.State == ConnectionState.Closed)
                //    conn.Open();
                query = "SELECT * FROM tbl_companysetup";
                dt = DbFunctions.GetDataTable(query);
                //adptr = new SqlDataAdapter(cmd);
                //adptr.Fill(dt);
                //conn.Close();
                report = "";
                report = "HSN Wise From " + StartDate.Value.ToShortDateString() + " To " + EndDate.Value.ToShortDateString();

                //heading
                worksheet3.Cells[1, 3] = "       " + dt.Rows[0]["company_name"].ToString();
                worksheet3.Cells[2, 3] = dt.Rows[0]["address"].ToString();
                worksheet3.Cells[2, 3] = dt.Rows[0]["address"].ToString();
                worksheet3.Cells[3, 3] = report;
                //merging
                worksheet3.Range[worksheet3.Cells[1, 2], worksheet3.Cells[1, 4]].Merge();
                worksheet3.Range[worksheet3.Cells[2, 2], worksheet3.Cells[2, 4]].Merge();
                worksheet3.Range[worksheet3.Cells[3, 2], worksheet3.Cells[3, 4]].Merge();

                //font bold
                worksheet3.Cells[1, 2].EntireRow.Font.Bold = true;
                worksheet3.Cells[1, 2].Interior.Color = Color.FromArgb(192, 192, 192);


                //  storing header part in Excel
                for (int i = 0; i < (dataGridView1.Columns.Count); i++)
                {
                    worksheet3.Cells[5, i + 1] = dataGridView1.Columns[i].HeaderText;
                    worksheet3.Cells[5, i + 1].EntireRow.Font.Bold = true;
                    worksheet3.Columns[i + 1].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;

                    // worksheet.Columns[i + 1].ColumnWidth = dataGridView1.Columns[i].Width;
                }



                // storing Each row and column value to excel sheet
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    for (int j = 0; j < (dataGridView1.Columns.Count); j++)
                    {
                        if (j == 1)
                        {
                            worksheet3.Cells[i + 6, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();


                        }
                        else
                        {
                            worksheet3.Cells[i + 6, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                        }
                        if (cmb_report.Text == "Tax Wise Summary")
                        {
                            if (dataGridView1.Rows[i].Cells[0].Value == "")
                            {
                                // worksheet.Cells[i + 6, 2].Interior.Color = Color.FromArgb(232, 218, 239);
                                worksheet3.Cells[i + 6, 2].Font.Color = Color.FromArgb(52, 73, 94);
                                worksheet3.Cells[i + 6, 2].Font.Bold = true;

                            }
                            if (dataGridView1.Rows[i].Cells[7].Value == "TOTAL")
                            {
                                // worksheet.Cells[i + 6, 8].Interior.Color = Color.Blue
                                worksheet3.Cells[i + 6, 8].Font.Color = Color.Blue;
                                worksheet3.Cells[i + 6, 10].Font.Color = Color.Blue;
                                worksheet3.Cells[i + 6, 12].Font.Color = Color.Blue;
                                worksheet3.Cells[i + 6, 14].Font.Color = Color.Blue;
                                worksheet3.Cells[i + 6, 16].Font.Color = Color.Blue;
                                worksheet3.Cells[i + 6, 19].Font.Color = Color.Blue;
                            }
                        }
                    }
                }
                //if (cmb_report.Text == "Salesman Summary" || cmb_report.Text == "Customer Summary" || cmb_report.Text == "Tax Wise Summary")
                //{
                //    Microsoft.Office.Interop.Excel.Range formatRange;
                //    formatRange = worksheet.get_Range("DATE", "DATE");
                //    formatRange.NumberFormat = "mm/dd/yyyy";
                //    //formatRange.NumberFormat = "mm/dd/yyyy hh:mm:ss";
                //    worksheet.Cells[1, 1] = "31/5/2014";
                //}
                worksheet3.Columns.AutoFit();
                worksheet3.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlPortrait;
                worksheet3.PageSetup.Zoom = false;
                worksheet3.PageSetup.FitToPagesTall = 100;
                worksheet3.PageSetup.FitToPagesWide = 1;
                // save the application
                System.Windows.Forms.SaveFileDialog saveDlg = new System.Windows.Forms.SaveFileDialog();
                saveDlg.InitialDirectory = @"D:\";
                saveDlg.Filter = "Excel files (*.xls)|*.xls";
                saveDlg.FilterIndex = 0;
                saveDlg.RestoreDirectory = true;
                saveDlg.Title = "Export Excel File To";
                if (saveDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string path = saveDlg.FileName;
                    workbook.SaveCopyAs(path);
                    workbook.Saved = true;
                    workbook.Close(true, Type.Missing, Type.Missing);
                    app.Quit();
                }
                //  workbook.SaveAs("D:\\output.xls", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                // Exit from the application
                //  app.Quit();

            }
            catch (Exception ex)
            {
                string st = ex.Message;
                //if (conn.State == ConnectionState.Open)
                //{
                //    conn.Close();
                //}
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            btnSave.PerformClick();
            pd.Run(report);
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            try
            {
                Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();


                // creating new WorkBook within Excel application
                Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);


                // creating new Excelsheet in workbook
                Microsoft.Office.Interop.Excel._Worksheet worksheet = null;

                // see the excel sheet behind the program
                app.Visible = true;

                // get the reference of first sheet. By default its name is Sheet1.
                // store its reference to worksheet
                worksheet = workbook.Sheets["Sheet1"];
                worksheet = workbook.ActiveSheet;

                // changing the name of active sheet
                worksheet.Name = "Exported from gridview";



                DataTable dt = new DataTable();
                //if (conn.State == ConnectionState.Closed)
                //    conn.Open();
                string query = "SELECT * FROM tbl_companysetup";
                dt = DbFunctions.GetDataTable(query);
                //SqlDataAdapter adptr = new SqlDataAdapter(cmd);
                //adptr.Fill(dt);
                //conn.Close();
                string report = "";
                if (Chk.Checked)
                {
                    if (cmb_report.Text == "Salesman Summary")
                        report = cmb_report.Text + " for the period from " + StartDate.Value.ToShortDateString() + " to " + EndDate.Value.ToShortDateString() + "(" + cmb_salesman.Text + ")";
                    else
                        report = cmb_report.Text + " for the period from " + StartDate.Value.ToShortDateString() + " to " + EndDate.Value.ToShortDateString();
                }
                else
                {
                    if (cmb_report.Text == "Salesman Summary")
                        report = cmb_report.Text + "(" + cmb_salesman.Text + ")";
                    else
                        report = cmb_report.Text;
                }

                //heading
                worksheet.Cells[1, 3] = "       " + dt.Rows[0]["company_name"].ToString();
                worksheet.Cells[2, 3] = dt.Rows[0]["address"].ToString();
                worksheet.Cells[2, 3] = dt.Rows[0]["address"].ToString();
                worksheet.Cells[3, 3] = report; ;
                //merging
                worksheet.Range[worksheet.Cells[1, 2], worksheet.Cells[1, 4]].Merge();
                worksheet.Range[worksheet.Cells[2, 2], worksheet.Cells[2, 4]].Merge();
                worksheet.Range[worksheet.Cells[3, 2], worksheet.Cells[3, 4]].Merge();

                //font bold
                worksheet.Cells[1, 2].EntireRow.Font.Bold = true;
                worksheet.Cells[1, 2].Interior.Color = Color.FromArgb(192, 192, 192);


                //  storing header part in Excel
                for (int i = 0; i < (dataGridView1.Columns.Count); i++)
                {
                    worksheet.Cells[5, i + 1] = dataGridView1.Columns[i].HeaderText;
                    worksheet.Cells[5, i + 1].EntireRow.Font.Bold = true;
                    worksheet.Columns[i + 1].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;

                    // worksheet.Columns[i + 1].ColumnWidth = dataGridView1.Columns[i].Width;
                }

                // worksheet.Columns[1].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                // storing Each row and column value to excel sheet
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    for (int j = 0; j < (dataGridView1.Columns.Count); j++)
                    {
                        if (j == 1)
                        {
                            worksheet.Cells[i + 6, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();


                        }
                        else
                        {
                            worksheet.Cells[i + 6, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                        }
                        if (cmb_report.Text == "Tax Wise Summary")
                        {
                            if (dataGridView1.Rows[i].Cells[0].Value == "")
                            {
                                worksheet.Cells[i + 6, 2].Interior.Color = Color.FromArgb(232, 218, 239);

                            }

                        }
                    }
                }
                //if (cmb_report.Text == "Salesman Summary" || cmb_report.Text == "Customer Summary" || cmb_report.Text == "Tax Wise Summary")
                //{
                //    Microsoft.Office.Interop.Excel.Range formatRange;
                //    formatRange = worksheet.get_Range("DATE", "DATE");
                //    formatRange.NumberFormat = "mm/dd/yyyy";
                //    //formatRange.NumberFormat = "mm/dd/yyyy hh:mm:ss";
                //    worksheet.Cells[1, 1] = "31/5/2014";
                //}
                worksheet.Columns.AutoFit();
                worksheet.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlPortrait;
                worksheet.PageSetup.Zoom = false;
                worksheet.PageSetup.FitToPagesTall = 100;
                worksheet.PageSetup.FitToPagesWide = 1;
                // save the application
                System.Windows.Forms.SaveFileDialog saveDlg = new System.Windows.Forms.SaveFileDialog();
                saveDlg.InitialDirectory = @"D:\";
                saveDlg.Filter = "Excel files (*.xls)|*.xls";
                saveDlg.FilterIndex = 0;
                saveDlg.RestoreDirectory = true;
                saveDlg.Title = "Export Excel File To";
                if (saveDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string path = saveDlg.FileName;
                    workbook.SaveCopyAs(path);
                    workbook.Saved = true;
                    workbook.Close(true, Type.Missing, Type.Missing);
                    app.Quit();
                }
                //  workbook.SaveAs("D:\\output.xls", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                // Exit from the application
                //  app.Quit();
                
            }
            catch (Exception ex)
            {
                string st = ex.Message;
            }
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (cmb_report.Text == "")
            {
                if (dataGridView1.Rows.Count > 0 && dataGridView1.SelectedRows[0].Cells["Voucher No"].Value.ToString() != "")
                {
                    try
                    {
                        ComponentFactory.Krypton.Navigator.KryptonPage kp = new ComponentFactory.Krypton.Navigator.KryptonPage();
                        mdi.maindocpanel.Pages.Add(kp);
                        string docno = dataGridView1.CurrentRow.Cells["Voucher No"].Value.ToString();
                        SalesQ m = new SalesQ(docno);
                        m.Show();
                        m.BackColor = Color.White;
                        m.TopLevel = false;
                        kp.Controls.Add(m);
                        m.Dock = DockStyle.Fill;
                        kp.Text = m.Text;
                        kp.Name = "Sales";
                        m.FormBorderStyle = FormBorderStyle.None;
                        //kp.Focus();
                        mdi.maindocpanel.SelectedPage = kp;
                        // m.Focus();

                    }
                    catch (Exception ex)
                    {
                    }

                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if ("Item Summary"==cmb_report.Text)
            {
                if ( e.ColumnIndex == 2 || e.ColumnIndex == 3||e.ColumnIndex == 4 || e.ColumnIndex == 5)
                {
                    e.CellStyle.Format = "N4";
                }
            }
        }
    }
}
