using ComponentFactory.Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sys_Sols_Inventory
{
    public partial class Initial_Setup_2 : Form
    {
        string companyName;
        Class.CompanySetup cset = new Class.CompanySetup();
        InputLanguage arabic;
        InputLanguage english;
        
        int TogMove;
        int MvalX;
        int MvalY;
        public Initial_Setup_2()
        {
            InitializeComponent();
        }

        private void btn_Next_Click(object sender, EventArgs e)
        {
            if (validbranch())
            {
                cset.CODE = txt_brnchCode.Text;
                cset.DESC_ENG = txt_branch.Text;
                cset.DESC_ARB = txt_arabbranch.Text;

                var checkedButton = panelPos.Controls.OfType<RadioButton>()
                                          .FirstOrDefault(r => r.Checked);
                switch (checkedButton.Text)
                {
                    case "Yes":
                        cset.POSTerminal = true;
                        break;
                    case "No":
                        cset.POSTerminal = false;
                        break;
                    default:
                        cset.POSTerminal = false;
                        break;
                }

                var checkedsp = panelArabic.Controls.OfType<RadioButton>()
                                          .FirstOrDefault(r => r.Checked);
                switch (checkedsp.Text)
                {
                    case "Yes":
                        cset.Arabic = true;
                        break;
                    case "No":
                        cset.Arabic=false;
                        break;
                    default:
                        cset.Arabic=false;
                        break;
                }

                var checkedser = panelbarcode.Controls.OfType<RadioButton>()
                                          .FirstOrDefault(r => r.Checked);
                switch (checkedser.Text)
                {
                    case "Yes":
                        cset.BARCODE=true;
                        break;
                    case "No":
                        cset.BARCODE=false;
                        break;
                    default:
                        cset.BARCODE=false;
                        break;
                }
                cset.ALLOW_APPLIANCES = 0;
                cset.ALLOW_SPARES = 0;
                cset.ALLOW_SERVICES = 0;
                cset.ADDRESS_1 = txt_addr1.Text;
                cset.ADDRESS_2 = txt_addr2.Text;
                cset.TELE_1 = txt_phone.Text;
                cset.Email = txt_mail.Text;
                cset.Fax = txt_fax.Text;
                cset.DEFAULT_CURRENCY_CODE = Drp_Currency.SelectedValue.ToString();
                cset.ARBADDRESS_1 = ARBADRESS1.Text;
                cset.ARBADDRESS_2 = ARBADRESS2.Text;
                cset.InsertBranch();
                cset.Company_Name = companyName;
                cset.CODE = txt_brnchCode.Text;
                cset.syssetup_addcurrentbranch();
                Login log = new Login();
                this.Hide();
                log.Show();
            }
        }

        private void Btn_Close_Click(object sender, EventArgs e)
        {
            //Application.Exit();
            Login log = new Login();
            this.Hide();
            log.Show();
        }

        public void bindcurrency()
        {
            DataTable tableCurrency = new DataTable();
            tableCurrency = cset.getcurrency();
            Drp_Currency.DataSource = tableCurrency;
            Drp_Currency.DisplayMember = "DESC_ENG";
            Drp_Currency.ValueMember = "CODE";
        }

        private bool validbranch()
        {
            if (txt_brnchCode.Text.Trim() != "" && txt_branch.Text != "")
            {
                return true;
            }
            else
            {
                MessageBox.Show("Please enter the following details \n 1 branch Name\n 2 branch code");
                return false;
            }
        }


        int indexofenglish;
        private void Initial_Setup_2_Load(object sender, EventArgs e)
        {
            bindcurrency();
            DataTable dt = new DataTable();
            dt = cset.SysSetup_selectcompany();
            if (dt.Rows.Count > 0)
            {
                companyName = dt.Rows[0][1].ToString();
            }


            try
            {
                arabic = InputLanguage.CurrentInputLanguage;
                english = InputLanguage.CurrentInputLanguage;
                int count = InputLanguage.InstalledInputLanguages.Count;
                for (int i = 0; i <= count - 1; i++)
                {
                    if (InputLanguage.InstalledInputLanguages[i].LayoutName.Contains("Arabic"))
                    {
                        arabic = InputLanguage.InstalledInputLanguages[i];
                    }
                    if (InputLanguage.InstalledInputLanguages[i].LayoutName.Contains("English"))
                    {
                        english = InputLanguage.InstalledInputLanguages[i];
                        indexofenglish = i;
                    }
                }
            }
            catch
            {
            }

           
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {

            TogMove = 1;
            MvalX = e.X;
            MvalY = e.Y;
        }

        private void panel2_MouseUp(object sender, MouseEventArgs e)
        {
            TogMove = 0;
        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (TogMove == 1)
            {
                this.SetDesktopLocation(MousePosition.X - MvalX, MousePosition.Y - MvalY);
            }
        }

        Class.Translation Transl = new Class.Translation();

        private void txt_brnchCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (sender is KryptonTextBox)
                {
                    string name = (sender as KryptonTextBox).Name;
                    switch (name)
                    {
                        case "txt_brnchCode":
                            txt_branch.Focus();
                            break;
                        case "txt_branch":
                            try
                            {
                                if(radioSpareYes.Checked)
                                txt_arabbranch.Text = Transl.TranslateText(txt_branch.Text.ToLower(), "en|ar");
                            }
                            catch
                            {
                            }
                            txt_arabbranch.Focus();
                            break;
                        case "txt_arabbranch":
                            txt_addr1.Focus();
                            break;
                        case "txt_addr1":
                            try
                            {
                                if (radioSpareYes.Checked)
                                ARBADRESS1.Text = Transl.TranslateText(txt_addr1.Text.ToLower(), "en|ar");
                            }
                            catch
                            {
                            }
                            txt_addr2.Focus();
                            break;
                        case "txt_addr2":
                            try
                            {
                               if (radioSpareYes.Checked)
                                ARBADRESS2.Text = Transl.TranslateText(txt_addr2.Text.ToLower(), "en|ar");
                            }
                            catch
                            {
                            }
                            txt_phone.Focus();
                            break;


                        case "txt_phone":
                            Drp_Currency.Focus();
                            break;
                        case "txt_mail":
                            txt_fax.Focus();
                            break;
                        case "txt_fax":
                            radioYes.Focus();
                            break;

                    }
                }
                else if (sender is KryptonComboBox)
                {
                    string name = (sender as KryptonComboBox).Name;
                    if (name == "Drp_Currency")
                    {
                        txt_mail.Focus();
                    }
                }

            }
        }


       
       

        private void txtEnglish_Enter(object sender, EventArgs e)
        {
            InputLanguage.CurrentInputLanguage = english;
        }

        

        private void lstlnputLanguages_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

        private void ARBADRESS1_Enter(object sender, EventArgs e)
        {
            try
            {

                InputLanguage.CurrentInputLanguage = InputLanguage.InstalledInputLanguages[1];
            }
            catch
            {
            }

        }

        private void txt_addr2_Enter(object sender, EventArgs e)
        {
            try
            {
                InputLanguage.CurrentInputLanguage = InputLanguage.InstalledInputLanguages[indexofenglish];
            }
            catch
            { 
            }
        }

        private void ARBADRESS2_Enter(object sender, EventArgs e)
        {
            try
            {
                InputLanguage.CurrentInputLanguage = InputLanguage.InstalledInputLanguages[1];
            }
            catch
            {
            }

        }

        private void ARBADRESS2_Leave(object sender, EventArgs e)
        {
            try
            {
                InputLanguage.CurrentInputLanguage = InputLanguage.InstalledInputLanguages[indexofenglish];
            }
            catch
            {
            }
        }


        private void ARBADRESS1_Leave(object sender, EventArgs e)
        {
            try
            {
                InputLanguage.CurrentInputLanguage = InputLanguage.InstalledInputLanguages[indexofenglish];
            }
            catch
            {
            }
        }
    }
}
