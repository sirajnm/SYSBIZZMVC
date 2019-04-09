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
using Sys_Sols_Inventory.Model;

namespace Sys_Sols_Inventory
{
    public partial class PriceChange : Form
    {
        //SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        //SqlCommand cmd = new SqlCommand();
        //DataTable dt = new DataTable();
        InvItemDirectoryUnits dirObj = new InvItemDirectoryUnits();
        InvStkTrxHdrDB trxhdrObj = new InvStkTrxHdrDB();
        private BindingSource source = new BindingSource();
        int price;
        string code = "";
        public PriceChange()
        {
            InitializeComponent();
        }

        private void PriceChange_Load(object sender, EventArgs e)
        {
            checkBox1.Checked = General.IsEnabled(Settings.StockOut);
            cmb_ratetype.SelectedIndexChanged -= new EventHandler(cmb_ratetype_SelectedIndexChanged);
            load_pricetype();
            loadBasePrice();
            LoadItems();
            cmb_ratetype.SelectedIndexChanged += new EventHandler(cmb_ratetype_SelectedIndexChanged);
            dataGridView1.Columns[8].DisplayIndex = 3;
            
        }

        public DataTable LoadItems()
        {
            DataTable dt = new DataTable();
            dataGridView1.DataSource = dt;
            try
            {

                //SqlCommand cmd = new SqlCommand(" SELECT INV_ITEM_DIRECTORY.DESC_ENG AS [ITEM_NAME],BATCH.[PRICE BATCH], INV_ITEM_PRICE.ITEM_CODE, INV_ITEM_PRICE.SAL_TYPE,INV_ITEM_PRICE.PRICE, INV_ITEM_PRICE.UNIT_CODE  FROM INV_ITEM_DIRECTORY  INNER JOIN (select  CONVERT(varchar, MAX(batch_increment)) id,MAX(batch_id) [PRICE BATCH],Item_id from tblstock group by Item_id ) AS BATCH  ON INV_ITEM_DIRECTORY.CODE=BATCH.Item_id LEFT OUTER JOIN INV_ITEM_PRICE ON BATCH.[PRICE BATCH]=INV_ITEM_PRICE.BATCH_ID AND INV_ITEM_PRICE.SAL_TYPE='" + cmb_ratetype.Text + "' ", conn);
                //SqlDataAdapter sda = new SqlDataAdapter(cmd);
                //sda.Fill(dt);
                dirObj.InvItemSalePriceType = cmb_ratetype.Text;
                dt=dirObj.DetailsBySaleType();

                dataGridView1.AutoGenerateColumns = false;
              
                dataGridView1.Columns[1].Name = "ITEM_NAME";
                dataGridView1.Columns[1].HeaderText = "ITEM_NAME";
                dataGridView1.Columns[1].DataPropertyName = "ITEM_NAME";
                dataGridView1.Columns[2].DataPropertyName = "ITEM_CODE";
                dataGridView1.Columns[2].HeaderText = "ITEM_CODE";
                dataGridView1.Columns[3].DataPropertyName = "SAL_TYPE";
                dataGridView1.Columns[3].HeaderText = "SAL_TYPE";
                dataGridView1.Columns[4].DataPropertyName = "PRICE";
                dataGridView1.Columns[4].HeaderText = "PRICE";
                dataGridView1.Columns[5].DataPropertyName = "UNIT_CODE";
                dataGridView1.Columns[5].HeaderText = "UNIT_CODE";
                dataGridView1.Columns[8].DataPropertyName = "PRICE BATCH";
                dataGridView1.Columns[8].HeaderText = "PRICE BATCH";
                source.DataSource = dt;
                dataGridView1.DataSource = source;
                dataGridView1.AutoGenerateColumns = false;
              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //conn.Close();
            }
                return dt;
        }

