namespace Sys_Sols_Inventory.Accounts
{
    partial class Post_Dated_Checks
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Post_Dated_Checks));
            this.pnlacct = new System.Windows.Forms.Panel();
            this.TXT_BANK = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel7 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.AMOUNT = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel6 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel28 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.CASHACC = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.kptn = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.TXT_CHECKNO = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.PARTYACC = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.DTP_CHECKDATE = new System.Windows.Forms.DateTimePicker();
            this.kryptonLabel5 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.btnDelete = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnClear = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnSave = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnExit = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.NOTES = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel11 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.dgDetail = new System.Windows.Forms.DataGridView();
            this.cInvNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cDateGRE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cDateHIJ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BankName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CheckNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Creditor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Debitor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PayAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Narration = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DOC_DATE_HIJ = new ComponentFactory.Krypton.Toolkit.KryptonMaskedTextBox();
            this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.DOC_DATE_GRE = new System.Windows.Forms.DateTimePicker();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.btnDoc = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.DOC_NO = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.pnlacct.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CASHACC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PARTYACC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlacct
            // 
            this.pnlacct.Controls.Add(this.TXT_BANK);
            this.pnlacct.Controls.Add(this.kryptonLabel7);
            this.pnlacct.Controls.Add(this.AMOUNT);
            this.pnlacct.Controls.Add(this.kryptonLabel6);
            this.pnlacct.Controls.Add(this.kryptonLabel28);
            this.pnlacct.Controls.Add(this.CASHACC);
            this.pnlacct.Controls.Add(this.kptn);
            this.pnlacct.Controls.Add(this.TXT_CHECKNO);
            this.pnlacct.Controls.Add(this.kryptonLabel2);
            this.pnlacct.Controls.Add(this.PARTYACC);
            this.pnlacct.Controls.Add(this.DTP_CHECKDATE);
            this.pnlacct.Controls.Add(this.kryptonLabel5);
            this.pnlacct.Location = new System.Drawing.Point(17, 40);
            this.pnlacct.Name = "pnlacct";
            this.pnlacct.Size = new System.Drawing.Size(766, 72);
            this.pnlacct.TabIndex = 174;
            // 
            // TXT_BANK
            // 
            this.TXT_BANK.Location = new System.Drawing.Point(638, 9);
            this.TXT_BANK.Name = "TXT_BANK";
            this.TXT_BANK.Size = new System.Drawing.Size(113, 20);
            this.TXT_BANK.TabIndex = 168;
            // 
            // kryptonLabel7
            // 
            this.kryptonLabel7.Location = new System.Drawing.Point(556, 9);
            this.kryptonLabel7.Name = "kryptonLabel7";
            this.kryptonLabel7.Size = new System.Drawing.Size(79, 20);
            this.kryptonLabel7.TabIndex = 167;
            this.kryptonLabel7.Values.Text = "Bank Name :";
            // 
            // AMOUNT
            // 
            this.AMOUNT.Location = new System.Drawing.Point(638, 33);
            this.AMOUNT.Name = "AMOUNT";
            this.AMOUNT.Size = new System.Drawing.Size(113, 20);
            this.AMOUNT.TabIndex = 168;
            this.AMOUNT.Text = "0";
            // 
            // kryptonLabel6
            // 
            this.kryptonLabel6.Location = new System.Drawing.Point(556, 34);
            this.kryptonLabel6.Name = "kryptonLabel6";
            this.kryptonLabel6.Size = new System.Drawing.Size(76, 20);
            this.kryptonLabel6.TabIndex = 167;
            this.kryptonLabel6.Values.Text = "Check Amt :";
            // 
            // kryptonLabel28
            // 
            this.kryptonLabel28.Location = new System.Drawing.Point(313, 8);
            this.kryptonLabel28.Name = "kryptonLabel28";
            this.kryptonLabel28.Size = new System.Drawing.Size(72, 20);
            this.kryptonLabel28.TabIndex = 106;
            this.kryptonLabel28.Values.Text = "Party/ Acc :";
            // 
            // CASHACC
            // 
            this.CASHACC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CASHACC.DropDownWidth = 114;
            this.CASHACC.Location = new System.Drawing.Point(103, 8);
            this.CASHACC.Name = "CASHACC";
            this.CASHACC.Size = new System.Drawing.Size(190, 21);
            this.CASHACC.TabIndex = 107;
            // 
            // kptn
            // 
            this.kptn.Location = new System.Drawing.Point(1, 8);
            this.kptn.Name = "kptn";
            this.kptn.Size = new System.Drawing.Size(99, 20);
            this.kptn.TabIndex = 104;
            this.kptn.Values.Text = "Bank/ Cash A/c :";
            // 
            // TXT_CHECKNO
            // 
            this.TXT_CHECKNO.Location = new System.Drawing.Point(103, 34);
            this.TXT_CHECKNO.Name = "TXT_CHECKNO";
            this.TXT_CHECKNO.Size = new System.Drawing.Size(190, 20);
            this.TXT_CHECKNO.TabIndex = 166;
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(31, 34);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(69, 20);
            this.kryptonLabel2.TabIndex = 165;
            this.kryptonLabel2.Values.Text = "Check No :";
            // 
            // PARTYACC
            // 
            this.PARTYACC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PARTYACC.DropDownWidth = 114;
            this.PARTYACC.Location = new System.Drawing.Point(394, 7);
            this.PARTYACC.Name = "PARTYACC";
            this.PARTYACC.Size = new System.Drawing.Size(153, 21);
            this.PARTYACC.TabIndex = 105;
            // 
            // DTP_CHECKDATE
            // 
            this.DTP_CHECKDATE.CustomFormat = "dd/MM/yyyy";
            this.DTP_CHECKDATE.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTP_CHECKDATE.Location = new System.Drawing.Point(394, 34);
            this.DTP_CHECKDATE.Name = "DTP_CHECKDATE";
            this.DTP_CHECKDATE.Size = new System.Drawing.Size(153, 20);
            this.DTP_CHECKDATE.TabIndex = 157;
            // 
            // kryptonLabel5
            // 
            this.kryptonLabel5.Location = new System.Drawing.Point(309, 34);
            this.kryptonLabel5.Name = "kryptonLabel5";
            this.kryptonLabel5.Size = new System.Drawing.Size(79, 20);
            this.kryptonLabel5.TabIndex = 156;
            this.kryptonLabel5.Values.Text = "Check Date :";
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(604, 403);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(90, 25);
            this.btnDelete.TabIndex = 168;
            this.btnDelete.Values.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(508, 403);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(90, 25);
            this.btnClear.TabIndex = 167;
            this.btnClear.Values.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(411, 403);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 25);
            this.btnSave.TabIndex = 164;
            this.btnSave.Values.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(700, 403);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(90, 25);
            this.btnExit.TabIndex = 163;
            this.btnExit.Values.Text = "Close";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // NOTES
            // 
            this.NOTES.Location = new System.Drawing.Point(91, 362);
            this.NOTES.Multiline = true;
            this.NOTES.Name = "NOTES";
            this.NOTES.Size = new System.Drawing.Size(271, 32);
            this.NOTES.TabIndex = 162;
            // 
            // kryptonLabel11
            // 
            this.kryptonLabel11.Location = new System.Drawing.Point(18, 370);
            this.kryptonLabel11.Name = "kryptonLabel11";
            this.kryptonLabel11.Size = new System.Drawing.Size(63, 20);
            this.kryptonLabel11.TabIndex = 161;
            this.kryptonLabel11.Values.Text = "Remarks :";
            // 
            // dgDetail
            // 
            this.dgDetail.AllowUserToAddRows = false;
            this.dgDetail.AllowUserToDeleteRows = false;
            this.dgDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgDetail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cInvNo,
            this.cDateGRE,
            this.cDateHIJ,
            this.BankName,
            this.CheckNo,
            this.Creditor,
            this.Debitor,
            this.PayAmount,
            this.Narration});
            this.dgDetail.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgDetail.Location = new System.Drawing.Point(17, 118);
            this.dgDetail.Name = "dgDetail";
            this.dgDetail.RowHeadersVisible = false;
            this.dgDetail.Size = new System.Drawing.Size(766, 238);
            this.dgDetail.TabIndex = 160;
            // 
            // cInvNo
            // 
            this.cInvNo.HeaderText = "DOC NO";
            this.cInvNo.Name = "cInvNo";
            this.cInvNo.ReadOnly = true;
            this.cInvNo.Width = 150;
            // 
            // cDateGRE
            // 
            this.cDateGRE.HeaderText = "Date [GRE]";
            this.cDateGRE.Name = "cDateGRE";
            this.cDateGRE.ReadOnly = true;
            this.cDateGRE.Width = 120;
            // 
            // cDateHIJ
            // 
            this.cDateHIJ.HeaderText = "Date [HIJ]";
            this.cDateHIJ.Name = "cDateHIJ";
            this.cDateHIJ.ReadOnly = true;
            // 
            // BankName
            // 
            this.BankName.HeaderText = "Bank Name";
            this.BankName.Name = "BankName";
            // 
            // CheckNo
            // 
            this.CheckNo.HeaderText = "Check No";
            this.CheckNo.Name = "CheckNo";
            // 
            // Creditor
            // 
            this.Creditor.HeaderText = "Creditor ACC";
            this.Creditor.Name = "Creditor";
            this.Creditor.ReadOnly = true;
            // 
            // Debitor
            // 
            this.Debitor.HeaderText = "Debitor ACC";
            this.Debitor.Name = "Debitor";
            this.Debitor.ReadOnly = true;
            // 
            // PayAmount
            // 
            this.PayAmount.HeaderText = "Amount";
            this.PayAmount.Name = "PayAmount";
            // 
            // Narration
            // 
            this.Narration.HeaderText = "Notes";
            this.Narration.Name = "Narration";
            // 
            // DOC_DATE_HIJ
            // 
            this.DOC_DATE_HIJ.Location = new System.Drawing.Point(645, 11);
            this.DOC_DATE_HIJ.Mask = "00/00/0000";
            this.DOC_DATE_HIJ.Name = "DOC_DATE_HIJ";
            this.DOC_DATE_HIJ.Size = new System.Drawing.Size(123, 20);
            this.DOC_DATE_HIJ.TabIndex = 159;
            this.DOC_DATE_HIJ.Text = "  -  -";
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(573, 11);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(69, 20);
            this.kryptonLabel4.TabIndex = 158;
            this.kryptonLabel4.Values.Text = "Date [HIJ] :";
            // 
            // DOC_DATE_GRE
            // 
            this.DOC_DATE_GRE.CustomFormat = "dd/MM/yyyy";
            this.DOC_DATE_GRE.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DOC_DATE_GRE.Location = new System.Drawing.Point(413, 11);
            this.DOC_DATE_GRE.Name = "DOC_DATE_GRE";
            this.DOC_DATE_GRE.Size = new System.Drawing.Size(153, 20);
            this.DOC_DATE_GRE.TabIndex = 157;
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(327, 11);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(75, 20);
            this.kryptonLabel3.TabIndex = 156;
            this.kryptonLabel3.Values.Text = "Date [GRE] :";
            // 
            // btnDoc
            // 
            this.btnDoc.Location = new System.Drawing.Point(291, 7);
            this.btnDoc.Name = "btnDoc";
            this.btnDoc.Size = new System.Drawing.Size(19, 26);
            this.btnDoc.TabIndex = 155;
            this.btnDoc.Values.Text = "...";
            this.btnDoc.Click += new System.EventHandler(this.btnDoc_Click);
            // 
            // DOC_NO
            // 
            this.DOC_NO.Location = new System.Drawing.Point(120, 11);
            this.DOC_NO.Name = "DOC_NO";
            this.DOC_NO.ReadOnly = true;
            this.DOC_NO.Size = new System.Drawing.Size(165, 20);
            this.DOC_NO.TabIndex = 154;
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(67, 14);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(49, 20);
            this.kryptonLabel1.TabIndex = 153;
            this.kryptonLabel1.Values.Text = "Doc # :";
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // Post_Dated_Checks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(791, 436);
            this.Controls.Add(this.pnlacct);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.NOTES);
            this.Controls.Add(this.kryptonLabel11);
            this.Controls.Add(this.dgDetail);
            this.Controls.Add(this.DOC_DATE_HIJ);
            this.Controls.Add(this.kryptonLabel4);
            this.Controls.Add(this.DOC_DATE_GRE);
            this.Controls.Add(this.kryptonLabel3);
            this.Controls.Add(this.btnDoc);
            this.Controls.Add(this.DOC_NO);
            this.Controls.Add(this.kryptonLabel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Post_Dated_Checks";
            this.Text = "Post Dated Checks";
            this.Load += new System.EventHandler(this.Post_Dated_Checks_Load);
            this.pnlacct.ResumeLayout(false);
            this.pnlacct.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CASHACC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PARTYACC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgDetail)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlacct;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel28;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox CASHACC;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kptn;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox PARTYACC;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnDelete;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnClear;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSave;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnExit;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox NOTES;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel11;
        private System.Windows.Forms.DataGridView dgDetail;
        private ComponentFactory.Krypton.Toolkit.KryptonMaskedTextBox DOC_DATE_HIJ;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private System.Windows.Forms.DateTimePicker DOC_DATE_GRE;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnDoc;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox DOC_NO;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private System.Windows.Forms.PrintDialog printDialog1;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox TXT_CHECKNO;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private System.Windows.Forms.DateTimePicker DTP_CHECKDATE;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel5;
        private System.Windows.Forms.DataGridViewTextBoxColumn cInvNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn cDateGRE;
        private System.Windows.Forms.DataGridViewTextBoxColumn cDateHIJ;
        private System.Windows.Forms.DataGridViewTextBoxColumn BankName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CheckNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Creditor;
        private System.Windows.Forms.DataGridViewTextBoxColumn Debitor;
        private System.Windows.Forms.DataGridViewTextBoxColumn PayAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Narration;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox AMOUNT;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel6;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox TXT_BANK;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel7;
    }
}