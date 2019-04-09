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
using Sys_Sols_Inventory.Model;
namespace Sys_Sols_Inventory
{
    public partial class Country : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        //private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        //private SqlCommand cmd = new SqlCommand();
        //private SqlDataAdapter adapter = new SqlDataAdapter();
        private DataTable table = new DataTable();
        public genEnum currentForm = genEnum.Country;
        clsCommon com = new clsCommon();
        private bool HasArabic = true;
        public int errorflag = 0;
        private BindingSource source = new BindingSource();
        private string ID = "";
        private string tableName = "";
        Initial mdi = (Initial)Application.OpenForms["Initial"];
        Login lg = (Login)Application.OpenForms["Login"];
        int index=-1;
        public string code, name;
        bool duplicate = false;
        public int callfrom=0;

        public Country(genEnum form)
        {
            InitializeComponent();
            currentForm = form;
            //cmd.Connection = conn;
            //adapter.SelectCommand = cmd;
            HasArabic = General.IsEnabled(Settings.Arabic);
            switch (currentForm)
            {
                case genEnum.Country:
                    tableName = "GEN_COUNTRY";
                    this.Text = "Country";
                    break;
                case genEnum.City:
                    tableName = "GEN_CITY";
                    this.Text = "City";
                    break;
                case genEnum.Type:
                    tableName = "INV_ITEM_TYPE";
                    this.Text = "Type";
                    break;
                case genEnum.Group:
                    tableName = "INV_ITEM_GROUP";
                    this.Text = "Group";
                    break;
                case genEnum.Category:
                    tableName = "INV_ITEM_CATEGORY";
                    this.Text = "Category";
                    break;
                case genEnum.TradeMark:
                    tableName = "INV_ITEM_TM";
                    this.Text = "Trade Mark";
                    break;
                case genEnum.Units:
                    tableName = "INV_UNIT";
                    this.Text = "Units";
                    break;
                case genEnum.RateSlab:
                    tableName = "INV_RATE_SLAB";
                    this.Text = "Rate Slab";
                    break;
                case genEnum.PayType:
                    tableName = "GEN_PAYTYPE";
                    this.Text = "Pay Type";
                    break;
                case genEnum.Branch:
                    tableName = "GEN_BRANCH";
                    this.Text = "Branch";
                    break;
                case genEnum.DiscountType:
                    tableName = "GEN_DISCOUNT_TYPE";
                    this.Text = "Discount Type";
                    break;
                case genEnum.PriceType:
                    tableName = "GEN_PRICE_TYPE";
                    this.Text = "Price Type";
                    break;
                case genEnum.SupplierType:
                    tableName = "PAY_SUPPLIER_TYPE";
                    this.Text = "Supplier Type";
                    break;
                case genEnum.Location:
                    tableName = "GEN_LOCATION";
                    this.Text = "Item Location";
                    break;
                default:
                    break;
            }
        }
        int flagclose = 0;
        public Country(genEnum form,int callfromto) : this(form)
        {
            callfrom = callfromto;
        }

