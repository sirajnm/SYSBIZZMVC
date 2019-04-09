namespace Sys_Sols_Inventory.Accounts
{
    partial class Opening_Balance_Entry
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Opening_Balance_Entry));
            this.cmbUnder = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.OPENING_BAL = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.btnOK = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnCancel = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.OPENTYPE = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.dgitems = new System.Windows.Forms.DataGridView();
            this.btnClear = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnDelete = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.Btn_Browse = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.dgv_bulkOpening = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.cmbUnder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OPENTYPE)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgitems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_bulkOpening)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbUnder
            // 
            this.cmbUnder.AllowDrop = true;
            this.cmbUnder.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbUnder.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbUnder.DropDownWidth = 211;
            this.cmbUnder.Location = new System.Drawing.Point(25, 37);
            this.cmbUnder.Name = "cmbUnder";
            this.cmbUnder.Size = new System.Drawing.Size(169, 21);
            this.cmbUnder.TabIndex = 19;
            this.cmbUnder.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbUnder_KeyDown);
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(25, 16);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(97, 20);
            this.kryptonLabel2.TabIndex = 20;
            this.kryptonLabel2.Values.Text = "Account Name :";
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(380, 16);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(61, 20);
            this.kryptonLabel1.TabIndex = 21;
            this.kryptonLabel1.Values.Text = "Amount :";
            // 
            // OPENING_BAL
            // 
            this.OPENING_BAL.Location = new System.Drawing.Point(380, 37);
            this.OPENING_BAL.Name = "OPENING_BAL";
            this.OPENING_BAL.Size = new System.Drawing.Size(157, 20);
            this.OPENING_BAL.TabIndex = 22;
            this.OPENING_BAL.Text = "0.00";
            this.OPENING_BAL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.OPENING_BAL.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAmount_KeyDown);
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(203, 15);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(42, 20);
            this.kryptonLabel3.TabIndex = 23;
            this.kryptonLabel3.Values.Text = "Date :";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(203, 37);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(171, 20);
            this.dateTimePicker1.TabIndex = 24;
            this.dateTimePicker1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dateTimePicker1_KeyDown);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(365, 421);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(69, 25);
            this.btnOK.TabIndex = 27;
            this.btnOK.Values.Text = "Save";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(603, 421);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(77, 25);
            this.btnCancel.TabIndex = 26;
            this.btnCancel.Values.Text = "Close";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // OPENTYPE
            // 
            this.OPENTYPE.AllowDrop = true;
            this.OPENTYPE.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.OPENTYPE.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.OPENTYPE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.OPENTYPE.DropDownWidth = 211;
            this.OPENTYPE.Items.AddRange(new object[] {
            "CR",
            "DR"});
            this.OPENTYPE.Location = new System.Drawing.Point(543, 36);
            this.OPENTYPE.Name = "OPENTYPE";
            this.OPENTYPE.Size = new System.Drawing.Size(113, 21);
            this.OPENTYPE.TabIndex = 28;
            this.OPENTYPE.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OPENTYPE_KeyDown);
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(543, 15);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(43, 20);
            this.kryptonLabel4.TabIndex = 29;
            this.kryptonLabel4.Values.Text = "Type :";
            // 
            // dgitems
            // 
            this.dgitems.AllowUserToAddRows = false;
            this.dgitems.AllowUserToDeleteRows = false;
            this.dgitems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgitems.Location = new System.Drawing.Point(12, 64);
            this.dgitems.Name = "dgitems";
            this.dgitems.ReadOnly = true;
            this.dgitems.Size = new System.Drawing.Size(668, 336);
            this.dgitems.TabIndex = 67;
            this.dgitems.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgitems_CellClick);
            this.dgitems.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgitems_KeyDown);
            // 
            // btnClear
            // 
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClear.Location = new System.Drawing.Point(521, 421);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(77, 25);
            this.btnClear.TabIndex = 68;
            this.btnClear.Values.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnDelete.Location = new System.Drawing.Point(439, 421);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(77, 25);
            this.btnDelete.TabIndex = 69;
            this.btnDelete.Values.Text = "Delete ";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // Btn_Browse
            // 
            this.Btn_Browse.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Btn_Browse.Location = new System.Drawing.Point(12, 421);
            this.Btn_Browse.Name = "Btn_Browse";
            this.Btn_Browse.Size = new System.Drawing.Size(133, 25);
            this.Btn_Browse.TabIndex = 70;
            this.Btn_Browse.Values.Text = "<< Browse Excel >>";
            this.Btn_Browse.Click += new System.EventHandler(this.Btn_Browse_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // dgv_bulkOpening
            // 
            this.dgv_bulkOpening.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_bulkOpening.Location = new System.Drawing.Point(7, 64);
            this.dgv_bulkOpening.Name = "dgv_bulkOpening";
            this.dgv_bulkOpening.Size = new System.Drawing.Size(681, 335);
            this.dgv_bulkOpening.TabIndex = 71;
            this.dgv_bulkOpening.Visible = false;
            // 
            // Opening_Balance_Entry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(702, 458);
            this.Controls.Add(this.dgv_bulkOpening);
            this.Controls.Add(this.Btn_Browse);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.dgitems);
            this.Controls.Add(this.OPENTYPE);
            this.Controls.Add(this.kryptonLabel4);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.kryptonLabel3);
            this.Controls.Add(this.OPENING_BAL);
            this.Controls.Add(this.kryptonLabel1);
            this.Controls.Add(this.cmbUnder);
            this.Controls.Add(this.kryptonLabel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Opening_Balance_Entry";
            this.Text = "Opening Balance Entry";
            this.Load += new System.EventHandler(this.Opening_Balance_Entry_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cmbUnder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OPENTYPE)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgitems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_bulkOpening)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cmbUnder;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox OPENING_BAL;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnOK;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnCancel;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox OPENTYPE;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private System.Windows.Forms.DataGridView dgitems;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnClear;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnDelete;
        private ComponentFactory.Krypton.Toolkit.KryptonButton Btn_Browse;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.DataGridView dgv_bulkOpening;
    }
}