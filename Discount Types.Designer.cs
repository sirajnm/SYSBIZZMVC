namespace Sys_Sols_Inventory
{
    partial class Discount_Types
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
            this.btnSave = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnClear = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnDelete = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnQuit = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.DESC_ARB = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.DESC_ENG = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.dgItems = new System.Windows.Forms.DataGridView();
            this.VALUE = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.DISCOUNT_TYPE = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.kryptonLabel6 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.PnlArabic = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DISCOUNT_TYPE)).BeginInit();
            this.PnlArabic.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.Location = new System.Drawing.Point(309, 275);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 25);
            this.btnSave.TabIndex = 12;
            this.btnSave.Values.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(405, 275);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(90, 25);
            this.btnClear.TabIndex = 14;
            this.btnClear.Values.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(501, 275);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(90, 25);
            this.btnDelete.TabIndex = 15;
            this.btnDelete.Values.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnQuit
            // 
            this.btnQuit.Location = new System.Drawing.Point(597, 275);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(90, 25);
            this.btnQuit.TabIndex = 17;
            this.btnQuit.Values.Text = "Close";
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // DESC_ARB
            // 
            this.DESC_ARB.Location = new System.Drawing.Point(84, 5);
            this.DESC_ARB.Name = "DESC_ARB";
            this.DESC_ARB.Size = new System.Drawing.Size(177, 20);
            this.DESC_ARB.TabIndex = 11;
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(10, 5);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(71, 20);
            this.kryptonLabel3.TabIndex = 16;
            this.kryptonLabel3.Values.Text = "Arb. Name:";
            // 
            // DESC_ENG
            // 
            this.DESC_ENG.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.DESC_ENG.Location = new System.Drawing.Point(84, 216);
            this.DESC_ENG.Name = "DESC_ENG";
            this.DESC_ENG.Size = new System.Drawing.Size(177, 20);
            this.DESC_ENG.TabIndex = 9;
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(35, 216);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(46, 20);
            this.kryptonLabel2.TabIndex = 13;
            this.kryptonLabel2.Values.Text = "Name:";
            // 
            // dgItems
            // 
            this.dgItems.AllowUserToAddRows = false;
            this.dgItems.AllowUserToDeleteRows = false;
            this.dgItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgItems.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgItems.Location = new System.Drawing.Point(0, 0);
            this.dgItems.Name = "dgItems";
            this.dgItems.ReadOnly = true;
            this.dgItems.Size = new System.Drawing.Size(695, 210);
            this.dgItems.TabIndex = 18;
            this.dgItems.Click += new System.EventHandler(this.dgItems_Click);
            // 
            // VALUE
            // 
            this.VALUE.Location = new System.Drawing.Point(85, 242);
            this.VALUE.Name = "VALUE";
            this.VALUE.Size = new System.Drawing.Size(176, 20);
            this.VALUE.TabIndex = 19;
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(38, 242);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(44, 20);
            this.kryptonLabel1.TabIndex = 20;
            this.kryptonLabel1.Values.Text = "Value:";
            // 
            // DISCOUNT_TYPE
            // 
            this.DISCOUNT_TYPE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DISCOUNT_TYPE.DropDownWidth = 129;
            this.DISCOUNT_TYPE.Items.AddRange(new object[] {
            "Percentage",
            "Amount"});
            this.DISCOUNT_TYPE.Location = new System.Drawing.Point(563, 216);
            this.DISCOUNT_TYPE.Name = "DISCOUNT_TYPE";
            this.DISCOUNT_TYPE.Size = new System.Drawing.Size(124, 21);
            this.DISCOUNT_TYPE.TabIndex = 21;
            // 
            // kryptonLabel6
            // 
            this.kryptonLabel6.Location = new System.Drawing.Point(521, 216);
            this.kryptonLabel6.Name = "kryptonLabel6";
            this.kryptonLabel6.Size = new System.Drawing.Size(39, 20);
            this.kryptonLabel6.TabIndex = 22;
            this.kryptonLabel6.Values.Text = "Type:";
            // 
            // PnlArabic
            // 
            this.PnlArabic.Controls.Add(this.kryptonLabel3);
            this.PnlArabic.Controls.Add(this.DESC_ARB);
            this.PnlArabic.Location = new System.Drawing.Point(256, 212);
            this.PnlArabic.Name = "PnlArabic";
            this.PnlArabic.Size = new System.Drawing.Size(274, 31);
            this.PnlArabic.TabIndex = 23;
            // 
            // Discount_Types
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(695, 308);
            this.Controls.Add(this.DISCOUNT_TYPE);
            this.Controls.Add(this.kryptonLabel6);
            this.Controls.Add(this.VALUE);
            this.Controls.Add(this.kryptonLabel1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnQuit);
            this.Controls.Add(this.DESC_ENG);
            this.Controls.Add(this.kryptonLabel2);
            this.Controls.Add(this.dgItems);
            this.Controls.Add(this.PnlArabic);
            this.Name = "Discount_Types";
            this.Text = "Discount Types";
            this.Load += new System.EventHandler(this.Discount_Types_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DISCOUNT_TYPE)).EndInit();
            this.PnlArabic.ResumeLayout(false);
            this.PnlArabic.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSave;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnClear;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnDelete;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnQuit;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox DESC_ARB;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox DESC_ENG;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private System.Windows.Forms.DataGridView dgItems;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox VALUE;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox DISCOUNT_TYPE;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel6;
        private System.Windows.Forms.Panel PnlArabic;
    }
}