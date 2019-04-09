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
    public partial class FrmProductManufacture : Form
    {

        private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        private SqlCommand cmd = new SqlCommand();
        private SqlDataAdapter adapter = new SqlDataAdapter();
        private DataTable table = new DataTable();
        private DataTable tableBatch = new DataTable();

        private SqlConnection connUnit = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        private SqlCommand cmdUnit = new SqlCommand();
        private SqlDataAdapter adapterUnit = new SqlDataAdapter();
        private DataTable tableUnit = new DataTable();

        public FrmProductManufacture()
        {
            InitializeComponent();

            cmd.Connection = conn;
            adapter.SelectCommand = cmd;

            cmdUnit.Connection = connUnit;
            adapterUnit.SelectCommand = cmdUnit;
        }

        private void FrmProductManufacture_Load(object sender, EventArgs e)
        {
            cmd.CommandText = "SELECT CODE, DESC_ENG FROM INV_ITEM_DIRECTORY WHERE ITEM_TYPE = 'ITEM' ORDER BY DESC_ENG ASC";
            adapter.Fill(table);
            DataRow row = table.NewRow();
            row[0] = 0;
            row[1] = "----- SELECT PRODUCT -----";
            table.Rows.InsertAt(row, 0);
            cmbProduct.DataSource = table;
            cmbProduct.DisplayMember = "DESC_ENG";
            cmbProduct.ValueMember = "CODE";
        }

        private void cmbProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!Convert.ToString(cmbProduct.SelectedValue).Equals("System.Data.DataRowView"))
            {
                loadProductBatches();
                loadProductUnits();
                loadMaterials();
            }
        }

        private void loadProductBatches()
        {
            cmd.CommandText = "SELECT DISTINCT(batch_id), Qty FROM tblStock WHERE Item_id = @item_id";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@item_id", cmbProduct.SelectedValue);
            tableBatch.Clear();
            adapter.Fill(tableBatch);
            cmbBatch.DataSource = tableBatch;
            cmbBatch.DisplayMember = "batch_id";
            cmbBatch.ValueMember = "Qty";
        }

        private void loadProductUnits()
        {
            cmdUnit.CommandText = "SELECT UNIT_CODE, PACK_SIZE FROM INV_ITEM_DIRECTORY_UNITS WHERE ITEM_CODE = @item_code";
            cmdUnit.Parameters.Clear();
            cmdUnit.Parameters.AddWithValue("@item_code", cmbProduct.SelectedValue);
            tableUnit.Clear();
            adapterUnit.Fill(tableUnit);
            cmbUnit.DataSource = tableUnit;
            cmbUnit.DisplayMember = "UNIT_CODE";
            cmbUnit.ValueMember = "PACK_SIZE";
        }

        private void loadMaterials()
        {
            cmd.CommandText = "SELECT item_materials.*, DESC_ENG as material_name FROM item_materials LEFT JOIN INV_ITEM_DIRECTORY ON item_materials.material_code = INV_ITEM_DIRECTORY.CODE WHERE item_code = @item_code";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@item_code", cmbProduct.SelectedValue);
            conn.Open();
            SqlDataReader r = cmd.ExecuteReader();
            dgvMaterials.Rows.Clear();
            while (r.Read())
            {
                dgvMaterials.Rows.Add((dgvMaterials.RowCount + 1), r["material_code"], r["material_name"], "", r["material_uom"], "", "", "", r["material_qty"]);
            }
            conn.Close();
        }

        private void dgvMaterials_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                Rectangle rect = dgvMaterials.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                cmbMaterialBatch.Location = new Point(dgvMaterials.Location.X + rect.X, dgvMaterials.Location.Y + rect.Y);
                cmd.CommandText = "SELECT DISTINCT(batch_id) FROM tblStock WHERE Item_id = @item_code";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@item_code", dgvMaterials.CurrentRow.Cells["cMaterialCode"].Value);
                DataTable tableMaterialBatch = new DataTable();
                adapter.Fill(tableMaterialBatch);
                DataRow row = tableMaterialBatch.NewRow();
                row[0] = "--Select Batch--";
                tableMaterialBatch.Rows.InsertAt(row, 0);
                cmbMaterialBatch.DataSource = tableMaterialBatch;
                cmbMaterialBatch.DisplayMember = "batch_id";
                cmbMaterialBatch.ValueMember = "batch_id";
                cmbMaterialBatch.Visible = true;
                cmbMaterialBatch.SelectedIndexChanged -= cmbMaterialBatch_SelectedIndexChanged;
                cmbMaterialBatch.SelectedIndexChanged += cmbMaterialBatch_SelectedIndexChanged;
            }
        }

        void cmbMaterialBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dgvMaterials.CurrentRow != null)
            {
                dgvMaterials.CurrentCell.Value = cmbMaterialBatch.Text;
            }
        }

        private void cmbMaterialBatch_Leave(object sender, EventArgs e)
        {
            cmbMaterialBatch.Visible = false;
        }
    }
}
