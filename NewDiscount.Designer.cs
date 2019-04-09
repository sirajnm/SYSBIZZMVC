namespace Sys_Sols_Inventory
{
    partial class NewDiscount
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.DISCOUNT_ARB = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.DISCOUNT_ENG = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.START_DATE = new System.Windows.Forms.DateTimePicker();
            this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.END_DATE = new System.Windows.Forms.DateTimePicker();
            this.kryptonLabel5 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.ORDER = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel6 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.IN_ACTIVE = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.kryptonLabel13 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.PnlArabic = new System.Windows.Forms.Panel();
            this.kryptonButton1 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.dataGridItem = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvItems = new System.Windows.Forms.DataGridView();
            this.Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Uom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SalType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiscountType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.linkRemoveUnit = new ComponentFactory.Krypton.Toolkit.KryptonLinkLabel();
            this.kryptonLabel29 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.RATE_CODE = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnapply = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.TYPE = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.kryptonLabel9 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.VAL = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel8 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel7 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.ITEM_NAME = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.BARCODE = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.btnSave = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnClose = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnClear = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnDelete = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.groupBox1.SuspendLayout();
            this.PnlArabic.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridItem)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RATE_CODE)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TYPE)).BeginInit();
            this.SuspendLayout();
            // 
            // DISCOUNT_ARB
            // 
            this.DISCOUNT_ARB.Location = new System.Drawing.Point(84, 7);
            this.DISCOUNT_ARB.Name = "DISCOUNT_ARB";
            this.DISCOUNT_ARB.Size = new System.Drawing.Size(177, 20);
            this.DISCOUNT_ARB.TabIndex = 1;
            this.DISCOUNT_ARB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DISCOUNT_ENG_KeyDown);
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(7, 7);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(75, 20);
            this.kryptonLabel3.TabIndex = 9;
            this.kryptonLabel3.Values.Text = "Arb. Name :";
            // 
            // DISCOUNT_ENG
            // 
            this.DISCOUNT_ENG.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.DISCOUNT_ENG.Location = new System.Drawing.Point(100, 19);
            this.DISCOUNT_ENG.Name = "DISCOUNT_ENG";
            this.DISCOUNT_ENG.Size = new System.Drawing.Size(177, 20);
            this.DISCOUNT_ENG.TabIndex = 0;
            this.DISCOUNT_ENG.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DISCOUNT_ENG_KeyDown);
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(32, 19);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(49, 20);
            this.kryptonLabel2.TabIndex = 7;
            this.kryptonLabel2.Values.Text = "Name :";
            // 
            // START_DATE
            // 
            this.START_DATE.CustomFormat = "dd/MM/yyyy";
            this.START_DATE.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.START_DATE.Location = new System.Drawing.Point(100, 48);
            this.START_DATE.Name = "START_DATE";
            this.START_DATE.Size = new System.Drawing.Size(177, 20);
            this.START_DATE.TabIndex = 2;
            this.START_DATE.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DISCOUNT_ENG_KeyDown);
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(26, 47);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(71, 20);
            this.kryptonLabel4.TabIndex = 61;
            this.kryptonLabel4.Values.Text = "Start Date :";
            // 
            // END_DATE
            // 
            this.END_DATE.CustomFormat = "dd/MM/yyyy";
            this.END_DATE.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.END_DATE.Location = new System.Drawing.Point(410, 47);
            this.END_DATE.Name = "END_DATE";
            this.END_DATE.Size = new System.Drawing.Size(176, 20);
            this.END_DATE.TabIndex = 3;
            this.END_DATE.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DISCOUNT_ENG_KeyDown);
            // 
            // kryptonLabel5
            // 
            this.kryptonLabel5.Location = new System.Drawing.Point(329, 47);
            this.kryptonLabel5.Name = "kryptonLabel5";
            this.kryptonLabel5.Size = new System.Drawing.Size(66, 20);
            this.kryptonLabel5.TabIndex = 63;
            this.kryptonLabel5.Values.Text = "End Date :";
            // 
            // ORDER
            // 
            this.ORDER.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.ORDER.Location = new System.Drawing.Point(100, 76);
            this.ORDER.Name = "ORDER";
            this.ORDER.Size = new System.Drawing.Size(177, 20);
            this.ORDER.TabIndex = 4;
            this.ORDER.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DISCOUNT_ENG_KeyDown);
            // 
            // kryptonLabel6
            // 
            this.kryptonLabel6.Location = new System.Drawing.Point(26, 76);
            this.kryptonLabel6.Name = "kryptonLabel6";
            this.kryptonLabel6.Size = new System.Drawing.Size(48, 20);
            this.kryptonLabel6.TabIndex = 65;
            this.kryptonLabel6.Values.Text = "Order :";
            // 
            // IN_ACTIVE
            // 
            this.IN_ACTIVE.Location = new System.Drawing.Point(412, 77);
            this.IN_ACTIVE.Name = "IN_ACTIVE";
            this.IN_ACTIVE.Size = new System.Drawing.Size(42, 20);
            this.IN_ACTIVE.TabIndex = 67;
            this.IN_ACTIVE.Values.Text = "Yes";
            this.IN_ACTIVE.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DISCOUNT_ENG_KeyDown);
            // 
            // kryptonLabel13
            // 
            this.kryptonLabel13.Location = new System.Drawing.Point(329, 76);
            this.kryptonLabel13.Name = "kryptonLabel13";
            this.kryptonLabel13.Size = new System.Drawing.Size(60, 20);
            this.kryptonLabel13.TabIndex = 68;
            this.kryptonLabel13.Values.Text = "InActive :";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.PnlArabic);
            this.groupBox1.Controls.Add(this.kryptonButton1);
            this.groupBox1.Controls.Add(this.kryptonLabel2);
            this.groupBox1.Controls.Add(this.END_DATE);
            this.groupBox1.Controls.Add(this.IN_ACTIVE);
            this.groupBox1.Controls.Add(this.kryptonLabel5);
            this.groupBox1.Controls.Add(this.DISCOUNT_ENG);
            this.groupBox1.Controls.Add(this.START_DATE);
            this.groupBox1.Controls.Add(this.kryptonLabel13);
            this.groupBox1.Controls.Add(this.kryptonLabel4);
            this.groupBox1.Controls.Add(this.ORDER);
            this.groupBox1.Controls.Add(this.kryptonLabel6);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(774, 108);
            this.groupBox1.TabIndex = 69;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Discount Details";
            // 
            // PnlArabic
            // 
            this.PnlArabic.Controls.Add(this.DISCOUNT_ARB);
            this.PnlArabic.Controls.Add(this.kryptonLabel3);
            this.PnlArabic.Location = new System.Drawing.Point(325, 11);
            this.PnlArabic.Name = "PnlArabic";
            this.PnlArabic.Size = new System.Drawing.Size(338, 33);
            this.PnlArabic.TabIndex = 73;
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.Location = new System.Drawing.Point(280, 17);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.Size = new System.Drawing.Size(29, 24);
            this.kryptonButton1.TabIndex = 72;
            this.kryptonButton1.Values.Text = "---";
            this.kryptonButton1.Click += new System.EventHandler(this.kryptonButton1_Click);
            // 
            // dataGridItem
            // 
            this.dataGridItem.AllowUserToAddRows = false;
            this.dataGridItem.AllowUserToDeleteRows = false;
            this.dataGridItem.BackgroundColor = System.Drawing.Color.PeachPuff;
            this.dataGridItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.IndianRed;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridItem.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridItem.Location = new System.Drawing.Point(312, 93);
            this.dataGridItem.Name = "dataGridItem";
            this.dataGridItem.ReadOnly = true;
            this.dataGridItem.RowHeadersVisible = false;
            this.dataGridItem.Size = new System.Drawing.Size(436, 132);
            this.dataGridItem.TabIndex = 100;
            this.dataGridItem.Visible = false;
            this.dataGridItem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridItem_KeyDown);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridItem);
            this.groupBox2.Controls.Add(this.dgvItems);
            this.groupBox2.Controls.Add(this.linkRemoveUnit);
            this.groupBox2.Controls.Add(this.kryptonLabel29);
            this.groupBox2.Controls.Add(this.RATE_CODE);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.kryptonLabel7);
            this.groupBox2.Controls.Add(this.ITEM_NAME);
            this.groupBox2.Controls.Add(this.kryptonLabel1);
            this.groupBox2.Controls.Add(this.BARCODE);
            this.groupBox2.Location = new System.Drawing.Point(12, 133);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(774, 354);
            this.groupBox2.TabIndex = 70;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Select Items";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // dgvItems
            // 
            this.dgvItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Code,
            this.ItemName,
            this.Uom,
            this.SalType,
            this.DiscountType,
            this.Value});
            this.dgvItems.Location = new System.Drawing.Point(47, 83);
            this.dgvItems.Name = "dgvItems";
            this.dgvItems.Size = new System.Drawing.Size(701, 249);
            this.dgvItems.TabIndex = 3;
            // 
            // Code
            // 
            this.Code.HeaderText = "item Code";
            this.Code.Name = "Code";
            this.Code.ReadOnly = true;
            // 
            // ItemName
            // 
            this.ItemName.HeaderText = "ItemName";
            this.ItemName.Name = "ItemName";
            this.ItemName.ReadOnly = true;
            this.ItemName.Width = 150;
            // 
            // Uom
            // 
            this.Uom.HeaderText = "Unit";
            this.Uom.Name = "Uom";
            this.Uom.ReadOnly = true;
            this.Uom.Width = 80;
            // 
            // SalType
            // 
            this.SalType.HeaderText = "SalesType";
            this.SalType.Name = "SalType";
            this.SalType.ReadOnly = true;
            // 
            // DiscountType
            // 
            this.DiscountType.HeaderText = "Discount Type";
            this.DiscountType.Items.AddRange(new object[] {
            "Percentage",
            "Amount"});
            this.DiscountType.Name = "DiscountType";
            // 
            // Value
            // 
            this.Value.HeaderText = "Value";
            this.Value.Name = "Value";
            // 
            // linkRemoveUnit
            // 
            this.linkRemoveUnit.Location = new System.Drawing.Point(47, 330);
            this.linkRemoveUnit.Name = "linkRemoveUnit";
            this.linkRemoveUnit.Size = new System.Drawing.Size(83, 20);
            this.linkRemoveUnit.TabIndex = 103;
            this.linkRemoveUnit.Values.Text = "Remove Item";
            this.linkRemoveUnit.LinkClicked += new System.EventHandler(this.linkRemoveUnit_LinkClicked);
            // 
            // kryptonLabel29
            // 
            this.kryptonLabel29.Location = new System.Drawing.Point(42, 18);
            this.kryptonLabel29.Name = "kryptonLabel29";
            this.kryptonLabel29.Size = new System.Drawing.Size(70, 20);
            this.kryptonLabel29.TabIndex = 101;
            this.kryptonLabel29.Values.Text = "Rate Type :";
            // 
            // RATE_CODE
            // 
            this.RATE_CODE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RATE_CODE.DropDownWidth = 114;
            this.RATE_CODE.Location = new System.Drawing.Point(115, 18);
            this.RATE_CODE.Name = "RATE_CODE";
            this.RATE_CODE.Size = new System.Drawing.Size(114, 21);
            this.RATE_CODE.TabIndex = 0;
            this.RATE_CODE.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DISCOUNT_ENG_KeyDown);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnapply);
            this.groupBox3.Controls.Add(this.TYPE);
            this.groupBox3.Controls.Add(this.kryptonLabel9);
            this.groupBox3.Controls.Add(this.VAL);
            this.groupBox3.Controls.Add(this.kryptonLabel8);
            this.groupBox3.Location = new System.Drawing.Point(477, 7);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(277, 70);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Apply to all";
            // 
            // btnapply
            // 
            this.btnapply.Location = new System.Drawing.Point(207, 38);
            this.btnapply.Name = "btnapply";
            this.btnapply.Size = new System.Drawing.Size(64, 25);
            this.btnapply.TabIndex = 2;
            this.btnapply.Values.Text = "Apply";
            this.btnapply.Click += new System.EventHandler(this.btnapply_Click);
            // 
            // TYPE
            // 
            this.TYPE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TYPE.DropDownWidth = 211;
            this.TYPE.Items.AddRange(new object[] {
            "Percentage",
            "Amount"});
            this.TYPE.Location = new System.Drawing.Point(69, 17);
            this.TYPE.Name = "TYPE";
            this.TYPE.Size = new System.Drawing.Size(131, 21);
            this.TYPE.TabIndex = 0;
            // 
            // kryptonLabel9
            // 
            this.kryptonLabel9.Location = new System.Drawing.Point(4, 43);
            this.kryptonLabel9.Name = "kryptonLabel9";
            this.kryptonLabel9.Size = new System.Drawing.Size(47, 20);
            this.kryptonLabel9.TabIndex = 17;
            this.kryptonLabel9.Values.Text = "Value :";
            // 
            // VAL
            // 
            this.VAL.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.VAL.Location = new System.Drawing.Point(69, 43);
            this.VAL.Name = "VAL";
            this.VAL.Size = new System.Drawing.Size(132, 20);
            this.VAL.TabIndex = 1;
            // 
            // kryptonLabel8
            // 
            this.kryptonLabel8.Location = new System.Drawing.Point(5, 17);
            this.kryptonLabel8.Name = "kryptonLabel8";
            this.kryptonLabel8.Size = new System.Drawing.Size(43, 20);
            this.kryptonLabel8.TabIndex = 15;
            this.kryptonLabel8.Values.Text = "Type :";
            // 
            // kryptonLabel7
            // 
            this.kryptonLabel7.Location = new System.Drawing.Point(234, 50);
            this.kryptonLabel7.Name = "kryptonLabel7";
            this.kryptonLabel7.Size = new System.Drawing.Size(77, 20);
            this.kryptonLabel7.TabIndex = 11;
            this.kryptonLabel7.Values.Text = "Item Name :";
            // 
            // ITEM_NAME
            // 
            this.ITEM_NAME.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.ITEM_NAME.Location = new System.Drawing.Point(312, 50);
            this.ITEM_NAME.Name = "ITEM_NAME";
            this.ITEM_NAME.Size = new System.Drawing.Size(156, 20);
            this.ITEM_NAME.TabIndex = 2;
            this.ITEM_NAME.TextChanged += new System.EventHandler(this.ITEM_NAME_TextChanged);
            this.ITEM_NAME.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ITEM_NAME_KeyDown);
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(41, 50);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(61, 20);
            this.kryptonLabel1.TabIndex = 9;
            this.kryptonLabel1.Values.Text = "Barcode : ";
            // 
            // BARCODE
            // 
            this.BARCODE.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.BARCODE.Location = new System.Drawing.Point(114, 50);
            this.BARCODE.Name = "BARCODE";
            this.BARCODE.Size = new System.Drawing.Size(115, 20);
            this.BARCODE.TabIndex = 1;
            this.BARCODE.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BARCODE_KeyDown);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(436, 491);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(84, 25);
            this.btnSave.TabIndex = 71;
            this.btnSave.Values.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(702, 491);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(84, 25);
            this.btnClose.TabIndex = 72;
            this.btnClose.Values.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(612, 491);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(84, 25);
            this.btnClear.TabIndex = 73;
            this.btnClear.Values.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(524, 492);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(84, 25);
            this.btnDelete.TabIndex = 74;
            this.btnDelete.Values.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // NewDiscount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 519);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Name = "NewDiscount";
            this.Text = "New Discount";
            this.Load += new System.EventHandler(this.NewDiscount_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.PnlArabic.ResumeLayout(false);
            this.PnlArabic.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridItem)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RATE_CODE)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TYPE)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonTextBox DISCOUNT_ARB;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox DISCOUNT_ENG;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private System.Windows.Forms.DateTimePicker START_DATE;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private System.Windows.Forms.DateTimePicker END_DATE;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel5;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox ORDER;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel6;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox IN_ACTIVE;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel13;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSave;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel7;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox ITEM_NAME;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox BARCODE;
        private System.Windows.Forms.DataGridView dgvItems;
        private System.Windows.Forms.GroupBox groupBox3;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel9;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox VAL;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel8;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox TYPE;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnapply;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnClose;
        private System.Windows.Forms.DataGridView dataGridItem;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnClear;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnDelete;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel29;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox RATE_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Uom;
        private System.Windows.Forms.DataGridViewTextBoxColumn SalType;
        private System.Windows.Forms.DataGridViewComboBoxColumn DiscountType;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private ComponentFactory.Krypton.Toolkit.KryptonLinkLabel linkRemoveUnit;
        private System.Windows.Forms.Panel PnlArabic;
    }
}