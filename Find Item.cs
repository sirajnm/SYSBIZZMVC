using ComponentFactory.Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sys_Sols_Inventory
{
    public partial class Find_Item : Form
    {
        //private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        //private SqlCommand cmd = new SqlCommand();
        //private SqlDataAdapter adapter = new SqlDataAdapter();
        private DataTable table = new DataTable();
        KryptonTextBox tb;

        private BindingSource source = new BindingSource();
        public Find_Item()
        {
            InitializeComponent();
        }

        public void bindgridview()
        {
            //cmd.Connection = conn;
            string query = "SELECT  INV_ITEM_DIRECTORY.CODE AS [BarCode], INV_ITEM_DIRECTORY.DESC_ENG AS [Item Name],"+
                " RateChange.R_Id AS rid,RateChange.Sale_Price AS [sale price],RateChange.Price as [Purchase price] "+
                " FROM INV_ITEM_DIRECTORY "+
                " LEFT OUTER JOIN  RateChange ON INV_ITEM_DIRECTORY.CODE = RateChange.Item_code and RateChange.Qty<>0 "+
                " LEFT OUTER JOIN  INV_ITEM_DIRECTORY_UNITS ON INV_ITEM_DIRECTORY.CODE = INV_ITEM_DIRECTORY_UNITS.ITEM_CODE "; 
          
            DataTable dt = new DataTable();

            //adapter.SelectCommand = cmd;
            
            
            //adapter.Fill(dt);
            dt = Model.DbFunctions.GetDataTable(query);
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                string bar = dt.Rows[j]["BarCode"].ToString() + dt.Rows[j]["rid"].ToString();
                dt.Rows[j]["BarCode"] = bar;
            }
           
            source.DataSource = dt;
            
            dataGridItem.DataSource = source;



        }
        private void txt_Itemname_TextChanged(object sender, EventArgs e)
        {
            if (txt_Itemname.Text == "" && Txt_Itemcode.Text == "")
            {
                dataGridItem.Visible = false;
                dataGridItem.Columns["rid"].Visible = false;
            }
            else
            {
                dataGridItem.Columns["rid"].Visible = false;
                dataGridItem.Visible = true;
                source.Filter = string.Format("[BarCode] LIKE '%{0}%' and [Item Name] like '%{1}%'", Txt_Itemcode.Text, txt_Itemname.Text);
            }
        }

        private void txt_Itemname_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyData == Keys.Down)
            {
                tb = sender as KryptonTextBox;

                if (dataGridItem.Visible == true)
                    dataGridItem.Focus();
            }
        }

        private void Txt_Itemcode_TextChanged(object sender, EventArgs e)
        {
            if (txt_Itemname.Text == "" && Txt_Itemcode.Text == "")
            {
                dataGridItem.Visible = false;
            }
            else
            {
                dataGridItem.Visible = true;
                source.Filter = string.Format("[BarCode] LIKE '%{0}%' and [Item Name] like '%{1}%'", Txt_Itemcode.Text, txt_Itemname.Text);
            }
        }

        private void Txt_Itemcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Down)
            {
                tb = sender as KryptonTextBox;

                if (dataGridItem.Visible == true)
                    dataGridItem.Focus();
            }
            else if (e.KeyData == Keys.Enter)
            {
                txt_Itemname.Focus();
            }
        }

        private void Find_Item_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {

           
            if (keyData == (Keys.Escape))
            {
                this.Close();

                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Find_Item_Load(object sender, EventArgs e)
        {
            bindgridview();
            dataGridItem.Columns[0].Width = 60;
            dataGridItem.Columns[1].Width = 200;
            dataGridItem.Columns[1].Width = 200;
        }

        private void dataGridItem_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string a = "12";
                SalesQ sa = new SalesQ("");
                sa.BARCODE.Text = dataGridItem.CurrentRow.Cells["BarCode"].Value.ToString();
            }
            catch { }
        }
    }
}
