using Sys_Sols_Inventory.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sys_Sols_Inventory.Statements
{
    public partial class Invoice_Payment : Form
    {
        int form = 0;
        public Invoice_Payment(int value)
        {
            this.form = value;
            InitializeComponent();
        }

        bool hasArabic = false;                
        string query = "";
        

        private void Invoice_Payment_Load(object sender, EventArgs e)
        {
            BindCustomer();
            Hashtable ht = Common.getSettings();
            hasArabic = Convert.ToBoolean(ht["Arabic"]);
        }

        public void BindCustomer()
        {
            string Query;
            if (form == 0)
                Query = "SELECT    CODE, DESC_ENG FROM         REC_CUSTOMER";
            else
                Query = "SELECT    CODE, DESC_ENG FROM         PAY_SUPPLIER";
            DataTable dt = DbFunctions.GetDataTable(Query);
            cmb_name.ValueMember = "CODE";
            cmb_name.DisplayMember = "DESC_ENG";
            DataRow row = dt.NewRow();
            row[0] = "";
            dt.Rows.InsertAt(row, 0);
            cmb_name.DataSource = dt;
        }

        private void kryptonLabel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_seach_Click(object sender, EventArgs e)
        {
            
            DataTable dt = new DataTable();
            if (form == 0)
            {
                if (cmb_name.SelectedIndex != 0)
                {

                    if (!Chk.Checked)
                    {
                        if (hasArabic)
                        {
                            query = @"SELECT        INV_SALES_HDR.DOC_ID AS [INV NO], SAL.DOC_NO AS REF,  SAL.DOC_DATE_GRE as DATE , SAL.DOC_DATE_HIJ AS [DATE ARB], INV_SALES_HDR.NOTES, 
                         EMP_EMPLOYEES.Emp_Fname + EMP_EMPLOYEES.Emp_Mname + EMP_EMPLOYEES.Emp_Lname AS [SALES MAN], SAL.NET_AMOUNT AS [INV AMOUNT], dbo.CR_SAL_PAID(SAL.DOC_NO) 
                         AS[PAID AMOUNT], SAL.NET_AMOUNT - dbo.CR_SAL_PAID(SAL.DOC_NO) AS BALANCE
FROM INV_SALES_HDR AS SAL INNER JOIN
                         INV_SALES_HDR ON SAL.DOC_NO = INV_SALES_HDR.DOC_NO LEFT OUTER JOIN
                         EMP_EMPLOYEES ON INV_SALES_HDR.SALESMAN_CODE = EMP_EMPLOYEES.Empid
WHERE(SAL.DOC_TYPE = 'SAL.CRD') AND(SAL.CUSTOMER_CODE = '" + cmb_name.SelectedValue + "') ORDER BY DATE";
                        }
                        else
                        {
                            query = @"SELECT        INV_SALES_HDR.DOC_ID AS [INV NO], SAL.DOC_NO AS REF, SAL.DOC_DATE_GRE as DATE , INV_SALES_HDR.NOTES, 
                         EMP_EMPLOYEES.Emp_Fname + EMP_EMPLOYEES.Emp_Mname + EMP_EMPLOYEES.Emp_Lname AS [SALES MAN], SAL.NET_AMOUNT AS [INV AMOUNT], dbo.CR_SAL_PAID(SAL.DOC_NO) 
                         AS[PAID AMOUNT], SAL.NET_AMOUNT - dbo.CR_SAL_PAID(SAL.DOC_NO) AS BALANCE
FROM INV_SALES_HDR AS SAL INNER JOIN
                         INV_SALES_HDR ON SAL.DOC_NO = INV_SALES_HDR.DOC_NO LEFT OUTER JOIN
                         EMP_EMPLOYEES ON INV_SALES_HDR.SALESMAN_CODE = EMP_EMPLOYEES.Empid
WHERE(SAL.DOC_TYPE = 'SAL.CRD') AND(SAL.CUSTOMER_CODE = '" + cmb_name.SelectedValue + "') ORDER BY DATE";
                        }

                        dt = DbFunctions.GetDataTable(query);
                    }
                    else
                    {
                        if (hasArabic)
                        {
                            query = @"SELECT        INV_SALES_HDR.DOC_ID AS [INV NO], SAL.DOC_NO AS REF, SAL.DOC_DATE_GRE AS DATE , SAL.DOC_DATE_HIJ AS [DATE ARB], INV_SALES_HDR.NOTES, 
                         EMP_EMPLOYEES.Emp_Fname + EMP_EMPLOYEES.Emp_Mname + EMP_EMPLOYEES.Emp_Lname AS [SALES MAN], SAL.NET_AMOUNT AS [INV AMOUNT], dbo.CR_SAL_PAID(SAL.DOC_NO) 
                         AS[PAID AMOUNT], SAL.NET_AMOUNT - dbo.CR_SAL_PAID(SAL.DOC_NO) AS BALANCE
FROM INV_SALES_HDR AS SAL INNER JOIN
                         INV_SALES_HDR ON SAL.DOC_NO = INV_SALES_HDR.DOC_NO LEFT OUTER JOIN
                         EMP_EMPLOYEES ON INV_SALES_HDR.SALESMAN_CODE = EMP_EMPLOYEES.Empid
WHERE(SAL.DOC_TYPE = 'SAL.CRD') AND(SAL.CUSTOMER_CODE = '" + cmb_name.SelectedValue + "') AND convert(varchar, SAL.DOC_DATE_GRE, 101) BETWEEN @d1 AND @d2 ORDER BY DATE";
                        }
                        else
                        {
                            query = @"SELECT        INV_SALES_HDR.DOC_ID AS [INV NO], SAL.DOC_NO AS REF, SAL.DOC_DATE_GRE AS DATE , INV_SALES_HDR.NOTES, 
                         EMP_EMPLOYEES.Emp_Fname + EMP_EMPLOYEES.Emp_Mname + EMP_EMPLOYEES.Emp_Lname AS [SALES MAN], SAL.NET_AMOUNT AS [INV AMOUNT], dbo.CR_SAL_PAID(SAL.DOC_NO) 
                         AS[PAID AMOUNT], SAL.NET_AMOUNT - dbo.CR_SAL_PAID(SAL.DOC_NO) AS BALANCE
FROM INV_SALES_HDR AS SAL INNER JOIN
                         INV_SALES_HDR ON SAL.DOC_NO = INV_SALES_HDR.DOC_NO LEFT OUTER JOIN
                         EMP_EMPLOYEES ON INV_SALES_HDR.SALESMAN_CODE = EMP_EMPLOYEES.Empid
WHERE(SAL.DOC_TYPE = 'SAL.CRD') AND(SAL.CUSTOMER_CODE = '" + cmb_name.SelectedValue + "') AND convert(varchar, SAL.DOC_DATE_GRE, 101) BETWEEN @d1 AND @d2 ORDER BY DATE";
                        }

                        Dictionary<string, object> param = new Dictionary<string, object>();
                        param.Add("@d1", StartDate.Value.Date);
                        param.Add("@d2", EndDate.Value.Date);
                        dt = DbFunctions.GetDataTable(query, param);
                    }
                }
            }
            else
            {
                if (cmb_name.SelectedIndex != 0)
                {

                    if (!Chk.Checked)
                    {
                        if (hasArabic)
                        {
                            query = @"SELECT        PUR_HDR.DOC_ID AS [INV NO], PUR_HDR.DOC_NO AS REF, PUR_HDR.DOC_DATE_GRE AS DATE, PUR_HDR.DOC_DATE_HIJ AS [DATE ARB], PUR_HDR.NOTES, PUR_HDR.SUP_INV_NO AS [SUPP INV NO], PUR_HDR.NET_VAL AS [INV AMOUNT], dbo.CR_PUR_PAID(PUR_HDR.DOC_NO) 
                         AS [PAID AMOUNT], PUR_HDR.NET_VAL - dbo.CR_PUR_PAID(PUR_HDR.DOC_NO) AS BALANCE
FROM            INV_PURCHASE_HDR AS PUR_HDR INNER JOIN
                         GEN_PUR_TYPE ON PUR_HDR.PUR_TYPE = GEN_PUR_TYPE.CODE
WHERE        (PUR_HDR.DOC_TYPE = 'PUR.CRD') AND (PUR_HDR.SUPPLIER_CODE ='" + cmb_name.SelectedValue + "') ORDER BY DATE";
                        }
                        else
                        {
                            query = @"SELECT        PUR_HDR.DOC_ID AS [INV NO], PUR_HDR.DOC_NO AS REF, PUR_HDR.DOC_DATE_GRE AS DATE, PUR_HDR.NOTES, PUR_HDR.SUP_INV_NO AS [SUPP INV NO], PUR_HDR.NET_VAL AS [INV AMOUNT], dbo.CR_PUR_PAID(PUR_HDR.DOC_NO) 
                         AS [PAID AMOUNT], PUR_HDR.NET_VAL - dbo.CR_PUR_PAID(PUR_HDR.DOC_NO) AS BALANCE
FROM            INV_PURCHASE_HDR AS PUR_HDR INNER JOIN
                         GEN_PUR_TYPE ON PUR_HDR.PUR_TYPE = GEN_PUR_TYPE.CODE
WHERE        (PUR_HDR.DOC_TYPE = 'PUR.CRD') AND (PUR_HDR.SUPPLIER_CODE ='" + cmb_name.SelectedValue + "') ORDER BY DATE";
                        }

                        dt = DbFunctions.GetDataTable(query);
                    }
                    else
                    {
                        if (hasArabic)
                        {
                            query = @"SELECT        PUR_HDR.DOC_ID AS [INV NO], PUR_HDR.DOC_NO AS REF, PUR_HDR.DOC_DATE_GRE AS DATE, PUR_HDR.DOC_DATE_HIJ AS [DATE ARB], PUR_HDR.NOTES, PUR_HDR.SUP_INV_NO AS [SUPP INV NO], PUR_HDR.NET_VAL AS [INV AMOUNT], dbo.CR_PUR_PAID(PUR_HDR.DOC_NO) 
                         AS [PAID AMOUNT], PUR_HDR.NET_VAL - dbo.CR_PUR_PAID(PUR_HDR.DOC_NO) AS BALANCE
FROM            INV_PURCHASE_HDR AS PUR_HDR INNER JOIN
                         GEN_PUR_TYPE ON PUR_HDR.PUR_TYPE = GEN_PUR_TYPE.CODE
WHERE        (PUR_HDR.DOC_TYPE = 'PUR.CRD') AND (PUR_HDR.SUPPLIER_CODE ='" + cmb_name.SelectedValue + "') AND convert(varchar, PUR_HDR.DOC_DATE_GRE, 101) BETWEEN @d1 AND @d2 ORDER BY DATE";

                        }
                        else
                        {
                            query = @"SELECT        PUR_HDR.DOC_ID AS [INV NO], PUR_HDR.DOC_NO AS REF, PUR_HDR.DOC_DATE_GRE AS DATE, PUR_HDR.NOTES, PUR_HDR.SUP_INV_NO AS [SUPP INV NO], PUR_HDR.NET_VAL AS [INV AMOUNT], dbo.CR_PUR_PAID(PUR_HDR.DOC_NO) 
                         AS [PAID AMOUNT], PUR_HDR.NET_VAL - dbo.CR_PUR_PAID(PUR_HDR.DOC_NO) AS BALANCE
FROM            INV_PURCHASE_HDR AS PUR_HDR INNER JOIN
                         GEN_PUR_TYPE ON PUR_HDR.PUR_TYPE = GEN_PUR_TYPE.CODE
WHERE        (PUR_HDR.DOC_TYPE = 'PUR.CRD') AND (PUR_HDR.SUPPLIER_CODE ='" + cmb_name.SelectedValue + "') AND convert(varchar, PUR_HDR.DOC_DATE_GRE, 101) BETWEEN @d1 AND @d2 ORDER BY DATE";
                        }

                        Dictionary<string, object> param = new Dictionary<string, object>();
                        param.Add("@d1", StartDate.Value.Date);
                        param.Add("@d2", EndDate.Value.Date);
                        dt = DbFunctions.GetDataTable(query, param);
                    }
                }
            }
            if(dt.Rows.Count>0)
            { 
                var balance = ((from s in dt.AsEnumerable()
                                    select decimal.Parse(s["BALANCE"].ToString())) as IEnumerable<decimal>).Sum();
                    var paid = ((from s in dt.AsEnumerable()
                                 select decimal.Parse(s["PAID AMOUNT"].ToString())) as IEnumerable<decimal>).Sum();
                    var total = ((from s in dt.AsEnumerable()
                                  select decimal.Parse(s["INV AMOUNT"].ToString())) as IEnumerable<decimal>).Sum();

                    if (hasArabic)
                        dt.Rows.Add(null, null, null, null, null, "Total", total, paid, balance);
                    else
                        dt.Rows.Add(null, null, null, null, "Total", total, paid, balance);

                    dgv_Bills.DataSource = dt;
                }

                    if (dt.Rows.Count > 0)
                    {
                        dgv_Bills.CurrentCell = dgv_Bills.Rows[dgv_Bills.RowCount - 1].Cells[0];
                        Font font = new Font(dgv_Bills.DefaultCellStyle.Font.FontFamily, 9, FontStyle.Bold);
                        dgv_Bills.Rows[dgv_Bills.RowCount - 1].DefaultCellStyle.Font = font;
                        dgv_Bills.Rows[dgv_Bills.RowCount - 1].DefaultCellStyle.ForeColor = Color.Red;
                        dgv_Bills.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                        dgv_Bills.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                        dgv_Bills.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                        dgv_Bills.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

                        dgv_Bills.Columns["BALANCE"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dgv_Bills.Columns["PAID AMOUNT"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dgv_Bills.Columns["INV AMOUNT"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dgv_Bills.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }              
            
        }

        private void dgv_Bills_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (hasArabic)
            {
                if (e.ColumnIndex == 6 || e.ColumnIndex == 7 || e.ColumnIndex == 8)
                {
                    e.CellStyle.Format = "N2";
                }
            }
            else
            {
                if (e.ColumnIndex == 5 || e.ColumnIndex == 6 || e.ColumnIndex == 7)
                {
                    e.CellStyle.Format = "N2";
                }
            }
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
    }
}
