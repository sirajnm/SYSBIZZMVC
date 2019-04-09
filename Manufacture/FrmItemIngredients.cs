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
    public partial class FrmItemIngredients : Form
    {
        #region data and connection variable declaration
        private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        private SqlCommand cmd = new SqlCommand();

        private SqlConnection connItem = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        private SqlCommand cmdItem = new SqlCommand();
        private SqlDataAdapter adapterItem = new SqlDataAdapter();
        DataTable tableItem = new DataTable();

        private SqlConnection connUnit = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        private SqlCommand cmdUnit = new SqlCommand();
        private SqlDataAdapter adapterUnit = new SqlDataAdapter();
        DataTable tableUnit = new DataTable();


        private SqlConnection connSuggest = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        private SqlCommand cmdSuggest = new SqlCommand();
        private SqlDataAdapter adapterSuggest = new SqlDataAdapter();
        DataTable tableSuggest = new DataTable();

        private SqlConnection connMaterialUnit = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        private SqlCommand cmdMaterialUnit = new SqlCommand();
        private SqlDataAdapter adapterMaterialUnit = new SqlDataAdapter();
        DataTable tableMaterialUnit = new DataTable();
        #endregion

        public FrmItemIngredients()
        {
            InitializeComponent();

            cmd.Connection = conn;

            cmdItem.Connection = connItem;
            adapterItem.SelectCommand = cmdItem;

            cmdUnit.Connection = connUnit;
            adapterUnit.SelectCommand = cmdUnit;

            cmdSuggest.Connection = connSuggest;
            adapterSuggest.SelectCommand = cmdSuggest;

            cmdMaterialUnit.Connection = connMaterialUnit;
            adapterMaterialUnit.SelectCommand = cmdMaterialUnit;
            cmbMaterialUnit.DataSource = tableMaterialUnit;
        }

        private void FrmItemIngredients_Load(object sender, EventArgs e)
        {
            LoadItems();
            dgvMaterials.Rows.Add();
            dgvMaterials[0, 0].Value = "1";
            LoadMaterials();
            
        }

        private void LoadMaterials()
        {
            cmdItem.CommandText = "SELECT CODE, DESC_ENG FROM INV_ITEM_DIRECTORY WHERE ITEM_TYPE != 'ITEM'";
            adapterItem.Fill(tableSuggest);
            dgvSuggestItems.DataSource = tableSuggest;
        }

        private void LoadItems()
        {
            cmdItem.CommandText = "SELECT CODE, DESC_ENG FROM INV_ITEM_DIRECTORY WHERE ITEM_TYPE = 'ITEM'";
            adapterItem.Fill(tableItem);
            DataRow row = tableItem.NewRow();
            row[0] = 0;
            row[1] = "--Select Item--";
            tableItem.Rows.InsertAt(row, 0);
            cmbItem.DataSource = tableItem;
            cmbItem.DisplayMember = "DESC_ENG";
            cmbItem.ValueMember = "CODE";
        }

        private void FocusRow()
        {
            foreach (DataGridViewRow row in dgvSuggestItems.Rows)
            {
                if (Convert.ToString(row.Cells[1].Value).ToLower().StartsWith(txtSuggestItem.Text.ToLower()))
                {
                    dgvSuggestItems.CurrentCell = row.Cells[1];
                    break;
                }
            }
        }

        private void cmbItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbItem.SelectedIndex == 0)
            {
                tableUnit.Rows.Clear();
                if (tableUnit.Columns.Count == 0)
                {
                    DataColumn column1 = new DataColumn("UNIT_CODE");
                    DataColumn column2 = new DataColumn("PACK_SIZE");
                    tableUnit.Columns.Add(column1);
                    tableUnit.Columns.Add(column2);
                }
                DataRow row = tableUnit.NewRow();
                row[0] = "--Select Item--";
                row[1] = 0;
                tableUnit.Rows.Add(row);
                return;
            }
            cmdUnit.CommandText = "SELECT UNIT_CODE, PACK_SIZE FROM INV_ITEM_DIRECTORY_UNITS WHERE ITEM_CODE = @item_code";
            cmdUnit.Parameters.Clear();
            cmdUnit.Parameters.AddWithValue("@item_code", cmbItem.SelectedValue);
            tableUnit.Rows.Clear();
            adapterUnit.Fill(tableUnit);
            DataRow row1 = tableUnit.NewRow();
            row1[0] = "--Select Unit--";
            row1[1] = 0;
            tableUnit.Rows.InsertAt(row1, 0);
            cmbUnit.DataSource = tableUnit;
            cmbUnit.DisplayMember = "UNIT_CODE";
            cmbUnit.ValueMember = "PACK_SIZE";
            cmbUnit.SelectedIndex = 0;

            txtQty.Text = "1";

            conn.Open();
            cmd.CommandText = "SELECT material_code, desc_eng as material_name, pack_size, unit_code FROM item_ingredients  LEFT JOIN INV_ITEM_DIRECTORY on item_ingredients.material_code = inv_item_directory.code LEFT JOIN INV_ITEM_DIRECTORY_UNITS ON INV_ITEM_DIRECTORY.CODE = INV_ITEM_DIRECTORY_UNITS.ITEM_CODE AND PACK_SIZE = '1'";
            conn.Close();
        }

        private void dgvMaterials_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            Rectangle location = dgvMaterials.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
            Point point = new Point(dgvMaterials.Location.X + location.X, dgvMaterials.Location.Y + location.Y);
            if (e.ColumnIndex == 2) //product name
            {
                pnlSuggest.Visible = true;
                pnlSuggest.Location = point;
                txtSuggestItem.Text = Convert.ToString(dgvMaterials[e.ColumnIndex, e.RowIndex].Value);
                txtSuggestItem.Focus();
                FocusRow();
            }
            else if (e.ColumnIndex == 4 && dgvMaterials.CurrentRow.Cells[1].Value != null) //unit name
            {
                cmbMaterialUnit.Visible = true;
                cmbMaterialUnit.Location = point;
                cmdMaterialUnit.CommandText = "SELECT UNIT_CODE, PACK_SIZE FROM INV_ITEM_DIRECTORY_UNITS WHERE ITEM_CODE = @item_code";
                cmdMaterialUnit.Parameters.Clear();
                cmdMaterialUnit.Parameters.AddWithValue("@item_code", dgvMaterials.CurrentRow.Cells["cItemId"].Value);
                tableMaterialUnit.Rows.Clear();
                adapterMaterialUnit.Fill(tableMaterialUnit);
                cmbMaterialUnit.DisplayMember = "UNIT_CODE";
                cmbMaterialUnit.ValueMember = "PACK_SIZE";
                if (dgvMaterials.CurrentRow.Cells[4].Value != null)
	            {
                    cmbMaterialUnit.Text = Convert.ToString(dgvMaterials.CurrentRow.Cells["cUnit"].Value);
	            }
                else
                {
                    if (tableMaterialUnit.Rows.Count > 0)
                    {
                        cmbMaterialUnit.SelectedIndex = 0;
                    }
                }
                cmbMaterialUnit.Focus();
            }
            else if (e.ColumnIndex == 6)
            {
                txtMaterialQty.Visible = true;
                txtMaterialQty.Location = point;
                txtMaterialQty.Width = dgvMaterials[e.ColumnIndex, e.RowIndex].Size.Width;
                txtMaterialQty.Text = Convert.ToString(dgvMaterials.CurrentRow.Cells["cQty"].Value);
                txtMaterialQty.Focus();
            }
        }

        private void txtSuggestItem_Leave(object sender, EventArgs e)
        {
            pnlSuggest.Visible = false;
        }

        private void txtSuggestItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (dgvSuggestItems.RowCount > 0)
            {
                switch (e.KeyCode)
                {
                    case Keys.Down:
                        int row = (dgvSuggestItems.CurrentRow.Index + 1) % (dgvSuggestItems.RowCount);
                        dgvSuggestItems.CurrentCell = dgvSuggestItems.Rows[row].Cells[1];
                        break;
                    case Keys.Up:
                        int row1 = (dgvSuggestItems.CurrentRow.Index - 1) % (dgvSuggestItems.RowCount);
                        dgvSuggestItems.CurrentCell = dgvSuggestItems.Rows[(row1 > -1) ? row1 : (dgvSuggestItems.RowCount - 1)].Cells[1];
                        break;
                    case Keys.Enter:
                        dgvMaterials.CurrentRow.Cells[dgvMaterials.CurrentCell.ColumnIndex - 1].Value = dgvSuggestItems.CurrentRow.Cells[dgvSuggestItems.CurrentCell.ColumnIndex - 1].Value;
                        dgvMaterials.CurrentCell.Value = dgvSuggestItems.CurrentCell.Value;
                        dgvMaterials.CurrentCell = dgvMaterials.CurrentRow.Cells[4];
                        pnlSuggest.Visible = false;
                        Common.preventDingSound(e);
                        break;
                    case Keys.Escape:
                        dgvMaterials.CurrentCell = dgvMaterials.CurrentRow.Cells[0];
                        pnlSuggest.Visible = false;
                        Common.preventDingSound(e);
                        break;
                }
            }
        }

        private void txtSuggestItem_TextChanged(object sender, EventArgs e)
        {
            FocusRow();
        }

        private void cmbMaterialUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!Convert.ToString(cmbMaterialUnit.SelectedValue).Equals("System.Data.DataRowView"))
            {
                dgvMaterials.CurrentRow.Cells["cPackSize"].Value = cmbMaterialUnit.SelectedValue;
                dgvMaterials.CurrentRow.Cells["cUnit"].Value = cmbMaterialUnit.Text;
            }
            else
            {
                dgvMaterials.CurrentRow.Cells["cPackSize"].Value = tableMaterialUnit.Rows[0][1];
                dgvMaterials.CurrentRow.Cells["cUnit"].Value = tableMaterialUnit.Rows[0][0];
            }
        }

        private void cmbMaterialUnit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbMaterialUnit.Visible = false;
                dgvMaterials.CurrentRow.Cells["cPackSize"].Value = cmbMaterialUnit.SelectedValue;
                dgvMaterials.CurrentRow.Cells["cUnit"].Value = cmbMaterialUnit.Text;
                dgvMaterials.CurrentCell = dgvMaterials.CurrentRow.Cells["cQty"];
                GetCostPrice();
                CalculateTotal();
                Common.preventDingSound(e);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                cmbMaterialUnit.Visible = false;
                dgvMaterials.CurrentCell = dgvMaterials.CurrentRow.Cells["cSlNo"];
                dgvMaterials.Focus();
                Common.preventDingSound(e);
            }
        }

        private void txtMaterialQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Common.preventDingSound(e);

                if (!isValidRow())
                {
                    MessageBox.Show("Incomplete Data!");
                    return;
                }

                if (dgvMaterials.CurrentRow.Index == (dgvMaterials.RowCount - 1))
                {
                    dgvMaterials.Rows.Add(new DataGridViewRow());
                    dgvMaterials["cSlNo", dgvMaterials.RowCount - 1].Value = dgvMaterials.RowCount; 
                }
                dgvMaterials.CurrentRow.Cells["cQty"].Value = txtMaterialQty.Text;
                dgvMaterials.CurrentCell = dgvMaterials.Rows[dgvMaterials.CurrentRow.Index + 1].Cells["cItemName"];
                txtMaterialQty.Visible = false;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Common.preventDingSound(e);
                dgvMaterials.CurrentCell = dgvMaterials.CurrentRow.Cells["cSlNo"];
                txtMaterialQty.Visible = false;
            }
            
        }

        private bool isValidRow()
        {
            DataGridViewCellCollection c = dgvMaterials.CurrentRow.Cells;
            if (!Convert.ToString(c[1].Value).Equals("") && !Convert.ToString(c[3].Value).Equals("") && !Convert.ToString(c[5].Value).Equals(""))
            {
                return true;
            }
            return false;
        }

        private void txtMaterialQty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dgvMaterials.CurrentRow.Cells["cQty"].Value = Convert.ToString(Convert.ToDouble(txtMaterialQty.Text));
            }
            catch (Exception)
            {
                dgvMaterials.CurrentRow.Cells["cQty"].Value = "0";
            }
            CalculateTotal();
        }

        private void GetCostPrice()
        {
            DataGridViewCellCollection c = dgvMaterials.CurrentRow.Cells;
            String itemId = Convert.ToString(c["cItemId"].Value);
            String unit = Convert.ToString(c["cUnit"].Value);

            cmd.CommandText = "SELECT ISNULL(AVG(PRICE), 0) FROM INV_ITEM_PRICE WHERE ITEM_CODE = @item_code AND SAL_TYPE = 'PUR' AND UNIT_CODE = @unit_code";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@item_code", itemId);
            cmd.Parameters.AddWithValue("@unit_code", unit);
            conn.Open();
            double value = Convert.ToDouble(cmd.ExecuteScalar());
            conn.Close();

            if (value > 0)
            {
                c["cCostPrice"].Value = value;
            }
            else
            {
                cmd.CommandText = "SELECT ISNULL(PRICE, 0) FROM INV_ITEM_PRICE_DF WHERE ITEM_CODE = @item_code AND SAL_TYPE = 'PUR' AND UNIT_CODE = @unit_code";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@item_code", itemId);
                cmd.Parameters.AddWithValue("@unit_code", unit);
                conn.Open();
                double value2 = Convert.ToDouble(cmd.ExecuteScalar());
                conn.Close();
                c["cCostPrice"].Value = value2;
            }
        }

        private void CalculateTotal()
        {
            DataGridViewCellCollection c = dgvMaterials.CurrentRow.Cells;
            double costPrice = 0;
            double qty = 0;
            double total = 0;

            try
            {
                costPrice = Convert.ToDouble(c["cCostPrice"].Value);
            }
            catch (Exception) { }

            try
            {
                qty = Convert.ToDouble(c["cQty"].Value);
            }
            catch (Exception) { }

            total = costPrice * qty;

            c["cTotal"].Value = total;
        }

        private void cmbUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblCostPer.Text = "Cost Per " + cmbUnit.Text + ":";
        }

        private void txtQty_TextChanged(object sender, EventArgs e)
        {
            CalculateCostPerUnit();
        }

        private void CalculateCostPerUnit()
        {
            double qty = 0;
            try
            {
                qty = Convert.ToDouble(txtQty.Text);
            }
            catch (Exception){}

            double totalCostPriceOfMaterials = 0;
            foreach (DataGridViewRow row in dgvMaterials.Rows)
            {
                totalCostPriceOfMaterials += Convert.ToDouble(row.Cells["cTotal"].Value);
            }
            txtCostPricePerUnit.Text = Convert.ToString(Math.Round(totalCostPriceOfMaterials / qty, 2));
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var itemQty = Convert.ToDouble(cmbUnit.SelectedValue) * Convert.ToDouble(txtQty.Text);
            conn.Open();
            cmd.CommandText = "DELETE FROM item_ingredients WHERE item_code = @item_code";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@item_code", cmbItem.SelectedValue);
            cmd.ExecuteNonQuery();

            cmd.CommandText = "INSERT INTO item_ingredients(item_code, material_code, material_qty)";

            for (int i = 0; i < dgvMaterials.RowCount - 1; i++)
            {
                DataGridViewCellCollection c = dgvMaterials.Rows[i].Cells;

                double materialQty = Convert.ToDouble(c["cPackSize"].Value) * Convert.ToDouble(c["cQty"].Value);
                double materialPerUnit = materialQty/itemQty;

                cmd.CommandText += " SELECT '"+cmbItem.SelectedValue+"', '"+c["cItemId"].Value+"', '"+materialPerUnit+"' UNION ALL ";
            }
            cmd.CommandText = cmd.CommandText.Substring(0, cmd.CommandText.Length - 10);
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Materials saved!");
            btnClear.PerformClick();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            cmbItem.SelectedIndex = 0;
            txtQty.Text = "";
            txtCostPricePerUnit.Text = "";
            lblCostPer.Text = "Cost Price Per:";
            dgvMaterials.Rows.Clear();
        }
    }
}
