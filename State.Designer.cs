namespace Sys_Sols_Inventory
{
    partial class State
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
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.btn_close = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btn_new = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btn_Save = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonLabel98 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txt_statecode = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel99 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txt_statename = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgv_state = new System.Windows.Forms.DataGridView();
            this.Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StateName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox6.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_state)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.btn_close);
            this.groupBox6.Controls.Add(this.btn_new);
            this.groupBox6.Controls.Add(this.btn_Save);
            this.groupBox6.Controls.Add(this.kryptonLabel98);
            this.groupBox6.Controls.Add(this.txt_statecode);
            this.groupBox6.Controls.Add(this.kryptonLabel99);
            this.groupBox6.Controls.Add(this.txt_statename);
            this.groupBox6.Location = new System.Drawing.Point(10, 4);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(295, 116);
            this.groupBox6.TabIndex = 76;
            this.groupBox6.TabStop = false;
            // 
            // btn_close
            // 
            this.btn_close.Location = new System.Drawing.Point(200, 76);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(75, 25);
            this.btn_close.TabIndex = 176;
            this.btn_close.Values.Text = "Close";
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // btn_new
            // 
            this.btn_new.Location = new System.Drawing.Point(15, 76);
            this.btn_new.Name = "btn_new";
            this.btn_new.Size = new System.Drawing.Size(75, 25);
            this.btn_new.TabIndex = 175;
            this.btn_new.Values.Text = "New";
            this.btn_new.Click += new System.EventHandler(this.btn_new_Click);
            // 
            // btn_Save
            // 
            this.btn_Save.Location = new System.Drawing.Point(98, 76);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(94, 25);
            this.btn_Save.TabIndex = 175;
            this.btn_Save.Values.Text = "Save";
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // kryptonLabel98
            // 
            this.kryptonLabel98.Location = new System.Drawing.Point(10, 20);
            this.kryptonLabel98.Name = "kryptonLabel98";
            this.kryptonLabel98.Size = new System.Drawing.Size(76, 20);
            this.kryptonLabel98.TabIndex = 71;
            this.kryptonLabel98.Values.Text = "State Code :";
            // 
            // txt_statecode
            // 
            this.txt_statecode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_statecode.Location = new System.Drawing.Point(96, 20);
            this.txt_statecode.MaxLength = 1000;
            this.txt_statecode.Name = "txt_statecode";
            this.txt_statecode.Size = new System.Drawing.Size(180, 20);
            this.txt_statecode.TabIndex = 73;
            this.txt_statecode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_statecode_KeyDown);
            // 
            // kryptonLabel99
            // 
            this.kryptonLabel99.Location = new System.Drawing.Point(10, 46);
            this.kryptonLabel99.Name = "kryptonLabel99";
            this.kryptonLabel99.Size = new System.Drawing.Size(80, 20);
            this.kryptonLabel99.TabIndex = 72;
            this.kryptonLabel99.Values.Text = "State Name :";
            // 
            // txt_statename
            // 
            this.txt_statename.Location = new System.Drawing.Point(96, 46);
            this.txt_statename.MaxLength = 1000;
            this.txt_statename.Name = "txt_statename";
            this.txt_statename.Size = new System.Drawing.Size(180, 20);
            this.txt_statename.TabIndex = 74;
            this.txt_statename.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_statename_KeyDown);
            this.txt_statename.Leave += new System.EventHandler(this.txt_statename_Leave);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgv_state);
            this.groupBox1.Location = new System.Drawing.Point(10, 122);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(295, 280);
            this.groupBox1.TabIndex = 177;
            this.groupBox1.TabStop = false;
            // 
            // dgv_state
            // 
            this.dgv_state.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.SeaShell;
            this.dgv_state.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_state.BackgroundColor = System.Drawing.Color.White;
            this.dgv_state.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_state.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Code,
            this.StateName});
            this.dgv_state.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgv_state.Location = new System.Drawing.Point(8, 15);
            this.dgv_state.Name = "dgv_state";
            this.dgv_state.RowHeadersVisible = false;
            this.dgv_state.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgv_state.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_state.Size = new System.Drawing.Size(279, 259);
            this.dgv_state.TabIndex = 0;
            this.dgv_state.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_state_CellClick);
            this.dgv_state.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_state_CellContentDoubleClick);
            // 
            // Code
            // 
            this.Code.DataPropertyName = "CODE";
            this.Code.HeaderText = "CODE";
            this.Code.Name = "Code";
            // 
            // StateName
            // 
            this.StateName.DataPropertyName = "DESC_ENG";
            this.StateName.HeaderText = "STATE NAME";
            this.StateName.Name = "StateName";
            this.StateName.Width = 175;
            // 
            // State
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(316, 412);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox6);
            this.Name = "State";
            this.Text = "State";
            this.Load += new System.EventHandler(this.State_Load);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_state)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox6;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel98;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txt_statecode;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel99;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txt_statename;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btn_close;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btn_new;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btn_Save;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgv_state;
        private System.Windows.Forms.DataGridViewTextBoxColumn Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn StateName;
    }
}