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

namespace Sys_Sols_Inventory
{
    public partial class Purchase_RPT_HDR : Form
    {
        private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        private SqlCommand cmd = new SqlCommand();
        private SqlDataAdapter adapter = new SqlDataAdapter();
        private DataTable table = new DataTable();
        Initial mdi = (Initial)Application.OpenForms["Initial"];
        Login lg = (Login)Application.OpenForms["Login"];
        private bool HasType = true;
        private bool HasGroup = true;
        private bool HasCategory = true;
        private bool HasTM = true;
        public string decpoint = "2";
        Class.Stock_Report stkrpt = new Class.Stock_Report();
        DataTable dt = new DataTable();
        public Purchase_RPT_HDR()
        {
            InitializeComponent();
            cmd.Connection = conn;
            adapter.SelectCommand = cmd;
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
        public void BindSupplier()
        {
            try
            {
                dt = stkrpt.BindSupplier();

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

        public void BindSalesType()
        {
            try
            {
                dt = stkrpt.BindSalesTypes();

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

        public void GetItemName()
        {
            try
            {
                //DataTable dt = new DataTable();
                //conn.Open();
                //cmd.CommandText = "select CODE,DESC_ENG FROM INV__ITM_DIRECTORY";
                //cmd.CommandType = CommandType.Text;
                //adapter.Fill(dt);
                DataTable dt= stkrpt.Bind_item_name();
                DataRow row = dt.NewRow();
                row[0] = "";
                dt.Rows.InsertAt(row, 0);
                cmb_item.DataSource = dt;


                cmb_item.ValueMember = "CODE";
                cmb_item.DisplayMember = "DESC_ENG";
                conn.Close();
            }
            catch(SqlException e)
            {
               // conn.Close();
                MessageBox.Show(e.Message);
            }
        }
        private void Purchase_RPT_HDR_Load(object sender, EventArgs e)
        {

            BindSettings();
            BindCategory();
            BindGroup();
            BindTradeMark();
            BindType();
            BindSupplier();
            BindSalesType();
            GetItemName();
            btnSave.PerformClick();
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

        private void Send_Mail_Click(object sender, EventArgs e)
        {
          //  SavePDF(reportViewer1, Path.GetTempPath() + "PurchaseRport_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xlsx");
        }

        //public void SavePDF(DataGrid viewer, string savePath)
        //{
        //    try
        //    {
        //        //  byte[] Bytes = viewer.LocalReport.Render(format:"WORDOPENXML", deviceInfo: "");
        //        byte[] Bytes = viewer.LocalReport.Render(format: "EXCELOPENXML", deviceInfo: "");
        //        // byte[] Bytes = viewer.LocalReport.Render(format: "PDF", deviceInfo: "");
        //        using (FileStream stream = new FileStream(savePath, FileMode.Create))
        //        {
        //            stream.Write(Bytes, 0, Bytes.Length);

        //        }
        //    }
        //    catch { }

        //    try
        //    {
        //        if (DialogResult.Yes == MessageBox.Show("Make sure you have configued outlook for sending email", "OutLook Config", MessageBoxButtons.YesNo))
        //        {
        //            Microsoft.Office.Interop.Outlook.Application oApp = new Microsoft.Office.Interop.Outlook.Application();
        //            Microsoft.Office.Interop.Outlook._MailItem oMailItem = (Microsoft.Office.Interop.Outlook._MailItem)oApp.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);

        //            // body, bcc etc...

        //            oMailItem.Attachments.Add((object)savePath, Microsoft.Office.Interop.Outlook.OlAttachmentType.olEmbeddeditem, 1, (object)"Attachment");
        //            oMailItem.Display(true);
        //        }
        //    }
        //    catch
        //    {
        //        MessageBox.Show("You may not have Outlook Installed Please check");
        //    }
        //}

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!chDet.Checked)
            {
                tb_Summary.SelectedTab = tp_summary;
                try
                {
                    DataTable dt = new DataTable();
                    // conn.Open();
                    if (Chk.Checked)
                    {
                        stkrpt.docType = Convert.ToString(Cbx_salestype.SelectedValue);
                        stkrpt.supplier = Convert.ToString(Cbx_supplier.SelectedValue);
                        stkrpt.startDate = StartDate.Value;
                        stkrpt.endDate = EndDate.Value;
                        stkrpt.itemName = cmb_item.Text;
                        if (cmb_item.Text != "")
                        {
                            // cmd.CommandText = "SELECT    distinct    INV_PURCHASE_HDR.DOC_NO,CASE WHEN INV_PURCHASE_HDR.DOC_TYPE='PUR.CSS' THEN 'CASH PURCHASE' WHEN INV_PURCHASE_HDR.DOC_TYPE='PUR.CRD' THEN 'CREDIT PURCHASE' WHEN INV_PURCHASE_HDR.DOC_TYPE='LGR.PRT' THEN 'PURCHASE RETURN' ELSE '' END as Type, INV_PURCHASE_HDR.DOC_DATE_GRE as Date, PAY_SUPPLIER.DESC_ENG as Supplier, INV_PURCHASE_HDR.TAX_TOTAL as 'Tax Total', INV_PURCHASE_HDR.FREIGHT_AMT as Freight, INV_PURCHASE_HDR.GROSS as 'Gross Amount', INV_PURCHASE_HDR.DISCOUNT_VAL as Discount, INV_PURCHASE_HDR.NET_VAL as 'Net Value',  INV_PURCHASE_DTL.ITEM_CODE as 'Item code', INV_PURCHASE_DTL.ITEM_DESC_ENG as 'Item Name'  FROM            INV_PURCHASE_HDR INNER JOIN INV_PURCHASE_DTL ON INV_PURCHASE_HDR.DOC_NO = INV_PURCHASE_DTL.DOC_NO LEFT OUTER JOIN PAY_SUPPLIER ON INV_PURCHASE_HDR.SUPPLIER_CODE = PAY_SUPPLIER.CODE WHERE        (INV_PURCHASE_HDR.DOC_TYPE LIKE '%' + @DOC_TYPE + '%') AND (INV_PURCHASE_HDR.SUPPLIER_CODE LIKE N'%' + @SUP_CODE + N'%') AND (INV_PURCHASE_HDR.DOC_DATE_GRE BETWEEN @Date1 AND @Date2) AND (INV_PURCHASE_DTL.ITEM_DESC_ENG LIKE '%' + @Name + '%')and (INV_PURCHASE_HDR.FLAGDEL=1)and (INV_PURCHASE_HDR.DOC_TYPE IN('PUR.CSS','LGR.CRD'))";
                            dt = stkrpt.dateWasePurchaseReportWithItem();
                        }
                        else
                        {
                            // cmd.CommandText = "SELECT distinct INV_PURCHASE_HDR.DOC_NO,CASE WHEN INV_PURCHASE_HDR.DOC_TYPE='PUR.CSS' THEN 'CASH PURCHASE' WHEN INV_PURCHASE_HDR.DOC_TYPE='PUR.CRD' THEN 'CREDIT PURCHASE' WHEN INV_PURCHASE_HDR.DOC_TYPE='LGR.PRT' THEN 'PURCHASE RETURN' ELSE '' END as Type, INV_PURCHASE_HDR.DOC_DATE_GRE as Date, PAY_SUPPLIER.DESC_ENG as Supplier, INV_PURCHASE_HDR.TAX_TOTAL as 'Tax Total', INV_PURCHASE_HDR.FREIGHT_AMT as Freight, INV_PURCHASE_HDR.GROSS as 'Gross Amount', INV_PURCHASE_HDR.DISCOUNT_VAL as Discount, INV_PURCHASE_HDR.NET_VAL as 'Net Value' FROM            INV_PURCHASE_HDR LEFT OUTER JOIN    PAY_SUPPLIER ON INV_PURCHASE_HDR.SUPPLIER_CODE = PAY_SUPPLIER.CODE WHERE        (INV_PURCHASE_HDR.DOC_TYPE LIKE '%' + @DOC_TYPE + '%') AND (INV_PURCHASE_HDR.SUPPLIER_CODE LIKE N'%' + @SUP_CODE + N'%') AND  (INV_PURCHASE_HDR.DOC_DATE_GRE BETWEEN @Date1 AND @Date2)and(INV_PURCHASE_HDR.FLAGDEL=1)and (INV_PURCHASE_HDR.DOC_TYPE IN('PUR.CSS','LGR.CRD')";
                            dt = stkrpt.dateWasePurchaseReportWithOutItem();
                        }
                    }
                    else
                    {
                        stkrpt.docType = Convert.ToString(Cbx_salestype.SelectedValue);
                        stkrpt.supplier = Convert.ToString(Cbx_supplier.SelectedValue);
                        stkrpt.itemName = cmb_item.Text;
                        if (cmb_item.Text != "")
                        {
                            //   // cmd.CommandText = "SELECT        INV_PURCHASE_HDR.DOC_NO,CASE WHEN INV_PURCHASE_HDR.DOC_TYPE='PUR.CSS' THEN 'CASH PURCHASE'  WHEN INV_PURCHASE_HDR.DOC_TYPE='PUR.CRD' THEN 'CREDIT PURCHASE'  WHEN INV_PURCHASE_HDR.DOC_TYPE='LGR.PRT' THEN 'PURCHASE RETURN' ELSE '' END as Type, INV_PURCHASE_HDR.DOC_DATE_GRE as Date, PAY_SUPPLIER.DESC_ENG as Supplier, INV_PURCHASE_DTL.ITEM_CODE as 'Item Code', INV_PURCHASE_DTL.ITEM_DESC_ENG as 'Item Name', INV_PURCHASE_HDR.TAX_TOTAL as 'Tax Amt', INV_PURCHASE_HDR.FREIGHT_AMT as 'Freight', INV_PURCHASE_HDR.GROSS as 'Gross Amount', INV_PURCHASE_HDR.DISCOUNT_VAL as Discount,INV_PURCHASE_HDR.NET_VAL as 'Net Value' FROM            INV_PURCHASE_HDR INNER JOIN INV_PURCHASE_DTL ON INV_PURCHASE_HDR.DOC_NO = INV_PURCHASE_DTL.DOC_NO LEFT OUTER JOIN  PAY_SUPPLIER ON INV_PURCHASE_HDR.SUPPLIER_CODE = PAY_SUPPLIER.CODE WHERE        (INV_PURCHASE_HDR.DOC_TYPE LIKE '%' + @DOC_TYPE + '%') AND (INV_PURCHASE_HDR.SUPPLIER_CODE LIKE N'%' + @SUP_CODE + N'%') AND  (INV_PURCHASE_DTL.ITEM_DESC_ENG LIKE N'%' + @Name + N'%')and (INV_PURCHASE_HDR.FLAGDEL=1)and (INV_PURCHASE_HDR.DOC_TYPE IN('PUR.CSS','LGR.CRD'))";
                            dt = stkrpt.PurchaseReportWithItem();
                        }
                        else
                        {
                            // cmd.CommandText = "SELECT  INV_PURCHASE_HDR.DOC_NO,CASE WHEN INV_PURCHASE_HDR.DOC_TYPE='PUR.CSS' THEN 'CASH PURCHASE' WHEN INV_PURCHASE_HDR.DOC_TYPE='PUR.CRD' THEN 'CREDIT PURCHASE' WHEN INV_PURCHASE_HDR.DOC_TYPE='LGR.PRT' THEN 'PURCHASE RETURN' ELSE '' END as Type, INV_PURCHASE_HDR.DOC_DATE_GRE as Date, PAY_SUPPLIER.DESC_ENG as Supplier, INV_PURCHASE_HDR.TAX_TOTAL as 'Tax Total', INV_PURCHASE_HDR.FREIGHT_AMT as Freight, INV_PURCHASE_HDR.GROSS as 'Gross Amount', INV_PURCHASE_HDR.DISCOUNT_VAL as Discount,INV_PURCHASE_HDR.NET_VAL as 'Net Value' FROM            INV_PURCHASE_HDR LEFT OUTER JOIN  PAY_SUPPLIER ON INV_PURCHASE_HDR.SUPPLIER_CODE = PAY_SUPPLIER.CODE WHERE (INV_PURCHASE_HDR.DOC_TYPE LIKE '%' + @DOC_TYPE + '%') AND (INV_PURCHASE_HDR.SUPPLIER_CODE LIKE N'%' + @SUP_CODE + N'%')and (INV_PURCHASE_HDR.FLAGDEL=1)and (INV_PURCHASE_HDR.DOC_TYPE IN('PUR.CSS','LGR.CRD'))";
                            dt = stkrpt.PurchaseReportWithOutItem();
                        }

                    }

                    try
                    {
                        //double TotalPurchase = 0, Discount = 0;
                        //for (int i = 0; i < dt.Rows.Count; i++)
                        //{
                        //    TotalPurchase = TotalPurchase + Convert.ToDouble(dt.Rows[i]["Net Value"].ToString());
                        //    Discount = Discount + Convert.ToDouble(dt.Rows[i]["Discount"].ToString());

                        //}
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

                        DataRow newRow5 = dt.NewRow();
                        //  newRow5["Freight"] = "Total :";
                        newRow5["Net Value"] = netamount;
                        newRow5["Discount"] = discount;
                        newRow5["Gross Amount"] = grosstotal;
                        newRow5["Freight"] = freight;
                        newRow5["Tax Total"] = taxamount;

                        dt.Rows.Add(newRow5);

                        DG_GRIDVIEW.DataSource = dt;
                        DG_GRIDVIEW.Columns["Net Value"].DefaultCellStyle.Format = "N" + decpoint;
                        DG_GRIDVIEW.Columns["Tax Total"].DefaultCellStyle.Format = "N" + decpoint;
                        DG_GRIDVIEW.Columns["Freight"].DefaultCellStyle.Format = "N" + decpoint;
                        DG_GRIDVIEW.Columns["Gross Amount"].DefaultCellStyle.Format = "N" + decpoint;
                        DG_GRIDVIEW.Columns["Discount"].DefaultCellStyle.Format = "N" + decpoint;
                        DG_GRIDVIEW.Columns["Net Value"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        DG_GRIDVIEW.Columns["Tax Total"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        DG_GRIDVIEW.Columns["Freight"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        DG_GRIDVIEW.Columns["Gross Amount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        DG_GRIDVIEW.Columns["Discount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        DG_GRIDVIEW.Rows[DG_GRIDVIEW.Rows.Count - 1].DefaultCellStyle.ForeColor = System.Drawing.Color.Red;
                        DG_GRIDVIEW.FirstDisplayedScrollingRowIndex = DG_GRIDVIEW.RowCount - 1;

                        DG_GRIDVIEW.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                        DG_GRIDVIEW.Columns["Freight"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
                        DG_GRIDVIEW.Columns["Tax Total"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                        DG_GRIDVIEW.Columns["Discount"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
                        DG_GRIDVIEW.Columns["Gross Amount"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
                        DG_GRIDVIEW.Columns["Net Value"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
                        DG_GRIDVIEW.Columns["Supplier"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;



                    }
                    catch (Exception ee)
                    {
                        MessageBox.Show(ee.Message);
                    }
                    
                        DG_GRIDVIEW.Rows[DG_GRIDVIEW.Rows.Count - 1].DefaultCellStyle.Font = new Font(DG_GRIDVIEW.Rows[DG_GRIDVIEW.Rows.Count - 1].Cells["Net Value"].InheritedStyle.Font, FontStyle.Bold);
                    Font f1 = new Font("Verdana", 8, FontStyle.Bold);
                    DG_GRIDVIEW.Columns["Net Value"].DefaultCellStyle.Font =f1;
                    //DG_GRIDVIEW.Columns["Type"].Width = 150;

                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                    conn.Close();
                }
            }
            else
            {
                tb_Summary.SelectedTab = tp_detail;
                //string conection = Properties.Settings.Default.ConnectionStrings.ToString();
                DataTable1TableAdapter.Connection = Model.DbFunctions.GetConnection();
                if (Chk.Checked == false)
                {
                    ReportParameter[] parameters = new ReportParameter[5];
                    parameters[0] = new ReportParameter("Date", "false");
                    if (HasType)
                        parameters[1] = new ReportParameter("Type", "True");
                    else
                        parameters[1] = new ReportParameter("Type", "True");//false
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
                    if (Cbx_supplier.Text == "")
                    {
                        string typ = "", grp = "", cat = "", tm = "", doc = "", Billtype = "", item = "";
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
                        if (cmbVoucher.SelectedIndex > 0)
                            Billtype = cmbVoucher.SelectedValue.ToString();
                        if (cmb_item.SelectedIndex > 0)
                            item = cmb_item.SelectedValue.ToString();

                        this.DataTable1TableAdapter.Fill(this.NewPurchase.DataTable1, grp, typ, cat, tm, doc, Billtype, item);
                        //  dataGridView1.DataSource = this.NewPurchase.DataTable1;
                        this.reportViewer1.RefreshReport();
                    }
                    else
                    {
                        string typ = "", grp = "", cat = "", tm = "", doc = "", sup = "", Billtype = "", item = "";
                        sup = Cbx_supplier.SelectedValue.ToString();
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
                        if (cmbVoucher.SelectedIndex > 0)
                            Billtype = cmbVoucher.SelectedValue.ToString();
                        if (cmb_item.SelectedIndex > 0)
                            item = cmb_item.SelectedValue.ToString();
                        this.DataTable1TableAdapter.FillByCust(this.NewPurchase.DataTable1, sup, doc, grp, typ, cat, tm, Billtype, item);
                        this.reportViewer1.RefreshReport();

                    }
                }
                else
                {
                    ReportParameter[] parameters = new ReportParameter[5];
                    parameters[0] = new ReportParameter("Date", "True");
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
                    if (Cbx_supplier.Text == "")
                    {
                        DateTime datestart, dateend;
                        string typ = "", grp = "", cat = "", tm = "", doc = "", Billtype = "", item = "";
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
                        if (cmbVoucher.SelectedIndex > 0)
                            Billtype = cmbVoucher.SelectedValue.ToString();
                        if (cmb_item.SelectedIndex > 0)
                            item = cmb_item.SelectedValue.ToString();
                        datestart = Convert.ToDateTime(StartDate.Value.ToShortDateString());
                        dateend = Convert.ToDateTime(EndDate.Value.ToShortDateString());
                        // this.DataTable1TableAdapter.FillByDate(this.NewPurchase.DataTable1, doc, grp, typ, cat, tm,datestart,dateend);
                        this.DataTable1TableAdapter.FillByDate(this.NewPurchase.DataTable1, datestart, dateend, Billtype, item);
                        this.reportViewer1.RefreshReport();
                    }
                    else
                    {
                        DateTime datestart, dateend;
                        string typ = "", grp = "", cat = "", tm = "", doc = "", sup = "", Billtype = "", item = "";
                        sup = Cbx_supplier.SelectedValue.ToString();
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
                        if (cmbVoucher.SelectedIndex > 0)
                            Billtype = cmbVoucher.SelectedValue.ToString();
                        if (cmb_item.SelectedIndex > 0)
                            item = cmb_item.SelectedValue.ToString();
                        datestart = Convert.ToDateTime(StartDate.Value.ToShortDateString());
                        dateend = Convert.ToDateTime(EndDate.Value.ToShortDateString());

                        this.DataTable1TableAdapter.FillBydateCus(this.NewPurchase.DataTable1, sup, datestart, dateend, Billtype, item);
                        this.reportViewer1.RefreshReport();
                    }

                }
            }
            
        }
        public void BindSettings()
        {
            try
            {
                DataTable dt = stkrpt.getCompanyDetails();
                //conn.Open();
                //cmd.Connection = conn;
                //cmd.CommandType = CommandType.Text;

                //cmd.CommandText = "SELECT * FROM SYS_SETUP";
                //adapter.SelectCommand = cmd;
                //adapter.Fill(dt);


                if (dt.Rows.Count > 0)
                {
                    decpoint = Convert.ToString(dt.Rows[0]["Dec_qty"]);
                    conn.Close();
                }
            }
            catch (Exception ee)
            {
                conn.Close();
                MessageBox.Show(ee.Message);
            }
        }
        private void Chk_CheckedChanged_1(object sender, EventArgs e)
        {
            StartDate.Enabled = Chk.Checked;
            EndDate.Enabled = Chk.Checked;
        }

        private void DG_GRIDVIEW_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DG_GRIDVIEW.Rows.Count > 0)
            {
                try
                {
                ComponentFactory.Krypton.Navigator.KryptonPage kp = new ComponentFactory.Krypton.Navigator.KryptonPage();
                mdi.maindocpanel.Pages.Add(kp);
                string docno=DG_GRIDVIEW.CurrentRow.Cells["DOC_NO"].Value.ToString();
                PurchaseMaster m = new PurchaseMaster(docno);
                m.Show();
                m.BackColor = Color.White;
                m.TopLevel = false;
                kp.Controls.Add(m);
                m.Dock = DockStyle.Fill;
                kp.Text = m.Text;
                kp.Name = "PurchaseMaster";
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

        private void btnExcel_Click(object sender, EventArgs e)
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


                // storing header part in Excel
                for (int i = 1; i < DG_GRIDVIEW.Columns.Count + 1; i++)
                {
                    worksheet.Cells[1, i] = DG_GRIDVIEW.Columns[i - 1].HeaderText;
                }



                // storing Each row and column value to excel sheet
                for (int i = 0; i < DG_GRIDVIEW.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < DG_GRIDVIEW.Columns.Count; j++)
                    {
                        worksheet.Cells[i + 2, j + 1] = DG_GRIDVIEW.Rows[i].Cells[j].Value.ToString();
                    }
                }


                // save the application
                workbook.SaveAs("c:\\output.xls", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                // Exit from the application
                app.Quit();

            }
            catch
            {

            }
        }

        private void btnDetailed_Click(object sender, EventArgs e)
        {
            try
            {
                ComponentFactory.Krypton.Navigator.KryptonPage kp = new ComponentFactory.Krypton.Navigator.KryptonPage();
                mdi.maindocpanel.Pages.Add(kp);
               
                reports.PurchaseReportNew m = new reports.PurchaseReportNew();
                m.Show();
                m.BackColor = Color.White;
                m.TopLevel = false;
                kp.Controls.Add(m);
                m.Dock = DockStyle.Fill;
                kp.Text = m.Text;
                kp.Name = "Purchase Report Detailed";
                m.FormBorderStyle = FormBorderStyle.None;
                //kp.Focus();
                mdi.maindocpanel.SelectedPage = kp;
                // m.Focus();

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnTax_Click(object sender, EventArgs e)
        {

        }

        private void btn_Tax_Click(object sender, EventArgs e)
        {
            reports.Purchase_tax_report ptr = new reports.Purchase_tax_report();
            ptr.ShowDialog();
        }

      
    }
}
