using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.SqlServer.Server;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management;
using System.Data.SqlClient;
using System.IO;

namespace Sys_Sols_Inventory
{
    public partial class Login : Form
    {
        public String state;
        Class.Login log = new Class.Login();
        Class.EmployeePrivilage EmpPri = new Class.EmployeePrivilage();
        //private SqlCommand cmd = new SqlCommand();
        //private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        public static string memid;
        Class.EmpLog emplog = new Class.EmpLog();
        Class.CompanySetup cmp = new Class.CompanySetup();
        CompanyCreation.CompanyCreation comp = new CompanyCreation.CompanyCreation();
        public int LOCK = 0;
        public bool ACCOUNTS;
     //   Initial mdi = new Initial();
       public string EmpId;
       string s;
       public  string Theme;
       public string Branch;
       public string UpdateLog;
        int flag = 0,theme;

        public Login()
        {
            InitializeComponent();
        }
        public Login(int I)
        {
            InitializeComponent();
            LOCK = I;
            button2.Visible = false;

        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Alt | Keys.C))
            {
                AppLicense.Scripts sc = new AppLicense.Scripts();
                sc.ShowDialog();
                return true;
            }
            if (keyData == (Keys.Alt | Keys.L))
            {
                AppLicense.Validate VL = new AppLicense.Validate();
                VL.ShowDialog();
                //  return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        string con = "";
        private void Login_Load(object sender, EventArgs e)
        {
            pictureBox1.Visible = CompanyCreation.CompanyCreation.packageType;
            if (Properties.Settings.Default.backup != null)
            {
                if (Properties.Settings.Default.backup == "True")
                {
                    CB_BACK.Checked = true;
                }
                else
                {
                    CB_BACK.Checked = false;
                }
            }
            DateTime now = DateTime.Now;
            s = now.DayOfWeek.ToString();
            ACCOUNTS = Properties.Settings.Default.Account;
          
            con = comp.GetServer_Instance();
        }

        private void BtnQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private IEnumerable<ToolStripMenuItem> GetItems(ToolStripMenuItem item)
        {
            foreach (ToolStripMenuItem dropDownItem in item.DropDownItems)
            {
                if (dropDownItem.HasDropDownItems)
                {
                    foreach (ToolStripMenuItem subItem in GetItems(dropDownItem))
                        yield return subItem;
                }
                yield return dropDownItem;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void TxtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                Common.preventDingSound(e);
                btnLogin.PerformClick();
            }

        }

        private void TxtUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                Common.preventDingSound(e);
                TxtPassword.Focus();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (CB_BACK.Checked == true)
            {
                if (Properties.Settings.Default.backupLocation == "")
                    BackupClass.Backup1(Environment.CurrentDirectory + "\\sysbizz\\backup\\" + s);
                else
                    BackupClass.Backup1(Properties.Settings.Default.backupLocation+"\\"+s);
            }
            Application.Exit();
        }
     
