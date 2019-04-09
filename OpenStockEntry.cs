using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using Sys_Sols_Inventory.Model;
namespace Sys_Sols_Inventory
{
    public partial class OpenStockEntry : Form
    {
        #region properties declaration
        private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        private SqlCommand cmd = new SqlCommand();
        private SqlDataAdapter adapter = new SqlDataAdapter();
        private DataTable table = new DataTable();
        InvStkTrxHdrDB stkHdr = new InvStkTrxHdrDB();
        StockDB stkdb = new StockDB();
        InvStkTrxDtlDB stkDtl = new InvStkTrxDtlDB();
        private string ID = "";
        private int selectedRow = -1;
        private bool HasTax = true;
        private bool HasBatch = true;
        private bool onlyFloat = false;
        public bool HasArabic = true;
        bool hasPriceBatch = false;
        private DataTable ratesTable = new DataTable();
        #endregion
        Initial mdi = (Initial)Application.OpenForms["Initial"];
        Login lg = (Login)Application.OpenForms["Login"];
        

        #region custom methods
        private bool ItemValid()
        {
            bool batch = true;
            if (HasBatch)
            {
                if (BATCH.Text.Trim() == "")
                {
                    batch = false;
                }
            }
            if (ITEM_CODE.Text.Trim() != ""  && UOM.Text.Trim() != "" && QUANTITY.Text.Trim() != "" && PRICE.Text.Trim() != "")
            {
                return true;
            }
            else
            {
                MessageBox.Show("Please enter all the fields!");
                return false;
            }
        }

        private void addItem()
        {
            if (ItemValid())
            {
                int i = 0;
                if (selectedRow == -1)
                {
                    i = dgItems.Rows.Add(new DataGridViewRow());
                }
                else
                {
                    i = selectedRow;
                }
                DataGridViewCellCollection c = dgItems.Rows[i].Cells;
                c["cCode"].Value = ITEM_CODE.Text;
                c["cName"].Value = txtItemName.Text;
                if (HasBatch)
                {
                    c["cBatch"].Value = BATCH.Text;
                    c["cExp"].Value = EXPIRY_DATE.Value.ToString("dd/MM/yyyy");
                }
                c["cUnit"].Value = UOM.Text;
                c["cQty"].Value = QUANTITY.Text;
                c["cPrice"].Value = PRICE.Text;
                if (HasTax)
                {
                    if (TAX_PER.Text.Trim() == "")
                    {
                        c["cTax"].Value = "0";
                        c["ctaxamount"].Value = "0";
                    }
                    else
                    {
                        c["cTax"].Value = TAX_PER.Text;
                        c["ctaxamount"].Value = TAX_AMT.Text;
                    }
                    
                }
                c["ctotal"].Value = ITEM_TOTAL.Text;
                c["cost_price"].Value = Convert.ToDouble(ITEM_TOTAL.Text) / Convert.ToDouble(QUANTITY.Text);
                c["uomQty"].Value = UOM.SelectedValue;
                int colm_count = dgItems.Columns.Count;
                for (int K = 1; K < dgRates.ColumnCount; K++)
                {
                    for (int j = 13; j < colm_count; j++)
                    {
                        if (dgRates.Columns[K].HeaderText.ToString() == dgItems.Columns[j].HeaderText)
                        {
                            double rate = 0;
                            if (dgRates.Rows[0].Cells[K].Value == null || dgRates.Rows[0].Cells[K].Value == "")
                            {
                                rate = 0;
                            }
                            else
                            {
                                rate = Convert.ToDouble(dgRates.Rows[0].Cells[K].Value);
                            }
                            c[dgItems.Columns[j].Name.ToString()].Value = rate.ToString();
                        }
                    }

                }
                foreach (DataGridViewRow row in dgRates.Rows)
                {
                    if (selectedRow == -1)
                    {
                        object[] values = new object[dgRates.Columns.Count + 2];
                        values[0] = i;
                        values[1] = ITEM_CODE.Text;
                        for (int j = 0; j < dgRates.Columns.Count; j++)
                        {
                            values[j + 2] = row.Cells[j].Value;
                        }
                        ratesTable.Rows.Add(values);
                    }
                    else
                    {
                        //DataRow values = ratesTable.Select("INDEX = '" + selectedRow + "' AND UNIT_CODE = '" + row.Cells[0].Value + "'").First();
                        //values[0] = selectedRow;
                        //values[1] = ITEM_CODE.Text;
                        //for (int j = 0; j < dgRates.Columns.Count; j++)
                        //{
                        //    values[j + 2] = row.Cells[j].Value;
                        //}
                    }
                }

                if (HasTax)
                {
                    double totalTax = 0;
                    double totalAmt = 0;
                    foreach (DataGridViewRow row in dgItems.Rows)
                    {
                        DataGridViewCellCollection cell = row.Cells;
                        totalTax = totalTax + Convert.ToDouble(cell["ctaxamount"].Value);
                        totalAmt = totalAmt + Convert.ToDouble(cell["ctotal"].Value);
                    }
                    TOTAL_TAX.Text = totalTax.ToString();
                    TOTAL_AMOUNT.Text = totalAmt.ToString();
                }
                else
                {
                    double totalAmt = 0;
                    foreach (DataGridViewRow row in dgItems.Rows)
                    {
                        DataGridViewCellCollection cell = row.Cells;
                      
                        totalAmt = totalAmt + Convert.ToDouble(cell["ctotal"].Value);
                    }
                   
                    TOTAL_AMOUNT.Text = totalAmt.ToString();
                }
                itemClear();
            }
        }

        private bool valid()
        {
            if (dgItems.RowCount > 0)
                return true;
            else
                return false;
        }

