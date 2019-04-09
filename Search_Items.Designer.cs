namespace Sys_Sols_Inventory
{
    partial class Search_Items
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Search_Items));
            this.dataGridItem = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BtnClear = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.txt_Itemname = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.Txt_Itemcode = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.txt_barcode = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridItem)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridItem
            // 
            this.dataGridItem.AllowUserToAddRows = false;
            this.dataGridItem.AllowUserToDeleteRows = false;
            this.dataGridItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridItem.Location = new System.Drawing.Point(50, 122);
            this.dataGridItem.Name = "dataGridItem";
            this.dataGridItem.ReadOnly = true;
            this.dataGridItem.Size = new System.Drawing.Size(925, 385);
            this.dataGridItem.TabIndex = 18;
            this.dataGridItem.Visible = false;
            this.dataGridItem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridItem_KeyDown);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.BtnClear);
            this.groupBox1.Controls.Add(this.txt_Itemname);
            this.groupBox1.Controls.Add(this.Txt_Itemcode);
            this.groupBox1.Controls.Add(this.txt_barcode);
            this.groupBox1.Controls.Add(this.kryptonLabel2);
            this.groupBox1.Controls.Add(this.kryptonLabel1);
            this.groupBox1.Controls.Add(this.kryptonLabel3);
            this.groupBox1.Location = new System.Drawing.Point(50, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(925, 104);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search By";
            // 
            // BtnClear
            // 
            this.BtnClear.Location = new System.Drawing.Point(816, 59);
            this.BtnClear.Name = "BtnClear";
            this.BtnClear.Size = new System.Drawing.Size(90, 25);
            this.BtnClear.TabIndex = 48;
            this.BtnClear.Values.Text = "Clear";
            this.BtnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // txt_Itemname
            // 
            this.txt_Itemname.Location = new System.Drawing.Point(582, 64);
            this.txt_Itemname.Name = "txt_Itemname";
            this.txt_Itemname.Size = new System.Drawing.Size(193, 20);
            this.txt_Itemname.TabIndex = 47;
            this.txt_Itemname.TextChanged += new System.EventHandler(this.txt_Itemname_TextChanged);
            this.txt_Itemname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_Itemname_KeyDown);
            // 
            // Txt_Itemcode
            // 
            this.Txt_Itemcode.Location = new System.Drawing.Point(311, 64);
            this.Txt_Itemcode.Name = "Txt_Itemcode";
            this.Txt_Itemcode.Size = new System.Drawing.Size(193, 20);
            this.Txt_Itemcode.TabIndex = 46;
            this.Txt_Itemcode.TextChanged += new System.EventHandler(this.Txt_Itemcode_TextChanged);
            this.Txt_Itemcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Txt_Itemcode_KeyDown);
            // 
            // txt_barcode
            // 
            this.txt_barcode.Location = new System.Drawing.Point(37, 64);
            this.txt_barcode.Name = "txt_barcode";
            this.txt_barcode.Size = new System.Drawing.Size(193, 20);
            this.txt_barcode.TabIndex = 45;
            this.txt_barcode.TextChanged += new System.EventHandler(this.txt_barcode_TextChanged);
            this.txt_barcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_barcode_KeyDown);
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(37, 39);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(58, 20);
            this.kryptonLabel2.TabIndex = 7;
            this.kryptonLabel2.Values.Text = "Barcode:";
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(582, 39);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(74, 20);
            this.kryptonLabel1.TabIndex = 6;
            this.kryptonLabel1.Values.Text = "Item Name:";
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(311, 39);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(70, 20);
            this.kryptonLabel3.TabIndex = 5;
            this.kryptonLabel3.Values.Text = "Item Code:";
            // 
            // Search_Items
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1013, 519);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridItem);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Search_Items";
            this.Text = "Search_Items";
            this.Load += new System.EventHandler(this.Search_Items_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Search_Items_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridItem)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txt_Itemname;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox Txt_Itemcode;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txt_barcode;
        private ComponentFactory.Krypton.Toolkit.KryptonButton BtnClear;
    }
}