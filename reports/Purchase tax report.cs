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
    public partial class Purchase_tax_report : Form
    {
        Class.Salesman_Report clsSR = new Class.Salesman_Report();
        BindingSource bind = new BindingSource();
        Class.Stock_Report stkrpt = new Class.Stock_Report();
        //SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);

        public Purchase_tax_report()
        {
            InitializeComponent();
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            Cbx_supplier.SelectedIndexChanged += Cbx_supplier_SelectedIndexChanged;
            if (Chk.Checked)
            {
                clsSR.Date1 = StartDate.Value;
                clsSR.Date2 = EndDate.Value;
                DataTable dt = clsSR.purchaseTax_date();
                bind.DataSource = dt;
                dgv_grdview.DataSource = bind;
                dgv_grdview.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                if (dt.Rows.Count > 0)
                {
                    dgv_grdview.Columns["SUPPLYER NAME"].FillWeight = 350;
                    dgv_grdview.Columns["GST/UIN NO"].FillWeight = 150;
                }
            }
            else
            {
                DataTable dt = clsSR.purchaseTax();
                bind.DataSource = dt;
                dgv_grdview.DataSource = bind;
                dgv_grdview.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                if (dt.Rows.Count > 0)
                {
                    dgv_grdview.Columns["SUPPLYER NAME"].FillWeight = 350;
                    dgv_grdview.Columns["GST/UIN NO"].FillWeight = 150;
                }
            }
            sum();
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

        public void sum()
        {
            if (dgv_grdview.Rows.Count > 0)
            {
                decimal taxtotal = 0, sgst = 0, cgst = 0, igst = 0, gross = 0;
                for (int i = 0; i < dgv_grdview.Rows.Count; i++)
                {
                    taxtotal = taxtotal + Convert.ToDecimal(dgv_grdview.Rows[i].Cells["TOTAL TAX"].Value);
                    sgst = sgst + Convert.ToDecimal(dgv_grdview.Rows[i].Cells["CGST"].Value);
                    cgst = cgst + Convert.ToDecimal(dgv_grdview.Rows[i].Cells["SGST"].Value);
                    igst = igst + Convert.ToDecimal(dgv_grdview.Rows[i].Cells["IGST"].Value);
                    gross = gross + Convert.ToDecimal(dgv_grdview.Rows[i].Cells["TOTAL GROSS"].Value);
                }

                dgv_grdview.Rows[dgv_grdview.Rows.Count - 1].Cells["ITEM TAX"].Value = "Total";
                dgv_grdview.Rows[dgv_grdview.Rows.Count - 1].Cells["TOTAL TAX"].Value = taxtotal;
                dgv_grdview.Rows[dgv_grdview.Rows.Count-1].Cells["CGST"].Value = cgst;
                dgv_grdview.Rows[dgv_grdview.Rows.Count-1].Cells["SGST"].Value = sgst;
                dgv_grdview.Rows[dgv_grdview.Rows.Count-1].Cells["IGST"].Value = igst;
                dgv_grdview.Rows[dgv_grdview.Rows.Count-1].Cells["TOTAL GROSS"].Value = gross;
                dgv_grdview.Rows[dgv_grdview.Rows.Count-1].DefaultCellStyle.Font = new Font(dgv_grdview.Font, FontStyle.Bold);
            }
        }
        private void btn_exprt_Click(object sender, EventArgs e)
        {
            try
            {
                Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
                Microsoft.Office.Interop.Excel._Worksheet worksheet = null;

                app.Visible = true;

                //B2B
                worksheet = workbook.Sheets["Sheet1"];
                worksheet = workbook.ActiveSheet;
                worksheet.Name = "Purchase";
                DataTable dt = new DataTable();
                //if (conn.State == ConnectionState.Closed)
                //    conn.Open();
               // SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_companysetup", conn);
                string cmd = "SELECT * FROM tbl_companysetup";
                //SqlDataAdapter adptr = new SqlDataAdapter(cmd);
                //adptr.Fill(dt);
                dt = Model.DbFunctions.GetDataTable(cmd);
               // conn.Close();
                string report = "";
                

                //heading
                worksheet.Cells[1, 3] = "       " + dt.Rows[0]["company_name"].ToString();
                worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[1, 14]].Merge();
                worksheet.Cells[2, 3] = dt.Rows[0]["address"].ToString();
                worksheet.Range[worksheet.Cells[2, 1], worksheet.Cells[2, 14]].Merge();                
                worksheet.Cells[3, 3] = "4A-B2B(Other than Reverse Charge & E-Commerce Operator)";
                worksheet.Range[worksheet.Cells[3, 1], worksheet.Cells[3, 14]].Merge();
                worksheet.Cells[5, 1] = "Sl.No";
                worksheet.Cells[5, 2] = "Date";
                worksheet.Cells[5, 3] = "Voucher No";
                worksheet.Cells[5, 4] = "Supplier";
                worksheet.Cells[5, 5] = "GSTIN/UIN";
                worksheet.Cells[5, 6] = "Address";
                worksheet.Cells[5, 7] = "Invoice Ref.No";
                worksheet.Cells[5, 8] = "Tax %";
                worksheet.Cells[5, 9] = "Central Tax";
                worksheet.Cells[5, 10] = "State Tax";
                worksheet.Cells[5, 11] = "Integrated Tax";
                worksheet.Cells[5, 12] = "Total Tax";
                worksheet.Cells[5, 13] = "Grand Total";
                worksheet.Cells[5, 14] = "Invoice Type";
                worksheet.Cells[5, 1].EntireRow.Font.Bold = worksheet.Cells[1, 1].EntireRow.Font.Bold = worksheet.Cells[3, 1].EntireRow.Font.Bold = true;
                int indx = 6;

                //GET DATA
                


                if (dgv_grdview.Rows.Count > 0)
                {
                    for (int i = 0; i < dgv_grdview.Rows.Count-1; i++)
                    {
                        worksheet.Cells[indx, 1] = i+1;
                        worksheet.Cells[indx, 2] = dgv_grdview.Rows[i].Cells["DATE"].Value.ToString();
                        worksheet.Cells[indx, 3] = dgv_grdview.Rows[i].Cells["INVOICE NO"].Value.ToString();
                        worksheet.Cells[indx, 4] = dgv_grdview.Rows[i].Cells["SUPPLYER NAME"].Value.ToString();
                        worksheet.Cells[indx, 5] = dgv_grdview.Rows[i].Cells["GST/UIN NO"].Value.ToString();
                        worksheet.Cells[indx, 6] = dgv_grdview.Rows[i].Cells["ADDRESS"].Value.ToString();
                        worksheet.Cells[indx, 7] = dgv_grdview.Rows[i].Cells["SUPPLYER INV.NO"].Value.ToString();
                        worksheet.Cells[indx, 8] = dgv_grdview.Rows[i].Cells["ITEM TAX"].Value.ToString();
                        worksheet.Cells[indx, 9] = dgv_grdview.Rows[i].Cells["CGST"].Value.ToString();
                        worksheet.Cells[indx, 10] = dgv_grdview.Rows[i].Cells["SGST"].Value.ToString();
                        worksheet.Cells[indx, 11] = dgv_grdview.Rows[i].Cells["IGST"].Value.ToString();
                        worksheet.Cells[indx, 12] = dgv_grdview.Rows[i].Cells["TOTAL TAX"].Value.ToString();
                        worksheet.Cells[indx, 13] = dgv_grdview.Rows[i].Cells["TOTAL GROSS"].Value.ToString();
                        if (dgv_grdview.Rows[i].Cells["STATE"].Value.ToString() == clsSR.Current_State())
                        {
                            worksheet.Cells[indx, 14] = "Local";
                        }
                        else
                        {
                             worksheet.Cells[indx, 14] = "Inter State";
                        }
                        
                        indx++;
                    }
                    worksheet.Cells[indx, 8]=dgv_grdview.Rows[dgv_grdview.Rows.Count - 1].Cells["ITEM TAX"].Value;
                    worksheet.Cells[indx, 12]=dgv_grdview.Rows[dgv_grdview.Rows.Count - 1].Cells["TOTAL TAX"].Value;
                    worksheet.Cells[indx, 9]=dgv_grdview.Rows[dgv_grdview.Rows.Count - 1].Cells["CGST"].Value; 
                    worksheet.Cells[indx, 10]=dgv_grdview.Rows[dgv_grdview.Rows.Count - 1].Cells["SGST"].Value; 
                    worksheet.Cells[indx, 11]=dgv_grdview.Rows[dgv_grdview.Rows.Count - 1].Cells["IGST"].Value ;
                    worksheet.Cells[indx, 13] = dgv_grdview.Rows[dgv_grdview.Rows.Count - 1].Cells["TOTAL GROSS"].Value;
                    worksheet.Cells[indx, 13].EntireRow.Font.Bold = true; 

                }
                worksheet.Columns[1].ColumnWidth = 10;
                worksheet.Columns[2].ColumnWidth = 17;
                worksheet.Columns[3].ColumnWidth = 11;
                worksheet.Columns[4].ColumnWidth = 50;
                worksheet.Columns[5].ColumnWidth = 20;
                worksheet.Columns[7].ColumnWidth = 15;
                worksheet.Columns[8].ColumnWidth = 10;
                worksheet.Columns[9].ColumnWidth = 10;
                worksheet.Columns[10].ColumnWidth = 10;
                worksheet.Columns[11].ColumnWidth = 10;
                worksheet.Columns[12].ColumnWidth = 10;
                worksheet.Columns[13].ColumnWidth = 10;
                worksheet.Columns[14].ColumnWidth = 15;
                worksheet.Columns[6].ColumnWidth = 50;

                
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
                MessageBox.Show("" + ex);
            }
        }

        private void cmb_saletype_SelectedIndexChanged(object sender, EventArgs e)
        {

            bind.Filter = string.Format("[PURCHASE TYPE] LIKE '%{0}%' AND [VOUCHER TYPE] LIKE '%{1}%' AND [SUPPLYER NAME] LIKE '%{2}%'", cmb_saletype.Text.Replace("'", "''").Replace("*", "[*]"), Cbx_salemode.Text.Replace("'", "''").Replace("*", "[*]"), Cbx_supplier.Text.Replace("'", "''").Replace("*", "[*]"));
            sum();
                        
        }

        private void Cbx_salemode_SelectedIndexChanged(object sender, EventArgs e)
        {

            bind.Filter = string.Format("[PURCHASE TYPE] LIKE '%{0}%' AND [VOUCHER TYPE] LIKE '%{1}%' AND [SUPPLYER NAME] LIKE '%{2}%'", cmb_saletype.Text.Replace("'", "''").Replace("*", "[*]"), Cbx_salemode.Text.Replace("'", "''").Replace("*", "[*]"), Cbx_supplier.Text.Replace("'", "''").Replace("*", "[*]"));
            sum();
           
        }

        private void Purchase_tax_report_Load(object sender, EventArgs e)
        {
            Cbx_supplier.SelectedIndexChanged -= Cbx_supplier_SelectedIndexChanged;
            BindSupplier();
        }

        public void BindSupplier()
        {
            try
            {
                DataTable dt = stkrpt.BindSupplier();

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

        private void Cbx_supplier_SelectedIndexChanged(object sender, EventArgs e)
        {

            bind.Filter = string.Format("[PURCHASE TYPE] LIKE '%{0}%' AND [VOUCHER TYPE] LIKE '%{1}%' AND [SUPPLYER NAME] LIKE '%{2}%'", cmb_saletype.Text.Replace("'", "''").Replace("*", "[*]"), Cbx_salemode.Text.Replace("'", "''").Replace("*", "[*]"), Cbx_supplier.Text.Replace("'", "''").Replace("*", "[*]"));
            sum();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
