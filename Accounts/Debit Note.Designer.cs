namespace Sys_Sols_Inventory.Accounts
{
    partial class Debit_Note
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Debit_Note));
            this.partyacclbl = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.pnlacct = new System.Windows.Forms.Panel();
            this.txt_taxamt = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.btn_inv_pick = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.BALANCE = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel7 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.doc_reference = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel5 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.CASHACC = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.ch_tax = new System.Windows.Forms.CheckBox();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.cashacclbl = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.TAX = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.PARTYACC = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.AMOUNT = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel6 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.ChekPrint = new System.Windows.Forms.CheckBox();
            this.Noteonreciept = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel15 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.PrintPage = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.btnDelete = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnClear = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnSave = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnExit = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.NOTES = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel11 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.dgDetail = new System.Windows.Forms.DataGridView();
            this.DOC_DATE_HIJ = new ComponentFactory.Krypton.Toolkit.KryptonMaskedTextBox();
            this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.DOC_DATE_GRE = new System.Windows.Forms.DateTimePicker();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.btnDoc = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.DOC_NO = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel31 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txt_docid = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnlacct.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CASHACC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TAX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PARTYACC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PrintPage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgDetail)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // partyacclbl
            // 
            this.partyacclbl.Location = new System.Drawing.Point(7, 10);
            this.partyacclbl.Name = "partyacclbl";
            this.partyacclbl.Size = new System.Drawing.Size(95, 20);
            this.partyacclbl.TabIndex = 104;
            this.partyacclbl.Values.Text = "Cr Acc              :";
            // 
            // pnlacct
            // 
            this.pnlacct.Controls.Add(this.txt_taxamt);
            this.pnlacct.Controls.Add(this.btn_inv_pick);
            this.pnlacct.Controls.Add(this.BALANCE);
            this.pnlacct.Controls.Add(this.kryptonLabel7);
            this.pnlacct.Controls.Add(this.doc_reference);
            this.pnlacct.Controls.Add(this.kryptonLabel5);
            this.pnlacct.Controls.Add(this.CASHACC);
            this.pnlacct.Controls.Add(this.ch_tax);
            this.pnlacct.Controls.Add(this.kryptonLabel2);
            this.pnlacct.Controls.Add(this.cashacclbl);
            this.pnlacct.Controls.Add(this.TAX);
            this.pnlacct.Controls.Add(this.PARTYACC);
            this.pnlacct.Controls.Add(this.partyacclbl);
            this.pnlacct.Controls.Add(this.AMOUNT);
            this.pnlacct.Controls.Add(this.kryptonLabel6);
            this.pnlacct.Location = new System.Drawing.Point(17, 67);
            this.pnlacct.Name = "pnlacct";
            this.pnlacct.Size = new System.Drawing.Size(768, 101);
            this.pnlacct.TabIndex = 100;
            // 
            // txt_taxamt
            // 
            this.txt_taxamt.Location = new System.Drawing.Point(622, 35);
            this.txt_taxamt.Name = "txt_taxamt";
            this.txt_taxamt.Size = new System.Drawing.Size(127, 20);
            this.txt_taxamt.TabIndex = 138;
            this.txt_taxamt.Text = "0";
            // 
            // btn_inv_pick
            // 
            this.btn_inv_pick.Location = new System.Drawing.Point(728, 62);
            this.btn_inv_pick.Name = "btn_inv_pick";
            this.btn_inv_pick.Size = new System.Drawing.Size(21, 20);
            this.btn_inv_pick.TabIndex = 137;
            this.btn_inv_pick.Values.Text = "...";
            this.btn_inv_pick.Click += new System.EventHandler(this.btn_inv_pick_Click);
            // 
            // BALANCE
            // 
            this.BALANCE.Location = new System.Drawing.Point(108, 78);
            this.BALANCE.Name = "BALANCE";
            this.BALANCE.ReadOnly = true;
            this.BALANCE.Size = new System.Drawing.Size(237, 20);
            this.BALANCE.TabIndex = 7;
            this.BALANCE.KeyDown += new System.Windows.Forms.KeyEventHandler(this.kryptonTextBox1_KeyDown);
            // 
            // kryptonLabel7
            // 
            this.kryptonLabel7.Location = new System.Drawing.Point(11, 78);
            this.kryptonLabel7.Name = "kryptonLabel7";
            this.kryptonLabel7.Size = new System.Drawing.Size(92, 20);
            this.kryptonLabel7.TabIndex = 135;
            this.kryptonLabel7.Values.Text = "Balance           :";
            // 
            // doc_reference
            // 
            this.doc_reference.Location = new System.Drawing.Point(495, 62);
            this.doc_reference.Name = "doc_reference";
            this.doc_reference.ReadOnly = true;
            this.doc_reference.Size = new System.Drawing.Size(227, 20);
            this.doc_reference.TabIndex = 8;
            this.doc_reference.KeyDown += new System.Windows.Forms.KeyEventHandler(this.doc_reference_KeyDown);
            // 
            // kryptonLabel5
            // 
            this.kryptonLabel5.Location = new System.Drawing.Point(381, 62);
            this.kryptonLabel5.Name = "kryptonLabel5";
            this.kryptonLabel5.Size = new System.Drawing.Size(97, 20);
            this.kryptonLabel5.TabIndex = 135;
            this.kryptonLabel5.Values.Text = "Reference No   :";
            // 
            // CASHACC
            // 
            this.CASHACC.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.CASHACC.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.CASHACC.DropDownWidth = 114;
            this.CASHACC.Location = new System.Drawing.Point(108, 10);
            this.CASHACC.Name = "CASHACC";
            this.CASHACC.Size = new System.Drawing.Size(237, 21);
            this.CASHACC.TabIndex = 3;
            this.CASHACC.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CASHACC_KeyDown);
            // 
            // ch_tax
            // 
            this.ch_tax.AutoSize = true;
            this.ch_tax.Location = new System.Drawing.Point(108, 58);
            this.ch_tax.Name = "ch_tax";
            this.ch_tax.Size = new System.Drawing.Size(101, 17);
            this.ch_tax.TabIndex = 6;
            this.ch_tax.Text = "Tax Inclusive";
            this.ch_tax.UseVisualStyleBackColor = true;
            this.ch_tax.CheckedChanged += new System.EventHandler(this.ch_tax_CheckedChanged);
            this.ch_tax.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ch_tax_KeyDown);
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(381, 36);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(103, 20);
            this.kryptonLabel2.TabIndex = 106;
            this.kryptonLabel2.Values.Text = "Tax %                 :";
            // 
            // cashacclbl
            // 
            this.cashacclbl.Location = new System.Drawing.Point(381, 7);
            this.cashacclbl.Name = "cashacclbl";
            this.cashacclbl.Size = new System.Drawing.Size(103, 20);
            this.cashacclbl.TabIndex = 106;
            this.cashacclbl.Values.Text = "Dr Acc                :";
            // 
            // TAX
            // 
            this.TAX.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TAX.DropDownWidth = 114;
            this.TAX.Location = new System.Drawing.Point(495, 35);
            this.TAX.Name = "TAX";
            this.TAX.Size = new System.Drawing.Size(121, 21);
            this.TAX.TabIndex = 5;
            this.TAX.SelectedIndexChanged += new System.EventHandler(this.TAX_SelectedIndexChanged);
            this.TAX.KeyDown += new System.Windows.Forms.KeyEventHandler(this.kryptonComboBox1_KeyDown);
            // 
            // PARTYACC
            // 
            this.PARTYACC.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.PARTYACC.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.PARTYACC.DropDownWidth = 114;
            this.PARTYACC.Location = new System.Drawing.Point(495, 6);
            this.PARTYACC.Name = "PARTYACC";
            this.PARTYACC.Size = new System.Drawing.Size(254, 21);
            this.PARTYACC.TabIndex = 2;
            this.PARTYACC.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PARTYACC_KeyDown);
            // 
            // AMOUNT
            // 
            this.AMOUNT.Location = new System.Drawing.Point(108, 36);
            this.AMOUNT.Name = "AMOUNT";
            this.AMOUNT.Size = new System.Drawing.Size(237, 20);
            this.AMOUNT.TabIndex = 4;
            this.AMOUNT.Text = "0";
            this.AMOUNT.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AMOUNT_KeyDown);
            this.AMOUNT.Leave += new System.EventHandler(this.AMOUNT_Leave);
            // 
            // kryptonLabel6
            // 
            this.kryptonLabel6.Location = new System.Drawing.Point(7, 36);
            this.kryptonLabel6.Name = "kryptonLabel6";
            this.kryptonLabel6.Size = new System.Drawing.Size(94, 20);
            this.kryptonLabel6.TabIndex = 120;
            this.kryptonLabel6.Values.Text = "Payment Amt  :";
            // 
            // ChekPrint
            // 
            this.ChekPrint.AutoSize = true;
            this.ChekPrint.Checked = true;
            this.ChekPrint.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChekPrint.Location = new System.Drawing.Point(126, 256);
            this.ChekPrint.Name = "ChekPrint";
            this.ChekPrint.Size = new System.Drawing.Size(102, 17);
            this.ChekPrint.TabIndex = 11;
            this.ChekPrint.Text = "Print Voucher";
            this.ChekPrint.UseVisualStyleBackColor = true;
            this.ChekPrint.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ChekPrint_KeyDown);
            // 
            // Noteonreciept
            // 
            this.Noteonreciept.Location = new System.Drawing.Point(108, 12);
            this.Noteonreciept.Multiline = true;
            this.Noteonreciept.Name = "Noteonreciept";
            this.Noteonreciept.Size = new System.Drawing.Size(237, 52);
            this.Noteonreciept.TabIndex = 9;
            this.Noteonreciept.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Noteonreciept_KeyDown);
            // 
            // kryptonLabel15
            // 
            this.kryptonLabel15.Location = new System.Drawing.Point(7, 29);
            this.kryptonLabel15.Name = "kryptonLabel15";
            this.kryptonLabel15.Size = new System.Drawing.Size(102, 20);
            this.kryptonLabel15.TabIndex = 1;
            this.kryptonLabel15.Values.Text = "Note on Reciept:";
            // 
            // PrintPage
            // 
            this.PrintPage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PrintPage.DropDownWidth = 58;
            this.PrintPage.Items.AddRange(new object[] {
            "Small Size"});
            this.PrintPage.Location = new System.Drawing.Point(125, 275);
            this.PrintPage.Name = "PrintPage";
            this.PrintPage.Size = new System.Drawing.Size(241, 21);
            this.PrintPage.TabIndex = 12;
            this.PrintPage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PrintPage_KeyDown);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(599, 271);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(81, 25);
            this.btnDelete.TabIndex = 7;
            this.btnDelete.Values.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(512, 271);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(81, 25);
            this.btnClear.TabIndex = 6;
            this.btnClear.Values.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(425, 271);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(81, 25);
            this.btnSave.TabIndex = 13;
            this.btnSave.Values.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.btnSave.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnSave_KeyDown);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(686, 271);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(81, 25);
            this.btnExit.TabIndex = 8;
            this.btnExit.Values.Text = "Close";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // NOTES
            // 
            this.NOTES.Location = new System.Drawing.Point(495, 12);
            this.NOTES.Multiline = true;
            this.NOTES.Name = "NOTES";
            this.NOTES.Size = new System.Drawing.Size(254, 52);
            this.NOTES.TabIndex = 10;
            this.NOTES.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NOTES_KeyDown);
            // 
            // kryptonLabel11
            // 
            this.kryptonLabel11.Location = new System.Drawing.Point(381, 29);
            this.kryptonLabel11.Name = "kryptonLabel11";
            this.kryptonLabel11.Size = new System.Drawing.Size(100, 20);
            this.kryptonLabel11.TabIndex = 134;
            this.kryptonLabel11.Values.Text = "Remarks            :";
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // dgDetail
            // 
            this.dgDetail.AllowUserToAddRows = false;
            this.dgDetail.AllowUserToDeleteRows = false;
            this.dgDetail.BackgroundColor = System.Drawing.Color.White;
            this.dgDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgDetail.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgDetail.Location = new System.Drawing.Point(12, 258);
            this.dgDetail.Name = "dgDetail";
            this.dgDetail.RowHeadersVisible = false;
            this.dgDetail.Size = new System.Drawing.Size(10, 10);
            this.dgDetail.TabIndex = 133;
            this.dgDetail.Visible = false;
            // 
            // DOC_DATE_HIJ
            // 
            this.DOC_DATE_HIJ.Location = new System.Drawing.Point(596, 14);
            this.DOC_DATE_HIJ.Mask = "00/00/0000";
            this.DOC_DATE_HIJ.Name = "DOC_DATE_HIJ";
            this.DOC_DATE_HIJ.Size = new System.Drawing.Size(154, 20);
            this.DOC_DATE_HIJ.TabIndex = 116;
            this.DOC_DATE_HIJ.Text = "  -  -";
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(529, 14);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(69, 20);
            this.kryptonLabel4.TabIndex = 115;
            this.kryptonLabel4.Values.Text = "Date [HIJ] :";
            // 
            // DOC_DATE_GRE
            // 
            this.DOC_DATE_GRE.CustomFormat = "dd/MM/yyyy";
            this.DOC_DATE_GRE.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DOC_DATE_GRE.Location = new System.Drawing.Point(345, 13);
            this.DOC_DATE_GRE.Name = "DOC_DATE_GRE";
            this.DOC_DATE_GRE.Size = new System.Drawing.Size(180, 21);
            this.DOC_DATE_GRE.TabIndex = 1;
            this.DOC_DATE_GRE.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DOC_DATE_GRE_KeyDown);
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(267, 14);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(82, 20);
            this.kryptonLabel3.TabIndex = 113;
            this.kryptonLabel3.Values.Text = "Date [GRE]   :";
            // 
            // btnDoc
            // 
            this.btnDoc.Location = new System.Drawing.Point(246, 14);
            this.btnDoc.Name = "btnDoc";
            this.btnDoc.Size = new System.Drawing.Size(21, 20);
            this.btnDoc.TabIndex = 108;
            this.btnDoc.Values.Text = "...";
            this.btnDoc.Click += new System.EventHandler(this.btnDoc_Click);
            // 
            // DOC_NO
            // 
            this.DOC_NO.Location = new System.Drawing.Point(142, 14);
            this.DOC_NO.Name = "DOC_NO";
            this.DOC_NO.ReadOnly = true;
            this.DOC_NO.Size = new System.Drawing.Size(100, 20);
            this.DOC_NO.TabIndex = 0;
            this.DOC_NO.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DOC_NO_KeyDown);
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(7, 14);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(81, 20);
            this.kryptonLabel1.TabIndex = 106;
            this.kryptonLabel1.Values.Text = "Doc # Id/No:";
            this.kryptonLabel1.Paint += new System.Windows.Forms.PaintEventHandler(this.kryptonLabel1_Paint);
            // 
            // kryptonLabel31
            // 
            this.kryptonLabel31.Location = new System.Drawing.Point(29, 276);
            this.kryptonLabel31.Name = "kryptonLabel31";
            this.kryptonLabel31.Size = new System.Drawing.Size(99, 20);
            this.kryptonLabel31.TabIndex = 146;
            this.kryptonLabel31.Values.Text = "Print Page .       :";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnDoc);
            this.panel1.Controls.Add(this.DOC_DATE_HIJ);
            this.panel1.Controls.Add(this.kryptonLabel4);
            this.panel1.Controls.Add(this.DOC_DATE_GRE);
            this.panel1.Controls.Add(this.kryptonLabel3);
            this.panel1.Controls.Add(this.txt_docid);
            this.panel1.Controls.Add(this.DOC_NO);
            this.panel1.Controls.Add(this.kryptonLabel1);
            this.panel1.Location = new System.Drawing.Point(17, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(768, 49);
            this.panel1.TabIndex = 100;
            // 
            // txt_docid
            // 
            this.txt_docid.Location = new System.Drawing.Point(84, 14);
            this.txt_docid.Name = "txt_docid";
            this.txt_docid.ReadOnly = true;
            this.txt_docid.Size = new System.Drawing.Size(52, 20);
            this.txt_docid.TabIndex = 0;
            this.txt_docid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DOC_NO_KeyDown);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.Noteonreciept);
            this.panel2.Controls.Add(this.NOTES);
            this.panel2.Controls.Add(this.kryptonLabel11);
            this.panel2.Controls.Add(this.kryptonLabel15);
            this.panel2.Location = new System.Drawing.Point(17, 174);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(768, 78);
            this.panel2.TabIndex = 2;
            // 
            // Debit_Note
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(801, 316);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlacct);
            this.Controls.Add(this.ChekPrint);
            this.Controls.Add(this.PrintPage);
            this.Controls.Add(this.kryptonLabel31);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.dgDetail);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnExit);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Debit_Note";
            this.Load += new System.EventHandler(this.Debit_Note_Load);
            this.pnlacct.ResumeLayout(false);
            this.pnlacct.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CASHACC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TAX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PARTYACC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PrintPage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgDetail)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonLabel partyacclbl;
        private System.Windows.Forms.Panel pnlacct;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel cashacclbl;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox CASHACC;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox PARTYACC;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox AMOUNT;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel6;
        private System.Windows.Forms.CheckBox ChekPrint;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox Noteonreciept;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel15;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox PrintPage;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnDelete;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnClear;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSave;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnExit;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox NOTES;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel11;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.DataGridView dgDetail;
        private ComponentFactory.Krypton.Toolkit.KryptonMaskedTextBox DOC_DATE_HIJ;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private System.Windows.Forms.DateTimePicker DOC_DATE_GRE;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnDoc;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox DOC_NO;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel31;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox TAX;
        private System.Windows.Forms.CheckBox ch_tax;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btn_inv_pick;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox doc_reference;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel5;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox BALANCE;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel7;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txt_taxamt;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txt_docid;
    }
}