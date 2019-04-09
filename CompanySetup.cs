using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using ComponentFactory.Krypton.Toolkit;

namespace Sys_Sols_Inventory
{
    public partial class CompanySetup : Form
    {

        Class.CompanySetup cset = new Class.CompanySetup();
        public bool HasArabic = true;
        private string ID;
        private string BranchId="";
        private string Logo;
        
        private string path="";

        public CompanySetup()
        {
            InitializeComponent();
        }

        private void CompanySetup_Load(object sender, EventArgs e)
        {
            HasArabic = General.IsEnabled(Settings.Arabic);
            binddata();
            bindcurrency();
            BindToDataGrid();
            if (!HasArabic)
                txt_arabbranch.Enabled = false;
            ActiveControl = txt_cname;
            btnclear.PerformClick();
        }

        public void binddata()
        {
              DataTable dt = new DataTable();
              dt = cset.getCompanyDetails();
              if (dt.Rows.Count > 0)
              {
                  ID = dt.Rows[0]["Id"].ToString();
                  txt_cname.Text = dt.Rows[0]["Company_Name"].ToString();
                  txt_website.Text = dt.Rows[0]["WebSite"].ToString();
                  txt_pan.Text = dt.Rows[0]["PAN_No"].ToString();
                  txt_tin.Text = dt.Rows[0]["TIN_No"].ToString();
                  txt_cst.Text = dt.Rows[0]["CST_No"].ToString();
                  txt_otherdetails.Text = dt.Rows[0]["Other_details"].ToString();
                  if (dt.Rows[0]["Logo"].ToString() != "")
                  {
                      path = dt.Rows[0]["Logo"].ToString();
                      Image im = GetCopyImage(path);
                      pictureBox1.Image = im;
                     // pictureBox1.Image = Image.FromFile(dt.Rows[0]["Logo"].ToString());
                  }
              }
        }

        private Image GetCopyImage(string path)
        {
            using (Image im = Image.FromFile(path))
            {
                Bitmap bm = new Bitmap(im);
                return bm;
            }
        }

        public void bindcurrency()
        {
            DataTable tableCurrency = new DataTable();
            tableCurrency = cset.getcurrency();
            Drp_Currency.DataSource = tableCurrency;
            Drp_Currency.DisplayMember = "DESC_ENG";
            Drp_Currency.ValueMember = "CODE";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (valid())
            {
                cset.Id = Convert.ToInt16(ID);
                cset.Company_Name = txt_cname.Text;
                cset.Other_Details = txt_otherdetails.Text;
                cset.PAN_No = txt_pan.Text;
                cset.CST_No = txt_cst.Text;
                cset.TIN_No = txt_tin.Text;
                cset.WebSite = txt_website.Text;
                cset.Logo = path;
                cset.updateCompanydetils();
                MessageBox.Show("Updated!");
                binddata();
            }
        }

        public void BindToDataGrid()
        {
            DataTable dat = new DataTable();
            dat = cset.getbaranchdetails();
            grdBranch.DataSource = dat;
            if (!HasArabic)
                grdBranch.Columns[2].Visible = false;
        }


        private bool valid()
        {
            if (txt_cname.Text.Trim() != "" )
            {
                return true;
            }
            else
            {
                MessageBox.Show("Please enter company name");
                return false;
            }
        }

        private bool validbranch()
        {
            if (txt_brnchCode.Text.Trim() != ""&&txt_branch.Text!="")
            {
                return true;
            }
            else
            {
                MessageBox.Show("Please enter the following details \n 1 branch Name\n 2 branch code");
                return false;
            }
        }

