namespace Sys_Sols_Inventory.Employee
{
    partial class EmployeeSalesLog
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource3 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.Category = new System.Windows.Forms.GroupBox();
            this.EndDate = new System.Windows.Forms.DateTimePicker();
            this.kryptonLabel8 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.StartDate = new System.Windows.Forms.DateTimePicker();
            this.kryptonLabel6 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.btnSave = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonButton1 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.EmpStatement = new Sys_Sols_Inventory.Employee.EmpStatement();
            this.Sp_EmpStatementBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.Sp_EmpStatementTableAdapter = new Sys_Sols_Inventory.Employee.EmpStatementTableAdapters.Sp_EmpStatementTableAdapter();
            this.Category.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EmpStatement)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Sp_EmpStatementBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // Category
            // 
            this.Category.Controls.Add(this.kryptonButton1);
            this.Category.Controls.Add(this.EndDate);
            this.Category.Controls.Add(this.kryptonLabel8);
            this.Category.Controls.Add(this.StartDate);
            this.Category.Controls.Add(this.kryptonLabel6);
            this.Category.Controls.Add(this.btnSave);
            this.Category.Dock = System.Windows.Forms.DockStyle.Top;
            this.Category.Location = new System.Drawing.Point(0, 0);
            this.Category.Name = "Category";
            this.Category.Size = new System.Drawing.Size(884, 70);
            this.Category.TabIndex = 7;
            this.Category.TabStop = false;
            this.Category.Text = "Search";
            // 
            // EndDate
            // 
            this.EndDate.CustomFormat = "dd-MM-yy      h:mm:ss tt";
            this.EndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.EndDate.Location = new System.Drawing.Point(356, 21);
            this.EndDate.Name = "EndDate";
            this.EndDate.Size = new System.Drawing.Size(155, 20);
            this.EndDate.TabIndex = 22;
            // 
            // kryptonLabel8
            // 
            this.kryptonLabel8.Location = new System.Drawing.Point(288, 21);
            this.kryptonLabel8.Name = "kryptonLabel8";
            this.kryptonLabel8.Size = new System.Drawing.Size(62, 19);
            this.kryptonLabel8.TabIndex = 21;
            this.kryptonLabel8.Values.Text = "End Time :";
            // 
            // StartDate
            // 
            this.StartDate.CustomFormat = "dd-MM-yy      hh:mm:ss tt";
            this.StartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.StartDate.Location = new System.Drawing.Point(104, 20);
            this.StartDate.Name = "StartDate";
            this.StartDate.Size = new System.Drawing.Size(155, 20);
            this.StartDate.TabIndex = 15;
            // 
            // kryptonLabel6
            // 
            this.kryptonLabel6.Location = new System.Drawing.Point(21, 20);
            this.kryptonLabel6.Name = "kryptonLabel6";
            this.kryptonLabel6.Size = new System.Drawing.Size(67, 19);
            this.kryptonLabel6.TabIndex = 14;
            this.kryptonLabel6.Values.Text = "Start Time :";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(549, 19);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(65, 25);
            this.btnSave.TabIndex = 11;
            this.btnSave.Values.Text = "Search";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.Location = new System.Drawing.Point(638, 19);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.Size = new System.Drawing.Size(181, 25);
            this.kryptonButton1.TabIndex = 23;
            this.kryptonButton1.Values.Text = "My Current Session Statement";
            this.kryptonButton1.Click += new System.EventHandler(this.kryptonButton1_Click);
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource3.Name = "DataSet1";
            reportDataSource3.Value = this.Sp_EmpStatementBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Sys_Sols_Inventory.Employee.Report15.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 70);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(884, 203);
            this.reportViewer1.TabIndex = 8;
            // 
            // EmpStatement
            // 
            this.EmpStatement.DataSetName = "EmpStatement";
            this.EmpStatement.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // Sp_EmpStatementBindingSource
            // 
            this.Sp_EmpStatementBindingSource.DataMember = "Sp_EmpStatement";
            this.Sp_EmpStatementBindingSource.DataSource = this.EmpStatement;
            // 
            // Sp_EmpStatementTableAdapter
            // 
            this.Sp_EmpStatementTableAdapter.ClearBeforeFill = true;
            // 
            // EmployeeSalesLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 273);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.Category);
            this.Name = "EmployeeSalesLog";
            this.Text = "Sales Steatement";
            this.Load += new System.EventHandler(this.EmployeeSalesLog_Load);
            this.Category.ResumeLayout(false);
            this.Category.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EmpStatement)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Sp_EmpStatementBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox Category;
        private System.Windows.Forms.DateTimePicker EndDate;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel8;
        private System.Windows.Forms.DateTimePicker StartDate;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel6;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSave;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton1;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource Sp_EmpStatementBindingSource;
        private EmpStatement EmpStatement;
        private EmpStatementTableAdapters.Sp_EmpStatementTableAdapter Sp_EmpStatementTableAdapter;
    }
}