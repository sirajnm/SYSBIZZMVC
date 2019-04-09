namespace Sys_Sols_Inventory
{
    partial class InovoicePrinterSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InovoicePrinterSettings));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txt_ItemLength = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.TXTDEFAULTHEIGHT = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.CHKFIXEDHEIGHT = new System.Windows.Forms.CheckBox();
            this.CHKREPEAT = new System.Windows.Forms.CheckBox();
            this.TXTHEIGHT = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.TXTWIDTH = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgItems = new System.Windows.Forms.DataGridView();
            this.Feild = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.XAXIS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.YAXIS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PREFIX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Active = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.btnSave = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonButton1 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chkPageTotal = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgItems)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txt_ItemLength);
            this.groupBox1.Controls.Add(this.kryptonLabel4);
            this.groupBox1.Controls.Add(this.TXTDEFAULTHEIGHT);
            this.groupBox1.Controls.Add(this.kryptonLabel3);
            this.groupBox1.Controls.Add(this.chkPageTotal);
            this.groupBox1.Controls.Add(this.CHKFIXEDHEIGHT);
            this.groupBox1.Controls.Add(this.CHKREPEAT);
            this.groupBox1.Controls.Add(this.TXTHEIGHT);
            this.groupBox1.Controls.Add(this.kryptonLabel1);
            this.groupBox1.Controls.Add(this.TXTWIDTH);
            this.groupBox1.Controls.Add(this.kryptonLabel2);
            this.groupBox1.Location = new System.Drawing.Point(15, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(712, 87);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Page Size";
            // 
            // txt_ItemLength
            // 
            this.txt_ItemLength.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_ItemLength.Location = new System.Drawing.Point(248, 48);
            this.txt_ItemLength.Name = "txt_ItemLength";
            this.txt_ItemLength.Size = new System.Drawing.Size(60, 20);
            this.txt_ItemLength.TabIndex = 105;
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(160, 48);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(82, 20);
            this.kryptonLabel4.TabIndex = 106;
            this.kryptonLabel4.Values.Text = "Item Length :";
            // 
            // TXTDEFAULTHEIGHT
            // 
            this.TXTDEFAULTHEIGHT.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TXTDEFAULTHEIGHT.Location = new System.Drawing.Point(248, 22);
            this.TXTDEFAULTHEIGHT.Name = "TXTDEFAULTHEIGHT";
            this.TXTDEFAULTHEIGHT.Size = new System.Drawing.Size(60, 20);
            this.TXTDEFAULTHEIGHT.TabIndex = 105;
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(146, 22);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(96, 20);
            this.kryptonLabel3.TabIndex = 106;
            this.kryptonLabel3.Values.Text = "Default Height :";
            // 
            // CHKFIXEDHEIGHT
            // 
            this.CHKFIXEDHEIGHT.AutoSize = true;
            this.CHKFIXEDHEIGHT.Location = new System.Drawing.Point(314, 25);
            this.CHKFIXEDHEIGHT.Name = "CHKFIXEDHEIGHT";
            this.CHKFIXEDHEIGHT.Size = new System.Drawing.Size(91, 17);
            this.CHKFIXEDHEIGHT.TabIndex = 104;
            this.CHKFIXEDHEIGHT.Text = "Fixed Height :";
            this.CHKFIXEDHEIGHT.UseVisualStyleBackColor = true;
            this.CHKFIXEDHEIGHT.CheckedChanged += new System.EventHandler(this.CHKFIXEDHEIGHT_CheckedChanged);
            // 
            // CHKREPEAT
            // 
            this.CHKREPEAT.AutoSize = true;
            this.CHKREPEAT.Location = new System.Drawing.Point(314, 51);
            this.CHKREPEAT.Name = "CHKREPEAT";
            this.CHKREPEAT.Size = new System.Drawing.Size(133, 17);
            this.CHKREPEAT.TabIndex = 104;
            this.CHKREPEAT.Text = "Increase Size By Items";
            this.CHKREPEAT.UseVisualStyleBackColor = true;
            // 
            // TXTHEIGHT
            // 
            this.TXTHEIGHT.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TXTHEIGHT.Location = new System.Drawing.Point(76, 48);
            this.TXTHEIGHT.Name = "TXTHEIGHT";
            this.TXTHEIGHT.Size = new System.Drawing.Size(63, 20);
            this.TXTHEIGHT.TabIndex = 102;
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(20, 48);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(53, 20);
            this.kryptonLabel1.TabIndex = 103;
            this.kryptonLabel1.Values.Text = "Height :";
            // 
            // TXTWIDTH
            // 
            this.TXTWIDTH.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TXTWIDTH.Location = new System.Drawing.Point(76, 22);
            this.TXTWIDTH.Name = "TXTWIDTH";
            this.TXTWIDTH.Size = new System.Drawing.Size(63, 20);
            this.TXTWIDTH.TabIndex = 102;
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(23, 22);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(50, 20);
            this.kryptonLabel2.TabIndex = 103;
            this.kryptonLabel2.Values.Text = "Width :";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgItems);
            this.groupBox2.Location = new System.Drawing.Point(13, 105);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(717, 353);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Feilds";
            // 
            // dgItems
            // 
            this.dgItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Feild,
            this.XAXIS,
            this.YAXIS,
            this.PREFIX,
            this.Active});
            this.dgItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgItems.Location = new System.Drawing.Point(3, 16);
            this.dgItems.Name = "dgItems";
            this.dgItems.Size = new System.Drawing.Size(711, 334);
            this.dgItems.TabIndex = 0;
            // 
            // Feild
            // 
            this.Feild.HeaderText = "Feild";
            this.Feild.Name = "Feild";
            // 
            // XAXIS
            // 
            this.XAXIS.HeaderText = "X-Positon";
            this.XAXIS.Name = "XAXIS";
            // 
            // YAXIS
            // 
            this.YAXIS.HeaderText = "Y-Postion";
            this.YAXIS.Name = "YAXIS";
            // 
            // PREFIX
            // 
            this.PREFIX.HeaderText = "PREFIX";
            this.PREFIX.Name = "PREFIX";
            // 
            // Active
            // 
            this.Active.HeaderText = "Active";
            this.Active.Name = "Active";
            // 
            // btnSave
            // 
            this.btnSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSave.Location = new System.Drawing.Point(640, 461);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 25);
            this.btnSave.TabIndex = 91;
            this.btnSave.Values.Text = "Close";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.kryptonButton1.Location = new System.Drawing.Point(544, 461);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.Size = new System.Drawing.Size(90, 25);
            this.kryptonButton1.TabIndex = 91;
            this.kryptonButton1.Values.Text = "Update";
            this.kryptonButton1.Click += new System.EventHandler(this.kryptonButton1_Click);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Feild";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 134;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "X-Positon";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 133;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Y-Postion";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 134;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "PREFIX";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 133;
            // 
            // chkPageTotal
            // 
            this.chkPageTotal.AutoSize = true;
            this.chkPageTotal.Location = new System.Drawing.Point(448, 25);
            this.chkPageTotal.Name = "chkPageTotal";
            this.chkPageTotal.Size = new System.Drawing.Size(84, 17);
            this.chkPageTotal.TabIndex = 104;
            this.chkPageTotal.Text = "Page Total :";
            this.chkPageTotal.UseVisualStyleBackColor = true;
            this.chkPageTotal.CheckedChanged += new System.EventHandler(this.CHKFIXEDHEIGHT_CheckedChanged);
            // 
            // InovoicePrinterSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(742, 494);
            this.Controls.Add(this.kryptonButton1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "InovoicePrinterSettings";
            this.Text = "Inovoice Settings";
            this.Load += new System.EventHandler(this.InovoicePrinterSettings_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgItems)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox TXTHEIGHT;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox TXTWIDTH;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgItems;
        private System.Windows.Forms.CheckBox CHKREPEAT;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox TXTDEFAULTHEIGHT;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private System.Windows.Forms.CheckBox CHKFIXEDHEIGHT;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSave;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Feild;
        private System.Windows.Forms.DataGridViewTextBoxColumn XAXIS;
        private System.Windows.Forms.DataGridViewTextBoxColumn YAXIS;
        private System.Windows.Forms.DataGridViewTextBoxColumn PREFIX;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Active;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txt_ItemLength;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private System.Windows.Forms.CheckBox chkPageTotal;
    }
}