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
    public partial class FrmStockWindow : Form
    {
        private DataTable table = new DataTable();
        //private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        //private SqlCommand cmd = new SqlCommand();
        //private SqlDataAdapter adapter = new SqlDataAdapter();
        private int selectedIndex = 0;

        public FrmStockWindow()
        {
            InitializeComponent();
            //cmd.Connection = conn;
            //adapter.SelectCommand = cmd;
        }

        public FrmStockWindow(int index):this()
        {
            selectedIndex = index;
        }

        private void frmStockWindow_Load(object sender, EventArgs e)
        {
            cmbStockType.SelectedIndex = selectedIndex;
            dgItems.DataSource = table;

            if (selectedIndex > 0)
            {
                btnGo.PerformClick();
            }
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            if (cmbStockType.SelectedIndex == 0)
            {
                MessageBox.Show("Please Select a stock type to proceed.");
                return;
            }
            table.Clear();
            string query = "";
            query = "SELECT CODE, DESC_ENG, DESC_ARB, TYPE, [GROUP], CATEGORY, TRADEMARK, UOM, isnull(tblStock.Qty,0) as [Stock Qty] FROM INV_ITEM_DIRECTORY JOIN (SELECT SUM(Qty) as Qty, Item_id FROM tblStock GROUP BY Item_id) as tblStock ON CODE = Item_id";
            if (cmbStockType.SelectedIndex == 1)
            {
                query += " WHERE tblStock.Qty = MINIMUM_QTY";
            }
            else if (cmbStockType.SelectedIndex == 2)
            {
                query = "SELECT CODE, DESC_ENG, DESC_ARB, TYPE, [GROUP], CATEGORY, TRADEMARK, UOM, isnull(tblStock.Qty, 0) as [Stock Qty] FROM INV_ITEM_DIRECTORY LEFT JOIN (SELECT SUM(Qty) as Qty, Item_id FROM tblStock GROUP BY Item_id) as tblStock ON CODE = Item_id AND isnull(tblStock.Qty, 0) < MINIMUM_QTY";
            }
            else
            {
                query = "SELECT CODE, DESC_ENG, DESC_ARB, TYPE, [GROUP], CATEGORY, TRADEMARK, UOM, isnull(tblStock.Qty, 0) as [Stock Qty] FROM INV_ITEM_DIRECTORY JOIN (SELECT SUM(Qty) as Qty, Item_id FROM tblStock GROUP BY Item_id) as tblStock ON CODE = Item_id AND isnull(tblStock.Qty, 0) >= MAXIMUM_QTY";
            }
            //adapter.Fill(table);
            table = Model.DbFunctions.GetDataTable(query);
        }
    }
}
