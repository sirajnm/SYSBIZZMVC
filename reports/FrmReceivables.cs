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
    public partial class FrmReceivables : Form
    {
        public FrmReceivables()
        {
            InitializeComponent();
        }

        private void FrmReceivables_Load(object sender, EventArgs e)
        {
            this.ReceivablesTableAdapter.Connection.ConnectionString = Properties.Settings.Default.ConnectionStrings;
            // TODO: This line of code loads data into the 'dsReceivables.Receivables' table. You can move, or remove it, as needed.
            this.ReceivablesTableAdapter.Fill(this.dsReceivables.Receivables);

            this.reportViewer1.RefreshReport();
        }
    }
}
