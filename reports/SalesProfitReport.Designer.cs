namespace Sys_Sols_Inventory.reports
{
    partial class SalesProfitReport
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
            this.StoredProcedure1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.SalesProfitDS = new Sys_Sols_Inventory.reports.SalesProfitDS();
            this.panel1 = new System.Windows.Forms.Panel();
            this.DRP_VARIATIONON = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.Send_Mail = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnSearch = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.Find = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.txtCode = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.Chk = new System.Windows.Forms.CheckBox();
            this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel7 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.paneldate = new System.Windows.Forms.Panel();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.DATE_TO = new System.Windows.Forms.DateTimePicker();
            this.DATE_FROM = new System.Windows.Forms.DateTimePicker();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.DG_REPORT = new System.Windows.Forms.DataGridView();
            this.StoredProcedure1TableAdapter = new Sys_Sols_Inventory.reports.SalesProfitDSTableAdapters.StoredProcedure1TableAdapter();
            this.DESC_ENG = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.dataGridItem = new System.Windows.Forms.DataGridView();
            this.panel_itemname = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.StoredProcedure1BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesProfitDS)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DRP_VARIATIONON)).BeginInit();
            this.panel3.SuspendLayout();
            this.paneldate.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DG_REPORT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridItem)).BeginInit();
            this.panel_itemname.SuspendLayout();
            this.SuspendLayout();
            // 
            // StoredProcedure1BindingSource
            // 
            this.StoredProcedure1BindingSource.DataMember = "StoredProcedure1";
            this.StoredProcedure1BindingSource.DataSource = this.SalesProfitDS;
            // 
            // SalesProfitDS
            // 
            this.SalesProfitDS.DataSetName = "SalesProfitDS";
            this.SalesProfitDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel_itemname);
            this.panel1.Controls.Add(this.DRP_VARIATIONON);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.Find);
            this.panel1.Controls.Add(this.txtCode);
            this.panel1.Controls.Add(this.Chk);
            this.panel1.Controls.Add(this.kryptonLabel4);
            this.panel1.Controls.Add(this.kryptonLabel7);
            this.panel1.Controls.Add(this.DESC_ENG);
            this.panel1.Controls.Add(this.kryptonLabel1);
            this.panel1.Controls.Add(this.paneldate);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(926, 130);
            this.panel1.TabIndex = 0;
            // 
            // DRP_VARIATIONON
            // 
            this.DRP_VARIATIONON.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DRP_VARIATIONON.DropDownWidth = 211;
            this.DRP_VARIATIONON.Items.AddRange(new object[] {
            "Purchase Price",
            "Last Purchase Rate",
            "Last Purchase Cost",
            "Last Sales Rate",
            "Last Sales Cost"});
            this.DRP_VARIATIONON.Location = new System.Drawing.Point(520, 55);
            this.DRP_VARIATIONON.Name = "DRP_VARIATIONON";
            this.DRP_VARIATIONON.Size = new System.Drawing.Size(211, 21);
            this.DRP_VARIATIONON.TabIndex = 114;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.reportViewer1);
            this.panel3.Controls.Add(this.Send_Mail);
            this.panel3.Controls.Add(this.btnSearch);
            this.panel3.Location = new System.Drawing.Point(815, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(90, 100);
            this.panel3.TabIndex = 113;
            this.panel3.Visible = false;
            // 
            // reportViewer1
            // 
            this.reportViewer1.DocumentMapWidth = 48;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.StoredProcedure1BindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Sys_Sols_Inventory.reports.Report8.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(19, 3);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(59, 35);
            this.reportViewer1.TabIndex = 0;
            // 
            // Send_Mail
            // 
            this.Send_Mail.Location = new System.Drawing.Point(13, 44);
            this.Send_Mail.Name = "Send_Mail";
            this.Send_Mail.Size = new System.Drawing.Size(46, 25);
            this.Send_Mail.TabIndex = 112;
            this.Send_Mail.Values.Text = "Send Mail";
            this.Send_Mail.Click += new System.EventHandler(this.kryptonButton1_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(13, 71);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(46, 25);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Values.Text = "Search";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // Find
            // 
            this.Find.Location = new System.Drawing.Point(661, 82);
            this.Find.Name = "Find";
            this.Find.Size = new System.Drawing.Size(70, 25);
            this.Find.TabIndex = 4;
            this.Find.Values.Text = "Search";
            this.Find.Click += new System.EventHandler(this.Find_Click);
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(520, 19);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(211, 20);
            this.txtCode.TabIndex = 1;
            this.txtCode.TextChanged += new System.EventHandler(this.txtCode_TextChanged);
            // 
            // Chk
            // 
            this.Chk.AutoSize = true;
            this.Chk.Location = new System.Drawing.Point(34, 56);
            this.Chk.Name = "Chk";
            this.Chk.Size = new System.Drawing.Size(99, 17);
            this.Chk.TabIndex = 2;
            this.Chk.Text = "Filter With Date";
            this.Chk.UseVisualStyleBackColor = true;
            this.Chk.CheckedChanged += new System.EventHandler(this.Chk_CheckedChanged);
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(437, 56);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(85, 20);
            this.kryptonLabel4.TabIndex = 111;
            this.kryptonLabel4.Values.Text = "Variation On :";
            // 
            // kryptonLabel7
            // 
            this.kryptonLabel7.Location = new System.Drawing.Point(437, 18);
            this.kryptonLabel7.Name = "kryptonLabel7";
            this.kryptonLabel7.Size = new System.Drawing.Size(73, 20);
            this.kryptonLabel7.TabIndex = 111;
            this.kryptonLabel7.Values.Text = "Item Code :";
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(34, 20);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(77, 20);
            this.kryptonLabel1.TabIndex = 110;
            this.kryptonLabel1.Values.Text = "Item Name :";
            // 
            // paneldate
            // 
            this.paneldate.Controls.Add(this.kryptonLabel2);
            this.paneldate.Controls.Add(this.DATE_TO);
            this.paneldate.Controls.Add(this.DATE_FROM);
            this.paneldate.Controls.Add(this.kryptonLabel3);
            this.paneldate.Enabled = false;
            this.paneldate.Location = new System.Drawing.Point(34, 75);
            this.paneldate.Name = "paneldate";
            this.paneldate.Size = new System.Drawing.Size(394, 44);
            this.paneldate.TabIndex = 3;
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(8, 9);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(70, 20);
            this.kryptonLabel2.TabIndex = 112;
            this.kryptonLabel2.Values.Text = "Date From:";
            // 
            // DATE_TO
            // 
            this.DATE_TO.CustomFormat = "dd/MM/yyyy";
            this.DATE_TO.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DATE_TO.Location = new System.Drawing.Point(251, 9);
            this.DATE_TO.Name = "DATE_TO";
            this.DATE_TO.Size = new System.Drawing.Size(98, 20);
            this.DATE_TO.TabIndex = 1;
            // 
            // DATE_FROM
            // 
            this.DATE_FROM.CustomFormat = "dd/MM/yyyy";
            this.DATE_FROM.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DATE_FROM.Location = new System.Drawing.Point(79, 9);
            this.DATE_FROM.Name = "DATE_FROM";
            this.DATE_FROM.Size = new System.Drawing.Size(98, 20);
            this.DATE_FROM.TabIndex = 0;
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(190, 9);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(56, 20);
            this.kryptonLabel3.TabIndex = 114;
            this.kryptonLabel3.Values.Text = "Date To:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 130);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(926, 357);
            this.panel2.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.DG_REPORT);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(10, 0, 20, 50);
            this.panel4.Size = new System.Drawing.Size(926, 357);
            this.panel4.TabIndex = 1;
            // 
            // DG_REPORT
            // 
            this.DG_REPORT.AllowUserToAddRows = false;
            this.DG_REPORT.AllowUserToDeleteRows = false;
            this.DG_REPORT.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DG_REPORT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DG_REPORT.Location = new System.Drawing.Point(10, 0);
            this.DG_REPORT.Name = "DG_REPORT";
            this.DG_REPORT.RowHeadersVisible = false;
            this.DG_REPORT.RowTemplate.Height = 25;
            this.DG_REPORT.Size = new System.Drawing.Size(896, 307);
            this.DG_REPORT.TabIndex = 0;
            this.DG_REPORT.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DG_REPORT_CellDoubleClick);
            // 
            // StoredProcedure1TableAdapter
            // 
            this.StoredProcedure1TableAdapter.ClearBeforeFill = true;
            // 
            // DESC_ENG
            // 
            this.DESC_ENG.Location = new System.Drawing.Point(111, 21);
            this.DESC_ENG.Name = "DESC_ENG";
            this.DESC_ENG.Size = new System.Drawing.Size(259, 20);
            this.DESC_ENG.TabIndex = 0;
            this.DESC_ENG.TextChanged += new System.EventHandler(this.DESC_ENG_TextChanged);
            this.DESC_ENG.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DESC_ENG_KeyDown);
            this.DESC_ENG.Leave += new System.EventHandler(this.DESC_ENG_Leave);
            // 
            // dataGridItem
            // 
            this.dataGridItem.AllowUserToDeleteRows = false;
            this.dataGridItem.BackgroundColor = System.Drawing.Color.PeachPuff;
            this.dataGridItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridItem.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridItem.GridColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dataGridItem.Location = new System.Drawing.Point(5, 3);
            this.dataGridItem.Name = "dataGridItem";
            this.dataGridItem.ReadOnly = true;
            this.dataGridItem.RowHeadersVisible = false;
            this.dataGridItem.Size = new System.Drawing.Size(295, 82);
            this.dataGridItem.TabIndex = 3;
            this.dataGridItem.Visible = false;
            this.dataGridItem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridItem_KeyDown);
            // 
            // panel_itemname
            // 
            this.panel_itemname.Controls.Add(this.dataGridItem);
            this.panel_itemname.Location = new System.Drawing.Point(113, 44);
            this.panel_itemname.Name = "panel_itemname";
            this.panel_itemname.Size = new System.Drawing.Size(314, 94);
            this.panel_itemname.TabIndex = 115;
            // 
            // SalesProfitReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(926, 487);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "SalesProfitReport";
            this.Text = "Sales Profit";
            this.Load += new System.EventHandler(this.SalesProfitReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.StoredProcedure1BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesProfitDS)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DRP_VARIATIONON)).EndInit();
            this.panel3.ResumeLayout(false);
            this.paneldate.ResumeLayout(false);
            this.paneldate.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DG_REPORT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridItem)).EndInit();
            this.panel_itemname.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox Chk;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtCode;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel7;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private System.Windows.Forms.DateTimePicker DATE_FROM;
        private System.Windows.Forms.DateTimePicker DATE_TO;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.BindingSource StoredProcedure1BindingSource;
        private SalesProfitDS SalesProfitDS;
        private SalesProfitDSTableAdapters.StoredProcedure1TableAdapter StoredProcedure1TableAdapter;
        private System.Windows.Forms.Panel paneldate;
        private System.Windows.Forms.Panel panel3;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton Send_Mail;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSearch;
        private ComponentFactory.Krypton.Toolkit.KryptonButton Find;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private System.Windows.Forms.DataGridView DG_REPORT;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox DRP_VARIATIONON;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel_itemname;
        private System.Windows.Forms.DataGridView dataGridItem;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox DESC_ENG;
    }
}