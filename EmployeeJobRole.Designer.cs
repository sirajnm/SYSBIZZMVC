namespace Sys_Sols_Inventory
{
    partial class EmployeeJobRole
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EmployeeJobRole));
            this.tbcJobRole = new System.Windows.Forms.TabControl();
            this.tbpJobRole = new System.Windows.Forms.TabPage();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.lblJobRole = new System.Windows.Forms.Label();
            this.txtJobRole = new System.Windows.Forms.TextBox();
            this.tbpPrivilegeSettings = new System.Windows.Forms.TabPage();
            this.lblJobId = new System.Windows.Forms.Label();
            this.cmbJbRole = new System.Windows.Forms.ComboBox();
            this.lblJbRole = new System.Windows.Forms.Label();
            this.trvPrivilege = new System.Windows.Forms.TreeView();
            this.Emp_privillage = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlEmployee = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.EmpDisplay = new System.Windows.Forms.DataGridView();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblHead = new System.Windows.Forms.Label();
            this.pnlJobRole = new System.Windows.Forms.Panel();
            this.lblJobRoleHead = new System.Windows.Forms.Label();
            this.dgvJbRleDisp = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.pnlButtonHolder = new System.Windows.Forms.Panel();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.tbcJobRole.SuspendLayout();
            this.tbpJobRole.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tbpPrivilegeSettings.SuspendLayout();
            this.Emp_privillage.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.pnlEmployee.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EmpDisplay)).BeginInit();
            this.pnlJobRole.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvJbRleDisp)).BeginInit();
            this.pnlButtonHolder.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbcJobRole
            // 
            this.tbcJobRole.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tbcJobRole.Controls.Add(this.tbpJobRole);
            this.tbcJobRole.Controls.Add(this.tbpPrivilegeSettings);
            this.tbcJobRole.Controls.Add(this.Emp_privillage);
            this.tbcJobRole.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbcJobRole.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tbcJobRole.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbcJobRole.Location = new System.Drawing.Point(0, 0);
            this.tbcJobRole.Name = "tbcJobRole";
            this.tbcJobRole.SelectedIndex = 0;
            this.tbcJobRole.Size = new System.Drawing.Size(434, 750);
            this.tbcJobRole.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.tbcJobRole.TabIndex = 1;
            this.tbcJobRole.TabStop = false;
            this.tbcJobRole.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tbcJobRole_DrawItem);
            this.tbcJobRole.SelectedIndexChanged += new System.EventHandler(this.tbcJobRole_SelectedIndexChanged);
            this.tbcJobRole.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tbcJobRole_Selecting);
            // 
            // tbpJobRole
            // 
            this.tbpJobRole.BackColor = System.Drawing.Color.White;
            this.tbpJobRole.Controls.Add(this.pictureBox1);
            this.tbpJobRole.Controls.Add(this.kryptonLabel2);
            this.tbpJobRole.Controls.Add(this.lblJobRole);
            this.tbpJobRole.Controls.Add(this.txtJobRole);
            this.tbpJobRole.Location = new System.Drawing.Point(4, 29);
            this.tbpJobRole.Name = "tbpJobRole";
            this.tbpJobRole.Padding = new System.Windows.Forms.Padding(3);
            this.tbpJobRole.Size = new System.Drawing.Size(426, 717);
            this.tbpJobRole.TabIndex = 1;
            this.tbpJobRole.Text = "Job Role";
            this.tbpJobRole.Click += new System.EventHandler(this.tbpJobRole_Click_1);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Sys_Sols_Inventory.Properties.Resources.job_role;
            this.pictureBox1.Location = new System.Drawing.Point(127, 110);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(152, 143);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 16;
            this.pictureBox1.TabStop = false;
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(18, 296);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(368, 52);
            this.kryptonLabel2.TabIndex = 15;
            this.kryptonLabel2.Values.Text = "You can create new Job Roles here. For deleting and editing a Job \r\nRole click th" +
    "e checkbox to your right panel and click \r\nthe buttons below\r\n";
            // 
            // lblJobRole
            // 
            this.lblJobRole.AutoSize = true;
            this.lblJobRole.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblJobRole.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblJobRole.Location = new System.Drawing.Point(19, 44);
            this.lblJobRole.Name = "lblJobRole";
            this.lblJobRole.Size = new System.Drawing.Size(78, 18);
            this.lblJobRole.TabIndex = 0;
            this.lblJobRole.Text = "Job Role :";
            // 
            // txtJobRole
            // 
            this.txtJobRole.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtJobRole.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtJobRole.Location = new System.Drawing.Point(99, 41);
            this.txtJobRole.Name = "txtJobRole";
            this.txtJobRole.Size = new System.Drawing.Size(276, 26);
            this.txtJobRole.TabIndex = 1;
            this.txtJobRole.TextChanged += new System.EventHandler(this.txtJobRole_TextChanged);
            // 
            // tbpPrivilegeSettings
            // 
            this.tbpPrivilegeSettings.BackColor = System.Drawing.Color.White;
            this.tbpPrivilegeSettings.Controls.Add(this.lblJobId);
            this.tbpPrivilegeSettings.Controls.Add(this.cmbJbRole);
            this.tbpPrivilegeSettings.Controls.Add(this.lblJbRole);
            this.tbpPrivilegeSettings.Controls.Add(this.trvPrivilege);
            this.tbpPrivilegeSettings.Location = new System.Drawing.Point(4, 29);
            this.tbpPrivilegeSettings.Name = "tbpPrivilegeSettings";
            this.tbpPrivilegeSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tbpPrivilegeSettings.Size = new System.Drawing.Size(426, 866);
            this.tbpPrivilegeSettings.TabIndex = 2;
            this.tbpPrivilegeSettings.Text = "Privilege Settings";
            // 
            // lblJobId
            // 
            this.lblJobId.AutoSize = true;
            this.lblJobId.Location = new System.Drawing.Point(29, 9);
            this.lblJobId.Name = "lblJobId";
            this.lblJobId.Size = new System.Drawing.Size(42, 17);
            this.lblJobId.TabIndex = 0;
            this.lblJobId.Text = "JobId";
            this.lblJobId.Visible = false;
            // 
            // cmbJbRole
            // 
            this.cmbJbRole.AllowDrop = true;
            this.cmbJbRole.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbJbRole.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbJbRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbJbRole.Enabled = false;
            this.cmbJbRole.FormattingEnabled = true;
            this.cmbJbRole.Location = new System.Drawing.Point(100, 26);
            this.cmbJbRole.Name = "cmbJbRole";
            this.cmbJbRole.Size = new System.Drawing.Size(282, 25);
            this.cmbJbRole.TabIndex = 2;
            this.cmbJbRole.SelectedIndexChanged += new System.EventHandler(this.cmbJbRole_SelectedIndexChanged);
            // 
            // lblJbRole
            // 
            this.lblJbRole.AutoSize = true;
            this.lblJbRole.Location = new System.Drawing.Point(29, 33);
            this.lblJbRole.Name = "lblJbRole";
            this.lblJbRole.Size = new System.Drawing.Size(65, 17);
            this.lblJbRole.TabIndex = 1;
            this.lblJbRole.Text = "Job Role";
            // 
            // trvPrivilege
            // 
            this.trvPrivilege.BackColor = System.Drawing.Color.White;
            this.trvPrivilege.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.trvPrivilege.CheckBoxes = true;
            this.trvPrivilege.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trvPrivilege.Location = new System.Drawing.Point(6, 59);
            this.trvPrivilege.Name = "trvPrivilege";
            this.trvPrivilege.PathSeparator = "";
            this.trvPrivilege.Size = new System.Drawing.Size(391, 464);
            this.trvPrivilege.TabIndex = 3;
            this.trvPrivilege.BeforeCheck += new System.Windows.Forms.TreeViewCancelEventHandler(this.trvPrivilege_BeforeCheck);
            this.trvPrivilege.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.trvPrivilege_AfterCheck);
            this.trvPrivilege.Enter += new System.EventHandler(this.trvPrivilege_Enter);
            // 
            // Emp_privillage
            // 
            this.Emp_privillage.Controls.Add(this.label2);
            this.Emp_privillage.Controls.Add(this.treeView1);
            this.Emp_privillage.Location = new System.Drawing.Point(4, 29);
            this.Emp_privillage.Name = "Emp_privillage";
            this.Emp_privillage.Size = new System.Drawing.Size(426, 866);
            this.Emp_privillage.TabIndex = 3;
            this.Emp_privillage.Text = "Employee privillage";
            this.Emp_privillage.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(18, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(148, 18);
            this.label2.TabIndex = 5;
            this.label2.Text = "Employee Privillage";
            // 
            // treeView1
            // 
            this.treeView1.BackColor = System.Drawing.Color.White;
            this.treeView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeView1.CheckBoxes = true;
            this.treeView1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeView1.Location = new System.Drawing.Point(21, 104);
            this.treeView1.Name = "treeView1";
            this.treeView1.PathSeparator = "";
            this.treeView1.Size = new System.Drawing.Size(391, 464);
            this.treeView1.TabIndex = 4;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(436, 12);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(72, 17);
            this.checkBox1.TabIndex = 6;
            this.checkBox1.Text = "Employee";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged_1);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.pnlEmployee);
            this.flowLayoutPanel1.Controls.Add(this.pnlJobRole);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(436, 29);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(467, 788);
            this.flowLayoutPanel1.TabIndex = 7;
            // 
            // pnlEmployee
            // 
            this.pnlEmployee.BackColor = System.Drawing.Color.Gainsboro;
            this.pnlEmployee.Controls.Add(this.label1);
            this.pnlEmployee.Controls.Add(this.EmpDisplay);
            this.pnlEmployee.Controls.Add(this.lblHead);
            this.pnlEmployee.Location = new System.Drawing.Point(3, 3);
            this.pnlEmployee.Name = "pnlEmployee";
            this.pnlEmployee.Size = new System.Drawing.Size(454, 378);
            this.pnlEmployee.TabIndex = 9;
            this.pnlEmployee.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(195, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "label1";
            // 
            // EmpDisplay
            // 
            this.EmpDisplay.AllowUserToAddRows = false;
            this.EmpDisplay.AllowUserToDeleteRows = false;
            this.EmpDisplay.AllowUserToResizeColumns = false;
            this.EmpDisplay.AllowUserToResizeRows = false;
            this.EmpDisplay.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.EmpDisplay.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.EmpDisplay.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.EmpDisplay.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.EmpDisplay.ColumnHeadersHeight = 30;
            this.EmpDisplay.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn1,
            this.Column2});
            this.EmpDisplay.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.EmpDisplay.Location = new System.Drawing.Point(6, 43);
            this.EmpDisplay.Name = "EmpDisplay";
            this.EmpDisplay.ReadOnly = true;
            this.EmpDisplay.RowHeadersVisible = false;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.EmpDisplay.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.EmpDisplay.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.EmpDisplay.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EmpDisplay.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.EmpDisplay.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.EmpDisplay.Size = new System.Drawing.Size(445, 285);
            this.EmpDisplay.TabIndex = 3;
            this.EmpDisplay.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.EmpDisplay_CellContentClick_1);
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.FalseValue = "False";
            this.dataGridViewCheckBoxColumn1.HeaderText = "Select";
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            this.dataGridViewCheckBoxColumn1.ReadOnly = true;
            this.dataGridViewCheckBoxColumn1.TrueValue = "True";
            this.dataGridViewCheckBoxColumn1.Width = 59;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Privilege Status";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 146;
            // 
            // lblHead
            // 
            this.lblHead.AutoSize = true;
            this.lblHead.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHead.ForeColor = System.Drawing.Color.Black;
            this.lblHead.Location = new System.Drawing.Point(13, 22);
            this.lblHead.Name = "lblHead";
            this.lblHead.Size = new System.Drawing.Size(107, 18);
            this.lblHead.TabIndex = 0;
            this.lblHead.Text = "Employee List";
            // 
            // pnlJobRole
            // 
            this.pnlJobRole.BackColor = System.Drawing.Color.White;
            this.pnlJobRole.Controls.Add(this.lblJobRoleHead);
            this.pnlJobRole.Controls.Add(this.dgvJbRleDisp);
            this.pnlJobRole.Controls.Add(this.lblSearch);
            this.pnlJobRole.Controls.Add(this.txtSearch);
            this.pnlJobRole.Location = new System.Drawing.Point(3, 387);
            this.pnlJobRole.Name = "pnlJobRole";
            this.pnlJobRole.Size = new System.Drawing.Size(454, 394);
            this.pnlJobRole.TabIndex = 10;
            // 
            // lblJobRoleHead
            // 
            this.lblJobRoleHead.AutoSize = true;
            this.lblJobRoleHead.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblJobRoleHead.ForeColor = System.Drawing.Color.Black;
            this.lblJobRoleHead.Location = new System.Drawing.Point(13, 9);
            this.lblJobRoleHead.Name = "lblJobRoleHead";
            this.lblJobRoleHead.Size = new System.Drawing.Size(79, 18);
            this.lblJobRoleHead.TabIndex = 0;
            this.lblJobRoleHead.Text = "Job Roles";
            // 
            // dgvJbRleDisp
            // 
            this.dgvJbRleDisp.AllowUserToAddRows = false;
            this.dgvJbRleDisp.AllowUserToDeleteRows = false;
            this.dgvJbRleDisp.AllowUserToResizeColumns = false;
            this.dgvJbRleDisp.AllowUserToResizeRows = false;
            this.dgvJbRleDisp.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvJbRleDisp.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgvJbRleDisp.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvJbRleDisp.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvJbRleDisp.ColumnHeadersHeight = 30;
            this.dgvJbRleDisp.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.dgvJbRleDisp.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.dgvJbRleDisp.Location = new System.Drawing.Point(43, 70);
            this.dgvJbRleDisp.Name = "dgvJbRleDisp";
            this.dgvJbRleDisp.RowHeadersVisible = false;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgvJbRleDisp.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvJbRleDisp.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dgvJbRleDisp.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvJbRleDisp.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgvJbRleDisp.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvJbRleDisp.Size = new System.Drawing.Size(327, 264);
            this.dgvJbRleDisp.TabIndex = 3;
            this.dgvJbRleDisp.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvJbRleDisp_CellContentClick_1);
            // 
            // Column1
            // 
            this.Column1.FalseValue = "False";
            this.Column1.HeaderText = "";
            this.Column1.Name = "Column1";
            this.Column1.TrueValue = "True";
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearch.Location = new System.Drawing.Point(43, 40);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(63, 17);
            this.lblSearch.TabIndex = 1;
            this.lblSearch.Text = "Search :";
            // 
            // txtSearch
            // 
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(115, 37);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(238, 25);
            this.txtSearch.TabIndex = 2;
            // 
            // pnlButtonHolder
            // 
            this.pnlButtonHolder.BackColor = System.Drawing.Color.LightCoral;
            this.pnlButtonHolder.Controls.Add(this.btnClear);
            this.pnlButtonHolder.Controls.Add(this.btnSave);
            this.pnlButtonHolder.Controls.Add(this.btnUpdate);
            this.pnlButtonHolder.Controls.Add(this.btnDelete);
            this.pnlButtonHolder.Location = new System.Drawing.Point(436, 823);
            this.pnlButtonHolder.Name = "pnlButtonHolder";
            this.pnlButtonHolder.Size = new System.Drawing.Size(471, 64);
            this.pnlButtonHolder.TabIndex = 8;
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.LightGray;
            this.btnClear.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Location = new System.Drawing.Point(95, 19);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(80, 30);
            this.btnClear.TabIndex = 1;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click_1);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.LightGray;
            this.btnSave.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(9, 19);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(83, 30);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click_1);
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.LightGray;
            this.btnUpdate.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.Location = new System.Drawing.Point(178, 19);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(80, 30);
            this.btnUpdate.TabIndex = 2;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click_1);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.LightGray;
            this.btnDelete.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Location = new System.Drawing.Point(261, 19);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 30);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click_1);
            // 
            // EmployeeJobRole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(905, 750);
            this.Controls.Add(this.pnlButtonHolder);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.tbcJobRole);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EmployeeJobRole";
            this.Load += new System.EventHandler(this.EmployeeJobRole_Load);
            this.tbcJobRole.ResumeLayout(false);
            this.tbpJobRole.ResumeLayout(false);
            this.tbpJobRole.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tbpPrivilegeSettings.ResumeLayout(false);
            this.tbpPrivilegeSettings.PerformLayout();
            this.Emp_privillage.ResumeLayout(false);
            this.Emp_privillage.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.pnlEmployee.ResumeLayout(false);
            this.pnlEmployee.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EmpDisplay)).EndInit();
            this.pnlJobRole.ResumeLayout(false);
            this.pnlJobRole.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvJbRleDisp)).EndInit();
            this.pnlButtonHolder.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tbcJobRole;
        private System.Windows.Forms.TabPage tbpJobRole;
        private System.Windows.Forms.Label lblJobRole;
        private System.Windows.Forms.TextBox txtJobRole;
        private System.Windows.Forms.TabPage tbpPrivilegeSettings;
        private System.Windows.Forms.Label lblJobId;
        private System.Windows.Forms.ComboBox cmbJbRole;
        private System.Windows.Forms.Label lblJbRole;
        private System.Windows.Forms.TreeView trvPrivilege;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TabPage Emp_privillage;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel pnlButtonHolder;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Panel pnlEmployee;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView EmpDisplay;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.Label lblHead;
        private System.Windows.Forms.Panel pnlJobRole;
        private System.Windows.Forms.Label lblJobRoleHead;
        private System.Windows.Forms.DataGridView dgvJbRleDisp;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;

    }
}