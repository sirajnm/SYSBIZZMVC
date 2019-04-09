namespace Sys_Sols_Inventory
{
    partial class StartUp
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartUp));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.Progress = new ColorProgressBar.ColorProgressBar();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 20;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Progress
            // 
            this.Progress.BackColor = System.Drawing.Color.White;
            this.Progress.BarColor = System.Drawing.Color.MediumVioletRed;
            this.Progress.BorderColor = System.Drawing.Color.Transparent;
            this.Progress.FillStyle = ColorProgressBar.ColorProgressBar.FillStyles.Solid;
            this.Progress.Location = new System.Drawing.Point(0, 282);
            this.Progress.Maximum = 100;
            this.Progress.Minimum = 0;
            this.Progress.Name = "Progress";
            this.Progress.Size = new System.Drawing.Size(447, 10);
            this.Progress.Step = 10;
            this.Progress.TabIndex = 1;
            this.Progress.Value = 0;
            // 
            // StartUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(447, 292);
            this.Controls.Add(this.Progress);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "StartUp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "StatrtUp";
            this.Load += new System.EventHandler(this.StatrtUp_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private ColorProgressBar.ColorProgressBar Progress;

    }
}