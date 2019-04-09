namespace Sys_Sols_Inventory.Manufacture
{
    partial class Raw_Materials
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSave = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.dgv_row = new System.Windows.Forms.DataGridView();
            this.sl_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnClear = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnDelete = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.RAW_QTY = new System.Windows.Forms.TextBox();
            this.MFG_QTY = new System.Windows.Forms.TextBox();
            this.btn_add = new System.Windows.Forms.Button();
            this.btn_brwsRAW = new System.Windows.Forms.Button();
            this.btn_brwsMFG = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.MFG_ID = new System.Windows.Forms.ComboBox();
            this.RAW_ID = new System.Windows.Forms.ComboBox();
            this.RAW_NAME = new System.Windows.Forms.ComboBox();
            this.MFG_NAME = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_row)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.btnClear);
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(911, 527);
            this.panel1.TabIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.btnSave.Location = new System.Drawing.Point(599, 491);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 25);
            this.btnSave.TabIndex = 23;
            this.btnSave.Values.Text = "&Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.linkLabel1);
            this.groupBox2.Controls.Add(this.dgv_row);
            this.groupBox2.ForeColor = System.Drawing.Color.Red;
            this.groupBox2.Location = new System.Drawing.Point(13, 90);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(885, 389);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(10, 371);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(85, 13);
            this.linkLabel1.TabIndex = 5;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Remove Item";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // dgv_row
            // 
            this.dgv_row.AllowUserToAddRows = false;
            this.dgv_row.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Green;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_row.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_row.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_row.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sl_no,
            this.ItemCode,
            this.ItemName,
            this.Quantity});
            this.dgv_row.Location = new System.Drawing.Point(8, 14);
            this.dgv_row.Name = "dgv_row";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Green;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_row.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_row.RowHeadersVisible = false;
            this.dgv_row.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_row.Size = new System.Drawing.Size(871, 354);
            this.dgv_row.TabIndex = 3;
            // 
            // sl_no
            // 
            this.sl_no.HeaderText = "Sl.No";
            this.sl_no.Name = "sl_no";
            this.sl_no.Width = 90;
            // 
            // ItemCode
            // 
            this.ItemCode.HeaderText = "Item Code";
            this.ItemCode.Name = "ItemCode";
            this.ItemCode.Width = 175;
            // 
            // ItemName
            // 
            this.ItemName.HeaderText = "Item Name";
            this.ItemName.Name = "ItemName";
            this.ItemName.Width = 450;
            // 
            // Quantity
            // 
            this.Quantity.HeaderText = "Quantity";
            this.Quantity.Name = "Quantity";
            this.Quantity.Width = 150;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(695, 489);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(90, 25);
            this.btnClear.TabIndex = 24;
            this.btnClear.Values.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(791, 489);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(90, 25);
            this.btnDelete.TabIndex = 25;
            this.btnDelete.Values.Text = "Delete";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.RAW_QTY);
            this.groupBox1.Controls.Add(this.MFG_QTY);
            this.groupBox1.Controls.Add(this.btn_add);
            this.groupBox1.Controls.Add(this.btn_brwsRAW);
            this.groupBox1.Controls.Add(this.btn_brwsMFG);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.MFG_ID);
            this.groupBox1.Controls.Add(this.RAW_ID);
            this.groupBox1.Controls.Add(this.RAW_NAME);
            this.groupBox1.Controls.Add(this.MFG_NAME);
            this.groupBox1.ForeColor = System.Drawing.Color.Red;
            this.groupBox1.Location = new System.Drawing.Point(13, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(885, 78);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Raw Materials";
            // 
            // RAW_QTY
            // 
            this.RAW_QTY.Location = new System.Drawing.Point(668, 47);
            this.RAW_QTY.Name = "RAW_QTY";
            this.RAW_QTY.Size = new System.Drawing.Size(185, 21);
            this.RAW_QTY.TabIndex = 3;
            this.RAW_QTY.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RAW_QTY_KeyDown);
            // 
            // MFG_QTY
            // 
            this.MFG_QTY.Location = new System.Drawing.Point(668, 20);
            this.MFG_QTY.Name = "MFG_QTY";
            this.MFG_QTY.Size = new System.Drawing.Size(185, 21);
            this.MFG_QTY.TabIndex = 3;
            this.MFG_QTY.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MFG_QTY_KeyDown);
            this.MFG_QTY.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MFG_QTY_KeyPress);
            // 
            // btn_add
            // 
            //this.btn_add.BackgroundImage = global::Sys_Sols_Inventory.Properties.Resources.plus_297823_960_720;
            this.btn_add.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_add.FlatAppearance.BorderSize = 0;
            this.btn_add.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_add.ForeColor = System.Drawing.Color.Transparent;
            this.btn_add.Location = new System.Drawing.Point(857, 44);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(23, 23);
            this.btn_add.TabIndex = 2;
            this.btn_add.Text = " ";
            this.btn_add.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // btn_brwsRAW
            // 
            this.btn_brwsRAW.ForeColor = System.Drawing.Color.Maroon;
            this.btn_brwsRAW.Location = new System.Drawing.Point(319, 46);
            this.btn_brwsRAW.Name = "btn_brwsRAW";
            this.btn_brwsRAW.Size = new System.Drawing.Size(35, 21);
            this.btn_brwsRAW.TabIndex = 1;
            this.btn_brwsRAW.Text = ">>";
            this.btn_brwsRAW.UseVisualStyleBackColor = true;
            this.btn_brwsRAW.Click += new System.EventHandler(this.btn_brwsRAW_Click);
            // 
            // btn_brwsMFG
            // 
            this.btn_brwsMFG.ForeColor = System.Drawing.Color.Maroon;
            this.btn_brwsMFG.Location = new System.Drawing.Point(319, 19);
            this.btn_brwsMFG.Name = "btn_brwsMFG";
            this.btn_brwsMFG.Size = new System.Drawing.Size(35, 21);
            this.btn_brwsMFG.TabIndex = 1;
            this.btn_brwsMFG.Text = ">>";
            this.btn_brwsMFG.UseVisualStyleBackColor = true;
            this.btn_brwsMFG.Click += new System.EventHandler(this.btn_brwsMFG_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Blue;
            this.label4.Location = new System.Drawing.Point(602, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Raw Qty :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(602, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Mfg Qty  :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(10, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(143, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Raw Materails             :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(10, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Manufacturing Product :";
            // 
            // MFG_ID
            // 
            this.MFG_ID.FormattingEnabled = true;
            this.MFG_ID.Location = new System.Drawing.Point(159, 19);
            this.MFG_ID.Name = "MFG_ID";
            this.MFG_ID.Size = new System.Drawing.Size(155, 21);
            this.MFG_ID.TabIndex = 0;
            // 
            // RAW_ID
            // 
            this.RAW_ID.FormattingEnabled = true;
            this.RAW_ID.Location = new System.Drawing.Point(159, 46);
            this.RAW_ID.Name = "RAW_ID";
            this.RAW_ID.Size = new System.Drawing.Size(155, 21);
            this.RAW_ID.TabIndex = 0;
            // 
            // RAW_NAME
            // 
            this.RAW_NAME.FormattingEnabled = true;
            this.RAW_NAME.Location = new System.Drawing.Point(358, 46);
            this.RAW_NAME.Name = "RAW_NAME";
            this.RAW_NAME.Size = new System.Drawing.Size(238, 21);
            this.RAW_NAME.TabIndex = 0;
            // 
            // MFG_NAME
            // 
            this.MFG_NAME.FormattingEnabled = true;
            this.MFG_NAME.Location = new System.Drawing.Point(358, 19);
            this.MFG_NAME.Name = "MFG_NAME";
            this.MFG_NAME.Size = new System.Drawing.Size(238, 21);
            this.MFG_NAME.TabIndex = 0;
            // 
            // Raw_Materials
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(911, 527);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Raw_Materials";
            this.Text = "Raw_Materials";
            this.Load += new System.EventHandler(this.Raw_Materials_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_row)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.Button btn_brwsRAW;
        private System.Windows.Forms.Button btn_brwsMFG;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox MFG_ID;
        private System.Windows.Forms.ComboBox RAW_ID;
        private System.Windows.Forms.ComboBox RAW_NAME;
        private System.Windows.Forms.ComboBox MFG_NAME;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgv_row;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.DataGridViewTextBoxColumn sl_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private System.Windows.Forms.TextBox RAW_QTY;
        private System.Windows.Forms.TextBox MFG_QTY;
        private System.Windows.Forms.Label label4;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSave;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnClear;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnDelete;
    }
}