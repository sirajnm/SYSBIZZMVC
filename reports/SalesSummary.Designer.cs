namespace Sys_Sols_Inventory.reports
{
    partial class SalesSummary
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
            this.Sp_SummarySalesAllBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.SalesSummaryEach = new Sys_Sols_Inventory.reports.SalesSummaryEach();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridItem = new System.Windows.Forms.DataGridView();
            this.btnSearch = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.txtCode = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.Chk = new System.Windows.Forms.CheckBox();
            this.kryptonLabel7 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.DESC_ENG = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.paneldate = new System.Windows.Forms.Panel();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.DATE_TO = new System.Windows.Forms.DateTimePicker();
            this.DATE_FROM = new System.Windows.Forms.DateTimePicker();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.Sp_SummarySalesAllTableAdapter = new Sys_Sols_Inventory.reports.SalesSummaryEachTableAdapters.Sp_SummarySalesAllTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.Sp_SummarySalesAllBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesSummaryEach)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridItem)).BeginInit();
            this.paneldate.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // Sp_SummarySalesAllBindingSource
            // 
            this.Sp_SummarySalesAllBindingSource.DataMember = "Sp_SummarySalesAll";
            this.Sp_SummarySalesAllBindingSource.DataSource = this.SalesSummaryEach;
            // 
            // SalesSummaryEach
            // 
            this.SalesSummaryEach.DataSetName = "SalesSummaryEach";
            this.SalesSummaryEach.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dataGridItem);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.txtCode);
            this.panel1.Controls.Add(this.Chk);
            this.panel1.Controls.Add(this.kryptonLabel7);
            this.panel1.Controls.Add(this.DESC_ENG);
            this.panel1.Controls.Add(this.kryptonLabel1);
            this.panel1.Controls.Add(this.paneldate);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(808, 127);
            this.panel1.TabIndex = 0;
            // 
            // dataGridItem
            // 
            this.dataGridItem.AllowUserToDeleteRows = false;
            this.dataGridItem.BackgroundColor = System.Drawing.Color.PeachPuff;
            this.dataGridItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridItem.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridItem.GridColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dataGridItem.Location = new System.Drawing.Point(89, 36);
            this.dataGridItem.Name = "dataGridItem";
            this.dataGridItem.ReadOnly = true;
            this.dataGridItem.RowHeadersVisible = false;
            this.dataGridItem.Size = new System.Drawing.Size(310, 82);
            this.dataGridItem.TabIndex = 115;
            this.dataGridItem.Visible = false;
            this.dataGridItem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridItem_KeyDown);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(541, 69);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(90, 25);
            this.btnSearch.TabIndex = 117;
            this.btnSearch.Values.Text = "Search";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(442, 17);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(267, 20);
            this.txtCode.TabIndex = 113;
            // 
            // Chk
            // 
            this.Chk.AutoSize = true;
            this.Chk.Location = new System.Drawing.Point(12, 50);
            this.Chk.Name = "Chk";
            this.Chk.Size = new System.Drawing.Size(99, 17);
            this.Chk.TabIndex = 114;
            this.Chk.Text = "Filter With Date";
            this.Chk.UseVisualStyleBackColor = true;
            this.Chk.CheckedChanged += new System.EventHandler(this.Chk_CheckedChanged);
            // 
            // kryptonLabel7
            // 
            this.kryptonLabel7.Location = new System.Drawing.Point(368, 16);
            this.kryptonLabel7.Name = "kryptonLabel7";
            this.kryptonLabel7.Size = new System.Drawing.Size(68, 19);
            this.kryptonLabel7.TabIndex = 119;
            this.kryptonLabel7.Values.Text = "Item Code :";
            // 
            // DESC_ENG
            // 
            this.DESC_ENG.Location = new System.Drawing.Point(89, 15);
            this.DESC_ENG.Name = "DESC_ENG";
            this.DESC_ENG.Size = new System.Drawing.Size(259, 20);
            this.DESC_ENG.TabIndex = 112;
            this.DESC_ENG.TextChanged += new System.EventHandler(this.DESC_ENG_TextChanged);
            this.DESC_ENG.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DESC_ENG_KeyDown);
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(12, 14);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(71, 19);
            this.kryptonLabel1.TabIndex = 118;
            this.kryptonLabel1.Values.Text = "Item Name :";
            // 
            // paneldate
            // 
            this.paneldate.Controls.Add(this.kryptonLabel2);
            this.paneldate.Controls.Add(this.DATE_TO);
            this.paneldate.Controls.Add(this.DATE_FROM);
            this.paneldate.Controls.Add(this.kryptonLabel3);
            this.paneldate.Enabled = false;
            this.paneldate.Location = new System.Drawing.Point(12, 69);
            this.paneldate.Name = "paneldate";
            this.paneldate.Size = new System.Drawing.Size(394, 44);
            this.paneldate.TabIndex = 116;
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(8, 9);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(65, 19);
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
            this.kryptonLabel3.Size = new System.Drawing.Size(52, 19);
            this.kryptonLabel3.TabIndex = 114;
            this.kryptonLabel3.Values.Text = "Date To:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.reportViewer1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 127);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(808, 315);
            this.panel2.TabIndex = 1;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.Sp_SummarySalesAllBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Sys_Sols_Inventory.reports.Report10.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(808, 315);
            this.reportViewer1.TabIndex = 0;
            // 
            // Sp_SummarySalesAllTableAdapter
            // 
            this.Sp_SummarySalesAllTableAdapter.ClearBeforeFill = true;
            // 
            // SalesSummary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(808, 442);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "SalesSummary";
            this.Text = "SalesSummary";
            this.Load += new System.EventHandler(this.SalesSummary_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Sp_SummarySalesAllBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesSummaryEach)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridItem)).EndInit();
            this.paneldate.ResumeLayout(false);
            this.paneldate.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridItem;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSearch;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtCode;
        private System.Windows.Forms.CheckBox Chk;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel7;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox DESC_ENG;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private System.Windows.Forms.Panel paneldate;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private System.Windows.Forms.DateTimePicker DATE_TO;
        private System.Windows.Forms.DateTimePicker DATE_FROM;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private System.Windows.Forms.Panel panel2;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource Sp_SummarySalesAllBindingSource;
        private SalesSummaryEach SalesSummaryEach;
        private SalesSummaryEachTableAdapters.Sp_SummarySalesAllTableAdapter Sp_SummarySalesAllTableAdapter;
    }
}