namespace Sys_Sols_Inventory
{
    partial class Receipt_Report
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.grpDate = new System.Windows.Forms.GroupBox();
            this.kryptonButton1 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnRun = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.label1 = new System.Windows.Forms.Label();
            this.rb_payment = new System.Windows.Forms.RadioButton();
            this.rb_reciept = new System.Windows.Forms.RadioButton();
            this.Chk = new System.Windows.Forms.CheckBox();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.DATE_FROM = new System.Windows.Forms.DateTimePicker();
            this.DATE_TO = new System.Windows.Forms.DateTimePicker();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.dg = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.grpDate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.grpDate);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(918, 126);
            this.panel1.TabIndex = 12;
            // 
            // grpDate
            // 
            this.grpDate.Controls.Add(this.kryptonButton1);
            this.grpDate.Controls.Add(this.btnRun);
            this.grpDate.Controls.Add(this.label1);
            this.grpDate.Controls.Add(this.rb_payment);
            this.grpDate.Controls.Add(this.rb_reciept);
            this.grpDate.Controls.Add(this.Chk);
            this.grpDate.Controls.Add(this.kryptonLabel2);
            this.grpDate.Controls.Add(this.DATE_FROM);
            this.grpDate.Controls.Add(this.DATE_TO);
            this.grpDate.Controls.Add(this.kryptonLabel3);
            this.grpDate.Dock = System.Windows.Forms.DockStyle.Left;
            this.grpDate.Location = new System.Drawing.Point(0, 0);
            this.grpDate.Name = "grpDate";
            this.grpDate.Size = new System.Drawing.Size(915, 126);
            this.grpDate.TabIndex = 0;
            this.grpDate.TabStop = false;
            this.grpDate.Enter += new System.EventHandler(this.grpDate_Enter);
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.Location = new System.Drawing.Point(481, 68);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.Size = new System.Drawing.Size(117, 25);
            this.kryptonButton1.TabIndex = 10;
            this.kryptonButton1.Values.Text = "Refresh";
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(481, 36);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(117, 25);
            this.btnRun.TabIndex = 9;
            this.btnRun.Values.Text = "Search";
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(350, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Type";
            // 
            // rb_payment
            // 
            this.rb_payment.AutoSize = true;
            this.rb_payment.Location = new System.Drawing.Point(362, 72);
            this.rb_payment.Name = "rb_payment";
            this.rb_payment.Size = new System.Drawing.Size(71, 17);
            this.rb_payment.TabIndex = 20;
            this.rb_payment.Text = "Payments";
            this.rb_payment.UseVisualStyleBackColor = true;
            // 
            // rb_reciept
            // 
            this.rb_reciept.AutoSize = true;
            this.rb_reciept.Checked = true;
            this.rb_reciept.Location = new System.Drawing.Point(362, 46);
            this.rb_reciept.Name = "rb_reciept";
            this.rb_reciept.Size = new System.Drawing.Size(62, 17);
            this.rb_reciept.TabIndex = 19;
            this.rb_reciept.TabStop = true;
            this.rb_reciept.Text = "Receipt";
            this.rb_reciept.UseVisualStyleBackColor = true;
            // 
            // Chk
            // 
            this.Chk.AutoSize = true;
            this.Chk.Location = new System.Drawing.Point(71, 27);
            this.Chk.Name = "Chk";
            this.Chk.Size = new System.Drawing.Size(99, 17);
            this.Chk.TabIndex = 18;
            this.Chk.Text = "Filter With Date";
            this.Chk.UseVisualStyleBackColor = true;
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(65, 51);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(70, 20);
            this.kryptonLabel2.TabIndex = 4;
            this.kryptonLabel2.Values.Text = "Date From:";
            // 
            // DATE_FROM
            // 
            this.DATE_FROM.CustomFormat = "dd/MM/yyyy";
            this.DATE_FROM.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DATE_FROM.Location = new System.Drawing.Point(135, 51);
            this.DATE_FROM.Name = "DATE_FROM";
            this.DATE_FROM.Size = new System.Drawing.Size(162, 20);
            this.DATE_FROM.TabIndex = 5;
            // 
            // DATE_TO
            // 
            this.DATE_TO.CustomFormat = "dd/MM/yyyy";
            this.DATE_TO.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DATE_TO.Location = new System.Drawing.Point(135, 74);
            this.DATE_TO.Name = "DATE_TO";
            this.DATE_TO.Size = new System.Drawing.Size(162, 20);
            this.DATE_TO.TabIndex = 7;
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(65, 74);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(56, 20);
            this.kryptonLabel3.TabIndex = 6;
            this.kryptonLabel3.Values.Text = "Date To:";
            // 
            // dg
            // 
            this.dg.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg.Dock = System.Windows.Forms.DockStyle.Top;
            this.dg.Location = new System.Drawing.Point(0, 126);
            this.dg.Name = "dg";
            this.dg.RowHeadersVisible = false;
            this.dg.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg.Size = new System.Drawing.Size(918, 333);
            this.dg.TabIndex = 100;
            // 
            // Receipt_Report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(918, 460);
            this.Controls.Add(this.dg);
            this.Controls.Add(this.panel1);
            this.Name = "Receipt_Report";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Receipt_Report";
            this.Load += new System.EventHandler(this.Receipt_Report_Load);
            this.panel1.ResumeLayout(false);
            this.grpDate.ResumeLayout(false);
            this.grpDate.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox Chk;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnRun;
        private System.Windows.Forms.GroupBox grpDate;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private System.Windows.Forms.DateTimePicker DATE_FROM;
        private System.Windows.Forms.DateTimePicker DATE_TO;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private System.Windows.Forms.DataGridView dg;
        private System.Windows.Forms.RadioButton rb_payment;
        private System.Windows.Forms.RadioButton rb_reciept;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton1;
        private System.Windows.Forms.Label label1;
    }
}