        private void itemClear()
        {
            selectedRow = -1;
            ITEM_CODE.Text = "";
            txtItemName.Text = "";
            BATCH.Text = "";
            EXPIRY_DATE.Value = DateTime.Today;
            UOM.Text = "";
            QUANTITY.Text = "";
            PRICE.Text = "";
            TAX_PER.Text = "0";
            TAX_AMT.Text = "0.00";
            ITEM_TOTAL.Text = "";
            dgRates.Rows.Clear();
            table.Rows.Clear();
        }

        #endregion

        #region construct,load,keydown,edicontrolshowing
        public OpenStockEntry()
        {
            InitializeComponent();
            cmd.Connection = conn;
            adapter.SelectCommand = cmd;
        }

        private void OpenStockEntry_Load(object sender, EventArgs e)
        {
            Class.CompanySetup CompStep = new Class.CompanySetup();
            DOC_DATE_GRE.Text = CompStep.GettDate();

            HasTax = General.IsEnabled(Settings.Tax);
            HasBatch = General.IsEnabled(Settings.Batch);
            HasArabic = General.IsEnabled(Settings.Arabic);
            hasPriceBatch = General.IsEnabled(Settings.priceBatch);
            if (!HasTax)
            {
                panTax.Visible = false;
                ctax.Visible = false;
                ctaxamount.Visible = false;
                panTotalTax.Visible = false;
            }

            if (!HasBatch)
            {
                panBatch.Visible = false;
                cBatch.Visible = false;
                cExp.Visible = false;
            }
            if (!HasArabic)
                DOC_DATE_HIJ.Enabled = false;
            PnlArabic.Visible = false;

            ratesTable.Columns.Add("INDEX");
            ratesTable.Columns.Add("ITEM_CODE");
            ratesTable.Columns.Add("UNIT_CODE");

            //conn.Open();
            //cmd.CommandText = "SELECT CODE,DESC_ENG FROM GEN_PRICE_TYPE";
            SqlDataReader r = stkHdr.selecrGenPriceType();
            while (r.Read())
            {
                ratesTable.Columns.Add(r[0].ToString());
                dgRates.Columns.Add(Convert.ToString(r[0]), Convert.ToString(r[0]));
            }
           // conn.Close();
            DbFunctions.CloseConnection();
            QUANTITY.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            PRICE.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            if (HasTax)
            {
                TAX_PER.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            }
            addRate_column();
        }
        public void addRate_column()
        {

            //cmd.CommandText = "SELECT CODE,DESC_ENG FROM GEN_PRICE_TYPE";
            //conn.Open();
            SqlDataReader r = stkHdr.selecrGenPriceType();
            while (r.Read())
            {
                //dgRates.Columns.Add(r["CODE"].ToString(), r["DESC_ENG"].ToString());
                DataGridViewColumn col = new DataGridViewTextBoxColumn();
                col.Name = r["CODE"].ToString();
                col.HeaderText = r["CODE"].ToString();
                col.Width = 50;
                dgItems.Columns.Add(col);
            }
           // conn.Close();
            DbFunctions.CloseConnection();
        }
        private void Entry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                
                if (sender is DateTimePicker)
                {
                    UOM.Focus();
                    return;
                }
                else if (sender is ComponentFactory.Krypton.Toolkit.KryptonComboBox)
                {
                    QUANTITY.Focus();
                    return;
                }

                string name = (sender as ComponentFactory.Krypton.Toolkit.KryptonTextBox).Name;

