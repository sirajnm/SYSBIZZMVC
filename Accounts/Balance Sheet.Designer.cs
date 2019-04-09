namespace Sys_Sols_Inventory.Accounts
{
    partial class Balance_Sheet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Balance_Sheet));
            this.panel1 = new System.Windows.Forms.Panel();
            this.Date_To = new System.Windows.Forms.DateTimePicker();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.Date_From = new System.Windows.Forms.DateTimePicker();
            this.btnSave = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnClear = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lbltitle = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dataAsset = new System.Windows.Forms.DataGridView();
            this.Assets = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amounta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataLiability = new System.Windows.Forms.DataGridView();
            this.Liabilities = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataAsset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataLiability)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Date_To);
            this.panel1.Controls.Add(this.kryptonLabel1);
            this.panel1.Controls.Add(this.Date_From);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.btnClear);
            this.panel1.Controls.Add(this.kryptonLabel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(904, 75);
            this.panel1.TabIndex = 26;
            // 
            // Date_To
            // 
            this.Date_To.CustomFormat = "dd/MM/yyyy";
            this.Date_To.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Date_To.Location = new System.Drawing.Point(367, 17);
            this.Date_To.Name = "Date_To";
            this.Date_To.Size = new System.Drawing.Size(221, 20);
            this.Date_To.TabIndex = 63;
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(315, 17);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(30, 20);
            this.kryptonLabel1.TabIndex = 62;
            this.kryptonLabel1.Values.Text = "To :";
            // 
            // Date_From
            // 
            this.Date_From.CustomFormat = "dd/MM/yyyy";
            this.Date_From.Enabled = false;
            this.Date_From.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Date_From.Location = new System.Drawing.Point(72, 16);
            this.Date_From.Name = "Date_From";
            this.Date_From.Size = new System.Drawing.Size(221, 20);
            this.Date_From.TabIndex = 61;
            this.Date_From.ValueChanged += new System.EventHandler(this.Date_From_ValueChanged);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(635, 12);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 25);
            this.btnSave.TabIndex = 35;
            this.btnSave.Values.Text = "Search";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(731, 12);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(90, 25);
            this.btnClear.TabIndex = 36;
            this.btnClear.Values.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Enabled = false;
            this.kryptonLabel3.Location = new System.Drawing.Point(20, 16);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(45, 20);
            this.kryptonLabel3.TabIndex = 20;
            this.kryptonLabel3.Values.Text = "From :";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.lbltitle);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 75);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(904, 31);
            this.panel3.TabIndex = 27;
            // 
            // lbltitle
            // 
            this.lbltitle.AutoSize = true;
            this.lbltitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltitle.ForeColor = System.Drawing.Color.Red;
            this.lbltitle.Location = new System.Drawing.Point(301, 2);
            this.lbltitle.Name = "lbltitle";
            this.lbltitle.Size = new System.Drawing.Size(127, 22);
            this.lbltitle.TabIndex = 0;
            this.lbltitle.Text = "Balance Sheet";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.dataAsset);
            this.panel2.Controls.Add(this.dataLiability);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 106);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(904, 347);
            this.panel2.TabIndex = 28;
            // 
            // dataAsset
            // 
            this.dataAsset.AllowUserToAddRows = false;
            this.dataAsset.AllowUserToDeleteRows = false;
            this.dataAsset.AllowUserToResizeRows = false;
            this.dataAsset.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataAsset.BackgroundColor = System.Drawing.Color.White;
            this.dataAsset.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightSalmon;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.SkyBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataAsset.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataAsset.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataAsset.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Assets,
            this.Amounta});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataAsset.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataAsset.Dock = System.Windows.Forms.DockStyle.Left;
            this.dataAsset.Location = new System.Drawing.Point(452, 0);
            this.dataAsset.Name = "dataAsset";
            this.dataAsset.ReadOnly = true;
            this.dataAsset.RowHeadersVisible = false;
            this.dataAsset.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataAsset.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataAsset.Size = new System.Drawing.Size(452, 347);
            this.dataAsset.TabIndex = 28;
            // 
            // Assets
            // 
            this.Assets.HeaderText = "Assets";
            this.Assets.Name = "Assets";
            this.Assets.ReadOnly = true;
            this.Assets.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Amounta
            // 
            this.Amounta.HeaderText = "Amount";
            this.Amounta.Name = "Amounta";
            this.Amounta.ReadOnly = true;
            this.Amounta.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataLiability
            // 
            this.dataLiability.AllowUserToAddRows = false;
            this.dataLiability.AllowUserToDeleteRows = false;
            this.dataLiability.AllowUserToResizeRows = false;
            this.dataLiability.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataLiability.BackgroundColor = System.Drawing.Color.White;
            this.dataLiability.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.LightSalmon;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataLiability.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataLiability.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataLiability.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Liabilities,
            this.Amount});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataLiability.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataLiability.Dock = System.Windows.Forms.DockStyle.Left;
            this.dataLiability.Location = new System.Drawing.Point(0, 0);
            this.dataLiability.Name = "dataLiability";
            this.dataLiability.ReadOnly = true;
            this.dataLiability.RowHeadersVisible = false;
            this.dataLiability.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataLiability.Size = new System.Drawing.Size(452, 347);
            this.dataLiability.TabIndex = 27;
            // 
            // Liabilities
            // 
            this.Liabilities.HeaderText = "Liabilities";
            this.Liabilities.Name = "Liabilities";
            this.Liabilities.ReadOnly = true;
            this.Liabilities.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Amount
            // 
            this.Amount.HeaderText = "Amount";
            this.Amount.Name = "Amount";
            this.Amount.ReadOnly = true;
            this.Amount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Balance_Sheet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(904, 453);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Balance_Sheet";
            this.Text = "Balance_Sheet";
            this.Load += new System.EventHandler(this.Balance_Sheet_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataAsset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataLiability)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DateTimePicker Date_To;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private System.Windows.Forms.DateTimePicker Date_From;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSave;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnClear;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lbltitle;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dataAsset;
        private System.Windows.Forms.DataGridView dataLiability;
        private System.Windows.Forms.DataGridViewTextBoxColumn Assets;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amounta;
        private System.Windows.Forms.DataGridViewTextBoxColumn Liabilities;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
    }
}