        private void btnremove_Click(object sender, EventArgs e)
        {
            if (path != null)
            {
               
                pictureBox1.Image = null;
                 File.Delete(path);
                 path = "";
            }
        }

        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            // open file dialog 
            OpenFileDialog open = new OpenFileDialog();
            string appPath = Path.GetDirectoryName(Application.ExecutablePath) + @"\logo\";
            if (Directory.Exists(appPath) == false)
            {
                Directory.CreateDirectory(appPath);
            }
            // image filters
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp;*.png;)|*.jpg; *.jpeg; *.gif; *.bmp;*.png;";
            if (open.ShowDialog() == DialogResult.OK)
            { 
                
                string iName = open.SafeFileName;
                string filepath = open.FileName;
                if (path != "")
                {
                    File.Delete(path);
                }

                File.Copy(filepath, appPath + iName);
                path = appPath + iName;
                // display image in picture box
                pictureBox1.Image = new Bitmap(open.FileName);
                // image file path
               // textBox1.Text = open.FileName;
            } 

        }

      

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            
            if (validbranch())
            {
                cset.CODE = txt_brnchCode.Text;
                cset.DESC_ENG = txt_branch.Text;
                cset.DESC_ARB = txt_arabbranch.Text;

                if (chkPOSDesk.Checked)
                {
                    cset.ALLOW_APPLIANCES = 1;
                }
                else
                {
                    cset.ALLOW_APPLIANCES = 0;
                }

                if (chkArabic.Checked)
                {
                    cset.ALLOW_SPARES = 1;
                }
                else
                {
                    cset.ALLOW_SPARES = 0;
                }

                if (chkBarcode.Checked)
                {
                    cset.ALLOW_SERVICES = 1;
                }
                else
                {
                    cset.ALLOW_SERVICES = 0;
                }
                cset.ADDRESS_1 = txt_addr1.Text;
                cset.ADDRESS_2 = txt_addr2.Text;
                cset.TELE_1 = txt_phone.Text;
                cset.Email = txt_mail.Text;
                cset.Fax = txt_fax.Text;
                cset.DEFAULT_CURRENCY_CODE = Drp_Currency.SelectedValue.ToString();
                if (BranchId != "")
                {
                    cset.BranchId = BranchId;
                    cset.UpdateBranchDetails();
                }
                else
                    cset.InsertBranch();
                BindToDataGrid();
                btnclear.PerformClick();
            }
        }

        private void grdBranch_Click(object sender, EventArgs e)
        {
            if (grdBranch.Rows.Count > 0 && grdBranch.CurrentRow != null)
            {
                DataGridViewCellCollection c = grdBranch.CurrentRow.Cells;
                BranchId= c[0].Value.ToString();
                txt_brnchCode.Text = c[0].Value.ToString();
                txt_branch.Text = c[1].Value.ToString();
                txt_arabbranch.Text = c[2].Value.ToString();

                chkPOSDesk.Checked = c[3].Value.ToString() == "1";
                chkArabic.Checked = c[4].Value.ToString() == "1";
                chkBarcode.Checked = c[5].Value.ToString() == "1";
            }
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            BranchId = "";
            txt_branch.Text = "";
            txt_brnchCode.Text = "";
            txt_arabbranch.Text = "";
            txt_addr1.Text = "";
            txt_addr2.Text = "";
            txt_phone.Text = "";
            txt_mail.Text = "";
            txt_fax.Text = "";
            chkBarcode.Checked = chkArabic.Checked = chkPOSDesk.Checked = false;
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (BranchId != "")
            {
                if (MessageBox.Show("Are you sure?", "Record Deletion", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    cset.CODE = BranchId;
                    cset.deletebranch();
                    BindToDataGrid();
                    btnclear.PerformClick();
                }
            }
        }

        private void txt_cname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (sender is KryptonTextBox)
                {
                    string name = (sender as KryptonTextBox).Name;
                    switch (name)
                    {
                        case "txt_cname":
                            txt_otherdetails.Focus();
                            break;
                        case "txt_otherdetails":
                            txt_pan.Focus();
                            break;
                        case "txt_pan":
                            txt_tin.Focus();
                            break;
                        case "txt_tin":
                            txt_cst.Focus();
                            break;
                        case "txt_cst":
                            txt_website.Focus();
                            break;
                        case "txt_website":
                            btnSave.Focus();
                            break;
                        case "txt_brnchCode":
                            txt_branch.Focus();
                            break;
                        case "txt_branch":
                            txt_arabbranch.Focus();
                            break;
                        case "txt_arabbranch":
                            txt_addr1.Focus();
                            break;
                        case "txt_addr1":
                            txt_addr2.Focus();
                            break;
                        case "txt_addr2":
                            txt_phone.Focus();
                            break;

                        case "txt_phone":
                            Drp_Currency.Focus();
                            break;
                        case "txt_mail":
                            txt_fax.Focus();
                            break;
                        case "txt_fax":
                            kryptonButton1.Focus();
                            break;
                        default:
                            break;
                    }
                }
                else if (sender is KryptonComboBox)
                {
                    string name = (sender as KryptonComboBox).Name;
                    switch (name)
                    {
                        case "Drp_Currency":
                            txt_mail.Focus();
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
