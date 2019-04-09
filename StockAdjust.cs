using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sys_Sols_Inventory.Model;

namespace Sys_Sols_Inventory
{
    public partial class StockAdjust : Form
    {
        #region properties declaration
        //private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        //private SqlCommand cmd = new SqlCommand();
        //private SqlDataAdapter adapter = new SqlDataAdapter();
        private DataTable table = new DataTable();
        private string ID = "";
        private int selectedRow = -1;
        private bool HasTax = true;
        private bool HasBatch = true;
        private bool onlyFloat = false;
        public bool HasArabic = true;
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
            if (ITEM_CODE.Text.Trim() != "" && batch && UOM.Text.Trim() != "" && QUANTITY.Text.Trim() != "" && PRICE.Text.Trim() != "")
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
                        DataRow values = ratesTable.Select("INDEX = '" + selectedRow + "' AND UNIT_CODE = '" + row.Cells[0].Value + "'").First();
                        values[0] = selectedRow;
                        values[1] = ITEM_CODE.Text;
                        for (int j = 0; j < dgRates.Columns.Count; j++)
                        {
                            values[j + 2] = row.Cells[j].Value;
                        }
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

        //Login lg = (Login)Application.OpenForms["Login"];
        public string SalesManCode, SalesManName;
        #region construct,load,keydown,edicontrolshowing
        public StockAdjust()
        {
            InitializeComponent();
            //cmd.Connection = conn;
            //adapter.SelectCommand = cmd;
        }

