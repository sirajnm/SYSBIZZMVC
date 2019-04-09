using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using System.Drawing.Printing;
using System.Globalization;
using System.Threading;
using System.Net;
using System.Drawing.Imaging;
using System.IO;
using System.Text.RegularExpressions;

namespace Sys_Sols_Inventory
{
    public partial class Invoice_Type_Report : Form
    {
        string name = "";
        string uom = "";
        string price = "";
        //SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        //SqlCommand cmd = new SqlCommand();
       // SqlDataReader sdr = new SqlDataReader();
      //  SqlDataAdapter sda = new SqlDataAdapter();
      
        public Invoice_Type_Report()
        {
            InitializeComponent();
        }
        public void Select()
        {
            try
            {
                //conn.Open();
                //DataTable dt = new DataTable();
                //cmd.CommandText = "SELECT ITEM_DESC_ENG FROM INV_SALES_DTL WHERE inv_type='" + inv_type.Text + "',conn";
                //SqlDataAdapter sda = new SqlDataAdapter(cmd);
  
                //sda.Fill(dt);
                //if (dt.Rows.Count > 0)
                //{
                //    name = dt.Rows[0][2].ToString();
                //}
                //conn.Open();
                //DataTable dt = new DataTable();
                //SqlDataAdapter sda =new SqlDataAdapter ("SELECT * FROM INV_SALES_DTL WHERE inv_type='" + inv_type.Text + "'",conn);
                //sda.Fill(dt);
                //if (dt.Rows.Count > 0)
                //{
                //    int i = dataGridView1.Rows.Add(new DataGridViewRow());
                //    DataGridViewCellCollection c = dataGridView1.Rows[i].Cells;
                //    name = dt.Rows[0]["ITEM_DESC_ENG"].ToString();
                //    uom = dt.Rows[0]["UOM"].ToString();
                //    price = dt.Rows[0]["PRICE"].ToString();
                //}
               // cmd.CommandText = "SELECT ITEM_DESC_ENG FROM INV_SALES_DTL WHERE inv_type='" + inv_type.Text + "',conn";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
               // conn.Close();
            }
      
        }

        private void Invoice_Type_Report_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns.Add("Column","ITEM_NAME");
            dataGridView1.Columns.Add("Column", "UOM");
            dataGridView1.Columns.Add("Column", "PRICE");
        }

        private void inv_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            Select();
        }
    }
}
