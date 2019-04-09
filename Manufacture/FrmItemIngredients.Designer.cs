namespace Sys_Sols_Inventory.Manufacture
{
    partial class FrmItemIngredients
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cmbItem = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvMaterials = new System.Windows.Forms.DataGridView();
            this.cSlNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cItemId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cPackSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cCostPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmbUnit = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlSuggest = new System.Windows.Forms.Panel();
            this.dgvSuggestItems = new System.Windows.Forms.DataGridView();
            this.txtSuggestItem = new System.Windows.Forms.TextBox();
            this.cmbMaterialUnit = new System.Windows.Forms.ComboBox();
            this.txtMaterialQty = new System.Windows.Forms.TextBox();
            this.txtCostPricePerUnit = new System.Windows.Forms.TextBox();
            this.lblCostPer = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaterials)).BeginInit();
            this.pnlSuggest.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSuggestItems)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbItem
            // 
            this.cmbItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbItem.FormattingEnabled = true;
            this.cmbItem.Location = new System.Drawing.Point(81, 50);
            this.cmbItem.Name = "cmbItem";
            this.cmbItem.Size = new System.Drawing.Size(200, 21);
            this.cmbItem.TabIndex = 0;
            this.cmbItem.SelectedIndexChanged += new System.EventHandler(this.cmbItem_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select Item:";
            // 
            // dgvMaterials
            // 
            this.dgvMaterials.AllowUserToAddRows = false;
            this.dgvMaterials.AllowUserToDeleteRows = false;
            this.dgvMaterials.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMaterials.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMaterials.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMaterials.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cSlNo,
            this.cItemId,
            this.cItemName,
            this.cPackSize,
            this.cUnit,
            this.cCostPrice,
            this.cQty,
            this.cTotal});
            this.dgvMaterials.Location = new System.Drawing.Point(12, 77);
            this.dgvMaterials.Name = "dgvMaterials";
            this.dgvMaterials.ReadOnly = true;
            this.dgvMaterials.RowHeadersVisible = false;
            this.dgvMaterials.Size = new System.Drawing.Size(760, 218);
            this.dgvMaterials.TabIndex = 3;
            this.dgvMaterials.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMaterials_CellEnter);
            // 
            // cSlNo
            // 
            this.cSlNo.FillWeight = 70F;
            this.cSlNo.HeaderText = "Sl. No.";
            this.cSlNo.Name = "cSlNo";
            this.cSlNo.ReadOnly = true;
            // 
            // cItemId
            // 
            this.cItemId.HeaderText = "Item Id";
            this.cItemId.Name = "cItemId";
            this.cItemId.ReadOnly = true;
            // 
            // cItemName
            // 
            this.cItemName.HeaderText = "Item Name";
            this.cItemName.Name = "cItemName";
            this.cItemName.ReadOnly = true;
            // 
            // cPackSize
            // 
            this.cPackSize.HeaderText = "Pack Size";
            this.cPackSize.Name = "cPackSize";
            this.cPackSize.ReadOnly = true;
            // 
            // cUnit
            // 
            this.cUnit.HeaderText = "Unit";
            this.cUnit.Name = "cUnit";
            this.cUnit.ReadOnly = true;
            // 
            // cCostPrice
            // 
            this.cCostPrice.HeaderText = "Cost Price";
            this.cCostPrice.Name = "cCostPrice";
            this.cCostPrice.ReadOnly = true;
            // 
            // cQty
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.cQty.DefaultCellStyle = dataGridViewCellStyle7;
            this.cQty.HeaderText = "Qty";
            this.cQty.Name = "cQty";
            this.cQty.ReadOnly = true;
            // 
            // cTotal
            // 
            this.cTotal.HeaderText = "Total";
            this.cTotal.Name = "cTotal";
            this.cTotal.ReadOnly = true;
            // 
            // cmbUnit
            // 
            this.cmbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUnit.FormattingEnabled = true;
            this.cmbUnit.Location = new System.Drawing.Point(322, 50);
            this.cmbUnit.Name = "cmbUnit";
            this.cmbUnit.Size = new System.Drawing.Size(100, 21);
            this.cmbUnit.TabIndex = 5;
            this.cmbUnit.SelectedIndexChanged += new System.EventHandler(this.cmbUnit_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(287, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Unit:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(431, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Qty:";
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(463, 50);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(100, 20);
            this.txtQty.TabIndex = 8;
            this.txtQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtQty.TextChanged += new System.EventHandler(this.txtQty_TextChanged);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(575, 301);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(99, 35);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(684, 301);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(88, 35);
            this.btnClear.TabIndex = 10;
            this.btnClear.Text = "&Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(166, 24);
            this.label4.TabIndex = 11;
            this.label4.Text = "Item Raw Materials";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.FillWeight = 50F;
            this.dataGridViewTextBoxColumn1.HeaderText = "Sl. No.";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 69;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Item Id";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 137;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Item Name";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 138;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Unit Id";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 138;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Unit";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Width = 137;
            // 
            // dataGridViewTextBoxColumn6
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn6.DefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridViewTextBoxColumn6.HeaderText = "Qty";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.Width = 138;
            // 
            // pnlSuggest
            // 
            this.pnlSuggest.Controls.Add(this.dgvSuggestItems);
            this.pnlSuggest.Controls.Add(this.txtSuggestItem);
            this.pnlSuggest.Location = new System.Drawing.Point(207, 204);
            this.pnlSuggest.Name = "pnlSuggest";
            this.pnlSuggest.Size = new System.Drawing.Size(270, 132);
            this.pnlSuggest.TabIndex = 12;
            this.pnlSuggest.Visible = false;
            // 
            // dgvSuggestItems
            // 
            this.dgvSuggestItems.AllowUserToAddRows = false;
            this.dgvSuggestItems.AllowUserToDeleteRows = false;
            this.dgvSuggestItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSuggestItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSuggestItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSuggestItems.Location = new System.Drawing.Point(0, 20);
            this.dgvSuggestItems.Name = "dgvSuggestItems";
            this.dgvSuggestItems.ReadOnly = true;
            this.dgvSuggestItems.RowHeadersVisible = false;
            this.dgvSuggestItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSuggestItems.Size = new System.Drawing.Size(270, 112);
            this.dgvSuggestItems.TabIndex = 1;
            // 
            // txtSuggestItem
            // 
            this.txtSuggestItem.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtSuggestItem.Location = new System.Drawing.Point(0, 0);
            this.txtSuggestItem.Name = "txtSuggestItem";
            this.txtSuggestItem.Size = new System.Drawing.Size(270, 20);
            this.txtSuggestItem.TabIndex = 0;
            this.txtSuggestItem.TextChanged += new System.EventHandler(this.txtSuggestItem_TextChanged);
            this.txtSuggestItem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSuggestItem_KeyDown);
            this.txtSuggestItem.Leave += new System.EventHandler(this.txtSuggestItem_Leave);
            // 
            // cmbMaterialUnit
            // 
            this.cmbMaterialUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMaterialUnit.FormattingEnabled = true;
            this.cmbMaterialUnit.Location = new System.Drawing.Point(506, 112);
            this.cmbMaterialUnit.Name = "cmbMaterialUnit";
            this.cmbMaterialUnit.Size = new System.Drawing.Size(121, 21);
            this.cmbMaterialUnit.TabIndex = 13;
            this.cmbMaterialUnit.Visible = false;
            this.cmbMaterialUnit.SelectedIndexChanged += new System.EventHandler(this.cmbMaterialUnit_SelectedIndexChanged);
            this.cmbMaterialUnit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbMaterialUnit_KeyDown);
            // 
            // txtMaterialQty
            // 
            this.txtMaterialQty.Location = new System.Drawing.Point(575, 159);
            this.txtMaterialQty.Name = "txtMaterialQty";
            this.txtMaterialQty.Size = new System.Drawing.Size(100, 20);
            this.txtMaterialQty.TabIndex = 14;
            this.txtMaterialQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtMaterialQty.Visible = false;
            this.txtMaterialQty.TextChanged += new System.EventHandler(this.txtMaterialQty_TextChanged);
            this.txtMaterialQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMaterialQty_KeyDown);
            // 
            // txtCostPricePerUnit
            // 
            this.txtCostPricePerUnit.Location = new System.Drawing.Point(672, 50);
            this.txtCostPricePerUnit.Name = "txtCostPricePerUnit";
            this.txtCostPricePerUnit.Size = new System.Drawing.Size(100, 20);
            this.txtCostPricePerUnit.TabIndex = 16;
            this.txtCostPricePerUnit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblCostPer
            // 
            this.lblCostPer.AutoSize = true;
            this.lblCostPer.Location = new System.Drawing.Point(589, 53);
            this.lblCostPer.Name = "lblCostPer";
            this.lblCostPer.Size = new System.Drawing.Size(77, 13);
            this.lblCostPer.TabIndex = 15;
            this.lblCostPer.Text = "Cost Price Per:";
            // 
            // FrmItemIngredients
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 348);
            this.Controls.Add(this.txtCostPricePerUnit);
            this.Controls.Add(this.lblCostPer);
            this.Controls.Add(this.txtMaterialQty);
            this.Controls.Add(this.cmbMaterialUnit);
            this.Controls.Add(this.pnlSuggest);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtQty);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbUnit);
            this.Controls.Add(this.dgvMaterials);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbItem);
            this.Name = "FrmItemIngredients";
            this.Text = "Item Raw Materials";
            this.Load += new System.EventHandler(this.FrmItemIngredients_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaterials)).EndInit();
            this.pnlSuggest.ResumeLayout(false);
            this.pnlSuggest.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSuggestItems)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvMaterials;
        private System.Windows.Forms.ComboBox cmbUnit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.Panel pnlSuggest;
        private System.Windows.Forms.TextBox txtSuggestItem;
        private System.Windows.Forms.DataGridView dgvSuggestItems;
        private System.Windows.Forms.ComboBox cmbMaterialUnit;
        private System.Windows.Forms.TextBox txtMaterialQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn cSlNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn cItemId;
        private System.Windows.Forms.DataGridViewTextBoxColumn cItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn cPackSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn cUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn cCostPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn cQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn cTotal;
        private System.Windows.Forms.TextBox txtCostPricePerUnit;
        private System.Windows.Forms.Label lblCostPer;
    }
}