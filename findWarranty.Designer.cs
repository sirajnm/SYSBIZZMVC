namespace Sys_Sols_Inventory
{
    partial class findWarranty
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
            this.txtSerialNo = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.btnFind = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.drgsales = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Purchasedetails = new System.Windows.Forms.GroupBox();
            this.drgPurchase = new System.Windows.Forms.DataGridView();
            this.kryptonButton1 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.lblSaledon = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.Wvalue = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.WType = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.lblwarretyends = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.drgsales)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.Purchasedetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.drgPurchase)).BeginInit();
            this.SuspendLayout();
            // 
            // txtSerialNo
            // 
            this.txtSerialNo.Location = new System.Drawing.Point(100, 27);
            this.txtSerialNo.MaxLength = 1000;
            this.txtSerialNo.Name = "txtSerialNo";
            this.txtSerialNo.Size = new System.Drawing.Size(224, 20);
            this.txtSerialNo.TabIndex = 50;
            this.txtSerialNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSerialNo_KeyDown);
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(28, 27);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(66, 20);
            this.kryptonLabel2.TabIndex = 51;
            this.kryptonLabel2.Values.Text = "Serial No :";
            this.kryptonLabel2.Visible = false;
            // 
            // btnFind
            // 
            this.btnFind.Location = new System.Drawing.Point(330, 24);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(77, 25);
            this.btnFind.TabIndex = 52;
            this.btnFind.Values.Text = "Find";
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // drgsales
            // 
            this.drgsales.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.drgsales.Location = new System.Drawing.Point(6, 19);
            this.drgsales.Name = "drgsales";
            this.drgsales.Size = new System.Drawing.Size(819, 139);
            this.drgsales.TabIndex = 53;
            this.drgsales.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.drgsales_CellDoubleClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.drgsales);
            this.groupBox1.Location = new System.Drawing.Point(28, 201);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(837, 169);
            this.groupBox1.TabIndex = 54;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sales Details";
            // 
            // Purchasedetails
            // 
            this.Purchasedetails.Controls.Add(this.drgPurchase);
            this.Purchasedetails.Location = new System.Drawing.Point(28, 65);
            this.Purchasedetails.Name = "Purchasedetails";
            this.Purchasedetails.Size = new System.Drawing.Size(837, 138);
            this.Purchasedetails.TabIndex = 55;
            this.Purchasedetails.TabStop = false;
            this.Purchasedetails.Text = "Purchase Details";
            // 
            // drgPurchase
            // 
            this.drgPurchase.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.drgPurchase.Location = new System.Drawing.Point(12, 19);
            this.drgPurchase.Name = "drgPurchase";
            this.drgPurchase.Size = new System.Drawing.Size(819, 110);
            this.drgPurchase.TabIndex = 53;
            this.drgPurchase.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.drgPurchase_CellDoubleClick);
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.Location = new System.Drawing.Point(782, 451);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.Size = new System.Drawing.Size(77, 25);
            this.kryptonButton1.TabIndex = 56;
            this.kryptonButton1.Values.Text = "Close";
            this.kryptonButton1.Click += new System.EventHandler(this.kryptonButton1_Click_1);
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(40, 392);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(67, 20);
            this.kryptonLabel1.TabIndex = 57;
            this.kryptonLabel1.Values.Text = "Soled On :";
            this.kryptonLabel1.Visible = false;
            // 
            // lblSaledon
            // 
            this.lblSaledon.Location = new System.Drawing.Point(147, 391);
            this.lblSaledon.Name = "lblSaledon";
            this.lblSaledon.Size = new System.Drawing.Size(40, 20);
            this.lblSaledon.TabIndex = 57;
            this.lblSaledon.Values.Text = "...........";
            this.lblSaledon.Visible = false;
            // 
            // Wvalue
            // 
            this.Wvalue.Location = new System.Drawing.Point(147, 417);
            this.Wvalue.Name = "Wvalue";
            this.Wvalue.Size = new System.Drawing.Size(40, 20);
            this.Wvalue.TabIndex = 58;
            this.Wvalue.Values.Text = "...........";
            this.Wvalue.Visible = false;
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(40, 418);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(105, 20);
            this.kryptonLabel4.TabIndex = 59;
            this.kryptonLabel4.Values.Text = "Warrenty Period :";
            this.kryptonLabel4.Visible = false;
            // 
            // WType
            // 
            this.WType.Location = new System.Drawing.Point(237, 417);
            this.WType.Name = "WType";
            this.WType.Size = new System.Drawing.Size(40, 20);
            this.WType.TabIndex = 60;
            this.WType.Values.Text = "...........";
            this.WType.Visible = false;
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(40, 444);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(113, 20);
            this.kryptonLabel3.TabIndex = 61;
            this.kryptonLabel3.Values.Text = "Warrenty Ends on :";
            this.kryptonLabel3.Visible = false;
            // 
            // lblwarretyends
            // 
            this.lblwarretyends.Location = new System.Drawing.Point(151, 443);
            this.lblwarretyends.Name = "lblwarretyends";
            this.lblwarretyends.Size = new System.Drawing.Size(40, 20);
            this.lblwarretyends.TabIndex = 58;
            this.lblwarretyends.Values.Text = "...........";
            this.lblwarretyends.Visible = false;
            // 
            // findWarranty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(871, 488);
            this.Controls.Add(this.kryptonLabel3);
            this.Controls.Add(this.WType);
            this.Controls.Add(this.lblwarretyends);
            this.Controls.Add(this.Wvalue);
            this.Controls.Add(this.kryptonLabel4);
            this.Controls.Add(this.lblSaledon);
            this.Controls.Add(this.kryptonLabel1);
            this.Controls.Add(this.kryptonButton1);
            this.Controls.Add(this.Purchasedetails);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnFind);
            this.Controls.Add(this.txtSerialNo);
            this.Controls.Add(this.kryptonLabel2);
            this.Name = "findWarranty";
            this.Text = "Find Warranty";
            this.Load += new System.EventHandler(this.findWarranty_Load);
            ((System.ComponentModel.ISupportInitialize)(this.drgsales)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.Purchasedetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.drgPurchase)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtSerialNo;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnFind;
        private System.Windows.Forms.DataGridView drgsales;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox Purchasedetails;
        private System.Windows.Forms.DataGridView drgPurchase;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblSaledon;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel Wvalue;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel WType;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblwarretyends;
    }
}