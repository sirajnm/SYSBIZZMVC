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
    public partial class Tax_Type : Form
    {
        Class.Tax_Type TP = new Class.Tax_Type();
        Initial mdi = (Initial)Application.OpenForms["Initial"];
        Login lg = (Login)Application.OpenForms["Login"];
        private bool HasArabic = true;
        int callfrom = 0;
        int TaxId = 0, Update = 0;
        public bool AddedfromMaster;
        public Tax_Type()
        {
            InitializeComponent();
            txttaxrate.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
        }

        public Tax_Type(int callto)
        {
            callfrom = callto;
            InitializeComponent();
            txttaxrate.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Update == 0)
                {
                    TP.code = CODE.Text;
                    TP.DESC_ENG = DESC_ENG.Text;
                    TP.DESC_ARB = DESC_ARB.Text;
                    TP.TaxRate = txttaxrate.Text;
                    TP.InsertTax();
                    MessageBox.Show("Tax Type Added Successfully");
                    AddedfromMaster = true;
                    clearfileds();
                    BindTax();
                }
                else
                {
                    TP.TaxId = TaxId;
                    TP.code = CODE.Text;
                    TP.DESC_ENG = DESC_ENG.Text;
                    TP.DESC_ARB = DESC_ARB.Text;
                    TP.TaxRate = txttaxrate.Text;
                    TP.UpdateTax();
                    MessageBox.Show("Tax Type Updated Successfully");
                    clearfileds();
                    BindTax();
                }

            }
            catch
            {
                MessageBox.Show("Something went wrong please try again");
            }


        }

        public void BindTax()
        {
            DataTable dt = new DataTable();
            if (HasArabic)
                dt = TP.GetTax();
            else
                dt = TP.GettaxNoArabic();
            dgCommon.DataSource = dt;

        }



        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {


            if (keyData == (Keys.Escape))
            {
                if (callfrom == 1)
                {
                    this.Close();
                }
                return true;
            }




            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Tax_Type_Load(object sender, EventArgs e)
        {
            HasArabic = General.IsEnabled(Settings.Arabic);
            if (!HasArabic)
                DESC_ARB.Enabled = false;
            PnlArabic.Visible = false;
            BindTax();
        }


        public void clearfileds()
        {
            CODE.Text = "";
            DESC_ARB.Text = "";
            DESC_ENG.Text = "";
            txttaxrate.Text = "";
            Update = 0;
            TaxId = 0;
        }
    
        private void btnClear_Click(object sender, EventArgs e)
        {
            clearfileds();
        }

        private void dgCommon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            TaxId =Convert.ToInt32(dgCommon.CurrentRow.Cells[0].Value);
            CODE.Text = dgCommon.CurrentRow.Cells[1].Value.ToString();
            DESC_ENG.Text = dgCommon.CurrentRow.Cells[2].Value.ToString();
            if(HasArabic)
            DESC_ARB.Text = dgCommon.CurrentRow.Cells[3].Value.ToString();
            txttaxrate.Text = dgCommon.CurrentRow.Cells["TaxRate"].Value.ToString();
            Update = 1;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if(DialogResult.Yes==MessageBox.Show("Are you sure to delete tax type","Confirmation Alert",MessageBoxButtons.YesNo))
                {
                TP.TaxId = TaxId;
                TP.DeleteTax();
                clearfileds();
                BindTax();
                }

            }
            catch
            {
            }

        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            if (callfrom == 1)
            {
                this.Close();
            }
            else
            {
                try
                {
                    if (lg.Theme == "1")
                    {

                        ComponentFactory.Krypton.Navigator.KryptonPage kp = new ComponentFactory.Krypton.Navigator.KryptonPage();

                        mdi.maindocpanel.SelectedPage.Dispose();
                    }
                    else
                    {
                        this.Close();
                        //ComponentFactory.Krypton.Navigator.KryptonPage kp = new ComponentFactory.Krypton.Navigator.KryptonPage();

                        //mdi.maindocpanel.SelectedPage.Dispose();
                    }


                }
                catch
                {
                    this.Close();
                }
            }

        }

        private void CODE_KeyDown(object sender, KeyEventArgs e)
        {
            if (CODE.Text != "")
            {
              if(e.KeyCode==Keys.Enter)
                {
                    DESC_ENG.Focus();
                }
            }
        }

        private void DESC_ENG_KeyDown(object sender, KeyEventArgs e)
        {
            if (DESC_ENG.Text != "")
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txttaxrate.Focus();
                }
            }
        }

        private void txttaxrate_KeyDown(object sender, KeyEventArgs e)
        {
            if (txttaxrate.Text != "")
            {
                if (e.KeyCode == Keys.Enter)
                {
                    btnSave.Focus();
                }
            }
        }
    }
}
