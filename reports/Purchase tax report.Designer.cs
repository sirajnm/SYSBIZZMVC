namespace Sys_Sols_Inventory.reports
{
    partial class Purchase_tax_report
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cmb_saletype = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.kryptonLabel11 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.Cbx_salemode = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.Cbx_supplier = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.kryptonLabel5 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel7 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.EndDate = new System.Windows.Forms.DateTimePicker();
            this.kryptonLabel8 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.StartDate = new System.Windows.Forms.DateTimePicker();
            this.kryptonLabel6 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.Chk = new System.Windows.Forms.CheckBox();
            this.btn_exprt = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btn_search = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.dgv_grdview = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_saletype)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cbx_salemode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cbx_supplier)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_grdview)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.btn_exprt);
            this.groupBox1.Controls.Add(this.btn_search);
            this.groupBox1.Location = new System.Drawing.Point(14, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1042, 119);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.cmb_saletype);
            this.groupBox3.Controls.Add(this.kryptonLabel11);
            this.groupBox3.Controls.Add(this.Cbx_salemode);
            this.groupBox3.Controls.Add(this.Cbx_supplier);
            this.groupBox3.Controls.Add(this.kryptonLabel5);
            this.groupBox3.Controls.Add(this.kryptonLabel7);
            this.groupBox3.Location = new System.Drawing.Point(361, 8);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(430, 101);
            this.groupBox3.TabIndex = 142;
            this.groupBox3.TabStop = false;
            // 
            // cmb_saletype
            // 
            this.cmb_saletype.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmb_saletype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_saletype.DropDownWidth = 211;
            this.cmb_saletype.Items.AddRange(new object[] {
            "",
            "ESTIMATE",
            "GST PURCHASE"});
            this.cmb_saletype.Location = new System.Drawing.Point(151, 15);
            this.cmb_saletype.Name = "cmb_saletype";
            this.cmb_saletype.Size = new System.Drawing.Size(241, 21);
            this.cmb_saletype.TabIndex = 140;
            this.cmb_saletype.SelectedIndexChanged += new System.EventHandler(this.cmb_saletype_SelectedIndexChanged);
            // 
            // kryptonLabel11
            // 
            this.kryptonLabel11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonLabel11.Location = new System.Drawing.Point(39, 19);
            this.kryptonLabel11.Name = "kryptonLabel11";
            this.kryptonLabel11.Size = new System.Drawing.Size(92, 20);
            this.kryptonLabel11.TabIndex = 138;
            this.kryptonLabel11.Values.Text = "Purchase Type:";
            // 
            // Cbx_salemode
            // 
            this.Cbx_salemode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Cbx_salemode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cbx_salemode.DropDownWidth = 211;
            this.Cbx_salemode.Items.AddRange(new object[] {
            "",
            "PURCHASE RETURN",
            "CASH PURCHASE",
            "CREDIT PURCHASE"});
            this.Cbx_salemode.Location = new System.Drawing.Point(151, 42);
            this.Cbx_salemode.Name = "Cbx_salemode";
            this.Cbx_salemode.Size = new System.Drawing.Size(241, 21);
            this.Cbx_salemode.TabIndex = 137;
            this.Cbx_salemode.SelectedIndexChanged += new System.EventHandler(this.Cbx_salemode_SelectedIndexChanged);
            // 
            // Cbx_supplier
            // 
            this.Cbx_supplier.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Cbx_supplier.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.Cbx_supplier.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.Cbx_supplier.DropDownWidth = 211;
            this.Cbx_supplier.Location = new System.Drawing.Point(151, 69);
            this.Cbx_supplier.Name = "Cbx_supplier";
            this.Cbx_supplier.Size = new System.Drawing.Size(241, 21);
            this.Cbx_supplier.TabIndex = 136;
            this.Cbx_supplier.SelectedIndexChanged += new System.EventHandler(this.Cbx_supplier_SelectedIndexChanged);
            // 
            // kryptonLabel5
            // 
            this.kryptonLabel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonLabel5.Location = new System.Drawing.Point(39, 69);
            this.kryptonLabel5.Name = "kryptonLabel5";
            this.kryptonLabel5.Size = new System.Drawing.Size(61, 20);
            this.kryptonLabel5.TabIndex = 135;
            this.kryptonLabel5.Values.Text = "Supplier :";
            // 
            // kryptonLabel7
            // 
            this.kryptonLabel7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonLabel7.Location = new System.Drawing.Point(39, 44);
            this.kryptonLabel7.Name = "kryptonLabel7";
            this.kryptonLabel7.Size = new System.Drawing.Size(88, 20);
            this.kryptonLabel7.TabIndex = 134;
            this.kryptonLabel7.Values.Text = "Voucher Type:";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.EndDate);
            this.groupBox2.Controls.Add(this.kryptonLabel8);
            this.groupBox2.Controls.Add(this.StartDate);
            this.groupBox2.Controls.Add(this.kryptonLabel6);
            this.groupBox2.Controls.Add(this.Chk);
            this.groupBox2.Location = new System.Drawing.Point(17, 8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(329, 101);
            this.groupBox2.TabIndex = 141;
            this.groupBox2.TabStop = false;
            // 
            // EndDate
            // 
            this.EndDate.Enabled = false;
            this.EndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.EndDate.Location = new System.Drawing.Point(105, 64);
            this.EndDate.Name = "EndDate";
            this.EndDate.Size = new System.Drawing.Size(184, 21);
            this.EndDate.TabIndex = 27;
            // 
            // kryptonLabel8
            // 
            this.kryptonLabel8.Location = new System.Drawing.Point(21, 64);
            this.kryptonLabel8.Name = "kryptonLabel8";
            this.kryptonLabel8.Size = new System.Drawing.Size(63, 20);
            this.kryptonLabel8.TabIndex = 26;
            this.kryptonLabel8.Values.Text = "End Date:";
            // 
            // StartDate
            // 
            this.StartDate.Enabled = false;
            this.StartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.StartDate.Location = new System.Drawing.Point(105, 37);
            this.StartDate.Name = "StartDate";
            this.StartDate.Size = new System.Drawing.Size(184, 21);
            this.StartDate.TabIndex = 24;
            // 
            // kryptonLabel6
            // 
            this.kryptonLabel6.Location = new System.Drawing.Point(21, 37);
            this.kryptonLabel6.Name = "kryptonLabel6";
            this.kryptonLabel6.Size = new System.Drawing.Size(71, 20);
            this.kryptonLabel6.TabIndex = 23;
            this.kryptonLabel6.Values.Text = "Start  Date:";
            // 
            // Chk
            // 
            this.Chk.AutoSize = true;
            this.Chk.Location = new System.Drawing.Point(27, 15);
            this.Chk.Name = "Chk";
            this.Chk.Size = new System.Drawing.Size(113, 17);
            this.Chk.TabIndex = 25;
            this.Chk.Text = "Report on Date";
            this.Chk.UseVisualStyleBackColor = true;
            this.Chk.CheckedChanged += new System.EventHandler(this.Chk_CheckedChanged);
            // 
            // btn_exprt
            // 
            this.btn_exprt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_exprt.Location = new System.Drawing.Point(839, 67);
            this.btn_exprt.Name = "btn_exprt";
            this.btn_exprt.Size = new System.Drawing.Size(155, 25);
            this.btn_exprt.TabIndex = 30;
            this.btn_exprt.Values.Text = "Export";
            this.btn_exprt.Click += new System.EventHandler(this.btn_exprt_Click);
            // 
            // btn_search
            // 
            this.btn_search.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_search.Location = new System.Drawing.Point(839, 33);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(155, 25);
            this.btn_search.TabIndex = 29;
            this.btn_search.Values.Text = "Search";
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // dgv_grdview
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightPink;
            this.dgv_grdview.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_grdview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_grdview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_grdview.Location = new System.Drawing.Point(14, 133);
            this.dgv_grdview.Name = "dgv_grdview";
            this.dgv_grdview.RowHeadersVisible = false;
            this.dgv_grdview.Size = new System.Drawing.Size(1042, 536);
            this.dgv_grdview.TabIndex = 1;
            // 
            // Purchase_tax_report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1065, 681);
            this.Controls.Add(this.dgv_grdview);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Purchase_tax_report";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Purchase_tax_report";
            this.Load += new System.EventHandler(this.Purchase_tax_report_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_saletype)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cbx_salemode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cbx_supplier)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_grdview)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox Chk;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btn_exprt;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btn_search;
        private System.Windows.Forms.DataGridView dgv_grdview;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cmb_saletype;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel11;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox Cbx_salemode;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox Cbx_supplier;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel5;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel7;
        private System.Windows.Forms.DateTimePicker EndDate;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel8;
        private System.Windows.Forms.DateTimePicker StartDate;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel6;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}