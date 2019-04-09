namespace Sys_Sols_Inventory.reports
{
    partial class CustomerList
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.CUSTOMERLISTBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.DataSetcustomer = new Sys_Sols_Inventory.reports.DataSetcustomer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Btn_Clear = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnRun = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.SUP_NAME = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.btnSup = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.SUP_CODE = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.CUSTOMERLISTTableAdapter = new Sys_Sols_Inventory.reports.DataSetcustomerTableAdapters.CUSTOMERLISTTableAdapter();
            this.Send_Mail = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.CUSTOMERLISTBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataSetcustomer)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // CUSTOMERLISTBindingSource
            // 
            this.CUSTOMERLISTBindingSource.DataMember = "CUSTOMERLIST";
            this.CUSTOMERLISTBindingSource.DataSource = this.DataSetcustomer;
            // 
            // DataSetcustomer
            // 
            this.DataSetcustomer.DataSetName = "DataSetcustomer";
            this.DataSetcustomer.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Send_Mail);
            this.panel1.Controls.Add(this.Btn_Clear);
            this.panel1.Controls.Add(this.btnRun);
            this.panel1.Controls.Add(this.SUP_NAME);
            this.panel1.Controls.Add(this.btnSup);
            this.panel1.Controls.Add(this.SUP_CODE);
            this.panel1.Controls.Add(this.kryptonLabel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(825, 82);
            this.panel1.TabIndex = 0;
            // 
            // Btn_Clear
            // 
            this.Btn_Clear.Location = new System.Drawing.Point(540, 29);
            this.Btn_Clear.Name = "Btn_Clear";
            this.Btn_Clear.Size = new System.Drawing.Size(90, 25);
            this.Btn_Clear.TabIndex = 21;
            this.Btn_Clear.Values.Text = "Clear";
            this.Btn_Clear.Click += new System.EventHandler(this.Btn_Clear_Click);
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(431, 29);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(90, 25);
            this.btnRun.TabIndex = 20;
            this.btnRun.Values.Text = "Run";
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // SUP_NAME
            // 
            this.SUP_NAME.Location = new System.Drawing.Point(223, 31);
            this.SUP_NAME.Name = "SUP_NAME";
            this.SUP_NAME.Size = new System.Drawing.Size(179, 20);
            this.SUP_NAME.TabIndex = 17;
            // 
            // btnSup
            // 
            this.btnSup.Location = new System.Drawing.Point(197, 29);
            this.btnSup.Name = "btnSup";
            this.btnSup.Size = new System.Drawing.Size(20, 25);
            this.btnSup.TabIndex = 16;
            this.btnSup.Values.Text = "...";
            this.btnSup.Click += new System.EventHandler(this.btnSup_Click);
            // 
            // SUP_CODE
            // 
            this.SUP_CODE.Location = new System.Drawing.Point(119, 32);
            this.SUP_CODE.Name = "SUP_CODE";
            this.SUP_CODE.Size = new System.Drawing.Size(75, 20);
            this.SUP_CODE.TabIndex = 15;
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(24, 32);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(98, 20);
            this.kryptonLabel1.TabIndex = 14;
            this.kryptonLabel1.Values.Text = "Customer Code:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.reportViewer1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 82);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(825, 305);
            this.panel2.TabIndex = 1;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.CUSTOMERLISTBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Sys_Sols_Inventory.reports.Report6.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(825, 305);
            this.reportViewer1.TabIndex = 0;
            // 
            // CUSTOMERLISTTableAdapter
            // 
            this.CUSTOMERLISTTableAdapter.ClearBeforeFill = true;
            // 
            // Send_Mail
            // 
            this.Send_Mail.Location = new System.Drawing.Point(636, 29);
            this.Send_Mail.Name = "Send_Mail";
            this.Send_Mail.Size = new System.Drawing.Size(90, 25);
            this.Send_Mail.TabIndex = 113;
            this.Send_Mail.Values.Text = "Send Mail";
            this.Send_Mail.Click += new System.EventHandler(this.Send_Mail_Click);
            // 
            // CustomerList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(825, 387);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "CustomerList";
            this.Text = "CustomerList";
            this.Load += new System.EventHandler(this.CustomerList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.CUSTOMERLISTBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataSetcustomer)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox SUP_NAME;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSup;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox SUP_CODE;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnRun;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource CUSTOMERLISTBindingSource;
        private DataSetcustomer DataSetcustomer;
        private DataSetcustomerTableAdapters.CUSTOMERLISTTableAdapter CUSTOMERLISTTableAdapter;
        private ComponentFactory.Krypton.Toolkit.KryptonButton Btn_Clear;
        private ComponentFactory.Krypton.Toolkit.KryptonButton Send_Mail;
    }
}