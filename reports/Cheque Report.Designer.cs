namespace Sys_Sols_Inventory.reports
{
    partial class Cheque_Report
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
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rb_reciept = new System.Windows.Forms.RadioButton();
            this.rb_voucher = new System.Windows.Forms.RadioButton();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.rbtn_sort = new System.Windows.Forms.RadioButton();
            this.rbtn_all = new System.Windows.Forms.RadioButton();
            this.gb_date = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtp_to = new System.Windows.Forms.DateTimePicker();
            this.dtp_from = new System.Windows.Forms.DateTimePicker();
            this.gb_sort = new System.Windows.Forms.GroupBox();
            this.cmb_party = new System.Windows.Forms.ComboBox();
            this.cmb_bank = new System.Windows.Forms.ComboBox();
            this.ch_Party = new System.Windows.Forms.CheckBox();
            this.ch_Bank = new System.Windows.Forms.CheckBox();
            this.btn_view = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgv_cheque = new System.Windows.Forms.DataGridView();
            this.slno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Voucher_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PARTY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.T_DATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CHEQUE_DATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STATUS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CHQ_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BANK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NOTES = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TRANSACTION_DATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btn_Delete = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmb_status = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_chqno = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_amount = new System.Windows.Forms.TextBox();
            this.btn_update = new System.Windows.Forms.Button();
            this.dtp_transdate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_voucher_no = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.gb_date.SuspendLayout();
            this.gb_sort.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_cheque)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Controls.Add(this.gb_date);
            this.groupBox1.Controls.Add(this.gb_sort);
            this.groupBox1.Controls.Add(this.btn_view);
            this.groupBox1.Location = new System.Drawing.Point(14, 3);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Size = new System.Drawing.Size(1066, 109);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rb_reciept);
            this.groupBox4.Controls.Add(this.rb_voucher);
            this.groupBox4.Location = new System.Drawing.Point(87, 9);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(158, 90);
            this.groupBox4.TabIndex = 10;
            this.groupBox4.TabStop = false;
            // 
            // rb_reciept
            // 
            this.rb_reciept.AutoSize = true;
            this.rb_reciept.Checked = true;
            this.rb_reciept.ForeColor = System.Drawing.Color.Blue;
            this.rb_reciept.Location = new System.Drawing.Point(8, 51);
            this.rb_reciept.Name = "rb_reciept";
            this.rb_reciept.Size = new System.Drawing.Size(135, 17);
            this.rb_reciept.TabIndex = 0;
            this.rb_reciept.TabStop = true;
            this.rb_reciept.Text = "RECIEPT CHEQUES";
            this.rb_reciept.UseVisualStyleBackColor = true;
            this.rb_reciept.CheckedChanged += new System.EventHandler(this.rb_reciept_CheckedChanged);
            // 
            // rb_voucher
            // 
            this.rb_voucher.AutoSize = true;
            this.rb_voucher.ForeColor = System.Drawing.Color.Blue;
            this.rb_voucher.Location = new System.Drawing.Point(8, 23);
            this.rb_voucher.Name = "rb_voucher";
            this.rb_voucher.Size = new System.Drawing.Size(142, 17);
            this.rb_voucher.TabIndex = 0;
            this.rb_voucher.Text = "VOUCHER CHEQUES";
            this.rb_voucher.UseVisualStyleBackColor = true;
            this.rb_voucher.CheckedChanged += new System.EventHandler(this.rb_voucher_CheckedChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.rbtn_sort);
            this.groupBox5.Controls.Add(this.rbtn_all);
            this.groupBox5.Location = new System.Drawing.Point(7, 9);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(77, 90);
            this.groupBox5.TabIndex = 9;
            this.groupBox5.TabStop = false;
            // 
            // rbtn_sort
            // 
            this.rbtn_sort.AutoSize = true;
            this.rbtn_sort.Checked = true;
            this.rbtn_sort.ForeColor = System.Drawing.Color.Blue;
            this.rbtn_sort.Location = new System.Drawing.Point(16, 51);
            this.rbtn_sort.Name = "rbtn_sort";
            this.rbtn_sort.Size = new System.Drawing.Size(57, 17);
            this.rbtn_sort.TabIndex = 0;
            this.rbtn_sort.TabStop = true;
            this.rbtn_sort.Text = "SORT";
            this.rbtn_sort.UseVisualStyleBackColor = true;
            this.rbtn_sort.CheckedChanged += new System.EventHandler(this.rbtn_sort_CheckedChanged);
            // 
            // rbtn_all
            // 
            this.rbtn_all.AutoSize = true;
            this.rbtn_all.ForeColor = System.Drawing.Color.Blue;
            this.rbtn_all.Location = new System.Drawing.Point(16, 23);
            this.rbtn_all.Name = "rbtn_all";
            this.rbtn_all.Size = new System.Drawing.Size(45, 17);
            this.rbtn_all.TabIndex = 0;
            this.rbtn_all.Text = "ALL";
            this.rbtn_all.UseVisualStyleBackColor = true;
            this.rbtn_all.CheckedChanged += new System.EventHandler(this.rbtn_all_CheckedChanged);
            // 
            // gb_date
            // 
            this.gb_date.Controls.Add(this.label2);
            this.gb_date.Controls.Add(this.label1);
            this.gb_date.Controls.Add(this.dtp_to);
            this.gb_date.Controls.Add(this.dtp_from);
            this.gb_date.Location = new System.Drawing.Point(250, 9);
            this.gb_date.Name = "gb_date";
            this.gb_date.Size = new System.Drawing.Size(324, 90);
            this.gb_date.TabIndex = 8;
            this.gb_date.TabStop = false;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Location = new System.Drawing.Point(22, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = "To:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Location = new System.Drawing.Point(22, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 21);
            this.label1.TabIndex = 2;
            this.label1.Text = "From :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtp_to
            // 
            this.dtp_to.Location = new System.Drawing.Point(105, 51);
            this.dtp_to.Name = "dtp_to";
            this.dtp_to.Size = new System.Drawing.Size(200, 21);
            this.dtp_to.TabIndex = 1;
            // 
            // dtp_from
            // 
            this.dtp_from.Location = new System.Drawing.Point(105, 20);
            this.dtp_from.Name = "dtp_from";
            this.dtp_from.Size = new System.Drawing.Size(200, 21);
            this.dtp_from.TabIndex = 0;
            // 
            // gb_sort
            // 
            this.gb_sort.Controls.Add(this.cmb_party);
            this.gb_sort.Controls.Add(this.cmb_bank);
            this.gb_sort.Controls.Add(this.ch_Party);
            this.gb_sort.Controls.Add(this.ch_Bank);
            this.gb_sort.Location = new System.Drawing.Point(578, 9);
            this.gb_sort.Name = "gb_sort";
            this.gb_sort.Size = new System.Drawing.Size(317, 91);
            this.gb_sort.TabIndex = 7;
            this.gb_sort.TabStop = false;
            // 
            // cmb_party
            // 
            this.cmb_party.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmb_party.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmb_party.DisplayMember = "LEDGERNAME";
            this.cmb_party.Enabled = false;
            this.cmb_party.FormattingEnabled = true;
            this.cmb_party.Location = new System.Drawing.Point(77, 52);
            this.cmb_party.Name = "cmb_party";
            this.cmb_party.Size = new System.Drawing.Size(227, 21);
            this.cmb_party.TabIndex = 5;
            this.cmb_party.ValueMember = "DEBIT_CODE";
            // 
            // cmb_bank
            // 
            this.cmb_bank.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmb_bank.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmb_bank.DisplayMember = "LEDGERNAME";
            this.cmb_bank.Enabled = false;
            this.cmb_bank.FormattingEnabled = true;
            this.cmb_bank.Location = new System.Drawing.Point(77, 21);
            this.cmb_bank.Name = "cmb_bank";
            this.cmb_bank.Size = new System.Drawing.Size(227, 21);
            this.cmb_bank.TabIndex = 4;
            this.cmb_bank.ValueMember = "CREDIT_CODE";
            // 
            // ch_Party
            // 
            this.ch_Party.AutoSize = true;
            this.ch_Party.ForeColor = System.Drawing.Color.Blue;
            this.ch_Party.Location = new System.Drawing.Point(11, 54);
            this.ch_Party.Name = "ch_Party";
            this.ch_Party.Size = new System.Drawing.Size(43, 17);
            this.ch_Party.TabIndex = 3;
            this.ch_Party.Text = "To ";
            this.ch_Party.UseVisualStyleBackColor = true;
            this.ch_Party.CheckedChanged += new System.EventHandler(this.ch_Party_CheckedChanged);
            // 
            // ch_Bank
            // 
            this.ch_Bank.AutoSize = true;
            this.ch_Bank.ForeColor = System.Drawing.Color.Blue;
            this.ch_Bank.Location = new System.Drawing.Point(11, 23);
            this.ch_Bank.Name = "ch_Bank";
            this.ch_Bank.Size = new System.Drawing.Size(55, 17);
            this.ch_Bank.TabIndex = 3;
            this.ch_Bank.Text = "From";
            this.ch_Bank.UseVisualStyleBackColor = true;
            this.ch_Bank.CheckedChanged += new System.EventHandler(this.ch_Bank_CheckedChanged);
            // 
            // btn_view
            // 
            this.btn_view.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_view.ForeColor = System.Drawing.Color.Black;
            this.btn_view.Location = new System.Drawing.Point(908, 48);
            this.btn_view.Name = "btn_view";
            this.btn_view.Size = new System.Drawing.Size(146, 29);
            this.btn_view.TabIndex = 6;
            this.btn_view.Text = "View";
            this.btn_view.UseVisualStyleBackColor = true;
            this.btn_view.Click += new System.EventHandler(this.btn_view_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgv_cheque);
            this.groupBox2.Location = new System.Drawing.Point(14, 109);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1066, 413);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // dgv_cheque
            // 
            this.dgv_cheque.AllowUserToAddRows = false;
            this.dgv_cheque.AllowUserToDeleteRows = false;
            this.dgv_cheque.AllowUserToOrderColumns = true;
            this.dgv_cheque.AllowUserToResizeColumns = false;
            this.dgv_cheque.AllowUserToResizeRows = false;
            this.dgv_cheque.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_cheque.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.slno,
            this.Voucher_no,
            this.PARTY,
            this.T_DATE,
            this.CHEQUE_DATE,
            this.STATUS,
            this.CHQ_NO,
            this.BANK,
            this.NOTES,
            this.TRANSACTION_DATE});
            this.dgv_cheque.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgv_cheque.Location = new System.Drawing.Point(11, 17);
            this.dgv_cheque.Name = "dgv_cheque";
            this.dgv_cheque.RowHeadersVisible = false;
            this.dgv_cheque.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgv_cheque.Size = new System.Drawing.Size(1043, 377);
            this.dgv_cheque.TabIndex = 0;
            this.dgv_cheque.DoubleClick += new System.EventHandler(this.dgv_cheque_DoubleClick);
            // 
            // slno
            // 
            this.slno.HeaderText = "SL_NO";
            this.slno.Name = "slno";
            this.slno.Width = 25;
            // 
            // Voucher_no
            // 
            this.Voucher_no.DataPropertyName = "REC_NO";
            this.Voucher_no.HeaderText = "Voucher No";
            this.Voucher_no.Name = "Voucher_no";
            this.Voucher_no.Width = 50;
            // 
            // PARTY
            // 
            this.PARTY.DataPropertyName = "DEBITER";
            this.PARTY.HeaderText = "PARTY";
            this.PARTY.Name = "PARTY";
            this.PARTY.Width = 220;
            // 
            // T_DATE
            // 
            this.T_DATE.DataPropertyName = "DOC_DATE_GRE";
            this.T_DATE.HeaderText = "VOU.DATE";
            this.T_DATE.Name = "T_DATE";
            // 
            // CHEQUE_DATE
            // 
            this.CHEQUE_DATE.DataPropertyName = "CHQ_DATE";
            this.CHEQUE_DATE.HeaderText = "CHQ_DATE";
            this.CHEQUE_DATE.Name = "CHEQUE_DATE";
            // 
            // STATUS
            // 
            this.STATUS.DataPropertyName = "CHQ STS";
            this.STATUS.HeaderText = "STATUS";
            this.STATUS.Name = "STATUS";
            this.STATUS.Width = 90;
            // 
            // CHQ_NO
            // 
            this.CHQ_NO.DataPropertyName = "CHQ_NO";
            this.CHQ_NO.HeaderText = "CHQ_NO";
            this.CHQ_NO.Name = "CHQ_NO";
            this.CHQ_NO.Width = 90;
            // 
            // BANK
            // 
            this.BANK.DataPropertyName = "CREDITER";
            this.BANK.HeaderText = "ACCOUNT";
            this.BANK.Name = "BANK";
            this.BANK.Width = 220;
            // 
            // NOTES
            // 
            this.NOTES.DataPropertyName = "NOTES";
            this.NOTES.HeaderText = "NOTES";
            this.NOTES.Name = "NOTES";
            this.NOTES.Width = 250;
            // 
            // TRANSACTION_DATE
            // 
            this.TRANSACTION_DATE.DataPropertyName = "TRANSACTION_DATE";
            this.TRANSACTION_DATE.HeaderText = "TRANSACTION_DATE";
            this.TRANSACTION_DATE.Name = "TRANSACTION_DATE";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "SL_NO";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 50;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "REC_NO";
            this.dataGridViewTextBoxColumn2.HeaderText = "VOUCHER NO";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 150;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "DEBITER";
            this.dataGridViewTextBoxColumn3.HeaderText = "TRANS_DATE";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 150;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "DOC_DATE_GRE";
            this.dataGridViewTextBoxColumn4.HeaderText = "CHQ_DATE";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 150;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "CHQ_DATE";
            this.dataGridViewTextBoxColumn5.HeaderText = "PARTY";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Width = 220;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "AMOUNT";
            this.dataGridViewTextBoxColumn6.HeaderText = "AMOUNT";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.Width = 220;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "CHQ STS";
            this.dataGridViewTextBoxColumn7.HeaderText = "CHQ_NO";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.Width = 220;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "CHQ_NO";
            this.dataGridViewTextBoxColumn8.HeaderText = "ACCOUNT";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.Width = 220;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.DataPropertyName = "CREDITER";
            this.dataGridViewTextBoxColumn9.HeaderText = "NOTES";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.Width = 250;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.DataPropertyName = "NOTES";
            this.dataGridViewTextBoxColumn10.HeaderText = "STATUS";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.Width = 150;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.DataPropertyName = "TRANSACTION_DATE";
            this.dataGridViewTextBoxColumn11.HeaderText = "TRANSACTION_DATE";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.btn_Delete);
            this.groupBox3.Controls.Add(this.panel1);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.txt_chqno);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.txt_amount);
            this.groupBox3.Controls.Add(this.btn_update);
            this.groupBox3.Controls.Add(this.dtp_transdate);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.txt_voucher_no);
            this.groupBox3.ForeColor = System.Drawing.Color.Red;
            this.groupBox3.Location = new System.Drawing.Point(15, 524);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1065, 104);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Update Cheque";
            this.groupBox3.Enter += new System.EventHandler(this.groupBox3_Enter);
            // 
            // btn_Delete
            // 
            this.btn_Delete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Delete.ForeColor = System.Drawing.Color.Red;
            this.btn_Delete.Location = new System.Drawing.Point(953, 52);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(100, 23);
            this.btn_Delete.TabIndex = 12;
            this.btn_Delete.Text = "Delete Cheque";
            this.btn_Delete.UseVisualStyleBackColor = true;
            this.btn_Delete.Click += new System.EventHandler(this.btn_Delete_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.cmb_status);
            this.panel1.Location = new System.Drawing.Point(614, 53);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(227, 21);
            this.panel1.TabIndex = 11;
            // 
            // cmb_status
            // 
            this.cmb_status.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmb_status.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmb_status.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmb_status.FormattingEnabled = true;
            this.cmb_status.Items.AddRange(new object[] {
            "- SELECT -",
            "POSTED",
            "CANCELED",
            "PENDING"});
            this.cmb_status.Location = new System.Drawing.Point(0, 0);
            this.cmb_status.Name = "cmb_status";
            this.cmb_status.Size = new System.Drawing.Size(227, 21);
            this.cmb_status.TabIndex = 5;
            this.cmb_status.Text = "- SELECT -";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(22, 76);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Chq. No :";
            // 
            // txt_chqno
            // 
            this.txt_chqno.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_chqno.Enabled = false;
            this.txt_chqno.Location = new System.Drawing.Point(85, 73);
            this.txt_chqno.Name = "txt_chqno";
            this.txt_chqno.Size = new System.Drawing.Size(185, 21);
            this.txt_chqno.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(325, 41);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Amount :";
            // 
            // txt_amount
            // 
            this.txt_amount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_amount.Enabled = false;
            this.txt_amount.Location = new System.Drawing.Point(385, 38);
            this.txt_amount.Name = "txt_amount";
            this.txt_amount.Size = new System.Drawing.Size(155, 21);
            this.txt_amount.TabIndex = 7;
            // 
            // btn_update
            // 
            this.btn_update.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_update.ForeColor = System.Drawing.Color.Black;
            this.btn_update.Location = new System.Drawing.Point(847, 52);
            this.btn_update.Name = "btn_update";
            this.btn_update.Size = new System.Drawing.Size(100, 23);
            this.btn_update.TabIndex = 6;
            this.btn_update.Text = "Update";
            this.btn_update.UseVisualStyleBackColor = true;
            this.btn_update.Click += new System.EventHandler(this.btn_update_Click);
            // 
            // dtp_transdate
            // 
            this.dtp_transdate.Location = new System.Drawing.Point(385, 75);
            this.dtp_transdate.Name = "dtp_transdate";
            this.dtp_transdate.Size = new System.Drawing.Size(155, 21);
            this.dtp_transdate.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(565, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Status:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(296, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Trans. Date :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(5, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Voucher No:";
            // 
            // txt_voucher_no
            // 
            this.txt_voucher_no.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_voucher_no.Enabled = false;
            this.txt_voucher_no.Location = new System.Drawing.Point(84, 38);
            this.txt_voucher_no.Name = "txt_voucher_no";
            this.txt_voucher_no.Size = new System.Drawing.Size(186, 21);
            this.txt_voucher_no.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(5, 18);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(303, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "* Double click on cheque details for update cheque.";
            // 
            // Cheque_Report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1091, 641);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Cheque_Report";
            this.Text = "Cheque_Report";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Cheque_Report_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.gb_date.ResumeLayout(false);
            this.gb_sort.ResumeLayout(false);
            this.gb_sort.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_cheque)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox ch_Party;
        private System.Windows.Forms.CheckBox ch_Bank;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtp_to;
        private System.Windows.Forms.DateTimePicker dtp_from;
        private System.Windows.Forms.Button btn_view;
        private System.Windows.Forms.ComboBox cmb_party;
        private System.Windows.Forms.ComboBox cmb_bank;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgv_cheque;
        private System.Windows.Forms.GroupBox gb_date;
        private System.Windows.Forms.GroupBox gb_sort;
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
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton rbtn_sort;
        private System.Windows.Forms.RadioButton rbtn_all;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn slno;
        private System.Windows.Forms.DataGridViewTextBoxColumn Voucher_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn PARTY;
        private System.Windows.Forms.DataGridViewTextBoxColumn T_DATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn CHEQUE_DATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn STATUS;
        private System.Windows.Forms.DataGridViewTextBoxColumn CHQ_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn BANK;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOTES;
        private System.Windows.Forms.DataGridViewTextBoxColumn TRANSACTION_DATE;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btn_update;
        private System.Windows.Forms.ComboBox cmb_status;
        private System.Windows.Forms.DateTimePicker dtp_transdate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_voucher_no;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_chqno;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_amount;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton rb_reciept;
        private System.Windows.Forms.RadioButton rb_voucher;
        private System.Windows.Forms.Button btn_Delete;
        private System.Windows.Forms.Label label8;
    }
}