namespace Sys_Sols_Inventory
{
    partial class Currency
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Currency));
            this.Grd_Currency = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.Txt_code = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_english = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_arab = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_exchange = new System.Windows.Forms.TextBox();
            this.btnSave = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.BtbClear = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.Btndelete = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.PnlArabic = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.Grd_Currency)).BeginInit();
            this.PnlArabic.SuspendLayout();
            this.SuspendLayout();
            // 
            // Grd_Currency
            // 
            this.Grd_Currency.AllowUserToAddRows = false;
            this.Grd_Currency.AllowUserToDeleteRows = false;
            this.Grd_Currency.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.Grd_Currency.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Grd_Currency.DefaultCellStyle = dataGridViewCellStyle1;
            this.Grd_Currency.Location = new System.Drawing.Point(12, 12);
            this.Grd_Currency.Name = "Grd_Currency";
            this.Grd_Currency.Size = new System.Drawing.Size(615, 183);
            this.Grd_Currency.TabIndex = 0;
            this.Grd_Currency.Click += new System.EventHandler(this.Grd_Currency_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.SteelBlue;
            this.label1.Location = new System.Drawing.Point(58, 206);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Code:";
            // 
            // Txt_code
            // 
            this.Txt_code.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.Txt_code.Location = new System.Drawing.Point(99, 204);
            this.Txt_code.MaxLength = 3;
            this.Txt_code.Name = "Txt_code";
            this.Txt_code.Size = new System.Drawing.Size(138, 20);
            this.Txt_code.TabIndex = 2;
            this.Txt_code.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Txt_code_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.SteelBlue;
            this.label2.Location = new System.Drawing.Point(302, 207);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Name :";
            // 
            // txt_english
            // 
            this.txt_english.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_english.Location = new System.Drawing.Point(395, 204);
            this.txt_english.Name = "txt_english";
            this.txt_english.Size = new System.Drawing.Size(169, 20);
            this.txt_english.TabIndex = 4;
            this.txt_english.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Txt_code_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.SteelBlue;
            this.label3.Location = new System.Drawing.Point(11, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Arab Name :";
            // 
            // txt_arab
            // 
            this.txt_arab.Location = new System.Drawing.Point(108, 5);
            this.txt_arab.Name = "txt_arab";
            this.txt_arab.Size = new System.Drawing.Size(169, 20);
            this.txt_arab.TabIndex = 6;
            this.txt_arab.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_arab.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Txt_code_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.SteelBlue;
            this.label4.Location = new System.Drawing.Point(9, 229);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Exchange Rate:";
            // 
            // txt_exchange
            // 
            this.txt_exchange.Location = new System.Drawing.Point(100, 229);
            this.txt_exchange.Name = "txt_exchange";
            this.txt_exchange.Size = new System.Drawing.Size(137, 20);
            this.txt_exchange.TabIndex = 9;
            this.txt_exchange.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Txt_code_KeyDown);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(335, 263);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 25);
            this.btnSave.TabIndex = 14;
            this.btnSave.Values.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // BtbClear
            // 
            this.BtbClear.Location = new System.Drawing.Point(431, 263);
            this.BtbClear.Name = "BtbClear";
            this.BtbClear.Size = new System.Drawing.Size(90, 25);
            this.BtbClear.TabIndex = 15;
            this.BtbClear.Values.Text = "Clear";
            this.BtbClear.Click += new System.EventHandler(this.BtbClear_Click);
            // 
            // Btndelete
            // 
            this.Btndelete.Location = new System.Drawing.Point(527, 263);
            this.Btndelete.Name = "Btndelete";
            this.Btndelete.Size = new System.Drawing.Size(90, 25);
            this.Btndelete.TabIndex = 16;
            this.Btndelete.Values.Text = "Delete";
            this.Btndelete.Click += new System.EventHandler(this.Btndelete_Click);
            // 
            // PnlArabic
            // 
            this.PnlArabic.Controls.Add(this.txt_arab);
            this.PnlArabic.Controls.Add(this.label3);
            this.PnlArabic.Location = new System.Drawing.Point(287, 229);
            this.PnlArabic.Name = "PnlArabic";
            this.PnlArabic.Size = new System.Drawing.Size(309, 33);
            this.PnlArabic.TabIndex = 17;
            // 
            // Currency
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(639, 297);
            this.Controls.Add(this.txt_english);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.PnlArabic);
            this.Controls.Add(this.Btndelete);
            this.Controls.Add(this.BtbClear);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txt_exchange);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Txt_code);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Grd_Currency);
            this.ForeColor = System.Drawing.Color.SteelBlue;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Currency";
            this.Text = "Currency";
            this.Load += new System.EventHandler(this.Currency_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Grd_Currency)).EndInit();
            this.PnlArabic.ResumeLayout(false);
            this.PnlArabic.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView Grd_Currency;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Txt_code;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_english;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_arab;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_exchange;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSave;
        private ComponentFactory.Krypton.Toolkit.KryptonButton BtbClear;
        private ComponentFactory.Krypton.Toolkit.KryptonButton Btndelete;
        private System.Windows.Forms.Panel PnlArabic;
    }
}