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
    public partial class DummyForm : Form
    {
        public DummyForm()
        {
            InitializeComponent();
        }

        private void DummyForm_Load(object sender, EventArgs e)
        {

            this.INV_ITEM_DIRECTORYTableAdapter.Connection.ConnectionString = Properties.Settings.Default.ConnectionStrings;
            // TODO: This line of code loads data into the 'dummyDataset.INV_ITEM_DIRECTORY' table. You can move, or remove it, as needed.
            this.INV_ITEM_DIRECTORYTableAdapter.Fill(this.dummyDataset.INV_ITEM_DIRECTORY);

            this.reportViewer1.RefreshReport();
        }
    }
}
