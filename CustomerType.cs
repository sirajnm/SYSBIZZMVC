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

    public partial class CustomerType : Form
    {
       
        //private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        private bool HasArabic = true;
        //private SqlCommand cmd = new SqlCommand();
        //SqlDataAdapter sda = new SqlDataAdapter();
        clsCustType custype = new clsCustType();
        Initial mdi = (Initial)Application.OpenForms["Initial"];
        Login lg = (Login)Application.OpenForms["Login"];
        private string ID = "";
        public CustomerType()
        {
            InitializeComponent();
            //cmd.Connection = conn;
            HasArabic = General.IsEnabled(Settings.Arabic);
            //sda.SelectCommand = cmd;
            
        }

        private bool valid()
        {
            if (CODE.Text != "")
            {

                return true;
            }
            else
            {
                MessageBox.Show("Enter Code");
                CODE.Focus();
                return false;

            }
        }

        public void GetCustomer()
        {
            try
            {
                DataTable dt = custype.GetCustomer();
                //cmd.CommandText = "SELECT CODE,DESC_ENG,DESC_ARB,CREDIT_LEVEL,PRICE_TYPE,DISCOUNT_TYPE FROM REC_CUSTOMER_TYPE";
             //   sda.Fill(dt);
                dataGridView1.DataSource = dt;
                if (!HasArabic)
                    dataGridView1.Columns["DESC_ARB"].Visible = false;
                   
            }
            catch { }
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            if (valid())
            {
                string priceType = "";
                string discountType = "";
                string type = "Added!";
                string credit;
                custype.Code = CODE.Text;
                custype.Desc_eng = DESC_ENG.Text;
                custype.Desc_arb = DESC_ARB.Text;
                custype.ID = ID;
                if (PRICE_TYPE.Items.Count > 0)
                {
                   
                    custype.PriceType = Convert.ToString(PRICE_TYPE.SelectedValue);

                }

                if (DISCOUNT_TYPE.Items.Count > 0)
                {
                  
                    custype.DiscountType = Convert.ToString(DISCOUNT_TYPE.SelectedValue);
                }
                if (CREDIT_LEVEL.Text == "")
                {
                    credit = "0";
                    custype.CreditLevel = 0;
                }
                else
                custype.CreditLevel = Convert.ToDecimal(CREDIT_LEVEL.Text);
                if (ID == "")
                {
                   // cmd.CommandText = "INSERT INTO REC_CUSTOMER_TYPE(CODE,DESC_ENG,DESC_ARB,CREDIT_LEVEL,PRICE_TYPE,DISCOUNT_TYPE) VALUES('"+CODE.Text+"','"+DESC_ENG.Text+"','"+DESC_ARB.Text+"','"+credit+"','"+priceType+"','"+discountType+"')";
                    custype.add();
                }
                else
                {
                    custype.Update();
                  //  cmd.CommandText = "UPDATE REC_CUSTOMER_TYPE SET CODE = '" + CODE.Text + "', DESC_ENG = '" + DESC_ENG.Text + "',DESC_ARB = '" + DESC_ARB.Text + "',CREDIT_LEVEL = '" + credit + "',PRICE_TYPE = '" + priceType + "',DISCOUNT_TYPE = '" + discountType + "' where CODE='" + ID + "'";
                    type = "Updated!";
                }

                try
                {
                    //conn.Open();
                    //cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer type "+type);
                    GetCustomer();
                    btnClear.PerformClick();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                  //  conn.Close();
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ID = "";
            CODE.Text = "";
            DESC_ENG.Text = "";
            DESC_ARB.Text = "";
            CREDIT_LEVEL.Text = "";
            if (PRICE_TYPE.Items.Count > 0)
            {
                PRICE_TYPE.SelectedIndex = 0; 
            }
            if (DISCOUNT_TYPE.Items.Count > 0)
            {
                DISCOUNT_TYPE.SelectedIndex = 0;
            }
            CODE.Focus();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //delete
            CommonHelp h = new CommonHelp(1, genEnum.CustomerType);
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

        private void btnCode_Click(object sender, EventArgs e)
        {
            CommonHelp h = new CommonHelp(0, genEnum.CustomerType);
            if (h.ShowDialog() == DialogResult.OK && h.c != null)
            {
                ID = Convert.ToString(h.c["CODE"].Value);
                CODE.Text = Convert.ToString(h.c["CODE"].Value);
                DESC_ENG.Text = Convert.ToString(h.c["DESC_ENG"].Value);
                if(HasArabic)
                DESC_ARB.Text = Convert.ToString(h.c["DESC_ARB"].Value);
                CREDIT_LEVEL.Text = Convert.ToString(h.c["CREDIT_LEVEL"].Value);
                if (Convert.ToString(h.c["PRICE_TYPE"].Value) != "")
                {
                    PRICE_TYPE.SelectedValue = h.c["PRICE_TYPE"].Value;
                }

                if (Convert.ToString(h.c["DISCOUNT_TYPE"].Value) != "")
                {
                    DISCOUNT_TYPE.SelectedValue = h.c["DISCOUNT_TYPE"].Value;
                }
            }
        }

        public void discounttype()
        {
            try
            {
                DataTable dt = custype.GetDiscoundType();
              //  cmd.CommandText = "SELECT CODE,DESC_ENG AS DESC_ENG FROM GEN_DISCOUNT_TYPE";
             //   sda.Fill(dt);
                DISCOUNT_TYPE.DataSource = dt;
                DISCOUNT_TYPE.DisplayMember = "DESC_ENG";
                DISCOUNT_TYPE.ValueMember = "CODE";
            }
            catch
            { }
        }
        public void PriceType()
        {
            try
            {
                DataTable dt = custype.GetPriceType();
               // cmd.CommandText = "SELECT CODE,CODE+' - '+DESC_ENG AS DESC_ENG FROM GEN_PRICE_TYPE";
              //  sda.Fill(dt);
                PRICE_TYPE.DataSource = dt;
                PRICE_TYPE.DisplayMember = "DESC_ENG";
                PRICE_TYPE.ValueMember = "CODE";
            }
            catch
            { }
        }
        private void CustomerType_Load(object sender, EventArgs e)
        {
            CREDIT_LEVEL.KeyPress +=new KeyPressEventHandler(General.OnlyFloat);
            if (!HasArabic)
            {
                PnlArabic.Visible = false;
            }
            PriceType();
            discounttype();
            ActiveControl = CODE;
            GetCustomer();
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
                                CREDIT_LEVEL.Focus();
                            }
                            break;
                        case "DESC_ARB":
                            CREDIT_LEVEL.Focus();
                            break;
                        case "CREDIT_LEVEL":
                            PRICE_TYPE.Focus();
                            break;
                        case "PRICE_TYPE":
                            DISCOUNT_TYPE.Focus();
                            break;
                        case "DISCOUNT_TYPE":
                            btnSave.PerformClick();
                            break;


                        default:
                            break;
                    }
                  
                }
                if (sender is KryptonComboBox)
                {
                    string name = (sender as KryptonComboBox).Name;
                      switch (name)
                    {
                        case "PRICE_TYPE":
                            btnSave.PerformClick();
                            break;
                        case "DISCOUNT_TYPE":
                            btnSave.PerformClick();
                            break;


                        default:
                            break;
                    }
                }

            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count > 0 && dataGridView1.CurrentRow != null)
                {
                    DataGridViewCellCollection c = dataGridView1.CurrentRow.Cells;
                    ID = Convert.ToString(c["CODE"].Value);
                    CODE.Text = Convert.ToString(c["CODE"].Value);
                    DESC_ENG.Text = Convert.ToString(c["DESC_ENG"].Value);
                    if (HasArabic)
                        DESC_ARB.Text = Convert.ToString(c["DESC_ARB"].Value);
                    CREDIT_LEVEL.Text = Convert.ToString(c["CREDIT_LEVEL"].Value);
                    if (Convert.ToString(c["PRICE_TYPE"].Value) != "")
                    {
                        PRICE_TYPE.SelectedValue = c["PRICE_TYPE"].Value;
                    }

                    if (Convert.ToString(c["DISCOUNT_TYPE"].Value) != "")
                    {
                        DISCOUNT_TYPE.SelectedValue = c["DISCOUNT_TYPE"].Value;
                    }
                }
            }
            catch
            { }
        }
    }
}