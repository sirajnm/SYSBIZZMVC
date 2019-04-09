namespace Sys_Sols_Inventory.reports
{
    partial class FrmPayables
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPayables));
            this.panel1 = new System.Windows.Forms.Panel();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.dsPayables = new Sys_Sols_Inventory.reports.dsPayables();
            this.PayablesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.PayablesTableAdapter = new Sys_Sols_Inventory.reports.dsPayablesTableAdapters.PayablesTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.dsPayables)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PayablesBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(701, 28);
            this.panel1.TabIndex = 2;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Bottom;
            reportDataSource1.Name = "dsPayables";
            reportDataSource1.Value = this.PayablesBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Sys_Sols_Inventory.reports.Payables.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 34);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(701, 544);
            this.reportViewer1.TabIndex = 3;
            // 
            // dsPayables
            // 
            this.dsPayables.DataSetName = "dsPayables";
            this.dsPayables.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // PayablesBindingSource
            // 
            this.PayablesBindingSource.DataMember = "Payables";
            this.PayablesBindingSource.DataSource = this.dsPayables;
            // 
            // PayablesTableAdapter
            // 
            this.PayablesTableAdapter.ClearBeforeFill = true;
            // 
            // FrmPayables
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(701, 578);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmPayables";
            this.Text = "Payables";
            this.Load += new System.EventHandler(this.FrmPayables_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dsPayables)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PayablesBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource PayablesBindingSource;
        private dsPayables dsPayables;
        private dsPayablesTableAdapters.PayablesTableAdapter PayablesTableAdapter;
    }
}