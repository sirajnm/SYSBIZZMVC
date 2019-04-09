namespace Sys_Sols_Inventory
{
    partial class Sales_RPT_HDR
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
            this.grp_Salesrpt = new System.Windows.Forms.GroupBox();
            this.btnDetailed = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnExcel = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnSave = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Trademark = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.DrpCategory = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.Group = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.TYPE = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.EndDate = new System.Windows.Forms.DateTimePicker();
            this.kryptonLabel8 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.Chk = new System.Windows.Forms.CheckBox();
            this.StartDate = new System.Windows.Forms.DateTimePicker();
            this.kryptonLabel6 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.ItemName = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.kryptonLabel9 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.Cbx_salestype = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.krlb_salesType = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.grp_Salesrpt.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Trademark)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DrpCategory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Group)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TYPE)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cbx_salestype)).BeginInit();
            this.SuspendLayout();
            // 
            // grp_Salesrpt
            // 
            this.grp_Salesrpt.Controls.Add(this.btnDetailed);
            this.grp_Salesrpt.Controls.Add(this.btnExcel);
            this.grp_Salesrpt.Controls.Add(this.btnSave);
            this.grp_Salesrpt.Controls.Add(this.panel1);
            this.grp_Salesrpt.Controls.Add(this.EndDate);
            this.grp_Salesrpt.Controls.Add(this.kryptonLabel8);
            this.grp_Salesrpt.Controls.Add(this.Chk);
            this.grp_Salesrpt.Controls.Add(this.StartDate);
            this.grp_Salesrpt.Controls.Add(this.kryptonLabel6);
            this.grp_Salesrpt.Controls.Add(this.ItemName);
            this.grp_Salesrpt.Controls.Add(this.kryptonLabel9);
            this.grp_Salesrpt.Controls.Add(this.Cbx_salestype);
            this.grp_Salesrpt.Controls.Add(this.krlb_salesType);
            this.grp_Salesrpt.Location = new System.Drawing.Point(2, 1);
            this.grp_Salesrpt.Name = "grp_Salesrpt";
            this.grp_Salesrpt.Size = new System.Drawing.Size(901, 165);
            this.grp_Salesrpt.TabIndex = 0;
            this.grp_Salesrpt.TabStop = false;
            this.grp_Salesrpt.Text = "Search";
            // 
            // btnDetailed
            // 
            this.btnDetailed.Location = new System.Drawing.Point(702, 128);
            this.btnDetailed.Name = "btnDetailed";
            this.btnDetailed.Size = new System.Drawing.Size(90, 25);
            this.btnDetailed.TabIndex = 126;
            this.btnDetailed.Values.Text = "Detailed";
            this.btnDetailed.Click += new System.EventHandler(this.btnDetailed_Click);
            // 
            // btnExcel
            // 
            this.btnExcel.Location = new System.Drawing.Point(606, 128);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(90, 25);
            this.btnExcel.TabIndex = 127;
            this.btnExcel.Values.Text = "Excel";
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(510, 128);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 25);
            this.btnSave.TabIndex = 128;
            this.btnSave.Values.Text = "Search";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Trademark);
            this.panel1.Controls.Add(this.DrpCategory);
            this.panel1.Controls.Add(this.Group);
            this.panel1.Controls.Add(this.TYPE);
            this.panel1.Controls.Add(this.kryptonLabel4);
            this.panel1.Controls.Add(this.kryptonLabel3);
            this.panel1.Controls.Add(this.kryptonLabel2);
            this.panel1.Controls.Add(this.kryptonLabel1);
            this.panel1.Location = new System.Drawing.Point(493, 11);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(402, 96);
            this.panel1.TabIndex = 125;
            // 
            // Trademark
            // 
            this.Trademark.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Trademark.DropDownWidth = 211;
            this.Trademark.Location = new System.Drawing.Point(258, 64);
            this.Trademark.Name = "Trademark";
            this.Trademark.Size = new System.Drawing.Size(141, 21);
            this.Trademark.TabIndex = 19;
            // 
            // DrpCategory
            // 
            this.DrpCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DrpCategory.DropDownWidth = 211;
            this.DrpCategory.Location = new System.Drawing.Point(258, 16);
            this.DrpCategory.Name = "DrpCategory";
            this.DrpCategory.Size = new System.Drawing.Size(141, 21);
            this.DrpCategory.TabIndex = 18;
            // 
            // Group
            // 
            this.Group.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Group.DropDownWidth = 211;
            this.Group.Location = new System.Drawing.Point(50, 62);
            this.Group.Name = "Group";
            this.Group.Size = new System.Drawing.Size(131, 21);
            this.Group.TabIndex = 17;
            // 
            // TYPE
            // 
            this.TYPE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TYPE.DropDownWidth = 211;
            this.TYPE.Location = new System.Drawing.Point(50, 16);
            this.TYPE.Name = "TYPE";
            this.TYPE.Size = new System.Drawing.Size(131, 21);
            this.TYPE.TabIndex = 16;
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(3, 63);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(47, 20);
            this.kryptonLabel4.TabIndex = 15;
            this.kryptonLabel4.Values.Text = "Group:";
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(198, 17);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(63, 20);
            this.kryptonLabel3.TabIndex = 14;
            this.kryptonLabel3.Values.Text = "Category:";
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(186, 62);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(75, 20);
            this.kryptonLabel2.TabIndex = 13;
            this.kryptonLabel2.Values.Text = "Trade Mark:";
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(3, 17);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(39, 20);
            this.kryptonLabel1.TabIndex = 12;
            this.kryptonLabel1.Values.Text = "Type:";
            // 
            // EndDate
            // 
            this.EndDate.Enabled = false;
            this.EndDate.Location = new System.Drawing.Point(346, 133);
            this.EndDate.Name = "EndDate";
            this.EndDate.Size = new System.Drawing.Size(141, 20);
            this.EndDate.TabIndex = 124;
            // 
            // kryptonLabel8
            // 
            this.kryptonLabel8.Location = new System.Drawing.Point(277, 133);
            this.kryptonLabel8.Name = "kryptonLabel8";
            this.kryptonLabel8.Size = new System.Drawing.Size(63, 20);
            this.kryptonLabel8.TabIndex = 123;
            this.kryptonLabel8.Values.Text = "End Date:";
            // 
            // Chk
            // 
            this.Chk.AutoSize = true;
            this.Chk.Location = new System.Drawing.Point(115, 110);
            this.Chk.Name = "Chk";
            this.Chk.Size = new System.Drawing.Size(99, 17);
            this.Chk.TabIndex = 122;
            this.Chk.Text = "Report on Date";
            this.Chk.UseVisualStyleBackColor = true;
            // 
            // StartDate
            // 
            this.StartDate.Enabled = false;
            this.StartDate.Location = new System.Drawing.Point(113, 133);
            this.StartDate.Name = "StartDate";
            this.StartDate.Size = new System.Drawing.Size(144, 20);
            this.StartDate.TabIndex = 121;
            // 
            // kryptonLabel6
            // 
            this.kryptonLabel6.Location = new System.Drawing.Point(22, 133);
            this.kryptonLabel6.Name = "kryptonLabel6";
            this.kryptonLabel6.Size = new System.Drawing.Size(71, 20);
            this.kryptonLabel6.TabIndex = 120;
            this.kryptonLabel6.Values.Text = "Start  Date:";
            // 
            // ItemName
            // 
            this.ItemName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.ItemName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ItemName.DropDownWidth = 211;
            this.ItemName.Location = new System.Drawing.Point(113, 74);
            this.ItemName.Name = "ItemName";
            this.ItemName.Size = new System.Drawing.Size(374, 21);
            this.ItemName.TabIndex = 119;
            // 
            // kryptonLabel9
            // 
            this.kryptonLabel9.Location = new System.Drawing.Point(21, 75);
            this.kryptonLabel9.Name = "kryptonLabel9";
            this.kryptonLabel9.Size = new System.Drawing.Size(77, 20);
            this.kryptonLabel9.TabIndex = 118;
            this.kryptonLabel9.Values.Text = "Item Name :";
            // 
            // Cbx_salestype
            // 
            this.Cbx_salestype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cbx_salestype.DropDownWidth = 211;
            this.Cbx_salestype.Location = new System.Drawing.Point(113, 27);
            this.Cbx_salestype.Name = "Cbx_salestype";
            this.Cbx_salestype.Size = new System.Drawing.Size(144, 21);
            this.Cbx_salestype.TabIndex = 22;
            // 
            // krlb_salesType
            // 
            this.krlb_salesType.Location = new System.Drawing.Point(19, 29);
            this.krlb_salesType.Name = "krlb_salesType";
            this.krlb_salesType.Size = new System.Drawing.Size(62, 20);
            this.krlb_salesType.TabIndex = 21;
            this.krlb_salesType.Values.Text = "SaleType:";
            // 
            // Sales_RPT_HDR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(904, 432);
            this.Controls.Add(this.grp_Salesrpt);
            this.Name = "Sales_RPT_HDR";
            this.Text = "Sales Report";
            this.grp_Salesrpt.ResumeLayout(false);
            this.grp_Salesrpt.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Trademark)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DrpCategory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Group)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TYPE)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cbx_salestype)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grp_Salesrpt;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox Cbx_salestype;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel krlb_salesType;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox ItemName;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel9;
        private System.Windows.Forms.DateTimePicker EndDate;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel8;
        private System.Windows.Forms.CheckBox Chk;
        private System.Windows.Forms.DateTimePicker StartDate;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel6;
        private System.Windows.Forms.Panel panel1;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox Trademark;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox DrpCategory;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox Group;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox TYPE;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnDetailed;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnExcel;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSave;

    }
}