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
    public partial class Currency_Report : Form
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
        Class.Stock_Report stkrpt = new Class.Stock_Report();
      
        DataTable dtb = new DataTable();
        public Currency_Report()
        {
            InitializeComponent();
            cmd.Connection = conn;
            adapter.SelectCommand = cmd;
            HasType = General.IsEnabled(Settings.HasType);
            HasCategory = General.IsEnabled(Settings.HasCategory);
            HasGroup = General.IsEnabled(Settings.HasGroup);
            HasTM = General.IsEnabled(Settings.HasTM);
        }

        private void btnCurr_Click(object sender, EventArgs e)
        {
            try
            {
                CurrencyHelp h = new CurrencyHelp();
                if (h.ShowDialog() == DialogResult.OK && h.c != null)
                {
                    CURCODE.Text = Convert.ToString(h.c["CODE"].Value);
                }

            }
            catch
            {
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                if (ItemName.Text != "")
                {
                    cmd.CommandText = "SELECT        INV_PURCHASE_HDR.DOC_NO, INV_PURCHASE_HDR.DOC_TYPE AS Type, INV_PURCHASE_HDR.DOC_DATE_GRE as Date, INV_PURCHASE_HDR.DOC_DATE_HIJ, PAY_SUPPLIER.DESC_ENG as Supplier, INV_PURCHASE_DTL.ITEM_CODE as 'Item Code', INV_PURCHASE_DTL.ITEM_DESC_ENG as 'Item Name', INV_PURCHASE_HDR.TAX_TOTAL as 'Tax Amt', INV_PURCHASE_HDR.FREIGHT_AMT as 'Freight', INV_PURCHASE_HDR.GROSS as 'Gross Amount', INV_PURCHASE_HDR.DISCOUNT_VAL as Discount,INV_PURCHASE_HDR.NET_VAL as 'Net Value' FROM            INV_PURCHASE_HDR INNER JOIN INV_PURCHASE_DTL ON INV_PURCHASE_HDR.DOC_NO = INV_PURCHASE_DTL.DOC_NO LEFT OUTER JOIN  PAY_SUPPLIER ON INV_PURCHASE_HDR.SUPPLIER_CODE = PAY_SUPPLIER.CODE WHERE        (INV_PURCHASE_HDR.DOC_TYPE LIKE '%' + @DOC_TYPE + '%') AND (INV_PURCHASE_HDR.SUPPLIER_CODE LIKE N'%' + @SUP_CODE + N'%') AND  (INV_PURCHASE_DTL.ITEM_DESC_ENG LIKE N'%' + @Name + N'%')";
                }
                if (CURCODE.Text != "")
                {
               
                   // cmd.CommandText = "SELECT  INV_PURCHASE_HDR.DOC_NO, INV_PURCHASE_HDR.DOC_TYPE as Type, INV_PURCHASE_HDR.DOC_DATE_GRE as Date, tbl_curRepor.Currency_amt,tbl_curRepor.Currency_code, tbl_curRepor.Invoice_no, PAY_SUPPLIER.DESC_ENG as Supplier, (INV_PURCHASE_HDR.TAX_TOTAL)*tbl_curRepor.Currency_amt as 'Tax Total', INV_PURCHASE_HDR.FREIGHT_AMT as Freight, INV_PURCHASE_HDR.GROSS*tbl_curRepor.Currency_amt as 'Gross Amount', INV_PURCHASE_HDR.DISCOUNT_VAL*tbl_curRepor.Currency_amt as Discount,INV_PURCHASE_HDR.NET_VAL*tbl_curRepor.Currency_amt as 'Net Value' FROM            INV_PURCHASE_HDR INNER JOIN tbl_CurRepor ON  INV_PURCHASE_HDR.DOC_ID=tbl_CurRepor.Invoice_no LEFT OUTER JOIN   PAY_SUPPLIER  ON INV_PURCHASE_HDR.SUPPLIER_CODE = PAY_SUPPLIER.CODE WHERE (INV_PURCHASE_HDR.DOC_TYPE LIKE '%' + @DOC_TYPE + '%') AND (INV_PURCHASE_HDR.SUPPLIER_CODE LIKE N'%' +  @SUP_CODE + N'%')AND(tbl_curRepor.Currency_code=@code)";
                    cmd.CommandText = "SELECT   tbl_curRepor.Invoice_no,INV_PURCHASE_HDR.DOC_NO, INV_PURCHASE_HDR.DOC_DATE_GRE as Date, tbl_curRepor.Currency_amt as ExcangeRate ,tbl_curRepor.Currency_code as Currency, PAY_SUPPLIER.DESC_ENG as Supplier, (INV_PURCHASE_HDR.TAX_TOTAL)*tbl_curRepor.Currency_amt as 'Tax Total', INV_PURCHASE_HDR.FREIGHT_AMT as Freight, INV_PURCHASE_HDR.GROSS*tbl_curRepor.Currency_amt as 'Gross Amount', INV_PURCHASE_HDR.DISCOUNT_VAL*tbl_curRepor.Currency_amt as Discount,INV_PURCHASE_HDR.NET_VAL*tbl_curRepor.Currency_amt as 'Net Value' FROM            INV_PURCHASE_HDR INNER JOIN tbl_CurRepor ON  INV_PURCHASE_HDR.DOC_ID=tbl_CurRepor.Invoice_no LEFT OUTER JOIN   PAY_SUPPLIER  ON INV_PURCHASE_HDR.SUPPLIER_CODE = PAY_SUPPLIER.CODE WHERE (INV_PURCHASE_HDR.DOC_TYPE LIKE '%' +  @DOC_TYPE + '%') AND (INV_PURCHASE_HDR.SUPPLIER_CODE LIKE N'%' + @SUP_CODE + N'%')AND(tbl_curRepor.Currency_code=@code)";
                }
                else
                {

                    cmd.CommandText = "SELECT   tbl_curRepor.Invoice_no,INV_PURCHASE_HDR.DOC_NO, INV_PURCHASE_HDR.DOC_DATE_GRE as Date, tbl_curRepor.Currency_amt as ExcangeRate ,tbl_curRepor.Currency_code as Currency, PAY_SUPPLIER.DESC_ENG as Supplier, (INV_PURCHASE_HDR.TAX_TOTAL)*tbl_curRepor.Currency_amt as 'Tax Total', INV_PURCHASE_HDR.FREIGHT_AMT as Freight, INV_PURCHASE_HDR.GROSS*tbl_curRepor.Currency_amt as 'Gross Amount', INV_PURCHASE_HDR.DISCOUNT_VAL*tbl_curRepor.Currency_amt as Discount,INV_PURCHASE_HDR.NET_VAL*tbl_curRepor.Currency_amt as 'Net Value' FROM            INV_PURCHASE_HDR INNER JOIN tbl_CurRepor ON  INV_PURCHASE_HDR.DOC_ID=tbl_CurRepor.Invoice_no LEFT OUTER JOIN   PAY_SUPPLIER  ON INV_PURCHASE_HDR.SUPPLIER_CODE = PAY_SUPPLIER.CODE WHERE (INV_PURCHASE_HDR.DOC_TYPE LIKE '%' +  @DOC_TYPE + '%') AND (INV_PURCHASE_HDR.SUPPLIER_CODE LIKE N'%' + @SUP_CODE + N'%')";
                   // cmd.CommandText = "SELECT   tbl_curRepor.Invoice_no,INV_PURCHASE_HDR.DOC_NO, INV_PURCHASE_HDR.DOC_DATE_GRE as Date, tbl_curRepor.Currency_amt as ExcangeRate ,tbl_curRepor.Currency_code as Currency, PAY_SUPPLIER.DESC_ENG as Supplier, (INV_PURCHASE_HDR.TAX_TOTAL)*tbl_curRepor.Currency_amt as 'Tax Total', INV_PURCHASE_HDR.FREIGHT_AMT as Freight, INV_PURCHASE_HDR.GROSS*tbl_curRepor.Currency_amt as 'Gross Amount', INV_PURCHASE_HDR.DISCOUNT_VAL*tbl_curRepor.Currency_amt as Discount,INV_PURCHASE_HDR.NET_VAL*tbl_curRepor.Currency_amt as 'Net Value' FROM            INV_PURCHASE_HDR INNER JOIN tbl_CurRepor ON  INV_PURCHASE_HDR.DOC_ID=tbl_CurRepor.Invoice_no LEFT OUTER JOIN   PAY_SUPPLIER  ON INV_PURCHASE_HDR.SUPPLIER_CODE = PAY_SUPPLIER.CODE WHERE (INV_PURCHASE_HDR.DOC_TYPE LIKE '%' + @DOC_TYPE + '%') AND (INV_PURCHASE_HDR.SUPPLIER_CODE LIKE N'%' + @SUP_CODE + N'%')";
                }
                //if (String.IsNullOrEmpty(Cbx_salestype.SelectedValue.ToString()))
                //{
                //    cmd.Parameters.AddWithValue("@DOC_TYPE", DBNull.Value);
                //}
                //else
                //{
                    cmd.Parameters.AddWithValue("@DOC_TYPE", Cbx_salestype.SelectedValue);
               
              
                    cmd.Parameters.AddWithValue("@SUP_CODE", Cbx_supplier.SelectedValue);
               
                
                    cmd.Parameters.AddWithValue("@Name", ItemName.Text);
               
                    cmd.Parameters.AddWithValue("@code", CURCODE.Text);
               
                cmd.CommandType = CommandType.Text;
                adapter.SelectCommand = cmd;
                adapter.Fill(dt);
                cmd.Parameters.Clear();
                conn.Close();
                try
                {
                    double TotalPurchase = 0, Discount = 0;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TotalPurchase = TotalPurchase + Convert.ToDouble(dt.Rows[i]["Net Value"].ToString());
                        Discount = Discount + Convert.ToDouble(dt.Rows[i]["Discount"].ToString());
                    }

                    DataRow newRow5 = dt.NewRow();
                    //  newRow5["Freight"] = "Total :";
                   
                    newRow5["Net Value"] = TotalPurchase;
                    newRow5["Discount"] = Discount;
                    dt.Rows.Add(newRow5);

                    DG_GRIDVIEW.DataSource = dt;

                    DG_GRIDVIEW.Columns["Net Value"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    DG_GRIDVIEW.Rows[DG_GRIDVIEW.Rows.Count - 1].DefaultCellStyle.ForeColor = System.Drawing.Color.Red;
                    DG_GRIDVIEW.FirstDisplayedScrollingRowIndex = DG_GRIDVIEW.RowCount - 1;
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }
            }
            catch(Exception ex) {
                MessageBox.Show(ex.Message);
                conn.Close();
            }
            try
            {
                for (int i = 3; i < DG_GRIDVIEW.Columns.Count; i++)
                    DG_GRIDVIEW.Columns[i].DefaultCellStyle.Format = "N3";
            }
            catch
            {


            }
        }

        private void Currency_Report_Load(object sender, EventArgs e)
        {
          
            BindSupplier();
            BindSalesType();
            GetItemName();
           
           
        }

       
      
        
       
        public void BindSupplier()
        {
            try
            {
                dtb = stkrpt.BindSupplier();

                Cbx_supplier.ValueMember = "CODE";
                Cbx_supplier.DisplayMember = "DESC_ENG";

                DataRow row = dtb.NewRow();
                row[0] = "";
                dtb.Rows.InsertAt(row, 0);
                Cbx_supplier.DataSource = dtb;
            }
            catch
            {
            }
        }

        public void BindSalesType()
        {
            try
            {
                dtb = stkrpt.BindSalesTypes();

                Cbx_salestype.ValueMember = "CODE";
                Cbx_salestype.DisplayMember = "DESC_ENG";

                DataRow row = dtb.NewRow();
                row[0] = "";
                dtb.Rows.InsertAt(row, 0);
                Cbx_salestype.DataSource = dtb;
            }
            catch
            {
            }
        }

        public void GetItemName()
        {
            try
            {
                DataTable dt = new DataTable();
                conn.Open();
                cmd.CommandText = "select CODE,DESC_ENG FROM INV__ITM_DIRECTORY";
                cmd.CommandType = CommandType.Text;
                adapter.Fill(dt);
                ItemName.DataSource = dt;
                ItemName.ValueMember = "CODE";
                ItemName.DisplayMember = "DESC_ENG";
                conn.Close();
            }
            catch
            {
                conn.Close();
            }
        }

        private void DG_GRIDVIEW_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
           
        }
    }
}
