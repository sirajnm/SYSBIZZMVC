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

namespace Sys_Sols_Inventory.Manufacture
{
    public partial class FrmProductionBrowse : Form
    {
        //private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        //private SqlCommand cmd = new SqlCommand();
        //private SqlDataAdapter adapter = new SqlDataAdapter();
        private DataTable table = new DataTable();
        public String DocId { get; set; }
        public String PDate { get; set; }
        public FrmProductionBrowse()
        {
            InitializeComponent();
            //cmd.Connection = conn;
            //adapter.SelectCommand = cmd;
        }

        private void FrmProductionBrowse_Load(object sender, EventArgs e)
        {
            //cmd.CommandText = "SELECT Id, CONVERT(NVARCHAR(50), ProductionDate, 103) AS ProductionDate, ProductionCost, DamageCost, OtherExpense FROM ProductionMaster ORDER BY Id DESC";
            //string query = "SELECT Id,DESC_ENG AS Item_Name, CONVERT(NVARCHAR(50), ProductionDate, 103) AS ProductionDate, ProductionCost, DamageCost, OtherExpense FROM ProductionMaster LEFT OUTER JOIN (SELECT ProductionId,DESC_ENG FROM INV_ITEM_DIRECTORY INNER JOIN ProductionProducts ON INV_ITEM_DIRECTORY.CODE=ProductionProducts.ItemCode) AS DESCNAME ON DESCNAME.ProductionId=ProductionMaster.Id ORDER BY ProductionDate DESC";
           // string query = "SELECT Id,DESC_ENG AS Item_Name, CONVERT(NVARCHAR(50), ProductionDate, 103) AS ProductionDate, ProductionCost, DamageCost, OtherExpense,ProductionDate AS SystemDate FROM ProductionMaster LEFT OUTER JOIN (SELECT ProductionId,DESC_ENG FROM INV_ITEM_DIRECTORY INNER JOIN ProductionProducts ON INV_ITEM_DIRECTORY.CODE=ProductionProducts.ItemCode) AS DESCNAME ON DESCNAME.ProductionId=ProductionMaster.Id ORDER BY SystemDate DESC";
            string query = "SELECT Id,DESC_ENG AS Item_Name, CONVERT(NVARCHAR(50), ProductionDate, 103) AS ProductionDate, ProductionCost, DamageCost, OtherExpense,ProductionDate AS SystemDate FROM ProductionMaster INNER JOIN (SELECT ProductionId,DESC_ENG FROM INV_ITEM_DIRECTORY INNER JOIN ProductionProducts ON INV_ITEM_DIRECTORY.CODE=ProductionProducts.ItemCode) AS DESCNAME ON DESCNAME.ProductionId=ProductionMaster.Id ORDER BY SystemDate DESC";
            table = DbFunctions.GetDataTable(query);
            
            //adapter.Fill(table);
            dgvList.DataSource = table;
        }


        private void btnOk_Click(object sender, EventArgs e)
        {
            if (dgvList.CurrentRow != null)
            {
                DocId = Convert.ToString(dgvList.CurrentRow.Cells["Id"].Value);
                PDate = Convert.ToString(dgvList.CurrentRow.Cells["ProductionDate"].Value);
            }
        }

        private void dgvList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnOk.PerformClick();
            }
        }

        private void dgvList_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            btnOk.PerformClick();
        }
    }
}
