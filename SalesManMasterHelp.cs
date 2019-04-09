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
    public partial class SalesManMasterHelp : Form
    {
        private BindingSource source = new BindingSource();
        public DataGridViewCellCollection c;
        Class.Employee Emp = new Class.Employee();
        public SalesManMasterHelp()
        {
            InitializeComponent();
        }

        public void BindEmployees()
        {
            try
            {
                DataTable dt = new DataTable();

                dt = Emp.GetSalesman();
                source.DataSource = dt;
                dgEmployee.DataSource = source;
                //   dataGridView1.Rows[0].Visible = false;
            }

            catch
            {
            }
        }

        private void SalesManMasterHelp_Load(object sender, EventArgs e)
        {
            BindEmployees();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (dgEmployee.Rows.Count > 0 && dgEmployee.CurrentRow != null)
            {
                c = dgEmployee.CurrentRow.Cells;
            }
        }

        private void dgEmployee_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnOK.PerformClick();
            }
        }

        private void dgEmployee_DoubleClick(object sender, EventArgs e)
        {
            btnOK.PerformClick();
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            if (txtFilter.Text == "")
            {
                source.Filter = "";
            }
            else
            {
                source.Filter = "Name" + " LIKE '" + txtFilter.Text + "%'";
            }
        }

      

    }
}
