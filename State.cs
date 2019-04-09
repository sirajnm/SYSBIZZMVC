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
    public partial class State : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        //SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        //SqlCommand cmd = new SqlCommand();
        clsState state=new clsState();
        public genEnum currentForm = genEnum.State;
        Initial mdi = (Initial)Application.OpenForms["Initial"];
        Login lg = (Login)Application.OpenForms["Login"];
        public int callfrom = 0;

        public State(genEnum form)
        {
            InitializeComponent();
            currentForm = form;
        }

        public State(genEnum form, int callfromto): this(form)
        {
            callfrom = callfromto;
        }
        public void clear()
        {
            txt_statecode.Text = "";
            txt_statename.Text = "";
            //conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
            //cmd = new SqlCommand();
            //cmd.Connection = conn;
            //conn.Open();
            //cmd.CommandType = CommandType.Text;
            //cmd.CommandText = "SELECT * FROM tblStates";
            //SqlDataAdapter dp = new SqlDataAdapter();
            //dp.SelectCommand = cmd;
            //DataTable dt = new DataTable();
            //dp.Fill(dt);
            dgv_state.DataSource = state.select();
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void State_Load(object sender, EventArgs e)
        {
            clear();
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (txt_statecode.Text != "" && txt_statename.Text != "")
            {
                state.Code = txt_statecode.Text;
                state.DescName = txt_statename.Text;
                if (General.StateExists(txt_statecode.Text) == true)
                {
                  
                    //cmd.Parameters.Clear();
                    //cmd.CommandText = "insert into tblStates (CODE,DESC_ENG)values(@code,@state)";
                    //cmd.Parameters.AddWithValue("@code", txt_statecode.Text);
                    //cmd.Parameters.AddWithValue("@state", txt_statename.Text);
                    //if(conn.State!=ConnectionState.Open)
                    //conn.Open();
                    //cmd.Connection = conn;
                    if (state.insert() == 1)
                    {
                        MessageBox.Show("New State Added", "SysBizz");
                        //conn.Close();
                        clear();
                    }
                    else
                    {
                        MessageBox.Show("Error Adding State", "SysBizz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //conn.Close();
                    }

                }
                else
                {
                    DialogResult dr = MessageBox.Show("State Code Allready Exist. Do you want to Update Name..?", "Sysbizz", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr == DialogResult.Yes)
                    {
                        //cmd.Parameters.Clear();
                        //cmd.CommandText = "Update tblStates Set DESC_ENG=@state where CODE=@Code";
                        //cmd.Parameters.AddWithValue("@code", txt_statecode.Text);
                        //cmd.Parameters.AddWithValue("@state", txt_statename.Text);
                        //if (conn.State != ConnectionState.Open)
                        //conn.Open();
                        //cmd.Connection = conn;
                        if (state.update() == 1)
                        {
                            MessageBox.Show("State Updated.", "SysBizz");
                           // conn.Close();
                            clear();
                        }
                        else
                        {
                            MessageBox.Show("Error in updation", "SysBizz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                           // conn.Close();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Code and Name should not be empty..!", "Sysbizz");
                if (txt_statecode.Text == "")
                {
                    txt_statecode.Focus();
                }
                else
                {
                    txt_statename.Focus();
                }
            }
        }

        private void dgv_state_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //txt_statecode.Text = dgv_state.SelectedRows[0].Cells[0].Value.ToString();
            //txt_statename.Text = dgv_state.SelectedRows[0].Cells[1].Value.ToString();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
           // this.Close();
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

        private void txt_statecode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_statename.Focus();
            }
        }

        private void txt_statename_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_Save.Focus();
            }
        }

        private void txt_statename_Leave(object sender, EventArgs e)
        {
            txt_statename.Text = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(txt_statename.Text.ToLower());
        }

        private void dgv_state_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txt_statecode.Text = dgv_state.SelectedRows[0].Cells[0].Value.ToString();
            txt_statename.Text = dgv_state.SelectedRows[0].Cells[1].Value.ToString();
        }
    }
}
