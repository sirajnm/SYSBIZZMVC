namespace Sys_Sols_Inventory.reports
{
    partial class Item_Price_List
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
            this.Sp_itempricelistBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.PRICELISTDATASET = new Sys_Sols_Inventory.reports.PRICELISTDATASET();
            this.DESC_ENG = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel5 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.btnSearch = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.Trademark = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.DrpCategory = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.Group = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.TYPE = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.Category = new System.Windows.Forms.GroupBox();
            this.Send_Mail = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.dataGridItem = new System.Windows.Forms.DataGridView();
            this.Drpsaltype = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.kryptonLabel8 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtCode = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel7 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.DrpUOM = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.kryptonLabel6 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.Sp_itempricelistTableAdapter = new Sys_Sols_Inventory.reports.PRICELISTDATASETTableAdapters.Sp_itempricelistTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.Sp_itempricelistBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PRICELISTDATASET)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Trademark)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DrpCategory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Group)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TYPE)).BeginInit();
            this.panel1.SuspendLayout();
            this.Category.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Drpsaltype)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DrpUOM)).BeginInit();
            this.SuspendLayout();
            // 
            // Sp_itempricelistBindingSource
            // 
            this.Sp_itempricelistBindingSource.DataMember = "Sp_itempricelist";
            this.Sp_itempricelistBindingSource.DataSource = this.PRICELISTDATASET;
            this.Sp_itempricelistBindingSource.CurrentChanged += new System.EventHandler(this.Sp_itempricelistBindingSource_CurrentChanged);
            // 
            // PRICELISTDATASET
            // 
            this.PRICELISTDATASET.DataSetName = "PRICELISTDATASET";
            this.PRICELISTDATASET.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // DESC_ENG
            // 
            this.DESC_ENG.Location = new System.Drawing.Point(97, 23);
            this.DESC_ENG.Name = "DESC_ENG";
            this.DESC_ENG.Size = new System.Drawing.Size(293, 20);
            this.DESC_ENG.TabIndex = 0;
            this.DESC_ENG.TextChanged += new System.EventHandler(this.DESC_ENG_TextChanged);
            this.DESC_ENG.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DESC_ENG_KeyDown);
            // 
            // kryptonLabel5
            // 
            this.kryptonLabel5.Location = new System.Drawing.Point(496, 24);
            this.kryptonLabel5.Name = "kryptonLabel5";
            this.kryptonLabel5.Size = new System.Drawing.Size(49, 20);
            this.kryptonLabel5.TabIndex = 17;
            this.kryptonLabel5.Values.Text = "Brand :";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(640, 139);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(90, 25);
            this.btnSearch.TabIndex = 8;
            this.btnSearch.Values.Text = "Search";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // Trademark
            // 
            this.Trademark.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Trademark.DropDownWidth = 211;
            this.Trademark.Location = new System.Drawing.Point(551, 24);
            this.Trademark.Name = "Trademark";
            this.Trademark.Size = new System.Drawing.Size(280, 21);
            this.Trademark.TabIndex = 4;
            this.Trademark.SelectedIndexChanged += new System.EventHandler(this.Trademark_SelectedIndexChanged);
            // 
            // DrpCategory
            // 
            this.DrpCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DrpCategory.DropDownWidth = 211;
            this.DrpCategory.Location = new System.Drawing.Point(551, 50);
            this.DrpCategory.Name = "DrpCategory";
            this.DrpCategory.Size = new System.Drawing.Size(280, 21);
            this.DrpCategory.TabIndex = 5;
            this.DrpCategory.SelectedIndexChanged += new System.EventHandler(this.DrpCategory_SelectedIndexChanged);
            // 
            // Group
            // 
            this.Group.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Group.DropDownWidth = 211;
            this.Group.Location = new System.Drawing.Point(552, 77);
            this.Group.Name = "Group";
            this.Group.Size = new System.Drawing.Size(280, 21);
            this.Group.TabIndex = 6;
            this.Group.SelectedIndexChanged += new System.EventHandler(this.Group_SelectedIndexChanged);
            // 
            // TYPE
            // 
            this.TYPE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TYPE.DropDownWidth = 211;
            this.TYPE.Location = new System.Drawing.Point(96, 75);
            this.TYPE.Name = "TYPE";
            this.TYPE.Size = new System.Drawing.Size(294, 21);
            this.TYPE.TabIndex = 2;
            this.TYPE.SelectedIndexChanged += new System.EventHandler(this.TYPE_SelectedIndexChanged);
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.Sp_itempricelistBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Sys_Sols_Inventory.reports.Report7.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(838, 354);
            this.reportViewer1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.reportViewer1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 170);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(838, 354);
            this.panel1.TabIndex = 5;
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(493, 77);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(51, 20);
            this.kryptonLabel4.TabIndex = 7;
            this.kryptonLabel4.Values.Text = "Group :";
            this.kryptonLabel4.Paint += new System.Windows.Forms.PaintEventHandler(this.kryptonLabel4_Paint);
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(480, 50);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(66, 20);
            this.kryptonLabel3.TabIndex = 6;
            this.kryptonLabel3.Values.Text = "Category :";
            this.kryptonLabel3.Paint += new System.Windows.Forms.PaintEventHandler(this.kryptonLabel3_Paint);
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(20, 22);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(77, 20);
            this.kryptonLabel2.TabIndex = 5;
            this.kryptonLabel2.Values.Text = "Item Name :";
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(51, 75);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(43, 20);
            this.kryptonLabel1.TabIndex = 4;
            this.kryptonLabel1.Values.Text = "Type :";
            this.kryptonLabel1.Paint += new System.Windows.Forms.PaintEventHandler(this.kryptonLabel1_Paint);
            // 
            // Category
            // 
            this.Category.Controls.Add(this.Send_Mail);
            this.Category.Controls.Add(this.dataGridItem);
            this.Category.Controls.Add(this.Drpsaltype);
            this.Category.Controls.Add(this.kryptonLabel8);
            this.Category.Controls.Add(this.txtCode);
            this.Category.Controls.Add(this.kryptonLabel7);
            this.Category.Controls.Add(this.DrpUOM);
            this.Category.Controls.Add(this.kryptonLabel6);
            this.Category.Controls.Add(this.DESC_ENG);
            this.Category.Controls.Add(this.kryptonLabel5);
            this.Category.Controls.Add(this.btnSearch);
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
            this.Category.Size = new System.Drawing.Size(838, 170);
            this.Category.TabIndex = 4;
            this.Category.TabStop = false;
            this.Category.Text = "Search";
            // 
            // Send_Mail
            // 
            this.Send_Mail.Location = new System.Drawing.Point(736, 139);
            this.Send_Mail.Name = "Send_Mail";
            this.Send_Mail.Size = new System.Drawing.Size(90, 25);
            this.Send_Mail.TabIndex = 114;
            this.Send_Mail.Values.Text = "Send Mail";
            this.Send_Mail.Click += new System.EventHandler(this.Send_Mail_Click);
            // 
            // dataGridItem
            // 
            this.dataGridItem.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridItem.BackgroundColor = System.Drawing.Color.PeachPuff;
            this.dataGridItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridItem.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridItem.GridColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dataGridItem.Location = new System.Drawing.Point(118, 45);
            this.dataGridItem.Name = "dataGridItem";
            this.dataGridItem.Size = new System.Drawing.Size(490, 125);
            this.dataGridItem.TabIndex = 9;
            this.dataGridItem.Visible = false;
            this.dataGridItem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridItem_KeyDown);
            // 
            // Drpsaltype
            // 
            this.Drpsaltype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Drpsaltype.DropDownWidth = 211;
            this.Drpsaltype.Location = new System.Drawing.Point(552, 104);
            this.Drpsaltype.Name = "Drpsaltype";
            this.Drpsaltype.Size = new System.Drawing.Size(279, 21);
            this.Drpsaltype.TabIndex = 7;
            this.Drpsaltype.SelectedIndexChanged += new System.EventHandler(this.Drpsaltype_SelectedIndexChanged);
            // 
            // kryptonLabel8
            // 
            this.kryptonLabel8.Location = new System.Drawing.Point(475, 103);
            this.kryptonLabel8.Name = "kryptonLabel8";
            this.kryptonLabel8.Size = new System.Drawing.Size(73, 20);
            this.kryptonLabel8.TabIndex = 109;
            this.kryptonLabel8.Values.Text = "Price Type :";
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(97, 49);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(293, 20);
            this.txtCode.TabIndex = 1;
            // 
            // kryptonLabel7
            // 
            this.kryptonLabel7.Location = new System.Drawing.Point(23, 48);
            this.kryptonLabel7.Name = "kryptonLabel7";
            this.kryptonLabel7.Size = new System.Drawing.Size(73, 20);
            this.kryptonLabel7.TabIndex = 107;
            this.kryptonLabel7.Values.Text = "Item Code :";
            // 
            // DrpUOM
            // 
            this.DrpUOM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DrpUOM.DropDownWidth = 211;
            this.DrpUOM.Location = new System.Drawing.Point(97, 102);
            this.DrpUOM.Name = "DrpUOM";
            this.DrpUOM.Size = new System.Drawing.Size(293, 21);
            this.DrpUOM.TabIndex = 3;
            this.DrpUOM.SelectedIndexChanged += new System.EventHandler(this.kryptonComboBox1_SelectedIndexChanged);
            // 
            // kryptonLabel6
            // 
            this.kryptonLabel6.Location = new System.Drawing.Point(48, 101);
            this.kryptonLabel6.Name = "kryptonLabel6";
            this.kryptonLabel6.Size = new System.Drawing.Size(45, 20);
            this.kryptonLabel6.TabIndex = 105;
            this.kryptonLabel6.Values.Text = "UOM :";
            this.kryptonLabel6.Paint += new System.Windows.Forms.PaintEventHandler(this.kryptonLabel6_Paint);
            // 
            // Sp_itempricelistTableAdapter
            // 
            this.Sp_itempricelistTableAdapter.ClearBeforeFill = true;
            // 
            // Item_Price_List
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(838, 524);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Category);
            this.Name = "Item_Price_List";
            this.Text = "Item Price List";
            this.Load += new System.EventHandler(this.Item_Price_List_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Sp_itempricelistBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PRICELISTDATASET)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Trademark)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DrpCategory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Group)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TYPE)).EndInit();
            this.panel1.ResumeLayout(false);
            this.Category.ResumeLayout(false);
            this.Category.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Drpsaltype)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DrpUOM)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonTextBox DESC_ENG;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel5;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSearch;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox Trademark;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox DrpCategory;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox Group;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox TYPE;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.Panel panel1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private System.Windows.Forms.GroupBox Category;
        private System.Windows.Forms.BindingSource Sp_itempricelistBindingSource;
        private PRICELISTDATASET PRICELISTDATASET;
        private PRICELISTDATASETTableAdapters.Sp_itempricelistTableAdapter Sp_itempricelistTableAdapter;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox DrpUOM;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel6;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtCode;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel7;
        private System.Windows.Forms.DataGridView dataGridItem;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox Drpsaltype;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel8;
        private ComponentFactory.Krypton.Toolkit.KryptonButton Send_Mail;
    }
}