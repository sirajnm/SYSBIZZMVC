namespace Sys_Sols_Inventory
{
    partial class Item_Mater_Bulk_Upload
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
            this.btnRun = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.lblFilename = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.kryptonButton1 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonButton2 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonButton3 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.Btn_Clear = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.UploadItem = new System.Windows.Forms.CheckBox();
            this.bt_download = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.chkBarcode = new System.Windows.Forms.CheckBox();
            this.kryptonButton4 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnUpload = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.progressUpload = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.bck_Itemworker = new System.ComponentModel.BackgroundWorker();
            this.barcodewrkr = new System.ComponentModel.BackgroundWorker();
            this.label2 = new System.Windows.Forms.Label();
            this.chkboarder = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(34, 21);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(90, 25);
            this.btnRun.TabIndex = 21;
            this.btnRun.Values.Text = "Browse";
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // lblFilename
            // 
            this.lblFilename.Location = new System.Drawing.Point(143, 22);
            this.lblFilename.Name = "lblFilename";
            this.lblFilename.Size = new System.Drawing.Size(21, 20);
            this.lblFilename.TabIndex = 22;
            this.lblFilename.Values.Text = "....";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Location = new System.Drawing.Point(12, 68);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(884, 326);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Data View";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(6, 19);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(872, 301);
            this.dataGridView1.TabIndex = 0;
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.Location = new System.Drawing.Point(710, 12);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.Size = new System.Drawing.Size(90, 25);
            this.kryptonButton1.TabIndex = 21;
            this.kryptonButton1.Values.Text = "Continue";
            this.kryptonButton1.Click += new System.EventHandler(this.kryptonButton1_Click);
            // 
            // kryptonButton2
            // 
            this.kryptonButton2.Location = new System.Drawing.Point(806, 410);
            this.kryptonButton2.Name = "kryptonButton2";
            this.kryptonButton2.Size = new System.Drawing.Size(90, 25);
            this.kryptonButton2.TabIndex = 21;
            this.kryptonButton2.Values.Text = "Close";
            this.kryptonButton2.Click += new System.EventHandler(this.kryptonButton2_Click);
            // 
            // kryptonButton3
            // 
            this.kryptonButton3.Location = new System.Drawing.Point(656, 410);
            this.kryptonButton3.Name = "kryptonButton3";
            this.kryptonButton3.Size = new System.Drawing.Size(144, 25);
            this.kryptonButton3.TabIndex = 21;
            this.kryptonButton3.Values.Text = "Confirm Upload";
            this.kryptonButton3.Click += new System.EventHandler(this.kryptonButton3_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Excel Files|*.xls;*.xlsx";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // Btn_Clear
            // 
            this.Btn_Clear.Location = new System.Drawing.Point(806, 12);
            this.Btn_Clear.Name = "Btn_Clear";
            this.Btn_Clear.Size = new System.Drawing.Size(90, 25);
            this.Btn_Clear.TabIndex = 24;
            this.Btn_Clear.Values.Text = "Clear";
            this.Btn_Clear.Click += new System.EventHandler(this.Btn_Clear_Click);
            // 
            // UploadItem
            // 
            this.UploadItem.AutoSize = true;
            this.UploadItem.Checked = true;
            this.UploadItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.UploadItem.ForeColor = System.Drawing.Color.Coral;
            this.UploadItem.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.UploadItem.Location = new System.Drawing.Point(554, 418);
            this.UploadItem.Name = "UploadItem";
            this.UploadItem.Size = new System.Drawing.Size(91, 17);
            this.UploadItem.TabIndex = 100;
            this.UploadItem.Text = "Upload Stock";
            this.UploadItem.UseVisualStyleBackColor = true;
            // 
            // bt_download
            // 
            this.bt_download.Location = new System.Drawing.Point(18, 410);
            this.bt_download.Name = "bt_download";
            this.bt_download.Size = new System.Drawing.Size(90, 25);
            this.bt_download.TabIndex = 101;
            this.bt_download.Values.Text = "Download";
            this.bt_download.Click += new System.EventHandler(this.bt_download_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(696, 52);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 1;
            // 
            // chkBarcode
            // 
            this.chkBarcode.AutoSize = true;
            this.chkBarcode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.chkBarcode.ForeColor = System.Drawing.Color.Coral;
            this.chkBarcode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkBarcode.Location = new System.Drawing.Point(394, 418);
            this.chkBarcode.Name = "chkBarcode";
            this.chkBarcode.Size = new System.Drawing.Size(131, 17);
            this.chkBarcode.TabIndex = 102;
            this.chkBarcode.Text = "Item Code as Barcode";
            this.chkBarcode.UseVisualStyleBackColor = true;
            // 
            // kryptonButton4
            // 
            this.kryptonButton4.Location = new System.Drawing.Point(113, 493);
            this.kryptonButton4.Name = "kryptonButton4";
            this.kryptonButton4.Size = new System.Drawing.Size(122, 25);
            this.kryptonButton4.TabIndex = 111;
            this.kryptonButton4.Values.Text = "Generate Barcode";
            this.kryptonButton4.Click += new System.EventHandler(this.kryptonButton4_Click);
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(18, 493);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(89, 25);
            this.btnUpload.TabIndex = 110;
            this.btnUpload.Values.Text = "Upload";
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // progressUpload
            // 
            this.progressUpload.Location = new System.Drawing.Point(65, 474);
            this.progressUpload.Name = "progressUpload";
            this.progressUpload.Size = new System.Drawing.Size(746, 13);
            this.progressUpload.TabIndex = 109;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 473);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 108;
            this.label1.Text = "Progress";
            // 
            // bck_Itemworker
            // 
            this.bck_Itemworker.WorkerReportsProgress = true;
            this.bck_Itemworker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bck_Itemworker_DoWork);
            this.bck_Itemworker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bck_Itemworker_ProgressChanged);
            this.bck_Itemworker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bck_Itemworker_RunWorkerCompleted);
            // 
            // barcodewrkr
            // 
            this.barcodewrkr.WorkerReportsProgress = true;
            this.barcodewrkr.DoWork += new System.ComponentModel.DoWorkEventHandler(this.barcodewrkr_DoWork);
            this.barcodewrkr.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.barcodewrkr_ProgressChanged);
            this.barcodewrkr.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.barcodewrkr_RunWorkerCompleted);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(824, 475);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 112;
            // 
            // chkboarder
            // 
            this.chkboarder.AutoSize = true;
            this.chkboarder.Location = new System.Drawing.Point(18, 524);
            this.chkboarder.Name = "chkboarder";
            this.chkboarder.Size = new System.Drawing.Size(74, 17);
            this.chkboarder.TabIndex = 1193;
            this.chkboarder.Text = "Is Boarder";
            this.chkboarder.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(113, 550);
            this.textBox1.Name = "textBox1";
            this.textBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 1194;
            this.textBox1.Text = "18";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 555);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 13);
            this.label3.TabIndex = 1195;
            this.label3.Text = "Set Top Margine";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 580);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 1197;
            this.label4.Text = "Font Size";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(113, 575);
            this.textBox2.Name = "textBox2";
            this.textBox2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 1196;
            this.textBox2.Text = "6";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 604);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 13);
            this.label5.TabIndex = 1199;
            this.label5.Text = "Fixed Cell Height";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(113, 599);
            this.textBox3.Name = "textBox3";
            this.textBox3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.textBox3.Size = new System.Drawing.Size(100, 20);
            this.textBox3.TabIndex = 1198;
            this.textBox3.Text = "62";
            // 
            // Item_Mater_Bulk_Upload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(908, 625);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.chkboarder);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.kryptonButton4);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.progressUpload);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkBarcode);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.bt_download);
            this.Controls.Add(this.UploadItem);
            this.Controls.Add(this.Btn_Clear);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblFilename);
            this.Controls.Add(this.kryptonButton3);
            this.Controls.Add(this.kryptonButton2);
            this.Controls.Add(this.kryptonButton1);
            this.Controls.Add(this.btnRun);
            this.Name = "Item_Mater_Bulk_Upload";
            this.Text = "ItemMaster Bulk Upload";
            this.Load += new System.EventHandler(this.Item_Mater_Bulk_Upload_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonButton btnRun;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblFilename;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton2;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton3;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton Btn_Clear;
        private System.Windows.Forms.CheckBox UploadItem;
        private ComponentFactory.Krypton.Toolkit.KryptonButton bt_download;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.CheckBox chkBarcode;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton4;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnUpload;
        private System.Windows.Forms.ProgressBar progressUpload;
        private System.Windows.Forms.Label label1;
        private System.ComponentModel.BackgroundWorker bck_Itemworker;
        private System.ComponentModel.BackgroundWorker barcodewrkr;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkboarder;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox3;
    }
}