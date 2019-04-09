namespace Sys_Sols_Inventory.rpt
{
    partial class FrmTransactionsReport
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
            this.GetTransactionsWithDetailsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dsReport = new Sys_Sols_Inventory.rpt.dsReport();
            this.rptViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.GetTransactionsWithDetailsTableAdapter = new Sys_Sols_Inventory.rpt.dsReportTableAdapters.GetTransactionsWithDetailsTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.GetTransactionsWithDetailsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsReport)).BeginInit();
            this.SuspendLayout();
            // 
            // GetTransactionsWithDetailsBindingSource
            // 
            this.GetTransactionsWithDetailsBindingSource.DataMember = "GetTransactionsWithDetails";
            this.GetTransactionsWithDetailsBindingSource.DataSource = this.dsReport;
            // 
            // dsReport
            // 
            this.dsReport.DataSetName = "dsReport";
            this.dsReport.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // rptViewer
            // 
            this.rptViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            reportDataSource1.Name = "Transactions";
            reportDataSource1.Value = this.GetTransactionsWithDetailsBindingSource;
            this.rptViewer.LocalReport.DataSources.Add(reportDataSource1);
            this.rptViewer.LocalReport.ReportEmbeddedResource = "Sys_Sols_Inventory.rpt.rptReceivables.rdlc";
            this.rptViewer.Location = new System.Drawing.Point(12, 12);
            this.rptViewer.Name = "rptViewer";
            this.rptViewer.Size = new System.Drawing.Size(773, 475);
            this.rptViewer.TabIndex = 0;
            // 
            // GetTransactionsWithDetailsTableAdapter
            // 
            this.GetTransactionsWithDetailsTableAdapter.ClearBeforeFill = true;
            // 
            // FrmTransactionsReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(798, 500);
            this.Controls.Add(this.rptViewer);
            this.Name = "FrmTransactionsReport";
            this.Text = "FrmReport";
            this.Load += new System.EventHandler(this.FrmReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GetTransactionsWithDetailsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsReport)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer rptViewer;
        private System.Windows.Forms.BindingSource GetTransactionsWithDetailsBindingSource;
        private dsReport dsReport;
        private dsReportTableAdapters.GetTransactionsWithDetailsTableAdapter GetTransactionsWithDetailsTableAdapter;
    }
}