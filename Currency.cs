using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using ComponentFactory.Krypton.Toolkit;
using Sys_Sols_Inventory.Model;


namespace Sys_Sols_Inventory
{
       
    public partial class Currency : Form
    {
    //    private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
    //    private SqlCommand cmd = new SqlCommand();
        private bool HasArabic = General.IsEnabled(Settings.Arabic);
        private SqlDataAdapter adapter = new SqlDataAdapter();
        private DataTable table = new DataTable();
          private string ID = "";
         private BindingSource source = new BindingSource();
        public Currency()
        {
            InitializeComponent();
        }


        private bool valid()
        {
            if (Txt_code.Text.Trim() != "" && txt_english.Text.Trim() != ""&&txt_exchange.Text.Trim()!="")
            {
                return true;
            }
            else
            {
                MessageBox.Show("Please fill the following fields. \n 1.Code \n 2.Eng. Name\n 3.exchange rate");
                return false;
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
         
        }

        private void Currency_Load(object sender, EventArgs e)
        {
            if (!HasArabic)
                PnlArabic.Visible = false;
            
            bindtable();
            ActiveControl = Txt_code;
        }


        private void bindtable()
        {
            try
            {
                table.Clear();
                string query = "SELECT  * FROM GEN_CURRENCY";
                //adapter.Fill(table);
                table = DbFunctions.GetDataTable(query);
                Grd_Currency.DataSource = table;
                DataGridViewColumnCollection c = Grd_Currency.Columns;
                c[0].HeaderText = "Code";
                c[1].HeaderText = "Eng. Name";
                if (HasArabic)
                    c[2].HeaderText = "Arb. Name";
                c[3].HeaderText = "Exchange Rate";
                c[4].HeaderText = "Exchange Date";
                if (!HasArabic)
                    Grd_Currency.Columns[2].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
        }

        private void Grd_Currency_Click(object sender, EventArgs e)
        {
            if (Grd_Currency.Rows.Count > 0 && Grd_Currency.CurrentRow != null)
            {
                DataGridViewCellCollection c = Grd_Currency.CurrentRow.Cells;
                ID = c[0].Value.ToString();
                Txt_code.Text = c[0].Value.ToString();
                txt_english.Text = c[1].Value.ToString();
                txt_arab.Text = c[2].Value.ToString();
                txt_exchange.Text = c[3].Value.ToString();
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
           
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string query;

            if (valid())
            {
                try
                {
                   
                    if (ID == "")
                    {                        
                        query = "INSERT INTO GEN_CURRENCY (CODE,DESC_ENG,DESC_ARB,RATE,EXCHANGE_DATE)" +
                                      "VALUES ('" + Txt_code.Text + "','" + txt_english.Text + "','" + txt_arab.Text + "','" + txt_exchange.Text + "',@date )";
                        Dictionary<string, object> Parameters = new Dictionary<string, object>();
                        Parameters.Add("@date", DateTime.Now);
                        DbFunctions.InsertUpdate(query,Parameters);
                    }
                    else
                    {                        
                        query = "UPDATE GEN_CURRENCY  SET CODE = '" + Txt_code.Text + "',DESC_ENG = '" + txt_english.Text + "',DESC_ARB = '" + txt_arab.Text + "',RATE='" + txt_exchange.Text + "',EXCHANGE_DATE=@date  WHERE CODE = '" + ID + "'";
                        Dictionary<string, object> Parameters = new Dictionary<string, object>();
                        Parameters.Add("@date", DateTime.Now);
                        DbFunctions.InsertUpdate(query, Parameters);
                    }
                    
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
                
                bindtable();
                BtbClear.PerformClick();
            }
        }

        private void BtbClear_Click(object sender, EventArgs e)
        {
            Txt_code.Text = "";
            txt_exchange.Text = "";
            txt_english.Text = "";
            txt_arab.Text = "";
            ID = "";
        }

        private void Btndelete_Click(object sender, EventArgs e)
        {
            if (ID == "")
            {
                BtbClear.PerformClick();
            }
            else
            {
                if (MessageBox.Show("Are you sure?", "Record Deletion", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {                        
                        string query = "DELETE FROM GEN_CURRENCY WHERE CODE = '" + ID + "'";
                        DbFunctions.InsertUpdate(query);                        
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }                    

                    MessageBox.Show(this.Text + " Deleted!");
                    bindtable();
                    BtbClear.PerformClick();
                }

            }
        }

        private void Txt_code_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (sender is TextBox)
                {
                    string name = (sender as TextBox).Name;
                    switch (name)
                    {
                        case "Txt_code":
                            txt_exchange.Focus();
                            break;
                        case "txt_exchange":
                            if (HasArabic)
                            {
                                txt_arab.Focus();
                            }
                            else
                            {
                                txt_english.Focus();
                            }
                            break;

                        case "txt_english":
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
