namespace Sys_Sols_Inventory.Accounts
{
    partial class Trail_Balance
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Trail_Balance));
            this.panel1 = new System.Windows.Forms.Panel();
            this.kryptonLabel5 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.UNDER = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.Date_To = new System.Windows.Forms.DateTimePicker();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.Date_From = new System.Windows.Forms.DateTimePicker();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.btnSave = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnClear = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.panel4 = new System.Windows.Forms.Panel();
            this.PicPdf = new System.Windows.Forms.PictureBox();
            this.PicExcel = new System.Windows.Forms.PictureBox();
            this.PicPrint = new System.Windows.Forms.PictureBox();
            this.lbltitle = new System.Windows.Forms.Label();
            this.dgData = new System.Windows.Forms.DataGridView();
            this.ACCID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ACCNAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Debit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Credit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aiN_INVENTORYDataSet1 = new Sys_Sols_Inventory.AIN_INVENTORYDataSet();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UNDER)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicPdf)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicExcel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicPrint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.aiN_INVENTORYDataSet1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.kryptonLabel5);
            this.panel1.Controls.Add(this.UNDER);
            this.panel1.Controls.Add(this.Date_To);
            this.panel1.Controls.Add(this.kryptonLabel1);
            this.panel1.Controls.Add(this.Date_From);
            this.panel1.Controls.Add(this.kryptonLabel3);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.btnClear);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1017, 64);
            this.panel1.TabIndex = 2;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // kryptonLabel5
            // 
            this.kryptonLabel5.Location = new System.Drawing.Point(457, 10);
            this.kryptonLabel5.Name = "kryptonLabel5";
            this.kryptonLabel5.Size = new System.Drawing.Size(47, 20);
            this.kryptonLabel5.TabIndex = 75;
            this.kryptonLabel5.Values.Text = "Under:";
            // 
            // UNDER
            // 
            this.UNDER.AllowDrop = true;
            this.UNDER.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.UNDER.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.UNDER.DropDownWidth = 211;
            this.UNDER.Location = new System.Drawing.Point(505, 9);
            this.UNDER.Name = "UNDER";
            this.UNDER.Size = new System.Drawing.Size(193, 21);
            this.UNDER.TabIndex = 74;
            // 
            // Date_To
            // 
            this.Date_To.CustomFormat = "dd/MM/yyyy";
            this.Date_To.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Date_To.Location = new System.Drawing.Point(258, 10);
            this.Date_To.Name = "Date_To";
            this.Date_To.Size = new System.Drawing.Size(176, 20);
            this.Date_To.TabIndex = 73;
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(229, 10);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(30, 20);
            this.kryptonLabel1.TabIndex = 72;
            this.kryptonLabel1.Values.Text = "To :";
            // 
            // Date_From
            // 
            this.Date_From.CustomFormat = "dd/MM/yyyy";
            this.Date_From.Enabled = false;
            this.Date_From.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Date_From.Location = new System.Drawing.Point(54, 10);
            this.Date_From.Name = "Date_From";
            this.Date_From.Size = new System.Drawing.Size(176, 20);
            this.Date_From.TabIndex = 71;
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Enabled = false;
            this.kryptonLabel3.Location = new System.Drawing.Point(8, 10);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(45, 20);
            this.kryptonLabel3.TabIndex = 70;
            this.kryptonLabel3.Values.Text = "From :";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(505, 36);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 25);
            this.btnSave.TabIndex = 66;
            this.btnSave.Values.Text = "Search";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(608, 36);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(90, 25);
            this.btnClear.TabIndex = 67;
            this.btnClear.Values.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // printDocument1
            // 
            this.printDocument1.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.printDocument1_BeginPrint_1);
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.Controls.Add(this.PicPdf);
            this.panel4.Controls.Add(this.PicExcel);
            this.panel4.Controls.Add(this.PicPrint);
            this.panel4.Controls.Add(this.lbltitle);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1017, 40);
            this.panel4.TabIndex = 75;
            // 
            // PicPdf
            // 
            this.PicPdf.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PicPdf.Image = global::Sys_Sols_Inventory.Properties.Resources.pdf24;
            this.PicPdf.Location = new System.Drawing.Point(80, 7);
            this.PicPdf.Name = "PicPdf";
            this.PicPdf.Size = new System.Drawing.Size(28, 28);
            this.PicPdf.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PicPdf.TabIndex = 4;
            this.PicPdf.TabStop = false;
            this.PicPdf.Click += new System.EventHandler(this.PicPdf_Click);
            // 
            // PicExcel
            // 
            this.PicExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PicExcel.Image = global::Sys_Sols_Inventory.Properties.Resources.microsoft_excel;
            this.PicExcel.Location = new System.Drawing.Point(46, 6);
            this.PicExcel.Name = "PicExcel";
            this.PicExcel.Size = new System.Drawing.Size(28, 28);
            this.PicExcel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PicExcel.TabIndex = 3;
            this.PicExcel.TabStop = false;
            this.PicExcel.Click += new System.EventHandler(this.PicExcel_Click);
            // 
            // PicPrint
            // 
            this.PicPrint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PicPrint.Image = global::Sys_Sols_Inventory.Properties.Resources.paper6;
            this.PicPrint.Location = new System.Drawing.Point(12, 7);
            this.PicPrint.Name = "PicPrint";
            this.PicPrint.Size = new System.Drawing.Size(28, 28);
            this.PicPrint.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PicPrint.TabIndex = 2;
            this.PicPrint.TabStop = false;
            this.PicPrint.Click += new System.EventHandler(this.PicPrint_Click);
            // 
            // lbltitle
            // 
            this.lbltitle.AutoSize = true;
            this.lbltitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltitle.ForeColor = System.Drawing.Color.Red;
            this.lbltitle.Location = new System.Drawing.Point(270, 6);
            this.lbltitle.Name = "lbltitle";
            this.lbltitle.Size = new System.Drawing.Size(116, 22);
            this.lbltitle.TabIndex = 1;
            this.lbltitle.Text = "Trail Balance";
            // 
            // dgData
            // 
            this.dgData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgData.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ACCID,
            this.UN,
            this.ACCNAME,
            this.Debit,
            this.Credit});
            this.dgData.Location = new System.Drawing.Point(12, 46);
            this.dgData.Name = "dgData";
            this.dgData.Size = new System.Drawing.Size(967, 459);
            this.dgData.TabIndex = 76;
            this.dgData.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgData_CellClick);
            this.dgData.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgData_CellContentClick);
            this.dgData.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgData_CellDoubleClick_1);
            this.dgData.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgData_CellFormatting);
            this.dgData.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgData_CellPainting_1);
            // 
            // ACCID
            // 
            this.ACCID.HeaderText = "ACCID";
            this.ACCID.Name = "ACCID";
            this.ACCID.Visible = false;
            // 
            // UN
            // 
            this.UN.HeaderText = "UNDER";
            this.UN.Name = "UN";
            // 
            // ACCNAME
            // 
            this.ACCNAME.HeaderText = "Account Name";
            this.ACCNAME.Name = "ACCNAME";
            // 
            // Debit
            // 
            this.Debit.HeaderText = "Debit";
            this.Debit.Name = "Debit";
            // 
            // Credit
            // 
            this.Credit.HeaderText = "Credit";
            this.Credit.Name = "Credit";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgData);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 64);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1017, 542);
            this.panel2.TabIndex = 3;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "ACCID";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Account Name";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 175;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Debit";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 200;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Credit";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 150;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Credit";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Width = 150;
            // 
            // aiN_INVENTORYDataSet1
            // 
            this.aiN_INVENTORYDataSet1.DataSetName = "AIN_INVENTORYDataSet";
            this.aiN_INVENTORYDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // Trail_Balance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1017, 606);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Trail_Balance";
            this.Text = "Trail_Balance";
            this.Load += new System.EventHandler(this.Trail_Balance_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UNDER)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicPdf)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicExcel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicPrint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.aiN_INVENTORYDataSet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DateTimePicker Date_To;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private System.Windows.Forms.DateTimePicker Date_From;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSave;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnClear;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel5;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox UNDER;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.PictureBox PicPdf;
        private System.Windows.Forms.PictureBox PicExcel;
        private System.Windows.Forms.PictureBox PicPrint;
        private System.Windows.Forms.Label lbltitle;
        private System.Windows.Forms.DataGridView dgData;
        private System.Windows.Forms.Panel panel2;
        private AIN_INVENTORYDataSet aiN_INVENTORYDataSet1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ACCID;
        private System.Windows.Forms.DataGridViewTextBoxColumn UN;
        private System.Windows.Forms.DataGridViewTextBoxColumn ACCNAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn Debit;
        private System.Windows.Forms.DataGridViewTextBoxColumn Credit;
    }
}