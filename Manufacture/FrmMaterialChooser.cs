using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sys_Sols_Inventory.Manufacture
{
    public partial class FrmMaterialChooser : Form
    {
        private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        private SqlCommand cmd = new SqlCommand();
        private SqlDataAdapter adapter = new SqlDataAdapter();
        private DataTable table = new DataTable();
        public DataGridViewSelectedRowCollection rows;

        public FrmMaterialChooser()
        {
            InitializeComponent();
            cmd.Connection = conn;
            adapter.SelectCommand = cmd;
        }

        private void FrmMaterialChooser_Load(object sender, EventArgs e)
        {
            cmd.CommandText = "SELECT CODE, DESC_ENG, TYPE, [GROUP], CATEGORY, TRADEMARK, SUPPLIER, UNIT_CODE AS UOM FROM INV_ITEM_DIRECTORY LEFT JOIN INV_ITEM_DIRECTORY_UNITS ON INV_ITEM_DIRECTORY.CODE = INV_ITEM_DIRECTORY_UNITS.ITEM_CODE AND PACK_SIZE = '1' WHERE ITEM_TYPE != 'ITEM'";
            adapter.Fill(table);
            dgvMaterials.DataSource = table;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            rows = dgvMaterials.SelectedRows;
        }
    }
}
