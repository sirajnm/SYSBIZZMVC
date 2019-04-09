using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
//using System.Collections.Generic;

namespace Sys_Sols_Inventory
{
    public partial class FrmAdmin : Form
    {
        //string jbId;
        //private SqlConnection conn = new SqlConnection();
        //private SqlCommand cmd = new SqlCommand();
        ArrayList al = new ArrayList();
        DataTable mdId = new DataTable();
        int flag1 = 0;
        //string[] modId = new string[] { };
        Class.Employee Emp = new Class.Employee();
        Class.JobRole JB = new Class.JobRole();
        Class.Privilage PRI = new Class.Privilage();
        Class.Login Lg = new Class.Login();
        Class.Ledgers led = new Class.Ledgers();
        Class.CompanySetup Comset = new Class.CompanySetup();
        Class.EmployeePrivilage EmpPriv = new Class.EmployeePrivilage();
        BindingSource bs = new BindingSource();
        Initial mdi = new Initial();

        private string UpdateLedgerId = "";
           //  int TogMove=0;
           //int MalX, MalY;
           int Empid = 0;
           int setPriv;
           
        public FrmAdmin()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (tbcAdmin.SelectedIndex==0)
            {
                if (btnSave.Text == "Save")
                {                   
                    InsertEmp();
                    if (chkEmpPriv.Checked)
                    {
                        DataTable dt = new DataTable();
                        dt = Emp.GetMaxEmpId();
                        for (int i = 0; i < mdId.Rows.Count; i++)
                        {
                            EmpPriv.EmpId = int.Parse(dt.Rows[0][0].ToString());
                            EmpPriv.modId = mdId.Rows[i][0].ToString();
                            EmpPriv.InsertPrivilage();
                        }
                    }
                    BindEmployees();
                    Clearfileds();
                }
                else if (btnSave.Text == "Update")
                {
                    Emp.empfname = txtfname.Text;
                    Emp.empmname = txtmname.Text;
                    Emp.emplname = txtlname.Text;
                    Emp.empaddress = txtaddress.Text;
                    Emp.dob = dtpdob.Value;
                    if (rdbMale.Checked)
                        Emp.sex = rdbMale.Text;
                    else

                        Emp.sex = rdbFemale.Text;
                    Emp.designation = txtdesign.Text;
                    Emp.email = txtemal.Text;
                    Emp.telephone = txttelephone.Text;
                    Emp.mobile = txtmobile.Text;
                    Emp.Emp_Branche = BRANCH.Text;
                    if (lblJbId.Text == "JbId")
                        Emp.jbId = "";
                    else
                        Emp.jbId = lblJbId.Text;
                    //if (chkEmpPriv.Checked)
                    //    Emp.setPriv = 1;
                    //else
                    //    Emp.setPriv = 0;                  
                    //Emp.jbId = cmbJbRole.SelectedText;
                    Emp.empid = int.Parse(dgvEmpDisplay.CurrentRow.Cells[2].Value.ToString());
                    Emp.UpdateEmployee();
                   
                    Emp.username = txtusername.Text;
                    Emp.password = txtConfirmPassword.Text;
                    Emp.UpdateLogin();
                    MessageBox.Show("Updated successfully");
                    BindEmployees();
                    Clearfileds();
                    txtfname.Focus();
                }

            }
            
