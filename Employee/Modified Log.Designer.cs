namespace Sys_Sols_Inventory.Employee
{
    partial class Modified_Log
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
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.ModifiedLogBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ModLog = new Sys_Sols_Inventory.Employee.ModLog();
            this.Category = new System.Windows.Forms.GroupBox();
            this.EndDate = new System.Windows.Forms.DateTimePicker();
            this.kryptonLabel8 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.Chk = new System.Windows.Forms.CheckBox();
            this.StartDate = new System.Windows.Forms.DateTimePicker();
            this.kryptonLabel6 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.btnSave = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.EditMode = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.Employee = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.ModifiedLogTableAdapter = new Sys_Sols_Inventory.Employee.ModLogTableAdapters.ModifiedLogTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.ModifiedLogBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ModLog)).BeginInit();
            this.Category.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EditMode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Employee)).BeginInit();
            this.SuspendLayout();
            // 
            // ModifiedLogBindingSource
            // 
            this.ModifiedLogBindingSource.DataMember = "ModifiedLog";
            this.ModifiedLogBindingSource.DataSource = this.ModLog;
            // 
            // ModLog
            // 
            this.ModLog.DataSetName = "ModLog";
            this.ModLog.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // Category
            // 
            this.Category.Controls.Add(this.EndDate);
            this.Category.Controls.Add(this.kryptonLabel8);
            this.Category.Controls.Add(this.Chk);
            this.Category.Controls.Add(this.StartDate);
            this.Category.Controls.Add(this.kryptonLabel6);
            this.Category.Controls.Add(this.btnSave);
            this.Category.Controls.Add(this.EditMode);
            this.Category.Controls.Add(this.Employee);
            this.Category.Controls.Add(this.kryptonLabel4);
            this.Category.Controls.Add(this.kryptonLabel1);
            this.Category.Dock = System.Windows.Forms.DockStyle.Top;
            this.Category.Location = new System.Drawing.Point(0, 0);
            this.Category.Name = "Category";
            this.Category.Size = new System.Drawing.Size(872, 116);
            this.Category.TabIndex = 6;
            this.Category.TabStop = false;
            this.Category.Text = "Search";
            // 
            // EndDate
            // 
            this.EndDate.CustomFormat = "dd-MM-yy      h:mm:ss tt";
            this.EndDate.Enabled = false;
            this.EndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.EndDate.Location = new System.Drawing.Point(434, 68);
            this.EndDate.Name = "EndDate";
            this.EndDate.Size = new System.Drawing.Size(200, 20);
            this.EndDate.TabIndex = 22;
            // 
            // kryptonLabel8
            // 
            this.kryptonLabel8.Location = new System.Drawing.Point(348, 68);
            this.kryptonLabel8.Name = "kryptonLabel8";
            this.kryptonLabel8.Size = new System.Drawing.Size(63, 20);
            this.kryptonLabel8.TabIndex = 21;
            this.kryptonLabel8.Values.Text = "End Date:";
            // 
            // Chk
            // 
            this.Chk.AutoSize = true;
            this.Chk.Location = new System.Drawing.Point(119, 46);
            this.Chk.Name = "Chk";
            this.Chk.Size = new System.Drawing.Size(99, 17);
            this.Chk.TabIndex = 16;
            this.Chk.Text = "Report on Date";
            this.Chk.UseVisualStyleBackColor = true;
            this.Chk.CheckedChanged += new System.EventHandler(this.Chk_CheckedChanged);
            // 
            // StartDate
            // 
            this.StartDate.CustomFormat = "dd-MM-yy      hh:mm:ss tt";
            this.StartDate.Enabled = false;
            this.StartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.StartDate.Location = new System.Drawing.Point(119, 68);
            this.StartDate.Name = "StartDate";
            this.StartDate.Size = new System.Drawing.Size(200, 20);
            this.StartDate.TabIndex = 15;
            // 
            // kryptonLabel6
            // 
            this.kryptonLabel6.Location = new System.Drawing.Point(18, 68);
            this.kryptonLabel6.Name = "kryptonLabel6";
            this.kryptonLabel6.Size = new System.Drawing.Size(71, 20);
            this.kryptonLabel6.TabIndex = 14;
            this.kryptonLabel6.Values.Text = "Start  Date:";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(766, 62);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 25);
            this.btnSave.TabIndex = 11;
            this.btnSave.Values.Text = "Search";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // EditMode
            // 
            this.EditMode.DropDownWidth = 211;
            this.EditMode.Items.AddRange(new object[] {
            "Update",
            "Delete"});
            this.EditMode.Location = new System.Drawing.Point(563, 18);
            this.EditMode.Name = "EditMode";
            this.EditMode.Size = new System.Drawing.Size(293, 21);
            this.EditMode.TabIndex = 9;
            // 
            // Employee
            // 
            this.Employee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Employee.DropDownWidth = 211;
            this.Employee.Location = new System.Drawing.Point(117, 18);
            this.Employee.Name = "Employee";
            this.Employee.Size = new System.Drawing.Size(337, 21);
            this.Employee.TabIndex = 8;
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(469, 19);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(73, 20);
            this.kryptonLabel4.TabIndex = 7;
            this.kryptonLabel4.Values.Text = "Edit Mode :";
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(16, 19);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(70, 20);
            this.kryptonLabel1.TabIndex = 4;
            this.kryptonLabel1.Values.Text = "Employee :";
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.ModifiedLogBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Sys_Sols_Inventory.Employee.Report14.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 116);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(872, 366);
            this.reportViewer1.TabIndex = 7;
            // 
            // ModifiedLogTableAdapter
            // 
            this.ModifiedLogTableAdapter.ClearBeforeFill = true;
            // 
            // Modified_Log
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(872, 482);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.Category);
            this.Name = "Modified_Log";
            this.Text = "Modified_Log";
            this.Load += new System.EventHandler(this.Modified_Log_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ModifiedLogBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ModLog)).EndInit();
            this.Category.ResumeLayout(false);
            this.Category.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EditMode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Employee)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox Category;
        private System.Windows.Forms.DateTimePicker EndDate;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel8;
        private System.Windows.Forms.CheckBox Chk;
        private System.Windows.Forms.DateTimePicker StartDate;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel6;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSave;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox EditMode;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox Employee;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource ModifiedLogBindingSource;
        private ModLog ModLog;
        private ModLogTableAdapters.ModifiedLogTableAdapter ModifiedLogTableAdapter;
    }
}