using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sys_Sols_Inventory
{
    public partial class Item_Helper : Form
    {

        private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        private SqlCommand cmd = new SqlCommand();
        private SqlDataAdapter adapter = new SqlDataAdapter();
        private DataTable table = new DataTable();
        private DataTable fieldTable = new DataTable();
        private BindingSource source = new BindingSource();

        public DataGridViewCellCollection c;
        bool ShowPurchase = false;
        public Item_Helper(int i)
        {
            InitializeComponent();
            if (i == 0)
            {
               
            }
            else
            {
                btnOK.Enabled = false;
            }

            cmd.Connection = conn;
            adapter.SelectCommand = cmd;
        }

        private void Item_Helper_Load(object sender, EventArgs e)
        {
            if (ShowPurchase)
            {
                //  cmd.CommandText = "SELECT     INV_ITEM_DIRECTORY.CODE AS [Item Code], INV_ITEM_DIRECTORY_UNITS.BARCODE AS Barcode, INV_ITEM_DIRECTORY.DESC_ENG AS [Item Name],INV_ITEM_DIRECTORY.DESC_ARB, INV_ITEM_DIRECTORY_UNITS.UNIT_CODE, INV_ITEM_PRICE.PRICE AS SALES, INV_ITEM_DIRECTORY.TaxId,INV_ITEM_PRICE_1.PRICE AS PURCHASE, GEN_TAX_MASTER.TaxRate FROM         INV_ITEM_DIRECTORY LEFT OUTER JOIN  GEN_TAX_MASTER ON INV_ITEM_DIRECTORY.TaxId = GEN_TAX_MASTER.TaxId LEFT OUTER JOIN  INV_ITEM_PRICE LEFT OUTER JOIN   INV_ITEM_DIRECTORY_UNITS ON INV_ITEM_PRICE.UNIT_CODE = INV_ITEM_DIRECTORY_UNITS.UNIT_CODE AND INV_ITEM_PRICE.ITEM_CODE = INV_ITEM_DIRECTORY_UNITS.ITEM_CODE LEFT OUTER JOIN INV_ITEM_PRICE AS INV_ITEM_PRICE_1 ON INV_ITEM_DIRECTORY_UNITS.UNIT_CODE = INV_ITEM_PRICE_1.UNIT_CODE AND INV_ITEM_DIRECTORY_UNITS.ITEM_CODE = INV_ITEM_PRICE_1.ITEM_CODE ON INV_ITEM_DIRECTORY.CODE = INV_ITEM_DIRECTORY_UNITS.ITEM_CODE WHERE     (INV_ITEM_PRICE.SAL_TYPE = '" + RATE_CODE.SelectedValue + "') AND (INV_ITEM_PRICE_1.SAL_TYPE = 'PUR')";
                // cmd.CommandText = "SELECT        INV_ITEM_DIRECTORY.CODE AS [Item Code], INV_ITEM_DIRECTORY_UNITS.BARCODE AS Barcode, INV_ITEM_DIRECTORY.DESC_ENG AS [Item Name], INV_ITEM_DIRECTORY.DESC_ARB, INV_ITEM_DIRECTORY_UNITS.UNIT_CODE, INV_ITEM_PRICE.PRICE AS SALES, GEN_TAX_MASTER.TaxRate,  ISNULL(INV_ITEM_PRICE_1.PRICE * GEN_TAX_MASTER.TaxRate / 100 + INV_ITEM_PRICE_1.PRICE, 0) AS PURVALUEWITHTAX,INV_ITEM_DIRECTORY.TaxId FROM            INV_ITEM_DIRECTORY LEFT OUTER JOIN  GEN_TAX_MASTER ON INV_ITEM_DIRECTORY.TaxId = GEN_TAX_MASTER.TaxId LEFT OUTER JOIN  INV_ITEM_PRICE LEFT OUTER JOIN INV_ITEM_DIRECTORY_UNITS ON INV_ITEM_PRICE.UNIT_CODE = INV_ITEM_DIRECTORY_UNITS.UNIT_CODE AND  INV_ITEM_PRICE.ITEM_CODE = INV_ITEM_DIRECTORY_UNITS.ITEM_CODE LEFT OUTER JOIN INV_ITEM_PRICE AS INV_ITEM_PRICE_1 ON INV_ITEM_DIRECTORY_UNITS.UNIT_CODE = INV_ITEM_PRICE_1.UNIT_CODE AND INV_ITEM_DIRECTORY_UNITS.ITEM_CODE = INV_ITEM_PRICE_1.ITEM_CODE ON INV_ITEM_DIRECTORY.CODE = INV_ITEM_DIRECTORY_UNITS.ITEM_CODE WHERE        (INV_ITEM_PRICE.SAL_TYPE = '" + RATE_CODE.SelectedValue + "') AND (INV_ITEM_PRICE_1.SAL_TYPE = 'PUR')";

                cmd.CommandText = "SELECT        INV_ITEM_DIRECTORY.CODE AS 'Item Code', INV_ITEM_DIRECTORY_UNITS.BARCODE AS Barcode, INV_ITEM_DIRECTORY.DESC_ENG AS [Item Name],  INV_ITEM_DIRECTORY.DESC_ARB, INV_ITEM_DIRECTORY_UNITS.UNIT_CODE, INV_ITEM_PRICE.PRICE AS SALES, GEN_TAX_MASTER.TaxRate,  ISNULL(INV_ITEM_PRICE_1.PRICE * GEN_TAX_MASTER.TaxRate / 100 + INV_ITEM_PRICE_1.PRICE, 0) AS PURVALUEWITHTAX, INV_ITEM_DIRECTORY.TaxId,INV_ITEM_DIRECTORY.HASSERIAL FROM            INV_ITEM_DIRECTORY LEFT OUTER JOIN  GEN_TAX_MASTER ON INV_ITEM_DIRECTORY.TaxId = GEN_TAX_MASTER.TaxId LEFT OUTER JOIN INV_ITEM_PRICE LEFT OUTER JOIN  INV_ITEM_DIRECTORY_UNITS ON INV_ITEM_PRICE.UNIT_CODE = INV_ITEM_DIRECTORY_UNITS.UNIT_CODE AND  INV_ITEM_PRICE.ITEM_CODE = INV_ITEM_DIRECTORY_UNITS.ITEM_CODE LEFT OUTER JOIN INV_ITEM_PRICE AS INV_ITEM_PRICE_1 ON INV_ITEM_DIRECTORY_UNITS.UNIT_CODE = INV_ITEM_PRICE_1.UNIT_CODE AND INV_ITEM_DIRECTORY_UNITS.ITEM_CODE = INV_ITEM_PRICE_1.ITEM_CODE ON INV_ITEM_DIRECTORY.CODE = INV_ITEM_DIRECTORY_UNITS.ITEM_CODE WHERE        (INV_ITEM_PRICE.SAL_TYPE = 'RTL') AND (INV_ITEM_PRICE_1.SAL_TYPE = 'PUR')";
            }
            else
            {
                cmd.CommandText = "SELECT     INV_ITEM_DIRECTORY.CODE AS 'Item Code', INV_ITEM_DIRECTORY_UNITS.BARCODE AS Barcode, INV_ITEM_DIRECTORY.DESC_ENG AS [Item Name],INV_ITEM_DIRECTORY.DESC_ARB, INV_ITEM_DIRECTORY_UNITS.UNIT_CODE, INV_ITEM_PRICE.PRICE AS SALES, INV_ITEM_DIRECTORY.TaxId,  GEN_TAX_MASTER.TaxRate,INV_ITEM_DIRECTORY.HASSERIAL FROM         INV_ITEM_PRICE INNER JOIN  INV_ITEM_DIRECTORY_UNITS ON INV_ITEM_PRICE.UNIT_CODE = INV_ITEM_DIRECTORY_UNITS.UNIT_CODE AND INV_ITEM_PRICE.ITEM_CODE = INV_ITEM_DIRECTORY_UNITS.ITEM_CODE LEFT OUTER JOIN INV_ITEM_DIRECTORY ON INV_ITEM_DIRECTORY_UNITS.ITEM_CODE = INV_ITEM_DIRECTORY.CODE LEFT OUTER JOIN GEN_TAX_MASTER ON GEN_TAX_MASTER.TaxId = INV_ITEM_DIRECTORY.TaxId WHERE     (INV_ITEM_PRICE.SAL_TYPE = 'RTL')";

            }
            cmd.CommandType = CommandType.Text;
            adapter.Fill(table);
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
            fieldTable.Rows.Add("TYPE", "Type Code");
            fieldTable.Rows.Add("GROUP", "Group Code");
            fieldTable.Rows.Add("CATEGORY", "Category Code");
            fieldTable.Rows.Add("TRADEMARK", "Trademark Code");
            fieldTable.Rows.Add("LOCATION", "location");
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (dgItems.Rows.Count > 0 && dgItems.CurrentRow != null)
            {
                c = dgItems.CurrentRow.Cells;
            }
        }

        private void dgItems_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnOK.PerformClick();
        }

        private void dgItems_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                btnOK.PerformClick();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            if (txtFilter.Text == "")
            {
                source.Filter = "";
            }
            else
            {
                source.Filter = cmbFilter.SelectedValue + " LIKE '" + txtFilter.Text + "%'";
            }
        }
    }
}
