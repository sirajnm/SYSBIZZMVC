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

namespace Sys_Sols_Inventory.Manufacture
{
    public partial class frmProductionMovement : Form
    {
        public frmProductionMovement()
        {
            InitializeComponent();
        }
        productionMovementReport pmr = new productionMovementReport();
        BindingSource source = new BindingSource();

        private void dataGridItem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            source.DataSource = pmr.getData();
            dataGridItem.DataSource = source;
            dataGridItem.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
           dataGridItem.Columns["NAME"].FillWeight = 750;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            source.Filter = string.Format("NAME LIKE '%{0}%' or BATCH LIKE '%{0}%'", textBox1.Text.Replace("'", "''").Replace("*", "[*]"));
        }

        private void frmProductionMovement_Load(object sender, EventArgs e)
        {
            btnSave.PerformClick();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridItem_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            e.AdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None;
            if (e.RowIndex < 1 || e.ColumnIndex < 0)
                return;
            if (e.ColumnIndex < 2)
            {
                if (IsTheSameCellValue(e.ColumnIndex, e.RowIndex))
                {
                    e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
                }
                else
                {
                    e.AdvancedBorderStyle.Top = dataGridItem.AdvancedCellBorderStyle.Top;
                }
            }
            else
            {
                e.AdvancedBorderStyle.Top = dataGridItem.AdvancedCellBorderStyle.Top;
            }

           
        }

        bool IsTheSameCellValue(int column, int row)
        {
            DataGridViewCell cell1 = dataGridItem[column, row];
            DataGridViewCell cell2 = dataGridItem[column, row - 1];
            if (cell1.Value == null || cell2.Value == null)
            {
                return false;
            }
            return cell1.Value.ToString() == cell2.Value.ToString();
        }

        private void dataGridItem_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex == 0 || e.ColumnIndex > 1)
                return;
            if (IsTheSameCellValue(e.ColumnIndex, e.RowIndex))
            {
                e.Value = "";
                e.FormattingApplied = true;
            }
        }

        private void dataGridItem_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = 0;
            string code,inward,outward;
            if (dataGridItem.Rows.Count > 0)
            {
                string batch = dataGridItem.Rows[dataGridItem.CurrentCell.RowIndex].Cells["BATCH"].Value.ToString();
                i = dataGridItem.CurrentCell.RowIndex;
                if (dataGridItem.Rows[dataGridItem.CurrentCell.RowIndex].Cells["CODE"].Value.ToString() == "")
                {
                    for (i = dataGridItem.CurrentCell.RowIndex; i > 0; i--)
                    {
                        if (dataGridItem.Rows[i].Cells["CODE"].Value.ToString() != "")
                        {
                            break;
                        }
                    }
                }

                code = dataGridItem.Rows[i].Cells["CODE"].Value.ToString();
                inward = dataGridItem.Rows[i].Cells["INWARD"].Value.ToString();
                outward = dataGridItem.Rows[i].Cells["OUTWARD"].Value.ToString();
                Manufacture.IOHistory io = new IOHistory(code, batch,inward,outward);
                io.ShowDialog();
            }
        }
    }
}