            else 
            {
                //Save job privilege Settings
                if (rdbJobRole.Checked == true)
                {
                    if (txtJobRle.Text != string.Empty)
                    {
                        if (btnSave.Text == "Save")
                        {
                            JB.JobRoleTitle = txtJobRle.Text;
                            JB.InsertJobRole();
                            MessageBox.Show("Job Role Added");
                            DisplayJbinGrid();
                            txtJobRle.Text = "";
                            DataTable dt = new DataTable();
                            dt = JB.GetMaxJobId();
                            for (int i = 0; i < dgvPrivileges.Rows.Count; i++)
                            {
                                if (dt.Rows[0][0].ToString() == dgvPrivileges.Rows[i].Cells[2].Value.ToString())
                                {
                                    dgvPrivileges.Rows[i].Selected = true;
                                    dgvPrivileges.Rows[i].Cells[0].Value = true;
                                    dgvPrivileges.Rows[i].DefaultCellStyle.SelectionBackColor = Color.Blue;
                                }
                            }
                            TreeNodeCollection nodes = this.trvPrivilege.Nodes;
                            foreach (TreeNode n in nodes)
                            {
                                GetNodeRecursiveJb(n);
                            }
                            DisplayJbinGrid();
                            txtJobRle.Text = "";
                            foreach (TreeNode node in trvPrivilege.Nodes)
                            {
                                node.Checked = false;
                            }
                        }
                        else if (btnSave.Text == "Update")
                        {
                            JB.JobRoleid = int.Parse(lblJbId.Text);
                            JB.JobRoleTitle = txtJobRle.Text;
                            JB.UpdateJobRole();
                            DisplayJbinGrid();
                            for (int i = 0; i < dgvPrivileges.Rows.Count; i++)
                            {
                                if (lblJbId.Text == dgvPrivileges.Rows[i].Cells[2].Value.ToString())
                                {
                                    dgvPrivileges.Rows[i].Selected = true;
                                    dgvPrivileges.Rows[i].Cells[0].Value = true;
                                    dgvPrivileges.Rows[i].DefaultCellStyle.SelectionBackColor = Color.Blue;
                                }
                            }
                            PRI.Job_Id = int.Parse(lblJbId.Text);
                            PRI.deleteAlljobright();
                            TreeNodeCollection nodes = this.trvPrivilege.Nodes;
                            foreach (TreeNode n in nodes)
                            {
                                GetNodeRecursiveJb(n);
                            }
                            DisplayJbinGrid();
                            txtJobRle.Text = "";
                            foreach (TreeNode node in trvPrivilege.Nodes)
                            {
                                node.Checked = false;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Fill required field");
                    }

                }
                else
                {
                //Save Emp Privilege settings
                    if (btnSave.Text == "Save")
                    {
                        TreeNodeCollection nodes = this.trvPrivilege.Nodes;
                        foreach (TreeNode n in nodes)
                        {
                            GetNodeRecursiveEmp(n);
                        }
                    }
                    else if (btnSave.Text == "Update")
                    {
                        foreach (DataGridViewRow rows in dgvPrivileges.Rows)
                        {
                            if (Convert.ToBoolean(((DataGridViewCheckBoxCell)rows.Cells[0]).Value))
                            {
                                EmpPriv.EmpId = int.Parse(rows.Cells[2].Value.ToString());
                            }
                        }
                        EmpPriv.DeleteEmpPriv();
                        TreeNodeCollection nodes = this.trvPrivilege.Nodes;
                        foreach (TreeNode n in nodes)
                        {
                            GetNodeRecursiveEmp(n);
                        }
                    }
                }
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

             DataTable dt = new DataTable();
             dt = led.MaxLedId();
             if (dt.Rows.Count > 0)
             {
                 return Convert.ToInt32(dt.Rows[0][0]);
             }
             else
             {
                 return 0;
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
         public void InsertLogin()
         {
             try
             {

                 DataTable dt = new DataTable();
                 dt = Emp.GetMaxId();
                 if (dt.Rows.Count > 0)
                 {
                     Lg.Empid1 = int.Parse(dt.Rows[0][0].ToString());
                     Lg.Usertype = "Employee";
                     Lg.Username = txtusername.Text;
                     if (txtConfirmPassword.Text == txtpassword.Text)
                         Lg.Password = txtpassword.Text;
                     else
                         lblPass.Visible = true;
                     Lg.Branch = BRANCH.SelectedValue.ToString();
                     Lg.InsertLogin();
                 }
             }
             catch
             {
             }
         }
         public void BindEmployees()
         {
             try
             {
                 DataTable dt1 = new DataTable();
                 dt1 = EmpPriv.View_Distinct_EmpId();
                 DataTable dt = new DataTable();

                 dt = Emp.GetEmployees();                 
                 dgvEmpDisplay.DataSource = dt;
                 //dgvPrivileges.DataSource = dt;
                 foreach (DataGridViewRow rows in dgvEmpDisplay.Rows)
                 {
                     foreach (DataRow row in dt1.Rows)
                     {
                         if (rows.Cells[2].Value.ToString() == row[0].ToString())
                         {
                             rows.Cells[1].Value = "Granded";
                             break;
                         }
                         else
                         {
                             rows.Cells[1].Value = "Not Granded";
                         }
                     }
                }

             }

             catch
             {
             }
         }
         public void Clearfileds()
         {
             txtfname.Text = "";
             txtmname.Text = "";
             txtlname.Text = "";
             txtaddress.Text = "";
             rdbMale.Checked = false;
             rdbFemale.Checked = false;
             Empid = 0;
             txtdesign.Text = "";
             txtemal.Text = "";
             txttelephone.Text = "";
             txtmobile.Text = "";
             txtusername.Text = "";
             txtpassword.Text = "";
             txtConfirmPassword.Text = "";
             BRANCH.SelectedIndex = 0;
             cmbJbRole.SelectedIndex = 0;
             chkSelectAll.Checked = false;
             foreach (TreeNode node in trvPrivilege.Nodes)
             {
                 node.Checked = false;
             }
        
         }
         
         private void btnQuit_Click(object sender, EventArgs e)
         {
             this.Close();
         }

         private void btnDelete_Click(object sender, EventArgs e)
         {
             if (tbcAdmin.SelectedIndex == 0)
             {
                 dgvEmpDisplay.Rows[0].Selected = false;
                 try
                 {
                     foreach (DataGridViewRow row in dgvEmpDisplay.Rows)
                     {
                         if (Convert.ToBoolean(((DataGridViewCheckBoxCell)row.Cells[0]).Value))
                         {
                             row.Selected = true;
                         }
                     }

                     if (dgvEmpDisplay.SelectedRows.Count > 0)
                     {
                         if (DialogResult.Yes == MessageBox.Show("Are sure to delete ", "Delete Employee Confirmation", MessageBoxButtons.YesNo))
                         {
                             foreach (DataGridViewRow row in dgvEmpDisplay.Rows)
                             {
                                 if (Convert.ToBoolean(((DataGridViewCheckBoxCell)row.Cells[0]).Value))
                                 {
                                     Empid = int.Parse(row.Cells[2].Value.ToString());
                                     Emp.DeleteEmployee(Empid);
                                     Emp.Deletelogin(Empid);
                                     //Clearfileds();
                                 }
                             }
                             BindEmployees();
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
             
             else
             {
                 if (rdbJobRole.Checked)
                 {
                     dgvPrivileges.Rows[0].Selected = false;
                     foreach (DataGridViewRow row in dgvPrivileges.Rows)
                     {
                         if (Convert.ToBoolean(((DataGridViewCheckBoxCell)row.Cells[0]).Value))
                         {
                             row.Selected = true;
                         }
                     }
                     if (dgvPrivileges.SelectedRows.Count > 0)
                     {
                         if (DialogResult.Yes == MessageBox.Show("Are sure to delete ", "Delete Employee Confirmation", MessageBoxButtons.YesNo))
                         {
                             foreach (DataGridViewRow row in dgvPrivileges.Rows)
                             {
                                 if (Convert.ToBoolean(((DataGridViewCheckBoxCell)row.Cells[0]).Value))
                                 {
                                     JB.JobRoleid = int.Parse(row.Cells[2].Value.ToString());
                                     JB.DeleteJobRole();
                                     PRI.Job_Id = int.Parse(row.Cells[2].Value.ToString());
                                     PRI.deleteprivilage();
                                     Emp.jbId = row.Cells[2].Value.ToString();
                                     Emp.UpdateJobId();
                                     //Clearfileds();
                                 }
                             }
                             DisplayJbinGrid();
                         }
                     }

                     else
                     {
                         MessageBox.Show("Select a job role");
                     }
                 }
                 //Delete Privilege
             }
         }

         private void kryptonButton3_Click(object sender, EventArgs e)
         {
             if(tbcAdmin.TabIndex==0)
             Clearfileds();
         }
         public void GetBranches()
         {
             try
             {
                 DataTable dt = new DataTable();
                 dt = Comset.getbaranchs();
                 DataRow row = dt.NewRow();
                 row[1] = "---------Select---------";
                 dt.Rows.InsertAt(row, 0);
                 BRANCH.DataSource = dt;
                 BRANCH.DisplayMember = "DESC_ENG";
                 BRANCH.ValueMember = "CODE";
             }
             catch (Exception ee)
             {
                 MessageBox.Show(ee.Message + ", Branch binding error");
             }
         }
         private void DisplayJobinCombo()
         {
             DataTable dt = new DataTable();
             dt = JB.bindJobroleName();
             //    string demo = balJobRole.jobRoleName;
             // cmbJbRole.DisplayMember = balJobRole.jobRoleName;

             DataRow row = dt.NewRow();
             row[0] = "---------Select---------";
             dt.Rows.InsertAt(row, 0);
             cmbJbRole.DataSource = dt;
             cmbJbRole.DisplayMember = "JobRoleTitle";
             cmbJobRole.DataSource = dt;
             cmbJobRole.DisplayMember = "JobRoleTitle";
             /*if (dt.Rows.Count == 1)
             {
                 cmbJbRole.Enabled = false;
                 cmbJbRole.BackColor = Color.Gray;
             }
             else
             {
                 cmbJbRole.Enabled = true;
                 cmbJbRole.BackColor = Color.White;
             }*/
         }
         private void FrmAdmin_Load(object sender, EventArgs e)
         {
            
             GetBranches();
             DisplayJobinCombo();
             BindEmployees();
             
             //DisplayJbinGrid();
             //DisplayJobinGrid();
             
             rdbEmployee.Checked = true;
             BindEmployee();
             try
             {
                 foreach (ToolStripMenuItem tsmi in mdi.menuMain.Items)
                 {
                     if (tsmi.Text != "Logout")
                     {
                         TreeNode tn = new TreeNode(tsmi.Text);
                         getChildNodes(tsmi, tn);
                         trvPrivilege.Nodes.Add(tn);
                     }

                 }
             }
             catch { }
             trvPrivilege.ExpandAll();
         }
         private void CheckAllChildNodes(TreeNode treeNode, bool nodeChecked)
         {
             foreach (TreeNode node in treeNode.Nodes)
             {
                 node.Checked = nodeChecked;
                 if (node.Nodes.Count > 0)
                 {
                     // If the current node has child nodes, call the CheckAllChildsNodes method recursively.
                     this.CheckAllChildNodes(node, nodeChecked);
                 }
             }
         }

         private void getChildNodes(ToolStripDropDownItem mi, TreeNode tn)
         {
             foreach (object item in mi.DropDownItems)
             {
                 if (item.GetType() == typeof(ToolStripSeparator))
                 {
                     continue;
                 }
                 TreeNode node = new TreeNode(((ToolStripDropDownItem)item).Text);
                 tn.Nodes.Add(node);
                 getChildNodes(((ToolStripDropDownItem)item), node);
             }
         }

         private void tbcAdmin_SelectedIndexChanged(object sender, EventArgs e)
         {
             BindEmployee();
             rdbEmployee.Checked = true;
             btnSave.Text = "Save";
         }

         private void tbpEmployee_Click(object sender, EventArgs e)
         {
             
         }

         private void tbpJobRole_Click(object sender, EventArgs e)
         {
             //chkJbPriv.Checked = false;  
         }

         private void tbpPrivileges_Click(object sender, EventArgs e)
         {
            
         }

         private void cmbJbRole_SelectedIndexChanged(object sender, EventArgs e)
         {
             if (cmbJbRole.SelectedIndex != 0)
             {
                 DataTable dt = new DataTable();
                 Emp.JbTitle = cmbJbRole.Text;
                 dt = Emp.GetJobId();
                 lblJbId.Text = dt.Rows[0][0].ToString();
                 chkEmpPriv.Enabled = true;
             }
             //else
             //{
             //    chkEmpPriv.Enabled = false;
             //}
             
         }

         private void chkSetPrivileges_CheckedChanged(object sender, EventArgs e)
         {

         }

         private void btnNew_Click(object sender, EventArgs e)
         {
             TabPage t = tbcAdmin.TabPages[1];
             tbcAdmin.SelectedTab = t; //go to tab
         }
         

         private void tbcAdmin_Click(object sender, EventArgs e)
         {

         }

         private void rdbJobRole_CheckedChanged(object sender, EventArgs e)
         {
             lblJbRole.Visible = false;
             cmbJobRole.Visible = false;
             lblJobRle.Visible = true;
             txtJobRle.Visible = true;
             DisplayJbinGrid();
         }

         private void rdbEmployee_CheckedChanged(object sender, EventArgs e)
         {
             if (rdbEmployee.Checked)
             {
                 lblJbRole.Visible = true;
                 cmbJobRole.Visible = true;
                 lblJobRle.Visible = false;
                 txtJobRle.Visible = false;
                 BindEmployee();
             }
         }
         
         private void DisplayJbinGrid()
         {
             DataTable dt1 = new DataTable();
             dt1 = JB.View_distinct_JobId();
             DataTable dt = new DataTable();
             dt = JB.BindJobRole();
             bs.DataSource = dt;
             dgvPrivileges.DataSource = bs;
             foreach (DataGridViewRow rows in dgvPrivileges.Rows)
             {
                 foreach (DataRow row in dt1.Rows)
                 {
                     if (rows.Cells[2].Value.ToString() == row[0].ToString())
                     {
                         rows.Cells[1].Value = "Granded";
                         break; 
                     }
                     else
                     {
                         rows.Cells[1].Value = "Not Granded";
                     }
                 }
             }
             //dgvEmpDisplay.DataSource = bs;
             //dgvPrivileges.Columns[2].Visible = false;
             
             dgvPrivileges.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
         }
         public void BindEmployee()
         {
             try
             {
                 try
                 {
                     DataTable dt1 = new DataTable();
                     dt1 = EmpPriv.View_Distinct_EmpId();
                     DataTable dt = new DataTable();

                     dt = Emp.GetEmployees();
                     //dgvEmpDisplay.DataSource = dt;
                     dgvPrivileges.DataSource = dt;

                     foreach (DataGridViewRow rows in dgvPrivileges.Rows)
                     {
                         foreach (DataRow row in dt1.Rows)
                         {
                             if (rows.Cells[2].Value.ToString() == row[0].ToString())
                             {
                                 rows.Cells[1].Value = "Granded";
                                 break;
                             }
                             else
                             {
                                 rows.Cells[1].Value = "Not Granded";
                             }
                         }
                     }
                     //   dataGridView1.Rows[0].Visible = false;
                 }

                 catch
                 {
                 }
                 //   dataGridView1.Rows[0].Visible = false;
                 dgvPrivileges.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
             }

             catch
             {
             }
         }

         private void trvPrivilege_AfterCheck(object sender, TreeViewEventArgs e)
         {           
             if (flag1 == 0)
             {
                 trvPrivilege.AfterCheck -= trvPrivilege_AfterCheck;

                 TreeNode node = e.Node;
                 if (node.Nodes != null)
                     node.Nodes.Cast<TreeNode>().ToList().ForEach(v => v.Checked = node.Checked);

                 node = e.Node.Parent;
                 while (node != null)
                 {
                     bool set = e.Node.Checked
                                ? node.Nodes.Cast<TreeNode>()
                                 .Any(v => v.Checked == e.Node.Checked)
                                : node.Nodes.Cast<TreeNode>()
                                 .All(v => v.Checked == e.Node.Checked);
                     if (set)
                     {
                         node.Checked = e.Node.Checked;
                         node = node.Parent;
                     }
                     else
                         node = null;
                 }
                 trvPrivilege.AfterCheck += trvPrivilege_AfterCheck;

                 if (e.Action != TreeViewAction.Unknown)
                 {
                     if (e.Node.Nodes.Count > 0)
                     {
                         /* Calls the CheckAllChildNodes method, passing in the current 
                         Checked value of the TreeNode whose checked state changed. */
                         this.CheckAllChildNodes(e.Node, e.Node.Checked);
                     }
                 }
             }
         }

         private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
         {
             if (chkSelectAll.Checked)
             {
                 CheckUncheckTreeNode(trvPrivilege.Nodes, true);
             }
             else
             {
                 CheckUncheckTreeNode(trvPrivilege.Nodes, false);
             }
         }
         private void CheckUncheckTreeNode(TreeNodeCollection trNodeCollection, bool isCheck)
         {
             foreach (TreeNode trNode in trNodeCollection)
             {
                 trNode.Checked = isCheck;
                 if (trNode.Nodes.Count > 0)
                     CheckUncheckTreeNode(trNode.Nodes, isCheck);
             }
         }

         private void txtEmployeeSearch_TextChanged(object sender, EventArgs e)
         {            
             DataTable dt = new DataTable();
             dt = Emp.GetEmployees();
             dgvEmpDisplay.DataSource = dt;
             dt.DefaultView.RowFilter = string.Format("LedgerId like '%{0}%' OR Name like '%{0}%'", txtEmployeeSearch.Text.Trim().Replace("'", "''"));
             dgvEmpDisplay.Refresh();
         }

        
         private void txtSearch_TextChanged(object sender, EventArgs e)
         {
             if (rdbEmployee.Checked == true)
             {
                 DataTable dt = new DataTable();

                 dt = Emp.GetEmployees();
                 dgvPrivileges.DataSource = dt;
                 dt.DefaultView.RowFilter = string.Format("LedgerId like '%{0}%' OR Name like '%{0}%'", txtSearch.Text.Trim().Replace("'", "''"));
                 dgvPrivileges.Refresh();
             }
             else
             {
                 DataTable dt = new DataTable();
                 dt = JB.BindJobRole();
                 bs.DataSource = dt;
                 dgvPrivileges.DataSource = bs;
                 //bs.Filter = string.Format("JobRoleTitle like '%{0}%'", txtJobRoleSearch.Text.Trim().Replace("'", "''"));
                 bs.Filter = string.Format("JobRoleTitle like  '" + txtSearch.Text + "%'");
                 dgvPrivileges.Refresh();
             }
         }

         

         private void tbcAdmin_SelectedIndexChanged_1(object sender, EventArgs e)
         {

         }

         private void dgvEmpDisplay_CellClick(object sender, DataGridViewCellEventArgs e)
         {
             DataGridViewRow row = dgvEmpDisplay.Rows[e.RowIndex];
             row.DefaultCellStyle.SelectionBackColor = Color.Blue;
             DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
             if (chk.Value == chk.TrueValue)
             {
                 chk.Value = chk.FalseValue;
                 row.Selected = false;
                 row.DefaultCellStyle.BackColor = Color.White;
                 dgvEmpDisplay.Refresh();

             }
             else
             {
                 chk.Value = chk.TrueValue;
                 row.Selected = true;
                 row.DefaultCellStyle.BackColor = Color.Blue;
                 dgvEmpDisplay.Refresh();


             }
             foreach (DataGridViewRow row1 in dgvEmpDisplay.Rows)
             {
                 if (Convert.ToBoolean(((DataGridViewCheckBoxCell)row1.Cells[0]).Value))
                 {
                     row1.Selected = true;
                 }
                 else
                 {
                     row1.Selected = false;
                 }
             }
             if (dgvEmpDisplay.SelectedRows.Count == 1)
             {
                 btnSave.Text = "Update";
                 int empId=Convert.ToInt32(dgvEmpDisplay.CurrentRow.Cells[2].Value.ToString());
                 DataTable dt = Emp.GetEmployee(empId);
                 foreach (DataRow row2 in dt.Rows)
                 {
                     if (Convert.ToBoolean(((DataGridViewCheckBoxCell)dgvEmpDisplay.CurrentRow.Cells[0]).Value))
                     {
                         txtfname.Text = row2[1].ToString();
                         txtmname.Text = row2[2].ToString();
                         txtlname.Text = row2[3].ToString();
                         txtaddress.Text = row2[4].ToString();
                         dtpdob.Text = row2[6].ToString();
                         if (row2[5].ToString() == "Male")
                             rdbMale.Checked = true;
                         else
                             rdbFemale.Checked = true;
                        
                         txtemal.Text = row2[8].ToString();
                         txtdesign.Text = row2[7].ToString();
                         txttelephone.Text = row2[9].ToString();
                         txtmobile.Text = row2[10].ToString();
                         BRANCH.Text = row2[12].ToString();
                         cmbJbRole.Text = row2[15].ToString();
                         txtusername.Text = row2[16].ToString();
                         txtpassword.Text = row2[17].ToString();
                         txtConfirmPassword.Text = row2[17].ToString();
                     }
                 }

             }
             else
             {
                 btnSave.Text = "Save";
                 Clearfileds();
             }
         }

         private void dgvPrivileges_CellClick(object sender, DataGridViewCellEventArgs e)
         {
             flag1 = 0;
             DataGridViewRow row = dgvPrivileges.Rows[e.RowIndex];
             //DataGridViewRow row = dgvPrivileges.CurrentRow;
             row.DefaultCellStyle.SelectionBackColor = Color.Blue;
             DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
             if (chk.Value == chk.TrueValue)
             {
                 chk.Value = chk.FalseValue;
                 row.Selected = false;
                 row.DefaultCellStyle.BackColor = Color.White;
                 dgvPrivileges.Refresh();

             }
             else
             {
                 chk.Value = chk.TrueValue;
                 row.Selected = true;
                 row.DefaultCellStyle.BackColor = Color.Blue;
                 dgvPrivileges.Refresh();
             }
             foreach (DataGridViewRow row1 in dgvPrivileges.Rows)
             {
                 if (Convert.ToBoolean(((DataGridViewCheckBoxCell)row1.Cells[0]).Value))
                 {
                     row1.Selected = true;
                 }
                 else
                 {
                     row1.Selected = false;
                 }
             }
             if (dgvPrivileges.SelectedRows.Count == 1)
             {
                 btnSave.Text = "Update";
                 if (rdbJobRole.Checked)
                 {
                     foreach (DataGridViewRow row2 in dgvPrivileges.Rows)
                     {
                         if (Convert.ToBoolean(((DataGridViewCheckBoxCell)row2.Cells[0]).Value))
                         {
                             lblJbId.Text = row2.Cells[2].Value.ToString();
                             txtJobRle.Text = row2.Cells[3].Value.ToString();
                         }
                     }
                     foreach (var node in trvPrivilege.Nodes.Cast<TreeNode>())
                     {
                         var walkedFrom = WalkNodes(node);
                         foreach (var subNode in walkedFrom)
                         {
                             subNode.Checked = false;

                         }
                     }
                     DataTable dt1 = new DataTable();
                     dt1.Columns.Add(new DataColumn("ModuleName", typeof(string)));

                     flag1 = 1;


                     DataTable dt2 = new DataTable();
                     PRI.jbId = lblJbId.Text;
                     dt2 = PRI.GetModuleName();
                     foreach (var node in trvPrivilege.Nodes.Cast<TreeNode>())
                     {
                         var walkedFrom = WalkNodes(node);
                         foreach (var subNode in walkedFrom)
                         {
                             dt1.Rows.Add(subNode.Text);

                         }
                     }
                     var intesectionRows = dt1.AsEnumerable().Intersect(dt2.AsEnumerable(), DataRowComparer.Default);
                     foreach (DataRow rows in intesectionRows)
                     {
                         foreach (var node in trvPrivilege.Nodes.Cast<TreeNode>())
                         {
                             var walkedFrom = WalkNodes(node);
                             foreach (var subNode in walkedFrom)
                             {
                                 if (subNode.Text == rows[0].ToString())
                                 {
                                     subNode.Checked = true;
                                     
                                 }

                             }
                         }
                     }
                 }
                 else
                 {
                     //Retrieve modules of employee
                     foreach (var node in trvPrivilege.Nodes.Cast<TreeNode>())
                     {
                         var walkedFrom = WalkNodes(node);
                         foreach (var subNode in walkedFrom)
                         {
                             subNode.Checked = false;

                         }
                     }
                     DataTable dt1 = new DataTable();
                     dt1.Columns.Add(new DataColumn("ModuleName", typeof(string)));
                     flag1 = 1;
                     DataTable dt2 = new DataTable();
                     foreach (DataGridViewRow rows in dgvPrivileges.Rows)
                     {
                         if (Convert.ToBoolean(((DataGridViewCheckBoxCell)rows.Cells[0]).Value))
                         {
                             EmpPriv.EmpId = int.Parse(rows.Cells[2].Value.ToString());
                         }
                     }
                     dt2 = EmpPriv.GetModuleName();
                     foreach (var node in trvPrivilege.Nodes.Cast<TreeNode>())
                     {
                         var walkedFrom = WalkNodes(node);
                         foreach (var subNode in walkedFrom)
                         {
                             dt1.Rows.Add(subNode.Text);

                         }
                     }
                     var intesectionRows = dt1.AsEnumerable().Intersect(dt2.AsEnumerable(), DataRowComparer.Default);
                     foreach (DataRow rows in intesectionRows)
                     {
                         foreach (var node in trvPrivilege.Nodes.Cast<TreeNode>())
                         {
                             var walkedFrom = WalkNodes(node);
                             foreach (var subNode in walkedFrom)
                             {
                                 if (subNode.Text == rows[0].ToString())
                                 {
                                     subNode.Checked = true;
                                 }

                             }
                         }
                     }
                 }
             }
             else
             {
                 btnSave.Text = "Save";
                 txtJobRle.Text = "";
                 foreach (var node in trvPrivilege.Nodes.Cast<TreeNode>())
                 {
                     var walkedFrom = WalkNodes(node);
                     foreach (var subNode in walkedFrom)
                     {
                         subNode.Checked = false;

                     }
                 }
                 //}
             }
         }
                      
         private void GetNodeRecursiveJb(TreeNode treeNode)
         {
             DataTable dt1 = new DataTable();

             if (treeNode.Checked == true)
             {
                 PRI.Module = treeNode.Text;

                 dt1 = PRI.getmoduleid();
                 for (int j = 0; j <= dgvPrivileges.RowCount - 1; j++)
                 {
                     if (Convert.ToBoolean(dgvPrivileges.Rows[j].Cells[0].Value) == true)
                     {

                         PRI.Job_Id = int.Parse(dgvPrivileges.Rows[j].Cells[2].Value.ToString());
                         for (int i = 0; i < dt1.Rows.Count; i++)
                         {
                             try
                             {
                                 PRI.Module_Id = int.Parse(dt1.Rows[i][0].ToString());
                                 try
                                 {
                                     PRI.insertjobright();
                                 }
                                 catch
                                 {
                                     //MessageBox.Show(PRI.message);
                                 }
                             }
                             catch
                             {
                                 MessageBox.Show("Already inserted or insertion of a blank data");  
                             }

                         }
                     }

                 }                        
                 
             }
             foreach (TreeNode tn in treeNode.Nodes)
             {
                 GetNodeRecursiveJb(tn);
             }
         }


         private void trvPrivilege_BeforeCheck(object sender, TreeViewCancelEventArgs e)
         {
             //flag1 = 0;
         }
         private void GetNodeRecursiveEmp(TreeNode treeNode)
         {
             DataTable dt1 = new DataTable();

             if (treeNode.Checked == true)
             {
                 PRI.Module = treeNode.Text;

                 dt1 = PRI.getmoduleid();
                 for (int j = 0; j <= dgvPrivileges.RowCount - 1; j++)
                 {
                     if (Convert.ToBoolean(dgvPrivileges.Rows[j].Cells[0].Value) == true)
                     {

                         EmpPriv.EmpId = int.Parse(dgvPrivileges.Rows[j].Cells[2].Value.ToString());
                         for (int i = 0; i < dt1.Rows.Count; i++)
                         {
                             try
                             {


                                 EmpPriv.modId = dt1.Rows[i][0].ToString();
                                 try
                                 {
                                     EmpPriv.InsertPrivilage();
                                 }
                                 catch
                                 {
                                     //MessageBox.Show(PRI.message);
                                 }
                             }
                             catch
                             {
                                 MessageBox.Show("Already inserted or insertion of a blank data");
                             }

                         }
                     }

                 }

             }
             foreach (TreeNode tn in treeNode.Nodes)
             {
                 GetNodeRecursiveEmp(tn);
             }
         }

         IEnumerable<TreeNode> WalkNodes(TreeNode root)
         {
             yield return root;
             var children = root.Nodes.Cast<TreeNode>();
             foreach (var child in children)
             {
                 foreach (var subChild in WalkNodes(child))
                 {
                     yield return subChild;
                 }
             }
         }

         private void cmbJobRole_SelectedIndexChanged(object sender, EventArgs e)
         {
             
             foreach (var node in trvPrivilege.Nodes.Cast<TreeNode>())
             {
                 var walkedFrom = WalkNodes(node);
                 foreach (var subNode in walkedFrom)
                 {
                     subNode.Checked = false;

                 }
             }
             DataTable dt1 = new DataTable();
             dt1.Columns.Add(new DataColumn("ModuleName", typeof(string)));
             if (cmbJobRole.SelectedIndex != 0)
             {
                 flag1 = 1;
                 DataTable dt = new DataTable();
                 Emp.JbTitle = cmbJobRole.Text;
                 dt = Emp.GetJobId();
                 lblJbId.Text = dt.Rows[0][0].ToString();

                DataTable dt2 = new DataTable();
                PRI.jbId = lblJbId.Text;
                dt2 = PRI.GetModuleName();
                foreach (var node in trvPrivilege.Nodes.Cast<TreeNode>())
                {
                    var walkedFrom = WalkNodes(node);
                    foreach (var subNode in walkedFrom)
                    {
                        dt1.Rows.Add(subNode.Text);

                    }
                }
                var intesectionRows = dt1.AsEnumerable().Intersect(dt2.AsEnumerable(), DataRowComparer.Default);
                foreach (DataRow row in intesectionRows)
                {
                    foreach (var node in trvPrivilege.Nodes.Cast<TreeNode>())
                    {
                        var walkedFrom = WalkNodes(node);
                        foreach (var subNode in walkedFrom)
                        {
                            if (subNode.Text == row[0].ToString())
                            {                               
                                subNode.Checked = true;
                            }

                        }
                    }
                 }            

             }
         }

         private void btnNewJbRole_Click(object sender, EventArgs e)
         {
             TabPage t = tbcAdmin.TabPages[1];
             tbcAdmin.SelectedTab = t; //go to tab
             rdbJobRole.Checked = true;
             trvPrivilege.SelectedNode = null;
         }

         private void chkEmpPriv_CheckedChanged(object sender, EventArgs e)
         {
             if (chkEmpPriv.Checked)
             {
                 cmbJobRole.Text = cmbJbRole.Text;
                 InsertEmp();
                 BindEmployee();
                 
                 TabPage t = tbcAdmin.TabPages[1];
                 tbcAdmin.SelectedTab = t; //go to tab
                 rdbEmployee.Checked = true;
                 trvPrivilege.SelectedNode = null;
                 DataTable dt = new DataTable();
                 dt = Emp.GetMaxEmpId();
                 for (int i = 0; i < dgvPrivileges.Rows.Count; i++)
                 {
                     if (dt.Rows[0][0].ToString() == dgvPrivileges.Rows[i].Cells[2].Value.ToString())
                     {
                         dgvPrivileges.Rows[i].Selected = true;
                         dgvPrivileges.Rows[i].Cells[0].Value = true;
                         dgvPrivileges.Rows[i].DefaultCellStyle.SelectionBackColor = Color.Blue;                            
                     }
                 }
                 BindEmployee(); 

             //if (chkEmpPriv.Checked)
             //{
             //   //DataTable dt = new DataTable();
             //   JB.JobRoleid = int.Parse(lblJbId.Text);
             //   mdId = JB.GetModId();
             //   if (mdId.Rows.Count == 0)
             //   {
             //       MessageBox.Show("You can't set Privilges.This Particular job role has no privilges.");
             //   }
                
             }
         }

         private void btnClear_Click(object sender, EventArgs e)
         {
             Clearfileds();
         }

         void InsertEmp()
         {
             if (valid())
             {
                 int LId = GetMaxLedger() + 1;
                 try
                 {
                     //if (Empid == 0)
                     //{
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
                         Regex phoneNumpattern = new Regex(@"\+[0-9]{3}\s+[0-9]{3}\s+[0-9]{5}\s+[0-9]{3}");
                         Regex emailPattern = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
                         Emp.empfname = txtfname.Text;
                         Emp.empmname = txtmname.Text;
                         Emp.emplname = txtlname.Text;
                         Emp.empaddress = txtaddress.Text;
                         Emp.dob = dtpdob.Value;
                         if (rdbMale.Checked)
                             Emp.sex = rdbMale.Text;
                         else

                             Emp.sex = rdbFemale.Text;
                         Emp.designation = txtdesign.Text;
                         if (emailPattern.IsMatch(txtemal.Text))
                         {
                             Emp.email = txtemal.Text;
                         }
                         else
                         {
                             MessageBox.Show("Invalid Email");
                         }
                         if (phoneNumpattern.IsMatch(txttelephone.Text) || txttelephone.Text == "")
                         {
                             Emp.telephone = txttelephone.Text;
                         }
                         else
                         {
                             MessageBox.Show("Invalid Phone Number");
                         }
                         if (phoneNumpattern.IsMatch(txtmobile.Text) || txtmobile.Text == "")
                         {
                             Emp.mobile = txtmobile.Text;
                         }
                         else
                         {
                             MessageBox.Show("Invalid Mobile Number");
                         }
                         Emp.LedgerId = LId.ToString();
                         Emp.Emp_Branche = BRANCH.Text;
                         if (lblJbId.Text == "JbId")
                             Emp.jbId = "";
                         else
                             Emp.jbId = lblJbId.Text;
                         //if (chkEmpPriv.Checked)
                         //    Emp.setPriv = 1;
                         //else
                         //    Emp.setPriv = 0;
                         Emp.InsertEmployee();
                         Addledger();
                         InsertLogin();
                         MessageBox.Show("Employee added successfully");
                         BindEmployees();

                         //Clearfileds();

                         txtfname.Focus();
                         if (chkEmpPriv.Checked == true)
                         {
                             //
                         }
                     }
                     //}
                     //else
                     //{

                     //}
                 }
                 catch
                 {
                     MessageBox.Show("Exception Occured Please Try again after restart application");
                 }
             }
         }
    }
}
