namespace Sys_Sols_Inventory.reports
{
    partial class SalesReportFinal
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
            this.DataTable1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.SaleDataNew = new Sys_Sols_Inventory.reports.SaleDataNew();
            this.Category = new System.Windows.Forms.GroupBox();
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
            this.DataTable1TableAdapter = new Sys_Sols_Inventory.reports.SaleDataNewTableAdapters.DataTable1TableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.DataTable1BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SaleDataNew)).BeginInit();
            this.Category.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Cbx_salestype)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cbx_supplier)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Trademark)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DrpCategory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Group)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TYPE)).BeginInit();
            this.SuspendLayout();
            // 
            // DataTable1BindingSource
            // 
            this.DataTable1BindingSource.DataMember = "DataTable1";
            this.DataTable1BindingSource.DataSource = this.SaleDataNew;
            // 
            // SaleDataNew
            // 
            this.SaleDataNew.DataSetName = "SaleDataNew";
            this.SaleDataNew.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // Category
            // 
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
            this.Category.Size = new System.Drawing.Size(923, 170);
            this.Category.TabIndex = 5;
            this.Category.TabStop = false;
            this.Category.Text = "Search";
            this.Category.Enter += new System.EventHandler(this.Category_Enter);
            // 
            // Send_Mail
            // 
            this.Send_Mail.Location = new System.Drawing.Point(800, 124);
            this.Send_Mail.Name = "Send_Mail";
            this.Send_Mail.Size = new System.Drawing.Size(90, 25);
            this.Send_Mail.TabIndex = 113;
            this.Send_Mail.Values.Text = "Send Mail";
            this.Send_Mail.Click += new System.EventHandler(this.Send_Mail_Click);
            // 
            // EndDate
            // 
            this.EndDate.Enabled = false;
            this.EndDate.Location = new System.Drawing.Point(434, 130);
            this.EndDate.Name = "EndDate";
            this.EndDate.Size = new System.Drawing.Size(200, 20);
            this.EndDate.TabIndex = 22;
            // 
            // kryptonLabel8
            // 
            this.kryptonLabel8.Location = new System.Drawing.Point(348, 130);
            this.kryptonLabel8.Name = "kryptonLabel8";
            this.kryptonLabel8.Size = new System.Drawing.Size(63, 20);
            this.kryptonLabel8.TabIndex = 21;
            this.kryptonLabel8.Values.Text = "End Date:";
            // 
            // Cbx_salestype
            // 
            this.Cbx_salestype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cbx_salestype.DropDownWidth = 211;
            this.Cbx_salestype.Location = new System.Drawing.Point(564, 73);
            this.Cbx_salestype.Name = "Cbx_salestype";
            this.Cbx_salestype.Size = new System.Drawing.Size(293, 21);
            this.Cbx_salestype.TabIndex = 20;
            // 
            // Cbx_supplier
            // 
            this.Cbx_supplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cbx_supplier.DropDownWidth = 211;
            this.Cbx_supplier.Location = new System.Drawing.Point(118, 74);
            this.Cbx_supplier.Name = "Cbx_supplier";
            this.Cbx_supplier.Size = new System.Drawing.Size(337, 21);
            this.Cbx_supplier.TabIndex = 19;
            // 
            // kryptonLabel5
            // 
            this.kryptonLabel5.Location = new System.Drawing.Point(17, 74);
            this.kryptonLabel5.Name = "kryptonLabel5";
            this.kryptonLabel5.Size = new System.Drawing.Size(66, 20);
            this.kryptonLabel5.TabIndex = 18;
            this.kryptonLabel5.Values.Text = "Customer:";
            // 
            // kryptonLabel7
            // 
            this.kryptonLabel7.Location = new System.Drawing.Point(470, 75);
            this.kryptonLabel7.Name = "kryptonLabel7";
            this.kryptonLabel7.Size = new System.Drawing.Size(92, 20);
            this.kryptonLabel7.TabIndex = 17;
            this.kryptonLabel7.Values.Text = "Purchase Type:";
            // 
            // Chk
            // 
            this.Chk.AutoSize = true;
            this.Chk.Location = new System.Drawing.Point(119, 108);
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
            this.StartDate.Location = new System.Drawing.Point(119, 130);
            this.StartDate.Name = "StartDate";
            this.StartDate.Size = new System.Drawing.Size(200, 20);
            this.StartDate.TabIndex = 15;
            // 
            // kryptonLabel6
            // 
            this.kryptonLabel6.Location = new System.Drawing.Point(18, 130);
            this.kryptonLabel6.Name = "kryptonLabel6";
            this.kryptonLabel6.Size = new System.Drawing.Size(71, 20);
            this.kryptonLabel6.TabIndex = 14;
            this.kryptonLabel6.Values.Text = "Start  Date:";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(704, 124);
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
            this.Trademark.Location = new System.Drawing.Point(563, 44);
            this.Trademark.Name = "Trademark";
            this.Trademark.Size = new System.Drawing.Size(293, 21);
            this.Trademark.TabIndex = 11;
            // 
            // DrpCategory
            // 
            this.DrpCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DrpCategory.DropDownWidth = 211;
            this.DrpCategory.Location = new System.Drawing.Point(117, 45);
            this.DrpCategory.Name = "DrpCategory";
            this.DrpCategory.Size = new System.Drawing.Size(337, 21);
            this.DrpCategory.TabIndex = 10;
            // 
            // Group
            // 
            this.Group.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Group.DropDownWidth = 211;
            this.Group.Location = new System.Drawing.Point(563, 18);
            this.Group.Name = "Group";
            this.Group.Size = new System.Drawing.Size(293, 21);
            this.Group.TabIndex = 9;
            // 
            // TYPE
            // 
            this.TYPE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TYPE.DropDownWidth = 211;
            this.TYPE.Location = new System.Drawing.Point(117, 18);
            this.TYPE.Name = "TYPE";
            this.TYPE.Size = new System.Drawing.Size(337, 21);
            this.TYPE.TabIndex = 8;
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(469, 19);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(47, 20);
            this.kryptonLabel4.TabIndex = 7;
            this.kryptonLabel4.Values.Text = "Group:";
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(16, 45);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(63, 20);
            this.kryptonLabel3.TabIndex = 6;
            this.kryptonLabel3.Values.Text = "Category:";
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(469, 46);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(75, 20);
            this.kryptonLabel2.TabIndex = 5;
            this.kryptonLabel2.Values.Text = "Trade Mark:";
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
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.DataTable1BindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Sys_Sols_Inventory.reports.Report5.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 170);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(923, 209);
            this.reportViewer1.TabIndex = 6;
            // 
            // DataTable1TableAdapter
            // 
            this.DataTable1TableAdapter.ClearBeforeFill = true;
            // 
            // SalesReportFinal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(923, 379);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.Category);
            this.Name = "SalesReportFinal";
            this.Text = "Sales Report";
            this.Load += new System.EventHandler(this.SalesReportFinal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataTable1BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SaleDataNew)).EndInit();
            this.Category.ResumeLayout(false);
            this.Category.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Cbx_salestype)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cbx_supplier)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Trademark)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DrpCategory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Group)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TYPE)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox Category;
        private System.Windows.Forms.DateTimePicker EndDate;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel8;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox Cbx_salestype;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox Cbx_supplier;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel5;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel7;
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
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource DataTable1BindingSource;
        private SaleDataNew SaleDataNew;
        private SaleDataNewTableAdapters.DataTable1TableAdapter DataTable1TableAdapter;
        private ComponentFactory.Krypton.Toolkit.KryptonButton Send_Mail;
    }
}