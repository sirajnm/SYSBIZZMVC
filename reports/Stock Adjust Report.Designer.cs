namespace Sys_Sols_Inventory.reports
{
    partial class Stock_Adjust_Report
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
            this.StockADJCodeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.StockADJDS = new Sys_Sols_Inventory.reports.StockADJDS();
            this.panel1 = new System.Windows.Forms.Panel();
            this.drpBranch = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.Send_Mail = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.dataGridItem = new System.Windows.Forms.DataGridView();
            this.txtCode = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel7 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.DESC_ENG = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.btnSave = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonLabel5 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.EndDate = new System.Windows.Forms.DateTimePicker();
            this.StartDate = new System.Windows.Forms.DateTimePicker();
            this.kryptonLabel8 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel6 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.StockADJCodeTableAdapter = new Sys_Sols_Inventory.reports.StockADJDSTableAdapters.StockADJCodeTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.StockADJCodeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StockADJDS)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.drpBranch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridItem)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // StockADJCodeBindingSource
            // 
            this.StockADJCodeBindingSource.DataMember = "StockADJCode";
            this.StockADJCodeBindingSource.DataSource = this.StockADJDS;
            // 
            // StockADJDS
            // 
            this.StockADJDS.DataSetName = "StockADJDS";
            this.StockADJDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.drpBranch);
            this.panel1.Controls.Add(this.Send_Mail);
            this.panel1.Controls.Add(this.dataGridItem);
            this.panel1.Controls.Add(this.txtCode);
            this.panel1.Controls.Add(this.kryptonLabel1);
            this.panel1.Controls.Add(this.kryptonLabel7);
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Controls.Add(this.DESC_ENG);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.kryptonLabel5);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(857, 105);
            this.panel1.TabIndex = 27;
            // 
            // drpBranch
            // 
            this.drpBranch.DropDownWidth = 118;
            this.drpBranch.Items.AddRange(new object[] {
            "Below Stock Items",
            "Over Stock Iems",
            "Minimum Stock Items"});
            this.drpBranch.Location = new System.Drawing.Point(552, 12);
            this.drpBranch.Name = "drpBranch";
            this.drpBranch.Size = new System.Drawing.Size(247, 21);
            this.drpBranch.TabIndex = 123;
            this.drpBranch.Text = "--select--";
            // 
            // Send_Mail
            // 
            this.Send_Mail.Location = new System.Drawing.Point(746, 70);
            this.Send_Mail.Name = "Send_Mail";
            this.Send_Mail.Size = new System.Drawing.Size(66, 25);
            this.Send_Mail.TabIndex = 122;
            this.Send_Mail.Values.Text = "Send Mail";
            this.Send_Mail.Click += new System.EventHandler(this.Send_Mail_Click);
            // 
            // dataGridItem
            // 
            this.dataGridItem.AllowUserToDeleteRows = false;
            this.dataGridItem.BackgroundColor = System.Drawing.Color.PeachPuff;
            this.dataGridItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridItem.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridItem.GridColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dataGridItem.Location = new System.Drawing.Point(90, 34);
            this.dataGridItem.Name = "dataGridItem";
            this.dataGridItem.ReadOnly = true;
            this.dataGridItem.RowHeadersVisible = false;
            this.dataGridItem.Size = new System.Drawing.Size(351, 88);
            this.dataGridItem.TabIndex = 116;
            this.dataGridItem.Visible = false;
            this.dataGridItem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridItem_KeyDown);
            this.dataGridItem.Leave += new System.EventHandler(this.dataGridItem_Leave);
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(370, 13);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(107, 20);
            this.txtCode.TabIndex = 120;
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(495, 13);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(54, 20);
            this.kryptonLabel1.TabIndex = 121;
            this.kryptonLabel1.Values.Text = "Branch :";
            // 
            // kryptonLabel7
            // 
            this.kryptonLabel7.Location = new System.Drawing.Point(296, 12);
            this.kryptonLabel7.Name = "kryptonLabel7";
            this.kryptonLabel7.Size = new System.Drawing.Size(73, 20);
            this.kryptonLabel7.TabIndex = 121;
            this.kryptonLabel7.Values.Text = "Item Code :";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(13, 36);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(76, 17);
            this.checkBox1.TabIndex = 110;
            this.checkBox1.Text = "Date Wise";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged_1);
            // 
            // DESC_ENG
            // 
            this.DESC_ENG.Location = new System.Drawing.Point(89, 13);
            this.DESC_ENG.Name = "DESC_ENG";
            this.DESC_ENG.Size = new System.Drawing.Size(200, 20);
            this.DESC_ENG.TabIndex = 106;
            this.DESC_ENG.TextChanged += new System.EventHandler(this.DESC_ENG_TextChanged);
            this.DESC_ENG.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DESC_ENG_KeyDown);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(679, 70);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(61, 25);
            this.btnSave.TabIndex = 27;
            this.btnSave.Values.Text = "Search";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // kryptonLabel5
            // 
            this.kryptonLabel5.Location = new System.Drawing.Point(9, 13);
            this.kryptonLabel5.Name = "kryptonLabel5";
            this.kryptonLabel5.Size = new System.Drawing.Size(77, 20);
            this.kryptonLabel5.TabIndex = 105;
            this.kryptonLabel5.Values.Text = "Item Name :";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.EndDate);
            this.groupBox1.Controls.Add(this.StartDate);
            this.groupBox1.Controls.Add(this.kryptonLabel8);
            this.groupBox1.Controls.Add(this.kryptonLabel6);
            this.groupBox1.Enabled = false;
            this.groupBox1.Location = new System.Drawing.Point(13, 47);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(651, 48);
            this.groupBox1.TabIndex = 109;
            this.groupBox1.TabStop = false;
            // 
            // EndDate
            // 
            this.EndDate.Location = new System.Drawing.Point(434, 15);
            this.EndDate.Name = "EndDate";
            this.EndDate.Size = new System.Drawing.Size(200, 20);
            this.EndDate.TabIndex = 26;
            // 
            // StartDate
            // 
            this.StartDate.Location = new System.Drawing.Point(119, 15);
            this.StartDate.Name = "StartDate";
            this.StartDate.Size = new System.Drawing.Size(200, 20);
            this.StartDate.TabIndex = 24;
            // 
            // kryptonLabel8
            // 
            this.kryptonLabel8.Location = new System.Drawing.Point(348, 15);
            this.kryptonLabel8.Name = "kryptonLabel8";
            this.kryptonLabel8.Size = new System.Drawing.Size(63, 20);
            this.kryptonLabel8.TabIndex = 25;
            this.kryptonLabel8.Values.Text = "End Date:";
            // 
            // kryptonLabel6
            // 
            this.kryptonLabel6.Location = new System.Drawing.Point(18, 15);
            this.kryptonLabel6.Name = "kryptonLabel6";
            this.kryptonLabel6.Size = new System.Drawing.Size(71, 20);
            this.kryptonLabel6.TabIndex = 23;
            this.kryptonLabel6.Values.Text = "Start  Date:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.reportViewer1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 105);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(857, 424);
            this.panel2.TabIndex = 28;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "StockAdj";
            reportDataSource1.Value = this.StockADJCodeBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Sys_Sols_Inventory.reports.Report12.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.PageCountMode = Microsoft.Reporting.WinForms.PageCountMode.Actual;
            this.reportViewer1.Size = new System.Drawing.Size(857, 424);
            this.reportViewer1.TabIndex = 0;
            // 
            // StockADJCodeTableAdapter
            // 
            this.StockADJCodeTableAdapter.ClearBeforeFill = true;
            // 
            // Stock_Adjust_Report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(857, 529);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Stock_Adjust_Report";
            this.Text = "Stock Adjustment Report";
            this.Load += new System.EventHandler(this.Stock_Adjust_Report_Load);
            ((System.ComponentModel.ISupportInitialize)(this.StockADJCodeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StockADJDS)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.drpBranch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridItem)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSave;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox DESC_ENG;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel5;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker EndDate;
        private System.Windows.Forms.DateTimePicker StartDate;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel8;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel6;
        private System.Windows.Forms.DataGridView dataGridItem;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtCode;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel7;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource StockADJCodeBindingSource;
        private StockADJDS StockADJDS;
        private StockADJDSTableAdapters.StockADJCodeTableAdapter StockADJCodeTableAdapter;
        private ComponentFactory.Krypton.Toolkit.KryptonButton Send_Mail;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox drpBranch;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
    }
}