        private void OpenStockEntry_Load(object sender, EventArgs e)
        {
            HasTax = General.IsEnabled(Settings.Tax);
            HasBatch = General.IsEnabled(Settings.Batch);
            HasArabic = General.IsEnabled(Settings.Arabic);

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

            ratesTable.Columns.Add("INDEX");
            ratesTable.Columns.Add("ITEM_CODE");
            ratesTable.Columns.Add("UNIT_CODE");
            string query = "SELECT CODE,DESC_ENG FROM GEN_PRICE_TYPE";
           // cmd.CommandType = CommandType.Text;
            SqlDataReader r =DbFunctions.GetDataReader(query);
            while (r.Read())
            {
                ratesTable.Columns.Add(r[0].ToString());
                dgRates.Columns.Add(Convert.ToString(r[0]), Convert.ToString(r[1]));
            }

            QUANTITY.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            PRICE.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            if (HasTax)
            {
                TAX_PER.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            }
            DbFunctions.CloseConnection();
        }
        public void GetSalesMan()
        {
            // Login log = (Login)Application.OpenForms["Login"];
            try
            {
                SalesManCode = lg.EmpId;
                //conn.Open();
                //cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandText = "GetSalesMan";
                //cmd.Parameters.Clear();
                //cmd.Parameters.AddWithValue("@Empid", lg.EmpId);
                //SalesManName = Convert.ToString(cmd.ExecuteScalar());
                //conn.Close();
                string cmd = "GetSalesMan";
                Dictionary<string ,object> param=new Dictionary<string ,object>();
                param.Add("@Empid", lg.EmpId);
                SalesManName =Convert.ToString(DbFunctions.GetAValueProcedure(cmd,param));
            }
            catch
            { }



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
                //txtItemName.Text = Convert.ToString(h.c["DESC_ENG"].Value);
                txtItemName.Text = Convert.ToString(h.c["ITEM_NAME"].Value);
                TaxId = Convert.ToString(h.c["TaxId"].Value);
                GetTaxRate();
                getRate();
                QUANTITY.Focus();
            }
        }
        private void getRate()
        {
            try
            {
               // conn.Open();
                //cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandText = "GET_RATE";
                //cmd.Parameters.Clear();
                //cmd.Parameters.AddWithValue("@ITEM_CODE", ITEM_CODE.Text);
                //cmd.Parameters.AddWithValue("@UNIT_CODE", UOM.Text);
                //cmd.Parameters.AddWithValue("RATE_CODE", "PUR");
                //string price = Convert.ToString(cmd.ExecuteScalar());
                //PRICE.Text = price;
                //conn.Close();
                string cmd = "GET_RATE";
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@ITEM_CODE", ITEM_CODE.Text);
                param.Add("@UNIT_CODE", UOM.Text);
                param.Add("RATE_CODE", "PUR");
                string price = Convert.ToString(DbFunctions.GetAValueProcedure(cmd, param));
                PRICE.Text = price;
            }
            catch
            {
            }
        }
        public void GetTaxRate()
        {
            try
            {
                //cmd.CommandText = "SELECT TaxRate from GEN_TAX_MASTER where TaxId=" + TaxId;
                //cmd.CommandType = CommandType.Text;
                //SqlDataReader r = cmd.ExecuteReader();
                string query = "SELECT TaxRate from GEN_TAX_MASTER where TaxId=" + TaxId;
                SqlDataReader r = DbFunctions.GetDataReader(query);
                while (r.Read())
                {
                    TAX_PER.Text = r[0].ToString();
                }
            }
            catch (Exception ex)
            {
                //conn.Close();
                DbFunctions.CloseConnection();
            }
            DbFunctions.CloseConnection();

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
                string qry = "";
                if (ID == "")
                {
                    DOC_NO.Text = General.generateStockID();

                    qry = "INSERT INTO INV_STK_TRX_HDR(DOC_NO,DOC_DATE_GRE,DOC_DATE_HIJ,DOC_REFERENCE,NOTES,TAX_AMOUNT,TOTAL_AMOUNT,DOC_TYPE,AddedBy,BRANCH) VALUES('" + DOC_NO.Text + "','" + DOC_DATE_GRE.Value.ToString("MM/dd/yyyy") + "','" + DOC_DATE_HIJ.Text + "','" + DOC_REFERENCE.Text + "','" + NOTES.Text + "','" + TOTAL_TAX.Text + "','" + TOTAL_AMOUNT.Text + "','INV.STK.ADJ','" + SalesManCode + "','" + lg.Branch + "')";
                    //cmd.CommandType = CommandType.Text;
                    //conn.Open();
                    //cmd.ExecuteNonQuery();
                    DbFunctions.InsertUpdate(qry);
                    qry = "INSERT INTO INV_STK_TRX_DTL(DOC_TYPE,DOC_NO,ITEM_CODE,UOM,PRICE,QUANTITY,BRANCH";
                    if (HasBatch)
                    {
                        qry += ",BATCH,EXPIRY_DATE";
                    }
                    if (HasTax)
                    {
                        qry += ",TAX_PER,TAX_AMOUNT";
                    }
                    qry += ") ";
                    string query = "";
                    foreach (DataGridViewRow row in dgItems.Rows)
                    {
                        DataGridViewCellCollection c = row.Cells;
                        query += "SELECT 'INV.STK.ADJ','" + DOC_NO.Text + "','" + Convert.ToString(c["cCode"].Value) + "','" + Convert.ToString(c["cUnit"].Value) + "','" + Convert.ToString(c["cPrice"].Value) + "','" + Convert.ToString(c["cQty"].Value) + "','" + lg.Branch + "'";
                        if (HasBatch)
                        {
                            query += ",'" + Convert.ToString(c["cBatch"].Value) + "','" + DateTime.ParseExact(c["cExp"].Value.ToString(), "dd/MM/yyyy", null).ToString("MM/dd/yyyy") + "'";
                        }
                        if (HasTax)
                        {
                            query += ",'" + c["ctax"].Value + "','" + c["ctaxamount"].Value + "'";
                        }
                        query += " UNION ALL ";
                    }
                    query = query.Substring(0, query.Length - 10);
                    qry += query;
                    //cmd.ExecuteNonQuery();
                    //conn.Close();
                    DbFunctions.InsertUpdate(qry);
                    MessageBox.Show("Stock Adjustment Added!");
                }
                else
                {
                    //conn.Open();
                    qry = "UPDATE INV_STK_TRX_HDR SET DOC_NO = '" + DOC_NO.Text + "',DOC_DATE_GRE = '" + DOC_DATE_GRE.Value.ToString("MM/dd/yyyy") + "',DOC_DATE_HIJ = '" + DOC_DATE_HIJ.Text + "',DOC_REFERENCE = '" + DOC_REFERENCE.Text + "',NOTES = '" + NOTES.Text + "',TAX_AMOUNT = '" + TOTAL_TAX.Text + "',TOTAL_AMOUNT = '" + TOTAL_AMOUNT.Text + "',AddedBy='" + SalesManCode + "' WHERE DOC_NO = '" + ID + "';DELETE FROM INV_STK_TRX_DTL WHERE DOC_NO = '" + ID + "'";
                    //cmd.CommandType = CommandType.Text;
                    //cmd.ExecuteNonQuery();
                    DbFunctions.InsertUpdate(qry);
                    qry = "INSERT INTO INV_STK_TRX_DTL(DOC_NO,ITEM_CODE,UOM,PRICE,QUANTITY";
                    if (HasBatch)
                    {
                        qry += ",BATCH,EXPIRY_DATE";
                    }
                    if (HasTax)
                    {
                        qry += ",TAX_PER,TAX_AMOUNT";
                    }
                    qry += ") ";
                    string query = "";
                    foreach (DataGridViewRow row in dgItems.Rows)
                    {
                        DataGridViewCellCollection c = row.Cells;
                        query += "SELECT '" + DOC_NO.Text + "','" + Convert.ToString(c["cCode"].Value) + "','" + Convert.ToString(c["cUnit"].Value) + "','" + Convert.ToString(c["cPrice"].Value) + "','" + Convert.ToString(c["cQty"].Value) + "'";
                        if (HasBatch)
                        {
                            query += ",'" + Convert.ToString(c["cBatch"].Value) + "','" + DateTime.ParseExact(c["cExp"].Value.ToString(), "dd/MM/yyyy", null).ToString("MM/dd/yyyy") + "'";
                        }
                        if (HasTax)
                        {
                            query += ",'" + c["ctax"].Value + "','" + c["ctaxamount"].Value + "'";
                        }
                        query += " UNION ALL ";
                    }
                    query = query.Substring(0, query.Length - 10);
                    qry += query;
                    //cmd.CommandType = CommandType.Text;
                    //cmd.ExecuteNonQuery();
                    //conn.Close();
                    DbFunctions.InsertUpdate(qry);
                    MessageBox.Show("Stock Adjustment Updated!");
                }

                if (!HasBatch)
                {
                    string values = "";
                    foreach (DataGridViewRow ro in dgItems.Rows)
                    {
                        values += "'" + ro.Cells[0].Value + "',";
                    }
                    values = values.Substring(0, values.Length - 1);
                    string query = "DELETE FROM INV_ITEM_PRICE WHERE ITEM_CODE IN (" + values + ");INSERT INTO INV_ITEM_PRICE(ITEM_CODE,SAL_TYPE,PRICE,UNIT_CODE,BRANCH)";
                    for (int i = 0; i < ratesTable.Rows.Count; i++)
                    {
                        for (int j = 1; j < dgRates.Columns.Count; j++)
                        {
                            string price = "0";
                            try
                            {
                                //   price = dgRates.Rows[i].Cells[j].Value.ToString();
                                if (Convert.ToString(ratesTable.Rows[i][j + 2]) == "")
                                {
                                    price = "0";
                                }
                                else
                                {
                                    price = Convert.ToString(ratesTable.Rows[i][j + 2]);
                                }
                            }
                            catch
                            {
                                price = "0";
                            }
                            query += "SELECT '" + ratesTable.Rows[i]["ITEM_CODE"] + "','" + dgRates.Columns[j].Name + "','" + price + "','" + ratesTable.Rows[i]["UNIT_CODE"] + "','001' UNION ALL ";
                        }
                    }
                    query = query.Substring(0, query.Length - 10);
                    qry = query;
                    //cmd.CommandType = CommandType.Text;
                    //conn.Open();
                    //cmd.ExecuteNonQuery();
                    //conn.Close();
                    DbFunctions.InsertUpdate(qry);
                }

                btnClear.PerformClick();
            }
            else
                MessageBox.Show("Fill entry");
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
            OpenStockEntryHelp h = new OpenStockEntryHelp(1, "INV.STK.ADJ");
            h.ShowDialog();
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
                    EXPIRY_DATE.Value = DateTime.ParseExact(c["cExp"].Value.ToString(), "dd/MM/yyyy", null);
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
                if (ratesTable.Select("INDEX = '" + selectedRow + "'").Count() > 0)
                {
                    foreach (DataRow row in ratesTable.Select("INDEX = '" + selectedRow + "'"))
                    {
                        int i = dgRates.Rows.Add(new DataGridViewRow());
                        for (int j = 0; j < dgRates.Columns.Count; j++)
                        {
                            dgRates.Rows[i].Cells[j].Value = row[j + 2];
                        }
                    }
                }
                else
                {
                    /*if (!HasBatch)
                    {*/
                    #region not has batch
                    //conn.Open();
                    string query = "";
                    query = "SELECT UNIT_CODE FROM INV_ITEM_DIRECTORY_UNITS WHERE ITEM_CODE = '" + ITEM_CODE.Text + "';SELECT SAL_TYPE,PRICE,UNIT_CODE FROM INV_ITEM_PRICE WHERE ITEM_CODE = '" + ITEM_CODE.Text + "'";
                    SqlDataReader r = DbFunctions.GetDataReader(query);
                    while (r.Read())
                    {
                        ratesTable.Rows.Add(selectedRow, ITEM_CODE.Text, r["UNIT_CODE"]);
                    }
                    r.NextResult();
                    while (r.Read())
                    {
                        for (int i = 0; i < ratesTable.Rows.Count; i++)
                        {
                            if (Convert.ToString(ratesTable.Rows[i][2]) == Convert.ToString(r["UNIT_CODE"]))
                            {
                                for (int j = 3; j < ratesTable.Columns.Count; j++)
                                {
                                    if (ratesTable.Columns[j].ColumnName == r["SAL_TYPE"].ToString())
                                    {
                                        ratesTable.Rows[i][j] = r["PRICE"];
                                    }
                                }
                            }

                        }

                    }
                    //conn.Close();
                    DbFunctions.CloseConnection();

                    foreach (DataRow row in ratesTable.Select("INDEX = '" + selectedRow + "'"))
                    {
                        int i = dgRates.Rows.Add(new DataGridViewRow());
                        for (int j = 0; j < dgRates.Columns.Count; j++)
                        {
                            dgRates.Rows[i].Cells[j].Value = row[j + 2];
                        }
                    }
                    #endregion
                    //}
                }
            }
        }

        private void btnCode_Click(object sender, EventArgs e)
        {
            OpenStockEntryHelp h = new OpenStockEntryHelp(0, "INV.STK.ADJ");
            if (h.ShowDialog() == DialogResult.OK && h.c != null)
            {
                btnClear.PerformClick();
                ratesTable.Rows.Clear();
           
                ID = Convert.ToString(h.c["DOC_NO"].Value);
                DOC_NO.Text = ID;
                DOC_DATE_GRE.Value = Convert.ToDateTime(Convert.ToString(h.c["DOC_DATE_GRE"].Value));
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

                    //if (conn.State == ConnectionState.Open)
                    //{
                    //}
                    //else
                    //{

                    //    conn.Open();
                    //}

                    //cmd.Connection = conn;
                   string query = "";
                   query = "SELECT ITEM_CODE,UOM,PRICE,QUANTITY,ITEM_TOTAL,BATCH,CONVERT(NVARCHAR,EXPIRY_DATE,103) AS EXPIRY_DATE,TAX_PER,TAX_AMOUNT FROM INV_STK_TRX_DTL WHERE DOC_NO = '" + DOC_NO.Text + "'";

                   using (SqlDataReader r = DbFunctions.GetDataReader(query))
                    {
                        while (r.Read())
                        {
                            int i = dgItems.Rows.Add(new DataGridViewRow());
                            DataGridViewCellCollection c = dgItems.Rows[i].Cells;
                            c["cCode"].Value = r["ITEM_CODE"];
                            if (HasBatch)
                            {
                                c["cBatch"].Value = r["BATCH"];
                                c["cExp"].Value = r["EXPIRY_DATE"];
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
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                DbFunctions.CloseConnection();
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
                    txtItemName.Text = General.getName(ITEM_CODE.Text, "INV_ITEM_DIRECTORY");
                    string query = "";
                    //conn.Open();
                    query = "SELECT UNIT_CODE,PACK_SIZE FROM INV_ITEM_DIRECTORY_UNITS WHERE ITEM_CODE = '" + ITEM_CODE.Text + "'";
                    //cmd.CommandType = CommandType.Text;
                    table.Rows.Clear();
                    dgRates.Rows.Clear();
                    //adapter.Fill(table);
                    table = DbFunctions.GetDataTable(query);
                    UOM.DataSource = table;
                    UOM.DisplayMember = "UNIT_CODE";
                    UOM.ValueMember = "PACK_SIZE";
                    SqlDataReader r = DbFunctions.GetDataReader(query);
                    while (r.Read())
                    {
                        dgRates.Rows.Add(r["UNIT_CODE"]);
                    }
                   // conn.Close();
                    DbFunctions.CloseConnection();

                    //conn.Open();
                    query = "SELECT UNIT_CODE,SAL_TYPE,PRICE FROM INV_ITEM_PRICE WHERE ITEM_CODE = '" + ITEM_CODE.Text + "'";
                    //cmd.CommandType = CommandType.Text;
                    SqlDataReader r1 = DbFunctions.GetDataReader(query);
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
                    //conn.Close();
                    DbFunctions.CloseConnection();
                }
            }
            catch
            {
                DbFunctions.CloseConnection();

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

        private void StockAdjust_Load(object sender, EventArgs e)
        {
            Class.CompanySetup CompStep = new Class.CompanySetup();
            DOC_DATE_GRE.Text = CompStep.GettDate();
            HasTax = General.IsEnabled(Settings.Tax);
            HasBatch = General.IsEnabled(Settings.Batch);
            HasArabic = General.IsEnabled(Settings.Arabic);

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
            string query = "";
            //conn.Open();
            query = "SELECT CODE,DESC_ENG FROM GEN_PRICE_TYPE";
            //cmd.CommandType=CommandType.Text;
            SqlDataReader r = DbFunctions.GetDataReader(query);
            while (r.Read())
            {
                ratesTable.Columns.Add(r[0].ToString());
                dgRates.Columns.Add(Convert.ToString(r[0]), Convert.ToString(r[1]));
            }
            //conn.Close();
            DbFunctions.CloseConnection();

            QUANTITY.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            PRICE.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            if (HasTax)
            {
                TAX_PER.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            }
          //  GetSalesMan();
        }

        private void UOM_SelectedIndexChanged(object sender, EventArgs e)
        {
            getRate();
        }

      

        
    }
}
