namespace Sys_Sols_Inventory
{
    partial class TheamChanger
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
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.pictureBoxClassic = new System.Windows.Forms.PictureBox();
            this.pictureBoxStandard = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxClassic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxStandard)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(62, 38);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(98, 19);
            this.kryptonLabel1.TabIndex = 7;
            this.kryptonLabel1.Values.Text = "Select The Theam";
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(31, 74);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(55, 19);
            this.kryptonLabel2.TabIndex = 9;
            this.kryptonLabel2.Values.Text = "Standard";
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(31, 212);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(43, 19);
            this.kryptonLabel3.TabIndex = 10;
            this.kryptonLabel3.Values.Text = "Classic";
            // 
            // pictureBoxClassic
            // 
            this.pictureBoxClassic.Image = global::Sys_Sols_Inventory.Properties.Resources.classic;
            this.pictureBoxClassic.Location = new System.Drawing.Point(31, 247);
            this.pictureBoxClassic.Name = "pictureBoxClassic";
            this.pictureBoxClassic.Size = new System.Drawing.Size(226, 82);
            this.pictureBoxClassic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxClassic.TabIndex = 11;
            this.pictureBoxClassic.TabStop = false;
            this.pictureBoxClassic.Click += new System.EventHandler(this.pictureBoxClassic_Click);
            // 
            // pictureBoxStandard
            // 
            this.pictureBoxStandard.Image = global::Sys_Sols_Inventory.Properties.Resources.saved;
            this.pictureBoxStandard.Location = new System.Drawing.Point(31, 99);
            this.pictureBoxStandard.Name = "pictureBoxStandard";
            this.pictureBoxStandard.Size = new System.Drawing.Size(226, 82);
            this.pictureBoxStandard.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxStandard.TabIndex = 8;
            this.pictureBoxStandard.TabStop = false;
            this.pictureBoxStandard.Click += new System.EventHandler(this.pictureBoxStandard_Click);
            // 
            // TheamChanger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(334, 419);
            this.Controls.Add(this.pictureBoxClassic);
            this.Controls.Add(this.kryptonLabel3);
            this.Controls.Add(this.kryptonLabel2);
            this.Controls.Add(this.pictureBoxStandard);
            this.Controls.Add(this.kryptonLabel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "TheamChanger";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TheamChanger";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxClassic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxStandard)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private System.Windows.Forms.PictureBox pictureBoxStandard;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private System.Windows.Forms.PictureBox pictureBoxClassic;
    }
}