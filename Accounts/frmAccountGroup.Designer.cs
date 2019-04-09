namespace Sys_Sols_Inventory.Accounts
{
    partial class frmAccountGroup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAccountGroup));
            this.panelMain = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dataGridAccGrp = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtFilter = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.cmbFilter = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.kryptonLabel20 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDelete = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnSave = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnClear = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.ISBUILDIN = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.kryptonLabel13 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.UNDER = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.DESC_ARB = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.DESC_ENG = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.panelMain.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridAccGrp)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbFilter)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UNDER)).BeginInit();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.panel2);
            this.panelMain.Controls.Add(this.panel1);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(834, 509);
            this.panelMain.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dataGridAccGrp);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(482, 509);
            this.panel2.TabIndex = 2;
            // 
            // dataGridAccGrp
            // 
            this.dataGridAccGrp.AllowUserToDeleteRows = false;
            this.dataGridAccGrp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridAccGrp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridAccGrp.Location = new System.Drawing.Point(0, 46);
            this.dataGridAccGrp.Name = "dataGridAccGrp";
            this.dataGridAccGrp.ReadOnly = true;
            this.dataGridAccGrp.Size = new System.Drawing.Size(482, 463);
            this.dataGridAccGrp.TabIndex = 1;
            this.dataGridAccGrp.Click += new System.EventHandler(this.dataGridAccGrp_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txtFilter);
            this.panel3.Controls.Add(this.cmbFilter);
            this.panel3.Controls.Add(this.kryptonLabel20);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(482, 46);
            this.panel3.TabIndex = 0;
            // 
            // txtFilter
            // 
            this.txtFilter.Location = new System.Drawing.Point(214, 14);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(234, 20);
            this.txtFilter.TabIndex = 6;
            this.txtFilter.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            // 
            // cmbFilter
            // 
            this.cmbFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilter.DropDownWidth = 199;
            this.cmbFilter.Location = new System.Drawing.Point(82, 13);
            this.cmbFilter.Name = "cmbFilter";
            this.cmbFilter.Size = new System.Drawing.Size(126, 21);
            this.cmbFilter.TabIndex = 5;
            // 
            // kryptonLabel20
            // 
            this.kryptonLabel20.Location = new System.Drawing.Point(19, 13);
            this.kryptonLabel20.Name = "kryptonLabel20";
            this.kryptonLabel20.Size = new System.Drawing.Size(57, 20);
            this.kryptonLabel20.TabIndex = 7;
            this.kryptonLabel20.Values.Text = "Filter By:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.btnClear);
            this.panel1.Controls.Add(this.ISBUILDIN);
            this.panel1.Controls.Add(this.kryptonLabel13);
            this.panel1.Controls.Add(this.UNDER);
            this.panel1.Controls.Add(this.kryptonLabel2);
            this.panel1.Controls.Add(this.DESC_ARB);
            this.panel1.Controls.Add(this.kryptonLabel4);
            this.panel1.Controls.Add(this.DESC_ENG);
            this.panel1.Controls.Add(this.kryptonLabel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(482, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(352, 509);
            this.panel1.TabIndex = 1;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(248, 152);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(90, 25);
            this.btnDelete.TabIndex = 35;
            this.btnDelete.Values.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(34, 152);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 25);
            this.btnSave.TabIndex = 33;
            this.btnSave.Values.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click_1);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(141, 152);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(90, 25);
            this.btnClear.TabIndex = 34;
            this.btnClear.Values.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // ISBUILDIN
            // 
            this.ISBUILDIN.Enabled = false;
            this.ISBUILDIN.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.ISBUILDIN.Location = new System.Drawing.Point(119, 133);
            this.ISBUILDIN.Name = "ISBUILDIN";
            this.ISBUILDIN.Size = new System.Drawing.Size(42, 20);
            this.ISBUILDIN.TabIndex = 31;
            this.ISBUILDIN.Text = "Yes";
            this.ISBUILDIN.Values.Text = "Yes";
            this.ISBUILDIN.Visible = false;
            this.ISBUILDIN.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DESC_ENG_KeyDown);
            // 
            // kryptonLabel13
            // 
            this.kryptonLabel13.Location = new System.Drawing.Point(16, 133);
            this.kryptonLabel13.Name = "kryptonLabel13";
            this.kryptonLabel13.Size = new System.Drawing.Size(59, 20);
            this.kryptonLabel13.TabIndex = 32;
            this.kryptonLabel13.Values.Text = "IsBuildIn:";
            this.kryptonLabel13.Visible = false;
            // 
            // UNDER
            // 
            this.UNDER.AllowDrop = true;
            this.UNDER.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.UNDER.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.UNDER.DropDownWidth = 211;
            this.UNDER.Location = new System.Drawing.Point(116, 109);
            this.UNDER.Name = "UNDER";
            this.UNDER.Size = new System.Drawing.Size(211, 21);
            this.UNDER.TabIndex = 17;
            this.UNDER.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DESC_ENG_KeyDown);
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(15, 109);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(47, 20);
            this.kryptonLabel2.TabIndex = 18;
            this.kryptonLabel2.Values.Text = "Under:";
            // 
            // DESC_ARB
            // 
            this.DESC_ARB.Location = new System.Drawing.Point(116, 84);
            this.DESC_ARB.Name = "DESC_ARB";
            this.DESC_ARB.Size = new System.Drawing.Size(211, 20);
            this.DESC_ARB.TabIndex = 14;
            this.DESC_ARB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DESC_ENG_KeyDown);
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(15, 84);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(71, 20);
            this.kryptonLabel4.TabIndex = 16;
            this.kryptonLabel4.Values.Text = "Arb. Name:";
            // 
            // DESC_ENG
            // 
            this.DESC_ENG.Location = new System.Drawing.Point(116, 58);
            this.DESC_ENG.Name = "DESC_ENG";
            this.DESC_ENG.Size = new System.Drawing.Size(211, 20);
            this.DESC_ENG.TabIndex = 13;
            this.DESC_ENG.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DESC_ENG_KeyDown);
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(15, 58);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(72, 20);
            this.kryptonLabel3.TabIndex = 15;
            this.kryptonLabel3.Values.Text = "Eng. Name:";
            // 
            // frmAccountGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 509);
            this.Controls.Add(this.panelMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmAccountGroup";
            this.Text = "Account Group";
            this.Load += new System.EventHandler(this.frmAccountGroup_Load);
            this.panelMain.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridAccGrp)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbFilter)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UNDER)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panel1;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox ISBUILDIN;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel13;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox UNDER;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox DESC_ARB;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox DESC_ENG;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSave;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnClear;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dataGridAccGrp;
        private System.Windows.Forms.Panel panel3;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtFilter;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel20;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cmbFilter;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnDelete;

    }
}