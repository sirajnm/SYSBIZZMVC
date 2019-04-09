using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sys_Sols_Inventory.Employee
{
   
    public partial class JobRole : Form
    {
        Class.JobRole JB = new Class.JobRole();
        Class.EmployeePrivilage EmpPriv = new Class.EmployeePrivilage();
      //  Main mdi = (Main)Application.OpenForms["Main"];
        Initial mdi = (Initial)Application.OpenForms["Initial"];
        int Togmove=0;
        int MalX;
        int MalY;
        string[] jbRoleName;
        int length;
        int[] jbId;
       // int flag0, flag1, flag2, flag3, flag4, flag5, flag6, flag7, flag8, flag9, flag10, flag11, flag12, count = 0;

       
        public JobRole()
        {
            InitializeComponent();
        }


        //private void DisplayJobinCombo()
        //{
        //    DataTable dt = new DataTable();
        //    dt = JB.View_JobRole_JobRoleName();
        //    cmbJbRole.DisplayMember = "JobRoleTitle";
        //    DataRow row = dt.NewRow();
        //    row[0] = "---------Select---------";
        //    dt.Rows.InsertAt(row, 0);
        //    cmbJbRole.DataSource = dt;
        //    if (dt.Rows.Count == 1)
        //    {
        //        cmbJbRole.Enabled = false;
        //        cmbJbRole.BackColor = Color.Gray;
        //    }
        //    else
        //    {
        //        cmbJbRole.Enabled = true;
        //        cmbJbRole.BackColor = Color.White;
        //    }
        //}

      
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            Togmove = 1;
            MalX = e.X;
            MalY = e.Y;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Togmove == 1)
            {
                
                this.SetDesktopLocation(MousePosition.X-MalX, MousePosition.Y -MalY);
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            Togmove =0;
            
        }

   

    

        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_MouseEnter(object sender, EventArgs e)
        {
            //label1.ForeColor = Color.Orange;
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
         //   label1.ForeColor = Color.White;
        }

    

        public void BindJobRole()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = JB.BindJobRole();
                DgvEmployee.DataSource = dt;
                //dgvJbRleDisp.DataSource = dt;
                //dgvJbRleDisp.Columns[0].HeaderText = "Select";
                //dgvJbRleDisp.Columns[1].Visible = false;
                //dgvJbRleDisp.Columns[2].HeaderText = "Job Roles";

                length = dt.Rows.Count;
                jbRoleName = new string[length];
                jbId = new int[length];
                //if (dgvJbRleDisp.Rows.Count > 0)
                //{
                //    dgvJbRleDisp.Rows[0].Selected = false;
                //}
            }
            catch
            {
            }
        }


        private void JobRole_Load(object sender, EventArgs e)
        {
            BindJobRole();
          //  DisplayJobinCombo();


            //foreach (ToolStripMenuItem tsmi in mdi.menuMain.Items)
            //{
            //    if (tsmi.Text != "Logout")
            //    {
            //        TreeNode tn = new TreeNode(tsmi.Text);
            //        getChildNodes(tsmi, tn);
            //        trvPrivilege.Nodes.Add(tn);
            //    }

            //}            

            ActiveControl = txtjobtitle;

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

        int jobroleid=0;
    

     

        private void txtjobtitle_TextChanged(object sender, EventArgs e)
        {
            if (txtjobtitle.Text == "")
            {
               
            }
        }

        private void btnsave_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (jobroleid == 0)
                {

                    JB.JobRoleTitle = txtjobtitle.Text;
                    JB.InsertJobRole();
                    MessageBox.Show("Job Role Added");
                    txtjobtitle.Text = "";
                    BindJobRole();
                 //   DisplayJobinCombo();
                }
                else
                {

                }
            }
            catch
            {
                MessageBox.Show("Smething Went Wrong Please Try Again");
            }
        }

        private void kryptonButton1_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (jobroleid != 0)
                {
                    JB.JobRoleid = jobroleid;
                    JB.JobRoleTitle = txtjobtitle.Text;
                    JB.UpdateJobRole();
                    MessageBox.Show("Updated Successfully");
                    txtjobtitle.Text = "";
                    jobroleid = 0;
                    BindJobRole();
                   // DisplayJobinCombo();
                }

                else
                {
                    MessageBox.Show("Please Select a JobRole");
                }
            }
            catch
            {
            }
        }

        private void btndelete_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (DialogResult.Yes == MessageBox.Show("Are You sure", "Delete Confirmation", MessageBoxButtons.YesNo))
                {
                    JB.JobRoleid = jobroleid;
                    JB.DeleteJobRole();
                    BindJobRole();
                    //DisplayJobinCombo();
                    txtjobtitle.Text = "";
                    jobroleid = 0;
                }
            }
            catch
            {
            }
        }

        private void kryptonButton3_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                jobroleid = Convert.ToInt32(DgvEmployee.CurrentRow.Cells[0].Value);
                txtjobtitle.Text = DgvEmployee.CurrentRow.Cells[1].Value.ToString();
                
            }
            catch
            {
                jobroleid = 0;
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void btnsaveprivilage_Click(object sender, EventArgs e)
        {
            ////if (flag9 != 1)
            ////{

            ////    if (cmbJbRole.SelectedIndex != 0)
            ////    {

            ////        //TreeNodeCollection nodes = this.trvPrivilege.Nodes;
            ////        foreach (TreeNode n in nodes)
            ////        {
            ////            if (n.Checked == false)
            ////            {
            ////              //  flag0 = 1;

            ////            }
            ////            else
            ////            {
            ////             //  GetNodeRecursive(n);
            ////              //  flag2 = 1;
            ////               // flag0 = 0;
            ////                //flag5 = 0;
            ////                //flag11 = 1;

            ////            }
            ////        }
            ////    }
            ////    else
            ////    {
            ////        //if (flag10 != 1 && flag5 != 1)
            ////        //{
            ////        //    mdi.erpWarning.SetError(cmbJbRole, "Select a job role");
            ////        //    flag0 = 0;
            ////        //}
            ////    }
            ////}
            ////else if (flag9 == 1)
            ////{
            ////    //DataTable dt = new DataTable();
            ////    //dt = JB.View_MaxJobId();
            ////    //TreeNodeCollection nodes = this.trvPrivilege.Nodes;
            ////    //foreach (TreeNode n in nodes)
            ////    //{
            ////    //    if (n.Checked == true)
            ////    //    {

            ////    //        lblJobId.Text = dt.Rows[0][0].ToString();
            ////    //        GetNodeRecursive(n);
            ////    //      //  flag2 = 1;
            ////    //        //flag11 = 1;
            ////    //    }
            ////    //}

            //}
        }




        //private void GetNodeRecursive(TreeNode treeNode)
        //{
        //    DataTable dt1 = new DataTable();

        //    if (treeNode.Checked == true)
        //    {
        //       EmpPriv.modName = treeNode.Text;

        //        dt1 = EmpPriv.View_Mod_ModuleId();
        //        for (int i = 0; i < dt1.Rows.Count; i++)
        //        {
        //            EmpPriv.JbPriv_JobId = Convert.ToInt32(lblJobId.Text);
        //            EmpPriv.JbPriv_ModuleId = int.Parse(dt1.Rows[i][0].ToString());
        //            EmpPriv.Insert();
        //        }
        //    }
        //    foreach (TreeNode tn in treeNode.Nodes)
        //    {
        //        GetNodeRecursive(tn);
        //    }
        //}

        private void cmbJbRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cmbJbRole.SelectedIndex != 0)
            //{
            //    trvPrivilege.ExpandAll();
            //    DataTable dt1 = new DataTable();
            //   JB.JobRoleTitle = cmbJbRole.Text;
            //    dt1 = JB.View_JobId();
            //    lblJobId.Text = dt1.Rows[0][0].ToString();
            //   // flag1 = 1;
            //   // flag6 = 1;
            //   // flag10 = 1;
            //   // flag9 = 0;
            //   // flag11 = 1;
            //}
            //else
            //{
            //   // flag10 = 0;
            //}


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

        private void trvPrivilege_AfterCheck(object sender, TreeViewEventArgs e)
        {
            //if (flag4 != 1)
            //{
            //    if (trvPrivilege.Enabled)
            //    {
            //        trvPrivilege.AfterCheck -= trvPrivilege_AfterCheck;

            //        TreeNode node = e.Node;
            //        if (node.Nodes != null)
            //            node.Nodes.Cast<TreeNode>().ToList().ForEach(v => v.Checked = node.Checked);

            //        node = e.Node.Parent;
            //        while (node != null)
            //        {
            //            bool set = e.Node.Checked
            //                       ? node.Nodes.Cast<TreeNode>()
            //                        .Any(v => v.Checked == e.Node.Checked)
            //                       : node.Nodes.Cast<TreeNode>()
            //                        .All(v => v.Checked == e.Node.Checked);
            //            if (set)
            //            {
            //                node.Checked = e.Node.Checked;
            //                node = node.Parent;
            //            }
            //            else
            //                node = null;
            //        }
            //        trvPrivilege.AfterCheck += trvPrivilege_AfterCheck;
            //    }

            //    // The code only executes if the user caused the checked state to change.
            //    if (e.Action != TreeViewAction.Unknown)
            //    {
            //        if (e.Node.Nodes.Count > 0)
            //        {
            //            /* Calls the CheckAllChildNodes method, passing in the current 
            //            Checked value of the TreeNode whose checked state changed. */
            //            this.CheckAllChildNodes(e.Node, e.Node.Checked);
            //        }
            //    }
            //}
        }

        private void trvPrivilege_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            //trvPrivilege.ExpandAll();
            //TreeNodeCollection nodes = this.trvPrivilege.Nodes;
            //foreach (TreeNode n in nodes)
            //{
            //    if (cmbJbRole.SelectedIndex == 0 && n.Checked == true)
            //    {
            //        flag1 = 2;
            //        flag10 = 0;

            //    }

            //    else if (cmbJbRole.SelectedIndex != 0 && n.Checked == true)
            //    {
            //        flag1 = 3;
            //        flag10 = 1;
            //        flag11 = 1;

            //    }

            //}

            //if (flag1 == 2 && flag4 != 1 && flag6 != 1 && flag7 != 1 && flag9 != 1 && flag10 != 1)
            //{
            //  //  mdi.erpWarning.SetError(cmbJbRole, "Select a job role");
            //    MessageBox.Show("flags");
            //}

            //if (flag1 == 3)
            //{
            //   // mdi.erpWarning.SetError(trvPrivilege, null);
            //    MessageBox.Show("flags1=3");

            //}
            
        }

        private void dgvJbRleDisp_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            jobroleid = 0;
            txtjobtitle.Text = "";
        }
    }
}
