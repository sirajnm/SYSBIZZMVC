using ComponentFactory.Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace Sys_Sols_Inventory
{
    public partial class Initinal_Setup_1 : Form
    {
        Class.CompanySetup cset = new Class.CompanySetup();

        int TogMove;
        int MvalX;
        int MvalY;
        private string path = "";
        DataTable dt = new DataTable();
        public Initinal_Setup_1()
        {
            InitializeComponent();
        }

        private bool valid()
        {
            if (txt_cname.Text.Trim() != "")
            {
                return true;
            }
            else
            {
                MessageBox.Show("Please enter company name");
                return false;
            }
        }

        public void AddXlInfor()
        {
            try
            {
                XElement xml = new XElement("Persons",
                   new XElement("Person",
                       new XElement("Name", txt_cname.Text),
                       new XElement("City", txt_cst.Text),
                       new XElement("Age", txt_tin.Text)
                   )
               );

                xml.Save("company.xml");
            }
            catch(Exception ee)
            {
              //  MessageBox.Show(ee.Message);
            }
        }

        private void btn_Next_Click(object sender, EventArgs e)
        {
            if (valid())
            {
                cset.Company_Name = txt_cname.Text;
                cset.ARBCompany_Name = ArbCname.Text;
                cset.PAN_No = txt_pan.Text;
                cset.CST_No = txt_cst.Text;
                cset.TIN_No = txt_tin.Text;
                cset.WebSite = txt_website.Text;
                cset.Logo = path;
                cset.SDate = dtpFrom.Value;
                cset.EDate = dtpTo.Value;
                cset.Status = true;
                cset.Country = cmbCountries.SelectedValue.ToString() ;
                cset.insertcompanydetails();
                cset.insertFinancialYear();
                cset.SysSetup_InsertCompany();
                AddXlInfor();
                updateLedger();
                if (cb_branch.Checked == true)
                {
                    Initial_Setup_2 set = new Initial_Setup_2();
                    set.Show();
                    this.Hide();
                }
                if (cb_branch.Checked == false)
                {
                    cset.CODE = "HDO";
                    cset.DESC_ENG = txt_cname.Text;
                    cset.DESC_ARB = "";
                    cset.POSTerminal = false;
                    cset.BARCODE = true;
                    cset.ALLOW_APPLIANCES = 0;
                    cset.ALLOW_SPARES = 0;
                    cset.ALLOW_SERVICES = 0;
                    cset.ADDRESS_1 = "";
                    cset.ADDRESS_2 = "";
                    cset.TELE_1 = "";
                    cset.Email = "";
                    cset.Fax = "";
                    cset.DEFAULT_CURRENCY_CODE = "INR";
                    cset.ARBADDRESS_1 = "";
                    cset.ARBADDRESS_2 = "";
                    cset.InsertBranch();
                    Login log = new Login();
                    this.Hide();
                    log.Show();
                }

            }
        }
        public void bindTaxType()
        {
            Dictionary<string, string> taxTypeDic = new Dictionary<string, string>();
            taxTypeDic.Add("VAT", "VAT");
            taxTypeDic.Add("GST", "GST");
            taxTypeDic.Add("GST(INDIA)", "GST");
            taxType.DisplayMember = "Key";
            taxType.ValueMember = "Value";
            taxType.DataSource= new BindingSource(taxTypeDic, null);
        }
        void updateLedger()
        {
            Properties.Settings.Default.Tax_Type = taxType.Text;
            Properties.Settings.Default.Save();
            cset.updateTaxLedger(66, "INPUT " + taxType.SelectedValue.ToString());
            cset.updateTaxLedger(83,"OUTPUT "+ taxType.SelectedValue.ToString());
        }
        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
           
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string appPath = Path.GetDirectoryName(Application.StartupPath) + @"\logo\";
                    if (Directory.Exists(appPath) == false)
                    {
                        Directory.CreateDirectory(appPath);
                    }
                    else
                    {
                        string[] filePaths = Directory.GetFiles(appPath);
                        foreach (string filePath in filePaths)
                            File.Delete(filePath);
                    }
                  

                    string iName = open.SafeFileName;
                    string filepath = open.FileName;


                    File.Copy(filepath, appPath + iName);
                    path = appPath + iName;
                    // display image in picture box
                    pictureBox1.Image = new Bitmap(open.FileName);
                    // image file path
                    // textBox1.Text = open.FileName;
                }
                catch { }

            } 
        }

        private void btnremove_Click(object sender, EventArgs e)
        {
            if (path != "")
            {
                try
                {
                    pictureBox1.Image = null;
                    File.Delete(path);
                    path = "";
                }
                catch { }
            }
        }

        private void Btn_Close_Click(object sender, EventArgs e)
        {
            if (path != "")
            {
                File.Delete(path);
            }
            Application.Exit();
            
        }

        private void Initinal_Setup_1_Load(object sender, EventArgs e)
        {
            Setfinancialyear();
            bindTaxType();
            getCountries();
        }
        void getCountries()
        {
            dt = cset.getCountries();
            cmbCountries.DisplayMember = "DESC_ENG";
            cmbCountries.ValueMember = "CODE";
            cmbCountries.DataSource = dt;
            dt.TableName = "Country";
            dt.WriteXml(Application.StartupPath + @"\country.xml");
        }
        public void Setfinancialyear()
        {
            try
            {
                if (DateTime.Now.Month >= 4)
                {
                    DateTime dt = new DateTime(DateTime.Now.Year, 4, 1);
                    dtpFrom.Value = dt;
                    DateTime dt2 = new DateTime(DateTime.Now.Year + 1, 3, 31);
                    dtpTo.Value = dt2;
                }
                else
                {
                    DateTime dt = new DateTime(DateTime.Now.Year - 1, 4, 1);
                    dtpFrom.Value = dt;
                    DateTime dt2 = new DateTime(DateTime.Now.Year, 3, 31);
                    dtpTo.Value = dt2;

                }
            }
            catch { }
        }
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            TogMove = 1;
            MvalX = e.X;
            MvalY = e.Y;
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            TogMove = 0;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (TogMove == 1)
            {
                this.SetDesktopLocation(MousePosition.X - MvalX, MousePosition.Y - MvalY);
            }
        }

        Class.Translation Transl = new Class.Translation();
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
                            try
                            {
                                arabic.Visible = true;
                                ArbCname.Text = Transl.TranslateText(txt_cname.Text.ToLower(), "en|ar");
                                arabic.Visible = false;
                            }
                            catch
                            {
                                arabic.Text = "no network";
                                arabic.Visible = true;
                            }
                            txt_website.Focus();
                            break;
                        case "txt_website":
                            cmbCountries.Focus();
                            break;
                        case "txt_pan":
                            txt_cst.Focus();
                            break;
                        case "txt_tin":
                            txt_pan.Focus();
                            break;
                        case "txt_cst":
                            btn_Next.Focus();
                            break;

                    }
                }
                else if (sender is KryptonComboBox)
                {
                    string name = (sender as KryptonComboBox).Name;
                    switch (name)
                    {
                        case "cmbCountries":
                            txt_tin.Focus();
                            break;
                    }
                }
            }
        }

        private void cmbCountries_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCountries.SelectedIndex>=0)
            {
                cset.Country = cmbCountries.SelectedValue.ToString();
                string UIN = cset.getTaxUin();
                if (UIN!="")
                {
                    lblTaxNo.Text = UIN;
                }
            }
        }

        private void Initinal_Setup_1_Leave(object sender, EventArgs e)
        {

        }
    }
}