        public Country(genEnum form, int callfromto ,int flag)   : this(form)
        {
            callfrom = callfromto;
            flagclose = flag;
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {



            if (keyData == (Keys.Escape))
            {
                if ( flagclose==1)
                {
                    btnQuit.PerformClick();
                    return true;
                }
                else
                {
                    ComponentFactory.Krypton.Navigator.KryptonPage k = new ComponentFactory.Krypton.Navigator.KryptonPage();
                    k = mdi.maindocpanel.SelectedPage;
                    if (k.Name == "Home")
                    {


                    }
                    else
                    {
                        mdi.maindocpanel.Pages.Remove(k);
                    }
                }
            }
            //  else if (e.KeyCode == Keys.S && e.Control)
            else if (keyData == (Keys.Alt | Keys.S))
            {


            }
            if (keyData == (Keys.F3))
            {

            }
            else if (keyData == (Keys.Alt | Keys.N))
            {



            }




            return base.ProcessCmdKey(ref msg, keyData);

        }
        private void Country_Load(object sender, EventArgs e)
        {
            try
            {
                com.TblName = tableName;
                if (HasArabic)
                {
                   // cmd.CommandText = "SELECT  * FROM " + tableName;
                    table = com.SelectWithAr();
                }
                else
                {
                    table = com.SelectWithoutAr();
                }
                //adapter.Fill(table);
                source.DataSource = table;
                dgCommon.DataSource = source;
                DataGridViewColumnCollection c = dgCommon.Columns;
                c[0].HeaderText = "Code";
                c[1].HeaderText = "Eng. Name";
                if (HasArabic)
                {
                    c[2].HeaderText = "Arb. Name";
                }
                CODE.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if(!HasArabic)
            {
                PnlArabic.Visible = false;
            }
        }

        private bool valid()
        {
            
            if (CODE.Text.Trim() != "" && DESC_ENG.Text.Trim() != "")
            {
                com.TblName = tableName;
                com.Code = CODE.Text;
                com.Desc_eng = DESC_ENG.Text;
                com.Desc_arb = DESC_ARB.Text;
                com.ID = ID;
                //if (CheckDuplication() && btnSave.Text != "Update")
                //{
                //    MessageBox.Show("Duplicate CODE found");
                //    btnClear.PerformClick();
                //    return false;
                //}
                if (Duplication())
                {
                    MessageBox.Show("Duplicate CODE found");
                    CODE.Focus();
                    return false;
                }
                return true;
            }
            else
            {
                MessageBox.Show("Please fill the following fields. \n 1.Code \n 2.Eng. Name");
                CODE.Focus();
                return false;
            }
        }

        public bool Duplication()
        {
            int flag=0;
            for (int i = 0; i < dgCommon.Rows.Count; i++)
            {
                if (dgCommon.Rows[i].Cells[0].Value.ToString() == CODE.Text)
                {
                    if (i == index)
                    {
                        
                    }
                    else
                    {
                        flag=1;
                        break;
                    }
                }
                else
                {
                   
                }
            }

            if (flag == 1)
                return true;
            else
                return false;
        }

        public bool CheckDuplication()
        {
            duplicate = false;
            try
            {
                DataTable dt = new DataTable();
                //cmd.CommandText = "SELECT  CODE from " + tableName+" where CODE='"+CODE.Text+"'";
                //adapter.Fill(dt);
                
                dt = com.Select();
                if (dt.Rows.Count > 0)
                {
                   duplicate = true;
                }
            }
            catch(Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
            return duplicate;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (valid())
            {
                try
                {
                    if (ID == "")
                    {                                     // cmd.CommandText = "INSERT INTO " + tableName + "(CODE,DESC_ENG,DESC_ARB)" +
                                      //"VALUES ('" + CODE.Text + "','" + DESC_ENG.Text + "','" + DESC_ARB.Text + "')";
                        com.add();

                        if (HasArabic)
                        {
                            table.Rows.Add(CODE.Text, DESC_ENG.Text, DESC_ARB.Text);
                        }
                        else
                        {
                            table.Rows.Add(CODE.Text, DESC_ENG.Text);
                        }
                    }
                    else
                    {
                        if (tableName == "GEN_PRICE_TYPE")
                        {
                            if (ID == "PUR" || ID == "RTL")
                            {
                                MessageBox.Show("Item Cannot Be Updated");
                                errorflag = 1;
                            }
                            else
                            {
                               // cmd.CommandText = "UPDATE " + tableName + " SET CODE = '" + CODE.Text + "',DESC_ENG = '" + DESC_ENG.Text + "',DESC_ARB = '" + DESC_ARB.Text + "' WHERE CODE = '" + ID + "'";
                                com.Update();
                                DataRow row = table.Select("CODE = '" + ID + "'").First();
                                row[0] = CODE.Text;
                                row[1] = DESC_ENG.Text;
                                if (HasArabic)
                                {
                                    row[2] = DESC_ARB.Text;
                                }
                            }
                        }
                        else
                        {
                           // cmd.CommandText = "UPDATE " + tableName + " SET CODE = '" + CODE.Text + "',DESC_ENG = '" + DESC_ENG.Text + "',DESC_ARB = '" + DESC_ARB.Text + "' WHERE CODE = '" + ID + "'";
                            com.Update();
                            DataRow row = table.Select("CODE = '" + ID + "'").First();
                            row[0] = CODE.Text;
                            row[1] = DESC_ENG.Text;
                            if (HasArabic)
                            {
                                row[2] = DESC_ARB.Text;
                            }
                        }
                    }
                    if (errorflag == 0)
                    {
                       
                        if (ID == "")
                        {
                            MessageBox.Show(this.Text + " Added!");
                        }
                        else
                        {
                            MessageBox.Show(this.Text + " Updated!");
                        }
                    }
                    errorflag = 0;
                    if (dgCommon.Rows.Count > 0 && dgCommon.CurrentRow != null)
                    {
                        code = CODE.Text;
                        name = DESC_ENG.Text;
                    }


                    if (callfrom != 1)
                    {
                        btnClear.PerformClick();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                   
                }               
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            index = -1;
            ID = "";
            CODE.Text = "";
            DESC_ENG.Text = "";
            DESC_ARB.Text = "";
            duplicate = false;
            CODE.Focus();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (ID != "")
            {
                if (MessageBox.Show("Are you sure?", "Record Deletion",MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        //conn.Open();
                        com.ID = ID;
                        com.Delete();
                        table.Select("CODE = '" + ID + "'").First().Delete();
                        //  cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }


                    finally
                    {
                       // conn.Close();
                    }

                    MessageBox.Show(this.Text+" Deleted!");
                    btnClear.PerformClick();
                }
            }
            else
            {
                MessageBox.Show("Select an Item to Delete");
            }
        }

        private void btnQuit_Click(object sender, EventArgs e)
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

        private void dgCommon_Click(object sender, EventArgs e)
        {
            if (dgCommon.Rows.Count > 0 && dgCommon.CurrentRow != null)
            {
            //    DataGridViewCellCollection c = dgCommon.CurrentRow.Cells;
                index = dgCommon.CurrentCell.RowIndex;
                ID = dgCommon.Rows[index].Cells[0].Value.ToString();
                CODE.Text = dgCommon.Rows[index].Cells[0].Value.ToString();
                DESC_ENG.Text = dgCommon.Rows[index].Cells[1].Value.ToString();
                if(HasArabic)
                    DESC_ARB.Text = dgCommon.Rows[index].Cells[2].Value.ToString();
            }
        }

        private void CODE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (sender is KryptonTextBox)
                {
                    string name = (sender as KryptonTextBox).Name;
                    switch (name)
                    {
                        case "CODE":
                            DESC_ENG.Focus();
                            break;
                        case "DESC_ENG":
                            if (HasArabic)
                            {
                                DESC_ARB.Focus();
                            }
                            else
                            {
                                btnSave.PerformClick();
                            }
                            break;
                        case "DESC_ARB":
                            btnSave.PerformClick();
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
