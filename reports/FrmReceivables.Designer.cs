namespace Sys_Sols_Inventory.reports
{
    partial class FrmReceivables
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmReceivables));
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dsReceivables = new Sys_Sols_Inventory.reports.dsReceivables();
            this.ReceivablesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ReceivablesTableAdapter = new Sys_Sols_Inventory.reports.dsReceivablesTableAdapters.ReceivablesTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.dsReceivables)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReceivablesBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Bottom;
            reportDataSource1.Name = "Receivables";
            reportDataSource1.Value = this.ReceivablesBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Sys_Sols_Inventory.reports.Receivables.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 34);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(617, 493);
            this.reportViewer1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(617, 28);
            this.panel1.TabIndex = 1;
            // 
            // dsReceivables
            // 
            this.dsReceivables.DataSetName = "dsReceivables";
            this.dsReceivables.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // ReceivablesBindingSource
            // 
            this.ReceivablesBindingSource.DataMember = "Receivables";
            this.ReceivablesBindingSource.DataSource = this.dsReceivables;
            // 
            // ReceivablesTableAdapter
            // 
            this.ReceivablesTableAdapter.ClearBeforeFill = true;
            // 
            // FrmReceivables
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(617, 527);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.reportViewer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmReceivables";
            this.Text = "Receivables";
            this.Load += new System.EventHandler(this.FrmReceivables_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dsReceivables)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReceivablesBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.BindingSource ReceivablesBindingSource;
        private dsReceivables dsReceivables;
        private dsReceivablesTableAdapters.ReceivablesTableAdapter ReceivablesTableAdapter;
    }
}