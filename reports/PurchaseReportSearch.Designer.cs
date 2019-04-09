namespace Sys_Sols_Inventory.reports
{
    partial class PurchaseReportSearch
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource34 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource35 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource36 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.panel1 = new System.Windows.Forms.Panel();
            this.drpselect = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.btnGenerate = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkgroup = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.cbx_Purchasediscount = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.chkItemname = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.chkpurchasetype = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.chktype = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.ChkTrademark = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.chkpurchasetotal = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.ChkCategory = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.chkpurchase = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.ChkUom = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.panelproduct = new System.Windows.Forms.Panel();
            this.cbx_product = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.cbx_customer = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.panelCustomer = new System.Windows.Forms.Panel();
            this.PurchaseDataSet = new Sys_Sols_Inventory.reports.PurchaseDataSet();
            this.DataTable1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.DataTable1TableAdapter = new Sys_Sols_Inventory.reports.PurchaseDataSetTableAdapters.DataTable1TableAdapter();
            this.DataTable2BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.DataTable2TableAdapter = new Sys_Sols_Inventory.reports.PurchaseDataSetTableAdapters.DataTable2TableAdapter();
            this.DataTable3BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.DataTable3TableAdapter = new Sys_Sols_Inventory.reports.PurchaseDataSetTableAdapters.DataTable3TableAdapter();
            this.paneldate = new System.Windows.Forms.Panel();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.Date_Start = new System.Windows.Forms.DateTimePicker();
            this.Date_end = new System.Windows.Forms.DateTimePicker();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.drpselect)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panelproduct.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbx_product)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbx_customer)).BeginInit();
            this.panelCustomer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PurchaseDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataTable1BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataTable2BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataTable3BindingSource)).BeginInit();
            this.paneldate.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panelCustomer);
            this.panel1.Controls.Add(this.paneldate);
            this.panel1.Controls.Add(this.panelproduct);
            this.panel1.Controls.Add(this.drpselect);
            this.panel1.Controls.Add(this.kryptonLabel3);
            this.panel1.Controls.Add(this.btnGenerate);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(865, 249);
            this.panel1.TabIndex = 0;
            // 
            // drpselect
            // 
            this.drpselect.DropDownWidth = 118;
            this.drpselect.Items.AddRange(new object[] {
            "Product",
            "Supplier",
            "Date"});
            this.drpselect.Location = new System.Drawing.Point(141, 38);
            this.drpselect.Name = "drpselect";
            this.drpselect.Size = new System.Drawing.Size(159, 21);
            this.drpselect.TabIndex = 109;
            this.drpselect.Text = "--select--";
            this.drpselect.SelectedIndexChanged += new System.EventHandler(this.drpselect_SelectedIndexChanged);
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(63, 40);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(55, 19);
            this.kryptonLabel3.TabIndex = 26;
            this.kryptonLabel3.Values.Text = "Select By";
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(338, 203);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(172, 25);
            this.btnGenerate.TabIndex = 28;
            this.btnGenerate.Values.Text = "Generate Report";
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkgroup);
            this.groupBox1.Controls.Add(this.cbx_Purchasediscount);
            this.groupBox1.Controls.Add(this.chkItemname);
            this.groupBox1.Controls.Add(this.chkpurchasetype);
            this.groupBox1.Controls.Add(this.chktype);
            this.groupBox1.Controls.Add(this.ChkTrademark);
            this.groupBox1.Controls.Add(this.chkpurchasetotal);
            this.groupBox1.Controls.Add(this.ChkCategory);
            this.groupBox1.Controls.Add(this.chkpurchase);
            this.groupBox1.Controls.Add(this.ChkUom);
            this.groupBox1.Location = new System.Drawing.Point(22, 103);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(831, 94);
            this.groupBox1.TabIndex = 27;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Fields in the report";
            // 
            // chkgroup
            // 
            this.chkgroup.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.chkgroup.Location = new System.Drawing.Point(290, 54);
            this.chkgroup.Name = "chkgroup";
            this.chkgroup.Size = new System.Drawing.Size(53, 19);
            this.chkgroup.TabIndex = 27;
            this.chkgroup.Text = "group";
            this.chkgroup.Values.Text = "group";
            // 
            // cbx_Purchasediscount
            // 
            this.cbx_Purchasediscount.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.cbx_Purchasediscount.Location = new System.Drawing.Point(608, 29);
            this.cbx_Purchasediscount.Name = "cbx_Purchasediscount";
            this.cbx_Purchasediscount.Size = new System.Drawing.Size(115, 19);
            this.cbx_Purchasediscount.TabIndex = 26;
            this.cbx_Purchasediscount.Text = "Purchase Discount";
            this.cbx_Purchasediscount.Values.Text = "Purchase Discount";
            // 
            // chkItemname
            // 
            this.chkItemname.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.chkItemname.Location = new System.Drawing.Point(41, 29);
            this.chkItemname.Name = "chkItemname";
            this.chkItemname.Size = new System.Drawing.Size(74, 19);
            this.chkItemname.TabIndex = 20;
            this.chkItemname.Text = "Itemname";
            this.chkItemname.Values.Text = "Itemname";
            // 
            // chkpurchasetype
            // 
            this.chkpurchasetype.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.chkpurchasetype.Location = new System.Drawing.Point(442, 29);
            this.chkpurchasetype.Name = "chkpurchasetype";
            this.chkpurchasetype.Size = new System.Drawing.Size(95, 19);
            this.chkpurchasetype.TabIndex = 25;
            this.chkpurchasetype.Text = "Purchase Type";
            this.chkpurchasetype.Values.Text = "Purchase Type";
            // 
            // chktype
            // 
            this.chktype.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.chktype.Location = new System.Drawing.Point(41, 54);
            this.chktype.Name = "chktype";
            this.chktype.Size = new System.Drawing.Size(73, 19);
            this.chktype.TabIndex = 21;
            this.chktype.Text = "Item Type";
            this.chktype.Values.Text = "Item Type";
            // 
            // ChkTrademark
            // 
            this.ChkTrademark.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.ChkTrademark.Location = new System.Drawing.Point(290, 29);
            this.ChkTrademark.Name = "ChkTrademark";
            this.ChkTrademark.Size = new System.Drawing.Size(77, 19);
            this.ChkTrademark.TabIndex = 19;
            this.ChkTrademark.Text = "TradeMark";
            this.ChkTrademark.Values.Text = "TradeMark";
            // 
            // chkpurchasetotal
            // 
            this.chkpurchasetotal.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.chkpurchasetotal.Location = new System.Drawing.Point(608, 54);
            this.chkpurchasetotal.Name = "chkpurchasetotal";
            this.chkpurchasetotal.Size = new System.Drawing.Size(96, 19);
            this.chkpurchasetotal.TabIndex = 24;
            this.chkpurchasetotal.Text = "Total Purchase";
            this.chkpurchasetotal.Values.Text = "Total Purchase";
            // 
            // ChkCategory
            // 
            this.ChkCategory.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.ChkCategory.Location = new System.Drawing.Point(149, 29);
            this.ChkCategory.Name = "ChkCategory";
            this.ChkCategory.Size = new System.Drawing.Size(68, 19);
            this.ChkCategory.TabIndex = 22;
            this.ChkCategory.Text = "Category";
            this.ChkCategory.Values.Text = "Category";
            // 
            // chkpurchase
            // 
            this.chkpurchase.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.chkpurchase.Location = new System.Drawing.Point(442, 54);
            this.chkpurchase.Name = "chkpurchase";
            this.chkpurchase.Size = new System.Drawing.Size(114, 19);
            this.chkpurchase.TabIndex = 23;
            this.chkpurchase.Text = "Purchase Quantity";
            this.chkpurchase.Values.Text = "Purchase Quantity";
            // 
            // ChkUom
            // 
            this.ChkUom.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.ChkUom.Location = new System.Drawing.Point(149, 54);
            this.ChkUom.Name = "ChkUom";
            this.ChkUom.Size = new System.Drawing.Size(105, 19);
            this.ChkUom.TabIndex = 20;
            this.ChkUom.Text = "Unit Of Measure";
            this.ChkUom.Values.Text = "Unit Of Measure";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.reportViewer1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 249);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(865, 88);
            this.panel2.TabIndex = 1;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource34.Name = "DataSet1";
            reportDataSource34.Value = this.DataTable1BindingSource;
            reportDataSource35.Name = "DataSet2";
            reportDataSource35.Value = this.DataTable2BindingSource;
            reportDataSource36.Name = "DataSet3";
            reportDataSource36.Value = this.DataTable3BindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource34);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource35);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource36);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Sys_Sols_Inventory.reports.reportgeneration.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(865, 88);
            this.reportViewer1.TabIndex = 0;
            // 
            // panelproduct
            // 
            this.panelproduct.Controls.Add(this.cbx_product);
            this.panelproduct.Location = new System.Drawing.Point(312, 22);
            this.panelproduct.Name = "panelproduct";
            this.panelproduct.Size = new System.Drawing.Size(490, 65);
            this.panelproduct.TabIndex = 110;
            this.panelproduct.Visible = false;
            this.panelproduct.VisibleChanged += new System.EventHandler(this.panelproduct_VisibleChanged);
            // 
            // cbx_product
            // 
            this.cbx_product.DropDownWidth = 118;
            this.cbx_product.Location = new System.Drawing.Point(26, 16);
            this.cbx_product.Name = "cbx_product";
            this.cbx_product.Size = new System.Drawing.Size(198, 21);
            this.cbx_product.TabIndex = 109;
            // 
            // cbx_customer
            // 
            this.cbx_customer.DropDownWidth = 118;
            this.cbx_customer.Items.AddRange(new object[] {
            "Product",
            "Purchase Type"});
            this.cbx_customer.Location = new System.Drawing.Point(26, 16);
            this.cbx_customer.Name = "cbx_customer";
            this.cbx_customer.Size = new System.Drawing.Size(198, 21);
            this.cbx_customer.TabIndex = 111;
            // 
            // panelCustomer
            // 
            this.panelCustomer.Controls.Add(this.cbx_customer);
            this.panelCustomer.Location = new System.Drawing.Point(315, 19);
            this.panelCustomer.Name = "panelCustomer";
            this.panelCustomer.Size = new System.Drawing.Size(538, 65);
            this.panelCustomer.TabIndex = 110;
            this.panelCustomer.Visible = false;
            this.panelCustomer.VisibleChanged += new System.EventHandler(this.panelCustomer_VisibleChanged);
            // 
            // PurchaseDataSet
            // 
            this.PurchaseDataSet.DataSetName = "PurchaseDataSet";
            this.PurchaseDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // DataTable1BindingSource
            // 
            this.DataTable1BindingSource.DataMember = "DataTable1";
            this.DataTable1BindingSource.DataSource = this.PurchaseDataSet;
            // 
            // DataTable1TableAdapter
            // 
            this.DataTable1TableAdapter.ClearBeforeFill = true;
            // 
            // DataTable2BindingSource
            // 
            this.DataTable2BindingSource.DataMember = "DataTable2";
            this.DataTable2BindingSource.DataSource = this.PurchaseDataSet;
            // 
            // DataTable2TableAdapter
            // 
            this.DataTable2TableAdapter.ClearBeforeFill = true;
            // 
            // DataTable3BindingSource
            // 
            this.DataTable3BindingSource.DataMember = "DataTable3";
            this.DataTable3BindingSource.DataSource = this.PurchaseDataSet;
            // 
            // DataTable3TableAdapter
            // 
            this.DataTable3TableAdapter.ClearBeforeFill = true;
            // 
            // paneldate
            // 
            this.paneldate.Controls.Add(this.Date_end);
            this.paneldate.Controls.Add(this.Date_Start);
            this.paneldate.Controls.Add(this.kryptonLabel2);
            this.paneldate.Controls.Add(this.kryptonLabel1);
            this.paneldate.Location = new System.Drawing.Point(306, 19);
            this.paneldate.Name = "paneldate";
            this.paneldate.Size = new System.Drawing.Size(520, 65);
            this.paneldate.TabIndex = 112;
            this.paneldate.Visible = false;
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(26, 18);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(60, 19);
            this.kryptonLabel1.TabIndex = 27;
            this.kryptonLabel1.Values.Text = "Start Date";
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(257, 18);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(56, 19);
            this.kryptonLabel2.TabIndex = 28;
            this.kryptonLabel2.Values.Text = "End Date";
            // 
            // Date_Start
            // 
            this.Date_Start.CustomFormat = "dd/MM/yyyy";
            this.Date_Start.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Date_Start.Location = new System.Drawing.Point(100, 18);
            this.Date_Start.Name = "Date_Start";
            this.Date_Start.Size = new System.Drawing.Size(135, 20);
            this.Date_Start.TabIndex = 44;
            // 
            // Date_end
            // 
            this.Date_end.CustomFormat = "dd/MM/yyyy";
            this.Date_end.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Date_end.Location = new System.Drawing.Point(319, 18);
            this.Date_end.Name = "Date_end";
            this.Date_end.Size = new System.Drawing.Size(135, 20);
            this.Date_end.TabIndex = 45;
            // 
            // PurchaseReportSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(865, 337);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "PurchaseReportSearch";
            this.Text = "PurchaseReportSearch";
            this.Load += new System.EventHandler(this.PurchaseReportSearch_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.drpselect)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panelproduct.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cbx_product)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbx_customer)).EndInit();
            this.panelCustomer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PurchaseDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataTable1BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataTable2BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataTable3BindingSource)).EndInit();
            this.paneldate.ResumeLayout(false);
            this.paneldate.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox chkItemname;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox chkpurchasetype;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox chktype;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox ChkTrademark;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox chkpurchasetotal;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox ChkCategory;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox chkpurchase;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox ChkUom;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnGenerate;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox drpselect;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox cbx_Purchasediscount;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox chkgroup;
        private System.Windows.Forms.Panel panelCustomer;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cbx_customer;
        private System.Windows.Forms.Panel panelproduct;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cbx_product;
        private System.Windows.Forms.BindingSource DataTable1BindingSource;
        private PurchaseDataSet PurchaseDataSet;
        private System.Windows.Forms.BindingSource DataTable2BindingSource;
        private System.Windows.Forms.BindingSource DataTable3BindingSource;
        private PurchaseDataSetTableAdapters.DataTable1TableAdapter DataTable1TableAdapter;
        private PurchaseDataSetTableAdapters.DataTable2TableAdapter DataTable2TableAdapter;
        private PurchaseDataSetTableAdapters.DataTable3TableAdapter DataTable3TableAdapter;
        private System.Windows.Forms.Panel paneldate;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private System.Windows.Forms.DateTimePicker Date_end;
        private System.Windows.Forms.DateTimePicker Date_Start;
    }
}