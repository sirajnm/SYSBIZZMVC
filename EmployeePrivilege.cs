using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sys_Sols_Inventory
{

    
    public partial class EmployeePrivilege : Form
    {
        Class.EmployeePrivilage EmpPri = new Class.EmployeePrivilage();
        Class.JobRole JOB = new Class.JobRole();
        Class.Privilage PRI = new Class.Privilage();

        BindingSource bs = new BindingSource();
        Initial mdi=new Initial();
       // Initial mdi = (Initial)Application.OpenForms["Initial"];

        int flag1, flag2, flag3, flag4, flag5, flag6 = 0;
        //int flag7, flag8, count, cnt = 0;
        int index = 0;
        int i = -1;
        int barflag = -1;
        int n = 0;
        string[] empName;
        int[] empId;
        int length;
        string gridValue = string.Empty;
        int employeeId; 



        public EmployeePrivilege()
        {
            InitializeComponent();
        }



        public  void DisplayinGrid()
        {
            DataTable dt = new DataTable();
          //  dt = dalInnerJoin.View_Emp_List(balEmp, balLoginInfo);
            dt = EmpPri.SelectEmpDetails();
            bs.DataSource = dt;
            dgvEmpDisplay.DataSource = bs;
            dgvEmpDisplay.Columns[2].Visible = false;
            dgvEmpDisplay.Columns[3].HeaderText = "First Name";
            dgvEmpDisplay.Columns[4].HeaderText = "Last Name";
            dgvEmpDisplay.Columns[5].Visible = false;
            dgvEmpDisplay.Columns[6].HeaderText = "Username";
            dgvEmpDisplay.Columns[7].Visible = false;
            
            for (int i = 0; i < dgvEmpDisplay.Rows.Count; i++)
            {

                if (int.Parse(dgvEmpDisplay.Rows[i].Cells[7].Value.ToString()) == 0)
                {
                    dgvEmpDisplay.Rows[i].Cells[1].Value = "Not granded";
                    string str = dgvEmpDisplay.Rows[i].Cells[1].Value.ToString();
                }
                else
                {
                    dgvEmpDisplay.Rows[i].Cells[1].Value = "Granded";
                    string str = dgvEmpDisplay.Rows[i].Cells[1].Value.ToString();
                }
            }

            length = dt.Rows.Count;
            empName = new string[length];
            empId = new int[length];

            if (dgvEmpDisplay.Rows.Count > 0)
            {
                dgvEmpDisplay.Rows[0].Selected = false;
            }
        }

        private void EmployeePrivilege_Load(object sender, EventArgs e)
        {

            btnUpdate.Enabled = false;
            btnUpdate.BackColor = Color.Gray;
            btnDelete.Enabled = false;
            btnDelete.BackColor = Color.Gray;
           // Initial mdi = (Initial)Application.OpenForms["Initial"];
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

           
            DisplayJob();
            DisplayinGrid();
            DisplayinGrid();
        }

        private void dgvEmpDisplay_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            mdi.erpWarning.SetError(dgvEmpDisplay, null);
            string val1 = string.Empty;
            string val2 = string.Empty;
            int employeesid=0;
            if (e.RowIndex >= 0)
            {
                index = e.RowIndex;
                try
                {
                    DataGridViewRow selectedRow = dgvEmpDisplay.Rows[e.RowIndex];
                    DataGridViewCheckBoxCell chk = new DataGridViewCheckBoxCell();
                    chk = (DataGridViewCheckBoxCell)dgvEmpDisplay.Rows[e.RowIndex].Cells[0];
                    DataGridViewRow theRow = dgvEmpDisplay.Rows[e.RowIndex];
                    if (chk.Value == null)
                        chk.Value = false;
                    switch (chk.Value.ToString())
                    {
                        case "True":
                            barflag--;
                            chk.Value = false;
                            int change = Convert.ToInt32(selectedRow.Cells[2].Value);
                            string chngd = Convert.ToString(selectedRow.Cells[3].Value);
                            for (n = 0; n < empName.Length; n++)
                            {
                                if (empName[n] == chngd)
                                    empName[n] = string.Empty;
                                if (empId[n] == change)
                                    empId[n] = 0;
                            }

                            dgvEmpDisplay.Rows[dgvEmpDisplay.CurrentRow.Index].Selected = false;
                            //dgvEmpDisplay.EndEdit();
                            theRow.DefaultCellStyle.BackColor = Color.White;

                            break;
                        case "False":
                            chk.Value = true;
                            i++;
                            barflag++;
                            try
                            {
                                empId[i] = Convert.ToInt32(selectedRow.Cells[2].Value);
                                empName[i] = Convert.ToString(selectedRow.Cells[3].Value);
                                gridValue = Convert.ToString(selectedRow.Cells[3].Value);
                                dgvEmpDisplay.Rows[dgvEmpDisplay.CurrentRow.Index].Selected = false;
                                theRow.DefaultCellStyle.BackColor = Color.Gainsboro;

                            }
                            catch (IndexOutOfRangeException)
                            {
                                for (n = 0; n < empName.Length; n++)
                                {
                                    if (empName[n] == string.Empty || empName[n] == null)
                                    {
                                        empId[n] = Convert.ToInt32(selectedRow.Cells[2].Value);
                                        empName[n] = Convert.ToString(selectedRow.Cells[3].Value);
                                        gridValue = Convert.ToString(selectedRow.Cells[3].Value);
                                        dgvEmpDisplay.Rows[dgvEmpDisplay.CurrentRow.Index].Selected = false;
                                        //dgvJbRleDisp.BeginEdit(true);
                                        theRow.DefaultCellStyle.BackColor = Color.Gainsboro;
                                        break;

                                    }
                                }
                            }
                            break;
                    }
                    if (barflag == -1)
                    {

                        foreach (var node in trvPrivilege.Nodes.Cast<TreeNode>())
                        {
                            var walkedFrom = WalkNodes(node);
                            foreach (var subNode in walkedFrom)
                            {
                                //flag6 = 1;
                                flag1 = 0;
                                subNode.Checked = false;

                            }
                        }

                        cmbJobRole.SelectedIndex = 0;
                        btnSave.Enabled = true;
                        btnSave.BackColor = Color.LightGray;
                        btnClear.Enabled = true;
                        btnClear.BackColor = Color.LightGray;
                        btnUpdate.Enabled = false;
                        btnUpdate.BackColor = Color.Gray;
                        btnDelete.Enabled = false;
                        btnDelete.BackColor = Color.Gray;

                    }
                    else if (barflag == 0)
                    {
                        foreach (var node in trvPrivilege.Nodes.Cast<TreeNode>())
                        {
                            var walkedFrom = WalkNodes(node);
                            foreach (var subNode in walkedFrom)
                            {
                                flag1 = 0;
                                subNode.Checked = false;

                            }
                        }

                        try
                        {
                            for (int i = 0; i < dgvEmpDisplay.Rows.Count; i++)
                            {
                                if (empId[i] != 0 && empName[i] != string.Empty)
                                {
                                    for (int j = 0; j < dgvEmpDisplay.Rows.Count; j++)
                                    {
                                        if (empId[i] == int.Parse(dgvEmpDisplay.Rows[j].Cells[2].Value.ToString()))
                                        {
                                            employeesid = int.Parse(dgvEmpDisplay.Rows[j].Cells[2].Value.ToString());
                                            val1 = dgvEmpDisplay.Rows[j].Cells[3].Value.ToString();
                                            val2 = dgvEmpDisplay.Rows[j].Cells[1].Value.ToString();
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        catch { }

                        if (val2 == "Granded")
                        {
                            btnDelete.Enabled = true;
                            btnDelete.BackColor = Color.LightGray;
                            btnUpdate.Enabled = true;
                            btnUpdate.BackColor = Color.LightGray;
                            btnSave.Enabled = false;
                            btnSave.BackColor = Color.Gray;
                            btnClear.Enabled = false;
                            btnClear.BackColor = Color.Gray;

                           // balEmp.emp_FName = val1;
                            EmpPri.emp_fname=val1;
                            DataTable dt2 = new DataTable();
                            EmpPri.EmpId=employeesid;
                          //  dt2 = dalInnerJoin.View_Mod_ModuleName(balMod, balEmp, balPriv);
                            dt2=EmpPri.getmodulesOfemployee();

                            DataTable dt1 = new DataTable();
                            dt1.Columns.Add(new DataColumn("ModuleName", typeof(string)));

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
                                            flag1 = 1;
                                            subNode.Checked = true;
                                        }

                                    }
                                }

                            }
                        }
                        else
                        {
                            cmbJobRole.SelectedIndex = 0;
                            btnSave.Enabled = true;
                            btnSave.BackColor = Color.LightGray;
                            btnClear.Enabled = true;
                            btnClear.BackColor = Color.LightGray;
                            btnUpdate.Enabled = false;
                            btnUpdate.BackColor = Color.Gray;
                            btnDelete.Enabled = false;
                            btnDelete.BackColor = Color.Gray;
                        }

                    }

                    else if (barflag > 0)
                    {
                        foreach (var node in trvPrivilege.Nodes.Cast<TreeNode>())
                        {
                            var walkedFrom = WalkNodes(node);
                            foreach (var subNode in walkedFrom)
                            {
                                flag1 = 0;
                                subNode.Checked = false;
                            }
                        }

                        btnDelete.Enabled = true;
                        btnDelete.BackColor = Color.LightGray;
                        btnUpdate.Enabled = true;
                        btnUpdate.BackColor = Color.LightGray;
                        btnSave.Enabled = false;
                        btnSave.BackColor = Color.Gray;
                        btnClear.Enabled = false;
                        btnClear.BackColor = Color.Gray;

                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    //NullReferenceException
                  //  MessageBox.Show("Rows not  selected");
                }
            }
        }

        private void DisplayJob()
        {
            try
            {
                DataTable dt = new DataTable();
                // dt = dalJobRole.View_JobName(balJobRole);
                //  cmbJobRole.DisplayMember = balJobRole.jobRoleName;
                dt = JOB.bindJobroleName();

                DataRow row = dt.NewRow();
                row[0] = "---------Select---------";
                dt.Rows.InsertAt(row, 0);
                cmbJobRole.DisplayMember = "JobRoleTitle";
                cmbJobRole.DataSource = dt;
            }
            catch { }
           
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

               private void trvPrivilege_BeforeCheck(object sender, TreeViewCancelEventArgs e)
               {
                   trvPrivilege.ExpandAll();
                   mdi.erpWarning.SetError(trvPrivilege, null);
               }

               private void ClearTextBoxes()
               {
                   Action<Control.ControlCollection> func = null;

                   func = (controls) =>
                   {
                       foreach (Control control in controls)
                           if (control is TextBox)
                               (control as TextBox).Clear();
                           else
                               func(control.Controls);
                   };

                   func(Controls);

               }

               private void trvPrivilege_AfterCheck(object sender, TreeViewEventArgs e)
               {
                   if (flag1 != 1)
                   {
                       if (trvPrivilege.Enabled)
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
                       }

                       // The code only executes if the user caused the checked state to change.
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
                      // balJobRole.jobRoleName = cmbJobRole.Text;
                       PRI.JobRole=cmbJobRole.Text;
                       DataTable dt2 = new DataTable();
                     //  dt2 = dalInnerJoin.View_Mod_ModName(balMod, balJobRole, balJobPriv);
                       dt2 = PRI.view_jobrole_privilage();

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
                                       flag1 = 1;
                                       subNode.Checked = true;
                                   }

                               }
                           }

                       }

                   }
               }

               private void trvPrivilege_Enter(object sender, EventArgs e)
               {
                   flag1 = 0;
               }

               private void GetNodeRecursive(TreeNode treeNode)
               {
                   DataTable dt1 = new DataTable();
                   if (treeNode.Checked == true)
                   {
                       //balMod.modName = treeNode.Text;

                       //dt1 = dalMod.View_Mod_ModuleId(balMod);
                       PRI.Module = treeNode.Text;
                        dt1=  PRI.getmoduleid();
                    
                       for (int k = 0; k < dt1.Rows.Count; k++)
                       {
                           for (int i = 0; i < dgvEmpDisplay.Rows.Count; i++)
                           {
                               if (empId[i] != 0 && empName[i] != string.Empty)
                               {
                                   for (int j = 0; j < dgvEmpDisplay.Rows.Count; j++)
                                   {
                                       if (empId[i] == int.Parse(dgvEmpDisplay.Rows[j].Cells[2].Value.ToString()))
                                       {
                                          // balPriv.priv_Emp_Id = int.Parse(empId[i].ToString());
                                          // balPriv.priv_ModId = int.Parse(dt1.Rows[k][0].ToString());
                                          // dalPriv.Insert(balPriv);
                                           try
                                           {
                                               EmpPri.EmpId = int.Parse(empId[i].ToString());
                                               EmpPri.mod_Id = int.Parse(dt1.Rows[k][0].ToString());
                                               EmpPri.InsertPrivilage();
                                           }
                                           catch { }

                                       }

                                   }
                               }
                           }
                       }

                   }
                   foreach (TreeNode tn in treeNode.Nodes)
                   {
                       GetNodeRecursive(tn);
                   }

               }

               private void btnSave_Click(object sender, EventArgs e)
               {
                   flag2 = 0;
                   flag5 = 0;
                   TreeNodeCollection nodes = this.trvPrivilege.Nodes;
                   if (barflag >= 0)
                   {
                       flag5 = 1;
                   }
                   foreach (TreeNode n in nodes)
                   {
                       if (n.Checked == true)
                       {
                           flag2 = 1;
                       }
                   }

                   if (flag2 == 1 && flag5 == 1)
                   {
                       foreach (TreeNode n in nodes)
                       {
                           GetNodeRecursive(n);
                       }
                       for (int i = 0; i < dgvEmpDisplay.Rows.Count; i++)
                       {
                           if (empId[i] != 0 && empName[i] != string.Empty)
                           {
                               for (int j = 0; j < dgvEmpDisplay.Rows.Count; j++)
                               {
                                   if (empId[i] == int.Parse(dgvEmpDisplay.Rows[j].Cells[2].Value.ToString()))
                                   {
                                   //    balLoginInfo.log_Emp_Id = empId[i];
                                   //    dalLoginInfo.Update_Log_Priv(balLoginInfo);
                                       try
                                       {
                                           EmpPri.EmpId = empId[i];
                                           EmpPri.Updatelogintableinfo();
                                       }
                                       catch { }
                                   }
                               }
                           }
                       }
                       MessageBox.Show("Submitted successfully");

                       DisplayinGrid();
                       barflag = -1;
                       foreach (var node in trvPrivilege.Nodes.Cast<TreeNode>())
                       {
                           var walkedFrom = WalkNodes(node);
                           foreach (var subNode in walkedFrom)
                           {
                               //flag6 = 1;
                               flag1 = 0;
                               subNode.Checked = false;

                           }
                       }


                   }

                   if (flag5 != 1)
                   {
                       mdi.erpWarning.SetError(dgvEmpDisplay, "Select employee");
                   }
                   else if (flag2 != 1)
                   {
                       mdi.erpWarning.SetError(trvPrivilege, "Select privileges");
                   }
        
               }

               private void btnUpdate_Click(object sender, EventArgs e)
               {

                   flag6 = 0;
                   DataTable dt1 = new DataTable();
                 //  dt1 = dalPriv.View_Priv_Dist_EmpId(balPriv);
                   dt1 = EmpPri.get_distinct_empid();
                   DataTable dt3 = new DataTable();
                   dt3.Columns.Add(new DataColumn("ModuleName", typeof(string)));
                   List<TreeNode> allItems = new List<TreeNode>();

                   DataTable dt4 = new DataTable();
                   DataTable dt5 = new DataTable();
                   if (MessageBox.Show("Are you sure to update?", "Updation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                   {
                       for (int i = 0; i < dgvEmpDisplay.Rows.Count; i++)
                       {
                           if (empId[i] != 0 && empName[i] != string.Empty)
                           {
                               for (int j = 0; j < dgvEmpDisplay.Rows.Count; j++)
                               {
                                   if (empId[i] == int.Parse(dgvEmpDisplay.Rows[j].Cells[2].Value.ToString()))
                                   {
                                       TreeNodeCollection nodes = this.trvPrivilege.Nodes;
                                       foreach (TreeNode n in nodes)
                                       {
                                           if (n.Checked == true)
                                           {
                                               flag6 = 1;
                                               //balEmp.emp_FName = dgvEmpDisplay.Rows[j].Cells[3].Value.ToString();
                                               //dt4 = dalInnerJoin.View_Mod_ModuleName(balMod, balEmp, balPriv);
                                               EmpPri.EmpId=int.Parse(dgvEmpDisplay.Rows[j].Cells[2].Value.ToString());
                                               dt4 = EmpPri.getmodulesOfemployee();
                                               foreach (var node in trvPrivilege.Nodes.Cast<TreeNode>())
                                               {
                                                   var walkedFrom = WalkNodes(node);
                                                   foreach (var subNode in walkedFrom)
                                                   {
                                                       if (subNode.Checked == true)
                                                       {
                                                           allItems.Add(subNode);
                                                       }

                                                   }
                                               }

                                               foreach (var items in allItems)
                                               {
                                                   dt3.Rows.Add(items.Text);
                                               }

                                               IEnumerable<DataRow> minus1 = dt3.AsEnumerable();
                                               IEnumerable<DataRow> minus2 = dt4.AsEnumerable();

                                               var exceptData1 = minus1.Except(minus2, DataRowComparer.Default);
                                               foreach (DataRow row1 in exceptData1)
                                               {
                                                   try
                                                   {
                                                       //balMod.modName = row1[0].ToString();
                                                       //dt5 = dalMod.View_Mod_ModuleId(balMod);
                                                       PRI.Module = row1[0].ToString();
                                                       dt5 = PRI.getmoduleid();
                                                       //balPriv.priv_Emp_Id = int.Parse(dgvEmpDisplay.Rows[j].Cells[2].Value.ToString());
                                                       //balPriv.priv_ModId = int.Parse(dt5.Rows[0][0].ToString());
                                                       //dalPriv.Insert(balPriv);
                                                       EmpPri.EmpId = int.Parse(dgvEmpDisplay.Rows[j].Cells[2].Value.ToString());
                                                       EmpPri.mod_Id = int.Parse(dt5.Rows[0][0].ToString());
                                                       EmpPri.InsertPrivilage();
                                                       //insert
                                                   }
                                                   catch
                                                   { }
                                               }


                                               var exceptData2 = minus2.Except(minus1, DataRowComparer.Default);
                                               foreach (DataRow row1 in exceptData2)
                                               {
                                                   //balMod.modName = row1[0].ToString();
                                                   //dt5 = dalMod.View_Mod_ModuleId(balMod);
                                                   PRI.Module = row1[0].ToString();
                                                   dt5 = PRI.getmoduleid();
                                                   //balPriv.priv_Emp_Id = int.Parse(dgvEmpDisplay.Rows[j].Cells[2].Value.ToString());
                                                   //balPriv.priv_ModId = int.Parse(dt5.Rows[0][0].ToString());
                                                   //dalPriv.DeletePrivilege(balPriv);
                                                   EmpPri.EmpId = int.Parse(dgvEmpDisplay.Rows[j].Cells[2].Value.ToString());
                                                   EmpPri.mod_Id = int.Parse(dt5.Rows[0][0].ToString());
                                                   EmpPri.DeleteEmpPrivilege();
                                                   //delete
                                               }

                                               if (dt1.Rows.Count > 0)
                                               {
                                                   //balLoginInfo.log_Emp_Id = empId[i];
                                                   //dalLoginInfo.Update_Log_Priv(balLoginInfo);
                                                   EmpPri.EmpId = empId[i];
                                                   EmpPri.Updatelogintableinfo();
                                               }
                                               else
                                               {
                                                  // balLoginInfo.log_Emp_Id = empId[i];
                                                  // dalLoginInfo.Update_Log_PrivCode(balLoginInfo);

                                                   EmpPri.EmpId = empId[i];
                                                   EmpPri.UpdatelogintabletoZero();
                                               }
                                               flag4 = 1;

                                           }

                                       }
                                       if (flag6 != 1)
                                       {
                                           mdi.erpWarning.SetError(trvPrivilege, "Select privileges");
                                           flag4 = 0;

                                       }

                                   }
                               }
                           }
                       }

                   }
                   if (flag4 == 1)
                   {
                       MessageBox.Show("Updated Successfully");
                       DataTable dt2 = new DataTable();
                       for (int i = 0; i < dgvEmpDisplay.Rows.Count; i++)
                       {
                           if (empId[i] != 0 && empName[i] != string.Empty)
                           {
                               for (int j = 0; j < dgvEmpDisplay.Rows.Count; j++)
                               {
                                   if (empId[i] == int.Parse(dgvEmpDisplay.Rows[j].Cells[2].Value.ToString()))
                                   {
                                       employeeId = int.Parse(dgvEmpDisplay.Rows[j].Cells[2].Value.ToString());
                                   }
                               }
                           }
                       }
                     //  balPriv.priv_Emp_Id = employeeId;
                     //  dt2 = dalInnerJoin.View_Mod_PrivModName(balMod, balPriv);
                       EmpPri.EmpId = employeeId;
                       dt2 = EmpPri.View_Modules_In_employeeprivilage();
                       List<ToolStripMenuItem> allMnuItems = new List<ToolStripMenuItem>();
                       foreach (ToolStripMenuItem toolItem in mdi.menuMain.Items)
                       {
                           allMnuItems.Add(toolItem);
                           allMnuItems.AddRange(GetItems(toolItem));
                       }
                       foreach (var item in allMnuItems)
                       {
                           foreach (DataRow row in dt2.Rows)
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
                           }
                       }

                       //if (mdi.tsmSupView.Enabled == false)
                       //{
                       //    mdi.tsmSupUpdate.Visible = false;
                       //    mdi.tsmSupDelete.Visible = false;
                       //}
                       //else
                       //{
                       //    mdi.tsmSupUpdate.Visible = true;
                       //    mdi.tsmSupDelete.Visible = true;
                       //}
                       //if (mdi.tsmCustView.Enabled == false)
                       //{
                       //    mdi.tsmCustUpdate.Visible = false;
                       //    mdi.tsmCustDelete.Visible = false;
                       //}
                       //else
                       //{
                       //    mdi.tsmCustUpdate.Visible = true;
                       //    mdi.tsmCustDelete.Visible = true;
                       //}
                       //if (mdi.tsmItmView.Enabled == false)
                       //{
                       //    mdi.tsmItmBarcode.Visible = false;
                       //    mdi.tsmItmBulkEdit.Visible = false;
                       //    mdi.tsmItmDelete.Visible = false;
                       //    mdi.tsmItmExport.Visible = false;
                       //    mdi.tsmItmImport.Visible = false;
                       //    mdi.tsmItmUpdate.Visible = false;
                       //}
                       //else
                       //{
                       //    mdi.tsmItmBarcode.Visible = true;
                       //    mdi.tsmItmBulkEdit.Visible = true;
                       //    mdi.tsmItmDelete.Visible = true;
                       //    mdi.tsmItmExport.Visible = true;
                       //    mdi.tsmItmImport.Visible = true;
                       //    mdi.tsmItmUpdate.Visible = true;
                       //}
                       //if (mdi.tsmItmKitView.Enabled == false)
                       //{
                       //    mdi.tsmItmKitBarcode.Visible = false;
                       //    //mdi.tsmItmKitBulkEdit.Visible = false;
                       //    mdi.tsmItmKitDelete.Visible = false;
                       //    mdi.tsmItmKitExport.Visible = false;
                       //    mdi.tsmItmKitImport.Visible = false;
                       //    mdi.tsmItmKitUpdate.Visible = false;
                       //}
                       //else
                       //{
                       //    mdi.tsmItmKitBarcode.Visible = true;
                       //    //mdi.tsmItmKitBulkEdit.Visible = true;
                       //    mdi.tsmItmKitDelete.Visible = true;
                       //    mdi.tsmItmKitExport.Visible = true;
                       //    mdi.tsmItmKitImport.Visible = true;
                       //    mdi.tsmItmKitUpdate.Visible = true;
                       //}
                       //if (mdi.tsmPerformSales.Enabled == false)
                       //{
                       //    mdi.tsmSuspend.Visible = false;
                       //    mdi.tsmCredit.Visible = false;
                       //    mdi.tsmSearch.Visible = false;
                       //    mdi.tsmCloseRegister.Visible = false;
                       //    mdi.tsmSellGiftCard.Visible = false;
                       //}
                       //else
                       //{
                       //    mdi.tsmSuspend.Visible = true;
                       //    mdi.tsmCredit.Visible = true;
                       //    mdi.tsmSearch.Visible = true;
                       //    mdi.tsmCloseRegister.Visible = true;
                       //    mdi.tsmSellGiftCard.Visible = true;
                       //}

                       foreach (var node in trvPrivilege.Nodes.Cast<TreeNode>())
                       {
                           var walkedFrom = WalkNodes(node);
                           foreach (var subNode in walkedFrom)
                           {

                               flag1 = 0;
                               subNode.Checked = false;

                           }
                       }
                       //txtSearch.Text = string.Empty;
                       cmbJobRole.SelectedIndex = 0;
                       DisplayinGrid();
                       barflag = -1;

                   }
                   
            
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
               private void btnDelete_Click(object sender, EventArgs e)
               {
                               if (MessageBox.Show("Are you sure to delete?", "Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
              {
                for (int i = 0; i < dgvEmpDisplay.Rows.Count; i++)
                {
                    if (empId[i] != 0 && empName[i] != string.Empty)
                    {
                        for (int j = 0; j < dgvEmpDisplay.Rows.Count; j++)
                        {
                            if (empId[i] == int.Parse(dgvEmpDisplay.Rows[j].Cells[2].Value.ToString()))
                            {

                                //balPriv.priv_Emp_Id = empId[i];
                                //dalPriv.Delete(balPriv);
                                //balLoginInfo.log_Emp_Id = empId[i];
                                //dalLoginInfo.Update_Log_PrivCode(balLoginInfo);
                                try
                                {
                                    EmpPri.EmpId = empId[i];
                                    EmpPri.DeleteEmpPrivilege();
                                    EmpPri.EmpId = empId[i];
                                    EmpPri.UpdatelogintabletoZero();
                                    flag3 = 0;
                                }
                                catch { }

                            }
                        }

                    }
                }

                if (flag3 == 0)
                {
                    MessageBox.Show("Deleted !!!");                  
                    foreach (var node in trvPrivilege.Nodes.Cast<TreeNode>())
                    {
                        var walkedFrom = WalkNodes(node);
                        foreach (var subNode in walkedFrom)
                        {

                            flag4 = 0;
                            subNode.Checked = false;

                        }
                    }
                    lblJbRole.Visible = true;
                    cmbJobRole.Visible = true;
                    btnSave.Enabled = true;
                    btnSave.BackColor = Color.LightGray;
                    btnClear.Enabled = true;
                    btnClear.BackColor = Color.LightGray;
                    btnUpdate.Enabled = false;
                    btnUpdate.BackColor = Color.Gray;
                    btnDelete.Enabled = false;
                    btnDelete.BackColor = Color.Gray;
                    DisplayinGrid();
                    DisplayJob();
                    barflag = -1;                  
                    
                }


            
        }

    }

            

            

    }
}
