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
   
    public partial class DiscountMasterHelp : Form
    {
        private BindingSource source = new BindingSource();
        Class.Discount Disc = new Class.Discount();
        public DataGridViewCellCollection c;
        public DiscountMasterHelp()
        {
            InitializeComponent();
        }

        private void DiscountMasterHelp_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = Disc.getproductDiscountDetailsMasterHelp();
            if (dt.Rows.Count > 0)
            {
                source.DataSource = dt;
                dgDiscountItems.DataSource = source;
            }
        }

        private void kryptonLabel20_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            if (txtFilter.Text == "")
            {
                source.Filter = "";
            }
            else
            {
                source.Filter = "Discount_Name" + " LIKE '" + txtFilter.Text + "%'";
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgDiscountItems.Rows.Count > 0 && dgDiscountItems.CurrentRow != null && MessageBox.Show("Are you sure?", "Item Deletion", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    Disc.Disc_Id = dgDiscountItems.CurrentRow.Cells[0].Value.ToString();
                    Disc.DeleteDiscountTypes();
                    MessageBox.Show("Item Deleted!");
                    dgDiscountItems.Rows.Remove(dgDiscountItems.CurrentRow);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
               
            }
        }

        private void dgDiscountItems_DoubleClick(object sender, EventArgs e)
        {
            btnOK.PerformClick();
        }

        private void dgDiscountItems_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnOK.PerformClick();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (dgDiscountItems.Rows.Count > 0 && dgDiscountItems.CurrentRow != null)
            {
                c = dgDiscountItems.CurrentRow.Cells;
            }
        }

        private void dgDiscountItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
