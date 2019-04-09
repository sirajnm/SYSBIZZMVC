namespace Sys_Sols_Inventory
{
    partial class Generate_Barcode
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Generate_Barcode));
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.btngeneratecode = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.panBarcode = new System.Windows.Forms.Panel();
            this.BARCODE = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel30 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.panItem = new System.Windows.Forms.Panel();
            this.btnItemCode = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonLabel9 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.ITEM_NAME = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel8 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.ITEM_CODE = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.panUnit = new System.Windows.Forms.Panel();
            this.PRICE = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.lblPriceType = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonButton1 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonButton2 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.lblbarcode = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.PrintFormat = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.dgRates = new System.Windows.Forms.DataGridView();
            this.rUnit = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.rQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rBarcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kryptonButton3 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.panBarcode.SuspendLayout();
            this.panItem.SuspendLayout();
            this.panUnit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PrintFormat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgRates)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(15, 243);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(86, 20);
            this.kryptonLabel3.TabIndex = 17;
            this.kryptonLabel3.Values.Text = "Barcode Data:";
            // 
            // btngeneratecode
            // 
            this.btngeneratecode.Location = new System.Drawing.Point(15, 280);
            this.btngeneratecode.Name = "btngeneratecode";
            this.btngeneratecode.Size = new System.Drawing.Size(132, 25);
            this.btngeneratecode.TabIndex = 19;
            this.btngeneratecode.Values.Text = "Gnerate";
            this.btngeneratecode.Visible = false;
            this.btngeneratecode.Click += new System.EventHandler(this.btngeneratecode_Click);
            // 
            // panBarcode
            // 
            this.panBarcode.Controls.Add(this.BARCODE);
            this.panBarcode.Controls.Add(this.kryptonLabel30);
            this.panBarcode.Location = new System.Drawing.Point(15, 16);
            this.panBarcode.Name = "panBarcode";
            this.panBarcode.Size = new System.Drawing.Size(183, 53);
            this.panBarcode.TabIndex = 79;
            // 
            // BARCODE
            // 
            this.BARCODE.Location = new System.Drawing.Point(3, 29);
            this.BARCODE.Name = "BARCODE";
            this.BARCODE.Size = new System.Drawing.Size(177, 20);
            this.BARCODE.TabIndex = 1;
            this.BARCODE.TextChanged += new System.EventHandler(this.BARCODE_TextChanged);
            // 
            // kryptonLabel30
            // 
            this.kryptonLabel30.Location = new System.Drawing.Point(3, 3);
            this.kryptonLabel30.Name = "kryptonLabel30";
            this.kryptonLabel30.Size = new System.Drawing.Size(58, 20);
            this.kryptonLabel30.TabIndex = 0;
            this.kryptonLabel30.Values.Text = "Barcode:";
            // 
            // panItem
            // 
            this.panItem.Controls.Add(this.btnItemCode);
            this.panItem.Controls.Add(this.kryptonLabel9);
            this.panItem.Controls.Add(this.ITEM_NAME);
            this.panItem.Controls.Add(this.kryptonLabel8);
            this.panItem.Controls.Add(this.ITEM_CODE);
            this.panItem.Location = new System.Drawing.Point(204, 16);
            this.panItem.Name = "panItem";
            this.panItem.Size = new System.Drawing.Size(350, 54);
            this.panItem.TabIndex = 78;
            // 
            // btnItemCode
            // 
            this.btnItemCode.Location = new System.Drawing.Point(132, 26);
            this.btnItemCode.Name = "btnItemCode";
            this.btnItemCode.Size = new System.Drawing.Size(22, 24);
            this.btnItemCode.TabIndex = 22;
            this.btnItemCode.Values.Text = "...";
            this.btnItemCode.Click += new System.EventHandler(this.btnItemCode_Click);
            // 
            // kryptonLabel9
            // 
            this.kryptonLabel9.Location = new System.Drawing.Point(157, 7);
            this.kryptonLabel9.Name = "kryptonLabel9";
            this.kryptonLabel9.Size = new System.Drawing.Size(74, 20);
            this.kryptonLabel9.TabIndex = 21;
            this.kryptonLabel9.Values.Text = "Item Name:";
            // 
            // ITEM_NAME
            // 
            this.ITEM_NAME.Location = new System.Drawing.Point(163, 29);
            this.ITEM_NAME.Name = "ITEM_NAME";
            this.ITEM_NAME.ReadOnly = true;
            this.ITEM_NAME.Size = new System.Drawing.Size(174, 20);
            this.ITEM_NAME.TabIndex = 22;
            // 
            // kryptonLabel8
            // 
            this.kryptonLabel8.Location = new System.Drawing.Point(3, 4);
            this.kryptonLabel8.Name = "kryptonLabel8";
            this.kryptonLabel8.Size = new System.Drawing.Size(42, 20);
            this.kryptonLabel8.TabIndex = 19;
            this.kryptonLabel8.Values.Text = "Code:";
            // 
            // ITEM_CODE
            // 
            this.ITEM_CODE.Location = new System.Drawing.Point(7, 28);
            this.ITEM_CODE.Name = "ITEM_CODE";
            this.ITEM_CODE.ReadOnly = true;
            this.ITEM_CODE.Size = new System.Drawing.Size(121, 20);
            this.ITEM_CODE.TabIndex = 20;
            // 
            // panUnit
            // 
            this.panUnit.Controls.Add(this.PRICE);
            this.panUnit.Controls.Add(this.lblPriceType);
            this.panUnit.Location = new System.Drawing.Point(557, 16);
            this.panUnit.Name = "panUnit";
            this.panUnit.Size = new System.Drawing.Size(130, 54);
            this.panUnit.TabIndex = 80;
            // 
            // PRICE
            // 
            this.PRICE.Location = new System.Drawing.Point(7, 28);
            this.PRICE.Name = "PRICE";
            this.PRICE.Size = new System.Drawing.Size(110, 20);
            this.PRICE.TabIndex = 33;
            // 
            // lblPriceType
            // 
            this.lblPriceType.Location = new System.Drawing.Point(7, 2);
            this.lblPriceType.Name = "lblPriceType";
            this.lblPriceType.Size = new System.Drawing.Size(6, 2);
            this.lblPriceType.TabIndex = 32;
            this.lblPriceType.Values.Text = "";
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.Location = new System.Drawing.Point(491, 268);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.Size = new System.Drawing.Size(89, 25);
            this.kryptonButton1.TabIndex = 81;
            this.kryptonButton1.Values.Text = "Print";
            this.kryptonButton1.Click += new System.EventHandler(this.kryptonButton1_Click);
            // 
            // kryptonButton2
            // 
            this.kryptonButton2.Location = new System.Drawing.Point(586, 268);
            this.kryptonButton2.Name = "kryptonButton2";
            this.kryptonButton2.Size = new System.Drawing.Size(94, 25);
            this.kryptonButton2.TabIndex = 82;
            this.kryptonButton2.Values.Text = "Close";
            this.kryptonButton2.Click += new System.EventHandler(this.kryptonButton2_Click);
            // 
            // lblbarcode
            // 
            this.lblbarcode.Location = new System.Drawing.Point(100, 244);
            this.lblbarcode.Name = "lblbarcode";
            this.lblbarcode.Size = new System.Drawing.Size(6, 2);
            this.lblbarcode.TabIndex = 83;
            this.lblbarcode.Values.Text = "";
            // 
            // PrintFormat
            // 
            this.PrintFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PrintFormat.DropDownWidth = 211;
            this.PrintFormat.Items.AddRange(new object[] {
            "Thermal Printer"});
            this.PrintFormat.Location = new System.Drawing.Point(491, 240);
            this.PrintFormat.Name = "PrintFormat";
            this.PrintFormat.Size = new System.Drawing.Size(190, 21);
            this.PrintFormat.TabIndex = 84;
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(404, 239);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(84, 20);
            this.kryptonLabel1.TabIndex = 85;
            this.kryptonLabel1.Values.Text = "Print Format :";
            // 
            // dgRates
            // 
            this.dgRates.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgRates.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.rUnit,
            this.rQty,
            this.rBarcode});
            this.dgRates.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgRates.Location = new System.Drawing.Point(14, 91);
            this.dgRates.Name = "dgRates";
            this.dgRates.Size = new System.Drawing.Size(673, 133);
            this.dgRates.TabIndex = 86;
            this.dgRates.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgRates_CellClick);
            // 
            // rUnit
            // 
            this.rUnit.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.rUnit.HeaderText = "Unit";
            this.rUnit.Name = "rUnit";
            this.rUnit.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // rQty
            // 
            dataGridViewCellStyle1.NullValue = null;
            this.rQty.DefaultCellStyle = dataGridViewCellStyle1;
            this.rQty.HeaderText = "Qty";
            this.rQty.Name = "rQty";
            // 
            // rBarcode
            // 
            this.rBarcode.HeaderText = "Barcode";
            this.rBarcode.Name = "rBarcode";
            // 
            // kryptonButton3
            // 
            this.kryptonButton3.Location = new System.Drawing.Point(18, 311);
            this.kryptonButton3.Name = "kryptonButton3";
            this.kryptonButton3.Size = new System.Drawing.Size(53, 26);
            this.kryptonButton3.TabIndex = 81;
            this.kryptonButton3.Values.Text = "Print";
            this.kryptonButton3.Visible = false;
            this.kryptonButton3.Click += new System.EventHandler(this.kryptonButton3_Click);
            // 
            // Generate_Barcode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(768, 378);
            this.Controls.Add(this.dgRates);
            this.Controls.Add(this.kryptonLabel1);
            this.Controls.Add(this.PrintFormat);
            this.Controls.Add(this.lblbarcode);
            this.Controls.Add(this.kryptonButton2);
            this.Controls.Add(this.kryptonButton3);
            this.Controls.Add(this.kryptonButton1);
            this.Controls.Add(this.panUnit);
            this.Controls.Add(this.panBarcode);
            this.Controls.Add(this.panItem);
            this.Controls.Add(this.btngeneratecode);
            this.Controls.Add(this.kryptonLabel3);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Generate_Barcode";
            this.Text = "Generate Barcode";
            this.Load += new System.EventHandler(this.Generate_Barcode_Load);
            this.panBarcode.ResumeLayout(false);
            this.panBarcode.PerformLayout();
            this.panItem.ResumeLayout(false);
            this.panItem.PerformLayout();
            this.panUnit.ResumeLayout(false);
            this.panUnit.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PrintFormat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgRates)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btngeneratecode;
        private System.Windows.Forms.Panel panBarcode;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox BARCODE;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel30;
        private System.Windows.Forms.Panel panItem;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnItemCode;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel9;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox ITEM_NAME;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel8;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox ITEM_CODE;
        private System.Windows.Forms.Panel panUnit;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox PRICE;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblPriceType;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton2;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblbarcode;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox PrintFormat;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private System.Windows.Forms.DataGridView dgRates;
        private System.Windows.Forms.DataGridViewComboBoxColumn rUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn rQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn rBarcode;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton3;
    }
}