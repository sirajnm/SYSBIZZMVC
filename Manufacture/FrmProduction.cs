using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Windows.Forms;
using Sys_Sols_Inventory.Model;

namespace Sys_Sols_Inventory.Manufacture
{
    public partial class FrmProduction : Form
    {
        //TODO: VALIDATE THE FORMS.
        //private SqlConnection conn = DbFunctions.GetConnection();
        //private SqlCommand cmd = new SqlCommand();
        //private SqlDataAdapter adapter = new SqlDataAdapter();
        private ProductionMaster master;
        private String decimalFormat;
        private int selectedRow = -1;
        private int RawGridselectedRow = -1;
        private BindingSource source2 = new BindingSource();
        private bool hasTax = false;
        private bool hasArabic = true;
        bool hasPriceBatch = false;
        decimal cost; bool edit = false;
        double tax_per=0;
        RawMaterials RawMaterials = new RawMaterials();

        public FrmProduction()
        {
            InitializeComponent();
            //cmd.Connection = conn;
            //adapter.SelectCommand = cmd;
            master = new ProductionMaster();
            master.CalculationUpdatedEventHandler += master_CalculationUpdatedEventHandler;
            decimalFormat = Common.getDecimalFormat();
            cmbProductItem.SelectedIndexChanged -= cmbProductItem_SelectedIndexChanged;
            txtProductCost.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            txtProductQty.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            txtProductMRP.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            txtRawCost.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            txtRawQty.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            txtRawDamageQty.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            txtOtherAmount.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            cmbProductItem.SelectedIndexChanged -= cmbRawItem_SelectedIndexChanged;
        }

        void master_CalculationUpdatedEventHandler(double productionCost, double damageCost, double otherExpense)
        {
            lblProductionCost.Text = productionCost.ToString(decimalFormat);
            lblDamageCost.Text = damageCost.ToString(decimalFormat);
            lblOtherExpenses.Text = otherExpense.ToString(decimalFormat);
        }

        private void resetProductForm()
        {
            //if (cmbProductItem.SelectedValue != null)
            //{
            //    cmbProductItem.SelectedIndex = 0;
            //}
            txtItenCode.Text = "";
            cmbProductItem.Text = "";
            txtProductCost.Text = "";
            txtProductQty.Text = "";
            txtProductMRP.Text = "";
            txtProductBatch.Text = "";
            ExDate.Value = DateTime.Now;
            selectedRow = -1;
            edit = false;
            cmbProductItem.Focus();
        }