        public void loadBasePrice()
        {
            try
            {
               // conn.Open();
                DataTable dt = new DataTable();
                //SqlCommand cmd = new SqlCommand("SELECT CODE,DESC_ENG FROM GEN_PRICE_TYPE", conn);
                //SqlDataAdapter sda = new SqlDataAdapter(cmd);
                //sda.Fill(dt);
                dt = trxhdrObj.genPriceType();
                cmb_basepricetype.DataSource = dt;
                cmb_basepricetype.DisplayMember = "CODE";
                cmb_basepricetype.ValueMember = "DESC_ENG";
              //  conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void load_pricetype()
        {
            try
            {
               // conn.Open();
                DataTable dt = new DataTable();
                //SqlCommand cmd = new SqlCommand("SELECT CODE,DESC_ENG FROM GEN_PRICE_TYPE", conn);
                //SqlDataAdapter sda = new SqlDataAdapter(cmd);
                //sda.Fill(dt);
                dt = trxhdrObj.genPriceType();
                cmb_ratetype.DataSource = dt;
                cmb_ratetype.DisplayMember = "CODE";
                cmb_ratetype.ValueMember = "DESC_ENG";
               // conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        decimal ab;
        public void price_change()
        {
            if (dataGridView1.Rows.Count > 0)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    double amt = Convert.ToDouble(dataGridView1.Rows[i].Cells[6].Value);
                    double perc = Convert.ToDouble(dataGridView1.Rows[i].Cells[7].Value);
                    string c = dataGridView1.Rows[i].Cells[2].Value.ToString();
                    string BATCH = dataGridView1.Rows[i].Cells["PriceBatch"].Value.ToString();
                    bool value = false;
                    value = Convert.ToBoolean(dataGridView1.Rows[i].Cells[0].Value);

                    //SqlCommand cmd = new SqlCommand("SELECT PRICE FROM INV_ITEM_PRICE WHERE SAL_TYPE = '" + cmb_basepricetype.Text + "' AND ITEM_CODE='" + c + "' AND BATCH_ID='" + BATCH + "'", conn);
                    //SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                   // sda.Fill(dt);
                    dirObj.InvItemSalePriceType = cmb_basepricetype.Text;
                    dirObj.ItemCode = c;
                    dirObj.Branch = BATCH;
                    dt = dirObj.getPrice();
                    string b = dt.Rows[0]["PRICE"].ToString();

                    if (rb_add.Checked == true)
                    {
                        if (perc != 0.0)
                        {
                            ab = (Convert.ToDecimal(b) + (Convert.ToDecimal(b) * Convert.ToDecimal(perc)) / 100);
                        }
                        if (amt != 0.0)
                        {
                            ab =  (Convert.ToDecimal(amt));
                        }
                    }

                    if (rb_subtract.Checked == true)
                    {
                        if (perc != 0.0)
                        {
                            ab = (Convert.ToDecimal(b) - (Convert.ToDecimal(b) * Convert.ToDecimal(perc)) / 100);
                        }

                        if (amt != 0.0)
                        {
                            ab = ((Convert.ToDecimal(b)) - (Convert.ToDecimal(amt)));
                        }
                    }

                    if (value == true)
                    {
                       // conn.Open();
                        string query = "";
                        query = "UPDATE INV_ITEM_PRICE SET PRICE='" + ab + "' WHERE SAL_TYPE='" + cmb_ratetype.Text + "' AND ITEM_CODE='" + c + "'AND BATCH_ID='" + BATCH + "'";
                        if (cmb_ratetype.Text=="PUR")
                        {
                            query += ";UPDATE tblStock SET cost_price=" + ab + " WHERE batch_id='" + BATCH+"'";
                        }
                       // cmd.CommandType = CommandType.Text;
                       // cmd.ExecuteNonQuery();
                       // conn.Close();
                        DbFunctions.InsertUpdate(query);

                       // conn.Open();
                        
                        query = "UPDATE INV_ITEM_PRICE_DF SET PRICE='" + ab + "' WHERE SAL_TYPE='" + cmb_ratetype.Text + "' AND ITEM_CODE='" + c + "'";
                        if (cmb_ratetype.Text == "PUR")
                        {
                            query += ";UPDATE tblStock SET cost_price=" + ab + " WHERE ITEM_CODE='" + c + "'";
                        }
                       // cmd.CommandType = CommandType.Text;
                        //cmd.ExecuteNonQuery();
                       // conn.Close();
                        DbFunctions.InsertUpdate(query);
                    }
                    else
                    {

                    }
                }
                MessageBox.Show("Price Changed Successfully...");
            }

            else
            {
                MessageBox.Show("Price Not Changed...");
            }
        }

        private void btn_apply_Click(object sender, EventArgs e)
        {
            if (rb_add.Checked == false && rb_subtract.Checked == false)
            {
                MessageBox.Show("Select Add OR Subtract Option");
            }
            else
            {
                price_change();
                load_pricetype();
            }
        }

        private void rb_type_percentage_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cmb_ratetype_SelectedIndexChanged(object sender, EventArgs e)
        {
         
            LoadItems();


        }

        private void txt_Search_TextChanged(object sender, EventArgs e)
        {
            try
            {
                source.Filter = string.Format("[ITEM_NAME] LIKE '%{0}%' or [ITEM_CODE] LIKE '%{0}%'", txt_Search.Text.Replace("'", "''").Replace("*", "[*] "));
                dataGridView1.AutoGenerateColumns = false;

                dataGridView1.Columns[1].Name = "ITEM_NAME";
                dataGridView1.Columns[1].DataPropertyName = "ITEM_NAME";
                dataGridView1.Columns[2].DataPropertyName = "ITEM_CODE";
                dataGridView1.Columns[3].DataPropertyName = "SAL_TYPE";
                dataGridView1.Columns[4].DataPropertyName = "PRICE";
                dataGridView1.Columns[5].DataPropertyName = "UNIT_CODE";
                dataGridView1.Columns[8].DataPropertyName = "PRICE BATCH";

            }
            catch (Exception en)
            {
                MessageBox.Show(en.Message);
            }
        }

        private void btn_move_Click(object sender, EventArgs e)
        {

        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (dg_selecteditem.Rows.Count > 0 && dg_selecteditem.CurrentRow != null)
            //    {
            //        dg_selecteditem.Rows.Remove(dg_selecteditem.CurrentRow);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        public void grid_check()
        {
            //foreach (DataGridViewRow dgr1 in dataGridView1.Rows)
            //{
            //    if (Convert.ToBoolean(dgr1.Cells[0].Value) == true)
            //    {
            //        string a = dgr1.Cells[1].Value.ToString();
            //        string b = "";
            //        foreach (DataGridViewRow dgr2 in dg_selecteditem.Rows)
            //        {
            //            b = dgr2.Cells[0].Value.ToString();
            //            if (a == b)
            //            {
            //                MessageBox.Show("duplicate found");
            //            }
            //        }
            //    }
            // }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
            //row.DefaultCellStyle.SelectionBackColor = Color.Blue;
            //DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];

            //if (chk.Value == chk.TrueValue)
            //{
            //    chk.Value = chk.FalseValue;
            //    row.Selected = false;
            //    row.DefaultCellStyle.BackColor = Color.White;
            //    dataGridView1.Refresh();
            //}
            //else
            //{
            //    chk.Value = chk.TrueValue;
            //    row.Selected = true;
            //    row.DefaultCellStyle.BackColor = Color.Blue;
            //    dataGridView1.Refresh();
            //}  
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSelectAll.Checked == true)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    bool value = false;
                    value = Convert.ToBoolean(dataGridView1.Rows[i].Cells[0].Value);
                    if (value == false)
                    {
                        dataGridView1.Rows[i].Cells[0].Value = true;
                    }
                }

            }

            if (chkSelectAll.Checked == false)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    bool value = false;
                    value = Convert.ToBoolean(dataGridView1.Rows[i].Cells[0].Value);
                    if (value == true)
                    {
                        dataGridView1.Rows[i].Cells[0].Value = false;
                    }
                }
            }
        }
        public void selectBatches(string code)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            DataRow dr = dt.NewRow();
            string query="";

            //cmd.Connection = conn;
            if (checkBox1.Checked)
            {
                query = " SELECT tblStock.batch_id batch_id,INV_ITEM_PRICE.PRICE as price from tblStock inner join INV_ITEM_PRICE on INV_ITEM_PRICE.BATCH_ID=tblStock.batch_id  where tblStock.item_id='" + code + "'  and INV_ITEM_PRICE.SAL_TYPE='" + cmb_ratetype.Text + "'";
            }
            else
            {
                query = " SELECT tblStock.batch_id batch_id,INV_ITEM_PRICE.PRICE as price from tblStock inner join INV_ITEM_PRICE on INV_ITEM_PRICE.BATCH_ID=tblStock.batch_id  where tblStock.item_id='" + code + "' and tblStock.Qty>0 and INV_ITEM_PRICE.SAL_TYPE='" + cmb_ratetype.Text + "'";
            }
            //da.SelectCommand = cmd;
            //da.Fill(dt);
            dt = DbFunctions.GetDataTable(query);
            dt.Rows.InsertAt(dr, 0);
            cmbUnit.DisplayMember = "batch_id";
            cmbUnit.ValueMember = "price";
            cmbUnit.DataSource = dt;


        }
        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 8)
            {
                System.Drawing.Rectangle rect = dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                if (e.ColumnIndex == 8)
                {
                    txtUnitProperty.Visible = false;
                    cmbUnit.Width = rect.Width;
                    cmbUnit.Height = rect.Height;
                    cmbUnit.Location = new Point(dataGridView1.Location.X + rect.X, dataGridView1.Location.Y + rect.Y);
                    var value = Convert.ToString(dataGridView1.CurrentCell.Value);
                    if (value.Equals("") && !cmbUnit.Text.Equals("System.Data.DataRowView"))
                    {
                        dataGridView1.CurrentCell.Value = cmbUnit.Text;
                    }
                    cmbUnit.Visible = true;
                    cmbUnit.Focus();
                    selectBatches(dataGridView1.CurrentRow.Cells[2].Value.ToString());
                }
            }
        }

        private void dataGridView1_DragDrop(object sender, DragEventArgs e)
        {

        }

        private void dataGridView1_Scroll(object sender, ScrollEventArgs e)
        {
            cmbUnit.Visible = false;
        }

        private void cmbUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbUnit.SelectedIndex > 0)
            {
                dataGridView1.CurrentRow.Cells["PriceBatch"].Value = cmbUnit.Text;
                dataGridView1.CurrentRow.Cells["Column5"].Value = cmbUnit.SelectedValue.ToString();
            }
        }

        private void cmb_basepricetype_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
