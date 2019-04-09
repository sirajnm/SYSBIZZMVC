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
    public partial class FrmProductMaterials : Form
    {

        private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        private SqlCommand cmd = new SqlCommand();
        private SqlDataAdapter adapter = new SqlDataAdapter();
        private DataTable table = new DataTable();

        private SqlConnection connUnit = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        private SqlCommand cmdUnit = new SqlCommand();
        private SqlDataAdapter adapterUnit = new SqlDataAdapter();
        private DataTable tableUnit = new DataTable();

        public FrmProductMaterials()
        {
            InitializeComponent();
            cmd.Connection = conn;
            adapter.SelectCommand = cmd;

            cmdUnit.Connection = connUnit;
            adapterUnit.SelectCommand = cmdUnit;
        }

        private void FrmProductMaterials_Load(object sender, EventArgs e)
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
                tableUnit.Clear();
                cmdUnit.CommandText = "SELECT UNIT_CODE, PACK_SIZE FROM INV_ITEM_DIRECTORY_UNITS WHERE ITEM_CODE = @item_code ORDER BY PACK_SIZE ASC";
                cmdUnit.Parameters.Clear();
                cmdUnit.Parameters.AddWithValue("@item_code", cmbProduct.SelectedValue);
                adapterUnit.Fill(tableUnit);
                cmbUnit.DataSource = tableUnit;
                cmbUnit.DisplayMember = "UNIT_CODE";
                cmbUnit.ValueMember = "PACK_SIZE";

                if (tableUnit.Rows.Count > 0)
                {
                    cmbUnit.SelectedIndex = 0;
                }

                cmd.CommandText = "SELECT item_materials.*, DESC_ENG as material_name FROM item_materials LEFT JOIN INV_ITEM_DIRECTORY ON item_materials.material_code = INV_ITEM_DIRECTORY.CODE WHERE item_code = @item_code";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@item_code", cmbProduct.SelectedValue);
                conn.Open();
                SqlDataReader r = cmd.ExecuteReader();
                dgvMaterials.Rows.Clear();
                while (r.Read())
                {
                    dgvMaterials.Rows.Add((dgvMaterials.RowCount + 1), r["material_code"], r["material_name"], r["material_uom"], r["material_qty"], r["material_qty"]);
                }
                conn.Close();
                if (dgvMaterials.RowCount > 0)
                {
                    txtQty.Text = "1";
                }
            }
            
        }

        private void btnChooseMaterials_Click(object sender, EventArgs e)
        {
            FrmMaterialChooser frm = new FrmMaterialChooser();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (frm.rows.Count > 0)
                {
                    foreach (DataGridViewRow row in frm.rows)
                    {
                        DataGridViewCellCollection c = row.Cells;
                        dgvMaterials.Rows.Add((dgvMaterials.RowCount+1), c["CODE"].Value, c["DESC_ENG"].Value, c["UOM"].Value);
                    }
                }
                else
                {
                    MessageBox.Show("No rows selected!");
                }
            }
        }

        private void txtQty_TextChanged(object sender, EventArgs e)
        {
            double qty = 0;
            try
            {
                qty = Convert.ToDouble(txtQty.Text);
            }
            catch (Exception)
            {
            }

            double pack_size = 0;
            try
            {
                pack_size = Convert.ToDouble(cmbUnit.SelectedValue);
            }
            catch (Exception)
            {
            }

            double totalQty = qty * pack_size;

            foreach (DataGridViewRow row in dgvMaterials.Rows)
            {
                DataGridViewCellCollection c = row.Cells;
                double materialQty = 0;
                try
                {
                    materialQty = Convert.ToDouble(c["cMaterialQty"].Value);
                }
                catch (Exception)
                {
                }

                c["cMaterialQtyPerProduct"].Value = (totalQty > 0)? Math.Round(materialQty / totalQty, 5) : 0;
            }
        }

        private void dgvMaterials_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridViewTextBoxEditingControl control = (e.Control as DataGridViewTextBoxEditingControl);
            control.TextChanged -= control_TextChanged;
            control.TextChanged += control_TextChanged;
        }

        void control_TextChanged(object sender, EventArgs e)
        {
            double qty = 0;
            try
            {
                qty = Convert.ToDouble((sender as DataGridViewTextBoxEditingControl).Text);
            }
            catch (Exception)
            {
            }

            double productQty = 0;
            try
            {
                productQty = Convert.ToDouble(txtQty.Text);
            }
            catch (Exception)
            {
            }

            dgvMaterials.CurrentRow.Cells["cMaterialQtyPerProduct"].Value = (productQty > 0)? Math.Round((qty/productQty) , 5) : 0;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            cmd.CommandText = "DELETE FROM item_materials WHERE item_code = @item_code";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@item_code", cmbProduct.SelectedValue);
            conn.Open();
            cmd.ExecuteNonQuery();

            cmd.CommandText = "INSERT INTO item_materials(item_code, material_code, material_uom, material_qty) ";
            foreach (DataGridViewRow row in dgvMaterials.Rows)
            {
                DataGridViewCellCollection c = row.Cells;
                cmd.CommandText += " SELECT '" + cmbProduct.SelectedValue +"', '" + c["cMaterialCode"].Value +"', '" + c["cMaterialUOM"].Value +"', '" + c["cMaterialQtyPerProduct"].Value +"' UNION ALL ";
            }
            cmd.CommandText = cmd.CommandText.Substring(0, cmd.CommandText.Length - 10);
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Materials Saved!");
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            cmbProduct.SelectedIndex = 0;
            txtQty.Text = "";
            dgvMaterials.Rows.Clear();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (cmbProduct.SelectedIndex > 0)
            {
                if (MessageBox.Show("Are you sure? you want to remove materials?", "Remove Materials", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    cmd.CommandText = "DELETE FROM item_materials WHERE item_code = @item_code";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@item_code", cmbProduct.SelectedValue);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Materials Removed!");
                    btnClear.PerformClick();
                }
                
            }
            else
            {
                MessageBox.Show("Select Product to Delete!");
            }
        }
    }
}
