using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sys_Sols_Inventory.Class
{
    public class DataGridViewExt : DataGridView
    {

        public event DataGridViewCellEventHandler CellButtonClick;

        public DataGridViewExt()
        {
            this.CellButtonClick += CellContentClicked;
        }

        private void CellContentClicked(System.Object sender, DataGridViewCellEventArgs e)
        {
            if (this.Columns[e.ColumnIndex].GetType() == typeof(DataGridViewButtonColumn))
            {
                if (this.Columns[e.ColumnIndex].GetType() == typeof(DataGridViewButtonColumn) && e.RowIndex >= 0) { CellButtonClick.Invoke(this, e); }
            }

        }

    }

}
