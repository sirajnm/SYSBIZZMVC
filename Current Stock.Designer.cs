namespace Sys_Sols_Inventory
{
    partial class Current_Stock
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Current_Stock));
            this.GetCurrentStockBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.AIN_INVENTORYDataSet1 = new Sys_Sols_Inventory.AIN_INVENTORYDataSet1();
            this.btnSave = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.Trademark = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.DrpCategory = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.Group = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.TYPE = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.Category = new System.Windows.Forms.GroupBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.DESC_ENG = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel5 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonButton1 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.drpBranch = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.kryptonLabel7 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.GetCurrentStockTableAdapter = new Sys_Sols_Inventory.AIN_INVENTORYDataSet1TableAdapters.GetCurrentStockTableAdapter();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.GetCurrentStockBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AIN_INVENTORYDataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Trademark)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DrpCategory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Group)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TYPE)).BeginInit();
            this.panel1.SuspendLayout();
            this.Category.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.drpBranch)).BeginInit();
            this.SuspendLayout();
            // 
            // GetCurrentStockBindingSource
            // 
            this.GetCurrentStockBindingSource.DataMember = "GetCurrentStock";
            this.GetCurrentStockBindingSource.DataSource = this.AIN_INVENTORYDataSet1;
            // 
            // AIN_INVENTORYDataSet1
            // 
            this.AIN_INVENTORYDataSet1.DataSetName = "AIN_INVENTORYDataSet1";
            this.AIN_INVENTORYDataSet1.EnforceConstraints = false;
            this.AIN_INVENTORYDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(670, 97);
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
            this.kryptonLabel4.Location = new System.Drawing.Point(508, 18);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(47, 20);
            this.kryptonLabel4.TabIndex = 7;
            this.kryptonLabel4.Values.Text = "Group:";
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(25, 45);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(63, 20);
            this.kryptonLabel3.TabIndex = 6;
            this.kryptonLabel3.Values.Text = "Category:";
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(508, 44);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(49, 20);
            this.kryptonLabel2.TabIndex = 5;
            this.kryptonLabel2.Values.Text = "Brand :";
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(40, 18);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(39, 20);
            this.kryptonLabel1.TabIndex = 4;
            this.kryptonLabel1.Values.Text = "Type:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.reportViewer1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 140);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1043, 450);
            this.panel1.TabIndex = 3;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.GetCurrentStockBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Sys_Sols_Inventory.rptStock.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(1043, 450);
            this.reportViewer1.TabIndex = 0;
            // 
            // Category
            // 
            this.Category.Controls.Add(this.linkLabel2);
            this.Category.Controls.Add(this.linkLabel1);
            this.Category.Controls.Add(this.DESC_ENG);
            this.Category.Controls.Add(this.kryptonLabel5);
            this.Category.Controls.Add(this.kryptonButton1);
            this.Category.Controls.Add(this.btnSave);
            this.Category.Controls.Add(this.Trademark);
            this.Category.Controls.Add(this.drpBranch);
            this.Category.Controls.Add(this.DrpCategory);
            this.Category.Controls.Add(this.Group);
            this.Category.Controls.Add(this.TYPE);
            this.Category.Controls.Add(this.kryptonLabel4);
            this.Category.Controls.Add(this.kryptonLabel7);
            this.Category.Controls.Add(this.kryptonLabel3);
            this.Category.Controls.Add(this.kryptonLabel2);
            this.Category.Controls.Add(this.kryptonLabel1);
            this.Category.Dock = System.Windows.Forms.DockStyle.Top;
            this.Category.Location = new System.Drawing.Point(0, 0);
            this.Category.Name = "Category";
            this.Category.Size = new System.Drawing.Size(1043, 140);
            this.Category.TabIndex = 2;
            this.Category.TabStop = false;
            this.Category.Text = "Search";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(633, 105);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(34, 13);
            this.linkLabel1.TabIndex = 105;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Detail";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // DESC_ENG
            // 
            this.DESC_ENG.Location = new System.Drawing.Point(563, 71);
            this.DESC_ENG.Name = "DESC_ENG";
            this.DESC_ENG.Size = new System.Drawing.Size(293, 23);
            this.DESC_ENG.TabIndex = 104;
            // 
            // kryptonLabel5
            // 
            this.kryptonLabel5.Location = new System.Drawing.Point(483, 71);
            this.kryptonLabel5.Name = "kryptonLabel5";
            this.kryptonLabel5.Size = new System.Drawing.Size(77, 20);
            this.kryptonLabel5.TabIndex = 17;
            this.kryptonLabel5.Values.Text = "Item Name :";
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.Location = new System.Drawing.Point(766, 97);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.Size = new System.Drawing.Size(90, 25);
            this.kryptonButton1.TabIndex = 11;
            this.kryptonButton1.Values.Text = "Send Mail";
            this.kryptonButton1.Click += new System.EventHandler(this.kryptonButton1_Click);
            // 
            // drpBranch
            // 
            this.drpBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.drpBranch.DropDownWidth = 211;
            this.drpBranch.Location = new System.Drawing.Point(117, 70);
            this.drpBranch.Name = "drpBranch";
            this.drpBranch.Size = new System.Drawing.Size(337, 21);
            this.drpBranch.TabIndex = 10;
            // 
            // kryptonLabel7
            // 
            this.kryptonLabel7.Location = new System.Drawing.Point(25, 70);
            this.kryptonLabel7.Name = "kryptonLabel7";
            this.kryptonLabel7.Size = new System.Drawing.Size(54, 20);
            this.kryptonLabel7.TabIndex = 6;
            this.kryptonLabel7.Values.Text = "Branch :";
            // 
            // GetCurrentStockTableAdapter
            // 
            this.GetCurrentStockTableAdapter.ClearBeforeFill = true;
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(556, 105);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(76, 13);
            this.linkLabel2.TabIndex = 106;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "Stock on Date";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // Current_Stock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1043, 590);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Category);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Current_Stock";
            this.Text = "Current_Stock";
            this.Load += new System.EventHandler(this.Current_Stock_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GetCurrentStockBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AIN_INVENTORYDataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Trademark)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DrpCategory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Group)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TYPE)).EndInit();
            this.panel1.ResumeLayout(false);
            this.Category.ResumeLayout(false);
            this.Category.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.drpBranch)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSave;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox Trademark;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox DrpCategory;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox Group;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox TYPE;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox Category;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel5;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox DESC_ENG;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton1;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox drpBranch;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel7;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource GetCurrentStockBindingSource;
        private AIN_INVENTORYDataSet1 AIN_INVENTORYDataSet1;
        private AIN_INVENTORYDataSet1TableAdapters.GetCurrentStockTableAdapter GetCurrentStockTableAdapter;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.LinkLabel linkLabel2;
    }
}