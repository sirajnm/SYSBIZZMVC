using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using ComponentFactory.Krypton.Toolkit;
using System.Data.SqlClient;
using Sys_Sols_Inventory.Model;
namespace Sys_Sols_Inventory
{
    public partial class NewDiscount : Form
    {
        private int selectedRow = -1;
        //private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        //private SqlCommand cmd = new SqlCommand();
        //private SqlDataAdapter adapter = new SqlDataAdapter();
        private BindingSource source = new BindingSource();
        private bool HasArabic = General.IsEnabled(Settings.Arabic);
        private string ID="";
        Class.Discount dis = new Class.Discount();


        Initial mdi = (Initial)Application.OpenForms["Initial"];
        Login lg = (Login)Application.OpenForms["Login"];


        public NewDiscount()
        {
            InitializeComponent();
        }
        private void NewDiscount_Load(object sender, EventArgs e)
        {
            try
            {
                //conn.Open();
                //DataTable tbl = new DataTable();
                //cmd.Connection = conn;
                //cmd.CommandType = CommandType.Text;
                //adapter.SelectCommand = cmd;
                //cmd.CommandText = "SELECT CODE AS [key],CODE+' - '+DESC_ENG AS value FROM GEN_PRICE_TYPE";
                //adapter.Fill(tbl);
                RATE_CODE.DisplayMember = "value";
                RATE_CODE.ValueMember = "key";
                RATE_CODE.DataSource = dis.SelectPriceType() ;
                //conn.Close();

            }
            catch { }
            bindgridview();

            PnlArabic.Visible = HasArabic;
            ActiveControl = DISCOUNT_ENG;
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.S | Keys.Control))
            {
                btnSave.PerformClick();
                return true;
            }