        private void btnLogin_Click_1(object sender, EventArgs e)
        {
            if (LOCK == 1)
            {
                this.Hide();
            }
            else
            {
                if (TxtUserName.Text != "" && TxtPassword.Text != "")
                {
                    DataTable dt = new DataTable();
                    log.Username = TxtUserName.Text;
                    log.Password = TxtPassword.Text;
                    dt = log.GetUsernamePassword();
                    if (dt.Rows.Count > 0)
                    {
                        CompanyCreation.clsDbUpgrade.upgradeDatabase();
                        EmpId = dt.Rows[0][1].ToString();
                        memid = EmpId;
                        Theme = dt.Rows[0][6].ToString();
                        Branch = dt.Rows[0]["Branch"].ToString();
                        if (Branch == null || Branch == "")
                        {
                            Branch = cmp.ReadBranch();
                        }
                        emplog.Emp_Id = EmpId;
                        emplog.Branch = Branch;
                        emplog.InsertEmplog();
                        UpdateLog = emplog.GetUpdatelogId();
                        flag = 1;
                        state = cmp.getCurrentState();
                    }
                    else
                    {
                        MessageBox.Show("invalid Username or Passowrd", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }
                else
                {
                    MessageBox.Show("Enter Username and Passowrd", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }

                if (flag == 1)
                {
                    flag = 0;
                    if (Theme == "1")
                    {
                        //Initial m = (Initial)Application.OpenForms["Initial"];
                        //if (m != null)
                        //    m.Close();
                        FormCollection fc = Application.OpenForms;
                        int f = fc.Count;
                        for (int i = 0; i < f; i++)
                        {
                            foreach (Form frm in fc)
                            {

                                if (frm.Name == "Initial")
                                {
                                    frm.Close();
                                    break;
                                }
                            }
                        }
                        int f2 = fc.Count;
                        Initial mdi = new Initial();


                        DataTable dt1 = new DataTable();
                        EmpPri.EmpId = int.Parse(EmpId);
                        dt1 = EmpPri.View_Modules_In_employeeprivilage();
                        if (dt1.Rows.Count > 0)
                        {
                            List<ToolStripMenuItem> allItems = new List<ToolStripMenuItem>();
                            foreach (ToolStripMenuItem toolItem in mdi.menuMain.Items)
                            {
                                allItems.Add(toolItem);
                                allItems.AddRange(GetItems(toolItem));
                            }
                            foreach (var item in allItems)
                            {
                                foreach (DataRow row in dt1.Rows)
                                {
                                    if (item.Text == row[0].ToString())
                                    {
                                        item.Enabled = true;
                                        break;
                                    }
                                    else
                                    {
                                        if (item.Text != "Logout")
                                        {
                                            item.Enabled = false;
                                        }
                                    }

                                    if (item.Text == "Modern" || item.Text == "Classic")
                                    {
                                        item.Enabled = true;
                                    }
                                }
                            }

                        }
                        else
                        {
                            List<ToolStripMenuItem> allItems = new List<ToolStripMenuItem>();
                            foreach (ToolStripMenuItem toolItem in mdi.menuMain.Items)
                            {
                                allItems.Add(toolItem);
                                allItems.AddRange(GetItems(toolItem));
                            }
                            foreach (var item in allItems)
                            {
                                if (item.Text != "Logout")
                                {
                                    item.Enabled = false;
                                }
                            }
                        }

                        //if (ACCOUNTS)
                        //{
                        //    mdi.toolStripAccounts.Enabled = true;
                        //    mdi.accountGroupToolStripMenuItem.Enabled = true;
                        //    mdi.ledgersToolStripMenuItem.Enabled = true;
                        //}
                        //else
                        //{
                        //    mdi.toolStripAccounts.Enabled = false;
                        //}
                        TxtPassword.Text = "";
                        TxtUserName.Text = "";
                        mdi.Show();
                        this.Hide();
                    }
                    else
                    {
                        Main mdi = new Main();
                        DataTable dt1 = new DataTable();
                        EmpPri.EmpId = int.Parse(EmpId);
                        dt1 = EmpPri.View_Modules_In_employeeprivilage();
                        if (dt1.Rows.Count > 0)
                        {
                            List<ToolStripMenuItem> allItems = new List<ToolStripMenuItem>();
                            foreach (ToolStripMenuItem toolItem in mdi.menuMain.Items)
                            {
                                allItems.Add(toolItem);
                                allItems.AddRange(GetItems(toolItem));
                            }
                            foreach (var item in allItems)
                            {


                                foreach (DataRow row in dt1.Rows)
                                {
                                    if (item.Text == row[0].ToString())
                                    {
                                        item.Enabled = true;
                                        break;
                                    }
                                    else
                                    {
                                        if (item.Text != "Logout")
                                        {
                                            item.Enabled = false;
                                        }
                                    }

                                    if (item.Text == "Modern" || item.Text == "Classic")
                                    {
                                        item.Enabled = true;
                                    }
                                }
                            }

                        }
                        else
                        {
                            List<ToolStripMenuItem> allItems = new List<ToolStripMenuItem>();
                            foreach (ToolStripMenuItem toolItem in mdi.menuMain.Items)
                            {
                                allItems.Add(toolItem);
                                allItems.AddRange(GetItems(toolItem));
                            }
                            foreach (var item in allItems)
                            {
                                if (item.Text != "Logout")
                                {
                                    item.Enabled = false;
                                }
                            }
                        }
                        //if (ACCOUNTS)
                        //{
                        //    mdi.toolStripAccounts.Enabled = true;
                        //}
                        //else
                        //{
                        //    mdi.toolStripAccounts.Enabled = false;
                        //}
                        TxtPassword.Text = "";
                        TxtUserName.Text = "";
                        mdi.Show();
                        this.Hide();
                    }
                }
            }
        }

        private void CB_BACK_CheckedChanged(object sender, EventArgs e)
        {
            if (CB_BACK.Checked == true)
                Properties.Settings.Default.backup = "True";
            else
                Properties.Settings.Default.backup = "False";

            Properties.Settings.Default.Save();
        }

        private void pictureBox1_Click_2(object sender, EventArgs e)
        {
            if (con != "" || con != null)
            {
                SqlConnectionStringBuilder cr = new SqlConnectionStringBuilder(con);
                SqlConnection conn = new SqlConnection(con);
                try
                {
                    conn.Open();

                    cr.InitialCatalog = "master";
                    conn.Close();
                }
                catch
                {
                }
                SqlDataReader r = CompanyCreation.CompanyCreation.getCompanies(cr.ToString());
                CompanyCreation.frmCompanies comp1 = new CompanyCreation.frmCompanies(cr.ToString(),"Login");

                int count = 0;
                while (r.Read())
                {
                    //  int a=(r["Location"].ToString().LastIndexOf('.'));
                    Button btn = new Button();
                    string b = r["Location"].ToString().Substring(0, (r["Location"].ToString().LastIndexOf('.'))) + "_log.ldf";
                    if (File.Exists(r["Location"].ToString()) && File.Exists(r["Location"].ToString().Substring(0, (r["Location"].ToString().LastIndexOf('.'))) + "_log.ldf"))
                    {
                        btn.Text = r["DBName"].ToString();
                        btn.Name = r["DBName"].ToString();
                        btn.Size = new Size(100, 80);
                        btn.FlatStyle = FlatStyle.Flat;
                        btn.BackColor = Color.MediumVioletRed;
                        btn.ForeColor = Color.White;
                        btn.Click += comp1.Company_Click;
                        btn.Cursor = Cursors.Hand;
                        comp1.pnlLoadCompanies.Controls.Add(btn);
                        count += 1;
                        // btn.Text = r["DBName"].ToString();
                    }

                }
                CompanyCreation.CompanyCreation.conn.Close();
                if (count >0)
                {
                    comp1.Show();
                }
            }
           
        }
    }
}
