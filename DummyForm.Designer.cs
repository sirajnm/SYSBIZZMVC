namespace Sys_Sols_Inventory
{
    partial class DummyForm
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
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.dummyDataset = new Sys_Sols_Inventory.dummyDataset();
            this.INV_ITEM_DIRECTORYBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.INV_ITEM_DIRECTORYTableAdapter = new Sys_Sols_Inventory.dummyDatasetTableAdapters.INV_ITEM_DIRECTORYTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.dummyDataset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.INV_ITEM_DIRECTORYBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.INV_ITEM_DIRECTORYBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Sys_Sols_Inventory.dummyReport.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(13, 13);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(624, 383);
            this.reportViewer1.TabIndex = 0;
            // 
            // dummyDataset
            // 
            this.dummyDataset.DataSetName = "dummyDataset";
            this.dummyDataset.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // INV_ITEM_DIRECTORYBindingSource
            // 
            this.INV_ITEM_DIRECTORYBindingSource.DataMember = "INV_ITEM_DIRECTORY";
            this.INV_ITEM_DIRECTORYBindingSource.DataSource = this.dummyDataset;
            // 
            // INV_ITEM_DIRECTORYTableAdapter
            // 
            this.INV_ITEM_DIRECTORYTableAdapter.ClearBeforeFill = true;
            // 
            // DummyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(649, 408);
            this.Controls.Add(this.reportViewer1);
            this.Name = "DummyForm";
            this.Text = "DummyForm";
            this.Load += new System.EventHandler(this.DummyForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dummyDataset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.INV_ITEM_DIRECTORYBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource INV_ITEM_DIRECTORYBindingSource;
        private dummyDataset dummyDataset;
        private dummyDatasetTableAdapters.INV_ITEM_DIRECTORYTableAdapter INV_ITEM_DIRECTORYTableAdapter;
    }
}