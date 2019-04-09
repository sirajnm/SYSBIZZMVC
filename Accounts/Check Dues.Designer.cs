namespace Sys_Sols_Inventory.Accounts
{
    partial class Check_Dues
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Check_Dues));
            this.dgCheques = new System.Windows.Forms.DataGridView();
            this.btnDocNo = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonButton1 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.btnSave = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.dgCheques)).BeginInit();
            this.SuspendLayout();
            // 
            // dgCheques
            // 
            this.dgCheques.AllowUserToAddRows = false;
            this.dgCheques.AllowUserToDeleteRows = false;
            this.dgCheques.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgCheques.Location = new System.Drawing.Point(12, 12);
            this.dgCheques.Name = "dgCheques";
            this.dgCheques.ReadOnly = true;
            this.dgCheques.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgCheques.Size = new System.Drawing.Size(888, 251);
            this.dgCheques.TabIndex = 0;
            this.dgCheques.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgCheques_CellClick);
            this.dgCheques.DoubleClick += new System.EventHandler(this.dgCheques_DoubleClick);
            // 
            // btnDocNo
            // 
            this.btnDocNo.Location = new System.Drawing.Point(827, 269);
            this.btnDocNo.Name = "btnDocNo";
            this.btnDocNo.Size = new System.Drawing.Size(73, 24);
            this.btnDocNo.TabIndex = 49;
            this.btnDocNo.Values.Text = "Close";
            this.btnDocNo.Click += new System.EventHandler(this.btnDocNo_Click);
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.Location = new System.Drawing.Point(748, 269);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.Size = new System.Drawing.Size(73, 24);
            this.kryptonButton1.TabIndex = 49;
            this.kryptonButton1.Values.Text = "Print";
            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Items.AddRange(new object[] {
            "Pending",
            "Posted",
            "Cancelled"});
            this.cmbStatus.Location = new System.Drawing.Point(12, 269);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(121, 21);
            this.cmbStatus.TabIndex = 50;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(233, 269);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(73, 24);
            this.btnSave.TabIndex = 51;
            this.btnSave.Values.Text = "&Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(140, 270);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(87, 20);
            this.dateTimePicker1.TabIndex = 52;
            // 
            // Check_Dues
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(912, 302);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.kryptonButton1);
            this.Controls.Add(this.btnDocNo);
            this.Controls.Add(this.dgCheques);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Check_Dues";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cheque Dues";
            this.Load += new System.EventHandler(this.Check_Dues_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Check_Dues_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgCheques)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgCheques;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnDocNo;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton1;
        private System.Windows.Forms.ComboBox cmbStatus;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSave;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
    }
}