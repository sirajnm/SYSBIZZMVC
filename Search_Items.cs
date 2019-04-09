using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using ComponentFactory.Krypton.Toolkit;
using Sys_Sols_Inventory.Model;

namespace Sys_Sols_Inventory
{
    public partial class Search_Items : Form
    {
        //private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        //private SqlCommand cmd = new SqlCommand();
        //private SqlDataAdapter adapter = new SqlDataAdapter();
        private DataTable table = new DataTable();
        KryptonTextBox tb;
        
        private BindingSource source = new BindingSource();
        public Search_Items()
        {
            InitializeComponent();
        }

        private void txt_cname_TextChanged(object sender, EventArgs e)
        {
           
        }

        public void bindgridview()
        {
            string query = "";
            //cmd.Connection = conn;
            ////cmd.CommandText = "SELECT     INV_ITEM_DIRECTORY.CODE AS [Item Code], INV_ITEM_DIRECTORY_UNITS.BARCODE AS Barcode, INV_ITEM_DIRECTORY.DESC_ENG AS [Item Name],INV_ITEM_DIRECTORY.TYPE, INV_ITEM_DIRECTORY.TRADEMARK, INV_ITEM_DIRECTORY_UNITS.UNIT_CODE, INV_ITEM_DIRECTORY.MINIMUM_QTY, INV_ITEM_DIRECTORY.MAXIMUM_QTY, INV_ITEM_DIRECTORY.IN_ACTIVE, INV_ITEM_DIRECTORY.[GROUP],  INV_ITEM_DIRECTORY.CATEGORY,STUFF((SELECT   ','+ SAL_TYPE+':'+CONVERT(varchar(10), PRICE) frOM         INV_ITEM_PRICE where INV_ITEM_PRICE.ITEM_CODE= INV_ITEM_DIRECTORY.CODE and INV_ITEM_PRICE.UNIT_CODE=INV_ITEM_DIRECTORY_UNITS.UNIT_CODE FOR XML PATH('')),1,1,'')as price,INV_ITEM_DIRECTORY.LOCATION, INV_ITEM_DIRECTORY.DESC_ARB FROM    INV_ITEM_DIRECTORY left outer join INV_ITEM_DIRECTORY_UNITS on INV_ITEM_DIRECTORY.CODE=INV_ITEM_DIRECTORY_UNITS.ITEM_CODE ";

            query = "  SELECT        INV_ITEM_DIRECTORY.CODE AS [Item Code], INV_ITEM_DIRECTORY_UNITS.BARCODE AS Barcode, INV_ITEM_DIRECTORY.DESC_ENG AS [Item Name], INV_ITEM_DIRECTORY.TYPE, INV_ITEM_DIRECTORY.TRADEMARK, INV_ITEM_DIRECTORY_UNITS.UNIT_CODE, INV_ITEM_DIRECTORY.MINIMUM_QTY, INV_ITEM_DIRECTORY.MAXIMUM_QTY, INV_ITEM_DIRECTORY.IN_ACTIVE, INV_ITEM_DIRECTORY.[GROUP], INV_ITEM_DIRECTORY.CATEGORY, STUFF((SELECT        ',' + SAL_TYPE + ':' + CONVERT(varchar(10), PRICE) AS Expr1  FROM            INV_ITEM_PRICE  WHERE        (ITEM_CODE = INV_ITEM_DIRECTORY.CODE) AND (UNIT_CODE = INV_ITEM_DIRECTORY_UNITS.UNIT_CODE) FOR XML PATH('')), 1, 1, '') AS price,  INV_ITEM_DIRECTORY.DESC_ARB, GEN_LOCATION.DESC_ENG FROM            INV_ITEM_DIRECTORY LEFT OUTER JOIN   GEN_LOCATION ON INV_ITEM_DIRECTORY.LOCATION = GEN_LOCATION.CODE LEFT OUTER JOIN INV_ITEM_DIRECTORY_UNITS ON INV_ITEM_DIRECTORY.CODE = INV_ITEM_DIRECTORY_UNITS.ITEM_CODE ";
            DataTable dt = new DataTable();
            //adapter.SelectCommand = cmd;
            //adapter.Fill(dt);
            dt = DbFunctions.GetDataTable(query);
            source.DataSource = dt;
            dataGridItem.DataSource = source;

          

        }

        private void Search_Items_Load(object sender, EventArgs e)
        {
            bindgridview();
            
        }

        private void Txt_Itemcode_TextChanged(object sender, EventArgs e)
        {
            if (txt_Itemname.Text == "" && txt_barcode.Text == "" && Txt_Itemcode.Text == "")
            {
                dataGridItem.Visible = false;
            }
            else
            {
                dataGridItem.Visible = true;
                source.Filter = string.Format("[Item Code] LIKE '%{0}%' and [Item Name] like '%{1}%' and Barcode like '%{2}%'", Txt_Itemcode.Text, txt_Itemname.Text, txt_barcode.Text);
            }
          }

        private void txt_Itemname_TextChanged(object sender, EventArgs e)
        {
            if (txt_Itemname.Text == "" && txt_barcode.Text == "" && Txt_Itemcode.Text == "")
            {
                dataGridItem.Visible = false;
            }
            else
            {
                dataGridItem.Visible = true;
                source.Filter = string.Format("[Item Code] LIKE '%{0}%' and [Item Name] like '%{1}%' and Barcode like '%{2}%'", Txt_Itemcode.Text, txt_Itemname.Text, txt_barcode.Text);
            }
        }

        private void txt_barcode_TextChanged(object sender, EventArgs e)
        {
            if (txt_Itemname.Text == "" && txt_barcode.Text == "" && Txt_Itemcode.Text == "")
            {
                dataGridItem.Visible = false;
            }
            else
            {
                dataGridItem.Visible = true;
                source.Filter = string.Format("[Item Code] LIKE '%{0}%' and [Item Name] like '%{1}%' and Barcode like '%{2}%'", Txt_Itemcode.Text, txt_Itemname.Text, txt_barcode.Text);
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            txt_barcode.Text = "";
            Txt_Itemcode.Text = "";
            txt_Itemname.Text = "";
        }

        private void Search_Items_KeyDown(object sender, KeyEventArgs e)
        {
           
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

        private void dataGridItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
                tb.Focus();
              
        }

        private void Txt_Itemcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Down)
            {
                tb = sender as KryptonTextBox;

                if (dataGridItem.Visible == true)
                    dataGridItem.Focus();
            }
        }

        private void txt_barcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Down)
            {
                tb = sender as KryptonTextBox;

                if (dataGridItem.Visible == true)
                    dataGridItem.Focus();
            }
        }
    }
}
