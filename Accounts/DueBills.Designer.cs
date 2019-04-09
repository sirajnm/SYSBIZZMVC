namespace Sys_Sols_Inventory.Accounts
{
    partial class DueBills
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DueBills));
            this.panel1 = new System.Windows.Forms.Panel();
            this.Date_To = new System.Windows.Forms.DateTimePicker();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.Date_From = new System.Windows.Forms.DateTimePicker();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.cmbSuppliers = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.btnSave = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnClear = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.drgDueBillsCustomers = new System.Windows.Forms.DataGridView();
            this.cDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cVoucherNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cVoucherType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cParticulars = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cToAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cPaidAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cBalance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cNarration = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgDuebills = new System.Windows.Forms.DataGridView();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VoucherNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VoucherType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Particulars = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ToAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PaidAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Balance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Narration = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSuppliers)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.drgDueBillsCustomers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgDuebills)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Date_To);
            this.panel1.Controls.Add(this.kryptonLabel1);
            this.panel1.Controls.Add(this.Date_From);
            this.panel1.Controls.Add(this.kryptonLabel3);
            this.panel1.Controls.Add(this.cmbSuppliers);
            this.panel1.Controls.Add(this.kryptonLabel2);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.btnClear);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(887, 62);
            this.panel1.TabIndex = 0;
            // 
            // Date_To
            // 
            this.Date_To.CustomFormat = "dd/MM/yyyy";
            this.Date_To.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Date_To.Location = new System.Drawing.Point(676, 34);
            this.Date_To.Name = "Date_To";
            this.Date_To.Size = new System.Drawing.Size(176, 20);
            this.Date_To.TabIndex = 73;
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(624, 34);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(30, 20);
            this.kryptonLabel1.TabIndex = 72;
            this.kryptonLabel1.Values.Text = "To :";
            // 
            // Date_From
            // 
            this.Date_From.CustomFormat = "dd/MM/yyyy";
            this.Date_From.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Date_From.Location = new System.Drawing.Point(676, 8);
            this.Date_From.Name = "Date_From";
            this.Date_From.Size = new System.Drawing.Size(176, 20);
            this.Date_From.TabIndex = 71;
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(624, 8);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(45, 20);
            this.kryptonLabel3.TabIndex = 70;
            this.kryptonLabel3.Values.Text = "From :";
            // 
            // cmbSuppliers
            // 
            this.cmbSuppliers.AllowDrop = true;
            this.cmbSuppliers.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbSuppliers.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbSuppliers.DropDownWidth = 211;
            this.cmbSuppliers.Location = new System.Drawing.Point(99, 14);
            this.cmbSuppliers.Name = "cmbSuppliers";
            this.cmbSuppliers.Size = new System.Drawing.Size(182, 21);
            this.cmbSuppliers.TabIndex = 69;
            this.cmbSuppliers.SelectedIndexChanged += new System.EventHandler(this.cmbSuppliers_SelectedIndexChanged);
            this.cmbSuppliers.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbSuppliers_KeyDown);
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(42, 13);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(54, 20);
            this.kryptonLabel2.TabIndex = 68;
            this.kryptonLabel2.Values.Text = "Ledger :";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(288, 12);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 25);
            this.btnSave.TabIndex = 66;
            this.btnSave.Values.Text = "Search";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(384, 12);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(90, 25);
            this.btnClear.TabIndex = 67;
            this.btnClear.Values.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.drgDueBillsCustomers);
            this.panel2.Controls.Add(this.dgDuebills);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 62);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(887, 365);
            this.panel2.TabIndex = 1;
            // 
            // drgDueBillsCustomers
            // 
            this.drgDueBillsCustomers.AllowUserToAddRows = false;
            this.drgDueBillsCustomers.AllowUserToDeleteRows = false;
            this.drgDueBillsCustomers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.drgDueBillsCustomers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cDate,
            this.cVoucherNo,
            this.cVoucherType,
            this.cParticulars,
            this.cToAmount,
            this.cPaidAmount,
            this.cBalance,
            this.cNarration});
            this.drgDueBillsCustomers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.drgDueBillsCustomers.Location = new System.Drawing.Point(0, 0);
            this.drgDueBillsCustomers.Name = "drgDueBillsCustomers";
            this.drgDueBillsCustomers.ReadOnly = true;
            this.drgDueBillsCustomers.Size = new System.Drawing.Size(887, 365);
            this.drgDueBillsCustomers.TabIndex = 1;
            this.drgDueBillsCustomers.Visible = false;
            // 
            // cDate
            // 
            this.cDate.HeaderText = "Date";
            this.cDate.Name = "cDate";
            this.cDate.ReadOnly = true;
            // 
            // cVoucherNo
            // 
            this.cVoucherNo.HeaderText = "Voucher No";
            this.cVoucherNo.Name = "cVoucherNo";
            this.cVoucherNo.ReadOnly = true;
            // 
            // cVoucherType
            // 
            this.cVoucherType.HeaderText = "Voucher Type";
            this.cVoucherType.Name = "cVoucherType";
            this.cVoucherType.ReadOnly = true;
            // 
            // cParticulars
            // 
            this.cParticulars.HeaderText = "Particulars";
            this.cParticulars.Name = "cParticulars";
            this.cParticulars.ReadOnly = true;
            this.cParticulars.Width = 230;
            // 
            // cToAmount
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.cToAmount.DefaultCellStyle = dataGridViewCellStyle1;
            this.cToAmount.HeaderText = "Debits";
            this.cToAmount.Name = "cToAmount";
            this.cToAmount.ReadOnly = true;
            // 
            // cPaidAmount
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.cPaidAmount.DefaultCellStyle = dataGridViewCellStyle2;
            this.cPaidAmount.HeaderText = "Credits";
            this.cPaidAmount.Name = "cPaidAmount";
            this.cPaidAmount.ReadOnly = true;
            // 
            // cBalance
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.cBalance.DefaultCellStyle = dataGridViewCellStyle3;
            this.cBalance.HeaderText = "Balance";
            this.cBalance.Name = "cBalance";
            this.cBalance.ReadOnly = true;
            // 
            // cNarration
            // 
            this.cNarration.HeaderText = "Narration";
            this.cNarration.Name = "cNarration";
            this.cNarration.ReadOnly = true;
            // 
            // dgDuebills
            // 
            this.dgDuebills.AllowUserToAddRows = false;
            this.dgDuebills.AllowUserToDeleteRows = false;
            this.dgDuebills.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgDuebills.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Date,
            this.VoucherNo,
            this.VoucherType,
            this.Particulars,
            this.ToAmount,
            this.PaidAmount,
            this.Balance,
            this.Narration});
            this.dgDuebills.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgDuebills.Location = new System.Drawing.Point(0, 0);
            this.dgDuebills.Name = "dgDuebills";
            this.dgDuebills.ReadOnly = true;
            this.dgDuebills.Size = new System.Drawing.Size(887, 365);
            this.dgDuebills.TabIndex = 0;
            // 
            // Date
            // 
            this.Date.HeaderText = "Date";
            this.Date.Name = "Date";
            this.Date.ReadOnly = true;
            // 
            // VoucherNo
            // 
            this.VoucherNo.HeaderText = "Voucher No";
            this.VoucherNo.Name = "VoucherNo";
            this.VoucherNo.ReadOnly = true;
            // 
            // VoucherType
            // 
            this.VoucherType.HeaderText = "Voucher Type";
            this.VoucherType.Name = "VoucherType";
            this.VoucherType.ReadOnly = true;
            // 
            // Particulars
            // 
            this.Particulars.HeaderText = "Particulars";
            this.Particulars.Name = "Particulars";
            this.Particulars.ReadOnly = true;
            this.Particulars.Width = 230;
            // 
            // ToAmount
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.ToAmount.DefaultCellStyle = dataGridViewCellStyle4;
            this.ToAmount.HeaderText = "Bill Amount";
            this.ToAmount.Name = "ToAmount";
            this.ToAmount.ReadOnly = true;
            // 
            // PaidAmount
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.PaidAmount.DefaultCellStyle = dataGridViewCellStyle5;
            this.PaidAmount.HeaderText = "Amount Paid";
            this.PaidAmount.Name = "PaidAmount";
            this.PaidAmount.ReadOnly = true;
            // 
            // Balance
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.Balance.DefaultCellStyle = dataGridViewCellStyle6;
            this.Balance.HeaderText = "Balance";
            this.Balance.Name = "Balance";
            this.Balance.ReadOnly = true;
            // 
            // Narration
            // 
            this.Narration.HeaderText = "Narration";
            this.Narration.Name = "Narration";
            this.Narration.ReadOnly = true;
            // 
            // DueBills
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(887, 427);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "DueBills";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Due Bills";
            this.Load += new System.EventHandler(this.DueBills_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSuppliers)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.drgDueBillsCustomers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgDuebills)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cmbSuppliers;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSave;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnClear;
        private System.Windows.Forms.DataGridView dgDuebills;
        private System.Windows.Forms.DateTimePicker Date_To;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private System.Windows.Forms.DateTimePicker Date_From;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn VoucherNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn VoucherType;
        private System.Windows.Forms.DataGridViewTextBoxColumn Particulars;
        private System.Windows.Forms.DataGridViewTextBoxColumn ToAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn PaidAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Balance;
        private System.Windows.Forms.DataGridViewTextBoxColumn Narration;
        private System.Windows.Forms.DataGridView drgDueBillsCustomers;
        private System.Windows.Forms.DataGridViewTextBoxColumn cDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn cVoucherNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn cVoucherType;
        private System.Windows.Forms.DataGridViewTextBoxColumn cParticulars;
        private System.Windows.Forms.DataGridViewTextBoxColumn cToAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn cPaidAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn cBalance;
        private System.Windows.Forms.DataGridViewTextBoxColumn cNarration;
    }
}