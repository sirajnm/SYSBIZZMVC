namespace Sys_Sols_Inventory.Manufacture
{
    partial class FrmProductManufacture
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
            this.label1 = new System.Windows.Forms.Label();
            this.dgvMaterials = new System.Windows.Forms.DataGridView();
            this.cSlNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cMaterialCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cMaterialName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cMaterialBatch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cMaterialUOM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cMaterialQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cMaterialCost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cMaterialAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cMaterialQtyPerProduct = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbUnit = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbProduct = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbBatch = new System.Windows.Forms.ComboBox();
            this.dgvDamage = new System.Windows.Forms.DataGridView();
            this.ccSlNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ccMaterialCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ccMaterialName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ccMaterialBatch = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ccMaterialUOM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ccMaterialQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ccMaterialQtyPerProduct = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ccMaterialCost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ccMaterialAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmbMaterialBatch = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaterials)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDamage)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(158, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Product Manufacture";
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
            this.cMaterialCode,
            this.cMaterialName,
            this.cMaterialBatch,
            this.cMaterialUOM,
            this.cMaterialQty,
            this.cMaterialCost,
            this.cMaterialAmount,
            this.cMaterialQtyPerProduct});
            this.dgvMaterials.Location = new System.Drawing.Point(13, 135);
            this.dgvMaterials.Name = "dgvMaterials";
            this.dgvMaterials.RowHeadersVisible = false;
            this.dgvMaterials.Size = new System.Drawing.Size(760, 164);
            this.dgvMaterials.TabIndex = 14;
            this.dgvMaterials.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMaterials_CellEnter);
            // 
            // cSlNo
            // 
            this.cSlNo.FillWeight = 50F;
            this.cSlNo.HeaderText = "Sl. No.";
            this.cSlNo.Name = "cSlNo";
            this.cSlNo.ReadOnly = true;
            // 
            // cMaterialCode
            // 
            this.cMaterialCode.FillWeight = 50F;
            this.cMaterialCode.HeaderText = "Code";
            this.cMaterialCode.Name = "cMaterialCode";
            this.cMaterialCode.ReadOnly = true;
            // 
            // cMaterialName
            // 
            this.cMaterialName.HeaderText = "Material";
            this.cMaterialName.Name = "cMaterialName";
            this.cMaterialName.ReadOnly = true;
            // 
            // cMaterialBatch
            // 
            this.cMaterialBatch.HeaderText = "Batch";
            this.cMaterialBatch.Name = "cMaterialBatch";
            this.cMaterialBatch.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.cMaterialBatch.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // cMaterialUOM
            // 
            this.cMaterialUOM.FillWeight = 50F;
            this.cMaterialUOM.HeaderText = "UOM";
            this.cMaterialUOM.Name = "cMaterialUOM";
            this.cMaterialUOM.ReadOnly = true;
            // 
            // cMaterialQty
            // 
            this.cMaterialQty.FillWeight = 50F;
            this.cMaterialQty.HeaderText = "Qty";
            this.cMaterialQty.Name = "cMaterialQty";
            this.cMaterialQty.ReadOnly = true;
            // 
            // cMaterialCost
            // 
            this.cMaterialCost.FillWeight = 50F;
            this.cMaterialCost.HeaderText = "Cost";
            this.cMaterialCost.Name = "cMaterialCost";
            this.cMaterialCost.ReadOnly = true;
            // 
            // cMaterialAmount
            // 
            this.cMaterialAmount.FillWeight = 50F;
            this.cMaterialAmount.HeaderText = "Amount";
            this.cMaterialAmount.Name = "cMaterialAmount";
            this.cMaterialAmount.ReadOnly = true;
            // 
            // cMaterialQtyPerProduct
            // 
            this.cMaterialQtyPerProduct.FillWeight = 50F;
            this.cMaterialQtyPerProduct.HeaderText = "Qty/Prod";
            this.cMaterialQtyPerProduct.Name = "cMaterialQtyPerProduct";
            this.cMaterialQtyPerProduct.ReadOnly = true;
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(531, 109);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(100, 20);
            this.txtQty.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(528, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Qty:";
            // 
            // cmbUnit
            // 
            this.cmbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUnit.FormattingEnabled = true;
            this.cmbUnit.Location = new System.Drawing.Point(425, 108);
            this.cmbUnit.Name = "cmbUnit";
            this.cmbUnit.Size = new System.Drawing.Size(100, 21);
            this.cmbUnit.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(422, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Unit:";
            // 
            // cmbProduct
            // 
            this.cmbProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProduct.FormattingEnabled = true;
            this.cmbProduct.Location = new System.Drawing.Point(13, 108);
            this.cmbProduct.Name = "cmbProduct";
            this.cmbProduct.Size = new System.Drawing.Size(300, 21);
            this.cmbProduct.TabIndex = 9;
            this.cmbProduct.SelectedIndexChanged += new System.EventHandler(this.cmbProduct_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Product:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(8, 308);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 20);
            this.label5.TabIndex = 16;
            this.label5.Text = "Damage";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(698, 501);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 35);
            this.button1.TabIndex = 17;
            this.button1.Text = "&Save";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "Ref No.:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(65, 48);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 19;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(171, 48);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(25, 20);
            this.button2.TabIndex = 20;
            this.button2.Text = "...";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(319, 92);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "Batch";
            // 
            // cmbBatch
            // 
            this.cmbBatch.FormattingEnabled = true;
            this.cmbBatch.Location = new System.Drawing.Point(319, 108);
            this.cmbBatch.Name = "cmbBatch";
            this.cmbBatch.Size = new System.Drawing.Size(100, 21);
            this.cmbBatch.TabIndex = 23;
            // 
            // dgvDamage
            // 
            this.dgvDamage.AllowUserToAddRows = false;
            this.dgvDamage.AllowUserToDeleteRows = false;
            this.dgvDamage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDamage.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDamage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDamage.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ccSlNo,
            this.ccMaterialCode,
            this.ccMaterialName,
            this.ccMaterialBatch,
            this.ccMaterialUOM,
            this.ccMaterialQty,
            this.ccMaterialQtyPerProduct,
            this.ccMaterialCost,
            this.ccMaterialAmount});
            this.dgvDamage.Location = new System.Drawing.Point(13, 331);
            this.dgvDamage.Name = "dgvDamage";
            this.dgvDamage.RowHeadersVisible = false;
            this.dgvDamage.Size = new System.Drawing.Size(760, 164);
            this.dgvDamage.TabIndex = 24;
            // 
            // ccSlNo
            // 
            this.ccSlNo.FillWeight = 40F;
            this.ccSlNo.HeaderText = "Sl. No.";
            this.ccSlNo.Name = "ccSlNo";
            this.ccSlNo.ReadOnly = true;
            // 
            // ccMaterialCode
            // 
            this.ccMaterialCode.FillWeight = 50F;
            this.ccMaterialCode.HeaderText = "Code";
            this.ccMaterialCode.Name = "ccMaterialCode";
            this.ccMaterialCode.ReadOnly = true;
            // 
            // ccMaterialName
            // 
            this.ccMaterialName.HeaderText = "Material";
            this.ccMaterialName.Name = "ccMaterialName";
            this.ccMaterialName.ReadOnly = true;
            // 
            // ccMaterialBatch
            // 
            this.ccMaterialBatch.HeaderText = "Batch";
            this.ccMaterialBatch.Name = "ccMaterialBatch";
            // 
            // ccMaterialUOM
            // 
            this.ccMaterialUOM.FillWeight = 50F;
            this.ccMaterialUOM.HeaderText = "UOM";
            this.ccMaterialUOM.Name = "ccMaterialUOM";
            this.ccMaterialUOM.ReadOnly = true;
            // 
            // ccMaterialQty
            // 
            this.ccMaterialQty.FillWeight = 50F;
            this.ccMaterialQty.HeaderText = "Qty";
            this.ccMaterialQty.Name = "ccMaterialQty";
            this.ccMaterialQty.ReadOnly = true;
            // 
            // ccMaterialQtyPerProduct
            // 
            this.ccMaterialQtyPerProduct.HeaderText = "Qty/Prod";
            this.ccMaterialQtyPerProduct.Name = "ccMaterialQtyPerProduct";
            this.ccMaterialQtyPerProduct.Visible = false;
            // 
            // ccMaterialCost
            // 
            this.ccMaterialCost.FillWeight = 50F;
            this.ccMaterialCost.HeaderText = "Cost";
            this.ccMaterialCost.Name = "ccMaterialCost";
            this.ccMaterialCost.ReadOnly = true;
            // 
            // ccMaterialAmount
            // 
            this.ccMaterialAmount.FillWeight = 50F;
            this.ccMaterialAmount.HeaderText = "Amount";
            this.ccMaterialAmount.Name = "ccMaterialAmount";
            this.ccMaterialAmount.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.FillWeight = 50F;
            this.dataGridViewTextBoxColumn1.HeaderText = "Sl. No.";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 69;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.FillWeight = 50F;
            this.dataGridViewTextBoxColumn2.HeaderText = "Code";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 69;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Material";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 137;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.FillWeight = 50F;
            this.dataGridViewTextBoxColumn4.HeaderText = "UOM";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 69;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.FillWeight = 50F;
            this.dataGridViewTextBoxColumn5.HeaderText = "Qty";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 69;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.FillWeight = 50F;
            this.dataGridViewTextBoxColumn6.HeaderText = "Cost";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 68;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.FillWeight = 50F;
            this.dataGridViewTextBoxColumn7.HeaderText = "Amount";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Width = 69;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.FillWeight = 50F;
            this.dataGridViewTextBoxColumn8.HeaderText = "Qty/Prod";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.Width = 69;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.FillWeight = 40F;
            this.dataGridViewTextBoxColumn9.HeaderText = "Sl. No.";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            this.dataGridViewTextBoxColumn9.Width = 62;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.FillWeight = 50F;
            this.dataGridViewTextBoxColumn10.HeaderText = "Code";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            this.dataGridViewTextBoxColumn10.Width = 77;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.HeaderText = "Material";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.ReadOnly = true;
            this.dataGridViewTextBoxColumn11.Width = 155;
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.FillWeight = 50F;
            this.dataGridViewTextBoxColumn12.HeaderText = "UOM";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.ReadOnly = true;
            this.dataGridViewTextBoxColumn12.Width = 77;
            // 
            // dataGridViewTextBoxColumn13
            // 
            this.dataGridViewTextBoxColumn13.FillWeight = 50F;
            this.dataGridViewTextBoxColumn13.HeaderText = "Qty";
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            this.dataGridViewTextBoxColumn13.ReadOnly = true;
            this.dataGridViewTextBoxColumn13.Width = 78;
            // 
            // dataGridViewTextBoxColumn14
            // 
            this.dataGridViewTextBoxColumn14.HeaderText = "Qty/Prod";
            this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
            this.dataGridViewTextBoxColumn14.Visible = false;
            // 
            // dataGridViewTextBoxColumn15
            // 
            this.dataGridViewTextBoxColumn15.FillWeight = 50F;
            this.dataGridViewTextBoxColumn15.HeaderText = "Cost";
            this.dataGridViewTextBoxColumn15.Name = "dataGridViewTextBoxColumn15";
            this.dataGridViewTextBoxColumn15.ReadOnly = true;
            this.dataGridViewTextBoxColumn15.Width = 77;
            // 
            // dataGridViewTextBoxColumn16
            // 
            this.dataGridViewTextBoxColumn16.FillWeight = 50F;
            this.dataGridViewTextBoxColumn16.HeaderText = "Amount";
            this.dataGridViewTextBoxColumn16.Name = "dataGridViewTextBoxColumn16";
            this.dataGridViewTextBoxColumn16.ReadOnly = true;
            this.dataGridViewTextBoxColumn16.Width = 77;
            // 
            // cmbMaterialBatch
            // 
            this.cmbMaterialBatch.FormattingEnabled = true;
            this.cmbMaterialBatch.Location = new System.Drawing.Point(191, 184);
            this.cmbMaterialBatch.Name = "cmbMaterialBatch";
            this.cmbMaterialBatch.Size = new System.Drawing.Size(121, 21);
            this.cmbMaterialBatch.TabIndex = 25;
            this.cmbMaterialBatch.Visible = false;
            this.cmbMaterialBatch.Leave += new System.EventHandler(this.cmbMaterialBatch_Leave);
            // 
            // FrmProductManufacture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 545);
            this.Controls.Add(this.cmbMaterialBatch);
            this.Controls.Add(this.dgvDamage);
            this.Controls.Add(this.cmbBatch);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dgvMaterials);
            this.Controls.Add(this.txtQty);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbUnit);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbProduct);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FrmProductManufacture";
            this.Text = "Product Manufacture";
            this.Load += new System.EventHandler(this.FrmProductManufacture_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaterials)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDamage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvMaterials;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbUnit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbProduct;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbBatch;
        private System.Windows.Forms.DataGridView dgvDamage;
        private System.Windows.Forms.DataGridViewTextBoxColumn ccSlNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ccMaterialCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ccMaterialName;
        private System.Windows.Forms.DataGridViewComboBoxColumn ccMaterialBatch;
        private System.Windows.Forms.DataGridViewTextBoxColumn ccMaterialUOM;
        private System.Windows.Forms.DataGridViewTextBoxColumn ccMaterialQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn ccMaterialQtyPerProduct;
        private System.Windows.Forms.DataGridViewTextBoxColumn ccMaterialCost;
        private System.Windows.Forms.DataGridViewTextBoxColumn ccMaterialAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn16;
        private System.Windows.Forms.ComboBox cmbMaterialBatch;
        private System.Windows.Forms.DataGridViewTextBoxColumn cSlNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn cMaterialCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn cMaterialName;
        private System.Windows.Forms.DataGridViewTextBoxColumn cMaterialBatch;
        private System.Windows.Forms.DataGridViewTextBoxColumn cMaterialUOM;
        private System.Windows.Forms.DataGridViewTextBoxColumn cMaterialQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn cMaterialCost;
        private System.Windows.Forms.DataGridViewTextBoxColumn cMaterialAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn cMaterialQtyPerProduct;
    }
}