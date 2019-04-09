using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sys_Sols_Inventory
{
    public partial class Project_report : Form
    {
        Class.Stock_Report stkrpt = new Class.Stock_Report();
        DataTable dt = new DataTable();
        Model.ProjectDB ProjectDB = new Model.ProjectDB();
        private DataGridViewCellStyle HighlightStyle;
        public Project_report()
        {
            InitializeComponent();
        }

        private void Project_report_Load(object sender, EventArgs e)
        {
            BindForms();
            BindCustomer();
            cmbBindProject();
            GridbindAllProject();

        }

        public void BindCustomer()
        {
            dt = stkrpt.BindCustomer();
            CmbCustomer.ValueMember = "CODE";
            CmbCustomer.DisplayMember = "DESC_ENG";

            DataRow row = dt.NewRow();
            row[1] = "------SELECT------";
            dt.Rows.InsertAt(row, 0);
            CmbCustomer.DataSource = dt;
           
            CmbCustomer.SelectedIndex = 0;
        }
        public void BindForms()
        {
            cmbForm.Items.Add("SALE");
            cmbForm.Items.Add("PURCHASE");
            cmbForm.Items.Add("PAYMENT VOUCHER");
            cmbForm.Items.Add("RECIEPT VOUCHER");
            cmbForm.Items.Add("ACCOUNTING VOUCHER");
        }
        private void LoadProject()
        {
            try
            {
                ProjectDB.Customer = Convert.ToInt32(CmbCustomer.SelectedValue);
                drpProject.DataSource = ProjectDB.ProjectForComboCustomer();
                drpProject.DisplayMember = "DESC_ENG";
                drpProject.ValueMember = "Id";
                drpProject.SelectedIndex = 0;
            }
            catch (Exception ex) { }
        }
         private void cmbBindProject()
         {

             drpProject.DataSource = ProjectDB.ProjectForCombo();
             drpProject.DisplayMember = "DESC_ENG";
             drpProject.ValueMember = "Id";
         }
        private void GridbindAllProject()
        {
            DataTable dt = new DataTable();
            string query = "select * from Tbl_Project";
            if(dataGridView1.Rows.Count<1)
            dataGridView1.DataSource = Model.DbFunctions.GetDataTable(query);
           // dataGridView1.DataSource = ProjectDB.AllData();
        }
        
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Columns.Count > 0)
            {
                dataGridView1.Columns.Clear();
            }
                DataTable dt = new DataTable();
                string query = "SELECT Tbl_Project.Id,Tbl_Project.DESC_ENG AS [PROJECT NAME],Tbl_Project.[Customer ] AS [CUST CODE], REC_CUSTOMER.DESC_ENG AS [CUSTOMER NAME],TBL_PROJECT.SiteAddress,TBL_PROJECT.State,TBL_PROJECT.Location, tblStates.DESC_ENG AS [STATE NAME],Tbl_Project.Status,Tbl_Project.ACTIVE,Tbl_Project.[Estimated Budget] FROM TBL_PROJECT INNER JOIN REC_CUSTOMER ON Tbl_Project.[Customer ]=REC_CUSTOMER.CODE INNER JOIN TBLSTATES ON TBL_PROJECT.STATE=tblStates.CODE WHERE Customer='" + CmbCustomer.SelectedValue + "' ";
                dataGridView1.DataSource = Model.DbFunctions.GetDataTable(query);
        }

        private void CmbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadProject();
        }

       
        private int HighlightedRowIndex = -1;
        private void dataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            HighlightStyle = new DataGridViewCellStyle();
            //HighlightStyle.ForeColor = Color.Red;
            HighlightStyle.BackColor = Color.Gainsboro;
            HighlightStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);
            if (e.RowIndex == HighlightedRowIndex) return;

            // Unhighlight the previously highlighted row.
            if (HighlightedRowIndex >= 0)
            {
                SetRowStyle(dataGridView1.Rows[HighlightedRowIndex], null);
            }

            // Highlight the row holding the mouse.
            HighlightedRowIndex = e.RowIndex;
            if (HighlightedRowIndex >= 0)
            {
                SetRowStyle(dataGridView1.Rows[HighlightedRowIndex],
                    HighlightStyle);
            }
        }
        private void SetRowStyle(DataGridViewRow row, DataGridViewCellStyle style)
        {
            foreach (DataGridViewCell cell in row.Cells)
            {
                cell.Style = style;
            }
        }
        private void dataGridView1_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (HighlightedRowIndex >= 0)
            {
                SetRowStyle(dataGridView1.Rows[HighlightedRowIndex], null);
                HighlightedRowIndex = -1;
            }
        }
    }
}
