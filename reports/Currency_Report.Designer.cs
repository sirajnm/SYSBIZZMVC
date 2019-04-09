namespace Sys_Sols_Inventory.reports
{
    partial class Currency_Report
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
            this.ItemName = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.kryptonLabel9 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.Cbx_salestype = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.Cbx_supplier = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.kryptonLabel5 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel7 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.DG_GRIDVIEW = new System.Windows.Forms.DataGridView();
            this.btnSave = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnCurr = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.CURCODE = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.lb_Currency = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ItemName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cbx_salestype)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cbx_supplier)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DG_GRIDVIEW)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCurr);
            this.panel1.Controls.Add(this.CURCODE);
            this.panel1.Controls.Add(this.lb_Currency);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.ItemName);
            this.panel1.Controls.Add(this.kryptonLabel9);
            this.panel1.Controls.Add(this.Cbx_salestype);
            this.panel1.Controls.Add(this.Cbx_supplier);
            this.panel1.Controls.Add(this.kryptonLabel5);
            this.panel1.Controls.Add(this.kryptonLabel7);
            this.panel1.Location = new System.Drawing.Point(3, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1005, 131);
            this.panel1.TabIndex = 0;
            // 
            // ItemName
            // 
            this.ItemName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.ItemName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ItemName.DropDownWidth = 211;
            this.ItemName.Location = new System.Drawing.Point(119, 41);
            this.ItemName.Name = "ItemName";
            this.ItemName.Size = new System.Drawing.Size(374, 21);
            this.ItemName.TabIndex = 123;
            // 
            // kryptonLabel9
            // 
            this.kryptonLabel9.Location = new System.Drawing.Point(44, 43);
            this.kryptonLabel9.Name = "kryptonLabel9";
            this.kryptonLabel9.Size = new System.Drawing.Size(77, 20);
            this.kryptonLabel9.TabIndex = 122;
            this.kryptonLabel9.Values.Text = "Item Name :";
            // 
            // Cbx_salestype
            // 
            this.Cbx_salestype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cbx_salestype.DropDownWidth = 211;
            this.Cbx_salestype.Location = new System.Drawing.Point(360, 14);
            this.Cbx_salestype.Name = "Cbx_salestype";
            this.Cbx_salestype.Size = new System.Drawing.Size(133, 21);
            this.Cbx_salestype.TabIndex = 121;
            // 
            // Cbx_supplier
            // 
            this.Cbx_supplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cbx_supplier.DropDownWidth = 211;
            this.Cbx_supplier.Location = new System.Drawing.Point(119, 14);
            this.Cbx_supplier.Name = "Cbx_supplier";
            this.Cbx_supplier.Size = new System.Drawing.Size(133, 21);
            this.Cbx_supplier.TabIndex = 120;
            // 
            // kryptonLabel5
            // 
            this.kryptonLabel5.Location = new System.Drawing.Point(44, 15);
            this.kryptonLabel5.Name = "kryptonLabel5";
            this.kryptonLabel5.Size = new System.Drawing.Size(58, 20);
            this.kryptonLabel5.TabIndex = 119;
            this.kryptonLabel5.Values.Text = "Supplier:";
            // 
            // kryptonLabel7
            // 
            this.kryptonLabel7.Location = new System.Drawing.Point(265, 14);
            this.kryptonLabel7.Name = "kryptonLabel7";
            this.kryptonLabel7.Size = new System.Drawing.Size(92, 20);
            this.kryptonLabel7.TabIndex = 118;
            this.kryptonLabel7.Values.Text = "Purchase Type:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.DG_GRIDVIEW);
            this.panel2.Location = new System.Drawing.Point(3, 149);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1005, 435);
            this.panel2.TabIndex = 1;
            // 
            // DG_GRIDVIEW
            // 
            this.DG_GRIDVIEW.AllowUserToAddRows = false;
            this.DG_GRIDVIEW.BackgroundColor = System.Drawing.SystemColors.ControlDark;
            this.DG_GRIDVIEW.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DG_GRIDVIEW.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DG_GRIDVIEW.Location = new System.Drawing.Point(0, 0);
            this.DG_GRIDVIEW.Name = "DG_GRIDVIEW";
            this.DG_GRIDVIEW.RowTemplate.Height = 30;
            this.DG_GRIDVIEW.Size = new System.Drawing.Size(1005, 435);
            this.DG_GRIDVIEW.TabIndex = 8;
            this.DG_GRIDVIEW.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DG_GRIDVIEW_CellFormatting);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(403, 94);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 25);
            this.btnSave.TabIndex = 124;
            this.btnSave.Values.Text = "Search";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCurr
            // 
            this.btnCurr.Location = new System.Drawing.Point(233, 68);
            this.btnCurr.Name = "btnCurr";
            this.btnCurr.Size = new System.Drawing.Size(19, 26);
            this.btnCurr.TabIndex = 132;
            this.btnCurr.Values.Text = ">";
            this.btnCurr.Click += new System.EventHandler(this.btnCurr_Click);
            // 
            // CURCODE
            // 
            this.CURCODE.Location = new System.Drawing.Point(119, 69);
            this.CURCODE.Name = "CURCODE";
            this.CURCODE.ReadOnly = true;
            this.CURCODE.Size = new System.Drawing.Size(104, 20);
            this.CURCODE.TabIndex = 131;
            // 
            // lb_Currency
            // 
            this.lb_Currency.Location = new System.Drawing.Point(44, 69);
            this.lb_Currency.Name = "lb_Currency";
            this.lb_Currency.Size = new System.Drawing.Size(68, 20);
            this.lb_Currency.TabIndex = 130;
            this.lb_Currency.Values.Text = "Currency  :";
            // 
            // Currency_Report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 750);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Currency_Report";
            this.Text = "Currency Report";
            this.Load += new System.EventHandler(this.Currency_Report_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ItemName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cbx_salestype)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cbx_supplier)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DG_GRIDVIEW)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox ItemName;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel9;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox Cbx_salestype;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox Cbx_supplier;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel5;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel7;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView DG_GRIDVIEW;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSave;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnCurr;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox CURCODE;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lb_Currency;
    }
}