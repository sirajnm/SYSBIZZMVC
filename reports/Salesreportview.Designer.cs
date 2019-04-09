namespace Sys_Sols_Inventory.reports
{
    partial class Salesreportview
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource4 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource5 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource6 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.DataTable1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.salesDataSet = new Sys_Sols_Inventory.reports.salesDataSet();
            this.DataTable2BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.DataTable3BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.btnGenerate = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chksalestype = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.chkitemcode = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.chktotal = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.ckkuom = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.chkdiscount = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.chkItemname = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.chktype = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.ChkCategory = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.chksalestotal = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.ChkGroup = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.chksale = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.ChkTrademark = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.Drpselect = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelCustomer = new System.Windows.Forms.Panel();
            this.cbx_customer = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.paneldate = new System.Windows.Forms.Panel();
            this.Date_end = new System.Windows.Forms.DateTimePicker();
            this.Date_Start = new System.Windows.Forms.DateTimePicker();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cbx_productname = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.DataTable1TableAdapter = new Sys_Sols_Inventory.reports.salesDataSetTableAdapters.DataTable1TableAdapter();
            this.DataTable2TableAdapter = new Sys_Sols_Inventory.reports.salesDataSetTableAdapters.DataTable2TableAdapter();
            this.DataTable3TableAdapter = new Sys_Sols_Inventory.reports.salesDataSetTableAdapters.DataTable3TableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.DataTable1BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.salesDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataTable2BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataTable3BindingSource)).BeginInit();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Drpselect)).BeginInit();
            this.panel1.SuspendLayout();
            this.panelCustomer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbx_customer)).BeginInit();
            this.paneldate.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbx_productname)).BeginInit();
            this.SuspendLayout();
            // 
            // DataTable1BindingSource
            // 
            this.DataTable1BindingSource.DataMember = "DataTable1";
            this.DataTable1BindingSource.DataSource = this.salesDataSet;
            // 
            // salesDataSet
            // 
            this.salesDataSet.DataSetName = "salesDataSet";
            this.salesDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // DataTable2BindingSource
            // 
            this.DataTable2BindingSource.DataMember = "DataTable2";
            this.DataTable2BindingSource.DataSource = this.salesDataSet;
            // 
            // DataTable3BindingSource
            // 
            this.DataTable3BindingSource.DataMember = "DataTable3";
            this.DataTable3BindingSource.DataSource = this.salesDataSet;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.reportViewer1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 249);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(932, 236);
            this.panel2.TabIndex = 1;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource4.Name = "DataSet1";
            reportDataSource4.Value = this.DataTable1BindingSource;
            reportDataSource5.Name = "DataSet2";
            reportDataSource5.Value = this.DataTable2BindingSource;
            reportDataSource6.Name = "DataSet3";
            reportDataSource6.Value = this.DataTable3BindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource4);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource5);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource6);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Sys_Sols_Inventory.reports.salesreport.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(932, 236);
            this.reportViewer1.TabIndex = 0;
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(54, 26);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(55, 19);
            this.kryptonLabel3.TabIndex = 17;
            this.kryptonLabel3.Values.Text = "Select By";
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(615, 75);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(172, 25);
            this.btnGenerate.TabIndex = 19;
            this.btnGenerate.Values.Text = "Generate Report";
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chksalestype);
            this.groupBox1.Controls.Add(this.chkitemcode);
            this.groupBox1.Controls.Add(this.chktotal);
            this.groupBox1.Controls.Add(this.ckkuom);
            this.groupBox1.Controls.Add(this.chkdiscount);
            this.groupBox1.Controls.Add(this.btnGenerate);
            this.groupBox1.Controls.Add(this.chkItemname);
            this.groupBox1.Controls.Add(this.chktype);
            this.groupBox1.Controls.Add(this.ChkCategory);
            this.groupBox1.Controls.Add(this.chksalestotal);
            this.groupBox1.Controls.Add(this.ChkGroup);
            this.groupBox1.Controls.Add(this.chksale);
            this.groupBox1.Controls.Add(this.ChkTrademark);
            this.groupBox1.Location = new System.Drawing.Point(36, 92);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(884, 117);
            this.groupBox1.TabIndex = 37;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Fields in report";
            // 
            // chksalestype
            // 
            this.chksalestype.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.chksalestype.Location = new System.Drawing.Point(592, 19);
            this.chksalestype.Name = "chksalestype";
            this.chksalestype.Size = new System.Drawing.Size(76, 19);
            this.chksalestype.TabIndex = 37;
            this.chksalestype.Text = "Sales Type";
            this.chksalestype.Values.Text = "Sales Type";
            // 
            // chkitemcode
            // 
            this.chkitemcode.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.chkitemcode.Location = new System.Drawing.Point(40, 19);
            this.chkitemcode.Name = "chkitemcode";
            this.chkitemcode.Size = new System.Drawing.Size(73, 19);
            this.chkitemcode.TabIndex = 34;
            this.chkitemcode.Text = "Item code";
            this.chkitemcode.Values.Text = "Item code";
            // 
            // chktotal
            // 
            this.chktotal.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.chktotal.Location = new System.Drawing.Point(483, 44);
            this.chktotal.Name = "chktotal";
            this.chktotal.Size = new System.Drawing.Size(48, 19);
            this.chktotal.TabIndex = 35;
            this.chktotal.Text = "Total";
            this.chktotal.Values.Text = "Total";
            // 
            // ckkuom
            // 
            this.ckkuom.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.ckkuom.Location = new System.Drawing.Point(234, 19);
            this.ckkuom.Name = "ckkuom";
            this.ckkuom.Size = new System.Drawing.Size(105, 19);
            this.ckkuom.TabIndex = 36;
            this.ckkuom.Text = "Unit Of Measure";
            this.ckkuom.Values.Text = "Unit Of Measure";
            // 
            // chkdiscount
            // 
            this.chkdiscount.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.chkdiscount.Location = new System.Drawing.Point(483, 19);
            this.chkdiscount.Name = "chkdiscount";
            this.chkdiscount.Size = new System.Drawing.Size(67, 19);
            this.chkdiscount.TabIndex = 33;
            this.chkdiscount.Text = "Discount";
            this.chkdiscount.Values.Text = "Discount";
            // 
            // chkItemname
            // 
            this.chkItemname.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.chkItemname.Location = new System.Drawing.Point(703, 19);
            this.chkItemname.Name = "chkItemname";
            this.chkItemname.Size = new System.Drawing.Size(74, 19);
            this.chkItemname.TabIndex = 27;
            this.chkItemname.Text = "Itemname";
            this.chkItemname.Values.Text = "Itemname";
            // 
            // chktype
            // 
            this.chktype.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.chktype.Location = new System.Drawing.Point(136, 19);
            this.chktype.Name = "chktype";
            this.chktype.Size = new System.Drawing.Size(73, 19);
            this.chktype.TabIndex = 28;
            this.chktype.Text = "Item Type";
            this.chktype.Values.Text = "Item Type";
            // 
            // ChkCategory
            // 
            this.ChkCategory.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.ChkCategory.Location = new System.Drawing.Point(40, 44);
            this.ChkCategory.Name = "ChkCategory";
            this.ChkCategory.Size = new System.Drawing.Size(68, 19);
            this.ChkCategory.TabIndex = 29;
            this.ChkCategory.Text = "Category";
            this.ChkCategory.Values.Text = "Category";
            // 
            // chksalestotal
            // 
            this.chksalestotal.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.chksalestotal.Location = new System.Drawing.Point(363, 44);
            this.chksalestotal.Name = "chksalestotal";
            this.chksalestotal.Size = new System.Drawing.Size(76, 19);
            this.chksalestotal.TabIndex = 31;
            this.chksalestotal.Text = "Total Sales";
            this.chksalestotal.Values.Text = "Total Sales";
            // 
            // ChkGroup
            // 
            this.ChkGroup.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.ChkGroup.Location = new System.Drawing.Point(136, 44);
            this.ChkGroup.Name = "ChkGroup";
            this.ChkGroup.Size = new System.Drawing.Size(55, 19);
            this.ChkGroup.TabIndex = 26;
            this.ChkGroup.Text = "Group";
            this.ChkGroup.Values.Text = "Group";
            // 
            // chksale
            // 
            this.chksale.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.chksale.Location = new System.Drawing.Point(234, 44);
            this.chksale.Name = "chksale";
            this.chksale.Size = new System.Drawing.Size(95, 19);
            this.chksale.TabIndex = 30;
            this.chksale.Text = "Sales Quantity";
            this.chksale.Values.Text = "Sales Quantity";
            // 
            // ChkTrademark
            // 
            this.ChkTrademark.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.ChkTrademark.Location = new System.Drawing.Point(362, 19);
            this.ChkTrademark.Name = "ChkTrademark";
            this.ChkTrademark.Size = new System.Drawing.Size(77, 19);
            this.ChkTrademark.TabIndex = 25;
            this.ChkTrademark.Text = "TradeMark";
            this.ChkTrademark.Values.Text = "TradeMark";
            // 
            // Drpselect
            // 
            this.Drpselect.DropDownWidth = 118;
            this.Drpselect.Items.AddRange(new object[] {
            "Product",
            "Date",
            "Customer"});
            this.Drpselect.Location = new System.Drawing.Point(150, 26);
            this.Drpselect.Name = "Drpselect";
            this.Drpselect.Size = new System.Drawing.Size(132, 21);
            this.Drpselect.TabIndex = 106;
            this.Drpselect.Text = "--select--";
            this.Drpselect.SelectedIndexChanged += new System.EventHandler(this.Drpselect_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panelCustomer);
            this.panel1.Controls.Add(this.paneldate);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.Drpselect);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.kryptonLabel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(932, 249);
            this.panel1.TabIndex = 0;
            // 
            // panelCustomer
            // 
            this.panelCustomer.Controls.Add(this.cbx_customer);
            this.panelCustomer.Location = new System.Drawing.Point(304, 9);
            this.panelCustomer.Name = "panelCustomer";
            this.panelCustomer.Size = new System.Drawing.Size(605, 71);
            this.panelCustomer.TabIndex = 45;
            this.panelCustomer.Visible = false;
            this.panelCustomer.VisibleChanged += new System.EventHandler(this.panelCustomer_VisibleChanged);
            // 
            // cbx_customer
            // 
            this.cbx_customer.DropDownWidth = 118;
            this.cbx_customer.Location = new System.Drawing.Point(32, 16);
            this.cbx_customer.Name = "cbx_customer";
            this.cbx_customer.Size = new System.Drawing.Size(198, 21);
            this.cbx_customer.TabIndex = 108;
            this.cbx_customer.Text = "--select--";
            // 
            // paneldate
            // 
            this.paneldate.Controls.Add(this.Date_end);
            this.paneldate.Controls.Add(this.Date_Start);
            this.paneldate.Controls.Add(this.kryptonLabel2);
            this.paneldate.Controls.Add(this.kryptonLabel1);
            this.paneldate.Location = new System.Drawing.Point(291, 9);
            this.paneldate.Name = "paneldate";
            this.paneldate.Size = new System.Drawing.Size(584, 74);
            this.paneldate.TabIndex = 109;
            this.paneldate.Visible = false;
            // 
            // Date_end
            // 
            this.Date_end.CustomFormat = "dd/MM/yyyy";
            this.Date_end.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Date_end.Location = new System.Drawing.Point(329, 16);
            this.Date_end.Name = "Date_end";
            this.Date_end.Size = new System.Drawing.Size(135, 20);
            this.Date_end.TabIndex = 44;
            // 
            // Date_Start
            // 
            this.Date_Start.CustomFormat = "dd/MM/yyyy";
            this.Date_Start.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Date_Start.Location = new System.Drawing.Point(95, 16);
            this.Date_Start.Name = "Date_Start";
            this.Date_Start.Size = new System.Drawing.Size(135, 20);
            this.Date_Start.TabIndex = 43;
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(267, 17);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(56, 19);
            this.kryptonLabel2.TabIndex = 19;
            this.kryptonLabel2.Values.Text = "End Date";
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(13, 16);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(60, 19);
            this.kryptonLabel1.TabIndex = 18;
            this.kryptonLabel1.Values.Text = "Start Date";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.cbx_productname);
            this.panel3.Location = new System.Drawing.Point(288, 12);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(587, 74);
            this.panel3.TabIndex = 107;
            this.panel3.Visible = false;
            this.panel3.VisibleChanged += new System.EventHandler(this.panel3_VisibleChanged);
            // 
            // cbx_productname
            // 
            this.cbx_productname.DropDownWidth = 118;
            this.cbx_productname.Location = new System.Drawing.Point(13, 14);
            this.cbx_productname.Name = "cbx_productname";
            this.cbx_productname.Size = new System.Drawing.Size(132, 21);
            this.cbx_productname.TabIndex = 107;
            this.cbx_productname.Text = "--select--";
            // 
            // DataTable1TableAdapter
            // 
            this.DataTable1TableAdapter.ClearBeforeFill = true;
            // 
            // DataTable2TableAdapter
            // 
            this.DataTable2TableAdapter.ClearBeforeFill = true;
            // 
            // DataTable3TableAdapter
            // 
            this.DataTable3TableAdapter.ClearBeforeFill = true;
            // 
            // Salesreportview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(932, 485);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Salesreportview";
            this.Text = "Salesreportview";
            this.Load += new System.EventHandler(this.Salesreportview_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataTable1BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.salesDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataTable2BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataTable3BindingSource)).EndInit();
            this.panel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Drpselect)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelCustomer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cbx_customer)).EndInit();
            this.paneldate.ResumeLayout(false);
            this.paneldate.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cbx_productname)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.BindingSource DataTable1BindingSource;
        private salesDataSet salesDataSet;
        private salesDataSetTableAdapters.DataTable1TableAdapter DataTable1TableAdapter;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnGenerate;
        private System.Windows.Forms.GroupBox groupBox1;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox chkitemcode;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox chktotal;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox ckkuom;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox chkdiscount;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox chkItemname;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox chktype;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox ChkCategory;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox chksalestotal;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox ChkGroup;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox chksale;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox ChkTrademark;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox Drpselect;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.BindingSource DataTable2BindingSource;
        private salesDataSetTableAdapters.DataTable2TableAdapter DataTable2TableAdapter;
        private System.Windows.Forms.Panel panel3;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cbx_productname;
        private System.Windows.Forms.Panel paneldate;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private System.Windows.Forms.DateTimePicker Date_end;
        private System.Windows.Forms.DateTimePicker Date_Start;
        private System.Windows.Forms.BindingSource DataTable3BindingSource;
        private salesDataSetTableAdapters.DataTable3TableAdapter DataTable3TableAdapter;
        private System.Windows.Forms.Panel panelCustomer;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cbx_customer;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox chksalestype;
    }
}