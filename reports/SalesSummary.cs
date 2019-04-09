using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Sys_Sols_Inventory.reports
{
    public partial class SalesSummary : Form
    {
        //private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        //private SqlCommand cmd = new SqlCommand();
        //private SqlDataAdapter adapter = new SqlDataAdapter();
        private BindingSource source = new BindingSource();
        public SalesSummary()
        {
            InitializeComponent();
        }

        private void SalesSummary_Load(object sender, EventArgs e)
        {
            bindgridview();
        }

        public void bindgridview()
        {
            try
            {
                //cmd.Connection = conn;
                string cmd = "SELECT        INV_ITEM_DIRECTORY.DESC_ENG AS [Item Name],CODE FROM   INV_ITEM_DIRECTORY ";
                //cmd.CommandType = CommandType.Text;
                DataTable dt = new DataTable();
                //adapter.SelectCommand = cmd;
                //adapter.Fill(dt);
                dt = Model.DbFunctions.GetDataTable(cmd);
                source.DataSource = dt;

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

        private void btnSearch_Click(object sender, EventArgs e)
        { // TODO: This line of code loads data into the 'SalesSummaryEach.Sp_SummarySalesAll' table. You can move, or remove it, as needed.
            //string conection = Properties.Settings.Default.ConnectionStrings.ToString();
            //Sp_SummarySalesAllTableAdapter.Connection = new System.Data.SqlClient.SqlConnection(conection);
            Sp_SummarySalesAllTableAdapter.Connection = Model.DbFunctions.GetConnection();

            if (Chk.Checked == false)
            {
                if (txtCode.Text != "")
                {
                    this.Sp_SummarySalesAllTableAdapter.FillByCode(this.SalesSummaryEach.Sp_SummarySalesAll, txtCode.Text);

                    this.reportViewer1.RefreshReport();
                }
                else
                {
                    this.Sp_SummarySalesAllTableAdapter.FillAll(this.SalesSummaryEach.Sp_SummarySalesAll);

                    this.reportViewer1.RefreshReport();
                }
            }
            else 
            {
                if (txtCode.Text == "")
                {
                    DateTime startdate, enddate;
                    startdate = Convert.ToDateTime(DATE_FROM.Value.ToShortDateString());
                    enddate = Convert.ToDateTime(DATE_TO.Value.ToShortDateString());

                    this.Sp_SummarySalesAllTableAdapter.FillByDate(this.SalesSummaryEach.Sp_SummarySalesAll,startdate,enddate);

                    this.reportViewer1.RefreshReport();
                }
                else
                {
                    DateTime startdate, enddate;
                    startdate = Convert.ToDateTime(DATE_FROM.Value.ToShortDateString());
                    enddate = Convert.ToDateTime(DATE_TO.Value.ToShortDateString());
                    this.Sp_SummarySalesAllTableAdapter.FillByCodeDate(this.SalesSummaryEach.Sp_SummarySalesAll, startdate, enddate,txtCode.Text);

                    this.reportViewer1.RefreshReport();
                }
            }

        }

        private void DESC_ENG_TextChanged(object sender, EventArgs e)
        {
            if (DESC_ENG.Text != "")
            {
                dataGridItem.Visible = true;
                source.Filter = string.Format("[Item Name] LIKE '%{0}%' ", DESC_ENG.Text);
                dataGridItem.ClearSelection();
            }
            else
            {
                dataGridItem.Visible = false;

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

                    dataGridItem.Visible = false;
                }
            }
            catch
            { }
        }

        private void DESC_ENG_KeyDown(object sender, KeyEventArgs e)
        {
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
    }
}
