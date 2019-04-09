namespace Sys_Sols_Inventory
{
    partial class EmployeePrivilege
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EmployeePrivilege));
            this.pnlButtonHolder = new System.Windows.Forms.Panel();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.pnlEmployee = new System.Windows.Forms.Panel();
            this.dgvEmpDisplay = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.lblHead = new System.Windows.Forms.Label();
            this.cmbJobRole = new System.Windows.Forms.ComboBox();
            this.lblJbRole = new System.Windows.Forms.Label();
            this.trvPrivilege = new System.Windows.Forms.TreeView();
            this.lblNote = new System.Windows.Forms.Label();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlButtonHolder.SuspendLayout();
            this.pnlEmployee.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmpDisplay)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlButtonHolder
            // 
            this.pnlButtonHolder.BackColor = System.Drawing.Color.Salmon;
            this.pnlButtonHolder.Controls.Add(this.btnClear);
            this.pnlButtonHolder.Controls.Add(this.btnSave);
            this.pnlButtonHolder.Controls.Add(this.btnUpdate);
            this.pnlButtonHolder.Controls.Add(this.btnDelete);
            this.pnlButtonHolder.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtonHolder.Location = new System.Drawing.Point(0, 534);
            this.pnlButtonHolder.Name = "pnlButtonHolder";
            this.pnlButtonHolder.Size = new System.Drawing.Size(1009, 64);
            this.pnlButtonHolder.TabIndex = 3;
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.LightGray;
            this.btnClear.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Location = new System.Drawing.Point(386, 19);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(102, 30);
            this.btnClear.TabIndex = 1;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.LightGray;
            this.btnSave.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(278, 19);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(102, 30);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.LightGray;
            this.btnUpdate.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.Location = new System.Drawing.Point(494, 19);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(102, 30);
            this.btnUpdate.TabIndex = 2;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.LightGray;
            this.btnDelete.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Location = new System.Drawing.Point(602, 19);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(102, 30);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // pnlEmployee
            // 
            this.pnlEmployee.BackColor = System.Drawing.Color.Gainsboro;
            this.pnlEmployee.Controls.Add(this.dgvEmpDisplay);
            this.pnlEmployee.Controls.Add(this.lblHead);
            this.pnlEmployee.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlEmployee.Location = new System.Drawing.Point(0, 0);
            this.pnlEmployee.Name = "pnlEmployee";
            this.pnlEmployee.Size = new System.Drawing.Size(496, 534);
            this.pnlEmployee.TabIndex = 4;
            // 
            // dgvEmpDisplay
            // 
            this.dgvEmpDisplay.AllowUserToAddRows = false;
            this.dgvEmpDisplay.AllowUserToDeleteRows = false;
            this.dgvEmpDisplay.AllowUserToResizeColumns = false;
            this.dgvEmpDisplay.AllowUserToResizeRows = false;
            this.dgvEmpDisplay.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvEmpDisplay.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgvEmpDisplay.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvEmpDisplay.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvEmpDisplay.ColumnHeadersHeight = 30;
            this.dgvEmpDisplay.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.dgvEmpDisplay.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.dgvEmpDisplay.Location = new System.Drawing.Point(16, 38);
            this.dgvEmpDisplay.Name = "dgvEmpDisplay";
            this.dgvEmpDisplay.ReadOnly = true;
            this.dgvEmpDisplay.RowHeadersVisible = false;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgvEmpDisplay.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvEmpDisplay.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dgvEmpDisplay.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvEmpDisplay.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgvEmpDisplay.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEmpDisplay.Size = new System.Drawing.Size(453, 480);
            this.dgvEmpDisplay.TabIndex = 3;
            this.dgvEmpDisplay.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEmpDisplay_CellContentClick);
            // 
            // Column1
            // 
            this.Column1.FalseValue = "False";
            this.Column1.HeaderText = "Select";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.TrueValue = "True";
            this.Column1.Width = 59;
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
            // cmbJobRole
            // 
            this.cmbJobRole.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbJobRole.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbJobRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbJobRole.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbJobRole.FormattingEnabled = true;
            this.cmbJobRole.Location = new System.Drawing.Point(626, 12);
            this.cmbJobRole.Name = "cmbJobRole";
            this.cmbJobRole.Size = new System.Drawing.Size(245, 25);
            this.cmbJobRole.TabIndex = 11;
            this.cmbJobRole.SelectedIndexChanged += new System.EventHandler(this.cmbJobRole_SelectedIndexChanged);
            // 
            // lblJbRole
            // 
            this.lblJbRole.AutoSize = true;
            this.lblJbRole.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblJbRole.Location = new System.Drawing.Point(544, 15);
            this.lblJbRole.Name = "lblJbRole";
            this.lblJbRole.Size = new System.Drawing.Size(65, 17);
            this.lblJbRole.TabIndex = 10;
            this.lblJbRole.Text = "Job Role";
            // 
            // trvPrivilege
            // 
            this.trvPrivilege.BackColor = System.Drawing.Color.White;
            this.trvPrivilege.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.trvPrivilege.CheckBoxes = true;
            this.trvPrivilege.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trvPrivilege.Location = new System.Drawing.Point(526, 38);
            this.trvPrivilege.Name = "trvPrivilege";
            this.trvPrivilege.PathSeparator = "";
            this.trvPrivilege.Size = new System.Drawing.Size(434, 459);
            this.trvPrivilege.TabIndex = 12;
            this.trvPrivilege.BeforeCheck += new System.Windows.Forms.TreeViewCancelEventHandler(this.trvPrivilege_BeforeCheck);
            this.trvPrivilege.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.trvPrivilege_AfterCheck);
            this.trvPrivilege.Enter += new System.EventHandler(this.trvPrivilege_Enter);
            // 
            // lblNote
            // 
            this.lblNote.AutoSize = true;
            this.lblNote.Font = new System.Drawing.Font("Times New Roman", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNote.ForeColor = System.Drawing.Color.Red;
            this.lblNote.Location = new System.Drawing.Point(523, 509);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(377, 18);
            this.lblNote.TabIndex = 13;
            this.lblNote.Text = "*** Selecting a job role will grand privileges automatically\r\n";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Privilege Status";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 146;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Privilege Status";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 146;
            // 
            // EmployeePrivilege
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1009, 598);
            this.Controls.Add(this.lblNote);
            this.Controls.Add(this.trvPrivilege);
            this.Controls.Add(this.cmbJobRole);
            this.Controls.Add(this.lblJbRole);
            this.Controls.Add(this.pnlEmployee);
            this.Controls.Add(this.pnlButtonHolder);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EmployeePrivilege";
            this.Text = "EmployeePrivilege";
            this.Load += new System.EventHandler(this.EmployeePrivilege_Load);
            this.pnlButtonHolder.ResumeLayout(false);
            this.pnlEmployee.ResumeLayout(false);
            this.pnlEmployee.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmpDisplay)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlButtonHolder;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Panel pnlEmployee;
        private System.Windows.Forms.DataGridView dgvEmpDisplay;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.Label lblHead;
        private System.Windows.Forms.ComboBox cmbJobRole;
        private System.Windows.Forms.Label lblJbRole;
        internal System.Windows.Forms.TreeView trvPrivilege;
        private System.Windows.Forms.Label lblNote;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

    }
}