using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sys_Sols_Inventory.Manufacture
{
    public partial class Raw_Materials : Form
    {
        Model.RawMaterials RawMaterials = new Model.RawMaterials();

        public Raw_Materials()
        {
            InitializeComponent();
        }

        private void btn_brwsMFG_Click(object sender, EventArgs e)
        {
            Manufacture.ItemMasterHelp h = new Manufacture.ItemMasterHelp("MFG");
            if (h.ShowDialog() == DialogResult.OK && h.c != null)
            {
                MFG_ID.Text = Convert.ToString(h.c["CODE"].Value);
                MFG_NAME.Text = Convert.ToString(h.c["DESC_ENG"].Value);

                RawMaterials.MfgId = MFG_ID.Text;
                DataTable dt = RawMaterials.ExistingMfgByID();
                dgv_row.Rows.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dgv_row.Rows.Add();
                    MFG_QTY.Text = dt.Rows[i][1].ToString();
                    dgv_row.Rows[dgv_row.Rows.Count - 1].Cells[0].Value = dgv_row.Rows.Count;
                    dgv_row.Rows[dgv_row.Rows.Count - 1].Cells[1].Value = dt.Rows[i][2].ToString();
                    dgv_row.Rows[dgv_row.Rows.Count - 1].Cells[2].Value = dt.Rows[i][3].ToString();
                    dgv_row.Rows[dgv_row.Rows.Count - 1].Cells[3].Value = dt.Rows[i][4].ToString();
                }

                MFG_QTY.Focus();
            }
        }

        private void btn_brwsRAW_Click(object sender, EventArgs e)
        {
            Manufacture.ItemMasterHelp h = new Manufacture.ItemMasterHelp("RAW");
            if (h.ShowDialog() == DialogResult.OK && h.c != null)
            {
                RAW_ID.Text = Convert.ToString(h.c["CODE"].Value);
                RAW_NAME.Text = Convert.ToString(h.c["DESC_ENG"].Value);
                RAW_QTY.Focus();
            }
        }

        private void MFG_QTY_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void MFG_QTY_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_brwsRAW.Focus();
            }
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            if (RAW_ID.Text != "")
            {
                int rowindex = -1;
                if (dgv_row.Rows.Count > 0)
                {
                    try
                    {
                        DataGridViewRow row = dgv_row.Rows.Cast<DataGridViewRow>().Where(r => r.Cells["ItemCode"].Value.ToString().Equals(RAW_ID.Text)).First();
                        rowindex = row.Index;
                    }
                    catch
                    {
                        rowindex = -1;
                    }
                }

                if (rowindex == -1)
                {
                    dgv_row.Rows.Add();
                    dgv_row.Rows[dgv_row.Rows.Count - 1].Cells[0].Value = dgv_row.Rows.Count;
                    dgv_row.Rows[dgv_row.Rows.Count - 1].Cells[1].Value = RAW_ID.Text;
                    dgv_row.Rows[dgv_row.Rows.Count - 1].Cells[2].Value = RAW_NAME.Text;
                    dgv_row.Rows[dgv_row.Rows.Count - 1].Cells[3].Value = RAW_QTY.Text;
                }
                else
                {
                    if (dgv_row.Rows.Count > 0)
                        dgv_row.Rows[rowindex].Cells[3].Value = Convert.ToDecimal(RAW_QTY.Text) + Convert.ToDecimal(dgv_row.Rows[rowindex].Cells[3].Value);
                }

                RAW_ID.Text = "";
                RAW_NAME.Text = "";
                RAW_QTY.Text = "";
                btn_brwsRAW.Focus();

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MFG_ID.Text != "")
            {
                RawMaterials.MfgId = MFG_ID.Text;
                if (RawMaterials.ExistingMfg())
                {
                    RawMaterials.DeleteEntry();
                    Insert();
                    MessageBox.Show("Raw materials Updated successfully ..!", "Sysbizz");
                }
                else
                {
                    Insert();
                    MessageBox.Show("Raw materials Added successfully ..!", "Sysbizz");
                }
                Clear();
                btn_brwsMFG.Focus();
            }
            else
            {
                MessageBox.Show("Please select a Manufacturing Product ..!", "Sysbizz", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Insert()
        {
            for (int i = 0; i < dgv_row.Rows.Count; i++)
            {
                RawMaterials.MfgId = MFG_ID.Text;
                RawMaterials.MfgQty = Convert.ToDecimal(MFG_QTY.Text);
                RawMaterials.RawId = dgv_row.Rows[i].Cells["ItemCode"].Value.ToString();
                RawMaterials.RawQty = Convert.ToDecimal(dgv_row.Rows[i].Cells["Quantity"].Value.ToString());
                RawMaterials.Insert();
            }
        }

        public void Clear()
        {
            dgv_row.Rows.Clear();
            MFG_ID.Text = "";
            MFG_NAME.Text = "";
            MFG_QTY.Text = "";
            RAW_ID.Text = "";
            RAW_NAME.Text = "";
            RAW_QTY.Text = "";
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (dgv_row.Rows.Count > 0)
            {
                if (dgv_row.SelectedRows.Count > 0)
                {
                    dgv_row.Rows.RemoveAt(dgv_row.CurrentRow.Index);
                }
                else
                {
                    MessageBox.Show("Please select a item to remove ..!", "Sysbizz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void RAW_QTY_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (RAW_QTY.Text != "")
                {
                    btn_add.PerformClick();
                }
                else
                {
                    btnSave.Focus();
                }
            }
        }

        private void Raw_Materials_Load(object sender, EventArgs e)
        {
            btn_brwsMFG.Focus();
        }
    }
}