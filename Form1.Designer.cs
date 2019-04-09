namespace Sys_Sols_Inventory
{
    partial class Form1
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlEmployee = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.EmpDisplay = new System.Windows.Forms.DataGridView();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblHead = new System.Windows.Forms.Label();
            this.pnlButtonHolder = new System.Windows.Forms.Panel();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.dgvJbRleDisp = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.lblJobRoleHead = new System.Windows.Forms.Label();
            this.pnlJobRole = new System.Windows.Forms.Panel();
            this.pnlEmployee.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EmpDisplay)).BeginInit();
            this.pnlButtonHolder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvJbRleDisp)).BeginInit();
            this.pnlJobRole.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlEmployee
            // 
            this.pnlEmployee.BackColor = System.Drawing.Color.Gainsboro;
            this.pnlEmployee.Controls.Add(this.label1);
            this.pnlEmployee.Controls.Add(this.EmpDisplay);
            this.pnlEmployee.Controls.Add(this.lblHead);
            this.pnlEmployee.Location = new System.Drawing.Point(0, -1);
            this.pnlEmployee.Name = "pnlEmployee";
            this.pnlEmployee.Size = new System.Drawing.Size(490, 409);
            this.pnlEmployee.TabIndex = 8;
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
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.EmpDisplay.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.EmpDisplay.ColumnHeadersHeight = 30;
            this.EmpDisplay.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn1,
            this.Column2});
            this.EmpDisplay.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.EmpDisplay.Location = new System.Drawing.Point(6, 43);
            this.EmpDisplay.Name = "EmpDisplay";
            this.EmpDisplay.ReadOnly = true;
            this.EmpDisplay.RowHeadersVisible = false;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.EmpDisplay.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this.EmpDisplay.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.EmpDisplay.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EmpDisplay.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.EmpDisplay.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.EmpDisplay.Size = new System.Drawing.Size(453, 240);
            this.EmpDisplay.TabIndex = 3;
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
            // pnlButtonHolder
            // 
            this.pnlButtonHolder.BackColor = System.Drawing.Color.LightCoral;
            this.pnlButtonHolder.Controls.Add(this.btnClear);
            this.pnlButtonHolder.Controls.Add(this.btnSave);
            this.pnlButtonHolder.Controls.Add(this.btnUpdate);
            this.pnlButtonHolder.Controls.Add(this.btnDelete);
            this.pnlButtonHolder.Location = new System.Drawing.Point(349, 817);
            this.pnlButtonHolder.Name = "pnlButtonHolder";
            this.pnlButtonHolder.Size = new System.Drawing.Size(471, 64);
            this.pnlButtonHolder.TabIndex = 7;
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
            // dgvJbRleDisp
            // 
            this.dgvJbRleDisp.AllowUserToAddRows = false;
            this.dgvJbRleDisp.AllowUserToDeleteRows = false;
            this.dgvJbRleDisp.AllowUserToResizeColumns = false;
            this.dgvJbRleDisp.AllowUserToResizeRows = false;
            this.dgvJbRleDisp.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvJbRleDisp.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgvJbRleDisp.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvJbRleDisp.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.dgvJbRleDisp.ColumnHeadersHeight = 30;
            this.dgvJbRleDisp.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.dgvJbRleDisp.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.dgvJbRleDisp.Location = new System.Drawing.Point(43, 70);
            this.dgvJbRleDisp.Name = "dgvJbRleDisp";
            this.dgvJbRleDisp.RowHeadersVisible = false;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgvJbRleDisp.RowsDefaultCellStyle = dataGridViewCellStyle12;
            this.dgvJbRleDisp.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dgvJbRleDisp.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvJbRleDisp.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgvJbRleDisp.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvJbRleDisp.Size = new System.Drawing.Size(327, 264);
            this.dgvJbRleDisp.TabIndex = 3;
            // 
            // Column1
            // 
            this.Column1.FalseValue = "False";
            this.Column1.HeaderText = "";
            this.Column1.Name = "Column1";
            this.Column1.TrueValue = "True";
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
            // pnlJobRole
            // 
            this.pnlJobRole.BackColor = System.Drawing.Color.White;
            this.pnlJobRole.Controls.Add(this.lblJobRoleHead);
            this.pnlJobRole.Controls.Add(this.dgvJbRleDisp);
            this.pnlJobRole.Controls.Add(this.lblSearch);
            this.pnlJobRole.Controls.Add(this.txtSearch);
            this.pnlJobRole.Location = new System.Drawing.Point(0, 408);
            this.pnlJobRole.Name = "pnlJobRole";
            this.pnlJobRole.Size = new System.Drawing.Size(459, 372);
            this.pnlJobRole.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(977, 863);
            this.Controls.Add(this.pnlEmployee);
            this.Controls.Add(this.pnlButtonHolder);
            this.Controls.Add(this.pnlJobRole);
            this.Name = "Form1";
            this.Text = "Form1";
            this.pnlEmployee.ResumeLayout(false);
            this.pnlEmployee.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EmpDisplay)).EndInit();
            this.pnlButtonHolder.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvJbRleDisp)).EndInit();
            this.pnlJobRole.ResumeLayout(false);
            this.pnlJobRole.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlEmployee;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView EmpDisplay;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.Label lblHead;
        private System.Windows.Forms.Panel pnlButtonHolder;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.DataGridView dgvJbRleDisp;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private System.Windows.Forms.Label lblJobRoleHead;
        private System.Windows.Forms.Panel pnlJobRole;
    }
}