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

   
    public partial class EmployeeJobRole : Form
    {
        Class.Privilage PRI = new Class.Privilage();
        Class.JobRole JB = new Class.JobRole();
        Class.EmployeePrivilage EmpPriv = new Class.EmployeePrivilage();
        Initial mdi = new Initial();
        //Initial mdi = (Initial)Application.OpenForms["Initial"];
        string[] empName;
        int[] empId;
        BindingSource bs = new BindingSource();
        int ch;
        int flag0, flag1, flag2, flag3, flag4, flag5, flag6, flag7, flag8, flag9, flag10, flag11, flag12, count = 0;
        int index = 0;
        int i = -1;
        int employeeId; 
        int barflag = -1;
        int n = 0;
        string[] jbRoleName;
        int[] jbId;
        int length;
        string gridValue = string.Empty;
       
        public EmployeeJobRole()
        {
            InitializeComponent();
           
        }
        //public void Bindemp()
        //{
        //    try
        //    {
        //        DataTable dt = new DataTable();
        //        //  dt = dalInnerJoin.View_Emp_List(balEmp, balLoginInfo);
        //        dt = EmpPriv.SelectEmpDetails();
                
        //        emp_CODE.DataSource = dt;
        //        emp_CODE.DisplayMember = "Emp_Fname";
        //        emp_CODE.ValueMember = "Empid";
                
        //    }
        //    catch
        //    {
        //    }
        //}
        private void EmployeeJobRole_Load(object sender, EventArgs e)
        {
            Initial ini = new Initial();
            txtJobRole.Focus();
            DisplayJobinGrid();
            //tbcJobRole.TabPages.Remove(tbpPrivilegeSettings);
            DisplayJobinCombo();
            btnUpdate.Enabled = false;
            btnUpdate.BackColor = Color.Gray;
            btnDelete.Enabled = false;
            btnDelete.BackColor = Color.Gray;

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
            try
            {
                foreach (ToolStripMenuItem tsmi in mdi.menuMain.Items)
                {
                    if (tsmi.Text != "Logout")
                    {
                        TreeNode tn = new TreeNode(tsmi.Text);
                        getChildNodes(tsmi, tn);
                        treeView1.Nodes.Add(tn);
                    }

                }
            }
            catch { }
       //  DisplayinGrid();
       //  Bindemp();
        }
        private void tbpJobRole_Click(object sender, EventArgs e)
        {

        }



        public void DisplayinGrid()
        {
            DataTable dt = new DataTable();
            //  dt = dalInnerJoin.View_Emp_List(balEmp, balLoginInfo);
            dt = EmpPriv.SelectEmpDetails();
            bs.DataSource = dt;
            EmpDisplay.DataSource = bs;
            EmpDisplay.Columns[2].Visible = false;
            EmpDisplay.Columns[3].HeaderText = "First Name";
            EmpDisplay.Columns[4].HeaderText = "Last Name";
            EmpDisplay.Columns[5].Visible = false;
            EmpDisplay.Columns[6].HeaderText = "Username";
            EmpDisplay.Columns[7].Visible = false;

            for (int i = 0; i < EmpDisplay.Rows.Count; i++)
            {

                if (int.Parse(EmpDisplay.Rows[i].Cells[7].Value.ToString()) == 0)
                {
                    EmpDisplay.Rows[i].Cells[1].Value = "Not granded";
                    string str = EmpDisplay.Rows[i].Cells[1].Value.ToString();
                }
                else
                {
                    EmpDisplay.Rows[i].Cells[1].Value = "Granded";
                    string str = EmpDisplay.Rows[i].Cells[1].Value.ToString();
                }
            }

            length = dt.Rows.Count;
            empName = new string[length];
            empId = new int[length];

            if (EmpDisplay.Rows.Count > 0)
            {
                EmpDisplay.Rows[0].Selected = false;
            }
        }

        private void tbcJobRole_DrawItem(object sender, DrawItemEventArgs e)
        {

            SolidBrush fillBrush = new SolidBrush(Color.White);

            //draw rectangle behind the tabs
            Rectangle lasttabrect = tbcJobRole.GetTabRect(tbcJobRole.TabPages.Count - 1);
            Rectangle background = new Rectangle();
            background.Location = new Point(lasttabrect.Right, 0);

            //pad the rectangle to cover the 1 pixel line between the top of the tabpage and the start of the tabs
            background.Size = new Size(tbcJobRole.Right - background.Left, lasttabrect.Height + 1);
            e.Graphics.FillRectangle(fillBrush, background);

            TabPage page = tbcJobRole.TabPages[e.Index];

            Rectangle paddedBounds = e.Bounds;
            int yOffset = (e.State == DrawItemState.Selected) ? -2 : 1;
            paddedBounds.Offset(1, yOffset);

            if (e.Index == this.tbcJobRole.SelectedIndex)
            {

                e.Graphics.FillRectangle(new SolidBrush(Color.White), e.Bounds);
                TextRenderer.DrawText(e.Graphics, page.Text, page.Font, paddedBounds, Color.Black);
            }

            else
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.Black), e.Bounds);
                TextRenderer.DrawText(e.Graphics, page.Text, page.Font, paddedBounds, Color.White);
            }
        }

        private void tbcJobRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tbcJobRole.SelectedIndex == 0)
            {
                mdi.erpWarning.SetError(cmbJbRole, null);
                mdi.erpWarning.SetError(trvPrivilege, null);
            }
            else
            {
                mdi.erpWarning.SetError(txtJobRole, null);

            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void tbcJobRole_Selecting(object sender, TabControlCancelEventArgs e)
        {
            TabPage t0 = tbcJobRole.TabPages[0];
            TabPage t1 = tbcJobRole.TabPages[1];
            flag9 = 0;
            DataTable dt = new DataTable();
            if (txtJobRole.Text != string.Empty)
            {
                for (int i = 0; i < 1; i++)
                {
                    dt.Columns.Add(new DataColumn("JobRole_Name", typeof(string)));
                }
                dt.Rows.Add(txtJobRole.Text);
                cmbJbRole.DisplayMember = "JobRole_Name";
                cmbJbRole.DataSource = dt;
                cmbJbRole.Enabled = false;
                cmbJbRole.BackColor = Color.Gray;
                flag9 = 1;
            }
            else
            {
                DisplayJobinCombo();
                foreach (var node in trvPrivilege.Nodes.Cast<TreeNode>())
                {
                    var walkedFrom = WalkNodes(node);
                    foreach (var subNode in walkedFrom)
                    {

                        flag4 = 0;
                        subNode.Checked = false;

                    }
                }
                //cmbJbRole.Enabled = true;
                //cmbJbRole.BackColor = Color.White;
                //flag10 = 1;
                flag9 = 0;
            }

            if (flag12 == 1)
            {
                if (e.TabPage == t0)
                {
                    e.Cancel = true;
                    t0.Enabled = false;

                }
            }
            else
            {
                if (e.TabPage == t0)
                {
                    e.Cancel = false;
                    t0.Enabled = true;
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




        private void DisplayJobinGrid()
        {
            DataTable dt = new DataTable();
            dt = JB.BindJobRole();
            bs.DataSource = dt;
            dgvJbRleDisp.DataSource = bs;
            dgvJbRleDisp.Columns[0].HeaderText = "Select";
            dgvJbRleDisp.Columns[1].Visible = false;
            dgvJbRleDisp.Columns[2].HeaderText = "Job Roles";

            length = dt.Rows.Count;
            jbRoleName = new string[length];
            jbId = new int[length];
            if (dgvJbRleDisp.Rows.Count > 0)
            {
                dgvJbRleDisp.Rows[0].Selected = false;
            }
        }


        private void DisplayJobinCombo()
        {
            DataTable dt = new DataTable();
            dt = JB.View_JobRole_JobRoleName();

        //    string demo = balJobRole.jobRoleName;
           // cmbJbRole.DisplayMember = balJobRole.jobRoleName;
          
            DataRow row = dt.NewRow();
            row[0] = "---------Select---------";
            dt.Rows.InsertAt(row, 0);
            cmbJbRole.DataSource = dt;
              cmbJbRole.DisplayMember = "JobRoleTitle";
            if (dt.Rows.Count == 1)
            {
                cmbJbRole.Enabled = false;
                cmbJbRole.BackColor = Color.Gray;
            }
            else
            {
                cmbJbRole.Enabled = true;
                cmbJbRole.BackColor = Color.White;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            tbcJobRole.SelectedIndex = 0;
            mdi.erpWarning.SetError(txtJobRole, null);
            ClearTextBoxes();

            DisplayJobinCombo();
            foreach (var node in trvPrivilege.Nodes.Cast<TreeNode>())
            {
                var walkedFrom = WalkNodes(node);
                foreach (var subNode in walkedFrom)
                {

                    flag4 = 0;
                    subNode.Checked = false;

                }
            }
           mdi.erpWarning.SetError(cmbJbRole, null);
           mdi.erpWarning.SetError(trvPrivilege, null);         
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

        private void trvPrivilege_Enter(object sender, EventArgs e)
        {
            flag4 = 0;
        }

        private void txtJobRole_TextChanged(object sender, EventArgs e)
        {
            mdi.erpWarning.SetError(txtJobRole, null);
        }

        string cmbjobroleselected;
        private void cmbJbRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbJbRole.SelectedIndex != 0)
            {
                try
                {
                    trvPrivilege.ExpandAll();
                    DataTable dt1 = new DataTable();
                    JB.JobRoleTitle = cmbJbRole.Text;
                    dt1 = JB.View_JobId();
                    cmbjobroleselected = cmbJbRole.Text;
                    //  balJobRole.jobRoleName = cmbJbRole.Text;
                    //  dt1 = dalJobRole.View_JobId(balJobRole);
                    lblJobId.Text = dt1.Rows[0][0].ToString();
                }
                catch { }
                flag1 = 1;
                flag6 = 1;
                flag10 = 1;
                flag9 = 0;
                flag11 = 1;
            }
            else
            {
                flag10 = 0;
            }

            if (flag1 == 1 && flag6 == 1 && flag10 == 1)
            {
                mdi.erpWarning.SetError(cmbJbRole, null);
            }
        }

        private void trvPrivilege_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            trvPrivilege.ExpandAll();
            TreeNodeCollection nodes = this.trvPrivilege.Nodes;
            foreach (TreeNode n in nodes)
            {
                if (cmbJbRole.SelectedIndex == 0 && n.Checked == true)
                {
                    flag1 = 2;
                    flag10 = 0;

                }

                else if (cmbJbRole.SelectedIndex != 0 && n.Checked == true)
                {
                    flag1 = 3;
                    flag10 = 1;
                    flag11 = 1;

                }

            }

            if (flag1 == 2 && flag4 != 1 && flag6 != 1 && flag7 != 1 && flag9 != 1 && flag10 != 1)
            {
                mdi.erpWarning.SetError(cmbJbRole, "Select a job role");
            }

            if (flag1 == 3)
            {
                mdi.erpWarning.SetError(trvPrivilege, null);

            }
            
        }

        private void trvPrivilege_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (flag4 != 1)
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

        private void dgvJbRleDisp_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string val = string.Empty;
            if (e.RowIndex >= 0)
            {
                index = e.RowIndex;

                try
                {
                    DataGridViewRow selectedRow = dgvJbRleDisp.Rows[e.RowIndex];
                    DataGridViewCheckBoxCell chk = new DataGridViewCheckBoxCell();
                    chk = (DataGridViewCheckBoxCell)dgvJbRleDisp.Rows[e.RowIndex].Cells[0];
                    DataGridViewRow theRow = dgvJbRleDisp.Rows[e.RowIndex];
                    if (chk.Value == null)
                        chk.Value = false;
                    switch (chk.Value.ToString())
                    {
                        case "True":
                            barflag--;
                            chk.Value = false;
                            int change = Convert.ToInt32(selectedRow.Cells[1].Value);
                            string chngd = Convert.ToString(selectedRow.Cells[2].Value);
                            for (n = 0; n < jbRoleName.Length; n++)
                            {
                                if (jbRoleName[n] == chngd)
                                    jbRoleName[n] = string.Empty;
                                if (jbId[n] == change)
                                    jbId[n] = 0;
                            }

                            dgvJbRleDisp.Rows[dgvJbRleDisp.CurrentRow.Index].Selected = false;
                            dgvJbRleDisp.EndEdit();
                            theRow.DefaultCellStyle.BackColor = Color.White;

                            break;
                        case "False":
                            chk.Value = true;
                            i++;
                            barflag++;
                            try
                            {
                                jbId[i] = Convert.ToInt32(selectedRow.Cells[1].Value);
                                jbRoleName[i] = Convert.ToString(selectedRow.Cells[2].Value);
                                gridValue = Convert.ToString(selectedRow.Cells[2].Value);
                                dgvJbRleDisp.Rows[dgvJbRleDisp.CurrentRow.Index].Selected = false;
                                theRow.DefaultCellStyle.BackColor = Color.Gainsboro;

                            }
                            catch (IndexOutOfRangeException)
                            {
                                for (n = 0; n < jbRoleName.Length; n++)
                                {
                                    if (jbRoleName[n] == string.Empty || jbRoleName[n] == null)
                                    {
                                        jbId[n] = Convert.ToInt32(selectedRow.Cells[1].Value);
                                        jbRoleName[n] = Convert.ToString(selectedRow.Cells[2].Value);
                                        gridValue = Convert.ToString(selectedRow.Cells[2].Value);
                                        dgvJbRleDisp.Rows[dgvJbRleDisp.CurrentRow.Index].Selected = false;
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
                        flag12 = 0;
                        tbcJobRole.SelectedIndex = 0;

                        txtJobRole.Text = string.Empty;
                        txtJobRole.BackColor = Color.White;
                        DisplayJobinCombo();
                        cmbJbRole.SelectedIndex = 0;
                        cmbJbRole.BackColor = Color.White;
                        btnSave.Enabled = true;
                        btnSave.BackColor = Color.LightGray;
                        btnClear.Enabled = true;
                        btnClear.BackColor = Color.LightGray;
                        btnUpdate.Enabled = false;
                        btnUpdate.BackColor = Color.Gray;
                        btnDelete.Enabled = false;
                        btnDelete.BackColor = Color.Gray;
                        txtJobRole.Enabled = true;
                        cmbJbRole.Enabled = true;
                        foreach (var node in trvPrivilege.Nodes.Cast<TreeNode>())
                        {
                            var walkedFrom = WalkNodes(node);
                            foreach (var subNode in walkedFrom)
                            {
                                flag6 = 1;
                                flag4 = 0;
                                subNode.Checked = false;

                            }
                        }


                    }
                    else if (barflag == 0)
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

                        for (int i = 0; i < dgvJbRleDisp.Rows.Count; i++)
                        {
                            if (jbId[i] != 0 && jbRoleName[i] != string.Empty)
                            {
                                for (int j = 0; j < dgvJbRleDisp.Rows.Count; j++)
                                {
                                    if (jbId[i] == int.Parse(dgvJbRleDisp.Rows[j].Cells[1].Value.ToString()))
                                    {
                                        val = dgvJbRleDisp.Rows[j].Cells[2].Value.ToString();
                                        break;
                                    }
                                }
                            }
                        }

                        txtJobRole.Text = val;
                        DataTable dt = new DataTable();
                        dt.Columns.Add(new DataColumn("JobRole_Name", typeof(string)));
                        DataRow row1 = dt.NewRow();
                        row1[0] = val;
                        dt.Rows.InsertAt(row1, 0);
                        cmbJbRole.DataSource = dt;
                        cmbJbRole.DisplayMember = "JobRole_Name";
                        
                        flag7 = 1;
                        flag10 = 1;
                        flag12 = 1;
                        tbcJobRole.SelectedIndex = 1;
                        txtJobRole.Enabled = false;
                        txtJobRole.BackColor = Color.Gray;
                        cmbJbRole.Enabled = false;
                        cmbJbRole.BackColor = Color.Gray;
                        btnDelete.Enabled = true;
                        btnDelete.BackColor = Color.LightGray;
                        btnUpdate.Enabled = true;
                        btnUpdate.BackColor = Color.LightGray;
                        btnSave.Enabled = false;
                        btnSave.BackColor = Color.Gray;
                        btnClear.Enabled = false;
                        btnClear.BackColor = Color.Gray;
                       // balJobRole.jobRoleName = val;
                        PRI.JobRole = val;
                        DataTable dt2 = new DataTable();
                        //dt2 = dalInnerJoin.View_Mod_ModName(balMod, balJobRole, balJobPriv);
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
                                        flag4 = 1;
                                        subNode.Checked = true;
                                    }

                                }
                            }

                        }

                    }

                    else if (barflag > 0)
                    {
                        foreach (var node in trvPrivilege.Nodes.Cast<TreeNode>())
                        {
                            var walkedFrom = WalkNodes(node);
                            foreach (var subNode in walkedFrom)
                            {
                                flag6 = 1;
                                flag4 = 0;
                                subNode.Checked = false;

                            }
                        }
                        //flag6 = 1;
                        tbcJobRole.SelectedIndex = 1;
                        flag12 = 1;
                        txtJobRole.Text = string.Empty;
                        DisplayJobinCombo();
                        cmbJbRole.SelectedIndex = 0;
                        txtJobRole.Enabled = false;
                        txtJobRole.BackColor = Color.Gray;
                        cmbJbRole.Enabled = false;
                        cmbJbRole.BackColor = Color.Gray;
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

                catch (NullReferenceException)
                {
                    MessageBox.Show("Rows not  selected");
                }
            }
        }



        private void GetNodeRecursive(TreeNode treeNode)
        {
            DataTable dt1 = new DataTable();

            if (treeNode.Checked == true)
            {
                PRI.Module= treeNode.Text;

                dt1 = PRI.getmoduleid();
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    try
                    {
                        PRI.Job_Id = int.Parse(lblJobId.Text);
                        PRI.Module_Id = int.Parse(dt1.Rows[i][0].ToString());
                        PRI.insertjobright();
                    }
                    catch {
                       
                    }
                    //balJobPriv.jbPriv_JobId = int.Parse(lblJobId.Text);
                    //balJobPriv.jbPriv_ModId = int.Parse(dt1.Rows[i][0].ToString());
                    //dalJobPriv.Insert(balJobPriv);
                }
            }
            foreach (TreeNode tn in treeNode.Nodes)
            {
                GetNodeRecursive(tn);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                flag2 = 0;
                flag5 = 0;
                TreeNodeCollection nodes = this.treeView1.Nodes;
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
                    for (int i = 0; i < EmpDisplay.Rows.Count; i++)
                    {
                        if (empId[i] != 0 && empName[i] != string.Empty)
                        {
                            for (int j = 0; j < EmpDisplay.Rows.Count; j++)
                            {
                                if (empId[i] == int.Parse(EmpDisplay.Rows[j].Cells[2].Value.ToString()))
                                {
                                    //    balLoginInfo.log_Emp_Id = empId[i];
                                    //    dalLoginInfo.Update_Log_Priv(balLoginInfo);
                                    try
                                    {
                                        EmpPriv.EmpId = empId[i];
                                        EmpPriv.Updatelogintableinfo();
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
                    mdi.erpWarning.SetError(EmpDisplay, "Select employee");
                }
                else if (flag2 != 1)
                {
                    mdi.erpWarning.SetError(trvPrivilege, "Select privileges");
                }
            }
            else
            {
                flag5 = 0;
                flag11 = 0;
                DataTable dt = new DataTable();
                if (!string.IsNullOrEmpty(txtJobRole.Text))
                {
                    try
                    {
                        flag11 = 1;
                        //balJobRole.jobRoleName = txtJobRole.Text;
                        //dalJobRole.Insert(balJobRole);
                        try
                        {
                            JB.JobRoleTitle = txtJobRole.Text;
                            JB.InsertJobRole();


                        }
                        catch { }
                        flag2 = 1;
                    }
                    catch (Exception)
                    {
                        flag2 = 2;
                        flag10 = 1;
                        MessageBox.Show("Already exists");
                    }

                }
                if (tbcJobRole.SelectedIndex == 0)
                {
                    if (string.IsNullOrEmpty(txtJobRole.Text))
                    {
                        mdi.erpWarning.SetError(txtJobRole, "Field cannot be empty");
                        flag5 = 1;
                    }
                }
                if (flag9 != 1)
                {

                    if (cmbJbRole.SelectedIndex != 0)
                    {

                        TreeNodeCollection nodes = this.trvPrivilege.Nodes;
                        foreach (TreeNode n in nodes)
                        {
                            if (n.Checked == false)
                            {
                                flag0 = 1;

                            }
                            else
                            {
                                GetNodeRecursive(n);
                                flag2 = 1;
                                flag0 = 0;
                                flag5 = 0;
                                flag11 = 1;

                            }
                        }
                    }
                    else
                    {
                        if (flag10 != 1 && flag5 != 1)
                        {
                            mdi.erpWarning.SetError(cmbJbRole, "Select a job role");
                            flag0 = 0;
                        }
                    }
                }
                else if (flag9 == 1)
                {

                    // dt = dalJobRole.View_MaxJobId(balJobRole);
                    dt = JB.getmaxidjob();
                    TreeNodeCollection nodes = this.trvPrivilege.Nodes;
                    foreach (TreeNode n in nodes)
                    {
                        if (n.Checked == true)
                        {

                            lblJobId.Text = dt.Rows[0][0].ToString();
                            GetNodeRecursive(n);
                            flag2 = 1;
                            flag11 = 1;
                        }
                    }

                }

                if (flag2 == 1 && flag5 != 1 && flag0 != 1 && flag11 != 0)
                {
                    MessageBox.Show("Submitted successfully");
                    flag10 = 1;
                    flag11 = 0;
                    ClearTextBoxes();
                    mdi.erpWarning.SetError(trvPrivilege, null);
                    mdi.erpWarning.SetError(cmbJbRole, null);
                    DisplayJobinGrid();
                    DisplayJobinCombo();
                    foreach (var node in trvPrivilege.Nodes.Cast<TreeNode>())
                    {
                        var walkedFrom = WalkNodes(node);
                        foreach (var subNode in walkedFrom)
                        {

                            flag4 = 0;
                            subNode.Checked = false;

                        }
                    }
                    barflag = -1;
                }
                if (flag0 == 1)
                {
                    mdi.erpWarning.SetError(trvPrivilege, "No selected privileges");
                }

            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                flag6 = 0;
                DataTable dt1 = new DataTable();
                //  dt1 = dalPriv.View_Priv_Dist_EmpId(balPriv);
                dt1 = EmpPriv.get_distinct_empid();
                DataTable dt3 = new DataTable();
                dt3.Columns.Add(new DataColumn("ModuleName", typeof(string)));
                List<TreeNode> allItems = new List<TreeNode>();

                DataTable dt4 = new DataTable();
                DataTable dt5 = new DataTable();
                if (MessageBox.Show("Are you sure to update?", "Updation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    for (int i = 0; i < EmpDisplay.Rows.Count; i++)
                    {
                        if (empId[i] != 0 && empName[i] != string.Empty)
                        {
                            for (int j = 0; j < EmpDisplay.Rows.Count; j++)
                            {
                                if (empId[i] == int.Parse(EmpDisplay.Rows[j].Cells[2].Value.ToString()))
                                {
                                    TreeNodeCollection nodes = this.treeView1.Nodes;
                                    foreach (TreeNode n in nodes)
                                    {
                                        if (n.Checked == true)
                                        {
                                            flag6 = 1;
                                            //balEmp.emp_FName = dgvEmpDisplay.Rows[j].Cells[3].Value.ToString();
                                            //dt4 = dalInnerJoin.View_Mod_ModuleName(balMod, balEmp, balPriv);
                                            EmpPriv.EmpId = int.Parse(EmpDisplay.Rows[j].Cells[2].Value.ToString());
                                            dt4 = EmpPriv.getmodulesOfemployee();
                                            foreach (var node in treeView1.Nodes.Cast<TreeNode>())
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
                                                    EmpPriv.EmpId = int.Parse(EmpDisplay.Rows[j].Cells[2].Value.ToString());
                                                    EmpPriv.mod_Id = int.Parse(dt5.Rows[0][0].ToString());
                                                    EmpPriv.InsertPrivilage();
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
                                                EmpPriv.EmpId = int.Parse(EmpDisplay.Rows[j].Cells[2].Value.ToString());
                                                EmpPriv.mod_Id = int.Parse(dt5.Rows[0][0].ToString());
                                                EmpPriv.DeleteEmpPrivilege();
                                                //delete
                                            }

                                            if (dt1.Rows.Count > 0)
                                            {
                                                //balLoginInfo.log_Emp_Id = empId[i];
                                                //dalLoginInfo.Update_Log_Priv(balLoginInfo);
                                                EmpPriv.EmpId = empId[i];
                                                EmpPriv.Updatelogintableinfo();
                                            }
                                            else
                                            {
                                                // balLoginInfo.log_Emp_Id = empId[i];
                                                // dalLoginInfo.Update_Log_PrivCode(balLoginInfo);

                                                EmpPriv.EmpId = empId[i];
                                                EmpPriv.UpdatelogintabletoZero();
                                            }
                                            flag4 = 1;

                                        }

                                    }
                                    if (flag6 != 1)
                                    {
                                        mdi.erpWarning.SetError(treeView1, "Select privileges");
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
                    for (int i = 0; i < EmpDisplay.Rows.Count; i++)
                    {
                        if (empId[i] != 0 && empName[i] != string.Empty)
                        {
                            for (int j = 0; j < EmpDisplay.Rows.Count; j++)
                            {
                                if (empId[i] == int.Parse(EmpDisplay.Rows[j].Cells[2].Value.ToString()))
                                {
                                    employeeId = int.Parse(EmpDisplay.Rows[j].Cells[2].Value.ToString());
                                }
                            }
                        }
                    }
                    //  balPriv.priv_Emp_Id = employeeId;
                    //  dt2 = dalInnerJoin.View_Mod_PrivModName(balMod, balPriv);
                    EmpPriv.EmpId = employeeId;
                    dt2 = EmpPriv.View_Modules_In_employeeprivilage();
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



                    foreach (var node in treeView1.Nodes.Cast<TreeNode>())
                    {
                        var walkedFrom = WalkNodes(node);
                        foreach (var subNode in walkedFrom)
                        {

                            flag1 = 0;
                            subNode.Checked = false;

                        }
                    }
                    //txtSearch.Text = string.Empty;

                    //cmbJobRole.SelectedIndex = 0;
                    DisplayinGrid();
                    barflag = -1;

                }
            }



            else
            {







                DataTable dt1 = new DataTable();
                DataTable dt3 = new DataTable();
                dt3.Columns.Add(new DataColumn("ModuleName", typeof(string)));
                List<TreeNode> allItems = new List<TreeNode>();

                DataTable dt4 = new DataTable();
                DataTable dt5 = new DataTable();

                if (MessageBox.Show("Are you sure to update?", "Updation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    for (int i = 0; i < dgvJbRleDisp.Rows.Count; i++)
                    {
                        if (jbId[i] != 0 && jbRoleName[i] != string.Empty)
                        {
                            for (int j = 0; j < dgvJbRleDisp.Rows.Count; j++)
                            {
                                if (jbId[i] == int.Parse(dgvJbRleDisp.Rows[j].Cells[1].Value.ToString()))
                                {
                                    //balJobRole.jb_Id = jbId[i];
                                    //dt1 = dalJobRole.View_Particular_JobName(balJobRole);
                                    JB.JobRoleid = jbId[i];
                                    dt1 = JB.view_particular_jobrole();
                                    if (dgvJbRleDisp.Rows[j].Cells[2].Value.ToString() != dt1.Rows[0][0].ToString())
                                    {
                                        try
                                        {
                                            //  balJobRole.jb_Id = jbId[i];
                                            // balJobRole.jobRoleName = dgvJbRleDisp.Rows[j].Cells[2].Value.ToString();
                                            // dalJobRole.Update(balJobRole);
                                            JB.JobRoleid = jbId[i];
                                            JB.JobRoleTitle = dgvJbRleDisp.Rows[j].Cells[2].Value.ToString();
                                            JB.UpdateJobRole();
                                        }
                                        catch (Exception)
                                        {
                                            flag3 = 1;
                                        }
                                    }

                                    TreeNodeCollection nodes = this.trvPrivilege.Nodes;
                                    foreach (TreeNode n in nodes)
                                    {
                                        if (n.Checked == true)
                                        {
                                            flag8 = 1;
                                            //    balJobRole.jobRoleName = dgvJbRleDisp.Rows[j].Cells[2].Value.ToString();
                                            //  dt4 = dalInnerJoin.View_Mod_ModName(balMod, balJobRole, balJobPriv);

                                            PRI.JobRole = dgvJbRleDisp.Rows[j].Cells[2].Value.ToString();
                                            dt4 = PRI.view_jobrole_privilage();

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
                                                // balMod.modName = row1[0].ToString();
                                                //  dt5 = dalMod.View_Mod_ModuleId(balMod);
                                                PRI.Module = row1[0].ToString();
                                                dt5 = PRI.getmoduleid();
                                                //  balJobPriv.jbPriv_JobId = int.Parse(dgvJbRleDisp.Rows[j].Cells[1].Value.ToString());
                                                //  balJobPriv.jbPriv_ModId = int.Parse(dt5.Rows[0][0].ToString());
                                                //  dalJobPriv.Insert(balJobPriv);
                                                try
                                                {
                                                    PRI.Job_Id = int.Parse(dgvJbRleDisp.Rows[j].Cells[1].Value.ToString());
                                                    PRI.Module_Id = int.Parse(dt5.Rows[0][0].ToString());
                                                    PRI.insertjobright();
                                                }
                                                catch { }

                                                //insert
                                            }

                                            var exceptData2 = minus2.Except(minus1, DataRowComparer.Default);
                                            foreach (DataRow row1 in exceptData2)
                                            {
                                                // balMod.modName = row1[0].ToString();
                                                //   dt5 = dalMod.View_Mod_ModuleId(balMod);
                                                PRI.Module = row1[0].ToString();
                                                dt5 = PRI.getmoduleid();
                                                // balJobPriv.jbPriv_JobId = int.Parse(dgvJbRleDisp.Rows[j].Cells[1].Value.ToString());
                                                // balJobPriv.jbPriv_ModId = int.Parse(dt5.Rows[0][0].ToString());
                                                //  dalJobPriv.DeletePrivilege(balJobPriv);
                                                try
                                                {
                                                    PRI.Job_Id = int.Parse(dgvJbRleDisp.Rows[j].Cells[1].Value.ToString());
                                                    PRI.Module_Id = int.Parse(dt5.Rows[0][0].ToString());
                                                    PRI.deleteJobRight();
                                                }
                                                catch { }
                                                //delete
                                            }

                                        }

                                    }
                                    if (flag8 == 0)
                                    {
                                        //balJobPriv.jbPriv_JobId = jbId[i];
                                        //dalJobPriv.Delete(balJobPriv);
                                        PRI.Job_Id = jbId[i];
                                        PRI.deleteAlljobright();
                                    }

                                }
                            }

                        }
                    }
                    if (flag3 == 0)
                    {
                        MessageBox.Show("Updated Successfully");
                        foreach (var node in trvPrivilege.Nodes.Cast<TreeNode>())
                        {
                            var walkedFrom = WalkNodes(node);
                            foreach (var subNode in walkedFrom)
                            {

                                flag4 = 0;
                                subNode.Checked = false;

                            }
                        }
                        txtJobRole.Enabled = true;
                        txtJobRole.BackColor = Color.White;
                        cmbJbRole.Enabled = true;
                        cmbJbRole.BackColor = Color.White;

                        //flag12 = 1;
                    }
                    if (flag3 == 1)
                    {
                        MessageBox.Show("Already Exists. Try deletion");
                    }

                    DisplayJobinGrid();
                    DisplayJobinCombo();
                    barflag = -1;
                }
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
             count = 0;
            DataTable dt = new DataTable();
          //  dt = dalJobPriv.View_Jb_Priv_Dist_JbId(balJobPriv);
            dt = JB.View_distinct_JobId();
            if (MessageBox.Show("Are you sure to delete?", "Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                for (int i = 0; i < dgvJbRleDisp.Rows.Count; i++)
                {
                    if (jbId[i] != 0 && jbRoleName[i] != string.Empty)
                    {
                        for (int j = 0; j < dgvJbRleDisp.Rows.Count; j++)
                        {
                            if (jbId[i] == int.Parse(dgvJbRleDisp.Rows[j].Cells[1].Value.ToString()))
                            {
                                foreach (DataRow row in dt.Rows)
                                {
                                    if (jbId[i] == int.Parse(row[0].ToString()))
                                    {
                                        ch = 1;
                                        break;
                                    }
                                    else
                                        ch = 2;
                                        
                                }
                                
                                    switch (ch)
                                    {
                                        case 1:
                                            count++;
                                            if (count == 1)
                                            {
                                                if (MessageBox.Show("Dependencies.. Want force deletion? Cause loss of data", "Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                                {
                                                    //balJobPriv.jbPriv_JobId = jbId[i];
                                                    //dalJobPriv.Delete(balJobPriv);
                                                    //balJobRole.jb_Id = jbId[i];
                                                    //balJobRole.jobRoleName = dgvJbRleDisp.Rows[j].Cells[2].Value.ToString(); comment
                                                    //dalJobRole.Delete(balJobRole);
                                                    //barflag--; comment
                                                    JB.JobRoleid = jbId[i];
                                                    JB.DeleteJobRole();
                                                    PRI.Job_Id = jbId[i];
                                                    PRI.deleteprivilage();
                                                    flag3 = 0;
                                                }
                                                else
                                                {
                                                    flag3 = 1;
                                                    //break;
                                                }
                                            }
                                            else
                                            {
                                               // balJobPriv.jbPriv_JobId = jbId[i];
                                              //  dalJobPriv.Delete(balJobPriv);
                                               // balJobRole.jb_Id = jbId[i];
                                                //balJobRole.jobRoleName = dgvJbRleDisp.Rows[j].Cells[2].Value.ToString();
                                              //  dalJobRole.Delete(balJobRole);
                                                //barflag--;
                                                JB.JobRoleid = jbId[i];
                                                JB.DeleteJobRole();
                                                PRI.Job_Id = jbId[i];
                                                PRI.deleteprivilage();
                                                flag3 = 0;
                                            }

                                                break;

                                            
                                        case 2:
                                                try
                                                {
                                                    if (flag3 != 1)
                                                    {
                                                       // balJobRole.jb_Id = jbId[i];
                                                        //balJobRole.jobRoleName = dgvJbRleDisp.Rows[j].Cells[2].Value.ToString();
                                                      //  dalJobRole.Delete(balJobRole);
                                                        //barflag--;
                                                        JB.JobRoleid = jbId[i];
                                                        JB.DeleteJobRole();
                                                        flag3 = 0;
                                                    }
                                                        break;
                                                    
                                                }
                                                catch(Exception)
                                                {
                                                    goto case 1;
                                                                                                       
                                                }

                                            
                                    }
                                
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

                            //flag4 = 0;
                            subNode.Checked = false;

                        }
                    }
                    txtJobRole.Text = string.Empty;
                    txtJobRole.Enabled = true;
                    txtJobRole.BackColor = Color.White;
                    //cmbJbRole.Enabled = true;
                    //cmbJbRole.BackColor = Color.White;
                    btnSave.Enabled = true;
                    btnSave.BackColor = Color.LightGray;
                    btnClear.Enabled = true;
                    btnClear.BackColor = Color.LightGray;
                    btnUpdate.Enabled = false;
                    btnUpdate.BackColor = Color.Gray;
                    btnDelete.Enabled = false;
                    btnDelete.BackColor = Color.Gray;
                    DisplayJobinGrid();
                    DisplayJobinCombo();  
                    barflag = -1;
                    //flag12 = 1;
                }

                                                                                              
            }

        }

        private void tbpJobRole_Click_1(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            DisplayinGrid();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                dgvJbRleDisp.Enabled = false;
                dgvJbRleDisp.Visible = false;
                pnlEmployee.Visible = true;
                DisplayinGrid();
            }
            else
            {
                dgvJbRleDisp.Enabled = true;
                dgvJbRleDisp.Visible = true;
              
            }
            
        }

        private void EmpDisplay_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            mdi.erpWarning.SetError(EmpDisplay, null);
            string val1 = string.Empty;
            string val2 = string.Empty;
            int employeesid = 0;
            if (e.RowIndex >= 0)
            {
                index = e.RowIndex;
                try
                {
                    DataGridViewRow selectedRow = EmpDisplay.Rows[e.RowIndex];
                    DataGridViewCheckBoxCell chk = new DataGridViewCheckBoxCell();
                    chk = (DataGridViewCheckBoxCell)EmpDisplay.Rows[e.RowIndex].Cells[0];
                    DataGridViewRow theRow = EmpDisplay.Rows[e.RowIndex];
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

                            EmpDisplay.Rows[EmpDisplay.CurrentRow.Index].Selected = false;
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
                                EmpDisplay.Rows[EmpDisplay.CurrentRow.Index].Selected = false;
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
                                        EmpDisplay.Rows[EmpDisplay.CurrentRow.Index].Selected = false;
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

                        foreach (var node in treeView1.Nodes.Cast<TreeNode>())
                        {
                            var walkedFrom = WalkNodes(node);
                            foreach (var subNode in walkedFrom)
                            {
                                //flag6 = 1;
                                flag1 = 0;
                                subNode.Checked = false;

                            }
                        }

                        cmbJbRole.SelectedIndex = 0;
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
                        foreach (var node in treeView1.Nodes.Cast<TreeNode>())
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
                            for (int i = 0; i < EmpDisplay.Rows.Count; i++)
                            {
                                if (empId[i] != 0 && empName[i] != string.Empty)
                                {
                                    for (int j = 0; j < EmpDisplay.Rows.Count; j++)
                                    {
                                        if (empId[i] == int.Parse(EmpDisplay.Rows[j].Cells[2].Value.ToString()))
                                        {
                                            employeesid = int.Parse(EmpDisplay.Rows[j].Cells[2].Value.ToString());
                                            val1 = EmpDisplay.Rows[j].Cells[3].Value.ToString();
                                            val2 = EmpDisplay.Rows[j].Cells[1].Value.ToString();
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
                            EmpPriv.emp_fname = val1;
                            DataTable dt2 = new DataTable();
                            EmpPriv.EmpId = employeesid;
                            //  dt2 = dalInnerJoin.View_Mod_ModuleName(balMod, balEmp, balPriv);
                            dt2 = EmpPriv.getmodulesOfemployee();

                            DataTable dt1 = new DataTable();
                            dt1.Columns.Add(new DataColumn("ModuleName", typeof(string)));

                            foreach (var node in treeView1.Nodes.Cast<TreeNode>())
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
                                foreach (var node in treeView1.Nodes.Cast<TreeNode>())
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
                            cmbJbRole.SelectedIndex = 0;
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
                        foreach (var node in treeView1.Nodes.Cast<TreeNode>())
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
                    tbcJobRole.SelectedIndex = 2;
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    //NullReferenceException
                    //  MessageBox.Show("Rows not  selected");
                }

            }




          














          


        }

        private void pnlJobRole_Paint(object sender, PaintEventArgs e)
        {

        }

        private void checkBox_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                dgvJbRleDisp.Enabled = false;
                dgvJbRleDisp.Visible = false;
                pnlEmployee.Visible = true;
                pnlJobRole.Visible = false;
                pnlButtonHolder.Visible = true;
                DisplayinGrid();
            }
            else
            {
                dgvJbRleDisp.Enabled = true;
                dgvJbRleDisp.Visible = true;
                pnlEmployee.Visible = false;
                pnlJobRole.Visible = true;

            }
        }

        private void EmpDisplay_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            mdi.erpWarning.SetError(EmpDisplay, null);
            string val1 = string.Empty;
            string val2 = string.Empty;
            int employeesid = 0;
            if (e.RowIndex >= 0)
            {
                index = e.RowIndex;
                try
                {
                    DataGridViewRow selectedRow = EmpDisplay.Rows[e.RowIndex];
                    DataGridViewCheckBoxCell chk = new DataGridViewCheckBoxCell();
                    chk = (DataGridViewCheckBoxCell)EmpDisplay.Rows[e.RowIndex].Cells[0];
                    DataGridViewRow theRow = EmpDisplay.Rows[e.RowIndex];
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

                            EmpDisplay.Rows[EmpDisplay.CurrentRow.Index].Selected = false;
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
                                EmpDisplay.Rows[EmpDisplay.CurrentRow.Index].Selected = false;
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
                                        EmpDisplay.Rows[EmpDisplay.CurrentRow.Index].Selected = false;
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

                        foreach (var node in treeView1.Nodes.Cast<TreeNode>())
                        {
                            var walkedFrom = WalkNodes(node);
                            foreach (var subNode in walkedFrom)
                            {
                                //flag6 = 1;
                                flag1 = 0;
                                subNode.Checked = false;

                            }
                        }

                        cmbJbRole.SelectedIndex = 0;
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
                        foreach (var node in treeView1.Nodes.Cast<TreeNode>())
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
                            for (int i = 0; i < EmpDisplay.Rows.Count; i++)
                            {
                                if (empId[i] != 0 && empName[i] != string.Empty)
                                {
                                    for (int j = 0; j < EmpDisplay.Rows.Count; j++)
                                    {
                                        if (empId[i] == int.Parse(EmpDisplay.Rows[j].Cells[2].Value.ToString()))
                                        {
                                            employeesid = int.Parse(EmpDisplay.Rows[j].Cells[2].Value.ToString());
                                            val1 = EmpDisplay.Rows[j].Cells[3].Value.ToString();
                                            val2 = EmpDisplay.Rows[j].Cells[1].Value.ToString();
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
                            EmpPriv.emp_fname = val1;
                            DataTable dt2 = new DataTable();
                            EmpPriv.EmpId = employeesid;
                            //  dt2 = dalInnerJoin.View_Mod_ModuleName(balMod, balEmp, balPriv);
                            dt2 = EmpPriv.getmodulesOfemployee();

                            DataTable dt1 = new DataTable();
                            dt1.Columns.Add(new DataColumn("ModuleName", typeof(string)));

                            foreach (var node in treeView1.Nodes.Cast<TreeNode>())
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
                                foreach (var node in treeView1.Nodes.Cast<TreeNode>())
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
                            cmbJbRole.SelectedIndex = 0;
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
                        foreach (var node in treeView1.Nodes.Cast<TreeNode>())
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
                    tbcJobRole.SelectedIndex = 2;
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    //NullReferenceException
                    //  MessageBox.Show("Rows not  selected");
                }

            }
        }

        private void dgvJbRleDisp_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            string val = string.Empty;
            if (e.RowIndex >= 0)
            {
                index = e.RowIndex;

                try
                {
                    DataGridViewRow selectedRow = dgvJbRleDisp.Rows[e.RowIndex];
                    DataGridViewCheckBoxCell chk = new DataGridViewCheckBoxCell();
                    chk = (DataGridViewCheckBoxCell)dgvJbRleDisp.Rows[e.RowIndex].Cells[0];
                    DataGridViewRow theRow = dgvJbRleDisp.Rows[e.RowIndex];
                    if (chk.Value == null)
                        chk.Value = false;
                    switch (chk.Value.ToString())
                    {
                        case "True":
                            barflag--;
                            chk.Value = false;
                            int change = Convert.ToInt32(selectedRow.Cells[1].Value);
                            string chngd = Convert.ToString(selectedRow.Cells[2].Value);
                            for (n = 0; n < jbRoleName.Length; n++)
                            {
                                if (jbRoleName[n] == chngd)
                                    jbRoleName[n] = string.Empty;
                                if (jbId[n] == change)
                                    jbId[n] = 0;
                            }

                            dgvJbRleDisp.Rows[dgvJbRleDisp.CurrentRow.Index].Selected = false;
                            dgvJbRleDisp.EndEdit();
                            theRow.DefaultCellStyle.BackColor = Color.White;

                            break;
                        case "False":
                            chk.Value = true;
                            i++;
                            barflag++;
                            try
                            {
                                jbId[i] = Convert.ToInt32(selectedRow.Cells[1].Value);
                                jbRoleName[i] = Convert.ToString(selectedRow.Cells[2].Value);
                                gridValue = Convert.ToString(selectedRow.Cells[2].Value);
                                dgvJbRleDisp.Rows[dgvJbRleDisp.CurrentRow.Index].Selected = false;
                                theRow.DefaultCellStyle.BackColor = Color.Gainsboro;

                            }
                            catch (IndexOutOfRangeException)
                            {
                                for (n = 0; n < jbRoleName.Length; n++)
                                {
                                    if (jbRoleName[n] == string.Empty || jbRoleName[n] == null)
                                    {
                                        jbId[n] = Convert.ToInt32(selectedRow.Cells[1].Value);
                                        jbRoleName[n] = Convert.ToString(selectedRow.Cells[2].Value);
                                        gridValue = Convert.ToString(selectedRow.Cells[2].Value);
                                        dgvJbRleDisp.Rows[dgvJbRleDisp.CurrentRow.Index].Selected = false;
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
                        flag12 = 0;
                        tbcJobRole.SelectedIndex = 0;

                        txtJobRole.Text = string.Empty;
                        txtJobRole.BackColor = Color.White;
                        DisplayJobinCombo();
                        cmbJbRole.SelectedIndex = 0;
                        cmbJbRole.BackColor = Color.White;
                        btnSave.Enabled = true;
                        btnSave.BackColor = Color.LightGray;
                        btnClear.Enabled = true;
                        btnClear.BackColor = Color.LightGray;
                        btnUpdate.Enabled = false;
                        btnUpdate.BackColor = Color.Gray;
                        btnDelete.Enabled = false;
                        btnDelete.BackColor = Color.Gray;
                        txtJobRole.Enabled = true;
                        cmbJbRole.Enabled = true;
                        foreach (var node in trvPrivilege.Nodes.Cast<TreeNode>())
                        {
                            var walkedFrom = WalkNodes(node);
                            foreach (var subNode in walkedFrom)
                            {
                                flag6 = 1;
                                flag4 = 0;
                                subNode.Checked = false;

                            }
                        }


                    }
                    else if (barflag == 0)
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

                        for (int i = 0; i < dgvJbRleDisp.Rows.Count; i++)
                        {
                            if (jbId[i] != 0 && jbRoleName[i] != string.Empty)
                            {
                                for (int j = 0; j < dgvJbRleDisp.Rows.Count; j++)
                                {
                                    if (jbId[i] == int.Parse(dgvJbRleDisp.Rows[j].Cells[1].Value.ToString()))
                                    {
                                        val = dgvJbRleDisp.Rows[j].Cells[2].Value.ToString();
                                        break;
                                    }
                                }
                            }
                        }

                        txtJobRole.Text = val;
                        DataTable dt = new DataTable();
                        dt.Columns.Add(new DataColumn("JobRole_Name", typeof(string)));
                        DataRow row1 = dt.NewRow();
                        row1[0] = val;
                        dt.Rows.InsertAt(row1, 0);
                        cmbJbRole.DataSource = dt;
                        cmbJbRole.DisplayMember = "JobRole_Name";

                        flag7 = 1;
                        flag10 = 1;
                        flag12 = 1;
                        tbcJobRole.SelectedIndex = 1;
                        txtJobRole.Enabled = false;
                        txtJobRole.BackColor = Color.Gray;
                        cmbJbRole.Enabled = false;
                        cmbJbRole.BackColor = Color.Gray;
                        btnDelete.Enabled = true;
                        btnDelete.BackColor = Color.LightGray;
                        btnUpdate.Enabled = true;
                        btnUpdate.BackColor = Color.LightGray;
                        btnSave.Enabled = false;
                        btnSave.BackColor = Color.Gray;
                        btnClear.Enabled = false;
                        btnClear.BackColor = Color.Gray;
                        // balJobRole.jobRoleName = val;
                        PRI.JobRole = val;
                        DataTable dt2 = new DataTable();
                        //dt2 = dalInnerJoin.View_Mod_ModName(balMod, balJobRole, balJobPriv);
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
                                        flag4 = 1;
                                        subNode.Checked = true;
                                    }

                                }
                            }

                        }

                    }

                    else if (barflag > 0)
                    {
                        foreach (var node in trvPrivilege.Nodes.Cast<TreeNode>())
                        {
                            var walkedFrom = WalkNodes(node);
                            foreach (var subNode in walkedFrom)
                            {
                                flag6 = 1;
                                flag4 = 0;
                                subNode.Checked = false;

                            }
                        }
                        //flag6 = 1;
                        tbcJobRole.SelectedIndex = 1;
                        flag12 = 1;
                        txtJobRole.Text = string.Empty;
                        DisplayJobinCombo();
                        cmbJbRole.SelectedIndex = 0;
                        txtJobRole.Enabled = false;
                        txtJobRole.BackColor = Color.Gray;
                        cmbJbRole.Enabled = false;
                        cmbJbRole.BackColor = Color.Gray;
                        btnDelete.Enabled = true;
                        btnDelete.BackColor = Color.LightGray;
                        btnUpdate.Enabled = true;
                        btnUpdate.BackColor = Color.LightGray;
                        btnSave.Enabled = false;
                        btnSave.BackColor = Color.Gray;
                        btnClear.Enabled = false;
                        btnClear.BackColor = Color.Gray;
                    }
                  //  tbcJobRole.TabPages.Add(tbpPrivilegeSettings);
                }

                catch (NullReferenceException)
                {
                    MessageBox.Show("Rows not  selected");
                }
            }
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                flag2 = 0;
                flag5 = 0;
                TreeNodeCollection nodes = this.treeView1.Nodes;
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
                    for (int i = 0; i < EmpDisplay.Rows.Count; i++)
                    {
                        if (empId[i] != 0 && empName[i] != string.Empty)
                        {
                            for (int j = 0; j < EmpDisplay.Rows.Count; j++)
                            {
                                if (empId[i] == int.Parse(EmpDisplay.Rows[j].Cells[2].Value.ToString()))
                                {
                                    //    balLoginInfo.log_Emp_Id = empId[i];
                                    //    dalLoginInfo.Update_Log_Priv(balLoginInfo);
                                    try
                                    {
                                        EmpPriv.EmpId = empId[i];
                                        EmpPriv.Updatelogintableinfo();
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
                    mdi.erpWarning.SetError(EmpDisplay, "Select employee");
                }
                else if (flag2 != 1)
                {
                    mdi.erpWarning.SetError(trvPrivilege, "Select privileges");
                }
            }
            else
            {
                flag5 = 0;
                flag11 = 0;
                DataTable dt = new DataTable();
                if (!string.IsNullOrEmpty(txtJobRole.Text))
                {
                    try
                    {
                        flag11 = 1;
                        //balJobRole.jobRoleName = txtJobRole.Text;
                        //dalJobRole.Insert(balJobRole);
                        try
                        {
                            JB.JobRoleTitle = txtJobRole.Text;
                            JB.InsertJobRole();


                        }
                        catch { }
                        flag2 = 1;
                    }
                    catch (Exception)
                    {
                        flag2 = 2;
                        flag10 = 1;
                        MessageBox.Show("Already exists");
                    }

                }
                if (tbcJobRole.SelectedIndex == 0)
                {
                    if (string.IsNullOrEmpty(txtJobRole.Text))
                    {
                        mdi.erpWarning.SetError(txtJobRole, "Field cannot be empty");
                        flag5 = 1;
                    }
                }
                if (flag9 != 1)
                {

                    if (cmbJbRole.SelectedIndex != 0)
                    {

                        TreeNodeCollection nodes = this.trvPrivilege.Nodes;
                        foreach (TreeNode n in nodes)
                        {
                            if (n.Checked == false)
                            {
                                flag0 = 1;

                            }
                            else
                            {
                                GetNodeRecursive(n);
                                flag2 = 1;
                                flag0 = 0;
                                flag5 = 0;
                                flag11 = 1;

                            }
                        }
                    }
                    else
                    {
                        if (flag10 != 1 && flag5 != 1)
                        {
                            mdi.erpWarning.SetError(cmbJbRole, "Select a job role");
                            flag0 = 0;
                        }
                    }
                }
                else if (flag9 == 1)
                {

                    // dt = dalJobRole.View_MaxJobId(balJobRole);
                    dt = JB.getmaxidjob();
                    TreeNodeCollection nodes = this.trvPrivilege.Nodes;
                    foreach (TreeNode n in nodes)
                    {
                        if (n.Checked == true)
                        {

                            lblJobId.Text = dt.Rows[0][0].ToString();
                            GetNodeRecursive(n);
                            flag2 = 1;
                            flag11 = 1;
                        }
                    }

                }

                if (flag2 == 1 && flag5 != 1 && flag0 != 1 && flag11 != 0)
                {
                    MessageBox.Show("Submitted successfully");
                    flag10 = 1;
                    flag11 = 0;
                    ClearTextBoxes();
                    mdi.erpWarning.SetError(trvPrivilege, null);
                    mdi.erpWarning.SetError(cmbJbRole, null);
                    DisplayJobinGrid();
                    DisplayJobinCombo();
                    foreach (var node in trvPrivilege.Nodes.Cast<TreeNode>())
                    {
                        var walkedFrom = WalkNodes(node);
                        foreach (var subNode in walkedFrom)
                        {

                            flag4 = 0;
                            subNode.Checked = false;

                        }
                    }
                    barflag = -1;
                }
                if (flag0 == 1)
                {
                    mdi.erpWarning.SetError(trvPrivilege, "No selected privileges");
                }

            }
        }

        private void btnClear_Click_1(object sender, EventArgs e)
        {
            tbcJobRole.SelectedIndex = 0;
            mdi.erpWarning.SetError(txtJobRole, null);
            ClearTextBoxes();

            DisplayJobinCombo();
            foreach (var node in trvPrivilege.Nodes.Cast<TreeNode>())
            {
                var walkedFrom = WalkNodes(node);
                foreach (var subNode in walkedFrom)
                {

                    flag4 = 0;
                    subNode.Checked = false;

                }
            }
            mdi.erpWarning.SetError(cmbJbRole, null);
            mdi.erpWarning.SetError(trvPrivilege, null);  
        }

        private void btnUpdate_Click_1(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                flag6 = 0;
                DataTable dt1 = new DataTable();
                //  dt1 = dalPriv.View_Priv_Dist_EmpId(balPriv);
                dt1 = EmpPriv.get_distinct_empid();
                DataTable dt3 = new DataTable();
                dt3.Columns.Add(new DataColumn("ModuleName", typeof(string)));
                List<TreeNode> allItems = new List<TreeNode>();

                DataTable dt4 = new DataTable();
                DataTable dt5 = new DataTable();
                if (MessageBox.Show("Are you sure to update?", "Updation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    for (int i = 0; i < EmpDisplay.Rows.Count; i++)
                    {
                        if (empId[i] != 0 && empName[i] != string.Empty)
                        {
                            for (int j = 0; j < EmpDisplay.Rows.Count; j++)
                            {
                                if (empId[i] == int.Parse(EmpDisplay.Rows[j].Cells[2].Value.ToString()))
                                {
                                    TreeNodeCollection nodes = this.treeView1.Nodes;
                                    foreach (TreeNode n in nodes)
                                    {
                                        if (n.Checked == true)
                                        {
                                            flag6 = 1;
                                            //balEmp.emp_FName = dgvEmpDisplay.Rows[j].Cells[3].Value.ToString();
                                            //dt4 = dalInnerJoin.View_Mod_ModuleName(balMod, balEmp, balPriv);
                                            EmpPriv.EmpId = int.Parse(EmpDisplay.Rows[j].Cells[2].Value.ToString());
                                            dt4 = EmpPriv.getmodulesOfemployee();
                                            foreach (var node in treeView1.Nodes.Cast<TreeNode>())
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
                                                    EmpPriv.EmpId = int.Parse(EmpDisplay.Rows[j].Cells[2].Value.ToString());
                                                    EmpPriv.mod_Id = int.Parse(dt5.Rows[0][0].ToString());
                                                    EmpPriv.InsertPrivilage();
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
                                                EmpPriv.EmpId = int.Parse(EmpDisplay.Rows[j].Cells[2].Value.ToString());
                                                EmpPriv.mod_Id = int.Parse(dt5.Rows[0][0].ToString());
                                                EmpPriv.DeleteEmpPrivilege();
                                                //delete
                                            }

                                            if (dt1.Rows.Count > 0)
                                            {
                                                //balLoginInfo.log_Emp_Id = empId[i];
                                                //dalLoginInfo.Update_Log_Priv(balLoginInfo);
                                                EmpPriv.EmpId = empId[i];
                                                EmpPriv.Updatelogintableinfo();
                                            }
                                            else
                                            {
                                                // balLoginInfo.log_Emp_Id = empId[i];
                                                // dalLoginInfo.Update_Log_PrivCode(balLoginInfo);

                                                EmpPriv.EmpId = empId[i];
                                                EmpPriv.UpdatelogintabletoZero();
                                            }
                                            flag4 = 1;

                                        }

                                    }
                                    if (flag6 != 1)
                                    {
                                        mdi.erpWarning.SetError(treeView1, "Select privileges");
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
                    for (int i = 0; i < EmpDisplay.Rows.Count; i++)
                    {
                        if (empId[i] != 0 && empName[i] != string.Empty)
                        {
                            for (int j = 0; j < EmpDisplay.Rows.Count; j++)
                            {
                                if (empId[i] == int.Parse(EmpDisplay.Rows[j].Cells[2].Value.ToString()))
                                {
                                    employeeId = int.Parse(EmpDisplay.Rows[j].Cells[2].Value.ToString());
                                }
                            }
                        }
                    }
                    //  balPriv.priv_Emp_Id = employeeId;
                    //  dt2 = dalInnerJoin.View_Mod_PrivModName(balMod, balPriv);
                    EmpPriv.EmpId = employeeId;
                    dt2 = EmpPriv.View_Modules_In_employeeprivilage();
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



                    foreach (var node in treeView1.Nodes.Cast<TreeNode>())
                    {
                        var walkedFrom = WalkNodes(node);
                        foreach (var subNode in walkedFrom)
                        {

                            flag1 = 0;
                            subNode.Checked = false;

                        }
                    }
                    //txtSearch.Text = string.Empty;

                    //cmbJobRole.SelectedIndex = 0;
                    DisplayinGrid();
                    barflag = -1;

                }
            }



            else
            {







                DataTable dt1 = new DataTable();
                DataTable dt3 = new DataTable();
                dt3.Columns.Add(new DataColumn("ModuleName", typeof(string)));
                List<TreeNode> allItems = new List<TreeNode>();

                DataTable dt4 = new DataTable();
                DataTable dt5 = new DataTable();

                if (MessageBox.Show("Are you sure to update?", "Updation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    for (int i = 0; i < dgvJbRleDisp.Rows.Count; i++)
                    {
                        if (jbId[i] != 0 && jbRoleName[i] != string.Empty)
                        {
                            for (int j = 0; j < dgvJbRleDisp.Rows.Count; j++)
                            {
                                if (jbId[i] == int.Parse(dgvJbRleDisp.Rows[j].Cells[1].Value.ToString()))
                                {
                                    //balJobRole.jb_Id = jbId[i];
                                    //dt1 = dalJobRole.View_Particular_JobName(balJobRole);
                                    JB.JobRoleid = jbId[i];
                                    dt1 = JB.view_particular_jobrole();
                                    if (dgvJbRleDisp.Rows[j].Cells[2].Value.ToString() != dt1.Rows[0][0].ToString())
                                    {
                                        try
                                        {
                                            //  balJobRole.jb_Id = jbId[i];
                                            // balJobRole.jobRoleName = dgvJbRleDisp.Rows[j].Cells[2].Value.ToString();
                                            // dalJobRole.Update(balJobRole);
                                            JB.JobRoleid = jbId[i];
                                            JB.JobRoleTitle = dgvJbRleDisp.Rows[j].Cells[2].Value.ToString();
                                            JB.UpdateJobRole();
                                        }
                                        catch (Exception)
                                        {
                                            flag3 = 1;
                                        }
                                    }

                                    TreeNodeCollection nodes = this.trvPrivilege.Nodes;
                                    foreach (TreeNode n in nodes)
                                    {
                                        if (n.Checked == true)
                                        {
                                            flag8 = 1;
                                            //    balJobRole.jobRoleName = dgvJbRleDisp.Rows[j].Cells[2].Value.ToString();
                                            //  dt4 = dalInnerJoin.View_Mod_ModName(balMod, balJobRole, balJobPriv);

                                            PRI.JobRole = dgvJbRleDisp.Rows[j].Cells[2].Value.ToString();
                                            dt4 = PRI.view_jobrole_privilage();

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
                                                // balMod.modName = row1[0].ToString();
                                                //  dt5 = dalMod.View_Mod_ModuleId(balMod);
                                                PRI.Module = row1[0].ToString();
                                                dt5 = PRI.getmoduleid();
                                                //  balJobPriv.jbPriv_JobId = int.Parse(dgvJbRleDisp.Rows[j].Cells[1].Value.ToString());
                                                //  balJobPriv.jbPriv_ModId = int.Parse(dt5.Rows[0][0].ToString());
                                                //  dalJobPriv.Insert(balJobPriv);
                                                try
                                                {
                                                    PRI.Job_Id = int.Parse(dgvJbRleDisp.Rows[j].Cells[1].Value.ToString());
                                                    PRI.Module_Id = int.Parse(dt5.Rows[0][0].ToString());
                                                    PRI.insertjobright();
                                                }
                                                catch { }

                                                //insert
                                            }

                                            var exceptData2 = minus2.Except(minus1, DataRowComparer.Default);
                                            foreach (DataRow row1 in exceptData2)
                                            {
                                                // balMod.modName = row1[0].ToString();
                                                //   dt5 = dalMod.View_Mod_ModuleId(balMod);
                                                PRI.Module = row1[0].ToString();
                                                dt5 = PRI.getmoduleid();
                                                // balJobPriv.jbPriv_JobId = int.Parse(dgvJbRleDisp.Rows[j].Cells[1].Value.ToString());
                                                // balJobPriv.jbPriv_ModId = int.Parse(dt5.Rows[0][0].ToString());
                                                //  dalJobPriv.DeletePrivilege(balJobPriv);
                                                try
                                                {
                                                    PRI.Job_Id = int.Parse(dgvJbRleDisp.Rows[j].Cells[1].Value.ToString());
                                                    PRI.Module_Id = int.Parse(dt5.Rows[0][0].ToString());
                                                    PRI.deleteJobRight();
                                                }
                                                catch { }
                                                //delete
                                            }

                                        }

                                    }
                                    if (flag8 == 0)
                                    {
                                        //balJobPriv.jbPriv_JobId = jbId[i];
                                        //dalJobPriv.Delete(balJobPriv);
                                        PRI.Job_Id = jbId[i];
                                        PRI.deleteAlljobright();
                                    }

                                }
                            }

                        }
                    }
                    if (flag3 == 0)
                    {
                        MessageBox.Show("Updated Successfully");
                        foreach (var node in trvPrivilege.Nodes.Cast<TreeNode>())
                        {
                            var walkedFrom = WalkNodes(node);
                            foreach (var subNode in walkedFrom)
                            {

                                flag4 = 0;
                                subNode.Checked = false;

                            }
                        }
                        txtJobRole.Enabled = true;
                        txtJobRole.BackColor = Color.White;
                        cmbJbRole.Enabled = true;
                        cmbJbRole.BackColor = Color.White;

                        //flag12 = 1;
                    }
                    if (flag3 == 1)
                    {
                        MessageBox.Show("Already Exists. Try deletion");
                    }

                    DisplayJobinGrid();
                    DisplayJobinCombo();
                    barflag = -1;
                }
            }
        }

        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            count = 0;
            DataTable dt = new DataTable();
            //  dt = dalJobPriv.View_Jb_Priv_Dist_JbId(balJobPriv);
            dt = JB.View_distinct_JobId();
            if (MessageBox.Show("Are you sure to delete?", "Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                for (int i = 0; i < dgvJbRleDisp.Rows.Count; i++)
                {
                    if (jbId[i] != 0 && jbRoleName[i] != string.Empty)
                    {
                        for (int j = 0; j < dgvJbRleDisp.Rows.Count; j++)
                        {
                            if (jbId[i] == int.Parse(dgvJbRleDisp.Rows[j].Cells[1].Value.ToString()))
                            {
                                foreach (DataRow row in dt.Rows)
                                {
                                    if (jbId[i] == int.Parse(row[0].ToString()))
                                    {
                                        ch = 1;
                                        break;
                                    }
                                    else
                                        ch = 2;

                                }

                                switch (ch)
                                {
                                    case 1:
                                        count++;
                                        if (count == 1)
                                        {
                                            if (MessageBox.Show("Dependencies.. Want force deletion? Cause loss of data", "Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                            {
                                                //balJobPriv.jbPriv_JobId = jbId[i];
                                                //dalJobPriv.Delete(balJobPriv);
                                                //balJobRole.jb_Id = jbId[i];
                                                //balJobRole.jobRoleName = dgvJbRleDisp.Rows[j].Cells[2].Value.ToString(); comment
                                                //dalJobRole.Delete(balJobRole);
                                                //barflag--; comment
                                                JB.JobRoleid = jbId[i];
                                                JB.DeleteJobRole();
                                                PRI.Job_Id = jbId[i];
                                                PRI.deleteprivilage();
                                                flag3 = 0;
                                            }
                                            else
                                            {
                                                flag3 = 1;
                                                //break;
                                            }
                                        }
                                        else
                                        {
                                            // balJobPriv.jbPriv_JobId = jbId[i];
                                            //  dalJobPriv.Delete(balJobPriv);
                                            // balJobRole.jb_Id = jbId[i];
                                            //balJobRole.jobRoleName = dgvJbRleDisp.Rows[j].Cells[2].Value.ToString();
                                            //  dalJobRole.Delete(balJobRole);
                                            //barflag--;
                                            JB.JobRoleid = jbId[i];
                                            JB.DeleteJobRole();
                                            PRI.Job_Id = jbId[i];
                                            PRI.deleteprivilage();
                                            flag3 = 0;
                                        }

                                        break;


                                    case 2:
                                        try
                                        {
                                            if (flag3 != 1)
                                            {
                                                // balJobRole.jb_Id = jbId[i];
                                                //balJobRole.jobRoleName = dgvJbRleDisp.Rows[j].Cells[2].Value.ToString();
                                                //  dalJobRole.Delete(balJobRole);
                                                //barflag--;
                                                JB.JobRoleid = jbId[i];
                                                JB.DeleteJobRole();
                                                flag3 = 0;
                                            }
                                            break;

                                        }
                                        catch (Exception)
                                        {
                                            goto case 1;

                                        }


                                }

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

                            //flag4 = 0;
                            subNode.Checked = false;

                        }
                    }
                    txtJobRole.Text = string.Empty;
                    txtJobRole.Enabled = true;
                    txtJobRole.BackColor = Color.White;
                    //cmbJbRole.Enabled = true;
                    //cmbJbRole.BackColor = Color.White;
                    btnSave.Enabled = true;
                    btnSave.BackColor = Color.LightGray;
                    btnClear.Enabled = true;
                    btnClear.BackColor = Color.LightGray;
                    btnUpdate.Enabled = false;
                    btnUpdate.BackColor = Color.Gray;
                    btnDelete.Enabled = false;
                    btnDelete.BackColor = Color.Gray;
                    DisplayJobinGrid();
                    DisplayJobinCombo();
                    barflag = -1;
                    //flag12 = 1;
                }


            }
        }

      

       


    }
}
