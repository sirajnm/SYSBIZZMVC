namespace Sys_Sols_Inventory.Accounts
{
    partial class Day_Book
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Day_Book));
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmLedgers = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.DrpVoucherType = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.Date_To = new System.Windows.Forms.DateTimePicker();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.btnClear = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.Date_From = new System.Windows.Forms.DateTimePicker();
            this.btnSave = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ChkNarration = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.ChkVchrType = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.ChkVoucherNo = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.PicPdf = new System.Windows.Forms.PictureBox();
            this.PicExcel = new System.Windows.Forms.PictureBox();
            this.PicPrint = new System.Windows.Forms.PictureBox();
            this.lbltitle = new System.Windows.Forms.Label();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.UNDER = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.kryptonLabel5 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmLedgers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DrpVoucherType)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicPdf)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicExcel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicPrint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UNDER)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(20, 16);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(45, 20);
            this.kryptonLabel3.TabIndex = 20;
            this.kryptonLabel3.Values.Text = "From :";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.kryptonLabel5);
            this.panel1.Controls.Add(this.UNDER);
            this.panel1.Controls.Add(this.cmLedgers);
            this.panel1.Controls.Add(this.kryptonLabel4);
            this.panel1.Controls.Add(this.DrpVoucherType);
            this.panel1.Controls.Add(this.kryptonLabel2);
            this.panel1.Controls.Add(this.Date_To);
            this.panel1.Controls.Add(this.kryptonLabel1);
            this.panel1.Controls.Add(this.btnClear);
            this.panel1.Controls.Add(this.Date_From);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.kryptonLabel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(956, 98);
            this.panel1.TabIndex = 23;
            // 
            // cmLedgers
            // 
            this.cmLedgers.AllowDrop = true;
            this.cmLedgers.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmLedgers.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmLedgers.DropDownWidth = 211;
            this.cmLedgers.Location = new System.Drawing.Point(365, 18);
            this.cmLedgers.Name = "cmLedgers";
            this.cmLedgers.Size = new System.Drawing.Size(211, 21);
            this.cmLedgers.TabIndex = 71;
            this.cmLedgers.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmLedgers_KeyDown);
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(308, 17);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(54, 20);
            this.kryptonLabel4.TabIndex = 70;
            this.kryptonLabel4.Values.Text = "Ledger :";
            // 
            // DrpVoucherType
            // 
            this.DrpVoucherType.AllowDrop = true;
            this.DrpVoucherType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.DrpVoucherType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.DrpVoucherType.DropDownWidth = 211;
            this.DrpVoucherType.Location = new System.Drawing.Point(365, 45);
            this.DrpVoucherType.Name = "DrpVoucherType";
            this.DrpVoucherType.Size = new System.Drawing.Size(211, 21);
            this.DrpVoucherType.TabIndex = 65;
            this.DrpVoucherType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DrpVoucherType_KeyDown);
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(271, 45);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(97, 20);
            this.kryptonLabel2.TabIndex = 64;
            this.kryptonLabel2.Values.Text = "Voucher Types :";
            // 
            // Date_To
            // 
            this.Date_To.CustomFormat = "dd/MM/yyyy";
            this.Date_To.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Date_To.Location = new System.Drawing.Point(72, 42);
            this.Date_To.Name = "Date_To";
            this.Date_To.Size = new System.Drawing.Size(176, 20);
            this.Date_To.TabIndex = 63;
            this.Date_To.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Date_To_KeyDown);
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(20, 42);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(30, 20);
            this.kryptonLabel1.TabIndex = 62;
            this.kryptonLabel1.Values.Text = "To :";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(800, 45);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(90, 25);
            this.btnClear.TabIndex = 36;
            this.btnClear.Values.Text = "Clear";
            // 
            // Date_From
            // 
            this.Date_From.CustomFormat = "dd/MM/yyyy";
            this.Date_From.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Date_From.Location = new System.Drawing.Point(72, 16);
            this.Date_From.Name = "Date_From";
            this.Date_From.Size = new System.Drawing.Size(176, 20);
            this.Date_From.TabIndex = 61;
            this.Date_From.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Date_From_KeyDown);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(800, 16);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 25);
            this.btnSave.TabIndex = 35;
            this.btnSave.Values.Text = "Search";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ChkNarration);
            this.groupBox1.Controls.Add(this.ChkVchrType);
            this.groupBox1.Controls.Add(this.ChkVoucherNo);
            this.groupBox1.Location = new System.Drawing.Point(582, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(212, 78);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Show Items";
            // 
            // ChkNarration
            // 
            this.ChkNarration.Checked = true;
            this.ChkNarration.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkNarration.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.ChkNarration.Location = new System.Drawing.Point(20, 50);
            this.ChkNarration.Name = "ChkNarration";
            this.ChkNarration.Size = new System.Drawing.Size(75, 20);
            this.ChkNarration.TabIndex = 16;
            this.ChkNarration.Text = "Narration";
            this.ChkNarration.Values.Text = "Narration";
            // 
            // ChkVchrType
            // 
            this.ChkVchrType.Checked = true;
            this.ChkVchrType.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkVchrType.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.ChkVchrType.Location = new System.Drawing.Point(101, 50);
            this.ChkVchrType.Name = "ChkVchrType";
            this.ChkVchrType.Size = new System.Drawing.Size(103, 20);
            this.ChkVchrType.TabIndex = 15;
            this.ChkVchrType.Text = "Vourcher Type";
            this.ChkVchrType.Values.Text = "Vourcher Type";
            // 
            // ChkVoucherNo
            // 
            this.ChkVoucherNo.Checked = true;
            this.ChkVoucherNo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkVoucherNo.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.ChkVoucherNo.Location = new System.Drawing.Point(21, 24);
            this.ChkVoucherNo.Name = "ChkVoucherNo";
            this.ChkVoucherNo.Size = new System.Drawing.Size(89, 20);
            this.ChkVoucherNo.TabIndex = 14;
            this.ChkVoucherNo.Text = "Voucher No ";
            this.ChkVoucherNo.Values.Text = "Voucher No ";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dataGridView1);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 98);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(956, 419);
            this.panel2.TabIndex = 24;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightSalmon;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 31);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(956, 388);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dataGridView1.CellMouseMove += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseMove);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.PicPdf);
            this.panel3.Controls.Add(this.PicExcel);
            this.panel3.Controls.Add(this.PicPrint);
            this.panel3.Controls.Add(this.lbltitle);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(956, 31);
            this.panel3.TabIndex = 0;
            // 
            // PicPdf
            // 
            this.PicPdf.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PicPdf.Image = global::Sys_Sols_Inventory.Properties.Resources.pdf24;
            this.PicPdf.Location = new System.Drawing.Point(61, 3);
            this.PicPdf.Name = "PicPdf";
            this.PicPdf.Size = new System.Drawing.Size(24, 24);
            this.PicPdf.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PicPdf.TabIndex = 7;
            this.PicPdf.TabStop = false;
            this.PicPdf.Click += new System.EventHandler(this.PicPdf_Click);
            // 
            // PicExcel
            // 
            this.PicExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PicExcel.Image = global::Sys_Sols_Inventory.Properties.Resources.microsoft_excel;
            this.PicExcel.Location = new System.Drawing.Point(32, 2);
            this.PicExcel.Name = "PicExcel";
            this.PicExcel.Size = new System.Drawing.Size(24, 24);
            this.PicExcel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PicExcel.TabIndex = 6;
            this.PicExcel.TabStop = false;
            this.PicExcel.Click += new System.EventHandler(this.PicExcel_Click);
            // 
            // PicPrint
            // 
            this.PicPrint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PicPrint.Image = global::Sys_Sols_Inventory.Properties.Resources.paper6;
            this.PicPrint.Location = new System.Drawing.Point(3, 3);
            this.PicPrint.Name = "PicPrint";
            this.PicPrint.Size = new System.Drawing.Size(24, 24);
            this.PicPrint.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PicPrint.TabIndex = 5;
            this.PicPrint.TabStop = false;
            this.PicPrint.Click += new System.EventHandler(this.PicPrint_Click);
            // 
            // lbltitle
            // 
            this.lbltitle.AutoSize = true;
            this.lbltitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltitle.ForeColor = System.Drawing.Color.Red;
            this.lbltitle.Location = new System.Drawing.Point(277, 4);
            this.lbltitle.Name = "lbltitle";
            this.lbltitle.Size = new System.Drawing.Size(88, 22);
            this.lbltitle.TabIndex = 0;
            this.lbltitle.Text = "Day Book";
            // 
            // printDocument1
            // 
            this.printDocument1.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.printDocument1_BeginPrint);
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // UNDER
            // 
            this.UNDER.AllowDrop = true;
            this.UNDER.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.UNDER.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.UNDER.DropDownWidth = 211;
            this.UNDER.Location = new System.Drawing.Point(365, 71);
            this.UNDER.Name = "UNDER";
            this.UNDER.Size = new System.Drawing.Size(211, 21);
            this.UNDER.TabIndex = 72;
            // 
            // kryptonLabel5
            // 
            this.kryptonLabel5.Location = new System.Drawing.Point(318, 72);
            this.kryptonLabel5.Name = "kryptonLabel5";
            this.kryptonLabel5.Size = new System.Drawing.Size(47, 20);
            this.kryptonLabel5.TabIndex = 73;
            this.kryptonLabel5.Values.Text = "Under:";
            // 
            // Day_Book
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(956, 517);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Day_Book";
            this.Text = "Day Book";
            this.Load += new System.EventHandler(this.Day_Book_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmLedgers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DrpVoucherType)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicPdf)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicExcel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicPrint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UNDER)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel2;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox ChkNarration;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox ChkVchrType;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox ChkVoucherNo;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSave;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnClear;
        private System.Windows.Forms.DateTimePicker Date_From;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private System.Windows.Forms.DateTimePicker Date_To;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lbltitle;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox DrpVoucherType;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.PictureBox PicPdf;
        private System.Windows.Forms.PictureBox PicExcel;
        private System.Windows.Forms.PictureBox PicPrint;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cmLedgers;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox UNDER;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel5;
    }
}