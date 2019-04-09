using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sys_Sols_Inventory.Model;
namespace Sys_Sols_Inventory
{
    public partial class SupplierMasterHelp : Form
    {
        //private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        //private SqlCommand cmd = new SqlCommand();
        //private SqlDataAdapter adapter = new SqlDataAdapter();
        private DataTable table = new DataTable();
        private BindingSource source = new BindingSource();
        public DataGridViewCellCollection c;
        private genEnum currentForm = genEnum.Group;
        private string tableName = "";
        private DataTable fieldTable = new DataTable();
        clsHelper help = new clsHelper();
        public SupplierMasterHelp(int i)
        {
            InitializeComponent();
            //cmd.Connection = conn;
            //adapter.SelectCommand = cmd;
            if (i == 0)
            {
                btnDelete.Enabled = false;
            }
            else
            {
                btnOK.Enabled = false;
            }
        }

        private void SupplierMasterHelp_Load(object sender, EventArgs e)
        {
            //cmd.CommandText = "SELECT CODE,DESC_ENG,DESC_ARB,TYPE,ADDRESS_A,ADDRESS_B,POBOX,TELE1,TELE2,MOBILE,EMAIL,CITY_CODE,COUNTRY_CODE,CONVERT(NVARCHAR,OPENING_BAL,103) AS OPENING_BAL,FAX,LedgerId FROM PAY_SUPPLIER" + tableName;
            //adapter.Fill(table);
            help.TableName = tableName;
            table = help.getAllSupplier();
            source.DataSource = table;
            dgItems.DataSource = source;

            fieldTable.Columns.Add("key");
            fieldTable.Columns.Add("value");
            cmbFilter.DataSource = fieldTable;
            cmbFilter.ValueMember = "key";
            cmbFilter.DisplayMember = "value";
            fieldTable.Rows.Add("CODE", "Code");
            fieldTable.Rows.Add("DESC_ENG", "Eng. Name");
            fieldTable.Rows.Add("DESC_ARB", "Arb. Name");
            fieldTable.Rows.Add("ADDRESS_A", "Address Line 1");
            fieldTable.Rows.Add("ADDRESS_B", "Address Line 2");
            fieldTable.Rows.Add("POBOX", "Address Line 3");
            fieldTable.Rows.Add("TELE1", "Telephone 1");
            fieldTable.Rows.Add("TELE2", "Telephone 2");
            fieldTable.Rows.Add("MOBILE", "Mobile");
            fieldTable.Rows.Add("EMAIL", "Email");
            fieldTable.Rows.Add("CITY_CODE", "City Code");
            fieldTable.Rows.Add("COUNTRY_CODE", "Country Code");
            fieldTable.Rows.Add("OPENING_BAL", "Opening Balance");
            fieldTable.Rows.Add("FAX","Fax");

            DataGridViewColumnCollection c = dgItems.Columns;
            c[0].HeaderText = "Code";
            c[1].HeaderText = "Eng. Name";
            c[2].HeaderText = "Arb. Name";
            if (currentForm != genEnum.CustomerType)
            {
                int colWidth = (dgItems.Width - 100) / 2;
                c[1].Width = colWidth - 22;
                c[2].Width = colWidth - 22;
            }
        }

        private void dgItems_DoubleClick(object sender, EventArgs e)
        {
            btnOK.PerformClick();
        }

        private void dgItems_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                btnOK.PerformClick();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (dgItems.Rows.Count > 0 && dgItems.CurrentRow != null)
            {
                c = dgItems.CurrentRow.Cells;
            }
        }
    }
}
