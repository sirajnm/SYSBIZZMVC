namespace Sys_Sols_Inventory.Manufacture
{
    partial class FrmProduction
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
            this.txtDocNo = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvProducts = new System.Windows.Forms.DataGridView();
            this.txtProductCost = new System.Windows.Forms.TextBox();
            this.dgvRawMaterials = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.dgvOtherCharges = new System.Windows.Forms.DataGridView();
            this.txtOtherDesc = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbProductItem = new System.Windows.Forms.ComboBox();
            this.cmbProductUnit = new System.Windows.Forms.ComboBox();
            this.txtProductQty = new System.Windows.Forms.TextBox();
            this.txtProductMRP = new System.Windows.Forms.TextBox();
            this.btnProductAdd = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtOtherAmount = new System.Windows.Forms.TextBox();
            this.btnOtherAdd = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.btnRawAdd = new System.Windows.Forms.Button();
            this.txtRawQty = new System.Windows.Forms.TextBox();
            this.cmbRawUnit = new System.Windows.Forms.ComboBox();
            this.cmbRawBatch = new System.Windows.Forms.ComboBox();
            this.cmbRawItem = new System.Windows.Forms.ComboBox();
            this.txtRawCost = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.cmbRawDamageUnit = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txtRawDamageQty = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.label20 = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.label21 = new System.Windows.Forms.Label();
            this.lblProductionCost = new System.Windows.Forms.Label();
            this.lblDamageCost = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.lblOtherExpenses = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.txtProductBatch = new System.Windows.Forms.TextBox();
            this.ExDate = new System.Windows.Forms.DateTimePicker();
            this.lblExpiryDate = new System.Windows.Forms.Label();
            this.dataGridItem = new System.Windows.Forms.DataGridView();
            this.label22 = new System.Windows.Forms.Label();
            this.txtItenCode = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.txtRawItemCode = new System.Windows.Forms.TextBox();
            this.dgvRowMaterial = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.removeRow = new System.Windows.Forms.LinkLabel();
            this.btnMovement = new System.Windows.Forms.Button();
            this.label27 = new System.Windows.Forms.Label();
            this.combManBatch = new System.Windows.Forms.ComboBox();
            this.label25 = new System.Windows.Forms.Label();
            this.txt_stock = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRawMaterials)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOtherCharges)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRowMaterial)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Doc #:";
            // 
            // txtDocNo
            // 
            this.txtDocNo.Location = new System.Drawing.Point(68, 16);
            this.txtDocNo.Name = "txtDocNo";
            this.txtDocNo.Size = new System.Drawing.Size(116, 21);
            this.txtDocNo.TabIndex = 0;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(184, 15);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(33, 22);
            this.btnBrowse.TabIndex = 1;
            this.btnBrowse.Text = "...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Products";
            // 
            // dgvProducts
            // 
            this.dgvProducts.AllowUserToAddRows = false;
            this.dgvProducts.AllowUserToDeleteRows = false;
            this.dgvProducts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProducts.Location = new System.Drawing.Point(13, 131);
            this.dgvProducts.Name = "dgvProducts";
            this.dgvProducts.ReadOnly = true;
            this.dgvProducts.RowHeadersVisible = false;
            this.dgvProducts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProducts.Size = new System.Drawing.Size(832, 150);
            this.dgvProducts.TabIndex = 4;
            this.dgvProducts.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProducts_CellDoubleClick);
            this.dgvProducts.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvProducts_KeyDown);
            // 
            // txtProductCost
            // 
            this.txtProductCost.Location = new System.Drawing.Point(686, 104);
            this.txtProductCost.Name = "txtProductCost";
            this.txtProductCost.Size = new System.Drawing.Size(77, 21);
            this.txtProductCost.TabIndex = 6;
            this.txtProductCost.KeyDown += new System.Windows.Forms.KeyEventHandler(this.common_KeyDown);
            // 
            // dgvRawMaterials
            // 
            this.dgvRawMaterials.AllowUserToAddRows = false;
            this.dgvRawMaterials.AllowUserToDeleteRows = false;
            this.dgvRawMaterials.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvRawMaterials.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRawMaterials.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRawMaterials.Location = new System.Drawing.Point(13, 357);
            this.dgvRawMaterials.Name = "dgvRawMaterials";
            this.dgvRawMaterials.ReadOnly = true;
            this.dgvRawMaterials.RowHeadersVisible = false;
            this.dgvRawMaterials.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRawMaterials.Size = new System.Drawing.Size(1211, 150);
            this.dgvRawMaterials.TabIndex = 7;
            this.dgvRawMaterials.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRawMaterials_CellContentClick);
            this.dgvRawMaterials.DoubleClick += new System.EventHandler(this.dgvRawMaterials_DoubleClick);
            this.dgvRawMaterials.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvRawMaterials_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(16, 287);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Raw Materials";
            // 
            // dgvOtherCharges
            // 
            this.dgvOtherCharges.AllowUserToAddRows = false;
            this.dgvOtherCharges.AllowUserToDeleteRows = false;
            this.dgvOtherCharges.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvOtherCharges.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvOtherCharges.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOtherCharges.Location = new System.Drawing.Point(862, 131);
            this.dgvOtherCharges.Name = "dgvOtherCharges";
            this.dgvOtherCharges.ReadOnly = true;
            this.dgvOtherCharges.RowHeadersVisible = false;
            this.dgvOtherCharges.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOtherCharges.Size = new System.Drawing.Size(363, 149);
            this.dgvOtherCharges.TabIndex = 9;
            this.dgvOtherCharges.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvOtherCharges_KeyDown);
            // 
            // txtOtherDesc
            // 
            this.txtOtherDesc.Location = new System.Drawing.Point(862, 105);
            this.txtOtherDesc.Name = "txtOtherDesc";
            this.txtOtherDesc.Size = new System.Drawing.Size(196, 21);
            this.txtOtherDesc.TabIndex = 10;
            this.txtOtherDesc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtOtherDesc_KeyDown);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(858, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(126, 20);
            this.label4.TabIndex = 10;
            this.label4.Text = "Other Charges";
            // 
            // cmbProductItem
            // 
            this.cmbProductItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cmbProductItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbProductItem.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.cmbProductItem.FormattingEnabled = true;
            this.cmbProductItem.Location = new System.Drawing.Point(97, 104);
            this.cmbProductItem.Name = "cmbProductItem";
            this.cmbProductItem.Size = new System.Drawing.Size(164, 21);
            this.cmbProductItem.TabIndex = 3;
            this.cmbProductItem.SelectedIndexChanged += new System.EventHandler(this.cmbProductItem_SelectedIndexChanged);
            this.cmbProductItem.TextChanged += new System.EventHandler(this.cmbProductItem_TextChanged);
            this.cmbProductItem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.common_KeyDown);
            // 
            // cmbProductUnit
            // 
            this.cmbProductUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProductUnit.FormattingEnabled = true;
            this.cmbProductUnit.Location = new System.Drawing.Point(532, 105);
            this.cmbProductUnit.Name = "cmbProductUnit";
            this.cmbProductUnit.Size = new System.Drawing.Size(65, 21);
            this.cmbProductUnit.TabIndex = 5;
            this.cmbProductUnit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.common_KeyDown);
            // 
            // txtProductQty
            // 
            this.txtProductQty.Location = new System.Drawing.Point(602, 104);
            this.txtProductQty.Name = "txtProductQty";
            this.txtProductQty.Size = new System.Drawing.Size(80, 21);
            this.txtProductQty.TabIndex = 7;
            this.txtProductQty.TextChanged += new System.EventHandler(this.txtProductQty_TextChanged);
            this.txtProductQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.common_KeyDown);
            // 
            // txtProductMRP
            // 
            this.txtProductMRP.Location = new System.Drawing.Point(768, 104);
            this.txtProductMRP.Name = "txtProductMRP";
            this.txtProductMRP.Size = new System.Drawing.Size(77, 21);
            this.txtProductMRP.TabIndex = 8;
            this.txtProductMRP.KeyDown += new System.Windows.Forms.KeyEventHandler(this.common_KeyDown);
            // 
            // btnProductAdd
            // 
            this.btnProductAdd.Location = new System.Drawing.Point(607, 150);
            this.btnProductAdd.Name = "btnProductAdd";
            this.btnProductAdd.Size = new System.Drawing.Size(82, 22);
            this.btnProductAdd.TabIndex = 9;
            this.btnProductAdd.Text = "Add";
            this.btnProductAdd.UseVisualStyleBackColor = true;
            this.btnProductAdd.Click += new System.EventHandler(this.btnProductAdd_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(93, 89);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Item";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(263, 89);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "Batch";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(528, 89);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "Unit";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(604, 87);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(27, 13);
            this.label8.TabIndex = 21;
            this.label8.Text = "Qty";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(682, 87);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(33, 13);
            this.label9.TabIndex = 22;
            this.label9.Text = "Cost";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(764, 87);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(31, 13);
            this.label10.TabIndex = 23;
            this.label10.Text = "MRP";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(859, 89);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(39, 13);
            this.label11.TabIndex = 24;
            this.label11.Text = "Desc.";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(1058, 89);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(51, 13);
            this.label12.TabIndex = 26;
            this.label12.Text = "Amount";
            // 
            // txtOtherAmount
            // 
            this.txtOtherAmount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOtherAmount.Location = new System.Drawing.Point(1061, 105);
            this.txtOtherAmount.Name = "txtOtherAmount";
            this.txtOtherAmount.Size = new System.Drawing.Size(131, 21);
            this.txtOtherAmount.TabIndex = 11;
            this.txtOtherAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtOtherAmount_KeyDown);
            // 
            // btnOtherAdd
            // 
            this.btnOtherAdd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOtherAdd.Location = new System.Drawing.Point(1167, 137);
            this.btnOtherAdd.Name = "btnOtherAdd";
            this.btnOtherAdd.Size = new System.Drawing.Size(58, 22);
            this.btnOtherAdd.TabIndex = 12;
            this.btnOtherAdd.Text = "Add";
            this.btnOtherAdd.UseVisualStyleBackColor = true;
            this.btnOtherAdd.Click += new System.EventHandler(this.btnOtherAdd_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(696, 311);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(33, 13);
            this.label14.TabIndex = 39;
            this.label14.Text = "Cost";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(763, 310);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(27, 13);
            this.label15.TabIndex = 38;
            this.label15.Text = "Qty";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(591, 310);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(29, 13);
            this.label16.TabIndex = 37;
            this.label16.Text = "Unit";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(256, 310);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(28, 13);
            this.label17.TabIndex = 36;
            this.label17.Text = "PID";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(76, 310);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(34, 13);
            this.label18.TabIndex = 35;
            this.label18.Text = "Item";
            // 
            // btnRawAdd
            // 
            this.btnRawAdd.Location = new System.Drawing.Point(840, 367);
            this.btnRawAdd.Name = "btnRawAdd";
            this.btnRawAdd.Size = new System.Drawing.Size(58, 22);
            this.btnRawAdd.TabIndex = 20;
            this.btnRawAdd.Text = "Add";
            this.btnRawAdd.UseVisualStyleBackColor = true;
            this.btnRawAdd.Click += new System.EventHandler(this.btnRawAdd_Click);
            // 
            // txtRawQty
            // 
            this.txtRawQty.Location = new System.Drawing.Point(760, 327);
            this.txtRawQty.Name = "txtRawQty";
            this.txtRawQty.Size = new System.Drawing.Size(79, 21);
            this.txtRawQty.TabIndex = 17;
            this.txtRawQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRawQty_KeyDown);
            // 
            // cmbRawUnit
            // 
            this.cmbRawUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRawUnit.FormattingEnabled = true;
            this.cmbRawUnit.Location = new System.Drawing.Point(594, 326);
            this.cmbRawUnit.Name = "cmbRawUnit";
            this.cmbRawUnit.Size = new System.Drawing.Size(93, 21);
            this.cmbRawUnit.TabIndex = 15;
            this.cmbRawUnit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbRawUnit_KeyDown);
            // 
            // cmbRawBatch
            // 
            this.cmbRawBatch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRawBatch.FormattingEnabled = true;
            this.cmbRawBatch.Location = new System.Drawing.Point(259, 326);
            this.cmbRawBatch.Name = "cmbRawBatch";
            this.cmbRawBatch.Size = new System.Drawing.Size(116, 21);
            this.cmbRawBatch.TabIndex = 14;
            this.cmbRawBatch.SelectedIndexChanged += new System.EventHandler(this.cmbRawBatch_SelectedIndexChanged);
            this.cmbRawBatch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbRawBatch_KeyDown);
            // 
            // cmbRawItem
            // 
            this.cmbRawItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cmbRawItem.FormattingEnabled = true;
            this.cmbRawItem.Location = new System.Drawing.Point(79, 326);
            this.cmbRawItem.Name = "cmbRawItem";
            this.cmbRawItem.Size = new System.Drawing.Size(174, 21);
            this.cmbRawItem.TabIndex = 13;
            this.cmbRawItem.SelectedIndexChanged += new System.EventHandler(this.cmbRawItem_SelectedIndexChanged);
            this.cmbRawItem.TextChanged += new System.EventHandler(this.cmbRawItem_TextChanged);
            this.cmbRawItem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.common_KeyDown);
            // 
            // txtRawCost
            // 
            this.txtRawCost.Location = new System.Drawing.Point(693, 328);
            this.txtRawCost.Name = "txtRawCost";
            this.txtRawCost.Size = new System.Drawing.Size(58, 21);
            this.txtRawCost.TabIndex = 16;
            this.txtRawCost.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRawCost_KeyDown);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(857, 310);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(81, 13);
            this.label13.TabIndex = 41;
            this.label13.Text = "Damage Unit";
            // 
            // cmbRawDamageUnit
            // 
            this.cmbRawDamageUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRawDamageUnit.FormattingEnabled = true;
            this.cmbRawDamageUnit.Location = new System.Drawing.Point(860, 326);
            this.cmbRawDamageUnit.Name = "cmbRawDamageUnit";
            this.cmbRawDamageUnit.Size = new System.Drawing.Size(93, 21);
            this.cmbRawDamageUnit.TabIndex = 18;
            this.cmbRawDamageUnit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbRawDamageUnit_KeyDown);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(965, 309);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(79, 13);
            this.label19.TabIndex = 43;
            this.label19.Text = "Damage Qty";
            // 
            // txtRawDamageQty
            // 
            this.txtRawDamageQty.Location = new System.Drawing.Point(965, 326);
            this.txtRawDamageQty.Name = "txtRawDamageQty";
            this.txtRawDamageQty.Size = new System.Drawing.Size(76, 21);
            this.txtRawDamageQty.TabIndex = 19;
            this.txtRawDamageQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRawDamageQty_KeyDown);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(768, 512);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(87, 23);
            this.btnSave.TabIndex = 21;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(862, 512);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(87, 23);
            this.btnClear.TabIndex = 22;
            this.btnClear.Text = "&Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(957, 512);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(87, 23);
            this.btnDelete.TabIndex = 23;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(249, 20);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(39, 13);
            this.label20.TabIndex = 47;
            this.label20.Text = "Date:";
            // 
            // dtpDate
            // 
            this.dtpDate.CustomFormat = "dd/MM/yyyy";
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(294, 16);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(117, 21);
            this.dtpDate.TabIndex = 2;
            this.dtpDate.ValueChanged += new System.EventHandler(this.dtpDate_ValueChanged);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(8, 515);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(128, 17);
            this.label21.TabIndex = 48;
            this.label21.Text = "Production Cost:";
            // 
            // lblProductionCost
            // 
            this.lblProductionCost.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProductionCost.Location = new System.Drawing.Point(160, 515);
            this.lblProductionCost.Name = "lblProductionCost";
            this.lblProductionCost.Size = new System.Drawing.Size(101, 17);
            this.lblProductionCost.TabIndex = 49;
            this.lblProductionCost.Text = "0";
            // 
            // lblDamageCost
            // 
            this.lblDamageCost.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDamageCost.Location = new System.Drawing.Point(397, 515);
            this.lblDamageCost.Name = "lblDamageCost";
            this.lblDamageCost.Size = new System.Drawing.Size(80, 17);
            this.lblDamageCost.TabIndex = 51;
            this.lblDamageCost.Text = "0";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(263, 515);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(109, 17);
            this.label24.TabIndex = 50;
            this.label24.Text = "Damage Cost:";
            // 
            // lblOtherExpenses
            // 
            this.lblOtherExpenses.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOtherExpenses.Location = new System.Drawing.Point(637, 515);
            this.lblOtherExpenses.Name = "lblOtherExpenses";
            this.lblOtherExpenses.Size = new System.Drawing.Size(80, 17);
            this.lblOtherExpenses.TabIndex = 53;
            this.lblOtherExpenses.Text = "0";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(481, 515);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(128, 17);
            this.label26.TabIndex = 52;
            this.label26.Text = "Other Expenses:";
            // 
            // txtProductBatch
            // 
            this.txtProductBatch.Location = new System.Drawing.Point(267, 105);
            this.txtProductBatch.Name = "txtProductBatch";
            this.txtProductBatch.Size = new System.Drawing.Size(132, 21);
            this.txtProductBatch.TabIndex = 54;
            this.txtProductBatch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.common_KeyDown);
            // 
            // ExDate
            // 
            this.ExDate.CustomFormat = "dd/MM/yyyy";
            this.ExDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ExDate.Location = new System.Drawing.Point(407, 105);
            this.ExDate.Name = "ExDate";
            this.ExDate.Size = new System.Drawing.Size(117, 21);
            this.ExDate.TabIndex = 61;
            this.ExDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.common_KeyDown);
            // 
            // lblExpiryDate
            // 
            this.lblExpiryDate.AutoSize = true;
            this.lblExpiryDate.Location = new System.Drawing.Point(403, 89);
            this.lblExpiryDate.Name = "lblExpiryDate";
            this.lblExpiryDate.Size = new System.Drawing.Size(74, 13);
            this.lblExpiryDate.TabIndex = 62;
            this.lblExpiryDate.Text = "Expiry Date";
            // 
            // dataGridItem
            // 
            this.dataGridItem.AllowUserToAddRows = false;
            this.dataGridItem.AllowUserToDeleteRows = false;
            this.dataGridItem.BackgroundColor = System.Drawing.Color.PeachPuff;
            this.dataGridItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridItem.GridColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dataGridItem.Location = new System.Drawing.Point(95, 137);
            this.dataGridItem.Name = "dataGridItem";
            this.dataGridItem.ReadOnly = true;
            this.dataGridItem.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridItem.Size = new System.Drawing.Size(561, 132);
            this.dataGridItem.TabIndex = 99;
            this.dataGridItem.DoubleClick += new System.EventHandler(this.dataGridItem_DoubleClick);
            this.dataGridItem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridItem_KeyDown);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(18, 87);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(37, 13);
            this.label22.TabIndex = 101;
            this.label22.Text = "Code";
            // 
            // txtItenCode
            // 
            this.txtItenCode.Location = new System.Drawing.Point(18, 104);
            this.txtItenCode.Name = "txtItenCode";
            this.txtItenCode.Size = new System.Drawing.Size(74, 21);
            this.txtItenCode.TabIndex = 100;
            this.txtItenCode.TextChanged += new System.EventHandler(this.txtItenCode_TextChanged);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(7, 311);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(41, 13);
            this.label23.TabIndex = 103;
            this.label23.Text = " Code";
            // 
            // txtRawItemCode
            // 
            this.txtRawItemCode.Location = new System.Drawing.Point(13, 326);
            this.txtRawItemCode.Name = "txtRawItemCode";
            this.txtRawItemCode.Size = new System.Drawing.Size(58, 21);
            this.txtRawItemCode.TabIndex = 102;
            this.txtRawItemCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRawItemCode_KeyDown);
            // 
            // dgvRowMaterial
            // 
            this.dgvRowMaterial.AllowUserToAddRows = false;
            this.dgvRowMaterial.AllowUserToDeleteRows = false;
            this.dgvRowMaterial.BackgroundColor = System.Drawing.Color.PeachPuff;
            this.dgvRowMaterial.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRowMaterial.GridColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dgvRowMaterial.Location = new System.Drawing.Point(97, 362);
            this.dgvRowMaterial.Name = "dgvRowMaterial";
            this.dgvRowMaterial.ReadOnly = true;
            this.dgvRowMaterial.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRowMaterial.Size = new System.Drawing.Size(559, 132);
            this.dgvRowMaterial.TabIndex = 104;
            this.dgvRowMaterial.DoubleClick += new System.EventHandler(this.dgvRowMaterial_DoubleClick);
            // 
            // panel1
            // 
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Controls.Add(this.removeRow);
            this.panel1.Controls.Add(this.btnMovement);
            this.panel1.Controls.Add(this.label27);
            this.panel1.Controls.Add(this.combManBatch);
            this.panel1.Controls.Add(this.label25);
            this.panel1.Controls.Add(this.txt_stock);
            this.panel1.Controls.Add(this.dgvRowMaterial);
            this.panel1.Controls.Add(this.label23);
            this.panel1.Controls.Add(this.txtRawItemCode);
            this.panel1.Controls.Add(this.label22);
            this.panel1.Controls.Add(this.txtItenCode);
            this.panel1.Controls.Add(this.dataGridItem);
            this.panel1.Controls.Add(this.lblExpiryDate);
            this.panel1.Controls.Add(this.ExDate);
            this.panel1.Controls.Add(this.txtProductBatch);
            this.panel1.Controls.Add(this.lblOtherExpenses);
            this.panel1.Controls.Add(this.label26);
            this.panel1.Controls.Add(this.lblDamageCost);
            this.panel1.Controls.Add(this.label24);
            this.panel1.Controls.Add(this.lblProductionCost);
            this.panel1.Controls.Add(this.label21);
            this.panel1.Controls.Add(this.dtpDate);
            this.panel1.Controls.Add(this.label20);
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Controls.Add(this.btnClear);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.label19);
            this.panel1.Controls.Add(this.txtRawDamageQty);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.cmbRawDamageUnit);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Controls.Add(this.label17);
            this.panel1.Controls.Add(this.label18);
            this.panel1.Controls.Add(this.txtRawQty);
            this.panel1.Controls.Add(this.cmbRawUnit);
            this.panel1.Controls.Add(this.cmbRawBatch);
            this.panel1.Controls.Add(this.cmbRawItem);
            this.panel1.Controls.Add(this.txtRawCost);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.txtOtherAmount);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txtProductMRP);
            this.panel1.Controls.Add(this.txtProductQty);
            this.panel1.Controls.Add(this.cmbProductUnit);
            this.panel1.Controls.Add(this.cmbProductItem);
            this.panel1.Controls.Add(this.txtOtherDesc);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtProductCost);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnBrowse);
            this.panel1.Controls.Add(this.txtDocNo);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.dgvRawMaterials);
            this.panel1.Controls.Add(this.btnRawAdd);
            this.panel1.Controls.Add(this.dgvOtherCharges);
            this.panel1.Controls.Add(this.btnOtherAdd);
            this.panel1.Controls.Add(this.dgvProducts);
            this.panel1.Controls.Add(this.btnProductAdd);
            this.panel1.Location = new System.Drawing.Point(1, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1288, 554);
            this.panel1.TabIndex = 105;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // removeRow
            // 
            this.removeRow.AutoSize = true;
            this.removeRow.Location = new System.Drawing.Point(1153, 510);
            this.removeRow.Name = "removeRow";
            this.removeRow.Size = new System.Drawing.Size(72, 13);
            this.removeRow.TabIndex = 110;
            this.removeRow.TabStop = true;
            this.removeRow.Text = "Delete Row";
            this.removeRow.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.removeRow_LinkClicked);
            // 
            // btnMovement
            // 
            this.btnMovement.Location = new System.Drawing.Point(429, 15);
            this.btnMovement.Name = "btnMovement";
            this.btnMovement.Size = new System.Drawing.Size(159, 22);
            this.btnMovement.TabIndex = 109;
            this.btnMovement.Text = "Production Movement";
            this.btnMovement.UseVisualStyleBackColor = true;
            this.btnMovement.Click += new System.EventHandler(this.btnMovement_Click);
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(377, 311);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(43, 13);
            this.label27.TabIndex = 108;
            this.label27.Text = "Batch ";
            this.label27.Click += new System.EventHandler(this.label27_Click);
            // 
            // combManBatch
            // 
            this.combManBatch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combManBatch.FormattingEnabled = true;
            this.combManBatch.Location = new System.Drawing.Point(378, 326);
            this.combManBatch.Name = "combManBatch";
            this.combManBatch.Size = new System.Drawing.Size(116, 21);
            this.combManBatch.TabIndex = 107;
            this.combManBatch.SelectedIndexChanged += new System.EventHandler(this.combManBatch_SelectedIndexChanged);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(492, 309);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(39, 13);
            this.label25.TabIndex = 106;
            this.label25.Text = "Stock";
            // 
            // txt_stock
            // 
            this.txt_stock.Enabled = false;
            this.txt_stock.Location = new System.Drawing.Point(495, 326);
            this.txt_stock.Name = "txt_stock";
            this.txt_stock.Size = new System.Drawing.Size(93, 21);
            this.txt_stock.TabIndex = 105;
            // 
            // FrmProduction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1289, 561);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FrmProduction";
            this.Text = "FrmProduction";
            this.Load += new System.EventHandler(this.FrmProduction_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRawMaterials)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOtherCharges)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRowMaterial)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDocNo;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvProducts;
        private System.Windows.Forms.TextBox txtProductCost;
        private System.Windows.Forms.DataGridView dgvRawMaterials;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgvOtherCharges;
        private System.Windows.Forms.TextBox txtOtherDesc;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbProductItem;
        private System.Windows.Forms.ComboBox cmbProductUnit;
        private System.Windows.Forms.TextBox txtProductQty;
        private System.Windows.Forms.TextBox txtProductMRP;
        private System.Windows.Forms.Button btnProductAdd;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtOtherAmount;
        private System.Windows.Forms.Button btnOtherAdd;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button btnRawAdd;
        private System.Windows.Forms.TextBox txtRawQty;
        private System.Windows.Forms.ComboBox cmbRawUnit;
        private System.Windows.Forms.ComboBox cmbRawBatch;
        private System.Windows.Forms.ComboBox cmbRawItem;
        private System.Windows.Forms.TextBox txtRawCost;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cmbRawDamageUnit;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtRawDamageQty;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label lblProductionCost;
        private System.Windows.Forms.Label lblDamageCost;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label lblOtherExpenses;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox txtProductBatch;
        private System.Windows.Forms.DateTimePicker ExDate;
        private System.Windows.Forms.Label lblExpiryDate;
        private System.Windows.Forms.DataGridView dataGridItem;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txtItenCode;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox txtRawItemCode;
        private System.Windows.Forms.DataGridView dgvRowMaterial;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox txt_stock;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.ComboBox combManBatch;
        private System.Windows.Forms.Button btnMovement;
        private System.Windows.Forms.LinkLabel removeRow;
    }
}