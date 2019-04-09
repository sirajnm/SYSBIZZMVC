namespace Sys_Sols_Inventory
{
    partial class CustomerStatement
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
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomerStatement));
            this.CUS_STATBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.AIN_INVENTORYDataSet2 = new Sys_Sols_Inventory.AIN_INVENTORYDataSet2();
            this.dgDetail = new System.Windows.Forms.DataGridView();
            this.cDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cDocNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cDocType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cRef = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cDebit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cCredit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cBalance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.SUP_CODE = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.btnSup = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.SUP_NAME = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.DATE_FROM = new System.Windows.Forms.DateTimePicker();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.DATE_TO = new System.Windows.Forms.DateTimePicker();
            this.btnRun = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Chk = new System.Windows.Forms.CheckBox();
            this.grpDate = new System.Windows.Forms.GroupBox();
            this.CUS_STATTableAdapter = new Sys_Sols_Inventory.AIN_INVENTORYDataSet2TableAdapters.CUS_STATTableAdapter();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.accname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lastpaydate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lastpayamount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.debit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.credit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.balance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnprint = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.kryptonButton1 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.CUS_STATBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AIN_INVENTORYDataSet2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgDetail)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.grpDate.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // CUS_STATBindingSource
            // 
            this.CUS_STATBindingSource.DataMember = "CUS_STAT";
            this.CUS_STATBindingSource.DataSource = this.AIN_INVENTORYDataSet2;
            // 
            // AIN_INVENTORYDataSet2
            // 
            this.AIN_INVENTORYDataSet2.DataSetName = "AIN_INVENTORYDataSet2";
            this.AIN_INVENTORYDataSet2.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dgDetail
            // 
            this.dgDetail.AllowUserToAddRows = false;
            this.dgDetail.AllowUserToDeleteRows = false;
            this.dgDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgDetail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cDate,
            this.cDocNo,
            this.cDocType,
            this.cRef,
            this.cDesc,
            this.cDebit,
            this.cCredit,
            this.cBalance});
            this.dgDetail.Location = new System.Drawing.Point(683, 14);
            this.dgDetail.Name = "dgDetail";
            this.dgDetail.ReadOnly = true;
            this.dgDetail.RowHeadersVisible = false;
            this.dgDetail.Size = new System.Drawing.Size(66, 48);
            this.dgDetail.TabIndex = 18;
            this.dgDetail.Visible = false;
            // 
            // cDate
            // 
            this.cDate.HeaderText = "Date";
            this.cDate.Name = "cDate";
            this.cDate.ReadOnly = true;
            // 
            // cDocNo
            // 
            this.cDocNo.HeaderText = "Doc. No.";
            this.cDocNo.Name = "cDocNo";
            this.cDocNo.ReadOnly = true;
            // 
            // cDocType
            // 
            this.cDocType.HeaderText = "Doc. Type";
            this.cDocType.Name = "cDocType";
            this.cDocType.ReadOnly = true;
            // 
            // cRef
            // 
            this.cRef.HeaderText = "Ref.";
            this.cRef.Name = "cRef";
            this.cRef.ReadOnly = true;
            // 
            // cDesc
            // 
            this.cDesc.HeaderText = "Desc.";
            this.cDesc.Name = "cDesc";
            this.cDesc.ReadOnly = true;
            // 
            // cDebit
            // 
            this.cDebit.HeaderText = "Debit";
            this.cDebit.Name = "cDebit";
            this.cDebit.ReadOnly = true;
            // 
            // cCredit
            // 
            this.cCredit.HeaderText = "Credit";
            this.cCredit.Name = "cCredit";
            this.cCredit.ReadOnly = true;
            // 
            // cBalance
            // 
            this.cBalance.HeaderText = "Balance";
            this.cBalance.Name = "cBalance";
            this.cBalance.ReadOnly = true;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSetCustomerStatement";
            reportDataSource1.Value = this.CUS_STATBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Sys_Sols_Inventory.Report3.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(949, 345);
            this.reportViewer1.TabIndex = 20;
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(13, 15);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(98, 20);
            this.kryptonLabel1.TabIndex = 10;
            this.kryptonLabel1.Values.Text = "Customer Code:";
            // 
            // SUP_CODE
            // 
            this.SUP_CODE.Location = new System.Drawing.Point(107, 15);
            this.SUP_CODE.Name = "SUP_CODE";
            this.SUP_CODE.Size = new System.Drawing.Size(137, 20);
            this.SUP_CODE.TabIndex = 0;
            this.SUP_CODE.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SUP_CODE_KeyDown);
            // 
            // btnSup
            // 
            this.btnSup.Location = new System.Drawing.Point(247, 12);
            this.btnSup.Name = "btnSup";
            this.btnSup.Size = new System.Drawing.Size(30, 25);
            this.btnSup.TabIndex = 1;
            this.btnSup.Values.Text = ">>";
            this.btnSup.Click += new System.EventHandler(this.btnSup_Click);
            // 
            // SUP_NAME
            // 
            this.SUP_NAME.Location = new System.Drawing.Point(279, 15);
            this.SUP_NAME.Name = "SUP_NAME";
            this.SUP_NAME.Size = new System.Drawing.Size(179, 20);
            this.SUP_NAME.TabIndex = 2;
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(6, 27);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(70, 20);
            this.kryptonLabel2.TabIndex = 14;
            this.kryptonLabel2.Values.Text = "Date From:";
            // 
            // DATE_FROM
            // 
            this.DATE_FROM.CustomFormat = "dd/MM/yyyy";
            this.DATE_FROM.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DATE_FROM.Location = new System.Drawing.Point(87, 27);
            this.DATE_FROM.Name = "DATE_FROM";
            this.DATE_FROM.Size = new System.Drawing.Size(98, 20);
            this.DATE_FROM.TabIndex = 0;
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(191, 26);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(56, 20);
            this.kryptonLabel3.TabIndex = 16;
            this.kryptonLabel3.Values.Text = "Date To:";
            // 
            // DATE_TO
            // 
            this.DATE_TO.CustomFormat = "dd/MM/yyyy";
            this.DATE_TO.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DATE_TO.Location = new System.Drawing.Point(266, 27);
            this.DATE_TO.Name = "DATE_TO";
            this.DATE_TO.Size = new System.Drawing.Size(98, 20);
            this.DATE_TO.TabIndex = 1;
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(403, 78);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(90, 25);
            this.btnRun.TabIndex = 0;
            this.btnRun.Values.Text = "Run";
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel1.Controls.Add(this.reportViewer1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(949, 345);
            this.panel1.TabIndex = 21;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnSup);
            this.panel2.Controls.Add(this.SUP_NAME);
            this.panel2.Controls.Add(this.Chk);
            this.panel2.Controls.Add(this.btnRun);
            this.panel2.Controls.Add(this.dgDetail);
            this.panel2.Controls.Add(this.grpDate);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(963, 120);
            this.panel2.TabIndex = 22;
            // 
            // Chk
            // 
            this.Chk.AutoSize = true;
            this.Chk.Location = new System.Drawing.Point(38, 40);
            this.Chk.Name = "Chk";
            this.Chk.Size = new System.Drawing.Size(99, 17);
            this.Chk.TabIndex = 17;
            this.Chk.Text = "Filter With Date";
            this.Chk.UseVisualStyleBackColor = true;
            this.Chk.CheckedChanged += new System.EventHandler(this.Chk_CheckedChanged);
            // 
            // grpDate
            // 
            this.grpDate.Controls.Add(this.DATE_TO);
            this.grpDate.Controls.Add(this.kryptonLabel2);
            this.grpDate.Controls.Add(this.kryptonLabel3);
            this.grpDate.Controls.Add(this.DATE_FROM);
            this.grpDate.Enabled = false;
            this.grpDate.Location = new System.Drawing.Point(30, 49);
            this.grpDate.Name = "grpDate";
            this.grpDate.Size = new System.Drawing.Size(369, 58);
            this.grpDate.TabIndex = 20;
            this.grpDate.TabStop = false;
            // 
            // CUS_STATTableAdapter
            // 
            this.CUS_STATTableAdapter.ClearBeforeFill = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 120);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(963, 377);
            this.tabControl1.TabIndex = 23;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Controls.Add(this.panel3);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(955, 351);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.accname,
            this.lastpaydate,
            this.lastpayamount,
            this.debit,
            this.credit,
            this.balance});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 51);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(949, 297);
            this.dataGridView1.TabIndex = 180;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // accname
            // 
            this.accname.FillWeight = 150F;
            this.accname.HeaderText = "ACCNAME";
            this.accname.Name = "accname";
            this.accname.Width = 150;
            // 
            // lastpaydate
            // 
            this.lastpaydate.HeaderText = "LAST PAY DATE";
            this.lastpaydate.Name = "lastpaydate";
            // 
            // lastpayamount
            // 
            this.lastpayamount.HeaderText = "LAST PAY AMOUNT";
            this.lastpayamount.Name = "lastpayamount";
            // 
            // debit
            // 
            this.debit.FillWeight = 150F;
            this.debit.HeaderText = "DEBIT";
            this.debit.Name = "debit";
            this.debit.Width = 150;
            // 
            // credit
            // 
            this.credit.FillWeight = 150F;
            this.credit.HeaderText = "CREDIT(With Opening Balance)";
            this.credit.Name = "credit";
            this.credit.Width = 150;
            // 
            // balance
            // 
            this.balance.FillWeight = 150F;
            this.balance.HeaderText = "BALANCE";
            this.balance.Name = "balance";
            this.balance.Width = 150;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.kryptonButton1);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.textBox1);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.btnprint);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(949, 48);
            this.panel3.TabIndex = 179;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 173;
            this.label1.Text = "Customer Summary";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(184, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(124, 20);
            this.label3.TabIndex = 174;
            this.label3.Text = "Total Balance:";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(314, 9);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(189, 26);
            this.textBox1.TabIndex = 177;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(319, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 20);
            this.label4.TabIndex = 175;
            this.label4.Text = "label4";
            this.label4.Visible = false;
            // 
            // btnprint
            // 
            this.btnprint.Location = new System.Drawing.Point(509, 9);
            this.btnprint.Name = "btnprint";
            this.btnprint.Size = new System.Drawing.Size(115, 26);
            this.btnprint.TabIndex = 176;
            this.btnprint.Values.Text = "Print";
            this.btnprint.Click += new System.EventHandler(this.btnprint_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(955, 351);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.Location = new System.Drawing.Point(627, 9);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.Size = new System.Drawing.Size(115, 26);
            this.kryptonButton1.TabIndex = 178;
            this.kryptonButton1.Values.Text = "Refresh";
            this.kryptonButton1.Click += new System.EventHandler(this.kryptonButton1_Click);
            // 
            // CustomerStatement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(963, 497);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.SUP_CODE);
            this.Controls.Add(this.kryptonLabel1);
            this.Controls.Add(this.panel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CustomerStatement";
            this.Text = "Customer Statement";
            this.Load += new System.EventHandler(this.CustomerStatement_Load);
            ((System.ComponentModel.ISupportInitialize)(this.CUS_STATBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AIN_INVENTORYDataSet2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgDetail)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.grpDate.ResumeLayout(false);
            this.grpDate.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgDetail;
        private System.Windows.Forms.DataGridViewTextBoxColumn cDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn cDocNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn cDocType;
        private System.Windows.Forms.DataGridViewTextBoxColumn cRef;
        private System.Windows.Forms.DataGridViewTextBoxColumn cDesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn cDebit;
        private System.Windows.Forms.DataGridViewTextBoxColumn cCredit;
        private System.Windows.Forms.DataGridViewTextBoxColumn cBalance;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource CUS_STATBindingSource;
        private AIN_INVENTORYDataSet2 AIN_INVENTORYDataSet2;
        private AIN_INVENTORYDataSet2TableAdapters.CUS_STATTableAdapter CUS_STATTableAdapter;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox SUP_CODE;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSup;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox SUP_NAME;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private System.Windows.Forms.DateTimePicker DATE_FROM;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private System.Windows.Forms.DateTimePicker DATE_TO;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnRun;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox grpDate;
        private System.Windows.Forms.CheckBox Chk;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label4;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnprint;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn accname;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastpaydate;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastpayamount;
        private System.Windows.Forms.DataGridViewTextBoxColumn debit;
        private System.Windows.Forms.DataGridViewTextBoxColumn credit;
        private System.Windows.Forms.DataGridViewTextBoxColumn balance;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton1;
    }
}