namespace Sys_Sols_Inventory
{
    partial class Purchase_RPT_HDR
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.Send_Mail = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.EndDate = new System.Windows.Forms.DateTimePicker();
            this.kryptonLabel8 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.Cbx_salestype = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.Cbx_supplier = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.kryptonLabel5 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel7 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.StartDate = new System.Windows.Forms.DateTimePicker();
            this.kryptonLabel6 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.btnSave = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.Category = new System.Windows.Forms.GroupBox();
            this.chDet = new System.Windows.Forms.CheckBox();
            this.cmbVoucher = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.kryptonLabel11 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.Trademark = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.DrpCategory = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.Group = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.TYPE = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.kryptonLabel14 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel15 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel16 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel17 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.btn_Tax = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.Chk = new System.Windows.Forms.CheckBox();
            this.cmb_item = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.kryptonLabel9 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.btnDetailed = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnExcel = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.tb_Summary = new System.Windows.Forms.TabControl();
            this.tp_summary = new System.Windows.Forms.TabPage();
            this.DG_GRIDVIEW = new System.Windows.Forms.DataGridView();
            this.tp_detail = new System.Windows.Forms.TabPage();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.NewPurchase = new Sys_Sols_Inventory.reports.NewPurchase();
            this.DataTable1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.DataTable1TableAdapter = new Sys_Sols_Inventory.reports.NewPurchaseTableAdapters.DataTable1TableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.Cbx_salestype)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cbx_supplier)).BeginInit();
            this.Category.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbVoucher)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Trademark)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DrpCategory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Group)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TYPE)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_item)).BeginInit();
            this.tb_Summary.SuspendLayout();
            this.tp_summary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DG_GRIDVIEW)).BeginInit();
            this.tp_detail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NewPurchase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataTable1BindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // Send_Mail
            // 
            this.Send_Mail.Location = new System.Drawing.Point(9, 18);
            this.Send_Mail.Name = "Send_Mail";
            this.Send_Mail.Size = new System.Drawing.Size(10, 10);
            this.Send_Mail.TabIndex = 113;
            this.Send_Mail.Values.Text = "Send Mail";
            this.Send_Mail.Visible = false;
            this.Send_Mail.Click += new System.EventHandler(this.Send_Mail_Click);
            // 
            // EndDate
            // 
            this.EndDate.Enabled = false;
            this.EndDate.Location = new System.Drawing.Point(105, 75);
            this.EndDate.Name = "EndDate";
            this.EndDate.Size = new System.Drawing.Size(135, 20);
            this.EndDate.TabIndex = 22;
            // 
            // kryptonLabel8
            // 
            this.kryptonLabel8.Location = new System.Drawing.Point(31, 75);
            this.kryptonLabel8.Name = "kryptonLabel8";
            this.kryptonLabel8.Size = new System.Drawing.Size(63, 20);
            this.kryptonLabel8.TabIndex = 21;
            this.kryptonLabel8.Values.Text = "End Date:";
            // 
            // Cbx_salestype
            // 
            this.Cbx_salestype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cbx_salestype.DropDownWidth = 211;
            this.Cbx_salestype.Location = new System.Drawing.Point(356, 21);
            this.Cbx_salestype.Name = "Cbx_salestype";
            this.Cbx_salestype.Size = new System.Drawing.Size(258, 21);
            this.Cbx_salestype.TabIndex = 20;
            // 
            // Cbx_supplier
            // 
            this.Cbx_supplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cbx_supplier.DropDownWidth = 211;
            this.Cbx_supplier.Location = new System.Drawing.Point(356, 48);
            this.Cbx_supplier.Name = "Cbx_supplier";
            this.Cbx_supplier.Size = new System.Drawing.Size(258, 21);
            this.Cbx_supplier.TabIndex = 19;
            // 
            // kryptonLabel5
            // 
            this.kryptonLabel5.Location = new System.Drawing.Point(263, 49);
            this.kryptonLabel5.Name = "kryptonLabel5";
            this.kryptonLabel5.Size = new System.Drawing.Size(99, 20);
            this.kryptonLabel5.TabIndex = 18;
            this.kryptonLabel5.Values.Text = "Supplier            :";
            // 
            // kryptonLabel7
            // 
            this.kryptonLabel7.Location = new System.Drawing.Point(263, 22);
            this.kryptonLabel7.Name = "kryptonLabel7";
            this.kryptonLabel7.Size = new System.Drawing.Size(95, 20);
            this.kryptonLabel7.TabIndex = 17;
            this.kryptonLabel7.Values.Text = "Purchase Type :";
            // 
            // StartDate
            // 
            this.StartDate.Enabled = false;
            this.StartDate.Location = new System.Drawing.Point(105, 46);
            this.StartDate.Name = "StartDate";
            this.StartDate.Size = new System.Drawing.Size(135, 20);
            this.StartDate.TabIndex = 15;
            // 
            // kryptonLabel6
            // 
            this.kryptonLabel6.Location = new System.Drawing.Point(31, 46);
            this.kryptonLabel6.Name = "kryptonLabel6";
            this.kryptonLabel6.Size = new System.Drawing.Size(71, 20);
            this.kryptonLabel6.TabIndex = 14;
            this.kryptonLabel6.Values.Text = "Start  Date:";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(745, 110);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 25);
            this.btnSave.TabIndex = 11;
            this.btnSave.Values.Text = "Search";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // Category
            // 
            this.Category.Controls.Add(this.chDet);
            this.Category.Controls.Add(this.cmbVoucher);
            this.Category.Controls.Add(this.kryptonLabel11);
            this.Category.Controls.Add(this.Trademark);
            this.Category.Controls.Add(this.DrpCategory);
            this.Category.Controls.Add(this.Group);
            this.Category.Controls.Add(this.TYPE);
            this.Category.Controls.Add(this.kryptonLabel14);
            this.Category.Controls.Add(this.kryptonLabel15);
            this.Category.Controls.Add(this.kryptonLabel16);
            this.Category.Controls.Add(this.kryptonLabel17);
            this.Category.Controls.Add(this.btn_Tax);
            this.Category.Controls.Add(this.Chk);
            this.Category.Controls.Add(this.cmb_item);
            this.Category.Controls.Add(this.kryptonLabel9);
            this.Category.Controls.Add(this.Send_Mail);
            this.Category.Controls.Add(this.EndDate);
            this.Category.Controls.Add(this.kryptonLabel8);
            this.Category.Controls.Add(this.Cbx_salestype);
            this.Category.Controls.Add(this.Cbx_supplier);
            this.Category.Controls.Add(this.kryptonLabel5);
            this.Category.Controls.Add(this.kryptonLabel7);
            this.Category.Controls.Add(this.StartDate);
            this.Category.Controls.Add(this.kryptonLabel6);
            this.Category.Controls.Add(this.btnDetailed);
            this.Category.Controls.Add(this.btnExcel);
            this.Category.Controls.Add(this.btnSave);
            this.Category.Dock = System.Windows.Forms.DockStyle.Top;
            this.Category.Location = new System.Drawing.Point(0, 0);
            this.Category.Name = "Category";
            this.Category.Size = new System.Drawing.Size(1144, 159);
            this.Category.TabIndex = 5;
            this.Category.TabStop = false;
            this.Category.Text = "Search";
            // 
            // chDet
            // 
            this.chDet.AutoSize = true;
            this.chDet.Location = new System.Drawing.Point(175, 19);
            this.chDet.Name = "chDet";
            this.chDet.Size = new System.Drawing.Size(65, 17);
            this.chDet.TabIndex = 137;
            this.chDet.Text = "Detailed";
            this.chDet.UseVisualStyleBackColor = true;
            // 
            // cmbVoucher
            // 
            this.cmbVoucher.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVoucher.DropDownWidth = 211;
            this.cmbVoucher.Location = new System.Drawing.Point(356, 75);
            this.cmbVoucher.Name = "cmbVoucher";
            this.cmbVoucher.Size = new System.Drawing.Size(258, 21);
            this.cmbVoucher.TabIndex = 136;
            // 
            // kryptonLabel11
            // 
            this.kryptonLabel11.Location = new System.Drawing.Point(263, 75);
            this.kryptonLabel11.Name = "kryptonLabel11";
            this.kryptonLabel11.Size = new System.Drawing.Size(95, 20);
            this.kryptonLabel11.TabIndex = 134;
            this.kryptonLabel11.Values.Text = "Voucher Type  :";
            // 
            // Trademark
            // 
            this.Trademark.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Trademark.DropDownWidth = 211;
            this.Trademark.Location = new System.Drawing.Point(697, 48);
            this.Trademark.Name = "Trademark";
            this.Trademark.Size = new System.Drawing.Size(184, 21);
            this.Trademark.TabIndex = 128;
            // 
            // DrpCategory
            // 
            this.DrpCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DrpCategory.DropDownWidth = 211;
            this.DrpCategory.Location = new System.Drawing.Point(954, 22);
            this.DrpCategory.Name = "DrpCategory";
            this.DrpCategory.Size = new System.Drawing.Size(184, 21);
            this.DrpCategory.TabIndex = 127;
            // 
            // Group
            // 
            this.Group.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Group.DropDownWidth = 211;
            this.Group.Location = new System.Drawing.Point(697, 21);
            this.Group.Name = "Group";
            this.Group.Size = new System.Drawing.Size(184, 21);
            this.Group.TabIndex = 126;
            // 
            // TYPE
            // 
            this.TYPE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TYPE.DropDownWidth = 211;
            this.TYPE.Location = new System.Drawing.Point(954, 48);
            this.TYPE.Name = "TYPE";
            this.TYPE.Size = new System.Drawing.Size(184, 21);
            this.TYPE.TabIndex = 125;
            // 
            // kryptonLabel14
            // 
            this.kryptonLabel14.Location = new System.Drawing.Point(636, 22);
            this.kryptonLabel14.Name = "kryptonLabel14";
            this.kryptonLabel14.Size = new System.Drawing.Size(47, 20);
            this.kryptonLabel14.TabIndex = 124;
            this.kryptonLabel14.Values.Text = "Group:";
            // 
            // kryptonLabel15
            // 
            this.kryptonLabel15.Location = new System.Drawing.Point(894, 22);
            this.kryptonLabel15.Name = "kryptonLabel15";
            this.kryptonLabel15.Size = new System.Drawing.Size(63, 20);
            this.kryptonLabel15.TabIndex = 123;
            this.kryptonLabel15.Values.Text = "Category:";
            // 
            // kryptonLabel16
            // 
            this.kryptonLabel16.Location = new System.Drawing.Point(636, 51);
            this.kryptonLabel16.Name = "kryptonLabel16";
            this.kryptonLabel16.Size = new System.Drawing.Size(45, 20);
            this.kryptonLabel16.TabIndex = 122;
            this.kryptonLabel16.Values.Text = "Brand:";
            // 
            // kryptonLabel17
            // 
            this.kryptonLabel17.Location = new System.Drawing.Point(897, 48);
            this.kryptonLabel17.Name = "kryptonLabel17";
            this.kryptonLabel17.Size = new System.Drawing.Size(39, 20);
            this.kryptonLabel17.TabIndex = 121;
            this.kryptonLabel17.Values.Text = "Type:";
            // 
            // btn_Tax
            // 
            this.btn_Tax.Location = new System.Drawing.Point(1033, 110);
            this.btn_Tax.Name = "btn_Tax";
            this.btn_Tax.Size = new System.Drawing.Size(97, 25);
            this.btn_Tax.TabIndex = 120;
            this.btn_Tax.Values.Text = "Tax Report";
            this.btn_Tax.Click += new System.EventHandler(this.btn_Tax_Click);
            // 
            // Chk
            // 
            this.Chk.AutoSize = true;
            this.Chk.Location = new System.Drawing.Point(36, 19);
            this.Chk.Name = "Chk";
            this.Chk.Size = new System.Drawing.Size(99, 17);
            this.Chk.TabIndex = 118;
            this.Chk.Text = "Report on Date";
            this.Chk.UseVisualStyleBackColor = true;
            this.Chk.CheckedChanged += new System.EventHandler(this.Chk_CheckedChanged_1);
            // 
            // cmb_item
            // 
            this.cmb_item.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmb_item.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmb_item.DropDownWidth = 211;
            this.cmb_item.Location = new System.Drawing.Point(710, 74);
            this.cmb_item.Name = "cmb_item";
            this.cmb_item.Size = new System.Drawing.Size(428, 21);
            this.cmb_item.TabIndex = 117;
            // 
            // kryptonLabel9
            // 
            this.kryptonLabel9.Location = new System.Drawing.Point(636, 76);
            this.kryptonLabel9.Name = "kryptonLabel9";
            this.kryptonLabel9.Size = new System.Drawing.Size(77, 20);
            this.kryptonLabel9.TabIndex = 116;
            this.kryptonLabel9.Values.Text = "Item Name :";
            // 
            // btnDetailed
            // 
            this.btnDetailed.Location = new System.Drawing.Point(937, 110);
            this.btnDetailed.Name = "btnDetailed";
            this.btnDetailed.Size = new System.Drawing.Size(90, 25);
            this.btnDetailed.TabIndex = 11;
            this.btnDetailed.Values.Text = "Detailed";
            this.btnDetailed.Click += new System.EventHandler(this.btnDetailed_Click);
            // 
            // btnExcel
            // 
            this.btnExcel.Location = new System.Drawing.Point(841, 110);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(90, 25);
            this.btnExcel.TabIndex = 11;
            this.btnExcel.Values.Text = "Excel";
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // tb_Summary
            // 
            this.tb_Summary.Controls.Add(this.tp_summary);
            this.tb_Summary.Controls.Add(this.tp_detail);
            this.tb_Summary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_Summary.Location = new System.Drawing.Point(0, 159);
            this.tb_Summary.Name = "tb_Summary";
            this.tb_Summary.SelectedIndex = 0;
            this.tb_Summary.Size = new System.Drawing.Size(1144, 343);
            this.tb_Summary.TabIndex = 6;
            // 
            // tp_summary
            // 
            this.tp_summary.Controls.Add(this.DG_GRIDVIEW);
            this.tp_summary.Location = new System.Drawing.Point(4, 22);
            this.tp_summary.Name = "tp_summary";
            this.tp_summary.Padding = new System.Windows.Forms.Padding(3);
            this.tp_summary.Size = new System.Drawing.Size(1136, 317);
            this.tp_summary.TabIndex = 0;
            this.tp_summary.Text = "Summary";
            this.tp_summary.UseVisualStyleBackColor = true;
            // 
            // DG_GRIDVIEW
            // 
            this.DG_GRIDVIEW.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.DG_GRIDVIEW.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.DG_GRIDVIEW.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DG_GRIDVIEW.BackgroundColor = System.Drawing.SystemColors.ControlDark;
            this.DG_GRIDVIEW.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DG_GRIDVIEW.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DG_GRIDVIEW.Location = new System.Drawing.Point(3, 3);
            this.DG_GRIDVIEW.Name = "DG_GRIDVIEW";
            this.DG_GRIDVIEW.RowHeadersVisible = false;
            this.DG_GRIDVIEW.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DG_GRIDVIEW.RowTemplate.Height = 25;
            this.DG_GRIDVIEW.Size = new System.Drawing.Size(1130, 311);
            this.DG_GRIDVIEW.TabIndex = 8;
            // 
            // tp_detail
            // 
            this.tp_detail.Controls.Add(this.reportViewer1);
            this.tp_detail.Location = new System.Drawing.Point(4, 22);
            this.tp_detail.Name = "tp_detail";
            this.tp_detail.Padding = new System.Windows.Forms.Padding(3);
            this.tp_detail.Size = new System.Drawing.Size(1136, 317);
            this.tp_detail.TabIndex = 1;
            this.tp_detail.Text = "Detailed";
            this.tp_detail.UseVisualStyleBackColor = true;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.DataTable1BindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Sys_Sols_Inventory.reports.Report4.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(3, 3);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.PageCountMode = Microsoft.Reporting.WinForms.PageCountMode.Actual;
            this.reportViewer1.Size = new System.Drawing.Size(1130, 311);
            this.reportViewer1.TabIndex = 5;
            // 
            // NewPurchase
            // 
            this.NewPurchase.DataSetName = "NewPurchase";
            this.NewPurchase.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // DataTable1BindingSource
            // 
            this.DataTable1BindingSource.DataMember = "DataTable1";
            this.DataTable1BindingSource.DataSource = this.NewPurchase;
            // 
            // DataTable1TableAdapter
            // 
            this.DataTable1TableAdapter.ClearBeforeFill = true;
            // 
            // Purchase_RPT_HDR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1144, 502);
            this.Controls.Add(this.tb_Summary);
            this.Controls.Add(this.Category);
            this.Name = "Purchase_RPT_HDR";
            this.Text = "Purchase Report";
            this.Load += new System.EventHandler(this.Purchase_RPT_HDR_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Cbx_salestype)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cbx_supplier)).EndInit();
            this.Category.ResumeLayout(false);
            this.Category.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbVoucher)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Trademark)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DrpCategory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Group)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TYPE)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_item)).EndInit();
            this.tb_Summary.ResumeLayout(false);
            this.tp_summary.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DG_GRIDVIEW)).EndInit();
            this.tp_detail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.NewPurchase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataTable1BindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonButton Send_Mail;
        private System.Windows.Forms.DateTimePicker EndDate;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel8;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox Cbx_salestype;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox Cbx_supplier;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel5;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel7;
        private System.Windows.Forms.DateTimePicker StartDate;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel6;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSave;
        private System.Windows.Forms.GroupBox Category;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cmb_item;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel9;
        private System.Windows.Forms.CheckBox Chk;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnExcel;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnDetailed;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btn_Tax;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cmbVoucher;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel11;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox Trademark;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox DrpCategory;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox Group;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox TYPE;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel14;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel15;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel16;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel17;
        private System.Windows.Forms.TabControl tb_Summary;
        private System.Windows.Forms.TabPage tp_summary;
        private System.Windows.Forms.DataGridView DG_GRIDVIEW;
        private System.Windows.Forms.TabPage tp_detail;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.CheckBox chDet;
        private reports.NewPurchase NewPurchase;
        private System.Windows.Forms.BindingSource DataTable1BindingSource;
        private reports.NewPurchaseTableAdapters.DataTable1TableAdapter DataTable1TableAdapter;
    }
}