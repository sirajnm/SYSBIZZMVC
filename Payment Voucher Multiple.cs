using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sys_Sols_Inventory
{
    
    public partial class Payment_Voucher_Multiple : Form
    {
        Class.Ledgers Ledgers = new Class.Ledgers();

        public Payment_Voucher_Multiple()
        {            
            InitializeComponent();
            DataTable dt = Ledgers.Selectledger_GRid();
            dgv_ledger.DataSource = dt;
        }

        private void NOTES_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //if (dgDetail.Rows.Count < 1)
                //{
                    dgDetail.Rows.Add();
                    dgDetail.Focus();
                    dgDetail.CurrentCell = dgDetail.Rows[0].Cells[0];
                   
                //}
            }
        }
        bool Enter=false;
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Enter))
            {
                if (Enter)
                {
                    if (dgDetail.CurrentCell.ColumnIndex != 5)
                    {
                        dgDetail.Focus();
                        dgDetail.CurrentCell = dgDetail.CurrentRow.Cells[dgDetail.CurrentCell.ColumnIndex + 1];
                        dgDetail.BeginEdit(true);
                    }
                    else
                    {
                        dgDetail.Rows.Add();
                        dgDetail.CurrentCell = dgDetail.Rows[dgDetail.CurrentCell.RowIndex+1].Cells[0];
                        dgDetail.BeginEdit(true);
                    }
                    return true;
                }
                return false;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void dgDetail_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    e.Handled = true;
            //    if (dgDetail.CurrentCell.ColumnIndex != 5)
            //    {
            //        dgDetail.Focus();
            //        dgDetail.CurrentCell = dgDetail.CurrentRow.Cells[dgDetail.CurrentCell.ColumnIndex+1];
            //        dgDetail.BeginEdit(true);
            //    }
            //    else
            //    {
            //        dgDetail.Rows.Add();
            //        dgDetail.CurrentCell = dgDetail.Rows[dgDetail.CurrentCell.RowIndex+1].Cells[0];
            //        dgDetail.BeginEdit(true);
            //    }
            //}
            //e.Handled = false;
            if (dgDetail.CurrentCell.ColumnIndex == 3)
            {
                dgDetail.KeyPress += new KeyPressEventHandler(General.CellOnlyFloat);
            }
        }

        private void dgDetail_Enter(object sender, EventArgs e)
        {
            Enter = true;
        }

        private void dgDetail_Leave(object sender, EventArgs e)
        {
            Enter = false;
        }

        private void Payment_Voucher_Multiple_Load(object sender, EventArgs e)
        {
            dgDetail.Rows.Add();
        }

        private void dgDetail_Paint(object sender, PaintEventArgs e)
        {
            //dgDetail.CellBorderStyle = DataGridViewCellBorderStyle.None;

            //dgDetail.Rows[dgv_ledger.RowCount - 1].DefaultCellStyle = new DataGridViewCellStyle();
        }

        public void textBox_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void dgDetail_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            
        }

        private void dgDetail_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(General.OnlyFloat);
            if (dgDetail.CurrentCell.ColumnIndex == 3 || dgDetail.CurrentCell.ColumnIndex == 0) //Desired Column
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
                }

                if (dgDetail.CurrentCell.ColumnIndex == 0)
                {
                    int i = ((Convert.ToInt32(dgDetail.CurrentCell.RowIndex) * 22) + 47);
                    dgv_ledger.Location = new Point(dgDetail.Left + 15, i);
                    dgv_ledger.Columns[0].Width = 100;
                    dgv_ledger.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; 
                    dgv_ledger.Visible = true;                                                           
                }
            }

            if (dgDetail.CurrentCell.ColumnIndex == 0 || dgDetail.CurrentCell.ColumnIndex == 1)
            {
                DataGridViewTextBoxEditingControl tb = (DataGridViewTextBoxEditingControl)e.Control;
                tb.KeyPress += new KeyPressEventHandler(dataGridViewTextBox_KeyPress);
                e.Control.KeyPress += new KeyPressEventHandler(dataGridViewTextBox_KeyPress);
            }
        }

        private void dataGridViewTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            //when i press enter,bellow code never run?
            if (e.KeyChar == (char)Keys.Down)
            {
                if (dgv_ledger.Visible == true)
                {
                    int idx = dgv_ledger.SelectedRows[0].Index;
                    dgv_ledger.SelectedRows.Clear();
                    //dgv_ledger.SelectedRows. = dgv_ledger.CurrentCell.RowIndex + 1;
                }


            }
        }

        private void dgDetail_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
