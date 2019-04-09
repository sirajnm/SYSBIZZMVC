namespace Sys_Sols_Inventory.reports
{
    partial class Pr
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
            this.Sale_ProfitBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.profit = new Sys_Sols_Inventory.profit();
            this.profitBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.Sale_ProfitBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.profit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.profitBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // Sale_ProfitBindingSource
            // 
            this.Sale_ProfitBindingSource.DataMember = "Sale_Profit";
            this.Sale_ProfitBindingSource.DataSource = this.profit;
            // 
            // profit
            // 
            this.profit.DataSetName = "profit";
            this.profit.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // profitBindingSource
            // 
            this.profitBindingSource.DataSource = this.profit;
            this.profitBindingSource.Position = 0;
            // 
            // reportViewer1
            // 
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.Sale_ProfitBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Sys_Sols_Inventory.reports.newprofit.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(12, 77);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(990, 477);
            this.reportViewer1.TabIndex = 0;
            // 
            // Pr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1014, 600);
            this.Controls.Add(this.reportViewer1);
            this.Name = "Pr";
            this.Text = "Pr";
            this.Load += new System.EventHandler(this.Pr_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Sale_ProfitBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.profit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.profitBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource profitBindingSource;
        private profit profit;
        private System.Windows.Forms.BindingSource Sale_ProfitBindingSource;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;

    }
}