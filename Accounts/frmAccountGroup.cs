using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;

namespace Sys_Sols_Inventory.Accounts
{
    public partial class frmAccountGroup : Form
    {
        Class.AccountGroup accgrp = new Class.AccountGroup();
        private BindingSource source = new BindingSource();
        private DataTable tableFilter = new DataTable();
        Initial mdi = (Initial)Application.OpenForms["Initial"];
        private bool HasArabic;
        private string ID = "";
        public frmAccountGroup()
        {
            InitializeComponent();
        }

        public bool valid()
        {
            if (DESC_ENG.Text == "")
            {
                MessageBox.Show("Enter Account Group Name");
                DESC_ENG.Focus();
                return false;
            }
            else
            {
                return true;  
            }
        }



        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Alt | Keys.S))
            {
                btnSave.PerformClick();
                return true;
            }

            if (keyData == (Keys.Escape))
            {
                //   this.Close();
                //ComponentFactory.Krypton.Docking.KryptonDockableNavigator n = mdi.sender as ComponentFactory.Krypton.Docking.KryptonDockableNavigator;
                ComponentFactory.Krypton.Navigator.KryptonPage k = new ComponentFactory.Krypton.Navigator.KryptonPage();
                k = mdi.maindocpanel.SelectedPage;
                if (k.Name == "Home")
                {


                }
                else
                {
                    mdi.maindocpanel.Pages.Remove(k);
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            if (valid())
            {
               
                    accgrp.DESC_ARB = DESC_ARB.Text;
                    accgrp.DESC_ENG = DESC_ENG.Text;
                    if (UNDER.Text == "")
                        accgrp.UNDER = "";
                    else
                        accgrp.UNDER = UNDER.SelectedValue.ToString();
                    //if (ISBUILDIN.Checked == true)
                    //    accgrp.ISBUILTIN = "Y";
                    //else
                        accgrp.ISBUILTIN = "N";
                    if (ID == "")
                    {

                    accgrp.insertAccountGroup();
                    MessageBox.Show("Account Group Added!");
                      }
                  else
                {
                    accgrp.ACOUNTID = Convert.ToInt32(ID);
                    accgrp.UpdateAccountGroup();
                    MessageBox.Show("Account Group Updated!");
                
                }
               btnClear.PerformClick();
                getgrpname();
                GetData();
                DESC_ENG.Focus();
            }
        }

        public void GetData()
        {
            DataTable dt = new DataTable();
            dt = accgrp.SelectAccountGroup();
            
            source.DataSource = dt;
            dataGridAccGrp.DataSource = source;
            dataGridAccGrp.Columns[0].Visible = false;
            dataGridAccGrp.Columns[3].Visible = false;
        }

        public void getgrpname()
        {
            DataTable dt = new DataTable();
            UNDER.DisplayMember = "DESC_ENG";
            UNDER.ValueMember = "ACOUNTID";
            dt = accgrp.SelectAccountGroupName();
            DataRow row = dt.NewRow();
            dt.Rows.InsertAt(row, 0);
            UNDER.DataSource = dt;
          
           
        }
        private void frmAccountGroup_Load(object sender, EventArgs e)
        {
            HasArabic = General.IsEnabled(Settings.Arabic);
            GetData();
            getgrpname();
            tableFilter.Columns.Add("key");
            tableFilter.Columns.Add("value");
            cmbFilter.DataSource = tableFilter;
            cmbFilter.DisplayMember = "value";
            cmbFilter.ValueMember = "key";

            tableFilter.Rows.Add("DESC_ENG", "Eng. Name");
            
            tableFilter.Rows.Add("DESC_ARB", "Arb. Name");
            tableFilter.Rows.Add("UNDER", "Parent Group");
            ActiveControl = DESC_ENG;
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            if (txtFilter.Text == "")
            {
                source.Filter = "";
            }
            else
            {
                source.Filter = cmbFilter.SelectedValue + " LIKE '" + txtFilter.Text + "%'";
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            DESC_ARB.Text = "";
            DESC_ENG.Text = "";
            UNDER.SelectedIndex = 0;
            ID = "";
            DESC_ENG.Focus();
        }

        private void dataGridAccGrp_Click(object sender, EventArgs e)
        {
            if (dataGridAccGrp.Rows.Count > 0 && dataGridAccGrp.CurrentRow != null)
            {
                btnClear.PerformClick();
                DataGridViewCellCollection c = dataGridAccGrp.CurrentRow.Cells;
                ID = Convert.ToString(c["ACOUNTID"].Value); //accgrp.ACOUNTID
                DESC_ENG.Text = Convert.ToString(c["DESC_ENG"].Value);
                DESC_ARB.Text = Convert.ToString(c["DESC_ARB"].Value);
                if(Convert.ToString(c["u"].Value)!="")
                UNDER.SelectedValue = Convert.ToString(c["u"].Value);
                switch (Convert.ToString(c["ISBUILTIN"].Value))
                { 
                    case "Y":
                        ISBUILDIN.Checked = true;
                        break;
                    case "N":
                        ISBUILDIN.Checked = false;
                        break;
                    default:
                        break;
                }
            }
        }

        private void DESC_ENG_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (sender is KryptonTextBox)
                {
                     string name = (sender as KryptonTextBox).Name;
                     switch (name)
                     {
                         case "DESC_ENG":
                             if (HasArabic)
                                 DESC_ARB.Focus();
                             else
                                 UNDER.Focus();
                             
                             break;

                         case "DESC_ARB":
                             UNDER.Focus();
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
                        case "UNDER":
                            btnSave.Focus();
                            break;

                        default:
                            btnSave.Focus();
                            break;
                    }
                }
                else if (sender is KryptonCheckBox)
                {
                    string name = (sender as KryptonCheckBox).Name;
                    switch (name)
                    {
                        case "ISBUILDIN":
                            btnSave.Focus();
                            break;

                        default:
                            break;
                    }
                
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (ID != "")
            {
                if (!ISBUILDIN.Checked)
                {
                    accgrp.ACOUNTID = Convert.ToInt32(ID);
                    DataTable dt = accgrp.UnderUseOrNot();
                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show("Can't delete this group...", "Warning");
                        return;

                    }
                    else
                    {
                        accgrp.deleteUnder();
                        GetData();
                    }
                }
                else
                {
                    MessageBox.Show("Can't delete this group...", "Warning");
                    return;
                }
            }
        }
      
       
    }
}
