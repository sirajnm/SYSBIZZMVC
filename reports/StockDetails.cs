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
    public partial class StockDetails : Form
    {
        private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        private SqlCommand cmd = new SqlCommand();
        private SqlDataAdapter adapter = new SqlDataAdapter();
        public StockDetails()
        {
            InitializeComponent();
        }

        private void StockDetails_Load(object sender, EventArgs e)
        {
            grid();
        }
        public void grid()
        {
            cmd.Connection = conn;
            cmd.CommandText = "SELECT item.CODE, item.DESC_ENG, [TYPE], [GROUP], CATEGORY, TRADEMARK, (tblStock.Cost_price * Qty) as Stock FROM INV_ITEM_DIRECTORY as item LEFT JOIN tblStock ON item.CODE = tblStock.Item_id";
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter Adap2 = new SqlDataAdapter();
            Adap2.SelectCommand = cmd;
            DataTable dt1 = new DataTable();
            Adap2.Fill(dt1);
            dgItems.DataSource = dt1;
                    
        }
    }
}
