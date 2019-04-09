namespace Sys_Sols_Inventory
{
    partial class Country
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Country));
            this.dgCommon = new System.Windows.Forms.DataGridView();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.CODE = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.DESC_ENG = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.DESC_ARB = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.btnQuit = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnDelete = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnClear = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnSave = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.PnlArabic = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgCommon)).BeginInit();
            this.PnlArabic.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgCommon
            // 
            this.dgCommon.AllowUserToAddRows = false;
            this.dgCommon.AllowUserToDeleteRows = false;
            this.dgCommon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgCommon.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgCommon.Location = new System.Drawing.Point(0, 0);
            this.dgCommon.Name = "dgCommon";
            this.dgCommon.ReadOnly = true;
            this.dgCommon.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgCommon.Size = new System.Drawing.Size(698, 281);
            this.dgCommon.TabIndex = 7;
            this.dgCommon.Click += new System.EventHandler(this.dgCommon_Click);
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(13, 288);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(42, 20);
            this.kryptonLabel1.TabIndex = 1;
            this.kryptonLabel1.Values.Text = "Code:";
            // 
            // CODE
            // 
            this.CODE.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.CODE.Location = new System.Drawing.Point(61, 288);
            this.CODE.Name = "CODE";
            this.CODE.Size = new System.Drawing.Size(67, 20);
            this.CODE.TabIndex = 0;
            this.CODE.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CODE_KeyDown);
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(166, 288);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(46, 20);
            this.kryptonLabel2.TabIndex = 3;
            this.kryptonLabel2.Values.Text = "Name:";
            // 
            // DESC_ENG
            // 
            this.DESC_ENG.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.DESC_ENG.Location = new System.Drawing.Point(230, 288);
            this.DESC_ENG.Name = "DESC_ENG";
            this.DESC_ENG.Size = new System.Drawing.Size(177, 20);
            this.DESC_ENG.TabIndex = 1;
            this.DESC_ENG.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CODE_KeyDown);
            // 
            // DESC_ARB
            // 
            this.DESC_ARB.Location = new System.Drawing.Point(82, 10);
            this.DESC_ARB.Name = "DESC_ARB";
            this.DESC_ARB.Size = new System.Drawing.Size(177, 20);
            this.DESC_ARB.TabIndex = 2;
            this.DESC_ARB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CODE_KeyDown);
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(5, 10);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(71, 20);
            this.kryptonLabel3.TabIndex = 5;
            this.kryptonLabel3.Values.Text = "Arb. Name:";
            // 
            // btnQuit
            // 
            this.btnQuit.Location = new System.Drawing.Point(596, 319);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(90, 25);
            this.btnQuit.TabIndex = 6;
            this.btnQuit.Values.Text = "Close";
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(500, 319);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(90, 25);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.Values.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(404, 319);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(90, 25);
            this.btnClear.TabIndex = 4;
            this.btnClear.Values.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSave
            // 
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.Location = new System.Drawing.Point(308, 319);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 25);
            this.btnSave.TabIndex = 3;
            this.btnSave.Values.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // PnlArabic
            // 
            this.PnlArabic.Controls.Add(this.DESC_ARB);
            this.PnlArabic.Controls.Add(this.kryptonLabel3);
            this.PnlArabic.Location = new System.Drawing.Point(418, 278);
            this.PnlArabic.Name = "PnlArabic";
            this.PnlArabic.Size = new System.Drawing.Size(280, 41);
            this.PnlArabic.TabIndex = 8;
            // 
            // Country
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(698, 351);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnQuit);
            this.Controls.Add(this.dgCommon);
            this.Controls.Add(this.PnlArabic);
            this.Controls.Add(this.DESC_ENG);
            this.Controls.Add(this.kryptonLabel2);
            this.Controls.Add(this.CODE);
            this.Controls.Add(this.kryptonLabel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Country";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Country_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgCommon)).EndInit();
            this.PnlArabic.ResumeLayout(false);
            this.PnlArabic.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgCommon;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox CODE;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox DESC_ENG;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox DESC_ARB;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnQuit;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnDelete;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnClear;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSave;
        private System.Windows.Forms.Panel PnlArabic;
    }
}

