namespace Sys_Sols_Inventory
{
    partial class Tax_Type
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Tax_Type));
            this.btnSave = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnClear = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnDelete = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnQuit = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.DESC_ARB = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.DESC_ENG = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.CODE = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.dgCommon = new System.Windows.Forms.DataGridView();
            this.txttaxrate = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.PnlArabic = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgCommon)).BeginInit();
            this.PnlArabic.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(326, 311);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 25);
            this.btnSave.TabIndex = 4;
            this.btnSave.Values.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(422, 311);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(90, 25);
            this.btnClear.TabIndex = 5;
            this.btnClear.Values.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(518, 311);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(90, 25);
            this.btnDelete.TabIndex = 6;
            this.btnDelete.Values.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnQuit
            // 
            this.btnQuit.Location = new System.Drawing.Point(614, 311);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(90, 25);
            this.btnQuit.TabIndex = 7;
            this.btnQuit.Values.Text = "Close";
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // DESC_ARB
            // 
            this.DESC_ARB.Location = new System.Drawing.Point(98, 5);
            this.DESC_ARB.Name = "DESC_ARB";
            this.DESC_ARB.Size = new System.Drawing.Size(177, 20);
            this.DESC_ARB.TabIndex = 2;
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(8, 5);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(93, 20);
            this.kryptonLabel3.TabIndex = 16;
            this.kryptonLabel3.Values.Text = "Arb. Tax Name:";
            // 
            // DESC_ENG
            // 
            this.DESC_ENG.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.DESC_ENG.Location = new System.Drawing.Point(254, 263);
            this.DESC_ENG.Name = "DESC_ENG";
            this.DESC_ENG.Size = new System.Drawing.Size(177, 20);
            this.DESC_ENG.TabIndex = 1;
            this.DESC_ENG.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DESC_ENG_KeyDown);
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(154, 263);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(68, 20);
            this.kryptonLabel2.TabIndex = 14;
            this.kryptonLabel2.Values.Text = "Tax Name:";
            // 
            // CODE
            // 
            this.CODE.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.CODE.Location = new System.Drawing.Point(63, 263);
            this.CODE.MaxLength = 3;
            this.CODE.Name = "CODE";
            this.CODE.Size = new System.Drawing.Size(67, 20);
            this.CODE.TabIndex = 0;
            this.CODE.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CODE_KeyDown);
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(15, 263);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(42, 20);
            this.kryptonLabel1.TabIndex = 12;
            this.kryptonLabel1.Values.Text = "Code:";
            // 
            // dgCommon
            // 
            this.dgCommon.AllowUserToAddRows = false;
            this.dgCommon.AllowUserToDeleteRows = false;
            this.dgCommon.BackgroundColor = System.Drawing.Color.White;
            this.dgCommon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgCommon.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgCommon.Location = new System.Drawing.Point(0, 0);
            this.dgCommon.Name = "dgCommon";
            this.dgCommon.ReadOnly = true;
            this.dgCommon.Size = new System.Drawing.Size(764, 246);
            this.dgCommon.TabIndex = 8;
            this.dgCommon.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgCommon_CellClick);
            // 
            // txttaxrate
            // 
            this.txttaxrate.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txttaxrate.Location = new System.Drawing.Point(254, 289);
            this.txttaxrate.MaxLength = 150;
            this.txttaxrate.Name = "txttaxrate";
            this.txttaxrate.Size = new System.Drawing.Size(67, 20);
            this.txttaxrate.TabIndex = 3;
            this.txttaxrate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txttaxrate_KeyDown);
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(154, 289);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(59, 20);
            this.kryptonLabel4.TabIndex = 10;
            this.kryptonLabel4.Values.Text = "Tax Rate:";
            // 
            // PnlArabic
            // 
            this.PnlArabic.Controls.Add(this.DESC_ARB);
            this.PnlArabic.Controls.Add(this.kryptonLabel3);
            this.PnlArabic.Location = new System.Drawing.Point(440, 257);
            this.PnlArabic.Name = "PnlArabic";
            this.PnlArabic.Size = new System.Drawing.Size(282, 29);
            this.PnlArabic.TabIndex = 103;
            // 
            // Tax_Type
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 343);
            this.Controls.Add(this.PnlArabic);
            this.Controls.Add(this.txttaxrate);
            this.Controls.Add(this.kryptonLabel4);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnQuit);
            this.Controls.Add(this.DESC_ENG);
            this.Controls.Add(this.kryptonLabel2);
            this.Controls.Add(this.CODE);
            this.Controls.Add(this.kryptonLabel1);
            this.Controls.Add(this.dgCommon);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Tax_Type";
            this.Text = "Tax Type";
            this.Load += new System.EventHandler(this.Tax_Type_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgCommon)).EndInit();
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
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox CODE;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private System.Windows.Forms.DataGridView dgCommon;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txttaxrate;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private System.Windows.Forms.Panel PnlArabic;


    }
}