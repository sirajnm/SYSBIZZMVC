using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Compression;
using System.IO;
using Ionic.Zlib;
using Ionic.Zip;
using System.Data.SqlClient;
using Microsoft.SqlServer.Server;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management;
using Microsoft.SqlServer.Management.Common;


namespace Sys_Sols_Inventory
{
    public partial class Backups : Form
    {
        private SqlConnection conn = Model.DbFunctions.GetConnection();
        private BindingSource source = new BindingSource();
        delegate void ProgressDelegate(string sMessage);
        public Backups()
        {
            InitializeComponent();
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            BackupClass.Backup1(txtDirectory.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fdb = new FolderBrowserDialog();
            fdb.ShowDialog();
            if (fdb.SelectedPath != null)
            {
                txtDirectory.Text = fdb.SelectedPath.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            txtUpdatepath.Text = openFileDialog1.FileName;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (txtUpdatepath.Text == "")
            {
                MessageBox.Show("Please select a backup file to restore");
            }
            else
            {
                try
                {
                    SqlConnectionStringBuilder connBuilder = new SqlConnectionStringBuilder(conn.ConnectionString);
                    BackupClass.Restore1(connBuilder.DataSource, connBuilder.AttachDBFilename, txtUpdatepath.Text);
                    MessageBox.Show("The restore of database " + "'" + conn.Database + "'" + " completed sccessfully", "Microsoft SQL Server Management Studio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception exSMO)
                {
                    MessageBox.Show(exSMO.ToString());
                }
            }
        }


        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();            
        }

        private void Backups_Load(object sender, EventArgs e)
        {
            txtDirectory.Text = Properties.Settings.Default.backupLocation;
            txtUpdatepath.Text = Properties.Settings.Default.restoreLocation;
        }

        private void txtDirectory_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.backupLocation = txtDirectory.Text;
            Properties.Settings.Default.Save();
        }

        private void txtUpdatepath_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.restoreLocation = txtUpdatepath.Text;
            Properties.Settings.Default.Save();
        }
    }
}