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
    public partial class Discount_Types : Form
    {
          private bool HasArabic = true;
          public genEnum currentForm = genEnum.DiscountType;
          Initial mdi = (Initial)Application.OpenForms["Initial"];
          Login lg = (Login)Application.OpenForms["Login"];
          public int callfrom = 0;
          public Discount_Types(genEnum form)
        {
            InitializeComponent();
              HasArabic = General.IsEnabled(Settings.Arabic);
              currentForm = form;
        }
          public Discount_Types(genEnum form, int callfromto): this(form)
        {
            callfrom = callfromto;
        }
        //private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        //private SqlCommand cmd = new SqlCommand();
        //private SqlDataAdapter adapter = new SqlDataAdapter();
        private DataTable table = new DataTable();
       
         private BindingSource source = new BindingSource();
        private string ID = "";
       
        private void Discount_Types_Load(object sender, EventArgs e)
        {
            
            if(!HasArabic)
            {
                DESC_ARB.Enabled = false;
                PnlArabic.Visible = false;
               
            }

            BindTable();
        }
        public void BindTable()
        {
            try
            {
                table.Clear();
                //conn.Open();
                //cmd.Connection = conn;
               string query = "SELECT     CODE, DESC_ENG, DESC_ARB, TYPE, VALUE FROM         GEN_DISCOUNT_TYPE";
                //adapter.SelectCommand = cmd;
                //adapter.Fill(table);
               table=Model.DbFunctions.GetDataTable(query);
              
                source.DataSource = table;
                dgItems.DataSource = source;
                if (!HasArabic)
                    dgItems.Columns["DESC_ARB"].Visible = false;
                dgItems.Columns["CODE"].Visible = false;
                DataGridViewColumnCollection c = dgItems.Columns;
                c[0].HeaderText = "Code";
                
                c[1].HeaderText = "Eng. Name";
                if (HasArabic)
                    c[2].HeaderText = "Arb. Name";
                c[3].HeaderText = "Discount Type";
                c[4].HeaderText = "Discount Value";
                //int colWidth = (dgItems.Width - 100) / 2;
                //c[1].Width = colWidth - 22;
                //if (HasArabic)
                //    c[2].Width = colWidth - 22;
                DESC_ENG.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //conn.Close();
            }
        }
        
   
        private bool valid()
        {
            if (VALUE.Text.Trim() != "" && DESC_ENG.Text.Trim() != "")
            {
                return true;
            }
            else
            {
                MessageBox.Show("Please fill the following fields. \n 1.Code \n 2.Value");
                return false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (valid())
            {
                try
                {
                    string query = "";
                    //conn.Open();
                    if (ID == "")
                    {
                        //cmd.Parameters.Clear();
                        query = "INSERT INTO  GEN_DISCOUNT_TYPE (DESC_ENG, DESC_ARB, TYPE, VALUE)" +
                                      "VALUES ('" + DESC_ENG.Text + "','" + DESC_ARB.Text + "','" + DISCOUNT_TYPE.Text + "','" + VALUE.Text + "')";
                       
                    }
                    else
                    {
                        //cmd.Parameters.Clear();
                        query = "UPDATE GEN_DISCOUNT_TYPE  SET DESC_ENG = '" + DESC_ENG.Text + "',DESC_ARB = '" + DESC_ARB.Text + "',TYPE = '" + DISCOUNT_TYPE.Text + "',VALUE='" + VALUE.Text + "'  WHERE CODE = '" + ID + "'";
                        
                    }
                    //cmd.ExecuteNonQuery();
                    Model.DbFunctions.InsertUpdate(query);
                    if (ID == "")
                    {
                        MessageBox.Show(this.Text + " Added!");
                    }
                    else
                    {
                        MessageBox.Show(this.Text + " Updated!");
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    //conn.Close();
                }

                BindTable();
                btnClear.PerformClick();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            DESC_ENG.Text = "";
            DESC_ARB.Text = "";
            VALUE.Text = "";
            DISCOUNT_TYPE.SelectedIndex = -1;
            ID = "";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (ID == "")
            {
                btnClear.PerformClick();
            }
            else
            {
                if (MessageBox.Show("Are you sure?", "Record Deletion", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        //conn.Open();
                        string query = "DELETE FROM GEN_DISCOUNT_TYPE WHERE CODE = '" + ID + "'";

                        //cmd.ExecuteNonQuery();
                        Model.DbFunctions.InsertUpdate(query);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        //conn.Close();
                    }

                    MessageBox.Show(this.Text + " Deleted!");
                    BindTable();
                    btnClear.PerformClick();
                }

            }
        }

        private void dgItems_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgItems.Rows.Count > 0 && dgItems.CurrentRow != null)
                {
                    DataGridViewCellCollection c = dgItems.CurrentRow.Cells;
                    ID = c[0].Value.ToString();
                    DESC_ENG.Text = c["DESC_ENG"].Value.ToString();
                    DESC_ARB.Text = c["DESC_ARB"].Value.ToString();
                    DISCOUNT_TYPE.Text = c["TYPE"].Value.ToString();
                    VALUE.Text = c["VALUE"].Value.ToString();
                }
            }
            catch { }
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
    }
}