            if (keyData == (Keys.Escape))
            {
                dataGridItem.Visible = false;
                ITEM_NAME.Focus();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        public void bindgridview()
        {
            try
            {

                //conn.Open();
                //cmd.Connection = conn;
                //cmd.CommandText = "SELECT        INV_ITEM_DIRECTORY.CODE AS [Item Code], INV_ITEM_DIRECTORY_UNITS.BARCODE AS Barcode, INV_ITEM_DIRECTORY.DESC_ENG AS [Item Name],  INV_ITEM_DIRECTORY_UNITS.UNIT_CODE, INV_ITEM_PRICE.PRICE FROM            INV_ITEM_PRICE INNER JOIN   INV_ITEM_DIRECTORY_UNITS ON INV_ITEM_PRICE.UNIT_CODE = INV_ITEM_DIRECTORY_UNITS.UNIT_CODE AND INV_ITEM_PRICE.ITEM_CODE = INV_ITEM_DIRECTORY_UNITS.ITEM_CODE RIGHT OUTER JOIN  INV_ITEM_DIRECTORY ON INV_ITEM_DIRECTORY_UNITS.ITEM_CODE = INV_ITEM_DIRECTORY.CODE WHERE        (INV_ITEM_PRICE.SAL_TYPE = '" + RATE_CODE.SelectedValue + "')";
                //cmd.CommandType = CommandType.Text;
                //DataTable dt = new DataTable();
                //adapter.SelectCommand = cmd;
                //adapter.Fill(dt);
                dis.RateCode = RATE_CODE.SelectedValue.ToString() ;
                source.DataSource = dis.getItem() ;
                dataGridItem.DataSource = source;
                dataGridItem.RowHeadersVisible = false;
                dataGridItem.Columns[1].Visible = false;
                dataGridItem.Columns[2].Width = 250;
                dataGridItem.ClearSelection();
                //conn.Close();

            }
            catch
            {
            }


        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (valid())
            {
                if (ID == "")
                {
                    dis.Discount_Name = DISCOUNT_ENG.Text;
                    dis.Discount_Arb = DISCOUNT_ARB.Text;
                    dis.Start_Date = Convert.ToDateTime(START_DATE.Text);
                    dis.End_Date = Convert.ToDateTime(END_DATE.Text);
                    dis.Orders = Convert.ToInt16(ORDER.Text);
                    if (IN_ACTIVE.Checked == true)
                    {
                        dis.Isactive = true;
                    }
                    else
                    {
                        dis.Isactive = false;
                    }
                    dis.InsertDiscountType();

                    ID = dis.getmaxid();
                    AddItems();
                    btnClear.PerformClick();

                }
                else
                {
                    dis.Discount_Name = DISCOUNT_ENG.Text;
                    dis.Discount_Arb = DISCOUNT_ARB.Text;
                    dis.Start_Date = Convert.ToDateTime(START_DATE.Text);
                    dis.End_Date = Convert.ToDateTime(END_DATE.Text);
                    dis.Orders = Convert.ToInt16(ORDER.Text);
                    if (IN_ACTIVE.Checked == true)
                    {
                        dis.Isactive = true;
                    }
                    else
                    {
                        dis.Isactive = false;
                    }
                    dis.Disc_Id = ID;
                    dis.UpdateDiscountType();
                    AddItems();
                    btnClear.PerformClick();
                }

            }
        }

        public bool valid()
        {
            if (DISCOUNT_ENG.Text == "" || ORDER.Text == "")
            {
                MessageBox.Show("Enter discount name and order");

                return false;
            }
            else
                return true;
        }
        private void ITEM_NAME_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (ITEM_NAME.Text == "")
                {
                    dataGridItem.Visible = false;
                 //   firstitemlistbyname = true;
                }
                else
                {
                    dataGridItem.Visible = true;
                    source.Filter = string.Format("[Item Name] LIKE '%{0}%' ", ITEM_NAME.Text);
                    dataGridItem.ClearSelection();
                }

            }
            catch
            {
            }
        }

        private void dataGridItem_KeyDown(object sender, KeyEventArgs e)
        {
              try
            {
                if (e.KeyData == Keys.Enter)
                {
                    if (selectedRow == -1)
                    {
                        selectedRow = dgvItems.Rows.Add(new DataGridViewRow());
                    }
                    DataGridViewCellCollection c = dgvItems.Rows[selectedRow].Cells;
                    c["Code"].Value = dataGridItem.CurrentRow.Cells[0].Value.ToString();
                    c["ItemName"].Value = dataGridItem.CurrentRow.Cells[2].Value.ToString();
                    c["Uom"].Value = dataGridItem.CurrentRow.Cells[3].Value.ToString();
                    c["SalType"].Value = RATE_CODE.SelectedValue.ToString();
                    

                    clear();
                }
            }
            catch
            {
            }
        }

        public void AddItems()
        {
            try
            {
                //conn.Open();
                //cmd.Connection = conn;
                //cmd.CommandText = "delete from Tbl_ProductDiscount where Discount_Id='" + ID + "'";
                //cmd.CommandType = CommandType.Text;
                //cmd.ExecuteNonQuery();
                //conn.Close();
                dis.Disc_Id = ID;
                dis.deleteProDiscount();
                for (int i = 0; i < dgvItems.Rows.Count - 1; i++)
                {

                    dis.Discount_Id = Convert.ToInt16(ID);
                    dis.Product_Id = dgvItems.Rows[i].Cells[0].Value.ToString();
                    dis.Uom = dgvItems.Rows[i].Cells[2].Value.ToString();
                    dis.SalType = dgvItems.Rows[i].Cells[3].Value.ToString();
                    dis.Discount_Type = dgvItems.Rows[i].Cells[4].Value.ToString();
                    dis.Value = dgvItems.Rows[i].Cells[5].Value.ToString();
                    dis.insertproductDiscount();

                }
            }
            catch(Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        
        }
        private void ITEM_NAME_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Down)
            {
                if (dataGridItem.Visible == true)
                {
                    dataGridItem.Focus();
                }
                else
                {
                    dgvItems.Focus();
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ID = "";
            DISCOUNT_ARB.Text = "";
            DISCOUNT_ENG.Text = "";
            ORDER.Text = "";
            START_DATE.Value = DateTime.Now;
            END_DATE.Value = DateTime.Now;
            IN_ACTIVE.Checked = false;
            dgvItems.Rows.Clear();
           
        }


        public void clear()
        {
            selectedRow = -1;
            ITEM_NAME.Text = string.Empty;
            ITEM_NAME.Focus();
        }

        private void btnapply_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvItems.Rows.Count - 1; i++)
            {
                dgvItems.Rows[i].Cells[4].Value = TYPE.Text;
                dgvItems.Rows[i].Cells[5].Value = VAL.Text;
            }
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {

            DiscountMasterHelp discm = new DiscountMasterHelp();
          
            if (discm.ShowDialog() == DialogResult.OK && discm.c != null)
            {
                btnClear.PerformClick();
                ID = Convert.ToString(discm.c["Disc_Id"].Value);
                dis.Disc_Id = ID;
                DISCOUNT_ENG.Text = discm.c["Discount_Name"].Value.ToString();
                START_DATE.Value =Convert.ToDateTime( discm.c["Start_Date"].Value.ToString());
                END_DATE.Value = Convert.ToDateTime(discm.c["End_Date"].Value.ToString());
                ORDER.Text = discm.c["Orders"].Value.ToString();
                IN_ACTIVE.Checked = Convert.ToBoolean( discm.c["Isactive"].Value.ToString());

                try
                {
                    //conn.Open();
                    //cmd.CommandText = "SELECT     Tbl_ProductDiscount.Product_Id as id, INV_ITEM_DIRECTORY.DESC_ENG AS name, Tbl_ProductDiscount.Uom AS uom, Tbl_ProductDiscount.Discount_Type AS disctype, Tbl_ProductDiscount.Value AS value, Tbl_ProductDiscount.SalType AS saltype FROM         Tbl_ProductDiscount INNER JOIN INV_ITEM_DIRECTORY ON Tbl_ProductDiscount.Product_Id = INV_ITEM_DIRECTORY.CODE WHERE     (Tbl_ProductDiscount.Discount_Id = '" + ID + "')";
                    SqlDataReader r = dis.GetProductDiscountSingleItem();
                    dgvItems.Rows.Clear();
                    while (r.Read())
                    {
                        dgvItems.Rows.Add(r["id"], r["name"], r["uom"], r["saltype"], r["disctype"], r["value"]);
                    }
                  //  conn.Close()
                    DbFunctions.CloseConnection();
                }
                catch { }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            btnClear.PerformClick();
            DiscountMasterHelp h = new DiscountMasterHelp();
            h.ShowDialog();
        }

        private void linkRemoveUnit_LinkClicked(object sender, EventArgs e)
        {
            if (dgvItems.Rows.Count > 0 && dgvItems.CurrentRow != null && dgvItems.NewRowIndex != dgvItems.CurrentRow.Index)
            {
                dgvItems.Rows.Remove(dgvItems.CurrentRow);
            }
        }

        private void BARCODE_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (BARCODE.Text != "")
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        DataTable t = General.Product4mBarcode(BARCODE.Text);
                        if (t.Rows.Count > 0)
                        {
                            if (selectedRow == -1)
                            {
                                selectedRow = dgvItems.Rows.Add(new DataGridViewRow());
                            }
                            DataGridViewCellCollection c = dgvItems.Rows[selectedRow].Cells;
                            c["Code"].Value = t.Rows[0][0].ToString(); ;
                            c["ItemName"].Value = t.Rows[0][1].ToString();
                            c["Uom"].Value = t.Rows[0][2].ToString();
                            c["SalType"].Value = RATE_CODE.SelectedValue.ToString();
                            clear();


                        }
                        else if (e.KeyData == Keys.Enter)
                        {
                            ITEM_NAME.Focus();
                        }
                    }
                }
                else if (e.KeyData == Keys.Enter)
                {
                    ITEM_NAME.Focus();
                }
            }
            catch
            {
            }
           
        }

        private void DISCOUNT_ENG_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (sender is KryptonTextBox)
                {
                    
                    string name = (sender as KryptonTextBox).Name;
                    switch (name)
                    {
                        case "DISCOUNT_ENG":
                            if (HasArabic)
                            {
                                DISCOUNT_ARB.Focus();
                            }
                            else
                            {
                                START_DATE.Focus();
                            }
                            break;
                        case "DISCOUNT_ARB":
                            START_DATE.Focus();
                            break;
                        case "ORDER":
                            IN_ACTIVE.Focus();
                            break;
                        default:
                            break;

                    }
                }
                else if (sender is KryptonComboBox)
                {
                    string name = (sender as KryptonComboBox).Name;
                    switch (name)
                    {
                        case "RATE_CODE":

                            BARCODE.Focus();

                            break;
                       
                        default:
                            break;
                    }
                }
                else if (sender is DateTimePicker)
                {
                    string name = (sender as DateTimePicker).Name;
                    switch (name)
                    {
                        case "START_DATE":
                            END_DATE.Focus();
                            break;
                        case "END_DATE":
                           ORDER.Focus();
                            break;
                        default:
                            break;
                    }
                }
                else if (sender is KryptonCheckBox)
                {
                    string name = (sender as KryptonCheckBox).Name;
                    switch (name)
                    {
                        case "IN_ACTIVE": 
                            RATE_CODE.Focus();
                            break;
                       
                        default:
                            break;
                    }
                }
            }

        }
       
        
        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                if (lg.Theme == "1")
                {

                    ComponentFactory.Krypton.Navigator.KryptonPage kp = new ComponentFactory.Krypton.Navigator.KryptonPage();

                    mdi.maindocpanel.SelectedPage.Dispose();
                }
                else
                {
                    this.Close();
                    //ComponentFactory.Krypton.Navigator.KryptonPage kp = new ComponentFactory.Krypton.Navigator.KryptonPage();

                    //mdi.maindocpanel.SelectedPage.Dispose();
                }


            }
            catch
            {
                this.Close();
            }

        }

        private void btndiscountlimit_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
       
    }
}