        private bool isProductValid()
        {
             if (txtItenCode.Text.Equals(""))
            {
                MessageBox.Show("You must select a item!");
                cmbProductItem.Focus();
                return false;
            }
             //else if(cmbProductBatch.SelectedIndex<0)
             //{
             // MessageBox.Show("You must select a Batch!");
             // cmbProductBatch.Focus();
             //return false;
             // }
            else if(cmbProductUnit.SelectedIndex < 0)
            {
                MessageBox.Show("You must select a Unit!");
                cmbProductUnit.Focus();
                return false;
            }
            //else if (txtProductCost.Text.Equals(""))
            //{
            //    MessageBox.Show("You must enter cost price!");
            //    txtProductCost.Focus();
            //    return false;
            //}
            else if (txtProductQty.Text.Equals(""))
            {
                MessageBox.Show("You must enter Quantity!");
                cmbProductItem.Focus();
                return false;
            }
             else if (txtProductMRP.Text.Equals(""))
             {
                 txtProductMRP.Text = "0.00";
                 return true;
             }
            else
            {

                return true;
            }
        }
     //   CultureInfo provider = CultureInfo.InvariantCulture;
        private void btnProductAdd_Click(object sender, EventArgs e)
        {
            double costPrice = 0;
            if (dgvProducts.RowCount == 0 || edit==true)
            {
                if (isProductValid())
                {
                    double qty = Convert.ToDouble(txtProductQty.Text);

                    if (txtProductCost.Text != "")
                    {
                        costPrice = Convert.ToDouble(txtProductCost.Text);
                    }
                    else
                    {
                        costPrice = 0;
                    }
                    ProductionItem item = new ProductionItem(
                        txtItenCode.Text, cmbProductItem.Text,
                        txtProductBatch.Text, "", ExDate.Value, cmbProductUnit.Text, Convert.ToDouble(cmbProductUnit.SelectedValue),
                        qty, costPrice, Convert.ToDouble(txtProductMRP.Text), qty * costPrice);
                    if (selectedRow <= -1)
                    {
                        master.AddItem(item);
                    }
                    else
                    {
                        master.AddItemByIndex(item, selectedRow);
                    }
                    resetProductForm();

                    // Auto Adding Raw materials

                    if (dgvProducts.Rows.Count>0)
                    {                        
                        RawMaterials.MfgId = dgvProducts.Rows[0].Cells["ItemCode"].Value.ToString();
                        DataTable dt = RawMaterials.ExistingMfgByID();

                        if (dt.Rows.Count > 0)
                        {
                            dgvRawMaterials.Rows.Clear();
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                double ratio = Convert.ToDouble(dt.Rows[i]["RAW_QTY"]) / Convert.ToDouble(dt.Rows[i]["MFG_QTY"]);
                                double qtyneed = ratio * Convert.ToDouble(dgvProducts.Rows[0].Cells["Qty"].Value.ToString());
                                RawMaterials.RawId=dt.Rows[i]["RAW_ID"].ToString();
                                DataTable rmdt = RawMaterials.StockDtl();
                                string query1 = "SELECT Production_Tax FROM SYS_SETUP";
                                bool tax = Convert.ToBoolean(DbFunctions.GetAValue(query1));
                                string query2 = "SELECT GEN_TAX_MASTER.TaxRate FROM INV_ITEM_DIRECTORY INNER JOIN GEN_TAX_MASTER ON INV_ITEM_DIRECTORY.TaxId=GEN_TAX_MASTER.TaxId WHERE INV_ITEM_DIRECTORY.CODE='"+dt.Rows[i]["RAW_ID"].ToString()+"'";
                                tax_per = Convert.ToDouble(DbFunctions.GetAValue(query2));

                                if (rmdt.Rows.Count > 0)
                                {
                                    for (int j = 0; j < rmdt.Rows.Count; j++)
                                    {
                                        double cost_price;
                                        if (qtyneed > 0)
                                        {
                                            //if (qtyneed < Convert.ToDouble(rmdt.Rows[j]["Qty"]))
                                            //{
                                            if (tax == true || tax_per == 0)
                                            {
                                                cost_price = Convert.ToDouble(rmdt.Rows[j]["Cost_price"]);
                                            }
                                            else
                                            {
                                                cost_price = (Convert.ToDouble(rmdt.Rows[j]["Cost_price"]) / (1 + (tax_per / 100)));
                                            }
                                            if (j != rmdt.Rows.Count - 1)
                                            {
                                                ProductionRawMaterial rawMaterial = new ProductionRawMaterial(
                                                Convert.ToString(dt.Rows[i]["RAW_ID"]),
                                                Convert.ToString(dt.Rows[i]["DESC_ENG"]),
                                                Convert.ToString(rmdt.Rows[j]["batch_id"]),
                                                Convert.ToString(rmdt.Rows[j]["UNIT_CODE"]),
                                                Convert.ToDouble("1.00"),
                                                qtyneed < Convert.ToDouble(rmdt.Rows[j]["Qty"]) ? Convert.ToDouble((qtyneed).ToString(decimalFormat)) : Convert.ToDouble(Convert.ToDouble(rmdt.Rows[j]["Qty"]).ToString(decimalFormat)),
                                                qtyneed < Convert.ToDouble(rmdt.Rows[j]["Qty"]) ? Convert.ToDouble((cost_price).ToString(decimalFormat)) : Convert.ToDouble((cost_price).ToString(decimalFormat)),
                                                Convert.ToDouble("0.00"),
                                                Convert.ToString(rmdt.Rows[j]["UNIT_CODE"]),
                                                Convert.ToDouble("0.00")
                                                );
                                                master.RawMaterials.Add(rawMaterial);
                                                qtyneed = qtyneed - Convert.ToDouble(rmdt.Rows[j]["Qty"]);
                                            }
                                            else
                                            {
                                                if (qtyneed < Convert.ToDouble(rmdt.Rows[j]["Qty"]))
                                                {
                                                    ProductionRawMaterial rawMaterial = new ProductionRawMaterial(
                                                Convert.ToString(dt.Rows[i]["RAW_ID"]),
                                                Convert.ToString(dt.Rows[i]["DESC_ENG"]),
                                                Convert.ToString(rmdt.Rows[j]["batch_id"]),
                                                Convert.ToString(rmdt.Rows[j]["UNIT_CODE"]),
                                                Convert.ToDouble("1.00"),
                                                qtyneed < Convert.ToDouble(rmdt.Rows[j]["Qty"]) ? Convert.ToDouble((qtyneed).ToString(decimalFormat)) : Convert.ToDouble(Convert.ToDouble(rmdt.Rows[j]["Qty"]).ToString(decimalFormat)),
                                                qtyneed < Convert.ToDouble(rmdt.Rows[j]["Qty"]) ? Convert.ToDouble((cost_price).ToString(decimalFormat)) : Convert.ToDouble((cost_price).ToString(decimalFormat)),
                                                Convert.ToDouble("0.00"),
                                                Convert.ToString(rmdt.Rows[j]["UNIT_CODE"]),
                                                Convert.ToDouble("0.00")
                                                );
                                                    master.RawMaterials.Add(rawMaterial);
                                                    qtyneed = qtyneed - Convert.ToDouble(rmdt.Rows[j]["Qty"]);
                                                }
                                                else
                                                {
                                                    MessageBox.Show("Insufficient StockFor Item " + dt.Rows[i]["DESC_ENG"] + "..!", "Sysbizz v1.5");
                                                    ProductionRawMaterial rawMaterial = new ProductionRawMaterial(
                                                Convert.ToString(dt.Rows[i]["RAW_ID"]),
                                                Convert.ToString(dt.Rows[i]["DESC_ENG"]),
                                                Convert.ToString(rmdt.Rows[j]["batch_id"]),
                                                Convert.ToString(rmdt.Rows[j]["UNIT_CODE"]),
                                                Convert.ToDouble("1.00"),
                                                0,
                                                Convert.ToDouble((cost_price).ToString(decimalFormat)),
                                                Convert.ToDouble("0.00"),
                                                Convert.ToString(rmdt.Rows[j]["UNIT_CODE"]),
                                                Convert.ToDouble("0.00")
                                                );
                                                    master.RawMaterials.Add(rawMaterial);                                                    
                                                }
                                            }

                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Insufficient StockFor Item " + dt.Rows[i]["DESC_ENG"] + "..!", "Sysbizz v1.5");

                                    ProductionRawMaterial rawMaterial = new ProductionRawMaterial(
                                                Convert.ToString(dt.Rows[i]["RAW_ID"]),
                                                Convert.ToString(dt.Rows[i]["DESC_ENG"]),
                                                "",
                                                "",
                                                Convert.ToDouble("1.00"),
                                                0,
                                                0,
                                                Convert.ToDouble("0.00"),
                                                "",
                                                Convert.ToDouble("0.00")
                                                );
                                    master.RawMaterials.Add(rawMaterial); 
                                }
                            }
                            Cost_Calculation();
                        }
                    }                    

                }
                
            }
            else
            {
                MessageBox.Show("Only one finished allowed...!!", "Warning SysBizz",  MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            txtOtherDesc.Focus();
        }

        private void resetOtherChargeForm()
        {
            txtOtherDesc.Text = "";
            txtOtherAmount.Text = "";
        }

        private bool isOtherChargesValid()
        {
            if (txtOtherDesc.Text.Equals(""))
            {
                MessageBox.Show("You must write description!");
                txtOtherDesc.Focus();
                return false;
            }
            else if (txtOtherAmount.Text.Equals(""))
            {
                MessageBox.Show("You must enter amount!");
                txtOtherAmount.Focus();
                return false;
            }
            else
            {

                return true;
            }
        }

        private void btnOtherAdd_Click(object sender, EventArgs e)
        {
            if (isOtherChargesValid())
            {
                ProductionOtherCharge otherCharge = new ProductionOtherCharge(txtOtherDesc.Text, Convert.ToDouble(txtOtherAmount.Text));
                master.OtherCharges.Add(otherCharge);
                resetOtherChargeForm();
                Cost_Calculation();
            }


        }

        private void resetRawMaterialForm()
        {
            if (cmbRawItem.SelectedValue != null)
            {
                cmbRawItem.SelectedIndex = 0;
            }
            txtRawCost.Text = "";
            txtRawQty.Text = "";
            txtRawDamageQty.Text = "";
            RawGridselectedRow = -1;
            cmbRawItem.Text = "";
            txtRawItemCode.Text = "";
        }
        
        private bool isRawMaterialValid()
        {
            if (cmbRawItem.Text =="")
            {
                MessageBox.Show("You must select a item!");
                cmbRawItem.Focus();
                return false;
            }
            else if (cmbRawBatch.SelectedIndex < 0)
            {
                MessageBox.Show("You must select a Batch!");
                cmbRawBatch.Focus();
                return false;
            }
            else if (cmbRawUnit.SelectedIndex < 0)
            {
                MessageBox.Show("You must select a Unit!");
                cmbRawUnit.Focus();
                return false;
            }
            else if (txtRawCost.Text.Equals(""))
            {
                MessageBox.Show("You must enter cost price!");
                txtRawCost.Focus();
                return false;
            }
            else if (txtRawQty.Text.Equals(""))
            {
                MessageBox.Show("You must enter Quantity!");
                txtRawQty.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }

        private void btnRawAdd_Click(object sender, EventArgs e)
        {
            if (isRawMaterialValid())
            {
                if (txtRawDamageQty.Text.Equals(""))
                    txtRawDamageQty.Text = "0";
                ProductionRawMaterial rawMaterial = new ProductionRawMaterial(
                    Convert.ToString(txtRawItemCode.Text),
                    cmbRawItem.Text,
                    cmbRawBatch.Text,
                    cmbRawUnit.Text,
                    Convert.ToDouble(cmbRawUnit.SelectedValue),
                    Convert.ToDouble(txtRawQty.Text),
                    Convert.ToDouble(txtRawCost.Text),
                    Convert.ToDouble(cmbRawDamageUnit.SelectedValue),
                    cmbRawDamageUnit.Text,
                    Convert.ToDouble(txtRawDamageQty.Text)
                    );
                if (RawGridselectedRow <= -1)
                {
                    master.RawMaterials.Add(rawMaterial);
                }
                else
                {
                    master.RawMaterials.RemoveAt(RawGridselectedRow);
                    master.RawMaterials.Insert(RawGridselectedRow, rawMaterial);
                }
             
                resetRawMaterialForm();
                Cost_Calculation();
            }
        }

        private void FrmProduction_Load(object sender, EventArgs e)
        {
            LoadProductItems();
            GetAllBatch();
            
            dgvProducts.DataSource = master.Items;
            dgvOtherCharges.DataSource = master.OtherCharges;
            dgvRawMaterials.DataSource = master.RawMaterials;
            hasPriceBatch = General.IsEnabled(Settings.priceBatch);
            hasArabic = General.IsEnabled(Settings.Arabic);
            hasTax = General.IsEnabled(Settings.Tax);
            dataGridItem.Location = new Point(cmbProductItem.Location.X + cmbProductItem.Size.Width / 2, cmbProductItem.Location.Y + cmbProductItem.Size.Height + 3);
            bindgridview();
            dataGridItem.Visible = false;
            dgvRowMaterial.Visible = false;
        }
       
        void GetAllBatch()
        {
            
            //SqlCommand cmd = new SqlCommand();
            //cmd.Connection = conn;
            DataTable BtachDt = new DataTable();
            string query = "SELECT DISTINCT BATCH FROM PRODUCTIONPRODUCTS";
            //cmd.CommandType = CommandType.Text;
            //conn.Open();
            //SqlDataAdapter da = new SqlDataAdapter(cmd);
            try
            {
                //da.Fill(BtachDt);
                BtachDt = DbFunctions.GetDataTable(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
                //conn.Close();
            txtProductBatch.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtProductBatch.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection col = new AutoCompleteStringCollection();
            foreach (DataRow row in BtachDt.Rows)
            {
                col.Add(row[0].ToString()); //assuming required data is in first column
               
            }
            txtProductBatch.AutoCompleteCustomSource = col;
           
        }
        private void LoadProductItems()
        {
            string query = "SELECT CODE, DESC_ENG FROM INV_ITEM_DIRECTORY ORDER BY DESC_ENG ASC";
            DataTable productDataTable = new DataTable();
            //adapter.Fill(productDataTable);
            productDataTable = DbFunctions.GetDataTable(query);
            DataTable rawMaterialDataTable = productDataTable.Copy();

            //cmbProductItem.DisplayMember = "DESC_ENG";
            //cmbProductItem.ValueMember = "CODE";
            //cmbProductItem.DataSource = productDataTable;

           // cmbRawItem.DisplayMember = "DESC_ENG";
           // cmbRawItem.ValueMember = "CODE";
            //cmbRawItem.DataSource = rawMaterialDataTable;
        }
        public void bindgridview()
        {
            try
            {
                //SqlCommand cmd = new SqlCommand();
                //cmd.Connection = conn;
                DataTable productDataTable = new DataTable();
                //cmd.CommandText = "ItemSuggestion";
                // cmd.CommandText = "itemSuggestion_test";
                string query = "item_suggestion_without_stock";
                //cmd.CommandType = CommandType.StoredProcedure;
                //SqlDataAdapter da = new SqlDataAdapter();
                //da.SelectCommand = cmd;
                //da.Fill(productDataTable);
                productDataTable = DbFunctions.GetDataTableProcedure(query);
                //cmd.CommandType = CommandType.Text;
                source2.DataSource = productDataTable;
                dataGridItem.DataSource = source2;
                dgvRowMaterial.DataSource = source2;

                //source.DataSource = productDataTable;
                //dataGridItem.DataSource = source;
            //    dataGridItem.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridItem.Columns["ITEM_CODE"].HeaderText = "Item Code";
                dataGridItem.Columns["TaxId"].Visible = false;

                if (!hasArabic)
                {
                    dataGridItem.Columns["DESC_ARB"].Visible = false;
                }

                if (!hasTax)
                {
                    dataGridItem.Columns["TaxRate"].Visible = false;

                }
                dataGridItem.Columns["HASSERIAL"].Visible = false;
                dataGridItem.Columns["Type"].Visible = false;
                dataGridItem.Columns["Category"].Visible = false;
                dataGridItem.Columns["Group"].Visible = false;
                dataGridItem.Columns["Trademark"].Visible = false;
                dataGridItem.Columns["Stock"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridItem.Columns["TaxId"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridItem.Columns["ITEM NAME"].DisplayIndex = 1;

                dataGridItem.ClearSelection();
            }
            catch { }
        }
        private void cmbProductItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cmd.CommandText = "SELECT DISTINCT batch_id FROM tblStock WHERE Item_id = @itemid ORDER BY batch_id DESC";
            //cmd.Parameters.Clear();
            //cmd.Parameters.AddWithValue("@itemid", cmbProductItem.SelectedValue);
            //DataTable batchTable = new DataTable();
            //adapter.Fill(batchTable);
            //cmbProductBatch.DisplayMember = "batch_id";
            //cmbProductBatch.ValueMember = "batch_id";
            //cmbProductBatch.DataSource = batchTable;

            string query = "SELECT PACK_SIZE, UNIT_CODE FROM INV_ITEM_DIRECTORY_UNITS WHERE ITEM_CODE = @itemid";
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@itemid", cmbProductItem.SelectedValue);
            //cmd.Parameters.Clear();
            //cmd.Parameters.AddWithValue("@itemid", cmbProductItem.SelectedValue);
            DataTable unitTable = new DataTable();
            //adapter.Fill(unitTable);
            unitTable = DbFunctions.GetDataTable(query, Parameters);
            cmbProductUnit.DisplayMember = "UNIT_CODE";
            cmbProductUnit.ValueMember = "PACK_SIZE";
            cmbProductUnit.DataSource = unitTable;




        }
        private void bindUnit(string code)
        {
            //cmd.Dispose();
            //cmd.CommandText = "";
            //cmd.CommandType = CommandType.Text;
            string query = "SELECT PACK_SIZE, UNIT_CODE FROM INV_ITEM_DIRECTORY_UNITS WHERE ITEM_CODE = @itemid";
            //cmd.Parameters.Clear();
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@itemid", code);
            //cmd.Parameters.AddWithValue("@itemid", code);
            DataTable unitTable = new DataTable();
            //adapter.Fill(unitTable);
            unitTable = DbFunctions.GetDataTable(query, Parameters);
            cmbProductUnit.DisplayMember = "UNIT_CODE";
            cmbProductUnit.ValueMember = "PACK_SIZE";
            cmbProductUnit.DataSource = unitTable;

        }

        private void bindUnit_Raw(string code)
        {
            //cmd.Dispose();
            //cmd.CommandText = "";
            //cmd.CommandType = CommandType.Text;
            string query = "SELECT PACK_SIZE, UNIT_CODE FROM INV_ITEM_DIRECTORY_UNITS WHERE ITEM_CODE = @itemid";
            //cmd.Parameters.Clear();
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@itemid", code);
            //cmd.Parameters.AddWithValue("@itemid", code);
            DataTable unitTable = new DataTable();
            unitTable = DbFunctions.GetDataTable(query, Parameters);
            //adapter.Fill(unitTable);
            cmbRawUnit.DisplayMember = "UNIT_CODE";
            cmbRawUnit.ValueMember = "PACK_SIZE";
            cmbRawUnit.DataSource = unitTable;

            DataTable damageUnitTable = unitTable.Copy();
            cmbRawDamageUnit.DisplayMember = "UNIT_CODE";
            cmbRawDamageUnit.ValueMember = "PACK_SIZE";
            cmbRawDamageUnit.DataSource = damageUnitTable;

        }


        public void fillBatch(string code)
        {
            string query = "SELECT DISTINCT batch_id FROM tblStock WHERE Item_id = @itemid ORDER BY batch_id DESC";
            //cmd.Parameters.Clear();
            //cmd.Parameters.AddWithValue("@itemid", code);
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@itemid", code);
            DataTable batchTable = new DataTable();
            batchTable = DbFunctions.GetDataTable(query, Parameters);
            //adapter.Fill(batchTable);
            cmbRawBatch.DisplayMember = "batch_id";
            cmbRawBatch.ValueMember = "batch_id";
            cmbRawBatch.DataSource = batchTable;
        }
        public void fillManBatch(string code)
        {
            string query = "SELECT  batch_id,ManBatch FROM tblStock WHERE Item_id = @itemid ORDER BY batch_id DESC";
            //cmd.Parameters.Clear();
            //cmd.Parameters.AddWithValue("@itemid", code);
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@itemid", code);
            DataTable batchTable = new DataTable();
            batchTable = DbFunctions.GetDataTable(query, Parameters);
            //adapter.Fill(batchTable);
            combManBatch.DisplayMember = "ManBatch";
            combManBatch.ValueMember = "batch_id";
            combManBatch.DataSource = batchTable;
        }
        private void cmbRawItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cmd.CommandText = "SELECT DISTINCT batch_id FROM tblStock WHERE Item_id = @itemid ORDER BY batch_id DESC";
            //cmd.Parameters.Clear();
            //cmd.Parameters.AddWithValue("@itemid", cmbRawItem.SelectedValue);
            //DataTable batchTable = new DataTable();
            //adapter.Fill(batchTable);
            //cmbRawBatch.DisplayMember = "batch_id";
            //cmbRawBatch.ValueMember = "batch_id";
            //cmbRawBatch.DataSource = batchTable;

            //cmd.CommandText = "SELECT PACK_SIZE, UNIT_CODE FROM INV_ITEM_DIRECTORY_UNITS WHERE ITEM_CODE = @itemid";
            //cmd.Parameters.Clear();
            //cmd.Parameters.AddWithValue("@itemid", cmbRawItem.SelectedValue);
            //DataTable unitTable = new DataTable();
            //adapter.Fill(unitTable);
            //cmbRawUnit.DisplayMember = "UNIT_CODE";
            //cmbRawUnit.ValueMember = "PACK_SIZE";
            //cmbRawUnit.DataSource = unitTable;

            //DataTable damageUnitTable = unitTable.Copy();
            //cmbRawDamageUnit.DisplayMember = "UNIT_CODE";
            //cmbRawDamageUnit.ValueMember = "PACK_SIZE";
            //cmbRawDamageUnit.DataSource = damageUnitTable;
        }

        private void cmbRawBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query1 = "SELECT Production_Tax FROM SYS_SETUP";
            bool tax = Convert.ToBoolean(DbFunctions.GetAValue(query1));           

            string query = "SELECT ISNULL(Cost_price, 0) FROM tblStock WHERE Item_id = @itemid AND batch_id = @batchid";
            //cmd.Parameters.Clear();
            //cmd.Parameters.AddWithValue("@itemid", txtRawItemCode.Text);
            //cmd.Parameters.AddWithValue("@batchid", cmbRawBatch.Text);
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@itemid", txtRawItemCode.Text);
            Parameters.Add("@batchid", cmbRawBatch.Text);
            //conn.Open();
            //txtRawCost.Text = Convert.ToDouble(cmd.ExecuteScalar()).ToString(decimalFormat); ;
            //conn.Close();
            if (tax==true||tax_per==0)
            {
                txtRawCost.Text = Convert.ToDouble(DbFunctions.GetAValue(query, Parameters)).ToString(decimalFormat);
            }
            else
            {
                txtRawCost.Text = (Convert.ToDouble(DbFunctions.GetAValue(query, Parameters)) / (1 + (tax_per / 100))).ToString(decimalFormat);
            }
            query = "SELECT ISNULL(Qty, 0) FROM tblStock WHERE Item_id = @itemid AND batch_id = @batchid";
            //cmd.Parameters.Clear();
            Parameters = new Dictionary<string, object>();
            Parameters.Add("@itemid", txtRawItemCode.Text);
            Parameters.Add("@batchid", cmbRawBatch.Text);
            //conn.Open();
            txt_stock.Text = Convert.ToDouble(DbFunctions.GetAValue(query, Parameters)).ToString(decimalFormat);
            //conn.Close();
            combManBatch.SelectedValue = cmbRawBatch.SelectedValue.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            master.hasPriceBatch = hasPriceBatch;
            master.Save();
           
            ///TODO: update the stock.

            master.ResetDocId();
            dtpDate.Value = DateTime.Now;
            txtDocNo.Text = "";
            MessageBox.Show("Production Saved!");
            btnClear.PerformClick();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            master.ResetDocId();
            resetProductForm();
            resetOtherChargeForm();
            resetRawMaterialForm();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure, you want to delete this production?", "Delete Production", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                master.DeleteInvoice();
                master.ResetDocId();
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            FrmProductionBrowse browse = new FrmProductionBrowse();
            if (browse.ShowDialog() == DialogResult.OK)
            {
                master.DocId = browse.DocId;
                txtDocNo.Text = browse.DocId;
                dtpDate.Value = Convert.ToDateTime(browse.PDate);
            }
        }

        private void dgvProducts_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (dgvProducts.CurrentRow != null)
                {
                    master.RemoveItem(dgvProducts.CurrentRow.Index);
                }
            }
        }

        private void dgvOtherCharges_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (dgvOtherCharges.CurrentRow != null)
                {
                    master.RemoveOtherCharge(dgvOtherCharges.CurrentRow.Index);
                }
            }
        }

        private void dgvRawMaterials_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (dgvRawMaterials.CurrentRow != null)
                {
                    master.RemoveRawMaterial(dgvRawMaterials.CurrentRow.Index);
                }
            }
        }
        
