namespace Sys_Sols_Inventory
{
    partial class Invoice_Type_Report
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
            this.label1 = new System.Windows.Forms.Label();
            this.inv_type = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.inv_type)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(81, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Invoice Type";
            // 
            // inv_type
            // 
            this.inv_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.inv_type.DropDownWidth = 58;
            this.inv_type.Items.AddRange(new object[] {
            "Small Size",
            "Medium Size",
            "A4",
            "Dynamic",
            "Form.8",
            "Form.8B",
            "Half",
            "Estimate",
            "Estimate Half"});
            this.inv_type.Location = new System.Drawing.Point(172, 28);
            this.inv_type.Name = "inv_type";
            this.inv_type.Size = new System.Drawing.Size(119, 21);
            this.inv_type.TabIndex = 95;
            this.inv_type.SelectedIndexChanged += new System.EventHandler(this.inv_type_SelectedIndexChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 84);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(990, 388);
            this.dataGridView1.TabIndex = 96;
            // 
            // Invoice_Type_Report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1014, 484);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.inv_type);
            this.Controls.Add(this.label1);
            this.Name = "Invoice_Type_Report";
            this.Text = "Invoice_Type_Report";
            this.Load += new System.EventHandler(this.Invoice_Type_Report_Load);
            ((System.ComponentModel.ISupportInitialize)(this.inv_type)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox inv_type;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}