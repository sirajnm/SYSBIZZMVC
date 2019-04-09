namespace Sys_Sols_Inventory
{
    partial class Find_Item
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BtnClear = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.txt_Itemname = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.Txt_Itemcode = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.dataGridItem = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridItem)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.BtnClear);
            this.groupBox1.Controls.Add(this.txt_Itemname);
            this.groupBox1.Controls.Add(this.Txt_Itemcode);
            this.groupBox1.Controls.Add(this.kryptonLabel1);
            this.groupBox1.Controls.Add(this.kryptonLabel3);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(365, 76);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search By";
            // 
            // BtnClear
            // 
            this.BtnClear.Location = new System.Drawing.Point(287, 40);
            this.BtnClear.Name = "BtnClear";
            this.BtnClear.Size = new System.Drawing.Size(61, 25);
            this.BtnClear.TabIndex = 48;
            this.BtnClear.Values.Text = "Clear";
            // 
            // txt_Itemname
            // 
            this.txt_Itemname.Location = new System.Drawing.Point(116, 45);
            this.txt_Itemname.Name = "txt_Itemname";
            this.txt_Itemname.Size = new System.Drawing.Size(165, 20);
            this.txt_Itemname.TabIndex = 47;
            this.txt_Itemname.TextChanged += new System.EventHandler(this.txt_Itemname_TextChanged);
            this.txt_Itemname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_Itemname_KeyDown);
            // 
            // Txt_Itemcode
            // 
            this.Txt_Itemcode.Location = new System.Drawing.Point(13, 45);
            this.Txt_Itemcode.Name = "Txt_Itemcode";
            this.Txt_Itemcode.Size = new System.Drawing.Size(97, 20);
            this.Txt_Itemcode.TabIndex = 46;
            this.Txt_Itemcode.TextChanged += new System.EventHandler(this.Txt_Itemcode_TextChanged);
            this.Txt_Itemcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Txt_Itemcode_KeyDown);
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(116, 20);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(74, 20);
            this.kryptonLabel1.TabIndex = 6;
            this.kryptonLabel1.Values.Text = "Item Name:";
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(13, 20);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(70, 20);
            this.kryptonLabel3.TabIndex = 5;
            this.kryptonLabel3.Values.Text = "Item Code:";
            // 
            // dataGridItem
            // 
            this.dataGridItem.AllowUserToAddRows = false;
            this.dataGridItem.AllowUserToDeleteRows = false;
            this.dataGridItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridItem.Location = new System.Drawing.Point(12, 94);
            this.dataGridItem.Name = "dataGridItem";
            this.dataGridItem.ReadOnly = true;
            this.dataGridItem.RowHeadersVisible = false;
            this.dataGridItem.Size = new System.Drawing.Size(365, 385);
            this.dataGridItem.TabIndex = 20;
            this.dataGridItem.Visible = false;
            this.dataGridItem.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridItem_CellDoubleClick);
            // 
            // Find_Item
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(387, 485);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridItem);
            this.Name = "Find_Item";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Find Item";
            this.Load += new System.EventHandler(this.Find_Item_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Find_Item_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridItem)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton BtnClear;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txt_Itemname;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox Txt_Itemcode;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private System.Windows.Forms.DataGridView dataGridItem;
    }
}