        private void dgvProducts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvProducts.Rows.Count>0)
            {
                edit = true;
                DataGridViewCellCollection c = dgvProducts.CurrentRow.Cells;
                selectedRow = dgvProducts.CurrentRow.Index;
                txtItenCode.Text = Convert.ToString(c["ItemCode"].Value);
                bindUnit(txtItenCode.Text);
                cmbProductItem.Text = Convert.ToString(c["ItemName"].Value);
                txtProductBatch.Text = Convert.ToString(c["Batch"].Value);
                ExDate.Value = Convert.ToDateTime(c["ExDate"].Value);
                cmbProductUnit.Text = Convert.ToString(c["UOM"].Value);
                txtProductCost.Text = Convert.ToString(c["CostPrice"].Value);
                txtProductQty.Text = Convert.ToString(c["Qty"].Value);
                txtProductMRP.Text = Convert.ToString(c["MRP"].Value);
                dataGridItem.Visible = false;
                txtProductBatch.Focus();
            }
        }

        private void dgvRawMaterials_DoubleClick(object sender, EventArgs e)
        {
            if (dgvRawMaterials.Rows.Count>0)
            {
                 DataGridViewCellCollection c = dgvRawMaterials.CurrentRow.Cells;
                 RawGridselectedRow = dgvRawMaterials.CurrentRow.Index;
                 txtRawItemCode.Text = Convert.ToString(c["ItemCode"].Value);
                 fillBatch(txtRawItemCode.Text);
                 fillManBatch(txtRawItemCode.Text);
                 bindUnit_Raw(txtRawItemCode.Text);
                 cmbRawItem.Text = Convert.ToString(c["ItemName"].Value);
                 cmbRawBatch.Text = Convert.ToString(c["Batch"].Value);
                 cmbRawUnit.Text = Convert.ToString(c["UOM"].Value);
                 txtRawCost.Text = Convert.ToString(c["CostPrice"].Value);
                 txtRawQty.Text = Convert.ToString(c["Qty"].Value);
                 cmbRawDamageUnit.Text = Convert.ToString(c["DamageUOM"].Value);
                 txtRawDamageQty.Text = Convert.ToString(c["DamageQty"].Value);
            }
        }
        private void common_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Down)
            {
                if (sender is ComboBox)
                {
                    ComboBox cmb = (sender as ComboBox);
                    switch(cmb.Name)
                    {
                        case "cmbProductItem":
                            showPanelSuggestion();
                            break;
                        case "cmbRawItem":
                            showPanelSuggestion_RawMaterials();
                            break;
                    }
                }
            }
            else if (e.KeyCode == Keys.Up)
            {
                if (sender is ComboBox)
                {
                    ComboBox cmb = (sender as ComboBox);
                    switch(cmb.Name)
                    {
                    case "cmbProductItem":
                    if (dataGridItem.RowCount > 0)
                    {
                        int r = dataGridItem.CurrentCell.RowIndex;
                        if (r > 0) //check for index out of range
                            dataGridItem.CurrentCell = dataGridItem[2, r - 1];
                    }
                    break;

                    case "cmbRawItem":
                    if (dgvRowMaterial.RowCount > 0)
                    {
                        int r = dgvRowMaterial.CurrentCell.RowIndex;
                        if (r > 0) //check for index out of range
                            dgvRowMaterial.CurrentCell = dgvRowMaterial[2, r - 1];
                    }
                    break;
                    }
                }
            }
            else if (e.KeyCode == Keys.Enter)
            {
                if (sender is ComboBox)
                {
                    ComboBox cmb = (sender as ComboBox);
                    switch(cmb.Name)
                    {
                    case "cmbProductItem":
                    if (dataGridItem.RowCount > 0&&dataGridItem.Visible==true)
                    {
                        addItemToDataGridView(e);
                        txtProductBatch.Focus();
                    }
                    break;
                        case "cmbProductUnit":
                    txtProductQty.Focus();
                    break;
                        case "cmbRawItem":
                    if (dgvRowMaterial.RowCount > 0 && dgvRowMaterial.Visible == true)
                    {
                        addItemToDataGridView_RawMaterial(e);
                        txtRawQty.Focus();
                    }
                    break;
                    }
                }
               else if (sender is TextBox)
                {
                    TextBox txt = (sender as TextBox);
                    switch (txt.Name)
                    {
                        case "txtProductBatch":
                            ExDate.Focus();
                            break;
                        case"txtProductCost":
                            txtProductQty.Focus();
                            break;
                        case "txtProductQty":
                            txtProductMRP.Focus();
                            break;
                        case "txtProductMRP":
                            btnProductAdd.PerformClick();
                            break;
                    }
                }
                else if (sender is DateTimePicker)
                {
                    DateTimePicker dtPicker = (sender as DateTimePicker);
                    switch (dtPicker.Name)
                    {
                        case "ExDate":
                            cmbProductUnit.Focus();
                            break;
                    }
                }
            }
        }
        private void addItemToDataGridView(KeyEventArgs e)
        {
            string itemcode = dataGridItem.CurrentRow.Cells["ITEM_CODE"].Value.ToString();
            cmbProductItem.Text = dataGridItem.CurrentRow.Cells["ITEM NAME"].Value.ToString();
            txtItenCode.Text = itemcode;
            bindUnit(itemcode);
            dataGridItem.Visible = false;
        }

        private void addItemToDataGridView_RawMaterial(KeyEventArgs e)
        {
            string itemcode = dgvRowMaterial.CurrentRow.Cells["ITEM_CODE"].Value.ToString();
            cmbRawItem.Text = dgvRowMaterial.CurrentRow.Cells["ITEM NAME"].Value.ToString();
            tax_per = Convert.ToDouble(dgvRowMaterial.CurrentRow.Cells["TaxRate"].Value.ToString());
            txtRawItemCode.Text = itemcode;
            bindUnit_Raw(itemcode);
            txtRawItemCode.Text = itemcode;
            fillBatch(itemcode);
            fillManBatch(itemcode);
            dgvRowMaterial.Visible = false;

            
        }


        private void showPanelSuggestion()
        {
            if (dataGridItem.Text == "" && dataGridItem.Visible == false)
            {
                source2.Filter = "";
      
                dataGridItem.Visible = true;
             
                if (dataGridItem.Rows.Count > 0)
                {
                    dataGridItem.CurrentCell = dataGridItem.Rows[0].Cells[2];
          
                    dataGridItem.Columns["TYPE"].Visible = false;
                    dataGridItem.Columns["CATEGORY"].Visible = false;
                    dataGridItem.Columns["GROUP"].Visible = false;
                    dataGridItem.Columns["TRADEMARK"].Visible = false;
                    dataGridItem.CurrentCell = dataGridItem[2, 0];
                }
            }
            else if (dataGridItem.Visible == true)
            {
                if (dataGridItem.RowCount > 0)
                {
                    int r = dataGridItem.CurrentCell.RowIndex;
                    if (r < dataGridItem.Rows.Count - 1) 
                        dataGridItem.CurrentCell = dataGridItem[2, r + 1];
                }
            }
        }

        private void showPanelSuggestion_RawMaterials()
        {
            if (dgvRowMaterial.Text == "" && dgvRowMaterial.Visible == false)
            {
                source2.Filter = "";

                dgvRowMaterial.Visible = true;

                if (dgvRowMaterial.Rows.Count > 0)
                {
                    dgvRowMaterial.CurrentCell = dgvRowMaterial.Rows[0].Cells[2];

                    dgvRowMaterial.Columns["TYPE"].Visible = false;
                    dgvRowMaterial.Columns["CATEGORY"].Visible = false;
                    dgvRowMaterial.Columns["GROUP"].Visible = false;
                    dgvRowMaterial.Columns["TRADEMARK"].Visible = false;
                    dgvRowMaterial.CurrentCell = dgvRowMaterial[2, 0];
                }
            }
            else if (dgvRowMaterial.Visible == true)
            {
                if (dgvRowMaterial.RowCount > 0)
                {
                    int r = dgvRowMaterial.CurrentCell.RowIndex;
                    if (r < dgvRowMaterial.Rows.Count - 1)
                        dgvRowMaterial.CurrentCell = dgvRowMaterial[2, r + 1];
                }
            }
        }


        private void cmbProductItem_TextChanged(object sender, EventArgs e)
        {
            if (cmbProductItem.Text == "")
            {
                dataGridItem.Visible = false;
            }
            else
            {                
                try
                {
                    bindgridview();
                    dataGridItem.Visible = true;
                    source2.Filter = string.Format("[ITEM NAME] LIKE '%{0}%' ", cmbProductItem.Text);                //dataGridItem.ClearSelection();
                    dataGridItem.Columns["ITEM NAME"].Width = 250;
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

        }

        private void dataGridItem_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void dataGridItem_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridItem.RowCount>0)
            {
                
                addItemToDataGridView(null);
                txtProductBatch.Focus();
            }
        }

        private void dgvRawMaterials_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        public void Cost_Calculation()
        {
            if (dgvProducts.Rows.Count > 0)
            {
                cost = 0;
                if (dgvOtherCharges.RowCount > 0 || dgvRawMaterials.RowCount > 0)
                {
                    for (int i = 0; i < dgvOtherCharges.RowCount; i++)
                    {
                        cost = cost + Convert.ToDecimal(dgvOtherCharges.Rows[i].Cells["Amount"].Value);
                    }
                    for (int i = 0; i < dgvRawMaterials.RowCount; i++)
                    {
                        cost = cost + (Convert.ToDecimal(dgvRawMaterials.Rows[i].Cells["CostPrice"].Value) * (Convert.ToDecimal(dgvRawMaterials.Rows[i].Cells["Qty"].Value) + Convert.ToDecimal(dgvRawMaterials.Rows[i].Cells["DamageQty"].Value)));
                    }
                    cost = cost / Convert.ToDecimal(dgvProducts.Rows[0].Cells["Qty"].Value);
                }
                if (dgvProducts.RowCount > 0)
                {
                    dgvProducts.Rows[0].Cells["CostPrice"].Value = cost.ToString(decimalFormat);

                    ProductionItem item = new ProductionItem(
                                dgvProducts.Rows[0].Cells["ItemCode"].Value.ToString(), dgvProducts.Rows[0].Cells["ItemName"].Value.ToString(),
                                dgvProducts.Rows[0].Cells["Batch"].Value.ToString(), "", Convert.ToDateTime(dgvProducts.Rows[0].Cells["ExDate"].Value.ToString()), dgvProducts.Rows[0].Cells["UOM"].Value.ToString(), Convert.ToDouble(dgvProducts.Rows[0].Cells["PackSize"].Value), Convert.ToDouble(dgvProducts.Rows[0].Cells["Qty"].Value),
                                Convert.ToDouble(dgvProducts.Rows[0].Cells["CostPrice"].Value), Convert.ToDouble(dgvProducts.Rows[0].Cells["MRP"].Value), Convert.ToDouble(dgvProducts.Rows[0].Cells["Qty"].Value) * Convert.ToDouble(dgvProducts.Rows[0].Cells["CostPrice"].Value));

                    master.AddItemByIndex(item, 0);
                }
            }
        }

        private void txtOtherDesc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                txtOtherAmount.Focus();
            }
        }        

        private void txtOtherAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                btnOtherAdd.PerformClick();
            }
        }

        private void txtRawItemCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                cmbRawBatch.Focus();
            }
        }

        private void cmbRawItem_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void cmbRawBatch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                cmbRawUnit.Focus();
            }
        }

        private void cmbRawUnit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                txtRawCost.Focus();
            }
        }

        private void txtRawCost_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                txtRawQty.Focus();
            }
        }

        private void txtRawQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                cmbRawDamageUnit.Focus();
            }
        }

        private void cmbRawDamageUnit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                txtRawDamageQty.Focus();
            }
        }

        private void txtRawDamageQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                btnRawAdd.PerformClick();
            }
        }

        private void cmbRawItem_TextChanged(object sender, EventArgs e)
        {
            if (cmbRawItem.Text == "")
            {
                dgvRowMaterial.Visible = false;
            }
            else
            {
                //  bindgridview();
                dgvRowMaterial.Visible = true;
                try
                {
                    source2.Filter = string.Format("[ITEM NAME] LIKE '%{0}%' ", cmbRawItem.Text.Replace("'", "''").Replace("*", "[*]"));
                    //dataGridItem.ClearSelection();
                    dgvRowMaterial.Columns["ITEM NAME"].Width = 250;
                }
                catch
                {
                }
            }
        }

        private void dgvRowMaterial_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridItem.RowCount > 0)
            {
                addItemToDataGridView_RawMaterial(null);
                txtRawQty.Focus();
            }
        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            master.Date = dtpDate.Value;
        }

        private void label27_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void combManBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combManBatch.SelectedIndex>-1)
            {
                cmbRawBatch.SelectedValue = combManBatch.SelectedValue.ToString();
            }
        }

        private void btnMovement_Click(object sender, EventArgs e)
        {
            frmProductionMovement pmr = new frmProductionMovement();
            pmr.ShowDialog();
        }

        private void txtItenCode_TextChanged(object sender, EventArgs e)
        {
            //if (txtItenCode.Text != "")
            //{
            //    RawMaterials.MfgId = txtItenCode.Text;
            //    DataTable dt = RawMaterials.ExistingMfgByID();

            //    if (dt.Rows.Count > 0)
            //    {
            //        for (int i = 0; i < dt.Rows.Count; i++)
            //        {
            //            ProductionRawMaterial rawMaterial = new ProductionRawMaterial(
            //            Convert.ToString(dt.Rows[i][2]),
            //            Convert.ToString(dt.Rows[i][3]),
            //            "",
            //            "",
            //            Convert.ToDouble("0.00"),
            //            Convert.ToDouble("0.00"),
            //            Convert.ToDouble("0.00"),
            //            Convert.ToDouble("0.00"),
            //            cmbRawDamageUnit.Text,
            //            Convert.ToDouble("0.00")
            //            );
            //            master.RawMaterials.Add(rawMaterial);
            //        }
            //    }
            //}
            
        }

        private void txtProductQty_TextChanged(object sender, EventArgs e)
        {
            if (txtProductQty.Text != "")
            {
 
            }
        }

        private void removeRow_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (dgvRawMaterials.Rows.Count > 0 && dgvRawMaterials.CurrentRow != null)
            {
                dgvRawMaterials.Rows.Remove(dgvRawMaterials.CurrentRow);
                Cost_Calculation();
            }
        }

       
       
    }
}
