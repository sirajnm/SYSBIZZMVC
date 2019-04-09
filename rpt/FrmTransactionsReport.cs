using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sys_Sols_Inventory.rpt
{
    public partial class FrmTransactionsReport : Form
    {
        private int? group_id;
        public FrmTransactionsReport()
        {
            InitializeComponent();
        }

        public FrmTransactionsReport(int group_id)
            : this()
        {
            this.group_id = group_id;
        }

        private void FrmReport_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dsReport.GetTransactionsWithDetails' table. You can move, or remove it, as needed.
            this.GetTransactionsWithDetailsTableAdapter.Connection = Model.DbFunctions.GetConnection();
            this.GetTransactionsWithDetailsTableAdapter.Fill(this.dsReport.GetTransactionsWithDetails, group_id);
            this.rptViewer.RefreshReport();
        }
    }
}
