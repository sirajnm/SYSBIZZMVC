namespace Sys_Sols_Inventory.reports
{
    partial class Recievables_and_Payables
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
            this.lb_recievable = new System.Windows.Forms.Label();
            this.lb_payables = new System.Windows.Forms.Label();
            this.cb_datserch = new System.Windows.Forms.CheckBox();
            this.Date_To = new System.Windows.Forms.DateTimePicker();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.Date_From = new System.Windows.Forms.DateTimePicker();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.Find = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dg_recieble = new System.Windows.Forms.DataGridView();
            this.d = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.e = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.f = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dg_payables = new System.Windows.Forms.DataGridView();
            this.a = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.b = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.net_profit = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.Profit_Margin = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_recieble)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dg_payables)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lb_recievable);
            this.panel1.Controls.Add(this.lb_payables);
            this.panel1.Controls.Add(this.cb_datserch);
            this.panel1.Controls.Add(this.Date_To);
            this.panel1.Controls.Add(this.kryptonLabel1);
            this.panel1.Controls.Add(this.Date_From);
            this.panel1.Controls.Add(this.kryptonLabel3);
            this.panel1.Controls.Add(this.Find);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1028, 131);
            this.panel1.TabIndex = 2;
            // 
            // lb_recievable
            // 
            this.lb_recievable.AutoSize = true;
            this.lb_recievable.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_recievable.ForeColor = System.Drawing.Color.Red;
            this.lb_recievable.Location = new System.Drawing.Point(623, 99);
            this.lb_recievable.Name = "lb_recievable";
            this.lb_recievable.Size = new System.Drawing.Size(108, 22);
            this.lb_recievable.TabIndex = 80;
            this.lb_recievable.Text = "Recievables";
            // 
            // lb_payables
            // 
            this.lb_payables.AutoSize = true;
            this.lb_payables.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_payables.ForeColor = System.Drawing.Color.Red;
            this.lb_payables.Location = new System.Drawing.Point(17, 99);
            this.lb_payables.Name = "lb_payables";
            this.lb_payables.Size = new System.Drawing.Size(84, 22);
            this.lb_payables.TabIndex = 79;
            this.lb_payables.Text = "Payables";
            // 
            // cb_datserch
            // 
            this.cb_datserch.AutoSize = true;
            this.cb_datserch.Location = new System.Drawing.Point(47, 13);
            this.cb_datserch.Name = "cb_datserch";
            this.cb_datserch.Size = new System.Drawing.Size(86, 17);
            this.cb_datserch.TabIndex = 78;
            this.cb_datserch.Text = "Date Search";
            this.cb_datserch.UseVisualStyleBackColor = true;
            this.cb_datserch.CheckedChanged += new System.EventHandler(this.cb_datserch_CheckedChanged);
            // 
            // Date_To
            // 
            this.Date_To.CustomFormat = "dd/MM/yyyy";
            this.Date_To.Enabled = false;
            this.Date_To.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Date_To.Location = new System.Drawing.Point(297, 39);
            this.Date_To.Name = "Date_To";
            this.Date_To.Size = new System.Drawing.Size(176, 20);
            this.Date_To.TabIndex = 77;
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(268, 39);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(30, 20);
            this.kryptonLabel1.TabIndex = 76;
            this.kryptonLabel1.Values.Text = "To :";
            // 
            // Date_From
            // 
            this.Date_From.CustomFormat = "dd/MM/yyyy";
            this.Date_From.Enabled = false;
            this.Date_From.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Date_From.Location = new System.Drawing.Point(87, 39);
            this.Date_From.Name = "Date_From";
            this.Date_From.Size = new System.Drawing.Size(165, 20);
            this.Date_From.TabIndex = 75;
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(47, 39);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(45, 20);
            this.kryptonLabel3.TabIndex = 74;
            this.kryptonLabel3.Values.Text = "From :";
            // 
            // Find
            // 
            this.Find.Location = new System.Drawing.Point(424, 82);
            this.Find.Name = "Find";
            this.Find.Size = new System.Drawing.Size(70, 25);
            this.Find.TabIndex = 4;
            this.Find.Values.Text = "Search";
            this.Find.Click += new System.EventHandler(this.Find_Click_1);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Location = new System.Drawing.Point(0, 137);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1102, 631);
            this.panel2.TabIndex = 3;
            // 
            // panel4
            // 
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(594, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(508, 631);
            this.panel4.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dg_recieble);
            this.panel3.Controls.Add(this.dg_payables);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(600, 631);
            this.panel3.TabIndex = 0;
            // 
            // dg_recieble
            // 
            this.dg_recieble.AllowUserToAddRows = false;
            this.dg_recieble.AllowUserToDeleteRows = false;
            this.dg_recieble.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dg_recieble.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_recieble.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.d,
            this.e,
            this.f});
            this.dg_recieble.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg_recieble.Location = new System.Drawing.Point(0, 0);
            this.dg_recieble.Name = "dg_recieble";
            this.dg_recieble.RowHeadersVisible = false;
            this.dg_recieble.RowTemplate.Height = 25;
            this.dg_recieble.Size = new System.Drawing.Size(600, 631);
            this.dg_recieble.TabIndex = 1;
            // 
            // d
            // 
            this.d.HeaderText = "";
            this.d.Name = "d";
            this.d.Width = 200;
            // 
            // e
            // 
            this.e.HeaderText = "";
            this.e.Name = "e";
            this.e.Width = 200;
            // 
            // f
            // 
            this.f.HeaderText = "";
            this.f.Name = "f";
            // 
            // dg_payables
            // 
            this.dg_payables.AllowUserToAddRows = false;
            this.dg_payables.AllowUserToDeleteRows = false;
            this.dg_payables.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dg_payables.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_payables.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.a,
            this.b,
            this.c});
            this.dg_payables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg_payables.Location = new System.Drawing.Point(0, 0);
            this.dg_payables.Name = "dg_payables";
            this.dg_payables.RowHeadersVisible = false;
            this.dg_payables.RowTemplate.Height = 25;
            this.dg_payables.Size = new System.Drawing.Size(600, 631);
            this.dg_payables.TabIndex = 1;
            // 
            // a
            // 
            this.a.HeaderText = "";
            this.a.Name = "a";
            this.a.Width = 200;
            // 
            // b
            // 
            this.b.HeaderText = "";
            this.b.Name = "b";
            this.b.Width = 200;
            // 
            // c
            // 
            this.c.HeaderText = "";
            this.c.Name = "c";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "a";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "b";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "c";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "a";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "b";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "c";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            // 
            // net_profit
            // 
            this.net_profit.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.net_profit.Location = new System.Drawing.Point(375, 777);
            this.net_profit.MaxLength = 3;
            this.net_profit.Name = "net_profit";
            this.net_profit.Size = new System.Drawing.Size(193, 34);
            this.net_profit.StateActive.Back.Color1 = System.Drawing.Color.White;
            this.net_profit.StateActive.Content.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.net_profit.TabIndex = 4;
            // 
            // Profit_Margin
            // 
            this.Profit_Margin.AutoSize = true;
            this.Profit_Margin.Location = new System.Drawing.Point(294, 789);
            this.Profit_Margin.Name = "Profit_Margin";
            this.Profit_Margin.Size = new System.Drawing.Size(66, 13);
            this.Profit_Margin.TabIndex = 81;
            this.Profit_Margin.Text = "Profit Margin";
            // 
            // Recievables_and_Payables
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 750);
            this.Controls.Add(this.Profit_Margin);
            this.Controls.Add(this.net_profit);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Recievables_and_Payables";
            this.Text = "Recievables and Payables";
            this.Load += new System.EventHandler(this.Recievables_and_Payables_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dg_recieble)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dg_payables)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox cb_datserch;
        private System.Windows.Forms.DateTimePicker Date_To;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private System.Windows.Forms.DateTimePicker Date_From;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private ComponentFactory.Krypton.Toolkit.KryptonButton Find;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView dg_recieble;
        private System.Windows.Forms.DataGridView dg_payables;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.Label lb_recievable;
        private System.Windows.Forms.Label lb_payables;
        private System.Windows.Forms.DataGridViewTextBoxColumn d;
        private System.Windows.Forms.DataGridViewTextBoxColumn e;
        private System.Windows.Forms.DataGridViewTextBoxColumn f;
        private System.Windows.Forms.DataGridViewTextBoxColumn a;
        private System.Windows.Forms.DataGridViewTextBoxColumn b;
        private System.Windows.Forms.DataGridViewTextBoxColumn c;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox net_profit;
        private System.Windows.Forms.Label Profit_Margin;
    }
}