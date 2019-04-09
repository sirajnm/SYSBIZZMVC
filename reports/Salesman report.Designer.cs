namespace Sys_Sols_Inventory.reports
{
    partial class Salesman_report
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
            this.SalesmanReportBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.SalesReportDatasetOnHDR = new Sys_Sols_Inventory.reports.SalesReportDatasetOnHDR();
            this.Cashier_reportBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.SaleDataNew = new Sys_Sols_Inventory.reports.SaleDataNew();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chk_date = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Btn_Search = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.dtp_to = new System.Windows.Forms.DateTimePicker();
            this.dtp_from = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.cmb_sman = new System.Windows.Forms.ComboBox();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.SalesmanReportBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesReportDatasetOnHDR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cashier_reportBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SaleDataNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // SalesmanReportBindingSource
            // 
            this.SalesmanReportBindingSource.DataMember = "SalesmanReport";
            this.SalesmanReportBindingSource.DataSource = this.SalesReportDatasetOnHDR;
            // 
            // SalesReportDatasetOnHDR
            // 
            this.SalesReportDatasetOnHDR.DataSetName = "SalesReportDatasetOnHDR";
            this.SalesReportDatasetOnHDR.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // Cashier_reportBindingSource
            // 
            this.Cashier_reportBindingSource.DataMember = "Cashier_report";
            this.Cashier_reportBindingSource.DataSource = this.SaleDataNew;
            // 
            // SaleDataNew
            // 
            this.SaleDataNew.DataSetName = "SaleDataNew";
            this.SaleDataNew.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.reportViewer1);
            this.splitContainer1.Size = new System.Drawing.Size(1050, 594);
            this.splitContainer1.SplitterDistance = 109;
            this.splitContainer1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chk_date);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.Btn_Search);
            this.groupBox1.Controls.Add(this.dtp_to);
            this.groupBox1.Controls.Add(this.dtp_from);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cmb_sman);
            this.groupBox1.Location = new System.Drawing.Point(12, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1027, 90);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // chk_date
            // 
            this.chk_date.AutoSize = true;
            this.chk_date.ForeColor = System.Drawing.Color.Blue;
            this.chk_date.Location = new System.Drawing.Point(352, 17);
            this.chk_date.Name = "chk_date";
            this.chk_date.Size = new System.Drawing.Size(99, 17);
            this.chk_date.TabIndex = 13;
            this.chk_date.Text = "Sort by Date";
            this.chk_date.UseVisualStyleBackColor = true;
            this.chk_date.CheckedChanged += new System.EventHandler(this.chk_date_CheckedChanged);
            // 
            // label4
            // 
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Location = new System.Drawing.Point(636, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 21);
            this.label4.TabIndex = 5;
            this.label4.Text = "To";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Location = new System.Drawing.Point(352, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 21);
            this.label3.TabIndex = 4;
            this.label3.Text = "From";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Btn_Search
            // 
            this.Btn_Search.ButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.Alternate;
            this.Btn_Search.Location = new System.Drawing.Point(905, 38);
            this.Btn_Search.Name = "Btn_Search";
            this.Btn_Search.Size = new System.Drawing.Size(100, 25);
            this.Btn_Search.TabIndex = 12;
            this.Btn_Search.Values.Text = "Search";
            this.Btn_Search.Click += new System.EventHandler(this.Btn_Search_Click);
            // 
            // dtp_to
            // 
            this.dtp_to.Enabled = false;
            this.dtp_to.Location = new System.Drawing.Point(672, 40);
            this.dtp_to.Name = "dtp_to";
            this.dtp_to.Size = new System.Drawing.Size(220, 21);
            this.dtp_to.TabIndex = 2;
            // 
            // dtp_from
            // 
            this.dtp_from.Enabled = false;
            this.dtp_from.Location = new System.Drawing.Point(410, 40);
            this.dtp_from.Name = "dtp_from";
            this.dtp_from.Size = new System.Drawing.Size(220, 21);
            this.dtp_from.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Location = new System.Drawing.Point(18, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "Sales Man :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmb_sman
            // 
            this.cmb_sman.DisplayMember = "Emp_Fname";
            this.cmb_sman.FormattingEnabled = true;
            this.cmb_sman.Location = new System.Drawing.Point(110, 40);
            this.cmb_sman.Name = "cmb_sman";
            this.cmb_sman.Size = new System.Drawing.Size(220, 21);
            this.cmb_sman.TabIndex = 0;
            this.cmb_sman.ValueMember = "Empid";
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "SalesMan";
            reportDataSource1.Value = this.SalesmanReportBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Sys_Sols_Inventory.reports.Cashier report.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(1050, 481);
            this.reportViewer1.TabIndex = 0;
            // 
            // Salesman_report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1050, 594);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Salesman_report";
            this.Text = "Salesman_report";
            this.Load += new System.EventHandler(this.Salesman_report_Load);
            ((System.ComponentModel.ISupportInitialize)(this.SalesmanReportBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesReportDatasetOnHDR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cashier_reportBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SaleDataNew)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtp_to;
        private System.Windows.Forms.DateTimePicker dtp_from;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmb_sman;
        private ComponentFactory.Krypton.Toolkit.KryptonButton Btn_Search;
        private System.Windows.Forms.BindingSource Cashier_reportBindingSource;
        private SaleDataNew SaleDataNew;
        private System.Windows.Forms.BindingSource SalesmanReportBindingSource;
        private SalesReportDatasetOnHDR SalesReportDatasetOnHDR;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.CheckBox chk_date;
    }
}