                switch (name)
                {
                    case "ITEM_CODE":
                        if (HasBatch)
                        {
                            BATCH.Focus();
                        }
                        else
                        {
                            UOM.Focus();
                        }
                        break;
                    case "BATCH":
                        EXPIRY_DATE.Focus();
                        break;
                    case "EXPIRY_DATE":
                        UOM.Focus();
                        break;
                    case "UOM":
                        QUANTITY.Focus();
                        break;
                    case "QUANTITY":
                        PRICE.Focus();
                        break;
                    case "PRICE":
                        if (HasTax)
                        {
                            TAX_PER.Focus();
                        }
                        else
                        {
                            ITEM_TOTAL.Focus();
                        }
                        break;
                    case "TAX_PER":
                        TAX_AMT.Focus();
                        break;
                    case "TAX_AMT":
                        ITEM_TOTAL.Focus();
                        break;
                    case "ITEM_TOTAL":
                        addItem();
                        ITEM_CODE.Focus();
                        break;
                    default:
                        break;
                }
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.F1)
            {
                btnItemCode.PerformClick();
            }
        }

        private void dgRates_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridViewTextBoxEditingControl txtBox = (e.Control as DataGridViewTextBoxEditingControl);
            if (!onlyFloat)
            {
                txtBox.KeyPress += new KeyPressEventHandler(General.CellOnlyFloat);
                onlyFloat = true;
            }
        }
        #endregion     

        #region click events
        string TaxId;
        private void btnItemCode_Click(object sender, EventArgs e)
        {
            ItemMasterHelp h = new ItemMasterHelp(0);
            if (h.ShowDialog() == DialogResult.OK && h.c != null)
            {
                ITEM_CODE.Text = Convert.ToString(h.c["ITEM_CODE"].Value);
                txtItemName.Text = Convert.ToString(h.c["ITEM_NAME"].Value);
                TaxId = Convert.ToString(h.c["TaxId"].Value);
                GetTaxRate();
                QUANTITY.Focus();
            }
        }

        public void GetTaxRate()
        {
            try
            {
                //conn.Open();
                //cmd.CommandText = "SELECT TaxRate from GEN_TAX_MASTER where TaxId=" + TaxId;
                //cmd.CommandType = CommandType.Text;
                stkHdr.TaxId = TaxId;
                SqlDataReader r = stkHdr.selectTaxRate();
                while (r.Read())
                {
                   TAX_PER.Text = r[0].ToString();
                }
               // conn.Close();
                DbFunctions.CloseConnection();
            }
            catch (Exception ex)
            {
               // conn.Close();
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (valid())
            {
                /*if (General.DocExists(DOC_NO.Text, ID, "INV_STK_TRX_HDR"))
                {
                    MessageBox.Show("A stock with the same Doc. No. Already Exists.");
                    return;
                }*/
                StockEntry se = new StockEntry();
                stkHdr.DocNo = DOC_NO.Text;
                stkHdr.DocDateGre = DOC_DATE_GRE.Value;
                stkHdr.DocDateHij = DOC_DATE_HIJ.Text;
                stkHdr.DocReference = DOC_REFERENCE.Text;
                stkHdr.Notes = NOTES.Text;
                stkHdr.TaxAmount = Convert.ToDecimal(TOTAL_TAX.Text);
                stkHdr.TotalAmount = Convert.ToDecimal(TOTAL_AMOUNT.Text);
                stkHdr.DocType = "INV.STK.OPN";
                stkHdr.Branch = lg.Branch;
                if (ID == "")
                {
                    DOC_NO.Text = General.generateStockID();
                    stkHdr.DocNo = General.generateStockID();
                    //cmd.Connection = conn;
                    //cmd.CommandText = "INSERT INTO INV_STK_TRX_HDR(DOC_NO,DOC_DATE_GRE,DOC_DATE_HIJ,DOC_REFERENCE,NOTES,TAX_AMOUNT,TOTAL_AMOUNT,DOC_TYPE,BRANCH) VALUES('" + DOC_NO.Text + "','" + DOC_DATE_GRE.Value.ToString("MM/dd/yyyy") + "','" + DOC_DATE_HIJ.Text + "','" + DOC_REFERENCE.Text + "','" + NOTES.Text + "','"+TOTAL_TAX.Text+"','" + TOTAL_AMOUNT.Text + "','INV.STK.OPN','"+lg.Branch+"')";
                    //if (conn.State == ConnectionState.Open)
                    //{
                    //    conn.Close();
                    //}
                    //conn.Open();
                    //cmd.ExecuteNonQuery();
                    //if (conn.State == ConnectionState.Open)
                    //{
                    //    conn.Close();
                    //}
                    stkHdr.InsertFromOpeningEntry();
                    //   conn.Open();
                    string command = "";
                    command = "INSERT INTO INV_STK_TRX_DTL(DOC_TYPE,DOC_NO,ITEM_CODE,ITEM_DESC_ENG,UOM,PRICE,QUANTITY,BRANCH";
                    //NOT VALIDATED//
                    command += ",PRICE_BATCH";
                    //NOT VALIDATED//
                    if (HasBatch)
                    {
                        command += ",BATCH,EXPIRY_DATE";
                    }
                    if (HasTax)
                    {
                        command += ",TAX_PER,TAX_AMOUNT";
                    }
                    command += ",UOM_QTY, cost_price";
                    command += ") ";
                    string query = "";
                    foreach (DataGridViewRow row in dgItems.Rows)
                    {

                        DataGridViewCellCollection c = row.Cells;
                        string item_id = Convert.ToString(c["cCode"].Value);
                        double qty = Convert.ToDouble(c["cQty"].Value);
                        double uom_qty = Convert.ToDouble(c["uomQty"].Value);
                        double total_qty = qty * uom_qty;
                        double tl_tax = Convert.ToDouble(c["ctaxamount"].Value);
                        double tax = tl_tax / total_qty;
                        double c_price = Convert.ToDouble(c["cTotal"].Value) / total_qty;
                        c_price += tax;
                        double MRP = Convert.ToDouble(c["MRP"].Value) / uom_qty;
                        string unit_code = c["cUnit"].Value.ToString();
                        //for price batch
                        DataTable dt_rates = new DataTable();
                        dt_rates.Columns.Add("Rate_type", typeof(string));
                        dt_rates.Columns.Add("rate", typeof(double));
                        for (int j = 14; j < dgItems.Columns.Count; j++)
                        {
                            DataRow dRow = dt_rates.NewRow();
                            dRow[0] = dgItems.Columns[j].HeaderText;
                            if (dgItems.Columns[j].HeaderText == "PUR")
                            {
                                dRow[1] = c_price;
                            }
                            else
                            {
                                dRow[1] = Convert.ToDouble(dgItems.Rows[row.Index].Cells[j].Value) / uom_qty;
                            }

                            dt_rates.Rows.Add(dRow);
                        }
                        DateTime Exdat = new DateTime();
                        if (c["cExp"].Value != null)
                        {
                            try
                            {
                                Exdat = DateTime.ParseExact(c["cExp"].Value.ToString(), "dd/MM/yyyy", null);
                            }
                            catch
                            {
                                Exdat = Convert.ToDateTime(c["cExp"].Value);
                            }
                        }
                        string flag = "";
                        string price_batch = "";
                        if (c["cBatch"].Value == null)
                            flag = "false";
                        else
                            flag = "true";
                        if (c["cBatch"].Value != null)
                            price_batch = c["cBatch"].Value.ToString();
                        //for price batch

                        string PRICE_BATCH = se.addStock_with_batch(item_id, total_qty.ToString(), c_price.ToString(), "", MRP, dt_rates, unit_code, price_batch, Exdat, flag, hasPriceBatch);
                        dgItems.Rows[row.Index].Cells["colBATCH"].Value = PRICE_BATCH;

                        query += "SELECT 'INV.STK.OPN','" + DOC_NO.Text + "','" + Convert.ToString(c["cCode"].Value) + "','" + Convert.ToString(c["cName"].Value) + "','" + Convert.ToString(c["cUnit"].Value) + "','" + Convert.ToString(c["cPrice"].Value) + "','" + Convert.ToString(c["cQty"].Value) + "','" + lg.Branch + "','" + Convert.ToString(c["colBATCH"].Value) + "'";
                        if (HasBatch)
                        {
                            //string Exdate = Convert.ToDateTime( c["cExp"].Value).ToShortDateString();
                            query += ",'" + Convert.ToString(c["cBatch"].Value) + "','" + DateTime.ParseExact(c["cExp"].Value.ToString(), "dd/MM/yyyy", null).ToString("yyyy/MM/dd") + "'";
                        }
                        if (HasTax)
                        {
                            query += ",'" + c["ctax"].Value + "','" + c["ctaxamount"].Value + "'";
                        }
                        query += ",'" + c["uomQty"].Value + "', '" + c["cost_price"].Value + "'";
                        query += " UNION ALL ";

                    }
                    query = query.Substring(0, query.Length - 10);
                    command += query;
                    // cmd.ExecuteNonQuery();
                    //  conn.Close();
                    DbFunctions.InsertUpdate(command);
                    MessageBox.Show("Stock Entry Added!");
                }
                else
                {
                    //SqlCommand reduceStockCommand = new SqlCommand();
                    //reduceStockCommand.Connection = conn;
                    //conn.Open();
                    //reduceStockCommand.CommandText = "SELECT ITEM_CODE, QUANTITY, UOM_QTY, cost_price,PRICE_BATCH FROM INV_STK_TRX_DTL WHERE DOC_NO = '" + DOC_NO.Text + "'";


                    stkdb.DocNo = DOC_NO.Text;
                    DataTable dt = stkdb.SelectDataForReduceStk();
                    //while (r.Read())
                    //{
                    //    double qty = -1 * (Convert.ToDouble(r["QUANTITY"]) * Convert.ToDouble(r["UOM_QTY"]));
                    //    se.addStockWithBatch(Convert.ToString(r["ITEM_CODE"]), Convert.ToString(qty), Convert.ToString(r["cost_price"]), Convert.ToString(r["PRICE_BATCH"]));
                    //}

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        double qty = -1 * (Convert.ToDouble(dt.Rows[i]["QUANTITY"]) * Convert.ToDouble(dt.Rows[i]["UOM_QTY"]));
                        se.addStockWithBatch(Convert.ToString(dt.Rows[i]["ITEM_CODE"]), Convert.ToString(qty), Convert.ToString(dt.Rows[i]["cost_price"]), Convert.ToString(dt.Rows[i]["PRICE_BATCH"]));

                    }



                    //DbFunctions.CloseConnection();
                    //conn.Close();

                    //cmd.Connection = conn;
                    //if (conn.State == ConnectionState.Open)
                    //{
                    //    conn.Close();
                    //}
                    //conn.Open();
                    //cmd.CommandText = "UPDATE INV_STK_TRX_HDR SET DOC_NO = '" + DOC_NO.Text + "',DOC_DATE_GRE = '" + DOC_DATE_GRE.Value.ToString("MM/dd/yyyy") + "',DOC_DATE_HIJ = '" + DOC_DATE_HIJ.Text + "',DOC_REFERENCE = '" + DOC_REFERENCE.Text + "',NOTES = '" + NOTES.Text + "',TAX_AMOUNT = '"+TOTAL_TAX.Text+"',TOTAL_AMOUNT = '" + TOTAL_AMOUNT.Text + "' WHERE DOC_NO = '" + ID + "';DELETE FROM INV_STK_TRX_DTL WHERE DOC_NO = '" + ID + "'";

                    //cmd.ExecuteNonQuery();
                    //if (conn.State == ConnectionState.Open)
                    //{
                    //    conn.Close();
                    //}
                    //conn.Open();
                    stkHdr.Id = ID;
                    stkHdr.updateFromOpeningEntry();
                    string command = "";
                    command = "INSERT INTO INV_STK_TRX_DTL(DOC_TYPE,DOC_NO,ITEM_CODE,ITEM_DESC_ENG,UOM,PRICE,QUANTITY";

                    //NOT VALIDATED//
                    command += ",PRICE_BATCH";
                    //NOT VALIDATED//
                    if (HasBatch)
                    {
                        command += ",BATCH,EXPIRY_DATE";
                    }
                    if (HasTax)
                    {
                        command += ",TAX_PER,TAX_AMOUNT";
                    }
                    command += ",UOM_QTY, cost_price";
                    command += ") ";
                    string query = "";
                    foreach (DataGridViewRow row in dgItems.Rows)
                    {
                        DataTable dt_rates = new DataTable();
                        dt_rates.Columns.Add("Rate_type", typeof(string));
                        dt_rates.Columns.Add("rate", typeof(double));
                        for (int j = 14; j < dgItems.Columns.Count; j++)
                        {
                            DataRow dRow = dt_rates.NewRow();
                            dRow[0] = dgItems.Columns[j].HeaderText;
                            dRow[1] = Convert.ToDouble(dgItems.Rows[row.Index].Cells[j].Value);
                            dt_rates.Rows.Add(dRow);
                        }
                        DataGridViewCellCollection c = row.Cells;
                        string item_id = Convert.ToString(c["cCode"].Value);
                        double qty = Convert.ToDouble(c["cQty"].Value);
                        double uom_qty = Convert.ToDouble(c["uomQty"].Value);
                        double total_qty = qty * uom_qty;
                        double tl_tax = Convert.ToDouble(c["ctaxamount"].Value);
                        double tax = tl_tax / total_qty;
                        double c_price = Convert.ToDouble(c["cTotal"].Value) / total_qty;
                        c_price += tax;
                        double MRP = Convert.ToDouble(c["MRP"].Value);
                        string unit_code = c["cUnit"].Value.ToString();
                        DateTime Exdat = new DateTime();
                        if (c["cExp"].Value != null)
                        {
                            try
                            {

                                Exdat = DateTime.ParseExact(c["cExp"].Value.ToString(), "dd/MM/yyyy", null);
                            }
                            catch
                            {
                                Exdat = Convert.ToDateTime(c["cExp"].Value);
                            }
                        }
                        string flag = "";
                        string price_batch = "";
                        if (c["cBatch"].Value == null)
                            flag = "false";
                        else
                            flag = "true";
                        if (c["cBatch"].Value != null)
                            price_batch = c["cBatch"].Value.ToString();
                        //for price batch
                        string PRICE_BATCH = se.addStock_with_batch(item_id, total_qty.ToString(), c_price.ToString(), "", MRP, dt_rates, unit_code, price_batch, Exdat, flag, hasPriceBatch);
                        dgItems.Rows[row.Index].Cells["colBATCH"].Value = PRICE_BATCH;
                        query += "SELECT 'INV.STK.OPN','" + DOC_NO.Text + "','" + Convert.ToString(c["cCode"].Value) + "','" + Convert.ToString(c["cName"].Value) + "','" + Convert.ToString(c["cUnit"].Value) + "','" + Convert.ToString(c["cPrice"].Value) + "','" + Convert.ToString(c["cQty"].Value) + "','" + Convert.ToString(c["colBATCH"].Value) + "'";
                        if (HasBatch)
                        {
                            //    string Exdate = Convert.ToDateTime( c["cExp"].Value).ToShortDateString();
                            query += ",'" + Convert.ToString(c["cBatch"].Value) + "','" + DateTime.ParseExact(c["cExp"].Value.ToString(), "dd/MM/yyyy", null).ToString("yyyy/MM/dd") + "'";
                        }

                        if (HasTax)
                        {
                            query += ",'" + c["ctax"].Value + "','" + c["ctaxamount"].Value + "'";
                        }
                        query += ",'" + c["uomQty"].Value + "', '" + c["cost_price"].Value + "'";
                        query += " UNION ALL ";

                    }
                    query = query.Substring(0, query.Length - 10);
                    command += query;
                    //cmd.ExecuteNonQuery();
                    //conn.Close();
                    DbFunctions.InsertUpdate(command);
                    MessageBox.Show("Stock Entry Updated!");
                }

                if (!HasBatch)
                {
                    //string values = "";
                    //foreach (DataGridViewRow ro in dgItems.Rows)
                    //{
                    //    values += "'" + ro.Cells[0].Value + "',";
                    //}
                    //values = values.Substring(0, values.Length - 1);
                    //string query = "DELETE FROM INV_ITEM_PRICE WHERE ITEM_CODE IN (" + values + ");INSERT INTO INV_ITEM_PRICE(ITEM_CODE,SAL_TYPE,PRICE,UNIT_CODE,BRANCH)";
                    //for (int i = 0; i < ratesTable.Rows.Count; i++)
                    //{
                    //    for (int j = 1; j < dgRates.Columns.Count; j++)
                    //    {
                    //        string price="0";
                    //        try
                    //        {
                    //         //   price = dgRates.Rows[i].Cells[j].Value.ToString();
                    //            if (Convert.ToString(ratesTable.Rows[i][j + 2]) == "")
                    //            {
                    //                price = "0";
                    //            }
                    //            else
                    //            {
                    //                price = Convert.ToString(ratesTable.Rows[i][j + 2]);
                    //            }
                    //        }
                    //        catch
                    //        {
                    //            price = "0";
                    //        }
                    //        query += "SELECT '" + ratesTable.Rows[i]["ITEM_CODE"] + "','" + dgRates.Columns[j].Name + "','" + price + "','" + ratesTable.Rows[i]["UNIT_CODE"] + "','001' UNION ALL ";
                    //    }
                    //}
                    //query = query.Substring(0, query.Length - 10);
                    //cmd.Connection = conn;
                    //cmd.CommandText = query;
                    //conn.Open();
                    //cmd.ExecuteNonQuery();
                    //conn.Close();
                }

                btnClear.PerformClick();
            }
            else
                MessageBox.Show("No entries");
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ID = "";
            DOC_NO.Text = "";
            DOC_DATE_GRE.Value = DateTime.Today;
            DOC_DATE_HIJ.Text = "";
            DOC_REFERENCE.Text = "";
            dgItems.Rows.Clear();
            NOTES.Text = "";
            if (HasTax)
            {
                TOTAL_TAX.Text = "0.00";
            }
            TOTAL_AMOUNT.Text = "0.00";
            itemClear();
            ratesTable.Rows.Clear();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //OpenStockEntryHelp h = new OpenStockEntryHelp(1, "INV.STK.OPN");
            //h.ShowDialog();
            if (dgItems.Rows.Count > 0 && dgItems.CurrentRow != null && MessageBox.Show("Are you sure?", "Stock Deletion", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    //SqlCommand reduceStockCommand = new SqlCommand();
                    //reduceStockCommand.Connection = conn;
                    //conn.Open();
                    //reduceStockCommand.CommandText = "SELECT ITEM_CODE, QUANTITY, UOM_QTY, cost_price FROM INV_STK_TRX_DTL WHERE DOC_NO = '" + DOC_NO.Text + "'";
                    //SqlDataReader r = reduceStockCommand.ExecuteReader();
                    //StockEntry se = new StockEntry();
                    //while (r.Read())
                    //{
                    //    double qty = -1 * (Convert.ToDouble(r["QUANTITY"]) * Convert.ToDouble(r["UOM_QTY"]));
                    //    //se.addStock(Convert.ToString(r["ITEM_CODE"]), Convert.ToString(qty), Convert.ToString(r["cost_price"]), "");
                    //}
                    //conn.Close();


                    //conn.Open();
                    //cmd.CommandText = "DELETE FROM INV_STK_TRX_HDR WHERE DOC_NO = '" + dgItems.CurrentRow.Cells["cDoc_No"].Value + "';DELETE FROM INV_STK_TRX_DTL WHERE DOC_NO = '" + dgItems.CurrentRow.Cells["cDoc_No"].Value + "'";
                    //cmd.Connection = conn;
                    //cmd.ExecuteNonQuery();
                    //MessageBox.Show("Item Deleted!");
                    stkHdr.DocNo = dgItems.CurrentRow.Cells["cDoc_No"].Value.ToString() ;
                    stkHdr.deleteFromOpeningEntry();
                    dgItems.Rows.Remove(dgItems.CurrentRow);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
                conn.Close();
            }
           
        }

        private void btnQuit_Click(object sender, EventArgs e)
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

        private void dgItems_Click(object sender, EventArgs e)
        {
            if (dgItems.Rows.Count > 0 && dgItems.CurrentRow != null)
            {
                selectedRow = dgItems.CurrentRow.Index;
                DataGridViewCellCollection c = dgItems.CurrentRow.Cells;
                ITEM_CODE.Text = "";
                ITEM_CODE.Text = Convert.ToString(c["cCode"].Value);
                if (HasBatch)
                {
                    BATCH.Text = Convert.ToString(c["cBatch"].Value);
                    if (c["cBatch"].Value != null && c["cBatch"].Value.ToString() != "")
                        // EXPIRY_DATE.Value = Convert.ToDateTime(c["cExp"].Value);
                        EXPIRY_DATE.Value = DateTime.ParseExact(c["cExp"].Value.ToString(), "dd/MM/yyyy", CultureInfo.CurrentCulture);
                }
                   
                UOM.Text = Convert.ToString(c["cUnit"].Value);
                QUANTITY.Text = Convert.ToString(c["cQty"].Value);
                PRICE.Text = Convert.ToString(c["cPrice"].Value);
                if (HasTax)
                {
                    TAX_PER.Text = Convert.ToString(c["cTax"].Value);
                    TAX_AMT.Text = Convert.ToString(c["ctaxamount"].Value);
                }
                ITEM_TOTAL.Text = Convert.ToString(c["ctotal"].Value);

                dgRates.Rows.Clear();
                int i = dgRates.Rows.Add(new DataGridViewRow());
                for (int K = 13; K < dgItems.ColumnCount; K++)
                {
                    for (int j = 1; j < dgRates.Columns.Count; j++)
                    {
                        if (dgItems.Columns[K].HeaderText.ToString() == dgRates.Columns[j].HeaderText)
                        {
                            double rate = 0;
                            if (dgItems.CurrentRow.Cells[K].Value == null || dgItems.CurrentRow.Cells[K].Value == "")
                            {
                                rate = 0;
                            }
                            else
                            {
                                rate = Convert.ToDouble(dgItems.CurrentRow.Cells[K].Value);
                            }
                          //  c[dgItems.Columns[j].Name.ToString()].Value = rate.ToString();
                            dgRates.Rows[i].Cells[j].Value = rate.ToString();
                        }
                    }
                }
              dgRates.Rows[0].Cells[0].Value = Convert.ToString(c["cUnit"].Value);
                        
                //if (ratesTable.Select("INDEX = '" + selectedRow + "'").Count() > 0)
                //{
                //    foreach (DataRow row in ratesTable.Select("INDEX = '" + selectedRow + "'"))
                //    {
                //        int i = dgRates.Rows.Add(new DataGridViewRow());
                //        for (int j = 0; j < dgRates.Columns.Count; j++)
                //        {
                //            dgRates.Rows[i].Cells[j].Value = row[j + 2];
                //        }
                //    }
                //}
                //else
                //{
                //    /*if (!HasBatch)
                //    {*/
                //        #region not has batch
                //        conn.Open();
                //        cmd.CommandText = "SELECT UNIT_CODE FROM INV_ITEM_DIRECTORY_UNITS WHERE ITEM_CODE = '"+ITEM_CODE.Text+"';SELECT SAL_TYPE,PRICE,UNIT_CODE FROM INV_ITEM_PRICE WHERE ITEM_CODE = '"+ITEM_CODE.Text+"'";
                //        SqlDataReader r = cmd.ExecuteReader();
                //        while (r.Read())
                //        {
                //            ratesTable.Rows.Add(selectedRow, ITEM_CODE.Text, r["UNIT_CODE"]);
                //        }
                //        r.NextResult();
                //        while (r.Read())
                //        {
                //            for (int i = 0; i < ratesTable.Rows.Count; i++)
                //            {
                //                if (Convert.ToString(ratesTable.Rows[i][2]) == Convert.ToString(r["UNIT_CODE"]))
                //                {
                //                    for (int j = 3; j < ratesTable.Columns.Count; j++)
                //                    {
                //                        if (ratesTable.Columns[j].ColumnName == r["SAL_TYPE"].ToString())
                //                        {
                //                            ratesTable.Rows[i][j] = r["PRICE"];
                //                        }
                //                    }
                //                }

                //            }

                //        }
                //        conn.Close();

                //        foreach (DataRow row in ratesTable.Select("INDEX = '" + selectedRow + "'"))
                //        {
                //            int i = dgRates.Rows.Add(new DataGridViewRow());
                //            for (int j = 0; j < dgRates.Columns.Count; j++)
                //            {
                //                dgRates.Rows[i].Cells[j].Value = row[j + 2];
                //            }
                //        }
                //        #endregion
                //    //}
                //}
            }
        }
        CultureInfo provider = CultureInfo.InvariantCulture;
        private void btnCode_Click(object sender, EventArgs e)
        {
            OpenStockEntryHelp h = new OpenStockEntryHelp(0, "INV.STK.OPN");
            if (h.ShowDialog() == DialogResult.OK && h.c != null)
            {
                btnClear.PerformClick();
                ratesTable.Rows.Clear();
                ID = Convert.ToString(h.c["DOC_NO"].Value);
                DOC_NO.Text = ID;
             //   DOC_DATE_GRE.Value = Convert.ToDateTime(h.c["DOC_DATE_GRE"].Value);
                try
                {
                    DOC_DATE_GRE.Text = DateTime.ParseExact(h.c["DOC_DATE_GRE"].Value.ToString(), "dd/MM/yyyy",provider).ToString("yyyy/MM/dd");
                }
                catch
                {
                }
                DOC_DATE_HIJ.Text = Convert.ToString(h.c["DOC_DATE_HIJ"].Value);
                DOC_REFERENCE.Text = Convert.ToString(h.c["DOC_REFERENCE"].Value);
                NOTES.Text = Convert.ToString(h.c["NOTES"].Value);
                if (HasTax)
                {
                    TOTAL_TAX.Text = Convert.ToString(h.c["TAX_AMOUNT"].Value);
                }
                TOTAL_AMOUNT.Text = Convert.ToString(h.c["TOTAL_AMOUNT"].Value);

                try
                {
                    dgItems.Rows.Clear();
               //     conn.Open();
               //     SqlCommand cmd1 = new SqlCommand();

               ////     cmd.CommandText = "SELECT ITEM_CODE,UOM,PRICE,QUANTITY,ITEM_TOTAL,BATCH,CONVERT(NVARCHAR,EXPIRY_DATE,103) AS EXPIRY_DATE,TAX_PER,TAX_AMOUNT,DOC_NO, UOM_QTY, cost_price FROM INV_STK_TRX_DTL WHERE DOC_NO = '" + ID + "'";
               //     cmd1.CommandText = "PREV_OPEN_STOCK_INV";
               //     cmd1.CommandType = CommandType.StoredProcedure;
               //     cmd1.Connection = conn;
               //     // cmd.Parameters.AddWithValue("@ID",ID);
               //     cmd1.Parameters.Clear();
               //     cmd1.Parameters.AddWithValue("@ID", SqlDbType.VarChar).Value = ID;
               //     SqlDataAdapter ad = new SqlDataAdapter(cmd1);
               //     DataTable dt = new DataTable();
               //     ad.Fill(dt);
                    stkHdr.Id = ID;
                    SqlDataReader r = stkHdr.selectPreData();
                    int count = r.FieldCount;
                  
                    while (r.Read())
                    {
                        int i = dgItems.Rows.Add(new DataGridViewRow());
                        DataGridViewCellCollection c = dgItems.Rows[i].Cells;
                        c["cCode"].Value = r["ITEM_CODE"];
                        c["cName"].Value = r["ITEM_DESC_ENG"];
                        if (HasBatch)
                        {
                            c["cBatch"].Value = r["BATCH"];
                            if (r["BATCH"].ToString()!="")
                            {
                                c["cExp"].Value = Convert.ToDateTime(r["EXPIRY_DATE"]).ToString("dd/MM/yyyy");
                            }
                           
                        }
                        if (HasTax)
                        {
                            c["ctax"].Value = r["TAX_PER"];
                            c["ctaxamount"].Value = r["TAX_AMOUNT"];
                        }
                        c["cUnit"].Value = r["UOM"];
                        c["cQty"].Value = r["QUANTITY"];
                        c["cPrice"].Value = r["PRICE"];
                        c["ctotal"].Value = r["ITEM_TOTAL"];
                        c["cDoc_No"].Value = r["DOC_NO"];
                        c["uomQty"].Value = r["UOM_QTY"];
                        c["cost_price"].Value = r["cost_price"];
                        if (r["PRICE_BATCH"].ToString() == "")
                        {
                            c["colBATCH"].Value = null;
                        }
                        else
                        {
                            c["colBATCH"].Value = r["PRICE_BATCH"];
                        }
                        for (int j = 21; j < count; j++)
                        {
                            for (int k = 14; k < dgItems.Columns.Count; k++)
                            {
                                if (r.GetName(j).ToString() == dgItems.Columns[k].HeaderText)
                                {
                                   // c[dgItems.Columns[k].HeaderText].Value = r[j].ToString();
                                    c[dgItems.Columns[k].HeaderText].Value = (Convert.ToDouble(r[j]) * Convert.ToDouble(r["UOM_QTY"])).ToString();
                                }
                            }
                        }
                    }
                    DbFunctions.CloseConnection();
                    //cmd1.CommandType = CommandType.Text;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    conn.Close();
                }

                itemClear();
            }
        }

        private void btnItemAdd_Click(object sender, EventArgs e)
        {
            if (ItemValid())
            {
                addItem();
            }
        }
        
        private void btnItemClear_Click(object sender, EventArgs e)
        {
            itemClear();
        }
        #endregion

        #region TextChanged Events
        private void QUANTITY_TextChanged(object sender, EventArgs e)
        {
            if (QUANTITY.Text.Trim() != "" && QUANTITY.Text.Trim() != "." && PRICE.Text.Trim() != "" && PRICE.Text.Trim() != ".")
            {
                ITEM_TOTAL.Text = (Convert.ToDouble(PRICE.Text) * Convert.ToDouble(QUANTITY.Text)).ToString();
            }
            else
            {
                ITEM_TOTAL.Text = "0";
            }

            if (TAX_PER.Text != "")
            {
                TAX_AMT.Text = (Convert.ToDouble(ITEM_TOTAL.Text) * (Convert.ToDouble(TAX_PER.Text) / 100)).ToString();
            }
        }

        private void PRICE_TextChanged(object sender, EventArgs e)
        {
            if (PRICE.Text.Trim() != "" && PRICE.Text.Trim() != ".")
            {
                double qty = 0;
                if (QUANTITY.Text.Trim() != "" && QUANTITY.Text.Trim() != ".")
                {
                    qty = Convert.ToDouble(QUANTITY.Text);
                }
                ITEM_TOTAL.Text = (Convert.ToDouble(PRICE.Text) * qty).ToString();
            }
            else
            {
                ITEM_TOTAL.Text = "0";
            }

            if (TAX_PER.Text != "")
            {
                TAX_AMT.Text = (Convert.ToDouble(ITEM_TOTAL.Text) * (Convert.ToDouble(TAX_PER.Text) / 100)).ToString();
            }
            if (dgRates.Rows.Count > 0)
            {
                if (PRICE.Text != "")
                {
                    //    dgRates.Rows[0].Cells["RTL"].Value = SALE_PRICE.Text;
                    //    dgRates.Rows[0].Cells["WHL"].Value = txt_wholesale.Text;
                    //    dgRates.Rows[0].Cells["MRP"].Value = txt_MRP.Text;
                    dgRates.Rows[0].Cells["PUR"].Value = PRICE.Text;
                }
            }
        }

        private void TAX_PER_TextChanged(object sender, EventArgs e)
        {
            if (TAX_PER.Text.Trim() != "" && ITEM_TOTAL.Text.Trim() != "")
            {
                TAX_AMT.Text = ((Convert.ToDouble(TAX_PER.Text) / 100) * Convert.ToDouble(ITEM_TOTAL.Text)).ToString();
            }
        }

        private void ITEM_CODE_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (ITEM_CODE.Text.Length >= 3)
                {
                    stkDtl.ItemCode = ITEM_CODE.Text;
                    txtItemName.Text = General.getName(ITEM_CODE.Text, "INV_ITEM_DIRECTORY");
                    //conn.Open();
                    //cmd.CommandText = "SELECT UNIT_CODE,PACK_SIZE FROM INV_ITEM_DIRECTORY_UNITS WHERE ITEM_CODE = '" + ITEM_CODE.Text + "'";
                    //table.Rows.Clear();
                    //dgRates.Rows.Clear();
                    //adapter.Fill(table);
                    UOM.SelectedIndexChanged -= UOM_SelectedIndexChanged;
                    UOM.DataSource = stkDtl.selectUnits();
                    
                    UOM.DisplayMember = "UNIT_CODE";
                    UOM.ValueMember = "PACK_SIZE";
                    UOM.SelectedIndexChanged += UOM_SelectedIndexChanged;
                    SqlDataReader r =stkDtl.selectunits();
                    while (r.Read())
                    {
                        dgRates.Rows.Add(r["UNIT_CODE"]);
                    }
                  //  conn.Close();
                    DbFunctions.CloseConnection(); 


                  //  conn.Open();
                   // cmd.CommandText = "SELECT UNIT_CODE,SAL_TYPE,PRICE FROM INV_ITEM_PRICE_DF WHERE ITEM_CODE = '" + ITEM_CODE.Text + "' AND UNIT_CODE='"+UOM.Text+"' ";
                    stkDtl.Uom = UOM.Text;
                    SqlDataReader r1 = stkDtl.selectItemPrices();
                    while (r1.Read())
                    {
                        for (int i = 0; i < dgRates.Rows.Count ; i++)
                        {
                            DataGridViewCellCollection c = dgRates.Rows[i].Cells;
                            if (Convert.ToString(r1["UNIT_CODE"]).Equals(Convert.ToString(c[0].Value)))
                            {
                                c[Convert.ToString(r1["SAL_TYPE"])].Value = r1["PRICE"];
                            }
                        }
                    }
                    DbFunctions.CloseConnection(); 
                    //conn.Close();
                   
                }
            }
            catch
            {
                //conn.Close();
            }



        }
        #endregion

        private void DOC_NO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                btnCode.PerformClick();
            }
        }

        private void linkRemoveRecord_LinkClicked(object sender, EventArgs e)
        {
            if (dgItems.Rows.Count > 0 && dgItems.CurrentRow != null)
            {
                dgItems.Rows.Remove(dgItems.CurrentRow);
                btnItemClear.PerformClick();
            }
        }

        private void UOM_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgRates.Rows.Clear();

            if (UOM.SelectedIndex > -1)
            {
                dgRates.Rows.Clear();
                dgRates.Rows.Add(UOM.Text);
                stkDtl.Uom = UOM.Text;
                stkDtl.ItemCode = ITEM_CODE.Text;
                //if (conn.State == ConnectionState.Open)
                //{
                //    conn.Close();
                //}
                //conn.Open();
                //cmd.CommandText = "SELECT UNIT_CODE,SAL_TYPE,PRICE FROM INV_ITEM_PRICE_DF WHERE ITEM_CODE = '" + ITEM_CODE.Text + "' AND UNIT_CODE='" + UOM.Text + "' ";
                SqlDataReader r1 = stkDtl.selectItemPrices();
                while (r1.Read())
                {
                    for (int i = 0; i < dgRates.Rows.Count; i++)
                    {
                        DataGridViewCellCollection c = dgRates.Rows[i].Cells;
                        if (Convert.ToString(r1["UNIT_CODE"]).Equals(Convert.ToString(c[0].Value)))
                        {
                            c[Convert.ToString(r1["SAL_TYPE"])].Value = r1["PRICE"];
                        }
                    }
                }
               // conn.Close();
                DbFunctions.CloseConnection();
            }
        }
    }
}
