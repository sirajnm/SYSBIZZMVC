namespace Sys_Sols_Inventory
{
    partial class CustomerType
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomerType));
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.CODE = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.DESC_ENG = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.DESC_ARB = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.CREDIT_LEVEL = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel5 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel6 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.DISCOUNT_TYPE = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.PRICE_TYPE = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.btnQuit = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnDelete = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnClear = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnSave = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnCode = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.PnlArabic = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.DISCOUNT_TYPE)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PRICE_TYPE)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.PnlArabic.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(13, 38);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(42, 20);
            this.kryptonLabel1.TabIndex = 0;
            this.kryptonLabel1.Values.Text = "Code:";
            // 
            // CODE
            // 
            this.CODE.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.CODE.Location = new System.Drawing.Point(113, 38);
            this.CODE.MaxLength = 3;
            this.CODE.Name = "CODE";
            this.CODE.Size = new System.Drawing.Size(76, 20);
            this.CODE.TabIndex = 0;
            this.CODE.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CODE_KeyDown);
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(14, 64);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(46, 20);
            this.kryptonLabel2.TabIndex = 2;
            this.kryptonLabel2.Values.Text = "Name:";
            // 
            // DESC_ENG
            // 
            this.DESC_ENG.Location = new System.Drawing.Point(113, 64);
            this.DESC_ENG.Name = "DESC_ENG";
            this.DESC_ENG.Size = new System.Drawing.Size(340, 20);
            this.DESC_ENG.TabIndex = 1;
            this.DESC_ENG.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CODE_KeyDown);
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(0, 3);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(71, 20);
            this.kryptonLabel3.TabIndex = 4;
            this.kryptonLabel3.Values.Text = "Arb. Name:";
            // 
            // DESC_ARB
            // 
            this.DESC_ARB.Location = new System.Drawing.Point(100, 3);
            this.DESC_ARB.Name = "DESC_ARB";
            this.DESC_ARB.Size = new System.Drawing.Size(340, 20);
            this.DESC_ARB.TabIndex = 2;
            this.DESC_ARB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CODE_KeyDown);
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(13, 116);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(77, 20);
            this.kryptonLabel4.TabIndex = 6;
            this.kryptonLabel4.Values.Text = "Credit Level:";
            // 
            // CREDIT_LEVEL
            // 
            this.CREDIT_LEVEL.Location = new System.Drawing.Point(113, 116);
            this.CREDIT_LEVEL.Name = "CREDIT_LEVEL";
            this.CREDIT_LEVEL.Size = new System.Drawing.Size(113, 20);
            this.CREDIT_LEVEL.TabIndex = 3;
            this.CREDIT_LEVEL.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CODE_KeyDown);
            // 
            // kryptonLabel5
            // 
            this.kryptonLabel5.Location = new System.Drawing.Point(231, 116);
            this.kryptonLabel5.Name = "kryptonLabel5";
            this.kryptonLabel5.Size = new System.Drawing.Size(69, 20);
            this.kryptonLabel5.TabIndex = 8;
            this.kryptonLabel5.Values.Text = "Price Type:";
            // 
            // kryptonLabel6
            // 
            this.kryptonLabel6.Location = new System.Drawing.Point(13, 144);
            this.kryptonLabel6.Name = "kryptonLabel6";
            this.kryptonLabel6.Size = new System.Drawing.Size(91, 20);
            this.kryptonLabel6.TabIndex = 10;
            this.kryptonLabel6.Values.Text = "Discount Type:";
            // 
            // DISCOUNT_TYPE
            // 
            this.DISCOUNT_TYPE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DISCOUNT_TYPE.DropDownWidth = 129;
            this.DISCOUNT_TYPE.Location = new System.Drawing.Point(113, 144);
            this.DISCOUNT_TYPE.Name = "DISCOUNT_TYPE";
            this.DISCOUNT_TYPE.Size = new System.Drawing.Size(340, 21);
            this.DISCOUNT_TYPE.TabIndex = 5;
            this.DISCOUNT_TYPE.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CODE_KeyDown);
            // 
            // PRICE_TYPE
            // 
            this.PRICE_TYPE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PRICE_TYPE.DropDownWidth = 129;
            this.PRICE_TYPE.Location = new System.Drawing.Point(306, 117);
            this.PRICE_TYPE.Name = "PRICE_TYPE";
            this.PRICE_TYPE.Size = new System.Drawing.Size(147, 21);
            this.PRICE_TYPE.TabIndex = 4;
            this.PRICE_TYPE.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CODE_KeyDown);
            // 
            // btnQuit
            // 
            this.btnQuit.Location = new System.Drawing.Point(363, 171);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(90, 25);
            this.btnQuit.TabIndex = 9;
            this.btnQuit.Values.Text = "Close";
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(267, 171);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(90, 25);
            this.btnDelete.TabIndex = 8;
            this.btnDelete.Values.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(171, 171);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(90, 25);
            this.btnClear.TabIndex = 7;
            this.btnClear.Values.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(75, 171);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 25);
            this.btnSave.TabIndex = 6;
            this.btnSave.Values.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCode
            // 
            this.btnCode.Location = new System.Drawing.Point(192, 35);
            this.btnCode.Name = "btnCode";
            this.btnCode.Size = new System.Drawing.Size(20, 25);
            this.btnCode.TabIndex = 17;
            this.btnCode.Values.Text = "...";
            this.btnCode.Click += new System.EventHandler(this.btnCode_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 202);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(696, 215);
            this.dataGridView1.TabIndex = 18;
            this.dataGridView1.Click += new System.EventHandler(this.dataGridView1_Click);
            // 
            // PnlArabic
            // 
            this.PnlArabic.Controls.Add(this.DESC_ARB);
            this.PnlArabic.Controls.Add(this.kryptonLabel3);
            this.PnlArabic.Location = new System.Drawing.Point(13, 86);
            this.PnlArabic.Name = "PnlArabic";
            this.PnlArabic.Size = new System.Drawing.Size(463, 25);
            this.PnlArabic.TabIndex = 19;
            // 
            // CustomerType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(863, 429);
            this.Controls.Add(this.PnlArabic);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnCode);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnQuit);
            this.Controls.Add(this.PRICE_TYPE);
            this.Controls.Add(this.DISCOUNT_TYPE);
            this.Controls.Add(this.kryptonLabel6);
            this.Controls.Add(this.kryptonLabel5);
            this.Controls.Add(this.CREDIT_LEVEL);
            this.Controls.Add(this.kryptonLabel4);
            this.Controls.Add(this.DESC_ENG);
            this.Controls.Add(this.kryptonLabel2);
            this.Controls.Add(this.CODE);
            this.Controls.Add(this.kryptonLabel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CustomerType";
            this.Text = "Customer Type";
            this.Load += new System.EventHandler(this.CustomerType_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DISCOUNT_TYPE)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PRICE_TYPE)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.PnlArabic.ResumeLayout(false);
            this.PnlArabic.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox CODE;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox DESC_ENG;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox DESC_ARB;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox CREDIT_LEVEL;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel5;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel6;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox DISCOUNT_TYPE;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox PRICE_TYPE;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnQuit;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnDelete;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnClear;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSave;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnCode;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel PnlArabic;
    }
}

