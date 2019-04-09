using SpannedDataGridView;
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
    public partial class Item_level_offer : Form
    {
        //private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        //private SqlCommand cmd = new SqlCommand();
        //private SqlDataAdapter adapter = new SqlDataAdapter();
        private DataTable table = new DataTable();
        private BindingSource source = new BindingSource();
        private DataTable rateTable = new DataTable();
        private bool hasTax = true;
        private bool hasArabic = true;
        bool ShowPurchase = false;
        string ID = "";
        int PROMOID = 0;

        public genEnum currentForm = genEnum.State;
        Initial mdi = (Initial)Application.OpenForms["Initial"];
        Login lg = (Login)Application.OpenForms["Login"];
        public int callfrom = 0;

        public Item_level_offer(genEnum form)
        {
            InitializeComponent();
            //cmd.Connection = conn;
            //adapter.SelectCommand = cmd;
            currentForm = form;
        }

        public Item_level_offer(genEnum form, int callfromto): this(form)
        {
            callfrom = callfromto;
        }
        private void Item_level_offer_Load(object sender, EventArgs e)
        {
            treeView1.ExpandAll();

           
            //cmd.CommandType = CommandType.Text;
            string cmd = "SELECT CODE AS [key],CODE+' - '+DESC_ENG AS value FROM GEN_PRICE_TYPE";
            //adapter.Fill(rateTable);
            rateTable = Model.DbFunctions.GetDataTable(cmd);
            RATE_CODE.DisplayMember = "value";
            RATE_CODE.ValueMember = "key";
            RATE_CODE.DataSource = rateTable; 
            bindgridview();

            treeView1.SelectedNode = treeView1.Nodes[0].Nodes[0];
        
            MessageBox.Show(treeView1.SelectedNode.Text.ToString());
        }


        public void bindgridview()
        {
            try
            {
                //cmd.Connection = conn;
                string query = "";
                if (ShowPurchase)
                {
                    //  cmd.CommandText = "SELECT     INV_ITEM_DIRECTORY.CODE AS [Item Code], INV_ITEM_DIRECTORY_UNITS.BARCODE AS Barcode, INV_ITEM_DIRECTORY.DESC_ENG AS [Item Name],INV_ITEM_DIRECTORY.DESC_ARB, INV_ITEM_DIRECTORY_UNITS.UNIT_CODE, INV_ITEM_PRICE.PRICE AS SALES, INV_ITEM_DIRECTORY.TaxId,INV_ITEM_PRICE_1.PRICE AS PURCHASE, GEN_TAX_MASTER.TaxRate FROM         INV_ITEM_DIRECTORY LEFT OUTER JOIN  GEN_TAX_MASTER ON INV_ITEM_DIRECTORY.TaxId = GEN_TAX_MASTER.TaxId LEFT OUTER JOIN  INV_ITEM_PRICE LEFT OUTER JOIN   INV_ITEM_DIRECTORY_UNITS ON INV_ITEM_PRICE.UNIT_CODE = INV_ITEM_DIRECTORY_UNITS.UNIT_CODE AND INV_ITEM_PRICE.ITEM_CODE = INV_ITEM_DIRECTORY_UNITS.ITEM_CODE LEFT OUTER JOIN INV_ITEM_PRICE AS INV_ITEM_PRICE_1 ON INV_ITEM_DIRECTORY_UNITS.UNIT_CODE = INV_ITEM_PRICE_1.UNIT_CODE AND INV_ITEM_DIRECTORY_UNITS.ITEM_CODE = INV_ITEM_PRICE_1.ITEM_CODE ON INV_ITEM_DIRECTORY.CODE = INV_ITEM_DIRECTORY_UNITS.ITEM_CODE WHERE     (INV_ITEM_PRICE.SAL_TYPE = '" + RATE_CODE.SelectedValue + "') AND (INV_ITEM_PRICE_1.SAL_TYPE = 'PUR')";
                    // cmd.CommandText = "SELECT        INV_ITEM_DIRECTORY.CODE AS [Item Code], INV_ITEM_DIRECTORY_UNITS.BARCODE AS Barcode, INV_ITEM_DIRECTORY.DESC_ENG AS [Item Name], INV_ITEM_DIRECTORY.DESC_ARB, INV_ITEM_DIRECTORY_UNITS.UNIT_CODE, INV_ITEM_PRICE.PRICE AS SALES, GEN_TAX_MASTER.TaxRate,  ISNULL(INV_ITEM_PRICE_1.PRICE * GEN_TAX_MASTER.TaxRate / 100 + INV_ITEM_PRICE_1.PRICE, 0) AS PURVALUEWITHTAX,INV_ITEM_DIRECTORY.TaxId FROM            INV_ITEM_DIRECTORY LEFT OUTER JOIN  GEN_TAX_MASTER ON INV_ITEM_DIRECTORY.TaxId = GEN_TAX_MASTER.TaxId LEFT OUTER JOIN  INV_ITEM_PRICE LEFT OUTER JOIN INV_ITEM_DIRECTORY_UNITS ON INV_ITEM_PRICE.UNIT_CODE = INV_ITEM_DIRECTORY_UNITS.UNIT_CODE AND  INV_ITEM_PRICE.ITEM_CODE = INV_ITEM_DIRECTORY_UNITS.ITEM_CODE LEFT OUTER JOIN INV_ITEM_PRICE AS INV_ITEM_PRICE_1 ON INV_ITEM_DIRECTORY_UNITS.UNIT_CODE = INV_ITEM_PRICE_1.UNIT_CODE AND INV_ITEM_DIRECTORY_UNITS.ITEM_CODE = INV_ITEM_PRICE_1.ITEM_CODE ON INV_ITEM_DIRECTORY.CODE = INV_ITEM_DIRECTORY_UNITS.ITEM_CODE WHERE        (INV_ITEM_PRICE.SAL_TYPE = '" + RATE_CODE.SelectedValue + "') AND (INV_ITEM_PRICE_1.SAL_TYPE = 'PUR')";

                    query = "SELECT        INV_ITEM_DIRECTORY.CODE AS 'Item Code', INV_ITEM_DIRECTORY_UNITS.BARCODE AS Barcode, INV_ITEM_DIRECTORY.DESC_ENG AS [Item Name],  INV_ITEM_DIRECTORY.DESC_ARB, INV_ITEM_DIRECTORY_UNITS.UNIT_CODE, INV_ITEM_PRICE.PRICE AS SALES, GEN_TAX_MASTER.TaxRate,  ISNULL(INV_ITEM_PRICE_1.PRICE * GEN_TAX_MASTER.TaxRate / 100 + INV_ITEM_PRICE_1.PRICE, 0) AS PURVALUEWITHTAX, INV_ITEM_DIRECTORY.TaxId,INV_ITEM_DIRECTORY.HASSERIAL FROM            INV_ITEM_DIRECTORY LEFT OUTER JOIN  GEN_TAX_MASTER ON INV_ITEM_DIRECTORY.TaxId = GEN_TAX_MASTER.TaxId LEFT OUTER JOIN INV_ITEM_PRICE LEFT OUTER JOIN  INV_ITEM_DIRECTORY_UNITS ON INV_ITEM_PRICE.UNIT_CODE = INV_ITEM_DIRECTORY_UNITS.UNIT_CODE AND  INV_ITEM_PRICE.ITEM_CODE = INV_ITEM_DIRECTORY_UNITS.ITEM_CODE LEFT OUTER JOIN INV_ITEM_PRICE AS INV_ITEM_PRICE_1 ON INV_ITEM_DIRECTORY_UNITS.UNIT_CODE = INV_ITEM_PRICE_1.UNIT_CODE AND INV_ITEM_DIRECTORY_UNITS.ITEM_CODE = INV_ITEM_PRICE_1.ITEM_CODE ON INV_ITEM_DIRECTORY.CODE = INV_ITEM_DIRECTORY_UNITS.ITEM_CODE WHERE        (INV_ITEM_PRICE.SAL_TYPE = '" + RATE_CODE.SelectedValue + "') AND (INV_ITEM_PRICE_1.SAL_TYPE = 'PUR')";
                }
                else
                {
                    query = "SELECT     INV_ITEM_DIRECTORY.CODE AS 'Item Code', INV_ITEM_DIRECTORY_UNITS.BARCODE AS Barcode, INV_ITEM_DIRECTORY.DESC_ENG AS [Item Name],INV_ITEM_DIRECTORY.DESC_ARB, INV_ITEM_DIRECTORY_UNITS.UNIT_CODE, INV_ITEM_PRICE.PRICE AS SALES, INV_ITEM_DIRECTORY.TaxId,  GEN_TAX_MASTER.TaxRate,INV_ITEM_DIRECTORY.HASSERIAL FROM         INV_ITEM_PRICE INNER JOIN  INV_ITEM_DIRECTORY_UNITS ON INV_ITEM_PRICE.UNIT_CODE = INV_ITEM_DIRECTORY_UNITS.UNIT_CODE AND INV_ITEM_PRICE.ITEM_CODE = INV_ITEM_DIRECTORY_UNITS.ITEM_CODE LEFT OUTER JOIN INV_ITEM_DIRECTORY ON INV_ITEM_DIRECTORY_UNITS.ITEM_CODE = INV_ITEM_DIRECTORY.CODE LEFT OUTER JOIN GEN_TAX_MASTER ON GEN_TAX_MASTER.TaxId = INV_ITEM_DIRECTORY.TaxId WHERE     (INV_ITEM_PRICE.SAL_TYPE = '" + RATE_CODE.SelectedValue + "')";

                }
                //cmd.CommandType = CommandType.Text;
                DataTable dt = new DataTable();
                //adapter.SelectCommand = cmd;
               // adapter.Fill(dt);
                dt = Model.DbFunctions.GetDataTable(query);
                source.DataSource = dt;
                dataGridItem.DataSource = source;
                dataGridItem.RowHeadersVisible = false;
            //    dataGridItem.Columns[1].Visible = false;
                dataGridItem.Columns[2].Width = 250;
                if (!hasArabic)
                {
                    dataGridItem.Columns["DESC_ARB"].Visible = false;
                }

                if (!hasTax)
                {
                    dataGridItem.Columns["TaxId"].Visible = false;
                    dataGridItem.Columns["TaxRate"].Visible = false;
                }

                dataGridItem.ClearSelection();

            }
            catch(Exception eee)
            {
                MessageBox.Show(eee.Message);
            }


        }

        private void ITEM_NAME_TextChanged(object sender, EventArgs e)
        {
            //ITEM_CODE.Text = "";
            try
            {

                if (ITEM_NAME.Text == "")
                {
                    dataGridItem.Visible = false;
                   
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

        private void ITEM_NAME_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                // bindgridview();
                if (e.KeyData == Keys.Down)
                {
                    if (dataGridItem.Visible == true)
                    {
                       
                        dataGridItem.Focus();
                        dataGridItem.CurrentCell = dataGridItem.Rows[0].Cells[2];
                    }
                    else
                    {



                    }
                }
                else if (e.KeyCode == Keys.Up)
                {
                    source.Filter = "";
                    bindgridview();
                    dataGridItem.Visible = true;
                    dataGridItem.ClearSelection();

                }
            }
            catch
            {
            }
        }


        public bool Valid()
        {
            return true;
        }


        private void dataGridItem_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == (Keys.Enter | Keys.Tab))
                {
                    string Itemcode = dataGridItem.CurrentRow.Cells[0].Value.ToString();
                    string ItemName = dataGridItem.CurrentRow.Cells["Item Name"].Value.ToString();
                    string Unit = dataGridItem.CurrentRow.Cells["UNIT_CODE"].Value.ToString();
                    string Qty = "1";
                    if (treeView1.SelectedNode.Text == "Same Item")
                    {
                        DG_SAME_BY.Rows.Add(Itemcode, ItemName, Unit, "Remove");
                    }
                    else if (treeView1.SelectedNode.Text == "Different Item")
                    {
                        DG_BY_DIFF.Rows.Add(Itemcode, ItemName, Unit,Qty,1,"END","Remove");
                    }
                    dataGridItem.Visible = false;
                }
            }
            catch
            {
            }
        }

        public void Create_DGOFFERITEMS_RATE_SPANING()
        {
            try
            {//ading header
            //    DGOFFERITEMS_RATE.Columns.Add("BuyGroupItems", "Buy Group Items");
            //    DGOFFERITEMS_RATE.Columns.Add("none", "");
            //    DGOFFERITEMS_RATE.Columns.Add("FreeGroupItems", "Free Group Items");
            //    //end adding header

                //adding rows
                DG_SAME_OFF_RATE.Rows.Add();
                DG_SAME_OFF_RATE.Rows.Add();
                DG_SAME_OFF_RATE.Rows.Add();  
                DG_SAME_OFF_RATE.Rows.Add();
                //end adding rows

                //spaning first column and row
                 var imgCell = (DataGridViewTextBoxCellEx)DG_SAME_OFF_RATE[0, 0];
                imgCell.ColumnSpan = 3;
                //end spaning firs columln and row
                //ading text values to first row and column
                DG_SAME_OFF_RATE.Rows[0].Cells[0].Value = "Buy Group Items";
                DG_SAME_OFF_RATE.Rows[0].Cells[3].Value = "Free Group Items";
                //end adding text values to first row and column


                var imgCell2=(DataGridViewTextBoxCellEx)DG_SAME_OFF_RATE[0, 1];
                imgCell2.ColumnSpan = 3;

                DG_SAME_OFF_RATE.Rows[1].Cells[0].Value = "Item Rate & Quantity";
                DG_SAME_OFF_RATE.Rows[1].Cells[3].Value = "Item Quantity";


                var imgCell3 = (DataGridViewTextBoxCellEx)DG_SAME_OFF_RATE[3, 1];
                imgCell3.RowSpan = 2;
               
                if (cmbRateType.Text != "Rate Range")
                {
                    var imgCell4 = (DataGridViewTextBoxCellEx)DG_SAME_OFF_RATE[0, 2];
                    imgCell4.ColumnSpan = 2;
                    var imgCell5 = (DataGridViewTextBoxCellEx)DG_SAME_OFF_RATE[0, 3];
                    imgCell5.ColumnSpan = 2;
                    DG_SAME_OFF_RATE.Rows[2].Cells[0].Value = cmbRateType.Text;
                }
                else
                {
                    DG_SAME_OFF_RATE.Rows[2].Cells[1].Value = "Rate <=";
                    DG_SAME_OFF_RATE.Rows[2].Cells[0].Value = "Rate >";

                }

              
                DG_SAME_OFF_RATE.Rows[2].Cells[2].Value = "Quantity";
            

             

                for (int i = 0; i < DG_SAME_OFF_RATE.Rows.Count-1; i++)
                {
                    DG_SAME_OFF_RATE.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.Gainsboro;
                }
            }
            catch(Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode.Text == "Same Item")
            {
                cmbRateType.Visible = CHK_SAME_OFF_RATE.Checked;
                cmbRateType.SelectedIndex = 0;
                DG_SAME_OFF_RATE.Visible = CHK_SAME_OFF_RATE.Checked;
            }
            else if (treeView1.SelectedNode.Text == "Different Item")
            {

            }
          
        }
        string ratetype = "QUANTITY";
        bool BUY_IsRate = false;
        bool BUY_IsQuantity = false;
        bool BUY_IsBundle = false;

        bool OFF_IsRate = false;
        bool OFF_IsQuantity = false;
        bool OFF_IsBundle = false;

        public void FindRateType()
        {
            try
            {
                BUY_IsRate = false;
                BUY_IsQuantity = false;
                BUY_IsBundle = false;
                if (treeView1.SelectedNode.Text == "Same Item")
                {
                    if (CHK_SAME_OFF_RATE.Checked)
                    {
                        BUY_IsRate = true;
                        BUY_IsQuantity = false;

                        OFF_IsQuantity = true;
                        OFF_IsRate = false;
                    }
                    else
                    {
                        BUY_IsQuantity = true;
                        OFF_IsQuantity = true;
                    
                    }
                }
                else if (treeView1.SelectedNode.Text == "Different Item")
                {
                    if (CHK_DIFF_BY_BUNDLEITEMS.Checked)
                    {

                        BUY_IsRate = false;
                        BUY_IsQuantity = true;
                        BUY_IsBundle = true;

                    }
                    else
                    {
                        BUY_IsRate = false;
                        BUY_IsQuantity = true;
                        BUY_IsBundle = false;
                    }

                    if (CHK_DIFF_OFF_BUNDLEITEMS.Checked)
                    {
                       OFF_IsRate = false;
                      OFF_IsQuantity = true;
                        OFF_IsBundle = true;

                    }
                    else
                    {
                        OFF_IsRate = false;
                        OFF_IsQuantity = true;
                        OFF_IsBundle = false;
                    }
                }
            }
            catch
            {
            }
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            try
            {
                //conn.Open();

                FindRateType();
                if (ID == "")
                {








                    string query = "INSERT INTO PROMO_HDR (PRO_CODE,PRO_DESC,PRO_ARABIC,DATE_START,DATE_END,DATE_HIJ_START,DATE_HIJ_END,ITEM_TYPE,BUY_ISRATE,BUY_ISQUANTITY,BUY_ISBUNDLE,OFF_ISRATE,OFF_ISQUANTITY,OFF_ISBUNDLE,RATETYPE) VALUES ('" + CODE.Text + "','" + DESC_ENG.Text + "','" + DESC_ARB.Text + "','" + START_DATE.Value + "','" + DATE_END.Value + "','','','" + treeView1.SelectedNode.Text + "','" + BUY_IsRate + "','" + BUY_IsQuantity + "','" + BUY_IsBundle + "','" +OFF_IsRate + "','" + OFF_IsQuantity + "','" + OFF_IsBundle + "','" + ratetype + "');SELECT MAX(PROMO_ID) FROM PROMO_HDR";
                   

                   // PROMOID = Convert.ToInt32(cmd.ExecuteScalar());
                    PROMOID = Convert.ToInt32(Model.DbFunctions.GetAValue(query));
                   



                    if (treeView1.SelectedNode.Text == "Same Item")
                    { 
                        ADDGROUPITEMDETAIL_SAMEITEM();
                        if (CHK_SAME_OFF_RATE.Checked)
                        {


                            ADDOFFERITEMDETAIL_RATE_SAMEITEM();
                        }
                        else
                        {
                            ADDOFFERITEMDETAIL_QTY_SAMEITEM();
                        }

                    }
                    else if (treeView1.SelectedNode.Text == "Different Item")
                    {
                        ADDGROUPITEMDETAIL_DIFFENTITEM();
                        ADDOFFERITEMDETAIL_QTY_DIFFERENTITEM();
                    }

                }

              

                //conn.Close();
            }
            catch(Exception ee)
            {
               // conn.Close();
                MessageBox.Show(ee.Message);
            }
        }


        public void ADDGROUPITEMDETAIL_SAMEITEM()
        {
            try
            {
                if (ID == "")
                {
                    string query="INSERT INTO PROMO_ITEM_DETAIL(PROMO_ID,ITEMCODE,UOM)";
                    for(int i=0;i<DG_SAME_BY.Rows.Count;i++)
                    {
                        query = query + " select '"+PROMOID+"','"+ DG_SAME_BY.Rows[i].Cells["ItemCode"].Value + "','" + DG_SAME_BY.Rows[i].Cells["Unit"].Value + "' UNION ALL ";
                    }
                    query = query.Substring(0, query.Length - 10);
                    string cmd = query;
                   //cmd.Connection = conn;
                   //cmd.CommandType = CommandType.Text;
                   
                  
                   // cmd.ExecuteNonQuery();
                    Model.DbFunctions.InsertUpdate(cmd);
                }
                else
                {
                }
            }
            catch(Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        public void ADDGROUPITEMDETAIL_DIFFENTITEM()
        {
            try
            {
                if (ID == "")
                {
                    string query = "INSERT INTO PROMO_ITEM_DETAIL(PROMO_ID,ITEMCODE,UOM,QTY1)";
                    for (int i = 0; i < DG_BY_DIFF.Rows.Count; i++)
                    {
                        query = query + " select '" + PROMOID + "','" + DG_BY_DIFF.Rows[i].Cells["DIFF_ITEMCODE"].Value + "','" + DG_BY_DIFF.Rows[i].Cells["DIFF_UNIT"].Value + "','" + DG_BY_DIFF.Rows[i].Cells["DIFF_QTY"].Value + "' UNION ALL ";
                    }
                    query = query.Substring(0, query.Length - 10);
                    //cmd.CommandText = query;
                    string cmd = query;
                    //cmd.Connection = conn;
                    //cmd.CommandType = CommandType.Text;


                    //cmd.ExecuteNonQuery();
                    Model.DbFunctions.InsertUpdate(cmd);

                }
                else
                {
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }



        public void ADDOFFERITEMDETAIL_QTY_SAMEITEM()
        {
            try
            {
                if (ID == "")
                {
                    var checkedButton = this.Controls.OfType<RadioButton>()
                                    .FirstOrDefault(r => r.Checked);
                 
                    string query = "INSERT INTO PROMO_OFFER_DTL(PROMO_ID,ITEM_QTY,OFFER_ITEM_QTY,OFFERITEM)";
                    for (int i = 0; i < DG_SAME_OFF_QTY.Rows.Count-1; i++)
                    {
                        query = query + " select '" + PROMOID + "','" + DG_SAME_OFF_QTY.Rows[i].Cells["BuyGroupDetails"].Value + "','" + DG_SAME_OFF_QTY.Rows[i].Cells["FreeGroupDetails"].Value + "','"+ checkedButton.Text+"' UNION ALL ";
                    }
                    query = query.Substring(0, query.Length - 10);
                    string cmd = query;
                    //cmd.Connection = conn;
                    //cmd.CommandType = CommandType.Text;


                    //cmd.ExecuteNonQuery();
                    Model.DbFunctions.InsertUpdate(cmd);

                }
                else
                {
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }


        public void ADDOFFERITEMDETAIL_RATE_SAMEITEM()
        {
            try
            {
                if (ID == "")
                {
                    var checkedButton = this.Controls.OfType<RadioButton>()
                                    .FirstOrDefault(r => r.Checked);

                    string query = "INSERT INTO PROMO_OFFER_DTL(PROMO_ID,RATELESSTHAN,RATEGREATERTHAN,RATEEQUAL,ITEM_QTY,OFFER_ITEM_QTY,OFFERITEM,LESSTHANEQUAL)";
                    if (cmbRateType.SelectedIndex == 0)
                    {
                        query = query + " select '" + PROMOID + "','0','" + DG_SAME_OFF_RATE.Rows[DG_SAME_OFF_RATE.Rows.Count - 1].Cells[0].Value + "','0','" + DG_SAME_OFF_RATE.Rows[DG_SAME_OFF_RATE.Rows.Count - 1].Cells[2].Value + "','" + DG_SAME_OFF_RATE.Rows[DG_SAME_OFF_RATE.Rows.Count - 1].Cells[3].Value + "','" + checkedButton.Text + "','0' UNION ALL ";
                    }
                    else if (cmbRateType.SelectedIndex == 1)
                    {
                        query = query + " select '" + PROMOID + "','0','0','" + DG_SAME_OFF_RATE.Rows[DG_SAME_OFF_RATE.Rows.Count - 1].Cells[0].Value + "','" + DG_SAME_OFF_RATE.Rows[DG_SAME_OFF_RATE.Rows.Count - 1].Cells[2].Value + "','" + DG_SAME_OFF_RATE.Rows[DG_SAME_OFF_RATE.Rows.Count - 1].Cells[3].Value + "','" + checkedButton.Text + "','0' UNION ALL ";
                    }
                    else if (cmbRateType.SelectedIndex == 2)
                    {
                        query = query + " select '" + PROMOID + "','0','0','0','" + DG_SAME_OFF_RATE.Rows[DG_SAME_OFF_RATE.Rows.Count - 1].Cells[2].Value + "','" + DG_SAME_OFF_RATE.Rows[DG_SAME_OFF_RATE.Rows.Count - 1].Cells[3].Value + "','" + checkedButton.Text + "','" + DG_SAME_OFF_RATE.Rows[DG_SAME_OFF_RATE.Rows.Count - 1].Cells[0].Value + "' UNION ALL ";
                    }
                    else
                    {
                        query = query + " select '" + PROMOID + "','" + DG_SAME_OFF_RATE.Rows[DG_SAME_OFF_RATE.Rows.Count - 1].Cells[0].Value + "','" + DG_SAME_OFF_RATE.Rows[DG_SAME_OFF_RATE.Rows.Count - 1].Cells[1].Value + "','0','" + DG_SAME_OFF_RATE.Rows[DG_SAME_OFF_RATE.Rows.Count - 1].Cells[2].Value + "','" + DG_SAME_OFF_RATE.Rows[DG_SAME_OFF_RATE.Rows.Count - 1].Cells[3].Value + "','" + checkedButton.Text + "','0' UNION ALL ";
                    }
                    
                    query = query.Substring(0, query.Length - 10);
                    string cmd = query;
                    //cmd.Connection = conn;
                    //cmd.CommandType = CommandType.Text;


                    //cmd.ExecuteNonQuery();
                    Model.DbFunctions.InsertUpdate(cmd);

                }
                else
                {
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        public void ADDOFFERITEMDETAIL_QTY_DIFFERENTITEM()
        {
            try
            {
                if (ID == "")
                {
                    var checkedButton = this.Controls.OfType<RadioButton>()
                                    .FirstOrDefault(r => r.Checked);

                    string query = "INSERT INTO PROMO_OFFER_DTL(PROMO_ID,ITEM_QTY,ITEMCODE,UNIT,SETNO,CONDITION,OFFERITEM)";
                    for (int i = 0; i < DG_DIFF_OFF_QTY.Rows.Count - 1; i++)
                    {
                        query = query + " select '" + PROMOID + "','" + DG_DIFF_OFF_QTY.Rows[i].Cells["OFF_DIFF_QTY"].Value + "','" + DG_DIFF_OFF_QTY.Rows[i].Cells["OFF_DIFF_ITEMCODE"].Value + "','" + DG_DIFF_OFF_QTY.Rows[i].Cells["OFF_DIFF_UNIT"].Value + "','" + DG_DIFF_OFF_QTY.Rows[i].Cells["OFF_DIFF_SETNO"].Value + "','" + DG_DIFF_OFF_QTY.Rows[i].Cells["OFF_DIFF_CONDITION"].Value + "','" + checkedButton.Text + "' UNION ALL ";
                    }
                    query = query.Substring(0, query.Length - 10);
                    //cmd.CommandText = query;
                    //cmd.Connection = conn;
                    //cmd.CommandType = CommandType.Text;
                    string cmd = query;

                   // cmd.ExecuteNonQuery();
                    Model.DbFunctions.InsertUpdate(cmd);

                }
                else
                {
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }


        private void cmbRateType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DG_SAME_OFF_RATE.Rows.Clear(); 
            Create_DGOFFERITEMS_RATE_SPANING();
        }

        

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

            if (treeView1.SelectedNode.Text == "Same Item")
            {
                //by items grid
                DG_BY_DIFF.Visible = false;
                DG_SAME_BY.Visible = true;
                PNL_DIFF_BY_BUNDLE.Visible = false;
                PNL_DIFF_OFF_BUNDLE.Visible = false;
             

                //offer items grid
                PNL_SAME_OFF_RATE.Visible = true;
                DG_SAME_OFF_QTY.Visible = true;
                DG_SAME_OFF_RATE.Visible = false;
                DG_DIFF_OFF_QTY.Visible = false;


            }
            else if (treeView1.SelectedNode.Text == "Different Item")
            {
                
                PNL_SAME_OFF_RATE.Visible = false;
                DG_BY_DIFF.Visible = true;
                PNL_DIFF_BY_BUNDLE.Visible = true;
                PNL_DIFF_OFF_BUNDLE.Visible = true;

                //OFFER ITEMS GRID
                DG_SAME_BY.Visible = false;
                checkBox1.Checked = false;
                DG_SAME_OFF_QTY.Visible = false;
                DG_SAME_OFF_RATE.Visible = false;
                DG_DIFF_OFF_QTY.Visible = true;
               
            }
        }

        private void chkBudleItems_CheckedChanged(object sender, EventArgs e)
        {
            DIFF_CONDITION.Visible = CHK_DIFF_BY_BUNDLEITEMS.Checked;
            DIFF_SETNO.Visible = CHK_DIFF_BY_BUNDLEITEMS.Checked;
            if (CHK_DIFF_BY_BUNDLEITEMS.Checked)
            {
                ratetype = "BUNDLES";
            }
            else
            {
                ratetype = "QUANTITY";
            }
        }

        private void checkBox2_CheckedChanged_1(object sender, EventArgs e)
        {
            OFF_DIFF_CONDITION.Visible = CHK_DIFF_OFF_BUNDLEITEMS.Checked;
            OFF_DIFF_SETNO.Visible = CHK_DIFF_OFF_BUNDLEITEMS.Checked;
        }

      

        private void DGOFFERITEMS_DIFFERENT_ITEMS_QTY_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                Item_Helper h = new Item_Helper(0);
                if (h.ShowDialog() == DialogResult.OK && h.c != null)
                {
                    DG_DIFF_OFF_QTY.CurrentCell.Value = Convert.ToString(h.c[0].Value);
                    DG_DIFF_OFF_QTY.CurrentRow.Cells["OFF_DIFF_ITEMNAME"].Value = Convert.ToString(h.c[2].Value);
                    DG_DIFF_OFF_QTY.CurrentRow.Cells["OFF_DIFF_UNIT"].Value = Convert.ToString(h.c[4].Value);
                    DG_DIFF_OFF_QTY.CurrentRow.Cells["OFF_DIFF_QTY"].Value = 1;
                    DG_DIFF_OFF_QTY.CurrentRow.Cells["OFF_DIFF_CONDITION"].Value = "END";
                    DG_DIFF_OFF_QTY.CurrentRow.Cells["OFF_DIFF_REMOVE"].Value = "Remove";
               

                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {

        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            if (callfrom == 1)
            {
                this.Close();
            }
            else
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
                    }
                }
                catch
                {
                    this.Close();
                }
            }
        }

        
    }
}
