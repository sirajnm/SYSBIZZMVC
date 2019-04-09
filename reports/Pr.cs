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
    public partial class Pr : Form
    {
        public Pr()
        {
            InitializeComponent();
        }

        private void Pr_Load(object sender, EventArgs e)
        {
            //this.Sale_ProfitBindingSource.Fill(this.fastmoving.Fastmovingwithdate, grp, tm, cat, doc, sup, typ, datestart, dateend);
           
            //this
            //this.Sale_Profittab.Fill(this.NewPurchase.DataTable1, doc, grp, typ, cat, tm);

            this.reportViewer1.RefreshReport();
        }
    }
}
