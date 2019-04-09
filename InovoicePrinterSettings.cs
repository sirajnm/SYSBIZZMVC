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
    public partial class InovoicePrinterSettings : Form
    {
        //private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        //private SqlCommand cmd = new SqlCommand();
        //private SqlDataAdapter adapter = new SqlDataAdapter();
        private DataTable table = new DataTable();

        Initial mdi = (Initial)Application.OpenForms["Initial"];
        Login lg = (Login)Application.OpenForms["Login"];

        public InovoicePrinterSettings()
        {
            InitializeComponent();
            //cmd.Connection = conn;
            //adapter.SelectCommand = cmd;
        }


        public void GETSETTINGS()
        {
            try
            {
                string query = "select * from INV_PRINTPOSITION WHERE SETTINGS=1";
                //adapter.Fill(table);
                table = Model.DbFunctions.GetDataTable(query);
   

                TXTHEIGHT.Text = table.Rows[0]["PAGESIZEHEIGHT"].ToString();
                TXTWIDTH.Text = table.Rows[0]["PAGESIZEWIDTH"].ToString();
                TXTDEFAULTHEIGHT.Text = table.Rows[0]["DEFAULTHEIGHT"].ToString();
                CHKFIXEDHEIGHT.Checked = Convert.ToBoolean(table.Rows[0]["FIXEDHEIGHTBIT"]);
                CHKREPEAT.Checked = Convert.ToBoolean(table.Rows[0]["REPEAT"]);
                txt_ItemLength.Text = table.Rows[0]["ITEMLENGTH"].ToString();

                chkPageTotal.Checked = Convert.ToBoolean(table.Rows[0]["PAGETOTAL"]);
                
            }
            catch(Exception EE)
            {
                MessageBox.Show(EE.Message);
            }
        }

        public void GetPositions()
        {
            try
            {
                DataTable dt= new DataTable();
                string query = "SELECT        ITEM, XAXIS, YAXIS, PRFIX, ACTIVE, PAGESIZEHEIGHT, PAGESIZEWIDTH, DEFAULTHEIGHT, REPEAT, SETTINGS, FIXEDHEIGHTBIT FROM            INV_PRINTPOSITION WHERE        (SETTINGS = 0)";
                //adapter.Fill(dt);
                dt = Model.DbFunctions.GetDataTable(query);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dgItems.Rows.Add(dt.Rows[i]["ITEM"].ToString(), dt.Rows[i]["XAXIS"].ToString(), dt.Rows[i]["YAXIS"].ToString(), dt.Rows[i]["PRFIX"].ToString(), dt.Rows[i]["ACTIVE"]);
                }

            }
            catch(Exception EE)
            {
                MessageBox.Show(EE.Message);
            }
        }
        private void InovoicePrinterSettings_Load(object sender, EventArgs e)
        { 
            GetPositions();
            GETSETTINGS();
            dgItems.Columns[0].ReadOnly = true;
           
        }

        private void CHKFIXEDHEIGHT_CheckedChanged(object sender, EventArgs e)
        {
            if (CHKFIXEDHEIGHT.Checked)
            {
                TXTDEFAULTHEIGHT.Enabled = false;
            }
            else
            {
                TXTDEFAULTHEIGHT.Enabled = true;
            }
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            try
            {
                //conn.Open();
                string query="";
               query= "DELETE FROM INV_PRINTPOSITION WHERE SETTINGS=0;";
                   
                for (int i = 0; i < dgItems.Rows.Count-1; i++)
                {
                    DataGridViewCellCollection c = dgItems.Rows[i].Cells;
                    query += "INSERT INTO INV_PRINTPOSITION(ITEM,XAXIS,YAXIS,PRFIX,ACTIVE)";
                    query += "VALUES ('" + c["Feild"].Value + "','" + c["XAXIS"].Value + "','" + c["YAXIS"].Value + "','" + c["PREFIX"].Value + "','" + c["Active"].Value + "')";
                }
                   
                  
                   //cmd.CommandText = query;
                   //cmd.ExecuteNonQuery();
                Model.DbFunctions.InsertUpdate(query);

                   query = "update INV_PRINTPOSITION SET PAGESIZEHEIGHT='" + TXTHEIGHT.Text + "',PAGESIZEWIDTH='" + TXTWIDTH.Text + "',DEFAULTHEIGHT='" + TXTDEFAULTHEIGHT.Text + "',REPEAT='" + CHKFIXEDHEIGHT.Checked + "',FIXEDHEIGHTBIT='" + CHKREPEAT.Checked + "',ITEMLENGTH='" + txt_ItemLength.Text + "',PAGETOTAL='" + chkPageTotal.Checked + "' WHERE SETTINGS=1";
                   //cmd.ExecuteNonQuery();
                   //conn.Close();
                   Model.DbFunctions.InsertUpdate(query);
                   MessageBox.Show("Settings Updated Successfully");
            }
            catch(Exception ee)
            {
                MessageBox.Show(ee.Message);
                //conn.Close();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
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
    }
}
