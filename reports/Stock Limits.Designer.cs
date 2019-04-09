namespace Sys_Sols_Inventory.reports
{
    partial class Stock_Limits
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
            this.MinimumStockBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.MinimumStockItems = new Sys_Sols_Inventory.reports.MinimumStockItems();
            this.drpselect = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.btnSearch = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Send_Mail = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.MinimumStockTableAdapter = new Sys_Sols_Inventory.reports.MinimumStockItemsTableAdapters.MinimumStockTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.MinimumStockBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinimumStockItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.drpselect)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // MinimumStockBindingSource
            // 
            this.MinimumStockBindingSource.DataMember = "MinimumStock";
            this.MinimumStockBindingSource.DataSource = this.MinimumStockItems;
            // 
            // MinimumStockItems
            // 
            this.MinimumStockItems.DataSetName = "MinimumStockItems";
            this.MinimumStockItems.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // drpselect
            // 
            this.drpselect.DropDownWidth = 118;
            this.drpselect.Items.AddRange(new object[] {
            "Below Stock Items",
            "Over Stock Iems",
            "Minimum Stock Items"});
            this.drpselect.Location = new System.Drawing.Point(185, 20);
            this.drpselect.Name = "drpselect";
            this.drpselect.Size = new System.Drawing.Size(247, 21);
            this.drpselect.TabIndex = 111;
            this.drpselect.Text = "--select--";
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(77, 21);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(102, 20);
            this.kryptonLabel3.TabIndex = 110;
            this.kryptonLabel3.Values.Text = "Select Category :";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(447, 16);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(93, 25);
            this.btnSearch.TabIndex = 112;
            this.btnSearch.Values.Text = "Generate";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "MinimumStock";
            reportDataSource1.Value = this.MinimumStockBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Sys_Sols_Inventory.reports.Report11.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(914, 400);
            this.reportViewer1.TabIndex = 113;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Send_Mail);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(914, 59);
            this.panel1.TabIndex = 114;
            // 
            // Send_Mail
            // 
            this.Send_Mail.Location = new System.Drawing.Point(546, 16);
            this.Send_Mail.Name = "Send_Mail";
            this.Send_Mail.Size = new System.Drawing.Size(90, 25);
            this.Send_Mail.TabIndex = 113;
            this.Send_Mail.Values.Text = "Send Mail";
            this.Send_Mail.Click += new System.EventHandler(this.Send_Mail_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.reportViewer1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 59);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(914, 400);
            this.panel2.TabIndex = 115;
            // 
            // MinimumStockTableAdapter
            // 
            this.MinimumStockTableAdapter.ClearBeforeFill = true;
            // 
            // Stock_Limits
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 459);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.drpselect);
            this.Controls.Add(this.kryptonLabel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Stock_Limits";
            this.Text = "Stock Limits";
            ((System.ComponentModel.ISupportInitialize)(this.MinimumStockBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinimumStockItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.drpselect)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonComboBox drpselect;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSearch;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.BindingSource MinimumStockBindingSource;
        private MinimumStockItems MinimumStockItems;
        private MinimumStockItemsTableAdapters.MinimumStockTableAdapter MinimumStockTableAdapter;
        private ComponentFactory.Krypton.Toolkit.KryptonButton Send_Mail;
    }
}