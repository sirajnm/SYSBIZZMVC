namespace Sys_Sols_Inventory.reports
{
    partial class PurchaseReportNew
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource3 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.Category = new System.Windows.Forms.GroupBox();
            this.cmbVoucher = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.kryptonLabel9 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonButton1 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.Send_Mail = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.EndDate = new System.Windows.Forms.DateTimePicker();
            this.kryptonLabel8 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.Cbx_salestype = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.Cbx_supplier = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.kryptonLabel5 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel7 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.Chk = new System.Windows.Forms.CheckBox();
            this.StartDate = new System.Windows.Forms.DateTimePicker();
            this.kryptonLabel6 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.btnSave = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.Trademark = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.DrpCategory = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.Group = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.TYPE = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.kryptonLabel10 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.cmb_item = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.DataTable1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.NewPurchase = new Sys_Sols_Inventory.reports.NewPurchase();
            this.DataTable1TableAdapter = new Sys_Sols_Inventory.reports.NewPurchaseTableAdapters.DataTable1TableAdapter();
            this.Category.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbVoucher)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cbx_salestype)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cbx_supplier)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Trademark)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DrpCategory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Group)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TYPE)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_item)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataTable1BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NewPurchase)).BeginInit();
            this.SuspendLayout();
            // 
            // Category
            // 
            this.Category.Controls.Add(this.cmb_item);
            this.Category.Controls.Add(this.kryptonLabel10);
            this.Category.Controls.Add(this.cmbVoucher);
            this.Category.Controls.Add(this.kryptonLabel9);
            this.Category.Controls.Add(this.kryptonButton1);
            this.Category.Controls.Add(this.Send_Mail);
            this.Category.Controls.Add(this.EndDate);
            this.Category.Controls.Add(this.kryptonLabel8);
            this.Category.Controls.Add(this.Cbx_salestype);
            this.Category.Controls.Add(this.Cbx_supplier);
            this.Category.Controls.Add(this.kryptonLabel5);
            this.Category.Controls.Add(this.kryptonLabel7);
            this.Category.Controls.Add(this.Chk);
            this.Category.Controls.Add(this.StartDate);
            this.Category.Controls.Add(this.kryptonLabel6);
            this.Category.Controls.Add(this.btnSave);
            this.Category.Controls.Add(this.Trademark);
            this.Category.Controls.Add(this.DrpCategory);
            this.Category.Controls.Add(this.Group);
            this.Category.Controls.Add(this.TYPE);
            this.Category.Controls.Add(this.kryptonLabel4);
            this.Category.Controls.Add(this.kryptonLabel3);
            this.Category.Controls.Add(this.kryptonLabel2);
            this.Category.Controls.Add(this.kryptonLabel1);
            this.Category.Dock = System.Windows.Forms.DockStyle.Top;
            this.Category.Location = new System.Drawing.Point(0, 0);
            this.Category.Name = "Category";
            this.Category.Size = new System.Drawing.Size(983, 125);
            this.Category.TabIndex = 3;
            this.Category.TabStop = false;
            this.Category.Text = "Search";
            // 
            // cmbVoucher
            // 
            this.cmbVoucher.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVoucher.DropDownWidth = 211;
            this.cmbVoucher.Location = new System.Drawing.Point(768, 20);
            this.cmbVoucher.Name = "cmbVoucher";
            this.cmbVoucher.Size = new System.Drawing.Size(203, 21);
            this.cmbVoucher.TabIndex = 117;
            // 
            // kryptonLabel9
            // 
            this.kryptonLabel9.Location = new System.Drawing.Point(674, 20);
            this.kryptonLabel9.Name = "kryptonLabel9";
            this.kryptonLabel9.Size = new System.Drawing.Size(88, 20);
            this.kryptonLabel9.TabIndex = 116;
            this.kryptonLabel9.Values.Text = "Voucher Type:";
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.Location = new System.Drawing.Point(872, 86);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.Size = new System.Drawing.Size(97, 25);
            this.kryptonButton1.TabIndex = 114;
            this.kryptonButton1.Values.Text = "Deleted Report";
            this.kryptonButton1.Click += new System.EventHandler(this.kryptonButton1_Click);
            // 
            // Send_Mail
            // 
            this.Send_Mail.Location = new System.Drawing.Point(776, 86);
            this.Send_Mail.Name = "Send_Mail";
            this.Send_Mail.Size = new System.Drawing.Size(90, 25);
            this.Send_Mail.TabIndex = 113;
            this.Send_Mail.Values.Text = "Send Mail";
            this.Send_Mail.Click += new System.EventHandler(this.Send_Mail_Click);
            // 
            // EndDate
            // 
            this.EndDate.Enabled = false;
            this.EndDate.Location = new System.Drawing.Point(299, 94);
            this.EndDate.Name = "EndDate";
            this.EndDate.Size = new System.Drawing.Size(153, 20);
            this.EndDate.TabIndex = 22;
            // 
            // kryptonLabel8
            // 
            this.kryptonLabel8.Location = new System.Drawing.Point(236, 94);
            this.kryptonLabel8.Name = "kryptonLabel8";
            this.kryptonLabel8.Size = new System.Drawing.Size(63, 20);
            this.kryptonLabel8.TabIndex = 21;
            this.kryptonLabel8.Values.Text = "End Date:";
            // 
            // Cbx_salestype
            // 
            this.Cbx_salestype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cbx_salestype.DropDownWidth = 211;
            this.Cbx_salestype.Location = new System.Drawing.Point(532, 45);
            this.Cbx_salestype.Name = "Cbx_salestype";
            this.Cbx_salestype.Size = new System.Drawing.Size(133, 21);
            this.Cbx_salestype.TabIndex = 20;
            // 
            // Cbx_supplier
            // 
            this.Cbx_supplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cbx_supplier.DropDownWidth = 211;
            this.Cbx_supplier.Location = new System.Drawing.Point(532, 20);
            this.Cbx_supplier.Name = "Cbx_supplier";
            this.Cbx_supplier.Size = new System.Drawing.Size(133, 21);
            this.Cbx_supplier.TabIndex = 19;
            // 
            // kryptonLabel5
            // 
            this.kryptonLabel5.Location = new System.Drawing.Point(438, 21);
            this.kryptonLabel5.Name = "kryptonLabel5";
            this.kryptonLabel5.Size = new System.Drawing.Size(58, 20);
            this.kryptonLabel5.TabIndex = 18;
            this.kryptonLabel5.Values.Text = "Supplier:";
            // 
            // kryptonLabel7
            // 
            this.kryptonLabel7.Location = new System.Drawing.Point(438, 45);
            this.kryptonLabel7.Name = "kryptonLabel7";
            this.kryptonLabel7.Size = new System.Drawing.Size(92, 20);
            this.kryptonLabel7.TabIndex = 17;
            this.kryptonLabel7.Values.Text = "Purchase Type:";
            // 
            // Chk
            // 
            this.Chk.AutoSize = true;
            this.Chk.Location = new System.Drawing.Point(16, 72);
            this.Chk.Name = "Chk";
            this.Chk.Size = new System.Drawing.Size(99, 17);
            this.Chk.TabIndex = 16;
            this.Chk.Text = "Report on Date";
            this.Chk.UseVisualStyleBackColor = true;
            this.Chk.CheckedChanged += new System.EventHandler(this.Chk_CheckedChanged);
            // 
            // StartDate
            // 
            this.StartDate.Enabled = false;
            this.StartDate.Location = new System.Drawing.Point(74, 94);
            this.StartDate.Name = "StartDate";
            this.StartDate.Size = new System.Drawing.Size(156, 20);
            this.StartDate.TabIndex = 15;
            // 
            // kryptonLabel6
            // 
            this.kryptonLabel6.Location = new System.Drawing.Point(8, 94);
            this.kryptonLabel6.Name = "kryptonLabel6";
            this.kryptonLabel6.Size = new System.Drawing.Size(71, 20);
            this.kryptonLabel6.TabIndex = 14;
            this.kryptonLabel6.Values.Text = "Start  Date:";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(680, 85);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 25);
            this.btnSave.TabIndex = 11;
            this.btnSave.Values.Text = "Search";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // Trademark
            // 
            this.Trademark.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Trademark.DropDownWidth = 211;
            this.Trademark.Location = new System.Drawing.Point(280, 46);
            this.Trademark.Name = "Trademark";
            this.Trademark.Size = new System.Drawing.Size(145, 21);
            this.Trademark.TabIndex = 11;
            // 
            // DrpCategory
            // 
            this.DrpCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DrpCategory.DropDownWidth = 211;
            this.DrpCategory.Location = new System.Drawing.Point(84, 44);
            this.DrpCategory.Name = "DrpCategory";
            this.DrpCategory.Size = new System.Drawing.Size(134, 21);
            this.DrpCategory.TabIndex = 10;
            // 
            // Group
            // 
            this.Group.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Group.DropDownWidth = 211;
            this.Group.Location = new System.Drawing.Point(280, 20);
            this.Group.Name = "Group";
            this.Group.Size = new System.Drawing.Size(145, 21);
            this.Group.TabIndex = 9;
            // 
            // TYPE
            // 
            this.TYPE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TYPE.DropDownWidth = 211;
            this.TYPE.Location = new System.Drawing.Point(84, 18);
            this.TYPE.Name = "TYPE";
            this.TYPE.Size = new System.Drawing.Size(134, 21);
            this.TYPE.TabIndex = 8;
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(225, 20);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(47, 20);
            this.kryptonLabel4.TabIndex = 7;
            this.kryptonLabel4.Values.Text = "Group:";
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(16, 44);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(63, 20);
            this.kryptonLabel3.TabIndex = 6;
            this.kryptonLabel3.Values.Text = "Category:";
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(225, 46);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(45, 20);
            this.kryptonLabel2.TabIndex = 5;
            this.kryptonLabel2.Values.Text = "Brand:";
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(16, 19);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(39, 20);
            this.kryptonLabel1.TabIndex = 4;
            this.kryptonLabel1.Values.Text = "Type:";
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource3.Name = "DataSet1";
            reportDataSource3.Value = this.DataTable1BindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Sys_Sols_Inventory.reports.Report4.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 125);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.PageCountMode = Microsoft.Reporting.WinForms.PageCountMode.Actual;
            this.reportViewer1.Size = new System.Drawing.Size(983, 307);
            this.reportViewer1.TabIndex = 4;
            // 
            // kryptonLabel10
            // 
            this.kryptonLabel10.Location = new System.Drawing.Point(674, 46);
            this.kryptonLabel10.Name = "kryptonLabel10";
            this.kryptonLabel10.Size = new System.Drawing.Size(38, 20);
            this.kryptonLabel10.TabIndex = 116;
            this.kryptonLabel10.Values.Text = "Item:";
            // 
            // cmb_item
            // 
            this.cmb_item.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmb_item.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmb_item.DropDownWidth = 211;
            this.cmb_item.Location = new System.Drawing.Point(768, 45);
            this.cmb_item.Name = "cmb_item";
            this.cmb_item.Size = new System.Drawing.Size(203, 21);
            this.cmb_item.TabIndex = 117;
            // 
            // DataTable1BindingSource
            // 
            this.DataTable1BindingSource.DataMember = "DataTable1";
            this.DataTable1BindingSource.DataSource = this.NewPurchase;
            // 
            // NewPurchase
            // 
            this.NewPurchase.DataSetName = "NewPurchase";
            this.NewPurchase.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // DataTable1TableAdapter
            // 
            this.DataTable1TableAdapter.ClearBeforeFill = true;
            // 
            // PurchaseReportNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(983, 432);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.Category);
            this.Name = "PurchaseReportNew";
            this.Text = "Purchase Report";
            this.Load += new System.EventHandler(this.PurchaseReportNew_Load);
            this.Category.ResumeLayout(false);
            this.Category.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbVoucher)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cbx_salestype)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cbx_supplier)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Trademark)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DrpCategory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Group)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TYPE)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_item)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataTable1BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NewPurchase)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox Category;
        private System.Windows.Forms.CheckBox Chk;
        private System.Windows.Forms.DateTimePicker StartDate;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel6;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSave;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox Trademark;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox DrpCategory;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox Group;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox TYPE;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private System.Windows.Forms.DateTimePicker EndDate;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel8;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox Cbx_salestype;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox Cbx_supplier;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel5;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel7;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource DataTable1BindingSource;
        private NewPurchase NewPurchase;
        private NewPurchaseTableAdapters.DataTable1TableAdapter DataTable1TableAdapter;
        private ComponentFactory.Krypton.Toolkit.KryptonButton Send_Mail;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton1;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cmbVoucher;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel9;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cmb_item;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel10;
    }
}