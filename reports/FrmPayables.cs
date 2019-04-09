using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sys_Sols_Inventory.reports
{
    public partial class FrmPayables : Form
    {
        public FrmPayables()
        {
            InitializeComponent();
        }

        private void FrmPayables_Load(object sender, EventArgs e)
        {
            this.PayablesTableAdapter.Connection.ConnectionString = Properties.Settings.Default.ConnectionStrings;
            // TODO: This line of code loads data into the 'dsPayables.Payables' table. You can move, or remove it, as needed.
            this.PayablesTableAdapter.Fill(this.dsPayables.Payables);
            // TODO: This line of code loads data into the 'dsReceivables.Receivables' table. You can move, or remove it, as needed.
            this.reportViewer1.RefreshReport();

        }
    }
}
