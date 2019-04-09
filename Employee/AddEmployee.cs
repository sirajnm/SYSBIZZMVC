using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;


namespace Sys_Sols_Inventory.Employee
{
  
    public partial class AddEmployee : Form
    {
        Class.Employee Emp = new Class.Employee();
        Class.Login Lg = new Class.Login();
        Login lg = (Login)Application.OpenForms["Login"];
        Initial mdi = (Initial)Application.OpenForms["Initial"];
        Class.Ledgers led = new Class.Ledgers();
        Class.CompanySetup Comset = new Class.CompanySetup();
        private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        SqlDataAdapter adapter = new SqlDataAdapter();
        private string UpdateLedgerId = "";
             int TogMove=0;
           int MalX, MalY;
           public int callfrom = 0;
           public genEnum currentForm = genEnum.State;
           public AddEmployee(genEnum form)
        {
            InitializeComponent();
            currentForm = form;
        }
           public AddEmployee(genEnum form, int callfromto): this(form)
        {
            callfrom = callfromto;
        }
        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_MouseHover(object sender, EventArgs e)
        {
            //label1.ForeColor = Color.Orange;
        }

     

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            //label1.ForeColor = Color.White;
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

        public void BindEmployees()
        {
            try
            {
                DataTable dt = new DataTable();

                dt = Emp.GetEmployees();
                dataGridView1.DataSource = dt;
             //   dataGridView1.Rows[0].Visible = false;
            }

            catch
            {
            }
        }
        private void AddEmployee_Load(object sender, EventArgs e)
        {
            BindEmployees();
            GetBranches();
            get_pos();
        }
        private void get_pos()
        {
            DataTable dt1 = new DataTable();
           // SqlCommand cmd1 = new SqlCommand();
            string cmd = "SELECT jobroleid,jobroletitle from EMP_JOBROLE";
            //cmd1.Connection = conn;
            //adapter.SelectCommand = cmd1;
            //adapter.Fill(dt1);
            //conn.Close();
            dt1 = Model.DbFunctions.GetDataTable(cmd);
            comDESIG.ValueMember = "jobroleid";
            comDESIG.DisplayMember = "jobroletitle";
            DataRow row = dt1.NewRow();
            dt1.Rows.InsertAt(row, 0);
            comDESIG.DataSource = dt1;


        }
        public void GetBranches()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = Comset.getbaranchs();
                BRANCH.DataSource = dt;
                BRANCH.DisplayMember = "DESC_ENG";
                BRANCH.ValueMember = "CODE";
            }
            catch(Exception ee)
            {
                MessageBox.Show(ee.Message + ", Branch binding error");
            }
        }

        public void InsertLogin()
        {
            try
            {

                DataTable dt = new DataTable();
                dt = Emp.GetMaxId();
               if(dt.Rows.Count>0)
                {
                    Lg.Empid1 = int.Parse( dt.Rows[0][0].ToString());
                    Lg.Usertype = "Employee";
                    Lg.Username = txtusername.Text;
                    Lg.Password = txtpassword.Text;
                    Lg.Branch = BRANCH.SelectedValue.ToString();
                    Lg.InsertLogin();
                }
            }
            catch
            {
            }
        }

        public bool valid()
        {
            if (txtfname.Text == "" || txtemal.Text == "")
            {
                MessageBox.Show("Enter Employee Name And Email");
                return false;
            }
            else
                return true;
        }
        public int GetMaxLedger()
        {
            return led.MaxLedGerid();
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            if (valid())
            {
                int LId = GetMaxLedger() + 1;
                try
                {
                    if (Empid == 0)
                    {
                        int flag;
                        flag = Emp.getusername(txtusername.Text);
                        if (flag == 1)
                        {
                            MessageBox.Show("user name already exist");
                            txtusername.Focus();
                            return;
                        }
                        else
                        {
                            Emp.empfname = txtfname.Text;
                            Emp.empmname = txtmname.Text;
                            Emp.emplname = txtlname.Text;
                            Emp.empaddress = txtaddress.Text;
                            Emp.dob = dtpdob.Value;
                            Emp.sex = drpsex.Text;
                            Emp.designation = txtdesign.Text;
                            Emp.email = txtemal.Text;
                            Emp.telephone = txttelephone.Text;
                            Emp.mobile = txtmobile.Text;
                            Emp.LedgerId = LId.ToString();
                            Emp.Emp_Branche = BRANCH.SelectedValue.ToString();
                            Emp.InsertEmployee();
                            Addledger();
                            InsertLogin();
                            MessageBox.Show("Employee added successfully");
                            BindEmployees();

                            Clearfileds();

                            txtfname.Focus();
                        }
                    }
                    else
                    {
                        Emp.empfname = txtfname.Text;
                        Emp.empmname = txtmname.Text;
                        Emp.emplname = txtlname.Text;
                        Emp.empaddress = txtaddress.Text;
                        Emp.dob = dtpdob.Value;
                        Emp.sex = drpsex.Text;
                        Emp.designation = txtdesign.Text;
                        Emp.email = txtemal.Text;
                        Emp.telephone = txttelephone.Text;
                        Emp.mobile = txtmobile.Text;
                        Emp.Emp_Branche = BRANCH.SelectedValue.ToString();
                        Emp.UpdateEmployee(Empid);
                        MessageBox.Show("Updated successfully");
                        BindEmployees();
                        Clearfileds();
                        txtfname.Focus();
                    }
                }
                catch
                {
                    MessageBox.Show("Exception Occured Please Try again after restart application");
                }
            }
        }
        public void Addledger()
        {
            led.LEDGERNAME = txtfname.Text;
            led.UNDER = "12";
            led.ADRESS = txtmname.Text + "," + txtlname.Text + "," + txtaddress.Text;
            led.TIN = "";
            led.CST = "";
            led.PIN = "";
            led.PHONE = txttelephone.Text;
            led.MOBILE = txtmobile.Text;
            led.EMAIL = txtemal.Text;
            led.CREDITPERIOD = "";
            led.CREDITAMOUNT = "";
            led.DISPLAYNAME = txtfname.Text;
            led.ISBUILTIN = "N";
            led.BANK = "";
            led.BANKBRANCH = "";
            led.IFCCODE = "";
            led.ACCOUNTNO = "";



            if (Empid == 0)
            {

                led.insertLedger();

            }
            else
            {

                led.LEDGERID = Convert.ToInt32(UpdateLedgerId);
                led.UpdateLedger();


            }

        }
        private void titlepanel_MouseDown(object sender, MouseEventArgs e)
        {
            TogMove = 1;
            MalX = e.X;
            MalY = e.Y;
        }

        private void titlepanel_MouseUp(object sender, MouseEventArgs e)
        {
            TogMove = 0;
        }

        private void titlepanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (TogMove == 1)
            {
                this.SetDesktopLocation(MousePosition.X - MalX, MousePosition.Y - MalY);
            }
        }

        int Empid = 0;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Empid=Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                if (Empid != 0)
                {
                    Emp.empid = Empid;
                    DataTable dt = new DataTable();
                    dt = Emp.GetEmployee(Empid);
                    if(dt.Rows.Count>0)
                    {
                        txtfname.Text = dt.Rows[0][1].ToString();
                        txtmname.Text = dt.Rows[0][2].ToString();
                        txtlname.Text = dt.Rows[0][3].ToString();
                        txtaddress.Text = dt.Rows[0][4].ToString();
                        drpsex.SelectedItem= dt.Rows[0][5].ToString();
                        dtpdob.Value =Convert.ToDateTime( dt.Rows[0][6].ToString());
                        txtdesign.Text = dt.Rows[0][7].ToString();
                        txtemal.Text = dt.Rows[0][8].ToString();
                        txttelephone.Text = dt.Rows[0][9].ToString();
                        txtmobile.Text = dt.Rows[0][10].ToString();
                        BRANCH.SelectedValue = dt.Rows[0]["Emp_Branch"].ToString();
                        comDESIG.SelectedValue = dt.Rows[0][7].ToString(); 

                    }
                }
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Clearfileds()
        {
            txtfname.Text = "";
            txtmname.Text = "";
            txtlname.Text = "";
            txtaddress.Text = "";
            drpsex.SelectedItem = "";
            Empid = 0;
            txtdesign.Text = "";
            txtemal.Text = "";
            txttelephone.Text = "";
            txtmobile.Text = "";
            txtusername.Text = "";
            txtpassword.Text = "";
            comDESIG.SelectedIndex = -1;
        }


        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (Empid != 0)
                {
                    if(DialogResult.Yes==MessageBox.Show("Are sure to delete ","Delete Employee Confirmation",MessageBoxButtons.YesNo))
                    {
                    Emp.DeleteEmployee(Empid);
                    Emp.Deletelogin(Empid);
                    BindEmployees();
                       
                    Clearfileds();
                    }
                }

                else
                {
                    MessageBox.Show("Select an Employee");
                }
            }
            catch
            {
            }
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            Clearfileds();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtfname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (sender is KryptonTextBox)
                {
                    string name = (sender as KryptonTextBox).Name;
                    switch (name)
                    {
                           
                        case "txtfname":
                            // DESC_ENG.Focus();
                            txtmname.Focus();
                            break;
                        case "txtmname":
                            txtlname.Focus();
                            break;
                        case "txtlname":
                            txtaddress.Focus();
                            break;
                        case "txtaddress":
                           dtpdob.Focus();
                            break;
                        case "txtemal":
                            txttelephone.Focus();
                            break;
                        case "txttelephone":
                            txtmobile.Focus();
                            break;
                        case "txtmobile":
                           txtdesign.Focus();
                            break;
                        case "txtdesign":
                            btnsave.Focus();
                            break;
                        
                        default:
                            break;
                    }
                }
                else if (sender is ComboBox)
                {
                    string name = (sender as ComboBox).Name;
                    switch (name)
                    {
                        case "drpsex":

                            txtemal.Focus();

                            break;
                       
                        default:
                            break;
                    }
                }
                else if (sender is KryptonDateTimePicker)
                {
                    string name = (sender as KryptonDateTimePicker).Name;
                    switch (name)
                    {
                        case "dtpdob":
                            drpsex.Focus();
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void comDESIG_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comDESIG.SelectedIndex > 0)
            {
                txtdesign.Text = comDESIG.SelectedValue.ToString();
            }
        }

      

       

        

        
    }
}
